using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;


namespace Temiang.Avicenna.Module.Inventory.Information
{
    public partial class ItemInformationList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected override void OnInit(EventArgs e)
        { 
            base.OnInit(e);
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.ItemInformation;

            if (!IsPostBack)
            {
				if (this.IsUserCrossUnitAble)
				{
					ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
					ComboBox.SelectedValue(cboSRItemType, BusinessObject.Reference.ItemType.Medical);
				}
				else
					ComboBox.PopulateWithItemTypeProductPerUser(cboSRItemType, AppSession.UserLogin.UserID);

				grdItem.Columns.FindByUniqueName("listED").Visible = !AppSession.Parameter.IsEnabledStockWithEdControl;
			}
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            grdItem.Rebind();
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = ItemInfos;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
					grd.DataSource = dataSource;
        }

        private DataTable ItemInfos
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboSRItemType.SelectedValue) && string.IsNullOrEmpty(txtBatchNo.Text) && string.IsNullOrEmpty(txtItemName.Text) && string.IsNullOrEmpty(cboItemID.SelectedValue) && string.IsNullOrEmpty(cboZatActiveID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Item Information")) return null;

                var classId = AppSession.Parameter.DefaultTariffClass;
                var tariffType = AppSession.Parameter.DefaultTariffType;

                var itemQ = new ItemQuery("a");
                var itemProductMedicQ = new ItemProductMedicQuery("b");
                var itemProductNonMedicQ = new ItemProductNonMedicQuery("c");
                var itemKitchenQ = new ItemKitchenQuery("d");
                var classQ = new ClassQuery("e");

                itemQ.LeftJoin(itemProductMedicQ).On(itemQ.ItemID == itemProductMedicQ.ItemID);
                itemQ.LeftJoin(itemProductNonMedicQ).On(itemQ.ItemID == itemProductNonMedicQ.ItemID);
                itemQ.LeftJoin(itemKitchenQ).On(itemQ.ItemID == itemKitchenQ.ItemID);
                itemQ.InnerJoin(classQ).On(classQ.ClassID == classId);

                itemQ.Where(itemQ.SRItemType == cboSRItemType.SelectedValue, itemQ.IsActive == true);

                if (txtItemName.Text != string.Empty)
                {
                    string search = txtItemName.Text.Trim();
                    itemQ.Where
                        (
                            itemQ.Or
                            (
                                itemQ.ItemID.Like("%" + search + "%"),
                                itemQ.ItemName.Like("%" + search + "%")
                            )
                        );
                }

				if (!string.IsNullOrEmpty(cboItemID.SelectedValue))
					itemQ.Where(itemQ.ItemID == cboItemID.SelectedValue);

				itemQ.Select
                (
                    itemQ.ItemID,
                    itemQ.ItemName
                );

                switch (cboSRItemType.SelectedValue)
                {
                    case BusinessObject.Reference.ItemType.Medical:
                        itemQ.Select
                            (
                            itemProductMedicQ.SRItemUnit,
                            itemProductMedicQ.PriceInBaseUnit,
                            itemProductMedicQ.PriceInBasedUnitWVat,
                            itemProductMedicQ.LastPriceInBaseUnit,
							itemProductMedicQ.HighestPriceInBasedUnit,
							itemProductMedicQ.IsControlExpired,
							@"<
                                CASE WHEN e.MarginPercentage > 0 THEN e.MarginPercentage
	                            ELSE 
                                        CASE WHEN b.MarginPercentage > 0 THEN
                                            b.MarginPercentage
                                        ELSE
			                                CASE WHEN EXISTS(SELECT ipm.MarginID FROM ItemProductMargin ipm WHERE ipm.MarginID = b.MarginID) THEN 
				                                ISNULL((
						                                SELECT CASE WHEN ipmv.MarginPercentage = 0 THEN ipmv.OutpatientMarginPercentage ELSE ipmv.MarginPercentage END
						                                FROM ItemProductMarginValue ipmv 
						                                WHERE ISNULL(
							                                (
								                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
								                                FROM ItemTariff it 
								                                WHERE it.ItemID = a.ItemID 
									                                AND it.ClassID = '" + classId + @"'
									                                AND it.SRTariffType = '" + tariffType + @"'  
									                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
								                                ORDER BY it.StartingDate DESC
							                                ), 0) >= ipmv.StartingValue AND 
							                                ISNULL(
							                                (
								                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
								                                FROM ItemTariff it 
								                                WHERE it.ItemID = a.ItemID 
									                                AND it.ClassID = '" + classId + @"'
									                                AND it.SRTariffType = '" + tariffType + @"' 
									                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
								                                ORDER BY it.StartingDate DESC
							                                ), 0) <= ipmv.EndingValue AND ipmv.MarginID = b.MarginID
						                                ), 0)
			                                ELSE 
				                                0
			                                END
		                                END 
                                    END AS 'Margin'
                            >",
							@"<
                                CASE WHEN b.SalesFixedPrice > 0 THEN b.SalesFixedPrice 
			                    ELSE
                                    CASE WHEN e.MarginPercentage > 0 THEN 
                                        ISNULL(
                                        (
	                                        SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price
	                                        FROM ItemTariff it 
	                                        WHERE it.ItemID = a.ItemID 
		                                        AND it.ClassID = '" + classId + @"'
		                                        AND it.SRTariffType = '" + tariffType + @"' 
		                                        AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
	                                        ORDER BY it.StartingDate DESC
                                        ), 0) + 
                                        (
	                                        ISNULL(
	                                        (
		                                        SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
		                                        FROM ItemTariff it 
		                                        WHERE it.ItemID = a.ItemID 
			                                        AND it.ClassID = '" + classId + @"'
			                                        AND it.SRTariffType = '" + tariffType + @"' 
			                                        AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
		                                        ORDER BY it.StartingDate DESC
	                                        ), 0) * e.MarginPercentage / 100
                                        )
	                                ELSE 
                                        CASE WHEN b.MarginPercentage > 0 THEN
                                            ISNULL(
			                                (
				                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
				                                FROM ItemTariff it 
				                                WHERE it.ItemID = a.ItemID 
					                                AND it.ClassID = '" + classId + @"'
					                                AND it.SRTariffType = '" + tariffType + @"' 
					                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
				                                ORDER BY it.StartingDate DESC
			                                ), 0) + 
			                                (
				                                ISNULL(
				                                (
					                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
					                                FROM ItemTariff it 
					                                WHERE it.ItemID = a.ItemID 
						                                AND it.ClassID = '" + classId + @"'
						                                AND it.SRTariffType = '" + tariffType + @"' 
						                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
					                                ORDER BY it.StartingDate DESC
				                                ), 0) * b.MarginPercentage / 100
			                                )
                                        ELSE
			                                CASE WHEN EXISTS(SELECT ipm.MarginID FROM ItemProductMargin ipm WHERE ipm.MarginID = b.MarginID) THEN 
				                                ISNULL(
				                                (
					                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
					                                FROM ItemTariff it 
					                                WHERE it.ItemID = a.ItemID 
						                                AND it.ClassID = '" + classId + @"'
						                                AND it.SRTariffType = '" + tariffType + @"'  
						                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
					                                ORDER BY it.StartingDate DESC
				                                ), 0) + 
				                                (
					                                ISNULL(
						                                (
							                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
							                                FROM ItemTariff it 
							                                WHERE it.ItemID = a.ItemID 
								                                AND it.ClassID = '" + classId + @"'
								                                AND it.SRTariffType = '" + tariffType + @"' 
								                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112) 
							                                ORDER BY it.StartingDate DESC
						                                ), 0) * 
					                                ISNULL((
						                                SELECT CASE WHEN ipmv.MarginPercentage = 0 THEN ipmv.OutpatientMarginPercentage ELSE ipmv.MarginPercentage END 
						                                FROM ItemProductMarginValue ipmv 
						                                WHERE ISNULL(
							                                (
								                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
								                                FROM ItemTariff it 
								                                WHERE it.ItemID = a.ItemID 
									                                AND it.ClassID = '" + classId + @"'
									                                AND it.SRTariffType = '" + tariffType + @"'  
									                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
								                                ORDER BY it.StartingDate DESC
							                                ), 0) >= ipmv.StartingValue AND 
							                                ISNULL(
							                                (
								                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
								                                FROM ItemTariff it 
								                                WHERE it.ItemID = a.ItemID 
									                                AND it.ClassID = '" + classId + @"'
									                                AND it.SRTariffType = '" + tariffType + @"' 
									                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
								                                ORDER BY it.StartingDate DESC
							                                ), 0) <= ipmv.EndingValue AND ipmv.MarginID = b.MarginID
						                                ), 0) / 100
				                                ) 
			                                ELSE 
				                                ISNULL(
				                                (
					                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
					                                FROM ItemTariff it 
					                                WHERE it.ItemID = a.ItemID 
						                                AND it.ClassID = '" + classId + @"'
						                                AND it.SRTariffType = '" + tariffType + @"'  
						                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
					                                ORDER BY it.StartingDate DESC
				                                ), 0)
			                                END
		                                END 
                                    END
	                            END AS 'Price'
                            >"
                            );
                        break;
                    case BusinessObject.Reference.ItemType.NonMedical:
                        itemQ.Select
                            (
                            itemProductNonMedicQ.SRItemUnit,
                            itemProductNonMedicQ.PriceInBaseUnit,
                            itemProductNonMedicQ.PriceInBasedUnitWVat,
                            itemProductNonMedicQ.LastPriceInBaseUnit,
							itemProductNonMedicQ.HighestPriceInBasedUnit,
							itemProductNonMedicQ.IsControlExpired,
							@"<
                                CASE WHEN e.MarginPercentage > 0 THEN e.MarginPercentage
	                            ELSE 
                                        CASE WHEN c.MarginPercentage > 0 THEN
                                            c.MarginPercentage
                                        ELSE
			                                CASE WHEN EXISTS(SELECT ipm.MarginID FROM ItemProductMargin ipm WHERE ipm.MarginID = c.MarginID) THEN 
				                                ISNULL((
						                                SELECT CASE WHEN ipmv.MarginPercentage = 0 THEN ipmv.OutpatientMarginPercentage ELSE ipmv.MarginPercentage END 
						                                FROM ItemProductMarginValue ipmv 
						                                WHERE ISNULL(
							                                (
								                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
								                                FROM ItemTariff it 
								                                WHERE it.ItemID = a.ItemID 
									                                AND it.ClassID = '" + classId + @"'
									                                AND it.SRTariffType = '" + tariffType + @"'  
									                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
								                                ORDER BY it.StartingDate DESC
							                                ), 0) >= ipmv.StartingValue AND 
							                                ISNULL(
							                                (
								                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
								                                FROM ItemTariff it 
								                                WHERE it.ItemID = a.ItemID 
									                                AND it.ClassID = '" + classId + @"'
									                                AND it.SRTariffType = '" + tariffType + @"' 
									                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
								                                ORDER BY it.StartingDate DESC
							                                ), 0) <= ipmv.EndingValue AND ipmv.MarginID = c.MarginID
						                                ), 0)
			                                ELSE 
				                                0
			                                END
		                                END 
                                    END AS 'Margin'
                            >",
							@"<
                                CASE WHEN c.SalesFixedPrice > 0 THEN c.SalesFixedPrice 
			                    ELSE
                                    CASE WHEN e.MarginPercentage > 0 THEN 
                                        ISNULL(
                                        (
	                                        SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price
	                                        FROM ItemTariff it 
	                                        WHERE it.ItemID = a.ItemID 
		                                        AND it.ClassID = '" + classId + @"'
		                                        AND it.SRTariffType = '" + tariffType + @"' 
		                                        AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
	                                        ORDER BY it.StartingDate DESC
                                        ), 0) + 
                                        (
	                                        ISNULL(
	                                        (
		                                        SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
		                                        FROM ItemTariff it 
		                                        WHERE it.ItemID = a.ItemID 
			                                        AND it.ClassID = '" + classId + @"'
			                                        AND it.SRTariffType = '" + tariffType + @"' 
			                                        AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
		                                        ORDER BY it.StartingDate DESC
	                                        ), 0) * e.MarginPercentage / 100
                                        )
	                                ELSE 
                                        CASE WHEN c.MarginPercentage > 0 THEN
                                            ISNULL(
			                                (
				                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
				                                FROM ItemTariff it 
				                                WHERE it.ItemID = a.ItemID 
					                                AND it.ClassID = '" + classId + @"'
					                                AND it.SRTariffType = '" + tariffType + @"' 
					                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
				                                ORDER BY it.StartingDate DESC
			                                ), 0) + 
			                                (
				                                ISNULL(
				                                (
					                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
					                                FROM ItemTariff it 
					                                WHERE it.ItemID = a.ItemID 
						                                AND it.ClassID = '" + classId + @"'
						                                AND it.SRTariffType = '" + tariffType + @"' 
						                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
					                                ORDER BY it.StartingDate DESC
				                                ), 0) * c.MarginPercentage / 100
			                                )
                                        ELSE
			                                CASE WHEN EXISTS(SELECT ipm.MarginID FROM ItemProductMargin ipm WHERE ipm.MarginID = c.MarginID) THEN 
				                                ISNULL(
				                                (
					                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
					                                FROM ItemTariff it 
					                                WHERE it.ItemID = a.ItemID 
						                                AND it.ClassID = '" + classId + @"'
						                                AND it.SRTariffType = '" + tariffType + @"'  
						                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
					                                ORDER BY it.StartingDate DESC
				                                ), 0) + 
				                                (
					                                ISNULL(
						                                (
							                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
							                                FROM ItemTariff it 
							                                WHERE it.ItemID = a.ItemID 
								                                AND it.ClassID = '" + classId + @"'
								                                AND it.SRTariffType = '" + tariffType + @"' 
								                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112) 
							                                ORDER BY it.StartingDate DESC
						                                ), 0) * 
					                                ISNULL((
						                                SELECT CASE WHEN ipmv.MarginPercentage = 0 THEN ipmv.OutpatientMarginPercentage ELSE ipmv.MarginPercentage END 
						                                FROM ItemProductMarginValue ipmv 
						                                WHERE ISNULL(
							                                (
								                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
								                                FROM ItemTariff it 
								                                WHERE it.ItemID = a.ItemID 
									                                AND it.ClassID = '" + classId + @"'
									                                AND it.SRTariffType = '" + tariffType + @"'  
									                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
								                                ORDER BY it.StartingDate DESC
							                                ), 0) >= ipmv.StartingValue AND 
							                                ISNULL(
							                                (
								                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
								                                FROM ItemTariff it 
								                                WHERE it.ItemID = a.ItemID 
									                                AND it.ClassID = '" + classId + @"'
									                                AND it.SRTariffType = '" + tariffType + @"' 
									                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
								                                ORDER BY it.StartingDate DESC
							                                ), 0) <= ipmv.EndingValue AND ipmv.MarginID = c.MarginID
						                                ), 0) / 100
				                                ) 
			                                ELSE 
				                                ISNULL(
				                                (
					                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
					                                FROM ItemTariff it 
					                                WHERE it.ItemID = a.ItemID 
						                                AND it.ClassID = '" + classId + @"'
						                                AND it.SRTariffType = '" + tariffType + @"'  
						                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
					                                ORDER BY it.StartingDate DESC
				                                ), 0)
			                                END
		                                END 
                                    END
	                            END AS 'Price'
                            >"
                            );
                        break;
                    case BusinessObject.Reference.ItemType.Kitchen:
                        itemQ.Select
                            (
                            itemKitchenQ.SRItemUnit,
                            itemKitchenQ.PriceInBaseUnit,
                            itemKitchenQ.PriceInBasedUnitWVat,
                            itemKitchenQ.LastPriceInBaseUnit,
							itemKitchenQ.HighestPriceInBasedUnit,
							itemKitchenQ.IsControlExpired,
							@"<
                                CASE WHEN e.MarginPercentage > 0 THEN e.MarginPercentage
	                            ELSE 
                                        CASE WHEN d.MarginPercentage > 0 THEN
                                            d.MarginPercentage
                                        ELSE
			                                CASE WHEN EXISTS(SELECT ipm.MarginID FROM ItemProductMargin ipm WHERE ipm.MarginID = d.MarginID) THEN 
				                                ISNULL((
						                                SELECT CASE WHEN ipmv.MarginPercentage = 0 THEN ipmv.OutpatientMarginPercentage ELSE ipmv.MarginPercentage END 
						                                FROM ItemProductMarginValue ipmv 
						                                WHERE ISNULL(
							                                (
								                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
								                                FROM ItemTariff it 
								                                WHERE it.ItemID = a.ItemID 
									                                AND it.ClassID = '" + classId + @"'
									                                AND it.SRTariffType = '" + tariffType + @"'  
									                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
								                                ORDER BY it.StartingDate DESC
							                                ), 0) >= ipmv.StartingValue AND 
							                                ISNULL(
							                                (
								                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
								                                FROM ItemTariff it 
								                                WHERE it.ItemID = a.ItemID 
									                                AND it.ClassID = '" + classId + @"'
									                                AND it.SRTariffType = '" + tariffType + @"' 
									                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
								                                ORDER BY it.StartingDate DESC
							                                ), 0) <= ipmv.EndingValue AND ipmv.MarginID = d.MarginID
						                                ), 0)
			                                ELSE 
				                                0
			                                END
		                                END 
                                    END AS 'Margin'
                            >",
							@"<
                                CASE WHEN d.SalesFixedPrice > 0 THEN d.SalesFixedPrice 
			                    ELSE
                                    CASE WHEN e.MarginPercentage > 0 THEN 
                                        ISNULL(
                                        (
	                                        SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price
	                                        FROM ItemTariff it 
	                                        WHERE it.ItemID = a.ItemID 
		                                        AND it.ClassID = '" + classId + @"'
		                                        AND it.SRTariffType = '" + tariffType + @"' 
		                                        AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
	                                        ORDER BY it.StartingDate DESC
                                        ), 0) + 
                                        (
	                                        ISNULL(
	                                        (
		                                        SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
		                                        FROM ItemTariff it 
		                                        WHERE it.ItemID = a.ItemID 
			                                        AND it.ClassID = '" + classId + @"'
			                                        AND it.SRTariffType = '" + tariffType + @"' 
			                                        AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
		                                        ORDER BY it.StartingDate DESC
	                                        ), 0) * e.MarginPercentage / 100
                                        )
	                                ELSE 
                                        CASE WHEN d.MarginPercentage > 0 THEN
                                            ISNULL(
			                                (
				                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
				                                FROM ItemTariff it 
				                                WHERE it.ItemID = a.ItemID 
					                                AND it.ClassID = '" + classId + @"'
					                                AND it.SRTariffType = '" + tariffType + @"' 
					                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
				                                ORDER BY it.StartingDate DESC
			                                ), 0) + 
			                                (
				                                ISNULL(
				                                (
					                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
					                                FROM ItemTariff it 
					                                WHERE it.ItemID = a.ItemID 
						                                AND it.ClassID = '" + classId + @"'
						                                AND it.SRTariffType = '" + tariffType + @"' 
						                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
					                                ORDER BY it.StartingDate DESC
				                                ), 0) * d.MarginPercentage / 100
			                                )
                                        ELSE
			                                CASE WHEN EXISTS(SELECT ipm.MarginID FROM ItemProductMargin ipm WHERE ipm.MarginID = d.MarginID) THEN 
				                                ISNULL(
				                                (
					                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
					                                FROM ItemTariff it 
					                                WHERE it.ItemID = a.ItemID 
						                                AND it.ClassID = '" + classId + @"'
						                                AND it.SRTariffType = '" + tariffType + @"'  
						                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
					                                ORDER BY it.StartingDate DESC
				                                ), 0) + 
				                                (
					                                ISNULL(
						                                (
							                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
							                                FROM ItemTariff it 
							                                WHERE it.ItemID = a.ItemID 
								                                AND it.ClassID = '" + classId + @"'
								                                AND it.SRTariffType = '" + tariffType + @"' 
								                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112) 
							                                ORDER BY it.StartingDate DESC
						                                ), 0) * 
					                                ISNULL((
						                                SELECT CASE WHEN ipmv.MarginPercentage = 0 THEN ipmv.OutpatientMarginPercentage ELSE ipmv.MarginPercentage END 
						                                FROM ItemProductMarginValue ipmv 
						                                WHERE ISNULL(
							                                (
								                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
								                                FROM ItemTariff it 
								                                WHERE it.ItemID = a.ItemID 
									                                AND it.ClassID = '" + classId + @"'
									                                AND it.SRTariffType = '" + tariffType + @"'  
									                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
								                                ORDER BY it.StartingDate DESC
							                                ), 0) >= ipmv.StartingValue AND 
							                                ISNULL(
							                                (
								                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
								                                FROM ItemTariff it 
								                                WHERE it.ItemID = a.ItemID 
									                                AND it.ClassID = '" + classId + @"'
									                                AND it.SRTariffType = '" + tariffType + @"' 
									                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
								                                ORDER BY it.StartingDate DESC
							                                ), 0) <= ipmv.EndingValue AND ipmv.MarginID = d.MarginID
						                                ), 0) / 100
				                                ) 
			                                ELSE 
				                                ISNULL(
				                                (
					                                SELECT TOP 1 (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) - (((it.Price/((100+ISNULL(it.Ppn, 10))/100)) + ((it.Price/((100+ISNULL(it.Ppn, 10))/100))* CAST((SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'Ppn') AS NUMERIC(5,2))/100)) * ISNULL(DiscPercentage, 0) / 100)) AS Price 
					                                FROM ItemTariff it 
					                                WHERE it.ItemID = a.ItemID 
						                                AND it.ClassID = '" + classId + @"'
						                                AND it.SRTariffType = '" + tariffType + @"'  
						                                AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
					                                ORDER BY it.StartingDate DESC
				                                ), 0)
			                                END
		                                END 
                                    END
	                            END AS 'Price'
                            >"
                            );
                        break;
                }

				if (!string.IsNullOrEmpty(txtBatchNo.Text))
				{
					itemQ.Where(@"<a.ItemID IN (SELECT x.ItemID 
												FROM ItemTransactionItemEd AS x
												INNER JOIN ItemTransaction AS it ON it.TransactionNo = x.TransactionNo
												WHERE it.TransactionCode = '040' AND x.BatchNumber = '" + txtBatchNo.Text + @"')>");
				}

				//Nurul - Tambah filter Generic ambil dari table Zat Active
				if (!string.IsNullOrEmpty(cboZatActiveID.SelectedValue))
				{
					var itemProductMedicZatActiveQ = new ItemProductMedicZatActiveQuery("ipmza");
					var zatActiveQ = new ZatActiveQuery("zaq"); 
					itemQ.InnerJoin(itemProductMedicZatActiveQ).On(itemQ.ItemID == itemProductMedicZatActiveQ.ItemID)
						.InnerJoin(zatActiveQ).On(itemProductMedicZatActiveQ.ZatActiveID == zatActiveQ.ZatActiveID);

					itemQ.Where(zatActiveQ.ZatActiveID == cboZatActiveID.SelectedValue);
				}

				itemQ.OrderBy(itemQ.ItemName.Ascending);

                DataTable dtb = itemQ.LoadDataTable();
                foreach (DataRow row in dtb.Rows)
                {
                    row["Price"] = Helper.Rounding(Convert.ToDecimal(row["Price"]), AppEnum.RoundingType.Transaction); 
                }
                dtb.AcceptChanges();

                return dtb;
            }
        }

        protected void grdItemDetail_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;

            //Load record
            var dtQ = new ItemTransactionItemQuery("a");
            var hdQ = new ItemTransactionQuery("b");
            var suppQ = new SupplierQuery("c");
            var vwItemQ = new VwItemProductMedicNonMedicQuery("d");

            dtQ.Select(
                hdQ.TransactionNo,
                hdQ.TransactionDate,
                suppQ.SupplierName,
                dtQ.SequenceNo,
                dtQ.Quantity,
                dtQ.SRItemUnit,
                dtQ.Price,
                dtQ.Discount1Percentage,
                dtQ.Discount2Percentage,
                dtQ.Discount,
                dtQ.IsBonusItem,
                dtQ.ExpiredDate,
                hdQ.InvoiceNo,
                hdQ.ReferenceNo
            );
			if (!AppSession.Parameter.IsEnabledStockWithEdControl)
				dtQ.Select(vwItemQ.IsControlExpired);
			else
				dtQ.Select(@"<CAST(0 AS BIT) AS 'IsControlExpired'>");

			dtQ.InnerJoin(hdQ).On(dtQ.TransactionNo == hdQ.TransactionNo && hdQ.IsApproved == true &&
                                 hdQ.TransactionCode == "040");
            dtQ.InnerJoin(suppQ).On(hdQ.BusinessPartnerID == suppQ.SupplierID);
            dtQ.InnerJoin(vwItemQ).On(dtQ.ItemID == vwItemQ.ItemID);

            dtQ.Where(dtQ.ItemID == dataItem.GetDataKeyValue("ItemID").ToString());

            dtQ.OrderBy(hdQ.TransactionDate.Descending);
            dtQ.es.Top = 50;

            DataTable dtb = dtQ.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }

		protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
		{
			e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"] + " [" + ((DataRowView)e.Item.DataItem)["ItemID"] + "]";
			e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
		}

		protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
		{
			var tbl = PopulateItem(e.Text);
			cboItemID.DataSource = tbl.Rows.Count == 0 ? PopulateItem(e.Text) : tbl;
			cboItemID.DataBind();
		}

		protected void cboZatActiveID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
		{
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ZatActiveName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ZatActiveID"].ToString();
        }

        protected void cboZatActiveID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataTable tbl = LoadItem(e.Text);
            cboZatActiveID.DataSource = tbl;
            cboZatActiveID.DataBind();
        }

        private DataTable LoadItem(string searchText)
        {
            string searchTextContain = string.Format("%{0}%", searchText);
            var query = new ZatActiveQuery("a");
            query.es.Top = 20;
            query.Select
                (
                    query.ZatActiveID,
                    query.ZatActiveName
                );
            query.Where(
                query.IsActive == true,
                query.Or(
                    query.ZatActiveName.Like(searchTextContain),
                    query.ZatActiveID.Like(searchTextContain)
                    )
                );
            query.OrderBy(query.ZatActiveName.Ascending);

            return query.LoadDataTable();
		}

		private DataTable PopulateItem(string parameter)
		{
			string searchTextContain = string.Format("%{0}%", parameter);
			var query = new ItemQuery("a");

			query.es.Top = 20;
			query.es.Distinct = true;
			query.Select
				(
					query.ItemID,
					query.ItemName
				);

			query.Where(
					query.Or(
							query.ItemName.Like(searchTextContain),
							query.ItemID.Like(searchTextContain)
						),
					query.IsActive == true,
					query.SRItemType == cboSRItemType.SelectedValue);
			query.OrderBy(query.ItemName.Ascending);

			var tbl = query.LoadDataTable();

			return tbl;
		}
	}
}
