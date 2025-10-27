using System;
using System.Collections.Generic;
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
                if (!Password.Equals(ConfirmPassword))
                {
                    errorMessage.InnerHtml = "Vui lòng nhập mật khẩu khớp";
                    return;
                }
                else
                {
                    errorMessage.InnerHtml = "";
                }
                User user = new User();
                user.fullname = name.Value;
                user.email = email.Value;
                user.password = Password;
                user.phoneNumber = "";
                user.role = "USER";


                List<User> users = (List<User>)Application["users"];
                users.Add(user);

                Application["users"] = users;
                Response.Redirect("~/Page/Login.aspx");
            }
        }


    }
}