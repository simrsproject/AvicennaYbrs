/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/5/2014 1:01:53 PM
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
	abstract public class esBirthAttendantsRecordCollection : esEntityCollectionWAuditLog
	{
		public esBirthAttendantsRecordCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "BirthAttendantsRecordCollection";
		}

		#region Query Logic
		protected void InitQuery(esBirthAttendantsRecordQuery query)
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
			this.InitQuery(query as esBirthAttendantsRecordQuery);
		}
		#endregion
		
		virtual public BirthAttendantsRecord DetachEntity(BirthAttendantsRecord entity)
		{
			return base.DetachEntity(entity) as BirthAttendantsRecord;
		}
		
		virtual public BirthAttendantsRecord AttachEntity(BirthAttendantsRecord entity)
		{
			return base.AttachEntity(entity) as BirthAttendantsRecord;
		}
		
		virtual public void Combine(BirthAttendantsRecordCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BirthAttendantsRecord this[int index]
		{
			get
			{
				return base[index] as BirthAttendantsRecord;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BirthAttendantsRecord);
		}
	}



	[Serializable]
	abstract public class esBirthAttendantsRecord : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBirthAttendantsRecordQuery GetDynamicQuery()
		{
			return null;
		}

		public esBirthAttendantsRecord()
		{

		}

		public esBirthAttendantsRecord(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationNo, System.String paramedicID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, paramedicID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, paramedicID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.String paramedicID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, paramedicID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, paramedicID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.String paramedicID)
		{
			esBirthAttendantsRecordQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.ParamedicID == paramedicID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.String paramedicID)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);			parms.Add("ParamedicID",paramedicID);
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
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "SRMidwivesType": this.str.SRMidwivesType = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
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
		/// Maps to BirthAttendantsRecord.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(BirthAttendantsRecordMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(BirthAttendantsRecordMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to BirthAttendantsRecord.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(BirthAttendantsRecordMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(BirthAttendantsRecordMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to BirthAttendantsRecord.SRMidwivesType
		/// </summary>
		virtual public System.String SRMidwivesType
		{
			get
			{
				return base.GetSystemString(BirthAttendantsRecordMetadata.ColumnNames.SRMidwivesType);
			}
			
			set
			{
				base.SetSystemString(BirthAttendantsRecordMetadata.ColumnNames.SRMidwivesType, value);
			}
		}
		
		/// <summary>
		/// Maps to BirthAttendantsRecord.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(BirthAttendantsRecordMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(BirthAttendantsRecordMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to BirthAttendantsRecord.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BirthAttendantsRecordMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BirthAttendantsRecordMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to BirthAttendantsRecord.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BirthAttendantsRecordMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BirthAttendantsRecordMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esBirthAttendantsRecord entity)
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
				
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
				}
			}
				
			public System.String SRMidwivesType
			{
				get
				{
					System.String data = entity.SRMidwivesType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMidwivesType = null;
					else entity.SRMidwivesType = Convert.ToString(value);
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
			

			private esBirthAttendantsRecord entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBirthAttendantsRecordQuery query)
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
				throw new Exception("esBirthAttendantsRecord can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class BirthAttendantsRecord : esBirthAttendantsRecord
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
	abstract public class esBirthAttendantsRecordQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return BirthAttendantsRecordMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, BirthAttendantsRecordMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, BirthAttendantsRecordMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRMidwivesType
		{
			get
			{
				return new esQueryItem(this, BirthAttendantsRecordMetadata.ColumnNames.SRMidwivesType, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, BirthAttendantsRecordMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BirthAttendantsRecordMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BirthAttendantsRecordMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BirthAttendantsRecordCollection")]
	public partial class BirthAttendantsRecordCollection : esBirthAttendantsRecordCollection, IEnumerable<BirthAttendantsRecord>
	{
		public BirthAttendantsRecordCollection()
		{

		}
		
		public static implicit operator List<BirthAttendantsRecord>(BirthAttendantsRecordCollection coll)
		{
			List<BirthAttendantsRecord> list = new List<BirthAttendantsRecord>();
			
			foreach (BirthAttendantsRecord emp in coll)
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
				return  BirthAttendantsRecordMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BirthAttendantsRecordQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BirthAttendantsRecord(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BirthAttendantsRecord();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public BirthAttendantsRecordQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BirthAttendantsRecordQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(BirthAttendantsRecordQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public BirthAttendantsRecord AddNew()
		{
			BirthAttendantsRecord entity = base.AddNewEntity() as BirthAttendantsRecord;
			
			return entity;
		}

		public BirthAttendantsRecord FindByPrimaryKey(System.String registrationNo, System.String paramedicID)
		{
			return base.FindByPrimaryKey(registrationNo, paramedicID) as BirthAttendantsRecord;
		}


		#region IEnumerable<BirthAttendantsRecord> Members

		IEnumerator<BirthAttendantsRecord> IEnumerable<BirthAttendantsRecord>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BirthAttendantsRecord;
			}
		}

		#endregion
		
		private BirthAttendantsRecordQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BirthAttendantsRecord' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("BirthAttendantsRecord ({RegistrationNo},{ParamedicID})")]
	[Serializable]
	public partial class BirthAttendantsRecord : esBirthAttendantsRecord
	{
		public BirthAttendantsRecord()
		{

		}
	
		public BirthAttendantsRecord(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BirthAttendantsRecordMetadata.Meta();
			}
		}
		
		
		
		override protected esBirthAttendantsRecordQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BirthAttendantsRecordQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public BirthAttendantsRecordQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BirthAttendantsRecordQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(BirthAttendantsRecordQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private BirthAttendantsRecordQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class BirthAttendantsRecordQuery : esBirthAttendantsRecordQuery
	{
		public BirthAttendantsRecordQuery()
		{

		}		
		
		public BirthAttendantsRecordQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "BirthAttendantsRecordQuery";
        }
		
			
	}


	[Serializable]
	public partial class BirthAttendantsRecordMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BirthAttendantsRecordMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BirthAttendantsRecordMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = BirthAttendantsRecordMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(BirthAttendantsRecordMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = BirthAttendantsRecordMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(BirthAttendantsRecordMetadata.ColumnNames.SRMidwivesType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = BirthAttendantsRecordMetadata.PropertyNames.SRMidwivesType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BirthAttendantsRecordMetadata.ColumnNames.Notes, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BirthAttendantsRecordMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BirthAttendantsRecordMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BirthAttendantsRecordMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BirthAttendantsRecordMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = BirthAttendantsRecordMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public BirthAttendantsRecordMetadata Meta()
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
			 public const string ParamedicID = "ParamedicID";
			 public const string SRMidwivesType = "SRMidwivesType";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string ParamedicID = "ParamedicID";
			 public const string SRMidwivesType = "SRMidwivesType";
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
			lock (typeof(BirthAttendantsRecordMetadata))
			{
				if(BirthAttendantsRecordMetadata.mapDelegates == null)
				{
					BirthAttendantsRecordMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BirthAttendantsRecordMetadata.meta == null)
				{
					BirthAttendantsRecordMetadata.meta = new BirthAttendantsRecordMetadata();
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
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMidwivesType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "BirthAttendantsRecord";
				meta.Destination = "BirthAttendantsRecord";
				
				meta.spInsert = "proc_BirthAttendantsRecordInsert";				
				meta.spUpdate = "proc_BirthAttendantsRecordUpdate";		
				meta.spDelete = "proc_BirthAttendantsRecordDelete";
				meta.spLoadAll = "proc_BirthAttendantsRecordLoadAll";
				meta.spLoadByPrimaryKey = "proc_BirthAttendantsRecordLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BirthAttendantsRecordMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
