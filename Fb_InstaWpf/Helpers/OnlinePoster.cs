using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Threading;
using Fb_InstaWpf.Enums;

namespace Fb_InstaWpf
{
    public class OnlinePoster
    {
        ConcurrentQueue<PostMessage> _producerConsumerCollection;
		public event Action MessagePosterEvent;
		private int flag=0;

        public OnlinePoster()
        {
            _producerConsumerCollection = new ConcurrentQueue<PostMessage>();

           // RestartThread();
        }
		public void ProcessMessage()
		{
		      DbHelper dbHelper = new DbHelper();
			  ObservableCollection<PostMessage> dataResponse = dbHelper.GetMessages();
			  foreach (var item in dataResponse)
			  {
				 if(TryMessagePosting(item)) {
				  dbHelper.UpdateMessageTable(item.Id) ;			
				  MessagePosterEvent();        			  
				 }
			  }			 
		}

      //
      

        private  bool TryMessagePosting(PostMessage message)
		{
			try {
			  switch (message.MessageTypeResponse)
            {
				case 1:
					return TryPostMessageToFacebook(message);
				case 2:
					return TryPostImageToFacebook(message);
				case 3:
					return TryPostMessageToInsta(message);
				case 4:
					return TryPostMessageToFBMessenger(message);
				case 5:
					return TryPostImageToFBMessenger(message);
				default:
					return false;

				//case MessageType.FacebookMessage:
				//	return TryPostMessageToFacebook(message);
				//case MessageType.FacebookImage:
				//	return TryPostImageToFacebook(message);
				//case MessageType.InstaMessage:
				//	return TryPostMessageToInsta(message);
				//case MessageType.FacebookMessengerMessage:
				//	return TryPostMessageToFBMessenger(message);
				//case MessageType.FacebookMessengerImage:
				//	return TryPostImageToFBMessenger(message);
				//default:
				//	return false;
            }
			}
			catch(Exception ex) {
			}
			return true;
		 
        }
        public  ChromeDriver GetDriver()
        {
            return new ChromeDriver();
        }

        public bool TryPostMessageToFBMessenger(PostMessage message)
        {
            try
            {			  
                var chromeWebDriver = GetDriver();
				string url = message.ToUrl;
				chromeWebDriver.Navigate().GoToUrl(url); 
				OnlineFetcher.SetCookies(chromeWebDriver);				
				chromeWebDriver.Navigate().GoToUrl(url);                
                Thread.Sleep(5000);
                
				
				ReadOnlyCollection<IWebElement> writeNode =
                chromeWebDriver.FindElements(By.XPath("//*[@placeholder='Write a reply...']"));
                if (writeNode.Count > 0)
                {
                    Thread.Sleep(4000);
                    writeNode[0].SendKeys(message.Message);
                    Thread.Sleep(4000);
                }
                ReadOnlyCollection<IWebElement> submitnode =
                       chromeWebDriver.FindElements(By.XPath("//*[@type='submit']"));
                if (submitnode.Count > 0)
                {
                    Thread.Sleep(3000);
                    submitnode[1].Click();
                    Thread.Sleep(2000);
					
					return true;
                }
            }
            catch (Exception)
            {

            }
			
			
			
            return false;
        }

        public bool TryPostImageToFBMessenger(PostMessage message)
        {
            var ChromeWebDriver = GetDriver();
			string url = message.ToUrl;
			ChromeWebDriver.Navigate().GoToUrl(url);	
			OnlineFetcher.SetCookies(ChromeWebDriver);				
            ChromeWebDriver.Navigate().GoToUrl(url);
            
			Thread.Sleep(5000);

            var maincontainer = ChromeWebDriver.FindElement(By.XPath("//input[@name='attachment[]']"));
                if (maincontainer != null)
                {
                    maincontainer.SendKeys("C:/Users/Lenovo/Downloads/2878030-128.png");
                }
                var maincontainersend = ChromeWebDriver.FindElement(By.XPath("//div[@class='_4dw3']"));
                if (maincontainersend != null)
                {
                    Thread.Sleep(2000);
                    maincontainersend.Click();
					return true;
                }  
				return false;          
        }

