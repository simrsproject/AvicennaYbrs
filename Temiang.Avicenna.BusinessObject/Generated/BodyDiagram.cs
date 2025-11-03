/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/26/2023 4:52:41 PM
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
	abstract public class esBodyDiagramCollection : esEntityCollectionWAuditLog
	{
		public esBodyDiagramCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "BodyDiagramCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esBodyDiagramQuery query)
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
			this.InitQuery(query as esBodyDiagramQuery);
		}
		#endregion
			
		virtual public BodyDiagram DetachEntity(BodyDiagram entity)
		{
			return base.DetachEntity(entity) as BodyDiagram;
		}
		
		virtual public BodyDiagram AttachEntity(BodyDiagram entity)
		{
			return base.AttachEntity(entity) as BodyDiagram;
		}
		
		virtual public void Combine(BodyDiagramCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BodyDiagram this[int index]
		{
			get
			{
				return base[index] as BodyDiagram;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BodyDiagram);
		}
	}

	[Serializable]
	abstract public class esBodyDiagram : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBodyDiagramQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esBodyDiagram()
		{
		}
	
		public esBodyDiagram(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String bodyID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bodyID);
			else
				return LoadByPrimaryKeyStoredProcedure(bodyID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String bodyID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bodyID);
			else
				return LoadByPrimaryKeyStoredProcedure(bodyID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String bodyID)
		{
			esBodyDiagramQuery query = this.GetDynamicQuery();
			query.Where(query.BodyID == bodyID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String bodyID)
		{
			esParameters parms = new esParameters();
			parms.Add("BodyID",bodyID);
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
						case "BodyID": this.str.BodyID = (string)value; break;
						case "BodyName": this.str.BodyName = (string)value; break;
						case "Description": this.str.Description = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ReferenceIDs": this.str.ReferenceIDs = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "BodyImage":
						
							if (value == null || value is System.Byte[])
								this.BodyImage = (System.Byte[])value;
							break;
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to BodyDiagram.BodyID
		/// </summary>
		virtual public System.String BodyID
		{
			get
			{
				return base.GetSystemString(BodyDiagramMetadata.ColumnNames.BodyID);
			}
			
			set
			{
				base.SetSystemString(BodyDiagramMetadata.ColumnNames.BodyID, value);
			}
		}
		/// <summary>
		/// Maps to BodyDiagram.BodyName
		/// </summary>
		virtual public System.String BodyName
		{
			get
			{
				return base.GetSystemString(BodyDiagramMetadata.ColumnNames.BodyName);
			}
			
			set
			{
				base.SetSystemString(BodyDiagramMetadata.ColumnNames.BodyName, value);
			}
		}
		/// <summary>
		/// Maps to BodyDiagram.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(BodyDiagramMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(BodyDiagramMetadata.ColumnNames.Description, value);
			}
		}
		/// <summary>
		/// Maps to BodyDiagram.BodyImage
		/// </summary>
		virtual public System.Byte[] BodyImage
		{
			get
			{
				return base.GetSystemByteArray(BodyDiagramMetadata.ColumnNames.BodyImage);
			}
			
			set
			{
				base.SetSystemByteArray(BodyDiagramMetadata.ColumnNames.BodyImage, value);
			}
		}
		/// <summary>
		/// Maps to BodyDiagram.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(BodyDiagramMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(BodyDiagramMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to BodyDiagram.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BodyDiagramMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BodyDiagramMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BodyDiagram.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BodyDiagramMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BodyDiagramMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BodyDiagram.ReferenceIDs
		/// </summary>
		virtual public System.String ReferenceIDs
		{
			get
			{
				return base.GetSystemString(BodyDiagramMetadata.ColumnNames.ReferenceIDs);
			}
			
			set
			{
				base.SetSystemString(BodyDiagramMetadata.ColumnNames.ReferenceIDs, value);
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
			public esStrings(esBodyDiagram entity)
			{
				this.entity = entity;
			}
			public System.String BodyID
			{
				get
				{
					System.String data = entity.BodyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BodyID = null;
					else entity.BodyID = Convert.ToString(value);
				}
			}
			public System.String BodyName
			{
				get
				{
					System.String data = entity.BodyName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BodyName = null;
					else entity.BodyName = Convert.ToString(value);
				}
			}
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
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
			public System.String ReferenceIDs
			{
				get
				{
					System.String data = entity.ReferenceIDs;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceIDs = null;
					else entity.ReferenceIDs = Convert.ToString(value);
				}
			}
			private esBodyDiagram entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBodyDiagramQuery query)
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
				throw new Exception("esBodyDiagram can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class BodyDiagram : esBodyDiagram
	{	
	}

	[Serializable]
	abstract public class esBodyDiagramQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return BodyDiagramMetadata.Meta();
			}
		}	
			
		public esQueryItem BodyID
		{
			get
			{
				return new esQueryItem(this, BodyDiagramMetadata.ColumnNames.BodyID, esSystemType.String);
			}
		} 
			
		public esQueryItem BodyName
		{
			get
			{
				return new esQueryItem(this, BodyDiagramMetadata.ColumnNames.BodyName, esSystemType.String);
			}
		} 
			
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, BodyDiagramMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
			
		public esQueryItem BodyImage
		{
			get
			{
				return new esQueryItem(this, BodyDiagramMetadata.ColumnNames.BodyImage, esSystemType.ByteArray);
			}
		} 
			
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, BodyDiagramMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BodyDiagramMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BodyDiagramMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem ReferenceIDs
		{
			get
			{
				return new esQueryItem(this, BodyDiagramMetadata.ColumnNames.ReferenceIDs, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BodyDiagramCollection")]
	public partial class BodyDiagramCollection : esBodyDiagramCollection, IEnumerable< BodyDiagram>
	{
		public BodyDiagramCollection()
		{

		}	
		
		public static implicit operator List< BodyDiagram>(BodyDiagramCollection coll)
		{
			List< BodyDiagram> list = new List< BodyDiagram>();
			
			foreach (BodyDiagram emp in coll)
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
				return  BodyDiagramMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BodyDiagramQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BodyDiagram(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BodyDiagram();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public BodyDiagramQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BodyDiagramQuery();
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
		public bool Load(BodyDiagramQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public BodyDiagram AddNew()
		{
			BodyDiagram entity = base.AddNewEntity() as BodyDiagram;
			
			return entity;		
		}
		public BodyDiagram FindByPrimaryKey(String bodyID)
		{
			return base.FindByPrimaryKey(bodyID) as BodyDiagram;
		}

		#region IEnumerable< BodyDiagram> Members

		IEnumerator< BodyDiagram> IEnumerable< BodyDiagram>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BodyDiagram;
			}
		}

		#endregion
		
		private BodyDiagramQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BodyDiagram' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("BodyDiagram ({BodyID})")]
	[Serializable]
	public partial class BodyDiagram : esBodyDiagram
	{
		public BodyDiagram()
		{
		}	
	
		public BodyDiagram(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BodyDiagramMetadata.Meta();
			}
		}	
	
		override protected esBodyDiagramQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BodyDiagramQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public BodyDiagramQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BodyDiagramQuery();
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
		public bool Load(BodyDiagramQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private BodyDiagramQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BodyDiagramQuery : esBodyDiagramQuery
	{
		public BodyDiagramQuery()
		{

		}		
		
		public BodyDiagramQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "BodyDiagramQuery";
        }
	}

	[Serializable]
	public partial class BodyDiagramMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BodyDiagramMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(BodyDiagramMetadata.ColumnNames.BodyID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = BodyDiagramMetadata.PropertyNames.BodyID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BodyDiagramMetadata.ColumnNames.BodyName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = BodyDiagramMetadata.PropertyNames.BodyName;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BodyDiagramMetadata.ColumnNames.Description, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = BodyDiagramMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BodyDiagramMetadata.ColumnNames.BodyImage, 3, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = BodyDiagramMetadata.PropertyNames.BodyImage;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BodyDiagramMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BodyDiagramMetadata.PropertyNames.IsActive;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BodyDiagramMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BodyDiagramMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BodyDiagramMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = BodyDiagramMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BodyDiagramMetadata.ColumnNames.ReferenceIDs, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = BodyDiagramMetadata.PropertyNames.ReferenceIDs;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public BodyDiagramMetadata Meta()
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
			public const string BodyID = "BodyID";
			public const string BodyName = "BodyName";
			public const string Description = "Description";
			public const string BodyImage = "BodyImage";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ReferenceIDs = "ReferenceIDs";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string BodyID = "BodyID";
			public const string BodyName = "BodyName";
			public const string Description = "Description";
			public const string BodyImage = "BodyImage";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ReferenceIDs = "ReferenceIDs";
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
			lock (typeof(BodyDiagramMetadata))
			{
				if(BodyDiagramMetadata.mapDelegates == null)
				{
					BodyDiagramMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BodyDiagramMetadata.meta == null)
				{
					BodyDiagramMetadata.meta = new BodyDiagramMetadata();
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
				
				meta.AddTypeMap("BodyID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BodyName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BodyImage", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceIDs", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "BodyDiagram";
				meta.Destination = "BodyDiagram";
				meta.spInsert = "proc_BodyDiagramInsert";				
				meta.spUpdate = "proc_BodyDiagramUpdate";		
				meta.spDelete = "proc_BodyDiagramDelete";
				meta.spLoadAll = "proc_BodyDiagramLoadAll";
				meta.spLoadByPrimaryKey = "proc_BodyDiagramLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BodyDiagramMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
