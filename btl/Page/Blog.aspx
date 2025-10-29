<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Blog.aspx.cs" Inherits="btl.Page.Blog" %>

<%@ Register Src="~/UserControl/Header.ascx" TagPrefix="uc" TagName="Header" %>
<%@ Register Src="~/UserControl/Footer.ascx" TagPrefix="uc" TagName="Footer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Blog - Shop ABC</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link
        href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="~/assets/css/reset.css" />
    <link rel="stylesheet" href="~/assets/css/grid.css" />
    <link rel="stylesheet" href="~/assets/css/common.css" />
    <link rel="stylesheet" href="~/assets/css/static-page.css" />
    <link rel="stylesheet" href="~/assets/css/products.css" />
    <link rel="stylesheet" href="~/assets/css/icons.css" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:Header runat="server" ID="header1" />
        <main>
            <div class="page-header">
                <h1>Tin tức & Bài viết</h1>
            </div>
            <div class="container py-5">
                <div class="grid grid-3">
                    <div class="grid-item">
                        <div class="blog-post-card">
                            <a href="./BlogDetail.aspx">
                                <img src="../assets/img/blog-1.jpg" alt="Xu hướng thời trang 2025" />
                            </a>
                            <div class="blog-post-content">
                                <h3><a href="#">Xu Hướng Thời Trang Nổi Bật 2025</a></h3>
                                <p>
                                    Cùng khám phá những xu hướng thời trang sẽ lên ngôi trong năm
                  tới, từ màu sắc, kiểu dáng đến chất liệu.
                                </p>
                                <a href="./BlogDetail.aspx" class="btn-read-more">Đọc thêm</a>
                            </div>
                        </div>
                    </div>
                    <div class="grid-item">
                        <div class="blog-post-card">
                            <a href="./BlogDetail.aspx">
                                <img src="../assets/img/blog-2.jpg" alt="Cách phối đồ" />
                            </a>
                            <div class="blog-post-content">
                                <h3><a href="#">5 Cách Phối Đồ Sành Điệu Với Áo Thun</a></h3>
                                <p>
                                    Áo thun là một item không thể thiếu. Học ngay 5 cách phối đồ
                  để biến chiếc áo thun đơn giản trở nên thật phong cách.
                                </p>
                                <a href="./BlogDetail.aspx" class="btn-read-more">Đọc thêm</a>
                            </div>
                        </div>
                    </div>
                    <div class="grid-item">
                        <div class="blog-post-card">
                            <a href="./BlogDetail.aspx">
                                <img src="../assets/img/blog-3.jpg" alt="Bảo quản quần áo" />
                            </a>
                            <div class="blog-post-content">
                                <h3><a href="#">Mẹo Bảo Quản Quần Áo Bền Đẹp Như Mới</a></h3>
                                <p>
                                    Những mẹo nhỏ giúp bạn giữ gìn quần áo yêu thích luôn mới và
                  bền màu theo thời gian.
                                </p>
                                <a href="./BlogDetail.aspx" class="btn-read-more">Đọc thêm</a>
                            </div>
                        </div>
                    </div>
                </div>
                <nav class="pagination-wrapper" aria-label="Page navigation">
                    <ul class="pagination">
                        <li class="page-item disabled">
                            <a class="page-link" href="#" tabindex="-1" aria-disabled="true">&laquo;</a>
                        </li>
                        <li class="page-item active" aria-current="page">
                            <a class="page-link" href="#">1</a>
                        </li>
                        <li class="page-item"><a class="page-link" href="#">2</a></li>
                        <li class="page-item">
                            <a class="page-link" href="#">&raquo;</a>
                        </li>
                    </ul>
                </nav>
            </div>
        </main>
        <uc:Footer runat="server" ID="header2" />
    </form>
    <script src="../assets/js/main.js"></script>


</body>
</html>
