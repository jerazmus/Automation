using Automation.Playwright.Core.Data;
using Automation.Playwright.Core.UI;
using NUnit.Framework;

namespace Automation.Playwright.Tests.UI
{
    [Parallelizable(ParallelScope.Self)]
    public class ProductTests : TestBase
    {
        [Test]
        public async Task Product_Details()
        {
            // Given
            var product = DataProvider.Products[0];

            var loginPage = await OpenAsync();
            var productsPage = await loginPage.LoginAsync();

            // When & Then - open product's details
            var productDetailsPage = await productsPage.OpenProductDetailsAsync(product.Name);
            await Expect(productDetailsPage.ProductDetailsName).ToHaveTextAsync(product.Name);
            await Expect(productDetailsPage.ProductDetailsDescription).ToHaveTextAsync(product.Description);
            await Expect(productDetailsPage.ProductDetailsPrice).ToHaveTextAsync(product.Price);

            // When & Then - go back to products page
            productsPage = await productDetailsPage.GoBackToProductsPageAsync();
            await ExpectURLAsync("inventory", true);
        }
    }
}
