using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using PBL3_QLHDTN.Models;
using PBL3_QLHDTN.ViewModel;
using System.Globalization;
using System.Net.Security;
namespace PBL3_QLHDTN.Controllers
{
    public class CanhanController : Controller
    {
        QLHDTNContext db = new QLHDTNContext();
    
        public IActionResult Trangchucanhan()
        {

            return View();
        }
        public IActionResult Chinhsuathongtincanhan()
        {

            return View();
        }
        public IActionResult Doimatkhau()
        {

            return View();
        }

        public IActionResult Hienthithongtincanhan()
        {
            int? userID = HttpContext.Session.GetInt32("UserID"); 
          
            var u = db.Canhans.Where(model => model.Idcanhan.Equals(userID)).Select(model => new { Ten = model.Ten, Email = model.Email, Sdt = model.Sdt, Diachi = model.Diachi, Gioitinh = model.Gioitinh, Namsinh = model.Namsinh }).FirstOrDefault();

            if (u != null)
            {
                ViewBag.Ten = u.Ten.ToString();
                ViewBag.Email = u.Email.ToString();
                ViewBag.SDT = u.Sdt.ToString();
                ViewBag.DC = u.Diachi.ToString();
                if (u.Gioitinh == false)
                {
                    ViewBag.GT = "Nữ";
                }
                else
                {
                    {
                        ViewBag.GT = "Nam";
                    }
                }
                ViewBag.NS = u.Namsinh.ToString();
            }
            var soLuongHoaDong = db.QuanlyTghds.Where(model => model.Idcanhan.Equals(userID) && model.Tinhtrangthamgia == true).ToList().Count();
            ViewBag.sohoatdong = soLuongHoaDong;


            return View();
        }

         public IActionResult Hoatdongcanhan()
         {
           int? userID = HttpContext.Session.GetInt32("UserID");
            var ketqua = (from tt in db.QuanlyTghds
                          join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                          join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                          where tt.Idcanhan == userID
                          select new HoatdongcanhanViewModel
                          {
                              Idhoatdong = tt.Idhoatdong,
                              Idtochuc = hd.Idtochuc,
                              Tenhoatdong = mt.Tenhoatdong,
                              Thoigiandangky = tt.Thoigiandangky,
                              Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                              Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                              Tinhtrang = Gettinhtrang(tt)
                          }).ToList();
            var linhvuc = db.Linhvucs.ToList();
            ViewBag.Linhvuc = linhvuc;
            return View(ketqua);

        }
        [HttpPost]
        public IActionResult Hoatdongcanhan(string cbbtinhtrang, string cbblinhvuc, string thoigian, string tenhoatdong,string action,int IdhoatdongHuy)
        {
            var linhvuc = db.Linhvucs.ToList();
            ViewBag.Linhvuc = linhvuc;
            if (action=="display")
            {
                int? userID = HttpContext.Session.GetInt32("UserID");
                var ketqua = (from tt in db.QuanlyTghds
                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                              where tt.Idcanhan == userID
                              select new HoatdongcanhanViewModel
                              {
                                  Idhoatdong = tt.Idhoatdong,
                                  Idtochuc = hd.Idtochuc,
                                  Tenhoatdong = mt.Tenhoatdong,
                                  Thoigiandangky = tt.Thoigiandangky,
                                  Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                  Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                  Tinhtrang = Gettinhtrang(tt)
                              }).ToList();
                
                return View(ketqua);
            }
           /* if (action == "cancel")
            {
                int? userID = HttpContext.Session.GetInt32("UserID");
                Huydangky(IdhoatdongHuy);
                var ketqua = (from tt in db.QuanlyTghds
                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                              where tt.Idcanhan == userID
                              select new HoatdongcanhanViewModel
                              {
                                  Idhoatdong = tt.Idhoatdong,
                                  Idtochuc = IdhoatdongHuy,
                                  Tenhoatdong = mt.Tenhoatdong,
                                  Thoigiandangky = tt.Thoigiandangky,
                                  Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                  Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                  Tinhtrang = Gettinhtrang(tt)
                              }).ToList();
                
                return View(ketqua);
            }*/
            
            return Hoatdongcanhan();
        }
        public IActionResult Huydangky(int Idhoatdonghuy)
        {

            int? userID = HttpContext.Session.GetInt32("UserID");
            var hoatdonghuy = db.QuanlyTghds.Where(model => model.Idcanhan.Equals(userID) && model.Idhoatdong.Equals(Idhoatdonghuy) && model.Tinhtrangthamgia.Equals(null)).FirstOrDefault();
            if (hoatdonghuy != null)
            {
                hoatdonghuy.Tinhtranghuy=true;
                db.SaveChanges();
                
            }
            ViewBag.thongbao = Idhoatdonghuy;
           // return View();
           return RedirectToAction("Hoatdongcanhan");
        }
        public static string Gettinhtrang(QuanlyTghd quanlyTghd)
        {
            if (quanlyTghd.Trangthaiduyetdon == true && quanlyTghd.Tinhtranghuy == null && quanlyTghd.Tinhtrangthamgia == null)
            {
                return "Đã được duyệt ";
            }

            if (quanlyTghd.Tinhtranghuy == true && quanlyTghd.Tinhtrangthamgia == null)
            {
                return "Đã hủy ";
            }
            if (quanlyTghd.Trangthaiduyetdon == true && quanlyTghd.Tinhtranghuy == null && quanlyTghd.Tinhtrangthamgia == true)
            {
                return "Đã tham gia";
            }
            return "Đã đăng ký";
        }
        
