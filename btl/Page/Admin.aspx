<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="btl.Page.Admin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Quản Lý - ABC Shop</title>
    <link
        href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="/assets/css/reset.css" />
    <link rel="stylesheet" href="/assets/css/admin.css" />
</head>
<body>
    <form id="form1" runat="server" novalidate="novalidate">
        <button class="menu-toggle" id="menuToggle">☰ Menu</button>
        <div class="admin-layout">
            <aside class="sidebar" id="sidebar">
                <div class="sidebar-header">
                    <h2>ABC Shop Admin</h2>
                </div>
                <nav class="nav-menu">
                    <ul>
                        <li class="nav-item">
                            <a href="#" class="nav-link" data-page="products-tshirt"><span class="nav-icon">👕</span><span>Áo Thun & T-shirt</span></a>
                        </li>
                        <li class="nav-item">
                            <a href="#" class="nav-link" data-page="products-somi"><span class="nav-icon">👔</span><span>Áo Sơ Mi</span></a>
                        </li>
                        <li class="nav-item">
                            <a href="#" class="nav-link" data-page="products-khoac"><span class="nav-icon">🧥</span><span>Áo Khoác</span></a>
                        </li>
                        <li class="nav-item">
                            <a href="#" class="nav-link" data-page="products-jeans"><span class="nav-icon">👖</span><span>Quần Jeans</span></a>
                        </li>
                        <li class="nav-item">
                            <a href="#" class="nav-link" data-page="products-shorts"><span class="nav-icon">🩳</span><span>Quần Shorts</span></a>
                        </li>
                        <li class="nav-item">
                            <a href="#" class="nav-link" data-page="categories"><span class="nav-icon">📑</span><span>Danh mục</span></a>
                        </li>
                        <li class="nav-item">
                            <a href="#" class="nav-link" data-page="orders"><span class="nav-icon">🛒</span><span>Đơn hàng</span></a>
                        </li>
                        <li class="nav-item">
                            <a href="#" class="nav-link" data-page="payment"><span class="nav-icon">💳</span><span>Quản lý thanh toán</span></a>
                        </li>
                        <li class="nav-item">
                            <a href="#" class="nav-link" data-page="customers"><span class="nav-icon">👥</span><span>Tài khoản khách hàng</span></a>
                        </li>
                        <li class="nav-item">
                            <a href="#" class="nav-link" onclick="window.location.href='Admin.aspx?action=logout'; return false;">
                                <span class="nav-icon">🚪</span><span>Đăng xuất</span>
                            </a>
                        </li>
                    </ul>
                </nav>
            </aside>

            <main class="main-content">
                <div id="dashboard-page" class="page-content">
                </div>

                <div id="products-tshirt-page" class="page-content hidden">
                    <div class="page-header">
                        <h1 class="page-title">Quản lý Áo Thun & T-shirt</h1>
                    </div>
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Danh sách sản phẩm</h3>
                            <button
                                type="button"
                                class="btn btn-primary"
                                onclick="showAddProductModal()">
                                + Thêm sản phẩm
                            </button>
                        </div>
                        <div class="table-responsive">
                            <table>
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Ảnh</th>
                                        <th>Tên sản phẩm</th>
                                        <th>Giá</th>
                                        <th>Tồn kho</th>
                                        <th>Thao tác</th>
                                    </tr>
                                </thead>
                                <tbody id="productsTshirtTableBody" runat="server"></tbody>
                            </table>

                        </div>
                    </div>
                </div>

                <div id="products-somi-page" class="page-content hidden">
                    <div class="page-header">
                        <h1 class="page-title">Quản lý Áo Sơ Mi</h1>
                    </div>
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Danh sách sản phẩm</h3>
                            <button type="button" class="btn btn-primary" onclick="showAddProductModal()">
                                + Thêm sản phẩm
                            </button>
                        </div>
                        <div class="table-responsive">
                            <table>
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Ảnh</th>
                                        <th>Tên sản phẩm</th>
                                        <th>Giá</th>
                                        <th>Tồn kho</th>
                                        <th>Thao tác</th>
                                    </tr>
                                </thead>
                                <tbody id="productsSomiTableBody" runat="server"></tbody>
                            </table>
                        </div>
                    </div>
                </div>

                <div id="products-khoac-page" class="page-content hidden">
                    <div class="page-header">
                        <h1 class="page-title">Quản lý Áo Khoác</h1>
                    </div>
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Danh sách sản phẩm</h3>
                            <button type="button" class="btn btn-primary" onclick="showAddProductModal()">
                                + Thêm sản phẩm
                            </button>
                        </div>
                        <div class="table-responsive">
                            <table>
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Ảnh</th>
                                        <th>Tên sản phẩm</th>
                                        <th>Giá</th>
                                        <th>Tồn kho</th>
                                        <th>Thao tác</th>

                                    </tr>
                                </thead>
                                <tbody id="productsKhoacTableBody" runat="server"></tbody>
                            </table>
                        </div>
                    </div>
                </div>

                <div id="products-jeans-page" class="page-content hidden">
                    <div class="page-header">
                        <h1 class="page-title">Quản lý Quần Jeans</h1>
                    </div>
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Danh sách sản phẩm</h3>
                            <button type="button" class="btn btn-primary" onclick="showAddProductModal()">
                                + Thêm sản phẩm
                            </button>
                        </div>
                        <div class="table-responsive">
                            <table>
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Ảnh</th>
                                        <th>Tên sản phẩm</th>
                                        <th>Giá</th>
                                        <th>Tồn kho</th>
                                        <th>Thao tác</th>
                                    </tr>
                                </thead>
                                <tbody id="productsJeansTableBody" runat="server"></tbody>
                            </table>
                        </div>
                    </div>
                </div>


                <div id="products-shorts-page" class="page-content hidden">
                    <div class="page-header">
                        <h1 class="page-title">Quản lý Quần Shorts</h1>
                    </div>
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Danh sách sản phẩm</h3>
                            <button type="button" class="btn btn-primary" onclick="showAddProductModal()">
                                + Thêm sản phẩm
                     
                            </button>
                        </div>
                        <div class="table-responsive">
                            <table>
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Ảnh</th>
                                        <th>Tên sản phẩm</th>
                                        <th>Giá</th>
                                        <th>Tồn kho</th>
                                        <th>Thao tác</th>
                                    </tr>
                                </thead>
                                <tbody id="productsShortsTableBody" runat="server"></tbody>
                            </table>
                        </div>
                    </div>
                </div>

                <div id="categories-page" class="page-content hidden">
                    <div class="page-header">
                        <h1 class="page-title">Quản lý Danh mục</h1>
                    </div>
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Danh sách danh mục</h3>
                            <button
                                type="button"
                                class="btn btn-primary"
                                onclick="showAddCategoryModal()">
                                + Thêm danh mục
                            </button>
                        </div>
                        <div class="table-responsive">
                            <table>
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Tên danh mục</th>
                                        <th>Mã Trang (PageData)</th>
                                        <th>Số sản phẩm</th>
                                        <th>Thao tác</th>
                                    </tr>
                                </thead>
                                <tbody id="categoriesTableBody" runat="server"></tbody>
                            </table>
                        </div>
                    </div>
                </div>

                <div id="orders-page" class="page-content hidden">
                    <div class="page-header">
                        <h1 class="page-title">Quản lý Đơn hàng</h1>
                    </div>
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Danh sách đơn hàng</h3>
                        </div>
                        <div class="table-responsive">
                            <table>
                                <thead>
                                    <tr>
                                        <th>Mã đơn</th>
                                        <th>Khách hàng</th>
                                        <th>Tổng tiền</th>
                                        <th>Trạng thái</th>
                                        <th>Ngày đặt</th>
                                        <th>Thao tác</th>
                                    </tr>
                                </thead>
                                <tbody id="ordersTableBody" runat="server"></tbody>
                            </table>
                        </div>
                    </div>
                </div>

                <div id="payment-page" class="page-content hidden">
                    <div class="page-header">
                        <h1 class="page-title">Quản lý Thanh toán</h1>
                    </div>
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Lịch sử giao dịch</h3>
                        </div>
                        <div class="table-responsive">
                            <table>
                                <thead>
                                    <tr>
                                        <th>Mã Giao dịch</th>
                                        <th>Mã Đơn hàng</th>
                                        <th>Số tiền</th>
                                        <th>Phương thức</th>
                                        <th>Trạng thái</th>
                                        <th>Ngày Giao dịch</th>
                                    </tr>
                                </thead>
                                <tbody id="paymentsTableBody" runat="server"></tbody>
                            </table>
                        </div>
                    </div>
                </div>

                <div id="customers-page" class="page-content hidden">
                    <div class="page-header">
                        <h1 class="page-title">Quản lý Khách hàng</h1>
                    </div>
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Danh sách khách hàng</h3>
                            <button
                                type="button"
                                class="btn btn-primary"
                                onclick="showAddCustomerModal()">
                                + Thêm khách hàng
                            </button>
                        </div>
                        <div class="table-responsive">
                            <table>
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Họ tên</th>
                                        <!-- ✅ MỚI -->
                                        <!-- <th>Tên hiển thị </th> -->
                                        <th>Email</th>
                                        <!-- ✅ MỚI -->
                                        <!--  <th>Số Điện thoại</th> -->
                                        <th>Tổng đơn</th>
                                        <th>Thao tác</th>
                                    </tr>
                                </thead>
                                <tbody id="customersTableBody" runat="server"></tbody>
                            </table>
                        </div>
                    </div>
                </div>

            </main>
        </div>

        <div id="productModal" class="modal">
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="modal-title">Thêm sản phẩm mới</h3>
                    <button type="button" class="close-btn" onclick="closeModal('productModal')">
                        &times;
                    </button>
                </div>
                <asp:HiddenField ID="hdnProductId" runat="server" Value="" />
                <div class="form-group">
                    <label>Tên sản phẩm *</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Danh mục *</label>
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" required="required">
                        <asp:ListItem Value="">Chọn danh mục</asp:ListItem>

                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Giá (VNĐ) *</label>
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" TextMode="Number" required="required"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Tồn kho *</label>
                    <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" TextMode="Number" required="required"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Ảnh sản phẩm</label>
                    <asp:FileUpload ID="fileUploadImage" runat="server" CssClass="form-control" accept="image/*" />
                    <small>Bỏ trống nếu không muốn thay đổi ảnh khi cập nhật.</small>
                </div>
                <asp:Button ID="btnSaveProduct" runat="server" Text="Lưu sản phẩm"
                    CssClass="btn btn-primary" OnClick="btnSaveProduct_Click" />
            </div>
        </div>

        <div id="categoryModal" class="modal">
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="modal-title">Thêm danh mục mới</h3>
                    <button type="button" class="close-btn" onclick="closeModal('categoryModal')">
                        &times;
                    </button>
                </div>
                <asp:HiddenField ID="hdnCategoryId" runat="server" Value="" />
                <div class="form-group">
                    <label>Tên danh mục *</label>
                    <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Mã Trang (PageData) *</label>
                    <asp:DropDownList ID="ddlCategoryPageData" runat="server" CssClass="form-control" required="required">
                        <asp:ListItem Value="">Chọn mã trang</asp:ListItem>
                        <asp:ListItem Value="products-tshirt">products-tshirt</asp:ListItem>
                        <asp:ListItem Value="products-somi">products-somi</asp:ListItem>
                        <asp:ListItem Value="products-khoac">products-khoac</asp:ListItem>
                        <asp:ListItem Value="products-jeans">products-jeans</asp:ListItem>
                        <asp:ListItem Value="products-shorts">products-shorts</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <asp:Button ID="btnSaveCategory" runat="server" Text="Lưu danh mục"
                    CssClass="btn btn-primary" OnClick="btnSaveCategory_Click" />

            </div>
        </div>

        <div id="customerModal" class="modal">
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="modal-title">Thêm khách hàng mới</h3>
                    <button type="button" class="close-btn" onclick="closeModal('customerModal')">
                        &times;
                    </button>
                </div>
                <asp:HiddenField ID="hdnCustomerId" runat="server" Value="" />
                <div class="form-group">
                    <label>Họ tên *</label>
                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>

                <!-- ✅ MỚI: Tên hiển thị -->
                <!-- <div class="form-group">
                    <label>Tên hiển thị</label>
                    <asp:TextBox ID="txtCustomerDisplayName" runat="server" CssClass="form-control"></asp:TextBox>
                    <small style="color: #777; display: block; margin-top: 5px;">Để trống sẽ dùng họ tên</small>
                </div> -->

                <div class="form-group">
                    <label>Email *</label>
                    <asp:TextBox ID="txtCustomerEmail" runat="server" CssClass="form-control" TextMode="Email" required="required"></asp:TextBox>
                </div>

                <!-- ✅ MỚI: Số điện thoại -->
                <!--   <div class="form-group">
                    <label>Số điện thoại *</label>
                    <asp:TextBox ID="txtCustomerPhone" runat="server" CssClass="form-control" TextMode="Phone" required="required"></asp:TextBox>
                </div> 
            </div>-->



                <div class="form-group">
                    <label>Mật khẩu *</label>
                    <asp:TextBox ID="txtCustomerPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    <small>Bỏ trống nếu không muốn thay đổi mật khẩu khi cập nhật.</small>
                </div>
                <div class="form-group">
                    <label>Quyền *</label>
                    <asp:DropDownList ID="ddlCustomerRole" runat="server" CssClass="form-control" required="required">
                        <asp:ListItem Value="USER">USER</asp:ListItem>
                        <asp:ListItem Value="ADMIN">ADMIN</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:Button ID="btnSaveCustomer" runat="server" Text="Lưu khách hàng"
                    CssClass="btn btn-primary" OnClick="btnSaveCustomer_Click" />
            </div>
        </div>

        <div id="orderModal" class="modal">
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="modal-title">Cập nhật trạng thái đơn hàng</h3>
                    <button type="button" class="close-btn" onclick="closeModal('orderModal')">
                        &times;
                    </button>
                </div>
                <asp:HiddenField ID="hdnOrderId" runat="server" Value="" />
                <div class="form-group">
                    <label>Mã đơn hàng: </label>
                    <asp:Label ID="lblOrderId" runat="server" Text=""></asp:Label>
                </div>
                <div class="form-group">
                    <label>Khách hàng: </label>
                    <asp:Label ID="lblOrderCustomer" runat="server" Text=""></asp:Label>
                </div>
                <div class="form-group">
                    <label>Trạng thái *</label>
                    <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="form-control" required="required">
                        <asp:ListItem Value="Pending">Pending</asp:ListItem>
                        <asp:ListItem Value="Processing">Processing</asp:ListItem>
                        <asp:ListItem Value="Shipped">Shipped</asp:ListItem>
                        <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:Button ID="btnSaveOrder" runat="server" Text="Cập nhật đơn hàng"
                    CssClass="btn btn-primary" OnClick="btnSaveOrder_Click" />
            </div>
        </div>

        <script src="/assets/js/admin.js"></script>
    </form>
    <script src="../assets/js/main.js"></script>

</body>
</html>
