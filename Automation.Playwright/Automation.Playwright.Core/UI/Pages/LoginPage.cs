using Microsoft.Playwright;

namespace Automation.Playwright.Core.UI.Pages
{
    public class LoginPage : PageBase
    {
        private ILocator UsernameInput
            => Page.Locator("[data-test='username']");

        private ILocator PasswordInput
            => Page.Locator("[data-test='password']");

        public ILocator LoginButton
            => Page.Locator("[data-test='login-button']");

        public ILocator ErrorMessage
            => Page.Locator("[data-test='error']");

        public ILocator ErrorMessageButton
            => ErrorMessage.GetByRole(AriaRole.Button);

        public string EmptyUsernameErrorMessage
            => "Epic sadface: Username is required";

        public string EmptyPasswordErrorMessage
            => "Epic sadface: Password is required";

        public string WrongCredentialsErrorMessage
            => "Epic sadface: Username and password do not match any user in this service";

        public LoginPage(IPage page) : base(page) { }

        public async Task<ProductsPage> LoginAsync(string username = "standard_user", string password = "secret_sauce")
        {
            await FillLoginInformationAndClickAsync(username, password);
            return new ProductsPage(Page);
        }

        public async Task FillLoginInformationAndClickAsync(string username = "", string password = "")
        {
            await UsernameInput.FillAsync(username);
            await PasswordInput.FillAsync(password);
            await LoginButton.ClickAsync();
        }
    }
}