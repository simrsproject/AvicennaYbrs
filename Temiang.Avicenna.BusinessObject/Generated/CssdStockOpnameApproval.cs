/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/4/2023 6:06:19 PM
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
	abstract public class esCssdStockOpnameApprovalCollection : esEntityCollectionWAuditLog
	{
		public esCssdStockOpnameApprovalCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CssdStockOpnameApprovalCollection";
		}

		#region Query Logic
		protected void InitQuery(esCssdStockOpnameApprovalQuery query)
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
			this.InitQuery(query as esCssdStockOpnameApprovalQuery);
		}
		#endregion

		virtual public CssdStockOpnameApproval DetachEntity(CssdStockOpnameApproval entity)
		{
			return base.DetachEntity(entity) as CssdStockOpnameApproval;
		}

		virtual public CssdStockOpnameApproval AttachEntity(CssdStockOpnameApproval entity)
		{
			return base.AttachEntity(entity) as CssdStockOpnameApproval;
		}

		virtual public void Combine(CssdStockOpnameApprovalCollection collection)
		{
			base.Combine(collection);
		}

		new public CssdStockOpnameApproval this[int index]
		{
			get
			{
				return base[index] as CssdStockOpnameApproval;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CssdStockOpnameApproval);
		}
	}

	[Serializable]
	abstract public class esCssdStockOpnameApproval : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCssdStockOpnameApprovalQuery GetDynamicQuery()
		{
			return null;
		}

		public esCssdStockOpnameApproval()
		{
		}

		public esCssdStockOpnameApproval(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, Int32 pageNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, pageNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, pageNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, Int32 pageNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, pageNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, pageNo);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, Int32 pageNo)
		{
			esCssdStockOpnameApprovalQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.PageNo == pageNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, Int32 pageNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("PageNo", pageNo);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "PageNo": this.str.PageNo = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PageNo":

							if (value == null || value is System.Int32)
								this.PageNo = (System.Int32?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDateTime":

							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
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
		/// Maps to CssdStockOpnameApproval.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(CssdStockOpnameApprovalMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(CssdStockOpnameApprovalMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameApproval.PageNo
		/// </summary>
		virtual public System.Int32? PageNo
		{
			get
			{
				return base.GetSystemInt32(CssdStockOpnameApprovalMetadata.ColumnNames.PageNo);
			}

			set
			{
				base.SetSystemInt32(CssdStockOpnameApprovalMetadata.ColumnNames.PageNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameApproval.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(CssdStockOpnameApprovalMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(CssdStockOpnameApprovalMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameApproval.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdStockOpnameApprovalMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdStockOpnameApprovalMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameApproval.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(CssdStockOpnameApprovalMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(CssdStockOpnameApprovalMetadata.ColumnNames.ApprovedByUserID, value);
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
			public esStrings(esCssdStockOpnameApproval entity)
			{
				this.entity = entity;
			}
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
				}
			}
			public System.String PageNo
			{
				get
				{
					System.Int32? data = entity.PageNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PageNo = null;
					else entity.PageNo = Convert.ToInt32(value);
				}
			}
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
				}
			}
			public System.String ApprovedDateTime
			{
				get
				{
					System.DateTime? data = entity.ApprovedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
					else entity.ApprovedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ApprovedByUserID
			{
				get
				{
					System.String data = entity.ApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
					else entity.ApprovedByUserID = Convert.ToString(value);
				}
			}
			private esCssdStockOpnameApproval entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCssdStockOpnameApprovalQuery query)
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
				throw new Exception("esCssdStockOpnameApproval can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CssdStockOpnameApproval : esCssdStockOpnameApproval
	{
	}

	[Serializable]
	abstract public class esCssdStockOpnameApprovalQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CssdStockOpnameApprovalMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameApprovalMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem PageNo
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameApprovalMetadata.ColumnNames.PageNo, esSystemType.Int32);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameApprovalMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameApprovalMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameApprovalMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CssdStockOpnameApprovalCollection")]
	public partial class CssdStockOpnameApprovalCollection : esCssdStockOpnameApprovalCollection, IEnumerable<CssdStockOpnameApproval>
	{
		public CssdStockOpnameApprovalCollection()
		{

		}

		public static implicit operator List<CssdStockOpnameApproval>(CssdStockOpnameApprovalCollection coll)
		{
			List<CssdStockOpnameApproval> list = new List<CssdStockOpnameApproval>();

			foreach (CssdStockOpnameApproval emp in coll)
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
				return CssdStockOpnameApprovalMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdStockOpnameApprovalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CssdStockOpnameApproval(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CssdStockOpnameApproval();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CssdStockOpnameApprovalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdStockOpnameApprovalQuery();
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
		public bool Load(CssdStockOpnameApprovalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CssdStockOpnameApproval AddNew()
		{
			CssdStockOpnameApproval entity = base.AddNewEntity() as CssdStockOpnameApproval;

			return entity;
		}
		public CssdStockOpnameApproval FindByPrimaryKey(String transactionNo, Int32 pageNo)
		{
			return base.FindByPrimaryKey(transactionNo, pageNo) as CssdStockOpnameApproval;
		}

		#region IEnumerable< CssdStockOpnameApproval> Members

		IEnumerator<CssdStockOpnameApproval> IEnumerable<CssdStockOpnameApproval>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CssdStockOpnameApproval;
			}
		}

		#endregion

		private CssdStockOpnameApprovalQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CssdStockOpnameApproval' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CssdStockOpnameApproval ({TransactionNo, PageNo})")]
	[Serializable]
	public partial class CssdStockOpnameApproval : esCssdStockOpnameApproval
	{
		public CssdStockOpnameApproval()
		{
		}

		public CssdStockOpnameApproval(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CssdStockOpnameApprovalMetadata.Meta();
			}
		}

		override protected esCssdStockOpnameApprovalQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdStockOpnameApprovalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CssdStockOpnameApprovalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdStockOpnameApprovalQuery();
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
		public bool Load(CssdStockOpnameApprovalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CssdStockOpnameApprovalQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CssdStockOpnameApprovalQuery : esCssdStockOpnameApprovalQuery
	{
		public CssdStockOpnameApprovalQuery()
		{

		}

		public CssdStockOpnameApprovalQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CssdStockOpnameApprovalQuery";
		}
	}

	[Serializable]
	public partial class CssdStockOpnameApprovalMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CssdStockOpnameApprovalMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CssdStockOpnameApprovalMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdStockOpnameApprovalMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameApprovalMetadata.ColumnNames.PageNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CssdStockOpnameApprovalMetadata.PropertyNames.PageNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameApprovalMetadata.ColumnNames.IsApproved, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdStockOpnameApprovalMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameApprovalMetadata.ColumnNames.ApprovedDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdStockOpnameApprovalMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameApprovalMetadata.ColumnNames.ApprovedByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdStockOpnameApprovalMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CssdStockOpnameApprovalMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string PageNo = "PageNo";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string PageNo = "PageNo";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
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
			lock (typeof(CssdStockOpnameApprovalMetadata))
			{
				if (CssdStockOpnameApprovalMetadata.mapDelegates == null)
				{
					CssdStockOpnameApprovalMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CssdStockOpnameApprovalMetadata.meta == null)
				{
					CssdStockOpnameApprovalMetadata.meta = new CssdStockOpnameApprovalMetadata();
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

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PageNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CssdStockOpnameApproval";
				meta.Destination = "CssdStockOpnameApproval";
				meta.spInsert = "proc_CssdStockOpnameApprovalInsert";
				meta.spUpdate = "proc_CssdStockOpnameApprovalUpdate";
				meta.spDelete = "proc_CssdStockOpnameApprovalDelete";
				meta.spLoadAll = "proc_CssdStockOpnameApprovalLoadAll";
				meta.spLoadByPrimaryKey = "proc_CssdStockOpnameApprovalLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CssdStockOpnameApprovalMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
