/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/27/2019 10:40:32 AM
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
	abstract public class esServiceUnitProductAccountMappingV2Collection : esEntityCollectionWAuditLog
	{
		public esServiceUnitProductAccountMappingV2Collection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ServiceUnitProductAccountMappingV2Collection";
		}		
		
		#region Query Logic
		protected void InitQuery(esServiceUnitProductAccountMappingV2Query query)
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
			this.InitQuery(query as esServiceUnitProductAccountMappingV2Query);
		}
		#endregion
			
		virtual public ServiceUnitProductAccountMappingV2 DetachEntity(ServiceUnitProductAccountMappingV2 entity)
		{
			return base.DetachEntity(entity) as ServiceUnitProductAccountMappingV2;
		}
		
		virtual public ServiceUnitProductAccountMappingV2 AttachEntity(ServiceUnitProductAccountMappingV2 entity)
		{
			return base.AttachEntity(entity) as ServiceUnitProductAccountMappingV2;
		}
		
		virtual public void Combine(ServiceUnitProductAccountMappingV2Collection collection)
		{
			base.Combine(collection);
		}
		
		new public ServiceUnitProductAccountMappingV2 this[int index]
		{
			get
			{
				return base[index] as ServiceUnitProductAccountMappingV2;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceUnitProductAccountMappingV2);
		}
	}

	[Serializable]
	abstract public class esServiceUnitProductAccountMappingV2 : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceUnitProductAccountMappingV2Query GetDynamicQuery()
		{
			return null;
		}
		
		public esServiceUnitProductAccountMappingV2()
		{
		}
	
		public esServiceUnitProductAccountMappingV2(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String serviceUnitId, String productAccountId, String sRRegistrationType)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitId, productAccountId, sRRegistrationType);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitId, productAccountId, sRRegistrationType);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String serviceUnitId, String productAccountId, String sRRegistrationType)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitId, productAccountId, sRRegistrationType);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitId, productAccountId, sRRegistrationType);
		}
	
		private bool LoadByPrimaryKeyDynamic(String serviceUnitId, String productAccountId, String sRRegistrationType)
		{
			esServiceUnitProductAccountMappingV2Query query = this.GetDynamicQuery();
			query.Where(query.ServiceUnitId==serviceUnitId, query.ProductAccountId==productAccountId, query.SRRegistrationType==sRRegistrationType);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String serviceUnitId, String productAccountId, String sRRegistrationType)
		{
			esParameters parms = new esParameters();
			parms.Add("ServiceUnitId",serviceUnitId);
			parms.Add("ProductAccountId",productAccountId);
			parms.Add("SRRegistrationType",sRRegistrationType);
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
						case "ServiceUnitId": this.str.ServiceUnitId = (string)value; break;
						case "ProductAccountId": this.str.ProductAccountId = (string)value; break;
						case "SRRegistrationType": this.str.SRRegistrationType = (string)value; break;
						case "ChartOfAccountIdIncome": this.str.ChartOfAccountIdIncome = (string)value; break;
						case "SubledgerIdIncome": this.str.SubledgerIdIncome = (string)value; break;
						case "ChartOfAccountIdAccrual": this.str.ChartOfAccountIdAccrual = (string)value; break;
						case "SubledgerIdAccrual": this.str.SubledgerIdAccrual = (string)value; break;
						case "ChartOfAccountIdDiscount": this.str.ChartOfAccountIdDiscount = (string)value; break;
						case "SubledgerIdDiscount": this.str.SubledgerIdDiscount = (string)value; break;
						case "ChartOfAccountIdInventory": this.str.ChartOfAccountIdInventory = (string)value; break;
						case "SubledgerIdInventory": this.str.SubledgerIdInventory = (string)value; break;
						case "ChartOfAccountIdCOGS": this.str.ChartOfAccountIdCOGS = (string)value; break;
						case "SubledgerIdCOGS": this.str.SubledgerIdCOGS = (string)value; break;
						case "ChartOfAccountIdExpense": this.str.ChartOfAccountIdExpense = (string)value; break;
						case "SubledgerIdExpense": this.str.SubledgerIdExpense = (string)value; break;
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
						case "ChartOfAccountIdAccrual":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdAccrual = (System.Int32?)value;
							break;
						case "SubledgerIdAccrual":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdAccrual = (System.Int32?)value;
							break;
						case "ChartOfAccountIdDiscount":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdDiscount = (System.Int32?)value;
							break;
						case "SubledgerIdDiscount":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdDiscount = (System.Int32?)value;
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
						case "ChartOfAccountIdExpense":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdExpense = (System.Int32?)value;
							break;
						case "SubledgerIdExpense":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdExpense = (System.Int32?)value;
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
		/// Maps to ServiceUnitProductAccountMappingV2.ServiceUnitId
		/// </summary>
		virtual public System.String ServiceUnitId
		{
			get
			{
				return base.GetSystemString(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ServiceUnitId);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ServiceUnitId, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitProductAccountMappingV2.ProductAccountId
		/// </summary>
		virtual public System.String ProductAccountId
		{
			get
			{
				return base.GetSystemString(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ProductAccountId);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ProductAccountId, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitProductAccountMappingV2.SRRegistrationType
		/// </summary>
		virtual public System.String SRRegistrationType
		{
			get
			{
				return base.GetSystemString(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SRRegistrationType);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SRRegistrationType, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitProductAccountMappingV2.ChartOfAccountIdIncome
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdIncome
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdIncome);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdIncome, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitProductAccountMappingV2.SubledgerIdIncome
		/// </summary>
		virtual public System.Int32? SubledgerIdIncome
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdIncome);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdIncome, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitProductAccountMappingV2.ChartOfAccountIdAccrual
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdAccrual
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdAccrual);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdAccrual, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitProductAccountMappingV2.SubledgerIdAccrual
		/// </summary>
		virtual public System.Int32? SubledgerIdAccrual
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdAccrual);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdAccrual, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitProductAccountMappingV2.ChartOfAccountIdDiscount
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdDiscount
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdDiscount);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdDiscount, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitProductAccountMappingV2.SubledgerIdDiscount
		/// </summary>
		virtual public System.Int32? SubledgerIdDiscount
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdDiscount);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdDiscount, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitProductAccountMappingV2.ChartOfAccountIdInventory
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdInventory
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdInventory);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdInventory, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitProductAccountMappingV2.SubledgerIdInventory
		/// </summary>
		virtual public System.Int32? SubledgerIdInventory
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdInventory);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdInventory, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitProductAccountMappingV2.ChartOfAccountIdCOGS
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCOGS
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdCOGS);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdCOGS, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitProductAccountMappingV2.SubledgerIdCOGS
		/// </summary>
		virtual public System.Int32? SubledgerIdCOGS
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdCOGS);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdCOGS, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitProductAccountMappingV2.ChartOfAccountIdExpense
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdExpense
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdExpense);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdExpense, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitProductAccountMappingV2.SubledgerIdExpense
		/// </summary>
		virtual public System.Int32? SubledgerIdExpense
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdExpense);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdExpense, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitProductAccountMappingV2.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitProductAccountMappingV2.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esServiceUnitProductAccountMappingV2 entity)
			{
				this.entity = entity;
			}
			public System.String ServiceUnitId
			{
				get
				{
					System.String data = entity.ServiceUnitId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitId = null;
					else entity.ServiceUnitId = Convert.ToString(value);
				}
			}
			public System.String ProductAccountId
			{
				get
				{
					System.String data = entity.ProductAccountId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProductAccountId = null;
					else entity.ProductAccountId = Convert.ToString(value);
				}
			}
			public System.String SRRegistrationType
			{
				get
				{
					System.String data = entity.SRRegistrationType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRegistrationType = null;
					else entity.SRRegistrationType = Convert.ToString(value);
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
			public System.String ChartOfAccountIdAccrual
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdAccrual;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdAccrual = null;
					else entity.ChartOfAccountIdAccrual = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdAccrual
			{
				get
				{
					System.Int32? data = entity.SubledgerIdAccrual;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdAccrual = null;
					else entity.SubledgerIdAccrual = Convert.ToInt32(value);
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
			public System.String ChartOfAccountIdExpense
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdExpense;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdExpense = null;
					else entity.ChartOfAccountIdExpense = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdExpense
			{
				get
				{
					System.Int32? data = entity.SubledgerIdExpense;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdExpense = null;
					else entity.SubledgerIdExpense = Convert.ToInt32(value);
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
			private esServiceUnitProductAccountMappingV2 entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceUnitProductAccountMappingV2Query query)
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
				throw new Exception("esServiceUnitProductAccountMappingV2 can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ServiceUnitProductAccountMappingV2 : esServiceUnitProductAccountMappingV2
	{	
	}

	[Serializable]
	abstract public class esServiceUnitProductAccountMappingV2Query : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitProductAccountMappingV2Metadata.Meta();
			}
		}	
			
		public esQueryItem ServiceUnitId
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ServiceUnitId, esSystemType.String);
			}
		} 
			
		public esQueryItem ProductAccountId
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ProductAccountId, esSystemType.String);
			}
		} 
			
		public esQueryItem SRRegistrationType
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SRRegistrationType, esSystemType.String);
			}
		} 
			
		public esQueryItem ChartOfAccountIdIncome
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdIncome, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdIncome
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdIncome, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdAccrual
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdAccrual, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdAccrual
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdAccrual, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdDiscount
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdDiscount, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdDiscount
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdDiscount, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdInventory
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdInventory, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdInventory
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdInventory, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdCOGS
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdCOGS, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdCOGS
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdCOGS, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdExpense
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdExpense, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdExpense
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdExpense, esSystemType.Int32);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingV2Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingV2Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceUnitProductAccountMappingV2Collection")]
	public partial class ServiceUnitProductAccountMappingV2Collection : esServiceUnitProductAccountMappingV2Collection, IEnumerable< ServiceUnitProductAccountMappingV2>
	{
		public ServiceUnitProductAccountMappingV2Collection()
		{

		}	
		
		public static implicit operator List< ServiceUnitProductAccountMappingV2>(ServiceUnitProductAccountMappingV2Collection coll)
		{
			List< ServiceUnitProductAccountMappingV2> list = new List< ServiceUnitProductAccountMappingV2>();
			
			foreach (ServiceUnitProductAccountMappingV2 emp in coll)
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
				return  ServiceUnitProductAccountMappingV2Metadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitProductAccountMappingV2Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceUnitProductAccountMappingV2(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceUnitProductAccountMappingV2();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ServiceUnitProductAccountMappingV2Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitProductAccountMappingV2Query();
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
		public bool Load(ServiceUnitProductAccountMappingV2Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ServiceUnitProductAccountMappingV2 AddNew()
		{
			ServiceUnitProductAccountMappingV2 entity = base.AddNewEntity() as ServiceUnitProductAccountMappingV2;
			
			return entity;		
		}
		public ServiceUnitProductAccountMappingV2 FindByPrimaryKey(String serviceUnitId, String productAccountId, String sRRegistrationType)
		{
			return base.FindByPrimaryKey(serviceUnitId, productAccountId, sRRegistrationType) as ServiceUnitProductAccountMappingV2;
		}

		#region IEnumerable< ServiceUnitProductAccountMappingV2> Members

		IEnumerator< ServiceUnitProductAccountMappingV2> IEnumerable< ServiceUnitProductAccountMappingV2>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ServiceUnitProductAccountMappingV2;
			}
		}

		#endregion
		
		private ServiceUnitProductAccountMappingV2Query query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceUnitProductAccountMappingV2' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ServiceUnitProductAccountMappingV2 ({ServiceUnitId, ProductAccountId, SRRegistrationType})")]
	[Serializable]
	public partial class ServiceUnitProductAccountMappingV2 : esServiceUnitProductAccountMappingV2
	{
		public ServiceUnitProductAccountMappingV2()
		{
		}	
	
		public ServiceUnitProductAccountMappingV2(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitProductAccountMappingV2Metadata.Meta();
			}
		}	
	
		override protected esServiceUnitProductAccountMappingV2Query GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitProductAccountMappingV2Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ServiceUnitProductAccountMappingV2Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitProductAccountMappingV2Query();
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
		public bool Load(ServiceUnitProductAccountMappingV2Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ServiceUnitProductAccountMappingV2Query query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ServiceUnitProductAccountMappingV2Query : esServiceUnitProductAccountMappingV2Query
	{
		public ServiceUnitProductAccountMappingV2Query()
		{

		}		
		
		public ServiceUnitProductAccountMappingV2Query(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ServiceUnitProductAccountMappingV2Query";
        }
	}

	[Serializable]
	public partial class ServiceUnitProductAccountMappingV2Metadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceUnitProductAccountMappingV2Metadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ServiceUnitId, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitProductAccountMappingV2Metadata.PropertyNames.ServiceUnitId;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ProductAccountId, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitProductAccountMappingV2Metadata.PropertyNames.ProductAccountId;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SRRegistrationType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitProductAccountMappingV2Metadata.PropertyNames.SRRegistrationType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdIncome, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingV2Metadata.PropertyNames.ChartOfAccountIdIncome;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdIncome, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingV2Metadata.PropertyNames.SubledgerIdIncome;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdAccrual, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingV2Metadata.PropertyNames.ChartOfAccountIdAccrual;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdAccrual, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingV2Metadata.PropertyNames.SubledgerIdAccrual;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdDiscount, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingV2Metadata.PropertyNames.ChartOfAccountIdDiscount;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdDiscount, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingV2Metadata.PropertyNames.SubledgerIdDiscount;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdInventory, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingV2Metadata.PropertyNames.ChartOfAccountIdInventory;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdInventory, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingV2Metadata.PropertyNames.SubledgerIdInventory;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdCOGS, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingV2Metadata.PropertyNames.ChartOfAccountIdCOGS;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdCOGS, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingV2Metadata.PropertyNames.SubledgerIdCOGS;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.ChartOfAccountIdExpense, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingV2Metadata.PropertyNames.ChartOfAccountIdExpense;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.SubledgerIdExpense, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingV2Metadata.PropertyNames.SubledgerIdExpense;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.LastUpdateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitProductAccountMappingV2Metadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingV2Metadata.ColumnNames.LastUpdateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitProductAccountMappingV2Metadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ServiceUnitProductAccountMappingV2Metadata Meta()
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
			public const string ServiceUnitId = "ServiceUnitId";
			public const string ProductAccountId = "ProductAccountId";
			public const string SRRegistrationType = "SRRegistrationType";
			public const string ChartOfAccountIdIncome = "ChartOfAccountIdIncome";
			public const string SubledgerIdIncome = "SubledgerIdIncome";
			public const string ChartOfAccountIdAccrual = "ChartOfAccountIdAccrual";
			public const string SubledgerIdAccrual = "SubledgerIdAccrual";
			public const string ChartOfAccountIdDiscount = "ChartOfAccountIdDiscount";
			public const string SubledgerIdDiscount = "SubledgerIdDiscount";
			public const string ChartOfAccountIdInventory = "ChartOfAccountIdInventory";
			public const string SubledgerIdInventory = "SubledgerIdInventory";
			public const string ChartOfAccountIdCOGS = "ChartOfAccountIdCOGS";
			public const string SubledgerIdCOGS = "SubledgerIdCOGS";
			public const string ChartOfAccountIdExpense = "ChartOfAccountIdExpense";
			public const string SubledgerIdExpense = "SubledgerIdExpense";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ServiceUnitId = "ServiceUnitId";
			public const string ProductAccountId = "ProductAccountId";
			public const string SRRegistrationType = "SRRegistrationType";
			public const string ChartOfAccountIdIncome = "ChartOfAccountIdIncome";
			public const string SubledgerIdIncome = "SubledgerIdIncome";
			public const string ChartOfAccountIdAccrual = "ChartOfAccountIdAccrual";
			public const string SubledgerIdAccrual = "SubledgerIdAccrual";
			public const string ChartOfAccountIdDiscount = "ChartOfAccountIdDiscount";
			public const string SubledgerIdDiscount = "SubledgerIdDiscount";
			public const string ChartOfAccountIdInventory = "ChartOfAccountIdInventory";
			public const string SubledgerIdInventory = "SubledgerIdInventory";
			public const string ChartOfAccountIdCOGS = "ChartOfAccountIdCOGS";
			public const string SubledgerIdCOGS = "SubledgerIdCOGS";
			public const string ChartOfAccountIdExpense = "ChartOfAccountIdExpense";
			public const string SubledgerIdExpense = "SubledgerIdExpense";
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
			lock (typeof(ServiceUnitProductAccountMappingV2Metadata))
			{
				if(ServiceUnitProductAccountMappingV2Metadata.mapDelegates == null)
				{
					ServiceUnitProductAccountMappingV2Metadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ServiceUnitProductAccountMappingV2Metadata.meta == null)
				{
					ServiceUnitProductAccountMappingV2Metadata.meta = new ServiceUnitProductAccountMappingV2Metadata();
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
				
				meta.AddTypeMap("ServiceUnitId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProductAccountId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRegistrationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountIdIncome", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdIncome", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdAccrual", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdAccrual", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdDiscount", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdDiscount", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdInventory", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdInventory", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdCOGS", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCOGS", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdExpense", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdExpense", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "ServiceUnitProductAccountMappingV2";
				meta.Destination = "ServiceUnitProductAccountMappingV2";
				meta.spInsert = "proc_ServiceUnitProductAccountMappingV2Insert";				
				meta.spUpdate = "proc_ServiceUnitProductAccountMappingV2Update";		
				meta.spDelete = "proc_ServiceUnitProductAccountMappingV2Delete";
				meta.spLoadAll = "proc_ServiceUnitProductAccountMappingV2LoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceUnitProductAccountMappingV2LoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceUnitProductAccountMappingV2Metadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
