using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges.BloodSamples
{
    public partial class BloodSamplesReceptionProcessList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = Request.QueryString["tr"].ToString() == "nrs" ? AppConstant.Program.LabBloodSamplesSubmittingProcess : AppConstant.Program.LabBloodSamplesReceptionProcess;

            if (!IsPostBack)
            {
                var rtypes = new AppStandardReferenceItemCollection();
                rtypes.Query.Where(rtypes.Query.StandardReferenceID == "RegistrationType", rtypes.Query.IsActive == true);
                rtypes.LoadAll();
                cboSRRegistrationType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (AppStandardReferenceItem entity in rtypes)
                {
                    cboSRRegistrationType.Items.Add(new RadComboBoxItem(entity.ItemName, entity.ItemID));
                }

                StandardReference.InitializeIncludeSpace(cboTakenBy, AppEnum.StandardReference.BloodSampleTakenBy);
                cboTakenBy.SelectedValue = "nrs";
                trTakenBy.Visible = Request.QueryString["tr"].ToString() != "nrs";

                cboStatus.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboStatus.Items.Add(new RadComboBoxItem("Outstanding", "0"));
                cboStatus.Items.Add(new RadComboBoxItem("Taken", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("Submitted", "2"));
                cboStatus.Items.Add(new RadComboBoxItem("Received (By Analyst)", "3"));

                txtOrderDate1.SelectedDate = DateTime.Now;
                txtOrderDate2.SelectedDate = DateTime.Now;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = TransChargess;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }

        private DataTable TransChargess
        {
            get
            {
                var isEmptyFilter = txtOrderDate1.IsEmpty && txtOrderDate1.IsEmpty && string.IsNullOrEmpty(txtTransactionNo.Text) && string.IsNullOrEmpty(cboSRRegistrationType.SelectedValue) && 
                    string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) && 
                    string.IsNullOrEmpty(cboStatus.SelectedValue) && string.IsNullOrEmpty(cboTakenBy.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Order")) return null;

                var query = new TransChargesQuery("a");
                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var grr = new GuarantorQuery("c");
                var sal = new AppStandardReferenceItemQuery("sal");
                var tb = new AppStandardReferenceItemQuery("tb");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.TransactionNo,
                        query.TransactionDate,
                        query.TransactionDate,
                        @"<CONVERT(VARCHAR(5), a.TransactionDate, 114) AS [TransactionTime]>",
                        query.RegistrationNo,
                        qr.RegistrationDate,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qm.ParamedicName,
                        query.FromServiceUnitID,
                        unit.ServiceUnitName,
                        room.RoomName,
                        qr.BedID,
                        grr.GuarantorName,
                        sal.ItemName.As("SalutationName"),
                        query.SRBloodSampleTakenBy,
                        tb.ItemName.As("BloodSampleTakenBy")

                    );
                query.InnerJoin(qr).On(query.RegistrationNo == qr.RegistrationNo);
                query.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                query.InnerJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                query.InnerJoin(unit).On(query.FromServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(room).On(qr.RoomID == room.RoomID);
                query.InnerJoin(grr).On(qr.GuarantorID == grr.GuarantorID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);
                query.InnerJoin(tb).On(tb.StandardReferenceID == "BloodSampleTakenBy" & tb.ItemID == query.SRBloodSampleTakenBy);

                if (Request.QueryString["tr"].ToString() == "nrs")
                {
                    var usr = new AppUserServiceUnitQuery("usr");
                    query.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == query.FromServiceUnitID);
                }

                if (!txtOrderDate1.IsEmpty && !txtOrderDate1.IsEmpty)
                    query.Where(query.TransactionDate.Date().Between(txtOrderDate1.SelectedDate, txtOrderDate2.SelectedDate));
                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                if (cboSRRegistrationType.SelectedValue != string.Empty)
                    query.Where(qr.SRRegistrationType == cboSRRegistrationType.SelectedValue);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    query.Where
                        (qr.Or
                             (
                                 string.Format("<r.RegistrationNo = '{0}' OR >", searchReg),
                                 string.Format("<p.MedicalNo = '{0}' OR >", searchReg),
                                 string.Format("<p.OldMedicalNo = '{0}'>", searchReg),
                                 string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                                 string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                             )
                        );
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                if (!string.IsNullOrEmpty(cboStatus.SelectedValue) || !string.IsNullOrEmpty(cboTakenBy.SelectedValue))
                {
                    var detail = new TransChargesItemQuery("aa");
                    query.InnerJoin(detail).On(detail.TransactionNo == query.TransactionNo);

                    if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
                    {
                        switch (cboStatus.SelectedValue)
                        {
                            case "0":
                                query.Where(detail.SpecimenTakenDateTime.IsNull());
                                break;
                            case "1":
                                query.Where(detail.SpecimenTakenDateTime.IsNotNull(), detail.SpecimenSubmittedDateTime.IsNull());
                                break;
                            case "2":
                                query.Where(detail.SpecimenTakenDateTime.IsNotNull(), detail.SpecimenSubmittedDateTime.IsNotNull(), detail.SpecimenReceivedDateTime.IsNull());
                                break;
                            case "3":
                                query.Where(detail.SpecimenReceivedDateTime.IsNotNull());
                                break;
                        }
                    }
                    if (!string.IsNullOrEmpty(cboTakenBy.SelectedValue))
                    {
                        var itmlab = new ItemLaboratoryQuery("bb");
                        var stype = new AppStandardReferenceItemQuery("st");
                        query.LeftJoin(itmlab).On(itmlab.ItemID == detail.ItemID);
                        query.LeftJoin(stype).On(stype.StandardReferenceID == "SpecimenType" & stype.ItemID == itmlab.SRSpecimenType);

                        if (cboTakenBy.SelectedValue == "nrs")
                            query.Where(@"<CASE WHEN a.SRBloodSampleTakenBy = 'nrs' THEN (CASE WHEN ISNULL(st.ReferenceID, '') LIKE '%' + a.SRBloodSampleTakenBy + '%' THEN a.SRBloodSampleTakenBy ELSE 'lab' END) ELSE 
                                    (CASE WHEN ISNULL(st.ReferenceID, '') LIKE '%' + a.SRBloodSampleTakenBy + '%' THEN a.SRBloodSampleTakenBy ELSE 'nrs' END) 
                                    END = 'nrs'>");
                        else
                            query.Where(@"<CASE WHEN a.SRBloodSampleTakenBy = 'nrs' THEN (CASE WHEN ISNULL(st.ReferenceID, '') LIKE '%' + a.SRBloodSampleTakenBy + '%' THEN a.SRBloodSampleTakenBy ELSE 'lab' END) ELSE 
                                    (CASE WHEN ISNULL(st.ReferenceID, '') LIKE '%' + a.SRBloodSampleTakenBy + '%' THEN a.SRBloodSampleTakenBy ELSE 'nrs' END) 
                                    END = 'lab'>");

                    }
                    query.es.Distinct = true;
                }

                query.Where(query.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID,
                    query.IsApproved == true,
                    query.Or(query.SRBloodSampleTakenBy.IsNotNull(), query.SRBloodSampleTakenBy != string.Empty));

                query.OrderBy(query.TransactionNo.Descending);

                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        private void BloodSamplesProcess(string transactionNo, string seqNo, string status)
        {
            var tc = new TransCharges();
            tc.LoadByPrimaryKey(transactionNo);

            var tci = new TransChargesItem();
            if (tci.LoadByPrimaryKey(transactionNo, seqNo))
            {
                var date = (new DateTime()).NowAtSqlServer();

                var itemLab = new ItemLaboratory();
                if (itemLab.LoadByPrimaryKey(tci.ItemID) & !string.IsNullOrEmpty(itemLab.SRSpecimenType))
                {
                    var query = new TransChargesItemQuery("a");
                    var qitmlab = new ItemLaboratoryQuery("b");
                    query.InnerJoin(qitmlab).On(qitmlab.ItemID == query.ItemID);
                    query.Where(query.TransactionNo == transactionNo, qitmlab.SRSpecimenType == itemLab.SRSpecimenType);

                    var coll = new TransChargesItemCollection();
                    coll.Load(query);
                    if (coll.Count > 0)
                    {
                        foreach (var c in coll)
                        {
                            var tci2 = new TransChargesItem();
                            if (tci2.LoadByPrimaryKey(c.TransactionNo, c.SequenceNo))
                            {
                                switch (status)
                                {
                                    case "0":
                                        tci2.SpecimenTakenDateTime = date;
                                        tci2.SpecimenTakenByUserID = AppSession.UserLogin.UserID;
                                        break;
                                    case "1":
                                        tci2.SpecimenSubmittedDateTime = date;
                                        tci2.SpecimenSubmittedByUserID = AppSession.UserLogin.UserID;
                                        break;
                                    case "2":
                                        tci2.SpecimenReceivedDateTime = date;
                                        tci2.SpecimenReceivedByUserID = AppSession.UserLogin.UserID;
                                        break;
                                }
                            }
                            tci2.Save();
                        }
                    }
                    else
                    {
                        switch (status)
                        {
                            case "0":
                                tci.SpecimenTakenDateTime = date;
                                tci.SpecimenTakenByUserID = AppSession.UserLogin.UserID;
                                break;
                            case "1":
                                tci.SpecimenSubmittedDateTime = date;
                                tci.SpecimenSubmittedByUserID = AppSession.UserLogin.UserID;
                                break;
                            case "2":
                                tci.SpecimenReceivedDateTime = date;
                                tci.SpecimenReceivedByUserID = AppSession.UserLogin.UserID;
                                break;
                        }
                        tci.Save();
                    }
                }
                else
                {
                    switch (status)
                    {
                        case "0":
                            tci.SpecimenTakenDateTime = date;
                            tci.SpecimenTakenByUserID = AppSession.UserLogin.UserID;
                            break;
                        case "1":
                            tci.SpecimenSubmittedDateTime = date;
                            tci.SpecimenSubmittedByUserID = AppSession.UserLogin.UserID;
                            break;
                        case "2":
                            tci.SpecimenReceivedDateTime = date;
                            tci.SpecimenReceivedByUserID = AppSession.UserLogin.UserID;
                            break;
                    }
                    tci.Save();
                }
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');

                BloodSamplesProcess(param[0], param[1], param[2]);

                //grdList.DataSource = null;
                grdList.Rebind();
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string tno = dataItem.GetDataKeyValue("TransactionNo").ToString();

            var query = new TransChargesItemQuery("a");
            var tc = new TransChargesQuery("aa");
            var itm = new ItemQuery("b");
            var itmlab = new ItemLaboratoryQuery("bb");
            var stype = new AppStandardReferenceItemQuery("st");
            var usrt = new AppUserQuery("ut");
            var usrs = new AppUserQuery("us");
            var usrr = new AppUserQuery("ur");

            query.InnerJoin(tc).On(tc.TransactionNo == query.TransactionNo);
            query.InnerJoin(itm).On(itm.ItemID == query.ItemID);
            query.LeftJoin(itmlab).On(itmlab.ItemID == query.ItemID);
            query.LeftJoin(stype).On(stype.StandardReferenceID == "SpecimenType" & stype.ItemID == itmlab.SRSpecimenType);
            query.LeftJoin(usrt).On(usrt.UserID == query.SpecimenTakenByUserID);
            query.LeftJoin(usrs).On(usrs.UserID == query.SpecimenSubmittedByUserID);
            query.LeftJoin(usrr).On(usrr.UserID == query.SpecimenReceivedByUserID);

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    itm.ItemName,
                    query.SRItemUnit,
                    query.ChargeQuantity,
                    stype.ItemName.As("SpecimenTypeName"),
                    query.SpecimenTakenDateTime,
                    query.SpecimenTakenByUserID,
                    usrt.UserName.As("SpecimenTakenByUserName"),
                    query.SpecimenSubmittedDateTime,
                    query.SpecimenSubmittedByUserID,
                    usrs.UserName.As("SpecimenSubmittedByUserName"),
                    query.SpecimenReceivedDateTime,
                    query.SpecimenReceivedByUserID,
                    usrr.UserName.As("SpecimenReceivedByUserName"),
                    @"<CASE WHEN aa.SRBloodSampleTakenBy = 'nrs' THEN (CASE WHEN ISNULL(st.ReferenceID, '') LIKE '%' + aa.SRBloodSampleTakenBy + '%' THEN aa.SRBloodSampleTakenBy ELSE 'lab' END) ELSE 
                        (CASE WHEN ISNULL(st.ReferenceID, '') LIKE '%' + aa.SRBloodSampleTakenBy + '%' THEN aa.SRBloodSampleTakenBy ELSE 'nrs' END) 
                    END AS 'SRBloodSampleTakenBy'>",
                    @"<CASE WHEN a.SpecimenReceivedDateTime IS NULL THEN (CASE WHEN a.SpecimenSubmittedDateTime IS NULL THEN (CASE WHEN a.SpecimenTakenDateTime IS NULL THEN '0' ELSE '1' END) ELSE '2' END) ELSE '3' END AS 'Status'>"
                );

            query.Where(query.TransactionNo == tno);
            query.OrderBy(itmlab.SRSpecimenType.Ascending, query.SequenceNo.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected void cboSRRegistrationType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.SelectedValue = string.Empty;
            cboServiceUnitID.Text = string.Empty;
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboServiceUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery("a");
            if (Request.QueryString["tr"].ToString() == "nrs")
            {
                var usr = new AppUserServiceUnitQuery("b");
                query.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == query.ServiceUnitID);
            }
            query.Select(query.ServiceUnitID, query.ServiceUnitName);

            if (!string.IsNullOrEmpty(cboSRRegistrationType.SelectedValue))
                query.Where(query.SRRegistrationType == cboSRRegistrationType.SelectedValue);
            else
                query.Where(query.SRRegistrationType.In(AppConstant.RegistrationType.InPatient,
                AppConstant.RegistrationType.OutPatient, AppConstant.RegistrationType.EmergencyPatient, AppConstant.RegistrationType.MedicalCheckUp));

            query.Where
                (
                    query.ServiceUnitName.Like(searchText), query.IsActive == true
                );

            cboServiceUnitID.DataSource = query.LoadDataTable();
            cboServiceUnitID.DataBind();
        }
    }
}