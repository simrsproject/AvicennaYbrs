/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/27/2023 3:42:01 PM
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
	abstract public class esTransPaymentItemVisiteRealizationCollection : esEntityCollectionWAuditLog
	{
		public esTransPaymentItemVisiteRealizationCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "TransPaymentItemVisiteRealizationCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransPaymentItemVisiteRealizationQuery query)
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
			this.InitQuery(query as esTransPaymentItemVisiteRealizationQuery);
		}
		#endregion

		virtual public TransPaymentItemVisiteRealization DetachEntity(TransPaymentItemVisiteRealization entity)
		{
			return base.DetachEntity(entity) as TransPaymentItemVisiteRealization;
		}

		virtual public TransPaymentItemVisiteRealization AttachEntity(TransPaymentItemVisiteRealization entity)
		{
			return base.AttachEntity(entity) as TransPaymentItemVisiteRealization;
		}

		virtual public void Combine(TransPaymentItemVisiteRealizationCollection collection)
		{
			base.Combine(collection);
		}

		new public TransPaymentItemVisiteRealization this[int index]
		{
			get
			{
				return base[index] as TransPaymentItemVisiteRealization;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransPaymentItemVisiteRealization);
		}
	}

	[Serializable]
	abstract public class esTransPaymentItemVisiteRealization : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransPaymentItemVisiteRealizationQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransPaymentItemVisiteRealization()
		{
		}

		public esTransPaymentItemVisiteRealization(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String paymentNo, String patientID, String itemID, String paymentReferenceNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo, patientID, itemID, paymentReferenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo, patientID, itemID, paymentReferenceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String paymentNo, String patientID, String itemID, String paymentReferenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo, patientID, itemID, paymentReferenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo, patientID, itemID, paymentReferenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(String paymentNo, String patientID, String itemID, String paymentReferenceNo)
		{
			esTransPaymentItemVisiteRealizationQuery query = this.GetDynamicQuery();
			query.Where(query.PaymentNo == paymentNo, query.PatientID == patientID, query.ItemID == itemID, query.PaymentReferenceNo == paymentReferenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String paymentNo, String patientID, String itemID, String paymentReferenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("PaymentNo", paymentNo);
			parms.Add("PatientID", patientID);
			parms.Add("ItemID", itemID);
			parms.Add("PaymentReferenceNo", paymentReferenceNo);
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
						case "PatientID": this.str.PatientID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "PaymentReferenceNo": this.str.PaymentReferenceNo = (string)value; break;
						case "RealizationQty": this.str.RealizationQty = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "RealizationQty":

							if (value == null || value is System.Int32)
								this.RealizationQty = (System.Int32?)value;
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
		/// Maps to TransPaymentItemVisiteRealization.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(TransPaymentItemVisiteRealizationMetadata.ColumnNames.PaymentNo);
			}

			set
			{
				base.SetSystemString(TransPaymentItemVisiteRealizationMetadata.ColumnNames.PaymentNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemVisiteRealization.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(TransPaymentItemVisiteRealizationMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(TransPaymentItemVisiteRealizationMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemVisiteRealization.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(TransPaymentItemVisiteRealizationMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(TransPaymentItemVisiteRealizationMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemVisiteRealization.PaymentReferenceNo
		/// </summary>
		virtual public System.String PaymentReferenceNo
		{
			get
			{
				return base.GetSystemString(TransPaymentItemVisiteRealizationMetadata.ColumnNames.PaymentReferenceNo);
			}

			set
			{
				base.SetSystemString(TransPaymentItemVisiteRealizationMetadata.ColumnNames.PaymentReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItemVisiteRealization.RealizationQty
		/// </summary>
		virtual public System.Int32? RealizationQty
		{
			get
			{
				return base.GetSystemInt32(TransPaymentItemVisiteRealizationMetadata.ColumnNames.RealizationQty);
			}

			set
			{
				base.SetSystemInt32(TransPaymentItemVisiteRealizationMetadata.ColumnNames.RealizationQty, value);
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
			public esStrings(esTransPaymentItemVisiteRealization entity)
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
			public System.String PaymentReferenceNo
			{
				get
				{
					System.String data = entity.PaymentReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentReferenceNo = null;
					else entity.PaymentReferenceNo = Convert.ToString(value);
				}
			}
			public System.String RealizationQty
			{
				get
				{
					System.Int32? data = entity.RealizationQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationQty = null;
					else entity.RealizationQty = Convert.ToInt32(value);
				}
			}
			private esTransPaymentItemVisiteRealization entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransPaymentItemVisiteRealizationQuery query)
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
				throw new Exception("esTransPaymentItemVisiteRealization can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class TransPaymentItemVisiteRealization : esTransPaymentItemVisiteRealization
	{
	}

	[Serializable]
	abstract public class esTransPaymentItemVisiteRealizationQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentItemVisiteRealizationMetadata.Meta();
			}
		}

		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemVisiteRealizationMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemVisiteRealizationMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemVisiteRealizationMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem PaymentReferenceNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemVisiteRealizationMetadata.ColumnNames.PaymentReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem RealizationQty
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemVisiteRealizationMetadata.ColumnNames.RealizationQty, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransPaymentItemVisiteRealizationCollection")]
	public partial class TransPaymentItemVisiteRealizationCollection : esTransPaymentItemVisiteRealizationCollection, IEnumerable<TransPaymentItemVisiteRealization>
	{
		public TransPaymentItemVisiteRealizationCollection()
		{

		}

		public static implicit operator List<TransPaymentItemVisiteRealization>(TransPaymentItemVisiteRealizationCollection coll)
		{
			List<TransPaymentItemVisiteRealization> list = new List<TransPaymentItemVisiteRealization>();

			foreach (TransPaymentItemVisiteRealization emp in coll)
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
				return TransPaymentItemVisiteRealizationMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentItemVisiteRealizationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransPaymentItemVisiteRealization(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransPaymentItemVisiteRealization();
		}

		#endregion

		[BrowsableAttribute(false)]
		public TransPaymentItemVisiteRealizationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentItemVisiteRealizationQuery();
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
		public bool Load(TransPaymentItemVisiteRealizationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public TransPaymentItemVisiteRealization AddNew()
		{
			TransPaymentItemVisiteRealization entity = base.AddNewEntity() as TransPaymentItemVisiteRealization;

			return entity;
		}
		public TransPaymentItemVisiteRealization FindByPrimaryKey(String paymentNo, String patientID, String itemID, String paymentReferenceNo)
		{
			return base.FindByPrimaryKey(paymentNo, patientID, itemID, paymentReferenceNo) as TransPaymentItemVisiteRealization;
		}

		#region IEnumerable< TransPaymentItemVisiteRealization> Members

		IEnumerator<TransPaymentItemVisiteRealization> IEnumerable<TransPaymentItemVisiteRealization>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as TransPaymentItemVisiteRealization;
			}
		}

		#endregion

		private TransPaymentItemVisiteRealizationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransPaymentItemVisiteRealization' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("TransPaymentItemVisiteRealization ({PaymentNo, PatientID, ItemID, PaymentReferenceNo})")]
	[Serializable]
	public partial class TransPaymentItemVisiteRealization : esTransPaymentItemVisiteRealization
	{
		public TransPaymentItemVisiteRealization()
		{
		}

		public TransPaymentItemVisiteRealization(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentItemVisiteRealizationMetadata.Meta();
			}
		}

		override protected esTransPaymentItemVisiteRealizationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentItemVisiteRealizationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public TransPaymentItemVisiteRealizationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentItemVisiteRealizationQuery();
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
		public bool Load(TransPaymentItemVisiteRealizationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private TransPaymentItemVisiteRealizationQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class TransPaymentItemVisiteRealizationQuery : esTransPaymentItemVisiteRealizationQuery
	{
		public TransPaymentItemVisiteRealizationQuery()
		{

		}

		public TransPaymentItemVisiteRealizationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "TransPaymentItemVisiteRealizationQuery";
		}
	}

	[Serializable]
	public partial class TransPaymentItemVisiteRealizationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransPaymentItemVisiteRealizationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransPaymentItemVisiteRealizationMetadata.ColumnNames.PaymentNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemVisiteRealizationMetadata.PropertyNames.PaymentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemVisiteRealizationMetadata.ColumnNames.PatientID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemVisiteRealizationMetadata.PropertyNames.PatientID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemVisiteRealizationMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemVisiteRealizationMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemVisiteRealizationMetadata.ColumnNames.PaymentReferenceNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemVisiteRealizationMetadata.PropertyNames.PaymentReferenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemVisiteRealizationMetadata.ColumnNames.RealizationQty, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = TransPaymentItemVisiteRealizationMetadata.PropertyNames.RealizationQty;
			c.NumericPrecision = 10;
			_columns.Add(c);


		}
		#endregion

		static public TransPaymentItemVisiteRealizationMetadata Meta()
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
			public const string PatientID = "PatientID";
			public const string ItemID = "ItemID";
			public const string PaymentReferenceNo = "PaymentReferenceNo";
			public const string RealizationQty = "RealizationQty";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PaymentNo = "PaymentNo";
			public const string PatientID = "PatientID";
			public const string ItemID = "ItemID";
			public const string PaymentReferenceNo = "PaymentReferenceNo";
			public const string RealizationQty = "RealizationQty";
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
			lock (typeof(TransPaymentItemVisiteRealizationMetadata))
			{
				if (TransPaymentItemVisiteRealizationMetadata.mapDelegates == null)
				{
					TransPaymentItemVisiteRealizationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (TransPaymentItemVisiteRealizationMetadata.meta == null)
				{
					TransPaymentItemVisiteRealizationMetadata.meta = new TransPaymentItemVisiteRealizationMetadata();
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
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RealizationQty", new esTypeMap("int", "System.Int32"));


				meta.Source = "TransPaymentItemVisiteRealization";
				meta.Destination = "TransPaymentItemVisiteRealization";
				meta.spInsert = "proc_TransPaymentItemVisiteRealizationInsert";
				meta.spUpdate = "proc_TransPaymentItemVisiteRealizationUpdate";
				meta.spDelete = "proc_TransPaymentItemVisiteRealizationDelete";
				meta.spLoadAll = "proc_TransPaymentItemVisiteRealizationLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransPaymentItemVisiteRealizationLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransPaymentItemVisiteRealizationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
