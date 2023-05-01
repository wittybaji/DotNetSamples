using System;
using System.Collections.Generic;

namespace EnumTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> debugLogTypeEnumList = new List<string>();
            foreach (var logType in Enum.GetValues(typeof(DebugLogTypeEnum)))
            {
                debugLogTypeEnumList.Add(logType.ToString());
            }
            debugLogTypeEnumList.Sort();
            string result = "";
            foreach (var type in debugLogTypeEnumList)
            {
                result += type + "," + Environment.NewLine;
            }
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
    enum DebugLogTypeEnum : byte
    {
        AtsManufacturer,
        Main,
        Init,
        InnerCommand,
        InnerStatus,
        OutStatus,
        FromDsu,
        FromZc,
        FromVobc,
        FromAom,
        FromCi,
        ToDsu,
        ToZc,
        ToVobc,
        ToCi,
        ToAom,
        Pipe,
        HostStand,
        Departlist,
        DepartListControl,
        TrainGraph,
        Dispatch,
        DispatchCmd,
        DispatchControl,
        WashTrain,
        ReadyTrain,
        RtdbHeart,
        Mid,
        Pis,
        Pa,
        TETRA,
        Report,
        Wcf,
        Warn,
        Exception,
        TrainTrace,
        OrderManage,
        TrainStationControl,
        SendCiData,
        SendTrainTrace,
        SendTrainSyn,
        SendLineFieldSyn,
        Alarm,
        FireObstacle,
        SendTdt,
        TrainSyn,
        UpLoad,
        TrainAllStatus,
        PlatFormStatus,
        AutoDispatch,
        Mss,
        FormDisplay,
        PowerOnStatus,
        AssignAbout,
        WcfServiceState,
        WcfConnect,
        App,
        Midlogandnetstate,
        Wcfreceivestate,
        Wcfpushstate,
        Udpmidcomm,
        Com,
        Tdt,
        Tdtcommand,
        Tdtheart,
        RepairGeteway,
        TrainBasic,
        FrontComReceive,
        InterfaceClassLibrary,
        SystemParam,
        EvenlySpaced,
        ComBase,
        trainRunInfoManage,
        MemoryAndCpu,
        doorStatus,
        Tcp,
        UserLogin,
        Playback,
        AtsToAts,
        TrainNum,
        DrawLineField,
        CTTrainTrace,
        Test,
        ThreadMonitor,
        weilianAlarm,
        SystemInfo,
        RouteSyn,
        EarlyOrLate,
        Lock,
        ToOtherInnerSubsystem,
        SendToRtdbData,//miao mark 增加发送到RTDB的log
        FromRTDB, // miao mark 从RTDB收到的
        FromInterface,//zhangchao 收到接口机的数据
        ToInterface, //zhangchao 发送接口机的数据
        RSSPRecord, //Rssp日志
        CrossLineJump,
        FromPsd,
        ToPsd,
        ElectricMeter,
        MDIAS,
        InnerParsePack,
        LightAndButton,
        /// <summary>
        /// 数据配置错误（数据错误）
        /// </summary>
        DataError,
        KVM,
        ActGraph,
        DeviceWorkStatus,
        IntelligentDispatch
    }
}
