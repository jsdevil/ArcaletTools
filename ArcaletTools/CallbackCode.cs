using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcaletTools.Data;

namespace ArcaletTools
{
    public enum LoginState
    {
        connectSuccess = 0,
        connectTimeout = 99,
        maxccu = 101,
        repeatlogin = 102,
        AccOrPassError = 103,
        failCreatMainSn = 104,
        connectLose = 105,
        connectFail = 106,
        connectFail2 = 107,
        unknowError,
        systemError = 10000
    }

    public enum ConnectState
    {
        unconnected = 0,
        connectionOmis = 100,
        connectedOmis = 200,
        connectedBinary = 300,
        connectedAccount = 400,
        enterPrivateSn = 500,
        enterMainSn = 600,
        disconnection,
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
                return new IStateResult(ConnectState.disconnection, state, code);
            }

        }
    }
}
