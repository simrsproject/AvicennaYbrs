/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/24/2023 3:38:34 PM
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
	abstract public class esCssdPackagingItemCollection : esEntityCollectionWAuditLog
	{
		public esCssdPackagingItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CssdPackagingItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esCssdPackagingItemQuery query)
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
			this.InitQuery(query as esCssdPackagingItemQuery);
		}
		#endregion

		virtual public CssdPackagingItem DetachEntity(CssdPackagingItem entity)
		{
			return base.DetachEntity(entity) as CssdPackagingItem;
		}

		virtual public CssdPackagingItem AttachEntity(CssdPackagingItem entity)
		{
			return base.AttachEntity(entity) as CssdPackagingItem;
		}

		virtual public void Combine(CssdPackagingItemCollection collection)
		{
			base.Combine(collection);
		}

		new public CssdPackagingItem this[int index]
		{
			get
			{
				return base[index] as CssdPackagingItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CssdPackagingItem);
		}
	}

	[Serializable]
	abstract public class esCssdPackagingItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCssdPackagingItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esCssdPackagingItem()
		{
		}

		public esCssdPackagingItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String seqNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, seqNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String seqNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, seqNo);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String seqNo)
		{
			esCssdPackagingItemQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SeqNo == seqNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String seqNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("SeqNo", seqNo);
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
						case "SeqNo": this.str.SeqNo = (string)value; break;
						case "ReceivedNo": this.str.ReceivedNo = (string)value; break;
						case "ReceivedSeqNo": this.str.ReceivedSeqNo = (string)value; break;
						case "CssdItemNo": this.str.CssdItemNo = (string)value; break;
						case "ExpiredDate": this.str.ExpiredDate = (string)value; break;
						case "ReuseTo": this.str.ReuseTo = (string)value; break;
						case "IsNeedUltrasound": this.str.IsNeedUltrasound = (string)value; break;
						case "IsDtt": this.str.IsDtt = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ExpiredDate":

							if (value == null || value is System.DateTime)
								this.ExpiredDate = (System.DateTime?)value;
							break;
						case "ReuseTo":

							if (value == null || value is System.Int16)
								this.ReuseTo = (System.Int16?)value;
							break;
						case "IsNeedUltrasound":

							if (value == null || value is System.Boolean)
								this.IsNeedUltrasound = (System.Boolean?)value;
							break;
						case "IsDtt":

							if (value == null || value is System.Boolean)
								this.IsDtt = (System.Boolean?)value;
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
		/// Maps to CssdPackagingItem.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(CssdPackagingItemMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(CssdPackagingItemMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdPackagingItem.SeqNo
		/// </summary>
		virtual public System.String SeqNo
		{
			get
			{
				return base.GetSystemString(CssdPackagingItemMetadata.ColumnNames.SeqNo);
			}

			set
			{
				base.SetSystemString(CssdPackagingItemMetadata.ColumnNames.SeqNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdPackagingItem.ReceivedNo
		/// </summary>
		virtual public System.String ReceivedNo
		{
			get
			{
				return base.GetSystemString(CssdPackagingItemMetadata.ColumnNames.ReceivedNo);
			}

			set
			{
				base.SetSystemString(CssdPackagingItemMetadata.ColumnNames.ReceivedNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdPackagingItem.ReceivedSeqNo
		/// </summary>
		virtual public System.String ReceivedSeqNo
		{
			get
			{
				return base.GetSystemString(CssdPackagingItemMetadata.ColumnNames.ReceivedSeqNo);
			}

			set
			{
				base.SetSystemString(CssdPackagingItemMetadata.ColumnNames.ReceivedSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdPackagingItem.CssdItemNo
		/// </summary>
		virtual public System.String CssdItemNo
		{
			get
			{
				return base.GetSystemString(CssdPackagingItemMetadata.ColumnNames.CssdItemNo);
			}

			set
			{
				base.SetSystemString(CssdPackagingItemMetadata.ColumnNames.CssdItemNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdPackagingItem.ExpiredDate
		/// </summary>
		virtual public System.DateTime? ExpiredDate
		{
			get
			{
				return base.GetSystemDateTime(CssdPackagingItemMetadata.ColumnNames.ExpiredDate);
			}

			set
			{
				base.SetSystemDateTime(CssdPackagingItemMetadata.ColumnNames.ExpiredDate, value);
			}
		}
		/// <summary>
		/// Maps to CssdPackagingItem.ReuseTo
		/// </summary>
		virtual public System.Int16? ReuseTo
		{
			get
			{
				return base.GetSystemInt16(CssdPackagingItemMetadata.ColumnNames.ReuseTo);
			}

			set
			{
				base.SetSystemInt16(CssdPackagingItemMetadata.ColumnNames.ReuseTo, value);
			}
		}
		/// <summary>
		/// Maps to CssdPackagingItem.IsNeedUltrasound
		/// </summary>
		virtual public System.Boolean? IsNeedUltrasound
		{
			get
			{
				return base.GetSystemBoolean(CssdPackagingItemMetadata.ColumnNames.IsNeedUltrasound);
			}

			set
			{
				base.SetSystemBoolean(CssdPackagingItemMetadata.ColumnNames.IsNeedUltrasound, value);
			}
		}
		/// <summary>
		/// Maps to CssdPackagingItem.IsDtt
		/// </summary>
		virtual public System.Boolean? IsDtt
		{
			get
			{
				return base.GetSystemBoolean(CssdPackagingItemMetadata.ColumnNames.IsDtt);
			}

			set
			{
				base.SetSystemBoolean(CssdPackagingItemMetadata.ColumnNames.IsDtt, value);
			}
		}
		/// <summary>
		/// Maps to CssdPackagingItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdPackagingItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdPackagingItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdPackagingItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CssdPackagingItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CssdPackagingItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCssdPackagingItem entity)
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
			public System.String SeqNo
			{
				get
				{
					System.String data = entity.SeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeqNo = null;
					else entity.SeqNo = Convert.ToString(value);
				}
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
			public System.String CssdItemNo
			{
				get
				{
					System.String data = entity.CssdItemNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CssdItemNo = null;
					else entity.CssdItemNo = Convert.ToString(value);
				}
			}
			public System.String ExpiredDate
			{
				get
				{
					System.DateTime? data = entity.ExpiredDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExpiredDate = null;
					else entity.ExpiredDate = Convert.ToDateTime(value);
				}
			}
			public System.String ReuseTo
			{
				get
				{
					System.Int16? data = entity.ReuseTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReuseTo = null;
					else entity.ReuseTo = Convert.ToInt16(value);
				}
			}
			public System.String IsNeedUltrasound
			{
				get
				{
					System.Boolean? data = entity.IsNeedUltrasound;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNeedUltrasound = null;
					else entity.IsNeedUltrasound = Convert.ToBoolean(value);
				}
			}
			public System.String IsDtt
			{
				get
				{
					System.Boolean? data = entity.IsDtt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDtt = null;
					else entity.IsDtt = Convert.ToBoolean(value);
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
			private esCssdPackagingItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCssdPackagingItemQuery query)
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
				throw new Exception("esCssdPackagingItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CssdPackagingItem : esCssdPackagingItem
	{
	}

	[Serializable]
	abstract public class esCssdPackagingItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CssdPackagingItemMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, CssdPackagingItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SeqNo
		{
			get
			{
				return new esQueryItem(this, CssdPackagingItemMetadata.ColumnNames.SeqNo, esSystemType.String);
			}
		}

		public esQueryItem ReceivedNo
		{
			get
			{
				return new esQueryItem(this, CssdPackagingItemMetadata.ColumnNames.ReceivedNo, esSystemType.String);
			}
		}

		public esQueryItem ReceivedSeqNo
		{
			get
			{
				return new esQueryItem(this, CssdPackagingItemMetadata.ColumnNames.ReceivedSeqNo, esSystemType.String);
			}
		}

		public esQueryItem CssdItemNo
		{
			get
			{
				return new esQueryItem(this, CssdPackagingItemMetadata.ColumnNames.CssdItemNo, esSystemType.String);
			}
		}

		public esQueryItem ExpiredDate
		{
			get
			{
				return new esQueryItem(this, CssdPackagingItemMetadata.ColumnNames.ExpiredDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ReuseTo
		{
			get
			{
				return new esQueryItem(this, CssdPackagingItemMetadata.ColumnNames.ReuseTo, esSystemType.Int16);
			}
		}

		public esQueryItem IsNeedUltrasound
		{
			get
			{
				return new esQueryItem(this, CssdPackagingItemMetadata.ColumnNames.IsNeedUltrasound, esSystemType.Boolean);
			}
		}

		public esQueryItem IsDtt
		{
			get
			{
				return new esQueryItem(this, CssdPackagingItemMetadata.ColumnNames.IsDtt, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CssdPackagingItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CssdPackagingItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CssdPackagingItemCollection")]
	public partial class CssdPackagingItemCollection : esCssdPackagingItemCollection, IEnumerable<CssdPackagingItem>
	{
		public CssdPackagingItemCollection()
		{

		}

		public static implicit operator List<CssdPackagingItem>(CssdPackagingItemCollection coll)
		{
			List<CssdPackagingItem> list = new List<CssdPackagingItem>();

			foreach (CssdPackagingItem emp in coll)
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
				return CssdPackagingItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdPackagingItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CssdPackagingItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CssdPackagingItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CssdPackagingItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdPackagingItemQuery();
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
		public bool Load(CssdPackagingItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CssdPackagingItem AddNew()
		{
			CssdPackagingItem entity = base.AddNewEntity() as CssdPackagingItem;

			return entity;
		}
		public CssdPackagingItem FindByPrimaryKey(String transactionNo, String seqNo)
		{
			return base.FindByPrimaryKey(transactionNo, seqNo) as CssdPackagingItem;
		}

		#region IEnumerable< CssdPackagingItem> Members

		IEnumerator<CssdPackagingItem> IEnumerable<CssdPackagingItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CssdPackagingItem;
			}
		}

		#endregion

		private CssdPackagingItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CssdPackagingItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CssdPackagingItem ({TransactionNo, SeqNo})")]
	[Serializable]
	public partial class CssdPackagingItem : esCssdPackagingItem
	{
		public CssdPackagingItem()
		{
		}

		public CssdPackagingItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CssdPackagingItemMetadata.Meta();
			}
		}

		override protected esCssdPackagingItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdPackagingItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CssdPackagingItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdPackagingItemQuery();
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
		public bool Load(CssdPackagingItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CssdPackagingItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CssdPackagingItemQuery : esCssdPackagingItemQuery
	{
		public CssdPackagingItemQuery()
		{

		}

		public CssdPackagingItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CssdPackagingItemQuery";
		}
	}

	[Serializable]
	public partial class CssdPackagingItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CssdPackagingItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CssdPackagingItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdPackagingItemMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdPackagingItemMetadata.ColumnNames.SeqNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdPackagingItemMetadata.PropertyNames.SeqNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(CssdPackagingItemMetadata.ColumnNames.ReceivedNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdPackagingItemMetadata.PropertyNames.ReceivedNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdPackagingItemMetadata.ColumnNames.ReceivedSeqNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdPackagingItemMetadata.PropertyNames.ReceivedSeqNo;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(CssdPackagingItemMetadata.ColumnNames.CssdItemNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdPackagingItemMetadata.PropertyNames.CssdItemNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdPackagingItemMetadata.ColumnNames.ExpiredDate, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdPackagingItemMetadata.PropertyNames.ExpiredDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdPackagingItemMetadata.ColumnNames.ReuseTo, 6, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = CssdPackagingItemMetadata.PropertyNames.ReuseTo;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdPackagingItemMetadata.ColumnNames.IsNeedUltrasound, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdPackagingItemMetadata.PropertyNames.IsNeedUltrasound;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdPackagingItemMetadata.ColumnNames.IsDtt, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdPackagingItemMetadata.PropertyNames.IsDtt;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdPackagingItemMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdPackagingItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdPackagingItemMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdPackagingItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CssdPackagingItemMetadata Meta()
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
			public const string SeqNo = "SeqNo";
			public const string ReceivedNo = "ReceivedNo";
			public const string ReceivedSeqNo = "ReceivedSeqNo";
			public const string CssdItemNo = "CssdItemNo";
			public const string ExpiredDate = "ExpiredDate";
			public const string ReuseTo = "ReuseTo";
			public const string IsNeedUltrasound = "IsNeedUltrasound";
			public const string IsDtt = "IsDtt";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string SeqNo = "SeqNo";
			public const string ReceivedNo = "ReceivedNo";
			public const string ReceivedSeqNo = "ReceivedSeqNo";
			public const string CssdItemNo = "CssdItemNo";
			public const string ExpiredDate = "ExpiredDate";
			public const string ReuseTo = "ReuseTo";
			public const string IsNeedUltrasound = "IsNeedUltrasound";
			public const string IsDtt = "IsDtt";
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
			lock (typeof(CssdPackagingItemMetadata))
			{
				if (CssdPackagingItemMetadata.mapDelegates == null)
				{
					CssdPackagingItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CssdPackagingItemMetadata.meta == null)
				{
					CssdPackagingItemMetadata.meta = new CssdPackagingItemMetadata();
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
				meta.AddTypeMap("SeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceivedNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceivedSeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CssdItemNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExpiredDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("ReuseTo", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("IsNeedUltrasound", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDtt", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CssdPackagingItem";
				meta.Destination = "CssdPackagingItem";
				meta.spInsert = "proc_CssdPackagingItemInsert";
				meta.spUpdate = "proc_CssdPackagingItemUpdate";
				meta.spDelete = "proc_CssdPackagingItemDelete";
				meta.spLoadAll = "proc_CssdPackagingItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_CssdPackagingItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CssdPackagingItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
