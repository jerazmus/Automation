using Automation.Playwright.Core.Data.Models;
using Microsoft.Playwright;

namespace Automation.Playwright.Core.UI.Pages
{
    public class ProductsPage : PageBase
    {
        private ILocator ProductContainer
            => Page.Locator(".inventory_item_description");
        public ILocator ProductName
            => Page.Locator(".inventory_item_name ");
        public ILocator ProductDescription
            => Page.Locator(".inventory_item_desc");
        public ILocator ProductPrice
            => Page.Locator(".inventory_item_price");
        private ILocator SortDropdown
            => Page.Locator("[data-test='product_sort_container']");

        public ProductsPage(IPage page) : base(page) { }

        public async Task AddProductToCartAsync(Product product)
            => await ProductContainer.Filter(new () { Has = Page.Locator($"text='{product.Name}'") })
                                     .GetByRole(AriaRole.Button)
                                     .Filter(new () { HasText = "Add to cart" })
                                     .ClickAsync();

        public async Task RemoveProductFromCartAsync(Product product)
            => await ProductContainer.Filter(new () { Has = Page.Locator($"text='{product.Name}'") })
                                     .GetByRole(AriaRole.Button)
                                     .Filter(new () { HasText = "Remove" })
                                     .ClickAsync();

        public async Task AddProductsToCartAsync(List<Product> products)
        {
            foreach (var product in products)
            {
                await AddProductToCartAsync(product);
            }
        }

        public async Task<ProductDetailsPage> OpenProductDetailsAsync(Product product)
        {
            await ProductName.Filter(new () { HasText = product.Name })
                             .ClickAsync();
            return new ProductDetailsPage(Page);
        }

        public async Task SortByAsync(string sort)
            => await SortDropdown.SelectOptionAsync(sort);
    }
}
