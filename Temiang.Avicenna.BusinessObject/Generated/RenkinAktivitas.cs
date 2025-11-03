/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/22/2022 10:45:47 AM
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
	abstract public class esRenkinAktivitasCollection : esEntityCollectionWAuditLog
	{
		public esRenkinAktivitasCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "RenkinAktivitasCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esRenkinAktivitasQuery query)
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
			this.InitQuery(query as esRenkinAktivitasQuery);
		}
		#endregion
			
		virtual public RenkinAktivitas DetachEntity(RenkinAktivitas entity)
		{
			return base.DetachEntity(entity) as RenkinAktivitas;
		}
		
		virtual public RenkinAktivitas AttachEntity(RenkinAktivitas entity)
		{
			return base.AttachEntity(entity) as RenkinAktivitas;
		}
		
		virtual public void Combine(RenkinAktivitasCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RenkinAktivitas this[int index]
		{
			get
			{
				return base[index] as RenkinAktivitas;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RenkinAktivitas);
		}
	}

	[Serializable]
	abstract public class esRenkinAktivitas : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRenkinAktivitasQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esRenkinAktivitas()
		{
		}
	
		public esRenkinAktivitas(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 aktivitasID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(aktivitasID);
			else
				return LoadByPrimaryKeyStoredProcedure(aktivitasID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 aktivitasID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(aktivitasID);
			else
				return LoadByPrimaryKeyStoredProcedure(aktivitasID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 aktivitasID)
		{
			esRenkinAktivitasQuery query = this.GetDynamicQuery();
			query.Where(query.AktivitasID == aktivitasID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 aktivitasID)
		{
			esParameters parms = new esParameters();
			parms.Add("AktivitasID",aktivitasID);
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
						case "AktivitasID": this.str.AktivitasID = (string)value; break;
						case "DetailID": this.str.DetailID = (string)value; break;
						case "AktivitasDate": this.str.AktivitasDate = (string)value; break;
						case "AktivitasTimeStart": this.str.AktivitasTimeStart = (string)value; break;
						case "AktivitasTimeEnd": this.str.AktivitasTimeEnd = (string)value; break;
						case "Aktivitas": this.str.Aktivitas = (string)value; break;
						case "Laporan": this.str.Laporan = (string)value; break;
						case "SRRenkinAktivitasStatus": this.str.SRRenkinAktivitasStatus = (string)value; break;
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
						case "AktivitasID":
						
							if (value == null || value is System.Int32)
								this.AktivitasID = (System.Int32?)value;
							break;
						case "DetailID":
						
							if (value == null || value is System.Int32)
								this.DetailID = (System.Int32?)value;
							break;
						case "AktivitasDate":
						
							if (value == null || value is System.DateTime)
								this.AktivitasDate = (System.DateTime?)value;
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
		/// Maps to RenkinAktivitas.AktivitasID
		/// </summary>
		virtual public System.Int32? AktivitasID
		{
			get
			{
				return base.GetSystemInt32(RenkinAktivitasMetadata.ColumnNames.AktivitasID);
			}
			
			set
			{
				base.SetSystemInt32(RenkinAktivitasMetadata.ColumnNames.AktivitasID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinAktivitas.DetailID
		/// </summary>
		virtual public System.Int32? DetailID
		{
			get
			{
				return base.GetSystemInt32(RenkinAktivitasMetadata.ColumnNames.DetailID);
			}
			
			set
			{
				base.SetSystemInt32(RenkinAktivitasMetadata.ColumnNames.DetailID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinAktivitas.AktivitasDate
		/// </summary>
		virtual public System.DateTime? AktivitasDate
		{
			get
			{
				return base.GetSystemDateTime(RenkinAktivitasMetadata.ColumnNames.AktivitasDate);
			}
			
			set
			{
				base.SetSystemDateTime(RenkinAktivitasMetadata.ColumnNames.AktivitasDate, value);
			}
		}
		/// <summary>
		/// Maps to RenkinAktivitas.AktivitasTimeStart
		/// </summary>
		virtual public System.String AktivitasTimeStart
		{
			get
			{
				return base.GetSystemString(RenkinAktivitasMetadata.ColumnNames.AktivitasTimeStart);
			}
			
			set
			{
				base.SetSystemString(RenkinAktivitasMetadata.ColumnNames.AktivitasTimeStart, value);
			}
		}
		/// <summary>
		/// Maps to RenkinAktivitas.AktivitasTimeEnd
		/// </summary>
		virtual public System.String AktivitasTimeEnd
		{
			get
			{
				return base.GetSystemString(RenkinAktivitasMetadata.ColumnNames.AktivitasTimeEnd);
			}
			
			set
			{
				base.SetSystemString(RenkinAktivitasMetadata.ColumnNames.AktivitasTimeEnd, value);
			}
		}
		/// <summary>
		/// Maps to RenkinAktivitas.Aktivitas
		/// </summary>
		virtual public System.String Aktivitas
		{
			get
			{
				return base.GetSystemString(RenkinAktivitasMetadata.ColumnNames.Aktivitas);
			}
			
			set
			{
				base.SetSystemString(RenkinAktivitasMetadata.ColumnNames.Aktivitas, value);
			}
		}
		/// <summary>
		/// Maps to RenkinAktivitas.Laporan
		/// </summary>
		virtual public System.String Laporan
		{
			get
			{
				return base.GetSystemString(RenkinAktivitasMetadata.ColumnNames.Laporan);
			}
			
			set
			{
				base.SetSystemString(RenkinAktivitasMetadata.ColumnNames.Laporan, value);
			}
		}
		/// <summary>
		/// Maps to RenkinAktivitas.SRRenkinAktivitasStatus
		/// </summary>
		virtual public System.String SRRenkinAktivitasStatus
		{
			get
			{
				return base.GetSystemString(RenkinAktivitasMetadata.ColumnNames.SRRenkinAktivitasStatus);
			}
			
			set
			{
				base.SetSystemString(RenkinAktivitasMetadata.ColumnNames.SRRenkinAktivitasStatus, value);
			}
		}
		/// <summary>
		/// Maps to RenkinAktivitas.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(RenkinAktivitasMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(RenkinAktivitasMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinAktivitas.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RenkinAktivitasMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RenkinAktivitasMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RenkinAktivitas.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RenkinAktivitasMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RenkinAktivitasMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinAktivitas.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RenkinAktivitasMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RenkinAktivitasMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esRenkinAktivitas entity)
			{
				this.entity = entity;
			}
			public System.String AktivitasID
			{
				get
				{
					System.Int32? data = entity.AktivitasID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AktivitasID = null;
					else entity.AktivitasID = Convert.ToInt32(value);
				}
			}
			public System.String DetailID
			{
				get
				{
					System.Int32? data = entity.DetailID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DetailID = null;
					else entity.DetailID = Convert.ToInt32(value);
				}
			}
			public System.String AktivitasDate
			{
				get
				{
					System.DateTime? data = entity.AktivitasDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AktivitasDate = null;
					else entity.AktivitasDate = Convert.ToDateTime(value);
				}
			}
			public System.String AktivitasTimeStart
			{
				get
				{
					System.String data = entity.AktivitasTimeStart;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AktivitasTimeStart = null;
					else entity.AktivitasTimeStart = Convert.ToString(value);
				}
			}
			public System.String AktivitasTimeEnd
			{
				get
				{
					System.String data = entity.AktivitasTimeEnd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AktivitasTimeEnd = null;
					else entity.AktivitasTimeEnd = Convert.ToString(value);
				}
			}
			public System.String Aktivitas
			{
				get
				{
					System.String data = entity.Aktivitas;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Aktivitas = null;
					else entity.Aktivitas = Convert.ToString(value);
				}
			}
			public System.String Laporan
			{
				get
				{
					System.String data = entity.Laporan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Laporan = null;
					else entity.Laporan = Convert.ToString(value);
				}
			}
			public System.String SRRenkinAktivitasStatus
			{
				get
				{
					System.String data = entity.SRRenkinAktivitasStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRenkinAktivitasStatus = null;
					else entity.SRRenkinAktivitasStatus = Convert.ToString(value);
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
			private esRenkinAktivitas entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRenkinAktivitasQuery query)
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
				throw new Exception("esRenkinAktivitas can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RenkinAktivitas : esRenkinAktivitas
	{	
	}

	[Serializable]
	abstract public class esRenkinAktivitasQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return RenkinAktivitasMetadata.Meta();
			}
		}	
			
		public esQueryItem AktivitasID
		{
			get
			{
				return new esQueryItem(this, RenkinAktivitasMetadata.ColumnNames.AktivitasID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem DetailID
		{
			get
			{
				return new esQueryItem(this, RenkinAktivitasMetadata.ColumnNames.DetailID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem AktivitasDate
		{
			get
			{
				return new esQueryItem(this, RenkinAktivitasMetadata.ColumnNames.AktivitasDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem AktivitasTimeStart
		{
			get
			{
				return new esQueryItem(this, RenkinAktivitasMetadata.ColumnNames.AktivitasTimeStart, esSystemType.String);
			}
		} 
			
		public esQueryItem AktivitasTimeEnd
		{
			get
			{
				return new esQueryItem(this, RenkinAktivitasMetadata.ColumnNames.AktivitasTimeEnd, esSystemType.String);
			}
		} 
			
		public esQueryItem Aktivitas
		{
			get
			{
				return new esQueryItem(this, RenkinAktivitasMetadata.ColumnNames.Aktivitas, esSystemType.String);
			}
		} 
			
		public esQueryItem Laporan
		{
			get
			{
				return new esQueryItem(this, RenkinAktivitasMetadata.ColumnNames.Laporan, esSystemType.String);
			}
		} 
			
		public esQueryItem SRRenkinAktivitasStatus
		{
			get
			{
				return new esQueryItem(this, RenkinAktivitasMetadata.ColumnNames.SRRenkinAktivitasStatus, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, RenkinAktivitasMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, RenkinAktivitasMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RenkinAktivitasMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RenkinAktivitasMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RenkinAktivitasCollection")]
	public partial class RenkinAktivitasCollection : esRenkinAktivitasCollection, IEnumerable< RenkinAktivitas>
	{
		public RenkinAktivitasCollection()
		{

		}	
		
		public static implicit operator List< RenkinAktivitas>(RenkinAktivitasCollection coll)
		{
			List< RenkinAktivitas> list = new List< RenkinAktivitas>();
			
			foreach (RenkinAktivitas emp in coll)
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
				return  RenkinAktivitasMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RenkinAktivitasQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RenkinAktivitas(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RenkinAktivitas();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public RenkinAktivitasQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RenkinAktivitasQuery();
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
		public bool Load(RenkinAktivitasQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RenkinAktivitas AddNew()
		{
			RenkinAktivitas entity = base.AddNewEntity() as RenkinAktivitas;
			
			return entity;		
		}
		public RenkinAktivitas FindByPrimaryKey(Int32 aktivitasID)
		{
			return base.FindByPrimaryKey(aktivitasID) as RenkinAktivitas;
		}

		#region IEnumerable< RenkinAktivitas> Members

		IEnumerator< RenkinAktivitas> IEnumerable< RenkinAktivitas>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RenkinAktivitas;
			}
		}

        public void LoadByPrimaryKey(int v)
        {
            throw new NotImplementedException();
        }

        #endregion

        private RenkinAktivitasQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RenkinAktivitas' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RenkinAktivitas ({AktivitasID})")]
	[Serializable]
	public partial class RenkinAktivitas : esRenkinAktivitas
	{
		public RenkinAktivitas()
		{
		}	
	
		public RenkinAktivitas(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RenkinAktivitasMetadata.Meta();
			}
		}	
	
		override protected esRenkinAktivitasQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RenkinAktivitasQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public RenkinAktivitasQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RenkinAktivitasQuery();
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
		public bool Load(RenkinAktivitasQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private RenkinAktivitasQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RenkinAktivitasQuery : esRenkinAktivitasQuery
	{
		public RenkinAktivitasQuery()
		{

		}		
		
		public RenkinAktivitasQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "RenkinAktivitasQuery";
        }
	}

	[Serializable]
	public partial class RenkinAktivitasMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RenkinAktivitasMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(RenkinAktivitasMetadata.ColumnNames.AktivitasID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinAktivitasMetadata.PropertyNames.AktivitasID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinAktivitasMetadata.ColumnNames.DetailID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinAktivitasMetadata.PropertyNames.DetailID;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinAktivitasMetadata.ColumnNames.AktivitasDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RenkinAktivitasMetadata.PropertyNames.AktivitasDate;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinAktivitasMetadata.ColumnNames.AktivitasTimeStart, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinAktivitasMetadata.PropertyNames.AktivitasTimeStart;
			c.CharacterMaxLength = 5;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinAktivitasMetadata.ColumnNames.AktivitasTimeEnd, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinAktivitasMetadata.PropertyNames.AktivitasTimeEnd;
			c.CharacterMaxLength = 5;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinAktivitasMetadata.ColumnNames.Aktivitas, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinAktivitasMetadata.PropertyNames.Aktivitas;
			c.CharacterMaxLength = 300;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinAktivitasMetadata.ColumnNames.Laporan, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinAktivitasMetadata.PropertyNames.Laporan;
			c.CharacterMaxLength = 300;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinAktivitasMetadata.ColumnNames.SRRenkinAktivitasStatus, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinAktivitasMetadata.PropertyNames.SRRenkinAktivitasStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinAktivitasMetadata.ColumnNames.CreateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinAktivitasMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinAktivitasMetadata.ColumnNames.CreateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RenkinAktivitasMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinAktivitasMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinAktivitasMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinAktivitasMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RenkinAktivitasMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public RenkinAktivitasMetadata Meta()
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
			public const string AktivitasID = "AktivitasID";
			public const string DetailID = "DetailID";
			public const string AktivitasDate = "AktivitasDate";
			public const string AktivitasTimeStart = "AktivitasTimeStart";
			public const string AktivitasTimeEnd = "AktivitasTimeEnd";
			public const string Aktivitas = "Aktivitas";
			public const string Laporan = "Laporan";
			public const string SRRenkinAktivitasStatus = "SRRenkinAktivitasStatus";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string AktivitasID = "AktivitasID";
			public const string DetailID = "DetailID";
			public const string AktivitasDate = "AktivitasDate";
			public const string AktivitasTimeStart = "AktivitasTimeStart";
			public const string AktivitasTimeEnd = "AktivitasTimeEnd";
			public const string Aktivitas = "Aktivitas";
			public const string Laporan = "Laporan";
			public const string SRRenkinAktivitasStatus = "SRRenkinAktivitasStatus";
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
			lock (typeof(RenkinAktivitasMetadata))
			{
				if(RenkinAktivitasMetadata.mapDelegates == null)
				{
					RenkinAktivitasMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RenkinAktivitasMetadata.meta == null)
				{
					RenkinAktivitasMetadata.meta = new RenkinAktivitasMetadata();
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
				
				meta.AddTypeMap("AktivitasID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DetailID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AktivitasDate", new esTypeMap("date", "System.DateTime"));
				meta.AddTypeMap("AktivitasTimeStart", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AktivitasTimeEnd", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Aktivitas", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Laporan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRenkinAktivitasStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "RenkinAktivitas";
				meta.Destination = "RenkinAktivitas";
				meta.spInsert = "proc_RenkinAktivitasInsert";				
				meta.spUpdate = "proc_RenkinAktivitasUpdate";		
				meta.spDelete = "proc_RenkinAktivitasDelete";
				meta.spLoadAll = "proc_RenkinAktivitasLoadAll";
				meta.spLoadByPrimaryKey = "proc_RenkinAktivitasLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RenkinAktivitasMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
