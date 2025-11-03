/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/20/2013 9:44:16 AM
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
	abstract public class esSnackOrderItemCollection : esEntityCollectionWAuditLog
	{
		public esSnackOrderItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "SnackOrderItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esSnackOrderItemQuery query)
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
			this.InitQuery(query as esSnackOrderItemQuery);
		}
		#endregion
		
		virtual public SnackOrderItem DetachEntity(SnackOrderItem entity)
		{
			return base.DetachEntity(entity) as SnackOrderItem;
		}
		
		virtual public SnackOrderItem AttachEntity(SnackOrderItem entity)
		{
			return base.AttachEntity(entity) as SnackOrderItem;
		}
		
		virtual public void Combine(SnackOrderItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public SnackOrderItem this[int index]
		{
			get
			{
				return base[index] as SnackOrderItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SnackOrderItem);
		}
	}



	[Serializable]
	abstract public class esSnackOrderItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSnackOrderItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esSnackOrderItem()
		{

		}

		public esSnackOrderItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String snackOrderNo, System.String snackID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(snackOrderNo, snackID);
			else
				return LoadByPrimaryKeyStoredProcedure(snackOrderNo, snackID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String snackOrderNo, System.String snackID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(snackOrderNo, snackID);
			else
				return LoadByPrimaryKeyStoredProcedure(snackOrderNo, snackID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String snackOrderNo, System.String snackID)
		{
			esSnackOrderItemQuery query = this.GetDynamicQuery();
			query.Where(query.SnackOrderNo == snackOrderNo, query.SnackID == snackID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String snackOrderNo, System.String snackID)
		{
			esParameters parms = new esParameters();
			parms.Add("SnackOrderNo",snackOrderNo);			parms.Add("SnackID",snackID);
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
						case "SnackOrderNo": this.str.SnackOrderNo = (string)value; break;							
						case "SnackID": this.str.SnackID = (string)value; break;							
						case "QtyShift1": this.str.QtyShift1 = (string)value; break;							
						case "QtyShift2": this.str.QtyShift2 = (string)value; break;							
						case "QtyShift3": this.str.QtyShift3 = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "QtyShift1":
						
							if (value == null || value is System.Decimal)
								this.QtyShift1 = (System.Decimal?)value;
							break;
						
						case "QtyShift2":
						
							if (value == null || value is System.Decimal)
								this.QtyShift2 = (System.Decimal?)value;
							break;
						
						case "QtyShift3":
						
							if (value == null || value is System.Decimal)
								this.QtyShift3 = (System.Decimal?)value;
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
		/// Maps to SnackOrderItem.SnackOrderNo
		/// </summary>
		virtual public System.String SnackOrderNo
		{
			get
			{
				return base.GetSystemString(SnackOrderItemMetadata.ColumnNames.SnackOrderNo);
			}
			
			set
			{
				base.SetSystemString(SnackOrderItemMetadata.ColumnNames.SnackOrderNo, value);
			}
		}
		
		/// <summary>
		/// Maps to SnackOrderItem.SnackID
		/// </summary>
		virtual public System.String SnackID
		{
			get
			{
				return base.GetSystemString(SnackOrderItemMetadata.ColumnNames.SnackID);
			}
			
			set
			{
				base.SetSystemString(SnackOrderItemMetadata.ColumnNames.SnackID, value);
			}
		}
		
		/// <summary>
		/// Maps to SnackOrderItem.QtyShift1
		/// </summary>
		virtual public System.Decimal? QtyShift1
		{
			get
			{
				return base.GetSystemDecimal(SnackOrderItemMetadata.ColumnNames.QtyShift1);
			}
			
			set
			{
				base.SetSystemDecimal(SnackOrderItemMetadata.ColumnNames.QtyShift1, value);
			}
		}
		
		/// <summary>
		/// Maps to SnackOrderItem.QtyShift2
		/// </summary>
		virtual public System.Decimal? QtyShift2
		{
			get
			{
				return base.GetSystemDecimal(SnackOrderItemMetadata.ColumnNames.QtyShift2);
			}
			
			set
			{
				base.SetSystemDecimal(SnackOrderItemMetadata.ColumnNames.QtyShift2, value);
			}
		}
		
		/// <summary>
		/// Maps to SnackOrderItem.QtyShift3
		/// </summary>
		virtual public System.Decimal? QtyShift3
		{
			get
			{
				return base.GetSystemDecimal(SnackOrderItemMetadata.ColumnNames.QtyShift3);
			}
			
			set
			{
				base.SetSystemDecimal(SnackOrderItemMetadata.ColumnNames.QtyShift3, value);
			}
		}
		
		/// <summary>
		/// Maps to SnackOrderItem.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(SnackOrderItemMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(SnackOrderItemMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to SnackOrderItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SnackOrderItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(SnackOrderItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to SnackOrderItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SnackOrderItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(SnackOrderItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSnackOrderItem entity)
			{
				this.entity = entity;
			}
			
	
			public System.String SnackOrderNo
			{
				get
				{
					System.String data = entity.SnackOrderNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SnackOrderNo = null;
					else entity.SnackOrderNo = Convert.ToString(value);
				}
			}
				
			public System.String SnackID
			{
				get
				{
					System.String data = entity.SnackID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SnackID = null;
					else entity.SnackID = Convert.ToString(value);
				}
			}
				
			public System.String QtyShift1
			{
				get
				{
					System.Decimal? data = entity.QtyShift1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyShift1 = null;
					else entity.QtyShift1 = Convert.ToDecimal(value);
				}
			}
				
			public System.String QtyShift2
			{
				get
				{
					System.Decimal? data = entity.QtyShift2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyShift2 = null;
					else entity.QtyShift2 = Convert.ToDecimal(value);
				}
			}
				
			public System.String QtyShift3
			{
				get
				{
					System.Decimal? data = entity.QtyShift3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyShift3 = null;
					else entity.QtyShift3 = Convert.ToDecimal(value);
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
			

			private esSnackOrderItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSnackOrderItemQuery query)
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
				throw new Exception("esSnackOrderItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class SnackOrderItem : esSnackOrderItem
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
	abstract public class esSnackOrderItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return SnackOrderItemMetadata.Meta();
			}
		}	
		

		public esQueryItem SnackOrderNo
		{
			get
			{
				return new esQueryItem(this, SnackOrderItemMetadata.ColumnNames.SnackOrderNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SnackID
		{
			get
			{
				return new esQueryItem(this, SnackOrderItemMetadata.ColumnNames.SnackID, esSystemType.String);
			}
		} 
		
		public esQueryItem QtyShift1
		{
			get
			{
				return new esQueryItem(this, SnackOrderItemMetadata.ColumnNames.QtyShift1, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem QtyShift2
		{
			get
			{
				return new esQueryItem(this, SnackOrderItemMetadata.ColumnNames.QtyShift2, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem QtyShift3
		{
			get
			{
				return new esQueryItem(this, SnackOrderItemMetadata.ColumnNames.QtyShift3, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, SnackOrderItemMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SnackOrderItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SnackOrderItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SnackOrderItemCollection")]
	public partial class SnackOrderItemCollection : esSnackOrderItemCollection, IEnumerable<SnackOrderItem>
	{
		public SnackOrderItemCollection()
		{

		}
		
		public static implicit operator List<SnackOrderItem>(SnackOrderItemCollection coll)
		{
			List<SnackOrderItem> list = new List<SnackOrderItem>();
			
			foreach (SnackOrderItem emp in coll)
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
				return  SnackOrderItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SnackOrderItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SnackOrderItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SnackOrderItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public SnackOrderItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SnackOrderItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(SnackOrderItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public SnackOrderItem AddNew()
		{
			SnackOrderItem entity = base.AddNewEntity() as SnackOrderItem;
			
			return entity;
		}

		public SnackOrderItem FindByPrimaryKey(System.String snackOrderNo, System.String snackID)
		{
			return base.FindByPrimaryKey(snackOrderNo, snackID) as SnackOrderItem;
		}


		#region IEnumerable<SnackOrderItem> Members

		IEnumerator<SnackOrderItem> IEnumerable<SnackOrderItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as SnackOrderItem;
			}
		}

		#endregion
		
		private SnackOrderItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SnackOrderItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("SnackOrderItem ({SnackOrderNo},{SnackID})")]
	[Serializable]
	public partial class SnackOrderItem : esSnackOrderItem
	{
		public SnackOrderItem()
		{

		}
	
		public SnackOrderItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SnackOrderItemMetadata.Meta();
			}
		}
		
		
		
		override protected esSnackOrderItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SnackOrderItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public SnackOrderItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SnackOrderItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(SnackOrderItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private SnackOrderItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class SnackOrderItemQuery : esSnackOrderItemQuery
	{
		public SnackOrderItemQuery()
		{

		}		
		
		public SnackOrderItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "SnackOrderItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class SnackOrderItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SnackOrderItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SnackOrderItemMetadata.ColumnNames.SnackOrderNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = SnackOrderItemMetadata.PropertyNames.SnackOrderNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(SnackOrderItemMetadata.ColumnNames.SnackID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SnackOrderItemMetadata.PropertyNames.SnackID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(SnackOrderItemMetadata.ColumnNames.QtyShift1, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SnackOrderItemMetadata.PropertyNames.QtyShift1;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(SnackOrderItemMetadata.ColumnNames.QtyShift2, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SnackOrderItemMetadata.PropertyNames.QtyShift2;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(SnackOrderItemMetadata.ColumnNames.QtyShift3, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SnackOrderItemMetadata.PropertyNames.QtyShift3;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(SnackOrderItemMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = SnackOrderItemMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(SnackOrderItemMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SnackOrderItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(SnackOrderItemMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = SnackOrderItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public SnackOrderItemMetadata Meta()
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
			 public const string SnackOrderNo = "SnackOrderNo";
			 public const string SnackID = "SnackID";
			 public const string QtyShift1 = "QtyShift1";
			 public const string QtyShift2 = "QtyShift2";
			 public const string QtyShift3 = "QtyShift3";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SnackOrderNo = "SnackOrderNo";
			 public const string SnackID = "SnackID";
			 public const string QtyShift1 = "QtyShift1";
			 public const string QtyShift2 = "QtyShift2";
			 public const string QtyShift3 = "QtyShift3";
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
			lock (typeof(SnackOrderItemMetadata))
			{
				if(SnackOrderItemMetadata.mapDelegates == null)
				{
					SnackOrderItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (SnackOrderItemMetadata.meta == null)
				{
					SnackOrderItemMetadata.meta = new SnackOrderItemMetadata();
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
				

				meta.AddTypeMap("SnackOrderNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SnackID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QtyShift1", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QtyShift2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QtyShift3", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "SnackOrderItem";
				meta.Destination = "SnackOrderItem";
				
				meta.spInsert = "proc_SnackOrderItemInsert";				
				meta.spUpdate = "proc_SnackOrderItemUpdate";		
				meta.spDelete = "proc_SnackOrderItemDelete";
				meta.spLoadAll = "proc_SnackOrderItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_SnackOrderItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SnackOrderItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
