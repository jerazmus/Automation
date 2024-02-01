namespace Automation.Playwright.Core.Data.Models
{
    public class Product
    {
        public int Index { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Price { get; init; }

        public Product(
            int index,
            string productName,
            string productDescription,
            string productPrice)
        {
            Index = index;
            Name = productName;
            Description = productDescription;
            Price = productPrice;
        }
    }
}
