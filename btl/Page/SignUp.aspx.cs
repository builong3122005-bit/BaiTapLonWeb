using System;
using System.Collections.Generic;
using System.Linq;
using btl.Models;

namespace btl.Page
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string Password = password.Value;
                string ConfirmPassword = confirm_password.Value;
                string Email = email.Value;

                // Kiểm tra mật khẩu khớp
                if (!Password.Equals(ConfirmPassword))
                {
                    errorMessage.InnerHtml = "Vui lòng nhập mật khẩu khớp";
                    return;
                }

                // Kiểm tra email đã tồn tại
                List<User> users = (List<User>)Application["users"];
                if (users.Any(u => u.email.Equals(Email, StringComparison.OrdinalIgnoreCase)))
                {
                    errorMessage.InnerHtml = "Email này đã được đăng ký. Vui lòng sử dụng email khác.";
                    return;
                }

                errorMessage.InnerHtml = "";

                User user = new User();
                user.fullname = name.Value;
                user.email = Email;
                user.password = Password;
                user.phoneNumber = "";
                user.role = "USER";

                users.Add(user);
                Application["users"] = users;
                Response.Redirect("~/Page/Login.aspx");
            }
        }


    }
}