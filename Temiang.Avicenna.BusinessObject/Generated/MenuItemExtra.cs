/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/7/2015 3:08:37 PM
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
	abstract public class esMenuItemExtraCollection : esEntityCollectionWAuditLog
	{
		public esMenuItemExtraCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "MenuItemExtraCollection";
		}

		#region Query Logic
		protected void InitQuery(esMenuItemExtraQuery query)
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
			this.InitQuery(query as esMenuItemExtraQuery);
		}
		#endregion
		
		virtual public MenuItemExtra DetachEntity(MenuItemExtra entity)
		{
			return base.DetachEntity(entity) as MenuItemExtra;
		}
		
		virtual public MenuItemExtra AttachEntity(MenuItemExtra entity)
		{
			return base.AttachEntity(entity) as MenuItemExtra;
		}
		
		virtual public void Combine(MenuItemExtraCollection collection)
		{
			base.Combine(collection);
		}
		
		new public MenuItemExtra this[int index]
		{
			get
			{
				return base[index] as MenuItemExtra;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MenuItemExtra);
		}
	}



	[Serializable]
	abstract public class esMenuItemExtra : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMenuItemExtraQuery GetDynamicQuery()
		{
			return null;
		}

		public esMenuItemExtra()
		{

		}

		public esMenuItemExtra(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String seqNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(seqNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String seqNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(seqNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String seqNo)
		{
			esMenuItemExtraQuery query = this.GetDynamicQuery();
			query.Where(query.SeqNo == seqNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String seqNo)
		{
			esParameters parms = new esParameters();
			parms.Add("SeqNo",seqNo);
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
						case "SeqNo": this.str.SeqNo = (string)value; break;							
						case "MenuID": this.str.MenuID = (string)value; break;							
						case "StartingDate": this.str.StartingDate = (string)value; break;							
						case "SRMealSet": this.str.SRMealSet = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "StartingDate":
						
							if (value == null || value is System.DateTime)
								this.StartingDate = (System.DateTime?)value;
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
		/// Maps to MenuItemExtra.SeqNo
		/// </summary>
		virtual public System.String SeqNo
		{
			get
			{
				return base.GetSystemString(MenuItemExtraMetadata.ColumnNames.SeqNo);
			}
			
			set
			{
				base.SetSystemString(MenuItemExtraMetadata.ColumnNames.SeqNo, value);
			}
		}
		
		/// <summary>
		/// Maps to MenuItemExtra.MenuID
		/// </summary>
		virtual public System.String MenuID
		{
			get
			{
				return base.GetSystemString(MenuItemExtraMetadata.ColumnNames.MenuID);
			}
			
			set
			{
				base.SetSystemString(MenuItemExtraMetadata.ColumnNames.MenuID, value);
			}
		}
		
		/// <summary>
		/// Maps to MenuItemExtra.StartingDate
		/// </summary>
		virtual public System.DateTime? StartingDate
		{
			get
			{
				return base.GetSystemDateTime(MenuItemExtraMetadata.ColumnNames.StartingDate);
			}
			
			set
			{
				base.SetSystemDateTime(MenuItemExtraMetadata.ColumnNames.StartingDate, value);
			}
		}
		
		/// <summary>
		/// Maps to MenuItemExtra.SRMealSet
		/// </summary>
		virtual public System.String SRMealSet
		{
			get
			{
				return base.GetSystemString(MenuItemExtraMetadata.ColumnNames.SRMealSet);
			}
			
			set
			{
				base.SetSystemString(MenuItemExtraMetadata.ColumnNames.SRMealSet, value);
			}
		}
		
		/// <summary>
		/// Maps to MenuItemExtra.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MenuItemExtraMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MenuItemExtraMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to MenuItemExtra.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MenuItemExtraMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(MenuItemExtraMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMenuItemExtra entity)
			{
				this.entity = entity;
			}
			
	
			public System.String SeqNo
			{
				get
				{
					System.String data = entity.SeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeqNo = null;
					else entity.SeqNo = Convert.ToString(value);
				}
			}
				
			public System.String MenuID
			{
				get
				{
					System.String data = entity.MenuID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MenuID = null;
					else entity.MenuID = Convert.ToString(value);
				}
			}
				
			public System.String StartingDate
			{
				get
				{
					System.DateTime? data = entity.StartingDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartingDate = null;
					else entity.StartingDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String SRMealSet
			{
				get
				{
					System.String data = entity.SRMealSet;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMealSet = null;
					else entity.SRMealSet = Convert.ToString(value);
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
			

			private esMenuItemExtra entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMenuItemExtraQuery query)
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
				throw new Exception("esMenuItemExtra can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class MenuItemExtra : esMenuItemExtra
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
	abstract public class esMenuItemExtraQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return MenuItemExtraMetadata.Meta();
			}
		}	
		

		public esQueryItem SeqNo
		{
			get
			{
				return new esQueryItem(this, MenuItemExtraMetadata.ColumnNames.SeqNo, esSystemType.String);
			}
		} 
		
		public esQueryItem MenuID
		{
			get
			{
				return new esQueryItem(this, MenuItemExtraMetadata.ColumnNames.MenuID, esSystemType.String);
			}
		} 
		
		public esQueryItem StartingDate
		{
			get
			{
				return new esQueryItem(this, MenuItemExtraMetadata.ColumnNames.StartingDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem SRMealSet
		{
			get
			{
				return new esQueryItem(this, MenuItemExtraMetadata.ColumnNames.SRMealSet, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MenuItemExtraMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MenuItemExtraMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MenuItemExtraCollection")]
	public partial class MenuItemExtraCollection : esMenuItemExtraCollection, IEnumerable<MenuItemExtra>
	{
		public MenuItemExtraCollection()
		{

		}
		
		public static implicit operator List<MenuItemExtra>(MenuItemExtraCollection coll)
		{
			List<MenuItemExtra> list = new List<MenuItemExtra>();
			
			foreach (MenuItemExtra emp in coll)
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
				return  MenuItemExtraMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MenuItemExtraQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MenuItemExtra(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MenuItemExtra();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public MenuItemExtraQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MenuItemExtraQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(MenuItemExtraQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public MenuItemExtra AddNew()
		{
			MenuItemExtra entity = base.AddNewEntity() as MenuItemExtra;
			
			return entity;
		}

		public MenuItemExtra FindByPrimaryKey(System.String seqNo)
		{
			return base.FindByPrimaryKey(seqNo) as MenuItemExtra;
		}


		#region IEnumerable<MenuItemExtra> Members

		IEnumerator<MenuItemExtra> IEnumerable<MenuItemExtra>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as MenuItemExtra;
			}
		}

		#endregion
		
		private MenuItemExtraQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MenuItemExtra' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("MenuItemExtra ({SeqNo})")]
	[Serializable]
	public partial class MenuItemExtra : esMenuItemExtra
	{
		public MenuItemExtra()
		{

		}
	
		public MenuItemExtra(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MenuItemExtraMetadata.Meta();
			}
		}
		
		
		
		override protected esMenuItemExtraQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MenuItemExtraQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public MenuItemExtraQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MenuItemExtraQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(MenuItemExtraQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private MenuItemExtraQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class MenuItemExtraQuery : esMenuItemExtraQuery
	{
		public MenuItemExtraQuery()
		{

		}		
		
		public MenuItemExtraQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "MenuItemExtraQuery";
        }
		
			
	}


	[Serializable]
	public partial class MenuItemExtraMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MenuItemExtraMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MenuItemExtraMetadata.ColumnNames.SeqNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MenuItemExtraMetadata.PropertyNames.SeqNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(MenuItemExtraMetadata.ColumnNames.MenuID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = MenuItemExtraMetadata.PropertyNames.MenuID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MenuItemExtraMetadata.ColumnNames.StartingDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MenuItemExtraMetadata.PropertyNames.StartingDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(MenuItemExtraMetadata.ColumnNames.SRMealSet, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = MenuItemExtraMetadata.PropertyNames.SRMealSet;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(MenuItemExtraMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MenuItemExtraMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MenuItemExtraMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = MenuItemExtraMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public MenuItemExtraMetadata Meta()
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
			 public const string SeqNo = "SeqNo";
			 public const string MenuID = "MenuID";
			 public const string StartingDate = "StartingDate";
			 public const string SRMealSet = "SRMealSet";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SeqNo = "SeqNo";
			 public const string MenuID = "MenuID";
			 public const string StartingDate = "StartingDate";
			 public const string SRMealSet = "SRMealSet";
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
			lock (typeof(MenuItemExtraMetadata))
			{
				if(MenuItemExtraMetadata.mapDelegates == null)
				{
					MenuItemExtraMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MenuItemExtraMetadata.meta == null)
				{
					MenuItemExtraMetadata.meta = new MenuItemExtraMetadata();
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
				

				meta.AddTypeMap("SeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MenuID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartingDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRMealSet", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "MenuItemExtra";
				meta.Destination = "MenuItemExtra";
				
				meta.spInsert = "proc_MenuItemExtraInsert";				
				meta.spUpdate = "proc_MenuItemExtraUpdate";		
				meta.spDelete = "proc_MenuItemExtraDelete";
				meta.spLoadAll = "proc_MenuItemExtraLoadAll";
				meta.spLoadByPrimaryKey = "proc_MenuItemExtraLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MenuItemExtraMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
