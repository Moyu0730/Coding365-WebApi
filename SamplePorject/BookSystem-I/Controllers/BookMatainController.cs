using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookSystem.Controllers
{
    [RoutePrefix("api/bookmatain")]
    public class BookMatainController : ApiController
    {
        [Route("TestStatus")]
        [HttpGet()]
        public IHttpActionResult TestStatus()
        {
            return NotFound();// 404
            //return BadRequest();// 400
            //return InternalServerError();// 500
            //return Ok();// 200
        }

        [Route("showbook")]
        [HttpGet()]
        public IHttpActionResult TestShowBook()
        {
            var result = new Models.Book();
            result.BookId = 130;
            result.BookName = "我國銀行實施客戶別利潤分析之研究";
            result.BookClassId = "BK";
            result.BookClassName = "Banking";
            result.BookStatusId = "B";
            result.BookStatusName = "已借出";

            return Ok(result);

        }

        [HttpPost()]
        [Route("addbook")]
        public IHttpActionResult AddBook(Models.Book book)
        {

            if (ModelState.IsValid)
            {
                
                //TODO:將資料寫入資料庫
                return Ok(true);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost()]
        [Route("querybook")]
        public IHttpActionResult QueryBook(Models.BookQueryArg arg)
        {
            //virtualData 模擬資料庫內的書籍
            List<Models.Book> virtualData = new List<Models.Book>();
            virtualData.Add(new Models.Book() { 
                BookId = 1, 
                BookName = "測試1",
                BookClassId = "BK", 
                BookClassName = "Banking", 
                BookStatusId = "A", 
                BookStatusName = "可以借出",
                BookBoughtDate = "2022/10/10" });
            
            virtualData.Add(new Models.Book()
            {
                BookId = 2,
                BookName = "測試2",
                BookClassId = "DB",
                BookClassName = "Database",
                BookStatusId = "A",
                BookStatusName = "可以借出",
                BookBoughtDate = "2022/10/11"
            });
            
            virtualData.Add(new Models.Book()
            {
                BookId = 3,
                BookName = "測試3",
                BookClassId = "DH",
                BookClassName = "Dataware House",
                BookStatusId = "A",
                BookStatusName = "可以借出",
                BookBoughtDate = "2022/10/12"
            });

            //依照傳入的參數.書名做模糊查詢
            var result = virtualData.Where(m => m.BookName.Contains(arg.BookName));
            return Ok(result);
        }
    }
}