using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class InternalSepList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = Common.AppConstant.Program.BpjsInternalSep;
        }

        private DataTable GridDataSource(List<Common.BPJS.VClaim.v11.InternalSep.Select.List> lists)
        {
            var dtb = new DataTable();

            dtb.Columns.Add("Nmtujuanrujuk", typeof(string));
            dtb.Columns.Add("Nmpoliasal", typeof(string));
            dtb.Columns.Add("Tglrujukinternal", typeof(string));
            dtb.Columns.Add("Nosep", typeof(string));
            dtb.Columns.Add("Nosepref", typeof(string));
            dtb.Columns.Add("Tglsep", typeof(string));
            dtb.Columns.Add("Nosurat", typeof(string));
            dtb.Columns.Add("Nmdokter", typeof(string));
            dtb.Columns.Add("Nmdiag", typeof(string));
            dtb.Columns.Add("Kdpolituj", typeof(string));

            foreach (var list in lists)
            {
                var row = dtb.NewRow();
                row[0] = list.Nmtujuanrujuk;
                row[1] = list.Nmpoliasal;
                row[2] = list.Tglrujukinternal;
                row[3] = list.Nosep;
                row[4] = list.Nosepref;
                row[5] = list.Tglsep;
                row[6] = list.Nosurat;
                row[7] = list.Nmdokter;
                row[8] = list.Nmdiag;
                row[9] = list.Kdpolituj;
                dtb.Rows.Add(row);
            }

            dtb.AcceptChanges();

            return dtb;
        }

        protected void grdList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var data = new List<Common.BPJS.VClaim.v11.InternalSep.Select.List>();
            if (!string.IsNullOrWhiteSpace(txtNoSep.Text))
            {
                var svc = new Common.BPJS.VClaim.v11.Service();
                var response = svc.GetInternalSep(txtNoSep.Text);
                if (response.MetaData.IsValid) grdList.DataSource = GridDataSource(response.Response.List);
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "poli", string.Format("alert('Code : {0}, Message : {1}');", response.MetaData.Code, response.MetaData.Message), true);

                    grdList.DataSource = GridDataSource(data);
                }
            }
            else
            {
                if (IsPostBack) ScriptManager.RegisterStartupScript(this, GetType(), "poli", string.Format("alert('Code : {0}, Message : {1}');", "000", "No SEP harus diisi"), true);

                grdList.DataSource = GridDataSource(data);
            }
        }

        protected void btnFilterNoSep_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void grdList_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var noSep = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Nosep"]);
            var noSurat = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Nosurat"]);
            var tgl = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Tglrujukinternal"]);
            var kdPoli = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Kdpolituj"]);
            var svc = new Common.BPJS.VClaim.v11.Service();
            var respose = svc.Delete(new Common.BPJS.VClaim.v11.InternalSep.Delete.Request.Root()
            {
                Request = new Common.BPJS.VClaim.v11.InternalSep.Delete.Request.TRequest()
                {
                    TSep = new Common.BPJS.VClaim.v11.InternalSep.Delete.Request.TSep()
                    {
                        NoSep = noSep,
                        NoSurat = noSurat,
                        TglRujukanInternal = tgl,
                        KdPoliTuj = kdPoli,
                        User = AppSession.UserLogin.UserID
                    }
                }
            });
            if (respose.MetaData.IsValid)
            {
                grdList.Rebind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "cari", string.Format("alert('Code : {0}, Message : {1}');", respose.MetaData.Code, respose.MetaData.Message), true);
            }
        }
    }
}