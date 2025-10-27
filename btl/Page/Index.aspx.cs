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
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFeaturedProducts();
            }
        }

        /// <summary>
        /// Tải và hiển thị các sản phẩm nổi bật (ví dụ: 8 sản phẩm đầu tiên)
        /// </summary>
        private void LoadFeaturedProducts()
        {
            List<Product> products = (List<Product>)Application["products"];
            if (products == null || !products.Any())
            {
                litFeaturedProducts.Text = "<p>Chưa có sản phẩm nào.</p>";
                return;
            }

            var featuredProducts = products.Take(8);

            StringBuilder htmlBuilder = new StringBuilder();

            foreach (var product in featuredProducts)
            {
                htmlBuilder.Append(GenerateProductCardHtml(product));
            }

            litFeaturedProducts.Text = htmlBuilder.ToString();
        }

        /// <summary>
        /// Tạo chuỗi HTML cho một thẻ sản phẩm (product card)
        /// </summary>
        private string GenerateProductCardHtml(Product product)
        {
            string imageUrl = ResolveUrl(product.ImageUrl ?? "~/assets/img/placeholder.png");
            string productDetailUrl = $"ProductDetail.aspx?id={product.Id}"; 

            return $@"
                <div class='grid-item'>
                    <div class='product-card'>
                        <a href='{productDetailUrl}'>
                            <img src='{imageUrl}' alt='{HttpUtility.HtmlEncode(product.Name)}' class='product-image' />
                        </a>
                        <div class='product-info'>
                            <h3 class='product-name'><a href='{productDetailUrl}'>{HttpUtility.HtmlEncode(product.Name)}</a></h3>
                            <p class='product-price'>{product.Price:N0}đ</p>
                            
                        </div>
                    </div>
                </div>";
        }
    }
}