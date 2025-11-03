using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.RADT
{
    /// <summary>
    /// PHR untuk diakses oleh umum terutama oleh selain PPRA
    /// QuestionForm yg dimunculkan berdasarkan UserType nya
    /// </summary>
    /// Create By: Handono
    /// Create Date: 2003-03
    /// Client Req: RSYS Padang
    /// -----------------------
    public partial class PhrList : BasePage
    {

        private string _patientId = string.Empty;
        private string _registrationNo = string.Empty;
        private string _serviceUnitId = string.Empty;



        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PatientHealthRecord;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                grdList.DataSource = string.Empty;
                RestoreValueFromCookie();
            }
        }
        protected void btnSearchPatient_Click(object sender, ImageClickEventArgs e)
        {
            grdList.DataSource = GetPatients();
            grdList.CurrentPageIndex = 0;
            grdList.Rebind();
        }
        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = GetPatients();
        }
        private DataTable GetPatients()
        {
            var searchPatient = Helper.EscapeQuery(txtPatientSearch.Text.Trim());
            string dateOfBirth = txtDateOfBirth.IsEmpty
                ? string.Empty
                : txtDateOfBirth.SelectedDate.Value.ToShortDateString();
            string phoneNo = Helper.EscapeQuery(txtPhoneNo.Text);
            string address = Helper.EscapeQuery(txtAddress.Text);
            byte isValidateByZipCode = IsValidateByZipCode;

            var qb = new HealthcareQuery("b");
            var qr = new PatientQuery("a");
            var qc = new AppStandardReferenceItemQuery("c");
            qr.LeftJoin(qb).On(qr.HealthcareID == qb.HealthcareID);
            qr.LeftJoin(qc).On(qc.StandardReferenceID == "Salutation" && qc.ItemID == qr.SRSalutation);

            qr.es.Top = AppSession.Parameter.MaxResultRecord;
            qr.Select(
                qr.PatientID,
                qr.MedicalNo,
                qr.OldMedicalNo,
                qr.PatientName,
                qr.Sex,
                qr.Address,
                qr.PhoneNo,
                qr.MobilePhoneNo,
                qr.DateOfBirth,
                qb.HealthcareName,
                qr.IsBlackList,
                qr.IsNotPaidOff,
                qr.IsAlive,
                qc.ItemName.As("Salutation"),
                qr.ZipCode.Coalesce("''"), string.Format("<CAST({0} AS BIT) AS IsValidateByZipCode>",
                isValidateByZipCode), qr.IsStoredToLokadok, qr.IsActive
                );
            qr.Where(//qr.IsActive == 1, 
                qr.IsNonPatient == false);

            if (!string.IsNullOrEmpty(dateOfBirth))
                qr.Where(qr.DateOfBirth == dateOfBirth);

            if (!string.IsNullOrEmpty(phoneNo))
                qr.Where(
                    qr.Or(
                        qr.PhoneNo == phoneNo,
                        qr.MobilePhoneNo == phoneNo
                        )
                    );

            if (searchPatient != string.Empty)
            {
                //string searchPatient = "%" + txtPatientName.Text + "%";

                string sNumber = searchPatient.Replace("-", "").Replace("/", "");
                int n;
                bool isNumeric = int.TryParse(sNumber, out n);
                if (isNumeric)
                {
                    // for fast search: numeric is medical no
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        qr.Where(qr.Or(
                            string.Format("<REPLACE(a.MedicalNo, '-', '') LIKE '%{0}%'>", sNumber),
                            string.Format("< OR REPLACE(a.OldMedicalNo, '-', '') LIKE '%{0}%'>", sNumber)));
                    else
                        qr.Where(qr.Or(
                            string.Format("< a.MedicalNo LIKE '%{0}%'>", sNumber),
                            string.Format("< OR a.OldMedicalNo LIKE '%{0}%'>", sNumber)));
                }
                else
                {
                    string searchTextContain = string.Format("%{0}%", searchPatient);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        qr.Where(
                            qr.Or(
                                qr.MedicalNo.Like(searchTextContain),
                                qr.OldMedicalNo.Like(searchTextContain),
                                string.Format("< OR LTRIM(RTRIM(LTRIM(a.FirstName + ' ' + a.MiddleName)) + ' ' + a.LastName) LIKE '%{0}%'>", searchPatient),
                                string.Format("< OR REPLACE(a.MedicalNo, '-', '') LIKE '%{0}%'>", searchPatient),
                                string.Format("< OR REPLACE(a.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchPatient)
                            )
                        );
                    else
                        qr.Where(
                            qr.Or(
                                qr.MedicalNo.Like(searchTextContain),
                                qr.OldMedicalNo.Like(searchTextContain),
                                string.Format("< OR LTRIM(RTRIM(LTRIM(a.FirstName + ' ' + a.MiddleName)) + ' ' + a.LastName) LIKE '%{0}%'>", searchPatient),
                                string.Format("< OR a.MedicalNo LIKE '%{0}%'>", searchPatient),
                                string.Format("< OR a.OldMedicalNo LIKE '%{0}%'>", searchPatient)
                            )
                        );
                }
            }

            if (!string.IsNullOrEmpty(address))
            {
                if (address.Trim().Contains(" "))
                {
                    var searchs = address.Split(' ');
                    foreach (var searchLike in searchs.Select(search => "%" + search + "%"))
                    {
                        qr.Where(
                            qr.Or(
                                qr.StreetName.Like(searchLike),
                                qr.City.Like(searchLike),
                                qr.County.Like(searchLike),
                                qr.ZipCode.Like(searchLike)
                                )
                            );
                    }
                }
                else
                {
                    var searchLike = "%" + address + "%";
                    qr.Where(
                        qr.Or(
                                qr.StreetName.Like(searchLike),
                                qr.City.Like(searchLike),
                                qr.County.Like(searchLike),
                                qr.ZipCode.Like(searchLike)
                            )
                        );
                }
            }

            qr.OrderBy(searchPatient != string.Empty ? qr.FirstName.Ascending : qr.MedicalNo.Descending);

            return qr.LoadDataTable();
        }
        private byte IsValidateByZipCode
        {
            get
            {
                var app = AppSession.Parameter.TablePatientFieldValidation;
                if (string.IsNullOrEmpty(app)) return 0;
                if (app.Contains("ZipCode")) return 1;
                return 0;
            }
        }



        protected void grdList_ItemCreated(object sender, GridItemEventArgs e)
        {
            var gridNestedViewItem = e.Item as GridNestedViewItem;
            if (gridNestedViewItem != null)
            {
                e.Item.FindControl("pnlRegistration").Visible = (gridNestedViewItem).ParentItem.Expanded;
            }
        }

        protected void grdList_OnItemCommand(object source, GridCommandEventArgs e)
        {
            var isVisible = false;
            if (e.CommandName == RadGrid.ExpandCollapseCommandName && e.Item is GridDataItem)
            {
                isVisible = !e.Item.Expanded;
                ((GridDataItem)e.Item).ChildItem.FindControl("pnlRegistration").Visible = isVisible;

                _patientId = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PatientID"]);
            }

            if (isVisible)
            {
                var grd = (RadGrid)((GridDataItem)e.Item).ChildItem.FindControl("grdRegistration");
                grd.InitializeCultureGrid();

                var reg = new RegistrationQuery("reg");
                var room = new ServiceRoomQuery("c");
                reg.LeftJoin(room).On(reg.RoomID == room.RoomID);

                var medic = new ParamedicQuery("m");
                reg.InnerJoin(medic).On(reg.ParamedicID == medic.ParamedicID);

                var unit = new ServiceUnitQuery("b");
                reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);

                var grr = new GuarantorQuery("g");
                reg.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);

                reg.Select
                    (
                        room.RoomName,
                        reg.RegistrationDate,
                        unit.ServiceUnitID,
                        unit.ServiceUnitName,
                        medic.ParamedicName,
                        reg.RegistrationNo,
                        grr.GuarantorName,
                        reg.PatientID,
                        reg.IsConsul,
                        reg.SRRegistrationType,
                        reg.RoomID,
                        reg.RegistrationTime,
                        reg.BedID
                    );

                reg.Where(reg.PatientID == _patientId);
                reg.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationNo.Descending);

                grd.DataSource = reg.LoadDataTable();
                grd.Rebind();

                _patientId = string.Empty;
            }
        }


        #region grid Registration
        protected void grdRegistration_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            var isVisible = false;
            if (e.CommandName == RadGrid.ExpandCollapseCommandName && e.Item is GridDataItem)
            {
                isVisible = !e.Item.Expanded;
                ((GridDataItem)e.Item).ChildItem.FindControl("pnlPhr").Visible = isVisible;

                _serviceUnitId = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ServiceUnitID"]);
                _patientId = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PatientID"]);
                _registrationNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RegistrationNo"]);
            }

            if (isVisible)
            {
                var childItem = ((GridDataItem)e.Item).ChildItem;

                var grd = (RadGrid)childItem.FindControl("grdPhr");
                var tbarPhr = (RadToolBar)childItem.FindControl("tbarPhr");

                PopulatePhr(grd, tbarPhr, _serviceUnitId, _patientId, _registrationNo, grd.ClientID);

                _serviceUnitId = string.Empty;
                _patientId = string.Empty;
                _registrationNo = string.Empty;
            }
        }
        protected void grdRegistration_OnItemCreated(object sender, GridItemEventArgs e)
        {
            var gridNestedViewItem = e.Item as GridNestedViewItem;
            if (gridNestedViewItem != null)
            {
                e.Item.FindControl("pnlPhr").Visible = (gridNestedViewItem).ParentItem.Expanded;
            }
        }
        #endregion


        #region Grid Phr
        private DataTable QuestionFormDatatable(string serviceUnitID, string patientID, string registrationNo)
        {
            var isNewPatient = true;
            // Check status new patient at clinic
            // Lebih tepat jika PatientAssessment baru 1 di Service Unit terpilih tapi akan bermasalah jika assessment dihapus
            var regColl = new RegistrationCollection();
            var regQuery = new RegistrationQuery();
            regQuery.Where(regQuery.PatientID == patientID, regQuery.ServiceUnitID == serviceUnitID, regQuery.RegistrationNo < registrationNo, regQuery.IsVoid == false);
            regQuery.es.Top = 1;
            if (regColl.Load(regQuery))
            {
                isNewPatient = regColl.Count == 0;
            }

            var query = new QuestionFormQuery("a");
            var suQr = new QuestionFormInServiceUnitQuery("s");

            query.InnerJoin(suQr).On(query.QuestionFormID == suQr.QuestionFormID && suQr.ServiceUnitID == serviceUnitID);
            if (isNewPatient)
                query.Where(query.IsActive == true && query.IsInitialAssessment == true);
            else
                query.Where(query.IsActive == true && query.IsContinuedAssessment == true);

            // Berdasarkan Form Type
            query.Where(query.Or(query.SRQuestionFormType.IsNull(), query.SRQuestionFormType == string.Empty,
                query.SRQuestionFormType == QuestionForm.QuestionFormType.PatientTransfer,
                query.SRQuestionFormType == QuestionForm.QuestionFormType.General));

            // Berdasarkan tipe user
            query.Where(query.Or(query.RestrictionUserType.IsNull(), query.RestrictionUserType == string.Empty,
                query.RestrictionUserType.Like("%" + AppSession.UserLogin.SRUserType + "%")));

            query.Select(string.Format("<'{0}' as registrationNo>", registrationNo),
                query.QuestionFormID,
                query.QuestionFormName,
                query.IsSingleEntry.Coalesce("0").As("IsSingleEntry"), @"<CAST(1 AS BIT) AS IsNewEnable>");

            query.OrderBy(query.QuestionFormName.Ascending);

            var dtb = query.LoadDataTable();
            foreach (DataRow row in dtb.Rows)
            {
                if (Convert.ToBoolean(row["IsSingleEntry"]))
                {
                    var phr = new PatientHealthRecordCollection();
                    phr.Query.Where(phr.Query.RegistrationNo == registrationNo,
                        phr.Query.QuestionFormID == row["QuestionFormID"].ToString());
                    phr.LoadAll();
                    row["IsNewEnable"] = phr.Count == 0;
                }
            }
            dtb.AcceptChanges();

            return dtb;
        }

        private void PopulatePhrMenuAdd(RadToolBar tbarPhr, string serviceUnitID, string patientID, string registrationNo, string grdPhrClientId, bool isRecordAddAble = true)
        {
            var tbarItemAdd = (RadToolBarDropDown)tbarPhr.Items[0];
            tbarItemAdd.Buttons.Clear();

            if (!IsUserAddAble || !isRecordAddAble)
            {
                tbarItemAdd.Enabled = false;
                return;
            }

            var dtbQuestionForm = QuestionFormDatatable(serviceUnitID, patientID, registrationNo); ;
            if (dtbQuestionForm.Rows.Count > 0)
            {
                tbarItemAdd.Enabled = true;
                foreach (DataRow row in dtbQuestionForm.Rows)
                {
                    var btn = new RadToolBarButton(row["QuestionFormName"].ToString())
                    {
                        Value = string.Format("addphr^{0}^{1}^{2}^{3}^{4}", row["QuestionFormID"], serviceUnitID, patientID, registrationNo, grdPhrClientId)
                    };
                    btn.Enabled = true.Equals(row["IsNewEnable"]);
                    tbarItemAdd.Buttons.Add(btn);
                }
            }
            else
                tbarItemAdd.Enabled = false;
        }

        private string PhrEditLink(DataRow row, string grdPhrClientId, bool isRecordEditble)
        {
            // entryPhr(md, id, fid,suId, patId, regNo,grdPhrClientId)
            var isEditAble = isRecordEditble && this.IsUserEditAble && (row["IsApproved"] == DBNull.Value || false.Equals(row["IsApproved"])) && (row["IsSharingEdit"] != DBNull.Value && true.Equals(row["IsSharingEdit"]) || AppSession.UserLogin.UserID.Equals(row["CreateByUserID"]));
            return string.Format(
                "<a href=\"#\" onclick=\"entryPhr('{6}', '{0}','{1}','{2}','{3}','{4}', '{5}'); return false;\"><img src=\"{8}/Images/Toolbar/{7}16.png\" border=\"0\" /></a>",
                row["TransactionNo"], row["QuestionFormID"], row["ServiceUnitID"], row["PatientID"], row["RegistrationNo"], grdPhrClientId,
                isEditAble ? "edit" : "view",
                isEditAble ? "edit" : "views", Helper.UrlRoot());
        }

        protected string PhrViewLink(DataRow row, string grdPhrClientId)
        {
            // entryPhr(md, id, fid,suId, patId, regNo,grdPhrClientId)
            var retval =
                string.Format(
                    "<a href=\"#\" onclick=\"entryPhr('view', '{0}','{1}','{2}','{3}','{4}','{5}'); return false;\">{0}</a>",
                    row["TransactionNo"], row["QuestionFormID"], row["ServiceUnitID"], row["PatientID"], row["RegistrationNo"], grdPhrClientId);
            return retval;
        }

        private DataTable PatientHeathRecordDataTable(string registrationNo, string grdPhrClientId, bool isRecordEditble)
        {
            // Display semua PHR krn untuk keperluan list history
            var query = new PatientHealthRecordQuery("phr");
            var form = new QuestionFormQuery("f");
            var userQr = new AppUserQuery("usr");
            var reg = new RegistrationQuery("x");
            var par = new ParamedicQuery("y");
            var unit = new ServiceUnitQuery("z");

            query.InnerJoin(form).On(query.QuestionFormID == form.QuestionFormID);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.LeftJoin(par).On(reg.ParamedicID == par.ParamedicID);
            query.LeftJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
            query.LeftJoin(userQr).On(query.CreateByUserID == userQr.UserID);

            query.Where(query.RegistrationNo == registrationNo);

            // Filter RestrictionUserType
            query.Where(query.Or(form.RestrictionUserType.IsNull(), form.RestrictionUserType == string.Empty,
                form.RestrictionUserType.Like("%" + AppSession.UserLogin.SRUserType + "%")));

            // Form Physiotherapy dan PatientLetter jangan dimunculkan
            query.Where(query.Or(form.SRQuestionFormType.IsNull(), form.SRQuestionFormType == string.Empty,
                form.SRQuestionFormType == QuestionForm.QuestionFormType.PatientTransfer,
                form.SRQuestionFormType == QuestionForm.QuestionFormType.General));

            query.Select(
                query.TransactionNo,
                reg.RegistrationNo,
                reg.PatientID,
                par.ParamedicName,
                unit.ServiceUnitName,
                unit.ServiceUnitID, // Untuk keperluan hak akses
                query.QuestionFormID,
                form.QuestionFormName,
                @"<CAST(CONVERT(VARCHAR(10), phr.RecordDate, 112) + ' ' + phr.RecordTime AS DATETIME) AS RecordDateTime>",
                userQr.UserName.As("CreatedByUserName"),
                query.IsComplete,
                query.ReferenceNo,
                form.RmNO,
                query.CreateByUserID,
                query.IsApproved,
                form.IsSharingEdit,
                form.RestrictionUserType,
                form.IsSingleEntry
                );

            var dtb = query.LoadDataTable();
            dtb.Columns.Add(new DataColumn("UrlEdit", typeof(string)));
            dtb.Columns.Add(new DataColumn("UrlView", typeof(string)));
            foreach (DataRow row in dtb.Rows)
            {
                row["UrlEdit"] = PhrEditLink(row, grdPhrClientId, isRecordEditble);
                row["UrlView"] = PhrViewLink(row, grdPhrClientId);
            }
            dtb.AcceptChanges();

            return dtb;
        }

        #endregion



        protected void grdPhr_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "RebindGrid")
            {
                var grd = (RadGrid)sender;
                var tbarPhr = (RadToolBar)grd.Parent.FindControl("tbarPhr");

                var serviceUnitID = hdnServiceUnitID.Value;
                var patientID = hdnPatientID.Value;
                var registrationNo = hdnRegistrationNo.Value;


                PopulatePhr(grd, tbarPhr, serviceUnitID, patientID, registrationNo, grd.ClientID);
            }
        }

        private void PopulatePhr(RadGrid grd, RadToolBar tbarPhr, string serviceUnitID, string patientID, string registrationNo,
            string grdPhrClientId)
        {
            var isRecordAddAble = true;
            var deadlineAddable = AppParameter.GetParameterValue(AppParameter.ParameterItem.DeadlineMedicalRecordAddableAfterDischarge).ToInt();
            var isRecordEditble = true;
            var deadlineEditable = AppParameter.GetParameterValue(AppParameter.ParameterItem.DeadlineMedicalRecordEditableAfterDischarge).ToInt();

            if (deadlineAddable > 0 || deadlineEditable > 0)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(registrationNo);

                if (!IsMedicalRecordOpen(deadlineAddable, reg))
                {
                    var par = AppParameter.GetParameter(AppParameter.ParameterItem.DeadlineMedicalRecordAddableAfterDischarge);
                    var msg = string.Format(par.Message, par.ParameterValue);

                    var litPhrMessage = (Literal)grd.Parent.FindControl("litPhrMessage");
                    litPhrMessage.Visible = true;
                    litPhrMessage.Text = string.Format("<div style=\"color:yellow;width:100%;padding:4px 4px 4px 4px;\"><img src=\"../../../Images/boundleft.gif\"/>&nbsp;{0}</div>", msg);
                    isRecordAddAble = false;
                }

                if (!IsMedicalRecordOpen(deadlineEditable, reg))
                {
                    var par = AppParameter.GetParameter(AppParameter.ParameterItem.DeadlineMedicalRecordEditableAfterDischarge);
                    var msg = string.Format(par.Message, par.ParameterValue);

                    var litPhrMessage = (Literal)grd.Parent.FindControl("litPhrMessage");
                    litPhrMessage.Visible = true;

                    if (!string.IsNullOrWhiteSpace(litPhrMessage.Text))
                    {
                        var editMsg = string.Format("<div style=\"color:yellow;width:100%;padding:0px 4px 4px 4px;\"><img src=\"../../../Images/boundleft.gif\"/>&nbsp;{0}</div>", msg);
                        litPhrMessage.Text = string.Concat(litPhrMessage.Text, editMsg);
                    }
                    else
                        litPhrMessage.Text = string.Format("<div style=\"color:yellow;width:100%;padding:4px 4px 4px 4px;\"><img src=\"../../../Images/boundleft.gif\"/>&nbsp;{0}</div>", msg);

                    isRecordEditble = false;
                }
            }

            grd.InitializeCultureGrid();

            PopulatePhrMenuAdd(tbarPhr, serviceUnitID, patientID, registrationNo, grdPhrClientId, isRecordAddAble);

            var tbarItemRefresh = (RadToolBarButton)tbarPhr.Items[1];
            tbarItemRefresh.Value = string.Concat("refresh_", grdPhrClientId);

            grd.DataSource = PatientHeathRecordDataTable(registrationNo, grdPhrClientId, isRecordEditble);
            grd.Rebind();
        }
    }
}
