/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/9/2023 1:24:19 PM
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
	abstract public class esCredentialCompetencyAssessmentDtCollection : esEntityCollectionWAuditLog
	{
		public esCredentialCompetencyAssessmentDtCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CredentialCompetencyAssessmentDtCollection";
		}

		#region Query Logic
		protected void InitQuery(esCredentialCompetencyAssessmentDtQuery query)
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
			this.InitQuery(query as esCredentialCompetencyAssessmentDtQuery);
		}
		#endregion

		virtual public CredentialCompetencyAssessmentDt DetachEntity(CredentialCompetencyAssessmentDt entity)
		{
			return base.DetachEntity(entity) as CredentialCompetencyAssessmentDt;
		}

		virtual public CredentialCompetencyAssessmentDt AttachEntity(CredentialCompetencyAssessmentDt entity)
		{
			return base.AttachEntity(entity) as CredentialCompetencyAssessmentDt;
		}

		virtual public void Combine(CredentialCompetencyAssessmentDtCollection collection)
		{
			base.Combine(collection);
		}

		new public CredentialCompetencyAssessmentDt this[int index]
		{
			get
			{
				return base[index] as CredentialCompetencyAssessmentDt;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CredentialCompetencyAssessmentDt);
		}
	}

	[Serializable]
	abstract public class esCredentialCompetencyAssessmentDt : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCredentialCompetencyAssessmentDtQuery GetDynamicQuery()
		{
			return null;
		}

		public esCredentialCompetencyAssessmentDt()
		{
		}

		public esCredentialCompetencyAssessmentDt(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sRMedicalCompetencyAssessmentDt)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sRMedicalCompetencyAssessmentDt);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sRMedicalCompetencyAssessmentDt);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sRMedicalCompetencyAssessmentDt)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sRMedicalCompetencyAssessmentDt);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sRMedicalCompetencyAssessmentDt);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sRMedicalCompetencyAssessmentDt)
		{
			esCredentialCompetencyAssessmentDtQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SRMedicalCompetencyAssessmentDt == sRMedicalCompetencyAssessmentDt);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sRMedicalCompetencyAssessmentDt)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("SRMedicalCompetencyAssessmentDt", sRMedicalCompetencyAssessmentDt);
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
						case "SRMedicalCompetencyAssessmentDt": this.str.SRMedicalCompetencyAssessmentDt = (string)value; break;
						case "SRMedicalCompetencyAsstResult": this.str.SRMedicalCompetencyAsstResult = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
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
		/// Maps to CredentialCompetencyAssessmentDt.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentDtMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentDtMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessmentDt.SRMedicalCompetencyAssessmentDt
		/// </summary>
		virtual public System.String SRMedicalCompetencyAssessmentDt
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentDtMetadata.ColumnNames.SRMedicalCompetencyAssessmentDt);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentDtMetadata.ColumnNames.SRMedicalCompetencyAssessmentDt, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessmentDt.SRMedicalCompetencyAsstResult
		/// </summary>
		virtual public System.String SRMedicalCompetencyAsstResult
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentDtMetadata.ColumnNames.SRMedicalCompetencyAsstResult);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentDtMetadata.ColumnNames.SRMedicalCompetencyAsstResult, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessmentDt.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentDtMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentDtMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessmentDt.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialCompetencyAssessmentDtMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialCompetencyAssessmentDtMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessmentDt.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentDtMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentDtMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCredentialCompetencyAssessmentDt entity)
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
			public System.String SRMedicalCompetencyAssessmentDt
			{
				get
				{
					System.String data = entity.SRMedicalCompetencyAssessmentDt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMedicalCompetencyAssessmentDt = null;
					else entity.SRMedicalCompetencyAssessmentDt = Convert.ToString(value);
				}
			}
			public System.String SRMedicalCompetencyAsstResult
			{
				get
				{
					System.String data = entity.SRMedicalCompetencyAsstResult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMedicalCompetencyAsstResult = null;
					else entity.SRMedicalCompetencyAsstResult = Convert.ToString(value);
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
			private esCredentialCompetencyAssessmentDt entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCredentialCompetencyAssessmentDtQuery query)
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
				throw new Exception("esCredentialCompetencyAssessmentDt can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CredentialCompetencyAssessmentDt : esCredentialCompetencyAssessmentDt
	{
	}

	[Serializable]
	abstract public class esCredentialCompetencyAssessmentDtQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CredentialCompetencyAssessmentDtMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentDtMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SRMedicalCompetencyAssessmentDt
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentDtMetadata.ColumnNames.SRMedicalCompetencyAssessmentDt, esSystemType.String);
			}
		}

		public esQueryItem SRMedicalCompetencyAsstResult
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentDtMetadata.ColumnNames.SRMedicalCompetencyAsstResult, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentDtMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentDtMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentDtMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CredentialCompetencyAssessmentDtCollection")]
	public partial class CredentialCompetencyAssessmentDtCollection : esCredentialCompetencyAssessmentDtCollection, IEnumerable<CredentialCompetencyAssessmentDt>
	{
		public CredentialCompetencyAssessmentDtCollection()
		{

		}

		public static implicit operator List<CredentialCompetencyAssessmentDt>(CredentialCompetencyAssessmentDtCollection coll)
		{
			List<CredentialCompetencyAssessmentDt> list = new List<CredentialCompetencyAssessmentDt>();

			foreach (CredentialCompetencyAssessmentDt emp in coll)
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
				return CredentialCompetencyAssessmentDtMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialCompetencyAssessmentDtQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CredentialCompetencyAssessmentDt(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CredentialCompetencyAssessmentDt();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CredentialCompetencyAssessmentDtQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialCompetencyAssessmentDtQuery();
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
		public bool Load(CredentialCompetencyAssessmentDtQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CredentialCompetencyAssessmentDt AddNew()
		{
			CredentialCompetencyAssessmentDt entity = base.AddNewEntity() as CredentialCompetencyAssessmentDt;

			return entity;
		}
		public CredentialCompetencyAssessmentDt FindByPrimaryKey(String transactionNo, String sRMedicalCompetencyAssessmentDt)
		{
			return base.FindByPrimaryKey(transactionNo, sRMedicalCompetencyAssessmentDt) as CredentialCompetencyAssessmentDt;
		}

		#region IEnumerable< CredentialCompetencyAssessmentDt> Members

		IEnumerator<CredentialCompetencyAssessmentDt> IEnumerable<CredentialCompetencyAssessmentDt>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CredentialCompetencyAssessmentDt;
			}
		}

		#endregion

		private CredentialCompetencyAssessmentDtQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CredentialCompetencyAssessmentDt' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CredentialCompetencyAssessmentDt ({TransactionNo, SRMedicalCompetencyAssessmentDt})")]
	[Serializable]
	public partial class CredentialCompetencyAssessmentDt : esCredentialCompetencyAssessmentDt
	{
		public CredentialCompetencyAssessmentDt()
		{
		}

		public CredentialCompetencyAssessmentDt(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CredentialCompetencyAssessmentDtMetadata.Meta();
			}
		}

		override protected esCredentialCompetencyAssessmentDtQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialCompetencyAssessmentDtQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CredentialCompetencyAssessmentDtQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialCompetencyAssessmentDtQuery();
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
		public bool Load(CredentialCompetencyAssessmentDtQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CredentialCompetencyAssessmentDtQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CredentialCompetencyAssessmentDtQuery : esCredentialCompetencyAssessmentDtQuery
	{
		public CredentialCompetencyAssessmentDtQuery()
		{

		}

		public CredentialCompetencyAssessmentDtQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CredentialCompetencyAssessmentDtQuery";
		}
	}

	[Serializable]
	public partial class CredentialCompetencyAssessmentDtMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CredentialCompetencyAssessmentDtMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CredentialCompetencyAssessmentDtMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentDtMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentDtMetadata.ColumnNames.SRMedicalCompetencyAssessmentDt, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentDtMetadata.PropertyNames.SRMedicalCompetencyAssessmentDt;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentDtMetadata.ColumnNames.SRMedicalCompetencyAsstResult, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentDtMetadata.PropertyNames.SRMedicalCompetencyAsstResult;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentDtMetadata.ColumnNames.Notes, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentDtMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentDtMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialCompetencyAssessmentDtMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentDtMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentDtMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CredentialCompetencyAssessmentDtMetadata Meta()
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
			public const string SRMedicalCompetencyAssessmentDt = "SRMedicalCompetencyAssessmentDt";
			public const string SRMedicalCompetencyAsstResult = "SRMedicalCompetencyAsstResult";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string SRMedicalCompetencyAssessmentDt = "SRMedicalCompetencyAssessmentDt";
			public const string SRMedicalCompetencyAsstResult = "SRMedicalCompetencyAsstResult";
			public const string Notes = "Notes";
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
			lock (typeof(CredentialCompetencyAssessmentDtMetadata))
			{
				if (CredentialCompetencyAssessmentDtMetadata.mapDelegates == null)
				{
					CredentialCompetencyAssessmentDtMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CredentialCompetencyAssessmentDtMetadata.meta == null)
				{
					CredentialCompetencyAssessmentDtMetadata.meta = new CredentialCompetencyAssessmentDtMetadata();
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
				meta.AddTypeMap("SRMedicalCompetencyAssessmentDt", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMedicalCompetencyAsstResult", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CredentialCompetencyAssessmentDt";
				meta.Destination = "CredentialCompetencyAssessmentDt";
				meta.spInsert = "proc_CredentialCompetencyAssessmentDtInsert";
				meta.spUpdate = "proc_CredentialCompetencyAssessmentDtUpdate";
				meta.spDelete = "proc_CredentialCompetencyAssessmentDtDelete";
				meta.spLoadAll = "proc_CredentialCompetencyAssessmentDtLoadAll";
				meta.spLoadByPrimaryKey = "proc_CredentialCompetencyAssessmentDtLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CredentialCompetencyAssessmentDtMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
