/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/7/2022 11:42:54 AM
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
	abstract public class esRenkinPeriodeCollection : esEntityCollectionWAuditLog
	{
		public esRenkinPeriodeCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "RenkinPeriodeCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esRenkinPeriodeQuery query)
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
			this.InitQuery(query as esRenkinPeriodeQuery);
		}
		#endregion
			
		virtual public RenkinPeriode DetachEntity(RenkinPeriode entity)
		{
			return base.DetachEntity(entity) as RenkinPeriode;
		}
		
		virtual public RenkinPeriode AttachEntity(RenkinPeriode entity)
		{
			return base.AttachEntity(entity) as RenkinPeriode;
		}
		
		virtual public void Combine(RenkinPeriodeCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RenkinPeriode this[int index]
		{
			get
			{
				return base[index] as RenkinPeriode;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RenkinPeriode);
		}
	}

	[Serializable]
	abstract public class esRenkinPeriode : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRenkinPeriodeQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esRenkinPeriode()
		{
		}
	
		public esRenkinPeriode(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 periodeID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(periodeID);
			else
				return LoadByPrimaryKeyStoredProcedure(periodeID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 periodeID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(periodeID);
			else
				return LoadByPrimaryKeyStoredProcedure(periodeID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 periodeID)
		{
			esRenkinPeriodeQuery query = this.GetDynamicQuery();
			query.Where(query.PeriodeID == periodeID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 periodeID)
		{
			esParameters parms = new esParameters();
			parms.Add("PeriodeID",periodeID);
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
						case "PeriodeID": this.str.PeriodeID = (string)value; break;
						case "Tahun": this.str.Tahun = (string)value; break;
						case "PeriodeAwal": this.str.PeriodeAwal = (string)value; break;
						case "PeriodeAkhir": this.str.PeriodeAkhir = (string)value; break;
						case "SRRenkinPeriodeStatus": this.str.SRRenkinPeriodeStatus = (string)value; break;
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
						case "PeriodeID":
						
							if (value == null || value is System.Int32)
								this.PeriodeID = (System.Int32?)value;
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
		/// Maps to RenkinPeriode.PeriodeID
		/// </summary>
		virtual public System.Int32? PeriodeID
		{
			get
			{
				return base.GetSystemInt32(RenkinPeriodeMetadata.ColumnNames.PeriodeID);
			}
			
			set
			{
				base.SetSystemInt32(RenkinPeriodeMetadata.ColumnNames.PeriodeID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinPeriode.Tahun
		/// </summary>
		virtual public System.String Tahun
		{
			get
			{
				return base.GetSystemString(RenkinPeriodeMetadata.ColumnNames.Tahun);
			}
			
			set
			{
				base.SetSystemString(RenkinPeriodeMetadata.ColumnNames.Tahun, value);
			}
		}
		/// <summary>
		/// Maps to RenkinPeriode.PeriodeAwal
		/// </summary>
		virtual public System.String PeriodeAwal
		{
			get
			{
				return base.GetSystemString(RenkinPeriodeMetadata.ColumnNames.PeriodeAwal);
			}
			
			set
			{
				base.SetSystemString(RenkinPeriodeMetadata.ColumnNames.PeriodeAwal, value);
			}
		}
		/// <summary>
		/// Maps to RenkinPeriode.PeriodeAkhir
		/// </summary>
		virtual public System.String PeriodeAkhir
		{
			get
			{
				return base.GetSystemString(RenkinPeriodeMetadata.ColumnNames.PeriodeAkhir);
			}
			
			set
			{
				base.SetSystemString(RenkinPeriodeMetadata.ColumnNames.PeriodeAkhir, value);
			}
		}
		/// <summary>
		/// Maps to RenkinPeriode.SRRenkinPeriodeStatus
		/// </summary>
		virtual public System.String SRRenkinPeriodeStatus
		{
			get
			{
				return base.GetSystemString(RenkinPeriodeMetadata.ColumnNames.SRRenkinPeriodeStatus);
			}
			
			set
			{
				base.SetSystemString(RenkinPeriodeMetadata.ColumnNames.SRRenkinPeriodeStatus, value);
			}
		}
		/// <summary>
		/// Maps to RenkinPeriode.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(RenkinPeriodeMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(RenkinPeriodeMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinPeriode.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RenkinPeriodeMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RenkinPeriodeMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RenkinPeriode.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RenkinPeriodeMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RenkinPeriodeMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinPeriode.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RenkinPeriodeMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RenkinPeriodeMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esRenkinPeriode entity)
			{
				this.entity = entity;
			}
			public System.String PeriodeID
			{
				get
				{
					System.Int32? data = entity.PeriodeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodeID = null;
					else entity.PeriodeID = Convert.ToInt32(value);
				}
			}
			public System.String Tahun
			{
				get
				{
					System.String data = entity.Tahun;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Tahun = null;
					else entity.Tahun = Convert.ToString(value);
				}
			}
			public System.String PeriodeAwal
			{
				get
				{
					System.String data = entity.PeriodeAwal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodeAwal = null;
					else entity.PeriodeAwal = Convert.ToString(value);
				}
			}
			public System.String PeriodeAkhir
			{
				get
				{
					System.String data = entity.PeriodeAkhir;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodeAkhir = null;
					else entity.PeriodeAkhir = Convert.ToString(value);
				}
			}
			public System.String SRRenkinPeriodeStatus
			{
				get
				{
					System.String data = entity.SRRenkinPeriodeStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRenkinPeriodeStatus = null;
					else entity.SRRenkinPeriodeStatus = Convert.ToString(value);
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
			private esRenkinPeriode entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRenkinPeriodeQuery query)
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
				throw new Exception("esRenkinPeriode can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RenkinPeriode : esRenkinPeriode
	{	
	}

	[Serializable]
	abstract public class esRenkinPeriodeQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return RenkinPeriodeMetadata.Meta();
			}
		}	
			
		public esQueryItem PeriodeID
		{
			get
			{
				return new esQueryItem(this, RenkinPeriodeMetadata.ColumnNames.PeriodeID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Tahun
		{
			get
			{
				return new esQueryItem(this, RenkinPeriodeMetadata.ColumnNames.Tahun, esSystemType.String);
			}
		} 
			
		public esQueryItem PeriodeAwal
		{
			get
			{
				return new esQueryItem(this, RenkinPeriodeMetadata.ColumnNames.PeriodeAwal, esSystemType.String);
			}
		} 
			
		public esQueryItem PeriodeAkhir
		{
			get
			{
				return new esQueryItem(this, RenkinPeriodeMetadata.ColumnNames.PeriodeAkhir, esSystemType.String);
			}
		} 
			
		public esQueryItem SRRenkinPeriodeStatus
		{
			get
			{
				return new esQueryItem(this, RenkinPeriodeMetadata.ColumnNames.SRRenkinPeriodeStatus, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, RenkinPeriodeMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, RenkinPeriodeMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RenkinPeriodeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RenkinPeriodeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RenkinPeriodeCollection")]
	public partial class RenkinPeriodeCollection : esRenkinPeriodeCollection, IEnumerable< RenkinPeriode>
	{
		public RenkinPeriodeCollection()
		{

		}	
		
		public static implicit operator List< RenkinPeriode>(RenkinPeriodeCollection coll)
		{
			List< RenkinPeriode> list = new List< RenkinPeriode>();
			
			foreach (RenkinPeriode emp in coll)
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
				return  RenkinPeriodeMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RenkinPeriodeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RenkinPeriode(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RenkinPeriode();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public RenkinPeriodeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RenkinPeriodeQuery();
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
		public bool Load(RenkinPeriodeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RenkinPeriode AddNew()
		{
			RenkinPeriode entity = base.AddNewEntity() as RenkinPeriode;
			
			return entity;		
		}
		public RenkinPeriode FindByPrimaryKey(Int32 periodeID)
		{
			return base.FindByPrimaryKey(periodeID) as RenkinPeriode;
		}

		#region IEnumerable< RenkinPeriode> Members

		IEnumerator< RenkinPeriode> IEnumerable< RenkinPeriode>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RenkinPeriode;
			}
		}

		#endregion
		
		private RenkinPeriodeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RenkinPeriode' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RenkinPeriode ({PeriodeID})")]
	[Serializable]
	public partial class RenkinPeriode : esRenkinPeriode
	{
		public RenkinPeriode()
		{
		}	
	
		public RenkinPeriode(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RenkinPeriodeMetadata.Meta();
			}
		}	
	
		override protected esRenkinPeriodeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RenkinPeriodeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public RenkinPeriodeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RenkinPeriodeQuery();
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
		public bool Load(RenkinPeriodeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private RenkinPeriodeQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RenkinPeriodeQuery : esRenkinPeriodeQuery
	{
		public RenkinPeriodeQuery()
		{

		}		
		
		public RenkinPeriodeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "RenkinPeriodeQuery";
        }
	}

	[Serializable]
	public partial class RenkinPeriodeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RenkinPeriodeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(RenkinPeriodeMetadata.ColumnNames.PeriodeID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinPeriodeMetadata.PropertyNames.PeriodeID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinPeriodeMetadata.ColumnNames.Tahun, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinPeriodeMetadata.PropertyNames.Tahun;
			c.CharacterMaxLength = 4;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinPeriodeMetadata.ColumnNames.PeriodeAwal, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinPeriodeMetadata.PropertyNames.PeriodeAwal;
			c.CharacterMaxLength = 25;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinPeriodeMetadata.ColumnNames.PeriodeAkhir, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinPeriodeMetadata.PropertyNames.PeriodeAkhir;
			c.CharacterMaxLength = 25;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinPeriodeMetadata.ColumnNames.SRRenkinPeriodeStatus, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinPeriodeMetadata.PropertyNames.SRRenkinPeriodeStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinPeriodeMetadata.ColumnNames.CreateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinPeriodeMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinPeriodeMetadata.ColumnNames.CreateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RenkinPeriodeMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinPeriodeMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinPeriodeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinPeriodeMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RenkinPeriodeMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public RenkinPeriodeMetadata Meta()
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
			public const string PeriodeID = "PeriodeID";
			public const string Tahun = "Tahun";
			public const string PeriodeAwal = "PeriodeAwal";
			public const string PeriodeAkhir = "PeriodeAkhir";
			public const string SRRenkinPeriodeStatus = "SRRenkinPeriodeStatus";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string PeriodeID = "PeriodeID";
			public const string Tahun = "Tahun";
			public const string PeriodeAwal = "PeriodeAwal";
			public const string PeriodeAkhir = "PeriodeAkhir";
			public const string SRRenkinPeriodeStatus = "SRRenkinPeriodeStatus";
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
			lock (typeof(RenkinPeriodeMetadata))
			{
				if(RenkinPeriodeMetadata.mapDelegates == null)
				{
					RenkinPeriodeMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RenkinPeriodeMetadata.meta == null)
				{
					RenkinPeriodeMetadata.meta = new RenkinPeriodeMetadata();
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
				
				meta.AddTypeMap("PeriodeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Tahun", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PeriodeAwal", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PeriodeAkhir", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRenkinPeriodeStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "RenkinPeriode";
				meta.Destination = "RenkinPeriode";
				meta.spInsert = "proc_RenkinPeriodeInsert";				
				meta.spUpdate = "proc_RenkinPeriodeUpdate";		
				meta.spDelete = "proc_RenkinPeriodeDelete";
				meta.spLoadAll = "proc_RenkinPeriodeLoadAll";
				meta.spLoadByPrimaryKey = "proc_RenkinPeriodeLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RenkinPeriodeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
