/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/16/2021 1:52:08 PM
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
	abstract public class esCashTransactionBalanceCollection : esEntityCollectionWAuditLog
	{
		public esCashTransactionBalanceCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "CashTransactionBalanceCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esCashTransactionBalanceQuery query)
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
			this.InitQuery(query as esCashTransactionBalanceQuery);
		}
		#endregion
			
		virtual public CashTransactionBalance DetachEntity(CashTransactionBalance entity)
		{
			return base.DetachEntity(entity) as CashTransactionBalance;
		}
		
		virtual public CashTransactionBalance AttachEntity(CashTransactionBalance entity)
		{
			return base.AttachEntity(entity) as CashTransactionBalance;
		}
		
		virtual public void Combine(CashTransactionBalanceCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CashTransactionBalance this[int index]
		{
			get
			{
				return base[index] as CashTransactionBalance;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CashTransactionBalance);
		}
	}

	[Serializable]
	abstract public class esCashTransactionBalance : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCashTransactionBalanceQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esCashTransactionBalance()
		{
		}
	
		public esCashTransactionBalance(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 txnBalanceId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(txnBalanceId);
			else
				return LoadByPrimaryKeyStoredProcedure(txnBalanceId);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 txnBalanceId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(txnBalanceId);
			else
				return LoadByPrimaryKeyStoredProcedure(txnBalanceId);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 txnBalanceId)
		{
			esCashTransactionBalanceQuery query = this.GetDynamicQuery();
			query.Where(query.TxnBalanceId==txnBalanceId);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 txnBalanceId)
		{
			esParameters parms = new esParameters();
			parms.Add("TxnBalanceId",txnBalanceId);
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
						case "TxnBalanceId": this.str.TxnBalanceId = (string)value; break;
						case "TransactionId": this.str.TransactionId = (string)value; break;
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;
						case "NormalBalance": this.str.NormalBalance = (string)value; break;
						case "Amount": this.str.Amount = (string)value; break;
						case "InitialBalance": this.str.InitialBalance = (string)value; break;
						case "DebitAmount": this.str.DebitAmount = (string)value; break;
						case "CreditAmount": this.str.CreditAmount = (string)value; break;
						case "FinalBalance": this.str.FinalBalance = (string)value; break;
						case "ReconcileID": this.str.ReconcileID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "TxnBalanceId":
						
							if (value == null || value is System.Int32)
								this.TxnBalanceId = (System.Int32?)value;
							break;
						case "TransactionId":
						
							if (value == null || value is System.Int32)
								this.TransactionId = (System.Int32?)value;
							break;
						case "ChartOfAccountId":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
							break;
						case "Amount":
						
							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
							break;
						case "InitialBalance":
						
							if (value == null || value is System.Decimal)
								this.InitialBalance = (System.Decimal?)value;
							break;
						case "DebitAmount":
						
							if (value == null || value is System.Decimal)
								this.DebitAmount = (System.Decimal?)value;
							break;
						case "CreditAmount":
						
							if (value == null || value is System.Decimal)
								this.CreditAmount = (System.Decimal?)value;
							break;
						case "FinalBalance":
						
							if (value == null || value is System.Decimal)
								this.FinalBalance = (System.Decimal?)value;
							break;
						case "ReconcileID":
						
							if (value == null || value is System.Int32)
								this.ReconcileID = (System.Int32?)value;
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
		/// Maps to CashTransactionBalance.TxnBalanceId
		/// </summary>
		virtual public System.Int32? TxnBalanceId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionBalanceMetadata.ColumnNames.TxnBalanceId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionBalanceMetadata.ColumnNames.TxnBalanceId, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionBalance.TransactionId
		/// </summary>
		virtual public System.Int32? TransactionId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionBalanceMetadata.ColumnNames.TransactionId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionBalanceMetadata.ColumnNames.TransactionId, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionBalance.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionBalanceMetadata.ColumnNames.ChartOfAccountId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionBalanceMetadata.ColumnNames.ChartOfAccountId, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionBalance.NormalBalance
		/// </summary>
		virtual public System.String NormalBalance
		{
			get
			{
				return base.GetSystemString(CashTransactionBalanceMetadata.ColumnNames.NormalBalance);
			}
			
			set
			{
				base.SetSystemString(CashTransactionBalanceMetadata.ColumnNames.NormalBalance, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionBalance.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(CashTransactionBalanceMetadata.ColumnNames.Amount);
			}
			
			set
			{
				base.SetSystemDecimal(CashTransactionBalanceMetadata.ColumnNames.Amount, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionBalance.InitialBalance
		/// </summary>
		virtual public System.Decimal? InitialBalance
		{
			get
			{
				return base.GetSystemDecimal(CashTransactionBalanceMetadata.ColumnNames.InitialBalance);
			}
			
			set
			{
				base.SetSystemDecimal(CashTransactionBalanceMetadata.ColumnNames.InitialBalance, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionBalance.DebitAmount
		/// </summary>
		virtual public System.Decimal? DebitAmount
		{
			get
			{
				return base.GetSystemDecimal(CashTransactionBalanceMetadata.ColumnNames.DebitAmount);
			}
			
			set
			{
				base.SetSystemDecimal(CashTransactionBalanceMetadata.ColumnNames.DebitAmount, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionBalance.CreditAmount
		/// </summary>
		virtual public System.Decimal? CreditAmount
		{
			get
			{
				return base.GetSystemDecimal(CashTransactionBalanceMetadata.ColumnNames.CreditAmount);
			}
			
			set
			{
				base.SetSystemDecimal(CashTransactionBalanceMetadata.ColumnNames.CreditAmount, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionBalance.FinalBalance
		/// </summary>
		virtual public System.Decimal? FinalBalance
		{
			get
			{
				return base.GetSystemDecimal(CashTransactionBalanceMetadata.ColumnNames.FinalBalance);
			}
			
			set
			{
				base.SetSystemDecimal(CashTransactionBalanceMetadata.ColumnNames.FinalBalance, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionBalance.ReconcileID
		/// </summary>
		virtual public System.Int32? ReconcileID
		{
			get
			{
				return base.GetSystemInt32(CashTransactionBalanceMetadata.ColumnNames.ReconcileID);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionBalanceMetadata.ColumnNames.ReconcileID, value);
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
			public esStrings(esCashTransactionBalance entity)
			{
				this.entity = entity;
			}
			public System.String TxnBalanceId
			{
				get
				{
					System.Int32? data = entity.TxnBalanceId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TxnBalanceId = null;
					else entity.TxnBalanceId = Convert.ToInt32(value);
				}
			}
			public System.String TransactionId
			{
				get
				{
					System.Int32? data = entity.TransactionId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionId = null;
					else entity.TransactionId = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountId
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountId = null;
					else entity.ChartOfAccountId = Convert.ToInt32(value);
				}
			}
			public System.String NormalBalance
			{
				get
				{
					System.String data = entity.NormalBalance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NormalBalance = null;
					else entity.NormalBalance = Convert.ToString(value);
				}
			}
			public System.String Amount
			{
				get
				{
					System.Decimal? data = entity.Amount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Amount = null;
					else entity.Amount = Convert.ToDecimal(value);
				}
			}
			public System.String InitialBalance
			{
				get
				{
					System.Decimal? data = entity.InitialBalance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InitialBalance = null;
					else entity.InitialBalance = Convert.ToDecimal(value);
				}
			}
			public System.String DebitAmount
			{
				get
				{
					System.Decimal? data = entity.DebitAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DebitAmount = null;
					else entity.DebitAmount = Convert.ToDecimal(value);
				}
			}
			public System.String CreditAmount
			{
				get
				{
					System.Decimal? data = entity.CreditAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreditAmount = null;
					else entity.CreditAmount = Convert.ToDecimal(value);
				}
			}
			public System.String FinalBalance
			{
				get
				{
					System.Decimal? data = entity.FinalBalance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FinalBalance = null;
					else entity.FinalBalance = Convert.ToDecimal(value);
				}
			}
			public System.String ReconcileID
			{
				get
				{
					System.Int32? data = entity.ReconcileID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReconcileID = null;
					else entity.ReconcileID = Convert.ToInt32(value);
				}
			}
			private esCashTransactionBalance entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCashTransactionBalanceQuery query)
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
				throw new Exception("esCashTransactionBalance can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CashTransactionBalance : esCashTransactionBalance
	{	
	}

	[Serializable]
	abstract public class esCashTransactionBalanceQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return CashTransactionBalanceMetadata.Meta();
			}
		}	
			
		public esQueryItem TxnBalanceId
		{
			get
			{
				return new esQueryItem(this, CashTransactionBalanceMetadata.ColumnNames.TxnBalanceId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem TransactionId
		{
			get
			{
				return new esQueryItem(this, CashTransactionBalanceMetadata.ColumnNames.TransactionId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, CashTransactionBalanceMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem NormalBalance
		{
			get
			{
				return new esQueryItem(this, CashTransactionBalanceMetadata.ColumnNames.NormalBalance, esSystemType.String);
			}
		} 
			
		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, CashTransactionBalanceMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem InitialBalance
		{
			get
			{
				return new esQueryItem(this, CashTransactionBalanceMetadata.ColumnNames.InitialBalance, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem DebitAmount
		{
			get
			{
				return new esQueryItem(this, CashTransactionBalanceMetadata.ColumnNames.DebitAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreditAmount
		{
			get
			{
				return new esQueryItem(this, CashTransactionBalanceMetadata.ColumnNames.CreditAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem FinalBalance
		{
			get
			{
				return new esQueryItem(this, CashTransactionBalanceMetadata.ColumnNames.FinalBalance, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem ReconcileID
		{
			get
			{
				return new esQueryItem(this, CashTransactionBalanceMetadata.ColumnNames.ReconcileID, esSystemType.Int32);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CashTransactionBalanceCollection")]
	public partial class CashTransactionBalanceCollection : esCashTransactionBalanceCollection, IEnumerable< CashTransactionBalance>
	{
		public CashTransactionBalanceCollection()
		{

		}	
		
		public static implicit operator List< CashTransactionBalance>(CashTransactionBalanceCollection coll)
		{
			List< CashTransactionBalance> list = new List< CashTransactionBalance>();
			
			foreach (CashTransactionBalance emp in coll)
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
				return  CashTransactionBalanceMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CashTransactionBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CashTransactionBalance(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CashTransactionBalance();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public CashTransactionBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CashTransactionBalanceQuery();
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
		public bool Load(CashTransactionBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CashTransactionBalance AddNew()
		{
			CashTransactionBalance entity = base.AddNewEntity() as CashTransactionBalance;
			
			return entity;		
		}
		public CashTransactionBalance FindByPrimaryKey(Int32 txnBalanceId)
		{
			return base.FindByPrimaryKey(txnBalanceId) as CashTransactionBalance;
		}

		#region IEnumerable< CashTransactionBalance> Members

		IEnumerator< CashTransactionBalance> IEnumerable< CashTransactionBalance>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CashTransactionBalance;
			}
		}

		#endregion
		
		private CashTransactionBalanceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CashTransactionBalance' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CashTransactionBalance ({TxnBalanceId})")]
	[Serializable]
	public partial class CashTransactionBalance : esCashTransactionBalance
	{
		public CashTransactionBalance()
		{
		}	
	
		public CashTransactionBalance(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CashTransactionBalanceMetadata.Meta();
			}
		}	
	
		override protected esCashTransactionBalanceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CashTransactionBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public CashTransactionBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CashTransactionBalanceQuery();
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
		public bool Load(CashTransactionBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private CashTransactionBalanceQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CashTransactionBalanceQuery : esCashTransactionBalanceQuery
	{
		public CashTransactionBalanceQuery()
		{

		}		
		
		public CashTransactionBalanceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "CashTransactionBalanceQuery";
        }
	}

	[Serializable]
	public partial class CashTransactionBalanceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CashTransactionBalanceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(CashTransactionBalanceMetadata.ColumnNames.TxnBalanceId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionBalanceMetadata.PropertyNames.TxnBalanceId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionBalanceMetadata.ColumnNames.TransactionId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionBalanceMetadata.PropertyNames.TransactionId;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionBalanceMetadata.ColumnNames.ChartOfAccountId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionBalanceMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionBalanceMetadata.ColumnNames.NormalBalance, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionBalanceMetadata.PropertyNames.NormalBalance;
			c.CharacterMaxLength = 2;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionBalanceMetadata.ColumnNames.Amount, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CashTransactionBalanceMetadata.PropertyNames.Amount;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionBalanceMetadata.ColumnNames.InitialBalance, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CashTransactionBalanceMetadata.PropertyNames.InitialBalance;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionBalanceMetadata.ColumnNames.DebitAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CashTransactionBalanceMetadata.PropertyNames.DebitAmount;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionBalanceMetadata.ColumnNames.CreditAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CashTransactionBalanceMetadata.PropertyNames.CreditAmount;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionBalanceMetadata.ColumnNames.FinalBalance, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CashTransactionBalanceMetadata.PropertyNames.FinalBalance;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionBalanceMetadata.ColumnNames.ReconcileID, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionBalanceMetadata.PropertyNames.ReconcileID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public CashTransactionBalanceMetadata Meta()
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
			public const string TxnBalanceId = "TxnBalanceId";
			public const string TransactionId = "TransactionId";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string NormalBalance = "NormalBalance";
			public const string Amount = "Amount";
			public const string InitialBalance = "InitialBalance";
			public const string DebitAmount = "DebitAmount";
			public const string CreditAmount = "CreditAmount";
			public const string FinalBalance = "FinalBalance";
			public const string ReconcileID = "ReconcileID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TxnBalanceId = "TxnBalanceId";
			public const string TransactionId = "TransactionId";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string NormalBalance = "NormalBalance";
			public const string Amount = "Amount";
			public const string InitialBalance = "InitialBalance";
			public const string DebitAmount = "DebitAmount";
			public const string CreditAmount = "CreditAmount";
			public const string FinalBalance = "FinalBalance";
			public const string ReconcileID = "ReconcileID";
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
			lock (typeof(CashTransactionBalanceMetadata))
			{
				if(CashTransactionBalanceMetadata.mapDelegates == null)
				{
					CashTransactionBalanceMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CashTransactionBalanceMetadata.meta == null)
				{
					CashTransactionBalanceMetadata.meta = new CashTransactionBalanceMetadata();
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
				
				meta.AddTypeMap("TxnBalanceId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TransactionId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NormalBalance", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Amount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("InitialBalance", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("DebitAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("CreditAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("FinalBalance", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("ReconcileID", new esTypeMap("int", "System.Int32"));
		

				meta.Source = "CashTransactionBalance";
				meta.Destination = "CashTransactionBalance";
				meta.spInsert = "proc_CashTransactionBalanceInsert";				
				meta.spUpdate = "proc_CashTransactionBalanceUpdate";		
				meta.spDelete = "proc_CashTransactionBalanceDelete";
				meta.spLoadAll = "proc_CashTransactionBalanceLoadAll";
				meta.spLoadByPrimaryKey = "proc_CashTransactionBalanceLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CashTransactionBalanceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
