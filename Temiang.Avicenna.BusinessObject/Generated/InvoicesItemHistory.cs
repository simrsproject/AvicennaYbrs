/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/22/2022 1:42:26 PM
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
	abstract public class esInvoicesItemHistoryCollection : esEntityCollectionWAuditLog
	{
		public esInvoicesItemHistoryCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "InvoicesItemHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esInvoicesItemHistoryQuery query)
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
			this.InitQuery(query as esInvoicesItemHistoryQuery);
		}
		#endregion

		virtual public InvoicesItemHistory DetachEntity(InvoicesItemHistory entity)
		{
			return base.DetachEntity(entity) as InvoicesItemHistory;
		}

		virtual public InvoicesItemHistory AttachEntity(InvoicesItemHistory entity)
		{
			return base.AttachEntity(entity) as InvoicesItemHistory;
		}

		virtual public void Combine(InvoicesItemHistoryCollection collection)
		{
			base.Combine(collection);
		}

		new public InvoicesItemHistory this[int index]
		{
			get
			{
				return base[index] as InvoicesItemHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(InvoicesItemHistory);
		}
	}

	[Serializable]
	abstract public class esInvoicesItemHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esInvoicesItemHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esInvoicesItemHistory()
		{
		}

		public esInvoicesItemHistory(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String invoiceNo, String paymentNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(invoiceNo, paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(invoiceNo, paymentNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String invoiceNo, String paymentNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(invoiceNo, paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(invoiceNo, paymentNo);
		}

		private bool LoadByPrimaryKeyDynamic(String invoiceNo, String paymentNo)
		{
			esInvoicesItemHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.InvoiceNo == invoiceNo, query.PaymentNo == paymentNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String invoiceNo, String paymentNo)
		{
			esParameters parms = new esParameters();
			parms.Add("InvoiceNo", invoiceNo);
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
						case "InvoiceNo": this.str.InvoiceNo = (string)value; break;
						case "PaymentNo": this.str.PaymentNo = (string)value; break;
						case "PaymentDate": this.str.PaymentDate = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "PatientID": this.str.PatientID = (string)value; break;
						case "PatientName": this.str.PatientName = (string)value; break;
						case "PaymentAmount": this.str.PaymentAmount = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "Amount": this.str.Amount = (string)value; break;
						case "VerifyAmount": this.str.VerifyAmount = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
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
						case "PaymentAmount":

							if (value == null || value is System.Decimal)
								this.PaymentAmount = (System.Decimal?)value;
							break;
						case "Amount":

							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
							break;
						case "VerifyAmount":

							if (value == null || value is System.Decimal)
								this.VerifyAmount = (System.Decimal?)value;
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
		/// Maps to InvoicesItemHistory.InvoiceNo
		/// </summary>
		virtual public System.String InvoiceNo
		{
			get
			{
				return base.GetSystemString(InvoicesItemHistoryMetadata.ColumnNames.InvoiceNo);
			}

			set
			{
				base.SetSystemString(InvoicesItemHistoryMetadata.ColumnNames.InvoiceNo, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItemHistory.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(InvoicesItemHistoryMetadata.ColumnNames.PaymentNo);
			}

			set
			{
				base.SetSystemString(InvoicesItemHistoryMetadata.ColumnNames.PaymentNo, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItemHistory.PaymentDate
		/// </summary>
		virtual public System.DateTime? PaymentDate
		{
			get
			{
				return base.GetSystemDateTime(InvoicesItemHistoryMetadata.ColumnNames.PaymentDate);
			}

			set
			{
				base.SetSystemDateTime(InvoicesItemHistoryMetadata.ColumnNames.PaymentDate, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItemHistory.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(InvoicesItemHistoryMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(InvoicesItemHistoryMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItemHistory.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(InvoicesItemHistoryMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(InvoicesItemHistoryMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItemHistory.PatientName
		/// </summary>
		virtual public System.String PatientName
		{
			get
			{
				return base.GetSystemString(InvoicesItemHistoryMetadata.ColumnNames.PatientName);
			}

			set
			{
				base.SetSystemString(InvoicesItemHistoryMetadata.ColumnNames.PatientName, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItemHistory.PaymentAmount
		/// </summary>
		virtual public System.Decimal? PaymentAmount
		{
			get
			{
				return base.GetSystemDecimal(InvoicesItemHistoryMetadata.ColumnNames.PaymentAmount);
			}

			set
			{
				base.SetSystemDecimal(InvoicesItemHistoryMetadata.ColumnNames.PaymentAmount, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItemHistory.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(InvoicesItemHistoryMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(InvoicesItemHistoryMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItemHistory.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(InvoicesItemHistoryMetadata.ColumnNames.Amount);
			}

			set
			{
				base.SetSystemDecimal(InvoicesItemHistoryMetadata.ColumnNames.Amount, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItemHistory.VerifyAmount
		/// </summary>
		virtual public System.Decimal? VerifyAmount
		{
			get
			{
				return base.GetSystemDecimal(InvoicesItemHistoryMetadata.ColumnNames.VerifyAmount);
			}

			set
			{
				base.SetSystemDecimal(InvoicesItemHistoryMetadata.ColumnNames.VerifyAmount, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItemHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(InvoicesItemHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(InvoicesItemHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItemHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(InvoicesItemHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(InvoicesItemHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esInvoicesItemHistory entity)
			{
				this.entity = entity;
			}
			public System.String InvoiceNo
			{
				get
				{
					System.String data = entity.InvoiceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoiceNo = null;
					else entity.InvoiceNo = Convert.ToString(value);
				}
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
			public System.String PatientName
			{
				get
				{
					System.String data = entity.PatientName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientName = null;
					else entity.PatientName = Convert.ToString(value);
				}
			}
			public System.String PaymentAmount
			{
				get
				{
					System.Decimal? data = entity.PaymentAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentAmount = null;
					else entity.PaymentAmount = Convert.ToDecimal(value);
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
			public System.String Amount
			{
				get
				{
					System.Decimal? data = entity.Amount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Amount = null;
					else entity.Amount = Convert.ToDecimal(value);
				}
			}
			public System.String VerifyAmount
			{
				get
				{
					System.Decimal? data = entity.VerifyAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifyAmount = null;
					else entity.VerifyAmount = Convert.ToDecimal(value);
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
			private esInvoicesItemHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esInvoicesItemHistoryQuery query)
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
				throw new Exception("esInvoicesItemHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class InvoicesItemHistory : esInvoicesItemHistory
	{
	}

	[Serializable]
	abstract public class esInvoicesItemHistoryQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return InvoicesItemHistoryMetadata.Meta();
			}
		}

		public esQueryItem InvoiceNo
		{
			get
			{
				return new esQueryItem(this, InvoicesItemHistoryMetadata.ColumnNames.InvoiceNo, esSystemType.String);
			}
		}

		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, InvoicesItemHistoryMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		}

		public esQueryItem PaymentDate
		{
			get
			{
				return new esQueryItem(this, InvoicesItemHistoryMetadata.ColumnNames.PaymentDate, esSystemType.DateTime);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, InvoicesItemHistoryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, InvoicesItemHistoryMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem PatientName
		{
			get
			{
				return new esQueryItem(this, InvoicesItemHistoryMetadata.ColumnNames.PatientName, esSystemType.String);
			}
		}

		public esQueryItem PaymentAmount
		{
			get
			{
				return new esQueryItem(this, InvoicesItemHistoryMetadata.ColumnNames.PaymentAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, InvoicesItemHistoryMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, InvoicesItemHistoryMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		}

		public esQueryItem VerifyAmount
		{
			get
			{
				return new esQueryItem(this, InvoicesItemHistoryMetadata.ColumnNames.VerifyAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, InvoicesItemHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, InvoicesItemHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("InvoicesItemHistoryCollection")]
	public partial class InvoicesItemHistoryCollection : esInvoicesItemHistoryCollection, IEnumerable<InvoicesItemHistory>
	{
		public InvoicesItemHistoryCollection()
		{

		}

		public static implicit operator List<InvoicesItemHistory>(InvoicesItemHistoryCollection coll)
		{
			List<InvoicesItemHistory> list = new List<InvoicesItemHistory>();

			foreach (InvoicesItemHistory emp in coll)
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
				return InvoicesItemHistoryMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InvoicesItemHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new InvoicesItemHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new InvoicesItemHistory();
		}

		#endregion

		[BrowsableAttribute(false)]
		public InvoicesItemHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InvoicesItemHistoryQuery();
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
		public bool Load(InvoicesItemHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public InvoicesItemHistory AddNew()
		{
			InvoicesItemHistory entity = base.AddNewEntity() as InvoicesItemHistory;

			return entity;
		}
		public InvoicesItemHistory FindByPrimaryKey(String invoiceNo, String paymentNo)
		{
			return base.FindByPrimaryKey(invoiceNo, paymentNo) as InvoicesItemHistory;
		}

		#region IEnumerable< InvoicesItemHistory> Members

		IEnumerator<InvoicesItemHistory> IEnumerable<InvoicesItemHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as InvoicesItemHistory;
			}
		}

		#endregion

		private InvoicesItemHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'InvoicesItemHistory' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("InvoicesItemHistory ({InvoiceNo, PaymentNo})")]
	[Serializable]
	public partial class InvoicesItemHistory : esInvoicesItemHistory
	{
		public InvoicesItemHistory()
		{
		}

		public InvoicesItemHistory(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return InvoicesItemHistoryMetadata.Meta();
			}
		}

		override protected esInvoicesItemHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InvoicesItemHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public InvoicesItemHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InvoicesItemHistoryQuery();
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
		public bool Load(InvoicesItemHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private InvoicesItemHistoryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class InvoicesItemHistoryQuery : esInvoicesItemHistoryQuery
	{
		public InvoicesItemHistoryQuery()
		{

		}

		public InvoicesItemHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "InvoicesItemHistoryQuery";
		}
	}

	[Serializable]
	public partial class InvoicesItemHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected InvoicesItemHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(InvoicesItemHistoryMetadata.ColumnNames.InvoiceNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesItemHistoryMetadata.PropertyNames.InvoiceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemHistoryMetadata.ColumnNames.PaymentNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesItemHistoryMetadata.PropertyNames.PaymentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemHistoryMetadata.ColumnNames.PaymentDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoicesItemHistoryMetadata.PropertyNames.PaymentDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemHistoryMetadata.ColumnNames.RegistrationNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesItemHistoryMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemHistoryMetadata.ColumnNames.PatientID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesItemHistoryMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemHistoryMetadata.ColumnNames.PatientName, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesItemHistoryMetadata.PropertyNames.PatientName;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemHistoryMetadata.ColumnNames.PaymentAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoicesItemHistoryMetadata.PropertyNames.PaymentAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemHistoryMetadata.ColumnNames.Notes, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesItemHistoryMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemHistoryMetadata.ColumnNames.Amount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoicesItemHistoryMetadata.PropertyNames.Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemHistoryMetadata.ColumnNames.VerifyAmount, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoicesItemHistoryMetadata.PropertyNames.VerifyAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemHistoryMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoicesItemHistoryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemHistoryMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesItemHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public InvoicesItemHistoryMetadata Meta()
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
			public const string InvoiceNo = "InvoiceNo";
			public const string PaymentNo = "PaymentNo";
			public const string PaymentDate = "PaymentDate";
			public const string RegistrationNo = "RegistrationNo";
			public const string PatientID = "PatientID";
			public const string PatientName = "PatientName";
			public const string PaymentAmount = "PaymentAmount";
			public const string Notes = "Notes";
			public const string Amount = "Amount";
			public const string VerifyAmount = "VerifyAmount";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string InvoiceNo = "InvoiceNo";
			public const string PaymentNo = "PaymentNo";
			public const string PaymentDate = "PaymentDate";
			public const string RegistrationNo = "RegistrationNo";
			public const string PatientID = "PatientID";
			public const string PatientName = "PatientName";
			public const string PaymentAmount = "PaymentAmount";
			public const string Notes = "Notes";
			public const string Amount = "Amount";
			public const string VerifyAmount = "VerifyAmount";
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
			lock (typeof(InvoicesItemHistoryMetadata))
			{
				if (InvoicesItemHistoryMetadata.mapDelegates == null)
				{
					InvoicesItemHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (InvoicesItemHistoryMetadata.meta == null)
				{
					InvoicesItemHistoryMetadata.meta = new InvoicesItemHistoryMetadata();
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

				meta.AddTypeMap("InvoiceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Amount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("VerifyAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "InvoicesItemHistory";
				meta.Destination = "InvoicesItemHistory";
				meta.spInsert = "proc_InvoicesItemHistoryInsert";
				meta.spUpdate = "proc_InvoicesItemHistoryUpdate";
				meta.spDelete = "proc_InvoicesItemHistoryDelete";
				meta.spLoadAll = "proc_InvoicesItemHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_InvoicesItemHistoryLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private InvoicesItemHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
