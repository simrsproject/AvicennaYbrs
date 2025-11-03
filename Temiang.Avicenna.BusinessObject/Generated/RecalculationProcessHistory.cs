/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/27/2012 12:53:40 PM
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
	abstract public class esRecalculationProcessHistoryCollection : esEntityCollectionWAuditLog
	{
		public esRecalculationProcessHistoryCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RecalculationProcessHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esRecalculationProcessHistoryQuery query)
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
			this.InitQuery(query as esRecalculationProcessHistoryQuery);
		}
		#endregion
		
		virtual public RecalculationProcessHistory DetachEntity(RecalculationProcessHistory entity)
		{
			return base.DetachEntity(entity) as RecalculationProcessHistory;
		}
		
		virtual public RecalculationProcessHistory AttachEntity(RecalculationProcessHistory entity)
		{
			return base.AttachEntity(entity) as RecalculationProcessHistory;
		}
		
		virtual public void Combine(RecalculationProcessHistoryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RecalculationProcessHistory this[int index]
		{
			get
			{
				return base[index] as RecalculationProcessHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RecalculationProcessHistory);
		}
	}



	[Serializable]
	abstract public class esRecalculationProcessHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRecalculationProcessHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esRecalculationProcessHistory()
		{

		}

		public esRecalculationProcessHistory(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String recalculationProcessNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(recalculationProcessNo);
			else
				return LoadByPrimaryKeyStoredProcedure(recalculationProcessNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String recalculationProcessNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(recalculationProcessNo);
			else
				return LoadByPrimaryKeyStoredProcedure(recalculationProcessNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String recalculationProcessNo)
		{
			esRecalculationProcessHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.RecalculationProcessNo == recalculationProcessNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String recalculationProcessNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RecalculationProcessNo",recalculationProcessNo);
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
						case "RecalculationProcessNo": this.str.RecalculationProcessNo = (string)value; break;							
						case "RecalculationProcessDate": this.str.RecalculationProcessDate = (string)value; break;							
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
						case "RecalculationProcessDate":
						
							if (value == null || value is System.DateTime)
								this.RecalculationProcessDate = (System.DateTime?)value;
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
		/// Maps to RecalculationProcessHistory.RecalculationProcessNo
		/// </summary>
		virtual public System.String RecalculationProcessNo
		{
			get
			{
				return base.GetSystemString(RecalculationProcessHistoryMetadata.ColumnNames.RecalculationProcessNo);
			}
			
			set
			{
				base.SetSystemString(RecalculationProcessHistoryMetadata.ColumnNames.RecalculationProcessNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RecalculationProcessHistory.RecalculationProcessDate
		/// </summary>
		virtual public System.DateTime? RecalculationProcessDate
		{
			get
			{
				return base.GetSystemDateTime(RecalculationProcessHistoryMetadata.ColumnNames.RecalculationProcessDate);
			}
			
			set
			{
				base.SetSystemDateTime(RecalculationProcessHistoryMetadata.ColumnNames.RecalculationProcessDate, value);
			}
		}
		
		/// <summary>
		/// Maps to RecalculationProcessHistory.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RecalculationProcessHistoryMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(RecalculationProcessHistoryMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RecalculationProcessHistory.FromGuarantorID
		/// </summary>
		virtual public System.String FromGuarantorID
		{
			get
			{
				return base.GetSystemString(RecalculationProcessHistoryMetadata.ColumnNames.FromGuarantorID);
			}
			
			set
			{
				base.SetSystemString(RecalculationProcessHistoryMetadata.ColumnNames.FromGuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to RecalculationProcessHistory.ToGuarantorID
		/// </summary>
		virtual public System.String ToGuarantorID
		{
			get
			{
				return base.GetSystemString(RecalculationProcessHistoryMetadata.ColumnNames.ToGuarantorID);
			}
			
			set
			{
				base.SetSystemString(RecalculationProcessHistoryMetadata.ColumnNames.ToGuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to RecalculationProcessHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RecalculationProcessHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RecalculationProcessHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RecalculationProcessHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RecalculationProcessHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RecalculationProcessHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRecalculationProcessHistory entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RecalculationProcessNo
			{
				get
				{
					System.String data = entity.RecalculationProcessNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecalculationProcessNo = null;
					else entity.RecalculationProcessNo = Convert.ToString(value);
				}
			}
				
			public System.String RecalculationProcessDate
			{
				get
				{
					System.DateTime? data = entity.RecalculationProcessDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecalculationProcessDate = null;
					else entity.RecalculationProcessDate = Convert.ToDateTime(value);
				}
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
			

			private esRecalculationProcessHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRecalculationProcessHistoryQuery query)
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
				throw new Exception("esRecalculationProcessHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RecalculationProcessHistory : esRecalculationProcessHistory
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
	abstract public class esRecalculationProcessHistoryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RecalculationProcessHistoryMetadata.Meta();
			}
		}	
		

		public esQueryItem RecalculationProcessNo
		{
			get
			{
				return new esQueryItem(this, RecalculationProcessHistoryMetadata.ColumnNames.RecalculationProcessNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RecalculationProcessDate
		{
			get
			{
				return new esQueryItem(this, RecalculationProcessHistoryMetadata.ColumnNames.RecalculationProcessDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RecalculationProcessHistoryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem FromGuarantorID
		{
			get
			{
				return new esQueryItem(this, RecalculationProcessHistoryMetadata.ColumnNames.FromGuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem ToGuarantorID
		{
			get
			{
				return new esQueryItem(this, RecalculationProcessHistoryMetadata.ColumnNames.ToGuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RecalculationProcessHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RecalculationProcessHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RecalculationProcessHistoryCollection")]
	public partial class RecalculationProcessHistoryCollection : esRecalculationProcessHistoryCollection, IEnumerable<RecalculationProcessHistory>
	{
		public RecalculationProcessHistoryCollection()
		{

		}
		
		public static implicit operator List<RecalculationProcessHistory>(RecalculationProcessHistoryCollection coll)
		{
			List<RecalculationProcessHistory> list = new List<RecalculationProcessHistory>();
			
			foreach (RecalculationProcessHistory emp in coll)
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
				return  RecalculationProcessHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RecalculationProcessHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RecalculationProcessHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RecalculationProcessHistory();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RecalculationProcessHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RecalculationProcessHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RecalculationProcessHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RecalculationProcessHistory AddNew()
		{
			RecalculationProcessHistory entity = base.AddNewEntity() as RecalculationProcessHistory;
			
			return entity;
		}

		public RecalculationProcessHistory FindByPrimaryKey(System.String recalculationProcessNo)
		{
			return base.FindByPrimaryKey(recalculationProcessNo) as RecalculationProcessHistory;
		}


		#region IEnumerable<RecalculationProcessHistory> Members

		IEnumerator<RecalculationProcessHistory> IEnumerable<RecalculationProcessHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RecalculationProcessHistory;
			}
		}

		#endregion
		
		private RecalculationProcessHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RecalculationProcessHistory' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RecalculationProcessHistory ({RecalculationProcessNo})")]
	[Serializable]
	public partial class RecalculationProcessHistory : esRecalculationProcessHistory
	{
		public RecalculationProcessHistory()
		{

		}
	
		public RecalculationProcessHistory(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RecalculationProcessHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esRecalculationProcessHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RecalculationProcessHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RecalculationProcessHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RecalculationProcessHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RecalculationProcessHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RecalculationProcessHistoryQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RecalculationProcessHistoryQuery : esRecalculationProcessHistoryQuery
	{
		public RecalculationProcessHistoryQuery()
		{

		}		
		
		public RecalculationProcessHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RecalculationProcessHistoryQuery";
        }
		
			
	}


	[Serializable]
	public partial class RecalculationProcessHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RecalculationProcessHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RecalculationProcessHistoryMetadata.ColumnNames.RecalculationProcessNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RecalculationProcessHistoryMetadata.PropertyNames.RecalculationProcessNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecalculationProcessHistoryMetadata.ColumnNames.RecalculationProcessDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RecalculationProcessHistoryMetadata.PropertyNames.RecalculationProcessDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecalculationProcessHistoryMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RecalculationProcessHistoryMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecalculationProcessHistoryMetadata.ColumnNames.FromGuarantorID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RecalculationProcessHistoryMetadata.PropertyNames.FromGuarantorID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecalculationProcessHistoryMetadata.ColumnNames.ToGuarantorID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RecalculationProcessHistoryMetadata.PropertyNames.ToGuarantorID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecalculationProcessHistoryMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RecalculationProcessHistoryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecalculationProcessHistoryMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RecalculationProcessHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RecalculationProcessHistoryMetadata Meta()
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
			 public const string RecalculationProcessNo = "RecalculationProcessNo";
			 public const string RecalculationProcessDate = "RecalculationProcessDate";
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
			 public const string RecalculationProcessNo = "RecalculationProcessNo";
			 public const string RecalculationProcessDate = "RecalculationProcessDate";
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
			lock (typeof(RecalculationProcessHistoryMetadata))
			{
				if(RecalculationProcessHistoryMetadata.mapDelegates == null)
				{
					RecalculationProcessHistoryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RecalculationProcessHistoryMetadata.meta == null)
				{
					RecalculationProcessHistoryMetadata.meta = new RecalculationProcessHistoryMetadata();
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
				

				meta.AddTypeMap("RecalculationProcessNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RecalculationProcessDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromGuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToGuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RecalculationProcessHistory";
				meta.Destination = "RecalculationProcessHistory";
				
				meta.spInsert = "proc_RecalculationProcessHistoryInsert";				
				meta.spUpdate = "proc_RecalculationProcessHistoryUpdate";		
				meta.spDelete = "proc_RecalculationProcessHistoryDelete";
				meta.spLoadAll = "proc_RecalculationProcessHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_RecalculationProcessHistoryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RecalculationProcessHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
