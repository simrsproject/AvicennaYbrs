/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/13/2021 6:59:05 PM
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
	abstract public class esLaunderedProcessItemConsumptionCollection : esEntityCollectionWAuditLog
	{
		public esLaunderedProcessItemConsumptionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "LaunderedProcessItemConsumptionCollection";
		}

		#region Query Logic
		protected void InitQuery(esLaunderedProcessItemConsumptionQuery query)
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
			this.InitQuery(query as esLaunderedProcessItemConsumptionQuery);
		}
		#endregion

		virtual public LaunderedProcessItemConsumption DetachEntity(LaunderedProcessItemConsumption entity)
		{
			return base.DetachEntity(entity) as LaunderedProcessItemConsumption;
		}

		virtual public LaunderedProcessItemConsumption AttachEntity(LaunderedProcessItemConsumption entity)
		{
			return base.AttachEntity(entity) as LaunderedProcessItemConsumption;
		}

		virtual public void Combine(LaunderedProcessItemConsumptionCollection collection)
		{
			base.Combine(collection);
		}

		new public LaunderedProcessItemConsumption this[int index]
		{
			get
			{
				return base[index] as LaunderedProcessItemConsumption;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LaunderedProcessItemConsumption);
		}
	}

	[Serializable]
	abstract public class esLaunderedProcessItemConsumption : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLaunderedProcessItemConsumptionQuery GetDynamicQuery()
		{
			return null;
		}

		public esLaunderedProcessItemConsumption()
		{
		}

		public esLaunderedProcessItemConsumption(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String processNo, String itemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(processNo, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(processNo, itemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String processNo, String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(processNo, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(processNo, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(String processNo, String itemID)
		{
			esLaunderedProcessItemConsumptionQuery query = this.GetDynamicQuery();
			query.Where(query.ProcessNo == processNo, query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String processNo, String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("ProcessNo", processNo);
			parms.Add("ItemID", itemID);
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
						case "ItemID": this.str.ItemID = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
						case "CostPrice": this.str.CostPrice = (string)value; break;
						case "Price": this.str.Price = (string)value; break;
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
						case "CostPrice":

							if (value == null || value is System.Decimal)
								this.CostPrice = (System.Decimal?)value;
							break;
						case "Price":

							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
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
		/// Maps to LaunderedProcessItemConsumption.ProcessNo
		/// </summary>
		virtual public System.String ProcessNo
		{
			get
			{
				return base.GetSystemString(LaunderedProcessItemConsumptionMetadata.ColumnNames.ProcessNo);
			}

			set
			{
				base.SetSystemString(LaunderedProcessItemConsumptionMetadata.ColumnNames.ProcessNo, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcessItemConsumption.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(LaunderedProcessItemConsumptionMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(LaunderedProcessItemConsumptionMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcessItemConsumption.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(LaunderedProcessItemConsumptionMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(LaunderedProcessItemConsumptionMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcessItemConsumption.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(LaunderedProcessItemConsumptionMetadata.ColumnNames.SRItemUnit);
			}

			set
			{
				base.SetSystemString(LaunderedProcessItemConsumptionMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcessItemConsumption.CostPrice
		/// </summary>
		virtual public System.Decimal? CostPrice
		{
			get
			{
				return base.GetSystemDecimal(LaunderedProcessItemConsumptionMetadata.ColumnNames.CostPrice);
			}

			set
			{
				base.SetSystemDecimal(LaunderedProcessItemConsumptionMetadata.ColumnNames.CostPrice, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcessItemConsumption.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(LaunderedProcessItemConsumptionMetadata.ColumnNames.Price);
			}

			set
			{
				base.SetSystemDecimal(LaunderedProcessItemConsumptionMetadata.ColumnNames.Price, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcessItemConsumption.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaunderedProcessItemConsumptionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaunderedProcessItemConsumptionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaunderedProcessItemConsumption.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LaunderedProcessItemConsumptionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(LaunderedProcessItemConsumptionMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLaunderedProcessItemConsumption entity)
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
			public System.String CostPrice
			{
				get
				{
					System.Decimal? data = entity.CostPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CostPrice = null;
					else entity.CostPrice = Convert.ToDecimal(value);
				}
			}
			public System.String Price
			{
				get
				{
					System.Decimal? data = entity.Price;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Price = null;
					else entity.Price = Convert.ToDecimal(value);
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
			private esLaunderedProcessItemConsumption entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLaunderedProcessItemConsumptionQuery query)
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
				throw new Exception("esLaunderedProcessItemConsumption can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class LaunderedProcessItemConsumption : esLaunderedProcessItemConsumption
	{
	}

	[Serializable]
	abstract public class esLaunderedProcessItemConsumptionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return LaunderedProcessItemConsumptionMetadata.Meta();
			}
		}

		public esQueryItem ProcessNo
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessItemConsumptionMetadata.ColumnNames.ProcessNo, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessItemConsumptionMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessItemConsumptionMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessItemConsumptionMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem CostPrice
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessItemConsumptionMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessItemConsumptionMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessItemConsumptionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LaunderedProcessItemConsumptionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LaunderedProcessItemConsumptionCollection")]
	public partial class LaunderedProcessItemConsumptionCollection : esLaunderedProcessItemConsumptionCollection, IEnumerable<LaunderedProcessItemConsumption>
	{
		public LaunderedProcessItemConsumptionCollection()
		{

		}

		public static implicit operator List<LaunderedProcessItemConsumption>(LaunderedProcessItemConsumptionCollection coll)
		{
			List<LaunderedProcessItemConsumption> list = new List<LaunderedProcessItemConsumption>();

			foreach (LaunderedProcessItemConsumption emp in coll)
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
				return LaunderedProcessItemConsumptionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaunderedProcessItemConsumptionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LaunderedProcessItemConsumption(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LaunderedProcessItemConsumption();
		}

		#endregion

		[BrowsableAttribute(false)]
		public LaunderedProcessItemConsumptionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaunderedProcessItemConsumptionQuery();
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
		public bool Load(LaunderedProcessItemConsumptionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public LaunderedProcessItemConsumption AddNew()
		{
			LaunderedProcessItemConsumption entity = base.AddNewEntity() as LaunderedProcessItemConsumption;

			return entity;
		}
		public LaunderedProcessItemConsumption FindByPrimaryKey(String processNo, String itemID)
		{
			return base.FindByPrimaryKey(processNo, itemID) as LaunderedProcessItemConsumption;
		}

		#region IEnumerable< LaunderedProcessItemConsumption> Members

		IEnumerator<LaunderedProcessItemConsumption> IEnumerable<LaunderedProcessItemConsumption>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as LaunderedProcessItemConsumption;
			}
		}

		#endregion

		private LaunderedProcessItemConsumptionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LaunderedProcessItemConsumption' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("LaunderedProcessItemConsumption ({ProcessNo, ItemID})")]
	[Serializable]
	public partial class LaunderedProcessItemConsumption : esLaunderedProcessItemConsumption
	{
		public LaunderedProcessItemConsumption()
		{
		}

		public LaunderedProcessItemConsumption(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LaunderedProcessItemConsumptionMetadata.Meta();
			}
		}

		override protected esLaunderedProcessItemConsumptionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaunderedProcessItemConsumptionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public LaunderedProcessItemConsumptionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaunderedProcessItemConsumptionQuery();
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
		public bool Load(LaunderedProcessItemConsumptionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private LaunderedProcessItemConsumptionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class LaunderedProcessItemConsumptionQuery : esLaunderedProcessItemConsumptionQuery
	{
		public LaunderedProcessItemConsumptionQuery()
		{

		}

		public LaunderedProcessItemConsumptionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "LaunderedProcessItemConsumptionQuery";
		}
	}

	[Serializable]
	public partial class LaunderedProcessItemConsumptionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LaunderedProcessItemConsumptionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LaunderedProcessItemConsumptionMetadata.ColumnNames.ProcessNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessItemConsumptionMetadata.PropertyNames.ProcessNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaunderedProcessItemConsumptionMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessItemConsumptionMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(LaunderedProcessItemConsumptionMetadata.ColumnNames.Qty, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LaunderedProcessItemConsumptionMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(LaunderedProcessItemConsumptionMetadata.ColumnNames.SRItemUnit, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessItemConsumptionMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaunderedProcessItemConsumptionMetadata.ColumnNames.CostPrice, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LaunderedProcessItemConsumptionMetadata.PropertyNames.CostPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaunderedProcessItemConsumptionMetadata.ColumnNames.Price, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LaunderedProcessItemConsumptionMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaunderedProcessItemConsumptionMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaunderedProcessItemConsumptionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaunderedProcessItemConsumptionMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = LaunderedProcessItemConsumptionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public LaunderedProcessItemConsumptionMetadata Meta()
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
			public const string ItemID = "ItemID";
			public const string Qty = "Qty";
			public const string SRItemUnit = "SRItemUnit";
			public const string CostPrice = "CostPrice";
			public const string Price = "Price";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ProcessNo = "ProcessNo";
			public const string ItemID = "ItemID";
			public const string Qty = "Qty";
			public const string SRItemUnit = "SRItemUnit";
			public const string CostPrice = "CostPrice";
			public const string Price = "Price";
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
			lock (typeof(LaunderedProcessItemConsumptionMetadata))
			{
				if (LaunderedProcessItemConsumptionMetadata.mapDelegates == null)
				{
					LaunderedProcessItemConsumptionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (LaunderedProcessItemConsumptionMetadata.meta == null)
				{
					LaunderedProcessItemConsumptionMetadata.meta = new LaunderedProcessItemConsumptionMetadata();
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
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CostPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "LaunderedProcessItemConsumption";
				meta.Destination = "LaunderedProcessItemConsumption";
				meta.spInsert = "proc_LaunderedProcessItemConsumptionInsert";
				meta.spUpdate = "proc_LaunderedProcessItemConsumptionUpdate";
				meta.spDelete = "proc_LaunderedProcessItemConsumptionDelete";
				meta.spLoadAll = "proc_LaunderedProcessItemConsumptionLoadAll";
				meta.spLoadByPrimaryKey = "proc_LaunderedProcessItemConsumptionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LaunderedProcessItemConsumptionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
