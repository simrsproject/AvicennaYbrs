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
	abstract public class esBankInquiryCollection : esEntityCollectionWAuditLog
	{
		public esBankInquiryCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "BankInquiryCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esBankInquiryQuery query)
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
			this.InitQuery(query as esBankInquiryQuery);
		}
		#endregion
			
		virtual public BankInquiry DetachEntity(BankInquiry entity)
		{
			return base.DetachEntity(entity) as BankInquiry;
		}
		
		virtual public BankInquiry AttachEntity(BankInquiry entity)
		{
			return base.AttachEntity(entity) as BankInquiry;
		}
		
		virtual public void Combine(BankInquiryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BankInquiry this[int index]
		{
			get
			{
				return base[index] as BankInquiry;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BankInquiry);
		}
	}

	[Serializable]
	abstract public class esBankInquiry : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBankInquiryQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esBankInquiry()
		{
		}
	
		public esBankInquiry(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 inquiryID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(inquiryID);
			else
				return LoadByPrimaryKeyStoredProcedure(inquiryID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 inquiryID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(inquiryID);
			else
				return LoadByPrimaryKeyStoredProcedure(inquiryID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 inquiryID)
		{
			esBankInquiryQuery query = this.GetDynamicQuery();
			query.Where(query.InquiryID==inquiryID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 inquiryID)
		{
			esParameters parms = new esParameters();
			parms.Add("InquiryID",inquiryID);
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
						case "InquiryID": this.str.InquiryID = (string)value; break;
						case "BankID": this.str.BankID = (string)value; break;
						case "TransactionDate": this.str.TransactionDate = (string)value; break;
						case "Debit": this.str.Debit = (string)value; break;
						case "Credit": this.str.Credit = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "FileName": this.str.FileName = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "InquiryID":
						
							if (value == null || value is System.Int32)
								this.InquiryID = (System.Int32?)value;
							break;
						case "TransactionDate":
						
							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
							break;
						case "Debit":
						
							if (value == null || value is System.Decimal)
								this.Debit = (System.Decimal?)value;
							break;
						case "Credit":
						
							if (value == null || value is System.Decimal)
								this.Credit = (System.Decimal?)value;
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
		/// Maps to BankInquiry.InquiryID
		/// </summary>
		virtual public System.Int32? InquiryID
		{
			get
			{
				return base.GetSystemInt32(BankInquiryMetadata.ColumnNames.InquiryID);
			}
			
			set
			{
				base.SetSystemInt32(BankInquiryMetadata.ColumnNames.InquiryID, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiry.BankID
		/// </summary>
		virtual public System.String BankID
		{
			get
			{
				return base.GetSystemString(BankInquiryMetadata.ColumnNames.BankID);
			}
			
			set
			{
				base.SetSystemString(BankInquiryMetadata.ColumnNames.BankID, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiry.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(BankInquiryMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(BankInquiryMetadata.ColumnNames.TransactionDate, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiry.Debit
		/// </summary>
		virtual public System.Decimal? Debit
		{
			get
			{
				return base.GetSystemDecimal(BankInquiryMetadata.ColumnNames.Debit);
			}
			
			set
			{
				base.SetSystemDecimal(BankInquiryMetadata.ColumnNames.Debit, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiry.Credit
		/// </summary>
		virtual public System.Decimal? Credit
		{
			get
			{
				return base.GetSystemDecimal(BankInquiryMetadata.ColumnNames.Credit);
			}
			
			set
			{
				base.SetSystemDecimal(BankInquiryMetadata.ColumnNames.Credit, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiry.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(BankInquiryMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BankInquiryMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiry.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(BankInquiryMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(BankInquiryMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiry.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BankInquiryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BankInquiryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiry.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BankInquiryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BankInquiryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiry.FileName
		/// </summary>
		virtual public System.String FileName
		{
			get
			{
				return base.GetSystemString(BankInquiryMetadata.ColumnNames.FileName);
			}
			
			set
			{
				base.SetSystemString(BankInquiryMetadata.ColumnNames.FileName, value);
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
			public esStrings(esBankInquiry entity)
			{
				this.entity = entity;
			}
			public System.String InquiryID
			{
				get
				{
					System.Int32? data = entity.InquiryID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InquiryID = null;
					else entity.InquiryID = Convert.ToInt32(value);
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
			public System.String TransactionDate
			{
				get
				{
					System.DateTime? data = entity.TransactionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionDate = null;
					else entity.TransactionDate = Convert.ToDateTime(value);
				}
			}
			public System.String Debit
			{
				get
				{
					System.Decimal? data = entity.Debit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Debit = null;
					else entity.Debit = Convert.ToDecimal(value);
				}
			}
			public System.String Credit
			{
				get
				{
					System.Decimal? data = entity.Credit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Credit = null;
					else entity.Credit = Convert.ToDecimal(value);
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
			public System.String FileName
			{
				get
				{
					System.String data = entity.FileName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FileName = null;
					else entity.FileName = Convert.ToString(value);
				}
			}
			private esBankInquiry entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBankInquiryQuery query)
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
				throw new Exception("esBankInquiry can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class BankInquiry : esBankInquiry
	{	
	}

	[Serializable]
	abstract public class esBankInquiryQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return BankInquiryMetadata.Meta();
			}
		}	
			
		public esQueryItem InquiryID
		{
			get
			{
				return new esQueryItem(this, BankInquiryMetadata.ColumnNames.InquiryID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem BankID
		{
			get
			{
				return new esQueryItem(this, BankInquiryMetadata.ColumnNames.BankID, esSystemType.String);
			}
		} 
			
		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, BankInquiryMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem Debit
		{
			get
			{
				return new esQueryItem(this, BankInquiryMetadata.ColumnNames.Debit, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Credit
		{
			get
			{
				return new esQueryItem(this, BankInquiryMetadata.ColumnNames.Credit, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, BankInquiryMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, BankInquiryMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BankInquiryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BankInquiryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem FileName
		{
			get
			{
				return new esQueryItem(this, BankInquiryMetadata.ColumnNames.FileName, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BankInquiryCollection")]
	public partial class BankInquiryCollection : esBankInquiryCollection, IEnumerable< BankInquiry>
	{
		public BankInquiryCollection()
		{

		}	
		
		public static implicit operator List< BankInquiry>(BankInquiryCollection coll)
		{
			List< BankInquiry> list = new List< BankInquiry>();
			
			foreach (BankInquiry emp in coll)
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
				return  BankInquiryMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BankInquiryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BankInquiry(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BankInquiry();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public BankInquiryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BankInquiryQuery();
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
		public bool Load(BankInquiryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public BankInquiry AddNew()
		{
			BankInquiry entity = base.AddNewEntity() as BankInquiry;
			
			return entity;		
		}
		public BankInquiry FindByPrimaryKey(Int32 inquiryID)
		{
			return base.FindByPrimaryKey(inquiryID) as BankInquiry;
		}

		#region IEnumerable< BankInquiry> Members

		IEnumerator< BankInquiry> IEnumerable< BankInquiry>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BankInquiry;
			}
		}

		#endregion
		
		private BankInquiryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BankInquiry' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("BankInquiry ({InquiryID})")]
	[Serializable]
	public partial class BankInquiry : esBankInquiry
	{
		public BankInquiry()
		{
		}	
	
		public BankInquiry(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BankInquiryMetadata.Meta();
			}
		}	
	
		override protected esBankInquiryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BankInquiryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public BankInquiryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BankInquiryQuery();
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
		public bool Load(BankInquiryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private BankInquiryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BankInquiryQuery : esBankInquiryQuery
	{
		public BankInquiryQuery()
		{

		}		
		
		public BankInquiryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "BankInquiryQuery";
        }
	}

	[Serializable]
	public partial class BankInquiryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BankInquiryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(BankInquiryMetadata.ColumnNames.InquiryID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BankInquiryMetadata.PropertyNames.InquiryID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryMetadata.ColumnNames.BankID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = BankInquiryMetadata.PropertyNames.BankID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryMetadata.ColumnNames.TransactionDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BankInquiryMetadata.PropertyNames.TransactionDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryMetadata.ColumnNames.Debit, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BankInquiryMetadata.PropertyNames.Debit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryMetadata.ColumnNames.Credit, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BankInquiryMetadata.PropertyNames.Credit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryMetadata.ColumnNames.CreatedDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BankInquiryMetadata.PropertyNames.CreatedDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryMetadata.ColumnNames.CreatedByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = BankInquiryMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BankInquiryMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = BankInquiryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryMetadata.ColumnNames.FileName, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = BankInquiryMetadata.PropertyNames.FileName;
			c.CharacterMaxLength = 150;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public BankInquiryMetadata Meta()
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
			public const string InquiryID = "InquiryID";
			public const string BankID = "BankID";
			public const string TransactionDate = "TransactionDate";
			public const string Debit = "Debit";
			public const string Credit = "Credit";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string FileName = "FileName";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string InquiryID = "InquiryID";
			public const string BankID = "BankID";
			public const string TransactionDate = "TransactionDate";
			public const string Debit = "Debit";
			public const string Credit = "Credit";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string FileName = "FileName";
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
			lock (typeof(BankInquiryMetadata))
			{
				if(BankInquiryMetadata.mapDelegates == null)
				{
					BankInquiryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BankInquiryMetadata.meta == null)
				{
					BankInquiryMetadata.meta = new BankInquiryMetadata();
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
				
				meta.AddTypeMap("InquiryID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("BankID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Debit", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Credit", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FileName", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "BankInquiry";
				meta.Destination = "BankInquiry";
				meta.spInsert = "proc_BankInquiryInsert";				
				meta.spUpdate = "proc_BankInquiryUpdate";		
				meta.spDelete = "proc_BankInquiryDelete";
				meta.spLoadAll = "proc_BankInquiryLoadAll";
				meta.spLoadByPrimaryKey = "proc_BankInquiryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BankInquiryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
