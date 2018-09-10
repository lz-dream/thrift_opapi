using System;
using Thrift;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;
using System.Collections.Generic;
using openPlant;
using System.IO;
using LitJson;
using static Thrift.Protocol.TBinaryProtocol;

namespace Thrift_Opapi
{
    class OpapiServerImp : ThriftService.Iface
    {
        //从Config中获取接口地址、端口等信息
        Config config = JsonMapper.ToObject<Config>(File.ReadAllText("config.txt"));

        public string HelloString(string para)
        {
            Console.WriteLine("HelloString方法被调用");
            OPAPI.op_init(config.host, config.port, config.timeout);
            IntPtr h = new IntPtr();
            int num = 1, beg_time, end_time, interval;
            int rc,rc2, numRec;

            IntPtr[] hResult = new IntPtr[num];
            IntPtr[] hResult2 = new IntPtr[num];
            byte[] types = new byte[num];
            float value = new float();
            short status = new short();
            int tm_tag = new int();
            string[] names = new string[num];

            //初始化组
            h = OPAPI.op_new_group(num);
            names[0] = "OP.1DCS.01BHT03AI01_XQ01";
            for (int i = 0; i < num; i++)
            {
                OPAPI.op_add_group_point(h, names[i]);
                types[i] = OPAPI.AX_TYPE;
            }

            //初始化历史数据类型
            short[] valtype = new short[num];                   //历史数据类型，平均，最大，最小。。。。。。。
            valtype[0] = OPAPI.HIS_VALUE_SAMPLE;

            interval = 2;
            beg_time = 1535868120;
            end_time = 1535868180;
            Console.WriteLine("开始时间：" + beg_time + "  结束时间：" + end_time);
            //请求历史数据
            rc = OPAPI.op_select_history(h, types, valtype, beg_time, end_time, interval, hResult);
            rc2 = OPAPI.op_select_history(h, types, valtype, beg_time+1, end_time-1, interval, hResult2);
            if (rc != 0 && rc2 !=0)
            {
                return null;
            }
            int yy = new int();
            int mm = new int();
            int dd = new int();
            int hh = new int();
            int mi = new int();
            int ss = new int();

            for (int i = 0; i < num; i++)
            {
                numRec = OPAPI.op_num_rows(hResult[i]);
                int numRec2 = OPAPI.op_num_rows(hResult2[i]);
                if (numRec < 0 && numRec2<0)
                    continue;

                Console.WriteLine("\n" + names[i] + " have " + (numRec+numRec2).ToString() + " records"+ numRec2);
                for (int j = 0; j < numRec; j++)
                {
                    OPAPI.op_fetch_timed_value(hResult[i], ref value, ref status, ref tm_tag);        //不对结果判断了， 比较影响性能。
                    
                    OPAPI.decode_time(tm_tag, ref yy, ref mm, ref dd, ref hh, ref mi, ref ss);
                    Console.WriteLine("\n\t" + yy.ToString() + "-" + mm.ToString() + "-" + dd.ToString() + " " + hh.ToString() + ":" +
                        mi.ToString() + ":" + ss.ToString() + " value=" + value.ToString() + ", status=" + status.ToString());
                    if (j<numRec2) {
                        OPAPI.op_fetch_timed_value(hResult2[i], ref value, ref status, ref tm_tag);
                        OPAPI.decode_time(tm_tag, ref yy, ref mm, ref dd, ref hh, ref mi, ref ss);
                        Console.WriteLine("\n\t" + yy.ToString() + "-" + mm.ToString() + "-" + dd.ToString() + " " + hh.ToString() + ":" +
                            mi.ToString() + ":" + ss.ToString() + " value=" + value.ToString() + ", status=" + status.ToString());
                    }                   
                }
                //释放结果集！
                OPAPI.op_free_result(hResult[i]);
            }

            //释放组 ！
            OPAPI.op_free_group(h);

            return "服务端返回:" + config.host+"  "+ config.port + "  "+ config.timeout;
        }

