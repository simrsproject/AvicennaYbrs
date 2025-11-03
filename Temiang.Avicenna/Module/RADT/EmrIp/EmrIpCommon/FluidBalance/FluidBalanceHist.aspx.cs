using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;


namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class FluidBalanceHist : BasePageDialog
    {
        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
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
                    this.Title = "Fluid Balance of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonCancel.Text = "Close";
            ButtonOk.Text = "Edit";
        }

        protected void grdHeader_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var qr = new PatientFluidBalanceQuery("fb");
            var reg = new RegistrationQuery("r");
            qr.InnerJoin(reg).On(qr.RegistrationNo == reg.RegistrationNo);
            qr.Where(qr.RegistrationNo.In(MergeRegistrations), reg.RegistrationNo.In(MergeRegistrations));
            qr.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationTime.Descending, qr.SequenceNo.Descending);
            grdHeader.DataSource = qr.LoadDataTable();
        }

        protected void grdHeader_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var dataItem = grdHeader.SelectedItems[0] as GridDataItem;
            hdnEditRegistrationNo.Value = Convert.ToString(dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["RegistrationNo"]);
            hdnEditSequenceNo.Value = Convert.ToString(dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["SequenceNo"]);

            fluidBalanceCtl.Rebind(hdnEditRegistrationNo.Value,hdnEditSequenceNo.Value.ToInt());
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            var script = string.Format(@"oArg.editRegNo = document.getElementById('{0}').value;
            oArg.editSeqNo = document.getElementById('{1}').value;", hdnEditRegistrationNo.ClientID,
                hdnEditSequenceNo.ClientID);
            return script;
        }
        protected override void OnButtonOkClicked(ValidateArgs args)
        {
            var items = grdHeader.MasterTableView.GetSelectedItems();
            if (items.Length == 0 && grdHeader.MasterTableView.DetailTables.Count > 0)
                items = grdHeader.MasterTableView.DetailTables[0].GetSelectedItems();

            if (items.Length > 0)
            {
                var dataItem = items[0];
                var regNo = dataItem.GetDataKeyValue("RegistrationNo").ToString();
                var sequenceNo = dataItem.GetDataKeyValue("SequenceNo").ToInt();

                var fb = new PatientFluidBalance();
                if (fb.LoadByPrimaryKey(regNo, sequenceNo))
                {
                    // Set LastUpdateDateTime supaya menjadi yg terakhir dan ini akan dijadikan sbg Fluid Balance yg diedit
                    fb.LastUpdateDateTime = DateTime.Now;
                    fb.Save();
                }
            }
        }
    }
}
