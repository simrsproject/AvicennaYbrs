/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/25/2022 1:29:04 PM
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
	abstract public class esSanitationWasteTransItemCollection : esEntityCollectionWAuditLog
	{
		public esSanitationWasteTransItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "SanitationWasteTransItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esSanitationWasteTransItemQuery query)
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
			this.InitQuery(query as esSanitationWasteTransItemQuery);
		}
		#endregion

		virtual public SanitationWasteTransItem DetachEntity(SanitationWasteTransItem entity)
		{
			return base.DetachEntity(entity) as SanitationWasteTransItem;
		}

		virtual public SanitationWasteTransItem AttachEntity(SanitationWasteTransItem entity)
		{
			return base.AttachEntity(entity) as SanitationWasteTransItem;
		}

		virtual public void Combine(SanitationWasteTransItemCollection collection)
		{
			base.Combine(collection);
		}

		new public SanitationWasteTransItem this[int index]
		{
			get
			{
				return base[index] as SanitationWasteTransItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SanitationWasteTransItem);
		}
	}

	[Serializable]
	abstract public class esSanitationWasteTransItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSanitationWasteTransItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esSanitationWasteTransItem()
		{
		}

		public esSanitationWasteTransItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sRWasteType, String referenceNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sRWasteType, referenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sRWasteType, referenceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sRWasteType, String referenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sRWasteType, referenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sRWasteType, referenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sRWasteType, String referenceNo)
		{
			esSanitationWasteTransItemQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SRWasteType == sRWasteType, query.ReferenceNo == referenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sRWasteType, String referenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("SRWasteType", sRWasteType);
			parms.Add("ReferenceNo", referenceNo);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "SRWasteType": this.str.SRWasteType = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
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
		/// Maps to SanitationWasteTransItem.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(SanitationWasteTransItemMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(SanitationWasteTransItemMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to SanitationWasteTransItem.SRWasteType
		/// </summary>
		virtual public System.String SRWasteType
		{
			get
			{
				return base.GetSystemString(SanitationWasteTransItemMetadata.ColumnNames.SRWasteType);
			}

			set
			{
				base.SetSystemString(SanitationWasteTransItemMetadata.ColumnNames.SRWasteType, value);
			}
		}
		/// <summary>
		/// Maps to SanitationWasteTransItem.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(SanitationWasteTransItemMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(SanitationWasteTransItemMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to SanitationWasteTransItem.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(SanitationWasteTransItemMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(SanitationWasteTransItemMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to SanitationWasteTransItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SanitationWasteTransItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(SanitationWasteTransItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SanitationWasteTransItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SanitationWasteTransItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(SanitationWasteTransItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSanitationWasteTransItem entity)
			{
				this.entity = entity;
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
			public System.String SRWasteType
			{
				get
				{
					System.String data = entity.SRWasteType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRWasteType = null;
					else entity.SRWasteType = Convert.ToString(value);
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
			private esSanitationWasteTransItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSanitationWasteTransItemQuery query)
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
				throw new Exception("esSanitationWasteTransItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SanitationWasteTransItem : esSanitationWasteTransItem
	{
	}

	[Serializable]
	abstract public class esSanitationWasteTransItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return SanitationWasteTransItemMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, SanitationWasteTransItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SRWasteType
		{
			get
			{
				return new esQueryItem(this, SanitationWasteTransItemMetadata.ColumnNames.SRWasteType, esSystemType.String);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, SanitationWasteTransItemMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, SanitationWasteTransItemMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SanitationWasteTransItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SanitationWasteTransItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SanitationWasteTransItemCollection")]
	public partial class SanitationWasteTransItemCollection : esSanitationWasteTransItemCollection, IEnumerable<SanitationWasteTransItem>
	{
		public SanitationWasteTransItemCollection()
		{

		}

		public static implicit operator List<SanitationWasteTransItem>(SanitationWasteTransItemCollection coll)
		{
			List<SanitationWasteTransItem> list = new List<SanitationWasteTransItem>();

			foreach (SanitationWasteTransItem emp in coll)
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
				return SanitationWasteTransItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationWasteTransItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SanitationWasteTransItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SanitationWasteTransItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public SanitationWasteTransItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationWasteTransItemQuery();
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
		public bool Load(SanitationWasteTransItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SanitationWasteTransItem AddNew()
		{
			SanitationWasteTransItem entity = base.AddNewEntity() as SanitationWasteTransItem;

			return entity;
		}
		public SanitationWasteTransItem FindByPrimaryKey(String transactionNo, String sRWasteType, String referenceNo)
		{
			return base.FindByPrimaryKey(transactionNo, sRWasteType, referenceNo) as SanitationWasteTransItem;
		}

		#region IEnumerable< SanitationWasteTransItem> Members

		IEnumerator<SanitationWasteTransItem> IEnumerable<SanitationWasteTransItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as SanitationWasteTransItem;
			}
		}

		#endregion

		private SanitationWasteTransItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SanitationWasteTransItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SanitationWasteTransItem ({TransactionNo, SRWasteType, ReferenceNo})")]
	[Serializable]
	public partial class SanitationWasteTransItem : esSanitationWasteTransItem
	{
		public SanitationWasteTransItem()
		{
		}

		public SanitationWasteTransItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SanitationWasteTransItemMetadata.Meta();
			}
		}

		override protected esSanitationWasteTransItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationWasteTransItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public SanitationWasteTransItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationWasteTransItemQuery();
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
		public bool Load(SanitationWasteTransItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private SanitationWasteTransItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SanitationWasteTransItemQuery : esSanitationWasteTransItemQuery
	{
		public SanitationWasteTransItemQuery()
		{

		}

		public SanitationWasteTransItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SanitationWasteTransItemQuery";
		}
	}

	[Serializable]
	public partial class SanitationWasteTransItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SanitationWasteTransItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SanitationWasteTransItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationWasteTransItemMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationWasteTransItemMetadata.ColumnNames.SRWasteType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationWasteTransItemMetadata.PropertyNames.SRWasteType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationWasteTransItemMetadata.ColumnNames.ReferenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationWasteTransItemMetadata.PropertyNames.ReferenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationWasteTransItemMetadata.ColumnNames.Qty, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SanitationWasteTransItemMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationWasteTransItemMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationWasteTransItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationWasteTransItemMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationWasteTransItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public SanitationWasteTransItemMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string SRWasteType = "SRWasteType";
			public const string ReferenceNo = "ReferenceNo";
			public const string Qty = "Qty";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string SRWasteType = "SRWasteType";
			public const string ReferenceNo = "ReferenceNo";
			public const string Qty = "Qty";
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
			lock (typeof(SanitationWasteTransItemMetadata))
			{
				if (SanitationWasteTransItemMetadata.mapDelegates == null)
				{
					SanitationWasteTransItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (SanitationWasteTransItemMetadata.meta == null)
				{
					SanitationWasteTransItemMetadata.meta = new SanitationWasteTransItemMetadata();
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

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRWasteType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "SanitationWasteTransItem";
				meta.Destination = "SanitationWasteTransItem";
				meta.spInsert = "proc_SanitationWasteTransItemInsert";
				meta.spUpdate = "proc_SanitationWasteTransItemUpdate";
				meta.spDelete = "proc_SanitationWasteTransItemDelete";
				meta.spLoadAll = "proc_SanitationWasteTransItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_SanitationWasteTransItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SanitationWasteTransItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
