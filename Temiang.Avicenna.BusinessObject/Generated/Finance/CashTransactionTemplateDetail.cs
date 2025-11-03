/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/1/2023 11:49:09 AM
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
	abstract public class esCashTransactionTemplateDetailCollection : esEntityCollectionWAuditLog
	{
		public esCashTransactionTemplateDetailCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "CashTransactionTemplateDetailCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esCashTransactionTemplateDetailQuery query)
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
			this.InitQuery(query as esCashTransactionTemplateDetailQuery);
		}
		#endregion
			
		virtual public CashTransactionTemplateDetail DetachEntity(CashTransactionTemplateDetail entity)
		{
			return base.DetachEntity(entity) as CashTransactionTemplateDetail;
		}
		
		virtual public CashTransactionTemplateDetail AttachEntity(CashTransactionTemplateDetail entity)
		{
			return base.AttachEntity(entity) as CashTransactionTemplateDetail;
		}
		
		virtual public void Combine(CashTransactionTemplateDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CashTransactionTemplateDetail this[int index]
		{
			get
			{
				return base[index] as CashTransactionTemplateDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CashTransactionTemplateDetail);
		}
	}

	[Serializable]
	abstract public class esCashTransactionTemplateDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCashTransactionTemplateDetailQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esCashTransactionTemplateDetail()
		{
		}
	
		public esCashTransactionTemplateDetail(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 templateDetailId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(templateDetailId);
			else
				return LoadByPrimaryKeyStoredProcedure(templateDetailId);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 templateDetailId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(templateDetailId);
			else
				return LoadByPrimaryKeyStoredProcedure(templateDetailId);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 templateDetailId)
		{
			esCashTransactionTemplateDetailQuery query = this.GetDynamicQuery();
			query.Where(query.TemplateDetailId==templateDetailId);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 templateDetailId)
		{
			esParameters parms = new esParameters();
			parms.Add("TemplateDetailId",templateDetailId);
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
						case "TemplateDetailId": this.str.TemplateDetailId = (string)value; break;
						case "TemplateId": this.str.TemplateId = (string)value; break;
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;
						case "SubLedgerId": this.str.SubLedgerId = (string)value; break;
						case "AmountVariablePercentage": this.str.AmountVariablePercentage = (string)value; break;
						case "AmountFixed": this.str.AmountFixed = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "TemplateDetailId":
						
							if (value == null || value is System.Int32)
								this.TemplateDetailId = (System.Int32?)value;
							break;
						case "TemplateId":
						
							if (value == null || value is System.Int32)
								this.TemplateId = (System.Int32?)value;
							break;
						case "ChartOfAccountId":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
							break;
						case "SubLedgerId":
						
							if (value == null || value is System.Int32)
								this.SubLedgerId = (System.Int32?)value;
							break;
						case "AmountVariablePercentage":
						
							if (value == null || value is System.Decimal)
								this.AmountVariablePercentage = (System.Decimal?)value;
							break;
						case "AmountFixed":
						
							if (value == null || value is System.Decimal)
								this.AmountFixed = (System.Decimal?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
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
		/// Maps to CashTransactionTemplateDetail.TemplateDetailId
		/// </summary>
		virtual public System.Int32? TemplateDetailId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionTemplateDetailMetadata.ColumnNames.TemplateDetailId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionTemplateDetailMetadata.ColumnNames.TemplateDetailId, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionTemplateDetail.TemplateId
		/// </summary>
		virtual public System.Int32? TemplateId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionTemplateDetailMetadata.ColumnNames.TemplateId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionTemplateDetailMetadata.ColumnNames.TemplateId, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionTemplateDetail.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionTemplateDetailMetadata.ColumnNames.ChartOfAccountId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionTemplateDetailMetadata.ColumnNames.ChartOfAccountId, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionTemplateDetail.SubLedgerId
		/// </summary>
		virtual public System.Int32? SubLedgerId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionTemplateDetailMetadata.ColumnNames.SubLedgerId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionTemplateDetailMetadata.ColumnNames.SubLedgerId, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionTemplateDetail.AmountVariablePercentage
		/// </summary>
		virtual public System.Decimal? AmountVariablePercentage
		{
			get
			{
				return base.GetSystemDecimal(CashTransactionTemplateDetailMetadata.ColumnNames.AmountVariablePercentage);
			}
			
			set
			{
				base.SetSystemDecimal(CashTransactionTemplateDetailMetadata.ColumnNames.AmountVariablePercentage, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionTemplateDetail.AmountFixed
		/// </summary>
		virtual public System.Decimal? AmountFixed
		{
			get
			{
				return base.GetSystemDecimal(CashTransactionTemplateDetailMetadata.ColumnNames.AmountFixed);
			}
			
			set
			{
				base.SetSystemDecimal(CashTransactionTemplateDetailMetadata.ColumnNames.AmountFixed, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionTemplateDetail.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CashTransactionTemplateDetailMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CashTransactionTemplateDetailMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionTemplateDetail.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(CashTransactionTemplateDetailMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(CashTransactionTemplateDetailMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionTemplateDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CashTransactionTemplateDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CashTransactionTemplateDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionTemplateDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CashTransactionTemplateDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(CashTransactionTemplateDetailMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCashTransactionTemplateDetail entity)
			{
				this.entity = entity;
			}
			public System.String TemplateDetailId
			{
				get
				{
					System.Int32? data = entity.TemplateDetailId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemplateDetailId = null;
					else entity.TemplateDetailId = Convert.ToInt32(value);
				}
			}
			public System.String TemplateId
			{
				get
				{
					System.Int32? data = entity.TemplateId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemplateId = null;
					else entity.TemplateId = Convert.ToInt32(value);
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
			public System.String SubLedgerId
			{
				get
				{
					System.Int32? data = entity.SubLedgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubLedgerId = null;
					else entity.SubLedgerId = Convert.ToInt32(value);
				}
			}
			public System.String AmountVariablePercentage
			{
				get
				{
					System.Decimal? data = entity.AmountVariablePercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountVariablePercentage = null;
					else entity.AmountVariablePercentage = Convert.ToDecimal(value);
				}
			}
			public System.String AmountFixed
			{
				get
				{
					System.Decimal? data = entity.AmountFixed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountFixed = null;
					else entity.AmountFixed = Convert.ToDecimal(value);
				}
			}
			public System.String CreateDateTime
			{
				get
				{
					System.DateTime? data = entity.CreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateDateTime = null;
					else entity.CreateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreateByUserID
			{
				get
				{
					System.String data = entity.CreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateByUserID = null;
					else entity.CreateByUserID = Convert.ToString(value);
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
			private esCashTransactionTemplateDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCashTransactionTemplateDetailQuery query)
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
				throw new Exception("esCashTransactionTemplateDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CashTransactionTemplateDetail : esCashTransactionTemplateDetail
	{	
	}

	[Serializable]
	abstract public class esCashTransactionTemplateDetailQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return CashTransactionTemplateDetailMetadata.Meta();
			}
		}	
			
		public esQueryItem TemplateDetailId
		{
			get
			{
				return new esQueryItem(this, CashTransactionTemplateDetailMetadata.ColumnNames.TemplateDetailId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem TemplateId
		{
			get
			{
				return new esQueryItem(this, CashTransactionTemplateDetailMetadata.ColumnNames.TemplateId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, CashTransactionTemplateDetailMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubLedgerId
		{
			get
			{
				return new esQueryItem(this, CashTransactionTemplateDetailMetadata.ColumnNames.SubLedgerId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem AmountVariablePercentage
		{
			get
			{
				return new esQueryItem(this, CashTransactionTemplateDetailMetadata.ColumnNames.AmountVariablePercentage, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem AmountFixed
		{
			get
			{
				return new esQueryItem(this, CashTransactionTemplateDetailMetadata.ColumnNames.AmountFixed, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, CashTransactionTemplateDetailMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, CashTransactionTemplateDetailMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CashTransactionTemplateDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CashTransactionTemplateDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CashTransactionTemplateDetailCollection")]
	public partial class CashTransactionTemplateDetailCollection : esCashTransactionTemplateDetailCollection, IEnumerable< CashTransactionTemplateDetail>
	{
		public CashTransactionTemplateDetailCollection()
		{

		}	
		
		public static implicit operator List< CashTransactionTemplateDetail>(CashTransactionTemplateDetailCollection coll)
		{
			List< CashTransactionTemplateDetail> list = new List< CashTransactionTemplateDetail>();
			
			foreach (CashTransactionTemplateDetail emp in coll)
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
				return  CashTransactionTemplateDetailMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CashTransactionTemplateDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CashTransactionTemplateDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CashTransactionTemplateDetail();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public CashTransactionTemplateDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CashTransactionTemplateDetailQuery();
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
		public bool Load(CashTransactionTemplateDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CashTransactionTemplateDetail AddNew()
		{
			CashTransactionTemplateDetail entity = base.AddNewEntity() as CashTransactionTemplateDetail;
			
			return entity;		
		}
		public CashTransactionTemplateDetail FindByPrimaryKey(Int32 templateDetailId)
		{
			return base.FindByPrimaryKey(templateDetailId) as CashTransactionTemplateDetail;
		}

		#region IEnumerable< CashTransactionTemplateDetail> Members

		IEnumerator< CashTransactionTemplateDetail> IEnumerable< CashTransactionTemplateDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CashTransactionTemplateDetail;
			}
		}

		#endregion
		
		private CashTransactionTemplateDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CashTransactionTemplateDetail' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CashTransactionTemplateDetail ({TemplateDetailId})")]
	[Serializable]
	public partial class CashTransactionTemplateDetail : esCashTransactionTemplateDetail
	{
		public CashTransactionTemplateDetail()
		{
		}	
	
		public CashTransactionTemplateDetail(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CashTransactionTemplateDetailMetadata.Meta();
			}
		}	
	
		override protected esCashTransactionTemplateDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CashTransactionTemplateDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public CashTransactionTemplateDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CashTransactionTemplateDetailQuery();
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
		public bool Load(CashTransactionTemplateDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private CashTransactionTemplateDetailQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CashTransactionTemplateDetailQuery : esCashTransactionTemplateDetailQuery
	{
		public CashTransactionTemplateDetailQuery()
		{

		}		
		
		public CashTransactionTemplateDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "CashTransactionTemplateDetailQuery";
        }
	}

	[Serializable]
	public partial class CashTransactionTemplateDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CashTransactionTemplateDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(CashTransactionTemplateDetailMetadata.ColumnNames.TemplateDetailId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionTemplateDetailMetadata.PropertyNames.TemplateDetailId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionTemplateDetailMetadata.ColumnNames.TemplateId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionTemplateDetailMetadata.PropertyNames.TemplateId;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionTemplateDetailMetadata.ColumnNames.ChartOfAccountId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionTemplateDetailMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionTemplateDetailMetadata.ColumnNames.SubLedgerId, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionTemplateDetailMetadata.PropertyNames.SubLedgerId;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionTemplateDetailMetadata.ColumnNames.AmountVariablePercentage, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CashTransactionTemplateDetailMetadata.PropertyNames.AmountVariablePercentage;
			c.NumericPrecision = 6;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionTemplateDetailMetadata.ColumnNames.AmountFixed, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CashTransactionTemplateDetailMetadata.PropertyNames.AmountFixed;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionTemplateDetailMetadata.ColumnNames.CreateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CashTransactionTemplateDetailMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionTemplateDetailMetadata.ColumnNames.CreateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionTemplateDetailMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionTemplateDetailMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CashTransactionTemplateDetailMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionTemplateDetailMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionTemplateDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public CashTransactionTemplateDetailMetadata Meta()
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
			public const string TemplateDetailId = "TemplateDetailId";
			public const string TemplateId = "TemplateId";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string SubLedgerId = "SubLedgerId";
			public const string AmountVariablePercentage = "AmountVariablePercentage";
			public const string AmountFixed = "AmountFixed";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TemplateDetailId = "TemplateDetailId";
			public const string TemplateId = "TemplateId";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string SubLedgerId = "SubLedgerId";
			public const string AmountVariablePercentage = "AmountVariablePercentage";
			public const string AmountFixed = "AmountFixed";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
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
			lock (typeof(CashTransactionTemplateDetailMetadata))
			{
				if(CashTransactionTemplateDetailMetadata.mapDelegates == null)
				{
					CashTransactionTemplateDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CashTransactionTemplateDetailMetadata.meta == null)
				{
					CashTransactionTemplateDetailMetadata.meta = new CashTransactionTemplateDetailMetadata();
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
				
				meta.AddTypeMap("TemplateDetailId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TemplateId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubLedgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AmountVariablePercentage", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("AmountFixed", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "CashTransactionTemplateDetail";
				meta.Destination = "CashTransactionTemplateDetail";
				meta.spInsert = "proc_CashTransactionTemplateDetailInsert";				
				meta.spUpdate = "proc_CashTransactionTemplateDetailUpdate";		
				meta.spDelete = "proc_CashTransactionTemplateDetailDelete";
				meta.spLoadAll = "proc_CashTransactionTemplateDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_CashTransactionTemplateDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CashTransactionTemplateDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
