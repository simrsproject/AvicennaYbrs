/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/26/2021 1:23:58 PM
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
	abstract public class esSmfDiagnoseCollection : esEntityCollectionWAuditLog
	{
		public esSmfDiagnoseCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "SmfDiagnoseCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esSmfDiagnoseQuery query)
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
			this.InitQuery(query as esSmfDiagnoseQuery);
		}
		#endregion
			
		virtual public SmfDiagnose DetachEntity(SmfDiagnose entity)
		{
			return base.DetachEntity(entity) as SmfDiagnose;
		}
		
		virtual public SmfDiagnose AttachEntity(SmfDiagnose entity)
		{
			return base.AttachEntity(entity) as SmfDiagnose;
		}
		
		virtual public void Combine(SmfDiagnoseCollection collection)
		{
			base.Combine(collection);
		}
		
		new public SmfDiagnose this[int index]
		{
			get
			{
				return base[index] as SmfDiagnose;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SmfDiagnose);
		}
	}

	[Serializable]
	abstract public class esSmfDiagnose : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSmfDiagnoseQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esSmfDiagnose()
		{
		}
	
		public esSmfDiagnose(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String smfID, String diagnoseID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(smfID, diagnoseID);
			else
				return LoadByPrimaryKeyStoredProcedure(smfID, diagnoseID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String smfID, String diagnoseID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(smfID, diagnoseID);
			else
				return LoadByPrimaryKeyStoredProcedure(smfID, diagnoseID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String smfID, String diagnoseID)
		{
			esSmfDiagnoseQuery query = this.GetDynamicQuery();
			query.Where(query.SmfID == smfID, query.DiagnoseID == diagnoseID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String smfID, String diagnoseID)
		{
			esParameters parms = new esParameters();
			parms.Add("SmfID",smfID);
			parms.Add("DiagnoseID",diagnoseID);
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
						case "SmfID": this.str.SmfID = (string)value; break;
						case "DiagnoseID": this.str.DiagnoseID = (string)value; break;
						case "IsVisible": this.str.IsVisible = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsVisible":
						
							if (value == null || value is System.Boolean)
								this.IsVisible = (System.Boolean?)value;
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
		/// Maps to SmfDiagnose.SmfID
		/// </summary>
		virtual public System.String SmfID
		{
			get
			{
				return base.GetSystemString(SmfDiagnoseMetadata.ColumnNames.SmfID);
			}
			
			set
			{
				base.SetSystemString(SmfDiagnoseMetadata.ColumnNames.SmfID, value);
			}
		}
		/// <summary>
		/// Maps to SmfDiagnose.DiagnoseID
		/// </summary>
		virtual public System.String DiagnoseID
		{
			get
			{
				return base.GetSystemString(SmfDiagnoseMetadata.ColumnNames.DiagnoseID);
			}
			
			set
			{
				base.SetSystemString(SmfDiagnoseMetadata.ColumnNames.DiagnoseID, value);
			}
		}
		/// <summary>
		/// Maps to SmfDiagnose.IsVisible
		/// </summary>
		virtual public System.Boolean? IsVisible
		{
			get
			{
				return base.GetSystemBoolean(SmfDiagnoseMetadata.ColumnNames.IsVisible);
			}
			
			set
			{
				base.SetSystemBoolean(SmfDiagnoseMetadata.ColumnNames.IsVisible, value);
			}
		}
		/// <summary>
		/// Maps to SmfDiagnose.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SmfDiagnoseMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(SmfDiagnoseMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SmfDiagnose.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SmfDiagnoseMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(SmfDiagnoseMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSmfDiagnose entity)
			{
				this.entity = entity;
			}
			public System.String SmfID
			{
				get
				{
					System.String data = entity.SmfID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SmfID = null;
					else entity.SmfID = Convert.ToString(value);
				}
			}
			public System.String DiagnoseID
			{
				get
				{
					System.String data = entity.DiagnoseID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiagnoseID = null;
					else entity.DiagnoseID = Convert.ToString(value);
				}
			}
			public System.String IsVisible
			{
				get
				{
					System.Boolean? data = entity.IsVisible;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVisible = null;
					else entity.IsVisible = Convert.ToBoolean(value);
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
			private esSmfDiagnose entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSmfDiagnoseQuery query)
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
				throw new Exception("esSmfDiagnose can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SmfDiagnose : esSmfDiagnose
	{	
	}

	[Serializable]
	abstract public class esSmfDiagnoseQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return SmfDiagnoseMetadata.Meta();
			}
		}	
			
		public esQueryItem SmfID
		{
			get
			{
				return new esQueryItem(this, SmfDiagnoseMetadata.ColumnNames.SmfID, esSystemType.String);
			}
		} 
			
		public esQueryItem DiagnoseID
		{
			get
			{
				return new esQueryItem(this, SmfDiagnoseMetadata.ColumnNames.DiagnoseID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsVisible
		{
			get
			{
				return new esQueryItem(this, SmfDiagnoseMetadata.ColumnNames.IsVisible, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SmfDiagnoseMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SmfDiagnoseMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SmfDiagnoseCollection")]
	public partial class SmfDiagnoseCollection : esSmfDiagnoseCollection, IEnumerable< SmfDiagnose>
	{
		public SmfDiagnoseCollection()
		{

		}	
		
		public static implicit operator List< SmfDiagnose>(SmfDiagnoseCollection coll)
		{
			List< SmfDiagnose> list = new List< SmfDiagnose>();
			
			foreach (SmfDiagnose emp in coll)
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
				return  SmfDiagnoseMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SmfDiagnoseQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SmfDiagnose(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SmfDiagnose();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public SmfDiagnoseQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SmfDiagnoseQuery();
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
		public bool Load(SmfDiagnoseQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SmfDiagnose AddNew()
		{
			SmfDiagnose entity = base.AddNewEntity() as SmfDiagnose;
			
			return entity;		
		}
		public SmfDiagnose FindByPrimaryKey(String smfID, String diagnoseID)
		{
			return base.FindByPrimaryKey(smfID, diagnoseID) as SmfDiagnose;
		}

		#region IEnumerable< SmfDiagnose> Members

		IEnumerator< SmfDiagnose> IEnumerable< SmfDiagnose>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as SmfDiagnose;
			}
		}

		#endregion
		
		private SmfDiagnoseQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SmfDiagnose' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SmfDiagnose ({SmfID, DiagnoseID})")]
	[Serializable]
	public partial class SmfDiagnose : esSmfDiagnose
	{
		public SmfDiagnose()
		{
		}	
	
		public SmfDiagnose(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SmfDiagnoseMetadata.Meta();
			}
		}	
	
		override protected esSmfDiagnoseQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SmfDiagnoseQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public SmfDiagnoseQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SmfDiagnoseQuery();
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
		public bool Load(SmfDiagnoseQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private SmfDiagnoseQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SmfDiagnoseQuery : esSmfDiagnoseQuery
	{
		public SmfDiagnoseQuery()
		{

		}		
		
		public SmfDiagnoseQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "SmfDiagnoseQuery";
        }
	}

	[Serializable]
	public partial class SmfDiagnoseMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SmfDiagnoseMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(SmfDiagnoseMetadata.ColumnNames.SmfID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = SmfDiagnoseMetadata.PropertyNames.SmfID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(SmfDiagnoseMetadata.ColumnNames.DiagnoseID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SmfDiagnoseMetadata.PropertyNames.DiagnoseID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(SmfDiagnoseMetadata.ColumnNames.IsVisible, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SmfDiagnoseMetadata.PropertyNames.IsVisible;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(SmfDiagnoseMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SmfDiagnoseMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(SmfDiagnoseMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = SmfDiagnoseMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public SmfDiagnoseMetadata Meta()
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
			public const string SmfID = "SmfID";
			public const string DiagnoseID = "DiagnoseID";
			public const string IsVisible = "IsVisible";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string SmfID = "SmfID";
			public const string DiagnoseID = "DiagnoseID";
			public const string IsVisible = "IsVisible";
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
			lock (typeof(SmfDiagnoseMetadata))
			{
				if(SmfDiagnoseMetadata.mapDelegates == null)
				{
					SmfDiagnoseMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (SmfDiagnoseMetadata.meta == null)
				{
					SmfDiagnoseMetadata.meta = new SmfDiagnoseMetadata();
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
				
				meta.AddTypeMap("SmfID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiagnoseID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVisible", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "SmfDiagnose";
				meta.Destination = "SmfDiagnose";
				meta.spInsert = "proc_SmfDiagnoseInsert";				
				meta.spUpdate = "proc_SmfDiagnoseUpdate";		
				meta.spDelete = "proc_SmfDiagnoseDelete";
				meta.spLoadAll = "proc_SmfDiagnoseLoadAll";
				meta.spLoadByPrimaryKey = "proc_SmfDiagnoseLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SmfDiagnoseMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
