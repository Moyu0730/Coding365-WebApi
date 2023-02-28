using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstWebApi.Models
{
    public class BookData
    {
        /// <summary>
        /// 書本ID
        /// </summary>
        public int BOOK_ID { get; set; }
        /// <summary>
        /// 書本名稱
        /// </summary>
        public string BOOK_NAME { get; set; }

        public string BOOK_CLASS_ID { get; set; }

        public string BOOK_AUTHOR { get; set; }

        public DateTime? BOOK_BOUGHT_DATE { get; set; }

        public string BOOK_PUBLISHER { get; set; }

        public string BOOK_NOTE { get; set; }

        public string BOOK_STATUS { get; set; }

        public string BOOK_KEEPER { get; set; }

        public int? BOOK_AMOUNT { get; set; }

        public DateTime? CREATE_DATE { get; set; }

        public string CREATE_USER { get; set; }

        public DateTime? MODIFY_DATE { get; set; }

        public string MODIFY_USER { get; set; }

    }




}