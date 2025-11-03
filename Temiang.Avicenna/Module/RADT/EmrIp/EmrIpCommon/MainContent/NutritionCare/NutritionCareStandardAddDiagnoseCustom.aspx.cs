using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NutritionCare
{
    public partial class NutritionCareStandardAddDiagnoseCustom : BasePageDialog
    {
        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        public NutritionCareTransHD nsHD
        {
            get
            {
                if (Session["NutritionCareTransHD" + RegistrationNo] == null)
                {
                    Session["NutritionCareTransHD" + RegistrationNo] = Common.NursingCare.SetTransHD(RegistrationNo, AppSession.UserLogin.UserID);
                }

                return (NutritionCareTransHD)Session["NutritionCareTransHD" + RegistrationNo];
            }
            set
            {
                Session["NutritionCareTransHD" + RegistrationNo] = value;
            }
        }

        private string[] ExceptionIDs(string Level)
        {
            // ambil dari database daftar diagnosa yang sudah pernah diangkat
            var dColl = NutritionCareDiagnoseTransDT.NutritionCareDiagnosa(nsHD.TransactionNo, Level);
            var idColl = from d in dColl select d.TerminologyID;

            return idColl.ToArray();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            if (true)
            {
                return "oWnd.argument.result = 'OK'";
            }
            else
            {
                return "oWnd.argument.result = 'Gak OK'";
            }
        }
        public override bool OnButtonOkClicked()
        {
            var nsDT = new NutritionCareDiagnoseTransDTCollection();
            foreach (GridDataItem x in gridEtiologyNew.MasterTableView.Items)
            {
                var chk = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        var et = new NutritionCareTerminology();
                        var idEt = x.GetDataKeyValue("TerminologyID").ToString();
                        if (et.LoadByPrimaryKey(idEt))
                        {
                            var etio = nsDT.AddNew();
                            etio.TerminologyID = et.TerminologyID;
                            var txt = x.FindControl("txtNutritionCareDiagnosaName") as RadTextBox;
                            etio.TerminologyName = txt.Text;

                            etio.SRNutritionCareTerminologyLevel = et.SRNutritionCareTerminologyLevel;
                            etio.TerminologyParentID = et.TerminologyParentID;

                            etio.S = txtNewS.Text;
                            etio.O = txtNewO.Text;

                            etio.TmpTerminologyID = string.Empty;
                        }
                    }
                }
            }

            if (nsDT.Count > 0)
            {
                Common.NutritionCare.SaveDiagL11(RegNo, nsDT.ToArray());
            }
            return true;
        }

        private string RegNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        protected void customValidatorNewDiag_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //if (string.IsNullOrEmpty(txtNewAnthropometry.Text))
            //{
            //    args.IsValid = false;
            //    ((CustomValidator)source).ErrorMessage = "Anthropometry can not be empty";
            //    return;
            //}

            foreach (GridDataItem x in gridEtiologyNew.MasterTableView.Items)
            {
                var chk = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        return;
                    }
                }
            }
            args.IsValid = false;
            ((CustomValidator)source).ErrorMessage = "Please choose at least one etiology";
            return;
        }

        protected void gridEtiologyNew_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var ls = NutritionCareDiagnoseTransDT.NutritionCareProblemForImplementation(
                nsHD.TransactionNo, txtNewS.Text, txtNewO.Text, ExceptionIDs("10"), chkShowAllDiag.Checked);
            gridEtiologyNew.DataSource = ls;
        }

        protected void lbtnRefresh_Click(Object sender, EventArgs e)
        {
            gridEtiologyNew.Rebind();
        }
    }
}