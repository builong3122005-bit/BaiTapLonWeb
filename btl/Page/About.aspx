<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="btl.Page.About" %>

<%@ Register Src="~/UserControl/Header.ascx" TagPrefix="uc" TagName="Header" %>
<%@ Register Src="~/UserControl/Footer.ascx" TagPrefix="uc" TagName="Footer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Về chúng tôi - Shop ABC</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <link
        href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700&display=swap"
        rel="stylesheet" />

    <link rel="stylesheet" href="~/assets/css/reset.css" />
    <link rel="stylesheet" href="~/assets/css/grid.css" />
    <link rel="stylesheet" href="~/assets/css/common.css" />
    <link rel="stylesheet" href="~/assets/css/static-page.css" />
    <link rel="stylesheet" href="~/assets/css/icons.css" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:Header runat="server" ID="header1" />
        <div>
            <main>
                <div class="page-header">
                    <h1>Về ABC Shop</h1>
                </div>

                <div class="container py-5">
                    <section class="about-section">
                        <div class="layout-two-col">
                            <div class="grid-item">
                                <img
                                    src="https://images.unsplash.com/photo-1556905055-8f358a7a47b2?q=80&w=2070&auto=format&fit=crop"
                                    alt="Câu chuyện thương hiệu"
                                    class="img-fluid rounded shadow" />
                            </div>
                            <div class="grid-item">
                                <div class="static-content">
                                    <h2 class="section-subtitle">Câu Chuyện Thương Hiệu</h2>
                                    <p>
                                        <strong>ABC Shop</strong> được thành lập vào năm 2020 với một
                  mục tiêu đơn giản: mang đến cho các bạn trẻ Việt Nam những sản
                  phẩm thời trang chất lượng, hợp xu hướng và giá cả phải chăng.
                  Chúng tôi tin rằng thời trang là một cách để thể hiện cá tính
                  và sự tự tin, và mọi người đều xứng đáng được mặc đẹp mỗi
                  ngày.
                                    </p>
                                    <p>
                                        Bắt đầu từ một cửa hàng nhỏ trực tuyến, với niềm đam mê và sự
                  nỗ lực không ngừng, ABC Shop đã dần chiếm được cảm tình của
                  khách hàng và phát triển thành một thương hiệu được nhiều
                  người biết đến.
                                    </p>
                                </div>
                            </div>
                        </div>
                    </section>

                    <section class="about-section text-center">
                        <h2 class="section-subtitle">Giá Trị Cốt Lõi</h2>
                        <div class="grid grid-3">
                            <div class="grid-item">
                                <div class="feature-box">
                                    <div class="feature-icon">
                                        <img
                                            src="../assets/img/icon-chatluong.png"
                                            alt="Chất lượng"
                                            class="icon-img" />
                                    </div>
                                    <h3 class="feature-title">Chất Lượng Hàng Đầu</h3>
                                    <p>
                                        Chúng tôi tỉ mỉ trong từng đường may, lựa chọn chất liệu vải
                  tốt nhất để tạo ra những sản phẩm bền đẹp theo thời gian.
                                    </p>
                                </div>
                            </div>
                            <div class="grid-item">
                                <div class="feature-box">
                                    <div class="feature-icon">
                                        <img
                                            src="../assets/img/icon-sangtao.png"
                                            alt="Sáng tạo"
                                            class="icon-img" />
                                    </div>
                                    <h3 class="feature-title">Thiết Kế Sáng Tạo</h3>
                                    <p>
                                        Luôn cập nhật những xu hướng mới nhất, mang đến những thiết kế
                  độc đáo và hiện đại, giúp bạn luôn nổi bật.
                                    </p>
                                </div>
                            </div>
                            <div class="grid-item">
                                <div class="feature-box">
                                    <div class="feature-icon">
                                        <img
                                            src="../assets/img/icon-khachhang.png"
                                            alt="Khách hàng"
                                            class="icon-img" />
                                    </div>
                                    <h3 class="feature-title">Khách Hàng Là Trung Tâm</h3>
                                    <p>
                                        Sự hài lòng của bạn là ưu tiên số một. Chúng tôi luôn lắng
                  nghe và sẵn sàng hỗ trợ để mang lại trải nghiệm mua sắm tốt
                  nhất.
                                    </p>
                                </div>
                            </div>
                        </div>
                    </section>

                    <section class="about-section text-center bg-light-gray">
                        <h2 class="section-subtitle">Gặp Gỡ Đội Ngũ</h2>
                        <div class="grid grid-3 team-grid">
                            <div class="grid-item">
                                <div class="team-member">
                                    <img
                                        src="../assets/img/Avt1.jpg"
                                        alt="Team member 1"
                                        class="team-photo" />
                                    <h4 class="team-name">Nguyễn Văn A</h4>
                                    <p class="team-role">Nhà sáng lập & CEO</p>
                                </div>
                            </div>
                            <div class="grid-item">
                                <div class="team-member">
                                    <img
                                        src="../assets/img/Avt2.jpg"
                                        alt="Team member 2"
                                        class="team-photo" />
                                    <h4 class="team-name">Trần Thị B</h4>
                                    <p class="team-role">Trưởng phòng Thiết kế</p>
                                </div>
                            </div>
                            <div class="grid-item">
                                <div class="team-member">
                                    <img
                                        src="../assets/img/Avt3.jpg"
                                        alt="Team member 3"
                                        class="team-photo" />
                                    <h4 class="team-name">Lê Văn C</h4>
                                    <p class="team-role">Giám đốc Marketing</p>
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
            </main>
        </div>
        <uc:Footer runat="server" ID="header2" />
    </form>
    <script src="../assets/js/main.js"></script>


</body>
</html>
