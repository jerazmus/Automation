using Automation.Playwright.Core.Data;
using Automation.Playwright.Core.Playwright;
using Automation.Playwright.Core.UI.Pages;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace Automation.Playwright.Core.UI
{
    public abstract class TestBase : Browser
    {
        [SetUp]
        protected async Task SetUp()
        {
            DataProvider.Initialize();
            await Page.Context.Tracing.StartAsync(new()
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }

        [TearDown]
        protected async Task TearDown()
        {
            var test = TestContext.CurrentContext;
            if (test.Result.Outcome.Status != NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                await Page.Context.Tracing.StopAsync(new()
                {
                    Path = $"{test.Test.Name}.zip"
                });
            }

        }

        protected async Task<LoginPage> OpenAsync(string? url = null)
        {
            await Page.GotoAsync("/");
            return new LoginPage(Page);
        }

        protected async Task ExpectURLAsync(string url = "", bool partial = false)
        {
            var task = partial
                ? Expect(Page).ToHaveURLAsync(new Regex($"^.*{url}.*$"))
                : Expect(Page).ToHaveURLAsync(url);
            await task;
        }
    }
}
