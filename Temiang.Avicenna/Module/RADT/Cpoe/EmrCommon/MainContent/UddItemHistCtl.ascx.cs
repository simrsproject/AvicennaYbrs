using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.MainContent
{
    public partial class UddItemHistCtl : BaseMainContentCtl
    {
        const int MaxPrescriptionCount = 20;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string GridDiagAndPrescriptionClientID
        {
            get { return grdUddItem.ClientID; }
        }


        public String ReferFromRegistrationNo
        {
            set { ViewState["freg"] = value; }
            get { return Convert.ToString(ViewState["freg"]); }
        }
        public RadGrid GridDiagAndPrescription
        {
            get { return grdUddItem; }
        }

        protected string RegistrationType
        {
            get
            {
                // AMbil dari main page
                return Request.QueryString["rt"];
            }
        }

        public void GridUddItemDatabind()
        {
            grdUddItem.DataBind();
        }

        protected void grdUddItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            grdUddItem.DataSource = UddItemsData();
        }

        private UddItemCollection UddItemsData()
        {

            var coll = new UddItemCollection();

            var query = new UddItemQuery("a");
            var qItem = new ItemQuery("b");
            var cons = new ConsumeMethodQuery("cm");
            var emb = new EmbalaceQuery("e");
            var acdcpc = new AppStandardReferenceItemQuery("acdcpc");


            query.Select
                (
                    query,
                    qItem.ItemName.As("refToItem_ItemName"),
                    "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                    "<COALESCE(cm.SRConsumeMethodName,'') + ' ' + COALESCE(acdcpc.ItemName,'') as refToConsumeMethod_SRConsumeMethodName>",
                    emb.EmbalaceLabel.Coalesce("''").As("refToEmbalace_EmbalaceLabel")
                );
            query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
            query.LeftJoin(cons).On(query.SRConsumeMethod == cons.SRConsumeMethod);
            query.LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID);
            query.LeftJoin(acdcpc).On(query.AcPcDc == acdcpc.ItemID &&
                                      acdcpc.StandardReferenceID == AppEnum.StandardReference.MedicationConsume);

            query.Where(query.RegistrationNo == RegistrationNo);
            query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);
            coll.Load(query);

            return coll;
        }


        protected void grdUddItem_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "refresh":
                    {
                        grdUddItem.DataSource = null;
                        grdUddItem.Rebind();
                        break;
                    }
                case "StopUdd":
                    {
                        var keys = e.CommandArgument.ToString().Split('|');
                        StopUdd(keys[0], keys[1]);
                        grdUddItem.DataSource = null;
                        grdUddItem.Rebind();
                        break;
                    }
            }
        }

        private void StopUdd(string locationId, string seqNo)
        {
            var entity = new UddItem();
            if (entity.LoadByPrimaryKey(RegistrationNo, locationId, seqNo))
            {
                entity.IsStop = true;
                entity.Save();
            }

        }

        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            return "&nbsp;&nbsp;&nbsp;" + itemName;
        }
    }
}