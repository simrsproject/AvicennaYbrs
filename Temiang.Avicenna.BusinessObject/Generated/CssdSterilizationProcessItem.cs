/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/16/2022 8:36:14 PM
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
	abstract public class esCssdSterilizationProcessItemCollection : esEntityCollectionWAuditLog
	{
		public esCssdSterilizationProcessItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CssdSterilizationProcessItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esCssdSterilizationProcessItemQuery query)
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
			this.InitQuery(query as esCssdSterilizationProcessItemQuery);
		}
		#endregion

		virtual public CssdSterilizationProcessItem DetachEntity(CssdSterilizationProcessItem entity)
		{
			return base.DetachEntity(entity) as CssdSterilizationProcessItem;
		}

		virtual public CssdSterilizationProcessItem AttachEntity(CssdSterilizationProcessItem entity)
		{
			return base.AttachEntity(entity) as CssdSterilizationProcessItem;
		}

		virtual public void Combine(CssdSterilizationProcessItemCollection collection)
		{
			base.Combine(collection);
		}

		new public CssdSterilizationProcessItem this[int index]
		{
			get
			{
				return base[index] as CssdSterilizationProcessItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CssdSterilizationProcessItem);
		}
	}

	[Serializable]
	abstract public class esCssdSterilizationProcessItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCssdSterilizationProcessItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esCssdSterilizationProcessItem()
		{
		}

		public esCssdSterilizationProcessItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String processNo, String processSeqNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(processNo, processSeqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(processNo, processSeqNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String processNo, String processSeqNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(processNo, processSeqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(processNo, processSeqNo);
		}

		private bool LoadByPrimaryKeyDynamic(String processNo, String processSeqNo)
		{
			esCssdSterilizationProcessItemQuery query = this.GetDynamicQuery();
			query.Where(query.ProcessNo == processNo, query.ProcessSeqNo == processSeqNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String processNo, String processSeqNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ProcessNo", processNo);
			parms.Add("ProcessSeqNo", processSeqNo);
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
						case "ProcessNo": this.str.ProcessNo = (string)value; break;
						case "ProcessSeqNo": this.str.ProcessSeqNo = (string)value; break;
						case "ReceivedNo": this.str.ReceivedNo = (string)value; break;
						case "ReceivedSeqNo": this.str.ReceivedSeqNo = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "Weight": this.str.Weight = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "CostAmount": this.str.CostAmount = (string)value; break;
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
						case "Weight":

							if (value == null || value is System.Decimal)
								this.Weight = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "CostAmount":

							if (value == null || value is System.Decimal)
								this.CostAmount = (System.Decimal?)value;
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
		/// Maps to CssdSterilizationProcessItem.ProcessNo
		/// </summary>
		virtual public System.String ProcessNo
		{
			get
			{
				return base.GetSystemString(CssdSterilizationProcessItemMetadata.ColumnNames.ProcessNo);
			}

			set
			{
				base.SetSystemString(CssdSterilizationProcessItemMetadata.ColumnNames.ProcessNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcessItem.ProcessSeqNo
		/// </summary>
		virtual public System.String ProcessSeqNo
		{
			get
			{
				return base.GetSystemString(CssdSterilizationProcessItemMetadata.ColumnNames.ProcessSeqNo);
			}

			set
			{
				base.SetSystemString(CssdSterilizationProcessItemMetadata.ColumnNames.ProcessSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcessItem.ReceivedNo
		/// </summary>
		virtual public System.String ReceivedNo
		{
			get
			{
				return base.GetSystemString(CssdSterilizationProcessItemMetadata.ColumnNames.ReceivedNo);
			}

			set
			{
				base.SetSystemString(CssdSterilizationProcessItemMetadata.ColumnNames.ReceivedNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcessItem.ReceivedSeqNo
		/// </summary>
		virtual public System.String ReceivedSeqNo
		{
			get
			{
				return base.GetSystemString(CssdSterilizationProcessItemMetadata.ColumnNames.ReceivedSeqNo);
			}

			set
			{
				base.SetSystemString(CssdSterilizationProcessItemMetadata.ColumnNames.ReceivedSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcessItem.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(CssdSterilizationProcessItemMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(CssdSterilizationProcessItemMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcessItem.Weight
		/// </summary>
		virtual public System.Decimal? Weight
		{
			get
			{
				return base.GetSystemDecimal(CssdSterilizationProcessItemMetadata.ColumnNames.Weight);
			}

			set
			{
				base.SetSystemDecimal(CssdSterilizationProcessItemMetadata.ColumnNames.Weight, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcessItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdSterilizationProcessItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdSterilizationProcessItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcessItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterilizationProcessItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterilizationProcessItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcessItem.CostAmount
		/// </summary>
		virtual public System.Decimal? CostAmount
		{
			get
			{
				return base.GetSystemDecimal(CssdSterilizationProcessItemMetadata.ColumnNames.CostAmount);
			}

			set
			{
				base.SetSystemDecimal(CssdSterilizationProcessItemMetadata.ColumnNames.CostAmount, value);
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
			public esStrings(esCssdSterilizationProcessItem entity)
			{
				this.entity = entity;
			}
			public System.String ProcessNo
			{
				get
				{
					System.String data = entity.ProcessNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcessNo = null;
					else entity.ProcessNo = Convert.ToString(value);
				}
			}
			public System.String ProcessSeqNo
			{
				get
				{
					System.String data = entity.ProcessSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcessSeqNo = null;
					else entity.ProcessSeqNo = Convert.ToString(value);
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
			public System.String Weight
			{
				get
				{
					System.Decimal? data = entity.Weight;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Weight = null;
					else entity.Weight = Convert.ToDecimal(value);
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
			public System.String CostAmount
			{
				get
				{
					System.Decimal? data = entity.CostAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CostAmount = null;
					else entity.CostAmount = Convert.ToDecimal(value);
				}
			}
			private esCssdSterilizationProcessItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCssdSterilizationProcessItemQuery query)
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
				throw new Exception("esCssdSterilizationProcessItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CssdSterilizationProcessItem : esCssdSterilizationProcessItem
	{
	}

	[Serializable]
	abstract public class esCssdSterilizationProcessItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CssdSterilizationProcessItemMetadata.Meta();
			}
		}

		public esQueryItem ProcessNo
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessItemMetadata.ColumnNames.ProcessNo, esSystemType.String);
			}
		}

		public esQueryItem ProcessSeqNo
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessItemMetadata.ColumnNames.ProcessSeqNo, esSystemType.String);
			}
		}

		public esQueryItem ReceivedNo
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessItemMetadata.ColumnNames.ReceivedNo, esSystemType.String);
			}
		}

		public esQueryItem ReceivedSeqNo
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessItemMetadata.ColumnNames.ReceivedSeqNo, esSystemType.String);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessItemMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem Weight
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessItemMetadata.ColumnNames.Weight, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem CostAmount
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessItemMetadata.ColumnNames.CostAmount, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CssdSterilizationProcessItemCollection")]
	public partial class CssdSterilizationProcessItemCollection : esCssdSterilizationProcessItemCollection, IEnumerable<CssdSterilizationProcessItem>
	{
		public CssdSterilizationProcessItemCollection()
		{

		}

		public static implicit operator List<CssdSterilizationProcessItem>(CssdSterilizationProcessItemCollection coll)
		{
			List<CssdSterilizationProcessItem> list = new List<CssdSterilizationProcessItem>();

			foreach (CssdSterilizationProcessItem emp in coll)
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
				return CssdSterilizationProcessItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdSterilizationProcessItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CssdSterilizationProcessItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CssdSterilizationProcessItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CssdSterilizationProcessItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdSterilizationProcessItemQuery();
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
		public bool Load(CssdSterilizationProcessItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CssdSterilizationProcessItem AddNew()
		{
			CssdSterilizationProcessItem entity = base.AddNewEntity() as CssdSterilizationProcessItem;

			return entity;
		}
		public CssdSterilizationProcessItem FindByPrimaryKey(String processNo, String processSeqNo)
		{
			return base.FindByPrimaryKey(processNo, processSeqNo) as CssdSterilizationProcessItem;
		}

		#region IEnumerable< CssdSterilizationProcessItem> Members

		IEnumerator<CssdSterilizationProcessItem> IEnumerable<CssdSterilizationProcessItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CssdSterilizationProcessItem;
			}
		}

		#endregion

		private CssdSterilizationProcessItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CssdSterilizationProcessItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CssdSterilizationProcessItem ({ProcessNo, ProcessSeqNo})")]
	[Serializable]
	public partial class CssdSterilizationProcessItem : esCssdSterilizationProcessItem
	{
		public CssdSterilizationProcessItem()
		{
		}

		public CssdSterilizationProcessItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CssdSterilizationProcessItemMetadata.Meta();
			}
		}

		override protected esCssdSterilizationProcessItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdSterilizationProcessItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CssdSterilizationProcessItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdSterilizationProcessItemQuery();
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
		public bool Load(CssdSterilizationProcessItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CssdSterilizationProcessItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CssdSterilizationProcessItemQuery : esCssdSterilizationProcessItemQuery
	{
		public CssdSterilizationProcessItemQuery()
		{

		}

		public CssdSterilizationProcessItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CssdSterilizationProcessItemQuery";
		}
	}

	[Serializable]
	public partial class CssdSterilizationProcessItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CssdSterilizationProcessItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CssdSterilizationProcessItemMetadata.ColumnNames.ProcessNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterilizationProcessItemMetadata.PropertyNames.ProcessNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessItemMetadata.ColumnNames.ProcessSeqNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterilizationProcessItemMetadata.PropertyNames.ProcessSeqNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessItemMetadata.ColumnNames.ReceivedNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterilizationProcessItemMetadata.PropertyNames.ReceivedNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessItemMetadata.ColumnNames.ReceivedSeqNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterilizationProcessItemMetadata.PropertyNames.ReceivedSeqNo;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessItemMetadata.ColumnNames.Qty, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdSterilizationProcessItemMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessItemMetadata.ColumnNames.Weight, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdSterilizationProcessItemMetadata.PropertyNames.Weight;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessItemMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterilizationProcessItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessItemMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterilizationProcessItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessItemMetadata.ColumnNames.CostAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdSterilizationProcessItemMetadata.PropertyNames.CostAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CssdSterilizationProcessItemMetadata Meta()
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
			public const string ProcessNo = "ProcessNo";
			public const string ProcessSeqNo = "ProcessSeqNo";
			public const string ReceivedNo = "ReceivedNo";
			public const string ReceivedSeqNo = "ReceivedSeqNo";
			public const string Qty = "Qty";
			public const string Weight = "Weight";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string CostAmount = "CostAmount";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ProcessNo = "ProcessNo";
			public const string ProcessSeqNo = "ProcessSeqNo";
			public const string ReceivedNo = "ReceivedNo";
			public const string ReceivedSeqNo = "ReceivedSeqNo";
			public const string Qty = "Qty";
			public const string Weight = "Weight";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string CostAmount = "CostAmount";
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
			lock (typeof(CssdSterilizationProcessItemMetadata))
			{
				if (CssdSterilizationProcessItemMetadata.mapDelegates == null)
				{
					CssdSterilizationProcessItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CssdSterilizationProcessItemMetadata.meta == null)
				{
					CssdSterilizationProcessItemMetadata.meta = new CssdSterilizationProcessItemMetadata();
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

				meta.AddTypeMap("ProcessNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProcessSeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceivedNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceivedSeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Weight", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CostAmount", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "CssdSterilizationProcessItem";
				meta.Destination = "CssdSterilizationProcessItem";
				meta.spInsert = "proc_CssdSterilizationProcessItemInsert";
				meta.spUpdate = "proc_CssdSterilizationProcessItemUpdate";
				meta.spDelete = "proc_CssdSterilizationProcessItemDelete";
				meta.spLoadAll = "proc_CssdSterilizationProcessItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_CssdSterilizationProcessItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CssdSterilizationProcessItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
