; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!
[Files]
;Source: C:\Program Files\SAP\SAP Business One\AddOnInstallAPI.dll; Flags: dontcopy
Source: bin\Debug\AddOnInstallAPI.dll; DestDir: {app}
;Source: C:\Program Files\Codejock Software\ISSkin\ISSkin.dll; DestDir: {app}; Flags: dontcopy
;Source: C:\Program Files\Codejock Software\ISSkin\Styles\Office2007.cjstyles; DestDir: {tmp}; Flags: dontcopy
Source: bin\Debug\Interop.SAPbobsCOM.dll; DestDir: {app}
Source: bin\Debug\Interop.SAPbouiCOM.dll; DestDir: {app}
Source: bin\Debug\VisualD.MasterDataMatrixForm.dll; DestDir: {app}
Source: bin\Debug\VisualD.Matrix_Helper.dll; DestDir: {app}
Source: bin\Debug\VisualD.MenuConfFr.dll; DestDir: {app}
Source: bin\Debug\VisualD.MultiFunctions.dll; DestDir: {app}
Source: bin\Debug\VisualD.SBOFunctions.dll; DestDir: {app}
Source: bin\Debug\VisualD.SBOGeneralService.dll; DestDir: {app}
Source: bin\Debug\VisualD.SBOObjectMg1.dll; DestDir: {app}
Source: bin\Debug\VisualD.untLog.dll; DestDir: {app}
Source: bin\Debug\VisualD.vkBaseForm.dll; DestDir: {app}
Source: bin\Debug\VisualD.vkFormInterface.dll; DestDir: {app}
Source: bin\Debug\VisualD.ChooseFromListSubQuery.pdb; DestDir: {app}
Source: bin\Debug\VisualD.GlobalVid.pdb; DestDir: {app}
Source: bin\Debug\VisualD.Main.pdb; DestDir: {app}
Source: bin\Debug\VisualD.MainObjBase.pdb; DestDir: {app}
Source: bin\Debug\VisualD.MasterDataMatrixForm.pdb; DestDir: {app}
Source: bin\Debug\VisualD.Matrix_Helper.pdb; DestDir: {app}
Source: bin\Debug\VisualD.MenuConfFr.pdb; DestDir: {app}
Source: bin\Debug\VisualD.MultiFunctions.pdb; DestDir: {app}
Source: bin\Debug\VisualD.SBOFunctions.pdb; DestDir: {app}
Source: bin\Debug\VisualD.SBOGeneralService.pdb; DestDir: {app}
Source: bin\Debug\VisualD.SBOObjectMg1.pdb; DestDir: {app}
Source: bin\Debug\VisualD.untLog.pdb; DestDir: {app}
Source: bin\Debug\VisualD.vkBaseForm.pdb; DestDir: {app}
Source: bin\Debug\VisualD.vkFormInterface.pdb; DestDir: {app}
Source: bin\Debug\VisualD.ChooseFromListSubQuery.dll; DestDir: {app}
Source: bin\Debug\VisualD.GlobalVid.dll; DestDir: {app}
Source: bin\Debug\VisualD.uEncrypt.dll; DestDir: {app}
Source: bin\Debug\VisualD.uEncrypt.pdb; DestDir: {app}
Source: bin\Debug\VisualD.SBOCrystalPreview.dll; DestDir: {app}
Source: bin\Debug\VisualD.SBOCrystalPreview.pdb; DestDir: {app}
Source: bin\Debug\VisualD.Main.dll; DestDir: {app}
Source: bin\Debug\VisualD.MainObjBase.dll; DestDir: {app}
Source: bin\Debug\VisualD.Core.dll; DestDir: {app}
Source: bin\Debug\VisuaD.Core.exe; DestDir: {app}; Flags: nocompression
Source: bin\Debug\Factura Electronica Peru.vshost.exe; DestDir: {app}
Source: bin\Debug\Factura Electronica Peru.exe; DestDir: {app}
Source: bin\Debug\Factura Electronica Peru.pdb; DestDir: {app}
Source: bin\Debug\CrystalDecisions.CrystalReports.Engine.dll; DestDir: {app}
Source: bin\Debug\CrystalDecisions.Shared.dll; DestDir: {app}
Source: bin\Debug\Newtonsoft.Json.dll; DestDir: {app}
Source: bin\Debug\Registro Cambios.txt; DestDir: {app}
Source: bin\Debug\DLLparaXMLPE.dll; DestDir: {app}
Source: bin\Debug\DLLparaXMLPE.pdb; DestDir: {app}
;Documentos
Source: bin\Debug\Docs\UDFSAPPE.xls; DestDir: {app}\Docs
Source: bin\Debug\Docs\UDFFELECPE.xls; DestDir: {app}\Docs
;Menus
Source: bin\Debug\Menus\Menu.xml; DestDir: {app}\Menus
Source: bin\Debug\Menus\RemoveMenuPE.xml; DestDir: {app}\Menus
;Forms
Source: bin\Debug\Forms\strCnn.srf; DestDir: {app}\Forms
Source: bin\Debug\Forms\VID_ReImprimir.srf; DestDir: {app}\Forms
Source: bin\Debug\Forms\VID_Impuestos.srf; DestDir: {app}\Forms
Source: bin\Debug\Forms\VID_Monitor.srf; DestDir: {app}\Forms
Source: bin\Debug\Forms\VID_FEDetalleLog.srf; DestDir: {app}\Forms
Source: bin\Debug\Forms\VID_FEPROCED.srf; DestDir: {app}\Forms
Source: bin\Debug\Forms\FM_IVA.srf; DestDir: {app}\Forms
Source: bin\Debug\Forms\FM_NOTES.srf; DestDir: {app}\Forms
Source: bin\Debug\Forms\FM_UMISO.srf; DestDir: {app}\Forms
Source: bin\Debug\Forms\VID_MenuConf.srf; DestDir: {app}\Forms
Source: bin\Debug\Forms\VID_DarBaja.srf; DestDir: {app}\Forms
;vistas SQL Peru
Source: bin\Debug\SQLs\SQLServer\VID_FN_FE_LimpiaCaracteres.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_FN_FE_CantidadConLetra.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_VW_FE_PE_ORIN_R.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_VW_FE_PE_ORIN_E.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_VW_FE_PE_ORIN_D.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_VW_FE_PE_OINV_R.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_VW_FE_PE_OINV_E.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_VW_FE_PE_OINV_D.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_VW_FE_PE_GUIA_R.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_VW_FE_PE_GUIA_E.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_VW_FE_PE_GUIA_D.sql; DestDir: {app}\SQLs\SQLServer
;Procedimientos SQL EasyDot
Source: bin\Debug\SQLs\SQLServer\VID_SP_FE_IN_ED_PE_Genera08R.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_SP_FE_IN_ED_PE_Genera08E.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_SP_FE_IN_ED_PE_Genera08D.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_SP_FE_IN_ED_PE_Genera07R.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_SP_FE_IN_ED_PE_Genera07E.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_SP_FE_IN_ED_PE_Genera07D.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_SP_FE_IN_ED_PE_Genera01E.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_SP_FE_IN_ED_PE_Genera01D.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_SP_FE_IN_ED_PE_Genera03E.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_SP_FE_IN_ED_PE_Genera03D.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_SP_FE_IN_ED_PE_Genera20E.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_SP_FE_IN_ED_PE_Genera20D.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_SP_FE_IN_ED_PE_Genera09R.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_SP_FE_IN_ED_PE_Genera09E.sql; DestDir: {app}\SQLs\SQLServer
Source: bin\Debug\SQLs\SQLServer\VID_SP_FE_IN_ED_PE_Genera09D.sql; DestDir: {app}\SQLs\SQLServer
;vistas HANA Peru
Source: bin\Debug\SQLs\HANA\VID_FN_FE_LimpiaCaracteres.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_FN_FE_CantidadConLetra.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_VW_FE_PE_ORIN_R.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_VW_FE_PE_ORIN_E.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_VW_FE_PE_ORIN_D.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_VW_FE_PE_OINV_R.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_VW_FE_PE_OINV_E.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_VW_FE_PE_OINV_D.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_VW_FE_PE_GUIA_R.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_VW_FE_PE_GUIA_E.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_VW_FE_PE_GUIA_D.sql; DestDir: {app}\SQLs\HANA
;Procedimientos HANA EasyDot
Source: bin\Debug\SQLs\HANA\VID_SP_FE_IN_ED_PE_Genera08R.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_SP_FE_IN_ED_PE_Genera08E.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_SP_FE_IN_ED_PE_Genera08D.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_SP_FE_IN_ED_PE_Genera07R.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_SP_FE_IN_ED_PE_Genera07E.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_SP_FE_IN_ED_PE_Genera07D.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_SP_FE_IN_ED_PE_Genera01E.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_SP_FE_IN_ED_PE_Genera01D.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_SP_FE_IN_ED_PE_Genera03E.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_SP_FE_IN_ED_PE_Genera03D.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_SP_FE_IN_ED_PE_Genera20E.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_SP_FE_IN_ED_PE_Genera20D.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_SP_FE_IN_ED_PE_Genera09R.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_SP_FE_IN_ED_PE_Genera09E.sql; DestDir: {app}\SQLs\HANA
Source: bin\Debug\SQLs\HANA\VID_SP_FE_IN_ED_PE_Genera09D.sql; DestDir: {app}\SQLs\HANA
;layouts Peru SQL
Source: bin\Debug\Reports\SQL\01_Report.rpt; DestDir: {app}\Reports\SQL
Source: bin\Debug\Reports\SQL\01A_Report.rpt; DestDir: {app}\Reports\SQL
Source: bin\Debug\Reports\SQL\03_Report.rpt; DestDir: {app}\Reports\SQL
Source: bin\Debug\Reports\SQL\07_Report.rpt; DestDir: {app}\Reports\SQL
Source: bin\Debug\Reports\SQL\08_Report.rpt; DestDir: {app}\Reports\SQL
Source: bin\Debug\Reports\SQL\09_Report.rpt; DestDir: {app}\Reports\SQL
Source: bin\Debug\Reports\SQL\20_Report.rpt; DestDir: {app}\Reports\SQL

