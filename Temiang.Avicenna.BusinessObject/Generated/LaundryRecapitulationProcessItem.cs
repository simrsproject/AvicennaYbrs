/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/24/2021 11:31:09 PM
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
	abstract public class esLaundryRecapitulationProcessItemCollection : esEntityCollectionWAuditLog
	{
		public esLaundryRecapitulationProcessItemCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "LaundryRecapitulationProcessItemCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esLaundryRecapitulationProcessItemQuery query)
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
			this.InitQuery(query as esLaundryRecapitulationProcessItemQuery);
		}
		#endregion
			
		virtual public LaundryRecapitulationProcessItem DetachEntity(LaundryRecapitulationProcessItem entity)
		{
			return base.DetachEntity(entity) as LaundryRecapitulationProcessItem;
		}
		
		virtual public LaundryRecapitulationProcessItem AttachEntity(LaundryRecapitulationProcessItem entity)
		{
			return base.AttachEntity(entity) as LaundryRecapitulationProcessItem;
		}
		
		virtual public void Combine(LaundryRecapitulationProcessItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public LaundryRecapitulationProcessItem this[int index]
		{
			get
			{
				return base[index] as LaundryRecapitulationProcessItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LaundryRecapitulationProcessItem);
		}
	}

	[Serializable]
	abstract public class esLaundryRecapitulationProcessItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLaundryRecapitulationProcessItemQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esLaundryRecapitulationProcessItem()
		{
		}
	
		public esLaundryRecapitulationProcessItem(DataRow row)
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
			esLaundryRecapitulationProcessItemQuery query = this.GetDynamicQuery();
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
						case "Qty": this.str.Qty = (string)value; break;
						case "QtyRewashing": this.str.QtyRewashing = (string)value; break;
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
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
						case "QtyRewashing":
						
							if (value == null || value is System.Decimal)
								this.QtyRewashing = (System.Decimal?)value;
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
		/// Maps to LaundryRecapitulationProcessItem.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(LaundryRecapitulationProcessItemMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(LaundryRecapitulationProcessItemMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to LaundryRecapitulationProcessItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(LaundryRecapitulationProcessItemMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(LaundryRecapitulationProcessItemMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryRecapitulationProcessItem.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(LaundryRecapitulationProcessItemMetadata.ColumnNames.Qty);
			}
			
			set
			{
				base.SetSystemDecimal(LaundryRecapitulationProcessItemMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to LaundryRecapitulationProcessItem.QtyRewashing
		/// </summary>
		virtual public System.Decimal? QtyRewashing
		{
			get
			{
				return base.GetSystemDecimal(LaundryRecapitulationProcessItemMetadata.ColumnNames.QtyRewashing);
			}
			
			set
			{
				base.SetSystemDecimal(LaundryRecapitulationProcessItemMetadata.ColumnNames.QtyRewashing, value);
			}
		}
		/// <summary>
		/// Maps to LaundryRecapitulationProcessItem.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(LaundryRecapitulationProcessItemMetadata.ColumnNames.SRItemUnit);
			}
			
			set
			{
				base.SetSystemString(LaundryRecapitulationProcessItemMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to LaundryRecapitulationProcessItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundryRecapitulationProcessItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(LaundryRecapitulationProcessItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundryRecapitulationProcessItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LaundryRecapitulationProcessItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(LaundryRecapitulationProcessItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLaundryRecapitulationProcessItem entity)
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
			public System.String QtyRewashing
			{
				get
				{
					System.Decimal? data = entity.QtyRewashing;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyRewashing = null;
					else entity.QtyRewashing = Convert.ToDecimal(value);
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
			private esLaundryRecapitulationProcessItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLaundryRecapitulationProcessItemQuery query)
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
				throw new Exception("esLaundryRecapitulationProcessItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class LaundryRecapitulationProcessItem : esLaundryRecapitulationProcessItem
	{	
	}

	[Serializable]
	abstract public class esLaundryRecapitulationProcessItemQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return LaundryRecapitulationProcessItemMetadata.Meta();
			}
		}	
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, LaundryRecapitulationProcessItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, LaundryRecapitulationProcessItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, LaundryRecapitulationProcessItemMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem QtyRewashing
		{
			get
			{
				return new esQueryItem(this, LaundryRecapitulationProcessItemMetadata.ColumnNames.QtyRewashing, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, LaundryRecapitulationProcessItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LaundryRecapitulationProcessItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LaundryRecapitulationProcessItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LaundryRecapitulationProcessItemCollection")]
	public partial class LaundryRecapitulationProcessItemCollection : esLaundryRecapitulationProcessItemCollection, IEnumerable< LaundryRecapitulationProcessItem>
	{
		public LaundryRecapitulationProcessItemCollection()
		{

		}	
		
		public static implicit operator List< LaundryRecapitulationProcessItem>(LaundryRecapitulationProcessItemCollection coll)
		{
			List< LaundryRecapitulationProcessItem> list = new List< LaundryRecapitulationProcessItem>();
			
			foreach (LaundryRecapitulationProcessItem emp in coll)
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
				return  LaundryRecapitulationProcessItemMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryRecapitulationProcessItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LaundryRecapitulationProcessItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LaundryRecapitulationProcessItem();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public LaundryRecapitulationProcessItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryRecapitulationProcessItemQuery();
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
		public bool Load(LaundryRecapitulationProcessItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public LaundryRecapitulationProcessItem AddNew()
		{
			LaundryRecapitulationProcessItem entity = base.AddNewEntity() as LaundryRecapitulationProcessItem;
			
			return entity;		
		}
		public LaundryRecapitulationProcessItem FindByPrimaryKey(String transactionNo, String itemID)
		{
			return base.FindByPrimaryKey(transactionNo, itemID) as LaundryRecapitulationProcessItem;
		}

		#region IEnumerable< LaundryRecapitulationProcessItem> Members

		IEnumerator< LaundryRecapitulationProcessItem> IEnumerable< LaundryRecapitulationProcessItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as LaundryRecapitulationProcessItem;
			}
		}

		#endregion
		
		private LaundryRecapitulationProcessItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LaundryRecapitulationProcessItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("LaundryRecapitulationProcessItem ({TransactionNo, ItemID})")]
	[Serializable]
	public partial class LaundryRecapitulationProcessItem : esLaundryRecapitulationProcessItem
	{
		public LaundryRecapitulationProcessItem()
		{
		}	
	
		public LaundryRecapitulationProcessItem(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LaundryRecapitulationProcessItemMetadata.Meta();
			}
		}	
	
		override protected esLaundryRecapitulationProcessItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryRecapitulationProcessItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public LaundryRecapitulationProcessItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryRecapitulationProcessItemQuery();
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
		public bool Load(LaundryRecapitulationProcessItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private LaundryRecapitulationProcessItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class LaundryRecapitulationProcessItemQuery : esLaundryRecapitulationProcessItemQuery
	{
		public LaundryRecapitulationProcessItemQuery()
		{

		}		
		
		public LaundryRecapitulationProcessItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "LaundryRecapitulationProcessItemQuery";
        }
	}

	[Serializable]
	public partial class LaundryRecapitulationProcessItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LaundryRecapitulationProcessItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(LaundryRecapitulationProcessItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryRecapitulationProcessItemMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(LaundryRecapitulationProcessItemMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryRecapitulationProcessItemMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(LaundryRecapitulationProcessItemMetadata.ColumnNames.Qty, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LaundryRecapitulationProcessItemMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(LaundryRecapitulationProcessItemMetadata.ColumnNames.QtyRewashing, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LaundryRecapitulationProcessItemMetadata.PropertyNames.QtyRewashing;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(LaundryRecapitulationProcessItemMetadata.ColumnNames.SRItemUnit, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryRecapitulationProcessItemMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(LaundryRecapitulationProcessItemMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryRecapitulationProcessItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(LaundryRecapitulationProcessItemMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryRecapitulationProcessItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public LaundryRecapitulationProcessItemMetadata Meta()
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
			public const string Qty = "Qty";
			public const string QtyRewashing = "QtyRewashing";
			public const string SRItemUnit = "SRItemUnit";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TransactionNo = "TransactionNo";
			public const string ItemID = "ItemID";
			public const string Qty = "Qty";
			public const string QtyRewashing = "QtyRewashing";
			public const string SRItemUnit = "SRItemUnit";
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
			lock (typeof(LaundryRecapitulationProcessItemMetadata))
			{
				if(LaundryRecapitulationProcessItemMetadata.mapDelegates == null)
				{
					LaundryRecapitulationProcessItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (LaundryRecapitulationProcessItemMetadata.meta == null)
				{
					LaundryRecapitulationProcessItemMetadata.meta = new LaundryRecapitulationProcessItemMetadata();
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
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QtyRewashing", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "LaundryRecapitulationProcessItem";
				meta.Destination = "LaundryRecapitulationProcessItem";
				meta.spInsert = "proc_LaundryRecapitulationProcessItemInsert";				
				meta.spUpdate = "proc_LaundryRecapitulationProcessItemUpdate";		
				meta.spDelete = "proc_LaundryRecapitulationProcessItemDelete";
				meta.spLoadAll = "proc_LaundryRecapitulationProcessItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_LaundryRecapitulationProcessItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LaundryRecapitulationProcessItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
