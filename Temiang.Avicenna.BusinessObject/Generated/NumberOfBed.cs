/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/7/2015 9:05:20 AM
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
	abstract public class esNumberOfBedCollection : esEntityCollectionWAuditLog
	{
		public esNumberOfBedCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "NumberOfBedCollection";
		}

		#region Query Logic
		protected void InitQuery(esNumberOfBedQuery query)
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
			this.InitQuery(query as esNumberOfBedQuery);
		}
		#endregion
		
		virtual public NumberOfBed DetachEntity(NumberOfBed entity)
		{
			return base.DetachEntity(entity) as NumberOfBed;
		}
		
		virtual public NumberOfBed AttachEntity(NumberOfBed entity)
		{
			return base.AttachEntity(entity) as NumberOfBed;
		}
		
		virtual public void Combine(NumberOfBedCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NumberOfBed this[int index]
		{
			get
			{
				return base[index] as NumberOfBed;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NumberOfBed);
		}
	}



	[Serializable]
	abstract public class esNumberOfBed : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNumberOfBedQuery GetDynamicQuery()
		{
			return null;
		}

		public esNumberOfBed()
		{

		}

		public esNumberOfBed(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.DateTime startingDate, System.String serviceUnitID, System.String classID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(startingDate, serviceUnitID, classID);
			else
				return LoadByPrimaryKeyStoredProcedure(startingDate, serviceUnitID, classID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.DateTime startingDate, System.String serviceUnitID, System.String classID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(startingDate, serviceUnitID, classID);
			else
				return LoadByPrimaryKeyStoredProcedure(startingDate, serviceUnitID, classID);
		}

		private bool LoadByPrimaryKeyDynamic(System.DateTime startingDate, System.String serviceUnitID, System.String classID)
		{
			esNumberOfBedQuery query = this.GetDynamicQuery();
			query.Where(query.StartingDate == startingDate, query.ServiceUnitID == serviceUnitID, query.ClassID == classID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.DateTime startingDate, System.String serviceUnitID, System.String classID)
		{
			esParameters parms = new esParameters();
			parms.Add("StartingDate",startingDate);			parms.Add("ServiceUnitID",serviceUnitID);			parms.Add("ClassID",classID);
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
						case "StartingDate": this.str.StartingDate = (string)value; break;							
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "ClassID": this.str.ClassID = (string)value; break;							
						case "NumberOfBed": this.str.NumberOfBed = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "StartingDate":
						
							if (value == null || value is System.DateTime)
								this.StartingDate = (System.DateTime?)value;
							break;
						
						case "NumberOfBed":
						
							if (value == null || value is System.Int32)
								this.NumberOfBed = (System.Int32?)value;
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
		/// Maps to NumberOfBed.StartingDate
		/// </summary>
		virtual public System.DateTime? StartingDate
		{
			get
			{
				return base.GetSystemDateTime(NumberOfBedMetadata.ColumnNames.StartingDate);
			}
			
			set
			{
				base.SetSystemDateTime(NumberOfBedMetadata.ColumnNames.StartingDate, value);
			}
		}
		
		/// <summary>
		/// Maps to NumberOfBed.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(NumberOfBedMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(NumberOfBedMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to NumberOfBed.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(NumberOfBedMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(NumberOfBedMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to NumberOfBed.NumberOfBed
		/// </summary>
		virtual public System.Int32? NumberOfBed
		{
			get
			{
				return base.GetSystemInt32(NumberOfBedMetadata.ColumnNames.NumberOfBed);
			}
			
			set
			{
				base.SetSystemInt32(NumberOfBedMetadata.ColumnNames.NumberOfBed, value);
			}
		}
		
		/// <summary>
		/// Maps to NumberOfBed.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(NumberOfBedMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(NumberOfBedMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to NumberOfBed.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NumberOfBedMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NumberOfBedMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esNumberOfBed entity)
			{
				this.entity = entity;
			}
			
	
			public System.String StartingDate
			{
				get
				{
					System.DateTime? data = entity.StartingDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartingDate = null;
					else entity.StartingDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
				
			public System.String ClassID
			{
				get
				{
					System.String data = entity.ClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassID = null;
					else entity.ClassID = Convert.ToString(value);
				}
			}
				
			public System.String NumberOfBed
			{
				get
				{
					System.Int32? data = entity.NumberOfBed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NumberOfBed = null;
					else entity.NumberOfBed = Convert.ToInt32(value);
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
			

			private esNumberOfBed entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNumberOfBedQuery query)
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
				throw new Exception("esNumberOfBed can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class NumberOfBed : esNumberOfBed
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
	abstract public class esNumberOfBedQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return NumberOfBedMetadata.Meta();
			}
		}	
		

		public esQueryItem StartingDate
		{
			get
			{
				return new esQueryItem(this, NumberOfBedMetadata.ColumnNames.StartingDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, NumberOfBedMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, NumberOfBedMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem NumberOfBed
		{
			get
			{
				return new esQueryItem(this, NumberOfBedMetadata.ColumnNames.NumberOfBed, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, NumberOfBedMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, NumberOfBedMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NumberOfBedCollection")]
	public partial class NumberOfBedCollection : esNumberOfBedCollection, IEnumerable<NumberOfBed>
	{
		public NumberOfBedCollection()
		{

		}
		
		public static implicit operator List<NumberOfBed>(NumberOfBedCollection coll)
		{
			List<NumberOfBed> list = new List<NumberOfBed>();
			
			foreach (NumberOfBed emp in coll)
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
				return  NumberOfBedMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NumberOfBedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NumberOfBed(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NumberOfBed();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public NumberOfBedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NumberOfBedQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(NumberOfBedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public NumberOfBed AddNew()
		{
			NumberOfBed entity = base.AddNewEntity() as NumberOfBed;
			
			return entity;
		}

		public NumberOfBed FindByPrimaryKey(System.DateTime startingDate, System.String serviceUnitID, System.String classID)
		{
			return base.FindByPrimaryKey(startingDate, serviceUnitID, classID) as NumberOfBed;
		}


		#region IEnumerable<NumberOfBed> Members

		IEnumerator<NumberOfBed> IEnumerable<NumberOfBed>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NumberOfBed;
			}
		}

		#endregion
		
		private NumberOfBedQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NumberOfBed' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("NumberOfBed ({StartingDate},{ServiceUnitID},{ClassID})")]
	[Serializable]
	public partial class NumberOfBed : esNumberOfBed
	{
		public NumberOfBed()
		{

		}
	
		public NumberOfBed(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NumberOfBedMetadata.Meta();
			}
		}
		
		
		
		override protected esNumberOfBedQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NumberOfBedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public NumberOfBedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NumberOfBedQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(NumberOfBedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private NumberOfBedQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class NumberOfBedQuery : esNumberOfBedQuery
	{
		public NumberOfBedQuery()
		{

		}		
		
		public NumberOfBedQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "NumberOfBedQuery";
        }
		
			
	}


	[Serializable]
	public partial class NumberOfBedMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NumberOfBedMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(NumberOfBedMetadata.ColumnNames.StartingDate, 0, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NumberOfBedMetadata.PropertyNames.StartingDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NumberOfBedMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = NumberOfBedMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(NumberOfBedMetadata.ColumnNames.ClassID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = NumberOfBedMetadata.PropertyNames.ClassID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(NumberOfBedMetadata.ColumnNames.NumberOfBed, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NumberOfBedMetadata.PropertyNames.NumberOfBed;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(NumberOfBedMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = NumberOfBedMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NumberOfBedMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NumberOfBedMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public NumberOfBedMetadata Meta()
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
			 public const string StartingDate = "StartingDate";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ClassID = "ClassID";
			 public const string NumberOfBed = "NumberOfBed";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string StartingDate = "StartingDate";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ClassID = "ClassID";
			 public const string NumberOfBed = "NumberOfBed";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(NumberOfBedMetadata))
			{
				if(NumberOfBedMetadata.mapDelegates == null)
				{
					NumberOfBedMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NumberOfBedMetadata.meta == null)
				{
					NumberOfBedMetadata.meta = new NumberOfBedMetadata();
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
				

				meta.AddTypeMap("StartingDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NumberOfBed", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "NumberOfBed";
				meta.Destination = "NumberOfBed";
				
				meta.spInsert = "proc_NumberOfBedInsert";				
				meta.spUpdate = "proc_NumberOfBedUpdate";		
				meta.spDelete = "proc_NumberOfBedDelete";
				meta.spLoadAll = "proc_NumberOfBedLoadAll";
				meta.spLoadByPrimaryKey = "proc_NumberOfBedLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NumberOfBedMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
