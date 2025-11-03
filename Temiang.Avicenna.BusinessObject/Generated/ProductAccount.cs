/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/22/2023 9:56:51 AM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
	[Serializable]
	abstract public class esProductAccountCollection : esEntityCollectionWAuditLog
	{
		public esProductAccountCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ProductAccountCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esProductAccountQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			query.es2.Connection = ((IEntityCollection)this).Connection;
		}

		protected bool OnQueryLoaded(DataTable table)
		{
			this.PopulateCollection(table);
			return (this.RowCount > 0) ? true : false;
		}
		
		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery(query as esProductAccountQuery);
		}
		#endregion
			
		virtual public ProductAccount DetachEntity(ProductAccount entity)
		{
			return base.DetachEntity(entity) as ProductAccount;
		}
		
		virtual public ProductAccount AttachEntity(ProductAccount entity)
		{
			return base.AttachEntity(entity) as ProductAccount;
		}
		
		virtual public void Combine(ProductAccountCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ProductAccount this[int index]
		{
			get
			{
				return base[index] as ProductAccount;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ProductAccount);
		}
	}

	[Serializable]
	abstract public class esProductAccount : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esProductAccountQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esProductAccount()
		{
		}
	
		public esProductAccount(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String productAccountID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(productAccountID);
			else
				return LoadByPrimaryKeyStoredProcedure(productAccountID);
		}
	
		/// <summary>
		/// Loads an entity by primary key
		/// </summary>
		/// <remarks>
		/// Requires primary keys be defined on all tables.
		/// If a table does not have a primary key set,
		/// this method will not compile.
		/// </remarks>
		/// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String productAccountID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(productAccountID);
			else
				return LoadByPrimaryKeyStoredProcedure(productAccountID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String productAccountID)
		{
			esProductAccountQuery query = this.GetDynamicQuery();
			query.Where(query.ProductAccountID==productAccountID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String productAccountID)
		{
			esParameters parms = new esParameters();
			parms.Add("ProductAccountID",productAccountID);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}		
		#endregion
		
		#region Properties
		
		public override void SetProperties(IDictionary values)
		{
			foreach (string propertyName in values.Keys)
			{
				this.SetProperty(propertyName, values[propertyName]);
			}
		}

		public override void SetProperty(string name, object value)
		{
			if(this.Row == null) this.AddNew();
			
			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if(value == null || value is System.String)
				{				
					// Use the strongly typed property
					switch (name)
					{
						case "ProductAccountID": this.str.ProductAccountID = (string)value; break;
						case "ProductAccountName": this.str.ProductAccountName = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "ChartOfAccountIdIncome": this.str.ChartOfAccountIdIncome = (string)value; break;
						case "SubledgerIdIncome": this.str.SubledgerIdIncome = (string)value; break;
						case "ChartOfAccountIdInventory": this.str.ChartOfAccountIdInventory = (string)value; break;
						case "SubledgerIdInventory": this.str.SubledgerIdInventory = (string)value; break;
						case "ChartOfAccountIdCOGS": this.str.ChartOfAccountIdCOGS = (string)value; break;
						case "SubledgerIdCOGS": this.str.SubledgerIdCOGS = (string)value; break;
						case "ChartOfAccountIdSalesReturn": this.str.ChartOfAccountIdSalesReturn = (string)value; break;
						case "SubledgerIdSalesReturn": this.str.SubledgerIdSalesReturn = (string)value; break;
						case "ChartOfAccountIdPurchaseReturn": this.str.ChartOfAccountIdPurchaseReturn = (string)value; break;
						case "SubledgerIdPurchaseReturn": this.str.SubledgerIdPurchaseReturn = (string)value; break;
						case "ChartOfAccountIdAcrual": this.str.ChartOfAccountIdAcrual = (string)value; break;
						case "SubledgerIdAcrual": this.str.SubledgerIdAcrual = (string)value; break;
						case "ChartOfAccountIdDiscount": this.str.ChartOfAccountIdDiscount = (string)value; break;
						case "SubledgerIdDiscount": this.str.SubledgerIdDiscount = (string)value; break;
						case "ChartOfAccountIdCost": this.str.ChartOfAccountIdCost = (string)value; break;
						case "SubledgerIdCost": this.str.SubledgerIdCost = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ChartOfAccountIdIncomeIP": this.str.ChartOfAccountIdIncomeIP = (string)value; break;
						case "SubledgerIdIncomeIP": this.str.SubledgerIdIncomeIP = (string)value; break;
						case "ChartOfAccountIdInventoryIP": this.str.ChartOfAccountIdInventoryIP = (string)value; break;
						case "SubledgerIdInventoryIP": this.str.SubledgerIdInventoryIP = (string)value; break;
						case "ChartOfAccountIdCOGSIP": this.str.ChartOfAccountIdCOGSIP = (string)value; break;
						case "SubledgerIdCOGSIP": this.str.SubledgerIdCOGSIP = (string)value; break;
						case "ChartOfAccountIdSalesReturnIP": this.str.ChartOfAccountIdSalesReturnIP = (string)value; break;
						case "SubledgerIdSalesReturnIP": this.str.SubledgerIdSalesReturnIP = (string)value; break;
						case "ChartOfAccountIdPurchaseReturnIP": this.str.ChartOfAccountIdPurchaseReturnIP = (string)value; break;
						case "SubledgerIdPurchaseReturnIP": this.str.SubledgerIdPurchaseReturnIP = (string)value; break;
						case "ChartOfAccountIdAcrualIP": this.str.ChartOfAccountIdAcrualIP = (string)value; break;
						case "SubledgerIdAcrualIP": this.str.SubledgerIdAcrualIP = (string)value; break;
						case "ChartOfAccountIdDiscountIP": this.str.ChartOfAccountIdDiscountIP = (string)value; break;
						case "SubledgerIdDiscountIP": this.str.SubledgerIdDiscountIP = (string)value; break;
						case "ChartOfAccountIdCostIP": this.str.ChartOfAccountIdCostIP = (string)value; break;
						case "SubledgerIdCostIP": this.str.SubledgerIdCostIP = (string)value; break;
						case "ChartOfAccountIdIncomeIGD": this.str.ChartOfAccountIdIncomeIGD = (string)value; break;
						case "SubledgerIdIncomeIGD": this.str.SubledgerIdIncomeIGD = (string)value; break;
						case "ChartOfAccountIdInventoryIGD": this.str.ChartOfAccountIdInventoryIGD = (string)value; break;
						case "SubledgerIdInventoryIGD": this.str.SubledgerIdInventoryIGD = (string)value; break;
						case "ChartOfAccountIdCOGSIGD": this.str.ChartOfAccountIdCOGSIGD = (string)value; break;
						case "SubledgerIdCOGSIGD": this.str.SubledgerIdCOGSIGD = (string)value; break;
						case "ChartOfAccountIdSalesReturnIGD": this.str.ChartOfAccountIdSalesReturnIGD = (string)value; break;
						case "SubledgerIdSalesReturnIGD": this.str.SubledgerIdSalesReturnIGD = (string)value; break;
						case "ChartOfAccountIdPurchaseReturnIGD": this.str.ChartOfAccountIdPurchaseReturnIGD = (string)value; break;
						case "SubledgerIdPurchaseReturnIGD": this.str.SubledgerIdPurchaseReturnIGD = (string)value; break;
						case "ChartOfAccountIdAcrualIGD": this.str.ChartOfAccountIdAcrualIGD = (string)value; break;
						case "SubledgerIdAcrualIGD": this.str.SubledgerIdAcrualIGD = (string)value; break;
						case "ChartOfAccountIdDiscountIGD": this.str.ChartOfAccountIdDiscountIGD = (string)value; break;
						case "SubledgerIdDiscountIGD": this.str.SubledgerIdDiscountIGD = (string)value; break;
						case "ChartOfAccountIdCostIGD": this.str.ChartOfAccountIdCostIGD = (string)value; break;
						case "SubledgerIdCostIGD": this.str.SubledgerIdCostIGD = (string)value; break;
						case "SRItemType": this.str.SRItemType = (string)value; break;
						case "ChartOfAccountIdCOGSOPTemp": this.str.ChartOfAccountIdCOGSOPTemp = (string)value; break;
						case "SubledgerIdCOGSOPTemp": this.str.SubledgerIdCOGSOPTemp = (string)value; break;
						case "ChartOfAccountIdCOGSIPTemp": this.str.ChartOfAccountIdCOGSIPTemp = (string)value; break;
						case "SubledgerIdCOGSIPTemp": this.str.SubledgerIdCOGSIPTemp = (string)value; break;
						case "ChartOfAccountIdCOGSIGDTemp": this.str.ChartOfAccountIdCOGSIGDTemp = (string)value; break;
						case "SubledgerIdCOGSIGDTemp": this.str.SubledgerIdCOGSIGDTemp = (string)value; break;
						case "IsPpnOpr": this.str.IsPpnOpr = (string)value; break;
						case "IsPpnEmr": this.str.IsPpnEmr = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "ChartOfAccountIdIncome":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdIncome = (System.Int32?)value;
							break;
						case "SubledgerIdIncome":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdIncome = (System.Int32?)value;
							break;
						case "ChartOfAccountIdInventory":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdInventory = (System.Int32?)value;
							break;
						case "SubledgerIdInventory":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdInventory = (System.Int32?)value;
							break;
						case "ChartOfAccountIdCOGS":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdCOGS = (System.Int32?)value;
							break;
						case "SubledgerIdCOGS":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdCOGS = (System.Int32?)value;
							break;
						case "ChartOfAccountIdSalesReturn":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdSalesReturn = (System.Int32?)value;
							break;
						case "SubledgerIdSalesReturn":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdSalesReturn = (System.Int32?)value;
							break;
						case "ChartOfAccountIdPurchaseReturn":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdPurchaseReturn = (System.Int32?)value;
							break;
						case "SubledgerIdPurchaseReturn":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdPurchaseReturn = (System.Int32?)value;
							break;
						case "ChartOfAccountIdAcrual":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdAcrual = (System.Int32?)value;
							break;
						case "SubledgerIdAcrual":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdAcrual = (System.Int32?)value;
							break;
						case "ChartOfAccountIdDiscount":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdDiscount = (System.Int32?)value;
							break;
						case "SubledgerIdDiscount":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdDiscount = (System.Int32?)value;
							break;
						case "ChartOfAccountIdCost":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdCost = (System.Int32?)value;
							break;
						case "SubledgerIdCost":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdCost = (System.Int32?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "ChartOfAccountIdIncomeIP":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdIncomeIP = (System.Int32?)value;
							break;
						case "SubledgerIdIncomeIP":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdIncomeIP = (System.Int32?)value;
							break;
						case "ChartOfAccountIdInventoryIP":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdInventoryIP = (System.Int32?)value;
							break;
						case "SubledgerIdInventoryIP":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdInventoryIP = (System.Int32?)value;
							break;
						case "ChartOfAccountIdCOGSIP":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdCOGSIP = (System.Int32?)value;
							break;
						case "SubledgerIdCOGSIP":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdCOGSIP = (System.Int32?)value;
							break;
						case "ChartOfAccountIdSalesReturnIP":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdSalesReturnIP = (System.Int32?)value;
							break;
						case "SubledgerIdSalesReturnIP":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdSalesReturnIP = (System.Int32?)value;
							break;
						case "ChartOfAccountIdPurchaseReturnIP":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdPurchaseReturnIP = (System.Int32?)value;
							break;
						case "SubledgerIdPurchaseReturnIP":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdPurchaseReturnIP = (System.Int32?)value;
							break;
						case "ChartOfAccountIdAcrualIP":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdAcrualIP = (System.Int32?)value;
							break;
						case "SubledgerIdAcrualIP":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdAcrualIP = (System.Int32?)value;
							break;
						case "ChartOfAccountIdDiscountIP":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdDiscountIP = (System.Int32?)value;
							break;
						case "SubledgerIdDiscountIP":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdDiscountIP = (System.Int32?)value;
							break;
						case "ChartOfAccountIdCostIP":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdCostIP = (System.Int32?)value;
							break;
						case "SubledgerIdCostIP":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdCostIP = (System.Int32?)value;
							break;
						case "ChartOfAccountIdIncomeIGD":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdIncomeIGD = (System.Int32?)value;
							break;
						case "SubledgerIdIncomeIGD":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdIncomeIGD = (System.Int32?)value;
							break;
						case "ChartOfAccountIdInventoryIGD":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdInventoryIGD = (System.Int32?)value;
							break;
						case "SubledgerIdInventoryIGD":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdInventoryIGD = (System.Int32?)value;
							break;
						case "ChartOfAccountIdCOGSIGD":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdCOGSIGD = (System.Int32?)value;
							break;
						case "SubledgerIdCOGSIGD":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdCOGSIGD = (System.Int32?)value;
							break;
						case "ChartOfAccountIdSalesReturnIGD":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdSalesReturnIGD = (System.Int32?)value;
							break;
						case "SubledgerIdSalesReturnIGD":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdSalesReturnIGD = (System.Int32?)value;
							break;
						case "ChartOfAccountIdPurchaseReturnIGD":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdPurchaseReturnIGD = (System.Int32?)value;
							break;
						case "SubledgerIdPurchaseReturnIGD":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdPurchaseReturnIGD = (System.Int32?)value;
							break;
						case "ChartOfAccountIdAcrualIGD":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdAcrualIGD = (System.Int32?)value;
							break;
						case "SubledgerIdAcrualIGD":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdAcrualIGD = (System.Int32?)value;
							break;
						case "ChartOfAccountIdDiscountIGD":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdDiscountIGD = (System.Int32?)value;
							break;
						case "SubledgerIdDiscountIGD":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdDiscountIGD = (System.Int32?)value;
							break;
						case "ChartOfAccountIdCostIGD":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdCostIGD = (System.Int32?)value;
							break;
						case "SubledgerIdCostIGD":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdCostIGD = (System.Int32?)value;
							break;
						case "ChartOfAccountIdCOGSOPTemp":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdCOGSOPTemp = (System.Int32?)value;
							break;
						case "SubledgerIdCOGSOPTemp":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdCOGSOPTemp = (System.Int32?)value;
							break;
						case "ChartOfAccountIdCOGSIPTemp":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdCOGSIPTemp = (System.Int32?)value;
							break;
						case "SubledgerIdCOGSIPTemp":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdCOGSIPTemp = (System.Int32?)value;
							break;
						case "ChartOfAccountIdCOGSIGDTemp":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdCOGSIGDTemp = (System.Int32?)value;
							break;
						case "SubledgerIdCOGSIGDTemp":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdCOGSIGDTemp = (System.Int32?)value;
							break;
						case "IsPpnOpr":
						
							if (value == null || value is System.Boolean)
								this.IsPpnOpr = (System.Boolean?)value;
							break;
						case "IsPpnEmr":
						
							if (value == null || value is System.Boolean)
								this.IsPpnEmr = (System.Boolean?)value;
							break;
					
						default:
							break;
					}
				}
			}
			else if(this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to ProductAccount.ProductAccountID
		/// </summary>
		virtual public System.String ProductAccountID
		{
			get
			{
				return base.GetSystemString(ProductAccountMetadata.ColumnNames.ProductAccountID);
			}
			
			set
			{
				base.SetSystemString(ProductAccountMetadata.ColumnNames.ProductAccountID, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ProductAccountName
		/// </summary>
		virtual public System.String ProductAccountName
		{
			get
			{
				return base.GetSystemString(ProductAccountMetadata.ColumnNames.ProductAccountName);
			}
			
			set
			{
				base.SetSystemString(ProductAccountMetadata.ColumnNames.ProductAccountName, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ProductAccountMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(ProductAccountMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdIncome
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdIncome
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdIncome);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdIncome, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdIncome
		/// </summary>
		virtual public System.Int32? SubledgerIdIncome
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdIncome);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdIncome, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdInventory
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdInventory
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdInventory);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdInventory, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdInventory
		/// </summary>
		virtual public System.Int32? SubledgerIdInventory
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdInventory);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdInventory, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdCOGS
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCOGS
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGS);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGS, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdCOGS
		/// </summary>
		virtual public System.Int32? SubledgerIdCOGS
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdCOGS);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdCOGS, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdSalesReturn
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdSalesReturn
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdSalesReturn);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdSalesReturn, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdSalesReturn
		/// </summary>
		virtual public System.Int32? SubledgerIdSalesReturn
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdSalesReturn);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdSalesReturn, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdPurchaseReturn
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdPurchaseReturn
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdPurchaseReturn);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdPurchaseReturn, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdPurchaseReturn
		/// </summary>
		virtual public System.Int32? SubledgerIdPurchaseReturn
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdPurchaseReturn);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdPurchaseReturn, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdAcrual
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdAcrual
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdAcrual);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdAcrual, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdAcrual
		/// </summary>
		virtual public System.Int32? SubledgerIdAcrual
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdAcrual);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdAcrual, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdDiscount
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdDiscount
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdDiscount);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdDiscount, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdDiscount
		/// </summary>
		virtual public System.Int32? SubledgerIdDiscount
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdDiscount);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdDiscount, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdCost
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCost
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCost);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCost, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdCost
		/// </summary>
		virtual public System.Int32? SubledgerIdCost
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdCost);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdCost, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ProductAccountMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ProductAccountMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ProductAccountMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ProductAccountMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdIncomeIP
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdIncomeIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdIncomeIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdIncomeIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdIncomeIP
		/// </summary>
		virtual public System.Int32? SubledgerIdIncomeIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdIncomeIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdIncomeIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdInventoryIP
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdInventoryIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdInventoryIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdInventoryIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdInventoryIP
		/// </summary>
		virtual public System.Int32? SubledgerIdInventoryIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdInventoryIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdInventoryIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdCOGSIP
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCOGSIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdCOGSIP
		/// </summary>
		virtual public System.Int32? SubledgerIdCOGSIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdCOGSIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdCOGSIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdSalesReturnIP
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdSalesReturnIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdSalesReturnIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdSalesReturnIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdSalesReturnIP
		/// </summary>
		virtual public System.Int32? SubledgerIdSalesReturnIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdSalesReturnIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdSalesReturnIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdPurchaseReturnIP
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdPurchaseReturnIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdPurchaseReturnIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdPurchaseReturnIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdPurchaseReturnIP
		/// </summary>
		virtual public System.Int32? SubledgerIdPurchaseReturnIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdPurchaseReturnIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdPurchaseReturnIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdAcrualIP
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdAcrualIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdAcrualIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdAcrualIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdAcrualIP
		/// </summary>
		virtual public System.Int32? SubledgerIdAcrualIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdAcrualIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdAcrualIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdDiscountIP
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdDiscountIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdDiscountIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdDiscountIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdDiscountIP
		/// </summary>
		virtual public System.Int32? SubledgerIdDiscountIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdDiscountIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdDiscountIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdCostIP
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCostIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCostIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCostIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdCostIP
		/// </summary>
		virtual public System.Int32? SubledgerIdCostIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdCostIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdCostIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdIncomeIGD
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdIncomeIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdIncomeIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdIncomeIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdIncomeIGD
		/// </summary>
		virtual public System.Int32? SubledgerIdIncomeIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdIncomeIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdIncomeIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdInventoryIGD
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdInventoryIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdInventoryIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdInventoryIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdInventoryIGD
		/// </summary>
		virtual public System.Int32? SubledgerIdInventoryIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdInventoryIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdInventoryIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdCOGSIGD
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCOGSIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdCOGSIGD
		/// </summary>
		virtual public System.Int32? SubledgerIdCOGSIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdCOGSIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdCOGSIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdSalesReturnIGD
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdSalesReturnIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdSalesReturnIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdSalesReturnIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdSalesReturnIGD
		/// </summary>
		virtual public System.Int32? SubledgerIdSalesReturnIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdSalesReturnIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdSalesReturnIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdPurchaseReturnIGD
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdPurchaseReturnIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdPurchaseReturnIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdPurchaseReturnIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdPurchaseReturnIGD
		/// </summary>
		virtual public System.Int32? SubledgerIdPurchaseReturnIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdPurchaseReturnIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdPurchaseReturnIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdAcrualIGD
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdAcrualIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdAcrualIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdAcrualIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdAcrualIGD
		/// </summary>
		virtual public System.Int32? SubledgerIdAcrualIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdAcrualIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdAcrualIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdDiscountIGD
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdDiscountIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdDiscountIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdDiscountIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdDiscountIGD
		/// </summary>
		virtual public System.Int32? SubledgerIdDiscountIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdDiscountIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdDiscountIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdCostIGD
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCostIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCostIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCostIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdCostIGD
		/// </summary>
		virtual public System.Int32? SubledgerIdCostIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdCostIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdCostIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SRItemType
		/// </summary>
		virtual public System.String SRItemType
		{
			get
			{
				return base.GetSystemString(ProductAccountMetadata.ColumnNames.SRItemType);
			}
			
			set
			{
				base.SetSystemString(ProductAccountMetadata.ColumnNames.SRItemType, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdCOGSOPTemp
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCOGSOPTemp
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSOPTemp);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSOPTemp, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdCOGSOPTemp
		/// </summary>
		virtual public System.Int32? SubledgerIdCOGSOPTemp
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdCOGSOPTemp);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdCOGSOPTemp, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdCOGSIPTemp
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCOGSIPTemp
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSIPTemp);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSIPTemp, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdCOGSIPTemp
		/// </summary>
		virtual public System.Int32? SubledgerIdCOGSIPTemp
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdCOGSIPTemp);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdCOGSIPTemp, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.ChartOfAccountIdCOGSIGDTemp
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCOGSIGDTemp
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSIGDTemp);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSIGDTemp, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.SubledgerIdCOGSIGDTemp
		/// </summary>
		virtual public System.Int32? SubledgerIdCOGSIGDTemp
		{
			get
			{
				return base.GetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdCOGSIGDTemp);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountMetadata.ColumnNames.SubledgerIdCOGSIGDTemp, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.IsPpnOpr
		/// </summary>
		virtual public System.Boolean? IsPpnOpr
		{
			get
			{
				return base.GetSystemBoolean(ProductAccountMetadata.ColumnNames.IsPpnOpr);
			}
			
			set
			{
				base.SetSystemBoolean(ProductAccountMetadata.ColumnNames.IsPpnOpr, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccount.IsPpnEmr
		/// </summary>
		virtual public System.Boolean? IsPpnEmr
		{
			get
			{
				return base.GetSystemBoolean(ProductAccountMetadata.ColumnNames.IsPpnEmr);
			}
			
			set
			{
				base.SetSystemBoolean(ProductAccountMetadata.ColumnNames.IsPpnEmr, value);
			}
		}
		
		#endregion	

		#region String Properties
		
		/// <summary>
		/// Converts an entity's properties to
		/// and from strings.
		/// </summary>
		/// <remarks>
		/// The str properties Get and Set provide easy conversion
		/// between a string and a property's data type. Not all
		/// data types will get a str property.
		/// </remarks>
		/// <example>
		/// Set a datetime from a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// entity.str.HireDate = "2007-01-01 00:00:00";
		/// entity.Save();
		/// </code>
		/// Get a datetime as a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// string theDate = entity.str.HireDate;
		/// </code>
		/// </example>
		[BrowsableAttribute( false )]		
		public esStrings str
		{
			get
			{
				if (esstrings == null)
				{
					esstrings = new esStrings(this);
				}
				return esstrings;
			}
		}

		[Serializable]
		sealed public class esStrings
		{
			public esStrings(esProductAccount entity)
			{
				this.entity = entity;
			}
			public System.String ProductAccountID
			{
				get
				{
					System.String data = entity.ProductAccountID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProductAccountID = null;
					else entity.ProductAccountID = Convert.ToString(value);
				}
			}
			public System.String ProductAccountName
			{
				get
				{
					System.String data = entity.ProductAccountName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProductAccountName = null;
					else entity.ProductAccountName = Convert.ToString(value);
				}
			}
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
				}
			}
			public System.String ChartOfAccountIdIncome
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdIncome;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdIncome = null;
					else entity.ChartOfAccountIdIncome = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdIncome
			{
				get
				{
					System.Int32? data = entity.SubledgerIdIncome;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdIncome = null;
					else entity.SubledgerIdIncome = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdInventory
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdInventory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdInventory = null;
					else entity.ChartOfAccountIdInventory = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdInventory
			{
				get
				{
					System.Int32? data = entity.SubledgerIdInventory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdInventory = null;
					else entity.SubledgerIdInventory = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdCOGS
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdCOGS;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdCOGS = null;
					else entity.ChartOfAccountIdCOGS = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdCOGS
			{
				get
				{
					System.Int32? data = entity.SubledgerIdCOGS;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdCOGS = null;
					else entity.SubledgerIdCOGS = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdSalesReturn
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdSalesReturn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdSalesReturn = null;
					else entity.ChartOfAccountIdSalesReturn = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdSalesReturn
			{
				get
				{
					System.Int32? data = entity.SubledgerIdSalesReturn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdSalesReturn = null;
					else entity.SubledgerIdSalesReturn = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdPurchaseReturn
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdPurchaseReturn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdPurchaseReturn = null;
					else entity.ChartOfAccountIdPurchaseReturn = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdPurchaseReturn
			{
				get
				{
					System.Int32? data = entity.SubledgerIdPurchaseReturn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdPurchaseReturn = null;
					else entity.SubledgerIdPurchaseReturn = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdAcrual
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdAcrual;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdAcrual = null;
					else entity.ChartOfAccountIdAcrual = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdAcrual
			{
				get
				{
					System.Int32? data = entity.SubledgerIdAcrual;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdAcrual = null;
					else entity.SubledgerIdAcrual = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdDiscount
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdDiscount = null;
					else entity.ChartOfAccountIdDiscount = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdDiscount
			{
				get
				{
					System.Int32? data = entity.SubledgerIdDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdDiscount = null;
					else entity.SubledgerIdDiscount = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdCost
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdCost = null;
					else entity.ChartOfAccountIdCost = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdCost
			{
				get
				{
					System.Int32? data = entity.SubledgerIdCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdCost = null;
					else entity.SubledgerIdCost = Convert.ToInt32(value);
				}
			}
			public System.String LastUpdateDateTime
			{
				get
				{
					System.DateTime? data = entity.LastUpdateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdateDateTime = null;
					else entity.LastUpdateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastUpdateByUserID
			{
				get
				{
					System.String data = entity.LastUpdateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdateByUserID = null;
					else entity.LastUpdateByUserID = Convert.ToString(value);
				}
			}
			public System.String ChartOfAccountIdIncomeIP
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdIncomeIP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdIncomeIP = null;
					else entity.ChartOfAccountIdIncomeIP = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdIncomeIP
			{
				get
				{
					System.Int32? data = entity.SubledgerIdIncomeIP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdIncomeIP = null;
					else entity.SubledgerIdIncomeIP = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdInventoryIP
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdInventoryIP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdInventoryIP = null;
					else entity.ChartOfAccountIdInventoryIP = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdInventoryIP
			{
				get
				{
					System.Int32? data = entity.SubledgerIdInventoryIP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdInventoryIP = null;
					else entity.SubledgerIdInventoryIP = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdCOGSIP
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdCOGSIP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdCOGSIP = null;
					else entity.ChartOfAccountIdCOGSIP = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdCOGSIP
			{
				get
				{
					System.Int32? data = entity.SubledgerIdCOGSIP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdCOGSIP = null;
					else entity.SubledgerIdCOGSIP = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdSalesReturnIP
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdSalesReturnIP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdSalesReturnIP = null;
					else entity.ChartOfAccountIdSalesReturnIP = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdSalesReturnIP
			{
				get
				{
					System.Int32? data = entity.SubledgerIdSalesReturnIP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdSalesReturnIP = null;
					else entity.SubledgerIdSalesReturnIP = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdPurchaseReturnIP
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdPurchaseReturnIP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdPurchaseReturnIP = null;
					else entity.ChartOfAccountIdPurchaseReturnIP = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdPurchaseReturnIP
			{
				get
				{
					System.Int32? data = entity.SubledgerIdPurchaseReturnIP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdPurchaseReturnIP = null;
					else entity.SubledgerIdPurchaseReturnIP = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdAcrualIP
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdAcrualIP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdAcrualIP = null;
					else entity.ChartOfAccountIdAcrualIP = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdAcrualIP
			{
				get
				{
					System.Int32? data = entity.SubledgerIdAcrualIP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdAcrualIP = null;
					else entity.SubledgerIdAcrualIP = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdDiscountIP
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdDiscountIP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdDiscountIP = null;
					else entity.ChartOfAccountIdDiscountIP = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdDiscountIP
			{
				get
				{
					System.Int32? data = entity.SubledgerIdDiscountIP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdDiscountIP = null;
					else entity.SubledgerIdDiscountIP = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdCostIP
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdCostIP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdCostIP = null;
					else entity.ChartOfAccountIdCostIP = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdCostIP
			{
				get
				{
					System.Int32? data = entity.SubledgerIdCostIP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdCostIP = null;
					else entity.SubledgerIdCostIP = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdIncomeIGD
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdIncomeIGD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdIncomeIGD = null;
					else entity.ChartOfAccountIdIncomeIGD = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdIncomeIGD
			{
				get
				{
					System.Int32? data = entity.SubledgerIdIncomeIGD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdIncomeIGD = null;
					else entity.SubledgerIdIncomeIGD = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdInventoryIGD
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdInventoryIGD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdInventoryIGD = null;
					else entity.ChartOfAccountIdInventoryIGD = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdInventoryIGD
			{
				get
				{
					System.Int32? data = entity.SubledgerIdInventoryIGD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdInventoryIGD = null;
					else entity.SubledgerIdInventoryIGD = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdCOGSIGD
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdCOGSIGD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdCOGSIGD = null;
					else entity.ChartOfAccountIdCOGSIGD = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdCOGSIGD
			{
				get
				{
					System.Int32? data = entity.SubledgerIdCOGSIGD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdCOGSIGD = null;
					else entity.SubledgerIdCOGSIGD = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdSalesReturnIGD
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdSalesReturnIGD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdSalesReturnIGD = null;
					else entity.ChartOfAccountIdSalesReturnIGD = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdSalesReturnIGD
			{
				get
				{
					System.Int32? data = entity.SubledgerIdSalesReturnIGD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdSalesReturnIGD = null;
					else entity.SubledgerIdSalesReturnIGD = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdPurchaseReturnIGD
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdPurchaseReturnIGD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdPurchaseReturnIGD = null;
					else entity.ChartOfAccountIdPurchaseReturnIGD = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdPurchaseReturnIGD
			{
				get
				{
					System.Int32? data = entity.SubledgerIdPurchaseReturnIGD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdPurchaseReturnIGD = null;
					else entity.SubledgerIdPurchaseReturnIGD = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdAcrualIGD
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdAcrualIGD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdAcrualIGD = null;
					else entity.ChartOfAccountIdAcrualIGD = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdAcrualIGD
			{
				get
				{
					System.Int32? data = entity.SubledgerIdAcrualIGD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdAcrualIGD = null;
					else entity.SubledgerIdAcrualIGD = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdDiscountIGD
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdDiscountIGD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdDiscountIGD = null;
					else entity.ChartOfAccountIdDiscountIGD = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdDiscountIGD
			{
				get
				{
					System.Int32? data = entity.SubledgerIdDiscountIGD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdDiscountIGD = null;
					else entity.SubledgerIdDiscountIGD = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdCostIGD
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdCostIGD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdCostIGD = null;
					else entity.ChartOfAccountIdCostIGD = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdCostIGD
			{
				get
				{
					System.Int32? data = entity.SubledgerIdCostIGD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdCostIGD = null;
					else entity.SubledgerIdCostIGD = Convert.ToInt32(value);
				}
			}
			public System.String SRItemType
			{
				get
				{
					System.String data = entity.SRItemType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemType = null;
					else entity.SRItemType = Convert.ToString(value);
				}
			}
			public System.String ChartOfAccountIdCOGSOPTemp
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdCOGSOPTemp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdCOGSOPTemp = null;
					else entity.ChartOfAccountIdCOGSOPTemp = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdCOGSOPTemp
			{
				get
				{
					System.Int32? data = entity.SubledgerIdCOGSOPTemp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdCOGSOPTemp = null;
					else entity.SubledgerIdCOGSOPTemp = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdCOGSIPTemp
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdCOGSIPTemp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdCOGSIPTemp = null;
					else entity.ChartOfAccountIdCOGSIPTemp = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdCOGSIPTemp
			{
				get
				{
					System.Int32? data = entity.SubledgerIdCOGSIPTemp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdCOGSIPTemp = null;
					else entity.SubledgerIdCOGSIPTemp = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdCOGSIGDTemp
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdCOGSIGDTemp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdCOGSIGDTemp = null;
					else entity.ChartOfAccountIdCOGSIGDTemp = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdCOGSIGDTemp
			{
				get
				{
					System.Int32? data = entity.SubledgerIdCOGSIGDTemp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdCOGSIGDTemp = null;
					else entity.SubledgerIdCOGSIGDTemp = Convert.ToInt32(value);
				}
			}
			public System.String IsPpnOpr
			{
				get
				{
					System.Boolean? data = entity.IsPpnOpr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPpnOpr = null;
					else entity.IsPpnOpr = Convert.ToBoolean(value);
				}
			}
			public System.String IsPpnEmr
			{
				get
				{
					System.Boolean? data = entity.IsPpnEmr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPpnEmr = null;
					else entity.IsPpnEmr = Convert.ToBoolean(value);
				}
			}
			private esProductAccount entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esProductAccountQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			query.es2.Connection = ((IEntity)this).Connection;
		}

		[System.Diagnostics.DebuggerNonUserCode]
		protected bool OnQueryLoaded(DataTable table)
		{
			bool dataFound = this.PopulateEntity(table);

			if (this.RowCount > 1)
			{
				throw new Exception("esProductAccount can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ProductAccount : esProductAccount
	{	
	}

	[Serializable]
	abstract public class esProductAccountQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ProductAccountMetadata.Meta();
			}
		}	
			
		public esQueryItem ProductAccountID
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ProductAccountID, esSystemType.String);
			}
		} 
			
		public esQueryItem ProductAccountName
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ProductAccountName, esSystemType.String);
			}
		} 
			
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ChartOfAccountIdIncome
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdIncome, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdIncome
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdIncome, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdInventory
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdInventory, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdInventory
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdInventory, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdCOGS
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGS, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdCOGS
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdCOGS, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdSalesReturn
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdSalesReturn, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdSalesReturn
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdSalesReturn, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdPurchaseReturn
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdPurchaseReturn, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdPurchaseReturn
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdPurchaseReturn, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdAcrual
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdAcrual, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdAcrual
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdAcrual, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdDiscount
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdDiscount, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdDiscount
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdDiscount, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdCost
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdCost, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdCost
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdCost, esSystemType.Int32);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem ChartOfAccountIdIncomeIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdIncomeIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdIncomeIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdIncomeIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdInventoryIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdInventoryIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdInventoryIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdInventoryIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdCOGSIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdCOGSIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdCOGSIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdSalesReturnIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdSalesReturnIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdSalesReturnIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdSalesReturnIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdPurchaseReturnIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdPurchaseReturnIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdPurchaseReturnIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdPurchaseReturnIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdAcrualIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdAcrualIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdAcrualIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdAcrualIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdDiscountIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdDiscountIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdDiscountIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdDiscountIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdCostIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdCostIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdCostIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdCostIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdIncomeIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdIncomeIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdIncomeIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdIncomeIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdInventoryIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdInventoryIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdInventoryIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdInventoryIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdCOGSIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdCOGSIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdCOGSIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdSalesReturnIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdSalesReturnIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdSalesReturnIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdSalesReturnIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdPurchaseReturnIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdPurchaseReturnIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdPurchaseReturnIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdPurchaseReturnIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdAcrualIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdAcrualIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdAcrualIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdAcrualIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdDiscountIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdDiscountIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdDiscountIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdDiscountIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdCostIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdCostIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdCostIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdCostIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SRItemType
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SRItemType, esSystemType.String);
			}
		} 
			
		public esQueryItem ChartOfAccountIdCOGSOPTemp
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSOPTemp, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdCOGSOPTemp
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdCOGSOPTemp, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdCOGSIPTemp
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSIPTemp, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdCOGSIPTemp
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdCOGSIPTemp, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdCOGSIGDTemp
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSIGDTemp, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdCOGSIGDTemp
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.SubledgerIdCOGSIGDTemp, esSystemType.Int32);
			}
		} 
			
		public esQueryItem IsPpnOpr
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.IsPpnOpr, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsPpnEmr
		{
			get
			{
				return new esQueryItem(this, ProductAccountMetadata.ColumnNames.IsPpnEmr, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ProductAccountCollection")]
	public partial class ProductAccountCollection : esProductAccountCollection, IEnumerable< ProductAccount>
	{
		public ProductAccountCollection()
		{

		}	
		
		public static implicit operator List< ProductAccount>(ProductAccountCollection coll)
		{
			List< ProductAccount> list = new List< ProductAccount>();
			
			foreach (ProductAccount emp in coll)
			{
				list.Add(emp);
			}
			
			return list;
		}		
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return  ProductAccountMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProductAccountQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ProductAccount(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ProductAccount();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ProductAccountQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductAccountQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}
		
		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one record was loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(ProductAccountQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ProductAccount AddNew()
		{
			ProductAccount entity = base.AddNewEntity() as ProductAccount;
			
			return entity;		
		}
		public ProductAccount FindByPrimaryKey(String productAccountID)
		{
			return base.FindByPrimaryKey(productAccountID) as ProductAccount;
		}

		#region IEnumerable< ProductAccount> Members

		IEnumerator< ProductAccount> IEnumerable< ProductAccount>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ProductAccount;
			}
		}

		#endregion
		
		private ProductAccountQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ProductAccount' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ProductAccount ({ProductAccountID})")]
	[Serializable]
	public partial class ProductAccount : esProductAccount
	{
		public ProductAccount()
		{
		}	
	
		public ProductAccount(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ProductAccountMetadata.Meta();
			}
		}	
	
		override protected esProductAccountQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProductAccountQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ProductAccountQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductAccountQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}
		
		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one row is loaded.
		/// For an entity, an exception will be thrown
		/// if more than one row is loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(ProductAccountQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ProductAccountQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ProductAccountQuery : esProductAccountQuery
	{
		public ProductAccountQuery()
		{

		}		
		
		public ProductAccountQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ProductAccountQuery";
        }
	}

	[Serializable]
	public partial class ProductAccountMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ProductAccountMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ProductAccountID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ProductAccountID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ProductAccountName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ProductAccountName;
			c.CharacterMaxLength = 100;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ProductAccountMetadata.PropertyNames.IsActive;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdIncome, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdIncome;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdIncome, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdIncome;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdInventory, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdInventory;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdInventory, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdInventory;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGS, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdCOGS;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdCOGS, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdCOGS;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdSalesReturn, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdSalesReturn;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdSalesReturn, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdSalesReturn;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdPurchaseReturn, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdPurchaseReturn;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdPurchaseReturn, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdPurchaseReturn;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdAcrual, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdAcrual;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdAcrual, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdAcrual;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdDiscount, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdDiscount;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdDiscount, 16, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdDiscount;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCost, 17, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdCost;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdCost, 18, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdCost;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.LastUpdateDateTime, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ProductAccountMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.LastUpdateByUserID, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductAccountMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdIncomeIP, 21, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdIncomeIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdIncomeIP, 22, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdIncomeIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdInventoryIP, 23, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdInventoryIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdInventoryIP, 24, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdInventoryIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSIP, 25, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdCOGSIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdCOGSIP, 26, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdCOGSIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdSalesReturnIP, 27, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdSalesReturnIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdSalesReturnIP, 28, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdSalesReturnIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdPurchaseReturnIP, 29, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdPurchaseReturnIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdPurchaseReturnIP, 30, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdPurchaseReturnIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdAcrualIP, 31, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdAcrualIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdAcrualIP, 32, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdAcrualIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdDiscountIP, 33, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdDiscountIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdDiscountIP, 34, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdDiscountIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCostIP, 35, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdCostIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdCostIP, 36, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdCostIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdIncomeIGD, 37, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdIncomeIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdIncomeIGD, 38, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdIncomeIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdInventoryIGD, 39, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdInventoryIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdInventoryIGD, 40, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdInventoryIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSIGD, 41, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdCOGSIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdCOGSIGD, 42, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdCOGSIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdSalesReturnIGD, 43, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdSalesReturnIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdSalesReturnIGD, 44, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdSalesReturnIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdPurchaseReturnIGD, 45, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdPurchaseReturnIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdPurchaseReturnIGD, 46, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdPurchaseReturnIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdAcrualIGD, 47, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdAcrualIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdAcrualIGD, 48, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdAcrualIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdDiscountIGD, 49, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdDiscountIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdDiscountIGD, 50, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdDiscountIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCostIGD, 51, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdCostIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdCostIGD, 52, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdCostIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SRItemType, 53, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SRItemType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSOPTemp, 54, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdCOGSOPTemp;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdCOGSOPTemp, 55, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdCOGSOPTemp;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSIPTemp, 56, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdCOGSIPTemp;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdCOGSIPTemp, 57, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdCOGSIPTemp;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.ChartOfAccountIdCOGSIGDTemp, 58, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.ChartOfAccountIdCOGSIGDTemp;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.SubledgerIdCOGSIGDTemp, 59, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountMetadata.PropertyNames.SubledgerIdCOGSIGDTemp;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.IsPpnOpr, 60, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ProductAccountMetadata.PropertyNames.IsPpnOpr;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountMetadata.ColumnNames.IsPpnEmr, 61, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ProductAccountMetadata.PropertyNames.IsPpnEmr;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ProductAccountMetadata Meta()
		{
			return meta;
		}	
		
		public Guid DataID
		{
			get { return base._dataID; }
		}	
		
		public bool MultiProviderMode
		{
			get { return false; }
		}		

		public esColumnMetadataCollection Columns
		{
			get	{ return base._columns; }
		}
		
		#region ColumnNames
		public class ColumnNames
		{ 
			public const string ProductAccountID = "ProductAccountID";
			public const string ProductAccountName = "ProductAccountName";
			public const string IsActive = "IsActive";
			public const string ChartOfAccountIdIncome = "ChartOfAccountIdIncome";
			public const string SubledgerIdIncome = "SubledgerIdIncome";
			public const string ChartOfAccountIdInventory = "ChartOfAccountIdInventory";
			public const string SubledgerIdInventory = "SubledgerIdInventory";
			public const string ChartOfAccountIdCOGS = "ChartOfAccountIdCOGS";
			public const string SubledgerIdCOGS = "SubledgerIdCOGS";
			public const string ChartOfAccountIdSalesReturn = "ChartOfAccountIdSalesReturn";
			public const string SubledgerIdSalesReturn = "SubledgerIdSalesReturn";
			public const string ChartOfAccountIdPurchaseReturn = "ChartOfAccountIdPurchaseReturn";
			public const string SubledgerIdPurchaseReturn = "SubledgerIdPurchaseReturn";
			public const string ChartOfAccountIdAcrual = "ChartOfAccountIdAcrual";
			public const string SubledgerIdAcrual = "SubledgerIdAcrual";
			public const string ChartOfAccountIdDiscount = "ChartOfAccountIdDiscount";
			public const string SubledgerIdDiscount = "SubledgerIdDiscount";
			public const string ChartOfAccountIdCost = "ChartOfAccountIdCost";
			public const string SubledgerIdCost = "SubledgerIdCost";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ChartOfAccountIdIncomeIP = "ChartOfAccountIdIncomeIP";
			public const string SubledgerIdIncomeIP = "SubledgerIdIncomeIP";
			public const string ChartOfAccountIdInventoryIP = "ChartOfAccountIdInventoryIP";
			public const string SubledgerIdInventoryIP = "SubledgerIdInventoryIP";
			public const string ChartOfAccountIdCOGSIP = "ChartOfAccountIdCOGSIP";
			public const string SubledgerIdCOGSIP = "SubledgerIdCOGSIP";
			public const string ChartOfAccountIdSalesReturnIP = "ChartOfAccountIdSalesReturnIP";
			public const string SubledgerIdSalesReturnIP = "SubledgerIdSalesReturnIP";
			public const string ChartOfAccountIdPurchaseReturnIP = "ChartOfAccountIdPurchaseReturnIP";
			public const string SubledgerIdPurchaseReturnIP = "SubledgerIdPurchaseReturnIP";
			public const string ChartOfAccountIdAcrualIP = "ChartOfAccountIdAcrualIP";
			public const string SubledgerIdAcrualIP = "SubledgerIdAcrualIP";
			public const string ChartOfAccountIdDiscountIP = "ChartOfAccountIdDiscountIP";
			public const string SubledgerIdDiscountIP = "SubledgerIdDiscountIP";
			public const string ChartOfAccountIdCostIP = "ChartOfAccountIdCostIP";
			public const string SubledgerIdCostIP = "SubledgerIdCostIP";
			public const string ChartOfAccountIdIncomeIGD = "ChartOfAccountIdIncomeIGD";
			public const string SubledgerIdIncomeIGD = "SubledgerIdIncomeIGD";
			public const string ChartOfAccountIdInventoryIGD = "ChartOfAccountIdInventoryIGD";
			public const string SubledgerIdInventoryIGD = "SubledgerIdInventoryIGD";
			public const string ChartOfAccountIdCOGSIGD = "ChartOfAccountIdCOGSIGD";
			public const string SubledgerIdCOGSIGD = "SubledgerIdCOGSIGD";
			public const string ChartOfAccountIdSalesReturnIGD = "ChartOfAccountIdSalesReturnIGD";
			public const string SubledgerIdSalesReturnIGD = "SubledgerIdSalesReturnIGD";
			public const string ChartOfAccountIdPurchaseReturnIGD = "ChartOfAccountIdPurchaseReturnIGD";
			public const string SubledgerIdPurchaseReturnIGD = "SubledgerIdPurchaseReturnIGD";
			public const string ChartOfAccountIdAcrualIGD = "ChartOfAccountIdAcrualIGD";
			public const string SubledgerIdAcrualIGD = "SubledgerIdAcrualIGD";
			public const string ChartOfAccountIdDiscountIGD = "ChartOfAccountIdDiscountIGD";
			public const string SubledgerIdDiscountIGD = "SubledgerIdDiscountIGD";
			public const string ChartOfAccountIdCostIGD = "ChartOfAccountIdCostIGD";
			public const string SubledgerIdCostIGD = "SubledgerIdCostIGD";
			public const string SRItemType = "SRItemType";
			public const string ChartOfAccountIdCOGSOPTemp = "ChartOfAccountIdCOGSOPTemp";
			public const string SubledgerIdCOGSOPTemp = "SubledgerIdCOGSOPTemp";
			public const string ChartOfAccountIdCOGSIPTemp = "ChartOfAccountIdCOGSIPTemp";
			public const string SubledgerIdCOGSIPTemp = "SubledgerIdCOGSIPTemp";
			public const string ChartOfAccountIdCOGSIGDTemp = "ChartOfAccountIdCOGSIGDTemp";
			public const string SubledgerIdCOGSIGDTemp = "SubledgerIdCOGSIGDTemp";
			public const string IsPpnOpr = "IsPpnOpr";
			public const string IsPpnEmr = "IsPpnEmr";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ProductAccountID = "ProductAccountID";
			public const string ProductAccountName = "ProductAccountName";
			public const string IsActive = "IsActive";
			public const string ChartOfAccountIdIncome = "ChartOfAccountIdIncome";
			public const string SubledgerIdIncome = "SubledgerIdIncome";
			public const string ChartOfAccountIdInventory = "ChartOfAccountIdInventory";
			public const string SubledgerIdInventory = "SubledgerIdInventory";
			public const string ChartOfAccountIdCOGS = "ChartOfAccountIdCOGS";
			public const string SubledgerIdCOGS = "SubledgerIdCOGS";
			public const string ChartOfAccountIdSalesReturn = "ChartOfAccountIdSalesReturn";
			public const string SubledgerIdSalesReturn = "SubledgerIdSalesReturn";
			public const string ChartOfAccountIdPurchaseReturn = "ChartOfAccountIdPurchaseReturn";
			public const string SubledgerIdPurchaseReturn = "SubledgerIdPurchaseReturn";
			public const string ChartOfAccountIdAcrual = "ChartOfAccountIdAcrual";
			public const string SubledgerIdAcrual = "SubledgerIdAcrual";
			public const string ChartOfAccountIdDiscount = "ChartOfAccountIdDiscount";
			public const string SubledgerIdDiscount = "SubledgerIdDiscount";
			public const string ChartOfAccountIdCost = "ChartOfAccountIdCost";
			public const string SubledgerIdCost = "SubledgerIdCost";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ChartOfAccountIdIncomeIP = "ChartOfAccountIdIncomeIP";
			public const string SubledgerIdIncomeIP = "SubledgerIdIncomeIP";
			public const string ChartOfAccountIdInventoryIP = "ChartOfAccountIdInventoryIP";
			public const string SubledgerIdInventoryIP = "SubledgerIdInventoryIP";
			public const string ChartOfAccountIdCOGSIP = "ChartOfAccountIdCOGSIP";
			public const string SubledgerIdCOGSIP = "SubledgerIdCOGSIP";
			public const string ChartOfAccountIdSalesReturnIP = "ChartOfAccountIdSalesReturnIP";
			public const string SubledgerIdSalesReturnIP = "SubledgerIdSalesReturnIP";
			public const string ChartOfAccountIdPurchaseReturnIP = "ChartOfAccountIdPurchaseReturnIP";
			public const string SubledgerIdPurchaseReturnIP = "SubledgerIdPurchaseReturnIP";
			public const string ChartOfAccountIdAcrualIP = "ChartOfAccountIdAcrualIP";
			public const string SubledgerIdAcrualIP = "SubledgerIdAcrualIP";
			public const string ChartOfAccountIdDiscountIP = "ChartOfAccountIdDiscountIP";
			public const string SubledgerIdDiscountIP = "SubledgerIdDiscountIP";
			public const string ChartOfAccountIdCostIP = "ChartOfAccountIdCostIP";
			public const string SubledgerIdCostIP = "SubledgerIdCostIP";
			public const string ChartOfAccountIdIncomeIGD = "ChartOfAccountIdIncomeIGD";
			public const string SubledgerIdIncomeIGD = "SubledgerIdIncomeIGD";
			public const string ChartOfAccountIdInventoryIGD = "ChartOfAccountIdInventoryIGD";
			public const string SubledgerIdInventoryIGD = "SubledgerIdInventoryIGD";
			public const string ChartOfAccountIdCOGSIGD = "ChartOfAccountIdCOGSIGD";
			public const string SubledgerIdCOGSIGD = "SubledgerIdCOGSIGD";
			public const string ChartOfAccountIdSalesReturnIGD = "ChartOfAccountIdSalesReturnIGD";
			public const string SubledgerIdSalesReturnIGD = "SubledgerIdSalesReturnIGD";
			public const string ChartOfAccountIdPurchaseReturnIGD = "ChartOfAccountIdPurchaseReturnIGD";
			public const string SubledgerIdPurchaseReturnIGD = "SubledgerIdPurchaseReturnIGD";
			public const string ChartOfAccountIdAcrualIGD = "ChartOfAccountIdAcrualIGD";
			public const string SubledgerIdAcrualIGD = "SubledgerIdAcrualIGD";
			public const string ChartOfAccountIdDiscountIGD = "ChartOfAccountIdDiscountIGD";
			public const string SubledgerIdDiscountIGD = "SubledgerIdDiscountIGD";
			public const string ChartOfAccountIdCostIGD = "ChartOfAccountIdCostIGD";
			public const string SubledgerIdCostIGD = "SubledgerIdCostIGD";
			public const string SRItemType = "SRItemType";
			public const string ChartOfAccountIdCOGSOPTemp = "ChartOfAccountIdCOGSOPTemp";
			public const string SubledgerIdCOGSOPTemp = "SubledgerIdCOGSOPTemp";
			public const string ChartOfAccountIdCOGSIPTemp = "ChartOfAccountIdCOGSIPTemp";
			public const string SubledgerIdCOGSIPTemp = "SubledgerIdCOGSIPTemp";
			public const string ChartOfAccountIdCOGSIGDTemp = "ChartOfAccountIdCOGSIGDTemp";
			public const string SubledgerIdCOGSIGDTemp = "SubledgerIdCOGSIGDTemp";
			public const string IsPpnOpr = "IsPpnOpr";
			public const string IsPpnEmr = "IsPpnEmr";
		}
		#endregion	

		public esProviderSpecificMetadata GetProviderMetadata(string mapName)
		{
			MapToMeta mapMethod = mapDelegates[mapName];

			if (mapMethod != null)
				return mapMethod(mapName);
			else
				return null;
		}
		
		#region MAP esDefault
		
		static private int RegisterDelegateesDefault()
		{
			// This is only executed once per the life of the application
			lock (typeof(ProductAccountMetadata))
			{
				if(ProductAccountMetadata.mapDelegates == null)
				{
					ProductAccountMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ProductAccountMetadata.meta == null)
				{
					ProductAccountMetadata.meta = new ProductAccountMetadata();
				}
				
				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}			

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if(!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();
				
				meta.AddTypeMap("ProductAccountID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProductAccountName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ChartOfAccountIdIncome", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdIncome", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdInventory", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdInventory", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdCOGS", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCOGS", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdSalesReturn", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdSalesReturn", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdPurchaseReturn", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdPurchaseReturn", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdAcrual", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdAcrual", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdDiscount", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdDiscount", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdCost", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCost", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountIdIncomeIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdIncomeIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdInventoryIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdInventoryIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdCOGSIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCOGSIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdSalesReturnIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdSalesReturnIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdPurchaseReturnIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdPurchaseReturnIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdAcrualIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdAcrualIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdDiscountIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdDiscountIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdCostIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCostIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdIncomeIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdIncomeIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdInventoryIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdInventoryIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdCOGSIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCOGSIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdSalesReturnIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdSalesReturnIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdPurchaseReturnIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdPurchaseReturnIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdAcrualIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdAcrualIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdDiscountIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdDiscountIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdCostIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCostIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRItemType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountIdCOGSOPTemp", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCOGSOPTemp", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdCOGSIPTemp", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCOGSIPTemp", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdCOGSIGDTemp", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCOGSIGDTemp", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsPpnOpr", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPpnEmr", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "ProductAccount";
				meta.Destination = "ProductAccount";
				meta.spInsert = "proc_ProductAccountInsert";				
				meta.spUpdate = "proc_ProductAccountUpdate";		
				meta.spDelete = "proc_ProductAccountDelete";
				meta.spLoadAll = "proc_ProductAccountLoadAll";
				meta.spLoadByPrimaryKey = "proc_ProductAccountLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ProductAccountMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
