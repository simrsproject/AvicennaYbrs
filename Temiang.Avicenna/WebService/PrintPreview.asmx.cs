using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for PrintPreview
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PrintPreview : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public void General(string programID, string parVal)
        {
            var jobParameters = new PrintJobParameterCollection();
            if (!string.IsNullOrEmpty(parVal) && parVal.Contains(":"))
            {
                if (parVal.Contains(";"))
                {
                    var vals = parVal.Split(';');
                    foreach (var val in vals)
                    {
                        var valItem = val.Split(':');
                        var jobParameter = jobParameters.AddNew();
                        jobParameter.Name = valItem[0];
                        jobParameter.ValueString = valItem[1];
                    }
                }
                else
                {
                    var valItem = parVal.Split(':');
                    var jobParameter = jobParameters.AddNew();
                    jobParameter.Name = valItem[0];
                    jobParameter.ValueString = valItem[1];
                }
            }

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = programID;
        }
    }
}
