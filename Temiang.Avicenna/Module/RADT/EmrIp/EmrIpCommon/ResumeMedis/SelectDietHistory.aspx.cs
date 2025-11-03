using System;
using System.Collections.Generic;
using System.Web.UI;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.ResumeMedis
{
    public partial class SelectDietHistory : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Title = "Select Diet Patient";
                ButtonOk.OnClientClick = "CloseAndReturnValue();return false;";
            }
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = DietHist(MergeRegistrations);
        }
        private class SelectedItem
        {
            public string Description { get; set; }
            public SelectedItem(string description)
            {
                Description = description;
            }
        }

        private List<SelectedItem> DietHist(List<string> mergeRegistrations)
        {
            var query = new DietPatientQuery("a");
            var dit = new DietPatientItemQuery("dit");
            query.InnerJoin(dit).On(query.TransactionNo == dit.TransactionNo);
            var diet = new DietQuery("p");
            query.InnerJoin(diet).On(dit.DietID == diet.DietID);
            query.Where(query.RegistrationNo.In(MergeRegistrations));
            query.Select(diet.DietName, dit.Calorie, dit.Protein, dit.Fat, dit.Carbohydrate, dit.Salt, dit.Notes);
            query.OrderBy(dit.TransactionNo.Ascending, diet.DietName.Ascending);

            var dtbSurgery = query.LoadDataTable();
            var listItems = new List<SelectedItem>();

            foreach (DataRow row in dtbSurgery.Rows)
            {
                listItems.Add(new SelectedItem(string.Format("{0} ({5}) Cal:{1:N2}, Pro:{2:N2}, Car:{3:N2}, Salt:{4:N2}",
                    row["DietName"], row["Calorie"], row["Protein"], row["Carbohydrate"],
                    row["Salt"], row["Notes"])));
            }
            return listItems;
        }

    }
}