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

namespace Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NursingCare
{
    public partial class NursingCareStandardAddDiagnoseMidwifeCustom : BasePageDialog
    {
        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        public NursingTransHD nsHD
        {
            get
            {
                if (Session["NursingTransHD" + RegistrationNo] == null)
                {
                    Session["NursingTransHD" + RegistrationNo] = Common.NursingCare.SetTransHD(RegistrationNo, AppSession.UserLogin.UserID);
                }

                return (NursingTransHD)Session["NursingTransHD" + RegistrationNo];
            }
            set
            {
                Session["NursingTransHD" + RegistrationNo] = value;
            }
        }

        private string[] ExceptionIDs(string Level)
        {
            // ambil dari database daftar diagnosa yang sudah pernah diangkat
            var dColl = NursingDiagnosaTransDT.NursingDiagnosa(nsHD.TransactionNo, Level).
                Where(x => x.SRNursingCarePlanning != "01");
            var idColl = from d in dColl select d.NursingDiagnosaID;

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
            else {
                return "oWnd.argument.result = 'Gak OK'";
            }
        }
        public override bool OnButtonOkClicked()
        {
            Page.Validate();
            if (!Page.IsValid) return false;

            var nsDT = new NursingDiagnosaTransDTCollection();
            foreach (GridDataItem x in gridEtiologyNew.MasterTableView.Items)
            {
                var chk = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        var et = new NursingDiagnosa();
                        var idEt = x.GetDataKeyValue("NursingDiagnosaID").ToString();
                        if (et.LoadByPrimaryKey(idEt))
                        {
                            var etio = nsDT.AddNew();
                            etio.NursingDiagnosaID = et.NursingDiagnosaID;
                            var txt = x.FindControl("txtNursingDiagnosaName") as RadTextBox;
                            //etio.NursingDiagnosaName = et.NursingDiagnosaName;
                            etio.NursingDiagnosaName = txt.Text;

                            etio.SRNursingDiagnosaLevel = et.SRNursingDiagnosaLevel;
                            etio.NursingDiagnosaParentID = et.NursingDiagnosaParentID;

                            etio.S = txtPrefix.Text;
                            etio.O = txtSuffix.Text;

                            etio.TmpNursingDiagnosaID = string.Empty;
                        }
                    }
                }
            }

            if (nsDT.Count > 0) {
                Common.NursingCare.SaveDiagL11(RegNo, nsDT.ToArray(), null, "04");
            }
            return true;
        }

        private string RegNo
        {
            get {
                return Request.QueryString["regno"];
            }
        }

        protected void customValidatorNewDiag_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(txtPrefix.Text))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Prefix can not be empty";
                return;
            }

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
            //var ls = NursingDiagnosa.GetDiagnosaByParentAndLevel(IdDiag, "11", ExceptionIDs("11"));
            var ls = NursingDiagnosaTransDT.NursingProblemForImplementation(
                nsHD.TransactionNo, string.Empty, string.Empty, ExceptionIDs("10"), "04"/*dipatok dulu ya*/, true);
            gridEtiologyNew.DataSource = ls;
        }

        protected void lbtnRefresh_Click(Object sender, EventArgs e)
        {
            gridEtiologyNew.Rebind();
        }

        protected void defaultChkBox_CheckedChanged(object sender, EventArgs e)
        {
            var chk = ((CheckBox)sender);
            if (chk.Checked)
            {
                var nsid = (chk.NamingContainer as GridDataItem).GetDataKeyValue("NursingDiagnosaID").ToString();
                var etio = new NursingDiagnosa();
                if (etio.LoadByPrimaryKey(nsid))
                {
                    var ns = new NursingDiagnosa();
                    if (ns.LoadByPrimaryKey(etio.NursingDiagnosaParentID))
                    {
                        //if (!string.IsNullOrEmpty(ns.Prefix) && txtPrefix.tex)
                        lblSelectedDiagnosa.Text = ns.NursingDiagnosaName;
                    }
                }
            }
        }
    }
}
