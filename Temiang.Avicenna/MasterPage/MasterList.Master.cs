using Telerik.Web.UI;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.MasterPage
{
    ///<summary>
    ///
    ///</summary>
    public partial class MasterList : System.Web.UI.MasterPage
    {
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            if (Page.Request.QueryString["md"] != null && Page.Request.QueryString["md"] == "search")
                fw_WinSearch.VisibleOnPageLoad = true;
        }

        ///<summary>
        ///
        ///</summary>
        protected BasePageList BasePageListCurrent
        {
            get {return (BasePageList)ContentPlaceHolder1.Page; }
        }

        protected string FirstGridClientID
        {
            get
            {
                return FirstGrid.ClientID;
            }
        }

        protected RadGrid FirstGrid
        {
            get
            {
                return Helper.FindFirstRadGridControl(ContentPlaceHolder1);
            }
        }
    }

}
