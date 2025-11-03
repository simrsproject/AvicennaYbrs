/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/5/2022 1:01:41 PM
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
	abstract public class esServiceFeeRemunRsucdrInvoicesCollection : esEntityCollectionWAuditLog
	{
		public esServiceFeeRemunRsucdrInvoicesCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ServiceFeeRemunRsucdrInvoicesCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esServiceFeeRemunRsucdrInvoicesQuery query)
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
			this.InitQuery(query as esServiceFeeRemunRsucdrInvoicesQuery);
		}
		#endregion
			
		virtual public ServiceFeeRemunRsucdrInvoices DetachEntity(ServiceFeeRemunRsucdrInvoices entity)
		{
			return base.DetachEntity(entity) as ServiceFeeRemunRsucdrInvoices;
		}
		
		virtual public ServiceFeeRemunRsucdrInvoices AttachEntity(ServiceFeeRemunRsucdrInvoices entity)
		{
			return base.AttachEntity(entity) as ServiceFeeRemunRsucdrInvoices;
		}
		
		virtual public void Combine(ServiceFeeRemunRsucdrInvoicesCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ServiceFeeRemunRsucdrInvoices this[int index]
		{
			get
			{
				return base[index] as ServiceFeeRemunRsucdrInvoices;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceFeeRemunRsucdrInvoices);
		}
	}

	[Serializable]
	abstract public class esServiceFeeRemunRsucdrInvoices : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceFeeRemunRsucdrInvoicesQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esServiceFeeRemunRsucdrInvoices()
		{
		}
	
		public esServiceFeeRemunRsucdrInvoices(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 remunID, String invoiceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(remunID, invoiceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(remunID, invoiceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 remunID, String invoiceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(remunID, invoiceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(remunID, invoiceNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 remunID, String invoiceNo)
		{
			esServiceFeeRemunRsucdrInvoicesQuery query = this.GetDynamicQuery();
			query.Where(query.RemunID==remunID, query.InvoiceNo==invoiceNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 remunID, String invoiceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RemunID",remunID);
			parms.Add("InvoiceNo",invoiceNo);
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
						case "RemunID": this.str.RemunID = (string)value; break;
						case "InvoiceNo": this.str.InvoiceNo = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RemunID":
						
							if (value == null || value is System.Int32)
								this.RemunID = (System.Int32?)value;
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
		/// Maps to ServiceFeeRemunRsucdrInvoices.RemunID
		/// </summary>
		virtual public System.Int32? RemunID
		{
			get
			{
				return base.GetSystemInt32(ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.RemunID);
			}
			
			set
			{
				base.SetSystemInt32(ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.RemunID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdrInvoices.InvoiceNo
		/// </summary>
		virtual public System.String InvoiceNo
		{
			get
			{
				return base.GetSystemString(ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.InvoiceNo);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.InvoiceNo, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdrInvoices.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdrInvoices.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdrInvoices.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdrInvoices.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esServiceFeeRemunRsucdrInvoices entity)
			{
				this.entity = entity;
			}
			public System.String RemunID
			{
				get
				{
					System.Int32? data = entity.RemunID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RemunID = null;
					else entity.RemunID = Convert.ToInt32(value);
				}
			}
			public System.String InvoiceNo
			{
				get
				{
					System.String data = entity.InvoiceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoiceNo = null;
					else entity.InvoiceNo = Convert.ToString(value);
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
			private esServiceFeeRemunRsucdrInvoices entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceFeeRemunRsucdrInvoicesQuery query)
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
				throw new Exception("esServiceFeeRemunRsucdrInvoices can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ServiceFeeRemunRsucdrInvoices : esServiceFeeRemunRsucdrInvoices
	{	
	}

	[Serializable]
	abstract public class esServiceFeeRemunRsucdrInvoicesQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ServiceFeeRemunRsucdrInvoicesMetadata.Meta();
			}
		}	
			
		public esQueryItem RemunID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.RemunID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem InvoiceNo
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.InvoiceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceFeeRemunRsucdrInvoicesCollection")]
	public partial class ServiceFeeRemunRsucdrInvoicesCollection : esServiceFeeRemunRsucdrInvoicesCollection, IEnumerable< ServiceFeeRemunRsucdrInvoices>
	{
		public ServiceFeeRemunRsucdrInvoicesCollection()
		{

		}	
		
		public static implicit operator List< ServiceFeeRemunRsucdrInvoices>(ServiceFeeRemunRsucdrInvoicesCollection coll)
		{
			List< ServiceFeeRemunRsucdrInvoices> list = new List< ServiceFeeRemunRsucdrInvoices>();
			
			foreach (ServiceFeeRemunRsucdrInvoices emp in coll)
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
				return  ServiceFeeRemunRsucdrInvoicesMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceFeeRemunRsucdrInvoicesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceFeeRemunRsucdrInvoices(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceFeeRemunRsucdrInvoices();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ServiceFeeRemunRsucdrInvoicesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceFeeRemunRsucdrInvoicesQuery();
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
		public bool Load(ServiceFeeRemunRsucdrInvoicesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ServiceFeeRemunRsucdrInvoices AddNew()
		{
			ServiceFeeRemunRsucdrInvoices entity = base.AddNewEntity() as ServiceFeeRemunRsucdrInvoices;
			
			return entity;		
		}
		public ServiceFeeRemunRsucdrInvoices FindByPrimaryKey(Int32 remunID, String invoiceNo)
		{
			return base.FindByPrimaryKey(remunID, invoiceNo) as ServiceFeeRemunRsucdrInvoices;
		}

		#region IEnumerable< ServiceFeeRemunRsucdrInvoices> Members

		IEnumerator< ServiceFeeRemunRsucdrInvoices> IEnumerable< ServiceFeeRemunRsucdrInvoices>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ServiceFeeRemunRsucdrInvoices;
			}
		}

		#endregion
		
		private ServiceFeeRemunRsucdrInvoicesQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceFeeRemunRsucdrInvoices' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ServiceFeeRemunRsucdrInvoices ({RemunID, InvoiceNo})")]
	[Serializable]
	public partial class ServiceFeeRemunRsucdrInvoices : esServiceFeeRemunRsucdrInvoices
	{
		public ServiceFeeRemunRsucdrInvoices()
		{
		}	
	
		public ServiceFeeRemunRsucdrInvoices(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceFeeRemunRsucdrInvoicesMetadata.Meta();
			}
		}	
	
		override protected esServiceFeeRemunRsucdrInvoicesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceFeeRemunRsucdrInvoicesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ServiceFeeRemunRsucdrInvoicesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceFeeRemunRsucdrInvoicesQuery();
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
		public bool Load(ServiceFeeRemunRsucdrInvoicesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ServiceFeeRemunRsucdrInvoicesQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ServiceFeeRemunRsucdrInvoicesQuery : esServiceFeeRemunRsucdrInvoicesQuery
	{
		public ServiceFeeRemunRsucdrInvoicesQuery()
		{

		}		
		
		public ServiceFeeRemunRsucdrInvoicesQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ServiceFeeRemunRsucdrInvoicesQuery";
        }
	}

	[Serializable]
	public partial class ServiceFeeRemunRsucdrInvoicesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceFeeRemunRsucdrInvoicesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.RemunID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceFeeRemunRsucdrInvoicesMetadata.PropertyNames.RemunID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.InvoiceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeRemunRsucdrInvoicesMetadata.PropertyNames.InvoiceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.CreateByUserID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeRemunRsucdrInvoicesMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.CreateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceFeeRemunRsucdrInvoicesMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeRemunRsucdrInvoicesMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrInvoicesMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceFeeRemunRsucdrInvoicesMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ServiceFeeRemunRsucdrInvoicesMetadata Meta()
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
			public const string RemunID = "RemunID";
			public const string InvoiceNo = "InvoiceNo";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RemunID = "RemunID";
			public const string InvoiceNo = "InvoiceNo";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(ServiceFeeRemunRsucdrInvoicesMetadata))
			{
				if(ServiceFeeRemunRsucdrInvoicesMetadata.mapDelegates == null)
				{
					ServiceFeeRemunRsucdrInvoicesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ServiceFeeRemunRsucdrInvoicesMetadata.meta == null)
				{
					ServiceFeeRemunRsucdrInvoicesMetadata.meta = new ServiceFeeRemunRsucdrInvoicesMetadata();
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
				
				meta.AddTypeMap("RemunID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("InvoiceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "ServiceFeeRemunRsucdrInvoices";
				meta.Destination = "ServiceFeeRemunRsucdrInvoices";
				meta.spInsert = "proc_ServiceFeeRemunRsucdrInvoicesInsert";				
				meta.spUpdate = "proc_ServiceFeeRemunRsucdrInvoicesUpdate";		
				meta.spDelete = "proc_ServiceFeeRemunRsucdrInvoicesDelete";
				meta.spLoadAll = "proc_ServiceFeeRemunRsucdrInvoicesLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceFeeRemunRsucdrInvoicesLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceFeeRemunRsucdrInvoicesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
