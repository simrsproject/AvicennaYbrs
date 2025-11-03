/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:24 PM
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
	abstract public class esRecipeMarginValueCollection : esEntityCollectionWAuditLog
	{
		public esRecipeMarginValueCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RecipeMarginValueCollection";
		}

		#region Query Logic
		protected void InitQuery(esRecipeMarginValueQuery query)
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
			this.InitQuery(query as esRecipeMarginValueQuery);
		}
		#endregion
		
		virtual public RecipeMarginValue DetachEntity(RecipeMarginValue entity)
		{
			return base.DetachEntity(entity) as RecipeMarginValue;
		}
		
		virtual public RecipeMarginValue AttachEntity(RecipeMarginValue entity)
		{
			return base.AttachEntity(entity) as RecipeMarginValue;
		}
		
		virtual public void Combine(RecipeMarginValueCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RecipeMarginValue this[int index]
		{
			get
			{
				return base[index] as RecipeMarginValue;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RecipeMarginValue);
		}
	}



	[Serializable]
	abstract public class esRecipeMarginValue : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRecipeMarginValueQuery GetDynamicQuery()
		{
			return null;
		}

		public esRecipeMarginValue()
		{

		}

		public esRecipeMarginValue(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 counterID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(counterID);
			else
				return LoadByPrimaryKeyStoredProcedure(counterID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 counterID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(counterID);
			else
				return LoadByPrimaryKeyStoredProcedure(counterID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 counterID)
		{
			esRecipeMarginValueQuery query = this.GetDynamicQuery();
			query.Where(query.CounterID == counterID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 counterID)
		{
			esParameters parms = new esParameters();
			parms.Add("CounterID",counterID);
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
						case "CounterID": this.str.CounterID = (string)value; break;							
						case "StartingValue": this.str.StartingValue = (string)value; break;							
						case "EndingValue": this.str.EndingValue = (string)value; break;							
						case "RecipeAmount": this.str.RecipeAmount = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CounterID":
						
							if (value == null || value is System.Int32)
								this.CounterID = (System.Int32?)value;
							break;
						
						case "StartingValue":
						
							if (value == null || value is System.Decimal)
								this.StartingValue = (System.Decimal?)value;
							break;
						
						case "EndingValue":
						
							if (value == null || value is System.Decimal)
								this.EndingValue = (System.Decimal?)value;
							break;
						
						case "RecipeAmount":
						
							if (value == null || value is System.Decimal)
								this.RecipeAmount = (System.Decimal?)value;
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
		/// Maps to RecipeMarginValue.CounterID
		/// </summary>
		virtual public System.Int32? CounterID
		{
			get
			{
				return base.GetSystemInt32(RecipeMarginValueMetadata.ColumnNames.CounterID);
			}
			
			set
			{
				base.SetSystemInt32(RecipeMarginValueMetadata.ColumnNames.CounterID, value);
			}
		}
		
		/// <summary>
		/// Maps to RecipeMarginValue.StartingValue
		/// </summary>
		virtual public System.Decimal? StartingValue
		{
			get
			{
				return base.GetSystemDecimal(RecipeMarginValueMetadata.ColumnNames.StartingValue);
			}
			
			set
			{
				base.SetSystemDecimal(RecipeMarginValueMetadata.ColumnNames.StartingValue, value);
			}
		}
		
		/// <summary>
		/// Maps to RecipeMarginValue.EndingValue
		/// </summary>
		virtual public System.Decimal? EndingValue
		{
			get
			{
				return base.GetSystemDecimal(RecipeMarginValueMetadata.ColumnNames.EndingValue);
			}
			
			set
			{
				base.SetSystemDecimal(RecipeMarginValueMetadata.ColumnNames.EndingValue, value);
			}
		}
		
		/// <summary>
		/// Maps to RecipeMarginValue.RecipeAmount
		/// </summary>
		virtual public System.Decimal? RecipeAmount
		{
			get
			{
				return base.GetSystemDecimal(RecipeMarginValueMetadata.ColumnNames.RecipeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(RecipeMarginValueMetadata.ColumnNames.RecipeAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to RecipeMarginValue.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RecipeMarginValueMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RecipeMarginValueMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RecipeMarginValue.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RecipeMarginValueMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RecipeMarginValueMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRecipeMarginValue entity)
			{
				this.entity = entity;
			}
			
	
			public System.String CounterID
			{
				get
				{
					System.Int32? data = entity.CounterID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CounterID = null;
					else entity.CounterID = Convert.ToInt32(value);
				}
			}
				
			public System.String StartingValue
			{
				get
				{
					System.Decimal? data = entity.StartingValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartingValue = null;
					else entity.StartingValue = Convert.ToDecimal(value);
				}
			}
				
			public System.String EndingValue
			{
				get
				{
					System.Decimal? data = entity.EndingValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndingValue = null;
					else entity.EndingValue = Convert.ToDecimal(value);
				}
			}
				
			public System.String RecipeAmount
			{
				get
				{
					System.Decimal? data = entity.RecipeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecipeAmount = null;
					else entity.RecipeAmount = Convert.ToDecimal(value);
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
			

			private esRecipeMarginValue entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRecipeMarginValueQuery query)
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
				throw new Exception("esRecipeMarginValue can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RecipeMarginValue : esRecipeMarginValue
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
	abstract public class esRecipeMarginValueQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RecipeMarginValueMetadata.Meta();
			}
		}	
		

		public esQueryItem CounterID
		{
			get
			{
				return new esQueryItem(this, RecipeMarginValueMetadata.ColumnNames.CounterID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem StartingValue
		{
			get
			{
				return new esQueryItem(this, RecipeMarginValueMetadata.ColumnNames.StartingValue, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem EndingValue
		{
			get
			{
				return new esQueryItem(this, RecipeMarginValueMetadata.ColumnNames.EndingValue, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem RecipeAmount
		{
			get
			{
				return new esQueryItem(this, RecipeMarginValueMetadata.ColumnNames.RecipeAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RecipeMarginValueMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RecipeMarginValueMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RecipeMarginValueCollection")]
	public partial class RecipeMarginValueCollection : esRecipeMarginValueCollection, IEnumerable<RecipeMarginValue>
	{
		public RecipeMarginValueCollection()
		{

		}
		
		public static implicit operator List<RecipeMarginValue>(RecipeMarginValueCollection coll)
		{
			List<RecipeMarginValue> list = new List<RecipeMarginValue>();
			
			foreach (RecipeMarginValue emp in coll)
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
				return  RecipeMarginValueMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RecipeMarginValueQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RecipeMarginValue(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RecipeMarginValue();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RecipeMarginValueQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RecipeMarginValueQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RecipeMarginValueQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RecipeMarginValue AddNew()
		{
			RecipeMarginValue entity = base.AddNewEntity() as RecipeMarginValue;
			
			return entity;
		}

		public RecipeMarginValue FindByPrimaryKey(System.Int32 counterID)
		{
			return base.FindByPrimaryKey(counterID) as RecipeMarginValue;
		}


		#region IEnumerable<RecipeMarginValue> Members

		IEnumerator<RecipeMarginValue> IEnumerable<RecipeMarginValue>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RecipeMarginValue;
			}
		}

		#endregion
		
		private RecipeMarginValueQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RecipeMarginValue' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RecipeMarginValue ({CounterID})")]
	[Serializable]
	public partial class RecipeMarginValue : esRecipeMarginValue
	{
		public RecipeMarginValue()
		{

		}
	
		public RecipeMarginValue(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RecipeMarginValueMetadata.Meta();
			}
		}
		
		
		
		override protected esRecipeMarginValueQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RecipeMarginValueQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RecipeMarginValueQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RecipeMarginValueQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RecipeMarginValueQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RecipeMarginValueQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RecipeMarginValueQuery : esRecipeMarginValueQuery
	{
		public RecipeMarginValueQuery()
		{

		}		
		
		public RecipeMarginValueQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RecipeMarginValueQuery";
        }
		
			
	}


	[Serializable]
	public partial class RecipeMarginValueMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RecipeMarginValueMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RecipeMarginValueMetadata.ColumnNames.CounterID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RecipeMarginValueMetadata.PropertyNames.CounterID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecipeMarginValueMetadata.ColumnNames.StartingValue, 1, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RecipeMarginValueMetadata.PropertyNames.StartingValue;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecipeMarginValueMetadata.ColumnNames.EndingValue, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RecipeMarginValueMetadata.PropertyNames.EndingValue;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecipeMarginValueMetadata.ColumnNames.RecipeAmount, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RecipeMarginValueMetadata.PropertyNames.RecipeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecipeMarginValueMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RecipeMarginValueMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecipeMarginValueMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RecipeMarginValueMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RecipeMarginValueMetadata Meta()
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
			 public const string CounterID = "CounterID";
			 public const string StartingValue = "StartingValue";
			 public const string EndingValue = "EndingValue";
			 public const string RecipeAmount = "RecipeAmount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string CounterID = "CounterID";
			 public const string StartingValue = "StartingValue";
			 public const string EndingValue = "EndingValue";
			 public const string RecipeAmount = "RecipeAmount";
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
			lock (typeof(RecipeMarginValueMetadata))
			{
				if(RecipeMarginValueMetadata.mapDelegates == null)
				{
					RecipeMarginValueMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RecipeMarginValueMetadata.meta == null)
				{
					RecipeMarginValueMetadata.meta = new RecipeMarginValueMetadata();
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
				

				meta.AddTypeMap("CounterID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("StartingValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("EndingValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RecipeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RecipeMarginValue";
				meta.Destination = "RecipeMarginValue";
				
				meta.spInsert = "proc_RecipeMarginValueInsert";				
				meta.spUpdate = "proc_RecipeMarginValueUpdate";		
				meta.spDelete = "proc_RecipeMarginValueDelete";
				meta.spLoadAll = "proc_RecipeMarginValueLoadAll";
				meta.spLoadByPrimaryKey = "proc_RecipeMarginValueLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RecipeMarginValueMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
