<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlogDetail.aspx.cs" Inherits="btl.Page.BlogDetail" %>

<%@ Register Src="~/UserControl/Header.ascx" TagPrefix="uc" TagName="Header" %>
<%@ Register Src="~/UserControl/Footer.ascx" TagPrefix="uc" TagName="Footer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Chi tiết Blog - Shop Thời Trang ABC</title>
    <link rel="stylesheet" href="~/assets/css/reset.css" />
    <link rel="stylesheet" href="~/assets/css/grid.css" />
    <link rel="stylesheet" href="~/assets/css/common.css" />
    <link rel="stylesheet" href="~/assets/css/blog-details.css" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:Header runat="server" ID="header1" />
        <main>
            <div class="container py-5">
                <div class="blog-details-wrapper">
                    <div class="blog-details-header">
                        <h2 class="blog-title">Xu Hướng Thời Trang Nổi Bật 2025</h2>
                        <p class="blog-meta">Đăng ngày: 21/10/2025 | Tác giả: ABC Shop</p>
                    </div>
                    <div class="blog-details-image">
                        <img src="../assets/img/blog-1.jpg" alt="Xu hướng thời trang 2025" />
                    </div>
                    <div class="blog-details-content">
                        <p>
                            Cùng khám phá những xu hướng thời trang sẽ lên ngôi trong năm tới,
              từ màu sắc, kiểu dáng đến chất liệu. Bài viết này sẽ giúp bạn cập
              nhật những phong cách mới nhất để luôn tự tin và nổi bật.
                        </p>
                        <h3>Màu sắc chủ đạo</h3>
                        <p>
                            Năm 2025, các gam màu trung tính như be, nâu, xanh pastel sẽ được
              ưa chuộng. Sự kết hợp giữa các màu này mang lại vẻ thanh lịch và
              hiện đại.
                        </p>
                        <h3>Kiểu dáng và chất liệu</h3>
                        <p>
                            Trang phục oversized, chất liệu cotton hữu cơ, linen sẽ là lựa
              chọn hàng đầu cho sự thoải mái và bảo vệ môi trường.
                        </p>
                        <h3>Phụ kiện</h3>
                        <p>
                            Túi xách mini, giày sneaker trắng và kính mát bản lớn sẽ là điểm
              nhấn cho set đồ của bạn.
                        </p>
                    </div>
                </div>
            </div>
        </main>
        <uc:Footer runat="server" ID="header2" />
    </form>
    <script src="/assets/js/admin.js"></script>


</body>
</html>
