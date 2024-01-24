using Automation.Playwright.Core.Data.Models;

namespace Automation.Playwright.Core.Data
{
    public static class UserProvider
    {
        public static User StandardUser
            => new("standard_user");
        public static User LockedOutUser
            => new("locked_out_user");
        public static User ProblemUser
            => new("problem_user");
        public static User PerformanceGlitchUser
            => new("performance_glitch_user");
    }
}
