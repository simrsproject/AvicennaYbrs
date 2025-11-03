/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 07/15/20 2:15:05 PM
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
	abstract public class esAbRestrictionSuggestionCollection : esEntityCollectionWAuditLog
	{
		public esAbRestrictionSuggestionCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "AbRestrictionSuggestionCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esAbRestrictionSuggestionQuery query)
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
			this.InitQuery(query as esAbRestrictionSuggestionQuery);
		}
		#endregion
			
		virtual public AbRestrictionSuggestion DetachEntity(AbRestrictionSuggestion entity)
		{
			return base.DetachEntity(entity) as AbRestrictionSuggestion;
		}
		
		virtual public AbRestrictionSuggestion AttachEntity(AbRestrictionSuggestion entity)
		{
			return base.AttachEntity(entity) as AbRestrictionSuggestion;
		}
		
		virtual public void Combine(AbRestrictionSuggestionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AbRestrictionSuggestion this[int index]
		{
			get
			{
				return base[index] as AbRestrictionSuggestion;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AbRestrictionSuggestion);
		}
	}

	[Serializable]
	abstract public class esAbRestrictionSuggestion : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAbRestrictionSuggestionQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esAbRestrictionSuggestion()
		{
		}
	
		public esAbRestrictionSuggestion(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String abRestrictionID, Int32 abLevel)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(abRestrictionID, abLevel);
			else
				return LoadByPrimaryKeyStoredProcedure(abRestrictionID, abLevel);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String abRestrictionID, Int32 abLevel)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(abRestrictionID, abLevel);
			else
				return LoadByPrimaryKeyStoredProcedure(abRestrictionID, abLevel);
		}
	
		private bool LoadByPrimaryKeyDynamic(String abRestrictionID, Int32 abLevel)
		{
			esAbRestrictionSuggestionQuery query = this.GetDynamicQuery();
			query.Where(query.AbRestrictionID == abRestrictionID, query.AbLevel == abLevel);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String abRestrictionID, Int32 abLevel)
		{
			esParameters parms = new esParameters();
			parms.Add("AbRestrictionID",abRestrictionID);
			parms.Add("AbLevel",abLevel);
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
						case "AbLevel": this.str.AbLevel = (string)value; break;
						case "SuggestionNote": this.str.SuggestionNote = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "AbLevel":
						
							if (value == null || value is System.Int32)
								this.AbLevel = (System.Int32?)value;
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
		/// Maps to AbRestrictionSuggestion.AbRestrictionID
		/// </summary>
		virtual public System.String AbRestrictionID
		{
			get
			{
				return base.GetSystemString(AbRestrictionSuggestionMetadata.ColumnNames.AbRestrictionID);
			}
			
			set
			{
				base.SetSystemString(AbRestrictionSuggestionMetadata.ColumnNames.AbRestrictionID, value);
			}
		}
		/// <summary>
		/// Maps to AbRestrictionSuggestion.AbLevel
		/// </summary>
		virtual public System.Int32? AbLevel
		{
			get
			{
				return base.GetSystemInt32(AbRestrictionSuggestionMetadata.ColumnNames.AbLevel);
			}
			
			set
			{
				base.SetSystemInt32(AbRestrictionSuggestionMetadata.ColumnNames.AbLevel, value);
			}
		}
		/// <summary>
		/// Maps to AbRestrictionSuggestion.SuggestionNote
		/// </summary>
		virtual public System.String SuggestionNote
		{
			get
			{
				return base.GetSystemString(AbRestrictionSuggestionMetadata.ColumnNames.SuggestionNote);
			}
			
			set
			{
				base.SetSystemString(AbRestrictionSuggestionMetadata.ColumnNames.SuggestionNote, value);
			}
		}
		/// <summary>
		/// Maps to AbRestrictionSuggestion.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AbRestrictionSuggestionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AbRestrictionSuggestionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AbRestrictionSuggestion.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AbRestrictionSuggestionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AbRestrictionSuggestionMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAbRestrictionSuggestion entity)
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
			public System.String AbLevel
			{
				get
				{
					System.Int32? data = entity.AbLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AbLevel = null;
					else entity.AbLevel = Convert.ToInt32(value);
				}
			}
			public System.String SuggestionNote
			{
				get
				{
					System.String data = entity.SuggestionNote;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SuggestionNote = null;
					else entity.SuggestionNote = Convert.ToString(value);
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
			private esAbRestrictionSuggestion entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAbRestrictionSuggestionQuery query)
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
				throw new Exception("esAbRestrictionSuggestion can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AbRestrictionSuggestion : esAbRestrictionSuggestion
	{	
	}

	[Serializable]
	abstract public class esAbRestrictionSuggestionQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return AbRestrictionSuggestionMetadata.Meta();
			}
		}	
			
		public esQueryItem AbRestrictionID
		{
			get
			{
				return new esQueryItem(this, AbRestrictionSuggestionMetadata.ColumnNames.AbRestrictionID, esSystemType.String);
			}
		} 
			
		public esQueryItem AbLevel
		{
			get
			{
				return new esQueryItem(this, AbRestrictionSuggestionMetadata.ColumnNames.AbLevel, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SuggestionNote
		{
			get
			{
				return new esQueryItem(this, AbRestrictionSuggestionMetadata.ColumnNames.SuggestionNote, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AbRestrictionSuggestionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AbRestrictionSuggestionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AbRestrictionSuggestionCollection")]
	public partial class AbRestrictionSuggestionCollection : esAbRestrictionSuggestionCollection, IEnumerable< AbRestrictionSuggestion>
	{
		public AbRestrictionSuggestionCollection()
		{

		}	
		
		public static implicit operator List< AbRestrictionSuggestion>(AbRestrictionSuggestionCollection coll)
		{
			List< AbRestrictionSuggestion> list = new List< AbRestrictionSuggestion>();
			
			foreach (AbRestrictionSuggestion emp in coll)
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
				return  AbRestrictionSuggestionMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AbRestrictionSuggestionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AbRestrictionSuggestion(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AbRestrictionSuggestion();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public AbRestrictionSuggestionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AbRestrictionSuggestionQuery();
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
		public bool Load(AbRestrictionSuggestionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AbRestrictionSuggestion AddNew()
		{
			AbRestrictionSuggestion entity = base.AddNewEntity() as AbRestrictionSuggestion;
			
			return entity;		
		}
		public AbRestrictionSuggestion FindByPrimaryKey(String abRestrictionID, Int32 abLevel)
		{
			return base.FindByPrimaryKey(abRestrictionID, abLevel) as AbRestrictionSuggestion;
		}

		#region IEnumerable< AbRestrictionSuggestion> Members

		IEnumerator< AbRestrictionSuggestion> IEnumerable< AbRestrictionSuggestion>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AbRestrictionSuggestion;
			}
		}

		#endregion
		
		private AbRestrictionSuggestionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AbRestrictionSuggestion' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AbRestrictionSuggestion ({AbRestrictionID, AbLevel})")]
	[Serializable]
	public partial class AbRestrictionSuggestion : esAbRestrictionSuggestion
	{
		public AbRestrictionSuggestion()
		{
		}	
	
		public AbRestrictionSuggestion(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AbRestrictionSuggestionMetadata.Meta();
			}
		}	
	
		override protected esAbRestrictionSuggestionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AbRestrictionSuggestionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public AbRestrictionSuggestionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AbRestrictionSuggestionQuery();
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
		public bool Load(AbRestrictionSuggestionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private AbRestrictionSuggestionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AbRestrictionSuggestionQuery : esAbRestrictionSuggestionQuery
	{
		public AbRestrictionSuggestionQuery()
		{

		}		
		
		public AbRestrictionSuggestionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "AbRestrictionSuggestionQuery";
        }
	}

	[Serializable]
	public partial class AbRestrictionSuggestionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AbRestrictionSuggestionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(AbRestrictionSuggestionMetadata.ColumnNames.AbRestrictionID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AbRestrictionSuggestionMetadata.PropertyNames.AbRestrictionID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AbRestrictionSuggestionMetadata.ColumnNames.AbLevel, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AbRestrictionSuggestionMetadata.PropertyNames.AbLevel;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AbRestrictionSuggestionMetadata.ColumnNames.SuggestionNote, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AbRestrictionSuggestionMetadata.PropertyNames.SuggestionNote;
			c.CharacterMaxLength = 2000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AbRestrictionSuggestionMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AbRestrictionSuggestionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AbRestrictionSuggestionMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AbRestrictionSuggestionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public AbRestrictionSuggestionMetadata Meta()
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
			public const string AbLevel = "AbLevel";
			public const string SuggestionNote = "SuggestionNote";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string AbRestrictionID = "AbRestrictionID";
			public const string AbLevel = "AbLevel";
			public const string SuggestionNote = "SuggestionNote";
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
			lock (typeof(AbRestrictionSuggestionMetadata))
			{
				if(AbRestrictionSuggestionMetadata.mapDelegates == null)
				{
					AbRestrictionSuggestionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AbRestrictionSuggestionMetadata.meta == null)
				{
					AbRestrictionSuggestionMetadata.meta = new AbRestrictionSuggestionMetadata();
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
				meta.AddTypeMap("AbLevel", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SuggestionNote", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "AbRestrictionSuggestion";
				meta.Destination = "AbRestrictionSuggestion";
				meta.spInsert = "proc_AbRestrictionSuggestionInsert";				
				meta.spUpdate = "proc_AbRestrictionSuggestionUpdate";		
				meta.spDelete = "proc_AbRestrictionSuggestionDelete";
				meta.spLoadAll = "proc_AbRestrictionSuggestionLoadAll";
				meta.spLoadByPrimaryKey = "proc_AbRestrictionSuggestionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AbRestrictionSuggestionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
