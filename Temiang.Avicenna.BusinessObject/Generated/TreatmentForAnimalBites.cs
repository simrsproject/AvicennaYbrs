/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/26/2015 3:38:58 PM
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
	abstract public class esTreatmentForAnimalBitesCollection : esEntityCollectionWAuditLog
	{
		public esTreatmentForAnimalBitesCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TreatmentForAnimalBitesCollection";
		}

		#region Query Logic
		protected void InitQuery(esTreatmentForAnimalBitesQuery query)
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
			this.InitQuery(query as esTreatmentForAnimalBitesQuery);
		}
		#endregion
		
		virtual public TreatmentForAnimalBites DetachEntity(TreatmentForAnimalBites entity)
		{
			return base.DetachEntity(entity) as TreatmentForAnimalBites;
		}
		
		virtual public TreatmentForAnimalBites AttachEntity(TreatmentForAnimalBites entity)
		{
			return base.AttachEntity(entity) as TreatmentForAnimalBites;
		}
		
		virtual public void Combine(TreatmentForAnimalBitesCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TreatmentForAnimalBites this[int index]
		{
			get
			{
				return base[index] as TreatmentForAnimalBites;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TreatmentForAnimalBites);
		}
	}



	[Serializable]
	abstract public class esTreatmentForAnimalBites : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTreatmentForAnimalBitesQuery GetDynamicQuery()
		{
			return null;
		}

		public esTreatmentForAnimalBites()
		{

		}

		public esTreatmentForAnimalBites(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationNo, System.String sRTreatmentForAnimalBites)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, sRTreatmentForAnimalBites);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, sRTreatmentForAnimalBites);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.String sRTreatmentForAnimalBites)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, sRTreatmentForAnimalBites);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, sRTreatmentForAnimalBites);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.String sRTreatmentForAnimalBites)
		{
			esTreatmentForAnimalBitesQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.SRTreatmentForAnimalBites == sRTreatmentForAnimalBites);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.String sRTreatmentForAnimalBites)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);			parms.Add("SRTreatmentForAnimalBites",sRTreatmentForAnimalBites);
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
						case "SRTreatmentForAnimalBites": this.str.SRTreatmentForAnimalBites = (string)value; break;							
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
		/// Maps to TreatmentForAnimalBites.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(TreatmentForAnimalBitesMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(TreatmentForAnimalBitesMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TreatmentForAnimalBites.SRTreatmentForAnimalBites
		/// </summary>
		virtual public System.String SRTreatmentForAnimalBites
		{
			get
			{
				return base.GetSystemString(TreatmentForAnimalBitesMetadata.ColumnNames.SRTreatmentForAnimalBites);
			}
			
			set
			{
				base.SetSystemString(TreatmentForAnimalBitesMetadata.ColumnNames.SRTreatmentForAnimalBites, value);
			}
		}
		
		/// <summary>
		/// Maps to TreatmentForAnimalBites.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TreatmentForAnimalBitesMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TreatmentForAnimalBitesMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to TreatmentForAnimalBites.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TreatmentForAnimalBitesMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(TreatmentForAnimalBitesMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esTreatmentForAnimalBites entity)
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
				
			public System.String SRTreatmentForAnimalBites
			{
				get
				{
					System.String data = entity.SRTreatmentForAnimalBites;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTreatmentForAnimalBites = null;
					else entity.SRTreatmentForAnimalBites = Convert.ToString(value);
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
			

			private esTreatmentForAnimalBites entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTreatmentForAnimalBitesQuery query)
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
				throw new Exception("esTreatmentForAnimalBites can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class TreatmentForAnimalBites : esTreatmentForAnimalBites
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
	abstract public class esTreatmentForAnimalBitesQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TreatmentForAnimalBitesMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, TreatmentForAnimalBitesMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SRTreatmentForAnimalBites
		{
			get
			{
				return new esQueryItem(this, TreatmentForAnimalBitesMetadata.ColumnNames.SRTreatmentForAnimalBites, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TreatmentForAnimalBitesMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TreatmentForAnimalBitesMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TreatmentForAnimalBitesCollection")]
	public partial class TreatmentForAnimalBitesCollection : esTreatmentForAnimalBitesCollection, IEnumerable<TreatmentForAnimalBites>
	{
		public TreatmentForAnimalBitesCollection()
		{

		}
		
		public static implicit operator List<TreatmentForAnimalBites>(TreatmentForAnimalBitesCollection coll)
		{
			List<TreatmentForAnimalBites> list = new List<TreatmentForAnimalBites>();
			
			foreach (TreatmentForAnimalBites emp in coll)
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
				return  TreatmentForAnimalBitesMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TreatmentForAnimalBitesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TreatmentForAnimalBites(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TreatmentForAnimalBites();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TreatmentForAnimalBitesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TreatmentForAnimalBitesQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TreatmentForAnimalBitesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public TreatmentForAnimalBites AddNew()
		{
			TreatmentForAnimalBites entity = base.AddNewEntity() as TreatmentForAnimalBites;
			
			return entity;
		}

		public TreatmentForAnimalBites FindByPrimaryKey(System.String registrationNo, System.String sRTreatmentForAnimalBites)
		{
			return base.FindByPrimaryKey(registrationNo, sRTreatmentForAnimalBites) as TreatmentForAnimalBites;
		}


		#region IEnumerable<TreatmentForAnimalBites> Members

		IEnumerator<TreatmentForAnimalBites> IEnumerable<TreatmentForAnimalBites>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TreatmentForAnimalBites;
			}
		}

		#endregion
		
		private TreatmentForAnimalBitesQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TreatmentForAnimalBites' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("TreatmentForAnimalBites ({RegistrationNo},{SRTreatmentForAnimalBites})")]
	[Serializable]
	public partial class TreatmentForAnimalBites : esTreatmentForAnimalBites
	{
		public TreatmentForAnimalBites()
		{

		}
	
		public TreatmentForAnimalBites(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TreatmentForAnimalBitesMetadata.Meta();
			}
		}
		
		
		
		override protected esTreatmentForAnimalBitesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TreatmentForAnimalBitesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TreatmentForAnimalBitesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TreatmentForAnimalBitesQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TreatmentForAnimalBitesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TreatmentForAnimalBitesQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TreatmentForAnimalBitesQuery : esTreatmentForAnimalBitesQuery
	{
		public TreatmentForAnimalBitesQuery()
		{

		}		
		
		public TreatmentForAnimalBitesQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TreatmentForAnimalBitesQuery";
        }
		
			
	}


	[Serializable]
	public partial class TreatmentForAnimalBitesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TreatmentForAnimalBitesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TreatmentForAnimalBitesMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TreatmentForAnimalBitesMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TreatmentForAnimalBitesMetadata.ColumnNames.SRTreatmentForAnimalBites, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TreatmentForAnimalBitesMetadata.PropertyNames.SRTreatmentForAnimalBites;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TreatmentForAnimalBitesMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TreatmentForAnimalBitesMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TreatmentForAnimalBitesMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TreatmentForAnimalBitesMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TreatmentForAnimalBitesMetadata Meta()
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
			 public const string SRTreatmentForAnimalBites = "SRTreatmentForAnimalBites";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string SRTreatmentForAnimalBites = "SRTreatmentForAnimalBites";
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
			lock (typeof(TreatmentForAnimalBitesMetadata))
			{
				if(TreatmentForAnimalBitesMetadata.mapDelegates == null)
				{
					TreatmentForAnimalBitesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TreatmentForAnimalBitesMetadata.meta == null)
				{
					TreatmentForAnimalBitesMetadata.meta = new TreatmentForAnimalBitesMetadata();
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
				meta.AddTypeMap("SRTreatmentForAnimalBites", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "TreatmentForAnimalBites";
				meta.Destination = "TreatmentForAnimalBites";
				
				meta.spInsert = "proc_TreatmentForAnimalBitesInsert";				
				meta.spUpdate = "proc_TreatmentForAnimalBitesUpdate";		
				meta.spDelete = "proc_TreatmentForAnimalBitesDelete";
				meta.spLoadAll = "proc_TreatmentForAnimalBitesLoadAll";
				meta.spLoadByPrimaryKey = "proc_TreatmentForAnimalBitesLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TreatmentForAnimalBitesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
