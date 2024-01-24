using Automation.Playwright.Core.Data;
using Automation.Playwright.Core.Extensions;
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
            var productsPage = await OpenLoginAsync();

            // When & Then - open product's details
            var productDetailsPage = await productsPage.OpenProductDetailsAsync(product.Name);
            await Expect(productDetailsPage.ProductDetailsName).ToHaveTextAsync(product.Name);
            await Expect(productDetailsPage.ProductDetailsDescription).ToHaveTextAsync(product.Description);
            await Expect(productDetailsPage.ProductDetailsPrice).ToHaveTextAsync(product.Price);

            // When & Then - go back to products page
            productsPage = await productDetailsPage.GoBackToProductsPageAsync();
            await ExpectURLAsync("inventory", true);
        }

        [Test]
        public async Task Product_Sort()
        {
            // Given
            var products = DataProvider.Products;
            var productsPage = await OpenLoginAsync();

            // When & Then - default sort (name A to Z)
            await products.ExpectSortedAsync(productsPage);

            // When & Then - change sort (name Z to A)
            products = products.OrderByName(true);
            await productsPage.SortByAsync("Name (Z to A)");
            await products.ExpectSortedAsync(productsPage);

            // When & Then - change sort (price low to high)
            products = products.OrderByPrice();
            await productsPage.SortByAsync("Price (low to high)");
            await products.ExpectSortedAsync(productsPage);

            // When & Then - change sort (price high to low)
            products = products.OrderByPrice(true);
            await productsPage.SortByAsync("Price (high to low)");
            await products.ExpectSortedAsync(productsPage);
        }

        [Test]
        public async Task Product_Add()
        {
            // Given
            var products = DataProvider.Products.GetRange(0, 2);
            var productsPage = await OpenLoginAsync();

            // When & Then - add product to cart from products page
            await productsPage.AddProductToCartAsync(products[0]);
            await Expect(productsPage.Header.CartItemCount).ToHaveTextAsync("1");

            // When & Then - add product to cart from product's details page
            var productDetailsPage = await productsPage.OpenProductDetailsAsync(products[1].Name);
            await productDetailsPage.AddToCartButton.ClickAsync();
            await Expect(productDetailsPage.Header.CartItemCount).ToHaveTextAsync("2");

            // When & Then - check if added products are visible in cart
            var cartPage = await productDetailsPage.Header.OpenCartAsync();
            await Expect(cartPage.CartItemContainer).ToHaveCountAsync(2);
            await products.ExpectVisibleAsync(cartPage);
        }
    }
}
