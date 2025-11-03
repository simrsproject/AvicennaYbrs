/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/14/2017 4:07:08 PM
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
	abstract public class esJournalGroupCollection : esEntityCollectionWAuditLog
	{
		public esJournalGroupCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "JournalGroupCollection";
		}

		#region Query Logic
		protected void InitQuery(esJournalGroupQuery query)
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
			this.InitQuery(query as esJournalGroupQuery);
		}
		#endregion
		
		virtual public JournalGroup DetachEntity(JournalGroup entity)
		{
			return base.DetachEntity(entity) as JournalGroup;
		}
		
		virtual public JournalGroup AttachEntity(JournalGroup entity)
		{
			return base.AttachEntity(entity) as JournalGroup;
		}
		
		virtual public void Combine(JournalGroupCollection collection)
		{
			base.Combine(collection);
		}
		
		new public JournalGroup this[int index]
		{
			get
			{
				return base[index] as JournalGroup;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(JournalGroup);
		}
	}



	[Serializable]
	abstract public class esJournalGroup : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esJournalGroupQuery GetDynamicQuery()
		{
			return null;
		}

		public esJournalGroup()
		{

		}

		public esJournalGroup(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 journalGroupID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(journalGroupID);
			else
				return LoadByPrimaryKeyStoredProcedure(journalGroupID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 journalGroupID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(journalGroupID);
			else
				return LoadByPrimaryKeyStoredProcedure(journalGroupID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 journalGroupID)
		{
			esJournalGroupQuery query = this.GetDynamicQuery();
			query.Where(query.JournalGroupID == journalGroupID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 journalGroupID)
		{
			esParameters parms = new esParameters();
			parms.Add("JournalGroupID",journalGroupID);
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
						case "JournalGroupID": this.str.JournalGroupID = (string)value; break;							
						case "JournalGroupName": this.str.JournalGroupName = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "JournalGroupID":
						
							if (value == null || value is System.Int32)
								this.JournalGroupID = (System.Int32?)value;
							break;
						
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to JournalGroup.JournalGroupID
		/// </summary>
		virtual public System.Int32? JournalGroupID
		{
			get
			{
				return base.GetSystemInt32(JournalGroupMetadata.ColumnNames.JournalGroupID);
			}
			
			set
			{
				base.SetSystemInt32(JournalGroupMetadata.ColumnNames.JournalGroupID, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalGroup.JournalGroupName
		/// </summary>
		virtual public System.String JournalGroupName
		{
			get
			{
				return base.GetSystemString(JournalGroupMetadata.ColumnNames.JournalGroupName);
			}
			
			set
			{
				base.SetSystemString(JournalGroupMetadata.ColumnNames.JournalGroupName, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalGroup.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(JournalGroupMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(JournalGroupMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalGroup.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(JournalGroupMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(JournalGroupMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalGroup.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(JournalGroupMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(JournalGroupMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalGroup.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(JournalGroupMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(JournalGroupMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esJournalGroup entity)
			{
				this.entity = entity;
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
				
			public System.String JournalGroupName
			{
				get
				{
					System.String data = entity.JournalGroupName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalGroupName = null;
					else entity.JournalGroupName = Convert.ToString(value);
				}
			}
				
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
				}
			}
				
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			

			private esJournalGroup entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esJournalGroupQuery query)
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
				throw new Exception("esJournalGroup can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class JournalGroup : esJournalGroup
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
	abstract public class esJournalGroupQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return JournalGroupMetadata.Meta();
			}
		}	
		

		public esQueryItem JournalGroupID
		{
			get
			{
				return new esQueryItem(this, JournalGroupMetadata.ColumnNames.JournalGroupID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem JournalGroupName
		{
			get
			{
				return new esQueryItem(this, JournalGroupMetadata.ColumnNames.JournalGroupName, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, JournalGroupMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, JournalGroupMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, JournalGroupMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, JournalGroupMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("JournalGroupCollection")]
	public partial class JournalGroupCollection : esJournalGroupCollection, IEnumerable<JournalGroup>
	{
		public JournalGroupCollection()
		{

		}
		
		public static implicit operator List<JournalGroup>(JournalGroupCollection coll)
		{
			List<JournalGroup> list = new List<JournalGroup>();
			
			foreach (JournalGroup emp in coll)
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
				return  JournalGroupMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new JournalGroupQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new JournalGroup(row);
		}

		override protected esEntity CreateEntity()
		{
			return new JournalGroup();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public JournalGroupQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new JournalGroupQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(JournalGroupQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public JournalGroup AddNew()
		{
			JournalGroup entity = base.AddNewEntity() as JournalGroup;
			
			return entity;
		}

		public JournalGroup FindByPrimaryKey(System.Int32 journalGroupID)
		{
			return base.FindByPrimaryKey(journalGroupID) as JournalGroup;
		}


		#region IEnumerable<JournalGroup> Members

		IEnumerator<JournalGroup> IEnumerable<JournalGroup>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as JournalGroup;
			}
		}

		#endregion
		
		private JournalGroupQuery query;
	}


	/// <summary>
	/// Encapsulates the 'JournalGroup' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("JournalGroup ({JournalGroupID})")]
	[Serializable]
	public partial class JournalGroup : esJournalGroup
	{
		public JournalGroup()
		{

		}
	
		public JournalGroup(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return JournalGroupMetadata.Meta();
			}
		}
		
		
		
		override protected esJournalGroupQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new JournalGroupQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public JournalGroupQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new JournalGroupQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(JournalGroupQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private JournalGroupQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class JournalGroupQuery : esJournalGroupQuery
	{
		public JournalGroupQuery()
		{

		}		
		
		public JournalGroupQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "JournalGroupQuery";
        }
		
			
	}


	[Serializable]
	public partial class JournalGroupMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected JournalGroupMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(JournalGroupMetadata.ColumnNames.JournalGroupID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = JournalGroupMetadata.PropertyNames.JournalGroupID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalGroupMetadata.ColumnNames.JournalGroupName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalGroupMetadata.PropertyNames.JournalGroupName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalGroupMetadata.ColumnNames.Notes, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalGroupMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalGroupMetadata.ColumnNames.IsActive, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = JournalGroupMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalGroupMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = JournalGroupMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalGroupMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalGroupMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public JournalGroupMetadata Meta()
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
			 public const string JournalGroupID = "JournalGroupID";
			 public const string JournalGroupName = "JournalGroupName";
			 public const string Notes = "Notes";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string JournalGroupID = "JournalGroupID";
			 public const string JournalGroupName = "JournalGroupName";
			 public const string Notes = "Notes";
			 public const string IsActive = "IsActive";
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
			lock (typeof(JournalGroupMetadata))
			{
				if(JournalGroupMetadata.mapDelegates == null)
				{
					JournalGroupMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (JournalGroupMetadata.meta == null)
				{
					JournalGroupMetadata.meta = new JournalGroupMetadata();
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
				

				meta.AddTypeMap("JournalGroupID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("JournalGroupName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "JournalGroup";
				meta.Destination = "JournalGroup";
				
				meta.spInsert = "proc_JournalGroupInsert";				
				meta.spUpdate = "proc_JournalGroupUpdate";		
				meta.spDelete = "proc_JournalGroupDelete";
				meta.spLoadAll = "proc_JournalGroupLoadAll";
				meta.spLoadByPrimaryKey = "proc_JournalGroupLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private JournalGroupMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
