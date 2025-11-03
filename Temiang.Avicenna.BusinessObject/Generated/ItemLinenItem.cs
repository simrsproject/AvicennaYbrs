/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/9/2021 10:59:16 AM
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
	abstract public class esItemLinenItemCollection : esEntityCollectionWAuditLog
	{
		public esItemLinenItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemLinenItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemLinenItemQuery query)
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
			this.InitQuery(query as esItemLinenItemQuery);
		}
		#endregion

		virtual public ItemLinenItem DetachEntity(ItemLinenItem entity)
		{
			return base.DetachEntity(entity) as ItemLinenItem;
		}

		virtual public ItemLinenItem AttachEntity(ItemLinenItem entity)
		{
			return base.AttachEntity(entity) as ItemLinenItem;
		}

		virtual public void Combine(ItemLinenItemCollection collection)
		{
			base.Combine(collection);
		}

		new public ItemLinenItem this[int index]
		{
			get
			{
				return base[index] as ItemLinenItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemLinenItem);
		}
	}

	[Serializable]
	abstract public class esItemLinenItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemLinenItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemLinenItem()
		{
		}

		public esItemLinenItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String itemID, String itemDetailID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, itemDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, itemDetailID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String itemID, String itemDetailID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, itemDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, itemDetailID);
		}

		private bool LoadByPrimaryKeyDynamic(String itemID, String itemDetailID)
		{
			esItemLinenItemQuery query = this.GetDynamicQuery();
			query.Where(query.ItemID == itemID, query.ItemDetailID == itemDetailID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String itemID, String itemDetailID)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemID", itemID);
			parms.Add("ItemDetailID", itemDetailID);
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
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ItemDetailID": this.str.ItemDetailID = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "QtyDetail": this.str.QtyDetail = (string)value; break;
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "Qty":

							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
							break;
						case "QtyDetail":

							if (value == null || value is System.Decimal)
								this.QtyDetail = (System.Decimal?)value;
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
		/// Maps to ItemLinenItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemLinenItemMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(ItemLinenItemMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ItemLinenItem.ItemDetailID
		/// </summary>
		virtual public System.String ItemDetailID
		{
			get
			{
				return base.GetSystemString(ItemLinenItemMetadata.ColumnNames.ItemDetailID);
			}

			set
			{
				base.SetSystemString(ItemLinenItemMetadata.ColumnNames.ItemDetailID, value);
			}
		}
		/// <summary>
		/// Maps to ItemLinenItem.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(ItemLinenItemMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(ItemLinenItemMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to ItemLinenItem.QtyDetail
		/// </summary>
		virtual public System.Decimal? QtyDetail
		{
			get
			{
				return base.GetSystemDecimal(ItemLinenItemMetadata.ColumnNames.QtyDetail);
			}

			set
			{
				base.SetSystemDecimal(ItemLinenItemMetadata.ColumnNames.QtyDetail, value);
			}
		}
		/// <summary>
		/// Maps to ItemLinenItem.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(ItemLinenItemMetadata.ColumnNames.SRItemUnit);
			}

			set
			{
				base.SetSystemString(ItemLinenItemMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemLinenItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemLinenItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemLinenItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemLinenItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemLinenItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemLinenItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esItemLinenItem entity)
			{
				this.entity = entity;
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
			public System.String ItemDetailID
			{
				get
				{
					System.String data = entity.ItemDetailID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemDetailID = null;
					else entity.ItemDetailID = Convert.ToString(value);
				}
			}
			public System.String Qty
			{
				get
				{
					System.Decimal? data = entity.Qty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Qty = null;
					else entity.Qty = Convert.ToDecimal(value);
				}
			}
			public System.String QtyDetail
			{
				get
				{
					System.Decimal? data = entity.QtyDetail;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyDetail = null;
					else entity.QtyDetail = Convert.ToDecimal(value);
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
			private esItemLinenItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemLinenItemQuery query)
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
				throw new Exception("esItemLinenItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemLinenItem : esItemLinenItem
	{
	}

	[Serializable]
	abstract public class esItemLinenItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemLinenItemMetadata.Meta();
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemLinenItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ItemDetailID
		{
			get
			{
				return new esQueryItem(this, ItemLinenItemMetadata.ColumnNames.ItemDetailID, esSystemType.String);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, ItemLinenItemMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyDetail
		{
			get
			{
				return new esQueryItem(this, ItemLinenItemMetadata.ColumnNames.QtyDetail, esSystemType.Decimal);
			}
		}

		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, ItemLinenItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemLinenItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemLinenItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemLinenItemCollection")]
	public partial class ItemLinenItemCollection : esItemLinenItemCollection, IEnumerable<ItemLinenItem>
	{
		public ItemLinenItemCollection()
		{

		}

		public static implicit operator List<ItemLinenItem>(ItemLinenItemCollection coll)
		{
			List<ItemLinenItem> list = new List<ItemLinenItem>();

			foreach (ItemLinenItem emp in coll)
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
				return ItemLinenItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemLinenItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemLinenItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemLinenItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemLinenItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemLinenItemQuery();
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
		public bool Load(ItemLinenItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemLinenItem AddNew()
		{
			ItemLinenItem entity = base.AddNewEntity() as ItemLinenItem;

			return entity;
		}
		public ItemLinenItem FindByPrimaryKey(String itemID, String itemDetailID)
		{
			return base.FindByPrimaryKey(itemID, itemDetailID) as ItemLinenItem;
		}

		#region IEnumerable< ItemLinenItem> Members

		IEnumerator<ItemLinenItem> IEnumerable<ItemLinenItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemLinenItem;
			}
		}

		#endregion

		private ItemLinenItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemLinenItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemLinenItem ({ItemID, ItemDetailID})")]
	[Serializable]
	public partial class ItemLinenItem : esItemLinenItem
	{
		public ItemLinenItem()
		{
		}

		public ItemLinenItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemLinenItemMetadata.Meta();
			}
		}

		override protected esItemLinenItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemLinenItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemLinenItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemLinenItemQuery();
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
		public bool Load(ItemLinenItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemLinenItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemLinenItemQuery : esItemLinenItemQuery
	{
		public ItemLinenItemQuery()
		{

		}

		public ItemLinenItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemLinenItemQuery";
		}
	}

	[Serializable]
	public partial class ItemLinenItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemLinenItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemLinenItemMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLinenItemMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ItemLinenItemMetadata.ColumnNames.ItemDetailID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLinenItemMetadata.PropertyNames.ItemDetailID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ItemLinenItemMetadata.ColumnNames.Qty, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemLinenItemMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ItemLinenItemMetadata.ColumnNames.QtyDetail, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemLinenItemMetadata.PropertyNames.QtyDetail;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ItemLinenItemMetadata.ColumnNames.SRItemUnit, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLinenItemMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ItemLinenItemMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemLinenItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemLinenItemMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLinenItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemLinenItemMetadata Meta()
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
			public const string ItemID = "ItemID";
			public const string ItemDetailID = "ItemDetailID";
			public const string Qty = "Qty";
			public const string QtyDetail = "QtyDetail";
			public const string SRItemUnit = "SRItemUnit";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ItemID = "ItemID";
			public const string ItemDetailID = "ItemDetailID";
			public const string Qty = "Qty";
			public const string QtyDetail = "QtyDetail";
			public const string SRItemUnit = "SRItemUnit";
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
			lock (typeof(ItemLinenItemMetadata))
			{
				if (ItemLinenItemMetadata.mapDelegates == null)
				{
					ItemLinenItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemLinenItemMetadata.meta == null)
				{
					ItemLinenItemMetadata.meta = new ItemLinenItemMetadata();
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

				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemDetailID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QtyDetail", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "ItemLinenItem";
				meta.Destination = "ItemLinenItem";
				meta.spInsert = "proc_ItemLinenItemInsert";
				meta.spUpdate = "proc_ItemLinenItemUpdate";
				meta.spDelete = "proc_ItemLinenItemDelete";
				meta.spLoadAll = "proc_ItemLinenItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemLinenItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemLinenItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
