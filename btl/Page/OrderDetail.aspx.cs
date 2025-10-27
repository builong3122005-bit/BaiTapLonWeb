using btl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace btl.Page
{
    public partial class OrderDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOrderDetail();
            }
        }

        private void LoadOrderDetail()
        {
            if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out int orderId))
            {
                ShowError("ID đơn hàng không hợp lệ.");
                return;
            }

            List<Order> orders = (List<Order>)Application["orders"];
            Order currentOrder = orders?.FirstOrDefault(o => o.Id == orderId);

            if (currentOrder == null)
            {
                ShowError($"Không tìm thấy đơn hàng với ID = {orderId}.");
                return;
            }

            // Hiển thị thông tin cơ bản
            litOrderIdHeader.Text = "#" + currentOrder.Id.ToString();
            litOrderId.Text = "#" + currentOrder.Id.ToString();
            litOrderDate.Text = currentOrder.OrderDate.ToString("dd/MM/yyyy HH:mm");
            litCustomerName.Text = HttpUtility.HtmlEncode(currentOrder.CustomerName);
            litOrderStatus.Text = $"<span class='order-status {currentOrder.Status.ToLower()}'>{currentOrder.Status}</span>";

            List<CartItem> cartForThisOrder = GetCartItemsForOrder(orderId); 

            if (cartForThisOrder != null && cartForThisOrder.Any())
            {
                rptOrderItems.DataSource = cartForThisOrder;
                rptOrderItems.DataBind();
                litOrderItemsFallback.Visible = false;
            }
            else
            {
                rptOrderItems.Visible = false;
                litOrderItemsFallback.Visible = true;
                litOrderItemsFallback.Text = "<tr><td colspan='5'>Không có thông tin chi tiết sản phẩm cho đơn hàng này.</td></tr>";
            }

            double subtotal = currentOrder.TotalAmount; // Lấy tổng tiền từ Order
            double shippingFee = 0; // Tính phí ship nếu có logic
            double total = subtotal + shippingFee;

            litSubtotal.Text = $"{subtotal:N0}đ";
            litShippingFee.Text = $"{shippingFee:N0}đ";
            litTotal.Text = $"{total:N0}đ";

        }

        private List<CartItem> GetCartItemsForOrder(int orderId)
        {
            if (Session["LastPlacedOrderId"] != null && (int)Session["LastPlacedOrderId"] == orderId && Session["LastPlacedOrderCart"] != null)
            {
                return Session["LastPlacedOrderCart"] as List<CartItem>;
            }
            return null; 
        }

        private void ShowError(string message)
        {
            pnlContent.Visible = false;
            litErrorMessage.Visible = true;
            litErrorMessage.Text = $"<div class='alert alert-danger'>{message} <a href='Profile.aspx?tab=orders'>Quay lại danh sách đơn hàng</a></div>";
        }
    }
}