/**
 * Autogenerated by Thrift Compiler (0.9.3)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;

public partial class ThriftService {
  public interface Iface {
    string HelloString(string para);
    #if SILVERLIGHT
    IAsyncResult Begin_HelloString(AsyncCallback callback, object state, string para);
    string End_HelloString(IAsyncResult asyncResult);
    #endif
    Dictionary<string, TRtSnapshotData> readLatestData(List<string> codes);
    #if SILVERLIGHT
    IAsyncResult Begin_readLatestData(AsyncCallback callback, object state, List<string> codes);
    Dictionary<string, TRtSnapshotData> End_readLatestData(IAsyncResult asyncResult);
    #endif
    Dictionary<string, TRtHistoryData> readHistoryData(List<string> codes, int startTime, int endTime, int interval);
    #if SILVERLIGHT
    IAsyncResult Begin_readHistoryData(AsyncCallback callback, object state, List<string> codes, int startTime, int endTime, int interval);
    Dictionary<string, TRtHistoryData> End_readHistoryData(IAsyncResult asyncResult);
    #endif
  }

  public class Client : IDisposable, Iface {
    public Client(TProtocol prot) : this(prot, prot)
    {
    }

    public Client(TProtocol iprot, TProtocol oprot)
    {
      iprot_ = iprot;
      oprot_ = oprot;
    }

    protected TProtocol iprot_;
    protected TProtocol oprot_;
    protected int seqid_;

    public TProtocol InputProtocol
    {
      get { return iprot_; }
    }
    public TProtocol OutputProtocol
    {
      get { return oprot_; }
    }


    #region " IDisposable Support "
    private bool _IsDisposed;

    // IDisposable
    public void Dispose()
    {
      Dispose(true);
    }
    

    protected virtual void Dispose(bool disposing)
    {
      if (!_IsDisposed)
      {
        if (disposing)
        {
          if (iprot_ != null)
          {
            ((IDisposable)iprot_).Dispose();
          }
          if (oprot_ != null)
          {
            ((IDisposable)oprot_).Dispose();
          }
        }
      }
      _IsDisposed = true;
    }
    #endregion


    
    #if SILVERLIGHT
    public IAsyncResult Begin_HelloString(AsyncCallback callback, object state, string para)
    {
      return send_HelloString(callback, state, para);
    }

    public string End_HelloString(IAsyncResult asyncResult)
    {
      oprot_.Transport.EndFlush(asyncResult);
      return recv_HelloString();
    }

    #endif

    public string HelloString(string para)
    {
      #if !SILVERLIGHT
      send_HelloString(para);
      return recv_HelloString();

      #else
      var asyncResult = Begin_HelloString(null, null, para);
      return End_HelloString(asyncResult);

      #endif
    }
    #if SILVERLIGHT
    public IAsyncResult send_HelloString(AsyncCallback callback, object state, string para)
    #else
    public void send_HelloString(string para)
    #endif
    {
      oprot_.WriteMessageBegin(new TMessage("HelloString", TMessageType.Call, seqid_));
      HelloString_args args = new HelloString_args();
      args.Para = para;
      args.Write(oprot_);
      oprot_.WriteMessageEnd();
      #if SILVERLIGHT
      return oprot_.Transport.BeginFlush(callback, state);
      #else
      oprot_.Transport.Flush();
      #endif
    }

    public string recv_HelloString()
    {
      TMessage msg = iprot_.ReadMessageBegin();
      if (msg.Type == TMessageType.Exception) {
        TApplicationException x = TApplicationException.Read(iprot_);
        iprot_.ReadMessageEnd();
        throw x;
      }
      HelloString_result result = new HelloString_result();
      result.Read(iprot_);
      iprot_.ReadMessageEnd();
      if (result.__isset.success) {
        return result.Success;
      }
      throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "HelloString failed: unknown result");
    }

    
    #if SILVERLIGHT
    public IAsyncResult Begin_readLatestData(AsyncCallback callback, object state, List<string> codes)
    {
      return send_readLatestData(callback, state, codes);
    }

    public Dictionary<string, TRtSnapshotData> End_readLatestData(IAsyncResult asyncResult)
    {
      oprot_.Transport.EndFlush(asyncResult);
      return recv_readLatestData();
    }

    #endif

    public Dictionary<string, TRtSnapshotData> readLatestData(List<string> codes)
    {
      #if !SILVERLIGHT
      send_readLatestData(codes);
      return recv_readLatestData();

      #else
      var asyncResult = Begin_readLatestData(null, null, codes);
      return End_readLatestData(asyncResult);

      #endif
    }
    #if SILVERLIGHT
    public IAsyncResult send_readLatestData(AsyncCallback callback, object state, List<string> codes)
    #else
    public void send_readLatestData(List<string> codes)
    #endif
    {
      oprot_.WriteMessageBegin(new TMessage("readLatestData", TMessageType.Call, seqid_));
      readLatestData_args args = new readLatestData_args();
      args.Codes = codes;
      args.Write(oprot_);
      oprot_.WriteMessageEnd();
      #if SILVERLIGHT
      return oprot_.Transport.BeginFlush(callback, state);
      #else
      oprot_.Transport.Flush();
      #endif
    }

    public Dictionary<string, TRtSnapshotData> recv_readLatestData()
    {
      TMessage msg = iprot_.ReadMessageBegin();
      if (msg.Type == TMessageType.Exception) {
        TApplicationException x = TApplicationException.Read(iprot_);
        iprot_.ReadMessageEnd();
        throw x;
      }
      readLatestData_result result = new readLatestData_result();
      result.Read(iprot_);
      iprot_.ReadMessageEnd();
      if (result.__isset.success) {
        return result.Success;
      }
      throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "readLatestData failed: unknown result");
    }

    
    #if SILVERLIGHT
    public IAsyncResult Begin_readHistoryData(AsyncCallback callback, object state, List<string> codes, int startTime, int endTime, int interval)
    {
      return send_readHistoryData(callback, state, codes, startTime, endTime, interval);
    }

    public Dictionary<string, TRtHistoryData> End_readHistoryData(IAsyncResult asyncResult)
    {
      oprot_.Transport.EndFlush(asyncResult);
      return recv_readHistoryData();
    }

    #endif

    public Dictionary<string, TRtHistoryData> readHistoryData(List<string> codes, int startTime, int endTime, int interval)
    {
      #if !SILVERLIGHT
      send_readHistoryData(codes, startTime, endTime, interval);
      return recv_readHistoryData();

      #else
      var asyncResult = Begin_readHistoryData(null, null, codes, startTime, endTime, interval);
      return End_readHistoryData(asyncResult);

      #endif
    }
    #if SILVERLIGHT
    public IAsyncResult send_readHistoryData(AsyncCallback callback, object state, List<string> codes, int startTime, int endTime, int interval)
    #else
    public void send_readHistoryData(List<string> codes, int startTime, int endTime, int interval)
    #endif
    {
      oprot_.WriteMessageBegin(new TMessage("readHistoryData", TMessageType.Call, seqid_));
      readHistoryData_args args = new readHistoryData_args();
      args.Codes = codes;
      args.StartTime = startTime;
      args.EndTime = endTime;
      args.Interval = interval;
      args.Write(oprot_);
      oprot_.WriteMessageEnd();
      #if SILVERLIGHT
      return oprot_.Transport.BeginFlush(callback, state);
      #else
      oprot_.Transport.Flush();
      #endif
    }

    public Dictionary<string, TRtHistoryData> recv_readHistoryData()
    {
      TMessage msg = iprot_.ReadMessageBegin();
      if (msg.Type == TMessageType.Exception) {
        TApplicationException x = TApplicationException.Read(iprot_);
        iprot_.ReadMessageEnd();
        throw x;
      }
      readHistoryData_result result = new readHistoryData_result();
      result.Read(iprot_);
      iprot_.ReadMessageEnd();
      if (result.__isset.success) {
        return result.Success;
      }
      throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "readHistoryData failed: unknown result");
    }

  }
  public class Processor : TProcessor {
    public Processor(Iface iface)
    {
      iface_ = iface;
      processMap_["HelloString"] = HelloString_Process;
      processMap_["readLatestData"] = readLatestData_Process;
      processMap_["readHistoryData"] = readHistoryData_Process;
    }

    protected delegate void ProcessFunction(int seqid, TProtocol iprot, TProtocol oprot);
    private Iface iface_;
    protected Dictionary<string, ProcessFunction> processMap_ = new Dictionary<string, ProcessFunction>();

    public bool Process(TProtocol iprot, TProtocol oprot)
    {
      try
      {
        TMessage msg = iprot.ReadMessageBegin();
        ProcessFunction fn;
        processMap_.TryGetValue(msg.Name, out fn);
        if (fn == null) {
          TProtocolUtil.Skip(iprot, TType.Struct);
          iprot.ReadMessageEnd();
          TApplicationException x = new TApplicationException (TApplicationException.ExceptionType.UnknownMethod, "Invalid method name: '" + msg.Name + "'");
          oprot.WriteMessageBegin(new TMessage(msg.Name, TMessageType.Exception, msg.SeqID));
          x.Write(oprot);
          oprot.WriteMessageEnd();
          oprot.Transport.Flush();
          return true;
        }
        fn(msg.SeqID, iprot, oprot);
      }
      catch (IOException)
      {
        return false;
      }
      return true;
    }

    public void HelloString_Process(int seqid, TProtocol iprot, TProtocol oprot)
    {
      HelloString_args args = new HelloString_args();
      args.Read(iprot);
      iprot.ReadMessageEnd();
      HelloString_result result = new HelloString_result();
      result.Success = iface_.HelloString(args.Para);
      oprot.WriteMessageBegin(new TMessage("HelloString", TMessageType.Reply, seqid)); 
      result.Write(oprot);
      oprot.WriteMessageEnd();
      oprot.Transport.Flush();
    }

    public void readLatestData_Process(int seqid, TProtocol iprot, TProtocol oprot)
    {
      readLatestData_args args = new readLatestData_args();
      args.Read(iprot);
      iprot.ReadMessageEnd();
      readLatestData_result result = new readLatestData_result();
      result.Success = iface_.readLatestData(args.Codes);
      oprot.WriteMessageBegin(new TMessage("readLatestData", TMessageType.Reply, seqid)); 
      result.Write(oprot);
      oprot.WriteMessageEnd();
      oprot.Transport.Flush();
    }

    public void readHistoryData_Process(int seqid, TProtocol iprot, TProtocol oprot)
    {
      readHistoryData_args args = new readHistoryData_args();
      args.Read(iprot);
      iprot.ReadMessageEnd();
      readHistoryData_result result = new readHistoryData_result();
      result.Success = iface_.readHistoryData(args.Codes, args.StartTime, args.EndTime, args.Interval);
      oprot.WriteMessageBegin(new TMessage("readHistoryData", TMessageType.Reply, seqid)); 
      result.Write(oprot);
      oprot.WriteMessageEnd();
      oprot.Transport.Flush();
    }

  }


  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class HelloString_args : TBase
  {
    private string _para;

    public string Para
    {
      get
      {
        return _para;
      }
      set
      {
        __isset.para = true;
        this._para = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool para;
    }

    public HelloString_args() {
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.String) {
                Para = iprot.ReadString();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("HelloString_args");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (Para != null && __isset.para) {
          field.Name = "para";
          field.Type = TType.String;
          field.ID = 1;
          oprot.WriteFieldBegin(field);
          oprot.WriteString(Para);
          oprot.WriteFieldEnd();
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("HelloString_args(");
      bool __first = true;
      if (Para != null && __isset.para) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Para: ");
        __sb.Append(Para);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }


  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class HelloString_result : TBase
  {
    private string _success;

    public string Success
    {
      get
      {
        return _success;
      }
      set
      {
        __isset.success = true;
        this._success = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool success;
    }

    public HelloString_result() {
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 0:
              if (field.Type == TType.String) {
                Success = iprot.ReadString();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("HelloString_result");
        oprot.WriteStructBegin(struc);
        TField field = new TField();

        if (this.__isset.success) {
          if (Success != null) {
            field.Name = "Success";
            field.Type = TType.String;
            field.ID = 0;
            oprot.WriteFieldBegin(field);
            oprot.WriteString(Success);
            oprot.WriteFieldEnd();
          }
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("HelloString_result(");
      bool __first = true;
      if (Success != null && __isset.success) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Success: ");
        __sb.Append(Success);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }


  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class readLatestData_args : TBase
  {
    private List<string> _codes;

    public List<string> Codes
    {
      get
      {
        return _codes;
      }
      set
      {
        __isset.codes = true;
        this._codes = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool codes;
    }

    public readLatestData_args() {
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.List) {
                {
                  Codes = new List<string>();
                  TList _list4 = iprot.ReadListBegin();
                  for( int _i5 = 0; _i5 < _list4.Count; ++_i5)
                  {
                    string _elem6;
                    _elem6 = iprot.ReadString();
                    Codes.Add(_elem6);
                  }
                  iprot.ReadListEnd();
                }
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("readLatestData_args");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (Codes != null && __isset.codes) {
          field.Name = "codes";
          field.Type = TType.List;
          field.ID = 1;
          oprot.WriteFieldBegin(field);
          {
            oprot.WriteListBegin(new TList(TType.String, Codes.Count));
            foreach (string _iter7 in Codes)
            {
              oprot.WriteString(_iter7);
            }
            oprot.WriteListEnd();
          }
          oprot.WriteFieldEnd();
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("readLatestData_args(");
      bool __first = true;
      if (Codes != null && __isset.codes) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Codes: ");
        __sb.Append(Codes);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }


  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class readLatestData_result : TBase
  {
    private Dictionary<string, TRtSnapshotData> _success;

    public Dictionary<string, TRtSnapshotData> Success
    {
      get
      {
        return _success;
      }
      set
      {
        __isset.success = true;
        this._success = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool success;
    }

    public readLatestData_result() {
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 0:
              if (field.Type == TType.Map) {
                {
                  Success = new Dictionary<string, TRtSnapshotData>();
                  TMap _map8 = iprot.ReadMapBegin();
                  for( int _i9 = 0; _i9 < _map8.Count; ++_i9)
                  {
                    string _key10;
                    TRtSnapshotData _val11;
                    _key10 = iprot.ReadString();
                    _val11 = new TRtSnapshotData();
                    _val11.Read(iprot);
                    Success[_key10] = _val11;
                  }
                  iprot.ReadMapEnd();
                }
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("readLatestData_result");
        oprot.WriteStructBegin(struc);
        TField field = new TField();

        if (this.__isset.success) {
          if (Success != null) {
            field.Name = "Success";
            field.Type = TType.Map;
            field.ID = 0;
            oprot.WriteFieldBegin(field);
            {
              oprot.WriteMapBegin(new TMap(TType.String, TType.Struct, Success.Count));
              foreach (string _iter12 in Success.Keys)
              {
                oprot.WriteString(_iter12);
                Success[_iter12].Write(oprot);
              }
              oprot.WriteMapEnd();
            }
            oprot.WriteFieldEnd();
          }
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("readLatestData_result(");
      bool __first = true;
      if (Success != null && __isset.success) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Success: ");
        __sb.Append(Success);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }


  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class readHistoryData_args : TBase
  {
    private List<string> _codes;
    private int _startTime;
    private int _endTime;
    private int _interval;

    public List<string> Codes
    {
      get
      {
        return _codes;
      }
      set
      {
        __isset.codes = true;
        this._codes = value;
      }
    }

    public int StartTime
    {
      get
      {
        return _startTime;
      }
      set
      {
        __isset.startTime = true;
        this._startTime = value;
      }
    }

    public int EndTime
    {
      get
      {
        return _endTime;
      }
      set
      {
        __isset.endTime = true;
        this._endTime = value;
      }
    }

    public int Interval
    {
      get
      {
        return _interval;
      }
      set
      {
        __isset.interval = true;
        this._interval = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool codes;
      public bool startTime;
      public bool endTime;
      public bool interval;
    }

    public readHistoryData_args() {
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.List) {
                {
                  Codes = new List<string>();
                  TList _list13 = iprot.ReadListBegin();
                  for( int _i14 = 0; _i14 < _list13.Count; ++_i14)
                  {
                    string _elem15;
                    _elem15 = iprot.ReadString();
                    Codes.Add(_elem15);
                  }
                  iprot.ReadListEnd();
                }
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.I32) {
                StartTime = iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 3:
              if (field.Type == TType.I32) {
                EndTime = iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 4:
              if (field.Type == TType.I32) {
                Interval = iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("readHistoryData_args");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (Codes != null && __isset.codes) {
          field.Name = "codes";
          field.Type = TType.List;
          field.ID = 1;
          oprot.WriteFieldBegin(field);
          {
            oprot.WriteListBegin(new TList(TType.String, Codes.Count));
            foreach (string _iter16 in Codes)
            {
              oprot.WriteString(_iter16);
            }
            oprot.WriteListEnd();
          }
          oprot.WriteFieldEnd();
        }
        if (__isset.startTime) {
          field.Name = "startTime";
          field.Type = TType.I32;
          field.ID = 2;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32(StartTime);
          oprot.WriteFieldEnd();
        }
        if (__isset.endTime) {
          field.Name = "endTime";
          field.Type = TType.I32;
          field.ID = 3;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32(EndTime);
          oprot.WriteFieldEnd();
        }
        if (__isset.interval) {
          field.Name = "interval";
          field.Type = TType.I32;
          field.ID = 4;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32(Interval);
          oprot.WriteFieldEnd();
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("readHistoryData_args(");
      bool __first = true;
      if (Codes != null && __isset.codes) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Codes: ");
        __sb.Append(Codes);
      }
      if (__isset.startTime) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("StartTime: ");
        __sb.Append(StartTime);
      }
      if (__isset.endTime) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("EndTime: ");
        __sb.Append(EndTime);
      }
      if (__isset.interval) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Interval: ");
        __sb.Append(Interval);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }


  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class readHistoryData_result : TBase
  {
    private Dictionary<string, TRtHistoryData> _success;

    public Dictionary<string, TRtHistoryData> Success
    {
      get
      {
        return _success;
      }
      set
      {
        __isset.success = true;
        this._success = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool success;
    }

    public readHistoryData_result() {
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 0:
              if (field.Type == TType.Map) {
                {
                  Success = new Dictionary<string, TRtHistoryData>();
                  TMap _map17 = iprot.ReadMapBegin();
                  for( int _i18 = 0; _i18 < _map17.Count; ++_i18)
                  {
                    string _key19;
                    TRtHistoryData _val20;
                    _key19 = iprot.ReadString();
                    _val20 = new TRtHistoryData();
                    _val20.Read(iprot);
                    Success[_key19] = _val20;
                  }
                  iprot.ReadMapEnd();
                }
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("readHistoryData_result");
        oprot.WriteStructBegin(struc);
        TField field = new TField();

        if (this.__isset.success) {
          if (Success != null) {
            field.Name = "Success";
            field.Type = TType.Map;
            field.ID = 0;
            oprot.WriteFieldBegin(field);
            {
              oprot.WriteMapBegin(new TMap(TType.String, TType.Struct, Success.Count));
              foreach (string _iter21 in Success.Keys)
              {
                oprot.WriteString(_iter21);
                Success[_iter21].Write(oprot);
              }
              oprot.WriteMapEnd();
            }
            oprot.WriteFieldEnd();
          }
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("readHistoryData_result(");
      bool __first = true;
      if (Success != null && __isset.success) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Success: ");
        __sb.Append(Success);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
