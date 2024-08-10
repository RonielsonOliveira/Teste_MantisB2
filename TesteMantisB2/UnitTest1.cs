using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace TesteMantisB2
{
    public class Tests : Driver
    {
        [Test]
        public void LoginTest(){
            driver.FindElement(By.Id("username")).SendKeys("francisco_nascimento");
            driver.FindElement(By.XPath("//*[@id=\"login-form\"]/fieldset/input[2]")).Click();
            driver.FindElement(By.Id("password")).SendKeys("310760fr");
            driver.FindElement(By.XPath("//*[@id=\"login-form\"]/fieldset/input[3]")).Click();
        }
        [Test]
        public void CadastrarTarefaTest(){
            driver.FindElement(By.Id("username")).SendKeys("francisco_nascimento");
            driver.FindElement(By.XPath("//input[@value='Entrar']")).Click();
            driver.FindElement(By.Id("password")).SendKeys("310760fr");
            driver.FindElement(By.XPath("//input[@value='Entrar']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.CssSelector("[href*='bug_report_page.php']")).Click();
            var category = driver.FindElement(By.Id("category_id"));
            var selectElement = new SelectElement(category);
            selectElement.SelectByText("[Todos os Projetos] categoria teste");
            var reproducibility = driver.FindElement(By.Id("reproducibility"));
            var selectReproducibility = new SelectElement(reproducibility);
            selectReproducibility.SelectByText("sempre");
            var severity = driver.FindElement(By.Id("severity"));
            var selectSeverity = new SelectElement(severity);
            selectSeverity.SelectByText("grande");
            var priority = driver.FindElement(By.Id("priority"));
            var selectPriority = new SelectElement(priority);
            selectPriority.SelectByText("alta");
            driver.FindElement(By.Id("platform")).SendKeys("teste");
            driver.FindElement(By.Id("os")).SendKeys("testOS");
            driver.FindElement(By.Id("os_build")).SendKeys("osBuildtest");
            driver.FindElement(By.Id("summary")).SendKeys("Resumo teste Francisco");
            driver.FindElement(By.Id("description")).SendKeys("Descrição teste");
            driver.FindElement(By.Id("steps_to_reproduce")).SendKeys("1-teste 2-teste de novo");
            driver.FindElement(By.Id("additional_info")).SendKeys("Informação adicional testes");
            var tags = driver.FindElement(By.Id("tag_select"));
            var tagElement = new SelectElement(tags);
            tagElement.SelectByText("Atividade");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            tagElement.SelectByText("bug");
            var visibilidade = driver.FindElement(By.XPath("//span[text()='privado']"));
            visibilidade.Click();
            driver.FindElement(By.XPath("//input[@value='Criar Nova Tarefa']")).Click();
            





















        }
    }
}