﻿using Microsoft.AspNetCore.Mvc;
using PBL3_QLHDTN.Models;

namespace PBL3_QLHDTN.Controllers
{
    public class TaikhoanController : Controller
    {
        public int ID;
        QLHDTNContext db = new QLHDTNContext();
        public IActionResult Dangnhap()
        {
            if (HttpContext.Session.GetString("Tendangnhap") == null)
            {
                return View();
            }
            return RedirectToAction("Trangchu", "Home");
        }
        [HttpPost]
        public IActionResult Dangnhap(Taikhoan user)
        {
            if (HttpContext.Session.GetString("Tendangnhap") == null)
            {

                var u = db.Taikhoans
                 .Where(model => model.Tendangnhap.Equals(user.Tendangnhap) && model.Matkhau.Equals(user.Matkhau))
                 .Select(model => new { Id = model.Id, Vaitro = model.Vaitro, Trangthai = model.Trangthai })
                 .FirstOrDefault();
                if (u == null)
                {
                    ViewBag.thongbao = "Thông tin không chính xác !";
                    return View();
                }
                else
                {
                    if (u.Trangthai == false)
                    {
                        ViewBag.thongbao = "Tài khoản đã bị khoá.";
                        return View();
                    }
                    else
                    {
                        if (u.Vaitro == 1)
                        {
                            HttpContext.Session.SetInt32("UserID", u.Id);
                            return Redirect("/Canhan/Trangchucanhan/"+u.Id);
                        }
                        if (u.Vaitro == 2)
                        {
                            HttpContext.Session.SetInt32("UserID", u.Id);
                            return Redirect("/Tochuc/Trangchutochuc");
                        }
                        if (u.Vaitro == 3)
                        {
                            HttpContext.Session.SetInt32("UserID", u.Id);
                            return Redirect("/Quantrivien/Trangchuquantrivien");
                        }

                    }
                }
            }

            return View();
        }
        /* public IActionResult Dangnhap(string TDN, string MK)
         {
             TDN = Request.Form["TDN"];
             MK = Request.Form["MK"];
             if (TDN == null || MK == null)
             {
                 ViewBag.thongbao("Ban chua nhap du thong tin");
                 return View();
             }
             else
             {
                 string connectionString = "Data Source=LAPTOP-JKMABAVK;Initial Catalog=QLYHDTN;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
                 using (SqlConnection con = new SqlConnection(connectionString))
                 {
                     con.Open();

                     // Tạo câu lệnh SQL để kiểm tra xem tên đăng nhập đã tồn tại chưa
                     string query = "SELECT COUNT(*) FROM Taikhoan WHERE Tendangnhap = @Username  AND Matkhau=@mk";
                     SqlCommand cmd = new SqlCommand(query, con);
                     cmd.Parameters.AddWithValue("@Username", TDN);
                     cmd.Parameters.AddWithValue("@mk", MK);
                     int count = (int)cmd.ExecuteScalar();

                     if (count == 0)
                     {
                         ViewBag.thongbao = "Thông tin không chính xác !";
                         return View();
                     }
                     else
                     {
                         query = "SELECT Vaitro,ID,Trangthai FROM TaiKhoan WHERE Tendangnhap = @Username ";
                         cmd = new SqlCommand(query, con);
                         cmd.Parameters.AddWithValue("@Username", TDN);
                         SqlDataReader reader = cmd.ExecuteReader();
                         int VT = -1;
                         bool TT = true;
                         while (reader.Read())
                         {
                             VT = Int32.Parse(reader["Vaitro"].ToString());
                             ID = Int32.Parse(reader["ID"].ToString());
                             TT = Boolean.Parse(reader["Trangthai"].ToString());
                         }
                         if (TT == false)
                         {
                             ViewBag.thongbao = "Tài khoản đã bị khoá.";
                             return View();
                         }
                         else
                         {
                             if (VT == 1)
                             {
                                 HttpContext.Session.SetInt32("UserID", ID);
                                 return Redirect("/Canhan/Trangchucanhan");

                             }
                             else if (VT == 2) return Redirect("/Tochuc/Trangchutochuc");
                             else return View();
                         }
                     }
                 }
             }

         }*/
        public IActionResult Dangkytaikhoan()
        {
            return View();
        }
        public IActionResult Dangxuat()
        {
            return View();
        }
        public IActionResult Doimatkhau()
        {
            return View();
        }
    }
}
