/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:12 PM
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
	abstract public class esBiayaJabatanCollection : esEntityCollectionWAuditLog
	{
		public esBiayaJabatanCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "BiayaJabatanCollection";
		}

		#region Query Logic
		protected void InitQuery(esBiayaJabatanQuery query)
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
			this.InitQuery(query as esBiayaJabatanQuery);
		}
		#endregion
		
		virtual public BiayaJabatan DetachEntity(BiayaJabatan entity)
		{
			return base.DetachEntity(entity) as BiayaJabatan;
		}
		
		virtual public BiayaJabatan AttachEntity(BiayaJabatan entity)
		{
			return base.AttachEntity(entity) as BiayaJabatan;
		}
		
		virtual public void Combine(BiayaJabatanCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BiayaJabatan this[int index]
		{
			get
			{
				return base[index] as BiayaJabatan;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BiayaJabatan);
		}
	}



	[Serializable]
	abstract public class esBiayaJabatan : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBiayaJabatanQuery GetDynamicQuery()
		{
			return null;
		}

		public esBiayaJabatan()
		{

		}

		public esBiayaJabatan(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 biayaJabatanID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(biayaJabatanID);
			else
				return LoadByPrimaryKeyStoredProcedure(biayaJabatanID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 biayaJabatanID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(biayaJabatanID);
			else
				return LoadByPrimaryKeyStoredProcedure(biayaJabatanID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 biayaJabatanID)
		{
			esBiayaJabatanQuery query = this.GetDynamicQuery();
			query.Where(query.BiayaJabatanID == biayaJabatanID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 biayaJabatanID)
		{
			esParameters parms = new esParameters();
			parms.Add("BiayaJabatanID",biayaJabatanID);
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
						case "BiayaJabatanID": this.str.BiayaJabatanID = (string)value; break;							
						case "ValidFrom": this.str.ValidFrom = (string)value; break;							
						case "AmountOfDeduction": this.str.AmountOfDeduction = (string)value; break;							
						case "PercentOfDeduction": this.str.PercentOfDeduction = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "BiayaJabatanID":
						
							if (value == null || value is System.Int32)
								this.BiayaJabatanID = (System.Int32?)value;
							break;
						
						case "ValidFrom":
						
							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						
						case "AmountOfDeduction":
						
							if (value == null || value is System.Decimal)
								this.AmountOfDeduction = (System.Decimal?)value;
							break;
						
						case "PercentOfDeduction":
						
							if (value == null || value is System.Decimal)
								this.PercentOfDeduction = (System.Decimal?)value;
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
		/// Maps to BiayaJabatan.BiayaJabatanID
		/// </summary>
		virtual public System.Int32? BiayaJabatanID
		{
			get
			{
				return base.GetSystemInt32(BiayaJabatanMetadata.ColumnNames.BiayaJabatanID);
			}
			
			set
			{
				base.SetSystemInt32(BiayaJabatanMetadata.ColumnNames.BiayaJabatanID, value);
			}
		}
		
		/// <summary>
		/// Maps to BiayaJabatan.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(BiayaJabatanMetadata.ColumnNames.ValidFrom);
			}
			
			set
			{
				base.SetSystemDateTime(BiayaJabatanMetadata.ColumnNames.ValidFrom, value);
			}
		}
		
		/// <summary>
		/// Maps to BiayaJabatan.AmountOfDeduction
		/// </summary>
		virtual public System.Decimal? AmountOfDeduction
		{
			get
			{
				return base.GetSystemDecimal(BiayaJabatanMetadata.ColumnNames.AmountOfDeduction);
			}
			
			set
			{
				base.SetSystemDecimal(BiayaJabatanMetadata.ColumnNames.AmountOfDeduction, value);
			}
		}
		
		/// <summary>
		/// Maps to BiayaJabatan.PercentOfDeduction
		/// </summary>
		virtual public System.Decimal? PercentOfDeduction
		{
			get
			{
				return base.GetSystemDecimal(BiayaJabatanMetadata.ColumnNames.PercentOfDeduction);
			}
			
			set
			{
				base.SetSystemDecimal(BiayaJabatanMetadata.ColumnNames.PercentOfDeduction, value);
			}
		}
		
		/// <summary>
		/// Maps to BiayaJabatan.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BiayaJabatanMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BiayaJabatanMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to BiayaJabatan.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BiayaJabatanMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BiayaJabatanMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esBiayaJabatan entity)
			{
				this.entity = entity;
			}
			
	
			public System.String BiayaJabatanID
			{
				get
				{
					System.Int32? data = entity.BiayaJabatanID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BiayaJabatanID = null;
					else entity.BiayaJabatanID = Convert.ToInt32(value);
				}
			}
				
			public System.String ValidFrom
			{
				get
				{
					System.DateTime? data = entity.ValidFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidFrom = null;
					else entity.ValidFrom = Convert.ToDateTime(value);
				}
			}
				
			public System.String AmountOfDeduction
			{
				get
				{
					System.Decimal? data = entity.AmountOfDeduction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountOfDeduction = null;
					else entity.AmountOfDeduction = Convert.ToDecimal(value);
				}
			}
				
			public System.String PercentOfDeduction
			{
				get
				{
					System.Decimal? data = entity.PercentOfDeduction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PercentOfDeduction = null;
					else entity.PercentOfDeduction = Convert.ToDecimal(value);
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
			

			private esBiayaJabatan entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBiayaJabatanQuery query)
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
				throw new Exception("esBiayaJabatan can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class BiayaJabatan : esBiayaJabatan
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
	abstract public class esBiayaJabatanQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return BiayaJabatanMetadata.Meta();
			}
		}	
		

		public esQueryItem BiayaJabatanID
		{
			get
			{
				return new esQueryItem(this, BiayaJabatanMetadata.ColumnNames.BiayaJabatanID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, BiayaJabatanMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem AmountOfDeduction
		{
			get
			{
				return new esQueryItem(this, BiayaJabatanMetadata.ColumnNames.AmountOfDeduction, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem PercentOfDeduction
		{
			get
			{
				return new esQueryItem(this, BiayaJabatanMetadata.ColumnNames.PercentOfDeduction, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BiayaJabatanMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BiayaJabatanMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BiayaJabatanCollection")]
	public partial class BiayaJabatanCollection : esBiayaJabatanCollection, IEnumerable<BiayaJabatan>
	{
		public BiayaJabatanCollection()
		{

		}
		
		public static implicit operator List<BiayaJabatan>(BiayaJabatanCollection coll)
		{
			List<BiayaJabatan> list = new List<BiayaJabatan>();
			
			foreach (BiayaJabatan emp in coll)
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
				return  BiayaJabatanMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BiayaJabatanQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BiayaJabatan(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BiayaJabatan();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public BiayaJabatanQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BiayaJabatanQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(BiayaJabatanQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public BiayaJabatan AddNew()
		{
			BiayaJabatan entity = base.AddNewEntity() as BiayaJabatan;
			
			return entity;
		}

		public BiayaJabatan FindByPrimaryKey(System.Int32 biayaJabatanID)
		{
			return base.FindByPrimaryKey(biayaJabatanID) as BiayaJabatan;
		}


		#region IEnumerable<BiayaJabatan> Members

		IEnumerator<BiayaJabatan> IEnumerable<BiayaJabatan>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BiayaJabatan;
			}
		}

		#endregion
		
		private BiayaJabatanQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BiayaJabatan' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("BiayaJabatan ({BiayaJabatanID})")]
	[Serializable]
	public partial class BiayaJabatan : esBiayaJabatan
	{
		public BiayaJabatan()
		{

		}
	
		public BiayaJabatan(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BiayaJabatanMetadata.Meta();
			}
		}
		
		
		
		override protected esBiayaJabatanQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BiayaJabatanQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public BiayaJabatanQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BiayaJabatanQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(BiayaJabatanQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private BiayaJabatanQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class BiayaJabatanQuery : esBiayaJabatanQuery
	{
		public BiayaJabatanQuery()
		{

		}		
		
		public BiayaJabatanQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "BiayaJabatanQuery";
        }
		
			
	}


	[Serializable]
	public partial class BiayaJabatanMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BiayaJabatanMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BiayaJabatanMetadata.ColumnNames.BiayaJabatanID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BiayaJabatanMetadata.PropertyNames.BiayaJabatanID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(BiayaJabatanMetadata.ColumnNames.ValidFrom, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BiayaJabatanMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);
				
			c = new esColumnMetadata(BiayaJabatanMetadata.ColumnNames.AmountOfDeduction, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BiayaJabatanMetadata.PropertyNames.AmountOfDeduction;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(BiayaJabatanMetadata.ColumnNames.PercentOfDeduction, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BiayaJabatanMetadata.PropertyNames.PercentOfDeduction;
			c.NumericPrecision = 4;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(BiayaJabatanMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BiayaJabatanMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(BiayaJabatanMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = BiayaJabatanMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public BiayaJabatanMetadata Meta()
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
			 public const string BiayaJabatanID = "BiayaJabatanID";
			 public const string ValidFrom = "ValidFrom";
			 public const string AmountOfDeduction = "AmountOfDeduction";
			 public const string PercentOfDeduction = "PercentOfDeduction";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string BiayaJabatanID = "BiayaJabatanID";
			 public const string ValidFrom = "ValidFrom";
			 public const string AmountOfDeduction = "AmountOfDeduction";
			 public const string PercentOfDeduction = "PercentOfDeduction";
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
			lock (typeof(BiayaJabatanMetadata))
			{
				if(BiayaJabatanMetadata.mapDelegates == null)
				{
					BiayaJabatanMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BiayaJabatanMetadata.meta == null)
				{
					BiayaJabatanMetadata.meta = new BiayaJabatanMetadata();
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
				

				meta.AddTypeMap("BiayaJabatanID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("AmountOfDeduction", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("PercentOfDeduction", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "BiayaJabatan";
				meta.Destination = "BiayaJabatan";
				
				meta.spInsert = "proc_BiayaJabatanInsert";				
				meta.spUpdate = "proc_BiayaJabatanUpdate";		
				meta.spDelete = "proc_BiayaJabatanDelete";
				meta.spLoadAll = "proc_BiayaJabatanLoadAll";
				meta.spLoadByPrimaryKey = "proc_BiayaJabatanLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BiayaJabatanMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
