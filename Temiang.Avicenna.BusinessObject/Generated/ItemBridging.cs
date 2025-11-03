/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/19/2022 1:53:49 PM
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
	abstract public class esItemBridgingCollection : esEntityCollectionWAuditLog
	{
		public esItemBridgingCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemBridgingCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemBridgingQuery query)
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
			this.InitQuery(query as esItemBridgingQuery);
		}
		#endregion

		virtual public ItemBridging DetachEntity(ItemBridging entity)
		{
			return base.DetachEntity(entity) as ItemBridging;
		}

		virtual public ItemBridging AttachEntity(ItemBridging entity)
		{
			return base.AttachEntity(entity) as ItemBridging;
		}

		virtual public void Combine(ItemBridgingCollection collection)
		{
			base.Combine(collection);
		}

		new public ItemBridging this[int index]
		{
			get
			{
				return base[index] as ItemBridging;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemBridging);
		}
	}

	[Serializable]
	abstract public class esItemBridging : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemBridgingQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemBridging()
		{
		}

		public esItemBridging(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String itemID, String sRBridgingType, String bridgingID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, sRBridgingType, bridgingID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, sRBridgingType, bridgingID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String itemID, String sRBridgingType, String bridgingID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, sRBridgingType, bridgingID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, sRBridgingType, bridgingID);
		}

		private bool LoadByPrimaryKeyDynamic(String itemID, String sRBridgingType, String bridgingID)
		{
			esItemBridgingQuery query = this.GetDynamicQuery();
			query.Where(query.ItemID == itemID, query.SRBridgingType == sRBridgingType, query.BridgingID == bridgingID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String itemID, String sRBridgingType, String bridgingID)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemID", itemID);
			parms.Add("SRBridgingType", sRBridgingType);
			parms.Add("BridgingID", bridgingID);
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
						case "SRBridgingType": this.str.SRBridgingType = (string)value; break;
						case "BridgingID": this.str.BridgingID = (string)value; break;
						case "BridgingName": this.str.BridgingName = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "BridgingGroupID": this.str.BridgingGroupID = (string)value; break;
						case "BridgingGroupName": this.str.BridgingGroupName = (string)value; break;
						case "ItemIdExternal": this.str.ItemIdExternal = (string)value; break;
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
		/// Maps to ItemBridging.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemBridgingMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(ItemBridgingMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ItemBridging.SRBridgingType
		/// </summary>
		virtual public System.String SRBridgingType
		{
			get
			{
				return base.GetSystemString(ItemBridgingMetadata.ColumnNames.SRBridgingType);
			}

			set
			{
				base.SetSystemString(ItemBridgingMetadata.ColumnNames.SRBridgingType, value);
			}
		}
		/// <summary>
		/// Maps to ItemBridging.BridgingID
		/// </summary>
		virtual public System.String BridgingID
		{
			get
			{
				return base.GetSystemString(ItemBridgingMetadata.ColumnNames.BridgingID);
			}

			set
			{
				base.SetSystemString(ItemBridgingMetadata.ColumnNames.BridgingID, value);
			}
		}
		/// <summary>
		/// Maps to ItemBridging.BridgingName
		/// </summary>
		virtual public System.String BridgingName
		{
			get
			{
				return base.GetSystemString(ItemBridgingMetadata.ColumnNames.BridgingName);
			}

			set
			{
				base.SetSystemString(ItemBridgingMetadata.ColumnNames.BridgingName, value);
			}
		}
		/// <summary>
		/// Maps to ItemBridging.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ItemBridgingMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(ItemBridgingMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to ItemBridging.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemBridgingMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemBridgingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemBridging.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemBridgingMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemBridgingMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemBridging.BridgingGroupID
		/// </summary>
		virtual public System.String BridgingGroupID
		{
			get
			{
				return base.GetSystemString(ItemBridgingMetadata.ColumnNames.BridgingGroupID);
			}

			set
			{
				base.SetSystemString(ItemBridgingMetadata.ColumnNames.BridgingGroupID, value);
			}
		}
		/// <summary>
		/// Maps to ItemBridging.BridgingGroupName
		/// </summary>
		virtual public System.String BridgingGroupName
		{
			get
			{
				return base.GetSystemString(ItemBridgingMetadata.ColumnNames.BridgingGroupName);
			}

			set
			{
				base.SetSystemString(ItemBridgingMetadata.ColumnNames.BridgingGroupName, value);
			}
		}
		/// <summary>
		/// Maps to ItemBridging.ItemIdExternal
		/// </summary>
		virtual public System.String ItemIdExternal
		{
			get
			{
				return base.GetSystemString(ItemBridgingMetadata.ColumnNames.ItemIdExternal);
			}

			set
			{
				base.SetSystemString(ItemBridgingMetadata.ColumnNames.ItemIdExternal, value);
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
			public esStrings(esItemBridging entity)
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
			public System.String SRBridgingType
			{
				get
				{
					System.String data = entity.SRBridgingType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBridgingType = null;
					else entity.SRBridgingType = Convert.ToString(value);
				}
			}
			public System.String BridgingID
			{
				get
				{
					System.String data = entity.BridgingID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BridgingID = null;
					else entity.BridgingID = Convert.ToString(value);
				}
			}
			public System.String BridgingName
			{
				get
				{
					System.String data = entity.BridgingName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BridgingName = null;
					else entity.BridgingName = Convert.ToString(value);
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
			public System.String BridgingGroupID
			{
				get
				{
					System.String data = entity.BridgingGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BridgingGroupID = null;
					else entity.BridgingGroupID = Convert.ToString(value);
				}
			}
			public System.String BridgingGroupName
			{
				get
				{
					System.String data = entity.BridgingGroupName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BridgingGroupName = null;
					else entity.BridgingGroupName = Convert.ToString(value);
				}
			}
			public System.String ItemIdExternal
			{
				get
				{
					System.String data = entity.ItemIdExternal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemIdExternal = null;
					else entity.ItemIdExternal = Convert.ToString(value);
				}
			}
			private esItemBridging entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemBridgingQuery query)
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
				throw new Exception("esItemBridging can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemBridging : esItemBridging
	{
	}

	[Serializable]
	abstract public class esItemBridgingQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemBridgingMetadata.Meta();
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemBridgingMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem SRBridgingType
		{
			get
			{
				return new esQueryItem(this, ItemBridgingMetadata.ColumnNames.SRBridgingType, esSystemType.String);
			}
		}

		public esQueryItem BridgingID
		{
			get
			{
				return new esQueryItem(this, ItemBridgingMetadata.ColumnNames.BridgingID, esSystemType.String);
			}
		}

		public esQueryItem BridgingName
		{
			get
			{
				return new esQueryItem(this, ItemBridgingMetadata.ColumnNames.BridgingName, esSystemType.String);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ItemBridgingMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemBridgingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemBridgingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem BridgingGroupID
		{
			get
			{
				return new esQueryItem(this, ItemBridgingMetadata.ColumnNames.BridgingGroupID, esSystemType.String);
			}
		}

		public esQueryItem BridgingGroupName
		{
			get
			{
				return new esQueryItem(this, ItemBridgingMetadata.ColumnNames.BridgingGroupName, esSystemType.String);
			}
		}

		public esQueryItem ItemIdExternal
		{
			get
			{
				return new esQueryItem(this, ItemBridgingMetadata.ColumnNames.ItemIdExternal, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemBridgingCollection")]
	public partial class ItemBridgingCollection : esItemBridgingCollection, IEnumerable<ItemBridging>
	{
		public ItemBridgingCollection()
		{

		}

		public static implicit operator List<ItemBridging>(ItemBridgingCollection coll)
		{
			List<ItemBridging> list = new List<ItemBridging>();

			foreach (ItemBridging emp in coll)
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
				return ItemBridgingMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemBridgingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemBridging(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemBridging();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemBridgingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemBridgingQuery();
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
		public bool Load(ItemBridgingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemBridging AddNew()
		{
			ItemBridging entity = base.AddNewEntity() as ItemBridging;

			return entity;
		}
		public ItemBridging FindByPrimaryKey(String itemID, String sRBridgingType, String bridgingID)
		{
			return base.FindByPrimaryKey(itemID, sRBridgingType, bridgingID) as ItemBridging;
		}

		#region IEnumerable< ItemBridging> Members

		IEnumerator<ItemBridging> IEnumerable<ItemBridging>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemBridging;
			}
		}

		#endregion

		private ItemBridgingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemBridging' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemBridging ({ItemID, SRBridgingType, BridgingID})")]
	[Serializable]
	public partial class ItemBridging : esItemBridging
	{
		public ItemBridging()
		{
		}

		public ItemBridging(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemBridgingMetadata.Meta();
			}
		}

		override protected esItemBridgingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemBridgingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemBridgingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemBridgingQuery();
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
		public bool Load(ItemBridgingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemBridgingQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemBridgingQuery : esItemBridgingQuery
	{
		public ItemBridgingQuery()
		{

		}

		public ItemBridgingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemBridgingQuery";
		}
	}

	[Serializable]
	public partial class ItemBridgingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemBridgingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemBridgingMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBridgingMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBridgingMetadata.ColumnNames.SRBridgingType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBridgingMetadata.PropertyNames.SRBridgingType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBridgingMetadata.ColumnNames.BridgingID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBridgingMetadata.PropertyNames.BridgingID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBridgingMetadata.ColumnNames.BridgingName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBridgingMetadata.PropertyNames.BridgingName;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBridgingMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemBridgingMetadata.PropertyNames.IsActive;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBridgingMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemBridgingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBridgingMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBridgingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBridgingMetadata.ColumnNames.BridgingGroupID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBridgingMetadata.PropertyNames.BridgingGroupID;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBridgingMetadata.ColumnNames.BridgingGroupName, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBridgingMetadata.PropertyNames.BridgingGroupName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemBridgingMetadata.ColumnNames.ItemIdExternal, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBridgingMetadata.PropertyNames.ItemIdExternal;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemBridgingMetadata Meta()
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
			public const string SRBridgingType = "SRBridgingType";
			public const string BridgingID = "BridgingID";
			public const string BridgingName = "BridgingName";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string BridgingGroupID = "BridgingGroupID";
			public const string BridgingGroupName = "BridgingGroupName";
			public const string ItemIdExternal = "ItemIdExternal";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ItemID = "ItemID";
			public const string SRBridgingType = "SRBridgingType";
			public const string BridgingID = "BridgingID";
			public const string BridgingName = "BridgingName";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string BridgingGroupID = "BridgingGroupID";
			public const string BridgingGroupName = "BridgingGroupName";
			public const string ItemIdExternal = "ItemIdExternal";
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
			lock (typeof(ItemBridgingMetadata))
			{
				if (ItemBridgingMetadata.mapDelegates == null)
				{
					ItemBridgingMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemBridgingMetadata.meta == null)
				{
					ItemBridgingMetadata.meta = new ItemBridgingMetadata();
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
				meta.AddTypeMap("SRBridgingType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BridgingID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BridgingName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BridgingGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BridgingGroupName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemIdExternal", new esTypeMap("varchar", "System.String"));


				meta.Source = "ItemBridging";
				meta.Destination = "ItemBridging";
				meta.spInsert = "proc_ItemBridgingInsert";
				meta.spUpdate = "proc_ItemBridgingUpdate";
				meta.spDelete = "proc_ItemBridgingDelete";
				meta.spLoadAll = "proc_ItemBridgingLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemBridgingLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemBridgingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
