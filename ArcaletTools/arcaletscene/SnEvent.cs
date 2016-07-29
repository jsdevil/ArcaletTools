using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcaletTools
{
    public partial class ArcaletSceneControl
    {
        public static event Action<int, ArcaletRoom> OnLoginRoomEvent;
        public static event Action<int, ArcaletRoom> OnLogoutRoomEvent;
        public static event Action<ArcaletMsg, ArcaletRoom> OnMessageInRoomEvent;


        public enum State { Fail, Success, Waiting };
    }
}
