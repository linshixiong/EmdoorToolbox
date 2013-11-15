using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;
using Newtonsoft.Json;




/// <summary>
/// 设备描述类
/// </summary>
public class DeviceInfo
{
    public DeviceInfo()
    {
    }

    private string id;

    /// <summary>
    /// 设备编号
    /// </summary>
  
    public string Id
    {
        get { return id; }
        set { id = value; }
    }
    private string name;
    /// <summary>
    /// 设备模块名称
    /// </summary>
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
   
    private string description;

    /// <summary>
    /// 描述
    /// </summary>
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    private string client;

    public string Client
    {
        get
        {
            return client;
        }
        set
        {
            this.client = value;
        }
    }


    private string fwListFile;

    public string FWListFile
    {
        get
        {
            return fwListFile;
        }
        set
        {
            this.fwListFile = value;
        }
    }

    private bool enable;

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enable
    {
        get { return enable; }
        set { enable = value; }
    }

    private long createTime;

    /// <summary>
    /// 创建时间
    /// </summary>
    public long CreateTime
    {
        get { return createTime; }
        set { createTime = value; }
    }



    public override string ToString()
    {
        string output = string.Empty;
        try
        {
            output = JsonConvert.SerializeObject(this);

            return output + "\n";
        }
        catch (Exception)
        {
            return null;
        }

    }


    public static DeviceInfo Pase(string json)
    {
        try
        {
            DeviceInfo device = (DeviceInfo)JsonConvert.DeserializeObject(json, typeof(DeviceInfo));
            return device;
        }
        catch (Exception)
        {
            return null;
        }
    }

}
