/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/6/2020 5:14:06 PM
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
	abstract public class esMembershipItemRedemptionItemCollection : esEntityCollectionWAuditLog
	{
		public esMembershipItemRedemptionItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MembershipItemRedemptionItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esMembershipItemRedemptionItemQuery query)
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
			this.InitQuery(query as esMembershipItemRedemptionItemQuery);
		}
		#endregion

		virtual public MembershipItemRedemptionItem DetachEntity(MembershipItemRedemptionItem entity)
		{
			return base.DetachEntity(entity) as MembershipItemRedemptionItem;
		}

		virtual public MembershipItemRedemptionItem AttachEntity(MembershipItemRedemptionItem entity)
		{
			return base.AttachEntity(entity) as MembershipItemRedemptionItem;
		}

		virtual public void Combine(MembershipItemRedemptionItemCollection collection)
		{
			base.Combine(collection);
		}

		new public MembershipItemRedemptionItem this[int index]
		{
			get
			{
				return base[index] as MembershipItemRedemptionItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MembershipItemRedemptionItem);
		}
	}

	[Serializable]
	abstract public class esMembershipItemRedemptionItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMembershipItemRedemptionItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esMembershipItemRedemptionItem()
		{
		}

		public esMembershipItemRedemptionItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String itemReedemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, itemReedemID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, itemReedemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String itemReedemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, itemReedemID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, itemReedemID);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String itemReedemID)
		{
			esMembershipItemRedemptionItemQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.ItemReedemID == itemReedemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String itemReedemID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("ItemReedemID", itemReedemID);
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
						case "ItemReedemID": this.str.ItemReedemID = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "PointsUsed": this.str.PointsUsed = (string)value; break;
						case "TotalPointsUsed": this.str.TotalPointsUsed = (string)value; break;
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
						case "PointsUsed":

							if (value == null || value is System.Decimal)
								this.PointsUsed = (System.Decimal?)value;
							break;
						case "TotalPointsUsed":

							if (value == null || value is System.Decimal)
								this.TotalPointsUsed = (System.Decimal?)value;
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
		/// Maps to MembershipItemRedemptionItem.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(MembershipItemRedemptionItemMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(MembershipItemRedemptionItemMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to MembershipItemRedemptionItem.ItemReedemID
		/// </summary>
		virtual public System.String ItemReedemID
		{
			get
			{
				return base.GetSystemString(MembershipItemRedemptionItemMetadata.ColumnNames.ItemReedemID);
			}

			set
			{
				base.SetSystemString(MembershipItemRedemptionItemMetadata.ColumnNames.ItemReedemID, value);
			}
		}
		/// <summary>
		/// Maps to MembershipItemRedemptionItem.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(MembershipItemRedemptionItemMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(MembershipItemRedemptionItemMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to MembershipItemRedemptionItem.PointsUsed
		/// </summary>
		virtual public System.Decimal? PointsUsed
		{
			get
			{
				return base.GetSystemDecimal(MembershipItemRedemptionItemMetadata.ColumnNames.PointsUsed);
			}

			set
			{
				base.SetSystemDecimal(MembershipItemRedemptionItemMetadata.ColumnNames.PointsUsed, value);
			}
		}
		/// <summary>
		/// Maps to MembershipItemRedemptionItem.TotalPointsUsed
		/// </summary>
		virtual public System.Decimal? TotalPointsUsed
		{
			get
			{
				return base.GetSystemDecimal(MembershipItemRedemptionItemMetadata.ColumnNames.TotalPointsUsed);
			}

			set
			{
				base.SetSystemDecimal(MembershipItemRedemptionItemMetadata.ColumnNames.TotalPointsUsed, value);
			}
		}
		/// <summary>
		/// Maps to MembershipItemRedemptionItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MembershipItemRedemptionItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MembershipItemRedemptionItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MembershipItemRedemptionItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MembershipItemRedemptionItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MembershipItemRedemptionItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMembershipItemRedemptionItem entity)
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
			public System.String ItemReedemID
			{
				get
				{
					System.String data = entity.ItemReedemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemReedemID = null;
					else entity.ItemReedemID = Convert.ToString(value);
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
			public System.String PointsUsed
			{
				get
				{
					System.Decimal? data = entity.PointsUsed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PointsUsed = null;
					else entity.PointsUsed = Convert.ToDecimal(value);
				}
			}
			public System.String TotalPointsUsed
			{
				get
				{
					System.Decimal? data = entity.TotalPointsUsed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalPointsUsed = null;
					else entity.TotalPointsUsed = Convert.ToDecimal(value);
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
			private esMembershipItemRedemptionItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMembershipItemRedemptionItemQuery query)
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
				throw new Exception("esMembershipItemRedemptionItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MembershipItemRedemptionItem : esMembershipItemRedemptionItem
	{
	}

	[Serializable]
	abstract public class esMembershipItemRedemptionItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MembershipItemRedemptionItemMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, MembershipItemRedemptionItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem ItemReedemID
		{
			get
			{
				return new esQueryItem(this, MembershipItemRedemptionItemMetadata.ColumnNames.ItemReedemID, esSystemType.String);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, MembershipItemRedemptionItemMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem PointsUsed
		{
			get
			{
				return new esQueryItem(this, MembershipItemRedemptionItemMetadata.ColumnNames.PointsUsed, esSystemType.Decimal);
			}
		}

		public esQueryItem TotalPointsUsed
		{
			get
			{
				return new esQueryItem(this, MembershipItemRedemptionItemMetadata.ColumnNames.TotalPointsUsed, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MembershipItemRedemptionItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MembershipItemRedemptionItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MembershipItemRedemptionItemCollection")]
	public partial class MembershipItemRedemptionItemCollection : esMembershipItemRedemptionItemCollection, IEnumerable<MembershipItemRedemptionItem>
	{
		public MembershipItemRedemptionItemCollection()
		{

		}

		public static implicit operator List<MembershipItemRedemptionItem>(MembershipItemRedemptionItemCollection coll)
		{
			List<MembershipItemRedemptionItem> list = new List<MembershipItemRedemptionItem>();

			foreach (MembershipItemRedemptionItem emp in coll)
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
				return MembershipItemRedemptionItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MembershipItemRedemptionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MembershipItemRedemptionItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MembershipItemRedemptionItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MembershipItemRedemptionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MembershipItemRedemptionItemQuery();
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
		public bool Load(MembershipItemRedemptionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MembershipItemRedemptionItem AddNew()
		{
			MembershipItemRedemptionItem entity = base.AddNewEntity() as MembershipItemRedemptionItem;

			return entity;
		}
		public MembershipItemRedemptionItem FindByPrimaryKey(String transactionNo, String itemReedemID)
		{
			return base.FindByPrimaryKey(transactionNo, itemReedemID) as MembershipItemRedemptionItem;
		}

		#region IEnumerable< MembershipItemRedemptionItem> Members

		IEnumerator<MembershipItemRedemptionItem> IEnumerable<MembershipItemRedemptionItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MembershipItemRedemptionItem;
			}
		}

		#endregion

		private MembershipItemRedemptionItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MembershipItemRedemptionItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MembershipItemRedemptionItem ({TransactionNo, ItemReedemID})")]
	[Serializable]
	public partial class MembershipItemRedemptionItem : esMembershipItemRedemptionItem
	{
		public MembershipItemRedemptionItem()
		{
		}

		public MembershipItemRedemptionItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MembershipItemRedemptionItemMetadata.Meta();
			}
		}

		override protected esMembershipItemRedemptionItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MembershipItemRedemptionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MembershipItemRedemptionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MembershipItemRedemptionItemQuery();
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
		public bool Load(MembershipItemRedemptionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MembershipItemRedemptionItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MembershipItemRedemptionItemQuery : esMembershipItemRedemptionItemQuery
	{
		public MembershipItemRedemptionItemQuery()
		{

		}

		public MembershipItemRedemptionItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MembershipItemRedemptionItemQuery";
		}
	}

	[Serializable]
	public partial class MembershipItemRedemptionItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MembershipItemRedemptionItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MembershipItemRedemptionItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipItemRedemptionItemMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipItemRedemptionItemMetadata.ColumnNames.ItemReedemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipItemRedemptionItemMetadata.PropertyNames.ItemReedemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipItemRedemptionItemMetadata.ColumnNames.Qty, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MembershipItemRedemptionItemMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipItemRedemptionItemMetadata.ColumnNames.PointsUsed, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MembershipItemRedemptionItemMetadata.PropertyNames.PointsUsed;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipItemRedemptionItemMetadata.ColumnNames.TotalPointsUsed, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MembershipItemRedemptionItemMetadata.PropertyNames.TotalPointsUsed;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipItemRedemptionItemMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MembershipItemRedemptionItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipItemRedemptionItemMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipItemRedemptionItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public MembershipItemRedemptionItemMetadata Meta()
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
			public const string ItemReedemID = "ItemReedemID";
			public const string Qty = "Qty";
			public const string PointsUsed = "PointsUsed";
			public const string TotalPointsUsed = "TotalPointsUsed";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string ItemReedemID = "ItemReedemID";
			public const string Qty = "Qty";
			public const string PointsUsed = "PointsUsed";
			public const string TotalPointsUsed = "TotalPointsUsed";
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
			lock (typeof(MembershipItemRedemptionItemMetadata))
			{
				if (MembershipItemRedemptionItemMetadata.mapDelegates == null)
				{
					MembershipItemRedemptionItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MembershipItemRedemptionItemMetadata.meta == null)
				{
					MembershipItemRedemptionItemMetadata.meta = new MembershipItemRedemptionItemMetadata();
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
				meta.AddTypeMap("ItemReedemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PointsUsed", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TotalPointsUsed", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "MembershipItemRedemptionItem";
				meta.Destination = "MembershipItemRedemptionItem";
				meta.spInsert = "proc_MembershipItemRedemptionItemInsert";
				meta.spUpdate = "proc_MembershipItemRedemptionItemUpdate";
				meta.spDelete = "proc_MembershipItemRedemptionItemDelete";
				meta.spLoadAll = "proc_MembershipItemRedemptionItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_MembershipItemRedemptionItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MembershipItemRedemptionItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
