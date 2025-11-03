using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
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
    public partial class NursingCareStandardAddDiagnoseMidwifeCustomWithEdit : BasePageDialog
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

        public NursingDiagnosaCollection SelectedDiag
        {
            get
            {
                if (Session["SelectedDiag"] == null)
                {
                    Session["SelectedDiag"] = new NursingDiagnosaCollection();
                }

                return (NursingDiagnosaCollection)Session["SelectedDiag"];
            }
            set
            {
                Session["SelectedDiag"] = value;
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
                SelectedDiag = null;
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

            var nsDTColl = new NursingDiagnosaTransDTCollection();

            // etio
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
                            var etio = nsDTColl.AddNew();
                            etio.NursingDiagnosaID = et.NursingDiagnosaID;
                            var txt = x.FindControl("txtNursingDiagnosaName") as RadTextBox;
                            //etio.NursingDiagnosaName = et.NursingDiagnosaName;
                            etio.NursingDiagnosaName = txt.Text;

                            etio.SRNursingDiagnosaLevel = et.SRNursingDiagnosaLevel;
                            etio.NursingDiagnosaParentID = et.NursingDiagnosaParentID;

                            //etio.S = txtPrefix.Text;
                            //etio.O = txtSuffix.Text;

                            etio.TmpNursingDiagnosaID = string.Empty;
                        }
                    }
                }
            }

            // diag
            foreach (var diag in SelectedDiag) {
                var nsDT = nsDTColl.AddNew();
                nsDT.NursingDiagnosaID = diag.NursingDiagnosaID;

                foreach (GridDataItem x in gridSelectedDiag.MasterTableView.Items)
                {
                    if (x.GetDataKeyValue("NursingDiagnosaID").ToString() == diag.NursingDiagnosaID) {
                        var txtNursingDiagnosaName = x.FindControl("txtNursingDiagnosaName") as RadTextBox;
                        if (txtNursingDiagnosaName != null)
                        {
                            nsDT.NursingDiagnosaName = txtNursingDiagnosaName.Text;
                        }
                        break;
                    }
                }

                nsDT.SRNursingDiagnosaLevel = diag.SRNursingDiagnosaLevel;
                nsDT.TmpNursingDiagnosaID = string.Empty;
            }

            if (nsDTColl.Count > 0) {
                Common.NursingCare.SaveDiagL11(
                    RegNo, 
                    nsDTColl.Where(n => n.SRNursingDiagnosaLevel == "11").ToArray(),
                    nsDTColl.Where(n => n.SRNursingDiagnosaLevel == "10").ToArray(),
                    "04"); 
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
            //foreach (GridDataItem x in gridEtiologyNew.MasterTableView.Items)
            //{
            //    var chk = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
            //    if (chk != null)
            //    {
            //        if (chk.Checked)
            //        {
            //            return;
            //        }
            //    }
            //}
            //args.IsValid = false;
            //((CustomValidator)source).ErrorMessage = "Please choose at least one etiology";
            //return;


            if (SelectedDiag.Count == 0) {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Please choose at least one etiology";
                return;
            }
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
            var nsid = (chk.NamingContainer as GridDataItem).GetDataKeyValue("NursingDiagnosaID").ToString();
            if (chk.Checked)
            {
                var etio = new NursingDiagnosa();
                if (etio.LoadByPrimaryKey(nsid))
                {
                    var ns = new NursingDiagnosa();
                    if (ns.LoadByPrimaryKey(etio.NursingDiagnosaParentID))
                    {
                        // jika sudah ada tidak perlu ditambahkan
                        if (!SelectedDiag.Any(x => x.NursingDiagnosaID == ns.NursingDiagnosaID)) {
                            SelectedDiag.AttachEntity(ns);
                        }
                    }
                }
            }
            else
            {
                // remove
                //var ns = SelectedDiag.Where(d => d.NursingDiagnosaID == nsid).FirstOrDefault();
                //if (ns != null) SelectedDiag.DetachEntity(ns);

                List<string> etioIds = new List<string>();
                foreach (GridDataItem x in (chk.NamingContainer as GridDataItem).OwnerTableView.Items) {
                    var chka = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
                    if (chka != null)
                    {
                        if (chka.Checked)
                        {
                            etioIds.Add(x.GetDataKeyValue("NursingDiagnosaID").ToString());
                        }
                    }
                }

                if (etioIds.Count == 0) etioIds.Add("xxx");
                var selectedD = new NursingDiagnosaCollection();
                var dx = new NursingDiagnosaQuery("dx");
                var et = new NursingDiagnosaQuery("et");
                dx.InnerJoin(et).On(dx.NursingDiagnosaID == et.NursingDiagnosaParentID)
                    .Where(et.NursingDiagnosaID.In(etioIds))
                    .Select(dx);
                dx.es.Distinct = true;
                selectedD.Load(dx);

                foreach (var toRemove in SelectedDiag.Where(s => !selectedD.Select(l => l.NursingDiagnosaID).Contains(s.NursingDiagnosaID))) {
                    SelectedDiag.DetachEntity(toRemove);
                }
            }

            gridSelectedDiag.Rebind();
        }

        protected void gridSelectedDiag_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            gridSelectedDiag.DataSource = SelectedDiag;
        }
    }
}
