/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 13/06/2022 14:55:01
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
	abstract public class esRenkinCollection : esEntityCollectionWAuditLog
	{
		public esRenkinCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "RenkinCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esRenkinQuery query)
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
			this.InitQuery(query as esRenkinQuery);
		}
		#endregion
			
		virtual public Renkin DetachEntity(Renkin entity)
		{
			return base.DetachEntity(entity) as Renkin;
		}
		
		virtual public Renkin AttachEntity(Renkin entity)
		{
			return base.AttachEntity(entity) as Renkin;
		}
		
		virtual public void Combine(RenkinCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Renkin this[int index]
		{
			get
			{
				return base[index] as Renkin;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Renkin);
		}
	}

	[Serializable]
	abstract public class esRenkin : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRenkinQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esRenkin()
		{
		}
	
		public esRenkin(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 renkinID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(renkinID);
			else
				return LoadByPrimaryKeyStoredProcedure(renkinID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 renkinID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(renkinID);
			else
				return LoadByPrimaryKeyStoredProcedure(renkinID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 renkinID)
		{
			esRenkinQuery query = this.GetDynamicQuery();
			query.Where(query.RenkinID == renkinID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 renkinID)
		{
			esParameters parms = new esParameters();
			parms.Add("RenkinID",renkinID);
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
						case "RenkinID": this.str.RenkinID = (string)value; break;
						case "PositionID": this.str.PositionID = (string)value; break;
						case "Kegiatan": this.str.Kegiatan = (string)value; break;
						case "SRRenkinJenisKegiatan": this.str.SRRenkinJenisKegiatan = (string)value; break;
						case "TargetPersen": this.str.TargetPersen = (string)value; break;
						case "TargetBulan": this.str.TargetBulan = (string)value; break;
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
						case "RenkinID":
						
							if (value == null || value is System.Int32)
								this.RenkinID = (System.Int32?)value;
							break;
						case "PositionID":
						
							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
							break;
						case "TargetPersen":
						
							if (value == null || value is System.Int32)
								this.TargetPersen = (System.Int32?)value;
							break;
						case "TargetBulan":
						
							if (value == null || value is System.Int32)
								this.TargetBulan = (System.Int32?)value;
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
		/// Maps to Renkin.RenkinID
		/// </summary>
		virtual public System.Int32? RenkinID
		{
			get
			{
				return base.GetSystemInt32(RenkinMetadata.ColumnNames.RenkinID);
			}
			
			set
			{
				base.SetSystemInt32(RenkinMetadata.ColumnNames.RenkinID, value);
			}
		}
		/// <summary>
		/// Maps to Renkin.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(RenkinMetadata.ColumnNames.PositionID);
			}
			
			set
			{
				base.SetSystemInt32(RenkinMetadata.ColumnNames.PositionID, value);
			}
		}
		/// <summary>
		/// Maps to Renkin.Kegiatan
		/// </summary>
		virtual public System.String Kegiatan
		{
			get
			{
				return base.GetSystemString(RenkinMetadata.ColumnNames.Kegiatan);
			}
			
			set
			{
				base.SetSystemString(RenkinMetadata.ColumnNames.Kegiatan, value);
			}
		}
		/// <summary>
		/// Maps to Renkin.SRRenkinJenisKegiatan
		/// </summary>
		virtual public System.String SRRenkinJenisKegiatan
		{
			get
			{
				return base.GetSystemString(RenkinMetadata.ColumnNames.SRRenkinJenisKegiatan);
			}
			
			set
			{
				base.SetSystemString(RenkinMetadata.ColumnNames.SRRenkinJenisKegiatan, value);
			}
		}
		/// <summary>
		/// Maps to Renkin.TargetPersen
		/// </summary>
		virtual public System.Int32? TargetPersen
		{
			get
			{
				return base.GetSystemInt32(RenkinMetadata.ColumnNames.TargetPersen);
			}
			
			set
			{
				base.SetSystemInt32(RenkinMetadata.ColumnNames.TargetPersen, value);
			}
		}
		/// <summary>
		/// Maps to Renkin.TargetBulan
		/// </summary>
		virtual public System.Int32? TargetBulan
		{
			get
			{
				return base.GetSystemInt32(RenkinMetadata.ColumnNames.TargetBulan);
			}
			
			set
			{
				base.SetSystemInt32(RenkinMetadata.ColumnNames.TargetBulan, value);
			}
		}
		/// <summary>
		/// Maps to Renkin.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(RenkinMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(RenkinMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Renkin.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RenkinMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RenkinMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Renkin.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RenkinMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RenkinMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Renkin.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RenkinMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RenkinMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esRenkin entity)
			{
				this.entity = entity;
			}
			public System.String RenkinID
			{
				get
				{
					System.Int32? data = entity.RenkinID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RenkinID = null;
					else entity.RenkinID = Convert.ToInt32(value);
				}
			}
			public System.String PositionID
			{
				get
				{
					System.Int32? data = entity.PositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionID = null;
					else entity.PositionID = Convert.ToInt32(value);
				}
			}
			public System.String Kegiatan
			{
				get
				{
					System.String data = entity.Kegiatan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kegiatan = null;
					else entity.Kegiatan = Convert.ToString(value);
				}
			}
			public System.String SRRenkinJenisKegiatan
			{
				get
				{
					System.String data = entity.SRRenkinJenisKegiatan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRenkinJenisKegiatan = null;
					else entity.SRRenkinJenisKegiatan = Convert.ToString(value);
				}
			}
			public System.String TargetPersen
			{
				get
				{
					System.Int32? data = entity.TargetPersen;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TargetPersen = null;
					else entity.TargetPersen = Convert.ToInt32(value);
				}
			}
			public System.String TargetBulan
			{
				get
				{
					System.Int32? data = entity.TargetBulan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TargetBulan = null;
					else entity.TargetBulan = Convert.ToInt32(value);
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
			private esRenkin entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRenkinQuery query)
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
				throw new Exception("esRenkin can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Renkin : esRenkin
	{	
	}

	[Serializable]
	abstract public class esRenkinQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return RenkinMetadata.Meta();
			}
		}	
			
		public esQueryItem RenkinID
		{
			get
			{
				return new esQueryItem(this, RenkinMetadata.ColumnNames.RenkinID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, RenkinMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Kegiatan
		{
			get
			{
				return new esQueryItem(this, RenkinMetadata.ColumnNames.Kegiatan, esSystemType.String);
			}
		} 
			
		public esQueryItem SRRenkinJenisKegiatan
		{
			get
			{
				return new esQueryItem(this, RenkinMetadata.ColumnNames.SRRenkinJenisKegiatan, esSystemType.String);
			}
		} 
			
		public esQueryItem TargetPersen
		{
			get
			{
				return new esQueryItem(this, RenkinMetadata.ColumnNames.TargetPersen, esSystemType.Int32);
			}
		} 
			
		public esQueryItem TargetBulan
		{
			get
			{
				return new esQueryItem(this, RenkinMetadata.ColumnNames.TargetBulan, esSystemType.Int32);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, RenkinMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, RenkinMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RenkinMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RenkinMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RenkinCollection")]
	public partial class RenkinCollection : esRenkinCollection, IEnumerable< Renkin>
	{
		public RenkinCollection()
		{

		}	
		
		public static implicit operator List< Renkin>(RenkinCollection coll)
		{
			List< Renkin> list = new List< Renkin>();
			
			foreach (Renkin emp in coll)
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
				return  RenkinMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RenkinQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Renkin(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Renkin();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public RenkinQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RenkinQuery();
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
		public bool Load(RenkinQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Renkin AddNew()
		{
			Renkin entity = base.AddNewEntity() as Renkin;
			
			return entity;		
		}
		public Renkin FindByPrimaryKey(Int32 renkinID)
		{
			return base.FindByPrimaryKey(renkinID) as Renkin;
		}

		#region IEnumerable< Renkin> Members

		IEnumerator< Renkin> IEnumerable< Renkin>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Renkin;
			}
		}

		#endregion
		
		private RenkinQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Renkin' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Renkin ({RenkinID})")]
	[Serializable]
	public partial class Renkin : esRenkin
	{
		public Renkin()
		{
		}	
	
		public Renkin(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RenkinMetadata.Meta();
			}
		}	
	
		override protected esRenkinQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RenkinQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public RenkinQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RenkinQuery();
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
		public bool Load(RenkinQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private RenkinQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RenkinQuery : esRenkinQuery
	{
		public RenkinQuery()
		{

		}		
		
		public RenkinQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "RenkinQuery";
        }
	}

	[Serializable]
	public partial class RenkinMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RenkinMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(RenkinMetadata.ColumnNames.RenkinID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinMetadata.PropertyNames.RenkinID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinMetadata.ColumnNames.PositionID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinMetadata.ColumnNames.Kegiatan, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinMetadata.PropertyNames.Kegiatan;
			c.CharacterMaxLength = 300;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinMetadata.ColumnNames.SRRenkinJenisKegiatan, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinMetadata.PropertyNames.SRRenkinJenisKegiatan;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinMetadata.ColumnNames.TargetPersen, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinMetadata.PropertyNames.TargetPersen;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinMetadata.ColumnNames.TargetBulan, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinMetadata.PropertyNames.TargetBulan;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinMetadata.ColumnNames.CreateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinMetadata.ColumnNames.CreateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RenkinMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RenkinMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public RenkinMetadata Meta()
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
			public const string RenkinID = "RenkinID";
			public const string PositionID = "PositionID";
			public const string Kegiatan = "Kegiatan";
			public const string SRRenkinJenisKegiatan = "SRRenkinJenisKegiatan";
			public const string TargetPersen = "TargetPersen";
			public const string TargetBulan = "TargetBulan";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RenkinID = "RenkinID";
			public const string PositionID = "PositionID";
			public const string Kegiatan = "Kegiatan";
			public const string SRRenkinJenisKegiatan = "SRRenkinJenisKegiatan";
			public const string TargetPersen = "TargetPersen";
			public const string TargetBulan = "TargetBulan";
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
			lock (typeof(RenkinMetadata))
			{
				if(RenkinMetadata.mapDelegates == null)
				{
					RenkinMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RenkinMetadata.meta == null)
				{
					RenkinMetadata.meta = new RenkinMetadata();
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
				
				meta.AddTypeMap("RenkinID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Kegiatan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRenkinJenisKegiatan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TargetPersen", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TargetBulan", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "Renkin";
				meta.Destination = "Renkin";
				meta.spInsert = "proc_RenkinInsert";				
				meta.spUpdate = "proc_RenkinUpdate";		
				meta.spDelete = "proc_RenkinDelete";
				meta.spLoadAll = "proc_RenkinLoadAll";
				meta.spLoadByPrimaryKey = "proc_RenkinLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RenkinMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
