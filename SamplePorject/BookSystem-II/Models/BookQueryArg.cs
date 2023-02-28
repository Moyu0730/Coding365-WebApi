using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSystem.Models
{
    /// <summary>
    /// 圖書查詢的查詢條件定義
    /// </summary>
    public class BookQueryArg
    {
        /// <summary>
        /// 圖書編號
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// 書名
        /// </summary>
        public string BookName { get; set; }

        /// <summary>
        /// 圖書類別
        /// </summary>
        public string BookClassId { get; set; }
        //TODO:補齊需要的查詢條件 (V)

        /// <summary>
        /// 借閱人使用者代號
        /// </summary>
        public string BookKeeperId { get; set; }

        /// <summary>
        /// 借閱狀態
        /// </summary>
        public string BookStatusId { get; set; }
    }
}