/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/16/2021 9:06:42 PM
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
	abstract public class esPatientMRNNameHistoryCollection : esEntityCollectionWAuditLog
	{
		public esPatientMRNNameHistoryCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PatientMRNNameHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esPatientMRNNameHistoryQuery query)
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
			this.InitQuery(query as esPatientMRNNameHistoryQuery);
		}
		#endregion

		virtual public PatientMRNNameHistory DetachEntity(PatientMRNNameHistory entity)
		{
			return base.DetachEntity(entity) as PatientMRNNameHistory;
		}

		virtual public PatientMRNNameHistory AttachEntity(PatientMRNNameHistory entity)
		{
			return base.AttachEntity(entity) as PatientMRNNameHistory;
		}

		virtual public void Combine(PatientMRNNameHistoryCollection collection)
		{
			base.Combine(collection);
		}

		new public PatientMRNNameHistory this[int index]
		{
			get
			{
				return base[index] as PatientMRNNameHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientMRNNameHistory);
		}
	}

	[Serializable]
	abstract public class esPatientMRNNameHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientMRNNameHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esPatientMRNNameHistory()
		{
		}

		public esPatientMRNNameHistory(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String patientID, DateTime updateDateTime, String updateByUserID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientID, updateDateTime, updateByUserID);
			else
				return LoadByPrimaryKeyStoredProcedure(patientID, updateDateTime, updateByUserID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String patientID, DateTime updateDateTime, String updateByUserID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientID, updateDateTime, updateByUserID);
			else
				return LoadByPrimaryKeyStoredProcedure(patientID, updateDateTime, updateByUserID);
		}

		private bool LoadByPrimaryKeyDynamic(String patientID, DateTime updateDateTime, String updateByUserID)
		{
			esPatientMRNNameHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.PatientID == patientID, query.UpdateDateTime == updateDateTime, query.UpdateByUserID == updateByUserID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String patientID, DateTime updateDateTime, String updateByUserID)
		{
			esParameters parms = new esParameters();
			parms.Add("PatientID", patientID);
			parms.Add("UpdateDateTime", updateDateTime);
			parms.Add("UpdateByUserID", updateByUserID);
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
						case "PatientID": this.str.PatientID = (string)value; break;
						case "UpdateDateTime": this.str.UpdateDateTime = (string)value; break;
						case "UpdateByUserID": this.str.UpdateByUserID = (string)value; break;
						case "FromMedicalNo": this.str.FromMedicalNo = (string)value; break;
						case "FromFirstName": this.str.FromFirstName = (string)value; break;
						case "FromMiddleName": this.str.FromMiddleName = (string)value; break;
						case "FromLastName": this.str.FromLastName = (string)value; break;
						case "ToMedicalNo": this.str.ToMedicalNo = (string)value; break;
						case "ToFirstName": this.str.ToFirstName = (string)value; break;
						case "ToMiddleName": this.str.ToMiddleName = (string)value; break;
						case "ToLastName": this.str.ToLastName = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "UpdateDateTime":

							if (value == null || value is System.DateTime)
								this.UpdateDateTime = (System.DateTime?)value;
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
		/// Maps to PatientMRNNameHistory.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to PatientMRNNameHistory.UpdateDateTime
		/// </summary>
		virtual public System.DateTime? UpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientMRNNameHistoryMetadata.ColumnNames.UpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PatientMRNNameHistoryMetadata.ColumnNames.UpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientMRNNameHistory.UpdateByUserID
		/// </summary>
		virtual public System.String UpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.UpdateByUserID);
			}

			set
			{
				base.SetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.UpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientMRNNameHistory.FromMedicalNo
		/// </summary>
		virtual public System.String FromMedicalNo
		{
			get
			{
				return base.GetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.FromMedicalNo);
			}

			set
			{
				base.SetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.FromMedicalNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientMRNNameHistory.FromFirstName
		/// </summary>
		virtual public System.String FromFirstName
		{
			get
			{
				return base.GetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.FromFirstName);
			}

			set
			{
				base.SetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.FromFirstName, value);
			}
		}
		/// <summary>
		/// Maps to PatientMRNNameHistory.FromMiddleName
		/// </summary>
		virtual public System.String FromMiddleName
		{
			get
			{
				return base.GetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.FromMiddleName);
			}

			set
			{
				base.SetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.FromMiddleName, value);
			}
		}
		/// <summary>
		/// Maps to PatientMRNNameHistory.FromLastName
		/// </summary>
		virtual public System.String FromLastName
		{
			get
			{
				return base.GetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.FromLastName);
			}

			set
			{
				base.SetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.FromLastName, value);
			}
		}
		/// <summary>
		/// Maps to PatientMRNNameHistory.ToMedicalNo
		/// </summary>
		virtual public System.String ToMedicalNo
		{
			get
			{
				return base.GetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.ToMedicalNo);
			}

			set
			{
				base.SetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.ToMedicalNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientMRNNameHistory.ToFirstName
		/// </summary>
		virtual public System.String ToFirstName
		{
			get
			{
				return base.GetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.ToFirstName);
			}

			set
			{
				base.SetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.ToFirstName, value);
			}
		}
		/// <summary>
		/// Maps to PatientMRNNameHistory.ToMiddleName
		/// </summary>
		virtual public System.String ToMiddleName
		{
			get
			{
				return base.GetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.ToMiddleName);
			}

			set
			{
				base.SetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.ToMiddleName, value);
			}
		}
		/// <summary>
		/// Maps to PatientMRNNameHistory.ToLastName
		/// </summary>
		virtual public System.String ToLastName
		{
			get
			{
				return base.GetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.ToLastName);
			}

			set
			{
				base.SetSystemString(PatientMRNNameHistoryMetadata.ColumnNames.ToLastName, value);
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
			public esStrings(esPatientMRNNameHistory entity)
			{
				this.entity = entity;
			}
			public System.String PatientID
			{
				get
				{
					System.String data = entity.PatientID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientID = null;
					else entity.PatientID = Convert.ToString(value);
				}
			}
			public System.String UpdateDateTime
			{
				get
				{
					System.DateTime? data = entity.UpdateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UpdateDateTime = null;
					else entity.UpdateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String UpdateByUserID
			{
				get
				{
					System.String data = entity.UpdateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UpdateByUserID = null;
					else entity.UpdateByUserID = Convert.ToString(value);
				}
			}
			public System.String FromMedicalNo
			{
				get
				{
					System.String data = entity.FromMedicalNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromMedicalNo = null;
					else entity.FromMedicalNo = Convert.ToString(value);
				}
			}
			public System.String FromFirstName
			{
				get
				{
					System.String data = entity.FromFirstName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromFirstName = null;
					else entity.FromFirstName = Convert.ToString(value);
				}
			}
			public System.String FromMiddleName
			{
				get
				{
					System.String data = entity.FromMiddleName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromMiddleName = null;
					else entity.FromMiddleName = Convert.ToString(value);
				}
			}
			public System.String FromLastName
			{
				get
				{
					System.String data = entity.FromLastName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromLastName = null;
					else entity.FromLastName = Convert.ToString(value);
				}
			}
			public System.String ToMedicalNo
			{
				get
				{
					System.String data = entity.ToMedicalNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToMedicalNo = null;
					else entity.ToMedicalNo = Convert.ToString(value);
				}
			}
			public System.String ToFirstName
			{
				get
				{
					System.String data = entity.ToFirstName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToFirstName = null;
					else entity.ToFirstName = Convert.ToString(value);
				}
			}
			public System.String ToMiddleName
			{
				get
				{
					System.String data = entity.ToMiddleName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToMiddleName = null;
					else entity.ToMiddleName = Convert.ToString(value);
				}
			}
			public System.String ToLastName
			{
				get
				{
					System.String data = entity.ToLastName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToLastName = null;
					else entity.ToLastName = Convert.ToString(value);
				}
			}
			private esPatientMRNNameHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientMRNNameHistoryQuery query)
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
				throw new Exception("esPatientMRNNameHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientMRNNameHistory : esPatientMRNNameHistory
	{
	}

	[Serializable]
	abstract public class esPatientMRNNameHistoryQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PatientMRNNameHistoryMetadata.Meta();
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, PatientMRNNameHistoryMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem UpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientMRNNameHistoryMetadata.ColumnNames.UpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem UpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientMRNNameHistoryMetadata.ColumnNames.UpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem FromMedicalNo
		{
			get
			{
				return new esQueryItem(this, PatientMRNNameHistoryMetadata.ColumnNames.FromMedicalNo, esSystemType.String);
			}
		}

		public esQueryItem FromFirstName
		{
			get
			{
				return new esQueryItem(this, PatientMRNNameHistoryMetadata.ColumnNames.FromFirstName, esSystemType.String);
			}
		}

		public esQueryItem FromMiddleName
		{
			get
			{
				return new esQueryItem(this, PatientMRNNameHistoryMetadata.ColumnNames.FromMiddleName, esSystemType.String);
			}
		}

		public esQueryItem FromLastName
		{
			get
			{
				return new esQueryItem(this, PatientMRNNameHistoryMetadata.ColumnNames.FromLastName, esSystemType.String);
			}
		}

		public esQueryItem ToMedicalNo
		{
			get
			{
				return new esQueryItem(this, PatientMRNNameHistoryMetadata.ColumnNames.ToMedicalNo, esSystemType.String);
			}
		}

		public esQueryItem ToFirstName
		{
			get
			{
				return new esQueryItem(this, PatientMRNNameHistoryMetadata.ColumnNames.ToFirstName, esSystemType.String);
			}
		}

		public esQueryItem ToMiddleName
		{
			get
			{
				return new esQueryItem(this, PatientMRNNameHistoryMetadata.ColumnNames.ToMiddleName, esSystemType.String);
			}
		}

		public esQueryItem ToLastName
		{
			get
			{
				return new esQueryItem(this, PatientMRNNameHistoryMetadata.ColumnNames.ToLastName, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientMRNNameHistoryCollection")]
	public partial class PatientMRNNameHistoryCollection : esPatientMRNNameHistoryCollection, IEnumerable<PatientMRNNameHistory>
	{
		public PatientMRNNameHistoryCollection()
		{

		}

		public static implicit operator List<PatientMRNNameHistory>(PatientMRNNameHistoryCollection coll)
		{
			List<PatientMRNNameHistory> list = new List<PatientMRNNameHistory>();

			foreach (PatientMRNNameHistory emp in coll)
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
				return PatientMRNNameHistoryMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientMRNNameHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientMRNNameHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientMRNNameHistory();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PatientMRNNameHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientMRNNameHistoryQuery();
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
		public bool Load(PatientMRNNameHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientMRNNameHistory AddNew()
		{
			PatientMRNNameHistory entity = base.AddNewEntity() as PatientMRNNameHistory;

			return entity;
		}
		public PatientMRNNameHistory FindByPrimaryKey(String patientID, DateTime updateDateTime, String updateByUserID)
		{
			return base.FindByPrimaryKey(patientID, updateDateTime, updateByUserID) as PatientMRNNameHistory;
		}

		#region IEnumerable< PatientMRNNameHistory> Members

		IEnumerator<PatientMRNNameHistory> IEnumerable<PatientMRNNameHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PatientMRNNameHistory;
			}
		}

		#endregion

		private PatientMRNNameHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientMRNNameHistory' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientMRNNameHistory ({PatientID, UpdateDateTime, UpdateByUserID})")]
	[Serializable]
	public partial class PatientMRNNameHistory : esPatientMRNNameHistory
	{
		public PatientMRNNameHistory()
		{
		}

		public PatientMRNNameHistory(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientMRNNameHistoryMetadata.Meta();
			}
		}

		override protected esPatientMRNNameHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientMRNNameHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PatientMRNNameHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientMRNNameHistoryQuery();
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
		public bool Load(PatientMRNNameHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PatientMRNNameHistoryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientMRNNameHistoryQuery : esPatientMRNNameHistoryQuery
	{
		public PatientMRNNameHistoryQuery()
		{

		}

		public PatientMRNNameHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PatientMRNNameHistoryQuery";
		}
	}

	[Serializable]
	public partial class PatientMRNNameHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientMRNNameHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PatientMRNNameHistoryMetadata.ColumnNames.PatientID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientMRNNameHistoryMetadata.PropertyNames.PatientID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PatientMRNNameHistoryMetadata.ColumnNames.UpdateDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientMRNNameHistoryMetadata.PropertyNames.UpdateDateTime;
			c.IsInPrimaryKey = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientMRNNameHistoryMetadata.ColumnNames.UpdateByUserID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientMRNNameHistoryMetadata.PropertyNames.UpdateByUserID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(PatientMRNNameHistoryMetadata.ColumnNames.FromMedicalNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientMRNNameHistoryMetadata.PropertyNames.FromMedicalNo;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(PatientMRNNameHistoryMetadata.ColumnNames.FromFirstName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientMRNNameHistoryMetadata.PropertyNames.FromFirstName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(PatientMRNNameHistoryMetadata.ColumnNames.FromMiddleName, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientMRNNameHistoryMetadata.PropertyNames.FromMiddleName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(PatientMRNNameHistoryMetadata.ColumnNames.FromLastName, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientMRNNameHistoryMetadata.PropertyNames.FromLastName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(PatientMRNNameHistoryMetadata.ColumnNames.ToMedicalNo, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientMRNNameHistoryMetadata.PropertyNames.ToMedicalNo;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(PatientMRNNameHistoryMetadata.ColumnNames.ToFirstName, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientMRNNameHistoryMetadata.PropertyNames.ToFirstName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(PatientMRNNameHistoryMetadata.ColumnNames.ToMiddleName, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientMRNNameHistoryMetadata.PropertyNames.ToMiddleName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(PatientMRNNameHistoryMetadata.ColumnNames.ToLastName, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientMRNNameHistoryMetadata.PropertyNames.ToLastName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);


		}
		#endregion

		static public PatientMRNNameHistoryMetadata Meta()
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
			public const string PatientID = "PatientID";
			public const string UpdateDateTime = "UpdateDateTime";
			public const string UpdateByUserID = "UpdateByUserID";
			public const string FromMedicalNo = "FromMedicalNo";
			public const string FromFirstName = "FromFirstName";
			public const string FromMiddleName = "FromMiddleName";
			public const string FromLastName = "FromLastName";
			public const string ToMedicalNo = "ToMedicalNo";
			public const string ToFirstName = "ToFirstName";
			public const string ToMiddleName = "ToMiddleName";
			public const string ToLastName = "ToLastName";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PatientID = "PatientID";
			public const string UpdateDateTime = "UpdateDateTime";
			public const string UpdateByUserID = "UpdateByUserID";
			public const string FromMedicalNo = "FromMedicalNo";
			public const string FromFirstName = "FromFirstName";
			public const string FromMiddleName = "FromMiddleName";
			public const string FromLastName = "FromLastName";
			public const string ToMedicalNo = "ToMedicalNo";
			public const string ToFirstName = "ToFirstName";
			public const string ToMiddleName = "ToMiddleName";
			public const string ToLastName = "ToLastName";
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
			lock (typeof(PatientMRNNameHistoryMetadata))
			{
				if (PatientMRNNameHistoryMetadata.mapDelegates == null)
				{
					PatientMRNNameHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PatientMRNNameHistoryMetadata.meta == null)
				{
					PatientMRNNameHistoryMetadata.meta = new PatientMRNNameHistoryMetadata();
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

				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("UpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("UpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromMedicalNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromFirstName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromMiddleName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromLastName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToMedicalNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToFirstName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToMiddleName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToLastName", new esTypeMap("varchar", "System.String"));


				meta.Source = "PatientMRNNameHistory";
				meta.Destination = "PatientMRNNameHistory";
				meta.spInsert = "proc_PatientMRNNameHistoryInsert";
				meta.spUpdate = "proc_PatientMRNNameHistoryUpdate";
				meta.spDelete = "proc_PatientMRNNameHistoryDelete";
				meta.spLoadAll = "proc_PatientMRNNameHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientMRNNameHistoryLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientMRNNameHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
