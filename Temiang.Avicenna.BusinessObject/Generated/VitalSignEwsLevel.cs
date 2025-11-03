/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/28/2023 7:50:01 AM
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
	abstract public class esVitalSignEwsLevelCollection : esEntityCollectionWAuditLog
	{
		public esVitalSignEwsLevelCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "VitalSignEwsLevelCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esVitalSignEwsLevelQuery query)
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
			this.InitQuery(query as esVitalSignEwsLevelQuery);
		}
		#endregion
			
		virtual public VitalSignEwsLevel DetachEntity(VitalSignEwsLevel entity)
		{
			return base.DetachEntity(entity) as VitalSignEwsLevel;
		}
		
		virtual public VitalSignEwsLevel AttachEntity(VitalSignEwsLevel entity)
		{
			return base.AttachEntity(entity) as VitalSignEwsLevel;
		}
		
		virtual public void Combine(VitalSignEwsLevelCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VitalSignEwsLevel this[int index]
		{
			get
			{
				return base[index] as VitalSignEwsLevel;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VitalSignEwsLevel);
		}
	}

	[Serializable]
	abstract public class esVitalSignEwsLevel : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVitalSignEwsLevelQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esVitalSignEwsLevel()
		{
		}
	
		public esVitalSignEwsLevel(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String sREwsType, String vitalSignID, Int32 startAgeInDay, Decimal startValue)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sREwsType, vitalSignID, startAgeInDay, startValue);
			else
				return LoadByPrimaryKeyStoredProcedure(sREwsType, vitalSignID, startAgeInDay, startValue);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sREwsType, String vitalSignID, Int32 startAgeInDay, Decimal startValue)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sREwsType, vitalSignID, startAgeInDay, startValue);
			else
				return LoadByPrimaryKeyStoredProcedure(sREwsType, vitalSignID, startAgeInDay, startValue);
		}
	
		private bool LoadByPrimaryKeyDynamic(String sREwsType, String vitalSignID, Int32 startAgeInDay, Decimal startValue)
		{
			esVitalSignEwsLevelQuery query = this.GetDynamicQuery();
			query.Where(query.SREwsType == sREwsType, query.VitalSignID == vitalSignID, query.StartAgeInDay == startAgeInDay, query.StartValue == startValue);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String sREwsType, String vitalSignID, Int32 startAgeInDay, Decimal startValue)
		{
			esParameters parms = new esParameters();
			parms.Add("SREwsType",sREwsType);
			parms.Add("VitalSignID",vitalSignID);
			parms.Add("StartAgeInDay",startAgeInDay);
			parms.Add("StartValue",startValue);
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
						case "SREwsType": this.str.SREwsType = (string)value; break;
						case "VitalSignID": this.str.VitalSignID = (string)value; break;
						case "StartAgeInDay": this.str.StartAgeInDay = (string)value; break;
						case "StartValue": this.str.StartValue = (string)value; break;
						case "EwsLevel": this.str.EwsLevel = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "StartAgeInDay":
						
							if (value == null || value is System.Int32)
								this.StartAgeInDay = (System.Int32?)value;
							break;
						case "StartValue":
						
							if (value == null || value is System.Decimal)
								this.StartValue = (System.Decimal?)value;
							break;
						case "EwsLevel":
						
							if (value == null || value is System.Int32)
								this.EwsLevel = (System.Int32?)value;
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
		/// Maps to VitalSignEwsLevel.SREwsType
		/// </summary>
		virtual public System.String SREwsType
		{
			get
			{
				return base.GetSystemString(VitalSignEwsLevelMetadata.ColumnNames.SREwsType);
			}
			
			set
			{
				base.SetSystemString(VitalSignEwsLevelMetadata.ColumnNames.SREwsType, value);
			}
		}
		/// <summary>
		/// Maps to VitalSignEwsLevel.VitalSignID
		/// </summary>
		virtual public System.String VitalSignID
		{
			get
			{
				return base.GetSystemString(VitalSignEwsLevelMetadata.ColumnNames.VitalSignID);
			}
			
			set
			{
				base.SetSystemString(VitalSignEwsLevelMetadata.ColumnNames.VitalSignID, value);
			}
		}
		/// <summary>
		/// Maps to VitalSignEwsLevel.StartAgeInDay
		/// </summary>
		virtual public System.Int32? StartAgeInDay
		{
			get
			{
				return base.GetSystemInt32(VitalSignEwsLevelMetadata.ColumnNames.StartAgeInDay);
			}
			
			set
			{
				base.SetSystemInt32(VitalSignEwsLevelMetadata.ColumnNames.StartAgeInDay, value);
			}
		}
		/// <summary>
		/// Maps to VitalSignEwsLevel.StartValue
		/// </summary>
		virtual public System.Decimal? StartValue
		{
			get
			{
				return base.GetSystemDecimal(VitalSignEwsLevelMetadata.ColumnNames.StartValue);
			}
			
			set
			{
				base.SetSystemDecimal(VitalSignEwsLevelMetadata.ColumnNames.StartValue, value);
			}
		}
		/// <summary>
		/// Maps to VitalSignEwsLevel.EwsLevel
		/// </summary>
		virtual public System.Int32? EwsLevel
		{
			get
			{
				return base.GetSystemInt32(VitalSignEwsLevelMetadata.ColumnNames.EwsLevel);
			}
			
			set
			{
				base.SetSystemInt32(VitalSignEwsLevelMetadata.ColumnNames.EwsLevel, value);
			}
		}
		/// <summary>
		/// Maps to VitalSignEwsLevel.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(VitalSignEwsLevelMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(VitalSignEwsLevelMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to VitalSignEwsLevel.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(VitalSignEwsLevelMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(VitalSignEwsLevelMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esVitalSignEwsLevel entity)
			{
				this.entity = entity;
			}
			public System.String SREwsType
			{
				get
				{
					System.String data = entity.SREwsType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREwsType = null;
					else entity.SREwsType = Convert.ToString(value);
				}
			}
			public System.String VitalSignID
			{
				get
				{
					System.String data = entity.VitalSignID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VitalSignID = null;
					else entity.VitalSignID = Convert.ToString(value);
				}
			}
			public System.String StartAgeInDay
			{
				get
				{
					System.Int32? data = entity.StartAgeInDay;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartAgeInDay = null;
					else entity.StartAgeInDay = Convert.ToInt32(value);
				}
			}
			public System.String StartValue
			{
				get
				{
					System.Decimal? data = entity.StartValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartValue = null;
					else entity.StartValue = Convert.ToDecimal(value);
				}
			}
			public System.String EwsLevel
			{
				get
				{
					System.Int32? data = entity.EwsLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EwsLevel = null;
					else entity.EwsLevel = Convert.ToInt32(value);
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
			private esVitalSignEwsLevel entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVitalSignEwsLevelQuery query)
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
				throw new Exception("esVitalSignEwsLevel can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class VitalSignEwsLevel : esVitalSignEwsLevel
	{	
	}

	[Serializable]
	abstract public class esVitalSignEwsLevelQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return VitalSignEwsLevelMetadata.Meta();
			}
		}	
			
		public esQueryItem SREwsType
		{
			get
			{
				return new esQueryItem(this, VitalSignEwsLevelMetadata.ColumnNames.SREwsType, esSystemType.String);
			}
		} 
			
		public esQueryItem VitalSignID
		{
			get
			{
				return new esQueryItem(this, VitalSignEwsLevelMetadata.ColumnNames.VitalSignID, esSystemType.String);
			}
		} 
			
		public esQueryItem StartAgeInDay
		{
			get
			{
				return new esQueryItem(this, VitalSignEwsLevelMetadata.ColumnNames.StartAgeInDay, esSystemType.Int32);
			}
		} 
			
		public esQueryItem StartValue
		{
			get
			{
				return new esQueryItem(this, VitalSignEwsLevelMetadata.ColumnNames.StartValue, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem EwsLevel
		{
			get
			{
				return new esQueryItem(this, VitalSignEwsLevelMetadata.ColumnNames.EwsLevel, esSystemType.Int32);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, VitalSignEwsLevelMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, VitalSignEwsLevelMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VitalSignEwsLevelCollection")]
	public partial class VitalSignEwsLevelCollection : esVitalSignEwsLevelCollection, IEnumerable< VitalSignEwsLevel>
	{
		public VitalSignEwsLevelCollection()
		{

		}	
		
		public static implicit operator List< VitalSignEwsLevel>(VitalSignEwsLevelCollection coll)
		{
			List< VitalSignEwsLevel> list = new List< VitalSignEwsLevel>();
			
			foreach (VitalSignEwsLevel emp in coll)
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
				return  VitalSignEwsLevelMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VitalSignEwsLevelQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VitalSignEwsLevel(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VitalSignEwsLevel();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public VitalSignEwsLevelQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VitalSignEwsLevelQuery();
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
		public bool Load(VitalSignEwsLevelQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public VitalSignEwsLevel AddNew()
		{
			VitalSignEwsLevel entity = base.AddNewEntity() as VitalSignEwsLevel;
			
			return entity;		
		}
		public VitalSignEwsLevel FindByPrimaryKey(String sREwsType, String vitalSignID, Int32 startAgeInDay, Decimal startValue)
		{
			return base.FindByPrimaryKey(sREwsType, vitalSignID, startAgeInDay, startValue) as VitalSignEwsLevel;
		}

		#region IEnumerable< VitalSignEwsLevel> Members

		IEnumerator< VitalSignEwsLevel> IEnumerable< VitalSignEwsLevel>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VitalSignEwsLevel;
			}
		}

		#endregion
		
		private VitalSignEwsLevelQuery query;
	}


	/// <summary>
	/// Encapsulates the 'VitalSignEwsLevel' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("VitalSignEwsLevel ({SREwsType, VitalSignID, StartAgeInDay, StartValue})")]
	[Serializable]
	public partial class VitalSignEwsLevel : esVitalSignEwsLevel
	{
		public VitalSignEwsLevel()
		{
		}	
	
		public VitalSignEwsLevel(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VitalSignEwsLevelMetadata.Meta();
			}
		}	
	
		override protected esVitalSignEwsLevelQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VitalSignEwsLevelQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public VitalSignEwsLevelQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VitalSignEwsLevelQuery();
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
		public bool Load(VitalSignEwsLevelQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private VitalSignEwsLevelQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class VitalSignEwsLevelQuery : esVitalSignEwsLevelQuery
	{
		public VitalSignEwsLevelQuery()
		{

		}		
		
		public VitalSignEwsLevelQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "VitalSignEwsLevelQuery";
        }
	}

	[Serializable]
	public partial class VitalSignEwsLevelMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VitalSignEwsLevelMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(VitalSignEwsLevelMetadata.ColumnNames.SREwsType, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VitalSignEwsLevelMetadata.PropertyNames.SREwsType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			c.HasDefault = true;
			c.Default = @"('EWS')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignEwsLevelMetadata.ColumnNames.VitalSignID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = VitalSignEwsLevelMetadata.PropertyNames.VitalSignID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignEwsLevelMetadata.ColumnNames.StartAgeInDay, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VitalSignEwsLevelMetadata.PropertyNames.StartAgeInDay;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignEwsLevelMetadata.ColumnNames.StartValue, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VitalSignEwsLevelMetadata.PropertyNames.StartValue;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 18;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignEwsLevelMetadata.ColumnNames.EwsLevel, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VitalSignEwsLevelMetadata.PropertyNames.EwsLevel;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignEwsLevelMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VitalSignEwsLevelMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(VitalSignEwsLevelMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = VitalSignEwsLevelMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public VitalSignEwsLevelMetadata Meta()
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
			public const string SREwsType = "SREwsType";
			public const string VitalSignID = "VitalSignID";
			public const string StartAgeInDay = "StartAgeInDay";
			public const string StartValue = "StartValue";
			public const string EwsLevel = "EwsLevel";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string SREwsType = "SREwsType";
			public const string VitalSignID = "VitalSignID";
			public const string StartAgeInDay = "StartAgeInDay";
			public const string StartValue = "StartValue";
			public const string EwsLevel = "EwsLevel";
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
			lock (typeof(VitalSignEwsLevelMetadata))
			{
				if(VitalSignEwsLevelMetadata.mapDelegates == null)
				{
					VitalSignEwsLevelMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VitalSignEwsLevelMetadata.meta == null)
				{
					VitalSignEwsLevelMetadata.meta = new VitalSignEwsLevelMetadata();
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
				
				meta.AddTypeMap("SREwsType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VitalSignID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartAgeInDay", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("StartValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("EwsLevel", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "VitalSignEwsLevel";
				meta.Destination = "VitalSignEwsLevel";
				meta.spInsert = "proc_VitalSignEwsLevelInsert";				
				meta.spUpdate = "proc_VitalSignEwsLevelUpdate";		
				meta.spDelete = "proc_VitalSignEwsLevelDelete";
				meta.spLoadAll = "proc_VitalSignEwsLevelLoadAll";
				meta.spLoadByPrimaryKey = "proc_VitalSignEwsLevelLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VitalSignEwsLevelMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
