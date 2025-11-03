/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:22 PM
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
	abstract public class esPositionLevelCollection : esEntityCollectionWAuditLog
	{
		public esPositionLevelCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PositionLevelCollection";
		}

		#region Query Logic
		protected void InitQuery(esPositionLevelQuery query)
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
			this.InitQuery(query as esPositionLevelQuery);
		}
		#endregion
		
		virtual public PositionLevel DetachEntity(PositionLevel entity)
		{
			return base.DetachEntity(entity) as PositionLevel;
		}
		
		virtual public PositionLevel AttachEntity(PositionLevel entity)
		{
			return base.AttachEntity(entity) as PositionLevel;
		}
		
		virtual public void Combine(PositionLevelCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PositionLevel this[int index]
		{
			get
			{
				return base[index] as PositionLevel;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PositionLevel);
		}
	}



	[Serializable]
	abstract public class esPositionLevel : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPositionLevelQuery GetDynamicQuery()
		{
			return null;
		}

		public esPositionLevel()
		{

		}

		public esPositionLevel(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 positionLevelID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionLevelID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionLevelID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 positionLevelID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionLevelID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionLevelID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 positionLevelID)
		{
			esPositionLevelQuery query = this.GetDynamicQuery();
			query.Where(query.PositionLevelID == positionLevelID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 positionLevelID)
		{
			esParameters parms = new esParameters();
			parms.Add("PositionLevelID",positionLevelID);
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
						case "PositionLevelID": this.str.PositionLevelID = (string)value; break;							
						case "PositionLevelCode": this.str.PositionLevelCode = (string)value; break;							
						case "PositionLevelName": this.str.PositionLevelName = (string)value; break;							
						case "Ranking": this.str.Ranking = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PositionLevelID":
						
							if (value == null || value is System.Int32)
								this.PositionLevelID = (System.Int32?)value;
							break;
						
						case "Ranking":
						
							if (value == null || value is System.Int16)
								this.Ranking = (System.Int16?)value;
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
		/// Maps to PositionLevel.PositionLevelID
		/// </summary>
		virtual public System.Int32? PositionLevelID
		{
			get
			{
				return base.GetSystemInt32(PositionLevelMetadata.ColumnNames.PositionLevelID);
			}
			
			set
			{
				base.SetSystemInt32(PositionLevelMetadata.ColumnNames.PositionLevelID, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionLevel.PositionLevelCode
		/// </summary>
		virtual public System.String PositionLevelCode
		{
			get
			{
				return base.GetSystemString(PositionLevelMetadata.ColumnNames.PositionLevelCode);
			}
			
			set
			{
				base.SetSystemString(PositionLevelMetadata.ColumnNames.PositionLevelCode, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionLevel.PositionLevelName
		/// </summary>
		virtual public System.String PositionLevelName
		{
			get
			{
				return base.GetSystemString(PositionLevelMetadata.ColumnNames.PositionLevelName);
			}
			
			set
			{
				base.SetSystemString(PositionLevelMetadata.ColumnNames.PositionLevelName, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionLevel.Ranking
		/// </summary>
		virtual public System.Int16? Ranking
		{
			get
			{
				return base.GetSystemInt16(PositionLevelMetadata.ColumnNames.Ranking);
			}
			
			set
			{
				base.SetSystemInt16(PositionLevelMetadata.ColumnNames.Ranking, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionLevel.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PositionLevelMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PositionLevelMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionLevel.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PositionLevelMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PositionLevelMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPositionLevel entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PositionLevelID
			{
				get
				{
					System.Int32? data = entity.PositionLevelID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionLevelID = null;
					else entity.PositionLevelID = Convert.ToInt32(value);
				}
			}
				
			public System.String PositionLevelCode
			{
				get
				{
					System.String data = entity.PositionLevelCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionLevelCode = null;
					else entity.PositionLevelCode = Convert.ToString(value);
				}
			}
				
			public System.String PositionLevelName
			{
				get
				{
					System.String data = entity.PositionLevelName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionLevelName = null;
					else entity.PositionLevelName = Convert.ToString(value);
				}
			}
				
			public System.String Ranking
			{
				get
				{
					System.Int16? data = entity.Ranking;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Ranking = null;
					else entity.Ranking = Convert.ToInt16(value);
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
			

			private esPositionLevel entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPositionLevelQuery query)
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
				throw new Exception("esPositionLevel can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PositionLevel : esPositionLevel
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
	abstract public class esPositionLevelQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PositionLevelMetadata.Meta();
			}
		}	
		

		public esQueryItem PositionLevelID
		{
			get
			{
				return new esQueryItem(this, PositionLevelMetadata.ColumnNames.PositionLevelID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PositionLevelCode
		{
			get
			{
				return new esQueryItem(this, PositionLevelMetadata.ColumnNames.PositionLevelCode, esSystemType.String);
			}
		} 
		
		public esQueryItem PositionLevelName
		{
			get
			{
				return new esQueryItem(this, PositionLevelMetadata.ColumnNames.PositionLevelName, esSystemType.String);
			}
		} 
		
		public esQueryItem Ranking
		{
			get
			{
				return new esQueryItem(this, PositionLevelMetadata.ColumnNames.Ranking, esSystemType.Int16);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PositionLevelMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PositionLevelMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PositionLevelCollection")]
	public partial class PositionLevelCollection : esPositionLevelCollection, IEnumerable<PositionLevel>
	{
		public PositionLevelCollection()
		{

		}
		
		public static implicit operator List<PositionLevel>(PositionLevelCollection coll)
		{
			List<PositionLevel> list = new List<PositionLevel>();
			
			foreach (PositionLevel emp in coll)
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
				return  PositionLevelMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionLevelQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PositionLevel(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PositionLevel();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PositionLevelQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionLevelQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PositionLevelQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PositionLevel AddNew()
		{
			PositionLevel entity = base.AddNewEntity() as PositionLevel;
			
			return entity;
		}

		public PositionLevel FindByPrimaryKey(System.Int32 positionLevelID)
		{
			return base.FindByPrimaryKey(positionLevelID) as PositionLevel;
		}


		#region IEnumerable<PositionLevel> Members

		IEnumerator<PositionLevel> IEnumerable<PositionLevel>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PositionLevel;
			}
		}

		#endregion
		
		private PositionLevelQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PositionLevel' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PositionLevel ({PositionLevelID})")]
	[Serializable]
	public partial class PositionLevel : esPositionLevel
	{
		public PositionLevel()
		{

		}
	
		public PositionLevel(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PositionLevelMetadata.Meta();
			}
		}
		
		
		
		override protected esPositionLevelQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionLevelQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PositionLevelQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionLevelQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PositionLevelQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PositionLevelQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PositionLevelQuery : esPositionLevelQuery
	{
		public PositionLevelQuery()
		{

		}		
		
		public PositionLevelQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PositionLevelQuery";
        }
		
			
	}


	[Serializable]
	public partial class PositionLevelMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PositionLevelMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PositionLevelMetadata.ColumnNames.PositionLevelID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionLevelMetadata.PropertyNames.PositionLevelID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionLevelMetadata.ColumnNames.PositionLevelCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionLevelMetadata.PropertyNames.PositionLevelCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionLevelMetadata.ColumnNames.PositionLevelName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionLevelMetadata.PropertyNames.PositionLevelName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionLevelMetadata.ColumnNames.Ranking, 3, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = PositionLevelMetadata.PropertyNames.Ranking;
			c.NumericPrecision = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionLevelMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PositionLevelMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionLevelMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionLevelMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PositionLevelMetadata Meta()
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
			 public const string PositionLevelID = "PositionLevelID";
			 public const string PositionLevelCode = "PositionLevelCode";
			 public const string PositionLevelName = "PositionLevelName";
			 public const string Ranking = "Ranking";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PositionLevelID = "PositionLevelID";
			 public const string PositionLevelCode = "PositionLevelCode";
			 public const string PositionLevelName = "PositionLevelName";
			 public const string Ranking = "Ranking";
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
			lock (typeof(PositionLevelMetadata))
			{
				if(PositionLevelMetadata.mapDelegates == null)
				{
					PositionLevelMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PositionLevelMetadata.meta == null)
				{
					PositionLevelMetadata.meta = new PositionLevelMetadata();
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
				

				meta.AddTypeMap("PositionLevelID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionLevelCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("PositionLevelName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Ranking", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "PositionLevel";
				meta.Destination = "PositionLevel";
				
				meta.spInsert = "proc_PositionLevelInsert";				
				meta.spUpdate = "proc_PositionLevelUpdate";		
				meta.spDelete = "proc_PositionLevelDelete";
				meta.spLoadAll = "proc_PositionLevelLoadAll";
				meta.spLoadByPrimaryKey = "proc_PositionLevelLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PositionLevelMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
