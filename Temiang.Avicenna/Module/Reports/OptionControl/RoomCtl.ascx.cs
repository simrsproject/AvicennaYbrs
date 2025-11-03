using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class RoomCtl : BaseOptionCtl
    {

        #region ComboBox
        protected void cboOutPatientRoomID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.OutPatientRoomItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboOutPatientRoomID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.OutPatientRoomItemDataBound(e);
        }
        #endregion

        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_OutPatienRoomID", cboOutPatientRoomID.SelectedValue);

            //Retun List
            return parameters;
        }

        public override string ParameterCaption
        {
            get { return lblOutPatientRoomID.Text; }
            set { lblOutPatientRoomID.Text = value; }
        }
        public override string ReportSubTitle
        {
            get
            {
                return string.Format("Out Patient Room : {0}", cboOutPatientRoomID.SelectedValue);
            }
        }

    }
}