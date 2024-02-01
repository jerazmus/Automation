using Automation.Playwright.Core.Data.Models;
using Microsoft.Playwright;

namespace Automation.Playwright.Core.UI.Pages
{
    public class CartPage : PageBase
    {
        public ILocator CartItemContainer
            => Page.Locator(".cart_item");
        public ILocator CartItemName
            => Page.Locator(".inventory_item_name");
        public ILocator CartItemDescription
            => Page.Locator(".inventory_item_desc");
        public ILocator CartItemPrice
            => Page.Locator(".inventory_item_price");
        private ILocator ContinueShoppingButton
            => Page.Locator("[data-test='continue-shopping']");
        private ILocator CheckoutButton
            => Page.Locator("[data-test='checkout']");

        public CartPage(IPage page) : base(page) { }

        public async Task RemoveFromCartAsync(Product product)
            => await CartItemContainer
                .Filter(new () { Has = Page.Locator($"text='{product.Name}'") })
                .GetByRole(AriaRole.Button)
                .Filter(new () { HasText = "Remove" })
                .ClickAsync();

        public async Task<ProductsPage> ContinueShoppingAsync()
        {
            await ContinueShoppingButton.ClickAsync();
            return new ProductsPage(Page);
        }

        public async Task<CheckoutPage> CheckoutAsync()
        {
            await CheckoutButton.ClickAsync();
            return new CheckoutPage(Page);
        }
    }
}
