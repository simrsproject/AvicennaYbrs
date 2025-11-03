/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:26 PM
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
	abstract public class esSupplierFabricCollection : esEntityCollectionWAuditLog
	{
		public esSupplierFabricCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "SupplierFabricCollection";
		}

		#region Query Logic
		protected void InitQuery(esSupplierFabricQuery query)
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
			this.InitQuery(query as esSupplierFabricQuery);
		}
		#endregion
		
		virtual public SupplierFabric DetachEntity(SupplierFabric entity)
		{
			return base.DetachEntity(entity) as SupplierFabric;
		}
		
		virtual public SupplierFabric AttachEntity(SupplierFabric entity)
		{
			return base.AttachEntity(entity) as SupplierFabric;
		}
		
		virtual public void Combine(SupplierFabricCollection collection)
		{
			base.Combine(collection);
		}
		
		new public SupplierFabric this[int index]
		{
			get
			{
				return base[index] as SupplierFabric;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SupplierFabric);
		}
	}



	[Serializable]
	abstract public class esSupplierFabric : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSupplierFabricQuery GetDynamicQuery()
		{
			return null;
		}

		public esSupplierFabric()
		{

		}

		public esSupplierFabric(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String fabricID, System.String supplierID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(fabricID, supplierID);
			else
				return LoadByPrimaryKeyStoredProcedure(fabricID, supplierID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String fabricID, System.String supplierID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(fabricID, supplierID);
			else
				return LoadByPrimaryKeyStoredProcedure(fabricID, supplierID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String fabricID, System.String supplierID)
		{
			esSupplierFabricQuery query = this.GetDynamicQuery();
			query.Where(query.FabricID == fabricID, query.SupplierID == supplierID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String fabricID, System.String supplierID)
		{
			esParameters parms = new esParameters();
			parms.Add("FabricID",fabricID);			parms.Add("SupplierID",supplierID);
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
						case "FabricID": this.str.FabricID = (string)value; break;							
						case "SupplierID": this.str.SupplierID = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to SupplierFabric.FabricID
		/// </summary>
		virtual public System.String FabricID
		{
			get
			{
				return base.GetSystemString(SupplierFabricMetadata.ColumnNames.FabricID);
			}
			
			set
			{
				if(base.SetSystemString(SupplierFabricMetadata.ColumnNames.FabricID, value))
				{
					this._UpToFabricByFabricID = null;
				}
			}
		}
		
		/// <summary>
		/// Maps to SupplierFabric.SupplierID
		/// </summary>
		virtual public System.String SupplierID
		{
			get
			{
				return base.GetSystemString(SupplierFabricMetadata.ColumnNames.SupplierID);
			}
			
			set
			{
				base.SetSystemString(SupplierFabricMetadata.ColumnNames.SupplierID, value);
			}
		}
		
		/// <summary>
		/// Maps to SupplierFabric.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(SupplierFabricMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(SupplierFabricMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to SupplierFabric.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SupplierFabricMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(SupplierFabricMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to SupplierFabric.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SupplierFabricMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(SupplierFabricMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		[CLSCompliant(false)]
		internal protected Fabric _UpToFabricByFabricID;
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
			public esStrings(esSupplierFabric entity)
			{
				this.entity = entity;
			}
			
	
			public System.String FabricID
			{
				get
				{
					System.String data = entity.FabricID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FabricID = null;
					else entity.FabricID = Convert.ToString(value);
				}
			}
				
			public System.String SupplierID
			{
				get
				{
					System.String data = entity.SupplierID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SupplierID = null;
					else entity.SupplierID = Convert.ToString(value);
				}
			}
				
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			

			private esSupplierFabric entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSupplierFabricQuery query)
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
				throw new Exception("esSupplierFabric can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class SupplierFabric : esSupplierFabric
	{

				
		#region UpToFabricByFabricID - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - RefFabricToSupplierFabric
		/// </summary>

		[XmlIgnore]
		public Fabric UpToFabricByFabricID
		{
			get
			{
				if(this._UpToFabricByFabricID == null
					&& FabricID != null					)
				{
					this._UpToFabricByFabricID = new Fabric();
					this._UpToFabricByFabricID.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToFabricByFabricID", this._UpToFabricByFabricID);
					this._UpToFabricByFabricID.Query.Where(this._UpToFabricByFabricID.Query.FabricID == this.FabricID);
					this._UpToFabricByFabricID.Query.Load();
				}

				return this._UpToFabricByFabricID;
			}
			
			set
			{
				this.RemovePreSave("UpToFabricByFabricID");
				

				if(value == null)
				{
					this.FabricID = null;
					this._UpToFabricByFabricID = null;
				}
				else
				{
					this.FabricID = value.FabricID;
					this._UpToFabricByFabricID = value;
					this.SetPreSave("UpToFabricByFabricID", this._UpToFabricByFabricID);
				}
				
			}
		}
		#endregion
		

		
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
	abstract public class esSupplierFabricQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return SupplierFabricMetadata.Meta();
			}
		}	
		

		public esQueryItem FabricID
		{
			get
			{
				return new esQueryItem(this, SupplierFabricMetadata.ColumnNames.FabricID, esSystemType.String);
			}
		} 
		
		public esQueryItem SupplierID
		{
			get
			{
				return new esQueryItem(this, SupplierFabricMetadata.ColumnNames.SupplierID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, SupplierFabricMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SupplierFabricMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SupplierFabricMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SupplierFabricCollection")]
	public partial class SupplierFabricCollection : esSupplierFabricCollection, IEnumerable<SupplierFabric>
	{
		public SupplierFabricCollection()
		{

		}
		
		public static implicit operator List<SupplierFabric>(SupplierFabricCollection coll)
		{
			List<SupplierFabric> list = new List<SupplierFabric>();
			
			foreach (SupplierFabric emp in coll)
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
				return  SupplierFabricMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SupplierFabricQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SupplierFabric(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SupplierFabric();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public SupplierFabricQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SupplierFabricQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(SupplierFabricQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public SupplierFabric AddNew()
		{
			SupplierFabric entity = base.AddNewEntity() as SupplierFabric;
			
			return entity;
		}

		public SupplierFabric FindByPrimaryKey(System.String fabricID, System.String supplierID)
		{
			return base.FindByPrimaryKey(fabricID, supplierID) as SupplierFabric;
		}


		#region IEnumerable<SupplierFabric> Members

		IEnumerator<SupplierFabric> IEnumerable<SupplierFabric>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as SupplierFabric;
			}
		}

		#endregion
		
		private SupplierFabricQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SupplierFabric' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("SupplierFabric ({FabricID},{SupplierID})")]
	[Serializable]
	public partial class SupplierFabric : esSupplierFabric
	{
		public SupplierFabric()
		{

		}
	
		public SupplierFabric(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SupplierFabricMetadata.Meta();
			}
		}
		
		
		
		override protected esSupplierFabricQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SupplierFabricQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public SupplierFabricQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SupplierFabricQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(SupplierFabricQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private SupplierFabricQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class SupplierFabricQuery : esSupplierFabricQuery
	{
		public SupplierFabricQuery()
		{

		}		
		
		public SupplierFabricQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "SupplierFabricQuery";
        }
		
			
	}


	[Serializable]
	public partial class SupplierFabricMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SupplierFabricMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SupplierFabricMetadata.ColumnNames.FabricID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = SupplierFabricMetadata.PropertyNames.FabricID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(SupplierFabricMetadata.ColumnNames.SupplierID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SupplierFabricMetadata.PropertyNames.SupplierID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(SupplierFabricMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SupplierFabricMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);
				
			c = new esColumnMetadata(SupplierFabricMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SupplierFabricMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(SupplierFabricMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = SupplierFabricMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public SupplierFabricMetadata Meta()
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
			 public const string FabricID = "FabricID";
			 public const string SupplierID = "SupplierID";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string FabricID = "FabricID";
			 public const string SupplierID = "SupplierID";
			 public const string IsActive = "IsActive";
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
			lock (typeof(SupplierFabricMetadata))
			{
				if(SupplierFabricMetadata.mapDelegates == null)
				{
					SupplierFabricMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (SupplierFabricMetadata.meta == null)
				{
					SupplierFabricMetadata.meta = new SupplierFabricMetadata();
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
				

				meta.AddTypeMap("FabricID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SupplierID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "SupplierFabric";
				meta.Destination = "SupplierFabric";
				
				meta.spInsert = "proc_SupplierFabricInsert";				
				meta.spUpdate = "proc_SupplierFabricUpdate";		
				meta.spDelete = "proc_SupplierFabricDelete";
				meta.spLoadAll = "proc_SupplierFabricLoadAll";
				meta.spLoadByPrimaryKey = "proc_SupplierFabricLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SupplierFabricMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
