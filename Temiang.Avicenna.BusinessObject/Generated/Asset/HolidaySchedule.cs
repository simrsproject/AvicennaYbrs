/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/15/2015 1:40:43 PM
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
	abstract public class esHolidayScheduleCollection : esEntityCollectionWAuditLog
	{
		public esHolidayScheduleCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "HolidayScheduleCollection";
		}

		#region Query Logic
		protected void InitQuery(esHolidayScheduleQuery query)
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
			this.InitQuery(query as esHolidayScheduleQuery);
		}
		#endregion
		
		virtual public HolidaySchedule DetachEntity(HolidaySchedule entity)
		{
			return base.DetachEntity(entity) as HolidaySchedule;
		}
		
		virtual public HolidaySchedule AttachEntity(HolidaySchedule entity)
		{
			return base.AttachEntity(entity) as HolidaySchedule;
		}
		
		virtual public void Combine(HolidayScheduleCollection collection)
		{
			base.Combine(collection);
		}
		
		new public HolidaySchedule this[int index]
		{
			get
			{
				return base[index] as HolidaySchedule;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(HolidaySchedule);
		}
	}



	[Serializable]
	abstract public class esHolidaySchedule : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esHolidayScheduleQuery GetDynamicQuery()
		{
			return null;
		}

		public esHolidaySchedule()
		{

		}

		public esHolidaySchedule(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String periodYear, System.DateTime holidayDate)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(periodYear, holidayDate);
			else
				return LoadByPrimaryKeyStoredProcedure(periodYear, holidayDate);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String periodYear, System.DateTime holidayDate)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(periodYear, holidayDate);
			else
				return LoadByPrimaryKeyStoredProcedure(periodYear, holidayDate);
		}

		private bool LoadByPrimaryKeyDynamic(System.String periodYear, System.DateTime holidayDate)
		{
			esHolidayScheduleQuery query = this.GetDynamicQuery();
			query.Where(query.PeriodYear == periodYear, query.HolidayDate == holidayDate);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String periodYear, System.DateTime holidayDate)
		{
			esParameters parms = new esParameters();
			parms.Add("PeriodYear",periodYear);			parms.Add("HolidayDate",holidayDate);
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
						case "PeriodYear": this.str.PeriodYear = (string)value; break;							
						case "HolidayDate": this.str.HolidayDate = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "HolidayDate":
						
							if (value == null || value is System.DateTime)
								this.HolidayDate = (System.DateTime?)value;
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
		/// Maps to HolidaySchedule.PeriodYear
		/// </summary>
		virtual public System.String PeriodYear
		{
			get
			{
				return base.GetSystemString(HolidayScheduleMetadata.ColumnNames.PeriodYear);
			}
			
			set
			{
				base.SetSystemString(HolidayScheduleMetadata.ColumnNames.PeriodYear, value);
			}
		}
		
		/// <summary>
		/// Maps to HolidaySchedule.HolidayDate
		/// </summary>
		virtual public System.DateTime? HolidayDate
		{
			get
			{
				return base.GetSystemDateTime(HolidayScheduleMetadata.ColumnNames.HolidayDate);
			}
			
			set
			{
				base.SetSystemDateTime(HolidayScheduleMetadata.ColumnNames.HolidayDate, value);
			}
		}
		
		/// <summary>
		/// Maps to HolidaySchedule.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(HolidayScheduleMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(HolidayScheduleMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to HolidaySchedule.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(HolidayScheduleMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(HolidayScheduleMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esHolidaySchedule entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PeriodYear
			{
				get
				{
					System.String data = entity.PeriodYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodYear = null;
					else entity.PeriodYear = Convert.ToString(value);
				}
			}
				
			public System.String HolidayDate
			{
				get
				{
					System.DateTime? data = entity.HolidayDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HolidayDate = null;
					else entity.HolidayDate = Convert.ToDateTime(value);
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
			

			private esHolidaySchedule entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esHolidayScheduleQuery query)
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
				throw new Exception("esHolidaySchedule can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class HolidaySchedule : esHolidaySchedule
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
	abstract public class esHolidayScheduleQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return HolidayScheduleMetadata.Meta();
			}
		}	
		

		public esQueryItem PeriodYear
		{
			get
			{
				return new esQueryItem(this, HolidayScheduleMetadata.ColumnNames.PeriodYear, esSystemType.String);
			}
		} 
		
		public esQueryItem HolidayDate
		{
			get
			{
				return new esQueryItem(this, HolidayScheduleMetadata.ColumnNames.HolidayDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, HolidayScheduleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, HolidayScheduleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("HolidayScheduleCollection")]
	public partial class HolidayScheduleCollection : esHolidayScheduleCollection, IEnumerable<HolidaySchedule>
	{
		public HolidayScheduleCollection()
		{

		}
		
		public static implicit operator List<HolidaySchedule>(HolidayScheduleCollection coll)
		{
			List<HolidaySchedule> list = new List<HolidaySchedule>();
			
			foreach (HolidaySchedule emp in coll)
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
				return  HolidayScheduleMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HolidayScheduleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new HolidaySchedule(row);
		}

		override protected esEntity CreateEntity()
		{
			return new HolidaySchedule();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public HolidayScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HolidayScheduleQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(HolidayScheduleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public HolidaySchedule AddNew()
		{
			HolidaySchedule entity = base.AddNewEntity() as HolidaySchedule;
			
			return entity;
		}

		public HolidaySchedule FindByPrimaryKey(System.String periodYear, System.DateTime holidayDate)
		{
			return base.FindByPrimaryKey(periodYear, holidayDate) as HolidaySchedule;
		}


		#region IEnumerable<HolidaySchedule> Members

		IEnumerator<HolidaySchedule> IEnumerable<HolidaySchedule>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as HolidaySchedule;
			}
		}

		#endregion
		
		private HolidayScheduleQuery query;
	}


	/// <summary>
	/// Encapsulates the 'HolidaySchedule' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("HolidaySchedule ({PeriodYear},{HolidayDate})")]
	[Serializable]
	public partial class HolidaySchedule : esHolidaySchedule
	{
		public HolidaySchedule()
		{

		}
	
		public HolidaySchedule(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return HolidayScheduleMetadata.Meta();
			}
		}
		
		
		
		override protected esHolidayScheduleQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HolidayScheduleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public HolidayScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HolidayScheduleQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(HolidayScheduleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private HolidayScheduleQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class HolidayScheduleQuery : esHolidayScheduleQuery
	{
		public HolidayScheduleQuery()
		{

		}		
		
		public HolidayScheduleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "HolidayScheduleQuery";
        }
		
			
	}


	[Serializable]
	public partial class HolidayScheduleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected HolidayScheduleMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(HolidayScheduleMetadata.ColumnNames.PeriodYear, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = HolidayScheduleMetadata.PropertyNames.PeriodYear;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 4;
			_columns.Add(c);
				
			c = new esColumnMetadata(HolidayScheduleMetadata.ColumnNames.HolidayDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = HolidayScheduleMetadata.PropertyNames.HolidayDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HolidayScheduleMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = HolidayScheduleMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HolidayScheduleMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = HolidayScheduleMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public HolidayScheduleMetadata Meta()
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
			 public const string PeriodYear = "PeriodYear";
			 public const string HolidayDate = "HolidayDate";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PeriodYear = "PeriodYear";
			 public const string HolidayDate = "HolidayDate";
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
			lock (typeof(HolidayScheduleMetadata))
			{
				if(HolidayScheduleMetadata.mapDelegates == null)
				{
					HolidayScheduleMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (HolidayScheduleMetadata.meta == null)
				{
					HolidayScheduleMetadata.meta = new HolidayScheduleMetadata();
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
				

				meta.AddTypeMap("PeriodYear", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("HolidayDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "HolidaySchedule";
				meta.Destination = "HolidaySchedule";
				
				meta.spInsert = "proc_HolidayScheduleInsert";				
				meta.spUpdate = "proc_HolidayScheduleUpdate";		
				meta.spDelete = "proc_HolidayScheduleDelete";
				meta.spLoadAll = "proc_HolidayScheduleLoadAll";
				meta.spLoadByPrimaryKey = "proc_HolidayScheduleLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private HolidayScheduleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
