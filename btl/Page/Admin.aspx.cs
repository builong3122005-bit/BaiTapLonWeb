using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using btl.Models;

namespace btl.Page
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HandleActions();

            if (!IsPostBack)
            {
                if (Request.Cookies["User"] != null)
                {
                    int userId = int.Parse(Request.Cookies["User"].Value);
                    List<User> users = (List<User>)Application["users"];
                    User currentUser = users.FirstOrDefault(u => u.id == userId);
                    if (currentUser == null || currentUser.role != "ADMIN")
                    {
                        Response.Redirect("Login.aspx");
                        return;
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
                loaduser();
                loadProducts();
                loadCategories();
                LoadCategoriesIntoDropdown();
                loadOrders();
                loadPayments();

                HandleEditActions();
            }
        }

        /// <summary>
        /// Xử lý các hành động Xóa (luôn chạy)
        /// </summary>
        private void HandleActions()
        {
            string action = Request.QueryString["action"];
            string idStr = Request.QueryString["id"];

            if (action == "logout")
            {
                Logout();
                return; // Dừng xử lý sau khi đăng xuất
            }

            if (string.IsNullOrEmpty(action) || string.IsNullOrEmpty(idStr)) return;

            int id = int.Parse(idStr);

            if (action == "delete_product")
            {
                DeleteProduct(id);
                Response.Redirect("Admin.aspx");
            }
            else if (action == "delete_category")
            {
                DeleteCategory(id);
                Response.Redirect("Admin.aspx");
            }
            else if (action == "delete_customer")
            {
                DeleteCustomer(id);
                Response.Redirect("Admin.aspx");
            }
            else if (action == "delete_order")
            {
                DeleteOrder(id);
                Response.Redirect("Admin.aspx");
            }
        }

        /// <summary>
        /// Xử lý các hành động Sửa (chỉ chạy khi !IsPostBack)
        /// </summary>
        private void HandleEditActions()
        {
            string action = Request.QueryString["action"];
            string idStr = Request.QueryString["id"];

            if (string.IsNullOrEmpty(action) || string.IsNullOrEmpty(idStr)) return;

            int id = int.Parse(idStr);

            if (action == "edit_product")
            {
                LoadProductForEdit(id);
            }
            else if (action == "edit_category")
            {
                LoadCategoryForEdit(id);
            }
            else if (action == "edit_customer")
            {
                LoadCustomerForEdit(id);
            }
            else if (action == "edit_order")
            {
                LoadOrderForEdit(id);
            }
        }


        // ===================================================================
        // KHÁCH HÀNG (USERS)
        // ===================================================================
        private void loaduser()
        {
            List<User> users = (List<User>)Application["users"];
            List<Order> orders = (List<Order>)Application["orders"] ?? new List<Order>();
            if (users == null) return;

            customersTableBody.InnerHtml = GenerateUserHtml(users, orders);
        }

        private string GenerateUserHtml(List<User> users, List<Order> orders)
        {
            string html = "";
            foreach (var user in users)
            {
                int orderCount = orders.Count(o => o.UserId == user.id); // Đếm tổng đơn
                html += " <tr>" +
                        $"<td>{user.id}</td>" +
                        $"<td>{user.fullname}</td>" +
                        $"<td>{user.email}</td>" +
                        $"<td>{orderCount}</td>" + // Hiển thị tổng đơn
                        "<td>" +
                        $"  <a href='Admin.aspx?action=edit_customer&id={user.id}' class='btn btn-small btn-warning'>Sửa</a>" +
                        $"  <a href='Admin.aspx?action=delete_customer&id={user.id}' class='btn btn-small btn-danger' onclick='return confirm(\"Bạn có chắc muốn xóa khách hàng này?\");'>Xóa</a>" +
                        "</td></tr>";
            }
            return html;
        }

        protected void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                List<User> users = (List<User>)Application["users"];
                string customerId = hdnCustomerId.Value;

                if (string.IsNullOrEmpty(customerId)) // THÊM MỚI
                {
                    User newUser = new User
                    {
                        id = users.Count > 0 ? users.Max(u => u.id) + 1 : 1,
                        fullname = txtCustomerName.Text,
                        email = txtCustomerEmail.Text,
                        password = txtCustomerPassword.Text, // (Lưu ý: nên mã hóa mật khẩu)
                        role = ddlCustomerRole.SelectedValue
                    };
                    users.Add(newUser);
                }
                else // CẬP NHẬT
                {
                    User userToUpdate = users.FirstOrDefault(u => u.id == int.Parse(customerId));
                    if (userToUpdate != null)
                    {
                        userToUpdate.fullname = txtCustomerName.Text;
                        userToUpdate.email = txtCustomerEmail.Text;
                        userToUpdate.role = ddlCustomerRole.SelectedValue;
                        if (!string.IsNullOrEmpty(txtCustomerPassword.Text))
                        {
                            userToUpdate.password = txtCustomerPassword.Text;
                        }
                    }
                }
                Application["users"] = users;
                loaduser();
                ClearCustomerForm();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideModal", "closeModal('customerModal');", true);
                Response.Redirect("Admin.aspx");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LỖI LƯU CUSTOMER: {ex.Message}");
            }
        }

        private void LoadCustomerForEdit(int id)
        {
            List<User> users = (List<User>)Application["users"];
            User userToEdit = users.FirstOrDefault(u => u.id == id);
            if (userToEdit != null)
            {
                hdnCustomerId.Value = userToEdit.id.ToString();
                txtCustomerName.Text = userToEdit.fullname;
                txtCustomerEmail.Text = userToEdit.email;
                ddlCustomerRole.SelectedValue = userToEdit.role;
                txtCustomerPassword.Text = ""; // Để trống mật khẩu

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", "openModal('customerModal');", true);
                string setTitleScript = "document.querySelector('#customerModal .modal-title').textContent = 'Cập nhật khách hàng';";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SetTitle", setTitleScript, true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPage", "showPage('customers');", true);
            }
        }

        private void DeleteCustomer(int id)
        {
            List<User> users = (List<User>)Application["users"];
            User userToRemove = users.FirstOrDefault(u => u.id == id);
            if (userToRemove != null)
            {
                users.Remove(userToRemove);
                Application["users"] = users;
            }
        }

        private void ClearCustomerForm()
        {
            hdnCustomerId.Value = "";
            txtCustomerName.Text = "";
            txtCustomerEmail.Text = "";
            txtCustomerPassword.Text = "";
            ddlCustomerRole.SelectedValue = "USER";
        }


        // ===================================================================
        // SẢN PHẨM (PRODUCTS) 
        // ===================================================================
        private void loadProducts()
        {
            List<Product> products = (List<Product>)Application["products"];
            if (products == null)
            {
                products = new List<Product>();
                Application["products"] = products;
            }
            productsTshirtTableBody.InnerHtml = GenerateProductHtml(products.Where(p => p.CategoryId == 1).ToList());
            productsSomiTableBody.InnerHtml = GenerateProductHtml(products.Where(p => p.CategoryId == 2).ToList());
            productsKhoacTableBody.InnerHtml = GenerateProductHtml(products.Where(p => p.CategoryId == 3).ToList());
            productsJeansTableBody.InnerHtml = GenerateProductHtml(products.Where(p => p.CategoryId == 4).ToList());
            productsShortsTableBody.InnerHtml = GenerateProductHtml(products.Where(p => p.CategoryId == 5).ToList());
        }

        private string GenerateProductHtml(List<Product> productList)
        {
            string html = "";
            foreach (var product in productList)
            {
                html += "<tr>" +
                        $"<td>{product.Id}</td>" +
                        $"<td><img src='{product.ImageUrl}' alt='{product.Name}' class='product-thumbnail' /></td>" +
                        $"<td>{product.Name}</td>" +
                        $"<td>{product.Price:N0} VNĐ</td>" +
                        $"<td>{product.Stock}</td>" +
                        "<td>" +
                        $"  <a href='Admin.aspx?action=edit_product&id={product.Id}' class='btn btn-small btn-warning'>Sửa</a>" +
                        $"  <a href='Admin.aspx?action=delete_product&id={product.Id}' class='btn btn-small btn-danger' onclick='return confirm(\"Bạn có chắc muốn xóa sản phẩm này?\");'>Xóa</a>" +
                        "</td></tr>";
            }
            return html;
        }

        /// <summary>
        /// Tải danh sách Categories từ Application vào DropDownList ddlCategory trong Product Modal
        /// </summary>
        private void LoadCategoriesIntoDropdown()
        {
            List<Category> categories = (List<Category>)Application["categories"];
            if (categories != null)
            {
                while (ddlCategory.Items.Count > 1)
                {
                    ddlCategory.Items.RemoveAt(1);
                }

                // Thêm các danh mục từ Application
                foreach (var category in categories)
                {
                    ddlCategory.Items.Add(new System.Web.UI.WebControls.ListItem(category.Name, category.Id.ToString()));
                }
            }
        }

        protected void btnSaveProduct_Click(object sender, EventArgs e)
        {
            string imageUrl = "";
            try
            {
                if (fileUploadImage.HasFile)
                {
                    try
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileUploadImage.FileName);
                        string physicalPath = Server.MapPath("~/Uploads/Products/");
                        if (!Directory.Exists(physicalPath))
                        {
                            Directory.CreateDirectory(physicalPath);
                        }
                        string savePath = Path.Combine(physicalPath, fileName);
                        fileUploadImage.SaveAs(savePath);
                        imageUrl = "/Uploads/Products/" + fileName;
                    }
                    catch (Exception exFile)
                    {
                        Debug.WriteLine($"LỖI LƯU FILE: {exFile.Message}");
                        return;
                    }
                }

                List<Product> products = (List<Product>)Application["products"];
                string productId = hdnProductId.Value;

                if (string.IsNullOrEmpty(productId))
                {
                    Product newProduct = new Product
                    {
                        Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1,
                        Name = txtName.Text,
                        CategoryId = int.Parse(ddlCategory.SelectedValue),
                        Price = double.Parse(txtPrice.Text),
                        Stock = int.Parse(txtStock.Text),
                        ImageUrl = string.IsNullOrEmpty(imageUrl) ? "/Uploads/Products/default.jpg" : imageUrl
                    };
                    products.Add(newProduct);
                }
                else
                {
                    Product productToUpdate = products.FirstOrDefault(p => p.Id == int.Parse(productId));
                    if (productToUpdate != null)
                    {
                        productToUpdate.Name = txtName.Text;
                        productToUpdate.CategoryId = int.Parse(ddlCategory.SelectedValue);
                        productToUpdate.Price = double.Parse(txtPrice.Text);
                        productToUpdate.Stock = int.Parse(txtStock.Text);
                        if (!string.IsNullOrEmpty(imageUrl))
                        {
                            productToUpdate.ImageUrl = imageUrl;
                        }
                    }
                }

                Application["products"] = products;
                loadProducts();
                ClearProductForm();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideModal", "closeModal('productModal');", true);
                Response.Redirect("Admin.aspx");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LỖI CHUNG TRONG btnSaveProduct_Click: {ex.Message}");
            }
        }

        private void LoadProductForEdit(int id)
        {
            List<Product> products = (List<Product>)Application["products"];
            Product productToEdit = products.FirstOrDefault(p => p.Id == id);
            if (productToEdit != null)
            {
                hdnProductId.Value = productToEdit.Id.ToString();
                txtName.Text = productToEdit.Name;
                ddlCategory.SelectedValue = productToEdit.CategoryId.ToString();
                txtPrice.Text = productToEdit.Price.ToString();
                txtStock.Text = productToEdit.Stock.ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", "openModal('productModal');", true);
                string setTitleScript = "document.querySelector('#productModal .modal-title').textContent = 'Cập nhật sản phẩm';";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SetTitle", setTitleScript, true);
                string pageName = GetPageNameFromCategoryId(productToEdit.CategoryId);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPage", $"showPage('{pageName}');", true);
            }
        }

        private void DeleteProduct(int id)
        {
            List<Product> products = (List<Product>)Application["products"];
            Product productToRemove = products.FirstOrDefault(p => p.Id == id);
            if (productToRemove != null)
            {
                products.Remove(productToRemove);
                Application["products"] = products;
            }
        }

        private void ClearProductForm()
        {
            hdnProductId.Value = "";
            txtName.Text = "";
            ddlCategory.SelectedIndex = 0;
            txtPrice.Text = "";
            txtStock.Text = "";
        }

        private string GetPageNameFromCategoryId(int categoryId)
        {
            List<Category> categories = (List<Category>)Application["categories"];
            Category cat = categories.FirstOrDefault(c => c.Id == categoryId);
            return cat != null ? cat.PageData : "dashboard";
        }


        // ===================================================================
        // DANH MỤC (CATEGORIES)
        // ===================================================================
        private void loadCategories()
        {
            List<Category> categories = (List<Category>)Application["categories"] ?? new List<Category>();
            List<Product> products = (List<Product>)Application["products"] ?? new List<Product>();
            categoriesTableBody.InnerHtml = GenerateCategoryHtml(categories, products);
        }

        private string GenerateCategoryHtml(List<Category> categories, List<Product> products)
        {
            string html = "";
            foreach (var category in categories)
            {
                int productCount = products.Count(p => p.CategoryId == category.Id);
                html += "<tr>" +
                        $"<td>{category.Id}</td>" +
                        $"<td>{category.Name}</td>" +
                        $"<td>{category.PageData}</td>" +
                        $"<td>{productCount}</td>" +
                        "<td>" +
                        $"  <a href='Admin.aspx?action=edit_category&id={category.Id}' class='btn btn-small btn-warning'>Sửa</a>" +
                        $"  <a href='Admin.aspx?action=delete_category&id={category.Id}' class='btn btn-small btn-danger' onclick='return confirm(\"Bạn có chắc muốn xóa danh mục này?\");'>Xóa</a>" +
                        "</td></tr>";
            }
            return html;
        }

        protected void btnSaveCategory_Click(object sender, EventArgs e)
        {
            try
            {
                List<Category> categories = (List<Category>)Application["categories"];
                string categoryId = hdnCategoryId.Value;

                if (string.IsNullOrEmpty(categoryId)) // THÊM MỚI
                {
                    Category newCategory = new Category
                    {
                        Id = categories.Count > 0 ? categories.Max(c => c.Id) + 1 : 1,
                        Name = txtCategoryName.Text,
                        PageData = ddlCategoryPageData.SelectedValue
                    };
                    categories.Add(newCategory);
                }
                else // CẬP NHẬT
                {
                    Category categoryToUpdate = categories.FirstOrDefault(c => c.Id == int.Parse(categoryId));
                    if (categoryToUpdate != null)
                    {
                        categoryToUpdate.Name = txtCategoryName.Text;
                        categoryToUpdate.PageData = ddlCategoryPageData.SelectedValue;
                    }
                }
                Application["categories"] = categories;
                loadCategories();
                ClearCategoryForm();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideModal", "closeModal('categoryModal');", true);
                Response.Redirect("Admin.aspx");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LỖI LƯU CATEGORY: {ex.Message}");
            }
        }

        private void LoadCategoryForEdit(int id)
        {
            List<Category> categories = (List<Category>)Application["categories"];
            Category categoryToEdit = categories.FirstOrDefault(c => c.Id == id);
            if (categoryToEdit != null)
            {
                hdnCategoryId.Value = categoryToEdit.Id.ToString();
                txtCategoryName.Text = categoryToEdit.Name;
                ddlCategoryPageData.SelectedValue = categoryToEdit.PageData;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", "openModal('categoryModal');", true);
                string setTitleScript = "document.querySelector('#categoryModal .modal-title').textContent = 'Cập nhật danh mục';";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SetTitle", setTitleScript, true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPage", "showPage('categories');", true);
            }
        }

        private void DeleteCategory(int id)
        {
            List<Category> categories = (List<Category>)Application["categories"];
            Category categoryToRemove = categories.FirstOrDefault(c => c.Id == id);
            if (categoryToRemove != null)
            {
                categories.Remove(categoryToRemove);
                Application["categories"] = categories;
            }
        }

        private void ClearCategoryForm()
        {
            hdnCategoryId.Value = "";
            txtCategoryName.Text = "";
            ddlCategoryPageData.SelectedIndex = 0;
        }


        // ===================================================================
        // ĐƠN HÀNG (ORDERS)
        // ===================================================================
        private void loadOrders()
        {
            List<Order> orders = (List<Order>)Application["orders"] ?? new List<Order>();
            ordersTableBody.InnerHtml = GenerateOrderHtml(orders);
        }

        private string GenerateOrderHtml(List<Order> orders)
        {
            string html = "";
            foreach (var order in orders)
            {
                html += "<tr>" +
                        $"<td>{order.Id}</td>" +
                        $"<td>{order.CustomerName} (ID: {order.UserId})</td>" +
                        $"<td>{order.TotalAmount:N0} VNĐ</td>" +
                        $"<td><span class='status-{order.Status.ToLower()}'>{order.Status}</span></td>" +
                        $"<td>{order.OrderDate:dd/MM/yyyy HH:mm}</td>" +
                        "<td>" +
                        $"  <a href='Admin.aspx?action=edit_order&id={order.Id}' class='btn btn-small btn-warning'>Sửa TT</a>" +
                        $"  <a href='Admin.aspx?action=delete_order&id={order.Id}' class='btn btn-small btn-danger' onclick='return confirm(\"Bạn có chắc muốn xóa đơn hàng này?\");'>Xóa</a>" +
                        "</td></tr>";
            }
            return html;
        }

        protected void btnSaveOrder_Click(object sender, EventArgs e)
        {
            try
            {
                List<Order> orders = (List<Order>)Application["orders"];
                string orderId = hdnOrderId.Value;

                Order orderToUpdate = orders.FirstOrDefault(o => o.Id == int.Parse(orderId));
                if (orderToUpdate != null)
                {
                    orderToUpdate.Status = ddlOrderStatus.SelectedValue;
                }

                Application["orders"] = orders;
                loadOrders();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideModal", "closeModal('orderModal');", true);
                Response.Redirect("Admin.aspx");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LỖI LƯU ORDER: {ex.Message}");
            }
        }

        private void LoadOrderForEdit(int id)
        {
            List<Order> orders = (List<Order>)Application["orders"];
            Order orderToEdit = orders.FirstOrDefault(o => o.Id == id);
            if (orderToEdit != null)
            {
                hdnOrderId.Value = orderToEdit.Id.ToString();
                lblOrderId.Text = orderToEdit.Id.ToString();
                lblOrderCustomer.Text = orderToEdit.CustomerName;
                ddlOrderStatus.SelectedValue = orderToEdit.Status;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", "openModal('orderModal');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPage", "showPage('orders');", true);
            }
        }

        private void DeleteOrder(int id)
        {
            List<Order> orders = (List<Order>)Application["orders"];
            Order orderToRemove = orders.FirstOrDefault(o => o.Id == id);
            if (orderToRemove != null)
            {
                orders.Remove(orderToRemove);
                Application["orders"] = orders;
            }
        }

        // ===================================================================
        // THANH TOÁN (PAYMENTS) - Read-only
        // ===================================================================
        private void loadPayments()
        {
            List<Payment> payments = (List<Payment>)Application["payments"] ?? new List<Payment>();
            paymentsTableBody.InnerHtml = GeneratePaymentHtml(payments);
        }

        private string GeneratePaymentHtml(List<Payment> payments)
        {
            string html = "";
            foreach (var payment in payments)
            {
                html += "<tr>" +
                        $"<td>{payment.TransactionId}</td>" +
                        $"<td>{payment.OrderId}</td>" +
                        $"<td>{payment.Amount:N0} VNĐ</td>" +
                        $"<td>{payment.Method}</td>" +
                         $"<td><span class='status-{payment.Status.ToLower()}'>{payment.Status}</span></td>" +
                        $"<td>{payment.PaymentDate:dd/MM/yyyy HH:mm}</td>" +
                        "</tr>";
            }
            return html;
        }

        /// <summary>
        /// Xử lý đăng xuất
        /// </summary>
        private void Logout()
        {
            // Xóa cookie "User" bằng cách cho nó hết hạn
            if (Request.Cookies["User"] != null)
            {
                HttpCookie userCookie = new HttpCookie("User");
                userCookie.Expires = DateTime.Now.AddDays(-1d); // Đặt thời gian hết hạn về quá khứ
                Response.Cookies.Add(userCookie);
            }

            // Chuyển hướng về trang Login
            // Bạn cũng có thể đổi "Login.aspx" thành "Index.aspx" nếu muốn
            Response.Redirect("Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

    }
}