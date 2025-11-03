/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 12/24/2014 11:33:46 AM
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
	abstract public class esIncomeJournalStatusCollection : esEntityCollection
	{
		public esIncomeJournalStatusCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "IncomeJournalStatusCollection";
		}

		#region Query Logic
		protected void InitQuery(esIncomeJournalStatusQuery query)
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
			this.InitQuery(query as esIncomeJournalStatusQuery);
		}
		#endregion
		
		virtual public IncomeJournalStatus DetachEntity(IncomeJournalStatus entity)
		{
			return base.DetachEntity(entity) as IncomeJournalStatus;
		}
		
		virtual public IncomeJournalStatus AttachEntity(IncomeJournalStatus entity)
		{
			return base.AttachEntity(entity) as IncomeJournalStatus;
		}
		
		virtual public void Combine(IncomeJournalStatusCollection collection)
		{
			base.Combine(collection);
		}
		
		new public IncomeJournalStatus this[int index]
		{
			get
			{
				return base[index] as IncomeJournalStatus;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(IncomeJournalStatus);
		}
	}



	[Serializable]
	abstract public class esIncomeJournalStatus : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esIncomeJournalStatusQuery GetDynamicQuery()
		{
			return null;
		}

		public esIncomeJournalStatus()
		{

		}

		public esIncomeJournalStatus(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 id)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 id)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 id)
		{
			esIncomeJournalStatusQuery query = this.GetDynamicQuery();
			query.Where(query.Id == id);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 id)
		{
			esParameters parms = new esParameters();
			parms.Add("Id",id);
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
						case "Id": this.str.Id = (string)value; break;							
						case "Month": this.str.Month = (string)value; break;							
						case "Year": this.str.Year = (string)value; break;							
						case "CreatedBy": this.str.CreatedBy = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "DateCreated": this.str.DateCreated = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "JournalId": this.str.JournalId = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
							break;
						
						case "DateCreated":
						
							if (value == null || value is System.DateTime)
								this.DateCreated = (System.DateTime?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "JournalId":
						
							if (value == null || value is System.Int32)
								this.JournalId = (System.Int32?)value;
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
		/// Maps to IncomeJournalStatus.Id
		/// </summary>
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(IncomeJournalStatusMetadata.ColumnNames.Id);
			}
			
			set
			{
				base.SetSystemInt32(IncomeJournalStatusMetadata.ColumnNames.Id, value);
			}
		}
		
		/// <summary>
		/// Maps to IncomeJournalStatus.Month
		/// </summary>
		virtual public System.String Month
		{
			get
			{
				return base.GetSystemString(IncomeJournalStatusMetadata.ColumnNames.Month);
			}
			
			set
			{
				base.SetSystemString(IncomeJournalStatusMetadata.ColumnNames.Month, value);
			}
		}
		
		/// <summary>
		/// Maps to IncomeJournalStatus.Year
		/// </summary>
		virtual public System.String Year
		{
			get
			{
				return base.GetSystemString(IncomeJournalStatusMetadata.ColumnNames.Year);
			}
			
			set
			{
				base.SetSystemString(IncomeJournalStatusMetadata.ColumnNames.Year, value);
			}
		}
		
		/// <summary>
		/// Maps to IncomeJournalStatus.CreatedBy
		/// </summary>
		virtual public System.String CreatedBy
		{
			get
			{
				return base.GetSystemString(IncomeJournalStatusMetadata.ColumnNames.CreatedBy);
			}
			
			set
			{
				base.SetSystemString(IncomeJournalStatusMetadata.ColumnNames.CreatedBy, value);
			}
		}
		
		/// <summary>
		/// Maps to IncomeJournalStatus.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(IncomeJournalStatusMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(IncomeJournalStatusMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to IncomeJournalStatus.DateCreated
		/// </summary>
		virtual public System.DateTime? DateCreated
		{
			get
			{
				return base.GetSystemDateTime(IncomeJournalStatusMetadata.ColumnNames.DateCreated);
			}
			
			set
			{
				base.SetSystemDateTime(IncomeJournalStatusMetadata.ColumnNames.DateCreated, value);
			}
		}
		
		/// <summary>
		/// Maps to IncomeJournalStatus.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(IncomeJournalStatusMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(IncomeJournalStatusMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to IncomeJournalStatus.JournalId
		/// </summary>
		virtual public System.Int32? JournalId
		{
			get
			{
				return base.GetSystemInt32(IncomeJournalStatusMetadata.ColumnNames.JournalId);
			}
			
			set
			{
				base.SetSystemInt32(IncomeJournalStatusMetadata.ColumnNames.JournalId, value);
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
			public esStrings(esIncomeJournalStatus entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Id
			{
				get
				{
					System.Int32? data = entity.Id;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Id = null;
					else entity.Id = Convert.ToInt32(value);
				}
			}
				
			public System.String Month
			{
				get
				{
					System.String data = entity.Month;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Month = null;
					else entity.Month = Convert.ToString(value);
				}
			}
				
			public System.String Year
			{
				get
				{
					System.String data = entity.Year;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Year = null;
					else entity.Year = Convert.ToString(value);
				}
			}
				
			public System.String CreatedBy
			{
				get
				{
					System.String data = entity.CreatedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedBy = null;
					else entity.CreatedBy = Convert.ToString(value);
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
				
			public System.String DateCreated
			{
				get
				{
					System.DateTime? data = entity.DateCreated;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateCreated = null;
					else entity.DateCreated = Convert.ToDateTime(value);
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
				
			public System.String JournalId
			{
				get
				{
					System.Int32? data = entity.JournalId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalId = null;
					else entity.JournalId = Convert.ToInt32(value);
				}
			}
			

			private esIncomeJournalStatus entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esIncomeJournalStatusQuery query)
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
				throw new Exception("esIncomeJournalStatus can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esIncomeJournalStatusQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return IncomeJournalStatusMetadata.Meta();
			}
		}	
		

		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, IncomeJournalStatusMetadata.ColumnNames.Id, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Month
		{
			get
			{
				return new esQueryItem(this, IncomeJournalStatusMetadata.ColumnNames.Month, esSystemType.String);
			}
		} 
		
		public esQueryItem Year
		{
			get
			{
				return new esQueryItem(this, IncomeJournalStatusMetadata.ColumnNames.Year, esSystemType.String);
			}
		} 
		
		public esQueryItem CreatedBy
		{
			get
			{
				return new esQueryItem(this, IncomeJournalStatusMetadata.ColumnNames.CreatedBy, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, IncomeJournalStatusMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem DateCreated
		{
			get
			{
				return new esQueryItem(this, IncomeJournalStatusMetadata.ColumnNames.DateCreated, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, IncomeJournalStatusMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem JournalId
		{
			get
			{
				return new esQueryItem(this, IncomeJournalStatusMetadata.ColumnNames.JournalId, esSystemType.Int32);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("IncomeJournalStatusCollection")]
	public partial class IncomeJournalStatusCollection : esIncomeJournalStatusCollection, IEnumerable<IncomeJournalStatus>
	{
		public IncomeJournalStatusCollection()
		{

		}
		
		public static implicit operator List<IncomeJournalStatus>(IncomeJournalStatusCollection coll)
		{
			List<IncomeJournalStatus> list = new List<IncomeJournalStatus>();
			
			foreach (IncomeJournalStatus emp in coll)
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
				return  IncomeJournalStatusMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new IncomeJournalStatusQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new IncomeJournalStatus(row);
		}

		override protected esEntity CreateEntity()
		{
			return new IncomeJournalStatus();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public IncomeJournalStatusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new IncomeJournalStatusQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(IncomeJournalStatusQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public IncomeJournalStatus AddNew()
		{
			IncomeJournalStatus entity = base.AddNewEntity() as IncomeJournalStatus;
			
			return entity;
		}

		public IncomeJournalStatus FindByPrimaryKey(System.Int32 id)
		{
			return base.FindByPrimaryKey(id) as IncomeJournalStatus;
		}


		#region IEnumerable<IncomeJournalStatus> Members

		IEnumerator<IncomeJournalStatus> IEnumerable<IncomeJournalStatus>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as IncomeJournalStatus;
			}
		}

		#endregion
		
		private IncomeJournalStatusQuery query;
	}


	/// <summary>
	/// Encapsulates the 'IncomeJournalStatus' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("IncomeJournalStatus ({Id})")]
	[Serializable]
	public partial class IncomeJournalStatus : esIncomeJournalStatus
	{
		public IncomeJournalStatus()
		{

		}
	
		public IncomeJournalStatus(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return IncomeJournalStatusMetadata.Meta();
			}
		}
		
		
		
		override protected esIncomeJournalStatusQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new IncomeJournalStatusQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public IncomeJournalStatusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new IncomeJournalStatusQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(IncomeJournalStatusQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private IncomeJournalStatusQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class IncomeJournalStatusQuery : esIncomeJournalStatusQuery
	{
		public IncomeJournalStatusQuery()
		{

		}		
		
		public IncomeJournalStatusQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "IncomeJournalStatusQuery";
        }
		
			
	}


	[Serializable]
	public partial class IncomeJournalStatusMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected IncomeJournalStatusMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(IncomeJournalStatusMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = IncomeJournalStatusMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(IncomeJournalStatusMetadata.ColumnNames.Month, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = IncomeJournalStatusMetadata.PropertyNames.Month;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(IncomeJournalStatusMetadata.ColumnNames.Year, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = IncomeJournalStatusMetadata.PropertyNames.Year;
			c.CharacterMaxLength = 4;
			_columns.Add(c);
				
			c = new esColumnMetadata(IncomeJournalStatusMetadata.ColumnNames.CreatedBy, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = IncomeJournalStatusMetadata.PropertyNames.CreatedBy;
			c.CharacterMaxLength = 25;
			_columns.Add(c);
				
			c = new esColumnMetadata(IncomeJournalStatusMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = IncomeJournalStatusMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 25;
			_columns.Add(c);
				
			c = new esColumnMetadata(IncomeJournalStatusMetadata.ColumnNames.DateCreated, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = IncomeJournalStatusMetadata.PropertyNames.DateCreated;
			_columns.Add(c);
				
			c = new esColumnMetadata(IncomeJournalStatusMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = IncomeJournalStatusMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(IncomeJournalStatusMetadata.ColumnNames.JournalId, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = IncomeJournalStatusMetadata.PropertyNames.JournalId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public IncomeJournalStatusMetadata Meta()
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
			 public const string Id = "Id";
			 public const string Month = "Month";
			 public const string Year = "Year";
			 public const string CreatedBy = "CreatedBy";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string DateCreated = "DateCreated";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string JournalId = "JournalId";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Month = "Month";
			 public const string Year = "Year";
			 public const string CreatedBy = "CreatedBy";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string DateCreated = "DateCreated";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string JournalId = "JournalId";
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
			lock (typeof(IncomeJournalStatusMetadata))
			{
				if(IncomeJournalStatusMetadata.mapDelegates == null)
				{
					IncomeJournalStatusMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (IncomeJournalStatusMetadata.meta == null)
				{
					IncomeJournalStatusMetadata.meta = new IncomeJournalStatusMetadata();
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
				

				meta.AddTypeMap("Id", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Month", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Year", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedBy", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("DateCreated", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("JournalId", new esTypeMap("int", "System.Int32"));			
				
				
				
				meta.Source = "IncomeJournalStatus";
				meta.Destination = "IncomeJournalStatus";
				
				meta.spInsert = "proc_IncomeJournalStatusInsert";				
				meta.spUpdate = "proc_IncomeJournalStatusUpdate";		
				meta.spDelete = "proc_IncomeJournalStatusDelete";
				meta.spLoadAll = "proc_IncomeJournalStatusLoadAll";
				meta.spLoadByPrimaryKey = "proc_IncomeJournalStatusLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private IncomeJournalStatusMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
