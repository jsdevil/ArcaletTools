using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

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
    public partial class ArcaletGameEx 
    {
        #region Static

        internal ArcaletGameEx()
        {
            AGlist = new Dictionary<string, ArcaletGame>();
        }

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

                ArcaletTool.Debuger("Arcalet Game List Count:" + AGlist.Count);
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

                ArcaletTool.Debuger("Arcalet Game List Count:" + AGlist.Count);
            }
        }

        public ArcaletGame GetFromAGList(string userid)
        {
            lock (AGlist)
            {
                ArcaletTool.Debuger("Arcalet Game List Count:" + AGlist.Count);

                if (AGlist.ContainsKey(userid))
                {
                    return AGlist[userid];
                }
                else
                {
                    ArcaletTool.Debuger(userid + " in Arcalet Game List no avaible");

                    return null;
                }
            }
        }

        #endregion

        
    }
}
