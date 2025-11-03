/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 4/14/2017 11:39:15 PM
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
	abstract public class esCashTransactionListItemCollection : esEntityCollectionWAuditLog
	{
		public esCashTransactionListItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "CashTransactionListItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esCashTransactionListItemQuery query)
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
			this.InitQuery(query as esCashTransactionListItemQuery);
		}
		#endregion
		
		virtual public CashTransactionListItem DetachEntity(CashTransactionListItem entity)
		{
			return base.DetachEntity(entity) as CashTransactionListItem;
		}
		
		virtual public CashTransactionListItem AttachEntity(CashTransactionListItem entity)
		{
			return base.AttachEntity(entity) as CashTransactionListItem;
		}
		
		virtual public void Combine(CashTransactionListItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CashTransactionListItem this[int index]
		{
			get
			{
				return base[index] as CashTransactionListItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CashTransactionListItem);
		}
	}



	[Serializable]
	abstract public class esCashTransactionListItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCashTransactionListItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esCashTransactionListItem()
		{

		}

		public esCashTransactionListItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 listItemId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(listItemId);
			else
				return LoadByPrimaryKeyStoredProcedure(listItemId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 listItemId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(listItemId);
			else
				return LoadByPrimaryKeyStoredProcedure(listItemId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 listItemId)
		{
			esCashTransactionListItemQuery query = this.GetDynamicQuery();
			query.Where(query.ListItemId == listItemId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 listItemId)
		{
			esParameters parms = new esParameters();
			parms.Add("ListItemId",listItemId);
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
						case "ListItemId": this.str.ListItemId = (string)value; break;							
						case "ListId": this.str.ListId = (string)value; break;							
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;							
						case "SubledgerId": this.str.SubledgerId = (string)value; break;							
						case "Amount": this.str.Amount = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserId": this.str.LastUpdateByUserId = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ListItemId":
						
							if (value == null || value is System.Int32)
								this.ListItemId = (System.Int32?)value;
							break;
						
						case "ChartOfAccountId":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
							break;
						
						case "SubledgerId":
						
							if (value == null || value is System.Int32)
								this.SubledgerId = (System.Int32?)value;
							break;
						
						case "Amount":
						
							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
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
		/// Maps to CashTransactionListItem.ListItemId
		/// </summary>
		virtual public System.Int32? ListItemId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionListItemMetadata.ColumnNames.ListItemId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionListItemMetadata.ColumnNames.ListItemId, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionListItem.ListId
		/// </summary>
		virtual public System.String ListId
		{
			get
			{
				return base.GetSystemString(CashTransactionListItemMetadata.ColumnNames.ListId);
			}
			
			set
			{
				base.SetSystemString(CashTransactionListItemMetadata.ColumnNames.ListId, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionListItem.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionListItemMetadata.ColumnNames.ChartOfAccountId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionListItemMetadata.ColumnNames.ChartOfAccountId, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionListItem.SubledgerId
		/// </summary>
		virtual public System.Int32? SubledgerId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionListItemMetadata.ColumnNames.SubledgerId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionListItemMetadata.ColumnNames.SubledgerId, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionListItem.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(CashTransactionListItemMetadata.ColumnNames.Amount);
			}
			
			set
			{
				base.SetSystemDecimal(CashTransactionListItemMetadata.ColumnNames.Amount, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionListItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CashTransactionListItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CashTransactionListItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionListItem.LastUpdateByUserId
		/// </summary>
		virtual public System.String LastUpdateByUserId
		{
			get
			{
				return base.GetSystemString(CashTransactionListItemMetadata.ColumnNames.LastUpdateByUserId);
			}
			
			set
			{
				base.SetSystemString(CashTransactionListItemMetadata.ColumnNames.LastUpdateByUserId, value);
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
			public esStrings(esCashTransactionListItem entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ListItemId
			{
				get
				{
					System.Int32? data = entity.ListItemId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ListItemId = null;
					else entity.ListItemId = Convert.ToInt32(value);
				}
			}
				
			public System.String ListId
			{
				get
				{
					System.String data = entity.ListId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ListId = null;
					else entity.ListId = Convert.ToString(value);
				}
			}
				
			public System.String ChartOfAccountId
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountId = null;
					else entity.ChartOfAccountId = Convert.ToInt32(value);
				}
			}
				
			public System.String SubledgerId
			{
				get
				{
					System.Int32? data = entity.SubledgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerId = null;
					else entity.SubledgerId = Convert.ToInt32(value);
				}
			}
				
			public System.String Amount
			{
				get
				{
					System.Decimal? data = entity.Amount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Amount = null;
					else entity.Amount = Convert.ToDecimal(value);
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
				
			public System.String LastUpdateByUserId
			{
				get
				{
					System.String data = entity.LastUpdateByUserId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdateByUserId = null;
					else entity.LastUpdateByUserId = Convert.ToString(value);
				}
			}
			

			private esCashTransactionListItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCashTransactionListItemQuery query)
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
				throw new Exception("esCashTransactionListItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class CashTransactionListItem : esCashTransactionListItem
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
	abstract public class esCashTransactionListItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CashTransactionListItemMetadata.Meta();
			}
		}	
		

		public esQueryItem ListItemId
		{
			get
			{
				return new esQueryItem(this, CashTransactionListItemMetadata.ColumnNames.ListItemId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ListId
		{
			get
			{
				return new esQueryItem(this, CashTransactionListItemMetadata.ColumnNames.ListId, esSystemType.String);
			}
		} 
		
		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, CashTransactionListItemMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubledgerId
		{
			get
			{
				return new esQueryItem(this, CashTransactionListItemMetadata.ColumnNames.SubledgerId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, CashTransactionListItemMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CashTransactionListItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserId
		{
			get
			{
				return new esQueryItem(this, CashTransactionListItemMetadata.ColumnNames.LastUpdateByUserId, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CashTransactionListItemCollection")]
	public partial class CashTransactionListItemCollection : esCashTransactionListItemCollection, IEnumerable<CashTransactionListItem>
	{
		public CashTransactionListItemCollection()
		{

		}
		
		public static implicit operator List<CashTransactionListItem>(CashTransactionListItemCollection coll)
		{
			List<CashTransactionListItem> list = new List<CashTransactionListItem>();
			
			foreach (CashTransactionListItem emp in coll)
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
				return  CashTransactionListItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CashTransactionListItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CashTransactionListItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CashTransactionListItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public CashTransactionListItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CashTransactionListItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(CashTransactionListItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public CashTransactionListItem AddNew()
		{
			CashTransactionListItem entity = base.AddNewEntity() as CashTransactionListItem;
			
			return entity;
		}

		public CashTransactionListItem FindByPrimaryKey(System.Int32 listItemId)
		{
			return base.FindByPrimaryKey(listItemId) as CashTransactionListItem;
		}


		#region IEnumerable<CashTransactionListItem> Members

		IEnumerator<CashTransactionListItem> IEnumerable<CashTransactionListItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CashTransactionListItem;
			}
		}

		#endregion
		
		private CashTransactionListItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CashTransactionListItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("CashTransactionListItem ({ListItemId})")]
	[Serializable]
	public partial class CashTransactionListItem : esCashTransactionListItem
	{
		public CashTransactionListItem()
		{

		}
	
		public CashTransactionListItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CashTransactionListItemMetadata.Meta();
			}
		}
		
		
		
		override protected esCashTransactionListItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CashTransactionListItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public CashTransactionListItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CashTransactionListItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(CashTransactionListItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private CashTransactionListItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class CashTransactionListItemQuery : esCashTransactionListItemQuery
	{
		public CashTransactionListItemQuery()
		{

		}		
		
		public CashTransactionListItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "CashTransactionListItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class CashTransactionListItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CashTransactionListItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CashTransactionListItemMetadata.ColumnNames.ListItemId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionListItemMetadata.PropertyNames.ListItemId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionListItemMetadata.ColumnNames.ListId, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionListItemMetadata.PropertyNames.ListId;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionListItemMetadata.ColumnNames.ChartOfAccountId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionListItemMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionListItemMetadata.ColumnNames.SubledgerId, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionListItemMetadata.PropertyNames.SubledgerId;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionListItemMetadata.ColumnNames.Amount, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CashTransactionListItemMetadata.PropertyNames.Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionListItemMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CashTransactionListItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionListItemMetadata.ColumnNames.LastUpdateByUserId, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionListItemMetadata.PropertyNames.LastUpdateByUserId;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public CashTransactionListItemMetadata Meta()
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
			 public const string ListItemId = "ListItemId";
			 public const string ListId = "ListId";
			 public const string ChartOfAccountId = "ChartOfAccountId";
			 public const string SubledgerId = "SubledgerId";
			 public const string Amount = "Amount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserId = "LastUpdateByUserId";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ListItemId = "ListItemId";
			 public const string ListId = "ListId";
			 public const string ChartOfAccountId = "ChartOfAccountId";
			 public const string SubledgerId = "SubledgerId";
			 public const string Amount = "Amount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserId = "LastUpdateByUserId";
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
			lock (typeof(CashTransactionListItemMetadata))
			{
				if(CashTransactionListItemMetadata.mapDelegates == null)
				{
					CashTransactionListItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CashTransactionListItemMetadata.meta == null)
				{
					CashTransactionListItemMetadata.meta = new CashTransactionListItemMetadata();
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
				

				meta.AddTypeMap("ListItemId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ListId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Amount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserId", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "CashTransactionListItem";
				meta.Destination = "CashTransactionListItem";
				
				meta.spInsert = "proc_CashTransactionListItemInsert";				
				meta.spUpdate = "proc_CashTransactionListItemUpdate";		
				meta.spDelete = "proc_CashTransactionListItemDelete";
				meta.spLoadAll = "proc_CashTransactionListItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_CashTransactionListItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CashTransactionListItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
