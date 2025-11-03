/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:07 PM
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
	abstract public class esAbsentCodeCollection : esEntityCollectionWAuditLog
	{
		public esAbsentCodeCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AbsentCodeCollection";
		}

		#region Query Logic
		protected void InitQuery(esAbsentCodeQuery query)
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
			this.InitQuery(query as esAbsentCodeQuery);
		}
		#endregion
		
		virtual public AbsentCode DetachEntity(AbsentCode entity)
		{
			return base.DetachEntity(entity) as AbsentCode;
		}
		
		virtual public AbsentCode AttachEntity(AbsentCode entity)
		{
			return base.AttachEntity(entity) as AbsentCode;
		}
		
		virtual public void Combine(AbsentCodeCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AbsentCode this[int index]
		{
			get
			{
				return base[index] as AbsentCode;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AbsentCode);
		}
	}



	[Serializable]
	abstract public class esAbsentCode : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAbsentCodeQuery GetDynamicQuery()
		{
			return null;
		}

		public esAbsentCode()
		{

		}

		public esAbsentCode(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 absentCodeID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(absentCodeID);
			else
				return LoadByPrimaryKeyStoredProcedure(absentCodeID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 absentCodeID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(absentCodeID);
			else
				return LoadByPrimaryKeyStoredProcedure(absentCodeID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 absentCodeID)
		{
			esAbsentCodeQuery query = this.GetDynamicQuery();
			query.Where(query.AbsentCodeID == absentCodeID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 absentCodeID)
		{
			esParameters parms = new esParameters();
			parms.Add("AbsentCodeID",absentCodeID);
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
						case "AbsentCodeID": this.str.AbsentCodeID = (string)value; break;							
						case "AbsentCode": this.str.AbsentCode = (string)value; break;							
						case "AbsentName": this.str.AbsentName = (string)value; break;							
						case "SRCodeClasification": this.str.SRCodeClasification = (string)value; break;							
						case "SRManagementType": this.str.SRManagementType = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "AbsentCodeID":
						
							if (value == null || value is System.Int32)
								this.AbsentCodeID = (System.Int32?)value;
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
		/// Maps to AbsentCode.AbsentCodeID
		/// </summary>
		virtual public System.Int32? AbsentCodeID
		{
			get
			{
				return base.GetSystemInt32(AbsentCodeMetadata.ColumnNames.AbsentCodeID);
			}
			
			set
			{
				base.SetSystemInt32(AbsentCodeMetadata.ColumnNames.AbsentCodeID, value);
			}
		}
		
		/// <summary>
		/// Maps to AbsentCode.AbsentCode
		/// </summary>
		virtual public System.String AbsentCode
		{
			get
			{
				return base.GetSystemString(AbsentCodeMetadata.ColumnNames.AbsentCode);
			}
			
			set
			{
				base.SetSystemString(AbsentCodeMetadata.ColumnNames.AbsentCode, value);
			}
		}
		
		/// <summary>
		/// Maps to AbsentCode.AbsentName
		/// </summary>
		virtual public System.String AbsentName
		{
			get
			{
				return base.GetSystemString(AbsentCodeMetadata.ColumnNames.AbsentName);
			}
			
			set
			{
				base.SetSystemString(AbsentCodeMetadata.ColumnNames.AbsentName, value);
			}
		}
		
		/// <summary>
		/// Maps to AbsentCode.SRCodeClasification
		/// </summary>
		virtual public System.String SRCodeClasification
		{
			get
			{
				return base.GetSystemString(AbsentCodeMetadata.ColumnNames.SRCodeClasification);
			}
			
			set
			{
				base.SetSystemString(AbsentCodeMetadata.ColumnNames.SRCodeClasification, value);
			}
		}
		
		/// <summary>
		/// Maps to AbsentCode.SRManagementType
		/// </summary>
		virtual public System.String SRManagementType
		{
			get
			{
				return base.GetSystemString(AbsentCodeMetadata.ColumnNames.SRManagementType);
			}
			
			set
			{
				base.SetSystemString(AbsentCodeMetadata.ColumnNames.SRManagementType, value);
			}
		}
		
		/// <summary>
		/// Maps to AbsentCode.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AbsentCodeMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AbsentCodeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AbsentCode.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AbsentCodeMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AbsentCodeMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAbsentCode entity)
			{
				this.entity = entity;
			}
			
	
			public System.String AbsentCodeID
			{
				get
				{
					System.Int32? data = entity.AbsentCodeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AbsentCodeID = null;
					else entity.AbsentCodeID = Convert.ToInt32(value);
				}
			}
				
			public System.String AbsentCode
			{
				get
				{
					System.String data = entity.AbsentCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AbsentCode = null;
					else entity.AbsentCode = Convert.ToString(value);
				}
			}
				
			public System.String AbsentName
			{
				get
				{
					System.String data = entity.AbsentName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AbsentName = null;
					else entity.AbsentName = Convert.ToString(value);
				}
			}
				
			public System.String SRCodeClasification
			{
				get
				{
					System.String data = entity.SRCodeClasification;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCodeClasification = null;
					else entity.SRCodeClasification = Convert.ToString(value);
				}
			}
				
			public System.String SRManagementType
			{
				get
				{
					System.String data = entity.SRManagementType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRManagementType = null;
					else entity.SRManagementType = Convert.ToString(value);
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
			

			private esAbsentCode entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAbsentCodeQuery query)
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
				throw new Exception("esAbsentCode can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AbsentCode : esAbsentCode
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
	abstract public class esAbsentCodeQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AbsentCodeMetadata.Meta();
			}
		}	
		

		public esQueryItem AbsentCodeID
		{
			get
			{
				return new esQueryItem(this, AbsentCodeMetadata.ColumnNames.AbsentCodeID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem AbsentCode
		{
			get
			{
				return new esQueryItem(this, AbsentCodeMetadata.ColumnNames.AbsentCode, esSystemType.String);
			}
		} 
		
		public esQueryItem AbsentName
		{
			get
			{
				return new esQueryItem(this, AbsentCodeMetadata.ColumnNames.AbsentName, esSystemType.String);
			}
		} 
		
		public esQueryItem SRCodeClasification
		{
			get
			{
				return new esQueryItem(this, AbsentCodeMetadata.ColumnNames.SRCodeClasification, esSystemType.String);
			}
		} 
		
		public esQueryItem SRManagementType
		{
			get
			{
				return new esQueryItem(this, AbsentCodeMetadata.ColumnNames.SRManagementType, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AbsentCodeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AbsentCodeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AbsentCodeCollection")]
	public partial class AbsentCodeCollection : esAbsentCodeCollection, IEnumerable<AbsentCode>
	{
		public AbsentCodeCollection()
		{

		}
		
		public static implicit operator List<AbsentCode>(AbsentCodeCollection coll)
		{
			List<AbsentCode> list = new List<AbsentCode>();
			
			foreach (AbsentCode emp in coll)
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
				return  AbsentCodeMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AbsentCodeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AbsentCode(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AbsentCode();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AbsentCodeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AbsentCodeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AbsentCodeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AbsentCode AddNew()
		{
			AbsentCode entity = base.AddNewEntity() as AbsentCode;
			
			return entity;
		}

		public AbsentCode FindByPrimaryKey(System.Int32 absentCodeID)
		{
			return base.FindByPrimaryKey(absentCodeID) as AbsentCode;
		}


		#region IEnumerable<AbsentCode> Members

		IEnumerator<AbsentCode> IEnumerable<AbsentCode>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AbsentCode;
			}
		}

		#endregion
		
		private AbsentCodeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AbsentCode' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AbsentCode ({AbsentCodeID})")]
	[Serializable]
	public partial class AbsentCode : esAbsentCode
	{
		public AbsentCode()
		{

		}
	
		public AbsentCode(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AbsentCodeMetadata.Meta();
			}
		}
		
		
		
		override protected esAbsentCodeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AbsentCodeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AbsentCodeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AbsentCodeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AbsentCodeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AbsentCodeQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AbsentCodeQuery : esAbsentCodeQuery
	{
		public AbsentCodeQuery()
		{

		}		
		
		public AbsentCodeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AbsentCodeQuery";
        }
		
			
	}


	[Serializable]
	public partial class AbsentCodeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AbsentCodeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AbsentCodeMetadata.ColumnNames.AbsentCodeID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AbsentCodeMetadata.PropertyNames.AbsentCodeID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AbsentCodeMetadata.ColumnNames.AbsentCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AbsentCodeMetadata.PropertyNames.AbsentCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AbsentCodeMetadata.ColumnNames.AbsentName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AbsentCodeMetadata.PropertyNames.AbsentName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);
				
			c = new esColumnMetadata(AbsentCodeMetadata.ColumnNames.SRCodeClasification, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AbsentCodeMetadata.PropertyNames.SRCodeClasification;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(AbsentCodeMetadata.ColumnNames.SRManagementType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AbsentCodeMetadata.PropertyNames.SRManagementType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(AbsentCodeMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AbsentCodeMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(AbsentCodeMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = AbsentCodeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AbsentCodeMetadata Meta()
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
			 public const string AbsentCodeID = "AbsentCodeID";
			 public const string AbsentCode = "AbsentCode";
			 public const string AbsentName = "AbsentName";
			 public const string SRCodeClasification = "SRCodeClasification";
			 public const string SRManagementType = "SRManagementType";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string AbsentCodeID = "AbsentCodeID";
			 public const string AbsentCode = "AbsentCode";
			 public const string AbsentName = "AbsentName";
			 public const string SRCodeClasification = "SRCodeClasification";
			 public const string SRManagementType = "SRManagementType";
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
			lock (typeof(AbsentCodeMetadata))
			{
				if(AbsentCodeMetadata.mapDelegates == null)
				{
					AbsentCodeMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AbsentCodeMetadata.meta == null)
				{
					AbsentCodeMetadata.meta = new AbsentCodeMetadata();
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
				

				meta.AddTypeMap("AbsentCodeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AbsentCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AbsentName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCodeClasification", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRManagementType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AbsentCode";
				meta.Destination = "AbsentCode";
				
				meta.spInsert = "proc_AbsentCodeInsert";				
				meta.spUpdate = "proc_AbsentCodeUpdate";		
				meta.spDelete = "proc_AbsentCodeDelete";
				meta.spLoadAll = "proc_AbsentCodeLoadAll";
				meta.spLoadByPrimaryKey = "proc_AbsentCodeLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AbsentCodeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
