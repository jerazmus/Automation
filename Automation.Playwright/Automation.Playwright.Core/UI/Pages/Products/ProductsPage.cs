using Automation.Playwright.Core.UI.Pages.Cart;
using Microsoft.Playwright;

namespace Automation.Playwright.Core.UI.Pages.Products
{
    public class ProductsPage : PageBase
    {
        private ILocator CartButton
            => Page.Locator("#shopping_cart_container");

        public ProductsPage(IPage page) : base(page) { }

        public async Task AddItemsAsync(int count)
        {
            var itemsButtons = Page.GetByRole(AriaRole.Button)
                                   .Filter(new() { HasText = "Add to cart" });
            for (int i = 0; i < count; i++)
            {
                await itemsButtons.First.ClickAsync();
            }
        }

        public async Task<CartPage> OpenCartAsync()
        {
            await CartButton.ClickAsync();
            return new CartPage(Page);
        }
    }
}
