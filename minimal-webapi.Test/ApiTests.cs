using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;

namespace minimal_webapi.Test
{
    [TestClass]
    public class ApiTests
    {
        private HttpClient _httpClient;

        public ApiTests()
        {
            var webApplicationFactory = new WebApplicationFactory<Program>();
            _httpClient = webApplicationFactory.CreateDefaultClient();
        }

        [TestMethod]
        public async Task DefaultRoute_ReturnsHelloWorld()
        {
            var response = await _httpClient.GetAsync("");
            var result = await response.Content.ReadAsStringAsync();
            
            Assert.AreEqual("Hello World!", result);
        }

        [TestMethod]
        public async Task Sum_Returns16For10And6()
        {
            var response = await _httpClient.GetAsync("/sum?n1=10&n2=6");
            var result = await response.Content.ReadAsStringAsync();
            var intResult = int.Parse(result);
            
            Assert.AreEqual(16, intResult);
        }


        [DataTestMethod]
        [DataRow(10, 6, 16)]
        [DataRow(2, 4, 6)]
        public async Task Sum_ReturnsCorrectValue(int? n1, int? n2, int? expectedResult)
        {
            var response = await _httpClient.GetAsync($"/sum?n1={n1}&n2={n2}");
            var result = await response.Content.ReadAsStringAsync();
            var intResult = int.Parse(result);

            Assert.AreEqual(expectedResult, intResult);
        }
    }
}
