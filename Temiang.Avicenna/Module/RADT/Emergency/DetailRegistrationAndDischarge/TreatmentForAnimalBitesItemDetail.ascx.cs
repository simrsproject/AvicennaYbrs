using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
namespace Temiang.Avicenna.Module.RADT.Emergency
{
    public partial class TreatmentForAnimalBitesItemDetail : BaseUserControl
    {
      public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRTreatmentForAnimalBites, AppEnum.StandardReference.TreatmentForAnimalBites);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                
            }

            ViewState["IsNewRecord"] = false;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (TreatmentForAnimalBitesCollection)Session["collTreatmentForAnimalBites"];

                string itemId = cboSRTreatmentForAnimalBites.SelectedValue;
                bool isExist = false;

                foreach (TreatmentForAnimalBites item in coll)
                {
                    if (item.SRTreatmentForAnimalBites.Equals(itemId))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Therapy : {0} already exist", cboSRTreatmentForAnimalBites.Text);
                }
            }
        }

        public String SRTreatmentForAnimalBites
        {
            get { return cboSRTreatmentForAnimalBites.SelectedValue; }
        }

        public String TreatmentForAnimalBitesName
        {
            get { return cboSRTreatmentForAnimalBites.Text; }
        }
    }
}