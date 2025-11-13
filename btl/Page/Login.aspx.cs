using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using btl.Models;

namespace btl.Page
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) return;

            var listUser = Application["users"] as List<User> ?? new List<User>();

            // ✅ THAY ĐỔI: Lấy số điện thoại thay vì email
            string Email = (email?.Value ?? "").Trim();

            //string Email = (email?.Value ?? "").Trim();
            string Password = password?.Value ?? "";

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                errorMessage.Text = "Vui lòng nhập Email và mật khẩu.";
                return;
            }

            var userLogin = listUser.FirstOrDefault(u =>
            string.Equals(u.email, Email, StringComparison.OrdinalIgnoreCase)
            //u.PhoneNumBer.Equals(PhoneNum) && u.password == Password);
            && u.password == Password);

            if (userLogin != null)
            {
                // Tạo cookie mới và ghi bằng Response
                var cookie = new HttpCookie("User", userLogin.id.ToString())
                {
                    Expires = DateTime.Now.AddDays(1),
                    HttpOnly = true,                              // chống JS đọc
                    Secure = Request.IsSecureConnection           // chỉ gửi qua HTTPS
                };

                Response.Cookies.Add(cookie);

                if (userLogin.role.Equals("ADMIN")) { Response.Redirect("~/Page/Admin.aspx", false); }
                else Response.Redirect("~/Page/Index.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            errorMessage.Text = "Email hoặc mật khẩu không chính xác.";
        }
    }
}