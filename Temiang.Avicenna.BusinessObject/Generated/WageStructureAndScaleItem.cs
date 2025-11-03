/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/7/2022 3:42:31 PM
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
	abstract public class esWageStructureAndScaleItemCollection : esEntityCollectionWAuditLog
	{
		public esWageStructureAndScaleItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "WageStructureAndScaleItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esWageStructureAndScaleItemQuery query)
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
			this.InitQuery(query as esWageStructureAndScaleItemQuery);
		}
		#endregion

		virtual public WageStructureAndScaleItem DetachEntity(WageStructureAndScaleItem entity)
		{
			return base.DetachEntity(entity) as WageStructureAndScaleItem;
		}

		virtual public WageStructureAndScaleItem AttachEntity(WageStructureAndScaleItem entity)
		{
			return base.AttachEntity(entity) as WageStructureAndScaleItem;
		}

		virtual public void Combine(WageStructureAndScaleItemCollection collection)
		{
			base.Combine(collection);
		}

		new public WageStructureAndScaleItem this[int index]
		{
			get
			{
				return base[index] as WageStructureAndScaleItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(WageStructureAndScaleItem);
		}
	}

	[Serializable]
	abstract public class esWageStructureAndScaleItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esWageStructureAndScaleItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esWageStructureAndScaleItem()
		{
		}

		public esWageStructureAndScaleItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 wageStructureAndScaleItemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(wageStructureAndScaleItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(wageStructureAndScaleItemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 wageStructureAndScaleItemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(wageStructureAndScaleItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(wageStructureAndScaleItemID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 wageStructureAndScaleItemID)
		{
			esWageStructureAndScaleItemQuery query = this.GetDynamicQuery();
			query.Where(query.WageStructureAndScaleItemID == wageStructureAndScaleItemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 wageStructureAndScaleItemID)
		{
			esParameters parms = new esParameters();
			parms.Add("WageStructureAndScaleItemID", wageStructureAndScaleItemID);
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
						case "WageStructureAndScaleItemID": this.str.WageStructureAndScaleItemID = (string)value; break;
						case "WageStructureAndScaleID": this.str.WageStructureAndScaleID = (string)value; break;
						case "SRWageStructureAndScaleItem": this.str.SRWageStructureAndScaleItem = (string)value; break;
						case "Points": this.str.Points = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "WageStructureAndScaleItemID":

							if (value == null || value is System.Int32)
								this.WageStructureAndScaleItemID = (System.Int32?)value;
							break;
						case "WageStructureAndScaleID":

							if (value == null || value is System.Int32)
								this.WageStructureAndScaleID = (System.Int32?)value;
							break;
						case "Points":

							if (value == null || value is System.Decimal)
								this.Points = (System.Decimal?)value;
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
		/// Maps to WageStructureAndScaleItem.WageStructureAndScaleItemID
		/// </summary>
		virtual public System.Int32? WageStructureAndScaleItemID
		{
			get
			{
				return base.GetSystemInt32(WageStructureAndScaleItemMetadata.ColumnNames.WageStructureAndScaleItemID);
			}

			set
			{
				base.SetSystemInt32(WageStructureAndScaleItemMetadata.ColumnNames.WageStructureAndScaleItemID, value);
			}
		}
		/// <summary>
		/// Maps to WageStructureAndScaleItem.WageStructureAndScaleID
		/// </summary>
		virtual public System.Int32? WageStructureAndScaleID
		{
			get
			{
				return base.GetSystemInt32(WageStructureAndScaleItemMetadata.ColumnNames.WageStructureAndScaleID);
			}

			set
			{
				base.SetSystemInt32(WageStructureAndScaleItemMetadata.ColumnNames.WageStructureAndScaleID, value);
			}
		}
		/// <summary>
		/// Maps to WageStructureAndScaleItem.SRWageStructureAndScaleItem
		/// </summary>
		virtual public System.String SRWageStructureAndScaleItem
		{
			get
			{
				return base.GetSystemString(WageStructureAndScaleItemMetadata.ColumnNames.SRWageStructureAndScaleItem);
			}

			set
			{
				base.SetSystemString(WageStructureAndScaleItemMetadata.ColumnNames.SRWageStructureAndScaleItem, value);
			}
		}
		/// <summary>
		/// Maps to WageStructureAndScaleItem.Points
		/// </summary>
		virtual public System.Decimal? Points
		{
			get
			{
				return base.GetSystemDecimal(WageStructureAndScaleItemMetadata.ColumnNames.Points);
			}

			set
			{
				base.SetSystemDecimal(WageStructureAndScaleItemMetadata.ColumnNames.Points, value);
			}
		}
		/// <summary>
		/// Maps to WageStructureAndScaleItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(WageStructureAndScaleItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(WageStructureAndScaleItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to WageStructureAndScaleItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(WageStructureAndScaleItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(WageStructureAndScaleItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esWageStructureAndScaleItem entity)
			{
				this.entity = entity;
			}
			public System.String WageStructureAndScaleItemID
			{
				get
				{
					System.Int32? data = entity.WageStructureAndScaleItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WageStructureAndScaleItemID = null;
					else entity.WageStructureAndScaleItemID = Convert.ToInt32(value);
				}
			}
			public System.String WageStructureAndScaleID
			{
				get
				{
					System.Int32? data = entity.WageStructureAndScaleID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WageStructureAndScaleID = null;
					else entity.WageStructureAndScaleID = Convert.ToInt32(value);
				}
			}
			public System.String SRWageStructureAndScaleItem
			{
				get
				{
					System.String data = entity.SRWageStructureAndScaleItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRWageStructureAndScaleItem = null;
					else entity.SRWageStructureAndScaleItem = Convert.ToString(value);
				}
			}
			public System.String Points
			{
				get
				{
					System.Decimal? data = entity.Points;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Points = null;
					else entity.Points = Convert.ToDecimal(value);
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
			private esWageStructureAndScaleItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esWageStructureAndScaleItemQuery query)
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
				throw new Exception("esWageStructureAndScaleItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class WageStructureAndScaleItem : esWageStructureAndScaleItem
	{
	}

	[Serializable]
	abstract public class esWageStructureAndScaleItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return WageStructureAndScaleItemMetadata.Meta();
			}
		}

		public esQueryItem WageStructureAndScaleItemID
		{
			get
			{
				return new esQueryItem(this, WageStructureAndScaleItemMetadata.ColumnNames.WageStructureAndScaleItemID, esSystemType.Int32);
			}
		}

		public esQueryItem WageStructureAndScaleID
		{
			get
			{
				return new esQueryItem(this, WageStructureAndScaleItemMetadata.ColumnNames.WageStructureAndScaleID, esSystemType.Int32);
			}
		}

		public esQueryItem SRWageStructureAndScaleItem
		{
			get
			{
				return new esQueryItem(this, WageStructureAndScaleItemMetadata.ColumnNames.SRWageStructureAndScaleItem, esSystemType.String);
			}
		}

		public esQueryItem Points
		{
			get
			{
				return new esQueryItem(this, WageStructureAndScaleItemMetadata.ColumnNames.Points, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, WageStructureAndScaleItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, WageStructureAndScaleItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("WageStructureAndScaleItemCollection")]
	public partial class WageStructureAndScaleItemCollection : esWageStructureAndScaleItemCollection, IEnumerable<WageStructureAndScaleItem>
	{
		public WageStructureAndScaleItemCollection()
		{

		}

		public static implicit operator List<WageStructureAndScaleItem>(WageStructureAndScaleItemCollection coll)
		{
			List<WageStructureAndScaleItem> list = new List<WageStructureAndScaleItem>();

			foreach (WageStructureAndScaleItem emp in coll)
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
				return WageStructureAndScaleItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WageStructureAndScaleItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new WageStructureAndScaleItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new WageStructureAndScaleItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public WageStructureAndScaleItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WageStructureAndScaleItemQuery();
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
		public bool Load(WageStructureAndScaleItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public WageStructureAndScaleItem AddNew()
		{
			WageStructureAndScaleItem entity = base.AddNewEntity() as WageStructureAndScaleItem;

			return entity;
		}
		public WageStructureAndScaleItem FindByPrimaryKey(Int32 wageStructureAndScaleItemID)
		{
			return base.FindByPrimaryKey(wageStructureAndScaleItemID) as WageStructureAndScaleItem;
		}

		#region IEnumerable< WageStructureAndScaleItem> Members

		IEnumerator<WageStructureAndScaleItem> IEnumerable<WageStructureAndScaleItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as WageStructureAndScaleItem;
			}
		}

		#endregion

		private WageStructureAndScaleItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'WageStructureAndScaleItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("WageStructureAndScaleItem ({WageStructureAndScaleItemID})")]
	[Serializable]
	public partial class WageStructureAndScaleItem : esWageStructureAndScaleItem
	{
		public WageStructureAndScaleItem()
		{
		}

		public WageStructureAndScaleItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return WageStructureAndScaleItemMetadata.Meta();
			}
		}

		override protected esWageStructureAndScaleItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WageStructureAndScaleItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public WageStructureAndScaleItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WageStructureAndScaleItemQuery();
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
		public bool Load(WageStructureAndScaleItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private WageStructureAndScaleItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class WageStructureAndScaleItemQuery : esWageStructureAndScaleItemQuery
	{
		public WageStructureAndScaleItemQuery()
		{

		}

		public WageStructureAndScaleItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "WageStructureAndScaleItemQuery";
		}
	}

	[Serializable]
	public partial class WageStructureAndScaleItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected WageStructureAndScaleItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(WageStructureAndScaleItemMetadata.ColumnNames.WageStructureAndScaleItemID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WageStructureAndScaleItemMetadata.PropertyNames.WageStructureAndScaleItemID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(WageStructureAndScaleItemMetadata.ColumnNames.WageStructureAndScaleID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WageStructureAndScaleItemMetadata.PropertyNames.WageStructureAndScaleID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(WageStructureAndScaleItemMetadata.ColumnNames.SRWageStructureAndScaleItem, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = WageStructureAndScaleItemMetadata.PropertyNames.SRWageStructureAndScaleItem;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(WageStructureAndScaleItemMetadata.ColumnNames.Points, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = WageStructureAndScaleItemMetadata.PropertyNames.Points;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(WageStructureAndScaleItemMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WageStructureAndScaleItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WageStructureAndScaleItemMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = WageStructureAndScaleItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public WageStructureAndScaleItemMetadata Meta()
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
			public const string WageStructureAndScaleItemID = "WageStructureAndScaleItemID";
			public const string WageStructureAndScaleID = "WageStructureAndScaleID";
			public const string SRWageStructureAndScaleItem = "SRWageStructureAndScaleItem";
			public const string Points = "Points";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string WageStructureAndScaleItemID = "WageStructureAndScaleItemID";
			public const string WageStructureAndScaleID = "WageStructureAndScaleID";
			public const string SRWageStructureAndScaleItem = "SRWageStructureAndScaleItem";
			public const string Points = "Points";
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
			lock (typeof(WageStructureAndScaleItemMetadata))
			{
				if (WageStructureAndScaleItemMetadata.mapDelegates == null)
				{
					WageStructureAndScaleItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (WageStructureAndScaleItemMetadata.meta == null)
				{
					WageStructureAndScaleItemMetadata.meta = new WageStructureAndScaleItemMetadata();
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

				meta.AddTypeMap("WageStructureAndScaleItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WageStructureAndScaleID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRWageStructureAndScaleItem", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Points", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "WageStructureAndScaleItem";
				meta.Destination = "WageStructureAndScaleItem";
				meta.spInsert = "proc_WageStructureAndScaleItemInsert";
				meta.spUpdate = "proc_WageStructureAndScaleItemUpdate";
				meta.spDelete = "proc_WageStructureAndScaleItemDelete";
				meta.spLoadAll = "proc_WageStructureAndScaleItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_WageStructureAndScaleItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private WageStructureAndScaleItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
