/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/20/2023 11:07:09 AM
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
	abstract public class esPatientDischargeHistoryCollection : esEntityCollectionWAuditLog
	{
		public esPatientDischargeHistoryCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PatientDischargeHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esPatientDischargeHistoryQuery query)
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
			this.InitQuery(query as esPatientDischargeHistoryQuery);
		}
		#endregion

		virtual public PatientDischargeHistory DetachEntity(PatientDischargeHistory entity)
		{
			return base.DetachEntity(entity) as PatientDischargeHistory;
		}

		virtual public PatientDischargeHistory AttachEntity(PatientDischargeHistory entity)
		{
			return base.AttachEntity(entity) as PatientDischargeHistory;
		}

		virtual public void Combine(PatientDischargeHistoryCollection collection)
		{
			base.Combine(collection);
		}

		new public PatientDischargeHistory this[int index]
		{
			get
			{
				return base[index] as PatientDischargeHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientDischargeHistory);
		}
	}

	[Serializable]
	abstract public class esPatientDischargeHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientDischargeHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esPatientDischargeHistory()
		{
		}

		public esPatientDischargeHistory(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, String bedID, DateTime dischargeDate, String dischargeTime, DateTime lastUpdateDateTime)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, bedID, dischargeDate, dischargeTime, lastUpdateDateTime);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, bedID, dischargeDate, dischargeTime, lastUpdateDateTime);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String bedID, DateTime dischargeDate, String dischargeTime, DateTime lastUpdateDateTime)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, bedID, dischargeDate, dischargeTime, lastUpdateDateTime);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, bedID, dischargeDate, dischargeTime, lastUpdateDateTime);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, String bedID, DateTime dischargeDate, String dischargeTime, DateTime lastUpdateDateTime)
		{
			esPatientDischargeHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.BedID == bedID, query.DischargeDate == dischargeDate, query.DischargeTime == dischargeTime, query.LastUpdateDateTime == lastUpdateDateTime);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String bedID, DateTime dischargeDate, String dischargeTime, DateTime lastUpdateDateTime)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("BedID", bedID);
			parms.Add("DischargeDate", dischargeDate);
			parms.Add("DischargeTime", dischargeTime);
			parms.Add("LastUpdateDateTime", lastUpdateDateTime);
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
						case "BedID": this.str.BedID = (string)value; break;
						case "DischargeDate": this.str.DischargeDate = (string)value; break;
						case "DischargeTime": this.str.DischargeTime = (string)value; break;
						case "DischargeOperatorID": this.str.DischargeOperatorID = (string)value; break;
						case "IsCancel": this.str.IsCancel = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "SRDischargeMethod": this.str.SRDischargeMethod = (string)value; break;
						case "SRDischargeCondition": this.str.SRDischargeCondition = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "DischargeDate":

							if (value == null || value is System.DateTime)
								this.DischargeDate = (System.DateTime?)value;
							break;
						case "IsCancel":

							if (value == null || value is System.Boolean)
								this.IsCancel = (System.Boolean?)value;
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
		/// Maps to PatientDischargeHistory.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(PatientDischargeHistoryMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(PatientDischargeHistoryMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientDischargeHistory.BedID
		/// </summary>
		virtual public System.String BedID
		{
			get
			{
				return base.GetSystemString(PatientDischargeHistoryMetadata.ColumnNames.BedID);
			}

			set
			{
				base.SetSystemString(PatientDischargeHistoryMetadata.ColumnNames.BedID, value);
			}
		}
		/// <summary>
		/// Maps to PatientDischargeHistory.DischargeDate
		/// </summary>
		virtual public System.DateTime? DischargeDate
		{
			get
			{
				return base.GetSystemDateTime(PatientDischargeHistoryMetadata.ColumnNames.DischargeDate);
			}

			set
			{
				base.SetSystemDateTime(PatientDischargeHistoryMetadata.ColumnNames.DischargeDate, value);
			}
		}
		/// <summary>
		/// Maps to PatientDischargeHistory.DischargeTime
		/// </summary>
		virtual public System.String DischargeTime
		{
			get
			{
				return base.GetSystemString(PatientDischargeHistoryMetadata.ColumnNames.DischargeTime);
			}

			set
			{
				base.SetSystemString(PatientDischargeHistoryMetadata.ColumnNames.DischargeTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientDischargeHistory.DischargeOperatorID
		/// </summary>
		virtual public System.String DischargeOperatorID
		{
			get
			{
				return base.GetSystemString(PatientDischargeHistoryMetadata.ColumnNames.DischargeOperatorID);
			}

			set
			{
				base.SetSystemString(PatientDischargeHistoryMetadata.ColumnNames.DischargeOperatorID, value);
			}
		}
		/// <summary>
		/// Maps to PatientDischargeHistory.IsCancel
		/// </summary>
		virtual public System.Boolean? IsCancel
		{
			get
			{
				return base.GetSystemBoolean(PatientDischargeHistoryMetadata.ColumnNames.IsCancel);
			}

			set
			{
				base.SetSystemBoolean(PatientDischargeHistoryMetadata.ColumnNames.IsCancel, value);
			}
		}
		/// <summary>
		/// Maps to PatientDischargeHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientDischargeHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PatientDischargeHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientDischargeHistory.SRDischargeMethod
		/// </summary>
		virtual public System.String SRDischargeMethod
		{
			get
			{
				return base.GetSystemString(PatientDischargeHistoryMetadata.ColumnNames.SRDischargeMethod);
			}

			set
			{
				base.SetSystemString(PatientDischargeHistoryMetadata.ColumnNames.SRDischargeMethod, value);
			}
		}
		/// <summary>
		/// Maps to PatientDischargeHistory.SRDischargeCondition
		/// </summary>
		virtual public System.String SRDischargeCondition
		{
			get
			{
				return base.GetSystemString(PatientDischargeHistoryMetadata.ColumnNames.SRDischargeCondition);
			}

			set
			{
				base.SetSystemString(PatientDischargeHistoryMetadata.ColumnNames.SRDischargeCondition, value);
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
			public esStrings(esPatientDischargeHistory entity)
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
			public System.String BedID
			{
				get
				{
					System.String data = entity.BedID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BedID = null;
					else entity.BedID = Convert.ToString(value);
				}
			}
			public System.String DischargeDate
			{
				get
				{
					System.DateTime? data = entity.DischargeDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DischargeDate = null;
					else entity.DischargeDate = Convert.ToDateTime(value);
				}
			}
			public System.String DischargeTime
			{
				get
				{
					System.String data = entity.DischargeTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DischargeTime = null;
					else entity.DischargeTime = Convert.ToString(value);
				}
			}
			public System.String DischargeOperatorID
			{
				get
				{
					System.String data = entity.DischargeOperatorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DischargeOperatorID = null;
					else entity.DischargeOperatorID = Convert.ToString(value);
				}
			}
			public System.String IsCancel
			{
				get
				{
					System.Boolean? data = entity.IsCancel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCancel = null;
					else entity.IsCancel = Convert.ToBoolean(value);
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
			public System.String SRDischargeMethod
			{
				get
				{
					System.String data = entity.SRDischargeMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDischargeMethod = null;
					else entity.SRDischargeMethod = Convert.ToString(value);
				}
			}
			public System.String SRDischargeCondition
			{
				get
				{
					System.String data = entity.SRDischargeCondition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDischargeCondition = null;
					else entity.SRDischargeCondition = Convert.ToString(value);
				}
			}
			private esPatientDischargeHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientDischargeHistoryQuery query)
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
				throw new Exception("esPatientDischargeHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientDischargeHistory : esPatientDischargeHistory
	{
	}

	[Serializable]
	abstract public class esPatientDischargeHistoryQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PatientDischargeHistoryMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, PatientDischargeHistoryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem BedID
		{
			get
			{
				return new esQueryItem(this, PatientDischargeHistoryMetadata.ColumnNames.BedID, esSystemType.String);
			}
		}

		public esQueryItem DischargeDate
		{
			get
			{
				return new esQueryItem(this, PatientDischargeHistoryMetadata.ColumnNames.DischargeDate, esSystemType.DateTime);
			}
		}

		public esQueryItem DischargeTime
		{
			get
			{
				return new esQueryItem(this, PatientDischargeHistoryMetadata.ColumnNames.DischargeTime, esSystemType.String);
			}
		}

		public esQueryItem DischargeOperatorID
		{
			get
			{
				return new esQueryItem(this, PatientDischargeHistoryMetadata.ColumnNames.DischargeOperatorID, esSystemType.String);
			}
		}

		public esQueryItem IsCancel
		{
			get
			{
				return new esQueryItem(this, PatientDischargeHistoryMetadata.ColumnNames.IsCancel, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientDischargeHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem SRDischargeMethod
		{
			get
			{
				return new esQueryItem(this, PatientDischargeHistoryMetadata.ColumnNames.SRDischargeMethod, esSystemType.String);
			}
		}

		public esQueryItem SRDischargeCondition
		{
			get
			{
				return new esQueryItem(this, PatientDischargeHistoryMetadata.ColumnNames.SRDischargeCondition, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientDischargeHistoryCollection")]
	public partial class PatientDischargeHistoryCollection : esPatientDischargeHistoryCollection, IEnumerable<PatientDischargeHistory>
	{
		public PatientDischargeHistoryCollection()
		{

		}

		public static implicit operator List<PatientDischargeHistory>(PatientDischargeHistoryCollection coll)
		{
			List<PatientDischargeHistory> list = new List<PatientDischargeHistory>();

			foreach (PatientDischargeHistory emp in coll)
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
				return PatientDischargeHistoryMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientDischargeHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientDischargeHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientDischargeHistory();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PatientDischargeHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientDischargeHistoryQuery();
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
		public bool Load(PatientDischargeHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientDischargeHistory AddNew()
		{
			PatientDischargeHistory entity = base.AddNewEntity() as PatientDischargeHistory;

			return entity;
		}
		public PatientDischargeHistory FindByPrimaryKey(String registrationNo, String bedID, DateTime dischargeDate, String dischargeTime, DateTime lastUpdateDateTime)
		{
			return base.FindByPrimaryKey(registrationNo, bedID, dischargeDate, dischargeTime, lastUpdateDateTime) as PatientDischargeHistory;
		}

		#region IEnumerable< PatientDischargeHistory> Members

		IEnumerator<PatientDischargeHistory> IEnumerable<PatientDischargeHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PatientDischargeHistory;
			}
		}

		#endregion

		private PatientDischargeHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientDischargeHistory' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientDischargeHistory ({RegistrationNo, BedID, DischargeDate, DischargeTime, LastUpdateDateTime})")]
	[Serializable]
	public partial class PatientDischargeHistory : esPatientDischargeHistory
	{
		public PatientDischargeHistory()
		{
		}

		public PatientDischargeHistory(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientDischargeHistoryMetadata.Meta();
			}
		}

		override protected esPatientDischargeHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientDischargeHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PatientDischargeHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientDischargeHistoryQuery();
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
		public bool Load(PatientDischargeHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PatientDischargeHistoryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientDischargeHistoryQuery : esPatientDischargeHistoryQuery
	{
		public PatientDischargeHistoryQuery()
		{

		}

		public PatientDischargeHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PatientDischargeHistoryQuery";
		}
	}

	[Serializable]
	public partial class PatientDischargeHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientDischargeHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PatientDischargeHistoryMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDischargeHistoryMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDischargeHistoryMetadata.ColumnNames.BedID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDischargeHistoryMetadata.PropertyNames.BedID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDischargeHistoryMetadata.ColumnNames.DischargeDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientDischargeHistoryMetadata.PropertyNames.DischargeDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDischargeHistoryMetadata.ColumnNames.DischargeTime, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDischargeHistoryMetadata.PropertyNames.DischargeTime;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDischargeHistoryMetadata.ColumnNames.DischargeOperatorID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDischargeHistoryMetadata.PropertyNames.DischargeOperatorID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDischargeHistoryMetadata.ColumnNames.IsCancel, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PatientDischargeHistoryMetadata.PropertyNames.IsCancel;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDischargeHistoryMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientDischargeHistoryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsInPrimaryKey = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDischargeHistoryMetadata.ColumnNames.SRDischargeMethod, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDischargeHistoryMetadata.PropertyNames.SRDischargeMethod;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDischargeHistoryMetadata.ColumnNames.SRDischargeCondition, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDischargeHistoryMetadata.PropertyNames.SRDischargeCondition;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PatientDischargeHistoryMetadata Meta()
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
			public const string BedID = "BedID";
			public const string DischargeDate = "DischargeDate";
			public const string DischargeTime = "DischargeTime";
			public const string DischargeOperatorID = "DischargeOperatorID";
			public const string IsCancel = "IsCancel";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string SRDischargeMethod = "SRDischargeMethod";
			public const string SRDischargeCondition = "SRDischargeCondition";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string BedID = "BedID";
			public const string DischargeDate = "DischargeDate";
			public const string DischargeTime = "DischargeTime";
			public const string DischargeOperatorID = "DischargeOperatorID";
			public const string IsCancel = "IsCancel";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string SRDischargeMethod = "SRDischargeMethod";
			public const string SRDischargeCondition = "SRDischargeCondition";
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
			lock (typeof(PatientDischargeHistoryMetadata))
			{
				if (PatientDischargeHistoryMetadata.mapDelegates == null)
				{
					PatientDischargeHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PatientDischargeHistoryMetadata.meta == null)
				{
					PatientDischargeHistoryMetadata.meta = new PatientDischargeHistoryMetadata();
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
				meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DischargeDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("DischargeTime", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("DischargeOperatorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsCancel", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRDischargeMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDischargeCondition", new esTypeMap("varchar", "System.String"));


				meta.Source = "PatientDischargeHistory";
				meta.Destination = "PatientDischargeHistory";
				meta.spInsert = "proc_PatientDischargeHistoryInsert";
				meta.spUpdate = "proc_PatientDischargeHistoryUpdate";
				meta.spDelete = "proc_PatientDischargeHistoryDelete";
				meta.spLoadAll = "proc_PatientDischargeHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientDischargeHistoryLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientDischargeHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
