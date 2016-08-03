using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcaletTools.Data;

namespace ArcaletTools
{
    public partial class ArcaletItemEx
    {

        internal ArcaletItemEx() { }

        /// <summary>
        /// 用iguidname獲取iteminstance
        /// </summary>
        /// <param name="ag"></param>
        /// <param name="iguidKey"></param>
        /// <param name="token"></param>
        /// <param name="OnItemInstanceHandle"></param>
        public void GetItemInstanceByName(ArcaletGame ag, string iguidKey, object token, OnItemInstanceReadComplete OnItemInstanceHandle)
        {
            _GetItemInstancebyIguidKey(ag, iguidKey, token, OnItemInstanceHandle);
        }

        /// <summary>
        /// 用iguid獲取iteminstance
        /// </summary>
        /// <param name="ag"></param>
        /// <param name="iguid"></param>
        /// <param name="token"></param>
        /// <param name="OnItemInstanceHandle"></param>
        public void GetItemInstanceByIguid(ArcaletGame ag, string iguid, object token, OnItemInstanceReadComplete OnItemInstanceHandle)
        {
            _GetItemInstancebyIguid(ag, iguid, token, OnItemInstanceHandle);
        }
        
        /// <summary>
        /// 使用itemid 與 itemname 獲得當前的value
        /// </summary>
        /// <param name="ag"></param>
        /// <param name="iguid"></param>
        /// <param name="itemid"></param>
        /// <param name="name"></param>
        /// <param name="token"></param>
        /// <param name="OnItemInstanceHandle"></param>
        public void GetItemInstanceAttribute(ArcaletGame ag, string iguid, int itemid , string name ,object token, OnItemInstanceReadComplete OnItemInstanceHandle)
        {
            _GetItemInstanceAttribute(ag,iguid,itemid, name, token,OnItemInstanceHandle);
        }

        /// <summary>
        /// 設定item （一筆）
        /// </summary>
        /// <param name="ag"></param>
        /// <param name="iguid"></param>
        /// <param name="item"></param>
        /// <param name="token"></param>
        /// <param name="OnItemInstanceHandle"></param>
        public void SetItemInstance(ArcaletGame ag, string iguid, ItemValue item, object token, OnItemInstanceReadComplete OnItemInstanceHandle)
        {
            _SetItemInstanceAttribute(ag, iguid, item, token, OnItemInstanceHandle);
        }

        /// <summary>
        /// 設定item （多筆）
        /// </summary>
        /// <param name="ag"></param>
        /// <param name="iguid"></param>
        /// <param name="item"></param>
        /// <param name="token"></param>
        /// <param name="OnItemInstanceHandle"></param>
        public void SetItemInstance(ArcaletGame ag, string iguid, ItemValue[] item, object token, OnItemInstanceReadComplete OnItemInstanceHandle)
        {
            _SetItemInstanceAttribute(ag, iguid, item, token, OnItemInstanceHandle);
        }

        /// <summary>
        /// Iguid 列表
        /// </summary>
        public Dictionary<string, string> IguidList = new Dictionary<string, string>();

        /// <summary>
        /// 增加IguidList
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_value"></param>
        public void AddIguidList(string _key,string _value)
        {
            if (IguidList.ContainsKey(_key))
            {
                IguidList[_key] = _value;
            }
            else
            {
                IguidList.Add(_key, _value); 
            }
        }

        public void GetIguidList()
        {

        }

    }
}
