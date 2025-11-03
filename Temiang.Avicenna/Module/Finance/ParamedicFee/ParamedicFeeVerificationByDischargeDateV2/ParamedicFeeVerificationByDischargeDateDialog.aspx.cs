using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee.V2
{
    public partial class ParamedicFeeVerificationByDischargeDateDialog : BasePageDialog
    {
        private bool IsPhysicianMember
        {
            get { return System.Convert.ToBoolean(Request.QueryString["IsPhysicianMember"]); }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //var fb = new MedicalRecordFileBorrowed();
                //fb.LoadByPrimaryKey(Request.QueryString["trn"]);

                //var pat = new Patient();
                //pat.LoadByPrimaryKey(fb.PatientID);

                //this.Title = pat.MedicalNo + " (" + (pat.FirstName + " " + pat.MiddleName + " " + pat.LastName).Trim() + ")";

                var par = new ParamedicQuery();
                par.Where(par.ParamedicID == Request.QueryString["parid"]);
                cboPhysicianID.DataSource = par.LoadDataTable();
                cboPhysicianID.DataBind();
                cboPhysicianID.SelectedValue = Request.QueryString["parid"];
            }
        }

        protected void cboPhysicianID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboPhysicianID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery("a");
            query.es.Top = 30;
            query.Where
                (
                    query.ParamedicName.Like(searchTextContain),
                    query.IsActive == true
                );
            query.OrderBy(query.ParamedicName.Ascending);

            cboPhysicianID.DataSource = query.LoadDataTable();
            cboPhysicianID.DataBind();
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            if (string.IsNullOrEmpty(cboPhysicianID.SelectedValue))
            {
                ShowInformationHeader("Physician is required.");
                return false;
            }

            var pParamedicId = cboPhysicianID.SelectedValue;

            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            var feeQ = feeColl.MainQuery();
            feeQ.Where(feeQ.TransactionNo == Request.QueryString["trno"],
                       feeQ.SequenceNo == Request.QueryString["seqno"],
                       feeQ.TariffComponentID == Request.QueryString["compid"]);
            feeQ.es.Top = 1;
            feeColl.Load(feeQ);

            if (feeColl.Count == 0)
            {
                ShowInformationHeader("Data not found.");
                return false;
            }

            if (feeColl.First().SRPhysicianFeeCategory == "01" || feeColl.First().SRPhysicianFeeCategory == "04" || 
                feeColl.First().SRPhysicianFeeCategory == "05")
            {
                feeColl.First().ParamedicID = pParamedicId;
                feeColl.CalculateGrossFee(AppSession.UserLogin.UserID);

                #region Deduction
                var decColl = new ParamedicFeeDeductionsCollection();
                var decQuery = new ParamedicFeeDeductionsQuery("a");
                var feeQuery = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("b");
                decQuery.InnerJoin(feeQuery).On(
                    decQuery.TransactionNo.Equal(feeQuery.TransactionNo) &&
                    decQuery.SequenceNo.Equal(feeQuery.SequenceNo) &&
                    decQuery.TariffComponentID.Equal(feeQuery.TariffComponentID))
                    .Where(decQuery.TransactionNo == Request.QueryString["trno"],
                       decQuery.SequenceNo == Request.QueryString["seqno"],
                       decQuery.TariffComponentID == Request.QueryString["compid"]
                    )
                    .Select(
                        decQuery
                    );
                decColl.Load(decQuery);
                feeColl.CalculateDeductionBeforeTax(decColl, AppSession.UserLogin.UserID);
                #endregion
                

                //-- update ParamedicID di table transChargesItemComp
                var pParamedicCollectionName = string.Empty;
                var tciComps = new TransChargesItemCompCollection();
                tciComps.Query.Where(tciComps.Query.TransactionNo == Request.QueryString["trno"],
                                   tciComps.Query.SequenceNo == Request.QueryString["seqno"]);
                tciComps.LoadAll();
                foreach (var tciComp in tciComps)
                {
                    if (tciComp.TariffComponentID == Request.QueryString["compid"])
                    {
                        tciComp.ParamedicID = pParamedicId;
                        tciComp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        tciComp.LastUpdateDateTime = DateTime.Now;
                    }

                    if (!string.IsNullOrEmpty(tciComp.ParamedicID))
                    {
                        var tComp = new TariffComponent();
                        if (tComp.LoadByPrimaryKey(tciComp.TariffComponentID))
                        {
                            if (tComp.IsPrintParamedicInSlip ?? false)
                            {
                                var par = new Paramedic();
                                par.LoadByPrimaryKey(tciComp.ParamedicID);
                                if (par.IsPrintInSlip ?? true)
                                {
                                    if (pParamedicCollectionName.Length == 0)
                                        pParamedicCollectionName = par.ParamedicName;
                                    else if (!pParamedicCollectionName.Contains(par.ParamedicName))
                                        pParamedicCollectionName = pParamedicCollectionName + "; " + par.ParamedicName;
                                }
                            }
                        }
                    }
                }

                //-- update ParamedicCollectionName di table TransChargesItem
                var tci = new TransChargesItem();
                tci.LoadByPrimaryKey(Request.QueryString["trno"], Request.QueryString["seqno"]);
                tci.ParamedicCollectionName = pParamedicCollectionName;

                using (esTransactionScope trans = new esTransactionScope())
                {
                    feeColl.Save();
                    decColl.Save();
                    tciComps.Save();
                    tci.Save();

                    trans.Complete();
                }

                (new ParamedicFeeTransChargesItemCompByDischargeDateCollection()).UpdateDataParamedic(
                    Request.QueryString["trno"],
                    Request.QueryString["seqno"],
                    Request.QueryString["compid"]);
            }
            else {
                ShowInformationHeader("Physician can not be changed.");
                return false;                
            }
            return true;
        }
    }
}
