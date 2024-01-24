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
        public ILocator CheckoutButton
            => Page.Locator("[data-test='checkout']");

        public CartPage(IPage page) : base(page) { }

        public async Task<CheckoutPage> CheckoutAsync()
        {
            await CheckoutButton.ClickAsync();
            return new CheckoutPage(Page);
        }
    }
}
