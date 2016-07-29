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


        public static void GetItemInstanceByName(ArcaletGame ag, string iguidKey, object token, OnItemInstanceComplete OnItemInstanceHandle)
        {
            Instance._GetItemInstancebyName(ag, iguidKey, token, OnItemInstanceHandle);
        }

        public static void GetItemInstanceByIguid(ArcaletGame ag, string iguid, object token, OnItemInstanceComplete OnItemInstanceHandle)
        {
            Instance._GetItemInstancebyIguid(ag, iguid, token, OnItemInstanceHandle);
        }

        #endregion

        public Dictionary<string, string> IguidList = new Dictionary<string, string>();

    }
}
