using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ArcaletTools
{

    public class ArcaletReleaseV
    {
        public static string Version
        {
            get
            {
                return "v" + ArcaletRelease.VerPrimary + "." + ArcaletRelease.VerSecondary + "." + ArcaletRelease.VerRelease + "." + ArcaletRelease.VerBuild;
            }
        }
    }

    public partial class ArcaletGameControl : MonoBehaviour
    {
        #region Static

        static ArcaletGameControl instance;

        public static ArcaletGameControl Instance
        {
            get
            {
                if (instance == null)
                {
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
            lock (AGlist)
            {
                if (AGlist.ContainsKey(game.gameUserid))
                {
                    AGlist[game.gameUserid] = game;
                }
                else
                {
                    AGlist.Add(game.gameUserid, game);
                }
            }
        }

        void RemoveArcaletGame(ArcaletGame game)
        {
            lock (AGlist)
            {
                if (AGlist.ContainsKey(game.gameUserid))
                {
                    AGlist.Remove(game.gameUserid);
                }
            }
        }

        public ArcaletGame GetFromAGList(string userid)
        {
            lock (AGlist)
            {
                if (AGlist.ContainsKey(userid))
                {
                    return AGlist[userid];
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion


        #region Setting

        public static string mainGguid = "";
        public static string mainSguid = "";
        public static byte[] mainCertificate = { };

        public bool ShowDebug = false;

        #endregion
    }
}
