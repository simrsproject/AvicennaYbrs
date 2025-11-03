/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/23/2023 12:28:49 PM
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
	abstract public class esCssdSterileItemsReceivedItemDetailCollection : esEntityCollectionWAuditLog
	{
		public esCssdSterileItemsReceivedItemDetailCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CssdSterileItemsReceivedItemDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esCssdSterileItemsReceivedItemDetailQuery query)
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
			this.InitQuery(query as esCssdSterileItemsReceivedItemDetailQuery);
		}
		#endregion

		virtual public CssdSterileItemsReceivedItemDetail DetachEntity(CssdSterileItemsReceivedItemDetail entity)
		{
			return base.DetachEntity(entity) as CssdSterileItemsReceivedItemDetail;
		}

		virtual public CssdSterileItemsReceivedItemDetail AttachEntity(CssdSterileItemsReceivedItemDetail entity)
		{
			return base.AttachEntity(entity) as CssdSterileItemsReceivedItemDetail;
		}

		virtual public void Combine(CssdSterileItemsReceivedItemDetailCollection collection)
		{
			base.Combine(collection);
		}

		new public CssdSterileItemsReceivedItemDetail this[int index]
		{
			get
			{
				return base[index] as CssdSterileItemsReceivedItemDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CssdSterileItemsReceivedItemDetail);
		}
	}

	[Serializable]
	abstract public class esCssdSterileItemsReceivedItemDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCssdSterileItemsReceivedItemDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esCssdSterileItemsReceivedItemDetail()
		{
		}

		public esCssdSterileItemsReceivedItemDetail(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String receivedNo, String receivedSeqNo, String itemID, String itemDetailID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(receivedNo, receivedSeqNo, itemID, itemDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(receivedNo, receivedSeqNo, itemID, itemDetailID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String receivedNo, String receivedSeqNo, String itemID, String itemDetailID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(receivedNo, receivedSeqNo, itemID, itemDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(receivedNo, receivedSeqNo, itemID, itemDetailID);
		}

		private bool LoadByPrimaryKeyDynamic(String receivedNo, String receivedSeqNo, String itemID, String itemDetailID)
		{
			esCssdSterileItemsReceivedItemDetailQuery query = this.GetDynamicQuery();
			query.Where(query.ReceivedNo == receivedNo, query.ReceivedSeqNo == receivedSeqNo, query.ItemID == itemID, query.ItemDetailID == itemDetailID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String receivedNo, String receivedSeqNo, String itemID, String itemDetailID)
		{
			esParameters parms = new esParameters();
			parms.Add("ReceivedNo", receivedNo);
			parms.Add("ReceivedSeqNo", receivedSeqNo);
			parms.Add("ItemID", itemID);
			parms.Add("ItemDetailID", itemDetailID);
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
						case "ReceivedNo": this.str.ReceivedNo = (string)value; break;
						case "ReceivedSeqNo": this.str.ReceivedSeqNo = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ItemDetailID": this.str.ItemDetailID = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "QtyReceived": this.str.QtyReceived = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsBrokenInstrument": this.str.IsBrokenInstrument = (string)value; break;
						case "QtyReplacements": this.str.QtyReplacements = (string)value; break;
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
						case "QtyReceived":

							if (value == null || value is System.Decimal)
								this.QtyReceived = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsBrokenInstrument":

							if (value == null || value is System.Boolean)
								this.IsBrokenInstrument = (System.Boolean?)value;
							break;
						case "QtyReplacements":

							if (value == null || value is System.Decimal)
								this.QtyReplacements = (System.Decimal?)value;
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
		/// Maps to CssdSterileItemsReceivedItemDetail.ReceivedNo
		/// </summary>
		virtual public System.String ReceivedNo
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.ReceivedNo);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.ReceivedNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItemDetail.ReceivedSeqNo
		/// </summary>
		virtual public System.String ReceivedSeqNo
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.ReceivedSeqNo);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.ReceivedSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItemDetail.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItemDetail.ItemDetailID
		/// </summary>
		virtual public System.String ItemDetailID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.ItemDetailID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.ItemDetailID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItemDetail.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItemDetail.QtyReceived
		/// </summary>
		virtual public System.Decimal? QtyReceived
		{
			get
			{
				return base.GetSystemDecimal(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.QtyReceived);
			}

			set
			{
				base.SetSystemDecimal(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.QtyReceived, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItemDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItemDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItemDetail.IsBrokenInstrument
		/// </summary>
		virtual public System.Boolean? IsBrokenInstrument
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.IsBrokenInstrument);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.IsBrokenInstrument, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItemDetail.QtyReplacements
		/// </summary>
		virtual public System.Decimal? QtyReplacements
		{
			get
			{
				return base.GetSystemDecimal(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.QtyReplacements);
			}

			set
			{
				base.SetSystemDecimal(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.QtyReplacements, value);
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
			public esStrings(esCssdSterileItemsReceivedItemDetail entity)
			{
				this.entity = entity;
			}
			public System.String ReceivedNo
			{
				get
				{
					System.String data = entity.ReceivedNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedNo = null;
					else entity.ReceivedNo = Convert.ToString(value);
				}
			}
			public System.String ReceivedSeqNo
			{
				get
				{
					System.String data = entity.ReceivedSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedSeqNo = null;
					else entity.ReceivedSeqNo = Convert.ToString(value);
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
			public System.String ItemDetailID
			{
				get
				{
					System.String data = entity.ItemDetailID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemDetailID = null;
					else entity.ItemDetailID = Convert.ToString(value);
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
			public System.String QtyReceived
			{
				get
				{
					System.Decimal? data = entity.QtyReceived;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyReceived = null;
					else entity.QtyReceived = Convert.ToDecimal(value);
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
			public System.String IsBrokenInstrument
			{
				get
				{
					System.Boolean? data = entity.IsBrokenInstrument;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBrokenInstrument = null;
					else entity.IsBrokenInstrument = Convert.ToBoolean(value);
				}
			}
			public System.String QtyReplacements
			{
				get
				{
					System.Decimal? data = entity.QtyReplacements;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyReplacements = null;
					else entity.QtyReplacements = Convert.ToDecimal(value);
				}
			}
			private esCssdSterileItemsReceivedItemDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCssdSterileItemsReceivedItemDetailQuery query)
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
				throw new Exception("esCssdSterileItemsReceivedItemDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CssdSterileItemsReceivedItemDetail : esCssdSterileItemsReceivedItemDetail
	{
	}

	[Serializable]
	abstract public class esCssdSterileItemsReceivedItemDetailQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CssdSterileItemsReceivedItemDetailMetadata.Meta();
			}
		}

		public esQueryItem ReceivedNo
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.ReceivedNo, esSystemType.String);
			}
		}

		public esQueryItem ReceivedSeqNo
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.ReceivedSeqNo, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ItemDetailID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.ItemDetailID, esSystemType.String);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyReceived
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.QtyReceived, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsBrokenInstrument
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.IsBrokenInstrument, esSystemType.Boolean);
			}
		}

		public esQueryItem QtyReplacements
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.QtyReplacements, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CssdSterileItemsReceivedItemDetailCollection")]
	public partial class CssdSterileItemsReceivedItemDetailCollection : esCssdSterileItemsReceivedItemDetailCollection, IEnumerable<CssdSterileItemsReceivedItemDetail>
	{
		public CssdSterileItemsReceivedItemDetailCollection()
		{

		}

		public static implicit operator List<CssdSterileItemsReceivedItemDetail>(CssdSterileItemsReceivedItemDetailCollection coll)
		{
			List<CssdSterileItemsReceivedItemDetail> list = new List<CssdSterileItemsReceivedItemDetail>();

			foreach (CssdSterileItemsReceivedItemDetail emp in coll)
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
				return CssdSterileItemsReceivedItemDetailMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdSterileItemsReceivedItemDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CssdSterileItemsReceivedItemDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CssdSterileItemsReceivedItemDetail();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CssdSterileItemsReceivedItemDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdSterileItemsReceivedItemDetailQuery();
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
		public bool Load(CssdSterileItemsReceivedItemDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CssdSterileItemsReceivedItemDetail AddNew()
		{
			CssdSterileItemsReceivedItemDetail entity = base.AddNewEntity() as CssdSterileItemsReceivedItemDetail;

			return entity;
		}
		public CssdSterileItemsReceivedItemDetail FindByPrimaryKey(String receivedNo, String receivedSeqNo, String itemID, String itemDetailID)
		{
			return base.FindByPrimaryKey(receivedNo, receivedSeqNo, itemID, itemDetailID) as CssdSterileItemsReceivedItemDetail;
		}

		#region IEnumerable< CssdSterileItemsReceivedItemDetail> Members

		IEnumerator<CssdSterileItemsReceivedItemDetail> IEnumerable<CssdSterileItemsReceivedItemDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CssdSterileItemsReceivedItemDetail;
			}
		}

		#endregion

		private CssdSterileItemsReceivedItemDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CssdSterileItemsReceivedItemDetail' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CssdSterileItemsReceivedItemDetail ({ReceivedNo, ReceivedSeqNo, ItemID, ItemDetailID})")]
	[Serializable]
	public partial class CssdSterileItemsReceivedItemDetail : esCssdSterileItemsReceivedItemDetail
	{
		public CssdSterileItemsReceivedItemDetail()
		{
		}

		public CssdSterileItemsReceivedItemDetail(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CssdSterileItemsReceivedItemDetailMetadata.Meta();
			}
		}

		override protected esCssdSterileItemsReceivedItemDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdSterileItemsReceivedItemDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CssdSterileItemsReceivedItemDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdSterileItemsReceivedItemDetailQuery();
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
		public bool Load(CssdSterileItemsReceivedItemDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CssdSterileItemsReceivedItemDetailQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CssdSterileItemsReceivedItemDetailQuery : esCssdSterileItemsReceivedItemDetailQuery
	{
		public CssdSterileItemsReceivedItemDetailQuery()
		{

		}

		public CssdSterileItemsReceivedItemDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CssdSterileItemsReceivedItemDetailQuery";
		}
	}

	[Serializable]
	public partial class CssdSterileItemsReceivedItemDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CssdSterileItemsReceivedItemDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.ReceivedNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedItemDetailMetadata.PropertyNames.ReceivedNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.ReceivedSeqNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedItemDetailMetadata.PropertyNames.ReceivedSeqNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedItemDetailMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.ItemDetailID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedItemDetailMetadata.PropertyNames.ItemDetailID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.Qty, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdSterileItemsReceivedItemDetailMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.QtyReceived, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdSterileItemsReceivedItemDetailMetadata.PropertyNames.QtyReceived;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterileItemsReceivedItemDetailMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedItemDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.IsBrokenInstrument, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsReceivedItemDetailMetadata.PropertyNames.IsBrokenInstrument;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemDetailMetadata.ColumnNames.QtyReplacements, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdSterileItemsReceivedItemDetailMetadata.PropertyNames.QtyReplacements;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CssdSterileItemsReceivedItemDetailMetadata Meta()
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
			public const string ReceivedNo = "ReceivedNo";
			public const string ReceivedSeqNo = "ReceivedSeqNo";
			public const string ItemID = "ItemID";
			public const string ItemDetailID = "ItemDetailID";
			public const string Qty = "Qty";
			public const string QtyReceived = "QtyReceived";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsBrokenInstrument = "IsBrokenInstrument";
			public const string QtyReplacements = "QtyReplacements";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ReceivedNo = "ReceivedNo";
			public const string ReceivedSeqNo = "ReceivedSeqNo";
			public const string ItemID = "ItemID";
			public const string ItemDetailID = "ItemDetailID";
			public const string Qty = "Qty";
			public const string QtyReceived = "QtyReceived";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsBrokenInstrument = "IsBrokenInstrument";
			public const string QtyReplacements = "QtyReplacements";
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
			lock (typeof(CssdSterileItemsReceivedItemDetailMetadata))
			{
				if (CssdSterileItemsReceivedItemDetailMetadata.mapDelegates == null)
				{
					CssdSterileItemsReceivedItemDetailMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CssdSterileItemsReceivedItemDetailMetadata.meta == null)
				{
					CssdSterileItemsReceivedItemDetailMetadata.meta = new CssdSterileItemsReceivedItemDetailMetadata();
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

				meta.AddTypeMap("ReceivedNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceivedSeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemDetailID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QtyReceived", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsBrokenInstrument", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("QtyReplacements", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "CssdSterileItemsReceivedItemDetail";
				meta.Destination = "CssdSterileItemsReceivedItemDetail";
				meta.spInsert = "proc_CssdSterileItemsReceivedItemDetailInsert";
				meta.spUpdate = "proc_CssdSterileItemsReceivedItemDetailUpdate";
				meta.spDelete = "proc_CssdSterileItemsReceivedItemDetailDelete";
				meta.spLoadAll = "proc_CssdSterileItemsReceivedItemDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_CssdSterileItemsReceivedItemDetailLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CssdSterileItemsReceivedItemDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