        //重写读取历史值的方法
        public Dictionary<string, TRtHistoryData> readHistoryData(List<string> codes, int startTime, int endTime, int interval)
        {
            Console.WriteLine("readHistoryData方法被调用");
            OPAPI.op_init(config.host, config.port, config.timeout);
            IntPtr h = new IntPtr();
            int rc, rc2, numRec;

            string[] names = codes.ToArray();
            int num = codes.Count;
            IntPtr[] hResult = new IntPtr[num];
            IntPtr[] hResult2 = new IntPtr[num];
            byte[] types = new byte[num];
            float value = new float();
            short status = new short();
            int tm_tag = new int();
            
            

            //初始化组 和 历史数据
            h = OPAPI.op_new_group(num);
            short[] valtype = new short[num];
            for (int i = 0; i < num; i++)
            {
                OPAPI.op_add_group_point(h, names[i]);
                valtype[i] = OPAPI.HIS_VALUE_SAMPLE; //存放历史值类型，？？写死了取采样值 
            }
            OPAPI.PointInfo[] pointInfos= new OPAPI.PointInfo[num];
            OPAPI.op_get_point_info(h, ref pointInfos[0]);
            for (int i = 0; i < num; i++)
            {
                types[i] = pointInfos[i].record_type;
            }
            //开始处理结果集
            Dictionary<string, TRtHistoryData> keyValuePairs = new Dictionary<string, TRtHistoryData>();
            Console.WriteLine("interval: "+interval);
            if (interval > 1)
            {
                Console.WriteLine("开始时间：" + startTime + "  结束时间：" + endTime);
                //请求历史数据
                long start1 = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
                rc = OPAPI.op_select_history(h, types, valtype, startTime, endTime, interval, hResult);
                long end1  = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
                Console.WriteLine("调用接口用时，"+end1+"--"+start1+" = "+(end1-start1));
                if (rc != 0 )
                {
                    return null;
                }
                long start2 = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
                for (int i = 0; i < num; i++)
                {
                    TRtHistoryData rtHistoryData = new TRtHistoryData();
                    List<TRtValue> rtValues = new List<TRtValue>();
                    rtHistoryData.Code = names[i];
                    if (pointInfos[i].id < 0) //测点不存在
                    {
                        rtHistoryData.State = RtDataState.CODE_NOT_EXIST.ToString();
                        keyValuePairs.Add(names[i], rtHistoryData);
                        continue;
                    }
                    numRec = OPAPI.op_num_rows(hResult[i]);
                    if (numRec < 0)
                        continue;

                    Console.WriteLine("\n" + names[i] + " have " + (numRec).ToString() + " records"+"。测点类型："+types[i]);
                    
                    for (int j = 0; j < numRec; j++)
                    {
                        OPAPI.op_fetch_timed_value(hResult[i], ref value, ref status, ref tm_tag);        //不对结果判断了， 比较影响性能。
                        TRtValue rtValue = new TRtValue();
                        rtValue.Time = ((long)tm_tag) * 1000;
                        rtValue.Value = value.ToString();
                        rtValues.Add(rtValue);
                        
                    }

                    if (rtValues.Count == 0)
                    {
                        rtHistoryData.State = RtDataState.CODE_NO_RTDB.ToString();
                    }
                    
                    rtHistoryData.Data = rtValues;
                    keyValuePairs.Add(names[i], rtHistoryData);
                    //释放结果集！
                    OPAPI.op_free_result(hResult[i]);
                }
                long end2 = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
                Console.WriteLine("调用接口用时，" + end2 + "--" + start2+" = "+(end2-start2));
            }
            else {
                Console.WriteLine("开始时间：" + startTime + "  结束时间：" + endTime);
                //请求历史数据
                rc = OPAPI.op_select_history(h, types, valtype, startTime, endTime, 2, hResult);
                rc2 = OPAPI.op_select_history(h, types, valtype, startTime + 1, endTime - 1, 2, hResult2);
                if (rc != 0 && rc2 != 0)
                {
                    return null;
                }


                for (int i = 0; i < num; i++)
                {
                    TRtHistoryData rtHistoryData = new TRtHistoryData();
                    List<TRtValue> rtValues = new List<TRtValue>();
                    rtHistoryData.Code = names[i];
                    if (pointInfos[i].id<0)
                    {
                        rtHistoryData.State = RtDataState.CODE_NOT_EXIST.ToString();
                        keyValuePairs.Add(names[i], rtHistoryData);
                        continue;
                    }

                    numRec = OPAPI.op_num_rows(hResult[i]);
                    Console.WriteLine("numRec: " + numRec);
                    int numRec2 = OPAPI.op_num_rows(hResult2[i]);
                    if (numRec < 0 && numRec2 < 0)
                        continue;

                    Console.WriteLine("\n" + names[i] + " have " + (numRec + numRec2).ToString() + " records"+ "。测点类型：" + types[i]);
                    
                    for (int j = 0; j < numRec; j++)
                    {
                        OPAPI.op_fetch_timed_value(hResult[i], ref value, ref status, ref tm_tag);        //不对结果判断了， 比较影响性能。
                        TRtValue rtValue = new TRtValue();
                        rtValue.Time =   ((long)tm_tag) * 1000;
                        rtValue.Value = value.ToString();
                        rtValues.Add(rtValue);
                        if (j < numRec2)
                        {
                            try
                            {
                                OPAPI.op_fetch_timed_value(hResult2[i], ref value, ref status, ref tm_tag);
                                TRtValue rtValue2 = new TRtValue();
                                rtValue2.Time = ((long)tm_tag) * 1000;
                                rtValue2.Value = value.ToString();
                                rtValues.Add(rtValue2);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("error");
                            }
                            
                        }

                    }
                    rtHistoryData.Data = rtValues;
                    keyValuePairs.Add(names[i], rtHistoryData);
                    //释放结果集！
                    OPAPI.op_free_result(hResult2[i]);
                    OPAPI.op_free_result(hResult[i]);
                }
            }
            //释放组 ！
            OPAPI.op_free_group(h);
            long end3 = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
            Console.WriteLine("服务器执行结束: " + end3);
            return keyValuePairs;
        }

