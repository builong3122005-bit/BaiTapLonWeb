using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using btl.Models;

namespace btl.Page
{
    public partial class Products : System.Web.UI.Page
    {
        // Các control này được liên kết tự động qua tệp .designer.cs,
        // không cần khai báo lại ở đây.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load danh mục vào CheckBoxList khi trang tải lần đầu
                LoadCategories();
                // Load sản phẩm ban đầu (áp dụng bộ lọc mặc định nếu có)
                ApplyFilters();
            }
        }

        // Hàm Load danh mục vào CheckBoxList
        private void LoadCategories()
        {
            List<Category> categories = Application["categories"] as List<Category>;
            // Kiểm tra control tồn tại trước khi sử dụng
            if (categories != null && cblCategories != null)
            {
                cblCategories.DataSource = categories;
                cblCategories.DataTextField = "Name"; // Thuộc tính Name của Category để hiển thị
                cblCategories.DataValueField = "Id";   // Thuộc tính Id của Category làm giá trị
                cblCategories.DataBind();
            }
        }


        // Phương thức này sẽ được gọi khi bất kỳ bộ lọc nào thay đổi (do AutoPostBack="true")
        protected void Filters_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        // Hàm lọc và hiển thị sản phẩm
        private void ApplyFilters()
        {
            List<Product> allProducts = Application["products"] as List<Product>;
            if (allProducts == null)
            {
                ShowNoProductsMessage("Lỗi tải danh sách sản phẩm.");
                return;
            }

            IEnumerable<Product> filteredProducts = allProducts;

            // --- Lọc theo Danh mục (Category) ---
            if (cblCategories != null) // Kiểm tra control tồn tại
            {
                List<string> selectedCategoryIds = cblCategories.Items.Cast<ListItem>()
                                                .Where(li => li.Selected)
                                                .Select(li => li.Value)
                                                .ToList();

                if (selectedCategoryIds.Any())
                {
                    List<int> categoryIds = selectedCategoryIds.Select(int.Parse).ToList();
                    filteredProducts = filteredProducts.Where(p => categoryIds.Contains(p.CategoryId));
                }
            }


            // --- Lọc theo Giá (Price Range) ---
            if (cblPriceRanges != null) // Kiểm tra control tồn tại
            {
                List<string> selectedPriceRanges = cblPriceRanges.Items.Cast<ListItem>()
                                                .Where(li => li.Selected)
                                                .Select(li => li.Value)
                                                .ToList();

                if (selectedPriceRanges.Any())
                {
                    List<Product> priceFilteredProducts = new List<Product>();
                    foreach (string rangeValue in selectedPriceRanges)
                    {
                        switch (rangeValue)
                        {
                            case "1": // Dưới 500k
                                priceFilteredProducts.AddRange(filteredProducts.Where(p => p.Price < 500000));
                                break;
                            case "2": // 500k - 1tr
                                priceFilteredProducts.AddRange(filteredProducts.Where(p => p.Price >= 500000 && p.Price <= 1000000));
                                break;
                            case "3": // Trên 1tr
                                priceFilteredProducts.AddRange(filteredProducts.Where(p => p.Price > 1000000));
                                break;
                        }
                    }
                    // Loại bỏ sản phẩm trùng lặp nếu chọn nhiều khoảng giá
                    filteredProducts = priceFilteredProducts.Distinct();
                }
            }


            // --- Phân trang (Pagination) ---
            // (Thêm logic phân trang ở đây nếu bạn muốn)
            List<Product> finalProductList = filteredProducts.ToList();


            // --- Hiển thị kết quả ---
            if (finalProductList.Any())
            {
                if (rptProductList != null) // Kiểm tra control tồn tại
                {
                    rptProductList.DataSource = finalProductList;
                    rptProductList.DataBind();
                    rptProductList.Visible = true;
                    if (litNoProducts != null) litNoProducts.Visible = false; // Ẩn thông báo "không có sản phẩm"
                }
            }
            else
            {
                ShowNoProductsMessage("Không tìm thấy sản phẩm phù hợp.");
            }
        }

        // Hàm hiển thị thông báo khi không có sản phẩm
        private void ShowNoProductsMessage(string message)
        {
            if (rptProductList != null) // Kiểm tra control tồn tại
            {
                rptProductList.DataSource = null;
                rptProductList.DataBind();
                rptProductList.Visible = false; // Ẩn repeater
            }
            if (litNoProducts != null) // Kiểm tra control tồn tại
            {
                litNoProducts.Text = $"<div class='grid-item full-width'><p>{message}</p></div>";
                litNoProducts.Visible = true; // Hiện thông báo
            }
        }


        // === PHƯƠNG THỨC NÀY ĐÃ ĐƯỢC THAY ĐỔI ĐỂ KIỂM TRA ĐĂNG NHẬP VÀ BỎ ALERT THÀNH CÔNG ===
        protected void rptProductList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {
                // *** BƯỚC 1: KIỂM TRA ĐĂNG NHẬP ***
                if (Request.Cookies["User"] == null)
                {
                    // *** CHƯA ĐĂNG NHẬP ***
                    string loginUrl = ResolveUrl("~/Page/Login.aspx");
                    string returnUrl = Request.Url.PathAndQuery;
                    // Chuyển hướng đến trang Login, đính kèm URL trang hiện tại để quay lại
                    Response.Redirect($"{loginUrl}?ReturnUrl={HttpUtility.UrlEncode(returnUrl)}", false);
                    Context.ApplicationInstance.CompleteRequest(); // Dừng xử lý trang hiện tại
                    return; // Dừng thực thi phương thức này
                }

                // *** BƯỚC 2: NẾU ĐÃ ĐĂNG NHẬP, TIẾP TỤC THÊM VÀO GIỎ ***
                try
                {
                    int productId = Convert.ToInt32(e.CommandArgument);
                    List<Product> allProducts = (List<Product>)Application["products"];
                    Product productToAdd = allProducts?.FirstOrDefault(p => p.Id == productId);

                    if (productToAdd != null)
                    {
                        List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
                        string defaultSize = "M"; // Size mặc định khi thêm nhanh
                        int defaultQuantity = 1; // Số lượng mặc định
                        CartItem existingItem = cart.FirstOrDefault(item => item.ProductId == productId && item.Size == defaultSize);

                        if (existingItem != null)
                        {
                            existingItem.Quantity += defaultQuantity; // Tăng số lượng nếu đã có
                        }
                        else
                        {
                            // Thêm mới vào giỏ
                            cart.Add(new CartItem
                            {
                                ProductId = productToAdd.Id,
                                ProductName = productToAdd.Name,
                                Price = productToAdd.Price,
                                Quantity = defaultQuantity,
                                ImageUrl = productToAdd.ImageUrl,
                                Size = defaultSize
                            });
                        }
                        Session["Cart"] = cart; // Lưu lại giỏ hàng vào Session

                        // Cập nhật số lượng hiển thị trên icon giỏ hàng ở Header
                        var header = (btl.UserControl.Header)FindControlRecursive(this.Page, "header1");
                        header?.UpdateCartCount();

                        // === DÒNG ALERT THÀNH CÔNG ĐÃ BỊ XÓA BỎ ===
                        // string script = $"alert('Đã thêm \"{HttpUtility.JavaScriptStringEncode(productToAdd.Name)}\" (Size: {defaultSize}) vào giỏ!');";
                        // if (ScriptManager1 != null) {
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertAddSuccess" + productId, script, true);
                        // } else {
                        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alertAddSuccess" + productId, script, true);
                        // }
                        // === KẾT THÚC PHẦN BỊ XÓA ===

                    }
                    else
                    {
                        // Thông báo lỗi nếu không tìm thấy sản phẩm (ID không đúng) - Vẫn giữ lại alert lỗi
                        string script = "alert('Lỗi: Không tìm thấy sản phẩm!');";
                        if (ScriptManager1 != null)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertAddErrorNotFound", script, true);
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alertAddErrorNotFound", script, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có vấn đề xảy ra - Vẫn giữ lại alert lỗi
                    System.Diagnostics.Debug.WriteLine("Lỗi thêm vào giỏ từ Products.aspx: " + ex.Message);
                    string script = "alert('Đã có lỗi xảy ra khi thêm vào giỏ. Vui lòng thử lại!');";
                    if (ScriptManager1 != null)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertAddException", script, true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alertAddException", script, true);
                    }
                }
            }
        }
        // === KẾT THÚC THAY ĐỔI ===


        // Hàm helper để tìm control lồng nhau (ví dụ Header trong MasterPage hoặc Page)
        private Control FindControlRecursive(Control rootControl, string controlID)
        {
            if (rootControl == null) return null;
            Control foundControl = rootControl.FindControl(controlID);
            if (foundControl != null) return foundControl;

            if (rootControl.HasControls())
            {
                foreach (Control controlToSearch in rootControl.Controls)
                {
                    foundControl = FindControlRecursive(controlToSearch, controlID);
                    if (foundControl != null) return foundControl;
                }
            }

            // Kiểm tra cả MasterPage nếu trang hiện tại có MasterPage
            System.Web.UI.Page currentPage = rootControl as System.Web.UI.Page;
            if (currentPage != null && currentPage.Master != null)
            {
                foundControl = FindControlRecursive(currentPage.Master, controlID);
                if (foundControl != null) return foundControl;
            }

            return null;
        }


        // (Optional) Hàm này chỉ cần thiết nếu bạn dùng Literal litProductList thay vì Repeater rptProducts
        // Hàm này không được sử dụng vì bạn đang dùng Repeater rptProductList
        /*
        private string GenerateProductListHtml(List<Product> products)
        {
             // ... code cũ ...
        }
        */
    }
}