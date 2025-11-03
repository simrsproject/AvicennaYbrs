/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/25/2015 8:24:36 AM
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
	abstract public class esRiskGradingCollection : esEntityCollectionWAuditLog
	{
		public esRiskGradingCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RiskGradingCollection";
		}

		#region Query Logic
		protected void InitQuery(esRiskGradingQuery query)
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
			this.InitQuery(query as esRiskGradingQuery);
		}
		#endregion
		
		virtual public RiskGrading DetachEntity(RiskGrading entity)
		{
			return base.DetachEntity(entity) as RiskGrading;
		}
		
		virtual public RiskGrading AttachEntity(RiskGrading entity)
		{
			return base.AttachEntity(entity) as RiskGrading;
		}
		
		virtual public void Combine(RiskGradingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RiskGrading this[int index]
		{
			get
			{
				return base[index] as RiskGrading;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RiskGrading);
		}
	}



	[Serializable]
	abstract public class esRiskGrading : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRiskGradingQuery GetDynamicQuery()
		{
			return null;
		}

		public esRiskGrading()
		{

		}

		public esRiskGrading(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String riskGradingID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(riskGradingID);
			else
				return LoadByPrimaryKeyStoredProcedure(riskGradingID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String riskGradingID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(riskGradingID);
			else
				return LoadByPrimaryKeyStoredProcedure(riskGradingID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String riskGradingID)
		{
			esRiskGradingQuery query = this.GetDynamicQuery();
			query.Where(query.RiskGradingID == riskGradingID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String riskGradingID)
		{
			esParameters parms = new esParameters();
			parms.Add("RiskGradingID",riskGradingID);
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
						case "RiskGradingID": this.str.RiskGradingID = (string)value; break;							
						case "RiskGradingName": this.str.RiskGradingName = (string)value; break;							
						case "RiskGradingColor": this.str.RiskGradingColor = (string)value; break;							
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
		/// Maps to RiskGrading.RiskGradingID
		/// </summary>
		virtual public System.String RiskGradingID
		{
			get
			{
				return base.GetSystemString(RiskGradingMetadata.ColumnNames.RiskGradingID);
			}
			
			set
			{
				base.SetSystemString(RiskGradingMetadata.ColumnNames.RiskGradingID, value);
			}
		}
		
		/// <summary>
		/// Maps to RiskGrading.RiskGradingName
		/// </summary>
		virtual public System.String RiskGradingName
		{
			get
			{
				return base.GetSystemString(RiskGradingMetadata.ColumnNames.RiskGradingName);
			}
			
			set
			{
				base.SetSystemString(RiskGradingMetadata.ColumnNames.RiskGradingName, value);
			}
		}
		
		/// <summary>
		/// Maps to RiskGrading.RiskGradingColor
		/// </summary>
		virtual public System.String RiskGradingColor
		{
			get
			{
				return base.GetSystemString(RiskGradingMetadata.ColumnNames.RiskGradingColor);
			}
			
			set
			{
				base.SetSystemString(RiskGradingMetadata.ColumnNames.RiskGradingColor, value);
			}
		}
		
		/// <summary>
		/// Maps to RiskGrading.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RiskGradingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RiskGradingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RiskGrading.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RiskGradingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RiskGradingMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRiskGrading entity)
			{
				this.entity = entity;
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
				
			public System.String RiskGradingName
			{
				get
				{
					System.String data = entity.RiskGradingName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RiskGradingName = null;
					else entity.RiskGradingName = Convert.ToString(value);
				}
			}
				
			public System.String RiskGradingColor
			{
				get
				{
					System.String data = entity.RiskGradingColor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RiskGradingColor = null;
					else entity.RiskGradingColor = Convert.ToString(value);
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
			

			private esRiskGrading entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRiskGradingQuery query)
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
				throw new Exception("esRiskGrading can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RiskGrading : esRiskGrading
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
	abstract public class esRiskGradingQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RiskGradingMetadata.Meta();
			}
		}	
		

		public esQueryItem RiskGradingID
		{
			get
			{
				return new esQueryItem(this, RiskGradingMetadata.ColumnNames.RiskGradingID, esSystemType.String);
			}
		} 
		
		public esQueryItem RiskGradingName
		{
			get
			{
				return new esQueryItem(this, RiskGradingMetadata.ColumnNames.RiskGradingName, esSystemType.String);
			}
		} 
		
		public esQueryItem RiskGradingColor
		{
			get
			{
				return new esQueryItem(this, RiskGradingMetadata.ColumnNames.RiskGradingColor, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RiskGradingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RiskGradingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RiskGradingCollection")]
	public partial class RiskGradingCollection : esRiskGradingCollection, IEnumerable<RiskGrading>
	{
		public RiskGradingCollection()
		{

		}
		
		public static implicit operator List<RiskGrading>(RiskGradingCollection coll)
		{
			List<RiskGrading> list = new List<RiskGrading>();
			
			foreach (RiskGrading emp in coll)
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
				return  RiskGradingMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RiskGradingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RiskGrading(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RiskGrading();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RiskGradingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RiskGradingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RiskGradingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RiskGrading AddNew()
		{
			RiskGrading entity = base.AddNewEntity() as RiskGrading;
			
			return entity;
		}

		public RiskGrading FindByPrimaryKey(System.String riskGradingID)
		{
			return base.FindByPrimaryKey(riskGradingID) as RiskGrading;
		}


		#region IEnumerable<RiskGrading> Members

		IEnumerator<RiskGrading> IEnumerable<RiskGrading>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RiskGrading;
			}
		}

		#endregion
		
		private RiskGradingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RiskGrading' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RiskGrading ({RiskGradingID})")]
	[Serializable]
	public partial class RiskGrading : esRiskGrading
	{
		public RiskGrading()
		{

		}
	
		public RiskGrading(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RiskGradingMetadata.Meta();
			}
		}
		
		
		
		override protected esRiskGradingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RiskGradingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RiskGradingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RiskGradingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RiskGradingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RiskGradingQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RiskGradingQuery : esRiskGradingQuery
	{
		public RiskGradingQuery()
		{

		}		
		
		public RiskGradingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RiskGradingQuery";
        }
		
			
	}


	[Serializable]
	public partial class RiskGradingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RiskGradingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RiskGradingMetadata.ColumnNames.RiskGradingID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskGradingMetadata.PropertyNames.RiskGradingID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RiskGradingMetadata.ColumnNames.RiskGradingName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskGradingMetadata.PropertyNames.RiskGradingName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(RiskGradingMetadata.ColumnNames.RiskGradingColor, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskGradingMetadata.PropertyNames.RiskGradingColor;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RiskGradingMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RiskGradingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RiskGradingMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskGradingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RiskGradingMetadata Meta()
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
			 public const string RiskGradingID = "RiskGradingID";
			 public const string RiskGradingName = "RiskGradingName";
			 public const string RiskGradingColor = "RiskGradingColor";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RiskGradingID = "RiskGradingID";
			 public const string RiskGradingName = "RiskGradingName";
			 public const string RiskGradingColor = "RiskGradingColor";
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
			lock (typeof(RiskGradingMetadata))
			{
				if(RiskGradingMetadata.mapDelegates == null)
				{
					RiskGradingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RiskGradingMetadata.meta == null)
				{
					RiskGradingMetadata.meta = new RiskGradingMetadata();
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
				

				meta.AddTypeMap("RiskGradingID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RiskGradingName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RiskGradingColor", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RiskGrading";
				meta.Destination = "RiskGrading";
				
				meta.spInsert = "proc_RiskGradingInsert";				
				meta.spUpdate = "proc_RiskGradingUpdate";		
				meta.spDelete = "proc_RiskGradingDelete";
				meta.spLoadAll = "proc_RiskGradingLoadAll";
				meta.spLoadByPrimaryKey = "proc_RiskGradingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RiskGradingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
