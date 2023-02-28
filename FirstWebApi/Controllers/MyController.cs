using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstWebApi.Controllers
{
    [RoutePrefix("api/say")]
    public class MyController : ApiController
    {
        [Route("world")]
        [HttpGet()]
        public IHttpActionResult SayHelloWorld()
        {
            return Ok("Hello World !!");
        }

        [Route("doworld")]
        [HttpPost()]
        public IHttpActionResult DoHelloWorld()
        {
            return Ok("Do Hello World !!");
        }

        [Route("who")]
        [HttpPost()]
        public IHttpActionResult HelloWho([FromBody]string who)
        {
            return Ok("Hello " + who);
        }

        [Route("employee")]
        [HttpPost()]
        public IHttpActionResult HelloEmp(Models.Employee employee)
        {
            return Ok(employee);
        }
    }
}