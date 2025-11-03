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
	abstract public class esLiquidFoodDietTimeCollection : esEntityCollectionWAuditLog
	{
		public esLiquidFoodDietTimeCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "LiquidFoodDietTimeCollection";
		}

		#region Query Logic
		protected void InitQuery(esLiquidFoodDietTimeQuery query)
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
			this.InitQuery(query as esLiquidFoodDietTimeQuery);
		}
		#endregion
		
		virtual public LiquidFoodDietTime DetachEntity(LiquidFoodDietTime entity)
		{
			return base.DetachEntity(entity) as LiquidFoodDietTime;
		}
		
		virtual public LiquidFoodDietTime AttachEntity(LiquidFoodDietTime entity)
		{
			return base.AttachEntity(entity) as LiquidFoodDietTime;
		}
		
		virtual public void Combine(LiquidFoodDietTimeCollection collection)
		{
			base.Combine(collection);
		}
		
		new public LiquidFoodDietTime this[int index]
		{
			get
			{
				return base[index] as LiquidFoodDietTime;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LiquidFoodDietTime);
		}
	}



	[Serializable]
	abstract public class esLiquidFoodDietTime : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLiquidFoodDietTimeQuery GetDynamicQuery()
		{
			return null;
		}

		public esLiquidFoodDietTime()
		{

		}

		public esLiquidFoodDietTime(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String dietID, System.String time)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(dietID, time);
			else
				return LoadByPrimaryKeyStoredProcedure(dietID, time);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String dietID, System.String time)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(dietID, time);
			else
				return LoadByPrimaryKeyStoredProcedure(dietID, time);
		}

		private bool LoadByPrimaryKeyDynamic(System.String dietID, System.String time)
		{
			esLiquidFoodDietTimeQuery query = this.GetDynamicQuery();
			query.Where(query.DietID == dietID, query.Time == time);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String dietID, System.String time)
		{
			esParameters parms = new esParameters();
			parms.Add("DietID",dietID);			parms.Add("Time",time);
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
						case "DietID": this.str.DietID = (string)value; break;							
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
		/// Maps to LiquidFoodDietTime.DietID
		/// </summary>
		virtual public System.String DietID
		{
			get
			{
				return base.GetSystemString(LiquidFoodDietTimeMetadata.ColumnNames.DietID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodDietTimeMetadata.ColumnNames.DietID, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodDietTime.Time
		/// </summary>
		virtual public System.String Time
		{
			get
			{
				return base.GetSystemString(LiquidFoodDietTimeMetadata.ColumnNames.Time);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodDietTimeMetadata.ColumnNames.Time, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodDietTime.FoodID
		/// </summary>
		virtual public System.String FoodID
		{
			get
			{
				return base.GetSystemString(LiquidFoodDietTimeMetadata.ColumnNames.FoodID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodDietTimeMetadata.ColumnNames.FoodID, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodDietTime.ChildrenFoodID
		/// </summary>
		virtual public System.String ChildrenFoodID
		{
			get
			{
				return base.GetSystemString(LiquidFoodDietTimeMetadata.ColumnNames.ChildrenFoodID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodDietTimeMetadata.ColumnNames.ChildrenFoodID, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodDietTime.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LiquidFoodDietTimeMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(LiquidFoodDietTimeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodDietTime.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LiquidFoodDietTimeMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodDietTimeMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLiquidFoodDietTime entity)
			{
				this.entity = entity;
			}
			
	
			public System.String DietID
			{
				get
				{
					System.String data = entity.DietID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DietID = null;
					else entity.DietID = Convert.ToString(value);
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
			

			private esLiquidFoodDietTime entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLiquidFoodDietTimeQuery query)
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
				throw new Exception("esLiquidFoodDietTime can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class LiquidFoodDietTime : esLiquidFoodDietTime
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
	abstract public class esLiquidFoodDietTimeQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return LiquidFoodDietTimeMetadata.Meta();
			}
		}	
		

		public esQueryItem DietID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodDietTimeMetadata.ColumnNames.DietID, esSystemType.String);
			}
		} 
		
		public esQueryItem Time
		{
			get
			{
				return new esQueryItem(this, LiquidFoodDietTimeMetadata.ColumnNames.Time, esSystemType.String);
			}
		} 
		
		public esQueryItem FoodID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodDietTimeMetadata.ColumnNames.FoodID, esSystemType.String);
			}
		} 
		
		public esQueryItem ChildrenFoodID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodDietTimeMetadata.ColumnNames.ChildrenFoodID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LiquidFoodDietTimeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodDietTimeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LiquidFoodDietTimeCollection")]
	public partial class LiquidFoodDietTimeCollection : esLiquidFoodDietTimeCollection, IEnumerable<LiquidFoodDietTime>
	{
		public LiquidFoodDietTimeCollection()
		{

		}
		
		public static implicit operator List<LiquidFoodDietTime>(LiquidFoodDietTimeCollection coll)
		{
			List<LiquidFoodDietTime> list = new List<LiquidFoodDietTime>();
			
			foreach (LiquidFoodDietTime emp in coll)
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
				return  LiquidFoodDietTimeMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LiquidFoodDietTimeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LiquidFoodDietTime(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LiquidFoodDietTime();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public LiquidFoodDietTimeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LiquidFoodDietTimeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(LiquidFoodDietTimeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public LiquidFoodDietTime AddNew()
		{
			LiquidFoodDietTime entity = base.AddNewEntity() as LiquidFoodDietTime;
			
			return entity;
		}

		public LiquidFoodDietTime FindByPrimaryKey(System.String dietID, System.String time)
		{
			return base.FindByPrimaryKey(dietID, time) as LiquidFoodDietTime;
		}


		#region IEnumerable<LiquidFoodDietTime> Members

		IEnumerator<LiquidFoodDietTime> IEnumerable<LiquidFoodDietTime>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as LiquidFoodDietTime;
			}
		}

		#endregion
		
		private LiquidFoodDietTimeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LiquidFoodDietTime' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("LiquidFoodDietTime ({DietID},{Time})")]
	[Serializable]
	public partial class LiquidFoodDietTime : esLiquidFoodDietTime
	{
		public LiquidFoodDietTime()
		{

		}
	
		public LiquidFoodDietTime(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LiquidFoodDietTimeMetadata.Meta();
			}
		}
		
		
		
		override protected esLiquidFoodDietTimeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LiquidFoodDietTimeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public LiquidFoodDietTimeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LiquidFoodDietTimeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(LiquidFoodDietTimeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private LiquidFoodDietTimeQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class LiquidFoodDietTimeQuery : esLiquidFoodDietTimeQuery
	{
		public LiquidFoodDietTimeQuery()
		{

		}		
		
		public LiquidFoodDietTimeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "LiquidFoodDietTimeQuery";
        }
		
			
	}


	[Serializable]
	public partial class LiquidFoodDietTimeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LiquidFoodDietTimeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LiquidFoodDietTimeMetadata.ColumnNames.DietID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodDietTimeMetadata.PropertyNames.DietID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodDietTimeMetadata.ColumnNames.Time, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodDietTimeMetadata.PropertyNames.Time;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodDietTimeMetadata.ColumnNames.FoodID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodDietTimeMetadata.PropertyNames.FoodID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodDietTimeMetadata.ColumnNames.ChildrenFoodID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodDietTimeMetadata.PropertyNames.ChildrenFoodID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodDietTimeMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LiquidFoodDietTimeMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodDietTimeMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodDietTimeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public LiquidFoodDietTimeMetadata Meta()
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
			 public const string DietID = "DietID";
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
			 public const string DietID = "DietID";
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
			lock (typeof(LiquidFoodDietTimeMetadata))
			{
				if(LiquidFoodDietTimeMetadata.mapDelegates == null)
				{
					LiquidFoodDietTimeMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (LiquidFoodDietTimeMetadata.meta == null)
				{
					LiquidFoodDietTimeMetadata.meta = new LiquidFoodDietTimeMetadata();
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
				

				meta.AddTypeMap("DietID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Time", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FoodID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChildrenFoodID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "LiquidFoodDietTime";
				meta.Destination = "LiquidFoodDietTime";
				
				meta.spInsert = "proc_LiquidFoodDietTimeInsert";				
				meta.spUpdate = "proc_LiquidFoodDietTimeUpdate";		
				meta.spDelete = "proc_LiquidFoodDietTimeDelete";
				meta.spLoadAll = "proc_LiquidFoodDietTimeLoadAll";
				meta.spLoadByPrimaryKey = "proc_LiquidFoodDietTimeLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LiquidFoodDietTimeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
