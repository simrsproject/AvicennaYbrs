/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/9/2023 1:25:32 PM
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
	abstract public class esCredentialCompetencyAssessmentExplorationCollection : esEntityCollectionWAuditLog
	{
		public esCredentialCompetencyAssessmentExplorationCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CredentialCompetencyAssessmentExplorationCollection";
		}

		#region Query Logic
		protected void InitQuery(esCredentialCompetencyAssessmentExplorationQuery query)
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
			this.InitQuery(query as esCredentialCompetencyAssessmentExplorationQuery);
		}
		#endregion

		virtual public CredentialCompetencyAssessmentExploration DetachEntity(CredentialCompetencyAssessmentExploration entity)
		{
			return base.DetachEntity(entity) as CredentialCompetencyAssessmentExploration;
		}

		virtual public CredentialCompetencyAssessmentExploration AttachEntity(CredentialCompetencyAssessmentExploration entity)
		{
			return base.AttachEntity(entity) as CredentialCompetencyAssessmentExploration;
		}

		virtual public void Combine(CredentialCompetencyAssessmentExplorationCollection collection)
		{
			base.Combine(collection);
		}

		new public CredentialCompetencyAssessmentExploration this[int index]
		{
			get
			{
				return base[index] as CredentialCompetencyAssessmentExploration;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CredentialCompetencyAssessmentExploration);
		}
	}

	[Serializable]
	abstract public class esCredentialCompetencyAssessmentExploration : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCredentialCompetencyAssessmentExplorationQuery GetDynamicQuery()
		{
			return null;
		}

		public esCredentialCompetencyAssessmentExploration()
		{
		}

		public esCredentialCompetencyAssessmentExploration(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sRCompetencyAssessmentExplor)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sRCompetencyAssessmentExplor);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sRCompetencyAssessmentExplor);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sRCompetencyAssessmentExplor)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sRCompetencyAssessmentExplor);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sRCompetencyAssessmentExplor);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sRCompetencyAssessmentExplor)
		{
			esCredentialCompetencyAssessmentExplorationQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SRCompetencyAssessmentExplor == sRCompetencyAssessmentExplor);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sRCompetencyAssessmentExplor)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("SRCompetencyAssessmentExplor", sRCompetencyAssessmentExplor);
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
						case "SRCompetencyAssessmentExplor": this.str.SRCompetencyAssessmentExplor = (string)value; break;
						case "IsResult": this.str.IsResult = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IsResult":

							if (value == null || value is System.Boolean)
								this.IsResult = (System.Boolean?)value;
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
		/// Maps to CredentialCompetencyAssessmentExploration.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessmentExploration.SRCompetencyAssessmentExplor
		/// </summary>
		virtual public System.String SRCompetencyAssessmentExplor
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.SRCompetencyAssessmentExplor);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.SRCompetencyAssessmentExplor, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessmentExploration.IsResult
		/// </summary>
		virtual public System.Boolean? IsResult
		{
			get
			{
				return base.GetSystemBoolean(CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.IsResult);
			}

			set
			{
				base.SetSystemBoolean(CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.IsResult, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessmentExploration.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessmentExploration.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCredentialCompetencyAssessmentExploration entity)
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
			public System.String SRCompetencyAssessmentExplor
			{
				get
				{
					System.String data = entity.SRCompetencyAssessmentExplor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCompetencyAssessmentExplor = null;
					else entity.SRCompetencyAssessmentExplor = Convert.ToString(value);
				}
			}
			public System.String IsResult
			{
				get
				{
					System.Boolean? data = entity.IsResult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsResult = null;
					else entity.IsResult = Convert.ToBoolean(value);
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
			private esCredentialCompetencyAssessmentExploration entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCredentialCompetencyAssessmentExplorationQuery query)
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
				throw new Exception("esCredentialCompetencyAssessmentExploration can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CredentialCompetencyAssessmentExploration : esCredentialCompetencyAssessmentExploration
	{
	}

	[Serializable]
	abstract public class esCredentialCompetencyAssessmentExplorationQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CredentialCompetencyAssessmentExplorationMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SRCompetencyAssessmentExplor
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.SRCompetencyAssessmentExplor, esSystemType.String);
			}
		}

		public esQueryItem IsResult
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.IsResult, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CredentialCompetencyAssessmentExplorationCollection")]
	public partial class CredentialCompetencyAssessmentExplorationCollection : esCredentialCompetencyAssessmentExplorationCollection, IEnumerable<CredentialCompetencyAssessmentExploration>
	{
		public CredentialCompetencyAssessmentExplorationCollection()
		{

		}

		public static implicit operator List<CredentialCompetencyAssessmentExploration>(CredentialCompetencyAssessmentExplorationCollection coll)
		{
			List<CredentialCompetencyAssessmentExploration> list = new List<CredentialCompetencyAssessmentExploration>();

			foreach (CredentialCompetencyAssessmentExploration emp in coll)
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
				return CredentialCompetencyAssessmentExplorationMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialCompetencyAssessmentExplorationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CredentialCompetencyAssessmentExploration(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CredentialCompetencyAssessmentExploration();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CredentialCompetencyAssessmentExplorationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialCompetencyAssessmentExplorationQuery();
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
		public bool Load(CredentialCompetencyAssessmentExplorationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CredentialCompetencyAssessmentExploration AddNew()
		{
			CredentialCompetencyAssessmentExploration entity = base.AddNewEntity() as CredentialCompetencyAssessmentExploration;

			return entity;
		}
		public CredentialCompetencyAssessmentExploration FindByPrimaryKey(String transactionNo, String sRCompetencyAssessmentExplor)
		{
			return base.FindByPrimaryKey(transactionNo, sRCompetencyAssessmentExplor) as CredentialCompetencyAssessmentExploration;
		}

		#region IEnumerable< CredentialCompetencyAssessmentExploration> Members

		IEnumerator<CredentialCompetencyAssessmentExploration> IEnumerable<CredentialCompetencyAssessmentExploration>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CredentialCompetencyAssessmentExploration;
			}
		}

		#endregion

		private CredentialCompetencyAssessmentExplorationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CredentialCompetencyAssessmentExploration' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CredentialCompetencyAssessmentExploration ({TransactionNo, SRCompetencyAssessmentExplor})")]
	[Serializable]
	public partial class CredentialCompetencyAssessmentExploration : esCredentialCompetencyAssessmentExploration
	{
		public CredentialCompetencyAssessmentExploration()
		{
		}

		public CredentialCompetencyAssessmentExploration(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CredentialCompetencyAssessmentExplorationMetadata.Meta();
			}
		}

		override protected esCredentialCompetencyAssessmentExplorationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialCompetencyAssessmentExplorationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CredentialCompetencyAssessmentExplorationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialCompetencyAssessmentExplorationQuery();
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
		public bool Load(CredentialCompetencyAssessmentExplorationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CredentialCompetencyAssessmentExplorationQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CredentialCompetencyAssessmentExplorationQuery : esCredentialCompetencyAssessmentExplorationQuery
	{
		public CredentialCompetencyAssessmentExplorationQuery()
		{

		}

		public CredentialCompetencyAssessmentExplorationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CredentialCompetencyAssessmentExplorationQuery";
		}
	}

	[Serializable]
	public partial class CredentialCompetencyAssessmentExplorationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CredentialCompetencyAssessmentExplorationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentExplorationMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.SRCompetencyAssessmentExplor, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentExplorationMetadata.PropertyNames.SRCompetencyAssessmentExplor;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.IsResult, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialCompetencyAssessmentExplorationMetadata.PropertyNames.IsResult;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialCompetencyAssessmentExplorationMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentExplorationMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentExplorationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CredentialCompetencyAssessmentExplorationMetadata Meta()
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
			public const string SRCompetencyAssessmentExplor = "SRCompetencyAssessmentExplor";
			public const string IsResult = "IsResult";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string SRCompetencyAssessmentExplor = "SRCompetencyAssessmentExplor";
			public const string IsResult = "IsResult";
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
			lock (typeof(CredentialCompetencyAssessmentExplorationMetadata))
			{
				if (CredentialCompetencyAssessmentExplorationMetadata.mapDelegates == null)
				{
					CredentialCompetencyAssessmentExplorationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CredentialCompetencyAssessmentExplorationMetadata.meta == null)
				{
					CredentialCompetencyAssessmentExplorationMetadata.meta = new CredentialCompetencyAssessmentExplorationMetadata();
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
				meta.AddTypeMap("SRCompetencyAssessmentExplor", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsResult", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CredentialCompetencyAssessmentExploration";
				meta.Destination = "CredentialCompetencyAssessmentExploration";
				meta.spInsert = "proc_CredentialCompetencyAssessmentExplorationInsert";
				meta.spUpdate = "proc_CredentialCompetencyAssessmentExplorationUpdate";
				meta.spDelete = "proc_CredentialCompetencyAssessmentExplorationDelete";
				meta.spLoadAll = "proc_CredentialCompetencyAssessmentExplorationLoadAll";
				meta.spLoadByPrimaryKey = "proc_CredentialCompetencyAssessmentExplorationLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CredentialCompetencyAssessmentExplorationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
