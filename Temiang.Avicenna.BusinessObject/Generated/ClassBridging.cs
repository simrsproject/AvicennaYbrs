/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/4/2018 7:41:53 PM
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
	abstract public class esClassBridgingCollection : esEntityCollectionWAuditLog
	{
		public esClassBridgingCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ClassBridgingCollection";
		}

		#region Query Logic
		protected void InitQuery(esClassBridgingQuery query)
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
			this.InitQuery(query as esClassBridgingQuery);
		}
		#endregion
		
		virtual public ClassBridging DetachEntity(ClassBridging entity)
		{
			return base.DetachEntity(entity) as ClassBridging;
		}
		
		virtual public ClassBridging AttachEntity(ClassBridging entity)
		{
			return base.AttachEntity(entity) as ClassBridging;
		}
		
		virtual public void Combine(ClassBridgingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ClassBridging this[int index]
		{
			get
			{
				return base[index] as ClassBridging;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ClassBridging);
		}
	}



	[Serializable]
	abstract public class esClassBridging : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esClassBridgingQuery GetDynamicQuery()
		{
			return null;
		}

		public esClassBridging()
		{

		}

		public esClassBridging(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String bridgingID, System.String classID, System.String sRBridgingType)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bridgingID, classID, sRBridgingType);
			else
				return LoadByPrimaryKeyStoredProcedure(bridgingID, classID, sRBridgingType);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String bridgingID, System.String classID, System.String sRBridgingType)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bridgingID, classID, sRBridgingType);
			else
				return LoadByPrimaryKeyStoredProcedure(bridgingID, classID, sRBridgingType);
		}

		private bool LoadByPrimaryKeyDynamic(System.String bridgingID, System.String classID, System.String sRBridgingType)
		{
			esClassBridgingQuery query = this.GetDynamicQuery();
			query.Where(query.BridgingID == bridgingID, query.ClassID == classID, query.SRBridgingType == sRBridgingType);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String bridgingID, System.String classID, System.String sRBridgingType)
		{
			esParameters parms = new esParameters();
			parms.Add("BridgingID",bridgingID);			parms.Add("ClassID",classID);			parms.Add("SRBridgingType",sRBridgingType);
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
						case "ClassID": this.str.ClassID = (string)value; break;							
						case "SRBridgingType": this.str.SRBridgingType = (string)value; break;							
						case "BridgingID": this.str.BridgingID = (string)value; break;							
						case "BridgingName": this.str.BridgingName = (string)value; break;							
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
		/// Maps to ClassBridging.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(ClassBridgingMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(ClassBridgingMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to ClassBridging.SRBridgingType
		/// </summary>
		virtual public System.String SRBridgingType
		{
			get
			{
				return base.GetSystemString(ClassBridgingMetadata.ColumnNames.SRBridgingType);
			}
			
			set
			{
				base.SetSystemString(ClassBridgingMetadata.ColumnNames.SRBridgingType, value);
			}
		}
		
		/// <summary>
		/// Maps to ClassBridging.BridgingID
		/// </summary>
		virtual public System.String BridgingID
		{
			get
			{
				return base.GetSystemString(ClassBridgingMetadata.ColumnNames.BridgingID);
			}
			
			set
			{
				base.SetSystemString(ClassBridgingMetadata.ColumnNames.BridgingID, value);
			}
		}
		
		/// <summary>
		/// Maps to ClassBridging.BridgingName
		/// </summary>
		virtual public System.String BridgingName
		{
			get
			{
				return base.GetSystemString(ClassBridgingMetadata.ColumnNames.BridgingName);
			}
			
			set
			{
				base.SetSystemString(ClassBridgingMetadata.ColumnNames.BridgingName, value);
			}
		}
		
		/// <summary>
		/// Maps to ClassBridging.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ClassBridgingMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(ClassBridgingMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to ClassBridging.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ClassBridgingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ClassBridgingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ClassBridging.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ClassBridgingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ClassBridgingMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esClassBridging entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ClassID
			{
				get
				{
					System.String data = entity.ClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassID = null;
					else entity.ClassID = Convert.ToString(value);
				}
			}
				
			public System.String SRBridgingType
			{
				get
				{
					System.String data = entity.SRBridgingType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBridgingType = null;
					else entity.SRBridgingType = Convert.ToString(value);
				}
			}
				
			public System.String BridgingID
			{
				get
				{
					System.String data = entity.BridgingID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BridgingID = null;
					else entity.BridgingID = Convert.ToString(value);
				}
			}
				
			public System.String BridgingName
			{
				get
				{
					System.String data = entity.BridgingName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BridgingName = null;
					else entity.BridgingName = Convert.ToString(value);
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
			

			private esClassBridging entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esClassBridgingQuery query)
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
				throw new Exception("esClassBridging can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ClassBridging : esClassBridging
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
	abstract public class esClassBridgingQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ClassBridgingMetadata.Meta();
			}
		}	
		

		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, ClassBridgingMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRBridgingType
		{
			get
			{
				return new esQueryItem(this, ClassBridgingMetadata.ColumnNames.SRBridgingType, esSystemType.String);
			}
		} 
		
		public esQueryItem BridgingID
		{
			get
			{
				return new esQueryItem(this, ClassBridgingMetadata.ColumnNames.BridgingID, esSystemType.String);
			}
		} 
		
		public esQueryItem BridgingName
		{
			get
			{
				return new esQueryItem(this, ClassBridgingMetadata.ColumnNames.BridgingName, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ClassBridgingMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ClassBridgingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ClassBridgingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ClassBridgingCollection")]
	public partial class ClassBridgingCollection : esClassBridgingCollection, IEnumerable<ClassBridging>
	{
		public ClassBridgingCollection()
		{

		}
		
		public static implicit operator List<ClassBridging>(ClassBridgingCollection coll)
		{
			List<ClassBridging> list = new List<ClassBridging>();
			
			foreach (ClassBridging emp in coll)
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
				return  ClassBridgingMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClassBridgingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ClassBridging(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ClassBridging();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ClassBridgingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClassBridgingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ClassBridgingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ClassBridging AddNew()
		{
			ClassBridging entity = base.AddNewEntity() as ClassBridging;
			
			return entity;
		}

		public ClassBridging FindByPrimaryKey(System.String bridgingID, System.String classID, System.String sRBridgingType)
		{
			return base.FindByPrimaryKey(bridgingID, classID, sRBridgingType) as ClassBridging;
		}


		#region IEnumerable<ClassBridging> Members

		IEnumerator<ClassBridging> IEnumerable<ClassBridging>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ClassBridging;
			}
		}

		#endregion
		
		private ClassBridgingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ClassBridging' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ClassBridging ({ClassID},{SRBridgingType},{BridgingID})")]
	[Serializable]
	public partial class ClassBridging : esClassBridging
	{
		public ClassBridging()
		{

		}
	
		public ClassBridging(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ClassBridgingMetadata.Meta();
			}
		}
		
		
		
		override protected esClassBridgingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClassBridgingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ClassBridgingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClassBridgingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ClassBridgingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ClassBridgingQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ClassBridgingQuery : esClassBridgingQuery
	{
		public ClassBridgingQuery()
		{

		}		
		
		public ClassBridgingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ClassBridgingQuery";
        }
		
			
	}


	[Serializable]
	public partial class ClassBridgingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ClassBridgingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ClassBridgingMetadata.ColumnNames.ClassID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ClassBridgingMetadata.PropertyNames.ClassID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClassBridgingMetadata.ColumnNames.SRBridgingType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ClassBridgingMetadata.PropertyNames.SRBridgingType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClassBridgingMetadata.ColumnNames.BridgingID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ClassBridgingMetadata.PropertyNames.BridgingID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClassBridgingMetadata.ColumnNames.BridgingName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ClassBridgingMetadata.PropertyNames.BridgingName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClassBridgingMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ClassBridgingMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClassBridgingMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ClassBridgingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClassBridgingMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ClassBridgingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ClassBridgingMetadata Meta()
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
			 public const string ClassID = "ClassID";
			 public const string SRBridgingType = "SRBridgingType";
			 public const string BridgingID = "BridgingID";
			 public const string BridgingName = "BridgingName";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ClassID = "ClassID";
			 public const string SRBridgingType = "SRBridgingType";
			 public const string BridgingID = "BridgingID";
			 public const string BridgingName = "BridgingName";
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
			lock (typeof(ClassBridgingMetadata))
			{
				if(ClassBridgingMetadata.mapDelegates == null)
				{
					ClassBridgingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ClassBridgingMetadata.meta == null)
				{
					ClassBridgingMetadata.meta = new ClassBridgingMetadata();
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
				

				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBridgingType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BridgingID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BridgingName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ClassBridging";
				meta.Destination = "ClassBridging";
				
				meta.spInsert = "proc_ClassBridgingInsert";				
				meta.spUpdate = "proc_ClassBridgingUpdate";		
				meta.spDelete = "proc_ClassBridgingDelete";
				meta.spLoadAll = "proc_ClassBridgingLoadAll";
				meta.spLoadByPrimaryKey = "proc_ClassBridgingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ClassBridgingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
