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
	abstract public class esMorphologyCollection : esEntityCollectionWAuditLog
	{
		public esMorphologyCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "MorphologyCollection";
		}

		#region Query Logic
		protected void InitQuery(esMorphologyQuery query)
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
			this.InitQuery(query as esMorphologyQuery);
		}
		#endregion
		
		virtual public Morphology DetachEntity(Morphology entity)
		{
			return base.DetachEntity(entity) as Morphology;
		}
		
		virtual public Morphology AttachEntity(Morphology entity)
		{
			return base.AttachEntity(entity) as Morphology;
		}
		
		virtual public void Combine(MorphologyCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Morphology this[int index]
		{
			get
			{
				return base[index] as Morphology;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Morphology);
		}
	}



	[Serializable]
	abstract public class esMorphology : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMorphologyQuery GetDynamicQuery()
		{
			return null;
		}

		public esMorphology()
		{

		}

		public esMorphology(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String morphologyID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(morphologyID);
			else
				return LoadByPrimaryKeyStoredProcedure(morphologyID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String morphologyID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(morphologyID);
			else
				return LoadByPrimaryKeyStoredProcedure(morphologyID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String morphologyID)
		{
			esMorphologyQuery query = this.GetDynamicQuery();
			query.Where(query.MorphologyID == morphologyID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String morphologyID)
		{
			esParameters parms = new esParameters();
			parms.Add("MorphologyID",morphologyID);
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
						case "MorphologyID": this.str.MorphologyID = (string)value; break;							
						case "DiagnoseID": this.str.DiagnoseID = (string)value; break;							
						case "MorphologyName": this.str.MorphologyName = (string)value; break;							
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
		/// Maps to Morphology.MorphologyID
		/// </summary>
		virtual public System.String MorphologyID
		{
			get
			{
				return base.GetSystemString(MorphologyMetadata.ColumnNames.MorphologyID);
			}
			
			set
			{
				base.SetSystemString(MorphologyMetadata.ColumnNames.MorphologyID, value);
			}
		}
		
		/// <summary>
		/// Maps to Morphology.DiagnoseID
		/// </summary>
		virtual public System.String DiagnoseID
		{
			get
			{
				return base.GetSystemString(MorphologyMetadata.ColumnNames.DiagnoseID);
			}
			
			set
			{
				base.SetSystemString(MorphologyMetadata.ColumnNames.DiagnoseID, value);
			}
		}
		
		/// <summary>
		/// Maps to Morphology.MorphologyName
		/// </summary>
		virtual public System.String MorphologyName
		{
			get
			{
				return base.GetSystemString(MorphologyMetadata.ColumnNames.MorphologyName);
			}
			
			set
			{
				base.SetSystemString(MorphologyMetadata.ColumnNames.MorphologyName, value);
			}
		}
		
		/// <summary>
		/// Maps to Morphology.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MorphologyMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MorphologyMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Morphology.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MorphologyMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(MorphologyMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMorphology entity)
			{
				this.entity = entity;
			}
			
	
			public System.String MorphologyID
			{
				get
				{
					System.String data = entity.MorphologyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MorphologyID = null;
					else entity.MorphologyID = Convert.ToString(value);
				}
			}
				
			public System.String DiagnoseID
			{
				get
				{
					System.String data = entity.DiagnoseID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiagnoseID = null;
					else entity.DiagnoseID = Convert.ToString(value);
				}
			}
				
			public System.String MorphologyName
			{
				get
				{
					System.String data = entity.MorphologyName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MorphologyName = null;
					else entity.MorphologyName = Convert.ToString(value);
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
			

			private esMorphology entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMorphologyQuery query)
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
				throw new Exception("esMorphology can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class Morphology : esMorphology
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
	abstract public class esMorphologyQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return MorphologyMetadata.Meta();
			}
		}	
		

		public esQueryItem MorphologyID
		{
			get
			{
				return new esQueryItem(this, MorphologyMetadata.ColumnNames.MorphologyID, esSystemType.String);
			}
		} 
		
		public esQueryItem DiagnoseID
		{
			get
			{
				return new esQueryItem(this, MorphologyMetadata.ColumnNames.DiagnoseID, esSystemType.String);
			}
		} 
		
		public esQueryItem MorphologyName
		{
			get
			{
				return new esQueryItem(this, MorphologyMetadata.ColumnNames.MorphologyName, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MorphologyMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MorphologyMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MorphologyCollection")]
	public partial class MorphologyCollection : esMorphologyCollection, IEnumerable<Morphology>
	{
		public MorphologyCollection()
		{

		}
		
		public static implicit operator List<Morphology>(MorphologyCollection coll)
		{
			List<Morphology> list = new List<Morphology>();
			
			foreach (Morphology emp in coll)
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
				return  MorphologyMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MorphologyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Morphology(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Morphology();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public MorphologyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MorphologyQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(MorphologyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Morphology AddNew()
		{
			Morphology entity = base.AddNewEntity() as Morphology;
			
			return entity;
		}

		public Morphology FindByPrimaryKey(System.String morphologyID)
		{
			return base.FindByPrimaryKey(morphologyID) as Morphology;
		}


		#region IEnumerable<Morphology> Members

		IEnumerator<Morphology> IEnumerable<Morphology>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Morphology;
			}
		}

		#endregion
		
		private MorphologyQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Morphology' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Morphology ({MorphologyID})")]
	[Serializable]
	public partial class Morphology : esMorphology
	{
		public Morphology()
		{

		}
	
		public Morphology(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MorphologyMetadata.Meta();
			}
		}
		
		
		
		override protected esMorphologyQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MorphologyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public MorphologyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MorphologyQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(MorphologyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private MorphologyQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class MorphologyQuery : esMorphologyQuery
	{
		public MorphologyQuery()
		{

		}		
		
		public MorphologyQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "MorphologyQuery";
        }
		
			
	}


	[Serializable]
	public partial class MorphologyMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MorphologyMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MorphologyMetadata.ColumnNames.MorphologyID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MorphologyMetadata.PropertyNames.MorphologyID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(MorphologyMetadata.ColumnNames.DiagnoseID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = MorphologyMetadata.PropertyNames.DiagnoseID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MorphologyMetadata.ColumnNames.MorphologyName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = MorphologyMetadata.PropertyNames.MorphologyName;
			c.CharacterMaxLength = 500;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(MorphologyMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MorphologyMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MorphologyMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = MorphologyMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public MorphologyMetadata Meta()
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
			 public const string MorphologyID = "MorphologyID";
			 public const string DiagnoseID = "DiagnoseID";
			 public const string MorphologyName = "MorphologyName";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string MorphologyID = "MorphologyID";
			 public const string DiagnoseID = "DiagnoseID";
			 public const string MorphologyName = "MorphologyName";
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
			lock (typeof(MorphologyMetadata))
			{
				if(MorphologyMetadata.mapDelegates == null)
				{
					MorphologyMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MorphologyMetadata.meta == null)
				{
					MorphologyMetadata.meta = new MorphologyMetadata();
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
				

				meta.AddTypeMap("MorphologyID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiagnoseID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MorphologyName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Morphology";
				meta.Destination = "Morphology";
				
				meta.spInsert = "proc_MorphologyInsert";				
				meta.spUpdate = "proc_MorphologyUpdate";		
				meta.spDelete = "proc_MorphologyDelete";
				meta.spLoadAll = "proc_MorphologyLoadAll";
				meta.spLoadByPrimaryKey = "proc_MorphologyLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MorphologyMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
