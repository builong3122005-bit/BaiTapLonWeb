<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="btl.Page.Cart" %>

<%@ Register Src="~/UserControl/Header.ascx" TagPrefix="uc" TagName="Header" %>
<%@ Register Src="~/UserControl/Footer.ascx" TagPrefix="uc" TagName="Footer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Giỏ hàng - Shop ABC</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="~/assets/css/reset.css" />
    <link rel="stylesheet" href="~/assets/css/grid.css" />
    <link rel="stylesheet" href="~/assets/css/common.css" />
    <link rel="stylesheet" href="~/assets/css/cart.css" />
    <link rel="stylesheet" href="~/assets/css/icons.css" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:Header runat="server" ID="header1" />
        <main>
            <div class="page-header">
                <h1>Giỏ hàng của bạn</h1>
            </div>

            <div class="container py-5">
                <asp:Panel ID="pnlCartEmpty" runat="server" Visible="false" CssClass="cart-empty-message">
                    <p>Giỏ hàng của bạn đang trống.</p>
                    <asp:HyperLink NavigateUrl="~/Page/Products.aspx" Text="Tiếp tục mua sắm" CssClass="btn btn-primary" runat="server" />
                </asp:Panel>

                <asp:Panel ID="pnlCartView" runat="server">
                    <div class="layout-70-30">
                        <div class="cart-items-section">
                            <div class="cart-items-wrapper">
                                <%-- Dùng Repeater để hiển thị cart items --%>
                                <asp:Repeater ID="rptCartItems" runat="server" OnItemCommand="rptCartItems_ItemCommand">
                                    <ItemTemplate>
                                        <div class="cart-item">
                                            <div class="cart-item-layout">
                                                <div class="cart-item-image-wrapper">
  <img src='<%# ResolveUrl( Eval("ImageUrl") != null ? Eval("ImageUrl").ToString() : "~/Uploads/Products/default.jpg" ) %>'
          alt='<%# Eval("ProductName") %>'
          class="cart-item-image" />
                                                </div>
                                                <div class="cart-item-details">
                                                    <h3 class="cart-item-name"><%# Eval("ProductName") %></h3>
                                                    <p class="cart-item-meta">Size: <%# Eval("Size") %></p>
                                                </div>
                                                <div class="cart-item-price-wrapper">
                                                    <p class="cart-item-price"><%# Eval("Price", "{0:N0}đ") %></p>
                                                </div>
                                                <div class="cart-item-quantity-wrapper">
                                                    <%-- Input số lượng --%>
                                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="quantity-input" TextMode="Number"
                                                        Text='<%# Eval("Quantity") %>' Width="60px" min="1"></asp:TextBox>
                                                    <%-- Nút cập nhật số lượng --%>
                                                    <asp:Button ID="btnUpdateQuantity" runat="server" Text="Cập nhật" CssClass="btn btn-sm btn-update"
                                                        CommandName="UpdateQuantity" CommandArgument='<%# Eval("ProductId") + "," + Eval("Size") %>' />
                                                </div>
                                                <div class="cart-item-remove-wrapper">
                                                    <%-- Nút xóa item --%>
                                                    <asp:LinkButton ID="btnRemoveItem" runat="server" CssClass="remove-item-btn" aria-label="Xóa sản phẩm"
                                                        CommandName="RemoveItem" CommandArgument='<%# Eval("ProductId") + "," + Eval("Size") %>'>
                                                         <img src='<%= ResolveUrl("~/assets/img/delete.png") %>' alt="Xóa" class="icon-img delete" />
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                             <div class="cart-actions">
                                <asp:HyperLink NavigateUrl="~/Page/Products.aspx" CssClass="btn btn-outline" runat="server">
                                     <img src='<%= ResolveUrl("~/assets/icons/arrow-left.png") %>' alt="" class="icon-img me-2" />Tiếp tục mua sắm
                                </asp:HyperLink>
                            </div>
                        </div>

                        <div class="cart-summary-section">
                            <div class="cart-summary">
                                <h3>Tóm tắt đơn hàng</h3>
                                <div class="summary-line">
                                    <span>Tạm tính</span>
                                    <asp:Label ID="lblSubtotal" runat="server" Text="0đ"></asp:Label>
                                </div>
                                <div class="summary-line">
                                    <span>Phí giao hàng</span>
                                    <span>Miễn phí</span> <%-- Hoặc tính toán phí ship --%>
                                </div>
                                <div class="summary-line total">
                                    <span>Tổng cộng</span>
                                    <asp:Label ID="lblTotal" runat="server" Text="0đ"></asp:Label>
                                </div>
                                <asp:HyperLink ID="lnkCheckout" NavigateUrl="~/Page/Checkout.aspx" CssClass="btn btn-checkout" runat="server">Tiến hành thanh toán</asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </main>
        <uc:Footer runat="server" ID="footer1" />
    </form>
</body>
</html>