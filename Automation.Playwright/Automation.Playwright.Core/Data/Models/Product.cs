namespace Automation.Playwright.Core.Data.Models
{
    public class Product
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string Price { get; init; }

        public Product(
            string productName,
            string productDescription,
            string productPrice)
        {
            Name = productName;
            Description = productDescription;
            Price = productPrice;
        }
    }
}
