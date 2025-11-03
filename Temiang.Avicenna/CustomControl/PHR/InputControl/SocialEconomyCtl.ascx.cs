using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Phr;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.CustomControl.Phr.InputControl
{
    public partial class SocialEconomyCtl : BasePhrCtl
    {
        protected override void OnPopulateEntryControl(Patient pat, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
            if (string.IsNullOrWhiteSpace(lastRegistrationNo) || reg.RegistrationNo == lastRegistrationNo)
            {
                ComboBox.SelectedValue(cboSRMaritalStatus, pat.SRMaritalStatus);
                ComboBox.SelectedValue(cboSRRelationshipQuality, pat.SRRelationshipQuality);
                ComboBox.SelectedValue(cboSRResidentialHome, pat.SRResidentialHome);
                ComboBox.SelectedValue(cboSROccupation, pat.SROccupation);
            }
            else
            {
                ComboBox.SelectedValue(cboSRMaritalStatus, reg.SRMaritalStatus);
                ComboBox.SelectedValue(cboSRRelationshipQuality, reg.SRRelationshipQuality);
                ComboBox.SelectedValue(cboSRResidentialHome, reg.SRResidentialHome);
                ComboBox.SelectedValue(cboSROccupation, reg.SROccupation);
            }
        }

        protected override void OnSetEntityValue(ValidateArgs args, Patient pat, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
            // Update Patient jika dari Reg terakhir
            if (string.IsNullOrWhiteSpace(lastRegistrationNo) || reg.RegistrationNo == lastRegistrationNo)
            {
                pat.LoadByPrimaryKey(reg.PatientID);
                pat.SRMaritalStatus = cboSRMaritalStatus.SelectedValue;
                pat.SRRelationshipQuality = cboSRRelationshipQuality.SelectedValue;
                pat.SRResidentialHome = cboSRResidentialHome.SelectedValue;
                pat.SROccupation = cboSROccupation.SelectedValue;
                pat.Save();
            }

            reg.SRMaritalStatus = cboSRMaritalStatus.SelectedValue;
            reg.SRRelationshipQuality = cboSRRelationshipQuality.SelectedValue;
            reg.SRResidentialHome = cboSRResidentialHome.SelectedValue;
            reg.SROccupation = cboSROccupation.SelectedValue;
            reg.Save();
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!IsPostBack)
            {
                ComboBox.StandardReferenceItem(cboSRMaritalStatus, "MaritalStatus");
                ComboBox.StandardReferenceItem(cboSROccupation, "Occupation");
                ComboBox.StandardReferenceItem(cboSRRelationshipQuality, "RelationshipQuality");
                ComboBox.StandardReferenceItem(cboSRResidentialHome, "ResidentialHome");
            }
        }


    }
}