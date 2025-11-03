/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 10/7/2014 8:52:01 AM
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
	abstract public class esDataRptHeaderCollection : esEntityCollection
	{
		public esDataRptHeaderCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "DataRptHeaderCollection";
		}

		#region Query Logic
		protected void InitQuery(esDataRptHeaderQuery query)
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
			this.InitQuery(query as esDataRptHeaderQuery);
		}
		#endregion
		
		virtual public DataRptHeader DetachEntity(DataRptHeader entity)
		{
			return base.DetachEntity(entity) as DataRptHeader;
		}
		
		virtual public DataRptHeader AttachEntity(DataRptHeader entity)
		{
			return base.AttachEntity(entity) as DataRptHeader;
		}
		
		virtual public void Combine(DataRptHeaderCollection collection)
		{
			base.Combine(collection);
		}
		
		new public DataRptHeader this[int index]
		{
			get
			{
				return base[index] as DataRptHeader;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(DataRptHeader);
		}
	}



	[Serializable]
	abstract public class esDataRptHeader : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDataRptHeaderQuery GetDynamicQuery()
		{
			return null;
		}

		public esDataRptHeader()
		{

		}

		public esDataRptHeader(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String sRDataRpt, System.DateTime transactionDate)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRDataRpt, transactionDate);
			else
				return LoadByPrimaryKeyStoredProcedure(sRDataRpt, transactionDate);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String sRDataRpt, System.DateTime transactionDate)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRDataRpt, transactionDate);
			else
				return LoadByPrimaryKeyStoredProcedure(sRDataRpt, transactionDate);
		}

		private bool LoadByPrimaryKeyDynamic(System.String sRDataRpt, System.DateTime transactionDate)
		{
			esDataRptHeaderQuery query = this.GetDynamicQuery();
			query.Where(query.SRDataRpt == sRDataRpt, query.TransactionDate == transactionDate);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String sRDataRpt, System.DateTime transactionDate)
		{
			esParameters parms = new esParameters();
			parms.Add("SRDataRpt",sRDataRpt);			parms.Add("TransactionDate",transactionDate);
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
						case "SRDataRpt": this.str.SRDataRpt = (string)value; break;							
						case "TransactionDate": this.str.TransactionDate = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "TransactionDate":
						
							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
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
		/// Maps to DataRptHeader.SRDataRpt
		/// </summary>
		virtual public System.String SRDataRpt
		{
			get
			{
				return base.GetSystemString(DataRptHeaderMetadata.ColumnNames.SRDataRpt);
			}
			
			set
			{
				base.SetSystemString(DataRptHeaderMetadata.ColumnNames.SRDataRpt, value);
			}
		}
		
		/// <summary>
		/// Maps to DataRptHeader.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(DataRptHeaderMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(DataRptHeaderMetadata.ColumnNames.TransactionDate, value);
			}
		}
		
		/// <summary>
		/// Maps to DataRptHeader.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(DataRptHeaderMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(DataRptHeaderMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to DataRptHeader.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(DataRptHeaderMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(DataRptHeaderMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esDataRptHeader entity)
			{
				this.entity = entity;
			}
			
	
			public System.String SRDataRpt
			{
				get
				{
					System.String data = entity.SRDataRpt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDataRpt = null;
					else entity.SRDataRpt = Convert.ToString(value);
				}
			}
				
			public System.String TransactionDate
			{
				get
				{
					System.DateTime? data = entity.TransactionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionDate = null;
					else entity.TransactionDate = Convert.ToDateTime(value);
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
			

			private esDataRptHeader entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDataRptHeaderQuery query)
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
				throw new Exception("esDataRptHeader can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esDataRptHeaderQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return DataRptHeaderMetadata.Meta();
			}
		}	
		

		public esQueryItem SRDataRpt
		{
			get
			{
				return new esQueryItem(this, DataRptHeaderMetadata.ColumnNames.SRDataRpt, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, DataRptHeaderMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, DataRptHeaderMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, DataRptHeaderMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DataRptHeaderCollection")]
	public partial class DataRptHeaderCollection : esDataRptHeaderCollection, IEnumerable<DataRptHeader>
	{
		public DataRptHeaderCollection()
		{

		}
		
		public static implicit operator List<DataRptHeader>(DataRptHeaderCollection coll)
		{
			List<DataRptHeader> list = new List<DataRptHeader>();
			
			foreach (DataRptHeader emp in coll)
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
				return  DataRptHeaderMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DataRptHeaderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new DataRptHeader(row);
		}

		override protected esEntity CreateEntity()
		{
			return new DataRptHeader();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public DataRptHeaderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DataRptHeaderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(DataRptHeaderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public DataRptHeader AddNew()
		{
			DataRptHeader entity = base.AddNewEntity() as DataRptHeader;
			
			return entity;
		}

		public DataRptHeader FindByPrimaryKey(System.String sRDataRpt, System.DateTime transactionDate)
		{
			return base.FindByPrimaryKey(sRDataRpt, transactionDate) as DataRptHeader;
		}


		#region IEnumerable<DataRptHeader> Members

		IEnumerator<DataRptHeader> IEnumerable<DataRptHeader>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as DataRptHeader;
			}
		}

		#endregion
		
		private DataRptHeaderQuery query;
	}


	/// <summary>
	/// Encapsulates the 'DataRptHeader' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("DataRptHeader ({SRDataRpt},{TransactionDate})")]
	[Serializable]
	public partial class DataRptHeader : esDataRptHeader
	{
		public DataRptHeader()
		{

		}
	
		public DataRptHeader(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DataRptHeaderMetadata.Meta();
			}
		}
		
		
		
		override protected esDataRptHeaderQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DataRptHeaderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public DataRptHeaderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DataRptHeaderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(DataRptHeaderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private DataRptHeaderQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class DataRptHeaderQuery : esDataRptHeaderQuery
	{
		public DataRptHeaderQuery()
		{

		}		
		
		public DataRptHeaderQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "DataRptHeaderQuery";
        }
		
			
	}


	[Serializable]
	public partial class DataRptHeaderMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DataRptHeaderMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DataRptHeaderMetadata.ColumnNames.SRDataRpt, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = DataRptHeaderMetadata.PropertyNames.SRDataRpt;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(DataRptHeaderMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DataRptHeaderMetadata.PropertyNames.TransactionDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DataRptHeaderMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DataRptHeaderMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DataRptHeaderMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = DataRptHeaderMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public DataRptHeaderMetadata Meta()
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
			 public const string SRDataRpt = "SRDataRpt";
			 public const string TransactionDate = "TransactionDate";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SRDataRpt = "SRDataRpt";
			 public const string TransactionDate = "TransactionDate";
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
			lock (typeof(DataRptHeaderMetadata))
			{
				if(DataRptHeaderMetadata.mapDelegates == null)
				{
					DataRptHeaderMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (DataRptHeaderMetadata.meta == null)
				{
					DataRptHeaderMetadata.meta = new DataRptHeaderMetadata();
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
				

				meta.AddTypeMap("SRDataRpt", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "DataRptHeader";
				meta.Destination = "DataRptHeader";
				
				meta.spInsert = "proc_DataRptHeaderInsert";				
				meta.spUpdate = "proc_DataRptHeaderUpdate";		
				meta.spDelete = "proc_DataRptHeaderDelete";
				meta.spLoadAll = "proc_DataRptHeaderLoadAll";
				meta.spLoadByPrimaryKey = "proc_DataRptHeaderLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DataRptHeaderMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
