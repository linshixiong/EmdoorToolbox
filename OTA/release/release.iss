; -- Example1.iss --
; Demonstrates copying 3 files and creating an icon.

; SEE THE DOCUMENTATION FOR DETAILS ON CREATING .ISS SCRIPT FILES!

[Setup]
AppName=Emdoor OTA Tools
AppVersion=1.0
DefaultDirName={pf}\Emdoor\OTA Tools
DefaultGroupName=Emdoor OTA Tools
UninstallDisplayIcon={app}\OTATools.exe
Compression=lzma2
SolidCompression=yes
ShowLanguageDialog=yes
OutputDir=.
AppCopyright=Copyright (C) 2013 Emdoor,Ltd.



[Languages]
Name: chs; MessagesFile: compiler:Default.isl


[Files]
Source: "E:\Projects\EMDOOR_OTA\Tools\OTATools\bin\Release\OTATools.exe"; DestDir: "{app}";Flags:ignoreversion
Source: "E:\Projects\EMDOOR_OTA\Tools\OTATools\bin\Release\OTATools.exe.config"; DestDir: "{app}";Flags:ignoreversion
Source: "E:\Projects\EMDOOR_OTA\Tools\OTATools\bin\Release\zh-CN\OTATools.resources.dll"; DestDir: "{app}\zh-CN";Flags:ignoreversion
Source: "E:\Projects\EMDOOR_OTA\Tools\OTATools\bin\Release\Ionic.Zip.dll"; DestDir: "{app}";Flags:ignoreversion
Source: "E:\Projects\EMDOOR_OTA\Tools\OTATools\bin\Release\Newtonsoft.Json.dll"; DestDir: "{app}";Flags:ignoreversion
Source: "manual.pdf"; DestDir: "{app}"; Flags:ignoreversion
Source: "readme.txt"; DestDir: "{app}"; Flags:ignoreversion isreadme


[Icons]
Name: "{group}\Emdoor OTA Tools(English)"; Filename: "{app}\OTATools.exe";Parameters:"en-US"
Name: "{group}\Emdoor OTA Tools(¼òÌåÖÐÎÄ)"; Filename: "{app}\OTATools.exe"
Name: "{group}\Manual"; Filename: "{app}\manual.pdf"
Name: "{group}\Uninstall"; Filename: "{app}\unins000.exe"
Name: "{commondesktop}\Emdoor OTA Tools"; Filename: "{app}\OTATools.exe";Parameters:"en-US"

[Code]

function InitializeSetup: Boolean;

    var Path:string ;

    ResultCode: Integer;

    dotNetV2RegPath:string;

    dotNetV2DownUrl:string;

    dotNetV2PackFile:string;

begin

  dotNetV2RegPath:='SOFTWARE\Microsoft\.NETFramework\policy\v2.0';

  dotNetV2DownUrl:='http://www.xxx.com/down/dotNetFx_v2.0(x86).exe';

  dotNetV2PackFile:='{src}/dotnetfx.exe';

  if RegKeyExists(HKLM, dotNetV2RegPath) then

  begin

    Result := true;

  end

  else

  begin

    if MsgBox('System detected you have not install .Net Framework2.0 environment, install now?', mbConfirmation, MB_YESNO) = idYes then

    begin



      Path := ExpandConstant(dotNetV2PackFile);

      if(FileOrDirExists(Path)) then

      begin

        Exec(Path, '/q', '', SW_SHOWNORMAL, ewWaitUntilTerminated, ResultCode);

        if RegKeyExists(HKLM, dotNetV2RegPath) then

        begin

           Result := true;

        end

        else

        begin

           MsgBox('Not installed the .Net Framework2.0 , it will not be able to run, the setup program will exit!',mbInformation,MB_OK);

        end

      end

      else

      begin

        if MsgBox('Software installation directory does not contain the .Net Framework setup file, immediately after the download installation?', mbConfirmation, MB_YESNO) = idYes then

        begin

          Path := ExpandConstant('{pf}/Internet Explorer/iexplore.exe');

          Exec(Path, dotNetV2DownUrl , '', SW_SHOWNORMAL, ewWaitUntilTerminated, ResultCode);

          MsgBox('Please install the .Net Framework2.0 environment, then run the installer program!',mbInformation,MB_OK);

          Result := false;

        end

        else

        begin

          MsgBox('Not installed the .Net Framework2.0 , it will not be able to run, the setup program will exit!',mbInformation,MB_OK);

          Result := false;

        end

      end

    end

    else

    begin

      MsgBox('Not installed the .Net Framework2.0 , it will not be able to run, the setup program will exit!',mbInformation,MB_OK);

      Result := false;

    end;

  end;

end;

