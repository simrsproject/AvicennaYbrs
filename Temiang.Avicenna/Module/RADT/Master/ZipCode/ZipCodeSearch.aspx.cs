using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ZipCodeSearch : BasePageDialog
    {
	    protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ZipCode;
            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSRProvince, AppEnum.StandardReference.Province);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ZipCodeQuery();
			if (!string.IsNullOrEmpty(txtZipCode.Text))
            {
                if (cboFilterZipCode.SelectedIndex == 1)
                    query.Where(query.ZipPostalCode == txtZipCode.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtZipCode.Text);
                    query.Where(query.ZipPostalCode.Like(searchTextContain));
                }
            }		
			if (!string.IsNullOrEmpty(txtStreetName.Text))
            {
                if (cboFilterStreetName.SelectedIndex==1)
                    query.Where(query.StreetName == txtStreetName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtStreetName.Text);
                    query.Where(query.StreetName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtDistrict.Text))
            {
                if (cboFilterDistrict.SelectedIndex == 1)
                    query.Where(query.District == txtDistrict.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDistrict.Text);
                    query.Where(query.District.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtCounty.Text))
            {
                if (cboFilterCounty.SelectedIndex == 1)
                    query.Where(query.County == txtCounty.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtCounty.Text);
                    query.Where(query.County.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtCity.Text))
            {
                if (cboFilterCity.SelectedIndex == 1)
                    query.Where(query.City == txtCity.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtCity.Text);
                    query.Where(query.City.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSRProvince.SelectedValue))
                query.Where(query.SRProvince == cboSRProvince.SelectedValue);

            query.OrderBy(query.ZipCode.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}