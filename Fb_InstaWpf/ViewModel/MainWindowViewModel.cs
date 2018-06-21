using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Fb_InstaWpf.Helper;
using Fb_InstaWpf.Model;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Threading;
using Fb_InstaWpf.DbModel;
using Fb_InstaWpf.Enums;

namespace Fb_InstaWpf.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        DatabaseContext _databaseContext;
        SocialUser _loginUser;
        private FbPageInfo _fbPageInfo;
        DbHelper _dbHelper;
        OnlineFetcher _onlineFetcher;
        OnlinePoster _onlinePoster;

        Task _onlineFetcherGetAllPagesTask;
        Task _onlineFetcherFacebookMessengerTask;
        Task _onlineFetcherInstagramMessagesTask;
        Task _onlinePosterTask;


        #region Commands

        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand FetchAllLoggedinUserPages { get; set; }
        public DelegateCommand FbMessengerListCommand { get; set; }
        public DelegateCommand FbPageInboxCommand { get; set; }
        public DelegateCommand InstaInboxCommand { get; set; }
        public DelegateCommand ShowAllLeftSideData { get; set; }

        public DelegateCommand TabCtrlLoaded { get; set; }
        public DelegateCommand Tab2CtrlLoaded { get; set; }
        public DelegateCommand Tab0CtrlLoaded { get; set; }
        public DelegateCommand ImageProgressBarLoaded { get; set; }

        public DelegateCommand CloseTabCommand { get; set; }

        #endregion

        public SocialUser LoginUser
        {
            get {
                return _loginUser; }
            set
            {
                if ( value != null)
                {
                    _loginUser = value;
                    OnPropertyChanged();

                }
				  // Button.IsEnabled = true;
				//   Image.Visibility = Visibility.Hidden;
            }
        }
        public FbPageInfo FbPageInfo
        {
            get { return _fbPageInfo; }
            set
            {
                if (value != null)
                {
                    _fbPageInfo = value;
                    OnPropertyChanged();
                }
            }
        }


 public DelegateCommand NewLoginButtonLoaded { get; set; }

        public ObservableCollection<SocialUser> LoginUsersList
        {
            get { return _loginUsersList; }
            set { _loginUsersList = value; OnPropertyChanged(); }
        }

        public ObservableCollection<FbPageInfo> PageList
        {
            get { return _pageList; }
            set { _pageList = value; OnPropertyChanged(); }
        }

        public SocialTabViewModel MessengerUserListViewModel
        {
            get {return _messengerUserListViewModel;}
            set
            {
                _messengerUserListViewModel = value;
                OnPropertyChanged();
            }
        }
        public SocialTabViewModel InstagramUserListViewModel
        {
            get { return _instaInboxmember; }
            set { _instaInboxmember = value; OnPropertyChanged(); }
        }

        public SocialTabViewModel FacebookUserListViewModel
        {
            get { return _fbPageInboxmember; }
            set { _fbPageInboxmember = value; OnPropertyChanged(); }
        }

        #region Contructor


        public MainWindowViewModel()
        {
			try 
			{
				LoginImageInfo = new ObservableCollection<ImageLoginTextbox>();
				LoginCommand = new DelegateCommand(LoginCommandHandler, null);
				FetchAllLoggedinUserPages = new DelegateCommand(FetchAllLoggedinUserPagesCommandHandler, null);
				FbMessengerListCommand = new DelegateCommand(LeftFbMessengerListCommandHandler, null);
				InstaInboxCommand = new DelegateCommand(LeftInstaInboxCommandHandler, null);
				FbPageInboxCommand = new DelegateCommand(LeftFbPageInboxCommandHandler, null);
				ShowAllLeftSideData = new DelegateCommand(ShowAllLeftSideDataSelectionChangedCommandHandler);
				ImageProgressBarLoaded = new DelegateCommand(ImageProgressBarLoadedCommandHandler, null);
				CloseTabCommand = new DelegateCommand(CloseTab);
				NewLoginButtonLoaded = new DelegateCommand(NewLoginButtonLoadedHandler);
				_databaseContext = new DatabaseContext();
				_onlineFetcher = new OnlineFetcher();
				_onlinePoster = new OnlinePoster();
				_dbHelper = new DbHelper();

				btnUserLogins_Click =new DelegateCommand(AddUserLogins);
				AddLoginUsersViewModel.UserAdded+=UserAdded;
                DbHelper.pagess += AddPagesIntoList;
				_onlineFetcher.LoginSuccessEvent += _onlineFetcher_LoginSuccessEvent;
				Task.Factory.StartNew(() => FillLoginUserList());
				Task.Factory.StartNew(() => FillPageList());
			} 
			catch(Exception) 
			{
				
				
			}

        }

        private void AddPagesIntoList(FbPageInfo obj)
        {
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                PageList.Add(obj);
            });
            //Task.Factory.StartNew(() => FillPageList());
        }

        private void UserAdded(SocialUser obj)
		{
            
                LoginUsersList.Add(obj);
           
		}

		private void AddUserLogins(object obj)
		{
			try 
			{
			AddLoginUsers addLoginUsers = new AddLoginUsers();
            addLoginUsers.ShowDialog();
			}
			catch(Exception) 
			 {
				
				
			}
		}

		 private static int flag = 0;

		public static System.Windows.Controls.Button Button { get; set; }
       private void NewLoginButtonLoadedHandler(object obj)
       {
           Button = obj as System.Windows.Controls.Button;	
		    Image.Visibility = Visibility.Visible;
           //Button.IsEnabled = false;	
       }

	   public static string selectedPageUrl { get; set; }
        private void ShowAllLeftSideDataSelectionChangedCommandHandler(object obj)
        {
			try 
			{

			 Dispatcher.CurrentDispatcher.Invoke(() =>
            {//Button.IsEnabled=true;
                 dataId=obj as System.Windows.Controls.ComboBox;
           FbPageInfo.FbComboboxIndexId = dataId.SelectedValue.ToString();
		   selectedPageUrl = string.Format("https://www.facebook.com/{0}/inbox/", dataId.SelectedValue.ToString());
           //System.Windows.MessageBox.Show("Combobox2");
            FetchLeftMessengerData(dataId.TabIndex);
            });
			//
          
			}
			 catch(Exception)
			 {
				
			}
        }

        private void FetchAllLoggedinUserPagesCommandHandler(object obj)
        {

			try 
			{
                //if (flag > 0) {
                // flag = 0;
                if (LoginUser != null)
                {
                    _onlineFetcherGetAllPagesTask = Task.Factory.StartNew(() => _onlineFetcher.GetAllPaLoggedinUserPages(LoginUser.InboxUserName, LoginUser.Password));

                }
                else
                {
                    System.Windows.MessageBox.Show("There is no 'User' select from dropdown list", "Alert Message Box", MessageBoxButton.OK, MessageBoxImage.Error);
                    Image.Visibility = Visibility.Hidden;
                }

                Task.Factory.StartNew(() => FillPageList());
                //}

            } catch(Exception) {
				
			}
        }

        private void _onlineFetcher_LoginSuccessEvent()
        {
		 string pageUrl = null;
           if (FbPageInfo == null)
           {
		   pageUrl= selectedPageUrl;
			  //pageUrl = PageList.FirstOrDefault().FbPageUrl;
           }
           else
           {
		    pageUrl= selectedPageUrl;
               //pageUrl = FbPageInfo.FbPageUrl;
           }
        
			_onlinePoster.MessagePosterEvent += PostNextMessage;
			_onlinePosterTask = Task.Factory.StartNew(() => _onlinePoster.ProcessMessage());

			_onlineFetcher.FacebookFetcherEvent += FetchFacebookMessage;
			_onlineFetcherFacebookMessengerTask = Task.Factory.StartNew(() => _onlineFetcher.GetFbMessengerMessages(pageUrl));
			_onlineFetcher.GetFbMessage += GetFbMessages;
			_onlineFetcherGetAllPagesTask = Task.Factory.StartNew(() => _onlineFetcher.GetFacebookMessages(pageUrl));
			_onlineFetcher.GetInstaMessage += GetInstaMessages;
			_onlineFetcherInstagramMessagesTask = Task.Factory.StartNew(() => _onlineFetcher.GetInstaMesages(pageUrl));

			
        }
		public void GetInstaMessages()
		{
		 string pageUrl = null;
           if (FbPageInfo == null)
           {
              pageUrl = selectedPageUrl;
           }
           else
           {
               pageUrl = selectedPageUrl;
           }
		_onlineFetcherInstagramMessagesTask = Task.Factory.StartNew(() => _onlineFetcher.GetInstaMesages(pageUrl));
		
		}
		public void GetFbMessages()
		{
		 string pageUrl = null;
           if (FbPageInfo == null)
           {
              pageUrl = selectedPageUrl;
           }
           else
           {
               pageUrl = selectedPageUrl;
           }
		_onlineFetcherGetAllPagesTask = Task.Factory.StartNew(() => _onlineFetcher.GetFacebookMessages(pageUrl));
		
		}
		public void FetchFacebookMessage()
		{
		 string pageUrl = null;
           if (FbPageInfo == null)
           {
                // pageUrl = PageList.FirstOrDefault().FbPageUrl;
                pageUrl = selectedPageUrl;
            }
           else
           {
               pageUrl = selectedPageUrl;
            }
		_onlineFetcherFacebookMessengerTask = Task.Factory.StartNew(() => _onlineFetcher.GetFbMessengerMessages(pageUrl));
		}

		public void PostNextMessage()
		{
            _onlinePosterTask = Task.Factory.StartNew(() => _onlinePoster.ProcessMessage());
		}
        public void FillLoginUserList()
        {
			try
			 {
			 var data = _dbHelper.GetLoginUsers();
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                LoginUsersList = data;
                LoginUser = data.FirstOrDefault();
            });
			
			} catch(Exception)
			 {
				
				
			}
        }

        private void FillPageList()
        {
			try
			 {
			 var data = _dbHelper.GetFacebookPage();
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                PageList = data;
                FbPageInfo = data.FirstOrDefault();
            });
			}
			 catch(Exception) {
				
			}
        }
		public DelegateCommand btnUserLogins_Click
		{
			get;
			set;
		}

        public void LeftFbMessengerListCommandHandler(object obj)
        {
            TabControlSelectedIndex = Convert.ToInt16(obj);
            //FatchLeftMessengerData(obj);
        }

        private void FetchLeftMessengerData(int index)
        {
            
          //  if (MessengerUserListViewModel == null)
                Task.Factory.StartNew(() => LeftMessengerData());
				TabControlSelectedIndex = index;
        }

        private void LeftFbPageInboxCommandHandler(object obj)
        {
            Task.Factory.StartNew(() => LeftFacebookData());
            TabControlSelectedIndex = Convert.ToInt16(obj);
        }

        private void LeftInstaInboxCommandHandler(object obj)
        {
            TabControlSelectedIndex = Convert.ToInt16(obj);
            Task.Factory.StartNew(() => LeftInstagramData());
        }
		public SocialUser _socialUser;
        private void LeftMessengerData()
        {

			try
			 {
			 // if (MessengerUserListViewModel != null) return;
            var data = _dbHelper.GetLeftMessengerListData(LoginUser.InboxUserId, TabType.Messenger, FbPageInfo.FbComboboxIndexId);

            Application.Current.Dispatcher.Invoke(() =>
            {
                MessengerUserListViewModel =  new SocialTabViewModel(Enums.TabType.Messenger,LoginUser)
                {
                    UserListInfo = data
                };
                if (MessengerUserListViewModel.SelectedItem == null)
                    MessengerUserListViewModel.SelectedItem = data.FirstOrDefault();
            });
			}
			catch(Exception) 
			 {
				
				
			}
        }

        private void LeftFacebookData()
        {
			try 
			{
			//if (FacebookUserListViewModel != null) return;
//
            var data = _dbHelper.GetLeftMessengerListData(LoginUser.InboxUserId, TabType.Facebook, FbPageInfo.FbComboboxIndexId);
           // var data = _dbHelper.GetFacebookListData(LoginUser.UserId);
            Application.Current.Dispatcher.Invoke(() =>
            {
                //FacebookUserListViewModel = FacebookUserListViewModel ?? new SocialTabViewModel(Enums.TabType.Facebook,LoginUser)
                FacebookUserListViewModel = new SocialTabViewModel(Enums.TabType.Facebook,LoginUser)
                {
                    UserListInfo = data
                };
                if (FacebookUserListViewModel.SelectedItem == null)
                    FacebookUserListViewModel.SelectedItem = data.FirstOrDefault();
            });
			}
			 catch(Exception) 
			 {
				
				
			}
        }

        private void LeftInstagramData()
        {
			try 
			{
			//if (InstagramUserListViewModel != null) return;

            //var data = _dbHelper.GetInstaUserList(LoginUser.UserId);
            var data = _dbHelper.GetLeftMessengerListData(LoginUser.InboxUserId, TabType.Instagram, FbPageInfo.FbComboboxIndexId);
            Application.Current.Dispatcher.Invoke(() =>
            {
                InstagramUserListViewModel =  new SocialTabViewModel(Enums.TabType.Instagram,LoginUser)
                {
                    UserListInfo = data
                };
                if (InstagramUserListViewModel.SelectedItem == null)
                    InstagramUserListViewModel.SelectedItem = data.FirstOrDefault();
            });
			} 
			catch(Exception) 
			 {
				
				
			}
        }

        private void CloseTab(object obj)
        {
			try 
			{
			string[] array = obj.ToString().Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
            string tabName = array[0];
            string parentTabName = array[1];
            switch (parentTabName)
            {
                case "Messenger":
				if (MessengerUserListViewModel != null && MessengerUserListViewModel.SelectedUsers !=null ){
				
				
                    MessengerUserListViewModel.SelectedUsers.Remove(MessengerUserListViewModel.SelectedUsers.FirstOrDefault(m => m.InboxUserName == tabName));
                    if (MessengerUserListViewModel.SelectedUsers.Count == 0)
                    {
                        MessengerUserListViewModel.SelectedItem = null;
                    }
					}
                    break;
                case "Facebook":
				if (FacebookUserListViewModel != null && FacebookUserListViewModel.SelectedUsers !=null ){
				
                    FacebookUserListViewModel.SelectedUsers.Remove(FacebookUserListViewModel.SelectedUsers.FirstOrDefault(m => m.InboxUserName == tabName));
                    if (FacebookUserListViewModel.SelectedUsers.Count == 0)
                    {
                        FacebookUserListViewModel.SelectedItem = null;
                    }
					}
                    break;
                case "Instagram":
				if (InstagramUserListViewModel != null && InstagramUserListViewModel.SelectedUsers !=null ){
				
                    InstagramUserListViewModel.SelectedUsers.Remove(InstagramUserListViewModel.SelectedUsers.FirstOrDefault(m => m.InboxUserName == tabName));
                    if (InstagramUserListViewModel.SelectedUsers.Count == 0)
                    {
                        InstagramUserListViewModel.SelectedItem = null;
                    }
					}
                    break;

                default:
                    break;
            }
			}
			catch(Exception) 
			 {
				
				
			}
        }

        private void ImageProgressBarLoadedCommandHandler(object obj)
        {
			try {
			Image = obj as System.Windows.Controls.Image;
			Image.Visibility = Visibility.Hidden;
			}
			catch(Exception) 
			 {
				
			
			}
        }

        #endregion

        #region Field

        private ObservableCollection<ImageLoginTextbox> _LoginImageInfo;

        private SocialTabViewModel _fbPageInboxmember;

        public string UrlName;

        private string _textcommet = string.Empty;

        public string DisplayProgressBarPath { get { return @"Images\GrayBar.gif"; } }
       
        #endregion

        #region Property

        #region User Info Details

        public ObservableCollection<ImageLoginTextbox> LoginImageInfo
        {
            get
            {
                return _LoginImageInfo;
            }
            set
            {
                _LoginImageInfo = value;
                OnPropertyChanged("LoginImageInfo");

            }
        }
        #endregion

        #endregion

        #region Methods

    
        private void Showimage()
        {
            Image.Visibility = Visibility.Visible;
        }

        private string fileName = Properties.Settings.Default.Filename;

        public System.Windows.Controls.Image Image { get; set; }

        private void LoginCommandHandler(object obj)
        {
			try 
			{
			 Image.Visibility = Visibility.Visible;
			  flag = 1;
			Button.IsEnabled = false;
                if (LoginUser!=null)
                {
                    _onlineFetcher.LoginWithSelenium(LoginUser.InboxUserName, LoginUser.Password);
                }
                else
                {
                    System.Windows.MessageBox.Show("There is no 'User' select from dropdown list","Alert Message Box",MessageBoxButton.OK,MessageBoxImage.Error);
                    Image.Visibility = Visibility.Hidden;
                }
           
			}
			 catch(Exception) 
			 {
				
				throw;
			}
       }

        private ObservableCollection<SocialUser> _loginUsersList;


        private string _messageToSend;

        private SocialTabViewModel _instaInboxmember;

        private SocialTabViewModel _messengerUserListViewModel;
        private int _tabControlSelectedIndex;
        private ObservableCollection<FbPageInfo> _pageList;
  
            

        #endregion

        public string chat { get; set; }

        public string imagesrc { get; set; }

        public string otherimagesrc { get; set; }
  
        public string MessageToSend
        {
            get {return _messageToSend;}
            set
            {
                _messageToSend = value;
                OnPropertyChanged();
            }
        }

        public int TabControlSelectedIndex
        {
            get { return _tabControlSelectedIndex; }
            set
            {
                _tabControlSelectedIndex = value;

                OnPropertyChanged();
            }
        }

        public string pageId { get; set; }
		public System.Windows.Controls.ComboBox dataId { get; set; }
    }
}
