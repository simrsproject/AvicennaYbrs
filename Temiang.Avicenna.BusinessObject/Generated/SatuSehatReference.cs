/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/24/2023 11:51:46 AM
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
	abstract public class esSatuSehatReferenceCollection : esEntityCollectionWAuditLog
	{
		public esSatuSehatReferenceCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "SatuSehatReferenceCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esSatuSehatReferenceQuery query)
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
			this.InitQuery(query as esSatuSehatReferenceQuery);
		}
		#endregion
			
		virtual public SatuSehatReference DetachEntity(SatuSehatReference entity)
		{
			return base.DetachEntity(entity) as SatuSehatReference;
		}
		
		virtual public SatuSehatReference AttachEntity(SatuSehatReference entity)
		{
			return base.AttachEntity(entity) as SatuSehatReference;
		}
		
		virtual public void Combine(SatuSehatReferenceCollection collection)
		{
			base.Combine(collection);
		}
		
		new public SatuSehatReference this[int index]
		{
			get
			{
				return base[index] as SatuSehatReference;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SatuSehatReference);
		}
	}

	[Serializable]
	abstract public class esSatuSehatReference : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSatuSehatReferenceQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esSatuSehatReference()
		{
		}
	
		public esSatuSehatReference(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String sRSatuSehatRefType, String internalID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRSatuSehatRefType, internalID);
			else
				return LoadByPrimaryKeyStoredProcedure(sRSatuSehatRefType, internalID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sRSatuSehatRefType, String internalID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRSatuSehatRefType, internalID);
			else
				return LoadByPrimaryKeyStoredProcedure(sRSatuSehatRefType, internalID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String sRSatuSehatRefType, String internalID)
		{
			esSatuSehatReferenceQuery query = this.GetDynamicQuery();
			query.Where(query.SRSatuSehatRefType == sRSatuSehatRefType, query.InternalID == internalID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String sRSatuSehatRefType, String internalID)
		{
			esParameters parms = new esParameters();
			parms.Add("SRSatuSehatRefType",sRSatuSehatRefType);
			parms.Add("InternalID",internalID);
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
						case "SRSatuSehatRefType": this.str.SRSatuSehatRefType = (string)value; break;
						case "InternalID": this.str.InternalID = (string)value; break;
						case "ReferenceID": this.str.ReferenceID = (string)value; break;
						case "ReferenceName": this.str.ReferenceName = (string)value; break;
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
		/// Maps to SatuSehatReference.SRSatuSehatRefType
		/// </summary>
		virtual public System.String SRSatuSehatRefType
		{
			get
			{
				return base.GetSystemString(SatuSehatReferenceMetadata.ColumnNames.SRSatuSehatRefType);
			}
			
			set
			{
				base.SetSystemString(SatuSehatReferenceMetadata.ColumnNames.SRSatuSehatRefType, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatReference.InternalID
		/// </summary>
		virtual public System.String InternalID
		{
			get
			{
				return base.GetSystemString(SatuSehatReferenceMetadata.ColumnNames.InternalID);
			}
			
			set
			{
				base.SetSystemString(SatuSehatReferenceMetadata.ColumnNames.InternalID, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatReference.ReferenceID
		/// </summary>
		virtual public System.String ReferenceID
		{
			get
			{
				return base.GetSystemString(SatuSehatReferenceMetadata.ColumnNames.ReferenceID);
			}
			
			set
			{
				base.SetSystemString(SatuSehatReferenceMetadata.ColumnNames.ReferenceID, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatReference.ReferenceName
		/// </summary>
		virtual public System.String ReferenceName
		{
			get
			{
				return base.GetSystemString(SatuSehatReferenceMetadata.ColumnNames.ReferenceName);
			}
			
			set
			{
				base.SetSystemString(SatuSehatReferenceMetadata.ColumnNames.ReferenceName, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatReference.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SatuSehatReferenceMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(SatuSehatReferenceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatReference.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SatuSehatReferenceMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(SatuSehatReferenceMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSatuSehatReference entity)
			{
				this.entity = entity;
			}
			public System.String SRSatuSehatRefType
			{
				get
				{
					System.String data = entity.SRSatuSehatRefType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRSatuSehatRefType = null;
					else entity.SRSatuSehatRefType = Convert.ToString(value);
				}
			}
			public System.String InternalID
			{
				get
				{
					System.String data = entity.InternalID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InternalID = null;
					else entity.InternalID = Convert.ToString(value);
				}
			}
			public System.String ReferenceID
			{
				get
				{
					System.String data = entity.ReferenceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceID = null;
					else entity.ReferenceID = Convert.ToString(value);
				}
			}
			public System.String ReferenceName
			{
				get
				{
					System.String data = entity.ReferenceName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceName = null;
					else entity.ReferenceName = Convert.ToString(value);
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
			private esSatuSehatReference entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSatuSehatReferenceQuery query)
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
				throw new Exception("esSatuSehatReference can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SatuSehatReference : esSatuSehatReference
	{	
	}

	[Serializable]
	abstract public class esSatuSehatReferenceQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return SatuSehatReferenceMetadata.Meta();
			}
		}	
			
		public esQueryItem SRSatuSehatRefType
		{
			get
			{
				return new esQueryItem(this, SatuSehatReferenceMetadata.ColumnNames.SRSatuSehatRefType, esSystemType.String);
			}
		} 
			
		public esQueryItem InternalID
		{
			get
			{
				return new esQueryItem(this, SatuSehatReferenceMetadata.ColumnNames.InternalID, esSystemType.String);
			}
		} 
			
		public esQueryItem ReferenceID
		{
			get
			{
				return new esQueryItem(this, SatuSehatReferenceMetadata.ColumnNames.ReferenceID, esSystemType.String);
			}
		} 
			
		public esQueryItem ReferenceName
		{
			get
			{
				return new esQueryItem(this, SatuSehatReferenceMetadata.ColumnNames.ReferenceName, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SatuSehatReferenceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SatuSehatReferenceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SatuSehatReferenceCollection")]
	public partial class SatuSehatReferenceCollection : esSatuSehatReferenceCollection, IEnumerable< SatuSehatReference>
	{
		public SatuSehatReferenceCollection()
		{

		}	
		
		public static implicit operator List< SatuSehatReference>(SatuSehatReferenceCollection coll)
		{
			List< SatuSehatReference> list = new List< SatuSehatReference>();
			
			foreach (SatuSehatReference emp in coll)
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
				return  SatuSehatReferenceMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SatuSehatReferenceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SatuSehatReference(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SatuSehatReference();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public SatuSehatReferenceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SatuSehatReferenceQuery();
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
		public bool Load(SatuSehatReferenceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SatuSehatReference AddNew()
		{
			SatuSehatReference entity = base.AddNewEntity() as SatuSehatReference;
			
			return entity;		
		}
		public SatuSehatReference FindByPrimaryKey(String sRSatuSehatRefType, String internalID)
		{
			return base.FindByPrimaryKey(sRSatuSehatRefType, internalID) as SatuSehatReference;
		}

		#region IEnumerable< SatuSehatReference> Members

		IEnumerator< SatuSehatReference> IEnumerable< SatuSehatReference>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as SatuSehatReference;
			}
		}

		#endregion
		
		private SatuSehatReferenceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SatuSehatReference' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SatuSehatReference ({SRSatuSehatRefType, InternalID})")]
	[Serializable]
	public partial class SatuSehatReference : esSatuSehatReference
	{
		public SatuSehatReference()
		{
		}	
	
		public SatuSehatReference(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SatuSehatReferenceMetadata.Meta();
			}
		}	
	
		override protected esSatuSehatReferenceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SatuSehatReferenceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public SatuSehatReferenceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SatuSehatReferenceQuery();
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
		public bool Load(SatuSehatReferenceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private SatuSehatReferenceQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SatuSehatReferenceQuery : esSatuSehatReferenceQuery
	{
		public SatuSehatReferenceQuery()
		{

		}		
		
		public SatuSehatReferenceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "SatuSehatReferenceQuery";
        }
	}

	[Serializable]
	public partial class SatuSehatReferenceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SatuSehatReferenceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(SatuSehatReferenceMetadata.ColumnNames.SRSatuSehatRefType, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = SatuSehatReferenceMetadata.PropertyNames.SRSatuSehatRefType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c); 
				
			c = new esColumnMetadata(SatuSehatReferenceMetadata.ColumnNames.InternalID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SatuSehatReferenceMetadata.PropertyNames.InternalID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			_columns.Add(c); 
				
			c = new esColumnMetadata(SatuSehatReferenceMetadata.ColumnNames.ReferenceID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = SatuSehatReferenceMetadata.PropertyNames.ReferenceID;
			c.CharacterMaxLength = 30;
			_columns.Add(c); 
				
			c = new esColumnMetadata(SatuSehatReferenceMetadata.ColumnNames.ReferenceName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = SatuSehatReferenceMetadata.PropertyNames.ReferenceName;
			c.CharacterMaxLength = 200;
			_columns.Add(c); 
				
			c = new esColumnMetadata(SatuSehatReferenceMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SatuSehatReferenceMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(SatuSehatReferenceMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = SatuSehatReferenceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public SatuSehatReferenceMetadata Meta()
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
			public const string SRSatuSehatRefType = "SRSatuSehatRefType";
			public const string InternalID = "InternalID";
			public const string ReferenceID = "ReferenceID";
			public const string ReferenceName = "ReferenceName";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string SRSatuSehatRefType = "SRSatuSehatRefType";
			public const string InternalID = "InternalID";
			public const string ReferenceID = "ReferenceID";
			public const string ReferenceName = "ReferenceName";
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
			lock (typeof(SatuSehatReferenceMetadata))
			{
				if(SatuSehatReferenceMetadata.mapDelegates == null)
				{
					SatuSehatReferenceMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (SatuSehatReferenceMetadata.meta == null)
				{
					SatuSehatReferenceMetadata.meta = new SatuSehatReferenceMetadata();
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
				
				meta.AddTypeMap("SRSatuSehatRefType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InternalID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "SatuSehatReference";
				meta.Destination = "SatuSehatReference";
				meta.spInsert = "proc_SatuSehatReferenceInsert";				
				meta.spUpdate = "proc_SatuSehatReferenceUpdate";		
				meta.spDelete = "proc_SatuSehatReferenceDelete";
				meta.spLoadAll = "proc_SatuSehatReferenceLoadAll";
				meta.spLoadByPrimaryKey = "proc_SatuSehatReferenceLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SatuSehatReferenceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
