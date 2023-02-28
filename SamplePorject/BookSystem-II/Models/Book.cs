using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookSystem.Models
{
    /// <summary>
    /// 圖書
    /// </summary>
    public class Book
    {
        // ( DONE ) TODO : 針對必填欄位請設定相關設定 - DONE

        // BOOK_ID ( BOOK_LEND_RECORD )
        public int BookId { get; set; }

        // BOOK_NAME ( BOOK_LEND_RECORD -> *BOOK_ID* -> BOOK_DATA )
        [Required(ErrorMessage = "書名不可空白")] // [Required(ErrorMessage = "{0}為必填欄位")]
        public string BookName { get; set; }

        // BOOK_CLASS_ID ( BOOK_LEND_RECORD -> *BOOK_ID* -> BOOK_DATA )
        [Required(ErrorMessage = "圖書類別不可空白")]
        public string BookClassId { get; set; }

        // BOOK_CLASS_NAME ( BOOK_LEND_RECORD -> *BOOK_ID* -> BOOK_DATA -> *BOOK_CLASS_ID* -> BOOK_CLASS )
        public string BookClassName { get; set; }

        // BOOK_BROUGHT_DATE ( BOOK_LEND_RECORD -> *BOOK_ID* -> BOOK_DATA )
        [Required(ErrorMessage = "購買日期不可空白")]
        public DateTime BookBroughtDate { get; set; }

        // CODE_ID ( BOOK_LEND_RECORD -> *BOOK_ID* -> BOOK_DATA -> *BOOK_STATUS* -> BOOK_CODE )
        public string BookStatusId { get; set; }

        // CODE_NAME ( BOOK_LEND_RECORD -> *BOOK_ID* -> BOOK_DATA -> *BOOK_STATUS* -> BOOK_CODE )
        public string BookStatusName { get; set; }

        // KEEPER_ID ( BOOK_LEND_RECORD )
        public string BookKeeperId { get; set; }

        // USER_CNAME ( BOOK_LEND_RECORD -> *KEEPER_ID* -> MEMBER_M )
        public string BookKeeperCname { get; set; }

        // USER_ENAME ( BOOK_LEND_RECORD -> *KEEPER_ID* -> MEMBER_M )
        public string BookKeeperEname { get; set; }

        // BOOK_AUTHOR ( BOOK_LEND_RECORD -> *BOOK_ID* -> BOOK_DATA )
        [Required(ErrorMessage = "作者不可空白")]
        public string BookAuthor { get; set; }

        // BOOK_PUBLISHER ( BOOK_LEND_RECORD -> *BOOK_ID* -> BOOK_DATA )
        [Required(ErrorMessage = "出版商不可空白")]
        public string BookPublisher { get; set; }

        // BOOK_NOTE ( BOOK_LEND_RECORD -> *BOOK_ID* -> BOOK_DATA )
        [Required(ErrorMessage = "內容簡介不可空白")]
        public string BookNote { get; set; }
    }
}