;layouts Peru HANA
Source: bin\Debug\Reports\HANA\01_Report.rpt; DestDir: {app}\Reports\HANA
Source: bin\Debug\Reports\HANA\01A_Report.rpt; DestDir: {app}\Reports\HANA
Source: bin\Debug\Reports\HANA\03_Report.rpt; DestDir: {app}\Reports\HANA
Source: bin\Debug\Reports\HANA\07_Report.rpt; DestDir: {app}\Reports\HANA
Source: bin\Debug\Reports\HANA\08_Report.rpt; DestDir: {app}\Reports\HANA
Source: bin\Debug\Reports\HANA\20_Report.rpt; DestDir: {app}\Reports\HANA

;Factura Movil
Source: bin\Debug\RestSharp.dll; DestDir: {app}
Source: bin\Debug\ServiceStack.Text.dll; DestDir: {app}
Source: bin\Debug\System.Net.Http.dll; DestDir: {app}

[Setup]
UsePreviousLanguage=no
AppName=Facturación Electronica VK Peru
AppPublisher=Visual Developer
AppVerName=Facturación Electronica Peru - VisualD
AppPublisherURL=http://www.visuald.cl
AppSupportURL=http://www.visuald.cl
AppUpdatesURL=http://www.visuald.cl
DefaultDirName={code:GetDefaultAddOnDir}
;OutputBaseFileName={code:GetSetupName}
OutputBaseFileName=Setup
DisableDirPage=true
Compression=lzma
SolidCompression=true
UsePreviousAppDir=false
AppendDefaultDirName=true
PrivilegesRequired=admin
WindowVisible=false
WizardSmallImageFile=compiler:WizModernSmallImage-IS.bmp
;WizardImageFile=C:\Program Files (x86)\Codejock Software\ISSkin\Styles\Office2007Gray.bmp
AppContact=soporte@visuald.cl
;SetupIconFile=C:\Software\-iconos\Settings and System Utilities\YaST.ico
[Messages]
BeveledLabel=Visual Developer - www.visuald.cl
[Registry]
Root: HKLM; Subkey: SOFTWARE\VID\Addons\{code:GetAddOnName}; ValueType: string; ValueName: InstallDir; ValueData: {code:GetDefaultAddOnDir}; Flags: uninsdeletevalue
[Languages]
Name: spanish; MessagesFile: compiler:Languages\Spanish.isl
[UninstallDelete]
Type: files; Name: {app}\{code:GetSetupName}.EXE
[Run]
Filename: {app}\VisuaD.Core.exe
[Code]
type
   TSHFileOpStruct = record
     Wnd    				: HWND;
     wFunc  				: UINT;
     pFrom  				: PChar;
     pTo    				: PChar;
     fFlags  				: Word;
     fAnyOperationsAborted 	: BOOL;
     hNameMappings			: HWND;
     lpszProgressTitle: PChar;
   end;

