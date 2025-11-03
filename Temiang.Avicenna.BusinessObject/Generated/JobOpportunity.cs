/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:19 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

	[Serializable]
	abstract public class esJobOpportunityCollection : esEntityCollectionWAuditLog
	{
		public esJobOpportunityCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "JobOpportunityCollection";
		}

		#region Query Logic
		protected void InitQuery(esJobOpportunityQuery query)
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
			this.InitQuery(query as esJobOpportunityQuery);
		}
		#endregion
		
		virtual public JobOpportunity DetachEntity(JobOpportunity entity)
		{
			return base.DetachEntity(entity) as JobOpportunity;
		}
		
		virtual public JobOpportunity AttachEntity(JobOpportunity entity)
		{
			return base.AttachEntity(entity) as JobOpportunity;
		}
		
		virtual public void Combine(JobOpportunityCollection collection)
		{
			base.Combine(collection);
		}
		
		new public JobOpportunity this[int index]
		{
			get
			{
				return base[index] as JobOpportunity;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(JobOpportunity);
		}
	}



	[Serializable]
	abstract public class esJobOpportunity : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esJobOpportunityQuery GetDynamicQuery()
		{
			return null;
		}

		public esJobOpportunity()
		{

		}

		public esJobOpportunity(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 jobOpportunityID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(jobOpportunityID);
			else
				return LoadByPrimaryKeyStoredProcedure(jobOpportunityID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 jobOpportunityID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(jobOpportunityID);
			else
				return LoadByPrimaryKeyStoredProcedure(jobOpportunityID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 jobOpportunityID)
		{
			esJobOpportunityQuery query = this.GetDynamicQuery();
			query.Where(query.JobOpportunityID == jobOpportunityID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 jobOpportunityID)
		{
			esParameters parms = new esParameters();
			parms.Add("JobOpportunityID",jobOpportunityID);
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
						case "JobOpportunityID": this.str.JobOpportunityID = (string)value; break;							
						case "JobContent": this.str.JobContent = (string)value; break;							
						case "DatePrepared": this.str.DatePrepared = (string)value; break;							
						case "LastDateAccept": this.str.LastDateAccept = (string)value; break;							
						case "ContactPerson": this.str.ContactPerson = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "JobOpportunityID":
						
							if (value == null || value is System.Int32)
								this.JobOpportunityID = (System.Int32?)value;
							break;
						
						case "DatePrepared":
						
							if (value == null || value is System.DateTime)
								this.DatePrepared = (System.DateTime?)value;
							break;
						
						case "LastDateAccept":
						
							if (value == null || value is System.DateTime)
								this.LastDateAccept = (System.DateTime?)value;
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
		/// Maps to JobOpportunity.JobOpportunityID
		/// </summary>
		virtual public System.Int32? JobOpportunityID
		{
			get
			{
				return base.GetSystemInt32(JobOpportunityMetadata.ColumnNames.JobOpportunityID);
			}
			
			set
			{
				base.SetSystemInt32(JobOpportunityMetadata.ColumnNames.JobOpportunityID, value);
			}
		}
		
		/// <summary>
		/// Maps to JobOpportunity.JobContent
		/// </summary>
		virtual public System.String JobContent
		{
			get
			{
				return base.GetSystemString(JobOpportunityMetadata.ColumnNames.JobContent);
			}
			
			set
			{
				base.SetSystemString(JobOpportunityMetadata.ColumnNames.JobContent, value);
			}
		}
		
		/// <summary>
		/// Maps to JobOpportunity.DatePrepared
		/// </summary>
		virtual public System.DateTime? DatePrepared
		{
			get
			{
				return base.GetSystemDateTime(JobOpportunityMetadata.ColumnNames.DatePrepared);
			}
			
			set
			{
				base.SetSystemDateTime(JobOpportunityMetadata.ColumnNames.DatePrepared, value);
			}
		}
		
		/// <summary>
		/// Maps to JobOpportunity.LastDateAccept
		/// </summary>
		virtual public System.DateTime? LastDateAccept
		{
			get
			{
				return base.GetSystemDateTime(JobOpportunityMetadata.ColumnNames.LastDateAccept);
			}
			
			set
			{
				base.SetSystemDateTime(JobOpportunityMetadata.ColumnNames.LastDateAccept, value);
			}
		}
		
		/// <summary>
		/// Maps to JobOpportunity.ContactPerson
		/// </summary>
		virtual public System.String ContactPerson
		{
			get
			{
				return base.GetSystemString(JobOpportunityMetadata.ColumnNames.ContactPerson);
			}
			
			set
			{
				base.SetSystemString(JobOpportunityMetadata.ColumnNames.ContactPerson, value);
			}
		}
		
		/// <summary>
		/// Maps to JobOpportunity.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(JobOpportunityMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(JobOpportunityMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to JobOpportunity.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(JobOpportunityMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(JobOpportunityMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		#endregion	

		#region String Properties


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
			public esStrings(esJobOpportunity entity)
			{
				this.entity = entity;
			}
			
	
			public System.String JobOpportunityID
			{
				get
				{
					System.Int32? data = entity.JobOpportunityID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JobOpportunityID = null;
					else entity.JobOpportunityID = Convert.ToInt32(value);
				}
			}
				
			public System.String JobContent
			{
				get
				{
					System.String data = entity.JobContent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JobContent = null;
					else entity.JobContent = Convert.ToString(value);
				}
			}
				
			public System.String DatePrepared
			{
				get
				{
					System.DateTime? data = entity.DatePrepared;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DatePrepared = null;
					else entity.DatePrepared = Convert.ToDateTime(value);
				}
			}
				
			public System.String LastDateAccept
			{
				get
				{
					System.DateTime? data = entity.LastDateAccept;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastDateAccept = null;
					else entity.LastDateAccept = Convert.ToDateTime(value);
				}
			}
				
			public System.String ContactPerson
			{
				get
				{
					System.String data = entity.ContactPerson;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContactPerson = null;
					else entity.ContactPerson = Convert.ToString(value);
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
			

			private esJobOpportunity entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esJobOpportunityQuery query)
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
				throw new Exception("esJobOpportunity can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class JobOpportunity : esJobOpportunity
	{

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
		
			return props;
		}	
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PreSave.
		/// </summary>
		protected override void ApplyPreSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostSave.
		/// </summary>
		protected override void ApplyPostSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostOneToOneSave.
		/// </summary>
		protected override void ApplyPostOneSaveKeys()
		{
		}
		
	}



	[Serializable]
	abstract public class esJobOpportunityQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return JobOpportunityMetadata.Meta();
			}
		}	
		

		public esQueryItem JobOpportunityID
		{
			get
			{
				return new esQueryItem(this, JobOpportunityMetadata.ColumnNames.JobOpportunityID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem JobContent
		{
			get
			{
				return new esQueryItem(this, JobOpportunityMetadata.ColumnNames.JobContent, esSystemType.String);
			}
		} 
		
		public esQueryItem DatePrepared
		{
			get
			{
				return new esQueryItem(this, JobOpportunityMetadata.ColumnNames.DatePrepared, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastDateAccept
		{
			get
			{
				return new esQueryItem(this, JobOpportunityMetadata.ColumnNames.LastDateAccept, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ContactPerson
		{
			get
			{
				return new esQueryItem(this, JobOpportunityMetadata.ColumnNames.ContactPerson, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, JobOpportunityMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, JobOpportunityMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("JobOpportunityCollection")]
	public partial class JobOpportunityCollection : esJobOpportunityCollection, IEnumerable<JobOpportunity>
	{
		public JobOpportunityCollection()
		{

		}
		
		public static implicit operator List<JobOpportunity>(JobOpportunityCollection coll)
		{
			List<JobOpportunity> list = new List<JobOpportunity>();
			
			foreach (JobOpportunity emp in coll)
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
				return  JobOpportunityMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new JobOpportunityQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new JobOpportunity(row);
		}

		override protected esEntity CreateEntity()
		{
			return new JobOpportunity();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public JobOpportunityQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new JobOpportunityQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(JobOpportunityQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public JobOpportunity AddNew()
		{
			JobOpportunity entity = base.AddNewEntity() as JobOpportunity;
			
			return entity;
		}

		public JobOpportunity FindByPrimaryKey(System.Int32 jobOpportunityID)
		{
			return base.FindByPrimaryKey(jobOpportunityID) as JobOpportunity;
		}


		#region IEnumerable<JobOpportunity> Members

		IEnumerator<JobOpportunity> IEnumerable<JobOpportunity>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as JobOpportunity;
			}
		}

		#endregion
		
		private JobOpportunityQuery query;
	}


	/// <summary>
	/// Encapsulates the 'JobOpportunity' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("JobOpportunity ({JobOpportunityID})")]
	[Serializable]
	public partial class JobOpportunity : esJobOpportunity
	{
		public JobOpportunity()
		{

		}
	
		public JobOpportunity(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return JobOpportunityMetadata.Meta();
			}
		}
		
		
		
		override protected esJobOpportunityQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new JobOpportunityQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public JobOpportunityQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new JobOpportunityQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(JobOpportunityQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private JobOpportunityQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class JobOpportunityQuery : esJobOpportunityQuery
	{
		public JobOpportunityQuery()
		{

		}		
		
		public JobOpportunityQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "JobOpportunityQuery";
        }
		
			
	}


	[Serializable]
	public partial class JobOpportunityMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected JobOpportunityMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(JobOpportunityMetadata.ColumnNames.JobOpportunityID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = JobOpportunityMetadata.PropertyNames.JobOpportunityID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(JobOpportunityMetadata.ColumnNames.JobContent, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = JobOpportunityMetadata.PropertyNames.JobContent;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);
				
			c = new esColumnMetadata(JobOpportunityMetadata.ColumnNames.DatePrepared, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = JobOpportunityMetadata.PropertyNames.DatePrepared;
			_columns.Add(c);
				
			c = new esColumnMetadata(JobOpportunityMetadata.ColumnNames.LastDateAccept, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = JobOpportunityMetadata.PropertyNames.LastDateAccept;
			_columns.Add(c);
				
			c = new esColumnMetadata(JobOpportunityMetadata.ColumnNames.ContactPerson, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = JobOpportunityMetadata.PropertyNames.ContactPerson;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);
				
			c = new esColumnMetadata(JobOpportunityMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = JobOpportunityMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(JobOpportunityMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = JobOpportunityMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public JobOpportunityMetadata Meta()
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
			 public const string JobOpportunityID = "JobOpportunityID";
			 public const string JobContent = "JobContent";
			 public const string DatePrepared = "DatePrepared";
			 public const string LastDateAccept = "LastDateAccept";
			 public const string ContactPerson = "ContactPerson";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string JobOpportunityID = "JobOpportunityID";
			 public const string JobContent = "JobContent";
			 public const string DatePrepared = "DatePrepared";
			 public const string LastDateAccept = "LastDateAccept";
			 public const string ContactPerson = "ContactPerson";
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
			lock (typeof(JobOpportunityMetadata))
			{
				if(JobOpportunityMetadata.mapDelegates == null)
				{
					JobOpportunityMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (JobOpportunityMetadata.meta == null)
				{
					JobOpportunityMetadata.meta = new JobOpportunityMetadata();
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
				

				meta.AddTypeMap("JobOpportunityID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("JobContent", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DatePrepared", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastDateAccept", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ContactPerson", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "JobOpportunity";
				meta.Destination = "JobOpportunity";
				
				meta.spInsert = "proc_JobOpportunityInsert";				
				meta.spUpdate = "proc_JobOpportunityUpdate";		
				meta.spDelete = "proc_JobOpportunityDelete";
				meta.spLoadAll = "proc_JobOpportunityLoadAll";
				meta.spLoadByPrimaryKey = "proc_JobOpportunityLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private JobOpportunityMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
