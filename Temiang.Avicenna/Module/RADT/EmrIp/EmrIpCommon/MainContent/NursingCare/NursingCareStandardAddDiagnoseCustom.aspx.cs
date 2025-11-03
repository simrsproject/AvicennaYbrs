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
using System.Text.RegularExpressions;

namespace Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NursingCare
{
    public partial class NursingCareStandardAddDiagnoseCustom : BasePageDialog
    {
        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        public string DiagType {
            get {
                return Request.QueryString["diagTypes"];
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
                ComboBox.StandardReferenceItem(cboNsTypeID, AppEnum.StandardReference.NsType.ToString(), true, DiagType);
                if (DiagType == "03") cboNsTypeID.SelectedValue = "05";

                lbtnSearchAssessment.Visible = (DiagType == "03");
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
                            //etio.NursingDiagnosaName = txt.Text;
                            etio.NursingDiagnosaName = Regex.Replace(txt.Text.Trim(), " +", " ");

                            etio.SRNursingDiagnosaLevel = et.SRNursingDiagnosaLevel;
                            etio.NursingDiagnosaParentID = et.NursingDiagnosaParentID;

                            //etio.S = txtNewDS.Text;
                            etio.S = Regex.Replace(txtNewDS.Text.Trim(), " +", " ");
                            //etio.O = txtNewDO.Text;
                            etio.O = Regex.Replace(txtNewDO.Text.Trim(), " +", " ");

                            etio.TmpNursingDiagnosaID = string.Empty;
                        }
                    }
                }
            }

            if (nsDT.Count > 0) {
                Common.NursingCare.SaveDiagL11(RegNo, nsDT.ToArray(), null, cboNsTypeID.SelectedValue);
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
            if (string.IsNullOrEmpty(txtNewDO.Text))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "DO can not be empty";
                return;
            }

            if (string.IsNullOrEmpty(cboNsTypeID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Please select nursing type";
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
                nsHD.TransactionNo, txtNewDS.Text, txtNewDO.Text, ExceptionIDs("10"), cboNsTypeID.SelectedValue, chkShowAllDiag.Checked);
            gridEtiologyNew.DataSource = ls;
        }

        protected void lbtnRefresh_Click(Object sender, EventArgs e)
        {
            gridEtiologyNew.Rebind();
        }
    }
}
