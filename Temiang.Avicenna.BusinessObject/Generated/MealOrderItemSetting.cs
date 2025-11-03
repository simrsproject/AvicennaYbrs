/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/26/2014 2:40:45 PM
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
	abstract public class esMealOrderItemSettingCollection : esEntityCollectionWAuditLog
	{
		public esMealOrderItemSettingCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "MealOrderItemSettingCollection";
		}

		#region Query Logic
		protected void InitQuery(esMealOrderItemSettingQuery query)
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
			this.InitQuery(query as esMealOrderItemSettingQuery);
		}
		#endregion
		
		virtual public MealOrderItemSetting DetachEntity(MealOrderItemSetting entity)
		{
			return base.DetachEntity(entity) as MealOrderItemSetting;
		}
		
		virtual public MealOrderItemSetting AttachEntity(MealOrderItemSetting entity)
		{
			return base.AttachEntity(entity) as MealOrderItemSetting;
		}
		
		virtual public void Combine(MealOrderItemSettingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public MealOrderItemSetting this[int index]
		{
			get
			{
				return base[index] as MealOrderItemSetting;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MealOrderItemSetting);
		}
	}



	[Serializable]
	abstract public class esMealOrderItemSetting : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMealOrderItemSettingQuery GetDynamicQuery()
		{
			return null;
		}

		public esMealOrderItemSetting()
		{

		}

		public esMealOrderItemSetting(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String orderNo, System.String sRMealSet)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo, sRMealSet);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo, sRMealSet);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String orderNo, System.String sRMealSet)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo, sRMealSet);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo, sRMealSet);
		}

		private bool LoadByPrimaryKeyDynamic(System.String orderNo, System.String sRMealSet)
		{
			esMealOrderItemSettingQuery query = this.GetDynamicQuery();
			query.Where(query.OrderNo == orderNo, query.SRMealSet == sRMealSet);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String orderNo, System.String sRMealSet)
		{
			esParameters parms = new esParameters();
			parms.Add("OrderNo",orderNo);			parms.Add("SRMealSet",sRMealSet);
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
						case "SRMealSet": this.str.SRMealSet = (string)value; break;							
						case "IsOptional": this.str.IsOptional = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsOptional":
						
							if (value == null || value is System.Boolean)
								this.IsOptional = (System.Boolean?)value;
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
		/// Maps to MealOrderItemSetting.OrderNo
		/// </summary>
		virtual public System.String OrderNo
		{
			get
			{
				return base.GetSystemString(MealOrderItemSettingMetadata.ColumnNames.OrderNo);
			}
			
			set
			{
				base.SetSystemString(MealOrderItemSettingMetadata.ColumnNames.OrderNo, value);
			}
		}
		
		/// <summary>
		/// Maps to MealOrderItemSetting.SRMealSet
		/// </summary>
		virtual public System.String SRMealSet
		{
			get
			{
				return base.GetSystemString(MealOrderItemSettingMetadata.ColumnNames.SRMealSet);
			}
			
			set
			{
				base.SetSystemString(MealOrderItemSettingMetadata.ColumnNames.SRMealSet, value);
			}
		}
		
		/// <summary>
		/// Maps to MealOrderItemSetting.IsOptional
		/// </summary>
		virtual public System.Boolean? IsOptional
		{
			get
			{
				return base.GetSystemBoolean(MealOrderItemSettingMetadata.ColumnNames.IsOptional);
			}
			
			set
			{
				base.SetSystemBoolean(MealOrderItemSettingMetadata.ColumnNames.IsOptional, value);
			}
		}
		
		/// <summary>
		/// Maps to MealOrderItemSetting.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MealOrderItemSettingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MealOrderItemSettingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to MealOrderItemSetting.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MealOrderItemSettingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(MealOrderItemSettingMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMealOrderItemSetting entity)
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
				
			public System.String IsOptional
			{
				get
				{
					System.Boolean? data = entity.IsOptional;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOptional = null;
					else entity.IsOptional = Convert.ToBoolean(value);
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
			

			private esMealOrderItemSetting entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMealOrderItemSettingQuery query)
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
				throw new Exception("esMealOrderItemSetting can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class MealOrderItemSetting : esMealOrderItemSetting
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
	abstract public class esMealOrderItemSettingQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return MealOrderItemSettingMetadata.Meta();
			}
		}	
		

		public esQueryItem OrderNo
		{
			get
			{
				return new esQueryItem(this, MealOrderItemSettingMetadata.ColumnNames.OrderNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SRMealSet
		{
			get
			{
				return new esQueryItem(this, MealOrderItemSettingMetadata.ColumnNames.SRMealSet, esSystemType.String);
			}
		} 
		
		public esQueryItem IsOptional
		{
			get
			{
				return new esQueryItem(this, MealOrderItemSettingMetadata.ColumnNames.IsOptional, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MealOrderItemSettingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MealOrderItemSettingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MealOrderItemSettingCollection")]
	public partial class MealOrderItemSettingCollection : esMealOrderItemSettingCollection, IEnumerable<MealOrderItemSetting>
	{
		public MealOrderItemSettingCollection()
		{

		}
		
		public static implicit operator List<MealOrderItemSetting>(MealOrderItemSettingCollection coll)
		{
			List<MealOrderItemSetting> list = new List<MealOrderItemSetting>();
			
			foreach (MealOrderItemSetting emp in coll)
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
				return  MealOrderItemSettingMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MealOrderItemSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MealOrderItemSetting(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MealOrderItemSetting();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public MealOrderItemSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MealOrderItemSettingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(MealOrderItemSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public MealOrderItemSetting AddNew()
		{
			MealOrderItemSetting entity = base.AddNewEntity() as MealOrderItemSetting;
			
			return entity;
		}

		public MealOrderItemSetting FindByPrimaryKey(System.String orderNo, System.String sRMealSet)
		{
			return base.FindByPrimaryKey(orderNo, sRMealSet) as MealOrderItemSetting;
		}


		#region IEnumerable<MealOrderItemSetting> Members

		IEnumerator<MealOrderItemSetting> IEnumerable<MealOrderItemSetting>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as MealOrderItemSetting;
			}
		}

		#endregion
		
		private MealOrderItemSettingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MealOrderItemSetting' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("MealOrderItemSetting ({OrderNo},{SRMealSet})")]
	[Serializable]
	public partial class MealOrderItemSetting : esMealOrderItemSetting
	{
		public MealOrderItemSetting()
		{

		}
	
		public MealOrderItemSetting(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MealOrderItemSettingMetadata.Meta();
			}
		}
		
		
		
		override protected esMealOrderItemSettingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MealOrderItemSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public MealOrderItemSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MealOrderItemSettingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(MealOrderItemSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private MealOrderItemSettingQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class MealOrderItemSettingQuery : esMealOrderItemSettingQuery
	{
		public MealOrderItemSettingQuery()
		{

		}		
		
		public MealOrderItemSettingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "MealOrderItemSettingQuery";
        }
		
			
	}


	[Serializable]
	public partial class MealOrderItemSettingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MealOrderItemSettingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MealOrderItemSettingMetadata.ColumnNames.OrderNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderItemSettingMetadata.PropertyNames.OrderNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealOrderItemSettingMetadata.ColumnNames.SRMealSet, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderItemSettingMetadata.PropertyNames.SRMealSet;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealOrderItemSettingMetadata.ColumnNames.IsOptional, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MealOrderItemSettingMetadata.PropertyNames.IsOptional;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealOrderItemSettingMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MealOrderItemSettingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealOrderItemSettingMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderItemSettingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public MealOrderItemSettingMetadata Meta()
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
			 public const string SRMealSet = "SRMealSet";
			 public const string IsOptional = "IsOptional";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string OrderNo = "OrderNo";
			 public const string SRMealSet = "SRMealSet";
			 public const string IsOptional = "IsOptional";
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
			lock (typeof(MealOrderItemSettingMetadata))
			{
				if(MealOrderItemSettingMetadata.mapDelegates == null)
				{
					MealOrderItemSettingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MealOrderItemSettingMetadata.meta == null)
				{
					MealOrderItemSettingMetadata.meta = new MealOrderItemSettingMetadata();
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
				meta.AddTypeMap("SRMealSet", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOptional", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "MealOrderItemSetting";
				meta.Destination = "MealOrderItemSetting";
				
				meta.spInsert = "proc_MealOrderItemSettingInsert";				
				meta.spUpdate = "proc_MealOrderItemSettingUpdate";		
				meta.spDelete = "proc_MealOrderItemSettingDelete";
				meta.spLoadAll = "proc_MealOrderItemSettingLoadAll";
				meta.spLoadByPrimaryKey = "proc_MealOrderItemSettingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MealOrderItemSettingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
