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
	abstract public class esAssetDepreciationMethodCollection : esEntityCollectionWAuditLog
	{
		public esAssetDepreciationMethodCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AssetDepreciationMethodCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetDepreciationMethodQuery query)
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
			this.InitQuery(query as esAssetDepreciationMethodQuery);
		}
		#endregion
		
		virtual public AssetDepreciationMethod DetachEntity(AssetDepreciationMethod entity)
		{
			return base.DetachEntity(entity) as AssetDepreciationMethod;
		}
		
		virtual public AssetDepreciationMethod AttachEntity(AssetDepreciationMethod entity)
		{
			return base.AttachEntity(entity) as AssetDepreciationMethod;
		}
		
		virtual public void Combine(AssetDepreciationMethodCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AssetDepreciationMethod this[int index]
		{
			get
			{
				return base[index] as AssetDepreciationMethod;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetDepreciationMethod);
		}
	}



	[Serializable]
	abstract public class esAssetDepreciationMethod : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetDepreciationMethodQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetDepreciationMethod()
		{

		}

		public esAssetDepreciationMethod(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String depreciationMethodID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(depreciationMethodID);
			else
				return LoadByPrimaryKeyStoredProcedure(depreciationMethodID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String depreciationMethodID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(depreciationMethodID);
			else
				return LoadByPrimaryKeyStoredProcedure(depreciationMethodID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String depreciationMethodID)
		{
			esAssetDepreciationMethodQuery query = this.GetDynamicQuery();
			query.Where(query.DepreciationMethodID == depreciationMethodID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String depreciationMethodID)
		{
			esParameters parms = new esParameters();
			parms.Add("DepreciationMethodID",depreciationMethodID);
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
						case "DepreciationMethodID": this.str.DepreciationMethodID = (string)value; break;							
						case "DepreciationMethodName": this.str.DepreciationMethodName = (string)value; break;							
						case "Factor": this.str.Factor = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Factor":
						
							if (value == null || value is System.Decimal)
								this.Factor = (System.Decimal?)value;
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
		/// Maps to AssetDepreciationMethod.DepreciationMethodID
		/// </summary>
		virtual public System.String DepreciationMethodID
		{
			get
			{
				return base.GetSystemString(AssetDepreciationMethodMetadata.ColumnNames.DepreciationMethodID);
			}
			
			set
			{
				base.SetSystemString(AssetDepreciationMethodMetadata.ColumnNames.DepreciationMethodID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationMethod.DepreciationMethodName
		/// </summary>
		virtual public System.String DepreciationMethodName
		{
			get
			{
				return base.GetSystemString(AssetDepreciationMethodMetadata.ColumnNames.DepreciationMethodName);
			}
			
			set
			{
				base.SetSystemString(AssetDepreciationMethodMetadata.ColumnNames.DepreciationMethodName, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationMethod.Factor
		/// </summary>
		virtual public System.Decimal? Factor
		{
			get
			{
				return base.GetSystemDecimal(AssetDepreciationMethodMetadata.ColumnNames.Factor);
			}
			
			set
			{
				base.SetSystemDecimal(AssetDepreciationMethodMetadata.ColumnNames.Factor, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationMethod.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(AssetDepreciationMethodMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(AssetDepreciationMethodMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationMethod.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetDepreciationMethodMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AssetDepreciationMethodMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationMethod.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AssetDepreciationMethodMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AssetDepreciationMethodMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAssetDepreciationMethod entity)
			{
				this.entity = entity;
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
				
			public System.String DepreciationMethodName
			{
				get
				{
					System.String data = entity.DepreciationMethodName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepreciationMethodName = null;
					else entity.DepreciationMethodName = Convert.ToString(value);
				}
			}
				
			public System.String Factor
			{
				get
				{
					System.Decimal? data = entity.Factor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Factor = null;
					else entity.Factor = Convert.ToDecimal(value);
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
			

			private esAssetDepreciationMethod entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetDepreciationMethodQuery query)
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
				throw new Exception("esAssetDepreciationMethod can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AssetDepreciationMethod : esAssetDepreciationMethod
	{

				
		#region AssetBookCollectionByDepreciationMethodID - Zero To Many
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - RefAssetBookToAssetDepreciationMethod
		/// </summary>

		[XmlIgnore]
		public AssetBookCollection AssetBookCollectionByDepreciationMethodID
		{
			get
			{
				if(this._AssetBookCollectionByDepreciationMethodID == null)
				{
					this._AssetBookCollectionByDepreciationMethodID = new AssetBookCollection();
					this._AssetBookCollectionByDepreciationMethodID.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("AssetBookCollectionByDepreciationMethodID", this._AssetBookCollectionByDepreciationMethodID);
				
					if(this.DepreciationMethodID != null)
					{
						this._AssetBookCollectionByDepreciationMethodID.Query.Where(this._AssetBookCollectionByDepreciationMethodID.Query.DepreciationMethodID == this.DepreciationMethodID);
						this._AssetBookCollectionByDepreciationMethodID.Query.Load();

						// Auto-hookup Foreign Keys
						this._AssetBookCollectionByDepreciationMethodID.fks.Add(AssetBookMetadata.ColumnNames.DepreciationMethodID, this.DepreciationMethodID);
					}
				}

				return this._AssetBookCollectionByDepreciationMethodID;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._AssetBookCollectionByDepreciationMethodID != null) 
				{ 
					this.RemovePostSave("AssetBookCollectionByDepreciationMethodID"); 
					this._AssetBookCollectionByDepreciationMethodID = null;
					
				} 
			} 			
		}

		private AssetBookCollection _AssetBookCollectionByDepreciationMethodID;
		#endregion

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
			props.Add(new esPropertyDescriptor(this, "AssetBookCollectionByDepreciationMethodID", typeof(AssetBookCollection), new AssetBook()));
		
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
	abstract public class esAssetDepreciationMethodQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AssetDepreciationMethodMetadata.Meta();
			}
		}	
		

		public esQueryItem DepreciationMethodID
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationMethodMetadata.ColumnNames.DepreciationMethodID, esSystemType.String);
			}
		} 
		
		public esQueryItem DepreciationMethodName
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationMethodMetadata.ColumnNames.DepreciationMethodName, esSystemType.String);
			}
		} 
		
		public esQueryItem Factor
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationMethodMetadata.ColumnNames.Factor, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationMethodMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationMethodMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationMethodMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetDepreciationMethodCollection")]
	public partial class AssetDepreciationMethodCollection : esAssetDepreciationMethodCollection, IEnumerable<AssetDepreciationMethod>
	{
		public AssetDepreciationMethodCollection()
		{

		}
		
		public static implicit operator List<AssetDepreciationMethod>(AssetDepreciationMethodCollection coll)
		{
			List<AssetDepreciationMethod> list = new List<AssetDepreciationMethod>();
			
			foreach (AssetDepreciationMethod emp in coll)
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
				return  AssetDepreciationMethodMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetDepreciationMethodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetDepreciationMethod(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetDepreciationMethod();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AssetDepreciationMethodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetDepreciationMethodQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AssetDepreciationMethodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AssetDepreciationMethod AddNew()
		{
			AssetDepreciationMethod entity = base.AddNewEntity() as AssetDepreciationMethod;
			
			return entity;
		}

		public AssetDepreciationMethod FindByPrimaryKey(System.String depreciationMethodID)
		{
			return base.FindByPrimaryKey(depreciationMethodID) as AssetDepreciationMethod;
		}


		#region IEnumerable<AssetDepreciationMethod> Members

		IEnumerator<AssetDepreciationMethod> IEnumerable<AssetDepreciationMethod>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AssetDepreciationMethod;
			}
		}

		#endregion
		
		private AssetDepreciationMethodQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetDepreciationMethod' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AssetDepreciationMethod ({DepreciationMethodID})")]
	[Serializable]
	public partial class AssetDepreciationMethod : esAssetDepreciationMethod
	{
		public AssetDepreciationMethod()
		{

		}
	
		public AssetDepreciationMethod(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetDepreciationMethodMetadata.Meta();
			}
		}
		
		
		
		override protected esAssetDepreciationMethodQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetDepreciationMethodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AssetDepreciationMethodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetDepreciationMethodQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AssetDepreciationMethodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AssetDepreciationMethodQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AssetDepreciationMethodQuery : esAssetDepreciationMethodQuery
	{
		public AssetDepreciationMethodQuery()
		{

		}		
		
		public AssetDepreciationMethodQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AssetDepreciationMethodQuery";
        }
		
			
	}


	[Serializable]
	public partial class AssetDepreciationMethodMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetDepreciationMethodMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetDepreciationMethodMetadata.ColumnNames.DepreciationMethodID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetDepreciationMethodMetadata.PropertyNames.DepreciationMethodID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationMethodMetadata.ColumnNames.DepreciationMethodName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetDepreciationMethodMetadata.PropertyNames.DepreciationMethodName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationMethodMetadata.ColumnNames.Factor, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetDepreciationMethodMetadata.PropertyNames.Factor;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationMethodMetadata.ColumnNames.IsActive, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetDepreciationMethodMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationMethodMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetDepreciationMethodMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationMethodMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetDepreciationMethodMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AssetDepreciationMethodMetadata Meta()
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
			 public const string DepreciationMethodID = "DepreciationMethodID";
			 public const string DepreciationMethodName = "DepreciationMethodName";
			 public const string Factor = "Factor";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string DepreciationMethodID = "DepreciationMethodID";
			 public const string DepreciationMethodName = "DepreciationMethodName";
			 public const string Factor = "Factor";
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
			lock (typeof(AssetDepreciationMethodMetadata))
			{
				if(AssetDepreciationMethodMetadata.mapDelegates == null)
				{
					AssetDepreciationMethodMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AssetDepreciationMethodMetadata.meta == null)
				{
					AssetDepreciationMethodMetadata.meta = new AssetDepreciationMethodMetadata();
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
				

				meta.AddTypeMap("DepreciationMethodID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DepreciationMethodName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Factor", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AssetDepreciationMethod";
				meta.Destination = "AssetDepreciationMethod";
				
				meta.spInsert = "proc_AssetDepreciationMethodInsert";				
				meta.spUpdate = "proc_AssetDepreciationMethodUpdate";		
				meta.spDelete = "proc_AssetDepreciationMethodDelete";
				meta.spLoadAll = "proc_AssetDepreciationMethodLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetDepreciationMethodLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetDepreciationMethodMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
