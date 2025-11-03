/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/14/2012 5:32:16 PM
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
	abstract public class esCompoundTemplateItemCollection : esEntityCollectionWAuditLog
	{
		public esCompoundTemplateItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "CompoundTemplateItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esCompoundTemplateItemQuery query)
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
			this.InitQuery(query as esCompoundTemplateItemQuery);
		}
		#endregion
		
		virtual public CompoundTemplateItem DetachEntity(CompoundTemplateItem entity)
		{
			return base.DetachEntity(entity) as CompoundTemplateItem;
		}
		
		virtual public CompoundTemplateItem AttachEntity(CompoundTemplateItem entity)
		{
			return base.AttachEntity(entity) as CompoundTemplateItem;
		}
		
		virtual public void Combine(CompoundTemplateItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CompoundTemplateItem this[int index]
		{
			get
			{
				return base[index] as CompoundTemplateItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CompoundTemplateItem);
		}
	}



	[Serializable]
	abstract public class esCompoundTemplateItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCompoundTemplateItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esCompoundTemplateItem()
		{

		}

		public esCompoundTemplateItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String compoundTemplateID, System.String detailItemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(compoundTemplateID, detailItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(compoundTemplateID, detailItemID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String compoundTemplateID, System.String detailItemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(compoundTemplateID, detailItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(compoundTemplateID, detailItemID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String compoundTemplateID, System.String detailItemID)
		{
			esCompoundTemplateItemQuery query = this.GetDynamicQuery();
			query.Where(query.CompoundTemplateID == compoundTemplateID, query.DetailItemID == detailItemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String compoundTemplateID, System.String detailItemID)
		{
			esParameters parms = new esParameters();
			parms.Add("CompoundTemplateID",compoundTemplateID);			parms.Add("DetailItemID",detailItemID);
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
						case "CompoundTemplateID": this.str.CompoundTemplateID = (string)value; break;							
						case "DetailItemID": this.str.DetailItemID = (string)value; break;							
						case "Qty": this.str.Qty = (string)value; break;							
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Qty":
						
							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
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
		/// Maps to CompoundTemplateItem.CompoundTemplateID
		/// </summary>
		virtual public System.String CompoundTemplateID
		{
			get
			{
				return base.GetSystemString(CompoundTemplateItemMetadata.ColumnNames.CompoundTemplateID);
			}
			
			set
			{
				base.SetSystemString(CompoundTemplateItemMetadata.ColumnNames.CompoundTemplateID, value);
			}
		}
		
		/// <summary>
		/// Maps to CompoundTemplateItem.DetailItemID
		/// </summary>
		virtual public System.String DetailItemID
		{
			get
			{
				return base.GetSystemString(CompoundTemplateItemMetadata.ColumnNames.DetailItemID);
			}
			
			set
			{
				base.SetSystemString(CompoundTemplateItemMetadata.ColumnNames.DetailItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to CompoundTemplateItem.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(CompoundTemplateItemMetadata.ColumnNames.Qty);
			}
			
			set
			{
				base.SetSystemDecimal(CompoundTemplateItemMetadata.ColumnNames.Qty, value);
			}
		}
		
		/// <summary>
		/// Maps to CompoundTemplateItem.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(CompoundTemplateItemMetadata.ColumnNames.SRItemUnit);
			}
			
			set
			{
				base.SetSystemString(CompoundTemplateItemMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		
		/// <summary>
		/// Maps to CompoundTemplateItem.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(CompoundTemplateItemMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(CompoundTemplateItemMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to CompoundTemplateItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CompoundTemplateItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CompoundTemplateItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to CompoundTemplateItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CompoundTemplateItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(CompoundTemplateItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCompoundTemplateItem entity)
			{
				this.entity = entity;
			}
			
	
			public System.String CompoundTemplateID
			{
				get
				{
					System.String data = entity.CompoundTemplateID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompoundTemplateID = null;
					else entity.CompoundTemplateID = Convert.ToString(value);
				}
			}
				
			public System.String DetailItemID
			{
				get
				{
					System.String data = entity.DetailItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DetailItemID = null;
					else entity.DetailItemID = Convert.ToString(value);
				}
			}
				
			public System.String Qty
			{
				get
				{
					System.Decimal? data = entity.Qty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Qty = null;
					else entity.Qty = Convert.ToDecimal(value);
				}
			}
				
			public System.String SRItemUnit
			{
				get
				{
					System.String data = entity.SRItemUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemUnit = null;
					else entity.SRItemUnit = Convert.ToString(value);
				}
			}
				
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
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
			

			private esCompoundTemplateItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCompoundTemplateItemQuery query)
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
				throw new Exception("esCompoundTemplateItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class CompoundTemplateItem : esCompoundTemplateItem
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
	abstract public class esCompoundTemplateItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CompoundTemplateItemMetadata.Meta();
			}
		}	
		

		public esQueryItem CompoundTemplateID
		{
			get
			{
				return new esQueryItem(this, CompoundTemplateItemMetadata.ColumnNames.CompoundTemplateID, esSystemType.String);
			}
		} 
		
		public esQueryItem DetailItemID
		{
			get
			{
				return new esQueryItem(this, CompoundTemplateItemMetadata.ColumnNames.DetailItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, CompoundTemplateItemMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, CompoundTemplateItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, CompoundTemplateItemMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CompoundTemplateItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CompoundTemplateItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CompoundTemplateItemCollection")]
	public partial class CompoundTemplateItemCollection : esCompoundTemplateItemCollection, IEnumerable<CompoundTemplateItem>
	{
		public CompoundTemplateItemCollection()
		{

		}
		
		public static implicit operator List<CompoundTemplateItem>(CompoundTemplateItemCollection coll)
		{
			List<CompoundTemplateItem> list = new List<CompoundTemplateItem>();
			
			foreach (CompoundTemplateItem emp in coll)
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
				return  CompoundTemplateItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CompoundTemplateItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CompoundTemplateItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CompoundTemplateItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public CompoundTemplateItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CompoundTemplateItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(CompoundTemplateItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public CompoundTemplateItem AddNew()
		{
			CompoundTemplateItem entity = base.AddNewEntity() as CompoundTemplateItem;
			
			return entity;
		}

		public CompoundTemplateItem FindByPrimaryKey(System.String compoundTemplateID, System.String detailItemID)
		{
			return base.FindByPrimaryKey(compoundTemplateID, detailItemID) as CompoundTemplateItem;
		}


		#region IEnumerable<CompoundTemplateItem> Members

		IEnumerator<CompoundTemplateItem> IEnumerable<CompoundTemplateItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CompoundTemplateItem;
			}
		}

		#endregion
		
		private CompoundTemplateItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CompoundTemplateItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("CompoundTemplateItem ({CompoundTemplateID},{DetailItemID})")]
	[Serializable]
	public partial class CompoundTemplateItem : esCompoundTemplateItem
	{
		public CompoundTemplateItem()
		{

		}
	
		public CompoundTemplateItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CompoundTemplateItemMetadata.Meta();
			}
		}
		
		
		
		override protected esCompoundTemplateItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CompoundTemplateItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public CompoundTemplateItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CompoundTemplateItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(CompoundTemplateItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private CompoundTemplateItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class CompoundTemplateItemQuery : esCompoundTemplateItemQuery
	{
		public CompoundTemplateItemQuery()
		{

		}		
		
		public CompoundTemplateItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "CompoundTemplateItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class CompoundTemplateItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CompoundTemplateItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CompoundTemplateItemMetadata.ColumnNames.CompoundTemplateID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CompoundTemplateItemMetadata.PropertyNames.CompoundTemplateID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompoundTemplateItemMetadata.ColumnNames.DetailItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CompoundTemplateItemMetadata.PropertyNames.DetailItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompoundTemplateItemMetadata.ColumnNames.Qty, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CompoundTemplateItemMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompoundTemplateItemMetadata.ColumnNames.SRItemUnit, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CompoundTemplateItemMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompoundTemplateItemMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CompoundTemplateItemMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompoundTemplateItemMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CompoundTemplateItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompoundTemplateItemMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CompoundTemplateItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public CompoundTemplateItemMetadata Meta()
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
			 public const string CompoundTemplateID = "CompoundTemplateID";
			 public const string DetailItemID = "DetailItemID";
			 public const string Qty = "Qty";
			 public const string SRItemUnit = "SRItemUnit";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string CompoundTemplateID = "CompoundTemplateID";
			 public const string DetailItemID = "DetailItemID";
			 public const string Qty = "Qty";
			 public const string SRItemUnit = "SRItemUnit";
			 public const string Notes = "Notes";
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
			lock (typeof(CompoundTemplateItemMetadata))
			{
				if(CompoundTemplateItemMetadata.mapDelegates == null)
				{
					CompoundTemplateItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CompoundTemplateItemMetadata.meta == null)
				{
					CompoundTemplateItemMetadata.meta = new CompoundTemplateItemMetadata();
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
				

				meta.AddTypeMap("CompoundTemplateID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DetailItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "CompoundTemplateItem";
				meta.Destination = "CompoundTemplateItem";
				
				meta.spInsert = "proc_CompoundTemplateItemInsert";				
				meta.spUpdate = "proc_CompoundTemplateItemUpdate";		
				meta.spDelete = "proc_CompoundTemplateItemDelete";
				meta.spLoadAll = "proc_CompoundTemplateItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_CompoundTemplateItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CompoundTemplateItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
