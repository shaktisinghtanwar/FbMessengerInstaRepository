using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using Fb_InstaWpf.Helper;
using Fb_InstaWpf.Model;
using System.IO;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Fb_InstaWpf.ViewModel
{
    public class AddLoginUsersViewModel : BaseViewModel
    {
        #region Filds
		private string _txtUserId;
		private string _txtPassword;
        public string TxtUserId 
		{ 
		get{return _txtUserId;} 
		set
		{_txtUserId=value;
		OnPropertyChanged();} 
		}
        public string TxtPassword 
		{ get
			{
			return _txtPassword;
			}
			set
			{
			_txtPassword=value;
			OnPropertyChanged();
			} 
		}
        public string Lblproxy { get; set; }
        private string fileName = Properties.Settings.Default.Filename;
        private ObservableCollection<FacebookUserLoginInfo> _newUserNameInfoList;
        private ObservableCollection<FacebookUserLoginInfo> _deleteListviewItem;

        #endregion

        #region Constructor
		DbHelper _dbHelper=new DbHelper();
        public AddLoginUsersViewModel()
        {
            NewUserClear = new DelegateCommand(NewUserClearCommandHandler, null);
            NewUserCommand = new DelegateCommand(NewUserCommandHandler, null);
            NewUserNameInfoList = new ObservableCollection<FacebookUserLoginInfo>();
            DeleteListviewItem = new ObservableCollection<FacebookUserLoginInfo>();
            clearListViewItemCommand = new DelegateCommand(clearListViewItemCommandHandler, null);
           // cmbUserLoaded = new DelegateCommand(cmbUserLoadedHandler, null);
           // CreateColumn();
            //BindListView();
         Task.Factory.StartNew(() => FillUserList());
            // 
        }

        private void NewUserClearCommandHandler(object obj)
        {
            try
            {
                _txtUserId = string.Empty;
                TxtUserId = string.Empty;
                TxtPassword = string.Empty;
            }
            catch (Exception)
            {

               
            }
        }

        private void FillUserList()
		{
			try
			 {
			 var data = _dbHelper.GetLoggedinUserList( );
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
				NewUserNameInfoList = data;
				
            });
			}
			 catch(Exception) {
				
			}
		}

        void mainWindowViewModel_LoginCommandMethod()
        {
           System.Windows. MessageBox.Show("hello............");
        }
       
		public FacebookUserLoginInfo LoginUserName
		{
			get;
			set;
		}
        private void clearListViewItemCommandHandler(object obj)
        {
		
            try
            {
			FacebookUserLoginInfo facebookUserLoginInfo=new FacebookUserLoginInfo();
		    SqLiteHelper sql = new SqLiteHelper();
			if(LoginUserName !=null) 
			{
				var data=_dbHelper.DeleteUsersByFacebookIdTable(LoginUserName.LoginUserName);
		    Task.Factory.StartNew(() => FillUserList());
			MessageBox.Show("Delete successfully");
			} 
			else 
			{
			 MessageBox.Show("Please select user");
			}
	     	
            }
            catch (Exception )
            {
                
            }
		


        }
		public static event Action<SocialUser> UserAdded;

        public ObservableCollection<FacebookUserLoginInfo> DeleteListviewItem
        {
            get { return _deleteListviewItem; }
            set
            {
                _deleteListviewItem = value;
                OnPropertyChanged("DeleteListviewItem");

            }
        }
        public DelegateCommand NewUserClear { get; set; }

        public ObservableCollection<FacebookUserLoginInfo> NewUserNameInfoList
        {

            get { return _newUserNameInfoList;
			
			}
            set
            {
                _newUserNameInfoList = value;
                OnPropertyChanged("NewUserNameInfoList");

            }
        }

	

        #endregion
        
        #region Property


        #endregion

        #region Command

        public DelegateCommand NewUserCommand { get; set; }
        public DelegateCommand clearListViewItemCommand { get; set; }
        //public DelegateCommand cmbUserLoaded { get; set; }

        #endregion

        #region Method
        SqLiteHelper sql = new SqLiteHelper();
        private void NewUserCommandHandler(object obj)
        {
            if (!string.IsNullOrWhiteSpace(TxtUserId) && !string.IsNullOrWhiteSpace(TxtPassword))
            {

                string Credential = TxtUserId + ":" + TxtPassword;

                SqLiteHelper sql1 = new SqLiteHelper();


                string query1 = "select Count(*) from Users where UserName='" + TxtUserId + "'";

                int count = Convert.ToInt32(sql1.ExecuteScalar(query1));


                if (TxtUserId != null && TxtPassword != null)
                {
                    if (count == 0)
                    {

                        string query = "INSERT INTO Users(UserName,Password,FacebookId) values('" + TxtUserId + "','" + TxtPassword + "','" + TxtUserId + TxtPassword + "')";

                        int yy = sql.ExecuteNonQuery(query);
                        
                        NewUserNameInfoList.Add(new FacebookUserLoginInfo { LoginUserName = TxtUserId });
                        MessageBox.Show("Save successfully");

                    }

                }



                UserAdded(new SocialUser { InboxUserName = TxtUserId, Password = TxtPassword });
                //UserAdded(new SocialUser { InboxUserName = TxtUserId });
                TxtUserId = null;
                TxtPassword = null;
            }
            else
            {
                
                MessageBox.Show("Please enter valid username and password..!");
            }
                

                }
        private DataTable dtuserCredential = new DataTable();

        void CreateColumn()
        {
            dtuserCredential.Columns.Add("UserName");
            dtuserCredential.Columns.Add("Password");
        }
    
        public System.Windows.Controls.ComboBox cmbUser
        {
            get;
            set;
        }

        void BindListView()
        {
            try
            {
                var file = File.ReadAllLines(fileName);
                foreach (string item in file)
                {
                    string[] splitedItems = item.Split(':');
                    NewUserNameInfoList.Add(new FacebookUserLoginInfo { LoginUserName = splitedItems[0] });
                }
            }
            catch (Exception)
            {

            }

        }
        #endregion
    }
}
