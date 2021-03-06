using System;
using System.Collections.Generic;
using System.Linq;
using SAPbouiCOM;
using SAPbobsCOM;
using System.Globalization;
using VisualD.GlobalVid;
using VisualD.SBOFunctions;
using System.Reflection;
using VisualD.vkBaseForm;
using VisualD.MultiFunctions;
using VisualD.vkFormInterface;
using System.IO;
using System.CodeDom.Compiler;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using Factura_Electronica_VK.Functions;
using VisualD.untLog;
using System.Data;
using FactRemota;
using ServiceStack.Text;
using System.Net.Http;
using System.Configuration;
using VisualD.SBOGeneralService;
using Newtonsoft.Json;
using DLLparaXMLPE;

namespace Factura_Electronica_VK.CreditNotes
{
    public class TCreditNotes : TvkBaseForm, IvkFormInterface
    {
        private SAPbobsCOM.Recordset oRecordSet;
        private SAPbouiCOM.Form oForm;
        private String s;
        private Boolean Flag;
        private SAPbouiCOM.EditText oEditText;
        private SAPbouiCOM.ComboBox oComboBox;
        private SAPbouiCOM.StaticText oStatic;
        private SAPbouiCOM.DataTable odt;
        private SAPbouiCOM.Grid ogrid;
        private List<string> Lista;
        //por Peru
        private String RUC;
        private CultureInfo _nf = new System.Globalization.CultureInfo("en-US");
        private String SeparadorM = "";

        private SAPbouiCOM.Matrix mtx;
        public VisualD.SBOFunctions.CSBOFunctions SBO_f;
        public static String DocSubType
        { get; set; }
        public static Boolean bFolderAdd
        { get; set; }
        public static String ObjType
        { get; set; }


