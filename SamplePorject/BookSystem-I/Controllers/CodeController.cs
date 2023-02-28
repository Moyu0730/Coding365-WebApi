using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookSystem.Controllers
{
    [RoutePrefix("api/code")]
    public class CodeController : ApiController
    {
        [Route("bookstatus")]
        [HttpPost()]
        public IHttpActionResult GetBookStatusData()
        {
            List<Models.Code> result = new List<Models.Code>();
            result.Add(new Models.Code() { Value = "A", Text = "可以借出" });
            result.Add(new Models.Code() { Value = "B", Text = "已借出" });
            result.Add(new Models.Code() { Value = "C", Text = "不可借出" });
            result.Add(new Models.Code() { Value = "U", Text = "已借出(未領)" });

            return Ok(result);
        }
    }
}