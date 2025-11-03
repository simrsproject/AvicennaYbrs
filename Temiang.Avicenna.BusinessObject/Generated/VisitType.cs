/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:28 PM
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
	abstract public class esVisitTypeCollection : esEntityCollectionWAuditLog
	{
		public esVisitTypeCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VisitTypeCollection";
		}

		#region Query Logic
		protected void InitQuery(esVisitTypeQuery query)
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
			this.InitQuery(query as esVisitTypeQuery);
		}
		#endregion
		
		virtual public VisitType DetachEntity(VisitType entity)
		{
			return base.DetachEntity(entity) as VisitType;
		}
		
		virtual public VisitType AttachEntity(VisitType entity)
		{
			return base.AttachEntity(entity) as VisitType;
		}
		
		virtual public void Combine(VisitTypeCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VisitType this[int index]
		{
			get
			{
				return base[index] as VisitType;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VisitType);
		}
	}



	[Serializable]
	abstract public class esVisitType : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVisitTypeQuery GetDynamicQuery()
		{
			return null;
		}

		public esVisitType()
		{

		}

		public esVisitType(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String visitTypeID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(visitTypeID);
			else
				return LoadByPrimaryKeyStoredProcedure(visitTypeID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String visitTypeID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(visitTypeID);
			else
				return LoadByPrimaryKeyStoredProcedure(visitTypeID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String visitTypeID)
		{
			esVisitTypeQuery query = this.GetDynamicQuery();
			query.Where(query.VisitTypeID == visitTypeID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String visitTypeID)
		{
			esParameters parms = new esParameters();
			parms.Add("VisitTypeID",visitTypeID);
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
						case "VisitTypeID": this.str.VisitTypeID = (string)value; break;							
						case "VisitTypeName": this.str.VisitTypeName = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
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
		/// Maps to VisitType.VisitTypeID
		/// </summary>
		virtual public System.String VisitTypeID
		{
			get
			{
				return base.GetSystemString(VisitTypeMetadata.ColumnNames.VisitTypeID);
			}
			
			set
			{
				base.SetSystemString(VisitTypeMetadata.ColumnNames.VisitTypeID, value);
			}
		}
		
		/// <summary>
		/// Maps to VisitType.VisitTypeName
		/// </summary>
		virtual public System.String VisitTypeName
		{
			get
			{
				return base.GetSystemString(VisitTypeMetadata.ColumnNames.VisitTypeName);
			}
			
			set
			{
				base.SetSystemString(VisitTypeMetadata.ColumnNames.VisitTypeName, value);
			}
		}
		
		/// <summary>
		/// Maps to VisitType.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(VisitTypeMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(VisitTypeMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to VisitType.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(VisitTypeMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(VisitTypeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to VisitType.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(VisitTypeMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(VisitTypeMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esVisitType entity)
			{
				this.entity = entity;
			}
			
	
			public System.String VisitTypeID
			{
				get
				{
					System.String data = entity.VisitTypeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VisitTypeID = null;
					else entity.VisitTypeID = Convert.ToString(value);
				}
			}
				
			public System.String VisitTypeName
			{
				get
				{
					System.String data = entity.VisitTypeName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VisitTypeName = null;
					else entity.VisitTypeName = Convert.ToString(value);
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
			

			private esVisitType entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVisitTypeQuery query)
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
				throw new Exception("esVisitType can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class VisitType : esVisitType
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
	abstract public class esVisitTypeQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VisitTypeMetadata.Meta();
			}
		}	
		

		public esQueryItem VisitTypeID
		{
			get
			{
				return new esQueryItem(this, VisitTypeMetadata.ColumnNames.VisitTypeID, esSystemType.String);
			}
		} 
		
		public esQueryItem VisitTypeName
		{
			get
			{
				return new esQueryItem(this, VisitTypeMetadata.ColumnNames.VisitTypeName, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, VisitTypeMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, VisitTypeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, VisitTypeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VisitTypeCollection")]
	public partial class VisitTypeCollection : esVisitTypeCollection, IEnumerable<VisitType>
	{
		public VisitTypeCollection()
		{

		}
		
		public static implicit operator List<VisitType>(VisitTypeCollection coll)
		{
			List<VisitType> list = new List<VisitType>();
			
			foreach (VisitType emp in coll)
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
				return  VisitTypeMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VisitTypeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VisitType(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VisitType();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public VisitTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VisitTypeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VisitTypeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VisitType AddNew()
		{
			VisitType entity = base.AddNewEntity() as VisitType;
			
			return entity;
		}

		public VisitType FindByPrimaryKey(System.String visitTypeID)
		{
			return base.FindByPrimaryKey(visitTypeID) as VisitType;
		}


		#region IEnumerable<VisitType> Members

		IEnumerator<VisitType> IEnumerable<VisitType>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VisitType;
			}
		}

		#endregion
		
		private VisitTypeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'VisitType' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VisitType ({VisitTypeID})")]
	[Serializable]
	public partial class VisitType : esVisitType
	{
		public VisitType()
		{

		}
	
		public VisitType(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VisitTypeMetadata.Meta();
			}
		}
		
		
		
		override protected esVisitTypeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VisitTypeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VisitTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VisitTypeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VisitTypeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VisitTypeQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VisitTypeQuery : esVisitTypeQuery
	{
		public VisitTypeQuery()
		{

		}		
		
		public VisitTypeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VisitTypeQuery";
        }
		
			
	}


	[Serializable]
	public partial class VisitTypeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VisitTypeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VisitTypeMetadata.ColumnNames.VisitTypeID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VisitTypeMetadata.PropertyNames.VisitTypeID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(VisitTypeMetadata.ColumnNames.VisitTypeName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = VisitTypeMetadata.PropertyNames.VisitTypeName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(VisitTypeMetadata.ColumnNames.Notes, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = VisitTypeMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 1000;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(VisitTypeMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VisitTypeMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VisitTypeMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = VisitTypeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VisitTypeMetadata Meta()
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
			 public const string VisitTypeID = "VisitTypeID";
			 public const string VisitTypeName = "VisitTypeName";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string VisitTypeID = "VisitTypeID";
			 public const string VisitTypeName = "VisitTypeName";
			 public const string Notes = "Notes";
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
			lock (typeof(VisitTypeMetadata))
			{
				if(VisitTypeMetadata.mapDelegates == null)
				{
					VisitTypeMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VisitTypeMetadata.meta == null)
				{
					VisitTypeMetadata.meta = new VisitTypeMetadata();
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
				

				meta.AddTypeMap("VisitTypeID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VisitTypeName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "VisitType";
				meta.Destination = "VisitType";
				
				meta.spInsert = "proc_VisitTypeInsert";				
				meta.spUpdate = "proc_VisitTypeUpdate";		
				meta.spDelete = "proc_VisitTypeDelete";
				meta.spLoadAll = "proc_VisitTypeLoadAll";
				meta.spLoadByPrimaryKey = "proc_VisitTypeLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VisitTypeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
