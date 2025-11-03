/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/25/2012 11:53:50 AM
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
	abstract public class esPettyCashItemCollection : esEntityCollectionWAuditLog
	{
		public esPettyCashItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PettyCashItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esPettyCashItemQuery query)
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
			this.InitQuery(query as esPettyCashItemQuery);
		}
		#endregion
		
		virtual public PettyCashItem DetachEntity(PettyCashItem entity)
		{
			return base.DetachEntity(entity) as PettyCashItem;
		}
		
		virtual public PettyCashItem AttachEntity(PettyCashItem entity)
		{
			return base.AttachEntity(entity) as PettyCashItem;
		}
		
		virtual public void Combine(PettyCashItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PettyCashItem this[int index]
		{
			get
			{
				return base[index] as PettyCashItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PettyCashItem);
		}
	}



	[Serializable]
	abstract public class esPettyCashItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPettyCashItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esPettyCashItem()
		{

		}

		public esPettyCashItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String transactionNo, System.String sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo, System.String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String transactionNo, System.String sequenceNo)
		{
			esPettyCashItemQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo, System.String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);			parms.Add("SequenceNo",sequenceNo);
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
						case "SequenceNo": this.str.SequenceNo = (string)value; break;							
						case "Description": this.str.Description = (string)value; break;							
						case "Debit": this.str.Debit = (string)value; break;							
						case "Credit": this.str.Credit = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Debit":
						
							if (value == null || value is System.Decimal)
								this.Debit = (System.Decimal?)value;
							break;
						
						case "Credit":
						
							if (value == null || value is System.Decimal)
								this.Credit = (System.Decimal?)value;
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
		/// Maps to PettyCashItem.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(PettyCashItemMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(PettyCashItemMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to PettyCashItem.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(PettyCashItemMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(PettyCashItemMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to PettyCashItem.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(PettyCashItemMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(PettyCashItemMetadata.ColumnNames.Description, value);
			}
		}
		
		/// <summary>
		/// Maps to PettyCashItem.Debit
		/// </summary>
		virtual public System.Decimal? Debit
		{
			get
			{
				return base.GetSystemDecimal(PettyCashItemMetadata.ColumnNames.Debit);
			}
			
			set
			{
				base.SetSystemDecimal(PettyCashItemMetadata.ColumnNames.Debit, value);
			}
		}
		
		/// <summary>
		/// Maps to PettyCashItem.Credit
		/// </summary>
		virtual public System.Decimal? Credit
		{
			get
			{
				return base.GetSystemDecimal(PettyCashItemMetadata.ColumnNames.Credit);
			}
			
			set
			{
				base.SetSystemDecimal(PettyCashItemMetadata.ColumnNames.Credit, value);
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
			public esStrings(esPettyCashItem entity)
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
				
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
				}
			}
				
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
				}
			}
				
			public System.String Debit
			{
				get
				{
					System.Decimal? data = entity.Debit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Debit = null;
					else entity.Debit = Convert.ToDecimal(value);
				}
			}
				
			public System.String Credit
			{
				get
				{
					System.Decimal? data = entity.Credit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Credit = null;
					else entity.Credit = Convert.ToDecimal(value);
				}
			}
			

			private esPettyCashItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPettyCashItemQuery query)
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
				throw new Exception("esPettyCashItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PettyCashItem : esPettyCashItem
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
	abstract public class esPettyCashItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PettyCashItemMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, PettyCashItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, PettyCashItemMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, PettyCashItemMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
		
		public esQueryItem Debit
		{
			get
			{
				return new esQueryItem(this, PettyCashItemMetadata.ColumnNames.Debit, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Credit
		{
			get
			{
				return new esQueryItem(this, PettyCashItemMetadata.ColumnNames.Credit, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PettyCashItemCollection")]
	public partial class PettyCashItemCollection : esPettyCashItemCollection, IEnumerable<PettyCashItem>
	{
		public PettyCashItemCollection()
		{

		}
		
		public static implicit operator List<PettyCashItem>(PettyCashItemCollection coll)
		{
			List<PettyCashItem> list = new List<PettyCashItem>();
			
			foreach (PettyCashItem emp in coll)
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
				return  PettyCashItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PettyCashItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PettyCashItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PettyCashItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PettyCashItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PettyCashItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PettyCashItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PettyCashItem AddNew()
		{
			PettyCashItem entity = base.AddNewEntity() as PettyCashItem;
			
			return entity;
		}

		public PettyCashItem FindByPrimaryKey(System.String transactionNo, System.String sequenceNo)
		{
			return base.FindByPrimaryKey(transactionNo, sequenceNo) as PettyCashItem;
		}


		#region IEnumerable<PettyCashItem> Members

		IEnumerator<PettyCashItem> IEnumerable<PettyCashItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PettyCashItem;
			}
		}

		#endregion
		
		private PettyCashItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PettyCashItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PettyCashItem ({TransactionNo},{SequenceNo})")]
	[Serializable]
	public partial class PettyCashItem : esPettyCashItem
	{
		public PettyCashItem()
		{

		}
	
		public PettyCashItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PettyCashItemMetadata.Meta();
			}
		}
		
		
		
		override protected esPettyCashItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PettyCashItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PettyCashItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PettyCashItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PettyCashItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PettyCashItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PettyCashItemQuery : esPettyCashItemQuery
	{
		public PettyCashItemQuery()
		{

		}		
		
		public PettyCashItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PettyCashItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class PettyCashItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PettyCashItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PettyCashItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PettyCashItemMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PettyCashItemMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PettyCashItemMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(PettyCashItemMetadata.ColumnNames.Description, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PettyCashItemMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 250;
			_columns.Add(c);
				
			c = new esColumnMetadata(PettyCashItemMetadata.ColumnNames.Debit, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PettyCashItemMetadata.PropertyNames.Debit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(PettyCashItemMetadata.ColumnNames.Credit, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PettyCashItemMetadata.PropertyNames.Credit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PettyCashItemMetadata Meta()
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
			 public const string SequenceNo = "SequenceNo";
			 public const string Description = "Description";
			 public const string Debit = "Debit";
			 public const string Credit = "Credit";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransactionNo = "TransactionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string Description = "Description";
			 public const string Debit = "Debit";
			 public const string Credit = "Credit";
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
			lock (typeof(PettyCashItemMetadata))
			{
				if(PettyCashItemMetadata.mapDelegates == null)
				{
					PettyCashItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PettyCashItemMetadata.meta == null)
				{
					PettyCashItemMetadata.meta = new PettyCashItemMetadata();
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
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Debit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Credit", new esTypeMap("numeric", "System.Decimal"));			
				
				
				
				meta.Source = "PettyCashItem";
				meta.Destination = "PettyCashItem";
				
				meta.spInsert = "proc_PettyCashItemInsert";				
				meta.spUpdate = "proc_PettyCashItemUpdate";		
				meta.spDelete = "proc_PettyCashItemDelete";
				meta.spLoadAll = "proc_PettyCashItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_PettyCashItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PettyCashItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