        public bool TryPostImageToFacebook(PostMessage message)
        {
            try
            {
			    var ChromeWebDriver = GetDriver();
			 string url = message.ToUrl;			
			 ChromeWebDriver.Navigate().GoToUrl(url);
			 OnlineFetcher.SetCookies(ChromeWebDriver);
			 ChromeWebDriver.Navigate().GoToUrl(url);
			   Thread.Sleep(4000);
			 
                var maincontainer = ChromeWebDriver.FindElementByXPath("//div[@class='tickerDialogFooter']");

                if (maincontainer != null)
                {
                    maincontainer = ChromeWebDriver.FindElementByClassName("UFICommentPhotoIcon");
                    ((IJavaScriptExecutor)ChromeWebDriver).ExecuteScript("HTMLInputElement.prototype.click = function() {" + "  if(input.type !== 'file') HTMLElement.prototype.click.call(this); " + "}; ");
                    //Thread.Sleep(5000);
                    maincontainer.Click();
                   
                    ((IJavaScriptExecutor)ChromeWebDriver).ExecuteScript("HTMLInputElement.prototype.click = function() {" + "  if(input.type !== 'file') HTMLElement.prototype.click.call(this); " + "}; ");
                    var maincontai = ChromeWebDriver.FindElementByXPath("//input[@type='file']");
                 
                    maincontainer = ChromeWebDriver.FindElementByXPath("//input[@type='file']");
                    maincontainer.SendKeys(message.ImagePath);
                    Thread.Sleep(2000);
                }
                var maincontainersend = ChromeWebDriver.FindElement(By.XPath("//div[@class='_5rp7']"));
                if (maincontainersend != null)
                {
                    maincontainersend.Click();
                    Thread.Sleep(2000);
					//SendKeys.SendWait(@"{ENTER}");
                    maincontainersend.SendKeys(Keys.Enter);
                }
               
            }
            catch (Exception)
            {

            } 
            return false;
        }


        public bool TryPostMessageToFacebook(PostMessage message)
        {
            var ChromeWebDriver = GetDriver();

             string url = message.ToUrl;	
			 ChromeWebDriver.Navigate().GoToUrl(url);				
			 OnlineFetcher.SetCookies(ChromeWebDriver);				
             ChromeWebDriver.Navigate().GoToUrl(url);
            Thread.Sleep(2000);
            ReadOnlyCollection<IWebElement> postcomment = ChromeWebDriver.FindElements(By.XPath("//*[@class='UFICommentContainer']"));
            if (postcomment.Count > 0)
            {
                postcomment[0].Click();
                ReadOnlyCollection<IWebElement> postcomghghment = ChromeWebDriver.FindElements(By.XPath("//*[@class='notranslate _5rpu']"));
                if (postcomghghment.Count > 0)
                {
                    postcomghghment[0].SendKeys(message.Message);
                    Thread.Sleep(3000);
                    postcomghghment[0].SendKeys(OpenQA.Selenium.Keys.Enter);
					return true;
                }

            }
            return false;
        }

        public bool TryPostMessageToInsta(PostMessage message)
        {
            var ChromeWebDriver = GetDriver();
			string url = message.ToUrl;
			ChromeWebDriver.Navigate().GoToUrl(url);
			 OnlineFetcher.SetCookies(ChromeWebDriver);
             ChromeWebDriver.Navigate().GoToUrl(url);
           
            Thread.Sleep(3000);
            var emailElement = ChromeWebDriver.FindElements(By.XPath("//input[@class='_58al']"));
            if (emailElement.Count > 0)
            {
                Thread.Sleep(3000);
                emailElement[0].SendKeys(message.Message);
            }
            Thread.Sleep(3000);
            var sendmessage = ChromeWebDriver.FindElements(By.XPath("//div[@class='_1fn8 _45dg']"));
            //ReadOnlyCollection<IWebElement> sendmessage = _chromeWebDriver.FindElements(By.ClassName("input"));
            if (sendmessage.Count > 0)
            {
                sendmessage[0].Click();
				   return true;
            }
         return false;
        }

    }
}
