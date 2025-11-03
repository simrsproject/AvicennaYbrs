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
    public partial class ApotekOnlineReference : BasePageDialog
    {
        protected void btnDpho_Click(object sender, EventArgs e)
        {
            grdDpho.Rebind();
        }

        protected void grdDpho_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "btnDpho_Click")
            {

            }
        }

        protected void grdDpho_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                var svc = new Common.BPJS.Apotek.Service();
                var response = svc.GetObatDpho();

                DataTable data = new DataTable();
                data.Columns.Add("Kodeobat");
                data.Columns.Add("Namaobat");
                data.Columns.Add("Prb");
                data.Columns.Add("Kronis");
                data.Columns.Add("Kemo");
                data.Columns.Add("Harga");
                data.Columns.Add("Restriksi");
                data.Columns.Add("Generik");
                data.Columns.Add("Aktif");

                if (response.MetaData.IsApolValid)
                {
                    foreach (var item in response.Response.List)
                    {
                        DataRow row = data.NewRow();
                        row["Kodeobat"] = item.Kodeobat;
                        row["Namaobat"] = item.Namaobat;
                        row["Prb"] = item.Prb;
                        row["Kronis"] = item.Kronis;
                        row["Kemo"] = item.Kemo;
                        double harga;
                        if (double.TryParse(item.Harga, out harga))
                        {
                            row["Harga"] = string.Format("Rp. {0:#,0.00}", harga);
                        }
                        else
                        {
                            row["Harga"] = item.Harga;
                        }
                        row["Restriksi"] = item.Restriksi;
                        row["Generik"] = item.Generik;
                        row["Aktif"] = item.Aktif;
                        data.Rows.Add(row);
                    }

                    grdDpho.DataSource = data;
                }
                else
                {
                    DataRow errorRow = data.NewRow();
                    errorRow["Kodeobat"] = response.MetaData.Code + " - " + response.MetaData.Message;
                    data.Rows.Add(errorRow);

                    grdDpho.DataSource = data;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected void btnPoli_Click(object sender, EventArgs e)
        {
            try
            {
                string kodePoli = txtKodePoli.Text.Trim();

                var svc = new Common.BPJS.Apotek.Service();
                var response = svc.GetPoli(kodePoli);

                DataTable data = new DataTable();
                data.Columns.Add("Kode");
                data.Columns.Add("Nama");

                if (response.MetaData.IsApolValid)
                {
                    foreach (var poliItem in response.Response.List)
                    {
                        DataRow row = data.NewRow();
                        row["Kode"] = poliItem.Kode;
                        row["Nama"] = poliItem.Nama;
                        data.Rows.Add(row);
                    }
                }
                else
                {
                    DataRow errorRow = data.NewRow();
                    errorRow["Kode"] = response.MetaData.Code + " - " + response.MetaData.Message;
                    data.Rows.Add(errorRow);
                }

                grdPoli.DataSource = data;
                grdPoli.DataBind();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        protected void btnSetting_Click(object sender, EventArgs e)
        {
            try
            {
                string kodeApotek = txtKodeApotek.Text.Trim();

                var svc = new Common.BPJS.Apotek.Service();
                var jsonResponse = svc.SettingApotek(kodeApotek);

                Response.Write(jsonResponse);

                DataTable data = new DataTable();
                data.Columns.Add("Kode");
                data.Columns.Add("Namaapoteker");
                data.Columns.Add("Namakepala");
                data.Columns.Add("Jabatankepala");
                data.Columns.Add("Nipkepala");
                data.Columns.Add("Siup");
                data.Columns.Add("Alamat");
                data.Columns.Add("Kota");
                data.Columns.Add("Namaverifikator");
                data.Columns.Add("Nppverifikator");
                data.Columns.Add("Namapetugasapotek");
                data.Columns.Add("Nippetugasapotek");
                data.Columns.Add("Checkstock");

                grdSetting.DataSource = data;
                grdSetting.DataBind();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected void btnFaskes_Click(object sender, EventArgs e)
        {
            try
            {
                string jenisFaskes = cboFaskes.SelectedValue;
                string namaFaskes = txtNamaFaskes.Text.Trim();

                var svc = new Common.BPJS.Apotek.Service();
                var response = svc.GetFaskes(jenisFaskes, namaFaskes);

                DataTable data = new DataTable();
                data.Columns.Add("Kode");
                data.Columns.Add("Nama");

                if (response.MetaData.IsApolValid)
                {
                    foreach (var faskesitem in response.Response.List)
                    {
                        DataRow row = data.NewRow();
                        row["Kode"] = faskesitem.Kode;
                        row["Nama"] = faskesitem.Nama;
                        data.Rows.Add(row);
                    }
                }
                else
                {
                    DataRow errorRow = data.NewRow();
                    errorRow["Kode"] = response.MetaData.Code + " - " + response.MetaData.Message;
                    data.Rows.Add(errorRow);
                }

                grdFaskes.DataSource = data;
                grdFaskes.DataBind();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected void btnSpesialistik_Click(object sender, EventArgs e)
        {
            grdSpesialistik.Rebind();
        }

        protected void grdSpesialistik_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "btnSpesialistik_Click")
            {

            }
        }

        protected void grdSpesialistik_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                var svc = new Common.BPJS.Apotek.Service();
                var response = svc.GetSpesialistik();

                DataTable data = new DataTable();
                data.Columns.Add("Kode");
                data.Columns.Add("Nama");

                if (response.MetaData.IsApolValid)
                {
                    foreach (var item in response.Response.List)
                    {
                        DataRow row = data.NewRow();
                        row["Kode"] = item.Kode;
                        row["Nama"] = item.Nama;
                        data.Rows.Add(row);
                    }
                    grdSpesialistik.DataSource = data;
                }
                else
                {
                    DataRow errorRow = data.NewRow();
                    errorRow["Jumlahdata"] = response.MetaData.Code + " - " + response.MetaData.Message;
                    data.Rows.Add(errorRow);

                    grdSpesialistik.DataSource = data;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        protected void btnObat_Click(object sender, EventArgs e)
        {
            try
            {
                string kodeJenisObat = cboJenisObat.SelectedValue;
                DateTime tglResep = txtTglResep.SelectedDate.Value;
                string filterPencarian = txtFilterPencarian.Text.Trim();

                var svc = new Common.BPJS.Apotek.Service();
                var response = svc.GetObat(kodeJenisObat, tglResep, filterPencarian);

                DataTable data = new DataTable();
                data.Columns.Add("Kode");
                data.Columns.Add("Nama");
                data.Columns.Add("Harga");

                if (response.MetaData.IsApolValid)
                {
                    foreach (var obat in response.Response.Obat)
                    {
                        DataRow row = data.NewRow();
                        row["Kode"] = obat.Kode;
                        row["Nama"] = obat.Nama;
                        row["Harga"] = obat.Nama;
                        data.Rows.Add(row);
                    }
                }
                else
                {
                    DataRow errorRow = data.NewRow();
                    errorRow["Kode"] = response.MetaData.Code + " - " + response.MetaData.Message;
                    data.Rows.Add(errorRow);
                }

                grdObat.DataSource = data;
                grdObat.DataBind();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
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