using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class PublicController : Controller
    {
        [HttpGet]
        public IActionResult Portal() => View();   // /Public/Portal
    }
}
