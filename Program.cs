using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

class Program{
    static void Main(){
        
        IWebDriver driver = new ChromeDriver();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        try{
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
             driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();
            Thread.Sleep(3000);

            //Verify successful login
            string productsPageTitle = driver.FindElement(By.ClassName("title")).Text;
            if (productsPageTitle != "Products")
            {
                throw new Exception("Login unsuccessful or not redirected to the products page.");
            }

            Console.WriteLine("Login successful!");


            driver.FindElement(By.CssSelector("#inventory_container > div > div:nth-child(3) > div.inventory_item_img")).Click();
            Thread.Sleep(3000);

            string tshritDetailsPageTitle = driver.FindElement(By.ClassName("inventory_details_name")).Text;
            if (string.IsNullOrEmpty(tshritDetailsPageTitle))
            {
                throw new Exception("T-shirt details page not displayed. ");
            }
            Console.WriteLine("T-shirt details page displayed");

            // Click the "Add to Cart" button
            driver.FindElement(By.CssSelector(".btn_inventory")).Click();

            // Verify T-shirt is added to the cart successfully
            string cartItemCount = driver.FindElement(By.CssSelector(".shopping_cart_badge")).Text;
            if (cartItemCount != "1") 
            {
               throw new Exception("T-shirt has not been added to cart successfully");
            }
            Console.WriteLine("T-shirt added to cart successfully");

            // Navigate to Cart Page
            driver.FindElement(By.CssSelector(".shopping_cart_link")).Click();

            // Verify that the cart page is displayed
            string cartPageTitle = driver.FindElement(By.ClassName("title")).Text;
            if (cartPageTitle != "Your Cart")
            {
                throw new Exception("Cart page not displayed.");
            }
            Thread.Sleep(3000);

            Console.WriteLine("Cart page displayed.");

            // Review the items in the cart and ensure T-shirt details are correct
            string itemNameInCart = driver.FindElement(By.CssSelector(".inventory_item_name")).Text; 
            string itemPriceInCart = driver.FindElement(By.CssSelector(".inventory_item_price")).Text;
            string itemQuantityInCart = driver.FindElement(By.CssSelector(".cart_quantity")).Text;

            if (itemNameInCart != "Sauce Labs Bolt T-Shirt" || itemPriceInCart != "$15.99" || itemQuantityInCart != "1")
            {
                throw new Exception("T-shirt details in the cart are incorrect.");
            }

            Console.WriteLine("T-shirt details in the cart are correct.");

            // Click the "Checkout" button.
            driver.FindElement(By.CssSelector(".checkout_button")).Click();
            Thread.Sleep(3000);

            // Verify that the checkout information page is displayed.
            string checkoutInfoPageTitle = driver.FindElement(By.ClassName("title")).Text;
            if (checkoutInfoPageTitle != "Checkout: Your Information")
            {
                throw new Exception("Checkout information page not displayed.");
            }

            Console.WriteLine("Checkout information page displayed.");

            // Enter the required checkout information.
            driver.FindElement(By.Id("first-name")).SendKeys("Jane");
            driver.FindElement(By.Id("last-name")).SendKeys("Doe");
            driver.FindElement(By.Id("postal-code")).SendKeys("20200");
            driver.FindElement(By.CssSelector("#continue")).Click();
            Thread.Sleep(3000);

             // Verify that the order summary page is displayed.
            string orderSummaryPageTitle = driver.FindElement(By.ClassName("title")).Text;
            if (orderSummaryPageTitle != "Checkout: Overview")
            {
                throw new Exception("Order summary page not displayed.");
            }

            Console.WriteLine("Order summary page displayed.");

            // Click the "Finish" button.
            driver.FindElement(By.CssSelector("#finish")).Click(); 
            //Thread.Sleep(3000);

            // Verify that the order confirmation page is displayed.
            string orderConfirmationPageTitle = driver.FindElement(By.ClassName("title")).Text;
            if (orderConfirmationPageTitle != "Checkout: Complete!")
            {
                throw new Exception("Order confirmation page not displayed.");
            }

            Console.WriteLine("Order confirmation page displayed.");

            //Click on hamburger button
            driver.FindElement(By.CssSelector("#react-burger-menu-btn")).Click();
            Thread.Sleep(3000);


            // Logout from the application.
            driver.FindElement(By.CssSelector("#logout_sidebar_link")).Click(); 
            Thread.Sleep(4000);

            // Verify that the user is successfully logged out and redirected to the login page.
            IWebElement loginButtonText = driver.FindElement(By.Id("login-button"));
            string buttontext = loginButtonText.GetAttribute("value");
            string expectedtext = "Login";

            if (buttontext != expectedtext)
            {
                throw new Exception("Logout unsuccessful or not redirected to the login page.");
            }

            Console.WriteLine("Logout successful!");
        

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test failed: {ex.Message}");
        }
        finally
        {
            // Close the browser
            driver.Quit();
        }
    }
        
}
