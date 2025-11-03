/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 10/13/2015 2:00:15 PM
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
	abstract public class esAssetWorkOrderImplementerCollection : esEntityCollectionWAuditLog
	{
		public esAssetWorkOrderImplementerCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AssetWorkOrderImplementerCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetWorkOrderImplementerQuery query)
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
			this.InitQuery(query as esAssetWorkOrderImplementerQuery);
		}
		#endregion
		
		virtual public AssetWorkOrderImplementer DetachEntity(AssetWorkOrderImplementer entity)
		{
			return base.DetachEntity(entity) as AssetWorkOrderImplementer;
		}
		
		virtual public AssetWorkOrderImplementer AttachEntity(AssetWorkOrderImplementer entity)
		{
			return base.AttachEntity(entity) as AssetWorkOrderImplementer;
		}
		
		virtual public void Combine(AssetWorkOrderImplementerCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AssetWorkOrderImplementer this[int index]
		{
			get
			{
				return base[index] as AssetWorkOrderImplementer;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetWorkOrderImplementer);
		}
	}



	[Serializable]
	abstract public class esAssetWorkOrderImplementer : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetWorkOrderImplementerQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetWorkOrderImplementer()
		{

		}

		public esAssetWorkOrderImplementer(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String orderNo, System.String userID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo, userID);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo, userID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String orderNo, System.String userID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo, userID);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo, userID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String orderNo, System.String userID)
		{
			esAssetWorkOrderImplementerQuery query = this.GetDynamicQuery();
			query.Where(query.OrderNo == orderNo, query.UserID == userID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String orderNo, System.String userID)
		{
			esParameters parms = new esParameters();
			parms.Add("OrderNo",orderNo);			parms.Add("UserID",userID);
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
						case "OrderNo": this.str.OrderNo = (string)value; break;							
						case "UserID": this.str.UserID = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
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
		/// Maps to AssetWorkOrderImplementer.OrderNo
		/// </summary>
		virtual public System.String OrderNo
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderImplementerMetadata.ColumnNames.OrderNo);
			}
			
			set
			{
				base.SetSystemString(AssetWorkOrderImplementerMetadata.ColumnNames.OrderNo, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetWorkOrderImplementer.UserID
		/// </summary>
		virtual public System.String UserID
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderImplementerMetadata.ColumnNames.UserID);
			}
			
			set
			{
				base.SetSystemString(AssetWorkOrderImplementerMetadata.ColumnNames.UserID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetWorkOrderImplementer.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderImplementerMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(AssetWorkOrderImplementerMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetWorkOrderImplementer.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetWorkOrderImplementerMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AssetWorkOrderImplementerMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetWorkOrderImplementer.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderImplementerMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AssetWorkOrderImplementerMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAssetWorkOrderImplementer entity)
			{
				this.entity = entity;
			}
			
	
			public System.String OrderNo
			{
				get
				{
					System.String data = entity.OrderNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderNo = null;
					else entity.OrderNo = Convert.ToString(value);
				}
			}
				
			public System.String UserID
			{
				get
				{
					System.String data = entity.UserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UserID = null;
					else entity.UserID = Convert.ToString(value);
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
			

			private esAssetWorkOrderImplementer entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetWorkOrderImplementerQuery query)
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
				throw new Exception("esAssetWorkOrderImplementer can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AssetWorkOrderImplementer : esAssetWorkOrderImplementer
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
	abstract public class esAssetWorkOrderImplementerQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AssetWorkOrderImplementerMetadata.Meta();
			}
		}	
		

		public esQueryItem OrderNo
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderImplementerMetadata.ColumnNames.OrderNo, esSystemType.String);
			}
		} 
		
		public esQueryItem UserID
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderImplementerMetadata.ColumnNames.UserID, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderImplementerMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderImplementerMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderImplementerMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetWorkOrderImplementerCollection")]
	public partial class AssetWorkOrderImplementerCollection : esAssetWorkOrderImplementerCollection, IEnumerable<AssetWorkOrderImplementer>
	{
		public AssetWorkOrderImplementerCollection()
		{

		}
		
		public static implicit operator List<AssetWorkOrderImplementer>(AssetWorkOrderImplementerCollection coll)
		{
			List<AssetWorkOrderImplementer> list = new List<AssetWorkOrderImplementer>();
			
			foreach (AssetWorkOrderImplementer emp in coll)
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
				return  AssetWorkOrderImplementerMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetWorkOrderImplementerQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetWorkOrderImplementer(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetWorkOrderImplementer();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AssetWorkOrderImplementerQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetWorkOrderImplementerQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AssetWorkOrderImplementerQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AssetWorkOrderImplementer AddNew()
		{
			AssetWorkOrderImplementer entity = base.AddNewEntity() as AssetWorkOrderImplementer;
			
			return entity;
		}

		public AssetWorkOrderImplementer FindByPrimaryKey(System.String orderNo, System.String userID)
		{
			return base.FindByPrimaryKey(orderNo, userID) as AssetWorkOrderImplementer;
		}


		#region IEnumerable<AssetWorkOrderImplementer> Members

		IEnumerator<AssetWorkOrderImplementer> IEnumerable<AssetWorkOrderImplementer>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AssetWorkOrderImplementer;
			}
		}

		#endregion
		
		private AssetWorkOrderImplementerQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetWorkOrderImplementer' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AssetWorkOrderImplementer ({OrderNo},{UserID})")]
	[Serializable]
	public partial class AssetWorkOrderImplementer : esAssetWorkOrderImplementer
	{
		public AssetWorkOrderImplementer()
		{

		}
	
		public AssetWorkOrderImplementer(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetWorkOrderImplementerMetadata.Meta();
			}
		}
		
		
		
		override protected esAssetWorkOrderImplementerQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetWorkOrderImplementerQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AssetWorkOrderImplementerQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetWorkOrderImplementerQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AssetWorkOrderImplementerQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AssetWorkOrderImplementerQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AssetWorkOrderImplementerQuery : esAssetWorkOrderImplementerQuery
	{
		public AssetWorkOrderImplementerQuery()
		{

		}		
		
		public AssetWorkOrderImplementerQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AssetWorkOrderImplementerQuery";
        }
		
			
	}


	[Serializable]
	public partial class AssetWorkOrderImplementerMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetWorkOrderImplementerMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetWorkOrderImplementerMetadata.ColumnNames.OrderNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderImplementerMetadata.PropertyNames.OrderNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetWorkOrderImplementerMetadata.ColumnNames.UserID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderImplementerMetadata.PropertyNames.UserID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetWorkOrderImplementerMetadata.ColumnNames.Notes, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderImplementerMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetWorkOrderImplementerMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetWorkOrderImplementerMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetWorkOrderImplementerMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderImplementerMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AssetWorkOrderImplementerMetadata Meta()
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
			 public const string OrderNo = "OrderNo";
			 public const string UserID = "UserID";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string OrderNo = "OrderNo";
			 public const string UserID = "UserID";
			 public const string Notes = "Notes";
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
			lock (typeof(AssetWorkOrderImplementerMetadata))
			{
				if(AssetWorkOrderImplementerMetadata.mapDelegates == null)
				{
					AssetWorkOrderImplementerMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AssetWorkOrderImplementerMetadata.meta == null)
				{
					AssetWorkOrderImplementerMetadata.meta = new AssetWorkOrderImplementerMetadata();
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
				

				meta.AddTypeMap("OrderNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("UserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AssetWorkOrderImplementer";
				meta.Destination = "AssetWorkOrderImplementer";
				
				meta.spInsert = "proc_AssetWorkOrderImplementerInsert";				
				meta.spUpdate = "proc_AssetWorkOrderImplementerUpdate";		
				meta.spDelete = "proc_AssetWorkOrderImplementerDelete";
				meta.spLoadAll = "proc_AssetWorkOrderImplementerLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetWorkOrderImplementerLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetWorkOrderImplementerMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
