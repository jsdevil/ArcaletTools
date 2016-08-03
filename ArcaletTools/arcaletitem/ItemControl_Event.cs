using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcaletTools.Data;

namespace ArcaletTools
{
    public partial class ArcaletItemEx
    {
        /// <summary>
        /// 當讀取結束時的代表函式
        /// </summary>
        /// <param name="result"></param>
        public delegate void OnItemInstanceReadComplete(IItemInstanceResult result);

        /// <summary>
        /// 當寫入結束時的代表函式
        /// </summary>
        /// <param name="result"></param>
        public delegate void OnItemInstanceWriteComplete(IItemInstanceResult result);

    }
}
