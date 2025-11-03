using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;

/// <summary>
/// Summary description for BaseReportCtl
/// </summary>
/// 
namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public class BaseOptionCtl : System.Web.UI.UserControl
    {
        public virtual PrintJobParameterCollection PrintJobParameters()
        {
            return null;
        }
        public virtual string ParameterCaption
        {
            get { return string.Empty; }
            set { }
        }
        public virtual string ReportSubTitle
        {
            get { return string.Empty; }
        }
        public virtual string ReferenceID
        {
            get { return string.Empty; }
            set { }
        }
    }
}