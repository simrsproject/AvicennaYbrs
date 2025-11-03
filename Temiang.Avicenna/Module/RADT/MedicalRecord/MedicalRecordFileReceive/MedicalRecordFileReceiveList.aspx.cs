using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class MedicalRecordFileReceiveList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.FileReceive;

            if (!IsPostBack)
            {
                txtDate.SelectedDate = DateTime.Now.Date;
                txtReceiveDate.SelectedDate = DateTime.Now.Date;
                txtReceiveTime.Text = DateTime.Now.ToString("HH:mm");

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

                var param = new ParamedicCollection();
                var medik = new ParamedicQuery("a");
                param.Query.Where
                    (
                        param.Query.IsActive == true,
                        param.Query.IsAvailable == true
                    );
                param.Query.OrderBy(param.Query.ParamedicName.Ascending);
                param.Load(medik);

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic entity in param)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                }

                txtDate.SelectedDate = DateTime.Today;
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

        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = txtDate.IsEmpty && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) && txtReceiveDate.IsEmpty && 
                    string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtMedicalNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) &&
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

                qa.es.Top = AppSession.Parameter.MaxResultRecord;
                qa.Select
                    (
                        qa.RegistrationNo,
                        qb.RegistrationDate,
                        qb.RegistrationTime,
                        qb.RegistrationDateTime,
                        qb.DischargeDate,
                        qb.DischargeTime,
                        qb.DischargeDateTime,
                        qc.MedicalNo,
                        asr.ItemName.As("SalutationName"),
                        qc.PatientName,
                        qc.Sex,
                        qa.SRMedicalFileStatus,
                        qa.SRMedicalFileCategory,
                        qe.ItemName.As("MedicalFileCategory"),
                        qf.ItemName.As("MedicalFileStatus"),
                        qg.ParamedicID,
                        qg.ParamedicName,
                        qh.ServiceUnitName,
                        qb.SRRegistrationType,
                        qb.IsConsul,
                        qb.IsVoid
                    );

                qa.InnerJoin(qb).On(qa.RegistrationNo == qb.RegistrationNo);
                qa.InnerJoin(qc).On(qb.PatientID == qc.PatientID);
                qa.LeftJoin(qe).On
                        (
                            qa.SRMedicalFileCategory == qe.ItemID &
                            qe.StandardReferenceID == "MedicalFileCategory"
                        );
                qa.LeftJoin(qf).On
                        (
                            qa.SRMedicalFileStatus == qf.ItemID &
                            qf.StandardReferenceID == "MedicalFileStatus"
                        );
                qa.InnerJoin(qg).On(qb.ParamedicID == qg.ParamedicID);
                qa.InnerJoin(qh).On(qb.ServiceUnitID == qh.ServiceUnitID);
                qa.LeftJoin(asr).On(asr.StandardReferenceID == "Title" & asr.ItemID == qc.SRSalutation);

                if (!txtDate.IsEmpty)
                    qa.Where(string.Format(@"<(b.SRRegistrationType = 'IPR' AND b.DischargeDate = '{0}') OR (b.SRRegistrationType <> 'IPR' AND b.RegistrationDate = '{0}')>", txtDate.SelectedDate));
                    
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
                }

                if (txtRegistrationNo.Text != string.Empty)
                    qa.Where(qa.RegistrationNo == txtRegistrationNo.Text);

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    qa.Where(qb.ServiceUnitID == cboServiceUnitID.SelectedValue);

                if (!string.IsNullOrEmpty(cboRegistrationType.SelectedValue))
                    qa.Where(qb.SRRegistrationType == cboRegistrationType.SelectedValue);

                if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                    qa.Where(qb.ParamedicID == cboParamedicID.SelectedValue);

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";

                    qa.Where
                        (
                          string.Format("<RTRIM(c.FirstName+' '+c.MiddleName)+' '+c.LastName LIKE '{0}'>", searchPatient)
                        );
                }

                qa.Where(qa.SRMedicalFileCategory == AppSession.Parameter.MedicalFileCategoryOut);

                qa.OrderBy(qb.RegistrationDate.Descending, qb.RegistrationTime.Descending);

                DataTable tbl = qa.LoadDataTable();

                return tbl;
            }
        }

        protected void grdRegisteredList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = Registrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        protected void grdRegisteredList_ItemDataBound(object source, GridItemEventArgs e)
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
            grdRegisteredList.DataSource = Registrations;
            grdRegisteredList.CurrentPageIndex = 0;
            grdRegisteredList.Rebind();
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
                Helper.FileStatusInOut.FileInConfirmedByPatientID(entity.PatientID,
                    DateTime.Parse(txtReceiveDate.SelectedDate.Value.ToShortDateString() + " " + txtReceiveTime.TextWithLiterals));
                grdRegisteredList.Rebind();

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

                Helper.FileStatusInOut.FileInConfirmed(param[1],
                    DateTime.Parse(txtReceiveDate.SelectedDate.Value.ToShortDateString() + " " + txtReceiveTime.TextWithLiterals));

                grdRegisteredList.Rebind();
            }
        }
    }
}
