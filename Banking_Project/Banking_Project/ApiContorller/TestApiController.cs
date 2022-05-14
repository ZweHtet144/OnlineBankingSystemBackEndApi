using Banking_Project.ApiServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Banking_Project.ApiContorller
{
    public class TestApiController : ApiController
    {
        
        private readonly TestService _services = new TestService();
        //GET
        [Route("api/CustomerApi/GetCustomerInfo")]
        [HttpGet]
        public Task<HttpResponseMessage> GetCustomerInfo()
        {
            var model = _services.GetCustomerInfo();
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(model))
            };
            return Task.FromResult(response);
        }
    }
}
