/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/9/2020 11:02:42 PM
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
	abstract public class esItemGroupCollection : esEntityCollectionWAuditLog
	{
		public esItemGroupCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemGroupCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemGroupQuery query)
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
			this.InitQuery(query as esItemGroupQuery);
		}
		#endregion

		virtual public ItemGroup DetachEntity(ItemGroup entity)
		{
			return base.DetachEntity(entity) as ItemGroup;
		}

		virtual public ItemGroup AttachEntity(ItemGroup entity)
		{
			return base.AttachEntity(entity) as ItemGroup;
		}

		virtual public void Combine(ItemGroupCollection collection)
		{
			base.Combine(collection);
		}

		new public ItemGroup this[int index]
		{
			get
			{
				return base[index] as ItemGroup;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemGroup);
		}
	}

	[Serializable]
	abstract public class esItemGroup : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemGroupQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemGroup()
		{
		}

		public esItemGroup(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String itemGroupID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemGroupID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemGroupID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String itemGroupID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemGroupID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemGroupID);
		}

		private bool LoadByPrimaryKeyDynamic(String itemGroupID)
		{
			esItemGroupQuery query = this.GetDynamicQuery();
			query.Where(query.ItemGroupID == itemGroupID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String itemGroupID)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemGroupID", itemGroupID);
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
						case "ItemGroupID": this.str.ItemGroupID = (string)value; break;
						case "ItemGroupName": this.str.ItemGroupName = (string)value; break;
						case "SRItemType": this.str.SRItemType = (string)value; break;
						case "CitoValue": this.str.CitoValue = (string)value; break;
						case "IsCitoInPercent": this.str.IsCitoInPercent = (string)value; break;
						case "AccountID": this.str.AccountID = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "Initial": this.str.Initial = (string)value; break;
						case "RestrictionUserType": this.str.RestrictionUserType = (string)value; break;
						case "CssClass": this.str.CssClass = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "CitoValue":

							if (value == null || value is System.Decimal)
								this.CitoValue = (System.Decimal?)value;
							break;
						case "IsCitoInPercent":

							if (value == null || value is System.Boolean)
								this.IsCitoInPercent = (System.Boolean?)value;
							break;
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
		/// Maps to ItemGroup.ItemGroupID
		/// </summary>
		virtual public System.String ItemGroupID
		{
			get
			{
				return base.GetSystemString(ItemGroupMetadata.ColumnNames.ItemGroupID);
			}

			set
			{
				base.SetSystemString(ItemGroupMetadata.ColumnNames.ItemGroupID, value);
			}
		}
		/// <summary>
		/// Maps to ItemGroup.ItemGroupName
		/// </summary>
		virtual public System.String ItemGroupName
		{
			get
			{
				return base.GetSystemString(ItemGroupMetadata.ColumnNames.ItemGroupName);
			}

			set
			{
				base.SetSystemString(ItemGroupMetadata.ColumnNames.ItemGroupName, value);
			}
		}
		/// <summary>
		/// Maps to ItemGroup.SRItemType
		/// </summary>
		virtual public System.String SRItemType
		{
			get
			{
				return base.GetSystemString(ItemGroupMetadata.ColumnNames.SRItemType);
			}

			set
			{
				base.SetSystemString(ItemGroupMetadata.ColumnNames.SRItemType, value);
			}
		}
		/// <summary>
		/// Maps to ItemGroup.CitoValue
		/// </summary>
		virtual public System.Decimal? CitoValue
		{
			get
			{
				return base.GetSystemDecimal(ItemGroupMetadata.ColumnNames.CitoValue);
			}

			set
			{
				base.SetSystemDecimal(ItemGroupMetadata.ColumnNames.CitoValue, value);
			}
		}
		/// <summary>
		/// Maps to ItemGroup.IsCitoInPercent
		/// </summary>
		virtual public System.Boolean? IsCitoInPercent
		{
			get
			{
				return base.GetSystemBoolean(ItemGroupMetadata.ColumnNames.IsCitoInPercent);
			}

			set
			{
				base.SetSystemBoolean(ItemGroupMetadata.ColumnNames.IsCitoInPercent, value);
			}
		}
		/// <summary>
		/// Maps to ItemGroup.AccountID
		/// </summary>
		virtual public System.String AccountID
		{
			get
			{
				return base.GetSystemString(ItemGroupMetadata.ColumnNames.AccountID);
			}

			set
			{
				base.SetSystemString(ItemGroupMetadata.ColumnNames.AccountID, value);
			}
		}
		/// <summary>
		/// Maps to ItemGroup.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ItemGroupMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(ItemGroupMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to ItemGroup.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemGroupMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemGroupMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemGroup.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemGroupMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemGroupMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemGroup.Initial
		/// </summary>
		virtual public System.String Initial
		{
			get
			{
				return base.GetSystemString(ItemGroupMetadata.ColumnNames.Initial);
			}

			set
			{
				base.SetSystemString(ItemGroupMetadata.ColumnNames.Initial, value);
			}
		}
		/// <summary>
		/// Maps to ItemGroup.RestrictionUserType
		/// </summary>
		virtual public System.String RestrictionUserType
		{
			get
			{
				return base.GetSystemString(ItemGroupMetadata.ColumnNames.RestrictionUserType);
			}

			set
			{
				base.SetSystemString(ItemGroupMetadata.ColumnNames.RestrictionUserType, value);
			}
		}
		/// <summary>
		/// Maps to ItemGroup.CssClass
		/// </summary>
		virtual public System.String CssClass
		{
			get
			{
				return base.GetSystemString(ItemGroupMetadata.ColumnNames.CssClass);
			}

			set
			{
				base.SetSystemString(ItemGroupMetadata.ColumnNames.CssClass, value);
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
			public esStrings(esItemGroup entity)
			{
				this.entity = entity;
			}
			public System.String ItemGroupID
			{
				get
				{
					System.String data = entity.ItemGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemGroupID = null;
					else entity.ItemGroupID = Convert.ToString(value);
				}
			}
			public System.String ItemGroupName
			{
				get
				{
					System.String data = entity.ItemGroupName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemGroupName = null;
					else entity.ItemGroupName = Convert.ToString(value);
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
			public System.String CitoValue
			{
				get
				{
					System.Decimal? data = entity.CitoValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CitoValue = null;
					else entity.CitoValue = Convert.ToDecimal(value);
				}
			}
			public System.String IsCitoInPercent
			{
				get
				{
					System.Boolean? data = entity.IsCitoInPercent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCitoInPercent = null;
					else entity.IsCitoInPercent = Convert.ToBoolean(value);
				}
			}
			public System.String AccountID
			{
				get
				{
					System.String data = entity.AccountID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AccountID = null;
					else entity.AccountID = Convert.ToString(value);
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
			public System.String Initial
			{
				get
				{
					System.String data = entity.Initial;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Initial = null;
					else entity.Initial = Convert.ToString(value);
				}
			}
			public System.String RestrictionUserType
			{
				get
				{
					System.String data = entity.RestrictionUserType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RestrictionUserType = null;
					else entity.RestrictionUserType = Convert.ToString(value);
				}
			}
			public System.String CssClass
			{
				get
				{
					System.String data = entity.CssClass;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CssClass = null;
					else entity.CssClass = Convert.ToString(value);
				}
			}
			private esItemGroup entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemGroupQuery query)
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
				throw new Exception("esItemGroup can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemGroup : esItemGroup
	{
	}

	[Serializable]
	abstract public class esItemGroupQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemGroupMetadata.Meta();
			}
		}

		public esQueryItem ItemGroupID
		{
			get
			{
				return new esQueryItem(this, ItemGroupMetadata.ColumnNames.ItemGroupID, esSystemType.String);
			}
		}

		public esQueryItem ItemGroupName
		{
			get
			{
				return new esQueryItem(this, ItemGroupMetadata.ColumnNames.ItemGroupName, esSystemType.String);
			}
		}

		public esQueryItem SRItemType
		{
			get
			{
				return new esQueryItem(this, ItemGroupMetadata.ColumnNames.SRItemType, esSystemType.String);
			}
		}

		public esQueryItem CitoValue
		{
			get
			{
				return new esQueryItem(this, ItemGroupMetadata.ColumnNames.CitoValue, esSystemType.Decimal);
			}
		}

		public esQueryItem IsCitoInPercent
		{
			get
			{
				return new esQueryItem(this, ItemGroupMetadata.ColumnNames.IsCitoInPercent, esSystemType.Boolean);
			}
		}

		public esQueryItem AccountID
		{
			get
			{
				return new esQueryItem(this, ItemGroupMetadata.ColumnNames.AccountID, esSystemType.String);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ItemGroupMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemGroupMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemGroupMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem Initial
		{
			get
			{
				return new esQueryItem(this, ItemGroupMetadata.ColumnNames.Initial, esSystemType.String);
			}
		}

		public esQueryItem RestrictionUserType
		{
			get
			{
				return new esQueryItem(this, ItemGroupMetadata.ColumnNames.RestrictionUserType, esSystemType.String);
			}
		}

		public esQueryItem CssClass
		{
			get
			{
				return new esQueryItem(this, ItemGroupMetadata.ColumnNames.CssClass, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemGroupCollection")]
	public partial class ItemGroupCollection : esItemGroupCollection, IEnumerable<ItemGroup>
	{
		public ItemGroupCollection()
		{

		}

		public static implicit operator List<ItemGroup>(ItemGroupCollection coll)
		{
			List<ItemGroup> list = new List<ItemGroup>();

			foreach (ItemGroup emp in coll)
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
				return ItemGroupMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemGroupQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemGroup(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemGroup();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemGroupQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemGroupQuery();
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
		public bool Load(ItemGroupQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemGroup AddNew()
		{
			ItemGroup entity = base.AddNewEntity() as ItemGroup;

			return entity;
		}
		public ItemGroup FindByPrimaryKey(String itemGroupID)
		{
			return base.FindByPrimaryKey(itemGroupID) as ItemGroup;
		}

		#region IEnumerable< ItemGroup> Members

		IEnumerator<ItemGroup> IEnumerable<ItemGroup>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemGroup;
			}
		}

		#endregion

		private ItemGroupQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemGroup' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemGroup ({ItemGroupID})")]
	[Serializable]
	public partial class ItemGroup : esItemGroup
	{
		public ItemGroup()
		{
		}

		public ItemGroup(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemGroupMetadata.Meta();
			}
		}

		override protected esItemGroupQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemGroupQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemGroupQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemGroupQuery();
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
		public bool Load(ItemGroupQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemGroupQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemGroupQuery : esItemGroupQuery
	{
		public ItemGroupQuery()
		{

		}

		public ItemGroupQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemGroupQuery";
		}
	}

	[Serializable]
	public partial class ItemGroupMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemGroupMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemGroupMetadata.ColumnNames.ItemGroupID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemGroupMetadata.PropertyNames.ItemGroupID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemGroupMetadata.ColumnNames.ItemGroupName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemGroupMetadata.PropertyNames.ItemGroupName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemGroupMetadata.ColumnNames.SRItemType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemGroupMetadata.PropertyNames.SRItemType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemGroupMetadata.ColumnNames.CitoValue, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemGroupMetadata.PropertyNames.CitoValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ItemGroupMetadata.ColumnNames.IsCitoInPercent, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemGroupMetadata.PropertyNames.IsCitoInPercent;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemGroupMetadata.ColumnNames.AccountID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemGroupMetadata.PropertyNames.AccountID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemGroupMetadata.ColumnNames.IsActive, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemGroupMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemGroupMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemGroupMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemGroupMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemGroupMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemGroupMetadata.ColumnNames.Initial, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemGroupMetadata.PropertyNames.Initial;
			c.CharacterMaxLength = 8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemGroupMetadata.ColumnNames.RestrictionUserType, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemGroupMetadata.PropertyNames.RestrictionUserType;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemGroupMetadata.ColumnNames.CssClass, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemGroupMetadata.PropertyNames.CssClass;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemGroupMetadata Meta()
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
			public const string ItemGroupID = "ItemGroupID";
			public const string ItemGroupName = "ItemGroupName";
			public const string SRItemType = "SRItemType";
			public const string CitoValue = "CitoValue";
			public const string IsCitoInPercent = "IsCitoInPercent";
			public const string AccountID = "AccountID";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Initial = "Initial";
			public const string RestrictionUserType = "RestrictionUserType";
			public const string CssClass = "CssClass";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ItemGroupID = "ItemGroupID";
			public const string ItemGroupName = "ItemGroupName";
			public const string SRItemType = "SRItemType";
			public const string CitoValue = "CitoValue";
			public const string IsCitoInPercent = "IsCitoInPercent";
			public const string AccountID = "AccountID";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Initial = "Initial";
			public const string RestrictionUserType = "RestrictionUserType";
			public const string CssClass = "CssClass";
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
			lock (typeof(ItemGroupMetadata))
			{
				if (ItemGroupMetadata.mapDelegates == null)
				{
					ItemGroupMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemGroupMetadata.meta == null)
				{
					ItemGroupMetadata.meta = new ItemGroupMetadata();
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

				meta.AddTypeMap("ItemGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemGroupName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CitoValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsCitoInPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AccountID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Initial", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RestrictionUserType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CssClass", new esTypeMap("varchar", "System.String"));


				meta.Source = "ItemGroup";
				meta.Destination = "ItemGroup";
				meta.spInsert = "proc_ItemGroupInsert";
				meta.spUpdate = "proc_ItemGroupUpdate";
				meta.spDelete = "proc_ItemGroupDelete";
				meta.spLoadAll = "proc_ItemGroupLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemGroupLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemGroupMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
