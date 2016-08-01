using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ArcaletTools
{
    public partial class ArcaletSceneControl
    {
        #region static

        public Dictionary<int, ArcaletRoom> RoomList = new Dictionary<int, ArcaletRoom>();

        static ArcaletSceneControl instance;

        public static ArcaletSceneControl Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ArcaletSceneControl();
                }
                return instance;
            }
        }

        private ArcaletSceneControl() { }


        public static ArcaletRoom LoginScene(ArcaletGame ag, string sguid, int SceneID)
        {
            return Instance._LoginScene(ag, sguid, SceneID);
        }

        public static ArcaletRoom LoginScene(ArcaletGame ag, string sguid)
        {
            return Instance._LoginScene(ag, sguid, 0);
        }

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

      
        public void AddSceneLoginEvent(int code,ArcaletRoom room)
        {
            if (OnLoginRoomEvent != null)
                OnLoginRoomEvent(CodeState.GetSceneState(code), room);
        }

        public void AddSceneLogOutEvent(int code, ArcaletRoom room)
        {
            if (OnLogoutRoomEvent != null)
                OnLogoutRoomEvent(CodeState.GetSceneState(code), room);
        }

        public void AddSceneMessageEvent(ArcaletMsg msg, ArcaletRoom room)
        {
            if (OnMessageInRoomEvent != null)
                OnMessageInRoomEvent(msg, room);
        }

    }

    public class ArcaletRoom : ArcaletScene
    {
        private bool enterScene = false;
        public bool EnterScene
        {
            get
            {
                return enterScene;
            }
        }

        public ArcaletRoom(ArcaletGame game, string sguid, int sid) : base(game, sguid, sid)
        {

        }

        public ArcaletRoom(ArcaletGame game, string sguid) : base(game, sguid)
        {

        }

        void CB_EnterScene(int code, ArcaletScene scene)
        {
            ArcaletSceneControl.Instance.AddSceneLoginEvent(code, this);

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
        void OnSceneMessageIn(string msg, int delay, ArcaletScene scene)
        {
            ArcaletSceneControl.Instance.AddSceneMessageEvent(new ArcaletMsg(msg, delay), this);
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
            ArcaletSceneControl.Instance.AddSceneLogOutEvent(code, this);
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
