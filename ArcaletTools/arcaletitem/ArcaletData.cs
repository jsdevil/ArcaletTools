using System;
using System.Collections;
using System.Collections.Generic;


namespace ArcaletTools
{
    namespace Data
    {
        #region Item

        /// <summary>
        /// ItemClass集合物件, 為ItemClass回傳的轉換函式。
        /// DicItem => Key為ItemID, Value為 ItemValue物件。
        /// 當DicItem.length = 0 為沒有任何物品
        /// </summary>
        [Serializable]
        public class ItemClass
        {
            /// <summary>
            /// 物品的 iguid
            /// </summary>
            public string iguid = "";

            /// <summary>
            /// 物品的名稱
            /// </summary>
            public string name = "";

            /// <summary>
            /// 物品的描述
            /// </summary>
            public string desc = "";

            /// <summary>
            /// 物品類型，1: 商品，2: 性質，3: 資料
            /// </summary>
            public int type = 1;

            /// <summary>
            /// 價格
            /// </summary>
            public int price = 0;

            /// <summary>
            /// 物品圖片之URL
            /// </summary>
            public string image = "";

            /// <summary>
            /// 物品列表。Key為該物品名稱，Value為物品屬性
            /// </summary>
            public Dictionary<string, ItemValue> DicItem = new Dictionary<string, ItemValue>();

            /// <summary>
            /// 新建ItemClass集合物件
            /// </summary>
            /// <param name="data">Callback data object</param>
            public ItemClass(object data)
            {
                //將取得的資料轉成 List<Hashtable> 
                List<Hashtable> list = data as List<Hashtable>;

                if (list.Count == 0)
                {
                    return;
                }

                //因為ItemClass只會有一個item 所以只取第一個
                Hashtable item_ht = list[0] as Hashtable;

                iguid = item_ht["iguid"].ToString();
                name = item_ht["name"].ToString();
                desc = item_ht["desc"].ToString();
                type = int.Parse(item_ht["type"].ToString());
                price = int.Parse(item_ht["price"].ToString());
                image = item_ht["image"].ToString();

                Hashtable attrlist = item_ht["attr"] as Hashtable;

                foreach (DictionaryEntry item in attrlist)
                {
                    string Name = item.Key.ToString();
                    string Value = item.Value.ToString();

                    ItemValue Item_Value = new ItemValue(Name, Value);

                    DicItem.Add(Item_Value.name, Item_Value);
                }
            }

