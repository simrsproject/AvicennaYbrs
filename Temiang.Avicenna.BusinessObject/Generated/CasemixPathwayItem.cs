/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/22/2021 1:52:17 PM
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
	abstract public class esCasemixPathwayItemCollection : esEntityCollectionWAuditLog
	{
		public esCasemixPathwayItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "CasemixPathwayItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esCasemixPathwayItemQuery query)
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
			this.InitQuery(query as esCasemixPathwayItemQuery);
		}
		#endregion
		
		virtual public CasemixPathwayItem DetachEntity(CasemixPathwayItem entity)
		{
			return base.DetachEntity(entity) as CasemixPathwayItem;
		}
		
		virtual public CasemixPathwayItem AttachEntity(CasemixPathwayItem entity)
		{
			return base.AttachEntity(entity) as CasemixPathwayItem;
		}
		
		virtual public void Combine(CasemixPathwayItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CasemixPathwayItem this[int index]
		{
			get
			{
				return base[index] as CasemixPathwayItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CasemixPathwayItem);
		}
	}



	[Serializable]
	abstract public class esCasemixPathwayItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCasemixPathwayItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esCasemixPathwayItem()
		{

		}

		public esCasemixPathwayItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String pathwayID, System.String itemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(pathwayID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(pathwayID, itemID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String pathwayID, System.String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(pathwayID, itemID);
			else
                return LoadByPrimaryKeyStoredProcedure(pathwayID, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String pathwayID, System.String itemID)
		{
			esCasemixPathwayItemQuery query = this.GetDynamicQuery();
			query.Where(query.PathwayID == pathwayID, query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String pathwayID, System.String itemID)
		{
			esParameters parms = new esParameters();
            parms.Add("PathwayID", pathwayID);
			parms.Add("ItemID", itemID);
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
						case "PathwayID": this.str.PathwayID = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
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
		/// Maps to CasemixPathwayItem.PathwayID
		/// </summary>
		virtual public System.String PathwayID
		{
			get
			{
				return base.GetSystemString(CasemixPathwayItemMetadata.ColumnNames.PathwayID);
			}
			
			set
			{
				base.SetSystemString(CasemixPathwayItemMetadata.ColumnNames.PathwayID, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixPathwayItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(CasemixPathwayItemMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(CasemixPathwayItemMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixPathwayItem.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(CasemixPathwayItemMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(CasemixPathwayItemMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixPathwayItem.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(CasemixPathwayItemMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(CasemixPathwayItemMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixPathwayItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CasemixPathwayItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CasemixPathwayItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixPathwayItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CasemixPathwayItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(CasemixPathwayItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCasemixPathwayItem entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PathwayID
			{
				get
				{
					System.String data = entity.PathwayID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PathwayID = null;
					else entity.PathwayID = Convert.ToString(value);
				}
			}
				
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
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
			

			private esCasemixPathwayItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCasemixPathwayItemQuery query)
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
				throw new Exception("esCasemixPathwayItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esCasemixPathwayItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CasemixPathwayItemMetadata.Meta();
			}
		}	
		

		public esQueryItem PathwayID
		{
			get
			{
				return new esQueryItem(this, CasemixPathwayItemMetadata.ColumnNames.PathwayID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, CasemixPathwayItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, CasemixPathwayItemMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, CasemixPathwayItemMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CasemixPathwayItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CasemixPathwayItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CasemixPathwayItemCollection")]
	public partial class CasemixPathwayItemCollection : esCasemixPathwayItemCollection, IEnumerable<CasemixPathwayItem>
	{
		public CasemixPathwayItemCollection()
		{

		}
		
		public static implicit operator List<CasemixPathwayItem>(CasemixPathwayItemCollection coll)
		{
			List<CasemixPathwayItem> list = new List<CasemixPathwayItem>();
			
			foreach (CasemixPathwayItem emp in coll)
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
				return  CasemixPathwayItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CasemixPathwayItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CasemixPathwayItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CasemixPathwayItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public CasemixPathwayItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CasemixPathwayItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(CasemixPathwayItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public CasemixPathwayItem AddNew()
		{
			CasemixPathwayItem entity = base.AddNewEntity() as CasemixPathwayItem;
			
			return entity;
		}

		public CasemixPathwayItem FindByPrimaryKey(System.String itemID, System.String pathwayID)
		{
			return base.FindByPrimaryKey(itemID, pathwayID) as CasemixPathwayItem;
		}


		#region IEnumerable<CasemixPathwayItem> Members

		IEnumerator<CasemixPathwayItem> IEnumerable<CasemixPathwayItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CasemixPathwayItem;
			}
		}

		#endregion
		
		private CasemixPathwayItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CasemixPathwayItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("CasemixPathwayItem ({PathwayID},{ItemID})")]
	[Serializable]
	public partial class CasemixPathwayItem : esCasemixPathwayItem
	{
		public CasemixPathwayItem()
		{

		}
	
		public CasemixPathwayItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CasemixPathwayItemMetadata.Meta();
			}
		}
		
		
		
		override protected esCasemixPathwayItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CasemixPathwayItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public CasemixPathwayItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CasemixPathwayItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(CasemixPathwayItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private CasemixPathwayItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class CasemixPathwayItemQuery : esCasemixPathwayItemQuery
	{
		public CasemixPathwayItemQuery()
		{

		}		
		
		public CasemixPathwayItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "CasemixPathwayItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class CasemixPathwayItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CasemixPathwayItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CasemixPathwayItemMetadata.ColumnNames.PathwayID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CasemixPathwayItemMetadata.PropertyNames.PathwayID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixPathwayItemMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CasemixPathwayItemMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixPathwayItemMetadata.ColumnNames.Notes, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CasemixPathwayItemMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixPathwayItemMetadata.ColumnNames.IsActive, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CasemixPathwayItemMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixPathwayItemMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CasemixPathwayItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixPathwayItemMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CasemixPathwayItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public CasemixPathwayItemMetadata Meta()
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
			 public const string PathwayID = "PathwayID";
			 public const string ItemID = "ItemID";
			 public const string Notes = "Notes";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PathwayID = "PathwayID";
			 public const string ItemID = "ItemID";
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
			lock (typeof(CasemixPathwayItemMetadata))
			{
				if(CasemixPathwayItemMetadata.mapDelegates == null)
				{
					CasemixPathwayItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CasemixPathwayItemMetadata.meta == null)
				{
					CasemixPathwayItemMetadata.meta = new CasemixPathwayItemMetadata();
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
				

				meta.AddTypeMap("PathwayID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "CasemixPathwayItem";
				meta.Destination = "CasemixPathwayItem";
				
				meta.spInsert = "proc_CasemixPathwayItemInsert";				
				meta.spUpdate = "proc_CasemixPathwayItemUpdate";		
				meta.spDelete = "proc_CasemixPathwayItemDelete";
				meta.spLoadAll = "proc_CasemixPathwayItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_CasemixPathwayItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CasemixPathwayItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
