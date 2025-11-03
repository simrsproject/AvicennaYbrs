/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/15/2011 10:42:27 PM
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
	abstract public class esWorkingGroupItemCollection : esEntityCollectionWAuditLog
	{
		public esWorkingGroupItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "WorkingGroupItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esWorkingGroupItemQuery query)
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
			this.InitQuery(query as esWorkingGroupItemQuery);
		}
		#endregion
		
		virtual public WorkingGroupItem DetachEntity(WorkingGroupItem entity)
		{
			return base.DetachEntity(entity) as WorkingGroupItem;
		}
		
		virtual public WorkingGroupItem AttachEntity(WorkingGroupItem entity)
		{
			return base.AttachEntity(entity) as WorkingGroupItem;
		}
		
		virtual public void Combine(WorkingGroupItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public WorkingGroupItem this[int index]
		{
			get
			{
				return base[index] as WorkingGroupItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(WorkingGroupItem);
		}
	}



	[Serializable]
	abstract public class esWorkingGroupItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esWorkingGroupItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esWorkingGroupItem()
		{

		}

		public esWorkingGroupItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 workingGroupID, System.Int32 workingTypeID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(workingGroupID, workingTypeID);
			else
				return LoadByPrimaryKeyStoredProcedure(workingGroupID, workingTypeID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 workingGroupID, System.Int32 workingTypeID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(workingGroupID, workingTypeID);
			else
				return LoadByPrimaryKeyStoredProcedure(workingGroupID, workingTypeID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 workingGroupID, System.Int32 workingTypeID)
		{
			esWorkingGroupItemQuery query = this.GetDynamicQuery();
			query.Where(query.WorkingGroupID == workingGroupID, query.WorkingTypeID == workingTypeID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 workingGroupID, System.Int32 workingTypeID)
		{
			esParameters parms = new esParameters();
			parms.Add("WorkingGroupID",workingGroupID);			parms.Add("WorkingTypeID",workingTypeID);
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
						case "WorkingGroupID": this.str.WorkingGroupID = (string)value; break;							
						case "WorkingTypeID": this.str.WorkingTypeID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "WorkingGroupID":
						
							if (value == null || value is System.Int32)
								this.WorkingGroupID = (System.Int32?)value;
							break;
						
						case "WorkingTypeID":
						
							if (value == null || value is System.Int32)
								this.WorkingTypeID = (System.Int32?)value;
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
		/// Maps to WorkingGroupItem.WorkingGroupID
		/// </summary>
		virtual public System.Int32? WorkingGroupID
		{
			get
			{
				return base.GetSystemInt32(WorkingGroupItemMetadata.ColumnNames.WorkingGroupID);
			}
			
			set
			{
				base.SetSystemInt32(WorkingGroupItemMetadata.ColumnNames.WorkingGroupID, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingGroupItem.WorkingTypeID
		/// </summary>
		virtual public System.Int32? WorkingTypeID
		{
			get
			{
				return base.GetSystemInt32(WorkingGroupItemMetadata.ColumnNames.WorkingTypeID);
			}
			
			set
			{
				base.SetSystemInt32(WorkingGroupItemMetadata.ColumnNames.WorkingTypeID, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingGroupItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(WorkingGroupItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingGroupItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingGroupItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(WorkingGroupItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(WorkingGroupItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esWorkingGroupItem entity)
			{
				this.entity = entity;
			}
			
	
			public System.String WorkingGroupID
			{
				get
				{
					System.Int32? data = entity.WorkingGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingGroupID = null;
					else entity.WorkingGroupID = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingTypeID
			{
				get
				{
					System.Int32? data = entity.WorkingTypeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingTypeID = null;
					else entity.WorkingTypeID = Convert.ToInt32(value);
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
			

			private esWorkingGroupItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esWorkingGroupItemQuery query)
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
				throw new Exception("esWorkingGroupItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class WorkingGroupItem : esWorkingGroupItem
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
	abstract public class esWorkingGroupItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return WorkingGroupItemMetadata.Meta();
			}
		}	
		

		public esQueryItem WorkingGroupID
		{
			get
			{
				return new esQueryItem(this, WorkingGroupItemMetadata.ColumnNames.WorkingGroupID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingTypeID
		{
			get
			{
				return new esQueryItem(this, WorkingGroupItemMetadata.ColumnNames.WorkingTypeID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, WorkingGroupItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, WorkingGroupItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("WorkingGroupItemCollection")]
	public partial class WorkingGroupItemCollection : esWorkingGroupItemCollection, IEnumerable<WorkingGroupItem>
	{
		public WorkingGroupItemCollection()
		{

		}
		
		public static implicit operator List<WorkingGroupItem>(WorkingGroupItemCollection coll)
		{
			List<WorkingGroupItem> list = new List<WorkingGroupItem>();
			
			foreach (WorkingGroupItem emp in coll)
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
				return  WorkingGroupItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WorkingGroupItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new WorkingGroupItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new WorkingGroupItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public WorkingGroupItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WorkingGroupItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(WorkingGroupItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public WorkingGroupItem AddNew()
		{
			WorkingGroupItem entity = base.AddNewEntity() as WorkingGroupItem;
			
			return entity;
		}

		public WorkingGroupItem FindByPrimaryKey(System.Int32 workingGroupID, System.Int32 workingTypeID)
		{
			return base.FindByPrimaryKey(workingGroupID, workingTypeID) as WorkingGroupItem;
		}


		#region IEnumerable<WorkingGroupItem> Members

		IEnumerator<WorkingGroupItem> IEnumerable<WorkingGroupItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as WorkingGroupItem;
			}
		}

		#endregion
		
		private WorkingGroupItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'WorkingGroupItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("WorkingGroupItem ({WorkingGroupID},{WorkingTypeID})")]
	[Serializable]
	public partial class WorkingGroupItem : esWorkingGroupItem
	{
		public WorkingGroupItem()
		{

		}
	
		public WorkingGroupItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return WorkingGroupItemMetadata.Meta();
			}
		}
		
		
		
		override protected esWorkingGroupItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WorkingGroupItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public WorkingGroupItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WorkingGroupItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(WorkingGroupItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private WorkingGroupItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class WorkingGroupItemQuery : esWorkingGroupItemQuery
	{
		public WorkingGroupItemQuery()
		{

		}		
		
		public WorkingGroupItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "WorkingGroupItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class WorkingGroupItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected WorkingGroupItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(WorkingGroupItemMetadata.ColumnNames.WorkingGroupID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingGroupItemMetadata.PropertyNames.WorkingGroupID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingGroupItemMetadata.ColumnNames.WorkingTypeID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingGroupItemMetadata.PropertyNames.WorkingTypeID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingGroupItemMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingGroupItemMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingGroupItemMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingGroupItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public WorkingGroupItemMetadata Meta()
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
			 public const string WorkingGroupID = "WorkingGroupID";
			 public const string WorkingTypeID = "WorkingTypeID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string WorkingGroupID = "WorkingGroupID";
			 public const string WorkingTypeID = "WorkingTypeID";
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
			lock (typeof(WorkingGroupItemMetadata))
			{
				if(WorkingGroupItemMetadata.mapDelegates == null)
				{
					WorkingGroupItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (WorkingGroupItemMetadata.meta == null)
				{
					WorkingGroupItemMetadata.meta = new WorkingGroupItemMetadata();
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
				

				meta.AddTypeMap("WorkingGroupID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingTypeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "WorkingGroupItem";
				meta.Destination = "WorkingGroupItem";
				
				meta.spInsert = "proc_WorkingGroupItemInsert";				
				meta.spUpdate = "proc_WorkingGroupItemUpdate";		
				meta.spDelete = "proc_WorkingGroupItemDelete";
				meta.spLoadAll = "proc_WorkingGroupItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_WorkingGroupItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private WorkingGroupItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
