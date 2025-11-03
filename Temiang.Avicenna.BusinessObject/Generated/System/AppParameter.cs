/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/13/2021 2:37:59 PM
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
	abstract public class esAppParameterCollection : esEntityCollectionWAuditLog
	{
		public esAppParameterCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "AppParameterCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esAppParameterQuery query)
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
			this.InitQuery(query as esAppParameterQuery);
		}
		#endregion
			
		virtual public AppParameter DetachEntity(AppParameter entity)
		{
			return base.DetachEntity(entity) as AppParameter;
		}
		
		virtual public AppParameter AttachEntity(AppParameter entity)
		{
			return base.AttachEntity(entity) as AppParameter;
		}
		
		virtual public void Combine(AppParameterCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AppParameter this[int index]
		{
			get
			{
				return base[index] as AppParameter;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppParameter);
		}
	}

	[Serializable]
	abstract public class esAppParameter : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppParameterQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esAppParameter()
		{
		}
	
		public esAppParameter(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String parameterID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(parameterID);
			else
				return LoadByPrimaryKeyStoredProcedure(parameterID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String parameterID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(parameterID);
			else
				return LoadByPrimaryKeyStoredProcedure(parameterID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String parameterID)
		{
			esAppParameterQuery query = this.GetDynamicQuery();
			query.Where(query.ParameterID == parameterID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String parameterID)
		{
			esParameters parms = new esParameters();
			parms.Add("ParameterID",parameterID);
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
						case "ParameterID": this.str.ParameterID = (string)value; break;
						case "ParameterName": this.str.ParameterName = (string)value; break;
						case "ParameterValue": this.str.ParameterValue = (string)value; break;
						case "ParameterType": this.str.ParameterType = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsUsedBySystem": this.str.IsUsedBySystem = (string)value; break;
						case "Message": this.str.Message = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsUsedBySystem":
						
							if (value == null || value is System.Boolean)
								this.IsUsedBySystem = (System.Boolean?)value;
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
		/// Maps to AppParameter.ParameterID
		/// </summary>
		virtual public System.String ParameterID
		{
			get
			{
				return base.GetSystemString(AppParameterMetadata.ColumnNames.ParameterID);
			}
			
			set
			{
				base.SetSystemString(AppParameterMetadata.ColumnNames.ParameterID, value);
			}
		}
		/// <summary>
		/// Maps to AppParameter.ParameterName
		/// </summary>
		virtual public System.String ParameterName
		{
			get
			{
				return base.GetSystemString(AppParameterMetadata.ColumnNames.ParameterName);
			}
			
			set
			{
				base.SetSystemString(AppParameterMetadata.ColumnNames.ParameterName, value);
			}
		}
		/// <summary>
		/// Maps to AppParameter.ParameterValue
		/// </summary>
		virtual public System.String ParameterValue
		{
			get
			{
				return base.GetSystemString(AppParameterMetadata.ColumnNames.ParameterValue);
			}
			
			set
			{
				base.SetSystemString(AppParameterMetadata.ColumnNames.ParameterValue, value);
			}
		}
		/// <summary>
		/// Maps to AppParameter.ParameterType
		/// </summary>
		virtual public System.String ParameterType
		{
			get
			{
				return base.GetSystemString(AppParameterMetadata.ColumnNames.ParameterType);
			}
			
			set
			{
				base.SetSystemString(AppParameterMetadata.ColumnNames.ParameterType, value);
			}
		}
		/// <summary>
		/// Maps to AppParameter.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppParameterMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AppParameterMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppParameter.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppParameterMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AppParameterMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AppParameter.IsUsedBySystem
		/// </summary>
		virtual public System.Boolean? IsUsedBySystem
		{
			get
			{
				return base.GetSystemBoolean(AppParameterMetadata.ColumnNames.IsUsedBySystem);
			}
			
			set
			{
				base.SetSystemBoolean(AppParameterMetadata.ColumnNames.IsUsedBySystem, value);
			}
		}
		/// <summary>
		/// Maps to AppParameter.Message
		/// </summary>
		virtual public System.String Message
		{
			get
			{
				return base.GetSystemString(AppParameterMetadata.ColumnNames.Message);
			}
			
			set
			{
				base.SetSystemString(AppParameterMetadata.ColumnNames.Message, value);
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
			public esStrings(esAppParameter entity)
			{
				this.entity = entity;
			}
			public System.String ParameterID
			{
				get
				{
					System.String data = entity.ParameterID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParameterID = null;
					else entity.ParameterID = Convert.ToString(value);
				}
			}
			public System.String ParameterName
			{
				get
				{
					System.String data = entity.ParameterName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParameterName = null;
					else entity.ParameterName = Convert.ToString(value);
				}
			}
			public System.String ParameterValue
			{
				get
				{
					System.String data = entity.ParameterValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParameterValue = null;
					else entity.ParameterValue = Convert.ToString(value);
				}
			}
			public System.String ParameterType
			{
				get
				{
					System.String data = entity.ParameterType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParameterType = null;
					else entity.ParameterType = Convert.ToString(value);
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
			public System.String IsUsedBySystem
			{
				get
				{
					System.Boolean? data = entity.IsUsedBySystem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUsedBySystem = null;
					else entity.IsUsedBySystem = Convert.ToBoolean(value);
				}
			}
			public System.String Message
			{
				get
				{
					System.String data = entity.Message;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Message = null;
					else entity.Message = Convert.ToString(value);
				}
			}
			private esAppParameter entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppParameterQuery query)
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
				throw new Exception("esAppParameter can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppParameter : esAppParameter
	{	
	}

	[Serializable]
	abstract public class esAppParameterQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return AppParameterMetadata.Meta();
			}
		}	
			
		public esQueryItem ParameterID
		{
			get
			{
				return new esQueryItem(this, AppParameterMetadata.ColumnNames.ParameterID, esSystemType.String);
			}
		} 
			
		public esQueryItem ParameterName
		{
			get
			{
				return new esQueryItem(this, AppParameterMetadata.ColumnNames.ParameterName, esSystemType.String);
			}
		} 
			
		public esQueryItem ParameterValue
		{
			get
			{
				return new esQueryItem(this, AppParameterMetadata.ColumnNames.ParameterValue, esSystemType.String);
			}
		} 
			
		public esQueryItem ParameterType
		{
			get
			{
				return new esQueryItem(this, AppParameterMetadata.ColumnNames.ParameterType, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AppParameterMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AppParameterMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsUsedBySystem
		{
			get
			{
				return new esQueryItem(this, AppParameterMetadata.ColumnNames.IsUsedBySystem, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem Message
		{
			get
			{
				return new esQueryItem(this, AppParameterMetadata.ColumnNames.Message, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppParameterCollection")]
	public partial class AppParameterCollection : esAppParameterCollection, IEnumerable< AppParameter>
	{
		public AppParameterCollection()
		{

		}	
		
		public static implicit operator List< AppParameter>(AppParameterCollection coll)
		{
			List< AppParameter> list = new List< AppParameter>();
			
			foreach (AppParameter emp in coll)
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
				return  AppParameterMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppParameterQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppParameter(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppParameter();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public AppParameterQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppParameterQuery();
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
		public bool Load(AppParameterQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppParameter AddNew()
		{
			AppParameter entity = base.AddNewEntity() as AppParameter;
			
			return entity;		
		}
		public AppParameter FindByPrimaryKey(String parameterID)
		{
			return base.FindByPrimaryKey(parameterID) as AppParameter;
		}

		#region IEnumerable< AppParameter> Members

		IEnumerator< AppParameter> IEnumerable< AppParameter>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AppParameter;
			}
		}

		#endregion
		
		private AppParameterQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppParameter' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppParameter ({ParameterID})")]
	[Serializable]
	public partial class AppParameter : esAppParameter
	{
		public AppParameter()
		{
		}	
	
		public AppParameter(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppParameterMetadata.Meta();
			}
		}	
	
		override protected esAppParameterQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppParameterQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public AppParameterQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppParameterQuery();
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
		public bool Load(AppParameterQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private AppParameterQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppParameterQuery : esAppParameterQuery
	{
		public AppParameterQuery()
		{

		}		
		
		public AppParameterQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "AppParameterQuery";
        }
	}

	[Serializable]
	public partial class AppParameterMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppParameterMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(AppParameterMetadata.ColumnNames.ParameterID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppParameterMetadata.PropertyNames.ParameterID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppParameterMetadata.ColumnNames.ParameterName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppParameterMetadata.PropertyNames.ParameterName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppParameterMetadata.ColumnNames.ParameterValue, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppParameterMetadata.PropertyNames.ParameterValue;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppParameterMetadata.ColumnNames.ParameterType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppParameterMetadata.PropertyNames.ParameterType;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppParameterMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppParameterMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppParameterMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AppParameterMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppParameterMetadata.ColumnNames.IsUsedBySystem, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppParameterMetadata.PropertyNames.IsUsedBySystem;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppParameterMetadata.ColumnNames.Message, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = AppParameterMetadata.PropertyNames.Message;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public AppParameterMetadata Meta()
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
			public const string ParameterID = "ParameterID";
			public const string ParameterName = "ParameterName";
			public const string ParameterValue = "ParameterValue";
			public const string ParameterType = "ParameterType";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsUsedBySystem = "IsUsedBySystem";
			public const string Message = "Message";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ParameterID = "ParameterID";
			public const string ParameterName = "ParameterName";
			public const string ParameterValue = "ParameterValue";
			public const string ParameterType = "ParameterType";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsUsedBySystem = "IsUsedBySystem";
			public const string Message = "Message";
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
			lock (typeof(AppParameterMetadata))
			{
				if(AppParameterMetadata.mapDelegates == null)
				{
					AppParameterMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AppParameterMetadata.meta == null)
				{
					AppParameterMetadata.meta = new AppParameterMetadata();
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
				
				meta.AddTypeMap("ParameterID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParameterName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParameterValue", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParameterType", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsUsedBySystem", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Message", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "AppParameter";
				meta.Destination = "AppParameter";
				meta.spInsert = "proc_AppParameterInsert";				
				meta.spUpdate = "proc_AppParameterUpdate";		
				meta.spDelete = "proc_AppParameterDelete";
				meta.spLoadAll = "proc_AppParameterLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppParameterLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppParameterMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
