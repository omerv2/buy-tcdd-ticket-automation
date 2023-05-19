using Buy.Tcdd.Ticket;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DotNetEnv.Configuration;
using System.Xml.Linq;

public static class Extensions
{
    public static void FindSetElement(this ChromeDriver driver, By by, string keyVaue)
    {
        IWebElement ele = driver.FindElement(by);
        ele.Clear();
        ele.SendKeys(keyVaue);
    }

    public static void FocusOut(this ChromeDriver driver)
    {
        IWebElement focusOut = driver.FindElement(By.TagName("body"));
        focusOut.Click();
        Thread.Sleep(2000);
    }
}
public class Program
{
    private static void Main(string[] args)
    {

        var temp = Host.CreateDefaultBuilder(args);
        DotNetEnv.Env.TraversePath().Load();
        var host = temp.Build();
        IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

        var EnvSettings = EnvironmentSettings.Bind(config);
        ChromeDriver driver = null;
        int Completed = 0;
        while (Completed == 0)
            try
            {
                driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://ebilet.tcddtasimacilik.gov.tr/view/eybis/tnmGenel/tcddWebContent.jsf");

                driver.FindSetElement(By.Name("nereden"), EnvSettings.NEREDEN);

                driver.FindSetElement(By.Name("nereye"), EnvSettings.NEREYE);

                driver.FindSetElement(By.Name("trCalGid_input"), EnvSettings.GIDIS_TARIH);

                driver.FocusOut();

                IWebElement btnAra = driver.FindElement(By.Id("btnSeferSorgula"));

                Actions actions = new Actions(driver);

                actions.MoveToElement(btnAra).Click().Perform();
                Thread.Sleep(5000);

                IWebElement findRelatedDepartureRow = driver.FindElement(By.XPath($"//span[text()='{EnvSettings.SAAT}']"));

                IWebElement parentElement = findRelatedDepartureRow.FindElement(By.XPath("parent::*"));

                IWebElement parentRowElement = parentElement.FindElement(By.XPath("parent::*"));
                IWebElement thElement = parentRowElement.FindElement(By.CssSelector("td:nth-child(7)"));
                ReadOnlyCollection<IWebElement> c = thElement.FindElements(By.XPath("./child::*"));

                var seciliButton = c.First();
                if (seciliButton.Displayed && seciliButton.Enabled)
                {
                    seciliButton.Click();
                }
                else
                {
                    throw new Exception("There is no place to sit");
                }

                if (seciliButton.GetAttribute("class").Contains("ui-state-disabled"))
                {
                    Console.WriteLine("Secili Button is Deactivated");
                    throw new Exception("Secili Button is Deactivated");
                }
                Thread.Sleep(2000);

                IWebElement btnDevamClick = driver.FindElement(By.Id("mainTabView:btnDevam44"));
                Actions newActions = new Actions(driver);
                newActions.MoveToElement(btnDevamClick).Click().Perform();
                Thread.Sleep(5000);

                ((IJavaScriptExecutor)driver).ExecuteScript($"document.getElementById('mainTabView:imIletisimCep').value = '{EnvSettings.CEP_TEL}'");
                Thread.Sleep(2000);

                driver.FindSetElement(By.Id("mainTabView:imIletisimEposta"), $"{EnvSettings.EMAIL}");

                driver.FindSetElement(By.Id("mainTabView:dtBiletRezBilgileri:0:iptYolcuIsmi"), $"{EnvSettings.YOLCU_ISIM}");

                driver.FindSetElement(By.Id("mainTabView:dtBiletRezBilgileri:0:iptYolcuSoyIsmi"), $"{EnvSettings.YOLCU_SOYISIM}");

                IWebElement radioCinsiyet = EnvSettings.CINSIYET == "E" ? driver.FindElement(By.Id("mainTabView:dtBiletRezBilgileri:0:tipradioCinsiyet:0")) : driver.FindElement(By.Id("mainTabView:dtBiletRezBilgileri:0:tipradioCinsiyet:1"));
                Actions newCinsiyetActions = new Actions(driver);
                newCinsiyetActions.MoveToElement(radioCinsiyet).Click().Perform();
                Thread.Sleep(5000);

                driver.FindSetElement(By.Id("mainTabView:dtBiletRezBilgileri:0:yolcutcno"), $"{EnvSettings.YOLCU_TC}");

                ((IJavaScriptExecutor)driver).ExecuteScript($"document.getElementById('mainTabView:dtBiletRezBilgileri:0:dogTar2').value = '{EnvSettings.YOLCU_DOGUM_TARIHI}'");

                string cacikFunc = @"function cacik() {
    var caciki=0;
Array.from(document.getElementsByClassName('ui-wagon-item-checkbox')).every(
    function(element, index, array) {
      if((element.closest('div').getElementsByTagName('img')[0].height<50)){
          element.click();
          caciki=1;
          return;
      } // do stuff
    });
    return caciki;
}
return cacik();";
                var vagons = driver.FindElements(By.ClassName("vagonText"));
                Thread.Sleep(3000);
                int i = 0;
                for (; i < vagons.Count;)
                {
                    vagons = driver.FindElements(By.ClassName("vagonText"));
                    vagons[i].Click();
                    Thread.Sleep(5000);
                    var result = ((IJavaScriptExecutor)driver).ExecuteScript(cacikFunc);
                    if (result != null && result.ToString() == "1")
                        break;

                    i++;
                    //all vagons are scanned, we need to start from scratch to find a convenient one
                    if (i == vagons.Count - 1)
                        i = 0;
                }

                IWebElement btnOdemeAsamasi = driver.FindElement(By.Id("mainTabView:btnDevam2"));
                Actions newActions1 = new Actions(driver);
                newActions1.MoveToElement(btnOdemeAsamasi).Click().Perform();
                Thread.Sleep(5000);

                ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementById('j_idt459').children[1].click()");

                driver.FindSetElement(By.Id("adSoyad"), $"{EnvSettings.KKART_ISIMSOYISIM}");

                driver.FindSetElement(By.Id("id_forwardGroupIndex"), $"{EnvSettings.KKART_NO}");

                driver.FocusOut();

                ((IJavaScriptExecutor)driver).ExecuteScript($"document.getElementById('cvvNo').value = '{EnvSettings.KKART_CVC}'");
                driver.FocusOut();

                ((IJavaScriptExecutor)driver).ExecuteScript($"document.evaluate(`//*[@id='somSonKullanmaTarihiAy_panel']/div/ul/li[text()='{EnvSettings.KKART_SKT_AY}']`, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue.click()");
                Thread.Sleep(2000);

                ((IJavaScriptExecutor)driver).ExecuteScript($"document.evaluate(`//*[@id='somSonKullanmaTarihiYil_panel']/div/ul/li[text()='{EnvSettings.KKART_SKT_YIL}']`, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue.click()");
                Thread.Sleep(2000);

                IWebElement btnOde = driver.FindElement(By.Id("odemeForInt"));
                Actions odeAction = new Actions(driver);
                odeAction.MoveToElement(btnOde).Click().Perform();
                Thread.Sleep(5000);
                Completed = 1;
            }
            catch (Exception ex)
            {
                driver?.Quit();
                //devam ke
            }
    }
}