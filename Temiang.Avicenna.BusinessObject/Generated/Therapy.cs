/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:27 PM
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
	abstract public class esTherapyCollection : esEntityCollectionWAuditLog
	{
		public esTherapyCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TherapyCollection";
		}

		#region Query Logic
		protected void InitQuery(esTherapyQuery query)
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
			this.InitQuery(query as esTherapyQuery);
		}
		#endregion
		
		virtual public Therapy DetachEntity(Therapy entity)
		{
			return base.DetachEntity(entity) as Therapy;
		}
		
		virtual public Therapy AttachEntity(Therapy entity)
		{
			return base.AttachEntity(entity) as Therapy;
		}
		
		virtual public void Combine(TherapyCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Therapy this[int index]
		{
			get
			{
				return base[index] as Therapy;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Therapy);
		}
	}



	[Serializable]
	abstract public class esTherapy : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTherapyQuery GetDynamicQuery()
		{
			return null;
		}

		public esTherapy()
		{

		}

		public esTherapy(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String therapyID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(therapyID);
			else
				return LoadByPrimaryKeyStoredProcedure(therapyID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String therapyID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(therapyID);
			else
				return LoadByPrimaryKeyStoredProcedure(therapyID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String therapyID)
		{
			esTherapyQuery query = this.GetDynamicQuery();
			query.Where(query.TherapyID == therapyID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String therapyID)
		{
			esParameters parms = new esParameters();
			parms.Add("TherapyID",therapyID);
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
						case "TherapyID": this.str.TherapyID = (string)value; break;							
						case "TherapyName": this.str.TherapyName = (string)value; break;							
						case "SRTherapyGroup": this.str.SRTherapyGroup = (string)value; break;							
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
		/// Maps to Therapy.TherapyID
		/// </summary>
		virtual public System.String TherapyID
		{
			get
			{
				return base.GetSystemString(TherapyMetadata.ColumnNames.TherapyID);
			}
			
			set
			{
				base.SetSystemString(TherapyMetadata.ColumnNames.TherapyID, value);
			}
		}
		
		/// <summary>
		/// Maps to Therapy.TherapyName
		/// </summary>
		virtual public System.String TherapyName
		{
			get
			{
				return base.GetSystemString(TherapyMetadata.ColumnNames.TherapyName);
			}
			
			set
			{
				base.SetSystemString(TherapyMetadata.ColumnNames.TherapyName, value);
			}
		}
		
		/// <summary>
		/// Maps to Therapy.SRTherapyGroup
		/// </summary>
		virtual public System.String SRTherapyGroup
		{
			get
			{
				return base.GetSystemString(TherapyMetadata.ColumnNames.SRTherapyGroup);
			}
			
			set
			{
				base.SetSystemString(TherapyMetadata.ColumnNames.SRTherapyGroup, value);
			}
		}
		
		/// <summary>
		/// Maps to Therapy.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TherapyMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TherapyMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Therapy.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TherapyMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(TherapyMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esTherapy entity)
			{
				this.entity = entity;
			}
			
	
			public System.String TherapyID
			{
				get
				{
					System.String data = entity.TherapyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TherapyID = null;
					else entity.TherapyID = Convert.ToString(value);
				}
			}
				
			public System.String TherapyName
			{
				get
				{
					System.String data = entity.TherapyName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TherapyName = null;
					else entity.TherapyName = Convert.ToString(value);
				}
			}
				
			public System.String SRTherapyGroup
			{
				get
				{
					System.String data = entity.SRTherapyGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTherapyGroup = null;
					else entity.SRTherapyGroup = Convert.ToString(value);
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
			

			private esTherapy entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTherapyQuery query)
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
				throw new Exception("esTherapy can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class Therapy : esTherapy
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
	abstract public class esTherapyQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TherapyMetadata.Meta();
			}
		}	
		

		public esQueryItem TherapyID
		{
			get
			{
				return new esQueryItem(this, TherapyMetadata.ColumnNames.TherapyID, esSystemType.String);
			}
		} 
		
		public esQueryItem TherapyName
		{
			get
			{
				return new esQueryItem(this, TherapyMetadata.ColumnNames.TherapyName, esSystemType.String);
			}
		} 
		
		public esQueryItem SRTherapyGroup
		{
			get
			{
				return new esQueryItem(this, TherapyMetadata.ColumnNames.SRTherapyGroup, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TherapyMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TherapyMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TherapyCollection")]
	public partial class TherapyCollection : esTherapyCollection, IEnumerable<Therapy>
	{
		public TherapyCollection()
		{

		}
		
		public static implicit operator List<Therapy>(TherapyCollection coll)
		{
			List<Therapy> list = new List<Therapy>();
			
			foreach (Therapy emp in coll)
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
				return  TherapyMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TherapyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Therapy(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Therapy();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TherapyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TherapyQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TherapyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Therapy AddNew()
		{
			Therapy entity = base.AddNewEntity() as Therapy;
			
			return entity;
		}

		public Therapy FindByPrimaryKey(System.String therapyID)
		{
			return base.FindByPrimaryKey(therapyID) as Therapy;
		}


		#region IEnumerable<Therapy> Members

		IEnumerator<Therapy> IEnumerable<Therapy>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Therapy;
			}
		}

		#endregion
		
		private TherapyQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Therapy' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Therapy ({TherapyID})")]
	[Serializable]
	public partial class Therapy : esTherapy
	{
		public Therapy()
		{

		}
	
		public Therapy(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TherapyMetadata.Meta();
			}
		}
		
		
		
		override protected esTherapyQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TherapyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TherapyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TherapyQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TherapyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TherapyQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TherapyQuery : esTherapyQuery
	{
		public TherapyQuery()
		{

		}		
		
		public TherapyQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TherapyQuery";
        }
		
			
	}


	[Serializable]
	public partial class TherapyMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TherapyMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TherapyMetadata.ColumnNames.TherapyID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TherapyMetadata.PropertyNames.TherapyID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(TherapyMetadata.ColumnNames.TherapyName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TherapyMetadata.PropertyNames.TherapyName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);
				
			c = new esColumnMetadata(TherapyMetadata.ColumnNames.SRTherapyGroup, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TherapyMetadata.PropertyNames.SRTherapyGroup;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TherapyMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TherapyMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TherapyMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TherapyMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TherapyMetadata Meta()
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
			 public const string TherapyID = "TherapyID";
			 public const string TherapyName = "TherapyName";
			 public const string SRTherapyGroup = "SRTherapyGroup";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TherapyID = "TherapyID";
			 public const string TherapyName = "TherapyName";
			 public const string SRTherapyGroup = "SRTherapyGroup";
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
			lock (typeof(TherapyMetadata))
			{
				if(TherapyMetadata.mapDelegates == null)
				{
					TherapyMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TherapyMetadata.meta == null)
				{
					TherapyMetadata.meta = new TherapyMetadata();
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
				

				meta.AddTypeMap("TherapyID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TherapyName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRTherapyGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Therapy";
				meta.Destination = "Therapy";
				
				meta.spInsert = "proc_TherapyInsert";				
				meta.spUpdate = "proc_TherapyUpdate";		
				meta.spDelete = "proc_TherapyDelete";
				meta.spLoadAll = "proc_TherapyLoadAll";
				meta.spLoadByPrimaryKey = "proc_TherapyLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TherapyMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
