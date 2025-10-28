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
                        </ul>" },
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
                        </ul>"
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
                        </ul>"
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
                        </ul>"
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
                        </ul>"
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
                        </ul>"
                },
                // (Thêm 4 sản phẩm áo thun khác ở đây... Id = 3, 4, 5, 6)

                // === DANH MỤC 2: Áo Sơ Mi (CategoryId = 2) ===
                new Product {
                    Id = 7,
                    Name = "Áo Sơ Mi Sản Phẩm 7",
                    CategoryId = 2,
                    Price = 350000,
                    Stock = 40,
                    ImageUrl = "/Uploads/Products/ten_anh_7.jpg",
                    Description = "Nội dung mô tả chi tiết của bạn cho sản phẩm 7."
                },
                new Product {
                    Id = 8,
                    Name = "Áo Sơ Mi Sản Phẩm 8",
                    CategoryId = 2,
                    Price = 360000,
                    Stock = 40,
                    ImageUrl = "/Uploads/Products/ten_anh_8.jpg",
                    Description = "Nội dung mô tả chi tiết của bạn cho sản phẩm 8."
                },
                // (Thêm 4 sản phẩm áo sơ mi khác ở đây... Id = 9, 10, 11, 12)

                // === DANH MỤC 3: Áo Khoác (CategoryId = 3) ===
                new Product {
                    Id = 13,
                    Name = "Áo Khoác Sản Phẩm 13",
                    CategoryId = 3,
                    Price = 550000,
                    Stock = 30,
                    ImageUrl = "/Uploads/Products/ten_anh_13.jpg",
                    Description = "Nội dung mô tả chi tiết của bạn cho sản phẩm 13."
                },
                // (Thêm 5 sản phẩm áo khoác khác ở đây...)

                // === DANH MỤC 4: Quần Jeans (CategoryId = 4) ===
                new Product {
                    Id = 19,
                    Name = "Quần Jeans Sản Phẩm 19",
                    CategoryId = 4,
                    Price = 450000,
                    Stock = 30,
                    ImageUrl = "/Uploads/Products/ten_anh_19.jpg",
                    Description = "Nội dung mô tả chi tiết của bạn cho sản phẩm 19."
                },
                // (Thêm 5 sản phẩm quần jeans khác ở đây...)

                // === DANH MỤC 5: Quần Shorts (CategoryId = 5) ===
                new Product {
                    Id = 25,
                    Name = "Quần Shorts Sản Phẩm 25",
                    CategoryId = 5,
                    Price = 200000,
                    Stock = 60,
                    ImageUrl = "/Uploads/Products/ten_anh_25.jpg",
                    Description = "Nội dung mô tả chi tiết của bạn cho sản phẩm 25."
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