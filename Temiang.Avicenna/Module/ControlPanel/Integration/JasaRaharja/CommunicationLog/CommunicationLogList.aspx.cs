using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.ControlPanel.Integration.JasaRaharja
{
    public partial class CommunicationLogList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.JasaRaharjaCommunicationLog;

            if (IsPostBack) return;
            txtRegistrationDate.SelectedDate = DateTime.Now.Date;
            txtRegistrationDateTo.SelectedDate = DateTime.Now.Date;
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = CommunicationLogs;
        }

        private DataTable CommunicationLogs
        {
            get
            {
                var reg = new VwJasaRaharjaLogQuery("a");                
                reg.es.Top = AppSession.Parameter.MaxResultRecord;
                reg.Select(
                    reg.LogId,
                    "<CASE WHEN a.OperationType = 0 THEN 'SEND' WHEN a.OperationType = 1 THEN 'QUERY' ELSE 'UPLOAD' END AS OperationType>",
                    reg.SendDateTime,
                    reg.SendParameter,
                    reg.ReceiveResult,
                    reg.RegistrationNo,
                    reg.IsOperationSuccess
                    );
                if (!txtRegistrationDate.IsEmpty && !txtRegistrationDateTo.IsEmpty)
                    reg.Where("<CONVERT(VARCHAR(MAX), a.SendDateTime, 101) BETWEEN '" + txtRegistrationDate.SelectedDate.Value.ToString("MM/dd/yyyy") + "' AND '" + txtRegistrationDateTo.SelectedDate.Value.ToString("MM/dd/yyyy") + "'>");
                if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
                    reg.Where(
                        reg.Or(
                            reg.RegistrationNo == txtRegistrationNo.Text,
                            reg.MedicalNo == txtRegistrationNo.Text
                        )
                    );
                if (!string.IsNullOrEmpty(txtPatientName.Text))
                    reg.Where(reg.SendParameter.Like("%" + txtPatientName.Text + "%"));
                reg.OrderBy(reg.SendDateTime.Descending);
                return reg.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }
    }
}
