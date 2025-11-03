using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class MedicalRecordFileStatusList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.MedicalFileStatus;

            if (!IsPostBack)
            {
                txtDate.SelectedDate = DateTime.Now;

                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");
                query.Where(
                    query.SRRegistrationType.In(
                        AppConstant.RegistrationType.ClusterPatient,
                        AppConstant.RegistrationType.EmergencyPatient,
                        AppConstant.RegistrationType.InPatient,
                        AppConstant.RegistrationType.OutPatient,
                        AppConstant.RegistrationType.MedicalCheckUp
                        ),
                    query.IsActive == true
                    );
                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);
                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                StandardReference.InitializeIncludeSpace(cboInOutStatus, AppEnum.StandardReference.MedicalFileCategory);
                StandardReference.InitializeIncludeSpace(cboFileStatus, AppEnum.StandardReference.MedicalFileStatus);
                StandardReference.InitializeIncludeSpace(cboRegistrationType, AppEnum.StandardReference.RegistrationType);
            }
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

            var dataSource = MedicalRecordFileStatuss;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        protected void grdList_ItemDataBound(object source, GridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case (GridItemType.AlternatingItem):
                case (GridItemType.Item):
                    {
                        // detect main table
                        var lname = e.Item.OwnerTableView.Name;
                        if (lname == "MainTable")
                        {
                            var SrStatus = (bool)DataBinder.Eval(e.Item.DataItem, "IsVoid");

                            if (SrStatus)
                            {
                                e.Item.ForeColor = System.Drawing.Color.Red;
                                e.Item.Font.Bold = true;
                            }
                        }
                        break;
                    }
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.DataSource = MedicalRecordFileStatuss;
            grdList.CurrentPageIndex = 0;
            grdList.Rebind();
        }

        private DataTable MedicalRecordFileStatuss
        {
            get
            {
                var isEmptyFilter = txtDate.IsEmpty && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboPhysicianID.SelectedValue) && string.IsNullOrEmpty(cboInOutStatus.SelectedValue) && 
                    string.IsNullOrEmpty(cboFileStatus.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtMedicalNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) && 
                    string.IsNullOrEmpty(cboRegistrationType.SelectedValue) && string.IsNullOrEmpty(txtBarcodeEntry.Text);
                if (!ValidateSearch(isEmptyFilter, "Medical Record File")) return null;

                var qa = new MedicalRecordFileStatusQuery("a");
                var qb = new RegistrationQuery("b");
                var qc = new PatientQuery("c");
                var qe = new AppStandardReferenceItemQuery("e");
                var qf = new AppStandardReferenceItemQuery("f");
                var qg = new ParamedicQuery("g");
                var qh = new ServiceUnitQuery("h");
                var asr = new AppStandardReferenceItemQuery("i");

                //qa.es.Top = AppSession.Parameter.MaxResultRecord;

                if (AppSession.Parameter.IsDisplayRegDateTimeUseCreateDate)
                {
                    qa.Select(
                            qb.LastCreateDateTime.As("RegistrationDate"),
                            "<convert(char(5), b.LastCreateDateTime, 108) [RegistrationTime]>"
                            );
                }else{
                    qa.Select(
                        qb.RegistrationDate,
                        qb.RegistrationTime
                        );
                }
                
                qa.Select
                    (
                        qa.RegistrationNo,
                        qc.MedicalNo,
                        asr.ItemName.As("SalutationName"),
                        qc.PatientName,
                        qc.Sex,
                        qa.SRMedicalFileStatus,
                        qa.SRMedicalFileCategory,
                        qe.ItemName.As("MedicalFileCategory"),
                        qf.ItemName.As("MedicalFileStatus"),
                        qg.ParamedicName,
                        qh.ServiceUnitName,
                        qb.SRRegistrationType,
                        qb.IsConsul,
                        @"<CAST(0 AS BIT) AS 'IsConfirmed'>",
                        qb.IsVoid,
                        qb.IsNewPatient,
                        qb.IsNewVisit,
                        qb.PatientID,
                        qb.RegistrationDate,
                        qb.RegistrationTime,
                        qb.IsTracer,
                        @"<ISNULL((SELECT TOP 1 ISNULL(su.MedicalFileFolderColor, '#FFFFFF') AS MedicalFileFolderColor
FROM Registration AS rr 
INNER JOIN MergeBilling AS mb ON mb.RegistrationNo = rr.RegistrationNo AND mb.FromRegistrationNo = ''
INNER JOIN ServiceUnit AS su ON su.ServiceUnitID = rr.ServiceUnitID
WHERE rr.PatientID = b.PatientID AND rr.IsVoid = 0 AND rr.IsFromDispensary = 0 AND rr.isDirectPrescriptionReturn = 0 AND ((LEFT(CONVERT(VARCHAR, rr.RegistrationDate, 20), 11) + rr.RegistrationTime) < (LEFT(CONVERT(VARCHAR, b.RegistrationDate, 20), 11) + b.RegistrationTime))
ORDER BY (LEFT(CONVERT(VARCHAR, rr.RegistrationDate, 20), 11) + rr.RegistrationTime) DESC), '') AS MedicalFileFolderColor>",
                        @"<'-' AS MedicalFileFolderText>"
                    );

                qa.InnerJoin(qb).On(qa.RegistrationNo == qb.RegistrationNo);
                qa.InnerJoin(qc).On(qb.PatientID == qc.PatientID);
                qa.LeftJoin(qe).On
                        (
                            qe.StandardReferenceID == "MedicalFileCategory" &
                            qa.SRMedicalFileCategory == qe.ItemID 
                            
                        );
                qa.LeftJoin(qf).On
                        (
                            qf.StandardReferenceID == "MedicalFileStatus" &
                            qa.SRMedicalFileStatus == qf.ItemID
                        );
                qa.InnerJoin(qg).On(qb.ParamedicID == qg.ParamedicID);
                qa.InnerJoin(qh).On(qb.ServiceUnitID == qh.ServiceUnitID);
                qa.LeftJoin(asr).On(asr.StandardReferenceID == "Salutation" & qc.SRSalutation == asr.ItemID);

                var isFilter = false;

                if (!txtDate.IsEmpty)
                {
                    qa.Where(qb.RegistrationDate == txtDate.SelectedDate);
                    isFilter = true;
                }

                if (txtMedicalNo.Text != string.Empty)
                {
                    string searchMedNo = Helper.EscapeQuery(txtMedicalNo.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        qa.Where(
                            qa.Or(
                                qc.MedicalNo == searchMedNo,
                                qc.OldMedicalNo == searchMedNo,
                                string.Format("< OR REPLACE(c.MedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo),
                                string.Format("< OR REPLACE(c.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo)
                                )
                            );
                    else
                        qa.Where(
                            qa.Or(
                                qc.MedicalNo == searchMedNo,
                                qc.OldMedicalNo == searchMedNo,
                                string.Format("< OR c.MedicalNo LIKE '%{0}%'>", searchMedNo),
                                string.Format("< OR c.OldMedicalNo LIKE '%{0}%'>", searchMedNo)
                                )
                            );
                    isFilter = true;
                }

                if (txtRegistrationNo.Text != string.Empty)
                {
                    qa.Where(qa.RegistrationNo == txtRegistrationNo.Text);
                    isFilter = true;
                }

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                {
                    qa.Where(qb.ServiceUnitID == cboServiceUnitID.SelectedValue);
                    //isFilter = true;
                }

                if (!string.IsNullOrEmpty(cboRegistrationType.SelectedValue))
                {
                    qa.Where(qb.SRRegistrationType == cboRegistrationType.SelectedValue);
                    //isFilter = true;
                }

                if (!string.IsNullOrEmpty(cboPhysicianID.SelectedValue))
                {
                    qa.Where(qb.ParamedicID == cboPhysicianID.SelectedValue);
                    //isFilter = true;
                }
                
                if (!string.IsNullOrEmpty(cboInOutStatus.SelectedValue))
                {
                    qa.Where(qa.SRMedicalFileCategory == cboInOutStatus.SelectedValue);
                    //isFilter = true;
                }
                
                if (!string.IsNullOrEmpty(cboFileStatus.SelectedValue))
                {
                    qa.Where(qa.SRMedicalFileStatus == cboFileStatus.SelectedValue);
                    //isFilter = true;
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";

                    qa.Where
                        (
                          string.Format("<RTRIM(c.FirstName+' '+c.MiddleName)+' '+c.LastName LIKE '{0}'>", searchPatient)
                        );
                    isFilter = true;
                }

                if (AppSession.Parameter.IsDisplayRegDateTimeUseCreateDate)
                {
                    qa.OrderBy(qb.LastCreateDateTime.Descending);
                }
                else
                {
                    qa.OrderBy(qb.RegistrationDate.Descending, qb.RegistrationTime.Descending);
                }

                qa.Where(qc.MedicalNo != string.Empty);

                if (isFilter == false)
                    qa.es.Top = AppSession.Parameter.MaxResultRecord;
                
                DataTable tbl = qa.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    if ((row["SRRegistrationType"].ToString() == AppConstant.RegistrationType.ClusterPatient 
                        || row["SRRegistrationType"].ToString() == AppConstant.RegistrationType.OutPatient) 
                        && (bool)row["IsConsul"])

                        row.Delete();
                    else
                    {
                        if (row["SRMedicalFileStatus"].ToString() == AppSession.Parameter.MedicalFileStatusConfirm)
                            row["IsConfirmed"] = true;
                    }
                }

                tbl.AcceptChanges();
                return tbl;
            }
        }

        protected virtual void txtBarcodeEntry_OnTextChanged(object sender, System.EventArgs e)
        {
            if (txtBarcodeEntry.Text == "") return;

            var que = new PatientQuery();
            que.es.Top = 1;
            que.Where(que.MedicalNo == txtBarcodeEntry.Text);
            var entity = new Patient();
            entity.Load(que);

            if (entity.PatientID != null)
            {
                Helper.FileStatusInOut.FileInOutConfirmed(entity.PatientID, false, txtDate.SelectedDate.Value);
                grdList.Rebind();

            }
            txtBarcodeEntry.Text = "";
            txtBarcodeEntry.Focus();
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
                    var fs = new MedicalRecordFileStatus();
                    if (fs.LoadByPrimaryKey(param[1]))
                    {
                        //fs.FileOutDate = DateTime.Now;
                        fs.SRMedicalFileStatus = AppSession.Parameter.MedicalFileStatusConfirm;
                        fs.FileOutDateConfirmed = DateTime.Now;
                        fs.FileOutUserIDComfirmed = AppSession.UserLogin.UserID;
                        fs.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        fs.LastUpdateDateTime = DateTime.Now;
                        fs.Save();
                    }

                    trans.Complete();
                }

                grdList.Rebind();
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new MedicalRecordFileStatusMovementQuery("a");
            var unit = new ServiceUnitQuery("b");
            var user = new AppUserQuery("c");
            var merge = new MergeBillingQuery("d");

            query.Select(
                query.RegistrationNo,
                unit.ServiceUnitName,
                query.LastPositionDateTime,
                user.UserName
                );
            query.InnerJoin(unit).On(unit.ServiceUnitID == query.LastPositionServiceUnitID);
            query.InnerJoin(user).On(user.UserID == query.LastPositionUserID);
            query.LeftJoin(merge).On(merge.RegistrationNo == query.RegistrationNo);
            query.Where(query.Or(
                query.RegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString(),
                merge.FromRegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString()));
            query.OrderBy(query.LastPositionDateTime.Descending);

            DataTable tbl = query.LoadDataTable();

            e.DetailTableView.DataSource = tbl;
        }

        protected void cboPhysicianID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery("a");
            query.Where(
                query.Or(query.ParamedicID == e.Text,
                query.ParamedicName.Like(searchTextContain)),
                query.IsActive == true
                );
            query.Select(query.ParamedicID, query.ParamedicName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboPhysicianID.DataSource = dtb;
            cboPhysicianID.DataBind();
        }

        protected void cboPhysicianID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ParamedicItemDataBound(e);
        }
    }
}
