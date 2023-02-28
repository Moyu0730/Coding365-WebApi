using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookSystem.Models
{
    /// <summary>
    /// 圖書相關服務
    /// </summary>
    public class BookService
    {
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
        }

        /// <summary>
        /// 查詢書籍
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public List<Book> QueryBook(BookQueryArg arg)
        {
            var result = new List<Book>();
            using (SqlConnection conn = new SqlConnection(GetDBConnectionString()))
            {
                //TODO:參考Book擁有的屬性,補齊SQL語法 (V)
                string sql = @"
                    Select 
	                    A.BOOK_ID As BookId,A.BOOK_NAME As BookName,
	                    A.BOOK_CLASS_ID As BookClassId,B.BOOK_CLASS_NAME As BookClassName,
	                    Convert(VarChar(10),A.BOOK_BOUGHT_DATE,120) As BookBoughtDate,
	                    A.BOOK_STATUS As BookStatusId,C.CODE_NAME As BookStatusName,
	                    A.BOOK_KEEPER As BookKeeperId,D.USER_CNAME As BookKeeperCname,D.USER_ENAME As BookKeeperEname,
	                    A.BOOK_AUTHOR As BookAuthor,A.BOOK_PUBLISHER As BookPublisher,A.BOOK_NOTE As BookNote
                    From BOOK_DATA As A
	                    Inner Join BOOK_CLASS As B On A.BOOK_CLASS_ID=B.BOOK_CLASS_ID
	                    Inner Join BOOK_CODE As C On A.BOOK_STATUS=C.CODE_ID And C.CODE_TYPE='BOOK_STATUS'
	                    Left Join MEMBER_M As D On A.BOOK_KEEPER=D.USER_ID
	                    Where (A.BOOK_NAME Like @BOOK_NAME Or @BOOK_NAME='') And
                              (A.BOOK_CLASS_ID=@BOOK_CLASS_ID Or @BOOK_CLASS_ID='') And
                              (A.BOOK_KEEPER=@BOOK_KEEPER Or @BOOK_KEEPER='') And
                              (A.BOOK_STATUS=@BOOK_STATUS Or @BOOK_STATUS='') And
                              (A.BOOK_ID=@BOOK_ID Or @BOOK_ID=0)";
                Dictionary<string, Object> parameter = new Dictionary<string, object>();
                parameter.Add("@BOOK_NAME", arg.BookName != null ? "%" + arg.BookName + "%" : string.Empty);
                parameter.Add("@BOOK_KEEPER", arg.BookKeeperId != null ? arg.BookKeeperId : string.Empty);
                parameter.Add("@BOOK_CLASS_ID", arg.BookClassId != null ? arg.BookClassId : string.Empty);
                parameter.Add("@BOOK_STATUS", arg.BookStatusId != null ? arg.BookStatusId : string.Empty);
                parameter.Add("@BOOK_ID", arg.BookId != 0 ? arg.BookId : 0);
                result = conn.Query<Book>(sql, parameter).ToList();
            }
            return result;
        }

        /// <summary>
        /// 新增書籍
        /// </summary>
        /// <param name="book"></param>
        public void AddBook(Book book)
        {
            using (SqlConnection conn = new SqlConnection(GetDBConnectionString()))
            {
                string sql = @"
                    Insert Into BOOK_DATA (
	                    BOOK_NAME,BOOK_CLASS_ID,
	                    BOOK_AUTHOR,BOOK_BOUGHT_DATE,
	                    BOOK_PUBLISHER,BOOK_NOTE,
	                    BOOK_STATUS,BOOK_KEEPER,
	                    BOOK_AMOUNT,
	                    CREATE_DATE,CREATE_USER,MODIFY_DATE,MODIFY_USER )
                    Select 
	                    @BOOK_NAME,@BOOK_CLASS_ID,
	                    @BOOK_AUTHOR,@BOOK_BOUGHT_DATE,
	                    @BOOK_PUBLISHER,@BOOK_NOTE,
	                    @BOOK_STATUS,@BOOK_KEEPER,
	                    0 As BOOK_AMOUNT,
	                    GetDate() As CREATE_DATE,'Admin' As CREATE_USER,GetDate() As MODIFY_DATE,'Admin' As MODIFY_USER";

                /*
                 * INSERT : *BOOK_NAME,   *BOOK_CLASS_ID,   *BOOK_AUTHOR,   *BOOK_BOUGHT_DATE,   *BOOK_PUBLISHER,   *BOOK_NOTE,   *BOOK_STATUS,   *BOOK_KEEPER
                 * SELECT : *@BOOK_NAME,  *@BOOK_CLASS_ID,  *@BOOK_AUTHOR,  *@BOOK_BOUGHT_DATE,  *@BOOK_PUBLISHER,  *@BOOK_NOTE,  *@BOOK_STATUS,  *@BOOK_KEEPER, 
                 * 
                 * INSERT : BOOK_AMOUNT,       CREATE_DATE,               CREATE_USER,             ODIFY_DATE,                 MODIFY_USER
                 * SELECT : 0 As BOOK_AMOUNT,  GetDate() As CREATE_DATE,  'Admin' As CREATE_USER,  GetDate() As MODIFY_DATE,  'Admin' As MODIFY_USER"
                 */

                Dictionary<string, Object> parameter = new Dictionary<string, object>();

                // ( DONE ) TODO : 完成所有需要指定的 parameter - DONE
                parameter.Add("@BOOK_NAME", book.BookName);
                parameter.Add("@BOOK_CLASS_ID", book.BookClassId);
                parameter.Add("@BOOK_AUTHOR", book.BookAuthor);
                parameter.Add("@BOOK_BOUGHT_DATE", book.BookBroughtDate);
                parameter.Add("@BOOK_PUBLISHER", book.BookPublisher);
                parameter.Add("@BOOK_NOTE", book.BookNote);
                parameter.Add("@BOOK_STATUS", "A"); //新增書籍借閱狀態預設為 A-可以借出
                parameter.Add("@BOOK_KEEPER", book.BookKeeperId);

                conn.Execute(sql, parameter);
            }
        }
        /* [ Test Case ]
             {
                "BookName" : "Coding 365",
                "BookClassId" : "DB",
                "BookBoughtDate" : "2022-12-09",
                "BookAuthor" : "Aslan",
                "BookPublisher" : "GSS",
                "BookNote" : "Reporting servics是微軟針對企業所設計的報表解決方案,"
            }

            public int BookId { get; set; }
                    public string BookName { get; set; } // Can't be Empty
                    public string BookClassId { get; set; } // Can't be Empty
                    public string BookClassName { get; set; }
                    public string BookBoughtDate { get; set; } // Can't be Empty
                    public string BookStatusId { get; set; }
                    public string BookStatusName { get; set; }
                    public string BookKeeperId { get; set; }
                    public string BookKeeperCname { get; set; }
                    public string BookKeeperEname { get; set; }
                    public string BookAuthor { get; set; } // Can't be Empty
                    public string BookPublisher { get; set; } // Can't be Empty
                    public string BookNote { get; set; } // Can't be Empty
         */


        /// <summary>
        /// 更新書籍
        /// </summary>
        /// <param name="book"></param>
        public void UpdateBook(Book book){
            using (SqlConnection conn = new SqlConnection(GetDBConnectionString())){
                try{
                    string sql = @"
                        Update BOOK_DATA 
	                        Set 
		                        BOOK_CLASS_ID = @BOOK_CLASS_ID,
		                        BOOK_NAME = @BOOK_NAME,
		                        BOOK_BOUGHT_DATE = @BOOK_BROUGHT_DATE,
		                        BOOK_STATUS =  @BOOK_STATUS,
		                        BOOK_AUTHOR = @BOOK_AUTHOR,
		                        BOOK_PUBLISHER = @BOOK_PUBLISHER,
		                        BOOK_NOTE = @BOOK_NOTE,
                                MODIFY_DATE = GetDate(),
                                MODIFY_USER = 'Admin'
                        WHERE BOOK_ID = @BOOK_ID";

                    Dictionary<string, Object> parameter = new Dictionary<string, object>();
                    parameter.Add("@BOOK_CLASS_ID", book.BookClassId);
                    parameter.Add("@BOOK_NAME", book.BookName);
                    parameter.Add("@BOOK_BROUGHT_DATE", book.BookBroughtDate);
                    parameter.Add("@BOOK_STATUS", book.BookStatusId);
                    parameter.Add("@BOOK_AUTHOR", book.BookAuthor);
                    parameter.Add("@BOOK_PUBLISHER", book.BookPublisher);
                    parameter.Add("@BOOK_NOTE", book.BookNote);
                    parameter.Add("@BOOK_ID", book.BookId);

                    conn.Execute(sql, parameter);
                }catch (Exception Ex){ 
                    throw Ex;
                }
            }
        }
        /* [ TestCase ]
             {
                "BookName" : "Coding 365.5",
                "BookClassId" : "OT",
                "BookBroughtDate" : "2022-12-31",
                "BookAuthor" : "Somebody",
                "BookPublisher" : "IDKIDK",
                "BookNote" : "Somewords",
                "BookStatusId" : "C",
                "BookId" : "2294"
            }
        */

        /// <summary>
        /// 刪除書籍
        /// </summary>
        /// <param name="bookId"></param>
        public void DeleteBookById(int bookId)
        {
            // ( DONE ) TODO : 完成刪除書籍的內容 - DONE
            using (SqlConnection conn = new SqlConnection(GetDBConnectionString()))
            {
                string sql = @"
                    DELETE FROM BOOK_DATA
                    WHERE BOOK_ID = @BOOK_ID";
                Dictionary<string, Object> parameter = new Dictionary<string, object>();
                parameter.Add("@BOOK_ID", bookId);

                conn.Execute(sql, parameter);
            }
        }

        /// <summary>
        /// 檢查書籍可否刪除
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public bool CheckBookIsDeleteable(int bookId)
        {
            BookService bookService = new BookService();
            var book = bookService.QueryBook(new BookQueryArg()
            {
                BookId = bookId
            })[0];

            bool result = true;
            if (book.BookStatusId == "B" || book.BookStatusId == "C")
            {
                result = false;
            }
            return result;
        }
    }
}