<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="btl.Page.Contact" %>

<%@ Register Src="~/UserControl/Header.ascx" TagPrefix="uc" TagName="Header" %>
<%@ Register Src="~/UserControl/Footer.ascx" TagPrefix="uc" TagName="Footer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Liên hệ - Shop ABC</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <link
        href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700&display=swap"
        rel="stylesheet" />

    <link rel="stylesheet" href="~/assets/css/reset.css" />
    <link rel="stylesheet" href="~/assets/css/grid.css" />
    <link rel="stylesheet" href="~/assets/css/common.css" />
    <link rel="stylesheet" href="~/assets/css/forms.css" />
    <link rel="stylesheet" href="~/assets/css/contact.css" />

    <link rel="stylesheet" href="css/icons.css" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:Header runat="server" ID="header1" />
        <main>
            <div class="page-header">
                <h1>Liên hệ với chúng tôi</h1>
            </div>
            <div class="container py-5">
                <div class="layout-two-col contact-layout">
                    <div class="grid-item">
                        <div class="contact-info-wrapper">
                            <h2 class="contact-title">Thông tin liên hệ</h2>
                            <p class="contact-intro">
                                Chúng tôi luôn sẵn sàng lắng nghe bạn. Vui lòng liên hệ qua
                thông tin dưới đây hoặc điền vào biểu mẫu bên cạnh.
                            </p>
                            <ul class="contact-details">
                                <li>
                                    <img src="../assets/img/address.png" alt="" class="icon-img" />
                                    <span>19 Lê Thánh Tông, Hoàn Kiếm, Hà Nội</span>
                                </li>
                                <li>
                                    <img src="../assets/img/phone.png" alt="" class="icon-img" />
                                    <span>(024) 39 876 543</span>
                                </li>
                                <li>
                                    <img src="../assets/img/email.png" alt="" class="icon-img" />
                                    <span>support@abcshop.com</span>
                                </li>
                                <li>
                                    <img src="../assets/img/time.png" alt="" class="icon-img" />
                                    <span>Giờ làm việc: 08:30 - 21:30 (T2 - CN)</span>
                                </li>
                            </ul>
                            <div class="map-container">
                                <iframe
                                    src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3724.113115933614!2d105.8509539153556!3d21.02813999312134!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3135ab95335821a1%3A0x80c255dbe36879e6!2zTmjDoCBow6F0IEzhu5duIEjDoCBO4buZaQ!5e0!3m2!1svi!2s!4v1677654321098!5m2!1svi!2s"
                                    width="100%"
                                    height="300"
                                    style="border: 0"
                                    allowfullscreen=""
                                    loading="lazy"
                                    referrerpolicy="no-referrer-when-downgrade"></iframe>
                            </div>
                        </div>
                    </div>
                    <div class="grid-item">
                        <div class="form-wrapper contact-form-wrapper">
                            <h2 class="contact-title text-center">Gửi tin nhắn cho chúng tôi
                            </h2>
                            <form>
                                <div class="form-group">
                                    <label for="contact-name">Họ và tên</label>
                                    <input type="text" id="contact-name" required />
                                </div>
                                <div class="form-group">
                                    <label for="contact-email">Email</label>
                                    <input type="email" id="contact-email" required />
                                </div>
                                <div class="form-group">
                                    <label for="contact-subject">Chủ đề</label>
                                    <input type="text" id="contact-subject" required />
                                </div>
                                <div class="form-group">
                                    <label for="contact-message">Nội dung tin nhắn</label>
                                    <textarea id="contact-message" rows="6" required></textarea>
                                </div>
                                <button type="submit" class="btn">Gửi đi</button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </main>
        <uc:Footer runat="server" ID="header2" />
    </form>
    <script src="/assets/js/main.js"></script>
</body>
</html>
