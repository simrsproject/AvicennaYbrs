/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/14/2013 10:51:28 AM
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
	abstract public class esSnackCollection : esEntityCollectionWAuditLog
	{
		public esSnackCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "SnackCollection";
		}

		#region Query Logic
		protected void InitQuery(esSnackQuery query)
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
			this.InitQuery(query as esSnackQuery);
		}
		#endregion
		
		virtual public Snack DetachEntity(Snack entity)
		{
			return base.DetachEntity(entity) as Snack;
		}
		
		virtual public Snack AttachEntity(Snack entity)
		{
			return base.AttachEntity(entity) as Snack;
		}
		
		virtual public void Combine(SnackCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Snack this[int index]
		{
			get
			{
				return base[index] as Snack;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Snack);
		}
	}



	[Serializable]
	abstract public class esSnack : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSnackQuery GetDynamicQuery()
		{
			return null;
		}

		public esSnack()
		{

		}

		public esSnack(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String snackID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(snackID);
			else
				return LoadByPrimaryKeyStoredProcedure(snackID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String snackID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(snackID);
			else
				return LoadByPrimaryKeyStoredProcedure(snackID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String snackID)
		{
			esSnackQuery query = this.GetDynamicQuery();
			query.Where(query.SnackID == snackID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String snackID)
		{
			esParameters parms = new esParameters();
			parms.Add("SnackID",snackID);
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
						case "SnackID": this.str.SnackID = (string)value; break;							
						case "SnackName": this.str.SnackName = (string)value; break;							
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
		/// Maps to Snack.SnackID
		/// </summary>
		virtual public System.String SnackID
		{
			get
			{
				return base.GetSystemString(SnackMetadata.ColumnNames.SnackID);
			}
			
			set
			{
				base.SetSystemString(SnackMetadata.ColumnNames.SnackID, value);
			}
		}
		
		/// <summary>
		/// Maps to Snack.SnackName
		/// </summary>
		virtual public System.String SnackName
		{
			get
			{
				return base.GetSystemString(SnackMetadata.ColumnNames.SnackName);
			}
			
			set
			{
				base.SetSystemString(SnackMetadata.ColumnNames.SnackName, value);
			}
		}
		
		/// <summary>
		/// Maps to Snack.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(SnackMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(SnackMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to Snack.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SnackMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(SnackMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Snack.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SnackMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(SnackMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSnack entity)
			{
				this.entity = entity;
			}
			
	
			public System.String SnackID
			{
				get
				{
					System.String data = entity.SnackID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SnackID = null;
					else entity.SnackID = Convert.ToString(value);
				}
			}
				
			public System.String SnackName
			{
				get
				{
					System.String data = entity.SnackName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SnackName = null;
					else entity.SnackName = Convert.ToString(value);
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
			

			private esSnack entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSnackQuery query)
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
				throw new Exception("esSnack can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class Snack : esSnack
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
	abstract public class esSnackQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return SnackMetadata.Meta();
			}
		}	
		

		public esQueryItem SnackID
		{
			get
			{
				return new esQueryItem(this, SnackMetadata.ColumnNames.SnackID, esSystemType.String);
			}
		} 
		
		public esQueryItem SnackName
		{
			get
			{
				return new esQueryItem(this, SnackMetadata.ColumnNames.SnackName, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, SnackMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SnackMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SnackMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SnackCollection")]
	public partial class SnackCollection : esSnackCollection, IEnumerable<Snack>
	{
		public SnackCollection()
		{

		}
		
		public static implicit operator List<Snack>(SnackCollection coll)
		{
			List<Snack> list = new List<Snack>();
			
			foreach (Snack emp in coll)
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
				return  SnackMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SnackQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Snack(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Snack();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public SnackQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SnackQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(SnackQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Snack AddNew()
		{
			Snack entity = base.AddNewEntity() as Snack;
			
			return entity;
		}

		public Snack FindByPrimaryKey(System.String snackID)
		{
			return base.FindByPrimaryKey(snackID) as Snack;
		}


		#region IEnumerable<Snack> Members

		IEnumerator<Snack> IEnumerable<Snack>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Snack;
			}
		}

		#endregion
		
		private SnackQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Snack' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Snack ({SnackID})")]
	[Serializable]
	public partial class Snack : esSnack
	{
		public Snack()
		{

		}
	
		public Snack(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SnackMetadata.Meta();
			}
		}
		
		
		
		override protected esSnackQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SnackQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public SnackQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SnackQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(SnackQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private SnackQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class SnackQuery : esSnackQuery
	{
		public SnackQuery()
		{

		}		
		
		public SnackQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "SnackQuery";
        }
		
			
	}


	[Serializable]
	public partial class SnackMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SnackMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SnackMetadata.ColumnNames.SnackID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = SnackMetadata.PropertyNames.SnackID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(SnackMetadata.ColumnNames.SnackName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SnackMetadata.PropertyNames.SnackName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);
				
			c = new esColumnMetadata(SnackMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SnackMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(SnackMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SnackMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(SnackMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = SnackMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public SnackMetadata Meta()
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
			 public const string SnackID = "SnackID";
			 public const string SnackName = "SnackName";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SnackID = "SnackID";
			 public const string SnackName = "SnackName";
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
			lock (typeof(SnackMetadata))
			{
				if(SnackMetadata.mapDelegates == null)
				{
					SnackMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (SnackMetadata.meta == null)
				{
					SnackMetadata.meta = new SnackMetadata();
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
				

				meta.AddTypeMap("SnackID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SnackName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Snack";
				meta.Destination = "Snack";
				
				meta.spInsert = "proc_SnackInsert";				
				meta.spUpdate = "proc_SnackUpdate";		
				meta.spDelete = "proc_SnackDelete";
				meta.spLoadAll = "proc_SnackLoadAll";
				meta.spLoadByPrimaryKey = "proc_SnackLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SnackMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
