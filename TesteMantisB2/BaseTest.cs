using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace TesteMantisB2
{
    public class BaseTest : Driver
    {
        private LoginPage loginPage;
        private TaskPage taskPage;
        private SearchElement searchPage;
        protected ExtentTest test;

        [SetUp]
        public void Setup()
        {
            TimeSpan timeout = TimeSpan.FromSeconds(7); // Define o tempo de espera implícita para o driver
            loginPage = new LoginPage(driver, timeout);
            taskPage = new TaskPage(driver, timeout);
            searchPage = new SearchElement(driver, timeout);
        }

        // Funções que executam os testes passando os parâmetros necessários para a execução dos mesmos
        [Test]
        public void LoginTest() // Teste para verificar o login
        {
            test = ReportManager.GetExtentReports().CreateTest("LoginTest");

            try
            {
                loginPage.Login("francisco_nascimento", "base123#");
                test.Log(Status.Pass, "Login realizado com sucesso.");
            }
            catch (Exception error)
            {
                test.Log(Status.Fail, $"Erro ao realizar login: {error.Message}");
            }
        }

        [Test]
        public void AddTaskTest() // Teste para verificar a adição de tarefas
        {
            test = ReportManager.GetExtentReports().CreateTest("AddTaskTest");

            try
            {
                loginPage.Login("francisco_nascimento", "base123#");

                taskPage.CreateTask(
                    categoryText: "[Todos os Projetos] categoria teste",
                    reproducibilityText: "sempre",
                    severityText: "grande",
                    priorityText: "alta",
                    platform: "teste",
                    os: "testOS",
                    osBuild: "osBuildtest",
                    summary: "Resumo teste Francisco",
                    description: "Descrição teste",
                    stepsToReproduce: "1-teste 2-teste de novo",
                    additionalInfo: "Informação adicional testes",
                    tags: new[] { "Atividade", "bug" },
                    visibilityText: "privado"
                );

                test.Log(Status.Pass, "Tarefa criada com sucesso.");
            }
            catch (Exception error)
            {
                test.Log(Status.Fail, $"Erro ao criar tarefa: {error.Message}");
            }
        }

        [Test]
        public void AddNoteTest() // Teste para verificar a adição de uma nota na tarefa
        {
            test = ReportManager.GetExtentReports().CreateTest("AddNoteTest");

            try
            {
                loginPage.Login("francisco_nascimento", "base123#");
                taskPage.CreateNote("0001371", "teste anotação Francisco");
                test.Log(Status.Pass, "Nota adicionada com sucesso.");
            }
            catch (Exception error)
            {
                test.Log(Status.Fail, $"Erro ao adicionar nota: {error.Message}");
            }
        }

        [Test]
        public void FindTaskById() // Teste para verificar o barra de pesquisa utilizando a Id da tarefa
        {
            test = ReportManager.GetExtentReports().CreateTest("FindTaskById");

            try
            {
                loginPage.Login("francisco_nascimento", "base123#");
                searchPage.FindTaskId("0001371");
                test.Log(Status.Pass, "Tarefa encontrada pelo ID com sucesso.");
            }
            catch (Exception error)
            {
                test.Log(Status.Fail, $"Erro ao encontrar tarefa pelo ID: {error.Message}");
            }
        }

        [Test]
        public void FindTaskByResume() // Teste para verificar o uso do filtro de resumo da tarefa
        {
            test = ReportManager.GetExtentReports().CreateTest("FindTaskByResume");

            try
            {
                loginPage.Login("francisco_nascimento", "base123#");
                searchPage.FindTaskResume("teste busca", "0001373");
                test.Log(Status.Pass, "Tarefa encontrada pelo resumo com sucesso.");
            }
            catch (Exception error)
            {
                test.Log(Status.Fail, $"Erro ao encontrar tarefa pelo resumo: {error.Message}");
            }
        }

        [Test]
        public void FindTaskByCategory() // Teste para verificar o uso do filtro de categoria da tarefa
        {
            test = ReportManager.GetExtentReports().CreateTest("FindTaskByCategory");

            try
            {
                loginPage.Login("francisco_nascimento", "base123#");
                searchPage.FindTaskCategory("nova categoria", "0001374");
                test.Log(Status.Pass, "Tarefa encontrada pela categoria com sucesso.");
            }
            catch (Exception error)
            {
                test.Log(Status.Fail, $"Erro ao encontrar tarefa pela categoria: {error.Message}");
            }
        }

        [Test]
        public void FindTaskByState() // Teste para verificar o uso do filtro de estado da tarefa
        {
            test = ReportManager.GetExtentReports().CreateTest("FindTaskByState");

            try
            {
                loginPage.Login("francisco_nascimento", "base123#");
                searchPage.FindTaskState("atribuído", "0001374");
                test.Log(Status.Pass, "Tarefa encontrada pelo estado com sucesso.");
            }
            catch (Exception error)
            {
                test.Log(Status.Fail, $"Erro ao encontrar tarefa pelo estado: {error.Message}");
            }
        }

        [Test]
        public void FindTaskBySeverity() // Teste para verificar o uso do filtro de gravidade da tarefa
        {
            test = ReportManager.GetExtentReports().CreateTest("FindTaskBySeverity");

            try
            {
                loginPage.Login("francisco_nascimento", "base123#");
                searchPage.FindTaskSeverity("pequeno", "0001374");
                test.Log(Status.Pass, "Tarefa encontrada pela gravidade com sucesso.");
            }
            catch (Exception error)
            {
                test.Log(Status.Fail, $"Erro ao encontrar tarefa pela gravidade: {error.Message}");
            }
        }
    }

    // Classes que têm como finalidade a interação dos elementos das páginas a serem testadas
    public class LoginPage // Classe que interage com a pagina de login
    {
        private readonly IWebDriver driver;
        private readonly TimeSpan timeout;

        public LoginPage(IWebDriver driver, TimeSpan timeout)
        {
            this.driver = driver;
            this.timeout = timeout;
            driver.Manage().Timeouts().ImplicitWait = timeout;
        }

        public void Login(string username, string password)
        {
            var usernameField = driver.FindElement(By.Id("username"));
            usernameField.SendKeys(username);
            driver.FindElement(By.XPath("//input[@value='Entrar']")).Click();

            var passwordField = driver.FindElement(By.Id("password"));
            passwordField.SendKeys(password);

            driver.FindElement(By.XPath("//input[@value='Entrar']")).Click();
        }
    }

    public class TaskPage // Classe que interage com a pagina de cadastro de tarefa
    {
        private readonly IWebDriver driver;
        private readonly TimeSpan timeout;

        public TaskPage(IWebDriver driver, TimeSpan timeout)
        {
            this.driver = driver;
            this.timeout = timeout;
            driver.Manage().Timeouts().ImplicitWait = timeout; 
        }

        public void CreateTask(string categoryText, string reproducibilityText, string severityText, string priorityText,
                               string platform, string os, string osBuild, string summary, string description,
                               string stepsToReproduce, string additionalInfo, string[] tags, string visibilityText)
        {
            driver.FindElement(By.CssSelector("[href*='bug_report_page.php']")).Click();

            var category = new SelectElement(driver.FindElement(By.Id("category_id")));
            category.SelectByText(categoryText);

            var reproducibility = new SelectElement(driver.FindElement(By.Id("reproducibility")));
            reproducibility.SelectByText(reproducibilityText);

            var severity = new SelectElement(driver.FindElement(By.Id("severity")));
            severity.SelectByText(severityText);

            var priority = new SelectElement(driver.FindElement(By.Id("priority")));
            priority.SelectByText(priorityText);

            driver.FindElement(By.Id("platform")).SendKeys(platform);
            driver.FindElement(By.Id("os")).SendKeys(os);
            driver.FindElement(By.Id("os_build")).SendKeys(osBuild);
            driver.FindElement(By.Id("summary")).SendKeys(summary);
            driver.FindElement(By.Id("description")).SendKeys(description);
            driver.FindElement(By.Id("steps_to_reproduce")).SendKeys(stepsToReproduce);
            driver.FindElement(By.Id("additional_info")).SendKeys(additionalInfo);

            var tagElement = new SelectElement(driver.FindElement(By.Id("tag_select")));
            foreach (var tag in tags)
            {
                tagElement.SelectByText(tag);
            }

            driver.FindElement(By.XPath($"//span[text()='{visibilityText}']")).Click();
            driver.FindElement(By.XPath("//input[@value='Criar Nova Tarefa']")).Click();
        }

        public void CreateNote(string taskId, string note) //Classe que interage com a criação de nota da tarefa
        {
            driver.FindElement(By.XPath("//span[text()=' Ver Tarefas ']")).Click();
            driver.FindElement(By.LinkText(taskId)).Click();
            driver.FindElement(By.XPath("//span[text()='privado']")).Click();
            driver.FindElement(By.Id("bugnote_text")).SendKeys(note);
            driver.FindElement(By.XPath("//input[@value='Adicionar Anotação']")).Click();
        }
    }

    public class SearchElement // Classe que interage com a barra de pesquisa e com os filtros das tarefas do mantis
    {
        private readonly IWebDriver driver;
        private readonly TimeSpan timeout;

        public SearchElement(IWebDriver driver, TimeSpan timeout)
        {
            this.driver = driver;
            this.timeout = timeout;
            driver.Manage().Timeouts().ImplicitWait = timeout; 
        }

        public void FindTaskId(string taskId)
        {
            driver.FindElement(By.XPath("//input[@name='bug_id']")).SendKeys(taskId);
            driver.FindElement(By.XPath("//input[@name='bug_id']")).SendKeys(Keys.Enter);
            IWebElement taskIdFound = driver.FindElement(By.CssSelector("td.bug-id"));
            string tableId = taskIdFound.Text;
            Assert.That(tableId, Is.EqualTo(taskId));
        }

        public void FindTaskResume(string taskSearch, string taskId)
        {
            driver.FindElement(By.XPath("//span[text()=' Ver Tarefas ']")).Click();
            driver.FindElement(By.LinkText("Redefinir")).Click();
            driver.FindElement(By.Id("filter-search-txt")).SendKeys(taskSearch);
            driver.FindElement(By.Id("filter-search-txt")).SendKeys(Keys.Space);
            driver.FindElement(By.Id("filter-search-txt")).SendKeys(taskId);
            driver.FindElement(By.XPath("//input[@name='filter_submit']")).Click();
            IWebElement taskResumeFound = driver.FindElement(By.CssSelector("td.column-summary"));
            string tableResume = taskResumeFound.Text;
            Assert.That(tableResume, Is.EqualTo(taskSearch));
        }

        public void FindTaskCategory(string taskCategory, string taskId)
        {
            driver.FindElement(By.XPath("//span[text()=' Ver Tarefas ']")).Click();
            driver.FindElement(By.LinkText("Redefinir")).Click();
            driver.FindElement(By.Id("show_category_filter")).Click();
            var category = new SelectElement(driver.FindElement(By.Name("category_id[]")));
            category.SelectByText(taskCategory);
            driver.FindElement(By.Id("filter-search-txt")).SendKeys(taskId);
            driver.FindElement(By.XPath("//input[@name='filter_submit']")).Click();
            IWebElement taskCategoryFound = driver.FindElement(By.CssSelector("td.column-category"));
            string tableCategory = taskCategoryFound.Text;
            Assert.That(tableCategory, Is.EqualTo(taskCategory));
        }

        public void FindTaskState(string taskState, string taskId)
        {
            driver.FindElement(By.XPath("//span[text()=' Ver Tarefas ']")).Click();
            driver.FindElement(By.LinkText("Redefinir")).Click();
            driver.FindElement(By.Id("show_status_filter")).Click();
            var state = new SelectElement(driver.FindElement(By.Name("status[]")));
            state.SelectByText(taskState);
            driver.FindElement(By.Id("filter-search-txt")).SendKeys(taskId);
            driver.FindElement(By.XPath("//input[@name='filter_submit']")).Click();
            IWebElement taskStateFound = driver.FindElement(By.CssSelector("td.column-status"));
            string tableState = taskStateFound.Text;
            Assert.That(tableState, Does.Contain(taskState));
        }

        public void FindTaskSeverity(string taskSeverity, string taskId)
        {
            driver.FindElement(By.XPath("//span[text()=' Ver Tarefas ']")).Click();
            driver.FindElement(By.LinkText("Redefinir")).Click();
            driver.FindElement(By.Id("show_severity_filter")).Click();
            var severity = new SelectElement(driver.FindElement(By.Name("severity[]")));
            severity.SelectByText(taskSeverity);
            driver.FindElement(By.Id("filter-search-txt")).SendKeys(taskId);
            driver.FindElement(By.XPath("//input[@name='filter_submit']")).Click();
            IWebElement taskSeverityFound = driver.FindElement(By.CssSelector("td.column-severity"));
            string tableSeverity = taskSeverityFound.Text;
            Assert.That(tableSeverity, Is.EqualTo(taskSeverity));
        }
    }
}
