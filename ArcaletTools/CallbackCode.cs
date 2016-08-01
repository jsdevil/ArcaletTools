using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcaletTools.Data;

namespace ArcaletTools
{
    /// <summary>
    /// 登入狀態
    /// </summary>
    public enum LoginState
    {
        /// <summary>
        /// 連接成功
        /// </summary>
        connectSuccess = 0,
        /// <summary>
        ///  例外或連線逾時
        /// </summary>
        connectTimeout = 99,
        /// <summary>
        ///  遊戲登入人數已達Max CCU上限
        /// </summary>
        maxccu = 101,
        /// <summary>
        /// 玩家重複登入同一遊戲
        /// </summary>
        repeatlogin = 102,
        /// <summary>
        /// 帳號密碼錯誤
        /// </summary>
        AccOrPassError = 103,
        /// <summary>
        ///  無法建立主大廳場景
        /// </summary>
        failCreatMainSn = 104,
        /// <summary>
        /// 無法連線到arcalet遊戲伺服器
        /// </summary>
        connectLose = 105,
        /// <summary>
        /// 找不到arcalet遊戲伺服器
        /// </summary>
        connectFail = 106,
        /// <summary>
        ///  找不到arcalet遊戲伺服器
        /// </summary>
        connectFail2 = 107,
        /// <summary>
        /// 未知錯誤
        /// </summary>
        unknowError,
        /// <summary>
        /// 系統錯誤
        /// </summary>
        systemError = 10000
    }

    /// <summary>
    /// 連接狀態
    /// </summary>
    public enum ConnectState
    {
        /// <summary>
        /// 未連接
        /// </summary>
        unconnected = 0,
        /// <summary>
        ///  正要連線
        /// </summary>
        connectionOmis = 100,
        /// <summary>
        /// 已連線
        /// </summary>
        connectedOmis = 200,
        /// <summary>
        ///  已進入 Binary Mode
        /// </summary>
        connectedBinary = 300,
        /// <summary>
        /// 已登入
        /// </summary>
        connectedAccount = 400,
        /// <summary>
        ///  已進入私人場景
        /// </summary>
        enterPrivateSn = 500,
        /// <summary>
        ///  已進入此遊戲的預設場景
        /// </summary>
        enterMainSn = 600,
        /// <summary>
        /// 無法連線至 arcalet server
        /// </summary>
        lostconnection = 900,
        /// <summary>
        ///接收資料時發生錯誤(斷線)
        /// </summary>
        disconnection1 = 901,
        /// <summary>
        ///傳送資料時發生錯誤(斷線)
        /// </summary>
        disconnection2 = 902,
        /// <summary>
        /// 偵測網路品質時發生錯誤(斷線)
        /// </summary>
        disconnection3 = 903,
        /// <summary>
        ///連線發生錯誤
        /// </summary>
        disconnection4,
    }

    /// <summary>
    /// 場景狀態
    /// </summary>
    public enum SceneState
    {
        /// <summary>
        /// 成功
        /// </summary>
        success = 0,
        /// <summary>
        /// sid參數錯誤!
        /// </summary>
        sidError = 10012,
        /// <summary>
        /// sid所指定的場景不存在
        /// </summary>
        sidNoAvaibleError = 10013,
        /// <summary>
        /// sid所指定的場景已被鎖住，無法進入
        /// </summary>
        sceneLockError = 10014,
        /// <summary>
        ///   指定的場景人數已達上限，無法進入!
        /// </summary>
        sceneMaxError = 10017,
        /// <summary>
        ///  sid所指定的場景不存在或無效
        /// </summary>
        sceneException = 10018,
        /// <summary>
        ///  找不到sid所指定的場景之創建者資訊!
        /// </summary>
        sceneMasterError = 10019,
        /// <summary>
        /// 場景類型不是動態場景
        /// </summary>
        sceneNoDynamic = 10020,
        /// <summary>
        /// 玩家不是sid所指定的場景之建造者
        /// </summary>
        playerNoSceneMaster = 10021,
        /// <summary>
        ///  type參數錯誤
        /// </summary>
        typeError = 10022,
        /// <summary>
        ///  設定場景資訊的key或value參數錯誤
        /// </summary>
        sceneKeyValueError,
       
    }

    public class ILoginResult
    {
        private LoginState state = LoginState.unknowError;
        private int code = 0;
        public bool isLogin = false;

