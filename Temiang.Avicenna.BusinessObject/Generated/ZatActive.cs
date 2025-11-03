/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/23/2021 9:06:51 PM
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
	abstract public class esZatActiveCollection : esEntityCollectionWAuditLog
	{
		public esZatActiveCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ZatActiveCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esZatActiveQuery query)
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
			this.InitQuery(query as esZatActiveQuery);
		}
		#endregion
			
		virtual public ZatActive DetachEntity(ZatActive entity)
		{
			return base.DetachEntity(entity) as ZatActive;
		}
		
		virtual public ZatActive AttachEntity(ZatActive entity)
		{
			return base.AttachEntity(entity) as ZatActive;
		}
		
		virtual public void Combine(ZatActiveCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ZatActive this[int index]
		{
			get
			{
				return base[index] as ZatActive;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ZatActive);
		}
	}

	[Serializable]
	abstract public class esZatActive : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esZatActiveQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esZatActive()
		{
		}
	
		public esZatActive(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String zatActiveID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(zatActiveID);
			else
				return LoadByPrimaryKeyStoredProcedure(zatActiveID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String zatActiveID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(zatActiveID);
			else
				return LoadByPrimaryKeyStoredProcedure(zatActiveID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String zatActiveID)
		{
			esZatActiveQuery query = this.GetDynamicQuery();
			query.Where(query.ZatActiveID == zatActiveID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String zatActiveID)
		{
			esParameters parms = new esParameters();
			parms.Add("ZatActiveID",zatActiveID);
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
						case "ZatActiveName": this.str.ZatActiveName = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "InsertDateTime": this.str.InsertDateTime = (string)value; break;
						case "InsertByUserID": this.str.InsertByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "DddOral": this.str.DddOral = (string)value; break;
						case "DddParenteral": this.str.DddParenteral = (string)value; break;
						case "SRZatActiveGroup": this.str.SRZatActiveGroup = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "InsertDateTime":
						
							if (value == null || value is System.DateTime)
								this.InsertDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "DddOral":
						
							if (value == null || value is System.Decimal)
								this.DddOral = (System.Decimal?)value;
							break;
						case "DddParenteral":
						
							if (value == null || value is System.Decimal)
								this.DddParenteral = (System.Decimal?)value;
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
		/// Maps to ZatActive.ZatActiveID
		/// </summary>
		virtual public System.String ZatActiveID
		{
			get
			{
				return base.GetSystemString(ZatActiveMetadata.ColumnNames.ZatActiveID);
			}
			
			set
			{
				base.SetSystemString(ZatActiveMetadata.ColumnNames.ZatActiveID, value);
			}
		}
		/// <summary>
		/// Maps to ZatActive.ZatActiveName
		/// </summary>
		virtual public System.String ZatActiveName
		{
			get
			{
				return base.GetSystemString(ZatActiveMetadata.ColumnNames.ZatActiveName);
			}
			
			set
			{
				base.SetSystemString(ZatActiveMetadata.ColumnNames.ZatActiveName, value);
			}
		}
		/// <summary>
		/// Maps to ZatActive.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ZatActiveMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(ZatActiveMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to ZatActive.InsertDateTime
		/// </summary>
		virtual public System.DateTime? InsertDateTime
		{
			get
			{
				return base.GetSystemDateTime(ZatActiveMetadata.ColumnNames.InsertDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ZatActiveMetadata.ColumnNames.InsertDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ZatActive.InsertByUserID
		/// </summary>
		virtual public System.String InsertByUserID
		{
			get
			{
				return base.GetSystemString(ZatActiveMetadata.ColumnNames.InsertByUserID);
			}
			
			set
			{
				base.SetSystemString(ZatActiveMetadata.ColumnNames.InsertByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ZatActive.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ZatActiveMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ZatActiveMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ZatActive.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ZatActiveMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ZatActiveMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ZatActive.DddOral
		/// </summary>
		virtual public System.Decimal? DddOral
		{
			get
			{
				return base.GetSystemDecimal(ZatActiveMetadata.ColumnNames.DddOral);
			}
			
			set
			{
				base.SetSystemDecimal(ZatActiveMetadata.ColumnNames.DddOral, value);
			}
		}
		/// <summary>
		/// Maps to ZatActive.DddParenteral
		/// </summary>
		virtual public System.Decimal? DddParenteral
		{
			get
			{
				return base.GetSystemDecimal(ZatActiveMetadata.ColumnNames.DddParenteral);
			}
			
			set
			{
				base.SetSystemDecimal(ZatActiveMetadata.ColumnNames.DddParenteral, value);
			}
		}
		/// <summary>
		/// Maps to ZatActive.SRZatActiveGroup
		/// </summary>
		virtual public System.String SRZatActiveGroup
		{
			get
			{
				return base.GetSystemString(ZatActiveMetadata.ColumnNames.SRZatActiveGroup);
			}
			
			set
			{
				base.SetSystemString(ZatActiveMetadata.ColumnNames.SRZatActiveGroup, value);
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
			public esStrings(esZatActive entity)
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
			public System.String ZatActiveName
			{
				get
				{
					System.String data = entity.ZatActiveName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ZatActiveName = null;
					else entity.ZatActiveName = Convert.ToString(value);
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
			public System.String InsertDateTime
			{
				get
				{
					System.DateTime? data = entity.InsertDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InsertDateTime = null;
					else entity.InsertDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String InsertByUserID
			{
				get
				{
					System.String data = entity.InsertByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InsertByUserID = null;
					else entity.InsertByUserID = Convert.ToString(value);
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
			public System.String DddOral
			{
				get
				{
					System.Decimal? data = entity.DddOral;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DddOral = null;
					else entity.DddOral = Convert.ToDecimal(value);
				}
			}
			public System.String DddParenteral
			{
				get
				{
					System.Decimal? data = entity.DddParenteral;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DddParenteral = null;
					else entity.DddParenteral = Convert.ToDecimal(value);
				}
			}
			public System.String SRZatActiveGroup
			{
				get
				{
					System.String data = entity.SRZatActiveGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRZatActiveGroup = null;
					else entity.SRZatActiveGroup = Convert.ToString(value);
				}
			}
			private esZatActive entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esZatActiveQuery query)
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
				throw new Exception("esZatActive can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ZatActive : esZatActive
	{	
	}

	[Serializable]
	abstract public class esZatActiveQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ZatActiveMetadata.Meta();
			}
		}	
			
		public esQueryItem ZatActiveID
		{
			get
			{
				return new esQueryItem(this, ZatActiveMetadata.ColumnNames.ZatActiveID, esSystemType.String);
			}
		} 
			
		public esQueryItem ZatActiveName
		{
			get
			{
				return new esQueryItem(this, ZatActiveMetadata.ColumnNames.ZatActiveName, esSystemType.String);
			}
		} 
			
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ZatActiveMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem InsertDateTime
		{
			get
			{
				return new esQueryItem(this, ZatActiveMetadata.ColumnNames.InsertDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem InsertByUserID
		{
			get
			{
				return new esQueryItem(this, ZatActiveMetadata.ColumnNames.InsertByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ZatActiveMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ZatActiveMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem DddOral
		{
			get
			{
				return new esQueryItem(this, ZatActiveMetadata.ColumnNames.DddOral, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem DddParenteral
		{
			get
			{
				return new esQueryItem(this, ZatActiveMetadata.ColumnNames.DddParenteral, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem SRZatActiveGroup
		{
			get
			{
				return new esQueryItem(this, ZatActiveMetadata.ColumnNames.SRZatActiveGroup, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ZatActiveCollection")]
	public partial class ZatActiveCollection : esZatActiveCollection, IEnumerable< ZatActive>
	{
		public ZatActiveCollection()
		{

		}	
		
		public static implicit operator List< ZatActive>(ZatActiveCollection coll)
		{
			List< ZatActive> list = new List< ZatActive>();
			
			foreach (ZatActive emp in coll)
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
				return  ZatActiveMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ZatActiveQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ZatActive(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ZatActive();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ZatActiveQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ZatActiveQuery();
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
		public bool Load(ZatActiveQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ZatActive AddNew()
		{
			ZatActive entity = base.AddNewEntity() as ZatActive;
			
			return entity;		
		}
		public ZatActive FindByPrimaryKey(String zatActiveID)
		{
			return base.FindByPrimaryKey(zatActiveID) as ZatActive;
		}

		#region IEnumerable< ZatActive> Members

		IEnumerator< ZatActive> IEnumerable< ZatActive>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ZatActive;
			}
		}

		#endregion
		
		private ZatActiveQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ZatActive' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ZatActive ({ZatActiveID})")]
	[Serializable]
	public partial class ZatActive : esZatActive
	{
		public ZatActive()
		{
		}	
	
		public ZatActive(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ZatActiveMetadata.Meta();
			}
		}	
	
		override protected esZatActiveQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ZatActiveQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ZatActiveQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ZatActiveQuery();
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
		public bool Load(ZatActiveQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ZatActiveQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ZatActiveQuery : esZatActiveQuery
	{
		public ZatActiveQuery()
		{

		}		
		
		public ZatActiveQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ZatActiveQuery";
        }
	}

	[Serializable]
	public partial class ZatActiveMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ZatActiveMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ZatActiveMetadata.ColumnNames.ZatActiveID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ZatActiveMetadata.PropertyNames.ZatActiveID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ZatActiveMetadata.ColumnNames.ZatActiveName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ZatActiveMetadata.PropertyNames.ZatActiveName;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ZatActiveMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ZatActiveMetadata.PropertyNames.IsActive;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ZatActiveMetadata.ColumnNames.InsertDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ZatActiveMetadata.PropertyNames.InsertDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ZatActiveMetadata.ColumnNames.InsertByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ZatActiveMetadata.PropertyNames.InsertByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ZatActiveMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ZatActiveMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ZatActiveMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ZatActiveMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ZatActiveMetadata.ColumnNames.DddOral, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ZatActiveMetadata.PropertyNames.DddOral;
			c.NumericPrecision = 4;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ZatActiveMetadata.ColumnNames.DddParenteral, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ZatActiveMetadata.PropertyNames.DddParenteral;
			c.NumericPrecision = 4;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ZatActiveMetadata.ColumnNames.SRZatActiveGroup, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ZatActiveMetadata.PropertyNames.SRZatActiveGroup;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ZatActiveMetadata Meta()
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
			public const string ZatActiveName = "ZatActiveName";
			public const string IsActive = "IsActive";
			public const string InsertDateTime = "InsertDateTime";
			public const string InsertByUserID = "InsertByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string DddOral = "DddOral";
			public const string DddParenteral = "DddParenteral";
			public const string SRZatActiveGroup = "SRZatActiveGroup";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ZatActiveID = "ZatActiveID";
			public const string ZatActiveName = "ZatActiveName";
			public const string IsActive = "IsActive";
			public const string InsertDateTime = "InsertDateTime";
			public const string InsertByUserID = "InsertByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string DddOral = "DddOral";
			public const string DddParenteral = "DddParenteral";
			public const string SRZatActiveGroup = "SRZatActiveGroup";
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
			lock (typeof(ZatActiveMetadata))
			{
				if(ZatActiveMetadata.mapDelegates == null)
				{
					ZatActiveMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ZatActiveMetadata.meta == null)
				{
					ZatActiveMetadata.meta = new ZatActiveMetadata();
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
				meta.AddTypeMap("ZatActiveName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("InsertDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("InsertByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DddOral", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DddParenteral", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRZatActiveGroup", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "ZatActive";
				meta.Destination = "ZatActive";
				meta.spInsert = "proc_ZatActiveInsert";				
				meta.spUpdate = "proc_ZatActiveUpdate";		
				meta.spDelete = "proc_ZatActiveDelete";
				meta.spLoadAll = "proc_ZatActiveLoadAll";
				meta.spLoadByPrimaryKey = "proc_ZatActiveLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ZatActiveMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
