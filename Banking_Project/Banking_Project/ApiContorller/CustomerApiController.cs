using Banking_Project.ApiServices;
using Banking_Project.Models;
using Banking_Project.Services;
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
    public class CustomerApiController : ApiController
    {
        private readonly CustomerApiServices _customerService = new CustomerApiServices();
        private readonly TransactionService _transactionService = new TransactionService();

        [HttpPost]
        [Route("api/customer/postlogin")]
        public Task<HttpResponseMessage> PostLogin([FromBody]CustomerModel viewModel)
        {
            var customer = _customerService.Login(viewModel);
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(customer))
            };
            return Task.FromResult(response);
        }


        [HttpPost]
        [Route("api/customer/PostAccountByCustomerId")]
        public Task<HttpResponseMessage> PostAccountByCustomerId([FromBody]String id)
        {
            AccountModel account = _customerService.AccountByCustomerId(id);
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(account))
            };
            return Task.FromResult(response);
        }

        [HttpPost]
        [Route("api/customer/PostProfileEdit")]
        public Task<HttpResponseMessage> PostProfileEdit([FromBody]CustomerProfileEditModel viewModel)
        {

            CustomerProfileEditModel model = _customerService.PostCustomerProfileEdit(viewModel);
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(model))
            };
            return Task.FromResult(response);
        }

        [HttpPost]
        [Route("api/customer/PostCustomerPasswordEdit")]
        public Task<HttpResponseMessage> PostCustomerPasswordEdit([FromBody]CustomerPasswordChangeModel viewModel)
        {

            CustomerPasswordChangeModel model = _customerService.PostCustomerPasswordEdit(viewModel);
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(model))
            };
            return Task.FromResult(response);
        }

        [HttpPost]
        [Route("api/customer/posttransfer")]
        public Task<HttpResponseMessage> PostTransfer([FromBody]CreateTransfer viewModel)
        {
            var transfer = _transactionService.Transfer(viewModel);
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(transfer))
            };
            return Task.FromResult(response);
        }
        [HttpPost]
        [Route("api/customer/posttopUp")]
        public Task<HttpResponseMessage> PostTopUp([FromBody]TopUp viewModel)
        {
            var topup = _transactionService.TopUp(viewModel);
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(topup))
            };
            return Task.FromResult(response);
        }
        //public IHttpActionResult GetTransactionByCustomerId(string customerid = "cus0000001")
        //{
        //    var transactions = _transactionService.TransactionByCustomerId(customerid);
        //    if (transactions.lstTransactionByCustomer.Count() == 0)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return Ok(transactions);
        //    }
        //}
        [HttpPost]
        [Route("api/customer/GetTransactionByCustomerId")]
        public Task<HttpResponseMessage> GetTransactionByCustomerId([FromBody]string customerid)
        {

            TransactionByCustomerModel model = _customerService.TransactionByCustomerId(customerid);
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(model))
            };
            return Task.FromResult(response);
        }

        //topUpList
        [HttpPost]
        [Route("api/customer/topuplist")]
        public Task<HttpResponseMessage> TopUpList([FromBody]string customerid)
        {

           TopUpListModel model = _customerService.TopUpList(customerid);
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(model))
            };
            return Task.FromResult(response);
        }

        [HttpPost]
        [Route("api/customer/TransactionSummery")]
        public Task<HttpResponseMessage> TransactionSummery([FromBody]string customerid)
        {
            TransactionSummeryModel model = _customerService.TransactionSummery(customerid);
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(model))
            };
            return Task.FromResult(response);
        }

    }
}
