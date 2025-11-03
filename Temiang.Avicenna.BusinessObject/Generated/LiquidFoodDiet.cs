/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/27/2015 10:11:14 AM
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
	abstract public class esLiquidFoodDietCollection : esEntityCollectionWAuditLog
	{
		public esLiquidFoodDietCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "LiquidFoodDietCollection";
		}

		#region Query Logic
		protected void InitQuery(esLiquidFoodDietQuery query)
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
			this.InitQuery(query as esLiquidFoodDietQuery);
		}
		#endregion
		
		virtual public LiquidFoodDiet DetachEntity(LiquidFoodDiet entity)
		{
			return base.DetachEntity(entity) as LiquidFoodDiet;
		}
		
		virtual public LiquidFoodDiet AttachEntity(LiquidFoodDiet entity)
		{
			return base.AttachEntity(entity) as LiquidFoodDiet;
		}
		
		virtual public void Combine(LiquidFoodDietCollection collection)
		{
			base.Combine(collection);
		}
		
		new public LiquidFoodDiet this[int index]
		{
			get
			{
				return base[index] as LiquidFoodDiet;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LiquidFoodDiet);
		}
	}



	[Serializable]
	abstract public class esLiquidFoodDiet : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLiquidFoodDietQuery GetDynamicQuery()
		{
			return null;
		}

		public esLiquidFoodDiet()
		{

		}

		public esLiquidFoodDiet(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String dietID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(dietID);
			else
				return LoadByPrimaryKeyStoredProcedure(dietID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String dietID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(dietID);
			else
				return LoadByPrimaryKeyStoredProcedure(dietID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String dietID)
		{
			esLiquidFoodDietQuery query = this.GetDynamicQuery();
			query.Where(query.DietID == dietID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String dietID)
		{
			esParameters parms = new esParameters();
			parms.Add("DietID",dietID);
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
		/// Maps to LiquidFoodDiet.DietID
		/// </summary>
		virtual public System.String DietID
		{
			get
			{
				return base.GetSystemString(LiquidFoodDietMetadata.ColumnNames.DietID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodDietMetadata.ColumnNames.DietID, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodDiet.FoodID
		/// </summary>
		virtual public System.String FoodID
		{
			get
			{
				return base.GetSystemString(LiquidFoodDietMetadata.ColumnNames.FoodID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodDietMetadata.ColumnNames.FoodID, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodDiet.ChildrenFoodID
		/// </summary>
		virtual public System.String ChildrenFoodID
		{
			get
			{
				return base.GetSystemString(LiquidFoodDietMetadata.ColumnNames.ChildrenFoodID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodDietMetadata.ColumnNames.ChildrenFoodID, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodDiet.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LiquidFoodDietMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(LiquidFoodDietMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodDiet.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LiquidFoodDietMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodDietMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLiquidFoodDiet entity)
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
			

			private esLiquidFoodDiet entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLiquidFoodDietQuery query)
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
				throw new Exception("esLiquidFoodDiet can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class LiquidFoodDiet : esLiquidFoodDiet
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
	abstract public class esLiquidFoodDietQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return LiquidFoodDietMetadata.Meta();
			}
		}	
		

		public esQueryItem DietID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodDietMetadata.ColumnNames.DietID, esSystemType.String);
			}
		} 
		
		public esQueryItem FoodID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodDietMetadata.ColumnNames.FoodID, esSystemType.String);
			}
		} 
		
		public esQueryItem ChildrenFoodID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodDietMetadata.ColumnNames.ChildrenFoodID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LiquidFoodDietMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodDietMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LiquidFoodDietCollection")]
	public partial class LiquidFoodDietCollection : esLiquidFoodDietCollection, IEnumerable<LiquidFoodDiet>
	{
		public LiquidFoodDietCollection()
		{

		}
		
		public static implicit operator List<LiquidFoodDiet>(LiquidFoodDietCollection coll)
		{
			List<LiquidFoodDiet> list = new List<LiquidFoodDiet>();
			
			foreach (LiquidFoodDiet emp in coll)
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
				return  LiquidFoodDietMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LiquidFoodDietQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LiquidFoodDiet(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LiquidFoodDiet();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public LiquidFoodDietQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LiquidFoodDietQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(LiquidFoodDietQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public LiquidFoodDiet AddNew()
		{
			LiquidFoodDiet entity = base.AddNewEntity() as LiquidFoodDiet;
			
			return entity;
		}

		public LiquidFoodDiet FindByPrimaryKey(System.String dietID)
		{
			return base.FindByPrimaryKey(dietID) as LiquidFoodDiet;
		}


		#region IEnumerable<LiquidFoodDiet> Members

		IEnumerator<LiquidFoodDiet> IEnumerable<LiquidFoodDiet>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as LiquidFoodDiet;
			}
		}

		#endregion
		
		private LiquidFoodDietQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LiquidFoodDiet' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("LiquidFoodDiet ({DietID})")]
	[Serializable]
	public partial class LiquidFoodDiet : esLiquidFoodDiet
	{
		public LiquidFoodDiet()
		{

		}
	
		public LiquidFoodDiet(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LiquidFoodDietMetadata.Meta();
			}
		}
		
		
		
		override protected esLiquidFoodDietQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LiquidFoodDietQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public LiquidFoodDietQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LiquidFoodDietQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(LiquidFoodDietQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private LiquidFoodDietQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class LiquidFoodDietQuery : esLiquidFoodDietQuery
	{
		public LiquidFoodDietQuery()
		{

		}		
		
		public LiquidFoodDietQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "LiquidFoodDietQuery";
        }
		
			
	}


	[Serializable]
	public partial class LiquidFoodDietMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LiquidFoodDietMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LiquidFoodDietMetadata.ColumnNames.DietID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodDietMetadata.PropertyNames.DietID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodDietMetadata.ColumnNames.FoodID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodDietMetadata.PropertyNames.FoodID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodDietMetadata.ColumnNames.ChildrenFoodID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodDietMetadata.PropertyNames.ChildrenFoodID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodDietMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LiquidFoodDietMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodDietMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodDietMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public LiquidFoodDietMetadata Meta()
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
			lock (typeof(LiquidFoodDietMetadata))
			{
				if(LiquidFoodDietMetadata.mapDelegates == null)
				{
					LiquidFoodDietMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (LiquidFoodDietMetadata.meta == null)
				{
					LiquidFoodDietMetadata.meta = new LiquidFoodDietMetadata();
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
				meta.AddTypeMap("FoodID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChildrenFoodID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "LiquidFoodDiet";
				meta.Destination = "LiquidFoodDiet";
				
				meta.spInsert = "proc_LiquidFoodDietInsert";				
				meta.spUpdate = "proc_LiquidFoodDietUpdate";		
				meta.spDelete = "proc_LiquidFoodDietDelete";
				meta.spLoadAll = "proc_LiquidFoodDietLoadAll";
				meta.spLoadByPrimaryKey = "proc_LiquidFoodDietLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LiquidFoodDietMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
