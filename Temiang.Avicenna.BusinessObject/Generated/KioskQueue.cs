/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/13/2020 8:56:04 AM
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
	abstract public class esKioskQueueCollection : esEntityCollectionWAuditLog
	{
		public esKioskQueueCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "KioskQueueCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esKioskQueueQuery query)
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
			this.InitQuery(query as esKioskQueueQuery);
		}
		#endregion
			
		virtual public KioskQueue DetachEntity(KioskQueue entity)
		{
			return base.DetachEntity(entity) as KioskQueue;
		}
		
		virtual public KioskQueue AttachEntity(KioskQueue entity)
		{
			return base.AttachEntity(entity) as KioskQueue;
		}
		
		virtual public void Combine(KioskQueueCollection collection)
		{
			base.Combine(collection);
		}
		
		new public KioskQueue this[int index]
		{
			get
			{
				return base[index] as KioskQueue;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(KioskQueue);
		}
	}

	[Serializable]
	abstract public class esKioskQueue : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esKioskQueueQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esKioskQueue()
		{
		}
	
		public esKioskQueue(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 kioskQueueID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(kioskQueueID);
			else
				return LoadByPrimaryKeyStoredProcedure(kioskQueueID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 kioskQueueID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(kioskQueueID);
			else
				return LoadByPrimaryKeyStoredProcedure(kioskQueueID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int64 kioskQueueID)
		{
			esKioskQueueQuery query = this.GetDynamicQuery();
			query.Where(query.KioskQueueID==kioskQueueID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int64 kioskQueueID)
		{
			esParameters parms = new esParameters();
			parms.Add("KioskQueueID",kioskQueueID);
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
						case "KioskQueueID": this.str.KioskQueueID = (string)value; break;
						case "KioskQueueNo": this.str.KioskQueueNo = (string)value; break;
						case "KioskQueueCode": this.str.KioskQueueCode = (string)value; break;
						case "KioskQueueDate": this.str.KioskQueueDate = (string)value; break;
						case "SRKioskQueueStatus": this.str.SRKioskQueueStatus = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ProcessByUserID": this.str.ProcessByUserID = (string)value; break;
						case "ProcessDateTime": this.str.ProcessDateTime = (string)value; break;
						case "Recall": this.str.Recall = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "KioskQueueID":
						
							if (value == null || value is System.Int64)
								this.KioskQueueID = (System.Int64?)value;
							break;
						case "KioskQueueDate":
						
							if (value == null || value is System.DateTime)
								this.KioskQueueDate = (System.DateTime?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "ProcessDateTime":
						
							if (value == null || value is System.DateTime)
								this.ProcessDateTime = (System.DateTime?)value;
							break;
						case "Recall":
						
							if (value == null || value is System.Boolean)
								this.Recall = (System.Boolean?)value;
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
		/// Maps to KioskQueue.KioskQueueID
		/// </summary>
		virtual public System.Int64? KioskQueueID
		{
			get
			{
				return base.GetSystemInt64(KioskQueueMetadata.ColumnNames.KioskQueueID);
			}
			
			set
			{
				base.SetSystemInt64(KioskQueueMetadata.ColumnNames.KioskQueueID, value);
			}
		}
		/// <summary>
		/// Maps to KioskQueue.KioskQueueNo
		/// </summary>
		virtual public System.String KioskQueueNo
		{
			get
			{
				return base.GetSystemString(KioskQueueMetadata.ColumnNames.KioskQueueNo);
			}
			
			set
			{
				base.SetSystemString(KioskQueueMetadata.ColumnNames.KioskQueueNo, value);
			}
		}
		/// <summary>
		/// Maps to KioskQueue.KioskQueueCode
		/// </summary>
		virtual public System.String KioskQueueCode
		{
			get
			{
				return base.GetSystemString(KioskQueueMetadata.ColumnNames.KioskQueueCode);
			}
			
			set
			{
				base.SetSystemString(KioskQueueMetadata.ColumnNames.KioskQueueCode, value);
			}
		}
		/// <summary>
		/// Maps to KioskQueue.KioskQueueDate
		/// </summary>
		virtual public System.DateTime? KioskQueueDate
		{
			get
			{
				return base.GetSystemDateTime(KioskQueueMetadata.ColumnNames.KioskQueueDate);
			}
			
			set
			{
				base.SetSystemDateTime(KioskQueueMetadata.ColumnNames.KioskQueueDate, value);
			}
		}
		/// <summary>
		/// Maps to KioskQueue.SRKioskQueueStatus
		/// </summary>
		virtual public System.String SRKioskQueueStatus
		{
			get
			{
				return base.GetSystemString(KioskQueueMetadata.ColumnNames.SRKioskQueueStatus);
			}
			
			set
			{
				base.SetSystemString(KioskQueueMetadata.ColumnNames.SRKioskQueueStatus, value);
			}
		}
		/// <summary>
		/// Maps to KioskQueue.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(KioskQueueMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(KioskQueueMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to KioskQueue.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(KioskQueueMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(KioskQueueMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to KioskQueue.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(KioskQueueMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(KioskQueueMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to KioskQueue.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(KioskQueueMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(KioskQueueMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to KioskQueue.ProcessByUserID
		/// </summary>
		virtual public System.String ProcessByUserID
		{
			get
			{
				return base.GetSystemString(KioskQueueMetadata.ColumnNames.ProcessByUserID);
			}
			
			set
			{
				base.SetSystemString(KioskQueueMetadata.ColumnNames.ProcessByUserID, value);
			}
		}
		/// <summary>
		/// Maps to KioskQueue.ProcessDateTime
		/// </summary>
		virtual public System.DateTime? ProcessDateTime
		{
			get
			{
				return base.GetSystemDateTime(KioskQueueMetadata.ColumnNames.ProcessDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(KioskQueueMetadata.ColumnNames.ProcessDateTime, value);
			}
		}
		/// <summary>
		/// Maps to KioskQueue.Recall
		/// </summary>
		virtual public System.Boolean? Recall
		{
			get
			{
				return base.GetSystemBoolean(KioskQueueMetadata.ColumnNames.Recall);
			}
			
			set
			{
				base.SetSystemBoolean(KioskQueueMetadata.ColumnNames.Recall, value);
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
			public esStrings(esKioskQueue entity)
			{
				this.entity = entity;
			}
			public System.String KioskQueueID
			{
				get
				{
					System.Int64? data = entity.KioskQueueID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KioskQueueID = null;
					else entity.KioskQueueID = Convert.ToInt64(value);
				}
			}
			public System.String KioskQueueNo
			{
				get
				{
					System.String data = entity.KioskQueueNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KioskQueueNo = null;
					else entity.KioskQueueNo = Convert.ToString(value);
				}
			}
			public System.String KioskQueueCode
			{
				get
				{
					System.String data = entity.KioskQueueCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KioskQueueCode = null;
					else entity.KioskQueueCode = Convert.ToString(value);
				}
			}
			public System.String KioskQueueDate
			{
				get
				{
					System.DateTime? data = entity.KioskQueueDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KioskQueueDate = null;
					else entity.KioskQueueDate = Convert.ToDateTime(value);
				}
			}
			public System.String SRKioskQueueStatus
			{
				get
				{
					System.String data = entity.SRKioskQueueStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRKioskQueueStatus = null;
					else entity.SRKioskQueueStatus = Convert.ToString(value);
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
			public System.String ProcessByUserID
			{
				get
				{
					System.String data = entity.ProcessByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcessByUserID = null;
					else entity.ProcessByUserID = Convert.ToString(value);
				}
			}
			public System.String ProcessDateTime
			{
				get
				{
					System.DateTime? data = entity.ProcessDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcessDateTime = null;
					else entity.ProcessDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String Recall
			{
				get
				{
					System.Boolean? data = entity.Recall;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Recall = null;
					else entity.Recall = Convert.ToBoolean(value);
				}
			}
			private esKioskQueue entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esKioskQueueQuery query)
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
				throw new Exception("esKioskQueue can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class KioskQueue : esKioskQueue
	{	
	}

	[Serializable]
	abstract public class esKioskQueueQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return KioskQueueMetadata.Meta();
			}
		}	
			
		public esQueryItem KioskQueueID
		{
			get
			{
				return new esQueryItem(this, KioskQueueMetadata.ColumnNames.KioskQueueID, esSystemType.Int64);
			}
		} 
			
		public esQueryItem KioskQueueNo
		{
			get
			{
				return new esQueryItem(this, KioskQueueMetadata.ColumnNames.KioskQueueNo, esSystemType.String);
			}
		} 
			
		public esQueryItem KioskQueueCode
		{
			get
			{
				return new esQueryItem(this, KioskQueueMetadata.ColumnNames.KioskQueueCode, esSystemType.String);
			}
		} 
			
		public esQueryItem KioskQueueDate
		{
			get
			{
				return new esQueryItem(this, KioskQueueMetadata.ColumnNames.KioskQueueDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem SRKioskQueueStatus
		{
			get
			{
				return new esQueryItem(this, KioskQueueMetadata.ColumnNames.SRKioskQueueStatus, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, KioskQueueMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, KioskQueueMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, KioskQueueMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, KioskQueueMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem ProcessByUserID
		{
			get
			{
				return new esQueryItem(this, KioskQueueMetadata.ColumnNames.ProcessByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem ProcessDateTime
		{
			get
			{
				return new esQueryItem(this, KioskQueueMetadata.ColumnNames.ProcessDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem Recall
		{
			get
			{
				return new esQueryItem(this, KioskQueueMetadata.ColumnNames.Recall, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("KioskQueueCollection")]
	public partial class KioskQueueCollection : esKioskQueueCollection, IEnumerable< KioskQueue>
	{
		public KioskQueueCollection()
		{

		}	
		
		public static implicit operator List< KioskQueue>(KioskQueueCollection coll)
		{
			List< KioskQueue> list = new List< KioskQueue>();
			
			foreach (KioskQueue emp in coll)
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
				return  KioskQueueMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new KioskQueueQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new KioskQueue(row);
		}

		override protected esEntity CreateEntity()
		{
			return new KioskQueue();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public KioskQueueQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new KioskQueueQuery();
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
		public bool Load(KioskQueueQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public KioskQueue AddNew()
		{
			KioskQueue entity = base.AddNewEntity() as KioskQueue;
			
			return entity;		
		}
		public KioskQueue FindByPrimaryKey(Int64 kioskQueueID)
		{
			return base.FindByPrimaryKey(kioskQueueID) as KioskQueue;
		}

		#region IEnumerable< KioskQueue> Members

		IEnumerator< KioskQueue> IEnumerable< KioskQueue>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as KioskQueue;
			}
		}

		#endregion
		
		private KioskQueueQuery query;
	}


	/// <summary>
	/// Encapsulates the 'KioskQueue' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("KioskQueue ({KioskQueueID})")]
	[Serializable]
	public partial class KioskQueue : esKioskQueue
	{
		public KioskQueue()
		{
		}	
	
		public KioskQueue(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return KioskQueueMetadata.Meta();
			}
		}	
	
		override protected esKioskQueueQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new KioskQueueQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public KioskQueueQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new KioskQueueQuery();
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
		public bool Load(KioskQueueQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private KioskQueueQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class KioskQueueQuery : esKioskQueueQuery
	{
		public KioskQueueQuery()
		{

		}		
		
		public KioskQueueQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "KioskQueueQuery";
        }
	}

	[Serializable]
	public partial class KioskQueueMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected KioskQueueMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(KioskQueueMetadata.ColumnNames.KioskQueueID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = KioskQueueMetadata.PropertyNames.KioskQueueID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c); 
				
			c = new esColumnMetadata(KioskQueueMetadata.ColumnNames.KioskQueueNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = KioskQueueMetadata.PropertyNames.KioskQueueNo;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(KioskQueueMetadata.ColumnNames.KioskQueueCode, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = KioskQueueMetadata.PropertyNames.KioskQueueCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(KioskQueueMetadata.ColumnNames.KioskQueueDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = KioskQueueMetadata.PropertyNames.KioskQueueDate;
			_columns.Add(c); 
				
			c = new esColumnMetadata(KioskQueueMetadata.ColumnNames.SRKioskQueueStatus, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = KioskQueueMetadata.PropertyNames.SRKioskQueueStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(KioskQueueMetadata.ColumnNames.CreateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = KioskQueueMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(KioskQueueMetadata.ColumnNames.CreateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = KioskQueueMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(KioskQueueMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = KioskQueueMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(KioskQueueMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = KioskQueueMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(KioskQueueMetadata.ColumnNames.ProcessByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = KioskQueueMetadata.PropertyNames.ProcessByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(KioskQueueMetadata.ColumnNames.ProcessDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = KioskQueueMetadata.PropertyNames.ProcessDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(KioskQueueMetadata.ColumnNames.Recall, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = KioskQueueMetadata.PropertyNames.Recall;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public KioskQueueMetadata Meta()
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
			public const string KioskQueueID = "KioskQueueID";
			public const string KioskQueueNo = "KioskQueueNo";
			public const string KioskQueueCode = "KioskQueueCode";
			public const string KioskQueueDate = "KioskQueueDate";
			public const string SRKioskQueueStatus = "SRKioskQueueStatus";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ProcessByUserID = "ProcessByUserID";
			public const string ProcessDateTime = "ProcessDateTime";
			public const string Recall = "Recall";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string KioskQueueID = "KioskQueueID";
			public const string KioskQueueNo = "KioskQueueNo";
			public const string KioskQueueCode = "KioskQueueCode";
			public const string KioskQueueDate = "KioskQueueDate";
			public const string SRKioskQueueStatus = "SRKioskQueueStatus";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ProcessByUserID = "ProcessByUserID";
			public const string ProcessDateTime = "ProcessDateTime";
			public const string Recall = "Recall";
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
			lock (typeof(KioskQueueMetadata))
			{
				if(KioskQueueMetadata.mapDelegates == null)
				{
					KioskQueueMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (KioskQueueMetadata.meta == null)
				{
					KioskQueueMetadata.meta = new KioskQueueMetadata();
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
				
				meta.AddTypeMap("KioskQueueID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("KioskQueueNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KioskQueueCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KioskQueueDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRKioskQueueStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProcessByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProcessDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Recall", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "KioskQueue";
				meta.Destination = "KioskQueue";
				meta.spInsert = "proc_KioskQueueInsert";				
				meta.spUpdate = "proc_KioskQueueUpdate";		
				meta.spDelete = "proc_KioskQueueDelete";
				meta.spLoadAll = "proc_KioskQueueLoadAll";
				meta.spLoadByPrimaryKey = "proc_KioskQueueLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private KioskQueueMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
