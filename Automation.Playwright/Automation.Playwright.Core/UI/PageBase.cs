using Microsoft.Playwright;

namespace Automation.Playwright.Core.UI
{
    public abstract class PageBase
    {
        public IPage Page { get; init; }

        protected PageBase(IPage page)
        {
            Page = page;
        }
    }
}
