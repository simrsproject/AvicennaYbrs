/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/5/2021 4:30:35 PM
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
	abstract public class esCssdSterileItemsRequestItemCollection : esEntityCollectionWAuditLog
	{
		public esCssdSterileItemsRequestItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CssdSterileItemsRequestItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esCssdSterileItemsRequestItemQuery query)
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
			this.InitQuery(query as esCssdSterileItemsRequestItemQuery);
		}
		#endregion

		virtual public CssdSterileItemsRequestItem DetachEntity(CssdSterileItemsRequestItem entity)
		{
			return base.DetachEntity(entity) as CssdSterileItemsRequestItem;
		}

		virtual public CssdSterileItemsRequestItem AttachEntity(CssdSterileItemsRequestItem entity)
		{
			return base.AttachEntity(entity) as CssdSterileItemsRequestItem;
		}

		virtual public void Combine(CssdSterileItemsRequestItemCollection collection)
		{
			base.Combine(collection);
		}

		new public CssdSterileItemsRequestItem this[int index]
		{
			get
			{
				return base[index] as CssdSterileItemsRequestItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CssdSterileItemsRequestItem);
		}
	}

	[Serializable]
	abstract public class esCssdSterileItemsRequestItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCssdSterileItemsRequestItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esCssdSterileItemsRequestItem()
		{
		}

		public esCssdSterileItemsRequestItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String requestNo, String requestSeqNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(requestNo, requestSeqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(requestNo, requestSeqNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String requestNo, String requestSeqNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(requestNo, requestSeqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(requestNo, requestSeqNo);
		}

		private bool LoadByPrimaryKeyDynamic(String requestNo, String requestSeqNo)
		{
			esCssdSterileItemsRequestItemQuery query = this.GetDynamicQuery();
			query.Where(query.RequestNo == requestNo, query.RequestSeqNo == requestSeqNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String requestNo, String requestSeqNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RequestNo", requestNo);
			parms.Add("RequestSeqNo", requestSeqNo);
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
						case "RequestNo": this.str.RequestNo = (string)value; break;
						case "RequestSeqNo": this.str.RequestSeqNo = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "SRCssdItemUnit": this.str.SRCssdItemUnit = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
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
		/// Maps to CssdSterileItemsRequestItem.RequestNo
		/// </summary>
		virtual public System.String RequestNo
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsRequestItemMetadata.ColumnNames.RequestNo);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsRequestItemMetadata.ColumnNames.RequestNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequestItem.RequestSeqNo
		/// </summary>
		virtual public System.String RequestSeqNo
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsRequestItemMetadata.ColumnNames.RequestSeqNo);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsRequestItemMetadata.ColumnNames.RequestSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequestItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsRequestItemMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsRequestItemMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequestItem.SRCssdItemUnit
		/// </summary>
		virtual public System.String SRCssdItemUnit
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsRequestItemMetadata.ColumnNames.SRCssdItemUnit);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsRequestItemMetadata.ColumnNames.SRCssdItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequestItem.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(CssdSterileItemsRequestItemMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(CssdSterileItemsRequestItemMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequestItem.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsRequestItemMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsRequestItemMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequestItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdSterileItemsRequestItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdSterileItemsRequestItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequestItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsRequestItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsRequestItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCssdSterileItemsRequestItem entity)
			{
				this.entity = entity;
			}
			public System.String RequestNo
			{
				get
				{
					System.String data = entity.RequestNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestNo = null;
					else entity.RequestNo = Convert.ToString(value);
				}
			}
			public System.String RequestSeqNo
			{
				get
				{
					System.String data = entity.RequestSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestSeqNo = null;
					else entity.RequestSeqNo = Convert.ToString(value);
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
			public System.String SRCssdItemUnit
			{
				get
				{
					System.String data = entity.SRCssdItemUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCssdItemUnit = null;
					else entity.SRCssdItemUnit = Convert.ToString(value);
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
			private esCssdSterileItemsRequestItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCssdSterileItemsRequestItemQuery query)
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
				throw new Exception("esCssdSterileItemsRequestItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CssdSterileItemsRequestItem : esCssdSterileItemsRequestItem
	{
	}

	[Serializable]
	abstract public class esCssdSterileItemsRequestItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CssdSterileItemsRequestItemMetadata.Meta();
			}
		}

		public esQueryItem RequestNo
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestItemMetadata.ColumnNames.RequestNo, esSystemType.String);
			}
		}

		public esQueryItem RequestSeqNo
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestItemMetadata.ColumnNames.RequestSeqNo, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem SRCssdItemUnit
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestItemMetadata.ColumnNames.SRCssdItemUnit, esSystemType.String);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestItemMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestItemMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CssdSterileItemsRequestItemCollection")]
	public partial class CssdSterileItemsRequestItemCollection : esCssdSterileItemsRequestItemCollection, IEnumerable<CssdSterileItemsRequestItem>
	{
		public CssdSterileItemsRequestItemCollection()
		{

		}

		public static implicit operator List<CssdSterileItemsRequestItem>(CssdSterileItemsRequestItemCollection coll)
		{
			List<CssdSterileItemsRequestItem> list = new List<CssdSterileItemsRequestItem>();

			foreach (CssdSterileItemsRequestItem emp in coll)
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
				return CssdSterileItemsRequestItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdSterileItemsRequestItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CssdSterileItemsRequestItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CssdSterileItemsRequestItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CssdSterileItemsRequestItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdSterileItemsRequestItemQuery();
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
		public bool Load(CssdSterileItemsRequestItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CssdSterileItemsRequestItem AddNew()
		{
			CssdSterileItemsRequestItem entity = base.AddNewEntity() as CssdSterileItemsRequestItem;

			return entity;
		}
		public CssdSterileItemsRequestItem FindByPrimaryKey(String requestNo, String requestSeqNo)
		{
			return base.FindByPrimaryKey(requestNo, requestSeqNo) as CssdSterileItemsRequestItem;
		}

		#region IEnumerable< CssdSterileItemsRequestItem> Members

		IEnumerator<CssdSterileItemsRequestItem> IEnumerable<CssdSterileItemsRequestItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CssdSterileItemsRequestItem;
			}
		}

		#endregion

		private CssdSterileItemsRequestItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CssdSterileItemsRequestItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CssdSterileItemsRequestItem ({RequestNo, RequestSeqNo})")]
	[Serializable]
	public partial class CssdSterileItemsRequestItem : esCssdSterileItemsRequestItem
	{
		public CssdSterileItemsRequestItem()
		{
		}

		public CssdSterileItemsRequestItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CssdSterileItemsRequestItemMetadata.Meta();
			}
		}

		override protected esCssdSterileItemsRequestItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdSterileItemsRequestItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CssdSterileItemsRequestItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdSterileItemsRequestItemQuery();
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
		public bool Load(CssdSterileItemsRequestItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CssdSterileItemsRequestItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CssdSterileItemsRequestItemQuery : esCssdSterileItemsRequestItemQuery
	{
		public CssdSterileItemsRequestItemQuery()
		{

		}

		public CssdSterileItemsRequestItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CssdSterileItemsRequestItemQuery";
		}
	}

	[Serializable]
	public partial class CssdSterileItemsRequestItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CssdSterileItemsRequestItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CssdSterileItemsRequestItemMetadata.ColumnNames.RequestNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsRequestItemMetadata.PropertyNames.RequestNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestItemMetadata.ColumnNames.RequestSeqNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsRequestItemMetadata.PropertyNames.RequestSeqNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestItemMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsRequestItemMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestItemMetadata.ColumnNames.SRCssdItemUnit, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsRequestItemMetadata.PropertyNames.SRCssdItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestItemMetadata.ColumnNames.Qty, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdSterileItemsRequestItemMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestItemMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsRequestItemMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestItemMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterileItemsRequestItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestItemMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsRequestItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CssdSterileItemsRequestItemMetadata Meta()
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
			public const string RequestNo = "RequestNo";
			public const string RequestSeqNo = "RequestSeqNo";
			public const string ItemID = "ItemID";
			public const string SRCssdItemUnit = "SRCssdItemUnit";
			public const string Qty = "Qty";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RequestNo = "RequestNo";
			public const string RequestSeqNo = "RequestSeqNo";
			public const string ItemID = "ItemID";
			public const string SRCssdItemUnit = "SRCssdItemUnit";
			public const string Qty = "Qty";
			public const string Notes = "Notes";
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
			lock (typeof(CssdSterileItemsRequestItemMetadata))
			{
				if (CssdSterileItemsRequestItemMetadata.mapDelegates == null)
				{
					CssdSterileItemsRequestItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CssdSterileItemsRequestItemMetadata.meta == null)
				{
					CssdSterileItemsRequestItemMetadata.meta = new CssdSterileItemsRequestItemMetadata();
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

				meta.AddTypeMap("RequestNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RequestSeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCssdItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CssdSterileItemsRequestItem";
				meta.Destination = "CssdSterileItemsRequestItem";
				meta.spInsert = "proc_CssdSterileItemsRequestItemInsert";
				meta.spUpdate = "proc_CssdSterileItemsRequestItemUpdate";
				meta.spDelete = "proc_CssdSterileItemsRequestItemDelete";
				meta.spLoadAll = "proc_CssdSterileItemsRequestItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_CssdSterileItemsRequestItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CssdSterileItemsRequestItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
