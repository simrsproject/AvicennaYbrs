/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/25/2021 6:01:10 AM
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
	abstract public class esItemIdiItemSmfCollection : esEntityCollectionWAuditLog
	{
		public esItemIdiItemSmfCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ItemIdiItemSmfCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esItemIdiItemSmfQuery query)
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
			this.InitQuery(query as esItemIdiItemSmfQuery);
		}
		#endregion
			
		virtual public ItemIdiItemSmf DetachEntity(ItemIdiItemSmf entity)
		{
			return base.DetachEntity(entity) as ItemIdiItemSmf;
		}
		
		virtual public ItemIdiItemSmf AttachEntity(ItemIdiItemSmf entity)
		{
			return base.AttachEntity(entity) as ItemIdiItemSmf;
		}
		
		virtual public void Combine(ItemIdiItemSmfCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemIdiItemSmf this[int index]
		{
			get
			{
				return base[index] as ItemIdiItemSmf;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemIdiItemSmf);
		}
	}

	[Serializable]
	abstract public class esItemIdiItemSmf : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemIdiItemSmfQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esItemIdiItemSmf()
		{
		}
	
		public esItemIdiItemSmf(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String idiCode, String itemID, String smfID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(idiCode, itemID, smfID);
			else
				return LoadByPrimaryKeyStoredProcedure(idiCode, itemID, smfID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String idiCode, String itemID, String smfID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(idiCode, itemID, smfID);
			else
				return LoadByPrimaryKeyStoredProcedure(idiCode, itemID, smfID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String idiCode, String itemID, String smfID)
		{
			esItemIdiItemSmfQuery query = this.GetDynamicQuery();
			query.Where(query.IdiCode==idiCode, query.ItemID==itemID, query.SmfID==smfID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String idiCode, String itemID, String smfID)
		{
			esParameters parms = new esParameters();
			parms.Add("IdiCode",idiCode);
			parms.Add("ItemID",itemID);
			parms.Add("SmfID",smfID);
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
						case "IdiCode": this.str.IdiCode = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "SmfID": this.str.SmfID = (string)value; break;
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
		/// Maps to ItemIdiItemSmf.IdiCode
		/// </summary>
		virtual public System.String IdiCode
		{
			get
			{
				return base.GetSystemString(ItemIdiItemSmfMetadata.ColumnNames.IdiCode);
			}
			
			set
			{
				base.SetSystemString(ItemIdiItemSmfMetadata.ColumnNames.IdiCode, value);
			}
		}
		/// <summary>
		/// Maps to ItemIdiItemSmf.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemIdiItemSmfMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ItemIdiItemSmfMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ItemIdiItemSmf.SmfID
		/// </summary>
		virtual public System.String SmfID
		{
			get
			{
				return base.GetSystemString(ItemIdiItemSmfMetadata.ColumnNames.SmfID);
			}
			
			set
			{
				base.SetSystemString(ItemIdiItemSmfMetadata.ColumnNames.SmfID, value);
			}
		}
		/// <summary>
		/// Maps to ItemIdiItemSmf.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemIdiItemSmfMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemIdiItemSmfMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemIdiItemSmf.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(ItemIdiItemSmfMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemIdiItemSmfMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemIdiItemSmf.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemIdiItemSmfMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemIdiItemSmfMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemIdiItemSmf.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemIdiItemSmfMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemIdiItemSmfMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esItemIdiItemSmf entity)
			{
				this.entity = entity;
			}
			public System.String IdiCode
			{
				get
				{
					System.String data = entity.IdiCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IdiCode = null;
					else entity.IdiCode = Convert.ToString(value);
				}
			}
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
				}
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
			private esItemIdiItemSmf entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemIdiItemSmfQuery query)
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
				throw new Exception("esItemIdiItemSmf can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemIdiItemSmf : esItemIdiItemSmf
	{	
	}

	[Serializable]
	abstract public class esItemIdiItemSmfQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ItemIdiItemSmfMetadata.Meta();
			}
		}	
			
		public esQueryItem IdiCode
		{
			get
			{
				return new esQueryItem(this, ItemIdiItemSmfMetadata.ColumnNames.IdiCode, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemIdiItemSmfMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem SmfID
		{
			get
			{
				return new esQueryItem(this, ItemIdiItemSmfMetadata.ColumnNames.SmfID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemIdiItemSmfMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemIdiItemSmfMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemIdiItemSmfMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemIdiItemSmfMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemIdiItemSmfCollection")]
	public partial class ItemIdiItemSmfCollection : esItemIdiItemSmfCollection, IEnumerable< ItemIdiItemSmf>
	{
		public ItemIdiItemSmfCollection()
		{

		}	
		
		public static implicit operator List< ItemIdiItemSmf>(ItemIdiItemSmfCollection coll)
		{
			List< ItemIdiItemSmf> list = new List< ItemIdiItemSmf>();
			
			foreach (ItemIdiItemSmf emp in coll)
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
				return  ItemIdiItemSmfMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemIdiItemSmfQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemIdiItemSmf(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemIdiItemSmf();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ItemIdiItemSmfQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemIdiItemSmfQuery();
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
		public bool Load(ItemIdiItemSmfQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemIdiItemSmf AddNew()
		{
			ItemIdiItemSmf entity = base.AddNewEntity() as ItemIdiItemSmf;
			
			return entity;		
		}
		public ItemIdiItemSmf FindByPrimaryKey(String idiCode, String itemID, String smfID)
		{
			return base.FindByPrimaryKey(idiCode, itemID, smfID) as ItemIdiItemSmf;
		}

		#region IEnumerable< ItemIdiItemSmf> Members

		IEnumerator< ItemIdiItemSmf> IEnumerable< ItemIdiItemSmf>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemIdiItemSmf;
			}
		}

		#endregion
		
		private ItemIdiItemSmfQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemIdiItemSmf' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemIdiItemSmf ({IdiCode, ItemID, SmfID})")]
	[Serializable]
	public partial class ItemIdiItemSmf : esItemIdiItemSmf
	{
		public ItemIdiItemSmf()
		{
		}	
	
		public ItemIdiItemSmf(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemIdiItemSmfMetadata.Meta();
			}
		}	
	
		override protected esItemIdiItemSmfQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemIdiItemSmfQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ItemIdiItemSmfQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemIdiItemSmfQuery();
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
		public bool Load(ItemIdiItemSmfQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ItemIdiItemSmfQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemIdiItemSmfQuery : esItemIdiItemSmfQuery
	{
		public ItemIdiItemSmfQuery()
		{

		}		
		
		public ItemIdiItemSmfQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ItemIdiItemSmfQuery";
        }
	}

	[Serializable]
	public partial class ItemIdiItemSmfMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemIdiItemSmfMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ItemIdiItemSmfMetadata.ColumnNames.IdiCode, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemIdiItemSmfMetadata.PropertyNames.IdiCode;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemIdiItemSmfMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemIdiItemSmfMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemIdiItemSmfMetadata.ColumnNames.SmfID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemIdiItemSmfMetadata.PropertyNames.SmfID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemIdiItemSmfMetadata.ColumnNames.CreateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemIdiItemSmfMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemIdiItemSmfMetadata.ColumnNames.CreateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemIdiItemSmfMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemIdiItemSmfMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemIdiItemSmfMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ItemIdiItemSmfMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemIdiItemSmfMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ItemIdiItemSmfMetadata Meta()
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
			public const string IdiCode = "IdiCode";
			public const string ItemID = "ItemID";
			public const string SmfID = "SmfID";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string IdiCode = "IdiCode";
			public const string ItemID = "ItemID";
			public const string SmfID = "SmfID";
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
			lock (typeof(ItemIdiItemSmfMetadata))
			{
				if(ItemIdiItemSmfMetadata.mapDelegates == null)
				{
					ItemIdiItemSmfMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemIdiItemSmfMetadata.meta == null)
				{
					ItemIdiItemSmfMetadata.meta = new ItemIdiItemSmfMetadata();
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
				
				meta.AddTypeMap("IdiCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SmfID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "ItemIdiItemSmf";
				meta.Destination = "ItemIdiItemSmf";
				meta.spInsert = "proc_ItemIdiItemSmfInsert";				
				meta.spUpdate = "proc_ItemIdiItemSmfUpdate";		
				meta.spDelete = "proc_ItemIdiItemSmfDelete";
				meta.spLoadAll = "proc_ItemIdiItemSmfLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemIdiItemSmfLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemIdiItemSmfMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
