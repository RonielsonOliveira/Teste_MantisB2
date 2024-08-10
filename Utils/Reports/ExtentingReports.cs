using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Reports
{
    public class ExtentingReports
    {
        private static ExtentReports extentReports;
        private static ExtentTest extentTest;

        private static ExtentingReports StartReporting()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"..\..\..\..\results\" ;

            if(extentReports == null)
            {   
                Directory.CreateDirectory(path);
                extentReports = new ExtentReports();
                
                var spark = new ExtentSparkReporter(path);
                extentReports.AttachReporter(spark);
            }
            return extentReports;
        }
    }
}
