using System;
using System.Runtime.InteropServices;


namespace openPlant
{
    /// <summary>
    /// OPAPI 的摘要说明。
    /// </summary>
    ///	

    public class OPAPI
    {
        public OPAPI()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public const string dll_name = "opapi.dll";

        // 点的记录类型
        public const byte AX_TYPE = 0;            // 模拟类型
        public const byte DX_TYPE = 1;            // 数字类型
        public const byte I2_TYPE = 2;            // 组合类型
        public const byte I4_TYPE = 3;            // 节点类型

        // 字符串长度常量
        public const int OP_PN_LEN = 30;
        public const int OP_NAME_LEN = 16;
        public const int OP_FULL_LEN = 64;
        public const int OP_DESC_LEN = 32;
        public const int OP_CHAR_LEN = 16;
        public const int OP_EU_LEN = 8;
        public const int OP_ST_LEN = 6;

        public const ushort OP_TIMEOUT = 0x8000;    // 点值的超时，如果最高位为1，则为超时
        public const short OP_BAD = 0x0300;         // 点值的好坏，如果最高位为1，则为坏值

        // 历史类型及常量
        public const short HIS_VALUE_AVG = 0;       // 取平均值
        public const short HIS_VALUE_MAX = 1;       // 取最大值 
        public const short HIS_VALUE_MIN = 2;       // 取最小值 
        public const short HIS_VALUE_FLOW = 3;      // 取累计值 

        public const short HIS_VALUE_STAT = 4;      // 取所有统计值：累计值/最大/最小/累计时间
        public const short HIS_VALUE_SAMPLE = 5;    // 取采样值 
        public const short HIS_VALUE_SPAN = 6;    	// 取等间距值 
        public const short HIS_VALUE_PLOT = 7;    	// 取PLOT值，每区间包括（起始值，最大值，最小值） 

        // 统计结构定义
        public struct StatVal
        {
            public float flow;                       // 累积值
            public float max;                        // 最大值
            public float min;                        // 最小值
            public float seconds;                    // 累积时间，（注：平均值 = 累积值/累积时间）
            public int tm_tag;                       // 时间标签
        }
        // 节点信息
        public unsafe struct NodeInfo
        {
            public int id;						                // Global node ID
            public int parent_id;				                // Belonged unit ID
            public int type;
            public fixed sbyte name[OP_NAME_LEN + 1];			// Node name
            public fixed sbyte desc[OP_DESC_LEN + 1];			// Node description
            public int freq;
            public int compress;				                // compress or not
            public int access_time;			                    // access time
        } ;
        // 点信息
        public unsafe struct PointInfo
        {
            // common informations - 17 fields, 138+4 bytes
            public int id;						            //[ID] global point ID
            public int parent_id;					        //[ND] belonged node ID
            public byte point_type;					        //[PT] point type: REAL / CALC
            public byte record_type;				        //[RT] data type: AX / DX / I2 / I4
            public fixed sbyte name[OP_PN_LEN + 1];			    //[PN] point name
            public fixed sbyte alias[OP_PN_LEN + 1];			//[AN] point alias
            public fixed sbyte desc[OP_DESC_LEN + 1];		    //[ED] point description
            public fixed sbyte charst[OP_CHAR_LEN + 1];		    //[KR] byteacteristics
            public short freq;						        //[FQ] sample frequency (in seconds)
            public short ctrl;						        //[DU] controller 
            public int location;					        //[HW] hardware address
            public byte channel;					        //[BP] point bit
            public byte alarm_type;					        //[LC] type of alarm check
            public byte alarm_prior;				        //[AP] alarm priority
            public byte archived;					        //[SA] save to history or not;
            public int flags;						        //[FL] flags (reserved)
            public int create_time;				            //[CT] 

            // DX fields - 2 fields, 12+2 bytes                
            public fixed sbyte set[OP_ST_LEN + 1];			//[ST] description of digital point is set
            public fixed sbyte reset[OP_ST_LEN + 1];			//[RS] description of digital point is reset

            // AX fields - 12 fields, 44+1 bytes               
            public fixed sbyte eu[OP_EU_LEN + 1];			//[EU] name of engineering unit
            public short format;						    //[FM] display format 
            public Single iv;							    //[IV] initial value
            public Single bv;							    //[BV] bottom scale value
            public Single tv;							    //[TV] top scale value
            public Single ll;							    //[LL] low alarm limit
            public Single hl;							    //[HH] high alarm limit
            public Single zl;							    //[ZL] low low alarm limit
            public Single zh;							    //[ZH] high high alarm limit
            public Single deadband;					        //[DB] default 0.5%
            public byte db_type;					        //[DT] 0 - PCT, 1 - ENG
            public byte comp_type;					        //[KZ] 0 - deadband, 1 - linuar

