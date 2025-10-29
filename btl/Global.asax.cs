using System;
using System.Collections.Generic;
using btl.Models;

namespace btl
{
    public class Global : System.Web.HttpApplication
    {
        // BIẾN NÀY VẪN GIỮ LẠI để tạo Đơn hàng (Orders) và Thanh toán (Payments) ngẫu nhiên
        private static Random random = new Random();

        protected void Application_Start(object sender, EventArgs e)
        {
            // === KHỐI 1: TẠO USERS (Giữ nguyên) ===
            List<User> users = new List<User>
            {
                new User { id = 1, fullname = "Admin User", email = "admin@gmail.com", password = "admin", role = "ADMIN" },
                new User { id = 2, fullname = "Khách Hàng A", email = "khachhangA@gmail.com", password = "123", role = "USER" }
            };
            Application["users"] = users;

            // === KHỐI 2: TẠO CATEGORIES (Giữ nguyên) ===
            List<Category> categories = new List<Category>
            {
                new Category { Id = 1, Name = "Áo Thun & T-shirt", PageData = "products-tshirt" },
                new Category { Id = 2, Name = "Áo Sơ Mi", PageData = "products-somi" },
                new Category { Id = 3, Name = "Áo Khoác", PageData = "products-khoac" },
                new Category { Id = 4, Name = "Quần Jeans", PageData = "products-jeans" },
                new Category { Id = 5, Name = "Quần Shorts", PageData = "products-shorts" }
            };
            Application["categories"] = categories;

            // === KHỐI 3: TẠO SẢN PHẨM CỐ ĐỊNH (ĐÃ THAY THẾ) ===
            // 
            // Dòng code cũ: List<Product> products = GenerateFakeProducts(30, categories);
            // Đã được thay bằng danh sách cố định bên dưới.
            // 
            List<Product> products = new List<Product>
            {
                // === DANH MỤC 1: Áo Thun & T-shirt (CategoryId = 1) ===
                new Product {
                    Id = 1,
                    Name = "Áo Universal T - 250 Black", // <-- THAY TÊN
                    CategoryId = 1,
                    Price = 499000, // <-- THAY GIÁ
                    Stock = 50, // <-- THAY TỒN KHO
                    ImageUrl = "/Uploads/Products/AoThun1.jpg", // <-- THAY ĐƯỜNG DẪN ẢNH
                    Description =@"
                        <ul>
                            <li>Chất liệu: 100% Cotton, định lượng 250gsm</li>
                            <li>Vải dày dặn, bề mặt chất mịn, giữ phom tốt, bền chắc sau nhiều lần giặt.</li>
                        </ul>",
                    ShortDescription = "Chất liệu 100% Cotton, dày dặn, giữ phom tốt."
                },

                new Product {
                    Id = 2,
                    Name = "Áo Universal T - 250 White",
                    CategoryId = 1,
                    Price = 490000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/AoThun2.jpg",
                    Description = @"
                        <ul>
                            <li>Chất liệu: 100% Cotton, định lượng 250gsm</li>
                            <li>Vải dày dặn, bề mặt chất mịn, giữ phom tốt, bền chắc sau nhiều lần giặt</li>
                            <li>Phom dáng: oversize, unisex</li>
                            <li>Thiết kế: Áo thun trơn, tay ngắn, cổ tròn bo viền cổ điển</li>
                        </ul>",
                    ShortDescription = "Chất liệu: 100% Cotton, định lượng 250gsm"

                },
                new Product {
                    Id =3,
                    Name = "Áo Daughter T - 250 Grey",
                    CategoryId = 1,
                    Price = 260000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/AoThun3.jpg",
                    Description = @"
                        <ul>
                            <li>Chất liệu: 100% Cotton, định lượng 250gsm</li>
                            <li>Vải dày dặn, bề mặt chất mịn, giữ phom tốt, bền chắc sau nhiều lần giặt</li>
                            <li>Phom dáng: Baby T – ôm gọn, tôn dáng nhưng vẫn thoải máix</li>
                            <li>Áo thun trơn, tay ngắn, cổ tròn bo viềnli>
                        </ul>",
                    ShortDescription = "Chất liệu: 100% Cotton, định lượng 250gsm"

                },
                new Product {
                    Id =4,
                    Name = "Áo Life T - 250 Black",
                    CategoryId = 1,
                    Price = 260000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/AoThun4.jpg",
                    Description = @"
                        <ul>
                            <li>Chất liệu: 100% Cotton, định lượng 250gsm</li>
                            <li>Vải dày dặn, bề mặt chất mịn, giữ phom tốt, bền chắc sau nhiều lần giặt</li>
                            <li>Phom dáng: Baby T – ôm gọn, tôn dáng nhưng vẫn thoải máix</li>
                            <li>Áo thun trơn, tay ngắn, cổ tròn bo viền<li>
                        </ul>",
                    ShortDescription = "Chất liệu: 100% Cotton, định lượng 250gsm"

                },
                new Product {
                    Id =5,
                    Name = "Áo Daughter T - 250 Yellow",
                    CategoryId = 1,
                    Price = 260000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/AoThun5.jpg",
                    Description = @"
                        <ul>
                            <li>Chất liệu: 100% Cotton, định lượng 250gsm</li>
                            <li>Vải dày dặn, bề mặt chất mịn, giữ phom tốt, bền chắc sau nhiều lần giặt</li>
                            <li>Phom dáng: Baby T – ôm gọn, tôn dáng nhưng vẫn thoải máix</li>
                            <li>Áo thun trơn, tay ngắn, cổ tròn bo viền<li>
                            <li>Chi tiết: Đường may zigzag tỉ mỉ, giữ form ổn định lâu dài<li>
                        </ul>",
                    ShortDescription = "Chất liệu: 100% Cotton, định lượng 250gsm"

                },
                new Product {
                    Id =6,
                    Name = "Áo thun sọc Sunday T - Signature Blue Stripe",
                    CategoryId = 1,
                    Price =549000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/AoThun6.jpeg",
                    Description = @"
                        <ul>
                            <li>Chất liệu: 100% Cotton</li>
                            <li>Chất vải mềm mại, bền màu, lâu phai với kỹ thuật nhuộm sợi Yarn Dye</li>
                            <li>Phom dáng: Cropped</li>
                            <li>Thiết kế: Cổ tròn, tay ngắn <li>
                            <li>Chi tiết: Đường may zigzag tỉ mỉ, giữ form ổn định lâu dài<li>
                        </ul>",
                    ShortDescription = "Chất liệu: 100% Cotton, định lượng 250gsm"

                },
                // (Thêm 4 sản phẩm áo thun khác ở đây... Id = 3, 4, 5, 6)

                // === DANH MỤC 2: Áo Sơ Mi (CategoryId = 2) ===
                new Product {
                    Id =7,
                    Name = "Áo Soho Shirt - Premium Carbon Peaching Milky Blue",
                    CategoryId = 2,
                    Price =1099000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/AoSoMi1.jpg",
                    Description = @"
                        <ul>
                            <li>Chất liệu: 100% Cotton</li>
                            <li>Chất vải premium chắc chắn, giữ phom tốt</li>
                            <li>Xử lý giặt: Vải có hiệu ứng washed appearance, màu sắc tự nhiên và kẻ sọc sắc nét</li>
                            <li>Tiêu chuẩn: Sản xuất xanh, chứng nhận OEKO - TEX Standard 100<li>
                            <li>Chi tiết: Thêu logo con mắt ở cổ tay áo<li>
                        </ul>",
                    ShortDescription = "Chất liệu: 100% Cotton"

                },
                new Product {
                    Id =8,
                    Name = "Áo Soho Shirt - Carbon Blue Stripe",
                    CategoryId = 2,
                    Price =1099000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/AoSoMi2.jpg",
                    Description = @"
                        <ul>
                            <li>Chất liệu: 100% Cotton</li>
                            <li>Chất vải premium chắc chắn, giữ phom tốt</li>
                            <li>Xử lý giặt: Vải có hiệu ứng washed appearance, màu sắc tự nhiên và kẻ sọc sắc nét</li>
                            <li>Tiêu chuẩn: Sản xuất xanh, chứng nhận OEKO - TEX Standard 100<li>
                            <li>Thiết kế: Sơ mi tay dài cơ bản, chiều dài qua mông, lai áo bầu<li>
                        </ul>",
                    ShortDescription = "Chất liệu: 100% Cotton"

                },
                 new Product {
                    Id =9,
                    Name = "Áo Standard Lux Shirt - Ivory White",
                    CategoryId = 2,
                    Price =999000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/AoSoMi3.jpg",
                    Description = @"
                        <ul>
                            <li>Chất liệu: Cotton (96% Cotton + 4% Spandex)</li>
                            <li>Chất vải mềm mại, co giãn vừa phải, đủ nhẹ để thoáng mát và dễ chịu</li>
                            <li>Xử lý bề mặt: Easy Care và Silky</li>
                            <li>Easy Care Finishing giúp hạn chế nhăn, giữ phom tốt, hạn chế co rút<li>
                            <li>Silky Finishing giúp vải mượt mà, giảm ma sát với da, tăng độ bóng và hạn chế xù lông<li>
                        </ul>",
                    ShortDescription = "Chất liệu: Cotton (96% Cotton + 4% Spandex)"

                },

                 new Product {
                    Id =10,
                    Name = "Áo Boyfriend Big Shirt - Premium Chess Blue",
                    CategoryId = 2,
                    Price =1099000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/AoSoMi4.jpg",
                    Description = @"
                        <ul>
                            <li>Chất liệu:Chất liệu: 100% Cotton</li>
                            <li>Xử lý bề mặt: Crisp, thoáng mát & Peach headfeel – mềm mịn, thoải mái trên da</li>
                            <li>Hiệu ứng giặt (washed appearance) tạo sắc độ tự nhiên, Carbon peaching: thêm công đoạn wash & peach, cho cảm giác mặc cao cấp hơn</li>
                            <li>Tiêu chuẩn: Sản xuất xanh, đạt chứng nhận OEKO-TEX Standard 100<li>
                            <li>Phom dáng: Oversized, dài và phóng khoáng theo tinh thần “boyfriend fit”<li>
                        </ul>",
                    ShortDescription = "Chất liệu: 100% Cotton"

                },

                 new Product {
                    Id =11,
                    Name = "Áo NewYork Shirt - Pearl Green Gingham",
                    CategoryId = 2,
                    Price =799000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/AoSoMi5.jpg",
                    Description = @"
                        <ul>
                            <li>Chất liệu:Chất liệu: 100% Cotton</li>
                            <li>Kĩ thuật dệt: Poplin cho cấu trúc vải bền chặt và đứng phom</li>
                            <li> Bề mặt xử lý: Regular Soft làm mềm vải, thoải mái khi mặc</li>
                            <li> Tiêu chuẩn: Sản xuất xanh, chứng nhận OEKO - TEX Standard 100<li>
                            <li>Phom dáng: Relaxed<li>
                        </ul>",
                    ShortDescription = "Chất liệu: 100% Cotton"

                },
                 new Product {
                    Id =12,
                    Name = "Áo Sơ Mi Modern Arc Oversized Shirt - Blue Navy Stripe",
                    CategoryId = 2,
                    Price =799000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/AoSoMi6.jpg",
                    Description = @"
                        <ul>
                            <li>Chất liệu: 100% Cotton</li>
                            <li>Chất vải mềm mịn, thoáng mát, có độ xốp nhẹ</li>
                            <li>Phom dáng: Oversized</li>
                            <li>Chi tiết: Phối viền trắng ở cổ áo, lai áo và các đường cắt<li>
                            <li>Chiều dài áo trước: size S - 69cm, mỗi size thêm 1cm<li>
                        </ul>",
                    ShortDescription = "Chất liệu: 100% Cotton"

                },
                // (Thêm 4 sản phẩm áo sơ mi khác ở đây... Id = 9, 10, 11, 12)

                // === DANH MỤC 3: Áo Khoác (CategoryId = 3) ===
               new Product {
                    Id =13,
                    Name = "Áo khoác gió nữ đa năng 2 lớp, gấp gọn tiện lợi",
                    CategoryId = 3,
                    Price =499000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/AoKhoac1.jpg",
                    Description = @"
                        <ul>
                            <li>Áo Khoác Gió Nữ Đa Năng Tokyolife - Chống gió, hạn chế nắng rát hiệu quả, thoải mái vận động ngoài trời</li>
                          
                        </ul>",
                    ShortDescription = "Áo Khoác Gió Nữ Đa Năng Tokyolife - Chống gió, hạn chế nắng rát hiệu quả, thoải mái vận động ngoài trời"

                },
                new Product {
                    Id =14,
                    Name = "Áo khoác gió nữ đa năng, thời trang tiện lợi",
                    CategoryId = 3,
                    Price =499000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/AoKhoac2.jpg",
                    Description = @"
                        <ul>
                            <li>Áo Khoác Gió Nữ Đa Năng Tokyolife: Che Chắn Hoàn Hảo, Phong Cách Thời Trang!</li>
                          
                        </ul>",
                    ShortDescription = "Áo Khoác Gió Nữ Đa Năng Tokyolife: Che Chắn Hoàn Hảo, Phong Cách Thời Trang!"

                },
                new Product {
                    Id =15,
                    Name = "Áo khoác gió nữ 1 lớp cổ cao",
                    CategoryId = 3,
                    Price =599000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/AoKhoac3.jpg",
                    Description = @"
                        <ul>
                            <li>Áo Khoác Gió Nữ 1 Lớp Cổ Cao - Gọn Nhẹ Linh Hoạt, Cản Gió Năng Động</li>                        
                            <li>Trải nghiệm sự gọn nhẹ và linh hoạt tuyệt đối cho mọi hoạt động. Áo khoác gió 1 lớp là giải pháp đa năng, giúp bạn cản gió nhẹ, giữ ấm cơ thể mà không gây bí bách hay cồng kềnh.</li>

                          
                        </ul>",
                    ShortDescription = "Áo Khoác Gió Nữ 1 Lớp Cổ Cao - Gọn Nhẹ Linh Hoạt, Cản Gió Năng Động"

                },
                new Product {
                    Id =16,
                    Name = "Áo khoác gió nữ vải gió 2 lớp mũ liền có dây rút",
                    CategoryId = 3,
                    Price =549000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/AoKhoac4.jpg",
                    Description = @"
                        <ul>
                            <li>Áo khoác gió nữ 2 lớp mũ liền có dây rút đa năng TokyoLife - thiết kế để mang đến sự cân bằng giữa phong cách và tiện ích.</li>
                          
                        </ul>",
                    ShortDescription = "Áo Khoác Gió Nữ Đa Năng Tokyolife - Chống gió, hạn chế nắng rát hiệu quả, thoải mái vận động ngoài trời"

                },
                new Product {
                    Id =17,
                    Name = "Áo khoác lông cừu sát nách cổ cao ấm áp",
                    CategoryId = 3,
                    Price =349000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/AoKhoac5.jpg",
                    Description = @"
                        <ul>
                            <li>Lông cừu có ở cả 2 bề mặt (kể cả túi áo) giúp giữ ấm cực tốt</li>                  
                            <li>Khả năng giữ ấm, giữ nhiệt hiệu quả gấp 3 lần áo khoác nỉ thông thường</li>

                          
                        </ul>",
                    ShortDescription = "Mềm mại, thoải mái, dễ chịu khi chạm vào"

                },
                  new Product {
                    Id =18,
                    Name = "Áo khoác phao",
                    CategoryId = 3,
                    Price =1190000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/AoKhoac6.jpg",
                    Description = @"
                        <ul>
                            <li>Khả năng giữ ấm, giữ nhiệt hiệu quả gấp 3 lần áo khoác nỉ thông thường</li>
                        </ul>",
                    ShortDescription = "Mềm mại, thoải mái, dễ chịu khi chạm vào"

                },
                // (Thêm 5 sản phẩm áo khoác khác ở đây...)

                // === DANH MỤC 4: Quần Jeans (CategoryId = 4) ===
                 new Product {
                    Id =19,
                    Name = "Quần The Original Dad Jeans - White Vintage",
                    CategoryId = 4,
                    Price =890000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/QuanJeans1.jpg",
                    Description = @"
                        <ul>
                            <li> Chất liệu: Denim 100% Cotton</li>                       
                           <li >Chất vải bền màu, chắc chắn</li>       
                           <li >Phom dáng: Suông</li>
                           <li>  Thiết kế: Cạp cao ôm hông, ống thẳng đứng</li>
                        </ul>",
                    ShortDescription = " Chất liệu: Denim 100% Cotton"

                },
                new Product {
                    Id =20,
                    Name = "Quần The Original Dad Jeans - Black Vintage",
                    CategoryId = 4,
                    Price =721650,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/QuanJeans2.jpg",
                    Description = @"
                        <ul>
                            <li> Chất liệu: Denim 100% Cotton</li>                       
                           <li >Chất vải bền màu, chắc chắn</li>       
                           <li >Phom dáng: Suông</li>
                           <li>  Thiết kế: Cạp cao ôm hông, ống thẳng đứng</li>
                        </ul>",
                    ShortDescription = " Chất liệu: Denim 100% Cotton"

                },
                new Product {
                    Id =21,
                    Name = "Quần Daughter Jeans - Favorite Black",
                    CategoryId = 4,
                    Price =890000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/QuanJeans3.jpg",
                    Description = @"
                        <ul>
                            <li> Chất liệu: Denim 100% Cotton</li>                       
                           <li >Chất vải bền màu, chắc chắn</li>       
                           <li >Sử dụng công nghệ Enzyme Wash làm mềm vải</li>
                           <li>Thiết kế: Quần ống rộng, cạp trễ, với 5 túi tiện dụng</li>
                        </ul>",
                    ShortDescription = " Chất liệu: Denim 100% Cotton"

                },
               new Product {
                    Id =22,
                    Name = "Quần Daughter Jeans - Dusty Rose Wash",
                    CategoryId = 4,
                    Price =721650,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/QuanJeans4.jpg",
                    Description = @"
                        <ul>
                            <li> Chất liệu: Denim 100% Cotton</li>                       
                           <li >Chất vải bền màu, chắc chắn</li>       
                           <li >Sử dụng công nghệ Enzyme Wash làm mềm vải</li>
                           <li> Phom dáng: Loose Fit</li>
                        </ul>",
                    ShortDescription = " Chất liệu: Denim 100% Cotton"

                },
               new Product {
                    Id =23,
                    Name = "Quần Daughter Jeans - Dusty Rose Wash",
                    CategoryId = 4,
                    Price =859000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/QuanJeans5.jpg",
                    Description = @"
                        <ul>
                            <li> Chất liệu: Denim 100% Cotton</li>                       
                           <li > Chất vải bền màu, chắc chắn, có độ mềm mại và dễ chịu</li>       
                           <li >Phom dáng: Suông thẳng</li>
                           <li> Thiết kế: Cạp cao ôm hông, ống quần rộng</li>
                        </ul>",
                    ShortDescription = " Chất liệu: Denim 100% Cotton"

                },
                new Product {
                    Id =24,
                    Name = "Quần The Original Grandpa Jeans - Worker Blue",
                    CategoryId = 4,
                    Price =849000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/QuanJeans6.jpg",
                    Description = @"
                        <ul>
                            <li> Chất liệu: Denim 100% Cotton</li>                       
                           <li > Chất vải bền màu, chắc chắn, có độ mềm mại và dễ chịu</li>       
                           <li >Phom dáng: Suông thẳng</li>
                           <li> Thiết kế: Cạp cao ôm hông, ống quần rộng</li>
                        </ul>",
                    ShortDescription = " Chất liệu: Denim 100% Cotton"

                },


                // (Thêm 5 sản phẩm quần jeans khác ở đây...)

                // === DANH MỤC 5: Quần Shorts (CategoryId = 5) ===
              new Product {
                    Id =25,
                    Name = "Quần Golden Hour Shorts - Blue Wash",
                    CategoryId = 5,
                    Price =749000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/QuanShorts1.png",
                    Description = @"
                        <ul>
                            <li> Chất liệu: Denim 100% Cotton</li>                       
                           <li > Chất vải bền màu, chắc chắn, có độ mềm mại và dễ chịu</li>       
                           <li >Phom dáng: Mid-rise fit</li>
                           <li>Chi tiết: Lai quần cắt tưa</li>
                        </ul>",
                    ShortDescription = " Chất liệu: Denim 100% Cotton"

                },
               new Product {
                    Id =26,
                    Name = "Quần Dad Shorts - Off White",
                    CategoryId = 5,
                    Price =749000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/QuanShorts2.png",
                    Description = @"
                        <ul>
                            <li> Chất liệu: Denim 100% Cotton</li>                       
                           <li > Chất vải bền màu, chắc chắn, có độ mềm mại và dễ chịu</li>       
                           <li >Phom dáng: Regular fit</li>
                           <li>Chi tiết: Sử dụng 5 nút gài thay vì giây kéo</li>
                        </ul>",
                    ShortDescription = " Chất liệu: Denim 100% Cotton"

                },

                new Product {
                    Id =27,
                    Name = "Quần Golden Hour Shorts - Modern White",
                    CategoryId = 5,
                    Price =749000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/QuanShorts3.png",
                    Description = @"
                        <ul>
                            <li> Chất liệu: Denim 100% Cotton</li>                       
                           <li >Công nghệ: Sử dụng Enzyme Wash làm mềm vải</li>       
                           <li >Phom dáng: Mid-rise fit</li>
                           <li> Chi tiết: Lai quần cắt tưa</li>
                        </ul>",
                    ShortDescription = " Chất liệu: Denim 100% Cotton"

                },

                  new Product {
                    Id =28,
                    Name = "Quần Dad Shorts - Worker Blue",
                    CategoryId = 5,
                    Price =749000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/QuanShorts4.png",
                    Description = @"
                        <ul>
                            <li> Chất liệu: Denim 100% Cotton</li>                       
                           <li > Thiết kế: Quần ngắn ngang đùi</li>       
                           <li >Phom dáng: Mid-rise fit</li>
                           <li> Chi tiết: Lai quần cắt tưa</li>
                        </ul>",
                    ShortDescription = " Chất liệu: Denim 100% Cotton"

                },
                    new Product {
                    Id =29,
                    Name = "Quần Dad Shorts - Black Vintage",
                    CategoryId = 5,
                    Price =749000,
                    Stock = 50,
                    ImageUrl = "/Uploads/Products/QuanShorts5.png",
                    Description = @"
                        <ul>
                            <li> Chất liệu: Denim 100% Cotton</li>                       
                           <li > Thiết kế: Quần ngắn ngang đùi</li>       
                           <li >Phom dáng: Mid-rise fit</li>
                           <li> Chi tiết: Lai quần cắt tưa</li>
                        </ul>",
                    ShortDescription = " Chất liệu: Denim 100% Cotton"

                },
                // (Thêm 5 sản phẩm quần shorts khác ở đây... đến Id = 30)

            };
            Application["products"] = products;


            // === KHỐI 4: TẠO ORDERS (Giữ nguyên) ===
            // (Khối này vẫn dùng 'random' để tạo dữ liệu đơn hàng mẫu)
            List<Order> orders = new List<Order>
            {
                new Order { Id = 1001, UserId = 2, CustomerName = "Khách Hàng A", TotalAmount = products[random.Next(products.Count)].Price * random.Next(1, 3), Status = "Pending", OrderDate = DateTime.Now.AddDays(-random.Next(1, 5)) },
                new Order { Id = 1002, UserId = 2, CustomerName = "Khách Hàng A", TotalAmount = products[random.Next(products.Count)].Price * random.Next(1, 2), Status = "Shipped", OrderDate = DateTime.Now.AddDays(-random.Next(6, 10)) },
                new Order { Id = 1003, UserId = 1, CustomerName = "Admin User", TotalAmount = products[random.Next(products.Count)].Price, Status = "Completed", OrderDate = DateTime.Now.AddDays(-random.Next(11, 20)) }
            };
            Application["orders"] = orders;


            // === KHỐI 5: TẠO PAYMENTS (Giữ nguyên) ===
            // (Khối này cũng dùng 'random')
            List<Payment> payments = new List<Payment>();
            foreach (var order in orders)
            {
                payments.Add(new Payment
                {
                    TransactionId = $"PM{order.Id}",
                    OrderId = order.Id,
                    Amount = order.TotalAmount,
                    Method = (random.Next(0, 2) == 0) ? "COD" : "VNPAY",
                    Status = (order.Status == "Completed" || order.Status == "Shipped") ? "Success" : "Pending",
                    PaymentDate = order.OrderDate.AddHours(random.Next(1, 5))
                });
            }
            Application["payments"] = payments;
        }

        // 
        // HÀM GenerateFakeProducts(int count, List<Category> categories) ĐÃ BỊ XÓA
        // 
    }
}