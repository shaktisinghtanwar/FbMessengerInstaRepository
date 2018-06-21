﻿using Fb_InstaWpf.Helper;
using Fb_InstaWpf.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Collections;
using Fb_InstaWpf.ViewModel;
using System.Windows;

namespace Fb_InstaWpf
{
    public class OnlineFetcher
    {
        public event Action LoginSuccessEvent;
		public event Action FacebookFetcherEvent;
		public event Action GetFbMessage;
		public event Action GetInstaMessage;

        public ChromeDriver GetDriver()
        {
            var driver = new ChromeDriver();

            return driver;
        }

        public static void SetCookies(ChromeDriver driver)
        {
            if (_cookieJar != null)
            {
                foreach (var cookie in _cookieJar.AllCookies)
                {
                    driver.Manage().Cookies.AddCookie(cookie); ;
                }
            }
        }

        public static ChromeDriver chromeWebDriver { get; set; }

        ChromeOptions _options = new ChromeOptions();
		public bool isLoggedIn=false;

        private static ICookieJar _cookieJar;
		public string passWord;

        public void LoginWithSelenium(string userName, string password)
        {
            try
            {
			passWord=password;
                FbPageInfo fbPageInfo = new FbPageInfo();

				Login(userName,passWord);
				
                    //  chromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/pages/?category=your_pages");
                    // ChromeWebDriver.Navigate().GoToUrl(url);
                    // Thread.Sleep(2000);
                    //ChromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/TP-1996120520653285/inbox/?selected_item_id=100002948674558");
                    try
                    {
                        var emailElement1 = chromeWebDriver.FindElements(By.XPath("//a[@class='_39g5']"));
                        foreach (var item in emailElement1)
                        {
                            string lin1k = item.GetAttribute("href");
                            if (item.GetAttribute("href").Contains("/live_video/launch_composer/?page_id="))
                            {
                                string pageId = lin1k.Replace("https://www.facebook.com/live_video/launch_composer/?page_id=", "");
                            }
                            if (item.GetAttribute("href").Contains("?modal=composer&ref=www_pages_browser_your_pages_section"))
                            {
                                string pageName = lin1k.Replace("https://www.facebook.com/", "").Replace("/?modal=composer&ref=www_pages_browser_your_pages_section", "");
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        _cookieJar = chromeWebDriver.Manage().Cookies;

                        // chromeWebDriver.Quit();

                    }
                    //Thread.Sleep(5000);

                    LoginSuccessEvent();
                     
                    //  Thread.Sleep(2000);
                //}

            }
            catch (Exception)
            {

                //;
            }
        }

		private void Login(string userName,string password)
		{
		//password=passWord;
			try 
			{
			string appStartupPath = System.IO.Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
			const string url = "https://www.facebook.com/pages/?category=your_pages";
			_options.AddArgument("--disable-notifications");
			_options.AddArgument("--disable-extensions");
			_options.AddArgument("--test-type");
			_options.AddArgument("--log-level=3");
			ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService(appStartupPath);
			chromeDriverService.HideCommandPromptWindow = true;
			chromeWebDriver = new ChromeDriver(chromeDriverService,_options);
			chromeWebDriver.Manage().Window.Maximize();
			chromeWebDriver.Navigate().GoToUrl(url);
			try {
				((IJavaScriptExecutor)chromeWebDriver).ExecuteScript("window.onbeforeunload = function(e){};");
			} catch(Exception) {

			}

			ReadOnlyCollection<IWebElement> emailElement = chromeWebDriver.FindElements(By.Id("email"));
			if(emailElement.Count > 0) {
				// emailElement[0].SendKeys("rishusingh77777@gmail.com");

				emailElement[0].SendKeys(userName);

				//CurrentLogedInFacebookUserinfo.Username = facebookUserinfo.Username
			}
			ReadOnlyCollection<IWebElement> passwordElement = chromeWebDriver.FindElements(By.Id("pass"));
			if(passwordElement.Count > 0) {
				passwordElement[0].SendKeys(password);
				// passwordElement[0].SendKeys(FileOperation.Password);

			}
                Thread.Sleep(3000);

                ReadOnlyCollection<IWebElement> signInElement = chromeWebDriver.FindElements(By.Id("loginbutton"));
			if(signInElement.Count > 0) {
				signInElement[0].Click();
				 }
				//}
				Thread.Sleep(3000);

                //var errormsg = chromeWebDriver.FindElement(By.XPath("//div[@class='_4rbf _53ij']"));
                //            if (errormsg != null)
                //            {
                //                    chromeWebDriver.Close();

                //    MessageBox.Show("Username Or Password Incorrect");
                //                return;
                //           }
                var dbHelper = new DbHelper();
                ReadOnlyCollection<IWebElement> profilIdtempnode = chromeWebDriver.FindElements(By.XPath("//div[@data-click='profile_icon']/a"));
				if(profilIdtempnode.Count > 0) {
					var urls = profilIdtempnode[0].GetAttribute("href").ToString();
					profilLoginId = urls.Split('?')[1].Split('=')[1].ToString();
					isLoggedIn = true;
                // var FbId=   dbHelper.getFbId(userName, password);

                   
                    dbHelper.UpdateUsersTable(userName, password,profilLoginId);




                }
			} 
			catch(Exception) 
			{
				
			}
		}

        List<FbUserMessageInfo> messagingFbpageListInfo = new List<FbUserMessageInfo>();

        public void GetAllPaLoggedinUserPages(string userName,string password)
        {

			try {
			if(isLoggedIn==true)
			 {
				GetaalPage(userName);
			} 
			else 
			{
					Login(userName,password);
					GetaalPage(profilLoginId);
			}
			} 
			catch(Exception) 
			{

			}
			
        }

		private void GetaalPage(string perentUserId)
		{
			var listFbPageInfo = new List<FbPageInfo>();
			Queue<string> queueFbCmntImgUrl = new Queue<string>();
			//var chromeWebDriver = GetDriver();
			//SetCookies(chromeWebDriver);
			chromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/pages/?category=your_pages");
			try {
				ReadOnlyCollection<IWebElement> pageNodepageUrl = chromeWebDriver.FindElements((By.XPath("//*[@class='clearfix _1vh8']/a")));
				if(pageNodepageUrl.Count > 0) {
					foreach(var pageNodeItem in pageNodepageUrl) {
						var TemppageNodeItem = pageNodeItem.GetAttribute("href");

						//fbPageInfo.FbPageUrl = TemppageNodeItem;
						//    LstPageUrl.Add(TemppageNodeItem);

						queueFbCmntImgUrl.Enqueue(TemppageNodeItem);
					}
					ArrayList arrlist1 = new ArrayList();
					ArrayList arrlist2 = new ArrayList();
					FbPageInfo fbPageInfo = new FbPageInfo();
					

					var emailElement1 = chromeWebDriver.FindElements(By.XPath("//a[@class='_39g5']"));
					foreach(var item in emailElement1) {
						string lin1k = item.GetAttribute("href");
						if(item.GetAttribute("href").Contains("/live_video/launch_composer/?page_id=")) {
							var pageId = lin1k.Replace("https://www.facebook.com/live_video/launch_composer/?page_id=","");
							arrlist1.Add(pageId);
							//fbPageInfo.FbPageId = pageId;

						}
						if(item.GetAttribute("href").Contains("?modal=composer&ref=www_pages_browser_your_pages_section")) {
							string pageName = lin1k.Replace("https://www.facebook.com/","").Replace("/?modal=composer&ref=www_pages_browser_your_pages_section","");
							arrlist2.Add(pageName);

						}

					}
					var dbHelper = new DbHelper();

					for(int i = 0;i < queueFbCmntImgUrl.Count;i++) 
					{
						fbPageInfo.FbPageId = arrlist1[i].ToString();
						fbPageInfo.FbPageName = arrlist2[i].ToString();
						var fbPageUrl = queueFbCmntImgUrl.Dequeue();
                        fbPageInfo.FbPageUrl = fbPageUrl.Split('?')[0] + "inbox/?";

                        listFbPageInfo.Add(fbPageInfo);
						//     _dbHelper.GetLoginUsers();
						dbHelper.AddFacebookPage(fbPageInfo.FbPageId,fbPageInfo.FbPageName,fbPageInfo.FbPageUrl,perentUserId);

					}

				}
			} catch(Exception) {

			} finally {
				chromeWebDriver.Quit();
			}
		}


        public static void InsertFacebookCommentToDb(List<FbUserMessageInfo> messagingFbpageListInfo)
        {
            for (int i = 0; i < messagingFbpageListInfo.Count; i++)
            {
                var chatFb = messagingFbpageListInfo[i].Message;
                var imagesrcFb = messagingFbpageListInfo[i].loginguserimage;
                var otherimagesrc = messagingFbpageListInfo[i].otheruserimage;

                string query1 = "select Count(*) from TblJobFb where Message='" + chatFb + "'and ImageSource='" + imagesrcFb + "'";
                SqLiteHelper sql1 = new SqLiteHelper();
                int count = Convert.ToInt32(sql1.ExecuteScalar(query1));

                if (count == 0)
                {
                    string query = "INSERT INTO TblJobFb(PlateformType,Message,ImageSource) values('1" + "','" + chatFb + "','" + imagesrcFb + "')";
                    SqLiteHelper sql = new SqLiteHelper();
                    int yy = sql.ExecuteNonQuery(query);
                }
            }
        }

        public FbPageInfo fbPageInfo=new FbPageInfo();

        public void GetInstaMesages(string url)
        {
		 messagingFbpageListInfo = new List<FbUserMessageInfo>();
            var chromeWebDriver = GetDriver();
            try
            {
                var touserid = string.Empty;
                Queue<string> queueInstaImgUrl = new Queue<string>();
                //fbPageInfo.FbComboboxIndexId.ToString();
               
                var dbHelper = new DbHelper();
                Thread.Sleep(3000);
                ListUsernameInfo listUsernameInfo = new ListUsernameInfo();
               // string url = "https://www.facebook.com/TP-1996120520653285/inbox/";
                chromeWebDriver.Navigate().GoToUrl(url);

                //  url = "https://www.facebook.com/TP-1996120520653285/inbox/";

                Thread.Sleep(10000);
                SetCookies(chromeWebDriver);
                Thread.Sleep(4000);
                chromeWebDriver.Navigate().GoToUrl(url);
                Thread.Sleep(7000);
                //  Thread.Sleep(3000);

                ReadOnlyCollection<IWebElement> collection = chromeWebDriver.FindElements(By.ClassName("_32wr"));
                {
                    if (collection.Count > 0)
                    {
                        collection[2].Click();
                        Thread.Sleep(3000);
                    }
                }

                ReadOnlyCollection<IWebElement> profilIdtempnode = chromeWebDriver.FindElements(By.XPath("//div[@data-click='profile_icon']/a"));
                if (profilIdtempnode.Count > 0)
                {
                    var urls = profilIdtempnode[0].GetAttribute("href").ToString();
                    profilIdtempinsta = urls.Split('?')[1].Split('=')[1].ToString();
                }
                Thread.Sleep(10000);
                try
                {
                  //  var pageId = string.Empty;
                    //var elemen = chromeWebDriver.FindElement(By.XPath("//form[@method='post']")).GetAttribute("action");
                    //if (elemen != null)
                    //{
                    //    pageId = elemen.Split('?')[1].Split('=')[1].ToString();
                    //    //urls.Split('?')[1].Split('=')[1].ToString()
                    //}
                }
                catch (Exception)
                {

                   
                }


                ReadOnlyCollection<IWebElement> commentpostImgNodCollection =
                    chromeWebDriver.FindElements(By.XPath(".//*[@class='_11eg _5aj7']/div/div/img"));
                if (commentpostImgNodCollection.Count > 0)
                {
                    for (int i = 0; i < commentpostImgNodCollection.Count; i++)
                    {
                        var DataImg = commentpostImgNodCollection[i].GetAttribute("src");
                        queueInstaImgUrl.Enqueue(DataImg);
                    }
                }

                ReadOnlyCollection<IWebElement> userlistnode = chromeWebDriver.FindElements(By.ClassName("_4k8x"));
                if (userlistnode.Count > 0)
                {
                    foreach (var itemurl in userlistnode)
                    {
                        itemurl.Click();
                        Thread.Sleep(3000);
                        string userName = itemurl.Text;
                        listUsernameInfo.ListUsername = userName;

                        var currentURL = chromeWebDriver.Url;
                        var tempId = currentURL.Split('?')[1].Split('=')[1];
                        touserid = tempId;
                        listUsernameInfo.ListUserId = tempId;
                        listUsernameInfo.InboxNavigationUrl = currentURL;

                        var imgUrl = queueInstaImgUrl.Dequeue();
                        Thread.Sleep(7000);
                        
                        dbHelper.InsertFbMessengerMessage(listUsernameInfo, userName, imgUrl, profilIdtempinsta,pageId, Convert.ToInt32(Enums.TabType.Instagram));

                       // MessageBox.Show(tempId);   

                        Thread.Sleep(3000);
                        var pageSource = chromeWebDriver.PageSource;
                        var htmlDocument = new HtmlDocument();
                        htmlDocument.LoadHtml(pageSource);
                        Thread.Sleep(1000);
                        HtmlNodeCollection commentNode = htmlDocument.DocumentNode.SelectNodes("//div[@class='_4cye _4-u2  _4-u8']");
                        if (commentNode!=null)
                        {
                            messagingFbpageListInfo = new List<FbUserMessageInfo>();
                            foreach (HtmlNode htmlcommentNode in commentNode)
                            {
                                HtmlNode selectNode = htmlcommentNode.SelectSingleNode("//div[@class='_4cyh']");
                                var pagename = selectNode.InnerText;
                                messagingFbpageListInfo.Add(new FbUserMessageInfo { UserType = 0, Message = pagename });

                                HtmlNode pageimg = htmlcommentNode.SelectSingleNode("//img[@class='img']");

                                var imgsrc = pageimg.Attributes["src"].Value.Replace(";", "&");
                                messagingFbpageListInfo.Add(new FbUserMessageInfo { UserType = 3, loginguserFbimage = imgsrc });
                            }
                        }
                        

                        HtmlNodeCollection commentBlock = htmlDocument.DocumentNode.SelectNodes("//div[@class='_3i4- _5aj7']");
                        if (commentBlock!=null)
                        {
                            var commentImg = string.Empty;
                            foreach (HtmlNode commentitem in commentBlock)
                            {
                                var usernameAndComment = commentitem.InnerText.Split();
                                var ccomment = usernameAndComment[0];

                                Regex timeRegex = new Regex(@"utime=(.*?)<");
                                Match matchtime = timeRegex.Match(commentitem.OuterHtml);
                                string msgTimeng = matchtime.Value.Replace("utime=", "").Replace("<", "");
                                var instaCommentTime = msgTimeng.Split('>')[1];
                                messagingFbpageListInfo.Add(new FbUserMessageInfo { UserType = 3, Message = ccomment });

                            }
                        }
                        
						
                        dbHelper.InsertFacebookCommentToDb(messagingFbpageListInfo, profilIdtempinsta, touserid,pageId);
                    }
                }

                // var chromeWebDriver = GetDriver();
                // chromeWebDriver.Navigate().GoToUrl(SelectedFBPageInfo.FBInboxNavigationUrl); navigationUrl
                // chromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/TP-1996120520653285/inbox/?selected_item_id=1996142970651040");



            }
            catch (Exception)
            {

            }
            finally
            {
                chromeWebDriver.Quit();
				GetInstaMessage();
            }
           
        }

        public void GetFacebookMessages(string url)
        {
		  messagingFbpageListInfo = new List<FbUserMessageInfo>(); ;
            var chromeWebDriver = GetDriver();
            try
            {
                var touserid = string.Empty;
                DbHelper dbHelper = new DbHelper();
                List<ListUsernameInfo> _MyListUsernameInfo = new List<ListUsernameInfo>();
                Queue<string> myQueue = new Queue<string>();
                //string url = "https://www.facebook.com/TP-1996120520653285/inbox/";
                Thread.Sleep(10000);
                chromeWebDriver.Navigate().GoToUrl(url);
                Thread.Sleep(4000);
                SetCookies(chromeWebDriver);
                chromeWebDriver.Navigate().GoToUrl(url);
                Thread.Sleep(10000);
                ReadOnlyCollection<IWebElement> LeftTabTempnode = chromeWebDriver.FindElements(By.ClassName("_32wr"));
                if (LeftTabTempnode.Count > 0)
                {
                    LeftTabTempnode[1].Click();
                }
                ReadOnlyCollection<IWebElement> profilIdtempnode = chromeWebDriver.FindElements(By.XPath("//div[@data-click='profile_icon']/a"));
                if (profilIdtempnode.Count > 0)
                {
                    var urls = profilIdtempnode[0].GetAttribute("href").ToString();
                    profilIdtempFb = urls.Split('?')[1].Split('=')[1].ToString();
                }

                Thread.Sleep(10000);
                //var pageId = string.Empty;
                //var elemen = chromeWebDriver.FindElement(By.XPath("//form[@method='post']")).GetAttribute("action");
                //if (elemen != null)
                //{
                //    //pageId = elemen.Split('?')[1].Split('=')[1].ToString();
                //    //urls.Split('?')[1].Split('=')[1].ToString()
                //}
                ListUsernameInfo listUsernameInfo = new ListUsernameInfo();
                var PageSource = chromeWebDriver.PageSource;

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(PageSource);

                HtmlNodeCollection imgNode =
                    htmlDocument.DocumentNode.SelectNodes("//*[@id='u_0_t']/div/div/div/table/tbody/tr/td[1]/div/div[2]/div/div[1]/div/div/div/div/div/div/img");

                if (imgNode != null)
                {
                    foreach (var imgNodeItem in imgNode)
                    {
                        var Getimgurl = imgNodeItem.Attributes["src"].Value.Replace(";", "&");
                        myQueue.Enqueue(Getimgurl);
                    }
                }
                var listNodeElements = htmlDocument.DocumentNode.SelectNodes("//div[@class='_4ik4 _4ik5']");

                ReadOnlyCollection<IWebElement> userlistnode = chromeWebDriver.FindElements(By.ClassName("_4k8x"));
                if (userlistnode.Count > 0)
                {
                    foreach (var itemurl in userlistnode)
                    {
                        Thread.Sleep(3000);
                        itemurl.Click();
                        // Thread.Sleep(3000);
                        string userName = itemurl.Text;
                        listUsernameInfo.ListUsername = userName;
                        var currentURL = chromeWebDriver.Url;
                        var tempId = currentURL.Split('?')[1].Split('=')[1];
                        touserid = tempId;
                        listUsernameInfo.ListUserId = tempId;
                        listUsernameInfo.InboxNavigationUrl = currentURL;
                        _MyListUsernameInfo.Add(listUsernameInfo);
                        var imgUrl = myQueue.Dequeue();

                       
                        dbHelper.InsertFbMessengerMessage(listUsernameInfo, userName, imgUrl, profilIdtempFb,pageId,Convert.ToInt32(Enums.TabType.Facebook));


                        //MessageBox.Show(userName);   
                        ///////////////
                        Thread.Sleep(7000);
                        chromeWebDriver.Navigate().GoToUrl(currentURL);
                        var pageSource = chromeWebDriver.PageSource;
                        htmlDocument = new HtmlDocument();
                        htmlDocument.LoadHtml(pageSource);
                        Thread.Sleep(7000);
                        HtmlNodeCollection commentNode = htmlDocument.DocumentNode.SelectNodes("//div[@class='_5v3q _5jmm _5pat _11m5']");
                        if (commentNode!=null)
                        {
                            messagingFbpageListInfo = new List<FbUserMessageInfo>();

                            foreach (HtmlNode htmlcommentNode in commentNode)
                            {
                                HtmlNode selectNode = htmlcommentNode.SelectSingleNode("//div[@class='_4vv0 _3ccb']");
                                var pagename = selectNode.InnerText;
                                messagingFbpageListInfo.Add(new FbUserMessageInfo { UserType = 0, Message = pagename, otheruserId = tempId });

                                HtmlNode pageimg = htmlcommentNode.SelectSingleNode("//img[@class='scaledImageFitWidth img']");

                                var imgsrc = pageimg.Attributes["src"].Value.Replace(";", "&");
                                messagingFbpageListInfo.Add(new FbUserMessageInfo { UserType = 3, loginguserFbimage = imgsrc, otheruserId = tempId });
                            }
                        }
                       

                        HtmlNodeCollection commentBlock = htmlDocument.DocumentNode.SelectNodes("//div[@class='UFIImageBlockContent _42ef']");

                        if (commentBlock!=null)
                        {
                            var commentImg = string.Empty;
                            foreach (HtmlNode commentitem in commentBlock)
                            {


                                var pagenamee = commentitem.InnerText;
                                var comment = pagenamee.Replace("ManageLikeShow More Reactions ", "").Split('·');
                                var fbComment = comment[0];

                                Regex timeRegex = new Regex(@"title=(.*?)data");
                                Match matchtime = timeRegex.Match(commentitem.OuterHtml);
                                string msgTimeng = matchtime.Value.Replace("title=", "").Replace("data", "").Replace(@"""", "");



                                Regex regex = new Regex(@"src(.*?)alt");
                                Match match = regex.Match(commentitem.InnerHtml);
                                if (match.Length != 0)
                                {
                                    string[] msgId = match.Value.Replace(";", "&").Split('"');
                                    var img = msgId[1];

                                    messagingFbpageListInfo.Add(new FbUserMessageInfo { UserType = 2, otheruserFbimage = img, otheruserId = tempId });
                                }

                            }
                        }
                       
						
                        dbHelper.InsertFacebookCommentToDb(messagingFbpageListInfo, profilIdtempFb, touserid,pageId);
                        ///////////////

                    }
                }

                //chromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/TP-1996120520653285/inbox/?selected_item_id=1996233970641940");


            }
            catch (Exception)
            {

            }
            finally
            {
                chromeWebDriver.Quit();
				GetFbMessage();
            }

            
        }

        

        public void GetFbMessengerMessages(string url)
        {
		//System.Object lockThis = new System.Object(); 
		//  lock (lockThis) 
		//  {
		var chromeWebDriver = GetDriver();
            try
            {
                var touserid = string.Empty;
                List<ListUsernameInfo> _MyListUsernameInfo = new List<ListUsernameInfo>();
                Queue<string> myQueue = new Queue<string>();
                ListUsernameInfo listUsernameInfo = new ListUsernameInfo();
                //string url = "https://www.facebook.com/TP-1996120520653285/inbox/";
                chromeWebDriver.Navigate().GoToUrl(url);
                Thread.Sleep(10000);
                SetCookies(chromeWebDriver);
                Thread.Sleep(4000);
                chromeWebDriver.Navigate().GoToUrl(url);
                Thread.Sleep(10000);

                ReadOnlyCollection<IWebElement> LeftTabTempnode = chromeWebDriver.FindElements(By.ClassName("_32wr"));
                if (LeftTabTempnode.Count > 0)
                {
                    LeftTabTempnode[0].Click();
                }

                ReadOnlyCollection<IWebElement> profilIdtempnode = chromeWebDriver.FindElements(By.XPath("//div[@data-click='profile_icon']/a"));
                if (profilIdtempnode.Count > 0)
                {
                    var urls = profilIdtempnode[0].GetAttribute("href").ToString();
                    profilIdtempmsngr = urls.Split('?')[1].Split('=')[1].ToString();
                }
                Thread.Sleep(10000);
                try
                {
                    try
                    {
                        //var pageId = string.Empty;
                        var elemen = chromeWebDriver.FindElement(By.XPath("//form[@method='post']")).GetAttribute("action");
                        if (elemen != null)
                        {
                            pageId = elemen.Split('?')[1].Split('=')[1].ToString();
                            //urls.Split('?')[1].Split('=')[1].ToString()
                        }
                    }
                    catch (Exception)
                    {

                      
                    }
                }
                catch (Exception)
                {

                    
                }

                var PageSource = chromeWebDriver.PageSource;

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(PageSource);

                HtmlNodeCollection imgNode =
                    htmlDocument.DocumentNode.SelectNodes("//*[@id='u_0_t']/div/div/div/table/tbody/tr/td[1]/div/div[2]/div/div[1]/div/div/div/div/div/div/img");

                if (imgNode != null)
                {
                    foreach (var imgNodeItem in imgNode)
                    {
                        var Getimgurl = imgNodeItem.Attributes["src"].Value.Replace(";", "&");
                        myQueue.Enqueue(Getimgurl);
                    }
                }
                var listNodeElements = htmlDocument.DocumentNode.SelectNodes("//div[@class='_4ik4 _4ik5']");
                var dbHelper = new DbHelper();
                //ReadOnlyCollection<IWebElement> userlistnode = chromeWebDriver.FindElements(By.ClassName("_4k8x"));
                var userlistnode = chromeWebDriver.FindElements(By.XPath("//div[@class='_4k8x']"));
                if (userlistnode.Count > 0)
                {
                    foreach (var itemurl in userlistnode)
                    {
                        Thread.Sleep(1000);
                        itemurl.Click();
                        // Thread.Sleep(3000);
                        string userName = itemurl.Text;
                        listUsernameInfo.ListUsername = userName;
                        var currentURL = chromeWebDriver.Url;
                        var tempId = currentURL.Split('?')[1].Split('=')[1];
                        touserid = tempId;
                        listUsernameInfo.ListUserId = tempId;
                        listUsernameInfo.InboxNavigationUrl = currentURL;
                        _MyListUsernameInfo.Add(listUsernameInfo);
                        var imgUrl = myQueue.Dequeue();
                        //MessageBox.Show(currentURL);   
                        
                        dbHelper.InsertFbMessengerMessage(listUsernameInfo, userName, imgUrl, profilIdtempmsngr, pageId, Convert.ToInt32(Enums.TabType.Messenger));

                        Thread.Sleep(2000);
                        var plateformType = "1";
                        var pageSource = chromeWebDriver.PageSource;
                        htmlDocument = new HtmlDocument();
                        htmlDocument.LoadHtml(pageSource);
                        Thread.Sleep(2000);


                        HtmlNodeCollection imgNodee = htmlDocument.DocumentNode.SelectNodes("//div[@class='_41ud']");
						//for (int second = 0; ; second++)
						//{
						//	if (second >= 17)
						//	{
						//		break;
						//	}
						//	((IJavaScriptExecutor)chromeWebDriver).ExecuteScript("window.scrollTo((0,-1000))", "");
						//	Thread.Sleep(1000);
						//}
						
                        foreach (HtmlNode htmlNodeDiv in imgNodee)
                        {
						
						
					//	var emailElement1 = chromeWebDriver.FindElements(By.XPath("//a[@class='_39g5']"));
					////foreach(var item in emailElement1) {
					//	string lin1k = emailElement1[0].GetAttribute("href");
					//	if(emailElement1[0].GetAttribute("href").Contains("/live_video/launch_composer/?page_id=")) {
					//		 pageId = lin1k.Replace("https://www.facebook.com/live_video/launch_composer/?page_id=","");
							
					//		}
                            var selectSingleNode = htmlNodeDiv.SelectSingleNode("//div[@class='clearfix _o46 _3erg _29_7 direction_ltr text_align_ltr']");

                            if (selectSingleNode != null)
                            {
                                string otheruser = selectSingleNode.InnerText;

                                Regex timeRegex = new Regex(@"data-tooltip-content(.*?)data");
                                Match match1 = timeRegex.Match(selectSingleNode.OuterHtml);
                                string msgTimeng = match1.Value.Replace("data-tooltip-content=", "").Replace("data", "").Replace(@"""", "");
                                messagingFbpageListInfo.Add(new FbUserMessageInfo { UserType = 1, Message = otheruser, OtherUserDateTime = msgTimeng });

                            }

                            var selectSingleimgNode = htmlNodeDiv.SelectSingleNode(".//*[@class='clearfix _o46 _3erg _29_7 direction_ltr text_align_ltr _ylc']");
                            if (selectSingleimgNode != null)
                            {
                                Regex regex = new Regex(@"src(.*?)style");
                                Match match = regex.Match(selectSingleimgNode.InnerHtml);
                                string imgId = match.Value.Replace("src=", "").Replace("style", "").Replace("\"", "").Replace(@"""", "").Replace("amp;", "");

                                Regex timeRegex = new Regex(@"data-tooltip-content(.*?)data");
                                Match match1 = timeRegex.Match(selectSingleimgNode.OuterHtml);
                                string msgTimeng = match1.Value.Replace("data-tooltip-content=", "").Replace("data", "").Replace(@"""", "");
                                messagingFbpageListInfo.Add(new FbUserMessageInfo { UserType = 1, OtherUserDateTime = msgTimeng, otheruserimage = imgId });

                            }
                            var selectSingleNode2 = htmlNodeDiv.SelectSingleNode(".//*[@class='clearfix _o46 _3erg _3i_m _nd_ direction_ltr text_align_ltr']");
                            if (selectSingleNode2 != null)
                            {
                                string loginuser = selectSingleNode2.InnerText;

                                Regex timeRegex = new Regex(@"data-tooltip-content(.*?)data");
                                Match match = timeRegex.Match(selectSingleNode2.OuterHtml);
                                string msgTimeng = match.Value.Replace("data-tooltip-content=", "").Replace("data", "").Replace(@"""", "");
                                messagingFbpageListInfo.Add(new FbUserMessageInfo { UserType = 0, Message = loginuser, OtherUserDateTime = msgTimeng });
                            }

                            HtmlNode selectSingleimgRightNode = htmlNodeDiv.SelectSingleNode(".//*[@class='clearfix _o46 _3erg _3i_m _nd_ direction_ltr text_align_ltr _ylc']");
                            if (selectSingleimgRightNode != null)
                            {
                                Regex regex = new Regex(@"src(.*?)style");
                                Match match = regex.Match(selectSingleimgRightNode.InnerHtml);
                                string msgId = match.Value.Replace("src=", "").Replace("style", "").Replace("\"", "").Replace(@"""", "").Replace("amp;", "");


                                Regex timeRegex = new Regex(@"data-tooltip-content(.*?)data");
                                Match match1 = timeRegex.Match(selectSingleimgRightNode.OuterHtml);
                                string msgTimeng = match1.Value.Replace("data-tooltip-content=", "").Replace("data", "").Replace(@"""", "");
                                messagingFbpageListInfo.Add(new FbUserMessageInfo { UserType = 0, loginguserimage = msgId, OtherUserDateTime = msgTimeng });
                            }
                            dbHelper.InsertFacebookCommentToDb(messagingFbpageListInfo, profilIdtempmsngr, touserid,pageId);
                        }


                    }
                }

                //   chromeWebDriver.Navigate().GoToUrl("https://www.facebook.com/TP-1996120520653285/inbox/?selected_item_id=100002324267540");

              

            }
            catch (Exception)
            {


            }
            finally
            {
                chromeWebDriver.Quit();
				FacebookFetcherEvent();
            }
		 // }

            
        }

        public string profilIdtempmsngr { get; set; }

        public string profilIdtempinsta { get; set; }

        public string profilIdtempFb { get; set; }
		public static string profilLoginId
		{
			get;
			set;
		}
        public string pageId { get; private set; }
    }
}
