/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/11/2020 1:51:33 PM
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
	abstract public class esBludDetailItemCollection : esEntityCollectionWAuditLog
	{
		public esBludDetailItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "BludDetailItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esBludDetailItemQuery query)
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
			this.InitQuery(query as esBludDetailItemQuery);
		}
		#endregion
		
		virtual public BludDetailItem DetachEntity(BludDetailItem entity)
		{
			return base.DetachEntity(entity) as BludDetailItem;
		}
		
		virtual public BludDetailItem AttachEntity(BludDetailItem entity)
		{
			return base.AttachEntity(entity) as BludDetailItem;
		}
		
		virtual public void Combine(BludDetailItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BludDetailItem this[int index]
		{
			get
			{
				return base[index] as BludDetailItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BludDetailItem);
		}
	}



	[Serializable]
	abstract public class esBludDetailItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBludDetailItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esBludDetailItem()
		{

		}

		public esBludDetailItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 bludDetailID, System.Int32 bludDetailItemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bludDetailID, bludDetailItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(bludDetailID, bludDetailItemID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 bludDetailID, System.Int32 bludDetailItemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bludDetailID, bludDetailItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(bludDetailID, bludDetailItemID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 bludDetailID, System.Int32 bludDetailItemID)
		{
			esBludDetailItemQuery query = this.GetDynamicQuery();
			query.Where(query.BludDetailID == bludDetailID, query.BludDetailItemID == bludDetailItemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 bludDetailID, System.Int32 bludDetailItemID)
		{
			esParameters parms = new esParameters();
			parms.Add("BludDetailID",bludDetailID);			parms.Add("BludDetailItemID",bludDetailItemID);
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
						case "BludDetailItemID": this.str.BludDetailItemID = (string)value; break;							
						case "BludDetailID": this.str.BludDetailID = (string)value; break;							
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;							
						case "Nominal": this.str.Nominal = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "BludDetailItemID":
						
							if (value == null || value is System.Int32)
								this.BludDetailItemID = (System.Int32?)value;
							break;
						
						case "BludDetailID":
						
							if (value == null || value is System.Int32)
								this.BludDetailID = (System.Int32?)value;
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
		/// Maps to BludDetailItem.BludDetailItemID
		/// </summary>
		virtual public System.Int32? BludDetailItemID
		{
			get
			{
				return base.GetSystemInt32(BludDetailItemMetadata.ColumnNames.BludDetailItemID);
			}
			
			set
			{
				base.SetSystemInt32(BludDetailItemMetadata.ColumnNames.BludDetailItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to BludDetailItem.BludDetailID
		/// </summary>
		virtual public System.Int32? BludDetailID
		{
			get
			{
				return base.GetSystemInt32(BludDetailItemMetadata.ColumnNames.BludDetailID);
			}
			
			set
			{
				base.SetSystemInt32(BludDetailItemMetadata.ColumnNames.BludDetailID, value);
			}
		}
		
		/// <summary>
		/// Maps to BludDetailItem.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(BludDetailItemMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(BludDetailItemMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to BludDetailItem.Nominal
		/// </summary>
		virtual public System.Decimal? Nominal
		{
			get
			{
				return base.GetSystemDecimal(BludDetailItemMetadata.ColumnNames.Nominal);
			}
			
			set
			{
				base.SetSystemDecimal(BludDetailItemMetadata.ColumnNames.Nominal, value);
			}
		}
		
		/// <summary>
		/// Maps to BludDetailItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BludDetailItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BludDetailItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to BludDetailItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BludDetailItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BludDetailItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esBludDetailItem entity)
			{
				this.entity = entity;
			}
			
	
			public System.String BludDetailItemID
			{
				get
				{
					System.Int32? data = entity.BludDetailItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BludDetailItemID = null;
					else entity.BludDetailItemID = Convert.ToInt32(value);
				}
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
				
			public System.String ReferenceNo
			{
				get
				{
					System.String data = entity.ReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceNo = null;
					else entity.ReferenceNo = Convert.ToString(value);
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
			

			private esBludDetailItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBludDetailItemQuery query)
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
				throw new Exception("esBludDetailItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esBludDetailItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return BludDetailItemMetadata.Meta();
			}
		}	
		

		public esQueryItem BludDetailItemID
		{
			get
			{
				return new esQueryItem(this, BludDetailItemMetadata.ColumnNames.BludDetailItemID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem BludDetailID
		{
			get
			{
				return new esQueryItem(this, BludDetailItemMetadata.ColumnNames.BludDetailID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, BludDetailItemMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem Nominal
		{
			get
			{
				return new esQueryItem(this, BludDetailItemMetadata.ColumnNames.Nominal, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BludDetailItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BludDetailItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BludDetailItemCollection")]
	public partial class BludDetailItemCollection : esBludDetailItemCollection, IEnumerable<BludDetailItem>
	{
		public BludDetailItemCollection()
		{

		}
		
		public static implicit operator List<BludDetailItem>(BludDetailItemCollection coll)
		{
			List<BludDetailItem> list = new List<BludDetailItem>();
			
			foreach (BludDetailItem emp in coll)
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
				return  BludDetailItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BludDetailItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BludDetailItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BludDetailItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public BludDetailItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BludDetailItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(BludDetailItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public BludDetailItem AddNew()
		{
			BludDetailItem entity = base.AddNewEntity() as BludDetailItem;
			
			return entity;
		}

		public BludDetailItem FindByPrimaryKey(System.Int32 bludDetailID, System.Int32 bludDetailItemID)
		{
			return base.FindByPrimaryKey(bludDetailID, bludDetailItemID) as BludDetailItem;
		}


		#region IEnumerable<BludDetailItem> Members

		IEnumerator<BludDetailItem> IEnumerable<BludDetailItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BludDetailItem;
			}
		}

		#endregion
		
		private BludDetailItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BludDetailItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("BludDetailItem ({BludDetailItemID},{BludDetailID})")]
	[Serializable]
	public partial class BludDetailItem : esBludDetailItem
	{
		public BludDetailItem()
		{

		}
	
		public BludDetailItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BludDetailItemMetadata.Meta();
			}
		}
		
		
		
		override protected esBludDetailItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BludDetailItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public BludDetailItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BludDetailItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(BludDetailItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private BludDetailItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class BludDetailItemQuery : esBludDetailItemQuery
	{
		public BludDetailItemQuery()
		{

		}		
		
		public BludDetailItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "BludDetailItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class BludDetailItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BludDetailItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BludDetailItemMetadata.ColumnNames.BludDetailItemID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BludDetailItemMetadata.PropertyNames.BludDetailItemID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludDetailItemMetadata.ColumnNames.BludDetailID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BludDetailItemMetadata.PropertyNames.BludDetailID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludDetailItemMetadata.ColumnNames.ReferenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = BludDetailItemMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludDetailItemMetadata.ColumnNames.Nominal, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BludDetailItemMetadata.PropertyNames.Nominal;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludDetailItemMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BludDetailItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludDetailItemMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = BludDetailItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public BludDetailItemMetadata Meta()
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
			 public const string BludDetailItemID = "BludDetailItemID";
			 public const string BludDetailID = "BludDetailID";
			 public const string ReferenceNo = "ReferenceNo";
			 public const string Nominal = "Nominal";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string BludDetailItemID = "BludDetailItemID";
			 public const string BludDetailID = "BludDetailID";
			 public const string ReferenceNo = "ReferenceNo";
			 public const string Nominal = "Nominal";
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
			lock (typeof(BludDetailItemMetadata))
			{
				if(BludDetailItemMetadata.mapDelegates == null)
				{
					BludDetailItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BludDetailItemMetadata.meta == null)
				{
					BludDetailItemMetadata.meta = new BludDetailItemMetadata();
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
				

				meta.AddTypeMap("BludDetailItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("BludDetailID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Nominal", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "BludDetailItem";
				meta.Destination = "BludDetailItem";
				
				meta.spInsert = "proc_BludDetailItemInsert";				
				meta.spUpdate = "proc_BludDetailItemUpdate";		
				meta.spDelete = "proc_BludDetailItemDelete";
				meta.spLoadAll = "proc_BludDetailItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_BludDetailItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BludDetailItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
