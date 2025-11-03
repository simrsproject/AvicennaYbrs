/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/29/2022 7:46:49 PM
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
	abstract public class esPositionWorkResultCollection : esEntityCollectionWAuditLog
	{
		public esPositionWorkResultCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PositionWorkResultCollection";
		}

		#region Query Logic
		protected void InitQuery(esPositionWorkResultQuery query)
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
			this.InitQuery(query as esPositionWorkResultQuery);
		}
		#endregion

		virtual public PositionWorkResult DetachEntity(PositionWorkResult entity)
		{
			return base.DetachEntity(entity) as PositionWorkResult;
		}

		virtual public PositionWorkResult AttachEntity(PositionWorkResult entity)
		{
			return base.AttachEntity(entity) as PositionWorkResult;
		}

		virtual public void Combine(PositionWorkResultCollection collection)
		{
			base.Combine(collection);
		}

		new public PositionWorkResult this[int index]
		{
			get
			{
				return base[index] as PositionWorkResult;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PositionWorkResult);
		}
	}

	[Serializable]
	abstract public class esPositionWorkResult : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPositionWorkResultQuery GetDynamicQuery()
		{
			return null;
		}

		public esPositionWorkResult()
		{
		}

		public esPositionWorkResult(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 positionWorkResultID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionWorkResultID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionWorkResultID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 positionWorkResultID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionWorkResultID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionWorkResultID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 positionWorkResultID)
		{
			esPositionWorkResultQuery query = this.GetDynamicQuery();
			query.Where(query.PositionWorkResultID == positionWorkResultID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 positionWorkResultID)
		{
			esParameters parms = new esParameters();
			parms.Add("PositionWorkResultID", positionWorkResultID);
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
						case "PositionWorkResultID": this.str.PositionWorkResultID = (string)value; break;
						case "PositionID": this.str.PositionID = (string)value; break;
						case "WorkResultName": this.str.WorkResultName = (string)value; break;
						case "Description": this.str.Description = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PositionWorkResultID":

							if (value == null || value is System.Int32)
								this.PositionWorkResultID = (System.Int32?)value;
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
		/// Maps to PositionWorkResult.PositionWorkResultID
		/// </summary>
		virtual public System.Int32? PositionWorkResultID
		{
			get
			{
				return base.GetSystemInt32(PositionWorkResultMetadata.ColumnNames.PositionWorkResultID);
			}

			set
			{
				base.SetSystemInt32(PositionWorkResultMetadata.ColumnNames.PositionWorkResultID, value);
			}
		}
		/// <summary>
		/// Maps to PositionWorkResult.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(PositionWorkResultMetadata.ColumnNames.PositionID);
			}

			set
			{
				base.SetSystemInt32(PositionWorkResultMetadata.ColumnNames.PositionID, value);
			}
		}
		/// <summary>
		/// Maps to PositionWorkResult.WorkResultName
		/// </summary>
		virtual public System.String WorkResultName
		{
			get
			{
				return base.GetSystemString(PositionWorkResultMetadata.ColumnNames.WorkResultName);
			}

			set
			{
				base.SetSystemString(PositionWorkResultMetadata.ColumnNames.WorkResultName, value);
			}
		}
		/// <summary>
		/// Maps to PositionWorkResult.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(PositionWorkResultMetadata.ColumnNames.Description);
			}

			set
			{
				base.SetSystemString(PositionWorkResultMetadata.ColumnNames.Description, value);
			}
		}
		/// <summary>
		/// Maps to PositionWorkResult.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PositionWorkResultMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PositionWorkResultMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PositionWorkResult.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PositionWorkResultMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PositionWorkResultMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPositionWorkResult entity)
			{
				this.entity = entity;
			}
			public System.String PositionWorkResultID
			{
				get
				{
					System.Int32? data = entity.PositionWorkResultID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionWorkResultID = null;
					else entity.PositionWorkResultID = Convert.ToInt32(value);
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
			public System.String WorkResultName
			{
				get
				{
					System.String data = entity.WorkResultName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkResultName = null;
					else entity.WorkResultName = Convert.ToString(value);
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
			private esPositionWorkResult entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPositionWorkResultQuery query)
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
				throw new Exception("esPositionWorkResult can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PositionWorkResult : esPositionWorkResult
	{
	}

	[Serializable]
	abstract public class esPositionWorkResultQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PositionWorkResultMetadata.Meta();
			}
		}

		public esQueryItem PositionWorkResultID
		{
			get
			{
				return new esQueryItem(this, PositionWorkResultMetadata.ColumnNames.PositionWorkResultID, esSystemType.Int32);
			}
		}

		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, PositionWorkResultMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		}

		public esQueryItem WorkResultName
		{
			get
			{
				return new esQueryItem(this, PositionWorkResultMetadata.ColumnNames.WorkResultName, esSystemType.String);
			}
		}

		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, PositionWorkResultMetadata.ColumnNames.Description, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PositionWorkResultMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PositionWorkResultMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PositionWorkResultCollection")]
	public partial class PositionWorkResultCollection : esPositionWorkResultCollection, IEnumerable<PositionWorkResult>
	{
		public PositionWorkResultCollection()
		{

		}

		public static implicit operator List<PositionWorkResult>(PositionWorkResultCollection coll)
		{
			List<PositionWorkResult> list = new List<PositionWorkResult>();

			foreach (PositionWorkResult emp in coll)
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
				return PositionWorkResultMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionWorkResultQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PositionWorkResult(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PositionWorkResult();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PositionWorkResultQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionWorkResultQuery();
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
		public bool Load(PositionWorkResultQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PositionWorkResult AddNew()
		{
			PositionWorkResult entity = base.AddNewEntity() as PositionWorkResult;

			return entity;
		}
		public PositionWorkResult FindByPrimaryKey(Int32 positionWorkResultID)
		{
			return base.FindByPrimaryKey(positionWorkResultID) as PositionWorkResult;
		}

		#region IEnumerable< PositionWorkResult> Members

		IEnumerator<PositionWorkResult> IEnumerable<PositionWorkResult>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PositionWorkResult;
			}
		}

		#endregion

		private PositionWorkResultQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PositionWorkResult' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PositionWorkResult ({PositionWorkResultID})")]
	[Serializable]
	public partial class PositionWorkResult : esPositionWorkResult
	{
		public PositionWorkResult()
		{
		}

		public PositionWorkResult(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PositionWorkResultMetadata.Meta();
			}
		}

		override protected esPositionWorkResultQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionWorkResultQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PositionWorkResultQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionWorkResultQuery();
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
		public bool Load(PositionWorkResultQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PositionWorkResultQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PositionWorkResultQuery : esPositionWorkResultQuery
	{
		public PositionWorkResultQuery()
		{

		}

		public PositionWorkResultQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PositionWorkResultQuery";
		}
	}

	[Serializable]
	public partial class PositionWorkResultMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PositionWorkResultMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PositionWorkResultMetadata.ColumnNames.PositionWorkResultID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionWorkResultMetadata.PropertyNames.PositionWorkResultID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PositionWorkResultMetadata.ColumnNames.PositionID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionWorkResultMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PositionWorkResultMetadata.ColumnNames.WorkResultName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionWorkResultMetadata.PropertyNames.WorkResultName;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(PositionWorkResultMetadata.ColumnNames.Description, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionWorkResultMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PositionWorkResultMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PositionWorkResultMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PositionWorkResultMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionWorkResultMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PositionWorkResultMetadata Meta()
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
			public const string PositionWorkResultID = "PositionWorkResultID";
			public const string PositionID = "PositionID";
			public const string WorkResultName = "WorkResultName";
			public const string Description = "Description";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PositionWorkResultID = "PositionWorkResultID";
			public const string PositionID = "PositionID";
			public const string WorkResultName = "WorkResultName";
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
			lock (typeof(PositionWorkResultMetadata))
			{
				if (PositionWorkResultMetadata.mapDelegates == null)
				{
					PositionWorkResultMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PositionWorkResultMetadata.meta == null)
				{
					PositionWorkResultMetadata.meta = new PositionWorkResultMetadata();
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

				meta.AddTypeMap("PositionWorkResultID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkResultName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PositionWorkResult";
				meta.Destination = "PositionWorkResult";
				meta.spInsert = "proc_PositionWorkResultInsert";
				meta.spUpdate = "proc_PositionWorkResultUpdate";
				meta.spDelete = "proc_PositionWorkResultDelete";
				meta.spLoadAll = "proc_PositionWorkResultLoadAll";
				meta.spLoadByPrimaryKey = "proc_PositionWorkResultLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PositionWorkResultMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