        public ILoginResult(LoginState _state, int _code)
        {
            state = _state;
            code = _code;

            if (state == LoginState.connectSuccess)
            {
                isLogin = true;
            }
            else
            {
                isLogin = false;
            }
        }

        public LoginState State
        {
            get
            {
                return state;
            }
        }

        public int Code
        {
            get
            {
                return code;
            }
        }

        public override string ToString()
        { 
            string outputstring = "";
            if(code != 0)
            {
                outputstring = string.Format("Login State = {0} , Error code:{1}", state.ToString(), code);
            }
            else
            {
                outputstring = string.Format("Login State = {0}",  code);
            }
           
            return outputstring;
        }
    }

    public class IStateResult
    {
        private ConnectState state = ConnectState.unconnected;
        private int statecode = 0;
        private int errorcode = 0;

        public IStateResult(ConnectState _state, int _code, int _errorcode)
        {
            state = _state;
            statecode = _code;
            errorcode = _errorcode;
        }

        public ConnectState State
        {
            get
            {
                return state;
            }
        }

        public int Code
        {
            get
            {
                return statecode;
            }
        }

        public int ErrorCode
        {
            get
            {
                return errorcode;
            }
        }

        public override string ToString()
        {
            string outputstring = "";
            if(statecode >= 900)
            {
                outputstring = string.Format("Connection State = {0} , state code:{1} , error code:{2}", state.ToString(), statecode , ErrorCode);
            }
            else
            {
                outputstring = string.Format("Connection State = {0} , state code:{1}", state.ToString(), statecode);
            }
           
            return outputstring;
        }
    }

    public class IItemResult
    {
        private int errorcode = 0;
        private ItemClass Itemlist;

        public IItemResult(int _code, ItemClass _Itemlist)
        {
            errorcode = _code;
            Itemlist = _Itemlist;
        }

        public int ErrorCode
        {
            get
            {
                return errorcode;
            }
        }


        public ItemClass ItemData
        {
            get
            {
                return Itemlist;
            }
        }
    }

    public class ISceneResult
    {
        private SceneState state = SceneState.sidError;
        private int code = 0;

        public ISceneResult(SceneState _state,int _code)
        {
            code = _code;
            state = _state;
        }

        public SceneState State
        {
            get
            {
                return state;
            }
        }

        public int Code
        {
            get
            {
                return code;
            }
        }

        public override string ToString()
        {
            string outputstring = "";
            if (code != 0)
            {
                outputstring = string.Format("Scene State = {0} , Error code:{1}", state.ToString(), code);
            }
            else
            {
                outputstring = string.Format("Scene State = {0}", code);
            }

            return outputstring;
        }
    }

    public class IItemInstanceResult
    {
        private int errorcode = 0;
        private ItemInstanceList Instancelist;
        private object token = null;

        public IItemInstanceResult(object _token)
        {
            token = _token;
        }

        public IItemInstanceResult(int _code, ItemInstanceList _Instancelist, object _token)
        {
            errorcode = _code;
            Instancelist = _Instancelist;
            token = _token;
        }

        public int ErrorCode
        {
            get
            {
                return errorcode;
            }
        }

        public ItemInstanceList ItemData
        {
            get
            {
                return Instancelist;
            }
        }

        public object Token
        {
            get
            {
                return token;
            }
        }
    }

    public static class CodeState
    {
        public static ILoginResult GetLoginState(int Code)
        {
            bool exists = Enum.IsDefined(typeof(LoginState), Code);

            if (exists)
            {
                return new ILoginResult((LoginState)Code, Code);
            }
            else
            {
                return new ILoginResult(LoginState.unknowError, Code);
            }

        }

        public static IStateResult GetOnState(int state, int code)
        {
            bool exists = Enum.IsDefined(typeof(ConnectState), state);

            if (exists)
            {
                return new IStateResult((ConnectState)state, state, code);
            }
            else
            {
                return new IStateResult(ConnectState.disconnection4, state, code);
            }

        }

        public static ISceneResult GetSceneState(int code)
        {
            bool exists = Enum.IsDefined(typeof(SceneState), code);

            if (exists)
            {
                return new ISceneResult((SceneState)code, code);
            }
            else
            {
                return new ISceneResult(SceneState.sceneException, code);
            }
        }
    }
}
