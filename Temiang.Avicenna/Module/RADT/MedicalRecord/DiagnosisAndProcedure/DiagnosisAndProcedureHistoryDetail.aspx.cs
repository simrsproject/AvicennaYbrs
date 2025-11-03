using System;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class DiagnosisAndProcedureHistoryDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var patient = new Patient();
                patient.LoadByPrimaryKey(Request.QueryString["pid"]);

                Title = "History List : " + patient.PatientName + " [MRN: " + patient.MedicalNo + "]";

                (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                (Helper.FindControlRecursive(this, "btnCancel") as Button).Visible = false;
            }
        }

        protected void grdEpisodeDiagnose_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new EpisodeDiagnoseQuery("a");
            var diag = new DiagnoseQuery("b");
            var item = new AppStandardReferenceItemQuery("e");
            var morph = new MorphologyQuery("c");
            var param = new ParamedicQuery("d");
            var reg = new RegistrationQuery("f");
            var su = new ServiceUnitQuery("g");

            query.LeftJoin(diag).On(query.DiagnoseID == diag.DiagnoseID);
            query.InnerJoin(item).On(query.SRDiagnoseType == item.ItemID);
            query.LeftJoin(morph).On(query.MorphologyID == morph.MorphologyID);
            query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID);

            query.Select
                (
                    query.SelectAll(),
                    diag.DiagnoseName,
                    item.ItemName.As("DiagnoseType"),
                    morph.MorphologyName.As("MorphologyName"),
                    param.ParamedicName.As("ParamedicName"),
                    reg.RegistrationDate,
                    @"<CONVERT(VARCHAR(12), f.RegistrationDate, 13) + '  [ Registration No : ' + a.RegistrationNo + ' - Unit : ' + g.ServiceUnitName + ' ]' AS GroupName>"
                );

            query.Where(reg.PatientID == Request.QueryString["pid"], query.IsVoid == false);
            query.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationNo.Descending, query.SRDiagnoseType.Ascending, query.DiagnoseID.Ascending);

            DataTable dtb = query.LoadDataTable();

            grdEpisodeDiagnose.DataSource = dtb;
        }

        protected void grdEpisodeProcedure_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new EpisodeProcedureQuery("a");
            var param = new ParamedicQuery("b");
            var proc = new ProcedureQuery("c");
            var reg = new RegistrationQuery("d");
            var su = new ServiceUnitQuery("e");

            query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);
            query.InnerJoin(proc).On(query.ProcedureID == proc.ProcedureID);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID);

            query.Select
                 (
                     query.SelectAll(),
                     param.ParamedicName.As("ParamedicName"),
                     proc.ProcedureName.As("ProcedureName"),
                     reg.RegistrationDate,
                     @"<CONVERT(VARCHAR(12), d.RegistrationDate, 13) + '  [ Registration No : ' + a.RegistrationNo + ' - Unit : ' + e.ServiceUnitName + ' ]' AS GroupName>"
                 );

            query.Where(reg.PatientID == Request.QueryString["pid"], query.IsFromOperatingRoom == true);
            query.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationNo.Descending, query.SequenceNo.Ascending);

            DataTable dtb = query.LoadDataTable();

            grdEpisodeProcedure.DataSource = dtb;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdEpisodeDiagnose.Rebind();
            grdEpisodeProcedure.Rebind();
        }
    }
}