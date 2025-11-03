/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/25/2022 1:58:37 PM
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
	abstract public class esMenuItemFoodCollection : esEntityCollectionWAuditLog
	{
		public esMenuItemFoodCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MenuItemFoodCollection";
		}

		#region Query Logic
		protected void InitQuery(esMenuItemFoodQuery query)
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
			this.InitQuery(query as esMenuItemFoodQuery);
		}
		#endregion

		virtual public MenuItemFood DetachEntity(MenuItemFood entity)
		{
			return base.DetachEntity(entity) as MenuItemFood;
		}

		virtual public MenuItemFood AttachEntity(MenuItemFood entity)
		{
			return base.AttachEntity(entity) as MenuItemFood;
		}

		virtual public void Combine(MenuItemFoodCollection collection)
		{
			base.Combine(collection);
		}

		new public MenuItemFood this[int index]
		{
			get
			{
				return base[index] as MenuItemFood;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MenuItemFood);
		}
	}

	[Serializable]
	abstract public class esMenuItemFood : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMenuItemFoodQuery GetDynamicQuery()
		{
			return null;
		}

		public esMenuItemFood()
		{
		}

		public esMenuItemFood(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String menuItemID, String sRMealSet, String foodID, String sRMenuItemFoodGroup)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(menuItemID, sRMealSet, foodID, sRMenuItemFoodGroup);
			else
				return LoadByPrimaryKeyStoredProcedure(menuItemID, sRMealSet, foodID, sRMenuItemFoodGroup);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String menuItemID, String sRMealSet, String foodID, String sRMenuItemFoodGroup)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(menuItemID, sRMealSet, foodID, sRMenuItemFoodGroup);
			else
				return LoadByPrimaryKeyStoredProcedure(menuItemID, sRMealSet, foodID, sRMenuItemFoodGroup);
		}

		private bool LoadByPrimaryKeyDynamic(String menuItemID, String sRMealSet, String foodID, String sRMenuItemFoodGroup)
		{
			esMenuItemFoodQuery query = this.GetDynamicQuery();
			query.Where(query.MenuItemID == menuItemID, query.SRMealSet == sRMealSet, query.FoodID == foodID, query.SRMenuItemFoodGroup == sRMenuItemFoodGroup);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String menuItemID, String sRMealSet, String foodID, String sRMenuItemFoodGroup)
		{
			esParameters parms = new esParameters();
			parms.Add("MenuItemID", menuItemID);
			parms.Add("SRMealSet", sRMealSet);
			parms.Add("FoodID", foodID);
			parms.Add("SRMenuItemFoodGroup", sRMenuItemFoodGroup);
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
						case "MenuItemID": this.str.MenuItemID = (string)value; break;
						case "SRMealSet": this.str.SRMealSet = (string)value; break;
						case "FoodID": this.str.FoodID = (string)value; break;
						case "SRMenuItemFoodGroup": this.str.SRMenuItemFoodGroup = (string)value; break;
						case "IsOptional": this.str.IsOptional = (string)value; break;
						case "IsStandard": this.str.IsStandard = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IsOptional":

							if (value == null || value is System.Boolean)
								this.IsOptional = (System.Boolean?)value;
							break;
						case "IsStandard":

							if (value == null || value is System.Boolean)
								this.IsStandard = (System.Boolean?)value;
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
		/// Maps to MenuItemFood.MenuItemID
		/// </summary>
		virtual public System.String MenuItemID
		{
			get
			{
				return base.GetSystemString(MenuItemFoodMetadata.ColumnNames.MenuItemID);
			}

			set
			{
				base.SetSystemString(MenuItemFoodMetadata.ColumnNames.MenuItemID, value);
			}
		}
		/// <summary>
		/// Maps to MenuItemFood.SRMealSet
		/// </summary>
		virtual public System.String SRMealSet
		{
			get
			{
				return base.GetSystemString(MenuItemFoodMetadata.ColumnNames.SRMealSet);
			}

			set
			{
				base.SetSystemString(MenuItemFoodMetadata.ColumnNames.SRMealSet, value);
			}
		}
		/// <summary>
		/// Maps to MenuItemFood.FoodID
		/// </summary>
		virtual public System.String FoodID
		{
			get
			{
				return base.GetSystemString(MenuItemFoodMetadata.ColumnNames.FoodID);
			}

			set
			{
				base.SetSystemString(MenuItemFoodMetadata.ColumnNames.FoodID, value);
			}
		}
		/// <summary>
		/// Maps to MenuItemFood.SRMenuItemFoodGroup
		/// </summary>
		virtual public System.String SRMenuItemFoodGroup
		{
			get
			{
				return base.GetSystemString(MenuItemFoodMetadata.ColumnNames.SRMenuItemFoodGroup);
			}

			set
			{
				base.SetSystemString(MenuItemFoodMetadata.ColumnNames.SRMenuItemFoodGroup, value);
			}
		}
		/// <summary>
		/// Maps to MenuItemFood.IsOptional
		/// </summary>
		virtual public System.Boolean? IsOptional
		{
			get
			{
				return base.GetSystemBoolean(MenuItemFoodMetadata.ColumnNames.IsOptional);
			}

			set
			{
				base.SetSystemBoolean(MenuItemFoodMetadata.ColumnNames.IsOptional, value);
			}
		}
		/// <summary>
		/// Maps to MenuItemFood.IsStandard
		/// </summary>
		virtual public System.Boolean? IsStandard
		{
			get
			{
				return base.GetSystemBoolean(MenuItemFoodMetadata.ColumnNames.IsStandard);
			}

			set
			{
				base.SetSystemBoolean(MenuItemFoodMetadata.ColumnNames.IsStandard, value);
			}
		}
		/// <summary>
		/// Maps to MenuItemFood.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MenuItemFoodMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MenuItemFoodMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MenuItemFood.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MenuItemFoodMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MenuItemFoodMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMenuItemFood entity)
			{
				this.entity = entity;
			}
			public System.String MenuItemID
			{
				get
				{
					System.String data = entity.MenuItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MenuItemID = null;
					else entity.MenuItemID = Convert.ToString(value);
				}
			}
			public System.String SRMealSet
			{
				get
				{
					System.String data = entity.SRMealSet;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMealSet = null;
					else entity.SRMealSet = Convert.ToString(value);
				}
			}
			public System.String FoodID
			{
				get
				{
					System.String data = entity.FoodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FoodID = null;
					else entity.FoodID = Convert.ToString(value);
				}
			}
			public System.String SRMenuItemFoodGroup
			{
				get
				{
					System.String data = entity.SRMenuItemFoodGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMenuItemFoodGroup = null;
					else entity.SRMenuItemFoodGroup = Convert.ToString(value);
				}
			}
			public System.String IsOptional
			{
				get
				{
					System.Boolean? data = entity.IsOptional;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOptional = null;
					else entity.IsOptional = Convert.ToBoolean(value);
				}
			}
			public System.String IsStandard
			{
				get
				{
					System.Boolean? data = entity.IsStandard;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsStandard = null;
					else entity.IsStandard = Convert.ToBoolean(value);
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
			private esMenuItemFood entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMenuItemFoodQuery query)
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
				throw new Exception("esMenuItemFood can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MenuItemFood : esMenuItemFood
	{
	}

	[Serializable]
	abstract public class esMenuItemFoodQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MenuItemFoodMetadata.Meta();
			}
		}

		public esQueryItem MenuItemID
		{
			get
			{
				return new esQueryItem(this, MenuItemFoodMetadata.ColumnNames.MenuItemID, esSystemType.String);
			}
		}

		public esQueryItem SRMealSet
		{
			get
			{
				return new esQueryItem(this, MenuItemFoodMetadata.ColumnNames.SRMealSet, esSystemType.String);
			}
		}

		public esQueryItem FoodID
		{
			get
			{
				return new esQueryItem(this, MenuItemFoodMetadata.ColumnNames.FoodID, esSystemType.String);
			}
		}

		public esQueryItem SRMenuItemFoodGroup
		{
			get
			{
				return new esQueryItem(this, MenuItemFoodMetadata.ColumnNames.SRMenuItemFoodGroup, esSystemType.String);
			}
		}

		public esQueryItem IsOptional
		{
			get
			{
				return new esQueryItem(this, MenuItemFoodMetadata.ColumnNames.IsOptional, esSystemType.Boolean);
			}
		}

		public esQueryItem IsStandard
		{
			get
			{
				return new esQueryItem(this, MenuItemFoodMetadata.ColumnNames.IsStandard, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MenuItemFoodMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MenuItemFoodMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MenuItemFoodCollection")]
	public partial class MenuItemFoodCollection : esMenuItemFoodCollection, IEnumerable<MenuItemFood>
	{
		public MenuItemFoodCollection()
		{

		}

		public static implicit operator List<MenuItemFood>(MenuItemFoodCollection coll)
		{
			List<MenuItemFood> list = new List<MenuItemFood>();

			foreach (MenuItemFood emp in coll)
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
				return MenuItemFoodMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MenuItemFoodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MenuItemFood(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MenuItemFood();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MenuItemFoodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MenuItemFoodQuery();
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
		public bool Load(MenuItemFoodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MenuItemFood AddNew()
		{
			MenuItemFood entity = base.AddNewEntity() as MenuItemFood;

			return entity;
		}
		public MenuItemFood FindByPrimaryKey(String menuItemID, String sRMealSet, String foodID, String sRMenuItemFoodGroup)
		{
			return base.FindByPrimaryKey(menuItemID, sRMealSet, foodID, sRMenuItemFoodGroup) as MenuItemFood;
		}

		#region IEnumerable< MenuItemFood> Members

		IEnumerator<MenuItemFood> IEnumerable<MenuItemFood>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MenuItemFood;
			}
		}

		#endregion

		private MenuItemFoodQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MenuItemFood' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MenuItemFood ({MenuItemID, SRMealSet, FoodID, SRMenuItemFoodGroup})")]
	[Serializable]
	public partial class MenuItemFood : esMenuItemFood
	{
		public MenuItemFood()
		{
		}

		public MenuItemFood(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MenuItemFoodMetadata.Meta();
			}
		}

		override protected esMenuItemFoodQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MenuItemFoodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MenuItemFoodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MenuItemFoodQuery();
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
		public bool Load(MenuItemFoodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MenuItemFoodQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MenuItemFoodQuery : esMenuItemFoodQuery
	{
		public MenuItemFoodQuery()
		{

		}

		public MenuItemFoodQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MenuItemFoodQuery";
		}
	}

	[Serializable]
	public partial class MenuItemFoodMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MenuItemFoodMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MenuItemFoodMetadata.ColumnNames.MenuItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MenuItemFoodMetadata.PropertyNames.MenuItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(MenuItemFoodMetadata.ColumnNames.SRMealSet, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = MenuItemFoodMetadata.PropertyNames.SRMealSet;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(MenuItemFoodMetadata.ColumnNames.FoodID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = MenuItemFoodMetadata.PropertyNames.FoodID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(MenuItemFoodMetadata.ColumnNames.SRMenuItemFoodGroup, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = MenuItemFoodMetadata.PropertyNames.SRMenuItemFoodGroup;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(MenuItemFoodMetadata.ColumnNames.IsOptional, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MenuItemFoodMetadata.PropertyNames.IsOptional;
			_columns.Add(c);

			c = new esColumnMetadata(MenuItemFoodMetadata.ColumnNames.IsStandard, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MenuItemFoodMetadata.PropertyNames.IsStandard;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MenuItemFoodMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MenuItemFoodMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MenuItemFoodMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = MenuItemFoodMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public MenuItemFoodMetadata Meta()
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
			public const string MenuItemID = "MenuItemID";
			public const string SRMealSet = "SRMealSet";
			public const string FoodID = "FoodID";
			public const string SRMenuItemFoodGroup = "SRMenuItemFoodGroup";
			public const string IsOptional = "IsOptional";
			public const string IsStandard = "IsStandard";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string MenuItemID = "MenuItemID";
			public const string SRMealSet = "SRMealSet";
			public const string FoodID = "FoodID";
			public const string SRMenuItemFoodGroup = "SRMenuItemFoodGroup";
			public const string IsOptional = "IsOptional";
			public const string IsStandard = "IsStandard";
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
			lock (typeof(MenuItemFoodMetadata))
			{
				if (MenuItemFoodMetadata.mapDelegates == null)
				{
					MenuItemFoodMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MenuItemFoodMetadata.meta == null)
				{
					MenuItemFoodMetadata.meta = new MenuItemFoodMetadata();
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

				meta.AddTypeMap("MenuItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMealSet", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FoodID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMenuItemFoodGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOptional", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsStandard", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "MenuItemFood";
				meta.Destination = "MenuItemFood";
				meta.spInsert = "proc_MenuItemFoodInsert";
				meta.spUpdate = "proc_MenuItemFoodUpdate";
				meta.spDelete = "proc_MenuItemFoodDelete";
				meta.spLoadAll = "proc_MenuItemFoodLoadAll";
				meta.spLoadByPrimaryKey = "proc_MenuItemFoodLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MenuItemFoodMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
