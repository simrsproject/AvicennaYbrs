/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 12/6/2016 11:41:08 PM
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
	abstract public class esBpjsCMGCollection : esEntityCollectionWAuditLog
	{
		public esBpjsCMGCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "BpjsCMGCollection";
		}

		#region Query Logic
		protected void InitQuery(esBpjsCMGQuery query)
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
			this.InitQuery(query as esBpjsCMGQuery);
		}
		#endregion
		
		virtual public BpjsCMG DetachEntity(BpjsCMG entity)
		{
			return base.DetachEntity(entity) as BpjsCMG;
		}
		
		virtual public BpjsCMG AttachEntity(BpjsCMG entity)
		{
			return base.AttachEntity(entity) as BpjsCMG;
		}
		
		virtual public void Combine(BpjsCMGCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BpjsCMG this[int index]
		{
			get
			{
				return base[index] as BpjsCMG;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BpjsCMG);
		}
	}



	[Serializable]
	abstract public class esBpjsCMG : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBpjsCMGQuery GetDynamicQuery()
		{
			return null;
		}

		public esBpjsCMG()
		{

		}

		public esBpjsCMG(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String noSEP, System.String kodeCMG)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(noSEP, kodeCMG);
			else
				return LoadByPrimaryKeyStoredProcedure(noSEP, kodeCMG);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String noSEP, System.String kodeCMG)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(noSEP, kodeCMG);
			else
				return LoadByPrimaryKeyStoredProcedure(noSEP, kodeCMG);
		}

		private bool LoadByPrimaryKeyDynamic(System.String noSEP, System.String kodeCMG)
		{
			esBpjsCMGQuery query = this.GetDynamicQuery();
			query.Where(query.NoSEP == noSEP, query.KodeCMG == kodeCMG);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String noSEP, System.String kodeCMG)
		{
			esParameters parms = new esParameters();
			parms.Add("NoSEP",noSEP);			parms.Add("KodeCMG",kodeCMG);
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
						case "NoSEP": this.str.NoSEP = (string)value; break;							
						case "KodeCMG": this.str.KodeCMG = (string)value; break;							
						case "TariffCMG": this.str.TariffCMG = (string)value; break;							
						case "DeskripsiCMG": this.str.DeskripsiCMG = (string)value; break;							
						case "TipeCMG": this.str.TipeCMG = (string)value; break;							
						case "IsOptionCMG": this.str.IsOptionCMG = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "TariffCMG":
						
							if (value == null || value is System.Decimal)
								this.TariffCMG = (System.Decimal?)value;
							break;
						
						case "IsOptionCMG":
						
							if (value == null || value is System.Boolean)
								this.IsOptionCMG = (System.Boolean?)value;
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
		/// Maps to BpjsCMG.NoSEP
		/// </summary>
		virtual public System.String NoSEP
		{
			get
			{
				return base.GetSystemString(BpjsCMGMetadata.ColumnNames.NoSEP);
			}
			
			set
			{
				base.SetSystemString(BpjsCMGMetadata.ColumnNames.NoSEP, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsCMG.KodeCMG
		/// </summary>
		virtual public System.String KodeCMG
		{
			get
			{
				return base.GetSystemString(BpjsCMGMetadata.ColumnNames.KodeCMG);
			}
			
			set
			{
				base.SetSystemString(BpjsCMGMetadata.ColumnNames.KodeCMG, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsCMG.TariffCMG
		/// </summary>
		virtual public System.Decimal? TariffCMG
		{
			get
			{
				return base.GetSystemDecimal(BpjsCMGMetadata.ColumnNames.TariffCMG);
			}
			
			set
			{
				base.SetSystemDecimal(BpjsCMGMetadata.ColumnNames.TariffCMG, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsCMG.DeskripsiCMG
		/// </summary>
		virtual public System.String DeskripsiCMG
		{
			get
			{
				return base.GetSystemString(BpjsCMGMetadata.ColumnNames.DeskripsiCMG);
			}
			
			set
			{
				base.SetSystemString(BpjsCMGMetadata.ColumnNames.DeskripsiCMG, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsCMG.TipeCMG
		/// </summary>
		virtual public System.String TipeCMG
		{
			get
			{
				return base.GetSystemString(BpjsCMGMetadata.ColumnNames.TipeCMG);
			}
			
			set
			{
				base.SetSystemString(BpjsCMGMetadata.ColumnNames.TipeCMG, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsCMG.IsOptionCMG
		/// </summary>
		virtual public System.Boolean? IsOptionCMG
		{
			get
			{
				return base.GetSystemBoolean(BpjsCMGMetadata.ColumnNames.IsOptionCMG);
			}
			
			set
			{
				base.SetSystemBoolean(BpjsCMGMetadata.ColumnNames.IsOptionCMG, value);
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
			public esStrings(esBpjsCMG entity)
			{
				this.entity = entity;
			}
			
	
			public System.String NoSEP
			{
				get
				{
					System.String data = entity.NoSEP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoSEP = null;
					else entity.NoSEP = Convert.ToString(value);
				}
			}
				
			public System.String KodeCMG
			{
				get
				{
					System.String data = entity.KodeCMG;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeCMG = null;
					else entity.KodeCMG = Convert.ToString(value);
				}
			}
				
			public System.String TariffCMG
			{
				get
				{
					System.Decimal? data = entity.TariffCMG;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffCMG = null;
					else entity.TariffCMG = Convert.ToDecimal(value);
				}
			}
				
			public System.String DeskripsiCMG
			{
				get
				{
					System.String data = entity.DeskripsiCMG;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeskripsiCMG = null;
					else entity.DeskripsiCMG = Convert.ToString(value);
				}
			}
				
			public System.String TipeCMG
			{
				get
				{
					System.String data = entity.TipeCMG;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TipeCMG = null;
					else entity.TipeCMG = Convert.ToString(value);
				}
			}
				
			public System.String IsOptionCMG
			{
				get
				{
					System.Boolean? data = entity.IsOptionCMG;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOptionCMG = null;
					else entity.IsOptionCMG = Convert.ToBoolean(value);
				}
			}
			

			private esBpjsCMG entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBpjsCMGQuery query)
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
				throw new Exception("esBpjsCMG can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esBpjsCMGQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return BpjsCMGMetadata.Meta();
			}
		}	
		

		public esQueryItem NoSEP
		{
			get
			{
				return new esQueryItem(this, BpjsCMGMetadata.ColumnNames.NoSEP, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeCMG
		{
			get
			{
				return new esQueryItem(this, BpjsCMGMetadata.ColumnNames.KodeCMG, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffCMG
		{
			get
			{
				return new esQueryItem(this, BpjsCMGMetadata.ColumnNames.TariffCMG, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DeskripsiCMG
		{
			get
			{
				return new esQueryItem(this, BpjsCMGMetadata.ColumnNames.DeskripsiCMG, esSystemType.String);
			}
		} 
		
		public esQueryItem TipeCMG
		{
			get
			{
				return new esQueryItem(this, BpjsCMGMetadata.ColumnNames.TipeCMG, esSystemType.String);
			}
		} 
		
		public esQueryItem IsOptionCMG
		{
			get
			{
				return new esQueryItem(this, BpjsCMGMetadata.ColumnNames.IsOptionCMG, esSystemType.Boolean);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BpjsCMGCollection")]
	public partial class BpjsCMGCollection : esBpjsCMGCollection, IEnumerable<BpjsCMG>
	{
		public BpjsCMGCollection()
		{

		}
		
		public static implicit operator List<BpjsCMG>(BpjsCMGCollection coll)
		{
			List<BpjsCMG> list = new List<BpjsCMG>();
			
			foreach (BpjsCMG emp in coll)
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
				return  BpjsCMGMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BpjsCMGQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BpjsCMG(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BpjsCMG();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public BpjsCMGQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BpjsCMGQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(BpjsCMGQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public BpjsCMG AddNew()
		{
			BpjsCMG entity = base.AddNewEntity() as BpjsCMG;
			
			return entity;
		}

		public BpjsCMG FindByPrimaryKey(System.String noSEP, System.String kodeCMG)
		{
			return base.FindByPrimaryKey(noSEP, kodeCMG) as BpjsCMG;
		}


		#region IEnumerable<BpjsCMG> Members

		IEnumerator<BpjsCMG> IEnumerable<BpjsCMG>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BpjsCMG;
			}
		}

		#endregion
		
		private BpjsCMGQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BpjsCMG' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("BpjsCMG ({NoSEP},{KodeCMG})")]
	[Serializable]
	public partial class BpjsCMG : esBpjsCMG
	{
		public BpjsCMG()
		{

		}
	
		public BpjsCMG(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BpjsCMGMetadata.Meta();
			}
		}
		
		
		
		override protected esBpjsCMGQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BpjsCMGQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public BpjsCMGQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BpjsCMGQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(BpjsCMGQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private BpjsCMGQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class BpjsCMGQuery : esBpjsCMGQuery
	{
		public BpjsCMGQuery()
		{

		}		
		
		public BpjsCMGQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "BpjsCMGQuery";
        }
		
			
	}


	[Serializable]
	public partial class BpjsCMGMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BpjsCMGMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BpjsCMGMetadata.ColumnNames.NoSEP, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsCMGMetadata.PropertyNames.NoSEP;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsCMGMetadata.ColumnNames.KodeCMG, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsCMGMetadata.PropertyNames.KodeCMG;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsCMGMetadata.ColumnNames.TariffCMG, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BpjsCMGMetadata.PropertyNames.TariffCMG;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsCMGMetadata.ColumnNames.DeskripsiCMG, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsCMGMetadata.PropertyNames.DeskripsiCMG;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsCMGMetadata.ColumnNames.TipeCMG, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsCMGMetadata.PropertyNames.TipeCMG;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsCMGMetadata.ColumnNames.IsOptionCMG, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BpjsCMGMetadata.PropertyNames.IsOptionCMG;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public BpjsCMGMetadata Meta()
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
			 public const string NoSEP = "NoSEP";
			 public const string KodeCMG = "KodeCMG";
			 public const string TariffCMG = "TariffCMG";
			 public const string DeskripsiCMG = "DeskripsiCMG";
			 public const string TipeCMG = "TipeCMG";
			 public const string IsOptionCMG = "IsOptionCMG";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string NoSEP = "NoSEP";
			 public const string KodeCMG = "KodeCMG";
			 public const string TariffCMG = "TariffCMG";
			 public const string DeskripsiCMG = "DeskripsiCMG";
			 public const string TipeCMG = "TipeCMG";
			 public const string IsOptionCMG = "IsOptionCMG";
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
			lock (typeof(BpjsCMGMetadata))
			{
				if(BpjsCMGMetadata.mapDelegates == null)
				{
					BpjsCMGMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BpjsCMGMetadata.meta == null)
				{
					BpjsCMGMetadata.meta = new BpjsCMGMetadata();
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
				

				meta.AddTypeMap("NoSEP", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodeCMG", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffCMG", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DeskripsiCMG", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TipeCMG", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOptionCMG", new esTypeMap("bit", "System.Boolean"));			
				
				
				
				meta.Source = "BpjsCMG";
				meta.Destination = "BpjsCMG";
				
				meta.spInsert = "proc_BpjsCMGInsert";				
				meta.spUpdate = "proc_BpjsCMGUpdate";		
				meta.spDelete = "proc_BpjsCMGDelete";
				meta.spLoadAll = "proc_BpjsCMGLoadAll";
				meta.spLoadByPrimaryKey = "proc_BpjsCMGLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BpjsCMGMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
