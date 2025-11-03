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
    public partial class BedInformationStatusList : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.BedInformationStatus;

            if (!IsPostBack)
            {
                var unitColl = new ServiceUnitCollection();
                unitColl.Query.Where(unitColl.Query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                                     unitColl.Query.IsActive == true);
                unitColl.LoadAll();
                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit unit in unitColl)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(unit.ServiceUnitName, unit.ServiceUnitID));
                }

                StandardReference.InitializeIncludeSpace(cboBedStatus, AppEnum.StandardReference.BedStatus);
                StandardReference.InitializeIncludeSpace(cboGenderType, AppEnum.StandardReference.GenderType);
                StandardReference.InitializeIncludeSpace(cboSRReligion, AppEnum.StandardReference.Religion);
                chkIsRoomIn.Visible = AppSession.Parameter.IsUsingRoomingIn;
                grdList.Columns.FindByUniqueName("IsRoomIn").Visible = AppSession.Parameter.IsUsingRoomingIn;
                trGender.Visible = AppSession.Parameter.IsShowGenderOnBedInformationStatus;
                grdList.Columns.FindByUniqueName("Sex").Visible = AppSession.Parameter.IsShowGenderOnBedInformationStatus;
                grdList.Columns.FindByUniqueName("TemplateColumnBedRelease").Visible = this.IsPowerUser; //clear bed

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

                GetNumberOfBeds();
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
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
                var smf = new SmfQuery("smf");
                var sumInfo = new RegistrationInfoSumaryQuery("x");
                var cl = new ClassQuery("cl");
                var cl2 = new ClassQuery("cl2");

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
                query.LeftJoin(smf).On(smf.SmfID == rq.SmfID);
                query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo);
                query.LeftJoin(cl).On(cl.ClassID == rq.ChargeClassID);
                query.LeftJoin(cl2).On(cl2.ClassID == rq.CoverageClassID);

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
                        rq.IsClosed,
                        query.Notes.Coalesce("''").As("BedNotes"),
                        @"<CASE WHEN ISNULL(a.Notes, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsBedNotes>",
                        smf.SmfName.Coalesce("''").As("SmfName"),
                        smf.SmfBackcolor.Coalesce("''").As("SmfBackcolor"),
                        rq.Notes,
                        @"<CASE WHEN x.NoteCount <= 0 THEN NULL ELSE x.NoteCount END AS NoteCount>",
                        @"<CAST(cl.ClassSeq AS VARCHAR) AS ClassSeq1>",
                        @"<CAST(cl2.ClassSeq AS VARCHAR) AS ClassSeq2>"
                    );

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(srq.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboRoomID.SelectedValue != string.Empty)
                    query.Where(srq.RoomID == cboRoomID.SelectedValue);
                if (cboClassID.SelectedValue != string.Empty)
                    query.Where(query.ClassID == cboClassID.SelectedValue);
                //if (cboBedStatus.SelectedValue != string.Empty)
                //    query.Where(query.SRBedStatus == cboBedStatus.SelectedValue);
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

                    if (isDelete == false & !string.IsNullOrEmpty(cboGenderType.SelectedValue))
                    {
                        if (row["RoomGender"].ToString() != cboGenderType.SelectedValue)
                            isDelete = true;
                    }
                    if (isDelete)
                        row.Delete();

                    //    if (isDelete == false & rblGender.SelectedIndex != 0)
                    //    {
                    //        if (rblGender.SelectedIndex == 1 && row["RoomGender"].ToString() == "F")
                    //            isDelete = true;
                    //        else if (rblGender.SelectedIndex == 2 && row["RoomGender"].ToString() == "M")
                    //            isDelete = true;
                    //    }
                    //    if (isDelete)
                    //        row.Delete();
                    //}
                    //if (rblGender.SelectedIndex == 3 || rblGender.SelectedIndex == 4)
                    //{
                    //    var uf = dtb.AsEnumerable().Where(f => f.Field<string>("Sex") == (rblGender.SelectedIndex == 3 ? "F" : "M"))
                    //        .Select(f => f.Field<string>("Group")).ToArray().Distinct();
                    //    var ufR = dtb.AsEnumerable().Where(d => uf.Contains(d.Field<string>("Group")));
                    //    foreach (var u in ufR)
                    //    {
                    //        u.Delete();
                    //    }
                }
                dtb.AcceptChanges();

                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
            GetNumberOfBeds();
        }
        protected void btnFilter2_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            if (e.DetailTableView.Name.Equals("grdBedRoomIn"))
            {
                var qa = new BedRoomInQuery("a");
                var qb = new RegistrationQuery("b");
                var qc = new PatientQuery("c");
                var qd = new ParamedicQuery("d");
                var sal = new AppStandardReferenceItemQuery("sal");

                qa.InnerJoin(qb).On(qa.RegistrationNo == qb.RegistrationNo);
                qa.InnerJoin(qc).On(qb.PatientID == qc.PatientID);
                qa.InnerJoin(qd).On(qb.ParamedicID == qd.ParamedicID);
                qa.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qc.SRSalutation == sal.ItemID);

                qa.es.Top = AppSession.Parameter.MaxResultRecord;
                qa.Select
                (
                    qa.BedID,
                    qa.RegistrationNo,
                    qa.DateOfEntry,
                    qa.TimeOfEntry,
                    qc.MedicalNo,
                    qc.PatientName,
                    qd.ParamedicName,
                    qb.ChargeClassID,
                    qb.CoverageClassID,
                    @"<'' AS DefaultClassID>",
                    qb.ClassID,
                    sal.ItemName.As("SalutationName")
                );

                qa.Where(qa.IsVoid == false, qa.DateOfExit.IsNull());
                qa.Where(qa.BedID == e.DetailTableView.ParentItem.GetDataKeyValue("BedID").ToString());
                qa.OrderBy(qa.RegistrationNo.Ascending);

                e.DetailTableView.DataSource = qa.LoadDataTable();
            }
            else if (e.DetailTableView.Name.Equals("grdBedManagement"))
            {
                var qa = new BedManagementQuery("a");
                var qb = new PatientQuery("b");
                var std = new AppStandardReferenceItemQuery("c");
                var sal = new AppStandardReferenceItemQuery("sal");
                var res = new ReservationQuery("res");

                qa.LeftJoin(qb).On(qa.PatientID == qb.PatientID);
                qa.InnerJoin(std).On(
                    qa.SRBedStatus == std.ItemID &&
                    std.StandardReferenceID == AppEnum.StandardReference.BedStatus
                    );
                qa.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qb.SRSalutation == sal.ItemID);
                qa.LeftJoin(res).On(res.ReservationNo == qa.ReservationNo);

                qa.es.Top = AppSession.Parameter.MaxResultRecord;
                qa.Select
                (
                    qa.BedManagementID,
                    qa.TransactionDate,
                    qb.MedicalNo,
                    qa.RegistrationNo,
                    qb.PatientName,
                    @"<CASE WHEN b.FirstName IS NULL THEN RTRIM(RTRIM(a.FirstName + ' ' + a.MiddleName) + ' ' + a.LastName) ELSE RTRIM(RTRIM(b.FirstName + ' ' + b.MiddleName) + ' ' + b.LastName) END AS PatientName>",
                    @"<CASE WHEN b.StreetName IS NULL THEN RTRIM(RTRIM(a.StreetName + ' ' + a.City) + ' ' + a.County) ELSE RTRIM(RTRIM(b.StreetName + ' ' + b.City) + ' ' + b.County) END AS Address>",
                    qa.BedID,
                    qa.SRBedStatus,
                    std.ItemName.As("BedStatusName"),
                    @"<'' AS ChargeClassID>",
                    @"<'' AS CoverageClassID>",
                    @"<'' AS DefaultClassID>",
                    sal.ItemName.As("SalutationName")
                );

                qa.Where(qa.BedID == e.DetailTableView.ParentItem.GetDataKeyValue("BedID").ToString(), qa.IsVoid == false,
                    qa.Or(qa.IsReleased == false, res.ReservationDate > (new DateTime()).NowAtSqlServer()));
                qa.OrderBy(qa.TransactionDate.Ascending);

                e.DetailTableView.DataSource = qa.LoadDataTable();
            }
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var tooltip = string.Empty;
                var dataItem = e.Item as GridDataItem;
                if (dataItem["RegistrationNo"].Text != "&nbsp;")
                {
                    if (dataItem["ChargeClassID"].Text != dataItem["CoverageClassID"].Text)
                    {
                        // Beri warna merah jika CoverageClassID berbeda dg ChargeClassID Up, 
                        // Beri warna biru jika CoverageClassID berbeda dg ChargeClassID Down, 
                        var classSeq1 = dataItem["ClassSeq1"].Text.ToInt();
                        var classSeq2 = dataItem["ClassSeq2"].Text.ToInt();

                        dataItem.ForeColor = classSeq1 < classSeq2 ? Color.Red : Color.Blue;
                        dataItem.Font.Bold = true;
                        tooltip = "Charge class is different from coverage class.";
                    }
                    if (dataItem["ChargeClassID"].Text != dataItem["DefaultClassID"].Text)
                    {
                        var c = new Class();
                        c.LoadByPrimaryKey(dataItem["DefaultClassID"].Text);
                        if (c.IsTariffClass ?? false)
                        {
                            dataItem.Font.Bold = true;
                            dataItem.Font.Italic = true;
                            tooltip = tooltip == string.Empty ? "Charge class is different from bed class." : "Charge class is different from coverage and bed class.";
                        }
                    }
                    dataItem.ToolTip = tooltip;
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

                using (var trans = new esTransactionScope())
                {
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

                                if (!string.IsNullOrEmpty(bedm2.ReservationNo))
                                {
                                    var res = new Reservation();
                                    if (res.LoadByPrimaryKey(bedm2.ReservationNo))
                                    {
                                        res.SRReservationStatus = AppSession.Parameter.AppointmentStatusCancel;
                                        res.LastUpdateDateTime = DateTime.Now;
                                        res.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                        res.Save();
                                    }
                                }
                            }

                            break;
                    }
                    trans.Complete();
                }

                grdList.Rebind();
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
            string searchText = string.Format("%{0}%", e.Text);
            var query = new ServiceRoomQuery("a");
            var suq = new ServiceUnitQuery("b");
            query.InnerJoin(suq).On(suq.ServiceUnitID == query.ServiceUnitID &&
                                    suq.SRRegistrationType == AppConstant.RegistrationType.InPatient);

            query.Where(
                query.RoomName.Like(searchText),
                query.IsActive == true
                );

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

        protected void GetNumberOfBeds()
        {
            var query = new BedQuery("a");
            var srq = new ServiceRoomQuery("b");
            var suq = new ServiceUnitQuery("c");
            var cq = new ClassQuery("e");
            var asri = new AppStandardReferenceItemQuery("h");

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
            query.InnerJoin(asri).On
                (
                    query.SRBedStatus == asri.ItemID &&
                    asri.StandardReferenceID == AppEnum.StandardReference.BedStatus
                );

            if (cboServiceUnitID.SelectedValue != string.Empty)
                query.Where(srq.ServiceUnitID == cboServiceUnitID.SelectedValue);
            if (cboRoomID.SelectedValue != string.Empty)
                query.Where(srq.RoomID == cboRoomID.SelectedValue);
            if (cboClassID.SelectedValue != string.Empty)
                query.Where(query.ClassID == cboClassID.SelectedValue);
            query.Where(query.IsActive == true);

            query.Select(query.SRBedStatus, query.BedID.Count().As("NumberOfBeds"));
            query.GroupBy(query.SRBedStatus);
            DataTable dtb = query.LoadDataTable();

            int allBed = 0, ready = 0, occupied = 0, booked = 0, pending = 0, cleaning = 0, reserved = 0, repaired = 0;
            foreach (DataRow row in dtb.Rows)
            {
                allBed += row["NumberOfBeds"].ToInt();
                switch (row["SRBedStatus"].ToString())
                {
                    case "BedStatus-01":
                        ready += row["NumberOfBeds"].ToInt();
                        break;
                    case "BedStatus-02":
                        occupied += row["NumberOfBeds"].ToInt();
                        break;
                    case "BedStatus-03":
                        booked += row["NumberOfBeds"].ToInt();
                        break;
                    case "BedStatus-04":
                        pending += row["NumberOfBeds"].ToInt();
                        break;
                    case "BedStatus-05":
                        cleaning += row["NumberOfBeds"].ToInt();
                        break;
                    case "BedStatus-06":
                        reserved += row["NumberOfBeds"].ToInt();
                        break;
                    case "BedStatus-07":
                        repaired += row["NumberOfBeds"].ToInt();
                        break;
                }
            }
            lblNumberOfBeds.Text = "Number of Beds : " + allBed.ToString();
            txtReady.Value = Convert.ToDouble(ready);
            txtOccupied.Value = Convert.ToDouble(occupied);
            txtBooked.Value = Convert.ToDouble(booked);
            txtPending.Value = Convert.ToDouble(pending);
            txtCleaning.Value = Convert.ToDouble(cleaning);
            txtReserved.Value = Convert.ToDouble(reserved);
            txtRepaired.Value = Convert.ToDouble(repaired);
        }
    }
}
