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
    public partial class Checkout : System.Web.UI.Page
    {
        private List<CartItem> currentCart;
        private User currentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadCart();
            LoadCurrentUser();

            if (currentCart == null || currentCart.Count == 0)
            {
                pnlCheckoutContent.Visible = false;
                pnlCheckoutError.Visible = true;
                pnlCheckoutError.Controls.Add(new LiteralControl("<p>Giỏ hàng của bạn đang trống. Vui lòng thêm sản phẩm trước khi thanh toán.</p><a href='Products.aspx' class='btn btn-primary'>Quay lại cửa hàng</a>"));
                return;
            }

            if (!IsPostBack)
            {
                DisplayOrderSummary();
                PreFillUserInfo();
            }
        }

        private void LoadCart()
        {
            currentCart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
        }

        private void LoadCurrentUser()
        {
            if (Request.Cookies["User"] != null && int.TryParse(Request.Cookies["User"].Value, out int userId))
            {
                List<User> users = (List<User>)Application["users"];
                currentUser = users?.FirstOrDefault(u => u.id == userId);
            }
            else
            {
                currentUser = null;
            }
        }

        private void DisplayOrderSummary()
        {
            StringBuilder itemsHtml = new StringBuilder();
            double subtotal = 0;

            foreach (var item in currentCart)
            {
                itemsHtml.AppendFormat(@"<div class='order-item'>
                                          <span class='item-name'>{0} (Size: {2}) x <strong>{1}</strong></span>
                                          <span class='item-price'>{3:N0}đ</span>
                                      </div>",
                                      HttpUtility.HtmlEncode(item.ProductName),
                                      item.Quantity,
                                      HttpUtility.HtmlEncode(item.Size),
                                      item.TotalPrice);
                subtotal += item.TotalPrice;
            }

            litOrderItems.Text = itemsHtml.ToString();
            lblSubtotal.Text = $"{subtotal:N0}đ";
            double total = subtotal;
            lblTotal.Text = $"{total:N0}đ";
        }

        private void PreFillUserInfo()
        {
            if (currentUser != null)
            {
                txtFullName.Text = currentUser.fullname;
                txtEmail.Text = currentUser.email;
            }
        }


        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            LoadCart(); 
            if (currentCart == null || currentCart.Count == 0)
            {
                Response.Redirect("Cart.aspx");
                return;
            }

            LoadCurrentUser(); 
            int userId = (currentUser != null) ? currentUser.id : 0;
            string customerName = txtFullName.Text;
            string customerAddress = txtAddress.Text; 
            string customerPhone = txtPhone.Text; 
            string customerEmail = txtEmail.Text; 
            string orderNotes = txtNotes.Text; 


            double totalAmount = currentCart.Sum(item => item.TotalPrice);

            List<Order> orders = (List<Order>)Application["orders"] ?? new List<Order>();
            int newOrderId = orders.Count > 0 ? orders.Max(o => o.Id) + 1 : 1001;

            Order newOrder = new Order
            {
                Id = newOrderId,
                UserId = userId,
                CustomerName = customerName,
                TotalAmount = totalAmount,
                Status = "Pending",
                OrderDate = DateTime.Now
            };
            orders.Add(newOrder);
            Application["orders"] = orders;

            Session["LastPlacedOrderId"] = newOrderId;
            Session["LastPlacedOrderCart"] = new List<CartItem>(currentCart); 


            List<Payment> payments = (List<Payment>)Application["payments"] ?? new List<Payment>();
            string paymentMethod = rblPaymentMethod.SelectedValue;
            Payment newPayment = new Payment
            {
                TransactionId = $"PM{newOrderId}",
                OrderId = newOrderId,
                Amount = totalAmount,
                Method = paymentMethod,
                Status = (paymentMethod == "COD") ? "Pending" : "Pending",
                PaymentDate = DateTime.Now
            };
            payments.Add(newPayment);
            Application["payments"] = payments;

            UpdateStockAfterOrder(currentCart);


            Session["Cart"] = null;

            Response.Redirect("OrderSuccess.aspx?orderId=" + newOrderId);
        }

        private void UpdateStockAfterOrder(List<CartItem> orderedItems)
        {
            List<Product> products = (List<Product>)Application["products"];
            if (products == null) return;

            bool updated = false;
            foreach (var item in orderedItems)
            {
                Product productToUpdate = products.FirstOrDefault(p => p.Id == item.ProductId);
                if (productToUpdate != null)
                {
                    productToUpdate.Stock -= item.Quantity;
                    if (productToUpdate.Stock < 0) productToUpdate.Stock = 0; 
                    updated = true;
                }
            }

            if (updated)
            {
                Application["products"] = products;
            }
        }
    }
}