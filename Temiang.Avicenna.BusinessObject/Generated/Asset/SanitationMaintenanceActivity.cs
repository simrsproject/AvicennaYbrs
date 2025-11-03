/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/3/2021 9:56:45 AM
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
	abstract public class esSanitationMaintenanceActivityCollection : esEntityCollectionWAuditLog
	{
		public esSanitationMaintenanceActivityCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "SanitationMaintenanceActivityCollection";
		}

		#region Query Logic
		protected void InitQuery(esSanitationMaintenanceActivityQuery query)
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
			this.InitQuery(query as esSanitationMaintenanceActivityQuery);
		}
		#endregion

		virtual public SanitationMaintenanceActivity DetachEntity(SanitationMaintenanceActivity entity)
		{
			return base.DetachEntity(entity) as SanitationMaintenanceActivity;
		}

		virtual public SanitationMaintenanceActivity AttachEntity(SanitationMaintenanceActivity entity)
		{
			return base.AttachEntity(entity) as SanitationMaintenanceActivity;
		}

		virtual public void Combine(SanitationMaintenanceActivityCollection collection)
		{
			base.Combine(collection);
		}

		new public SanitationMaintenanceActivity this[int index]
		{
			get
			{
				return base[index] as SanitationMaintenanceActivity;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SanitationMaintenanceActivity);
		}
	}

	[Serializable]
	abstract public class esSanitationMaintenanceActivity : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSanitationMaintenanceActivityQuery GetDynamicQuery()
		{
			return null;
		}

		public esSanitationMaintenanceActivity()
		{
		}

		public esSanitationMaintenanceActivity(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo)
		{
			esSanitationMaintenanceActivityQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
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
						case "TransactionDate": this.str.TransactionDate = (string)value; break;
						case "SRWorkTradeItem": this.str.SRWorkTradeItem = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "TargetDate": this.str.TargetDate = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "TransactionDate":

							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
							break;
						case "TargetDate":

							if (value == null || value is System.DateTime)
								this.TargetDate = (System.DateTime?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDateTime":

							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
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
		/// Maps to SanitationMaintenanceActivity.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(SanitationMaintenanceActivityMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(SanitationMaintenanceActivityMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivity.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(SanitationMaintenanceActivityMetadata.ColumnNames.TransactionDate);
			}

			set
			{
				base.SetSystemDateTime(SanitationMaintenanceActivityMetadata.ColumnNames.TransactionDate, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivity.SRWorkTradeItem
		/// </summary>
		virtual public System.String SRWorkTradeItem
		{
			get
			{
				return base.GetSystemString(SanitationMaintenanceActivityMetadata.ColumnNames.SRWorkTradeItem);
			}

			set
			{
				base.SetSystemString(SanitationMaintenanceActivityMetadata.ColumnNames.SRWorkTradeItem, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivity.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(SanitationMaintenanceActivityMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(SanitationMaintenanceActivityMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivity.TargetDate
		/// </summary>
		virtual public System.DateTime? TargetDate
		{
			get
			{
				return base.GetSystemDateTime(SanitationMaintenanceActivityMetadata.ColumnNames.TargetDate);
			}

			set
			{
				base.SetSystemDateTime(SanitationMaintenanceActivityMetadata.ColumnNames.TargetDate, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivity.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(SanitationMaintenanceActivityMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(SanitationMaintenanceActivityMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivity.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(SanitationMaintenanceActivityMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(SanitationMaintenanceActivityMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivity.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(SanitationMaintenanceActivityMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(SanitationMaintenanceActivityMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivity.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(SanitationMaintenanceActivityMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(SanitationMaintenanceActivityMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivity.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SanitationMaintenanceActivityMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(SanitationMaintenanceActivityMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivity.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SanitationMaintenanceActivityMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(SanitationMaintenanceActivityMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSanitationMaintenanceActivity entity)
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
			public System.String TransactionDate
			{
				get
				{
					System.DateTime? data = entity.TransactionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionDate = null;
					else entity.TransactionDate = Convert.ToDateTime(value);
				}
			}
			public System.String SRWorkTradeItem
			{
				get
				{
					System.String data = entity.SRWorkTradeItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRWorkTradeItem = null;
					else entity.SRWorkTradeItem = Convert.ToString(value);
				}
			}
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String TargetDate
			{
				get
				{
					System.DateTime? data = entity.TargetDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TargetDate = null;
					else entity.TargetDate = Convert.ToDateTime(value);
				}
			}
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
				}
			}
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
				}
			}
			public System.String VoidDateTime
			{
				get
				{
					System.DateTime? data = entity.VoidDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDateTime = null;
					else entity.VoidDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VoidByUserID
			{
				get
				{
					System.String data = entity.VoidByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidByUserID = null;
					else entity.VoidByUserID = Convert.ToString(value);
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
			private esSanitationMaintenanceActivity entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSanitationMaintenanceActivityQuery query)
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
				throw new Exception("esSanitationMaintenanceActivity can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SanitationMaintenanceActivity : esSanitationMaintenanceActivity
	{
	}

	[Serializable]
	abstract public class esSanitationMaintenanceActivityQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return SanitationMaintenanceActivityMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SRWorkTradeItem
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityMetadata.ColumnNames.SRWorkTradeItem, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem TargetDate
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityMetadata.ColumnNames.TargetDate, esSystemType.DateTime);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SanitationMaintenanceActivityCollection")]
	public partial class SanitationMaintenanceActivityCollection : esSanitationMaintenanceActivityCollection, IEnumerable<SanitationMaintenanceActivity>
	{
		public SanitationMaintenanceActivityCollection()
		{

		}

		public static implicit operator List<SanitationMaintenanceActivity>(SanitationMaintenanceActivityCollection coll)
		{
			List<SanitationMaintenanceActivity> list = new List<SanitationMaintenanceActivity>();

			foreach (SanitationMaintenanceActivity emp in coll)
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
				return SanitationMaintenanceActivityMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationMaintenanceActivityQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SanitationMaintenanceActivity(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SanitationMaintenanceActivity();
		}

		#endregion

		[BrowsableAttribute(false)]
		public SanitationMaintenanceActivityQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationMaintenanceActivityQuery();
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
		public bool Load(SanitationMaintenanceActivityQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SanitationMaintenanceActivity AddNew()
		{
			SanitationMaintenanceActivity entity = base.AddNewEntity() as SanitationMaintenanceActivity;

			return entity;
		}
		public SanitationMaintenanceActivity FindByPrimaryKey(String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as SanitationMaintenanceActivity;
		}

		#region IEnumerable< SanitationMaintenanceActivity> Members

		IEnumerator<SanitationMaintenanceActivity> IEnumerable<SanitationMaintenanceActivity>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as SanitationMaintenanceActivity;
			}
		}

		#endregion

		private SanitationMaintenanceActivityQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SanitationMaintenanceActivity' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SanitationMaintenanceActivity ({TransactionNo})")]
	[Serializable]
	public partial class SanitationMaintenanceActivity : esSanitationMaintenanceActivity
	{
		public SanitationMaintenanceActivity()
		{
		}

		public SanitationMaintenanceActivity(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SanitationMaintenanceActivityMetadata.Meta();
			}
		}

		override protected esSanitationMaintenanceActivityQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationMaintenanceActivityQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public SanitationMaintenanceActivityQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationMaintenanceActivityQuery();
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
		public bool Load(SanitationMaintenanceActivityQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private SanitationMaintenanceActivityQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SanitationMaintenanceActivityQuery : esSanitationMaintenanceActivityQuery
	{
		public SanitationMaintenanceActivityQuery()
		{

		}

		public SanitationMaintenanceActivityQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SanitationMaintenanceActivityQuery";
		}
	}

	[Serializable]
	public partial class SanitationMaintenanceActivityMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SanitationMaintenanceActivityMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SanitationMaintenanceActivityMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationMaintenanceActivityMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationMaintenanceActivityMetadata.PropertyNames.TransactionDate;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityMetadata.ColumnNames.SRWorkTradeItem, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationMaintenanceActivityMetadata.PropertyNames.SRWorkTradeItem;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityMetadata.ColumnNames.ServiceUnitID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationMaintenanceActivityMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityMetadata.ColumnNames.TargetDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationMaintenanceActivityMetadata.PropertyNames.TargetDate;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityMetadata.ColumnNames.IsApproved, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SanitationMaintenanceActivityMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityMetadata.ColumnNames.IsVoid, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SanitationMaintenanceActivityMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityMetadata.ColumnNames.VoidDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationMaintenanceActivityMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityMetadata.ColumnNames.VoidByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationMaintenanceActivityMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationMaintenanceActivityMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationMaintenanceActivityMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public SanitationMaintenanceActivityMetadata Meta()
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
			public const string TransactionDate = "TransactionDate";
			public const string SRWorkTradeItem = "SRWorkTradeItem";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string TargetDate = "TargetDate";
			public const string IsApproved = "IsApproved";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string TransactionDate = "TransactionDate";
			public const string SRWorkTradeItem = "SRWorkTradeItem";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string TargetDate = "TargetDate";
			public const string IsApproved = "IsApproved";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
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
			lock (typeof(SanitationMaintenanceActivityMetadata))
			{
				if (SanitationMaintenanceActivityMetadata.mapDelegates == null)
				{
					SanitationMaintenanceActivityMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (SanitationMaintenanceActivityMetadata.meta == null)
				{
					SanitationMaintenanceActivityMetadata.meta = new SanitationMaintenanceActivityMetadata();
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
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRWorkTradeItem", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TargetDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "SanitationMaintenanceActivity";
				meta.Destination = "SanitationMaintenanceActivity";
				meta.spInsert = "proc_SanitationMaintenanceActivityInsert";
				meta.spUpdate = "proc_SanitationMaintenanceActivityUpdate";
				meta.spDelete = "proc_SanitationMaintenanceActivityDelete";
				meta.spLoadAll = "proc_SanitationMaintenanceActivityLoadAll";
				meta.spLoadByPrimaryKey = "proc_SanitationMaintenanceActivityLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SanitationMaintenanceActivityMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