const
   { $EXTERNALSYM FO_COPY }
   FO_COPY           = $0002;
   { $EXTERNALSYM FOF_SILENT }
   FOF_SILENT                 = $0004;
   { $EXTERNALSYM FOF_NOCONFIRMATION }
   FOF_NOCONFIRMATION         = $0010;

var
CurrentLocation : string;
AddOnDir        : string;
FinishedInstall : Boolean;
Params          : string;
i               : integer;

//Copy
function SHFileOperation(const lpFileOp: TSHFileOpStruct):Integer; external 'SHFileOperation@shell32.dll stdcall';

//SAP B1
function EndInstallEx(Dir:String;Ok:Boolean): integer; external 'EndInstallEx@files:AddOnInstallAPI.dll stdcall';
function EndUninstall(path: string; succeed: boolean): integer; external 'EndUninstall@files:AddOnInstallAPI.dll stdcall';
function SetAddOnFolder(srcPath : string): integer; external 'SetAddOnFolder@files:AddOnInstallAPI.dll stdcall';
function RestartNeeded :integer; external 'RestartNeeded@files:AddOnInstallAPI.dll stdcall delayload ';


Function ExistStrInParam(StrInParam:string) : boolean;
var
 j: integer;
begin
result:=false;
  for j:=0 to ParamCount do
  if UpperCase(ParamStr(j))=UpperCase(StrInParam) then
  begin
   result:=true;
   break;
  end;
