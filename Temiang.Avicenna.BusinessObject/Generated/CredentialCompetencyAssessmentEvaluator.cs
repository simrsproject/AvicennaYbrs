/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/9/2023 1:24:53 PM
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
	abstract public class esCredentialCompetencyAssessmentEvaluatorCollection : esEntityCollectionWAuditLog
	{
		public esCredentialCompetencyAssessmentEvaluatorCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CredentialCompetencyAssessmentEvaluatorCollection";
		}

		#region Query Logic
		protected void InitQuery(esCredentialCompetencyAssessmentEvaluatorQuery query)
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
			this.InitQuery(query as esCredentialCompetencyAssessmentEvaluatorQuery);
		}
		#endregion

		virtual public CredentialCompetencyAssessmentEvaluator DetachEntity(CredentialCompetencyAssessmentEvaluator entity)
		{
			return base.DetachEntity(entity) as CredentialCompetencyAssessmentEvaluator;
		}

		virtual public CredentialCompetencyAssessmentEvaluator AttachEntity(CredentialCompetencyAssessmentEvaluator entity)
		{
			return base.AttachEntity(entity) as CredentialCompetencyAssessmentEvaluator;
		}

		virtual public void Combine(CredentialCompetencyAssessmentEvaluatorCollection collection)
		{
			base.Combine(collection);
		}

		new public CredentialCompetencyAssessmentEvaluator this[int index]
		{
			get
			{
				return base[index] as CredentialCompetencyAssessmentEvaluator;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CredentialCompetencyAssessmentEvaluator);
		}
	}

	[Serializable]
	abstract public class esCredentialCompetencyAssessmentEvaluator : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCredentialCompetencyAssessmentEvaluatorQuery GetDynamicQuery()
		{
			return null;
		}

		public esCredentialCompetencyAssessmentEvaluator()
		{
		}

		public esCredentialCompetencyAssessmentEvaluator(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, Int32 evaluatorID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, evaluatorID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, evaluatorID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, Int32 evaluatorID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, evaluatorID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, evaluatorID);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, Int32 evaluatorID)
		{
			esCredentialCompetencyAssessmentEvaluatorQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.EvaluatorID == evaluatorID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, Int32 evaluatorID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("EvaluatorID", evaluatorID);
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
						case "EvaluatorID": this.str.EvaluatorID = (string)value; break;
						case "SRCompetencyAssessmentEvalRole": this.str.SRCompetencyAssessmentEvalRole = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "IsEvaluated": this.str.IsEvaluated = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EvaluatorID":

							if (value == null || value is System.Int32)
								this.EvaluatorID = (System.Int32?)value;
							break;
						case "IsEvaluated":

							if (value == null || value is System.Boolean)
								this.IsEvaluated = (System.Boolean?)value;
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
		/// Maps to CredentialCompetencyAssessmentEvaluator.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessmentEvaluator.EvaluatorID
		/// </summary>
		virtual public System.Int32? EvaluatorID
		{
			get
			{
				return base.GetSystemInt32(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.EvaluatorID);
			}

			set
			{
				base.SetSystemInt32(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.EvaluatorID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessmentEvaluator.SRCompetencyAssessmentEvalRole
		/// </summary>
		virtual public System.String SRCompetencyAssessmentEvalRole
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.SRCompetencyAssessmentEvalRole);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.SRCompetencyAssessmentEvalRole, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessmentEvaluator.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessmentEvaluator.IsEvaluated
		/// </summary>
		virtual public System.Boolean? IsEvaluated
		{
			get
			{
				return base.GetSystemBoolean(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.IsEvaluated);
			}

			set
			{
				base.SetSystemBoolean(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.IsEvaluated, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessmentEvaluator.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessmentEvaluator.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCredentialCompetencyAssessmentEvaluator entity)
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
			public System.String EvaluatorID
			{
				get
				{
					System.Int32? data = entity.EvaluatorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EvaluatorID = null;
					else entity.EvaluatorID = Convert.ToInt32(value);
				}
			}
			public System.String SRCompetencyAssessmentEvalRole
			{
				get
				{
					System.String data = entity.SRCompetencyAssessmentEvalRole;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCompetencyAssessmentEvalRole = null;
					else entity.SRCompetencyAssessmentEvalRole = Convert.ToString(value);
				}
			}
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
				}
			}
			public System.String IsEvaluated
			{
				get
				{
					System.Boolean? data = entity.IsEvaluated;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEvaluated = null;
					else entity.IsEvaluated = Convert.ToBoolean(value);
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
			private esCredentialCompetencyAssessmentEvaluator entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCredentialCompetencyAssessmentEvaluatorQuery query)
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
				throw new Exception("esCredentialCompetencyAssessmentEvaluator can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CredentialCompetencyAssessmentEvaluator : esCredentialCompetencyAssessmentEvaluator
	{
	}

	[Serializable]
	abstract public class esCredentialCompetencyAssessmentEvaluatorQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CredentialCompetencyAssessmentEvaluatorMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem EvaluatorID
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.EvaluatorID, esSystemType.Int32);
			}
		}

		public esQueryItem SRCompetencyAssessmentEvalRole
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.SRCompetencyAssessmentEvalRole, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem IsEvaluated
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.IsEvaluated, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CredentialCompetencyAssessmentEvaluatorCollection")]
	public partial class CredentialCompetencyAssessmentEvaluatorCollection : esCredentialCompetencyAssessmentEvaluatorCollection, IEnumerable<CredentialCompetencyAssessmentEvaluator>
	{
		public CredentialCompetencyAssessmentEvaluatorCollection()
		{

		}

		public static implicit operator List<CredentialCompetencyAssessmentEvaluator>(CredentialCompetencyAssessmentEvaluatorCollection coll)
		{
			List<CredentialCompetencyAssessmentEvaluator> list = new List<CredentialCompetencyAssessmentEvaluator>();

			foreach (CredentialCompetencyAssessmentEvaluator emp in coll)
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
				return CredentialCompetencyAssessmentEvaluatorMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialCompetencyAssessmentEvaluatorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CredentialCompetencyAssessmentEvaluator(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CredentialCompetencyAssessmentEvaluator();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CredentialCompetencyAssessmentEvaluatorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialCompetencyAssessmentEvaluatorQuery();
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
		public bool Load(CredentialCompetencyAssessmentEvaluatorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CredentialCompetencyAssessmentEvaluator AddNew()
		{
			CredentialCompetencyAssessmentEvaluator entity = base.AddNewEntity() as CredentialCompetencyAssessmentEvaluator;

			return entity;
		}
		public CredentialCompetencyAssessmentEvaluator FindByPrimaryKey(String transactionNo, Int32 evaluatorID)
		{
			return base.FindByPrimaryKey(transactionNo, evaluatorID) as CredentialCompetencyAssessmentEvaluator;
		}

		#region IEnumerable< CredentialCompetencyAssessmentEvaluator> Members

		IEnumerator<CredentialCompetencyAssessmentEvaluator> IEnumerable<CredentialCompetencyAssessmentEvaluator>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CredentialCompetencyAssessmentEvaluator;
			}
		}

		#endregion

		private CredentialCompetencyAssessmentEvaluatorQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CredentialCompetencyAssessmentEvaluator' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CredentialCompetencyAssessmentEvaluator ({TransactionNo, EvaluatorID})")]
	[Serializable]
	public partial class CredentialCompetencyAssessmentEvaluator : esCredentialCompetencyAssessmentEvaluator
	{
		public CredentialCompetencyAssessmentEvaluator()
		{
		}

		public CredentialCompetencyAssessmentEvaluator(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CredentialCompetencyAssessmentEvaluatorMetadata.Meta();
			}
		}

		override protected esCredentialCompetencyAssessmentEvaluatorQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialCompetencyAssessmentEvaluatorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CredentialCompetencyAssessmentEvaluatorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialCompetencyAssessmentEvaluatorQuery();
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
		public bool Load(CredentialCompetencyAssessmentEvaluatorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CredentialCompetencyAssessmentEvaluatorQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CredentialCompetencyAssessmentEvaluatorQuery : esCredentialCompetencyAssessmentEvaluatorQuery
	{
		public CredentialCompetencyAssessmentEvaluatorQuery()
		{

		}

		public CredentialCompetencyAssessmentEvaluatorQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CredentialCompetencyAssessmentEvaluatorQuery";
		}
	}

	[Serializable]
	public partial class CredentialCompetencyAssessmentEvaluatorMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CredentialCompetencyAssessmentEvaluatorMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentEvaluatorMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.EvaluatorID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CredentialCompetencyAssessmentEvaluatorMetadata.PropertyNames.EvaluatorID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.SRCompetencyAssessmentEvalRole, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentEvaluatorMetadata.PropertyNames.SRCompetencyAssessmentEvalRole;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.Notes, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentEvaluatorMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.IsEvaluated, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialCompetencyAssessmentEvaluatorMetadata.PropertyNames.IsEvaluated;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialCompetencyAssessmentEvaluatorMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentEvaluatorMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CredentialCompetencyAssessmentEvaluatorMetadata Meta()
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
			public const string EvaluatorID = "EvaluatorID";
			public const string SRCompetencyAssessmentEvalRole = "SRCompetencyAssessmentEvalRole";
			public const string Notes = "Notes";
			public const string IsEvaluated = "IsEvaluated";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string EvaluatorID = "EvaluatorID";
			public const string SRCompetencyAssessmentEvalRole = "SRCompetencyAssessmentEvalRole";
			public const string Notes = "Notes";
			public const string IsEvaluated = "IsEvaluated";
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
			lock (typeof(CredentialCompetencyAssessmentEvaluatorMetadata))
			{
				if (CredentialCompetencyAssessmentEvaluatorMetadata.mapDelegates == null)
				{
					CredentialCompetencyAssessmentEvaluatorMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CredentialCompetencyAssessmentEvaluatorMetadata.meta == null)
				{
					CredentialCompetencyAssessmentEvaluatorMetadata.meta = new CredentialCompetencyAssessmentEvaluatorMetadata();
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
				meta.AddTypeMap("EvaluatorID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRCompetencyAssessmentEvalRole", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsEvaluated", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CredentialCompetencyAssessmentEvaluator";
				meta.Destination = "CredentialCompetencyAssessmentEvaluator";
				meta.spInsert = "proc_CredentialCompetencyAssessmentEvaluatorInsert";
				meta.spUpdate = "proc_CredentialCompetencyAssessmentEvaluatorUpdate";
				meta.spDelete = "proc_CredentialCompetencyAssessmentEvaluatorDelete";
				meta.spLoadAll = "proc_CredentialCompetencyAssessmentEvaluatorLoadAll";
				meta.spLoadByPrimaryKey = "proc_CredentialCompetencyAssessmentEvaluatorLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CredentialCompetencyAssessmentEvaluatorMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