            public ItemValue GetAttr(string _name)
            {
                if (DicItem.ContainsKey(_name))
                {
                    return DicItem[name];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// ItemInstance集合物件, 為ItemInstance回傳的轉換函式。
        /// DicItemInstance => Key為ItemID,Value為 ItemInstance的物件。
        /// 當DicItemInstance.length = 0 為沒有任何物品
        /// </summary>
        public class ItemInstanceList
        {
            // 因為裡面包含多筆資料所以為Dicitonary集合。
            public Dictionary<int, ItemInstance> DicItemInstance = new Dictionary<int, ItemInstance>();

            public ItemInstanceList()
            {
                
            }

            /// <summary>
            /// 新建ItemInstance集合物件
            /// </summary>
            /// <param name="data">Callback data object</param>
            public ItemInstanceList(object data)
            {
                //將取得的資料轉成 List<Hashtable> 
                List<Hashtable> list = data as List<Hashtable>;

                if (list.Count == 0)
                {
                    return;
                }

                foreach (Hashtable item_ht in list)
                {
                    ItemInstance item_Instance = new ItemInstance(item_ht);
                    DicItemInstance.Add(item_Instance.itemid, item_Instance);
                }
            }
        }

        /// <summary>
        /// 實例化後的 Item 列表
        /// </summary>
        [Serializable]
        public class ItemInstance
        {
            /// <summary>
            /// 該物品的 iguid
            /// </summary>
            public string iguid = "";

            /// <summary>
            /// 該物品的名稱
            /// </summary>
            public string name = "";

            /// <summary>
            /// 該物品 instance 的 id
            /// </summary>
            public int itemid = 0;

            /// <summary>
            /// 目前伺服器的系統時間
            /// </summary>
            public DateTime timenow = new DateTime();

            /// <summary>
            /// 該物品實例的到期時間
            /// </summary>
            public DateTime time_expire = new DateTime();

            /// <summary>
            /// 物品列表。Key為該物品名稱，Value為物品屬性
            /// </summary>
            public Dictionary<string, ItemValue> DicItem = new Dictionary<string, ItemValue>();

            public ItemInstance(Hashtable item_ht)
            {
                itemid = int.Parse(item_ht["id"].ToString());
                timenow = DateTime.Parse(item_ht["now"].ToString());
                iguid = item_ht["iguid"].ToString();
                name = item_ht["name"].ToString();

                if (item_ht["expire"].ToString() != "")
                {
                    time_expire = DateTime.Parse(item_ht["expire"].ToString());
                }

                Hashtable attrlist = item_ht["attr"] as Hashtable;

                foreach (DictionaryEntry item in attrlist)
                {
                    ItemValue Item_Value = new ItemValue(item,itemid);

                    DicItem.Add(Item_Value.name, Item_Value);
                }
            }

            /// <summary>
            /// 從ItemList裡獲取ItemValue
            /// </summary>
            /// <param name="_name"></param>
            /// <returns></returns>
            public ItemValue GetAttr(string _name)
            {
                if (DicItem.ContainsKey(_name))
                {
                    return DicItem[name];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Item 屬性
        /// </summary>
        [Serializable]
        public class ItemValue
        {
            /// <summary>
            /// 屬性名稱，也就是 key。
            /// </summary>
            public readonly string name;
            private string _Value;
            
            /// <summary>
            /// 屬性值
            /// </summary>
            public string value
            {
                get
                {
                    return _Value;
                }

                set
                {
                    _Value = value;
                    boolNew = true;
                }
            }

            /// <summary>
            /// 物品ID
            /// 若是instance的item才會有值
            /// </summary>
            public readonly int itemid = 0;

            /// <summary>
            /// 物品類型=
            /// 0:itemClass 1:itemInstance
            /// </summary>
            public int itemType = 0;

            /// <summary>
            /// 是否為修改過的Item
            /// (初始化的不算)
            /// </summary>
            public bool boolNew = false;

            /// <summary>
            /// 寫入此屬性值的時間
            /// </summary>
            public DateTime TimeStamp = new DateTime();

            /// <summary>
            /// 初始化ItemClass
            /// </summary>
            /// <param name="_name"></param>
            /// <param name="_value"></param>
            public ItemValue(string _name, string _value)
            {
                name = _name;
                _Value = _value;
                itemType = 0;
            }

            /// <summary>
            /// 初始化ItemInstance
            /// </summary>
            /// <param name="item"></param>
            /// <param name="id"></param>
            public ItemValue(DictionaryEntry item,int id)
            {
                name = item.Key.ToString();
                Hashtable ValueH = item.Value as Hashtable;
                _Value = ValueH["value"].ToString();
                TimeStamp = DateTime.Parse(ValueH["stamp"].ToString());
                itemid = id;
                itemType = 1;
            }

            /// <summary>
            /// 初始化ItemInstance
            /// </summary>
            /// <param name="_name"></param>
            /// <param name="_value"></param>
            /// <param name="_timestamp"></param>
            /// <param name="_itemid"></param>
            public ItemValue(string _name,string _value,DateTime _timestamp ,int _itemid)
            {
                name = _name;
                _Value = _value;
                TimeStamp = _timestamp;
                itemid = _itemid;
                itemType = 1;
            }

            /// <summary>
            /// 初始化ItemInstance 
            /// （作為從後台獲取的item）
            /// </summary>
            /// <param name="_name"></param>
            /// <param name="_itemid"></param>
            public ItemValue(string _name,int _itemid)
            {
                name = _name;
                itemid = _itemid;
                itemType = 1;
            }

            /// <summary>
            /// 更新資料後執行的函式
            /// </summary>
            public void UploadComplete()
            {
                boolNew = false;
                TimeStamp = DateTime.Now;
            }
        }
        #endregion

        #region Leaderboard

        public class LeaderBoardList
        {
            public List<LeaderBoardData> ListLeaderBoard = new List<LeaderBoardData>();

            public LeaderBoardList(object data)
            {
                ListLeaderBoard.Clear();

                List<Hashtable> list = data as List<Hashtable>;

                if (list.Count == 0)
                    return;

                foreach (Hashtable attr_ht in list)
                {
                    ListLeaderBoard.Add(new LeaderBoardData(attr_ht));
                }


            }
        }

        public class LeaderBoardData
        {
            public string userid = "";
            public int score = 0;
            public string note = "";
            public DateTime timestamp = new DateTime();
            public DateTime timenow = new DateTime();
            public string nickname = "";

            public LeaderBoardData(Hashtable attr_ht)
            {
                userid = attr_ht["userid"].ToString();
                score = int.Parse(attr_ht["score"].ToString());
                note = attr_ht["note"].ToString();
                timestamp = DateTime.Parse(attr_ht["stamp"].ToString());
                timenow = DateTime.Parse(attr_ht["now"].ToString());
                nickname = attr_ht["nickname"].ToString();
            }
        }

        #endregion
    }

}
