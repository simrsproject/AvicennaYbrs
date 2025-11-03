using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using System.Data;
using System.Drawing;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT.Bpjs.Casemix
{
    public partial class CasemixCoverageList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CasemixCoverage;

            if (!IsPostBack)
            {
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }


        public bool AddNewException
        {
            get
            {
                //var casemix = new CasemixCoveredCollection();
                //casemix.Query.Load();

                //return casemix.Count == 0;

                return true;
            }
        }

        protected void grdException_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var casemix = new CasemixCoveredCollection();
            casemix.Query.Load();

            grdException.DataSource = casemix;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (sourceControl is RadGrid)
            {
                if (((RadGrid)sourceControl).ID == grdException.ID)
                    grdException.Rebind();
            }
        }
    }
}