/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/23/2015 2:46:24 PM
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
	abstract public class esMealOrderItemPlanCollection : esEntityCollectionWAuditLog
	{
		public esMealOrderItemPlanCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "MealOrderItemPlanCollection";
		}

		#region Query Logic
		protected void InitQuery(esMealOrderItemPlanQuery query)
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
			this.InitQuery(query as esMealOrderItemPlanQuery);
		}
		#endregion
		
		virtual public MealOrderItemPlan DetachEntity(MealOrderItemPlan entity)
		{
			return base.DetachEntity(entity) as MealOrderItemPlan;
		}
		
		virtual public MealOrderItemPlan AttachEntity(MealOrderItemPlan entity)
		{
			return base.AttachEntity(entity) as MealOrderItemPlan;
		}
		
		virtual public void Combine(MealOrderItemPlanCollection collection)
		{
			base.Combine(collection);
		}
		
		new public MealOrderItemPlan this[int index]
		{
			get
			{
				return base[index] as MealOrderItemPlan;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MealOrderItemPlan);
		}
	}



	[Serializable]
	abstract public class esMealOrderItemPlan : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMealOrderItemPlanQuery GetDynamicQuery()
		{
			return null;
		}

		public esMealOrderItemPlan()
		{

		}

		public esMealOrderItemPlan(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String orderNo, System.String sRMealSet, System.String foodID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo, sRMealSet, foodID);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo, sRMealSet, foodID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String orderNo, System.String sRMealSet, System.String foodID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo, sRMealSet, foodID);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo, sRMealSet, foodID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String orderNo, System.String sRMealSet, System.String foodID)
		{
			esMealOrderItemPlanQuery query = this.GetDynamicQuery();
			query.Where(query.OrderNo == orderNo, query.SRMealSet == sRMealSet, query.FoodID == foodID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String orderNo, System.String sRMealSet, System.String foodID)
		{
			esParameters parms = new esParameters();
			parms.Add("OrderNo",orderNo);			parms.Add("SRMealSet",sRMealSet);			parms.Add("FoodID",foodID);
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
						case "OrderToDate": this.str.OrderToDate = (string)value; break;							
						case "SRMealSet": this.str.SRMealSet = (string)value; break;							
						case "FoodID": this.str.FoodID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "OrderToDate":
						
							if (value == null || value is System.DateTime)
								this.OrderToDate = (System.DateTime?)value;
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
		/// Maps to MealOrderItemPlan.OrderNo
		/// </summary>
		virtual public System.String OrderNo
		{
			get
			{
				return base.GetSystemString(MealOrderItemPlanMetadata.ColumnNames.OrderNo);
			}
			
			set
			{
				base.SetSystemString(MealOrderItemPlanMetadata.ColumnNames.OrderNo, value);
			}
		}
		
		/// <summary>
		/// Maps to MealOrderItemPlan.OrderToDate
		/// </summary>
		virtual public System.DateTime? OrderToDate
		{
			get
			{
				return base.GetSystemDateTime(MealOrderItemPlanMetadata.ColumnNames.OrderToDate);
			}
			
			set
			{
				base.SetSystemDateTime(MealOrderItemPlanMetadata.ColumnNames.OrderToDate, value);
			}
		}
		
		/// <summary>
		/// Maps to MealOrderItemPlan.SRMealSet
		/// </summary>
		virtual public System.String SRMealSet
		{
			get
			{
				return base.GetSystemString(MealOrderItemPlanMetadata.ColumnNames.SRMealSet);
			}
			
			set
			{
				base.SetSystemString(MealOrderItemPlanMetadata.ColumnNames.SRMealSet, value);
			}
		}
		
		/// <summary>
		/// Maps to MealOrderItemPlan.FoodID
		/// </summary>
		virtual public System.String FoodID
		{
			get
			{
				return base.GetSystemString(MealOrderItemPlanMetadata.ColumnNames.FoodID);
			}
			
			set
			{
				base.SetSystemString(MealOrderItemPlanMetadata.ColumnNames.FoodID, value);
			}
		}
		
		/// <summary>
		/// Maps to MealOrderItemPlan.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MealOrderItemPlanMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MealOrderItemPlanMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to MealOrderItemPlan.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MealOrderItemPlanMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(MealOrderItemPlanMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMealOrderItemPlan entity)
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
				
			public System.String OrderToDate
			{
				get
				{
					System.DateTime? data = entity.OrderToDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderToDate = null;
					else entity.OrderToDate = Convert.ToDateTime(value);
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
			

			private esMealOrderItemPlan entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMealOrderItemPlanQuery query)
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
				throw new Exception("esMealOrderItemPlan can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class MealOrderItemPlan : esMealOrderItemPlan
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
	abstract public class esMealOrderItemPlanQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return MealOrderItemPlanMetadata.Meta();
			}
		}	
		

		public esQueryItem OrderNo
		{
			get
			{
				return new esQueryItem(this, MealOrderItemPlanMetadata.ColumnNames.OrderNo, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderToDate
		{
			get
			{
				return new esQueryItem(this, MealOrderItemPlanMetadata.ColumnNames.OrderToDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem SRMealSet
		{
			get
			{
				return new esQueryItem(this, MealOrderItemPlanMetadata.ColumnNames.SRMealSet, esSystemType.String);
			}
		} 
		
		public esQueryItem FoodID
		{
			get
			{
				return new esQueryItem(this, MealOrderItemPlanMetadata.ColumnNames.FoodID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MealOrderItemPlanMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MealOrderItemPlanMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MealOrderItemPlanCollection")]
	public partial class MealOrderItemPlanCollection : esMealOrderItemPlanCollection, IEnumerable<MealOrderItemPlan>
	{
		public MealOrderItemPlanCollection()
		{

		}
		
		public static implicit operator List<MealOrderItemPlan>(MealOrderItemPlanCollection coll)
		{
			List<MealOrderItemPlan> list = new List<MealOrderItemPlan>();
			
			foreach (MealOrderItemPlan emp in coll)
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
				return  MealOrderItemPlanMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MealOrderItemPlanQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MealOrderItemPlan(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MealOrderItemPlan();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public MealOrderItemPlanQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MealOrderItemPlanQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(MealOrderItemPlanQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public MealOrderItemPlan AddNew()
		{
			MealOrderItemPlan entity = base.AddNewEntity() as MealOrderItemPlan;
			
			return entity;
		}

		public MealOrderItemPlan FindByPrimaryKey(System.String orderNo, System.String sRMealSet, System.String foodID)
		{
			return base.FindByPrimaryKey(orderNo, sRMealSet, foodID) as MealOrderItemPlan;
		}


		#region IEnumerable<MealOrderItemPlan> Members

		IEnumerator<MealOrderItemPlan> IEnumerable<MealOrderItemPlan>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as MealOrderItemPlan;
			}
		}

		#endregion
		
		private MealOrderItemPlanQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MealOrderItemPlan' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("MealOrderItemPlan ({OrderNo},{SRMealSet},{FoodID})")]
	[Serializable]
	public partial class MealOrderItemPlan : esMealOrderItemPlan
	{
		public MealOrderItemPlan()
		{

		}
	
		public MealOrderItemPlan(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MealOrderItemPlanMetadata.Meta();
			}
		}
		
		
		
		override protected esMealOrderItemPlanQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MealOrderItemPlanQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public MealOrderItemPlanQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MealOrderItemPlanQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(MealOrderItemPlanQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private MealOrderItemPlanQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class MealOrderItemPlanQuery : esMealOrderItemPlanQuery
	{
		public MealOrderItemPlanQuery()
		{

		}		
		
		public MealOrderItemPlanQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "MealOrderItemPlanQuery";
        }
		
			
	}


	[Serializable]
	public partial class MealOrderItemPlanMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MealOrderItemPlanMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MealOrderItemPlanMetadata.ColumnNames.OrderNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderItemPlanMetadata.PropertyNames.OrderNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealOrderItemPlanMetadata.ColumnNames.OrderToDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MealOrderItemPlanMetadata.PropertyNames.OrderToDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealOrderItemPlanMetadata.ColumnNames.SRMealSet, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderItemPlanMetadata.PropertyNames.SRMealSet;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealOrderItemPlanMetadata.ColumnNames.FoodID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderItemPlanMetadata.PropertyNames.FoodID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealOrderItemPlanMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MealOrderItemPlanMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealOrderItemPlanMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderItemPlanMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public MealOrderItemPlanMetadata Meta()
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
			 public const string OrderToDate = "OrderToDate";
			 public const string SRMealSet = "SRMealSet";
			 public const string FoodID = "FoodID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string OrderNo = "OrderNo";
			 public const string OrderToDate = "OrderToDate";
			 public const string SRMealSet = "SRMealSet";
			 public const string FoodID = "FoodID";
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
			lock (typeof(MealOrderItemPlanMetadata))
			{
				if(MealOrderItemPlanMetadata.mapDelegates == null)
				{
					MealOrderItemPlanMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MealOrderItemPlanMetadata.meta == null)
				{
					MealOrderItemPlanMetadata.meta = new MealOrderItemPlanMetadata();
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
				meta.AddTypeMap("OrderToDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRMealSet", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FoodID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "MealOrderItemPlan";
				meta.Destination = "MealOrderItemPlan";
				
				meta.spInsert = "proc_MealOrderItemPlanInsert";				
				meta.spUpdate = "proc_MealOrderItemPlanUpdate";		
				meta.spDelete = "proc_MealOrderItemPlanDelete";
				meta.spLoadAll = "proc_MealOrderItemPlanLoadAll";
				meta.spLoadByPrimaryKey = "proc_MealOrderItemPlanLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MealOrderItemPlanMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
