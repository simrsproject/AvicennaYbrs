/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/16/2015 10:28:05 AM
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
	abstract public class esIncidentTypeCollection : esEntityCollectionWAuditLog
	{
		public esIncidentTypeCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "IncidentTypeCollection";
		}

		#region Query Logic
		protected void InitQuery(esIncidentTypeQuery query)
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
			this.InitQuery(query as esIncidentTypeQuery);
		}
		#endregion
		
		virtual public IncidentType DetachEntity(IncidentType entity)
		{
			return base.DetachEntity(entity) as IncidentType;
		}
		
		virtual public IncidentType AttachEntity(IncidentType entity)
		{
			return base.AttachEntity(entity) as IncidentType;
		}
		
		virtual public void Combine(IncidentTypeCollection collection)
		{
			base.Combine(collection);
		}
		
		new public IncidentType this[int index]
		{
			get
			{
				return base[index] as IncidentType;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(IncidentType);
		}
	}



	[Serializable]
	abstract public class esIncidentType : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esIncidentTypeQuery GetDynamicQuery()
		{
			return null;
		}

		public esIncidentType()
		{

		}

		public esIncidentType(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String sRIncidentType, System.String componentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRIncidentType, componentID);
			else
				return LoadByPrimaryKeyStoredProcedure(sRIncidentType, componentID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String sRIncidentType, System.String componentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRIncidentType, componentID);
			else
				return LoadByPrimaryKeyStoredProcedure(sRIncidentType, componentID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String sRIncidentType, System.String componentID)
		{
			esIncidentTypeQuery query = this.GetDynamicQuery();
			query.Where(query.SRIncidentType == sRIncidentType, query.ComponentID == componentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String sRIncidentType, System.String componentID)
		{
			esParameters parms = new esParameters();
			parms.Add("SRIncidentType",sRIncidentType);			parms.Add("ComponentID",componentID);
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
						case "SRIncidentType": this.str.SRIncidentType = (string)value; break;							
						case "ComponentID": this.str.ComponentID = (string)value; break;							
						case "ComponentName": this.str.ComponentName = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
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
		/// Maps to IncidentType.SRIncidentType
		/// </summary>
		virtual public System.String SRIncidentType
		{
			get
			{
				return base.GetSystemString(IncidentTypeMetadata.ColumnNames.SRIncidentType);
			}
			
			set
			{
				base.SetSystemString(IncidentTypeMetadata.ColumnNames.SRIncidentType, value);
			}
		}
		
		/// <summary>
		/// Maps to IncidentType.ComponentID
		/// </summary>
		virtual public System.String ComponentID
		{
			get
			{
				return base.GetSystemString(IncidentTypeMetadata.ColumnNames.ComponentID);
			}
			
			set
			{
				base.SetSystemString(IncidentTypeMetadata.ColumnNames.ComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to IncidentType.ComponentName
		/// </summary>
		virtual public System.String ComponentName
		{
			get
			{
				return base.GetSystemString(IncidentTypeMetadata.ColumnNames.ComponentName);
			}
			
			set
			{
				base.SetSystemString(IncidentTypeMetadata.ColumnNames.ComponentName, value);
			}
		}
		
		/// <summary>
		/// Maps to IncidentType.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(IncidentTypeMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(IncidentTypeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to IncidentType.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(IncidentTypeMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(IncidentTypeMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esIncidentType entity)
			{
				this.entity = entity;
			}
			
	
			public System.String SRIncidentType
			{
				get
				{
					System.String data = entity.SRIncidentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncidentType = null;
					else entity.SRIncidentType = Convert.ToString(value);
				}
			}
				
			public System.String ComponentID
			{
				get
				{
					System.String data = entity.ComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ComponentID = null;
					else entity.ComponentID = Convert.ToString(value);
				}
			}
				
			public System.String ComponentName
			{
				get
				{
					System.String data = entity.ComponentName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ComponentName = null;
					else entity.ComponentName = Convert.ToString(value);
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
			

			private esIncidentType entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esIncidentTypeQuery query)
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
				throw new Exception("esIncidentType can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class IncidentType : esIncidentType
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
	abstract public class esIncidentTypeQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return IncidentTypeMetadata.Meta();
			}
		}	
		

		public esQueryItem SRIncidentType
		{
			get
			{
				return new esQueryItem(this, IncidentTypeMetadata.ColumnNames.SRIncidentType, esSystemType.String);
			}
		} 
		
		public esQueryItem ComponentID
		{
			get
			{
				return new esQueryItem(this, IncidentTypeMetadata.ColumnNames.ComponentID, esSystemType.String);
			}
		} 
		
		public esQueryItem ComponentName
		{
			get
			{
				return new esQueryItem(this, IncidentTypeMetadata.ColumnNames.ComponentName, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, IncidentTypeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, IncidentTypeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("IncidentTypeCollection")]
	public partial class IncidentTypeCollection : esIncidentTypeCollection, IEnumerable<IncidentType>
	{
		public IncidentTypeCollection()
		{

		}
		
		public static implicit operator List<IncidentType>(IncidentTypeCollection coll)
		{
			List<IncidentType> list = new List<IncidentType>();
			
			foreach (IncidentType emp in coll)
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
				return  IncidentTypeMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new IncidentTypeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new IncidentType(row);
		}

		override protected esEntity CreateEntity()
		{
			return new IncidentType();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public IncidentTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new IncidentTypeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(IncidentTypeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public IncidentType AddNew()
		{
			IncidentType entity = base.AddNewEntity() as IncidentType;
			
			return entity;
		}

		public IncidentType FindByPrimaryKey(System.String sRIncidentType, System.String componentID)
		{
			return base.FindByPrimaryKey(sRIncidentType, componentID) as IncidentType;
		}


		#region IEnumerable<IncidentType> Members

		IEnumerator<IncidentType> IEnumerable<IncidentType>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as IncidentType;
			}
		}

		#endregion
		
		private IncidentTypeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'IncidentType' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("IncidentType ({SRIncidentType},{ComponentID})")]
	[Serializable]
	public partial class IncidentType : esIncidentType
	{
		public IncidentType()
		{

		}
	
		public IncidentType(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return IncidentTypeMetadata.Meta();
			}
		}
		
		
		
		override protected esIncidentTypeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new IncidentTypeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public IncidentTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new IncidentTypeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(IncidentTypeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private IncidentTypeQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class IncidentTypeQuery : esIncidentTypeQuery
	{
		public IncidentTypeQuery()
		{

		}		
		
		public IncidentTypeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "IncidentTypeQuery";
        }
		
			
	}


	[Serializable]
	public partial class IncidentTypeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected IncidentTypeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(IncidentTypeMetadata.ColumnNames.SRIncidentType, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = IncidentTypeMetadata.PropertyNames.SRIncidentType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(IncidentTypeMetadata.ColumnNames.ComponentID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = IncidentTypeMetadata.PropertyNames.ComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(IncidentTypeMetadata.ColumnNames.ComponentName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = IncidentTypeMetadata.PropertyNames.ComponentName;
			c.CharacterMaxLength = 250;
			_columns.Add(c);
				
			c = new esColumnMetadata(IncidentTypeMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = IncidentTypeMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(IncidentTypeMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = IncidentTypeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public IncidentTypeMetadata Meta()
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
			 public const string SRIncidentType = "SRIncidentType";
			 public const string ComponentID = "ComponentID";
			 public const string ComponentName = "ComponentName";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SRIncidentType = "SRIncidentType";
			 public const string ComponentID = "ComponentID";
			 public const string ComponentName = "ComponentName";
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
			lock (typeof(IncidentTypeMetadata))
			{
				if(IncidentTypeMetadata.mapDelegates == null)
				{
					IncidentTypeMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (IncidentTypeMetadata.meta == null)
				{
					IncidentTypeMetadata.meta = new IncidentTypeMetadata();
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
				

				meta.AddTypeMap("SRIncidentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ComponentName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "IncidentType";
				meta.Destination = "IncidentType";
				
				meta.spInsert = "proc_IncidentTypeInsert";				
				meta.spUpdate = "proc_IncidentTypeUpdate";		
				meta.spDelete = "proc_IncidentTypeDelete";
				meta.spLoadAll = "proc_IncidentTypeLoadAll";
				meta.spLoadByPrimaryKey = "proc_IncidentTypeLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private IncidentTypeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
