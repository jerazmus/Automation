using Microsoft.Playwright;

namespace Automation.Playwright.Core.UI.Pages
{
    public class ProductDetailsPage : PageBase
    {
        private ILocator BackToProductsButton
            => Page.Locator("[data-test='back-to-products']");
        private ILocator ProductDetailsContainer
            => Page.Locator(".inventory_details_desc_container");
        public ILocator ProductDetailsName
            => ProductDetailsContainer.Locator("div").First;
        public ILocator ProductDetailsDescription
            => ProductDetailsContainer.Locator("div").Nth(1);
        public ILocator ProductDetailsPrice
            => ProductDetailsContainer.Locator("div").Nth(2);
        public ILocator AddToCartButton
            => ProductDetailsContainer.GetByRole(AriaRole.Button)
                                      .Filter(new () { HasText = "Add to cart" });
        public ILocator RemoveFromCartButton
            => ProductDetailsContainer.GetByRole(AriaRole.Button)
                                      .Filter(new () { HasText = "Remove" });

        public ProductDetailsPage(IPage page) : base(page) { }

        public async Task<ProductsPage> GoBackToProductsPageAsync()
        {
            await BackToProductsButton.ClickAsync();
            return new ProductsPage(Page);
        }
    }
}
