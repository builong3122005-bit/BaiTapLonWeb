using btl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace btl.Page
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCart();
            }
        }

        private void BindCart()
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();

            if (cart.Count == 0)
            {
                pnlCartEmpty.Visible = true;
                pnlCartView.Visible = false;
            }
            else
            {
                pnlCartEmpty.Visible = false;
                pnlCartView.Visible = true;
                rptCartItems.DataSource = cart;
                rptCartItems.DataBind();
                UpdateCartSummary(cart);
            }
        }

        private void UpdateCartSummary(List<CartItem> cart)
        {
            double subtotal = cart.Sum(item => item.TotalPrice);
            // double shippingFee = 0; // Tính phí ship nếu cần
            double total = subtotal; // + shippingFee;

            lblSubtotal.Text = $"{subtotal:N0}đ";
            lblTotal.Text = $"{total:N0}đ";
        }

        protected void rptCartItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            string[] args = e.CommandArgument.ToString().Split(',');
            int productId = int.Parse(args[0]);
            string size = args[1];

            CartItem itemToModify = cart.FirstOrDefault(item => item.ProductId == productId && item.Size == size);

            if (itemToModify == null) return;

            if (e.CommandName == "RemoveItem")
            {
                cart.Remove(itemToModify);
            }
            else if (e.CommandName == "UpdateQuantity")
            {
                TextBox txtQuantity = (TextBox)e.Item.FindControl("txtQuantity");
                if (int.TryParse(txtQuantity.Text, out int newQuantity) && newQuantity > 0)
                {
                    itemToModify.Quantity = newQuantity;
                }
                else
                {
                    txtQuantity.Text = itemToModify.Quantity.ToString(); 
                }
            }

            Session["Cart"] = cart;
            BindCart(); 
        }
    }
}