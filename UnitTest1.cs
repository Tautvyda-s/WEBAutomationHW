using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Security.Claims;

namespace TUTA_Homework
{
    public class Tests
    {
        public IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test, Order(1)]
        // [Ignore("Register New Account Part Done")]
        public void RegisterNewAccount()
        {
            driver.Navigate().GoToUrl("https://www.demoblaze.com/index.html");
            var NaviSignUp = driver.FindElement(By.Id("signin2"));
            NaviSignUp.Click();

            var Username = driver.FindElement(By.Id("sign-username")); 
            var Password = driver.FindElement(By.Id("sign-password"));

            Thread.Sleep(1000); //otherwise can't send values to Username and Password text fields, "Sign up" element needs time to load to be interactable

            Username.SendKeys("Tuta7hwusere");
            Password.SendKeys("Tuta7hwusere");

            
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@class = 'btn btn-primary' and text() = 'Sign up']"))).Click(); //explicit wait until "Sign Up" button is clickable, when it's clickable it's clicked

            wait.Until(ExpectedConditions.AlertIsPresent()); //waits until alert is present
            driver.SwitchTo().Alert().Accept(); //accepts the "Sign Up succesfull" alert
        }

        [Test, Order(2)]
        // [Ignore("Validate correct Sign In part done")]
        public void ValidateSignIn() 
        {
            driver.Navigate().GoToUrl("https://www.demoblaze.com/index.html");
            var NaviLogIn = driver.FindElement(By.Id("login2"));
            NaviLogIn.Click();

            var Username = driver.FindElement(By.Id("loginusername"));
            var Password = driver.FindElement(By.Id("loginpassword"));

            Thread.Sleep(1000); //otherwise can't send values to Username and Password text fields, "Log In" element needs time to load to be interactable

            Username.SendKeys("Tuta7hwusere");
            Password.SendKeys("Tuta7hwusere");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//button[@class='btn btn-primary'])[3]"))).Click(); //explicit wait until "Log In" button is clickable, when it's clickable it's clicked

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@id='nameofuser']"))); //waiting until nameofuser element is visible to get it's text value
            var successLogin = driver.FindElement(By.XPath("//a[@id='nameofuser']"));
            var successLoginText = successLogin.Text;
            Console.WriteLine(successLoginText);

            Assert.That(successLoginText, Is.EqualTo("Welcome Tuta7hwusere"));
        }

        [Test, Order(3)]
        public void SelectItem()
        {
            //3. Select item - MacBook air
            driver.Navigate().GoToUrl("https://www.demoblaze.com/prod.html?idp_=11");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[.='Add to cart']"))).Click();


            wait.Until(ExpectedConditions.AlertIsPresent()); //wait until Alert is present

            var alertText = driver.SwitchTo().Alert();
            Console.WriteLine(alertText.Text);


            Assert.That(alertText.Text, Is.EqualTo("Product added.")); //assert confirmation that laptop was sucessfully added
            driver.SwitchTo().Alert().Accept();
        }

        [Test, Order(4)]
        public void FinishBuyItem()
        {
            //4. Finish buying the item
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[.='Cart']"))).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@class='btn btn-success']"))).Click();

            var Name = driver.FindElement(By.XPath("//input[@id='name']"));
            var Country = driver.FindElement(By.XPath("//input[@id='country']"));
            var City = driver.FindElement(By.XPath("//input[@id='city']"));
            var CreditCard = driver.FindElement(By.XPath("//input[@id='card']"));
            var Month = driver.FindElement(By.XPath("//input[@id='month']"));
            var Year = driver.FindElement(By.XPath("//input[@id='year']"));

            Thread.Sleep(1000); //otherwise can't send values to text fields, "Order form" needs time to load to be interactable

            Name.SendKeys("Tautvydas B");
            Country.SendKeys("Zimbabve");
            City.SendKeys("Yoko");
            CreditCard.SendKeys("12345678");
            Month.SendKeys("January");
            Year.SendKeys("2023");

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[.='Purchase']"))).Click();

            Assert.IsTrue(driver.FindElement(By.XPath("//p[@class='lead text-muted ']")).Displayed); //If "Thanks for the Purchase with order details..." element is displayed, then order is completed and assertion is successful
            Console.WriteLine("If 'lead text-muted' element is displayed then assertion passes");
        }

    }


 }
