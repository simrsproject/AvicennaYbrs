/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:13 PM
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
	abstract public class esClosingAccountingCollection : esEntityCollectionWAuditLog
	{
		public esClosingAccountingCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ClosingAccountingCollection";
		}

		#region Query Logic
		protected void InitQuery(esClosingAccountingQuery query)
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
			this.InitQuery(query as esClosingAccountingQuery);
		}
		#endregion
		
		virtual public ClosingAccounting DetachEntity(ClosingAccounting entity)
		{
			return base.DetachEntity(entity) as ClosingAccounting;
		}
		
		virtual public ClosingAccounting AttachEntity(ClosingAccounting entity)
		{
			return base.AttachEntity(entity) as ClosingAccounting;
		}
		
		virtual public void Combine(ClosingAccountingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ClosingAccounting this[int index]
		{
			get
			{
				return base[index] as ClosingAccounting;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ClosingAccounting);
		}
	}



	[Serializable]
	abstract public class esClosingAccounting : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esClosingAccountingQuery GetDynamicQuery()
		{
			return null;
		}

		public esClosingAccounting()
		{

		}

		public esClosingAccounting(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String yearNo, System.String monthNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(yearNo, monthNo);
			else
				return LoadByPrimaryKeyStoredProcedure(yearNo, monthNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String yearNo, System.String monthNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(yearNo, monthNo);
			else
				return LoadByPrimaryKeyStoredProcedure(yearNo, monthNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String yearNo, System.String monthNo)
		{
			esClosingAccountingQuery query = this.GetDynamicQuery();
			query.Where(query.YearNo == yearNo, query.MonthNo == monthNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String yearNo, System.String monthNo)
		{
			esParameters parms = new esParameters();
			parms.Add("YearNo",yearNo);			parms.Add("MonthNo",monthNo);
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
						case "YearNo": this.str.YearNo = (string)value; break;							
						case "MonthNo": this.str.MonthNo = (string)value; break;							
						case "IsClosed": this.str.IsClosed = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsClosed":
						
							if (value == null || value is System.Boolean)
								this.IsClosed = (System.Boolean?)value;
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
		/// Maps to ClosingAccounting.YearNo
		/// </summary>
		virtual public System.String YearNo
		{
			get
			{
				return base.GetSystemString(ClosingAccountingMetadata.ColumnNames.YearNo);
			}
			
			set
			{
				base.SetSystemString(ClosingAccountingMetadata.ColumnNames.YearNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ClosingAccounting.MonthNo
		/// </summary>
		virtual public System.String MonthNo
		{
			get
			{
				return base.GetSystemString(ClosingAccountingMetadata.ColumnNames.MonthNo);
			}
			
			set
			{
				base.SetSystemString(ClosingAccountingMetadata.ColumnNames.MonthNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ClosingAccounting.IsClosed
		/// </summary>
		virtual public System.Boolean? IsClosed
		{
			get
			{
				return base.GetSystemBoolean(ClosingAccountingMetadata.ColumnNames.IsClosed);
			}
			
			set
			{
				base.SetSystemBoolean(ClosingAccountingMetadata.ColumnNames.IsClosed, value);
			}
		}
		
		/// <summary>
		/// Maps to ClosingAccounting.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ClosingAccountingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ClosingAccountingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ClosingAccounting.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ClosingAccountingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ClosingAccountingMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esClosingAccounting entity)
			{
				this.entity = entity;
			}
			
	
			public System.String YearNo
			{
				get
				{
					System.String data = entity.YearNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.YearNo = null;
					else entity.YearNo = Convert.ToString(value);
				}
			}
				
			public System.String MonthNo
			{
				get
				{
					System.String data = entity.MonthNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MonthNo = null;
					else entity.MonthNo = Convert.ToString(value);
				}
			}
				
			public System.String IsClosed
			{
				get
				{
					System.Boolean? data = entity.IsClosed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClosed = null;
					else entity.IsClosed = Convert.ToBoolean(value);
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
			

			private esClosingAccounting entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esClosingAccountingQuery query)
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
				throw new Exception("esClosingAccounting can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ClosingAccounting : esClosingAccounting
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
	abstract public class esClosingAccountingQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ClosingAccountingMetadata.Meta();
			}
		}	
		

		public esQueryItem YearNo
		{
			get
			{
				return new esQueryItem(this, ClosingAccountingMetadata.ColumnNames.YearNo, esSystemType.String);
			}
		} 
		
		public esQueryItem MonthNo
		{
			get
			{
				return new esQueryItem(this, ClosingAccountingMetadata.ColumnNames.MonthNo, esSystemType.String);
			}
		} 
		
		public esQueryItem IsClosed
		{
			get
			{
				return new esQueryItem(this, ClosingAccountingMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ClosingAccountingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ClosingAccountingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ClosingAccountingCollection")]
	public partial class ClosingAccountingCollection : esClosingAccountingCollection, IEnumerable<ClosingAccounting>
	{
		public ClosingAccountingCollection()
		{

		}
		
		public static implicit operator List<ClosingAccounting>(ClosingAccountingCollection coll)
		{
			List<ClosingAccounting> list = new List<ClosingAccounting>();
			
			foreach (ClosingAccounting emp in coll)
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
				return  ClosingAccountingMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClosingAccountingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ClosingAccounting(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ClosingAccounting();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ClosingAccountingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClosingAccountingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ClosingAccountingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ClosingAccounting AddNew()
		{
			ClosingAccounting entity = base.AddNewEntity() as ClosingAccounting;
			
			return entity;
		}

		public ClosingAccounting FindByPrimaryKey(System.String yearNo, System.String monthNo)
		{
			return base.FindByPrimaryKey(yearNo, monthNo) as ClosingAccounting;
		}


		#region IEnumerable<ClosingAccounting> Members

		IEnumerator<ClosingAccounting> IEnumerable<ClosingAccounting>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ClosingAccounting;
			}
		}

		#endregion
		
		private ClosingAccountingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ClosingAccounting' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ClosingAccounting ({YearNo},{MonthNo})")]
	[Serializable]
	public partial class ClosingAccounting : esClosingAccounting
	{
		public ClosingAccounting()
		{

		}
	
		public ClosingAccounting(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ClosingAccountingMetadata.Meta();
			}
		}
		
		
		
		override protected esClosingAccountingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClosingAccountingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ClosingAccountingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClosingAccountingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ClosingAccountingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ClosingAccountingQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ClosingAccountingQuery : esClosingAccountingQuery
	{
		public ClosingAccountingQuery()
		{

		}		
		
		public ClosingAccountingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ClosingAccountingQuery";
        }
		
			
	}


	[Serializable]
	public partial class ClosingAccountingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ClosingAccountingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ClosingAccountingMetadata.ColumnNames.YearNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ClosingAccountingMetadata.PropertyNames.YearNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 4;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClosingAccountingMetadata.ColumnNames.MonthNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ClosingAccountingMetadata.PropertyNames.MonthNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClosingAccountingMetadata.ColumnNames.IsClosed, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ClosingAccountingMetadata.PropertyNames.IsClosed;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClosingAccountingMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ClosingAccountingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ClosingAccountingMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ClosingAccountingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ClosingAccountingMetadata Meta()
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
			 public const string YearNo = "YearNo";
			 public const string MonthNo = "MonthNo";
			 public const string IsClosed = "IsClosed";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string YearNo = "YearNo";
			 public const string MonthNo = "MonthNo";
			 public const string IsClosed = "IsClosed";
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
			lock (typeof(ClosingAccountingMetadata))
			{
				if(ClosingAccountingMetadata.mapDelegates == null)
				{
					ClosingAccountingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ClosingAccountingMetadata.meta == null)
				{
					ClosingAccountingMetadata.meta = new ClosingAccountingMetadata();
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
				

				meta.AddTypeMap("YearNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MonthNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ClosingAccounting";
				meta.Destination = "ClosingAccounting";
				
				meta.spInsert = "proc_ClosingAccountingInsert";				
				meta.spUpdate = "proc_ClosingAccountingUpdate";		
				meta.spDelete = "proc_ClosingAccountingDelete";
				meta.spLoadAll = "proc_ClosingAccountingLoadAll";
				meta.spLoadByPrimaryKey = "proc_ClosingAccountingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ClosingAccountingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
