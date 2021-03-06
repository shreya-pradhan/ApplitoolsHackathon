﻿using Applitools;
using Applitools.Selenium;
using Applitools.VisualGrid;
using Configuration = Applitools.Selenium.Configuration;
using IConfiguration = Applitools.Selenium.IConfiguration;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Applitools.Utils.Geometry;

namespace ApplitoolsHackathon
{
    [TestFixture]
    public class AppliBase
    {
     
         private readonly string appName = "AppliFashion";
         private readonly string batchName = "UFG Hackathon";
         private readonly string apiKey = "Z98eCImryQjbVkJaC6oBQuzh9ZqHmxwmgMXVmKLxyOlg110";
         private readonly int concurrentSessions = 5;

         private EyesRunner runner = null;
         private Configuration suiteConfig;
         protected Eyes eyes;
         protected IWebDriver webDriver;


         #region PageObject
         public const string FilterByttonByCss = "#filterBtn";

         public const string CheckBoxElementByBlackColor = "input#colors__Black";

         public const string FirstIteminGridByCss = "div#product_grid div:nth-of-type(1) div.grid_item a";
         public const string SearchFilterIcon = "#ti-filter";
         public const string StableVersionUrl = "https://demo.applitools.com/gridHackathonV1.html";

         public const string NewVersionUrl = "https://demo.applitools.com/gridHackathonV2.html";

         
         public IWebDriver Driver { get; set; }
         public Eyes Eyes { get; set; }
        
         
         // public static BatchInfo BatchName { get; set; }



         public IWebElement FindElementByCss(string elementLocator)
         {
             return Driver.FindElement(By.CssSelector(elementLocator));
         }

         public void clickonFilterIcon()
         {
             Thread.Sleep(300);
             ClickOnElement(SearchFilterIcon);
         }

         

         public void ClickOnElement(string locator)
         {
             FindElementByCss(locator).Click();
         }
         public void ClickOnFilterButton()
         {
             ClickOnElement(FilterByttonByCss);

         }

         public void SelectBlackColor()
         {
             ClickOnElement(CheckBoxElementByBlackColor);
         }

         public void ClickonFirstItem()
         {
             ClickOnElement(FirstIteminGridByCss);
         }
         public IJavaScriptExecutor Javascript { get; set; }


         public void NavigateToAppliFashion(bool stableVersion = true)
         {
             //This uses Selenium to navigate to a url of the page below
             var applicationUrl = stableVersion == true ? StableVersionUrl : NewVersionUrl;
             Driver.Navigate().GoToUrl(applicationUrl);

         }




         #endregion

        [OneTimeSetUp]
         public void BeforeTestSuite()
         {
             runner = new VisualGridRunner(concurrentSessions);
             // Create a configuration object, we will use this when setting up each test
             suiteConfig = new Configuration();
             IConfiguration newsuiteConfg = new Configuration();
            suiteConfig.AddBrowser(1200, 700, BrowserType.CHROME);
            suiteConfig.AddBrowser(1200, 700, BrowserType.FIREFOX);
             suiteConfig.AddBrowser(1200, 700, BrowserType.EDGE_CHROMIUM);
             suiteConfig.AddBrowser(768, 700, BrowserType.CHROME);
             suiteConfig.AddBrowser(768, 700, BrowserType.FIREFOX);
             suiteConfig.AddBrowser(768, 700, BrowserType.EDGE_CHROMIUM);
             suiteConfig.AddDeviceEmulation(DeviceName.iPhone_X, Applitools.VisualGrid.ScreenOrientation.Portrait);

            suiteConfig
                 .SetApiKey(apiKey)
                 .SetAppName(appName)
                 .SetBatch(new BatchInfo(batchName))
                 ;

         }

         [SetUp]
         public void BeforeEachTest()
         {
             // Create the Eyes instance for the test and associate it with the runner
             eyes = new Eyes(runner);
             eyes.SetConfiguration(suiteConfig);
             webDriver = new OpenQA.Selenium.Chrome.ChromeDriver();
         }

         

         [TearDown]
         public void AfterEachTest()
         {
             // check if an exception was thrown
             Boolean testPassed = TestContext.CurrentContext.Result.Outcome.Status != NUnit.Framework.Interfaces.TestStatus.Failed;
             if (testPassed)
             {
                 // Close the Eyes instance, no need to wait for results, we'll get those at the end in afterTestSuite
                 eyes.CloseAsync();
             }
             else
             {
                 // There was an exception so the test may be incomplete - abort the test
                 eyes.AbortIfNotClosed();
             }
             webDriver.Quit();
             
         }

         [OneTimeTearDown]
         public void AfterTestSuite()
         {
             // Wait until the test results are available and retrieve them 
             TestResultsSummary allTestResults = runner.GetAllTestResults(false);
             foreach (var result in allTestResults)
             {
                 HandleTestResults(result);
             }

         }
         void HandleTestResults(TestResultContainer summary)
         {
             Exception ex = summary.Exception;
             if (ex != null)
             {
                 TestContext.WriteLine("System error occured while checking target.");
             }
             TestResults result = summary.TestResults;
             if (result == null)
             {
                 TestContext.WriteLine("No test results information available");
             }
             else
             {
                 TestContext.WriteLine(
                     "AppName = {0}, testname = {1}, Browser = {2},OS = {3} viewport = {4}x{5}, matched = {6},mismatched = {7}, missing = {8}, aborted = {9}\n",
                     result.AppName,
                     result.Name,
                     result.HostApp,
                     result.HostOS,
                     result.HostDisplaySize.Width,
                     result.HostDisplaySize.Height,
                     result.Matches,
                     result.Mismatches,
                     result.Missing,
                     (result.IsAborted ? "aborted" : "no"));
             }
         }
    }
}
