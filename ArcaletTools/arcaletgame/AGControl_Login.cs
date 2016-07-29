using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ArcaletTools
{
    public partial class ArcaletGameControl
    {
        /// <summary>
        /// Get ArcaletGameList by Login Userid.
        /// </summary>
        public Dictionary<string, ArcaletGame> AGlist = new Dictionary<string, ArcaletGame>();

        #region static
        public static ArcaletGame GameLogin(string username, string password)
        {
            return Instance.ArcaletLaunch(username, password);
        }

        public static ArcaletGame GameLogin(string username, string password, OnCompleteHandle CallBackHandler)
        {
            return Instance.ArcaletLaunch(username, password, CallBackHandler);
        }

        public static ArcaletGame GameLogin(string username, string password, string _gguid, string _sguid, byte[] _certificate)
        {
            return Instance.ArcaletLaunch(username, password, _gguid, _sguid, _certificate);
        }

        public static void GameLogout(ArcaletGame ag)
        {
            Instance.ArcaletLogOut(ag);
        }

        public static void GameLogout(string userid)
        {
            Instance.ArcaletLogOut(userid);
        }
        #endregion

        ArcaletGame ArcaletLaunch(string username, string password)
        {
            ArcaletGame ag = new ArcaletGame(username, password, mainGguid, mainSguid, mainCertificate);
            ag.onCompletion += CB_ArcaletLaunch;
            ag.onStateChanged += OnStateChanged;
            ag.Launch();
            return ag;
        }

        ArcaletGame ArcaletLaunch(string username, string password, string _gguid, string _sguid, byte[] _certificate)
        {
            ArcaletGame ag = new ArcaletGame(username, password, _gguid, _sguid, _certificate);
            ag.onCompletion += CB_ArcaletLaunch;
            ag.onStateChanged += OnStateChanged;
            ag.Launch();
            return ag;
        }

        ArcaletGame ArcaletLaunch(string username, string password, OnCompleteHandle handler)
        {
            ArcaletGame ag = new ArcaletGame(username, password, mainGguid, mainSguid, mainCertificate);
            OnMainAGLoginHandle = handler;
            ag.onCompletion += CB_ArcaletLaunchSpecial;
            ag.onStateChanged += OnStateChanged;
            ag.Launch();
            return ag;
        }

        void CB_ArcaletLaunchSpecial(int code, ArcaletGame game)
        {
            OnMainAGLoginHandle(CodeState.GetLoginState(code));

            if (OnLoginGameEvent != null)
            {
                OnLoginGameEvent(CodeState.GetLoginState(code), game);
            }
        }

        void CB_ArcaletLaunch(int code, ArcaletGame game)
        {
            if (OnLoginGameEvent != null)
            {
                OnLoginGameEvent(CodeState.GetLoginState(code), game);
            }
        }

        void OnStateChanged(int state, int code, ArcaletGame game)
        {
            if (OnStateChangedEvent != null)
            {
                OnStateChangedEvent(CodeState.GetOnState(state,code), game);
            }

            if (state >= 900)
            {
                ArcaletLogOut(game);
            }
        }

        #region LogOut

        void ArcaletLogOut(ArcaletGame ag)
        {
            if (ag != null)
            {
                ag.Dispose();
                ag = null;
            }
        }

        void ArcaletLogOut(string Userid)
        {
            ArcaletGame ag = GetFromAGList(Userid);
            if (ag != null)
            {
                ag.Dispose();
                ag = null;
            }
        }

        #endregion



    }
}
