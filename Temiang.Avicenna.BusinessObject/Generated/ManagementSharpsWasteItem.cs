/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/16/2023 4:55:17 PM
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
	abstract public class esManagementSharpsWasteItemCollection : esEntityCollectionWAuditLog
	{
		public esManagementSharpsWasteItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ManagementSharpsWasteItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esManagementSharpsWasteItemQuery query)
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
			this.InitQuery(query as esManagementSharpsWasteItemQuery);
		}
		#endregion

		virtual public ManagementSharpsWasteItem DetachEntity(ManagementSharpsWasteItem entity)
		{
			return base.DetachEntity(entity) as ManagementSharpsWasteItem;
		}

		virtual public ManagementSharpsWasteItem AttachEntity(ManagementSharpsWasteItem entity)
		{
			return base.AttachEntity(entity) as ManagementSharpsWasteItem;
		}

		virtual public void Combine(ManagementSharpsWasteItemCollection collection)
		{
			base.Combine(collection);
		}

		new public ManagementSharpsWasteItem this[int index]
		{
			get
			{
				return base[index] as ManagementSharpsWasteItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ManagementSharpsWasteItem);
		}
	}

	[Serializable]
	abstract public class esManagementSharpsWasteItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esManagementSharpsWasteItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esManagementSharpsWasteItem()
		{
		}

		public esManagementSharpsWasteItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sRManagementSharpsWaste)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sRManagementSharpsWaste);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sRManagementSharpsWaste);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sRManagementSharpsWaste)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sRManagementSharpsWaste);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sRManagementSharpsWaste);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sRManagementSharpsWaste)
		{
			esManagementSharpsWasteItemQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SRManagementSharpsWaste == sRManagementSharpsWaste);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sRManagementSharpsWaste)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("SRManagementSharpsWaste", sRManagementSharpsWaste);
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
						case "SRManagementSharpsWaste": this.str.SRManagementSharpsWaste = (string)value; break;
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
		/// Maps to ManagementSharpsWasteItem.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ManagementSharpsWasteItemMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(ManagementSharpsWasteItemMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to ManagementSharpsWasteItem.SRManagementSharpsWaste
		/// </summary>
		virtual public System.String SRManagementSharpsWaste
		{
			get
			{
				return base.GetSystemString(ManagementSharpsWasteItemMetadata.ColumnNames.SRManagementSharpsWaste);
			}

			set
			{
				base.SetSystemString(ManagementSharpsWasteItemMetadata.ColumnNames.SRManagementSharpsWaste, value);
			}
		}
		/// <summary>
		/// Maps to ManagementSharpsWasteItem.IsYes
		/// </summary>
		virtual public System.Boolean? IsYes
		{
			get
			{
				return base.GetSystemBoolean(ManagementSharpsWasteItemMetadata.ColumnNames.IsYes);
			}

			set
			{
				base.SetSystemBoolean(ManagementSharpsWasteItemMetadata.ColumnNames.IsYes, value);
			}
		}
		/// <summary>
		/// Maps to ManagementSharpsWasteItem.IsNotApplicable
		/// </summary>
		virtual public System.Boolean? IsNotApplicable
		{
			get
			{
				return base.GetSystemBoolean(ManagementSharpsWasteItemMetadata.ColumnNames.IsNotApplicable);
			}

			set
			{
				base.SetSystemBoolean(ManagementSharpsWasteItemMetadata.ColumnNames.IsNotApplicable, value);
			}
		}
		/// <summary>
		/// Maps to ManagementSharpsWasteItem.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ManagementSharpsWasteItemMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(ManagementSharpsWasteItemMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to ManagementSharpsWasteItem.Recommendation
		/// </summary>
		virtual public System.String Recommendation
		{
			get
			{
				return base.GetSystemString(ManagementSharpsWasteItemMetadata.ColumnNames.Recommendation);
			}

			set
			{
				base.SetSystemString(ManagementSharpsWasteItemMetadata.ColumnNames.Recommendation, value);
			}
		}
		/// <summary>
		/// Maps to ManagementSharpsWasteItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ManagementSharpsWasteItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ManagementSharpsWasteItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ManagementSharpsWasteItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ManagementSharpsWasteItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ManagementSharpsWasteItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esManagementSharpsWasteItem entity)
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
			public System.String SRManagementSharpsWaste
			{
				get
				{
					System.String data = entity.SRManagementSharpsWaste;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRManagementSharpsWaste = null;
					else entity.SRManagementSharpsWaste = Convert.ToString(value);
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
			private esManagementSharpsWasteItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esManagementSharpsWasteItemQuery query)
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
				throw new Exception("esManagementSharpsWasteItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ManagementSharpsWasteItem : esManagementSharpsWasteItem
	{
	}

	[Serializable]
	abstract public class esManagementSharpsWasteItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ManagementSharpsWasteItemMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ManagementSharpsWasteItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SRManagementSharpsWaste
		{
			get
			{
				return new esQueryItem(this, ManagementSharpsWasteItemMetadata.ColumnNames.SRManagementSharpsWaste, esSystemType.String);
			}
		}

		public esQueryItem IsYes
		{
			get
			{
				return new esQueryItem(this, ManagementSharpsWasteItemMetadata.ColumnNames.IsYes, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNotApplicable
		{
			get
			{
				return new esQueryItem(this, ManagementSharpsWasteItemMetadata.ColumnNames.IsNotApplicable, esSystemType.Boolean);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ManagementSharpsWasteItemMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem Recommendation
		{
			get
			{
				return new esQueryItem(this, ManagementSharpsWasteItemMetadata.ColumnNames.Recommendation, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ManagementSharpsWasteItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ManagementSharpsWasteItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ManagementSharpsWasteItemCollection")]
	public partial class ManagementSharpsWasteItemCollection : esManagementSharpsWasteItemCollection, IEnumerable<ManagementSharpsWasteItem>
	{
		public ManagementSharpsWasteItemCollection()
		{

		}

		public static implicit operator List<ManagementSharpsWasteItem>(ManagementSharpsWasteItemCollection coll)
		{
			List<ManagementSharpsWasteItem> list = new List<ManagementSharpsWasteItem>();

			foreach (ManagementSharpsWasteItem emp in coll)
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
				return ManagementSharpsWasteItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ManagementSharpsWasteItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ManagementSharpsWasteItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ManagementSharpsWasteItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ManagementSharpsWasteItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ManagementSharpsWasteItemQuery();
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
		public bool Load(ManagementSharpsWasteItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ManagementSharpsWasteItem AddNew()
		{
			ManagementSharpsWasteItem entity = base.AddNewEntity() as ManagementSharpsWasteItem;

			return entity;
		}
		public ManagementSharpsWasteItem FindByPrimaryKey(String transactionNo, String sRManagementSharpsWaste)
		{
			return base.FindByPrimaryKey(transactionNo, sRManagementSharpsWaste) as ManagementSharpsWasteItem;
		}

		#region IEnumerable< ManagementSharpsWasteItem> Members

		IEnumerator<ManagementSharpsWasteItem> IEnumerable<ManagementSharpsWasteItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ManagementSharpsWasteItem;
			}
		}

		#endregion

		private ManagementSharpsWasteItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ManagementSharpsWasteItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ManagementSharpsWasteItem ({TransactionNo, SRManagementSharpsWaste})")]
	[Serializable]
	public partial class ManagementSharpsWasteItem : esManagementSharpsWasteItem
	{
		public ManagementSharpsWasteItem()
		{
		}

		public ManagementSharpsWasteItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ManagementSharpsWasteItemMetadata.Meta();
			}
		}

		override protected esManagementSharpsWasteItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ManagementSharpsWasteItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ManagementSharpsWasteItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ManagementSharpsWasteItemQuery();
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
		public bool Load(ManagementSharpsWasteItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ManagementSharpsWasteItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ManagementSharpsWasteItemQuery : esManagementSharpsWasteItemQuery
	{
		public ManagementSharpsWasteItemQuery()
		{

		}

		public ManagementSharpsWasteItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ManagementSharpsWasteItemQuery";
		}
	}

	[Serializable]
	public partial class ManagementSharpsWasteItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ManagementSharpsWasteItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ManagementSharpsWasteItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ManagementSharpsWasteItemMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ManagementSharpsWasteItemMetadata.ColumnNames.SRManagementSharpsWaste, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ManagementSharpsWasteItemMetadata.PropertyNames.SRManagementSharpsWaste;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ManagementSharpsWasteItemMetadata.ColumnNames.IsYes, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ManagementSharpsWasteItemMetadata.PropertyNames.IsYes;
			_columns.Add(c);

			c = new esColumnMetadata(ManagementSharpsWasteItemMetadata.ColumnNames.IsNotApplicable, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ManagementSharpsWasteItemMetadata.PropertyNames.IsNotApplicable;
			_columns.Add(c);

			c = new esColumnMetadata(ManagementSharpsWasteItemMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ManagementSharpsWasteItemMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ManagementSharpsWasteItemMetadata.ColumnNames.Recommendation, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ManagementSharpsWasteItemMetadata.PropertyNames.Recommendation;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ManagementSharpsWasteItemMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ManagementSharpsWasteItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ManagementSharpsWasteItemMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ManagementSharpsWasteItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ManagementSharpsWasteItemMetadata Meta()
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
			public const string SRManagementSharpsWaste = "SRManagementSharpsWaste";
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
			public const string SRManagementSharpsWaste = "SRManagementSharpsWaste";
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
			lock (typeof(ManagementSharpsWasteItemMetadata))
			{
				if (ManagementSharpsWasteItemMetadata.mapDelegates == null)
				{
					ManagementSharpsWasteItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ManagementSharpsWasteItemMetadata.meta == null)
				{
					ManagementSharpsWasteItemMetadata.meta = new ManagementSharpsWasteItemMetadata();
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
				meta.AddTypeMap("SRManagementSharpsWaste", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsYes", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNotApplicable", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Recommendation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "ManagementSharpsWasteItem";
				meta.Destination = "ManagementSharpsWasteItem";
				meta.spInsert = "proc_ManagementSharpsWasteItemInsert";
				meta.spUpdate = "proc_ManagementSharpsWasteItemUpdate";
				meta.spDelete = "proc_ManagementSharpsWasteItemDelete";
				meta.spLoadAll = "proc_ManagementSharpsWasteItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_ManagementSharpsWasteItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ManagementSharpsWasteItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
