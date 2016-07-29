using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcaletTools
{
    public partial class ArcaletGameControl
    {
        //action when any ArcaletGame Login
        public static event Action<ILoginResult, ArcaletGame> OnLoginGameEvent;
        public static event Action<IStateResult, ArcaletGame> OnStateChangedEvent;

        public static event Action<ArcaletMsg, ArcaletGame> OnMessageInEvent;
        public static event Action<ArcaletMsg, ArcaletGame> OnPrivateInEvent;

        public delegate void OnCompleteHandle(ILoginResult result);
        public OnCompleteHandle OnMainAGLoginHandle;

    }
}
