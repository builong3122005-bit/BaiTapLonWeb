<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="btl.UserControl.Header" %>

<header class="header">
    <div class="container">
        <div class="header-wrapper">
            <h1 class="logo"><a href='<%= ResolveUrl("~/Page/Index.aspx") %>'>ABC Shop</a></h1>
            <nav class="navigation">
                <ul class="nav-list">
                    <li><a href='<%= ResolveUrl("~/Page/Index.aspx") %>' class="nav-link">Trang chủ</a></li>
                    <li><a href='<%= ResolveUrl("~/Page/Products.aspx") %>' class="nav-link">Sản phẩm</a></li>
                    <li><a href='<%= ResolveUrl("~/Page/About.aspx") %>' class="nav-link">Về chúng tôi</a></li>
                    <li><a href='<%= ResolveUrl("~/Page/Contact.aspx") %>' class="nav-link">Liên hệ</a></li>
                    <li><a href='<%= ResolveUrl("~/Page/Blog.aspx") %>' class="nav-link">Blog</a></li>
                </ul>
            </nav>
            <div class="header-actions">
                <div id="switch_login" runat="server">
                </div>
                <%-- Bọc icon và số lượng trong div để dễ style --%>
                <a href='<%= ResolveUrl("~/Page/Cart.aspx") %>' aria-label="Giỏ hàng" class="cart-icon-link">
                    <img src='<%= ResolveUrl("~/assets/img/icon-cart.png") %>' alt="Giỏ hàng" class="icon-img" />
                    <%-- Thêm span để hiển thị số lượng --%>
                    <span id="cartItemCount" runat="server" class="cart-item-count" visible="false">0</span>
                </a>
                <div class="menu-toggle" id="menu-toggle">
                    <img src='<%= ResolveUrl("~/assets/img/icon-menu.png") %>' alt="Menu" class="icon-img" />
                </div>
            </div>
        </div>
    </div>
</header>
<%-- Thêm CSS cho số lượng --%>
<style>
.cart-icon-link {
    position: relative; /* Để định vị số lượng */
    display: inline-block; /* Hoặc display: block tùy layout */
     margin-right: 15px; /* Khoảng cách với icon user/login */
}

.cart-item-count {
    position: absolute;
    top: -8px; /* Điều chỉnh vị trí */
    right: -10px; /* Điều chỉnh vị trí */
    background-color: red;
    color: white;
    border-radius: 50%;
    padding: 2px 6px;
    font-size: 10px; /* Kích thước chữ nhỏ */
    font-weight: bold;
    line-height: 1; /* Căn giữa số */
    min-width: 18px; /* Đảm bảo hình tròn */
    text-align: center;
}
</style>