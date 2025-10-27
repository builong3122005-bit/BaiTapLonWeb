using System;
using System.Web.UI.WebControls;

namespace btl.Page
{
    public partial class OrderSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["orderId"]))
                {
                    lblOrderId.Text = "#" + Request.QueryString["orderId"];
                }
                else
                {
                    lblOrderId.Text = "(không xác định)";
                }
            }
        }
    }
}