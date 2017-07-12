using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMBAPP.Entity.AppEntity
{
    /// <summary>
    /// 基础输出模型
    /// </summary>
    public class APIOutPut
    {
        //输出数据
        public object data { get; set; }
        public State state { get; set; }
    }

    public class State
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string errorMsg { get; set; }
    }
    /// <summary>
    /// 输出List基础模型
    /// 2016-04-28
    /// </summary>
    public class APIListOutPut<T>
    {
        /// <summary>
        /// 数据数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public List<T> DataList { get; set; }
    }
}
