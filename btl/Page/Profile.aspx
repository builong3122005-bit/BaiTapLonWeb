<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="btl.Page.Profile" %>

<%@ Register Src="~/UserControl/Header.ascx" TagPrefix="uc" TagName="Header" %>
<%@ Register Src="~/UserControl/Footer.ascx" TagPrefix="uc" TagName="Footer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Tài khoản - Shop Thời Trang ABC</title>

    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="~/assets/css/reset.css" />
    <link rel="stylesheet" href="~/assets/css/grid.css" />
    <link rel="stylesheet" href="~/assets/css/common.css" />
    <link rel="stylesheet" href="~/assets/css/products.css" />
    <link rel="stylesheet" href="~/assets/css/icons.css" />
    <link rel="stylesheet" href="~/assets/css/profile.css" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:Header runat="server" ID="header1" />

        <main>
            <div class="page-header">
                <h1>Tài khoản của tôi</h1>
            </div>

            <div class="container py-5">
                <div class="profile-layout">
                    <aside class="profile-sidebar">
                        <nav class="profile-nav" role="tablist" aria-label="Tabs tài khoản">
                            <a href="#info" class="profile-nav-item active" data-tab="info" role="tab" aria-selected="true" aria-controls="info-tab">
                                <span class="nav-icon">👤</span><span>Thông tin cá nhân</span>
                            </a>
                            <a href="#orders" class="profile-nav-item" data-tab="orders" role="tab" aria-selected="false" aria-controls="orders-tab">
                                <span class="nav-icon">📦</span><span>Đơn hàng của tôi</span>
                            </a>
                           <%-- <a href="#addresses" class="profile-nav-item" data-tab="addresses" role="tab" aria-selected="false" aria-controls="addresses-tab">
                                <span class="nav-icon">📍</span><span>Địa chỉ giao hàng</span>
                            </a> --%>
                            <a href="#password" class="profile-nav-item" data-tab="password" role="tab" aria-selected="false" aria-controls="password-tab">
                                <span class="nav-icon">🔒</span><span>Đổi mật khẩu</span>
                            </a>
                            <%-- Dùng LinkButton cho Đăng xuất --%>
                            <asp:LinkButton ID="btnLogout" runat="server" CssClass="profile-nav-item logout" OnClick="btnLogout_Click">
                                <span class="nav-icon">🚪</span><span>Đăng xuất</span>
                            </asp:LinkButton>
                        </nav>
                    </aside>

                    <div class="profile-content">
                        <div class="tab-content active" id="info-tab" role="tabpanel" aria-hidden="false">
                            <div class="profile-card">
                                <div class="profile-header">
                                    <div class="profile-welcome">
                                        <h2>Xin chào, <asp:Literal ID="litWelcomeName" runat="server">Khách</asp:Literal>!</h2>
                                        <p class="profile-subtitle">Quản lý thông tin cá nhân của bạn</p>
                                    </div>
                                </div>

                                <div class="profile-info-section">
                                    <h3 class="section-title-small">Thông tin tài khoản</h3>
                                    <div class="info-grid">
                                        <div class="info-item">
                                            <span class="info-label">Họ và tên</span>
                                            <div class="info-value"><asp:Literal ID="litFullName" runat="server"></asp:Literal></div>
                                        </div>
                                        <div class="info-item">
                                            <span class="info-label">Email</span>
                                            <div class="info-value"><asp:Literal ID="litEmail" runat="server"></asp:Literal></div>
                                        </div>
                                        <%-- Thêm các trường khác nếu có trong Model User --%>
                                        <%--
                                        <div class="info-item">
                                            <span class="info-label">Số điện thoại</span>
                                            <div class="info-value"><asp:Literal ID="litPhone" runat="server"></asp:Literal></div>
                                        </div>
                                         <div class="info-item full-width">
                                            <span class="info-label">Địa chỉ</span>
                                            <div class="info-value"><asp:Literal ID="litAddress" runat="server"></asp:Literal></div>
                                        </div>
                                        --%>
                                    </div>
                                    <div class="profile-actions">
                                        <%-- Nút chỉnh sửa (cần trang/modal riêng) --%>
                                        <asp:Button ID="btnEditInfo" runat="server" Text="Chỉnh sửa thông tin" CssClass="btn btn-edit" OnClientClick="alert('Chức năng đang phát triển!'); return false;" />
                                    </div>
                                </div>

                                <div class="profile-stats">
                                    <h3 class="section-title-small">Thống kê đơn hàng</h3>
                                     <div class="stats-grid">
                                         <div class="stat-card">
                                             <div class="stat-icon">📦</div>
                                             <div class="stat-info">
                                                 <div class="stat-number"><asp:Literal ID="litTotalOrders" runat="server">0</asp:Literal></div>
                                                 <div class="stat-label">Tổng đơn</div>
                                             </div>
                                         </div>
                                          <div class="stat-card">
                                             <div class="stat-icon">💰</div>
                                             <div class="stat-info">
                                                 <div class="stat-number"><asp:Literal ID="litTotalSpent" runat="server">0đ</asp:Literal></div>
                                                 <div class="stat-label">Tổng chi tiêu</div>
                                             </div>
                                         </div>
                                          <%-- Có thể thêm các thống kê khác --%>
                                     </div>
                                </div>

                            </div>
                        </div>

                        <div class="tab-content" id="orders-tab" role="tabpanel" aria-hidden="true">
                            <div class="profile-card">
                                <div class="profile-header">
                                    <div class="profile-welcome">
                                        <h2>Đơn hàng của tôi</h2>
                                        <p class="profile-subtitle">Quản lý và theo dõi đơn hàng của bạn</p>
                                    </div>
                                </div>
                                <div class="profile-info-section">
                                    <%-- Dùng Repeater để hiển thị danh sách đơn hàng --%>
                                    <asp:Repeater ID="rptOrders" runat="server">
                                        <ItemTemplate>
                                             <div class="order-card">
                                                <div class="order-header">
                                                    <div>
                                                        <div class="order-id">Đơn hàng #<%# Eval("Id") %></div>
                                                        <div class="order-item-meta">Ngày đặt: <%# Eval("OrderDate", "{0:dd/MM/yyyy HH:mm}") %></div>
                                                    </div>
                                                    <%-- Hiển thị trạng thái với class CSS tương ứng --%>
                                                    <span class='order-status <%# Eval("Status").ToString().ToLower() %>'><%# Eval("Status") %></span>
                                                </div>

                                                <%-- Có thể thêm chi tiết sản phẩm trong đơn hàng nếu muốn (cần truy vấn thêm) --%>

                                                <div class="order-footer">
                                                    <div>
                                                        <span>Tổng tiền: </span>
                                                        <span class="order-total"><%# Eval("TotalAmount", "{0:N0}đ") %></span>
                                                    </div>
                                                    <div class="address-actions">
                                                         <%-- Nút Chi tiết đơn hàng (cần trang riêng) --%>
                                                        <asp:HyperLink NavigateUrl='<%# Eval("Id", "OrderDetail.aspx?id={0}") %>' Text="Xem chi tiết" CssClass="btn btn-small btn-secondary" runat="server" />
                                                         <%-- Nút Mua lại/Hủy đơn tùy trạng thái (logic phức tạp hơn) --%>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate> <%-- Style khác cho hàng xen kẽ nếu muốn --%>
                                             <div class="order-card alt-style"> <%-- Thêm class alt-style --%>
                                                <div class="order-header">
                                                    <div>
                                                        <div class="order-id">Đơn hàng #<%# Eval("Id") %></div>
                                                        <div class="order-item-meta">Ngày đặt: <%# Eval("OrderDate", "{0:dd/MM/yyyy HH:mm}") %></div>
                                                    </div>
                                                    <span class='order-status <%# Eval("Status").ToString().ToLower() %>'><%# Eval("Status") %></span>
                                                </div>
                                                <div class="order-footer">
                                                    <div>
                                                        <span>Tổng tiền: </span>
                                                        <span class="order-total"><%# Eval("TotalAmount", "{0:N0}đ") %></span>
                                                    </div>
                                                    <div class="address-actions">
                                                        <asp:HyperLink NavigateUrl='<%# Eval("Id", "OrderDetail.aspx?id={0}") %>' Text="Xem chi tiết" CssClass="btn btn-small btn-secondary" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </AlternatingItemTemplate>
                                        <FooterTemplate>
                                            <%-- Hiển thị nếu không có đơn hàng --%>
                                            <asp:Panel ID="pnlNoOrders" runat="server" Visible='<%# !((Repeater)Container.Parent).Items.OfType<RepeaterItem>().Any() %>'>
                                                <p>Bạn chưa có đơn hàng nào.</p>
                                            </asp:Panel>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>

                       <%-- <div class="tab-content" id="addresses-tab" role="tabpanel" aria-hidden="true">
                           Nội dung tab Địa chỉ (Tĩnh hoặc động)
                        </div> --%>

                        <div class="tab-content" id="password-tab" role="tabpanel" aria-hidden="true">
                            <div class="profile-card">
                                <div class="profile-header">
                                    <div class="profile-welcome">
                                        <h2>Đổi mật khẩu</h2>
                                        <p class="profile-subtitle">Cập nhật mật khẩu để bảo mật tài khoản</p>
                                    </div>
                                </div>
                                <div class="profile-info-section">
                                    <div class="password-form">
                                        <div class="form-group">
                                            <label for="<%= txtCurrentPassword.ClientID %>">Mật khẩu hiện tại</label>
                                            <asp:TextBox ID="txtCurrentPassword" runat="server" TextMode="Password" required="required"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="rfvCurrentPassword" runat="server" ControlToValidate="txtCurrentPassword"
                                                ErrorMessage="Vui lòng nhập mật khẩu hiện tại" CssClass="error-message" Display="Dynamic" ValidationGroup="ChangePassword">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <label for="<%= txtNewPassword.ClientID %>">Mật khẩu mới</label>
                                            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" required="required"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword"
                                                ErrorMessage="Vui lòng nhập mật khẩu mới" CssClass="error-message" Display="Dynamic" ValidationGroup="ChangePassword">*</asp:RequiredFieldValidator>
                                             <asp:RegularExpressionValidator ID="revNewPassword" runat="server" ControlToValidate="txtNewPassword"
                                                 ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"
                                                 ErrorMessage="Mật khẩu ít nhất 8 ký tự, gồm chữ hoa, thường, số" CssClass="error-message" Display="Dynamic" ValidationGroup="ChangePassword">*</asp:RegularExpressionValidator>
                                            <small style="color: #777; margin-top: 5px; display: block;">Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường và số</small>
                                        </div>
                                        <div class="form-group">
                                            <label for="<%= txtConfirmPassword.ClientID %>">Xác nhận mật khẩu mới</label>
                                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" required="required"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                                                ErrorMessage="Vui lòng xác nhận mật khẩu" CssClass="error-message" Display="Dynamic" ValidationGroup="ChangePassword">*</asp:RequiredFieldValidator>
                                             <asp:CompareValidator ID="cvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" ControlToCompare="txtNewPassword"
                                                 Operator="Equal" ErrorMessage="Mật khẩu xác nhận không khớp" CssClass="error-message" Display="Dynamic" ValidationGroup="ChangePassword">*</asp:CompareValidator>
                                        </div>
                                        <div class="profile-actions" style="margin-top: 30px;">
                                            <asp:Button ID="btnChangePassword" runat="server" Text="Cập nhật mật khẩu" CssClass="btn" OnClick="btnChangePassword_Click" ValidationGroup="ChangePassword" />
                                             <asp:Label ID="lblPasswordMessage" runat="server" CssClass="password-message" EnableViewState="false"></asp:Label>
                                             <asp:ValidationSummary ID="ValidationSummaryPassword" runat="server" CssClass="error-summary" HeaderText="Lỗi:" ValidationGroup="ChangePassword" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </main>

        <uc:Footer runat="server" ID="footer1" />
    </form>

    <script src="<%= ResolveUrl("~/assets/js/main.js") %>"></script>
    <script src="<%= ResolveUrl("~/assets/js/profile.js") %>" type="module"></script>
</body>
</html>