; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "cor3-gen"
#define MyAppVersion "0.1"
#define MyAppPublisher "tfwio"
#define MyAppURL "http://github.com/tfwio/cor3-gen"
#define MyAppExeName "GeneratorTool.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{893BFA31-F886-42BA-BAD7-82D1DA7A34D6}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\Cor3 Generator Tool
DefaultGroupName=Cor3 Generator
AllowNoIcons=yes
OutputDir=.\setup
OutputBaseFilename=setup-cor3-gen-20140529
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\GeneratorTool.exe"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\AvalonDock.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\Generator.Lib.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\System.Cor3.Data.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\System.Cor3.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\AvalonDock.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\EntityFramework.SqlServer.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\EntityFramework.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\FirstFloor.ModernUI.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\ICSharpCode.AvalonEdit.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\System.Data.SQLite.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\AvalonDock.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\AvalonDock.Themes.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\EntityFramework.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\EntityFramework.SqlServer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\FirstFloor.ModernUI.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\Generator.Lib.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\ICSharpCode.AvalonEdit.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\ICSharpCode.SharpDevelop.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\ICSharpCode.SharpDevelop.Dom.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\ICSharpCode.SharpDevelop.Sda.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\ICSharpCode.SharpDevelop.Widgets.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\System.Cor3.Data.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\System.Cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\System.Cor3.Parsers.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\System.Data.SQLite.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\System.Data.SQLite.EF6.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\System.Data.SQLite.Linq.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\GeneratorTool.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\x64\SQLite.Interop.dll"; DestDir: "{app}\x64\"; Flags: ignoreversion
Source: "D:\DEV\WIN\CS_ROOT\Projects\cor3.gen\source\GeneratorTool\bin\Release\x86\SQLite.Interop.dll"; DestDir: "{app}\x86\"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:ProgramOnTheWeb,{#MyAppName}}"; Filename: "{#MyAppURL}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Dirs]
Name: "{app}\x86\"