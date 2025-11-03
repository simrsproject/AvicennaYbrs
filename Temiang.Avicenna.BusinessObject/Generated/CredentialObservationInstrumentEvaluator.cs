/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/9/2023 1:31:15 PM
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
	abstract public class esCredentialObservationInstrumentEvaluatorCollection : esEntityCollectionWAuditLog
	{
		public esCredentialObservationInstrumentEvaluatorCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CredentialObservationInstrumentEvaluatorCollection";
		}

		#region Query Logic
		protected void InitQuery(esCredentialObservationInstrumentEvaluatorQuery query)
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
			this.InitQuery(query as esCredentialObservationInstrumentEvaluatorQuery);
		}
		#endregion

		virtual public CredentialObservationInstrumentEvaluator DetachEntity(CredentialObservationInstrumentEvaluator entity)
		{
			return base.DetachEntity(entity) as CredentialObservationInstrumentEvaluator;
		}

		virtual public CredentialObservationInstrumentEvaluator AttachEntity(CredentialObservationInstrumentEvaluator entity)
		{
			return base.AttachEntity(entity) as CredentialObservationInstrumentEvaluator;
		}

		virtual public void Combine(CredentialObservationInstrumentEvaluatorCollection collection)
		{
			base.Combine(collection);
		}

		new public CredentialObservationInstrumentEvaluator this[int index]
		{
			get
			{
				return base[index] as CredentialObservationInstrumentEvaluator;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CredentialObservationInstrumentEvaluator);
		}
	}

	[Serializable]
	abstract public class esCredentialObservationInstrumentEvaluator : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCredentialObservationInstrumentEvaluatorQuery GetDynamicQuery()
		{
			return null;
		}

		public esCredentialObservationInstrumentEvaluator()
		{
		}

		public esCredentialObservationInstrumentEvaluator(DataRow row)
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
			esCredentialObservationInstrumentEvaluatorQuery query = this.GetDynamicQuery();
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
						case "SRCredentialEvaluatorNo": this.str.SRCredentialEvaluatorNo = (string)value; break;
						case "SRCredentialEvaluatorRole": this.str.SRCredentialEvaluatorRole = (string)value; break;
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
		/// Maps to CredentialObservationInstrumentEvaluator.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentEvaluator.EvaluatorID
		/// </summary>
		virtual public System.Int32? EvaluatorID
		{
			get
			{
				return base.GetSystemInt32(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.EvaluatorID);
			}

			set
			{
				base.SetSystemInt32(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.EvaluatorID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentEvaluator.SRCredentialEvaluatorNo
		/// </summary>
		virtual public System.String SRCredentialEvaluatorNo
		{
			get
			{
				return base.GetSystemString(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.SRCredentialEvaluatorNo);
			}

			set
			{
				base.SetSystemString(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.SRCredentialEvaluatorNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentEvaluator.SRCredentialEvaluatorRole
		/// </summary>
		virtual public System.String SRCredentialEvaluatorRole
		{
			get
			{
				return base.GetSystemString(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.SRCredentialEvaluatorRole);
			}

			set
			{
				base.SetSystemString(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.SRCredentialEvaluatorRole, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentEvaluator.IsEvaluated
		/// </summary>
		virtual public System.Boolean? IsEvaluated
		{
			get
			{
				return base.GetSystemBoolean(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.IsEvaluated);
			}

			set
			{
				base.SetSystemBoolean(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.IsEvaluated, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentEvaluator.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentEvaluator.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCredentialObservationInstrumentEvaluator entity)
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
			public System.String SRCredentialEvaluatorNo
			{
				get
				{
					System.String data = entity.SRCredentialEvaluatorNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCredentialEvaluatorNo = null;
					else entity.SRCredentialEvaluatorNo = Convert.ToString(value);
				}
			}
			public System.String SRCredentialEvaluatorRole
			{
				get
				{
					System.String data = entity.SRCredentialEvaluatorRole;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCredentialEvaluatorRole = null;
					else entity.SRCredentialEvaluatorRole = Convert.ToString(value);
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
			private esCredentialObservationInstrumentEvaluator entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCredentialObservationInstrumentEvaluatorQuery query)
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
				throw new Exception("esCredentialObservationInstrumentEvaluator can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CredentialObservationInstrumentEvaluator : esCredentialObservationInstrumentEvaluator
	{
	}

	[Serializable]
	abstract public class esCredentialObservationInstrumentEvaluatorQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CredentialObservationInstrumentEvaluatorMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem EvaluatorID
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.EvaluatorID, esSystemType.Int32);
			}
		}

		public esQueryItem SRCredentialEvaluatorNo
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.SRCredentialEvaluatorNo, esSystemType.String);
			}
		}

		public esQueryItem SRCredentialEvaluatorRole
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.SRCredentialEvaluatorRole, esSystemType.String);
			}
		}

		public esQueryItem IsEvaluated
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.IsEvaluated, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CredentialObservationInstrumentEvaluatorCollection")]
	public partial class CredentialObservationInstrumentEvaluatorCollection : esCredentialObservationInstrumentEvaluatorCollection, IEnumerable<CredentialObservationInstrumentEvaluator>
	{
		public CredentialObservationInstrumentEvaluatorCollection()
		{

		}

		public static implicit operator List<CredentialObservationInstrumentEvaluator>(CredentialObservationInstrumentEvaluatorCollection coll)
		{
			List<CredentialObservationInstrumentEvaluator> list = new List<CredentialObservationInstrumentEvaluator>();

			foreach (CredentialObservationInstrumentEvaluator emp in coll)
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
				return CredentialObservationInstrumentEvaluatorMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialObservationInstrumentEvaluatorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CredentialObservationInstrumentEvaluator(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CredentialObservationInstrumentEvaluator();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CredentialObservationInstrumentEvaluatorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialObservationInstrumentEvaluatorQuery();
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
		public bool Load(CredentialObservationInstrumentEvaluatorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CredentialObservationInstrumentEvaluator AddNew()
		{
			CredentialObservationInstrumentEvaluator entity = base.AddNewEntity() as CredentialObservationInstrumentEvaluator;

			return entity;
		}
		public CredentialObservationInstrumentEvaluator FindByPrimaryKey(String transactionNo, Int32 evaluatorID)
		{
			return base.FindByPrimaryKey(transactionNo, evaluatorID) as CredentialObservationInstrumentEvaluator;
		}

		#region IEnumerable< CredentialObservationInstrumentEvaluator> Members

		IEnumerator<CredentialObservationInstrumentEvaluator> IEnumerable<CredentialObservationInstrumentEvaluator>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CredentialObservationInstrumentEvaluator;
			}
		}

		#endregion

		private CredentialObservationInstrumentEvaluatorQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CredentialObservationInstrumentEvaluator' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CredentialObservationInstrumentEvaluator ({TransactionNo, EvaluatorID})")]
	[Serializable]
	public partial class CredentialObservationInstrumentEvaluator : esCredentialObservationInstrumentEvaluator
	{
		public CredentialObservationInstrumentEvaluator()
		{
		}

		public CredentialObservationInstrumentEvaluator(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CredentialObservationInstrumentEvaluatorMetadata.Meta();
			}
		}

		override protected esCredentialObservationInstrumentEvaluatorQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialObservationInstrumentEvaluatorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CredentialObservationInstrumentEvaluatorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialObservationInstrumentEvaluatorQuery();
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
		public bool Load(CredentialObservationInstrumentEvaluatorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CredentialObservationInstrumentEvaluatorQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CredentialObservationInstrumentEvaluatorQuery : esCredentialObservationInstrumentEvaluatorQuery
	{
		public CredentialObservationInstrumentEvaluatorQuery()
		{

		}

		public CredentialObservationInstrumentEvaluatorQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CredentialObservationInstrumentEvaluatorQuery";
		}
	}

	[Serializable]
	public partial class CredentialObservationInstrumentEvaluatorMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CredentialObservationInstrumentEvaluatorMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialObservationInstrumentEvaluatorMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.EvaluatorID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CredentialObservationInstrumentEvaluatorMetadata.PropertyNames.EvaluatorID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.SRCredentialEvaluatorNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialObservationInstrumentEvaluatorMetadata.PropertyNames.SRCredentialEvaluatorNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.SRCredentialEvaluatorRole, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialObservationInstrumentEvaluatorMetadata.PropertyNames.SRCredentialEvaluatorRole;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.IsEvaluated, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialObservationInstrumentEvaluatorMetadata.PropertyNames.IsEvaluated;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialObservationInstrumentEvaluatorMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentEvaluatorMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialObservationInstrumentEvaluatorMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CredentialObservationInstrumentEvaluatorMetadata Meta()
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
			public const string SRCredentialEvaluatorNo = "SRCredentialEvaluatorNo";
			public const string SRCredentialEvaluatorRole = "SRCredentialEvaluatorRole";
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
			public const string SRCredentialEvaluatorNo = "SRCredentialEvaluatorNo";
			public const string SRCredentialEvaluatorRole = "SRCredentialEvaluatorRole";
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
			lock (typeof(CredentialObservationInstrumentEvaluatorMetadata))
			{
				if (CredentialObservationInstrumentEvaluatorMetadata.mapDelegates == null)
				{
					CredentialObservationInstrumentEvaluatorMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CredentialObservationInstrumentEvaluatorMetadata.meta == null)
				{
					CredentialObservationInstrumentEvaluatorMetadata.meta = new CredentialObservationInstrumentEvaluatorMetadata();
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
				meta.AddTypeMap("SRCredentialEvaluatorNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCredentialEvaluatorRole", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsEvaluated", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CredentialObservationInstrumentEvaluator";
				meta.Destination = "CredentialObservationInstrumentEvaluator";
				meta.spInsert = "proc_CredentialObservationInstrumentEvaluatorInsert";
				meta.spUpdate = "proc_CredentialObservationInstrumentEvaluatorUpdate";
				meta.spDelete = "proc_CredentialObservationInstrumentEvaluatorDelete";
				meta.spLoadAll = "proc_CredentialObservationInstrumentEvaluatorLoadAll";
				meta.spLoadByPrimaryKey = "proc_CredentialObservationInstrumentEvaluatorLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CredentialObservationInstrumentEvaluatorMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
