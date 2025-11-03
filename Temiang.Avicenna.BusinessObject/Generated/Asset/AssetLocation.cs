/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:11 PM
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
	abstract public class esAssetLocationCollection : esEntityCollectionWAuditLog
	{
		public esAssetLocationCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AssetLocationCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetLocationQuery query)
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
			this.InitQuery(query as esAssetLocationQuery);
		}
		#endregion
		
		virtual public AssetLocation DetachEntity(AssetLocation entity)
		{
			return base.DetachEntity(entity) as AssetLocation;
		}
		
		virtual public AssetLocation AttachEntity(AssetLocation entity)
		{
			return base.AttachEntity(entity) as AssetLocation;
		}
		
		virtual public void Combine(AssetLocationCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AssetLocation this[int index]
		{
			get
			{
				return base[index] as AssetLocation;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetLocation);
		}
	}



	[Serializable]
	abstract public class esAssetLocation : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetLocationQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetLocation()
		{

		}

		public esAssetLocation(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String assetLocationID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetLocationID);
			else
				return LoadByPrimaryKeyStoredProcedure(assetLocationID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String assetLocationID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetLocationID);
			else
				return LoadByPrimaryKeyStoredProcedure(assetLocationID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String assetLocationID)
		{
			esAssetLocationQuery query = this.GetDynamicQuery();
			query.Where(query.AssetLocationID == assetLocationID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String assetLocationID)
		{
			esParameters parms = new esParameters();
			parms.Add("AssetLocationID",assetLocationID);
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
						case "AssetLocationID": this.str.AssetLocationID = (string)value; break;							
						case "AssetLocationName": this.str.AssetLocationName = (string)value; break;							
						case "DepartmentID": this.str.DepartmentID = (string)value; break;							
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "Approver": this.str.Approver = (string)value; break;							
						case "PersonInCharge": this.str.PersonInCharge = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
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
		/// Maps to AssetLocation.AssetLocationID
		/// </summary>
		virtual public System.String AssetLocationID
		{
			get
			{
				return base.GetSystemString(AssetLocationMetadata.ColumnNames.AssetLocationID);
			}
			
			set
			{
				base.SetSystemString(AssetLocationMetadata.ColumnNames.AssetLocationID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetLocation.AssetLocationName
		/// </summary>
		virtual public System.String AssetLocationName
		{
			get
			{
				return base.GetSystemString(AssetLocationMetadata.ColumnNames.AssetLocationName);
			}
			
			set
			{
				base.SetSystemString(AssetLocationMetadata.ColumnNames.AssetLocationName, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetLocation.DepartmentID
		/// </summary>
		virtual public System.String DepartmentID
		{
			get
			{
				return base.GetSystemString(AssetLocationMetadata.ColumnNames.DepartmentID);
			}
			
			set
			{
				base.SetSystemString(AssetLocationMetadata.ColumnNames.DepartmentID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetLocation.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(AssetLocationMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(AssetLocationMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetLocation.Approver
		/// </summary>
		virtual public System.String Approver
		{
			get
			{
				return base.GetSystemString(AssetLocationMetadata.ColumnNames.Approver);
			}
			
			set
			{
				base.SetSystemString(AssetLocationMetadata.ColumnNames.Approver, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetLocation.PersonInCharge
		/// </summary>
		virtual public System.String PersonInCharge
		{
			get
			{
				return base.GetSystemString(AssetLocationMetadata.ColumnNames.PersonInCharge);
			}
			
			set
			{
				base.SetSystemString(AssetLocationMetadata.ColumnNames.PersonInCharge, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetLocation.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(AssetLocationMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(AssetLocationMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetLocation.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(AssetLocationMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(AssetLocationMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetLocation.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetLocationMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AssetLocationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetLocation.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AssetLocationMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AssetLocationMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAssetLocation entity)
			{
				this.entity = entity;
			}
			
	
			public System.String AssetLocationID
			{
				get
				{
					System.String data = entity.AssetLocationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetLocationID = null;
					else entity.AssetLocationID = Convert.ToString(value);
				}
			}
				
			public System.String AssetLocationName
			{
				get
				{
					System.String data = entity.AssetLocationName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetLocationName = null;
					else entity.AssetLocationName = Convert.ToString(value);
				}
			}
				
			public System.String DepartmentID
			{
				get
				{
					System.String data = entity.DepartmentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepartmentID = null;
					else entity.DepartmentID = Convert.ToString(value);
				}
			}
				
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
				
			public System.String Approver
			{
				get
				{
					System.String data = entity.Approver;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Approver = null;
					else entity.Approver = Convert.ToString(value);
				}
			}
				
			public System.String PersonInCharge
			{
				get
				{
					System.String data = entity.PersonInCharge;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonInCharge = null;
					else entity.PersonInCharge = Convert.ToString(value);
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
			

			private esAssetLocation entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetLocationQuery query)
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
				throw new Exception("esAssetLocation can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AssetLocation : esAssetLocation
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
	abstract public class esAssetLocationQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AssetLocationMetadata.Meta();
			}
		}	
		

		public esQueryItem AssetLocationID
		{
			get
			{
				return new esQueryItem(this, AssetLocationMetadata.ColumnNames.AssetLocationID, esSystemType.String);
			}
		} 
		
		public esQueryItem AssetLocationName
		{
			get
			{
				return new esQueryItem(this, AssetLocationMetadata.ColumnNames.AssetLocationName, esSystemType.String);
			}
		} 
		
		public esQueryItem DepartmentID
		{
			get
			{
				return new esQueryItem(this, AssetLocationMetadata.ColumnNames.DepartmentID, esSystemType.String);
			}
		} 
		
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, AssetLocationMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem Approver
		{
			get
			{
				return new esQueryItem(this, AssetLocationMetadata.ColumnNames.Approver, esSystemType.String);
			}
		} 
		
		public esQueryItem PersonInCharge
		{
			get
			{
				return new esQueryItem(this, AssetLocationMetadata.ColumnNames.PersonInCharge, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, AssetLocationMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, AssetLocationMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetLocationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AssetLocationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetLocationCollection")]
	public partial class AssetLocationCollection : esAssetLocationCollection, IEnumerable<AssetLocation>
	{
		public AssetLocationCollection()
		{

		}
		
		public static implicit operator List<AssetLocation>(AssetLocationCollection coll)
		{
			List<AssetLocation> list = new List<AssetLocation>();
			
			foreach (AssetLocation emp in coll)
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
				return  AssetLocationMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetLocationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetLocation(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetLocation();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AssetLocationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetLocationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AssetLocationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AssetLocation AddNew()
		{
			AssetLocation entity = base.AddNewEntity() as AssetLocation;
			
			return entity;
		}

		public AssetLocation FindByPrimaryKey(System.String assetLocationID)
		{
			return base.FindByPrimaryKey(assetLocationID) as AssetLocation;
		}


		#region IEnumerable<AssetLocation> Members

		IEnumerator<AssetLocation> IEnumerable<AssetLocation>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AssetLocation;
			}
		}

		#endregion
		
		private AssetLocationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetLocation' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AssetLocation ({AssetLocationID})")]
	[Serializable]
	public partial class AssetLocation : esAssetLocation
	{
		public AssetLocation()
		{

		}
	
		public AssetLocation(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetLocationMetadata.Meta();
			}
		}
		
		
		
		override protected esAssetLocationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetLocationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AssetLocationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetLocationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AssetLocationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AssetLocationQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AssetLocationQuery : esAssetLocationQuery
	{
		public AssetLocationQuery()
		{

		}		
		
		public AssetLocationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AssetLocationQuery";
        }
		
			
	}


	[Serializable]
	public partial class AssetLocationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetLocationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetLocationMetadata.ColumnNames.AssetLocationID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetLocationMetadata.PropertyNames.AssetLocationID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetLocationMetadata.ColumnNames.AssetLocationName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetLocationMetadata.PropertyNames.AssetLocationName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetLocationMetadata.ColumnNames.DepartmentID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetLocationMetadata.PropertyNames.DepartmentID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetLocationMetadata.ColumnNames.ServiceUnitID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetLocationMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetLocationMetadata.ColumnNames.Approver, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetLocationMetadata.PropertyNames.Approver;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetLocationMetadata.ColumnNames.PersonInCharge, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetLocationMetadata.PropertyNames.PersonInCharge;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetLocationMetadata.ColumnNames.Notes, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetLocationMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetLocationMetadata.ColumnNames.IsActive, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetLocationMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetLocationMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetLocationMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetLocationMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetLocationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AssetLocationMetadata Meta()
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
			 public const string AssetLocationID = "AssetLocationID";
			 public const string AssetLocationName = "AssetLocationName";
			 public const string DepartmentID = "DepartmentID";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string Approver = "Approver";
			 public const string PersonInCharge = "PersonInCharge";
			 public const string Notes = "Notes";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string AssetLocationID = "AssetLocationID";
			 public const string AssetLocationName = "AssetLocationName";
			 public const string DepartmentID = "DepartmentID";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string Approver = "Approver";
			 public const string PersonInCharge = "PersonInCharge";
			 public const string Notes = "Notes";
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
			lock (typeof(AssetLocationMetadata))
			{
				if(AssetLocationMetadata.mapDelegates == null)
				{
					AssetLocationMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AssetLocationMetadata.meta == null)
				{
					AssetLocationMetadata.meta = new AssetLocationMetadata();
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
				

				meta.AddTypeMap("AssetLocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssetLocationName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DepartmentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Approver", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PersonInCharge", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AssetLocation";
				meta.Destination = "AssetLocation";
				
				meta.spInsert = "proc_AssetLocationInsert";				
				meta.spUpdate = "proc_AssetLocationUpdate";		
				meta.spDelete = "proc_AssetLocationDelete";
				meta.spLoadAll = "proc_AssetLocationLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetLocationLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetLocationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
