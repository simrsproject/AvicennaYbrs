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
    public class BasePageDialogEntry : BasePage
    {
        private RadAjaxManager _ajaxManager;
        private RadAjaxPanel _ajaxPanel;
        private ContentPlaceHolder _contentPlaceHolder;
        private List<EntryControl> _listControlForEntry;
        private RadToolBar _toolBarMenuData;
        private Panel _panelStatus;
        private RadWindow _windowSearch;
        private HiddenField _hdnIsCancelForFirstNewRecord;
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
        #region standard QueryString
        public virtual string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        public virtual string FromRegistrationNo
        {
            get
            {
                return Request.QueryString["fregno"];
            }
        }

        public virtual string ParamedicID
        {
            get
            {
                return Request.QueryString["parid"];
            }
        }

        private Registration _cureg;
        public Registration RegistrationCurrent
        {
            get
            {
                if (_cureg == null)
                {
                    _cureg = new Registration();
                    _cureg.LoadByPrimaryKey(RegistrationNo);
                }
                return _cureg;
            }
        }

        private string _patientID;
        public virtual string PatientID
        {
            get
            {
                // Jika ada RegistrationNo maka ambil dari data Reg jangan dari query string
                // Untuk mencegah kesalahan 
                if (!string.IsNullOrEmpty(RegistrationNo) && string.IsNullOrEmpty(_patientID))
                {
                    _patientID = RegistrationCurrent.PatientID;
                }
                else
                    _patientID = Request.QueryString["patid"];

                return _patientID;
            }
        }

        #endregion

        override protected string SignatureUrlPage
        {
            get
            {
                return "~/Login/PassCodeWithoutMenu.aspx";
            }
        }

        #region override method
        public virtual void OnServerValidate(ValidateArgs args)
        {
        }
        protected virtual void OnPopulateEntryControl(ValidateArgs args)
        {
        }
        protected virtual void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected virtual void OnMenuNewClick()
        {
        }
        protected virtual void OnMenuSaveNewClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected virtual void OnMenuSaveEditClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        protected virtual void OnMenuCancelNewClick(ValidateArgs args)
        {
        }

        protected virtual void OnMenuCancelEditClick(ValidateArgs args)
        {
        }
        protected virtual void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
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
            throw new Exception("OnMenuMoveNextClick is not implemented.");
        }

        protected virtual void OnMenuDeleteClick(ValidateArgs args)
        {
            throw new Exception("OnMenuDeleteClick is not implemented.");
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
        protected virtual void OnMenuRejournalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }
        public virtual string OnGetScriptToolBarSaveClicking()
        {
            return string.Empty;
        }
        public virtual bool OnGetStatusMenuAdd()
        {
            return true;
        }

        /// <summary>
        /// Untuk tambahan kondisi Enabled / Disabled menu Edit dan method ini dijalankan setelah OnPopulateEntryControl
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool OnGetStatusMenuEdit()
        {
            return true;
        }

        bool _statusMenuEdit;
        bool _onGetStatusMenuEditExecuted;
        private bool StatusMenuEdit
        {
            get
            {
                if (!_onGetStatusMenuEditExecuted)
                {
                    _onGetStatusMenuEditExecuted = true;
                    _statusMenuEdit = OnGetStatusMenuEdit();
                }
                return _statusMenuEdit;
            }
        }

        public virtual bool OnGetStatusMenuDelete()
        {
            return true;
        }
        bool _statusMenuDelete;
        bool _onGetStatusMenuDeleteExecuted;
        private bool StatusMenuDelete
        {
            get
            {
                if (!_onGetStatusMenuDeleteExecuted)
                {
                    _onGetStatusMenuDeleteExecuted = true;
                    _statusMenuDelete = OnGetStatusMenuDelete();
                }
                return _statusMenuDelete;
            }
        }

        public virtual bool? OnGetStatusMenuApproval()
        {
            return true;
        }

        bool? _statusMenuApprovalReal;
        bool _onGetStatusMenuApprovalExecuted;
        private bool StatusMenuApproval
        {
            get
            {
                if (!_onGetStatusMenuApprovalExecuted)
                {
                    _onGetStatusMenuApprovalExecuted = true;
                    _statusMenuApprovalReal = OnGetStatusMenuApproval();
                }
                return _statusMenuApprovalReal ?? false;
            }
        }
        public virtual bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected virtual void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected virtual void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Jika diset true
        /// maka form hanya untuk mode View, Edit dan Save
        /// dan untuk mode New harus diset pada query string md=new
        /// </summary>
        public bool IsSingleRecordMode
        {
            // diset public krn diakses dari masterpage
            get
            {
                if (ViewState["fw_srm"] == null)
                    ViewState["fw_srm"] = false;
                return (bool)ViewState["fw_srm"];
            }
            set
            {
                ViewState["fw_srm"] = value;
                ToolBar.NavigationVisible = false;
                ToolBar.AddVisible = false;
            }
        }

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
        public AppEnum.DataMode DataModeCurrent
        {
            get
            {
                //if (ViewState["fw_DataMode"] == null)
                //    ViewState["fw_DataMode"] = AppEnum.DataMode.Read;
                //return (AppEnum.DataMode)ViewState["fw_DataMode"];

                // Viewstate diganti ke HiddenField untuk keperluan autosave yg menggunaka ajax
                // sedangkan viewstate tidak bisa diupdate melalui ajax (Handono 230127)
                var fw_hdnDataMode = (HiddenField)Helper.FindControlRecursive(Master, "fw_hdnDataMode");
                if (string.IsNullOrWhiteSpace(fw_hdnDataMode.Value))
                    fw_hdnDataMode.Value = AppEnum.DataMode.Read.ToInt().ToString();
                return (AppEnum.DataMode)fw_hdnDataMode.Value.ToInt();
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
                var fw_hdnDataMode = (HiddenField)Helper.FindControlRecursive(Master, "fw_hdnDataMode");
                if (string.IsNullOrWhiteSpace(fw_hdnDataMode.Value))
                    fw_hdnDataMode.Value = AppEnum.DataMode.Read.ToInt().ToString();
                else
                    if (IsPostBack && fw_hdnDataMode.Value.Equals(value.ToInt().ToString())) return;

                var oldVal = (AppEnum.DataMode)fw_hdnDataMode.Value.ToInt();
                fw_hdnDataMode.Value = value.ToInt().ToString();

                //SetReadOnlyStatus(value);
                BasePageDetail.SetReadOnlyStatus(value, ContentPlaceHolderMain, _listControlForEntry);
                OnDataModeChanged(oldVal, value);
            }
        }


        #endregion

        #region toolbar button
        public RadToolBarButton ToolBarMenuAdd
        {
            get
            {
                return (RadToolBarButton)ToolBarMenuData.Items[0];
            }
        }

        public RadToolBarButton ToolBarMenuEdit
        {
            get
            {
                return (RadToolBarButton)ToolBarMenuData.Items[1];
            }
        }
        public RadToolBarButton ToolBarMenuDelete
        {
            get
            {
                return (RadToolBarButton)ToolBarMenuData.Items[3];
            }
        }
        public RadToolBarButton ToolBarMenuSave
        {
            get
            {
                return (RadToolBarButton)ToolBarMenuData.Items[4];
            }
        }

        public RadToolBarButton ToolBarMenuMovePrev
        {
            get
            {
                return (RadToolBarButton)ToolBarMenuData.Items[7];
            }
        }

        public RadToolBarButton ToolBarMenuMoveNext
        {
            get
            {
                return (RadToolBarButton)ToolBarMenuData.Items[8];
            }
        }

        public RadToolBarButton ToolBarMenuApproval
        {
            get
            {
                return (RadToolBarButton)ToolBarMenuData.Items[10];
            }
        }
        public RadToolBarButton ToolBarMenuUnApproval
        {
            get
            {
                return (RadToolBarButton)ToolBarMenuData.Items[11];
            }
        }
        public RadToolBarButton ToolBarMenuVoid
        {
            get
            {
                return (RadToolBarButton)ToolBarMenuData.Items[13];
            }
        }
        public RadToolBarButton ToolBarMenuUnVoid
        {
            get
            {
                return (RadToolBarButton)ToolBarMenuData.Items[14];
            }
        }
        public RadToolBarDropDown ToolBarMenuPrint
        {
            get
            {
                return (RadToolBarDropDown)ToolBarMenuData.Items[16];
            }
        }
        #endregion


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
            private bool _approvalUnApprovalVisible = true;
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

        protected HiddenField hdnIsCancelForFirstNewRecord
        {
            get { return _hdnIsCancelForFirstNewRecord ?? (_hdnIsCancelForFirstNewRecord = (HiddenField)Helper.FindControlRecursive(this.Master, "fw_hdnIsCancelForFirstNewRecord")); }
        }
        protected RadAjaxPanel AjaxPanel
        {
            get { return _ajaxPanel ?? (_ajaxPanel = (RadAjaxPanel)Helper.FindControlRecursive(this, "fw_ajxPanel")); }
        }

        protected RadToolBar ToolBarMenuData
        {
            get
            {
                return _toolBarMenuData ?? (_toolBarMenuData = (RadToolBar)Helper.FindControlRecursive(this.Master, "fw_tbarData"));
            }
        }

        protected Panel PanelStatus
        {
            get
            {
                return _panelStatus ?? (_panelStatus = (Panel)Helper.FindControlRecursive(Master, "fw_PanelStatus"));
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


        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            if (IsPostBack)
            {
                //User Access
                if (UserAccess.IsExist)
                {
                    ToolBarMenuData.Items[0].Enabled = UserAccess.IsProgramAddAble && UserAccess.IsAddAble; // New
                    ToolBarMenuData.Items[1].Enabled = UserAccess.IsProgramEditAble && UserAccess.IsEditAble; // Edit
                    ToolBarMenuData.Items[3].Enabled = UserAccess.IsProgramDeleteAble && UserAccess.IsDeleteAble; //Delete
                }
            }

            _listControlForEntry = new List<EntryControl>();
            BasePageDetail.PopulateListControlForEntry(ContentPlaceHolderMain, _listControlForEntry);

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

        //private void PopulateListControlForEntry(Control root)
        //{
        //    // Fungsi ini harus dipanggil saat init complete sehingga status 
        //    // control nya masih belum dirubah waktu runtime 
        //    // bila bisa dientry maka dianggap ctl tsb untuk entry
        //    // atau jika waktu designer diset bisa dientry maka dianggap ctl tsb untuk entry

        //    foreach (Control ctl in root.Controls)
        //    {
        //        switch (ctl.GetType().Name)
        //        {
        //            case "RadDatePicker":
        //                var dtCtl = (RadDatePicker)ctl;
        //                if (dtCtl.Enabled)
        //                    _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
        //                break;
        //            case "RadEditor":
        //                var edCtl = (RadEditor)ctl;
        //                if (edCtl.Enabled)
        //                    _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
        //                break;
        //            case "RadTextBox":
        //            case "RadMaskedTextBox":
        //            case "RadNumericTextBox":
        //                // Jika waktu designer diset bisa dientry maka dianggap ctl tsb untuk entry
        //                var inputCtl = (RadInputControl)ctl;
        //                if (!inputCtl.ReadOnly && inputCtl.Enabled)
        //                    _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
        //                break;
        //            case "RadComboBox":
        //                var cbo = (RadComboBox)ctl;
        //                if (cbo.Enabled)
        //                {
        //                    cbo.EmptyMessage = "Select...";
        //                    _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
        //                }
        //                break;
        //            case "RadDropDownList":
        //                var ddl = (RadDropDownList)ctl;
        //                if (ddl.Enabled)
        //                    _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
        //                break;
        //            case "RadTimePicker":
        //                var time = (RadTimePicker)ctl;
        //                if (time.Enabled)
        //                    _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
        //                break;
        //            case "CheckBox":
        //                var chk = (CheckBox)ctl;
        //                if (chk.Enabled)
        //                    _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
        //                break;
        //            case "RadColorPicker":
        //                var clrPicker = (RadColorPicker)ctl;
        //                if (clrPicker.Enabled)
        //                    _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
        //                break;
        //            case "RadioButtonList":
        //                var rbl = (RadioButtonList)ctl;
        //                if (rbl.Enabled)
        //                    _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
        //                break;
        //            case "RadRadioButtonList":
        //                var rrbl = (RadRadioButtonList)ctl;
        //                if (rrbl.Enabled)
        //                    _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
        //                break;
        //            case "RadDateTimePicker":
        //                var rdtp = (RadDateTimePicker)ctl;
        //                if (rdtp.Enabled)
        //                    _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
        //                break;
        //            case "RadCheckBoxList":
        //                var rcbl = (RadCheckBoxList)ctl;
        //                if (rcbl.Enabled)
        //                    _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
        //                break;
        //            default:
        //                if (ctl.HasControls())
        //                    PopulateListControlForEntry(ctl);
        //                break;
        //        }
        //    }
        //}

        //private void SetReadOnlyStatus(AppEnum.DataMode datamode)
        //{
        //    foreach (var ctlForEntry in _listControlForEntry)
        //    {
        //        var ctl = Helper.FindControlRecursive(ContentPlaceHolderMain, ctlForEntry.ControlID);
        //        if (ctl != null)
        //            switch (ctlForEntry.ControlType)
        //            {
        //                case "RadTextBox":
        //                    var txt = (RadTextBox)ctl;
        //                    txt.ReadOnly = (datamode == AppEnum.DataMode.Read);

        //                    if (txt.ClientEvents.OnButtonClick != string.Empty)
        //                        txt.ShowButton = datamode != AppEnum.DataMode.Read;
        //                    break;
        //                case "RadEditor":
        //                    var edit = (RadEditor)ctl;
        //                    edit.Enabled = (datamode != AppEnum.DataMode.Read);
        //                    break;
        //                case "RadNumericTextBox":
        //                    var txtNum = (RadNumericTextBox)ctl;
        //                    txtNum.ReadOnly = (datamode == AppEnum.DataMode.Read);
        //                    break;
        //                case "CheckBox":
        //                    ((CheckBox)ctl).Enabled = (datamode != AppEnum.DataMode.Read);
        //                    break;
        //                case "RadioButtonList":
        //                    ((RadioButtonList)ctl).Enabled = (datamode != AppEnum.DataMode.Read);
        //                    break;
        //                case "RadComboBox":
        //                    ((RadComboBox)ctl).Enabled = (datamode != AppEnum.DataMode.Read);
        //                    break;
        //                case "RadDropDownList":
        //                    ((RadDropDownList)ctl).Enabled = (datamode != AppEnum.DataMode.Read);
        //                    break;
        //                case "RadDatePicker":
        //                    ((RadDatePicker)ctl).Enabled = (datamode != AppEnum.DataMode.Read);
        //                    break;
        //                case "RadTimePicker":
        //                    ((RadTimePicker)ctl).Enabled = (datamode != AppEnum.DataMode.Read);
        //                    break;
        //                case "RadColorPicker":
        //                    ((RadColorPicker)ctl).Enabled = (datamode != AppEnum.DataMode.Read);
        //                    break;
        //                case "RadMaskedTextBox":
        //                    ((RadMaskedTextBox)ctl).ReadOnly = (datamode == AppEnum.DataMode.Read);
        //                    break;
        //            }
        //    }
        //}

        public void ShowInformationHeader(string messageText)
        {
            var pnlInfo = (Panel)Helper.FindControlRecursive(Master, "fw_PanelInfo");
            var lblInfo = (Label)Helper.FindControlRecursive(Master, "fw_LabelInfo");
            lblInfo.Text = messageText;
            pnlInfo.Visible = true;
            //ClientScript.RegisterStartupScript(this.Page.GetType(), "info", "alert(\"" + messageText.Replace("\n","<br />") + "\");", true);
            Helper.ShowMessageAfterPostback(this.Page, messageText);
        }

        public void HideInformationHeader()
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
            try
            {
                switch (e.Item.Value)
                {
                    case "home":
                        RedirectFromContent(UserAccess.NavigateUrl);
                        break;
                    case "new":
                        if (IsMedicalRecordEntry)
                            MedicalRecordAddableValidate(args, RegistrationCurrent);
                        if (!args.IsCancel)
                        {
                            OnBeforeMenuNewClick(args);
                            if (!args.IsCancel)
                            {
                                PanelStatus.Visible = false;
                                DataModeCurrent = AppEnum.DataMode.New;
                                OnMenuNewClick();
                                RefreshMenuStatus();

                            }
                        }
                        break;
                    case "edit":
                        if (IsMedicalRecordEntry)
                            MedicalRecordEditableValidate(args, RegistrationCurrent);
                        if (!args.IsCancel)
                        {
                            OnBeforeMenuEditClick(args);
                            if (!args.IsCancel)
                            {
                                PanelStatus.Visible = false;
                                DataModeCurrent = AppEnum.DataMode.Edit;
                                OnMenuEditClick();
                                RefreshMenuStatus();
                            }
                        }
                        break;
                    case "save":
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
                                if (IsSingleRecordMode)
                                {
                                    //Reset Record List untuk PageList
                                    Session.Remove(SessionNameForList);

                                    //Close
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "closeAndApply", "CloseAndApply();", true);
                                    return;
                                }

                                hdnIsCancelForFirstNewRecord.Value = "false"; //set status for popup window close condition

                                DataModeCurrent = AppEnum.DataMode.Read;
                                OnPopulateEntryControl(args);
                                RefreshMenuStatus();

                                //Reset Record List untuk PageList
                                Session.Remove(SessionNameForList);
                            }
                        }

                        break;
                    case "delete":
                        if (Page.IsValid)
                        {
                            if (IsMedicalRecordEntry)
                                MedicalRecordEditableValidate(args, RegistrationCurrent);

                            if (!args.IsCancel)
                            {
                                OnMenuDeleteClick(args);

                                if (!args.IsCancel)
                                {
                                    //Reset Record List untuk PageList
                                    Session.Remove(SessionNameForList);
                                    IsRecordHasChanged = true;

                                    if (IsSingleRecordMode)
                                    {
                                        //Close
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "closeAndApply", "CloseAndApply();", true);
                                        return;
                                    }

                                    args = new ValidateArgs();
                                    OnMenuMoveNextClick(args);
                                    RefreshMenuStatus();
                                }
                                else
                                {
                                    RefreshMenuStatus();

                                    //Reset Record List untuk PageList
                                    Session.Remove(SessionNameForList);

                                    args.IsCancel = false;
                                }
                            }
                        }
                        break;
                    case "cancel":
                        if (DataModeCurrent == AppEnum.DataMode.Edit)
                            OnMenuCancelEditClick(args);
                        else
                            OnMenuCancelNewClick(args);

                        DataModeCurrent = AppEnum.DataMode.Read;
                        OnPopulateEntryControl(args);
                        RefreshMenuStatus();
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
                        if (IsMedicalRecordEntry)
                            MedicalRecordEditableValidate(args, RegistrationCurrent);

                        if (!args.IsCancel)
                        {
                            OnMenuApprovalClick(args);
                            if (!args.IsCancel)
                            {
                                OnPopulateEntryControl(args);
                                RefreshMenuStatus();
                            }
                        }
                        break;
                    case "unapproval":
                        if (IsMedicalRecordEntry)
                            MedicalRecordEditableValidate(args, RegistrationCurrent);

                        if (!args.IsCancel)
                        {
                            OnMenuUnApprovalClick(args);
                            if (!args.IsCancel)
                            {
                                OnPopulateEntryControl(args);
                                RefreshMenuStatus();
                            }
                        }
                        break;
                    case "void":
                        if (IsMedicalRecordEntry)
                            MedicalRecordEditableValidate(args, RegistrationCurrent);

                        if (!args.IsCancel)
                        {
                            OnMenuVoidClick(args);
                            if (!args.IsCancel)
                            {
                                OnPopulateEntryControl(args);
                                RefreshMenuStatus();
                            }
                        }
                        break;
                    case "unvoid":
                        if (IsMedicalRecordEntry)
                            MedicalRecordEditableValidate(args, RegistrationCurrent);

                        if (!args.IsCancel)
                        {
                            OnMenuUnVoidClick(args);
                            if (!args.IsCancel)
                            {
                                OnPopulateEntryControl(args);
                                RefreshMenuStatus();
                            }
                        }
                        break;
                    case "rejournal":
                        if (IsMedicalRecordEntry)
                            MedicalRecordEditableValidate(args, RegistrationCurrent);

                        if (!args.IsCancel)
                        {
                            OnMenuRejournalClick(args);
                            if (!args.IsCancel)
                            {
                                OnPopulateEntryControl(args);
                                RefreshMenuStatus();
                            }
                        }
                        break;
                    default:
                        if (e.Item.Value.ToLower().Contains("rpt_"))
                        {
                            string programID = e.Item.Value.Substring(4);
                            var printJobParameters = new PrintJobParameterCollection();

                            //Populate printJobParameters
                            OnMenuPrintClick(args, programID, printJobParameters);
                            if (!args.IsCancel)
                            {
                                AppSession.PrintJobReportID = programID;
                                AppSession.PrintJobParameters = printJobParameters;
                                AppSession.PrintShowToolBarPrint = true;

                                Helper.RegisterStartupScript(this.Page, "rpt", "showPreview()");
                            }
                        }
                        else
                        {
                            OnToolBarMenuDataAdditionalButtonClick(args, e.Item.Value);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                args.IsCancel = true;
                args.MessageText = ex.Message;
            }

            if (args.IsCancel || args.MessageText != string.Empty)
            {
                ShowInformationHeader(args.MessageText);
            }

        }

        protected virtual void OnToolBarMenuDataAdditionalButtonClick(ValidateArgs args, string value)
        {
        }

        protected void RefreshMenuStatus()
        {
            bool isModusRead = (DataModeCurrent.Equals(AppEnum.DataMode.Read));

            ToolBarMenuAdd.Visible = ToolBar.AddVisible && UserAccess.IsMenuAddVisible && isModusRead;//Add
            if (ToolBarMenuAdd.Visible)
                ToolBarMenuAdd.Enabled = isModusRead && ToolBar.AddEnabled && OnGetStatusMenuAdd() && UserAccess.IsProgramAddAble &&
                                     UserAccess.IsAddAble; // Edit

            //Edit
            ToolBarMenuEdit.Visible = ToolBar.EditVisible && isModusRead; // Edit
            if (ToolBarMenuEdit.Visible)
            {
                //if (UserAccess.IsProgramApprovalAble || UserAccess.IsProgramVoidAble)
                //    ToolBarMenuEdit.Enabled = isModusRead && ToolBar.EditEnabled && OnGetStatusMenuVoid() && StatusMenuApproval &&
                //                                       StatusMenuEdit && UserAccess.IsProgramEditAble &&
                //                                       UserAccess.IsEditAble; // Edit
                //else
                ToolBarMenuEdit.Enabled = isModusRead && ToolBar.EditEnabled && StatusMenuEdit &&
                                                   UserAccess.IsProgramEditAble && UserAccess.IsEditAble; // Edit
            }

            // Delete
            ToolBarMenuDelete.Visible = ToolBar.DeleteVisible && isModusRead; // Delete
            if (ToolBarMenuDelete.Visible)
                ToolBarMenuDelete.Enabled = isModusRead && ToolBar.DeleteEnabled && UserAccess.IsProgramDeleteAble && UserAccess.IsDeleteAble && StatusMenuDelete;  // Delete
            ToolBarMenuData.Items[2].Visible = ToolBarMenuDelete.Visible; //Sep

            ToolBarMenuData.Items[4].Visible = !isModusRead; // Save
            ToolBarMenuData.Items[5].Visible = !isModusRead; // Cancel


            // Navigation
            ToolBarMenuData.Items[7].Visible = ToolBar.NavigationVisible && isModusRead; // Prev
            ToolBarMenuData.Items[8].Visible = ToolBar.NavigationVisible && isModusRead; // Next
            ToolBarMenuData.Items[6].Visible = ToolBar.NavigationVisible && isModusRead; // Sep


            ToolBarMenuPrint.Visible = ToolBar.PrintVisible && isModusRead;
            ToolBarMenuPrint.Enabled = ToolBar.PrintEnabled && ToolBarMenuPrint.Buttons.Count > 0; //Print
            ToolBarMenuData.Items[15].Visible = ToolBarMenuPrint.Visible; // Sep

            // Close
            ToolBarMenuData.Items[17].Visible = isModusRead; //Sep
            ToolBarMenuData.Items[18].Visible = isModusRead; //Close

            // Approval & UnApproval
            ToolBarMenuData.Items[10].Visible = isModusRead && ToolBar.ApprovalUnApprovalVisible && StatusMenuApproval && UserAccess.IsProgramApprovalAble; //Aproval
            ToolBarMenuData.Items[11].Visible = isModusRead && ToolBar.ApprovalUnApprovalVisible && !ToolBarMenuData.Items[10].Visible && UserAccess.IsProgramUnApprovalAble; //UnApproval
            ToolBarMenuData.Items[9].Visible = ToolBarMenuData.Items[10].Visible || ToolBarMenuData.Items[11].Visible; //Separator

            if (ToolBarMenuData.Items[10].Visible) // Approval
            {
                bool isCustomApprovalAble = ToolBar.ApprovalEnabled && StatusMenuApproval;
                ToolBarMenuData.Items[10].Enabled = UserAccess.IsProgramApprovalAble && UserAccess.IsApprovalAble && isCustomApprovalAble;
            }

            if (ToolBarMenuData.Items[11].Visible) // UnApproval
            {
                bool isCustomUnApprovalAble = ToolBar.UnApprovalEnabled && !StatusMenuApproval;
                ToolBarMenuData.Items[11].Enabled = UserAccess.IsProgramUnApprovalAble && UserAccess.IsUnApprovalAble && isCustomUnApprovalAble;
            }

            // Void & UnVoid
            ToolBarMenuVoid.Visible = isModusRead && ToolBar.VoidUnVoidVisible && OnGetStatusMenuVoid() && UserAccess.IsProgramVoidAble; //Void
            ToolBarMenuData.Items[14].Visible = isModusRead && ToolBar.VoidUnVoidVisible && !ToolBarMenuVoid.Visible && UserAccess.IsProgramUnVoidAble; //UnVoid
            ToolBarMenuData.Items[12].Visible = ToolBarMenuVoid.Visible || ToolBarMenuData.Items[14].Visible; //Separator

            if (ToolBarMenuVoid.Visible) //Void
            {
                bool isCustomVoidAble = ToolBar.VoidEnabled && OnGetStatusMenuVoid() && StatusMenuEdit && StatusMenuApproval;
                ToolBarMenuVoid.Enabled = UserAccess.IsProgramVoidAble && UserAccess.IsVoidAble && isCustomVoidAble;
            }
            if (ToolBarMenuData.Items[14].Visible) //UnVoid
            {
                bool isCustomUnVoidAble = ToolBar.UnVoidEnabled && !OnGetStatusMenuVoid() && StatusMenuEdit && StatusMenuApproval;
                ToolBarMenuData.Items[14].Enabled = UserAccess.IsProgramUnVoidAble && UserAccess.IsUnVoidAble && isCustomUnVoidAble;
            }


            if (isModusRead)
            {
                if (ToolBar.ApprovalUnApprovalVisible && UserAccess.IsProgramApprovalAble && !StatusMenuApproval)
                {
                    //Approval Status Information
                    PanelStatus.Visible = true;
                    ((RadBinaryImage)Helper.FindControlRecursive(Master, "fw_StampStatus")).ImageUrl =
                        "~/Images/ApprovedStampRed.png";
                }
                else if (ToolBar.VoidUnVoidVisible && UserAccess.IsProgramVoidAble && !OnGetStatusMenuVoid())
                {
                    //Approval Status Information
                    PanelStatus.Visible = true;
                    ((RadBinaryImage)Helper.FindControlRecursive(Master, "fw_StampStatus")).ImageUrl =
                        "~/Images/VoidStampBlue.png";
                }
                else
                    PanelStatus.Visible = false;
            }
            else
                PanelStatus.Visible = false;

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
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                if (!IsCustomReportList)
                {
                    PopulateReportRelated(ToolBarMenuPrint, ProgramID, ProgramReferenceID);
                }

                var qsCount = Page.Request.QueryString.Count;
                if (qsCount > 0)
                {
                    var modus = Page.Request.QueryString["md"];
                    if (string.IsNullOrWhiteSpace(modus))
                        modus = Page.Request.QueryString["mod"];

                    if (string.IsNullOrWhiteSpace(modus))
                        modus = "view";

                    hdnIsCancelForFirstNewRecord.Value = "false"; //set status for close condition

                    var args = new ValidateArgs();
                    switch (modus)
                    {
                        case "view":
                            DataModeCurrent = AppEnum.DataMode.Read;
                            OnPopulateEntryControl(args);
                            RefreshMenuStatus();
                            break;
                        case "edit":
                            ForceToEditMode(args);
                            break;
                        case "new":
                            ForceToNewMode(args);
                            break;
                    }
                }
                else
                {
                    DataModeCurrent = AppEnum.DataMode.Read;
                    RefreshMenuStatus();
                }
            }
        }

        protected void ForceToNewMode(ValidateArgs args)
        {
            if (UserAccess.IsProgramAddAble && UserAccess.IsAddAble && ViewState["qStringProcessed"] == null)
            {
                ViewState["qStringProcessed"] = "yes";
                if (IsMedicalRecordEntry)
                    MedicalRecordAddableValidate(args, RegistrationCurrent);

                if (!args.IsCancel)
                {
                    OnBeforeMenuNewClick(args);
                    if (!args.IsCancel)
                    {
                        hdnIsCancelForFirstNewRecord.Value = "true"; //set status for close condition

                        DataModeCurrent = AppEnum.DataMode.New;
                        OnMenuNewClick();
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

            RefreshMenuStatus();
        }

        protected void ForceToEditMode(ValidateArgs args)
        {
            if (UserAccess.IsProgramEditAble && UserAccess.IsEditAble &&
                ViewState["qStringProcessed"] == null)
            {
                ViewState["qStringProcessed"] = "yes";
                DataModeCurrent = AppEnum.DataMode.Edit;
                OnPopulateEntryControl(args);

                if (!string.IsNullOrWhiteSpace(RegistrationNo) && IsMedicalRecordEntry)
                    MedicalRecordAddableValidate(args, RegistrationCurrent);

                if (!args.IsCancel)
                {
                    OnBeforeMenuEditClick(args);
                    if (!args.IsCancel)
                    {
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
            {
                DataModeCurrent = AppEnum.DataMode.Read;
                OnPopulateEntryControl(args);
            }

            RefreshMenuStatus();
        }

        protected void RedirectFromContent(string urlRedirect)
        {
            AjaxPanel.Redirect(urlRedirect);
        }

        #region Nested type: EntryControl

        //private class EntryControl
        //{
        //    private readonly string _controlID;
        //    private readonly string _controlType;

        //    public EntryControl(string controlID, string controlType)
        //    {
        //        _controlType = controlType;
        //        _controlID = controlID;
        //    }

        //    public string ControlID
        //    {
        //        get { return _controlID; }
        //    }

        //    public string ControlType
        //    {
        //        get { return _controlType; }
        //    }
        //}

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

        public virtual string OnGetCloseScript()
        {
            return "Close();args.set_cancel(true);";
        }

        public virtual string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Empty;
        }

        public virtual string OnGetAdditionalMenuScript()
        {
            return String.Empty;
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