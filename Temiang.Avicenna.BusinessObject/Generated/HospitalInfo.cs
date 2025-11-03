/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:16 PM
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
	abstract public class esHospitalInfoCollection : esEntityCollectionWAuditLog
	{
		public esHospitalInfoCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "HospitalInfoCollection";
		}

		#region Query Logic
		protected void InitQuery(esHospitalInfoQuery query)
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
			this.InitQuery(query as esHospitalInfoQuery);
		}
		#endregion
		
		virtual public HospitalInfo DetachEntity(HospitalInfo entity)
		{
			return base.DetachEntity(entity) as HospitalInfo;
		}
		
		virtual public HospitalInfo AttachEntity(HospitalInfo entity)
		{
			return base.AttachEntity(entity) as HospitalInfo;
		}
		
		virtual public void Combine(HospitalInfoCollection collection)
		{
			base.Combine(collection);
		}
		
		new public HospitalInfo this[int index]
		{
			get
			{
				return base[index] as HospitalInfo;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(HospitalInfo);
		}
	}



	[Serializable]
	abstract public class esHospitalInfo : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esHospitalInfoQuery GetDynamicQuery()
		{
			return null;
		}

		public esHospitalInfo()
		{

		}

		public esHospitalInfo(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 hospitalInfoID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(hospitalInfoID);
			else
				return LoadByPrimaryKeyStoredProcedure(hospitalInfoID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 hospitalInfoID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(hospitalInfoID);
			else
				return LoadByPrimaryKeyStoredProcedure(hospitalInfoID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 hospitalInfoID)
		{
			esHospitalInfoQuery query = this.GetDynamicQuery();
			query.Where(query.HospitalInfoID == hospitalInfoID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 hospitalInfoID)
		{
			esParameters parms = new esParameters();
			parms.Add("HospitalInfoID",hospitalInfoID);
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
						case "HospitalInfoID": this.str.HospitalInfoID = (string)value; break;							
						case "HospitalName": this.str.HospitalName = (string)value; break;							
						case "Address": this.str.Address = (string)value; break;							
						case "SRState": this.str.SRState = (string)value; break;							
						case "SRCity": this.str.SRCity = (string)value; break;							
						case "ZipCode": this.str.ZipCode = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "HospitalInfoID":
						
							if (value == null || value is System.Int32)
								this.HospitalInfoID = (System.Int32?)value;
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
		/// Maps to HospitalInfo.HospitalInfoID
		/// </summary>
		virtual public System.Int32? HospitalInfoID
		{
			get
			{
				return base.GetSystemInt32(HospitalInfoMetadata.ColumnNames.HospitalInfoID);
			}
			
			set
			{
				base.SetSystemInt32(HospitalInfoMetadata.ColumnNames.HospitalInfoID, value);
			}
		}
		
		/// <summary>
		/// Maps to HospitalInfo.HospitalName
		/// </summary>
		virtual public System.String HospitalName
		{
			get
			{
				return base.GetSystemString(HospitalInfoMetadata.ColumnNames.HospitalName);
			}
			
			set
			{
				base.SetSystemString(HospitalInfoMetadata.ColumnNames.HospitalName, value);
			}
		}
		
		/// <summary>
		/// Maps to HospitalInfo.Address
		/// </summary>
		virtual public System.String Address
		{
			get
			{
				return base.GetSystemString(HospitalInfoMetadata.ColumnNames.Address);
			}
			
			set
			{
				base.SetSystemString(HospitalInfoMetadata.ColumnNames.Address, value);
			}
		}
		
		/// <summary>
		/// Maps to HospitalInfo.SRState
		/// </summary>
		virtual public System.String SRState
		{
			get
			{
				return base.GetSystemString(HospitalInfoMetadata.ColumnNames.SRState);
			}
			
			set
			{
				base.SetSystemString(HospitalInfoMetadata.ColumnNames.SRState, value);
			}
		}
		
		/// <summary>
		/// Maps to HospitalInfo.SRCity
		/// </summary>
		virtual public System.String SRCity
		{
			get
			{
				return base.GetSystemString(HospitalInfoMetadata.ColumnNames.SRCity);
			}
			
			set
			{
				base.SetSystemString(HospitalInfoMetadata.ColumnNames.SRCity, value);
			}
		}
		
		/// <summary>
		/// Maps to HospitalInfo.ZipCode
		/// </summary>
		virtual public System.String ZipCode
		{
			get
			{
				return base.GetSystemString(HospitalInfoMetadata.ColumnNames.ZipCode);
			}
			
			set
			{
				base.SetSystemString(HospitalInfoMetadata.ColumnNames.ZipCode, value);
			}
		}
		
		/// <summary>
		/// Maps to HospitalInfo.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(HospitalInfoMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(HospitalInfoMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to HospitalInfo.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(HospitalInfoMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(HospitalInfoMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esHospitalInfo entity)
			{
				this.entity = entity;
			}
			
	
			public System.String HospitalInfoID
			{
				get
				{
					System.Int32? data = entity.HospitalInfoID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HospitalInfoID = null;
					else entity.HospitalInfoID = Convert.ToInt32(value);
				}
			}
				
			public System.String HospitalName
			{
				get
				{
					System.String data = entity.HospitalName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HospitalName = null;
					else entity.HospitalName = Convert.ToString(value);
				}
			}
				
			public System.String Address
			{
				get
				{
					System.String data = entity.Address;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Address = null;
					else entity.Address = Convert.ToString(value);
				}
			}
				
			public System.String SRState
			{
				get
				{
					System.String data = entity.SRState;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRState = null;
					else entity.SRState = Convert.ToString(value);
				}
			}
				
			public System.String SRCity
			{
				get
				{
					System.String data = entity.SRCity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCity = null;
					else entity.SRCity = Convert.ToString(value);
				}
			}
				
			public System.String ZipCode
			{
				get
				{
					System.String data = entity.ZipCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ZipCode = null;
					else entity.ZipCode = Convert.ToString(value);
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
			

			private esHospitalInfo entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esHospitalInfoQuery query)
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
				throw new Exception("esHospitalInfo can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class HospitalInfo : esHospitalInfo
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
	abstract public class esHospitalInfoQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return HospitalInfoMetadata.Meta();
			}
		}	
		

		public esQueryItem HospitalInfoID
		{
			get
			{
				return new esQueryItem(this, HospitalInfoMetadata.ColumnNames.HospitalInfoID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem HospitalName
		{
			get
			{
				return new esQueryItem(this, HospitalInfoMetadata.ColumnNames.HospitalName, esSystemType.String);
			}
		} 
		
		public esQueryItem Address
		{
			get
			{
				return new esQueryItem(this, HospitalInfoMetadata.ColumnNames.Address, esSystemType.String);
			}
		} 
		
		public esQueryItem SRState
		{
			get
			{
				return new esQueryItem(this, HospitalInfoMetadata.ColumnNames.SRState, esSystemType.String);
			}
		} 
		
		public esQueryItem SRCity
		{
			get
			{
				return new esQueryItem(this, HospitalInfoMetadata.ColumnNames.SRCity, esSystemType.String);
			}
		} 
		
		public esQueryItem ZipCode
		{
			get
			{
				return new esQueryItem(this, HospitalInfoMetadata.ColumnNames.ZipCode, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, HospitalInfoMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, HospitalInfoMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("HospitalInfoCollection")]
	public partial class HospitalInfoCollection : esHospitalInfoCollection, IEnumerable<HospitalInfo>
	{
		public HospitalInfoCollection()
		{

		}
		
		public static implicit operator List<HospitalInfo>(HospitalInfoCollection coll)
		{
			List<HospitalInfo> list = new List<HospitalInfo>();
			
			foreach (HospitalInfo emp in coll)
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
				return  HospitalInfoMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HospitalInfoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new HospitalInfo(row);
		}

		override protected esEntity CreateEntity()
		{
			return new HospitalInfo();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public HospitalInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HospitalInfoQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(HospitalInfoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public HospitalInfo AddNew()
		{
			HospitalInfo entity = base.AddNewEntity() as HospitalInfo;
			
			return entity;
		}

		public HospitalInfo FindByPrimaryKey(System.Int32 hospitalInfoID)
		{
			return base.FindByPrimaryKey(hospitalInfoID) as HospitalInfo;
		}


		#region IEnumerable<HospitalInfo> Members

		IEnumerator<HospitalInfo> IEnumerable<HospitalInfo>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as HospitalInfo;
			}
		}

		#endregion
		
		private HospitalInfoQuery query;
	}


	/// <summary>
	/// Encapsulates the 'HospitalInfo' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("HospitalInfo ({HospitalInfoID})")]
	[Serializable]
	public partial class HospitalInfo : esHospitalInfo
	{
		public HospitalInfo()
		{

		}
	
		public HospitalInfo(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return HospitalInfoMetadata.Meta();
			}
		}
		
		
		
		override protected esHospitalInfoQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HospitalInfoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public HospitalInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HospitalInfoQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(HospitalInfoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private HospitalInfoQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class HospitalInfoQuery : esHospitalInfoQuery
	{
		public HospitalInfoQuery()
		{

		}		
		
		public HospitalInfoQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "HospitalInfoQuery";
        }
		
			
	}


	[Serializable]
	public partial class HospitalInfoMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected HospitalInfoMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(HospitalInfoMetadata.ColumnNames.HospitalInfoID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = HospitalInfoMetadata.PropertyNames.HospitalInfoID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(HospitalInfoMetadata.ColumnNames.HospitalName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = HospitalInfoMetadata.PropertyNames.HospitalName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);
				
			c = new esColumnMetadata(HospitalInfoMetadata.ColumnNames.Address, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = HospitalInfoMetadata.PropertyNames.Address;
			c.CharacterMaxLength = 500;
			_columns.Add(c);
				
			c = new esColumnMetadata(HospitalInfoMetadata.ColumnNames.SRState, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = HospitalInfoMetadata.PropertyNames.SRState;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(HospitalInfoMetadata.ColumnNames.SRCity, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = HospitalInfoMetadata.PropertyNames.SRCity;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HospitalInfoMetadata.ColumnNames.ZipCode, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = HospitalInfoMetadata.PropertyNames.ZipCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HospitalInfoMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = HospitalInfoMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(HospitalInfoMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = HospitalInfoMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public HospitalInfoMetadata Meta()
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
			 public const string HospitalInfoID = "HospitalInfoID";
			 public const string HospitalName = "HospitalName";
			 public const string Address = "Address";
			 public const string SRState = "SRState";
			 public const string SRCity = "SRCity";
			 public const string ZipCode = "ZipCode";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string HospitalInfoID = "HospitalInfoID";
			 public const string HospitalName = "HospitalName";
			 public const string Address = "Address";
			 public const string SRState = "SRState";
			 public const string SRCity = "SRCity";
			 public const string ZipCode = "ZipCode";
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
			lock (typeof(HospitalInfoMetadata))
			{
				if(HospitalInfoMetadata.mapDelegates == null)
				{
					HospitalInfoMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (HospitalInfoMetadata.meta == null)
				{
					HospitalInfoMetadata.meta = new HospitalInfoMetadata();
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
				

				meta.AddTypeMap("HospitalInfoID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("HospitalName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Address", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRState", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCity", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ZipCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "HospitalInfo";
				meta.Destination = "HospitalInfo";
				
				meta.spInsert = "proc_HospitalInfoInsert";				
				meta.spUpdate = "proc_HospitalInfoUpdate";		
				meta.spDelete = "proc_HospitalInfoDelete";
				meta.spLoadAll = "proc_HospitalInfoLoadAll";
				meta.spLoadByPrimaryKey = "proc_HospitalInfoLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private HospitalInfoMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
