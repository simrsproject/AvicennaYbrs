/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/28/2022 4:52:56 PM
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
	abstract public class esEsignLogHistCollection : esEntityCollectionWAuditLog
	{
		public esEsignLogHistCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EsignLogHistCollection";
		}

		#region Query Logic
		protected void InitQuery(esEsignLogHistQuery query)
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
			this.InitQuery(query as esEsignLogHistQuery);
		}
		#endregion

		virtual public EsignLogHist DetachEntity(EsignLogHist entity)
		{
			return base.DetachEntity(entity) as EsignLogHist;
		}

		virtual public EsignLogHist AttachEntity(EsignLogHist entity)
		{
			return base.AttachEntity(entity) as EsignLogHist;
		}

		virtual public void Combine(EsignLogHistCollection collection)
		{
			base.Combine(collection);
		}

		new public EsignLogHist this[int index]
		{
			get
			{
				return base[index] as EsignLogHist;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EsignLogHist);
		}
	}

	[Serializable]
	abstract public class esEsignLogHist : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEsignLogHistQuery GetDynamicQuery()
		{
			return null;
		}

		public esEsignLogHist()
		{
		}

		public esEsignLogHist(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 logID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(logID);
			else
				return LoadByPrimaryKeyStoredProcedure(logID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 logID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(logID);
			else
				return LoadByPrimaryKeyStoredProcedure(logID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 logID)
		{
			esEsignLogHistQuery query = this.GetDynamicQuery();
			query.Where(query.LogID == logID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 logID)
		{
			esParameters parms = new esParameters();
			parms.Add("LogID", logID);
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
						case "LogID": this.str.LogID = (string)value; break;
						case "ApiUrl": this.str.ApiUrl = (string)value; break;
						case "Nik": this.str.Nik = (string)value; break;
						case "ProgramID": this.str.ProgramID = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "SignedFilePath": this.str.SignedFilePath = (string)value; break;
						case "ErrorMessage": this.str.ErrorMessage = (string)value; break;
						case "StatusCode": this.str.StatusCode = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "LogID":

							if (value == null || value is System.Int64)
								this.LogID = (System.Int64?)value;
							break;
						case "StatusCode":

							if (value == null || value is System.Int32)
								this.StatusCode = (System.Int32?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to EsignLogHist.LogID
		/// </summary>
		virtual public System.Int64? LogID
		{
			get
			{
				return base.GetSystemInt64(EsignLogHistMetadata.ColumnNames.LogID);
			}

			set
			{
				base.SetSystemInt64(EsignLogHistMetadata.ColumnNames.LogID, value);
			}
		}
		/// <summary>
		/// Maps to EsignLogHist.ApiUrl
		/// </summary>
		virtual public System.String ApiUrl
		{
			get
			{
				return base.GetSystemString(EsignLogHistMetadata.ColumnNames.ApiUrl);
			}

			set
			{
				base.SetSystemString(EsignLogHistMetadata.ColumnNames.ApiUrl, value);
			}
		}
		/// <summary>
		/// Maps to EsignLogHist.Nik
		/// </summary>
		virtual public System.String Nik
		{
			get
			{
				return base.GetSystemString(EsignLogHistMetadata.ColumnNames.Nik);
			}

			set
			{
				base.SetSystemString(EsignLogHistMetadata.ColumnNames.Nik, value);
			}
		}
		/// <summary>
		/// Maps to EsignLogHist.ProgramID
		/// </summary>
		virtual public System.String ProgramID
		{
			get
			{
				return base.GetSystemString(EsignLogHistMetadata.ColumnNames.ProgramID);
			}

			set
			{
				base.SetSystemString(EsignLogHistMetadata.ColumnNames.ProgramID, value);
			}
		}
		/// <summary>
		/// Maps to EsignLogHist.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(EsignLogHistMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(EsignLogHistMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to EsignLogHist.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(EsignLogHistMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(EsignLogHistMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to EsignLogHist.SignedFilePath
		/// </summary>
		virtual public System.String SignedFilePath
		{
			get
			{
				return base.GetSystemString(EsignLogHistMetadata.ColumnNames.SignedFilePath);
			}

			set
			{
				base.SetSystemString(EsignLogHistMetadata.ColumnNames.SignedFilePath, value);
			}
		}
		/// <summary>
		/// Maps to EsignLogHist.ErrorMessage
		/// </summary>
		virtual public System.String ErrorMessage
		{
			get
			{
				return base.GetSystemString(EsignLogHistMetadata.ColumnNames.ErrorMessage);
			}

			set
			{
				base.SetSystemString(EsignLogHistMetadata.ColumnNames.ErrorMessage, value);
			}
		}
		/// <summary>
		/// Maps to EsignLogHist.StatusCode
		/// </summary>
		virtual public System.Int32? StatusCode
		{
			get
			{
				return base.GetSystemInt32(EsignLogHistMetadata.ColumnNames.StatusCode);
			}

			set
			{
				base.SetSystemInt32(EsignLogHistMetadata.ColumnNames.StatusCode, value);
			}
		}
		/// <summary>
		/// Maps to EsignLogHist.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EsignLogHistMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EsignLogHistMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EsignLogHist.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(EsignLogHistMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(EsignLogHistMetadata.ColumnNames.CreatedByUserID, value);
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
			public esStrings(esEsignLogHist entity)
			{
				this.entity = entity;
			}
			public System.String LogID
			{
				get
				{
					System.Int64? data = entity.LogID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LogID = null;
					else entity.LogID = Convert.ToInt64(value);
				}
			}
			public System.String ApiUrl
			{
				get
				{
					System.String data = entity.ApiUrl;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApiUrl = null;
					else entity.ApiUrl = Convert.ToString(value);
				}
			}
			public System.String Nik
			{
				get
				{
					System.String data = entity.Nik;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Nik = null;
					else entity.Nik = Convert.ToString(value);
				}
			}
			public System.String ProgramID
			{
				get
				{
					System.String data = entity.ProgramID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProgramID = null;
					else entity.ProgramID = Convert.ToString(value);
				}
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
			public System.String SignedFilePath
			{
				get
				{
					System.String data = entity.SignedFilePath;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SignedFilePath = null;
					else entity.SignedFilePath = Convert.ToString(value);
				}
			}
			public System.String ErrorMessage
			{
				get
				{
					System.String data = entity.ErrorMessage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ErrorMessage = null;
					else entity.ErrorMessage = Convert.ToString(value);
				}
			}
			public System.String StatusCode
			{
				get
				{
					System.Int32? data = entity.StatusCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StatusCode = null;
					else entity.StatusCode = Convert.ToInt32(value);
				}
			}
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
				}
			}
			private esEsignLogHist entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEsignLogHistQuery query)
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
				throw new Exception("esEsignLogHist can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EsignLogHist : esEsignLogHist
	{
	}

	[Serializable]
	abstract public class esEsignLogHistQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EsignLogHistMetadata.Meta();
			}
		}

		public esQueryItem LogID
		{
			get
			{
				return new esQueryItem(this, EsignLogHistMetadata.ColumnNames.LogID, esSystemType.Int64);
			}
		}

		public esQueryItem ApiUrl
		{
			get
			{
				return new esQueryItem(this, EsignLogHistMetadata.ColumnNames.ApiUrl, esSystemType.String);
			}
		}

		public esQueryItem Nik
		{
			get
			{
				return new esQueryItem(this, EsignLogHistMetadata.ColumnNames.Nik, esSystemType.String);
			}
		}

		public esQueryItem ProgramID
		{
			get
			{
				return new esQueryItem(this, EsignLogHistMetadata.ColumnNames.ProgramID, esSystemType.String);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, EsignLogHistMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, EsignLogHistMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SignedFilePath
		{
			get
			{
				return new esQueryItem(this, EsignLogHistMetadata.ColumnNames.SignedFilePath, esSystemType.String);
			}
		}

		public esQueryItem ErrorMessage
		{
			get
			{
				return new esQueryItem(this, EsignLogHistMetadata.ColumnNames.ErrorMessage, esSystemType.String);
			}
		}

		public esQueryItem StatusCode
		{
			get
			{
				return new esQueryItem(this, EsignLogHistMetadata.ColumnNames.StatusCode, esSystemType.Int32);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, EsignLogHistMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, EsignLogHistMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EsignLogHistCollection")]
	public partial class EsignLogHistCollection : esEsignLogHistCollection, IEnumerable<EsignLogHist>
	{
		public EsignLogHistCollection()
		{

		}

		public static implicit operator List<EsignLogHist>(EsignLogHistCollection coll)
		{
			List<EsignLogHist> list = new List<EsignLogHist>();

			foreach (EsignLogHist emp in coll)
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
				return EsignLogHistMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EsignLogHistQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EsignLogHist(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EsignLogHist();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EsignLogHistQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EsignLogHistQuery();
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
		public bool Load(EsignLogHistQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EsignLogHist AddNew()
		{
			EsignLogHist entity = base.AddNewEntity() as EsignLogHist;

			return entity;
		}
		public EsignLogHist FindByPrimaryKey(Int64 logID)
		{
			return base.FindByPrimaryKey(logID) as EsignLogHist;
		}

		#region IEnumerable< EsignLogHist> Members

		IEnumerator<EsignLogHist> IEnumerable<EsignLogHist>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EsignLogHist;
			}
		}

		#endregion

		private EsignLogHistQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EsignLogHist' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EsignLogHist ({LogID})")]
	[Serializable]
	public partial class EsignLogHist : esEsignLogHist
	{
		public EsignLogHist()
		{
		}

		public EsignLogHist(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EsignLogHistMetadata.Meta();
			}
		}

		override protected esEsignLogHistQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EsignLogHistQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EsignLogHistQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EsignLogHistQuery();
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
		public bool Load(EsignLogHistQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EsignLogHistQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EsignLogHistQuery : esEsignLogHistQuery
	{
		public EsignLogHistQuery()
		{

		}

		public EsignLogHistQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EsignLogHistQuery";
		}
	}

	[Serializable]
	public partial class EsignLogHistMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EsignLogHistMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EsignLogHistMetadata.ColumnNames.LogID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = EsignLogHistMetadata.PropertyNames.LogID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(EsignLogHistMetadata.ColumnNames.ApiUrl, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EsignLogHistMetadata.PropertyNames.ApiUrl;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(EsignLogHistMetadata.ColumnNames.Nik, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EsignLogHistMetadata.PropertyNames.Nik;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EsignLogHistMetadata.ColumnNames.ProgramID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EsignLogHistMetadata.PropertyNames.ProgramID;
			c.CharacterMaxLength = 30;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EsignLogHistMetadata.ColumnNames.RegistrationNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EsignLogHistMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EsignLogHistMetadata.ColumnNames.TransactionNo, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EsignLogHistMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EsignLogHistMetadata.ColumnNames.SignedFilePath, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EsignLogHistMetadata.PropertyNames.SignedFilePath;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EsignLogHistMetadata.ColumnNames.ErrorMessage, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EsignLogHistMetadata.PropertyNames.ErrorMessage;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EsignLogHistMetadata.ColumnNames.StatusCode, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EsignLogHistMetadata.PropertyNames.StatusCode;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EsignLogHistMetadata.ColumnNames.CreatedDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EsignLogHistMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EsignLogHistMetadata.ColumnNames.CreatedByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = EsignLogHistMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EsignLogHistMetadata Meta()
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
			public const string LogID = "LogID";
			public const string ApiUrl = "ApiUrl";
			public const string Nik = "Nik";
			public const string ProgramID = "ProgramID";
			public const string RegistrationNo = "RegistrationNo";
			public const string TransactionNo = "TransactionNo";
			public const string SignedFilePath = "SignedFilePath";
			public const string ErrorMessage = "ErrorMessage";
			public const string StatusCode = "StatusCode";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string LogID = "LogID";
			public const string ApiUrl = "ApiUrl";
			public const string Nik = "Nik";
			public const string ProgramID = "ProgramID";
			public const string RegistrationNo = "RegistrationNo";
			public const string TransactionNo = "TransactionNo";
			public const string SignedFilePath = "SignedFilePath";
			public const string ErrorMessage = "ErrorMessage";
			public const string StatusCode = "StatusCode";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
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
			lock (typeof(EsignLogHistMetadata))
			{
				if (EsignLogHistMetadata.mapDelegates == null)
				{
					EsignLogHistMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EsignLogHistMetadata.meta == null)
				{
					EsignLogHistMetadata.meta = new EsignLogHistMetadata();
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

				meta.AddTypeMap("LogID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("ApiUrl", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Nik", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProgramID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SignedFilePath", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ErrorMessage", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StatusCode", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EsignLogHist";
				meta.Destination = "EsignLogHist";
				meta.spInsert = "proc_EsignLogHistInsert";
				meta.spUpdate = "proc_EsignLogHistUpdate";
				meta.spDelete = "proc_EsignLogHistDelete";
				meta.spLoadAll = "proc_EsignLogHistLoadAll";
				meta.spLoadByPrimaryKey = "proc_EsignLogHistLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EsignLogHistMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
