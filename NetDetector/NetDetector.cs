using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace NetDetector
{
    internal class NetDetector
    {
        #region 常量定义

        //Local system uses a modem to connect to the Internet.
        private const long INTERNET_CONNECTION_MODEM = 1;
        //Local system uses a local area network to connect to the Internet.
        private const long INTERNET_CONNECTION_LAN = 2;
        //Local system uses a proxy server to connect to the Internet.
        private const long INTERNET_CONNECTION_PROXY = 4;
        //No longer used.
        private const long INTERNET_CONNECTION_MODEM_BUSY = 8;
        //Local system has RAS installed.
        private const long INTERNET_RAS_INSTALLED = 16;
        //Local system is in offline mode.
        private const long INTERNET_CONNECTION_OFFLINE = 32;
        //Local system has a valid connection to the Internet, but it might or might not be currently connected.
        private const long INTERNET_CONNECTION_CONFIGURED = 64;

        #endregion

        [DllImport("wininet")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);

        /// <summary>
        /// 检测本机是否联网
        /// </summary>
        /// <param name="connectionType">上网方式</param>
        /// <returns></returns>
        public static bool IsConnectedInternet(out string connectionType)
        {
            connectionType = "";

            //true:已联网,false:未联网
            int status = 0;
            bool connected = InternetGetConnectedState(out status, 0);

            if (connected)
                connectionType = "网络连接正常!";
            else
                connectionType = "网络连接不可用!";

            //if ((INTERNET_CONNECTION_MODEM & status) > 0)
            //{
            //    connectionType = "modem";
            //}
            //if ((INTERNET_CONNECTION_LAN & status) > 0)
            //{
            //    connectionType = "lan";
            //}
            //if ((INTERNET_CONNECTION_MODEM_BUSY & status) > 0)
            //{
            //    connectionType = "busy";
            //}
            //if ((INTERNET_CONNECTION_PROXY & status) > 0)
            //{
            //    connectionType = "proxy";
            //}

            if ((status & INTERNET_CONNECTION_OFFLINE) > 0)
                connectionType += "OFFLINE 本地系统处于离线模式。";
            if ((status & INTERNET_CONNECTION_MODEM) > 0)
                connectionType += "Modem 本地系统使用调制解调器连接到互联网。";
            if ((status & INTERNET_CONNECTION_LAN) > 0)
                connectionType += "LAN 本地系统使用的局域网连接到互联网。";
            if ((status & INTERNET_CONNECTION_PROXY) > 0)
                connectionType += "a Proxy";
            if ((status & INTERNET_CONNECTION_MODEM_BUSY) > 0)
                connectionType += "Modem but modem is busy";

            return connected;
        }

        /// <summary>
        /// 检测/监控远程连接网络端口的情况（例如：3389端口是否处于监听状态，是否建立了连接等）。
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTcpConnections()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();

            string tmpStr;
            List<string> connectionList = new List<string>();

            TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();
            foreach (TcpConnectionInformation t in connections)
            {
                tmpStr = string.Format("{0} <------> {1}状态：{2}", t.LocalEndPoint, t.RemoteEndPoint, t.State);
                connectionList.Add(tmpStr);
            }
            return connectionList;
        }
    }
}
