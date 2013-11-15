using System;
using System.Data;
using System.IO;
using System.Text;
using Newtonsoft.Json;


/// <summary>
/// Summary description for Firmware
/// </summary>
public class UpdateFileInfo
{
	public UpdateFileInfo()
	{	
	}

    private string id;

    public string Id
    {
        get
        {
            return id;
        }
        set
        {
            this.id = value;
        }
    }


    private string modelId;

    public string ModelId
    {

        get
        {
            return modelId;
        }
        set
        {
            this.modelId = value;
        }
    }



    private long version;

    public long Version
    {
        get { return version; }
        set { version = value; }
    }
    private string fileName;

    public string FileName
    {
        get { return fileName; }
        set { fileName = value; }
    }

    private long fileSize;

    public long FileSize
    {
        get { return fileSize; }
        set { fileSize = value; }
    }

    private string md5;

    public string Md5
    {
        get { return md5; }
        set { md5 = value; }
    }
    private long releaseTime;

    public long ReleaseTime
    {
        get { return releaseTime; }
        set { releaseTime = value; }
    }


    private string description;

    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    private string changeLog;

    public string ChangeLog
    {
        get { return changeLog; }
        set { changeLog = value; }
    }


    private bool enable;

    public bool Enable
    {
        get
        {
            return enable;
        }
        set
        {
            this.enable = value;
        }
    }


    private bool additional;

    public bool Additional
    {
        get
        {
            return additional;
        }
        set
        {
            this.additional = value;
        }
    }

    private string buildNumber;

    public string BuildNumber
    {
        get
        {
            return buildNumber;
        }
        set
        {
            this.buildNumber = value;
        }
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

    


    public static UpdateFileInfo Pase(string json)
    {
        try
        {
            UpdateFileInfo firmware = (UpdateFileInfo)JsonConvert.DeserializeObject(json, typeof(UpdateFileInfo));
            return firmware;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
