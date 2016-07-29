using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcaletTools.Data;
using UnityEngine;

namespace ArcaletTools
{
    public partial class ArcaletItemControl
    {
        class SendToken
        {
            public OnItemInstanceComplete OnItemInstanceHandle = null;
            public object token = null;

            public SendToken(OnItemInstanceComplete _OnItemInstanceHandle, object _token)
            {
                OnItemInstanceHandle = _OnItemInstanceHandle;
                token = _token;
            }
        }


        void _GetItemInstancebyName(ArcaletGame ag,string iguidKey,object token, OnItemInstanceComplete OnItemInstanceHandle)
        {
            if (GetIguid(iguidKey) == null)
            {
                SendToken atoken = new SendToken(OnItemInstanceHandle, token);
                atoken.OnItemInstanceHandle(new IItemInstanceResult(-1, null, atoken.token));
                return;
            }

            SendToken stoken = new SendToken(OnItemInstanceHandle, token);

            string iguid = GetIguid(iguidKey);
            ArcaletItem.GetItemInstance(ag, iguid, OnItemInstanceCallBack, stoken);
        }

        void _GetItemInstancebyIguid(ArcaletGame ag, string iguid, object token, OnItemInstanceComplete OnItemInstanceHandle)
        {
            SendToken stoken = new SendToken(OnItemInstanceHandle, token);
            ArcaletItem.GetItemInstance(ag, iguid, OnItemInstanceCallBack, stoken);
        }

        void OnItemInstanceCallBack(int code, object data,object token)
        {
            SendToken stoken = token as SendToken;
            ItemInstanceList ItemInstance_list = new ItemInstanceList();

            if (code == 0)
            {
               
                ItemInstance_list = new ItemInstanceList(data);
            }

            stoken.OnItemInstanceHandle(new IItemInstanceResult(code, ItemInstance_list, stoken.token));
        }

        string GetIguid(string iguidKey)
        {
            if (IguidList.ContainsKey(iguidKey))
            {
                return IguidList[iguidKey];
            }
            else
            {
                return null;
            }
        }
    }
}
