/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 10/8/2014 10:26:57 AM
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
	abstract public class esDataRptMasterCollection : esEntityCollection
	{
		public esDataRptMasterCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "DataRptMasterCollection";
		}

		#region Query Logic
		protected void InitQuery(esDataRptMasterQuery query)
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
			this.InitQuery(query as esDataRptMasterQuery);
		}
		#endregion
		
		virtual public DataRptMaster DetachEntity(DataRptMaster entity)
		{
			return base.DetachEntity(entity) as DataRptMaster;
		}
		
		virtual public DataRptMaster AttachEntity(DataRptMaster entity)
		{
			return base.AttachEntity(entity) as DataRptMaster;
		}
		
		virtual public void Combine(DataRptMasterCollection collection)
		{
			base.Combine(collection);
		}
		
		new public DataRptMaster this[int index]
		{
			get
			{
				return base[index] as DataRptMaster;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(DataRptMaster);
		}
	}



	[Serializable]
	abstract public class esDataRptMaster : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDataRptMasterQuery GetDynamicQuery()
		{
			return null;
		}

		public esDataRptMaster()
		{

		}

		public esDataRptMaster(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String sRDataRpt, System.String itemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRDataRpt, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(sRDataRpt, itemID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String sRDataRpt, System.String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRDataRpt, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(sRDataRpt, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String sRDataRpt, System.String itemID)
		{
			esDataRptMasterQuery query = this.GetDynamicQuery();
			query.Where(query.SRDataRpt == sRDataRpt, query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String sRDataRpt, System.String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("SRDataRpt",sRDataRpt);			parms.Add("ItemID",itemID);
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
						case "SeqNo": this.str.SeqNo = (string)value; break;							
						case "ItemCode": this.str.ItemCode = (string)value; break;							
						case "ItemName": this.str.ItemName = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "SeqNo":
						
							if (value == null || value is System.Int32)
								this.SeqNo = (System.Int32?)value;
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
		/// Maps to DataRptMaster.SRDataRpt
		/// </summary>
		virtual public System.String SRDataRpt
		{
			get
			{
				return base.GetSystemString(DataRptMasterMetadata.ColumnNames.SRDataRpt);
			}
			
			set
			{
				base.SetSystemString(DataRptMasterMetadata.ColumnNames.SRDataRpt, value);
			}
		}
		
		/// <summary>
		/// Maps to DataRptMaster.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(DataRptMasterMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(DataRptMasterMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to DataRptMaster.SeqNo
		/// </summary>
		virtual public System.Int32? SeqNo
		{
			get
			{
				return base.GetSystemInt32(DataRptMasterMetadata.ColumnNames.SeqNo);
			}
			
			set
			{
				base.SetSystemInt32(DataRptMasterMetadata.ColumnNames.SeqNo, value);
			}
		}
		
		/// <summary>
		/// Maps to DataRptMaster.ItemCode
		/// </summary>
		virtual public System.String ItemCode
		{
			get
			{
				return base.GetSystemString(DataRptMasterMetadata.ColumnNames.ItemCode);
			}
			
			set
			{
				base.SetSystemString(DataRptMasterMetadata.ColumnNames.ItemCode, value);
			}
		}
		
		/// <summary>
		/// Maps to DataRptMaster.ItemName
		/// </summary>
		virtual public System.String ItemName
		{
			get
			{
				return base.GetSystemString(DataRptMasterMetadata.ColumnNames.ItemName);
			}
			
			set
			{
				base.SetSystemString(DataRptMasterMetadata.ColumnNames.ItemName, value);
			}
		}
		
		/// <summary>
		/// Maps to DataRptMaster.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(DataRptMasterMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(DataRptMasterMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to DataRptMaster.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(DataRptMasterMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(DataRptMasterMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esDataRptMaster entity)
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
				
			public System.String SeqNo
			{
				get
				{
					System.Int32? data = entity.SeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeqNo = null;
					else entity.SeqNo = Convert.ToInt32(value);
				}
			}
				
			public System.String ItemCode
			{
				get
				{
					System.String data = entity.ItemCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemCode = null;
					else entity.ItemCode = Convert.ToString(value);
				}
			}
				
			public System.String ItemName
			{
				get
				{
					System.String data = entity.ItemName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemName = null;
					else entity.ItemName = Convert.ToString(value);
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
			

			private esDataRptMaster entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDataRptMasterQuery query)
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
				throw new Exception("esDataRptMaster can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esDataRptMasterQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return DataRptMasterMetadata.Meta();
			}
		}	
		

		public esQueryItem SRDataRpt
		{
			get
			{
				return new esQueryItem(this, DataRptMasterMetadata.ColumnNames.SRDataRpt, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, DataRptMasterMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem SeqNo
		{
			get
			{
				return new esQueryItem(this, DataRptMasterMetadata.ColumnNames.SeqNo, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ItemCode
		{
			get
			{
				return new esQueryItem(this, DataRptMasterMetadata.ColumnNames.ItemCode, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemName
		{
			get
			{
				return new esQueryItem(this, DataRptMasterMetadata.ColumnNames.ItemName, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, DataRptMasterMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, DataRptMasterMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DataRptMasterCollection")]
	public partial class DataRptMasterCollection : esDataRptMasterCollection, IEnumerable<DataRptMaster>
	{
		public DataRptMasterCollection()
		{

		}
		
		public static implicit operator List<DataRptMaster>(DataRptMasterCollection coll)
		{
			List<DataRptMaster> list = new List<DataRptMaster>();
			
			foreach (DataRptMaster emp in coll)
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
				return  DataRptMasterMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DataRptMasterQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new DataRptMaster(row);
		}

		override protected esEntity CreateEntity()
		{
			return new DataRptMaster();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public DataRptMasterQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DataRptMasterQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(DataRptMasterQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public DataRptMaster AddNew()
		{
			DataRptMaster entity = base.AddNewEntity() as DataRptMaster;
			
			return entity;
		}

		public DataRptMaster FindByPrimaryKey(System.String sRDataRpt, System.String itemID)
		{
			return base.FindByPrimaryKey(sRDataRpt, itemID) as DataRptMaster;
		}


		#region IEnumerable<DataRptMaster> Members

		IEnumerator<DataRptMaster> IEnumerable<DataRptMaster>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as DataRptMaster;
			}
		}

		#endregion
		
		private DataRptMasterQuery query;
	}


	/// <summary>
	/// Encapsulates the 'DataRptMaster' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("DataRptMaster ({SRDataRpt},{ItemID})")]
	[Serializable]
	public partial class DataRptMaster : esDataRptMaster
	{
		public DataRptMaster()
		{

		}
	
		public DataRptMaster(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DataRptMasterMetadata.Meta();
			}
		}
		
		
		
		override protected esDataRptMasterQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DataRptMasterQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public DataRptMasterQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DataRptMasterQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(DataRptMasterQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private DataRptMasterQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class DataRptMasterQuery : esDataRptMasterQuery
	{
		public DataRptMasterQuery()
		{

		}		
		
		public DataRptMasterQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "DataRptMasterQuery";
        }
		
			
	}


	[Serializable]
	public partial class DataRptMasterMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DataRptMasterMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DataRptMasterMetadata.ColumnNames.SRDataRpt, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = DataRptMasterMetadata.PropertyNames.SRDataRpt;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(DataRptMasterMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = DataRptMasterMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(DataRptMasterMetadata.ColumnNames.SeqNo, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = DataRptMasterMetadata.PropertyNames.SeqNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DataRptMasterMetadata.ColumnNames.ItemCode, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = DataRptMasterMetadata.PropertyNames.ItemCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DataRptMasterMetadata.ColumnNames.ItemName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = DataRptMasterMetadata.PropertyNames.ItemName;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DataRptMasterMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DataRptMasterMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DataRptMasterMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = DataRptMasterMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public DataRptMasterMetadata Meta()
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
			 public const string SeqNo = "SeqNo";
			 public const string ItemCode = "ItemCode";
			 public const string ItemName = "ItemName";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SRDataRpt = "SRDataRpt";
			 public const string ItemID = "ItemID";
			 public const string SeqNo = "SeqNo";
			 public const string ItemCode = "ItemCode";
			 public const string ItemName = "ItemName";
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
			lock (typeof(DataRptMasterMetadata))
			{
				if(DataRptMasterMetadata.mapDelegates == null)
				{
					DataRptMasterMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (DataRptMasterMetadata.meta == null)
				{
					DataRptMasterMetadata.meta = new DataRptMasterMetadata();
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
				meta.AddTypeMap("SeqNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ItemCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "DataRptMaster";
				meta.Destination = "DataRptMaster";
				
				meta.spInsert = "proc_DataRptMasterInsert";				
				meta.spUpdate = "proc_DataRptMasterUpdate";		
				meta.spDelete = "proc_DataRptMasterDelete";
				meta.spLoadAll = "proc_DataRptMasterLoadAll";
				meta.spLoadByPrimaryKey = "proc_DataRptMasterLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DataRptMasterMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
