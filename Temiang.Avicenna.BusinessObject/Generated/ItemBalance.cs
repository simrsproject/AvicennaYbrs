/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/25/2022 6:02:43 PM
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
	abstract public class esItemBalanceCollection : esEntityCollectionWAuditLog
	{
		public esItemBalanceCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemBalanceCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemBalanceQuery query)
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
			this.InitQuery(query as esItemBalanceQuery);
		}
		#endregion

		virtual public ItemBalance DetachEntity(ItemBalance entity)
		{
			return base.DetachEntity(entity) as ItemBalance;
		}

		virtual public ItemBalance AttachEntity(ItemBalance entity)
		{
			return base.AttachEntity(entity) as ItemBalance;
		}

		virtual public void Combine(ItemBalanceCollection collection)
		{
			base.Combine(collection);
		}

		new public ItemBalance this[int index]
		{
			get
			{
				return base[index] as ItemBalance;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemBalance);
		}
	}

	[Serializable]
	abstract public class esItemBalance : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemBalanceQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemBalance()
		{
		}

		public esItemBalance(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String locationID, String itemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(locationID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(locationID, itemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String locationID, String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(locationID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(locationID, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(String locationID, String itemID)
		{
			esItemBalanceQuery query = this.GetDynamicQuery();
			query.Where(query.LocationID == locationID, query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String locationID, String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("LocationID", locationID);
			parms.Add("ItemID", itemID);
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
						case "ReorderType": this.str.ReorderType = (string)value; break;
						case "Minimum": this.str.Minimum = (string)value; break;
						case "Maximum": this.str.Maximum = (string)value; break;
						case "Balance": this.str.Balance = (string)value; break;
						case "Booking": this.str.Booking = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SRItemBin": this.str.SRItemBin = (string)value; break;
						case "ItemSubBin": this.str.ItemSubBin = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "Minimum":

							if (value == null || value is System.Decimal)
								this.Minimum = (System.Decimal?)value;
							break;
						case "Maximum":

							if (value == null || value is System.Decimal)
								this.Maximum = (System.Decimal?)value;
							break;
						case "Balance":

							if (value == null || value is System.Decimal)
								this.Balance = (System.Decimal?)value;
							break;
						case "Booking":

							if (value == null || value is System.Decimal)
								this.Booking = (System.Decimal?)value;
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
		/// Maps to ItemBalance.LocationID
		/// </summary>
		virtual public System.String LocationID
		{
			get
			{
				return base.GetSystemString(ItemBalanceMetadata.ColumnNames.LocationID);
			}

			set
			{
				base.SetSystemString(ItemBalanceMetadata.ColumnNames.LocationID, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalance.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemBalanceMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(ItemBalanceMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalance.ReorderType
		/// </summary>
		virtual public System.String ReorderType
		{
			get
			{
				return base.GetSystemString(ItemBalanceMetadata.ColumnNames.ReorderType);
			}

			set
			{
				base.SetSystemString(ItemBalanceMetadata.ColumnNames.ReorderType, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalance.Minimum
		/// </summary>
		virtual public System.Decimal? Minimum
		{
			get
			{
				return base.GetSystemDecimal(ItemBalanceMetadata.ColumnNames.Minimum);
			}

			set
			{
				base.SetSystemDecimal(ItemBalanceMetadata.ColumnNames.Minimum, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalance.Maximum
		/// </summary>
		virtual public System.Decimal? Maximum
		{
			get
			{
				return base.GetSystemDecimal(ItemBalanceMetadata.ColumnNames.Maximum);
			}

			set
			{
				base.SetSystemDecimal(ItemBalanceMetadata.ColumnNames.Maximum, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalance.Balance
		/// </summary>
		virtual public System.Decimal? Balance
		{
			get
			{
				return base.GetSystemDecimal(ItemBalanceMetadata.ColumnNames.Balance);
			}

			set
			{
				base.SetSystemDecimal(ItemBalanceMetadata.ColumnNames.Balance, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalance.Booking
		/// </summary>
		virtual public System.Decimal? Booking
		{
			get
			{
				return base.GetSystemDecimal(ItemBalanceMetadata.ColumnNames.Booking);
			}

			set
			{
				base.SetSystemDecimal(ItemBalanceMetadata.ColumnNames.Booking, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalance.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemBalanceMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemBalanceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalance.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemBalanceMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemBalanceMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalance.SRItemBin
		/// </summary>
		virtual public System.String SRItemBin
		{
			get
			{
				return base.GetSystemString(ItemBalanceMetadata.ColumnNames.SRItemBin);
			}

			set
			{
				base.SetSystemString(ItemBalanceMetadata.ColumnNames.SRItemBin, value);
			}
		}
		/// <summary>
		/// Maps to ItemBalance.ItemSubBin
		/// </summary>
		virtual public System.String ItemSubBin
		{
			get
			{
				return base.GetSystemString(ItemBalanceMetadata.ColumnNames.ItemSubBin);
			}

			set
			{
				base.SetSystemString(ItemBalanceMetadata.ColumnNames.ItemSubBin, value);
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
			public esStrings(esItemBalance entity)
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
			public System.String ReorderType
			{
				get
				{
					System.String data = entity.ReorderType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReorderType = null;
					else entity.ReorderType = Convert.ToString(value);
				}
			}
			public System.String Minimum
			{
				get
				{
					System.Decimal? data = entity.Minimum;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Minimum = null;
					else entity.Minimum = Convert.ToDecimal(value);
				}
			}
			public System.String Maximum
			{
				get
				{
					System.Decimal? data = entity.Maximum;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Maximum = null;
					else entity.Maximum = Convert.ToDecimal(value);
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
			public System.String Booking
			{
				get
				{
					System.Decimal? data = entity.Booking;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Booking = null;
					else entity.Booking = Convert.ToDecimal(value);
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
			public System.String SRItemBin
			{
				get
				{
					System.String data = entity.SRItemBin;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemBin = null;
					else entity.SRItemBin = Convert.ToString(value);
				}
			}
			public System.String ItemSubBin
			{
				get
				{
					System.String data = entity.ItemSubBin;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemSubBin = null;
					else entity.ItemSubBin = Convert.ToString(value);
				}
			}
			private esItemBalance entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemBalanceQuery query)
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
				throw new Exception("esItemBalance can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemBalance : esItemBalance
	{
	}

	[Serializable]
	abstract public class esItemBalanceQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemBalanceMetadata.Meta();
			}
		}

		public esQueryItem LocationID
		{
			get
			{
				return new esQueryItem(this, ItemBalanceMetadata.ColumnNames.LocationID, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemBalanceMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ReorderType
		{
			get
			{
				return new esQueryItem(this, ItemBalanceMetadata.ColumnNames.ReorderType, esSystemType.String);
			}
		}

		public esQueryItem Minimum
		{
			get
			{
				return new esQueryItem(this, ItemBalanceMetadata.ColumnNames.Minimum, esSystemType.Decimal);
			}
		}

		public esQueryItem Maximum
		{
			get
			{
				return new esQueryItem(this, ItemBalanceMetadata.ColumnNames.Maximum, esSystemType.Decimal);
			}
		}

		public esQueryItem Balance
		{
			get
			{
				return new esQueryItem(this, ItemBalanceMetadata.ColumnNames.Balance, esSystemType.Decimal);
			}
		}

		public esQueryItem Booking
		{
			get
			{
				return new esQueryItem(this, ItemBalanceMetadata.ColumnNames.Booking, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemBalanceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemBalanceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SRItemBin
		{
			get
			{
				return new esQueryItem(this, ItemBalanceMetadata.ColumnNames.SRItemBin, esSystemType.String);
			}
		}

		public esQueryItem ItemSubBin
		{
			get
			{
				return new esQueryItem(this, ItemBalanceMetadata.ColumnNames.ItemSubBin, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemBalanceCollection")]
	public partial class ItemBalanceCollection : esItemBalanceCollection, IEnumerable<ItemBalance>
	{
		public ItemBalanceCollection()
		{

		}

		public static implicit operator List<ItemBalance>(ItemBalanceCollection coll)
		{
			List<ItemBalance> list = new List<ItemBalance>();

			foreach (ItemBalance emp in coll)
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
				return ItemBalanceMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemBalance(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemBalance();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemBalanceQuery();
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
		public bool Load(ItemBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemBalance AddNew()
		{
			ItemBalance entity = base.AddNewEntity() as ItemBalance;

			return entity;
		}
		public ItemBalance FindByPrimaryKey(String locationID, String itemID)
		{
			return base.FindByPrimaryKey(locationID, itemID) as ItemBalance;
		}

		#region IEnumerable< ItemBalance> Members

		IEnumerator<ItemBalance> IEnumerable<ItemBalance>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemBalance;
			}
		}

		#endregion

		private ItemBalanceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemBalance' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemBalance ({LocationID, ItemID})")]
	[Serializable]
	public partial class ItemBalance : esItemBalance
	{
		public ItemBalance()
		{
		}

		public ItemBalance(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemBalanceMetadata.Meta();
			}
		}

		override protected esItemBalanceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemBalanceQuery();
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
		public bool Load(ItemBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemBalanceQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemBalanceQuery : esItemBalanceQuery
	{
		public ItemBalanceQuery()
		{

		}

		public ItemBalanceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemBalanceQuery";
		}
	}

	[Serializable]
	public partial class ItemBalanceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemBalanceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemBalanceMetadata.ColumnNames.LocationID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceMetadata.PropertyNames.LocationID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceMetadata.ColumnNames.ReorderType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceMetadata.PropertyNames.ReorderType;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceMetadata.ColumnNames.Minimum, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemBalanceMetadata.PropertyNames.Minimum;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceMetadata.ColumnNames.Maximum, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemBalanceMetadata.PropertyNames.Maximum;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceMetadata.ColumnNames.Balance, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemBalanceMetadata.PropertyNames.Balance;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceMetadata.ColumnNames.Booking, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemBalanceMetadata.PropertyNames.Booking;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemBalanceMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceMetadata.ColumnNames.SRItemBin, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceMetadata.PropertyNames.SRItemBin;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBalanceMetadata.ColumnNames.ItemSubBin, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceMetadata.PropertyNames.ItemSubBin;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemBalanceMetadata Meta()
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
			public const string ReorderType = "ReorderType";
			public const string Minimum = "Minimum";
			public const string Maximum = "Maximum";
			public const string Balance = "Balance";
			public const string Booking = "Booking";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRItemBin = "SRItemBin";
			public const string ItemSubBin = "ItemSubBin";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string LocationID = "LocationID";
			public const string ItemID = "ItemID";
			public const string ReorderType = "ReorderType";
			public const string Minimum = "Minimum";
			public const string Maximum = "Maximum";
			public const string Balance = "Balance";
			public const string Booking = "Booking";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRItemBin = "SRItemBin";
			public const string ItemSubBin = "ItemSubBin";
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
			lock (typeof(ItemBalanceMetadata))
			{
				if (ItemBalanceMetadata.mapDelegates == null)
				{
					ItemBalanceMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemBalanceMetadata.meta == null)
				{
					ItemBalanceMetadata.meta = new ItemBalanceMetadata();
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
				meta.AddTypeMap("ReorderType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Minimum", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Maximum", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Balance", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Booking", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemBin", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemSubBin", new esTypeMap("varchar", "System.String"));


				meta.Source = "ItemBalance";
				meta.Destination = "ItemBalance";
				meta.spInsert = "proc_ItemBalanceInsert";
				meta.spUpdate = "proc_ItemBalanceUpdate";
				meta.spDelete = "proc_ItemBalanceDelete";
				meta.spLoadAll = "proc_ItemBalanceLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemBalanceLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemBalanceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
