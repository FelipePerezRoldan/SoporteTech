using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoporteTech.WebAPI;
using SoporteTech.WebAPI.Controllers;
using System.Web.Mvc;

namespace SoporteTech.WebAPI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Disponer
            HomeController controller = new HomeController();

            // Actuar
            ViewResult result = controller.Index() as ViewResult;

            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
