using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ArcaletTools
{
    public partial class ArcaletGameEx
    {
        /// <summary>
        /// Get ArcaletGameList by Login Userid.
        /// </summary>
        public Dictionary<string, ArcaletGame> AGlist = new Dictionary<string, ArcaletGame>();

        #region static
        /// <summary>
        /// 使用帳號密碼直接登入遊戲
        /// </summary>
        /// <param name="username">遊戲賬號</param>
        /// <param name="password">遊戲密碼</param>
        /// <returns>ArcaletGame 物件</returns>
        public ArcaletGame Login(string username, string password)
        {
            return ArcaletLaunch(username, password);
        }

        /// <summary>
        /// 使用帳號密碼直接登入遊戲
        /// </summary>
        /// <param name="username">遊戲賬號</param>
        /// <param name="password">遊戲密碼</param>
        /// <param name="CallBackHandler">登入成功或失敗的Callback函式</param>
        /// <returns>ArcaletGame 物件</returns>
        public ArcaletGame Login(string username, string password, OnCompleteHandle CallBackHandler)
        {
            return ArcaletLaunch(username, password, CallBackHandler);
        }

        /// <summary>
        /// 使用自定義的方式登入遊戲
        /// </summary>
        /// <param name="username">遊戲賬號</param>
        /// <param name="password">遊戲密碼</param>
        /// <param name="_gguid">遊戲guid</param>
        /// <param name="_sguid">大廳guid</param>
        /// <param name="_certificate">遊戲憑證</param>
        /// <returns>ArcaletGame 物件</returns>
        public ArcaletGame Login(string username, string password, string _gguid, string _sguid, byte[] _certificate)
        {
            return ArcaletLaunch(username, password, _gguid, _sguid, _certificate);
        }

        /// <summary>
        /// 登出遊戲
        /// </summary>
        /// <param name="ag">需要登出的遊戲ArcaletGame</param>
        public void Logout(ArcaletGame ag)
        {
            ArcaletLogOut(ag);
        }

        /// <summary>
        /// 登出遊戲
        /// </summary>
        /// <param name="userid">需要登出的賬號（會自動搜尋）</param>
        public void Logout(string userid)
        {
            ArcaletLogOut(userid);
        }
        #endregion

        #region LoginGame

        ArcaletGame ArcaletLaunch(string username, string password)
        {
            ArcaletGame ag = new ArcaletGame(username, password, ArcaletTool.mainGguid, ArcaletTool.mainSguid, ArcaletTool.mainCertificate);
            ag.onCompletion += CB_ArcaletLaunch;
            ag.onStateChanged += OnStateChanged;
            ag.Launch();
            AddArcaletGame(ag);
            return ag;
        }

        ArcaletGame ArcaletLaunch(string username, string password, string _gguid, string _sguid, byte[] _certificate)
        {
            ArcaletGame ag = new ArcaletGame(username, password, _gguid, _sguid, _certificate);
            ag.onCompletion += CB_ArcaletLaunch;
            ag.onStateChanged += OnStateChanged;
            ag.Launch();
            AddArcaletGame(ag);
            return ag;
        }

        ArcaletGame ArcaletLaunch(string username, string password, OnCompleteHandle handler)
        {
            ArcaletGame ag = new ArcaletGame(username, password, ArcaletTool.mainGguid, ArcaletTool.mainSguid, ArcaletTool.mainCertificate);
            OnMainAGLoginHandle = handler;
            ag.onCompletion += CB_ArcaletLaunchSpecial;
            ag.onStateChanged += OnStateChanged;
            ag.Launch();
            AddArcaletGame(ag);
            return ag;
        }

        #endregion

        #region CB_ArcaletLaunch
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
        #endregion

        #region LogOut

        void ArcaletLogOut(ArcaletGame ag)
        {
            if (ag != null)
            {
                ag.Dispose();
                ag = null;
                RemoveArcaletGame(ag);
            }
        }

        void ArcaletLogOut(string Userid)
        {
            ArcaletGame ag = GetFromAGList(Userid);
            if (ag != null)
            {
                ag.Dispose();
                ag = null;
                RemoveArcaletGame(ag);
            }
        }

        #endregion

        void OnStateChanged(int state, int code, ArcaletGame game)
        {
            Debug.Log("state:" + state.ToString() + " code:" + code);
            if (OnStateChangedEvent != null)
            {
                OnStateChangedEvent(CodeState.GetOnState(state,code), game);
            }

            if (state >= 900)
            {
                ArcaletLogOut(game);
            }
        }
    }
}
