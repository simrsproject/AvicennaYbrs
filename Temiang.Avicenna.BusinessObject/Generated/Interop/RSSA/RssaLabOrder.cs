/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/10/2012 9:36:45 AM
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



namespace Temiang.Avicenna.BusinessObject.Interop.RSSA
{

	[Serializable]
	abstract public class esRssaLabOrderCollection : esEntityCollectionWAuditLog
	{
		public esRssaLabOrderCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RssaLabOrderCollection";
		}

		#region Query Logic
		protected void InitQuery(esRssaLabOrderQuery query)
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
			this.InitQuery(query as esRssaLabOrderQuery);
		}
		#endregion
		
		virtual public RssaLabOrder DetachEntity(RssaLabOrder entity)
		{
			return base.DetachEntity(entity) as RssaLabOrder;
		}
		
		virtual public RssaLabOrder AttachEntity(RssaLabOrder entity)
		{
			return base.AttachEntity(entity) as RssaLabOrder;
		}
		
		virtual public void Combine(RssaLabOrderCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RssaLabOrder this[int index]
		{
			get
			{
				return base[index] as RssaLabOrder;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RssaLabOrder);
		}
	}



	[Serializable]
	abstract public class esRssaLabOrder : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRssaLabOrderQuery GetDynamicQuery()
		{
			return null;
		}

		public esRssaLabOrder()
		{

		}

		public esRssaLabOrder(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String transactionNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String transactionNo)
		{
			esRssaLabOrderQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "TransactionDate": this.str.TransactionDate = (string)value; break;
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
		/// Maps to RssaLabOrder.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(RssaLabOrderMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(RssaLabOrderMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RssaLabOrder.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(RssaLabOrderMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(RssaLabOrderMetadata.ColumnNames.TransactionDate, value);
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
			public esStrings(esRssaLabOrder entity)
			{
				this.entity = entity;
			}
			
	
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
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
			

			private esRssaLabOrder entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRssaLabOrderQuery query)
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
				throw new Exception("esRssaLabOrder can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RssaLabOrder : esRssaLabOrder
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
	abstract public class esRssaLabOrderQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RssaLabOrderMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, RssaLabOrderMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, RssaLabOrderMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RssaLabOrderCollection")]
	public partial class RssaLabOrderCollection : esRssaLabOrderCollection, IEnumerable<RssaLabOrder>
	{
		public RssaLabOrderCollection()
		{

		}
		
		public static implicit operator List<RssaLabOrder>(RssaLabOrderCollection coll)
		{
			List<RssaLabOrder> list = new List<RssaLabOrder>();
			
			foreach (RssaLabOrder emp in coll)
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
				return  RssaLabOrderMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RssaLabOrderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RssaLabOrder(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RssaLabOrder();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RssaLabOrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RssaLabOrderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RssaLabOrderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RssaLabOrder AddNew()
		{
			RssaLabOrder entity = base.AddNewEntity() as RssaLabOrder;
			
			return entity;
		}

		public RssaLabOrder FindByPrimaryKey(System.String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as RssaLabOrder;
		}


		#region IEnumerable<RssaLabOrder> Members

		IEnumerator<RssaLabOrder> IEnumerable<RssaLabOrder>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RssaLabOrder;
			}
		}

		#endregion
		
		private RssaLabOrderQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RssaLabOrder' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RssaLabOrder ({TransactionNo})")]
	[Serializable]
	public partial class RssaLabOrder : esRssaLabOrder
	{
		public RssaLabOrder()
		{

		}
	
		public RssaLabOrder(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RssaLabOrderMetadata.Meta();
			}
		}
		
		
		
		override protected esRssaLabOrderQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RssaLabOrderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RssaLabOrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RssaLabOrderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RssaLabOrderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RssaLabOrderQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RssaLabOrderQuery : esRssaLabOrderQuery
	{
		public RssaLabOrderQuery()
		{

		}		
		
		public RssaLabOrderQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RssaLabOrderQuery";
        }
		
			
	}


	[Serializable]
	public partial class RssaLabOrderMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RssaLabOrderMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RssaLabOrderMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RssaLabOrderMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RssaLabOrderMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RssaLabOrderMetadata.PropertyNames.TransactionDate;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RssaLabOrderMetadata Meta()
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
			 public const string TransactionNo = "TransactionNo";
			 public const string TransactionDate = "TransactionDate";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransactionNo = "TransactionNo";
			 public const string TransactionDate = "TransactionDate";
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
			lock (typeof(RssaLabOrderMetadata))
			{
				if(RssaLabOrderMetadata.mapDelegates == null)
				{
					RssaLabOrderMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RssaLabOrderMetadata.meta == null)
				{
					RssaLabOrderMetadata.meta = new RssaLabOrderMetadata();
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
				

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "RssaLabOrder";
				meta.Destination = "RssaLabOrder";
				
				meta.spInsert = "proc_RssaLabOrderInsert";				
				meta.spUpdate = "proc_RssaLabOrderUpdate";		
				meta.spDelete = "proc_RssaLabOrderDelete";
				meta.spLoadAll = "proc_RssaLabOrderLoadAll";
				meta.spLoadByPrimaryKey = "proc_RssaLabOrderLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RssaLabOrderMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
