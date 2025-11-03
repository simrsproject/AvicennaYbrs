/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 10/21/2013 10:43:49 AM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

	[Serializable]
	abstract public class esServiceUnitProductAccountMappingCollection : esEntityCollectionWAuditLog
	{
		public esServiceUnitProductAccountMappingCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ServiceUnitProductAccountMappingCollection";
		}

		#region Query Logic
		protected void InitQuery(esServiceUnitProductAccountMappingQuery query)
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
			this.InitQuery(query as esServiceUnitProductAccountMappingQuery);
		}
		#endregion
		
		virtual public ServiceUnitProductAccountMapping DetachEntity(ServiceUnitProductAccountMapping entity)
		{
			return base.DetachEntity(entity) as ServiceUnitProductAccountMapping;
		}
		
		virtual public ServiceUnitProductAccountMapping AttachEntity(ServiceUnitProductAccountMapping entity)
		{
			return base.AttachEntity(entity) as ServiceUnitProductAccountMapping;
		}
		
		virtual public void Combine(ServiceUnitProductAccountMappingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ServiceUnitProductAccountMapping this[int index]
		{
			get
			{
				return base[index] as ServiceUnitProductAccountMapping;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceUnitProductAccountMapping);
		}
	}



	[Serializable]
	abstract public class esServiceUnitProductAccountMapping : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceUnitProductAccountMappingQuery GetDynamicQuery()
		{
			return null;
		}

		public esServiceUnitProductAccountMapping()
		{

		}

		public esServiceUnitProductAccountMapping(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String locationId, System.String serviceUnitId, System.String productAccountId, System.String sRRegistrationType)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(locationId, serviceUnitId, productAccountId, sRRegistrationType);
			else
				return LoadByPrimaryKeyStoredProcedure(locationId, serviceUnitId, productAccountId, sRRegistrationType);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String locationId, System.String serviceUnitId, System.String productAccountId, System.String sRRegistrationType)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(locationId, serviceUnitId, productAccountId, sRRegistrationType);
			else
				return LoadByPrimaryKeyStoredProcedure(locationId, serviceUnitId, productAccountId, sRRegistrationType);
		}

		private bool LoadByPrimaryKeyDynamic(System.String locationId, System.String serviceUnitId, System.String productAccountId, System.String sRRegistrationType)
		{
			esServiceUnitProductAccountMappingQuery query = this.GetDynamicQuery();
			query.Where(query.LocationId == locationId, query.ServiceUnitId == serviceUnitId, query.ProductAccountId == productAccountId, query.SRRegistrationType == sRRegistrationType);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String locationId, System.String serviceUnitId, System.String productAccountId, System.String sRRegistrationType)
		{
			esParameters parms = new esParameters();
			parms.Add("LocationId",locationId);			parms.Add("ServiceUnitId",serviceUnitId);			parms.Add("ProductAccountId",productAccountId);			parms.Add("SRRegistrationType",sRRegistrationType);
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
						case "LocationId": this.str.LocationId = (string)value; break;							
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
		/// Maps to ServiceUnitProductAccountMapping.LocationId
		/// </summary>
		virtual public System.String LocationId
		{
			get
			{
				return base.GetSystemString(ServiceUnitProductAccountMappingMetadata.ColumnNames.LocationId);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitProductAccountMappingMetadata.ColumnNames.LocationId, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitProductAccountMapping.ServiceUnitId
		/// </summary>
		virtual public System.String ServiceUnitId
		{
			get
			{
				return base.GetSystemString(ServiceUnitProductAccountMappingMetadata.ColumnNames.ServiceUnitId);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitProductAccountMappingMetadata.ColumnNames.ServiceUnitId, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitProductAccountMapping.ProductAccountId
		/// </summary>
		virtual public System.String ProductAccountId
		{
			get
			{
				return base.GetSystemString(ServiceUnitProductAccountMappingMetadata.ColumnNames.ProductAccountId);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitProductAccountMappingMetadata.ColumnNames.ProductAccountId, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitProductAccountMapping.SRRegistrationType
		/// </summary>
		virtual public System.String SRRegistrationType
		{
			get
			{
				return base.GetSystemString(ServiceUnitProductAccountMappingMetadata.ColumnNames.SRRegistrationType);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitProductAccountMappingMetadata.ColumnNames.SRRegistrationType, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitProductAccountMapping.ChartOfAccountIdIncome
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdIncome
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdIncome);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdIncome, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitProductAccountMapping.SubledgerIdIncome
		/// </summary>
		virtual public System.Int32? SubledgerIdIncome
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdIncome);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdIncome, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitProductAccountMapping.ChartOfAccountIdAccrual
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdAccrual
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdAccrual);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdAccrual, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitProductAccountMapping.SubledgerIdAccrual
		/// </summary>
		virtual public System.Int32? SubledgerIdAccrual
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdAccrual);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdAccrual, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitProductAccountMapping.ChartOfAccountIdDiscount
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdDiscount
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdDiscount);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdDiscount, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitProductAccountMapping.SubledgerIdDiscount
		/// </summary>
		virtual public System.Int32? SubledgerIdDiscount
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdDiscount);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdDiscount, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitProductAccountMapping.ChartOfAccountIdInventory
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdInventory
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdInventory);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdInventory, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitProductAccountMapping.SubledgerIdInventory
		/// </summary>
		virtual public System.Int32? SubledgerIdInventory
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdInventory);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdInventory, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitProductAccountMapping.ChartOfAccountIdCOGS
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCOGS
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdCOGS);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdCOGS, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitProductAccountMapping.SubledgerIdCOGS
		/// </summary>
		virtual public System.Int32? SubledgerIdCOGS
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdCOGS);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdCOGS, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitProductAccountMapping.ChartOfAccountIdExpense
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdExpense
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdExpense);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdExpense, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitProductAccountMapping.SubledgerIdExpense
		/// </summary>
		virtual public System.Int32? SubledgerIdExpense
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdExpense);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdExpense, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitProductAccountMapping.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitProductAccountMappingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceUnitProductAccountMappingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitProductAccountMapping.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitProductAccountMappingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitProductAccountMappingMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		#endregion	

		#region String Properties


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
			public esStrings(esServiceUnitProductAccountMapping entity)
			{
				this.entity = entity;
			}
			
	
			public System.String LocationId
			{
				get
				{
					System.String data = entity.LocationId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationId = null;
					else entity.LocationId = Convert.ToString(value);
				}
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
			

			private esServiceUnitProductAccountMapping entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceUnitProductAccountMappingQuery query)
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
				throw new Exception("esServiceUnitProductAccountMapping can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ServiceUnitProductAccountMapping : esServiceUnitProductAccountMapping
	{

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
		
			return props;
		}	
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PreSave.
		/// </summary>
		protected override void ApplyPreSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostSave.
		/// </summary>
		protected override void ApplyPostSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostOneToOneSave.
		/// </summary>
		protected override void ApplyPostOneSaveKeys()
		{
		}
		
	}



	[Serializable]
	abstract public class esServiceUnitProductAccountMappingQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitProductAccountMappingMetadata.Meta();
			}
		}	
		

		public esQueryItem LocationId
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingMetadata.ColumnNames.LocationId, esSystemType.String);
			}
		} 
		
		public esQueryItem ServiceUnitId
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingMetadata.ColumnNames.ServiceUnitId, esSystemType.String);
			}
		} 
		
		public esQueryItem ProductAccountId
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingMetadata.ColumnNames.ProductAccountId, esSystemType.String);
			}
		} 
		
		public esQueryItem SRRegistrationType
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingMetadata.ColumnNames.SRRegistrationType, esSystemType.String);
			}
		} 
		
		public esQueryItem ChartOfAccountIdIncome
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdIncome, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubledgerIdIncome
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdIncome, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ChartOfAccountIdAccrual
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdAccrual, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubledgerIdAccrual
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdAccrual, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ChartOfAccountIdDiscount
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdDiscount, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubledgerIdDiscount
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdDiscount, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ChartOfAccountIdInventory
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdInventory, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubledgerIdInventory
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdInventory, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ChartOfAccountIdCOGS
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdCOGS, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubledgerIdCOGS
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdCOGS, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ChartOfAccountIdExpense
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdExpense, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubledgerIdExpense
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdExpense, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitProductAccountMappingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceUnitProductAccountMappingCollection")]
	public partial class ServiceUnitProductAccountMappingCollection : esServiceUnitProductAccountMappingCollection, IEnumerable<ServiceUnitProductAccountMapping>
	{
		public ServiceUnitProductAccountMappingCollection()
		{

		}
		
		public static implicit operator List<ServiceUnitProductAccountMapping>(ServiceUnitProductAccountMappingCollection coll)
		{
			List<ServiceUnitProductAccountMapping> list = new List<ServiceUnitProductAccountMapping>();
			
			foreach (ServiceUnitProductAccountMapping emp in coll)
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
				return  ServiceUnitProductAccountMappingMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitProductAccountMappingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceUnitProductAccountMapping(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceUnitProductAccountMapping();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ServiceUnitProductAccountMappingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitProductAccountMappingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ServiceUnitProductAccountMappingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ServiceUnitProductAccountMapping AddNew()
		{
			ServiceUnitProductAccountMapping entity = base.AddNewEntity() as ServiceUnitProductAccountMapping;
			
			return entity;
		}

		public ServiceUnitProductAccountMapping FindByPrimaryKey(System.String locationId, System.String serviceUnitId, System.String productAccountId, System.String sRRegistrationType)
		{
			return base.FindByPrimaryKey(locationId, serviceUnitId, productAccountId, sRRegistrationType) as ServiceUnitProductAccountMapping;
		}


		#region IEnumerable<ServiceUnitProductAccountMapping> Members

		IEnumerator<ServiceUnitProductAccountMapping> IEnumerable<ServiceUnitProductAccountMapping>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ServiceUnitProductAccountMapping;
			}
		}

		#endregion
		
		private ServiceUnitProductAccountMappingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceUnitProductAccountMapping' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ServiceUnitProductAccountMapping ({LocationId},{ServiceUnitId},{ProductAccountId},{SRRegistrationType})")]
	[Serializable]
	public partial class ServiceUnitProductAccountMapping : esServiceUnitProductAccountMapping
	{
		public ServiceUnitProductAccountMapping()
		{

		}
	
		public ServiceUnitProductAccountMapping(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitProductAccountMappingMetadata.Meta();
			}
		}
		
		
		
		override protected esServiceUnitProductAccountMappingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitProductAccountMappingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ServiceUnitProductAccountMappingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitProductAccountMappingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ServiceUnitProductAccountMappingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ServiceUnitProductAccountMappingQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ServiceUnitProductAccountMappingQuery : esServiceUnitProductAccountMappingQuery
	{
		public ServiceUnitProductAccountMappingQuery()
		{

		}		
		
		public ServiceUnitProductAccountMappingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ServiceUnitProductAccountMappingQuery";
        }
		
			
	}


	[Serializable]
	public partial class ServiceUnitProductAccountMappingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceUnitProductAccountMappingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ServiceUnitProductAccountMappingMetadata.ColumnNames.LocationId, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitProductAccountMappingMetadata.PropertyNames.LocationId;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingMetadata.ColumnNames.ServiceUnitId, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitProductAccountMappingMetadata.PropertyNames.ServiceUnitId;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingMetadata.ColumnNames.ProductAccountId, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitProductAccountMappingMetadata.PropertyNames.ProductAccountId;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingMetadata.ColumnNames.SRRegistrationType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitProductAccountMappingMetadata.PropertyNames.SRRegistrationType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdIncome, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingMetadata.PropertyNames.ChartOfAccountIdIncome;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdIncome, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingMetadata.PropertyNames.SubledgerIdIncome;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdAccrual, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingMetadata.PropertyNames.ChartOfAccountIdAccrual;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdAccrual, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingMetadata.PropertyNames.SubledgerIdAccrual;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdDiscount, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingMetadata.PropertyNames.ChartOfAccountIdDiscount;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdDiscount, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingMetadata.PropertyNames.SubledgerIdDiscount;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdInventory, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingMetadata.PropertyNames.ChartOfAccountIdInventory;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdInventory, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingMetadata.PropertyNames.SubledgerIdInventory;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdCOGS, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingMetadata.PropertyNames.ChartOfAccountIdCOGS;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdCOGS, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingMetadata.PropertyNames.SubledgerIdCOGS;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingMetadata.ColumnNames.ChartOfAccountIdExpense, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingMetadata.PropertyNames.ChartOfAccountIdExpense;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingMetadata.ColumnNames.SubledgerIdExpense, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitProductAccountMappingMetadata.PropertyNames.SubledgerIdExpense;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingMetadata.ColumnNames.LastUpdateDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitProductAccountMappingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitProductAccountMappingMetadata.ColumnNames.LastUpdateByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitProductAccountMappingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ServiceUnitProductAccountMappingMetadata Meta()
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
			 public const string LocationId = "LocationId";
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
			 public const string LocationId = "LocationId";
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
			lock (typeof(ServiceUnitProductAccountMappingMetadata))
			{
				if(ServiceUnitProductAccountMappingMetadata.mapDelegates == null)
				{
					ServiceUnitProductAccountMappingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ServiceUnitProductAccountMappingMetadata.meta == null)
				{
					ServiceUnitProductAccountMappingMetadata.meta = new ServiceUnitProductAccountMappingMetadata();
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
				

				meta.AddTypeMap("LocationId", new esTypeMap("varchar", "System.String"));
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
				
				
				
				meta.Source = "ServiceUnitProductAccountMapping";
				meta.Destination = "ServiceUnitProductAccountMapping";
				
				meta.spInsert = "proc_ServiceUnitProductAccountMappingInsert";				
				meta.spUpdate = "proc_ServiceUnitProductAccountMappingUpdate";		
				meta.spDelete = "proc_ServiceUnitProductAccountMappingDelete";
				meta.spLoadAll = "proc_ServiceUnitProductAccountMappingLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceUnitProductAccountMappingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceUnitProductAccountMappingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
