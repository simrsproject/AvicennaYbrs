/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/20/2014 9:51:21 AM
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
	abstract public class esRegistrationBpjsPackageCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationBpjsPackageCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RegistrationBpjsPackageCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationBpjsPackageQuery query)
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
			this.InitQuery(query as esRegistrationBpjsPackageQuery);
		}
		#endregion
		
		virtual public RegistrationBpjsPackage DetachEntity(RegistrationBpjsPackage entity)
		{
			return base.DetachEntity(entity) as RegistrationBpjsPackage;
		}
		
		virtual public RegistrationBpjsPackage AttachEntity(RegistrationBpjsPackage entity)
		{
			return base.AttachEntity(entity) as RegistrationBpjsPackage;
		}
		
		virtual public void Combine(RegistrationBpjsPackageCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RegistrationBpjsPackage this[int index]
		{
			get
			{
				return base[index] as RegistrationBpjsPackage;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationBpjsPackage);
		}
	}



	[Serializable]
	abstract public class esRegistrationBpjsPackage : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationBpjsPackageQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationBpjsPackage()
		{

		}

		public esRegistrationBpjsPackage(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationNo, System.String packageID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, packageID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, packageID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.String packageID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, packageID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, packageID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.String packageID)
		{
			esRegistrationBpjsPackageQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.PackageID == packageID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.String packageID)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);			parms.Add("PackageID",packageID);
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
						case "PackageID": this.str.PackageID = (string)value; break;							
						case "Price": this.str.Price = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Price":
						
							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
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
		/// Maps to RegistrationBpjsPackage.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationBpjsPackageMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationBpjsPackageMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationBpjsPackage.PackageID
		/// </summary>
		virtual public System.String PackageID
		{
			get
			{
				return base.GetSystemString(RegistrationBpjsPackageMetadata.ColumnNames.PackageID);
			}
			
			set
			{
				base.SetSystemString(RegistrationBpjsPackageMetadata.ColumnNames.PackageID, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationBpjsPackage.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(RegistrationBpjsPackageMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(RegistrationBpjsPackageMetadata.ColumnNames.Price, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationBpjsPackage.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationBpjsPackageMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationBpjsPackageMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationBpjsPackage.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationBpjsPackageMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationBpjsPackageMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRegistrationBpjsPackage entity)
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
				
			public System.String PackageID
			{
				get
				{
					System.String data = entity.PackageID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PackageID = null;
					else entity.PackageID = Convert.ToString(value);
				}
			}
				
			public System.String Price
			{
				get
				{
					System.Decimal? data = entity.Price;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Price = null;
					else entity.Price = Convert.ToDecimal(value);
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
			

			private esRegistrationBpjsPackage entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationBpjsPackageQuery query)
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
				throw new Exception("esRegistrationBpjsPackage can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RegistrationBpjsPackage : esRegistrationBpjsPackage
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
	abstract public class esRegistrationBpjsPackageQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationBpjsPackageMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationBpjsPackageMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem PackageID
		{
			get
			{
				return new esQueryItem(this, RegistrationBpjsPackageMetadata.ColumnNames.PackageID, esSystemType.String);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, RegistrationBpjsPackageMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationBpjsPackageMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationBpjsPackageMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationBpjsPackageCollection")]
	public partial class RegistrationBpjsPackageCollection : esRegistrationBpjsPackageCollection, IEnumerable<RegistrationBpjsPackage>
	{
		public RegistrationBpjsPackageCollection()
		{

		}
		
		public static implicit operator List<RegistrationBpjsPackage>(RegistrationBpjsPackageCollection coll)
		{
			List<RegistrationBpjsPackage> list = new List<RegistrationBpjsPackage>();
			
			foreach (RegistrationBpjsPackage emp in coll)
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
				return  RegistrationBpjsPackageMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationBpjsPackageQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationBpjsPackage(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationBpjsPackage();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RegistrationBpjsPackageQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationBpjsPackageQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RegistrationBpjsPackageQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RegistrationBpjsPackage AddNew()
		{
			RegistrationBpjsPackage entity = base.AddNewEntity() as RegistrationBpjsPackage;
			
			return entity;
		}

		public RegistrationBpjsPackage FindByPrimaryKey(System.String registrationNo, System.String packageID)
		{
			return base.FindByPrimaryKey(registrationNo, packageID) as RegistrationBpjsPackage;
		}


		#region IEnumerable<RegistrationBpjsPackage> Members

		IEnumerator<RegistrationBpjsPackage> IEnumerable<RegistrationBpjsPackage>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationBpjsPackage;
			}
		}

		#endregion
		
		private RegistrationBpjsPackageQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationBpjsPackage' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RegistrationBpjsPackage ({RegistrationNo},{PackageID})")]
	[Serializable]
	public partial class RegistrationBpjsPackage : esRegistrationBpjsPackage
	{
		public RegistrationBpjsPackage()
		{

		}
	
		public RegistrationBpjsPackage(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationBpjsPackageMetadata.Meta();
			}
		}
		
		
		
		override protected esRegistrationBpjsPackageQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationBpjsPackageQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RegistrationBpjsPackageQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationBpjsPackageQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RegistrationBpjsPackageQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RegistrationBpjsPackageQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RegistrationBpjsPackageQuery : esRegistrationBpjsPackageQuery
	{
		public RegistrationBpjsPackageQuery()
		{

		}		
		
		public RegistrationBpjsPackageQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RegistrationBpjsPackageQuery";
        }
		
			
	}


	[Serializable]
	public partial class RegistrationBpjsPackageMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationBpjsPackageMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationBpjsPackageMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationBpjsPackageMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationBpjsPackageMetadata.ColumnNames.PackageID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationBpjsPackageMetadata.PropertyNames.PackageID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationBpjsPackageMetadata.ColumnNames.Price, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RegistrationBpjsPackageMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationBpjsPackageMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationBpjsPackageMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationBpjsPackageMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationBpjsPackageMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RegistrationBpjsPackageMetadata Meta()
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
			 public const string PackageID = "PackageID";
			 public const string Price = "Price";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string PackageID = "PackageID";
			 public const string Price = "Price";
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
			lock (typeof(RegistrationBpjsPackageMetadata))
			{
				if(RegistrationBpjsPackageMetadata.mapDelegates == null)
				{
					RegistrationBpjsPackageMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RegistrationBpjsPackageMetadata.meta == null)
				{
					RegistrationBpjsPackageMetadata.meta = new RegistrationBpjsPackageMetadata();
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
				meta.AddTypeMap("PackageID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RegistrationBpjsPackage";
				meta.Destination = "RegistrationBpjsPackage";
				
				meta.spInsert = "proc_RegistrationBpjsPackageInsert";				
				meta.spUpdate = "proc_RegistrationBpjsPackageUpdate";		
				meta.spDelete = "proc_RegistrationBpjsPackageDelete";
				meta.spLoadAll = "proc_RegistrationBpjsPackageLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationBpjsPackageLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationBpjsPackageMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
