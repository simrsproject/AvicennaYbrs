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
	abstract public class esCompanyLaborProfileCollection : esEntityCollectionWAuditLog
	{
		public esCompanyLaborProfileCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "CompanyLaborProfileCollection";
		}

		#region Query Logic
		protected void InitQuery(esCompanyLaborProfileQuery query)
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
			this.InitQuery(query as esCompanyLaborProfileQuery);
		}
		#endregion
		
		virtual public CompanyLaborProfile DetachEntity(CompanyLaborProfile entity)
		{
			return base.DetachEntity(entity) as CompanyLaborProfile;
		}
		
		virtual public CompanyLaborProfile AttachEntity(CompanyLaborProfile entity)
		{
			return base.AttachEntity(entity) as CompanyLaborProfile;
		}
		
		virtual public void Combine(CompanyLaborProfileCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CompanyLaborProfile this[int index]
		{
			get
			{
				return base[index] as CompanyLaborProfile;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CompanyLaborProfile);
		}
	}



	[Serializable]
	abstract public class esCompanyLaborProfile : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCompanyLaborProfileQuery GetDynamicQuery()
		{
			return null;
		}

		public esCompanyLaborProfile()
		{

		}

		public esCompanyLaborProfile(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 companyLaborProfileID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(companyLaborProfileID);
			else
				return LoadByPrimaryKeyStoredProcedure(companyLaborProfileID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 companyLaborProfileID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(companyLaborProfileID);
			else
				return LoadByPrimaryKeyStoredProcedure(companyLaborProfileID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 companyLaborProfileID)
		{
			esCompanyLaborProfileQuery query = this.GetDynamicQuery();
			query.Where(query.CompanyLaborProfileID == companyLaborProfileID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 companyLaborProfileID)
		{
			esParameters parms = new esParameters();
			parms.Add("CompanyLaborProfileID",companyLaborProfileID);
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
						case "CompanyLaborProfileID": this.str.CompanyLaborProfileID = (string)value; break;							
						case "CompanyLaborProfileCode": this.str.CompanyLaborProfileCode = (string)value; break;							
						case "CompanyLaborProfileName": this.str.CompanyLaborProfileName = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
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
		/// Maps to CompanyLaborProfile.CompanyLaborProfileID
		/// </summary>
		virtual public System.Int32? CompanyLaborProfileID
		{
			get
			{
				return base.GetSystemInt32(CompanyLaborProfileMetadata.ColumnNames.CompanyLaborProfileID);
			}
			
			set
			{
				base.SetSystemInt32(CompanyLaborProfileMetadata.ColumnNames.CompanyLaborProfileID, value);
			}
		}
		
		/// <summary>
		/// Maps to CompanyLaborProfile.CompanyLaborProfileCode
		/// </summary>
		virtual public System.String CompanyLaborProfileCode
		{
			get
			{
				return base.GetSystemString(CompanyLaborProfileMetadata.ColumnNames.CompanyLaborProfileCode);
			}
			
			set
			{
				base.SetSystemString(CompanyLaborProfileMetadata.ColumnNames.CompanyLaborProfileCode, value);
			}
		}
		
		/// <summary>
		/// Maps to CompanyLaborProfile.CompanyLaborProfileName
		/// </summary>
		virtual public System.String CompanyLaborProfileName
		{
			get
			{
				return base.GetSystemString(CompanyLaborProfileMetadata.ColumnNames.CompanyLaborProfileName);
			}
			
			set
			{
				base.SetSystemString(CompanyLaborProfileMetadata.ColumnNames.CompanyLaborProfileName, value);
			}
		}
		
		/// <summary>
		/// Maps to CompanyLaborProfile.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CompanyLaborProfileMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CompanyLaborProfileMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to CompanyLaborProfile.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CompanyLaborProfileMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(CompanyLaborProfileMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCompanyLaborProfile entity)
			{
				this.entity = entity;
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
				
			public System.String CompanyLaborProfileCode
			{
				get
				{
					System.String data = entity.CompanyLaborProfileCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompanyLaborProfileCode = null;
					else entity.CompanyLaborProfileCode = Convert.ToString(value);
				}
			}
				
			public System.String CompanyLaborProfileName
			{
				get
				{
					System.String data = entity.CompanyLaborProfileName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompanyLaborProfileName = null;
					else entity.CompanyLaborProfileName = Convert.ToString(value);
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
			

			private esCompanyLaborProfile entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCompanyLaborProfileQuery query)
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
				throw new Exception("esCompanyLaborProfile can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class CompanyLaborProfile : esCompanyLaborProfile
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
	abstract public class esCompanyLaborProfileQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CompanyLaborProfileMetadata.Meta();
			}
		}	
		

		public esQueryItem CompanyLaborProfileID
		{
			get
			{
				return new esQueryItem(this, CompanyLaborProfileMetadata.ColumnNames.CompanyLaborProfileID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem CompanyLaborProfileCode
		{
			get
			{
				return new esQueryItem(this, CompanyLaborProfileMetadata.ColumnNames.CompanyLaborProfileCode, esSystemType.String);
			}
		} 
		
		public esQueryItem CompanyLaborProfileName
		{
			get
			{
				return new esQueryItem(this, CompanyLaborProfileMetadata.ColumnNames.CompanyLaborProfileName, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CompanyLaborProfileMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CompanyLaborProfileMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CompanyLaborProfileCollection")]
	public partial class CompanyLaborProfileCollection : esCompanyLaborProfileCollection, IEnumerable<CompanyLaborProfile>
	{
		public CompanyLaborProfileCollection()
		{

		}
		
		public static implicit operator List<CompanyLaborProfile>(CompanyLaborProfileCollection coll)
		{
			List<CompanyLaborProfile> list = new List<CompanyLaborProfile>();
			
			foreach (CompanyLaborProfile emp in coll)
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
				return  CompanyLaborProfileMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CompanyLaborProfileQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CompanyLaborProfile(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CompanyLaborProfile();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public CompanyLaborProfileQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CompanyLaborProfileQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(CompanyLaborProfileQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public CompanyLaborProfile AddNew()
		{
			CompanyLaborProfile entity = base.AddNewEntity() as CompanyLaborProfile;
			
			return entity;
		}

		public CompanyLaborProfile FindByPrimaryKey(System.Int32 companyLaborProfileID)
		{
			return base.FindByPrimaryKey(companyLaborProfileID) as CompanyLaborProfile;
		}


		#region IEnumerable<CompanyLaborProfile> Members

		IEnumerator<CompanyLaborProfile> IEnumerable<CompanyLaborProfile>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CompanyLaborProfile;
			}
		}

		#endregion
		
		private CompanyLaborProfileQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CompanyLaborProfile' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("CompanyLaborProfile ({CompanyLaborProfileID})")]
	[Serializable]
	public partial class CompanyLaborProfile : esCompanyLaborProfile
	{
		public CompanyLaborProfile()
		{

		}
	
		public CompanyLaborProfile(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CompanyLaborProfileMetadata.Meta();
			}
		}
		
		
		
		override protected esCompanyLaborProfileQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CompanyLaborProfileQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public CompanyLaborProfileQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CompanyLaborProfileQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(CompanyLaborProfileQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private CompanyLaborProfileQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class CompanyLaborProfileQuery : esCompanyLaborProfileQuery
	{
		public CompanyLaborProfileQuery()
		{

		}		
		
		public CompanyLaborProfileQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "CompanyLaborProfileQuery";
        }
		
			
	}


	[Serializable]
	public partial class CompanyLaborProfileMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CompanyLaborProfileMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CompanyLaborProfileMetadata.ColumnNames.CompanyLaborProfileID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CompanyLaborProfileMetadata.PropertyNames.CompanyLaborProfileID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompanyLaborProfileMetadata.ColumnNames.CompanyLaborProfileCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CompanyLaborProfileMetadata.PropertyNames.CompanyLaborProfileCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompanyLaborProfileMetadata.ColumnNames.CompanyLaborProfileName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CompanyLaborProfileMetadata.PropertyNames.CompanyLaborProfileName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompanyLaborProfileMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CompanyLaborProfileMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompanyLaborProfileMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CompanyLaborProfileMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public CompanyLaborProfileMetadata Meta()
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
			 public const string CompanyLaborProfileID = "CompanyLaborProfileID";
			 public const string CompanyLaborProfileCode = "CompanyLaborProfileCode";
			 public const string CompanyLaborProfileName = "CompanyLaborProfileName";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string CompanyLaborProfileID = "CompanyLaborProfileID";
			 public const string CompanyLaborProfileCode = "CompanyLaborProfileCode";
			 public const string CompanyLaborProfileName = "CompanyLaborProfileName";
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
			lock (typeof(CompanyLaborProfileMetadata))
			{
				if(CompanyLaborProfileMetadata.mapDelegates == null)
				{
					CompanyLaborProfileMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CompanyLaborProfileMetadata.meta == null)
				{
					CompanyLaborProfileMetadata.meta = new CompanyLaborProfileMetadata();
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
				

				meta.AddTypeMap("CompanyLaborProfileID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CompanyLaborProfileCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("CompanyLaborProfileName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "CompanyLaborProfile";
				meta.Destination = "CompanyLaborProfile";
				
				meta.spInsert = "proc_CompanyLaborProfileInsert";				
				meta.spUpdate = "proc_CompanyLaborProfileUpdate";		
				meta.spDelete = "proc_CompanyLaborProfileDelete";
				meta.spLoadAll = "proc_CompanyLaborProfileLoadAll";
				meta.spLoadByPrimaryKey = "proc_CompanyLaborProfileLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CompanyLaborProfileMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