        public new bool InitForm(string uid, string xmlPath, ref Application application, ref SAPbobsCOM.Company company, ref CSBOFunctions SBOFunctions, ref TGlobalVid _GlobalSettings)
        {
            SAPbouiCOM.Folder oFolder;
            SAPbouiCOM.Item oItem;
            SAPbouiCOM.Item oItemB;
            SAPbouiCOM.StaticText oStaticText;
            SAPbouiCOM.Matrix oMatrix;
            SAPbouiCOM.GridColumns oColumns;
            SAPbouiCOM.GridColumn oColumn;
            SAPbouiCOM.DataTable oDataTable;
            String sSeries;
            //result  := inherited InitForm(uid, xmlPath,var application,var company,var sboFunctions,var _GlobalSettings );
            bool Result = base.InitForm(uid, xmlPath, ref application, ref company, ref SBOFunctions, ref _GlobalSettings);

            try
            {
                oRecordSet = (SAPbobsCOM.Recordset)(FCmpny.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset));
                //FSBOf.LoadForm(xmlPath, 'VID_Entrega.srf', Uid);
                oForm = FSBOApp.Forms.Item(uid);
                Flag = false;
                oForm.Freeze(true);

                //Campo con el estado de DTE
                oItemB = oForm.Items.Item("84");
                oItem = oForm.Items.Add("lblEstado", SAPbouiCOM.BoFormItemTypes.it_STATIC);
                oItem.Left = oItemB.Left;
                oItem.Width = oItemB.Width;
                oItem.Top = oItemB.Top + oItemB.Height + 5;
                oItem.Height = oItem.Height;
                oItem.LinkTo = "VID_FEEstado";
                oStatic = (StaticText)(oForm.Items.Item("lblEstado").Specific);
                oStatic.Caption = "Estado Doc. Electronico";

                oItemB = oForm.Items.Item("208");
                oItem = oForm.Items.Add("VID_Estado", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX);
                oItem.Left = oItemB.Left;
                oItem.Width = oItemB.Width + 30;
                oItem.Top = oItemB.Top + oItemB.Height + 5;
                oItem.Height = oItem.Height;
                oItem.DisplayDesc = true;
                oItem.Enabled = false;
                oComboBox = (ComboBox)(oForm.Items.Item("VID_Estado").Specific);
                if (ObjType == "13")
                    oComboBox.DataBind.SetBound(true, "OINV", "U_EstadoFE");
                else if (ObjType == "203")
                    oComboBox.DataBind.SetBound(true, "ODPI", "U_EstadoFE");



                if (GlobalSettings.RunningUnderSQLServer)
                    s = "select ISNULL(TaxIdNum,'') TaxIdNum from OADM ";
                else
                    s = @"select IFNULL(""TaxIdNum"",'') ""TaxIdNum"" from ""OADM"" ";

                oRecordSet.DoQuery(s);
                if (oRecordSet.RecordCount == 0)
                    throw new Exception("Debe ingresar RUC de Emisor, Gestión -> Inicialización Sistema -> Detalle Sociedad -> Datos de Contabilidad -> ID fiscal general 1");
                else
                    RUC = ((System.String)oRecordSet.Fields.Item("TaxIdNum").Value).Trim();

                //colocar folder con los campos necesarios en FE PERU
                oForm.DataSources.UserDataSources.Add("VID_FEDCTO", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 1);
                oItem = oForm.Items.Add("VID_FEDCTO", SAPbouiCOM.BoFormItemTypes.it_FOLDER);

                if (DocSubType == "--")
                {
                    //para SAP 882 en adelante
                    oItemB = oForm.Items.Item("1320002137");

                    oItem.Left = oItemB.Left + 30;
                    oItem.Width = oItemB.Width;
                    oItem.Top = oItemB.Top;
                    oItem.Height = oItem.Height;
                    oFolder = (Folder)((oItem.Specific));
                    oFolder.Caption = "Factura Electrónica";
                    oFolder.Pane = 333;
                    oFolder.DataBind.SetBound(true, "", "VID_FEDCTO");
                    //para SAP 882 en adelante
                    oFolder.GroupWith("1320002137");

                    //cargar campos de usuarios
                    oItemB = oForm.Items.Item("40");
                    oItem = oForm.Items.Add("lblMDTD", SAPbouiCOM.BoFormItemTypes.it_STATIC);
                    oItem.Left = 50; //oItemB.Left;
                    oItem.Width = 125;//;oItemB.Width;
                    oItem.Top = oItemB.Top + 15;//195
                    oItem.Height = oItem.Height;//14
                    oItem.FromPane = 333;
                    oItem.ToPane = 333;
                    oItem.LinkTo = "VID_FEMDTD";
                    oStatic = (StaticText)(oForm.Items.Item("lblMDTD").Specific);
                    oStatic.Caption = "Tipo de Documento";

                    oItemB = oForm.Items.Item("lblMDTD");
                    oItem = oForm.Items.Add("VID_FEMDTD", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                    oItem.Left = oItemB.Left + oItemB.Width + 5;
                    oItem.Width = 60;
                    oItem.Top = oItemB.Top;
                    oItem.Height = oItemB.Height;
                    oItem.FromPane = 333;
                    oItem.ToPane = 333;
                    oItem.RightJustified = true;
                    oEditText = (EditText)(oForm.Items.Item("VID_FEMDTD").Specific);
                    oEditText.DataBind.SetBound(true, "ORIN", "U_BPP_MDTD");

                    //--
                    oItemB = oForm.Items.Item("lblMDTD");
                    oItem = oForm.Items.Add("lblMDSD", SAPbouiCOM.BoFormItemTypes.it_STATIC);
                    oItem.Left = oItemB.Left;
                    oItem.Width = oItemB.Width;
                    oItem.Top = oItemB.Top + oItemB.Height + 5;
                    oItem.Height = oItem.Height;
                    oItem.FromPane = 333;
                    oItem.ToPane = 333;
                    oItem.LinkTo = "VID_FEMDSD";
                    oStatic = (StaticText)(oForm.Items.Item("lblMDSD").Specific);
                    oStatic.Caption = "Serie del documento";

                    oItemB = oForm.Items.Item("lblMDSD");
                    oItem = oForm.Items.Add("VID_FEMDSD", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                    oItem.Left = oItemB.Left + oItemB.Width + 5;
                    oItem.Width = 90; // oItemB.Width;
                    oItem.Top = oItemB.Top;
                    oItem.Height = oItem.Height;
                    oItem.FromPane = 333;
                    oItem.ToPane = 333;
                    oItem.RightJustified = true;
                    oEditText = (EditText)(oForm.Items.Item("VID_FEMDSD").Specific);
                    oEditText.DataBind.SetBound(true, "ORIN", "U_BPP_MDSD");

                    //--
                    oItemB = oForm.Items.Item("lblMDSD");
                    oItem = oForm.Items.Add("lblMDCD", SAPbouiCOM.BoFormItemTypes.it_STATIC);
                    oItem.Left = oItemB.Left;
                    oItem.Width = oItemB.Width;
                    oItem.Top = oItemB.Top + oItemB.Height + 5;
                    oItem.Height = oItem.Height;
                    oItem.FromPane = 333;
                    oItem.ToPane = 333;
                    oItem.LinkTo = "VID_FEMDCD";
                    oStatic = (StaticText)(oForm.Items.Item("lblMDCD").Specific);
                    oStatic.Caption = "Correlativo del documento";

                    oItemB = oForm.Items.Item("lblMDCD");
                    oItem = oForm.Items.Add("VID_FEMDCD", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                    oItem.Left = oItemB.Left + oItemB.Width + 5;
                    oItem.Width = 90; // oItemB.Width;
                    oItem.Top = oItemB.Top;
                    oItem.Height = oItem.Height;
                    oItem.FromPane = 333;
                    oItem.ToPane = 333;
                    oItem.RightJustified = true;
                    oEditText = (EditText)(oForm.Items.Item("VID_FEMDCD").Specific);
                    oEditText.DataBind.SetBound(true, "ORIN", "U_BPP_MDCD");


                    //--
                    oItemB = oForm.Items.Item("lblMDCD");
                    oItem = oForm.Items.Add("lblMDTN", SAPbouiCOM.BoFormItemTypes.it_STATIC);
                    oItem.Left = oItemB.Left;
                    oItem.Width = oItemB.Width;
                    oItem.Top = oItemB.Top + oItemB.Height + 5;
                    oItem.Height = oItem.Height;
                    oItem.FromPane = 333;
                    oItem.ToPane = 333;
                    oItem.LinkTo = "VID_FEMDTN";
                    oStatic = (StaticText)(oForm.Items.Item("lblMDTN").Specific);
                    oStatic.Caption = "Tipo de operacion";

                    oItemB = oForm.Items.Item("lblMDTN");
                    oItem = oForm.Items.Add("VID_FEMDTN", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX);
                    oItem.Left = oItemB.Left + oItemB.Width + 5;
                    oItem.Width = 140; // oItemB.Width;
                    oItem.Top = oItemB.Top;
                    oItem.Height = oItem.Height;
                    oItem.FromPane = 333;
                    oItem.ToPane = 333;
                    oItem.DisplayDesc = true;
                    oComboBox = (ComboBox)(oForm.Items.Item("VID_FEMDTN").Specific);
                    oComboBox.DataBind.SetBound(true, "ORIN", "U_BPP_MDTN");

                    if (GlobalSettings.RunningUnderSQLServer)
                    {
                        s = @"select Code 'Code', Name 'Name'
                                    from [@FM_NOTES] 
                                   ORDER BY Code ";
                    }
                    else
                    {
                        s = @"select ""Code"" ""Code"", ""Name"" ""Name""
                                    from ""@FM_NOTES""
                                   ORDER BY ""Code"" ";
                    }
                    oRecordSet.DoQuery(s);
                    FSBOf.FillCombo((ComboBox)(oForm.Items.Item("VID_FEMDTN").Specific), ref oRecordSet, false);

                    //--
                    oItemB = oForm.Items.Item("VID_FEMDTD");
                    oItem = oForm.Items.Add("lblFE", SAPbouiCOM.BoFormItemTypes.it_STATIC);
                    oItem.Left = oItemB.Left + oItemB.Width + 100;
                    oItem.Width = oItemB.Width + 60;
                    oItem.Top = oItemB.Top - oItemB.Height - 5;
                    oItem.Height = oItem.Height;
                    oItem.FromPane = 333;
                    oItem.ToPane = 333;
                    oItem.LinkTo = "lblMDCD";
                    oItem.Visible = false;
                    oStatic = (StaticText)(oForm.Items.Item("lblFE").Specific);
                    oStatic.Caption = "Datos documento origen";
                    oForm.Items.Item("lblFE").Visible = false;


                    //--
                    oItemB = oForm.Items.Item("lblFE");
                    oItem = oForm.Items.Add("lblMDTO", SAPbouiCOM.BoFormItemTypes.it_STATIC);
                    oItem.Left = oItemB.Left;
                    oItem.Width = oItemB.Width;
                    oItem.Top = oItemB.Top + oItemB.Height + 5;
                    oItem.Height = oItem.Height;
                    oItem.FromPane = 333;
                    oItem.ToPane = 333;
                    oItem.LinkTo = "VID_FEMDTO";
                    oStatic = (StaticText)(oForm.Items.Item("lblMDTO").Specific);
                    oStatic.Caption = "Tipo de Docto. origen";

                    oItemB = oForm.Items.Item("lblMDTO");
                    oItem = oForm.Items.Add("VID_FEMDTO", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                    oItem.Left = oItemB.Left + oItemB.Width + 5;
                    oItem.Width = 90;
                    oItem.Top = oItemB.Top;
                    oItem.Height = oItem.Height;
                    oItem.FromPane = 333;
                    oItem.ToPane = 333;
                    oItem.RightJustified = true;
                    oEditText = (EditText)(oForm.Items.Item("VID_FEMDTO").Specific);
                    oEditText.DataBind.SetBound(true, "ORIN", "U_BPP_MDTO");

                    //--
                    oItemB = oForm.Items.Item("lblMDTO");
                    oItem = oForm.Items.Add("lblMDSO", SAPbouiCOM.BoFormItemTypes.it_STATIC);
                    oItem.Left = oItemB.Left;
                    oItem.Width = oItemB.Width;
                    oItem.Top = oItemB.Top + oItemB.Height + 5;
                    oItem.Height = oItem.Height;
                    oItem.FromPane = 333;
                    oItem.ToPane = 333;
                    oItem.LinkTo = "VID_FEMDSO";
                    oStatic = (StaticText)(oForm.Items.Item("lblMDSO").Specific);
                    oStatic.Caption = "Serie documento origen";

                    oItemB = oForm.Items.Item("lblMDSO");
                    oItem = oForm.Items.Add("VID_FEMDSO", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                    oItem.Left = oItemB.Left + oItemB.Width + 5;
                    oItem.Width = 90;
                    oItem.Top = oItemB.Top;
                    oItem.Height = oItem.Height;
                    oItem.FromPane = 333;
                    oItem.ToPane = 333;
                    oItem.RightJustified = true;
                    oEditText = (EditText)(oForm.Items.Item("VID_FEMDSO").Specific);
                    oEditText.DataBind.SetBound(true, "ORIN", "U_BPP_MDSO");

                    //--
                    oItemB = oForm.Items.Item("lblMDSO");
                    oItem = oForm.Items.Add("lblMDCO", SAPbouiCOM.BoFormItemTypes.it_STATIC);
                    oItem.Left = oItemB.Left;
                    oItem.Width = oItemB.Width;
                    oItem.Top = oItemB.Top + oItemB.Height + 5;
                    oItem.Height = oItem.Height;
                    oItem.FromPane = 333;
                    oItem.ToPane = 333;
                    oItem.LinkTo = "VID_FEMDCO";
                    oStatic = (StaticText)(oForm.Items.Item("lblMDCO").Specific);
                    oStatic.Caption = "Correlativo docto. origen";

                    oItemB = oForm.Items.Item("lblMDCO");
                    oItem = oForm.Items.Add("VID_FEMDCO", SAPbouiCOM.BoFormItemTypes.it_EDIT);
                    oItem.Left = oItemB.Left + oItemB.Width + 5;
                    oItem.Width = 90;
                    oItem.Top = oItemB.Top;
                    oItem.Height = oItem.Height;
                    oItem.FromPane = 333;
                    oItem.ToPane = 333;
                    oItem.RightJustified = true;
                    oEditText = (EditText)(oForm.Items.Item("VID_FEMDCO").Specific);
                    oEditText.DataBind.SetBound(true, "ORIN", "U_BPP_MDCO");
                }
            }
            catch (Exception e)
            {
                OutLog("InitForm: " + e.Message + " ** Trace: " + e.StackTrace);
                FSBOApp.MessageBox(e.Message + " ** Trace: " + e.StackTrace, 1, "Ok", "", "");
            }
            finally
            {
                oForm.Visible = true;
                oForm.Freeze(false);
            }
            return Result;

        }//fin InitForm

        public new void MenuEvent(ref MenuEvent pVal, ref Boolean BubbleEvent)
        {
            SAPbouiCOM.Conditions oConditions;
            SAPbouiCOM.Condition oCondition;
            //Int32 Entry;
            base.MenuEvent(ref pVal, ref BubbleEvent);
            try
            {
                //1281 Buscar; 
                //1282 Crear
                //1284 cancelar; 
                //1285 Restablecer; 
                //1286 Cerrar; 
                //1288 Registro siguiente;
                //1289 Registro anterior; 
                //1290 Primer Registro; 
                //1291 Ultimo Registro; 
                //1287 Duplicar;
                //1304 Actualizar
                if ((pVal.MenuUID != "") && (pVal.BeforeAction == false))
                {
                    if ((pVal.MenuUID == "1288") || (pVal.MenuUID == "1289") || (pVal.MenuUID == "1290") || (pVal.MenuUID == "1291") || (pVal.MenuUID == "1304"))
                    {
                        oForm.Freeze(true);
                        //oForm.Items.Item("VID_Estado").Enabled = false;

                        oComboBox = (ComboBox)(oForm.Items.Item("88").Specific);
                        var sSeries = (System.String)(oComboBox.Value);

                        if (GlobalSettings.RunningUnderSQLServer)
                            s = @"select LEFT(ISNULL(UPPER(BeginStr),''),1) 'Valor', SUBSTRING(ISNULL(BeginStr,''), 2, LEN(BeginStr)) 'TipoDocElect' from NNM1 where Series = {0} ";
                        else
                            s = @"select LEFT(IFNULL(UPPER(""BeginStr""),''),1) ""Valor"", SUBSTRING(IFNULL(""BeginStr"",''), 2, LENGTH(""BeginStr"")) ""TipoDocElect"" from ""NNM1"" where ""Series"" = {0} ";
                        s = String.Format(s, sSeries);
                        oRecordSet.DoQuery(s);
                        if (oRecordSet.RecordCount > 0)
                        {
                            if (((System.String)oRecordSet.Fields.Item("Valor").Value).Trim() == "E")
                            {
                                oForm.Items.Item("VID_Estado").Visible = true;
                                oForm.Items.Item("VID_Estado").Enabled = false;
                                oForm.Items.Item("lblEstado").Visible = true;
                                oForm.Items.Item("VID_FEDCTO").Visible = true;
                            }
                            else
                            {
                                oForm.Items.Item("VID_Estado").Visible = false;
                                oForm.Items.Item("lblEstado").Visible = false;
                                oForm.Items.Item("VID_FEDCTO").Visible = false;
                            }
                        }
                        oForm.Freeze(false);
                    }

                    if ((pVal.MenuUID == "1282") || (pVal.MenuUID == "1281") || (pVal.MenuUID == "1287"))
                    {
                        oForm.Freeze(true);
                        //oForm.Items.Item("VID_Estado").Enabled = false;

                        oComboBox = (ComboBox)(oForm.Items.Item("88").Specific);
                        var sSeries = (System.String)(oComboBox.Value);

                        if (GlobalSettings.RunningUnderSQLServer)
                            s = @"select LEFT(ISNULL(UPPER(BeginStr),''),1) 'Valor', SUBSTRING(ISNULL(BeginStr,''), 2, LEN(BeginStr)) 'TipoDocElect' from NNM1 where Series = {0}  ";
                        else
                            s = @"select LEFT(IFNULL(UPPER(""BeginStr""),''),1) ""Valor"", SUBSTRING(IFNULL(""BeginStr"",''), 2, LENGTH(""BeginStr"")) ""TipoDocElect"" from ""NNM1"" where ""Series"" = {0} ";
                        s = String.Format(s, sSeries);
                        oRecordSet.DoQuery(s);
                        if (oRecordSet.RecordCount > 0)
                        {
                            if ((System.String)(oRecordSet.Fields.Item("Valor").Value) == "E")
                            {
                                oForm.Items.Item("VID_Estado").Visible = true;
                                oForm.Items.Item("VID_Estado").Enabled = false;
                                oForm.Items.Item("lblEstado").Visible = true;
                                oForm.Items.Item("VID_FEDCTO").Visible = true;
                            }
                            else
                            {
                                oForm.Items.Item("VID_Estado").Visible = false;
                                oForm.Items.Item("lblEstado").Visible = false;
                                oForm.Items.Item("VID_FEDCTO").Visible = false;
                            }

                            if ((pVal.MenuUID == "1282") || (pVal.MenuUID == "1287"))
                                ((ComboBox)oForm.Items.Item("VID_Estado").Specific).Select("N", BoSearchKey.psk_ByValue);
                        }
                        oForm.Freeze(false);
                    }
                }
            }
            catch (Exception e)
            {
                FSBOApp.MessageBox(e.Message + " ** Trace: " + e.StackTrace, 1, "Ok", "", "");
                OutLog("MenuEvent: " + e.Message + " ** Trace: " + e.StackTrace);
            }
        }//fin MenuEvent

        public new void FormEvent(String FormUID, ref SAPbouiCOM.ItemEvent pVal, ref Boolean BubbleEvent)
        {
            Int32 nErr;
            String sErr;
            SAPbouiCOM.IChooseFromListEvent oCFLEvento = null;
            SAPbouiCOM.DataTable oDataTableD;
            //inherited FormEvent(FormUID,Var pVal,Var BubbleEvent);
            base.FormEvent(FormUID, ref pVal, ref BubbleEvent);
            try
            {
                if ((pVal.EventType == BoEventTypes.et_ITEM_PRESSED) && (pVal.BeforeAction))
                {
                    if ((pVal.ItemUID == "1") && (oForm.Mode == BoFormMode.fm_ADD_MODE))
                        BubbleEvent = ValidarDatosFE_PE();
                }

                if ((pVal.EventType == BoEventTypes.et_ITEM_PRESSED) && (!pVal.BeforeAction))
                {
                    if (pVal.ItemUID == "VID_FEDCTO")
                    {
                        oForm.PaneLevel = 333;
                    }
                }

                if ((pVal.EventType == BoEventTypes.et_FORM_ACTIVATE) && (!pVal.BeforeAction))
                {
                    GlobalSettings.PrevFormUID = oForm.UniqueID;
                }

                if ((pVal.ItemUID == "88") && (pVal.EventType == BoEventTypes.et_COMBO_SELECT) && (!pVal.BeforeAction))
                {
                    oComboBox = (ComboBox)(oForm.Items.Item("88").Specific);
                    var sSeries = (System.String)(oComboBox.Value);

                    if (GlobalSettings.RunningUnderSQLServer)
                        s = @"select LEFT(ISNULL(UPPER(BeginStr),''),1) 'Valor' from NNM1 where Series = {0} ";
                    else
                        s = @"select LEFT(IFNULL(UPPER(""BeginStr""),''),1) ""Valor"" from ""NNM1"" where ""Series"" = {0} ";
                    s = String.Format(s, sSeries);
                    oRecordSet.DoQuery(s);
                    if (oRecordSet.RecordCount > 0)
                    {
                        if ((System.String)(oRecordSet.Fields.Item("Valor").Value) == "E")
                        {
                            oForm.Items.Item("VID_Estado").Visible = true;
                            oForm.Items.Item("lblEstado").Visible = true;
                            oForm.Items.Item("VID_FEDCTO").Visible = true;
                        }
                        else
                        {
                            oForm.Items.Item("VID_Estado").Visible = false;
                            oForm.Items.Item("lblEstado").Visible = false;
                            oForm.Items.Item("VID_FEDCTO").Visible = false;
                            s = "112";
                            oForm.Items.Item(s).Click(BoCellClickType.ct_Regular);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                FCmpny.GetLastError(out nErr, out sErr);
                FSBOApp.StatusBar.SetText("FormEvent: " + e.Message + " ** Trace: " + e.StackTrace, BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                OutLog("FormEvent: " + e.Message + " ** Trace: " + e.StackTrace);
                if (oForm != null)
                    oForm.Freeze(false);
            }
            finally
            {
                ;
            }

        }//fin FormEvent


        public new void FormDataEvent(ref BusinessObjectInfo BusinessObjectInfo, ref Boolean BubbleEvent)
        {
            String sDocEntry;
            String sDocSubType;
            String TipoDocElec = "";
            Int32 lRetCode;
            String Tipo;
            TFunctions Reg;
            String TaxIdNum;
            String Canceled = "";
            Int32 FolioNum;
            String tabla;
            String TipoElect;
            SAPbobsCOM.Documents oDocument;
            //inherited FormDataEvent(var BusinessObjectInfo,var BubbleEvent);
            base.FormDataEvent(ref BusinessObjectInfo, ref BubbleEvent);

            try
            {
                if ((BusinessObjectInfo.BeforeAction == false) && (BusinessObjectInfo.EventType == BoEventTypes.et_FORM_DATA_ADD) && (BusinessObjectInfo.ActionSuccess))
                {
                    sDocEntry = FSBOf.GetDocEntryBusinessObjectInfo(BusinessObjectInfo.ObjectKey);
                    if (GlobalSettings.RunningUnderSQLServer)
                    {
                        s = @"select T0.DocSubType, SUBSTRING(UPPER(T2.BeginStr), 1, 1) 'Tipo', SUBSTRING(ISNULL(T2.BeginStr,''), 2, LEN(T2.BeginStr)) 'Inst'
                                                 ,ISNULL(T0.U_BPP_MDTD,'') BPP_MDTD, ISNULL(T0.U_BPP_MDSD,'') BPP_MDSD, ISNULL(T0.U_BPP_MDCD,'') BPP_MDCD, T0.CANCELED
                                             FROM ORIN T0 WITH (NOLOCK)
                                                JOIN NNM1 T2 WITH (NOLOCK) ON T0.Series = T2.Series 
                                               WHERE T0.DocEntry = {0}";
                    }
                    else
                    {
                        s = @"select T0.""DocSubType"", SUBSTRING(UPPER(T2.""BeginStr""), 1, 1) ""Tipo"", SUBSTRING(IFNULL(T2.""BeginStr"",''), 2, LENGTH(T2.""BeginStr"")) ""Inst""
                                                 ,IFNULL(T0.""U_BPP_MDTD"",'') ""BPP_MDTD"", IFNULL(T0.""U_BPP_MDSD"",'') ""BPP_MDSD"", IFNULL(T0.""U_BPP_MDCD"",'') ""BPP_MDCD"", T0.""CANCELED""
                                             FROM ""ORIN"" T0
                                             JOIN ""NNM1"" T2 ON T0.""Series"" = T2.""Series""
                                            WHERE T0.""DocEntry"" = {0} ";
                    }
                    s = String.Format(s, sDocEntry);
                    oRecordSet.DoQuery(s);
                    sDocSubType = (System.String)(oRecordSet.Fields.Item("DocSubType").Value);
                    Canceled = (System.String)(oRecordSet.Fields.Item("CANCELED").Value);

                    if (Canceled == "N")
                    {
                        if (((System.String)oRecordSet.Fields.Item("BPP_MDTD").Value).Trim() == "")
                            FSBOApp.StatusBar.SetText("No se encuentra ingresado tipo de documento", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                        else if (((System.String)oRecordSet.Fields.Item("BPP_MDSD").Value).Trim() == "")
                            FSBOApp.StatusBar.SetText("No se encuentra ingresado serie del documento", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                        else if (((System.String)oRecordSet.Fields.Item("BPP_MDCD").Value).Trim() == "")
                            FSBOApp.StatusBar.SetText("No se encuentra ingresado correlativo del documento", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                        else
                        {
                            SBO_f = FSBOf;
                            TipoDocElec = "07";
                            EnviarCN_PE_ED(sDocEntry, GlobalSettings.RunningUnderSQLServer, ((System.String)oRecordSet.Fields.Item("BPP_MDSD").Value).Trim(), ((System.String)oRecordSet.Fields.Item("BPP_MDCD").Value).Trim(), TipoDocElec, oForm.BusinessObject.Type, sDocSubType, RUC, TipoDocElec);
                        }
                    }
                    //--
                }
                else if ((BusinessObjectInfo.BeforeAction == false) && (BusinessObjectInfo.EventType == BoEventTypes.et_FORM_DATA_UPDATE) && (BusinessObjectInfo.ActionSuccess))
                {
                    ;
                }//fin eventype
            }
            catch (Exception e)
            {
                FSBOApp.StatusBar.SetText(e.Message, BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                OutLog("FormDataEvent " + e.Message + " ** Trace: " + e.StackTrace);
            }

        }//fin FormDataEvent


        public new void PrintEvent(ref SAPbouiCOM.PrintEventInfo eventInfo, ref Boolean BubbleEvent)
        {
            String tabla;
            //XmlDocument _xmlDocument;
            //XmlNode N;
            //inherited PrintEvent(var eventInfo,var BubbleEvent);
            base.PrintEvent(ref eventInfo, ref BubbleEvent);
            oForm = FSBOApp.Forms.Item(eventInfo.FormUID);

            if ((eventInfo.FormUID.Length > 0) && (eventInfo.WithPrinterPreferences))
            {
                if ((eventInfo.EventType == BoEventTypes.et_PRINT) && (eventInfo.BeforeAction))
                {
                    ;
                    //OutLog(s);
                    //end;
                }
            }
        }//fin PrintEvent

        public new void ReportDataEvent(ref SAPbouiCOM.ReportDataInfo eventInfo, ref Boolean BubbleEvent)
        {
            String tabla;
            base.ReportDataEvent(ref eventInfo, ref BubbleEvent);
            //inherited ReportDataEvent(var eventInfo,var BubbleEvent);
            oForm = FSBOApp.Forms.Item(eventInfo.FormUID);

            //OutLog("ReportData " + eventInfo.EventType.ToString);
            if (eventInfo.FormUID.Length > 0) //and (eventInfo.WithPrinterPreferences) then
            {
                if ((eventInfo.EventType == BoEventTypes.et_PRINT_DATA) && (eventInfo.BeforeAction))
                {
                    ;
                }
            }
        }//fin ReportDataEvent


        //Para PEru EasyDot
        public new void EnviarCN_PE_ED(String DocEntry, Boolean RunningUnderSQLServer, String SeriePE, String FolioNum, String TipoDocElec, String sObjType, String DocSubType, String lRUC, String TipoDocElecAddon)
        {
            String URL;
            String URL_PDF;
            String ProcedimientoE;
            String ProcedimientoD;
            String ProcedimientoR;
            XmlDocument oXml = null;
            String userED;
            String passED;
            TFunctions Reg = new TFunctions();
            SAPbobsCOM.Company Cmpny = SBO_f.Cmpny;
            Reg.SBO_f = SBO_f;
            String Status;
            String sMessage = "";
            Int32 lRetCode;
            String DocDate = "";
            XDocument miXML = null;
            XElement xNodo = null;
            String ExternalFolio;
            SAPbobsCOM.Recordset ors = ((SAPbobsCOM.Recordset)Cmpny.GetBusinessObject(BoObjectTypes.BoRecordset));
            TDLLparaXMLPE Dll = new TDLLparaXMLPE();
            Dll.SBO_f = SBO_f;
            String sXML;
            String MostrarXML = "N";

            try
            {
                if (RunningUnderSQLServer)
                    s = @"SELECT U_URLEasyDot 'URL', ISNULL(U_UserED,'') 'User', ISNULL(U_PwdED,'') 'Pass', ISNULL(U_MostrarXML,'N') 'MostrarXML' FROM [@VID_FEPARAM]";
                else
                    s = @"SELECT ""U_URLEasyDot"" ""URL"", IFNULL(""U_UserED"",'') ""User"", IFNULL(""U_PwdED"",'') ""Pass"", IFNULL(""U_MostrarXML"",'N') ""MostrarXML"" FROM ""@VID_FEPARAM"" ";

                ors.DoQuery(s);
                if (ors.RecordCount == 0)
                    SBO_f.SBOApp.StatusBar.SetText("No se ha ingresado URL", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                else if (((System.String)ors.Fields.Item("URL").Value).Trim() == "")
                    SBO_f.SBOApp.StatusBar.SetText("No se ha ingresado URL", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                else if (((System.String)ors.Fields.Item("User").Value).Trim() == "")
                    throw new Exception("No se encuentra usuario en Parametros");
                else if (((System.String)ors.Fields.Item("Pass").Value).Trim() == "")
                    throw new Exception("No se encuentra password en Parametros");
                else
                {
                    userED = Reg.DesEncriptar((System.String)(ors.Fields.Item("User").Value).ToString().Trim());
                    passED = Reg.DesEncriptar((System.String)(ors.Fields.Item("Pass").Value).ToString().Trim());
                    MostrarXML = ((System.String)ors.Fields.Item("MostrarXML").Value).Trim();

                    URL = ((System.String)ors.Fields.Item("URL").Value).Trim() + "/SendDocument2.ashx";
                    URL_PDF = ((System.String)ors.Fields.Item("URL").Value).Trim() + "/SendPdf.ashx";
                    //validar que exista procedimentos para tipo documento
                    if (RunningUnderSQLServer)
                        s = "select ISNULL(U_ProcNomE,'') 'ProcNomE', ISNULL(U_ProcNomD,'') 'ProcNomD', ISNULL(U_ProcNomR,'') 'ProcNomR' FROM [@VID_FEPROCED] where ISNULL(U_Habili,'N') = 'Y' and U_TipoDocPE = '{0}'";
                    else
                        s = @"select IFNULL(""U_ProcNomE"",'') ""ProcNomE"", IFNULL(""U_ProcNomD"",'') ""ProcNomD"", IFNULL(""U_ProcNomR"",'') ""ProcNomR"" FROM ""@VID_FEPROCED"" where IFNULL(""U_Habili"",'N') = 'Y' and ""U_TipoDocPE"" = '{0}'";

                    s = String.Format(s, TipoDocElec);
                    ors.DoQuery(s);
                    if (ors.RecordCount == 0)
                        throw new Exception("No se encuentra procedimientos para Documento electronico " + TipoDocElec);
                    else
                    {
                        ProcedimientoE = ((System.String)ors.Fields.Item("ProcNomE").Value).Trim();
                        ProcedimientoD = ((System.String)ors.Fields.Item("ProcNomD").Value).Trim();
                        ProcedimientoR = ((System.String)ors.Fields.Item("ProcNomR").Value).Trim();

                        if (ProcedimientoE == "")
                            throw new Exception("No se encuentra procedimiento de encabezado para Documento electronico " + TipoDocElec);
                        else if (ProcedimientoD == "")
                            throw new Exception("No se encuentra procedimiento de detalle para Documento electronico " + TipoDocElec);
                        else if ((ProcedimientoR == "") && (TipoDocElec == "08"))
                            throw new Exception("No se encuentra procedimiento de referencia para Documento electronico " + TipoDocElec);

                        if (RunningUnderSQLServer)
                            s = @"exec {0} {1}, '{2}', '{3}'";//Encabezado
                        else
                            s = @"CALL {0} ({1}, '{2}', '{3}')";
                        s = String.Format(s, ProcedimientoE, DocEntry, TipoDocElec, sObjType);
                        ors.DoQuery(s);

                        if (ors.RecordCount == 0)
                            throw new Exception("No se encuentra datos para Documento electronico " + TipoDocElec);
                        else
                        {
                            var bImpresion = false;
                            ExternalFolio = ((System.String)ors.Fields.Item("IdDocumento").Value).Trim();
                            miXML = new XDocument(
                             new XDeclaration("1.0", "utf-8", "yes"),
                                    new XElement("DocumentoElectronico"));

                            sXML = Dll.GenerarXMLStringCreditNote(ref ors, TipoDocElec, ref miXML, "E");
                            if (sXML == "")
                                throw new Exception("Problema para generar xml Documento electronico " + TipoDocElec);

                            if (TipoDocElec == "07") //para REFERENCIA
                            {
                                if (RunningUnderSQLServer)
                                    s = @"exec {0} {1}, '{2}', '{3}'";
                                else
                                    s = @"CALL {0} ({1}, '{2}', '{3}')";
                                s = String.Format(s, ProcedimientoR, DocEntry, TipoDocElec, sObjType);
                                ors.DoQuery(s);

                                if (ors.RecordCount == 0)
                                    throw new Exception("No se encuentra datos de referencia para Documento electronico " + TipoDocElec);
                                else
                                {
                                    sXML = Dll.GenerarXMLStringCreditNote(ref ors, TipoDocElec, ref miXML, "R");
                                    if (sXML == "")
                                        throw new Exception("Problema para generar xml Documento electronico " + TipoDocElec);
                                }
                            }

                            //DETALLE
                            if (RunningUnderSQLServer)
                                s = @"exec {0} {1}, '{2}', '{3}'";
                            else
                                s = @"CALL {0} ({1}, '{2}', '{3}')";
                            s = String.Format(s, ProcedimientoD, DocEntry, TipoDocElec, sObjType);
                            ors.DoQuery(s);

                            if (ors.RecordCount == 0)
                                throw new Exception("No se encuentra datos de detalle para Documento electronico " + TipoDocElec);
                            else
                            {
                                sXML = Dll.GenerarXMLStringCreditNote(ref ors, TipoDocElec, ref miXML, "D");
                                if (sXML == "")
                                    throw new Exception("Problema para generar xml Documento electronico " + TipoDocElec);

                                oXml = new XmlDocument();
                                using (var xmlReader = miXML.CreateReader())
                                {
                                    oXml.Load(xmlReader);
                                }


                                //Agrega el PDF al xml
                                XmlNode node;
                                if (oXml.SelectSingleNode("//CamposExtras") == null)
                                    node = oXml.CreateNode(XmlNodeType.Element, "CamposExtras", null);
                                else
                                    node = oXml.SelectSingleNode("//CamposExtras");

                                if (MostrarXML == "Y")
                                    SBO_f.oLog.OutLog(oXml.InnerXml);
                                //ENVIO AL PORTAL
                                s = Reg.UpLoadDocumentByUrl2(oXml, null, RunningUnderSQLServer, URL, userED, passED, TipoDocElec + "_" + ExternalFolio);

                                //SBO_f.SBOApp.StatusBar.SetText(s, BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Warning);

                                oXml.LoadXml(s);
                                //var Configuracion = oXml.GetElementsByTagName("Error");
                                var lista = ((XmlElement)oXml.GetElementsByTagName("Error")[0]).GetElementsByTagName("ErrorText");
                                var ErrorText = lista[0].InnerText;
                                if (ErrorText.Length > 250)
                                    ErrorText = ErrorText.Substring(0, 250);
                                lista = ((XmlElement)oXml.GetElementsByTagName("Error")[0]).GetElementsByTagName("ErrorCode");
                                var ErrorCode = lista[0].InnerText;
                                lista = ((XmlElement)oXml.GetElementsByTagName("Error")[0]).GetElementsByTagName("PDF417");
                                var PDF417 = lista[0].InnerText;
                                lista = ((XmlElement)oXml.GetElementsByTagName("Error")[0]).GetElementsByTagName("IdDocument");
                                var IdDocument = lista[0].InnerText;

                                if (ErrorCode != "0")
                                {
                                    SBO_f.SBOApp.StatusBar.SetText("Error envio documento electrónico (1)", SAPbouiCOM.BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                                    SBO_f.oLog.OutLog("Error en envio documento electronico al portal (1) Codigo Error Portal: " + ErrorCode + " Mensaje Portal: " + ErrorText);
                                    //sObjType = "13";
                                    if (ErrorCode == "-103")
                                        Status = "RR";
                                    else
                                        Status = "EE";
                                    sMessage = ErrorText;
                                    var emsg = sMessage;
                                    if (sMessage == "")
                                        sMessage = "Error envio documento electronico a EasyDot";

                                    var oDocumento1 = (SAPbobsCOM.Documents)(Cmpny.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices));
                                    if (oDocumento1.GetByKey(Convert.ToInt32(DocEntry)))
                                    {
                                        DocDate = SBO_f.DateToStr(oDocumento1.DocDate);
                                        if (emsg == "")
                                            oDocumento1.UserFields.Fields.Item("U_EstadoFE").Value = "N";
                                        else if (Status == "EE")
                                            oDocumento1.UserFields.Fields.Item("U_EstadoFE").Value = "P";
                                        else if (Status == "RR")
                                            oDocumento1.UserFields.Fields.Item("U_EstadoFE").Value = "A";
                                        else
                                            oDocumento1.UserFields.Fields.Item("U_EstadoFE").Value = "P";

                                        lRetCode = oDocumento1.Update();
                                        if (lRetCode != 0)
                                        {
                                            s = SBO_f.Cmpny.GetLastErrorDescription();
                                            SBO_f.SBOApp.StatusBar.SetText("Error actualizar documento - " + s, BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Warning);
                                            sMessage = "Error actualizar documento - " + s;
                                            SBO_f.oLog.OutLog(sMessage);
                                        }
                                    }
                                }
                                else
                                {
                                    if (PDF417 == "")
                                    {
                                        SBO_f.oLog.OutLog("No se ha recibido PDF417 -> " + ExternalFolio);
                                        SBO_f.SBOApp.StatusBar.SetText("No se ha recibido PDF417", SAPbouiCOM.BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                                    }
                                    Status = "RR";
                                    //sObjType = "13";
                                    sMessage = "Enviado satisfactoriamente a EasyDot y Aceptado";
                                    SBO_f.SBOApp.StatusBar.SetText("Se ha enviado satisfactoriamente el documento electronico", SAPbouiCOM.BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Success);
                                    var oDocumento = (SAPbobsCOM.Documents)(Cmpny.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oCreditNotes));
                                    if (oDocumento.GetByKey(Convert.ToInt32(DocEntry)))
                                    {
                                        DocDate = SBO_f.DateToStr(oDocumento.DocDate);
                                        oDocumento.Printed = PrintStatusEnum.psYes;
                                        oDocumento.UserFields.Fields.Item("U_EstadoFE").Value = "A";
                                        oDocumento.UserFields.Fields.Item("U_PDF417").Value = PDF417.Trim();
                                        lRetCode = oDocumento.Update();
                                        if (lRetCode != 0)
                                        {
                                            s = SBO_f.Cmpny.GetLastErrorDescription();
                                            SBO_f.SBOApp.StatusBar.SetText("Error actualizar documento - " + s, BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Warning);
                                            sMessage = "Error actualizar documento - " + s;
                                            SBO_f.oLog.OutLog(sMessage);
                                        }
                                        else
                                            bImpresion = true;
                                    }
                                    else
                                    {
                                        sMessage = "No se ha encontrado documento al actualizar Impresion";
                                        bImpresion = false;
                                    }
                                }
                                oXml = null;

                                if (RunningUnderSQLServer)
                                    s = "SELECT DocEntry, U_Status, U_Id FROM [@VID_FELOG] WITH (NOLOCK) WHERE U_DocEntry = {0} AND U_ObjType = '{1}' AND U_SubType = '{2}'";
                                else
                                    s = @"SELECT ""DocEntry"", ""U_Status"", ""U_Id"" FROM ""@VID_FELOG"" WHERE ""U_DocEntry"" = {0} AND ""U_ObjType"" = '{1}' AND ""U_SubType"" = '{2}' ";
                                s = String.Format(s, DocEntry, sObjType, DocSubType);
                                ors.DoQuery(s);
                                if (ors.RecordCount == 0)
                                    Reg.FELOGAdd(Int32.Parse(DocEntry), sObjType, DocSubType, SeriePE, Int32.Parse(FolioNum), Status, sMessage, TipoDocElec, SBO_f.SBOApp.Company.UserName, "", IdDocument, ErrorText, DocDate, ExternalFolio);
                                else
                                {
                                    if ((System.String)(ors.Fields.Item("U_Status").Value) != "RR")
                                    {
                                        SBO_f.SBOApp.StatusBar.SetText("Documento se ha enviado a EasyDot", SAPbouiCOM.BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Success);
                                        Reg.FELOGUptM((System.Int32)(ors.Fields.Item("DocEntry").Value), Int32.Parse(DocEntry), sObjType, DocSubType, SeriePE, Int32.Parse(FolioNum), Status, sMessage, TipoDocElec, SBO_f.SBOApp.Company.UserName, "", (IdDocument == "0" ? ((System.String)ors.Fields.Item("U_Id").Value).Trim() : IdDocument), ErrorText, DocDate, ExternalFolio);
                                    }
                                    else
                                        SBO_f.SBOApp.StatusBar.SetText("Documento ya se ha enviado anteriormente a EasyDot y se encuentra en Sunat", SAPbouiCOM.BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Success);
                                }

                                if ((bImpresion) && (PDF417 != ""))
                                {
                                    //obtiene string de pdf
                                    var sPDF = Reg.PDFenString(TipoDocElecAddon, DocEntry, sObjType, SeriePE, FolioNum, RunningUnderSQLServer);
                                    /*var sjson = @"<root><DocType>{0}</DocType><DocNum>{1}</DocNum><RUC>{2}</RUC><PDF>{3}</PDF></root>";
                                    sjson = String.Format(sjson, TipoDocElec, ExternalFolio, RUC, sPDF);
                                    XmlDocument xm = new XmlDocument();
                                    xm.LoadXml(sjson);
                                    String json = JsonConvert.SerializeXmlNode(xm);*/
                                    var sjson = @"""DocType"":""{0}"", " + Environment.NewLine + @"""DocNum"":""{1}"", " + Environment.NewLine + @"""RUC"":""{2}""," + Environment.NewLine + @"""PDF"":""{3}""";
                                    sjson = String.Format(sjson, TipoDocElec, ExternalFolio, lRUC, sPDF);
                                    sjson = "{" + Environment.NewLine + sjson + Environment.NewLine + "}";
                                    s = Reg.UpLoadDocumentByUrl(null, sjson, RunningUnderSQLServer, URL_PDF, userED, passED, TipoDocElec + "_" + ExternalFolio);
                                    var results = JsonConvert.DeserializeObject<dynamic>(s);
                                    var jStatus = results.Status;
                                    var jDescripcion = results.Descripcion;

                                    if (jStatus.Value == "OK")
                                        SBO_f.SBOApp.StatusBar.SetText("PDF enviado al portal", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Success);
                                    else
                                    {
                                        SBO_f.SBOApp.StatusBar.SetText("PDF no se ha enviado al portal, " + ((System.String)jDescripcion.Value).Trim(), BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                                        SBO_f.oLog.OutLog("PDF no se ha enviado al portal, Tipo Doc " + TipoDocElec + ", Folio " + ExternalFolio + " -> " + ((System.String)jDescripcion.Value).Trim());
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception x)
            {
                SBO_f.SBOApp.StatusBar.SetText("EnviarFE_PE_ED: " + x.Message + " ** Trace: " + x.StackTrace, BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                SBO_f.oLog.OutLog("EnviarFE_PE_ED: " + x.Message + " ** Trace: " + x.StackTrace);
            }
            finally
            {
                SBO_f._ReleaseCOMObject(ors);
                SBO_f._ReleaseCOMObject(oXml);
                SBO_f._ReleaseCOMObject(miXML);
                SBO_f._ReleaseCOMObject(xNodo);
            }
        }


        private Boolean ValidarDatosFE_PE()
        {
            Boolean _result;
            SAPbouiCOM.DBDataSource oDBDSDir;
            SAPbouiCOM.DBDataSource oDBDSH;
            TFunctions Param;
            Boolean DocElec;
            String Tabla;
            Int32 i;
            SAPbouiCOM.EditText oEditText;
            SAPbouiCOM.ComboBox oComboBox;
            String TipoLinea = "";
            String TipoDoc = "";
            String TipoDocElec = "";
            String[] CaracteresInvalidos = { "Ñ", "°", "|", "!", @"""", "#", "$", "=", "?", "\\", "¿", "¡", "~", "´", "+", "{", "}", "[", "]", "-", ":", "%" };
            String s1;
            Int32 CantLineas;


            try
            {
                _result = true;
                oDBDSDir = oForm.DataSources.DBDataSources.Item("RIN12");
                oDBDSH = oForm.DataSources.DBDataSources.Item("ORIN");

                var sDocSubType = (System.String)(oDBDSH.GetValue("DocSubType", 0)).Trim();

                if (sDocSubType == "--") //Nota de Credito
                    TipoDocElec = "07";


                if ((TipoDocElec == "07"))
                {
                    if ((System.String)(oDBDSDir.GetValue("CityB", 0)).Trim() == "")
                    {
                        FSBOApp.StatusBar.SetText("Debe ingresar ciudad en Destinatario de Factura", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                        _result = false;
                    }

                    //if (((System.String)(oDBDSDir.GetValue("CityS", 0)).Trim() == "") && (_result))
                    //{
                    //    FSBOApp.StatusBar.SetText("Debe ingresar ciudad en Destinatario de Despacho", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                    //    _result = false;
                    //}

                    if (((System.String)(oDBDSDir.GetValue("BlockB", 0)).Trim() == "") && (_result))
                    {
                        FSBOApp.StatusBar.SetText("Debe ingresar comuna en Destinatario de Factura", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                        _result = false;
                    }

                    //if (((System.String)(oDBDSDir.GetValue("CountyS", 0)).Trim() == "") && (_result))
                    //{
                    //    FSBOApp.StatusBar.SetText("Debe ingresar comuna en Destinatario de Despacho", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                    //    _result = false;
                    //}

                    if (((System.String)(oDBDSDir.GetValue("StreetB", 0)).Trim() == "") && (_result))
                    {
                        FSBOApp.StatusBar.SetText("Debe ingresar calle en Destinatario de Factura", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                        _result = false;
                    }

                    //if (((System.String)(oDBDSDir.GetValue("StreetS", 0)).Trim() == "") && (_result))
                    //{
                    //    FSBOApp.StatusBar.SetText("Debe ingresar calle en Destinatario de Despacho", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                    //    _result = false;
                    //}

                    s = (System.String)(oDBDSH.GetValue("CardName", 0)).Trim();
                    if ((s == "") && (_result))
                    {
                        FSBOApp.StatusBar.SetText("Debe ingresar Nombre Cliente", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                        _result = false;
                    }

                    ////valida RUC
                    //se deja comentado, por problemas en la validacion de un cliente, Jimmy colocara una validacion en el TN 20151204
                    //if (_result)
                    //{
                    //    Param = new TFunctions();
                    //    Param.SBO_f = FSBOf;
                    //    s = Param.ValidarRuc((System.String)(oDBDSH.GetValue("LicTradNum", 0)));
                    //    if (s != "OK")
                    //    {
                    //        FSBOApp.StatusBar.SetText(s, BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                    //        _result = false;
                    //    }
                    //}

                    if (_result)
                    {

                        if (GlobalSettings.RunningUnderSQLServer)
                        {
                            //s = "select ISNULL(U_ValDescL,'Y') 'ValDescL' from [@VID_FEPARAM]";
                            s1 = "select ISNULL(U_CantLineas,0) CantLineas from [@VID_FEPROCED] where U_TipoDocPE = '" + TipoDocElec + "' and U_Habili = 'Y'";
                        }

                        else
                        {
                            //s = @"select IFNULL(""U_ValDescL"",'Y') ""ValDescL"" from ""@VID_FEPARAM"" ";
                            s1 = @"select IFNULL(""U_CantLineas"",0) ""CantLineas"" from ""@VID_FEPROCED"" where ""U_TipoDocPE"" = '" + TipoDocElec + @"' and ""U_Habili"" = 'Y'";
                        }

                        oRecordSet.DoQuery(s1);
                        if (oRecordSet.RecordCount > 0)
                            CantLineas = (System.Int32)(oRecordSet.Fields.Item("CantLineas").Value);
                        else
                        {
                            FSBOApp.StatusBar.SetText("Debe parametrizar el maximo de lineas para documento " + TipoDocElec, BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                            return false;
                        }

                        oComboBox = (ComboBox)(oForm.Items.Item("3").Specific);
                        TipoDoc = oComboBox.Selected.Value.Trim();
                        if (TipoDoc == "S")
                            mtx = (Matrix)(oForm.Items.Item("39").Specific);
                        else
                            mtx = (Matrix)(oForm.Items.Item("38").Specific);


                        if ((mtx.RowCount - 1 > CantLineas) && (((System.String)oDBDSH.GetValue("SummryType", 0)).Trim() == "N")) //valida total de lineas solo cuando no es resumen
                        {
                            FSBOApp.StatusBar.SetText("Cantidad de lineas supera lo permitido, parametrización FE", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                            return false;
                        }

                        i = 1;
                        while (i < mtx.RowCount)
                        {
                            if (TipoDoc == "S") //System.String(oDBDSH.GetValue("DocType",0)).Trim()
                            {
                                TipoLinea = "";
                            }
                            else
                            {
                                oComboBox = (ComboBox)(mtx.Columns.Item("257").Cells.Item(i).Specific);
                                TipoLinea = (System.String)(oComboBox.Selected.Value);
                            }

                            //if ((System.String)(oRecordSet.Fields.Item("ValDescL").Value) == "Y")
                            //{
                            //    if (TipoDoc == "S") //System.String(oDBDSH.GetValue("DocType",0)).Trim()
                            //    {
                            //        oEditText = (EditText)(mtx.Columns.Item("6").Cells.Item(i).Specific);
                            //    }
                            //    else
                            //    {
                            //        oEditText = (EditText)(mtx.Columns.Item("15").Cells.Item(i).Specific);
                            //    }

                            //    if ((Convert.ToDouble((System.String)(oEditText.Value)) < 0) && (TipoLinea == ""))
                            //    {
                            //        s = "Descuento negativo en la linea " + Convert.ToString(i);
                            //        FSBOApp.StatusBar.SetText(s, BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                            //        _result = false;
                            //        i = mtx.RowCount;
                            //    }
                            //}

                            if (_result)
                            {
                                if (TipoDoc == "S")
                                    oEditText = (EditText)(mtx.Columns.Item("1").Cells.Item(i).Specific);
                                else
                                    oEditText = (EditText)(mtx.Columns.Item("3").Cells.Item(i).Specific);

                                s = oEditText.Value;
                                if ((s == "") && (TipoLinea == ""))
                                {
                                    FSBOApp.StatusBar.SetText("Debe ingresar descripción en la linea " + Convert.ToString(i), BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                                    _result = false;
                                    i = mtx.RowCount;
                                }

                            }

                            i++;
                        }


                        //validacion solo para nota de debito
                        if (TipoDocElec == "07")
                        {
                            //Validacion tipo de operacion
                            if ((System.String)(oDBDSH.GetValue("U_BPP_MDTN", 0)).Trim() == "")
                            {
                                FSBOApp.StatusBar.SetText("Debe ingresar tipo de operacion", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                                return false;
                            }
                            else
                            {
                                if (GlobalSettings.RunningUnderSQLServer)
                                {
                                    s = @"select U_TypeCode
                                            from [@FM_NOTES] 
                                           where Code = '{0}' ";
                                }
                                else
                                {
                                    s = @"select ""U_TypeCode""
                                            from ""@FM_NOTES""
                                           where ""Code"" = '{0}' ";
                                }
                                s = String.Format(s, (System.String)(oDBDSH.GetValue("U_BPP_MDTN", 0)).Trim());
                                oRecordSet.DoQuery(s);
                                if (oRecordSet.RecordCount == 0)
                                {
                                    FSBOApp.StatusBar.SetText("No se encuentra tipo de operacion", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                                    return false;
                                }
                                //else if (((System.String)(oRecordSet.Fields.Item("Distribuido").Value)).Trim() != "02")
                                else if (((System.String)(oDBDSH.GetValue("U_BPP_MDTN", 0)).Trim() == "11") || ((System.String)(oDBDSH.GetValue("U_BPP_MDTN", 0)).Trim() == "10") || ((System.String)(oDBDSH.GetValue("U_BPP_MDTN", 0)).Trim() == "04"))
                                {
                                    FSBOApp.StatusBar.SetText("Debe seleccionar tipo de operacion valida por FM", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                                    return false;
                                }
                            }
                        }
                    }
                }

                return _result;
            }
            catch (Exception e)
            {
                FSBOApp.StatusBar.SetText(e.Message, BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                OutLog("ValidarDatosFE_PE " + e.Message + " ** Trace: " + e.StackTrace);
                return false;
            }

        }

    }//fin class
}
