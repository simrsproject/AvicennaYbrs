/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/26/2021 2:30:32 PM
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
	abstract public class esSanitationActivityResultCollection : esEntityCollectionWAuditLog
	{
		public esSanitationActivityResultCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "SanitationActivityResultCollection";
		}

		#region Query Logic
		protected void InitQuery(esSanitationActivityResultQuery query)
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
			this.InitQuery(query as esSanitationActivityResultQuery);
		}
		#endregion

		virtual public SanitationActivityResult DetachEntity(SanitationActivityResult entity)
		{
			return base.DetachEntity(entity) as SanitationActivityResult;
		}

		virtual public SanitationActivityResult AttachEntity(SanitationActivityResult entity)
		{
			return base.AttachEntity(entity) as SanitationActivityResult;
		}

		virtual public void Combine(SanitationActivityResultCollection collection)
		{
			base.Combine(collection);
		}

		new public SanitationActivityResult this[int index]
		{
			get
			{
				return base[index] as SanitationActivityResult;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SanitationActivityResult);
		}
	}

	[Serializable]
	abstract public class esSanitationActivityResult : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSanitationActivityResultQuery GetDynamicQuery()
		{
			return null;
		}

		public esSanitationActivityResult()
		{
		}

		public esSanitationActivityResult(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String orderNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String orderNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo);
		}

		private bool LoadByPrimaryKeyDynamic(String orderNo)
		{
			esSanitationActivityResultQuery query = this.GetDynamicQuery();
			query.Where(query.OrderNo == orderNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String orderNo)
		{
			esParameters parms = new esParameters();
			parms.Add("OrderNo", orderNo);
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
						case "OrderNo": this.str.OrderNo = (string)value; break;
						case "ResultDateTime": this.str.ResultDateTime = (string)value; break;
						case "Result": this.str.Result = (string)value; break;
						case "Summary": this.str.Summary = (string)value; break;
						case "Suggest": this.str.Suggest = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ResultDateTime":

							if (value == null || value is System.DateTime)
								this.ResultDateTime = (System.DateTime?)value;
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
		/// Maps to SanitationActivityResult.OrderNo
		/// </summary>
		virtual public System.String OrderNo
		{
			get
			{
				return base.GetSystemString(SanitationActivityResultMetadata.ColumnNames.OrderNo);
			}

			set
			{
				base.SetSystemString(SanitationActivityResultMetadata.ColumnNames.OrderNo, value);
			}
		}
		/// <summary>
		/// Maps to SanitationActivityResult.ResultDateTime
		/// </summary>
		virtual public System.DateTime? ResultDateTime
		{
			get
			{
				return base.GetSystemDateTime(SanitationActivityResultMetadata.ColumnNames.ResultDateTime);
			}

			set
			{
				base.SetSystemDateTime(SanitationActivityResultMetadata.ColumnNames.ResultDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SanitationActivityResult.Result
		/// </summary>
		virtual public System.String Result
		{
			get
			{
				return base.GetSystemString(SanitationActivityResultMetadata.ColumnNames.Result);
			}

			set
			{
				base.SetSystemString(SanitationActivityResultMetadata.ColumnNames.Result, value);
			}
		}
		/// <summary>
		/// Maps to SanitationActivityResult.Summary
		/// </summary>
		virtual public System.String Summary
		{
			get
			{
				return base.GetSystemString(SanitationActivityResultMetadata.ColumnNames.Summary);
			}

			set
			{
				base.SetSystemString(SanitationActivityResultMetadata.ColumnNames.Summary, value);
			}
		}
		/// <summary>
		/// Maps to SanitationActivityResult.Suggest
		/// </summary>
		virtual public System.String Suggest
		{
			get
			{
				return base.GetSystemString(SanitationActivityResultMetadata.ColumnNames.Suggest);
			}

			set
			{
				base.SetSystemString(SanitationActivityResultMetadata.ColumnNames.Suggest, value);
			}
		}
		/// <summary>
		/// Maps to SanitationActivityResult.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SanitationActivityResultMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(SanitationActivityResultMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SanitationActivityResult.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SanitationActivityResultMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(SanitationActivityResultMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSanitationActivityResult entity)
			{
				this.entity = entity;
			}
			public System.String OrderNo
			{
				get
				{
					System.String data = entity.OrderNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderNo = null;
					else entity.OrderNo = Convert.ToString(value);
				}
			}
			public System.String ResultDateTime
			{
				get
				{
					System.DateTime? data = entity.ResultDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultDateTime = null;
					else entity.ResultDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String Result
			{
				get
				{
					System.String data = entity.Result;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Result = null;
					else entity.Result = Convert.ToString(value);
				}
			}
			public System.String Summary
			{
				get
				{
					System.String data = entity.Summary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Summary = null;
					else entity.Summary = Convert.ToString(value);
				}
			}
			public System.String Suggest
			{
				get
				{
					System.String data = entity.Suggest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Suggest = null;
					else entity.Suggest = Convert.ToString(value);
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
			private esSanitationActivityResult entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSanitationActivityResultQuery query)
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
				throw new Exception("esSanitationActivityResult can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SanitationActivityResult : esSanitationActivityResult
	{
	}

	[Serializable]
	abstract public class esSanitationActivityResultQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return SanitationActivityResultMetadata.Meta();
			}
		}

		public esQueryItem OrderNo
		{
			get
			{
				return new esQueryItem(this, SanitationActivityResultMetadata.ColumnNames.OrderNo, esSystemType.String);
			}
		}

		public esQueryItem ResultDateTime
		{
			get
			{
				return new esQueryItem(this, SanitationActivityResultMetadata.ColumnNames.ResultDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem Result
		{
			get
			{
				return new esQueryItem(this, SanitationActivityResultMetadata.ColumnNames.Result, esSystemType.String);
			}
		}

		public esQueryItem Summary
		{
			get
			{
				return new esQueryItem(this, SanitationActivityResultMetadata.ColumnNames.Summary, esSystemType.String);
			}
		}

		public esQueryItem Suggest
		{
			get
			{
				return new esQueryItem(this, SanitationActivityResultMetadata.ColumnNames.Suggest, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SanitationActivityResultMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SanitationActivityResultMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SanitationActivityResultCollection")]
	public partial class SanitationActivityResultCollection : esSanitationActivityResultCollection, IEnumerable<SanitationActivityResult>
	{
		public SanitationActivityResultCollection()
		{

		}

		public static implicit operator List<SanitationActivityResult>(SanitationActivityResultCollection coll)
		{
			List<SanitationActivityResult> list = new List<SanitationActivityResult>();

			foreach (SanitationActivityResult emp in coll)
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
				return SanitationActivityResultMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationActivityResultQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SanitationActivityResult(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SanitationActivityResult();
		}

		#endregion

		[BrowsableAttribute(false)]
		public SanitationActivityResultQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationActivityResultQuery();
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
		public bool Load(SanitationActivityResultQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SanitationActivityResult AddNew()
		{
			SanitationActivityResult entity = base.AddNewEntity() as SanitationActivityResult;

			return entity;
		}
		public SanitationActivityResult FindByPrimaryKey(String orderNo)
		{
			return base.FindByPrimaryKey(orderNo) as SanitationActivityResult;
		}

		#region IEnumerable< SanitationActivityResult> Members

		IEnumerator<SanitationActivityResult> IEnumerable<SanitationActivityResult>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as SanitationActivityResult;
			}
		}

		#endregion

		private SanitationActivityResultQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SanitationActivityResult' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SanitationActivityResult ({OrderNo})")]
	[Serializable]
	public partial class SanitationActivityResult : esSanitationActivityResult
	{
		public SanitationActivityResult()
		{
		}

		public SanitationActivityResult(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SanitationActivityResultMetadata.Meta();
			}
		}

		override protected esSanitationActivityResultQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationActivityResultQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public SanitationActivityResultQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationActivityResultQuery();
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
		public bool Load(SanitationActivityResultQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private SanitationActivityResultQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SanitationActivityResultQuery : esSanitationActivityResultQuery
	{
		public SanitationActivityResultQuery()
		{

		}

		public SanitationActivityResultQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SanitationActivityResultQuery";
		}
	}

	[Serializable]
	public partial class SanitationActivityResultMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SanitationActivityResultMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SanitationActivityResultMetadata.ColumnNames.OrderNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationActivityResultMetadata.PropertyNames.OrderNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationActivityResultMetadata.ColumnNames.ResultDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationActivityResultMetadata.PropertyNames.ResultDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationActivityResultMetadata.ColumnNames.Result, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationActivityResultMetadata.PropertyNames.Result;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationActivityResultMetadata.ColumnNames.Summary, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationActivityResultMetadata.PropertyNames.Summary;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationActivityResultMetadata.ColumnNames.Suggest, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationActivityResultMetadata.PropertyNames.Suggest;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationActivityResultMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationActivityResultMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationActivityResultMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationActivityResultMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public SanitationActivityResultMetadata Meta()
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
			public const string OrderNo = "OrderNo";
			public const string ResultDateTime = "ResultDateTime";
			public const string Result = "Result";
			public const string Summary = "Summary";
			public const string Suggest = "Suggest";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string OrderNo = "OrderNo";
			public const string ResultDateTime = "ResultDateTime";
			public const string Result = "Result";
			public const string Summary = "Summary";
			public const string Suggest = "Suggest";
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
			lock (typeof(SanitationActivityResultMetadata))
			{
				if (SanitationActivityResultMetadata.mapDelegates == null)
				{
					SanitationActivityResultMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (SanitationActivityResultMetadata.meta == null)
				{
					SanitationActivityResultMetadata.meta = new SanitationActivityResultMetadata();
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

				meta.AddTypeMap("OrderNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResultDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Result", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Summary", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Suggest", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "SanitationActivityResult";
				meta.Destination = "SanitationActivityResult";
				meta.spInsert = "proc_SanitationActivityResultInsert";
				meta.spUpdate = "proc_SanitationActivityResultUpdate";
				meta.spDelete = "proc_SanitationActivityResultDelete";
				meta.spLoadAll = "proc_SanitationActivityResultLoadAll";
				meta.spLoadByPrimaryKey = "proc_SanitationActivityResultLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SanitationActivityResultMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
