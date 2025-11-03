/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/1/2022 7:21:48 AM
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
	abstract public class esMonthlyAttendanceDetailHistoryCollection : esEntityCollectionWAuditLog
	{
		public esMonthlyAttendanceDetailHistoryCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "MonthlyAttendanceDetailHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esMonthlyAttendanceDetailHistoryQuery query)
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
			this.InitQuery(query as esMonthlyAttendanceDetailHistoryQuery);
		}
		#endregion
		
		virtual public MonthlyAttendanceDetailHistory DetachEntity(MonthlyAttendanceDetailHistory entity)
		{
			return base.DetachEntity(entity) as MonthlyAttendanceDetailHistory;
		}
		
		virtual public MonthlyAttendanceDetailHistory AttachEntity(MonthlyAttendanceDetailHistory entity)
		{
			return base.AttachEntity(entity) as MonthlyAttendanceDetailHistory;
		}
		
		virtual public void Combine(MonthlyAttendanceDetailHistoryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public MonthlyAttendanceDetailHistory this[int index]
		{
			get
			{
				return base[index] as MonthlyAttendanceDetailHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MonthlyAttendanceDetailHistory);
		}
	}



	[Serializable]
	abstract public class esMonthlyAttendanceDetailHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMonthlyAttendanceDetailHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esMonthlyAttendanceDetailHistory()
		{

		}

		public esMonthlyAttendanceDetailHistory(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int64 monthlyAttendanceDetailID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(monthlyAttendanceDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(monthlyAttendanceDetailID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int64 monthlyAttendanceDetailID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(monthlyAttendanceDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(monthlyAttendanceDetailID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int64 monthlyAttendanceDetailID)
		{
			esMonthlyAttendanceDetailHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.MonthlyAttendanceDetailID == monthlyAttendanceDetailID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int64 monthlyAttendanceDetailID)
		{
			esParameters parms = new esParameters();
			parms.Add("MonthlyAttendanceDetailID",monthlyAttendanceDetailID);
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
						case "MonthlyAttendanceDetailID": this.str.MonthlyAttendanceDetailID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "WorkingHourID": this.str.WorkingHourID = (string)value; break;							
						case "CheckInDateTime": this.str.CheckInDateTime = (string)value; break;							
						case "CheckOutDateTime": this.str.CheckOutDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "MonthlyAttendanceDetailID":
						
							if (value == null || value is System.Int64)
								this.MonthlyAttendanceDetailID = (System.Int64?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						
						case "WorkingHourID":
						
							if (value == null || value is System.Int32)
								this.WorkingHourID = (System.Int32?)value;
							break;
						
						case "CheckInDateTime":
						
							if (value == null || value is System.DateTime)
								this.CheckInDateTime = (System.DateTime?)value;
							break;
						
						case "CheckOutDateTime":
						
							if (value == null || value is System.DateTime)
								this.CheckOutDateTime = (System.DateTime?)value;
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
		/// Maps to MonthlyAttendanceDetailHistory.MonthlyAttendanceDetailID
		/// </summary>
		virtual public System.Int64? MonthlyAttendanceDetailID
		{
			get
			{
				return base.GetSystemInt64(MonthlyAttendanceDetailHistoryMetadata.ColumnNames.MonthlyAttendanceDetailID);
			}
			
			set
			{
				base.SetSystemInt64(MonthlyAttendanceDetailHistoryMetadata.ColumnNames.MonthlyAttendanceDetailID, value);
			}
		}
		
		/// <summary>
		/// Maps to MonthlyAttendanceDetailHistory.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceDetailHistoryMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(MonthlyAttendanceDetailHistoryMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to MonthlyAttendanceDetailHistory.WorkingHourID
		/// </summary>
		virtual public System.Int32? WorkingHourID
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceDetailHistoryMetadata.ColumnNames.WorkingHourID);
			}
			
			set
			{
				base.SetSystemInt32(MonthlyAttendanceDetailHistoryMetadata.ColumnNames.WorkingHourID, value);
			}
		}
		
		/// <summary>
		/// Maps to MonthlyAttendanceDetailHistory.CheckInDateTime
		/// </summary>
		virtual public System.DateTime? CheckInDateTime
		{
			get
			{
				return base.GetSystemDateTime(MonthlyAttendanceDetailHistoryMetadata.ColumnNames.CheckInDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MonthlyAttendanceDetailHistoryMetadata.ColumnNames.CheckInDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to MonthlyAttendanceDetailHistory.CheckOutDateTime
		/// </summary>
		virtual public System.DateTime? CheckOutDateTime
		{
			get
			{
				return base.GetSystemDateTime(MonthlyAttendanceDetailHistoryMetadata.ColumnNames.CheckOutDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MonthlyAttendanceDetailHistoryMetadata.ColumnNames.CheckOutDateTime, value);
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
			public esStrings(esMonthlyAttendanceDetailHistory entity)
			{
				this.entity = entity;
			}
			
	
			public System.String MonthlyAttendanceDetailID
			{
				get
				{
					System.Int64? data = entity.MonthlyAttendanceDetailID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MonthlyAttendanceDetailID = null;
					else entity.MonthlyAttendanceDetailID = Convert.ToInt64(value);
				}
			}
				
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingHourID
			{
				get
				{
					System.Int32? data = entity.WorkingHourID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingHourID = null;
					else entity.WorkingHourID = Convert.ToInt32(value);
				}
			}
				
			public System.String CheckInDateTime
			{
				get
				{
					System.DateTime? data = entity.CheckInDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckInDateTime = null;
					else entity.CheckInDateTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String CheckOutDateTime
			{
				get
				{
					System.DateTime? data = entity.CheckOutDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckOutDateTime = null;
					else entity.CheckOutDateTime = Convert.ToDateTime(value);
				}
			}
			

			private esMonthlyAttendanceDetailHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMonthlyAttendanceDetailHistoryQuery query)
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
				throw new Exception("esMonthlyAttendanceDetailHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esMonthlyAttendanceDetailHistoryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return MonthlyAttendanceDetailHistoryMetadata.Meta();
			}
		}	
		

		public esQueryItem MonthlyAttendanceDetailID
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailHistoryMetadata.ColumnNames.MonthlyAttendanceDetailID, esSystemType.Int64);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailHistoryMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingHourID
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailHistoryMetadata.ColumnNames.WorkingHourID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem CheckInDateTime
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailHistoryMetadata.ColumnNames.CheckInDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem CheckOutDateTime
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailHistoryMetadata.ColumnNames.CheckOutDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MonthlyAttendanceDetailHistoryCollection")]
	public partial class MonthlyAttendanceDetailHistoryCollection : esMonthlyAttendanceDetailHistoryCollection, IEnumerable<MonthlyAttendanceDetailHistory>
	{
		public MonthlyAttendanceDetailHistoryCollection()
		{

		}
		
		public static implicit operator List<MonthlyAttendanceDetailHistory>(MonthlyAttendanceDetailHistoryCollection coll)
		{
			List<MonthlyAttendanceDetailHistory> list = new List<MonthlyAttendanceDetailHistory>();
			
			foreach (MonthlyAttendanceDetailHistory emp in coll)
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
				return  MonthlyAttendanceDetailHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MonthlyAttendanceDetailHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MonthlyAttendanceDetailHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MonthlyAttendanceDetailHistory();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public MonthlyAttendanceDetailHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MonthlyAttendanceDetailHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(MonthlyAttendanceDetailHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public MonthlyAttendanceDetailHistory AddNew()
		{
			MonthlyAttendanceDetailHistory entity = base.AddNewEntity() as MonthlyAttendanceDetailHistory;
			
			return entity;
		}

		public MonthlyAttendanceDetailHistory FindByPrimaryKey(System.Int64 monthlyAttendanceDetailID)
		{
			return base.FindByPrimaryKey(monthlyAttendanceDetailID) as MonthlyAttendanceDetailHistory;
		}


		#region IEnumerable<MonthlyAttendanceDetailHistory> Members

		IEnumerator<MonthlyAttendanceDetailHistory> IEnumerable<MonthlyAttendanceDetailHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as MonthlyAttendanceDetailHistory;
			}
		}

		#endregion
		
		private MonthlyAttendanceDetailHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MonthlyAttendanceDetailHistory' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("MonthlyAttendanceDetailHistory ({MonthlyAttendanceDetailID})")]
	[Serializable]
	public partial class MonthlyAttendanceDetailHistory : esMonthlyAttendanceDetailHistory
	{
		public MonthlyAttendanceDetailHistory()
		{

		}
	
		public MonthlyAttendanceDetailHistory(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MonthlyAttendanceDetailHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esMonthlyAttendanceDetailHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MonthlyAttendanceDetailHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public MonthlyAttendanceDetailHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MonthlyAttendanceDetailHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(MonthlyAttendanceDetailHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private MonthlyAttendanceDetailHistoryQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class MonthlyAttendanceDetailHistoryQuery : esMonthlyAttendanceDetailHistoryQuery
	{
		public MonthlyAttendanceDetailHistoryQuery()
		{

		}		
		
		public MonthlyAttendanceDetailHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "MonthlyAttendanceDetailHistoryQuery";
        }
		
			
	}


	[Serializable]
	public partial class MonthlyAttendanceDetailHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MonthlyAttendanceDetailHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MonthlyAttendanceDetailHistoryMetadata.ColumnNames.MonthlyAttendanceDetailID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = MonthlyAttendanceDetailHistoryMetadata.PropertyNames.MonthlyAttendanceDetailID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(MonthlyAttendanceDetailHistoryMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceDetailHistoryMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MonthlyAttendanceDetailHistoryMetadata.ColumnNames.WorkingHourID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceDetailHistoryMetadata.PropertyNames.WorkingHourID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MonthlyAttendanceDetailHistoryMetadata.ColumnNames.CheckInDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MonthlyAttendanceDetailHistoryMetadata.PropertyNames.CheckInDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MonthlyAttendanceDetailHistoryMetadata.ColumnNames.CheckOutDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MonthlyAttendanceDetailHistoryMetadata.PropertyNames.CheckOutDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public MonthlyAttendanceDetailHistoryMetadata Meta()
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
			 public const string MonthlyAttendanceDetailID = "MonthlyAttendanceDetailID";
			 public const string PersonID = "PersonID";
			 public const string WorkingHourID = "WorkingHourID";
			 public const string CheckInDateTime = "CheckInDateTime";
			 public const string CheckOutDateTime = "CheckOutDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string MonthlyAttendanceDetailID = "MonthlyAttendanceDetailID";
			 public const string PersonID = "PersonID";
			 public const string WorkingHourID = "WorkingHourID";
			 public const string CheckInDateTime = "CheckInDateTime";
			 public const string CheckOutDateTime = "CheckOutDateTime";
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
			lock (typeof(MonthlyAttendanceDetailHistoryMetadata))
			{
				if(MonthlyAttendanceDetailHistoryMetadata.mapDelegates == null)
				{
					MonthlyAttendanceDetailHistoryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MonthlyAttendanceDetailHistoryMetadata.meta == null)
				{
					MonthlyAttendanceDetailHistoryMetadata.meta = new MonthlyAttendanceDetailHistoryMetadata();
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
				

				meta.AddTypeMap("MonthlyAttendanceDetailID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingHourID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CheckInDateTime", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("CheckOutDateTime", new esTypeMap("smalldatetime", "System.DateTime"));			
				
				
				
				meta.Source = "MonthlyAttendanceDetailHistory";
				meta.Destination = "MonthlyAttendanceDetailHistory";
				
				meta.spInsert = "proc_MonthlyAttendanceDetailHistoryInsert";				
				meta.spUpdate = "proc_MonthlyAttendanceDetailHistoryUpdate";		
				meta.spDelete = "proc_MonthlyAttendanceDetailHistoryDelete";
				meta.spLoadAll = "proc_MonthlyAttendanceDetailHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_MonthlyAttendanceDetailHistoryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MonthlyAttendanceDetailHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
