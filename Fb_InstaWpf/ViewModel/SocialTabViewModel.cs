using Fb_InstaWpf.Enums;
using Fb_InstaWpf.Helper;
using Fb_InstaWpf.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;

namespace Fb_InstaWpf.ViewModel
{
    public class SocialTabViewModel : BaseViewModel
    {
        private ObservableCollection<SocialUser> _userListInfo;
        private SocialUser _loginUser;

        private SocialUser _selectedUserInfo;

        private SocialUser _activeTabUserInfo;
        OnlineFetcher _onlineFetcher;

        private ObservableCollection<SocialUser> _selectedUsers;

        PubSubEvent<SocialUser> _pubSubEvent;

        string _tabType;
        string _messageToSend;

        DbHelper _dbHelper=new DbHelper();
        
        public SocialUser SelectedItem
        {
            get {return _selectedUserInfo;}
            set
            {
                _selectedUserInfo = value;
                if ( _selectedUserInfo !=null)
                  BindUserMessage(_selectedUserInfo);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SocialUser> UserListInfo
        {
            get {return _userListInfo;}
            set
            {
                _userListInfo = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SocialUser> SelectedUsers
        {
            get {return  _selectedUsers;}
            set
            {
                _selectedUsers = value;
                OnPropertyChanged();
            }
        }

        public SocialUser ActiveTabUser
        {
            get {return _activeTabUserInfo;}
            set
            {
                _activeTabUserInfo = value;
				LinkToSend = _activeTabUserInfo.InboxNavigationUrl;
                OnPropertyChanged();
            }
        }
        public DelegateCommand SendMessageCommand { get; set; }
        public DelegateCommand SendimageCommand { get; set; }
        public DelegateCommand SendMessageInstaCommand { get; set; }
        public DelegateCommand SendimageFBCommand { get; set; }
        public DelegateCommand SendFbCommentCommand { get; set; }

        public SocialUser LoginUser
        {
            get {return _loginUser;}
            set
            {
                _loginUser = value;
                OnPropertyChanged();
            }
        }

        public string MessageToSend
        {
            get {return _messageToSend;}
            set
            {
                _messageToSend = value;
                OnPropertyChanged();
            }
        }

        public string TabType
        {
            get { return _tabType; }
            set
            {
                _tabType = value;
                OnPropertyChanged();
            }
        }

        public SocialTabViewModel(TabType tabType, SocialUser loginUser)
        {
			try 
			{
				 LoginUser = loginUser;
            TabType = tabType.ToString();
			
            SendMessageCommand = new DelegateCommand(SendMessageCommandHandler, null);
            SelectedUsers = new ObservableCollection<SocialUser>();
            SendMessageInstaCommand = new DelegateCommand(SendMessageInstaCommandhandlar, null);
            SendimageFBCommand = new DelegateCommand(SendimageFBCommandHandler, null);
            SendFbCommentCommand = new DelegateCommand(SendFbCommentCommandHandler, null);
			SendimageCommand= new DelegateCommand(SendImageCommandHandler,null);
            _onlineFetcher = new OnlineFetcher();
            _dbHelper = new DbHelper();
			} 
			catch(Exception) 
			{
				
			}
        }

		

        private void OnSelectedUserChanged(SocialUser socialUser)
        {
            if (SelectedUsers == null)
                SelectedUsers = new ObservableCollection<SocialUser>();

            if ( SelectedUsers.FirstOrDefault(s=> s.InboxUserId == socialUser.InboxUserId) == null)
                SelectedUsers.Add(socialUser);
        }

        private static string ShowDialogAndFetchFileName()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg)|*.png|All files (*.*)|*.*";
            string fileName = string.Empty;
            if (openFileDialog.ShowDialog() == true)
            {
                fileName = openFileDialog.FileName;
            }
            else
                MessageBox.Show("Please select image");
            return fileName;
        }

         private void SendImageCommandHandler(object obj)
        {
		 if (ActiveTabUser !=null)
                {
                   
            if (SelectedItem !=null)
            {
                if (SelectedItem.MessageUserType == "Instagram")
                {
                    return;
                }
                try
                {
					
                    string fileName = ShowDialogAndFetchFileName();

					if(!string.IsNullOrEmpty(fileName)) {
						
					
                    if (ActiveTabUser.MessageUserType == "Messenger")
                    {

                        _dbHelper.Add(new PostMessage()
                        {
                            FromUserId = LoginUser.InboxUserId,
                            ToUserId = ActiveTabUser.InboxUserId,
                            Message = null,

                            MessageType = MessageType.FacebookMessengerImage, //FacebookMessengerImage =5
                            ImagePath = fileName,
                            Status = 0,
                            ToUrl = LinkToSend
                        });
                        ActiveTabUser.Messages.Add(new FbUserMessageInfo { UserType = 1, otheruserimage = fileName });
                        MessageToSend = "";
                    }
                    else if (ActiveTabUser.MessageUserType == "Facebook")
                    {
                        _dbHelper.Add(new PostMessage()
                        {
                            FromUserId = LoginUser.InboxUserId,
                            ToUserId = ActiveTabUser.InboxUserId,
                            Message = null,

                            MessageType = MessageType.FacebookImage, //FacebookImage =2,
                            ImagePath = fileName,
                            Status = 0,
                            ToUrl = LinkToSend
                        });
                        //MessageBox.Show("Message save successfully");
                        ActiveTabUser.Messages.Add(new FbUserMessageInfo { UserType = 1, otheruserimage = fileName });
                        MessageToSend = "";
                    }
					}
                }
                catch (Exception)
                {

                }
            }
			 } else {
			  MessageBox.Show("Please select usear tab");
		 }
        }

        private void SendMessageInstaCommandhandlar(object obj)
        {
            _dbHelper.Add(new PostMessage()
            {
                FromUserId = LoginUser.InboxUserId,
                ToUserId = ActiveTabUser.InboxUserId,
                ImagePath = MessageToSend,
                MessageType = MessageType.InstaMessage,
                Status = 0,
                //ToUrl = SelectedItem.InboxNavigationUrl
				ToUrl=LinkToSend
            });
            MessageToSend = string.Empty;
        }

        public void SendMessageCommandHandler(object message)
        { 
		try
				{

				 if (ActiveTabUser !=null)
            {

			if(ActiveTabUser.MessageUserType=="Messenger") 
			{
			_dbHelper.Add(new PostMessage()
               {
				FromUserId = LoginUser.InboxUserId,
				ToUserId = ActiveTabUser.InboxUserId,
	            Message = MessageToSend,
				
				MessageType = MessageType.FacebookMessengerMessage,  //FacebookMessengerMessage =4,
                ImagePath = null,
                Status = 0,
                //ToUrl = SelectedItem.InboxNavigationUrl
				ToUrl=LinkToSend
               });
				//MessageBox.Show("Message save successfully");

				 if (!string.IsNullOrWhiteSpace(MessageToSend))
                    {
                        ActiveTabUser.Messages.Add(new FbUserMessageInfo { UserType = 1, Message = MessageToSend });
                        MessageToSend = "";
                    }
                    else
                    {
                        MessageBox.Show("Text is empty");
						
                    }
				 }

				 if(ActiveTabUser.MessageUserType=="Facebook")
				  {
		//SocialUser socialuser=new SocialUser();
			
					_dbHelper.Add(new PostMessage()
               {
				FromUserId = LoginUser.InboxUserId,
				ToUserId = ActiveTabUser.InboxUserId,
	            Message = MessageToSend,
				
	            MessageType = MessageType.FacebookMessage, //FacebookMessage =1,
				
				//MessageType = MessageType.FacebookMessengerMessage,
                ImagePath = null,
                Status = 0,
                //ToUrl = SelectedItem.InboxNavigationUrl
				ToUrl=LinkToSend
               });
				//MessageBox.Show("Message save successfully");
				if (!string.IsNullOrWhiteSpace(MessageToSend))
                    {
                        ActiveTabUser.Messages.Add(new FbUserMessageInfo { UserType = 1, Message = MessageToSend });
                        MessageToSend = "";
                    }
                    else
                    {
                        MessageBox.Show("Text is empty");
                    }
				 }

				

				 if(ActiveTabUser.MessageUserType=="Instagram")
				  {
				
			
		//SocialUser socialuser=new SocialUser();
			
					_dbHelper.Add(new PostMessage()
               {
				FromUserId = LoginUser.InboxUserId,
				ToUserId = ActiveTabUser.InboxUserId,
	            Message = MessageToSend,
				
	            //MessageType = MessageType.FacebookMessage,
				
				MessageType = MessageType.InstaMessage, //InstaMessage =3,
                ImagePath = null,
                Status = 0,
                //ToUrl = SelectedItem.InboxNavigationUrl
				ToUrl=LinkToSend
               });
				//MessageBox.Show("Message save successfully");
				if (!string.IsNullOrWhiteSpace(MessageToSend))
                    {
                        ActiveTabUser.Messages.Add(new FbUserMessageInfo { UserType = 1, Message = MessageToSend });
                        MessageToSend = "";
                    }
                    else
                    {
                        MessageBox.Show("Text is empty");
                    }
				 }

				 }
				 else
			{
                MessageBox.Show("Please select user tab");
            }

				 }
			catch (Exception)
			{
                
            }
			         
        }
        private void SendimageFBCommandHandler(object obj)
        {
            _dbHelper.Add(new PostMessage()
            {
                FromUserId = LoginUser.InboxUserId,
                ToUserId = ActiveTabUser.InboxUserId,
                Message = MessageToSend,
                MessageType = MessageType.FacebookImage,
				ImagePath = null,
                Status = 0,
                //ToUrl = SelectedItem.InboxNavigationUrl
				ToUrl=LinkToSend

            });
            MessageToSend = string.Empty;
        }
        private void SendFbCommentCommandHandler(object obj)
        {
            _dbHelper.Add(new PostMessage()
            {
                FromUserId = LoginUser.InboxUserId,
                ToUserId = ActiveTabUser.InboxUserId,
                Message = MessageToSend,
                MessageType = MessageType.FacebookMessage,
				ImagePath = null,
                Status = 0,
                //ToUrl = SelectedItem.InboxNavigationUrl
				ToUrl=LinkToSend
            });
            MessageToSend = string.Empty;
        }
		public string LinkToSend
		{
			get;
			set;
		}
        private void BindUserMessage(SocialUser fbpageInboxUserInfo)
        {
            if (!SelectedUsers.Any(m => m.InboxUserId.Equals(fbpageInboxUserInfo.InboxUserId)))
            {
                SelectedUsers.Add(fbpageInboxUserInfo);
                //LinkToSend=fbpageInboxUserInfo.InboxNavigationUrl;
               fbpageInboxUserInfo.Messages = _dbHelper.GetMessengerUserComments(fbpageInboxUserInfo.InboxUserId,fbpageInboxUserInfo.PageId);
             
            }
        }
    }
}
