/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/16/2023 4:59:36 PM
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
	abstract public class esComplianceWasteDisposalItemCollection : esEntityCollectionWAuditLog
	{
		public esComplianceWasteDisposalItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ComplianceWasteDisposalItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esComplianceWasteDisposalItemQuery query)
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
			this.InitQuery(query as esComplianceWasteDisposalItemQuery);
		}
		#endregion

		virtual public ComplianceWasteDisposalItem DetachEntity(ComplianceWasteDisposalItem entity)
		{
			return base.DetachEntity(entity) as ComplianceWasteDisposalItem;
		}

		virtual public ComplianceWasteDisposalItem AttachEntity(ComplianceWasteDisposalItem entity)
		{
			return base.AttachEntity(entity) as ComplianceWasteDisposalItem;
		}

		virtual public void Combine(ComplianceWasteDisposalItemCollection collection)
		{
			base.Combine(collection);
		}

		new public ComplianceWasteDisposalItem this[int index]
		{
			get
			{
				return base[index] as ComplianceWasteDisposalItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ComplianceWasteDisposalItem);
		}
	}

	[Serializable]
	abstract public class esComplianceWasteDisposalItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esComplianceWasteDisposalItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esComplianceWasteDisposalItem()
		{
		}

		public esComplianceWasteDisposalItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sRComplianceWasteDisposal)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sRComplianceWasteDisposal);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sRComplianceWasteDisposal);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sRComplianceWasteDisposal)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sRComplianceWasteDisposal);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sRComplianceWasteDisposal);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sRComplianceWasteDisposal)
		{
			esComplianceWasteDisposalItemQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SRComplianceWasteDisposal == sRComplianceWasteDisposal);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sRComplianceWasteDisposal)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("SRComplianceWasteDisposal", sRComplianceWasteDisposal);
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
						case "SRComplianceWasteDisposal": this.str.SRComplianceWasteDisposal = (string)value; break;
						case "IsYes": this.str.IsYes = (string)value; break;
						case "IsNotApplicable": this.str.IsNotApplicable = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "Recommendation": this.str.Recommendation = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IsYes":

							if (value == null || value is System.Boolean)
								this.IsYes = (System.Boolean?)value;
							break;
						case "IsNotApplicable":

							if (value == null || value is System.Boolean)
								this.IsNotApplicable = (System.Boolean?)value;
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
		/// Maps to ComplianceWasteDisposalItem.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ComplianceWasteDisposalItemMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(ComplianceWasteDisposalItemMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to ComplianceWasteDisposalItem.SRComplianceWasteDisposal
		/// </summary>
		virtual public System.String SRComplianceWasteDisposal
		{
			get
			{
				return base.GetSystemString(ComplianceWasteDisposalItemMetadata.ColumnNames.SRComplianceWasteDisposal);
			}

			set
			{
				base.SetSystemString(ComplianceWasteDisposalItemMetadata.ColumnNames.SRComplianceWasteDisposal, value);
			}
		}
		/// <summary>
		/// Maps to ComplianceWasteDisposalItem.IsYes
		/// </summary>
		virtual public System.Boolean? IsYes
		{
			get
			{
				return base.GetSystemBoolean(ComplianceWasteDisposalItemMetadata.ColumnNames.IsYes);
			}

			set
			{
				base.SetSystemBoolean(ComplianceWasteDisposalItemMetadata.ColumnNames.IsYes, value);
			}
		}
		/// <summary>
		/// Maps to ComplianceWasteDisposalItem.IsNotApplicable
		/// </summary>
		virtual public System.Boolean? IsNotApplicable
		{
			get
			{
				return base.GetSystemBoolean(ComplianceWasteDisposalItemMetadata.ColumnNames.IsNotApplicable);
			}

			set
			{
				base.SetSystemBoolean(ComplianceWasteDisposalItemMetadata.ColumnNames.IsNotApplicable, value);
			}
		}
		/// <summary>
		/// Maps to ComplianceWasteDisposalItem.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ComplianceWasteDisposalItemMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(ComplianceWasteDisposalItemMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to ComplianceWasteDisposalItem.Recommendation
		/// </summary>
		virtual public System.String Recommendation
		{
			get
			{
				return base.GetSystemString(ComplianceWasteDisposalItemMetadata.ColumnNames.Recommendation);
			}

			set
			{
				base.SetSystemString(ComplianceWasteDisposalItemMetadata.ColumnNames.Recommendation, value);
			}
		}
		/// <summary>
		/// Maps to ComplianceWasteDisposalItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ComplianceWasteDisposalItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ComplianceWasteDisposalItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ComplianceWasteDisposalItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ComplianceWasteDisposalItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ComplianceWasteDisposalItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esComplianceWasteDisposalItem entity)
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
			public System.String SRComplianceWasteDisposal
			{
				get
				{
					System.String data = entity.SRComplianceWasteDisposal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRComplianceWasteDisposal = null;
					else entity.SRComplianceWasteDisposal = Convert.ToString(value);
				}
			}
			public System.String IsYes
			{
				get
				{
					System.Boolean? data = entity.IsYes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsYes = null;
					else entity.IsYes = Convert.ToBoolean(value);
				}
			}
			public System.String IsNotApplicable
			{
				get
				{
					System.Boolean? data = entity.IsNotApplicable;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNotApplicable = null;
					else entity.IsNotApplicable = Convert.ToBoolean(value);
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
			public System.String Recommendation
			{
				get
				{
					System.String data = entity.Recommendation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Recommendation = null;
					else entity.Recommendation = Convert.ToString(value);
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
			private esComplianceWasteDisposalItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esComplianceWasteDisposalItemQuery query)
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
				throw new Exception("esComplianceWasteDisposalItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ComplianceWasteDisposalItem : esComplianceWasteDisposalItem
	{
	}

	[Serializable]
	abstract public class esComplianceWasteDisposalItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ComplianceWasteDisposalItemMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ComplianceWasteDisposalItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SRComplianceWasteDisposal
		{
			get
			{
				return new esQueryItem(this, ComplianceWasteDisposalItemMetadata.ColumnNames.SRComplianceWasteDisposal, esSystemType.String);
			}
		}

		public esQueryItem IsYes
		{
			get
			{
				return new esQueryItem(this, ComplianceWasteDisposalItemMetadata.ColumnNames.IsYes, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNotApplicable
		{
			get
			{
				return new esQueryItem(this, ComplianceWasteDisposalItemMetadata.ColumnNames.IsNotApplicable, esSystemType.Boolean);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ComplianceWasteDisposalItemMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem Recommendation
		{
			get
			{
				return new esQueryItem(this, ComplianceWasteDisposalItemMetadata.ColumnNames.Recommendation, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ComplianceWasteDisposalItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ComplianceWasteDisposalItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ComplianceWasteDisposalItemCollection")]
	public partial class ComplianceWasteDisposalItemCollection : esComplianceWasteDisposalItemCollection, IEnumerable<ComplianceWasteDisposalItem>
	{
		public ComplianceWasteDisposalItemCollection()
		{

		}

		public static implicit operator List<ComplianceWasteDisposalItem>(ComplianceWasteDisposalItemCollection coll)
		{
			List<ComplianceWasteDisposalItem> list = new List<ComplianceWasteDisposalItem>();

			foreach (ComplianceWasteDisposalItem emp in coll)
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
				return ComplianceWasteDisposalItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ComplianceWasteDisposalItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ComplianceWasteDisposalItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ComplianceWasteDisposalItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ComplianceWasteDisposalItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ComplianceWasteDisposalItemQuery();
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
		public bool Load(ComplianceWasteDisposalItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ComplianceWasteDisposalItem AddNew()
		{
			ComplianceWasteDisposalItem entity = base.AddNewEntity() as ComplianceWasteDisposalItem;

			return entity;
		}
		public ComplianceWasteDisposalItem FindByPrimaryKey(String transactionNo, String sRComplianceWasteDisposal)
		{
			return base.FindByPrimaryKey(transactionNo, sRComplianceWasteDisposal) as ComplianceWasteDisposalItem;
		}

		#region IEnumerable< ComplianceWasteDisposalItem> Members

		IEnumerator<ComplianceWasteDisposalItem> IEnumerable<ComplianceWasteDisposalItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ComplianceWasteDisposalItem;
			}
		}

		#endregion

		private ComplianceWasteDisposalItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ComplianceWasteDisposalItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ComplianceWasteDisposalItem ({TransactionNo, SRComplianceWasteDisposal})")]
	[Serializable]
	public partial class ComplianceWasteDisposalItem : esComplianceWasteDisposalItem
	{
		public ComplianceWasteDisposalItem()
		{
		}

		public ComplianceWasteDisposalItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ComplianceWasteDisposalItemMetadata.Meta();
			}
		}

		override protected esComplianceWasteDisposalItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ComplianceWasteDisposalItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ComplianceWasteDisposalItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ComplianceWasteDisposalItemQuery();
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
		public bool Load(ComplianceWasteDisposalItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ComplianceWasteDisposalItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ComplianceWasteDisposalItemQuery : esComplianceWasteDisposalItemQuery
	{
		public ComplianceWasteDisposalItemQuery()
		{

		}

		public ComplianceWasteDisposalItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ComplianceWasteDisposalItemQuery";
		}
	}

	[Serializable]
	public partial class ComplianceWasteDisposalItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ComplianceWasteDisposalItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ComplianceWasteDisposalItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplianceWasteDisposalItemMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ComplianceWasteDisposalItemMetadata.ColumnNames.SRComplianceWasteDisposal, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplianceWasteDisposalItemMetadata.PropertyNames.SRComplianceWasteDisposal;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ComplianceWasteDisposalItemMetadata.ColumnNames.IsYes, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ComplianceWasteDisposalItemMetadata.PropertyNames.IsYes;
			_columns.Add(c);

			c = new esColumnMetadata(ComplianceWasteDisposalItemMetadata.ColumnNames.IsNotApplicable, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ComplianceWasteDisposalItemMetadata.PropertyNames.IsNotApplicable;
			_columns.Add(c);

			c = new esColumnMetadata(ComplianceWasteDisposalItemMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplianceWasteDisposalItemMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplianceWasteDisposalItemMetadata.ColumnNames.Recommendation, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplianceWasteDisposalItemMetadata.PropertyNames.Recommendation;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplianceWasteDisposalItemMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ComplianceWasteDisposalItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ComplianceWasteDisposalItemMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ComplianceWasteDisposalItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ComplianceWasteDisposalItemMetadata Meta()
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
			public const string SRComplianceWasteDisposal = "SRComplianceWasteDisposal";
			public const string IsYes = "IsYes";
			public const string IsNotApplicable = "IsNotApplicable";
			public const string Notes = "Notes";
			public const string Recommendation = "Recommendation";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string SRComplianceWasteDisposal = "SRComplianceWasteDisposal";
			public const string IsYes = "IsYes";
			public const string IsNotApplicable = "IsNotApplicable";
			public const string Notes = "Notes";
			public const string Recommendation = "Recommendation";
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
			lock (typeof(ComplianceWasteDisposalItemMetadata))
			{
				if (ComplianceWasteDisposalItemMetadata.mapDelegates == null)
				{
					ComplianceWasteDisposalItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ComplianceWasteDisposalItemMetadata.meta == null)
				{
					ComplianceWasteDisposalItemMetadata.meta = new ComplianceWasteDisposalItemMetadata();
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
				meta.AddTypeMap("SRComplianceWasteDisposal", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsYes", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNotApplicable", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Recommendation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "ComplianceWasteDisposalItem";
				meta.Destination = "ComplianceWasteDisposalItem";
				meta.spInsert = "proc_ComplianceWasteDisposalItemInsert";
				meta.spUpdate = "proc_ComplianceWasteDisposalItemUpdate";
				meta.spDelete = "proc_ComplianceWasteDisposalItemDelete";
				meta.spLoadAll = "proc_ComplianceWasteDisposalItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_ComplianceWasteDisposalItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ComplianceWasteDisposalItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
