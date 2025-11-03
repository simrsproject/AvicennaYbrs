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

namespace Temiang.Avicenna.Module.RADT.EmrIp.MainContent
{
    public partial class MedicationCtl : BaseMainContentCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string GridClientID
        {
            get { return grdMedicationReceive.ClientID; }
        }

        #region MedicationReceive
        protected void grdMedicationReceive_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {

            }
            else if (e.CommandName == "Rebind")
            {
            }
        }

        protected void grdMedicationReceive_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdMedicationReceive.DataSource = MedicationReceiveDataTable(RegistrationNo);
        }

        protected void grdMedicationReceive_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var medicationReceiveNo = Convert.ToDecimal(item.OwnerTableView.DataKeyValues[item.ItemIndex]["MedicationReceiveNo"]).ToInt();

            var nmd = new MedicationReceive();
            if (nmd.LoadByPrimaryKey(medicationReceiveNo))
            {
                nmd.IsVoid = true;
                nmd.Save();
            }

            grdMedicationReceive.Rebind();
        }

        private DataTable MedicationReceiveDataTable(string registrationNo)
        {
            var query = new MedicationReceiveQuery("a");
            var cm = new ConsumeMethodQuery("cm");
            query.LeftJoin(cm).On(query.SRConsumeMethod == cm.SRConsumeMethod);

            query.Select(query.SelectAll(), cm.SRConsumeMethodName);

            query.Where(query.RegistrationNo == registrationNo);
            query.OrderBy(query.MedicationReceiveNo.Descending);
            var dtb = query.LoadDataTable();

            return dtb;
        }

        #endregion


        protected void grdMedicationReceive_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string medicationReceiveNo = dataItem.GetDataKeyValue("MedicationReceiveNo").ToString();

            if (e.DetailTableView.Name.Equals("grdMedicationReceiveUsed"))
            {
                var query = new MedicationReceiveUsedQuery("a");
                var medic = new ParamedicQuery("p");

                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.Select(query.SelectAll(), medic.ParamedicName);
                query.Where(query.MedicationReceiveNo == medicationReceiveNo);
                query.OrderBy(query.SequenceNo.Descending);

                e.DetailTableView.DataSource = query.LoadDataTable();
            }
        }

        protected object MedicationRealizationHtml(object medrecno, object conMethod, object isVoid, object oBalanceQty, object oConsumeQty, int dayFromCurrentDay,object oIsContinue)
        {
            if (oIsContinue==DBNull.Value || Convert.ToBoolean(oIsContinue) == false)
                return string.Empty;

            var balanceQty = (decimal)oBalanceQty;
            if ((isVoid != DBNull.Value && isVoid.Equals(true))) return string.Empty;

            // Cek pemakaian jumlah hari ini dan tambahkan ke balance
            var dateSelected = DateTime.Today;

            var qr = new MedicationReceiveUsedQuery();
            qr.Select(qr.Qty.Sum().As("QtyUsed"));
            qr.Where(qr.MedicationReceiveNo == medrecno,qr.RealizedDateTime.IsNotNull(), qr.ScheduleDateTime.Between(dateSelected, Convert.ToDateTime(dateSelected).AddDays(1)));
            var dtbCount = qr.LoadDataTable();
            if (dtbCount.Rows.Count > 0 && dtbCount.Rows[0][0] != DBNull.Value)
                balanceQty = balanceQty +  (decimal)dtbCount.Rows[0][0];

            if (balanceQty <= 0) return string.Empty;

            var strb = new StringBuilder();

            // Untuk pemberian obat diluar schedule
            if (dayFromCurrentDay==0)
                strb.AppendFormat("<a style='vertical-align: text-bottom;' href=\"#\" onclick=\"javascript:entryMedicationReceiveUsed('new', '{0}', 0,''); return false;\"><img src=\"{1}/Images/Toolbar/insert16.png\"  alt=\"New\" /></a>&nbsp;|", medrecno,Helper.UrlRoot());

            var consumeMethodId = (string)conMethod;
            var cm = new ConsumeMethod();
            cm.LoadByPrimaryKey(consumeMethodId);

            var date = DateTime.Today.AddDays(dayFromCurrentDay);

            // Tidak diketahui aturan pakainya
            if (cm.IterationQty == null || cm.IterationQty < 0)
            {
                var addScript = strb.ToString();
                return string.IsNullOrEmpty(addScript) ? string.Empty : addScript.Substring(0, addScript.Length - 2);
            }

            var consumeQty = (decimal)oConsumeQty;
            balanceQty = balanceQty - (dayFromCurrentDay * (decimal)cm.IterationQty * consumeQty);

            if (balanceQty <= 0) return string.Empty;

            var isScriptMedicationCreated = false;
            for (int i = 0; i < cm.IterationQty; i++)
            {
                var timeSchedule = cm.GetColumn(string.Format("Time{0:00}", i + 1)).ToString();
                var timeSchedules = timeSchedule.Split(':');

                // Hanya dihari+0 bisa medication
                if (dayFromCurrentDay == 0)
                {
                    // Check status
                    var scheduleDateTime = new DateTime(date.Year, date.Month, date.Day, timeSchedules[0].ToInt(),
                        timeSchedules[1].ToInt(), 0);
                    qr = new MedicationReceiveUsedQuery();
                    var used = new MedicationReceiveUsed();
                    qr.Where(qr.MedicationReceiveNo == medrecno, qr.ScheduleDateTime == scheduleDateTime);
                    qr.es.Top = 1;

                    // Hanya bisa 1 schedule yg dientry
                    if (isScriptMedicationCreated || (used.Load(qr) && used.RealizedDateTime != null))
                        strb.AppendFormat(
                            "&nbsp;<img style='vertical-align: text-bottom;' src=\"{1}/Images/Toolbar/post16_d.png\"  alt=\"New\" />&nbsp;{0}&nbsp;|",
                            timeSchedule, Helper.UrlRoot());
                    else
                    {
                        isScriptMedicationCreated = true;
                        strb.AppendFormat(
                            "&nbsp;<a  href=\"#\" onclick=\"javascript:entryMedicationReceiveUsed('new', '{0}', '{1}', '{2}'); return false;\"><img style='vertical-align: text-bottom;' src=\"{3}/Images/Toolbar/post16.png\"  alt=\"New\" />&nbsp;{2}</a>&nbsp;|",
                            medrecno, used.SequenceNo ?? 0, timeSchedule, Helper.UrlRoot());
                    }

                }
                else
                {
                    strb.AppendFormat("&nbsp;{0}&nbsp;|", timeSchedule);
                }

                balanceQty = balanceQty - consumeQty;
                if (balanceQty <= 0)
                {
                    break;
                }
            }

            var retval = strb.ToString();
            return retval.Substring(0, retval.Length-2);
        }

        protected void grdMedicationReceive_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (item.GetDataKeyValue("IsVoid") == DBNull.Value) return;
                var isVoid = Convert.ToBoolean(item.GetDataKeyValue("IsVoid"));
                //TableCell cell = item["ItemName"];
                if (isVoid)
                {
                    item.Style.Add(HtmlTextWriterStyle.TextDecoration, "line-through");
                    item.Style.Add(HtmlTextWriterStyle.Color, "gray");
                }
            }
        }
    }
}