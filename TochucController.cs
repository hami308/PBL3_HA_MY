using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PBL3_QLHDTN.Models;

namespace PBL3_QLHDTN.Controllers
{
    public class TochucController : Controller
    {
        QLHDTNContext _db=new QLHDTNContext();
        public IActionResult Trangchutochuc()
        {
            return View();
        }
        public IActionResult Hienthithongtintochuc()
        {
            
           
            int? userID = HttpContext.Session.GetInt32("UserID");
                      
            var u = _db.Tochucs.Where(model => model.Idtochuc.Equals(userID)).Select(model => new { Ten = model.Ten, Email = model.Email, Sdt = model.Sdt, Diachi = model.Diachi }).FirstOrDefault();

            if (u != null)
            {
                ViewBag.Ten = u.Ten.ToString();
                ViewBag.Email = u.Email.ToString();
                ViewBag.Sdt = u.Sdt.ToString();
                ViewBag.Diachi = u.Diachi.ToString();
            }
            var u1 = _db.Motatochucs
                .Where(model => model.Idtochuc.Equals(userID))
                .Select(model => new { Gioithieu = model.Gioithieu, Thanhtuu = model.Thanhtuu }).FirstOrDefault();
            if (u1 != null)
            {
                ViewBag.Mota = u1.Gioithieu.ToString();
                ViewBag.Thanhtuu = u1.Thanhtuu.ToString();
               
            }
           
            return View();
            
        }
        public IActionResult Quanlyhoatdong()
        {
            return View();
        }
    }
}
