/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/13/2012 9:44:00 AM
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
	abstract public class esGuarantorSurgicalPackageCoveredCollection : esEntityCollectionWAuditLog
	{
		public esGuarantorSurgicalPackageCoveredCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "GuarantorSurgicalPackageCoveredCollection";
		}

		#region Query Logic
		protected void InitQuery(esGuarantorSurgicalPackageCoveredQuery query)
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
			this.InitQuery(query as esGuarantorSurgicalPackageCoveredQuery);
		}
		#endregion
		
		virtual public GuarantorSurgicalPackageCovered DetachEntity(GuarantorSurgicalPackageCovered entity)
		{
			return base.DetachEntity(entity) as GuarantorSurgicalPackageCovered;
		}
		
		virtual public GuarantorSurgicalPackageCovered AttachEntity(GuarantorSurgicalPackageCovered entity)
		{
			return base.AttachEntity(entity) as GuarantorSurgicalPackageCovered;
		}
		
		virtual public void Combine(GuarantorSurgicalPackageCoveredCollection collection)
		{
			base.Combine(collection);
		}
		
		new public GuarantorSurgicalPackageCovered this[int index]
		{
			get
			{
				return base[index] as GuarantorSurgicalPackageCovered;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(GuarantorSurgicalPackageCovered);
		}
	}



	[Serializable]
	abstract public class esGuarantorSurgicalPackageCovered : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esGuarantorSurgicalPackageCoveredQuery GetDynamicQuery()
		{
			return null;
		}

		public esGuarantorSurgicalPackageCovered()
		{

		}

		public esGuarantorSurgicalPackageCovered(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String guarantorID, System.String packageID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, packageID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, packageID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String guarantorID, System.String packageID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, packageID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, packageID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String guarantorID, System.String packageID)
		{
			esGuarantorSurgicalPackageCoveredQuery query = this.GetDynamicQuery();
			query.Where(query.GuarantorID == guarantorID, query.PackageID == packageID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String guarantorID, System.String packageID)
		{
			esParameters parms = new esParameters();
			parms.Add("GuarantorID",guarantorID);			parms.Add("PackageID",packageID);
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
						case "GuarantorID": this.str.GuarantorID = (string)value; break;							
						case "PackageID": this.str.PackageID = (string)value; break;							
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
		/// Maps to GuarantorSurgicalPackageCovered.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(GuarantorSurgicalPackageCoveredMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(GuarantorSurgicalPackageCoveredMetadata.ColumnNames.GuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorSurgicalPackageCovered.PackageID
		/// </summary>
		virtual public System.String PackageID
		{
			get
			{
				return base.GetSystemString(GuarantorSurgicalPackageCoveredMetadata.ColumnNames.PackageID);
			}
			
			set
			{
				base.SetSystemString(GuarantorSurgicalPackageCoveredMetadata.ColumnNames.PackageID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorSurgicalPackageCovered.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(GuarantorSurgicalPackageCoveredMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(GuarantorSurgicalPackageCoveredMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorSurgicalPackageCovered.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(GuarantorSurgicalPackageCoveredMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(GuarantorSurgicalPackageCoveredMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esGuarantorSurgicalPackageCovered entity)
			{
				this.entity = entity;
			}
			
	
			public System.String GuarantorID
			{
				get
				{
					System.String data = entity.GuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorID = null;
					else entity.GuarantorID = Convert.ToString(value);
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
			

			private esGuarantorSurgicalPackageCovered entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esGuarantorSurgicalPackageCoveredQuery query)
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
				throw new Exception("esGuarantorSurgicalPackageCovered can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class GuarantorSurgicalPackageCovered : esGuarantorSurgicalPackageCovered
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
	abstract public class esGuarantorSurgicalPackageCoveredQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorSurgicalPackageCoveredMetadata.Meta();
			}
		}	
		

		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, GuarantorSurgicalPackageCoveredMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem PackageID
		{
			get
			{
				return new esQueryItem(this, GuarantorSurgicalPackageCoveredMetadata.ColumnNames.PackageID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, GuarantorSurgicalPackageCoveredMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, GuarantorSurgicalPackageCoveredMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("GuarantorSurgicalPackageCoveredCollection")]
	public partial class GuarantorSurgicalPackageCoveredCollection : esGuarantorSurgicalPackageCoveredCollection, IEnumerable<GuarantorSurgicalPackageCovered>
	{
		public GuarantorSurgicalPackageCoveredCollection()
		{

		}
		
		public static implicit operator List<GuarantorSurgicalPackageCovered>(GuarantorSurgicalPackageCoveredCollection coll)
		{
			List<GuarantorSurgicalPackageCovered> list = new List<GuarantorSurgicalPackageCovered>();
			
			foreach (GuarantorSurgicalPackageCovered emp in coll)
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
				return  GuarantorSurgicalPackageCoveredMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorSurgicalPackageCoveredQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new GuarantorSurgicalPackageCovered(row);
		}

		override protected esEntity CreateEntity()
		{
			return new GuarantorSurgicalPackageCovered();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public GuarantorSurgicalPackageCoveredQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorSurgicalPackageCoveredQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(GuarantorSurgicalPackageCoveredQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public GuarantorSurgicalPackageCovered AddNew()
		{
			GuarantorSurgicalPackageCovered entity = base.AddNewEntity() as GuarantorSurgicalPackageCovered;
			
			return entity;
		}

		public GuarantorSurgicalPackageCovered FindByPrimaryKey(System.String guarantorID, System.String packageID)
		{
			return base.FindByPrimaryKey(guarantorID, packageID) as GuarantorSurgicalPackageCovered;
		}


		#region IEnumerable<GuarantorSurgicalPackageCovered> Members

		IEnumerator<GuarantorSurgicalPackageCovered> IEnumerable<GuarantorSurgicalPackageCovered>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as GuarantorSurgicalPackageCovered;
			}
		}

		#endregion
		
		private GuarantorSurgicalPackageCoveredQuery query;
	}


	/// <summary>
	/// Encapsulates the 'GuarantorSurgicalPackageCovered' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("GuarantorSurgicalPackageCovered ({GuarantorID},{PackageID})")]
	[Serializable]
	public partial class GuarantorSurgicalPackageCovered : esGuarantorSurgicalPackageCovered
	{
		public GuarantorSurgicalPackageCovered()
		{

		}
	
		public GuarantorSurgicalPackageCovered(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorSurgicalPackageCoveredMetadata.Meta();
			}
		}
		
		
		
		override protected esGuarantorSurgicalPackageCoveredQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorSurgicalPackageCoveredQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public GuarantorSurgicalPackageCoveredQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorSurgicalPackageCoveredQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(GuarantorSurgicalPackageCoveredQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private GuarantorSurgicalPackageCoveredQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class GuarantorSurgicalPackageCoveredQuery : esGuarantorSurgicalPackageCoveredQuery
	{
		public GuarantorSurgicalPackageCoveredQuery()
		{

		}		
		
		public GuarantorSurgicalPackageCoveredQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "GuarantorSurgicalPackageCoveredQuery";
        }
		
			
	}


	[Serializable]
	public partial class GuarantorSurgicalPackageCoveredMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected GuarantorSurgicalPackageCoveredMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(GuarantorSurgicalPackageCoveredMetadata.ColumnNames.GuarantorID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorSurgicalPackageCoveredMetadata.PropertyNames.GuarantorID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorSurgicalPackageCoveredMetadata.ColumnNames.PackageID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorSurgicalPackageCoveredMetadata.PropertyNames.PackageID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorSurgicalPackageCoveredMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorSurgicalPackageCoveredMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorSurgicalPackageCoveredMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorSurgicalPackageCoveredMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public GuarantorSurgicalPackageCoveredMetadata Meta()
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
			 public const string GuarantorID = "GuarantorID";
			 public const string PackageID = "PackageID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string GuarantorID = "GuarantorID";
			 public const string PackageID = "PackageID";
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
			lock (typeof(GuarantorSurgicalPackageCoveredMetadata))
			{
				if(GuarantorSurgicalPackageCoveredMetadata.mapDelegates == null)
				{
					GuarantorSurgicalPackageCoveredMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (GuarantorSurgicalPackageCoveredMetadata.meta == null)
				{
					GuarantorSurgicalPackageCoveredMetadata.meta = new GuarantorSurgicalPackageCoveredMetadata();
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
				

				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PackageID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "GuarantorSurgicalPackageCovered";
				meta.Destination = "GuarantorSurgicalPackageCovered";
				
				meta.spInsert = "proc_GuarantorSurgicalPackageCoveredInsert";				
				meta.spUpdate = "proc_GuarantorSurgicalPackageCoveredUpdate";		
				meta.spDelete = "proc_GuarantorSurgicalPackageCoveredDelete";
				meta.spLoadAll = "proc_GuarantorSurgicalPackageCoveredLoadAll";
				meta.spLoadByPrimaryKey = "proc_GuarantorSurgicalPackageCoveredLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private GuarantorSurgicalPackageCoveredMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
