/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:11 PM
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
	abstract public class esAssetBookCollection : esEntityCollectionWAuditLog
	{
		public esAssetBookCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AssetBookCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetBookQuery query)
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
			this.InitQuery(query as esAssetBookQuery);
		}
		#endregion
		
		virtual public AssetBook DetachEntity(AssetBook entity)
		{
			return base.DetachEntity(entity) as AssetBook;
		}
		
		virtual public AssetBook AttachEntity(AssetBook entity)
		{
			return base.AttachEntity(entity) as AssetBook;
		}
		
		virtual public void Combine(AssetBookCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AssetBook this[int index]
		{
			get
			{
				return base[index] as AssetBook;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetBook);
		}
	}



	[Serializable]
	abstract public class esAssetBook : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetBookQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetBook()
		{

		}

		public esAssetBook(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String assetBookID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetBookID);
			else
				return LoadByPrimaryKeyStoredProcedure(assetBookID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String assetBookID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetBookID);
			else
				return LoadByPrimaryKeyStoredProcedure(assetBookID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String assetBookID)
		{
			esAssetBookQuery query = this.GetDynamicQuery();
			query.Where(query.AssetBookID == assetBookID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String assetBookID)
		{
			esParameters parms = new esParameters();
			parms.Add("AssetBookID",assetBookID);
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
						case "AssetBookID": this.str.AssetBookID = (string)value; break;							
						case "AssetBookName": this.str.AssetBookName = (string)value; break;							
						case "DepreciationMethodID": this.str.DepreciationMethodID = (string)value; break;							
						case "SRAssetType": this.str.SRAssetType = (string)value; break;							
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
		/// Maps to AssetBook.AssetBookID
		/// </summary>
		virtual public System.String AssetBookID
		{
			get
			{
				return base.GetSystemString(AssetBookMetadata.ColumnNames.AssetBookID);
			}
			
			set
			{
				base.SetSystemString(AssetBookMetadata.ColumnNames.AssetBookID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetBook.AssetBookName
		/// </summary>
		virtual public System.String AssetBookName
		{
			get
			{
				return base.GetSystemString(AssetBookMetadata.ColumnNames.AssetBookName);
			}
			
			set
			{
				base.SetSystemString(AssetBookMetadata.ColumnNames.AssetBookName, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetBook.DepreciationMethodID
		/// </summary>
		virtual public System.String DepreciationMethodID
		{
			get
			{
				return base.GetSystemString(AssetBookMetadata.ColumnNames.DepreciationMethodID);
			}
			
			set
			{
				if(base.SetSystemString(AssetBookMetadata.ColumnNames.DepreciationMethodID, value))
				{
					this._UpToAssetDepreciationMethodByDepreciationMethodID = null;
				}
			}
		}
		
		/// <summary>
		/// Maps to AssetBook.SRAssetType
		/// </summary>
		virtual public System.String SRAssetType
		{
			get
			{
				return base.GetSystemString(AssetBookMetadata.ColumnNames.SRAssetType);
			}
			
			set
			{
				base.SetSystemString(AssetBookMetadata.ColumnNames.SRAssetType, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetBook.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetBookMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AssetBookMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetBook.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AssetBookMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AssetBookMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		[CLSCompliant(false)]
		internal protected AssetDepreciationMethod _UpToAssetDepreciationMethodByDepreciationMethodID;
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
			public esStrings(esAssetBook entity)
			{
				this.entity = entity;
			}
			
	
			public System.String AssetBookID
			{
				get
				{
					System.String data = entity.AssetBookID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetBookID = null;
					else entity.AssetBookID = Convert.ToString(value);
				}
			}
				
			public System.String AssetBookName
			{
				get
				{
					System.String data = entity.AssetBookName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetBookName = null;
					else entity.AssetBookName = Convert.ToString(value);
				}
			}
				
			public System.String DepreciationMethodID
			{
				get
				{
					System.String data = entity.DepreciationMethodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepreciationMethodID = null;
					else entity.DepreciationMethodID = Convert.ToString(value);
				}
			}
				
			public System.String SRAssetType
			{
				get
				{
					System.String data = entity.SRAssetType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAssetType = null;
					else entity.SRAssetType = Convert.ToString(value);
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
			

			private esAssetBook entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetBookQuery query)
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
				throw new Exception("esAssetBook can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AssetBook : esAssetBook
	{

				
		#region UpToAssetDepreciationMethodByDepreciationMethodID - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - RefAssetBookToAssetDepreciationMethod
		/// </summary>

		[XmlIgnore]
		public AssetDepreciationMethod UpToAssetDepreciationMethodByDepreciationMethodID
		{
			get
			{
				if(this._UpToAssetDepreciationMethodByDepreciationMethodID == null
					&& DepreciationMethodID != null					)
				{
					this._UpToAssetDepreciationMethodByDepreciationMethodID = new AssetDepreciationMethod();
					this._UpToAssetDepreciationMethodByDepreciationMethodID.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToAssetDepreciationMethodByDepreciationMethodID", this._UpToAssetDepreciationMethodByDepreciationMethodID);
					this._UpToAssetDepreciationMethodByDepreciationMethodID.Query.Where(this._UpToAssetDepreciationMethodByDepreciationMethodID.Query.DepreciationMethodID == this.DepreciationMethodID);
					this._UpToAssetDepreciationMethodByDepreciationMethodID.Query.Load();
				}

				return this._UpToAssetDepreciationMethodByDepreciationMethodID;
			}
			
			set
			{
				this.RemovePreSave("UpToAssetDepreciationMethodByDepreciationMethodID");
				

				if(value == null)
				{
					this.DepreciationMethodID = null;
					this._UpToAssetDepreciationMethodByDepreciationMethodID = null;
				}
				else
				{
					this.DepreciationMethodID = value.DepreciationMethodID;
					this._UpToAssetDepreciationMethodByDepreciationMethodID = value;
					this.SetPreSave("UpToAssetDepreciationMethodByDepreciationMethodID", this._UpToAssetDepreciationMethodByDepreciationMethodID);
				}
				
			}
		}
		#endregion
		

		
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
	abstract public class esAssetBookQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AssetBookMetadata.Meta();
			}
		}	
		

		public esQueryItem AssetBookID
		{
			get
			{
				return new esQueryItem(this, AssetBookMetadata.ColumnNames.AssetBookID, esSystemType.String);
			}
		} 
		
		public esQueryItem AssetBookName
		{
			get
			{
				return new esQueryItem(this, AssetBookMetadata.ColumnNames.AssetBookName, esSystemType.String);
			}
		} 
		
		public esQueryItem DepreciationMethodID
		{
			get
			{
				return new esQueryItem(this, AssetBookMetadata.ColumnNames.DepreciationMethodID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRAssetType
		{
			get
			{
				return new esQueryItem(this, AssetBookMetadata.ColumnNames.SRAssetType, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetBookMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AssetBookMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetBookCollection")]
	public partial class AssetBookCollection : esAssetBookCollection, IEnumerable<AssetBook>
	{
		public AssetBookCollection()
		{

		}
		
		public static implicit operator List<AssetBook>(AssetBookCollection coll)
		{
			List<AssetBook> list = new List<AssetBook>();
			
			foreach (AssetBook emp in coll)
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
				return  AssetBookMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetBookQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetBook(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetBook();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AssetBookQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetBookQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AssetBookQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AssetBook AddNew()
		{
			AssetBook entity = base.AddNewEntity() as AssetBook;
			
			return entity;
		}

		public AssetBook FindByPrimaryKey(System.String assetBookID)
		{
			return base.FindByPrimaryKey(assetBookID) as AssetBook;
		}


		#region IEnumerable<AssetBook> Members

		IEnumerator<AssetBook> IEnumerable<AssetBook>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AssetBook;
			}
		}

		#endregion
		
		private AssetBookQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetBook' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AssetBook ({AssetBookID})")]
	[Serializable]
	public partial class AssetBook : esAssetBook
	{
		public AssetBook()
		{

		}
	
		public AssetBook(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetBookMetadata.Meta();
			}
		}
		
		
		
		override protected esAssetBookQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetBookQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AssetBookQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetBookQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AssetBookQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AssetBookQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AssetBookQuery : esAssetBookQuery
	{
		public AssetBookQuery()
		{

		}		
		
		public AssetBookQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AssetBookQuery";
        }
		
			
	}


	[Serializable]
	public partial class AssetBookMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetBookMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetBookMetadata.ColumnNames.AssetBookID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetBookMetadata.PropertyNames.AssetBookID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetBookMetadata.ColumnNames.AssetBookName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetBookMetadata.PropertyNames.AssetBookName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetBookMetadata.ColumnNames.DepreciationMethodID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetBookMetadata.PropertyNames.DepreciationMethodID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetBookMetadata.ColumnNames.SRAssetType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetBookMetadata.PropertyNames.SRAssetType;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetBookMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetBookMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetBookMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetBookMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AssetBookMetadata Meta()
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
			 public const string AssetBookID = "AssetBookID";
			 public const string AssetBookName = "AssetBookName";
			 public const string DepreciationMethodID = "DepreciationMethodID";
			 public const string SRAssetType = "SRAssetType";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string AssetBookID = "AssetBookID";
			 public const string AssetBookName = "AssetBookName";
			 public const string DepreciationMethodID = "DepreciationMethodID";
			 public const string SRAssetType = "SRAssetType";
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
			lock (typeof(AssetBookMetadata))
			{
				if(AssetBookMetadata.mapDelegates == null)
				{
					AssetBookMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AssetBookMetadata.meta == null)
				{
					AssetBookMetadata.meta = new AssetBookMetadata();
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
				

				meta.AddTypeMap("AssetBookID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssetBookName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DepreciationMethodID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAssetType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AssetBook";
				meta.Destination = "AssetBook";
				
				meta.spInsert = "proc_AssetBookInsert";				
				meta.spUpdate = "proc_AssetBookUpdate";		
				meta.spDelete = "proc_AssetBookDelete";
				meta.spLoadAll = "proc_AssetBookLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetBookLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetBookMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
