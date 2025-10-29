using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using btl.Models;

namespace btl.Page
{
    public partial class ProductDetail : System.Web.UI.Page
    {
        private Product currentProduct;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadProductDetails();
            if (!IsPostBack)
            {
            }
        }

        private void LoadProductDetails()
        {
            if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out int productId))
            {
                ShowError("ID sản phẩm không hợp lệ.");
                currentProduct = null;
                return;
            }

            List<Product> products = (List<Product>)Application["products"];
            if (products == null)
            {
                ShowError("Không thể tải danh sách sản phẩm.");
                currentProduct = null;
                return;
            }

            currentProduct = products.FirstOrDefault(p => p.Id == productId);

            if (currentProduct == null)
            {
                ShowError($"Không tìm thấy sản phẩm với ID = {productId}.");
                return;
            }
            else
            {
                mainImage.Src = ResolveUrl(currentProduct.ImageUrl ?? "~/assets/img/placeholder.png");
                mainImage.Alt = currentProduct.Name;
                productName.InnerText = currentProduct.Name;
                productPrice.InnerText = $"{currentProduct.Price:N0}đ";

                //string fullDescription = currentProduct.Description ?? "Chưa có mô tả cho sản phẩm này.";
                //productShortDescription.InnerText = fullDescription.Length > 150
                //                                   ? HttpUtility.HtmlEncode(fullDescription.Substring(0, 150)) + "..."
                //                                   : HttpUtility.HtmlEncode(fullDescription);
                productShortDescription.InnerText = currentProduct.ShortDescription ?? ""; // Lấy trực tiếp từ trường mới
                litLongDescription.Text = currentProduct.Description ?? "Chưa có mô tả.";


                if (!IsPostBack)
                {
                    LoadRelatedProducts(products, currentProduct.CategoryId, currentProduct.Id);
                    this.Title = $"{currentProduct.Name} - Shop Thời Trang ABC";
                }
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (currentProduct == null)
            {
                lblAddToCartMessage.Text = "Lỗi: Không tìm thấy sản phẩm.";
                lblAddToCartMessage.CssClass = "add-cart-message error";
                return;
            }

            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();

            int quantityToAdd = 1;
            int.TryParse(quantity.Text, out quantityToAdd);
            if (quantityToAdd < 1) quantityToAdd = 1;

            string selectedSize = rblSize.SelectedValue;

            CartItem existingItem = cart.FirstOrDefault(item => item.ProductId == currentProduct.Id && item.Size == selectedSize);

            if (existingItem != null)
            {
                existingItem.Quantity += quantityToAdd;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = currentProduct.Id,
                    ProductName = currentProduct.Name,
                    Price = currentProduct.Price,
                    Quantity = quantityToAdd,
                    ImageUrl = currentProduct.ImageUrl,
                    Size = selectedSize
                });
            }

            Session["Cart"] = cart;
            var header = (btl.UserControl.Header)Page.FindControl("header1");
            header?.UpdateCartCount();
            lblAddToCartMessage.Text = $"Đã thêm {quantityToAdd} sản phẩm vào giỏ!";
            lblAddToCartMessage.CssClass = "add-cart-message success";

        }

        private void ShowError(string message)
        {
            pnlProductDetail.Visible = false;
            litErrorMessage.Visible = true;
            litErrorMessage.Text = $"<div class='alert alert-danger'>{message} <a href='Products.aspx'>Quay lại danh sách</a></div>";
        }

        //private string FormatDescription(string description)
        //{
        //    if (string.IsNullOrEmpty(description)) return "<p>Chưa có mô tả chi tiết.</p>";
        //    return "<p>" + HttpUtility.HtmlEncode(description).Replace("\n", "<br />") + "</p>";
        //}


        private void LoadRelatedProducts(List<Product> allProducts, int currentCategoryId, int currentProductId)
        {
            var relatedProducts = allProducts
                                    .Where(p => p.CategoryId == currentCategoryId && p.Id != currentProductId)
                                    .Take(4);

            StringBuilder htmlBuilder = new StringBuilder();
            if (relatedProducts.Any())
            {
                foreach (var product in relatedProducts)
                {
                    htmlBuilder.Append(GenerateProductCardHtml(product));
                }
            }
            else
            {
                htmlBuilder.Append("<div class='grid-item full-width'><p>Không có sản phẩm liên quan.</p></div>");
            }
            litRelatedProducts.Text = htmlBuilder.ToString();
        }

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