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
	abstract public class esLiquidFoodDietTimeGenderCollection : esEntityCollectionWAuditLog
	{
		public esLiquidFoodDietTimeGenderCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "LiquidFoodDietTimeGenderCollection";
		}

		#region Query Logic
		protected void InitQuery(esLiquidFoodDietTimeGenderQuery query)
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
			this.InitQuery(query as esLiquidFoodDietTimeGenderQuery);
		}
		#endregion
		
		virtual public LiquidFoodDietTimeGender DetachEntity(LiquidFoodDietTimeGender entity)
		{
			return base.DetachEntity(entity) as LiquidFoodDietTimeGender;
		}
		
		virtual public LiquidFoodDietTimeGender AttachEntity(LiquidFoodDietTimeGender entity)
		{
			return base.AttachEntity(entity) as LiquidFoodDietTimeGender;
		}
		
		virtual public void Combine(LiquidFoodDietTimeGenderCollection collection)
		{
			base.Combine(collection);
		}
		
		new public LiquidFoodDietTimeGender this[int index]
		{
			get
			{
				return base[index] as LiquidFoodDietTimeGender;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LiquidFoodDietTimeGender);
		}
	}



	[Serializable]
	abstract public class esLiquidFoodDietTimeGender : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLiquidFoodDietTimeGenderQuery GetDynamicQuery()
		{
			return null;
		}

		public esLiquidFoodDietTimeGender()
		{

		}

		public esLiquidFoodDietTimeGender(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String dietID, System.String gender, System.String time)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(dietID, gender, time);
			else
				return LoadByPrimaryKeyStoredProcedure(dietID, gender, time);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String dietID, System.String gender, System.String time)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(dietID, gender, time);
			else
				return LoadByPrimaryKeyStoredProcedure(dietID, gender, time);
		}

		private bool LoadByPrimaryKeyDynamic(System.String dietID, System.String gender, System.String time)
		{
			esLiquidFoodDietTimeGenderQuery query = this.GetDynamicQuery();
			query.Where(query.DietID == dietID, query.Gender == gender, query.Time == time);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String dietID, System.String gender, System.String time)
		{
			esParameters parms = new esParameters();
			parms.Add("DietID",dietID);			parms.Add("Gender",gender);			parms.Add("Time",time);
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
						case "Gender": this.str.Gender = (string)value; break;							
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
		/// Maps to LiquidFoodDietTimeGender.DietID
		/// </summary>
		virtual public System.String DietID
		{
			get
			{
				return base.GetSystemString(LiquidFoodDietTimeGenderMetadata.ColumnNames.DietID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodDietTimeGenderMetadata.ColumnNames.DietID, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodDietTimeGender.Time
		/// </summary>
		virtual public System.String Time
		{
			get
			{
				return base.GetSystemString(LiquidFoodDietTimeGenderMetadata.ColumnNames.Time);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodDietTimeGenderMetadata.ColumnNames.Time, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodDietTimeGender.Gender
		/// </summary>
		virtual public System.String Gender
		{
			get
			{
				return base.GetSystemString(LiquidFoodDietTimeGenderMetadata.ColumnNames.Gender);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodDietTimeGenderMetadata.ColumnNames.Gender, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodDietTimeGender.FoodID
		/// </summary>
		virtual public System.String FoodID
		{
			get
			{
				return base.GetSystemString(LiquidFoodDietTimeGenderMetadata.ColumnNames.FoodID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodDietTimeGenderMetadata.ColumnNames.FoodID, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodDietTimeGender.ChildrenFoodID
		/// </summary>
		virtual public System.String ChildrenFoodID
		{
			get
			{
				return base.GetSystemString(LiquidFoodDietTimeGenderMetadata.ColumnNames.ChildrenFoodID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodDietTimeGenderMetadata.ColumnNames.ChildrenFoodID, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodDietTimeGender.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LiquidFoodDietTimeGenderMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(LiquidFoodDietTimeGenderMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodDietTimeGender.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LiquidFoodDietTimeGenderMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodDietTimeGenderMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLiquidFoodDietTimeGender entity)
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
				
			public System.String Gender
			{
				get
				{
					System.String data = entity.Gender;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Gender = null;
					else entity.Gender = Convert.ToString(value);
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
			

			private esLiquidFoodDietTimeGender entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLiquidFoodDietTimeGenderQuery query)
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
				throw new Exception("esLiquidFoodDietTimeGender can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class LiquidFoodDietTimeGender : esLiquidFoodDietTimeGender
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
	abstract public class esLiquidFoodDietTimeGenderQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return LiquidFoodDietTimeGenderMetadata.Meta();
			}
		}	
		

		public esQueryItem DietID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodDietTimeGenderMetadata.ColumnNames.DietID, esSystemType.String);
			}
		} 
		
		public esQueryItem Time
		{
			get
			{
				return new esQueryItem(this, LiquidFoodDietTimeGenderMetadata.ColumnNames.Time, esSystemType.String);
			}
		} 
		
		public esQueryItem Gender
		{
			get
			{
				return new esQueryItem(this, LiquidFoodDietTimeGenderMetadata.ColumnNames.Gender, esSystemType.String);
			}
		} 
		
		public esQueryItem FoodID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodDietTimeGenderMetadata.ColumnNames.FoodID, esSystemType.String);
			}
		} 
		
		public esQueryItem ChildrenFoodID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodDietTimeGenderMetadata.ColumnNames.ChildrenFoodID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LiquidFoodDietTimeGenderMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodDietTimeGenderMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LiquidFoodDietTimeGenderCollection")]
	public partial class LiquidFoodDietTimeGenderCollection : esLiquidFoodDietTimeGenderCollection, IEnumerable<LiquidFoodDietTimeGender>
	{
		public LiquidFoodDietTimeGenderCollection()
		{

		}
		
		public static implicit operator List<LiquidFoodDietTimeGender>(LiquidFoodDietTimeGenderCollection coll)
		{
			List<LiquidFoodDietTimeGender> list = new List<LiquidFoodDietTimeGender>();
			
			foreach (LiquidFoodDietTimeGender emp in coll)
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
				return  LiquidFoodDietTimeGenderMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LiquidFoodDietTimeGenderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LiquidFoodDietTimeGender(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LiquidFoodDietTimeGender();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public LiquidFoodDietTimeGenderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LiquidFoodDietTimeGenderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(LiquidFoodDietTimeGenderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public LiquidFoodDietTimeGender AddNew()
		{
			LiquidFoodDietTimeGender entity = base.AddNewEntity() as LiquidFoodDietTimeGender;
			
			return entity;
		}

		public LiquidFoodDietTimeGender FindByPrimaryKey(System.String dietID, System.String gender, System.String time)
		{
			return base.FindByPrimaryKey(dietID, gender, time) as LiquidFoodDietTimeGender;
		}


		#region IEnumerable<LiquidFoodDietTimeGender> Members

		IEnumerator<LiquidFoodDietTimeGender> IEnumerable<LiquidFoodDietTimeGender>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as LiquidFoodDietTimeGender;
			}
		}

		#endregion
		
		private LiquidFoodDietTimeGenderQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LiquidFoodDietTimeGender' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("LiquidFoodDietTimeGender ({DietID},{Time},{Gender})")]
	[Serializable]
	public partial class LiquidFoodDietTimeGender : esLiquidFoodDietTimeGender
	{
		public LiquidFoodDietTimeGender()
		{

		}
	
		public LiquidFoodDietTimeGender(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LiquidFoodDietTimeGenderMetadata.Meta();
			}
		}
		
		
		
		override protected esLiquidFoodDietTimeGenderQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LiquidFoodDietTimeGenderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public LiquidFoodDietTimeGenderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LiquidFoodDietTimeGenderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(LiquidFoodDietTimeGenderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private LiquidFoodDietTimeGenderQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class LiquidFoodDietTimeGenderQuery : esLiquidFoodDietTimeGenderQuery
	{
		public LiquidFoodDietTimeGenderQuery()
		{

		}		
		
		public LiquidFoodDietTimeGenderQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "LiquidFoodDietTimeGenderQuery";
        }
		
			
	}


	[Serializable]
	public partial class LiquidFoodDietTimeGenderMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LiquidFoodDietTimeGenderMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LiquidFoodDietTimeGenderMetadata.ColumnNames.DietID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodDietTimeGenderMetadata.PropertyNames.DietID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodDietTimeGenderMetadata.ColumnNames.Time, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodDietTimeGenderMetadata.PropertyNames.Time;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodDietTimeGenderMetadata.ColumnNames.Gender, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodDietTimeGenderMetadata.PropertyNames.Gender;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 1;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodDietTimeGenderMetadata.ColumnNames.FoodID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodDietTimeGenderMetadata.PropertyNames.FoodID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodDietTimeGenderMetadata.ColumnNames.ChildrenFoodID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodDietTimeGenderMetadata.PropertyNames.ChildrenFoodID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodDietTimeGenderMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LiquidFoodDietTimeGenderMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodDietTimeGenderMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodDietTimeGenderMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public LiquidFoodDietTimeGenderMetadata Meta()
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
			 public const string Gender = "Gender";
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
			 public const string Gender = "Gender";
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
			lock (typeof(LiquidFoodDietTimeGenderMetadata))
			{
				if(LiquidFoodDietTimeGenderMetadata.mapDelegates == null)
				{
					LiquidFoodDietTimeGenderMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (LiquidFoodDietTimeGenderMetadata.meta == null)
				{
					LiquidFoodDietTimeGenderMetadata.meta = new LiquidFoodDietTimeGenderMetadata();
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
				meta.AddTypeMap("Gender", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("FoodID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChildrenFoodID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "LiquidFoodDietTimeGender";
				meta.Destination = "LiquidFoodDietTimeGender";
				
				meta.spInsert = "proc_LiquidFoodDietTimeGenderInsert";				
				meta.spUpdate = "proc_LiquidFoodDietTimeGenderUpdate";		
				meta.spDelete = "proc_LiquidFoodDietTimeGenderDelete";
				meta.spLoadAll = "proc_LiquidFoodDietTimeGenderLoadAll";
				meta.spLoadByPrimaryKey = "proc_LiquidFoodDietTimeGenderLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LiquidFoodDietTimeGenderMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
