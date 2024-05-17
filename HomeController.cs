using Microsoft.AspNetCore.Mvc;
using PBL3_QLHDTN.Models;
using System.Diagnostics;

namespace PBL3_QLHDTN.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Trangchu()
        {
            return View();
        }
        public IActionResult Gioithieu()
        {
            return View();
        }
    }
}
