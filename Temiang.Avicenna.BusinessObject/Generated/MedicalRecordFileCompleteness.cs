/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/20/2023 8:45:05 PM
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
	abstract public class esMedicalRecordFileCompletenessCollection : esEntityCollectionWAuditLog
	{
		public esMedicalRecordFileCompletenessCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MedicalRecordFileCompletenessCollection";
		}

		#region Query Logic
		protected void InitQuery(esMedicalRecordFileCompletenessQuery query)
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
			this.InitQuery(query as esMedicalRecordFileCompletenessQuery);
		}
		#endregion

		virtual public MedicalRecordFileCompleteness DetachEntity(MedicalRecordFileCompleteness entity)
		{
			return base.DetachEntity(entity) as MedicalRecordFileCompleteness;
		}

		virtual public MedicalRecordFileCompleteness AttachEntity(MedicalRecordFileCompleteness entity)
		{
			return base.AttachEntity(entity) as MedicalRecordFileCompleteness;
		}

		virtual public void Combine(MedicalRecordFileCompletenessCollection collection)
		{
			base.Combine(collection);
		}

		new public MedicalRecordFileCompleteness this[int index]
		{
			get
			{
				return base[index] as MedicalRecordFileCompleteness;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicalRecordFileCompleteness);
		}
	}

	[Serializable]
	abstract public class esMedicalRecordFileCompleteness : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicalRecordFileCompletenessQuery GetDynamicQuery()
		{
			return null;
		}

		public esMedicalRecordFileCompleteness()
		{
		}

		public esMedicalRecordFileCompleteness(DataRow row)
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
			esMedicalRecordFileCompletenessQuery query = this.GetDynamicQuery();
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
						case "TransactionDate": this.str.TransactionDate = (string)value; break;
						case "SRFilesAnalysis": this.str.SRFilesAnalysis = (string)value; break;
						case "LastSubmitDate": this.str.LastSubmitDate = (string)value; break;
						case "LastReturnDate": this.str.LastReturnDate = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "TransactionDate":

							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
							break;
						case "LastSubmitDate":

							if (value == null || value is System.DateTime)
								this.LastSubmitDate = (System.DateTime?)value;
							break;
						case "LastReturnDate":

							if (value == null || value is System.DateTime)
								this.LastReturnDate = (System.DateTime?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDateTime":

							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to MedicalRecordFileCompleteness.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileCompletenessMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(MedicalRecordFileCompletenessMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompleteness.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(MedicalRecordFileCompletenessMetadata.ColumnNames.TransactionDate);
			}

			set
			{
				base.SetSystemDateTime(MedicalRecordFileCompletenessMetadata.ColumnNames.TransactionDate, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompleteness.SRFilesAnalysis
		/// </summary>
		virtual public System.String SRFilesAnalysis
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileCompletenessMetadata.ColumnNames.SRFilesAnalysis);
			}

			set
			{
				base.SetSystemString(MedicalRecordFileCompletenessMetadata.ColumnNames.SRFilesAnalysis, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompleteness.LastSubmitDate
		/// </summary>
		virtual public System.DateTime? LastSubmitDate
		{
			get
			{
				return base.GetSystemDateTime(MedicalRecordFileCompletenessMetadata.ColumnNames.LastSubmitDate);
			}

			set
			{
				base.SetSystemDateTime(MedicalRecordFileCompletenessMetadata.ColumnNames.LastSubmitDate, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompleteness.LastReturnDate
		/// </summary>
		virtual public System.DateTime? LastReturnDate
		{
			get
			{
				return base.GetSystemDateTime(MedicalRecordFileCompletenessMetadata.ColumnNames.LastReturnDate);
			}

			set
			{
				base.SetSystemDateTime(MedicalRecordFileCompletenessMetadata.ColumnNames.LastReturnDate, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompleteness.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(MedicalRecordFileCompletenessMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(MedicalRecordFileCompletenessMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompleteness.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalRecordFileCompletenessMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicalRecordFileCompletenessMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompleteness.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileCompletenessMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(MedicalRecordFileCompletenessMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompleteness.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalRecordFileCompletenessMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicalRecordFileCompletenessMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompleteness.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileCompletenessMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(MedicalRecordFileCompletenessMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompleteness.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalRecordFileCompletenessMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicalRecordFileCompletenessMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompleteness.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileCompletenessMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MedicalRecordFileCompletenessMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMedicalRecordFileCompleteness entity)
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
			public System.String TransactionDate
			{
				get
				{
					System.DateTime? data = entity.TransactionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionDate = null;
					else entity.TransactionDate = Convert.ToDateTime(value);
				}
			}
			public System.String SRFilesAnalysis
			{
				get
				{
					System.String data = entity.SRFilesAnalysis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRFilesAnalysis = null;
					else entity.SRFilesAnalysis = Convert.ToString(value);
				}
			}
			public System.String LastSubmitDate
			{
				get
				{
					System.DateTime? data = entity.LastSubmitDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastSubmitDate = null;
					else entity.LastSubmitDate = Convert.ToDateTime(value);
				}
			}
			public System.String LastReturnDate
			{
				get
				{
					System.DateTime? data = entity.LastReturnDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastReturnDate = null;
					else entity.LastReturnDate = Convert.ToDateTime(value);
				}
			}
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
				}
			}
			public System.String ApprovedDateTime
			{
				get
				{
					System.DateTime? data = entity.ApprovedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
					else entity.ApprovedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ApprovedByUserID
			{
				get
				{
					System.String data = entity.ApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
					else entity.ApprovedByUserID = Convert.ToString(value);
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
			private esMedicalRecordFileCompleteness entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicalRecordFileCompletenessQuery query)
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
				throw new Exception("esMedicalRecordFileCompleteness can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MedicalRecordFileCompleteness : esMedicalRecordFileCompleteness
	{
	}

	[Serializable]
	abstract public class esMedicalRecordFileCompletenessQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MedicalRecordFileCompletenessMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SRFilesAnalysis
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessMetadata.ColumnNames.SRFilesAnalysis, esSystemType.String);
			}
		}

		public esQueryItem LastSubmitDate
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessMetadata.ColumnNames.LastSubmitDate, esSystemType.DateTime);
			}
		}

		public esQueryItem LastReturnDate
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessMetadata.ColumnNames.LastReturnDate, esSystemType.DateTime);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicalRecordFileCompletenessCollection")]
	public partial class MedicalRecordFileCompletenessCollection : esMedicalRecordFileCompletenessCollection, IEnumerable<MedicalRecordFileCompleteness>
	{
		public MedicalRecordFileCompletenessCollection()
		{

		}

		public static implicit operator List<MedicalRecordFileCompleteness>(MedicalRecordFileCompletenessCollection coll)
		{
			List<MedicalRecordFileCompleteness> list = new List<MedicalRecordFileCompleteness>();

			foreach (MedicalRecordFileCompleteness emp in coll)
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
				return MedicalRecordFileCompletenessMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalRecordFileCompletenessQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicalRecordFileCompleteness(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicalRecordFileCompleteness();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MedicalRecordFileCompletenessQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalRecordFileCompletenessQuery();
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
		public bool Load(MedicalRecordFileCompletenessQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MedicalRecordFileCompleteness AddNew()
		{
			MedicalRecordFileCompleteness entity = base.AddNewEntity() as MedicalRecordFileCompleteness;

			return entity;
		}
		public MedicalRecordFileCompleteness FindByPrimaryKey(String registrationNo)
		{
			return base.FindByPrimaryKey(registrationNo) as MedicalRecordFileCompleteness;
		}

		#region IEnumerable< MedicalRecordFileCompleteness> Members

		IEnumerator<MedicalRecordFileCompleteness> IEnumerable<MedicalRecordFileCompleteness>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MedicalRecordFileCompleteness;
			}
		}

		#endregion

		private MedicalRecordFileCompletenessQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicalRecordFileCompleteness' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MedicalRecordFileCompleteness ({RegistrationNo})")]
	[Serializable]
	public partial class MedicalRecordFileCompleteness : esMedicalRecordFileCompleteness
	{
		public MedicalRecordFileCompleteness()
		{
		}

		public MedicalRecordFileCompleteness(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicalRecordFileCompletenessMetadata.Meta();
			}
		}

		override protected esMedicalRecordFileCompletenessQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalRecordFileCompletenessQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MedicalRecordFileCompletenessQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalRecordFileCompletenessQuery();
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
		public bool Load(MedicalRecordFileCompletenessQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MedicalRecordFileCompletenessQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MedicalRecordFileCompletenessQuery : esMedicalRecordFileCompletenessQuery
	{
		public MedicalRecordFileCompletenessQuery()
		{

		}

		public MedicalRecordFileCompletenessQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MedicalRecordFileCompletenessQuery";
		}
	}

	[Serializable]
	public partial class MedicalRecordFileCompletenessMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicalRecordFileCompletenessMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MedicalRecordFileCompletenessMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileCompletenessMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalRecordFileCompletenessMetadata.PropertyNames.TransactionDate;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessMetadata.ColumnNames.SRFilesAnalysis, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileCompletenessMetadata.PropertyNames.SRFilesAnalysis;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessMetadata.ColumnNames.LastSubmitDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalRecordFileCompletenessMetadata.PropertyNames.LastSubmitDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessMetadata.ColumnNames.LastReturnDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalRecordFileCompletenessMetadata.PropertyNames.LastReturnDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessMetadata.ColumnNames.IsApproved, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalRecordFileCompletenessMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessMetadata.ColumnNames.ApprovedDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalRecordFileCompletenessMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessMetadata.ColumnNames.ApprovedByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileCompletenessMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessMetadata.ColumnNames.CreatedDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalRecordFileCompletenessMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessMetadata.ColumnNames.CreatedByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileCompletenessMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalRecordFileCompletenessMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileCompletenessMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public MedicalRecordFileCompletenessMetadata Meta()
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
			public const string TransactionDate = "TransactionDate";
			public const string SRFilesAnalysis = "SRFilesAnalysis";
			public const string LastSubmitDate = "LastSubmitDate";
			public const string LastReturnDate = "LastReturnDate";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string TransactionDate = "TransactionDate";
			public const string SRFilesAnalysis = "SRFilesAnalysis";
			public const string LastSubmitDate = "LastSubmitDate";
			public const string LastReturnDate = "LastReturnDate";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
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
			lock (typeof(MedicalRecordFileCompletenessMetadata))
			{
				if (MedicalRecordFileCompletenessMetadata.mapDelegates == null)
				{
					MedicalRecordFileCompletenessMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MedicalRecordFileCompletenessMetadata.meta == null)
				{
					MedicalRecordFileCompletenessMetadata.meta = new MedicalRecordFileCompletenessMetadata();
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
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRFilesAnalysis", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastSubmitDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastReturnDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "MedicalRecordFileCompleteness";
				meta.Destination = "MedicalRecordFileCompleteness";
				meta.spInsert = "proc_MedicalRecordFileCompletenessInsert";
				meta.spUpdate = "proc_MedicalRecordFileCompletenessUpdate";
				meta.spDelete = "proc_MedicalRecordFileCompletenessDelete";
				meta.spLoadAll = "proc_MedicalRecordFileCompletenessLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicalRecordFileCompletenessLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicalRecordFileCompletenessMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
