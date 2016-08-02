using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcaletTools
{
    public partial class ArcaletSceneControl
    {
        /// <summary>
        /// 登入場景會觸發的Event
        /// </summary>
        public static event Action<ISceneResult, ArcaletRoom> OnLoginRoomEvent;
        /// <summary>
        /// 登出場景會觸發的Event
        /// </summary>
        public static event Action<ISceneResult, ArcaletRoom> OnLogoutRoomEvent;
        /// <summary>
        /// 場景訊息會觸發的Event
        /// </summary>
        public static event Action<ArcaletMsg, ArcaletRoom> OnMessageInRoomEvent;

        
    }
}
