/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/10/2021 12:05:03 PM
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
	abstract public class esLaunderedProcessItemInfectiousCollection : esEntityCollectionWAuditLog
	{
		public esLaunderedProcessItemInfectiousCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "LaunderedProcessItemInfectiousCollection";
		}

		#region Query Logic
		protected void InitQuery(esLaunderedProcessItemInfectiousQuery query)
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
			this.InitQuery(query as esLaunderedProcessItemInfectiousQuery);
		}
		#endregion

		virtual public LaunderedProcessItemInfectious DetachEntity(LaunderedProcessItemInfectious entity)
		{
			return base.DetachEntity(entity) as LaunderedProcessItemInfectious;
		}

		virtual public LaunderedProcessItemInfectious AttachEntity(LaunderedProcessItemInfectious entity)
		{
			return base.AttachEntity(entity) as LaunderedProcessItemInfectious;
		}

		virtual public void Combine(LaunderedProcessItemInfectiousCollection collection)
		{
			base.Combine(collection);
		}

		new public LaunderedProcessItemInfectious this[int index]
		{
			get
			{
				return base[index] as LaunderedProcessItemInfectious;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LaunderedProcessItemInfectious);
		}
	}

	[Serializable]
	abstract public class esLaunderedProcessItemInfectious : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLaunderedProcessItemInfectiousQuery GetDynamicQuery()
		{
			return null;
		}

		public esLaunderedProcessItemInfectious()
		{
		}

		public esLaunderedProcessItemInfectious(DataRow row)
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
			esLaunderedProcessItemInfectiousQuery query = this.GetDynamicQuery();
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
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
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
		/// Maps to LaunderedProcessItemInfectious.ProcessNo
		/// </summary>
		virtual public System.String ProcessNo
		{
			get
			{
				return base.GetSystemString(LaunderedProcessItemInfectiousMetadata.ColumnNames.ProcessNo);
			}

			set
			{
				base.SetSystemString(LaunderedProcessItemInfectiousMetadata.ColumnNames.ProcessNo, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcessItemInfectious.ProcessSeqNo
		/// </summary>
		virtual public System.String ProcessSeqNo
		{
			get
			{
				return base.GetSystemString(LaunderedProcessItemInfectiousMetadata.ColumnNames.ProcessSeqNo);
			}

			set
			{
				base.SetSystemString(LaunderedProcessItemInfectiousMetadata.ColumnNames.ProcessSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcessItemInfectious.ReceivedNo
		/// </summary>
		virtual public System.String ReceivedNo
		{
			get
			{
				return base.GetSystemString(LaunderedProcessItemInfectiousMetadata.ColumnNames.ReceivedNo);
			}

			set
			{
				base.SetSystemString(LaunderedProcessItemInfectiousMetadata.ColumnNames.ReceivedNo, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcessItemInfectious.ReceivedSeqNo
		/// </summary>
		virtual public System.String ReceivedSeqNo
		{
			get
			{
				return base.GetSystemString(LaunderedProcessItemInfectiousMetadata.ColumnNames.ReceivedSeqNo);
			}

			set
			{
				base.SetSystemString(LaunderedProcessItemInfectiousMetadata.ColumnNames.ReceivedSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcessItemInfectious.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(LaunderedProcessItemInfectiousMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(LaunderedProcessItemInfectiousMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcessItemInfectious.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(LaunderedProcessItemInfectiousMetadata.ColumnNames.SRItemUnit);
			}

			set
			{
				base.SetSystemString(LaunderedProcessItemInfectiousMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcessItemInfectious.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaunderedProcessItemInfectiousMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaunderedProcessItemInfectiousMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcessItemInfectious.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LaunderedProcessItemInfectiousMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(LaunderedProcessItemInfectiousMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLaunderedProcessItemInfectious entity)
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
			public System.String SRItemUnit
			{
				get
				{
					System.String data = entity.SRItemUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemUnit = null;
					else entity.SRItemUnit = Convert.ToString(value);
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
			private esLaunderedProcessItemInfectious entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLaunderedProcessItemInfectiousQuery query)
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
				throw new Exception("esLaunderedProcessItemInfectious can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class LaunderedProcessItemInfectious : esLaunderedProcessItemInfectious
	{
	}

	[Serializable]
	abstract public class esLaunderedProcessItemInfectiousQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return LaunderedProcessItemInfectiousMetadata.Meta();
			}
		}

		public esQueryItem ProcessNo
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessItemInfectiousMetadata.ColumnNames.ProcessNo, esSystemType.String);
			}
		}

		public esQueryItem ProcessSeqNo
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessItemInfectiousMetadata.ColumnNames.ProcessSeqNo, esSystemType.String);
			}
		}

		public esQueryItem ReceivedNo
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessItemInfectiousMetadata.ColumnNames.ReceivedNo, esSystemType.String);
			}
		}

		public esQueryItem ReceivedSeqNo
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessItemInfectiousMetadata.ColumnNames.ReceivedSeqNo, esSystemType.String);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessItemInfectiousMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessItemInfectiousMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessItemInfectiousMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessItemInfectiousMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LaunderedProcessItemInfectiousCollection")]
	public partial class LaunderedProcessItemInfectiousCollection : esLaunderedProcessItemInfectiousCollection, IEnumerable<LaunderedProcessItemInfectious>
	{
		public LaunderedProcessItemInfectiousCollection()
		{

		}

		public static implicit operator List<LaunderedProcessItemInfectious>(LaunderedProcessItemInfectiousCollection coll)
		{
			List<LaunderedProcessItemInfectious> list = new List<LaunderedProcessItemInfectious>();

			foreach (LaunderedProcessItemInfectious emp in coll)
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
				return LaunderedProcessItemInfectiousMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaunderedProcessItemInfectiousQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LaunderedProcessItemInfectious(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LaunderedProcessItemInfectious();
		}

		#endregion

		[BrowsableAttribute(false)]
		public LaunderedProcessItemInfectiousQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaunderedProcessItemInfectiousQuery();
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
		public bool Load(LaunderedProcessItemInfectiousQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public LaunderedProcessItemInfectious AddNew()
		{
			LaunderedProcessItemInfectious entity = base.AddNewEntity() as LaunderedProcessItemInfectious;

			return entity;
		}
		public LaunderedProcessItemInfectious FindByPrimaryKey(String processNo, String processSeqNo)
		{
			return base.FindByPrimaryKey(processNo, processSeqNo) as LaunderedProcessItemInfectious;
		}

		#region IEnumerable< LaunderedProcessItemInfectious> Members

		IEnumerator<LaunderedProcessItemInfectious> IEnumerable<LaunderedProcessItemInfectious>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as LaunderedProcessItemInfectious;
			}
		}

		#endregion

		private LaunderedProcessItemInfectiousQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LaunderedProcessItemInfectious' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("LaunderedProcessItemInfectious ({ProcessNo, ProcessSeqNo})")]
	[Serializable]
	public partial class LaunderedProcessItemInfectious : esLaunderedProcessItemInfectious
	{
		public LaunderedProcessItemInfectious()
		{
		}

		public LaunderedProcessItemInfectious(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LaunderedProcessItemInfectiousMetadata.Meta();
			}
		}

		override protected esLaunderedProcessItemInfectiousQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaunderedProcessItemInfectiousQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public LaunderedProcessItemInfectiousQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaunderedProcessItemInfectiousQuery();
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
		public bool Load(LaunderedProcessItemInfectiousQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private LaunderedProcessItemInfectiousQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class LaunderedProcessItemInfectiousQuery : esLaunderedProcessItemInfectiousQuery
	{
		public LaunderedProcessItemInfectiousQuery()
		{

		}

		public LaunderedProcessItemInfectiousQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "LaunderedProcessItemInfectiousQuery";
		}
	}

	[Serializable]
	public partial class LaunderedProcessItemInfectiousMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LaunderedProcessItemInfectiousMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LaunderedProcessItemInfectiousMetadata.ColumnNames.ProcessNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessItemInfectiousMetadata.PropertyNames.ProcessNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaunderedProcessItemInfectiousMetadata.ColumnNames.ProcessSeqNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessItemInfectiousMetadata.PropertyNames.ProcessSeqNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(LaunderedProcessItemInfectiousMetadata.ColumnNames.ReceivedNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessItemInfectiousMetadata.PropertyNames.ReceivedNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaunderedProcessItemInfectiousMetadata.ColumnNames.ReceivedSeqNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessItemInfectiousMetadata.PropertyNames.ReceivedSeqNo;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(LaunderedProcessItemInfectiousMetadata.ColumnNames.Qty, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LaunderedProcessItemInfectiousMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(LaunderedProcessItemInfectiousMetadata.ColumnNames.SRItemUnit, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessItemInfectiousMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaunderedProcessItemInfectiousMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaunderedProcessItemInfectiousMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaunderedProcessItemInfectiousMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessItemInfectiousMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public LaunderedProcessItemInfectiousMetadata Meta()
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
			public const string SRItemUnit = "SRItemUnit";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
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
			public const string SRItemUnit = "SRItemUnit";
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
			lock (typeof(LaunderedProcessItemInfectiousMetadata))
			{
				if (LaunderedProcessItemInfectiousMetadata.mapDelegates == null)
				{
					LaunderedProcessItemInfectiousMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (LaunderedProcessItemInfectiousMetadata.meta == null)
				{
					LaunderedProcessItemInfectiousMetadata.meta = new LaunderedProcessItemInfectiousMetadata();
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
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "LaunderedProcessItemInfectious";
				meta.Destination = "LaunderedProcessItemInfectious";
				meta.spInsert = "proc_LaunderedProcessItemInfectiousInsert";
				meta.spUpdate = "proc_LaunderedProcessItemInfectiousUpdate";
				meta.spDelete = "proc_LaunderedProcessItemInfectiousDelete";
				meta.spLoadAll = "proc_LaunderedProcessItemInfectiousLoadAll";
				meta.spLoadByPrimaryKey = "proc_LaunderedProcessItemInfectiousLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LaunderedProcessItemInfectiousMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
