namespace Automation.Playwright.Core.Data.Models
{
    public class User
    {
        public string Username { get; init; }
        public string Password { get; init; }

        public User(string username)
        {
            Username = username;
            Password = "secret_sauce";
        }
    }
}
