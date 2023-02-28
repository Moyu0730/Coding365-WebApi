using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSystem.Models
{
    /// <summary>
    /// 提供API回傳的固定格式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T>
    {
        /// <summary>
        /// 狀態 treu:成功,false:失敗
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 訊息,是否有商業邏輯的判斷需顯示訊息供前端顯示使用
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 回傳資料,由呼叫端定義資料型態
        /// </summary>
        public T Data { get; set; }
    }
}