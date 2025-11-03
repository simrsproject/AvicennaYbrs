using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalPhysicalSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.QueryPersonalPhysical;//TODO: Isi ProgramID

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSRPhysicalCharacteristic, AppEnum.StandardReference.PhysicalCharacteristic);
        }

        public override bool OnButtonOkClicked()
        {
            var measurement = new AppStandardReferenceItemQuery("d");
            var characteristic = new AppStandardReferenceItemQuery("c");
            var personal = new PersonalInfoQuery("b");
            var query = new PersonalPhysicalQuery("a");

            query.Select
                (
                   query.PersonalPhysicalID,
                   query.PersonID,
                   personal.EmployeeNumber,
                   personal.EmployeeName,
                   characteristic.ItemName.As("PhysicalCharacteristicName"),
                   query.PhysicalValue,
                   measurement.ItemName.As("MeasurementCodeName")
                );

            query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
            query.LeftJoin(characteristic).On
                    (
                        query.SRPhysicalCharacteristic == characteristic.ItemID &
                        characteristic.StandardReferenceID == "PhysicalCharacteristic"
                    );
            query.LeftJoin(measurement).On
                    (
                        query.SRMeasurementCode == measurement.ItemID &
                        measurement.StandardReferenceID == "MeasurementCode"
                    );

            query.OrderBy(query.PersonalPhysicalID.Ascending); //TODO: Betulkan ordernya

            if (!string.IsNullOrEmpty(cboSRPhysicalCharacteristic.SelectedValue))
                query.Where(query.SRPhysicalCharacteristic == cboSRPhysicalCharacteristic.SelectedValue);
            if (!string.IsNullOrEmpty(txtPhysicalValue.Text))
            {
                if (cboFilterPhysicalValue.SelectedIndex == 1)
                    query.Where(query.PhysicalValue == txtPhysicalValue.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPhysicalValue.Text);
                    query.Where(query.PhysicalValue.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
