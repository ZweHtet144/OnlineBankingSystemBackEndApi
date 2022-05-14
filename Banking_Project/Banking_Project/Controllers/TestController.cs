using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Banking_Project.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public async Task<ActionResult> GetCustomer()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:2333/");
                var response = client.GetAsync("api/CustomerApi/GetCustomerInfo").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var dataReturn = JsonConvert.DeserializeObject<CustomerModel>(data);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }
        public class CustomerModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string PhoneNo { get; set; }
            public List<CustomerModel> CustomerList { get; set; }
        }
    }
}