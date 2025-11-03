/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:26 PM
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
	abstract public class esServiceUnitItemServiceClassCollection : esEntityCollectionWAuditLog
	{
		public esServiceUnitItemServiceClassCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ServiceUnitItemServiceClassCollection";
		}

		#region Query Logic
		protected void InitQuery(esServiceUnitItemServiceClassQuery query)
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
			this.InitQuery(query as esServiceUnitItemServiceClassQuery);
		}
		#endregion
		
		virtual public ServiceUnitItemServiceClass DetachEntity(ServiceUnitItemServiceClass entity)
		{
			return base.DetachEntity(entity) as ServiceUnitItemServiceClass;
		}
		
		virtual public ServiceUnitItemServiceClass AttachEntity(ServiceUnitItemServiceClass entity)
		{
			return base.AttachEntity(entity) as ServiceUnitItemServiceClass;
		}
		
		virtual public void Combine(ServiceUnitItemServiceClassCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ServiceUnitItemServiceClass this[int index]
		{
			get
			{
				return base[index] as ServiceUnitItemServiceClass;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceUnitItemServiceClass);
		}
	}



	[Serializable]
	abstract public class esServiceUnitItemServiceClass : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceUnitItemServiceClassQuery GetDynamicQuery()
		{
			return null;
		}

		public esServiceUnitItemServiceClass()
		{

		}

		public esServiceUnitItemServiceClass(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String serviceUnitID, System.String itemID, System.String classID, System.String tariffComponentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, itemID, classID, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, itemID, classID, tariffComponentID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String serviceUnitID, System.String itemID, System.String classID, System.String tariffComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, itemID, classID, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, itemID, classID, tariffComponentID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String serviceUnitID, System.String itemID, System.String classID, System.String tariffComponentID)
		{
			esServiceUnitItemServiceClassQuery query = this.GetDynamicQuery();
			query.Where(query.ServiceUnitID == serviceUnitID, query.ItemID == itemID, query.ClassID == classID, query.TariffComponentID == tariffComponentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String serviceUnitID, System.String itemID, System.String classID, System.String tariffComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("ServiceUnitID",serviceUnitID);			parms.Add("ItemID",itemID);			parms.Add("ClassID",classID);			parms.Add("TariffComponentID",tariffComponentID);
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
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "ClassID": this.str.ClassID = (string)value; break;							
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;							
						case "ChartOfAccountIdIncome": this.str.ChartOfAccountIdIncome = (string)value; break;							
						case "SubledgerIdIncome": this.str.SubledgerIdIncome = (string)value; break;							
						case "ChartOfAccountIdDiscount": this.str.ChartOfAccountIdDiscount = (string)value; break;							
						case "SubledgerIdDiscount": this.str.SubledgerIdDiscount = (string)value; break;							
						case "ChartOfAccountIdCost": this.str.ChartOfAccountIdCost = (string)value; break;							
						case "SubledgerIdCost": this.str.SubledgerIdCost = (string)value; break;							
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
		/// Maps to ServiceUnitItemServiceClass.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ServiceUnitItemServiceClassMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitItemServiceClassMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitItemServiceClass.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ServiceUnitItemServiceClassMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitItemServiceClassMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitItemServiceClass.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(ServiceUnitItemServiceClassMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitItemServiceClassMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitItemServiceClass.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ServiceUnitItemServiceClassMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitItemServiceClassMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitItemServiceClass.ChartOfAccountIdIncome
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdIncome
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitItemServiceClassMetadata.ColumnNames.ChartOfAccountIdIncome);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitItemServiceClassMetadata.ColumnNames.ChartOfAccountIdIncome, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitItemServiceClass.SubledgerIdIncome
		/// </summary>
		virtual public System.Int32? SubledgerIdIncome
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitItemServiceClassMetadata.ColumnNames.SubledgerIdIncome);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitItemServiceClassMetadata.ColumnNames.SubledgerIdIncome, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitItemServiceClass.ChartOfAccountIdDiscount
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdDiscount
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitItemServiceClassMetadata.ColumnNames.ChartOfAccountIdDiscount);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitItemServiceClassMetadata.ColumnNames.ChartOfAccountIdDiscount, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitItemServiceClass.SubledgerIdDiscount
		/// </summary>
		virtual public System.Int32? SubledgerIdDiscount
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitItemServiceClassMetadata.ColumnNames.SubledgerIdDiscount);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitItemServiceClassMetadata.ColumnNames.SubledgerIdDiscount, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitItemServiceClass.ChartOfAccountIdCost
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCost
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitItemServiceClassMetadata.ColumnNames.ChartOfAccountIdCost);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitItemServiceClassMetadata.ColumnNames.ChartOfAccountIdCost, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitItemServiceClass.SubledgerIdCost
		/// </summary>
		virtual public System.Int32? SubledgerIdCost
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitItemServiceClassMetadata.ColumnNames.SubledgerIdCost);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitItemServiceClassMetadata.ColumnNames.SubledgerIdCost, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitItemServiceClass.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitItemServiceClassMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceUnitItemServiceClassMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitItemServiceClass.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitItemServiceClassMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitItemServiceClassMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esServiceUnitItemServiceClass entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
				
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
				}
			}
				
			public System.String ClassID
			{
				get
				{
					System.String data = entity.ClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassID = null;
					else entity.ClassID = Convert.ToString(value);
				}
			}
				
			public System.String TariffComponentID
			{
				get
				{
					System.String data = entity.TariffComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffComponentID = null;
					else entity.TariffComponentID = Convert.ToString(value);
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
			

			private esServiceUnitItemServiceClass entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceUnitItemServiceClassQuery query)
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
				throw new Exception("esServiceUnitItemServiceClass can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ServiceUnitItemServiceClass : esServiceUnitItemServiceClass
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
	abstract public class esServiceUnitItemServiceClassQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitItemServiceClassMetadata.Meta();
			}
		}	
		

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceClassMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceClassMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceClassMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceClassMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
		
		public esQueryItem ChartOfAccountIdIncome
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceClassMetadata.ColumnNames.ChartOfAccountIdIncome, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubledgerIdIncome
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceClassMetadata.ColumnNames.SubledgerIdIncome, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ChartOfAccountIdDiscount
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceClassMetadata.ColumnNames.ChartOfAccountIdDiscount, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubledgerIdDiscount
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceClassMetadata.ColumnNames.SubledgerIdDiscount, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ChartOfAccountIdCost
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceClassMetadata.ColumnNames.ChartOfAccountIdCost, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubledgerIdCost
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceClassMetadata.ColumnNames.SubledgerIdCost, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceClassMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceClassMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceUnitItemServiceClassCollection")]
	public partial class ServiceUnitItemServiceClassCollection : esServiceUnitItemServiceClassCollection, IEnumerable<ServiceUnitItemServiceClass>
	{
		public ServiceUnitItemServiceClassCollection()
		{

		}
		
		public static implicit operator List<ServiceUnitItemServiceClass>(ServiceUnitItemServiceClassCollection coll)
		{
			List<ServiceUnitItemServiceClass> list = new List<ServiceUnitItemServiceClass>();
			
			foreach (ServiceUnitItemServiceClass emp in coll)
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
				return  ServiceUnitItemServiceClassMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitItemServiceClassQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceUnitItemServiceClass(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceUnitItemServiceClass();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ServiceUnitItemServiceClassQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitItemServiceClassQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ServiceUnitItemServiceClassQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ServiceUnitItemServiceClass AddNew()
		{
			ServiceUnitItemServiceClass entity = base.AddNewEntity() as ServiceUnitItemServiceClass;
			
			return entity;
		}

		public ServiceUnitItemServiceClass FindByPrimaryKey(System.String serviceUnitID, System.String itemID, System.String classID, System.String tariffComponentID)
		{
			return base.FindByPrimaryKey(serviceUnitID, itemID, classID, tariffComponentID) as ServiceUnitItemServiceClass;
		}


		#region IEnumerable<ServiceUnitItemServiceClass> Members

		IEnumerator<ServiceUnitItemServiceClass> IEnumerable<ServiceUnitItemServiceClass>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ServiceUnitItemServiceClass;
			}
		}

		#endregion
		
		private ServiceUnitItemServiceClassQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceUnitItemServiceClass' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ServiceUnitItemServiceClass ({ServiceUnitID},{ItemID},{ClassID},{TariffComponentID})")]
	[Serializable]
	public partial class ServiceUnitItemServiceClass : esServiceUnitItemServiceClass
	{
		public ServiceUnitItemServiceClass()
		{

		}
	
		public ServiceUnitItemServiceClass(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitItemServiceClassMetadata.Meta();
			}
		}
		
		
		
		override protected esServiceUnitItemServiceClassQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitItemServiceClassQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ServiceUnitItemServiceClassQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitItemServiceClassQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ServiceUnitItemServiceClassQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ServiceUnitItemServiceClassQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ServiceUnitItemServiceClassQuery : esServiceUnitItemServiceClassQuery
	{
		public ServiceUnitItemServiceClassQuery()
		{

		}		
		
		public ServiceUnitItemServiceClassQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ServiceUnitItemServiceClassQuery";
        }
		
			
	}


	[Serializable]
	public partial class ServiceUnitItemServiceClassMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceUnitItemServiceClassMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ServiceUnitItemServiceClassMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitItemServiceClassMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitItemServiceClassMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitItemServiceClassMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitItemServiceClassMetadata.ColumnNames.ClassID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitItemServiceClassMetadata.PropertyNames.ClassID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitItemServiceClassMetadata.ColumnNames.TariffComponentID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitItemServiceClassMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitItemServiceClassMetadata.ColumnNames.ChartOfAccountIdIncome, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitItemServiceClassMetadata.PropertyNames.ChartOfAccountIdIncome;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitItemServiceClassMetadata.ColumnNames.SubledgerIdIncome, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitItemServiceClassMetadata.PropertyNames.SubledgerIdIncome;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitItemServiceClassMetadata.ColumnNames.ChartOfAccountIdDiscount, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitItemServiceClassMetadata.PropertyNames.ChartOfAccountIdDiscount;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitItemServiceClassMetadata.ColumnNames.SubledgerIdDiscount, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitItemServiceClassMetadata.PropertyNames.SubledgerIdDiscount;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitItemServiceClassMetadata.ColumnNames.ChartOfAccountIdCost, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitItemServiceClassMetadata.PropertyNames.ChartOfAccountIdCost;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitItemServiceClassMetadata.ColumnNames.SubledgerIdCost, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitItemServiceClassMetadata.PropertyNames.SubledgerIdCost;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitItemServiceClassMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitItemServiceClassMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitItemServiceClassMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitItemServiceClassMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ServiceUnitItemServiceClassMetadata Meta()
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
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ItemID = "ItemID";
			 public const string ClassID = "ClassID";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string ChartOfAccountIdIncome = "ChartOfAccountIdIncome";
			 public const string SubledgerIdIncome = "SubledgerIdIncome";
			 public const string ChartOfAccountIdDiscount = "ChartOfAccountIdDiscount";
			 public const string SubledgerIdDiscount = "SubledgerIdDiscount";
			 public const string ChartOfAccountIdCost = "ChartOfAccountIdCost";
			 public const string SubledgerIdCost = "SubledgerIdCost";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ItemID = "ItemID";
			 public const string ClassID = "ClassID";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string ChartOfAccountIdIncome = "ChartOfAccountIdIncome";
			 public const string SubledgerIdIncome = "SubledgerIdIncome";
			 public const string ChartOfAccountIdDiscount = "ChartOfAccountIdDiscount";
			 public const string SubledgerIdDiscount = "SubledgerIdDiscount";
			 public const string ChartOfAccountIdCost = "ChartOfAccountIdCost";
			 public const string SubledgerIdCost = "SubledgerIdCost";
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
			lock (typeof(ServiceUnitItemServiceClassMetadata))
			{
				if(ServiceUnitItemServiceClassMetadata.mapDelegates == null)
				{
					ServiceUnitItemServiceClassMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ServiceUnitItemServiceClassMetadata.meta == null)
				{
					ServiceUnitItemServiceClassMetadata.meta = new ServiceUnitItemServiceClassMetadata();
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
				

				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountIdIncome", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdIncome", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdDiscount", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdDiscount", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdCost", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCost", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ServiceUnitItemServiceClass";
				meta.Destination = "ServiceUnitItemServiceClass";
				
				meta.spInsert = "proc_ServiceUnitItemServiceClassInsert";				
				meta.spUpdate = "proc_ServiceUnitItemServiceClassUpdate";		
				meta.spDelete = "proc_ServiceUnitItemServiceClassDelete";
				meta.spLoadAll = "proc_ServiceUnitItemServiceClassLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceUnitItemServiceClassLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceUnitItemServiceClassMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
