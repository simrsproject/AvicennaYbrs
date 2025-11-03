/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/29/2020 7:22:38 PM
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
	abstract public class esBkuTransactionBalancesCollection : esEntityCollectionWAuditLog
	{
		public esBkuTransactionBalancesCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "BkuTransactionBalancesCollection";
		}

		#region Query Logic
		protected void InitQuery(esBkuTransactionBalancesQuery query)
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
			this.InitQuery(query as esBkuTransactionBalancesQuery);
		}
		#endregion
		
		virtual public BkuTransactionBalances DetachEntity(BkuTransactionBalances entity)
		{
			return base.DetachEntity(entity) as BkuTransactionBalances;
		}
		
		virtual public BkuTransactionBalances AttachEntity(BkuTransactionBalances entity)
		{
			return base.AttachEntity(entity) as BkuTransactionBalances;
		}
		
		virtual public void Combine(BkuTransactionBalancesCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BkuTransactionBalances this[int index]
		{
			get
			{
				return base[index] as BkuTransactionBalances;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BkuTransactionBalances);
		}
	}



	[Serializable]
	abstract public class esBkuTransactionBalances : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBkuTransactionBalancesQuery GetDynamicQuery()
		{
			return null;
		}

		public esBkuTransactionBalances()
		{

		}

		public esBkuTransactionBalances(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 balanceID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(balanceID);
			else
				return LoadByPrimaryKeyStoredProcedure(balanceID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 balanceID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(balanceID);
			else
				return LoadByPrimaryKeyStoredProcedure(balanceID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 balanceID)
		{
			esBkuTransactionBalancesQuery query = this.GetDynamicQuery();
			query.Where(query.BalanceID == balanceID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 balanceID)
		{
			esParameters parms = new esParameters();
			parms.Add("BalanceID",balanceID);
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
						case "BalanceID": this.str.BalanceID = (string)value; break;							
						case "RekeningID": this.str.RekeningID = (string)value; break;							
						case "Date": this.str.Date = (string)value; break;							
						case "InitialBalance": this.str.InitialBalance = (string)value; break;							
						case "DebitAmount": this.str.DebitAmount = (string)value; break;							
						case "CreditAmount": this.str.CreditAmount = (string)value; break;							
						case "FinalBalance": this.str.FinalBalance = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "BalanceID":
						
							if (value == null || value is System.Int32)
								this.BalanceID = (System.Int32?)value;
							break;
						
						case "RekeningID":
						
							if (value == null || value is System.Int32)
								this.RekeningID = (System.Int32?)value;
							break;
						
						case "Date":
						
							if (value == null || value is System.DateTime)
								this.Date = (System.DateTime?)value;
							break;
						
						case "InitialBalance":
						
							if (value == null || value is System.Decimal)
								this.InitialBalance = (System.Decimal?)value;
							break;
						
						case "DebitAmount":
						
							if (value == null || value is System.Decimal)
								this.DebitAmount = (System.Decimal?)value;
							break;
						
						case "CreditAmount":
						
							if (value == null || value is System.Decimal)
								this.CreditAmount = (System.Decimal?)value;
							break;
						
						case "FinalBalance":
						
							if (value == null || value is System.Decimal)
								this.FinalBalance = (System.Decimal?)value;
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
		/// Maps to BkuTransactionBalances.BalanceID
		/// </summary>
		virtual public System.Int32? BalanceID
		{
			get
			{
				return base.GetSystemInt32(BkuTransactionBalancesMetadata.ColumnNames.BalanceID);
			}
			
			set
			{
				base.SetSystemInt32(BkuTransactionBalancesMetadata.ColumnNames.BalanceID, value);
			}
		}
		
		/// <summary>
		/// Maps to BkuTransactionBalances.RekeningID
		/// </summary>
		virtual public System.Int32? RekeningID
		{
			get
			{
				return base.GetSystemInt32(BkuTransactionBalancesMetadata.ColumnNames.RekeningID);
			}
			
			set
			{
				base.SetSystemInt32(BkuTransactionBalancesMetadata.ColumnNames.RekeningID, value);
			}
		}
		
		/// <summary>
		/// Maps to BkuTransactionBalances.Date
		/// </summary>
		virtual public System.DateTime? Date
		{
			get
			{
				return base.GetSystemDateTime(BkuTransactionBalancesMetadata.ColumnNames.Date);
			}
			
			set
			{
				base.SetSystemDateTime(BkuTransactionBalancesMetadata.ColumnNames.Date, value);
			}
		}
		
		/// <summary>
		/// Maps to BkuTransactionBalances.InitialBalance
		/// </summary>
		virtual public System.Decimal? InitialBalance
		{
			get
			{
				return base.GetSystemDecimal(BkuTransactionBalancesMetadata.ColumnNames.InitialBalance);
			}
			
			set
			{
				base.SetSystemDecimal(BkuTransactionBalancesMetadata.ColumnNames.InitialBalance, value);
			}
		}
		
		/// <summary>
		/// Maps to BkuTransactionBalances.DebitAmount
		/// </summary>
		virtual public System.Decimal? DebitAmount
		{
			get
			{
				return base.GetSystemDecimal(BkuTransactionBalancesMetadata.ColumnNames.DebitAmount);
			}
			
			set
			{
				base.SetSystemDecimal(BkuTransactionBalancesMetadata.ColumnNames.DebitAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to BkuTransactionBalances.CreditAmount
		/// </summary>
		virtual public System.Decimal? CreditAmount
		{
			get
			{
				return base.GetSystemDecimal(BkuTransactionBalancesMetadata.ColumnNames.CreditAmount);
			}
			
			set
			{
				base.SetSystemDecimal(BkuTransactionBalancesMetadata.ColumnNames.CreditAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to BkuTransactionBalances.FinalBalance
		/// </summary>
		virtual public System.Decimal? FinalBalance
		{
			get
			{
				return base.GetSystemDecimal(BkuTransactionBalancesMetadata.ColumnNames.FinalBalance);
			}
			
			set
			{
				base.SetSystemDecimal(BkuTransactionBalancesMetadata.ColumnNames.FinalBalance, value);
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
			public esStrings(esBkuTransactionBalances entity)
			{
				this.entity = entity;
			}
			
	
			public System.String BalanceID
			{
				get
				{
					System.Int32? data = entity.BalanceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BalanceID = null;
					else entity.BalanceID = Convert.ToInt32(value);
				}
			}
				
			public System.String RekeningID
			{
				get
				{
					System.Int32? data = entity.RekeningID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RekeningID = null;
					else entity.RekeningID = Convert.ToInt32(value);
				}
			}
				
			public System.String Date
			{
				get
				{
					System.DateTime? data = entity.Date;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Date = null;
					else entity.Date = Convert.ToDateTime(value);
				}
			}
				
			public System.String InitialBalance
			{
				get
				{
					System.Decimal? data = entity.InitialBalance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InitialBalance = null;
					else entity.InitialBalance = Convert.ToDecimal(value);
				}
			}
				
			public System.String DebitAmount
			{
				get
				{
					System.Decimal? data = entity.DebitAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DebitAmount = null;
					else entity.DebitAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String CreditAmount
			{
				get
				{
					System.Decimal? data = entity.CreditAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreditAmount = null;
					else entity.CreditAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String FinalBalance
			{
				get
				{
					System.Decimal? data = entity.FinalBalance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FinalBalance = null;
					else entity.FinalBalance = Convert.ToDecimal(value);
				}
			}
			

			private esBkuTransactionBalances entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBkuTransactionBalancesQuery query)
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
				throw new Exception("esBkuTransactionBalances can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class BkuTransactionBalances : esBkuTransactionBalances
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
	abstract public class esBkuTransactionBalancesQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return BkuTransactionBalancesMetadata.Meta();
			}
		}	
		

		public esQueryItem BalanceID
		{
			get
			{
				return new esQueryItem(this, BkuTransactionBalancesMetadata.ColumnNames.BalanceID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RekeningID
		{
			get
			{
				return new esQueryItem(this, BkuTransactionBalancesMetadata.ColumnNames.RekeningID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Date
		{
			get
			{
				return new esQueryItem(this, BkuTransactionBalancesMetadata.ColumnNames.Date, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem InitialBalance
		{
			get
			{
				return new esQueryItem(this, BkuTransactionBalancesMetadata.ColumnNames.InitialBalance, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DebitAmount
		{
			get
			{
				return new esQueryItem(this, BkuTransactionBalancesMetadata.ColumnNames.DebitAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CreditAmount
		{
			get
			{
				return new esQueryItem(this, BkuTransactionBalancesMetadata.ColumnNames.CreditAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem FinalBalance
		{
			get
			{
				return new esQueryItem(this, BkuTransactionBalancesMetadata.ColumnNames.FinalBalance, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BkuTransactionBalancesCollection")]
	public partial class BkuTransactionBalancesCollection : esBkuTransactionBalancesCollection, IEnumerable<BkuTransactionBalances>
	{
		public BkuTransactionBalancesCollection()
		{

		}
		
		public static implicit operator List<BkuTransactionBalances>(BkuTransactionBalancesCollection coll)
		{
			List<BkuTransactionBalances> list = new List<BkuTransactionBalances>();
			
			foreach (BkuTransactionBalances emp in coll)
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
				return  BkuTransactionBalancesMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BkuTransactionBalancesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BkuTransactionBalances(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BkuTransactionBalances();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public BkuTransactionBalancesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BkuTransactionBalancesQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(BkuTransactionBalancesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public BkuTransactionBalances AddNew()
		{
			BkuTransactionBalances entity = base.AddNewEntity() as BkuTransactionBalances;
			
			return entity;
		}

		public BkuTransactionBalances FindByPrimaryKey(System.Int32 balanceID)
		{
			return base.FindByPrimaryKey(balanceID) as BkuTransactionBalances;
		}


		#region IEnumerable<BkuTransactionBalances> Members

		IEnumerator<BkuTransactionBalances> IEnumerable<BkuTransactionBalances>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BkuTransactionBalances;
			}
		}

		#endregion
		
		private BkuTransactionBalancesQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BkuTransactionBalances' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("BkuTransactionBalances ({BalanceID})")]
	[Serializable]
	public partial class BkuTransactionBalances : esBkuTransactionBalances
	{
		public BkuTransactionBalances()
		{

		}
	
		public BkuTransactionBalances(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BkuTransactionBalancesMetadata.Meta();
			}
		}
		
		
		
		override protected esBkuTransactionBalancesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BkuTransactionBalancesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public BkuTransactionBalancesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BkuTransactionBalancesQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(BkuTransactionBalancesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private BkuTransactionBalancesQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class BkuTransactionBalancesQuery : esBkuTransactionBalancesQuery
	{
		public BkuTransactionBalancesQuery()
		{

		}		
		
		public BkuTransactionBalancesQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "BkuTransactionBalancesQuery";
        }
		
			
	}


	[Serializable]
	public partial class BkuTransactionBalancesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BkuTransactionBalancesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BkuTransactionBalancesMetadata.ColumnNames.BalanceID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BkuTransactionBalancesMetadata.PropertyNames.BalanceID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(BkuTransactionBalancesMetadata.ColumnNames.RekeningID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BkuTransactionBalancesMetadata.PropertyNames.RekeningID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(BkuTransactionBalancesMetadata.ColumnNames.Date, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BkuTransactionBalancesMetadata.PropertyNames.Date;
			_columns.Add(c);
				
			c = new esColumnMetadata(BkuTransactionBalancesMetadata.ColumnNames.InitialBalance, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BkuTransactionBalancesMetadata.PropertyNames.InitialBalance;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(BkuTransactionBalancesMetadata.ColumnNames.DebitAmount, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BkuTransactionBalancesMetadata.PropertyNames.DebitAmount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(BkuTransactionBalancesMetadata.ColumnNames.CreditAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BkuTransactionBalancesMetadata.PropertyNames.CreditAmount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(BkuTransactionBalancesMetadata.ColumnNames.FinalBalance, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BkuTransactionBalancesMetadata.PropertyNames.FinalBalance;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public BkuTransactionBalancesMetadata Meta()
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
			 public const string BalanceID = "BalanceID";
			 public const string RekeningID = "RekeningID";
			 public const string Date = "Date";
			 public const string InitialBalance = "InitialBalance";
			 public const string DebitAmount = "DebitAmount";
			 public const string CreditAmount = "CreditAmount";
			 public const string FinalBalance = "FinalBalance";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string BalanceID = "BalanceID";
			 public const string RekeningID = "RekeningID";
			 public const string Date = "Date";
			 public const string InitialBalance = "InitialBalance";
			 public const string DebitAmount = "DebitAmount";
			 public const string CreditAmount = "CreditAmount";
			 public const string FinalBalance = "FinalBalance";
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
			lock (typeof(BkuTransactionBalancesMetadata))
			{
				if(BkuTransactionBalancesMetadata.mapDelegates == null)
				{
					BkuTransactionBalancesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BkuTransactionBalancesMetadata.meta == null)
				{
					BkuTransactionBalancesMetadata.meta = new BkuTransactionBalancesMetadata();
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
				

				meta.AddTypeMap("BalanceID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RekeningID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Date", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("InitialBalance", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("DebitAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("CreditAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("FinalBalance", new esTypeMap("money", "System.Decimal"));			
				
				
				
				meta.Source = "BkuTransactionBalances";
				meta.Destination = "BkuTransactionBalances";
				
				meta.spInsert = "proc_BkuTransactionBalancesInsert";				
				meta.spUpdate = "proc_BkuTransactionBalancesUpdate";		
				meta.spDelete = "proc_BkuTransactionBalancesDelete";
				meta.spLoadAll = "proc_BkuTransactionBalancesLoadAll";
				meta.spLoadByPrimaryKey = "proc_BkuTransactionBalancesLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BkuTransactionBalancesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
