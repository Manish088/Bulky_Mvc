using DI_Seriver_Lifetime.Models;
using DI_Seriver_Lifetime.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace DI_Seriver_Lifetime.Controllers
{
    public class HomeController : Controller
    {
        private readonly IScopedGuidServices _scoped1;
        private readonly IScopedGuidServices _scoped2;
        
        private readonly ITransientGuidServices _transient1;
        private readonly ITransientGuidServices _transient2;

        private readonly ISingletonGuidServices _singleton1;
        private readonly ISingletonGuidServices _singleton2;
        
        
        

        public HomeController(
            IScopedGuidServices scoped1,
            IScopedGuidServices scoped2,
            ITransientGuidServices transient1,
            ITransientGuidServices transient2,
            ISingletonGuidServices singleton1,
            ISingletonGuidServices singleton2
            )
        {
            _singleton1 = singleton1;
            _singleton2 = singleton2;
            _scoped1 = scoped1;
            _scoped2 = scoped2;
            _transient1 = transient1;
            _transient2 = transient2;


        }

        public IActionResult Index()
        {
            StringBuilder message=new StringBuilder();
            message.Append($"Transient 1 : {_transient1.getGuid()}\n");
            message.Append($"Transient 2 : {_transient2.getGuid()}\n\n\n");
            message.Append($"Singleton 1 : {_singleton1.getGuid()}\n");
            message.Append($"Singleton 2 : {_singleton1.getGuid()}\n");
            message.Append($"Scoped 1 : {_scoped1.getGuid()}\n");
            message.Append($"Scoped 2 : {_scoped2.getGuid()}\n");
            
           


            return View(message.ToString());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}