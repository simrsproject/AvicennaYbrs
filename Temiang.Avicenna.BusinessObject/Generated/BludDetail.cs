/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/22/2020 3:21:57 PM
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
	abstract public class esBludDetailCollection : esEntityCollectionWAuditLog
	{
		public esBludDetailCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "BludDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esBludDetailQuery query)
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
			this.InitQuery(query as esBludDetailQuery);
		}
		#endregion
		
		virtual public BludDetail DetachEntity(BludDetail entity)
		{
			return base.DetachEntity(entity) as BludDetail;
		}
		
		virtual public BludDetail AttachEntity(BludDetail entity)
		{
			return base.AttachEntity(entity) as BludDetail;
		}
		
		virtual public void Combine(BludDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BludDetail this[int index]
		{
			get
			{
				return base[index] as BludDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BludDetail);
		}
	}



	[Serializable]
	abstract public class esBludDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBludDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esBludDetail()
		{

		}

		public esBludDetail(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 bludDetailID, System.Int32 bludID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bludDetailID, bludID);
			else
				return LoadByPrimaryKeyStoredProcedure(bludDetailID, bludID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 bludDetailID, System.Int32 bludID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bludDetailID, bludID);
			else
				return LoadByPrimaryKeyStoredProcedure(bludDetailID, bludID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 bludDetailID, System.Int32 bludID)
		{
			esBludDetailQuery query = this.GetDynamicQuery();
			query.Where(query.BludDetailID == bludDetailID, query.BludID == bludID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 bludDetailID, System.Int32 bludID)
		{
			esParameters parms = new esParameters();
			parms.Add("BludDetailID",bludDetailID);			parms.Add("BludID",bludID);
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
						case "BludDetailID": this.str.BludDetailID = (string)value; break;							
						case "BludID": this.str.BludID = (string)value; break;							
						case "KodeRekeningBludID": this.str.KodeRekeningBludID = (string)value; break;							
						case "Item": this.str.Item = (string)value; break;							
						case "Nominal": this.str.Nominal = (string)value; break;							
						case "Uraian": this.str.Uraian = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "BludDetailID":
						
							if (value == null || value is System.Int32)
								this.BludDetailID = (System.Int32?)value;
							break;
						
						case "BludID":
						
							if (value == null || value is System.Int32)
								this.BludID = (System.Int32?)value;
							break;
						
						case "KodeRekeningBludID":
						
							if (value == null || value is System.Int32)
								this.KodeRekeningBludID = (System.Int32?)value;
							break;
						
						case "Nominal":
						
							if (value == null || value is System.Decimal)
								this.Nominal = (System.Decimal?)value;
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
		/// Maps to BludDetail.BludDetailID
		/// </summary>
		virtual public System.Int32? BludDetailID
		{
			get
			{
				return base.GetSystemInt32(BludDetailMetadata.ColumnNames.BludDetailID);
			}
			
			set
			{
				base.SetSystemInt32(BludDetailMetadata.ColumnNames.BludDetailID, value);
			}
		}
		
		/// <summary>
		/// Maps to BludDetail.BludID
		/// </summary>
		virtual public System.Int32? BludID
		{
			get
			{
				return base.GetSystemInt32(BludDetailMetadata.ColumnNames.BludID);
			}
			
			set
			{
				base.SetSystemInt32(BludDetailMetadata.ColumnNames.BludID, value);
			}
		}
		
		/// <summary>
		/// Maps to BludDetail.KodeRekeningBludID
		/// </summary>
		virtual public System.Int32? KodeRekeningBludID
		{
			get
			{
				return base.GetSystemInt32(BludDetailMetadata.ColumnNames.KodeRekeningBludID);
			}
			
			set
			{
				base.SetSystemInt32(BludDetailMetadata.ColumnNames.KodeRekeningBludID, value);
			}
		}
		
		/// <summary>
		/// Maps to BludDetail.Item
		/// </summary>
		virtual public System.String Item
		{
			get
			{
				return base.GetSystemString(BludDetailMetadata.ColumnNames.Item);
			}
			
			set
			{
				base.SetSystemString(BludDetailMetadata.ColumnNames.Item, value);
			}
		}
		
		/// <summary>
		/// Maps to BludDetail.Nominal
		/// </summary>
		virtual public System.Decimal? Nominal
		{
			get
			{
				return base.GetSystemDecimal(BludDetailMetadata.ColumnNames.Nominal);
			}
			
			set
			{
				base.SetSystemDecimal(BludDetailMetadata.ColumnNames.Nominal, value);
			}
		}
		
		/// <summary>
		/// Maps to BludDetail.Uraian
		/// </summary>
		virtual public System.String Uraian
		{
			get
			{
				return base.GetSystemString(BludDetailMetadata.ColumnNames.Uraian);
			}
			
			set
			{
				base.SetSystemString(BludDetailMetadata.ColumnNames.Uraian, value);
			}
		}
		
		/// <summary>
		/// Maps to BludDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BludDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BludDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to BludDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BludDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BludDetailMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esBludDetail entity)
			{
				this.entity = entity;
			}
			
	
			public System.String BludDetailID
			{
				get
				{
					System.Int32? data = entity.BludDetailID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BludDetailID = null;
					else entity.BludDetailID = Convert.ToInt32(value);
				}
			}
				
			public System.String BludID
			{
				get
				{
					System.Int32? data = entity.BludID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BludID = null;
					else entity.BludID = Convert.ToInt32(value);
				}
			}
				
			public System.String KodeRekeningBludID
			{
				get
				{
					System.Int32? data = entity.KodeRekeningBludID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeRekeningBludID = null;
					else entity.KodeRekeningBludID = Convert.ToInt32(value);
				}
			}
				
			public System.String Item
			{
				get
				{
					System.String data = entity.Item;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Item = null;
					else entity.Item = Convert.ToString(value);
				}
			}
				
			public System.String Nominal
			{
				get
				{
					System.Decimal? data = entity.Nominal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Nominal = null;
					else entity.Nominal = Convert.ToDecimal(value);
				}
			}
				
			public System.String Uraian
			{
				get
				{
					System.String data = entity.Uraian;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Uraian = null;
					else entity.Uraian = Convert.ToString(value);
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
			

			private esBludDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBludDetailQuery query)
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
				throw new Exception("esBludDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class BludDetail : esBludDetail
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
	abstract public class esBludDetailQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return BludDetailMetadata.Meta();
			}
		}	
		

		public esQueryItem BludDetailID
		{
			get
			{
				return new esQueryItem(this, BludDetailMetadata.ColumnNames.BludDetailID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem BludID
		{
			get
			{
				return new esQueryItem(this, BludDetailMetadata.ColumnNames.BludID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KodeRekeningBludID
		{
			get
			{
				return new esQueryItem(this, BludDetailMetadata.ColumnNames.KodeRekeningBludID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Item
		{
			get
			{
				return new esQueryItem(this, BludDetailMetadata.ColumnNames.Item, esSystemType.String);
			}
		} 
		
		public esQueryItem Nominal
		{
			get
			{
				return new esQueryItem(this, BludDetailMetadata.ColumnNames.Nominal, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Uraian
		{
			get
			{
				return new esQueryItem(this, BludDetailMetadata.ColumnNames.Uraian, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BludDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BludDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BludDetailCollection")]
	public partial class BludDetailCollection : esBludDetailCollection, IEnumerable<BludDetail>
	{
		public BludDetailCollection()
		{

		}
		
		public static implicit operator List<BludDetail>(BludDetailCollection coll)
		{
			List<BludDetail> list = new List<BludDetail>();
			
			foreach (BludDetail emp in coll)
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
				return  BludDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BludDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BludDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BludDetail();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public BludDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BludDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(BludDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public BludDetail AddNew()
		{
			BludDetail entity = base.AddNewEntity() as BludDetail;
			
			return entity;
		}

		public BludDetail FindByPrimaryKey(System.Int32 bludDetailID, System.Int32 bludID)
		{
			return base.FindByPrimaryKey(bludDetailID, bludID) as BludDetail;
		}


		#region IEnumerable<BludDetail> Members

		IEnumerator<BludDetail> IEnumerable<BludDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BludDetail;
			}
		}

		#endregion
		
		private BludDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BludDetail' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("BludDetail ({BludDetailID},{BludID})")]
	[Serializable]
	public partial class BludDetail : esBludDetail
	{
		public BludDetail()
		{

		}
	
		public BludDetail(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BludDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esBludDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BludDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public BludDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BludDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(BludDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private BludDetailQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class BludDetailQuery : esBludDetailQuery
	{
		public BludDetailQuery()
		{

		}		
		
		public BludDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "BludDetailQuery";
        }
		
			
	}


	[Serializable]
	public partial class BludDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BludDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BludDetailMetadata.ColumnNames.BludDetailID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BludDetailMetadata.PropertyNames.BludDetailID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludDetailMetadata.ColumnNames.BludID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BludDetailMetadata.PropertyNames.BludID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludDetailMetadata.ColumnNames.KodeRekeningBludID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BludDetailMetadata.PropertyNames.KodeRekeningBludID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludDetailMetadata.ColumnNames.Item, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BludDetailMetadata.PropertyNames.Item;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludDetailMetadata.ColumnNames.Nominal, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BludDetailMetadata.PropertyNames.Nominal;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludDetailMetadata.ColumnNames.Uraian, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = BludDetailMetadata.PropertyNames.Uraian;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludDetailMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BludDetailMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludDetailMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = BludDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public BludDetailMetadata Meta()
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
			 public const string BludDetailID = "BludDetailID";
			 public const string BludID = "BludID";
			 public const string KodeRekeningBludID = "KodeRekeningBludID";
			 public const string Item = "Item";
			 public const string Nominal = "Nominal";
			 public const string Uraian = "Uraian";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string BludDetailID = "BludDetailID";
			 public const string BludID = "BludID";
			 public const string KodeRekeningBludID = "KodeRekeningBludID";
			 public const string Item = "Item";
			 public const string Nominal = "Nominal";
			 public const string Uraian = "Uraian";
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
			lock (typeof(BludDetailMetadata))
			{
				if(BludDetailMetadata.mapDelegates == null)
				{
					BludDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BludDetailMetadata.meta == null)
				{
					BludDetailMetadata.meta = new BludDetailMetadata();
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
				

				meta.AddTypeMap("BludDetailID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("BludID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KodeRekeningBludID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Item", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Nominal", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Uraian", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "BludDetail";
				meta.Destination = "BludDetail";
				
				meta.spInsert = "proc_BludDetailInsert";				
				meta.spUpdate = "proc_BludDetailUpdate";		
				meta.spDelete = "proc_BludDetailDelete";
				meta.spLoadAll = "proc_BludDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_BludDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BludDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
