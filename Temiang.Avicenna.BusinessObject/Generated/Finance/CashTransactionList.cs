/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/30/2012 11:42:38 AM
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
	abstract public class esCashTransactionListCollection : esEntityCollectionWAuditLog
	{
		public esCashTransactionListCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "CashTransactionListCollection";
		}

		#region Query Logic
		protected void InitQuery(esCashTransactionListQuery query)
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
			this.InitQuery(query as esCashTransactionListQuery);
		}
		#endregion
		
		virtual public CashTransactionList DetachEntity(CashTransactionList entity)
		{
			return base.DetachEntity(entity) as CashTransactionList;
		}
		
		virtual public CashTransactionList AttachEntity(CashTransactionList entity)
		{
			return base.AttachEntity(entity) as CashTransactionList;
		}
		
		virtual public void Combine(CashTransactionListCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CashTransactionList this[int index]
		{
			get
			{
				return base[index] as CashTransactionList;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CashTransactionList);
		}
	}



	[Serializable]
	abstract public class esCashTransactionList : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCashTransactionListQuery GetDynamicQuery()
		{
			return null;
		}

		public esCashTransactionList()
		{

		}

		public esCashTransactionList(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String listId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(listId);
			else
				return LoadByPrimaryKeyStoredProcedure(listId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String listId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(listId);
			else
				return LoadByPrimaryKeyStoredProcedure(listId);
		}

		private bool LoadByPrimaryKeyDynamic(System.String listId)
		{
			esCashTransactionListQuery query = this.GetDynamicQuery();
			query.Where(query.ListId == listId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String listId)
		{
			esParameters parms = new esParameters();
			parms.Add("ListId",listId);
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
						case "ListId": this.str.ListId = (string)value; break;							
						case "Description": this.str.Description = (string)value; break;							
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;							
						case "SubledgerId": this.str.SubledgerId = (string)value; break;							
						case "CashType": this.str.CashType = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserId": this.str.LastUpdateByUserId = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ChartOfAccountId":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
							break;
						
						case "SubledgerId":
						
							if (value == null || value is System.Int32)
								this.SubledgerId = (System.Int32?)value;
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
		/// Maps to CashTransactionList.ListId
		/// </summary>
		virtual public System.String ListId
		{
			get
			{
				return base.GetSystemString(CashTransactionListMetadata.ColumnNames.ListId);
			}
			
			set
			{
				base.SetSystemString(CashTransactionListMetadata.ColumnNames.ListId, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionList.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(CashTransactionListMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(CashTransactionListMetadata.ColumnNames.Description, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionList.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionListMetadata.ColumnNames.ChartOfAccountId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionListMetadata.ColumnNames.ChartOfAccountId, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionList.SubledgerId
		/// </summary>
		virtual public System.Int32? SubledgerId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionListMetadata.ColumnNames.SubledgerId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionListMetadata.ColumnNames.SubledgerId, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionList.CashType
		/// </summary>
		virtual public System.String CashType
		{
			get
			{
				return base.GetSystemString(CashTransactionListMetadata.ColumnNames.CashType);
			}
			
			set
			{
				base.SetSystemString(CashTransactionListMetadata.ColumnNames.CashType, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionList.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CashTransactionListMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CashTransactionListMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionList.LastUpdateByUserId
		/// </summary>
		virtual public System.String LastUpdateByUserId
		{
			get
			{
				return base.GetSystemString(CashTransactionListMetadata.ColumnNames.LastUpdateByUserId);
			}
			
			set
			{
				base.SetSystemString(CashTransactionListMetadata.ColumnNames.LastUpdateByUserId, value);
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
			public esStrings(esCashTransactionList entity)
			{
				this.entity = entity;
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
				
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
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
				
			public System.String CashType
			{
				get
				{
					System.String data = entity.CashType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CashType = null;
					else entity.CashType = Convert.ToString(value);
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
			

			private esCashTransactionList entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCashTransactionListQuery query)
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
				throw new Exception("esCashTransactionList can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class CashTransactionList : esCashTransactionList
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
	abstract public class esCashTransactionListQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CashTransactionListMetadata.Meta();
			}
		}	
		

		public esQueryItem ListId
		{
			get
			{
				return new esQueryItem(this, CashTransactionListMetadata.ColumnNames.ListId, esSystemType.String);
			}
		} 
		
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, CashTransactionListMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
		
		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, CashTransactionListMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubledgerId
		{
			get
			{
				return new esQueryItem(this, CashTransactionListMetadata.ColumnNames.SubledgerId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem CashType
		{
			get
			{
				return new esQueryItem(this, CashTransactionListMetadata.ColumnNames.CashType, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CashTransactionListMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserId
		{
			get
			{
				return new esQueryItem(this, CashTransactionListMetadata.ColumnNames.LastUpdateByUserId, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CashTransactionListCollection")]
	public partial class CashTransactionListCollection : esCashTransactionListCollection, IEnumerable<CashTransactionList>
	{
		public CashTransactionListCollection()
		{

		}
		
		public static implicit operator List<CashTransactionList>(CashTransactionListCollection coll)
		{
			List<CashTransactionList> list = new List<CashTransactionList>();
			
			foreach (CashTransactionList emp in coll)
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
				return  CashTransactionListMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CashTransactionListQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CashTransactionList(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CashTransactionList();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public CashTransactionListQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CashTransactionListQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(CashTransactionListQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public CashTransactionList AddNew()
		{
			CashTransactionList entity = base.AddNewEntity() as CashTransactionList;
			
			return entity;
		}

		public CashTransactionList FindByPrimaryKey(System.String listId)
		{
			return base.FindByPrimaryKey(listId) as CashTransactionList;
		}


		#region IEnumerable<CashTransactionList> Members

		IEnumerator<CashTransactionList> IEnumerable<CashTransactionList>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CashTransactionList;
			}
		}

		#endregion
		
		private CashTransactionListQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CashTransactionList' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("CashTransactionList ({ListId})")]
	[Serializable]
	public partial class CashTransactionList : esCashTransactionList
	{
		public CashTransactionList()
		{

		}
	
		public CashTransactionList(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CashTransactionListMetadata.Meta();
			}
		}
		
		
		
		override protected esCashTransactionListQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CashTransactionListQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public CashTransactionListQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CashTransactionListQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(CashTransactionListQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private CashTransactionListQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class CashTransactionListQuery : esCashTransactionListQuery
	{
		public CashTransactionListQuery()
		{

		}		
		
		public CashTransactionListQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "CashTransactionListQuery";
        }
		
			
	}


	[Serializable]
	public partial class CashTransactionListMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CashTransactionListMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CashTransactionListMetadata.ColumnNames.ListId, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionListMetadata.PropertyNames.ListId;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionListMetadata.ColumnNames.Description, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionListMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionListMetadata.ColumnNames.ChartOfAccountId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionListMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionListMetadata.ColumnNames.SubledgerId, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionListMetadata.PropertyNames.SubledgerId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionListMetadata.ColumnNames.CashType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionListMetadata.PropertyNames.CashType;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionListMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CashTransactionListMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionListMetadata.ColumnNames.LastUpdateByUserId, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionListMetadata.PropertyNames.LastUpdateByUserId;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public CashTransactionListMetadata Meta()
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
			 public const string ListId = "ListId";
			 public const string Description = "Description";
			 public const string ChartOfAccountId = "ChartOfAccountId";
			 public const string SubledgerId = "SubledgerId";
			 public const string CashType = "CashType";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserId = "LastUpdateByUserId";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ListId = "ListId";
			 public const string Description = "Description";
			 public const string ChartOfAccountId = "ChartOfAccountId";
			 public const string SubledgerId = "SubledgerId";
			 public const string CashType = "CashType";
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
			lock (typeof(CashTransactionListMetadata))
			{
				if(CashTransactionListMetadata.mapDelegates == null)
				{
					CashTransactionListMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CashTransactionListMetadata.meta == null)
				{
					CashTransactionListMetadata.meta = new CashTransactionListMetadata();
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
				

				meta.AddTypeMap("ListId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CashType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserId", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "CashTransactionList";
				meta.Destination = "CashTransactionList";
				
				meta.spInsert = "proc_CashTransactionListInsert";				
				meta.spUpdate = "proc_CashTransactionListUpdate";		
				meta.spDelete = "proc_CashTransactionListDelete";
				meta.spLoadAll = "proc_CashTransactionListLoadAll";
				meta.spLoadByPrimaryKey = "proc_CashTransactionListLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CashTransactionListMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
