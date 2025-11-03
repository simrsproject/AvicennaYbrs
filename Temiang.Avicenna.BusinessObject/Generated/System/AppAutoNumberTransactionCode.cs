/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:09 PM
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
	abstract public class esAppAutoNumberTransactionCodeCollection : esEntityCollectionWAuditLog
	{
		public esAppAutoNumberTransactionCodeCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AppAutoNumberTransactionCodeCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppAutoNumberTransactionCodeQuery query)
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
			this.InitQuery(query as esAppAutoNumberTransactionCodeQuery);
		}
		#endregion
		
		virtual public AppAutoNumberTransactionCode DetachEntity(AppAutoNumberTransactionCode entity)
		{
			return base.DetachEntity(entity) as AppAutoNumberTransactionCode;
		}
		
		virtual public AppAutoNumberTransactionCode AttachEntity(AppAutoNumberTransactionCode entity)
		{
			return base.AttachEntity(entity) as AppAutoNumberTransactionCode;
		}
		
		virtual public void Combine(AppAutoNumberTransactionCodeCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AppAutoNumberTransactionCode this[int index]
		{
			get
			{
				return base[index] as AppAutoNumberTransactionCode;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppAutoNumberTransactionCode);
		}
	}



	[Serializable]
	abstract public class esAppAutoNumberTransactionCode : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppAutoNumberTransactionCodeQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppAutoNumberTransactionCode()
		{

		}

		public esAppAutoNumberTransactionCode(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String sRTransactionCode)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRTransactionCode);
			else
				return LoadByPrimaryKeyStoredProcedure(sRTransactionCode);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String sRTransactionCode)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRTransactionCode);
			else
				return LoadByPrimaryKeyStoredProcedure(sRTransactionCode);
		}

		private bool LoadByPrimaryKeyDynamic(System.String sRTransactionCode)
		{
			esAppAutoNumberTransactionCodeQuery query = this.GetDynamicQuery();
			query.Where(query.SRTransactionCode == sRTransactionCode);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String sRTransactionCode)
		{
			esParameters parms = new esParameters();
			parms.Add("SRTransactionCode",sRTransactionCode);
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
						case "SRTransactionCode": this.str.SRTransactionCode = (string)value; break;							
						case "SRAutoNumber": this.str.SRAutoNumber = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
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
		/// Maps to AppAutoNumberTransactionCode.SRTransactionCode
		/// </summary>
		virtual public System.String SRTransactionCode
		{
			get
			{
				return base.GetSystemString(AppAutoNumberTransactionCodeMetadata.ColumnNames.SRTransactionCode);
			}
			
			set
			{
				base.SetSystemString(AppAutoNumberTransactionCodeMetadata.ColumnNames.SRTransactionCode, value);
			}
		}
		
		/// <summary>
		/// Maps to AppAutoNumberTransactionCode.SRAutoNumber
		/// </summary>
		virtual public System.String SRAutoNumber
		{
			get
			{
				return base.GetSystemString(AppAutoNumberTransactionCodeMetadata.ColumnNames.SRAutoNumber);
			}
			
			set
			{
				base.SetSystemString(AppAutoNumberTransactionCodeMetadata.ColumnNames.SRAutoNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to AppAutoNumberTransactionCode.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppAutoNumberTransactionCodeMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AppAutoNumberTransactionCodeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AppAutoNumberTransactionCode.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppAutoNumberTransactionCodeMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AppAutoNumberTransactionCodeMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAppAutoNumberTransactionCode entity)
			{
				this.entity = entity;
			}
			
	
			public System.String SRTransactionCode
			{
				get
				{
					System.String data = entity.SRTransactionCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTransactionCode = null;
					else entity.SRTransactionCode = Convert.ToString(value);
				}
			}
				
			public System.String SRAutoNumber
			{
				get
				{
					System.String data = entity.SRAutoNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAutoNumber = null;
					else entity.SRAutoNumber = Convert.ToString(value);
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
			

			private esAppAutoNumberTransactionCode entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppAutoNumberTransactionCodeQuery query)
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
				throw new Exception("esAppAutoNumberTransactionCode can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AppAutoNumberTransactionCode : esAppAutoNumberTransactionCode
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
	abstract public class esAppAutoNumberTransactionCodeQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AppAutoNumberTransactionCodeMetadata.Meta();
			}
		}	
		

		public esQueryItem SRTransactionCode
		{
			get
			{
				return new esQueryItem(this, AppAutoNumberTransactionCodeMetadata.ColumnNames.SRTransactionCode, esSystemType.String);
			}
		} 
		
		public esQueryItem SRAutoNumber
		{
			get
			{
				return new esQueryItem(this, AppAutoNumberTransactionCodeMetadata.ColumnNames.SRAutoNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AppAutoNumberTransactionCodeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AppAutoNumberTransactionCodeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppAutoNumberTransactionCodeCollection")]
	public partial class AppAutoNumberTransactionCodeCollection : esAppAutoNumberTransactionCodeCollection, IEnumerable<AppAutoNumberTransactionCode>
	{
		public AppAutoNumberTransactionCodeCollection()
		{

		}
		
		public static implicit operator List<AppAutoNumberTransactionCode>(AppAutoNumberTransactionCodeCollection coll)
		{
			List<AppAutoNumberTransactionCode> list = new List<AppAutoNumberTransactionCode>();
			
			foreach (AppAutoNumberTransactionCode emp in coll)
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
				return  AppAutoNumberTransactionCodeMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppAutoNumberTransactionCodeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppAutoNumberTransactionCode(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppAutoNumberTransactionCode();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AppAutoNumberTransactionCodeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppAutoNumberTransactionCodeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AppAutoNumberTransactionCodeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AppAutoNumberTransactionCode AddNew()
		{
			AppAutoNumberTransactionCode entity = base.AddNewEntity() as AppAutoNumberTransactionCode;
			
			return entity;
		}

		public AppAutoNumberTransactionCode FindByPrimaryKey(System.String sRTransactionCode)
		{
			return base.FindByPrimaryKey(sRTransactionCode) as AppAutoNumberTransactionCode;
		}


		#region IEnumerable<AppAutoNumberTransactionCode> Members

		IEnumerator<AppAutoNumberTransactionCode> IEnumerable<AppAutoNumberTransactionCode>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AppAutoNumberTransactionCode;
			}
		}

		#endregion
		
		private AppAutoNumberTransactionCodeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppAutoNumberTransactionCode' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AppAutoNumberTransactionCode ({SRTransactionCode})")]
	[Serializable]
	public partial class AppAutoNumberTransactionCode : esAppAutoNumberTransactionCode
	{
		public AppAutoNumberTransactionCode()
		{

		}
	
		public AppAutoNumberTransactionCode(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppAutoNumberTransactionCodeMetadata.Meta();
			}
		}
		
		
		
		override protected esAppAutoNumberTransactionCodeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppAutoNumberTransactionCodeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AppAutoNumberTransactionCodeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppAutoNumberTransactionCodeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AppAutoNumberTransactionCodeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AppAutoNumberTransactionCodeQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AppAutoNumberTransactionCodeQuery : esAppAutoNumberTransactionCodeQuery
	{
		public AppAutoNumberTransactionCodeQuery()
		{

		}		
		
		public AppAutoNumberTransactionCodeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AppAutoNumberTransactionCodeQuery";
        }
		
			
	}


	[Serializable]
	public partial class AppAutoNumberTransactionCodeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppAutoNumberTransactionCodeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppAutoNumberTransactionCodeMetadata.ColumnNames.SRTransactionCode, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppAutoNumberTransactionCodeMetadata.PropertyNames.SRTransactionCode;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppAutoNumberTransactionCodeMetadata.ColumnNames.SRAutoNumber, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppAutoNumberTransactionCodeMetadata.PropertyNames.SRAutoNumber;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppAutoNumberTransactionCodeMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppAutoNumberTransactionCodeMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppAutoNumberTransactionCodeMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppAutoNumberTransactionCodeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AppAutoNumberTransactionCodeMetadata Meta()
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
			 public const string SRTransactionCode = "SRTransactionCode";
			 public const string SRAutoNumber = "SRAutoNumber";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SRTransactionCode = "SRTransactionCode";
			 public const string SRAutoNumber = "SRAutoNumber";
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
			lock (typeof(AppAutoNumberTransactionCodeMetadata))
			{
				if(AppAutoNumberTransactionCodeMetadata.mapDelegates == null)
				{
					AppAutoNumberTransactionCodeMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AppAutoNumberTransactionCodeMetadata.meta == null)
				{
					AppAutoNumberTransactionCodeMetadata.meta = new AppAutoNumberTransactionCodeMetadata();
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
				

				meta.AddTypeMap("SRTransactionCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAutoNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AppAutoNumberTransactionCode";
				meta.Destination = "AppAutoNumberTransactionCode";
				
				meta.spInsert = "proc_AppAutoNumberTransactionCodeInsert";				
				meta.spUpdate = "proc_AppAutoNumberTransactionCodeUpdate";		
				meta.spDelete = "proc_AppAutoNumberTransactionCodeDelete";
				meta.spLoadAll = "proc_AppAutoNumberTransactionCodeLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppAutoNumberTransactionCodeLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppAutoNumberTransactionCodeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
