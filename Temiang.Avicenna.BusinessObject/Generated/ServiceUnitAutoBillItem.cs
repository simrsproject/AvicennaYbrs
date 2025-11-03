/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/29/2020 10:48:55 AM
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
	abstract public class esServiceUnitAutoBillItemCollection : esEntityCollectionWAuditLog
	{
		public esServiceUnitAutoBillItemCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ServiceUnitAutoBillItemCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esServiceUnitAutoBillItemQuery query)
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
			this.InitQuery(query as esServiceUnitAutoBillItemQuery);
		}
		#endregion
			
		virtual public ServiceUnitAutoBillItem DetachEntity(ServiceUnitAutoBillItem entity)
		{
			return base.DetachEntity(entity) as ServiceUnitAutoBillItem;
		}
		
		virtual public ServiceUnitAutoBillItem AttachEntity(ServiceUnitAutoBillItem entity)
		{
			return base.AttachEntity(entity) as ServiceUnitAutoBillItem;
		}
		
		virtual public void Combine(ServiceUnitAutoBillItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ServiceUnitAutoBillItem this[int index]
		{
			get
			{
				return base[index] as ServiceUnitAutoBillItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceUnitAutoBillItem);
		}
	}

	[Serializable]
	abstract public class esServiceUnitAutoBillItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceUnitAutoBillItemQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esServiceUnitAutoBillItem()
		{
		}
	
		public esServiceUnitAutoBillItem(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String serviceUnitID, String itemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, itemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String serviceUnitID, String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, itemID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String serviceUnitID, String itemID)
		{
			esServiceUnitAutoBillItemQuery query = this.GetDynamicQuery();
			query.Where(query.ServiceUnitID == serviceUnitID, query.ItemID == itemID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String serviceUnitID, String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("ServiceUnitID",serviceUnitID);
			parms.Add("ItemID",itemID);
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
						case "Quantity": this.str.Quantity = (string)value; break;
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
						case "IsAutoPayment": this.str.IsAutoPayment = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "IsGenerateOnRegistration": this.str.IsGenerateOnRegistration = (string)value; break;
						case "IsGenerateOnNewRegistration": this.str.IsGenerateOnNewRegistration = (string)value; break;
						case "IsGenerateOnReferral": this.str.IsGenerateOnReferral = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsGenerateOnFirstRegistration": this.str.IsGenerateOnFirstRegistration = (string)value; break;
						case "IsGenerateOnSchedule": this.str.IsGenerateOnSchedule = (string)value; break;
						case "GenerateOnClassIDs": this.str.GenerateOnClassIDs = (string)value; break;
						case "GenerateOnDayStart": this.str.GenerateOnDayStart = (string)value; break;
						case "GenerateOnDayEnd": this.str.GenerateOnDayEnd = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Quantity":
						
							if (value == null || value is System.Decimal)
								this.Quantity = (System.Decimal?)value;
							break;
						case "IsAutoPayment":
						
							if (value == null || value is System.Boolean)
								this.IsAutoPayment = (System.Boolean?)value;
							break;
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "IsGenerateOnRegistration":
						
							if (value == null || value is System.Boolean)
								this.IsGenerateOnRegistration = (System.Boolean?)value;
							break;
						case "IsGenerateOnNewRegistration":
						
							if (value == null || value is System.Boolean)
								this.IsGenerateOnNewRegistration = (System.Boolean?)value;
							break;
						case "IsGenerateOnReferral":
						
							if (value == null || value is System.Boolean)
								this.IsGenerateOnReferral = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsGenerateOnFirstRegistration":
						
							if (value == null || value is System.Boolean)
								this.IsGenerateOnFirstRegistration = (System.Boolean?)value;
							break;
						case "IsGenerateOnSchedule":
						
							if (value == null || value is System.Boolean)
								this.IsGenerateOnSchedule = (System.Boolean?)value;
							break;
						case "GenerateOnDayStart":
						
							if (value == null || value is System.Int32)
								this.GenerateOnDayStart = (System.Int32?)value;
							break;
						case "GenerateOnDayEnd":
						
							if (value == null || value is System.Int32)
								this.GenerateOnDayEnd = (System.Int32?)value;
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
		/// Maps to ServiceUnitAutoBillItem.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ServiceUnitAutoBillItemMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitAutoBillItemMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitAutoBillItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ServiceUnitAutoBillItemMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitAutoBillItemMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitAutoBillItem.Quantity
		/// </summary>
		virtual public System.Decimal? Quantity
		{
			get
			{
				return base.GetSystemDecimal(ServiceUnitAutoBillItemMetadata.ColumnNames.Quantity);
			}
			
			set
			{
				base.SetSystemDecimal(ServiceUnitAutoBillItemMetadata.ColumnNames.Quantity, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitAutoBillItem.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(ServiceUnitAutoBillItemMetadata.ColumnNames.SRItemUnit);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitAutoBillItemMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitAutoBillItem.IsAutoPayment
		/// </summary>
		virtual public System.Boolean? IsAutoPayment
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitAutoBillItemMetadata.ColumnNames.IsAutoPayment);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitAutoBillItemMetadata.ColumnNames.IsAutoPayment, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitAutoBillItem.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitAutoBillItemMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitAutoBillItemMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitAutoBillItem.IsGenerateOnRegistration
		/// </summary>
		virtual public System.Boolean? IsGenerateOnRegistration
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnRegistration);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnRegistration, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitAutoBillItem.IsGenerateOnNewRegistration
		/// </summary>
		virtual public System.Boolean? IsGenerateOnNewRegistration
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnNewRegistration);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnNewRegistration, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitAutoBillItem.IsGenerateOnReferral
		/// </summary>
		virtual public System.Boolean? IsGenerateOnReferral
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnReferral);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnReferral, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitAutoBillItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitAutoBillItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceUnitAutoBillItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitAutoBillItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitAutoBillItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitAutoBillItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitAutoBillItem.IsGenerateOnFirstRegistration
		/// </summary>
		virtual public System.Boolean? IsGenerateOnFirstRegistration
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnFirstRegistration);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnFirstRegistration, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitAutoBillItem.IsGenerateOnSchedule
		/// </summary>
		virtual public System.Boolean? IsGenerateOnSchedule
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnSchedule);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnSchedule, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitAutoBillItem.GenerateOnClassIDs
		/// </summary>
		virtual public System.String GenerateOnClassIDs
		{
			get
			{
				return base.GetSystemString(ServiceUnitAutoBillItemMetadata.ColumnNames.GenerateOnClassIDs);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitAutoBillItemMetadata.ColumnNames.GenerateOnClassIDs, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitAutoBillItem.GenerateOnDayStart
		/// </summary>
		virtual public System.Int32? GenerateOnDayStart
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitAutoBillItemMetadata.ColumnNames.GenerateOnDayStart);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitAutoBillItemMetadata.ColumnNames.GenerateOnDayStart, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitAutoBillItem.GenerateOnDayEnd
		/// </summary>
		virtual public System.Int32? GenerateOnDayEnd
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitAutoBillItemMetadata.ColumnNames.GenerateOnDayEnd);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitAutoBillItemMetadata.ColumnNames.GenerateOnDayEnd, value);
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
			public esStrings(esServiceUnitAutoBillItem entity)
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
			public System.String Quantity
			{
				get
				{
					System.Decimal? data = entity.Quantity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Quantity = null;
					else entity.Quantity = Convert.ToDecimal(value);
				}
			}
			public System.String SRItemUnit
			{
				get
				{
					System.String data = entity.SRItemUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemUnit = null;
					else entity.SRItemUnit = Convert.ToString(value);
				}
			}
			public System.String IsAutoPayment
			{
				get
				{
					System.Boolean? data = entity.IsAutoPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAutoPayment = null;
					else entity.IsAutoPayment = Convert.ToBoolean(value);
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
			public System.String IsGenerateOnRegistration
			{
				get
				{
					System.Boolean? data = entity.IsGenerateOnRegistration;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGenerateOnRegistration = null;
					else entity.IsGenerateOnRegistration = Convert.ToBoolean(value);
				}
			}
			public System.String IsGenerateOnNewRegistration
			{
				get
				{
					System.Boolean? data = entity.IsGenerateOnNewRegistration;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGenerateOnNewRegistration = null;
					else entity.IsGenerateOnNewRegistration = Convert.ToBoolean(value);
				}
			}
			public System.String IsGenerateOnReferral
			{
				get
				{
					System.Boolean? data = entity.IsGenerateOnReferral;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGenerateOnReferral = null;
					else entity.IsGenerateOnReferral = Convert.ToBoolean(value);
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
			public System.String IsGenerateOnFirstRegistration
			{
				get
				{
					System.Boolean? data = entity.IsGenerateOnFirstRegistration;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGenerateOnFirstRegistration = null;
					else entity.IsGenerateOnFirstRegistration = Convert.ToBoolean(value);
				}
			}
			public System.String IsGenerateOnSchedule
			{
				get
				{
					System.Boolean? data = entity.IsGenerateOnSchedule;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGenerateOnSchedule = null;
					else entity.IsGenerateOnSchedule = Convert.ToBoolean(value);
				}
			}
			public System.String GenerateOnClassIDs
			{
				get
				{
					System.String data = entity.GenerateOnClassIDs;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GenerateOnClassIDs = null;
					else entity.GenerateOnClassIDs = Convert.ToString(value);
				}
			}
			public System.String GenerateOnDayStart
			{
				get
				{
					System.Int32? data = entity.GenerateOnDayStart;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GenerateOnDayStart = null;
					else entity.GenerateOnDayStart = Convert.ToInt32(value);
				}
			}
			public System.String GenerateOnDayEnd
			{
				get
				{
					System.Int32? data = entity.GenerateOnDayEnd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GenerateOnDayEnd = null;
					else entity.GenerateOnDayEnd = Convert.ToInt32(value);
				}
			}
			private esServiceUnitAutoBillItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceUnitAutoBillItemQuery query)
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
				throw new Exception("esServiceUnitAutoBillItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ServiceUnitAutoBillItem : esServiceUnitAutoBillItem
	{	
	}

	[Serializable]
	abstract public class esServiceUnitAutoBillItemQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitAutoBillItemMetadata.Meta();
			}
		}	
			
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitAutoBillItemMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitAutoBillItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem Quantity
		{
			get
			{
				return new esQueryItem(this, ServiceUnitAutoBillItemMetadata.ColumnNames.Quantity, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, ServiceUnitAutoBillItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		} 
			
		public esQueryItem IsAutoPayment
		{
			get
			{
				return new esQueryItem(this, ServiceUnitAutoBillItemMetadata.ColumnNames.IsAutoPayment, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ServiceUnitAutoBillItemMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsGenerateOnRegistration
		{
			get
			{
				return new esQueryItem(this, ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnRegistration, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsGenerateOnNewRegistration
		{
			get
			{
				return new esQueryItem(this, ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnNewRegistration, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsGenerateOnReferral
		{
			get
			{
				return new esQueryItem(this, ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnReferral, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitAutoBillItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitAutoBillItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsGenerateOnFirstRegistration
		{
			get
			{
				return new esQueryItem(this, ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnFirstRegistration, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsGenerateOnSchedule
		{
			get
			{
				return new esQueryItem(this, ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnSchedule, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem GenerateOnClassIDs
		{
			get
			{
				return new esQueryItem(this, ServiceUnitAutoBillItemMetadata.ColumnNames.GenerateOnClassIDs, esSystemType.String);
			}
		} 
			
		public esQueryItem GenerateOnDayStart
		{
			get
			{
				return new esQueryItem(this, ServiceUnitAutoBillItemMetadata.ColumnNames.GenerateOnDayStart, esSystemType.Int32);
			}
		} 
			
		public esQueryItem GenerateOnDayEnd
		{
			get
			{
				return new esQueryItem(this, ServiceUnitAutoBillItemMetadata.ColumnNames.GenerateOnDayEnd, esSystemType.Int32);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceUnitAutoBillItemCollection")]
	public partial class ServiceUnitAutoBillItemCollection : esServiceUnitAutoBillItemCollection, IEnumerable< ServiceUnitAutoBillItem>
	{
		public ServiceUnitAutoBillItemCollection()
		{

		}	
		
		public static implicit operator List< ServiceUnitAutoBillItem>(ServiceUnitAutoBillItemCollection coll)
		{
			List< ServiceUnitAutoBillItem> list = new List< ServiceUnitAutoBillItem>();
			
			foreach (ServiceUnitAutoBillItem emp in coll)
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
				return  ServiceUnitAutoBillItemMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitAutoBillItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceUnitAutoBillItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceUnitAutoBillItem();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ServiceUnitAutoBillItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitAutoBillItemQuery();
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
		public bool Load(ServiceUnitAutoBillItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ServiceUnitAutoBillItem AddNew()
		{
			ServiceUnitAutoBillItem entity = base.AddNewEntity() as ServiceUnitAutoBillItem;
			
			return entity;		
		}
		public ServiceUnitAutoBillItem FindByPrimaryKey(String serviceUnitID, String itemID)
		{
			return base.FindByPrimaryKey(serviceUnitID, itemID) as ServiceUnitAutoBillItem;
		}

		#region IEnumerable< ServiceUnitAutoBillItem> Members

		IEnumerator< ServiceUnitAutoBillItem> IEnumerable< ServiceUnitAutoBillItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ServiceUnitAutoBillItem;
			}
		}

		#endregion
		
		private ServiceUnitAutoBillItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceUnitAutoBillItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ServiceUnitAutoBillItem ({ServiceUnitID, ItemID})")]
	[Serializable]
	public partial class ServiceUnitAutoBillItem : esServiceUnitAutoBillItem
	{
		public ServiceUnitAutoBillItem()
		{
		}	
	
		public ServiceUnitAutoBillItem(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitAutoBillItemMetadata.Meta();
			}
		}	
	
		override protected esServiceUnitAutoBillItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitAutoBillItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ServiceUnitAutoBillItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitAutoBillItemQuery();
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
		public bool Load(ServiceUnitAutoBillItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ServiceUnitAutoBillItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ServiceUnitAutoBillItemQuery : esServiceUnitAutoBillItemQuery
	{
		public ServiceUnitAutoBillItemQuery()
		{

		}		
		
		public ServiceUnitAutoBillItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ServiceUnitAutoBillItemQuery";
        }
	}

	[Serializable]
	public partial class ServiceUnitAutoBillItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceUnitAutoBillItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ServiceUnitAutoBillItemMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitAutoBillItemMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitAutoBillItemMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitAutoBillItemMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitAutoBillItemMetadata.ColumnNames.Quantity, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceUnitAutoBillItemMetadata.PropertyNames.Quantity;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
 			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitAutoBillItemMetadata.ColumnNames.SRItemUnit, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitAutoBillItemMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitAutoBillItemMetadata.ColumnNames.IsAutoPayment, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitAutoBillItemMetadata.PropertyNames.IsAutoPayment;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitAutoBillItemMetadata.ColumnNames.IsActive, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitAutoBillItemMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnRegistration, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitAutoBillItemMetadata.PropertyNames.IsGenerateOnRegistration;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnNewRegistration, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitAutoBillItemMetadata.PropertyNames.IsGenerateOnNewRegistration;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnReferral, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitAutoBillItemMetadata.PropertyNames.IsGenerateOnReferral;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitAutoBillItemMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitAutoBillItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitAutoBillItemMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitAutoBillItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnFirstRegistration, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitAutoBillItemMetadata.PropertyNames.IsGenerateOnFirstRegistration;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitAutoBillItemMetadata.ColumnNames.IsGenerateOnSchedule, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitAutoBillItemMetadata.PropertyNames.IsGenerateOnSchedule;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitAutoBillItemMetadata.ColumnNames.GenerateOnClassIDs, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitAutoBillItemMetadata.PropertyNames.GenerateOnClassIDs;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitAutoBillItemMetadata.ColumnNames.GenerateOnDayStart, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitAutoBillItemMetadata.PropertyNames.GenerateOnDayStart;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitAutoBillItemMetadata.ColumnNames.GenerateOnDayEnd, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitAutoBillItemMetadata.PropertyNames.GenerateOnDayEnd;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ServiceUnitAutoBillItemMetadata Meta()
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
			public const string Quantity = "Quantity";
			public const string SRItemUnit = "SRItemUnit";
			public const string IsAutoPayment = "IsAutoPayment";
			public const string IsActive = "IsActive";
			public const string IsGenerateOnRegistration = "IsGenerateOnRegistration";
			public const string IsGenerateOnNewRegistration = "IsGenerateOnNewRegistration";
			public const string IsGenerateOnReferral = "IsGenerateOnReferral";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsGenerateOnFirstRegistration = "IsGenerateOnFirstRegistration";
			public const string IsGenerateOnSchedule = "IsGenerateOnSchedule";
			public const string GenerateOnClassIDs = "GenerateOnClassIDs";
			public const string GenerateOnDayStart = "GenerateOnDayStart";
			public const string GenerateOnDayEnd = "GenerateOnDayEnd";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ItemID = "ItemID";
			public const string Quantity = "Quantity";
			public const string SRItemUnit = "SRItemUnit";
			public const string IsAutoPayment = "IsAutoPayment";
			public const string IsActive = "IsActive";
			public const string IsGenerateOnRegistration = "IsGenerateOnRegistration";
			public const string IsGenerateOnNewRegistration = "IsGenerateOnNewRegistration";
			public const string IsGenerateOnReferral = "IsGenerateOnReferral";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsGenerateOnFirstRegistration = "IsGenerateOnFirstRegistration";
			public const string IsGenerateOnSchedule = "IsGenerateOnSchedule";
			public const string GenerateOnClassIDs = "GenerateOnClassIDs";
			public const string GenerateOnDayStart = "GenerateOnDayStart";
			public const string GenerateOnDayEnd = "GenerateOnDayEnd";
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
			lock (typeof(ServiceUnitAutoBillItemMetadata))
			{
				if(ServiceUnitAutoBillItemMetadata.mapDelegates == null)
				{
					ServiceUnitAutoBillItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ServiceUnitAutoBillItemMetadata.meta == null)
				{
					ServiceUnitAutoBillItemMetadata.meta = new ServiceUnitAutoBillItemMetadata();
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
				meta.AddTypeMap("Quantity", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAutoPayment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsGenerateOnRegistration", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsGenerateOnNewRegistration", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsGenerateOnReferral", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsGenerateOnFirstRegistration", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsGenerateOnSchedule", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("GenerateOnClassIDs", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GenerateOnDayStart", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("GenerateOnDayEnd", new esTypeMap("int", "System.Int32"));
		

				meta.Source = "ServiceUnitAutoBillItem";
				meta.Destination = "ServiceUnitAutoBillItem";
				meta.spInsert = "proc_ServiceUnitAutoBillItemInsert";				
				meta.spUpdate = "proc_ServiceUnitAutoBillItemUpdate";		
				meta.spDelete = "proc_ServiceUnitAutoBillItemDelete";
				meta.spLoadAll = "proc_ServiceUnitAutoBillItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceUnitAutoBillItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceUnitAutoBillItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
