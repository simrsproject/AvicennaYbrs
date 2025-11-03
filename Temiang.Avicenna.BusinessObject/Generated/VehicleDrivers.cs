/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/16/2021 11:10:41 AM
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
	abstract public class esVehicleDriversCollection : esEntityCollectionWAuditLog
	{
		public esVehicleDriversCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "VehicleDriversCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esVehicleDriversQuery query)
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
			this.InitQuery(query as esVehicleDriversQuery);
		}
		#endregion
			
		virtual public VehicleDrivers DetachEntity(VehicleDrivers entity)
		{
			return base.DetachEntity(entity) as VehicleDrivers;
		}
		
		virtual public VehicleDrivers AttachEntity(VehicleDrivers entity)
		{
			return base.AttachEntity(entity) as VehicleDrivers;
		}
		
		virtual public void Combine(VehicleDriversCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VehicleDrivers this[int index]
		{
			get
			{
				return base[index] as VehicleDrivers;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VehicleDrivers);
		}
	}

	[Serializable]
	abstract public class esVehicleDrivers : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVehicleDriversQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esVehicleDrivers()
		{
		}
	
		public esVehicleDrivers(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 driverID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(driverID);
			else
				return LoadByPrimaryKeyStoredProcedure(driverID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 driverID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(driverID);
			else
				return LoadByPrimaryKeyStoredProcedure(driverID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 driverID)
		{
			esVehicleDriversQuery query = this.GetDynamicQuery();
			query.Where(query.DriverID==driverID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 driverID)
		{
			esParameters parms = new esParameters();
			parms.Add("DriverID",driverID);
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
						case "DriverID": this.str.DriverID = (string)value; break;
						case "DriverName": this.str.DriverName = (string)value; break;
						case "SRDriverStatus": this.str.SRDriverStatus = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "DriverID":
						
							if (value == null || value is System.Int32)
								this.DriverID = (System.Int32?)value;
							break;
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
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
		/// Maps to VehicleDrivers.DriverID
		/// </summary>
		virtual public System.Int32? DriverID
		{
			get
			{
				return base.GetSystemInt32(VehicleDriversMetadata.ColumnNames.DriverID);
			}
			
			set
			{
				base.SetSystemInt32(VehicleDriversMetadata.ColumnNames.DriverID, value);
			}
		}
		/// <summary>
		/// Maps to VehicleDrivers.DriverName
		/// </summary>
		virtual public System.String DriverName
		{
			get
			{
				return base.GetSystemString(VehicleDriversMetadata.ColumnNames.DriverName);
			}
			
			set
			{
				base.SetSystemString(VehicleDriversMetadata.ColumnNames.DriverName, value);
			}
		}
		/// <summary>
		/// Maps to VehicleDrivers.SRDriverStatus
		/// </summary>
		virtual public System.String SRDriverStatus
		{
			get
			{
				return base.GetSystemString(VehicleDriversMetadata.ColumnNames.SRDriverStatus);
			}
			
			set
			{
				base.SetSystemString(VehicleDriversMetadata.ColumnNames.SRDriverStatus, value);
			}
		}
		/// <summary>
		/// Maps to VehicleDrivers.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(VehicleDriversMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(VehicleDriversMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to VehicleDrivers.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(VehicleDriversMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(VehicleDriversMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to VehicleDrivers.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(VehicleDriversMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(VehicleDriversMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to VehicleDrivers.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(VehicleDriversMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(VehicleDriversMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to VehicleDrivers.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(VehicleDriversMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(VehicleDriversMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to VehicleDrivers.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(VehicleDriversMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(VehicleDriversMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esVehicleDrivers entity)
			{
				this.entity = entity;
			}
			public System.String DriverID
			{
				get
				{
					System.Int32? data = entity.DriverID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DriverID = null;
					else entity.DriverID = Convert.ToInt32(value);
				}
			}
			public System.String DriverName
			{
				get
				{
					System.String data = entity.DriverName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DriverName = null;
					else entity.DriverName = Convert.ToString(value);
				}
			}
			public System.String SRDriverStatus
			{
				get
				{
					System.String data = entity.SRDriverStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDriverStatus = null;
					else entity.SRDriverStatus = Convert.ToString(value);
				}
			}
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
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
			private esVehicleDrivers entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVehicleDriversQuery query)
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
				throw new Exception("esVehicleDrivers can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class VehicleDrivers : esVehicleDrivers
	{	
	}

	[Serializable]
	abstract public class esVehicleDriversQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return VehicleDriversMetadata.Meta();
			}
		}	
			
		public esQueryItem DriverID
		{
			get
			{
				return new esQueryItem(this, VehicleDriversMetadata.ColumnNames.DriverID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem DriverName
		{
			get
			{
				return new esQueryItem(this, VehicleDriversMetadata.ColumnNames.DriverName, esSystemType.String);
			}
		} 
			
		public esQueryItem SRDriverStatus
		{
			get
			{
				return new esQueryItem(this, VehicleDriversMetadata.ColumnNames.SRDriverStatus, esSystemType.String);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, VehicleDriversMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, VehicleDriversMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, VehicleDriversMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, VehicleDriversMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, VehicleDriversMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, VehicleDriversMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VehicleDriversCollection")]
	public partial class VehicleDriversCollection : esVehicleDriversCollection, IEnumerable< VehicleDrivers>
	{
		public VehicleDriversCollection()
		{

		}	
		
		public static implicit operator List< VehicleDrivers>(VehicleDriversCollection coll)
		{
			List< VehicleDrivers> list = new List< VehicleDrivers>();
			
			foreach (VehicleDrivers emp in coll)
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
				return  VehicleDriversMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VehicleDriversQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VehicleDrivers(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VehicleDrivers();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public VehicleDriversQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VehicleDriversQuery();
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
		public bool Load(VehicleDriversQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public VehicleDrivers AddNew()
		{
			VehicleDrivers entity = base.AddNewEntity() as VehicleDrivers;
			
			return entity;		
		}
		public VehicleDrivers FindByPrimaryKey(Int32 driverID)
		{
			return base.FindByPrimaryKey(driverID) as VehicleDrivers;
		}

		#region IEnumerable< VehicleDrivers> Members

		IEnumerator< VehicleDrivers> IEnumerable< VehicleDrivers>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VehicleDrivers;
			}
		}

		#endregion
		
		private VehicleDriversQuery query;
	}


	/// <summary>
	/// Encapsulates the 'VehicleDrivers' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("VehicleDrivers ({DriverID})")]
	[Serializable]
	public partial class VehicleDrivers : esVehicleDrivers
	{
		public VehicleDrivers()
		{
		}	
	
		public VehicleDrivers(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VehicleDriversMetadata.Meta();
			}
		}	
	
		override protected esVehicleDriversQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VehicleDriversQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public VehicleDriversQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VehicleDriversQuery();
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
		public bool Load(VehicleDriversQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private VehicleDriversQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class VehicleDriversQuery : esVehicleDriversQuery
	{
		public VehicleDriversQuery()
		{

		}		
		
		public VehicleDriversQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "VehicleDriversQuery";
        }
	}

	[Serializable]
	public partial class VehicleDriversMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VehicleDriversMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(VehicleDriversMetadata.ColumnNames.DriverID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VehicleDriversMetadata.PropertyNames.DriverID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VehicleDriversMetadata.ColumnNames.DriverName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = VehicleDriversMetadata.PropertyNames.DriverName;
			c.CharacterMaxLength = 100;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VehicleDriversMetadata.ColumnNames.SRDriverStatus, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = VehicleDriversMetadata.PropertyNames.SRDriverStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VehicleDriversMetadata.ColumnNames.Notes, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = VehicleDriversMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 255;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VehicleDriversMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VehicleDriversMetadata.PropertyNames.IsActive;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VehicleDriversMetadata.ColumnNames.CreateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = VehicleDriversMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VehicleDriversMetadata.ColumnNames.CreateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VehicleDriversMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VehicleDriversMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = VehicleDriversMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VehicleDriversMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VehicleDriversMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public VehicleDriversMetadata Meta()
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
			public const string DriverID = "DriverID";
			public const string DriverName = "DriverName";
			public const string SRDriverStatus = "SRDriverStatus";
			public const string Notes = "Notes";
			public const string IsActive = "IsActive";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string DriverID = "DriverID";
			public const string DriverName = "DriverName";
			public const string SRDriverStatus = "SRDriverStatus";
			public const string Notes = "Notes";
			public const string IsActive = "IsActive";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(VehicleDriversMetadata))
			{
				if(VehicleDriversMetadata.mapDelegates == null)
				{
					VehicleDriversMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VehicleDriversMetadata.meta == null)
				{
					VehicleDriversMetadata.meta = new VehicleDriversMetadata();
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
				
				meta.AddTypeMap("DriverID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DriverName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDriverStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "VehicleDrivers";
				meta.Destination = "VehicleDrivers";
				meta.spInsert = "proc_VehicleDriversInsert";				
				meta.spUpdate = "proc_VehicleDriversUpdate";		
				meta.spDelete = "proc_VehicleDriversDelete";
				meta.spLoadAll = "proc_VehicleDriversLoadAll";
				meta.spLoadByPrimaryKey = "proc_VehicleDriversLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VehicleDriversMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
