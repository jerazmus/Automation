using Automation.Playwright.Core.Data.Models;
using Microsoft.Playwright;

namespace Automation.Playwright.Core.UI.Pages
{
    public class ProductsPage : PageBase
    {
        public ILocator ProductContainer
            => Page.Locator(".inventory_item_description");
        public ILocator ProductName
            => Page.Locator(".inventory_item_name ");
        public ILocator ProductDescription
            => Page.Locator(".inventory_item_desc");
        public ILocator ProductPrice
            => Page.Locator(".inventory_item_price");
        public ILocator SortDropdown
            => Page.Locator("[data-test='product_sort_container']");

        public ProductsPage(IPage page) : base(page) { }

        public async Task AddProductToCartAsync(Product product)
        {
            await ProductContainer
                .Filter(new () { Has = Page.Locator($"text='{product.Name}'") })
                .GetByRole(AriaRole.Button)
                .Filter(new() { HasText = "Add to cart" })
                .ClickAsync();
        }

        public async Task<ProductDetailsPage> OpenProductDetailsAsync(string productName)
        {
            await ProductName
                .Filter(new () { HasText = productName })
                .ClickAsync();
            return new ProductDetailsPage(Page);
        }

        public async Task SortByAsync(string sort)
            => await SortDropdown.SelectOptionAsync(sort);
    }
}
