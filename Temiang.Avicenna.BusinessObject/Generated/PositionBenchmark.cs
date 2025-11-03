/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/6/2020 1:23:59 PM
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
	abstract public class esPositionBenchmarkCollection : esEntityCollectionWAuditLog
	{
		public esPositionBenchmarkCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PositionBenchmarkCollection";
		}

		#region Query Logic
		protected void InitQuery(esPositionBenchmarkQuery query)
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
			this.InitQuery(query as esPositionBenchmarkQuery);
		}
		#endregion

		virtual public PositionBenchmark DetachEntity(PositionBenchmark entity)
		{
			return base.DetachEntity(entity) as PositionBenchmark;
		}

		virtual public PositionBenchmark AttachEntity(PositionBenchmark entity)
		{
			return base.AttachEntity(entity) as PositionBenchmark;
		}

		virtual public void Combine(PositionBenchmarkCollection collection)
		{
			base.Combine(collection);
		}

		new public PositionBenchmark this[int index]
		{
			get
			{
				return base[index] as PositionBenchmark;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PositionBenchmark);
		}
	}

	[Serializable]
	abstract public class esPositionBenchmark : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPositionBenchmarkQuery GetDynamicQuery()
		{
			return null;
		}

		public esPositionBenchmark()
		{
		}

		public esPositionBenchmark(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 positionBenchmarkID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionBenchmarkID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionBenchmarkID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 positionBenchmarkID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionBenchmarkID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionBenchmarkID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 positionBenchmarkID)
		{
			esPositionBenchmarkQuery query = this.GetDynamicQuery();
			query.Where(query.PositionBenchmarkID == positionBenchmarkID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 positionBenchmarkID)
		{
			esParameters parms = new esParameters();
			parms.Add("PositionBenchmarkID", positionBenchmarkID);
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
						case "PositionBenchmarkID": this.str.PositionBenchmarkID = (string)value; break;
						case "PositionID": this.str.PositionID = (string)value; break;
						case "BenchmarkName": this.str.BenchmarkName = (string)value; break;
						case "Description": this.str.Description = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PositionBenchmarkID":

							if (value == null || value is System.Int32)
								this.PositionBenchmarkID = (System.Int32?)value;
							break;
						case "PositionID":

							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
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
		/// Maps to PositionBenchmark.PositionBenchmarkID
		/// </summary>
		virtual public System.Int32? PositionBenchmarkID
		{
			get
			{
				return base.GetSystemInt32(PositionBenchmarkMetadata.ColumnNames.PositionBenchmarkID);
			}

			set
			{
				base.SetSystemInt32(PositionBenchmarkMetadata.ColumnNames.PositionBenchmarkID, value);
			}
		}
		/// <summary>
		/// Maps to PositionBenchmark.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(PositionBenchmarkMetadata.ColumnNames.PositionID);
			}

			set
			{
				base.SetSystemInt32(PositionBenchmarkMetadata.ColumnNames.PositionID, value);
			}
		}
		/// <summary>
		/// Maps to PositionBenchmark.BenchmarkName
		/// </summary>
		virtual public System.String BenchmarkName
		{
			get
			{
				return base.GetSystemString(PositionBenchmarkMetadata.ColumnNames.BenchmarkName);
			}

			set
			{
				base.SetSystemString(PositionBenchmarkMetadata.ColumnNames.BenchmarkName, value);
			}
		}
		/// <summary>
		/// Maps to PositionBenchmark.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(PositionBenchmarkMetadata.ColumnNames.Description);
			}

			set
			{
				base.SetSystemString(PositionBenchmarkMetadata.ColumnNames.Description, value);
			}
		}
		/// <summary>
		/// Maps to PositionBenchmark.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PositionBenchmarkMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PositionBenchmarkMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PositionBenchmark.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PositionBenchmarkMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PositionBenchmarkMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPositionBenchmark entity)
			{
				this.entity = entity;
			}
			public System.String PositionBenchmarkID
			{
				get
				{
					System.Int32? data = entity.PositionBenchmarkID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionBenchmarkID = null;
					else entity.PositionBenchmarkID = Convert.ToInt32(value);
				}
			}
			public System.String PositionID
			{
				get
				{
					System.Int32? data = entity.PositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionID = null;
					else entity.PositionID = Convert.ToInt32(value);
				}
			}
			public System.String BenchmarkName
			{
				get
				{
					System.String data = entity.BenchmarkName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BenchmarkName = null;
					else entity.BenchmarkName = Convert.ToString(value);
				}
			}
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
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
			private esPositionBenchmark entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPositionBenchmarkQuery query)
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
				throw new Exception("esPositionBenchmark can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PositionBenchmark : esPositionBenchmark
	{
	}

	[Serializable]
	abstract public class esPositionBenchmarkQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PositionBenchmarkMetadata.Meta();
			}
		}

		public esQueryItem PositionBenchmarkID
		{
			get
			{
				return new esQueryItem(this, PositionBenchmarkMetadata.ColumnNames.PositionBenchmarkID, esSystemType.Int32);
			}
		}

		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, PositionBenchmarkMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		}

		public esQueryItem BenchmarkName
		{
			get
			{
				return new esQueryItem(this, PositionBenchmarkMetadata.ColumnNames.BenchmarkName, esSystemType.String);
			}
		}

		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, PositionBenchmarkMetadata.ColumnNames.Description, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PositionBenchmarkMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PositionBenchmarkMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PositionBenchmarkCollection")]
	public partial class PositionBenchmarkCollection : esPositionBenchmarkCollection, IEnumerable<PositionBenchmark>
	{
		public PositionBenchmarkCollection()
		{

		}

		public static implicit operator List<PositionBenchmark>(PositionBenchmarkCollection coll)
		{
			List<PositionBenchmark> list = new List<PositionBenchmark>();

			foreach (PositionBenchmark emp in coll)
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
				return PositionBenchmarkMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionBenchmarkQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PositionBenchmark(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PositionBenchmark();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PositionBenchmarkQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionBenchmarkQuery();
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
		public bool Load(PositionBenchmarkQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PositionBenchmark AddNew()
		{
			PositionBenchmark entity = base.AddNewEntity() as PositionBenchmark;

			return entity;
		}
		public PositionBenchmark FindByPrimaryKey(Int32 positionBenchmarkID)
		{
			return base.FindByPrimaryKey(positionBenchmarkID) as PositionBenchmark;
		}

		#region IEnumerable< PositionBenchmark> Members

		IEnumerator<PositionBenchmark> IEnumerable<PositionBenchmark>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PositionBenchmark;
			}
		}

		#endregion

		private PositionBenchmarkQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PositionBenchmark' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PositionBenchmark ({PositionBenchmarkID})")]
	[Serializable]
	public partial class PositionBenchmark : esPositionBenchmark
	{
		public PositionBenchmark()
		{
		}

		public PositionBenchmark(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PositionBenchmarkMetadata.Meta();
			}
		}

		override protected esPositionBenchmarkQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionBenchmarkQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PositionBenchmarkQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionBenchmarkQuery();
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
		public bool Load(PositionBenchmarkQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PositionBenchmarkQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PositionBenchmarkQuery : esPositionBenchmarkQuery
	{
		public PositionBenchmarkQuery()
		{

		}

		public PositionBenchmarkQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PositionBenchmarkQuery";
		}
	}

	[Serializable]
	public partial class PositionBenchmarkMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PositionBenchmarkMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PositionBenchmarkMetadata.ColumnNames.PositionBenchmarkID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionBenchmarkMetadata.PropertyNames.PositionBenchmarkID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PositionBenchmarkMetadata.ColumnNames.PositionID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionBenchmarkMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PositionBenchmarkMetadata.ColumnNames.BenchmarkName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionBenchmarkMetadata.PropertyNames.BenchmarkName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(PositionBenchmarkMetadata.ColumnNames.Description, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionBenchmarkMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PositionBenchmarkMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PositionBenchmarkMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(PositionBenchmarkMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionBenchmarkMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);


		}
		#endregion

		static public PositionBenchmarkMetadata Meta()
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
			public const string PositionBenchmarkID = "PositionBenchmarkID";
			public const string PositionID = "PositionID";
			public const string BenchmarkName = "BenchmarkName";
			public const string Description = "Description";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PositionBenchmarkID = "PositionBenchmarkID";
			public const string PositionID = "PositionID";
			public const string BenchmarkName = "BenchmarkName";
			public const string Description = "Description";
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
			lock (typeof(PositionBenchmarkMetadata))
			{
				if (PositionBenchmarkMetadata.mapDelegates == null)
				{
					PositionBenchmarkMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PositionBenchmarkMetadata.meta == null)
				{
					PositionBenchmarkMetadata.meta = new PositionBenchmarkMetadata();
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

				meta.AddTypeMap("PositionBenchmarkID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("BenchmarkName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PositionBenchmark";
				meta.Destination = "PositionBenchmark";
				meta.spInsert = "proc_PositionBenchmarkInsert";
				meta.spUpdate = "proc_PositionBenchmarkUpdate";
				meta.spDelete = "proc_PositionBenchmarkDelete";
				meta.spLoadAll = "proc_PositionBenchmarkLoadAll";
				meta.spLoadByPrimaryKey = "proc_PositionBenchmarkLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PositionBenchmarkMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