            // stat & calc - 4 fields, 6 bytes             
            public byte stat_type;					        //[TT] use for stat points
            public short period;						    //[TP] use for stat points
            public short offset;						    //[TO] use for stat points
            public byte calc_type;					        //[KT] calculate type
            public int expression;					        //[KX] calculate expression
        }

        [DllImport(dll_name, EntryPoint = "op_init")]
        public static extern int op_init(string server, int port, int timeout); // 初始化连接

        [DllImport(dll_name, EntryPoint = "op_get_system_time")]
        public static extern int op_get_system_time(ref int t);     


        //组操作
        [DllImport(dll_name, EntryPoint = "op_new_group")]
        public static extern IntPtr op_new_group(int num);                    //我这里不给参数 ，有问题。 

        [DllImport(dll_name, EntryPoint = "op_add_group_point")]
        public static extern int op_add_group_point(IntPtr h, string name);

        [DllImport(dll_name, EntryPoint = "op_free_group")]
        public static extern int op_free_group(IntPtr h);


        //节点/点信息
        [DllImport(dll_name, EntryPoint = "op_get_node_id")]
        public static extern int op_get_node_id(string name, ref int id);

        [DllImport(dll_name, EntryPoint = "op_get_point_id")]
        public static extern int op_get_point_id(IntPtr gh, int[] pids);                 

        [DllImport(dll_name, EntryPoint = "op_get_point_info")]
        public static extern unsafe int op_get_point_info(IntPtr gh,ref PointInfo info);       //1   

        [DllImport(dll_name, EntryPoint = "op_select_node")]
        public static extern int op_select_node(string dbname, ref IntPtr hResult);

        [DllImport(dll_name, EntryPoint = "op_select_point")]
        public static extern int op_select_point(string dbname, ref IntPtr hResult);

        //读实时/历史数据
        [DllImport(dll_name, EntryPoint = "op_get_multi_value")]
        public static extern int op_get_multi_value(IntPtr gh, float[] values, short[] status);

        [DllImport(dll_name, EntryPoint = "op_read_value2")]
        public static extern int op_read_value2(int num, int[] ids, double[] values, short[] status, int[] t);

        [DllImport(dll_name, EntryPoint = "op_get_multi_hist")]
        public static extern int op_get_multi_hist(IntPtr gh, int t, byte[] rtype, float[] values, short[] status);

        [DllImport(dll_name, EntryPoint = "op_read_history2")]
        public static extern int op_read_history2(int t, int num, int[] ids, double[] values, short[] status);

        [DllImport(dll_name, EntryPoint = "op_select_history")]
        public static extern int op_select_history(IntPtr gh, byte[] rtype, short[] valtype, int beg_m, int end_m, int interval, IntPtr [] hResult);    //1

        //结果集操作
        [DllImport(dll_name, EntryPoint = "op_num_rows")]
        public static extern int op_num_rows(IntPtr hResult);

        [DllImport(dll_name, EntryPoint = "op_fetch_node_info")]
        public static extern int op_fetch_node_info(IntPtr hResult, ref NodeInfo info);

        [DllImport(dll_name, EntryPoint = "op_fetch_point_info")]
        public static extern int op_fetch_point_info(IntPtr hResult, ref PointInfo info);

        [DllImport(dll_name, EntryPoint = "op_fetch_timed_value")]
        public static extern int op_fetch_timed_value(IntPtr hResult, ref float values, ref short status, ref int tm_tag);

        [DllImport(dll_name, EntryPoint = "op_fetch_stat_value")]
        public static extern int op_fetch_stat_value(IntPtr hResult, ref StatVal val);

        [DllImport(dll_name, EntryPoint = "op_free_result")]
        public static extern int op_free_result(IntPtr hResult);


        //写实时数据
        [DllImport(dll_name, EntryPoint = "op_send_point_value")]
        public static extern int op_send_point_value(int node_id, int num, int[] pids, byte[] rtype, float[] values, int t);

        [DllImport(dll_name, EntryPoint = "op_send_point_value_ex")]
        public static extern int op_send_point_value_ex(int num, int[] pids, double[] values, short[] status, int t, int pt);

        //写历史 数据
        [DllImport(dll_name, EntryPoint = "op_write_history2")]
        public static extern int op_write_history2(int t, int num, int[] pids, double[] values, short []stauts);

        [DllImport(dll_name, EntryPoint = "op_write_history2")]
        public static extern int op_write_history_ex2(int id, int num, int[] t, double[] values, short[] status);

        //时间操作
        [DllImport(dll_name, EntryPoint = "encode_time")]
        public static extern int encode_time(int yy, int mm, int dd, int hh, int mi, int ss);

        [DllImport(dll_name, EntryPoint = "decode_time")]
        public static extern int decode_time(int t, ref int yy, ref int mm, ref int dd, ref int hh, ref int mi, ref int ss);
    }
}

