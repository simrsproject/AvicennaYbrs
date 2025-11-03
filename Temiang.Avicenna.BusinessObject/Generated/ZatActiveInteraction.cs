/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/29/2022 2:05:35 PM
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
	abstract public class esZatActiveInteractionCollection : esEntityCollectionWAuditLog
	{
		public esZatActiveInteractionCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ZatActiveInteractionCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esZatActiveInteractionQuery query)
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
			this.InitQuery(query as esZatActiveInteractionQuery);
		}
		#endregion
			
		virtual public ZatActiveInteraction DetachEntity(ZatActiveInteraction entity)
		{
			return base.DetachEntity(entity) as ZatActiveInteraction;
		}
		
		virtual public ZatActiveInteraction AttachEntity(ZatActiveInteraction entity)
		{
			return base.AttachEntity(entity) as ZatActiveInteraction;
		}
		
		virtual public void Combine(ZatActiveInteractionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ZatActiveInteraction this[int index]
		{
			get
			{
				return base[index] as ZatActiveInteraction;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ZatActiveInteraction);
		}
	}

	[Serializable]
	abstract public class esZatActiveInteraction : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esZatActiveInteractionQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esZatActiveInteraction()
		{
		}
	
		public esZatActiveInteraction(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String zatActiveID, String interactionZatActiveID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(zatActiveID, interactionZatActiveID);
			else
				return LoadByPrimaryKeyStoredProcedure(zatActiveID, interactionZatActiveID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String zatActiveID, String interactionZatActiveID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(zatActiveID, interactionZatActiveID);
			else
				return LoadByPrimaryKeyStoredProcedure(zatActiveID, interactionZatActiveID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String zatActiveID, String interactionZatActiveID)
		{
			esZatActiveInteractionQuery query = this.GetDynamicQuery();
			query.Where(query.ZatActiveID == zatActiveID, query.InteractionZatActiveID == interactionZatActiveID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String zatActiveID, String interactionZatActiveID)
		{
			esParameters parms = new esParameters();
			parms.Add("ZatActiveID",zatActiveID);
			parms.Add("InteractionZatActiveID",interactionZatActiveID);
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
						case "ZatActiveID": this.str.ZatActiveID = (string)value; break;
						case "InteractionZatActiveID": this.str.InteractionZatActiveID = (string)value; break;
						case "Interaction": this.str.Interaction = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
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
		/// Maps to ZatActiveInteraction.ZatActiveID
		/// </summary>
		virtual public System.String ZatActiveID
		{
			get
			{
				return base.GetSystemString(ZatActiveInteractionMetadata.ColumnNames.ZatActiveID);
			}
			
			set
			{
				base.SetSystemString(ZatActiveInteractionMetadata.ColumnNames.ZatActiveID, value);
			}
		}
		/// <summary>
		/// Maps to ZatActiveInteraction.InteractionZatActiveID
		/// </summary>
		virtual public System.String InteractionZatActiveID
		{
			get
			{
				return base.GetSystemString(ZatActiveInteractionMetadata.ColumnNames.InteractionZatActiveID);
			}
			
			set
			{
				base.SetSystemString(ZatActiveInteractionMetadata.ColumnNames.InteractionZatActiveID, value);
			}
		}
		/// <summary>
		/// Maps to ZatActiveInteraction.Interaction
		/// </summary>
		virtual public System.String Interaction
		{
			get
			{
				return base.GetSystemString(ZatActiveInteractionMetadata.ColumnNames.Interaction);
			}
			
			set
			{
				base.SetSystemString(ZatActiveInteractionMetadata.ColumnNames.Interaction, value);
			}
		}
		/// <summary>
		/// Maps to ZatActiveInteraction.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ZatActiveInteractionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ZatActiveInteractionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ZatActiveInteraction.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ZatActiveInteractionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ZatActiveInteractionMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esZatActiveInteraction entity)
			{
				this.entity = entity;
			}
			public System.String ZatActiveID
			{
				get
				{
					System.String data = entity.ZatActiveID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ZatActiveID = null;
					else entity.ZatActiveID = Convert.ToString(value);
				}
			}
			public System.String InteractionZatActiveID
			{
				get
				{
					System.String data = entity.InteractionZatActiveID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InteractionZatActiveID = null;
					else entity.InteractionZatActiveID = Convert.ToString(value);
				}
			}
			public System.String Interaction
			{
				get
				{
					System.String data = entity.Interaction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Interaction = null;
					else entity.Interaction = Convert.ToString(value);
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
			private esZatActiveInteraction entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esZatActiveInteractionQuery query)
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
				throw new Exception("esZatActiveInteraction can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ZatActiveInteraction : esZatActiveInteraction
	{	
	}

	[Serializable]
	abstract public class esZatActiveInteractionQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ZatActiveInteractionMetadata.Meta();
			}
		}	
			
		public esQueryItem ZatActiveID
		{
			get
			{
				return new esQueryItem(this, ZatActiveInteractionMetadata.ColumnNames.ZatActiveID, esSystemType.String);
			}
		} 
			
		public esQueryItem InteractionZatActiveID
		{
			get
			{
				return new esQueryItem(this, ZatActiveInteractionMetadata.ColumnNames.InteractionZatActiveID, esSystemType.String);
			}
		} 
			
		public esQueryItem Interaction
		{
			get
			{
				return new esQueryItem(this, ZatActiveInteractionMetadata.ColumnNames.Interaction, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ZatActiveInteractionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ZatActiveInteractionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ZatActiveInteractionCollection")]
	public partial class ZatActiveInteractionCollection : esZatActiveInteractionCollection, IEnumerable< ZatActiveInteraction>
	{
		public ZatActiveInteractionCollection()
		{

		}	
		
		public static implicit operator List< ZatActiveInteraction>(ZatActiveInteractionCollection coll)
		{
			List< ZatActiveInteraction> list = new List< ZatActiveInteraction>();
			
			foreach (ZatActiveInteraction emp in coll)
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
				return  ZatActiveInteractionMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ZatActiveInteractionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ZatActiveInteraction(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ZatActiveInteraction();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ZatActiveInteractionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ZatActiveInteractionQuery();
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
		public bool Load(ZatActiveInteractionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ZatActiveInteraction AddNew()
		{
			ZatActiveInteraction entity = base.AddNewEntity() as ZatActiveInteraction;
			
			return entity;		
		}
		public ZatActiveInteraction FindByPrimaryKey(String zatActiveID, String interactionZatActiveID)
		{
			return base.FindByPrimaryKey(zatActiveID, interactionZatActiveID) as ZatActiveInteraction;
		}

		#region IEnumerable< ZatActiveInteraction> Members

		IEnumerator< ZatActiveInteraction> IEnumerable< ZatActiveInteraction>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ZatActiveInteraction;
			}
		}

		#endregion
		
		private ZatActiveInteractionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ZatActiveInteraction' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ZatActiveInteraction ({ZatActiveID, InteractionZatActiveID})")]
	[Serializable]
	public partial class ZatActiveInteraction : esZatActiveInteraction
	{
		public ZatActiveInteraction()
		{
		}	
	
		public ZatActiveInteraction(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ZatActiveInteractionMetadata.Meta();
			}
		}	
	
		override protected esZatActiveInteractionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ZatActiveInteractionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ZatActiveInteractionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ZatActiveInteractionQuery();
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
		public bool Load(ZatActiveInteractionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ZatActiveInteractionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ZatActiveInteractionQuery : esZatActiveInteractionQuery
	{
		public ZatActiveInteractionQuery()
		{

		}		
		
		public ZatActiveInteractionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ZatActiveInteractionQuery";
        }
	}

	[Serializable]
	public partial class ZatActiveInteractionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ZatActiveInteractionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ZatActiveInteractionMetadata.ColumnNames.ZatActiveID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ZatActiveInteractionMetadata.PropertyNames.ZatActiveID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ZatActiveInteractionMetadata.ColumnNames.InteractionZatActiveID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ZatActiveInteractionMetadata.PropertyNames.InteractionZatActiveID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ZatActiveInteractionMetadata.ColumnNames.Interaction, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ZatActiveInteractionMetadata.PropertyNames.Interaction;
			c.CharacterMaxLength = 500;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ZatActiveInteractionMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ZatActiveInteractionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ZatActiveInteractionMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ZatActiveInteractionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ZatActiveInteractionMetadata Meta()
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
			public const string ZatActiveID = "ZatActiveID";
			public const string InteractionZatActiveID = "InteractionZatActiveID";
			public const string Interaction = "Interaction";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ZatActiveID = "ZatActiveID";
			public const string InteractionZatActiveID = "InteractionZatActiveID";
			public const string Interaction = "Interaction";
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
			lock (typeof(ZatActiveInteractionMetadata))
			{
				if(ZatActiveInteractionMetadata.mapDelegates == null)
				{
					ZatActiveInteractionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ZatActiveInteractionMetadata.meta == null)
				{
					ZatActiveInteractionMetadata.meta = new ZatActiveInteractionMetadata();
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
				
				meta.AddTypeMap("ZatActiveID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InteractionZatActiveID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Interaction", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "ZatActiveInteraction";
				meta.Destination = "ZatActiveInteraction";
				meta.spInsert = "proc_ZatActiveInteractionInsert";				
				meta.spUpdate = "proc_ZatActiveInteractionUpdate";		
				meta.spDelete = "proc_ZatActiveInteractionDelete";
				meta.spLoadAll = "proc_ZatActiveInteractionLoadAll";
				meta.spLoadByPrimaryKey = "proc_ZatActiveInteractionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ZatActiveInteractionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
