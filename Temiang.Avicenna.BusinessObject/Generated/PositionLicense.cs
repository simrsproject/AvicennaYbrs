/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:22 PM
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
	abstract public class esPositionLicenseCollection : esEntityCollectionWAuditLog
	{
		public esPositionLicenseCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PositionLicenseCollection";
		}

		#region Query Logic
		protected void InitQuery(esPositionLicenseQuery query)
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
			this.InitQuery(query as esPositionLicenseQuery);
		}
		#endregion
		
		virtual public PositionLicense DetachEntity(PositionLicense entity)
		{
			return base.DetachEntity(entity) as PositionLicense;
		}
		
		virtual public PositionLicense AttachEntity(PositionLicense entity)
		{
			return base.AttachEntity(entity) as PositionLicense;
		}
		
		virtual public void Combine(PositionLicenseCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PositionLicense this[int index]
		{
			get
			{
				return base[index] as PositionLicense;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PositionLicense);
		}
	}



	[Serializable]
	abstract public class esPositionLicense : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPositionLicenseQuery GetDynamicQuery()
		{
			return null;
		}

		public esPositionLicense()
		{

		}

		public esPositionLicense(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 positionLicenseID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionLicenseID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionLicenseID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 positionLicenseID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionLicenseID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionLicenseID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 positionLicenseID)
		{
			esPositionLicenseQuery query = this.GetDynamicQuery();
			query.Where(query.PositionLicenseID == positionLicenseID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 positionLicenseID)
		{
			esParameters parms = new esParameters();
			parms.Add("PositionLicenseID",positionLicenseID);
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
						case "PositionLicenseID": this.str.PositionLicenseID = (string)value; break;							
						case "PositionID": this.str.PositionID = (string)value; break;							
						case "SRRequirement": this.str.SRRequirement = (string)value; break;							
						case "SRLicenseType": this.str.SRLicenseType = (string)value; break;							
						case "LicenseNotes": this.str.LicenseNotes = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PositionLicenseID":
						
							if (value == null || value is System.Int32)
								this.PositionLicenseID = (System.Int32?)value;
							break;
						
						case "PositionID":
						
							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
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
		/// Maps to PositionLicense.PositionLicenseID
		/// </summary>
		virtual public System.Int32? PositionLicenseID
		{
			get
			{
				return base.GetSystemInt32(PositionLicenseMetadata.ColumnNames.PositionLicenseID);
			}
			
			set
			{
				base.SetSystemInt32(PositionLicenseMetadata.ColumnNames.PositionLicenseID, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionLicense.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(PositionLicenseMetadata.ColumnNames.PositionID);
			}
			
			set
			{
				base.SetSystemInt32(PositionLicenseMetadata.ColumnNames.PositionID, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionLicense.SRRequirement
		/// </summary>
		virtual public System.String SRRequirement
		{
			get
			{
				return base.GetSystemString(PositionLicenseMetadata.ColumnNames.SRRequirement);
			}
			
			set
			{
				base.SetSystemString(PositionLicenseMetadata.ColumnNames.SRRequirement, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionLicense.SRLicenseType
		/// </summary>
		virtual public System.String SRLicenseType
		{
			get
			{
				return base.GetSystemString(PositionLicenseMetadata.ColumnNames.SRLicenseType);
			}
			
			set
			{
				base.SetSystemString(PositionLicenseMetadata.ColumnNames.SRLicenseType, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionLicense.LicenseNotes
		/// </summary>
		virtual public System.String LicenseNotes
		{
			get
			{
				return base.GetSystemString(PositionLicenseMetadata.ColumnNames.LicenseNotes);
			}
			
			set
			{
				base.SetSystemString(PositionLicenseMetadata.ColumnNames.LicenseNotes, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionLicense.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PositionLicenseMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PositionLicenseMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionLicense.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PositionLicenseMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PositionLicenseMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPositionLicense entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PositionLicenseID
			{
				get
				{
					System.Int32? data = entity.PositionLicenseID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionLicenseID = null;
					else entity.PositionLicenseID = Convert.ToInt32(value);
				}
			}
				
			public System.String PositionID
			{
				get
				{
					System.Int32? data = entity.PositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionID = null;
					else entity.PositionID = Convert.ToInt32(value);
				}
			}
				
			public System.String SRRequirement
			{
				get
				{
					System.String data = entity.SRRequirement;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRequirement = null;
					else entity.SRRequirement = Convert.ToString(value);
				}
			}
				
			public System.String SRLicenseType
			{
				get
				{
					System.String data = entity.SRLicenseType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRLicenseType = null;
					else entity.SRLicenseType = Convert.ToString(value);
				}
			}
				
			public System.String LicenseNotes
			{
				get
				{
					System.String data = entity.LicenseNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LicenseNotes = null;
					else entity.LicenseNotes = Convert.ToString(value);
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
			

			private esPositionLicense entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPositionLicenseQuery query)
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
				throw new Exception("esPositionLicense can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PositionLicense : esPositionLicense
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
	abstract public class esPositionLicenseQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PositionLicenseMetadata.Meta();
			}
		}	
		

		public esQueryItem PositionLicenseID
		{
			get
			{
				return new esQueryItem(this, PositionLicenseMetadata.ColumnNames.PositionLicenseID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, PositionLicenseMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRRequirement
		{
			get
			{
				return new esQueryItem(this, PositionLicenseMetadata.ColumnNames.SRRequirement, esSystemType.String);
			}
		} 
		
		public esQueryItem SRLicenseType
		{
			get
			{
				return new esQueryItem(this, PositionLicenseMetadata.ColumnNames.SRLicenseType, esSystemType.String);
			}
		} 
		
		public esQueryItem LicenseNotes
		{
			get
			{
				return new esQueryItem(this, PositionLicenseMetadata.ColumnNames.LicenseNotes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PositionLicenseMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PositionLicenseMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PositionLicenseCollection")]
	public partial class PositionLicenseCollection : esPositionLicenseCollection, IEnumerable<PositionLicense>
	{
		public PositionLicenseCollection()
		{

		}
		
		public static implicit operator List<PositionLicense>(PositionLicenseCollection coll)
		{
			List<PositionLicense> list = new List<PositionLicense>();
			
			foreach (PositionLicense emp in coll)
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
				return  PositionLicenseMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionLicenseQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PositionLicense(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PositionLicense();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PositionLicenseQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionLicenseQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PositionLicenseQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PositionLicense AddNew()
		{
			PositionLicense entity = base.AddNewEntity() as PositionLicense;
			
			return entity;
		}

		public PositionLicense FindByPrimaryKey(System.Int32 positionLicenseID)
		{
			return base.FindByPrimaryKey(positionLicenseID) as PositionLicense;
		}


		#region IEnumerable<PositionLicense> Members

		IEnumerator<PositionLicense> IEnumerable<PositionLicense>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PositionLicense;
			}
		}

		#endregion
		
		private PositionLicenseQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PositionLicense' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PositionLicense ({PositionLicenseID})")]
	[Serializable]
	public partial class PositionLicense : esPositionLicense
	{
		public PositionLicense()
		{

		}
	
		public PositionLicense(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PositionLicenseMetadata.Meta();
			}
		}
		
		
		
		override protected esPositionLicenseQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionLicenseQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PositionLicenseQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionLicenseQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PositionLicenseQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PositionLicenseQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PositionLicenseQuery : esPositionLicenseQuery
	{
		public PositionLicenseQuery()
		{

		}		
		
		public PositionLicenseQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PositionLicenseQuery";
        }
		
			
	}


	[Serializable]
	public partial class PositionLicenseMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PositionLicenseMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PositionLicenseMetadata.ColumnNames.PositionLicenseID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionLicenseMetadata.PropertyNames.PositionLicenseID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionLicenseMetadata.ColumnNames.PositionID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionLicenseMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionLicenseMetadata.ColumnNames.SRRequirement, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionLicenseMetadata.PropertyNames.SRRequirement;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionLicenseMetadata.ColumnNames.SRLicenseType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionLicenseMetadata.PropertyNames.SRLicenseType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionLicenseMetadata.ColumnNames.LicenseNotes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionLicenseMetadata.PropertyNames.LicenseNotes;
			c.CharacterMaxLength = 400;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionLicenseMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PositionLicenseMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionLicenseMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionLicenseMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PositionLicenseMetadata Meta()
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
			 public const string PositionLicenseID = "PositionLicenseID";
			 public const string PositionID = "PositionID";
			 public const string SRRequirement = "SRRequirement";
			 public const string SRLicenseType = "SRLicenseType";
			 public const string LicenseNotes = "LicenseNotes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PositionLicenseID = "PositionLicenseID";
			 public const string PositionID = "PositionID";
			 public const string SRRequirement = "SRRequirement";
			 public const string SRLicenseType = "SRLicenseType";
			 public const string LicenseNotes = "LicenseNotes";
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
			lock (typeof(PositionLicenseMetadata))
			{
				if(PositionLicenseMetadata.mapDelegates == null)
				{
					PositionLicenseMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PositionLicenseMetadata.meta == null)
				{
					PositionLicenseMetadata.meta = new PositionLicenseMetadata();
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
				

				meta.AddTypeMap("PositionLicenseID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRRequirement", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRLicenseType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LicenseNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "PositionLicense";
				meta.Destination = "PositionLicense";
				
				meta.spInsert = "proc_PositionLicenseInsert";				
				meta.spUpdate = "proc_PositionLicenseUpdate";		
				meta.spDelete = "proc_PositionLicenseDelete";
				meta.spLoadAll = "proc_PositionLicenseLoadAll";
				meta.spLoadByPrimaryKey = "proc_PositionLicenseLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PositionLicenseMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
