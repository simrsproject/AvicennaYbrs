using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Drawing;

namespace Temiang.Avicenna.Module.BloodBank
{
    public partial class TransfusionMonitoringList : BasePage
    {      
        protected void Page_Init(object sender, EventArgs e)
        {           
            ProgramID = AppConstant.Program.BloodBankTransfusionMonitoring;

            if (!IsPostBack)
            {
                var query = new ServiceUnitQuery("a");
                var usr = new AppUserServiceUnitQuery("b");
                query.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID &&
                                        usr.ServiceUnitID == query.ServiceUnitID);
                query.Where(query.SRRegistrationType.In(AppConstant.RegistrationType.InPatient, AppConstant.RegistrationType.OutPatient, AppConstant.RegistrationType.EmergencyPatient), 
                    query.IsActive == true);
                query.OrderBy(query.ServiceUnitID.Ascending);
                
                var coll = new ServiceUnitCollection();
                coll.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in coll)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        protected void grdOutstandingList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdOutstandingList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = TransactionOutstandings;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                {
                    var dt = dataSource.AsEnumerable()
                    .OrderBy(x => x.Field<DateTime>("ReceivedDate"))
                    .ThenBy(x => x.Field<string>("TransactionNo"))
                    .ThenBy(x => x.Field<string>("BagNo"))
                    .ThenBy(x => x.Field<string>("FromRefer"));
                    grd.DataSource = dt.AsDataView().ToTable();
                }
                   
            }
            //if (!e.IsFromDetailTable)
            //{
            //    var dt = TransactionOutstandings.AsEnumerable()
            //        .OrderBy(x => x.Field<DateTime>("ReceivedDate"))
            //        .ThenBy(x => x.Field<string>("TransactionNo"))
            //        .ThenBy(x => x.Field<string>("BagNo"))
            //        .ThenBy(x => x.Field<string>("FromRefer"));
            //    grdOutstandingList.DataSource = dt.AsDataView().ToTable();

