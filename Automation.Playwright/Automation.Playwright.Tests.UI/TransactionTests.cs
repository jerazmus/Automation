using Automation.Playwright.Core.UI;
using NUnit.Framework;

namespace Automation.Playwright.Tests.UI
{
    [Parallelizable(ParallelScope.Self)]
    public class TransactionTests : TestBase
    {
        [Test]
        public async Task Transaction_Finalize()
        {
            // Given
            var numberOfItems = 2;
            var firstName = "test_firstName";
            var lastName = "test_lastName";
            var zipCode = "test_zipCode";

            // When & Then - login correctly
            var loginPage = await OpenAsync();
            var productsPage = await loginPage.LoginAsync();
            await ExpectURLAsync("inventory", true);

            // When & Then - add items to cart
            await productsPage.AddItemsAsync(numberOfItems);
            var cartPage = await productsPage.OpenCartAsync();
            await Expect(cartPage.CartItem).ToHaveCountAsync(numberOfItems);

            // When & Then - checkout and provide information
            var checkoutPage = await cartPage.CheckoutAsync();
            await ExpectURLAsync("checkout-step-one", true);
            await checkoutPage.FillInformationAndContinueAsync(firstName, lastName, zipCode);
            await ExpectURLAsync("checkout-step-two", true);

            // When & Then - finalize transaction
            await checkoutPage.FinishButton.ClickAsync();
            await ExpectURLAsync("checkout-complete", true);
            await Expect(checkoutPage.CheckoutHeader).ToHaveTextAsync("Thank you for your order!");
        }
    }
}
