/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 10/7/2014 8:28:55 AM
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
	abstract public class esDataRptItemCollection : esEntityCollection
	{
		public esDataRptItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "DataRptItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esDataRptItemQuery query)
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
			this.InitQuery(query as esDataRptItemQuery);
		}
		#endregion
		
		virtual public DataRptItem DetachEntity(DataRptItem entity)
		{
			return base.DetachEntity(entity) as DataRptItem;
		}
		
		virtual public DataRptItem AttachEntity(DataRptItem entity)
		{
			return base.AttachEntity(entity) as DataRptItem;
		}
		
		virtual public void Combine(DataRptItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public DataRptItem this[int index]
		{
			get
			{
				return base[index] as DataRptItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(DataRptItem);
		}
	}



	[Serializable]
	abstract public class esDataRptItem : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDataRptItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esDataRptItem()
		{

		}

		public esDataRptItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String sRDataRpt, System.String itemID, System.DateTime transactionDate)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRDataRpt, itemID, transactionDate);
			else
				return LoadByPrimaryKeyStoredProcedure(sRDataRpt, itemID, transactionDate);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String sRDataRpt, System.String itemID, System.DateTime transactionDate)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRDataRpt, itemID, transactionDate);
			else
				return LoadByPrimaryKeyStoredProcedure(sRDataRpt, itemID, transactionDate);
		}

		private bool LoadByPrimaryKeyDynamic(System.String sRDataRpt, System.String itemID, System.DateTime transactionDate)
		{
			esDataRptItemQuery query = this.GetDynamicQuery();
			query.Where(query.SRDataRpt == sRDataRpt, query.ItemID == itemID, query.TransactionDate == transactionDate);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String sRDataRpt, System.String itemID, System.DateTime transactionDate)
		{
			esParameters parms = new esParameters();
			parms.Add("SRDataRpt",sRDataRpt);			parms.Add("ItemID",itemID);			parms.Add("TransactionDate",transactionDate);
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
						case "SRDataRpt": this.str.SRDataRpt = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "TransactionDate": this.str.TransactionDate = (string)value; break;							
						case "Qty": this.str.Qty = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "TransactionDate":
						
							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
							break;
						
						case "Qty":
						
							if (value == null || value is System.Int16)
								this.Qty = (System.Int16?)value;
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
		/// Maps to DataRptItem.SRDataRpt
		/// </summary>
		virtual public System.String SRDataRpt
		{
			get
			{
				return base.GetSystemString(DataRptItemMetadata.ColumnNames.SRDataRpt);
			}
			
			set
			{
				base.SetSystemString(DataRptItemMetadata.ColumnNames.SRDataRpt, value);
			}
		}
		
		/// <summary>
		/// Maps to DataRptItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(DataRptItemMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(DataRptItemMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to DataRptItem.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(DataRptItemMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(DataRptItemMetadata.ColumnNames.TransactionDate, value);
			}
		}
		
		/// <summary>
		/// Maps to DataRptItem.Qty
		/// </summary>
		virtual public System.Int16? Qty
		{
			get
			{
				return base.GetSystemInt16(DataRptItemMetadata.ColumnNames.Qty);
			}
			
			set
			{
				base.SetSystemInt16(DataRptItemMetadata.ColumnNames.Qty, value);
			}
		}
		
		/// <summary>
		/// Maps to DataRptItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(DataRptItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(DataRptItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to DataRptItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(DataRptItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(DataRptItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esDataRptItem entity)
			{
				this.entity = entity;
			}
			
	
			public System.String SRDataRpt
			{
				get
				{
					System.String data = entity.SRDataRpt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDataRpt = null;
					else entity.SRDataRpt = Convert.ToString(value);
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
				
			public System.String TransactionDate
			{
				get
				{
					System.DateTime? data = entity.TransactionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionDate = null;
					else entity.TransactionDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String Qty
			{
				get
				{
					System.Int16? data = entity.Qty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Qty = null;
					else entity.Qty = Convert.ToInt16(value);
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
			

			private esDataRptItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDataRptItemQuery query)
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
				throw new Exception("esDataRptItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esDataRptItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return DataRptItemMetadata.Meta();
			}
		}	
		

		public esQueryItem SRDataRpt
		{
			get
			{
				return new esQueryItem(this, DataRptItemMetadata.ColumnNames.SRDataRpt, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, DataRptItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, DataRptItemMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, DataRptItemMetadata.ColumnNames.Qty, esSystemType.Int16);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, DataRptItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, DataRptItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DataRptItemCollection")]
	public partial class DataRptItemCollection : esDataRptItemCollection, IEnumerable<DataRptItem>
	{
		public DataRptItemCollection()
		{

		}
		
		public static implicit operator List<DataRptItem>(DataRptItemCollection coll)
		{
			List<DataRptItem> list = new List<DataRptItem>();
			
			foreach (DataRptItem emp in coll)
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
				return  DataRptItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DataRptItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new DataRptItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new DataRptItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public DataRptItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DataRptItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(DataRptItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public DataRptItem AddNew()
		{
			DataRptItem entity = base.AddNewEntity() as DataRptItem;
			
			return entity;
		}

		public DataRptItem FindByPrimaryKey(System.String sRDataRpt, System.String itemID, System.DateTime transactionDate)
		{
			return base.FindByPrimaryKey(sRDataRpt, itemID, transactionDate) as DataRptItem;
		}


		#region IEnumerable<DataRptItem> Members

		IEnumerator<DataRptItem> IEnumerable<DataRptItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as DataRptItem;
			}
		}

		#endregion
		
		private DataRptItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'DataRptItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("DataRptItem ({SRDataRpt},{ItemID},{TransactionDate})")]
	[Serializable]
	public partial class DataRptItem : esDataRptItem
	{
		public DataRptItem()
		{

		}
	
		public DataRptItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DataRptItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDataRptItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DataRptItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public DataRptItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DataRptItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(DataRptItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private DataRptItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class DataRptItemQuery : esDataRptItemQuery
	{
		public DataRptItemQuery()
		{

		}		
		
		public DataRptItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "DataRptItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class DataRptItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DataRptItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DataRptItemMetadata.ColumnNames.SRDataRpt, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = DataRptItemMetadata.PropertyNames.SRDataRpt;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(DataRptItemMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = DataRptItemMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(DataRptItemMetadata.ColumnNames.TransactionDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DataRptItemMetadata.PropertyNames.TransactionDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DataRptItemMetadata.ColumnNames.Qty, 3, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = DataRptItemMetadata.PropertyNames.Qty;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DataRptItemMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DataRptItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DataRptItemMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = DataRptItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public DataRptItemMetadata Meta()
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
			 public const string SRDataRpt = "SRDataRpt";
			 public const string ItemID = "ItemID";
			 public const string TransactionDate = "TransactionDate";
			 public const string Qty = "Qty";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SRDataRpt = "SRDataRpt";
			 public const string ItemID = "ItemID";
			 public const string TransactionDate = "TransactionDate";
			 public const string Qty = "Qty";
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
			lock (typeof(DataRptItemMetadata))
			{
				if(DataRptItemMetadata.mapDelegates == null)
				{
					DataRptItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (DataRptItemMetadata.meta == null)
				{
					DataRptItemMetadata.meta = new DataRptItemMetadata();
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
				

				meta.AddTypeMap("SRDataRpt", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("Qty", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "DataRptItem";
				meta.Destination = "DataRptItem";
				
				meta.spInsert = "proc_DataRptItemInsert";				
				meta.spUpdate = "proc_DataRptItemUpdate";		
				meta.spDelete = "proc_DataRptItemDelete";
				meta.spLoadAll = "proc_DataRptItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_DataRptItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DataRptItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
