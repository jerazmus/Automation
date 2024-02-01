using Automation.Playwright.Core.UI.Pages;
using Microsoft.Playwright;

namespace Automation.Playwright.Core.UI.Components
{
    public class Sidebar
    {
        private ILocator SidebarPanel
            => Page.Locator(".bm-menu-wrap");
        private ILocator SidebarOpenButton
            => Page.GetByRole(AriaRole.Button)
                   .Filter(new () { HasText = "Open Menu" });
        private ILocator SidebarCloseButton
            => Page.GetByRole(AriaRole.Button)
                   .Filter(new () { HasText = "Close Menu" });
        private ILocator AllItemsLink
            => Page.Locator("#inventory_sidebar_link");
        public ILocator AboutLink
            => Page.Locator("#about_sidebar_link");
        private ILocator LogoutLink
            => Page.Locator("#logout_sidebar_link");
        public ILocator ResetLink
            => Page.Locator("#reset_sidebar_link");

        private IPage Page;

        public Sidebar(IPage page)
        {
            Page = page;
        }

        public async Task OpenCloseAsync()
        {
            var hidden = bool.Parse(SidebarPanel.GetAttributeAsync("aria-hidden").Result);
            var task = hidden
                ? SidebarOpenButton.ClickAsync()
                : SidebarCloseButton.ClickAsync();
            await task;
        }

        public async Task<ProductsPage> OpenProductsPageAsync()
        {
            await AllItemsLink.ClickAsync();
            return new ProductsPage(Page);
        }

        public async Task<LoginPage> LogoutAsync()
        {
            await LogoutLink.ClickAsync();
            return new LoginPage(Page);
        }
    }
}
