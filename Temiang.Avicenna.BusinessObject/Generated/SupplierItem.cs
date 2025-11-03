/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/3/2018 5:27:38 AM
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
	abstract public class esSupplierItemCollection : esEntityCollectionWAuditLog
	{
		public esSupplierItemCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "SupplierItemCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esSupplierItemQuery query)
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
			this.InitQuery(query as esSupplierItemQuery);
		}
		#endregion
			
		virtual public SupplierItem DetachEntity(SupplierItem entity)
		{
			return base.DetachEntity(entity) as SupplierItem;
		}
		
		virtual public SupplierItem AttachEntity(SupplierItem entity)
		{
			return base.AttachEntity(entity) as SupplierItem;
		}
		
		virtual public void Combine(SupplierItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public SupplierItem this[int index]
		{
			get
			{
				return base[index] as SupplierItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SupplierItem);
		}
	}

	[Serializable]
	abstract public class esSupplierItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSupplierItemQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esSupplierItem()
		{
		}
	
		public esSupplierItem(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String supplierID, String itemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(supplierID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(supplierID, itemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String supplierID, String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(supplierID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(supplierID, itemID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String supplierID, String itemID)
		{
			esSupplierItemQuery query = this.GetDynamicQuery();
			query.Where(query.SupplierID==supplierID, query.ItemID==itemID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String supplierID, String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("SupplierID",supplierID);
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
						case "SupplierID": this.str.SupplierID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "PurchaseDiscount1": this.str.PurchaseDiscount1 = (string)value; break;
						case "PurchaseDiscount2": this.str.PurchaseDiscount2 = (string)value; break;
						case "SRPurchaseUnit": this.str.SRPurchaseUnit = (string)value; break;
						case "PriceInPurchaseUnit": this.str.PriceInPurchaseUnit = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "DrugDistributionLicenseNo": this.str.DrugDistributionLicenseNo = (string)value; break;
						case "ConversionFactor": this.str.ConversionFactor = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PurchaseDiscount1":
						
							if (value == null || value is System.Decimal)
								this.PurchaseDiscount1 = (System.Decimal?)value;
							break;
						case "PurchaseDiscount2":
						
							if (value == null || value is System.Decimal)
								this.PurchaseDiscount2 = (System.Decimal?)value;
							break;
						case "PriceInPurchaseUnit":
						
							if (value == null || value is System.Decimal)
								this.PriceInPurchaseUnit = (System.Decimal?)value;
							break;
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "ConversionFactor":
						
							if (value == null || value is System.Decimal)
								this.ConversionFactor = (System.Decimal?)value;
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
		/// Maps to SupplierItem.SupplierID
		/// </summary>
		virtual public System.String SupplierID
		{
			get
			{
				return base.GetSystemString(SupplierItemMetadata.ColumnNames.SupplierID);
			}
			
			set
			{
				base.SetSystemString(SupplierItemMetadata.ColumnNames.SupplierID, value);
			}
		}
		/// <summary>
		/// Maps to SupplierItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(SupplierItemMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(SupplierItemMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to SupplierItem.PurchaseDiscount1
		/// </summary>
		virtual public System.Decimal? PurchaseDiscount1
		{
			get
			{
				return base.GetSystemDecimal(SupplierItemMetadata.ColumnNames.PurchaseDiscount1);
			}
			
			set
			{
				base.SetSystemDecimal(SupplierItemMetadata.ColumnNames.PurchaseDiscount1, value);
			}
		}
		/// <summary>
		/// Maps to SupplierItem.PurchaseDiscount2
		/// </summary>
		virtual public System.Decimal? PurchaseDiscount2
		{
			get
			{
				return base.GetSystemDecimal(SupplierItemMetadata.ColumnNames.PurchaseDiscount2);
			}
			
			set
			{
				base.SetSystemDecimal(SupplierItemMetadata.ColumnNames.PurchaseDiscount2, value);
			}
		}
		/// <summary>
		/// Maps to SupplierItem.SRPurchaseUnit
		/// </summary>
		virtual public System.String SRPurchaseUnit
		{
			get
			{
				return base.GetSystemString(SupplierItemMetadata.ColumnNames.SRPurchaseUnit);
			}
			
			set
			{
				base.SetSystemString(SupplierItemMetadata.ColumnNames.SRPurchaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to SupplierItem.PriceInPurchaseUnit
		/// </summary>
		virtual public System.Decimal? PriceInPurchaseUnit
		{
			get
			{
				return base.GetSystemDecimal(SupplierItemMetadata.ColumnNames.PriceInPurchaseUnit);
			}
			
			set
			{
				base.SetSystemDecimal(SupplierItemMetadata.ColumnNames.PriceInPurchaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to SupplierItem.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(SupplierItemMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(SupplierItemMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to SupplierItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SupplierItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(SupplierItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SupplierItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SupplierItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(SupplierItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to SupplierItem.DrugDistributionLicenseNo
		/// </summary>
		virtual public System.String DrugDistributionLicenseNo
		{
			get
			{
				return base.GetSystemString(SupplierItemMetadata.ColumnNames.DrugDistributionLicenseNo);
			}
			
			set
			{
				base.SetSystemString(SupplierItemMetadata.ColumnNames.DrugDistributionLicenseNo, value);
			}
		}
		/// <summary>
		/// Maps to SupplierItem.ConversionFactor
		/// </summary>
		virtual public System.Decimal? ConversionFactor
		{
			get
			{
				return base.GetSystemDecimal(SupplierItemMetadata.ColumnNames.ConversionFactor);
			}
			
			set
			{
				base.SetSystemDecimal(SupplierItemMetadata.ColumnNames.ConversionFactor, value);
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
			public esStrings(esSupplierItem entity)
			{
				this.entity = entity;
			}
			public System.String SupplierID
			{
				get
				{
					System.String data = entity.SupplierID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SupplierID = null;
					else entity.SupplierID = Convert.ToString(value);
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
			public System.String PurchaseDiscount1
			{
				get
				{
					System.Decimal? data = entity.PurchaseDiscount1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PurchaseDiscount1 = null;
					else entity.PurchaseDiscount1 = Convert.ToDecimal(value);
				}
			}
			public System.String PurchaseDiscount2
			{
				get
				{
					System.Decimal? data = entity.PurchaseDiscount2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PurchaseDiscount2 = null;
					else entity.PurchaseDiscount2 = Convert.ToDecimal(value);
				}
			}
			public System.String SRPurchaseUnit
			{
				get
				{
					System.String data = entity.SRPurchaseUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPurchaseUnit = null;
					else entity.SRPurchaseUnit = Convert.ToString(value);
				}
			}
			public System.String PriceInPurchaseUnit
			{
				get
				{
					System.Decimal? data = entity.PriceInPurchaseUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriceInPurchaseUnit = null;
					else entity.PriceInPurchaseUnit = Convert.ToDecimal(value);
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
			public System.String DrugDistributionLicenseNo
			{
				get
				{
					System.String data = entity.DrugDistributionLicenseNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DrugDistributionLicenseNo = null;
					else entity.DrugDistributionLicenseNo = Convert.ToString(value);
				}
			}
			public System.String ConversionFactor
			{
				get
				{
					System.Decimal? data = entity.ConversionFactor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConversionFactor = null;
					else entity.ConversionFactor = Convert.ToDecimal(value);
				}
			}
			private esSupplierItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSupplierItemQuery query)
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
				throw new Exception("esSupplierItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SupplierItem : esSupplierItem
	{	
	}

	[Serializable]
	abstract public class esSupplierItemQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return SupplierItemMetadata.Meta();
			}
		}	
			
		public esQueryItem SupplierID
		{
			get
			{
				return new esQueryItem(this, SupplierItemMetadata.ColumnNames.SupplierID, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, SupplierItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem PurchaseDiscount1
		{
			get
			{
				return new esQueryItem(this, SupplierItemMetadata.ColumnNames.PurchaseDiscount1, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem PurchaseDiscount2
		{
			get
			{
				return new esQueryItem(this, SupplierItemMetadata.ColumnNames.PurchaseDiscount2, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem SRPurchaseUnit
		{
			get
			{
				return new esQueryItem(this, SupplierItemMetadata.ColumnNames.SRPurchaseUnit, esSystemType.String);
			}
		} 
			
		public esQueryItem PriceInPurchaseUnit
		{
			get
			{
				return new esQueryItem(this, SupplierItemMetadata.ColumnNames.PriceInPurchaseUnit, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, SupplierItemMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SupplierItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SupplierItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem DrugDistributionLicenseNo
		{
			get
			{
				return new esQueryItem(this, SupplierItemMetadata.ColumnNames.DrugDistributionLicenseNo, esSystemType.String);
			}
		} 
			
		public esQueryItem ConversionFactor
		{
			get
			{
				return new esQueryItem(this, SupplierItemMetadata.ColumnNames.ConversionFactor, esSystemType.Decimal);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SupplierItemCollection")]
	public partial class SupplierItemCollection : esSupplierItemCollection, IEnumerable< SupplierItem>
	{
		public SupplierItemCollection()
		{

		}	
		
		public static implicit operator List< SupplierItem>(SupplierItemCollection coll)
		{
			List< SupplierItem> list = new List< SupplierItem>();
			
			foreach (SupplierItem emp in coll)
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
				return  SupplierItemMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SupplierItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SupplierItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SupplierItem();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public SupplierItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SupplierItemQuery();
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
		public bool Load(SupplierItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SupplierItem AddNew()
		{
			SupplierItem entity = base.AddNewEntity() as SupplierItem;
			
			return entity;		
		}
		public SupplierItem FindByPrimaryKey(String supplierID, String itemID)
		{
			return base.FindByPrimaryKey(supplierID, itemID) as SupplierItem;
		}

		#region IEnumerable< SupplierItem> Members

		IEnumerator< SupplierItem> IEnumerable< SupplierItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as SupplierItem;
			}
		}

		#endregion
		
		private SupplierItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SupplierItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SupplierItem ({SupplierID, ItemID})")]
	[Serializable]
	public partial class SupplierItem : esSupplierItem
	{
		public SupplierItem()
		{
		}	
	
		public SupplierItem(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SupplierItemMetadata.Meta();
			}
		}	
	
		override protected esSupplierItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SupplierItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public SupplierItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SupplierItemQuery();
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
		public bool Load(SupplierItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private SupplierItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SupplierItemQuery : esSupplierItemQuery
	{
		public SupplierItemQuery()
		{

		}		
		
		public SupplierItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "SupplierItemQuery";
        }
	}

	[Serializable]
	public partial class SupplierItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SupplierItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(SupplierItemMetadata.ColumnNames.SupplierID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = SupplierItemMetadata.PropertyNames.SupplierID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(SupplierItemMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SupplierItemMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(SupplierItemMetadata.ColumnNames.PurchaseDiscount1, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SupplierItemMetadata.PropertyNames.PurchaseDiscount1;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
 			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(SupplierItemMetadata.ColumnNames.PurchaseDiscount2, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SupplierItemMetadata.PropertyNames.PurchaseDiscount2;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
 			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(SupplierItemMetadata.ColumnNames.SRPurchaseUnit, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = SupplierItemMetadata.PropertyNames.SRPurchaseUnit;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(SupplierItemMetadata.ColumnNames.PriceInPurchaseUnit, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SupplierItemMetadata.PropertyNames.PriceInPurchaseUnit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(SupplierItemMetadata.ColumnNames.IsActive, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SupplierItemMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(SupplierItemMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SupplierItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(SupplierItemMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = SupplierItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(SupplierItemMetadata.ColumnNames.DrugDistributionLicenseNo, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = SupplierItemMetadata.PropertyNames.DrugDistributionLicenseNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(SupplierItemMetadata.ColumnNames.ConversionFactor, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SupplierItemMetadata.PropertyNames.ConversionFactor;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public SupplierItemMetadata Meta()
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
			public const string SupplierID = "SupplierID";
			public const string ItemID = "ItemID";
			public const string PurchaseDiscount1 = "PurchaseDiscount1";
			public const string PurchaseDiscount2 = "PurchaseDiscount2";
			public const string SRPurchaseUnit = "SRPurchaseUnit";
			public const string PriceInPurchaseUnit = "PriceInPurchaseUnit";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string DrugDistributionLicenseNo = "DrugDistributionLicenseNo";
			public const string ConversionFactor = "ConversionFactor";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string SupplierID = "SupplierID";
			public const string ItemID = "ItemID";
			public const string PurchaseDiscount1 = "PurchaseDiscount1";
			public const string PurchaseDiscount2 = "PurchaseDiscount2";
			public const string SRPurchaseUnit = "SRPurchaseUnit";
			public const string PriceInPurchaseUnit = "PriceInPurchaseUnit";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string DrugDistributionLicenseNo = "DrugDistributionLicenseNo";
			public const string ConversionFactor = "ConversionFactor";
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
			lock (typeof(SupplierItemMetadata))
			{
				if(SupplierItemMetadata.mapDelegates == null)
				{
					SupplierItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (SupplierItemMetadata.meta == null)
				{
					SupplierItemMetadata.meta = new SupplierItemMetadata();
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
				
				meta.AddTypeMap("SupplierID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PurchaseDiscount1", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PurchaseDiscount2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRPurchaseUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PriceInPurchaseUnit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DrugDistributionLicenseNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConversionFactor", new esTypeMap("numeric", "System.Decimal"));
		

				meta.Source = "SupplierItem";
				meta.Destination = "SupplierItem";
				meta.spInsert = "proc_SupplierItemInsert";				
				meta.spUpdate = "proc_SupplierItemUpdate";		
				meta.spDelete = "proc_SupplierItemDelete";
				meta.spLoadAll = "proc_SupplierItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_SupplierItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SupplierItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
