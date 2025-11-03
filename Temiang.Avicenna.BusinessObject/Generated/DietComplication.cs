/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/4/2013 11:12:51 AM
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
	abstract public class esDietComplicationCollection : esEntityCollectionWAuditLog
	{
		public esDietComplicationCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "DietComplicationCollection";
		}

		#region Query Logic
		protected void InitQuery(esDietComplicationQuery query)
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
			this.InitQuery(query as esDietComplicationQuery);
		}
		#endregion
		
		virtual public DietComplication DetachEntity(DietComplication entity)
		{
			return base.DetachEntity(entity) as DietComplication;
		}
		
		virtual public DietComplication AttachEntity(DietComplication entity)
		{
			return base.AttachEntity(entity) as DietComplication;
		}
		
		virtual public void Combine(DietComplicationCollection collection)
		{
			base.Combine(collection);
		}
		
		new public DietComplication this[int index]
		{
			get
			{
				return base[index] as DietComplication;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(DietComplication);
		}
	}



	[Serializable]
	abstract public class esDietComplication : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDietComplicationQuery GetDynamicQuery()
		{
			return null;
		}

		public esDietComplication()
		{

		}

		public esDietComplication(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String dietID, System.String dietComplicationID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(dietID, dietComplicationID);
			else
				return LoadByPrimaryKeyStoredProcedure(dietID, dietComplicationID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String dietID, System.String dietComplicationID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(dietID, dietComplicationID);
			else
				return LoadByPrimaryKeyStoredProcedure(dietID, dietComplicationID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String dietID, System.String dietComplicationID)
		{
			esDietComplicationQuery query = this.GetDynamicQuery();
			query.Where(query.DietID == dietID, query.DietComplicationID == dietComplicationID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String dietID, System.String dietComplicationID)
		{
			esParameters parms = new esParameters();
			parms.Add("DietID",dietID);			parms.Add("DietComplicationID",dietComplicationID);
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
						case "DietID": this.str.DietID = (string)value; break;							
						case "DietComplicationID": this.str.DietComplicationID = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;
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
		/// Maps to DietComplication.DietID
		/// </summary>
		virtual public System.String DietID
		{
			get
			{
				return base.GetSystemString(DietComplicationMetadata.ColumnNames.DietID);
			}
			
			set
			{
				base.SetSystemString(DietComplicationMetadata.ColumnNames.DietID, value);
			}
		}
		
		/// <summary>
		/// Maps to DietComplication.DietComplicationID
		/// </summary>
		virtual public System.String DietComplicationID
		{
			get
			{
				return base.GetSystemString(DietComplicationMetadata.ColumnNames.DietComplicationID);
			}
			
			set
			{
				base.SetSystemString(DietComplicationMetadata.ColumnNames.DietComplicationID, value);
			}
		}
		
		/// <summary>
		/// Maps to DietComplication.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(DietComplicationMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(DietComplicationMetadata.ColumnNames.IsActive, value);
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
			public esStrings(esDietComplication entity)
			{
				this.entity = entity;
			}
			
	
			public System.String DietID
			{
				get
				{
					System.String data = entity.DietID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DietID = null;
					else entity.DietID = Convert.ToString(value);
				}
			}
				
			public System.String DietComplicationID
			{
				get
				{
					System.String data = entity.DietComplicationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DietComplicationID = null;
					else entity.DietComplicationID = Convert.ToString(value);
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
			

			private esDietComplication entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDietComplicationQuery query)
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
				throw new Exception("esDietComplication can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class DietComplication : esDietComplication
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
	abstract public class esDietComplicationQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return DietComplicationMetadata.Meta();
			}
		}	
		

		public esQueryItem DietID
		{
			get
			{
				return new esQueryItem(this, DietComplicationMetadata.ColumnNames.DietID, esSystemType.String);
			}
		} 
		
		public esQueryItem DietComplicationID
		{
			get
			{
				return new esQueryItem(this, DietComplicationMetadata.ColumnNames.DietComplicationID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, DietComplicationMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DietComplicationCollection")]
	public partial class DietComplicationCollection : esDietComplicationCollection, IEnumerable<DietComplication>
	{
		public DietComplicationCollection()
		{

		}
		
		public static implicit operator List<DietComplication>(DietComplicationCollection coll)
		{
			List<DietComplication> list = new List<DietComplication>();
			
			foreach (DietComplication emp in coll)
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
				return  DietComplicationMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DietComplicationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new DietComplication(row);
		}

		override protected esEntity CreateEntity()
		{
			return new DietComplication();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public DietComplicationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DietComplicationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(DietComplicationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public DietComplication AddNew()
		{
			DietComplication entity = base.AddNewEntity() as DietComplication;
			
			return entity;
		}

		public DietComplication FindByPrimaryKey(System.String dietID, System.String dietComplicationID)
		{
			return base.FindByPrimaryKey(dietID, dietComplicationID) as DietComplication;
		}


		#region IEnumerable<DietComplication> Members

		IEnumerator<DietComplication> IEnumerable<DietComplication>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as DietComplication;
			}
		}

		#endregion
		
		private DietComplicationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'DietComplication' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("DietComplication ({DietID},{DietComplicationID})")]
	[Serializable]
	public partial class DietComplication : esDietComplication
	{
		public DietComplication()
		{

		}
	
		public DietComplication(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DietComplicationMetadata.Meta();
			}
		}
		
		
		
		override protected esDietComplicationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DietComplicationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public DietComplicationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DietComplicationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(DietComplicationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private DietComplicationQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class DietComplicationQuery : esDietComplicationQuery
	{
		public DietComplicationQuery()
		{

		}		
		
		public DietComplicationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "DietComplicationQuery";
        }
		
			
	}


	[Serializable]
	public partial class DietComplicationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DietComplicationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DietComplicationMetadata.ColumnNames.DietID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = DietComplicationMetadata.PropertyNames.DietID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(DietComplicationMetadata.ColumnNames.DietComplicationID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = DietComplicationMetadata.PropertyNames.DietComplicationID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(DietComplicationMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DietComplicationMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public DietComplicationMetadata Meta()
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
			 public const string DietID = "DietID";
			 public const string DietComplicationID = "DietComplicationID";
			 public const string IsActive = "IsActive";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string DietID = "DietID";
			 public const string DietComplicationID = "DietComplicationID";
			 public const string IsActive = "IsActive";
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
			lock (typeof(DietComplicationMetadata))
			{
				if(DietComplicationMetadata.mapDelegates == null)
				{
					DietComplicationMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (DietComplicationMetadata.meta == null)
				{
					DietComplicationMetadata.meta = new DietComplicationMetadata();
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
				

				meta.AddTypeMap("DietID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DietComplicationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));			
				
				
				
				meta.Source = "DietComplication";
				meta.Destination = "DietComplication";
				
				meta.spInsert = "proc_DietComplicationInsert";				
				meta.spUpdate = "proc_DietComplicationUpdate";		
				meta.spDelete = "proc_DietComplicationDelete";
				meta.spLoadAll = "proc_DietComplicationLoadAll";
				meta.spLoadByPrimaryKey = "proc_DietComplicationLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DietComplicationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