            //    //grdOutstandingList.DataSource = TransactionOutstandings;
            //}
        }

        protected void grdTransfusionList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdTransfusionList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = TransactionTransfusions;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                {
                    var dt = dataSource.AsEnumerable()
                    .OrderBy(x => x.Field<DateTime>("TransfusionStartDateTime"))
                    .ThenBy(x => x.Field<string>("TransactionNo"))
                    .ThenBy(x => x.Field<string>("BagNo"))
                    .ThenBy(x => x.Field<string>("FromRefer"));
                    grd.DataSource = dt.AsDataView().ToTable();
                }
            }

            //if (!e.IsFromDetailTable)
            //{
            //    var dt = TransactionTransfusions.AsEnumerable()
            //        .OrderBy(x => x.Field<DateTime>("TransfusionStartDateTime"))
            //        .ThenBy(x => x.Field<string>("TransactionNo"))
            //        .ThenBy(x => x.Field<string>("BagNo"))
            //        .ThenBy(x => x.Field<string>("FromRefer"));
            //    grdTransfusionList.DataSource = dt.AsDataView().ToTable();
                
            //    //grdTransfusionList.DataSource = TransactionTransfusions;
            //}
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdOutstandingList.Rebind();
            grdTransfusionList.Rebind();
        }

        private DataTable TransactionOutstandings
        {
            get
            {               
                var maxResultRecord = AppSession.Parameter.MaxResultRecord;
                var dtb = BloodBankTransactionOutstandings(maxResultRecord);
                maxResultRecord = maxResultRecord - dtb.Rows.Count;
                if (maxResultRecord > 0)
                {
                    var dtb2 = BloodBankTransactionOutstandingsReferTo(maxResultRecord);
                    dtb.Merge(dtb2);
                    maxResultRecord = maxResultRecord - dtb2.Rows.Count;
                }
                if (maxResultRecord > 0)
                {
                    var dtb3 = BloodBankTransactionOutstandingsOperatingRoom(maxResultRecord);
                    dtb.Merge(dtb3);
                }

                return dtb;
            }
        }

        private DataTable BloodBankTransactionOutstandings(int maxResultRecord)
        {
            var query = new BloodBankTransactionQuery("bb");
            var iquery = new BloodBankTransactionItemQuery("bbi");
            var qr = new RegistrationQuery("r");
            var qp = new PatientQuery("p");
            var qm = new ParamedicQuery("m");
            var unit = new ServiceUnitQuery("s");
            var room = new ServiceRoomQuery("d");
            var grr = new GuarantorQuery("c");
            var btype = new AppStandardReferenceItemQuery("bt");
            var bgroup = new AppStandardReferenceItemQuery("bg");
            var sal = new AppStandardReferenceItemQuery("sal");

            query.es.Top = maxResultRecord;

            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    query.RequestDate,
                    query.RequestTime,
                    query.BloodBankNo,
                    query.PdutNo,

                    iquery.BagNo,
                    iquery.ReceivedDate,
                    iquery.ReceivedTime,
                    bgroup.ItemName.As("BloodGroupReceived"),

                    query.RegistrationNo,
                    qr.RegistrationDate,
                    qp.MedicalNo,
                    qp.PatientName,
                    qp.Sex,
                    qm.ParamedicName,
                    unit.ServiceUnitName,
                    room.RoomName,
                    qr.BedID,
                    grr.GuarantorName,
                    sal.ItemName.As("SalutationName"),

                    btype.ItemName.As("BloodType"),
                    qp.BloodRhesus,
                    query.QtyBagRequest,
                    query.VolumeBag,
                    @"<'0' AS FromRefer>"
                );
            query.InnerJoin(iquery).On(iquery.TransactionNo == query.TransactionNo && iquery.IsVoid == false);
            query.InnerJoin(qr).On(qr.RegistrationNo == query.RegistrationNo);

            query.InnerJoin(qp).On(qp.PatientID == qr.PatientID);
            query.LeftJoin(qm).On(qm.ParamedicID == qr.ParamedicID);
            query.InnerJoin(unit).On(unit.ServiceUnitID == qr.ServiceUnitID);
            query.LeftJoin(room).On(room.RoomID == qr.RoomID);
            query.InnerJoin(grr).On(grr.GuarantorID == qr.GuarantorID);
            query.LeftJoin(btype).On(btype.StandardReferenceID == AppEnum.StandardReference.BloodType && btype.ItemID == qp.SRBloodType);
            query.LeftJoin(bgroup).On(bgroup.StandardReferenceID == AppEnum.StandardReference.BloodGroup && bgroup.ItemID == iquery.SRBloodGroupReceived);
            query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == qp.SRSalutation);

            var usr = new AppUserServiceUnitQuery("usr");
            query.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == unit.ServiceUnitID);

            if (!txtRequestDate1.IsEmpty)
                query.Where(query.RequestDate >= txtRequestDate1.SelectedDate);
            if (!txtRequestDate2.IsEmpty)
                query.Where(query.RequestDate <= txtRequestDate2.SelectedDate);
            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                query.Where(query.TransactionNo == txtTransactionNo.Text);
            if (!string.IsNullOrEmpty(txtBloodBankNo.Text))
                query.Where(query.Or(query.BloodBankNo == txtBloodBankNo.Text, query.PdutNo == txtBloodBankNo.Text));
            if (!string.IsNullOrEmpty(txtBagNo.Text))
                query.Where(iquery.BagNo == txtBagNo.Text);
            if (cboServiceUnitID.SelectedValue != string.Empty)
                query.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
            if (txtRegistrationNo.Text != string.Empty)
            {
                string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                if (AppSession.Parameter.IsMedicalNoContainStrip)
                    query.Where
                        (qr.Or
                             (
                                 string.Format("<bb.RegistrationNo = '{0}' OR >", searchReg),
                                 string.Format("<p.MedicalNo = '{0}' OR >", searchReg),
                                 string.Format("<p.OldMedicalNo = '{0}'>", searchReg),
                                 string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                                 string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                             )
                        );
                else
                    query.Where
                        (qr.Or
                             (
                                 string.Format("<bb.RegistrationNo = '{0}' OR >", searchReg),
                                 string.Format("<p.MedicalNo = '{0}' OR >", searchReg),
                                 string.Format("<p.OldMedicalNo = '{0}'>", searchReg),
                                 string.Format("< OR p.MedicalNo LIKE '%{0}%'>", searchReg),
                                 string.Format("< OR p.OldMedicalNo LIKE '%{0}%'>", searchReg)
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
            if (!txtReceived.IsEmpty)
                query.Where(iquery.ReceivedDate == txtReceived.SelectedDate);

            query.Where(query.IsApproved == true, iquery.IsProceedToTransfusion == true, iquery.TransfusionStartDateTime.IsNull());
            query.OrderBy(iquery.ReceivedDate.Ascending, query.TransactionNo.Ascending, iquery.BagNo.Ascending);

            DataTable tbl = query.LoadDataTable();

            return tbl;
        }

        private DataTable BloodBankTransactionOutstandingsReferTo(int maxResultRecord)
        {
            var query = new BloodBankTransactionQuery("bb");
            var iquery = new BloodBankTransactionItemQuery("bbi");
            var mb = new MergeBillingQuery("mb");
            var qr = new RegistrationQuery("r");
            var qp = new PatientQuery("p");
            var qm = new ParamedicQuery("m");
            var unit = new ServiceUnitQuery("s");
            var room = new ServiceRoomQuery("d");
            var grr = new GuarantorQuery("c");
            var btype = new AppStandardReferenceItemQuery("bt");
            var bgroup = new AppStandardReferenceItemQuery("bg");
            var sal = new AppStandardReferenceItemQuery("sal");

            query.es.Top = maxResultRecord;

            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    query.RequestDate,
                    query.RequestTime,
                    query.BloodBankNo,
                    query.PdutNo,

                    iquery.BagNo,
                    iquery.ReceivedDate,
                    iquery.ReceivedTime,
                    bgroup.ItemName.As("BloodGroupReceived"),

                    query.RegistrationNo,
                    qr.RegistrationDate,
                    qp.MedicalNo,
                    qp.PatientName,
                    qp.Sex,
                    qm.ParamedicName,
                    unit.ServiceUnitName,
                    room.RoomName,
                    qr.BedID,
                    grr.GuarantorName,
                    sal.ItemName.As("SalutationName"),

                    btype.ItemName.As("BloodType"),
                    qp.BloodRhesus,
                    query.QtyBagRequest,
                    query.VolumeBag,
                    @"<'1' AS FromRefer>"
                );
            query.InnerJoin(iquery).On(iquery.TransactionNo == query.TransactionNo && iquery.IsVoid == false);
            query.InnerJoin(mb).On(mb.FromRegistrationNo == query.RegistrationNo);
            query.InnerJoin(qr).On(qr.RegistrationNo == mb.RegistrationNo);
            query.InnerJoin(qp).On(qp.PatientID == qr.PatientID);
            query.LeftJoin(qm).On(qm.ParamedicID == qr.ParamedicID);
            query.InnerJoin(unit).On(unit.ServiceUnitID == qr.ServiceUnitID);
            query.LeftJoin(room).On(room.RoomID == qr.RoomID);
            query.InnerJoin(grr).On(grr.GuarantorID == qr.GuarantorID);
            query.LeftJoin(btype).On(btype.StandardReferenceID == AppEnum.StandardReference.BloodType && btype.ItemID == qp.SRBloodType);
            query.LeftJoin(bgroup).On(bgroup.StandardReferenceID == AppEnum.StandardReference.BloodGroup && bgroup.ItemID == iquery.SRBloodGroupReceived);
            query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == qp.SRSalutation);

            var usr = new AppUserServiceUnitQuery("usr");
            query.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == unit.ServiceUnitID);

            if (!txtRequestDate1.IsEmpty && !txtRequestDate2.IsEmpty)
                query.Where(query.RequestDate >= txtRequestDate1.SelectedDate, query.RequestDate <= txtRequestDate2.SelectedDate);
            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                query.Where(query.TransactionNo == txtTransactionNo.Text);
            if (!string.IsNullOrEmpty(txtBloodBankNo.Text))
                query.Where(query.Or(query.BloodBankNo == txtBloodBankNo.Text, query.PdutNo == txtBloodBankNo.Text));
            if (!string.IsNullOrEmpty(txtBagNo.Text))
                query.Where(iquery.BagNo == txtBagNo.Text);
            if (cboServiceUnitID.SelectedValue != string.Empty)
                query.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
            if (txtRegistrationNo.Text != string.Empty)
            {
                string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                query.Where
                    (qr.Or
                         (
                             string.Format("<bb.RegistrationNo = '{0}' OR >", searchReg),
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
            if (!txtReceived.IsEmpty)
                query.Where(iquery.ReceivedDate == txtReceived.SelectedDate);

            query.Where(query.IsApproved == true, iquery.IsProceedToTransfusion == true, iquery.TransfusionStartDateTime.IsNull());
            query.OrderBy(iquery.ReceivedDate.Ascending, query.TransactionNo.Ascending, iquery.BagNo.Ascending);

            DataTable tbl = query.LoadDataTable();

            return tbl;
        }

        private DataTable BloodBankTransactionOutstandingsOperatingRoom(int maxResultRecord)
        {
            var query = new BloodBankTransactionQuery("bb");
            var iquery = new BloodBankTransactionItemQuery("bbi");
            var qr = new RegistrationQuery("r");
            var qp = new PatientQuery("p");
            var qm = new ParamedicQuery("m");
            var unit = new ServiceUnitQuery("s");
            var room = new ServiceRoomQuery("d");
            var grr = new GuarantorQuery("c");
            var btype = new AppStandardReferenceItemQuery("bt");
            var bgroup = new AppStandardReferenceItemQuery("bg");
            var sal = new AppStandardReferenceItemQuery("sal");
            var sub = new ServiceUnitBookingQuery("sub");

            query.es.Top = maxResultRecord;

            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    query.RequestDate,
                    query.RequestTime,
                    query.BloodBankNo,
                    query.PdutNo,

                    iquery.BagNo,
                    iquery.ReceivedDate,
                    iquery.ReceivedTime,
                    bgroup.ItemName.As("BloodGroupReceived"),

                    query.RegistrationNo,
                    qr.RegistrationDate,
                    qp.MedicalNo,
                    qp.PatientName,
                    qp.Sex,
                    qm.ParamedicName,
                    unit.ServiceUnitName,
                    room.RoomName,
                    @"<'' AS BedID>",
                    grr.GuarantorName,
                    sal.ItemName.As("SalutationName"),

                    btype.ItemName.As("BloodType"),
                    qp.BloodRhesus,
                    query.QtyBagRequest,
                    query.VolumeBag,
                    @"<'1' AS FromRefer>"
                );
            query.InnerJoin(iquery).On(iquery.TransactionNo == query.TransactionNo && iquery.IsVoid == false);
            query.InnerJoin(sub).On(sub.RegistrationNo == query.RegistrationNo && sub.IsApproved == true);
            query.InnerJoin(qr).On(qr.RegistrationNo == query.RegistrationNo);
            query.InnerJoin(qp).On(qp.PatientID == qr.PatientID);
            query.LeftJoin(qm).On(qm.ParamedicID == qr.ParamedicID);
            query.InnerJoin(unit).On(unit.ServiceUnitID == sub.ServiceUnitID);
            query.LeftJoin(room).On(room.RoomID == sub.RoomID);
            query.InnerJoin(grr).On(grr.GuarantorID == qr.GuarantorID);
            query.LeftJoin(btype).On(btype.StandardReferenceID == AppEnum.StandardReference.BloodType && btype.ItemID == qp.SRBloodType);
            query.LeftJoin(bgroup).On(bgroup.StandardReferenceID == AppEnum.StandardReference.BloodGroup && bgroup.ItemID == iquery.SRBloodGroupReceived);
            query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == qp.SRSalutation);

            var usr = new AppUserServiceUnitQuery("usr");
            query.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == unit.ServiceUnitID);

            if (!txtRequestDate1.IsEmpty && !txtRequestDate2.IsEmpty)
                query.Where(query.RequestDate >= txtRequestDate1.SelectedDate, query.RequestDate <= txtRequestDate2.SelectedDate);
            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                query.Where(query.TransactionNo == txtTransactionNo.Text);
            if (!string.IsNullOrEmpty(txtBloodBankNo.Text))
                query.Where(query.Or(query.BloodBankNo == txtBloodBankNo.Text, query.PdutNo == txtBloodBankNo.Text));
            if (!string.IsNullOrEmpty(txtBagNo.Text))
                query.Where(iquery.BagNo == txtBagNo.Text);
            if (cboServiceUnitID.SelectedValue != string.Empty)
                query.Where(room.ServiceUnitID == cboServiceUnitID.SelectedValue);
            if (txtRegistrationNo.Text != string.Empty)
            {
                string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                query.Where
                    (qr.Or
                         (
                             string.Format("<bb.RegistrationNo = '{0}' OR >", searchReg),
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
            if (!txtReceived.IsEmpty)
                query.Where(iquery.ReceivedDate == txtReceived.SelectedDate);

            query.Where(query.IsApproved == true, iquery.IsProceedToTransfusion == true, iquery.TransfusionStartDateTime.IsNull());
            query.OrderBy(iquery.ReceivedDate.Ascending, query.TransactionNo.Ascending, iquery.BagNo.Ascending);

            DataTable tbl = query.LoadDataTable();

            return tbl;
        }

        private DataTable TransactionTransfusions
        {
            get
            {                
                var maxResultRecord = AppSession.Parameter.MaxResultRecord;
                var dtb = BloodBankTransactionTransfusions(maxResultRecord);
                maxResultRecord = maxResultRecord - dtb.Rows.Count;
                if (maxResultRecord > 0)
                {
                    var dtb2 = BloodBankTransactionTransfusionsReferTo(maxResultRecord);
                    dtb.Merge(dtb2);
                    maxResultRecord = maxResultRecord - dtb2.Rows.Count;
                }
                if (maxResultRecord > 0)
                {
                    var dtb3 = BloodBankTransactionTransfusionsOperatingRoom(maxResultRecord);
                    dtb.Merge(dtb3);
                }

                return dtb;
            }
        }

        private DataTable BloodBankTransactionTransfusions(int maxResultRecord)
        {
            var query = new BloodBankTransactionQuery("bb");
            var iquery = new BloodBankTransactionItemQuery("bbi");
            var qr = new RegistrationQuery("r");
            var qp = new PatientQuery("p");
            var qm = new ParamedicQuery("m");
            var unit = new ServiceUnitQuery("s");
            var room = new ServiceRoomQuery("d");
            var grr = new GuarantorQuery("c");
            var btype = new AppStandardReferenceItemQuery("bt");
            var bgroup = new AppStandardReferenceItemQuery("bg");
            var sal = new AppStandardReferenceItemQuery("sal");

            query.es.Top = maxResultRecord;

            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    query.RequestDate,
                    query.RequestTime,
                    query.BloodBankNo,
                    query.PdutNo,

                    iquery.BagNo,
                    iquery.ReceivedDate,
                    iquery.ReceivedTime,
                    iquery.TransfusionStartDateTime,
                    iquery.TransfusionEndDateTime,
                    bgroup.ItemName.As("BloodGroupReceived"),

                    query.RegistrationNo,
                    qr.RegistrationDate,
                    qp.MedicalNo,
                    qp.PatientName,
                    qp.Sex,
                    qm.ParamedicName,
                    unit.ServiceUnitName,
                    room.RoomName,
                    qr.BedID,
                    grr.GuarantorName,
                    sal.ItemName.As("SalutationName"),

                    btype.ItemName.As("BloodType"),
                    qp.BloodRhesus,
                    query.QtyBagRequest,
                    query.VolumeBag,

                    "<'TransfusionMonitoringDetail.aspx?md=view&id='+bb.TransactionNo+'&bagno='+bbi.BagNo+'&regno='+bb.RegistrationNo AS TransfusionUrl>",
                    @"<'0' AS FromRefer>"
                );
            query.InnerJoin(iquery).On(iquery.TransactionNo == query.TransactionNo);
            query.InnerJoin(qr).On(qr.RegistrationNo == query.RegistrationNo);
            query.InnerJoin(qp).On(qp.PatientID == qr.PatientID);
            query.LeftJoin(qm).On(qm.ParamedicID == qr.ParamedicID);
            query.InnerJoin(unit).On(unit.ServiceUnitID == qr.ServiceUnitID);
            query.LeftJoin(room).On(room.RoomID == qr.RoomID);
            query.InnerJoin(grr).On(grr.GuarantorID == qr.GuarantorID);
            query.LeftJoin(btype).On(btype.StandardReferenceID == AppEnum.StandardReference.BloodType && btype.ItemID == qp.SRBloodType);
            query.LeftJoin(bgroup).On(bgroup.StandardReferenceID == AppEnum.StandardReference.BloodGroup && bgroup.ItemID == iquery.SRBloodGroupReceived);
            query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == qp.SRSalutation);

            var usr = new AppUserServiceUnitQuery("usr");
            query.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == unit.ServiceUnitID);

            if (!txtRequestDate1.IsEmpty && !txtRequestDate2.IsEmpty)
                query.Where(query.RequestDate >= txtRequestDate1.SelectedDate, query.RequestDate <= txtRequestDate2.SelectedDate);
            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                query.Where(query.TransactionNo == txtTransactionNo.Text);
            if (!string.IsNullOrEmpty(txtBloodBankNo.Text))
                query.Where(query.Or(query.BloodBankNo == txtBloodBankNo.Text, query.PdutNo == txtBloodBankNo.Text));
            if (!string.IsNullOrEmpty(txtBagNo.Text))
                query.Where(iquery.BagNo == txtBagNo.Text);
            if (cboServiceUnitID.SelectedValue != string.Empty)
                query.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
            if (txtRegistrationNo.Text != string.Empty)
            {
                string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                query.Where
                    (qr.Or
                         (
                             string.Format("<bb.RegistrationNo = '{0}' OR >", searchReg),
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
            if (!txtTransfusionDate.IsEmpty)
                query.Where(iquery.TransfusionStartDateTime.Date() == txtTransfusionDate.SelectedDate);

            query.Where(query.IsApproved == true, iquery.IsProceedToTransfusion == true, iquery.TransfusionStartDateTime.IsNotNull());
            query.OrderBy(iquery.TransfusionStartDateTime.Ascending, query.TransactionNo.Ascending, iquery.BagNo.Ascending);

            DataTable tbl = query.LoadDataTable();

            return tbl;
        }

        private DataTable BloodBankTransactionTransfusionsReferTo(int maxResultRecord)
        {
            var query = new BloodBankTransactionQuery("bb");
            var iquery = new BloodBankTransactionItemQuery("bbi");
            var mb = new MergeBillingQuery("mb");
            var qr = new RegistrationQuery("r");
            var qp = new PatientQuery("p");
            var qm = new ParamedicQuery("m");
            var unit = new ServiceUnitQuery("s");
            var room = new ServiceRoomQuery("d");
            var grr = new GuarantorQuery("c");
            var btype = new AppStandardReferenceItemQuery("bt");
            var bgroup = new AppStandardReferenceItemQuery("bg");
            var sal = new AppStandardReferenceItemQuery("sal");

            query.es.Top = maxResultRecord;

            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    query.RequestDate,
                    query.RequestTime,
                    query.BloodBankNo,
                    query.PdutNo,

                    iquery.BagNo,
                    iquery.ReceivedDate,
                    iquery.ReceivedTime,
                    iquery.TransfusionStartDateTime,
                    iquery.TransfusionEndDateTime,
                    bgroup.ItemName.As("BloodGroupReceived"),

                    query.RegistrationNo,
                    qr.RegistrationDate,
                    qp.MedicalNo,
                    qp.PatientName,
                    qp.Sex,
                    qm.ParamedicName,
                    unit.ServiceUnitName,
                    room.RoomName,
                    qr.BedID,
                    grr.GuarantorName,
                    sal.ItemName.As("SalutationName"),

                    btype.ItemName.As("BloodType"),
                    qp.BloodRhesus,
                    query.QtyBagRequest,
                    query.VolumeBag,

                    "<'TransfusionMonitoringDetail.aspx?md=view&id='+bb.TransactionNo+'&bagno='+bbi.BagNo+'&regno='+bb.RegistrationNo AS TransfusionUrl>",
                    @"<'1' AS FromRefer>"
                );
            query.InnerJoin(iquery).On(iquery.TransactionNo == query.TransactionNo);
            query.InnerJoin(mb).On(mb.FromRegistrationNo == query.RegistrationNo);
            query.InnerJoin(qr).On(qr.RegistrationNo == mb.RegistrationNo);
            query.InnerJoin(qp).On(qp.PatientID == qr.PatientID);
            query.LeftJoin(qm).On(qm.ParamedicID == qr.ParamedicID);
            query.InnerJoin(unit).On(unit.ServiceUnitID == qr.ServiceUnitID);
            query.LeftJoin(room).On(room.RoomID == qr.RoomID);
            query.InnerJoin(grr).On(grr.GuarantorID == qr.GuarantorID);
            query.LeftJoin(btype).On(btype.StandardReferenceID == AppEnum.StandardReference.BloodType && btype.ItemID == qp.SRBloodType);
            query.LeftJoin(bgroup).On(bgroup.StandardReferenceID == AppEnum.StandardReference.BloodGroup && bgroup.ItemID == iquery.SRBloodGroupReceived);
            query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == qp.SRSalutation);

            var usr = new AppUserServiceUnitQuery("usr");
            query.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == unit.ServiceUnitID);

            if (!txtRequestDate1.IsEmpty && !txtRequestDate2.IsEmpty)
                query.Where(query.RequestDate >= txtRequestDate1.SelectedDate, query.RequestDate <= txtRequestDate2.SelectedDate);
            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                query.Where(query.TransactionNo == txtTransactionNo.Text);
            if (!string.IsNullOrEmpty(txtBloodBankNo.Text))
                query.Where(query.Or(query.BloodBankNo == txtBloodBankNo.Text, query.PdutNo == txtBloodBankNo.Text));
            if (!string.IsNullOrEmpty(txtBagNo.Text))
                query.Where(iquery.BagNo == txtBagNo.Text);
            if (cboServiceUnitID.SelectedValue != string.Empty)
                query.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
            if (txtRegistrationNo.Text != string.Empty)
            {
                string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                query.Where
                    (qr.Or
                         (
                             string.Format("<bb.RegistrationNo = '{0}' OR >", searchReg),
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
            if (!txtTransfusionDate.IsEmpty)
                query.Where(iquery.TransfusionStartDateTime.Date() == txtTransfusionDate.SelectedDate);

            query.Where(query.IsApproved == true, iquery.IsProceedToTransfusion == true, iquery.TransfusionStartDateTime.IsNotNull());
            query.OrderBy(iquery.TransfusionStartDateTime.Ascending, query.TransactionNo.Ascending, iquery.BagNo.Ascending);

            DataTable tbl = query.LoadDataTable();

            return tbl;
        }

        private DataTable BloodBankTransactionTransfusionsOperatingRoom(int maxResultRecord)
        {
            var query = new BloodBankTransactionQuery("bb");
            var iquery = new BloodBankTransactionItemQuery("bbi");
            var qr = new RegistrationQuery("r");
            var qp = new PatientQuery("p");
            var qm = new ParamedicQuery("m");
            var unit = new ServiceUnitQuery("s");
            var room = new ServiceRoomQuery("d");
            var grr = new GuarantorQuery("c");
            var btype = new AppStandardReferenceItemQuery("bt");
            var bgroup = new AppStandardReferenceItemQuery("bg");
            var sal = new AppStandardReferenceItemQuery("sal");
            var sub = new ServiceUnitBookingQuery("sub");

            query.es.Top = maxResultRecord;

            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    query.RequestDate,
                    query.RequestTime,
                    query.BloodBankNo,
                    query.PdutNo,

                    iquery.BagNo,
                    iquery.ReceivedDate,
                    iquery.ReceivedTime,
                    iquery.TransfusionStartDateTime,
                    iquery.TransfusionEndDateTime,
                    bgroup.ItemName.As("BloodGroupReceived"),

                    query.RegistrationNo,
                    qr.RegistrationDate,
                    qp.MedicalNo,
                    qp.PatientName,
                    qp.Sex,
                    qm.ParamedicName,
                    unit.ServiceUnitName,
                    room.RoomName,
                    qr.BedID,
                    grr.GuarantorName,
                    sal.ItemName.As("SalutationName"),

                    btype.ItemName.As("BloodType"),
                    qp.BloodRhesus,
                    query.QtyBagRequest,
                    query.VolumeBag,

                    "<'TransfusionMonitoringDetail.aspx?md=view&id='+bb.TransactionNo+'&bagno='+bbi.BagNo+'&regno='+bb.RegistrationNo AS TransfusionUrl>",
                    @"<'1' AS FromRefer>"
                );
            query.InnerJoin(iquery).On(iquery.TransactionNo == query.TransactionNo);
            query.InnerJoin(sub).On(sub.RegistrationNo == query.RegistrationNo && sub.IsApproved == true);
            query.InnerJoin(qr).On(qr.RegistrationNo == query.RegistrationNo);
            query.InnerJoin(qp).On(qp.PatientID == qr.PatientID);
            query.LeftJoin(qm).On(qm.ParamedicID == qr.ParamedicID);
            query.InnerJoin(unit).On(unit.ServiceUnitID == sub.ServiceUnitID);
            query.LeftJoin(room).On(room.RoomID == sub.RoomID);
            query.InnerJoin(grr).On(grr.GuarantorID == qr.GuarantorID);
            query.LeftJoin(btype).On(btype.StandardReferenceID == AppEnum.StandardReference.BloodType && btype.ItemID == qp.SRBloodType);
            query.LeftJoin(bgroup).On(bgroup.StandardReferenceID == AppEnum.StandardReference.BloodGroup && bgroup.ItemID == iquery.SRBloodGroupReceived);
            query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == qp.SRSalutation);

            var usr = new AppUserServiceUnitQuery("usr");
            query.InnerJoin(usr).On(usr.UserID == AppSession.UserLogin.UserID && usr.ServiceUnitID == unit.ServiceUnitID);

            if (!txtRequestDate1.IsEmpty && !txtRequestDate2.IsEmpty)
                query.Where(query.RequestDate >= txtRequestDate1.SelectedDate, query.RequestDate <= txtRequestDate2.SelectedDate);
            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                query.Where(query.TransactionNo == txtTransactionNo.Text);
            if (!string.IsNullOrEmpty(txtBloodBankNo.Text))
                query.Where(query.Or(query.BloodBankNo == txtBloodBankNo.Text, query.PdutNo == txtBloodBankNo.Text));
            if (!string.IsNullOrEmpty(txtBagNo.Text))
                query.Where(iquery.BagNo == txtBagNo.Text);
            if (cboServiceUnitID.SelectedValue != string.Empty)
                query.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
            if (txtRegistrationNo.Text != string.Empty)
            {
                string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                query.Where
                    (qr.Or
                         (
                             string.Format("<bb.RegistrationNo = '{0}' OR >", searchReg),
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
            if (!txtTransfusionDate.IsEmpty)
                query.Where(iquery.TransfusionStartDateTime.Date() == txtTransfusionDate.SelectedDate);

            query.Where(query.IsApproved == true, iquery.IsProceedToTransfusion == true, iquery.TransfusionStartDateTime.IsNotNull());
            query.OrderBy(iquery.TransfusionStartDateTime.Ascending, query.TransactionNo.Ascending, iquery.BagNo.Ascending);

            DataTable tbl = query.LoadDataTable();

            return tbl;
        }

        protected void grdOutstandingList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string tno = dataItem.GetDataKeyValue("TransactionNo").ToString();

            var query = new BloodBankTransactionQuery("a");
            var reg = new RegistrationQuery("b");
            var pat = new PatientQuery("c");
            var btype = new AppStandardReferenceItemQuery("d");
            var bgroup = new AppStandardReferenceItemQuery("e");
            var usr = new AppUserQuery("f");

            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            query.LeftJoin(btype).On(pat.SRBloodType == btype.ItemID &&
                                     btype.StandardReferenceID == AppEnum.StandardReference.BloodType);
            query.LeftJoin(bgroup).On(query.SRBloodGroupRequest == bgroup.ItemID &&
                                      bgroup.StandardReferenceID == AppEnum.StandardReference.BloodGroup);
            query.InnerJoin(usr).On(query.OfficerByUserID == usr.UserID);
            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    query.RequestDate,
                    query.RequestTime,
                    query.BloodBankNo,
                    query.PdutNo,
                    btype.ItemName.As("BloodType"),
                    pat.BloodRhesus,
                    bgroup.ItemName.As("BloodGroup"),
                    query.QtyBagRequest,
                    query.VolumeBag,
                    usr.UserName.As("Officer")
                );

            query.Where(query.TransactionNo == tno);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected void grdTransfusionList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string tno = dataItem.GetDataKeyValue("TransactionNo").ToString();
            string bagno = dataItem.GetDataKeyValue("BagNo").ToString();

            var query = new BloodBankTransactionQuery("a");
            var iquery = new BloodBankTransactionItemQuery("ia");
            var reg = new RegistrationQuery("b");
            var pat = new PatientQuery("c");
            var btype = new AppStandardReferenceItemQuery("d");
            var bgroup = new AppStandardReferenceItemQuery("e");
            var usr = new AppUserQuery("f");

            query.InnerJoin(iquery).On(query.TransactionNo == iquery.TransactionNo);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            query.LeftJoin(btype).On(pat.SRBloodType == btype.ItemID &&
                                     btype.StandardReferenceID == AppEnum.StandardReference.BloodType);
            query.LeftJoin(bgroup).On(query.SRBloodGroupRequest == bgroup.ItemID &&
                                      bgroup.StandardReferenceID == AppEnum.StandardReference.BloodGroup);
            query.InnerJoin(usr).On(query.OfficerByUserID == usr.UserID);
            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    query.RequestDate,
                    query.RequestTime,
                    query.BloodBankNo,
                    query.PdutNo,
                    btype.ItemName.As("BloodType"),
                    pat.BloodRhesus,
                    bgroup.ItemName.As("BloodGroup"),
                    query.QtyBagRequest,
                    query.VolumeBag,
                    usr.UserName.As("Officer"),

                    iquery.BagNo,
                    iquery.ReceivedDate,
                    iquery.ReceivedTime
                );

            query.Where(query.TransactionNo == tno, iquery.BagNo == bagno);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected void grdOutstandingList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var tooltip = string.Empty;
                var dataItem = e.Item as GridDataItem;
                if (dataItem.OwnerTableView.Name == "master")
                {
                    if (dataItem["FromRefer"].Text == "1")
                    {
                        dataItem.ForeColor = Color.DarkBlue;
                        //dataItem.Font.Italic = true;
                    }
                }
            }
        }

        protected void grdTransfusionList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var tooltip = string.Empty;
                var dataItem = e.Item as GridDataItem;
                if (dataItem.OwnerTableView.Name == "master")
                {
                    if (dataItem["FromRefer"].Text == "1")
                    {
                        dataItem.ForeColor = Color.DarkBlue;
                        //dataItem.Font.Italic = true;
                    }
                }
            }
        }
    }
}
