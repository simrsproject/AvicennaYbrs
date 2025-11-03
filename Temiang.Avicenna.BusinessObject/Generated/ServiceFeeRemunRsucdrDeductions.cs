/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/5/2022 1:01:40 PM
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
	abstract public class esServiceFeeRemunRsucdrDeductionsCollection : esEntityCollectionWAuditLog
	{
		public esServiceFeeRemunRsucdrDeductionsCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ServiceFeeRemunRsucdrDeductionsCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esServiceFeeRemunRsucdrDeductionsQuery query)
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
			this.InitQuery(query as esServiceFeeRemunRsucdrDeductionsQuery);
		}
		#endregion
			
		virtual public ServiceFeeRemunRsucdrDeductions DetachEntity(ServiceFeeRemunRsucdrDeductions entity)
		{
			return base.DetachEntity(entity) as ServiceFeeRemunRsucdrDeductions;
		}
		
		virtual public ServiceFeeRemunRsucdrDeductions AttachEntity(ServiceFeeRemunRsucdrDeductions entity)
		{
			return base.AttachEntity(entity) as ServiceFeeRemunRsucdrDeductions;
		}
		
		virtual public void Combine(ServiceFeeRemunRsucdrDeductionsCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ServiceFeeRemunRsucdrDeductions this[int index]
		{
			get
			{
				return base[index] as ServiceFeeRemunRsucdrDeductions;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceFeeRemunRsucdrDeductions);
		}
	}

	[Serializable]
	abstract public class esServiceFeeRemunRsucdrDeductions : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceFeeRemunRsucdrDeductionsQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esServiceFeeRemunRsucdrDeductions()
		{
		}
	
		public esServiceFeeRemunRsucdrDeductions(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 remunID, String sRRemunDeduction)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(remunID, sRRemunDeduction);
			else
				return LoadByPrimaryKeyStoredProcedure(remunID, sRRemunDeduction);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 remunID, String sRRemunDeduction)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(remunID, sRRemunDeduction);
			else
				return LoadByPrimaryKeyStoredProcedure(remunID, sRRemunDeduction);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 remunID, String sRRemunDeduction)
		{
			esServiceFeeRemunRsucdrDeductionsQuery query = this.GetDynamicQuery();
			query.Where(query.RemunID==remunID, query.SRRemunDeduction==sRRemunDeduction);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 remunID, String sRRemunDeduction)
		{
			esParameters parms = new esParameters();
			parms.Add("RemunID",remunID);
			parms.Add("SRRemunDeduction",sRRemunDeduction);
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
						case "SRRemunDeduction": this.str.SRRemunDeduction = (string)value; break;
						case "Amount": this.str.Amount = (string)value; break;
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
						case "Amount":
						
							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
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
		/// Maps to ServiceFeeRemunRsucdrDeductions.RemunID
		/// </summary>
		virtual public System.Int32? RemunID
		{
			get
			{
				return base.GetSystemInt32(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.RemunID);
			}
			
			set
			{
				base.SetSystemInt32(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.RemunID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdrDeductions.SRRemunDeduction
		/// </summary>
		virtual public System.String SRRemunDeduction
		{
			get
			{
				return base.GetSystemString(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.SRRemunDeduction);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.SRRemunDeduction, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdrDeductions.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.Amount);
			}
			
			set
			{
				base.SetSystemDecimal(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.Amount, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdrDeductions.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdrDeductions.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdrDeductions.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceFeeRemunRsucdrDeductions.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esServiceFeeRemunRsucdrDeductions entity)
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
			public System.String SRRemunDeduction
			{
				get
				{
					System.String data = entity.SRRemunDeduction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRemunDeduction = null;
					else entity.SRRemunDeduction = Convert.ToString(value);
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
			private esServiceFeeRemunRsucdrDeductions entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceFeeRemunRsucdrDeductionsQuery query)
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
				throw new Exception("esServiceFeeRemunRsucdrDeductions can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ServiceFeeRemunRsucdrDeductions : esServiceFeeRemunRsucdrDeductions
	{	
	}

	[Serializable]
	abstract public class esServiceFeeRemunRsucdrDeductionsQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ServiceFeeRemunRsucdrDeductionsMetadata.Meta();
			}
		}	
			
		public esQueryItem RemunID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.RemunID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SRRemunDeduction
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.SRRemunDeduction, esSystemType.String);
			}
		} 
			
		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceFeeRemunRsucdrDeductionsCollection")]
	public partial class ServiceFeeRemunRsucdrDeductionsCollection : esServiceFeeRemunRsucdrDeductionsCollection, IEnumerable< ServiceFeeRemunRsucdrDeductions>
	{
		public ServiceFeeRemunRsucdrDeductionsCollection()
		{

		}	
		
		public static implicit operator List< ServiceFeeRemunRsucdrDeductions>(ServiceFeeRemunRsucdrDeductionsCollection coll)
		{
			List< ServiceFeeRemunRsucdrDeductions> list = new List< ServiceFeeRemunRsucdrDeductions>();
			
			foreach (ServiceFeeRemunRsucdrDeductions emp in coll)
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
				return  ServiceFeeRemunRsucdrDeductionsMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceFeeRemunRsucdrDeductionsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceFeeRemunRsucdrDeductions(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceFeeRemunRsucdrDeductions();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ServiceFeeRemunRsucdrDeductionsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceFeeRemunRsucdrDeductionsQuery();
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
		public bool Load(ServiceFeeRemunRsucdrDeductionsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ServiceFeeRemunRsucdrDeductions AddNew()
		{
			ServiceFeeRemunRsucdrDeductions entity = base.AddNewEntity() as ServiceFeeRemunRsucdrDeductions;
			
			return entity;		
		}
		public ServiceFeeRemunRsucdrDeductions FindByPrimaryKey(Int32 remunID, String sRRemunDeduction)
		{
			return base.FindByPrimaryKey(remunID, sRRemunDeduction) as ServiceFeeRemunRsucdrDeductions;
		}

		#region IEnumerable< ServiceFeeRemunRsucdrDeductions> Members

		IEnumerator< ServiceFeeRemunRsucdrDeductions> IEnumerable< ServiceFeeRemunRsucdrDeductions>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ServiceFeeRemunRsucdrDeductions;
			}
		}

		#endregion
		
		private ServiceFeeRemunRsucdrDeductionsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceFeeRemunRsucdrDeductions' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ServiceFeeRemunRsucdrDeductions ({RemunID, SRRemunDeduction})")]
	[Serializable]
	public partial class ServiceFeeRemunRsucdrDeductions : esServiceFeeRemunRsucdrDeductions
	{
		public ServiceFeeRemunRsucdrDeductions()
		{
		}	
	
		public ServiceFeeRemunRsucdrDeductions(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceFeeRemunRsucdrDeductionsMetadata.Meta();
			}
		}	
	
		override protected esServiceFeeRemunRsucdrDeductionsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceFeeRemunRsucdrDeductionsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ServiceFeeRemunRsucdrDeductionsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceFeeRemunRsucdrDeductionsQuery();
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
		public bool Load(ServiceFeeRemunRsucdrDeductionsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ServiceFeeRemunRsucdrDeductionsQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ServiceFeeRemunRsucdrDeductionsQuery : esServiceFeeRemunRsucdrDeductionsQuery
	{
		public ServiceFeeRemunRsucdrDeductionsQuery()
		{

		}		
		
		public ServiceFeeRemunRsucdrDeductionsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ServiceFeeRemunRsucdrDeductionsQuery";
        }
	}

	[Serializable]
	public partial class ServiceFeeRemunRsucdrDeductionsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceFeeRemunRsucdrDeductionsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.RemunID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceFeeRemunRsucdrDeductionsMetadata.PropertyNames.RemunID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.SRRemunDeduction, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeRemunRsucdrDeductionsMetadata.PropertyNames.SRRemunDeduction;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.Amount, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceFeeRemunRsucdrDeductionsMetadata.PropertyNames.Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.CreateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeRemunRsucdrDeductionsMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.CreateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceFeeRemunRsucdrDeductionsMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceFeeRemunRsucdrDeductionsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceFeeRemunRsucdrDeductionsMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceFeeRemunRsucdrDeductionsMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ServiceFeeRemunRsucdrDeductionsMetadata Meta()
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
			public const string SRRemunDeduction = "SRRemunDeduction";
			public const string Amount = "Amount";
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
			public const string SRRemunDeduction = "SRRemunDeduction";
			public const string Amount = "Amount";
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
			lock (typeof(ServiceFeeRemunRsucdrDeductionsMetadata))
			{
				if(ServiceFeeRemunRsucdrDeductionsMetadata.mapDelegates == null)
				{
					ServiceFeeRemunRsucdrDeductionsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ServiceFeeRemunRsucdrDeductionsMetadata.meta == null)
				{
					ServiceFeeRemunRsucdrDeductionsMetadata.meta = new ServiceFeeRemunRsucdrDeductionsMetadata();
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
				meta.AddTypeMap("SRRemunDeduction", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Amount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "ServiceFeeRemunRsucdrDeductions";
				meta.Destination = "ServiceFeeRemunRsucdrDeductions";
				meta.spInsert = "proc_ServiceFeeRemunRsucdrDeductionsInsert";				
				meta.spUpdate = "proc_ServiceFeeRemunRsucdrDeductionsUpdate";		
				meta.spDelete = "proc_ServiceFeeRemunRsucdrDeductionsDelete";
				meta.spLoadAll = "proc_ServiceFeeRemunRsucdrDeductionsLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceFeeRemunRsucdrDeductionsLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceFeeRemunRsucdrDeductionsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
