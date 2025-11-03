/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/22/2022 3:54:03 PM
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
	abstract public class esEmployeeSafetyCultureIncidentReportsChronologySubjectCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeSafetyCultureIncidentReportsChronologySubjectCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeSafetyCultureIncidentReportsChronologySubjectCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeSafetyCultureIncidentReportsChronologySubjectQuery query)
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
			this.InitQuery(query as esEmployeeSafetyCultureIncidentReportsChronologySubjectQuery);
		}
		#endregion

		virtual public EmployeeSafetyCultureIncidentReportsChronologySubject DetachEntity(EmployeeSafetyCultureIncidentReportsChronologySubject entity)
		{
			return base.DetachEntity(entity) as EmployeeSafetyCultureIncidentReportsChronologySubject;
		}

		virtual public EmployeeSafetyCultureIncidentReportsChronologySubject AttachEntity(EmployeeSafetyCultureIncidentReportsChronologySubject entity)
		{
			return base.AttachEntity(entity) as EmployeeSafetyCultureIncidentReportsChronologySubject;
		}

		virtual public void Combine(EmployeeSafetyCultureIncidentReportsChronologySubjectCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeSafetyCultureIncidentReportsChronologySubject this[int index]
		{
			get
			{
				return base[index] as EmployeeSafetyCultureIncidentReportsChronologySubject;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeSafetyCultureIncidentReportsChronologySubject);
		}
	}

	[Serializable]
	abstract public class esEmployeeSafetyCultureIncidentReportsChronologySubject : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeSafetyCultureIncidentReportsChronologySubjectQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeSafetyCultureIncidentReportsChronologySubject()
		{
		}

		public esEmployeeSafetyCultureIncidentReportsChronologySubject(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sequenceNo, Int32 subjectPersonID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, subjectPersonID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, subjectPersonID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sequenceNo, Int32 subjectPersonID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, subjectPersonID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, subjectPersonID);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sequenceNo, Int32 subjectPersonID)
		{
			esEmployeeSafetyCultureIncidentReportsChronologySubjectQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo, query.SubjectPersonID == subjectPersonID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sequenceNo, Int32 subjectPersonID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("SequenceNo", sequenceNo);
			parms.Add("SubjectPersonID", subjectPersonID);
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
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "SubjectPersonID": this.str.SubjectPersonID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "SubjectPersonID":

							if (value == null || value is System.Int32)
								this.SubjectPersonID = (System.Int32?)value;
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
		/// Maps to EmployeeSafetyCultureIncidentReportsChronologySubject.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsChronologySubject.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.SequenceNo);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsChronologySubject.SubjectPersonID
		/// </summary>
		virtual public System.Int32? SubjectPersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.SubjectPersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.SubjectPersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsChronologySubject.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsChronologySubject.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeSafetyCultureIncidentReportsChronologySubject entity)
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
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
				}
			}
			public System.String SubjectPersonID
			{
				get
				{
					System.Int32? data = entity.SubjectPersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubjectPersonID = null;
					else entity.SubjectPersonID = Convert.ToInt32(value);
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
			private esEmployeeSafetyCultureIncidentReportsChronologySubject entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeSafetyCultureIncidentReportsChronologySubjectQuery query)
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
				throw new Exception("esEmployeeSafetyCultureIncidentReportsChronologySubject can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeSafetyCultureIncidentReportsChronologySubject : esEmployeeSafetyCultureIncidentReportsChronologySubject
	{
	}

	[Serializable]
	abstract public class esEmployeeSafetyCultureIncidentReportsChronologySubjectQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		}

		public esQueryItem SubjectPersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.SubjectPersonID, esSystemType.Int32);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeSafetyCultureIncidentReportsChronologySubjectCollection")]
	public partial class EmployeeSafetyCultureIncidentReportsChronologySubjectCollection : esEmployeeSafetyCultureIncidentReportsChronologySubjectCollection, IEnumerable<EmployeeSafetyCultureIncidentReportsChronologySubject>
	{
		public EmployeeSafetyCultureIncidentReportsChronologySubjectCollection()
		{

		}

		public static implicit operator List<EmployeeSafetyCultureIncidentReportsChronologySubject>(EmployeeSafetyCultureIncidentReportsChronologySubjectCollection coll)
		{
			List<EmployeeSafetyCultureIncidentReportsChronologySubject> list = new List<EmployeeSafetyCultureIncidentReportsChronologySubject>();

			foreach (EmployeeSafetyCultureIncidentReportsChronologySubject emp in coll)
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
				return EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeSafetyCultureIncidentReportsChronologySubjectQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeSafetyCultureIncidentReportsChronologySubject(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeSafetyCultureIncidentReportsChronologySubject();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeSafetyCultureIncidentReportsChronologySubjectQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeSafetyCultureIncidentReportsChronologySubjectQuery();
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
		public bool Load(EmployeeSafetyCultureIncidentReportsChronologySubjectQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeSafetyCultureIncidentReportsChronologySubject AddNew()
		{
			EmployeeSafetyCultureIncidentReportsChronologySubject entity = base.AddNewEntity() as EmployeeSafetyCultureIncidentReportsChronologySubject;

			return entity;
		}
		public EmployeeSafetyCultureIncidentReportsChronologySubject FindByPrimaryKey(String transactionNo, String sequenceNo, Int32 subjectPersonID)
		{
			return base.FindByPrimaryKey(transactionNo, sequenceNo, subjectPersonID) as EmployeeSafetyCultureIncidentReportsChronologySubject;
		}

		#region IEnumerable< EmployeeSafetyCultureIncidentReportsChronologySubject> Members

		IEnumerator<EmployeeSafetyCultureIncidentReportsChronologySubject> IEnumerable<EmployeeSafetyCultureIncidentReportsChronologySubject>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeSafetyCultureIncidentReportsChronologySubject;
			}
		}

		#endregion

		private EmployeeSafetyCultureIncidentReportsChronologySubjectQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeSafetyCultureIncidentReportsChronologySubject' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeSafetyCultureIncidentReportsChronologySubject ({TransactionNo, SequenceNo, SubjectPersonID})")]
	[Serializable]
	public partial class EmployeeSafetyCultureIncidentReportsChronologySubject : esEmployeeSafetyCultureIncidentReportsChronologySubject
	{
		public EmployeeSafetyCultureIncidentReportsChronologySubject()
		{
		}

		public EmployeeSafetyCultureIncidentReportsChronologySubject(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.Meta();
			}
		}

		override protected esEmployeeSafetyCultureIncidentReportsChronologySubjectQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeSafetyCultureIncidentReportsChronologySubjectQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeSafetyCultureIncidentReportsChronologySubjectQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeSafetyCultureIncidentReportsChronologySubjectQuery();
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
		public bool Load(EmployeeSafetyCultureIncidentReportsChronologySubjectQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeSafetyCultureIncidentReportsChronologySubjectQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeSafetyCultureIncidentReportsChronologySubjectQuery : esEmployeeSafetyCultureIncidentReportsChronologySubjectQuery
	{
		public EmployeeSafetyCultureIncidentReportsChronologySubjectQuery()
		{

		}

		public EmployeeSafetyCultureIncidentReportsChronologySubjectQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeSafetyCultureIncidentReportsChronologySubjectQuery";
		}
	}

	[Serializable]
	public partial class EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.SubjectPersonID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.PropertyNames.SubjectPersonID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata Meta()
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
			public const string SequenceNo = "SequenceNo";
			public const string SubjectPersonID = "SubjectPersonID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string SubjectPersonID = "SubjectPersonID";
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
			lock (typeof(EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata))
			{
				if (EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.mapDelegates == null)
				{
					EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.meta == null)
				{
					EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.meta = new EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata();
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
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SubjectPersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeSafetyCultureIncidentReportsChronologySubject";
				meta.Destination = "EmployeeSafetyCultureIncidentReportsChronologySubject";
				meta.spInsert = "proc_EmployeeSafetyCultureIncidentReportsChronologySubjectInsert";
				meta.spUpdate = "proc_EmployeeSafetyCultureIncidentReportsChronologySubjectUpdate";
				meta.spDelete = "proc_EmployeeSafetyCultureIncidentReportsChronologySubjectDelete";
				meta.spLoadAll = "proc_EmployeeSafetyCultureIncidentReportsChronologySubjectLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeSafetyCultureIncidentReportsChronologySubjectLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
