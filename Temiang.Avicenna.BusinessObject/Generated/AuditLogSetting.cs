/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/26/2023 6:35:49 PM
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
	abstract public class esAuditLogSettingCollection : esEntityCollectionWAuditLog
	{
		public esAuditLogSettingCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "AuditLogSettingCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esAuditLogSettingQuery query)
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
			this.InitQuery(query as esAuditLogSettingQuery);
		}
		#endregion
			
		virtual public AuditLogSetting DetachEntity(AuditLogSetting entity)
		{
			return base.DetachEntity(entity) as AuditLogSetting;
		}
		
		virtual public AuditLogSetting AttachEntity(AuditLogSetting entity)
		{
			return base.AttachEntity(entity) as AuditLogSetting;
		}
		
		virtual public void Combine(AuditLogSettingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AuditLogSetting this[int index]
		{
			get
			{
				return base[index] as AuditLogSetting;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AuditLogSetting);
		}
	}

	[Serializable]
	abstract public class esAuditLogSetting : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAuditLogSettingQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esAuditLogSetting()
		{
		}
	
		public esAuditLogSetting(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String tableName)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(tableName);
			else
				return LoadByPrimaryKeyStoredProcedure(tableName);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String tableName)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(tableName);
			else
				return LoadByPrimaryKeyStoredProcedure(tableName);
		}
	
		private bool LoadByPrimaryKeyDynamic(String tableName)
		{
			esAuditLogSettingQuery query = this.GetDynamicQuery();
			query.Where(query.TableName == tableName);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String tableName)
		{
			esParameters parms = new esParameters();
			parms.Add("TableName",tableName);
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
						case "TableName": this.str.TableName = (string)value; break;
						case "TableDescription": this.str.TableDescription = (string)value; break;
						case "IsAuditLog": this.str.IsAuditLog = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsConsolidationBranchToHO": this.str.IsConsolidationBranchToHO = (string)value; break;
						case "IsConsolidationHOToBranch": this.str.IsConsolidationHOToBranch = (string)value; break;
						case "ExcludeAuditColumn": this.str.ExcludeAuditColumn = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsAuditLog":
						
							if (value == null || value is System.Boolean)
								this.IsAuditLog = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsConsolidationBranchToHO":
						
							if (value == null || value is System.Boolean)
								this.IsConsolidationBranchToHO = (System.Boolean?)value;
							break;
						case "IsConsolidationHOToBranch":
						
							if (value == null || value is System.Boolean)
								this.IsConsolidationHOToBranch = (System.Boolean?)value;
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
		/// Maps to AuditLogSetting.TableName
		/// </summary>
		virtual public System.String TableName
		{
			get
			{
				return base.GetSystemString(AuditLogSettingMetadata.ColumnNames.TableName);
			}
			
			set
			{
				base.SetSystemString(AuditLogSettingMetadata.ColumnNames.TableName, value);
			}
		}
		/// <summary>
		/// Maps to AuditLogSetting.TableDescription
		/// </summary>
		virtual public System.String TableDescription
		{
			get
			{
				return base.GetSystemString(AuditLogSettingMetadata.ColumnNames.TableDescription);
			}
			
			set
			{
				base.SetSystemString(AuditLogSettingMetadata.ColumnNames.TableDescription, value);
			}
		}
		/// <summary>
		/// Maps to AuditLogSetting.IsAuditLog
		/// </summary>
		virtual public System.Boolean? IsAuditLog
		{
			get
			{
				return base.GetSystemBoolean(AuditLogSettingMetadata.ColumnNames.IsAuditLog);
			}
			
			set
			{
				base.SetSystemBoolean(AuditLogSettingMetadata.ColumnNames.IsAuditLog, value);
			}
		}
		/// <summary>
		/// Maps to AuditLogSetting.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AuditLogSettingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AuditLogSettingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AuditLogSetting.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AuditLogSettingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AuditLogSettingMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AuditLogSetting.IsConsolidationBranchToHO
		/// </summary>
		virtual public System.Boolean? IsConsolidationBranchToHO
		{
			get
			{
				return base.GetSystemBoolean(AuditLogSettingMetadata.ColumnNames.IsConsolidationBranchToHO);
			}
			
			set
			{
				base.SetSystemBoolean(AuditLogSettingMetadata.ColumnNames.IsConsolidationBranchToHO, value);
			}
		}
		/// <summary>
		/// Maps to AuditLogSetting.IsConsolidationHOToBranch
		/// </summary>
		virtual public System.Boolean? IsConsolidationHOToBranch
		{
			get
			{
				return base.GetSystemBoolean(AuditLogSettingMetadata.ColumnNames.IsConsolidationHOToBranch);
			}
			
			set
			{
				base.SetSystemBoolean(AuditLogSettingMetadata.ColumnNames.IsConsolidationHOToBranch, value);
			}
		}
		/// <summary>
		/// Maps to AuditLogSetting.ExcludeAuditColumn
		/// </summary>
		virtual public System.String ExcludeAuditColumn
		{
			get
			{
				return base.GetSystemString(AuditLogSettingMetadata.ColumnNames.ExcludeAuditColumn);
			}
			
			set
			{
				base.SetSystemString(AuditLogSettingMetadata.ColumnNames.ExcludeAuditColumn, value);
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
			public esStrings(esAuditLogSetting entity)
			{
				this.entity = entity;
			}
			public System.String TableName
			{
				get
				{
					System.String data = entity.TableName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TableName = null;
					else entity.TableName = Convert.ToString(value);
				}
			}
			public System.String TableDescription
			{
				get
				{
					System.String data = entity.TableDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TableDescription = null;
					else entity.TableDescription = Convert.ToString(value);
				}
			}
			public System.String IsAuditLog
			{
				get
				{
					System.Boolean? data = entity.IsAuditLog;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAuditLog = null;
					else entity.IsAuditLog = Convert.ToBoolean(value);
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
			public System.String IsConsolidationBranchToHO
			{
				get
				{
					System.Boolean? data = entity.IsConsolidationBranchToHO;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsConsolidationBranchToHO = null;
					else entity.IsConsolidationBranchToHO = Convert.ToBoolean(value);
				}
			}
			public System.String IsConsolidationHOToBranch
			{
				get
				{
					System.Boolean? data = entity.IsConsolidationHOToBranch;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsConsolidationHOToBranch = null;
					else entity.IsConsolidationHOToBranch = Convert.ToBoolean(value);
				}
			}
			public System.String ExcludeAuditColumn
			{
				get
				{
					System.String data = entity.ExcludeAuditColumn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExcludeAuditColumn = null;
					else entity.ExcludeAuditColumn = Convert.ToString(value);
				}
			}
			private esAuditLogSetting entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAuditLogSettingQuery query)
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
				throw new Exception("esAuditLogSetting can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AuditLogSetting : esAuditLogSetting
	{	
	}

	[Serializable]
	abstract public class esAuditLogSettingQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return AuditLogSettingMetadata.Meta();
			}
		}	
			
		public esQueryItem TableName
		{
			get
			{
				return new esQueryItem(this, AuditLogSettingMetadata.ColumnNames.TableName, esSystemType.String);
			}
		} 
			
		public esQueryItem TableDescription
		{
			get
			{
				return new esQueryItem(this, AuditLogSettingMetadata.ColumnNames.TableDescription, esSystemType.String);
			}
		} 
			
		public esQueryItem IsAuditLog
		{
			get
			{
				return new esQueryItem(this, AuditLogSettingMetadata.ColumnNames.IsAuditLog, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AuditLogSettingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AuditLogSettingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsConsolidationBranchToHO
		{
			get
			{
				return new esQueryItem(this, AuditLogSettingMetadata.ColumnNames.IsConsolidationBranchToHO, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsConsolidationHOToBranch
		{
			get
			{
				return new esQueryItem(this, AuditLogSettingMetadata.ColumnNames.IsConsolidationHOToBranch, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ExcludeAuditColumn
		{
			get
			{
				return new esQueryItem(this, AuditLogSettingMetadata.ColumnNames.ExcludeAuditColumn, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AuditLogSettingCollection")]
	public partial class AuditLogSettingCollection : esAuditLogSettingCollection, IEnumerable< AuditLogSetting>
	{
		public AuditLogSettingCollection()
		{

		}	
		
		public static implicit operator List< AuditLogSetting>(AuditLogSettingCollection coll)
		{
			List< AuditLogSetting> list = new List< AuditLogSetting>();
			
			foreach (AuditLogSetting emp in coll)
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
				return  AuditLogSettingMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AuditLogSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AuditLogSetting(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AuditLogSetting();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public AuditLogSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AuditLogSettingQuery();
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
		public bool Load(AuditLogSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AuditLogSetting AddNew()
		{
			AuditLogSetting entity = base.AddNewEntity() as AuditLogSetting;
			
			return entity;		
		}
		public AuditLogSetting FindByPrimaryKey(String tableName)
		{
			return base.FindByPrimaryKey(tableName) as AuditLogSetting;
		}

		#region IEnumerable< AuditLogSetting> Members

		IEnumerator< AuditLogSetting> IEnumerable< AuditLogSetting>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AuditLogSetting;
			}
		}

		#endregion
		
		private AuditLogSettingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AuditLogSetting' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AuditLogSetting ({TableName})")]
	[Serializable]
	public partial class AuditLogSetting : esAuditLogSetting
	{
		public AuditLogSetting()
		{
		}	
	
		public AuditLogSetting(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AuditLogSettingMetadata.Meta();
			}
		}	
	
		override protected esAuditLogSettingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AuditLogSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public AuditLogSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AuditLogSettingQuery();
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
		public bool Load(AuditLogSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private AuditLogSettingQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AuditLogSettingQuery : esAuditLogSettingQuery
	{
		public AuditLogSettingQuery()
		{

		}		
		
		public AuditLogSettingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "AuditLogSettingQuery";
        }
	}

	[Serializable]
	public partial class AuditLogSettingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AuditLogSettingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(AuditLogSettingMetadata.ColumnNames.TableName, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AuditLogSettingMetadata.PropertyNames.TableName;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 100;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AuditLogSettingMetadata.ColumnNames.TableDescription, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AuditLogSettingMetadata.PropertyNames.TableDescription;
			c.CharacterMaxLength = 200;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AuditLogSettingMetadata.ColumnNames.IsAuditLog, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AuditLogSettingMetadata.PropertyNames.IsAuditLog;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AuditLogSettingMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AuditLogSettingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AuditLogSettingMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AuditLogSettingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AuditLogSettingMetadata.ColumnNames.IsConsolidationBranchToHO, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AuditLogSettingMetadata.PropertyNames.IsConsolidationBranchToHO;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AuditLogSettingMetadata.ColumnNames.IsConsolidationHOToBranch, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AuditLogSettingMetadata.PropertyNames.IsConsolidationHOToBranch;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AuditLogSettingMetadata.ColumnNames.ExcludeAuditColumn, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = AuditLogSettingMetadata.PropertyNames.ExcludeAuditColumn;
			c.CharacterMaxLength = 300;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public AuditLogSettingMetadata Meta()
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
			public const string TableName = "TableName";
			public const string TableDescription = "TableDescription";
			public const string IsAuditLog = "IsAuditLog";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsConsolidationBranchToHO = "IsConsolidationBranchToHO";
			public const string IsConsolidationHOToBranch = "IsConsolidationHOToBranch";
			public const string ExcludeAuditColumn = "ExcludeAuditColumn";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TableName = "TableName";
			public const string TableDescription = "TableDescription";
			public const string IsAuditLog = "IsAuditLog";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsConsolidationBranchToHO = "IsConsolidationBranchToHO";
			public const string IsConsolidationHOToBranch = "IsConsolidationHOToBranch";
			public const string ExcludeAuditColumn = "ExcludeAuditColumn";
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
			lock (typeof(AuditLogSettingMetadata))
			{
				if(AuditLogSettingMetadata.mapDelegates == null)
				{
					AuditLogSettingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AuditLogSettingMetadata.meta == null)
				{
					AuditLogSettingMetadata.meta = new AuditLogSettingMetadata();
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
				
				meta.AddTypeMap("TableName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TableDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAuditLog", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsConsolidationBranchToHO", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsConsolidationHOToBranch", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ExcludeAuditColumn", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "AuditLogSetting";
				meta.Destination = "AuditLogSetting";
				meta.spInsert = "proc_AuditLogSettingInsert";				
				meta.spUpdate = "proc_AuditLogSettingUpdate";		
				meta.spDelete = "proc_AuditLogSettingDelete";
				meta.spLoadAll = "proc_AuditLogSettingLoadAll";
				meta.spLoadByPrimaryKey = "proc_AuditLogSettingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AuditLogSettingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
