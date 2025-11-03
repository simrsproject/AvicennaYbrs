/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/20/2021 7:10:05 PM
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
	abstract public class esInventoryBalanceCollection : esEntityCollectionWAuditLog
	{
		public esInventoryBalanceCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "InventoryBalanceCollection";
		}

		#region Query Logic
		protected void InitQuery(esInventoryBalanceQuery query)
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
			this.InitQuery(query as esInventoryBalanceQuery);
		}
		#endregion

		virtual public InventoryBalance DetachEntity(InventoryBalance entity)
		{
			return base.DetachEntity(entity) as InventoryBalance;
		}

		virtual public InventoryBalance AttachEntity(InventoryBalance entity)
		{
			return base.AttachEntity(entity) as InventoryBalance;
		}

		virtual public void Combine(InventoryBalanceCollection collection)
		{
			base.Combine(collection);
		}

		new public InventoryBalance this[int index]
		{
			get
			{
				return base[index] as InventoryBalance;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(InventoryBalance);
		}
	}

	[Serializable]
	abstract public class esInventoryBalance : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esInventoryBalanceQuery GetDynamicQuery()
		{
			return null;
		}

		public esInventoryBalance()
		{
		}

		public esInventoryBalance(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(DateTime periodDate, String locationID, String itemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(periodDate, locationID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(periodDate, locationID, itemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, DateTime periodDate, String locationID, String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(periodDate, locationID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(periodDate, locationID, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(DateTime periodDate, String locationID, String itemID)
		{
			esInventoryBalanceQuery query = this.GetDynamicQuery();
			query.Where(query.PeriodDate == periodDate, query.LocationID == locationID, query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(DateTime periodDate, String locationID, String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("PeriodDate", periodDate);
			parms.Add("LocationID", locationID);
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
						case "PeriodDate": this.str.PeriodDate = (string)value; break;
						case "LocationID": this.str.LocationID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "BeginningBalance": this.str.BeginningBalance = (string)value; break;
						case "InitialQuantity": this.str.InitialQuantity = (string)value; break;
						case "QuantityIn": this.str.QuantityIn = (string)value; break;
						case "QuantityOut": this.str.QuantityOut = (string)value; break;
						case "BalanceIn": this.str.BalanceIn = (string)value; break;
						case "BalanceOut": this.str.BalanceOut = (string)value; break;
						case "Balance": this.str.Balance = (string)value; break;
						case "CostPrice": this.str.CostPrice = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PeriodDate":

							if (value == null || value is System.DateTime)
								this.PeriodDate = (System.DateTime?)value;
							break;
						case "BeginningBalance":

							if (value == null || value is System.Decimal)
								this.BeginningBalance = (System.Decimal?)value;
							break;
						case "InitialQuantity":

							if (value == null || value is System.Decimal)
								this.InitialQuantity = (System.Decimal?)value;
							break;
						case "QuantityIn":

							if (value == null || value is System.Decimal)
								this.QuantityIn = (System.Decimal?)value;
							break;
						case "QuantityOut":

							if (value == null || value is System.Decimal)
								this.QuantityOut = (System.Decimal?)value;
							break;
						case "BalanceIn":

							if (value == null || value is System.Decimal)
								this.BalanceIn = (System.Decimal?)value;
							break;
						case "BalanceOut":

							if (value == null || value is System.Decimal)
								this.BalanceOut = (System.Decimal?)value;
							break;
						case "Balance":

							if (value == null || value is System.Decimal)
								this.Balance = (System.Decimal?)value;
							break;
						case "CostPrice":

							if (value == null || value is System.Decimal)
								this.CostPrice = (System.Decimal?)value;
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
		/// Maps to InventoryBalance.PeriodDate
		/// </summary>
		virtual public System.DateTime? PeriodDate
		{
			get
			{
				return base.GetSystemDateTime(InventoryBalanceMetadata.ColumnNames.PeriodDate);
			}

			set
			{
				base.SetSystemDateTime(InventoryBalanceMetadata.ColumnNames.PeriodDate, value);
			}
		}
		/// <summary>
		/// Maps to InventoryBalance.LocationID
		/// </summary>
		virtual public System.String LocationID
		{
			get
			{
				return base.GetSystemString(InventoryBalanceMetadata.ColumnNames.LocationID);
			}

			set
			{
				base.SetSystemString(InventoryBalanceMetadata.ColumnNames.LocationID, value);
			}
		}
		/// <summary>
		/// Maps to InventoryBalance.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(InventoryBalanceMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(InventoryBalanceMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to InventoryBalance.BeginningBalance
		/// </summary>
		virtual public System.Decimal? BeginningBalance
		{
			get
			{
				return base.GetSystemDecimal(InventoryBalanceMetadata.ColumnNames.BeginningBalance);
			}

			set
			{
				base.SetSystemDecimal(InventoryBalanceMetadata.ColumnNames.BeginningBalance, value);
			}
		}
		/// <summary>
		/// Maps to InventoryBalance.InitialQuantity
		/// </summary>
		virtual public System.Decimal? InitialQuantity
		{
			get
			{
				return base.GetSystemDecimal(InventoryBalanceMetadata.ColumnNames.InitialQuantity);
			}

			set
			{
				base.SetSystemDecimal(InventoryBalanceMetadata.ColumnNames.InitialQuantity, value);
			}
		}
		/// <summary>
		/// Maps to InventoryBalance.QuantityIn
		/// </summary>
		virtual public System.Decimal? QuantityIn
		{
			get
			{
				return base.GetSystemDecimal(InventoryBalanceMetadata.ColumnNames.QuantityIn);
			}

			set
			{
				base.SetSystemDecimal(InventoryBalanceMetadata.ColumnNames.QuantityIn, value);
			}
		}
		/// <summary>
		/// Maps to InventoryBalance.QuantityOut
		/// </summary>
		virtual public System.Decimal? QuantityOut
		{
			get
			{
				return base.GetSystemDecimal(InventoryBalanceMetadata.ColumnNames.QuantityOut);
			}

			set
			{
				base.SetSystemDecimal(InventoryBalanceMetadata.ColumnNames.QuantityOut, value);
			}
		}
		/// <summary>
		/// Maps to InventoryBalance.BalanceIn
		/// </summary>
		virtual public System.Decimal? BalanceIn
		{
			get
			{
				return base.GetSystemDecimal(InventoryBalanceMetadata.ColumnNames.BalanceIn);
			}

			set
			{
				base.SetSystemDecimal(InventoryBalanceMetadata.ColumnNames.BalanceIn, value);
			}
		}
		/// <summary>
		/// Maps to InventoryBalance.BalanceOut
		/// </summary>
		virtual public System.Decimal? BalanceOut
		{
			get
			{
				return base.GetSystemDecimal(InventoryBalanceMetadata.ColumnNames.BalanceOut);
			}

			set
			{
				base.SetSystemDecimal(InventoryBalanceMetadata.ColumnNames.BalanceOut, value);
			}
		}
		/// <summary>
		/// Maps to InventoryBalance.Balance
		/// </summary>
		virtual public System.Decimal? Balance
		{
			get
			{
				return base.GetSystemDecimal(InventoryBalanceMetadata.ColumnNames.Balance);
			}

			set
			{
				base.SetSystemDecimal(InventoryBalanceMetadata.ColumnNames.Balance, value);
			}
		}
		/// <summary>
		/// Maps to InventoryBalance.CostPrice
		/// </summary>
		virtual public System.Decimal? CostPrice
		{
			get
			{
				return base.GetSystemDecimal(InventoryBalanceMetadata.ColumnNames.CostPrice);
			}

			set
			{
				base.SetSystemDecimal(InventoryBalanceMetadata.ColumnNames.CostPrice, value);
			}
		}
		/// <summary>
		/// Maps to InventoryBalance.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(InventoryBalanceMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(InventoryBalanceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to InventoryBalance.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(InventoryBalanceMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(InventoryBalanceMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esInventoryBalance entity)
			{
				this.entity = entity;
			}
			public System.String PeriodDate
			{
				get
				{
					System.DateTime? data = entity.PeriodDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodDate = null;
					else entity.PeriodDate = Convert.ToDateTime(value);
				}
			}
			public System.String LocationID
			{
				get
				{
					System.String data = entity.LocationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationID = null;
					else entity.LocationID = Convert.ToString(value);
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
			public System.String BeginningBalance
			{
				get
				{
					System.Decimal? data = entity.BeginningBalance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BeginningBalance = null;
					else entity.BeginningBalance = Convert.ToDecimal(value);
				}
			}
			public System.String InitialQuantity
			{
				get
				{
					System.Decimal? data = entity.InitialQuantity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InitialQuantity = null;
					else entity.InitialQuantity = Convert.ToDecimal(value);
				}
			}
			public System.String QuantityIn
			{
				get
				{
					System.Decimal? data = entity.QuantityIn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuantityIn = null;
					else entity.QuantityIn = Convert.ToDecimal(value);
				}
			}
			public System.String QuantityOut
			{
				get
				{
					System.Decimal? data = entity.QuantityOut;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuantityOut = null;
					else entity.QuantityOut = Convert.ToDecimal(value);
				}
			}
			public System.String BalanceIn
			{
				get
				{
					System.Decimal? data = entity.BalanceIn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BalanceIn = null;
					else entity.BalanceIn = Convert.ToDecimal(value);
				}
			}
			public System.String BalanceOut
			{
				get
				{
					System.Decimal? data = entity.BalanceOut;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BalanceOut = null;
					else entity.BalanceOut = Convert.ToDecimal(value);
				}
			}
			public System.String Balance
			{
				get
				{
					System.Decimal? data = entity.Balance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Balance = null;
					else entity.Balance = Convert.ToDecimal(value);
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
			private esInventoryBalance entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esInventoryBalanceQuery query)
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
				throw new Exception("esInventoryBalance can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class InventoryBalance : esInventoryBalance
	{
	}

	[Serializable]
	abstract public class esInventoryBalanceQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return InventoryBalanceMetadata.Meta();
			}
		}

		public esQueryItem PeriodDate
		{
			get
			{
				return new esQueryItem(this, InventoryBalanceMetadata.ColumnNames.PeriodDate, esSystemType.DateTime);
			}
		}

		public esQueryItem LocationID
		{
			get
			{
				return new esQueryItem(this, InventoryBalanceMetadata.ColumnNames.LocationID, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, InventoryBalanceMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem BeginningBalance
		{
			get
			{
				return new esQueryItem(this, InventoryBalanceMetadata.ColumnNames.BeginningBalance, esSystemType.Decimal);
			}
		}

		public esQueryItem InitialQuantity
		{
			get
			{
				return new esQueryItem(this, InventoryBalanceMetadata.ColumnNames.InitialQuantity, esSystemType.Decimal);
			}
		}

		public esQueryItem QuantityIn
		{
			get
			{
				return new esQueryItem(this, InventoryBalanceMetadata.ColumnNames.QuantityIn, esSystemType.Decimal);
			}
		}

		public esQueryItem QuantityOut
		{
			get
			{
				return new esQueryItem(this, InventoryBalanceMetadata.ColumnNames.QuantityOut, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceIn
		{
			get
			{
				return new esQueryItem(this, InventoryBalanceMetadata.ColumnNames.BalanceIn, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceOut
		{
			get
			{
				return new esQueryItem(this, InventoryBalanceMetadata.ColumnNames.BalanceOut, esSystemType.Decimal);
			}
		}

		public esQueryItem Balance
		{
			get
			{
				return new esQueryItem(this, InventoryBalanceMetadata.ColumnNames.Balance, esSystemType.Decimal);
			}
		}

		public esQueryItem CostPrice
		{
			get
			{
				return new esQueryItem(this, InventoryBalanceMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, InventoryBalanceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, InventoryBalanceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("InventoryBalanceCollection")]
	public partial class InventoryBalanceCollection : esInventoryBalanceCollection, IEnumerable<InventoryBalance>
	{
		public InventoryBalanceCollection()
		{

		}

		public static implicit operator List<InventoryBalance>(InventoryBalanceCollection coll)
		{
			List<InventoryBalance> list = new List<InventoryBalance>();

			foreach (InventoryBalance emp in coll)
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
				return InventoryBalanceMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InventoryBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new InventoryBalance(row);
		}

		override protected esEntity CreateEntity()
		{
			return new InventoryBalance();
		}

		#endregion

		[BrowsableAttribute(false)]
		public InventoryBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InventoryBalanceQuery();
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
		public bool Load(InventoryBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public InventoryBalance AddNew()
		{
			InventoryBalance entity = base.AddNewEntity() as InventoryBalance;

			return entity;
		}
		public InventoryBalance FindByPrimaryKey(DateTime periodDate, String locationID, String itemID)
		{
			return base.FindByPrimaryKey(periodDate, locationID, itemID) as InventoryBalance;
		}

		#region IEnumerable< InventoryBalance> Members

		IEnumerator<InventoryBalance> IEnumerable<InventoryBalance>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as InventoryBalance;
			}
		}

		#endregion

		private InventoryBalanceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'InventoryBalance' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("InventoryBalance ({PeriodDate, LocationID, ItemID})")]
	[Serializable]
	public partial class InventoryBalance : esInventoryBalance
	{
		public InventoryBalance()
		{
		}

		public InventoryBalance(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return InventoryBalanceMetadata.Meta();
			}
		}

		override protected esInventoryBalanceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InventoryBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public InventoryBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InventoryBalanceQuery();
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
		public bool Load(InventoryBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private InventoryBalanceQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class InventoryBalanceQuery : esInventoryBalanceQuery
	{
		public InventoryBalanceQuery()
		{

		}

		public InventoryBalanceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "InventoryBalanceQuery";
		}
	}

	[Serializable]
	public partial class InventoryBalanceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected InventoryBalanceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(InventoryBalanceMetadata.ColumnNames.PeriodDate, 0, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InventoryBalanceMetadata.PropertyNames.PeriodDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);

			c = new esColumnMetadata(InventoryBalanceMetadata.ColumnNames.LocationID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = InventoryBalanceMetadata.PropertyNames.LocationID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(InventoryBalanceMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = InventoryBalanceMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(InventoryBalanceMetadata.ColumnNames.BeginningBalance, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InventoryBalanceMetadata.PropertyNames.BeginningBalance;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(InventoryBalanceMetadata.ColumnNames.InitialQuantity, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InventoryBalanceMetadata.PropertyNames.InitialQuantity;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(InventoryBalanceMetadata.ColumnNames.QuantityIn, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InventoryBalanceMetadata.PropertyNames.QuantityIn;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(InventoryBalanceMetadata.ColumnNames.QuantityOut, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InventoryBalanceMetadata.PropertyNames.QuantityOut;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(InventoryBalanceMetadata.ColumnNames.BalanceIn, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InventoryBalanceMetadata.PropertyNames.BalanceIn;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(InventoryBalanceMetadata.ColumnNames.BalanceOut, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InventoryBalanceMetadata.PropertyNames.BalanceOut;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(InventoryBalanceMetadata.ColumnNames.Balance, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InventoryBalanceMetadata.PropertyNames.Balance;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(InventoryBalanceMetadata.ColumnNames.CostPrice, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InventoryBalanceMetadata.PropertyNames.CostPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(InventoryBalanceMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InventoryBalanceMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InventoryBalanceMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = InventoryBalanceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public InventoryBalanceMetadata Meta()
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
			public const string PeriodDate = "PeriodDate";
			public const string LocationID = "LocationID";
			public const string ItemID = "ItemID";
			public const string BeginningBalance = "BeginningBalance";
			public const string InitialQuantity = "InitialQuantity";
			public const string QuantityIn = "QuantityIn";
			public const string QuantityOut = "QuantityOut";
			public const string BalanceIn = "BalanceIn";
			public const string BalanceOut = "BalanceOut";
			public const string Balance = "Balance";
			public const string CostPrice = "CostPrice";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PeriodDate = "PeriodDate";
			public const string LocationID = "LocationID";
			public const string ItemID = "ItemID";
			public const string BeginningBalance = "BeginningBalance";
			public const string InitialQuantity = "InitialQuantity";
			public const string QuantityIn = "QuantityIn";
			public const string QuantityOut = "QuantityOut";
			public const string BalanceIn = "BalanceIn";
			public const string BalanceOut = "BalanceOut";
			public const string Balance = "Balance";
			public const string CostPrice = "CostPrice";
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
			lock (typeof(InventoryBalanceMetadata))
			{
				if (InventoryBalanceMetadata.mapDelegates == null)
				{
					InventoryBalanceMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (InventoryBalanceMetadata.meta == null)
				{
					InventoryBalanceMetadata.meta = new InventoryBalanceMetadata();
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

				meta.AddTypeMap("PeriodDate", new esTypeMap("date", "System.DateTime"));
				meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BeginningBalance", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("InitialQuantity", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QuantityIn", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QuantityOut", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BalanceIn", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BalanceOut", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Balance", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CostPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "InventoryBalance";
				meta.Destination = "InventoryBalance";
				meta.spInsert = "proc_InventoryBalanceInsert";
				meta.spUpdate = "proc_InventoryBalanceUpdate";
				meta.spDelete = "proc_InventoryBalanceDelete";
				meta.spLoadAll = "proc_InventoryBalanceLoadAll";
				meta.spLoadByPrimaryKey = "proc_InventoryBalanceLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private InventoryBalanceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
