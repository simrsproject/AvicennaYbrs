/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/10/2018 10:39:06 PM
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
	abstract public class esProductAccountGuarantorGroupCollection : esEntityCollectionWAuditLog
	{
		public esProductAccountGuarantorGroupCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ProductAccountGuarantorGroupCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esProductAccountGuarantorGroupQuery query)
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
			this.InitQuery(query as esProductAccountGuarantorGroupQuery);
		}
		#endregion
			
		virtual public ProductAccountGuarantorGroup DetachEntity(ProductAccountGuarantorGroup entity)
		{
			return base.DetachEntity(entity) as ProductAccountGuarantorGroup;
		}
		
		virtual public ProductAccountGuarantorGroup AttachEntity(ProductAccountGuarantorGroup entity)
		{
			return base.AttachEntity(entity) as ProductAccountGuarantorGroup;
		}
		
		virtual public void Combine(ProductAccountGuarantorGroupCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ProductAccountGuarantorGroup this[int index]
		{
			get
			{
				return base[index] as ProductAccountGuarantorGroup;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ProductAccountGuarantorGroup);
		}
	}

	[Serializable]
	abstract public class esProductAccountGuarantorGroup : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esProductAccountGuarantorGroupQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esProductAccountGuarantorGroup()
		{
		}
	
		public esProductAccountGuarantorGroup(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String productAccountID, String sRGuarantorIncomeGroup)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(productAccountID, sRGuarantorIncomeGroup);
			else
				return LoadByPrimaryKeyStoredProcedure(productAccountID, sRGuarantorIncomeGroup);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String productAccountID, String sRGuarantorIncomeGroup)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(productAccountID, sRGuarantorIncomeGroup);
			else
				return LoadByPrimaryKeyStoredProcedure(productAccountID, sRGuarantorIncomeGroup);
		}
	
		private bool LoadByPrimaryKeyDynamic(String productAccountID, String sRGuarantorIncomeGroup)
		{
			esProductAccountGuarantorGroupQuery query = this.GetDynamicQuery();
			query.Where(query.ProductAccountID==productAccountID, query.SRGuarantorIncomeGroup==sRGuarantorIncomeGroup);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String productAccountID, String sRGuarantorIncomeGroup)
		{
			esParameters parms = new esParameters();
			parms.Add("ProductAccountID",productAccountID);
			parms.Add("SRGuarantorIncomeGroup",sRGuarantorIncomeGroup);
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
						case "SRGuarantorIncomeGroup": this.str.SRGuarantorIncomeGroup = (string)value; break;
						case "ChartOfAccountIdIncome": this.str.ChartOfAccountIdIncome = (string)value; break;
						case "SubledgerIdIncome": this.str.SubledgerIdIncome = (string)value; break;
						case "ChartOfAccountIdInventory": this.str.ChartOfAccountIdInventory = (string)value; break;
						case "SubledgerIdInventory": this.str.SubledgerIdInventory = (string)value; break;
						case "ChartOfAccountIdCOGS": this.str.ChartOfAccountIdCOGS = (string)value; break;
						case "SubledgerIdCOGS": this.str.SubledgerIdCOGS = (string)value; break;
						case "ChartOfAccountIdAcrual": this.str.ChartOfAccountIdAcrual = (string)value; break;
						case "SubledgerIdAcrual": this.str.SubledgerIdAcrual = (string)value; break;
						case "ChartOfAccountIdDiscount": this.str.ChartOfAccountIdDiscount = (string)value; break;
						case "SubledgerIdDiscount": this.str.SubledgerIdDiscount = (string)value; break;
						case "ChartOfAccountIdIncomeIP": this.str.ChartOfAccountIdIncomeIP = (string)value; break;
						case "SubledgerIdIncomeIP": this.str.SubledgerIdIncomeIP = (string)value; break;
						case "ChartOfAccountIdInventoryIP": this.str.ChartOfAccountIdInventoryIP = (string)value; break;
						case "SubledgerIdInventoryIP": this.str.SubledgerIdInventoryIP = (string)value; break;
						case "ChartOfAccountIdCOGSIP": this.str.ChartOfAccountIdCOGSIP = (string)value; break;
						case "SubledgerIdCOGSIP": this.str.SubledgerIdCOGSIP = (string)value; break;
						case "ChartOfAccountIdAcrualIP": this.str.ChartOfAccountIdAcrualIP = (string)value; break;
						case "SubledgerIdAcrualIP": this.str.SubledgerIdAcrualIP = (string)value; break;
						case "ChartOfAccountIdDiscountIP": this.str.ChartOfAccountIdDiscountIP = (string)value; break;
						case "SubledgerIdDiscountIP": this.str.SubledgerIdDiscountIP = (string)value; break;
						case "ChartOfAccountIdIncomeIGD": this.str.ChartOfAccountIdIncomeIGD = (string)value; break;
						case "SubledgerIdIncomeIGD": this.str.SubledgerIdIncomeIGD = (string)value; break;
						case "ChartOfAccountIdInventoryIGD": this.str.ChartOfAccountIdInventoryIGD = (string)value; break;
						case "SubledgerIdInventoryIGD": this.str.SubledgerIdInventoryIGD = (string)value; break;
						case "ChartOfAccountIdCOGSIGD": this.str.ChartOfAccountIdCOGSIGD = (string)value; break;
						case "SubledgerIdCOGSIGD": this.str.SubledgerIdCOGSIGD = (string)value; break;
						case "ChartOfAccountIdAcrualIGD": this.str.ChartOfAccountIdAcrualIGD = (string)value; break;
						case "SubledgerIdAcrualIGD": this.str.SubledgerIdAcrualIGD = (string)value; break;
						case "ChartOfAccountIdDiscountIGD": this.str.ChartOfAccountIdDiscountIGD = (string)value; break;
						case "SubledgerIdDiscountIGD": this.str.SubledgerIdDiscountIGD = (string)value; break;
						case "SRItemType": this.str.SRItemType = (string)value; break;
						case "ChartOfAccountIdCOGSOPTemp": this.str.ChartOfAccountIdCOGSOPTemp = (string)value; break;
						case "SubledgerIdCOGSOPTemp": this.str.SubledgerIdCOGSOPTemp = (string)value; break;
						case "ChartOfAccountIdCOGSIPTemp": this.str.ChartOfAccountIdCOGSIPTemp = (string)value; break;
						case "SubledgerIdCOGSIPTemp": this.str.SubledgerIdCOGSIPTemp = (string)value; break;
						case "ChartOfAccountIdCOGSIGDTemp": this.str.ChartOfAccountIdCOGSIGDTemp = (string)value; break;
						case "SubledgerIdCOGSIGDTemp": this.str.SubledgerIdCOGSIGDTemp = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
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
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
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
		/// Maps to ProductAccountGuarantorGroup.ProductAccountID
		/// </summary>
		virtual public System.String ProductAccountID
		{
			get
			{
				return base.GetSystemString(ProductAccountGuarantorGroupMetadata.ColumnNames.ProductAccountID);
			}
			
			set
			{
				base.SetSystemString(ProductAccountGuarantorGroupMetadata.ColumnNames.ProductAccountID, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SRGuarantorIncomeGroup
		/// </summary>
		virtual public System.String SRGuarantorIncomeGroup
		{
			get
			{
				return base.GetSystemString(ProductAccountGuarantorGroupMetadata.ColumnNames.SRGuarantorIncomeGroup);
			}
			
			set
			{
				base.SetSystemString(ProductAccountGuarantorGroupMetadata.ColumnNames.SRGuarantorIncomeGroup, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.ChartOfAccountIdIncome
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdIncome
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdIncome);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdIncome, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SubledgerIdIncome
		/// </summary>
		virtual public System.Int32? SubledgerIdIncome
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdIncome);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdIncome, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.ChartOfAccountIdInventory
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdInventory
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdInventory);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdInventory, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SubledgerIdInventory
		/// </summary>
		virtual public System.Int32? SubledgerIdInventory
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdInventory);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdInventory, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.ChartOfAccountIdCOGS
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCOGS
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGS);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGS, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SubledgerIdCOGS
		/// </summary>
		virtual public System.Int32? SubledgerIdCOGS
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGS);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGS, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.ChartOfAccountIdAcrual
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdAcrual
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdAcrual);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdAcrual, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SubledgerIdAcrual
		/// </summary>
		virtual public System.Int32? SubledgerIdAcrual
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdAcrual);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdAcrual, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.ChartOfAccountIdDiscount
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdDiscount
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdDiscount);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdDiscount, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SubledgerIdDiscount
		/// </summary>
		virtual public System.Int32? SubledgerIdDiscount
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdDiscount);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdDiscount, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.ChartOfAccountIdIncomeIP
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdIncomeIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdIncomeIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdIncomeIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SubledgerIdIncomeIP
		/// </summary>
		virtual public System.Int32? SubledgerIdIncomeIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdIncomeIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdIncomeIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.ChartOfAccountIdInventoryIP
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdInventoryIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdInventoryIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdInventoryIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SubledgerIdInventoryIP
		/// </summary>
		virtual public System.Int32? SubledgerIdInventoryIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdInventoryIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdInventoryIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.ChartOfAccountIdCOGSIP
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCOGSIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SubledgerIdCOGSIP
		/// </summary>
		virtual public System.Int32? SubledgerIdCOGSIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.ChartOfAccountIdAcrualIP
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdAcrualIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdAcrualIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdAcrualIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SubledgerIdAcrualIP
		/// </summary>
		virtual public System.Int32? SubledgerIdAcrualIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdAcrualIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdAcrualIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.ChartOfAccountIdDiscountIP
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdDiscountIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdDiscountIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdDiscountIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SubledgerIdDiscountIP
		/// </summary>
		virtual public System.Int32? SubledgerIdDiscountIP
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdDiscountIP);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdDiscountIP, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.ChartOfAccountIdIncomeIGD
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdIncomeIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdIncomeIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdIncomeIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SubledgerIdIncomeIGD
		/// </summary>
		virtual public System.Int32? SubledgerIdIncomeIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdIncomeIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdIncomeIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.ChartOfAccountIdInventoryIGD
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdInventoryIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdInventoryIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdInventoryIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SubledgerIdInventoryIGD
		/// </summary>
		virtual public System.Int32? SubledgerIdInventoryIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdInventoryIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdInventoryIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.ChartOfAccountIdCOGSIGD
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCOGSIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SubledgerIdCOGSIGD
		/// </summary>
		virtual public System.Int32? SubledgerIdCOGSIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.ChartOfAccountIdAcrualIGD
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdAcrualIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdAcrualIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdAcrualIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SubledgerIdAcrualIGD
		/// </summary>
		virtual public System.Int32? SubledgerIdAcrualIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdAcrualIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdAcrualIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.ChartOfAccountIdDiscountIGD
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdDiscountIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdDiscountIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdDiscountIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SubledgerIdDiscountIGD
		/// </summary>
		virtual public System.Int32? SubledgerIdDiscountIGD
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdDiscountIGD);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdDiscountIGD, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SRItemType
		/// </summary>
		virtual public System.String SRItemType
		{
			get
			{
				return base.GetSystemString(ProductAccountGuarantorGroupMetadata.ColumnNames.SRItemType);
			}
			
			set
			{
				base.SetSystemString(ProductAccountGuarantorGroupMetadata.ColumnNames.SRItemType, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.ChartOfAccountIdCOGSOPTemp
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCOGSOPTemp
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSOPTemp);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSOPTemp, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SubledgerIdCOGSOPTemp
		/// </summary>
		virtual public System.Int32? SubledgerIdCOGSOPTemp
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSOPTemp);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSOPTemp, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.ChartOfAccountIdCOGSIPTemp
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCOGSIPTemp
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSIPTemp);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSIPTemp, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SubledgerIdCOGSIPTemp
		/// </summary>
		virtual public System.Int32? SubledgerIdCOGSIPTemp
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSIPTemp);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSIPTemp, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.ChartOfAccountIdCOGSIGDTemp
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCOGSIGDTemp
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSIGDTemp);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSIGDTemp, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.SubledgerIdCOGSIGDTemp
		/// </summary>
		virtual public System.Int32? SubledgerIdCOGSIGDTemp
		{
			get
			{
				return base.GetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSIGDTemp);
			}
			
			set
			{
				base.SetSystemInt32(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSIGDTemp, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ProductAccountGuarantorGroupMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ProductAccountGuarantorGroupMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ProductAccountGuarantorGroup.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ProductAccountGuarantorGroupMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ProductAccountGuarantorGroupMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esProductAccountGuarantorGroup entity)
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
			public System.String SRGuarantorIncomeGroup
			{
				get
				{
					System.String data = entity.SRGuarantorIncomeGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRGuarantorIncomeGroup = null;
					else entity.SRGuarantorIncomeGroup = Convert.ToString(value);
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
			private esProductAccountGuarantorGroup entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esProductAccountGuarantorGroupQuery query)
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
				throw new Exception("esProductAccountGuarantorGroup can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ProductAccountGuarantorGroup : esProductAccountGuarantorGroup
	{	
	}

	[Serializable]
	abstract public class esProductAccountGuarantorGroupQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ProductAccountGuarantorGroupMetadata.Meta();
			}
		}	
			
		public esQueryItem ProductAccountID
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.ProductAccountID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRGuarantorIncomeGroup
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SRGuarantorIncomeGroup, esSystemType.String);
			}
		} 
			
		public esQueryItem ChartOfAccountIdIncome
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdIncome, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdIncome
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdIncome, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdInventory
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdInventory, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdInventory
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdInventory, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdCOGS
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGS, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdCOGS
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGS, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdAcrual
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdAcrual, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdAcrual
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdAcrual, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdDiscount
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdDiscount, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdDiscount
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdDiscount, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdIncomeIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdIncomeIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdIncomeIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdIncomeIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdInventoryIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdInventoryIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdInventoryIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdInventoryIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdCOGSIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdCOGSIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdAcrualIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdAcrualIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdAcrualIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdAcrualIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdDiscountIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdDiscountIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdDiscountIP
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdDiscountIP, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdIncomeIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdIncomeIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdIncomeIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdIncomeIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdInventoryIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdInventoryIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdInventoryIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdInventoryIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdCOGSIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdCOGSIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdAcrualIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdAcrualIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdAcrualIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdAcrualIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdDiscountIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdDiscountIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdDiscountIGD
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdDiscountIGD, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SRItemType
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SRItemType, esSystemType.String);
			}
		} 
			
		public esQueryItem ChartOfAccountIdCOGSOPTemp
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSOPTemp, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdCOGSOPTemp
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSOPTemp, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdCOGSIPTemp
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSIPTemp, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdCOGSIPTemp
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSIPTemp, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdCOGSIGDTemp
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSIGDTemp, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdCOGSIGDTemp
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSIGDTemp, esSystemType.Int32);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ProductAccountGuarantorGroupMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ProductAccountGuarantorGroupCollection")]
	public partial class ProductAccountGuarantorGroupCollection : esProductAccountGuarantorGroupCollection, IEnumerable< ProductAccountGuarantorGroup>
	{
		public ProductAccountGuarantorGroupCollection()
		{

		}	
		
		public static implicit operator List< ProductAccountGuarantorGroup>(ProductAccountGuarantorGroupCollection coll)
		{
			List< ProductAccountGuarantorGroup> list = new List< ProductAccountGuarantorGroup>();
			
			foreach (ProductAccountGuarantorGroup emp in coll)
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
				return  ProductAccountGuarantorGroupMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProductAccountGuarantorGroupQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ProductAccountGuarantorGroup(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ProductAccountGuarantorGroup();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ProductAccountGuarantorGroupQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductAccountGuarantorGroupQuery();
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
		public bool Load(ProductAccountGuarantorGroupQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ProductAccountGuarantorGroup AddNew()
		{
			ProductAccountGuarantorGroup entity = base.AddNewEntity() as ProductAccountGuarantorGroup;
			
			return entity;		
		}
		public ProductAccountGuarantorGroup FindByPrimaryKey(String productAccountID, String sRGuarantorIncomeGroup)
		{
			return base.FindByPrimaryKey(productAccountID, sRGuarantorIncomeGroup) as ProductAccountGuarantorGroup;
		}

		#region IEnumerable< ProductAccountGuarantorGroup> Members

		IEnumerator< ProductAccountGuarantorGroup> IEnumerable< ProductAccountGuarantorGroup>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ProductAccountGuarantorGroup;
			}
		}

		#endregion
		
		private ProductAccountGuarantorGroupQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ProductAccountGuarantorGroup' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ProductAccountGuarantorGroup ({ProductAccountID, SRGuarantorIncomeGroup})")]
	[Serializable]
	public partial class ProductAccountGuarantorGroup : esProductAccountGuarantorGroup
	{
		public ProductAccountGuarantorGroup()
		{
		}	
	
		public ProductAccountGuarantorGroup(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ProductAccountGuarantorGroupMetadata.Meta();
			}
		}	
	
		override protected esProductAccountGuarantorGroupQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProductAccountGuarantorGroupQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ProductAccountGuarantorGroupQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductAccountGuarantorGroupQuery();
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
		public bool Load(ProductAccountGuarantorGroupQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ProductAccountGuarantorGroupQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ProductAccountGuarantorGroupQuery : esProductAccountGuarantorGroupQuery
	{
		public ProductAccountGuarantorGroupQuery()
		{

		}		
		
		public ProductAccountGuarantorGroupQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ProductAccountGuarantorGroupQuery";
        }
	}

	[Serializable]
	public partial class ProductAccountGuarantorGroupMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ProductAccountGuarantorGroupMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.ProductAccountID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.ProductAccountID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SRGuarantorIncomeGroup, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SRGuarantorIncomeGroup;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdIncome, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.ChartOfAccountIdIncome;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdIncome, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SubledgerIdIncome;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdInventory, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.ChartOfAccountIdInventory;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdInventory, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SubledgerIdInventory;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGS, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.ChartOfAccountIdCOGS;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGS, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SubledgerIdCOGS;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdAcrual, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.ChartOfAccountIdAcrual;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdAcrual, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SubledgerIdAcrual;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdDiscount, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.ChartOfAccountIdDiscount;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdDiscount, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SubledgerIdDiscount;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdIncomeIP, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.ChartOfAccountIdIncomeIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdIncomeIP, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SubledgerIdIncomeIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdInventoryIP, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.ChartOfAccountIdInventoryIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdInventoryIP, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SubledgerIdInventoryIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSIP, 16, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.ChartOfAccountIdCOGSIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSIP, 17, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SubledgerIdCOGSIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdAcrualIP, 18, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.ChartOfAccountIdAcrualIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdAcrualIP, 19, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SubledgerIdAcrualIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdDiscountIP, 20, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.ChartOfAccountIdDiscountIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdDiscountIP, 21, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SubledgerIdDiscountIP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdIncomeIGD, 22, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.ChartOfAccountIdIncomeIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdIncomeIGD, 23, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SubledgerIdIncomeIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdInventoryIGD, 24, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.ChartOfAccountIdInventoryIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdInventoryIGD, 25, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SubledgerIdInventoryIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSIGD, 26, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.ChartOfAccountIdCOGSIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSIGD, 27, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SubledgerIdCOGSIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdAcrualIGD, 28, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.ChartOfAccountIdAcrualIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdAcrualIGD, 29, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SubledgerIdAcrualIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdDiscountIGD, 30, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.ChartOfAccountIdDiscountIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdDiscountIGD, 31, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SubledgerIdDiscountIGD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SRItemType, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SRItemType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSOPTemp, 33, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.ChartOfAccountIdCOGSOPTemp;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSOPTemp, 34, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SubledgerIdCOGSOPTemp;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSIPTemp, 35, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.ChartOfAccountIdCOGSIPTemp;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSIPTemp, 36, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SubledgerIdCOGSIPTemp;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.ChartOfAccountIdCOGSIGDTemp, 37, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.ChartOfAccountIdCOGSIGDTemp;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.SubledgerIdCOGSIGDTemp, 38, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.SubledgerIdCOGSIGDTemp;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.LastUpdateDateTime, 39, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ProductAccountGuarantorGroupMetadata.ColumnNames.LastUpdateByUserID, 40, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductAccountGuarantorGroupMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ProductAccountGuarantorGroupMetadata Meta()
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
			public const string SRGuarantorIncomeGroup = "SRGuarantorIncomeGroup";
			public const string ChartOfAccountIdIncome = "ChartOfAccountIdIncome";
			public const string SubledgerIdIncome = "SubledgerIdIncome";
			public const string ChartOfAccountIdInventory = "ChartOfAccountIdInventory";
			public const string SubledgerIdInventory = "SubledgerIdInventory";
			public const string ChartOfAccountIdCOGS = "ChartOfAccountIdCOGS";
			public const string SubledgerIdCOGS = "SubledgerIdCOGS";
			public const string ChartOfAccountIdAcrual = "ChartOfAccountIdAcrual";
			public const string SubledgerIdAcrual = "SubledgerIdAcrual";
			public const string ChartOfAccountIdDiscount = "ChartOfAccountIdDiscount";
			public const string SubledgerIdDiscount = "SubledgerIdDiscount";
			public const string ChartOfAccountIdIncomeIP = "ChartOfAccountIdIncomeIP";
			public const string SubledgerIdIncomeIP = "SubledgerIdIncomeIP";
			public const string ChartOfAccountIdInventoryIP = "ChartOfAccountIdInventoryIP";
			public const string SubledgerIdInventoryIP = "SubledgerIdInventoryIP";
			public const string ChartOfAccountIdCOGSIP = "ChartOfAccountIdCOGSIP";
			public const string SubledgerIdCOGSIP = "SubledgerIdCOGSIP";
			public const string ChartOfAccountIdAcrualIP = "ChartOfAccountIdAcrualIP";
			public const string SubledgerIdAcrualIP = "SubledgerIdAcrualIP";
			public const string ChartOfAccountIdDiscountIP = "ChartOfAccountIdDiscountIP";
			public const string SubledgerIdDiscountIP = "SubledgerIdDiscountIP";
			public const string ChartOfAccountIdIncomeIGD = "ChartOfAccountIdIncomeIGD";
			public const string SubledgerIdIncomeIGD = "SubledgerIdIncomeIGD";
			public const string ChartOfAccountIdInventoryIGD = "ChartOfAccountIdInventoryIGD";
			public const string SubledgerIdInventoryIGD = "SubledgerIdInventoryIGD";
			public const string ChartOfAccountIdCOGSIGD = "ChartOfAccountIdCOGSIGD";
			public const string SubledgerIdCOGSIGD = "SubledgerIdCOGSIGD";
			public const string ChartOfAccountIdAcrualIGD = "ChartOfAccountIdAcrualIGD";
			public const string SubledgerIdAcrualIGD = "SubledgerIdAcrualIGD";
			public const string ChartOfAccountIdDiscountIGD = "ChartOfAccountIdDiscountIGD";
			public const string SubledgerIdDiscountIGD = "SubledgerIdDiscountIGD";
			public const string SRItemType = "SRItemType";
			public const string ChartOfAccountIdCOGSOPTemp = "ChartOfAccountIdCOGSOPTemp";
			public const string SubledgerIdCOGSOPTemp = "SubledgerIdCOGSOPTemp";
			public const string ChartOfAccountIdCOGSIPTemp = "ChartOfAccountIdCOGSIPTemp";
			public const string SubledgerIdCOGSIPTemp = "SubledgerIdCOGSIPTemp";
			public const string ChartOfAccountIdCOGSIGDTemp = "ChartOfAccountIdCOGSIGDTemp";
			public const string SubledgerIdCOGSIGDTemp = "SubledgerIdCOGSIGDTemp";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ProductAccountID = "ProductAccountID";
			public const string SRGuarantorIncomeGroup = "SRGuarantorIncomeGroup";
			public const string ChartOfAccountIdIncome = "ChartOfAccountIdIncome";
			public const string SubledgerIdIncome = "SubledgerIdIncome";
			public const string ChartOfAccountIdInventory = "ChartOfAccountIdInventory";
			public const string SubledgerIdInventory = "SubledgerIdInventory";
			public const string ChartOfAccountIdCOGS = "ChartOfAccountIdCOGS";
			public const string SubledgerIdCOGS = "SubledgerIdCOGS";
			public const string ChartOfAccountIdAcrual = "ChartOfAccountIdAcrual";
			public const string SubledgerIdAcrual = "SubledgerIdAcrual";
			public const string ChartOfAccountIdDiscount = "ChartOfAccountIdDiscount";
			public const string SubledgerIdDiscount = "SubledgerIdDiscount";
			public const string ChartOfAccountIdIncomeIP = "ChartOfAccountIdIncomeIP";
			public const string SubledgerIdIncomeIP = "SubledgerIdIncomeIP";
			public const string ChartOfAccountIdInventoryIP = "ChartOfAccountIdInventoryIP";
			public const string SubledgerIdInventoryIP = "SubledgerIdInventoryIP";
			public const string ChartOfAccountIdCOGSIP = "ChartOfAccountIdCOGSIP";
			public const string SubledgerIdCOGSIP = "SubledgerIdCOGSIP";
			public const string ChartOfAccountIdAcrualIP = "ChartOfAccountIdAcrualIP";
			public const string SubledgerIdAcrualIP = "SubledgerIdAcrualIP";
			public const string ChartOfAccountIdDiscountIP = "ChartOfAccountIdDiscountIP";
			public const string SubledgerIdDiscountIP = "SubledgerIdDiscountIP";
			public const string ChartOfAccountIdIncomeIGD = "ChartOfAccountIdIncomeIGD";
			public const string SubledgerIdIncomeIGD = "SubledgerIdIncomeIGD";
			public const string ChartOfAccountIdInventoryIGD = "ChartOfAccountIdInventoryIGD";
			public const string SubledgerIdInventoryIGD = "SubledgerIdInventoryIGD";
			public const string ChartOfAccountIdCOGSIGD = "ChartOfAccountIdCOGSIGD";
			public const string SubledgerIdCOGSIGD = "SubledgerIdCOGSIGD";
			public const string ChartOfAccountIdAcrualIGD = "ChartOfAccountIdAcrualIGD";
			public const string SubledgerIdAcrualIGD = "SubledgerIdAcrualIGD";
			public const string ChartOfAccountIdDiscountIGD = "ChartOfAccountIdDiscountIGD";
			public const string SubledgerIdDiscountIGD = "SubledgerIdDiscountIGD";
			public const string SRItemType = "SRItemType";
			public const string ChartOfAccountIdCOGSOPTemp = "ChartOfAccountIdCOGSOPTemp";
			public const string SubledgerIdCOGSOPTemp = "SubledgerIdCOGSOPTemp";
			public const string ChartOfAccountIdCOGSIPTemp = "ChartOfAccountIdCOGSIPTemp";
			public const string SubledgerIdCOGSIPTemp = "SubledgerIdCOGSIPTemp";
			public const string ChartOfAccountIdCOGSIGDTemp = "ChartOfAccountIdCOGSIGDTemp";
			public const string SubledgerIdCOGSIGDTemp = "SubledgerIdCOGSIGDTemp";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
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
			lock (typeof(ProductAccountGuarantorGroupMetadata))
			{
				if(ProductAccountGuarantorGroupMetadata.mapDelegates == null)
				{
					ProductAccountGuarantorGroupMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ProductAccountGuarantorGroupMetadata.meta == null)
				{
					ProductAccountGuarantorGroupMetadata.meta = new ProductAccountGuarantorGroupMetadata();
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
				meta.AddTypeMap("SRGuarantorIncomeGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountIdIncome", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdIncome", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdInventory", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdInventory", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdCOGS", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCOGS", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdAcrual", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdAcrual", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdDiscount", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdDiscount", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdIncomeIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdIncomeIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdInventoryIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdInventoryIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdCOGSIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCOGSIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdAcrualIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdAcrualIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdDiscountIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdDiscountIP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdIncomeIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdIncomeIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdInventoryIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdInventoryIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdCOGSIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCOGSIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdAcrualIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdAcrualIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdDiscountIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdDiscountIGD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRItemType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountIdCOGSOPTemp", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCOGSOPTemp", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdCOGSIPTemp", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCOGSIPTemp", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdCOGSIGDTemp", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCOGSIGDTemp", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "ProductAccountGuarantorGroup";
				meta.Destination = "ProductAccountGuarantorGroup";
				meta.spInsert = "proc_ProductAccountGuarantorGroupInsert";				
				meta.spUpdate = "proc_ProductAccountGuarantorGroupUpdate";		
				meta.spDelete = "proc_ProductAccountGuarantorGroupDelete";
				meta.spLoadAll = "proc_ProductAccountGuarantorGroupLoadAll";
				meta.spLoadByPrimaryKey = "proc_ProductAccountGuarantorGroupLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ProductAccountGuarantorGroupMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
