using Automation.Playwright.Core.Data.Models;
using Automation.Playwright.Core.UI.Pages;
using Microsoft.Playwright;
using System.Globalization;

namespace Automation.Playwright.Core.Extensions
{
	public static class ProductsExtensions
	{
		public static List<Product> OrderByName(this List<Product> products, bool desc = false)
			=> desc
			? products.OrderByDescending(p => p.Name).ToList()
			: products.OrderBy(p => p.Name).ToList();

		public static List<Product> OrderByPrice(this List<Product> products, bool desc = false)
			=> desc
			? products.OrderByDescending(p => decimal.Parse(p.Price[1..], CultureInfo.InvariantCulture)).ThenBy(p => p.Name).ToList()
			: products.OrderBy(p => decimal.Parse(p.Price[1..], CultureInfo.InvariantCulture)).ThenBy(p => p.Name).ToList();

		public async static Task ExpectSortedAsync(this List<Product> products, ProductsPage productsPage)
		{
			for (var i = 0; i < products.Count; i++)
			{
				await Assertions.Expect(productsPage.ProductName.Nth(i)).ToHaveTextAsync(products[i].Name);
				await Assertions.Expect(productsPage.ProductDescription.Nth(i)).ToHaveTextAsync(products[i].Description);
				await Assertions.Expect(productsPage.ProductPrice.Nth(i)).ToHaveTextAsync(products[i].Price);
			}
		}

		public async static Task ExpectVisibleAsync(this List<Product>? products, CartPage cartPage)
		{
			await Assertions.Expect(cartPage.CartItemContainer).ToHaveCountAsync(products != null ? products.Count : 0);
			for (var i = 0; i < products?.Count; i++)
			{
				await Assertions.Expect(cartPage.CartItemName.Nth(i)).ToHaveTextAsync(products[i].Name);
				await Assertions.Expect(cartPage.CartItemDescription.Nth(i)).ToHaveTextAsync(products[i].Description);
				await Assertions.Expect(cartPage.CartItemPrice.Nth(i)).ToHaveTextAsync(products[i].Price);
			}
		}

		public async static Task ExpectDetailsAsync(this Product product, ProductDetailsPage productDetailsPage)
		{
			await Assertions.Expect(productDetailsPage.ProductDetailsName).ToHaveTextAsync(product.Name);
			await Assertions.Expect(productDetailsPage.ProductDetailsDescription).ToHaveTextAsync(product.Description);
			await Assertions.Expect(productDetailsPage.ProductDetailsPrice).ToHaveTextAsync(product.Price);
		}
	}
}
