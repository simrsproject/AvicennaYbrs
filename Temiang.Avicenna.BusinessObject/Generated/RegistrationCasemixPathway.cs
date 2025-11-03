/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/23/2021 2:44:14 PM
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
	abstract public class esRegistrationCasemixPathwayCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationCasemixPathwayCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RegistrationCasemixPathwayCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationCasemixPathwayQuery query)
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
			this.InitQuery(query as esRegistrationCasemixPathwayQuery);
		}
		#endregion
		
		virtual public RegistrationCasemixPathway DetachEntity(RegistrationCasemixPathway entity)
		{
			return base.DetachEntity(entity) as RegistrationCasemixPathway;
		}
		
		virtual public RegistrationCasemixPathway AttachEntity(RegistrationCasemixPathway entity)
		{
			return base.AttachEntity(entity) as RegistrationCasemixPathway;
		}
		
		virtual public void Combine(RegistrationCasemixPathwayCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RegistrationCasemixPathway this[int index]
		{
			get
			{
				return base[index] as RegistrationCasemixPathway;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationCasemixPathway);
		}
	}



	[Serializable]
	abstract public class esRegistrationCasemixPathway : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationCasemixPathwayQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationCasemixPathway()
		{

		}

		public esRegistrationCasemixPathway(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String casemixPathwayID, System.String registrationNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(casemixPathwayID, registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(casemixPathwayID, registrationNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String casemixPathwayID, System.String registrationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(casemixPathwayID, registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(casemixPathwayID, registrationNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String casemixPathwayID, System.String registrationNo)
		{
			esRegistrationCasemixPathwayQuery query = this.GetDynamicQuery();
			query.Where(query.CasemixPathwayID == casemixPathwayID, query.RegistrationNo == registrationNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String casemixPathwayID, System.String registrationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("CasemixPathwayID",casemixPathwayID);			parms.Add("RegistrationNo",registrationNo);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "CasemixPathwayID": this.str.CasemixPathwayID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "PathwayStatus": this.str.PathwayStatus = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;
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
		/// Maps to RegistrationCasemixPathway.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationCasemixPathwayMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationCasemixPathwayMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationCasemixPathway.CasemixPathwayID
		/// </summary>
		virtual public System.String CasemixPathwayID
		{
			get
			{
				return base.GetSystemString(RegistrationCasemixPathwayMetadata.ColumnNames.CasemixPathwayID);
			}
			
			set
			{
				base.SetSystemString(RegistrationCasemixPathwayMetadata.ColumnNames.CasemixPathwayID, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationCasemixPathway.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationCasemixPathwayMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationCasemixPathwayMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationCasemixPathway.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationCasemixPathwayMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationCasemixPathwayMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationCasemixPathway.PathwayStatus
		/// </summary>
		virtual public System.String PathwayStatus
		{
			get
			{
				return base.GetSystemString(RegistrationCasemixPathwayMetadata.ColumnNames.PathwayStatus);
			}
			
			set
			{
				base.SetSystemString(RegistrationCasemixPathwayMetadata.ColumnNames.PathwayStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationCasemixPathway.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(RegistrationCasemixPathwayMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(RegistrationCasemixPathwayMetadata.ColumnNames.Notes, value);
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
			public esStrings(esRegistrationCasemixPathway entity)
			{
				this.entity = entity;
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
				
			public System.String CasemixPathwayID
			{
				get
				{
					System.String data = entity.CasemixPathwayID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CasemixPathwayID = null;
					else entity.CasemixPathwayID = Convert.ToString(value);
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
				
			public System.String PathwayStatus
			{
				get
				{
					System.String data = entity.PathwayStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PathwayStatus = null;
					else entity.PathwayStatus = Convert.ToString(value);
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
			

			private esRegistrationCasemixPathway entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationCasemixPathwayQuery query)
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
				throw new Exception("esRegistrationCasemixPathway can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esRegistrationCasemixPathwayQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationCasemixPathwayMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationCasemixPathwayMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem CasemixPathwayID
		{
			get
			{
				return new esQueryItem(this, RegistrationCasemixPathwayMetadata.ColumnNames.CasemixPathwayID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationCasemixPathwayMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationCasemixPathwayMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem PathwayStatus
		{
			get
			{
				return new esQueryItem(this, RegistrationCasemixPathwayMetadata.ColumnNames.PathwayStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, RegistrationCasemixPathwayMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationCasemixPathwayCollection")]
	public partial class RegistrationCasemixPathwayCollection : esRegistrationCasemixPathwayCollection, IEnumerable<RegistrationCasemixPathway>
	{
		public RegistrationCasemixPathwayCollection()
		{

		}
		
		public static implicit operator List<RegistrationCasemixPathway>(RegistrationCasemixPathwayCollection coll)
		{
			List<RegistrationCasemixPathway> list = new List<RegistrationCasemixPathway>();
			
			foreach (RegistrationCasemixPathway emp in coll)
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
				return  RegistrationCasemixPathwayMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationCasemixPathwayQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationCasemixPathway(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationCasemixPathway();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RegistrationCasemixPathwayQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationCasemixPathwayQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RegistrationCasemixPathwayQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RegistrationCasemixPathway AddNew()
		{
			RegistrationCasemixPathway entity = base.AddNewEntity() as RegistrationCasemixPathway;
			
			return entity;
		}

		public RegistrationCasemixPathway FindByPrimaryKey(System.String casemixPathwayID, System.String registrationNo)
		{
			return base.FindByPrimaryKey(casemixPathwayID, registrationNo) as RegistrationCasemixPathway;
		}


		#region IEnumerable<RegistrationCasemixPathway> Members

		IEnumerator<RegistrationCasemixPathway> IEnumerable<RegistrationCasemixPathway>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationCasemixPathway;
			}
		}

		#endregion
		
		private RegistrationCasemixPathwayQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationCasemixPathway' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RegistrationCasemixPathway ({RegistrationNo},{CasemixPathwayID})")]
	[Serializable]
	public partial class RegistrationCasemixPathway : esRegistrationCasemixPathway
	{
		public RegistrationCasemixPathway()
		{

		}
	
		public RegistrationCasemixPathway(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationCasemixPathwayMetadata.Meta();
			}
		}
		
		
		
		override protected esRegistrationCasemixPathwayQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationCasemixPathwayQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RegistrationCasemixPathwayQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationCasemixPathwayQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RegistrationCasemixPathwayQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RegistrationCasemixPathwayQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RegistrationCasemixPathwayQuery : esRegistrationCasemixPathwayQuery
	{
		public RegistrationCasemixPathwayQuery()
		{

		}		
		
		public RegistrationCasemixPathwayQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RegistrationCasemixPathwayQuery";
        }
		
			
	}


	[Serializable]
	public partial class RegistrationCasemixPathwayMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationCasemixPathwayMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationCasemixPathwayMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCasemixPathwayMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationCasemixPathwayMetadata.ColumnNames.CasemixPathwayID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCasemixPathwayMetadata.PropertyNames.CasemixPathwayID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationCasemixPathwayMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationCasemixPathwayMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationCasemixPathwayMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCasemixPathwayMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationCasemixPathwayMetadata.ColumnNames.PathwayStatus, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCasemixPathwayMetadata.PropertyNames.PathwayStatus;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationCasemixPathwayMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCasemixPathwayMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RegistrationCasemixPathwayMetadata Meta()
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
			 public const string RegistrationNo = "RegistrationNo";
			 public const string CasemixPathwayID = "CasemixPathwayID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string PathwayStatus = "PathwayStatus";
			 public const string Notes = "Notes";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string CasemixPathwayID = "CasemixPathwayID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string PathwayStatus = "PathwayStatus";
			 public const string Notes = "Notes";
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
			lock (typeof(RegistrationCasemixPathwayMetadata))
			{
				if(RegistrationCasemixPathwayMetadata.mapDelegates == null)
				{
					RegistrationCasemixPathwayMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RegistrationCasemixPathwayMetadata.meta == null)
				{
					RegistrationCasemixPathwayMetadata.meta = new RegistrationCasemixPathwayMetadata();
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
				

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CasemixPathwayID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PathwayStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RegistrationCasemixPathway";
				meta.Destination = "RegistrationCasemixPathway";
				
				meta.spInsert = "proc_RegistrationCasemixPathwayInsert";				
				meta.spUpdate = "proc_RegistrationCasemixPathwayUpdate";		
				meta.spDelete = "proc_RegistrationCasemixPathwayDelete";
				meta.spLoadAll = "proc_RegistrationCasemixPathwayLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationCasemixPathwayLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationCasemixPathwayMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
