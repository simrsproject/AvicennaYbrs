/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/5/2023 2:21:46 PM
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
	abstract public class esMedicalRecordFileCompletenessHistoryCollection : esEntityCollectionWAuditLog
	{
		public esMedicalRecordFileCompletenessHistoryCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MedicalRecordFileCompletenessHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esMedicalRecordFileCompletenessHistoryQuery query)
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
			this.InitQuery(query as esMedicalRecordFileCompletenessHistoryQuery);
		}
		#endregion

		virtual public MedicalRecordFileCompletenessHistory DetachEntity(MedicalRecordFileCompletenessHistory entity)
		{
			return base.DetachEntity(entity) as MedicalRecordFileCompletenessHistory;
		}

		virtual public MedicalRecordFileCompletenessHistory AttachEntity(MedicalRecordFileCompletenessHistory entity)
		{
			return base.AttachEntity(entity) as MedicalRecordFileCompletenessHistory;
		}

		virtual public void Combine(MedicalRecordFileCompletenessHistoryCollection collection)
		{
			base.Combine(collection);
		}

		new public MedicalRecordFileCompletenessHistory this[int index]
		{
			get
			{
				return base[index] as MedicalRecordFileCompletenessHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicalRecordFileCompletenessHistory);
		}
	}

	[Serializable]
	abstract public class esMedicalRecordFileCompletenessHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicalRecordFileCompletenessHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esMedicalRecordFileCompletenessHistory()
		{
		}

		public esMedicalRecordFileCompletenessHistory(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 txId)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(txId);
			else
				return LoadByPrimaryKeyStoredProcedure(txId);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 txId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(txId);
			else
				return LoadByPrimaryKeyStoredProcedure(txId);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 txId)
		{
			esMedicalRecordFileCompletenessHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.TxId == txId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 txId)
		{
			esParameters parms = new esParameters();
			parms.Add("TxId", txId);
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
						case "TxId": this.str.TxId = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "SubmitDate": this.str.SubmitDate = (string)value; break;
						case "SubmitNotes": this.str.SubmitNotes = (string)value; break;
						case "ReturnDate": this.str.ReturnDate = (string)value; break;
						case "ReturnNotes": this.str.ReturnNotes = (string)value; break;
						case "SubmitDateTime": this.str.SubmitDateTime = (string)value; break;
						case "SubmitByUserID": this.str.SubmitByUserID = (string)value; break;
						case "ReturnDateTime": this.str.ReturnDateTime = (string)value; break;
						case "ReturnByUserID": this.str.ReturnByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "TxId":

							if (value == null || value is System.Int64)
								this.TxId = (System.Int64?)value;
							break;
						case "SubmitDate":

							if (value == null || value is System.DateTime)
								this.SubmitDate = (System.DateTime?)value;
							break;
						case "ReturnDate":

							if (value == null || value is System.DateTime)
								this.ReturnDate = (System.DateTime?)value;
							break;
						case "SubmitDateTime":

							if (value == null || value is System.DateTime)
								this.SubmitDateTime = (System.DateTime?)value;
							break;
						case "ReturnDateTime":

							if (value == null || value is System.DateTime)
								this.ReturnDateTime = (System.DateTime?)value;
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
		/// Maps to MedicalRecordFileCompletenessHistory.TxId
		/// </summary>
		virtual public System.Int64? TxId
		{
			get
			{
				return base.GetSystemInt64(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.TxId);
			}

			set
			{
				base.SetSystemInt64(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.TxId, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessHistory.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessHistory.SubmitDate
		/// </summary>
		virtual public System.DateTime? SubmitDate
		{
			get
			{
				return base.GetSystemDateTime(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.SubmitDate);
			}

			set
			{
				base.SetSystemDateTime(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.SubmitDate, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessHistory.SubmitNotes
		/// </summary>
		virtual public System.String SubmitNotes
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.SubmitNotes);
			}

			set
			{
				base.SetSystemString(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.SubmitNotes, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessHistory.ReturnDate
		/// </summary>
		virtual public System.DateTime? ReturnDate
		{
			get
			{
				return base.GetSystemDateTime(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnDate);
			}

			set
			{
				base.SetSystemDateTime(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnDate, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessHistory.ReturnNotes
		/// </summary>
		virtual public System.String ReturnNotes
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnNotes);
			}

			set
			{
				base.SetSystemString(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnNotes, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessHistory.SubmitDateTime
		/// </summary>
		virtual public System.DateTime? SubmitDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.SubmitDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.SubmitDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessHistory.SubmitByUserID
		/// </summary>
		virtual public System.String SubmitByUserID
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.SubmitByUserID);
			}

			set
			{
				base.SetSystemString(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.SubmitByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessHistory.ReturnDateTime
		/// </summary>
		virtual public System.DateTime? ReturnDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessHistory.ReturnByUserID
		/// </summary>
		virtual public System.String ReturnByUserID
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnByUserID);
			}

			set
			{
				base.SetSystemString(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMedicalRecordFileCompletenessHistory entity)
			{
				this.entity = entity;
			}
			public System.String TxId
			{
				get
				{
					System.Int64? data = entity.TxId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TxId = null;
					else entity.TxId = Convert.ToInt64(value);
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
			public System.String SubmitDate
			{
				get
				{
					System.DateTime? data = entity.SubmitDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubmitDate = null;
					else entity.SubmitDate = Convert.ToDateTime(value);
				}
			}
			public System.String SubmitNotes
			{
				get
				{
					System.String data = entity.SubmitNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubmitNotes = null;
					else entity.SubmitNotes = Convert.ToString(value);
				}
			}
			public System.String ReturnDate
			{
				get
				{
					System.DateTime? data = entity.ReturnDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReturnDate = null;
					else entity.ReturnDate = Convert.ToDateTime(value);
				}
			}
			public System.String ReturnNotes
			{
				get
				{
					System.String data = entity.ReturnNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReturnNotes = null;
					else entity.ReturnNotes = Convert.ToString(value);
				}
			}
			public System.String SubmitDateTime
			{
				get
				{
					System.DateTime? data = entity.SubmitDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubmitDateTime = null;
					else entity.SubmitDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String SubmitByUserID
			{
				get
				{
					System.String data = entity.SubmitByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubmitByUserID = null;
					else entity.SubmitByUserID = Convert.ToString(value);
				}
			}
			public System.String ReturnDateTime
			{
				get
				{
					System.DateTime? data = entity.ReturnDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReturnDateTime = null;
					else entity.ReturnDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ReturnByUserID
			{
				get
				{
					System.String data = entity.ReturnByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReturnByUserID = null;
					else entity.ReturnByUserID = Convert.ToString(value);
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
			private esMedicalRecordFileCompletenessHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicalRecordFileCompletenessHistoryQuery query)
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
				throw new Exception("esMedicalRecordFileCompletenessHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MedicalRecordFileCompletenessHistory : esMedicalRecordFileCompletenessHistory
	{
	}

	[Serializable]
	abstract public class esMedicalRecordFileCompletenessHistoryQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MedicalRecordFileCompletenessHistoryMetadata.Meta();
			}
		}

		public esQueryItem TxId
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.TxId, esSystemType.Int64);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem SubmitDate
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.SubmitDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SubmitNotes
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.SubmitNotes, esSystemType.String);
			}
		}

		public esQueryItem ReturnDate
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ReturnNotes
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnNotes, esSystemType.String);
			}
		}

		public esQueryItem SubmitDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.SubmitDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem SubmitByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.SubmitByUserID, esSystemType.String);
			}
		}

		public esQueryItem ReturnDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ReturnByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicalRecordFileCompletenessHistoryCollection")]
	public partial class MedicalRecordFileCompletenessHistoryCollection : esMedicalRecordFileCompletenessHistoryCollection, IEnumerable<MedicalRecordFileCompletenessHistory>
	{
		public MedicalRecordFileCompletenessHistoryCollection()
		{

		}

		public static implicit operator List<MedicalRecordFileCompletenessHistory>(MedicalRecordFileCompletenessHistoryCollection coll)
		{
			List<MedicalRecordFileCompletenessHistory> list = new List<MedicalRecordFileCompletenessHistory>();

			foreach (MedicalRecordFileCompletenessHistory emp in coll)
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
				return MedicalRecordFileCompletenessHistoryMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalRecordFileCompletenessHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicalRecordFileCompletenessHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicalRecordFileCompletenessHistory();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MedicalRecordFileCompletenessHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalRecordFileCompletenessHistoryQuery();
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
		public bool Load(MedicalRecordFileCompletenessHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MedicalRecordFileCompletenessHistory AddNew()
		{
			MedicalRecordFileCompletenessHistory entity = base.AddNewEntity() as MedicalRecordFileCompletenessHistory;

			return entity;
		}
		public MedicalRecordFileCompletenessHistory FindByPrimaryKey(Int64 txId)
		{
			return base.FindByPrimaryKey(txId) as MedicalRecordFileCompletenessHistory;
		}

		#region IEnumerable< MedicalRecordFileCompletenessHistory> Members

		IEnumerator<MedicalRecordFileCompletenessHistory> IEnumerable<MedicalRecordFileCompletenessHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MedicalRecordFileCompletenessHistory;
			}
		}

		#endregion

		private MedicalRecordFileCompletenessHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicalRecordFileCompletenessHistory' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MedicalRecordFileCompletenessHistory ({TxId})")]
	[Serializable]
	public partial class MedicalRecordFileCompletenessHistory : esMedicalRecordFileCompletenessHistory
	{
		public MedicalRecordFileCompletenessHistory()
		{
		}

		public MedicalRecordFileCompletenessHistory(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicalRecordFileCompletenessHistoryMetadata.Meta();
			}
		}

		override protected esMedicalRecordFileCompletenessHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalRecordFileCompletenessHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MedicalRecordFileCompletenessHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalRecordFileCompletenessHistoryQuery();
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
		public bool Load(MedicalRecordFileCompletenessHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MedicalRecordFileCompletenessHistoryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MedicalRecordFileCompletenessHistoryQuery : esMedicalRecordFileCompletenessHistoryQuery
	{
		public MedicalRecordFileCompletenessHistoryQuery()
		{

		}

		public MedicalRecordFileCompletenessHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MedicalRecordFileCompletenessHistoryQuery";
		}
	}

	[Serializable]
	public partial class MedicalRecordFileCompletenessHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicalRecordFileCompletenessHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.TxId, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = MedicalRecordFileCompletenessHistoryMetadata.PropertyNames.TxId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileCompletenessHistoryMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.SubmitDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalRecordFileCompletenessHistoryMetadata.PropertyNames.SubmitDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.SubmitNotes, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileCompletenessHistoryMetadata.PropertyNames.SubmitNotes;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalRecordFileCompletenessHistoryMetadata.PropertyNames.ReturnDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnNotes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileCompletenessHistoryMetadata.PropertyNames.ReturnNotes;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.SubmitDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalRecordFileCompletenessHistoryMetadata.PropertyNames.SubmitDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.SubmitByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileCompletenessHistoryMetadata.PropertyNames.SubmitByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalRecordFileCompletenessHistoryMetadata.PropertyNames.ReturnDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.ReturnByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileCompletenessHistoryMetadata.PropertyNames.ReturnByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalRecordFileCompletenessHistoryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileCompletenessHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public MedicalRecordFileCompletenessHistoryMetadata Meta()
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
			public const string TxId = "TxId";
			public const string RegistrationNo = "RegistrationNo";
			public const string SubmitDate = "SubmitDate";
			public const string SubmitNotes = "SubmitNotes";
			public const string ReturnDate = "ReturnDate";
			public const string ReturnNotes = "ReturnNotes";
			public const string SubmitDateTime = "SubmitDateTime";
			public const string SubmitByUserID = "SubmitByUserID";
			public const string ReturnDateTime = "ReturnDateTime";
			public const string ReturnByUserID = "ReturnByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TxId = "TxId";
			public const string RegistrationNo = "RegistrationNo";
			public const string SubmitDate = "SubmitDate";
			public const string SubmitNotes = "SubmitNotes";
			public const string ReturnDate = "ReturnDate";
			public const string ReturnNotes = "ReturnNotes";
			public const string SubmitDateTime = "SubmitDateTime";
			public const string SubmitByUserID = "SubmitByUserID";
			public const string ReturnDateTime = "ReturnDateTime";
			public const string ReturnByUserID = "ReturnByUserID";
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
			lock (typeof(MedicalRecordFileCompletenessHistoryMetadata))
			{
				if (MedicalRecordFileCompletenessHistoryMetadata.mapDelegates == null)
				{
					MedicalRecordFileCompletenessHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MedicalRecordFileCompletenessHistoryMetadata.meta == null)
				{
					MedicalRecordFileCompletenessHistoryMetadata.meta = new MedicalRecordFileCompletenessHistoryMetadata();
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

				meta.AddTypeMap("TxId", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SubmitDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SubmitNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReturnDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ReturnNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SubmitDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SubmitByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReturnDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ReturnByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "MedicalRecordFileCompletenessHistory";
				meta.Destination = "MedicalRecordFileCompletenessHistory";
				meta.spInsert = "proc_MedicalRecordFileCompletenessHistoryInsert";
				meta.spUpdate = "proc_MedicalRecordFileCompletenessHistoryUpdate";
				meta.spDelete = "proc_MedicalRecordFileCompletenessHistoryDelete";
				meta.spLoadAll = "proc_MedicalRecordFileCompletenessHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicalRecordFileCompletenessHistoryLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicalRecordFileCompletenessHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
