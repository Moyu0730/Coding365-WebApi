using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstWebApi.Models
{
    public class BookDataDetail
    {

        /// <summary>
        /// 書本ID
        /// </summary>
        public int BookID { get; set; }
        /// <summary>
        /// 書本名稱
        /// </summary>
        public string BookName { get; set; }

        public string BookClassID { get; set; }
        public string BookClassName { get; set; }

        public string BookAuthor { get; set; }

        public DateTime? BookBoughtDate { get; set; }

        public string BookPublisher { get; set; }

        public string BookNote { get; set; }

        public string BookStatusID { get; set; }
        public string BookStatusName { get; set; }

        public string BookKeeperID { get; set; }
        public string BookKeeperName { get; set; }
        public int? BookAmount { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateUser { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyUser { get; set; }

    }
}