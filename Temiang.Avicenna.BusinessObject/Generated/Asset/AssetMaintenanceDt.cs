/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:11 PM
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
	abstract public class esAssetMaintenanceDtCollection : esEntityCollectionWAuditLog
	{
		public esAssetMaintenanceDtCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AssetMaintenanceDtCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetMaintenanceDtQuery query)
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
			this.InitQuery(query as esAssetMaintenanceDtQuery);
		}
		#endregion
		
		virtual public AssetMaintenanceDt DetachEntity(AssetMaintenanceDt entity)
		{
			return base.DetachEntity(entity) as AssetMaintenanceDt;
		}
		
		virtual public AssetMaintenanceDt AttachEntity(AssetMaintenanceDt entity)
		{
			return base.AttachEntity(entity) as AssetMaintenanceDt;
		}
		
		virtual public void Combine(AssetMaintenanceDtCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AssetMaintenanceDt this[int index]
		{
			get
			{
				return base[index] as AssetMaintenanceDt;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetMaintenanceDt);
		}
	}



	[Serializable]
	abstract public class esAssetMaintenanceDt : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetMaintenanceDtQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetMaintenanceDt()
		{

		}

		public esAssetMaintenanceDt(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 maintenanceItemId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(maintenanceItemId);
			else
				return LoadByPrimaryKeyStoredProcedure(maintenanceItemId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 maintenanceItemId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(maintenanceItemId);
			else
				return LoadByPrimaryKeyStoredProcedure(maintenanceItemId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 maintenanceItemId)
		{
			esAssetMaintenanceDtQuery query = this.GetDynamicQuery();
			query.Where(query.MaintenanceItemId == maintenanceItemId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 maintenanceItemId)
		{
			esParameters parms = new esParameters();
			parms.Add("MaintenanceItemId",maintenanceItemId);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "IsMasterItem": this.str.IsMasterItem = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "Quantity": this.str.Quantity = (string)value; break;							
						case "ItemUnit": this.str.ItemUnit = (string)value; break;							
						case "ConversionFactor": this.str.ConversionFactor = (string)value; break;							
						case "BaseItemUnit": this.str.BaseItemUnit = (string)value; break;							
						case "BaseQuantity": this.str.BaseQuantity = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "MaintenanceItemId": this.str.MaintenanceItemId = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsMasterItem":
						
							if (value == null || value is System.Boolean)
								this.IsMasterItem = (System.Boolean?)value;
							break;
						
						case "Quantity":
						
							if (value == null || value is System.Decimal)
								this.Quantity = (System.Decimal?)value;
							break;
						
						case "ConversionFactor":
						
							if (value == null || value is System.Decimal)
								this.ConversionFactor = (System.Decimal?)value;
							break;
						
						case "BaseQuantity":
						
							if (value == null || value is System.Decimal)
								this.BaseQuantity = (System.Decimal?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "MaintenanceItemId":
						
							if (value == null || value is System.Int32)
								this.MaintenanceItemId = (System.Int32?)value;
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
		/// Maps to AssetMaintenanceDt.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceDtMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceDtMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceDt.IsMasterItem
		/// </summary>
		virtual public System.Boolean? IsMasterItem
		{
			get
			{
				return base.GetSystemBoolean(AssetMaintenanceDtMetadata.ColumnNames.IsMasterItem);
			}
			
			set
			{
				base.SetSystemBoolean(AssetMaintenanceDtMetadata.ColumnNames.IsMasterItem, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceDt.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceDtMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceDtMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceDt.Quantity
		/// </summary>
		virtual public System.Decimal? Quantity
		{
			get
			{
				return base.GetSystemDecimal(AssetMaintenanceDtMetadata.ColumnNames.Quantity);
			}
			
			set
			{
				base.SetSystemDecimal(AssetMaintenanceDtMetadata.ColumnNames.Quantity, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceDt.ItemUnit
		/// </summary>
		virtual public System.String ItemUnit
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceDtMetadata.ColumnNames.ItemUnit);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceDtMetadata.ColumnNames.ItemUnit, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceDt.ConversionFactor
		/// </summary>
		virtual public System.Decimal? ConversionFactor
		{
			get
			{
				return base.GetSystemDecimal(AssetMaintenanceDtMetadata.ColumnNames.ConversionFactor);
			}
			
			set
			{
				base.SetSystemDecimal(AssetMaintenanceDtMetadata.ColumnNames.ConversionFactor, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceDt.BaseItemUnit
		/// </summary>
		virtual public System.String BaseItemUnit
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceDtMetadata.ColumnNames.BaseItemUnit);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceDtMetadata.ColumnNames.BaseItemUnit, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceDt.BaseQuantity
		/// </summary>
		virtual public System.Decimal? BaseQuantity
		{
			get
			{
				return base.GetSystemDecimal(AssetMaintenanceDtMetadata.ColumnNames.BaseQuantity);
			}
			
			set
			{
				base.SetSystemDecimal(AssetMaintenanceDtMetadata.ColumnNames.BaseQuantity, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceDt.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetMaintenanceDtMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AssetMaintenanceDtMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceDt.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceDtMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceDtMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceDt.MaintenanceItemId
		/// </summary>
		virtual public System.Int32? MaintenanceItemId
		{
			get
			{
				return base.GetSystemInt32(AssetMaintenanceDtMetadata.ColumnNames.MaintenanceItemId);
			}
			
			set
			{
				base.SetSystemInt32(AssetMaintenanceDtMetadata.ColumnNames.MaintenanceItemId, value);
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
			public esStrings(esAssetMaintenanceDt entity)
			{
				this.entity = entity;
			}
			
	
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
				}
			}
				
			public System.String IsMasterItem
			{
				get
				{
					System.Boolean? data = entity.IsMasterItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMasterItem = null;
					else entity.IsMasterItem = Convert.ToBoolean(value);
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
				
			public System.String Quantity
			{
				get
				{
					System.Decimal? data = entity.Quantity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Quantity = null;
					else entity.Quantity = Convert.ToDecimal(value);
				}
			}
				
			public System.String ItemUnit
			{
				get
				{
					System.String data = entity.ItemUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemUnit = null;
					else entity.ItemUnit = Convert.ToString(value);
				}
			}
				
			public System.String ConversionFactor
			{
				get
				{
					System.Decimal? data = entity.ConversionFactor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConversionFactor = null;
					else entity.ConversionFactor = Convert.ToDecimal(value);
				}
			}
				
			public System.String BaseItemUnit
			{
				get
				{
					System.String data = entity.BaseItemUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BaseItemUnit = null;
					else entity.BaseItemUnit = Convert.ToString(value);
				}
			}
				
			public System.String BaseQuantity
			{
				get
				{
					System.Decimal? data = entity.BaseQuantity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BaseQuantity = null;
					else entity.BaseQuantity = Convert.ToDecimal(value);
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
				
			public System.String MaintenanceItemId
			{
				get
				{
					System.Int32? data = entity.MaintenanceItemId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaintenanceItemId = null;
					else entity.MaintenanceItemId = Convert.ToInt32(value);
				}
			}
			

			private esAssetMaintenanceDt entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetMaintenanceDtQuery query)
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
				throw new Exception("esAssetMaintenanceDt can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AssetMaintenanceDt : esAssetMaintenanceDt
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
	abstract public class esAssetMaintenanceDtQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AssetMaintenanceDtMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceDtMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem IsMasterItem
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceDtMetadata.ColumnNames.IsMasterItem, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceDtMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem Quantity
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceDtMetadata.ColumnNames.Quantity, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ItemUnit
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceDtMetadata.ColumnNames.ItemUnit, esSystemType.String);
			}
		} 
		
		public esQueryItem ConversionFactor
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceDtMetadata.ColumnNames.ConversionFactor, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem BaseItemUnit
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceDtMetadata.ColumnNames.BaseItemUnit, esSystemType.String);
			}
		} 
		
		public esQueryItem BaseQuantity
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceDtMetadata.ColumnNames.BaseQuantity, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceDtMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceDtMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem MaintenanceItemId
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceDtMetadata.ColumnNames.MaintenanceItemId, esSystemType.Int32);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetMaintenanceDtCollection")]
	public partial class AssetMaintenanceDtCollection : esAssetMaintenanceDtCollection, IEnumerable<AssetMaintenanceDt>
	{
		public AssetMaintenanceDtCollection()
		{

		}
		
		public static implicit operator List<AssetMaintenanceDt>(AssetMaintenanceDtCollection coll)
		{
			List<AssetMaintenanceDt> list = new List<AssetMaintenanceDt>();
			
			foreach (AssetMaintenanceDt emp in coll)
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
				return  AssetMaintenanceDtMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetMaintenanceDtQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetMaintenanceDt(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetMaintenanceDt();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AssetMaintenanceDtQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetMaintenanceDtQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AssetMaintenanceDtQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AssetMaintenanceDt AddNew()
		{
			AssetMaintenanceDt entity = base.AddNewEntity() as AssetMaintenanceDt;
			
			return entity;
		}

		public AssetMaintenanceDt FindByPrimaryKey(System.Int32 maintenanceItemId)
		{
			return base.FindByPrimaryKey(maintenanceItemId) as AssetMaintenanceDt;
		}


		#region IEnumerable<AssetMaintenanceDt> Members

		IEnumerator<AssetMaintenanceDt> IEnumerable<AssetMaintenanceDt>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AssetMaintenanceDt;
			}
		}

		#endregion
		
		private AssetMaintenanceDtQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetMaintenanceDt' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AssetMaintenanceDt ({MaintenanceItemId})")]
	[Serializable]
	public partial class AssetMaintenanceDt : esAssetMaintenanceDt
	{
		public AssetMaintenanceDt()
		{

		}
	
		public AssetMaintenanceDt(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetMaintenanceDtMetadata.Meta();
			}
		}
		
		
		
		override protected esAssetMaintenanceDtQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetMaintenanceDtQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AssetMaintenanceDtQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetMaintenanceDtQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AssetMaintenanceDtQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AssetMaintenanceDtQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AssetMaintenanceDtQuery : esAssetMaintenanceDtQuery
	{
		public AssetMaintenanceDtQuery()
		{

		}		
		
		public AssetMaintenanceDtQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AssetMaintenanceDtQuery";
        }
		
			
	}


	[Serializable]
	public partial class AssetMaintenanceDtMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetMaintenanceDtMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetMaintenanceDtMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceDtMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceDtMetadata.ColumnNames.IsMasterItem, 1, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetMaintenanceDtMetadata.PropertyNames.IsMasterItem;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceDtMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceDtMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceDtMetadata.ColumnNames.Quantity, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetMaintenanceDtMetadata.PropertyNames.Quantity;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceDtMetadata.ColumnNames.ItemUnit, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceDtMetadata.PropertyNames.ItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceDtMetadata.ColumnNames.ConversionFactor, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetMaintenanceDtMetadata.PropertyNames.ConversionFactor;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceDtMetadata.ColumnNames.BaseItemUnit, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceDtMetadata.PropertyNames.BaseItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceDtMetadata.ColumnNames.BaseQuantity, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetMaintenanceDtMetadata.PropertyNames.BaseQuantity;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceDtMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMaintenanceDtMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceDtMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceDtMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceDtMetadata.ColumnNames.MaintenanceItemId, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetMaintenanceDtMetadata.PropertyNames.MaintenanceItemId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AssetMaintenanceDtMetadata Meta()
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
			 public const string TransactionNo = "TransactionNo";
			 public const string IsMasterItem = "IsMasterItem";
			 public const string ItemID = "ItemID";
			 public const string Quantity = "Quantity";
			 public const string ItemUnit = "ItemUnit";
			 public const string ConversionFactor = "ConversionFactor";
			 public const string BaseItemUnit = "BaseItemUnit";
			 public const string BaseQuantity = "BaseQuantity";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string MaintenanceItemId = "MaintenanceItemId";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransactionNo = "TransactionNo";
			 public const string IsMasterItem = "IsMasterItem";
			 public const string ItemID = "ItemID";
			 public const string Quantity = "Quantity";
			 public const string ItemUnit = "ItemUnit";
			 public const string ConversionFactor = "ConversionFactor";
			 public const string BaseItemUnit = "BaseItemUnit";
			 public const string BaseQuantity = "BaseQuantity";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string MaintenanceItemId = "MaintenanceItemId";
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
			lock (typeof(AssetMaintenanceDtMetadata))
			{
				if(AssetMaintenanceDtMetadata.mapDelegates == null)
				{
					AssetMaintenanceDtMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AssetMaintenanceDtMetadata.meta == null)
				{
					AssetMaintenanceDtMetadata.meta = new AssetMaintenanceDtMetadata();
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
				

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsMasterItem", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Quantity", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConversionFactor", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BaseItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BaseQuantity", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MaintenanceItemId", new esTypeMap("int", "System.Int32"));			
				
				
				
				meta.Source = "AssetMaintenanceDt";
				meta.Destination = "AssetMaintenanceDt";
				
				meta.spInsert = "proc_AssetMaintenanceDtInsert";				
				meta.spUpdate = "proc_AssetMaintenanceDtUpdate";		
				meta.spDelete = "proc_AssetMaintenanceDtDelete";
				meta.spLoadAll = "proc_AssetMaintenanceDtLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetMaintenanceDtLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetMaintenanceDtMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
