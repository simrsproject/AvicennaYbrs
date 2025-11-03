/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/20/2022 9:57:41 AM
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
	abstract public class esCredentialProcessWorkExperienceCollection : esEntityCollectionWAuditLog
	{
		public esCredentialProcessWorkExperienceCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CredentialProcessWorkExperienceCollection";
		}

		#region Query Logic
		protected void InitQuery(esCredentialProcessWorkExperienceQuery query)
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
			this.InitQuery(query as esCredentialProcessWorkExperienceQuery);
		}
		#endregion

		virtual public CredentialProcessWorkExperience DetachEntity(CredentialProcessWorkExperience entity)
		{
			return base.DetachEntity(entity) as CredentialProcessWorkExperience;
		}

		virtual public CredentialProcessWorkExperience AttachEntity(CredentialProcessWorkExperience entity)
		{
			return base.AttachEntity(entity) as CredentialProcessWorkExperience;
		}

		virtual public void Combine(CredentialProcessWorkExperienceCollection collection)
		{
			base.Combine(collection);
		}

		new public CredentialProcessWorkExperience this[int index]
		{
			get
			{
				return base[index] as CredentialProcessWorkExperience;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CredentialProcessWorkExperience);
		}
	}

	[Serializable]
	abstract public class esCredentialProcessWorkExperience : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCredentialProcessWorkExperienceQuery GetDynamicQuery()
		{
			return null;
		}

		public esCredentialProcessWorkExperience()
		{
		}

		public esCredentialProcessWorkExperience(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String workExperienceNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, workExperienceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, workExperienceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String workExperienceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, workExperienceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, workExperienceNo);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String workExperienceNo)
		{
			esCredentialProcessWorkExperienceQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.WorkExperienceNo == workExperienceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String workExperienceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("WorkExperienceNo", workExperienceNo);
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
						case "WorkExperienceNo": this.str.WorkExperienceNo = (string)value; break;
						case "InstitutionName": this.str.InstitutionName = (string)value; break;
						case "StartPeriod": this.str.StartPeriod = (string)value; break;
						case "EndPeriod": this.str.EndPeriod = (string)value; break;
						case "PositionName": this.str.PositionName = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
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
		/// Maps to CredentialProcessWorkExperience.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(CredentialProcessWorkExperienceMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(CredentialProcessWorkExperienceMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessWorkExperience.WorkExperienceNo
		/// </summary>
		virtual public System.String WorkExperienceNo
		{
			get
			{
				return base.GetSystemString(CredentialProcessWorkExperienceMetadata.ColumnNames.WorkExperienceNo);
			}

			set
			{
				base.SetSystemString(CredentialProcessWorkExperienceMetadata.ColumnNames.WorkExperienceNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessWorkExperience.InstitutionName
		/// </summary>
		virtual public System.String InstitutionName
		{
			get
			{
				return base.GetSystemString(CredentialProcessWorkExperienceMetadata.ColumnNames.InstitutionName);
			}

			set
			{
				base.SetSystemString(CredentialProcessWorkExperienceMetadata.ColumnNames.InstitutionName, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessWorkExperience.StartPeriod
		/// </summary>
		virtual public System.String StartPeriod
		{
			get
			{
				return base.GetSystemString(CredentialProcessWorkExperienceMetadata.ColumnNames.StartPeriod);
			}

			set
			{
				base.SetSystemString(CredentialProcessWorkExperienceMetadata.ColumnNames.StartPeriod, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessWorkExperience.EndPeriod
		/// </summary>
		virtual public System.String EndPeriod
		{
			get
			{
				return base.GetSystemString(CredentialProcessWorkExperienceMetadata.ColumnNames.EndPeriod);
			}

			set
			{
				base.SetSystemString(CredentialProcessWorkExperienceMetadata.ColumnNames.EndPeriod, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessWorkExperience.PositionName
		/// </summary>
		virtual public System.String PositionName
		{
			get
			{
				return base.GetSystemString(CredentialProcessWorkExperienceMetadata.ColumnNames.PositionName);
			}

			set
			{
				base.SetSystemString(CredentialProcessWorkExperienceMetadata.ColumnNames.PositionName, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessWorkExperience.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessWorkExperienceMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessWorkExperienceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessWorkExperience.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessWorkExperienceMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessWorkExperienceMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCredentialProcessWorkExperience entity)
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
			public System.String WorkExperienceNo
			{
				get
				{
					System.String data = entity.WorkExperienceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkExperienceNo = null;
					else entity.WorkExperienceNo = Convert.ToString(value);
				}
			}
			public System.String InstitutionName
			{
				get
				{
					System.String data = entity.InstitutionName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InstitutionName = null;
					else entity.InstitutionName = Convert.ToString(value);
				}
			}
			public System.String StartPeriod
			{
				get
				{
					System.String data = entity.StartPeriod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartPeriod = null;
					else entity.StartPeriod = Convert.ToString(value);
				}
			}
			public System.String EndPeriod
			{
				get
				{
					System.String data = entity.EndPeriod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndPeriod = null;
					else entity.EndPeriod = Convert.ToString(value);
				}
			}
			public System.String PositionName
			{
				get
				{
					System.String data = entity.PositionName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionName = null;
					else entity.PositionName = Convert.ToString(value);
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
			private esCredentialProcessWorkExperience entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCredentialProcessWorkExperienceQuery query)
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
				throw new Exception("esCredentialProcessWorkExperience can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CredentialProcessWorkExperience : esCredentialProcessWorkExperience
	{
	}

	[Serializable]
	abstract public class esCredentialProcessWorkExperienceQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CredentialProcessWorkExperienceMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessWorkExperienceMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem WorkExperienceNo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessWorkExperienceMetadata.ColumnNames.WorkExperienceNo, esSystemType.String);
			}
		}

		public esQueryItem InstitutionName
		{
			get
			{
				return new esQueryItem(this, CredentialProcessWorkExperienceMetadata.ColumnNames.InstitutionName, esSystemType.String);
			}
		}

		public esQueryItem StartPeriod
		{
			get
			{
				return new esQueryItem(this, CredentialProcessWorkExperienceMetadata.ColumnNames.StartPeriod, esSystemType.String);
			}
		}

		public esQueryItem EndPeriod
		{
			get
			{
				return new esQueryItem(this, CredentialProcessWorkExperienceMetadata.ColumnNames.EndPeriod, esSystemType.String);
			}
		}

		public esQueryItem PositionName
		{
			get
			{
				return new esQueryItem(this, CredentialProcessWorkExperienceMetadata.ColumnNames.PositionName, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessWorkExperienceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessWorkExperienceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CredentialProcessWorkExperienceCollection")]
	public partial class CredentialProcessWorkExperienceCollection : esCredentialProcessWorkExperienceCollection, IEnumerable<CredentialProcessWorkExperience>
	{
		public CredentialProcessWorkExperienceCollection()
		{

		}

		public static implicit operator List<CredentialProcessWorkExperience>(CredentialProcessWorkExperienceCollection coll)
		{
			List<CredentialProcessWorkExperience> list = new List<CredentialProcessWorkExperience>();

			foreach (CredentialProcessWorkExperience emp in coll)
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
				return CredentialProcessWorkExperienceMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialProcessWorkExperienceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CredentialProcessWorkExperience(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CredentialProcessWorkExperience();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CredentialProcessWorkExperienceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialProcessWorkExperienceQuery();
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
		public bool Load(CredentialProcessWorkExperienceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CredentialProcessWorkExperience AddNew()
		{
			CredentialProcessWorkExperience entity = base.AddNewEntity() as CredentialProcessWorkExperience;

			return entity;
		}
		public CredentialProcessWorkExperience FindByPrimaryKey(String transactionNo, String workExperienceNo)
		{
			return base.FindByPrimaryKey(transactionNo, workExperienceNo) as CredentialProcessWorkExperience;
		}

		#region IEnumerable< CredentialProcessWorkExperience> Members

		IEnumerator<CredentialProcessWorkExperience> IEnumerable<CredentialProcessWorkExperience>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CredentialProcessWorkExperience;
			}
		}

		#endregion

		private CredentialProcessWorkExperienceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CredentialProcessWorkExperience' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CredentialProcessWorkExperience ({TransactionNo, WorkExperienceNo})")]
	[Serializable]
	public partial class CredentialProcessWorkExperience : esCredentialProcessWorkExperience
	{
		public CredentialProcessWorkExperience()
		{
		}

		public CredentialProcessWorkExperience(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CredentialProcessWorkExperienceMetadata.Meta();
			}
		}

		override protected esCredentialProcessWorkExperienceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialProcessWorkExperienceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CredentialProcessWorkExperienceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialProcessWorkExperienceQuery();
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
		public bool Load(CredentialProcessWorkExperienceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CredentialProcessWorkExperienceQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CredentialProcessWorkExperienceQuery : esCredentialProcessWorkExperienceQuery
	{
		public CredentialProcessWorkExperienceQuery()
		{

		}

		public CredentialProcessWorkExperienceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CredentialProcessWorkExperienceQuery";
		}
	}

	[Serializable]
	public partial class CredentialProcessWorkExperienceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CredentialProcessWorkExperienceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CredentialProcessWorkExperienceMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessWorkExperienceMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessWorkExperienceMetadata.ColumnNames.WorkExperienceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessWorkExperienceMetadata.PropertyNames.WorkExperienceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessWorkExperienceMetadata.ColumnNames.InstitutionName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessWorkExperienceMetadata.PropertyNames.InstitutionName;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessWorkExperienceMetadata.ColumnNames.StartPeriod, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessWorkExperienceMetadata.PropertyNames.StartPeriod;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessWorkExperienceMetadata.ColumnNames.EndPeriod, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessWorkExperienceMetadata.PropertyNames.EndPeriod;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessWorkExperienceMetadata.ColumnNames.PositionName, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessWorkExperienceMetadata.PropertyNames.PositionName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessWorkExperienceMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessWorkExperienceMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessWorkExperienceMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessWorkExperienceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CredentialProcessWorkExperienceMetadata Meta()
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
			public const string WorkExperienceNo = "WorkExperienceNo";
			public const string InstitutionName = "InstitutionName";
			public const string StartPeriod = "StartPeriod";
			public const string EndPeriod = "EndPeriod";
			public const string PositionName = "PositionName";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string WorkExperienceNo = "WorkExperienceNo";
			public const string InstitutionName = "InstitutionName";
			public const string StartPeriod = "StartPeriod";
			public const string EndPeriod = "EndPeriod";
			public const string PositionName = "PositionName";
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
			lock (typeof(CredentialProcessWorkExperienceMetadata))
			{
				if (CredentialProcessWorkExperienceMetadata.mapDelegates == null)
				{
					CredentialProcessWorkExperienceMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CredentialProcessWorkExperienceMetadata.meta == null)
				{
					CredentialProcessWorkExperienceMetadata.meta = new CredentialProcessWorkExperienceMetadata();
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
				meta.AddTypeMap("WorkExperienceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InstitutionName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartPeriod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EndPeriod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CredentialProcessWorkExperience";
				meta.Destination = "CredentialProcessWorkExperience";
				meta.spInsert = "proc_CredentialProcessWorkExperienceInsert";
				meta.spUpdate = "proc_CredentialProcessWorkExperienceUpdate";
				meta.spDelete = "proc_CredentialProcessWorkExperienceDelete";
				meta.spLoadAll = "proc_CredentialProcessWorkExperienceLoadAll";
				meta.spLoadByPrimaryKey = "proc_CredentialProcessWorkExperienceLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CredentialProcessWorkExperienceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
