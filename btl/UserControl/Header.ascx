<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="btl.UserControl.Header" %>

<header class="header">
    <div class="container">
        <div class="header-wrapper">
            <h1 class="logo"><a href='<%= ResolveUrl("~/Page/Index.aspx") %>'>ABC Shop</a></h1>
            <nav class="navigation">
                <ul class="nav-list" id="navList">
                    <li><a href='<%= ResolveUrl("~/Page/Index.aspx") %>' class="nav-link" data-page="Index">Trang chủ</a></li>
                    <li><a href='<%= ResolveUrl("~/Page/Products.aspx") %>' class="nav-link" data-page="Products">Sản phẩm</a></li>
                    <li><a href='<%= ResolveUrl("~/Page/About.aspx") %>' class="nav-link" data-page="About">Về chúng tôi</a></li>
                    <li><a href='<%= ResolveUrl("~/Page/Contact.aspx") %>' class="nav-link" data-page="Contact">Liên hệ</a></li>
                    <li><a href='<%= ResolveUrl("~/Page/Blog.aspx") %>' class="nav-link" data-page="Blog">Blog</a></li>
                </ul>
            </nav>
            <div class="header-actions">
                <div id="switch_login" runat="server">
                </div>
                <%-- Icon giỏ hàng với số lượng --%>
                <a href='<%= ResolveUrl("~/Page/Cart.aspx") %>' aria-label="Giỏ hàng" class="cart-icon-link">
                    <img src='<%= ResolveUrl("~/assets/img/icon-cart.png") %>' alt="Giỏ hàng" class="icon-img" />
                    <span id="cartItemCount" runat="server" class="cart-item-count" visible="false">0</span>
                </a>
                <%-- Nút menu toggle (ĐỔI TỪ DIV THÀNH BUTTON) --%>
                <button class="menu-toggle" id="menu-toggle" type="button" aria-label="Toggle Menu" aria-expanded="false">
                    <img src='<%= ResolveUrl("~/assets/img/icon-menu.png") %>' alt="Menu" class="icon-img" />
                </button>
            </div>
        </div>
    </div>
</header>

<%-- CSS cho số lượng giỏ hàng --%>
<style>
    .cart-icon-link {
        position: relative;
        display: inline-block;
        margin-right: 15px;
    }

    .cart-item-count {
        position: absolute;
        top: -8px;
        right: -10px;
        background-color: red;
        color: white;
        border-radius: 50%;
        padding: 2px 6px;
        font-size: 10px;
        font-weight: bold;
        line-height: 1;
        min-width: 18px;
        text-align: center;
    }
</style>
