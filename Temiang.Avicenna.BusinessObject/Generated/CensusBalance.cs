/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/11/2016 11:41:27 AM
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
	abstract public class esCensusBalanceCollection : esEntityCollectionWAuditLog
	{
		public esCensusBalanceCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "CensusBalanceCollection";
		}

		#region Query Logic
		protected void InitQuery(esCensusBalanceQuery query)
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
			this.InitQuery(query as esCensusBalanceQuery);
		}
		#endregion
		
		virtual public CensusBalance DetachEntity(CensusBalance entity)
		{
			return base.DetachEntity(entity) as CensusBalance;
		}
		
		virtual public CensusBalance AttachEntity(CensusBalance entity)
		{
			return base.AttachEntity(entity) as CensusBalance;
		}
		
		virtual public void Combine(CensusBalanceCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CensusBalance this[int index]
		{
			get
			{
				return base[index] as CensusBalance;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CensusBalance);
		}
	}



	[Serializable]
	abstract public class esCensusBalance : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCensusBalanceQuery GetDynamicQuery()
		{
			return null;
		}

		public esCensusBalance()
		{

		}

		public esCensusBalance(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.DateTime censusDate, System.String serviceUnitID, System.String classID, System.String smfID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(censusDate, serviceUnitID, classID, smfID);
			else
				return LoadByPrimaryKeyStoredProcedure(censusDate, serviceUnitID, classID, smfID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.DateTime censusDate, System.String serviceUnitID, System.String classID, System.String smfID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(censusDate, serviceUnitID, classID, smfID);
			else
				return LoadByPrimaryKeyStoredProcedure(censusDate, serviceUnitID, classID, smfID);
		}

		private bool LoadByPrimaryKeyDynamic(System.DateTime censusDate, System.String serviceUnitID, System.String classID, System.String smfID)
		{
			esCensusBalanceQuery query = this.GetDynamicQuery();
			query.Where(query.CensusDate == censusDate, query.ServiceUnitID == serviceUnitID, query.ClassID == classID, query.SmfID == smfID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.DateTime censusDate, System.String serviceUnitID, System.String classID, System.String smfID)
		{
			esParameters parms = new esParameters();
			parms.Add("CensusDate",censusDate);			parms.Add("ServiceUnitID",serviceUnitID);			parms.Add("ClassID",classID);			parms.Add("SmfID",smfID);
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
						case "CensusDate": this.str.CensusDate = (string)value; break;							
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "ClassID": this.str.ClassID = (string)value; break;							
						case "SmfID": this.str.SmfID = (string)value; break;							
						case "Balance": this.str.Balance = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "NumberOfBed": this.str.NumberOfBed = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CensusDate":
						
							if (value == null || value is System.DateTime)
								this.CensusDate = (System.DateTime?)value;
							break;
						
						case "Balance":
						
							if (value == null || value is System.Int32)
								this.Balance = (System.Int32?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "NumberOfBed":
						
							if (value == null || value is System.Int32)
								this.NumberOfBed = (System.Int32?)value;
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
		/// Maps to CensusBalance.CensusDate
		/// </summary>
		virtual public System.DateTime? CensusDate
		{
			get
			{
				return base.GetSystemDateTime(CensusBalanceMetadata.ColumnNames.CensusDate);
			}
			
			set
			{
				base.SetSystemDateTime(CensusBalanceMetadata.ColumnNames.CensusDate, value);
			}
		}
		
		/// <summary>
		/// Maps to CensusBalance.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(CensusBalanceMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(CensusBalanceMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to CensusBalance.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(CensusBalanceMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(CensusBalanceMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to CensusBalance.SmfID
		/// </summary>
		virtual public System.String SmfID
		{
			get
			{
				return base.GetSystemString(CensusBalanceMetadata.ColumnNames.SmfID);
			}
			
			set
			{
				base.SetSystemString(CensusBalanceMetadata.ColumnNames.SmfID, value);
			}
		}
		
		/// <summary>
		/// Maps to CensusBalance.Balance
		/// </summary>
		virtual public System.Int32? Balance
		{
			get
			{
				return base.GetSystemInt32(CensusBalanceMetadata.ColumnNames.Balance);
			}
			
			set
			{
				base.SetSystemInt32(CensusBalanceMetadata.ColumnNames.Balance, value);
			}
		}
		
		/// <summary>
		/// Maps to CensusBalance.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CensusBalanceMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CensusBalanceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to CensusBalance.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CensusBalanceMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(CensusBalanceMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to CensusBalance.NumberOfBed
		/// </summary>
		virtual public System.Int32? NumberOfBed
		{
			get
			{
				return base.GetSystemInt32(CensusBalanceMetadata.ColumnNames.NumberOfBed);
			}
			
			set
			{
				base.SetSystemInt32(CensusBalanceMetadata.ColumnNames.NumberOfBed, value);
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
			public esStrings(esCensusBalance entity)
			{
				this.entity = entity;
			}
			
	
			public System.String CensusDate
			{
				get
				{
					System.DateTime? data = entity.CensusDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CensusDate = null;
					else entity.CensusDate = Convert.ToDateTime(value);
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
				
			public System.String SmfID
			{
				get
				{
					System.String data = entity.SmfID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SmfID = null;
					else entity.SmfID = Convert.ToString(value);
				}
			}
				
			public System.String Balance
			{
				get
				{
					System.Int32? data = entity.Balance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Balance = null;
					else entity.Balance = Convert.ToInt32(value);
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
			

			private esCensusBalance entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCensusBalanceQuery query)
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
				throw new Exception("esCensusBalance can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class CensusBalance : esCensusBalance
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
	abstract public class esCensusBalanceQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CensusBalanceMetadata.Meta();
			}
		}	
		

		public esQueryItem CensusDate
		{
			get
			{
				return new esQueryItem(this, CensusBalanceMetadata.ColumnNames.CensusDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, CensusBalanceMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, CensusBalanceMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem SmfID
		{
			get
			{
				return new esQueryItem(this, CensusBalanceMetadata.ColumnNames.SmfID, esSystemType.String);
			}
		} 
		
		public esQueryItem Balance
		{
			get
			{
				return new esQueryItem(this, CensusBalanceMetadata.ColumnNames.Balance, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CensusBalanceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CensusBalanceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem NumberOfBed
		{
			get
			{
				return new esQueryItem(this, CensusBalanceMetadata.ColumnNames.NumberOfBed, esSystemType.Int32);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CensusBalanceCollection")]
	public partial class CensusBalanceCollection : esCensusBalanceCollection, IEnumerable<CensusBalance>
	{
		public CensusBalanceCollection()
		{

		}
		
		public static implicit operator List<CensusBalance>(CensusBalanceCollection coll)
		{
			List<CensusBalance> list = new List<CensusBalance>();
			
			foreach (CensusBalance emp in coll)
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
				return  CensusBalanceMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CensusBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CensusBalance(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CensusBalance();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public CensusBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CensusBalanceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(CensusBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public CensusBalance AddNew()
		{
			CensusBalance entity = base.AddNewEntity() as CensusBalance;
			
			return entity;
		}

		public CensusBalance FindByPrimaryKey(System.DateTime censusDate, System.String serviceUnitID, System.String classID, System.String smfID)
		{
			return base.FindByPrimaryKey(censusDate, serviceUnitID, classID, smfID) as CensusBalance;
		}


		#region IEnumerable<CensusBalance> Members

		IEnumerator<CensusBalance> IEnumerable<CensusBalance>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CensusBalance;
			}
		}

		#endregion
		
		private CensusBalanceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CensusBalance' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("CensusBalance ({CensusDate},{ServiceUnitID},{ClassID},{SmfID})")]
	[Serializable]
	public partial class CensusBalance : esCensusBalance
	{
		public CensusBalance()
		{

		}
	
		public CensusBalance(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CensusBalanceMetadata.Meta();
			}
		}
		
		
		
		override protected esCensusBalanceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CensusBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public CensusBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CensusBalanceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(CensusBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private CensusBalanceQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class CensusBalanceQuery : esCensusBalanceQuery
	{
		public CensusBalanceQuery()
		{

		}		
		
		public CensusBalanceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "CensusBalanceQuery";
        }
		
			
	}


	[Serializable]
	public partial class CensusBalanceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CensusBalanceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CensusBalanceMetadata.ColumnNames.CensusDate, 0, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CensusBalanceMetadata.PropertyNames.CensusDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CensusBalanceMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CensusBalanceMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CensusBalanceMetadata.ColumnNames.ClassID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CensusBalanceMetadata.PropertyNames.ClassID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CensusBalanceMetadata.ColumnNames.SmfID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CensusBalanceMetadata.PropertyNames.SmfID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CensusBalanceMetadata.ColumnNames.Balance, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CensusBalanceMetadata.PropertyNames.Balance;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CensusBalanceMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CensusBalanceMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CensusBalanceMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CensusBalanceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CensusBalanceMetadata.ColumnNames.NumberOfBed, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CensusBalanceMetadata.PropertyNames.NumberOfBed;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public CensusBalanceMetadata Meta()
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
			 public const string CensusDate = "CensusDate";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ClassID = "ClassID";
			 public const string SmfID = "SmfID";
			 public const string Balance = "Balance";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string NumberOfBed = "NumberOfBed";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string CensusDate = "CensusDate";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ClassID = "ClassID";
			 public const string SmfID = "SmfID";
			 public const string Balance = "Balance";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string NumberOfBed = "NumberOfBed";
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
			lock (typeof(CensusBalanceMetadata))
			{
				if(CensusBalanceMetadata.mapDelegates == null)
				{
					CensusBalanceMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CensusBalanceMetadata.meta == null)
				{
					CensusBalanceMetadata.meta = new CensusBalanceMetadata();
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
				

				meta.AddTypeMap("CensusDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SmfID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Balance", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NumberOfBed", new esTypeMap("int", "System.Int32"));			
				
				
				
				meta.Source = "CensusBalance";
				meta.Destination = "CensusBalance";
				
				meta.spInsert = "proc_CensusBalanceInsert";				
				meta.spUpdate = "proc_CensusBalanceUpdate";		
				meta.spDelete = "proc_CensusBalanceDelete";
				meta.spLoadAll = "proc_CensusBalanceLoadAll";
				meta.spLoadByPrimaryKey = "proc_CensusBalanceLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CensusBalanceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
