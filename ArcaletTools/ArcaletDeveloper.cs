using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ArcaletTools
{
    class ArcaletDeveloper
    {
        /*
        internal static int FindOmis(_Omis_ omis)
        {
            int errCode;

            if (omis.omisFound) return 0; // 先前已經呼叫過FindOmis()，不再重複呼叫 

            try
            {

                System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

                XmlUrlResolver resolver = new XmlUrlResolver();
                XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();

                xmlReaderSettings.XmlResolver = resolver;
                xmlReaderSettings.IgnoreWhitespace = true;

                XmlReader reader;


                if (_UWWW_.instance.uwpHandle == null)
                {
                    reader = XmlReader.Create(URLGETOMIS, xmlReaderSettings);
                }
                else { // flash or unity web player 
                    byte[] resp = _UWWW_.instance.uwpHandle.wwwRequestBytes(URLGETOMIS);
                    Stream respr = new System.IO.MemoryStream(resp);
                    reader = XmlReader.Create(respr, xmlReaderSettings);
                }

                while (reader.Read())
                {
                    if (reader.Name == "server" && reader.NodeType == XmlNodeType.Element)
                    { // 2.1.1.1329 改版時加上 
                        String systime = reader.GetAttribute("systime");
                        ArcaletSystem.SetServerTime(systime);
                    }
                    else if (reader.Name == "connect")
                    {
                        omis.omisSrv = int.Parse(reader.GetAttribute("srv"));
                        omis.omisHostName = reader.GetAttribute("hostname");
                        omis.omisSno = int.Parse(reader.GetAttribute("sno"));


                        // omis.omisHostName = "h169.a8.arcalet.com"; 
                        // omis.omisSrv = 5; 
                        // omis.omisSno = 5; 

                        //omis.omisIP = reader.GetAttribute("ip"); 

                        //omis.omisSrv = 3; 
                        //omis.omisIP = "192.168.1.201"; 
                        //omis.omisIP = "60.199.169.6"; 
                        //omis.omisPort[0] = 168; 
                        //omis.omisPort[1] = 168; 
                        //omis.omisPort[2] = 168;

                        omis.omisPort[0] = int.Parse(reader.GetAttribute("port"));
                        omis.omisPort[1] = int.Parse(reader.GetAttribute("port2"));
                        omis.omisPort[2] = int.Parse(reader.GetAttribute("port3"));
                        omis.omisFound = true;

                        return 0;
                    }
                    else if (reader.Name == "err")
                    {
                        errCode = int.Parse(reader.GetAttribute("no"));
                        //errMsg =reader.ReadString(); 
                        return 10000 + errCode;
                    }
                }

                return 1;
            }
            catch (Exception e)
            {
                _APIERR_.instance.Message = e.ToString();
                return -2;
            }
        }*/
    }
}
