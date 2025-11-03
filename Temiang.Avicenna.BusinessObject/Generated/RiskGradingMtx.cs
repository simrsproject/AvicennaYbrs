/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/25/2015 8:24:41 AM
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
	abstract public class esRiskGradingMtxCollection : esEntityCollectionWAuditLog
	{
		public esRiskGradingMtxCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RiskGradingMtxCollection";
		}

		#region Query Logic
		protected void InitQuery(esRiskGradingMtxQuery query)
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
			this.InitQuery(query as esRiskGradingMtxQuery);
		}
		#endregion
		
		virtual public RiskGradingMtx DetachEntity(RiskGradingMtx entity)
		{
			return base.DetachEntity(entity) as RiskGradingMtx;
		}
		
		virtual public RiskGradingMtx AttachEntity(RiskGradingMtx entity)
		{
			return base.AttachEntity(entity) as RiskGradingMtx;
		}
		
		virtual public void Combine(RiskGradingMtxCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RiskGradingMtx this[int index]
		{
			get
			{
				return base[index] as RiskGradingMtx;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RiskGradingMtx);
		}
	}



	[Serializable]
	abstract public class esRiskGradingMtx : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRiskGradingMtxQuery GetDynamicQuery()
		{
			return null;
		}

		public esRiskGradingMtx()
		{

		}

		public esRiskGradingMtx(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String sRClinicalImpact, System.String sRIncidentProbabilityFrequency)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRClinicalImpact, sRIncidentProbabilityFrequency);
			else
				return LoadByPrimaryKeyStoredProcedure(sRClinicalImpact, sRIncidentProbabilityFrequency);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String sRClinicalImpact, System.String sRIncidentProbabilityFrequency)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRClinicalImpact, sRIncidentProbabilityFrequency);
			else
				return LoadByPrimaryKeyStoredProcedure(sRClinicalImpact, sRIncidentProbabilityFrequency);
		}

		private bool LoadByPrimaryKeyDynamic(System.String sRClinicalImpact, System.String sRIncidentProbabilityFrequency)
		{
			esRiskGradingMtxQuery query = this.GetDynamicQuery();
			query.Where(query.SRClinicalImpact == sRClinicalImpact, query.SRIncidentProbabilityFrequency == sRIncidentProbabilityFrequency);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String sRClinicalImpact, System.String sRIncidentProbabilityFrequency)
		{
			esParameters parms = new esParameters();
			parms.Add("SRClinicalImpact",sRClinicalImpact);			parms.Add("SRIncidentProbabilityFrequency",sRIncidentProbabilityFrequency);
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
						case "SRClinicalImpact": this.str.SRClinicalImpact = (string)value; break;							
						case "SRIncidentProbabilityFrequency": this.str.SRIncidentProbabilityFrequency = (string)value; break;							
						case "SRIncidentFollowUp": this.str.SRIncidentFollowUp = (string)value; break;							
						case "RiskGradingID": this.str.RiskGradingID = (string)value; break;							
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
		/// Maps to RiskGradingMtx.SRClinicalImpact
		/// </summary>
		virtual public System.String SRClinicalImpact
		{
			get
			{
				return base.GetSystemString(RiskGradingMtxMetadata.ColumnNames.SRClinicalImpact);
			}
			
			set
			{
				base.SetSystemString(RiskGradingMtxMetadata.ColumnNames.SRClinicalImpact, value);
			}
		}
		
		/// <summary>
		/// Maps to RiskGradingMtx.SRIncidentProbabilityFrequency
		/// </summary>
		virtual public System.String SRIncidentProbabilityFrequency
		{
			get
			{
				return base.GetSystemString(RiskGradingMtxMetadata.ColumnNames.SRIncidentProbabilityFrequency);
			}
			
			set
			{
				base.SetSystemString(RiskGradingMtxMetadata.ColumnNames.SRIncidentProbabilityFrequency, value);
			}
		}
		
		/// <summary>
		/// Maps to RiskGradingMtx.SRIncidentFollowUp
		/// </summary>
		virtual public System.String SRIncidentFollowUp
		{
			get
			{
				return base.GetSystemString(RiskGradingMtxMetadata.ColumnNames.SRIncidentFollowUp);
			}
			
			set
			{
				base.SetSystemString(RiskGradingMtxMetadata.ColumnNames.SRIncidentFollowUp, value);
			}
		}
		
		/// <summary>
		/// Maps to RiskGradingMtx.RiskGradingID
		/// </summary>
		virtual public System.String RiskGradingID
		{
			get
			{
				return base.GetSystemString(RiskGradingMtxMetadata.ColumnNames.RiskGradingID);
			}
			
			set
			{
				base.SetSystemString(RiskGradingMtxMetadata.ColumnNames.RiskGradingID, value);
			}
		}
		
		/// <summary>
		/// Maps to RiskGradingMtx.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RiskGradingMtxMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RiskGradingMtxMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RiskGradingMtx.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RiskGradingMtxMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RiskGradingMtxMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRiskGradingMtx entity)
			{
				this.entity = entity;
			}
			
	
			public System.String SRClinicalImpact
			{
				get
				{
					System.String data = entity.SRClinicalImpact;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRClinicalImpact = null;
					else entity.SRClinicalImpact = Convert.ToString(value);
				}
			}
				
			public System.String SRIncidentProbabilityFrequency
			{
				get
				{
					System.String data = entity.SRIncidentProbabilityFrequency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncidentProbabilityFrequency = null;
					else entity.SRIncidentProbabilityFrequency = Convert.ToString(value);
				}
			}
				
			public System.String SRIncidentFollowUp
			{
				get
				{
					System.String data = entity.SRIncidentFollowUp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncidentFollowUp = null;
					else entity.SRIncidentFollowUp = Convert.ToString(value);
				}
			}
				
			public System.String RiskGradingID
			{
				get
				{
					System.String data = entity.RiskGradingID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RiskGradingID = null;
					else entity.RiskGradingID = Convert.ToString(value);
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
			

			private esRiskGradingMtx entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRiskGradingMtxQuery query)
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
				throw new Exception("esRiskGradingMtx can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RiskGradingMtx : esRiskGradingMtx
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
	abstract public class esRiskGradingMtxQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RiskGradingMtxMetadata.Meta();
			}
		}	
		

		public esQueryItem SRClinicalImpact
		{
			get
			{
				return new esQueryItem(this, RiskGradingMtxMetadata.ColumnNames.SRClinicalImpact, esSystemType.String);
			}
		} 
		
		public esQueryItem SRIncidentProbabilityFrequency
		{
			get
			{
				return new esQueryItem(this, RiskGradingMtxMetadata.ColumnNames.SRIncidentProbabilityFrequency, esSystemType.String);
			}
		} 
		
		public esQueryItem SRIncidentFollowUp
		{
			get
			{
				return new esQueryItem(this, RiskGradingMtxMetadata.ColumnNames.SRIncidentFollowUp, esSystemType.String);
			}
		} 
		
		public esQueryItem RiskGradingID
		{
			get
			{
				return new esQueryItem(this, RiskGradingMtxMetadata.ColumnNames.RiskGradingID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RiskGradingMtxMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RiskGradingMtxMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RiskGradingMtxCollection")]
	public partial class RiskGradingMtxCollection : esRiskGradingMtxCollection, IEnumerable<RiskGradingMtx>
	{
		public RiskGradingMtxCollection()
		{

		}
		
		public static implicit operator List<RiskGradingMtx>(RiskGradingMtxCollection coll)
		{
			List<RiskGradingMtx> list = new List<RiskGradingMtx>();
			
			foreach (RiskGradingMtx emp in coll)
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
				return  RiskGradingMtxMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RiskGradingMtxQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RiskGradingMtx(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RiskGradingMtx();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RiskGradingMtxQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RiskGradingMtxQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RiskGradingMtxQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RiskGradingMtx AddNew()
		{
			RiskGradingMtx entity = base.AddNewEntity() as RiskGradingMtx;
			
			return entity;
		}

		public RiskGradingMtx FindByPrimaryKey(System.String sRClinicalImpact, System.String sRIncidentProbabilityFrequency)
		{
			return base.FindByPrimaryKey(sRClinicalImpact, sRIncidentProbabilityFrequency) as RiskGradingMtx;
		}


		#region IEnumerable<RiskGradingMtx> Members

		IEnumerator<RiskGradingMtx> IEnumerable<RiskGradingMtx>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RiskGradingMtx;
			}
		}

		#endregion
		
		private RiskGradingMtxQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RiskGradingMtx' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RiskGradingMtx ({SRClinicalImpact},{SRIncidentProbabilityFrequency})")]
	[Serializable]
	public partial class RiskGradingMtx : esRiskGradingMtx
	{
		public RiskGradingMtx()
		{

		}
	
		public RiskGradingMtx(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RiskGradingMtxMetadata.Meta();
			}
		}
		
		
		
		override protected esRiskGradingMtxQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RiskGradingMtxQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RiskGradingMtxQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RiskGradingMtxQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RiskGradingMtxQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RiskGradingMtxQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RiskGradingMtxQuery : esRiskGradingMtxQuery
	{
		public RiskGradingMtxQuery()
		{

		}		
		
		public RiskGradingMtxQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RiskGradingMtxQuery";
        }
		
			
	}


	[Serializable]
	public partial class RiskGradingMtxMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RiskGradingMtxMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RiskGradingMtxMetadata.ColumnNames.SRClinicalImpact, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskGradingMtxMetadata.PropertyNames.SRClinicalImpact;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(RiskGradingMtxMetadata.ColumnNames.SRIncidentProbabilityFrequency, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskGradingMtxMetadata.PropertyNames.SRIncidentProbabilityFrequency;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(RiskGradingMtxMetadata.ColumnNames.SRIncidentFollowUp, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskGradingMtxMetadata.PropertyNames.SRIncidentFollowUp;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(RiskGradingMtxMetadata.ColumnNames.RiskGradingID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskGradingMtxMetadata.PropertyNames.RiskGradingID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RiskGradingMtxMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RiskGradingMtxMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RiskGradingMtxMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskGradingMtxMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RiskGradingMtxMetadata Meta()
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
			 public const string SRClinicalImpact = "SRClinicalImpact";
			 public const string SRIncidentProbabilityFrequency = "SRIncidentProbabilityFrequency";
			 public const string SRIncidentFollowUp = "SRIncidentFollowUp";
			 public const string RiskGradingID = "RiskGradingID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SRClinicalImpact = "SRClinicalImpact";
			 public const string SRIncidentProbabilityFrequency = "SRIncidentProbabilityFrequency";
			 public const string SRIncidentFollowUp = "SRIncidentFollowUp";
			 public const string RiskGradingID = "RiskGradingID";
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
			lock (typeof(RiskGradingMtxMetadata))
			{
				if(RiskGradingMtxMetadata.mapDelegates == null)
				{
					RiskGradingMtxMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RiskGradingMtxMetadata.meta == null)
				{
					RiskGradingMtxMetadata.meta = new RiskGradingMtxMetadata();
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
				

				meta.AddTypeMap("SRClinicalImpact", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncidentProbabilityFrequency", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncidentFollowUp", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RiskGradingID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RiskGradingMtx";
				meta.Destination = "RiskGradingMtx";
				
				meta.spInsert = "proc_RiskGradingMtxInsert";				
				meta.spUpdate = "proc_RiskGradingMtxUpdate";		
				meta.spDelete = "proc_RiskGradingMtxDelete";
				meta.spLoadAll = "proc_RiskGradingMtxLoadAll";
				meta.spLoadByPrimaryKey = "proc_RiskGradingMtxLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RiskGradingMtxMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
