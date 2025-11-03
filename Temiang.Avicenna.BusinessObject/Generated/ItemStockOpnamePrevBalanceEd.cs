/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/7/2023 3:45:13 PM
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
	abstract public class esItemStockOpnamePrevBalanceEdCollection : esEntityCollectionWAuditLog
	{
		public esItemStockOpnamePrevBalanceEdCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemStockOpnamePrevBalanceEdCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemStockOpnamePrevBalanceEdQuery query)
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
			this.InitQuery(query as esItemStockOpnamePrevBalanceEdQuery);
		}
		#endregion

		virtual public ItemStockOpnamePrevBalanceEd DetachEntity(ItemStockOpnamePrevBalanceEd entity)
		{
			return base.DetachEntity(entity) as ItemStockOpnamePrevBalanceEd;
		}

		virtual public ItemStockOpnamePrevBalanceEd AttachEntity(ItemStockOpnamePrevBalanceEd entity)
		{
			return base.AttachEntity(entity) as ItemStockOpnamePrevBalanceEd;
		}

		virtual public void Combine(ItemStockOpnamePrevBalanceEdCollection collection)
		{
			base.Combine(collection);
		}

		new public ItemStockOpnamePrevBalanceEd this[int index]
		{
			get
			{
				return base[index] as ItemStockOpnamePrevBalanceEd;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemStockOpnamePrevBalanceEd);
		}
	}

	[Serializable]
	abstract public class esItemStockOpnamePrevBalanceEd : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemStockOpnamePrevBalanceEdQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemStockOpnamePrevBalanceEd()
		{
		}

		public esItemStockOpnamePrevBalanceEd(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sequenceNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sequenceNo)
		{
			esItemStockOpnamePrevBalanceEdQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("SequenceNo", sequenceNo);
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
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ExpiredDate": this.str.ExpiredDate = (string)value; break;
						case "BatchNumber": this.str.BatchNumber = (string)value; break;
						case "Quantity": this.str.Quantity = (string)value; break;
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
						case "CostPrice": this.str.CostPrice = (string)value; break;
						case "QtyAtApprove": this.str.QtyAtApprove = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ExpiredDate":

							if (value == null || value is System.DateTime)
								this.ExpiredDate = (System.DateTime?)value;
							break;
						case "Quantity":

							if (value == null || value is System.Decimal)
								this.Quantity = (System.Decimal?)value;
							break;
						case "CostPrice":

							if (value == null || value is System.Decimal)
								this.CostPrice = (System.Decimal?)value;
							break;
						case "QtyAtApprove":

							if (value == null || value is System.Decimal)
								this.QtyAtApprove = (System.Decimal?)value;
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
		/// Maps to ItemStockOpnamePrevBalanceEd.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemStockOpnamePrevBalanceEd.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.SequenceNo);
			}

			set
			{
				base.SetSystemString(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemStockOpnamePrevBalanceEd.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ItemStockOpnamePrevBalanceEd.ExpiredDate
		/// </summary>
		virtual public System.DateTime? ExpiredDate
		{
			get
			{
				return base.GetSystemDateTime(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.ExpiredDate);
			}

			set
			{
				base.SetSystemDateTime(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.ExpiredDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemStockOpnamePrevBalanceEd.BatchNumber
		/// </summary>
		virtual public System.String BatchNumber
		{
			get
			{
				return base.GetSystemString(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.BatchNumber);
			}

			set
			{
				base.SetSystemString(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.BatchNumber, value);
			}
		}
		/// <summary>
		/// Maps to ItemStockOpnamePrevBalanceEd.Quantity
		/// </summary>
		virtual public System.Decimal? Quantity
		{
			get
			{
				return base.GetSystemDecimal(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.Quantity);
			}

			set
			{
				base.SetSystemDecimal(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.Quantity, value);
			}
		}
		/// <summary>
		/// Maps to ItemStockOpnamePrevBalanceEd.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.SRItemUnit);
			}

			set
			{
				base.SetSystemString(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemStockOpnamePrevBalanceEd.CostPrice
		/// </summary>
		virtual public System.Decimal? CostPrice
		{
			get
			{
				return base.GetSystemDecimal(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.CostPrice);
			}

			set
			{
				base.SetSystemDecimal(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.CostPrice, value);
			}
		}
		/// <summary>
		/// Maps to ItemStockOpnamePrevBalanceEd.QtyAtApprove
		/// </summary>
		virtual public System.Decimal? QtyAtApprove
		{
			get
			{
				return base.GetSystemDecimal(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.QtyAtApprove);
			}

			set
			{
				base.SetSystemDecimal(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.QtyAtApprove, value);
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
			public esStrings(esItemStockOpnamePrevBalanceEd entity)
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
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
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
			public System.String ExpiredDate
			{
				get
				{
					System.DateTime? data = entity.ExpiredDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExpiredDate = null;
					else entity.ExpiredDate = Convert.ToDateTime(value);
				}
			}
			public System.String BatchNumber
			{
				get
				{
					System.String data = entity.BatchNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BatchNumber = null;
					else entity.BatchNumber = Convert.ToString(value);
				}
			}
			public System.String Quantity
			{
				get
				{
					System.Decimal? data = entity.Quantity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Quantity = null;
					else entity.Quantity = Convert.ToDecimal(value);
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
			public System.String QtyAtApprove
			{
				get
				{
					System.Decimal? data = entity.QtyAtApprove;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyAtApprove = null;
					else entity.QtyAtApprove = Convert.ToDecimal(value);
				}
			}
			private esItemStockOpnamePrevBalanceEd entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemStockOpnamePrevBalanceEdQuery query)
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
				throw new Exception("esItemStockOpnamePrevBalanceEd can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemStockOpnamePrevBalanceEd : esItemStockOpnamePrevBalanceEd
	{
	}

	[Serializable]
	abstract public class esItemStockOpnamePrevBalanceEdQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemStockOpnamePrevBalanceEdMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ExpiredDate
		{
			get
			{
				return new esQueryItem(this, ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.ExpiredDate, esSystemType.DateTime);
			}
		}

		public esQueryItem BatchNumber
		{
			get
			{
				return new esQueryItem(this, ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.BatchNumber, esSystemType.String);
			}
		}

		public esQueryItem Quantity
		{
			get
			{
				return new esQueryItem(this, ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.Quantity, esSystemType.Decimal);
			}
		}

		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem CostPrice
		{
			get
			{
				return new esQueryItem(this, ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyAtApprove
		{
			get
			{
				return new esQueryItem(this, ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.QtyAtApprove, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemStockOpnamePrevBalanceEdCollection")]
	public partial class ItemStockOpnamePrevBalanceEdCollection : esItemStockOpnamePrevBalanceEdCollection, IEnumerable<ItemStockOpnamePrevBalanceEd>
	{
		public ItemStockOpnamePrevBalanceEdCollection()
		{

		}

		public static implicit operator List<ItemStockOpnamePrevBalanceEd>(ItemStockOpnamePrevBalanceEdCollection coll)
		{
			List<ItemStockOpnamePrevBalanceEd> list = new List<ItemStockOpnamePrevBalanceEd>();

			foreach (ItemStockOpnamePrevBalanceEd emp in coll)
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
				return ItemStockOpnamePrevBalanceEdMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemStockOpnamePrevBalanceEdQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemStockOpnamePrevBalanceEd(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemStockOpnamePrevBalanceEd();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemStockOpnamePrevBalanceEdQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemStockOpnamePrevBalanceEdQuery();
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
		public bool Load(ItemStockOpnamePrevBalanceEdQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemStockOpnamePrevBalanceEd AddNew()
		{
			ItemStockOpnamePrevBalanceEd entity = base.AddNewEntity() as ItemStockOpnamePrevBalanceEd;

			return entity;
		}
		public ItemStockOpnamePrevBalanceEd FindByPrimaryKey(String transactionNo, String sequenceNo)
		{
			return base.FindByPrimaryKey(transactionNo, sequenceNo) as ItemStockOpnamePrevBalanceEd;
		}

		#region IEnumerable< ItemStockOpnamePrevBalanceEd> Members

		IEnumerator<ItemStockOpnamePrevBalanceEd> IEnumerable<ItemStockOpnamePrevBalanceEd>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemStockOpnamePrevBalanceEd;
			}
		}

		#endregion

		private ItemStockOpnamePrevBalanceEdQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemStockOpnamePrevBalanceEd' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemStockOpnamePrevBalanceEd ({TransactionNo, SequenceNo})")]
	[Serializable]
	public partial class ItemStockOpnamePrevBalanceEd : esItemStockOpnamePrevBalanceEd
	{
		public ItemStockOpnamePrevBalanceEd()
		{
		}

		public ItemStockOpnamePrevBalanceEd(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemStockOpnamePrevBalanceEdMetadata.Meta();
			}
		}

		override protected esItemStockOpnamePrevBalanceEdQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemStockOpnamePrevBalanceEdQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemStockOpnamePrevBalanceEdQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemStockOpnamePrevBalanceEdQuery();
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
		public bool Load(ItemStockOpnamePrevBalanceEdQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemStockOpnamePrevBalanceEdQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemStockOpnamePrevBalanceEdQuery : esItemStockOpnamePrevBalanceEdQuery
	{
		public ItemStockOpnamePrevBalanceEdQuery()
		{

		}

		public ItemStockOpnamePrevBalanceEdQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemStockOpnamePrevBalanceEdQuery";
		}
	}

	[Serializable]
	public partial class ItemStockOpnamePrevBalanceEdMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemStockOpnamePrevBalanceEdMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemStockOpnamePrevBalanceEdMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemStockOpnamePrevBalanceEdMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemStockOpnamePrevBalanceEdMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.ExpiredDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemStockOpnamePrevBalanceEdMetadata.PropertyNames.ExpiredDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.BatchNumber, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemStockOpnamePrevBalanceEdMetadata.PropertyNames.BatchNumber;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.Quantity, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemStockOpnamePrevBalanceEdMetadata.PropertyNames.Quantity;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.SRItemUnit, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemStockOpnamePrevBalanceEdMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.CostPrice, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemStockOpnamePrevBalanceEdMetadata.PropertyNames.CostPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemStockOpnamePrevBalanceEdMetadata.ColumnNames.QtyAtApprove, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemStockOpnamePrevBalanceEdMetadata.PropertyNames.QtyAtApprove;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);


		}
		#endregion

		static public ItemStockOpnamePrevBalanceEdMetadata Meta()
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
			public const string SequenceNo = "SequenceNo";
			public const string ItemID = "ItemID";
			public const string ExpiredDate = "ExpiredDate";
			public const string BatchNumber = "BatchNumber";
			public const string Quantity = "Quantity";
			public const string SRItemUnit = "SRItemUnit";
			public const string CostPrice = "CostPrice";
			public const string QtyAtApprove = "QtyAtApprove";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string ItemID = "ItemID";
			public const string ExpiredDate = "ExpiredDate";
			public const string BatchNumber = "BatchNumber";
			public const string Quantity = "Quantity";
			public const string SRItemUnit = "SRItemUnit";
			public const string CostPrice = "CostPrice";
			public const string QtyAtApprove = "QtyAtApprove";
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
			lock (typeof(ItemStockOpnamePrevBalanceEdMetadata))
			{
				if (ItemStockOpnamePrevBalanceEdMetadata.mapDelegates == null)
				{
					ItemStockOpnamePrevBalanceEdMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemStockOpnamePrevBalanceEdMetadata.meta == null)
				{
					ItemStockOpnamePrevBalanceEdMetadata.meta = new ItemStockOpnamePrevBalanceEdMetadata();
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
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExpiredDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("BatchNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Quantity", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CostPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QtyAtApprove", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "ItemStockOpnamePrevBalanceEd";
				meta.Destination = "ItemStockOpnamePrevBalanceEd";
				meta.spInsert = "proc_ItemStockOpnamePrevBalanceEdInsert";
				meta.spUpdate = "proc_ItemStockOpnamePrevBalanceEdUpdate";
				meta.spDelete = "proc_ItemStockOpnamePrevBalanceEdDelete";
				meta.spLoadAll = "proc_ItemStockOpnamePrevBalanceEdLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemStockOpnamePrevBalanceEdLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemStockOpnamePrevBalanceEdMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
