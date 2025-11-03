/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/15/2021 5:28:00 PM
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
	abstract public class esVehiclesCollection : esEntityCollectionWAuditLog
	{
		public esVehiclesCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "VehiclesCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esVehiclesQuery query)
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
			this.InitQuery(query as esVehiclesQuery);
		}
		#endregion
			
		virtual public Vehicles DetachEntity(Vehicles entity)
		{
			return base.DetachEntity(entity) as Vehicles;
		}
		
		virtual public Vehicles AttachEntity(Vehicles entity)
		{
			return base.AttachEntity(entity) as Vehicles;
		}
		
		virtual public void Combine(VehiclesCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Vehicles this[int index]
		{
			get
			{
				return base[index] as Vehicles;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Vehicles);
		}
	}

	[Serializable]
	abstract public class esVehicles : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVehiclesQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esVehicles()
		{
		}
	
		public esVehicles(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 vehicleID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(vehicleID);
			else
				return LoadByPrimaryKeyStoredProcedure(vehicleID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 vehicleID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(vehicleID);
			else
				return LoadByPrimaryKeyStoredProcedure(vehicleID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 vehicleID)
		{
			esVehiclesQuery query = this.GetDynamicQuery();
			query.Where(query.VehicleID==vehicleID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 vehicleID)
		{
			esParameters parms = new esParameters();
			parms.Add("VehicleID",vehicleID);
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
						case "VehicleID": this.str.VehicleID = (string)value; break;
						case "PlateNo": this.str.PlateNo = (string)value; break;
						case "SRVehicleType": this.str.SRVehicleType = (string)value; break;
						case "SRVehicleStatus": this.str.SRVehicleStatus = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "AssetID": this.str.AssetID = (string)value; break;
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
						case "VehicleID":
						
							if (value == null || value is System.Int32)
								this.VehicleID = (System.Int32?)value;
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
		/// Maps to Vehicles.VehicleID
		/// </summary>
		virtual public System.Int32? VehicleID
		{
			get
			{
				return base.GetSystemInt32(VehiclesMetadata.ColumnNames.VehicleID);
			}
			
			set
			{
				base.SetSystemInt32(VehiclesMetadata.ColumnNames.VehicleID, value);
			}
		}
		/// <summary>
		/// Maps to Vehicles.PlateNo
		/// </summary>
		virtual public System.String PlateNo
		{
			get
			{
				return base.GetSystemString(VehiclesMetadata.ColumnNames.PlateNo);
			}
			
			set
			{
				base.SetSystemString(VehiclesMetadata.ColumnNames.PlateNo, value);
			}
		}
		/// <summary>
		/// Maps to Vehicles.SRVehicleType
		/// </summary>
		virtual public System.String SRVehicleType
		{
			get
			{
				return base.GetSystemString(VehiclesMetadata.ColumnNames.SRVehicleType);
			}
			
			set
			{
				base.SetSystemString(VehiclesMetadata.ColumnNames.SRVehicleType, value);
			}
		}
		/// <summary>
		/// Maps to Vehicles.SRVehicleStatus
		/// </summary>
		virtual public System.String SRVehicleStatus
		{
			get
			{
				return base.GetSystemString(VehiclesMetadata.ColumnNames.SRVehicleStatus);
			}
			
			set
			{
				base.SetSystemString(VehiclesMetadata.ColumnNames.SRVehicleStatus, value);
			}
		}
		/// <summary>
		/// Maps to Vehicles.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(VehiclesMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(VehiclesMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to Vehicles.AssetID
		/// </summary>
		virtual public System.String AssetID
		{
			get
			{
				return base.GetSystemString(VehiclesMetadata.ColumnNames.AssetID);
			}
			
			set
			{
				base.SetSystemString(VehiclesMetadata.ColumnNames.AssetID, value);
			}
		}
		/// <summary>
		/// Maps to Vehicles.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(VehiclesMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(VehiclesMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to Vehicles.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(VehiclesMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(VehiclesMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Vehicles.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(VehiclesMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(VehiclesMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Vehicles.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(VehiclesMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(VehiclesMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Vehicles.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(VehiclesMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(VehiclesMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esVehicles entity)
			{
				this.entity = entity;
			}
			public System.String VehicleID
			{
				get
				{
					System.Int32? data = entity.VehicleID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VehicleID = null;
					else entity.VehicleID = Convert.ToInt32(value);
				}
			}
			public System.String PlateNo
			{
				get
				{
					System.String data = entity.PlateNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PlateNo = null;
					else entity.PlateNo = Convert.ToString(value);
				}
			}
			public System.String SRVehicleType
			{
				get
				{
					System.String data = entity.SRVehicleType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRVehicleType = null;
					else entity.SRVehicleType = Convert.ToString(value);
				}
			}
			public System.String SRVehicleStatus
			{
				get
				{
					System.String data = entity.SRVehicleStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRVehicleStatus = null;
					else entity.SRVehicleStatus = Convert.ToString(value);
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
			public System.String AssetID
			{
				get
				{
					System.String data = entity.AssetID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetID = null;
					else entity.AssetID = Convert.ToString(value);
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
			private esVehicles entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVehiclesQuery query)
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
				throw new Exception("esVehicles can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Vehicles : esVehicles
	{	
	}

	[Serializable]
	abstract public class esVehiclesQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return VehiclesMetadata.Meta();
			}
		}	
			
		public esQueryItem VehicleID
		{
			get
			{
				return new esQueryItem(this, VehiclesMetadata.ColumnNames.VehicleID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem PlateNo
		{
			get
			{
				return new esQueryItem(this, VehiclesMetadata.ColumnNames.PlateNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SRVehicleType
		{
			get
			{
				return new esQueryItem(this, VehiclesMetadata.ColumnNames.SRVehicleType, esSystemType.String);
			}
		} 
			
		public esQueryItem SRVehicleStatus
		{
			get
			{
				return new esQueryItem(this, VehiclesMetadata.ColumnNames.SRVehicleStatus, esSystemType.String);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, VehiclesMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem AssetID
		{
			get
			{
				return new esQueryItem(this, VehiclesMetadata.ColumnNames.AssetID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, VehiclesMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, VehiclesMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, VehiclesMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, VehiclesMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, VehiclesMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VehiclesCollection")]
	public partial class VehiclesCollection : esVehiclesCollection, IEnumerable< Vehicles>
	{
		public VehiclesCollection()
		{

		}	
		
		public static implicit operator List< Vehicles>(VehiclesCollection coll)
		{
			List< Vehicles> list = new List< Vehicles>();
			
			foreach (Vehicles emp in coll)
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
				return  VehiclesMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VehiclesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Vehicles(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Vehicles();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public VehiclesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VehiclesQuery();
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
		public bool Load(VehiclesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Vehicles AddNew()
		{
			Vehicles entity = base.AddNewEntity() as Vehicles;
			
			return entity;		
		}
		public Vehicles FindByPrimaryKey(Int32 vehicleID)
		{
			return base.FindByPrimaryKey(vehicleID) as Vehicles;
		}

		#region IEnumerable< Vehicles> Members

		IEnumerator< Vehicles> IEnumerable< Vehicles>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Vehicles;
			}
		}

		#endregion
		
		private VehiclesQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Vehicles' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Vehicles ({VehicleID})")]
	[Serializable]
	public partial class Vehicles : esVehicles
	{
		public Vehicles()
		{
		}	
	
		public Vehicles(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VehiclesMetadata.Meta();
			}
		}	
	
		override protected esVehiclesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VehiclesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public VehiclesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VehiclesQuery();
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
		public bool Load(VehiclesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private VehiclesQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class VehiclesQuery : esVehiclesQuery
	{
		public VehiclesQuery()
		{

		}		
		
		public VehiclesQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "VehiclesQuery";
        }
	}

	[Serializable]
	public partial class VehiclesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VehiclesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(VehiclesMetadata.ColumnNames.VehicleID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VehiclesMetadata.PropertyNames.VehicleID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VehiclesMetadata.ColumnNames.PlateNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = VehiclesMetadata.PropertyNames.PlateNo;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VehiclesMetadata.ColumnNames.SRVehicleType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = VehiclesMetadata.PropertyNames.SRVehicleType;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VehiclesMetadata.ColumnNames.SRVehicleStatus, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = VehiclesMetadata.PropertyNames.SRVehicleStatus;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VehiclesMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = VehiclesMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 255;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VehiclesMetadata.ColumnNames.AssetID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = VehiclesMetadata.PropertyNames.AssetID;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VehiclesMetadata.ColumnNames.IsActive, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VehiclesMetadata.PropertyNames.IsActive;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VehiclesMetadata.ColumnNames.CreateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = VehiclesMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VehiclesMetadata.ColumnNames.CreateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VehiclesMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VehiclesMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = VehiclesMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VehiclesMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VehiclesMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public VehiclesMetadata Meta()
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
			public const string VehicleID = "VehicleID";
			public const string PlateNo = "PlateNo";
			public const string SRVehicleType = "SRVehicleType";
			public const string SRVehicleStatus = "SRVehicleStatus";
			public const string Notes = "Notes";
			public const string AssetID = "AssetID";
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
			public const string VehicleID = "VehicleID";
			public const string PlateNo = "PlateNo";
			public const string SRVehicleType = "SRVehicleType";
			public const string SRVehicleStatus = "SRVehicleStatus";
			public const string Notes = "Notes";
			public const string AssetID = "AssetID";
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
			lock (typeof(VehiclesMetadata))
			{
				if(VehiclesMetadata.mapDelegates == null)
				{
					VehiclesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VehiclesMetadata.meta == null)
				{
					VehiclesMetadata.meta = new VehiclesMetadata();
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
				
				meta.AddTypeMap("VehicleID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PlateNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRVehicleType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRVehicleStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssetID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "Vehicles";
				meta.Destination = "Vehicles";
				meta.spInsert = "proc_VehiclesInsert";				
				meta.spUpdate = "proc_VehiclesUpdate";		
				meta.spDelete = "proc_VehiclesDelete";
				meta.spLoadAll = "proc_VehiclesLoadAll";
				meta.spLoadByPrimaryKey = "proc_VehiclesLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VehiclesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
