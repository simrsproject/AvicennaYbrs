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
	abstract public class esErrorMessageCollection : esEntityCollectionWAuditLog
	{
		public esErrorMessageCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ErrorMessageCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esErrorMessageQuery query)
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
			this.InitQuery(query as esErrorMessageQuery);
		}
		#endregion
			
		virtual public ErrorMessage DetachEntity(ErrorMessage entity)
		{
			return base.DetachEntity(entity) as ErrorMessage;
		}
		
		virtual public ErrorMessage AttachEntity(ErrorMessage entity)
		{
			return base.AttachEntity(entity) as ErrorMessage;
		}
		
		virtual public void Combine(ErrorMessageCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ErrorMessage this[int index]
		{
			get
			{
				return base[index] as ErrorMessage;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ErrorMessage);
		}
	}

	[Serializable]
	abstract public class esErrorMessage : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esErrorMessageQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esErrorMessage()
		{
		}
	
		public esErrorMessage(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 iD)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(iD);
			else
				return LoadByPrimaryKeyStoredProcedure(iD);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 iD)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(iD);
			else
				return LoadByPrimaryKeyStoredProcedure(iD);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 iD)
		{
			esErrorMessageQuery query = this.GetDynamicQuery();
			query.Where(query.ID==iD);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 iD)
		{
			esParameters parms = new esParameters();
			parms.Add("ID",iD);
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
						case "ID": this.str.ID = (string)value; break;
						case "Message": this.str.Message = (string)value; break;
						case "CreatedBy": this.str.CreatedBy = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ID":
						
							if (value == null || value is System.Int32)
								this.ID = (System.Int32?)value;
							break;
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to ErrorMessage.ID
		/// </summary>
		virtual public System.Int32? ID
		{
			get
			{
				return base.GetSystemInt32(ErrorMessageMetadata.ColumnNames.ID);
			}
			
			set
			{
				base.SetSystemInt32(ErrorMessageMetadata.ColumnNames.ID, value);
			}
		}
		/// <summary>
		/// Maps to ErrorMessage.Message
		/// </summary>
		virtual public System.String Message
		{
			get
			{
				return base.GetSystemString(ErrorMessageMetadata.ColumnNames.Message);
			}
			
			set
			{
				base.SetSystemString(ErrorMessageMetadata.ColumnNames.Message, value);
			}
		}
		/// <summary>
		/// Maps to ErrorMessage.CreatedBy
		/// </summary>
		virtual public System.String CreatedBy
		{
			get
			{
				return base.GetSystemString(ErrorMessageMetadata.ColumnNames.CreatedBy);
			}
			
			set
			{
				base.SetSystemString(ErrorMessageMetadata.ColumnNames.CreatedBy, value);
			}
		}
		/// <summary>
		/// Maps to ErrorMessage.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ErrorMessageMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ErrorMessageMetadata.ColumnNames.CreatedDateTime, value);
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
			public esStrings(esErrorMessage entity)
			{
				this.entity = entity;
			}
			public System.String ID
			{
				get
				{
					System.Int32? data = entity.ID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ID = null;
					else entity.ID = Convert.ToInt32(value);
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
			private esErrorMessage entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esErrorMessageQuery query)
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
				throw new Exception("esErrorMessage can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ErrorMessage : esErrorMessage
	{	
	}

	[Serializable]
	abstract public class esErrorMessageQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ErrorMessageMetadata.Meta();
			}
		}	
			
		public esQueryItem ID
		{
			get
			{
				return new esQueryItem(this, ErrorMessageMetadata.ColumnNames.ID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Message
		{
			get
			{
				return new esQueryItem(this, ErrorMessageMetadata.ColumnNames.Message, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedBy
		{
			get
			{
				return new esQueryItem(this, ErrorMessageMetadata.ColumnNames.CreatedBy, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, ErrorMessageMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ErrorMessageCollection")]
	public partial class ErrorMessageCollection : esErrorMessageCollection, IEnumerable< ErrorMessage>
	{
		public ErrorMessageCollection()
		{

		}	
		
		public static implicit operator List< ErrorMessage>(ErrorMessageCollection coll)
		{
			List< ErrorMessage> list = new List< ErrorMessage>();
			
			foreach (ErrorMessage emp in coll)
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
				return  ErrorMessageMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ErrorMessageQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ErrorMessage(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ErrorMessage();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ErrorMessageQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ErrorMessageQuery();
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
		public bool Load(ErrorMessageQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ErrorMessage AddNew()
		{
			ErrorMessage entity = base.AddNewEntity() as ErrorMessage;
			
			return entity;		
		}
		public ErrorMessage FindByPrimaryKey(Int32 iD)
		{
			return base.FindByPrimaryKey(iD) as ErrorMessage;
		}

		#region IEnumerable< ErrorMessage> Members

		IEnumerator< ErrorMessage> IEnumerable< ErrorMessage>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ErrorMessage;
			}
		}

		#endregion
		
		private ErrorMessageQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ErrorMessage' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ErrorMessage ({ID})")]
	[Serializable]
	public partial class ErrorMessage : esErrorMessage
	{
		public ErrorMessage()
		{
		}	
	
		public ErrorMessage(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ErrorMessageMetadata.Meta();
			}
		}	
	
		override protected esErrorMessageQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ErrorMessageQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ErrorMessageQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ErrorMessageQuery();
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
		public bool Load(ErrorMessageQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ErrorMessageQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ErrorMessageQuery : esErrorMessageQuery
	{
		public ErrorMessageQuery()
		{

		}		
		
		public ErrorMessageQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ErrorMessageQuery";
        }
	}

	[Serializable]
	public partial class ErrorMessageMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ErrorMessageMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ErrorMessageMetadata.ColumnNames.ID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ErrorMessageMetadata.PropertyNames.ID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ErrorMessageMetadata.ColumnNames.Message, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ErrorMessageMetadata.PropertyNames.Message;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ErrorMessageMetadata.ColumnNames.CreatedBy, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ErrorMessageMetadata.PropertyNames.CreatedBy;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ErrorMessageMetadata.ColumnNames.CreatedDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ErrorMessageMetadata.PropertyNames.CreatedDateTime;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ErrorMessageMetadata Meta()
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
			public const string ID = "ID";
			public const string Message = "Message";
			public const string CreatedBy = "CreatedBy";
			public const string CreatedDateTime = "CreatedDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ID = "ID";
			public const string Message = "Message";
			public const string CreatedBy = "CreatedBy";
			public const string CreatedDateTime = "CreatedDateTime";
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
			lock (typeof(ErrorMessageMetadata))
			{
				if(ErrorMessageMetadata.mapDelegates == null)
				{
					ErrorMessageMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ErrorMessageMetadata.meta == null)
				{
					ErrorMessageMetadata.meta = new ErrorMessageMetadata();
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
				
				meta.AddTypeMap("ID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Message", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "ErrorMessage";
				meta.Destination = "ErrorMessage";
				meta.spInsert = "proc_ErrorMessageInsert";				
				meta.spUpdate = "proc_ErrorMessageUpdate";		
				meta.spDelete = "proc_ErrorMessageDelete";
				meta.spLoadAll = "proc_ErrorMessageLoadAll";
				meta.spLoadByPrimaryKey = "proc_ErrorMessageLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ErrorMessageMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
