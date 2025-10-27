using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace btl.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Mã định danh (ví dụ: "products-tshirt") để liên kết với CategoryId của Product
        public string PageData { get; set; }
        
        public Category() { }
    }
}