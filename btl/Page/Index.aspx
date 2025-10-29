<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="btl.Page.Index" %>

<%@ Register Src="~/UserControl/Header.ascx" TagPrefix="uc" TagName="Header" %>
<%@ Register Src="~/UserControl/Footer.ascx" TagPrefix="uc" TagName="Footer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Shop Thời Trang ABC - Trang chủ</title>

    <link
        href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700&display=swap"
        rel="stylesheet" />

    <link rel="stylesheet" href="~/assets/css/reset.css" />
    <link rel="stylesheet" href="~/assets/css/grid.css" />
    <link rel="stylesheet" href="~/assets/css/common.css" />
    <link rel="stylesheet" href="~/assets/css/home.css" />
    <link rel="stylesheet" href="~/assets/css/products.css" />
    <link rel="stylesheet" href="~/assets/css/icons.css" />
</head>
<body>

    <form id="form1" runat="server">
        <uc:Header runat="server" ID="header1" />
        <div>
            <main>
                <section class="hero">
                    <h1>Bộ Sưu Tập Mùa Thu 2025</h1>
                    <p>
                        Khám phá những thiết kế mới nhất, mang đậm phong cách và sự thoải mái.
                    </p>
                    <a href="./products.aspx" class="btn">Mua Ngay</a>
                </section>

                <section class="py-5">
                    <div class="container">
                        <h2 class="section-title">Sản Phẩm Nổi Bật</h2>
                        <div class="grid grid-4">
                            <asp:Literal ID="litFeaturedProducts" runat="server"></asp:Literal>
                        </div>
                    </div>
                </section>

                <section class="promo-banner">
                    <div class="container">
                        <h2>Giảm giá cuối mùa</h2>
                        <p>Ưu đãi lên đến 50% cho các sản phẩm chọn lọc. Đừng bỏ lỡ!</p>
                        <a href="./products.aspx" class="btn">Xem Ngay</a>
                    </div>
                </section>
            </main>
        </div>
        <uc:Footer runat="server" ID="footer" />
    </form>
    <script src="../assets/js/main.js"></script>
</body>
</html>
