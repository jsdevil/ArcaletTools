using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcaletTools
{
    public partial class ArcaletItemControl
    {
        #region Static

        static ArcaletItemControl instance;

        public static ArcaletItemControl Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ArcaletItemControl();
                }
                return instance;
            }
        }

        private ArcaletItemControl() { }

        /// <summary>
        /// 用iguidname獲取iteminstance
        /// </summary>
        /// <param name="ag"></param>
        /// <param name="iguidKey"></param>
        /// <param name="token"></param>
        /// <param name="OnItemInstanceHandle"></param>
        public static void GetItemInstanceByName(ArcaletGame ag, string iguidKey, object token, OnItemInstanceReadComplete OnItemInstanceHandle)
        {
            Instance._GetItemInstancebyName(ag, iguidKey, token, OnItemInstanceHandle);
        }

        /// <summary>
        /// 用iguid獲取iteminstance
        /// </summary>
        /// <param name="ag"></param>
        /// <param name="iguid"></param>
        /// <param name="token"></param>
        /// <param name="OnItemInstanceHandle"></param>
        public static void GetItemInstanceByIguid(ArcaletGame ag, string iguid, object token, OnItemInstanceReadComplete OnItemInstanceHandle)
        {
            Instance._GetItemInstancebyIguid(ag, iguid, token, OnItemInstanceHandle);
        }

        #endregion

        public Dictionary<string, string> IguidList = new Dictionary<string, string>();

    }
}
