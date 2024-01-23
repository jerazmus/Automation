using Automation.Playwright.Core.UI;
using Automation.Playwright.Core.UI.Components;
using NUnit.Framework;

namespace Automation.Playwright.Tests.UI
{
    [Parallelizable(ParallelScope.Self)]
    public class SidebarTests : TestBase
    {
        [Test]
        public async Task Sidebar_Navigate()
        {
            // Given
            var loginPage = await OpenAsync();
            var productsPage = await loginPage.LoginAsync();

            // When & Then - reset app state & navigate to products page
            await productsPage.AddProductsAsync(6);
            await Expect(productsPage.Header.CartItemCount).ToContainTextAsync("6");

            var cartPage = await productsPage.Header.OpenCartAsync();
            await cartPage.Sidebar.OpenCloseAsync();
            await cartPage.Sidebar.ResetLink.ClickAsync();
            await Expect(cartPage.Header.CartItemCount).Not.ToBeVisibleAsync();

            productsPage = await cartPage.Sidebar.OpenProductsPageAsync();
            await Expect(productsPage.AddToCartButton).ToHaveCountAsync(6);
            await ExpectURLAsync("inventory", true);

            // When & Then - navigate to about page
            await productsPage.Sidebar.OpenCloseAsync();
            await productsPage.Sidebar.AboutLink.ClickAsync();
            await ExpectURLAsync("https://saucelabs.com/");
            await Page.GoBackAsync();

            // When & Then - logout
            await productsPage.Sidebar.OpenCloseAsync();
            await productsPage.Sidebar.LogoutAsync();
            await ExpectURLAsync("/");
        }
    }
}
