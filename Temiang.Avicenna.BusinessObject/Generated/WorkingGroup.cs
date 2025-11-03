/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/15/2011 10:42:18 PM
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
	abstract public class esWorkingGroupCollection : esEntityCollectionWAuditLog
	{
		public esWorkingGroupCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "WorkingGroupCollection";
		}

		#region Query Logic
		protected void InitQuery(esWorkingGroupQuery query)
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
			this.InitQuery(query as esWorkingGroupQuery);
		}
		#endregion
		
		virtual public WorkingGroup DetachEntity(WorkingGroup entity)
		{
			return base.DetachEntity(entity) as WorkingGroup;
		}
		
		virtual public WorkingGroup AttachEntity(WorkingGroup entity)
		{
			return base.AttachEntity(entity) as WorkingGroup;
		}
		
		virtual public void Combine(WorkingGroupCollection collection)
		{
			base.Combine(collection);
		}
		
		new public WorkingGroup this[int index]
		{
			get
			{
				return base[index] as WorkingGroup;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(WorkingGroup);
		}
	}



	[Serializable]
	abstract public class esWorkingGroup : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esWorkingGroupQuery GetDynamicQuery()
		{
			return null;
		}

		public esWorkingGroup()
		{

		}

		public esWorkingGroup(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 workingGroupID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(workingGroupID);
			else
				return LoadByPrimaryKeyStoredProcedure(workingGroupID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 workingGroupID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(workingGroupID);
			else
				return LoadByPrimaryKeyStoredProcedure(workingGroupID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 workingGroupID)
		{
			esWorkingGroupQuery query = this.GetDynamicQuery();
			query.Where(query.WorkingGroupID == workingGroupID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 workingGroupID)
		{
			esParameters parms = new esParameters();
			parms.Add("WorkingGroupID",workingGroupID);
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
						case "WorkingGroupCode": this.str.WorkingGroupCode = (string)value; break;							
						case "WorkingGroupName": this.str.WorkingGroupName = (string)value; break;							
						case "Note": this.str.Note = (string)value; break;							
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
		/// Maps to WorkingGroup.WorkingGroupID
		/// </summary>
		virtual public System.Int32? WorkingGroupID
		{
			get
			{
				return base.GetSystemInt32(WorkingGroupMetadata.ColumnNames.WorkingGroupID);
			}
			
			set
			{
				base.SetSystemInt32(WorkingGroupMetadata.ColumnNames.WorkingGroupID, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingGroup.WorkingGroupCode
		/// </summary>
		virtual public System.String WorkingGroupCode
		{
			get
			{
				return base.GetSystemString(WorkingGroupMetadata.ColumnNames.WorkingGroupCode);
			}
			
			set
			{
				base.SetSystemString(WorkingGroupMetadata.ColumnNames.WorkingGroupCode, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingGroup.WorkingGroupName
		/// </summary>
		virtual public System.String WorkingGroupName
		{
			get
			{
				return base.GetSystemString(WorkingGroupMetadata.ColumnNames.WorkingGroupName);
			}
			
			set
			{
				base.SetSystemString(WorkingGroupMetadata.ColumnNames.WorkingGroupName, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingGroup.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(WorkingGroupMetadata.ColumnNames.Note);
			}
			
			set
			{
				base.SetSystemString(WorkingGroupMetadata.ColumnNames.Note, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingGroup.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(WorkingGroupMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingGroupMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingGroup.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(WorkingGroupMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(WorkingGroupMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esWorkingGroup entity)
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
				
			public System.String WorkingGroupCode
			{
				get
				{
					System.String data = entity.WorkingGroupCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingGroupCode = null;
					else entity.WorkingGroupCode = Convert.ToString(value);
				}
			}
				
			public System.String WorkingGroupName
			{
				get
				{
					System.String data = entity.WorkingGroupName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingGroupName = null;
					else entity.WorkingGroupName = Convert.ToString(value);
				}
			}
				
			public System.String Note
			{
				get
				{
					System.String data = entity.Note;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Note = null;
					else entity.Note = Convert.ToString(value);
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
			

			private esWorkingGroup entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esWorkingGroupQuery query)
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
				throw new Exception("esWorkingGroup can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class WorkingGroup : esWorkingGroup
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
	abstract public class esWorkingGroupQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return WorkingGroupMetadata.Meta();
			}
		}	
		

		public esQueryItem WorkingGroupID
		{
			get
			{
				return new esQueryItem(this, WorkingGroupMetadata.ColumnNames.WorkingGroupID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingGroupCode
		{
			get
			{
				return new esQueryItem(this, WorkingGroupMetadata.ColumnNames.WorkingGroupCode, esSystemType.String);
			}
		} 
		
		public esQueryItem WorkingGroupName
		{
			get
			{
				return new esQueryItem(this, WorkingGroupMetadata.ColumnNames.WorkingGroupName, esSystemType.String);
			}
		} 
		
		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, WorkingGroupMetadata.ColumnNames.Note, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, WorkingGroupMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, WorkingGroupMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("WorkingGroupCollection")]
	public partial class WorkingGroupCollection : esWorkingGroupCollection, IEnumerable<WorkingGroup>
	{
		public WorkingGroupCollection()
		{

		}
		
		public static implicit operator List<WorkingGroup>(WorkingGroupCollection coll)
		{
			List<WorkingGroup> list = new List<WorkingGroup>();
			
			foreach (WorkingGroup emp in coll)
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
				return  WorkingGroupMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WorkingGroupQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new WorkingGroup(row);
		}

		override protected esEntity CreateEntity()
		{
			return new WorkingGroup();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public WorkingGroupQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WorkingGroupQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(WorkingGroupQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public WorkingGroup AddNew()
		{
			WorkingGroup entity = base.AddNewEntity() as WorkingGroup;
			
			return entity;
		}

		public WorkingGroup FindByPrimaryKey(System.Int32 workingGroupID)
		{
			return base.FindByPrimaryKey(workingGroupID) as WorkingGroup;
		}


		#region IEnumerable<WorkingGroup> Members

		IEnumerator<WorkingGroup> IEnumerable<WorkingGroup>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as WorkingGroup;
			}
		}

		#endregion
		
		private WorkingGroupQuery query;
	}


	/// <summary>
	/// Encapsulates the 'WorkingGroup' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("WorkingGroup ({WorkingGroupID})")]
	[Serializable]
	public partial class WorkingGroup : esWorkingGroup
	{
		public WorkingGroup()
		{

		}
	
		public WorkingGroup(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return WorkingGroupMetadata.Meta();
			}
		}
		
		
		
		override protected esWorkingGroupQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WorkingGroupQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public WorkingGroupQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WorkingGroupQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(WorkingGroupQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private WorkingGroupQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class WorkingGroupQuery : esWorkingGroupQuery
	{
		public WorkingGroupQuery()
		{

		}		
		
		public WorkingGroupQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "WorkingGroupQuery";
        }
		
			
	}


	[Serializable]
	public partial class WorkingGroupMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected WorkingGroupMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(WorkingGroupMetadata.ColumnNames.WorkingGroupID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingGroupMetadata.PropertyNames.WorkingGroupID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingGroupMetadata.ColumnNames.WorkingGroupCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingGroupMetadata.PropertyNames.WorkingGroupCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingGroupMetadata.ColumnNames.WorkingGroupName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingGroupMetadata.PropertyNames.WorkingGroupName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingGroupMetadata.ColumnNames.Note, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingGroupMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingGroupMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingGroupMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingGroupMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingGroupMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public WorkingGroupMetadata Meta()
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
			 public const string WorkingGroupCode = "WorkingGroupCode";
			 public const string WorkingGroupName = "WorkingGroupName";
			 public const string Note = "Note";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string WorkingGroupID = "WorkingGroupID";
			 public const string WorkingGroupCode = "WorkingGroupCode";
			 public const string WorkingGroupName = "WorkingGroupName";
			 public const string Note = "Note";
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
			lock (typeof(WorkingGroupMetadata))
			{
				if(WorkingGroupMetadata.mapDelegates == null)
				{
					WorkingGroupMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (WorkingGroupMetadata.meta == null)
				{
					WorkingGroupMetadata.meta = new WorkingGroupMetadata();
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
				meta.AddTypeMap("WorkingGroupCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("WorkingGroupName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "WorkingGroup";
				meta.Destination = "WorkingGroup";
				
				meta.spInsert = "proc_WorkingGroupInsert";				
				meta.spUpdate = "proc_WorkingGroupUpdate";		
				meta.spDelete = "proc_WorkingGroupDelete";
				meta.spLoadAll = "proc_WorkingGroupLoadAll";
				meta.spLoadByPrimaryKey = "proc_WorkingGroupLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private WorkingGroupMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
