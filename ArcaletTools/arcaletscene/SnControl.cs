using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ArcaletTools
{
    public delegate void OnSceneCompleteHandle(ISceneResult result);

    public partial class ArcaletSceneEx
    {
        #region static

        public Dictionary<int, ArcaletRoom> RoomList = new Dictionary<int, ArcaletRoom>();

        internal ArcaletSceneEx()
        {
            RoomList = new Dictionary<int, ArcaletRoom>();
        }

        /// <summary>
        /// 登入遊戲場景 （動態場景）
        /// </summary>
        /// <param name="ag">ArcaletGame</param>
        /// <param name="sguid">場景GUID</param>
        /// <param name="SceneID">動態場景ID</param>
        /// <returns></returns>
        public ArcaletRoom LoginScene(ArcaletGame ag, string sguid, int SceneID)
        {
            return _LoginScene(ag, sguid, SceneID);
        }

        /// <summary>
        /// 登入遊戲場景 （動態場景）
        /// </summary>
        /// <param name="ag">ArcaletGame</param>
        /// <param name="sguid">場景GUID</param>
        /// <param name="SceneID">動態場景ID</param>
        /// <param name="cb">CallBack</param>
        /// <returns></returns>
        public ArcaletRoom LoginScene(ArcaletGame ag, string sguid, int SceneID, OnSceneCompleteHandle cb)
        {
            return _LoginScene(ag, sguid, SceneID,cb);
        }

        /// <summary>
        /// 登入遊戲場景 （靜態場景）
        /// </summary>
        /// <param name="ag">ArcaletGame</param>
        /// <param name="sguid">場景GUID</param>
        /// <returns></returns>
        public ArcaletRoom LoginScene(ArcaletGame ag, string sguid)
        {
            return _LoginScene(ag, sguid, 0);
        }

        /// <summary>
        /// 登入遊戲場景 （靜態場景）
        /// </summary>
        /// <param name="ag">ArcaletGame</param>
        /// <param name="sguid">場景GUID</param>
        /// <param name="cb">CallBack</param>
        /// <returns></returns>
        public ArcaletRoom LoginScene(ArcaletGame ag, string sguid, OnSceneCompleteHandle cb)
        {
            return _LoginScene(ag, sguid, 0, cb);
        }

        /// <summary>
        /// 離開場景
        /// </summary>
        /// <param name="sn"></param>
        public static void Leave(ArcaletRoom sn)
        {
            sn.LeaveScene();
        }

        #endregion

        ArcaletRoom _LoginScene(ArcaletGame game, string sguid, int SceneID)
        {
            ArcaletRoom sn = null;

            if (SceneID != 0)
            {
                sn = new ArcaletRoom(game, sguid, SceneID);
            }
            else
            {
                sn = new ArcaletRoom(game, sguid);
            }
            sn.onMessageIn += sn.OnSceneMessageIn;
            sn.onCompletion += sn.CB_EnterScene;

            sn.Launch();

            return sn;
        }

        ArcaletRoom _LoginScene(ArcaletGame game, string sguid, int SceneID,OnSceneCompleteHandle handle)
        {
            ArcaletRoom sn = null;

            if (SceneID != 0)
            {
                sn = new ArcaletRoom(game, sguid, SceneID, handle);
            }
            else
            {
                sn = new ArcaletRoom(game, sguid, handle);
            }
            sn.onMessageIn += sn.OnSceneMessageIn;
            sn.onCompletion += sn.CB_EnterScene;

            sn.Launch();

            return sn;
        }

        #region ArcaletGameControl

        void AddArcaletGame(ArcaletRoom room)
        {
            lock (RoomList)
            {
                if (RoomList.ContainsKey(room.sid))
                {
                    RoomList[room.sid] = room;
                }
                else
                {
                    RoomList.Add(room.sid, room);
                }
            }
        }

        void RemoveArcaletGame(ArcaletRoom room)
        {
            lock (RoomList)
            {
                if (RoomList.ContainsKey(room.sid))
                {
                    RoomList.Remove(room.sid);
                }
            }
        }

        public ArcaletRoom GetFromAGList(int sceneID)
        {
            lock (RoomList)
            {
                if (RoomList.ContainsKey(sceneID))
                {
                    return RoomList[sceneID];
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

      
        internal void AddSceneLoginEvent(int code,ArcaletRoom room)
        {
            if (OnLoginRoomEvent != null)
                OnLoginRoomEvent(CodeState.GetSceneState(code), room);
        }

        internal void AddSceneLogOutEvent(int code, ArcaletRoom room)
        {
            if (OnLogoutRoomEvent != null)
                OnLogoutRoomEvent(CodeState.GetSceneState(code), room);
        }

        internal void AddSceneMessageEvent(ArcaletMsg msg, ArcaletRoom room)
        {
            if (OnMessageInRoomEvent != null)
                OnMessageInRoomEvent(msg, room);
        }

    }

    public class ArcaletRoom : ArcaletScene
    {
        /// <summary>
        /// 登入場景會觸發的Handle
        /// </summary>
        /// <param name="result">登入狀態</param>

        public OnSceneCompleteHandle OnSceneCompleteHandle;

        private bool enterScene = false;
        public bool EnterScene
        {
            get
            {
                return enterScene;
            }
        }

        public ArcaletRoom(ArcaletGame game, string _sguid, int _sid , OnSceneCompleteHandle cb) : base(game, _sguid, _sid)
        {
            
            OnSceneCompleteHandle = cb;
        }

        public ArcaletRoom(ArcaletGame game, string _sguid, int _sid) : base(game, _sguid, _sid)
        {

        }


        public ArcaletRoom(ArcaletGame game, string sguid, OnSceneCompleteHandle cb) : base(game, sguid)
        {
            OnSceneCompleteHandle = cb;
        }

        public ArcaletRoom(ArcaletGame game, string sguid) : base(game, sguid)
        {

        }

        public virtual void CB_EnterScene(int code, ArcaletScene scene)
        {
            ArcaletTool.Scene.AddSceneLoginEvent(code, this);

            if(OnSceneCompleteHandle != null)
                OnSceneCompleteHandle(CodeState.GetSceneState(code));

            //Code為0表示進入場景成功
            if (code == 0)
            {
                enterScene = true;
            }
            //Code非0表示進入場景失敗
            else
            {
                enterScene = false;
            }
        }

        /// <summary>
        /// 遊戲場景大廳訊息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="delay"></param>
        /// <param name="scene"></param>
        public virtual void OnSceneMessageIn(string msg, int delay, ArcaletScene scene)
        {
            ArcaletTool.Scene.AddSceneMessageEvent(new ArcaletMsg(msg, delay), this);
        }

        /// <summary>
        /// 離開遊戲場景
        /// </summary>
        public void LeaveScene()
        {
            Leave(CB_LeaveScene, null);
        }


        void CB_LeaveScene(int code, object token)
        {
            ArcaletTool.Scene.AddSceneLogOutEvent(code, this);
            //code為0表示離開Scene成功
            if (code == 0)
            {
                enterScene = false;
            }
            //code非0表示離開Scene失敗
            else
            {

            }
        }
    }
}
