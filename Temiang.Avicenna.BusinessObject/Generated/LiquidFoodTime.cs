/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/27/2015 10:11:18 AM
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
	abstract public class esLiquidFoodTimeCollection : esEntityCollectionWAuditLog
	{
		public esLiquidFoodTimeCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "LiquidFoodTimeCollection";
		}

		#region Query Logic
		protected void InitQuery(esLiquidFoodTimeQuery query)
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
			this.InitQuery(query as esLiquidFoodTimeQuery);
		}
		#endregion
		
		virtual public LiquidFoodTime DetachEntity(LiquidFoodTime entity)
		{
			return base.DetachEntity(entity) as LiquidFoodTime;
		}
		
		virtual public LiquidFoodTime AttachEntity(LiquidFoodTime entity)
		{
			return base.AttachEntity(entity) as LiquidFoodTime;
		}
		
		virtual public void Combine(LiquidFoodTimeCollection collection)
		{
			base.Combine(collection);
		}
		
		new public LiquidFoodTime this[int index]
		{
			get
			{
				return base[index] as LiquidFoodTime;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LiquidFoodTime);
		}
	}



	[Serializable]
	abstract public class esLiquidFoodTime : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLiquidFoodTimeQuery GetDynamicQuery()
		{
			return null;
		}

		public esLiquidFoodTime()
		{

		}

		public esLiquidFoodTime(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String itemID, System.String standardReferenceID, System.String time)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, standardReferenceID, time);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, standardReferenceID, time);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String itemID, System.String standardReferenceID, System.String time)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, standardReferenceID, time);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, standardReferenceID, time);
		}

		private bool LoadByPrimaryKeyDynamic(System.String itemID, System.String standardReferenceID, System.String time)
		{
			esLiquidFoodTimeQuery query = this.GetDynamicQuery();
			query.Where(query.ItemID == itemID, query.StandardReferenceID == standardReferenceID, query.Time == time);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String itemID, System.String standardReferenceID, System.String time)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemID",itemID);			parms.Add("StandardReferenceID",standardReferenceID);			parms.Add("Time",time);
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
						case "StandardReferenceID": this.str.StandardReferenceID = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "Time": this.str.Time = (string)value; break;							
						case "FoodID": this.str.FoodID = (string)value; break;							
						case "ChildrenFoodID": this.str.ChildrenFoodID = (string)value; break;							
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
		/// Maps to LiquidFoodTime.StandardReferenceID
		/// </summary>
		virtual public System.String StandardReferenceID
		{
			get
			{
				return base.GetSystemString(LiquidFoodTimeMetadata.ColumnNames.StandardReferenceID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodTimeMetadata.ColumnNames.StandardReferenceID, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodTime.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(LiquidFoodTimeMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodTimeMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodTime.Time
		/// </summary>
		virtual public System.String Time
		{
			get
			{
				return base.GetSystemString(LiquidFoodTimeMetadata.ColumnNames.Time);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodTimeMetadata.ColumnNames.Time, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodTime.FoodID
		/// </summary>
		virtual public System.String FoodID
		{
			get
			{
				return base.GetSystemString(LiquidFoodTimeMetadata.ColumnNames.FoodID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodTimeMetadata.ColumnNames.FoodID, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodTime.ChildrenFoodID
		/// </summary>
		virtual public System.String ChildrenFoodID
		{
			get
			{
				return base.GetSystemString(LiquidFoodTimeMetadata.ColumnNames.ChildrenFoodID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodTimeMetadata.ColumnNames.ChildrenFoodID, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodTime.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LiquidFoodTimeMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(LiquidFoodTimeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodTime.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LiquidFoodTimeMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodTimeMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLiquidFoodTime entity)
			{
				this.entity = entity;
			}
			
	
			public System.String StandardReferenceID
			{
				get
				{
					System.String data = entity.StandardReferenceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StandardReferenceID = null;
					else entity.StandardReferenceID = Convert.ToString(value);
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
				
			public System.String Time
			{
				get
				{
					System.String data = entity.Time;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Time = null;
					else entity.Time = Convert.ToString(value);
				}
			}
				
			public System.String FoodID
			{
				get
				{
					System.String data = entity.FoodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FoodID = null;
					else entity.FoodID = Convert.ToString(value);
				}
			}
				
			public System.String ChildrenFoodID
			{
				get
				{
					System.String data = entity.ChildrenFoodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChildrenFoodID = null;
					else entity.ChildrenFoodID = Convert.ToString(value);
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
			

			private esLiquidFoodTime entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLiquidFoodTimeQuery query)
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
				throw new Exception("esLiquidFoodTime can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class LiquidFoodTime : esLiquidFoodTime
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
	abstract public class esLiquidFoodTimeQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return LiquidFoodTimeMetadata.Meta();
			}
		}	
		

		public esQueryItem StandardReferenceID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodTimeMetadata.ColumnNames.StandardReferenceID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodTimeMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem Time
		{
			get
			{
				return new esQueryItem(this, LiquidFoodTimeMetadata.ColumnNames.Time, esSystemType.String);
			}
		} 
		
		public esQueryItem FoodID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodTimeMetadata.ColumnNames.FoodID, esSystemType.String);
			}
		} 
		
		public esQueryItem ChildrenFoodID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodTimeMetadata.ColumnNames.ChildrenFoodID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LiquidFoodTimeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodTimeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LiquidFoodTimeCollection")]
	public partial class LiquidFoodTimeCollection : esLiquidFoodTimeCollection, IEnumerable<LiquidFoodTime>
	{
		public LiquidFoodTimeCollection()
		{

		}
		
		public static implicit operator List<LiquidFoodTime>(LiquidFoodTimeCollection coll)
		{
			List<LiquidFoodTime> list = new List<LiquidFoodTime>();
			
			foreach (LiquidFoodTime emp in coll)
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
				return  LiquidFoodTimeMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LiquidFoodTimeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LiquidFoodTime(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LiquidFoodTime();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public LiquidFoodTimeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LiquidFoodTimeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(LiquidFoodTimeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public LiquidFoodTime AddNew()
		{
			LiquidFoodTime entity = base.AddNewEntity() as LiquidFoodTime;
			
			return entity;
		}

		public LiquidFoodTime FindByPrimaryKey(System.String itemID, System.String standardReferenceID, System.String time)
		{
			return base.FindByPrimaryKey(itemID, standardReferenceID, time) as LiquidFoodTime;
		}


		#region IEnumerable<LiquidFoodTime> Members

		IEnumerator<LiquidFoodTime> IEnumerable<LiquidFoodTime>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as LiquidFoodTime;
			}
		}

		#endregion
		
		private LiquidFoodTimeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LiquidFoodTime' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("LiquidFoodTime ({StandardReferenceID},{ItemID},{Time})")]
	[Serializable]
	public partial class LiquidFoodTime : esLiquidFoodTime
	{
		public LiquidFoodTime()
		{

		}
	
		public LiquidFoodTime(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LiquidFoodTimeMetadata.Meta();
			}
		}
		
		
		
		override protected esLiquidFoodTimeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LiquidFoodTimeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public LiquidFoodTimeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LiquidFoodTimeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(LiquidFoodTimeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private LiquidFoodTimeQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class LiquidFoodTimeQuery : esLiquidFoodTimeQuery
	{
		public LiquidFoodTimeQuery()
		{

		}		
		
		public LiquidFoodTimeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "LiquidFoodTimeQuery";
        }
		
			
	}


	[Serializable]
	public partial class LiquidFoodTimeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LiquidFoodTimeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LiquidFoodTimeMetadata.ColumnNames.StandardReferenceID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodTimeMetadata.PropertyNames.StandardReferenceID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodTimeMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodTimeMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodTimeMetadata.ColumnNames.Time, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodTimeMetadata.PropertyNames.Time;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodTimeMetadata.ColumnNames.FoodID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodTimeMetadata.PropertyNames.FoodID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodTimeMetadata.ColumnNames.ChildrenFoodID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodTimeMetadata.PropertyNames.ChildrenFoodID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodTimeMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LiquidFoodTimeMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodTimeMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodTimeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public LiquidFoodTimeMetadata Meta()
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
			 public const string StandardReferenceID = "StandardReferenceID";
			 public const string ItemID = "ItemID";
			 public const string Time = "Time";
			 public const string FoodID = "FoodID";
			 public const string ChildrenFoodID = "ChildrenFoodID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string StandardReferenceID = "StandardReferenceID";
			 public const string ItemID = "ItemID";
			 public const string Time = "Time";
			 public const string FoodID = "FoodID";
			 public const string ChildrenFoodID = "ChildrenFoodID";
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
			lock (typeof(LiquidFoodTimeMetadata))
			{
				if(LiquidFoodTimeMetadata.mapDelegates == null)
				{
					LiquidFoodTimeMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (LiquidFoodTimeMetadata.meta == null)
				{
					LiquidFoodTimeMetadata.meta = new LiquidFoodTimeMetadata();
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
				

				meta.AddTypeMap("StandardReferenceID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Time", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FoodID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChildrenFoodID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "LiquidFoodTime";
				meta.Destination = "LiquidFoodTime";
				
				meta.spInsert = "proc_LiquidFoodTimeInsert";				
				meta.spUpdate = "proc_LiquidFoodTimeUpdate";		
				meta.spDelete = "proc_LiquidFoodTimeDelete";
				meta.spLoadAll = "proc_LiquidFoodTimeLoadAll";
				meta.spLoadByPrimaryKey = "proc_LiquidFoodTimeLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LiquidFoodTimeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
