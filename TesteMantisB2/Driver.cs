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
        public void InicioTeste()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://mantis-prova.base2.com.br/");
            driver.Manage().Window.Maximize();

            // Configurar ExtentReports
            var sparkReporter = new ExtentSparkReporter("spark-report.html");
            extent = new ExtentReports();
            extent.AttachReporter(sparkReporter);
            ReportManager.GetExtentReports();
        }


        [TearDown]
        public void FimDoTeste() {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test.Log(Status.Fail, "Test failed");
            }

            ReportManager.FlushReport();

            // Fechar o driver do Selenium
            driver.Quit();

        }
        
    }
}
