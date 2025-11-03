using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs.ApotekOnline
{
    public partial class ApotekOnlineDashboard : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int currentYear = DateTime.Now.Year;
                int currentMonth = DateTime.Now.Month;
                for (int i = 0; i < 15; i++)
                {
                    string yearText = (currentYear - 6 + i).ToString();
                    cboPeriodYear.Items.Add(new RadComboBoxItem(yearText, yearText));
                }

                cboPeriodYear.SelectedValue = currentYear.ToString();

                if (cboMonth.Items.Count == 0)
                {
                    for (int m = 1; m <= 12; m++)
                    {
                        string monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m);
                        cboMonth.Items.Add(new RadComboBoxItem(monthName, m.ToString()));
                    }
                }

                cboMonth.SelectedValue = currentMonth.ToString();
            }
        }

        protected void btnDataKlaim_Click(object sender, EventArgs e)
        {
            try
            {
                string bulan = cboMonth.SelectedValue;
                string tahun = cboPeriodYear.SelectedValue;
                string jenisObat = cboJnsObt.SelectedValue;
                string status = cboStatus.SelectedValue;

                var svc = new Common.BPJS.Apotek.Service();
                var response = svc.GetDataKlaim(bulan, tahun, jenisObat, status);

                if (response != null && response.MetaData != null && response.MetaData.IsApolValid)
                {
                    var list = response.Response?.Listsep;

                    if (list != null && list.Count > 0)
                    {
                        grdListKlaim.DataSource = list;
                    }
                    else
                    {
                        grdListKlaim.DataSource = GetEmptyTable();
                    }
                }
                else
                {
                    string errorMsg = response?.MetaData?.Code + " - " + response?.MetaData?.Message;
                    grdListKlaim.DataSource = GetErrorTable(errorMsg);
                }
            }
            catch (Exception ex)
            {
                grdListKlaim.DataSource = GetErrorTable("Exception: " + ex.Message);
            }

            grdListKlaim.DataBind();
        }


        private DataTable GetEmptyTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Nosepapotek", typeof(string)); // wajib sesuai DataKeyNames
            return dt;
        }

        private DataTable GetErrorTable(string message)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Nosepapotek", typeof(string));
            dt.Columns.Add("Pesan", typeof(string));

            var row = dt.NewRow();
            row["Nosepapotek"] = ""; // kosong untuk key
            row["Pesan"] = message;
            dt.Rows.Add(row);

            return dt;
        }


        private void HandleException(Exception ex)
        {
            string errorMessage = ex.Message;

            var serverError = ex.InnerException as WebException;
            if (serverError != null)
            {
                var response = serverError.Response as HttpWebResponse;
                if (response != null && response.StatusCode != HttpStatusCode.OK)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        errorMessage = reader.ReadToEnd();
                    }
                }
            }
            Response.Write(errorMessage);
        }

    }
}