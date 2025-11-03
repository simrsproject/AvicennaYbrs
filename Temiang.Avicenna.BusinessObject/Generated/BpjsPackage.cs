/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/20/2014 9:51:15 AM
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
	abstract public class esBpjsPackageCollection : esEntityCollectionWAuditLog
	{
		public esBpjsPackageCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "BpjsPackageCollection";
		}

		#region Query Logic
		protected void InitQuery(esBpjsPackageQuery query)
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
			this.InitQuery(query as esBpjsPackageQuery);
		}
		#endregion
		
		virtual public BpjsPackage DetachEntity(BpjsPackage entity)
		{
			return base.DetachEntity(entity) as BpjsPackage;
		}
		
		virtual public BpjsPackage AttachEntity(BpjsPackage entity)
		{
			return base.AttachEntity(entity) as BpjsPackage;
		}
		
		virtual public void Combine(BpjsPackageCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BpjsPackage this[int index]
		{
			get
			{
				return base[index] as BpjsPackage;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BpjsPackage);
		}
	}



	[Serializable]
	abstract public class esBpjsPackage : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBpjsPackageQuery GetDynamicQuery()
		{
			return null;
		}

		public esBpjsPackage()
		{

		}

		public esBpjsPackage(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String packageID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(packageID);
			else
				return LoadByPrimaryKeyStoredProcedure(packageID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String packageID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(packageID);
			else
				return LoadByPrimaryKeyStoredProcedure(packageID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String packageID)
		{
			esBpjsPackageQuery query = this.GetDynamicQuery();
			query.Where(query.PackageID == packageID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String packageID)
		{
			esParameters parms = new esParameters();
			parms.Add("PackageID",packageID);
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
						case "PackageID": this.str.PackageID = (string)value; break;							
						case "PackageName": this.str.PackageName = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
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
		/// Maps to BpjsPackage.PackageID
		/// </summary>
		virtual public System.String PackageID
		{
			get
			{
				return base.GetSystemString(BpjsPackageMetadata.ColumnNames.PackageID);
			}
			
			set
			{
				base.SetSystemString(BpjsPackageMetadata.ColumnNames.PackageID, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsPackage.PackageName
		/// </summary>
		virtual public System.String PackageName
		{
			get
			{
				return base.GetSystemString(BpjsPackageMetadata.ColumnNames.PackageName);
			}
			
			set
			{
				base.SetSystemString(BpjsPackageMetadata.ColumnNames.PackageName, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsPackage.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(BpjsPackageMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(BpjsPackageMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsPackage.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BpjsPackageMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BpjsPackageMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsPackage.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BpjsPackageMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BpjsPackageMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esBpjsPackage entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PackageID
			{
				get
				{
					System.String data = entity.PackageID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PackageID = null;
					else entity.PackageID = Convert.ToString(value);
				}
			}
				
			public System.String PackageName
			{
				get
				{
					System.String data = entity.PackageName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PackageName = null;
					else entity.PackageName = Convert.ToString(value);
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
			

			private esBpjsPackage entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBpjsPackageQuery query)
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
				throw new Exception("esBpjsPackage can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class BpjsPackage : esBpjsPackage
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
	abstract public class esBpjsPackageQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return BpjsPackageMetadata.Meta();
			}
		}	
		

		public esQueryItem PackageID
		{
			get
			{
				return new esQueryItem(this, BpjsPackageMetadata.ColumnNames.PackageID, esSystemType.String);
			}
		} 
		
		public esQueryItem PackageName
		{
			get
			{
				return new esQueryItem(this, BpjsPackageMetadata.ColumnNames.PackageName, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, BpjsPackageMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BpjsPackageMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BpjsPackageMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BpjsPackageCollection")]
	public partial class BpjsPackageCollection : esBpjsPackageCollection, IEnumerable<BpjsPackage>
	{
		public BpjsPackageCollection()
		{

		}
		
		public static implicit operator List<BpjsPackage>(BpjsPackageCollection coll)
		{
			List<BpjsPackage> list = new List<BpjsPackage>();
			
			foreach (BpjsPackage emp in coll)
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
				return  BpjsPackageMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BpjsPackageQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BpjsPackage(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BpjsPackage();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public BpjsPackageQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BpjsPackageQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(BpjsPackageQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public BpjsPackage AddNew()
		{
			BpjsPackage entity = base.AddNewEntity() as BpjsPackage;
			
			return entity;
		}

		public BpjsPackage FindByPrimaryKey(System.String packageID)
		{
			return base.FindByPrimaryKey(packageID) as BpjsPackage;
		}


		#region IEnumerable<BpjsPackage> Members

		IEnumerator<BpjsPackage> IEnumerable<BpjsPackage>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BpjsPackage;
			}
		}

		#endregion
		
		private BpjsPackageQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BpjsPackage' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("BpjsPackage ({PackageID})")]
	[Serializable]
	public partial class BpjsPackage : esBpjsPackage
	{
		public BpjsPackage()
		{

		}
	
		public BpjsPackage(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BpjsPackageMetadata.Meta();
			}
		}
		
		
		
		override protected esBpjsPackageQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BpjsPackageQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public BpjsPackageQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BpjsPackageQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(BpjsPackageQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private BpjsPackageQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class BpjsPackageQuery : esBpjsPackageQuery
	{
		public BpjsPackageQuery()
		{

		}		
		
		public BpjsPackageQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "BpjsPackageQuery";
        }
		
			
	}


	[Serializable]
	public partial class BpjsPackageMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BpjsPackageMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BpjsPackageMetadata.ColumnNames.PackageID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsPackageMetadata.PropertyNames.PackageID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsPackageMetadata.ColumnNames.PackageName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsPackageMetadata.PropertyNames.PackageName;
			c.CharacterMaxLength = 250;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsPackageMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BpjsPackageMetadata.PropertyNames.IsActive;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsPackageMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BpjsPackageMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsPackageMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsPackageMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public BpjsPackageMetadata Meta()
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
			 public const string PackageID = "PackageID";
			 public const string PackageName = "PackageName";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PackageID = "PackageID";
			 public const string PackageName = "PackageName";
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
			lock (typeof(BpjsPackageMetadata))
			{
				if(BpjsPackageMetadata.mapDelegates == null)
				{
					BpjsPackageMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BpjsPackageMetadata.meta == null)
				{
					BpjsPackageMetadata.meta = new BpjsPackageMetadata();
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
				

				meta.AddTypeMap("PackageID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PackageName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "BpjsPackage";
				meta.Destination = "BpjsPackage";
				
				meta.spInsert = "proc_BpjsPackageInsert";				
				meta.spUpdate = "proc_BpjsPackageUpdate";		
				meta.spDelete = "proc_BpjsPackageDelete";
				meta.spLoadAll = "proc_BpjsPackageLoadAll";
				meta.spLoadByPrimaryKey = "proc_BpjsPackageLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BpjsPackageMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
