/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 02/12/20 11:59:53 AM
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
	abstract public class esItemStockOpnamePrevBalanceCollection : esEntityCollectionWAuditLog
	{
		public esItemStockOpnamePrevBalanceCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ItemStockOpnamePrevBalanceCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esItemStockOpnamePrevBalanceQuery query)
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
			this.InitQuery(query as esItemStockOpnamePrevBalanceQuery);
		}
		#endregion
			
		virtual public ItemStockOpnamePrevBalance DetachEntity(ItemStockOpnamePrevBalance entity)
		{
			return base.DetachEntity(entity) as ItemStockOpnamePrevBalance;
		}
		
		virtual public ItemStockOpnamePrevBalance AttachEntity(ItemStockOpnamePrevBalance entity)
		{
			return base.AttachEntity(entity) as ItemStockOpnamePrevBalance;
		}
		
		virtual public void Combine(ItemStockOpnamePrevBalanceCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemStockOpnamePrevBalance this[int index]
		{
			get
			{
				return base[index] as ItemStockOpnamePrevBalance;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemStockOpnamePrevBalance);
		}
	}

	[Serializable]
	abstract public class esItemStockOpnamePrevBalance : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemStockOpnamePrevBalanceQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esItemStockOpnamePrevBalance()
		{
		}
	
		public esItemStockOpnamePrevBalance(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String itemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, itemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, itemID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String transactionNo, String itemID)
		{
			esItemStockOpnamePrevBalanceQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.ItemID == itemID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);
			parms.Add("ItemID",itemID);
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
			if(this.Row == null) this.AddNew();
			
			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if(value == null || value is System.String)
				{				
					// Use the strongly typed property
					switch (name)
					{
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
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
			else if(this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to ItemStockOpnamePrevBalance.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ItemStockOpnamePrevBalanceMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(ItemStockOpnamePrevBalanceMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemStockOpnamePrevBalance.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemStockOpnamePrevBalanceMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ItemStockOpnamePrevBalanceMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ItemStockOpnamePrevBalance.Quantity
		/// </summary>
		virtual public System.Decimal? Quantity
		{
			get
			{
				return base.GetSystemDecimal(ItemStockOpnamePrevBalanceMetadata.ColumnNames.Quantity);
			}
			
			set
			{
				base.SetSystemDecimal(ItemStockOpnamePrevBalanceMetadata.ColumnNames.Quantity, value);
			}
		}
		/// <summary>
		/// Maps to ItemStockOpnamePrevBalance.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(ItemStockOpnamePrevBalanceMetadata.ColumnNames.SRItemUnit);
			}
			
			set
			{
				base.SetSystemString(ItemStockOpnamePrevBalanceMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemStockOpnamePrevBalance.CostPrice
		/// </summary>
		virtual public System.Decimal? CostPrice
		{
			get
			{
				return base.GetSystemDecimal(ItemStockOpnamePrevBalanceMetadata.ColumnNames.CostPrice);
			}
			
			set
			{
				base.SetSystemDecimal(ItemStockOpnamePrevBalanceMetadata.ColumnNames.CostPrice, value);
			}
		}
		/// <summary>
		/// Maps to ItemStockOpnamePrevBalance.QtyAtApprove
		/// </summary>
		virtual public System.Decimal? QtyAtApprove
		{
			get
			{
				return base.GetSystemDecimal(ItemStockOpnamePrevBalanceMetadata.ColumnNames.QtyAtApprove);
			}
			
			set
			{
				base.SetSystemDecimal(ItemStockOpnamePrevBalanceMetadata.ColumnNames.QtyAtApprove, value);
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
		[BrowsableAttribute( false )]		
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
			public esStrings(esItemStockOpnamePrevBalance entity)
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
			private esItemStockOpnamePrevBalance entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemStockOpnamePrevBalanceQuery query)
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
				throw new Exception("esItemStockOpnamePrevBalance can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemStockOpnamePrevBalance : esItemStockOpnamePrevBalance
	{	
	}

	[Serializable]
	abstract public class esItemStockOpnamePrevBalanceQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ItemStockOpnamePrevBalanceMetadata.Meta();
			}
		}	
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ItemStockOpnamePrevBalanceMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemStockOpnamePrevBalanceMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem Quantity
		{
			get
			{
				return new esQueryItem(this, ItemStockOpnamePrevBalanceMetadata.ColumnNames.Quantity, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, ItemStockOpnamePrevBalanceMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		} 
			
		public esQueryItem CostPrice
		{
			get
			{
				return new esQueryItem(this, ItemStockOpnamePrevBalanceMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem QtyAtApprove
		{
			get
			{
				return new esQueryItem(this, ItemStockOpnamePrevBalanceMetadata.ColumnNames.QtyAtApprove, esSystemType.Decimal);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemStockOpnamePrevBalanceCollection")]
	public partial class ItemStockOpnamePrevBalanceCollection : esItemStockOpnamePrevBalanceCollection, IEnumerable< ItemStockOpnamePrevBalance>
	{
		public ItemStockOpnamePrevBalanceCollection()
		{

		}	
		
		public static implicit operator List< ItemStockOpnamePrevBalance>(ItemStockOpnamePrevBalanceCollection coll)
		{
			List< ItemStockOpnamePrevBalance> list = new List< ItemStockOpnamePrevBalance>();
			
			foreach (ItemStockOpnamePrevBalance emp in coll)
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
				return  ItemStockOpnamePrevBalanceMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemStockOpnamePrevBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemStockOpnamePrevBalance(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemStockOpnamePrevBalance();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ItemStockOpnamePrevBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemStockOpnamePrevBalanceQuery();
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
		public bool Load(ItemStockOpnamePrevBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemStockOpnamePrevBalance AddNew()
		{
			ItemStockOpnamePrevBalance entity = base.AddNewEntity() as ItemStockOpnamePrevBalance;
			
			return entity;		
		}
		public ItemStockOpnamePrevBalance FindByPrimaryKey(String transactionNo, String itemID)
		{
			return base.FindByPrimaryKey(transactionNo, itemID) as ItemStockOpnamePrevBalance;
		}

		#region IEnumerable< ItemStockOpnamePrevBalance> Members

		IEnumerator< ItemStockOpnamePrevBalance> IEnumerable< ItemStockOpnamePrevBalance>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemStockOpnamePrevBalance;
			}
		}

		#endregion
		
		private ItemStockOpnamePrevBalanceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemStockOpnamePrevBalance' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemStockOpnamePrevBalance ({TransactionNo, ItemID})")]
	[Serializable]
	public partial class ItemStockOpnamePrevBalance : esItemStockOpnamePrevBalance
	{
		public ItemStockOpnamePrevBalance()
		{
		}	
	
		public ItemStockOpnamePrevBalance(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemStockOpnamePrevBalanceMetadata.Meta();
			}
		}	
	
		override protected esItemStockOpnamePrevBalanceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemStockOpnamePrevBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ItemStockOpnamePrevBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemStockOpnamePrevBalanceQuery();
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
		public bool Load(ItemStockOpnamePrevBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ItemStockOpnamePrevBalanceQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemStockOpnamePrevBalanceQuery : esItemStockOpnamePrevBalanceQuery
	{
		public ItemStockOpnamePrevBalanceQuery()
		{

		}		
		
		public ItemStockOpnamePrevBalanceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ItemStockOpnamePrevBalanceQuery";
        }
	}

	[Serializable]
	public partial class ItemStockOpnamePrevBalanceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemStockOpnamePrevBalanceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ItemStockOpnamePrevBalanceMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemStockOpnamePrevBalanceMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemStockOpnamePrevBalanceMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemStockOpnamePrevBalanceMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemStockOpnamePrevBalanceMetadata.ColumnNames.Quantity, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemStockOpnamePrevBalanceMetadata.PropertyNames.Quantity;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
 			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemStockOpnamePrevBalanceMetadata.ColumnNames.SRItemUnit, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemStockOpnamePrevBalanceMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemStockOpnamePrevBalanceMetadata.ColumnNames.CostPrice, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemStockOpnamePrevBalanceMetadata.PropertyNames.CostPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemStockOpnamePrevBalanceMetadata.ColumnNames.QtyAtApprove, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemStockOpnamePrevBalanceMetadata.PropertyNames.QtyAtApprove;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
 			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ItemStockOpnamePrevBalanceMetadata Meta()
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
			get	{ return base._columns; }
		}
		
		#region ColumnNames
		public class ColumnNames
		{ 
			public const string TransactionNo = "TransactionNo";
			public const string ItemID = "ItemID";
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
			public const string ItemID = "ItemID";
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
			lock (typeof(ItemStockOpnamePrevBalanceMetadata))
			{
				if(ItemStockOpnamePrevBalanceMetadata.mapDelegates == null)
				{
					ItemStockOpnamePrevBalanceMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemStockOpnamePrevBalanceMetadata.meta == null)
				{
					ItemStockOpnamePrevBalanceMetadata.meta = new ItemStockOpnamePrevBalanceMetadata();
				}
				
				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}			

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if(!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();
				
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Quantity", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CostPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QtyAtApprove", new esTypeMap("numeric", "System.Decimal"));
		

				meta.Source = "ItemStockOpnamePrevBalance";
				meta.Destination = "ItemStockOpnamePrevBalance";
				meta.spInsert = "proc_ItemStockOpnamePrevBalanceInsert";				
				meta.spUpdate = "proc_ItemStockOpnamePrevBalanceUpdate";		
				meta.spDelete = "proc_ItemStockOpnamePrevBalanceDelete";
				meta.spLoadAll = "proc_ItemStockOpnamePrevBalanceLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemStockOpnamePrevBalanceLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemStockOpnamePrevBalanceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
