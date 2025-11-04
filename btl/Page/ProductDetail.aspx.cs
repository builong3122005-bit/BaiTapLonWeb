using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions; // Thư viện cần thiết cho Regex
using System.Web; // Thư viện cần thiết cho HttpUtility
using System.Web.UI;
using btl.Models;

namespace btl.Page
{
    public partial class ProductDetail : System.Web.UI.Page
    {
        private Product currentProduct;

        protected void Page_Load(object sender, EventArgs e)
        {
            // LoadProductDetails luôn chạy để lấy thông tin sản phẩm
            LoadProductDetails();
            // Các xử lý khác trong !IsPostBack có thể giữ nguyên nếu cần
            if (!IsPostBack)
            {
                if (currentProduct != null) // Chỉ load sản phẩm liên quan nếu sản phẩm chính tồn tại
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
                // Hiển thị thông tin cơ bản
                mainImage.Src = ResolveUrl(currentProduct.ImageUrl ?? "~/assets/img/placeholder.png");
                mainImage.Alt = currentProduct.Name;
                productName.InnerText = currentProduct.Name;
                productPrice.InnerText = $"{currentProduct.Price:N0}đ";

                // Xử lý mô tả
                string fullDescription = currentProduct.Description ?? "Chưa có mô tả cho sản phẩm này.";

                // 1. Loại bỏ tất cả thẻ HTML để lấy văn bản thuần túy cho mô tả ngắn
                string plainTextDescription = Regex.Replace(fullDescription, "<.*?>", string.Empty).Trim();
                // 2. Thay thế nhiều khoảng trắng/xuống dòng thừa thành một khoảng trắng
                plainTextDescription = Regex.Replace(plainTextDescription, @"\s+", " ");
                // 3. Lấy 150 ký tự đầu tiên của văn bản thuần túy làm tóm tắt
                productShortDescription.InnerText = plainTextDescription.Length > 150
                                                    ? plainTextDescription.Substring(0, 150) + "..."
                                                    : plainTextDescription;

                // 4. Gán mô tả đầy đủ (vẫn còn HTML) cho phần chi tiết (Literal)
                litLongDescription.Text = fullDescription;
            }
        }

        // === PHƯƠNG THỨC NÀY ĐÃ ĐƯỢC THAY ĐỔI ĐỂ KIỂM TRA ĐĂNG NHẬP ===
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            // *** BƯỚC 1: KIỂM TRA ĐĂNG NHẬP ***
            if (Request.Cookies["User"] == null)
            {
                // *** CHƯA ĐĂNG NHẬP ***

                // Lấy URL trang Login
                string loginUrl = ResolveUrl("~/Page/Login.aspx");
                // Lấy URL trang hiện tại (ProductDetail.aspx?id=...) để quay lại sau khi đăng nhập
                string returnUrl = Request.Url.PathAndQuery;

                // (Tùy chọn) Hiển thị thông báo yêu cầu đăng nhập - Bỏ dòng này nếu không cần
                // lblAddToCartMessage.Text = "Vui lòng đăng nhập để thêm sản phẩm vào giỏ hàng!";
                // lblAddToCartMessage.CssClass = "add-cart-message error";
                // lblAddToCartMessage.Visible = true;

                // *** CHUYỂN HƯỚNG NGAY LẬP TỨC ĐẾN TRANG LOGIN ***
                Response.Redirect($"{loginUrl}?ReturnUrl={HttpUtility.UrlEncode(returnUrl)}", false);
                Context.ApplicationInstance.CompleteRequest(); // Dừng xử lý trang hiện tại
                return; // Dừng thực thi phương thức này
            }

            // *** BƯỚC 2: NẾU ĐÃ ĐĂNG NHẬP, TIẾP TỤC THÊM VÀO GIỎ ***
            // (Code gốc của bạn)

            // Kiểm tra lại currentProduct vì Page_Load chạy trước Event Handler
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

            string selectedSize = rblSize.SelectedValue; // Lấy size đã chọn

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
            // Tìm UserControl Header (có thể nằm trong MasterPage hoặc trực tiếp trên Page)
            var header = (btl.UserControl.Header)FindControlRecursive(Page, "header1");
            header?.UpdateCartCount();

            lblAddToCartMessage.Text = $"Đã thêm {quantityToAdd} \"{currentProduct.Name}\" (Size: {selectedSize}) vào giỏ!";
            lblAddToCartMessage.CssClass = "add-cart-message success";
            lblAddToCartMessage.Visible = true; // Đảm bảo Label hiển thị
        }
        // === KẾT THÚC THAY ĐỔI ===

        private void ShowError(string message)
        {
            pnlProductDetail.Visible = false; // Ẩn panel chi tiết sản phẩm
            litErrorMessage.Visible = true;
            litErrorMessage.Text = $"<div class='container py-3'><div class='alert alert-danger'>{message} <a href='{ResolveUrl("~/Page/Products.aspx")}'>Quay lại danh sách sản phẩm</a></div></div>";
        }

        // Hàm helper để tìm control lồng nhau (ví dụ Header trong MasterPage)
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
            if (allProducts == null || !allProducts.Any()) return; // Kiểm tra null hoặc rỗng

            var relatedProducts = allProducts
                                    .Where(p => p.CategoryId == currentCategoryId && p.Id != currentProductId)
                                    .Take(4); // Lấy tối đa 4 sản phẩm

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
                // Bỏ thẻ div thừa đi
                htmlBuilder.Append("<p>Không có sản phẩm liên quan.</p>");
            }
            litRelatedProducts.Text = htmlBuilder.ToString();
        }

        private string GenerateProductCardHtml(Product product)
        {
            // Đảm bảo ResolveUrl được gọi đúng cách
            string imageUrl = ResolveUrl(product.ImageUrl ?? "~/assets/img/placeholder.png");
            // Sử dụng ResolveUrl cho link sản phẩm để đảm bảo đường dẫn đúng
            string productDetailUrl = ResolveUrl($"~/Page/ProductDetail.aspx?id={product.Id}");
            string productNameEncoded = HttpUtility.HtmlEncode(product.Name);

            // Cập nhật lại HTML card sản phẩm (nếu cần thay đổi class hoặc cấu trúc)
            return $@"
                 <div class='grid-item'>
                     <div class='product-card'>
                         <a href='{productDetailUrl}' class='product-image-link'>
                             <img src='{imageUrl}' alt='{productNameEncoded}' class='product-image' loading='lazy' />
                         </a>
                         <div class='product-info'>
                             <h3 class='product-name'><a href='{productDetailUrl}'>{productNameEncoded}</a></h3>
                             <p class='product-price'>{product.Price:N0}đ</p>
                             <%-- Có thể thêm nút Add to cart nhỏ ở đây nếu muốn --%>
                         </div>
                     </div>
                 </div>";
        }
    }
}