using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteMantisB2
{   
     public class Driver
    {
        public IWebDriver driver;
        public ExtentReports extent;
        public ExtentTest test;


        [SetUp]
        public void StartTest()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://mantis-prova.base2.com.br/");
            driver.Manage().Window.Maximize();

            var sparkReporter = new ExtentSparkReporter("spark-report.html");
            extent = new ExtentReports();
            extent.AttachReporter(sparkReporter);
            ReportManager.GetExtentReports();
        }


        [TearDown]
        public void EndTest() {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test.Log(Status.Fail, "Test failed");
            }

            ReportManager.FlushReport();
            driver.Quit();

        }
        
    }
}
