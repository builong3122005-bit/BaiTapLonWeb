<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductDetail.aspx.cs" Inherits="btl.Page.ProductDetail" %>

<%@ Register Src="~/UserControl/Header.ascx" TagPrefix="uc" TagName="Header" %>
<%@ Register Src="~/UserControl/Footer.ascx" TagPrefix="uc" TagName="Footer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Chi tiết sản phẩm - Shop Thời Trang ABC</title>
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="~/assets/css/reset.css" />
    <link rel="stylesheet" href="~/assets/css/grid.css" />
    <link rel="stylesheet" href="~/assets/css/common.css" />
    <link rel="stylesheet" href="~/assets/css/product-detail.css" />
    <link rel="stylesheet" href="~/assets/css/products.css" />
    <link rel="stylesheet" href="~/assets/css/icons.css" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:Header runat="server" ID="header1" />
        <main>
            <div class="container py-5">
                <asp:Literal ID="litErrorMessage" runat="server" Visible="false"></asp:Literal>

                <asp:Panel ID="pnlProductDetail" runat="server">
                    <div class="layout-two-col product-main-layout">
                        <div class="product-gallery-section">
                            <div class="product-gallery">
                                <div class="main-image-container">
                                    <img id="mainImage" runat="server" src="#" alt="Hình ảnh sản phẩm" class="product-main-image" />
                                </div>
                                <div class="thumbnail-container">
                                    <asp:Literal ID="litThumbnails" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>

                        <div class="product-info-section">
                            <div class="product-info-details">
                                <h1 id="productName" runat="server" class="product-title">Tên sản phẩm</h1>
                                <p id="productPrice" runat="server" class="product-price-detail">0đ</p>
                                <p id="productShortDescription" runat="server" class="product-description">
                                    Mô tả ngắn gọn...
                                </p>

                                <div class="product-options">
                                    <div class="option-group">
                                        <label class="option-label">Kích cỡ:</label>
                                        <div class="size-selector">
                                            <asp:RadioButtonList ID="rblSize" runat="server" RepeatDirection="Horizontal" CssClass="size-options">
                                                <asp:ListItem Value="S">S</asp:ListItem>
                                                <asp:ListItem Value="M" Selected="True">M</asp:ListItem>
                                                <asp:ListItem Value="L">L</asp:ListItem>
                                                <asp:ListItem Value="XL">XL</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="option-group">
                                        <label for="<%= quantity.ClientID %>" class="option-label">Số lượng:</label>
                                        <asp:TextBox ID="quantity" runat="server" TextMode="Number" value="1" min="1" CssClass="quantity-input"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:Button ID="btnAddToCart" runat="server" Text="Thêm vào giỏ"
                                    CssClass="btn btn-add-to-cart" OnClick="btnAddToCart_Click" />
                                <asp:Label ID="lblAddToCartMessage" runat="server" CssClass="add-cart-message" EnableViewState="false"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="product-tabs-section">
                        <div class="product-tabs">
                            <div class="tab-header">
                                <button type="button" class="tab-link active" data-tab="descriptionTab">Mô tả chi tiết</button>
                                <button type="button" class="tab-link" data-tab="reviewsTab">Đánh giá</button>
                            </div>
                            <div class="tab-content">
                                <div class="tab-pane active" id="descriptionTab">
                                    <asp:Literal ID="litLongDescription" runat="server"></asp:Literal>
                                </div>
                                <div class="tab-pane" id="reviewsTab">
                                    <p>Hiện chưa có đánh giá nào cho sản phẩm này.</p>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="related-products-section">
                        <h2 class="section-title">Sản phẩm liên quan</h2>
                        <div class="grid grid-4">
                            <asp:Literal ID="litRelatedProducts" runat="server"></asp:Literal>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </main>
        <uc:Footer runat="server" ID="footer1" />
    </form>

    <script src="<%= ResolveUrl("~/assets/js/main.js") %>"></script>
    <script>
        const tabLinks = document.querySelectorAll('.tab-link');
        const tabPanes = document.querySelectorAll('.tab-pane');
        if (tabLinks.length > 0 && tabPanes.length > 0) {
            tabLinks.forEach(link => {
                link.addEventListener('click', () => {
                    const tabId = link.getAttribute('data-tab');
                    tabLinks.forEach(l => l.classList.remove('active'));
                    tabPanes.forEach(p => p.classList.remove('active'));
                    link.classList.add('active');
                    const targetPane = document.getElementById(tabId);
                    if (targetPane) {
                        targetPane.classList.add('active');
                    }
                });
            });
        }

        const thumbnails = document.querySelectorAll('.thumbnail-image');
        const mainImg = document.getElementById('<%= mainImage.ClientID %>');
        if (mainImg && thumbnails.length > 0) {
            thumbnails.forEach(thumb => {
                thumb.addEventListener('click', () => {
                    thumbnails.forEach(t => t.classList.remove('active'));
                    thumb.classList.add('active');
                    mainImg.src = thumb.src;
                });
            });
        }

        const sizeRadios = document.querySelectorAll('#<%= rblSize.ClientID %> input[type=radio]');
        const sizeLabels = document.querySelectorAll('#<%= rblSize.ClientID %> label');
        sizeRadios.forEach((radio, index) => {
            radio.addEventListener('change', () => {
                sizeLabels.forEach(label => label.classList.remove('active'));
                if (radio.checked) {
                    sizeLabels[index].classList.add('active');
                }
            });
            if (radio.checked && sizeLabels[index]) {
                sizeLabels[index].classList.add('active');
            }
        });


    </script>
    <style>
        .size-options label {
            display: inline-block;
            padding: 5px 10px;
            border: 1px solid #ccc;
            margin-right: 5px;
            cursor: pointer;
            border-radius: 4px;
        }


        .size-options input[type=radio] {
            display: none;
        }

        .size-options label.active {
            background-color: #333;
            color: white;
            border-color: #333;
        }
    </style>
    <script src="../assets/js/admin.js"></script>
    <script src="../assets/js/main.js"></script>


</body>
</html>
