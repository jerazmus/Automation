using Microsoft.Playwright;

namespace Automation.Playwright.Core.UI.Pages.Checkout
{
    public class CheckoutPage : PageBase
    {
        private ILocator FirstName
            => Page.GetByRole(AriaRole.Textbox).First;

        private ILocator LastName
            => Page.GetByRole(AriaRole.Textbox).Nth(1);

        private ILocator ZipCode
            => Page.GetByRole(AriaRole.Textbox).Nth(2);

        public ILocator ContinueButton
            => Page.Locator("[data-test='continue']");

        public ILocator FinishButton
            => Page.Locator("[data-test='finish']");

        public ILocator CheckoutHeader
            => Page.Locator(".complete-header");

        public CheckoutPage(IPage page) : base(page) { }

        public async Task FillInformationAndContinueAsync(string firstName, string lastName, string zipCode)
        {
            await FirstName.FillAsync(firstName);
            await LastName.FillAsync(lastName);
            await ZipCode.FillAsync(zipCode);
            await ContinueButton.ClickAsync();
        }
    }
}
