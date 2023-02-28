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
        /// <summary>
        /// 取得借閱狀態代碼資料 API
        /// </summary>
        /// <returns></returns>
        [Route("bookstatus")]
        [HttpPost()]
        public IHttpActionResult GetBookStatusData()
        {
            Models.CodeService codeService = new Models.CodeService();

            var result = new Models.ApiResult<List<Models.Code>>
            {
                Status = true,
                Message = string.Empty,
                Data = codeService.GetBookStatusData()
            };

            return Ok(result.Data);
        }

        /// <summary>
        /// 取得使用者代碼資料 API
        /// </summary>
        /// <returns></returns>
        [Route("member")]
        [HttpPost()]
        public IHttpActionResult GetMemberData()
        {
            Models.CodeService codeService = new Models.CodeService();

            var result = new Models.ApiResult<List<Models.Code>>
            {
                Status = true,
                Message = string.Empty,
                Data = codeService.GetMemberData()
            };

            return Ok(result.Data);
        }

        //TODO : 請完成取得圖書類別的 API
        [Route("bookclass")]
        [HttpPost()]
        public IHttpActionResult GetBookClass()
        {
            Models.CodeService codeService = new Models.CodeService();

            var result = new Models.ApiResult<List<Models.Code>>
            {
                Status = true,
                Message = "Success",
                Data = codeService.GetBookClassData()
            };

            return Ok(result.Data);
        }
    }
}