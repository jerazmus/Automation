using Automation.Playwright.Core.UI.Pages.Checkout;
using Microsoft.Playwright;

namespace Automation.Playwright.Core.UI.Pages.Cart
{
    public class CartPage : PageBase
    {
        public ILocator CartItem
            => Page.Locator(".cart_item");

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
