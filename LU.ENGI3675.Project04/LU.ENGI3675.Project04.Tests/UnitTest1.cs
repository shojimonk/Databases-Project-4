// <copyright file="UnitTest1.cs" company="ENGI3675">
//     Required copyright tag.
// </copyright>
namespace ASP.NET_01.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using Npgsql;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Collections.Generic;

    /// <summary>
    /// The class performs an insertion attack against a protected web page and a non protected one
    /// </summary>
    [TestClass]
    public class UITest
    {
        /// <summary>
        /// Performs an insertion attack against the protected page 
        /// </summary>
        [TestMethod]
        public void Attack1_Protected()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost/LU.ENGI3675.Project04/StudentInput.aspx");

            IWebElement query = driver.FindElement(By.Name("fullName"));

            query.SendKeys("a','2.1'); update students * set gpa = 4.0; --");
            query = driver.FindElement(By.Name("GPA"));
            query.SendKeys("3");
            query = driver.FindElement(By.Name("button"));
            query.Click();
            System.Threading.Thread.Sleep(5000);

            List<LU.ENGI3675.Proj04.App_Code.Students> students =
                LU.ENGI3675.Proj04.App_Code.DatabaseAccess.Read();
            bool allfour = true;
            foreach (LU.ENGI3675.Proj04.App_Code.Students temp in students)
            {
                if (temp.GPA < 4.0) allfour = false;
                Debug.Write((string)temp.Name);
                Debug.Write(" ");
                Debug.WriteLine((double)temp.GPA);
            }
            Assert.IsFalse(allfour);    //if they aren't all 4.0 gpa, then protected against SQL injection
            driver.Quit();
        }

        /// <summary>
        /// Performs an insertion attack against the non protected page 
        /// </summary>
        [TestMethod]
        public void Attack2_NonProtected()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost/LU.ENGI3675.Project04/StudentInputUNSAFE.aspx");

            IWebElement query = driver.FindElement(By.Name("fullNameUS"));

            query.SendKeys("a','2.1'); update students * set gpa = 4.0; --");
            query = driver.FindElement(By.Name("GPAUS"));
            query.SendKeys("0");
            query = driver.FindElement(By.Name("button"));
            query.Click();
            System.Threading.Thread.Sleep(1000);

            List<LU.ENGI3675.Proj04.App_Code.Students> students =
                LU.ENGI3675.Proj04.App_Code.DatabaseAccess.Read();

            foreach (LU.ENGI3675.Proj04.App_Code.Students temp in students)
            {
                Assert.IsTrue((double)temp.GPA == 4);   //if any of them aren't 4.0, injection failed
                Debug.Write((string)temp.Name);
                Debug.Write(" ");
                Debug.WriteLine((double)temp.GPA);
            }
            driver.Quit();
        }

        /// <summary>
        /// Performs an XSS attack against the protected page 
        /// </summary>
        [TestMethod]
        public void XSS_ATK1_Protected()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost/LU.ENGI3675.Project04/StudentInput.aspx");

            IWebElement query = driver.FindElement(By.Name("fullName"));

            query.SendKeys("<script>document.getElementsByTagName('td')[1].innerHTML = '<img src=\"http://stream1.gifsoup.com/view6/4584074/annoying-guy-at-the-club-o.gif\" name = \"hahaha\">';</script>");
            query = driver.FindElement(By.Name("GPA"));
            query.SendKeys("3");
            query = driver.FindElement(By.Name("button"));
            query.Click();
            driver.Navigate().GoToUrl("http://localhost/LU.ENGI3675.Project04/StudentsTable_XSS_SAFE.aspx");

            try
            {
                driver.FindElement(By.Name("hahaha"));
                Assert.Fail("found image, should not be there.");
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
            }
            
            System.Threading.Thread.Sleep(5000);
            driver.Quit();
        }

        /// <summary>
        ///Performs an XSS attack against the non protected page 
        /// </summary>
        [TestMethod]
        public void XSS_ATK2_NonProtected()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost/LU.ENGI3675.Project04/StudentInput.aspx");

            IWebElement query = driver.FindElement(By.Name("fullName"));

            query.SendKeys("<script>document.getElementsByTagName('td')[1].innerHTML = '<img src=\"http://stream1.gifsoup.com/view6/4584074/annoying-guy-at-the-club-o.gif\" name = \"hahaha\">';</script>");
            query = driver.FindElement(By.Name("GPA"));
            query.SendKeys("3");
            query = driver.FindElement(By.Name("button"));
            query.Click();
            driver.Navigate().GoToUrl("http://localhost/LU.ENGI3675.Project04/StudentsTable.aspx");
            
            Assert.IsNotNull(driver.FindElement(By.Name("hahaha")));

            System.Threading.Thread.Sleep(1000);
            driver.Quit();
        }
    }
}