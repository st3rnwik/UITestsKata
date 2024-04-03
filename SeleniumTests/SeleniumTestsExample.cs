using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    public class SeleniumTestsExample
    {
        IWebDriver? driver;

        [SetUp]
        public void startBrowser()
        {
            // Path to your ChromeDriver
            driver = new ChromeDriver("C:\\Users\\st3rnwik\\Downloads\\chromedriver-win64\\chromedriver-win64"); // Replace with your path
        }

        [Test]
        public void OrderProcessTest()
        {
            driver.Url = "https://www.saucedemo.com";
            
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();
            Assert.AreEqual(driver.Url, "https://www.saucedemo.com/inventory.html");

            // adding souce laps
            driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();
            var cartCount = driver.FindElement(By.ClassName("shopping_cart_badge")).Text;
            Assert.AreEqual("1", cartCount);

            // add sauce labs bike light
            driver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light")).Click();
            cartCount = driver.FindElement(By.ClassName("shopping_cart_badge")).Text; 
            Assert.AreEqual("2", cartCount);           

            // checkout cart
            driver.FindElement(By.ClassName("shopping_cart_link")).Click();
            Assert.AreEqual(driver.Url, "https://www.saucedemo.com/cart.html");

            // heck cart contents (alternativne misto xpath)
            Assert.IsTrue(driver.FindElement(By.Id("item_4_title_link")).Displayed);  // backpack
            Assert.IsTrue(driver.FindElement(By.Id("item_0_title_link")).Displayed);  // light version

            // navigate checkout
            driver.FindElement(By.Id("checkout")).Click();

            // check info
            driver.FindElement(By.Id("first-name")).SendKeys("Jiri");
            driver.FindElement(By.Id("last-name")).SendKeys("Jirka");
            driver.FindElement(By.Id("postal-code")).SendKeys("42");
            driver.FindElement(By.Id("continue")).Click();

            // mock checkout overview
            Assert.IsTrue(driver.FindElement(By.ClassName("summary_info")).Displayed);

            // finish ordr
            driver.FindElement(By.Id("finish")).Click();

            // check for completion
            Assert.IsTrue(driver.FindElement(By.ClassName("complete-header")).Displayed);
            Assert.AreEqual(driver.Url, "https://www.saucedemo.com/checkout-complete.html");
        }

        [TearDown]
        public void closeBrowser()
        {
            //driver.Quit();
        }
    }
}
