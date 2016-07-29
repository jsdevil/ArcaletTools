using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using ArcaletTools;

namespace ArcaletTools
{
    class ArcaletEditor:EditorWindow
    {
        public static ArcaletEditor Instance;

        [MenuItem("Arcalet/Add/AGCC")]
        static void AddAgccObject()
        {
            if (FindObjectOfType<ArcaletGameControl>() != null)
            {
                Debug.LogWarning("Scene already got AGCC!");
                return;
            }

            GameObject agc = (GameObject)Instantiate(Resources.Load("AGCC", typeof(GameObject)));
            agc.name = "ArcaletGameObject";
        }

        const float windowWidth = 420f;
        const float windowHeight = 480f;

        [MenuItem("Arcalet/Setting")]
        static void AgccSetting()
        {
            if (Instance == null)
            {
                logotexture = (Texture2D)Resources.Load("arcaletlogo", typeof(Texture2D));

                ArcaletEditor SettingWindows = (ArcaletEditor)EditorWindow.GetWindow(typeof(ArcaletEditor));
                SettingWindows.minSize = new Vector2(windowWidth, windowHeight);
                SettingWindows.maxSize = SettingWindows.minSize;
                GUIContent titleContent = new GUIContent(ArcaletReleaseV.Version);
                SettingWindows.titleContent = titleContent;
            }
            else {
                Instance.Close();
            }

        }

        static Texture2D logotexture = null;
        private static string gguid = "";
        private static string sguid = "";
        private static string userid = "";
        private static string password = "";
        private static string cert = "";
        string BtnSaveText = "Save";
        string BtnLoadText = "Load";
        AccountUser sAcountNow = null;
        List<AccountUser> accountlist = new List<AccountUser>() {
            new AccountUser("test0","test123")
        };

        void OnGUI()
        {
            GUILayout.Label(logotexture);
            GUILayout.Label("[Arcalet Game Setting]");
            GUILayout.Label("GGUID:");
            gguid = EditorGUILayout.TextField(gguid);
            GUILayout.Label("SGUID:");
            sguid = EditorGUILayout.TextField(sguid);
            GUILayout.Label("Certificate(type 1):");
            cert = EditorGUILayout.TextField(cert);
            //GUILayout.Label("");
            //GUILayout.Label("[SuperUser]");
            //DrawToolStrip();
            //GUILayout.Label("UserID:");
            //userid = EditorGUILayout.TextField(userid);
            //GUILayout.Label("Password:");
            //password = EditorGUILayout.PasswordField(password);
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(BtnSaveText, GUILayout.Height(30),GUILayout.Width(windowWidth/2f - 5f)))
            {
                ArcaletGameControl.mainGguid = gguid;
                ArcaletGameControl.mainSguid = sguid;
                ArcaletGameControl.mainCertificate = StringToByteArray(cert);

                Debug.LogWarning("Save Aracalet Done");
            }

            if (GUILayout.Button(BtnLoadText, GUILayout.Height(30), GUILayout.Width(windowWidth / 2f - 5f)))
            {
                Debug.LogWarning("Load Arcalet Done");

                gguid = ArcaletGameControl.mainGguid;
                sguid = ArcaletGameControl.mainSguid;
                //ArcaletGameControl.mainCertificate = StringToByteArray(cert);
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            Repaint();
        }


        void DrawToolStrip()
        {
            GUILayout.Label("Select Account:");
            string toolbarname = "";
            if(sAcountNow == null)
            {
                toolbarname = "null";
            }
            else
            {
                toolbarname = sAcountNow.Userid;
            }

            if (GUILayout.Button(toolbarname, EditorStyles.toolbarDropDown))
            {
                GenericMenu toolsMenu = new GenericMenu();
                if (accountlist.Count == 0)
                    toolsMenu.AddItem(new GUIContent("null"), false, null);
                else
                {
                    foreach(AccountUser acuser in accountlist)
                    {
                        toolsMenu.AddItem(new GUIContent(acuser.Userid), false, OnChangeUser, acuser);
                    }
                    //toolsMenu.AddItem(new GUIContent("Add"), false, OnChangeUser);
                }
                // Offset menu from right of editor window
                //toolsMenu.DropDown(new Rect(Screen.width - 216 - 40, 0, 0, 16));
                toolsMenu.ShowAsContext();
                GUIUtility.ExitGUI();
            }
        }

        void OnChangeUser(object acuser)
        {
            AccountUser AcUser = (AccountUser)acuser;
            sAcountNow = AcUser;

            userid = sAcountNow.Userid;
            password = sAcountNow.Passwd;
        }

        void AddUser()
        {
           //AccountUser AcUser = (AccountUser)acuser;
           //sAcountNow = AcUser;
           //
           //userid = sAcountNow.Userid;
           //password = sAcountNow.Passwd;
        }

        #region SaveData
        public static string GetEditorSaveData(guidtype _guild)
        {
            return EditorPrefs.GetString(_guild.ToString(), "");
        }

        public static string GetEditorSaveData(account _account)
        {
            return EditorPrefs.GetString(_account.ToString(), "");
        }

        public static void SetEditorSaveData(guidtype _guild, string value)
        {
            EditorPrefs.SetString(_guild.ToString(), "");
        }

        public static void SetEditorSaveData(account _account, string value)
        {
            EditorPrefs.SetString(_account.ToString(), value);
        }
        #endregion


        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }

    public class AccountUser
    {
        public string Userid = "";
        public string Passwd = "";

        public AccountUser(string _Userid, string _Passwd)
        {
            Userid = _Userid;
            Passwd = _Passwd;
        }
    }

    public enum guidtype { game, sscene, dscene, item, leaderboard };
    public enum account { userid, password };
}
