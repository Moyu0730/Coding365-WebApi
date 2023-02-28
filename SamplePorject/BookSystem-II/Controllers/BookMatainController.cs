using BookSystem.Models;
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
        public IHttpActionResult TestShowBook(string param1, string param2)
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
                //TODO:呼叫 bookService內的方法將書本新增近資料庫 (V)
                Models.BookService bookService = new Models.BookService();
                bookService.AddBook(book);
                ApiResult<string> result = new ApiResult<string>
                {
                    Data = "",
                    Status = true,
                    Message = "success"
                };
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost()]
        [Route("updatebook")]
        public IHttpActionResult UpdateBook(Models.Book book)
        {
            try
            {
                Models.BookService bookService = new Models.BookService();

                if (ModelState.IsValid)
                {
                    bookService.UpdateBook(book);
                    return Ok(new ApiResult<string>()
                    {
                        Data = string.Empty,
                        Status = true,
                        Message = string.Empty
                    }
                     );
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// 查詢圖書資料 API
        /// </summary>
        /// <param name="arg">查詢條件</param>
        /// <returns></returns>
        [HttpPost()]
        // ( DONE ) TODO : 請定義Route - DONE
        [Route("querybook")]
        public IHttpActionResult QueryBook(Models.BookQueryArg arg)
        {
            try
            {
                Models.BookService bookService = new Models.BookService();

                ApiResult<List<Book>> result = new ApiResult<List<Book>>
                {
                    Data = bookService.QueryBook(arg),
                    Status = true,
                    Message = "Search Success"
                };
                return Ok(result.Data);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost()]
        [Route("deletebook")]
        public IHttpActionResult DeleteBooById([FromBody] int bookId)   // Frombody & No Frombody
        {
            try
            {
                Models.BookService bookService = new Models.BookService();
                bookService.DeleteBookById(bookId);

                // TODO : 已借書能否刪除、如何處理
                ApiResult<string> result = new ApiResult<string>
                {
                    Data = string.Empty,
                    Status = true,
                    Message = "Success"
                };

                if (bookService.CheckBookIsDeleteable(bookId))
                {
                    bookService.DeleteBookById(bookId);
                }
                else
                {
                    result.Status = false;
                    result.Message = "圖書已借出不可刪除";
                }

                return Ok(result.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost()]
        [Route("loadbook")]
        public IHttpActionResult GetBookById([FromBody] int bookId)
        {
            try
            {
                Models.BookService bookService = new Models.BookService();
                
                // TODO : 請呼叫 BookService 內的方法取得要讀取的書籍
                ApiResult<Book> result = new ApiResult<Book>
                {
                    Data = new Book() {BookId=9999,BookName="xxxxx"},
                    Status = true,
                    Message = string.Empty
                };
                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}