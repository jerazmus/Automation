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
        public async Task Product_Add()
        {
            // Given
            var products = DataProvider.Products.GetRange(0, 2);
            var productsPage = await OpenAuthAsync();

            // When & Then - add product to cart from products page
            await productsPage.AddProductToCartAsync(products[0]);
            await Expect(productsPage.Header.CartItemCount).ToHaveTextAsync("1");

            // When & Then - add product to cart from product's details page
            var productDetailsPage = await productsPage.OpenProductDetailsAsync(products[1]);
            await productDetailsPage.AddToCartButton.ClickAsync();
            await Expect(productDetailsPage.Header.CartItemCount).ToHaveTextAsync(products.Count.ToString());

            // When & Then - check if added products are visible in cart
            var cartPage = await productDetailsPage.Header.OpenCartAsync();
            await products.ExpectVisibleAsync(cartPage);
        }

        [Test]
        public async Task Product_Details()
        {
            // Given
            var product = DataProvider.Products[0];
            var productsPage = await OpenAuthAsync();

            // When & Then - open product's details
            var productDetailsPage = await productsPage.OpenProductDetailsAsync(product);
            await product.ExpectDetailsAsync(productDetailsPage);

            // When & Then - go back to products page
            await productDetailsPage.GoBackToProductsPageAsync();
            await ExpectURLAsync("inventory", true);
        }

        [Test]
        public async Task Product_Remove()
        {
            // Given
            var products = DataProvider.Products.GetRange(0, 3);
            var productsPage = await OpenAuthAsync(products);
            var cartPage = await productsPage.Header.OpenCartAsync();

            // When & Then - remove item directly from cart
            await cartPage.RemoveFromCartAsync(products[2]);
            products.RemoveAt(2);
            await Expect(productsPage.Header.CartItemCount).ToHaveTextAsync(products.Count.ToString());
            await products.ExpectVisibleAsync(cartPage);

            // When & Then - remove item from products page
            productsPage = await cartPage.ContinueShoppingAsync();
            await productsPage.RemoveProductFromCartAsync(products[1]);
            products.RemoveAt(1);
            cartPage = await productsPage.Header.OpenCartAsync();
            await Expect(productsPage.Header.CartItemCount).ToHaveTextAsync(products.Count.ToString());
            await products.ExpectVisibleAsync(cartPage);

            // When & Then - remove item from product's details page
            productsPage = await cartPage.ContinueShoppingAsync();
            var productsDetailsPage = await productsPage.OpenProductDetailsAsync(products[0]);
            await productsDetailsPage.RemoveFromCartButton.ClickAsync();
            products.RemoveAt(0);
            cartPage = await productsDetailsPage.Header.OpenCartAsync();
            await Expect(cartPage.Header.CartItemCount).Not.ToBeVisibleAsync();
            await products.ExpectVisibleAsync(cartPage);
        }

        [Test]
        public async Task Product_Sort()
        {
            // Given
            var products = DataProvider.Products;
            var productsPage = await OpenAuthAsync();

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
    }
}
