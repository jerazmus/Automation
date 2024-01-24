using Automation.Playwright.Core.UI.Pages;
using Microsoft.Playwright;


namespace Automation.Playwright.Core.UI.Components
{
    public class Header
    {
        public ILocator CartButton
            => Page.Locator("#shopping_cart_container");
        public ILocator CartItemCount
            => Page.Locator(".shopping_cart_badge");

        private IPage Page;

        public Header(IPage page) 
        {
            Page = page;
        }

        public async Task<CartPage> OpenCartAsync()
        {
            await CartButton.ClickAsync();
            return new CartPage(Page);
        }
    }
}
