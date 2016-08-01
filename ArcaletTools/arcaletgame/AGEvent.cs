using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcaletTools
{
    public partial class ArcaletGameControl
    {
        //action when any ArcaletGame Login
        /// <summary>
        /// 登入後會觸發的Event
        /// </summary>
        public static event Action<ILoginResult, ArcaletGame> OnLoginGameEvent;

        /// <summary>
        /// 遊戲連線狀態的Event
        /// </summary>
        public static event Action<IStateResult, ArcaletGame> OnStateChangedEvent;

        /// <summary>
        /// 主大廳訊息Event
        /// </summary>
        public static event Action<ArcaletMsg, ArcaletGame> OnMessageInEvent;

        /// <summary>
        /// 私人訊息Event
        /// </summary>
        public static event Action<ArcaletMsg, ArcaletGame> OnPrivateInEvent;

        /// <summary>
        /// 登入遊戲會觸發的Handle
        /// </summary>
        /// <param name="result">登入狀態</param>
        public delegate void OnCompleteHandle(ILoginResult result);

        /// <summary>
        /// 默認主要登入遊戲Handle
        /// </summary>
        public OnCompleteHandle OnMainAGLoginHandle;

    }
}
