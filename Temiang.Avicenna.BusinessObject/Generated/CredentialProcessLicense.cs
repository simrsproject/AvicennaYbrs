/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/13/2022 2:39:20 PM
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
	abstract public class esCredentialProcessLicenseCollection : esEntityCollectionWAuditLog
	{
		public esCredentialProcessLicenseCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CredentialProcessLicenseCollection";
		}

		#region Query Logic
		protected void InitQuery(esCredentialProcessLicenseQuery query)
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
			this.InitQuery(query as esCredentialProcessLicenseQuery);
		}
		#endregion

		virtual public CredentialProcessLicense DetachEntity(CredentialProcessLicense entity)
		{
			return base.DetachEntity(entity) as CredentialProcessLicense;
		}

		virtual public CredentialProcessLicense AttachEntity(CredentialProcessLicense entity)
		{
			return base.AttachEntity(entity) as CredentialProcessLicense;
		}

		virtual public void Combine(CredentialProcessLicenseCollection collection)
		{
			base.Combine(collection);
		}

		new public CredentialProcessLicense this[int index]
		{
			get
			{
				return base[index] as CredentialProcessLicense;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CredentialProcessLicense);
		}
	}

	[Serializable]
	abstract public class esCredentialProcessLicense : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCredentialProcessLicenseQuery GetDynamicQuery()
		{
			return null;
		}

		public esCredentialProcessLicense()
		{
		}

		public esCredentialProcessLicense(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sRLicenseType)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sRLicenseType);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sRLicenseType);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sRLicenseType)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sRLicenseType);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sRLicenseType);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sRLicenseType)
		{
			esCredentialProcessLicenseQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SRLicenseType == sRLicenseType);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sRLicenseType)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("SRLicenseType", sRLicenseType);
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
						case "SRLicenseType": this.str.SRLicenseType = (string)value; break;
						case "LicenseNo": this.str.LicenseNo = (string)value; break;
						case "DateOfIssue": this.str.DateOfIssue = (string)value; break;
						case "ValidUntil": this.str.ValidUntil = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "DateOfIssue":

							if (value == null || value is System.DateTime)
								this.DateOfIssue = (System.DateTime?)value;
							break;
						case "ValidUntil":

							if (value == null || value is System.DateTime)
								this.ValidUntil = (System.DateTime?)value;
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
		/// Maps to CredentialProcessLicense.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(CredentialProcessLicenseMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(CredentialProcessLicenseMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessLicense.SRLicenseType
		/// </summary>
		virtual public System.String SRLicenseType
		{
			get
			{
				return base.GetSystemString(CredentialProcessLicenseMetadata.ColumnNames.SRLicenseType);
			}

			set
			{
				base.SetSystemString(CredentialProcessLicenseMetadata.ColumnNames.SRLicenseType, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessLicense.LicenseNo
		/// </summary>
		virtual public System.String LicenseNo
		{
			get
			{
				return base.GetSystemString(CredentialProcessLicenseMetadata.ColumnNames.LicenseNo);
			}

			set
			{
				base.SetSystemString(CredentialProcessLicenseMetadata.ColumnNames.LicenseNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessLicense.DateOfIssue
		/// </summary>
		virtual public System.DateTime? DateOfIssue
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessLicenseMetadata.ColumnNames.DateOfIssue);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessLicenseMetadata.ColumnNames.DateOfIssue, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessLicense.ValidUntil
		/// </summary>
		virtual public System.DateTime? ValidUntil
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessLicenseMetadata.ColumnNames.ValidUntil);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessLicenseMetadata.ColumnNames.ValidUntil, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessLicense.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessLicenseMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessLicenseMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessLicense.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessLicenseMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessLicenseMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCredentialProcessLicense entity)
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
			public System.String SRLicenseType
			{
				get
				{
					System.String data = entity.SRLicenseType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRLicenseType = null;
					else entity.SRLicenseType = Convert.ToString(value);
				}
			}
			public System.String LicenseNo
			{
				get
				{
					System.String data = entity.LicenseNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LicenseNo = null;
					else entity.LicenseNo = Convert.ToString(value);
				}
			}
			public System.String DateOfIssue
			{
				get
				{
					System.DateTime? data = entity.DateOfIssue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateOfIssue = null;
					else entity.DateOfIssue = Convert.ToDateTime(value);
				}
			}
			public System.String ValidUntil
			{
				get
				{
					System.DateTime? data = entity.ValidUntil;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidUntil = null;
					else entity.ValidUntil = Convert.ToDateTime(value);
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
			private esCredentialProcessLicense entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCredentialProcessLicenseQuery query)
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
				throw new Exception("esCredentialProcessLicense can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CredentialProcessLicense : esCredentialProcessLicense
	{
	}

	[Serializable]
	abstract public class esCredentialProcessLicenseQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CredentialProcessLicenseMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessLicenseMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SRLicenseType
		{
			get
			{
				return new esQueryItem(this, CredentialProcessLicenseMetadata.ColumnNames.SRLicenseType, esSystemType.String);
			}
		}

		public esQueryItem LicenseNo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessLicenseMetadata.ColumnNames.LicenseNo, esSystemType.String);
			}
		}

		public esQueryItem DateOfIssue
		{
			get
			{
				return new esQueryItem(this, CredentialProcessLicenseMetadata.ColumnNames.DateOfIssue, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidUntil
		{
			get
			{
				return new esQueryItem(this, CredentialProcessLicenseMetadata.ColumnNames.ValidUntil, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessLicenseMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessLicenseMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CredentialProcessLicenseCollection")]
	public partial class CredentialProcessLicenseCollection : esCredentialProcessLicenseCollection, IEnumerable<CredentialProcessLicense>
	{
		public CredentialProcessLicenseCollection()
		{

		}

		public static implicit operator List<CredentialProcessLicense>(CredentialProcessLicenseCollection coll)
		{
			List<CredentialProcessLicense> list = new List<CredentialProcessLicense>();

			foreach (CredentialProcessLicense emp in coll)
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
				return CredentialProcessLicenseMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialProcessLicenseQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CredentialProcessLicense(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CredentialProcessLicense();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CredentialProcessLicenseQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialProcessLicenseQuery();
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
		public bool Load(CredentialProcessLicenseQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CredentialProcessLicense AddNew()
		{
			CredentialProcessLicense entity = base.AddNewEntity() as CredentialProcessLicense;

			return entity;
		}
		public CredentialProcessLicense FindByPrimaryKey(String transactionNo, String sRLicenseType)
		{
			return base.FindByPrimaryKey(transactionNo, sRLicenseType) as CredentialProcessLicense;
		}

		#region IEnumerable< CredentialProcessLicense> Members

		IEnumerator<CredentialProcessLicense> IEnumerable<CredentialProcessLicense>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CredentialProcessLicense;
			}
		}

		#endregion

		private CredentialProcessLicenseQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CredentialProcessLicense' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CredentialProcessLicense ({TransactionNo, SRLicenseType})")]
	[Serializable]
	public partial class CredentialProcessLicense : esCredentialProcessLicense
	{
		public CredentialProcessLicense()
		{
		}

		public CredentialProcessLicense(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CredentialProcessLicenseMetadata.Meta();
			}
		}

		override protected esCredentialProcessLicenseQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialProcessLicenseQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CredentialProcessLicenseQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialProcessLicenseQuery();
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
		public bool Load(CredentialProcessLicenseQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CredentialProcessLicenseQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CredentialProcessLicenseQuery : esCredentialProcessLicenseQuery
	{
		public CredentialProcessLicenseQuery()
		{

		}

		public CredentialProcessLicenseQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CredentialProcessLicenseQuery";
		}
	}

	[Serializable]
	public partial class CredentialProcessLicenseMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CredentialProcessLicenseMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CredentialProcessLicenseMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessLicenseMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessLicenseMetadata.ColumnNames.SRLicenseType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessLicenseMetadata.PropertyNames.SRLicenseType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessLicenseMetadata.ColumnNames.LicenseNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessLicenseMetadata.PropertyNames.LicenseNo;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessLicenseMetadata.ColumnNames.DateOfIssue, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessLicenseMetadata.PropertyNames.DateOfIssue;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessLicenseMetadata.ColumnNames.ValidUntil, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessLicenseMetadata.PropertyNames.ValidUntil;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessLicenseMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessLicenseMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessLicenseMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessLicenseMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CredentialProcessLicenseMetadata Meta()
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
			public const string SRLicenseType = "SRLicenseType";
			public const string LicenseNo = "LicenseNo";
			public const string DateOfIssue = "DateOfIssue";
			public const string ValidUntil = "ValidUntil";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string SRLicenseType = "SRLicenseType";
			public const string LicenseNo = "LicenseNo";
			public const string DateOfIssue = "DateOfIssue";
			public const string ValidUntil = "ValidUntil";
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
			lock (typeof(CredentialProcessLicenseMetadata))
			{
				if (CredentialProcessLicenseMetadata.mapDelegates == null)
				{
					CredentialProcessLicenseMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CredentialProcessLicenseMetadata.meta == null)
				{
					CredentialProcessLicenseMetadata.meta = new CredentialProcessLicenseMetadata();
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
				meta.AddTypeMap("SRLicenseType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LicenseNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DateOfIssue", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidUntil", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CredentialProcessLicense";
				meta.Destination = "CredentialProcessLicense";
				meta.spInsert = "proc_CredentialProcessLicenseInsert";
				meta.spUpdate = "proc_CredentialProcessLicenseUpdate";
				meta.spDelete = "proc_CredentialProcessLicenseDelete";
				meta.spLoadAll = "proc_CredentialProcessLicenseLoadAll";
				meta.spLoadByPrimaryKey = "proc_CredentialProcessLicenseLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CredentialProcessLicenseMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
