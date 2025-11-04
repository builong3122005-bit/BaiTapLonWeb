using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
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
                if (currentProduct != null)
                {
                    List<Product> products = (List<Product>)Application["products"];
                    LoadRelatedProducts(products ?? new List<Product>(), currentProduct.CategoryId, currentProduct.Id);
                    this.Title = $"{currentProduct.Name} - Shop Thời Trang ABC";
                }
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

                string fullDescription = currentProduct.Description ?? "Chưa có mô tả cho sản phẩm này.";
                string plainTextDescription = Regex.Replace(fullDescription, "<.*?>", string.Empty).Trim();
                plainTextDescription = Regex.Replace(plainTextDescription, @"\s+", " ");
                productShortDescription.InnerText = plainTextDescription.Length > 150
                                                    ? plainTextDescription.Substring(0, 150) + "..."
                                                    : plainTextDescription;

                litLongDescription.Text = fullDescription;
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["User"] == null)
            {
                string loginUrl = ResolveUrl("~/Page/Login.aspx");
                string returnUrl = Request.Url.PathAndQuery;
                Response.Redirect($"{loginUrl}?ReturnUrl={HttpUtility.UrlEncode(returnUrl)}", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (currentProduct == null)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]) && int.TryParse(Request.QueryString["id"], out int productId))
                {
                    List<Product> products = (List<Product>)Application["products"];
                    currentProduct = products?.FirstOrDefault(p => p.Id == productId);
                }
            }

            if (currentProduct == null)
            {
                lblAddToCartMessage.Text = "Lỗi: Không tìm thấy sản phẩm để thêm vào giỏ.";
                lblAddToCartMessage.CssClass = "add-cart-message error";
                lblAddToCartMessage.Visible = true;
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
            var header = (btl.UserControl.Header)FindControlRecursive(Page, "header1");
            header?.UpdateCartCount();

            lblAddToCartMessage.Text = $"Đã thêm {quantityToAdd} \"{currentProduct.Name}\" (Size: {selectedSize}) vào giỏ!";
            lblAddToCartMessage.CssClass = "add-cart-message success";
            lblAddToCartMessage.Visible = true;
        }

        private void ShowError(string message)
        {
            pnlProductDetail.Visible = false;
            litErrorMessage.Visible = true;
            litErrorMessage.Text = $"<div class='container py-3'><div class='alert alert-danger'>{message} <a href='{ResolveUrl("~/Page/Products.aspx")}'>Quay lại danh sách sản phẩm</a></div></div>";
        }

        private Control FindControlRecursive(Control rootControl, string controlID)
        {
            if (rootControl.ID == controlID) return rootControl;

            foreach (Control controlToSearch in rootControl.Controls)
            {
                Control controlToReturn = FindControlRecursive(controlToSearch, controlID);
                if (controlToReturn != null) return controlToReturn;
            }
            return null;
        }

        private void LoadRelatedProducts(List<Product> allProducts, int currentCategoryId, int currentProductId)
        {
            if (allProducts == null || !allProducts.Any()) return;

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
                htmlBuilder.Append("<p>Không có sản phẩm liên quan.</p>");
            }
            litRelatedProducts.Text = htmlBuilder.ToString();
        }

        private string GenerateProductCardHtml(Product product)
        {
            string imageUrl = ResolveUrl(product.ImageUrl ?? "~/assets/img/placeholder.png");
            string productDetailUrl = ResolveUrl($"~/Page/ProductDetail.aspx?id={product.Id}");
            string productNameEncoded = HttpUtility.HtmlEncode(product.Name);

            // ✅ LẤY MÔ TẢ NGẮN
            string shortDesc = product.ShortDescription;
            if (string.IsNullOrWhiteSpace(shortDesc) && !string.IsNullOrWhiteSpace(product.Description))
            {
                string plainText = Regex.Replace(product.Description, "<.*?>", string.Empty);
                shortDesc = plainText.Length > 100 ? plainText.Substring(0, 100) + "..." : plainText;
            }
            if (string.IsNullOrWhiteSpace(shortDesc))
            {
                shortDesc = "Chưa có mô tả";
            }
            string shortDescEncoded = HttpUtility.HtmlEncode(shortDesc);

            return $@"
                 <div class='grid-item'>
                     <div class='product-card'>
                         <a href='{productDetailUrl}' class='product-image-link'>
                             <img src='{imageUrl}' alt='{productNameEncoded}' class='product-image' loading='lazy' />
                         </a>
                         <div class='product-info'>
                             <h3 class='product-name'><a href='{productDetailUrl}'>{productNameEncoded}</a></h3>
                             <p class='product-description'>{shortDescEncoded}</p>
                         </div>
                     </div>
                 </div>";
        }
    }
}