/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:13 PM
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
	abstract public class esCurrencyRateCollection : esEntityCollectionWAuditLog
	{
		public esCurrencyRateCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "CurrencyRateCollection";
		}

		#region Query Logic
		protected void InitQuery(esCurrencyRateQuery query)
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
			this.InitQuery(query as esCurrencyRateQuery);
		}
		#endregion
		
		virtual public CurrencyRate DetachEntity(CurrencyRate entity)
		{
			return base.DetachEntity(entity) as CurrencyRate;
		}
		
		virtual public CurrencyRate AttachEntity(CurrencyRate entity)
		{
			return base.AttachEntity(entity) as CurrencyRate;
		}
		
		virtual public void Combine(CurrencyRateCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CurrencyRate this[int index]
		{
			get
			{
				return base[index] as CurrencyRate;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CurrencyRate);
		}
	}



	[Serializable]
	abstract public class esCurrencyRate : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCurrencyRateQuery GetDynamicQuery()
		{
			return null;
		}

		public esCurrencyRate()
		{

		}

		public esCurrencyRate(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String currencyID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(currencyID);
			else
				return LoadByPrimaryKeyStoredProcedure(currencyID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String currencyID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(currencyID);
			else
				return LoadByPrimaryKeyStoredProcedure(currencyID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String currencyID)
		{
			esCurrencyRateQuery query = this.GetDynamicQuery();
			query.Where(query.CurrencyID == currencyID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String currencyID)
		{
			esParameters parms = new esParameters();
			parms.Add("CurrencyID",currencyID);
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
						case "CurrencyID": this.str.CurrencyID = (string)value; break;							
						case "CurrencyName": this.str.CurrencyName = (string)value; break;							
						case "CurrencyRate": this.str.CurrencyRate = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CurrencyRate":
						
							if (value == null || value is System.Decimal)
								this.CurrencyRate = (System.Decimal?)value;
							break;
						
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
		/// Maps to CurrencyRate.CurrencyID
		/// </summary>
		virtual public System.String CurrencyID
		{
			get
			{
				return base.GetSystemString(CurrencyRateMetadata.ColumnNames.CurrencyID);
			}
			
			set
			{
				base.SetSystemString(CurrencyRateMetadata.ColumnNames.CurrencyID, value);
			}
		}
		
		/// <summary>
		/// Maps to CurrencyRate.CurrencyName
		/// </summary>
		virtual public System.String CurrencyName
		{
			get
			{
				return base.GetSystemString(CurrencyRateMetadata.ColumnNames.CurrencyName);
			}
			
			set
			{
				base.SetSystemString(CurrencyRateMetadata.ColumnNames.CurrencyName, value);
			}
		}
		
		/// <summary>
		/// Maps to CurrencyRate.CurrencyRate
		/// </summary>
		virtual public System.Decimal? CurrencyRate
		{
			get
			{
				return base.GetSystemDecimal(CurrencyRateMetadata.ColumnNames.CurrencyRate);
			}
			
			set
			{
				base.SetSystemDecimal(CurrencyRateMetadata.ColumnNames.CurrencyRate, value);
			}
		}
		
		/// <summary>
		/// Maps to CurrencyRate.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(CurrencyRateMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(CurrencyRateMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to CurrencyRate.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CurrencyRateMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CurrencyRateMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to CurrencyRate.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CurrencyRateMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(CurrencyRateMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCurrencyRate entity)
			{
				this.entity = entity;
			}
			
	
			public System.String CurrencyID
			{
				get
				{
					System.String data = entity.CurrencyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CurrencyID = null;
					else entity.CurrencyID = Convert.ToString(value);
				}
			}
				
			public System.String CurrencyName
			{
				get
				{
					System.String data = entity.CurrencyName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CurrencyName = null;
					else entity.CurrencyName = Convert.ToString(value);
				}
			}
				
			public System.String CurrencyRate
			{
				get
				{
					System.Decimal? data = entity.CurrencyRate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CurrencyRate = null;
					else entity.CurrencyRate = Convert.ToDecimal(value);
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
			

			private esCurrencyRate entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCurrencyRateQuery query)
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
				throw new Exception("esCurrencyRate can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class CurrencyRate : esCurrencyRate
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
	abstract public class esCurrencyRateQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CurrencyRateMetadata.Meta();
			}
		}	
		

		public esQueryItem CurrencyID
		{
			get
			{
				return new esQueryItem(this, CurrencyRateMetadata.ColumnNames.CurrencyID, esSystemType.String);
			}
		} 
		
		public esQueryItem CurrencyName
		{
			get
			{
				return new esQueryItem(this, CurrencyRateMetadata.ColumnNames.CurrencyName, esSystemType.String);
			}
		} 
		
		public esQueryItem CurrencyRate
		{
			get
			{
				return new esQueryItem(this, CurrencyRateMetadata.ColumnNames.CurrencyRate, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, CurrencyRateMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CurrencyRateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CurrencyRateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CurrencyRateCollection")]
	public partial class CurrencyRateCollection : esCurrencyRateCollection, IEnumerable<CurrencyRate>
	{
		public CurrencyRateCollection()
		{

		}
		
		public static implicit operator List<CurrencyRate>(CurrencyRateCollection coll)
		{
			List<CurrencyRate> list = new List<CurrencyRate>();
			
			foreach (CurrencyRate emp in coll)
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
				return  CurrencyRateMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CurrencyRateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CurrencyRate(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CurrencyRate();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public CurrencyRateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CurrencyRateQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(CurrencyRateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public CurrencyRate AddNew()
		{
			CurrencyRate entity = base.AddNewEntity() as CurrencyRate;
			
			return entity;
		}

		public CurrencyRate FindByPrimaryKey(System.String currencyID)
		{
			return base.FindByPrimaryKey(currencyID) as CurrencyRate;
		}


		#region IEnumerable<CurrencyRate> Members

		IEnumerator<CurrencyRate> IEnumerable<CurrencyRate>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CurrencyRate;
			}
		}

		#endregion
		
		private CurrencyRateQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CurrencyRate' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("CurrencyRate ({CurrencyID})")]
	[Serializable]
	public partial class CurrencyRate : esCurrencyRate
	{
		public CurrencyRate()
		{

		}
	
		public CurrencyRate(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CurrencyRateMetadata.Meta();
			}
		}
		
		
		
		override protected esCurrencyRateQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CurrencyRateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public CurrencyRateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CurrencyRateQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(CurrencyRateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private CurrencyRateQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class CurrencyRateQuery : esCurrencyRateQuery
	{
		public CurrencyRateQuery()
		{

		}		
		
		public CurrencyRateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "CurrencyRateQuery";
        }
		
			
	}


	[Serializable]
	public partial class CurrencyRateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CurrencyRateMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CurrencyRateMetadata.ColumnNames.CurrencyID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CurrencyRateMetadata.PropertyNames.CurrencyID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CurrencyRateMetadata.ColumnNames.CurrencyName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CurrencyRateMetadata.PropertyNames.CurrencyName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(CurrencyRateMetadata.ColumnNames.CurrencyRate, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CurrencyRateMetadata.PropertyNames.CurrencyRate;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(CurrencyRateMetadata.ColumnNames.IsActive, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CurrencyRateMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(CurrencyRateMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CurrencyRateMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CurrencyRateMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CurrencyRateMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public CurrencyRateMetadata Meta()
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
			 public const string CurrencyID = "CurrencyID";
			 public const string CurrencyName = "CurrencyName";
			 public const string CurrencyRate = "CurrencyRate";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string CurrencyID = "CurrencyID";
			 public const string CurrencyName = "CurrencyName";
			 public const string CurrencyRate = "CurrencyRate";
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
			lock (typeof(CurrencyRateMetadata))
			{
				if(CurrencyRateMetadata.mapDelegates == null)
				{
					CurrencyRateMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CurrencyRateMetadata.meta == null)
				{
					CurrencyRateMetadata.meta = new CurrencyRateMetadata();
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
				

				meta.AddTypeMap("CurrencyID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CurrencyName", new esTypeMap("nchar", "System.String"));
				meta.AddTypeMap("CurrencyRate", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "CurrencyRate";
				meta.Destination = "CurrencyRate";
				
				meta.spInsert = "proc_CurrencyRateInsert";				
				meta.spUpdate = "proc_CurrencyRateUpdate";		
				meta.spDelete = "proc_CurrencyRateDelete";
				meta.spLoadAll = "proc_CurrencyRateLoadAll";
				meta.spLoadByPrimaryKey = "proc_CurrencyRateLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CurrencyRateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
