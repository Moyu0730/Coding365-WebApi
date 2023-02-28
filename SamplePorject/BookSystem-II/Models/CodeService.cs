using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Dapper;

namespace BookSystem.Models
{
    /// <summary>
    /// 下拉選單資料服務
    /// </summary>
    public class CodeService
    {
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
        }

        /// <summary>
        /// 取得借閱狀態代碼資料
        /// </summary>
        /// <returns></returns>
        public List<Code> GetBookStatusData()
        {
            List<Models.Code> result = new List<Models.Code>();
            using (SqlConnection conn=new SqlConnection(GetDBConnectionString()))
            {
                string sql = "Select CODE_ID As Value,CODE_NAME As Text From BOOK_CODE Where CODE_TYPE=@CODE_TYPE";
                Dictionary<string, Object> parameter = new Dictionary<string, object>();
                parameter.Add("@CODE_TYPE", "BOOK_STATUS");
                result = conn.Query<Code>(sql, parameter).ToList();
            }
            return result;
        }

        /// <summary>
        /// 取得使用者代碼資料
        /// </summary>
        /// <returns></returns>
        public List<Code> GetMemberData()
        {
            //TODO:修正以下SQL語法,已取得完整的使用者帶碼資料
            List<Models.Code> result = new List<Models.Code>();
            using (SqlConnection conn = new SqlConnection(GetDBConnectionString()))
            {
                string sql = "Select [USER_ID] As Value , USER_CNAME As Text From MEMBER_M";
                result = conn.Query<Code>(sql).ToList();
            }
            return result;
        }

        /// <summary>
        /// 取得圖書類別代碼資料
        /// </summary>
        /// <returns></returns>
        public List<Code> GetBookClassData()
        {
            //     todo : 請改從資料庫取得圖書類別帶碼資料
            //    var result = new list<code>
            //    {
            //        models.codeservice
            //        new code() { value = "bk", text = "banking" },
            //        new code() { value = "db", text = "database" },
            //        new code() { value = "dh", text = "dataware house" },
            //        new code() { value = "in", text = "internet" }
            //    };
            //    return result;
            List<Models.Code> result = new List<Models.Code>();
            using (SqlConnection conn = new SqlConnection(GetDBConnectionString()))
            {
                string sql = "SELECT BOOK_CLASS_ID As Value, BOOK_CLASS_NAME As Text From BOOK_CLASS";
                Dictionary<string, Object> parameter = new Dictionary<string, object>();

                result = conn.Query<Code>(sql).ToList();
            }
            return result;
        }
    }
}