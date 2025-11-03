/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/8/2020 10:33:31 AM
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
	abstract public class esTransPaymentItemOrderCollection : esEntityCollectionWAuditLog
	{
		public esTransPaymentItemOrderCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "TransPaymentItemOrderCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransPaymentItemOrderQuery query)
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
			this.InitQuery(query as esTransPaymentItemOrderQuery);
		}
		#endregion

		virtual public TransPaymentItemOrder DetachEntity(TransPaymentItemOrder entity)
		{
			return base.DetachEntity(entity) as TransPaymentItemOrder;
		}

		virtual public TransPaymentItemOrder AttachEntity(TransPaymentItemOrder entity)
		{
			return base.AttachEntity(entity) as TransPaymentItemOrder;
		}

		virtual public void Combine(TransPaymentItemOrderCollection collection)
		{
			base.Combine(collection);
		}

		new public TransPaymentItemOrder this[int index]
		{
			get
			{
				return base[index] as TransPaymentItemOrder;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransPaymentItemOrder);
		}
	}

	[Serializable]
	abstract public class esTransPaymentItemOrder : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransPaymentItemOrderQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransPaymentItemOrder()
		{
		}

		public esTransPaymentItemOrder(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String paymentNo, String transactionNo, String sequenceNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo, transactionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo, transactionNo, sequenceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String paymentNo, String transactionNo, String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo, transactionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo, transactionNo, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(String paymentNo, String transactionNo, String sequenceNo)
		{
			esTransPaymentItemOrderQuery query = this.GetDynamicQuery();
			query.Where(query.PaymentNo == paymentNo, query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String paymentNo, String transactionNo, String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("PaymentNo", paymentNo);
			parms.Add("TransactionNo", transactionNo);
			parms.Add("SequenceNo", sequenceNo);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "Price": this.str.Price = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsPaymentProceed": this.str.IsPaymentProceed = (string)value; break;
						case "IsPaymentReturned": this.str.IsPaymentReturned = (string)value; break;
						case "JournalIncomePaymentNo": this.str.JournalIncomePaymentNo = (string)value; break;
						case "Total": this.str.Total = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "Qty":

							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
							break;
						case "Price":

							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsPaymentProceed":

							if (value == null || value is System.Boolean)
								this.IsPaymentProceed = (System.Boolean?)value;
							break;
						case "IsPaymentReturned":

							if (value == null || value is System.Boolean)
								this.IsPaymentReturned = (System.Boolean?)value;
							break;
						case "Total":

							if (value == null || value is System.Decimal)
								this.Total = (System.Decimal?)value;
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
		/// Maps to TransPaymentItemOrder.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(TransPaymentItemOrderMetadata.ColumnNames.PaymentNo);
			}

			set
			{
				base.SetSystemString(TransPaymentItemOrderMetadata.ColumnNames.PaymentNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemOrder.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(TransPaymentItemOrderMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(TransPaymentItemOrderMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemOrder.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(TransPaymentItemOrderMetadata.ColumnNames.SequenceNo);
			}

			set
			{
				base.SetSystemString(TransPaymentItemOrderMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemOrder.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(TransPaymentItemOrderMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(TransPaymentItemOrderMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemOrder.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(TransPaymentItemOrderMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(TransPaymentItemOrderMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemOrder.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(TransPaymentItemOrderMetadata.ColumnNames.Price);
			}

			set
			{
				base.SetSystemDecimal(TransPaymentItemOrderMetadata.ColumnNames.Price, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemOrder.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentItemOrderMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransPaymentItemOrderMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemOrder.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransPaymentItemOrderMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(TransPaymentItemOrderMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemOrder.IsPaymentProceed
		/// </summary>
		virtual public System.Boolean? IsPaymentProceed
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentItemOrderMetadata.ColumnNames.IsPaymentProceed);
			}

			set
			{
				base.SetSystemBoolean(TransPaymentItemOrderMetadata.ColumnNames.IsPaymentProceed, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemOrder.IsPaymentReturned
		/// </summary>
		virtual public System.Boolean? IsPaymentReturned
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentItemOrderMetadata.ColumnNames.IsPaymentReturned);
			}

			set
			{
				base.SetSystemBoolean(TransPaymentItemOrderMetadata.ColumnNames.IsPaymentReturned, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemOrder.JournalIncomePaymentNo
		/// </summary>
		virtual public System.String JournalIncomePaymentNo
		{
			get
			{
				return base.GetSystemString(TransPaymentItemOrderMetadata.ColumnNames.JournalIncomePaymentNo);
			}

			set
			{
				base.SetSystemString(TransPaymentItemOrderMetadata.ColumnNames.JournalIncomePaymentNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemOrder.Total
		/// </summary>
		virtual public System.Decimal? Total
		{
			get
			{
				return base.GetSystemDecimal(TransPaymentItemOrderMetadata.ColumnNames.Total);
			}

			set
			{
				base.SetSystemDecimal(TransPaymentItemOrderMetadata.ColumnNames.Total, value);
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
			public esStrings(esTransPaymentItemOrder entity)
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
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
				}
			}
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
				}
			}
			public System.String Qty
			{
				get
				{
					System.Decimal? data = entity.Qty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Qty = null;
					else entity.Qty = Convert.ToDecimal(value);
				}
			}
			public System.String Price
			{
				get
				{
					System.Decimal? data = entity.Price;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Price = null;
					else entity.Price = Convert.ToDecimal(value);
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
			public System.String IsPaymentProceed
			{
				get
				{
					System.Boolean? data = entity.IsPaymentProceed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPaymentProceed = null;
					else entity.IsPaymentProceed = Convert.ToBoolean(value);
				}
			}
			public System.String IsPaymentReturned
			{
				get
				{
					System.Boolean? data = entity.IsPaymentReturned;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPaymentReturned = null;
					else entity.IsPaymentReturned = Convert.ToBoolean(value);
				}
			}
			public System.String JournalIncomePaymentNo
			{
				get
				{
					System.String data = entity.JournalIncomePaymentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalIncomePaymentNo = null;
					else entity.JournalIncomePaymentNo = Convert.ToString(value);
				}
			}
			public System.String Total
			{
				get
				{
					System.Decimal? data = entity.Total;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Total = null;
					else entity.Total = Convert.ToDecimal(value);
				}
			}
			private esTransPaymentItemOrder entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransPaymentItemOrderQuery query)
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
				throw new Exception("esTransPaymentItemOrder can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class TransPaymentItemOrder : esTransPaymentItemOrder
	{
	}

	[Serializable]
	abstract public class esTransPaymentItemOrderQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentItemOrderMetadata.Meta();
			}
		}

		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemOrderMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemOrderMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemOrderMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemOrderMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemOrderMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemOrderMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemOrderMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemOrderMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsPaymentProceed
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemOrderMetadata.ColumnNames.IsPaymentProceed, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPaymentReturned
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemOrderMetadata.ColumnNames.IsPaymentReturned, esSystemType.Boolean);
			}
		}

		public esQueryItem JournalIncomePaymentNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemOrderMetadata.ColumnNames.JournalIncomePaymentNo, esSystemType.String);
			}
		}

		public esQueryItem Total
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemOrderMetadata.ColumnNames.Total, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransPaymentItemOrderCollection")]
	public partial class TransPaymentItemOrderCollection : esTransPaymentItemOrderCollection, IEnumerable<TransPaymentItemOrder>
	{
		public TransPaymentItemOrderCollection()
		{

		}

		public static implicit operator List<TransPaymentItemOrder>(TransPaymentItemOrderCollection coll)
		{
			List<TransPaymentItemOrder> list = new List<TransPaymentItemOrder>();

			foreach (TransPaymentItemOrder emp in coll)
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
				return TransPaymentItemOrderMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentItemOrderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransPaymentItemOrder(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransPaymentItemOrder();
		}

		#endregion

		[BrowsableAttribute(false)]
		public TransPaymentItemOrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentItemOrderQuery();
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
		public bool Load(TransPaymentItemOrderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public TransPaymentItemOrder AddNew()
		{
			TransPaymentItemOrder entity = base.AddNewEntity() as TransPaymentItemOrder;

			return entity;
		}
		public TransPaymentItemOrder FindByPrimaryKey(String paymentNo, String transactionNo, String sequenceNo)
		{
			return base.FindByPrimaryKey(paymentNo, transactionNo, sequenceNo) as TransPaymentItemOrder;
		}

		#region IEnumerable< TransPaymentItemOrder> Members

		IEnumerator<TransPaymentItemOrder> IEnumerable<TransPaymentItemOrder>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as TransPaymentItemOrder;
			}
		}

		#endregion

		private TransPaymentItemOrderQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransPaymentItemOrder' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("TransPaymentItemOrder ({PaymentNo, TransactionNo, SequenceNo})")]
	[Serializable]
	public partial class TransPaymentItemOrder : esTransPaymentItemOrder
	{
		public TransPaymentItemOrder()
		{
		}

		public TransPaymentItemOrder(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentItemOrderMetadata.Meta();
			}
		}

		override protected esTransPaymentItemOrderQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentItemOrderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public TransPaymentItemOrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentItemOrderQuery();
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
		public bool Load(TransPaymentItemOrderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private TransPaymentItemOrderQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class TransPaymentItemOrderQuery : esTransPaymentItemOrderQuery
	{
		public TransPaymentItemOrderQuery()
		{

		}

		public TransPaymentItemOrderQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "TransPaymentItemOrderQuery";
		}
	}

	[Serializable]
	public partial class TransPaymentItemOrderMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransPaymentItemOrderMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransPaymentItemOrderMetadata.ColumnNames.PaymentNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemOrderMetadata.PropertyNames.PaymentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemOrderMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemOrderMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemOrderMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemOrderMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 7;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemOrderMetadata.ColumnNames.ItemID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemOrderMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemOrderMetadata.ColumnNames.Qty, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPaymentItemOrderMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemOrderMetadata.ColumnNames.Price, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPaymentItemOrderMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemOrderMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentItemOrderMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemOrderMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemOrderMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemOrderMetadata.ColumnNames.IsPaymentProceed, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentItemOrderMetadata.PropertyNames.IsPaymentProceed;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemOrderMetadata.ColumnNames.IsPaymentReturned, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentItemOrderMetadata.PropertyNames.IsPaymentReturned;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemOrderMetadata.ColumnNames.JournalIncomePaymentNo, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemOrderMetadata.PropertyNames.JournalIncomePaymentNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemOrderMetadata.ColumnNames.Total, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPaymentItemOrderMetadata.PropertyNames.Total;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public TransPaymentItemOrderMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string ItemID = "ItemID";
			public const string Qty = "Qty";
			public const string Price = "Price";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsPaymentProceed = "IsPaymentProceed";
			public const string IsPaymentReturned = "IsPaymentReturned";
			public const string JournalIncomePaymentNo = "JournalIncomePaymentNo";
			public const string Total = "Total";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PaymentNo = "PaymentNo";
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string ItemID = "ItemID";
			public const string Qty = "Qty";
			public const string Price = "Price";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsPaymentProceed = "IsPaymentProceed";
			public const string IsPaymentReturned = "IsPaymentReturned";
			public const string JournalIncomePaymentNo = "JournalIncomePaymentNo";
			public const string Total = "Total";
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
			lock (typeof(TransPaymentItemOrderMetadata))
			{
				if (TransPaymentItemOrderMetadata.mapDelegates == null)
				{
					TransPaymentItemOrderMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (TransPaymentItemOrderMetadata.meta == null)
				{
					TransPaymentItemOrderMetadata.meta = new TransPaymentItemOrderMetadata();
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
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPaymentProceed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPaymentReturned", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("JournalIncomePaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Total", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "TransPaymentItemOrder";
				meta.Destination = "TransPaymentItemOrder";
				meta.spInsert = "proc_TransPaymentItemOrderInsert";
				meta.spUpdate = "proc_TransPaymentItemOrderUpdate";
				meta.spDelete = "proc_TransPaymentItemOrderDelete";
				meta.spLoadAll = "proc_TransPaymentItemOrderLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransPaymentItemOrderLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransPaymentItemOrderMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