        [HttpGet]
        public IActionResult Chitiethoatdongcanhan(int idhd)
        {

            int? user = HttpContext.Session.GetInt32("UserID");
            var ketqua = (from tt in db.QuanlyTghds
                          join mota in db.MotaHds on tt.Idhoatdong equals mota.Idhoatdong
                          join dgtutochuc in db.DanhgiaTnvs on new { tt.Idhoatdong, tt.Idcanhan } equals new { dgtutochuc.Idhoatdong, dgtutochuc.Idcanhan } into dg1
                          from dgtc in dg1.DefaultIfEmpty()
                          join dgtucanhan in db.DanhgiaHds on new { tt.Idhoatdong, tt.Idcanhan } equals new { dgtucanhan.Idhoatdong, dgtucanhan.Idcanhan } into dg2
                          from dgcn in dg2.DefaultIfEmpty()
                          where tt.Idcanhan == user && tt.Idhoatdong == idhd
                          select new ChitiethoatdongViewModel
                          {
                              Tenhoatdong = mota.Tenhoatdong,
                              Thoigianbatdau = mota.Thoigianbatdau,
                              Tgbdchinhsua = mota.Tgbdchinhsua,
                              Thoigiaketthuc = mota.Thoigiaketthuc,
                              Tgktchinhsua = mota.Tgktchinhsua,
                              DiaDiem = mota.DiaDiem,
                              MuctieuHd = mota.MuctieuHd,
                              Tinhtrang = Gettinhtrang(tt),
                              Linhvuc = db.Linhvucs.Where(model => model.Idlinhvuc.Equals(mota.Linhvuc)).Select(model => model.Linhvuc1).FirstOrDefault(),
                              Thoigianhuy = mota.Thoigianhuy,
                              Lydohuy = mota.Lydohuy,
                              Danhgiatutochuc = dgtc.Danhgia,
                              Tgdanhgia1 = dgtc.Tgdanhgia,
                              Danhgiacuatnv = dgcn.Danhgia,
                              Tgdanhgia2 = dgcn.Tgdanhgia

                          }).FirstOrDefault();

            return View(ketqua);
        }
        [HttpPost]
        public IActionResult Chitiethoatdongcanhan(int idhd,string Danhgia)
        {
            if (!string.IsNullOrEmpty(Danhgia))
            {

                int? user = HttpContext.Session.GetInt32("UserID");
                var danhgiamoi = new DanhgiaHd
                {
                    Idcanhan = user,
                    Idhoatdong = idhd,
                    Danhgia = Danhgia,
                    Tgdanhgia=DateTime.Now,
                };
                db.DanhgiaHds.Add(danhgiamoi);
                db.SaveChanges();
            }
            return Chitiethoatdongcanhan(idhd);
        }
       
        [HttpGet]
        public IActionResult Hienthithongtintochuc(int idtc)
        {
            var u = db.Tochucs.Where(model => model.Idtochuc.Equals(idtc)).Select(model => new { Ten = model.Ten, Email = model.Email, Sdt = model.Sdt, Diachi = model.Diachi }).FirstOrDefault();

            if (u != null)
            {
                ViewBag.Ten = u.Ten.ToString();
                ViewBag.Email = u.Email.ToString();
                ViewBag.Sdt = u.Sdt.ToString();
                ViewBag.Diachi = u.Diachi.ToString();
            }
            var u1 = db.Motatochucs
                .Where(model => model.Idtochuc.Equals(idtc))
                .Select(model => new { Gioithieu = model.Gioithieu, Thanhtuu = model.Thanhtuu }).FirstOrDefault();
            if (u1 != null)
            {
                ViewBag.Mota = u1.Gioithieu.ToString();
                ViewBag.Thanhtuu = u1.Thanhtuu.ToString();

            }

            return View();
        }

        public IActionResult Baocaotaikhoan()
        {
            
            return View();

        }
    }
}
