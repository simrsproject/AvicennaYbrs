/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:13 PM
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
	abstract public class esCompanyEducationProfileCollection : esEntityCollectionWAuditLog
	{
		public esCompanyEducationProfileCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "CompanyEducationProfileCollection";
		}

		#region Query Logic
		protected void InitQuery(esCompanyEducationProfileQuery query)
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
			this.InitQuery(query as esCompanyEducationProfileQuery);
		}
		#endregion
		
		virtual public CompanyEducationProfile DetachEntity(CompanyEducationProfile entity)
		{
			return base.DetachEntity(entity) as CompanyEducationProfile;
		}
		
		virtual public CompanyEducationProfile AttachEntity(CompanyEducationProfile entity)
		{
			return base.AttachEntity(entity) as CompanyEducationProfile;
		}
		
		virtual public void Combine(CompanyEducationProfileCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CompanyEducationProfile this[int index]
		{
			get
			{
				return base[index] as CompanyEducationProfile;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CompanyEducationProfile);
		}
	}



	[Serializable]
	abstract public class esCompanyEducationProfile : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCompanyEducationProfileQuery GetDynamicQuery()
		{
			return null;
		}

		public esCompanyEducationProfile()
		{

		}

		public esCompanyEducationProfile(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 companyEducationProfileID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(companyEducationProfileID);
			else
				return LoadByPrimaryKeyStoredProcedure(companyEducationProfileID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 companyEducationProfileID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(companyEducationProfileID);
			else
				return LoadByPrimaryKeyStoredProcedure(companyEducationProfileID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 companyEducationProfileID)
		{
			esCompanyEducationProfileQuery query = this.GetDynamicQuery();
			query.Where(query.CompanyEducationProfileID == companyEducationProfileID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 companyEducationProfileID)
		{
			esParameters parms = new esParameters();
			parms.Add("CompanyEducationProfileID",companyEducationProfileID);
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
						case "CompanyEducationProfileID": this.str.CompanyEducationProfileID = (string)value; break;							
						case "CompanyLaborProfileID": this.str.CompanyLaborProfileID = (string)value; break;							
						case "CompanyEducationProfileCode": this.str.CompanyEducationProfileCode = (string)value; break;							
						case "CompanyEducationProfileName": this.str.CompanyEducationProfileName = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CompanyEducationProfileID":
						
							if (value == null || value is System.Int32)
								this.CompanyEducationProfileID = (System.Int32?)value;
							break;
						
						case "CompanyLaborProfileID":
						
							if (value == null || value is System.Int32)
								this.CompanyLaborProfileID = (System.Int32?)value;
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
		/// Maps to CompanyEducationProfile.CompanyEducationProfileID
		/// </summary>
		virtual public System.Int32? CompanyEducationProfileID
		{
			get
			{
				return base.GetSystemInt32(CompanyEducationProfileMetadata.ColumnNames.CompanyEducationProfileID);
			}
			
			set
			{
				base.SetSystemInt32(CompanyEducationProfileMetadata.ColumnNames.CompanyEducationProfileID, value);
			}
		}
		
		/// <summary>
		/// Maps to CompanyEducationProfile.CompanyLaborProfileID
		/// </summary>
		virtual public System.Int32? CompanyLaborProfileID
		{
			get
			{
				return base.GetSystemInt32(CompanyEducationProfileMetadata.ColumnNames.CompanyLaborProfileID);
			}
			
			set
			{
				base.SetSystemInt32(CompanyEducationProfileMetadata.ColumnNames.CompanyLaborProfileID, value);
			}
		}
		
		/// <summary>
		/// Maps to CompanyEducationProfile.CompanyEducationProfileCode
		/// </summary>
		virtual public System.String CompanyEducationProfileCode
		{
			get
			{
				return base.GetSystemString(CompanyEducationProfileMetadata.ColumnNames.CompanyEducationProfileCode);
			}
			
			set
			{
				base.SetSystemString(CompanyEducationProfileMetadata.ColumnNames.CompanyEducationProfileCode, value);
			}
		}
		
		/// <summary>
		/// Maps to CompanyEducationProfile.CompanyEducationProfileName
		/// </summary>
		virtual public System.String CompanyEducationProfileName
		{
			get
			{
				return base.GetSystemString(CompanyEducationProfileMetadata.ColumnNames.CompanyEducationProfileName);
			}
			
			set
			{
				base.SetSystemString(CompanyEducationProfileMetadata.ColumnNames.CompanyEducationProfileName, value);
			}
		}
		
		/// <summary>
		/// Maps to CompanyEducationProfile.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CompanyEducationProfileMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CompanyEducationProfileMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to CompanyEducationProfile.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CompanyEducationProfileMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(CompanyEducationProfileMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCompanyEducationProfile entity)
			{
				this.entity = entity;
			}
			
	
			public System.String CompanyEducationProfileID
			{
				get
				{
					System.Int32? data = entity.CompanyEducationProfileID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompanyEducationProfileID = null;
					else entity.CompanyEducationProfileID = Convert.ToInt32(value);
				}
			}
				
			public System.String CompanyLaborProfileID
			{
				get
				{
					System.Int32? data = entity.CompanyLaborProfileID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompanyLaborProfileID = null;
					else entity.CompanyLaborProfileID = Convert.ToInt32(value);
				}
			}
				
			public System.String CompanyEducationProfileCode
			{
				get
				{
					System.String data = entity.CompanyEducationProfileCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompanyEducationProfileCode = null;
					else entity.CompanyEducationProfileCode = Convert.ToString(value);
				}
			}
				
			public System.String CompanyEducationProfileName
			{
				get
				{
					System.String data = entity.CompanyEducationProfileName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompanyEducationProfileName = null;
					else entity.CompanyEducationProfileName = Convert.ToString(value);
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
			

			private esCompanyEducationProfile entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCompanyEducationProfileQuery query)
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
				throw new Exception("esCompanyEducationProfile can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class CompanyEducationProfile : esCompanyEducationProfile
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
	abstract public class esCompanyEducationProfileQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CompanyEducationProfileMetadata.Meta();
			}
		}	
		

		public esQueryItem CompanyEducationProfileID
		{
			get
			{
				return new esQueryItem(this, CompanyEducationProfileMetadata.ColumnNames.CompanyEducationProfileID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem CompanyLaborProfileID
		{
			get
			{
				return new esQueryItem(this, CompanyEducationProfileMetadata.ColumnNames.CompanyLaborProfileID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem CompanyEducationProfileCode
		{
			get
			{
				return new esQueryItem(this, CompanyEducationProfileMetadata.ColumnNames.CompanyEducationProfileCode, esSystemType.String);
			}
		} 
		
		public esQueryItem CompanyEducationProfileName
		{
			get
			{
				return new esQueryItem(this, CompanyEducationProfileMetadata.ColumnNames.CompanyEducationProfileName, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CompanyEducationProfileMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CompanyEducationProfileMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CompanyEducationProfileCollection")]
	public partial class CompanyEducationProfileCollection : esCompanyEducationProfileCollection, IEnumerable<CompanyEducationProfile>
	{
		public CompanyEducationProfileCollection()
		{

		}
		
		public static implicit operator List<CompanyEducationProfile>(CompanyEducationProfileCollection coll)
		{
			List<CompanyEducationProfile> list = new List<CompanyEducationProfile>();
			
			foreach (CompanyEducationProfile emp in coll)
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
				return  CompanyEducationProfileMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CompanyEducationProfileQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CompanyEducationProfile(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CompanyEducationProfile();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public CompanyEducationProfileQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CompanyEducationProfileQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(CompanyEducationProfileQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public CompanyEducationProfile AddNew()
		{
			CompanyEducationProfile entity = base.AddNewEntity() as CompanyEducationProfile;
			
			return entity;
		}

		public CompanyEducationProfile FindByPrimaryKey(System.Int32 companyEducationProfileID)
		{
			return base.FindByPrimaryKey(companyEducationProfileID) as CompanyEducationProfile;
		}


		#region IEnumerable<CompanyEducationProfile> Members

		IEnumerator<CompanyEducationProfile> IEnumerable<CompanyEducationProfile>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CompanyEducationProfile;
			}
		}

		#endregion
		
		private CompanyEducationProfileQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CompanyEducationProfile' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("CompanyEducationProfile ({CompanyEducationProfileID})")]
	[Serializable]
	public partial class CompanyEducationProfile : esCompanyEducationProfile
	{
		public CompanyEducationProfile()
		{

		}
	
		public CompanyEducationProfile(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CompanyEducationProfileMetadata.Meta();
			}
		}
		
		
		
		override protected esCompanyEducationProfileQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CompanyEducationProfileQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public CompanyEducationProfileQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CompanyEducationProfileQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(CompanyEducationProfileQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private CompanyEducationProfileQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class CompanyEducationProfileQuery : esCompanyEducationProfileQuery
	{
		public CompanyEducationProfileQuery()
		{

		}		
		
		public CompanyEducationProfileQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "CompanyEducationProfileQuery";
        }
		
			
	}


	[Serializable]
	public partial class CompanyEducationProfileMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CompanyEducationProfileMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CompanyEducationProfileMetadata.ColumnNames.CompanyEducationProfileID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CompanyEducationProfileMetadata.PropertyNames.CompanyEducationProfileID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompanyEducationProfileMetadata.ColumnNames.CompanyLaborProfileID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CompanyEducationProfileMetadata.PropertyNames.CompanyLaborProfileID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompanyEducationProfileMetadata.ColumnNames.CompanyEducationProfileCode, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CompanyEducationProfileMetadata.PropertyNames.CompanyEducationProfileCode;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompanyEducationProfileMetadata.ColumnNames.CompanyEducationProfileName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CompanyEducationProfileMetadata.PropertyNames.CompanyEducationProfileName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompanyEducationProfileMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CompanyEducationProfileMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompanyEducationProfileMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CompanyEducationProfileMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public CompanyEducationProfileMetadata Meta()
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
			 public const string CompanyEducationProfileID = "CompanyEducationProfileID";
			 public const string CompanyLaborProfileID = "CompanyLaborProfileID";
			 public const string CompanyEducationProfileCode = "CompanyEducationProfileCode";
			 public const string CompanyEducationProfileName = "CompanyEducationProfileName";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string CompanyEducationProfileID = "CompanyEducationProfileID";
			 public const string CompanyLaborProfileID = "CompanyLaborProfileID";
			 public const string CompanyEducationProfileCode = "CompanyEducationProfileCode";
			 public const string CompanyEducationProfileName = "CompanyEducationProfileName";
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
			lock (typeof(CompanyEducationProfileMetadata))
			{
				if(CompanyEducationProfileMetadata.mapDelegates == null)
				{
					CompanyEducationProfileMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CompanyEducationProfileMetadata.meta == null)
				{
					CompanyEducationProfileMetadata.meta = new CompanyEducationProfileMetadata();
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
				

				meta.AddTypeMap("CompanyEducationProfileID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CompanyLaborProfileID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CompanyEducationProfileCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CompanyEducationProfileName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "CompanyEducationProfile";
				meta.Destination = "CompanyEducationProfile";
				
				meta.spInsert = "proc_CompanyEducationProfileInsert";				
				meta.spUpdate = "proc_CompanyEducationProfileUpdate";		
				meta.spDelete = "proc_CompanyEducationProfileDelete";
				meta.spLoadAll = "proc_CompanyEducationProfileLoadAll";
				meta.spLoadByPrimaryKey = "proc_CompanyEducationProfileLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CompanyEducationProfileMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
