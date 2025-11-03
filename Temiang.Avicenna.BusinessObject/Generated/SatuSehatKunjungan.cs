/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/7/2022 5:39:50 PM
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
	abstract public class esSatuSehatKunjunganCollection : esEntityCollectionWAuditLog
	{
		public esSatuSehatKunjunganCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "SatuSehatKunjunganCollection";
		}

		#region Query Logic
		protected void InitQuery(esSatuSehatKunjunganQuery query)
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
			this.InitQuery(query as esSatuSehatKunjunganQuery);
		}
		#endregion

		virtual public SatuSehatKunjungan DetachEntity(SatuSehatKunjungan entity)
		{
			return base.DetachEntity(entity) as SatuSehatKunjungan;
		}

		virtual public SatuSehatKunjungan AttachEntity(SatuSehatKunjungan entity)
		{
			return base.AttachEntity(entity) as SatuSehatKunjungan;
		}

		virtual public void Combine(SatuSehatKunjunganCollection collection)
		{
			base.Combine(collection);
		}

		new public SatuSehatKunjungan this[int index]
		{
			get
			{
				return base[index] as SatuSehatKunjungan;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SatuSehatKunjungan);
		}
	}

	[Serializable]
	abstract public class esSatuSehatKunjungan : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSatuSehatKunjunganQuery GetDynamicQuery()
		{
			return null;
		}

		public esSatuSehatKunjungan()
		{
		}

		public esSatuSehatKunjungan(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo)
		{
			esSatuSehatKunjunganQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "EncounterID": this.str.EncounterID = (string)value; break;
						case "KunjunganPostData": this.str.KunjunganPostData = (string)value; break;
						case "IsClosed": this.str.IsClosed = (string)value; break;
						case "ErrorResponse": this.str.ErrorResponse = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EncounterID":

							if (value == null || value is System.Guid)
								this.EncounterID = (System.Guid?)value;
							break;
						case "IsClosed":

							if (value == null || value is System.Boolean)
								this.IsClosed = (System.Boolean?)value;
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
		/// Maps to SatuSehatKunjungan.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(SatuSehatKunjunganMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(SatuSehatKunjunganMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatKunjungan.EncounterID
		/// </summary>
		virtual public System.Guid? EncounterID
		{
			get
			{
				return base.GetSystemGuid(SatuSehatKunjunganMetadata.ColumnNames.EncounterID);
			}

			set
			{
				base.SetSystemGuid(SatuSehatKunjunganMetadata.ColumnNames.EncounterID, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatKunjungan.KunjunganPostData
		/// </summary>
		virtual public System.String KunjunganPostData
		{
			get
			{
				return base.GetSystemString(SatuSehatKunjunganMetadata.ColumnNames.KunjunganPostData);
			}

			set
			{
				base.SetSystemString(SatuSehatKunjunganMetadata.ColumnNames.KunjunganPostData, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatKunjungan.IsClosed
		/// </summary>
		virtual public System.Boolean? IsClosed
		{
			get
			{
				return base.GetSystemBoolean(SatuSehatKunjunganMetadata.ColumnNames.IsClosed);
			}

			set
			{
				base.SetSystemBoolean(SatuSehatKunjunganMetadata.ColumnNames.IsClosed, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatKunjungan.ErrorResponse
		/// </summary>
		virtual public System.String ErrorResponse
		{
			get
			{
				return base.GetSystemString(SatuSehatKunjunganMetadata.ColumnNames.ErrorResponse);
			}

			set
			{
				base.SetSystemString(SatuSehatKunjunganMetadata.ColumnNames.ErrorResponse, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatKunjungan.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SatuSehatKunjunganMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(SatuSehatKunjunganMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatKunjungan.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SatuSehatKunjunganMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(SatuSehatKunjunganMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSatuSehatKunjungan entity)
			{
				this.entity = entity;
			}
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
			public System.String EncounterID
			{
				get
				{
					System.Guid? data = entity.EncounterID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EncounterID = null;
					else entity.EncounterID = new Guid(value);
				}
			}
			public System.String KunjunganPostData
			{
				get
				{
					System.String data = entity.KunjunganPostData;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KunjunganPostData = null;
					else entity.KunjunganPostData = Convert.ToString(value);
				}
			}
			public System.String IsClosed
			{
				get
				{
					System.Boolean? data = entity.IsClosed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClosed = null;
					else entity.IsClosed = Convert.ToBoolean(value);
				}
			}
			public System.String ErrorResponse
			{
				get
				{
					System.String data = entity.ErrorResponse;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ErrorResponse = null;
					else entity.ErrorResponse = Convert.ToString(value);
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
			private esSatuSehatKunjungan entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSatuSehatKunjunganQuery query)
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
				throw new Exception("esSatuSehatKunjungan can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SatuSehatKunjungan : esSatuSehatKunjungan
	{
	}

	[Serializable]
	abstract public class esSatuSehatKunjunganQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return SatuSehatKunjunganMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, SatuSehatKunjunganMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem EncounterID
		{
			get
			{
				return new esQueryItem(this, SatuSehatKunjunganMetadata.ColumnNames.EncounterID, esSystemType.Guid);
			}
		}

		public esQueryItem KunjunganPostData
		{
			get
			{
				return new esQueryItem(this, SatuSehatKunjunganMetadata.ColumnNames.KunjunganPostData, esSystemType.String);
			}
		}

		public esQueryItem IsClosed
		{
			get
			{
				return new esQueryItem(this, SatuSehatKunjunganMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
			}
		}

		public esQueryItem ErrorResponse
		{
			get
			{
				return new esQueryItem(this, SatuSehatKunjunganMetadata.ColumnNames.ErrorResponse, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SatuSehatKunjunganMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SatuSehatKunjunganMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SatuSehatKunjunganCollection")]
	public partial class SatuSehatKunjunganCollection : esSatuSehatKunjunganCollection, IEnumerable<SatuSehatKunjungan>
	{
		public SatuSehatKunjunganCollection()
		{

		}

		public static implicit operator List<SatuSehatKunjungan>(SatuSehatKunjunganCollection coll)
		{
			List<SatuSehatKunjungan> list = new List<SatuSehatKunjungan>();

			foreach (SatuSehatKunjungan emp in coll)
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
				return SatuSehatKunjunganMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SatuSehatKunjunganQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SatuSehatKunjungan(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SatuSehatKunjungan();
		}

		#endregion

		[BrowsableAttribute(false)]
		public SatuSehatKunjunganQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SatuSehatKunjunganQuery();
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
		public bool Load(SatuSehatKunjunganQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SatuSehatKunjungan AddNew()
		{
			SatuSehatKunjungan entity = base.AddNewEntity() as SatuSehatKunjungan;

			return entity;
		}
		public SatuSehatKunjungan FindByPrimaryKey(String registrationNo)
		{
			return base.FindByPrimaryKey(registrationNo) as SatuSehatKunjungan;
		}

		#region IEnumerable< SatuSehatKunjungan> Members

		IEnumerator<SatuSehatKunjungan> IEnumerable<SatuSehatKunjungan>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as SatuSehatKunjungan;
			}
		}

		#endregion

		private SatuSehatKunjunganQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SatuSehatKunjungan' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SatuSehatKunjungan ({RegistrationNo})")]
	[Serializable]
	public partial class SatuSehatKunjungan : esSatuSehatKunjungan
	{
		public SatuSehatKunjungan()
		{
		}

		public SatuSehatKunjungan(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SatuSehatKunjunganMetadata.Meta();
			}
		}

		override protected esSatuSehatKunjunganQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SatuSehatKunjunganQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public SatuSehatKunjunganQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SatuSehatKunjunganQuery();
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
		public bool Load(SatuSehatKunjunganQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private SatuSehatKunjunganQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SatuSehatKunjunganQuery : esSatuSehatKunjunganQuery
	{
		public SatuSehatKunjunganQuery()
		{

		}

		public SatuSehatKunjunganQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SatuSehatKunjunganQuery";
		}
	}

	[Serializable]
	public partial class SatuSehatKunjunganMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SatuSehatKunjunganMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SatuSehatKunjunganMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = SatuSehatKunjunganMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SatuSehatKunjunganMetadata.ColumnNames.EncounterID, 1, typeof(System.Guid), esSystemType.Guid);
			c.PropertyName = SatuSehatKunjunganMetadata.PropertyNames.EncounterID;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SatuSehatKunjunganMetadata.ColumnNames.KunjunganPostData, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = SatuSehatKunjunganMetadata.PropertyNames.KunjunganPostData;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SatuSehatKunjunganMetadata.ColumnNames.IsClosed, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SatuSehatKunjunganMetadata.PropertyNames.IsClosed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SatuSehatKunjunganMetadata.ColumnNames.ErrorResponse, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = SatuSehatKunjunganMetadata.PropertyNames.ErrorResponse;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SatuSehatKunjunganMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SatuSehatKunjunganMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(SatuSehatKunjunganMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = SatuSehatKunjunganMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);


		}
		#endregion

		static public SatuSehatKunjunganMetadata Meta()
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
			public const string RegistrationNo = "RegistrationNo";
			public const string EncounterID = "EncounterID";
			public const string KunjunganPostData = "KunjunganPostData";
			public const string IsClosed = "IsClosed";
			public const string ErrorResponse = "ErrorResponse";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string EncounterID = "EncounterID";
			public const string KunjunganPostData = "KunjunganPostData";
			public const string IsClosed = "IsClosed";
			public const string ErrorResponse = "ErrorResponse";
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
			lock (typeof(SatuSehatKunjunganMetadata))
			{
				if (SatuSehatKunjunganMetadata.mapDelegates == null)
				{
					SatuSehatKunjunganMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (SatuSehatKunjunganMetadata.meta == null)
				{
					SatuSehatKunjunganMetadata.meta = new SatuSehatKunjunganMetadata();
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

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EncounterID", new esTypeMap("uniqueidentifier", "System.Guid"));
				meta.AddTypeMap("KunjunganPostData", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ErrorResponse", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "SatuSehatKunjungan";
				meta.Destination = "SatuSehatKunjungan";
				meta.spInsert = "proc_SatuSehatKunjunganInsert";
				meta.spUpdate = "proc_SatuSehatKunjunganUpdate";
				meta.spDelete = "proc_SatuSehatKunjunganDelete";
				meta.spLoadAll = "proc_SatuSehatKunjunganLoadAll";
				meta.spLoadByPrimaryKey = "proc_SatuSehatKunjunganLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SatuSehatKunjunganMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
