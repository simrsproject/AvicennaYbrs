/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/20/2014 11:15:55 AM
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
	abstract public class esRegistrationGuarantorHistoryCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationGuarantorHistoryCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RegistrationGuarantorHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationGuarantorHistoryQuery query)
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
			this.InitQuery(query as esRegistrationGuarantorHistoryQuery);
		}
		#endregion
		
		virtual public RegistrationGuarantorHistory DetachEntity(RegistrationGuarantorHistory entity)
		{
			return base.DetachEntity(entity) as RegistrationGuarantorHistory;
		}
		
		virtual public RegistrationGuarantorHistory AttachEntity(RegistrationGuarantorHistory entity)
		{
			return base.AttachEntity(entity) as RegistrationGuarantorHistory;
		}
		
		virtual public void Combine(RegistrationGuarantorHistoryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RegistrationGuarantorHistory this[int index]
		{
			get
			{
				return base[index] as RegistrationGuarantorHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationGuarantorHistory);
		}
	}



	[Serializable]
	abstract public class esRegistrationGuarantorHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationGuarantorHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationGuarantorHistory()
		{

		}

		public esRegistrationGuarantorHistory(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationNo, System.String fromGuarantorID, System.String toGuarantorID, System.DateTime lastUpdateDateTime)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, fromGuarantorID, toGuarantorID, lastUpdateDateTime);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, fromGuarantorID, toGuarantorID, lastUpdateDateTime);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.String fromGuarantorID, System.String toGuarantorID, System.DateTime lastUpdateDateTime)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, fromGuarantorID, toGuarantorID, lastUpdateDateTime);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, fromGuarantorID, toGuarantorID, lastUpdateDateTime);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.String fromGuarantorID, System.String toGuarantorID, System.DateTime lastUpdateDateTime)
		{
			esRegistrationGuarantorHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.FromGuarantorID == fromGuarantorID, query.ToGuarantorID == toGuarantorID, query.LastUpdateDateTime == lastUpdateDateTime);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.String fromGuarantorID, System.String toGuarantorID, System.DateTime lastUpdateDateTime)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);			parms.Add("FromGuarantorID",fromGuarantorID);			parms.Add("ToGuarantorID",toGuarantorID);			parms.Add("LastUpdateDateTime",lastUpdateDateTime);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "FromGuarantorID": this.str.FromGuarantorID = (string)value; break;							
						case "ToGuarantorID": this.str.ToGuarantorID = (string)value; break;							
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
		/// Maps to RegistrationGuarantorHistory.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationGuarantorHistoryMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationGuarantorHistoryMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationGuarantorHistory.FromGuarantorID
		/// </summary>
		virtual public System.String FromGuarantorID
		{
			get
			{
				return base.GetSystemString(RegistrationGuarantorHistoryMetadata.ColumnNames.FromGuarantorID);
			}
			
			set
			{
				base.SetSystemString(RegistrationGuarantorHistoryMetadata.ColumnNames.FromGuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationGuarantorHistory.ToGuarantorID
		/// </summary>
		virtual public System.String ToGuarantorID
		{
			get
			{
				return base.GetSystemString(RegistrationGuarantorHistoryMetadata.ColumnNames.ToGuarantorID);
			}
			
			set
			{
				base.SetSystemString(RegistrationGuarantorHistoryMetadata.ColumnNames.ToGuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationGuarantorHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationGuarantorHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationGuarantorHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationGuarantorHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationGuarantorHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationGuarantorHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRegistrationGuarantorHistory entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
				
			public System.String FromGuarantorID
			{
				get
				{
					System.String data = entity.FromGuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromGuarantorID = null;
					else entity.FromGuarantorID = Convert.ToString(value);
				}
			}
				
			public System.String ToGuarantorID
			{
				get
				{
					System.String data = entity.ToGuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToGuarantorID = null;
					else entity.ToGuarantorID = Convert.ToString(value);
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
			

			private esRegistrationGuarantorHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationGuarantorHistoryQuery query)
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
				throw new Exception("esRegistrationGuarantorHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RegistrationGuarantorHistory : esRegistrationGuarantorHistory
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
	abstract public class esRegistrationGuarantorHistoryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationGuarantorHistoryMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationGuarantorHistoryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem FromGuarantorID
		{
			get
			{
				return new esQueryItem(this, RegistrationGuarantorHistoryMetadata.ColumnNames.FromGuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem ToGuarantorID
		{
			get
			{
				return new esQueryItem(this, RegistrationGuarantorHistoryMetadata.ColumnNames.ToGuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationGuarantorHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationGuarantorHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationGuarantorHistoryCollection")]
	public partial class RegistrationGuarantorHistoryCollection : esRegistrationGuarantorHistoryCollection, IEnumerable<RegistrationGuarantorHistory>
	{
		public RegistrationGuarantorHistoryCollection()
		{

		}
		
		public static implicit operator List<RegistrationGuarantorHistory>(RegistrationGuarantorHistoryCollection coll)
		{
			List<RegistrationGuarantorHistory> list = new List<RegistrationGuarantorHistory>();
			
			foreach (RegistrationGuarantorHistory emp in coll)
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
				return  RegistrationGuarantorHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationGuarantorHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationGuarantorHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationGuarantorHistory();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RegistrationGuarantorHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationGuarantorHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RegistrationGuarantorHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RegistrationGuarantorHistory AddNew()
		{
			RegistrationGuarantorHistory entity = base.AddNewEntity() as RegistrationGuarantorHistory;
			
			return entity;
		}

		public RegistrationGuarantorHistory FindByPrimaryKey(System.String registrationNo, System.String fromGuarantorID, System.String toGuarantorID, System.DateTime lastUpdateDateTime)
		{
			return base.FindByPrimaryKey(registrationNo, fromGuarantorID, toGuarantorID, lastUpdateDateTime) as RegistrationGuarantorHistory;
		}


		#region IEnumerable<RegistrationGuarantorHistory> Members

		IEnumerator<RegistrationGuarantorHistory> IEnumerable<RegistrationGuarantorHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationGuarantorHistory;
			}
		}

		#endregion
		
		private RegistrationGuarantorHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationGuarantorHistory' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RegistrationGuarantorHistory ({RegistrationNo},{FromGuarantorID},{ToGuarantorID},{LastUpdateDateTime})")]
	[Serializable]
	public partial class RegistrationGuarantorHistory : esRegistrationGuarantorHistory
	{
		public RegistrationGuarantorHistory()
		{

		}
	
		public RegistrationGuarantorHistory(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationGuarantorHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esRegistrationGuarantorHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationGuarantorHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RegistrationGuarantorHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationGuarantorHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RegistrationGuarantorHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RegistrationGuarantorHistoryQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RegistrationGuarantorHistoryQuery : esRegistrationGuarantorHistoryQuery
	{
		public RegistrationGuarantorHistoryQuery()
		{

		}		
		
		public RegistrationGuarantorHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RegistrationGuarantorHistoryQuery";
        }
		
			
	}


	[Serializable]
	public partial class RegistrationGuarantorHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationGuarantorHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationGuarantorHistoryMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationGuarantorHistoryMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationGuarantorHistoryMetadata.ColumnNames.FromGuarantorID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationGuarantorHistoryMetadata.PropertyNames.FromGuarantorID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationGuarantorHistoryMetadata.ColumnNames.ToGuarantorID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationGuarantorHistoryMetadata.PropertyNames.ToGuarantorID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationGuarantorHistoryMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationGuarantorHistoryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationGuarantorHistoryMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationGuarantorHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RegistrationGuarantorHistoryMetadata Meta()
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
			 public const string RegistrationNo = "RegistrationNo";
			 public const string FromGuarantorID = "FromGuarantorID";
			 public const string ToGuarantorID = "ToGuarantorID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string FromGuarantorID = "FromGuarantorID";
			 public const string ToGuarantorID = "ToGuarantorID";
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
			lock (typeof(RegistrationGuarantorHistoryMetadata))
			{
				if(RegistrationGuarantorHistoryMetadata.mapDelegates == null)
				{
					RegistrationGuarantorHistoryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RegistrationGuarantorHistoryMetadata.meta == null)
				{
					RegistrationGuarantorHistoryMetadata.meta = new RegistrationGuarantorHistoryMetadata();
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
				

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromGuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToGuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RegistrationGuarantorHistory";
				meta.Destination = "RegistrationGuarantorHistory";
				
				meta.spInsert = "proc_RegistrationGuarantorHistoryInsert";				
				meta.spUpdate = "proc_RegistrationGuarantorHistoryUpdate";		
				meta.spDelete = "proc_RegistrationGuarantorHistoryDelete";
				meta.spLoadAll = "proc_RegistrationGuarantorHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationGuarantorHistoryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationGuarantorHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
