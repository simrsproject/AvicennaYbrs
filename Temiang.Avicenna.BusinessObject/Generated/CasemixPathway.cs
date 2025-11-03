/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/22/2021 12:19:01 PM
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
	abstract public class esCasemixPathwayCollection : esEntityCollectionWAuditLog
	{
		public esCasemixPathwayCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "CasemixPathwayCollection";
		}

		#region Query Logic
		protected void InitQuery(esCasemixPathwayQuery query)
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
			this.InitQuery(query as esCasemixPathwayQuery);
		}
		#endregion
		
		virtual public CasemixPathway DetachEntity(CasemixPathway entity)
		{
			return base.DetachEntity(entity) as CasemixPathway;
		}
		
		virtual public CasemixPathway AttachEntity(CasemixPathway entity)
		{
			return base.AttachEntity(entity) as CasemixPathway;
		}
		
		virtual public void Combine(CasemixPathwayCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CasemixPathway this[int index]
		{
			get
			{
				return base[index] as CasemixPathway;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CasemixPathway);
		}
	}



	[Serializable]
	abstract public class esCasemixPathway : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCasemixPathwayQuery GetDynamicQuery()
		{
			return null;
		}

		public esCasemixPathway()
		{

		}

		public esCasemixPathway(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String pathwayID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(pathwayID);
			else
				return LoadByPrimaryKeyStoredProcedure(pathwayID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String pathwayID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(pathwayID);
			else
				return LoadByPrimaryKeyStoredProcedure(pathwayID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String pathwayID)
		{
			esCasemixPathwayQuery query = this.GetDynamicQuery();
			query.Where(query.PathwayID == pathwayID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String pathwayID)
		{
			esParameters parms = new esParameters();
			parms.Add("PathwayID",pathwayID);
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
						case "PathwayID": this.str.PathwayID = (string)value; break;							
						case "PathwayName": this.str.PathwayName = (string)value; break;							
						case "CoverageValue": this.str.CoverageValue = (string)value; break;							
						case "Alos": this.str.Alos = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CoverageValue":
						
							if (value == null || value is System.Decimal)
								this.CoverageValue = (System.Decimal?)value;
							break;
						
						case "Alos":
						
							if (value == null || value is System.Int32)
								this.Alos = (System.Int32?)value;
							break;
						
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to CasemixPathway.PathwayID
		/// </summary>
		virtual public System.String PathwayID
		{
			get
			{
				return base.GetSystemString(CasemixPathwayMetadata.ColumnNames.PathwayID);
			}
			
			set
			{
				base.SetSystemString(CasemixPathwayMetadata.ColumnNames.PathwayID, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixPathway.PathwayName
		/// </summary>
		virtual public System.String PathwayName
		{
			get
			{
				return base.GetSystemString(CasemixPathwayMetadata.ColumnNames.PathwayName);
			}
			
			set
			{
				base.SetSystemString(CasemixPathwayMetadata.ColumnNames.PathwayName, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixPathway.CoverageValue
		/// </summary>
		virtual public System.Decimal? CoverageValue
		{
			get
			{
				return base.GetSystemDecimal(CasemixPathwayMetadata.ColumnNames.CoverageValue);
			}
			
			set
			{
				base.SetSystemDecimal(CasemixPathwayMetadata.ColumnNames.CoverageValue, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixPathway.ALOS
		/// </summary>
		virtual public System.Int32? Alos
		{
			get
			{
				return base.GetSystemInt32(CasemixPathwayMetadata.ColumnNames.Alos);
			}
			
			set
			{
				base.SetSystemInt32(CasemixPathwayMetadata.ColumnNames.Alos, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixPathway.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(CasemixPathwayMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(CasemixPathwayMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixPathway.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(CasemixPathwayMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(CasemixPathwayMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixPathway.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CasemixPathwayMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CasemixPathwayMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixPathway.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CasemixPathwayMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(CasemixPathwayMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCasemixPathway entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PathwayID
			{
				get
				{
					System.String data = entity.PathwayID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PathwayID = null;
					else entity.PathwayID = Convert.ToString(value);
				}
			}
				
			public System.String PathwayName
			{
				get
				{
					System.String data = entity.PathwayName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PathwayName = null;
					else entity.PathwayName = Convert.ToString(value);
				}
			}
				
			public System.String CoverageValue
			{
				get
				{
					System.Decimal? data = entity.CoverageValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoverageValue = null;
					else entity.CoverageValue = Convert.ToDecimal(value);
				}
			}
				
			public System.String Alos
			{
				get
				{
					System.Int32? data = entity.Alos;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Alos = null;
					else entity.Alos = Convert.ToInt32(value);
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
				
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			

			private esCasemixPathway entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCasemixPathwayQuery query)
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
				throw new Exception("esCasemixPathway can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esCasemixPathwayQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CasemixPathwayMetadata.Meta();
			}
		}	
		

		public esQueryItem PathwayID
		{
			get
			{
				return new esQueryItem(this, CasemixPathwayMetadata.ColumnNames.PathwayID, esSystemType.String);
			}
		} 
		
		public esQueryItem PathwayName
		{
			get
			{
				return new esQueryItem(this, CasemixPathwayMetadata.ColumnNames.PathwayName, esSystemType.String);
			}
		} 
		
		public esQueryItem CoverageValue
		{
			get
			{
				return new esQueryItem(this, CasemixPathwayMetadata.ColumnNames.CoverageValue, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Alos
		{
			get
			{
				return new esQueryItem(this, CasemixPathwayMetadata.ColumnNames.Alos, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, CasemixPathwayMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, CasemixPathwayMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CasemixPathwayMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CasemixPathwayMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CasemixPathwayCollection")]
	public partial class CasemixPathwayCollection : esCasemixPathwayCollection, IEnumerable<CasemixPathway>
	{
		public CasemixPathwayCollection()
		{

		}
		
		public static implicit operator List<CasemixPathway>(CasemixPathwayCollection coll)
		{
			List<CasemixPathway> list = new List<CasemixPathway>();
			
			foreach (CasemixPathway emp in coll)
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
				return  CasemixPathwayMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CasemixPathwayQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CasemixPathway(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CasemixPathway();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public CasemixPathwayQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CasemixPathwayQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(CasemixPathwayQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public CasemixPathway AddNew()
		{
			CasemixPathway entity = base.AddNewEntity() as CasemixPathway;
			
			return entity;
		}

		public CasemixPathway FindByPrimaryKey(System.String pathwayID)
		{
			return base.FindByPrimaryKey(pathwayID) as CasemixPathway;
		}


		#region IEnumerable<CasemixPathway> Members

		IEnumerator<CasemixPathway> IEnumerable<CasemixPathway>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CasemixPathway;
			}
		}

		#endregion
		
		private CasemixPathwayQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CasemixPathway' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("CasemixPathway ({PathwayID})")]
	[Serializable]
	public partial class CasemixPathway : esCasemixPathway
	{
		public CasemixPathway()
		{

		}
	
		public CasemixPathway(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CasemixPathwayMetadata.Meta();
			}
		}
		
		
		
		override protected esCasemixPathwayQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CasemixPathwayQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public CasemixPathwayQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CasemixPathwayQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(CasemixPathwayQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private CasemixPathwayQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class CasemixPathwayQuery : esCasemixPathwayQuery
	{
		public CasemixPathwayQuery()
		{

		}		
		
		public CasemixPathwayQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "CasemixPathwayQuery";
        }
		
			
	}


	[Serializable]
	public partial class CasemixPathwayMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CasemixPathwayMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CasemixPathwayMetadata.ColumnNames.PathwayID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CasemixPathwayMetadata.PropertyNames.PathwayID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixPathwayMetadata.ColumnNames.PathwayName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CasemixPathwayMetadata.PropertyNames.PathwayName;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixPathwayMetadata.ColumnNames.CoverageValue, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CasemixPathwayMetadata.PropertyNames.CoverageValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixPathwayMetadata.ColumnNames.Alos, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CasemixPathwayMetadata.PropertyNames.Alos;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixPathwayMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CasemixPathwayMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixPathwayMetadata.ColumnNames.IsActive, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CasemixPathwayMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixPathwayMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CasemixPathwayMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixPathwayMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CasemixPathwayMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public CasemixPathwayMetadata Meta()
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
			 public const string PathwayID = "PathwayID";
			 public const string PathwayName = "PathwayName";
			 public const string CoverageValue = "CoverageValue";
			 public const string Alos = "ALOS";
			 public const string Notes = "Notes";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PathwayID = "PathwayID";
			 public const string PathwayName = "PathwayName";
			 public const string CoverageValue = "CoverageValue";
			 public const string Alos = "Alos";
			 public const string Notes = "Notes";
			 public const string IsActive = "IsActive";
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
			lock (typeof(CasemixPathwayMetadata))
			{
				if(CasemixPathwayMetadata.mapDelegates == null)
				{
					CasemixPathwayMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CasemixPathwayMetadata.meta == null)
				{
					CasemixPathwayMetadata.meta = new CasemixPathwayMetadata();
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
				

				meta.AddTypeMap("PathwayID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PathwayName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CoverageValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Alos", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "CasemixPathway";
				meta.Destination = "CasemixPathway";
				
				meta.spInsert = "proc_CasemixPathwayInsert";				
				meta.spUpdate = "proc_CasemixPathwayUpdate";		
				meta.spDelete = "proc_CasemixPathwayDelete";
				meta.spLoadAll = "proc_CasemixPathwayLoadAll";
				meta.spLoadByPrimaryKey = "proc_CasemixPathwayLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CasemixPathwayMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
