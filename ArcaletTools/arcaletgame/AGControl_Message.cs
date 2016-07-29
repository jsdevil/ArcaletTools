using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcaletTools
{
    public partial class ArcaletGameControl
    {
        void OnMessageIn(string msg, int delay, ArcaletGame game)
        {
            if(OnMessageInEvent != null)
                OnMessageInEvent(new ArcaletMsg(msg, delay), game);
        }

        void OnPrivateMessageIn(string msg, int delay, ArcaletGame game)
        {
            if (OnPrivateInEvent != null)
                OnPrivateInEvent(new ArcaletMsg(msg, delay), game);
        }

       
    }


    public class ArcaletMsg
    {
        private string msg = "";
        private int Delay = 0;

        public ArcaletMsg(string _msg, int _dalay)
        {
            msg = _msg;
            Delay = _dalay;
        }

        public string message
        {
            get
            {
                return msg;
            }
        }

        public int delay
        {
            get
            {
                return Delay;
            }
        }
    }
}
