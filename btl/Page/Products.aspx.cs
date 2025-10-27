using btl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace btl.Page
{
    public partial class Products : System.Web.UI.Page
    {
        private const int PageSize = 6;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateFilters();
                LoadProducts();
            }
        }

        private void PopulateFilters()
        {
            List<Category> categories = (List<Category>)Application["categories"];
            if (categories != null && cblCategories.Items.Count == 0)
            {
                foreach (var category in categories)
                {
                    cblCategories.Items.Add(new ListItem(Server.HtmlEncode(category.Name), category.Id.ToString()));
                }
            }

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["cat"]))
                {
                    string[] selectedCats = Request.QueryString["cat"].Split(',');
                    foreach (ListItem item in cblCategories.Items)
                    {
                        item.Selected = selectedCats.Contains(item.Value);
                    }
                }
                if (!string.IsNullOrEmpty(Request.QueryString["price"]))
                {
                    string[] selectedPrices = Request.QueryString["price"].Split(',');
                    foreach (ListItem item in cblPriceRanges.Items)
                    {
                        item.Selected = selectedPrices.Contains(item.Value);
                    }
                }
            }
        }

        protected void Filters_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProducts(true);
        }

        protected void rptProductList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {
                if (int.TryParse(e.CommandArgument.ToString(), out int productId))
                {
                    AddProductToCart(productId);
                }
            }
        }

        private void AddProductToCart(int productId)
        {
            List<Product> allProducts = (List<Product>)Application["products"];
            Product productToAdd = allProducts?.FirstOrDefault(p => p.Id == productId);

            if (productToAdd == null) return;

            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();

            int quantityToAdd = 1;
            string selectedSize = "M"; // Default size for quick add

            CartItem existingItem = cart.FirstOrDefault(item => item.ProductId == productToAdd.Id && item.Size == selectedSize);

            if (existingItem != null)
            {
                existingItem.Quantity += quantityToAdd;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = productToAdd.Id,
                    ProductName = productToAdd.Name,
                    Price = productToAdd.Price,
                    Quantity = quantityToAdd,
                    ImageUrl = productToAdd.ImageUrl,
                    Size = selectedSize
                });
            }

            Session["Cart"] = cart;
            var header = (btl.UserControl.Header)Page.FindControl("header1"); 
            header?.UpdateCartCount(); 
            // Show confirmation message using JavaScript
            string script = @"
                var popup = document.getElementById('addCartMessagePopup');
                if(popup) {
                    popup.classList.add('show');
                    setTimeout(function() { popup.classList.remove('show'); }, 2000); // Hide after 2 seconds
                }";
            ScriptManager.RegisterStartupScript(UpdatePanelProducts, UpdatePanelProducts.GetType(), "showAddCartPopup", script, true);
        }


        private void LoadProducts(bool resetPage = false)
        {
            List<Product> allProducts = (List<Product>)Application["products"];
            if (allProducts == null)
            {
                rptProductList.DataSource = null;
                rptProductList.DataBind();
                litNoProducts.Visible = true;
                litPagination.Text = "";
                return;
            }

            IEnumerable<Product> filteredProducts = allProducts;

            List<string> selectedCategoryIds = cblCategories.Items.Cast<ListItem>()
                                                 .Where(li => li.Selected)
                                                 .Select(li => li.Value)
                                                 .ToList();
            if (selectedCategoryIds.Any())
            {
                List<int> filterCategoryIntIds = selectedCategoryIds.Select(int.Parse).ToList();
                filteredProducts = filteredProducts.Where(p => filterCategoryIntIds.Contains(p.CategoryId));
            }


            List<string> selectedPriceRanges = cblPriceRanges.Items.Cast<ListItem>()
                                                 .Where(li => li.Selected)
                                                 .Select(li => li.Value)
                                                 .ToList();
            if (selectedPriceRanges.Any())
            {
                List<int> filterPriceIntRanges = selectedPriceRanges.Select(int.Parse).ToList();
                filteredProducts = filteredProducts.Where(p => {
                    bool match = false;
                    if (filterPriceIntRanges.Contains(1) && p.Price < 500000) match = true;
                    if (filterPriceIntRanges.Contains(2) && p.Price >= 500000 && p.Price <= 1000000) match = true;
                    if (filterPriceIntRanges.Contains(3) && p.Price > 1000000) match = true;
                    return match;
                });
            }

            int totalItems = filteredProducts.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / PageSize);
            int currentPage = 1;

            if (!resetPage && !string.IsNullOrEmpty(Request.QueryString["page"]))
            {
                int.TryParse(Request.QueryString["page"], out currentPage);
            }


            if (currentPage < 1) currentPage = 1;
            if (currentPage > totalPages && totalPages > 0) currentPage = totalPages;

            var pagedProducts = filteredProducts.Skip((currentPage - 1) * PageSize).Take(PageSize);

            rptProductList.DataSource = pagedProducts;
            rptProductList.DataBind();
            litNoProducts.Visible = !pagedProducts.Any();


            litPagination.Text = GeneratePaginationHtml(currentPage, totalPages);

            UpdateBrowserUrlAfterFilter(currentPage);

        }

        private string GeneratePaginationHtml(int currentPage, int totalPages)
        {
            if (totalPages <= 1) return string.Empty;

            StringBuilder paginationHtml = new StringBuilder("<ul class='pagination'>");

            List<string> selectedCategoryIds = cblCategories.Items.Cast<ListItem>()
                                                .Where(li => li.Selected)
                                                .Select(li => li.Value)
                                                .ToList();
            List<string> selectedPriceRanges = cblPriceRanges.Items.Cast<ListItem>()
                                                  .Where(li => li.Selected)
                                                  .Select(li => li.Value)
                                                  .ToList();

            var queryParams = HttpUtility.ParseQueryString(string.Empty);
            if (selectedCategoryIds.Any())
            {
                queryParams["cat"] = string.Join(",", selectedCategoryIds);
            }
            if (selectedPriceRanges.Any())
            {
                queryParams["price"] = string.Join(",", selectedPriceRanges);
            }

            string baseQuery = queryParams.ToString();

            string prevQuery = baseQuery + (string.IsNullOrEmpty(baseQuery) ? "" : "&") + $"page={(currentPage - 1)}";
            string prevUrl = $"Products.aspx?{prevQuery}";
            paginationHtml.Append($"<li class='page-item {(currentPage == 1 ? "disabled" : "")}'>");
            paginationHtml.Append($"<a class='page-link' href='{(currentPage == 1 ? "#" : prevUrl)}' aria-label='Previous'><span aria-hidden='true'>&laquo;</span></a></li>");

            for (int i = 1; i <= totalPages; i++)
            {
                string pageQuery = baseQuery + (string.IsNullOrEmpty(baseQuery) ? "" : "&") + $"page={i}";
                string pageUrl = $"Products.aspx?{pageQuery}";
                paginationHtml.Append($"<li class='page-item {(i == currentPage ? "active" : "")}'>");
                paginationHtml.Append($"<a class='page-link' href='{pageUrl}'>{i}</a></li>");
            }

            string nextQuery = baseQuery + (string.IsNullOrEmpty(baseQuery) ? "" : "&") + $"page={(currentPage + 1)}";
            string nextUrl = $"Products.aspx?{nextQuery}";
            paginationHtml.Append($"<li class='page-item {(currentPage == totalPages ? "disabled" : "")}'>");
            paginationHtml.Append($"<a class='page-link' href='{(currentPage == totalPages ? "#" : nextUrl)}' aria-label='Next'><span aria-hidden='true'>&raquo;</span></a></li>");

            paginationHtml.Append("</ul>");
            return paginationHtml.ToString();
        }

        private void UpdateBrowserUrlAfterFilter(int currentPage)
        {
            List<string> selectedCategoryIds = cblCategories.Items.Cast<ListItem>()
                                                .Where(li => li.Selected)
                                                .Select(li => li.Value)
                                                .ToList();
            List<string> selectedPriceRanges = cblPriceRanges.Items.Cast<ListItem>()
                                                  .Where(li => li.Selected)
                                                  .Select(li => li.Value)
                                                  .ToList();

            var queryParams = HttpUtility.ParseQueryString(string.Empty);
            if (selectedCategoryIds.Any())
            {
                queryParams["cat"] = string.Join(",", selectedCategoryIds);
            }
            if (selectedPriceRanges.Any())
            {
                queryParams["price"] = string.Join(",", selectedPriceRanges);
            }
            if (currentPage > 1)
            {
                queryParams["page"] = currentPage.ToString();
            }


            string newUrl = "Products.aspx";
            if (queryParams.HasKeys())
            {
                newUrl += "?" + queryParams.ToString();
            }

            string script = $"if (history.pushState) {{ history.pushState(null, null, '{ResolveUrl(newUrl)}'); }}";
            ScriptManager.RegisterStartupScript(UpdatePanelProducts, UpdatePanelProducts.GetType(), "updateUrl", script, true);
        }
    }
}