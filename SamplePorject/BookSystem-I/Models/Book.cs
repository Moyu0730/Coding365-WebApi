using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookSystem.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "書名不可空白")]

        public string BookName { get; set; }

        [Required(ErrorMessage = "圖書類別不可空白")]
        public string BookClassId { get; set; }
        public string BookClassName { get; set; }

        [Required(ErrorMessage = "購買日期不可空白")]
        public string BookBoughtDate { get; set; }
        [Required(ErrorMessage = "借閱狀態不可空白")]
        public string BookStatusId { get; set; }
        public string BookStatusName { get; set; }

        //TODO:補齊需要的欄位

    }
}