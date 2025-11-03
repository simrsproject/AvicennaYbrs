using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;


namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class MedicationReceiveMaintenance : BasePageDialog
    {

        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }        
        public string ReferFromRegistrationNo
        {
            get
            {
                return Request.QueryString["fregno"];
            }
        }

        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Medication of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonCancel.Text = "Close";
            ButtonOk.Visible = false;
        }

        #region MedicationReceive
        protected void grdMedicationReceive_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Continue")
            {
                var medicationReceiveNo = Convert.ToDecimal(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["MedicationReceiveNo"]).ToInt();
                var med = new MedicationReceive();
                if (med.LoadByPrimaryKey(medicationReceiveNo))
                {

                    med.IsContinue = true;
                    med.Save();

                    var stat = new MedicationReceiveStatus();
                    stat.MedicationReceiveNo = medicationReceiveNo;
                    stat.StatusDateTime = DateTime.Now;
                    stat.IsMedicationStop = false;
                    stat.Save();

                    grdMedicationReceive.Rebind();
                }
            }
        }

        protected void grdMedicationReceive_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdMedicationReceive.DataSource = MedicationReceiveDataTable(RegistrationNo, ReferFromRegistrationNo);
        }

        protected void grdMedicationReceive_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var medicationReceiveNo = Convert.ToDecimal(item.OwnerTableView.DataKeyValues[item.ItemIndex]["MedicationReceiveNo"]).ToInt();

            var nmd = new MedicationReceive();
            if (nmd.LoadByPrimaryKey(medicationReceiveNo))
            {
                nmd.IsVoid = true;
                nmd.Save();
            }

            grdMedicationReceive.DataSource = null;
            grdMedicationReceive.Rebind();
        }


        private DataTable MedicationReceiveDataTable(string registrationNo, string referFromRegistrationNo)
        {
            var query = new MedicationReceiveQuery("a");
            var cm = new ConsumeMethodQuery("cm");
            query.LeftJoin(cm).On(query.SRConsumeMethod == cm.SRConsumeMethod);

            var patrec = new MedicationReceiveFromPatientQuery("b");
            query.LeftJoin(patrec).On(query.MedicationReceiveNo == patrec.MedicationReceiveNo);

            var mc = new AppStandardReferenceItemQuery("mc");
            query.LeftJoin(mc).On(query.SRMedicationConsume == mc.ItemID &&
                                  mc.StandardReferenceID == "MedicationConsume");

            query.Select(query, patrec.Condition, patrec.ExpireDate, patrec.ApprovedByParamedicID, patrec.LastConsumeDateTime, cm.SRConsumeMethodName,
                mc.ItemName.As("SRMedicationConsumeName"));

            query.Where(query.Or(query.RegistrationNo == registrationNo, query.RegistrationNo == referFromRegistrationNo));
            query.OrderBy(query.MedicationReceiveNo.Descending);
            var dtb = query.LoadDataTable();

            return dtb;
        }
        protected void grdMedicationReceive_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string medicationReceiveNo = dataItem.GetDataKeyValue("MedicationReceiveNo").ToString();

            if (e.DetailTableView.Name.Equals("grdMedicationReceiveUsed"))
            {
                var query = new MedicationReceiveUsedQuery("a");

                var setup = new AppUserQuery("u1");
                query.LeftJoin(setup).On(query.SetupByUserID == setup.UserID);
                var verif = new AppUserQuery("u2");
                query.LeftJoin(verif).On(query.VerificationByUserID == verif.UserID);
                var realize = new AppUserQuery("u3");
                query.LeftJoin(realize).On(query.RealizedByUserID == realize.UserID);

                query.Select(query.SelectAll(), setup.UserName.As("SetupByUserName"), verif.UserName.As("VerificationByUserName"), realize.UserName.As("RealizedByUserName"));
                query.Where(query.MedicationReceiveNo == medicationReceiveNo);
                query.OrderBy(query.SequenceNo.Descending);

                e.DetailTableView.DataSource = query.LoadDataTable();
            }
        }

        protected void grdMedicationReceive_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (true.Equals(item.GetDataKeyValue("IsVoid")))
                {
                    item.Style.Add(HtmlTextWriterStyle.TextDecoration, "line-through");
                    item.Style.Add(HtmlTextWriterStyle.Color, "gray");
                }
                else if (false.Equals(item.GetDataKeyValue("IsContinue")))
                {
                    item.Style.Add(HtmlTextWriterStyle.Color, "yellow");
                }
            }
        }
        #endregion

    }
}
