/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/23/2022 11:44:00 AM
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
	abstract public class esTransPaymentPatientCollection : esEntityCollectionWAuditLog
	{
		public esTransPaymentPatientCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "TransPaymentPatientCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransPaymentPatientQuery query)
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
			this.InitQuery(query as esTransPaymentPatientQuery);
		}
		#endregion

		virtual public TransPaymentPatient DetachEntity(TransPaymentPatient entity)
		{
			return base.DetachEntity(entity) as TransPaymentPatient;
		}

		virtual public TransPaymentPatient AttachEntity(TransPaymentPatient entity)
		{
			return base.AttachEntity(entity) as TransPaymentPatient;
		}

		virtual public void Combine(TransPaymentPatientCollection collection)
		{
			base.Combine(collection);
		}

		new public TransPaymentPatient this[int index]
		{
			get
			{
				return base[index] as TransPaymentPatient;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransPaymentPatient);
		}
	}

	[Serializable]
	abstract public class esTransPaymentPatient : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransPaymentPatientQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransPaymentPatient()
		{
		}

		public esTransPaymentPatient(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String paymentNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String paymentNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo);
		}

		private bool LoadByPrimaryKeyDynamic(String paymentNo)
		{
			esTransPaymentPatientQuery query = this.GetDynamicQuery();
			query.Where(query.PaymentNo == paymentNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String paymentNo)
		{
			esParameters parms = new esParameters();
			parms.Add("PaymentNo", paymentNo);
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
						case "PaymentNo": this.str.PaymentNo = (string)value; break;
						case "TransactionCode": this.str.TransactionCode = (string)value; break;
						case "PatientID": this.str.PatientID = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "PaymentDate": this.str.PaymentDate = (string)value; break;
						case "PaymentTime": this.str.PaymentTime = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "CashManagementNo": this.str.CashManagementNo = (string)value; break;
						case "SRPatientDepositType": this.str.SRPatientDepositType = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PaymentDate":

							if (value == null || value is System.DateTime)
								this.PaymentDate = (System.DateTime?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
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
		/// Maps to TransPaymentPatient.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientMetadata.ColumnNames.PaymentNo);
			}

			set
			{
				base.SetSystemString(TransPaymentPatientMetadata.ColumnNames.PaymentNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentPatient.TransactionCode
		/// </summary>
		virtual public System.String TransactionCode
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientMetadata.ColumnNames.TransactionCode);
			}

			set
			{
				base.SetSystemString(TransPaymentPatientMetadata.ColumnNames.TransactionCode, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentPatient.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(TransPaymentPatientMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentPatient.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(TransPaymentPatientMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentPatient.PaymentDate
		/// </summary>
		virtual public System.DateTime? PaymentDate
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentPatientMetadata.ColumnNames.PaymentDate);
			}

			set
			{
				base.SetSystemDateTime(TransPaymentPatientMetadata.ColumnNames.PaymentDate, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentPatient.PaymentTime
		/// </summary>
		virtual public System.String PaymentTime
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientMetadata.ColumnNames.PaymentTime);
			}

			set
			{
				base.SetSystemString(TransPaymentPatientMetadata.ColumnNames.PaymentTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentPatient.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentPatientMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(TransPaymentPatientMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentPatient.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentPatientMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(TransPaymentPatientMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentPatient.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(TransPaymentPatientMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentPatient.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentPatientMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransPaymentPatientMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentPatient.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(TransPaymentPatientMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentPatient.CashManagementNo
		/// </summary>
		virtual public System.String CashManagementNo
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientMetadata.ColumnNames.CashManagementNo);
			}

			set
			{
				base.SetSystemString(TransPaymentPatientMetadata.ColumnNames.CashManagementNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentPatient.SRPatientDepositType
		/// </summary>
		virtual public System.String SRPatientDepositType
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientMetadata.ColumnNames.SRPatientDepositType);
			}

			set
			{
				base.SetSystemString(TransPaymentPatientMetadata.ColumnNames.SRPatientDepositType, value);
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
			public esStrings(esTransPaymentPatient entity)
			{
				this.entity = entity;
			}
			public System.String PaymentNo
			{
				get
				{
					System.String data = entity.PaymentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentNo = null;
					else entity.PaymentNo = Convert.ToString(value);
				}
			}
			public System.String TransactionCode
			{
				get
				{
					System.String data = entity.TransactionCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionCode = null;
					else entity.TransactionCode = Convert.ToString(value);
				}
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
			public System.String ReferenceNo
			{
				get
				{
					System.String data = entity.ReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceNo = null;
					else entity.ReferenceNo = Convert.ToString(value);
				}
			}
			public System.String PaymentDate
			{
				get
				{
					System.DateTime? data = entity.PaymentDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentDate = null;
					else entity.PaymentDate = Convert.ToDateTime(value);
				}
			}
			public System.String PaymentTime
			{
				get
				{
					System.String data = entity.PaymentTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentTime = null;
					else entity.PaymentTime = Convert.ToString(value);
				}
			}
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
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
			public System.String CashManagementNo
			{
				get
				{
					System.String data = entity.CashManagementNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CashManagementNo = null;
					else entity.CashManagementNo = Convert.ToString(value);
				}
			}
			public System.String SRPatientDepositType
			{
				get
				{
					System.String data = entity.SRPatientDepositType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPatientDepositType = null;
					else entity.SRPatientDepositType = Convert.ToString(value);
				}
			}
			private esTransPaymentPatient entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransPaymentPatientQuery query)
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
				throw new Exception("esTransPaymentPatient can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class TransPaymentPatient : esTransPaymentPatient
	{
	}

	[Serializable]
	abstract public class esTransPaymentPatientQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentPatientMetadata.Meta();
			}
		}

		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		}

		public esQueryItem TransactionCode
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientMetadata.ColumnNames.TransactionCode, esSystemType.String);
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem PaymentDate
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientMetadata.ColumnNames.PaymentDate, esSystemType.DateTime);
			}
		}

		public esQueryItem PaymentTime
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientMetadata.ColumnNames.PaymentTime, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem CashManagementNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientMetadata.ColumnNames.CashManagementNo, esSystemType.String);
			}
		}

		public esQueryItem SRPatientDepositType
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientMetadata.ColumnNames.SRPatientDepositType, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransPaymentPatientCollection")]
	public partial class TransPaymentPatientCollection : esTransPaymentPatientCollection, IEnumerable<TransPaymentPatient>
	{
		public TransPaymentPatientCollection()
		{

		}

		public static implicit operator List<TransPaymentPatient>(TransPaymentPatientCollection coll)
		{
			List<TransPaymentPatient> list = new List<TransPaymentPatient>();

			foreach (TransPaymentPatient emp in coll)
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
				return TransPaymentPatientMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentPatientQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransPaymentPatient(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransPaymentPatient();
		}

		#endregion

		[BrowsableAttribute(false)]
		public TransPaymentPatientQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentPatientQuery();
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
		public bool Load(TransPaymentPatientQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public TransPaymentPatient AddNew()
		{
			TransPaymentPatient entity = base.AddNewEntity() as TransPaymentPatient;

			return entity;
		}
		public TransPaymentPatient FindByPrimaryKey(String paymentNo)
		{
			return base.FindByPrimaryKey(paymentNo) as TransPaymentPatient;
		}

		#region IEnumerable< TransPaymentPatient> Members

		IEnumerator<TransPaymentPatient> IEnumerable<TransPaymentPatient>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as TransPaymentPatient;
			}
		}

		#endregion

		private TransPaymentPatientQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransPaymentPatient' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("TransPaymentPatient ({PaymentNo})")]
	[Serializable]
	public partial class TransPaymentPatient : esTransPaymentPatient
	{
		public TransPaymentPatient()
		{
		}

		public TransPaymentPatient(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentPatientMetadata.Meta();
			}
		}

		override protected esTransPaymentPatientQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentPatientQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public TransPaymentPatientQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentPatientQuery();
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
		public bool Load(TransPaymentPatientQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private TransPaymentPatientQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class TransPaymentPatientQuery : esTransPaymentPatientQuery
	{
		public TransPaymentPatientQuery()
		{

		}

		public TransPaymentPatientQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "TransPaymentPatientQuery";
		}
	}

	[Serializable]
	public partial class TransPaymentPatientMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransPaymentPatientMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransPaymentPatientMetadata.ColumnNames.PaymentNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientMetadata.PropertyNames.PaymentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentPatientMetadata.ColumnNames.TransactionCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientMetadata.PropertyNames.TransactionCode;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentPatientMetadata.ColumnNames.PatientID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentPatientMetadata.ColumnNames.ReferenceNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentPatientMetadata.ColumnNames.PaymentDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentPatientMetadata.PropertyNames.PaymentDate;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentPatientMetadata.ColumnNames.PaymentTime, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientMetadata.PropertyNames.PaymentTime;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentPatientMetadata.ColumnNames.IsVoid, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentPatientMetadata.PropertyNames.IsVoid;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentPatientMetadata.ColumnNames.IsApproved, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentPatientMetadata.PropertyNames.IsApproved;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentPatientMetadata.ColumnNames.Notes, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentPatientMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentPatientMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentPatientMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentPatientMetadata.ColumnNames.CashManagementNo, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientMetadata.PropertyNames.CashManagementNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentPatientMetadata.ColumnNames.SRPatientDepositType, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientMetadata.PropertyNames.SRPatientDepositType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public TransPaymentPatientMetadata Meta()
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
			public const string PaymentNo = "PaymentNo";
			public const string TransactionCode = "TransactionCode";
			public const string PatientID = "PatientID";
			public const string ReferenceNo = "ReferenceNo";
			public const string PaymentDate = "PaymentDate";
			public const string PaymentTime = "PaymentTime";
			public const string IsVoid = "IsVoid";
			public const string IsApproved = "IsApproved";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string CashManagementNo = "CashManagementNo";
			public const string SRPatientDepositType = "SRPatientDepositType";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PaymentNo = "PaymentNo";
			public const string TransactionCode = "TransactionCode";
			public const string PatientID = "PatientID";
			public const string ReferenceNo = "ReferenceNo";
			public const string PaymentDate = "PaymentDate";
			public const string PaymentTime = "PaymentTime";
			public const string IsVoid = "IsVoid";
			public const string IsApproved = "IsApproved";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string CashManagementNo = "CashManagementNo";
			public const string SRPatientDepositType = "SRPatientDepositType";
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
			lock (typeof(TransPaymentPatientMetadata))
			{
				if (TransPaymentPatientMetadata.mapDelegates == null)
				{
					TransPaymentPatientMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (TransPaymentPatientMetadata.meta == null)
				{
					TransPaymentPatientMetadata.meta = new TransPaymentPatientMetadata();
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

				meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("PaymentTime", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CashManagementNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPatientDepositType", new esTypeMap("varchar", "System.String"));


				meta.Source = "TransPaymentPatient";
				meta.Destination = "TransPaymentPatient";
				meta.spInsert = "proc_TransPaymentPatientInsert";
				meta.spUpdate = "proc_TransPaymentPatientUpdate";
				meta.spDelete = "proc_TransPaymentPatientDelete";
				meta.spLoadAll = "proc_TransPaymentPatientLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransPaymentPatientLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransPaymentPatientMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
