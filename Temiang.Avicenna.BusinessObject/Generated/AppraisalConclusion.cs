/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/15/2020 4:39:22 PM
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
	abstract public class esAppraisalConclusionCollection : esEntityCollectionWAuditLog
	{
		public esAppraisalConclusionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AppraisalConclusionCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppraisalConclusionQuery query)
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
			this.InitQuery(query as esAppraisalConclusionQuery);
		}
		#endregion

		virtual public AppraisalConclusion DetachEntity(AppraisalConclusion entity)
		{
			return base.DetachEntity(entity) as AppraisalConclusion;
		}

		virtual public AppraisalConclusion AttachEntity(AppraisalConclusion entity)
		{
			return base.AttachEntity(entity) as AppraisalConclusion;
		}

		virtual public void Combine(AppraisalConclusionCollection collection)
		{
			base.Combine(collection);
		}

		new public AppraisalConclusion this[int index]
		{
			get
			{
				return base[index] as AppraisalConclusion;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppraisalConclusion);
		}
	}

	[Serializable]
	abstract public class esAppraisalConclusion : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppraisalConclusionQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppraisalConclusion()
		{
		}

		public esAppraisalConclusion(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 conclusionID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(conclusionID);
			else
				return LoadByPrimaryKeyStoredProcedure(conclusionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 conclusionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(conclusionID);
			else
				return LoadByPrimaryKeyStoredProcedure(conclusionID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 conclusionID)
		{
			esAppraisalConclusionQuery query = this.GetDynamicQuery();
			query.Where(query.ConclusionID == conclusionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 conclusionID)
		{
			esParameters parms = new esParameters();
			parms.Add("ConclusionID", conclusionID);
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
						case "ConclusionID": this.str.ConclusionID = (string)value; break;
						case "ConclusionName": this.str.ConclusionName = (string)value; break;
						case "MinValue": this.str.MinValue = (string)value; break;
						case "MaxValue": this.str.MaxValue = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ConclusionID":

							if (value == null || value is System.Int32)
								this.ConclusionID = (System.Int32?)value;
							break;
						case "MinValue":

							if (value == null || value is System.Decimal)
								this.MinValue = (System.Decimal?)value;
							break;
						case "MaxValue":

							if (value == null || value is System.Decimal)
								this.MaxValue = (System.Decimal?)value;
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
		/// Maps to AppraisalConclusion.ConclusionID
		/// </summary>
		virtual public System.Int32? ConclusionID
		{
			get
			{
				return base.GetSystemInt32(AppraisalConclusionMetadata.ColumnNames.ConclusionID);
			}

			set
			{
				base.SetSystemInt32(AppraisalConclusionMetadata.ColumnNames.ConclusionID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalConclusion.ConclusionName
		/// </summary>
		virtual public System.String ConclusionName
		{
			get
			{
				return base.GetSystemString(AppraisalConclusionMetadata.ColumnNames.ConclusionName);
			}

			set
			{
				base.SetSystemString(AppraisalConclusionMetadata.ColumnNames.ConclusionName, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalConclusion.MinValue
		/// </summary>
		virtual public System.Decimal? MinValue
		{
			get
			{
				return base.GetSystemDecimal(AppraisalConclusionMetadata.ColumnNames.MinValue);
			}

			set
			{
				base.SetSystemDecimal(AppraisalConclusionMetadata.ColumnNames.MinValue, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalConclusion.MaxValue
		/// </summary>
		virtual public System.Decimal? MaxValue
		{
			get
			{
				return base.GetSystemDecimal(AppraisalConclusionMetadata.ColumnNames.MaxValue);
			}

			set
			{
				base.SetSystemDecimal(AppraisalConclusionMetadata.ColumnNames.MaxValue, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalConclusion.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppraisalConclusionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AppraisalConclusionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalConclusion.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppraisalConclusionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AppraisalConclusionMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAppraisalConclusion entity)
			{
				this.entity = entity;
			}
			public System.String ConclusionID
			{
				get
				{
					System.Int32? data = entity.ConclusionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConclusionID = null;
					else entity.ConclusionID = Convert.ToInt32(value);
				}
			}
			public System.String ConclusionName
			{
				get
				{
					System.String data = entity.ConclusionName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConclusionName = null;
					else entity.ConclusionName = Convert.ToString(value);
				}
			}
			public System.String MinValue
			{
				get
				{
					System.Decimal? data = entity.MinValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MinValue = null;
					else entity.MinValue = Convert.ToDecimal(value);
				}
			}
			public System.String MaxValue
			{
				get
				{
					System.Decimal? data = entity.MaxValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaxValue = null;
					else entity.MaxValue = Convert.ToDecimal(value);
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
			private esAppraisalConclusion entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppraisalConclusionQuery query)
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
				throw new Exception("esAppraisalConclusion can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppraisalConclusion : esAppraisalConclusion
	{
	}

	[Serializable]
	abstract public class esAppraisalConclusionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AppraisalConclusionMetadata.Meta();
			}
		}

		public esQueryItem ConclusionID
		{
			get
			{
				return new esQueryItem(this, AppraisalConclusionMetadata.ColumnNames.ConclusionID, esSystemType.Int32);
			}
		}

		public esQueryItem ConclusionName
		{
			get
			{
				return new esQueryItem(this, AppraisalConclusionMetadata.ColumnNames.ConclusionName, esSystemType.String);
			}
		}

		public esQueryItem MinValue
		{
			get
			{
				return new esQueryItem(this, AppraisalConclusionMetadata.ColumnNames.MinValue, esSystemType.Decimal);
			}
		}

		public esQueryItem MaxValue
		{
			get
			{
				return new esQueryItem(this, AppraisalConclusionMetadata.ColumnNames.MaxValue, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AppraisalConclusionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AppraisalConclusionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppraisalConclusionCollection")]
	public partial class AppraisalConclusionCollection : esAppraisalConclusionCollection, IEnumerable<AppraisalConclusion>
	{
		public AppraisalConclusionCollection()
		{

		}

		public static implicit operator List<AppraisalConclusion>(AppraisalConclusionCollection coll)
		{
			List<AppraisalConclusion> list = new List<AppraisalConclusion>();

			foreach (AppraisalConclusion emp in coll)
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
				return AppraisalConclusionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalConclusionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppraisalConclusion(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppraisalConclusion();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AppraisalConclusionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalConclusionQuery();
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
		public bool Load(AppraisalConclusionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppraisalConclusion AddNew()
		{
			AppraisalConclusion entity = base.AddNewEntity() as AppraisalConclusion;

			return entity;
		}
		public AppraisalConclusion FindByPrimaryKey(Int32 conclusionID)
		{
			return base.FindByPrimaryKey(conclusionID) as AppraisalConclusion;
		}

		#region IEnumerable< AppraisalConclusion> Members

		IEnumerator<AppraisalConclusion> IEnumerable<AppraisalConclusion>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AppraisalConclusion;
			}
		}

		#endregion

		private AppraisalConclusionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppraisalConclusion' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppraisalConclusion ({ConclusionID})")]
	[Serializable]
	public partial class AppraisalConclusion : esAppraisalConclusion
	{
		public AppraisalConclusion()
		{
		}

		public AppraisalConclusion(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppraisalConclusionMetadata.Meta();
			}
		}

		override protected esAppraisalConclusionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalConclusionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AppraisalConclusionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalConclusionQuery();
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
		public bool Load(AppraisalConclusionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AppraisalConclusionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppraisalConclusionQuery : esAppraisalConclusionQuery
	{
		public AppraisalConclusionQuery()
		{

		}

		public AppraisalConclusionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppraisalConclusionQuery";
		}
	}

	[Serializable]
	public partial class AppraisalConclusionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppraisalConclusionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppraisalConclusionMetadata.ColumnNames.ConclusionID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalConclusionMetadata.PropertyNames.ConclusionID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalConclusionMetadata.ColumnNames.ConclusionName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalConclusionMetadata.PropertyNames.ConclusionName;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalConclusionMetadata.ColumnNames.MinValue, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalConclusionMetadata.PropertyNames.MinValue;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalConclusionMetadata.ColumnNames.MaxValue, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalConclusionMetadata.PropertyNames.MaxValue;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalConclusionMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppraisalConclusionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalConclusionMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalConclusionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AppraisalConclusionMetadata Meta()
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
			public const string ConclusionID = "ConclusionID";
			public const string ConclusionName = "ConclusionName";
			public const string MinValue = "MinValue";
			public const string MaxValue = "MaxValue";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ConclusionID = "ConclusionID";
			public const string ConclusionName = "ConclusionName";
			public const string MinValue = "MinValue";
			public const string MaxValue = "MaxValue";
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
			lock (typeof(AppraisalConclusionMetadata))
			{
				if (AppraisalConclusionMetadata.mapDelegates == null)
				{
					AppraisalConclusionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AppraisalConclusionMetadata.meta == null)
				{
					AppraisalConclusionMetadata.meta = new AppraisalConclusionMetadata();
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

				meta.AddTypeMap("ConclusionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ConclusionName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MinValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("MaxValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "AppraisalConclusion";
				meta.Destination = "AppraisalConclusion";
				meta.spInsert = "proc_AppraisalConclusionInsert";
				meta.spUpdate = "proc_AppraisalConclusionUpdate";
				meta.spDelete = "proc_AppraisalConclusionDelete";
				meta.spLoadAll = "proc_AppraisalConclusionLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppraisalConclusionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppraisalConclusionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
