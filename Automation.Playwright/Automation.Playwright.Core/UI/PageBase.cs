using Automation.Playwright.Core.UI.Components;
using Microsoft.Playwright;

namespace Automation.Playwright.Core.UI
{
    public abstract class PageBase
    {
        public IPage Page { get; init; }

        public Sidebar Sidebar;

        public Components.Header Header;

        protected PageBase(IPage page)
        {
            Page = page;
            Sidebar = new Sidebar(page);
            Header = new Components.Header(page);
        }
    }
}
