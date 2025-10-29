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
                UpdateCartCount();
                UpdateLoginStatus();
                SetActiveNavigation(); // ✅ Thêm dòng này để set active cho navigation
            }
        }

        /// <summary>
        /// Cập nhật số lượng sản phẩm trong giỏ hàng
        /// </summary>
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

        /// <summary>
        /// Hiển thị trạng thái đăng nhập (icon user hoặc link login)
        /// </summary>
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
                        // Đã đăng nhập: hiển thị link đến Profile
                        divLogin.InnerHtml = $@"<a href='{ResolveUrl("~/Page/Profile.aspx")}' aria-label='Tài khoản' title='{currentUser.fullname}'>
                                                    <img src='{ResolveUrl("~/assets/img/icon-user.png")}' alt='Tài khoản' class='icon-img' />
                                                 </a>";
                    }
                    else
                    {
                        // Cookie có nhưng user không tồn tại -> Xóa cookie và hiển thị Login
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
                    // Chưa đăng nhập: hiển thị link Login
                    divLogin.InnerHtml = $@"<a href='{ResolveUrl("~/Page/Login.aspx")}' aria-label='Đăng nhập'>
                                                 <img src='{ResolveUrl("~/assets/img/icon-user.png")}' alt='Đăng nhập' class='icon-img' />
                                             </a>";
                }
            }
        }

        /// <summary>
        /// ✅ HÀM MỚI: Tự động thêm class 'active' vào navigation link của trang hiện tại
        /// </summary>
        private void SetActiveNavigation()
        {
            // Lấy tên file của trang hiện tại (VD: "Products", "About", "Contact"...)
            string currentPage = System.IO.Path.GetFileNameWithoutExtension(Request.Url.AbsolutePath);

            // Tạo JavaScript để thêm class 'active' vào link tương ứng
            string script = $@"
                <script>
                    document.addEventListener('DOMContentLoaded', function() {{
                        const currentPage = '{currentPage}';
                        const navLinks = document.querySelectorAll('.nav-link');
                        
                        navLinks.forEach(link => {{
                            const pageName = link.getAttribute('data-page');
                            // So sánh tên trang (không phân biệt hoa thường)
                            if (pageName && pageName.toLowerCase() === currentPage.toLowerCase()) {{
                                link.classList.add('active');
                            }}
                        }});
                    }});
                </script>
            ";

            // Đăng ký script vào trang
            Page.ClientScript.RegisterStartupScript(this.GetType(), "SetActiveNav", script, false);
        }
    }
}