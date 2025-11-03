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
	abstract public class esCompanyFieldOfWorkProfileCollection : esEntityCollectionWAuditLog
	{
		public esCompanyFieldOfWorkProfileCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "CompanyFieldOfWorkProfileCollection";
		}

		#region Query Logic
		protected void InitQuery(esCompanyFieldOfWorkProfileQuery query)
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
			this.InitQuery(query as esCompanyFieldOfWorkProfileQuery);
		}
		#endregion
		
		virtual public CompanyFieldOfWorkProfile DetachEntity(CompanyFieldOfWorkProfile entity)
		{
			return base.DetachEntity(entity) as CompanyFieldOfWorkProfile;
		}
		
		virtual public CompanyFieldOfWorkProfile AttachEntity(CompanyFieldOfWorkProfile entity)
		{
			return base.AttachEntity(entity) as CompanyFieldOfWorkProfile;
		}
		
		virtual public void Combine(CompanyFieldOfWorkProfileCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CompanyFieldOfWorkProfile this[int index]
		{
			get
			{
				return base[index] as CompanyFieldOfWorkProfile;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CompanyFieldOfWorkProfile);
		}
	}



	[Serializable]
	abstract public class esCompanyFieldOfWorkProfile : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCompanyFieldOfWorkProfileQuery GetDynamicQuery()
		{
			return null;
		}

		public esCompanyFieldOfWorkProfile()
		{

		}

		public esCompanyFieldOfWorkProfile(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 companyFieldOfWorkProfileID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(companyFieldOfWorkProfileID);
			else
				return LoadByPrimaryKeyStoredProcedure(companyFieldOfWorkProfileID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 companyFieldOfWorkProfileID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(companyFieldOfWorkProfileID);
			else
				return LoadByPrimaryKeyStoredProcedure(companyFieldOfWorkProfileID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 companyFieldOfWorkProfileID)
		{
			esCompanyFieldOfWorkProfileQuery query = this.GetDynamicQuery();
			query.Where(query.CompanyFieldOfWorkProfileID == companyFieldOfWorkProfileID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 companyFieldOfWorkProfileID)
		{
			esParameters parms = new esParameters();
			parms.Add("CompanyFieldOfWorkProfileID",companyFieldOfWorkProfileID);
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
						case "CompanyFieldOfWorkProfileID": this.str.CompanyFieldOfWorkProfileID = (string)value; break;							
						case "CompanyLaborProfileID": this.str.CompanyLaborProfileID = (string)value; break;							
						case "CompanyFieldOfWorkProfileCode": this.str.CompanyFieldOfWorkProfileCode = (string)value; break;							
						case "CompanyFieldOfWorkProfileName": this.str.CompanyFieldOfWorkProfileName = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CompanyFieldOfWorkProfileID":
						
							if (value == null || value is System.Int32)
								this.CompanyFieldOfWorkProfileID = (System.Int32?)value;
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
		/// Maps to CompanyFieldOfWorkProfile.CompanyFieldOfWorkProfileID
		/// </summary>
		virtual public System.Int32? CompanyFieldOfWorkProfileID
		{
			get
			{
				return base.GetSystemInt32(CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyFieldOfWorkProfileID);
			}
			
			set
			{
				base.SetSystemInt32(CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyFieldOfWorkProfileID, value);
			}
		}
		
		/// <summary>
		/// Maps to CompanyFieldOfWorkProfile.CompanyLaborProfileID
		/// </summary>
		virtual public System.Int32? CompanyLaborProfileID
		{
			get
			{
				return base.GetSystemInt32(CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyLaborProfileID);
			}
			
			set
			{
				base.SetSystemInt32(CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyLaborProfileID, value);
			}
		}
		
		/// <summary>
		/// Maps to CompanyFieldOfWorkProfile.CompanyFieldOfWorkProfileCode
		/// </summary>
		virtual public System.String CompanyFieldOfWorkProfileCode
		{
			get
			{
				return base.GetSystemString(CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyFieldOfWorkProfileCode);
			}
			
			set
			{
				base.SetSystemString(CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyFieldOfWorkProfileCode, value);
			}
		}
		
		/// <summary>
		/// Maps to CompanyFieldOfWorkProfile.CompanyFieldOfWorkProfileName
		/// </summary>
		virtual public System.String CompanyFieldOfWorkProfileName
		{
			get
			{
				return base.GetSystemString(CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyFieldOfWorkProfileName);
			}
			
			set
			{
				base.SetSystemString(CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyFieldOfWorkProfileName, value);
			}
		}
		
		/// <summary>
		/// Maps to CompanyFieldOfWorkProfile.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CompanyFieldOfWorkProfileMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CompanyFieldOfWorkProfileMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to CompanyFieldOfWorkProfile.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CompanyFieldOfWorkProfileMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(CompanyFieldOfWorkProfileMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCompanyFieldOfWorkProfile entity)
			{
				this.entity = entity;
			}
			
	
			public System.String CompanyFieldOfWorkProfileID
			{
				get
				{
					System.Int32? data = entity.CompanyFieldOfWorkProfileID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompanyFieldOfWorkProfileID = null;
					else entity.CompanyFieldOfWorkProfileID = Convert.ToInt32(value);
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
				
			public System.String CompanyFieldOfWorkProfileCode
			{
				get
				{
					System.String data = entity.CompanyFieldOfWorkProfileCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompanyFieldOfWorkProfileCode = null;
					else entity.CompanyFieldOfWorkProfileCode = Convert.ToString(value);
				}
			}
				
			public System.String CompanyFieldOfWorkProfileName
			{
				get
				{
					System.String data = entity.CompanyFieldOfWorkProfileName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompanyFieldOfWorkProfileName = null;
					else entity.CompanyFieldOfWorkProfileName = Convert.ToString(value);
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
			

			private esCompanyFieldOfWorkProfile entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCompanyFieldOfWorkProfileQuery query)
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
				throw new Exception("esCompanyFieldOfWorkProfile can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class CompanyFieldOfWorkProfile : esCompanyFieldOfWorkProfile
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
	abstract public class esCompanyFieldOfWorkProfileQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CompanyFieldOfWorkProfileMetadata.Meta();
			}
		}	
		

		public esQueryItem CompanyFieldOfWorkProfileID
		{
			get
			{
				return new esQueryItem(this, CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyFieldOfWorkProfileID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem CompanyLaborProfileID
		{
			get
			{
				return new esQueryItem(this, CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyLaborProfileID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem CompanyFieldOfWorkProfileCode
		{
			get
			{
				return new esQueryItem(this, CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyFieldOfWorkProfileCode, esSystemType.String);
			}
		} 
		
		public esQueryItem CompanyFieldOfWorkProfileName
		{
			get
			{
				return new esQueryItem(this, CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyFieldOfWorkProfileName, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CompanyFieldOfWorkProfileMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CompanyFieldOfWorkProfileMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CompanyFieldOfWorkProfileCollection")]
	public partial class CompanyFieldOfWorkProfileCollection : esCompanyFieldOfWorkProfileCollection, IEnumerable<CompanyFieldOfWorkProfile>
	{
		public CompanyFieldOfWorkProfileCollection()
		{

		}
		
		public static implicit operator List<CompanyFieldOfWorkProfile>(CompanyFieldOfWorkProfileCollection coll)
		{
			List<CompanyFieldOfWorkProfile> list = new List<CompanyFieldOfWorkProfile>();
			
			foreach (CompanyFieldOfWorkProfile emp in coll)
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
				return  CompanyFieldOfWorkProfileMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CompanyFieldOfWorkProfileQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CompanyFieldOfWorkProfile(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CompanyFieldOfWorkProfile();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public CompanyFieldOfWorkProfileQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CompanyFieldOfWorkProfileQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(CompanyFieldOfWorkProfileQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public CompanyFieldOfWorkProfile AddNew()
		{
			CompanyFieldOfWorkProfile entity = base.AddNewEntity() as CompanyFieldOfWorkProfile;
			
			return entity;
		}

		public CompanyFieldOfWorkProfile FindByPrimaryKey(System.Int32 companyFieldOfWorkProfileID)
		{
			return base.FindByPrimaryKey(companyFieldOfWorkProfileID) as CompanyFieldOfWorkProfile;
		}


		#region IEnumerable<CompanyFieldOfWorkProfile> Members

		IEnumerator<CompanyFieldOfWorkProfile> IEnumerable<CompanyFieldOfWorkProfile>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CompanyFieldOfWorkProfile;
			}
		}

		#endregion
		
		private CompanyFieldOfWorkProfileQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CompanyFieldOfWorkProfile' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("CompanyFieldOfWorkProfile ({CompanyFieldOfWorkProfileID})")]
	[Serializable]
	public partial class CompanyFieldOfWorkProfile : esCompanyFieldOfWorkProfile
	{
		public CompanyFieldOfWorkProfile()
		{

		}
	
		public CompanyFieldOfWorkProfile(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CompanyFieldOfWorkProfileMetadata.Meta();
			}
		}
		
		
		
		override protected esCompanyFieldOfWorkProfileQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CompanyFieldOfWorkProfileQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public CompanyFieldOfWorkProfileQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CompanyFieldOfWorkProfileQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(CompanyFieldOfWorkProfileQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private CompanyFieldOfWorkProfileQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class CompanyFieldOfWorkProfileQuery : esCompanyFieldOfWorkProfileQuery
	{
		public CompanyFieldOfWorkProfileQuery()
		{

		}		
		
		public CompanyFieldOfWorkProfileQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "CompanyFieldOfWorkProfileQuery";
        }
		
			
	}


	[Serializable]
	public partial class CompanyFieldOfWorkProfileMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CompanyFieldOfWorkProfileMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyFieldOfWorkProfileID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CompanyFieldOfWorkProfileMetadata.PropertyNames.CompanyFieldOfWorkProfileID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyLaborProfileID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CompanyFieldOfWorkProfileMetadata.PropertyNames.CompanyLaborProfileID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyFieldOfWorkProfileCode, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CompanyFieldOfWorkProfileMetadata.PropertyNames.CompanyFieldOfWorkProfileCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyFieldOfWorkProfileName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CompanyFieldOfWorkProfileMetadata.PropertyNames.CompanyFieldOfWorkProfileName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompanyFieldOfWorkProfileMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CompanyFieldOfWorkProfileMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompanyFieldOfWorkProfileMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CompanyFieldOfWorkProfileMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public CompanyFieldOfWorkProfileMetadata Meta()
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
			 public const string CompanyFieldOfWorkProfileID = "CompanyFieldOfWorkProfileID";
			 public const string CompanyLaborProfileID = "CompanyLaborProfileID";
			 public const string CompanyFieldOfWorkProfileCode = "CompanyFieldOfWorkProfileCode";
			 public const string CompanyFieldOfWorkProfileName = "CompanyFieldOfWorkProfileName";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string CompanyFieldOfWorkProfileID = "CompanyFieldOfWorkProfileID";
			 public const string CompanyLaborProfileID = "CompanyLaborProfileID";
			 public const string CompanyFieldOfWorkProfileCode = "CompanyFieldOfWorkProfileCode";
			 public const string CompanyFieldOfWorkProfileName = "CompanyFieldOfWorkProfileName";
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
			lock (typeof(CompanyFieldOfWorkProfileMetadata))
			{
				if(CompanyFieldOfWorkProfileMetadata.mapDelegates == null)
				{
					CompanyFieldOfWorkProfileMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CompanyFieldOfWorkProfileMetadata.meta == null)
				{
					CompanyFieldOfWorkProfileMetadata.meta = new CompanyFieldOfWorkProfileMetadata();
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
				

				meta.AddTypeMap("CompanyFieldOfWorkProfileID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CompanyLaborProfileID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CompanyFieldOfWorkProfileCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("CompanyFieldOfWorkProfileName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "CompanyFieldOfWorkProfile";
				meta.Destination = "CompanyFieldOfWorkProfile";
				
				meta.spInsert = "proc_CompanyFieldOfWorkProfileInsert";				
				meta.spUpdate = "proc_CompanyFieldOfWorkProfileUpdate";		
				meta.spDelete = "proc_CompanyFieldOfWorkProfileDelete";
				meta.spLoadAll = "proc_CompanyFieldOfWorkProfileLoadAll";
				meta.spLoadByPrimaryKey = "proc_CompanyFieldOfWorkProfileLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CompanyFieldOfWorkProfileMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