        //重写读取实时值的方法
        public Dictionary<string, TRtSnapshotData> readLatestData(List<string> codes)
        {
            Console.WriteLine("readLatestData方法被调用");
            OPAPI.op_init(config.host, config.port, config.timeout);
            IntPtr h = new IntPtr();
            int rc, num = codes.Count;
            double[] values = new double[num];
            short[] status = new short[num];
            string[] names = codes.ToArray();


            //初始化组
            h = OPAPI.op_new_group(num);
            for (int i = 0; i < num; i++)
            {
                OPAPI.op_add_group_point(h, names[i]);
            }
            //@ 2 取实时值
            int[] pids = new int[num];
            int[] ptime = new int[num];
            rc = OPAPI.op_get_point_id(h, pids);
            if (rc != 0)
            {
                return null;
            }
            rc = OPAPI.op_read_value2(num, pids, values, status, ptime);
            if (rc != 0)
            {
                return null;
            }
            //@取值结束
            Dictionary<string, TRtSnapshotData> keyValuePairs = new Dictionary<string, TRtSnapshotData>();
            for (int i = 0; i < num; i++)
            {
                TRtSnapshotData rt = new TRtSnapshotData();
                rt.Code = names[i];
                if (pids[i]<0)
                {
                    rt.State = RtDataState.CODE_NOT_EXIST.ToString();
                    keyValuePairs.Add(names[i], rt);
                    continue;
                }
                if (ptime[i] > 0) {
                    TRtValue rtValue = new TRtValue();
                    rtValue.Time = ptime[i];
                    rtValue.Value = values[i].ToString();
                    rt.Data = rtValue;
                }
                else
                {
                    rt.State = RtDataState.CODE_NO_RTDB.ToString();
                }
                
                keyValuePairs.Add(names[i], rt);

                Console.WriteLine("\n" + names[i] + " : ");
                Console.WriteLine(ptime[i]+" "+values[i].ToString() + " " + status[i].ToString());
            }

            //释放组 ！
            OPAPI.op_free_group(h);
            return keyValuePairs;
        }
    }
    public class OpapiRun
    {
        

        public static void Main()
        {
            Config config = JsonMapper.ToObject<Config>(File.ReadAllText("config.txt"));
            OPAPI.op_init(config.host, config.port, config.timeout);
            int num = 2;
            string[] name = new string[num];
            name[0] = "aaaa";
            name[1] = "OP.1DCS.01BHT03DB_OUT";
            IntPtr h = new IntPtr();
            int[] ids = new int[num];
            OPAPI.PointInfo[] point = new OPAPI.PointInfo[num];
            h= OPAPI.op_new_group(num);
            for (int i = 0; i < num; i++)
            {
                OPAPI.op_add_group_point(h, name[i]);
            
            }
            int rc = OPAPI.op_get_point_info(h, ref point[0]);
            for (int i = 0; i < num; i++)
            {
                Console.WriteLine(point[i].id);
            }
            

            
            try
            {

                OpapiServerImp opapi = new OpapiServerImp();
                ThriftService.Processor processor = new ThriftService.Processor(opapi);
                TServerTransport serverTransport = new TServerSocket(9090);
                TProtocolFactory protocolFactory = new TCompactProtocol.Factory();
                //TProtocolFactory protocolFactory = new TJSONProtocol.Factory();
                TServer server = new TThreadPoolServer(processor, serverTransport, new TTransportFactory(), protocolFactory);




                //ThriftService.Processor processor = new ThriftService.Processor(opapi);
                //TServerTransport serverTransport = new TServerSocket(9090);
                //TServer server = new TThreadPoolServer(processor, serverTransport);
                // Use this for a multithreaded server
                // server = new TThreadPoolServer(processor, serverTransport);
                Console.WriteLine("Starting the server...");
                server.Serve();
               
            }
            catch (Exception x)
            {
                Console.WriteLine(x.StackTrace);
            }
            Console.WriteLine("done.");
        }
    }
}

public enum RtDataState
{
    CODE_NOT_EXIST,
    CODE_NO_ALIAS,
    CODE_NO_RTDB,
    DATA_INVALID,
    RTDB_INVALID,
    RTDB_ERROR,
    RTDB_UNSUPPORT,
    DS_NO_ALIAS,
    DS_CONNECT_FAIL,
    DS_CONNECT_INTERRUPT,
    DS_PROCESS_FAIL,
    DS_ERROR,
    DS_UNSUPPORT,
    ERROR
}
