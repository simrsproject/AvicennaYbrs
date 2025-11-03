/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 05/08/20 10:02:57 AM
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
	abstract public class esPostingStatusCollection : esEntityCollectionWAuditLog
	{
		public esPostingStatusCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "PostingStatusCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esPostingStatusQuery query)
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
			this.InitQuery(query as esPostingStatusQuery);
		}
		#endregion
			
		virtual public PostingStatus DetachEntity(PostingStatus entity)
		{
			return base.DetachEntity(entity) as PostingStatus;
		}
		
		virtual public PostingStatus AttachEntity(PostingStatus entity)
		{
			return base.AttachEntity(entity) as PostingStatus;
		}
		
		virtual public void Combine(PostingStatusCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PostingStatus this[int index]
		{
			get
			{
				return base[index] as PostingStatus;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PostingStatus);
		}
	}

	[Serializable]
	abstract public class esPostingStatus : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPostingStatusQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esPostingStatus()
		{
		}
	
		public esPostingStatus(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 postingId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(postingId);
			else
				return LoadByPrimaryKeyStoredProcedure(postingId);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 postingId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(postingId);
			else
				return LoadByPrimaryKeyStoredProcedure(postingId);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 postingId)
		{
			esPostingStatusQuery query = this.GetDynamicQuery();
			query.Where(query.PostingId == postingId);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 postingId)
		{
			esParameters parms = new esParameters();
			parms.Add("PostingId",postingId);
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
						case "PostingId": this.str.PostingId = (string)value; break;
						case "Month": this.str.Month = (string)value; break;
						case "Year": this.str.Year = (string)value; break;
						case "IsEnabled": this.str.IsEnabled = (string)value; break;
						case "CreatedBy": this.str.CreatedBy = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "DateCreated": this.str.DateCreated = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "PostingUntilDate": this.str.PostingUntilDate = (string)value; break;
						case "JournalSummaryId": this.str.JournalSummaryId = (string)value; break;
						case "IsFiscalYear": this.str.IsFiscalYear = (string)value; break;
						case "IsUncompleteAppr": this.str.IsUncompleteAppr = (string)value; break;
						case "JournalGroupID": this.str.JournalGroupID = (string)value; break;
						case "IsConsolidation": this.str.IsConsolidation = (string)value; break;
						case "ConsolidationJournalID": this.str.ConsolidationJournalID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PostingId":
						
							if (value == null || value is System.Int32)
								this.PostingId = (System.Int32?)value;
							break;
						case "IsEnabled":
						
							if (value == null || value is System.Boolean)
								this.IsEnabled = (System.Boolean?)value;
							break;
						case "DateCreated":
						
							if (value == null || value is System.DateTime)
								this.DateCreated = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "PostingUntilDate":
						
							if (value == null || value is System.DateTime)
								this.PostingUntilDate = (System.DateTime?)value;
							break;
						case "JournalSummaryId":
						
							if (value == null || value is System.Int32)
								this.JournalSummaryId = (System.Int32?)value;
							break;
						case "isFiscalYear":
						
							if (value == null || value is System.Boolean)
								this.IsFiscalYear = (System.Boolean?)value;
							break;
						case "isUncompleteAppr":
						
							if (value == null || value is System.Boolean)
								this.IsUncompleteAppr = (System.Boolean?)value;
							break;
						case "JournalGroupID":
						
							if (value == null || value is System.Int32)
								this.JournalGroupID = (System.Int32?)value;
							break;
						case "IsConsolidation":
						
							if (value == null || value is System.Boolean)
								this.IsConsolidation = (System.Boolean?)value;
							break;
						case "ConsolidationJournalID":
						
							if (value == null || value is System.Int32)
								this.ConsolidationJournalID = (System.Int32?)value;
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
		/// Maps to PostingStatus.PostingId
		/// </summary>
		virtual public System.Int32? PostingId
		{
			get
			{
				return base.GetSystemInt32(PostingStatusMetadata.ColumnNames.PostingId);
			}
			
			set
			{
				base.SetSystemInt32(PostingStatusMetadata.ColumnNames.PostingId, value);
			}
		}
		/// <summary>
		/// Maps to PostingStatus.Month
		/// </summary>
		virtual public System.String Month
		{
			get
			{
				return base.GetSystemString(PostingStatusMetadata.ColumnNames.Month);
			}
			
			set
			{
				base.SetSystemString(PostingStatusMetadata.ColumnNames.Month, value);
			}
		}
		/// <summary>
		/// Maps to PostingStatus.Year
		/// </summary>
		virtual public System.String Year
		{
			get
			{
				return base.GetSystemString(PostingStatusMetadata.ColumnNames.Year);
			}
			
			set
			{
				base.SetSystemString(PostingStatusMetadata.ColumnNames.Year, value);
			}
		}
		/// <summary>
		/// Maps to PostingStatus.IsEnabled
		/// </summary>
		virtual public System.Boolean? IsEnabled
		{
			get
			{
				return base.GetSystemBoolean(PostingStatusMetadata.ColumnNames.IsEnabled);
			}
			
			set
			{
				base.SetSystemBoolean(PostingStatusMetadata.ColumnNames.IsEnabled, value);
			}
		}
		/// <summary>
		/// Maps to PostingStatus.CreatedBy
		/// </summary>
		virtual public System.String CreatedBy
		{
			get
			{
				return base.GetSystemString(PostingStatusMetadata.ColumnNames.CreatedBy);
			}
			
			set
			{
				base.SetSystemString(PostingStatusMetadata.ColumnNames.CreatedBy, value);
			}
		}
		/// <summary>
		/// Maps to PostingStatus.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PostingStatusMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PostingStatusMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PostingStatus.DateCreated
		/// </summary>
		virtual public System.DateTime? DateCreated
		{
			get
			{
				return base.GetSystemDateTime(PostingStatusMetadata.ColumnNames.DateCreated);
			}
			
			set
			{
				base.SetSystemDateTime(PostingStatusMetadata.ColumnNames.DateCreated, value);
			}
		}
		/// <summary>
		/// Maps to PostingStatus.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PostingStatusMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PostingStatusMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PostingStatus.PostingUntilDate
		/// </summary>
		virtual public System.DateTime? PostingUntilDate
		{
			get
			{
				return base.GetSystemDateTime(PostingStatusMetadata.ColumnNames.PostingUntilDate);
			}
			
			set
			{
				base.SetSystemDateTime(PostingStatusMetadata.ColumnNames.PostingUntilDate, value);
			}
		}
		/// <summary>
		/// Maps to PostingStatus.JournalSummaryId
		/// </summary>
		virtual public System.Int32? JournalSummaryId
		{
			get
			{
				return base.GetSystemInt32(PostingStatusMetadata.ColumnNames.JournalSummaryId);
			}
			
			set
			{
				base.SetSystemInt32(PostingStatusMetadata.ColumnNames.JournalSummaryId, value);
			}
		}
		/// <summary>
		/// Maps to PostingStatus.isFiscalYear
		/// </summary>
		virtual public System.Boolean? IsFiscalYear
		{
			get
			{
				return base.GetSystemBoolean(PostingStatusMetadata.ColumnNames.IsFiscalYear);
			}
			
			set
			{
				base.SetSystemBoolean(PostingStatusMetadata.ColumnNames.IsFiscalYear, value);
			}
		}
		/// <summary>
		/// Maps to PostingStatus.isUncompleteAppr
		/// </summary>
		virtual public System.Boolean? IsUncompleteAppr
		{
			get
			{
				return base.GetSystemBoolean(PostingStatusMetadata.ColumnNames.IsUncompleteAppr);
			}
			
			set
			{
				base.SetSystemBoolean(PostingStatusMetadata.ColumnNames.IsUncompleteAppr, value);
			}
		}
		/// <summary>
		/// Maps to PostingStatus.JournalGroupID
		/// </summary>
		virtual public System.Int32? JournalGroupID
		{
			get
			{
				return base.GetSystemInt32(PostingStatusMetadata.ColumnNames.JournalGroupID);
			}
			
			set
			{
				base.SetSystemInt32(PostingStatusMetadata.ColumnNames.JournalGroupID, value);
			}
		}
		/// <summary>
		/// Maps to PostingStatus.IsConsolidation
		/// </summary>
		virtual public System.Boolean? IsConsolidation
		{
			get
			{
				return base.GetSystemBoolean(PostingStatusMetadata.ColumnNames.IsConsolidation);
			}
			
			set
			{
				base.SetSystemBoolean(PostingStatusMetadata.ColumnNames.IsConsolidation, value);
			}
		}
		/// <summary>
		/// Maps to PostingStatus.ConsolidationJournalID
		/// </summary>
		virtual public System.Int32? ConsolidationJournalID
		{
			get
			{
				return base.GetSystemInt32(PostingStatusMetadata.ColumnNames.ConsolidationJournalID);
			}
			
			set
			{
				base.SetSystemInt32(PostingStatusMetadata.ColumnNames.ConsolidationJournalID, value);
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
			public esStrings(esPostingStatus entity)
			{
				this.entity = entity;
			}
			public System.String PostingId
			{
				get
				{
					System.Int32? data = entity.PostingId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PostingId = null;
					else entity.PostingId = Convert.ToInt32(value);
				}
			}
			public System.String Month
			{
				get
				{
					System.String data = entity.Month;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Month = null;
					else entity.Month = Convert.ToString(value);
				}
			}
			public System.String Year
			{
				get
				{
					System.String data = entity.Year;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Year = null;
					else entity.Year = Convert.ToString(value);
				}
			}
			public System.String IsEnabled
			{
				get
				{
					System.Boolean? data = entity.IsEnabled;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEnabled = null;
					else entity.IsEnabled = Convert.ToBoolean(value);
				}
			}
			public System.String CreatedBy
			{
				get
				{
					System.String data = entity.CreatedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedBy = null;
					else entity.CreatedBy = Convert.ToString(value);
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
			public System.String DateCreated
			{
				get
				{
					System.DateTime? data = entity.DateCreated;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateCreated = null;
					else entity.DateCreated = Convert.ToDateTime(value);
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
			public System.String PostingUntilDate
			{
				get
				{
					System.DateTime? data = entity.PostingUntilDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PostingUntilDate = null;
					else entity.PostingUntilDate = Convert.ToDateTime(value);
				}
			}
			public System.String JournalSummaryId
			{
				get
				{
					System.Int32? data = entity.JournalSummaryId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalSummaryId = null;
					else entity.JournalSummaryId = Convert.ToInt32(value);
				}
			}
			public System.String IsFiscalYear
			{
				get
				{
					System.Boolean? data = entity.IsFiscalYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFiscalYear = null;
					else entity.IsFiscalYear = Convert.ToBoolean(value);
				}
			}
			public System.String IsUncompleteAppr
			{
				get
				{
					System.Boolean? data = entity.IsUncompleteAppr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUncompleteAppr = null;
					else entity.IsUncompleteAppr = Convert.ToBoolean(value);
				}
			}
			public System.String JournalGroupID
			{
				get
				{
					System.Int32? data = entity.JournalGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalGroupID = null;
					else entity.JournalGroupID = Convert.ToInt32(value);
				}
			}
			public System.String IsConsolidation
			{
				get
				{
					System.Boolean? data = entity.IsConsolidation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsConsolidation = null;
					else entity.IsConsolidation = Convert.ToBoolean(value);
				}
			}
			public System.String ConsolidationJournalID
			{
				get
				{
					System.Int32? data = entity.ConsolidationJournalID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConsolidationJournalID = null;
					else entity.ConsolidationJournalID = Convert.ToInt32(value);
				}
			}
			private esPostingStatus entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPostingStatusQuery query)
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
				throw new Exception("esPostingStatus can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PostingStatus : esPostingStatus
	{	
	}

	[Serializable]
	abstract public class esPostingStatusQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return PostingStatusMetadata.Meta();
			}
		}	
			
		public esQueryItem PostingId
		{
			get
			{
				return new esQueryItem(this, PostingStatusMetadata.ColumnNames.PostingId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Month
		{
			get
			{
				return new esQueryItem(this, PostingStatusMetadata.ColumnNames.Month, esSystemType.String);
			}
		} 
			
		public esQueryItem Year
		{
			get
			{
				return new esQueryItem(this, PostingStatusMetadata.ColumnNames.Year, esSystemType.String);
			}
		} 
			
		public esQueryItem IsEnabled
		{
			get
			{
				return new esQueryItem(this, PostingStatusMetadata.ColumnNames.IsEnabled, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem CreatedBy
		{
			get
			{
				return new esQueryItem(this, PostingStatusMetadata.ColumnNames.CreatedBy, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PostingStatusMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem DateCreated
		{
			get
			{
				return new esQueryItem(this, PostingStatusMetadata.ColumnNames.DateCreated, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PostingStatusMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem PostingUntilDate
		{
			get
			{
				return new esQueryItem(this, PostingStatusMetadata.ColumnNames.PostingUntilDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem JournalSummaryId
		{
			get
			{
				return new esQueryItem(this, PostingStatusMetadata.ColumnNames.JournalSummaryId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem IsFiscalYear
		{
			get
			{
				return new esQueryItem(this, PostingStatusMetadata.ColumnNames.IsFiscalYear, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsUncompleteAppr
		{
			get
			{
				return new esQueryItem(this, PostingStatusMetadata.ColumnNames.IsUncompleteAppr, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem JournalGroupID
		{
			get
			{
				return new esQueryItem(this, PostingStatusMetadata.ColumnNames.JournalGroupID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem IsConsolidation
		{
			get
			{
				return new esQueryItem(this, PostingStatusMetadata.ColumnNames.IsConsolidation, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ConsolidationJournalID
		{
			get
			{
				return new esQueryItem(this, PostingStatusMetadata.ColumnNames.ConsolidationJournalID, esSystemType.Int32);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PostingStatusCollection")]
	public partial class PostingStatusCollection : esPostingStatusCollection, IEnumerable< PostingStatus>
	{
		public PostingStatusCollection()
		{

		}	
		
		public static implicit operator List< PostingStatus>(PostingStatusCollection coll)
		{
			List< PostingStatus> list = new List< PostingStatus>();
			
			foreach (PostingStatus emp in coll)
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
				return  PostingStatusMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PostingStatusQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PostingStatus(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PostingStatus();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public PostingStatusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PostingStatusQuery();
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
		public bool Load(PostingStatusQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PostingStatus AddNew()
		{
			PostingStatus entity = base.AddNewEntity() as PostingStatus;
			
			return entity;		
		}
		public PostingStatus FindByPrimaryKey(Int32 postingId)
		{
			return base.FindByPrimaryKey(postingId) as PostingStatus;
		}

		#region IEnumerable< PostingStatus> Members

		IEnumerator< PostingStatus> IEnumerable< PostingStatus>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PostingStatus;
			}
		}

		#endregion
		
		private PostingStatusQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PostingStatus' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PostingStatus ({PostingId})")]
	[Serializable]
	public partial class PostingStatus : esPostingStatus
	{
		public PostingStatus()
		{
		}	
	
		public PostingStatus(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PostingStatusMetadata.Meta();
			}
		}	
	
		override protected esPostingStatusQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PostingStatusQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public PostingStatusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PostingStatusQuery();
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
		public bool Load(PostingStatusQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private PostingStatusQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PostingStatusQuery : esPostingStatusQuery
	{
		public PostingStatusQuery()
		{

		}		
		
		public PostingStatusQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "PostingStatusQuery";
        }
	}

	[Serializable]
	public partial class PostingStatusMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PostingStatusMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(PostingStatusMetadata.ColumnNames.PostingId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PostingStatusMetadata.PropertyNames.PostingId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PostingStatusMetadata.ColumnNames.Month, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PostingStatusMetadata.PropertyNames.Month;
			c.CharacterMaxLength = 2;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PostingStatusMetadata.ColumnNames.Year, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PostingStatusMetadata.PropertyNames.Year;
			c.CharacterMaxLength = 4;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PostingStatusMetadata.ColumnNames.IsEnabled, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PostingStatusMetadata.PropertyNames.IsEnabled;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PostingStatusMetadata.ColumnNames.CreatedBy, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PostingStatusMetadata.PropertyNames.CreatedBy;
			c.CharacterMaxLength = 25;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PostingStatusMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PostingStatusMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 25;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PostingStatusMetadata.ColumnNames.DateCreated, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PostingStatusMetadata.PropertyNames.DateCreated;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PostingStatusMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PostingStatusMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PostingStatusMetadata.ColumnNames.PostingUntilDate, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PostingStatusMetadata.PropertyNames.PostingUntilDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PostingStatusMetadata.ColumnNames.JournalSummaryId, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PostingStatusMetadata.PropertyNames.JournalSummaryId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PostingStatusMetadata.ColumnNames.IsFiscalYear, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PostingStatusMetadata.PropertyNames.IsFiscalYear;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PostingStatusMetadata.ColumnNames.IsUncompleteAppr, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PostingStatusMetadata.PropertyNames.IsUncompleteAppr;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PostingStatusMetadata.ColumnNames.JournalGroupID, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PostingStatusMetadata.PropertyNames.JournalGroupID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PostingStatusMetadata.ColumnNames.IsConsolidation, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PostingStatusMetadata.PropertyNames.IsConsolidation;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PostingStatusMetadata.ColumnNames.ConsolidationJournalID, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PostingStatusMetadata.PropertyNames.ConsolidationJournalID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public PostingStatusMetadata Meta()
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
			public const string PostingId = "PostingId";
			public const string Month = "Month";
			public const string Year = "Year";
			public const string IsEnabled = "IsEnabled";
			public const string CreatedBy = "CreatedBy";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string DateCreated = "DateCreated";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string PostingUntilDate = "PostingUntilDate";
			public const string JournalSummaryId = "JournalSummaryId";
			public const string IsFiscalYear = "isFiscalYear";
			public const string IsUncompleteAppr = "isUncompleteAppr";
			public const string JournalGroupID = "JournalGroupID";
			public const string IsConsolidation = "IsConsolidation";
			public const string ConsolidationJournalID = "ConsolidationJournalID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string PostingId = "PostingId";
			public const string Month = "Month";
			public const string Year = "Year";
			public const string IsEnabled = "IsEnabled";
			public const string CreatedBy = "CreatedBy";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string DateCreated = "DateCreated";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string PostingUntilDate = "PostingUntilDate";
			public const string JournalSummaryId = "JournalSummaryId";
			public const string IsFiscalYear = "IsFiscalYear";
			public const string IsUncompleteAppr = "IsUncompleteAppr";
			public const string JournalGroupID = "JournalGroupID";
			public const string IsConsolidation = "IsConsolidation";
			public const string ConsolidationJournalID = "ConsolidationJournalID";
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
			lock (typeof(PostingStatusMetadata))
			{
				if(PostingStatusMetadata.mapDelegates == null)
				{
					PostingStatusMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PostingStatusMetadata.meta == null)
				{
					PostingStatusMetadata.meta = new PostingStatusMetadata();
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
				
				meta.AddTypeMap("PostingId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Month", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Year", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsEnabled", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedBy", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("DateCreated", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PostingUntilDate", new esTypeMap("date", "System.DateTime"));
				meta.AddTypeMap("JournalSummaryId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsFiscalYear", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUncompleteAppr", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("JournalGroupID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsConsolidation", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ConsolidationJournalID", new esTypeMap("int", "System.Int32"));
		

				meta.Source = "PostingStatus";
				meta.Destination = "PostingStatus";
				meta.spInsert = "proc_PostingStatusInsert";				
				meta.spUpdate = "proc_PostingStatusUpdate";		
				meta.spDelete = "proc_PostingStatusDelete";
				meta.spLoadAll = "proc_PostingStatusLoadAll";
				meta.spLoadByPrimaryKey = "proc_PostingStatusLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PostingStatusMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
