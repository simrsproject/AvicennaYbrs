/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/24/2015 11:04:55 AM
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
	abstract public class esMenuCollection : esEntityCollectionWAuditLog
	{
		public esMenuCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "MenuCollection";
		}

		#region Query Logic
		protected void InitQuery(esMenuQuery query)
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
			this.InitQuery(query as esMenuQuery);
		}
		#endregion
		
		virtual public Menu DetachEntity(Menu entity)
		{
			return base.DetachEntity(entity) as Menu;
		}
		
		virtual public Menu AttachEntity(Menu entity)
		{
			return base.AttachEntity(entity) as Menu;
		}
		
		virtual public void Combine(MenuCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Menu this[int index]
		{
			get
			{
				return base[index] as Menu;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Menu);
		}
	}



	[Serializable]
	abstract public class esMenu : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMenuQuery GetDynamicQuery()
		{
			return null;
		}

		public esMenu()
		{

		}

		public esMenu(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String menuID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(menuID);
			else
				return LoadByPrimaryKeyStoredProcedure(menuID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String menuID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(menuID);
			else
				return LoadByPrimaryKeyStoredProcedure(menuID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String menuID)
		{
			esMenuQuery query = this.GetDynamicQuery();
			query.Where(query.MenuID == menuID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String menuID)
		{
			esParameters parms = new esParameters();
			parms.Add("MenuID",menuID);
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
						case "MenuID": this.str.MenuID = (string)value; break;							
						case "MenuName": this.str.MenuName = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "IsExtra": this.str.IsExtra = (string)value; break;							
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
						
						case "IsExtra":
						
							if (value == null || value is System.Boolean)
								this.IsExtra = (System.Boolean?)value;
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
		/// Maps to Menu.MenuID
		/// </summary>
		virtual public System.String MenuID
		{
			get
			{
				return base.GetSystemString(MenuMetadata.ColumnNames.MenuID);
			}
			
			set
			{
				base.SetSystemString(MenuMetadata.ColumnNames.MenuID, value);
			}
		}
		
		/// <summary>
		/// Maps to Menu.MenuName
		/// </summary>
		virtual public System.String MenuName
		{
			get
			{
				return base.GetSystemString(MenuMetadata.ColumnNames.MenuName);
			}
			
			set
			{
				base.SetSystemString(MenuMetadata.ColumnNames.MenuName, value);
			}
		}
		
		/// <summary>
		/// Maps to Menu.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(MenuMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(MenuMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to Menu.IsExtra
		/// </summary>
		virtual public System.Boolean? IsExtra
		{
			get
			{
				return base.GetSystemBoolean(MenuMetadata.ColumnNames.IsExtra);
			}
			
			set
			{
				base.SetSystemBoolean(MenuMetadata.ColumnNames.IsExtra, value);
			}
		}
		
		/// <summary>
		/// Maps to Menu.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MenuMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MenuMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Menu.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MenuMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(MenuMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMenu entity)
			{
				this.entity = entity;
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
				
			public System.String MenuName
			{
				get
				{
					System.String data = entity.MenuName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MenuName = null;
					else entity.MenuName = Convert.ToString(value);
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
				
			public System.String IsExtra
			{
				get
				{
					System.Boolean? data = entity.IsExtra;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsExtra = null;
					else entity.IsExtra = Convert.ToBoolean(value);
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
			

			private esMenu entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMenuQuery query)
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
				throw new Exception("esMenu can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class Menu : esMenu
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
	abstract public class esMenuQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return MenuMetadata.Meta();
			}
		}	
		

		public esQueryItem MenuID
		{
			get
			{
				return new esQueryItem(this, MenuMetadata.ColumnNames.MenuID, esSystemType.String);
			}
		} 
		
		public esQueryItem MenuName
		{
			get
			{
				return new esQueryItem(this, MenuMetadata.ColumnNames.MenuName, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, MenuMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsExtra
		{
			get
			{
				return new esQueryItem(this, MenuMetadata.ColumnNames.IsExtra, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MenuMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MenuMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MenuCollection")]
	public partial class MenuCollection : esMenuCollection, IEnumerable<Menu>
	{
		public MenuCollection()
		{

		}
		
		public static implicit operator List<Menu>(MenuCollection coll)
		{
			List<Menu> list = new List<Menu>();
			
			foreach (Menu emp in coll)
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
				return  MenuMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MenuQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Menu(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Menu();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public MenuQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MenuQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(MenuQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Menu AddNew()
		{
			Menu entity = base.AddNewEntity() as Menu;
			
			return entity;
		}

		public Menu FindByPrimaryKey(System.String menuID)
		{
			return base.FindByPrimaryKey(menuID) as Menu;
		}


		#region IEnumerable<Menu> Members

		IEnumerator<Menu> IEnumerable<Menu>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Menu;
			}
		}

		#endregion
		
		private MenuQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Menu' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Menu ({MenuID})")]
	[Serializable]
	public partial class Menu : esMenu
	{
		public Menu()
		{

		}
	
		public Menu(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MenuMetadata.Meta();
			}
		}
		
		
		
		override protected esMenuQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MenuQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public MenuQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MenuQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(MenuQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private MenuQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class MenuQuery : esMenuQuery
	{
		public MenuQuery()
		{

		}		
		
		public MenuQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "MenuQuery";
        }
		
			
	}


	[Serializable]
	public partial class MenuMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MenuMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MenuMetadata.ColumnNames.MenuID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MenuMetadata.PropertyNames.MenuID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MenuMetadata.ColumnNames.MenuName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = MenuMetadata.PropertyNames.MenuName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);
				
			c = new esColumnMetadata(MenuMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MenuMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(MenuMetadata.ColumnNames.IsExtra, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MenuMetadata.PropertyNames.IsExtra;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MenuMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MenuMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MenuMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = MenuMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public MenuMetadata Meta()
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
			 public const string MenuID = "MenuID";
			 public const string MenuName = "MenuName";
			 public const string IsActive = "IsActive";
			 public const string IsExtra = "IsExtra";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string MenuID = "MenuID";
			 public const string MenuName = "MenuName";
			 public const string IsActive = "IsActive";
			 public const string IsExtra = "IsExtra";
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
			lock (typeof(MenuMetadata))
			{
				if(MenuMetadata.mapDelegates == null)
				{
					MenuMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MenuMetadata.meta == null)
				{
					MenuMetadata.meta = new MenuMetadata();
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
				

				meta.AddTypeMap("MenuID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MenuName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsExtra", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Menu";
				meta.Destination = "Menu";
				
				meta.spInsert = "proc_MenuInsert";				
				meta.spUpdate = "proc_MenuUpdate";		
				meta.spDelete = "proc_MenuDelete";
				meta.spLoadAll = "proc_MenuLoadAll";
				meta.spLoadByPrimaryKey = "proc_MenuLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MenuMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
