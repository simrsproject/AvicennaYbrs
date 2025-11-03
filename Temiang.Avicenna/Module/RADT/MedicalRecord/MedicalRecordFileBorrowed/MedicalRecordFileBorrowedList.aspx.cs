using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class MedicalRecordFileBorrowedList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "MedicalRecordFileBorrowedSearch.aspx";
            UrlPageDetail = "MedicalRecordFileBorrowedDetail.aspx";
            ProgramID = AppConstant.Program.MedicalRecordFileBorrowed;

            this.WindowSearch.Height = 400;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(MedicalRecordFileBorrowedMetadata.ColumnNames.TransactionNo).ToString();
            Page.Response.Redirect("MedicalRecordFileBorrowedDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = MedicalRecordFileBorroweds;
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = e.Item as GridDataItem;
                if (dataItem.OwnerTableView.Name == "master" && dataItem["LoB"].Text != "0")
                {
                    dataItem.ToolTip = dataItem["LoB"].Text + " Day(s)";
                }
            }
        }

        private DataTable MedicalRecordFileBorroweds
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                MedicalRecordFileBorrowedQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (MedicalRecordFileBorrowedQuery)Session[SessionNameForQuery];
                else
                {
                    query = new MedicalRecordFileBorrowedQuery("a");
                    var pat = new PatientQuery("c");
                    var reg = new RegistrationQuery("b");
                    var su = new ServiceUnitQuery("d");
                    var usr = new AppUserQuery("e");
                    var usrg = new AppUserQuery("f");
                    var sal = new AppStandardReferenceItemQuery("sal");

                    query.InnerJoin(pat).On(query.PatientID == pat.PatientID);
                    query.LeftJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                    query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);
                    query.LeftJoin(usr).On(query.NameOfRecipientID == usr.UserID);
                    query.LeftJoin(usrg).On(query.NameOfGivenID == usrg.UserID);
                    query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == pat.SRSalutation);
                    query.Select
                        (
                            query.TransactionNo,
                            query.PatientID,
                            query.RegistrationNo,
                            pat.MedicalNo,
                            pat.PatientName,
                            query.DateOfBorrowing,
                            query.DateOfReturn,
                            query.ServiceUnitID,
                            su.ServiceUnitName,
                            query.NameOfTheBorrower,
                            query.SRForThePurposesOf,
                            query.Notes,
                            query.NameOfRecipientID,
                            usr.UserName.As("ReceivedBy"),
                            usrg.UserName.As("GivenBy"),
                            query.Duration,
                            sal.ItemName.As("SalutationName"),
                            @"<CASE WHEN a.DateOfReturn IS NULL THEN (DATEDIFF(DAY, a.DateOfBorrowing, GETDATE())) - a.Duration ELSE 0 END AS 'OrderBy'>",
                            @"<CASE WHEN a.DateOfReturn IS NULL THEN CASE WHEN ((DATEDIFF(DAY, a.DateOfBorrowing, GETDATE())) - a.Duration) > 0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END AS 'IsWarnedVisible'>",
                            @"<DATEADD(DAY, a.Duration, a.DateOfBorrowing) AS 'ShouldBeReturnDate'>",
                            @"<CASE WHEN a.DateOfReturn IS NULL THEN CASE WHEN ((DATEDIFF(DAY, a.DateOfBorrowing, GETDATE())) - a.Duration) > 0 THEN ((DATEDIFF(DAY, a.DateOfBorrowing, GETDATE())) - a.Duration) ELSE 0 END ELSE 0 END AS 'LoB'>"
                        );
                    //query.OrderBy(query.TransactionNo.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();

                dtb.DefaultView.Sort = "OrderBy DESC, DateOfBorrowing DESC, TransactionNo DESC";
                dtb.DefaultView.ToTable();

                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
