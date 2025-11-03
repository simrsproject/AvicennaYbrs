using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PpiProcedureItemDetail : BaseUserControl
    {
        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRWoundClassification, AppEnum.StandardReference.WoundClassification);
            StandardReference.InitializeIncludeSpace(cboSRAsaScore, AppEnum.StandardReference.AsaScore);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;
            ViewState["BookingNo"] = (String)DataBinder.Eval(DataItem, PpiProcedureSurveillanceMetadata.ColumnNames.BookingNo);

            var booking = new ServiceUnitBooking();
            booking.LoadByPrimaryKey(ViewState["BookingNo"].ToString());
            txtDiagnose.Text = booking.Diagnose;
            txtRealizationDateFrom.SelectedDate = booking.RealizationDateTimeFrom.Value.Date;
            txtRealizationTimeFrom.SelectedDate = booking.RealizationDateTimeFrom.Value;
            txtRealizationDateTo.SelectedDate = booking.RealizationDateTimeTo.Value.Date;
            txtRealizationTimeTo.SelectedDate = booking.RealizationDateTimeTo.Value;
            cboSRWoundClassification.SelectedValue = (String)DataBinder.Eval(DataItem, PpiProcedureSurveillanceMetadata.ColumnNames.SRWoundClassification);
            rblIsCito.SelectedIndex = booking.IsCito ?? false ? 0 : 1;
            cboSRAsaScore.SelectedValue = (String)DataBinder.Eval(DataItem, PpiProcedureSurveillanceMetadata.ColumnNames.SRAsaScore);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
        }

        #region Properties for return entry value

        public String SRWoundClassification
        {
            get { return cboSRWoundClassification.SelectedValue; }
        }

        public String WoundClassificationName
        {
            get { return cboSRWoundClassification.Text; }
        }

        public String SRAsaScore
        {
            get { return cboSRAsaScore.SelectedValue; }
        }

        public String AsaScoreName
        {
            get { return cboSRAsaScore.Text; }
        }

        #endregion
    }
}