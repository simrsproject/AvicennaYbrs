/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/23/2023 4:23:45 PM
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
	abstract public class esCssdFeasibilityTestItemCollection : esEntityCollectionWAuditLog
	{
		public esCssdFeasibilityTestItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CssdFeasibilityTestItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esCssdFeasibilityTestItemQuery query)
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
			this.InitQuery(query as esCssdFeasibilityTestItemQuery);
		}
		#endregion

		virtual public CssdFeasibilityTestItem DetachEntity(CssdFeasibilityTestItem entity)
		{
			return base.DetachEntity(entity) as CssdFeasibilityTestItem;
		}

		virtual public CssdFeasibilityTestItem AttachEntity(CssdFeasibilityTestItem entity)
		{
			return base.AttachEntity(entity) as CssdFeasibilityTestItem;
		}

		virtual public void Combine(CssdFeasibilityTestItemCollection collection)
		{
			base.Combine(collection);
		}

		new public CssdFeasibilityTestItem this[int index]
		{
			get
			{
				return base[index] as CssdFeasibilityTestItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CssdFeasibilityTestItem);
		}
	}

	[Serializable]
	abstract public class esCssdFeasibilityTestItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCssdFeasibilityTestItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esCssdFeasibilityTestItem()
		{
		}

		public esCssdFeasibilityTestItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String feasibilityTestNo, String feasibilityTestSeqNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(feasibilityTestNo, feasibilityTestSeqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(feasibilityTestNo, feasibilityTestSeqNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String feasibilityTestNo, String feasibilityTestSeqNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(feasibilityTestNo, feasibilityTestSeqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(feasibilityTestNo, feasibilityTestSeqNo);
		}

		private bool LoadByPrimaryKeyDynamic(String feasibilityTestNo, String feasibilityTestSeqNo)
		{
			esCssdFeasibilityTestItemQuery query = this.GetDynamicQuery();
			query.Where(query.FeasibilityTestNo == feasibilityTestNo, query.FeasibilityTestSeqNo == feasibilityTestSeqNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String feasibilityTestNo, String feasibilityTestSeqNo)
		{
			esParameters parms = new esParameters();
			parms.Add("FeasibilityTestNo", feasibilityTestNo);
			parms.Add("FeasibilityTestSeqNo", feasibilityTestSeqNo);
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
						case "FeasibilityTestNo": this.str.FeasibilityTestNo = (string)value; break;
						case "FeasibilityTestSeqNo": this.str.FeasibilityTestSeqNo = (string)value; break;
						case "ReceivedNo": this.str.ReceivedNo = (string)value; break;
						case "ReceivedSeqNo": this.str.ReceivedSeqNo = (string)value; break;
						case "IsBrokenInstrument": this.str.IsBrokenInstrument = (string)value; break;
						case "QtyReplacements": this.str.QtyReplacements = (string)value; break;
						case "IsFeasibilityTestPassed": this.str.IsFeasibilityTestPassed = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IsBrokenInstrument":

							if (value == null || value is System.Boolean)
								this.IsBrokenInstrument = (System.Boolean?)value;
							break;
						case "QtyReplacements":

							if (value == null || value is System.Decimal)
								this.QtyReplacements = (System.Decimal?)value;
							break;
						case "IsFeasibilityTestPassed":

							if (value == null || value is System.Boolean)
								this.IsFeasibilityTestPassed = (System.Boolean?)value;
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
		/// Maps to CssdFeasibilityTestItem.FeasibilityTestNo
		/// </summary>
		virtual public System.String FeasibilityTestNo
		{
			get
			{
				return base.GetSystemString(CssdFeasibilityTestItemMetadata.ColumnNames.FeasibilityTestNo);
			}

			set
			{
				base.SetSystemString(CssdFeasibilityTestItemMetadata.ColumnNames.FeasibilityTestNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdFeasibilityTestItem.FeasibilityTestSeqNo
		/// </summary>
		virtual public System.String FeasibilityTestSeqNo
		{
			get
			{
				return base.GetSystemString(CssdFeasibilityTestItemMetadata.ColumnNames.FeasibilityTestSeqNo);
			}

			set
			{
				base.SetSystemString(CssdFeasibilityTestItemMetadata.ColumnNames.FeasibilityTestSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdFeasibilityTestItem.ReceivedNo
		/// </summary>
		virtual public System.String ReceivedNo
		{
			get
			{
				return base.GetSystemString(CssdFeasibilityTestItemMetadata.ColumnNames.ReceivedNo);
			}

			set
			{
				base.SetSystemString(CssdFeasibilityTestItemMetadata.ColumnNames.ReceivedNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdFeasibilityTestItem.ReceivedSeqNo
		/// </summary>
		virtual public System.String ReceivedSeqNo
		{
			get
			{
				return base.GetSystemString(CssdFeasibilityTestItemMetadata.ColumnNames.ReceivedSeqNo);
			}

			set
			{
				base.SetSystemString(CssdFeasibilityTestItemMetadata.ColumnNames.ReceivedSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdFeasibilityTestItem.IsBrokenInstrument
		/// </summary>
		virtual public System.Boolean? IsBrokenInstrument
		{
			get
			{
				return base.GetSystemBoolean(CssdFeasibilityTestItemMetadata.ColumnNames.IsBrokenInstrument);
			}

			set
			{
				base.SetSystemBoolean(CssdFeasibilityTestItemMetadata.ColumnNames.IsBrokenInstrument, value);
			}
		}
		/// <summary>
		/// Maps to CssdFeasibilityTestItem.QtyReplacements
		/// </summary>
		virtual public System.Decimal? QtyReplacements
		{
			get
			{
				return base.GetSystemDecimal(CssdFeasibilityTestItemMetadata.ColumnNames.QtyReplacements);
			}

			set
			{
				base.SetSystemDecimal(CssdFeasibilityTestItemMetadata.ColumnNames.QtyReplacements, value);
			}
		}
		/// <summary>
		/// Maps to CssdFeasibilityTestItem.IsFeasibilityTestPassed
		/// </summary>
		virtual public System.Boolean? IsFeasibilityTestPassed
		{
			get
			{
				return base.GetSystemBoolean(CssdFeasibilityTestItemMetadata.ColumnNames.IsFeasibilityTestPassed);
			}

			set
			{
				base.SetSystemBoolean(CssdFeasibilityTestItemMetadata.ColumnNames.IsFeasibilityTestPassed, value);
			}
		}
		/// <summary>
		/// Maps to CssdFeasibilityTestItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdFeasibilityTestItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdFeasibilityTestItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdFeasibilityTestItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CssdFeasibilityTestItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CssdFeasibilityTestItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCssdFeasibilityTestItem entity)
			{
				this.entity = entity;
			}
			public System.String FeasibilityTestNo
			{
				get
				{
					System.String data = entity.FeasibilityTestNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeasibilityTestNo = null;
					else entity.FeasibilityTestNo = Convert.ToString(value);
				}
			}
			public System.String FeasibilityTestSeqNo
			{
				get
				{
					System.String data = entity.FeasibilityTestSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeasibilityTestSeqNo = null;
					else entity.FeasibilityTestSeqNo = Convert.ToString(value);
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
			public System.String IsFeasibilityTestPassed
			{
				get
				{
					System.Boolean? data = entity.IsFeasibilityTestPassed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeasibilityTestPassed = null;
					else entity.IsFeasibilityTestPassed = Convert.ToBoolean(value);
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
			private esCssdFeasibilityTestItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCssdFeasibilityTestItemQuery query)
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
				throw new Exception("esCssdFeasibilityTestItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CssdFeasibilityTestItem : esCssdFeasibilityTestItem
	{
	}

	[Serializable]
	abstract public class esCssdFeasibilityTestItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CssdFeasibilityTestItemMetadata.Meta();
			}
		}

		public esQueryItem FeasibilityTestNo
		{
			get
			{
				return new esQueryItem(this, CssdFeasibilityTestItemMetadata.ColumnNames.FeasibilityTestNo, esSystemType.String);
			}
		}

		public esQueryItem FeasibilityTestSeqNo
		{
			get
			{
				return new esQueryItem(this, CssdFeasibilityTestItemMetadata.ColumnNames.FeasibilityTestSeqNo, esSystemType.String);
			}
		}

		public esQueryItem ReceivedNo
		{
			get
			{
				return new esQueryItem(this, CssdFeasibilityTestItemMetadata.ColumnNames.ReceivedNo, esSystemType.String);
			}
		}

		public esQueryItem ReceivedSeqNo
		{
			get
			{
				return new esQueryItem(this, CssdFeasibilityTestItemMetadata.ColumnNames.ReceivedSeqNo, esSystemType.String);
			}
		}

		public esQueryItem IsBrokenInstrument
		{
			get
			{
				return new esQueryItem(this, CssdFeasibilityTestItemMetadata.ColumnNames.IsBrokenInstrument, esSystemType.Boolean);
			}
		}

		public esQueryItem QtyReplacements
		{
			get
			{
				return new esQueryItem(this, CssdFeasibilityTestItemMetadata.ColumnNames.QtyReplacements, esSystemType.Decimal);
			}
		}

		public esQueryItem IsFeasibilityTestPassed
		{
			get
			{
				return new esQueryItem(this, CssdFeasibilityTestItemMetadata.ColumnNames.IsFeasibilityTestPassed, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CssdFeasibilityTestItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CssdFeasibilityTestItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CssdFeasibilityTestItemCollection")]
	public partial class CssdFeasibilityTestItemCollection : esCssdFeasibilityTestItemCollection, IEnumerable<CssdFeasibilityTestItem>
	{
		public CssdFeasibilityTestItemCollection()
		{

		}

		public static implicit operator List<CssdFeasibilityTestItem>(CssdFeasibilityTestItemCollection coll)
		{
			List<CssdFeasibilityTestItem> list = new List<CssdFeasibilityTestItem>();

			foreach (CssdFeasibilityTestItem emp in coll)
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
				return CssdFeasibilityTestItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdFeasibilityTestItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CssdFeasibilityTestItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CssdFeasibilityTestItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CssdFeasibilityTestItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdFeasibilityTestItemQuery();
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
		public bool Load(CssdFeasibilityTestItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CssdFeasibilityTestItem AddNew()
		{
			CssdFeasibilityTestItem entity = base.AddNewEntity() as CssdFeasibilityTestItem;

			return entity;
		}
		public CssdFeasibilityTestItem FindByPrimaryKey(String feasibilityTestNo, String feasibilityTestSeqNo)
		{
			return base.FindByPrimaryKey(feasibilityTestNo, feasibilityTestSeqNo) as CssdFeasibilityTestItem;
		}

		#region IEnumerable< CssdFeasibilityTestItem> Members

		IEnumerator<CssdFeasibilityTestItem> IEnumerable<CssdFeasibilityTestItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CssdFeasibilityTestItem;
			}
		}

		#endregion

		private CssdFeasibilityTestItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CssdFeasibilityTestItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CssdFeasibilityTestItem ({FeasibilityTestNo, FeasibilityTestSeqNo})")]
	[Serializable]
	public partial class CssdFeasibilityTestItem : esCssdFeasibilityTestItem
	{
		public CssdFeasibilityTestItem()
		{
		}

		public CssdFeasibilityTestItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CssdFeasibilityTestItemMetadata.Meta();
			}
		}

		override protected esCssdFeasibilityTestItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdFeasibilityTestItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CssdFeasibilityTestItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdFeasibilityTestItemQuery();
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
		public bool Load(CssdFeasibilityTestItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CssdFeasibilityTestItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CssdFeasibilityTestItemQuery : esCssdFeasibilityTestItemQuery
	{
		public CssdFeasibilityTestItemQuery()
		{

		}

		public CssdFeasibilityTestItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CssdFeasibilityTestItemQuery";
		}
	}

	[Serializable]
	public partial class CssdFeasibilityTestItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CssdFeasibilityTestItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CssdFeasibilityTestItemMetadata.ColumnNames.FeasibilityTestNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdFeasibilityTestItemMetadata.PropertyNames.FeasibilityTestNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdFeasibilityTestItemMetadata.ColumnNames.FeasibilityTestSeqNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdFeasibilityTestItemMetadata.PropertyNames.FeasibilityTestSeqNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(CssdFeasibilityTestItemMetadata.ColumnNames.ReceivedNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdFeasibilityTestItemMetadata.PropertyNames.ReceivedNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdFeasibilityTestItemMetadata.ColumnNames.ReceivedSeqNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdFeasibilityTestItemMetadata.PropertyNames.ReceivedSeqNo;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(CssdFeasibilityTestItemMetadata.ColumnNames.IsBrokenInstrument, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdFeasibilityTestItemMetadata.PropertyNames.IsBrokenInstrument;
			_columns.Add(c);

			c = new esColumnMetadata(CssdFeasibilityTestItemMetadata.ColumnNames.QtyReplacements, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdFeasibilityTestItemMetadata.PropertyNames.QtyReplacements;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(CssdFeasibilityTestItemMetadata.ColumnNames.IsFeasibilityTestPassed, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdFeasibilityTestItemMetadata.PropertyNames.IsFeasibilityTestPassed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdFeasibilityTestItemMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdFeasibilityTestItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdFeasibilityTestItemMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdFeasibilityTestItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CssdFeasibilityTestItemMetadata Meta()
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
			public const string FeasibilityTestNo = "FeasibilityTestNo";
			public const string FeasibilityTestSeqNo = "FeasibilityTestSeqNo";
			public const string ReceivedNo = "ReceivedNo";
			public const string ReceivedSeqNo = "ReceivedSeqNo";
			public const string IsBrokenInstrument = "IsBrokenInstrument";
			public const string QtyReplacements = "QtyReplacements";
			public const string IsFeasibilityTestPassed = "IsFeasibilityTestPassed";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string FeasibilityTestNo = "FeasibilityTestNo";
			public const string FeasibilityTestSeqNo = "FeasibilityTestSeqNo";
			public const string ReceivedNo = "ReceivedNo";
			public const string ReceivedSeqNo = "ReceivedSeqNo";
			public const string IsBrokenInstrument = "IsBrokenInstrument";
			public const string QtyReplacements = "QtyReplacements";
			public const string IsFeasibilityTestPassed = "IsFeasibilityTestPassed";
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
			lock (typeof(CssdFeasibilityTestItemMetadata))
			{
				if (CssdFeasibilityTestItemMetadata.mapDelegates == null)
				{
					CssdFeasibilityTestItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CssdFeasibilityTestItemMetadata.meta == null)
				{
					CssdFeasibilityTestItemMetadata.meta = new CssdFeasibilityTestItemMetadata();
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

				meta.AddTypeMap("FeasibilityTestNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FeasibilityTestSeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceivedNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceivedSeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsBrokenInstrument", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("QtyReplacements", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsFeasibilityTestPassed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CssdFeasibilityTestItem";
				meta.Destination = "CssdFeasibilityTestItem";
				meta.spInsert = "proc_CssdFeasibilityTestItemInsert";
				meta.spUpdate = "proc_CssdFeasibilityTestItemUpdate";
				meta.spDelete = "proc_CssdFeasibilityTestItemDelete";
				meta.spLoadAll = "proc_CssdFeasibilityTestItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_CssdFeasibilityTestItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CssdFeasibilityTestItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
