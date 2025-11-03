using Telerik.Web.UI;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.MasterPage
{
    ///<summary>
    ///
    ///</summary>
    public partial class MasterDialogList : System.Web.UI.MasterPage
    {

        ///<summary>
        ///
        ///</summary>
        protected BasePageDialogList BasePageDialogListCurrent
        {
            get {return (BasePageDialogList)cphList.Page; }
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
                return Helper.FindFirstRadGridControl(cphList);
            }
        }
    }

}
