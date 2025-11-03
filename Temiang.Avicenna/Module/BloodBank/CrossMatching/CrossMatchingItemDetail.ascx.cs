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
    public partial class CrossMatchingItemDetail : BaseUserControl
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

        protected override void OnDataBinding(EventArgs e)
        {
            CboSrBloodType.Enabled = false;
            RblBloodRhesus.Enabled = false;

            StandardReference.InitializeIncludeSpace(cboSRBloodGroupReceived, AppEnum.StandardReference.BloodGroup);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                txtCrossmatchStartDateTime.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtCrossmatchEndDateTime.SelectedDate = (new DateTime()).NowAtSqlServer();

                var au = new AppUserQuery();
                au.Where(au.UserID == AppSession.UserLogin.UserID);
                cboCrossMatchingByUserID.DataSource = au.LoadDataTable();
                cboCrossMatchingByUserID.DataBind();
                cboCrossMatchingByUserID.SelectedValue = AppSession.UserLogin.UserID;

                return;
            }
            ViewState["IsNewRecord"] = false;

            cboBagNo.Enabled = false;
            cboSRBloodSource.Enabled = false;
            cboSRBloodSourceFrom.Enabled = false;

            StandardReference.InitializeIncludeSpace(cboSRBloodSource, AppEnum.StandardReference.BloodSource);
            StandardReference.InitializeIncludeSpace(cboSRBloodSourceFrom, AppEnum.StandardReference.BloodSourceFrom);

            PopulateCboBagNo(cboBagNo, (String)DataBinder.Eval(DataItem, "BagNo"), false);
            cboBagNo.Text = (String)DataBinder.Eval(DataItem, "BagNo");
            cboBagNo.SelectedValue = (String)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.BagNo);
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
            object startDate = DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchStartDateTime);
            if (startDate != null)
                txtCrossmatchStartDateTime.SelectedDate = (DateTime)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchStartDateTime);
            else
                txtCrossmatchStartDateTime.Clear();
            object endDate = DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchEndDateTime);
            if (endDate != null)
                txtCrossmatchEndDateTime.SelectedDate = (DateTime)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchEndDateTime);
            else
                txtCrossmatchEndDateTime.Clear();
            rblIsCrossMatchingSuitability.SelectedValue = (bool)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsCrossMatchingSuitability) ? "1" : "0";
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
            
            chkIsCrossmatchingPassed.Checked = (bool) DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsCrossmatchingPassed);
            var crossmatchingByUserId =
                (String)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.CrossMatchingByUserID);
            var usr = new AppUserQuery();
            usr.Where(usr.UserID == crossmatchingByUserId);
            cboCrossMatchingByUserID.DataSource = usr.LoadDataTable();
            cboCrossMatchingByUserID.DataBind();
            cboCrossMatchingByUserID.SelectedValue = crossmatchingByUserId;
            chkIsVoid.Checked = (bool)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsVoid);
            chkIsCrossmatchBillProceed.Checked = (bool)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsCrossmatchBillProceed);
            chkIsScreening1.Checked = (bool)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsScreening1);
            chkIsScreening2.Checked = (bool)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsScreening2);
            chkIsScreening3.Checked = (bool)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.IsScreening3);

            SetEnabledCrossmatchCompatible(false);

            var srBloodBagStatus = (String)DataBinder.Eval(DataItem, BloodBankTransactionItemMetadata.ColumnNames.SRBloodBagStatus);
            if (srBloodBagStatus != "1")
            {
                txtCrossmatchStartDateTime.Enabled = false;
                txtCrossmatchStartDateTime.Enabled = false;
                cboSRBloodGroupReceived.Enabled = false;
                rblIsCrossMatchingSuitability.Enabled = false;
                rblCrossmatchCompatibleMajor.Enabled = false;
                rblCrossmatchCompatibleMinor.Enabled = false;
                txtCrossmatchCompatibleMinorLevel.Enabled = false;
                rblCrossmatchCompatibleAutoControl.Enabled = false;
                txtCrossmatchCompatibleAutoControlLevel.Enabled = false;
                rblCrossmatchCompatibleDctControl.Enabled = false;
                txtCrossmatchCompatibleDctControlLevel.Enabled = false;
                cboCrossMatchingByUserID.Enabled = false;
                chkIsCrossmatchingPassed.Enabled = false;

                chkIsScreening1.Enabled = false;
                chkIsScreening2.Enabled = false;
                chkIsScreening3.Enabled = false;
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (BloodBankTransactionItemCollection)Session["collBloodBankTransactionItemCrossMatching" + Request.UserHostName];

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
            var coll = (BloodBankTransactionItemCollection)Session["collBloodBankTransactionItemCrossMatching" + Request.UserHostName];
            if (coll.Count == 0)
            {
                CboSrBloodType.Enabled = true;
                RblBloodRhesus.Enabled = true;
            }
        }

        #region ComboBox
        protected void cboBagNo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboBagNo((RadComboBox)sender, e.Text, true);
        }
        private void PopulateCboBagNo(RadComboBox comboBox, string textSearch, bool isNew)
        {
            var rhesus = RblBloodRhesus.SelectedValue == "1" ? "-" : "+";
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new BloodBalanceQuery("a");
            var bagno = new BloodBagNoQuery("b");
            var source = new AppStandardReferenceItemQuery("c");
            var sourcefrom = new AppStandardReferenceItemQuery("d");
            query.Select(query.BagNo, bagno.ExpiredDateTime, bagno.VolumeBag.Coalesce("0"), source.ItemName.As("BloodSource"), sourcefrom.ItemName.As("BloodSourceFrom"));
            query.InnerJoin(bagno).On(bagno.BagNo == query.BagNo);
            query.InnerJoin(source).On(source.ItemID == query.SRBloodSource &&
                                       source.StandardReferenceID == AppEnum.StandardReference.BloodSource);
            query.InnerJoin(sourcefrom).On(sourcefrom.ItemID == query.SRBloodSourceFrom &&
                                           sourcefrom.StandardReferenceID == AppEnum.StandardReference.BloodSourceFrom);
            query.Where(
                query.BagNo.Like(searchTextContain),
                bagno.SRBloodType == CboSrBloodType.SelectedValue,
                bagno.BloodRhesus == rhesus);

            if (isNew)
            {
                query.Where(bagno.SRBloodGroup == CboSrBloodGroupRequest.SelectedValue, query.Balance > 0,
                            bagno.IsCrossMatching == false,
                            bagno.IsExtermination == false);
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
        protected void cboCrossMatchingByUserID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.UserByUnitItemRequested((RadComboBox)sender, AppSession.Parameter.ServiceUnitBloodBankID, e.Text);
        }
        protected void cboCrossMatchingByUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.UserItemDataBound(e);
        }
        #endregion

        protected void rblIsCrossMatchingSuitability_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SetEnabledCrossmatchCompatible(true);
        }

        private void SetEnabledCrossmatchCompatible(bool isNew)
        {
            chkIsCrossmatchingPassed.Checked = rblIsCrossMatchingSuitability.SelectedValue == "1";
            rblCrossmatchCompatibleMajor.Enabled = rblIsCrossMatchingSuitability.SelectedValue == "0";
            rblCrossmatchCompatibleMinor.Enabled = rblIsCrossMatchingSuitability.SelectedValue == "0";
            rblCrossmatchCompatibleAutoControl.Enabled = rblIsCrossMatchingSuitability.SelectedValue == "0";
            rblCrossmatchCompatibleDctControl.Enabled = rblIsCrossMatchingSuitability.SelectedValue == "0";
            txtCrossmatchCompatibleMinorLevel.ReadOnly = rblIsCrossMatchingSuitability.SelectedValue == "1";
            txtCrossmatchCompatibleAutoControlLevel.ReadOnly = rblIsCrossMatchingSuitability.SelectedValue == "1";
            txtCrossmatchCompatibleDctControlLevel.ReadOnly = rblIsCrossMatchingSuitability.SelectedValue == "1";

            if (isNew)
            {
                CalculatedForPassedStatus();
            }
        }

        protected void rblIsCrossMatching_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CalculatedForPassedStatus();
        }

        private void CalculatedForPassedStatus()
        {
            if (rblIsCrossMatchingSuitability.SelectedValue == "1")
                chkIsCrossmatchingPassed.Checked = true;
            else
            {
                if (rblCrossmatchCompatibleMajor.SelectedValue == "+")
                    chkIsCrossmatchingPassed.Checked = false;
                else
                {
                    if (rblCrossmatchCompatibleMinor.SelectedValue == "-" && rblCrossmatchCompatibleAutoControl.SelectedValue == "-")
                        chkIsCrossmatchingPassed.Checked = true;
                    else
                        chkIsCrossmatchingPassed.Checked = (txtCrossmatchCompatibleMinorLevel.Value ?? 0) <=
                                                           (txtCrossmatchCompatibleAutoControlLevel.Value ?? 0);
                }
            }
        }

        #region Properties for return entry value
        public String BagNo
        {
            get { return cboBagNo.SelectedValue; }
        }
        public DateTime? CrossmatchStartDateTime
        {
            get { return txtCrossmatchStartDateTime.SelectedDate; }
        }
        public DateTime? CrossmatchEndDateTime
        {
            get { return txtCrossmatchEndDateTime.SelectedDate; }
        }
        public string SrBloodGroupReceived
        {
            get { return cboSRBloodGroupReceived.SelectedValue; }
        }
        public string BloodGroupReceived
        {
            get { return cboSRBloodGroupReceived.Text; }
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
        public string CrossMatchingByUserID
        {
            get { return cboCrossMatchingByUserID.SelectedValue; }
        }
        public string CrossMatchingByUserName
        {
            get { return cboCrossMatchingByUserID.Text; }
        }
        public string SrBloodSource
        {
            get { return cboSRBloodSource.SelectedValue; }
        }
        public string SrBloodSourceFrom
        {
            get { return cboSRBloodSourceFrom.SelectedValue; }
        }
        public Boolean IsVoid
        {
            get { return chkIsVoid.Checked; }
        }
        public Boolean IsCrossmatchingPassed
        {
            get { return chkIsCrossmatchingPassed.Checked; }
        }
        public Boolean IsCrossmatchBillProceed
        {
            get { return chkIsCrossmatchBillProceed.Checked; }
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
    }
}