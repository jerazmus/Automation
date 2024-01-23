using Microsoft.Playwright;

namespace Automation.Playwright.Core.UI.Pages
{
    public class ProductsPage : PageBase
    {
        public ILocator ProductNameLabel
            => Page.Locator(".inventory_item_name ");

        public ILocator AddToCartButton
            => Page.GetByRole(AriaRole.Button)
                   .Filter(new() { HasText = "Add to cart" });

        public ProductsPage(IPage page) : base(page) { }

        public async Task AddProductsAsync(int count)
        {
            for (int i = 0; i < count; i++)
            {
                await AddToCartButton.First.ClickAsync();
            }
        }

        public async Task<ProductDetailsPage> OpenProductDetailsAsync(string productName)
        {
            await ProductNameLabel.Filter(new()
            {
                HasText = productName
            }).ClickAsync();
            return new ProductDetailsPage(Page);
        }
    }
}
