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
    public class BasePageDialogHistEntry : BasePage
    {
        private RadAjaxManager _ajaxManager;
        private RadAjaxPanel _ajaxPanel;
        private ContentPlaceHolder _contentPlaceHolder;
        private RadWindow _winPrintPreview;
        private RadSplitter _splitter;
        private List<EntryControl> _listControlForEntry;
        private RadToolBar _toolBarMenuData;
        private RadWindow _windowSearch;
        private HiddenField _hdnIsCancelForFirstNewRecord;
        private RadPane _paneEntry;
        private RadPane _paneList;

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

        protected virtual void OnMenuDeleteClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
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

        public virtual string OnGetScriptToolBarAdditional()
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
        public virtual bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public virtual bool OnGetStatusMenuDelete()
        {
            return OnGetStatusMenuEdit();
        }

        public virtual bool? OnGetStatusMenuApproval()
        {
            return true;
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
        public bool IsSingleRecordMode
        {
            // diset public krn diakses dari masterpage
            get
            {
                if (ViewState["fw_SRM"] == null)
                    ViewState["fw_SRM"] = false;
                return (bool)ViewState["fw_SRM"];
            }
            set { ViewState["fw_SRM"] = value; }
        }
        public AppEnum.DataMode DataModeCurrent
        {
            get
            {
                if (ViewState["fw_DataMode"] == null)
                    ViewState["fw_DataMode"] = AppEnum.DataMode.Read;
                return (AppEnum.DataMode)ViewState["fw_DataMode"];
            }
            set
            {
                if (ViewState["fw_DataMode"] == null)
                    ViewState["fw_DataMode"] = AppEnum.DataMode.Read;
                else
                    if (IsPostBack && ViewState["fw_DataMode"].Equals(value)) return;

                var oldVal = (AppEnum.DataMode)ViewState["fw_DataMode"];

                ViewState["fw_DataMode"] = value;

                RefreshMenuStatus();

                SetReadOnlyStatus(value);

                OnDataModeChanged(oldVal, value);

            }
        }


        #endregion

        #region toolbar button
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
        public RadToolBarButton ToolBarMenuSave
        {
            get
            {
                return (RadToolBarButton)ToolBarMenuData.Items[4];
            }
        }
        public RadToolBarDropDown ToolBarMenuPrint
        {
            get
            {
                return (RadToolBarDropDown)ToolBarMenuData.Items[13];
            }
        }
        public RadToolBarItem ToolBarMenuVoid
        {
            get
            {
                return ToolBarMenuData.Items[10];
            }
        }

        #endregion

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
            get { return _hdnIsCancelForFirstNewRecord ?? (_hdnIsCancelForFirstNewRecord = (HiddenField)Helper.FindControlRecursive(this.Master, "hdnIsCancelForFirstNewRecord")); }
        }

        protected RadPane PaneEntry
        {
            get { return _paneEntry ?? (_paneEntry = (RadPane)Helper.FindControlRecursive(this.Master, "paneEntry")); }
        }
        protected RadPane PaneList
        {
            get { return _paneList ?? (_paneList = (RadPane)Helper.FindControlRecursive(this.Master, "paneList")); }
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
                return _contentPlaceHolder ?? (_contentPlaceHolder = (ContentPlaceHolder)Helper.FindControlRecursive(this, "cphEntry"));
            }
        }
        protected RadWindow WinPrintPreview
        {
            get
            {
                return _winPrintPreview ?? (_winPrintPreview = (RadWindow)Helper.FindControlRecursive(this, "fw_WinPrint"));
            }
        }

        protected RadSplitter Splitter
        {
            get
            {
                return _splitter ?? (_splitter = (RadSplitter)Helper.FindControlRecursive(this.Master, "fw_Splitter"));
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
                    ToolBarMenuAdd.Enabled = UserAccess.IsProgramAddAble && UserAccess.IsAddAble; // New
                    ToolBarMenuEdit.Enabled = UserAccess.IsProgramEditAble && UserAccess.IsEditAble; // Edit
                    ToolBarMenuDelete.Enabled = UserAccess.IsProgramDeleteAble && UserAccess.IsDeleteAble; //Delete
                }

            }

            _listControlForEntry = new List<EntryControl>();
            PopulateListControlForEntry(ContentPlaceHolderMain);

            ToolBarMenuData.ButtonClick += ToolBarMenuData_ButtonClick;
            OnInitializeAjaxManager(AjaxManager);
            AjaxManager.AjaxSettings.Clear();
            OnInitializeAjaxManagerSettingsCollection(AjaxManager.AjaxSettings);
        }

        private void PopulateListControlForEntry(Control root)
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
                            _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                        break;
                    case "RadEditor":
                        var edCtl = (RadEditor)ctl;
                        if (edCtl.Enabled)
                            _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                        break;
                    case "RadTextBox":
                    case "RadMaskedTextBox":
                    case "RadNumericTextBox":
                        // Jika waktu designer diset bisa dientry maka dianggap ctl tsb untuk entry
                        var inputCtl = (RadInputControl)ctl;
                        if (!inputCtl.ReadOnly && inputCtl.Enabled)
                            _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                        break;
                    case "RadComboBox":
                        var cbo = (RadComboBox)ctl;
                        if (cbo.Enabled)
                        {
                            cbo.EmptyMessage = "Select...";
                            _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                        }
                        break;
                    case "RadDropDownList":
                        var ddl = (RadDropDownList)ctl;
                        if (ddl.Enabled)
                            _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                        break;
                    case "RadTimePicker":
                        var time = (RadTimePicker)ctl;
                        if (time.Enabled)
                            _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                        break;
                    case "RadDateTimePicker":
                        var dttime = (RadDateTimePicker)ctl;
                        if (dttime.Enabled)
                            _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                        break;
                    case "CheckBox":
                        var chk = (CheckBox)ctl;
                        if (chk.Enabled)
                            _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                        break;
                    case "RadColorPicker":
                        var clrPicker = (RadColorPicker)ctl;
                        if (clrPicker.Enabled)
                            _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                        break;
                    case "RadioButtonList":
                        var rbl = (RadioButtonList)ctl;
                        if (rbl.Enabled)
                            _listControlForEntry.Add(new EntryControl(ctl.ID, ctl.GetType().Name));
                        break;
                    default:
                        if (ctl.HasControls())
                            PopulateListControlForEntry(ctl);
                        break;
                }
            }
        }

        private void SetReadOnlyStatus(AppEnum.DataMode datamode)
        {
            foreach (var ctlForEntry in _listControlForEntry)
            {
                var ctl = Helper.FindControlRecursive(ContentPlaceHolderMain, ctlForEntry.ControlID);
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
                            var edit = (RadEditor)ctl;
                            edit.Enabled = (datamode != AppEnum.DataMode.Read);
                            break;
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
                        case "RadDropDownList":
                            ((RadDropDownList)ctl).Enabled = (datamode != AppEnum.DataMode.Read);
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
                    }
            }
        }

        private void ShowInformationHeader(string messageText)
        {
            var pnlInfo = (Panel)Helper.FindControlRecursive(Master, "fw_PanelInfo");
            var lblInfo = (Label)Helper.FindControlRecursive(Master, "fw_LabelInfo");
            lblInfo.Text = messageText;
            pnlInfo.Visible = true;
        }

        private void HideInformationHeader()
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
                    OnBeforeMenuNewClick(args);
                    if (!args.IsCancel)
                    {
                        var info = (Panel)Helper.FindControlRecursive(Master, "fw_PanelStatus");
                        info.Visible = false;
                        DataModeCurrent = AppEnum.DataMode.New;
                        OnMenuNewClick();
                    }
                    break;
                case "edit":
                    OnBeforeMenuEditClick(args);
                    if (!args.IsCancel)
                    {
                        var info = (Panel)Helper.FindControlRecursive(Master, "fw_PanelStatus");
                        info.Visible = false;
                        DataModeCurrent = AppEnum.DataMode.Edit;
                        OnMenuEditClick();
                    }
                    break;
                case "save":
                    if (Page.IsValid)
                    {
                        if (DataModeCurrent == AppEnum.DataMode.Edit)
                            OnMenuSaveEditClick(args);
                        else
                            OnMenuSaveNewClick(args);

                        if (!args.IsCancel) // Save success
                        {
                            IsRecordHasChanged = true;
                            if (IsSingleRecordMode)
                            {
                                //Close
                                Helper.RegisterStartupScript(this, "closeMe", "CloseAndApply();");
                                return;
                            }

                            hdnIsCancelForFirstNewRecord.Value = "false"; //set status for popup window close condition

                            DataModeCurrent = AppEnum.DataMode.Read;
                            OnPopulateEntryControl(args);
                            RefreshMenuStatus();
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
                            args = new ValidateArgs();
                            OnPopulateEntryControl(args);
                            RefreshMenuStatus();

                            //Reset Record List untuk PageList
                            Session.Remove(SessionNameForList);
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
                    if (DataModeCurrent == AppEnum.DataMode.Edit)
                        OnMenuCancelEditClick(args);
                    else
                        OnMenuCancelNewClick(args);

                    DataModeCurrent = AppEnum.DataMode.Read;
                    OnPopulateEntryControl(args);
                    RefreshMenuStatus();
                    break;
                case "approval":
                    OnMenuApprovalClick(args);
                    if (!args.IsCancel)
                    {
                        OnPopulateEntryControl(args);
                        RefreshMenuStatus();
                    }
                    break;
                case "unapproval":
                    OnMenuUnApprovalClick(args);
                    if (!args.IsCancel)
                    {
                        OnPopulateEntryControl(args);
                        RefreshMenuStatus();
                    }
                    break;
                case "void":
                    OnMenuVoidClick(args);
                    if (!args.IsCancel)
                    {
                        OnPopulateEntryControl(args);
                        RefreshMenuStatus();
                    }
                    break;
                case "unvoid":
                    OnMenuUnVoidClick(args);
                    if (!args.IsCancel)
                    {
                        OnPopulateEntryControl(args);
                        RefreshMenuStatus();
                    }
                    break;
                case "rejournal":
                    OnMenuRejournalClick(args);
                    if (!args.IsCancel)
                    {
                        OnPopulateEntryControl(args);
                        RefreshMenuStatus();
                    }
                    break;
                default:
                    if (e.Item.Value.ToLower().Contains("rpt_"))
                    {
                        string programID = e.Item.Value.Substring(4);
                        var printJobParameters = new PrintJobParameterCollection();

                        //Populate printJobParameters
                        AppSession.PrintJobReportID = programID;
                        OnMenuPrintClick(args, programID, printJobParameters);

                        if (!args.IsCancel)
                        {
                            AppSession.PrintJobParameters = printJobParameters;
                            AppSession.PrintShowToolBarPrint = true;

                            Helper.RegisterStartupScript(this.Page, "rpt", "showPreview()");
                            //ShowPrintPreview();
                        }
                    }
                    break;
            }

            if (args.IsCancel || args.MessageText != string.Empty)
            {
                ShowInformationHeader(args.MessageText);
            }

        }
        protected void ShowPrintPreview()
        {
            var url = Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx");
            Helper.ShowRadWindowAfterPostback(WinPrintPreview, url, "preview", true);
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
                if (UserAccess.IsProgramApprovalAble || UserAccess.IsProgramVoidAble)
                    ToolBarMenuEdit.Enabled = isModusRead && ToolBar.EditEnabled && UserAccess.IsProgramEditAble &&
                                                       UserAccess.IsEditAble && OnGetStatusMenuVoid() && StatusMenuApproval &&
                                                       OnGetStatusMenuEdit(); // Edit
                else
                    ToolBarMenuEdit.Enabled = isModusRead && ToolBar.EditEnabled &&
                                                       UserAccess.IsProgramEditAble && UserAccess.IsEditAble && OnGetStatusMenuEdit(); // Edit
            }

            // Delete
            ToolBarMenuDelete.Visible = ToolBar.DeleteVisible && isModusRead; // Delete
            if (ToolBarMenuDelete.Visible)
                ToolBarMenuDelete.Enabled = isModusRead && ToolBar.DeleteEnabled && UserAccess.IsProgramDeleteAble && UserAccess.IsDeleteAble && OnGetStatusMenuDelete();  // Delete
            ToolBarMenuData.Items[2].Visible = ToolBarMenuDelete.Visible; //Sep

            ToolBarMenuSave.Visible = !isModusRead; // Save
            ToolBarMenuData.Items[5].Visible = !isModusRead; // Cancel


            ToolBarMenuPrint.Visible = ToolBar.PrintVisible && isModusRead;
            ToolBarMenuPrint.Enabled = ToolBar.PrintEnabled && ToolBarMenuPrint.Buttons.Count > 0; //Print
            ToolBarMenuData.Items[12].Visible = ToolBarMenuPrint.Visible; // Sep

            // Close
            ToolBarMenuData.Items[14].Visible = isModusRead; //Sep
            ToolBarMenuData.Items[15].Visible = isModusRead; //Close


            var infoStamp = (Panel)Helper.FindControlRecursive(Master, "fw_PanelStatus");

            // Approval & UnApproval
            ToolBarMenuData.Items[7].Visible = isModusRead && ToolBar.ApprovalUnApprovalVisible && StatusMenuApproval && UserAccess.IsProgramApprovalAble; //Aproval
            ToolBarMenuData.Items[8].Visible = isModusRead && ToolBar.ApprovalUnApprovalVisible && !ToolBarMenuData.Items[7].Visible && UserAccess.IsProgramUnApprovalAble; //UnApproval
            ToolBarMenuData.Items[6].Visible = ToolBarMenuVoid.Visible || ToolBarMenuData.Items[8].Visible; //Separator

            if (ToolBarMenuData.Items[7].Visible) // Approval
            {
                ToolBarMenuData.Items[7].Enabled = ToolBar.ApprovalEnabled && UserAccess.IsProgramApprovalAble && UserAccess.IsApprovalAble && StatusMenuApproval && OnGetStatusMenuEdit() && OnGetStatusMenuVoid();
            }

            if (ToolBarMenuData.Items[8].Visible) // UnApproval
            {
                ToolBarMenuData.Items[8].Enabled = ToolBar.UnApprovalEnabled && UserAccess.IsProgramUnApprovalAble && UserAccess.IsUnApprovalAble && !StatusMenuApproval && OnGetStatusMenuEdit();
            }

            // Void & UnVoid
            ToolBarMenuVoid.Visible = isModusRead && ToolBar.VoidUnVoidVisible && OnGetStatusMenuVoid() && UserAccess.IsProgramVoidAble; //Void
            ToolBarMenuData.Items[11].Visible = isModusRead && ToolBar.VoidUnVoidVisible && !ToolBarMenuVoid.Visible && UserAccess.IsProgramUnVoidAble; //UnVoid
            ToolBarMenuData.Items[9].Visible = ToolBarMenuVoid.Visible || ToolBarMenuData.Items[11].Visible; //Separator

            if (ToolBarMenuVoid.Visible) //Void
            {
                ToolBarMenuVoid.Enabled = ToolBar.VoidEnabled && UserAccess.IsProgramVoidAble && UserAccess.IsVoidAble && OnGetStatusMenuVoid() && OnGetStatusMenuEdit() && StatusMenuApproval;
            }
            if (ToolBarMenuData.Items[11].Visible) //UnVoid
            {
                ToolBarMenuData.Items[11].Enabled = ToolBar.UnVoidEnabled && UserAccess.IsProgramUnVoidAble && UserAccess.IsUnVoidAble && !OnGetStatusMenuVoid() && OnGetStatusMenuEdit() && StatusMenuApproval;
            }


            if (isModusRead)
            {
                if (UserAccess.IsProgramApprovalAble && !StatusMenuApproval)
                {
                    //Approval Status Information
                    infoStamp.Visible = true;
                    ((RadBinaryImage)Helper.FindControlRecursive(Master, "fw_StampStatus")).ImageUrl =
                        "~/Images/ApprovedStampRed.png";
                }
                else if (UserAccess.IsProgramVoidAble && !OnGetStatusMenuVoid())
                {
                    //Approval Status Information
                    infoStamp.Visible = true;
                    ((RadBinaryImage)Helper.FindControlRecursive(Master, "fw_StampStatus")).ImageUrl =
                        "~/Images/VoidStampBlue.png";
                }
                else
                    infoStamp.Visible = false;
            }
            else
                infoStamp.Visible = false;

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

                    var modus = Page.Request.QueryString["md"];
                    if (string.IsNullOrWhiteSpace(modus))
                        modus = Page.Request.QueryString["mod"];

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
                            OnPopulateEntryControl(args);
                            DataModeCurrent = AppEnum.DataMode.Read;
                            break;
                        case "edit":
                            OnPopulateEntryControl(args);
                            if (UserAccess.IsProgramEditAble && UserAccess.IsEditAble &&
                                ViewState["qStringProcessed"] == null)
                            {
                                ViewState["qStringProcessed"] = "yes";
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
                                DataModeCurrent = AppEnum.DataMode.Read;
                            break;
                        case "new":
                            if (UserAccess.IsProgramAddAble && UserAccess.IsAddAble && ViewState["qStringProcessed"] == null)
                            {
                                hdnIsCancelForFirstNewRecord.Value = "true";
                                ViewState["qStringProcessed"] = "yes";
                                OnBeforeMenuNewClick(args);
                                if (!args.IsCancel)
                                {
                                    DataModeCurrent = AppEnum.DataMode.New;
                                    OnMenuNewClick();
                                }
                                else
                                    ShowInformationHeader(args.MessageText);
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

        private class EntryControl
        {
            private readonly string _controlID;
            private readonly string _controlType;

            public EntryControl(string controlID, string controlType)
            {
                _controlType = controlType;
                _controlID = controlID;
            }

            public string ControlID
            {
                get { return _controlID; }
            }

            public string ControlType
            {
                get { return _controlType; }
            }
        }

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


        public virtual void OnServerValidate(object source, ServerValidateEventArgs args)
        {
        }

        public virtual string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Empty;
        }

        public bool IsRecordHasChanged
        {
            get
            {
                if (ViewState["fw_irhcd"] == null)
                    ViewState["fw_irhcd"] = false;
                return (bool)ViewState["fw_irhcd"];
            }
            set { ViewState["fw_irhcd"] = value; }
        }
    }

}