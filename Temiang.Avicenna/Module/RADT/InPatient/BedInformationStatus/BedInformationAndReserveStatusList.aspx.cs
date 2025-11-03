using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using System.Drawing;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class BedInformationAndReserveStatusList : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.BedInformationAndReserveStatus;

            if (!IsPostBack)
            {
                var unitColl = new ServiceUnitCollection();
                unitColl.Query.Where(unitColl.Query.DepartmentID == AppSession.Parameter.InPatientDepartmentID,
                                     unitColl.Query.IsActive == true);
                unitColl.LoadAll();
                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit unit in unitColl)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(unit.ServiceUnitName, unit.ServiceUnitID));
                }

                StandardReference.InitializeIncludeSpace(cboBedStatus, AppEnum.StandardReference.BedStatus);
                StandardReference.InitializeIncludeSpace(cboSRReligion, AppEnum.StandardReference.Religion);
                trRoomIn.Visible = AppSession.Parameter.IsUsingRoomingIn;
                trReligion.Visible = false;
                trAddress.Visible = false;
                grdList.Columns[5].Visible = AppSession.Parameter.IsUsingRoomingIn;
                grdList.Columns[grdList.Columns.Count - 1].Visible = this.IsPowerUser; //clear bed

                grdBedManagement.Columns[grdBedManagement.Columns.Count - 2].Visible = this.IsPowerUser; //clear bed
                grdBedManagement.Columns[grdBedManagement.Columns.Count - 1].Visible = this.IsPowerUser; //clear bed

                txtReady.ReadOnly = true;
                txtReady.BackColor = System.Drawing.Color.Green;
                txtOccupied.ReadOnly = true;
                txtOccupied.BackColor = System.Drawing.Color.Red;
                txtBooked.ReadOnly = true;
                txtBooked.BackColor = System.Drawing.Color.Brown;
                txtPending.ReadOnly = true;
                txtPending.BackColor = System.Drawing.Color.Orange;
                txtCleaning.ReadOnly = true;
                txtCleaning.BackColor = System.Drawing.Color.Yellow;
                txtReserved.ReadOnly = true;
                txtReserved.BackColor = System.Drawing.Color.Blue;
                txtRepaired.ReadOnly = true;
                txtRepaired.BackColor = System.Drawing.Color.Purple;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Beds;
        }

        private DataTable Beds
        {
            get
            {
                var query = new BedQuery("a");
                var srq = new ServiceRoomQuery("b");
                var suq = new ServiceUnitQuery("c");
                var cq = new ClassQuery("e");
                var rq = new RegistrationQuery("f");
                var gq = new GuarantorQuery("d");
                var pq = new PatientQuery("g");
                var asri = new AppStandardReferenceItemQuery("h");
                var religion = new AppStandardReferenceItemQuery("rgn");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.InnerJoin(srq).On
                    (
                        query.RoomID == srq.RoomID &
                        srq.IsActive == true
                    );
                query.InnerJoin(suq).On
                    (
                        srq.ServiceUnitID == suq.ServiceUnitID &
                        suq.IsActive == true
                    );
                query.InnerJoin(cq).On
                    (
                        query.ClassID == cq.ClassID &
                        cq.IsActive == true
                    );
                query.LeftJoin(rq).On
                    (
                        query.RegistrationNo == rq.RegistrationNo && rq.SRRegistrationType == AppConstant.RegistrationType.InPatient
                    );
                query.LeftJoin(gq).On(rq.GuarantorID == gq.GuarantorID);
                query.LeftJoin(pq).On(rq.PatientID == pq.PatientID);
                query.InnerJoin(asri).On
                    (
                        query.SRBedStatus == asri.ItemID &&
                        asri.StandardReferenceID == AppEnum.StandardReference.BedStatus
                    );
                // Religion
                query.LeftJoin(religion).On
                    (
                        pq.SRReligion == religion.ItemID &&
                        religion.StandardReferenceID == AppEnum.StandardReference.Religion
                    );
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & pq.SRSalutation == sal.ItemID);

                query.Select
                    (
                        query.BedID,
                        cq.ClassName,
                        query.RegistrationNo,
                        pq.MedicalNo.Coalesce("''"),
                        pq.PatientName.Coalesce("''"),
                        pq.Address.Coalesce("''"),
                        gq.GuarantorName,
                        query.SRBedStatus,
                        asri.ItemName,
                        query.IsRoomIn,
                        rq.DischargePlanDate,
                        pq.Sex,
                        rq.IsHoldTransactionEntry.Cast(esCastType.Boolean),
                        "<CASE WHEN f.IsVoid = 1 OR f.SRDischargeMethod <> '' OR a.RegistrationNo = '' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsNeedToBeClear>",
                        "<CAST(0 AS BIT) AS IsAttention>",
                        @"<'' AS AttentionNotes>",
                        @"<'' AS SRBedStatusDetail>",
                        rq.ChargeClassID,
                        rq.CoverageClassID,
                        rq.ClassID,
                        query.DefaultChargeClassID.As("DefaultClassID"),
                        religion.ItemName.As("ReligionName"),
                        sal.ItemName.As("SalutationName"),
                        srq.ServiceUnitID,
                        suq.ServiceUnitName,
                        query.RoomID,
                        srq.RoomName,
                        "<CASE WHEN ISNULL(g.Sex, '') <> '' THEN g.Sex ELSE ISNULL(b.SRGenderType, '') END AS RoomGender>",
                        rq.IsClosed
                    );

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(srq.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboRoomID.SelectedValue != string.Empty)
                    query.Where(srq.RoomID == cboRoomID.SelectedValue);
                if (cboClassID.SelectedValue != string.Empty)
                    query.Where(query.ClassID == cboClassID.SelectedValue);
                if (cboBedStatus.SelectedValue != string.Empty)
                    query.Where(query.SRBedStatus == cboBedStatus.SelectedValue);
                if (cboSRReligion.SelectedValue != string.Empty)
                    query.Where(pq.SRReligion == cboSRReligion.SelectedValue);
                if (!(string.IsNullOrEmpty(txtPatientSearch.Text)))
                {
                    if (txtPatientSearch.Text.Trim().Contains(" "))
                    {
                        var searchs = txtPatientSearch.Text.Trim().Split(' ');
                        foreach (var search in searchs)
                        {
                            var searchLike = "%" + search + "%";
                            query.Where(
                                pq.Or(
                                    pq.PatientID.Like(searchLike),
                                    pq.FirstName.Like(searchLike),
                                    pq.LastName.Like(searchLike),
                                    pq.MiddleName.Like(searchLike),
                                    pq.MedicalNo.Like(searchLike),
                                    pq.OldMedicalNo.Like(searchLike)
                                    )
                                );
                        }
                    }
                    else
                    {
                        var searchLike = "%" + txtPatientSearch.Text.Trim() + "%";
                        query.Where(
                            pq.Or(
                                pq.PatientID.Like(searchLike),
                                pq.FirstName.Like(searchLike),
                                pq.LastName.Like(searchLike),
                                pq.MiddleName.Like(searchLike),
                                pq.MedicalNo.Like(searchLike),
                                pq.OldMedicalNo.Like(searchLike)
                                )
                            );
                    }
                }
                if (chkIsRoomIn.Checked)
                    query.Where(query.IsRoomIn == true);
                if (!string.IsNullOrEmpty(txtAddress.Text))
                    query.Where("<COALESCE((RTRIM((((RTRIM(g.[StreetName])+RTRIM((' '+g.[City])))+' ')+g.[County]))+RTRIM((' '+COALESCE(g.[ZipCode],'')))),'') LIKE '%" + txtAddress.Text + "%'>");
                if (chkIsClosed.Checked)
                    query.Where(rq.IsClosed == true);
                query.Where(query.IsActive == true);
                query.OrderBy(suq.ServiceUnitName.Ascending, srq.RoomName.Ascending, query.BedID.Ascending);

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var bmQ = new BedManagementQuery("a");
                    var resQ = new ReservationQuery("b");
                    bmQ.LeftJoin(resQ).On(resQ.ReservationNo == bmQ.ReservationNo);
                    bmQ.Where(bmQ.BedID == row["BedID"].ToString(), bmQ.IsVoid == false,
                        bmQ.Or(bmQ.IsReleased == false, resQ.ReservationDate > (new DateTime()).NowAtSqlServer())
                        );
                    bmQ.OrderBy(bmQ.SRBedStatus.Ascending);

                    var bm = new BedManagementCollection();
                    bm.Load(bmQ);
                    if (bm.Count > 0)
                    {
                        row["IsAttention"] = true;
                        row["AttentionNotes"] = "Reserved / Booked";
                        var status = string.Empty;
                        foreach (var x in bm)
                        {
                            if (status != x.SRBedStatus)
                            {
                                status = x.SRBedStatus;
                                if (row["SRBedStatusDetail"].ToString() == string.Empty)
                                    row["SRBedStatusDetail"] = status;
                                else row["SRBedStatusDetail"] = row["SRBedStatusDetail"].ToString() + "," + status;
                            }
                        }
                    }
                    else
                    {
                        var ri = new BedRoomInCollection();
                        ri.Query.Where(ri.Query.BedID == row["BedID"].ToString(), ri.Query.IsVoid == false,
                            ri.Query.DateOfExit.IsNull());
                        ri.LoadAll();
                        if (ri.Count > 0)
                        {
                            row["IsAttention"] = true;
                            row["AttentionNotes"] = "Room In";
                        }
                    }
                    if (string.IsNullOrEmpty(row["RoomGender"].ToString()))
                    {
                        var bedq = new BedQuery("a");
                        var regq = new RegistrationQuery("b");
                        var patq = new PatientQuery("c");
                        bedq.InnerJoin(regq).On(bedq.RegistrationNo == regq.RegistrationNo);
                        bedq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);
                        bedq.Where(bedq.RoomID == row["RoomID"].ToString());
                        bedq.es.Top = 1;
                        bedq.Select(patq.Sex);
                        DataTable dtbbed = bedq.LoadDataTable();
                        if (dtbbed.Rows.Count > 0)
                            row["RoomGender"] = dtbbed.Rows[0]["Sex"].ToString();
                        else
                            row["RoomGender"] = "MF";
                    }

                    var isDelete = false;
                    if (!string.IsNullOrEmpty(cboBedStatus.SelectedValue))
                    {
                        if (row["SRBedStatus"].ToString() != cboBedStatus.SelectedValue & !(row["SRBedStatusDetail"].ToString().Contains(cboBedStatus.SelectedValue)))
                            isDelete = true;
                    }
                    if (isDelete == false & rblGender.SelectedIndex != 0)
                    {
                        if (rblGender.SelectedIndex == 1 && row["RoomGender"].ToString() == "F")
                            isDelete = true;
                        else if (rblGender.SelectedIndex == 2 && row["RoomGender"].ToString() == "M")
                            isDelete = true;
                    }
                    if (isDelete)
                        row.Delete();
                }
                if (rblGender.SelectedIndex == 3 || rblGender.SelectedIndex == 4)
                {
                    var uf = dtb.AsEnumerable().Where(f => f.Field<string>("Sex") == (rblGender.SelectedIndex == 3 ? "F" : "M"))
                        .Select(f => f.Field<string>("Group")).ToArray().Distinct();
                    var ufR = dtb.AsEnumerable().Where(d => uf.Contains(d.Field<string>("Group")));
                    foreach (var u in ufR)
                    {
                        u.Delete();
                    }
                }
                dtb.AcceptChanges();

                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
            grdBedManagement.Rebind();
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    var tooltip = string.Empty;
            //    var dataItem = e.Item as GridDataItem;
            //    if (dataItem["RegistrationNo"].Text != "&nbsp;")
            //    {
            //        if (dataItem["ChargeClassID"].Text != dataItem["CoverageClassID"].Text)
            //        {
            //            // Beri warna merah jika CoverageClassID berbeda dg ChargeClassID
            //            dataItem.ForeColor = Color.Red;
            //            dataItem.Font.Bold = true;
            //            tooltip = "Charge class is different from coverage class.";
            //        }
            //        if (dataItem["ChargeClassID"].Text != dataItem["DefaultClassID"].Text)
            //        {
            //            var c = new Class();
            //            c.LoadByPrimaryKey(dataItem["DefaultClassID"].Text);
            //            if (c.IsTariffClass ?? false)
            //            {
            //                dataItem.Font.Bold = true;
            //                dataItem.Font.Italic = true;
            //                tooltip = tooltip == string.Empty ? "Charge class is different from bed class." : "Charge class is different from coverage and bed class.";
            //            }
            //        }
            //        dataItem.ToolTip = tooltip;
            //    }
            //}
        }

        protected void grdBedManagement_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdBedManagement.DataSource = BedManagements;
        }

        private DataTable BedManagements
        {
            get
            {
                var query = new BedManagementQuery("a");
                var qpat = new PatientQuery("b");
                var qstd = new AppStandardReferenceItemQuery("c");
                var qsal = new AppStandardReferenceItemQuery("d");
                var qres = new ReservationQuery("e");
                var qbed = new BedQuery("f");
                var qsr = new ServiceRoomQuery("g");
                var qsu = new ServiceUnitQuery("h");
                var qc = new ClassQuery("i");

                query.LeftJoin(qpat).On(qpat.PatientID == query.PatientID);
                query.InnerJoin(qstd).On(
                    query.SRBedStatus == qstd.ItemID &&
                    qstd.StandardReferenceID == AppEnum.StandardReference.BedStatus
                    );
                query.LeftJoin(qsal).On(qsal.StandardReferenceID == "Salutation" & qpat.SRSalutation == qsal.ItemID);
                query.LeftJoin(qres).On(qres.RegistrationNo == query.ReservationNo);
                query.InnerJoin(qbed).On(qbed.BedID == query.BedID);
                query.InnerJoin(qsr).On(qsr.RoomID == qbed.RoomID && qsr.IsActive == true);
                query.InnerJoin(qsu).On(qsu.ServiceUnitID == qsr.ServiceUnitID && qsu.IsActive == true);
                query.InnerJoin(qc).On(qc.ClassID == qbed.ClassID && qc.IsActive == true);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.Select
                (
                    query.BedID,
                    query.BedManagementID,
                    query.TransactionDate,
                    qpat.MedicalNo,
                    query.RegistrationNo,
                    @"<CASE WHEN b.FirstName IS NULL THEN RTRIM(RTRIM(a.FirstName + ' ' + a.MiddleName) + ' ' + a.LastName) ELSE RTRIM(RTRIM(b.FirstName + ' ' + b.MiddleName) + ' ' + b.LastName) END AS PatientName>",
                    @"<CASE WHEN b.StreetName IS NULL THEN RTRIM(RTRIM(a.StreetName + ' ' + a.City) + ' ' + a.County) ELSE RTRIM(RTRIM(b.StreetName + ' ' + b.City) + ' ' + b.County) END AS Address>",
                    query.BedID,
                    query.SRBedStatus,
                    qstd.ItemName.As("BedStatusName"),
                    qsal.ItemName.As("SalutationName"),

                    qc.ClassName,
                    qsr.RoomName,
                    qsu.ServiceUnitName
                );

                query.Where(query.IsVoid == false, query.Or(query.IsReleased == false, qres.ReservationDate > (new DateTime()).NowAtSqlServer()));

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(qsr.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboRoomID.SelectedValue != string.Empty)
                    query.Where(qsr.RoomID == cboRoomID.SelectedValue);
                if (cboClassID.SelectedValue != string.Empty)
                    query.Where(qbed.ClassID == cboClassID.SelectedValue);

                query.OrderBy(qsu.ServiceUnitName.Ascending, qsr.RoomName.Ascending, query.BedID.Ascending, query.TransactionDate.Ascending);

                DataTable dtb = query.LoadDataTable();

                return dtb;
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

                switch (param[0])
                {
                    case "clear":
                        var bed = new Bed();
                        bed.LoadByPrimaryKey(param[1]);

                        bool isAllowClear = false;
                        var reg = new Registration();
                        if (reg.LoadByPrimaryKey(bed.RegistrationNo))
                        {
                            if (reg.IsVoid == true || reg.DischargeDate != null || bed.BedID != reg.BedID)
                                isAllowClear = true;
                        }
                        else
                        {
                            isAllowClear = true;
                        }

                        if (isAllowClear && bed.SRBedStatus == AppSession.Parameter.BedStatusOccupied)
                        {
                            if (AppSession.Parameter.IsBedNeedCleanedProcess)
                            {
                                var bedStatusHistory = new BedStatusHistory();
                                bedStatusHistory.AddNew();
                                bedStatusHistory.BedID = bed.BedID;
                                bedStatusHistory.SRBedStatusFrom = AppSession.Parameter.BedStatusOccupied;
                                bedStatusHistory.SRBedStatusTo = AppSession.Parameter.BedStatusCleaning;
                                bedStatusHistory.RegistrationNo = bed.RegistrationNo;
                                bedStatusHistory.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                bedStatusHistory.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                bedStatusHistory.Save();
                                bed.SRBedStatus = AppSession.Parameter.BedStatusCleaning;
                            }
                            else
                            {
                                bed.SRBedStatus = AppSession.Parameter.BedStatusUnoccupied;
                            }
                            bed.RegistrationNo = string.Empty;
                            bed.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            bed.LastUpdateDateTime = DateTime.Now;

                            bed.Save();
                        }

                        grdList.Rebind();
                        break;

                    case "released":
                        var bedm1 = new BedManagement();
                        if (bedm1.LoadByPrimaryKey(param[1].ToInt()))
                        {
                            bedm1.IsReleased = true;
                            bedm1.ReleasedDateTime = DateTime.Now;
                            bedm1.ReleasedByUserID = AppSession.UserLogin.UserID;
                            bedm1.LastUpdateDateTime = DateTime.Now;
                            bedm1.LastUpdateByUserID = AppSession.UserLogin.UserID;

                            bedm1.Save();
                        }

                        grdBedManagement.Rebind();
                        break;

                    case "void":
                        var bedm2 = new BedManagement();
                        if (bedm2.LoadByPrimaryKey(param[1].ToInt()))
                        {
                            bedm2.IsVoid = true;
                            bedm2.VoidDateTime = DateTime.Now;
                            bedm2.VoidByUserID = AppSession.UserLogin.UserID;
                            bedm2.LastUpdateDateTime = DateTime.Now;
                            bedm2.LastUpdateByUserID = AppSession.UserLogin.UserID;

                            bedm2.Save();
                        }

                        grdBedManagement.Rebind();
                        break;
                }
            }
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboRoomID.Items.Clear();
            cboRoomID.Text = string.Empty;

            cboClassID.Items.Clear();
            cboClassID.Text = string.Empty;
        }

        protected void cboRoomID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RoomName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RoomID"].ToString();
        }

        protected void cboRoomID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new ServiceRoomQuery("a");
            var suq = new ServiceUnitQuery("b");
            query.InnerJoin(suq).On(suq.ServiceUnitID == query.ServiceUnitID &&
                                    suq.SRRegistrationType == AppConstant.RegistrationType.InPatient);
            query.Where(query.IsActive == true);
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
            query.Select(query.RoomID, query.RoomName);
            query.OrderBy(query.RoomID.Ascending);
            query.es.Top = 20;
            cboRoomID.DataSource = query.LoadDataTable();
            cboRoomID.DataBind();
        }

        protected void cboRoomID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboClassID.Items.Clear();
            cboClassID.Text = string.Empty;
        }

        protected void cboClassID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ClassName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ClassID"].ToString();
        }

        protected void cboClassID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new ClassQuery("a");
            var bedq = new BedQuery("b");
            query.InnerJoin(bedq).On(bedq.ClassID == query.ClassID);
            query.Where(query.IsActive == true, query.IsInPatientClass == true, bedq.IsActive == true);
            if (!string.IsNullOrEmpty(cboRoomID.SelectedValue))
                query.Where(bedq.RoomID == cboRoomID.SelectedValue);
            query.Select(query.ClassID, query.ClassName);
            query.OrderBy(query.ClassID.Ascending);
            query.es.Distinct = true;

            cboClassID.DataSource = query.LoadDataTable();
            cboClassID.DataBind();
        }

        public System.Drawing.Color GetColor(object srBedStatus)
        {
            System.Drawing.Color color = System.Drawing.Color.White;
            switch (srBedStatus.ToString())
            {
                case "BedStatus-01":
                    color = System.Drawing.Color.Green;
                    break;

                case "BedStatus-02":
                    color = System.Drawing.Color.Red;
                    break;

                case "BedStatus-03":
                    color = System.Drawing.Color.Brown;
                    break;

                case "BedStatus-04":
                    color = System.Drawing.Color.Orange;
                    break;

                case "BedStatus-05":
                    color = System.Drawing.Color.Yellow;
                    break;

                case "BedStatus-06":
                    color = System.Drawing.Color.Blue;
                    break;

                case "BedStatus-07":
                    color = System.Drawing.Color.Purple;
                    break;
            }

            return color;
        }
    }
}