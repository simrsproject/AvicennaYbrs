/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/13/2012 9:44:03 AM
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
	abstract public class esGuarantorSurgicalPackageCoveredItemCollection : esEntityCollectionWAuditLog
	{
		public esGuarantorSurgicalPackageCoveredItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "GuarantorSurgicalPackageCoveredItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esGuarantorSurgicalPackageCoveredItemQuery query)
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
			this.InitQuery(query as esGuarantorSurgicalPackageCoveredItemQuery);
		}
		#endregion
		
		virtual public GuarantorSurgicalPackageCoveredItem DetachEntity(GuarantorSurgicalPackageCoveredItem entity)
		{
			return base.DetachEntity(entity) as GuarantorSurgicalPackageCoveredItem;
		}
		
		virtual public GuarantorSurgicalPackageCoveredItem AttachEntity(GuarantorSurgicalPackageCoveredItem entity)
		{
			return base.AttachEntity(entity) as GuarantorSurgicalPackageCoveredItem;
		}
		
		virtual public void Combine(GuarantorSurgicalPackageCoveredItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public GuarantorSurgicalPackageCoveredItem this[int index]
		{
			get
			{
				return base[index] as GuarantorSurgicalPackageCoveredItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(GuarantorSurgicalPackageCoveredItem);
		}
	}



	[Serializable]
	abstract public class esGuarantorSurgicalPackageCoveredItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esGuarantorSurgicalPackageCoveredItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esGuarantorSurgicalPackageCoveredItem()
		{

		}

		public esGuarantorSurgicalPackageCoveredItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String guarantorID, System.String packageID, System.String itemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, packageID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, packageID, itemID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String guarantorID, System.String packageID, System.String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, packageID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, packageID, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String guarantorID, System.String packageID, System.String itemID)
		{
			esGuarantorSurgicalPackageCoveredItemQuery query = this.GetDynamicQuery();
			query.Where(query.GuarantorID == guarantorID, query.PackageID == packageID, query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String guarantorID, System.String packageID, System.String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("GuarantorID",guarantorID);			parms.Add("PackageID",packageID);			parms.Add("ItemID",itemID);
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
						case "GuarantorID": this.str.GuarantorID = (string)value; break;							
						case "PackageID": this.str.PackageID = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "CoveredAmount": this.str.CoveredAmount = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CoveredAmount":
						
							if (value == null || value is System.Decimal)
								this.CoveredAmount = (System.Decimal?)value;
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
		/// Maps to GuarantorSurgicalPackageCoveredItem.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.GuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorSurgicalPackageCoveredItem.PackageID
		/// </summary>
		virtual public System.String PackageID
		{
			get
			{
				return base.GetSystemString(GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.PackageID);
			}
			
			set
			{
				base.SetSystemString(GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.PackageID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorSurgicalPackageCoveredItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorSurgicalPackageCoveredItem.CoveredAmount
		/// </summary>
		virtual public System.Decimal? CoveredAmount
		{
			get
			{
				return base.GetSystemDecimal(GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.CoveredAmount);
			}
			
			set
			{
				base.SetSystemDecimal(GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.CoveredAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorSurgicalPackageCoveredItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorSurgicalPackageCoveredItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esGuarantorSurgicalPackageCoveredItem entity)
			{
				this.entity = entity;
			}
			
	
			public System.String GuarantorID
			{
				get
				{
					System.String data = entity.GuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorID = null;
					else entity.GuarantorID = Convert.ToString(value);
				}
			}
				
			public System.String PackageID
			{
				get
				{
					System.String data = entity.PackageID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PackageID = null;
					else entity.PackageID = Convert.ToString(value);
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
				
			public System.String CoveredAmount
			{
				get
				{
					System.Decimal? data = entity.CoveredAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoveredAmount = null;
					else entity.CoveredAmount = Convert.ToDecimal(value);
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
			

			private esGuarantorSurgicalPackageCoveredItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esGuarantorSurgicalPackageCoveredItemQuery query)
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
				throw new Exception("esGuarantorSurgicalPackageCoveredItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class GuarantorSurgicalPackageCoveredItem : esGuarantorSurgicalPackageCoveredItem
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
	abstract public class esGuarantorSurgicalPackageCoveredItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorSurgicalPackageCoveredItemMetadata.Meta();
			}
		}	
		

		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem PackageID
		{
			get
			{
				return new esQueryItem(this, GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.PackageID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem CoveredAmount
		{
			get
			{
				return new esQueryItem(this, GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.CoveredAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("GuarantorSurgicalPackageCoveredItemCollection")]
	public partial class GuarantorSurgicalPackageCoveredItemCollection : esGuarantorSurgicalPackageCoveredItemCollection, IEnumerable<GuarantorSurgicalPackageCoveredItem>
	{
		public GuarantorSurgicalPackageCoveredItemCollection()
		{

		}
		
		public static implicit operator List<GuarantorSurgicalPackageCoveredItem>(GuarantorSurgicalPackageCoveredItemCollection coll)
		{
			List<GuarantorSurgicalPackageCoveredItem> list = new List<GuarantorSurgicalPackageCoveredItem>();
			
			foreach (GuarantorSurgicalPackageCoveredItem emp in coll)
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
				return  GuarantorSurgicalPackageCoveredItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorSurgicalPackageCoveredItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new GuarantorSurgicalPackageCoveredItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new GuarantorSurgicalPackageCoveredItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public GuarantorSurgicalPackageCoveredItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorSurgicalPackageCoveredItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(GuarantorSurgicalPackageCoveredItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public GuarantorSurgicalPackageCoveredItem AddNew()
		{
			GuarantorSurgicalPackageCoveredItem entity = base.AddNewEntity() as GuarantorSurgicalPackageCoveredItem;
			
			return entity;
		}

		public GuarantorSurgicalPackageCoveredItem FindByPrimaryKey(System.String guarantorID, System.String packageID, System.String itemID)
		{
			return base.FindByPrimaryKey(guarantorID, packageID, itemID) as GuarantorSurgicalPackageCoveredItem;
		}


		#region IEnumerable<GuarantorSurgicalPackageCoveredItem> Members

		IEnumerator<GuarantorSurgicalPackageCoveredItem> IEnumerable<GuarantorSurgicalPackageCoveredItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as GuarantorSurgicalPackageCoveredItem;
			}
		}

		#endregion
		
		private GuarantorSurgicalPackageCoveredItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'GuarantorSurgicalPackageCoveredItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("GuarantorSurgicalPackageCoveredItem ({GuarantorID},{PackageID},{ItemID})")]
	[Serializable]
	public partial class GuarantorSurgicalPackageCoveredItem : esGuarantorSurgicalPackageCoveredItem
	{
		public GuarantorSurgicalPackageCoveredItem()
		{

		}
	
		public GuarantorSurgicalPackageCoveredItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorSurgicalPackageCoveredItemMetadata.Meta();
			}
		}
		
		
		
		override protected esGuarantorSurgicalPackageCoveredItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorSurgicalPackageCoveredItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public GuarantorSurgicalPackageCoveredItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorSurgicalPackageCoveredItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(GuarantorSurgicalPackageCoveredItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private GuarantorSurgicalPackageCoveredItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class GuarantorSurgicalPackageCoveredItemQuery : esGuarantorSurgicalPackageCoveredItemQuery
	{
		public GuarantorSurgicalPackageCoveredItemQuery()
		{

		}		
		
		public GuarantorSurgicalPackageCoveredItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "GuarantorSurgicalPackageCoveredItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class GuarantorSurgicalPackageCoveredItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected GuarantorSurgicalPackageCoveredItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.GuarantorID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorSurgicalPackageCoveredItemMetadata.PropertyNames.GuarantorID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.PackageID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorSurgicalPackageCoveredItemMetadata.PropertyNames.PackageID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorSurgicalPackageCoveredItemMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.CoveredAmount, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorSurgicalPackageCoveredItemMetadata.PropertyNames.CoveredAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorSurgicalPackageCoveredItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorSurgicalPackageCoveredItemMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorSurgicalPackageCoveredItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public GuarantorSurgicalPackageCoveredItemMetadata Meta()
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
			 public const string GuarantorID = "GuarantorID";
			 public const string PackageID = "PackageID";
			 public const string ItemID = "ItemID";
			 public const string CoveredAmount = "CoveredAmount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string GuarantorID = "GuarantorID";
			 public const string PackageID = "PackageID";
			 public const string ItemID = "ItemID";
			 public const string CoveredAmount = "CoveredAmount";
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
			lock (typeof(GuarantorSurgicalPackageCoveredItemMetadata))
			{
				if(GuarantorSurgicalPackageCoveredItemMetadata.mapDelegates == null)
				{
					GuarantorSurgicalPackageCoveredItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (GuarantorSurgicalPackageCoveredItemMetadata.meta == null)
				{
					GuarantorSurgicalPackageCoveredItemMetadata.meta = new GuarantorSurgicalPackageCoveredItemMetadata();
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
				

				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PackageID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CoveredAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "GuarantorSurgicalPackageCoveredItem";
				meta.Destination = "GuarantorSurgicalPackageCoveredItem";
				
				meta.spInsert = "proc_GuarantorSurgicalPackageCoveredItemInsert";				
				meta.spUpdate = "proc_GuarantorSurgicalPackageCoveredItemUpdate";		
				meta.spDelete = "proc_GuarantorSurgicalPackageCoveredItemDelete";
				meta.spLoadAll = "proc_GuarantorSurgicalPackageCoveredItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_GuarantorSurgicalPackageCoveredItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private GuarantorSurgicalPackageCoveredItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
