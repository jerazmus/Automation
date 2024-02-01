using Automation.Playwright.Core.Data;
using Automation.Playwright.Core.UI;
using NUnit.Framework;

namespace Automation.Playwright.Tests.UI
{
    [Parallelizable(ParallelScope.Self)]
    public class LoginTests : TestBase
    {
        [Test]
        public async Task Login_Successful()
        {
            // Given
            var loginPage = await OpenAsync();

            // When & Then - login successfully
            await loginPage.LoginAsync(UserProvider.StandardUser);
            await ExpectURLAsync("inventory", true);
        }

        [Test]
        public async Task Login_Validation()
        {
            // Given
            var loginPage = await OpenAsync();

            // When & Then - empty username
            await loginPage.LoginButton.ClickAsync();
            await Expect(loginPage.ErrorMessage).ToHaveTextAsync(loginPage.EmptyUsernameErrorMessage);

            // WheN & Then - empty password
            await loginPage.FillCredentialsAsync("test");
            await loginPage.LoginButton.ClickAsync();
            await Expect(loginPage.ErrorMessage).ToHaveTextAsync(loginPage.EmptyPasswordErrorMessage);

            // When & Then - wrong credentials
            await loginPage.FillCredentialsAsync("test", "test");
            await loginPage.LoginButton.ClickAsync();
            await Expect(loginPage.ErrorMessage).ToHaveTextAsync(loginPage.WrongCredentialsErrorMessage);

            // When & Then - error message disappear
            await loginPage.ErrorMessageButton.ClickAsync();
            await Expect(loginPage.ErrorMessage).Not.ToBeVisibleAsync();
        }
    }
}
