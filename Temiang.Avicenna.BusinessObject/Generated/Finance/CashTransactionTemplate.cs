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
	abstract public class esCashTransactionTemplateCollection : esEntityCollectionWAuditLog
	{
		public esCashTransactionTemplateCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "CashTransactionTemplateCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esCashTransactionTemplateQuery query)
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
			this.InitQuery(query as esCashTransactionTemplateQuery);
		}
		#endregion
			
		virtual public CashTransactionTemplate DetachEntity(CashTransactionTemplate entity)
		{
			return base.DetachEntity(entity) as CashTransactionTemplate;
		}
		
		virtual public CashTransactionTemplate AttachEntity(CashTransactionTemplate entity)
		{
			return base.AttachEntity(entity) as CashTransactionTemplate;
		}
		
		virtual public void Combine(CashTransactionTemplateCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CashTransactionTemplate this[int index]
		{
			get
			{
				return base[index] as CashTransactionTemplate;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CashTransactionTemplate);
		}
	}

	[Serializable]
	abstract public class esCashTransactionTemplate : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCashTransactionTemplateQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esCashTransactionTemplate()
		{
		}
	
		public esCashTransactionTemplate(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 templateId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(templateId);
			else
				return LoadByPrimaryKeyStoredProcedure(templateId);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 templateId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(templateId);
			else
				return LoadByPrimaryKeyStoredProcedure(templateId);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 templateId)
		{
			esCashTransactionTemplateQuery query = this.GetDynamicQuery();
			query.Where(query.TemplateId==templateId);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 templateId)
		{
			esParameters parms = new esParameters();
			parms.Add("TemplateId",templateId);
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
						case "TemplateId": this.str.TemplateId = (string)value; break;
						case "TemplateName": this.str.TemplateName = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
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
						case "TemplateId":
						
							if (value == null || value is System.Int32)
								this.TemplateId = (System.Int32?)value;
							break;
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to CashTransactionTemplate.TemplateId
		/// </summary>
		virtual public System.Int32? TemplateId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionTemplateMetadata.ColumnNames.TemplateId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionTemplateMetadata.ColumnNames.TemplateId, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionTemplate.TemplateName
		/// </summary>
		virtual public System.String TemplateName
		{
			get
			{
				return base.GetSystemString(CashTransactionTemplateMetadata.ColumnNames.TemplateName);
			}
			
			set
			{
				base.SetSystemString(CashTransactionTemplateMetadata.ColumnNames.TemplateName, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionTemplate.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(CashTransactionTemplateMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(CashTransactionTemplateMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionTemplate.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CashTransactionTemplateMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CashTransactionTemplateMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionTemplate.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(CashTransactionTemplateMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(CashTransactionTemplateMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionTemplate.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CashTransactionTemplateMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CashTransactionTemplateMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CashTransactionTemplate.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CashTransactionTemplateMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(CashTransactionTemplateMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCashTransactionTemplate entity)
			{
				this.entity = entity;
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
			public System.String TemplateName
			{
				get
				{
					System.String data = entity.TemplateName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemplateName = null;
					else entity.TemplateName = Convert.ToString(value);
				}
			}
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			private esCashTransactionTemplate entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCashTransactionTemplateQuery query)
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
				throw new Exception("esCashTransactionTemplate can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CashTransactionTemplate : esCashTransactionTemplate
	{	
	}

	[Serializable]
	abstract public class esCashTransactionTemplateQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return CashTransactionTemplateMetadata.Meta();
			}
		}	
			
		public esQueryItem TemplateId
		{
			get
			{
				return new esQueryItem(this, CashTransactionTemplateMetadata.ColumnNames.TemplateId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem TemplateName
		{
			get
			{
				return new esQueryItem(this, CashTransactionTemplateMetadata.ColumnNames.TemplateName, esSystemType.String);
			}
		} 
			
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, CashTransactionTemplateMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, CashTransactionTemplateMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, CashTransactionTemplateMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CashTransactionTemplateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CashTransactionTemplateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CashTransactionTemplateCollection")]
	public partial class CashTransactionTemplateCollection : esCashTransactionTemplateCollection, IEnumerable< CashTransactionTemplate>
	{
		public CashTransactionTemplateCollection()
		{

		}	
		
		public static implicit operator List< CashTransactionTemplate>(CashTransactionTemplateCollection coll)
		{
			List< CashTransactionTemplate> list = new List< CashTransactionTemplate>();
			
			foreach (CashTransactionTemplate emp in coll)
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
				return  CashTransactionTemplateMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CashTransactionTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CashTransactionTemplate(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CashTransactionTemplate();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public CashTransactionTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CashTransactionTemplateQuery();
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
		public bool Load(CashTransactionTemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CashTransactionTemplate AddNew()
		{
			CashTransactionTemplate entity = base.AddNewEntity() as CashTransactionTemplate;
			
			return entity;		
		}
		public CashTransactionTemplate FindByPrimaryKey(Int32 templateId)
		{
			return base.FindByPrimaryKey(templateId) as CashTransactionTemplate;
		}

		#region IEnumerable< CashTransactionTemplate> Members

		IEnumerator< CashTransactionTemplate> IEnumerable< CashTransactionTemplate>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CashTransactionTemplate;
			}
		}

		#endregion
		
		private CashTransactionTemplateQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CashTransactionTemplate' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CashTransactionTemplate ({TemplateId})")]
	[Serializable]
	public partial class CashTransactionTemplate : esCashTransactionTemplate
	{
		public CashTransactionTemplate()
		{
		}	
	
		public CashTransactionTemplate(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CashTransactionTemplateMetadata.Meta();
			}
		}	
	
		override protected esCashTransactionTemplateQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CashTransactionTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public CashTransactionTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CashTransactionTemplateQuery();
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
		public bool Load(CashTransactionTemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private CashTransactionTemplateQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CashTransactionTemplateQuery : esCashTransactionTemplateQuery
	{
		public CashTransactionTemplateQuery()
		{

		}		
		
		public CashTransactionTemplateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "CashTransactionTemplateQuery";
        }
	}

	[Serializable]
	public partial class CashTransactionTemplateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CashTransactionTemplateMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(CashTransactionTemplateMetadata.ColumnNames.TemplateId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionTemplateMetadata.PropertyNames.TemplateId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionTemplateMetadata.ColumnNames.TemplateName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionTemplateMetadata.PropertyNames.TemplateName;
			c.CharacterMaxLength = 50;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionTemplateMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CashTransactionTemplateMetadata.PropertyNames.IsActive;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionTemplateMetadata.ColumnNames.CreateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CashTransactionTemplateMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionTemplateMetadata.ColumnNames.CreateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionTemplateMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionTemplateMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CashTransactionTemplateMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CashTransactionTemplateMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionTemplateMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public CashTransactionTemplateMetadata Meta()
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
			public const string TemplateId = "TemplateId";
			public const string TemplateName = "TemplateName";
			public const string IsActive = "IsActive";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TemplateId = "TemplateId";
			public const string TemplateName = "TemplateName";
			public const string IsActive = "IsActive";
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
			lock (typeof(CashTransactionTemplateMetadata))
			{
				if(CashTransactionTemplateMetadata.mapDelegates == null)
				{
					CashTransactionTemplateMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CashTransactionTemplateMetadata.meta == null)
				{
					CashTransactionTemplateMetadata.meta = new CashTransactionTemplateMetadata();
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
				
				meta.AddTypeMap("TemplateId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TemplateName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "CashTransactionTemplate";
				meta.Destination = "CashTransactionTemplate";
				meta.spInsert = "proc_CashTransactionTemplateInsert";				
				meta.spUpdate = "proc_CashTransactionTemplateUpdate";		
				meta.spDelete = "proc_CashTransactionTemplateDelete";
				meta.spLoadAll = "proc_CashTransactionTemplateLoadAll";
				meta.spLoadByPrimaryKey = "proc_CashTransactionTemplateLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CashTransactionTemplateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
