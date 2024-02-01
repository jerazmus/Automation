using Automation.Playwright.Core.Data;
using Automation.Playwright.Core.UI;
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
            var products = DataProvider.Products;
            var productsPage = await OpenAuthAsync();

            // When & Then - reset app state
            await productsPage.AddProductToCartAsync(products[0]);
            await Expect(productsPage.Header.CartItemCount).ToContainTextAsync("1");

            var cartPage = await productsPage.Header.OpenCartAsync();
            await cartPage.Sidebar.OpenCloseAsync();
            await cartPage.Sidebar.ResetLink.ClickAsync();
            await Expect(cartPage.Header.CartItemCount).Not.ToBeVisibleAsync();

            // When & Then - navigate to products page
            productsPage = await cartPage.Sidebar.OpenProductsPageAsync();
            await ExpectURLAsync("inventory", true);

            // When & Then - navigate to about page
            await productsPage.Sidebar.OpenCloseAsync();
            await productsPage.Sidebar.AboutLink.ClickAsync();
            await ExpectURLAsync("https://saucelabs.com/");

            // When & Then - logout
            await Page.GoBackAsync();
            await productsPage.Sidebar.OpenCloseAsync();
            await productsPage.Sidebar.LogoutAsync();
            await ExpectURLAsync("/");
        }
    }
}
