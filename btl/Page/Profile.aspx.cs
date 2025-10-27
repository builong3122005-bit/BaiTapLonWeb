using btl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace btl.Page
{
    // Đổi tên class thành Profile
    public partial class Profile : System.Web.UI.Page
    {
        private User currentUser;
        private List<Order> userOrders;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadCurrentUserAndOrders();

            // Kiểm tra đăng nhập
            if (currentUser == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                BindUserInfo();
                BindUserOrders();
            }
        }

        private void LoadCurrentUserAndOrders()
        {
            if (Request.Cookies["User"] != null && int.TryParse(Request.Cookies["User"].Value, out int userId))
            {
                List<User> users = (List<User>)Application["users"];
                currentUser = users?.FirstOrDefault(u => u.id == userId);

                if (currentUser != null)
                {
                    List<Order> allOrders = (List<Order>)Application["orders"];
                    userOrders = allOrders?.Where(o => o.UserId == currentUser.id).OrderByDescending(o => o.OrderDate).ToList() ?? new List<Order>();
                }
                else
                {
                    userOrders = new List<Order>();
                }
            }
            else
            {
                currentUser = null;
                userOrders = new List<Order>();
            }
        }

        private void BindUserInfo()
        {
            if (currentUser != null)
            {
                litWelcomeName.Text = HttpUtility.HtmlEncode(currentUser.fullname);
                litFullName.Text = HttpUtility.HtmlEncode(currentUser.fullname);
                litEmail.Text = HttpUtility.HtmlEncode(currentUser.email);
                // Gán các thông tin khác nếu có
                // litPhone.Text = HttpUtility.HtmlEncode(currentUser.Phone ?? "Chưa cập nhật");
                // litAddress.Text = HttpUtility.HtmlEncode(currentUser.Address ?? "Chưa cập nhật");

                // Thống kê
                litTotalOrders.Text = userOrders.Count.ToString();
                double totalSpent = userOrders.Where(o => o.Status == "Shipped" || o.Status == "Completed").Sum(o => o.TotalAmount); // Chỉ tính đơn hoàn thành/đã giao
                litTotalSpent.Text = $"{totalSpent:N0}đ";
            }
        }

        private void BindUserOrders()
        {
            rptOrders.DataSource = userOrders;
            rptOrders.DataBind();
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Xóa cookie User
            if (Request.Cookies["User"] != null)
            {
                HttpCookie userCookie = new HttpCookie("User");
                userCookie.Expires = DateTime.Now.AddDays(-1d); // Hết hạn cookie
                Response.Cookies.Add(userCookie);
            }

            // Chuyển hướng về trang đăng nhập
            Response.Redirect("Login.aspx");
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) // Kiểm tra validator
            {
                return;
            }

            if (currentUser == null) return;

            List<User> users = (List<User>)Application["users"];
            User userToUpdate = users.FirstOrDefault(u => u.id == currentUser.id);

            if (userToUpdate == null) return;

            // Kiểm tra mật khẩu hiện tại
            // LƯU Ý: Nên mã hóa mật khẩu khi lưu trữ và so sánh bản mã hóa
            if (userToUpdate.password != txtCurrentPassword.Text)
            {
                lblPasswordMessage.Text = "Mật khẩu hiện tại không đúng.";
                lblPasswordMessage.CssClass = "password-message error";
                return;
            }

            // Cập nhật mật khẩu mới
            userToUpdate.password = txtNewPassword.Text; // Nên mã hóa trước khi lưu
            Application["users"] = users; // Lưu lại danh sách user đã cập nhật

            lblPasswordMessage.Text = "Đổi mật khẩu thành công!";
            lblPasswordMessage.CssClass = "password-message success";

            // Xóa trắng các ô input
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
        }
    }
}