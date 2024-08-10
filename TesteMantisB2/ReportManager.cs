using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace TesteMantisB2
{
    public static class ReportManager
    {
        private static ExtentReports extent;
        private static ExtentSparkReporter htmlReporter;

        // Cria e retorna o objeto ExtentReports
        public static ExtentReports GetExtentReports()
        {
            if (extent == null)
            {
                htmlReporter = new ExtentSparkReporter("ExtentReports.html");
                extent = new ExtentReports();
                extent.AttachReporter(htmlReporter);
            }
            return extent;
        }

        // Finaliza o relatório
        public static void FlushReport()
        {
            if (extent != null)
            {
                extent.Flush();
            }
        }
    }
}
