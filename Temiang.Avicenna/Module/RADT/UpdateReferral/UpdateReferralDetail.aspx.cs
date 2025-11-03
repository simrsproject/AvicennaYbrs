using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class UpdateReferralDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.UpdateReferral;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtRegistrationNo.Text = Request.QueryString["regno"];
                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName.Text = pat.PatientName;
                txtGender.Text = pat.Sex;
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));

                txtRegistrationDate.SelectedDate = reg.RegistrationDate;
                txtRegistrationTime.Text = reg.RegistrationTime;
                var su = new ServiceUnit();
                if (su.LoadByPrimaryKey(reg.ServiceUnitID))
                    txtServiceUnitName.Text = su.ServiceUnitName;
                else txtServiceUnitName.Text = string.Empty;

                var room = new ServiceRoom();
                if (room.LoadByPrimaryKey(reg.RoomID))
                    txtRoomBed.Text = room.RoomName + (!string.IsNullOrEmpty(reg.BedID) ? " / " + reg.BedID : "");
                else
                    txtRoomBed.Text = reg.BedID;

                var par = new Paramedic();
                if (par.LoadByPrimaryKey(reg.ParamedicID))
                    txtParamedicName.Text = par.ParamedicName;
                else txtParamedicName.Text = string.Empty;

                ComboBox.StandartReferenceItemSelectOne(cboSRReferralGroup, "ReferralGroup", reg.SRReferralGroup);

                if (!string.IsNullOrEmpty(reg.ReferralID))
                {
                    var qref = new ReferralQuery();
                    qref.Select(qref.ReferralID, qref.ReferralName);
                    qref.Where(qref.ReferralID == reg.str.ReferralID);
                    DataTable dtbg = qref.LoadDataTable();
                    cboReferralID.DataSource = dtbg;
                    cboReferralID.DataBind();
                    cboReferralID.SelectedValue = reg.ReferralID;
                    cboReferralID.Text = dtbg.Rows[0]["ReferralName"].ToString();
                }
                else
                {
                    cboReferralID.DataSource = null;
                    cboReferralID.DataBind();
                    cboReferralID.SelectedValue = string.Empty;
                    cboReferralID.Text = string.Empty;
                }
                txtReferralName.Text = reg.ReferralName;
                ReadOnlyReferralName();

            }
        }

        protected void cboSRReferralGroup_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }

        protected void cboSRReferralGroup_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested((RadComboBox)o, "ReferralGroup", e.Text);
        }

        protected void cboSRReferralGroup_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboReferralID.Items.Clear();
            cboReferralID.SelectedValue = string.Empty;
            cboReferralID.Text = string.Empty;
            txtReferralName.ReadOnly = false;
            txtReferralName.Text = string.Empty;
        }

        protected void cboReferralID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ReferralName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ReferralID"].ToString();
        }

        protected void cboReferralID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new ReferralQuery();
            query.es.Top = 30;
            query.Where
                (
                    query.ReferralName.Like(string.Format("%{0}%", e.Text)),
                    query.IsActive == true, query.SRReferralGroup == cboSRReferralGroup.SelectedValue
                );
            query.OrderBy(query.ReferralName.Ascending);

            cboReferralID.DataSource = query.LoadDataTable();
            cboReferralID.DataBind();
        }

        protected void cboReferralID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ReadOnlyReferralName();
            txtReferralName.Text = string.Empty;
            if (string.IsNullOrEmpty(cboSRReferralGroup.SelectedValue))
            {
                var r = new Referral();
                r.LoadByPrimaryKey(e.Value);
                ComboBox.StandartReferenceItemSelectOne(cboSRReferralGroup, "ReferralGroup", r.SRReferralGroup);
            }
        }

        private void ReadOnlyReferralName()
        {
            var referral = new Referral();
            if (referral.LoadByPrimaryKey(cboReferralID.SelectedValue))
            {
                var std = new AppStandardReferenceItem();
                std.LoadByPrimaryKey("ReferralGroup", referral.SRReferralGroup);
                txtReferralName.ReadOnly = (std.ReferenceID == "JM");
            }
            else
                txtReferralName.ReadOnly = false;
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            //update registration
            var entity = new Registration();
            entity.LoadByPrimaryKey(Request.QueryString["regno"]);
            entity.SRReferralGroup = cboSRReferralGroup.SelectedValue;
            entity.ReferralID = cboReferralID.SelectedValue;
            entity.ReferralName = txtReferralName.Text;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                var collmerge = new MergeBillingCollection();
                collmerge.Query.Where(collmerge.Query.FromRegistrationNo == Request.QueryString["regno"]);
                collmerge.LoadAll();

                foreach (var merge in collmerge)
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(merge.RegistrationNo);
                    reg.SRReferralGroup = cboSRReferralGroup.SelectedValue;
                    reg.ReferralID = cboReferralID.SelectedValue;
                    reg.ReferralName = txtReferralName.Text;
                    reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    
                    reg.Save();
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }
    }
}