end;

function GetAddOnName(dummy: String): string;
begin
   result := 'Facturación Electronica';
end;

function GetSetupName(dummy: String): string;
begin
   result := 'Setup';
end;

function PreparePaths() : Boolean;
var
position : integer;
aux : string;
begin
   if pos('|', paramstr(2)) <> 0 then //la ruta puede venir en le parametro 2 o 4
   begin
      aux := paramstr(2);
      position := Pos('|', aux);
      AddOnDir := Copy(aux,1, position - 1);
      Result := True;
   end
   else
   if pos('|', paramstr(4)) <> 0 then //la ruta puede venir en le parametro 2 o 4
   begin
      aux := paramstr(4);
      position := Pos('|', aux);
      AddOnDir := Copy(aux,1, position - 1);
      Result := True;
   end
   else
   begin
     MsgBox('El Instalador debe ser ejecutado desde SAP Business One.', mbInformation, MB_OK);
     EndInstallEx('',false);//Avisa a Sap B1 que se aborto la instalacion
     Result := False;
   end;
end;

function GetDefaultAddOnDir(Param : string): string;
begin
   result := AddOnDir;
end;

function InitializeSetup(): Boolean;
var
ResultCode      : Integer;
UninstallerPath : String;
begin
   //ExtractTemporaryFile('Office2007.cjstyles');
   //LoadSkin(ExpandConstant('{tmp}\Office2007.cjstyles'), 'NormalBlack.ini');

   Params:='';
   for i:=0 to ParamCount do
    Params := Params+' Param'+inttoStr(i)+' = '+paramstr(i)+#13;
   //MsgBox(Params, mbInformation, MB_OK);

   if ExistStrInParam('/U') then
   begin
    if RegQueryStringValue(HKEY_LOCAL_MACHINE, 'SOFTWARE\VID\Addons\'+GetAddOnName(''),'InstallDir', CurrentLocation) then
    UninstallerPath:=CurrentLocation + '\unins000.exe';
    //MsgBox('UninstallerPath '+UninstallerPath, mbInformation, MB_OK);
    Exec(UninstallerPath, '/SILENT', '', SW_SHOW, ewWaitUntilTerminated, ResultCode);
    EndUninstall('',true);
    result:=False;
   end
   else
   begin
   result := PreparePaths;
   end;
end;

procedure DeinitializeSetup();
begin
	//ShowWindow(StrToInt(ExpandConstant('{wizardhwnd}')), 0);
	//UnloadSkin();
end;

function NextButtonClick(CurPageID: Integer): Boolean;
begin
   Result := True;
   case CurPageID of
       wpSelectDir :
       begin
           AddOnDir := ExpandConstant('{app}');
       end;
       wpFinished :
       begin
          if FinishedInstall then
          begin
             SetAddOnFolder(AddOnDir);
             EndInstallEx('',True);
          end;
       end;
   end;
end;

Function ExtractFileNameParam(StrParam:string) : string;
var
 j: integer;
begin
 result:=StrParam;
 j:=1;
 repeat
 result:=Copy(StrParam,j,Length(StrParam));
 j:=j+1;
 until FileExists(result) or (j>=Length(StrParam));
end;

procedure CopyFile(FromFileName, ToFileName: string);
var
  ShellInfo: TSHFileOpStruct;
  Files    : String;
begin
  Files:=FromFileName+#0+#0;
  ShellInfo.wFunc:=FO_COPY;
  ShellInfo.pFrom:=PChar(Files);
  ShellInfo.pTo:=PChar(ToFileName);
  ShellInfo.fFlags:=FOF_NOCONFIRMATION or FOF_SILENT;
  SHFileOperation(shellinfo);
end;

procedure CurStepChanged(CurStep: TSetupStep);
Var
SetupFile : string;
begin
   if CurStep = ssPostInstall then
   begin
    SetupFile:=ExtractFileNameParam(ParamStr(1));
    //FileCopy(SetupFile,AddOnDir+'\'+GetSetupName('')+'.exe',False); esta opcion no funciona en este caso
    //MsgBox('Origen '+SetupFile+' Destino '+AddOnDir+'\'+GetSetupName('')+'.exe', mbInformation, MB_OK);
    DeleteFile(AddOnDir+'\'+GetSetupName('')+'.exe');
    CopyFile(SetupFile,AddOnDir+'\'+GetSetupName('')+'.exe');
    FinishedInstall := True;
   end;
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
{ no se usa por el momento debido a que el instalador llama al unist000.exe
	case CurUninstallStep of
		ssDone : EndUninstall('',true);
	end;
}
end;
[Dirs]
Name: {app}\Docs
Name: {app}\Menus
Name: {app}\Forms
Name: {app}\SQLs
Name: {app}\SQLs\PE
Name: {app}\SQLs\PE\SQLServer
Name: {app}\SQLs\PE\HANA
Name: {app}\Reports
Name: {app}\Reports\PE
Name: {app}\Reports\PE\SQL
Name: {app}\Reports\PE\HANA
Name: {app}\xml
