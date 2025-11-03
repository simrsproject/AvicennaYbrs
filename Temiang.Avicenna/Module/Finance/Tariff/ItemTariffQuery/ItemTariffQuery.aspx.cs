using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Finance.Tariff
{
    public partial class ItemTariffQueries : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.TARIFF_QUERY;

            if (!IsPostBack)
            {
                var unitCollection = new ServiceUnitCollection();
                unitCollection.Query.Where(unitCollection.Query.IsActive == true);
                unitCollection.LoadAll();

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var unit in unitCollection)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(unit.ServiceUnitName, unit.ServiceUnitID));
                }

                var itypeColl = new AppStandardReferenceItemCollection();
                itypeColl.Query.Where(
                    itypeColl.Query.StandardReferenceID == AppEnum.StandardReference.ItemType.ToString(),
                    itypeColl.Query.IsActive == true);
                itypeColl.Query.OrderBy(itypeColl.Query.ReferenceID.Descending, itypeColl.Query.ItemID.Ascending);
                itypeColl.LoadAll();
                foreach (var item in itypeColl)
                {
                    cboSRItemType.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
                }

                //StandardReference.Initialize(cboSRItemType, AppEnum.StandardReference.ItemType, true);
                StandardReference.Initialize(cboSRTariffType, AppEnum.StandardReference.TariffType);

                ComboBox.PopulateWithClass(cboClassID);
                ComboBox.SelectedValue(cboClassID, AppSession.Parameter.DefaultTariffClass);
            }
        }

        private DataTable ItemServices
        {
            get
            {
                string searchTextContain = string.Format("%{0}%", cboItemID.SelectedValue);
                var itemQ = new ItemQuery("a");
                var itemGroupQ = new ItemGroupQuery("b");
                var itemServiceQ = new ItemServiceQuery("d");
                var itemUnitQ = new ServiceUnitItemServiceQuery("e");
                var suQ = new ServiceUnitQuery("f");

                itemQ.es.Top = AppSession.Parameter.MaxResultRecord;
                itemQ.Select
                    (
                        itemQ.ItemID,
                        itemGroupQ.ItemGroupName,
                        itemQ.ItemName,
                        itemQ.IsActive,
                        suQ.ServiceUnitName,
                        itemServiceQ.SRItemUnit,
                        @"<
                            ISNULL(
                                (
                                    SELECT TOP 1 Price 
                                    FROM ItemTariff it 
                                    WHERE it.ItemID = a.ItemID 
	                                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
	                                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
	                                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112) 
                                    ORDER BY it.StartingDate DESC
                                ), 0)
                            AS 'Price'
                        >"
                    );
                itemQ.LeftJoin(itemGroupQ).On(itemQ.ItemGroupID == itemGroupQ.ItemGroupID);
                itemQ.InnerJoin(itemServiceQ).On
                    (
                        itemQ.ItemID == itemServiceQ.ItemID &&
                        itemQ.SRItemType == BusinessObject.Reference.ItemType.Service
                    );

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    itemQ.InnerJoin(itemUnitQ).On
                        (
                            itemQ.ItemID == itemUnitQ.ItemID &&
                            itemUnitQ.ServiceUnitID == cboServiceUnitID.SelectedValue
                        );
                else
                    itemQ.InnerJoin(itemUnitQ).On(itemQ.ItemID == itemUnitQ.ItemID);
                itemQ.InnerJoin(suQ).On(itemUnitQ.ServiceUnitID == suQ.ServiceUnitID);

                itemQ.Where
                    (
                        itemQ.ItemID.Like(searchTextContain),
                        itemQ.IsActive == true
                    );

                itemQ.OrderBy(itemQ.ItemID.Ascending);

                return itemQ.LoadDataTable();
            }
        }

        private DataTable ItemDiagnostics
        {
            get
            {
                string searchTextContain = string.Format("%{0}%", cboItemID.SelectedValue);
                var itemQ = new ItemQuery("a");
                var itemGroupQ = new ItemGroupQuery("b");
                var itemDiagQ = new ItemDiagnosticQuery("d");
                var itemUnitQ = new ServiceUnitItemServiceQuery("e");
                var suQ = new ServiceUnitQuery("f");

                itemQ.es.Top = AppSession.Parameter.MaxResultRecord;
                itemQ.Select
                    (
                        itemQ.ItemID,
                        itemGroupQ.ItemGroupName,
                        itemQ.ItemName,
                        itemQ.IsActive,
                        suQ.ServiceUnitName,
                        @"<'X' AS SRItemUnit>",
                        @"<
                            ISNULL(
                                (
                                    SELECT TOP 1 Price 
                                    FROM ItemTariff it 
                                    WHERE it.ItemID = a.ItemID 
	                                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
	                                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
	                                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112) 
                                    ORDER BY it.StartingDate DESC
                                ), 0)
                            AS 'Price'
                        >"
                    );
                itemQ.LeftJoin(itemGroupQ).On(itemQ.ItemGroupID == itemGroupQ.ItemGroupID);
                itemQ.InnerJoin(itemDiagQ).On
                    (
                        itemQ.ItemID == itemDiagQ.ItemID &&
                        itemQ.SRItemType == BusinessObject.Reference.ItemType.Diagnostic
                    );

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    itemQ.InnerJoin(itemUnitQ).On
                        (
                            itemQ.ItemID == itemUnitQ.ItemID &&
                            itemUnitQ.ServiceUnitID == cboServiceUnitID.SelectedValue
                        );
                else
                    itemQ.InnerJoin(itemUnitQ).On(itemQ.ItemID == itemUnitQ.ItemID);
                itemQ.InnerJoin(suQ).On(itemUnitQ.ServiceUnitID == suQ.ServiceUnitID);

                itemQ.Where
                    (
                        itemQ.ItemID.Like(searchTextContain),
                        itemQ.IsActive == true
                    );
                itemQ.OrderBy(itemQ.ItemID.Ascending);

                return itemQ.LoadDataTable();
            }
        }

        private DataTable ItemRadiologies
        {
            get
            {
                string searchTextContain = string.Format("%{0}%", cboItemID.SelectedValue);
                var itemQ = new ItemQuery("a");
                var itemGroupQ = new ItemGroupQuery("b");
                var itemRadQ = new ItemRadiologyQuery("d");
                var itemUnitQ = new ServiceUnitItemServiceQuery("e");
                var suQ = new ServiceUnitQuery("f");

                itemQ.es.Top = AppSession.Parameter.MaxResultRecord;
                itemQ.Select
                    (
                        itemQ.ItemID,
                        itemGroupQ.ItemGroupName,
                        itemQ.ItemName,
                        itemQ.IsActive,
                        suQ.ServiceUnitName,
                        @"<'X' AS SRItemUnit>",
                        @"<
                            ISNULL(
                                (
                                    SELECT TOP 1 Price 
                                    FROM ItemTariff it 
                                    WHERE it.ItemID = a.ItemID 
	                                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
	                                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
	                                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
                                    ORDER BY it.StartingDate DESC
                                ), 0)
                            AS 'Price'
                        >"
                    );
                itemQ.InnerJoin(itemRadQ).On
                    (
                        itemQ.ItemID == itemRadQ.ItemID &&
                        itemQ.SRItemType == BusinessObject.Reference.ItemType.Radiology
                    );
                itemQ.LeftJoin(itemGroupQ).On(itemQ.ItemGroupID == itemGroupQ.ItemGroupID);

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    itemQ.InnerJoin(itemUnitQ).On
                        (
                            itemQ.ItemID == itemUnitQ.ItemID &&
                            itemUnitQ.ServiceUnitID == cboServiceUnitID.SelectedValue
                        );
                else
                    itemQ.InnerJoin(itemUnitQ).On(itemQ.ItemID == itemUnitQ.ItemID);
                itemQ.InnerJoin(suQ).On(itemUnitQ.ServiceUnitID == suQ.ServiceUnitID);

                itemQ.Where
                    (
                        itemQ.ItemID.Like(searchTextContain),
                        itemQ.IsActive == true
                    ); 
                itemQ.OrderBy(itemQ.ItemID.Ascending);

                return itemQ.LoadDataTable();

            }
        }

        private DataTable ItemLaboratories
        {
            get
            {
                string searchTextContain = string.Format("%{0}%", cboItemID.SelectedValue);
                var itemQ = new ItemQuery("a");
                var itemGroupQ = new ItemGroupQuery("b");
                var itemLabQ = new ItemLaboratoryQuery("d");
                var itemUnitQ = new ServiceUnitItemServiceQuery("e");
                var suQ = new ServiceUnitQuery("f");

                itemQ.es.Top = AppSession.Parameter.MaxResultRecord;
                itemQ.Select
                    (
                        itemQ.ItemID,
                        itemGroupQ.ItemGroupName,
                        itemQ.ItemName,
                        itemQ.IsActive,
                        suQ.ServiceUnitName,
                        @"<'X' AS SRItemUnit>",
                        @"<
                            ISNULL(
                                (
                                    SELECT TOP 1 Price 
                                    FROM ItemTariff it 
                                    WHERE it.ItemID = a.ItemID 
	                                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
	                                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
	                                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
                                    ORDER BY it.StartingDate DESC
                                ), 0)
                            AS 'Price'
                        >"
                    );
                itemQ.InnerJoin(itemLabQ).On
                    (
                        itemQ.ItemID == itemLabQ.ItemID &&
                        itemQ.SRItemType == BusinessObject.Reference.ItemType.Laboratory
                    );
                itemQ.LeftJoin(itemGroupQ).On(itemQ.ItemGroupID == itemGroupQ.ItemGroupID);

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    itemQ.InnerJoin(itemUnitQ).On
                        (
                            itemQ.ItemID == itemUnitQ.ItemID &&
                            itemUnitQ.ServiceUnitID == cboServiceUnitID.SelectedValue
                        );
                else
                    itemQ.InnerJoin(itemUnitQ).On(itemQ.ItemID == itemUnitQ.ItemID);
                itemQ.InnerJoin(suQ).On(itemUnitQ.ServiceUnitID == suQ.ServiceUnitID);

                itemQ.Where
                    (
                        itemQ.ItemID.Like(searchTextContain),
                        itemQ.IsActive == true
                    ); 
                itemQ.OrderBy(itemQ.ItemID.Ascending);

                return itemQ.LoadDataTable();

            }
        }

        private DataTable ItemPackages
        {
            get
            {
                string searchTextContain = string.Format("%{0}%", cboItemID.SelectedValue);
                var itemQ = new ItemQuery("a");
                var itemGroupQ = new ItemGroupQuery("b");
                var itemPackageQ = new ItemPackageQuery("d");
                var itemUnitQ = new ServiceUnitItemServiceQuery("e");
                var suQ = new ServiceUnitQuery("f");

                itemQ.es.Top = AppSession.Parameter.MaxResultRecord;
                itemQ.es.Distinct = true;
                itemQ.Select
                    (
                        itemQ.ItemID,
                        itemGroupQ.ItemGroupName,
                        itemQ.ItemName,
                        itemQ.IsActive,
                        suQ.ServiceUnitName,
                        itemPackageQ.SRItemUnit,
                        @"<
                            ISNULL(
                                (
                                    SELECT TOP 1 Price 
                                    FROM ItemTariff it 
                                    WHERE it.ItemID = a.ItemID 
	                                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
	                                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
	                                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
                                    ORDER BY it.StartingDate DESC
                                ), 0)
                            AS 'Price'
                        >"
                    );
                itemQ.InnerJoin(itemPackageQ).On
                    (
                        itemQ.ItemID == itemPackageQ.ItemID &&
                        itemQ.SRItemType == BusinessObject.Reference.ItemType.Package
                    );
                itemQ.LeftJoin(itemGroupQ).On(itemQ.ItemGroupID == itemGroupQ.ItemGroupID);

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    itemQ.InnerJoin(itemUnitQ).On
                        (
                            itemQ.ItemID == itemUnitQ.ItemID &&
                            itemUnitQ.ServiceUnitID == cboServiceUnitID.SelectedValue
                        );
                else
                    itemQ.InnerJoin(itemUnitQ).On(itemQ.ItemID == itemUnitQ.ItemID);
                itemQ.InnerJoin(suQ).On(itemUnitQ.ServiceUnitID == suQ.ServiceUnitID);

                itemQ.Where
                    (
                        itemQ.ItemID.Like(searchTextContain),
                        itemQ.IsActive == true
                    ); 
                itemQ.OrderBy(itemQ.ItemID.Ascending);

                return itemQ.LoadDataTable();

            }
        }

        private DataTable ItemProductMedics
        {
            get
            {
                string searchTextContain = string.Format("%{0}%", cboItemID.SelectedValue);
                var itemQ = new ItemQuery("a");
                var itemGroupQ = new ItemGroupQuery("b");
                var medicQ = new ItemProductMedicQuery("d");
                var classQ = new ClassQuery("e");

                itemQ.es.Top = AppSession.Parameter.MaxResultRecord;
                itemQ.Select
                    (
                        itemQ.ItemID,
                        itemGroupQ.ItemGroupName,
                        itemQ.ItemName,
                        itemQ.IsActive,
                        @"<'' AS 'ServiceUnitName'>",
                        medicQ.SRItemUnit,
                        @"<
                            CASE WHEN ((SELECT COUNT(*) 
                                        FROM ItemProductMedicMarginDetail ipmmd 
                                        WHERE ipmmd.ItemID = a.ItemID 
                                            AND ipmmd.ClassID = '" + cboClassID.SelectedValue + @"') > 0 AND
                                       (SELECT ipmmd.AmountPercentage 
                                        FROM ItemProductMedicMarginDetail ipmmd 
                                        WHERE ipmmd.ItemID = a.ItemID 
                                            AND ipmmd.ClassID = '" + cboClassID.SelectedValue + @"') > 0)
		                    THEN 
			                    ISNULL(
			                    (
				                    SELECT TOP 1 (Price - (Price * ISNULL(DiscPercentage, 0) / 100)) AS Price 
				                    FROM ItemTariff it 
				                    WHERE it.ItemID = a.ItemID 
					                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
					                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
					                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
				                    ORDER BY it.StartingDate DESC
			                    ), 0) + 
			                    (
				                    ISNULL(
				                    (
					                    SELECT TOP 1 (Price - (Price * ISNULL(DiscPercentage, 0) / 100)) AS Price 
					                    FROM ItemTariff it 
					                    WHERE it.ItemID = a.ItemID 
						                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
						                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
						                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
					                    ORDER BY it.StartingDate DESC
				                    ), 0) * (SELECT ipmmd.AmountPercentage 
                                             FROM ItemProductMedicMarginDetail ipmmd 
                                             WHERE ipmmd.ItemID = a.ItemID 
                                                 AND ipmmd.ClassID = '" + cboClassID.SelectedValue + @"') / 100
			                    )
		                    ELSE  
			                    CASE WHEN d.SalesFixedPrice > 0 
			                    THEN d.SalesFixedPrice 
			                    ELSE 
				                    CASE WHEN EXISTS(SELECT ipm.MarginID FROM ItemProductMargin ipm WHERE ipm.MarginID = d.MarginID) THEN 
                    					
					                    ISNULL(
					                    (
						                    SELECT TOP 1 (Price - (Price * ISNULL(DiscPercentage, 0) / 100)) AS Price 
						                    FROM ItemTariff it 
						                    WHERE it.ItemID = a.ItemID 
							                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
							                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"'  
							                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
						                    ORDER BY it.StartingDate DESC
					                    ), 0) + 
					                    (
						                    ISNULL(
							                    (
								                    SELECT TOP 1 (Price - (Price * ISNULL(DiscPercentage, 0) / 100)) AS Price 
								                    FROM ItemTariff it 
								                    WHERE it.ItemID = a.ItemID 
									                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
									                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
									                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112) 
								                    ORDER BY it.StartingDate DESC
							                    ), 0) * 
						                    ISNULL((
							                    SELECT ipmv.MarginPercentage 
							                    FROM ItemProductMarginValue ipmv 
							                    WHERE ISNULL(
								                    (
									                    SELECT TOP 1 (Price - (Price * ISNULL(DiscPercentage, 0) / 100)) AS Price 
									                    FROM ItemTariff it 
									                    WHERE it.ItemID = a.ItemID 
										                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
										                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"'  
										                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
									                    ORDER BY it.StartingDate DESC
								                    ), 0) >= ipmv.StartingValue AND 
								                    ISNULL(
								                    (
									                    SELECT TOP 1 (Price - (Price * ISNULL(DiscPercentage, 0) / 100)) AS Price 
									                    FROM ItemTariff it 
									                    WHERE it.ItemID = a.ItemID 
										                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
										                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
										                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
									                    ORDER BY it.StartingDate DESC
								                    ), 0) <= ipmv.EndingValue AND ipmv.MarginID = d.MarginID
							                    ), 0) / 100
					                    ) 
				                    ELSE 
					                    ISNULL(
					                    (
						                    SELECT TOP 1 (Price - (Price * ISNULL(DiscPercentage, 0) / 100)) AS Price 
						                    FROM ItemTariff it 
						                    WHERE it.ItemID = a.ItemID 
							                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
							                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
							                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
						                    ORDER BY it.StartingDate DESC
					                    ), 0) + 
					                    (
						                    ISNULL(
						                    (
							                    SELECT TOP 1 (Price - (Price * ISNULL(DiscPercentage, 0) / 100)) AS Price 
							                    FROM ItemTariff it 
							                    WHERE it.ItemID = a.ItemID 
								                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
								                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
								                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
							                    ORDER BY it.StartingDate DESC
						                    ), 0) * d.MarginPercentage / 100
					                    )
				                    END
			                    END 
		                    END AS 'Price'
                        >"
                    );
                itemQ.InnerJoin(medicQ).On
                    (
                        itemQ.ItemID == medicQ.ItemID &&
                        itemQ.SRItemType == BusinessObject.Reference.ItemType.Medical
                    );
                itemQ.InnerJoin(classQ).On(classQ.ClassID == cboClassID.SelectedValue);
                itemQ.Where
                    (
                        itemQ.ItemID.Like(searchTextContain),
                        itemQ.IsActive == true
                    ); 
                itemQ.LeftJoin(itemGroupQ).On(itemQ.ItemGroupID == itemGroupQ.ItemGroupID);
                itemQ.OrderBy(itemQ.ItemID.Ascending);

                DataTable dtb = itemQ.LoadDataTable();
                foreach (DataRow row in dtb.Rows)
                {
                    row["Price"] = Helper.Rounding(Convert.ToDecimal(row["Price"]), AppEnum.RoundingType.Transaction);
                }
                dtb.AcceptChanges();

                return dtb;
            }
        }

        private DataTable ItemProductNonMedics
        {
            get
            {
                string searchTextContain = string.Format("%{0}%", cboItemID.SelectedValue);
                var itemQ = new ItemQuery("a");
                var itemGroupQ = new ItemGroupQuery("b");
                var nonMedicQ = new ItemProductNonMedicQuery("d");
                var classQ = new ClassQuery("e");

                itemQ.es.Top = AppSession.Parameter.MaxResultRecord;
                itemQ.Select
                    (
                        itemQ.ItemID,
                        itemGroupQ.ItemGroupName,
                        itemQ.ItemName,
                        itemQ.IsActive,
                        @"<'' AS 'ServiceUnitName'>",
                        nonMedicQ.SRItemUnit,
                        @"<
                            CASE WHEN e.MarginPercentage > 0 
		                    THEN 
			                    ISNULL(
			                    (
				                    SELECT TOP 1 (Price - (Price * ISNULL(DiscPercentage, 0) / 100)) AS Price  
				                    FROM ItemTariff it 
				                    WHERE it.ItemID = a.ItemID 
					                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
					                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
					                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
				                    ORDER BY it.StartingDate DESC
			                    ), 0) + 
			                    (
				                    ISNULL(
				                    (
					                    SELECT TOP 1 (Price - (Price * ISNULL(DiscPercentage, 0) / 100)) AS Price  
					                    FROM ItemTariff it 
					                    WHERE it.ItemID = a.ItemID 
						                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
						                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
						                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
					                    ORDER BY it.StartingDate DESC
				                    ), 0) * e.MarginPercentage / 100
			                    )
		                    ELSE  
			                    CASE WHEN d.SalesFixedPrice > 0 
			                    THEN d.SalesFixedPrice 
			                    ELSE 
				                    CASE WHEN EXISTS(SELECT ipm.MarginID FROM ItemProductMargin ipm WHERE ipm.MarginID = d.MarginID) THEN 
                    					
					                    ISNULL(
					                    (
						                    SELECT TOP 1 (Price - (Price * ISNULL(DiscPercentage, 0) / 100)) AS Price  
						                    FROM ItemTariff it 
						                    WHERE it.ItemID = a.ItemID 
							                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
							                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"'  
							                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112) 
						                    ORDER BY it.StartingDate DESC
					                    ), 0) + 
					                    (
						                    ISNULL(
							                    (
								                    SELECT TOP 1 (Price - (Price * ISNULL(DiscPercentage, 0) / 100)) AS Price  
								                    FROM ItemTariff it 
								                    WHERE it.ItemID = a.ItemID 
									                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
									                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
									                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112) 
								                    ORDER BY it.StartingDate DESC
							                    ), 0) * 
						                    ISNULL((
							                    SELECT ipmv.MarginPercentage 
							                    FROM ItemProductMarginValue ipmv 
							                    WHERE ISNULL(
								                    (
									                    SELECT TOP 1 (Price - (Price * ISNULL(DiscPercentage, 0) / 100)) AS Price  
									                    FROM ItemTariff it 
									                    WHERE it.ItemID = a.ItemID 
										                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
										                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"'  
										                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112) 
									                    ORDER BY it.StartingDate DESC
								                    ), 0) >= ipmv.StartingValue AND 
								                    ISNULL(
								                    (
									                    SELECT TOP 1 (Price - (Price * ISNULL(DiscPercentage, 0) / 100)) AS Price  
									                    FROM ItemTariff it 
									                    WHERE it.ItemID = a.ItemID 
										                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
										                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
										                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112) 
									                    ORDER BY it.StartingDate DESC
								                    ), 0) <= ipmv.EndingValue AND ipmv.MarginID = d.MarginID
							                    ), 0) / 100
					                    ) 
				                    ELSE 
					                    ISNULL(
					                    (
						                    SELECT TOP 1 (Price - (Price * ISNULL(DiscPercentage, 0) / 100)) AS Price  
						                    FROM ItemTariff it 
						                    WHERE it.ItemID = a.ItemID 
							                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
							                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
							                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
						                    ORDER BY it.StartingDate DESC
					                    ), 0) + 
					                    (
						                    ISNULL(
						                    (
							                    SELECT TOP 1 (Price - (Price * ISNULL(DiscPercentage, 0) / 100)) AS Price  
							                    FROM ItemTariff it 
							                    WHERE it.ItemID = a.ItemID 
								                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
								                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
								                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112) 
							                    ORDER BY it.StartingDate DESC
						                    ), 0) * d.MarginPercentage / 100
					                    )
				                    END
			                    END 
		                    END AS 'Price'
                        >"
                    );
                itemQ.InnerJoin(nonMedicQ).On
                    (
                        itemQ.ItemID == nonMedicQ.ItemID &&
                        itemQ.SRItemType == BusinessObject.Reference.ItemType.NonMedical
                    );
                itemQ.InnerJoin(classQ).On(classQ.ClassID == cboClassID.SelectedValue);
                itemQ.LeftJoin(itemGroupQ).On(itemQ.ItemGroupID == itemGroupQ.ItemGroupID);
                itemQ.Where
                    (
                        itemQ.ItemID.Like(searchTextContain),
                        itemQ.IsActive == true
                    ); 
                itemQ.OrderBy(itemQ.ItemID.Ascending);

                DataTable dtb = itemQ.LoadDataTable();
                foreach (DataRow row in dtb.Rows)
                {
                    row["Price"] = Helper.Rounding(Convert.ToDecimal(row["Price"]), AppEnum.RoundingType.Transaction);
                }
                dtb.AcceptChanges();

                return dtb;
            }
        }

        private DataTable ItemKitchens
        {
            get
            {
                string searchTextContain = string.Format("%{0}%", cboItemID.SelectedValue);
                var itemQ = new ItemQuery("a");
                var itemGroupQ = new ItemGroupQuery("b");
                var kitchenQ = new ItemKitchenQuery("d");
                var classQ = new ClassQuery("e");

                itemQ.es.Top = AppSession.Parameter.MaxResultRecord;
                itemQ.Select
                    (
                        itemQ.ItemID,
                        itemGroupQ.ItemGroupName,
                        itemQ.ItemName,
                        itemQ.IsActive,
                        @"<'' AS 'ServiceUnitName'>",
                        kitchenQ.SRItemUnit,
                        @"<
                            CASE WHEN e.MarginPercentage > 0 
		                    THEN 
			                    ISNULL(
			                    (
				                    SELECT TOP 1 (Price - (Price * ISNULL(DiscPercentage, 0) / 100)) AS Price  
				                    FROM ItemTariff it 
				                    WHERE it.ItemID = a.ItemID 
					                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
					                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
					                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
				                    ORDER BY it.StartingDate DESC
			                    ), 0) + 
			                    (
				                    ISNULL(
				                    (
					                    SELECT TOP 1 (Price - (Price * ISNULL(DiscPercentage, 0) / 100)) AS Price  
					                    FROM ItemTariff it 
					                    WHERE it.ItemID = a.ItemID 
						                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
						                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
						                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
					                    ORDER BY it.StartingDate DESC
				                    ), 0) * e.MarginPercentage / 100
			                    )
		                    ELSE  
			                    ISNULL(
					                    (
						                    SELECT TOP 1 (Price - (Price * ISNULL(DiscPercentage, 0) / 100)) AS Price  
						                    FROM ItemTariff it 
						                    WHERE it.ItemID = a.ItemID 
							                    AND it.ClassID = '" + cboClassID.SelectedValue + @"'
							                    AND it.SRTariffType = '" + cboSRTariffType.SelectedValue + @"' 
							                    AND CONVERT(CHAR(8), it.StartingDate, 112) <= CONVERT(CHAR(8), GETDATE(), 112)  
						                    ORDER BY it.StartingDate DESC
					                    ), 0)
		                    END AS 'Price'
                        >"
                    );
                itemQ.InnerJoin(kitchenQ).On
                    (
                        itemQ.ItemID == kitchenQ.ItemID &&
                        itemQ.SRItemType == BusinessObject.Reference.ItemType.Kitchen
                    );
                itemQ.InnerJoin(classQ).On(classQ.ClassID == cboClassID.SelectedValue);
                itemQ.LeftJoin(itemGroupQ).On(itemQ.ItemGroupID == itemGroupQ.ItemGroupID);
                itemQ.Where
                    (
                        itemQ.ItemID.Like(searchTextContain),
                        itemQ.IsActive == true
                    );
                itemQ.OrderBy(itemQ.ItemID.Ascending);

                DataTable dtb = itemQ.LoadDataTable();
                foreach (DataRow row in dtb.Rows)
                {
                    row["Price"] = Helper.Rounding(Convert.ToDecimal(row["Price"]), AppEnum.RoundingType.Transaction);
                }
                dtb.AcceptChanges();

                return dtb;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            switch (cboSRItemType.SelectedValue)
            {
                case BusinessObject.Reference.ItemType.Service:
                    grdTariff.DataSource = ItemServices;
                    break;
                case BusinessObject.Reference.ItemType.Diagnostic:
                    grdTariff.DataSource = ItemDiagnostics;
                    break;
                case BusinessObject.Reference.ItemType.Radiology:
                    grdTariff.DataSource = ItemRadiologies;
                    break;
                case BusinessObject.Reference.ItemType.Laboratory:
                    grdTariff.DataSource = ItemLaboratories;
                    break;
                case BusinessObject.Reference.ItemType.Medical:
                    grdTariff.DataSource = ItemProductMedics;
                    break;
                case BusinessObject.Reference.ItemType.NonMedical:
                    grdTariff.DataSource = ItemProductNonMedics;
                    break;
                case BusinessObject.Reference.ItemType.Package:
                    grdTariff.DataSource = ItemPackages;
                    break;
                case BusinessObject.Reference.ItemType.Kitchen:
                    grdTariff.DataSource = ItemKitchens;
                    break;
            }
            grdTariff.DataBind();
        }

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ItemItemsRequested((RadComboBox)sender, e.Text, cboSRItemType.SelectedValue);
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemItemDataBound(e);
        }

        protected void cboSRItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItemID.Items.Clear();
            cboItemID.Text = string.Empty;
            //ComboBox.ItemItemsRequested(cboItemID, e.Text, cboSRItemType.SelectedValue);
        }
    }
}
