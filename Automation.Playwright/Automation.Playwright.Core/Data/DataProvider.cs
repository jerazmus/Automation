using Automation.Playwright.Core.Data.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Automation.Playwright.Core.Data
{
    public static class DataProvider
    {
        public static List<Product> Products;

        public static void Initialize()
        {
            Products = GetTestData().Products;
        }

        public static TestData GetTestData()
        {
            using (StreamReader reader = new StreamReader(TestContext.CurrentContext.TestDirectory + @"\Data\testData.json"))
            {
                return JsonConvert.DeserializeObject<TestData>(reader.ReadToEnd());
            }
        }
    }
}
