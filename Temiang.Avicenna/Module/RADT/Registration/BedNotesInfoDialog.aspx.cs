using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class BedNotesInfoDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Admitting;
            var btkOk = (Button)Helper.FindControlRecursive(Master, "btnOk");
            var btkCancel = (Button)Helper.FindControlRecursive(Master, "btnCancel");
            btkOk.Visible = false;
            btkCancel.Visible = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var bed = new Bed();
                bed.LoadByPrimaryKey(Request.QueryString["id"]);

                txtBedID.Text = bed.BedID;
                txtNotes.Text = bed.Notes;
            }
        }
    }
}