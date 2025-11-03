using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class ExamOrderImageZoomView : BasePageDialog
    {
        public string TransactionNo
        {
            get
            {
                return Request.QueryString["trno"];
            }
        }

        public string SequenceNo
        {
            get
            {
                return Request.QueryString["seqno"];
            }
        }
        public int ImageNo
        {
            get
            {
                return Request.QueryString["imgno"].ToInt();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonOk.Visible = false;
            ButtonCancel.Text = "Close";
            if (SequenceNo == "PAT")
            {
                var ent = new PathologyAnatomyImage();
                if (ent.LoadByPrimaryKey(TransactionNo, ImageNo))
                {
                    imgDocumentImage.DataValue = ent.DocumentImage;
                }
            }
            else
            {
                var ent = new TransChargesItemImage();
                if (ent.LoadByPrimaryKey(TransactionNo, SequenceNo, ImageNo))
                {
                    imgDocumentImage.DataValue = ent.DocumentImage;
                }
            }
        }
    }
}