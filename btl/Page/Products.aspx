<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="btl.Page.Products" %>

<%@ Register Src="~/UserControl/Header.ascx" TagPrefix="uc" TagName="Header" %>
<%@ Register Src="~/UserControl/Footer.ascx" TagPrefix="uc" TagName="Footer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sản phẩm - Shop Thời Trang ABC</title>
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="~/assets/css/reset.css" />
    <link rel="stylesheet" href="~/assets/css/grid.css" />
    <link rel="stylesheet" href="~/assets/css/common.css" />
    <link rel="stylesheet" href="~/assets/css/products.css" />
    <link rel="stylesheet" href="~/assets/css/icons.css" />
    <style>
        .product-card .add-to-cart-btn {
            position: absolute;
            bottom: 10px;
            right: 10px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 50%;
            width: 35px;
            height: 35px;
            font-size: 18px;
            line-height: 35px;
            text-align: center;
            cursor: pointer;
            opacity: 0;
            transition: opacity 0.3s ease, background-color 0.3s ease;
            padding: 0;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .product-card:hover .add-to-cart-btn {
            opacity: 1;
        }

        .product-card .add-to-cart-btn:hover {
            background-color: #0056b3;
        }

        .product-card .add-to-cart-btn img {
            width: 16px;
            height: 16px;
            vertical-align: middle;
        }

        .product-card {
            position: relative;
        }

            .product-card .product-info {
                padding-bottom: 50px;
            }

        .add-cart-message-popup {
            position: fixed;
            bottom: 20px;
            left: 50%;
            transform: translateX(-50%);
            background-color: rgba(0, 0, 0, 0.7);
            color: white;
            padding: 10px 20px;
            border-radius: 5px;
            z-index: 1000;
            opacity: 0;
            transition: opacity 0.5s ease;
        }

            .add-cart-message-popup.show {
                opacity: 1;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <uc:Header runat="server" ID="header1" />
        <main>
            <div class="page-header">
                <h1>Tất cả sản phẩm</h1>
            </div>

            <div class="container py-5">
                <asp:UpdatePanel ID="UpdatePanelProducts" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="layout-sidebar">
                            <aside class="sidebar">
                                <div class="filter-sidebar">
                                    <div class="filter-widget">
                                        <h3 class="widget-title">Danh mục sản phẩm</h3>
                                        <asp:CheckBoxList ID="cblCategories" runat="server"
                                            CssClass="widget-list checkbox-list"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="Filters_SelectedIndexChanged">
                                        </asp:CheckBoxList>
                                    </div>
                                    <div class="filter-widget">
                                        <h3 class="widget-title">Lọc theo giá</h3>
                                        <asp:CheckBoxList ID="cblPriceRanges" runat="server"
                                            CssClass="widget-list checkbox-list"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="Filters_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Dưới 500.000đ</asp:ListItem>
                                            <asp:ListItem Value="2">500.000đ - 1.000.000đ</asp:ListItem>
                                            <asp:ListItem Value="3">Trên 1.000.000đ</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </aside>

                            <section class="main-content">
                                <div class="grid grid-3">
                                    <asp:Repeater ID="rptProductList" runat="server" OnItemCommand="rptProductList_ItemCommand">
                                        <ItemTemplate>
                                            <div class='grid-item'>
                                                <div class='product-card'>
                                                    <a href='<%# ResolveUrl( Eval("Id", "ProductDetail.aspx?id={0}") ) %>'>
                                                        <img src='<%# ResolveUrl( Eval("ImageUrl") != null ? Eval("ImageUrl").ToString() : "~/assets/img/placeholder.png" ) %>'
                                                            alt='<%# Eval("Name") %>' class='product-image' />
                                                    </a>
                                                    <div class='product-info'>
                                                        <h3 class='product-name'><a href='<%# ResolveUrl( Eval("Id", "ProductDetail.aspx?id={0}") ) %>'><%# Eval("Name") %></a></h3>
                                                        <p class='product-price'><%# Eval("Price", "{0:N0}đ") %></p>
                                                    </div>
                                                    <asp:LinkButton ID="btnAddToCartSmall" runat="server"
                                                        CssClass="add-to-cart-btn"
                                                        CommandName="AddToCart"
                                                        CommandArgument='<%# Eval("Id") %>'
                                                        ToolTip="Thêm vào giỏ">
                                                        <img src='<%= ResolveUrl("~/assets/img/icon-cart.png") %>' alt="+" />
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Literal ID="litNoProducts" runat="server" Visible="false">
                                        <div class='grid-item full-width'><p>Không tìm thấy sản phẩm phù hợp.</p></div>
                                    </asp:Literal>
                                </div>
                                <nav class="pagination-wrapper" aria-label="Page navigation">
                                    <asp:Literal ID="litPagination" runat="server"></asp:Literal>
                                </nav>
                            </section>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cblCategories" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cblPriceRanges" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="rptProductList" EventName="ItemCommand" />
                    </Triggers>
                </asp:UpdatePanel>
                <div id="addCartMessagePopup" class="add-cart-message-popup" role="alert">Sản phẩm đã được thêm vào giỏ!</div>
            </div>
        </main>
        <uc:Footer runat="server" ID="footer1" />
    </form>
    <script src="../assets/js/main.js"></script>
</body>
</html>
