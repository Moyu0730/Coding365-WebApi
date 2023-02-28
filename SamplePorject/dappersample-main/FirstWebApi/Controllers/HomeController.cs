using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using FirstWebApi.Models;

namespace FirstWebApi.Controllers
{
    public class HomeController : Controller
    {

        private string ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["GSSWEB"].ConnectionString;
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var classId = "DB";
            string bookName1;
            string bookName2;

            //// 查詢 dynamic object
            //using (var conn = new SqlConnection(ConnectionString))
            //{

            //    var sql = "SELECT * FROM BOOK_DATA WHERE BOOK_CLASS_ID = @ClassId";
            //    var results = conn.Query(sql, new { ClassId = classId }).ToList();

            //    bookName1 = results[0].BOOK_NAME;

            //}


            //// 查詢 BookData model object
            //using (var conn = new SqlConnection(ConnectionString))
            //{

            //    var sql = "SELECT * FROM BOOK_DATA WHERE BOOK_CLASS_ID = @ClassId";
            //    var results = conn.Query<BookData>(sql, new { ClassId = classId }).ToList();

            //    bookName2 = results[0].BOOK_NAME;

            //}

            // 查詢 BookData by model object (Table 欄位名稱與Model屬性名稱不一樣時)
            using (var conn = new SqlConnection(ConnectionString))
            {

                var sql = @"
                    SELECT A.BOOK_ID AS BookID, A.BOOK_NAME AS BookName, 
                        A.BOOK_CLASS_ID AS BookClassID, A.BOOK_AUTHOR
                    FROM BOOK_DATA AS A  
                    WHERE BOOK_CLASS_ID = @ClassId";
                var results = conn.Query<BookDataDetail>(sql, new { ClassId = classId }).ToList();

                bookName2 = results[0].BookName;

            }

            // Insert
            //using (var conn = new SqlConnection(ConnectionString))
            //{

            //    var sql = @"
            //        INSERT INTO BOOK_DATA(BOOK_NAME, BOOK_CLASS_ID, BOOK_AUTHOR, BOOK_BOUGHT_DATE, BOOK_PUBLISHER, BOOK_NOTE, BOOK_STATUS, BOOK_KEEPER, BOOK_AMOUNT, CREATE_DATE, CREATE_USER, MODIFY_DATE, MODIFY_USER)
            //        VALUES(@BOOK_NAME, @BOOK_CLASS_ID, @BOOK_AUTHOR, @BOOK_BOUGHT_DATE, @BOOK_PUBLISHER, @BOOK_NOTE, @BOOK_STATUS, @BOOK_KEEPER, @BOOK_AMOUNT, getdate(), @CREATE_USER, getdate(), @MODIFY_USER)
            //            ";

            //    var param = new BookData
            //    {
            //        BOOK_NAME = "DataBase Adv",
            //        BOOK_CLASS_ID = "DB",
            //        BOOK_AUTHOR = "microsoft",
            //        BOOK_BOUGHT_DATE = new DateTime(2022, 01, 01),
            //        BOOK_PUBLISHER = "microsoft",
            //        BOOK_NOTE = "",
            //        BOOK_STATUS = "A",
            //        BOOK_KEEPER = "",
            //        BOOK_AMOUNT = 300,
            //        CREATE_USER = "admin",
            //        MODIFY_USER = "admin"
            //    };
            //    var results = conn.Execute(sql, param);

            //}


            // Update
            //using (var conn = new SqlConnection(ConnectionString))
            //{

            //    var sql = @"
            //        UPDATE A SET A.BOOK_NOTE = @BOOK_NOTE, MODIFY_DATE = GETDATE(), MODIFY_USER = @MODIFY_USER
            //        FROM BOOK_DATA AS A WHERE BOOK_ID = @BOOK_ID
            //            ";

            //    var param = new 
            //    {
            //        BOOK_ID = 2297,
            //        BOOK_NOTE = "資料庫書籍",
            //        MODIFY_USER = "admin"
            //    };
            //    var results = conn.Execute(sql, param);

            //}

            // Delete
            using (var conn = new SqlConnection(ConnectionString))
            {

                var sql = @"
                    DELETE A FROM BOOK_DATA AS A WHERE BOOK_ID = @BOOK_ID
                        ";

                var param = new
                {
                    BOOK_ID = 2297
                };
                var results = conn.Execute(sql, param);

            }




            return View();
        }
    }
}
