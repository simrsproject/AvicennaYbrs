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
	abstract public class esLiquidFoodTimeGenderCollection : esEntityCollectionWAuditLog
	{
		public esLiquidFoodTimeGenderCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "LiquidFoodTimeGenderCollection";
		}

		#region Query Logic
		protected void InitQuery(esLiquidFoodTimeGenderQuery query)
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
			this.InitQuery(query as esLiquidFoodTimeGenderQuery);
		}
		#endregion
		
		virtual public LiquidFoodTimeGender DetachEntity(LiquidFoodTimeGender entity)
		{
			return base.DetachEntity(entity) as LiquidFoodTimeGender;
		}
		
		virtual public LiquidFoodTimeGender AttachEntity(LiquidFoodTimeGender entity)
		{
			return base.AttachEntity(entity) as LiquidFoodTimeGender;
		}
		
		virtual public void Combine(LiquidFoodTimeGenderCollection collection)
		{
			base.Combine(collection);
		}
		
		new public LiquidFoodTimeGender this[int index]
		{
			get
			{
				return base[index] as LiquidFoodTimeGender;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LiquidFoodTimeGender);
		}
	}



	[Serializable]
	abstract public class esLiquidFoodTimeGender : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLiquidFoodTimeGenderQuery GetDynamicQuery()
		{
			return null;
		}

		public esLiquidFoodTimeGender()
		{

		}

		public esLiquidFoodTimeGender(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String gender, System.String itemID, System.String standardReferenceID, System.String time)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(gender, itemID, standardReferenceID, time);
			else
				return LoadByPrimaryKeyStoredProcedure(gender, itemID, standardReferenceID, time);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String gender, System.String itemID, System.String standardReferenceID, System.String time)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(gender, itemID, standardReferenceID, time);
			else
				return LoadByPrimaryKeyStoredProcedure(gender, itemID, standardReferenceID, time);
		}

		private bool LoadByPrimaryKeyDynamic(System.String gender, System.String itemID, System.String standardReferenceID, System.String time)
		{
			esLiquidFoodTimeGenderQuery query = this.GetDynamicQuery();
			query.Where(query.Gender == gender, query.ItemID == itemID, query.StandardReferenceID == standardReferenceID, query.Time == time);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String gender, System.String itemID, System.String standardReferenceID, System.String time)
		{
			esParameters parms = new esParameters();
			parms.Add("Gender",gender);			parms.Add("ItemID",itemID);			parms.Add("StandardReferenceID",standardReferenceID);			parms.Add("Time",time);
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
		/// Maps to LiquidFoodTimeGender.StandardReferenceID
		/// </summary>
		virtual public System.String StandardReferenceID
		{
			get
			{
				return base.GetSystemString(LiquidFoodTimeGenderMetadata.ColumnNames.StandardReferenceID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodTimeGenderMetadata.ColumnNames.StandardReferenceID, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodTimeGender.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(LiquidFoodTimeGenderMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodTimeGenderMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodTimeGender.Time
		/// </summary>
		virtual public System.String Time
		{
			get
			{
				return base.GetSystemString(LiquidFoodTimeGenderMetadata.ColumnNames.Time);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodTimeGenderMetadata.ColumnNames.Time, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodTimeGender.Gender
		/// </summary>
		virtual public System.String Gender
		{
			get
			{
				return base.GetSystemString(LiquidFoodTimeGenderMetadata.ColumnNames.Gender);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodTimeGenderMetadata.ColumnNames.Gender, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodTimeGender.FoodID
		/// </summary>
		virtual public System.String FoodID
		{
			get
			{
				return base.GetSystemString(LiquidFoodTimeGenderMetadata.ColumnNames.FoodID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodTimeGenderMetadata.ColumnNames.FoodID, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodTimeGender.ChildrenFoodID
		/// </summary>
		virtual public System.String ChildrenFoodID
		{
			get
			{
				return base.GetSystemString(LiquidFoodTimeGenderMetadata.ColumnNames.ChildrenFoodID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodTimeGenderMetadata.ColumnNames.ChildrenFoodID, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodTimeGender.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LiquidFoodTimeGenderMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(LiquidFoodTimeGenderMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to LiquidFoodTimeGender.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LiquidFoodTimeGenderMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(LiquidFoodTimeGenderMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLiquidFoodTimeGender entity)
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
			

			private esLiquidFoodTimeGender entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLiquidFoodTimeGenderQuery query)
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
				throw new Exception("esLiquidFoodTimeGender can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class LiquidFoodTimeGender : esLiquidFoodTimeGender
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
	abstract public class esLiquidFoodTimeGenderQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return LiquidFoodTimeGenderMetadata.Meta();
			}
		}	
		

		public esQueryItem StandardReferenceID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodTimeGenderMetadata.ColumnNames.StandardReferenceID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodTimeGenderMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem Time
		{
			get
			{
				return new esQueryItem(this, LiquidFoodTimeGenderMetadata.ColumnNames.Time, esSystemType.String);
			}
		} 
		
		public esQueryItem Gender
		{
			get
			{
				return new esQueryItem(this, LiquidFoodTimeGenderMetadata.ColumnNames.Gender, esSystemType.String);
			}
		} 
		
		public esQueryItem FoodID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodTimeGenderMetadata.ColumnNames.FoodID, esSystemType.String);
			}
		} 
		
		public esQueryItem ChildrenFoodID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodTimeGenderMetadata.ColumnNames.ChildrenFoodID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LiquidFoodTimeGenderMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LiquidFoodTimeGenderMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LiquidFoodTimeGenderCollection")]
	public partial class LiquidFoodTimeGenderCollection : esLiquidFoodTimeGenderCollection, IEnumerable<LiquidFoodTimeGender>
	{
		public LiquidFoodTimeGenderCollection()
		{

		}
		
		public static implicit operator List<LiquidFoodTimeGender>(LiquidFoodTimeGenderCollection coll)
		{
			List<LiquidFoodTimeGender> list = new List<LiquidFoodTimeGender>();
			
			foreach (LiquidFoodTimeGender emp in coll)
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
				return  LiquidFoodTimeGenderMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LiquidFoodTimeGenderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LiquidFoodTimeGender(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LiquidFoodTimeGender();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public LiquidFoodTimeGenderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LiquidFoodTimeGenderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(LiquidFoodTimeGenderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public LiquidFoodTimeGender AddNew()
		{
			LiquidFoodTimeGender entity = base.AddNewEntity() as LiquidFoodTimeGender;
			
			return entity;
		}

		public LiquidFoodTimeGender FindByPrimaryKey(System.String gender, System.String itemID, System.String standardReferenceID, System.String time)
		{
			return base.FindByPrimaryKey(gender, itemID, standardReferenceID, time) as LiquidFoodTimeGender;
		}


		#region IEnumerable<LiquidFoodTimeGender> Members

		IEnumerator<LiquidFoodTimeGender> IEnumerable<LiquidFoodTimeGender>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as LiquidFoodTimeGender;
			}
		}

		#endregion
		
		private LiquidFoodTimeGenderQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LiquidFoodTimeGender' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("LiquidFoodTimeGender ({StandardReferenceID},{ItemID},{Time},{Gender})")]
	[Serializable]
	public partial class LiquidFoodTimeGender : esLiquidFoodTimeGender
	{
		public LiquidFoodTimeGender()
		{

		}
	
		public LiquidFoodTimeGender(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LiquidFoodTimeGenderMetadata.Meta();
			}
		}
		
		
		
		override protected esLiquidFoodTimeGenderQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LiquidFoodTimeGenderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public LiquidFoodTimeGenderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LiquidFoodTimeGenderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(LiquidFoodTimeGenderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private LiquidFoodTimeGenderQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class LiquidFoodTimeGenderQuery : esLiquidFoodTimeGenderQuery
	{
		public LiquidFoodTimeGenderQuery()
		{

		}		
		
		public LiquidFoodTimeGenderQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "LiquidFoodTimeGenderQuery";
        }
		
			
	}


	[Serializable]
	public partial class LiquidFoodTimeGenderMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LiquidFoodTimeGenderMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LiquidFoodTimeGenderMetadata.ColumnNames.StandardReferenceID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodTimeGenderMetadata.PropertyNames.StandardReferenceID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodTimeGenderMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodTimeGenderMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodTimeGenderMetadata.ColumnNames.Time, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodTimeGenderMetadata.PropertyNames.Time;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodTimeGenderMetadata.ColumnNames.Gender, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodTimeGenderMetadata.PropertyNames.Gender;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 1;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodTimeGenderMetadata.ColumnNames.FoodID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodTimeGenderMetadata.PropertyNames.FoodID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodTimeGenderMetadata.ColumnNames.ChildrenFoodID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodTimeGenderMetadata.PropertyNames.ChildrenFoodID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodTimeGenderMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LiquidFoodTimeGenderMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LiquidFoodTimeGenderMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = LiquidFoodTimeGenderMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public LiquidFoodTimeGenderMetadata Meta()
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
			 public const string StandardReferenceID = "StandardReferenceID";
			 public const string ItemID = "ItemID";
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
			lock (typeof(LiquidFoodTimeGenderMetadata))
			{
				if(LiquidFoodTimeGenderMetadata.mapDelegates == null)
				{
					LiquidFoodTimeGenderMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (LiquidFoodTimeGenderMetadata.meta == null)
				{
					LiquidFoodTimeGenderMetadata.meta = new LiquidFoodTimeGenderMetadata();
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
				meta.AddTypeMap("Gender", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("FoodID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChildrenFoodID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "LiquidFoodTimeGender";
				meta.Destination = "LiquidFoodTimeGender";
				
				meta.spInsert = "proc_LiquidFoodTimeGenderInsert";				
				meta.spUpdate = "proc_LiquidFoodTimeGenderUpdate";		
				meta.spDelete = "proc_LiquidFoodTimeGenderDelete";
				meta.spLoadAll = "proc_LiquidFoodTimeGenderLoadAll";
				meta.spLoadByPrimaryKey = "proc_LiquidFoodTimeGenderLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LiquidFoodTimeGenderMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
