using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ArcaletTools
{
    /// <summary>
    /// Arcalet Release
    /// </summary>
    public class ArcaletReleaseV
    {
        /// <summary>
        /// 目前版本號
        /// </summary>
        public static string Version
        {
            get
            {
                return "v" + ArcaletRelease.VerPrimary + "." + ArcaletRelease.VerSecondary + "." + ArcaletRelease.VerRelease + "." + ArcaletRelease.VerBuild;
            }
        }
    }

    [System.Serializable]
    public partial class ArcaletGameControl 
    {
        #region Static

        static ArcaletGameControl instance;

        public static ArcaletGameControl Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.Log("instance = null");
                    instance = new ArcaletGameControl();
                }
                return instance;
            }
        }

        private ArcaletGameControl() { }

        #endregion

        #region ArcaletGameControl

        void AddArcaletGame(ArcaletGame game)
        {
            lock (Instance.AGlist)
            {
                if (Instance.AGlist.ContainsKey(game.gameUserid))
                {
                    Instance.AGlist[game.gameUserid] = game;
                }
                else
                {
                    Instance.AGlist.Add(game.gameUserid, game);
                }

                Debug.Log("Aglist count:" + Instance.AGlist.Count);
            }
        }

        void RemoveArcaletGame(ArcaletGame game)
        {
            lock (Instance.AGlist)
            {
                if (Instance.AGlist.ContainsKey(game.gameUserid))
                {
                    Instance.AGlist.Remove(game.gameUserid);
                }
            }
        }

        public ArcaletGame GetFromAGList(string userid)
        {
            lock (Instance.AGlist)
            {
                Debug.Log("Aglist count:" + Instance.AGlist.Count);

                if (Instance.AGlist.ContainsKey(userid))
                {
                    return Instance.AGlist[userid];
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        /// <summary>
        /// 主要憑證
        /// </summary>
        public static byte[] mainCertificate;

        /// <summary>
        /// 主要遊戲 GUID
        /// </summary>
        public static string mainGguid;

        /// <summary>
        /// 主要主大廳 GUID
        /// </summary>
        public static string mainSguid;

        /// <summary>
        /// 是否 Debug
        /// </summary>
        public bool ShowDebug;
    }
}
