/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/25/2017 4:05:06 PM
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
	abstract public class esJournalTransactionsCollection : esEntityCollectionWAuditLog
	{
		public esJournalTransactionsCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "JournalTransactionsCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esJournalTransactionsQuery query)
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
			this.InitQuery(query as esJournalTransactionsQuery);
		}
		#endregion
			
		virtual public JournalTransactions DetachEntity(JournalTransactions entity)
		{
			return base.DetachEntity(entity) as JournalTransactions;
		}
		
		virtual public JournalTransactions AttachEntity(JournalTransactions entity)
		{
			return base.AttachEntity(entity) as JournalTransactions;
		}
		
		virtual public void Combine(JournalTransactionsCollection collection)
		{
			base.Combine(collection);
		}
		
		new public JournalTransactions this[int index]
		{
			get
			{
				return base[index] as JournalTransactions;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(JournalTransactions);
		}
	}

	[Serializable]
	abstract public class esJournalTransactions : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esJournalTransactionsQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esJournalTransactions()
		{
		}
	
		public esJournalTransactions(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 journalId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(journalId);
			else
				return LoadByPrimaryKeyStoredProcedure(journalId);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 journalId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(journalId);
			else
				return LoadByPrimaryKeyStoredProcedure(journalId);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 journalId)
		{
			esJournalTransactionsQuery query = this.GetDynamicQuery();
			query.Where(query.JournalId==journalId);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 journalId)
		{
			esParameters parms = new esParameters();
			parms.Add("JournalId",journalId);
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
						case "JournalId": this.str.JournalId = (string)value; break;
						case "JournalCode": this.str.JournalCode = (string)value; break;
						case "JournalType": this.str.JournalType = (string)value; break;
						case "TransactionNumber": this.str.TransactionNumber = (string)value; break;
						case "TransactionDate": this.str.TransactionDate = (string)value; break;
						case "Description": this.str.Description = (string)value; break;
						case "IsPosted": this.str.IsPosted = (string)value; break;
						case "DateCreated": this.str.DateCreated = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "CreatedBy": this.str.CreatedBy = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDate": this.str.VoidDate = (string)value; break;
						case "RefferenceNumber": this.str.RefferenceNumber = (string)value; break;
						case "JournalIdRefference": this.str.JournalIdRefference = (string)value; break;
						case "BudgetingCode": this.str.BudgetingCode = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "JournalId":
						
							if (value == null || value is System.Int32)
								this.JournalId = (System.Int32?)value;
							break;
						case "TransactionDate":
						
							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
							break;
						case "IsPosted":
						
							if (value == null || value is System.Boolean)
								this.IsPosted = (System.Boolean?)value;
							break;
						case "DateCreated":
						
							if (value == null || value is System.DateTime)
								this.DateCreated = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDate":
						
							if (value == null || value is System.DateTime)
								this.VoidDate = (System.DateTime?)value;
							break;
						case "JournalIdRefference":
						
							if (value == null || value is System.Int32)
								this.JournalIdRefference = (System.Int32?)value;
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
		/// Maps to JournalTransactions.JournalId
		/// </summary>
		virtual public System.Int32? JournalId
		{
			get
			{
				return base.GetSystemInt32(JournalTransactionsMetadata.ColumnNames.JournalId);
			}
			
			set
			{
				base.SetSystemInt32(JournalTransactionsMetadata.ColumnNames.JournalId, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactions.JournalCode
		/// </summary>
		virtual public System.String JournalCode
		{
			get
			{
				return base.GetSystemString(JournalTransactionsMetadata.ColumnNames.JournalCode);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionsMetadata.ColumnNames.JournalCode, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactions.JournalType
		/// </summary>
		virtual public System.String JournalType
		{
			get
			{
				return base.GetSystemString(JournalTransactionsMetadata.ColumnNames.JournalType);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionsMetadata.ColumnNames.JournalType, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactions.TransactionNumber
		/// </summary>
		virtual public System.String TransactionNumber
		{
			get
			{
				return base.GetSystemString(JournalTransactionsMetadata.ColumnNames.TransactionNumber);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionsMetadata.ColumnNames.TransactionNumber, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactions.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(JournalTransactionsMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(JournalTransactionsMetadata.ColumnNames.TransactionDate, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactions.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(JournalTransactionsMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionsMetadata.ColumnNames.Description, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactions.IsPosted
		/// </summary>
		virtual public System.Boolean? IsPosted
		{
			get
			{
				return base.GetSystemBoolean(JournalTransactionsMetadata.ColumnNames.IsPosted);
			}
			
			set
			{
				base.SetSystemBoolean(JournalTransactionsMetadata.ColumnNames.IsPosted, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactions.DateCreated
		/// </summary>
		virtual public System.DateTime? DateCreated
		{
			get
			{
				return base.GetSystemDateTime(JournalTransactionsMetadata.ColumnNames.DateCreated);
			}
			
			set
			{
				base.SetSystemDateTime(JournalTransactionsMetadata.ColumnNames.DateCreated, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactions.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(JournalTransactionsMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(JournalTransactionsMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactions.CreatedBy
		/// </summary>
		virtual public System.String CreatedBy
		{
			get
			{
				return base.GetSystemString(JournalTransactionsMetadata.ColumnNames.CreatedBy);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionsMetadata.ColumnNames.CreatedBy, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactions.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(JournalTransactionsMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionsMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactions.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(JournalTransactionsMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(JournalTransactionsMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactions.VoidDate
		/// </summary>
		virtual public System.DateTime? VoidDate
		{
			get
			{
				return base.GetSystemDateTime(JournalTransactionsMetadata.ColumnNames.VoidDate);
			}
			
			set
			{
				base.SetSystemDateTime(JournalTransactionsMetadata.ColumnNames.VoidDate, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactions.RefferenceNumber
		/// </summary>
		virtual public System.String RefferenceNumber
		{
			get
			{
				return base.GetSystemString(JournalTransactionsMetadata.ColumnNames.RefferenceNumber);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionsMetadata.ColumnNames.RefferenceNumber, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactions.JournalIdRefference
		/// </summary>
		virtual public System.Int32? JournalIdRefference
		{
			get
			{
				return base.GetSystemInt32(JournalTransactionsMetadata.ColumnNames.JournalIdRefference);
			}
			
			set
			{
				base.SetSystemInt32(JournalTransactionsMetadata.ColumnNames.JournalIdRefference, value);
			}
		}
		/// <summary>
		/// Maps to JournalTransactions.BudgetingCode
		/// </summary>
		virtual public System.String BudgetingCode
		{
			get
			{
				return base.GetSystemString(JournalTransactionsMetadata.ColumnNames.BudgetingCode);
			}
			
			set
			{
				base.SetSystemString(JournalTransactionsMetadata.ColumnNames.BudgetingCode, value);
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
			public esStrings(esJournalTransactions entity)
			{
				this.entity = entity;
			}
			public System.String JournalId
			{
				get
				{
					System.Int32? data = entity.JournalId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalId = null;
					else entity.JournalId = Convert.ToInt32(value);
				}
			}
			public System.String JournalCode
			{
				get
				{
					System.String data = entity.JournalCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalCode = null;
					else entity.JournalCode = Convert.ToString(value);
				}
			}
			public System.String JournalType
			{
				get
				{
					System.String data = entity.JournalType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalType = null;
					else entity.JournalType = Convert.ToString(value);
				}
			}
			public System.String TransactionNumber
			{
				get
				{
					System.String data = entity.TransactionNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNumber = null;
					else entity.TransactionNumber = Convert.ToString(value);
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
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
				}
			}
			public System.String IsPosted
			{
				get
				{
					System.Boolean? data = entity.IsPosted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPosted = null;
					else entity.IsPosted = Convert.ToBoolean(value);
				}
			}
			public System.String DateCreated
			{
				get
				{
					System.DateTime? data = entity.DateCreated;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateCreated = null;
					else entity.DateCreated = Convert.ToDateTime(value);
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
			public System.String CreatedBy
			{
				get
				{
					System.String data = entity.CreatedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedBy = null;
					else entity.CreatedBy = Convert.ToString(value);
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
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
				}
			}
			public System.String VoidDate
			{
				get
				{
					System.DateTime? data = entity.VoidDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDate = null;
					else entity.VoidDate = Convert.ToDateTime(value);
				}
			}
			public System.String RefferenceNumber
			{
				get
				{
					System.String data = entity.RefferenceNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RefferenceNumber = null;
					else entity.RefferenceNumber = Convert.ToString(value);
				}
			}
			public System.String JournalIdRefference
			{
				get
				{
					System.Int32? data = entity.JournalIdRefference;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalIdRefference = null;
					else entity.JournalIdRefference = Convert.ToInt32(value);
				}
			}
			public System.String BudgetingCode
			{
				get
				{
					System.String data = entity.BudgetingCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BudgetingCode = null;
					else entity.BudgetingCode = Convert.ToString(value);
				}
			}
			private esJournalTransactions entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esJournalTransactionsQuery query)
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
				throw new Exception("esJournalTransactions can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class JournalTransactions : esJournalTransactions
	{	
	}

	[Serializable]
	abstract public class esJournalTransactionsQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return JournalTransactionsMetadata.Meta();
			}
		}	
			
		public esQueryItem JournalId
		{
			get
			{
				return new esQueryItem(this, JournalTransactionsMetadata.ColumnNames.JournalId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem JournalCode
		{
			get
			{
				return new esQueryItem(this, JournalTransactionsMetadata.ColumnNames.JournalCode, esSystemType.String);
			}
		} 
			
		public esQueryItem JournalType
		{
			get
			{
				return new esQueryItem(this, JournalTransactionsMetadata.ColumnNames.JournalType, esSystemType.String);
			}
		} 
			
		public esQueryItem TransactionNumber
		{
			get
			{
				return new esQueryItem(this, JournalTransactionsMetadata.ColumnNames.TransactionNumber, esSystemType.String);
			}
		} 
			
		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, JournalTransactionsMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, JournalTransactionsMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
			
		public esQueryItem IsPosted
		{
			get
			{
				return new esQueryItem(this, JournalTransactionsMetadata.ColumnNames.IsPosted, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem DateCreated
		{
			get
			{
				return new esQueryItem(this, JournalTransactionsMetadata.ColumnNames.DateCreated, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, JournalTransactionsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreatedBy
		{
			get
			{
				return new esQueryItem(this, JournalTransactionsMetadata.ColumnNames.CreatedBy, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, JournalTransactionsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, JournalTransactionsMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem VoidDate
		{
			get
			{
				return new esQueryItem(this, JournalTransactionsMetadata.ColumnNames.VoidDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem RefferenceNumber
		{
			get
			{
				return new esQueryItem(this, JournalTransactionsMetadata.ColumnNames.RefferenceNumber, esSystemType.String);
			}
		} 
			
		public esQueryItem JournalIdRefference
		{
			get
			{
				return new esQueryItem(this, JournalTransactionsMetadata.ColumnNames.JournalIdRefference, esSystemType.Int32);
			}
		} 
			
		public esQueryItem BudgetingCode
		{
			get
			{
				return new esQueryItem(this, JournalTransactionsMetadata.ColumnNames.BudgetingCode, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("JournalTransactionsCollection")]
	public partial class JournalTransactionsCollection : esJournalTransactionsCollection, IEnumerable< JournalTransactions>
	{
		public JournalTransactionsCollection()
		{

		}	
		
		public static implicit operator List< JournalTransactions>(JournalTransactionsCollection coll)
		{
			List< JournalTransactions> list = new List< JournalTransactions>();
			
			foreach (JournalTransactions emp in coll)
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
				return  JournalTransactionsMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new JournalTransactionsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new JournalTransactions(row);
		}

		override protected esEntity CreateEntity()
		{
			return new JournalTransactions();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public JournalTransactionsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new JournalTransactionsQuery();
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
		public bool Load(JournalTransactionsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public JournalTransactions AddNew()
		{
			JournalTransactions entity = base.AddNewEntity() as JournalTransactions;
			
			return entity;		
		}
		public JournalTransactions FindByPrimaryKey(Int32 journalId)
		{
			return base.FindByPrimaryKey(journalId) as JournalTransactions;
		}

		#region IEnumerable< JournalTransactions> Members

		IEnumerator< JournalTransactions> IEnumerable< JournalTransactions>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as JournalTransactions;
			}
		}

		#endregion
		
		private JournalTransactionsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'JournalTransactions' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("JournalTransactions ({JournalId})")]
	[Serializable]
	public partial class JournalTransactions : esJournalTransactions
	{
		public JournalTransactions()
		{
		}	
	
		public JournalTransactions(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return JournalTransactionsMetadata.Meta();
			}
		}	
	
		override protected esJournalTransactionsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new JournalTransactionsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public JournalTransactionsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new JournalTransactionsQuery();
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
		public bool Load(JournalTransactionsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private JournalTransactionsQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class JournalTransactionsQuery : esJournalTransactionsQuery
	{
		public JournalTransactionsQuery()
		{

		}		
		
		public JournalTransactionsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "JournalTransactionsQuery";
        }
	}

	[Serializable]
	public partial class JournalTransactionsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected JournalTransactionsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(JournalTransactionsMetadata.ColumnNames.JournalId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = JournalTransactionsMetadata.PropertyNames.JournalId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionsMetadata.ColumnNames.JournalCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionsMetadata.PropertyNames.JournalCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionsMetadata.ColumnNames.JournalType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionsMetadata.PropertyNames.JournalType;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionsMetadata.ColumnNames.TransactionNumber, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionsMetadata.PropertyNames.TransactionNumber;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionsMetadata.ColumnNames.TransactionDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = JournalTransactionsMetadata.PropertyNames.TransactionDate;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionsMetadata.ColumnNames.Description, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionsMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 255;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionsMetadata.ColumnNames.IsPosted, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = JournalTransactionsMetadata.PropertyNames.IsPosted;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionsMetadata.ColumnNames.DateCreated, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = JournalTransactionsMetadata.PropertyNames.DateCreated;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionsMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = JournalTransactionsMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionsMetadata.ColumnNames.CreatedBy, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionsMetadata.PropertyNames.CreatedBy;
			c.CharacterMaxLength = 25;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionsMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 25;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionsMetadata.ColumnNames.IsVoid, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = JournalTransactionsMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionsMetadata.ColumnNames.VoidDate, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = JournalTransactionsMetadata.PropertyNames.VoidDate;
			c.HasDefault = true;
			c.Default = @"('1900-01-01')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionsMetadata.ColumnNames.RefferenceNumber, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionsMetadata.PropertyNames.RefferenceNumber;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionsMetadata.ColumnNames.JournalIdRefference, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = JournalTransactionsMetadata.PropertyNames.JournalIdRefference;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(JournalTransactionsMetadata.ColumnNames.BudgetingCode, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalTransactionsMetadata.PropertyNames.BudgetingCode;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public JournalTransactionsMetadata Meta()
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
			public const string JournalId = "JournalId";
			public const string JournalCode = "JournalCode";
			public const string JournalType = "JournalType";
			public const string TransactionNumber = "TransactionNumber";
			public const string TransactionDate = "TransactionDate";
			public const string Description = "Description";
			public const string IsPosted = "IsPosted";
			public const string DateCreated = "DateCreated";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string CreatedBy = "CreatedBy";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDate = "VoidDate";
			public const string RefferenceNumber = "RefferenceNumber";
			public const string JournalIdRefference = "JournalIdRefference";
			public const string BudgetingCode = "BudgetingCode";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string JournalId = "JournalId";
			public const string JournalCode = "JournalCode";
			public const string JournalType = "JournalType";
			public const string TransactionNumber = "TransactionNumber";
			public const string TransactionDate = "TransactionDate";
			public const string Description = "Description";
			public const string IsPosted = "IsPosted";
			public const string DateCreated = "DateCreated";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string CreatedBy = "CreatedBy";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDate = "VoidDate";
			public const string RefferenceNumber = "RefferenceNumber";
			public const string JournalIdRefference = "JournalIdRefference";
			public const string BudgetingCode = "BudgetingCode";
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
			lock (typeof(JournalTransactionsMetadata))
			{
				if(JournalTransactionsMetadata.mapDelegates == null)
				{
					JournalTransactionsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (JournalTransactionsMetadata.meta == null)
				{
					JournalTransactionsMetadata.meta = new JournalTransactionsMetadata();
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
				
				meta.AddTypeMap("JournalId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("JournalCode", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("JournalType", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("TransactionNumber", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Description", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("IsPosted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DateCreated", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedBy", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RefferenceNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JournalIdRefference", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("BudgetingCode", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "JournalTransactions";
				meta.Destination = "JournalTransactions";
				meta.spInsert = "proc_JournalTransactionsInsert";				
				meta.spUpdate = "proc_JournalTransactionsUpdate";		
				meta.spDelete = "proc_JournalTransactionsDelete";
				meta.spLoadAll = "proc_JournalTransactionsLoadAll";
				meta.spLoadByPrimaryKey = "proc_JournalTransactionsLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private JournalTransactionsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
