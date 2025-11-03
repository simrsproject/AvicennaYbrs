/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/22/2022 2:31:52 PM
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
	abstract public class esQueueingSoundCollection : esEntityCollectionWAuditLog
	{
		public esQueueingSoundCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "QueueingSoundCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esQueueingSoundQuery query)
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
			this.InitQuery(query as esQueueingSoundQuery);
		}
		#endregion
			
		virtual public QueueingSound DetachEntity(QueueingSound entity)
		{
			return base.DetachEntity(entity) as QueueingSound;
		}
		
		virtual public QueueingSound AttachEntity(QueueingSound entity)
		{
			return base.AttachEntity(entity) as QueueingSound;
		}
		
		virtual public void Combine(QueueingSoundCollection collection)
		{
			base.Combine(collection);
		}
		
		new public QueueingSound this[int index]
		{
			get
			{
				return base[index] as QueueingSound;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(QueueingSound);
		}
	}

	[Serializable]
	abstract public class esQueueingSound : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esQueueingSoundQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esQueueingSound()
		{
		}
	
		public esQueueingSound(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 soundID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(soundID);
			else
				return LoadByPrimaryKeyStoredProcedure(soundID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 soundID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(soundID);
			else
				return LoadByPrimaryKeyStoredProcedure(soundID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 soundID)
		{
			esQueueingSoundQuery query = this.GetDynamicQuery();
			query.Where(query.SoundID==soundID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 soundID)
		{
			esParameters parms = new esParameters();
			parms.Add("SoundID",soundID);
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
						case "SoundID": this.str.SoundID = (string)value; break;
						case "Name": this.str.Name = (string)value; break;
						case "Number": this.str.Number = (string)value; break;
						case "FilePath": this.str.FilePath = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "IsServiceCounter": this.str.IsServiceCounter = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "SoundID":
						
							if (value == null || value is System.Int32)
								this.SoundID = (System.Int32?)value;
							break;
						case "Number":
						
							if (value == null || value is System.Int32)
								this.Number = (System.Int32?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsServiceCounter":
						
							if (value == null || value is System.Boolean)
								this.IsServiceCounter = (System.Boolean?)value;
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
		/// Maps to QueueingSound.SoundID
		/// </summary>
		virtual public System.Int32? SoundID
		{
			get
			{
				return base.GetSystemInt32(QueueingSoundMetadata.ColumnNames.SoundID);
			}
			
			set
			{
				base.SetSystemInt32(QueueingSoundMetadata.ColumnNames.SoundID, value);
			}
		}
		/// <summary>
		/// Maps to QueueingSound.Name
		/// </summary>
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(QueueingSoundMetadata.ColumnNames.Name);
			}
			
			set
			{
				base.SetSystemString(QueueingSoundMetadata.ColumnNames.Name, value);
			}
		}
		/// <summary>
		/// Maps to QueueingSound.Number
		/// </summary>
		virtual public System.Int32? Number
		{
			get
			{
				return base.GetSystemInt32(QueueingSoundMetadata.ColumnNames.Number);
			}
			
			set
			{
				base.SetSystemInt32(QueueingSoundMetadata.ColumnNames.Number, value);
			}
		}
		/// <summary>
		/// Maps to QueueingSound.FilePath
		/// </summary>
		virtual public System.String FilePath
		{
			get
			{
				return base.GetSystemString(QueueingSoundMetadata.ColumnNames.FilePath);
			}
			
			set
			{
				base.SetSystemString(QueueingSoundMetadata.ColumnNames.FilePath, value);
			}
		}
		/// <summary>
		/// Maps to QueueingSound.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(QueueingSoundMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(QueueingSoundMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to QueueingSound.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(QueueingSoundMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(QueueingSoundMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to QueueingSound.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(QueueingSoundMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(QueueingSoundMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to QueueingSound.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(QueueingSoundMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(QueueingSoundMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to QueueingSound.IsServiceCounter
		/// </summary>
		virtual public System.Boolean? IsServiceCounter
		{
			get
			{
				return base.GetSystemBoolean(QueueingSoundMetadata.ColumnNames.IsServiceCounter);
			}
			
			set
			{
				base.SetSystemBoolean(QueueingSoundMetadata.ColumnNames.IsServiceCounter, value);
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
			public esStrings(esQueueingSound entity)
			{
				this.entity = entity;
			}
			public System.String SoundID
			{
				get
				{
					System.Int32? data = entity.SoundID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SoundID = null;
					else entity.SoundID = Convert.ToInt32(value);
				}
			}
			public System.String Name
			{
				get
				{
					System.String data = entity.Name;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Name = null;
					else entity.Name = Convert.ToString(value);
				}
			}
			public System.String Number
			{
				get
				{
					System.Int32? data = entity.Number;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Number = null;
					else entity.Number = Convert.ToInt32(value);
				}
			}
			public System.String FilePath
			{
				get
				{
					System.String data = entity.FilePath;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FilePath = null;
					else entity.FilePath = Convert.ToString(value);
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
			public System.String IsServiceCounter
			{
				get
				{
					System.Boolean? data = entity.IsServiceCounter;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsServiceCounter = null;
					else entity.IsServiceCounter = Convert.ToBoolean(value);
				}
			}
			private esQueueingSound entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esQueueingSoundQuery query)
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
				throw new Exception("esQueueingSound can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class QueueingSound : esQueueingSound
	{	
	}

	[Serializable]
	abstract public class esQueueingSoundQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return QueueingSoundMetadata.Meta();
			}
		}	
			
		public esQueryItem SoundID
		{
			get
			{
				return new esQueryItem(this, QueueingSoundMetadata.ColumnNames.SoundID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Name
		{
			get
			{
				return new esQueryItem(this, QueueingSoundMetadata.ColumnNames.Name, esSystemType.String);
			}
		} 
			
		public esQueryItem Number
		{
			get
			{
				return new esQueryItem(this, QueueingSoundMetadata.ColumnNames.Number, esSystemType.Int32);
			}
		} 
			
		public esQueryItem FilePath
		{
			get
			{
				return new esQueryItem(this, QueueingSoundMetadata.ColumnNames.FilePath, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, QueueingSoundMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, QueueingSoundMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, QueueingSoundMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, QueueingSoundMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsServiceCounter
		{
			get
			{
				return new esQueryItem(this, QueueingSoundMetadata.ColumnNames.IsServiceCounter, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("QueueingSoundCollection")]
	public partial class QueueingSoundCollection : esQueueingSoundCollection, IEnumerable< QueueingSound>
	{
		public QueueingSoundCollection()
		{

		}	
		
		public static implicit operator List< QueueingSound>(QueueingSoundCollection coll)
		{
			List< QueueingSound> list = new List< QueueingSound>();
			
			foreach (QueueingSound emp in coll)
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
				return  QueueingSoundMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QueueingSoundQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new QueueingSound(row);
		}

		override protected esEntity CreateEntity()
		{
			return new QueueingSound();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public QueueingSoundQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QueueingSoundQuery();
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
		public bool Load(QueueingSoundQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public QueueingSound AddNew()
		{
			QueueingSound entity = base.AddNewEntity() as QueueingSound;
			
			return entity;		
		}
		public QueueingSound FindByPrimaryKey(Int32 soundID)
		{
			return base.FindByPrimaryKey(soundID) as QueueingSound;
		}

		#region IEnumerable< QueueingSound> Members

		IEnumerator< QueueingSound> IEnumerable< QueueingSound>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as QueueingSound;
			}
		}

		#endregion
		
		private QueueingSoundQuery query;
	}


	/// <summary>
	/// Encapsulates the 'QueueingSound' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("QueueingSound ({SoundID})")]
	[Serializable]
	public partial class QueueingSound : esQueueingSound
	{
		public QueueingSound()
		{
		}	
	
		public QueueingSound(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return QueueingSoundMetadata.Meta();
			}
		}	
	
		override protected esQueueingSoundQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QueueingSoundQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public QueueingSoundQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QueueingSoundQuery();
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
		public bool Load(QueueingSoundQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private QueueingSoundQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class QueueingSoundQuery : esQueueingSoundQuery
	{
		public QueueingSoundQuery()
		{

		}		
		
		public QueueingSoundQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "QueueingSoundQuery";
        }
	}

	[Serializable]
	public partial class QueueingSoundMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected QueueingSoundMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(QueueingSoundMetadata.ColumnNames.SoundID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = QueueingSoundMetadata.PropertyNames.SoundID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QueueingSoundMetadata.ColumnNames.Name, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = QueueingSoundMetadata.PropertyNames.Name;
			c.CharacterMaxLength = 50;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QueueingSoundMetadata.ColumnNames.Number, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = QueueingSoundMetadata.PropertyNames.Number;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QueueingSoundMetadata.ColumnNames.FilePath, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = QueueingSoundMetadata.PropertyNames.FilePath;
			c.CharacterMaxLength = 500;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QueueingSoundMetadata.ColumnNames.CreateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = QueueingSoundMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QueueingSoundMetadata.ColumnNames.CreateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = QueueingSoundMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QueueingSoundMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = QueueingSoundMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QueueingSoundMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = QueueingSoundMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QueueingSoundMetadata.ColumnNames.IsServiceCounter, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QueueingSoundMetadata.PropertyNames.IsServiceCounter;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public QueueingSoundMetadata Meta()
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
			public const string SoundID = "SoundID";
			public const string Name = "Name";
			public const string Number = "Number";
			public const string FilePath = "FilePath";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsServiceCounter = "IsServiceCounter";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string SoundID = "SoundID";
			public const string Name = "Name";
			public const string Number = "Number";
			public const string FilePath = "FilePath";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsServiceCounter = "IsServiceCounter";
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
			lock (typeof(QueueingSoundMetadata))
			{
				if(QueueingSoundMetadata.mapDelegates == null)
				{
					QueueingSoundMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (QueueingSoundMetadata.meta == null)
				{
					QueueingSoundMetadata.meta = new QueueingSoundMetadata();
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
				
				meta.AddTypeMap("SoundID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Name", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Number", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("FilePath", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsServiceCounter", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "QueueingSound";
				meta.Destination = "QueueingSound";
				meta.spInsert = "proc_QueueingSoundInsert";				
				meta.spUpdate = "proc_QueueingSoundUpdate";		
				meta.spDelete = "proc_QueueingSoundDelete";
				meta.spLoadAll = "proc_QueueingSoundLoadAll";
				meta.spLoadByPrimaryKey = "proc_QueueingSoundLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private QueueingSoundMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
