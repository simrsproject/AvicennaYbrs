/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/22/2021 10:14:36 AM
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
	abstract public class esBankReconcileCollection : esEntityCollectionWAuditLog
	{
		public esBankReconcileCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "BankReconcileCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esBankReconcileQuery query)
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
			this.InitQuery(query as esBankReconcileQuery);
		}
		#endregion
			
		virtual public BankReconcile DetachEntity(BankReconcile entity)
		{
			return base.DetachEntity(entity) as BankReconcile;
		}
		
		virtual public BankReconcile AttachEntity(BankReconcile entity)
		{
			return base.AttachEntity(entity) as BankReconcile;
		}
		
		virtual public void Combine(BankReconcileCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BankReconcile this[int index]
		{
			get
			{
				return base[index] as BankReconcile;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BankReconcile);
		}
	}

	[Serializable]
	abstract public class esBankReconcile : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBankReconcileQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esBankReconcile()
		{
		}
	
		public esBankReconcile(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 reconcileID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(reconcileID);
			else
				return LoadByPrimaryKeyStoredProcedure(reconcileID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 reconcileID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(reconcileID);
			else
				return LoadByPrimaryKeyStoredProcedure(reconcileID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 reconcileID)
		{
			esBankReconcileQuery query = this.GetDynamicQuery();
			query.Where(query.ReconcileID==reconcileID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 reconcileID)
		{
			esParameters parms = new esParameters();
			parms.Add("ReconcileID",reconcileID);
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
						case "ReconcileID": this.str.ReconcileID = (string)value; break;
						case "BankID": this.str.BankID = (string)value; break;
						case "DebitCashTransaction": this.str.DebitCashTransaction = (string)value; break;
						case "CreditCashTransaction": this.str.CreditCashTransaction = (string)value; break;
						case "DebitInquiry": this.str.DebitInquiry = (string)value; break;
						case "CreditInquiry": this.str.CreditInquiry = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ReconcileID":
						
							if (value == null || value is System.Int32)
								this.ReconcileID = (System.Int32?)value;
							break;
						case "DebitCashTransaction":
						
							if (value == null || value is System.Decimal)
								this.DebitCashTransaction = (System.Decimal?)value;
							break;
						case "CreditCashTransaction":
						
							if (value == null || value is System.Decimal)
								this.CreditCashTransaction = (System.Decimal?)value;
							break;
						case "DebitInquiry":
						
							if (value == null || value is System.Decimal)
								this.DebitInquiry = (System.Decimal?)value;
							break;
						case "CreditInquiry":
						
							if (value == null || value is System.Decimal)
								this.CreditInquiry = (System.Decimal?)value;
							break;
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to BankReconcile.ReconcileID
		/// </summary>
		virtual public System.Int32? ReconcileID
		{
			get
			{
				return base.GetSystemInt32(BankReconcileMetadata.ColumnNames.ReconcileID);
			}
			
			set
			{
				base.SetSystemInt32(BankReconcileMetadata.ColumnNames.ReconcileID, value);
			}
		}
		/// <summary>
		/// Maps to BankReconcile.BankID
		/// </summary>
		virtual public System.String BankID
		{
			get
			{
				return base.GetSystemString(BankReconcileMetadata.ColumnNames.BankID);
			}
			
			set
			{
				base.SetSystemString(BankReconcileMetadata.ColumnNames.BankID, value);
			}
		}
		/// <summary>
		/// Maps to BankReconcile.DebitCashTransaction
		/// </summary>
		virtual public System.Decimal? DebitCashTransaction
		{
			get
			{
				return base.GetSystemDecimal(BankReconcileMetadata.ColumnNames.DebitCashTransaction);
			}
			
			set
			{
				base.SetSystemDecimal(BankReconcileMetadata.ColumnNames.DebitCashTransaction, value);
			}
		}
		/// <summary>
		/// Maps to BankReconcile.CreditCashTransaction
		/// </summary>
		virtual public System.Decimal? CreditCashTransaction
		{
			get
			{
				return base.GetSystemDecimal(BankReconcileMetadata.ColumnNames.CreditCashTransaction);
			}
			
			set
			{
				base.SetSystemDecimal(BankReconcileMetadata.ColumnNames.CreditCashTransaction, value);
			}
		}
		/// <summary>
		/// Maps to BankReconcile.DebitInquiry
		/// </summary>
		virtual public System.Decimal? DebitInquiry
		{
			get
			{
				return base.GetSystemDecimal(BankReconcileMetadata.ColumnNames.DebitInquiry);
			}
			
			set
			{
				base.SetSystemDecimal(BankReconcileMetadata.ColumnNames.DebitInquiry, value);
			}
		}
		/// <summary>
		/// Maps to BankReconcile.CreditInquiry
		/// </summary>
		virtual public System.Decimal? CreditInquiry
		{
			get
			{
				return base.GetSystemDecimal(BankReconcileMetadata.ColumnNames.CreditInquiry);
			}
			
			set
			{
				base.SetSystemDecimal(BankReconcileMetadata.ColumnNames.CreditInquiry, value);
			}
		}
		/// <summary>
		/// Maps to BankReconcile.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(BankReconcileMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BankReconcileMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BankReconcile.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(BankReconcileMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(BankReconcileMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BankReconcile.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BankReconcileMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BankReconcileMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BankReconcile.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BankReconcileMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BankReconcileMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esBankReconcile entity)
			{
				this.entity = entity;
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
			public System.String BankID
			{
				get
				{
					System.String data = entity.BankID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankID = null;
					else entity.BankID = Convert.ToString(value);
				}
			}
			public System.String DebitCashTransaction
			{
				get
				{
					System.Decimal? data = entity.DebitCashTransaction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DebitCashTransaction = null;
					else entity.DebitCashTransaction = Convert.ToDecimal(value);
				}
			}
			public System.String CreditCashTransaction
			{
				get
				{
					System.Decimal? data = entity.CreditCashTransaction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreditCashTransaction = null;
					else entity.CreditCashTransaction = Convert.ToDecimal(value);
				}
			}
			public System.String DebitInquiry
			{
				get
				{
					System.Decimal? data = entity.DebitInquiry;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DebitInquiry = null;
					else entity.DebitInquiry = Convert.ToDecimal(value);
				}
			}
			public System.String CreditInquiry
			{
				get
				{
					System.Decimal? data = entity.CreditInquiry;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreditInquiry = null;
					else entity.CreditInquiry = Convert.ToDecimal(value);
				}
			}
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
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
			private esBankReconcile entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBankReconcileQuery query)
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
				throw new Exception("esBankReconcile can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class BankReconcile : esBankReconcile
	{	
	}

	[Serializable]
	abstract public class esBankReconcileQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return BankReconcileMetadata.Meta();
			}
		}	
			
		public esQueryItem ReconcileID
		{
			get
			{
				return new esQueryItem(this, BankReconcileMetadata.ColumnNames.ReconcileID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem BankID
		{
			get
			{
				return new esQueryItem(this, BankReconcileMetadata.ColumnNames.BankID, esSystemType.String);
			}
		} 
			
		public esQueryItem DebitCashTransaction
		{
			get
			{
				return new esQueryItem(this, BankReconcileMetadata.ColumnNames.DebitCashTransaction, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreditCashTransaction
		{
			get
			{
				return new esQueryItem(this, BankReconcileMetadata.ColumnNames.CreditCashTransaction, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem DebitInquiry
		{
			get
			{
				return new esQueryItem(this, BankReconcileMetadata.ColumnNames.DebitInquiry, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreditInquiry
		{
			get
			{
				return new esQueryItem(this, BankReconcileMetadata.ColumnNames.CreditInquiry, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, BankReconcileMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, BankReconcileMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BankReconcileMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BankReconcileMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BankReconcileCollection")]
	public partial class BankReconcileCollection : esBankReconcileCollection, IEnumerable< BankReconcile>
	{
		public BankReconcileCollection()
		{

		}	
		
		public static implicit operator List< BankReconcile>(BankReconcileCollection coll)
		{
			List< BankReconcile> list = new List< BankReconcile>();
			
			foreach (BankReconcile emp in coll)
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
				return  BankReconcileMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BankReconcileQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BankReconcile(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BankReconcile();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public BankReconcileQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BankReconcileQuery();
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
		public bool Load(BankReconcileQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public BankReconcile AddNew()
		{
			BankReconcile entity = base.AddNewEntity() as BankReconcile;
			
			return entity;		
		}
		public BankReconcile FindByPrimaryKey(Int32 reconcileID)
		{
			return base.FindByPrimaryKey(reconcileID) as BankReconcile;
		}

		#region IEnumerable< BankReconcile> Members

		IEnumerator< BankReconcile> IEnumerable< BankReconcile>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BankReconcile;
			}
		}

		#endregion
		
		private BankReconcileQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BankReconcile' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("BankReconcile ({ReconcileID})")]
	[Serializable]
	public partial class BankReconcile : esBankReconcile
	{
		public BankReconcile()
		{
		}	
	
		public BankReconcile(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BankReconcileMetadata.Meta();
			}
		}	
	
		override protected esBankReconcileQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BankReconcileQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public BankReconcileQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BankReconcileQuery();
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
		public bool Load(BankReconcileQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private BankReconcileQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BankReconcileQuery : esBankReconcileQuery
	{
		public BankReconcileQuery()
		{

		}		
		
		public BankReconcileQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "BankReconcileQuery";
        }
	}

	[Serializable]
	public partial class BankReconcileMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BankReconcileMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(BankReconcileMetadata.ColumnNames.ReconcileID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BankReconcileMetadata.PropertyNames.ReconcileID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankReconcileMetadata.ColumnNames.BankID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = BankReconcileMetadata.PropertyNames.BankID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankReconcileMetadata.ColumnNames.DebitCashTransaction, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BankReconcileMetadata.PropertyNames.DebitCashTransaction;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankReconcileMetadata.ColumnNames.CreditCashTransaction, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BankReconcileMetadata.PropertyNames.CreditCashTransaction;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankReconcileMetadata.ColumnNames.DebitInquiry, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BankReconcileMetadata.PropertyNames.DebitInquiry;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankReconcileMetadata.ColumnNames.CreditInquiry, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BankReconcileMetadata.PropertyNames.CreditInquiry;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankReconcileMetadata.ColumnNames.CreatedDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BankReconcileMetadata.PropertyNames.CreatedDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankReconcileMetadata.ColumnNames.CreatedByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = BankReconcileMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankReconcileMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BankReconcileMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankReconcileMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = BankReconcileMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public BankReconcileMetadata Meta()
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
			public const string ReconcileID = "ReconcileID";
			public const string BankID = "BankID";
			public const string DebitCashTransaction = "DebitCashTransaction";
			public const string CreditCashTransaction = "CreditCashTransaction";
			public const string DebitInquiry = "DebitInquiry";
			public const string CreditInquiry = "CreditInquiry";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ReconcileID = "ReconcileID";
			public const string BankID = "BankID";
			public const string DebitCashTransaction = "DebitCashTransaction";
			public const string CreditCashTransaction = "CreditCashTransaction";
			public const string DebitInquiry = "DebitInquiry";
			public const string CreditInquiry = "CreditInquiry";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
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
			lock (typeof(BankReconcileMetadata))
			{
				if(BankReconcileMetadata.mapDelegates == null)
				{
					BankReconcileMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BankReconcileMetadata.meta == null)
				{
					BankReconcileMetadata.meta = new BankReconcileMetadata();
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
				
				meta.AddTypeMap("ReconcileID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("BankID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DebitCashTransaction", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreditCashTransaction", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("DebitInquiry", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreditInquiry", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "BankReconcile";
				meta.Destination = "BankReconcile";
				meta.spInsert = "proc_BankReconcileInsert";				
				meta.spUpdate = "proc_BankReconcileUpdate";		
				meta.spDelete = "proc_BankReconcileDelete";
				meta.spLoadAll = "proc_BankReconcileLoadAll";
				meta.spLoadByPrimaryKey = "proc_BankReconcileLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BankReconcileMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
