/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/23/2022 5:57:23 PM
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
	abstract public class esEmployeeIncentiveProcessItemCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeIncentiveProcessItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeIncentiveProcessItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeIncentiveProcessItemQuery query)
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
			this.InitQuery(query as esEmployeeIncentiveProcessItemQuery);
		}
		#endregion

		virtual public EmployeeIncentiveProcessItem DetachEntity(EmployeeIncentiveProcessItem entity)
		{
			return base.DetachEntity(entity) as EmployeeIncentiveProcessItem;
		}

		virtual public EmployeeIncentiveProcessItem AttachEntity(EmployeeIncentiveProcessItem entity)
		{
			return base.AttachEntity(entity) as EmployeeIncentiveProcessItem;
		}

		virtual public void Combine(EmployeeIncentiveProcessItemCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeIncentiveProcessItem this[int index]
		{
			get
			{
				return base[index] as EmployeeIncentiveProcessItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeIncentiveProcessItem);
		}
	}

	[Serializable]
	abstract public class esEmployeeIncentiveProcessItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeIncentiveProcessItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeIncentiveProcessItem()
		{
		}

		public esEmployeeIncentiveProcessItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 employeeIncentiveProcessID, String sRIncentiveServiceUnitGroup)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeIncentiveProcessID, sRIncentiveServiceUnitGroup);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeIncentiveProcessID, sRIncentiveServiceUnitGroup);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 employeeIncentiveProcessID, String sRIncentiveServiceUnitGroup)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeIncentiveProcessID, sRIncentiveServiceUnitGroup);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeIncentiveProcessID, sRIncentiveServiceUnitGroup);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 employeeIncentiveProcessID, String sRIncentiveServiceUnitGroup)
		{
			esEmployeeIncentiveProcessItemQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeIncentiveProcessID == employeeIncentiveProcessID, query.SRIncentiveServiceUnitGroup == sRIncentiveServiceUnitGroup);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 employeeIncentiveProcessID, String sRIncentiveServiceUnitGroup)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeIncentiveProcessID", employeeIncentiveProcessID);
			parms.Add("SRIncentiveServiceUnitGroup", sRIncentiveServiceUnitGroup);
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
						case "EmployeeIncentiveProcessID": this.str.EmployeeIncentiveProcessID = (string)value; break;
						case "SRIncentiveServiceUnitGroup": this.str.SRIncentiveServiceUnitGroup = (string)value; break;
						case "Nominal": this.str.Nominal = (string)value; break;
						case "TotalPoint": this.str.TotalPoint = (string)value; break;
						case "NominalPerPoint": this.str.NominalPerPoint = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EmployeeIncentiveProcessID":

							if (value == null || value is System.Int32)
								this.EmployeeIncentiveProcessID = (System.Int32?)value;
							break;
						case "Nominal":

							if (value == null || value is System.Decimal)
								this.Nominal = (System.Decimal?)value;
							break;
						case "TotalPoint":

							if (value == null || value is System.Decimal)
								this.TotalPoint = (System.Decimal?)value;
							break;
						case "NominalPerPoint":

							if (value == null || value is System.Decimal)
								this.NominalPerPoint = (System.Decimal?)value;
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
		/// Maps to EmployeeIncentiveProcessItem.EmployeeIncentiveProcessID
		/// </summary>
		virtual public System.Int32? EmployeeIncentiveProcessID
		{
			get
			{
				return base.GetSystemInt32(EmployeeIncentiveProcessItemMetadata.ColumnNames.EmployeeIncentiveProcessID);
			}

			set
			{
				base.SetSystemInt32(EmployeeIncentiveProcessItemMetadata.ColumnNames.EmployeeIncentiveProcessID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentiveProcessItem.SRIncentiveServiceUnitGroup
		/// </summary>
		virtual public System.String SRIncentiveServiceUnitGroup
		{
			get
			{
				return base.GetSystemString(EmployeeIncentiveProcessItemMetadata.ColumnNames.SRIncentiveServiceUnitGroup);
			}

			set
			{
				base.SetSystemString(EmployeeIncentiveProcessItemMetadata.ColumnNames.SRIncentiveServiceUnitGroup, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentiveProcessItem.Nominal
		/// </summary>
		virtual public System.Decimal? Nominal
		{
			get
			{
				return base.GetSystemDecimal(EmployeeIncentiveProcessItemMetadata.ColumnNames.Nominal);
			}

			set
			{
				base.SetSystemDecimal(EmployeeIncentiveProcessItemMetadata.ColumnNames.Nominal, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentiveProcessItem.TotalPoint
		/// </summary>
		virtual public System.Decimal? TotalPoint
		{
			get
			{
				return base.GetSystemDecimal(EmployeeIncentiveProcessItemMetadata.ColumnNames.TotalPoint);
			}

			set
			{
				base.SetSystemDecimal(EmployeeIncentiveProcessItemMetadata.ColumnNames.TotalPoint, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentiveProcessItem.NominalPerPoint
		/// </summary>
		virtual public System.Decimal? NominalPerPoint
		{
			get
			{
				return base.GetSystemDecimal(EmployeeIncentiveProcessItemMetadata.ColumnNames.NominalPerPoint);
			}

			set
			{
				base.SetSystemDecimal(EmployeeIncentiveProcessItemMetadata.ColumnNames.NominalPerPoint, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentiveProcessItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeIncentiveProcessItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeIncentiveProcessItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentiveProcessItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeIncentiveProcessItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeIncentiveProcessItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeIncentiveProcessItem entity)
			{
				this.entity = entity;
			}
			public System.String EmployeeIncentiveProcessID
			{
				get
				{
					System.Int32? data = entity.EmployeeIncentiveProcessID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeIncentiveProcessID = null;
					else entity.EmployeeIncentiveProcessID = Convert.ToInt32(value);
				}
			}
			public System.String SRIncentiveServiceUnitGroup
			{
				get
				{
					System.String data = entity.SRIncentiveServiceUnitGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncentiveServiceUnitGroup = null;
					else entity.SRIncentiveServiceUnitGroup = Convert.ToString(value);
				}
			}
			public System.String Nominal
			{
				get
				{
					System.Decimal? data = entity.Nominal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Nominal = null;
					else entity.Nominal = Convert.ToDecimal(value);
				}
			}
			public System.String TotalPoint
			{
				get
				{
					System.Decimal? data = entity.TotalPoint;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalPoint = null;
					else entity.TotalPoint = Convert.ToDecimal(value);
				}
			}
			public System.String NominalPerPoint
			{
				get
				{
					System.Decimal? data = entity.NominalPerPoint;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NominalPerPoint = null;
					else entity.NominalPerPoint = Convert.ToDecimal(value);
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
			private esEmployeeIncentiveProcessItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeIncentiveProcessItemQuery query)
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
				throw new Exception("esEmployeeIncentiveProcessItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeIncentiveProcessItem : esEmployeeIncentiveProcessItem
	{
	}

	[Serializable]
	abstract public class esEmployeeIncentiveProcessItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeIncentiveProcessItemMetadata.Meta();
			}
		}

		public esQueryItem EmployeeIncentiveProcessID
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentiveProcessItemMetadata.ColumnNames.EmployeeIncentiveProcessID, esSystemType.Int32);
			}
		}

		public esQueryItem SRIncentiveServiceUnitGroup
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentiveProcessItemMetadata.ColumnNames.SRIncentiveServiceUnitGroup, esSystemType.String);
			}
		}

		public esQueryItem Nominal
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentiveProcessItemMetadata.ColumnNames.Nominal, esSystemType.Decimal);
			}
		}

		public esQueryItem TotalPoint
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentiveProcessItemMetadata.ColumnNames.TotalPoint, esSystemType.Decimal);
			}
		}

		public esQueryItem NominalPerPoint
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentiveProcessItemMetadata.ColumnNames.NominalPerPoint, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentiveProcessItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentiveProcessItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeIncentiveProcessItemCollection")]
	public partial class EmployeeIncentiveProcessItemCollection : esEmployeeIncentiveProcessItemCollection, IEnumerable<EmployeeIncentiveProcessItem>
	{
		public EmployeeIncentiveProcessItemCollection()
		{

		}

		public static implicit operator List<EmployeeIncentiveProcessItem>(EmployeeIncentiveProcessItemCollection coll)
		{
			List<EmployeeIncentiveProcessItem> list = new List<EmployeeIncentiveProcessItem>();

			foreach (EmployeeIncentiveProcessItem emp in coll)
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
				return EmployeeIncentiveProcessItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeIncentiveProcessItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeIncentiveProcessItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeIncentiveProcessItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeIncentiveProcessItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeIncentiveProcessItemQuery();
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
		public bool Load(EmployeeIncentiveProcessItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeIncentiveProcessItem AddNew()
		{
			EmployeeIncentiveProcessItem entity = base.AddNewEntity() as EmployeeIncentiveProcessItem;

			return entity;
		}
		public EmployeeIncentiveProcessItem FindByPrimaryKey(Int32 employeeIncentiveProcessID, String sRIncentiveServiceUnitGroup)
		{
			return base.FindByPrimaryKey(employeeIncentiveProcessID, sRIncentiveServiceUnitGroup) as EmployeeIncentiveProcessItem;
		}

		#region IEnumerable< EmployeeIncentiveProcessItem> Members

		IEnumerator<EmployeeIncentiveProcessItem> IEnumerable<EmployeeIncentiveProcessItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeIncentiveProcessItem;
			}
		}

		#endregion

		private EmployeeIncentiveProcessItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeIncentiveProcessItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeIncentiveProcessItem ({EmployeeIncentiveProcessID, SRIncentiveServiceUnitGroup})")]
	[Serializable]
	public partial class EmployeeIncentiveProcessItem : esEmployeeIncentiveProcessItem
	{
		public EmployeeIncentiveProcessItem()
		{
		}

		public EmployeeIncentiveProcessItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeIncentiveProcessItemMetadata.Meta();
			}
		}

		override protected esEmployeeIncentiveProcessItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeIncentiveProcessItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeIncentiveProcessItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeIncentiveProcessItemQuery();
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
		public bool Load(EmployeeIncentiveProcessItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeIncentiveProcessItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeIncentiveProcessItemQuery : esEmployeeIncentiveProcessItemQuery
	{
		public EmployeeIncentiveProcessItemQuery()
		{

		}

		public EmployeeIncentiveProcessItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeIncentiveProcessItemQuery";
		}
	}

	[Serializable]
	public partial class EmployeeIncentiveProcessItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeIncentiveProcessItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeIncentiveProcessItemMetadata.ColumnNames.EmployeeIncentiveProcessID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeIncentiveProcessItemMetadata.PropertyNames.EmployeeIncentiveProcessID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentiveProcessItemMetadata.ColumnNames.SRIncentiveServiceUnitGroup, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeIncentiveProcessItemMetadata.PropertyNames.SRIncentiveServiceUnitGroup;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentiveProcessItemMetadata.ColumnNames.Nominal, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeIncentiveProcessItemMetadata.PropertyNames.Nominal;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentiveProcessItemMetadata.ColumnNames.TotalPoint, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeIncentiveProcessItemMetadata.PropertyNames.TotalPoint;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentiveProcessItemMetadata.ColumnNames.NominalPerPoint, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeIncentiveProcessItemMetadata.PropertyNames.NominalPerPoint;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentiveProcessItemMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeIncentiveProcessItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentiveProcessItemMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeIncentiveProcessItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeIncentiveProcessItemMetadata Meta()
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
			public const string EmployeeIncentiveProcessID = "EmployeeIncentiveProcessID";
			public const string SRIncentiveServiceUnitGroup = "SRIncentiveServiceUnitGroup";
			public const string Nominal = "Nominal";
			public const string TotalPoint = "TotalPoint";
			public const string NominalPerPoint = "NominalPerPoint";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EmployeeIncentiveProcessID = "EmployeeIncentiveProcessID";
			public const string SRIncentiveServiceUnitGroup = "SRIncentiveServiceUnitGroup";
			public const string Nominal = "Nominal";
			public const string TotalPoint = "TotalPoint";
			public const string NominalPerPoint = "NominalPerPoint";
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
			lock (typeof(EmployeeIncentiveProcessItemMetadata))
			{
				if (EmployeeIncentiveProcessItemMetadata.mapDelegates == null)
				{
					EmployeeIncentiveProcessItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeIncentiveProcessItemMetadata.meta == null)
				{
					EmployeeIncentiveProcessItemMetadata.meta = new EmployeeIncentiveProcessItemMetadata();
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

				meta.AddTypeMap("EmployeeIncentiveProcessID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRIncentiveServiceUnitGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Nominal", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TotalPoint", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("NominalPerPoint", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeIncentiveProcessItem";
				meta.Destination = "EmployeeIncentiveProcessItem";
				meta.spInsert = "proc_EmployeeIncentiveProcessItemInsert";
				meta.spUpdate = "proc_EmployeeIncentiveProcessItemUpdate";
				meta.spDelete = "proc_EmployeeIncentiveProcessItemDelete";
				meta.spLoadAll = "proc_EmployeeIncentiveProcessItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeIncentiveProcessItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeIncentiveProcessItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
