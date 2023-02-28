using Dapper;
using FirstWebApi.Models;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GSSWEB"].ConnectionString;

        public Form1()
        {
            InitializeComponent();

            // 預設值
            textClassID.Text = "DB";

        }

        /// <summary>
        /// 清除查詢結果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }

        /// <summary>
        /// 查詢 用 dynamic object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            // 查詢 dynamic object
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = "SELECT * FROM BOOK_DATA";
                var results = conn.Query(sql, new { ClassId = "DB" }).ToList();

                //綁定資料到GridView
                var bindingList = new BindingList<dynamic>(results);
                var source = new BindingSource(bindingList, null);
                dataGridView1.DataSource = source;
            }

        }

        /// <summary>
        /// 查詢 用 model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            // 查詢 BookData model object
            using (var conn = new SqlConnection(ConnectionString))
            {

                var sql = "SELECT * FROM BOOK_DATA";
                var results = conn.Query<BookData>(sql, new { ClassId = "DB" }).ToList();

                //綁定資料到GridView
                var bindingList = new BindingList<BookData>(results);
                var source = new BindingSource(bindingList, null);
                dataGridView1.DataSource = source;
            }

        }

        /// <summary>
        /// 查詢 (+ 參數條件)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // 查詢 BookData model object
            using (var conn = new SqlConnection(ConnectionString))
            {

                var sql = @"SELECT * FROM BOOK_DATA 
                            WHERE (BOOK_CLASS_ID = @ClassId or @ClassId = '') 
                                AND (BOOK_ID = @BookID or @BookID = '') ";
                var results = conn.Query<BookData>(sql, new { ClassId = textClassID.Text, BookID = textBookID.Text }).ToList();

                //綁定資料到GridView
                var bindingList = new BindingList<BookData>(results);
                var source = new BindingSource(bindingList, null);
                dataGridView1.DataSource = source;

            }


        }

        /// <summary>
        /// 查詢 (+ 參數條件)
        /// (Model屬性名稱與Table不一樣)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string bookName1;
            string bookName2;

            // 查詢 BookData by model object (Table 欄位名稱與Model屬性名稱不一樣時)
            using (var conn = new SqlConnection(ConnectionString))
            {

                var sql = @"
                    SELECT A.BOOK_ID AS BookID, A.BOOK_NAME AS BookName, 
                        A.BOOK_CLASS_ID AS BookClassID, A.BOOK_AUTHOR
                    FROM BOOK_DATA AS A  
                    WHERE (BOOK_CLASS_ID = @ClassId or @ClassId='')  
                        AND (BOOK_ID = @BookID or @BookID='') ";
                var results = conn.Query<BookDataDetail>(sql, new { ClassId = textClassID.Text, BookID = textBookID.Text }).ToList();

                bookName2 = results[0].BookName;


                //綁定資料到GridView
                var bindingList = new BindingList<BookDataDetail>(results);
                var source = new BindingSource(bindingList, null);
                dataGridView1.DataSource = source;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            // Insert
            using (var conn = new SqlConnection(ConnectionString))
            {

                var sql = @"
                    INSERT INTO BOOK_DATA(BOOK_NAME, BOOK_CLASS_ID, BOOK_AUTHOR, BOOK_BOUGHT_DATE, BOOK_PUBLISHER, BOOK_NOTE, BOOK_STATUS, BOOK_KEEPER, BOOK_AMOUNT, CREATE_DATE, CREATE_USER, MODIFY_DATE, MODIFY_USER)
                    VALUES(@BOOK_NAME, @BOOK_CLASS_ID, @BOOK_AUTHOR, @BOOK_BOUGHT_DATE, @BOOK_PUBLISHER, @BOOK_NOTE, @BOOK_STATUS, @BOOK_KEEPER, @BOOK_AMOUNT, getdate(), @CREATE_USER, getdate(), @MODIFY_USER)
                    
                    SELECT SCOPE_IDENTITY()";

                var param = new BookData
                {
                    BOOK_NAME = textBookName.Text,
                    BOOK_CLASS_ID = "DB",
                    BOOK_AUTHOR = "microsoft",
                    BOOK_BOUGHT_DATE = new DateTime(2022, 01, 01),
                    BOOK_PUBLISHER = "microsoft",
                    BOOK_NOTE = "",
                    BOOK_STATUS = "A",
                    BOOK_KEEPER = "",
                    BOOK_AMOUNT = 300,
                    CREATE_USER = "admin",
                    MODIFY_USER = "admin"
                };
                var results = conn.ExecuteScalar(sql, param);
                MessageBox.Show($"新增成功，新增的書本ID為 {results}");

            }

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBookID_U.Text))
            {
                MessageBox.Show("請輸入書本ID");
                return;
            }

            // Update
            using (var conn = new SqlConnection(ConnectionString))
            {

                var sql = @"
                    UPDATE A SET A.BOOK_NOTE = @BOOK_NOTE, MODIFY_DATE = GETDATE(), MODIFY_USER = @MODIFY_USER
                    FROM BOOK_DATA AS A WHERE BOOK_ID = @BOOK_ID
                        ";

                var param = new
                {
                    BOOK_ID = textBookID_U.Text,
                    BOOK_NOTE = textBookNote_U.Text,
                    MODIFY_USER = "admin"
                };
                var results = conn.Execute(sql, param);

                MessageBox.Show("修改完成");
            }
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBookID_D.Text))
            {
                MessageBox.Show("請輸入書本ID");
                return;
            }

            // Delete
            using (var conn = new SqlConnection(ConnectionString))
            {

                var sql = @"
                    DELETE A FROM BOOK_DATA AS A WHERE BOOK_ID = @BOOK_ID
                        ";

                var param = new
                {
                    BOOK_ID = textBookID_D.Text
                };
                var results = conn.Execute(sql, param);

            }
            MessageBox.Show("刪除完成");
        }

    }
}
