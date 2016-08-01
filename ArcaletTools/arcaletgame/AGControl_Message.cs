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

    /// <summary>
    /// Arcalet 訊息格式
    /// </summary>
    public class ArcaletMsg
    {
        private string msg = "";
        private int Delay = 0;

        /// <summary>
        /// Arcalet 訊息格式
        /// </summary>
        /// <param name="_msg">接收的訊息</param>
        /// <param name="_dalay">延遲 （毫秒）</param>
        public ArcaletMsg(string _msg, int _dalay)
        {
            msg = _msg;
            Delay = _dalay;
        }

        /// <summary>
        /// 接收的訊息
        /// </summary>
        public string message
        {
            get
            {
                return msg;
            }
        }

        /// <summary>
        /// 延遲 （毫秒）
        /// </summary>
        public int delay
        {
            get
            {
                return Delay;
            }
        }
    }
}
