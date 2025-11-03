using System;
using System.Data;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges.Dispensary
{
    public partial class ViewAllergy : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PrescriptionSales;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regno"]);

                //allergy data
                var allergyCollection = new PatientAllergyCollection();
                allergyCollection.Query.Where(allergyCollection.Query.PatientID == reg.PatientID);
                allergyCollection.Query.OrderBy(allergyCollection.Query.AllergyGroup.Ascending);
                allergyCollection.LoadAll();

                var query = new AppStandardReferenceItemQuery("a");
                query.Select
                    (
                        query.StandardReferenceID,
                        query.ItemID,
                        query.ItemName
                    );
                query.Where(query.ItemID.Like("%Allergen%"));

                var tbl = AllergyTable(query.LoadDataTable());

                foreach (DataRow row in tbl.Rows)
                {
                    foreach (var all in allergyCollection.Where(all => ((string)row[1] == all.AllergyGroup) &&
                                                                       ((string)row[2] == all.Allergen)))
                    {
                        row[4] = all.DescAndReaction;
                        break;
                    }
                }

                tbl.AcceptChanges();

                ViewState["collPatientAllergy" + Request.UserHostName] = tbl;
            }
        }

        protected void grdPatientAllergy_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPatientAllergy.DataSource = ViewState["collPatientAllergy" + Request.UserHostName];
            grdPatientAllergy.MasterTableView.GroupsDefaultExpanded = false;
        }

        private DataTable AllergyTable(DataTable table)
        {
            var tbl = new DataTable();

            tbl.Columns.Add("Group", typeof(string));
            tbl.Columns.Add("StandardReferenceID", typeof(string));
            tbl.Columns.Add("ItemID", typeof(string));
            tbl.Columns.Add("ItemName", typeof(string));
            tbl.Columns.Add("DescAndReaction", typeof(string));

            foreach (DataRow row in table.Rows)
            {
                tbl.Rows.Add(WordProcessing((string)row[0]), row[0], row[1], row[2], string.Empty);
            }

            return tbl;
        }

        private static string WordProcessing(string value)
        {
            var capital = string.Empty;
            var index = 0;
            foreach (var c in value)
            {
                if (Char.IsUpper(c) && index > 0)
                {
                    capital = c.ToString();
                    break;
                }

                index++;
            }

            return !capital.Equals(string.Empty) ? value.Insert(index, " ") : value;
        }
    }
}