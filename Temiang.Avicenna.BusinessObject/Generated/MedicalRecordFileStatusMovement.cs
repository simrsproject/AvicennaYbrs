/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/14/2014 1:39:58 PM
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
	abstract public class esMedicalRecordFileStatusMovementCollection : esEntityCollectionWAuditLog
	{
		public esMedicalRecordFileStatusMovementCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "MedicalRecordFileStatusMovementCollection";
		}

		#region Query Logic
		protected void InitQuery(esMedicalRecordFileStatusMovementQuery query)
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
			this.InitQuery(query as esMedicalRecordFileStatusMovementQuery);
		}
		#endregion
		
		virtual public MedicalRecordFileStatusMovement DetachEntity(MedicalRecordFileStatusMovement entity)
		{
			return base.DetachEntity(entity) as MedicalRecordFileStatusMovement;
		}
		
		virtual public MedicalRecordFileStatusMovement AttachEntity(MedicalRecordFileStatusMovement entity)
		{
			return base.AttachEntity(entity) as MedicalRecordFileStatusMovement;
		}
		
		virtual public void Combine(MedicalRecordFileStatusMovementCollection collection)
		{
			base.Combine(collection);
		}
		
		new public MedicalRecordFileStatusMovement this[int index]
		{
			get
			{
				return base[index] as MedicalRecordFileStatusMovement;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicalRecordFileStatusMovement);
		}
	}



	[Serializable]
	abstract public class esMedicalRecordFileStatusMovement : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicalRecordFileStatusMovementQuery GetDynamicQuery()
		{
			return null;
		}

		public esMedicalRecordFileStatusMovement()
		{

		}

		public esMedicalRecordFileStatusMovement(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String lastPositionServiceUnitID, System.String registrationNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(lastPositionServiceUnitID, registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(lastPositionServiceUnitID, registrationNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String lastPositionServiceUnitID, System.String registrationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(lastPositionServiceUnitID, registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(lastPositionServiceUnitID, registrationNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String lastPositionServiceUnitID, System.String registrationNo)
		{
			esMedicalRecordFileStatusMovementQuery query = this.GetDynamicQuery();
			query.Where(query.LastPositionServiceUnitID == lastPositionServiceUnitID, query.RegistrationNo == registrationNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String lastPositionServiceUnitID, System.String registrationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("LastPositionServiceUnitID",lastPositionServiceUnitID);			parms.Add("RegistrationNo",registrationNo);
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
						case "LastPositionServiceUnitID": this.str.LastPositionServiceUnitID = (string)value; break;							
						case "LastPositionDateTime": this.str.LastPositionDateTime = (string)value; break;							
						case "LastPositionUserID": this.str.LastPositionUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "LastPositionDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastPositionDateTime = (System.DateTime?)value;
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
		/// Maps to MedicalRecordFileStatusMovement.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileStatusMovementMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(MedicalRecordFileStatusMovementMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalRecordFileStatusMovement.LastPositionServiceUnitID
		/// </summary>
		virtual public System.String LastPositionServiceUnitID
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileStatusMovementMetadata.ColumnNames.LastPositionServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(MedicalRecordFileStatusMovementMetadata.ColumnNames.LastPositionServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalRecordFileStatusMovement.LastPositionDateTime
		/// </summary>
		virtual public System.DateTime? LastPositionDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalRecordFileStatusMovementMetadata.ColumnNames.LastPositionDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MedicalRecordFileStatusMovementMetadata.ColumnNames.LastPositionDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalRecordFileStatusMovement.LastPositionUserID
		/// </summary>
		virtual public System.String LastPositionUserID
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileStatusMovementMetadata.ColumnNames.LastPositionUserID);
			}
			
			set
			{
				base.SetSystemString(MedicalRecordFileStatusMovementMetadata.ColumnNames.LastPositionUserID, value);
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
			public esStrings(esMedicalRecordFileStatusMovement entity)
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
				
			public System.String LastPositionServiceUnitID
			{
				get
				{
					System.String data = entity.LastPositionServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastPositionServiceUnitID = null;
					else entity.LastPositionServiceUnitID = Convert.ToString(value);
				}
			}
				
			public System.String LastPositionDateTime
			{
				get
				{
					System.DateTime? data = entity.LastPositionDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastPositionDateTime = null;
					else entity.LastPositionDateTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String LastPositionUserID
			{
				get
				{
					System.String data = entity.LastPositionUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastPositionUserID = null;
					else entity.LastPositionUserID = Convert.ToString(value);
				}
			}
			

			private esMedicalRecordFileStatusMovement entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicalRecordFileStatusMovementQuery query)
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
				throw new Exception("esMedicalRecordFileStatusMovement can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esMedicalRecordFileStatusMovementQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return MedicalRecordFileStatusMovementMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileStatusMovementMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem LastPositionServiceUnitID
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileStatusMovementMetadata.ColumnNames.LastPositionServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastPositionDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileStatusMovementMetadata.ColumnNames.LastPositionDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastPositionUserID
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileStatusMovementMetadata.ColumnNames.LastPositionUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicalRecordFileStatusMovementCollection")]
	public partial class MedicalRecordFileStatusMovementCollection : esMedicalRecordFileStatusMovementCollection, IEnumerable<MedicalRecordFileStatusMovement>
	{
		public MedicalRecordFileStatusMovementCollection()
		{

		}
		
		public static implicit operator List<MedicalRecordFileStatusMovement>(MedicalRecordFileStatusMovementCollection coll)
		{
			List<MedicalRecordFileStatusMovement> list = new List<MedicalRecordFileStatusMovement>();
			
			foreach (MedicalRecordFileStatusMovement emp in coll)
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
				return  MedicalRecordFileStatusMovementMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalRecordFileStatusMovementQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicalRecordFileStatusMovement(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicalRecordFileStatusMovement();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public MedicalRecordFileStatusMovementQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalRecordFileStatusMovementQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(MedicalRecordFileStatusMovementQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public MedicalRecordFileStatusMovement AddNew()
		{
			MedicalRecordFileStatusMovement entity = base.AddNewEntity() as MedicalRecordFileStatusMovement;
			
			return entity;
		}

		public MedicalRecordFileStatusMovement FindByPrimaryKey(System.String lastPositionServiceUnitID, System.String registrationNo)
		{
			return base.FindByPrimaryKey(lastPositionServiceUnitID, registrationNo) as MedicalRecordFileStatusMovement;
		}


		#region IEnumerable<MedicalRecordFileStatusMovement> Members

		IEnumerator<MedicalRecordFileStatusMovement> IEnumerable<MedicalRecordFileStatusMovement>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as MedicalRecordFileStatusMovement;
			}
		}

		#endregion
		
		private MedicalRecordFileStatusMovementQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicalRecordFileStatusMovement' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("MedicalRecordFileStatusMovement ({RegistrationNo},{LastPositionServiceUnitID})")]
	[Serializable]
	public partial class MedicalRecordFileStatusMovement : esMedicalRecordFileStatusMovement
	{
		public MedicalRecordFileStatusMovement()
		{

		}
	
		public MedicalRecordFileStatusMovement(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicalRecordFileStatusMovementMetadata.Meta();
			}
		}
		
		
		
		override protected esMedicalRecordFileStatusMovementQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalRecordFileStatusMovementQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public MedicalRecordFileStatusMovementQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalRecordFileStatusMovementQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(MedicalRecordFileStatusMovementQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private MedicalRecordFileStatusMovementQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class MedicalRecordFileStatusMovementQuery : esMedicalRecordFileStatusMovementQuery
	{
		public MedicalRecordFileStatusMovementQuery()
		{

		}		
		
		public MedicalRecordFileStatusMovementQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "MedicalRecordFileStatusMovementQuery";
        }
		
			
	}


	[Serializable]
	public partial class MedicalRecordFileStatusMovementMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicalRecordFileStatusMovementMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MedicalRecordFileStatusMovementMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileStatusMovementMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalRecordFileStatusMovementMetadata.ColumnNames.LastPositionServiceUnitID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileStatusMovementMetadata.PropertyNames.LastPositionServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalRecordFileStatusMovementMetadata.ColumnNames.LastPositionDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalRecordFileStatusMovementMetadata.PropertyNames.LastPositionDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalRecordFileStatusMovementMetadata.ColumnNames.LastPositionUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileStatusMovementMetadata.PropertyNames.LastPositionUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public MedicalRecordFileStatusMovementMetadata Meta()
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
			 public const string LastPositionServiceUnitID = "LastPositionServiceUnitID";
			 public const string LastPositionDateTime = "LastPositionDateTime";
			 public const string LastPositionUserID = "LastPositionUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string LastPositionServiceUnitID = "LastPositionServiceUnitID";
			 public const string LastPositionDateTime = "LastPositionDateTime";
			 public const string LastPositionUserID = "LastPositionUserID";
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
			lock (typeof(MedicalRecordFileStatusMovementMetadata))
			{
				if(MedicalRecordFileStatusMovementMetadata.mapDelegates == null)
				{
					MedicalRecordFileStatusMovementMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MedicalRecordFileStatusMovementMetadata.meta == null)
				{
					MedicalRecordFileStatusMovementMetadata.meta = new MedicalRecordFileStatusMovementMetadata();
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
				meta.AddTypeMap("LastPositionServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastPositionDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastPositionUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "MedicalRecordFileStatusMovement";
				meta.Destination = "MedicalRecordFileStatusMovement";
				
				meta.spInsert = "proc_MedicalRecordFileStatusMovementInsert";				
				meta.spUpdate = "proc_MedicalRecordFileStatusMovementUpdate";		
				meta.spDelete = "proc_MedicalRecordFileStatusMovementDelete";
				meta.spLoadAll = "proc_MedicalRecordFileStatusMovementLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicalRecordFileStatusMovementLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicalRecordFileStatusMovementMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
