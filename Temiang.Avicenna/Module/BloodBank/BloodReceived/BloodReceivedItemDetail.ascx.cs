using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.BloodBank
{
    public partial class BloodReceivedItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadComboBox CboSrBloodType
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboSRBloodType"); }
        }

        private RadioButtonList RblBloodRhesus
        {
            get
            { return (RadioButtonList)Helper.FindControlRecursive(Page, "rblBloodRhesus"); }
        }

        private RadComboBox CboSrBloodGroupRequest
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboSRBloodGroupRequest"); }
        }

        private RadTextBox TxtServiceUnitID
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtServiceUnitID"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            bool isBypass = false;
            var bg = new AppStandardReferenceItem();
            if (bg.LoadByPrimaryKey(AppEnum.StandardReference.BloodGroup.ToString(), CboSrBloodGroupRequest.SelectedValue))
            {
                isBypass = bg.CustomField == "0";
            }
            hdnIsBypassBloodCrossMatching.Value = (AppSession.Parameter.IsBypassBloodCrossMatching || isBypass) ? "1" : "0";

            CboSrBloodType.Enabled = false;
            RblBloodRhesus.Enabled = false;

            StandardReference.InitializeIncludeSpace(cboSRBloodGroupReceived, AppEnum.StandardReference.BloodGroup);

            if (hdnIsBypassBloodCrossMatching.Value == "0")
            {
                rblIsCrossMatchingSuitability.Enabled = false;
                rblCrossmatchCompatibleMajor.Enabled = false;
                rblCrossmatchCompatibleMinor.Enabled = false;
                txtCrossmatchCompatibleMinorLevel.Enabled = false;
                rblCrossmatchCompatibleAutoControl.Enabled = false;
                txtCrossmatchCompatibleAutoControlLevel.Enabled = false;
                rblCrossmatchCompatibleDctControl.Enabled = false;
                txtCrossmatchCompatibleDctControlLevel.Enabled = false;
                chkIsScreening1.Enabled = false;
                chkIsScreening2.Enabled = false;
                chkIsScreening3.Enabled = false;
            }
            else
            {
                rfvIsCrossMatchingSuitability.Visible = false;
            }

            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsBloodDistributionReceivedByCombobox))
            {
                pnlTxt.Visible = false;
                pnlCbo.Visible = true;
            }
            else
            {
                pnlTxt.Visible = true;
                pnlCbo.Visible = false;
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                txtReceivedDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtReceivedTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

                var au = new AppUserQuery();
                au.Where(au.UserID == AppSession.UserLogin.UserID);
                cboExaminerByUserID.DataSource = au.LoadDataTable();
                cboExaminerByUserID.DataBind();
                cboExaminerByUserID.SelectedValue = AppSession.UserLogin.UserID;

                return;
            }
            ViewState["IsNewRecord"] = false;

            cboBagNo.Enabled = false;
            cboSRBloodSource.Enabled = false;
            cboSRBloodSourceFrom.Enabled = false;

            object transfusionDate = DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.TransfusionStartDateTime);
            if (transfusionDate != null)
            {
                txtReceivedDate.Enabled = false;
                txtReceivedTime.Enabled = false;
                cboSRBloodGroupReceived.Enabled = false;
                rblIsScreeningLabelPassedPmi.Enabled = false;
                rblIsExpiredDate.Enabled = false;
                rblIsLeak.Enabled = false;
                rblIsHemolysis.Enabled = false;
                rblIsCrossMatchingSuitability.Enabled = false;
                rblCrossmatchCompatibleMajor.Enabled = false;
                rblCrossmatchCompatibleMinor.Enabled = false;
                txtCrossmatchCompatibleMinorLevel.Enabled = false;
                rblCrossmatchCompatibleAutoControl.Enabled = false;
                txtCrossmatchCompatibleAutoControlLevel.Enabled = false;
                rblCrossmatchCompatibleDctControl.Enabled = false;
                txtCrossmatchCompatibleDctControlLevel.Enabled = false;
                rblIsClotting.Enabled = false;
                rblIsBloodTypeCompatibility.Enabled = false;
                rblIsLabelDonorsMatchesWithPatientsForm.Enabled = false;
                rblIsScreeningLabelPassedBd.Enabled = false;
                cboExaminerByUserID.Enabled = false;
                txtUnitOfficer.ReadOnly = true;
                chkIsScreening1.Enabled = false;
                chkIsScreening2.Enabled = false;
                chkIsScreening3.Enabled = false;
            }

            StandardReference.InitializeIncludeSpace(cboSRBloodSource, AppEnum.StandardReference.BloodSource);
            StandardReference.InitializeIncludeSpace(cboSRBloodSourceFrom, AppEnum.StandardReference.BloodSourceFrom);

            PopulateCboBagNo(cboBagNo, (String)DataBinder.Eval(DataItem, "BagNo"), false);
            cboBagNo.Text = (String)DataBinder.Eval(DataItem, "BagNo");
            cboBagNo.SelectedValue = (String)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.BagNo);
            try
            {
                txtReceivedDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.ReceivedDate);
            }
            catch (Exception exception)
            {
                txtReceivedDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            }

            var receivedTime = (String)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.ReceivedTime);
            if (!string.IsNullOrEmpty(receivedTime))
                txtReceivedTime.Text = (String)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.ReceivedTime);
            else
                txtReceivedTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

            cboSRBloodSource.SelectedValue = (String)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.SRBloodSource);
            cboSRBloodSourceFrom.SelectedValue = (String)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.SRBloodSourceFrom);
            cboSRBloodGroupReceived.SelectedValue = (String)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.SRBloodGroupReceived);
            var bagno = new BloodBagNo();
            if (bagno.LoadByPrimaryKey(cboBagNo.SelectedValue))
            {
                txtVolumeBag.Value = Convert.ToDouble(bagno.VolumeBag);
                if (bagno.ExpiredDateTime != null)
                    txtExpiredDateTime.SelectedDate = bagno.ExpiredDateTime;
                else txtExpiredDateTime.Clear();
            }
            else
            {
                txtVolumeBag.Value = 0;
                txtExpiredDateTime.Clear();
            }

            try
            {
                rblIsCrossMatchingSuitability.SelectedValue = (bool)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsCrossMatchingSuitability) ? "1" : "0";
            }
            catch (Exception exception)
            {
            }
            var major = (String)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleMajor);
            if (!string.IsNullOrEmpty(major.Trim()))
                rblCrossmatchCompatibleMajor.SelectedValue = major;

            var minor = (String)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleMinor);
            if (!string.IsNullOrEmpty(minor.Trim()))
                rblCrossmatchCompatibleMinor.SelectedValue = minor;

            var autoControl = (String)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleAutoControl);
            if (!string.IsNullOrEmpty(autoControl.Trim()))
                rblCrossmatchCompatibleAutoControl.SelectedValue = autoControl;

            var dct = (String)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleDctControl);
            if (!string.IsNullOrEmpty(dct.Trim()))
                rblCrossmatchCompatibleDctControl.SelectedValue = dct;

            txtCrossmatchCompatibleMinorLevel.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleMinorLevel));
            txtCrossmatchCompatibleAutoControlLevel.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleAutoControlLevel));
            txtCrossmatchCompatibleDctControlLevel.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleDctControlLevel));

            try
            {
                rblIsScreeningLabelPassedPmi.SelectedValue = (bool)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsScreeningLabelPassedPmi) ? "1" : "0";
            }
            catch (Exception exception)
            {
            }

            try
            {
                rblIsExpiredDate.SelectedValue = (bool)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsExpiredDate) ? "1" : "0";
            }
            catch (Exception exception)
            {
            }

            try
            {
                rblIsLeak.SelectedValue = (bool)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsLeak) ? "1" : "0";
            }
            catch (Exception exception)
            {
            }

            try
            {
                rblIsHemolysis.SelectedValue = (bool)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsHemolysis) ? "1" : "0";
            }
            catch (Exception exception)
            {
            }

            try
            {
                rblIsClotting.SelectedValue = (bool)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsClotting) ? "1" : "0";
            }
            catch (Exception exception)
            {
            }

            try
            {
                rblIsBloodTypeCompatibility.SelectedValue = (bool)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsBloodTypeCompatibility) ? "1" : "0";
            }
            catch (Exception exception)
            {
            }

            try
            {
                rblIsLabelDonorsMatchesWithPatientsForm.SelectedValue = (bool)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsLabelDonorsMatchesWithPatientsForm) ? "1" : "0";
            }
            catch (Exception exception)
            {
            }

            try
            {
                rblIsScreeningLabelPassedBd.SelectedValue = (bool)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsScreeningLabelPassedBd) ? "1" : "0";
            }
            catch (Exception exception)
            {
            }

            var examinerByUserId =
                (String)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.ExaminerByUserID);
            if (string.IsNullOrEmpty(examinerByUserId))
                examinerByUserId = AppSession.UserLogin.UserID;

            var usr = new AppUserQuery();
            usr.Where(usr.UserID == examinerByUserId);
            cboExaminerByUserID.DataSource = usr.LoadDataTable();
            cboExaminerByUserID.DataBind();
            cboExaminerByUserID.SelectedValue = examinerByUserId;

            if (pnlCbo.Visible)
            {
                var unitOfficerByUserId =
                (String)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.UnitOfficerByUserID);
                if (!string.IsNullOrEmpty(unitOfficerByUserId)) 
                {
                    usr = new AppUserQuery();
                    usr.Where(usr.UserID == unitOfficerByUserId);
                    cboReceivedByUserID.DataSource = usr.LoadDataTable();
                    cboReceivedByUserID.DataBind();
                    cboReceivedByUserID.SelectedValue = unitOfficerByUserId;
                }
            }
           
            txtUnitOfficer.Text = (String)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.UnitOfficer);
            chkIsVoid.Checked = (bool)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsVoid);
            chkIsScreening1.Checked = (bool)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsScreening1);
            chkIsScreening2.Checked = (bool)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsScreening2);
            chkIsScreening3.Checked = (bool)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsScreening3);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (BloodBankTransactionItemCollection)Session["collBloodBankTransactionItemReceived" + Request.UserHostName];

                string bagNo = cboBagNo.SelectedValue;
                bool isExist = false;

                foreach (BloodBankTransactionItem item in coll)
                {
                    if (item.BagNo.Equals(bagNo))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Bag No : {0} already exist", bagNo);
                }
            }

            if (rblIsCrossMatchingSuitability.SelectedValue == "0")
            {
                if (string.IsNullOrEmpty(rblCrossmatchCompatibleMajor.SelectedValue))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Cross Matching - Major is required.");
                    return;
                }

                if (string.IsNullOrEmpty(rblCrossmatchCompatibleMinor.SelectedValue))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Cross Matching - Minor is required.");
                    return;
                }

                if (string.IsNullOrEmpty(rblCrossmatchCompatibleAutoControl.SelectedValue))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Cross Matching - Auto Control is required.");
                }
            }
        }

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
            if (hdnIsBypassBloodCrossMatching.Value == "0")
            {
                CboSrBloodType.Enabled = false;
                RblBloodRhesus.Enabled = false;
            }
            else
            {
                var coll = (BloodBankTransactionItemCollection)Session["collBloodBankTransactionItemReceived" + Request.UserHostName];
                if (coll.Count == 0)
                {
                    CboSrBloodType.Enabled = true;
                    RblBloodRhesus.Enabled = true;
                }
            }
        }

        protected void cboExaminerByUserID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.UserByUnitItemRequested((RadComboBox)sender, AppSession.Parameter.ServiceUnitBloodBankID, e.Text);
        }

        protected void cboExaminerByUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.UserItemDataBound(e);
        }

        #region Properties for return entry value
        public String BagNo
        {
            get { return cboBagNo.SelectedValue; }
        }
        public DateTime? ReceivedDate
        {
            get { return txtReceivedDate.SelectedDate; }
        }
        public String ReceivedTime
        {
            get { return txtReceivedTime.TextWithLiterals; }
        }
        public string SrBloodGroupReceived
        {
            get { return cboSRBloodGroupReceived.SelectedValue; }
        }
        public string BloodGroupReceived
        {
            get { return cboSRBloodGroupReceived.Text; }
        }
        public Boolean IsScreeningLabelPassedPmi
        {
            get { return rblIsScreeningLabelPassedPmi.SelectedValue == "1"; }
        }
        public Boolean IsExpiredDate
        {
            get { return rblIsExpiredDate.SelectedValue == "1"; }
        }
        public Boolean IsLeak
        {
            get { return rblIsLeak.SelectedValue == "1"; }
        }
        public Boolean IsHemolysis
        {
            get { return rblIsHemolysis.SelectedValue == "1"; }
        }
        public Boolean IsCrossMatchingSuitability
        {
            get { return rblIsCrossMatchingSuitability.SelectedValue == "1"; }
        }
        public string CrossmatchCompatibleMajor
        {
            get { return rblCrossmatchCompatibleMajor.SelectedValue; }
        }
        public string CrossmatchCompatibleMinor
        {
            get { return rblCrossmatchCompatibleMinor.SelectedValue; }
        }
        public Int16? CrossmatchCompatibleMinorLevel
        {
            get { return Convert.ToInt16(txtCrossmatchCompatibleMinorLevel.Value); }
        }
        public string CrossmatchCompatibleAutoControl
        {
            get { return rblCrossmatchCompatibleAutoControl.SelectedValue; }
        }
        public Int16? CrossmatchCompatibleAutoControlLevel
        {
            get { return Convert.ToInt16(txtCrossmatchCompatibleAutoControlLevel.Value); }
        }
        public string CrossmatchCompatibleDctControl
        {
            get { return rblCrossmatchCompatibleDctControl.SelectedValue; }
        }
        public Int16? CrossmatchCompatibleDctControlLevel
        {
            get { return Convert.ToInt16(txtCrossmatchCompatibleDctControlLevel.Value); }
        }
        public Boolean IsClotting
        {
            get { return rblIsClotting.SelectedValue == "1"; }
        }
        public Boolean IsBloodTypeCompatibility
        {
            get { return rblIsBloodTypeCompatibility.SelectedValue == "1"; }
        }
        public Boolean IsLabelDonorsMatchesWithPatientsForm
        {
            get { return rblIsLabelDonorsMatchesWithPatientsForm.SelectedValue == "1"; }
        }
        public Boolean IsScreeningLabelPassedBd
        {
            get { return rblIsScreeningLabelPassedBd.SelectedValue == "1"; }
        }
        public string ExaminerByUserId
        {
            get { return cboExaminerByUserID.SelectedValue; }
        }
        public string ExaminerByUserName
        {
            get { return cboExaminerByUserID.Text; }
        }

        public string ReceivedByUserID
        {
            get { return cboReceivedByUserID.SelectedValue; }
        }

        public string UnitOfficer
        {
            get { return pnlCbo.Visible ? cboReceivedByUserID.Text : txtUnitOfficer.Text; }
        }
        public string SrBloodSource
        {
            get { return cboSRBloodSource.SelectedValue; }
        }
        public string SrBloodSourceFrom
        {
            get { return cboSRBloodSourceFrom.SelectedValue; }
        }
        public Decimal? BloodBagTemperature
        {
            get { return Convert.ToDecimal(txtBloodBagTemperature.Value); }
        }
        public string BloodBagNotes
        {
            get { return txtBloodBagNotes.Text; }
        }
        public Boolean IsVoid
        {
            get { return chkIsVoid.Checked; }
        }
        public Boolean IsScreening1
        {
            get { return chkIsScreening1.Checked; }
        }
        public Boolean IsScreening2
        {
            get { return chkIsScreening2.Checked; }
        }
        public Boolean IsScreening3
        {
            get { return chkIsScreening3.Checked; }
        }

        #endregion

        #region ComboBox
        protected void cboBagNo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboBagNo((RadComboBox)sender, e.Text, true);
        }
        private void PopulateCboBagNo(RadComboBox comboBox, string textSearch, bool isNew)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var rhesus = RblBloodRhesus.SelectedValue == "1" ? "-" : "+";

            var query = new BloodBalanceQuery("a");
            var bagno = new BloodBagNoQuery("b");
            var source = new AppStandardReferenceItemQuery("c");
            var sourcefrom = new AppStandardReferenceItemQuery("d");
            query.Select(query.BagNo, bagno.VolumeBag.Coalesce("0"), bagno.ExpiredDateTime, source.ItemName.As("BloodSource"), sourcefrom.ItemName.As("BloodSourceFrom"));
            query.InnerJoin(bagno).On(bagno.BagNo == query.BagNo);
            query.InnerJoin(source).On(source.StandardReferenceID == AppEnum.StandardReference.BloodSource && source.ItemID == query.SRBloodSource);
            query.InnerJoin(sourcefrom).On(sourcefrom.StandardReferenceID == AppEnum.StandardReference.BloodSourceFrom && sourcefrom.ItemID == query.SRBloodSourceFrom);
            query.Where(query.BagNo.Like(searchTextContain));

            if (isNew)
            {
                query.Where(bagno.SRBloodType == CboSrBloodType.SelectedValue,
                            bagno.BloodRhesus == rhesus,
                            bagno.SRBloodGroup == CboSrBloodGroupRequest.SelectedValue,
                            query.Balance > 0,
                            bagno.IsExtermination == false);

                if (hdnIsBypassBloodCrossMatching.Value == "0")
                    query.Where(bagno.IsCrossMatching == true);
            }
            query.OrderBy(bagno.ExpiredDateTime.Ascending);

            query.es.Top = 20;
            query.es.Distinct = true;
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        protected void cboBagNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["BagNo"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["BagNo"].ToString();
        }
        protected void cboBagNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRBloodGroupReceived.SelectedValue = CboSrBloodGroupRequest.SelectedValue;

            var balanceq = new BloodBalanceQuery();
            balanceq.Select(balanceq.SRBloodSource, balanceq.SRBloodSourceFrom);
            balanceq.Where(balanceq.BagNo == e.Value, balanceq.Balance > 0);
            balanceq.OrderBy(balanceq.SRBloodSource.Ascending, balanceq.SRBloodSourceFrom.Ascending);
            balanceq.es.Top = 1;
            DataTable dbbal = balanceq.LoadDataTable();
            if (dbbal.Rows.Count > 0)
            {
                var coll = new AppStandardReferenceItemCollection();
                var query = new AppStandardReferenceItemQuery("a");
                query.Where(
                    query.StandardReferenceID == AppEnum.StandardReference.BloodSource.ToString(),
                    query.ItemID == dbbal.Rows[0]["SRBloodSource"].ToString()
                    );
                coll.Load(query);

                foreach (var item in coll)
                {
                    cboSRBloodSource.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
                }
                cboSRBloodSource.SelectedValue = dbbal.Rows[0]["SRBloodSource"].ToString();

                coll = new AppStandardReferenceItemCollection();
                query = new AppStandardReferenceItemQuery("a");
                query.Where(
                    query.StandardReferenceID == AppEnum.StandardReference.BloodSourceFrom.ToString(),
                    query.ItemID == dbbal.Rows[0]["SRBloodSourceFrom"].ToString()
                    );
                coll.Load(query);

                foreach (var item in coll)
                {
                    cboSRBloodSourceFrom.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
                }
                cboSRBloodSourceFrom.SelectedValue = dbbal.Rows[0]["SRBloodSourceFrom"].ToString();

                var bbn = new BloodBagNo();
                if (bbn.LoadByPrimaryKey(e.Value))
                {
                    txtVolumeBag.Value = Convert.ToDouble(bbn.VolumeBag);
                    if (bbn.ExpiredDateTime != null)
                        txtExpiredDateTime.SelectedDate = bbn.ExpiredDateTime;
                    else txtExpiredDateTime.Clear();
                }
                else
                {
                    txtVolumeBag.Value = 0;
                    txtExpiredDateTime.Clear();
                }
            }
            else
            {
                cboSRBloodSource.SelectedValue = string.Empty;
                cboSRBloodSourceFrom.SelectedValue = string.Empty;
                txtVolumeBag.Value = 0;
                txtExpiredDateTime.Clear();
            }
        }
        protected void cboSRBloodSource_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRBloodSourceFrom.Items.Clear();
            StandardReference.InitializeIncludeSpace(cboSRBloodSourceFrom, AppEnum.StandardReference.BloodSourceFrom, e.Value);
            cboSRBloodSourceFrom.Text = string.Empty;
        }

        protected void cboReceivedByUserID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.UserByUnitItemRequested((RadComboBox)sender, TxtServiceUnitID.Text, e.Text);
        }
        protected void cboReceivedByUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.UserItemDataBound(e);
        }
        #endregion
    }
}