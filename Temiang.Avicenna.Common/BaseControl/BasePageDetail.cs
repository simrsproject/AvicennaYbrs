using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Common
{
    public class BasePageDetail : BasePage
    {
        private RadAjaxManager _ajaxManager;
        private RadAjaxPanel _ajaxPanel;
        private ContentPlaceHolder _contentPlaceHolder;
        private List<EntryControl> _listControlForEntry;
        private RadToolBar _toolBarMenuData;
        private RadWindow _windowSearch;
        private int? _autoSaveInterval = null;

        public int AutoSaveInterval
        {
            get
            {
                if (_autoSaveInterval == null)
                    _autoSaveInterval = Convert.ToInt16(Convert.ToDecimal(new Temiang.Avicenna.Common.Fraction(AppParameter.GetParameterValue(AppParameter.ParameterItem.AutoSaveInterval))) * 60);
                return _autoSaveInterval ?? 0;
            }
        }

        public virtual string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }

        /// <summary>
        /// Untuk mode popup atau dijalankan didalam RadWindow set IsSingleRecordMode=true
        /// </summary>
        public bool IsSingleRecordMode
        {
            // diset public krn diakses dari masterpage
            get
            {
                return !MainMenu.Visible;
            }
            set
            {
                MainMenu.Visible = !value;
            }
        }

        public bool IsUsingBeforeVoidValidation;
        public bool IsUsingBeforeUnapprovalValidation;
        public string ReasonValidation;

        public string UrlAuditLog
        {
            get { return Helper.UrlRoot() + "/Module/ControlPanel/AuditLog/AuditLogView/AuditLogList.aspx"; }
        }

        public string UrlPageList
        {
            get
            {
                if (ViewState["fw_UrlPageList"] == null)
                    ViewState["fw_UrlPageList"] = "";
                return (string)ViewState["fw_UrlPageList"];
            }
            set { ViewState["fw_UrlPageList"] = value; }
        }

        public string UrlPageSearch
        {
            get
            {
                if (ViewState["fw_UrlPageSearch"] == null)
                    ViewState["fw_UrlPageSearch"] = "";
                return (string)ViewState["fw_UrlPageSearch"];
            }
            set { ViewState["fw_UrlPageSearch"] = value; }
        }

        /// <summary>
        /// If true will check deadline edit & add after discharge date for IPR & reg date for non IPR
        /// but will ignore if IsOpenMR = true
        /// </summary>
        protected bool IsMedicalRecordEntry
        {
            get
            {
                if (ViewState["fw_mre"] == null)
                    ViewState["fw_mre"] = false;
                return (bool)ViewState["fw_mre"];
            }
            set
            {
                ViewState["fw_mre"] = value;
            }
        }
        public virtual bool OnGetStatusMenuEdit()
        {
            return true;
        }
        public virtual string OnGetScriptToolBarSaveClicking()
        {
            return string.Empty;
        }
        public virtual bool OnGetStatusMenuDelete()
        {
            return true;
        }

        public virtual bool? OnGetStatusMenuApproval()
        {
            return true;
        }
        public virtual bool OnGetStatusMenuUnApprovalEnabled()
        {
            return true;
        }
        public virtual bool OnGetStatusMenuVoid()
        {
            return true;
        }

        private HiddenField _fw_hdnDataMode = null;
        public AppEnum.DataMode DataModeCurrent
        {
            get
            {
                //if (ViewState["fw_DataMode"] == null)
                //    ViewState["fw_DataMode"] = AppEnum.DataMode.Read;
                //return (AppEnum.DataMode)ViewState["fw_DataMode"];

                // Viewstate diganti ke HiddenField untuk keperluan autosave yg menggunaka ajax
                // sedangkan viewstate tidak bisa diupdate melalui ajax (Handono 230127)
                if (_fw_hdnDataMode == null)
                    _fw_hdnDataMode = (HiddenField)Helper.FindControlRecursive(Master, "fw_hdnDataMode");

                if (string.IsNullOrWhiteSpace(_fw_hdnDataMode.Value))
                    _fw_hdnDataMode.Value = AppEnum.DataMode.Read.ToInt().ToString();
                return (AppEnum.DataMode)_fw_hdnDataMode.Value.ToInt();
            }
            set
            {
                //if (ViewState["fw_DataMode"] == null)
                //    ViewState["fw_DataMode"] = AppEnum.DataMode.Read;
                //else
                //    if (IsPostBack && ViewState["fw_DataMode"].Equals(value)) return;

                //var oldVal = (AppEnum.DataMode)ViewState["fw_DataMode"];

                //ViewState["fw_DataMode"] = value;

                // Viewstate diganti ke HiddenField untuk keperluan autosave yg menggunaka ajax
                // sedangkan viewstate tidak bisa diupdate melalui ajax (Handono 230127)
                if (_fw_hdnDataMode == null)
                    _fw_hdnDataMode = (HiddenField)Helper.FindControlRecursive(Master, "fw_hdnDataMode");

                if (string.IsNullOrWhiteSpace(_fw_hdnDataMode.Value))
                    _fw_hdnDataMode.Value = AppEnum.DataMode.Read.ToInt().ToString();
                else
                    if (IsPostBack && _fw_hdnDataMode.Value.Equals(value.ToInt().ToString())) return;

                var oldVal = (AppEnum.DataMode)_fw_hdnDataMode.Value.ToInt();
                _fw_hdnDataMode.Value = value.ToInt().ToString();

                // Default toolbar visible on off
                bool isModusRead = (value.Equals(AppEnum.DataMode.Read));

                ToolBarMenuEdit.Visible = isModusRead; // Edit
                ToolBarMenuData.Items[2].Visible = isModusRead; // Separator
                ToolBarMenuDelete.Visible = isModusRead; // Delete

                ToolBarMenuSave.Visible = !isModusRead; //Save
                ToolBarMenuData.Items[5].Visible = !isModusRead; //Cancel

                var isSingleRecordMode = IsSingleRecordMode;
                ToolBarMenuData.Items[6].Visible = isModusRead && !isSingleRecordMode; // Sep
                ToolBarMenuData.Items[7].Visible = isModusRead && !isSingleRecordMode; // Presv
                ToolBarMenuData.Items[8].Visible = isModusRead && !isSingleRecordMode; // Next
                ToolBarMenuData.Items[9].Visible = isModusRead && !isSingleRecordMode; // Sep
                ToolBarMenuData.Items[10].Visible = isModusRead && !isSingleRecordMode; // Search
                ToolBarMenuData.Items[11].Visible = isModusRead && !isSingleRecordMode; // List
                ToolBarMenuData.Items[12].Visible = isModusRead && !isSingleRecordMode; // AuditLog
                ToolBarMenuData.Items[13].Visible = isModusRead && !isSingleRecordMode; // Separator

                //Approval UnApproval
                ToolBarMenuData.Items[13].Visible = isModusRead && ToolBar.UnApprovalVisible;
                ToolBarMenuData.Items[14].Visible = isModusRead && ToolBar.UnApprovalVisible;
                ToolBarMenuData.Items[15].Visible = false;
                ToolBarMenuData.Items[14].Enabled = false;

                //Void Unvoid
                ToolBarMenuData.Items[16].Visible = isModusRead;
                ToolBarMenuVoid.Visible = isModusRead;
                ToolBarMenuUnVoid.Visible = false;
                ToolBarMenuVoid.Enabled = false;

                var tbarPrint = (RadToolBarDropDown)ToolBarMenuData.Items[20];
                tbarPrint.Visible = isModusRead && ToolBar.PrintVisible && tbarPrint.Buttons.Count > 0; //Print
                ToolBarMenuData.Items[19].Visible = tbarPrint.Visible;

                //Add Menu
                ToolBarMenuData.Items[0].Visible = UserAccess.IsMenuAddVisible && isModusRead && !isSingleRecordMode; // New

                //Home Menu
                ToolBarMenuData.Items[21].Visible = UserAccess.IsMenuHomeVisible && isModusRead && !isSingleRecordMode;
                ToolBarMenuData.Items[22].Visible = UserAccess.IsMenuHomeVisible && isModusRead && !isSingleRecordMode;

                //Close Menu
                ToolBarMenuData.Items[26].Visible = isModusRead && !!isSingleRecordMode;
                ToolBarMenuData.Items[27].Visible = isModusRead && !!isSingleRecordMode; // Close

                SetReadOnlyStatus(value, ContentPlaceHolderMain, _listControlForEntry);

                OnDataModeChanged(oldVal, value);

            }
        }

        public RadToolBarItem ToolBarMenuAdd
        {
            get
            {
                return ToolBarMenuData.Items[0];
            }
        }

        public RadToolBarItem ToolBarMenuEdit
        {
            get
            {
                return ToolBarMenuData.Items[1];
            }
        }

        public RadToolBarItem ToolBarMenuDelete
        {
            get
            {
                return ToolBarMenuData.Items[3];
            }
        }

        public RadToolBarItem ToolBarMenuSave
        {
            get
            {
                return ToolBarMenuData.Items[4];
            }
        }

        public RadToolBarItem ToolBarMenuMovePrev
        {
            get
            {
                return ToolBarMenuData.Items[7];
            }
        }

        public RadToolBarItem ToolBarMenuMoveNext
        {
            get
            {
                return ToolBarMenuData.Items[8];
            }
        }

        public RadToolBarItem ToolBarMenuSearch
        {
            get
            {
                return ToolBarMenuData.Items[10];
            }
        }

        public RadToolBarItem ToolBarMenuList
        {
            get
            {
                return ToolBarMenuData.Items[11];
            }
        }

        public RadToolBarItem ToolBarMenuApproval
        {
            get
            {
                return ToolBarMenuData.Items[14];
            }
        }

        public RadToolBarItem ToolBarMenuUnApproval
        {
            get
            {
                return ToolBarMenuData.Items[15];
            }
        }

        public RadToolBarItem ToolBarMenuVoid
        {
            get
            {
                return ToolBarMenuData.Items[17];
            }
        }

        public RadToolBarItem ToolBarMenuUnVoid
        {
            get
            {
                return ToolBarMenuData.Items[18];
            }
        }

        public RadToolBarDropDown ToolBarMenuPrint
        {
            get
            {
                return (RadToolBarDropDown)ToolBarMenuData.Items[20];
            }
        }

        public RadToolBarItem ToolBarMenuSEPtoReg
        {
            get
            {
                return ToolBarMenuData.Items[24];
            }
        }

        public RadToolBarItem ToolBarMenuReloadLab
        {
            get
            {
                return ToolBarMenuData.Items[25];
            }
        }

        public bool ToolBarMenuAutoSaveVisible
        {
            get
            {
                if (_toolBar == null)
                    _toolBar = new _ToolBar();
                return _toolBar.AutoSaveVisible;
            }
        }
        public bool ToolBarMenuSaveAndEditVisible
        {
            get
            {
                if (_toolBar == null)
                    _toolBar = new _ToolBar();
                return _toolBar.SaveAndEditVisible;
            }
        }

        private _ToolBar _toolBar;
        protected _ToolBar ToolBar
        {
            get
            {
                if (_toolBar == null)
                    _toolBar = new _ToolBar();
                return _toolBar;
            }
        }

        protected class _ToolBar
        {
            private bool _addVisible = true;
            private bool _editVisible = true;
            private bool _deleteVisible = true;
            private bool _printVisible = true;
            private bool _voidUnVoidVisible = true;
            private bool _unVoidVisible = true;
            private bool _approvalUnApprovalVisible = true;
            private bool _unApprovalVisible = true;
            private bool _navigationVisible = true;
            private bool _saveAndEditVisible = false;
            private bool _autoSaveVisible = false;

            private bool _addEnabled = true;
            private bool _editEnabled = true;
            private bool _deleteEnabled = true;
            private bool _printEnabled = true;
            private bool _voidEnabled = true;
            private bool _unVoidEnabled = true;
            private bool _approvalEnabled = true;
            private bool _unApprovalEnabled = true;


            public bool AddVisible
            {
                get { return _addVisible; }
                set { _addVisible = value; }
            }

            public bool EditVisible
            {
                get { return _editVisible; }
                set { _editVisible = value; }
            }
            public bool DeleteVisible
            {
                get { return _deleteVisible; }
                set { _deleteVisible = value; }
            }
            public bool PrintVisible
            {
                get { return _printVisible; }
                set { _printVisible = value; }
            }

            public bool VoidUnVoidVisible
            {
                get { return _voidUnVoidVisible; }
                set { _voidUnVoidVisible = value; }
            }

            public bool ApprovalUnApprovalVisible
            {
                get { return _approvalUnApprovalVisible; }
                set { _approvalUnApprovalVisible = value; }
            }

            public bool UnApprovalVisible
            {
                get { return _unApprovalVisible; }
                set { _unApprovalVisible = value; }
            }
            public bool NavigationVisible
            {
                get { return _navigationVisible; }
                set { _navigationVisible = value; }
            }
            public bool AutoSaveVisible
            {
                get { return _autoSaveVisible; }
                set { _autoSaveVisible = value; }
            }
            public bool SaveAndEditVisible
            {
                get { return _saveAndEditVisible; }
                set { _saveAndEditVisible = value; }
            }

            // Enabled
            public bool AddEnabled
            {
                get { return _addEnabled; }
                set { _addEnabled = value; }
            }

            public bool EditEnabled
            {
                get { return _editEnabled; }
                set { _editEnabled = value; }
            }
            public bool DeleteEnabled
            {
                get { return _deleteEnabled; }
                set { _deleteEnabled = value; }
            }
            public bool PrintEnabled
            {
                get { return _printEnabled; }
                set { _printEnabled = value; }
            }

            public bool VoidEnabled
            {
                get { return _voidEnabled; }
                set { _voidEnabled = value; }
            }
            public bool UnVoidEnabled
            {
                get { return _unVoidEnabled; }
                set { _unVoidEnabled = value; }
            }
            public bool ApprovalEnabled
            {
                get { return _approvalEnabled; }
                set { _approvalEnabled = value; }
            }
            public bool UnApprovalEnabled
            {
                get { return _unApprovalEnabled; }
                set { _unApprovalEnabled = value; }
            }
        }

        public RadTextBox ToolBarMenuQuickSearch
        {
            get { return (RadTextBox)ToolBarMenuData.Items[7].FindControl("txtQuickSearch"); }
        }

        protected RadAjaxPanel AjaxPanel
        {
            get { return _ajaxPanel ?? (_ajaxPanel = (RadAjaxPanel)Helper.FindControlRecursive(this, "fw_ajxPanel")); }
        }

        protected RadWindow WindowSearch
        {
            get { return _windowSearch ?? (_windowSearch = (RadWindow)Helper.FindControlRecursive(this, "fw_WinSearch")); }
        }

        protected RadToolBar ToolBarMenuData
        {
            get
            {
                return _toolBarMenuData ?? (_toolBarMenuData = (RadToolBar)Helper.FindControlRecursive(this, "fw_tbarData"));
            }
        }

        protected RadAjaxManager AjaxManager
        {
            get
            {
                return _ajaxManager ?? (_ajaxManager = (RadAjaxManager)Helper.FindControlRecursive(this, "fw_RadAjaxManager"));
            }
        }

        protected ContentPlaceHolder ContentPlaceHolderMain
        {
            get
            {
                return _contentPlaceHolder ?? (_contentPlaceHolder = (ContentPlaceHolder)Helper.FindControlRecursive(this, "ContentPlaceHolder1"));
            }
        }

        protected virtual void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);

            if (!string.IsNullOrWhiteSpace(Request.QueryString["isrm"]) && Request.QueryString["isrm"]=="1")
                IsSingleRecordMode = true;

            if (IsPostBack)
            {
                //AuditLog
                var accessAudit = new UserAccess(AppSession.UserLogin.UserID, AppConstant.Program.AuditLogView);

                ToolBarMenuData.Items[12].Enabled = accessAudit.IsExist; // AuditLog

                ////User Access
                //if (UserAccess.IsExist)
                //{
                //    ToolBarMenuData.Items[0].Enabled = UserAccess.IsProgramAddAble && UserAccess.IsAddAble; // New
                //    ToolBarMenuData.Items[1].Enabled = UserAccess.IsProgramEditAble && UserAccess.IsEditAble; // Edit
                //    ToolBarMenuData.Items[3].Enabled = UserAccess.IsProgramDeleteAble && UserAccess.IsDeleteAble; //Delete
                //}

            }

            _listControlForEntry = new List<EntryControl>();
            PopulateListControlForEntry(ContentPlaceHolderMain, _listControlForEntry);

            ToolBarMenuData.ButtonClick += ToolBarMenuData_ButtonClick;
            OnInitializeAjaxManager(AjaxManager);

            AjaxManager.AjaxSettings.Clear();
            if ((AutoSaveInterval > 0 && ToolBar.AutoSaveVisible) || ToolBar.SaveAndEditVisible)
            {
                var fw_btnAutoSave = (Button)Helper.FindControlRecursive(Master, "fw_btnAutoSave");
                AjaxManager.AjaxSettings.AddAjaxSetting(fw_btnAutoSave, (HiddenField)Helper.FindControlRecursive(Master, "fw_hdnRecordHasChanged"));
                AjaxManager.AjaxSettings.AddAjaxSetting(fw_btnAutoSave, (RadNotification)Helper.FindControlRecursive(Master, "fw_radNotif"));
                AjaxManager.AjaxSettings.AddAjaxSetting(fw_btnAutoSave, (HiddenField)Helper.FindControlRecursive(Master, "fw_hdnDataMode"));
            }
            OnInitializeAjaxManagerSettingsCollection(AjaxManager.AjaxSettings);
        }

        public static void PopulateListControlForEntry(Control root, List<EntryControl> listControlForEntry)
        {
            // Fungsi ini harus dipanggil saat init complete sehingga status 
            // control nya masih belum dirubah waktu runtime 
            // bila bisa dientry maka dianggap ctl tsb untuk entry
            // atau jika waktu designer diset bisa dientry maka dianggap ctl tsb untuk entry

            foreach (Control ctl in root.Controls)
            {
                switch (ctl.GetType().Name)
                {
                    case "RadDatePicker":
                        var dtCtl = (RadDatePicker)ctl;
                        if (dtCtl.Enabled)
                            listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                        break;
                    case "RadEditor":
                        {
                            var edCtl = (RadEditor)ctl;
                            if (edCtl.Enabled)
                                listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                            break;
                        }
                    case "RadAutoCompleteBox":
                        {
                            var edCtl = (RadAutoCompleteBox)ctl;
                            if (edCtl.Enabled)
                                listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                            break;
                        }
                    case "RadTextBox":
                    case "RadMaskedTextBox":
                    case "RadNumericTextBox":
                        // Jika waktu designer diset bisa dientry maka dianggap ctl tsb untuk entry
                        var inputCtl = (RadInputControl)ctl;
                        if (!inputCtl.ReadOnly && inputCtl.Enabled)
                            listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                        break;
                    case "RadComboBox":
                        var cbo = (RadComboBox)ctl;
                        if (cbo.Enabled)
                        {
                            cbo.EmptyMessage = "Select...";
                            listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                        }
                        break;
                    case "RadTimePicker":
                        var time = (RadTimePicker)ctl;
                        if (time.Enabled)
                            listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                        break;
                    case "RadDateTimePicker":
                        var dttime = (RadDateTimePicker)ctl;
                        if (dttime.Enabled)
                            listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                        break;
                    case "CheckBox":
                        var chk = (CheckBox)ctl;
                        if (chk.Enabled)
                            listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                        break;
                    case "RadColorPicker":
                        var clrPicker = (RadColorPicker)ctl;
                        if (clrPicker.Enabled)
                            listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                        break;
                    case "RadioButtonList":
                        {
                            var rbl = (RadioButtonList)ctl;
                            if (rbl.Enabled)
                                listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                            break;
                        }
                    case "RadRadioButtonList":
                        {
                            var octl = (RadRadioButtonList)ctl;
                            if (octl.Enabled)
                                listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                            break;
                        }
                    case "RadCheckBox":
                        {
                            var octl = (RadCheckBox)ctl;
                            if (octl.Enabled)
                                listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                            break;
                        }
                    case "RadCheckBoxList":
                        {
                            var octl = (RadCheckBoxList)ctl;
                            if (octl.Enabled)
                                listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                            break;
                        }
                    default:
                        if (ctl.HasControls())
                            PopulateListControlForEntry(ctl, listControlForEntry);
                        break;
                }
            }
        }

        public static void SetReadOnlyStatus(AppEnum.DataMode datamode, ContentPlaceHolder contentPlace, List<EntryControl> listControlForEntry)
        {
            foreach (var ctlForEntry in listControlForEntry)
            {
                var ctl = Helper.FindControlRecursive(contentPlace, ctlForEntry.ControlID);
                if (ctl != null)
                    switch (ctlForEntry.ControlType)
                    {
                        case "RadTextBox":
                            var txt = (RadTextBox)ctl;
                            txt.ReadOnly = (datamode == AppEnum.DataMode.Read);

                            if (txt.ClientEvents.OnButtonClick != string.Empty)
                                txt.ShowButton = datamode != AppEnum.DataMode.Read;
                            break;
                        case "RadEditor":
                            {
                                var edit = (RadEditor)ctl;
                                edit.EditModes = (datamode == AppEnum.DataMode.Read) ? EditModes.Preview : EditModes.Design;
                                //edit.Enabled = (datamode != AppEnum.DataMode.Read);
                                break;
                            }
                        case "RadAutoCompleteBox":
                            {
                                var edit = (RadAutoCompleteBox)ctl;
                                edit.Enabled = (datamode != AppEnum.DataMode.Read);
                                break;
                            }
                        case "RadNumericTextBox":
                            var txtNum = (RadNumericTextBox)ctl;
                            txtNum.ReadOnly = (datamode == AppEnum.DataMode.Read);
                            break;
                        case "CheckBox":
                            ((CheckBox)ctl).Enabled = (datamode != AppEnum.DataMode.Read);
                            break;
                        case "RadioButtonList":
                            ((RadioButtonList)ctl).Enabled = (datamode != AppEnum.DataMode.Read);
                            break;
                        case "RadComboBox":
                            ((RadComboBox)ctl).Enabled = (datamode != AppEnum.DataMode.Read);
                            break;
                        case "RadDatePicker":
                            ((RadDatePicker)ctl).Enabled = (datamode != AppEnum.DataMode.Read);
                            break;
                        case "RadDateTimePicker":
                            ((RadDateTimePicker)ctl).Enabled = (datamode != AppEnum.DataMode.Read);
                            break;
                        case "RadTimePicker":
                            ((RadTimePicker)ctl).Enabled = (datamode != AppEnum.DataMode.Read);
                            break;
                        case "RadColorPicker":
                            ((RadColorPicker)ctl).Enabled = (datamode != AppEnum.DataMode.Read);
                            break;
                        case "RadMaskedTextBox":
                            ((RadMaskedTextBox)ctl).ReadOnly = (datamode == AppEnum.DataMode.Read);
                            break;
                        case "RadRadioButtonList":
                            ((RadRadioButtonList)ctl).Enabled = (datamode != AppEnum.DataMode.Read);
                            break;
                        case "RadCheckBox":
                            ((RadCheckBox)ctl).Enabled = (datamode != AppEnum.DataMode.Read);
                            break;
                        case "RadCheckBoxList":
                            ((RadCheckBoxList)ctl).Enabled = (datamode != AppEnum.DataMode.Read);
                            break;
                    }
            }
        }

        protected virtual void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected virtual void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected void ShowInformationHeader(string messageText)
        {
            var pnlInfo = (Panel)Helper.FindControlRecursive(Master, "fw_PanelInfo");
            var lblInfo = (Label)Helper.FindControlRecursive(Master, "fw_LabelInfo");
            lblInfo.Text = messageText;
            pnlInfo.Visible = true;
        }

        protected void HideInformationHeader()
        {
            var pnlInfo = (Panel)Helper.FindControlRecursive(Master, "fw_PanelInfo");
            var lblInfo = (Label)Helper.FindControlRecursive(Master, "fw_LabelInfo");
            lblInfo.Text = string.Empty;
            pnlInfo.Visible = false;
        }

        private void ToolBarMenuData_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var args = new ValidateArgs();
            HideInformationHeader();
            switch (e.Item.Value)
            {
                case "home":
                    RedirectFromContent(UserAccess.NavigateUrl);
                    break;
                case "new":
                    if (!string.IsNullOrWhiteSpace(RegistrationNo) && IsMedicalRecordEntry)
                        MedicalRecordEditableValidate(args, RegistrationNo);

                    if (!args.IsCancel)
                    {
                        OnBeforeMenuNewClick(args);
                        if (!args.IsCancel)
                        {
                            var info = (Panel)Helper.FindControlRecursive(Master, "fw_PanelStatus");
                            info.Visible = false;
                            DataModeCurrent = AppEnum.DataMode.New;
                            OnMenuNewClick();
                        }
                    }
                    break;
                case "edit":
                    if (!string.IsNullOrWhiteSpace(RegistrationNo) && IsMedicalRecordEntry)
                        MedicalRecordEditableValidate(args, RegistrationNo);

                    if (!args.IsCancel)
                    {
                        OnBeforeMenuEditClick(args);
                        if (!args.IsCancel)
                        {
                            var info = (Panel)Helper.FindControlRecursive(Master, "fw_PanelStatus");
                            info.Visible = false;
                            DataModeCurrent = AppEnum.DataMode.Edit;
                            OnMenuEditClick();
                        }
                    }
                    break;
                case "save":
                    e.Item.Enabled = true;
                    if (Page.IsValid)
                    {
                        var notif = string.Empty;
                        if (DataModeCurrent == AppEnum.DataMode.Edit)
                        {
                            OnMenuSaveEditClick(args);
                            if (!args.IsCancel && string.IsNullOrWhiteSpace(args.MessageText))
                                notif = "Current Record has saved";
                        }
                        else
                        {
                            OnMenuSaveNewClick(args);
                            if (!args.IsCancel && string.IsNullOrWhiteSpace(args.MessageText))
                                notif = "New Record has saved";
                        }

                        if (!string.IsNullOrEmpty(notif))
                        {
                            var fw_radNotif = (RadNotification)Helper.FindControlRecursive(Master, "fw_radNotif");
                            fw_radNotif.Text = notif;
                            fw_radNotif.Title = "Save Confirmation";
                            fw_radNotif.Show();
                        }
                        if (!args.IsCancel)
                        {
                            IsRecordHasChanged = true;
                            DataModeCurrent = AppEnum.DataMode.Read;
                            OnPopulateEntryControl();
                            RefreshMenuStatus();

                            //Reset Record List untuk PageList
                            Session.Remove(SessionNameForList);
                        }
                    }

                    break;
                case "delete":
                    if (Page.IsValid)
                    {
                        OnMenuDeleteClick(args);

                        if (!args.IsCancel)
                        {
                            IsRecordHasChanged = true;
                            if (IsSingleRecordMode)
                            {
                                //Close
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeAndApply", "CloseAndApply();", true);
                                return;
                            }
                            else
                            {
                                args = new ValidateArgs();
                                OnMenuMoveNextClick(args);
                                RefreshMenuStatus();

                                //Reset Record List untuk PageList
                                Session.Remove(SessionNameForList);
                            }
                        }
                        else
                        {
                            RefreshMenuStatus();

                            //Reset Record List untuk PageList
                            Session.Remove(SessionNameForList);

                            args.IsCancel = false;
                        }
                    }
                    break;
                case "cancel":
                    var oldVal = DataModeCurrent;
                    DataModeCurrent = AppEnum.DataMode.Read;
                    if (oldVal == AppEnum.DataMode.New)
                    {
                        if (IsSingleRecordMode)
                        {
                            //Close
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "close", "Close()();", true);
                            return;
                        }
                        else
                        {
                            OnMenuMoveNextClick(args);
                            RefreshMenuStatus();
                        }
                    }
                    else
                    {
                        OnPopulateEntryControl();
                        RefreshMenuStatus();
                    }
                    break;
                case "next":
                    OnMenuMoveNextClick(args);
                    RefreshMenuStatus();
                    break;
                case "prev":
                    OnMenuMovePrevClick(args);
                    RefreshMenuStatus();
                    break;
                case "auditlog":
                    var query = new AuditLogQuery();
                    var auditLogFilter = new AuditLogFilter();
                    OnMenuAuditLogClick(auditLogFilter);
                    query.Where(query.TableName == auditLogFilter.TableName &
                                query.PrimaryKeyData == auditLogFilter.PrimaryKeyData);
                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    Session[string.Format("_que.{0}", AppConstant.Program.AuditLogView)] = query;
                    Session.Remove(string.Format("_coll.{0}", AppConstant.Program.AuditLogView));
                    RedirectFromContent("~/Module/ControlPanel/AuditLog/AuditLogView/AuditLogList.aspx");
                    break;
                case "approval":
                    OnMenuApprovalClick(args);
                    if (!args.IsCancel)
                    {
                        IsRecordHasChanged = true;

                        OnPopulateEntryControl();
                        RefreshMenuStatus();
                    }
                    break;
                case "unapproval":
                    if (IsUsingBeforeUnapprovalValidation)
                    {
                        var str = (HiddenField)Helper.FindControlRecursive(Master, "hdnLastReason");
                        args.ReasonText = str.Value;

                        OnMenuUnApprovalClick(args);
                        if (!args.IsCancel)
                        {
                            IsRecordHasChanged = true;

                            OnPopulateEntryControl();
                            RefreshMenuStatus();
                        }
                    }
                    else
                    {
                        OnMenuUnApprovalClick(args);
                        if (!args.IsCancel)
                        {
                            IsRecordHasChanged = true;

                            OnPopulateEntryControl();
                            RefreshMenuStatus();
                        }
                    }
                    break;
                case "void":
                    if (IsUsingBeforeVoidValidation)
                    {
                        var str = (HiddenField)Helper.FindControlRecursive(Master, "hdnLastReason");
                        args.ReasonText = str.Value;

                        OnMenuVoidClick(args);
                        if (!args.IsCancel)
                        {
                            IsRecordHasChanged = true;

                            OnPopulateEntryControl();
                            RefreshMenuStatus();
                        }
                    }
                    else
                    {
                        OnMenuVoidClick(args);
                        if (!args.IsCancel)
                        {
                            IsRecordHasChanged = true;

                            OnPopulateEntryControl();
                            RefreshMenuStatus();
                        }
                    }
                    break;
                case "unvoid":
                    OnMenuUnVoidClick(args);
                    if (!args.IsCancel)
                    {
                        IsRecordHasChanged = true;

                        OnPopulateEntryControl();
                        RefreshMenuStatus();
                    }
                    break;
                case "rejournal":
                    OnMenuRejournalClick(args);
                    if (!args.IsCancel)
                    {
                        IsRecordHasChanged = true;

                        OnPopulateEntryControl();
                        RefreshMenuStatus();
                    }
                    break;
                case "reload":
                    OnReloadLabClick();
                    break;
                default:
                    if (e.Item.Value.ToLower().Contains("rpt_"))
                    {
                        string programID = e.Item.Value.Substring(4);
                        var printJobParameters = new PrintJobParameterCollection();

                        //Populate printJobParameters
                        OnMenuPrintClick(args, ref programID, printJobParameters);
                        if (!args.IsCancel)
                        {
                            AppSession.PrintJobReportID = programID;
                            AppSession.PrintJobParameters = printJobParameters;
                            AppSession.PrintShowToolBarPrint = true;
                        }
                    }
                    else
                    {
                        OnToolBarMenuDataAdditionalButtonClick(args, e.Item.Value);
                    }
                    break;
            }

            if (args.IsCancel || args.MessageText != string.Empty)
                ShowInformationHeader(args.MessageText);

        }

        protected virtual void OnToolBarMenuDataAdditionalButtonClick(ValidateArgs args, string value)
        {

        }

        protected void RefreshMenuStatus()
        {
            // Overide default toolbar enable and visible 
            var info = (Panel)Helper.FindControlRecursive(Master, "fw_PanelStatus");
            if (DataModeCurrent == AppEnum.DataMode.Read)
            {
                var onGetStatusMenuApproval = (OnGetStatusMenuApproval() ?? false);
                var onGetStatusMenuEdit = OnGetStatusMenuEdit();
                var onGetStatusMenuVoid = OnGetStatusMenuVoid();

                // Approval & UnApproval
                ToolBarMenuApproval.Visible = ToolBar.ApprovalUnApprovalVisible && onGetStatusMenuApproval && UserAccess.IsProgramApprovalAble;
                ToolBarMenuUnApproval.Visible = ToolBar.ApprovalUnApprovalVisible && ToolBar.UnApprovalVisible && !ToolBarMenuApproval.Visible && UserAccess.IsProgramUnApprovalAble;
                ToolBarMenuData.Items[13].Visible = ToolBarMenuApproval.Visible || ToolBarMenuData.Items[13].Visible; //Separator

                if (ToolBarMenuApproval.Visible)
                {
                    bool isCustomApprovalAble = onGetStatusMenuApproval && onGetStatusMenuEdit && onGetStatusMenuVoid;
                    ToolBarMenuApproval.Enabled = UserAccess.IsProgramApprovalAble && UserAccess.IsApprovalAble && isCustomApprovalAble;
                }

                if (ToolBarMenuUnApproval.Visible)
                {
                    bool isCustomUnApprovalAble = !onGetStatusMenuApproval && onGetStatusMenuEdit; //&& !onGetStatusMenuVoid;
                    ToolBarMenuUnApproval.Enabled = UserAccess.IsProgramUnApprovalAble && UserAccess.IsUnApprovalAble && isCustomUnApprovalAble;
                }

                // Void & UnVoid
                ToolBarMenuVoid.Visible = ToolBar.VoidUnVoidVisible && onGetStatusMenuVoid && UserAccess.IsProgramVoidAble;
                ToolBarMenuUnVoid.Visible = ToolBar.VoidUnVoidVisible && !ToolBarMenuVoid.Visible && UserAccess.IsProgramUnVoidAble;
                ToolBarMenuData.Items[16].Visible = ToolBarMenuVoid.Visible || ToolBarMenuUnVoid.Visible; //Separator

                if (ToolBarMenuVoid.Visible)
                {
                    bool isCustomVoidAble = onGetStatusMenuVoid && onGetStatusMenuEdit && onGetStatusMenuApproval;
                    ToolBarMenuVoid.Enabled = UserAccess.IsProgramVoidAble && UserAccess.IsVoidAble && isCustomVoidAble;
                }
                if (ToolBarMenuUnVoid.Visible)
                {
                    bool isCustomUnVoidAble = !onGetStatusMenuVoid && onGetStatusMenuEdit && onGetStatusMenuApproval;
                    ToolBarMenuUnVoid.Enabled = UserAccess.IsProgramUnVoidAble && UserAccess.IsUnVoidAble && isCustomUnVoidAble;
                }

                //add new
                ToolBarMenuData.Items[0].Enabled = UserAccess.IsProgramAddAble && UserAccess.IsAddAble;


                //Edit
                if (UserAccess.IsProgramApprovalAble || UserAccess.IsProgramVoidAble)
                    ToolBarMenuData.Items[1].Enabled = onGetStatusMenuVoid && onGetStatusMenuApproval && onGetStatusMenuEdit && UserAccess.IsProgramEditAble && UserAccess.IsEditAble;  // Edit
                else
                    ToolBarMenuData.Items[1].Enabled = onGetStatusMenuEdit && UserAccess.IsProgramEditAble && UserAccess.IsEditAble;  // Edit

                ToolBarMenuData.Items[3].Enabled = OnGetStatusMenuDelete() && UserAccess.IsProgramDeleteAble && UserAccess.IsDeleteAble && onGetStatusMenuApproval;  // Delete

                // SEP
                ToolBarMenuData.Items[23].Visible = ToolBarMenuData.Items[24].Visible;

                if (UserAccess.IsProgramApprovalAble && !onGetStatusMenuApproval)
                {
                    //Approval Status Information
                    info.Visible = true;
                    ((RadBinaryImage)Helper.FindControlRecursive(Master, "fw_StampStatus")).ImageUrl = "~/Images/ApprovedStampRed.png";
                }
                else if (UserAccess.IsProgramVoidAble && !onGetStatusMenuVoid)
                {
                    //Approval Status Information
                    info.Visible = true;
                    ((RadBinaryImage)Helper.FindControlRecursive(Master, "fw_StampStatus")).ImageUrl = "~/Images/VoidStampBlue.png";
                }
                else
                    info.Visible = false;

                if (ProgramID == AppConstant.Program.BpjsSep || ProgramID == AppConstant.Program.InhealthSjp)
                    ToolBarMenuData.Items[24].Visible = OnGetStatusMenuEdit() && UserAccess.IsProgramEditAble && UserAccess.IsEditAble;
            }
            else
                info.Visible = false;

        }
        protected override void OnLoadComplete(EventArgs e)
        {
            // Save and Edit
            var tbi = (RadToolBarButton)ToolBarMenuData.FindItemByValue("saveandedit");
            if (tbi != null)
            {
                tbi.Visible = ToolBar.SaveAndEditVisible && ToolBar.SaveAndEditVisible && ToolBarMenuSave.Visible;
                tbi.Enabled = tbi.Visible && ToolBarMenuSave.Enabled;
            }

            // Autosave toggle
            tbi = (RadToolBarButton)ToolBarMenuData.FindItemByValue("autosave");
            if (tbi != null)
            {
                tbi.Visible = ToolBar.AutoSaveVisible && AutoSaveInterval > 0 && ToolBarMenuSave.Visible;
                tbi.Enabled = tbi.Visible && ToolBarMenuSave.Enabled;

                tbi.Checked = tbi.Enabled;
                ((HiddenField)Helper.FindControlRecursive(this.Master, "fw_hdnIsAutoSaveToggleOn")).Value = tbi.Checked ? "1" : "0";
            }

            base.OnLoadComplete(e);

        }

        protected virtual void OnMenuApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected virtual void OnMenuUnApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected virtual void OnMenuVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected virtual void OnMenuUnVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected virtual void OnMenuSaveNewClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected virtual void OnMenuSaveEditClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected virtual void OnMenuPrintClick(ValidateArgs args, ref string programID,
                                        PrintJobParameterCollection printJobParameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected virtual void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected virtual void OnMenuEditClick()
        {
        }

        protected virtual void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected virtual void OnMenuMovePrevClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected virtual void OnMenuMoveNextClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected virtual void OnMenuDeleteClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public virtual void OnMenuSaveAndEditClick(ValidateArgs args)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                OnMenuSaveNewClick(args);
                if (!args.IsCancel && string.IsNullOrWhiteSpace(args.MessageText))
                    args.MessageText = "New Record has saved";
            }
            else if (DataModeCurrent == AppEnum.DataMode.Edit)
            {
                OnMenuSaveEditClick(args);
                if (!args.IsCancel && string.IsNullOrWhiteSpace(args.MessageText))
                    args.MessageText = "Current Record has saved";
            }
        }
        protected virtual void OnBeforeMenuNewClick(ValidateArgs args)
        {

        }

        protected virtual void OnMenuRejournalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected virtual void OnMenuNewClick()
        {
        }

        protected virtual void OnPopulateEntryControl(params string[] parameters)
        {

        }

        protected virtual void OnPopulateEntryControl(esEntity entity)
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                if (!IsCustomReportList)
                {
                    // Isi Related Program type Report
                    PopulateReportRelated(ToolBarMenuPrint, ProgramID, ProgramReferenceID);
                }

                var qsCount = Page.Request.QueryString.Count;
                if (qsCount > 0)
                {
                    var modus = Page.Request.QueryString[0];
                    if (modus != "new" || modus != "edit" || modus != "view")
                    {
                        modus = Page.Request.QueryString["md"];
                        if (string.IsNullOrEmpty(modus))
                            modus = Page.Request.QueryString["mod"];
                    }
                    if (string.IsNullOrEmpty(modus))
                        modus = "view";

                    var parameters = new string[qsCount];

                    if (qsCount > 1)
                    {
                        for (var i = 1; i < qsCount; i++)
                        {
                            parameters[i - 1] = Page.Request.QueryString[i];
                        }
                    }

                    var args = new ValidateArgs();
                    switch (modus)
                    {
                        case "view":
                            OnPopulateEntryControl(parameters);
                            DataModeCurrent = AppEnum.DataMode.Read;
                            break;
                        case "edit":
                            OnPopulateEntryControl(parameters);
                            if (UserAccess.IsProgramEditAble && UserAccess.IsEditAble &&
                                ViewState["qStringProcessed"] == null)
                            {
                                ViewState["qStringProcessed"] = "yes";
                                if (!string.IsNullOrWhiteSpace(RegistrationNo) && IsMedicalRecordEntry)
                                    MedicalRecordEditableValidate(args, RegistrationNo);

                                if (!args.IsCancel)
                                {
                                    OnBeforeMenuEditClick(args);
                                    if (!args.IsCancel)
                                    {
                                        DataModeCurrent = AppEnum.DataMode.Edit;
                                        OnMenuEditClick();
                                    }
                                    else
                                    {
                                        ShowInformationHeader(args.MessageText);
                                        DataModeCurrent = AppEnum.DataMode.Read;
                                    }
                                }
                                else
                                {
                                    ShowInformationHeader(args.MessageText);
                                    DataModeCurrent = AppEnum.DataMode.Read;
                                }
                            }
                            else
                                DataModeCurrent = AppEnum.DataMode.Read;
                            break;
                        case "new":
                            if (UserAccess.IsProgramAddAble && UserAccess.IsAddAble && ViewState["qStringProcessed"] == null)
                            {
                                ViewState["qStringProcessed"] = "yes";

                                if (!string.IsNullOrWhiteSpace(RegistrationNo) && IsMedicalRecordEntry)
                                    MedicalRecordAddableValidate(args, RegistrationNo);

                                if (!args.IsCancel)
                                {
                                    OnBeforeMenuNewClick(args);
                                    if (!args.IsCancel)
                                    {
                                        DataModeCurrent = AppEnum.DataMode.New;
                                        OnMenuNewClick();
                                    }
                                    else
                                    {
                                        DataModeCurrent = AppEnum.DataMode.Read;
                                        ShowInformationHeader(args.MessageText);
                                    }
                                }
                                else
                                {
                                    DataModeCurrent = AppEnum.DataMode.Read;
                                    ShowInformationHeader(args.MessageText);
                                }

                            }
                            else
                                DataModeCurrent = AppEnum.DataMode.Read;
                            break;
                    }
                }
                else
                    DataModeCurrent = AppEnum.DataMode.Read;
                RefreshMenuStatus();

            }
        }

        protected void RedirectFromContent(string urlRedirect)
        {
            AjaxPanel.Redirect(urlRedirect);
        }

        #region Nested type: EntryControl


        protected override void OnError(EventArgs e)
        {
            var ex = Server.GetLastError();
            Logger.LogException(ex, Request.UserHostName, AppSession.UserLogin.UserName);

            if (Page.IsCallback)
            {
                string script = string.Format("document.location.href = '{0}');", "~/ErrorPage.aspx");
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "redirect", script, true);
            }
            else
            {
                Response.Redirect("~/ErrorPage.aspx");
            }
        }
        #endregion

        protected virtual void OnIntegrityDataCheck()
        {

        }

        protected virtual void OnReloadLabClick()
        {

        }

        public virtual string OnGetCloseScript()
        {
            return "Close();args.set_cancel(true);";
        }
        public virtual string OnGetAdditionalMenuScript()
        {
            return String.Empty;
        }
        public virtual string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Empty;
        }

        // Diganti ke hiddenfield karena ViewState tidak bisa diupdate melalui ajax (Handono 230129)
        //public bool IsRecordHasChanged
        //{
        //    get
        //    {
        //        if (ViewState["fw_irhcd"] == null)
        //            ViewState["fw_irhcd"] = false;
        //        return (bool)ViewState["fw_irhcd"];
        //    }
        //    set { ViewState["fw_irhcd"] = value; }
        //}

        public bool IsRecordHasChanged
        {
            get
            {
                var fw_hdnRecordHasChanged = (HiddenField)Helper.FindControlRecursive(Master, "fw_hdnRecordHasChanged");
                if (string.IsNullOrEmpty(fw_hdnRecordHasChanged.Value))
                    fw_hdnRecordHasChanged.Value = "0";
                return fw_hdnRecordHasChanged.Value.Equals("1");
            }
            set { ((HiddenField)Helper.FindControlRecursive(Master, "fw_hdnRecordHasChanged")).Value = value ? "1" : "0"; }
        }

    }
}