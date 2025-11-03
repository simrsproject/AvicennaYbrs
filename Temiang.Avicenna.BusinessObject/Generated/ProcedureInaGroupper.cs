/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/8/2022 2:33:15 PM
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
	abstract public class esProcedureInaGroupperCollection : esEntityCollectionWAuditLog
	{
		public esProcedureInaGroupperCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ProcedureInaGroupperCollection";
		}

		#region Query Logic
		protected void InitQuery(esProcedureInaGroupperQuery query)
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
			this.InitQuery(query as esProcedureInaGroupperQuery);
		}
		#endregion
		
		virtual public ProcedureInaGroupper DetachEntity(ProcedureInaGroupper entity)
		{
			return base.DetachEntity(entity) as ProcedureInaGroupper;
		}
		
		virtual public ProcedureInaGroupper AttachEntity(ProcedureInaGroupper entity)
		{
			return base.AttachEntity(entity) as ProcedureInaGroupper;
		}
		
		virtual public void Combine(ProcedureInaGroupperCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ProcedureInaGroupper this[int index]
		{
			get
			{
				return base[index] as ProcedureInaGroupper;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ProcedureInaGroupper);
		}
	}



	[Serializable]
	abstract public class esProcedureInaGroupper : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esProcedureInaGroupperQuery GetDynamicQuery()
		{
			return null;
		}

		public esProcedureInaGroupper()
		{

		}

		public esProcedureInaGroupper(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String procedureID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(procedureID);
			else
				return LoadByPrimaryKeyStoredProcedure(procedureID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String procedureID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(procedureID);
			else
				return LoadByPrimaryKeyStoredProcedure(procedureID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String procedureID)
		{
			esProcedureInaGroupperQuery query = this.GetDynamicQuery();
			query.Where(query.ProcedureID == procedureID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String procedureID)
		{
			esParameters parms = new esParameters();
			parms.Add("ProcedureID",procedureID);
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
						case "ProcedureID": this.str.ProcedureID = (string)value; break;							
						case "ProcedureName": this.str.ProcedureName = (string)value; break;							
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
		/// Maps to ProcedureInaGroupper.ProcedureID
		/// </summary>
		virtual public System.String ProcedureID
		{
			get
			{
				return base.GetSystemString(ProcedureInaGroupperMetadata.ColumnNames.ProcedureID);
			}
			
			set
			{
				base.SetSystemString(ProcedureInaGroupperMetadata.ColumnNames.ProcedureID, value);
			}
		}
		
		/// <summary>
		/// Maps to ProcedureInaGroupper.ProcedureName
		/// </summary>
		virtual public System.String ProcedureName
		{
			get
			{
				return base.GetSystemString(ProcedureInaGroupperMetadata.ColumnNames.ProcedureName);
			}
			
			set
			{
				base.SetSystemString(ProcedureInaGroupperMetadata.ColumnNames.ProcedureName, value);
			}
		}
		
		/// <summary>
		/// Maps to ProcedureInaGroupper.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ProcedureInaGroupperMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ProcedureInaGroupperMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ProcedureInaGroupper.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ProcedureInaGroupperMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ProcedureInaGroupperMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esProcedureInaGroupper entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ProcedureID
			{
				get
				{
					System.String data = entity.ProcedureID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcedureID = null;
					else entity.ProcedureID = Convert.ToString(value);
				}
			}
				
			public System.String ProcedureName
			{
				get
				{
					System.String data = entity.ProcedureName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcedureName = null;
					else entity.ProcedureName = Convert.ToString(value);
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
			

			private esProcedureInaGroupper entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esProcedureInaGroupperQuery query)
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
				throw new Exception("esProcedureInaGroupper can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esProcedureInaGroupperQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ProcedureInaGroupperMetadata.Meta();
			}
		}	
		

		public esQueryItem ProcedureID
		{
			get
			{
				return new esQueryItem(this, ProcedureInaGroupperMetadata.ColumnNames.ProcedureID, esSystemType.String);
			}
		} 
		
		public esQueryItem ProcedureName
		{
			get
			{
				return new esQueryItem(this, ProcedureInaGroupperMetadata.ColumnNames.ProcedureName, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ProcedureInaGroupperMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ProcedureInaGroupperMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ProcedureInaGroupperCollection")]
	public partial class ProcedureInaGroupperCollection : esProcedureInaGroupperCollection, IEnumerable<ProcedureInaGroupper>
	{
		public ProcedureInaGroupperCollection()
		{

		}
		
		public static implicit operator List<ProcedureInaGroupper>(ProcedureInaGroupperCollection coll)
		{
			List<ProcedureInaGroupper> list = new List<ProcedureInaGroupper>();
			
			foreach (ProcedureInaGroupper emp in coll)
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
				return  ProcedureInaGroupperMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProcedureInaGroupperQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ProcedureInaGroupper(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ProcedureInaGroupper();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ProcedureInaGroupperQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProcedureInaGroupperQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ProcedureInaGroupperQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ProcedureInaGroupper AddNew()
		{
			ProcedureInaGroupper entity = base.AddNewEntity() as ProcedureInaGroupper;
			
			return entity;
		}

		public ProcedureInaGroupper FindByPrimaryKey(System.String procedureID)
		{
			return base.FindByPrimaryKey(procedureID) as ProcedureInaGroupper;
		}


		#region IEnumerable<ProcedureInaGroupper> Members

		IEnumerator<ProcedureInaGroupper> IEnumerable<ProcedureInaGroupper>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ProcedureInaGroupper;
			}
		}

		#endregion
		
		private ProcedureInaGroupperQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ProcedureInaGroupper' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ProcedureInaGroupper ({ProcedureID})")]
	[Serializable]
	public partial class ProcedureInaGroupper : esProcedureInaGroupper
	{
		public ProcedureInaGroupper()
		{

		}
	
		public ProcedureInaGroupper(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ProcedureInaGroupperMetadata.Meta();
			}
		}
		
		
		
		override protected esProcedureInaGroupperQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProcedureInaGroupperQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ProcedureInaGroupperQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProcedureInaGroupperQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ProcedureInaGroupperQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ProcedureInaGroupperQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ProcedureInaGroupperQuery : esProcedureInaGroupperQuery
	{
		public ProcedureInaGroupperQuery()
		{

		}		
		
		public ProcedureInaGroupperQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ProcedureInaGroupperQuery";
        }
		
			
	}


	[Serializable]
	public partial class ProcedureInaGroupperMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ProcedureInaGroupperMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ProcedureInaGroupperMetadata.ColumnNames.ProcedureID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ProcedureInaGroupperMetadata.PropertyNames.ProcedureID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ProcedureInaGroupperMetadata.ColumnNames.ProcedureName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ProcedureInaGroupperMetadata.PropertyNames.ProcedureName;
			c.CharacterMaxLength = 250;
			_columns.Add(c);
				
			c = new esColumnMetadata(ProcedureInaGroupperMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ProcedureInaGroupperMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ProcedureInaGroupperMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ProcedureInaGroupperMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ProcedureInaGroupperMetadata Meta()
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
			 public const string ProcedureID = "ProcedureID";
			 public const string ProcedureName = "ProcedureName";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ProcedureID = "ProcedureID";
			 public const string ProcedureName = "ProcedureName";
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
			lock (typeof(ProcedureInaGroupperMetadata))
			{
				if(ProcedureInaGroupperMetadata.mapDelegates == null)
				{
					ProcedureInaGroupperMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ProcedureInaGroupperMetadata.meta == null)
				{
					ProcedureInaGroupperMetadata.meta = new ProcedureInaGroupperMetadata();
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
				

				meta.AddTypeMap("ProcedureID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProcedureName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ProcedureInaGroupper";
				meta.Destination = "ProcedureInaGroupper";
				
				meta.spInsert = "proc_ProcedureInaGroupperInsert";				
				meta.spUpdate = "proc_ProcedureInaGroupperUpdate";		
				meta.spDelete = "proc_ProcedureInaGroupperDelete";
				meta.spLoadAll = "proc_ProcedureInaGroupperLoadAll";
				meta.spLoadByPrimaryKey = "proc_ProcedureInaGroupperLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ProcedureInaGroupperMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
