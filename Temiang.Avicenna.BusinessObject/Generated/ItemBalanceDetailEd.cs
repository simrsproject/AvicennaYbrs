/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/10/2023 11:44:20 AM
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
	abstract public class esItemBalanceDetailEdCollection : esEntityCollectionWAuditLog
	{
		public esItemBalanceDetailEdCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemBalanceDetailEdCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemBalanceDetailEdQuery query)
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
			this.InitQuery(query as esItemBalanceDetailEdQuery);
		}
		#endregion

		virtual public ItemBalanceDetailEd DetachEntity(ItemBalanceDetailEd entity)
		{
			return base.DetachEntity(entity) as ItemBalanceDetailEd;
		}

		virtual public ItemBalanceDetailEd AttachEntity(ItemBalanceDetailEd entity)
		{
			return base.AttachEntity(entity) as ItemBalanceDetailEd;
		}

		virtual public void Combine(ItemBalanceDetailEdCollection collection)
		{
			base.Combine(collection);
		}

		new public ItemBalanceDetailEd this[int index]
		{
			get
			{
				return base[index] as ItemBalanceDetailEd;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemBalanceDetailEd);
		}
	}

	[Serializable]
	abstract public class esItemBalanceDetailEd : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemBalanceDetailEdQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemBalanceDetailEd()
		{
		}

		public esItemBalanceDetailEd(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String locationID, String itemID, DateTime expiredDate, String batchNumber)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(locationID, itemID, expiredDate, batchNumber);
			else
				return LoadByPrimaryKeyStoredProcedure(locationID, itemID, expiredDate, batchNumber);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String locationID, String itemID, DateTime expiredDate, String batchNumber)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(locationID, itemID, expiredDate, batchNumber);
			else
				return LoadByPrimaryKeyStoredProcedure(locationID, itemID, expiredDate, batchNumber);
		}

		private bool LoadByPrimaryKeyDynamic(String locationID, String itemID, DateTime expiredDate, String batchNumber)
		{
			esItemBalanceDetailEdQuery query = this.GetDynamicQuery();
			query.Where(query.LocationID == locationID, query.ItemID == itemID, query.ExpiredDate == expiredDate, query.BatchNumber == batchNumber);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String locationID, String itemID, DateTime expiredDate, String batchNumber)
		{
			esParameters parms = new esParameters();
			parms.Add("LocationID", locationID);
			parms.Add("ItemID", itemID);
			parms.Add("ExpiredDate", expiredDate);
			parms.Add("BatchNumber", batchNumber);
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
			if (this.Row == null) this.AddNew();

			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if (value == null || value is System.String)
				{
					// Use the strongly typed property
					switch (name)
					{
						case "LocationID": this.str.LocationID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ExpiredDate": this.str.ExpiredDate = (string)value; break;
						case "BatchNumber": this.str.BatchNumber = (string)value; break;
						case "Balance": this.str.Balance = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "ST": this.str.ST = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ExpiredDate":

							if (value == null || value is System.DateTime)
								this.ExpiredDate = (System.DateTime?)value;
							break;
						case "Balance":

							if (value == null || value is System.Decimal)
								this.Balance = (System.Decimal?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
			else if (this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to ItemBalanceDetailEd.LocationID
		/// </summary>
		virtual public System.String LocationID
		{
			get
			{
				return base.GetSystemString(ItemBalanceDetailEdMetadata.ColumnNames.LocationID);
			}

			set
			{
				base.SetSystemString(ItemBalanceDetailEdMetadata.ColumnNames.LocationID, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalanceDetailEd.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemBalanceDetailEdMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(ItemBalanceDetailEdMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalanceDetailEd.ExpiredDate
		/// </summary>
		virtual public System.DateTime? ExpiredDate
		{
			get
			{
				return base.GetSystemDateTime(ItemBalanceDetailEdMetadata.ColumnNames.ExpiredDate);
			}

			set
			{
				base.SetSystemDateTime(ItemBalanceDetailEdMetadata.ColumnNames.ExpiredDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalanceDetailEd.BatchNumber
		/// </summary>
		virtual public System.String BatchNumber
		{
			get
			{
				return base.GetSystemString(ItemBalanceDetailEdMetadata.ColumnNames.BatchNumber);
			}

			set
			{
				base.SetSystemString(ItemBalanceDetailEdMetadata.ColumnNames.BatchNumber, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalanceDetailEd.Balance
		/// </summary>
		virtual public System.Decimal? Balance
		{
			get
			{
				return base.GetSystemDecimal(ItemBalanceDetailEdMetadata.ColumnNames.Balance);
			}

			set
			{
				base.SetSystemDecimal(ItemBalanceDetailEdMetadata.ColumnNames.Balance, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalanceDetailEd.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ItemBalanceDetailEdMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(ItemBalanceDetailEdMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalanceDetailEd.ST
		/// </summary>
		virtual public System.String ST
		{
			get
			{
				return base.GetSystemString(ItemBalanceDetailEdMetadata.ColumnNames.ST);
			}

			set
			{
				base.SetSystemString(ItemBalanceDetailEdMetadata.ColumnNames.ST, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalanceDetailEd.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemBalanceDetailEdMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemBalanceDetailEdMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalanceDetailEd.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(ItemBalanceDetailEdMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(ItemBalanceDetailEdMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalanceDetailEd.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemBalanceDetailEdMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemBalanceDetailEdMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalanceDetailEd.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemBalanceDetailEdMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemBalanceDetailEdMetadata.ColumnNames.LastUpdateByUserID, value);
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
		[BrowsableAttribute(false)]
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
			public esStrings(esItemBalanceDetailEd entity)
			{
				this.entity = entity;
			}
			public System.String LocationID
			{
				get
				{
					System.String data = entity.LocationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationID = null;
					else entity.LocationID = Convert.ToString(value);
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
			public System.String ExpiredDate
			{
				get
				{
					System.DateTime? data = entity.ExpiredDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExpiredDate = null;
					else entity.ExpiredDate = Convert.ToDateTime(value);
				}
			}
			public System.String BatchNumber
			{
				get
				{
					System.String data = entity.BatchNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BatchNumber = null;
					else entity.BatchNumber = Convert.ToString(value);
				}
			}
			public System.String Balance
			{
				get
				{
					System.Decimal? data = entity.Balance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Balance = null;
					else entity.Balance = Convert.ToDecimal(value);
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
			public System.String ST
			{
				get
				{
					System.String data = entity.ST;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ST = null;
					else entity.ST = Convert.ToString(value);
				}
			}
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
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
			private esItemBalanceDetailEd entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemBalanceDetailEdQuery query)
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
				throw new Exception("esItemBalanceDetailEd can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemBalanceDetailEd : esItemBalanceDetailEd
	{
	}

	[Serializable]
	abstract public class esItemBalanceDetailEdQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemBalanceDetailEdMetadata.Meta();
			}
		}

		public esQueryItem LocationID
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailEdMetadata.ColumnNames.LocationID, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailEdMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ExpiredDate
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailEdMetadata.ColumnNames.ExpiredDate, esSystemType.DateTime);
			}
		}

		public esQueryItem BatchNumber
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailEdMetadata.ColumnNames.BatchNumber, esSystemType.String);
			}
		}

		public esQueryItem Balance
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailEdMetadata.ColumnNames.Balance, esSystemType.Decimal);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailEdMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem ST
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailEdMetadata.ColumnNames.ST, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailEdMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailEdMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailEdMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailEdMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemBalanceDetailEdCollection")]
	public partial class ItemBalanceDetailEdCollection : esItemBalanceDetailEdCollection, IEnumerable<ItemBalanceDetailEd>
	{
		public ItemBalanceDetailEdCollection()
		{

		}

		public static implicit operator List<ItemBalanceDetailEd>(ItemBalanceDetailEdCollection coll)
		{
			List<ItemBalanceDetailEd> list = new List<ItemBalanceDetailEd>();

			foreach (ItemBalanceDetailEd emp in coll)
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
				return ItemBalanceDetailEdMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemBalanceDetailEdQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemBalanceDetailEd(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemBalanceDetailEd();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemBalanceDetailEdQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemBalanceDetailEdQuery();
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
		public bool Load(ItemBalanceDetailEdQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemBalanceDetailEd AddNew()
		{
			ItemBalanceDetailEd entity = base.AddNewEntity() as ItemBalanceDetailEd;

			return entity;
		}
		public ItemBalanceDetailEd FindByPrimaryKey(String locationID, String itemID, DateTime expiredDate, String batchNumber)
		{
			return base.FindByPrimaryKey(locationID, itemID, expiredDate, batchNumber) as ItemBalanceDetailEd;
		}

		#region IEnumerable< ItemBalanceDetailEd> Members

		IEnumerator<ItemBalanceDetailEd> IEnumerable<ItemBalanceDetailEd>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemBalanceDetailEd;
			}
		}

		#endregion

		private ItemBalanceDetailEdQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemBalanceDetailEd' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemBalanceDetailEd ({LocationID, ItemID, ExpiredDate, BatchNumber})")]
	[Serializable]
	public partial class ItemBalanceDetailEd : esItemBalanceDetailEd
	{
		public ItemBalanceDetailEd()
		{
		}

		public ItemBalanceDetailEd(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemBalanceDetailEdMetadata.Meta();
			}
		}

		override protected esItemBalanceDetailEdQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemBalanceDetailEdQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemBalanceDetailEdQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemBalanceDetailEdQuery();
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
		public bool Load(ItemBalanceDetailEdQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemBalanceDetailEdQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemBalanceDetailEdQuery : esItemBalanceDetailEdQuery
	{
		public ItemBalanceDetailEdQuery()
		{

		}

		public ItemBalanceDetailEdQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemBalanceDetailEdQuery";
		}
	}

	[Serializable]
	public partial class ItemBalanceDetailEdMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemBalanceDetailEdMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemBalanceDetailEdMetadata.ColumnNames.LocationID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceDetailEdMetadata.PropertyNames.LocationID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceDetailEdMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceDetailEdMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceDetailEdMetadata.ColumnNames.ExpiredDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemBalanceDetailEdMetadata.PropertyNames.ExpiredDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceDetailEdMetadata.ColumnNames.BatchNumber, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceDetailEdMetadata.PropertyNames.BatchNumber;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceDetailEdMetadata.ColumnNames.Balance, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemBalanceDetailEdMetadata.PropertyNames.Balance;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceDetailEdMetadata.ColumnNames.IsActive, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemBalanceDetailEdMetadata.PropertyNames.IsActive;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceDetailEdMetadata.ColumnNames.ST, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceDetailEdMetadata.PropertyNames.ST;
			c.CharacterMaxLength = 1;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceDetailEdMetadata.ColumnNames.CreatedDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemBalanceDetailEdMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceDetailEdMetadata.ColumnNames.CreatedByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceDetailEdMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceDetailEdMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemBalanceDetailEdMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceDetailEdMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceDetailEdMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemBalanceDetailEdMetadata Meta()
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
			get { return base._columns; }
		}

		#region ColumnNames
		public class ColumnNames
		{
			public const string LocationID = "LocationID";
			public const string ItemID = "ItemID";
			public const string ExpiredDate = "ExpiredDate";
			public const string BatchNumber = "BatchNumber";
			public const string Balance = "Balance";
			public const string IsActive = "IsActive";
			public const string ST = "ST";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string LocationID = "LocationID";
			public const string ItemID = "ItemID";
			public const string ExpiredDate = "ExpiredDate";
			public const string BatchNumber = "BatchNumber";
			public const string Balance = "Balance";
			public const string IsActive = "IsActive";
			public const string ST = "ST";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
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
			lock (typeof(ItemBalanceDetailEdMetadata))
			{
				if (ItemBalanceDetailEdMetadata.mapDelegates == null)
				{
					ItemBalanceDetailEdMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemBalanceDetailEdMetadata.meta == null)
				{
					ItemBalanceDetailEdMetadata.meta = new ItemBalanceDetailEdMetadata();
				}

				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if (!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

				meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExpiredDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("BatchNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Balance", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ST", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "ItemBalanceDetailEd";
				meta.Destination = "ItemBalanceDetailEd";
				meta.spInsert = "proc_ItemBalanceDetailEdInsert";
				meta.spUpdate = "proc_ItemBalanceDetailEdUpdate";
				meta.spDelete = "proc_ItemBalanceDetailEdDelete";
				meta.spLoadAll = "proc_ItemBalanceDetailEdLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemBalanceDetailEdLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemBalanceDetailEdMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
