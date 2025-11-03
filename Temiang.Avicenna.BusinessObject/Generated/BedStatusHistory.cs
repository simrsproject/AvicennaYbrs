/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/26/2022 4:18:45 PM
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
	abstract public class esBedStatusHistoryCollection : esEntityCollectionWAuditLog
	{
		public esBedStatusHistoryCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "BedStatusHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esBedStatusHistoryQuery query)
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
			this.InitQuery(query as esBedStatusHistoryQuery);
		}
		#endregion

		virtual public BedStatusHistory DetachEntity(BedStatusHistory entity)
		{
			return base.DetachEntity(entity) as BedStatusHistory;
		}

		virtual public BedStatusHistory AttachEntity(BedStatusHistory entity)
		{
			return base.AttachEntity(entity) as BedStatusHistory;
		}

		virtual public void Combine(BedStatusHistoryCollection collection)
		{
			base.Combine(collection);
		}

		new public BedStatusHistory this[int index]
		{
			get
			{
				return base[index] as BedStatusHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BedStatusHistory);
		}
	}

	[Serializable]
	abstract public class esBedStatusHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBedStatusHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esBedStatusHistory()
		{
		}

		public esBedStatusHistory(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 transactionID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 transactionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 transactionID)
		{
			esBedStatusHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionID == transactionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 transactionID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionID", transactionID);
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
						case "TransactionID": this.str.TransactionID = (string)value; break;
						case "BedID": this.str.BedID = (string)value; break;
						case "SRBedStatusFrom": this.str.SRBedStatusFrom = (string)value; break;
						case "SRBedStatusTo": this.str.SRBedStatusTo = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "TransferNo": this.str.TransferNo = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "TransactionID":

							if (value == null || value is System.Int64)
								this.TransactionID = (System.Int64?)value;
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
		/// Maps to BedStatusHistory.TransactionID
		/// </summary>
		virtual public System.Int64? TransactionID
		{
			get
			{
				return base.GetSystemInt64(BedStatusHistoryMetadata.ColumnNames.TransactionID);
			}

			set
			{
				base.SetSystemInt64(BedStatusHistoryMetadata.ColumnNames.TransactionID, value);
			}
		}
		/// <summary>
		/// Maps to BedStatusHistory.BedID
		/// </summary>
		virtual public System.String BedID
		{
			get
			{
				return base.GetSystemString(BedStatusHistoryMetadata.ColumnNames.BedID);
			}

			set
			{
				base.SetSystemString(BedStatusHistoryMetadata.ColumnNames.BedID, value);
			}
		}
		/// <summary>
		/// Maps to BedStatusHistory.SRBedStatusFrom
		/// </summary>
		virtual public System.String SRBedStatusFrom
		{
			get
			{
				return base.GetSystemString(BedStatusHistoryMetadata.ColumnNames.SRBedStatusFrom);
			}

			set
			{
				base.SetSystemString(BedStatusHistoryMetadata.ColumnNames.SRBedStatusFrom, value);
			}
		}
		/// <summary>
		/// Maps to BedStatusHistory.SRBedStatusTo
		/// </summary>
		virtual public System.String SRBedStatusTo
		{
			get
			{
				return base.GetSystemString(BedStatusHistoryMetadata.ColumnNames.SRBedStatusTo);
			}

			set
			{
				base.SetSystemString(BedStatusHistoryMetadata.ColumnNames.SRBedStatusTo, value);
			}
		}
		/// <summary>
		/// Maps to BedStatusHistory.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(BedStatusHistoryMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(BedStatusHistoryMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to BedStatusHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BedStatusHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(BedStatusHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BedStatusHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BedStatusHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(BedStatusHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BedStatusHistory.TransferNo
		/// </summary>
		virtual public System.String TransferNo
		{
			get
			{
				return base.GetSystemString(BedStatusHistoryMetadata.ColumnNames.TransferNo);
			}

			set
			{
				base.SetSystemString(BedStatusHistoryMetadata.ColumnNames.TransferNo, value);
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
			public esStrings(esBedStatusHistory entity)
			{
				this.entity = entity;
			}
			public System.String TransactionID
			{
				get
				{
					System.Int64? data = entity.TransactionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionID = null;
					else entity.TransactionID = Convert.ToInt64(value);
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
			public System.String SRBedStatusFrom
			{
				get
				{
					System.String data = entity.SRBedStatusFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBedStatusFrom = null;
					else entity.SRBedStatusFrom = Convert.ToString(value);
				}
			}
			public System.String SRBedStatusTo
			{
				get
				{
					System.String data = entity.SRBedStatusTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBedStatusTo = null;
					else entity.SRBedStatusTo = Convert.ToString(value);
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
			public System.String TransferNo
			{
				get
				{
					System.String data = entity.TransferNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransferNo = null;
					else entity.TransferNo = Convert.ToString(value);
				}
			}
			private esBedStatusHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBedStatusHistoryQuery query)
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
				throw new Exception("esBedStatusHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class BedStatusHistory : esBedStatusHistory
	{
	}

	[Serializable]
	abstract public class esBedStatusHistoryQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return BedStatusHistoryMetadata.Meta();
			}
		}

		public esQueryItem TransactionID
		{
			get
			{
				return new esQueryItem(this, BedStatusHistoryMetadata.ColumnNames.TransactionID, esSystemType.Int64);
			}
		}

		public esQueryItem BedID
		{
			get
			{
				return new esQueryItem(this, BedStatusHistoryMetadata.ColumnNames.BedID, esSystemType.String);
			}
		}

		public esQueryItem SRBedStatusFrom
		{
			get
			{
				return new esQueryItem(this, BedStatusHistoryMetadata.ColumnNames.SRBedStatusFrom, esSystemType.String);
			}
		}

		public esQueryItem SRBedStatusTo
		{
			get
			{
				return new esQueryItem(this, BedStatusHistoryMetadata.ColumnNames.SRBedStatusTo, esSystemType.String);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, BedStatusHistoryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BedStatusHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BedStatusHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem TransferNo
		{
			get
			{
				return new esQueryItem(this, BedStatusHistoryMetadata.ColumnNames.TransferNo, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BedStatusHistoryCollection")]
	public partial class BedStatusHistoryCollection : esBedStatusHistoryCollection, IEnumerable<BedStatusHistory>
	{
		public BedStatusHistoryCollection()
		{

		}

		public static implicit operator List<BedStatusHistory>(BedStatusHistoryCollection coll)
		{
			List<BedStatusHistory> list = new List<BedStatusHistory>();

			foreach (BedStatusHistory emp in coll)
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
				return BedStatusHistoryMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BedStatusHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BedStatusHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BedStatusHistory();
		}

		#endregion

		[BrowsableAttribute(false)]
		public BedStatusHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BedStatusHistoryQuery();
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
		public bool Load(BedStatusHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public BedStatusHistory AddNew()
		{
			BedStatusHistory entity = base.AddNewEntity() as BedStatusHistory;

			return entity;
		}
		public BedStatusHistory FindByPrimaryKey(Int64 transactionID)
		{
			return base.FindByPrimaryKey(transactionID) as BedStatusHistory;
		}

		#region IEnumerable< BedStatusHistory> Members

		IEnumerator<BedStatusHistory> IEnumerable<BedStatusHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as BedStatusHistory;
			}
		}

		#endregion

		private BedStatusHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BedStatusHistory' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("BedStatusHistory ({TransactionID})")]
	[Serializable]
	public partial class BedStatusHistory : esBedStatusHistory
	{
		public BedStatusHistory()
		{
		}

		public BedStatusHistory(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BedStatusHistoryMetadata.Meta();
			}
		}

		override protected esBedStatusHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BedStatusHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public BedStatusHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BedStatusHistoryQuery();
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
		public bool Load(BedStatusHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private BedStatusHistoryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BedStatusHistoryQuery : esBedStatusHistoryQuery
	{
		public BedStatusHistoryQuery()
		{

		}

		public BedStatusHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "BedStatusHistoryQuery";
		}
	}

	[Serializable]
	public partial class BedStatusHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BedStatusHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BedStatusHistoryMetadata.ColumnNames.TransactionID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = BedStatusHistoryMetadata.PropertyNames.TransactionID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(BedStatusHistoryMetadata.ColumnNames.BedID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = BedStatusHistoryMetadata.PropertyNames.BedID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(BedStatusHistoryMetadata.ColumnNames.SRBedStatusFrom, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = BedStatusHistoryMetadata.PropertyNames.SRBedStatusFrom;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(BedStatusHistoryMetadata.ColumnNames.SRBedStatusTo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BedStatusHistoryMetadata.PropertyNames.SRBedStatusTo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(BedStatusHistoryMetadata.ColumnNames.RegistrationNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = BedStatusHistoryMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(BedStatusHistoryMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BedStatusHistoryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BedStatusHistoryMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = BedStatusHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BedStatusHistoryMetadata.ColumnNames.TransferNo, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = BedStatusHistoryMetadata.PropertyNames.TransferNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public BedStatusHistoryMetadata Meta()
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
			public const string TransactionID = "TransactionID";
			public const string BedID = "BedID";
			public const string SRBedStatusFrom = "SRBedStatusFrom";
			public const string SRBedStatusTo = "SRBedStatusTo";
			public const string RegistrationNo = "RegistrationNo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string TransferNo = "TransferNo";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionID = "TransactionID";
			public const string BedID = "BedID";
			public const string SRBedStatusFrom = "SRBedStatusFrom";
			public const string SRBedStatusTo = "SRBedStatusTo";
			public const string RegistrationNo = "RegistrationNo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string TransferNo = "TransferNo";
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
			lock (typeof(BedStatusHistoryMetadata))
			{
				if (BedStatusHistoryMetadata.mapDelegates == null)
				{
					BedStatusHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (BedStatusHistoryMetadata.meta == null)
				{
					BedStatusHistoryMetadata.meta = new BedStatusHistoryMetadata();
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

				meta.AddTypeMap("TransactionID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBedStatusFrom", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBedStatusTo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransferNo", new esTypeMap("varchar", "System.String"));


				meta.Source = "BedStatusHistory";
				meta.Destination = "BedStatusHistory";
				meta.spInsert = "proc_BedStatusHistoryInsert";
				meta.spUpdate = "proc_BedStatusHistoryUpdate";
				meta.spDelete = "proc_BedStatusHistoryDelete";
				meta.spLoadAll = "proc_BedStatusHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_BedStatusHistoryLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BedStatusHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
