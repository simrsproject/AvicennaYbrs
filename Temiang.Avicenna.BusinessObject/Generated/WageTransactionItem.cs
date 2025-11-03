/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/22/2020 6:56:36 PM
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
	abstract public class esWageTransactionItemCollection : esEntityCollectionWAuditLog
	{
		public esWageTransactionItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "WageTransactionItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esWageTransactionItemQuery query)
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
			this.InitQuery(query as esWageTransactionItemQuery);
		}
		#endregion

		virtual public WageTransactionItem DetachEntity(WageTransactionItem entity)
		{
			return base.DetachEntity(entity) as WageTransactionItem;
		}

		virtual public WageTransactionItem AttachEntity(WageTransactionItem entity)
		{
			return base.AttachEntity(entity) as WageTransactionItem;
		}

		virtual public void Combine(WageTransactionItemCollection collection)
		{
			base.Combine(collection);
		}

		new public WageTransactionItem this[int index]
		{
			get
			{
				return base[index] as WageTransactionItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(WageTransactionItem);
		}
	}

	[Serializable]
	abstract public class esWageTransactionItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esWageTransactionItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esWageTransactionItem()
		{
		}

		public esWageTransactionItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 wageTransactionItemID, Int64 wageTransactionID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(wageTransactionItemID, wageTransactionID);
			else
				return LoadByPrimaryKeyStoredProcedure(wageTransactionItemID, wageTransactionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 wageTransactionItemID, Int64 wageTransactionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(wageTransactionItemID, wageTransactionID);
			else
				return LoadByPrimaryKeyStoredProcedure(wageTransactionItemID, wageTransactionID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 wageTransactionItemID, Int64 wageTransactionID)
		{
			esWageTransactionItemQuery query = this.GetDynamicQuery();
			query.Where(query.WageTransactionItemID == wageTransactionItemID, query.WageTransactionID == wageTransactionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 wageTransactionItemID, Int64 wageTransactionID)
		{
			esParameters parms = new esParameters();
			parms.Add("WageTransactionItemID", wageTransactionItemID);
			parms.Add("WageTransactionID", wageTransactionID);
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
						case "WageTransactionItemID": this.str.WageTransactionItemID = (string)value; break;
						case "WageTransactionID": this.str.WageTransactionID = (string)value; break;
						case "SalaryComponentID": this.str.SalaryComponentID = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "NominalAmount": this.str.NominalAmount = (string)value; break;
						case "SRCurrencyCode": this.str.SRCurrencyCode = (string)value; break;
						case "CurrencyRate": this.str.CurrencyRate = (string)value; break;
						case "CurrencyAmount": this.str.CurrencyAmount = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsModified": this.str.IsModified = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "WageTransactionItemID":

							if (value == null || value is System.Int64)
								this.WageTransactionItemID = (System.Int64?)value;
							break;
						case "WageTransactionID":

							if (value == null || value is System.Int64)
								this.WageTransactionID = (System.Int64?)value;
							break;
						case "SalaryComponentID":

							if (value == null || value is System.Int32)
								this.SalaryComponentID = (System.Int32?)value;
							break;
						case "Qty":

							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
							break;
						case "NominalAmount":

							if (value == null || value is System.Decimal)
								this.NominalAmount = (System.Decimal?)value;
							break;
						case "CurrencyRate":

							if (value == null || value is System.Decimal)
								this.CurrencyRate = (System.Decimal?)value;
							break;
						case "CurrencyAmount":

							if (value == null || value is System.Decimal)
								this.CurrencyAmount = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsModified":

							if (value == null || value is System.Boolean)
								this.IsModified = (System.Boolean?)value;
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
		/// Maps to WageTransactionItem.WageTransactionItemID
		/// </summary>
		virtual public System.Int64? WageTransactionItemID
		{
			get
			{
				return base.GetSystemInt64(WageTransactionItemMetadata.ColumnNames.WageTransactionItemID);
			}

			set
			{
				base.SetSystemInt64(WageTransactionItemMetadata.ColumnNames.WageTransactionItemID, value);
			}
		}
		/// <summary>
		/// Maps to WageTransactionItem.WageTransactionID
		/// </summary>
		virtual public System.Int64? WageTransactionID
		{
			get
			{
				return base.GetSystemInt64(WageTransactionItemMetadata.ColumnNames.WageTransactionID);
			}

			set
			{
				base.SetSystemInt64(WageTransactionItemMetadata.ColumnNames.WageTransactionID, value);
			}
		}
		/// <summary>
		/// Maps to WageTransactionItem.SalaryComponentID
		/// </summary>
		virtual public System.Int32? SalaryComponentID
		{
			get
			{
				return base.GetSystemInt32(WageTransactionItemMetadata.ColumnNames.SalaryComponentID);
			}

			set
			{
				base.SetSystemInt32(WageTransactionItemMetadata.ColumnNames.SalaryComponentID, value);
			}
		}
		/// <summary>
		/// Maps to WageTransactionItem.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(WageTransactionItemMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(WageTransactionItemMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to WageTransactionItem.NominalAmount
		/// </summary>
		virtual public System.Decimal? NominalAmount
		{
			get
			{
				return base.GetSystemDecimal(WageTransactionItemMetadata.ColumnNames.NominalAmount);
			}

			set
			{
				base.SetSystemDecimal(WageTransactionItemMetadata.ColumnNames.NominalAmount, value);
			}
		}
		/// <summary>
		/// Maps to WageTransactionItem.SRCurrencyCode
		/// </summary>
		virtual public System.String SRCurrencyCode
		{
			get
			{
				return base.GetSystemString(WageTransactionItemMetadata.ColumnNames.SRCurrencyCode);
			}

			set
			{
				base.SetSystemString(WageTransactionItemMetadata.ColumnNames.SRCurrencyCode, value);
			}
		}
		/// <summary>
		/// Maps to WageTransactionItem.CurrencyRate
		/// </summary>
		virtual public System.Decimal? CurrencyRate
		{
			get
			{
				return base.GetSystemDecimal(WageTransactionItemMetadata.ColumnNames.CurrencyRate);
			}

			set
			{
				base.SetSystemDecimal(WageTransactionItemMetadata.ColumnNames.CurrencyRate, value);
			}
		}
		/// <summary>
		/// Maps to WageTransactionItem.CurrencyAmount
		/// </summary>
		virtual public System.Decimal? CurrencyAmount
		{
			get
			{
				return base.GetSystemDecimal(WageTransactionItemMetadata.ColumnNames.CurrencyAmount);
			}

			set
			{
				base.SetSystemDecimal(WageTransactionItemMetadata.ColumnNames.CurrencyAmount, value);
			}
		}
		/// <summary>
		/// Maps to WageTransactionItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(WageTransactionItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(WageTransactionItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to WageTransactionItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(WageTransactionItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(WageTransactionItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to WageTransactionItem.IsModified
		/// </summary>
		virtual public System.Boolean? IsModified
		{
			get
			{
				return base.GetSystemBoolean(WageTransactionItemMetadata.ColumnNames.IsModified);
			}

			set
			{
				base.SetSystemBoolean(WageTransactionItemMetadata.ColumnNames.IsModified, value);
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
			public esStrings(esWageTransactionItem entity)
			{
				this.entity = entity;
			}
			public System.String WageTransactionItemID
			{
				get
				{
					System.Int64? data = entity.WageTransactionItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WageTransactionItemID = null;
					else entity.WageTransactionItemID = Convert.ToInt64(value);
				}
			}
			public System.String WageTransactionID
			{
				get
				{
					System.Int64? data = entity.WageTransactionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WageTransactionID = null;
					else entity.WageTransactionID = Convert.ToInt64(value);
				}
			}
			public System.String SalaryComponentID
			{
				get
				{
					System.Int32? data = entity.SalaryComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryComponentID = null;
					else entity.SalaryComponentID = Convert.ToInt32(value);
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
			public System.String NominalAmount
			{
				get
				{
					System.Decimal? data = entity.NominalAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NominalAmount = null;
					else entity.NominalAmount = Convert.ToDecimal(value);
				}
			}
			public System.String SRCurrencyCode
			{
				get
				{
					System.String data = entity.SRCurrencyCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCurrencyCode = null;
					else entity.SRCurrencyCode = Convert.ToString(value);
				}
			}
			public System.String CurrencyRate
			{
				get
				{
					System.Decimal? data = entity.CurrencyRate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CurrencyRate = null;
					else entity.CurrencyRate = Convert.ToDecimal(value);
				}
			}
			public System.String CurrencyAmount
			{
				get
				{
					System.Decimal? data = entity.CurrencyAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CurrencyAmount = null;
					else entity.CurrencyAmount = Convert.ToDecimal(value);
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
			public System.String IsModified
			{
				get
				{
					System.Boolean? data = entity.IsModified;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsModified = null;
					else entity.IsModified = Convert.ToBoolean(value);
				}
			}
			private esWageTransactionItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esWageTransactionItemQuery query)
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
				throw new Exception("esWageTransactionItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class WageTransactionItem : esWageTransactionItem
	{
	}

	[Serializable]
	abstract public class esWageTransactionItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return WageTransactionItemMetadata.Meta();
			}
		}

		public esQueryItem WageTransactionItemID
		{
			get
			{
				return new esQueryItem(this, WageTransactionItemMetadata.ColumnNames.WageTransactionItemID, esSystemType.Int64);
			}
		}

		public esQueryItem WageTransactionID
		{
			get
			{
				return new esQueryItem(this, WageTransactionItemMetadata.ColumnNames.WageTransactionID, esSystemType.Int64);
			}
		}

		public esQueryItem SalaryComponentID
		{
			get
			{
				return new esQueryItem(this, WageTransactionItemMetadata.ColumnNames.SalaryComponentID, esSystemType.Int32);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, WageTransactionItemMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem NominalAmount
		{
			get
			{
				return new esQueryItem(this, WageTransactionItemMetadata.ColumnNames.NominalAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem SRCurrencyCode
		{
			get
			{
				return new esQueryItem(this, WageTransactionItemMetadata.ColumnNames.SRCurrencyCode, esSystemType.String);
			}
		}

		public esQueryItem CurrencyRate
		{
			get
			{
				return new esQueryItem(this, WageTransactionItemMetadata.ColumnNames.CurrencyRate, esSystemType.Decimal);
			}
		}

		public esQueryItem CurrencyAmount
		{
			get
			{
				return new esQueryItem(this, WageTransactionItemMetadata.ColumnNames.CurrencyAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, WageTransactionItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, WageTransactionItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsModified
		{
			get
			{
				return new esQueryItem(this, WageTransactionItemMetadata.ColumnNames.IsModified, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("WageTransactionItemCollection")]
	public partial class WageTransactionItemCollection : esWageTransactionItemCollection, IEnumerable<WageTransactionItem>
	{
		public WageTransactionItemCollection()
		{

		}

		public static implicit operator List<WageTransactionItem>(WageTransactionItemCollection coll)
		{
			List<WageTransactionItem> list = new List<WageTransactionItem>();

			foreach (WageTransactionItem emp in coll)
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
				return WageTransactionItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WageTransactionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new WageTransactionItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new WageTransactionItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public WageTransactionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WageTransactionItemQuery();
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
		public bool Load(WageTransactionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public WageTransactionItem AddNew()
		{
			WageTransactionItem entity = base.AddNewEntity() as WageTransactionItem;

			return entity;
		}
		public WageTransactionItem FindByPrimaryKey(Int64 wageTransactionItemID, Int64 wageTransactionID)
		{
			return base.FindByPrimaryKey(wageTransactionItemID, wageTransactionID) as WageTransactionItem;
		}

		#region IEnumerable< WageTransactionItem> Members

		IEnumerator<WageTransactionItem> IEnumerable<WageTransactionItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as WageTransactionItem;
			}
		}

		#endregion

		private WageTransactionItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'WageTransactionItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("WageTransactionItem ({WageTransactionItemID, WageTransactionID})")]
	[Serializable]
	public partial class WageTransactionItem : esWageTransactionItem
	{
		public WageTransactionItem()
		{
		}

		public WageTransactionItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return WageTransactionItemMetadata.Meta();
			}
		}

		override protected esWageTransactionItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WageTransactionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public WageTransactionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WageTransactionItemQuery();
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
		public bool Load(WageTransactionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private WageTransactionItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class WageTransactionItemQuery : esWageTransactionItemQuery
	{
		public WageTransactionItemQuery()
		{

		}

		public WageTransactionItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "WageTransactionItemQuery";
		}
	}

	[Serializable]
	public partial class WageTransactionItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected WageTransactionItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(WageTransactionItemMetadata.ColumnNames.WageTransactionItemID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = WageTransactionItemMetadata.PropertyNames.WageTransactionItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(WageTransactionItemMetadata.ColumnNames.WageTransactionID, 1, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = WageTransactionItemMetadata.PropertyNames.WageTransactionID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(WageTransactionItemMetadata.ColumnNames.SalaryComponentID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WageTransactionItemMetadata.PropertyNames.SalaryComponentID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(WageTransactionItemMetadata.ColumnNames.Qty, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = WageTransactionItemMetadata.PropertyNames.Qty;
			c.NumericPrecision = 18;
			c.NumericScale = 4;
			_columns.Add(c);

			c = new esColumnMetadata(WageTransactionItemMetadata.ColumnNames.NominalAmount, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = WageTransactionItemMetadata.PropertyNames.NominalAmount;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
			_columns.Add(c);

			c = new esColumnMetadata(WageTransactionItemMetadata.ColumnNames.SRCurrencyCode, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = WageTransactionItemMetadata.PropertyNames.SRCurrencyCode;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(WageTransactionItemMetadata.ColumnNames.CurrencyRate, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = WageTransactionItemMetadata.PropertyNames.CurrencyRate;
			c.NumericPrecision = 18;
			c.NumericScale = 4;
			_columns.Add(c);

			c = new esColumnMetadata(WageTransactionItemMetadata.ColumnNames.CurrencyAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = WageTransactionItemMetadata.PropertyNames.CurrencyAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 4;
			_columns.Add(c);

			c = new esColumnMetadata(WageTransactionItemMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WageTransactionItemMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(WageTransactionItemMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = WageTransactionItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(WageTransactionItemMetadata.ColumnNames.IsModified, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = WageTransactionItemMetadata.PropertyNames.IsModified;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public WageTransactionItemMetadata Meta()
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
			public const string WageTransactionItemID = "WageTransactionItemID";
			public const string WageTransactionID = "WageTransactionID";
			public const string SalaryComponentID = "SalaryComponentID";
			public const string Qty = "Qty";
			public const string NominalAmount = "NominalAmount";
			public const string SRCurrencyCode = "SRCurrencyCode";
			public const string CurrencyRate = "CurrencyRate";
			public const string CurrencyAmount = "CurrencyAmount";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsModified = "IsModified";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string WageTransactionItemID = "WageTransactionItemID";
			public const string WageTransactionID = "WageTransactionID";
			public const string SalaryComponentID = "SalaryComponentID";
			public const string Qty = "Qty";
			public const string NominalAmount = "NominalAmount";
			public const string SRCurrencyCode = "SRCurrencyCode";
			public const string CurrencyRate = "CurrencyRate";
			public const string CurrencyAmount = "CurrencyAmount";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsModified = "IsModified";
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
			lock (typeof(WageTransactionItemMetadata))
			{
				if (WageTransactionItemMetadata.mapDelegates == null)
				{
					WageTransactionItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (WageTransactionItemMetadata.meta == null)
				{
					WageTransactionItemMetadata.meta = new WageTransactionItemMetadata();
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

				meta.AddTypeMap("WageTransactionItemID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("WageTransactionID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("SalaryComponentID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("NominalAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("SRCurrencyCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CurrencyRate", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CurrencyAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsModified", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "WageTransactionItem";
				meta.Destination = "WageTransactionItem";
				meta.spInsert = "proc_WageTransactionItemInsert";
				meta.spUpdate = "proc_WageTransactionItemUpdate";
				meta.spDelete = "proc_WageTransactionItemDelete";
				meta.spLoadAll = "proc_WageTransactionItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_WageTransactionItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private WageTransactionItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
