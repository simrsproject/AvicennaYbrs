/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:25 PM
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
	abstract public class esSalaryComponentRoundingCollection : esEntityCollectionWAuditLog
	{
		public esSalaryComponentRoundingCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "SalaryComponentRoundingCollection";
		}

		#region Query Logic
		protected void InitQuery(esSalaryComponentRoundingQuery query)
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
			this.InitQuery(query as esSalaryComponentRoundingQuery);
		}
		#endregion
		
		virtual public SalaryComponentRounding DetachEntity(SalaryComponentRounding entity)
		{
			return base.DetachEntity(entity) as SalaryComponentRounding;
		}
		
		virtual public SalaryComponentRounding AttachEntity(SalaryComponentRounding entity)
		{
			return base.AttachEntity(entity) as SalaryComponentRounding;
		}
		
		virtual public void Combine(SalaryComponentRoundingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public SalaryComponentRounding this[int index]
		{
			get
			{
				return base[index] as SalaryComponentRounding;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SalaryComponentRounding);
		}
	}



	[Serializable]
	abstract public class esSalaryComponentRounding : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSalaryComponentRoundingQuery GetDynamicQuery()
		{
			return null;
		}

		public esSalaryComponentRounding()
		{

		}

		public esSalaryComponentRounding(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 salaryComponentRoundingID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(salaryComponentRoundingID);
			else
				return LoadByPrimaryKeyStoredProcedure(salaryComponentRoundingID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 salaryComponentRoundingID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(salaryComponentRoundingID);
			else
				return LoadByPrimaryKeyStoredProcedure(salaryComponentRoundingID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 salaryComponentRoundingID)
		{
			esSalaryComponentRoundingQuery query = this.GetDynamicQuery();
			query.Where(query.SalaryComponentRoundingID == salaryComponentRoundingID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 salaryComponentRoundingID)
		{
			esParameters parms = new esParameters();
			parms.Add("SalaryComponentRoundingID",salaryComponentRoundingID);
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
						case "SalaryComponentRoundingID": this.str.SalaryComponentRoundingID = (string)value; break;							
						case "SalaryComponentRoundingName": this.str.SalaryComponentRoundingName = (string)value; break;							
						case "NominalValue": this.str.NominalValue = (string)value; break;							
						case "NearestValue": this.str.NearestValue = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "SalaryComponentRoundingID":
						
							if (value == null || value is System.Int32)
								this.SalaryComponentRoundingID = (System.Int32?)value;
							break;
						
						case "NominalValue":
						
							if (value == null || value is System.Decimal)
								this.NominalValue = (System.Decimal?)value;
							break;
						
						case "NearestValue":
						
							if (value == null || value is System.Decimal)
								this.NearestValue = (System.Decimal?)value;
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
		/// Maps to SalaryComponentRounding.SalaryComponentRoundingID
		/// </summary>
		virtual public System.Int32? SalaryComponentRoundingID
		{
			get
			{
				return base.GetSystemInt32(SalaryComponentRoundingMetadata.ColumnNames.SalaryComponentRoundingID);
			}
			
			set
			{
				base.SetSystemInt32(SalaryComponentRoundingMetadata.ColumnNames.SalaryComponentRoundingID, value);
			}
		}
		
		/// <summary>
		/// Maps to SalaryComponentRounding.SalaryComponentRoundingName
		/// </summary>
		virtual public System.String SalaryComponentRoundingName
		{
			get
			{
				return base.GetSystemString(SalaryComponentRoundingMetadata.ColumnNames.SalaryComponentRoundingName);
			}
			
			set
			{
				base.SetSystemString(SalaryComponentRoundingMetadata.ColumnNames.SalaryComponentRoundingName, value);
			}
		}
		
		/// <summary>
		/// Maps to SalaryComponentRounding.NominalValue
		/// </summary>
		virtual public System.Decimal? NominalValue
		{
			get
			{
				return base.GetSystemDecimal(SalaryComponentRoundingMetadata.ColumnNames.NominalValue);
			}
			
			set
			{
				base.SetSystemDecimal(SalaryComponentRoundingMetadata.ColumnNames.NominalValue, value);
			}
		}
		
		/// <summary>
		/// Maps to SalaryComponentRounding.NearestValue
		/// </summary>
		virtual public System.Decimal? NearestValue
		{
			get
			{
				return base.GetSystemDecimal(SalaryComponentRoundingMetadata.ColumnNames.NearestValue);
			}
			
			set
			{
				base.SetSystemDecimal(SalaryComponentRoundingMetadata.ColumnNames.NearestValue, value);
			}
		}
		
		/// <summary>
		/// Maps to SalaryComponentRounding.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SalaryComponentRoundingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(SalaryComponentRoundingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to SalaryComponentRounding.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SalaryComponentRoundingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(SalaryComponentRoundingMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSalaryComponentRounding entity)
			{
				this.entity = entity;
			}
			
	
			public System.String SalaryComponentRoundingID
			{
				get
				{
					System.Int32? data = entity.SalaryComponentRoundingID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryComponentRoundingID = null;
					else entity.SalaryComponentRoundingID = Convert.ToInt32(value);
				}
			}
				
			public System.String SalaryComponentRoundingName
			{
				get
				{
					System.String data = entity.SalaryComponentRoundingName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryComponentRoundingName = null;
					else entity.SalaryComponentRoundingName = Convert.ToString(value);
				}
			}
				
			public System.String NominalValue
			{
				get
				{
					System.Decimal? data = entity.NominalValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NominalValue = null;
					else entity.NominalValue = Convert.ToDecimal(value);
				}
			}
				
			public System.String NearestValue
			{
				get
				{
					System.Decimal? data = entity.NearestValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NearestValue = null;
					else entity.NearestValue = Convert.ToDecimal(value);
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
			

			private esSalaryComponentRounding entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSalaryComponentRoundingQuery query)
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
				throw new Exception("esSalaryComponentRounding can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class SalaryComponentRounding : esSalaryComponentRounding
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
	abstract public class esSalaryComponentRoundingQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return SalaryComponentRoundingMetadata.Meta();
			}
		}	
		

		public esQueryItem SalaryComponentRoundingID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRoundingMetadata.ColumnNames.SalaryComponentRoundingID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SalaryComponentRoundingName
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRoundingMetadata.ColumnNames.SalaryComponentRoundingName, esSystemType.String);
			}
		} 
		
		public esQueryItem NominalValue
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRoundingMetadata.ColumnNames.NominalValue, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem NearestValue
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRoundingMetadata.ColumnNames.NearestValue, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRoundingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SalaryComponentRoundingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SalaryComponentRoundingCollection")]
	public partial class SalaryComponentRoundingCollection : esSalaryComponentRoundingCollection, IEnumerable<SalaryComponentRounding>
	{
		public SalaryComponentRoundingCollection()
		{

		}
		
		public static implicit operator List<SalaryComponentRounding>(SalaryComponentRoundingCollection coll)
		{
			List<SalaryComponentRounding> list = new List<SalaryComponentRounding>();
			
			foreach (SalaryComponentRounding emp in coll)
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
				return  SalaryComponentRoundingMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SalaryComponentRoundingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SalaryComponentRounding(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SalaryComponentRounding();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public SalaryComponentRoundingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SalaryComponentRoundingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(SalaryComponentRoundingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public SalaryComponentRounding AddNew()
		{
			SalaryComponentRounding entity = base.AddNewEntity() as SalaryComponentRounding;
			
			return entity;
		}

		public SalaryComponentRounding FindByPrimaryKey(System.Int32 salaryComponentRoundingID)
		{
			return base.FindByPrimaryKey(salaryComponentRoundingID) as SalaryComponentRounding;
		}


		#region IEnumerable<SalaryComponentRounding> Members

		IEnumerator<SalaryComponentRounding> IEnumerable<SalaryComponentRounding>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as SalaryComponentRounding;
			}
		}

		#endregion
		
		private SalaryComponentRoundingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SalaryComponentRounding' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("SalaryComponentRounding ({SalaryComponentRoundingID})")]
	[Serializable]
	public partial class SalaryComponentRounding : esSalaryComponentRounding
	{
		public SalaryComponentRounding()
		{

		}
	
		public SalaryComponentRounding(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SalaryComponentRoundingMetadata.Meta();
			}
		}
		
		
		
		override protected esSalaryComponentRoundingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SalaryComponentRoundingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public SalaryComponentRoundingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SalaryComponentRoundingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(SalaryComponentRoundingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private SalaryComponentRoundingQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class SalaryComponentRoundingQuery : esSalaryComponentRoundingQuery
	{
		public SalaryComponentRoundingQuery()
		{

		}		
		
		public SalaryComponentRoundingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "SalaryComponentRoundingQuery";
        }
		
			
	}


	[Serializable]
	public partial class SalaryComponentRoundingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SalaryComponentRoundingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SalaryComponentRoundingMetadata.ColumnNames.SalaryComponentRoundingID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryComponentRoundingMetadata.PropertyNames.SalaryComponentRoundingID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(SalaryComponentRoundingMetadata.ColumnNames.SalaryComponentRoundingName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentRoundingMetadata.PropertyNames.SalaryComponentRoundingName;
			c.CharacterMaxLength = 250;
			_columns.Add(c);
				
			c = new esColumnMetadata(SalaryComponentRoundingMetadata.ColumnNames.NominalValue, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SalaryComponentRoundingMetadata.PropertyNames.NominalValue;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(SalaryComponentRoundingMetadata.ColumnNames.NearestValue, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SalaryComponentRoundingMetadata.PropertyNames.NearestValue;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(SalaryComponentRoundingMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SalaryComponentRoundingMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(SalaryComponentRoundingMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryComponentRoundingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public SalaryComponentRoundingMetadata Meta()
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
			 public const string SalaryComponentRoundingID = "SalaryComponentRoundingID";
			 public const string SalaryComponentRoundingName = "SalaryComponentRoundingName";
			 public const string NominalValue = "NominalValue";
			 public const string NearestValue = "NearestValue";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SalaryComponentRoundingID = "SalaryComponentRoundingID";
			 public const string SalaryComponentRoundingName = "SalaryComponentRoundingName";
			 public const string NominalValue = "NominalValue";
			 public const string NearestValue = "NearestValue";
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
			lock (typeof(SalaryComponentRoundingMetadata))
			{
				if(SalaryComponentRoundingMetadata.mapDelegates == null)
				{
					SalaryComponentRoundingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (SalaryComponentRoundingMetadata.meta == null)
				{
					SalaryComponentRoundingMetadata.meta = new SalaryComponentRoundingMetadata();
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
				

				meta.AddTypeMap("SalaryComponentRoundingID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SalaryComponentRoundingName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NominalValue", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("NearestValue", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "SalaryComponentRounding";
				meta.Destination = "SalaryComponentRounding";
				
				meta.spInsert = "proc_SalaryComponentRoundingInsert";				
				meta.spUpdate = "proc_SalaryComponentRoundingUpdate";		
				meta.spDelete = "proc_SalaryComponentRoundingDelete";
				meta.spLoadAll = "proc_SalaryComponentRoundingLoadAll";
				meta.spLoadByPrimaryKey = "proc_SalaryComponentRoundingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SalaryComponentRoundingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
