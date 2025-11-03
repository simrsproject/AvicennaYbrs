/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 07/29/20 9:36:33 AM
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
	abstract public class esAbRestrictionCollection : esEntityCollectionWAuditLog
	{
		public esAbRestrictionCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "AbRestrictionCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esAbRestrictionQuery query)
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
			this.InitQuery(query as esAbRestrictionQuery);
		}
		#endregion
			
		virtual public AbRestriction DetachEntity(AbRestriction entity)
		{
			return base.DetachEntity(entity) as AbRestriction;
		}
		
		virtual public AbRestriction AttachEntity(AbRestriction entity)
		{
			return base.AttachEntity(entity) as AbRestriction;
		}
		
		virtual public void Combine(AbRestrictionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AbRestriction this[int index]
		{
			get
			{
				return base[index] as AbRestriction;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AbRestriction);
		}
	}

	[Serializable]
	abstract public class esAbRestriction : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAbRestrictionQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esAbRestriction()
		{
		}
	
		public esAbRestriction(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String abRestrictionID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(abRestrictionID);
			else
				return LoadByPrimaryKeyStoredProcedure(abRestrictionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String abRestrictionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(abRestrictionID);
			else
				return LoadByPrimaryKeyStoredProcedure(abRestrictionID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String abRestrictionID)
		{
			esAbRestrictionQuery query = this.GetDynamicQuery();
			query.Where(query.AbRestrictionID == abRestrictionID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String abRestrictionID)
		{
			esParameters parms = new esParameters();
			parms.Add("AbRestrictionID",abRestrictionID);
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
						case "AbRestrictionID": this.str.AbRestrictionID = (string)value; break;
						case "ParentID": this.str.ParentID = (string)value; break;
						case "AbRestrictionName": this.str.AbRestrictionName = (string)value; break;
						case "SRAbRestrictionType": this.str.SRAbRestrictionType = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsNotRestricted": this.str.IsNotRestricted = (string)value; break;
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
						case "IsNotRestricted":
						
							if (value == null || value is System.Boolean)
								this.IsNotRestricted = (System.Boolean?)value;
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
		/// Maps to AbRestriction.AbRestrictionID
		/// </summary>
		virtual public System.String AbRestrictionID
		{
			get
			{
				return base.GetSystemString(AbRestrictionMetadata.ColumnNames.AbRestrictionID);
			}
			
			set
			{
				base.SetSystemString(AbRestrictionMetadata.ColumnNames.AbRestrictionID, value);
			}
		}
		/// <summary>
		/// Maps to AbRestriction.ParentID
		/// </summary>
		virtual public System.String ParentID
		{
			get
			{
				return base.GetSystemString(AbRestrictionMetadata.ColumnNames.ParentID);
			}
			
			set
			{
				base.SetSystemString(AbRestrictionMetadata.ColumnNames.ParentID, value);
			}
		}
		/// <summary>
		/// Maps to AbRestriction.AbRestrictionName
		/// </summary>
		virtual public System.String AbRestrictionName
		{
			get
			{
				return base.GetSystemString(AbRestrictionMetadata.ColumnNames.AbRestrictionName);
			}
			
			set
			{
				base.SetSystemString(AbRestrictionMetadata.ColumnNames.AbRestrictionName, value);
			}
		}
		/// <summary>
		/// Maps to AbRestriction.SRAbRestrictionType
		/// </summary>
		virtual public System.String SRAbRestrictionType
		{
			get
			{
				return base.GetSystemString(AbRestrictionMetadata.ColumnNames.SRAbRestrictionType);
			}
			
			set
			{
				base.SetSystemString(AbRestrictionMetadata.ColumnNames.SRAbRestrictionType, value);
			}
		}
		/// <summary>
		/// Maps to AbRestriction.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AbRestrictionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AbRestrictionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AbRestriction.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AbRestrictionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AbRestrictionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AbRestriction.IsNotRestricted
		/// </summary>
		virtual public System.Boolean? IsNotRestricted
		{
			get
			{
				return base.GetSystemBoolean(AbRestrictionMetadata.ColumnNames.IsNotRestricted);
			}
			
			set
			{
				base.SetSystemBoolean(AbRestrictionMetadata.ColumnNames.IsNotRestricted, value);
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
			public esStrings(esAbRestriction entity)
			{
				this.entity = entity;
			}
			public System.String AbRestrictionID
			{
				get
				{
					System.String data = entity.AbRestrictionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AbRestrictionID = null;
					else entity.AbRestrictionID = Convert.ToString(value);
				}
			}
			public System.String ParentID
			{
				get
				{
					System.String data = entity.ParentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParentID = null;
					else entity.ParentID = Convert.ToString(value);
				}
			}
			public System.String AbRestrictionName
			{
				get
				{
					System.String data = entity.AbRestrictionName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AbRestrictionName = null;
					else entity.AbRestrictionName = Convert.ToString(value);
				}
			}
			public System.String SRAbRestrictionType
			{
				get
				{
					System.String data = entity.SRAbRestrictionType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAbRestrictionType = null;
					else entity.SRAbRestrictionType = Convert.ToString(value);
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
			public System.String IsNotRestricted
			{
				get
				{
					System.Boolean? data = entity.IsNotRestricted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNotRestricted = null;
					else entity.IsNotRestricted = Convert.ToBoolean(value);
				}
			}
			private esAbRestriction entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAbRestrictionQuery query)
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
				throw new Exception("esAbRestriction can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AbRestriction : esAbRestriction
	{	
	}

	[Serializable]
	abstract public class esAbRestrictionQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return AbRestrictionMetadata.Meta();
			}
		}	
			
		public esQueryItem AbRestrictionID
		{
			get
			{
				return new esQueryItem(this, AbRestrictionMetadata.ColumnNames.AbRestrictionID, esSystemType.String);
			}
		} 
			
		public esQueryItem ParentID
		{
			get
			{
				return new esQueryItem(this, AbRestrictionMetadata.ColumnNames.ParentID, esSystemType.String);
			}
		} 
			
		public esQueryItem AbRestrictionName
		{
			get
			{
				return new esQueryItem(this, AbRestrictionMetadata.ColumnNames.AbRestrictionName, esSystemType.String);
			}
		} 
			
		public esQueryItem SRAbRestrictionType
		{
			get
			{
				return new esQueryItem(this, AbRestrictionMetadata.ColumnNames.SRAbRestrictionType, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AbRestrictionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AbRestrictionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsNotRestricted
		{
			get
			{
				return new esQueryItem(this, AbRestrictionMetadata.ColumnNames.IsNotRestricted, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AbRestrictionCollection")]
	public partial class AbRestrictionCollection : esAbRestrictionCollection, IEnumerable< AbRestriction>
	{
		public AbRestrictionCollection()
		{

		}	
		
		public static implicit operator List< AbRestriction>(AbRestrictionCollection coll)
		{
			List< AbRestriction> list = new List< AbRestriction>();
			
			foreach (AbRestriction emp in coll)
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
				return  AbRestrictionMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AbRestrictionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AbRestriction(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AbRestriction();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public AbRestrictionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AbRestrictionQuery();
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
		public bool Load(AbRestrictionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AbRestriction AddNew()
		{
			AbRestriction entity = base.AddNewEntity() as AbRestriction;
			
			return entity;		
		}
		public AbRestriction FindByPrimaryKey(String abRestrictionID)
		{
			return base.FindByPrimaryKey(abRestrictionID) as AbRestriction;
		}

		#region IEnumerable< AbRestriction> Members

		IEnumerator< AbRestriction> IEnumerable< AbRestriction>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AbRestriction;
			}
		}

		#endregion
		
		private AbRestrictionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AbRestriction' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AbRestriction ({AbRestrictionID})")]
	[Serializable]
	public partial class AbRestriction : esAbRestriction
	{
		public AbRestriction()
		{
		}	
	
		public AbRestriction(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AbRestrictionMetadata.Meta();
			}
		}	
	
		override protected esAbRestrictionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AbRestrictionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public AbRestrictionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AbRestrictionQuery();
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
		public bool Load(AbRestrictionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private AbRestrictionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AbRestrictionQuery : esAbRestrictionQuery
	{
		public AbRestrictionQuery()
		{

		}		
		
		public AbRestrictionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "AbRestrictionQuery";
        }
	}

	[Serializable]
	public partial class AbRestrictionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AbRestrictionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(AbRestrictionMetadata.ColumnNames.AbRestrictionID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AbRestrictionMetadata.PropertyNames.AbRestrictionID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AbRestrictionMetadata.ColumnNames.ParentID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AbRestrictionMetadata.PropertyNames.ParentID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AbRestrictionMetadata.ColumnNames.AbRestrictionName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AbRestrictionMetadata.PropertyNames.AbRestrictionName;
			c.CharacterMaxLength = 300;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AbRestrictionMetadata.ColumnNames.SRAbRestrictionType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AbRestrictionMetadata.PropertyNames.SRAbRestrictionType;
			c.CharacterMaxLength = 4;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AbRestrictionMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AbRestrictionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AbRestrictionMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AbRestrictionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AbRestrictionMetadata.ColumnNames.IsNotRestricted, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AbRestrictionMetadata.PropertyNames.IsNotRestricted;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public AbRestrictionMetadata Meta()
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
			public const string AbRestrictionID = "AbRestrictionID";
			public const string ParentID = "ParentID";
			public const string AbRestrictionName = "AbRestrictionName";
			public const string SRAbRestrictionType = "SRAbRestrictionType";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsNotRestricted = "IsNotRestricted";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string AbRestrictionID = "AbRestrictionID";
			public const string ParentID = "ParentID";
			public const string AbRestrictionName = "AbRestrictionName";
			public const string SRAbRestrictionType = "SRAbRestrictionType";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsNotRestricted = "IsNotRestricted";
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
			lock (typeof(AbRestrictionMetadata))
			{
				if(AbRestrictionMetadata.mapDelegates == null)
				{
					AbRestrictionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AbRestrictionMetadata.meta == null)
				{
					AbRestrictionMetadata.meta = new AbRestrictionMetadata();
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
				
				meta.AddTypeMap("AbRestrictionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AbRestrictionName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAbRestrictionType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsNotRestricted", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "AbRestriction";
				meta.Destination = "AbRestriction";
				meta.spInsert = "proc_AbRestrictionInsert";				
				meta.spUpdate = "proc_AbRestrictionUpdate";		
				meta.spDelete = "proc_AbRestrictionDelete";
				meta.spLoadAll = "proc_AbRestrictionLoadAll";
				meta.spLoadByPrimaryKey = "proc_AbRestrictionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AbRestrictionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
