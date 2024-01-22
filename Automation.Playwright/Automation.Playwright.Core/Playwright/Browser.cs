using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace Automation.Playwright.Core.Playwright
{
    public abstract class Browser : PageTest
    {
        public override BrowserNewContextOptions ContextOptions()
        {
            return new()
            {
                BaseURL = "https://www.saucedemo.com",
                ColorScheme = ColorScheme.Dark,
                Locale = "pl-PL",
                ViewportSize = new ViewportSize()
                { 
                    Width = 1920, 
                    Height = 1080
                }
            };
        }
    }
}
