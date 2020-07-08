using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Applitools.Selenium;
using Applitools.VisualGrid;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ApplitoolsHackathon.Task1
{
    class AppliToolsV2: AppliBase
    {

        [Test]
        public void  Task1()
        {
            // Update the Eyes configuration with test specific values
            Configuration testConfig = eyes.GetConfiguration();
            testConfig.SetTestName("Task1");
            eyes.SetConfiguration(testConfig);

            // Open Eyes, the application,test name and viewport size are allready configured
            IWebDriver driver = eyes.Open(webDriver, "Applifashion", "Task1", new Size(800, 600));

            // Now run the test

            // Visual checkpoint #1.
            driver.Url = NewVersionUrl;   // navigate to website
            eyes.Check(Target.Window().Fully().WithName("Cross-Device Elements Test"));
        }

        [Test]
        public void Task2()
        {
            // Update the Eyes configuration with test specific values
            Configuration testConfig = eyes.GetConfiguration();
            testConfig.SetTestName("Task2");
            eyes.SetConfiguration(testConfig);
            Driver= eyes.Open(webDriver,"Applifashion", "Task2", new Size(800, 600));
            // Open Eyes, the application,test name and viewport size are allready configured
            // IWebDriver driver = 

            // Now run the test

            // Visual checkpoint #1.
            Driver.Url = NewVersionUrl;   // navigate to website
            SelectBlackColor();
            ClickOnFilterButton();
            eyes.Check(Target.Region(By.Id("product_grid")).WithName("Filter Result"));
        }

        [Test]
        public void Task3()
        {
            // Update the Eyes configuration with test specific values
            Configuration testConfig = eyes.GetConfiguration();
            testConfig.SetTestName("Task3");
            eyes.SetConfiguration(testConfig);

            // Open Eyes, the application,test name and viewport size are allready configured
            Driver = eyes.Open(webDriver, "Applifashion", "Task3", new Size(800, 600));

            // Now run the test

            // Visual checkpoint #1.
            Driver.Url = NewVersionUrl;   // navigate to website
            //eyes.CheckWindow("Before mouse click");
            SelectBlackColor();
            ClickOnFilterButton();
            ClickonFirstItem();
            eyes.Check(Target.Window().Fully().WithName("Product Details test"));
        }
    }
}
