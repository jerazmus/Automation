using Automation.Playwright.Core.UI.Pages.Products;
using Microsoft.Playwright;

namespace Automation.Playwright.Core.UI.Pages.Login
{
    public class LoginPage : PageBase
    {
        private ILocator Username
            => Page.GetByRole(AriaRole.Textbox).First;

        private ILocator Password
            => Page.GetByRole(AriaRole.Textbox).Nth(1);

        private ILocator LoginButton
            => Page.Locator("[data-test='login-button']");

        public LoginPage(IPage page) : base(page) { }

        public async Task<ProductsPage> LoginAsync(string username = "standard_user", string password = "secret_sauce")
        {
            await Username.FillAsync(username);
            await Password.FillAsync(password);
            await LoginButton.ClickAsync();
            return new ProductsPage(Page);
        }
    }
}