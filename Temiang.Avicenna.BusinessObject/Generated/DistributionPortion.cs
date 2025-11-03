/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/9/2020 1:36:06 PM
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
	abstract public class esDistributionPortionCollection : esEntityCollectionWAuditLog
	{
		public esDistributionPortionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "DistributionPortionCollection";
		}

		#region Query Logic
		protected void InitQuery(esDistributionPortionQuery query)
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
			this.InitQuery(query as esDistributionPortionQuery);
		}
		#endregion

		virtual public DistributionPortion DetachEntity(DistributionPortion entity)
		{
			return base.DetachEntity(entity) as DistributionPortion;
		}

		virtual public DistributionPortion AttachEntity(DistributionPortion entity)
		{
			return base.AttachEntity(entity) as DistributionPortion;
		}

		virtual public void Combine(DistributionPortionCollection collection)
		{
			base.Combine(collection);
		}

		new public DistributionPortion this[int index]
		{
			get
			{
				return base[index] as DistributionPortion;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(DistributionPortion);
		}
	}

	[Serializable]
	abstract public class esDistributionPortion : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDistributionPortionQuery GetDynamicQuery()
		{
			return null;
		}

		public esDistributionPortion()
		{
		}

		public esDistributionPortion(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String orderNo, String sRMealSet)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo, sRMealSet);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo, sRMealSet);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String orderNo, String sRMealSet)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo, sRMealSet);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo, sRMealSet);
		}

		private bool LoadByPrimaryKeyDynamic(String orderNo, String sRMealSet)
		{
			esDistributionPortionQuery query = this.GetDynamicQuery();
			query.Where(query.OrderNo == orderNo, query.SRMealSet == sRMealSet);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String orderNo, String sRMealSet)
		{
			esParameters parms = new esParameters();
			parms.Add("OrderNo", orderNo);
			parms.Add("SRMealSet", sRMealSet);
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
						case "OrderNo": this.str.OrderNo = (string)value; break;
						case "SRMealSet": this.str.SRMealSet = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsInvalid": this.str.IsInvalid = (string)value; break;
						case "SRInvalidReason": this.str.SRInvalidReason = (string)value; break;
						case "CheckedByUserID": this.str.CheckedByUserID = (string)value; break;
						case "CheckedDateTime": this.str.CheckedDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsInvalid":

							if (value == null || value is System.Boolean)
								this.IsInvalid = (System.Boolean?)value;
							break;
						case "CheckedDateTime":

							if (value == null || value is System.DateTime)
								this.CheckedDateTime = (System.DateTime?)value;
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
		/// Maps to DistributionPortion.OrderNo
		/// </summary>
		virtual public System.String OrderNo
		{
			get
			{
				return base.GetSystemString(DistributionPortionMetadata.ColumnNames.OrderNo);
			}

			set
			{
				base.SetSystemString(DistributionPortionMetadata.ColumnNames.OrderNo, value);
			}
		}
		/// <summary>
		/// Maps to DistributionPortion.SRMealSet
		/// </summary>
		virtual public System.String SRMealSet
		{
			get
			{
				return base.GetSystemString(DistributionPortionMetadata.ColumnNames.SRMealSet);
			}

			set
			{
				base.SetSystemString(DistributionPortionMetadata.ColumnNames.SRMealSet, value);
			}
		}
		/// <summary>
		/// Maps to DistributionPortion.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(DistributionPortionMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(DistributionPortionMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to DistributionPortion.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(DistributionPortionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(DistributionPortionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to DistributionPortion.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(DistributionPortionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(DistributionPortionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to DistributionPortion.IsInvalid
		/// </summary>
		virtual public System.Boolean? IsInvalid
		{
			get
			{
				return base.GetSystemBoolean(DistributionPortionMetadata.ColumnNames.IsInvalid);
			}

			set
			{
				base.SetSystemBoolean(DistributionPortionMetadata.ColumnNames.IsInvalid, value);
			}
		}
		/// <summary>
		/// Maps to DistributionPortion.SRInvalidReason
		/// </summary>
		virtual public System.String SRInvalidReason
		{
			get
			{
				return base.GetSystemString(DistributionPortionMetadata.ColumnNames.SRInvalidReason);
			}

			set
			{
				base.SetSystemString(DistributionPortionMetadata.ColumnNames.SRInvalidReason, value);
			}
		}
		/// <summary>
		/// Maps to DistributionPortion.CheckedByUserID
		/// </summary>
		virtual public System.String CheckedByUserID
		{
			get
			{
				return base.GetSystemString(DistributionPortionMetadata.ColumnNames.CheckedByUserID);
			}

			set
			{
				base.SetSystemString(DistributionPortionMetadata.ColumnNames.CheckedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to DistributionPortion.CheckedDateTime
		/// </summary>
		virtual public System.DateTime? CheckedDateTime
		{
			get
			{
				return base.GetSystemDateTime(DistributionPortionMetadata.ColumnNames.CheckedDateTime);
			}

			set
			{
				base.SetSystemDateTime(DistributionPortionMetadata.ColumnNames.CheckedDateTime, value);
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
			public esStrings(esDistributionPortion entity)
			{
				this.entity = entity;
			}
			public System.String OrderNo
			{
				get
				{
					System.String data = entity.OrderNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderNo = null;
					else entity.OrderNo = Convert.ToString(value);
				}
			}
			public System.String SRMealSet
			{
				get
				{
					System.String data = entity.SRMealSet;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMealSet = null;
					else entity.SRMealSet = Convert.ToString(value);
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
			public System.String IsInvalid
			{
				get
				{
					System.Boolean? data = entity.IsInvalid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInvalid = null;
					else entity.IsInvalid = Convert.ToBoolean(value);
				}
			}
			public System.String SRInvalidReason
			{
				get
				{
					System.String data = entity.SRInvalidReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRInvalidReason = null;
					else entity.SRInvalidReason = Convert.ToString(value);
				}
			}
			public System.String CheckedByUserID
			{
				get
				{
					System.String data = entity.CheckedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckedByUserID = null;
					else entity.CheckedByUserID = Convert.ToString(value);
				}
			}
			public System.String CheckedDateTime
			{
				get
				{
					System.DateTime? data = entity.CheckedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckedDateTime = null;
					else entity.CheckedDateTime = Convert.ToDateTime(value);
				}
			}
			private esDistributionPortion entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDistributionPortionQuery query)
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
				throw new Exception("esDistributionPortion can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class DistributionPortion : esDistributionPortion
	{
	}

	[Serializable]
	abstract public class esDistributionPortionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return DistributionPortionMetadata.Meta();
			}
		}

		public esQueryItem OrderNo
		{
			get
			{
				return new esQueryItem(this, DistributionPortionMetadata.ColumnNames.OrderNo, esSystemType.String);
			}
		}

		public esQueryItem SRMealSet
		{
			get
			{
				return new esQueryItem(this, DistributionPortionMetadata.ColumnNames.SRMealSet, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, DistributionPortionMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, DistributionPortionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, DistributionPortionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsInvalid
		{
			get
			{
				return new esQueryItem(this, DistributionPortionMetadata.ColumnNames.IsInvalid, esSystemType.Boolean);
			}
		}

		public esQueryItem SRInvalidReason
		{
			get
			{
				return new esQueryItem(this, DistributionPortionMetadata.ColumnNames.SRInvalidReason, esSystemType.String);
			}
		}

		public esQueryItem CheckedByUserID
		{
			get
			{
				return new esQueryItem(this, DistributionPortionMetadata.ColumnNames.CheckedByUserID, esSystemType.String);
			}
		}

		public esQueryItem CheckedDateTime
		{
			get
			{
				return new esQueryItem(this, DistributionPortionMetadata.ColumnNames.CheckedDateTime, esSystemType.DateTime);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DistributionPortionCollection")]
	public partial class DistributionPortionCollection : esDistributionPortionCollection, IEnumerable<DistributionPortion>
	{
		public DistributionPortionCollection()
		{

		}

		public static implicit operator List<DistributionPortion>(DistributionPortionCollection coll)
		{
			List<DistributionPortion> list = new List<DistributionPortion>();

			foreach (DistributionPortion emp in coll)
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
				return DistributionPortionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DistributionPortionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new DistributionPortion(row);
		}

		override protected esEntity CreateEntity()
		{
			return new DistributionPortion();
		}

		#endregion

		[BrowsableAttribute(false)]
		public DistributionPortionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DistributionPortionQuery();
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
		public bool Load(DistributionPortionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public DistributionPortion AddNew()
		{
			DistributionPortion entity = base.AddNewEntity() as DistributionPortion;

			return entity;
		}
		public DistributionPortion FindByPrimaryKey(String orderNo, String sRMealSet)
		{
			return base.FindByPrimaryKey(orderNo, sRMealSet) as DistributionPortion;
		}

		#region IEnumerable< DistributionPortion> Members

		IEnumerator<DistributionPortion> IEnumerable<DistributionPortion>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as DistributionPortion;
			}
		}

		#endregion

		private DistributionPortionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'DistributionPortion' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("DistributionPortion ({OrderNo, SRMealSet})")]
	[Serializable]
	public partial class DistributionPortion : esDistributionPortion
	{
		public DistributionPortion()
		{
		}

		public DistributionPortion(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DistributionPortionMetadata.Meta();
			}
		}

		override protected esDistributionPortionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DistributionPortionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public DistributionPortionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DistributionPortionQuery();
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
		public bool Load(DistributionPortionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private DistributionPortionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class DistributionPortionQuery : esDistributionPortionQuery
	{
		public DistributionPortionQuery()
		{

		}

		public DistributionPortionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "DistributionPortionQuery";
		}
	}

	[Serializable]
	public partial class DistributionPortionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DistributionPortionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DistributionPortionMetadata.ColumnNames.OrderNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = DistributionPortionMetadata.PropertyNames.OrderNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(DistributionPortionMetadata.ColumnNames.SRMealSet, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = DistributionPortionMetadata.PropertyNames.SRMealSet;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(DistributionPortionMetadata.ColumnNames.IsVoid, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DistributionPortionMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DistributionPortionMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DistributionPortionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DistributionPortionMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = DistributionPortionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DistributionPortionMetadata.ColumnNames.IsInvalid, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DistributionPortionMetadata.PropertyNames.IsInvalid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DistributionPortionMetadata.ColumnNames.SRInvalidReason, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = DistributionPortionMetadata.PropertyNames.SRInvalidReason;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DistributionPortionMetadata.ColumnNames.CheckedByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = DistributionPortionMetadata.PropertyNames.CheckedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DistributionPortionMetadata.ColumnNames.CheckedDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DistributionPortionMetadata.PropertyNames.CheckedDateTime;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public DistributionPortionMetadata Meta()
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
			public const string OrderNo = "OrderNo";
			public const string SRMealSet = "SRMealSet";
			public const string IsVoid = "IsVoid";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsInvalid = "IsInvalid";
			public const string SRInvalidReason = "SRInvalidReason";
			public const string CheckedByUserID = "CheckedByUserID";
			public const string CheckedDateTime = "CheckedDateTime";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string OrderNo = "OrderNo";
			public const string SRMealSet = "SRMealSet";
			public const string IsVoid = "IsVoid";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsInvalid = "IsInvalid";
			public const string SRInvalidReason = "SRInvalidReason";
			public const string CheckedByUserID = "CheckedByUserID";
			public const string CheckedDateTime = "CheckedDateTime";
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
			lock (typeof(DistributionPortionMetadata))
			{
				if (DistributionPortionMetadata.mapDelegates == null)
				{
					DistributionPortionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (DistributionPortionMetadata.meta == null)
				{
					DistributionPortionMetadata.meta = new DistributionPortionMetadata();
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

				meta.AddTypeMap("OrderNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMealSet", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsInvalid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRInvalidReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CheckedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CheckedDateTime", new esTypeMap("datetime", "System.DateTime"));


				meta.Source = "DistributionPortion";
				meta.Destination = "DistributionPortion";
				meta.spInsert = "proc_DistributionPortionInsert";
				meta.spUpdate = "proc_DistributionPortionUpdate";
				meta.spDelete = "proc_DistributionPortionDelete";
				meta.spLoadAll = "proc_DistributionPortionLoadAll";
				meta.spLoadByPrimaryKey = "proc_DistributionPortionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DistributionPortionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
