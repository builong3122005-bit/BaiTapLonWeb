using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using btl.Models;

namespace btl.UserControl
{
    public partial class Header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //    string html = "<a href=\"Login.aspx\" aria-label=\"Tài khoản\">\r\n                        <img src=\"/assets/img/icon-user.png\" alt=\"Tài khoản\" class=\"icon-img\" />\r\n                    </a>";
                //    if (Request.Cookies["User"] != null)
                //    {
                //        int userid = Int32.Parse(Request.Cookies["User"].Value);
                //        List<User> listUser = Application["users"] as List<User> ?? new List<User>();
                //        var user = listUser.FirstOrDefault(u => u.id == userid);
                //        if (user != null)
                //        {
                //            html = $"<a href=\"Profile.aspx\" aria-label=\"Tài khoản\">" +
                //                $"<img src=\"/assets/img/icon-user.png\" alt=\"Tài khoản\" class=\"icon-img\" />" +
                //                $"<span>{user.fullname}</span>" +
                //                $"</a>";
                //        }
                //    }
                //    switch_login.InnerHtml = html;
                UpdateCartCount();
                UpdateLoginStatus();
            }
        }

        public void UpdateCartCount()
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            int totalItems = cart.Sum(item => item.Quantity);

            if (totalItems > 0)
            {
                cartItemCount.InnerText = totalItems.ToString();
                cartItemCount.Visible = true;
            }
            else
            {
                cartItemCount.Visible = false;
            }
        }

        private void UpdateLoginStatus()
        {
            HtmlGenericControl divLogin = (HtmlGenericControl)FindControl("switch_login");
            if (divLogin != null)
            {
                if (Request.Cookies["User"] != null && int.TryParse(Request.Cookies["User"].Value, out int userId))
                {
                    List<User> users = (List<User>)HttpContext.Current.Application["users"];
                    User currentUser = users?.FirstOrDefault(u => u.id == userId);

                    if (currentUser != null)
                    {
                        // Đã đăng nhập: (Phần này đã đúng, dùng icon-user.png)
                        divLogin.InnerHtml = $@"<a href='{ResolveUrl("~/Page/Profile.aspx")}' aria-label='Tài khoản' title='{currentUser.fullname}'>
                                                    <img src='{ResolveUrl("~/assets/img/icon-user.png")}' alt='Tài khoản' class='icon-img' />
                                                 </a>";
                    }
                    else
                    {
                        // Cookie có nhưng user không tồn tại -> Hiển thị Login
                        HttpCookie userCookie = new HttpCookie("User");
                        userCookie.Expires = DateTime.Now.AddDays(-1d);
                        Response.Cookies.Add(userCookie);
                        divLogin.InnerHtml = $@"<a href='{ResolveUrl("~/Page/Login.aspx")}' aria-label='Đăng nhập'>
                                                     <img src='{ResolveUrl("~/assets/img/icon-user.png")}' alt='Đăng nhập' class='icon-img' />
                                                 </a>";
                    }
                }
                else
                {
                    // Chưa đăng nhập: Hiển thị link Login
                    divLogin.InnerHtml = $@"<a href='{ResolveUrl("~/Page/Login.aspx")}' aria-label='Đăng nhập'>
                                                 <img src='{ResolveUrl("~/assets/img/icon-user.png")}' alt='Đăng nhập' class='icon-img' />
                                             </a>";
                }
            }
        }

    }
}