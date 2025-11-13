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

                // ✅ LẤY GIÁ TRỊ MỚI
                //string PhoneNum = phoneNumber.Value?.Trim();
                //string DisplayNameValue = displayName.Value?.Trim();


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

                //// ✅ KIỂM TRA SỐ ĐIỆN THOẠI ĐÃ TỒN TẠI
                //if (!string.IsNullOrWhiteSpace(PhoneNum) &&
                //    users.Any(u => !string.IsNullOrWhiteSpace(u.PhoneNumBer) &&
                //                   u.PhoneNumBer.Equals(PhoneNum, StringComparison.OrdinalIgnoreCase)))
                //{
                //    errorMessage.InnerHtml = "Số điện thoại này đã được đăng ký. Vui lòng sử dụng số khác.";
                //    return;
                //}


                errorMessage.InnerHtml = "";
                // ✅ TẠO USER MỚI VỚI ĐẦY ĐỦ THÔNG TIN
                User user = new User
                {
                    fullname = name.Value,
                    // display va phone
                    //DisplayName = string.IsNullOrWhiteSpace(DisplayNameValue) ? name.Value : DisplayNameValue,
                    email = Email,
                    //PhoneNumBer = PhoneNum,
                    password = Password,

                    role = "USER"
                };

                users.Add(user);
                Application["users"] = users;
                Response.Redirect("~/Page/Login.aspx");
            }
        }


    }
}