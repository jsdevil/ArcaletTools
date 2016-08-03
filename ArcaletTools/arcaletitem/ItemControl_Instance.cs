using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcaletTools.Data;
using UnityEngine;

namespace ArcaletTools
{
    public partial class ArcaletItemEx
    {
        class SendToken
        {
            public OnItemInstanceReadComplete OnItemInstanceHandle = null;
            public object token = null;

            public SendToken(OnItemInstanceReadComplete _OnItemInstanceHandle, object _token)
            {
                OnItemInstanceHandle = _OnItemInstanceHandle;
                token = _token;
            }
        }

        #region GetIteminstance

        void _GetItemInstancebyIguidKey(ArcaletGame ag, string iguidKey, object token, OnItemInstanceReadComplete OnItemInstanceHandle)
        {
            if (GetIguid(iguidKey) == null)
            {
                SendToken atoken = new SendToken(OnItemInstanceHandle, token);
                atoken.OnItemInstanceHandle(new IItemInstanceResult(-1, null, atoken.token));
                return;
            }

            SendToken stoken = new SendToken(OnItemInstanceHandle, token);

            string iguid = GetIguid(iguidKey);
            ArcaletItem.GetItemInstance(ag, iguid, OnItemInstanceReadCallBack, stoken);
        }

        void _GetItemInstancebyIguid(ArcaletGame ag, string iguid, object token, OnItemInstanceReadComplete OnItemInstanceHandle)
        {
            SendToken stoken = new SendToken(OnItemInstanceHandle, token);
            ArcaletItem.GetItemInstance(ag, iguid, OnItemInstanceReadCallBack, stoken);
        }

        void _GetItemInstanceAttribute(ArcaletGame ag, string iguid, int id, string attrName, object token, OnItemInstanceReadComplete OnItemInstanceHandle)
        {
            SendToken stoken = new SendToken(OnItemInstanceHandle, token);
            ArcaletItem.GetItemInstanceAttribute(ag, iguid, id, attrName, OnItemInstanceReadCallBack, token);
        }

        //void _GetItemInstanceby

        #endregion

        #region SetItemInstance

        void _SetItemInstanceAttribute(ArcaletGame ag, string iguid, ItemValue item, object token, OnItemInstanceReadComplete OnItemInstanceHandle)
        {
            SendToken stoken = new SendToken(OnItemInstanceHandle, token);
            ArcaletItem.SetItemInstanceAttribute(ag, iguid, item.itemid, item.name, item.value, OnItemInstanceWriteCallBack, token);
        }

        void _SetItemInstanceAttribute(ArcaletGame ag, string iguid, ItemValue[] item, object token, OnItemInstanceReadComplete OnItemInstanceHandle)
        {
            if(item.Length == 0)
            {
                Debug.LogWarning("ItemValue count length must largger than 0.");
                return;
            }

            SendToken stoken = new SendToken(OnItemInstanceHandle, token);

            string[] namelist = new string[item.Length];
            string[] valuelist = new string[item.Length];
            int itemid = item[0].itemid;

            for (int i = 0; i < item.Length; i++)
            {
                namelist[i] = item[i].name;
                valuelist[i] = item[i].value;
            }

            ArcaletItem.SetItemInstanceAttribute(ag, iguid, itemid, namelist, valuelist, OnItemInstanceWriteCallBack, token);
        }

        #endregion

        #region CallBack

        void OnItemInstanceReadCallBack(int code, object data, object token)
        {
            SendToken stoken = token as SendToken;
            ItemInstanceList ItemInstance_list = new ItemInstanceList();

            if (code == 0)
            {

                ItemInstance_list = new ItemInstanceList(data);
            }

            stoken.OnItemInstanceHandle(new IItemInstanceResult(code, ItemInstance_list, stoken.token));
        }

        void OnItemInstanceWriteCallBack(int code, object token)
        {
            SendToken stoken = token as SendToken;

            stoken.OnItemInstanceHandle(new IItemInstanceResult(code, stoken.token));
        }
        #endregion

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
