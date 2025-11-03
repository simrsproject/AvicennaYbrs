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
	abstract public class esBankAccountBalanceCollection : esEntityCollectionWAuditLog
	{
		public esBankAccountBalanceCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "BankAccountBalanceCollection";
		}

		#region Query Logic
		protected void InitQuery(esBankAccountBalanceQuery query)
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
			this.InitQuery(query as esBankAccountBalanceQuery);
		}
		#endregion
		
		virtual public BankAccountBalance DetachEntity(BankAccountBalance entity)
		{
			return base.DetachEntity(entity) as BankAccountBalance;
		}
		
		virtual public BankAccountBalance AttachEntity(BankAccountBalance entity)
		{
			return base.AttachEntity(entity) as BankAccountBalance;
		}
		
		virtual public void Combine(BankAccountBalanceCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BankAccountBalance this[int index]
		{
			get
			{
				return base[index] as BankAccountBalance;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BankAccountBalance);
		}
	}



	[Serializable]
	abstract public class esBankAccountBalance : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBankAccountBalanceQuery GetDynamicQuery()
		{
			return null;
		}

		public esBankAccountBalance()
		{

		}

		public esBankAccountBalance(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 balanceId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(balanceId);
			else
				return LoadByPrimaryKeyStoredProcedure(balanceId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 balanceId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(balanceId);
			else
				return LoadByPrimaryKeyStoredProcedure(balanceId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 balanceId)
		{
			esBankAccountBalanceQuery query = this.GetDynamicQuery();
			query.Where(query.BalanceId == balanceId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 balanceId)
		{
			esParameters parms = new esParameters();
			parms.Add("BalanceId",balanceId);
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
						case "BalanceId": this.str.BalanceId = (string)value; break;							
						case "BankId": this.str.BankId = (string)value; break;							
						case "Month": this.str.Month = (string)value; break;							
						case "Year": this.str.Year = (string)value; break;							
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
						case "BalanceId":
						
							if (value == null || value is System.Int32)
								this.BalanceId = (System.Int32?)value;
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
		/// Maps to BankAccountBalance.BalanceId
		/// </summary>
		virtual public System.Int32? BalanceId
		{
			get
			{
				return base.GetSystemInt32(BankAccountBalanceMetadata.ColumnNames.BalanceId);
			}
			
			set
			{
				base.SetSystemInt32(BankAccountBalanceMetadata.ColumnNames.BalanceId, value);
			}
		}
		
		/// <summary>
		/// Maps to BankAccountBalance.BankId
		/// </summary>
		virtual public System.String BankId
		{
			get
			{
				return base.GetSystemString(BankAccountBalanceMetadata.ColumnNames.BankId);
			}
			
			set
			{
				if(base.SetSystemString(BankAccountBalanceMetadata.ColumnNames.BankId, value))
				{
					this._UpToBankByBankId = null;
				}
			}
		}
		
		/// <summary>
		/// Maps to BankAccountBalance.Month
		/// </summary>
		virtual public System.String Month
		{
			get
			{
				return base.GetSystemString(BankAccountBalanceMetadata.ColumnNames.Month);
			}
			
			set
			{
				base.SetSystemString(BankAccountBalanceMetadata.ColumnNames.Month, value);
			}
		}
		
		/// <summary>
		/// Maps to BankAccountBalance.Year
		/// </summary>
		virtual public System.String Year
		{
			get
			{
				return base.GetSystemString(BankAccountBalanceMetadata.ColumnNames.Year);
			}
			
			set
			{
				base.SetSystemString(BankAccountBalanceMetadata.ColumnNames.Year, value);
			}
		}
		
		/// <summary>
		/// Maps to BankAccountBalance.InitialBalance
		/// </summary>
		virtual public System.Decimal? InitialBalance
		{
			get
			{
				return base.GetSystemDecimal(BankAccountBalanceMetadata.ColumnNames.InitialBalance);
			}
			
			set
			{
				base.SetSystemDecimal(BankAccountBalanceMetadata.ColumnNames.InitialBalance, value);
			}
		}
		
		/// <summary>
		/// Maps to BankAccountBalance.DebitAmount
		/// </summary>
		virtual public System.Decimal? DebitAmount
		{
			get
			{
				return base.GetSystemDecimal(BankAccountBalanceMetadata.ColumnNames.DebitAmount);
			}
			
			set
			{
				base.SetSystemDecimal(BankAccountBalanceMetadata.ColumnNames.DebitAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to BankAccountBalance.CreditAmount
		/// </summary>
		virtual public System.Decimal? CreditAmount
		{
			get
			{
				return base.GetSystemDecimal(BankAccountBalanceMetadata.ColumnNames.CreditAmount);
			}
			
			set
			{
				base.SetSystemDecimal(BankAccountBalanceMetadata.ColumnNames.CreditAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to BankAccountBalance.FinalBalance
		/// </summary>
		virtual public System.Decimal? FinalBalance
		{
			get
			{
				return base.GetSystemDecimal(BankAccountBalanceMetadata.ColumnNames.FinalBalance);
			}
			
			set
			{
				base.SetSystemDecimal(BankAccountBalanceMetadata.ColumnNames.FinalBalance, value);
			}
		}
		
		[CLSCompliant(false)]
		internal protected Bank _UpToBankByBankId;
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
			public esStrings(esBankAccountBalance entity)
			{
				this.entity = entity;
			}
			
	
			public System.String BalanceId
			{
				get
				{
					System.Int32? data = entity.BalanceId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BalanceId = null;
					else entity.BalanceId = Convert.ToInt32(value);
				}
			}
				
			public System.String BankId
			{
				get
				{
					System.String data = entity.BankId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankId = null;
					else entity.BankId = Convert.ToString(value);
				}
			}
				
			public System.String Month
			{
				get
				{
					System.String data = entity.Month;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Month = null;
					else entity.Month = Convert.ToString(value);
				}
			}
				
			public System.String Year
			{
				get
				{
					System.String data = entity.Year;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Year = null;
					else entity.Year = Convert.ToString(value);
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
			

			private esBankAccountBalance entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBankAccountBalanceQuery query)
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
				throw new Exception("esBankAccountBalance can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class BankAccountBalance : esBankAccountBalance
	{

				
		#region UpToBankByBankId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_BankAccountBalance_BankAccount
		/// </summary>

		[XmlIgnore]
		public Bank UpToBankByBankId
		{
			get
			{
				if(this._UpToBankByBankId == null
					&& BankId != null					)
				{
					this._UpToBankByBankId = new Bank();
					this._UpToBankByBankId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToBankByBankId", this._UpToBankByBankId);
					this._UpToBankByBankId.Query.Where(this._UpToBankByBankId.Query.BankID == this.BankId);
					this._UpToBankByBankId.Query.Load();
				}

				return this._UpToBankByBankId;
			}
			
			set
			{
				this.RemovePreSave("UpToBankByBankId");
				

				if(value == null)
				{
					this.BankId = null;
					this._UpToBankByBankId = null;
				}
				else
				{
					this.BankId = value.BankID;
					this._UpToBankByBankId = value;
					this.SetPreSave("UpToBankByBankId", this._UpToBankByBankId);
				}
				
			}
		}
		#endregion
		

		
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
	abstract public class esBankAccountBalanceQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return BankAccountBalanceMetadata.Meta();
			}
		}	
		

		public esQueryItem BalanceId
		{
			get
			{
				return new esQueryItem(this, BankAccountBalanceMetadata.ColumnNames.BalanceId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem BankId
		{
			get
			{
				return new esQueryItem(this, BankAccountBalanceMetadata.ColumnNames.BankId, esSystemType.String);
			}
		} 
		
		public esQueryItem Month
		{
			get
			{
				return new esQueryItem(this, BankAccountBalanceMetadata.ColumnNames.Month, esSystemType.String);
			}
		} 
		
		public esQueryItem Year
		{
			get
			{
				return new esQueryItem(this, BankAccountBalanceMetadata.ColumnNames.Year, esSystemType.String);
			}
		} 
		
		public esQueryItem InitialBalance
		{
			get
			{
				return new esQueryItem(this, BankAccountBalanceMetadata.ColumnNames.InitialBalance, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DebitAmount
		{
			get
			{
				return new esQueryItem(this, BankAccountBalanceMetadata.ColumnNames.DebitAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CreditAmount
		{
			get
			{
				return new esQueryItem(this, BankAccountBalanceMetadata.ColumnNames.CreditAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem FinalBalance
		{
			get
			{
				return new esQueryItem(this, BankAccountBalanceMetadata.ColumnNames.FinalBalance, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BankAccountBalanceCollection")]
	public partial class BankAccountBalanceCollection : esBankAccountBalanceCollection, IEnumerable<BankAccountBalance>
	{
		public BankAccountBalanceCollection()
		{

		}
		
		public static implicit operator List<BankAccountBalance>(BankAccountBalanceCollection coll)
		{
			List<BankAccountBalance> list = new List<BankAccountBalance>();
			
			foreach (BankAccountBalance emp in coll)
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
				return  BankAccountBalanceMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BankAccountBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BankAccountBalance(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BankAccountBalance();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public BankAccountBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BankAccountBalanceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(BankAccountBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public BankAccountBalance AddNew()
		{
			BankAccountBalance entity = base.AddNewEntity() as BankAccountBalance;
			
			return entity;
		}

		public BankAccountBalance FindByPrimaryKey(System.Int32 balanceId)
		{
			return base.FindByPrimaryKey(balanceId) as BankAccountBalance;
		}


		#region IEnumerable<BankAccountBalance> Members

		IEnumerator<BankAccountBalance> IEnumerable<BankAccountBalance>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BankAccountBalance;
			}
		}

		#endregion
		
		private BankAccountBalanceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BankAccountBalance' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("BankAccountBalance ({BalanceId})")]
	[Serializable]
	public partial class BankAccountBalance : esBankAccountBalance
	{
		public BankAccountBalance()
		{

		}
	
		public BankAccountBalance(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BankAccountBalanceMetadata.Meta();
			}
		}
		
		
		
		override protected esBankAccountBalanceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BankAccountBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public BankAccountBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BankAccountBalanceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(BankAccountBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private BankAccountBalanceQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class BankAccountBalanceQuery : esBankAccountBalanceQuery
	{
		public BankAccountBalanceQuery()
		{

		}		
		
		public BankAccountBalanceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "BankAccountBalanceQuery";
        }
		
			
	}


	[Serializable]
	public partial class BankAccountBalanceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BankAccountBalanceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BankAccountBalanceMetadata.ColumnNames.BalanceId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BankAccountBalanceMetadata.PropertyNames.BalanceId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(BankAccountBalanceMetadata.ColumnNames.BankId, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = BankAccountBalanceMetadata.PropertyNames.BankId;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(BankAccountBalanceMetadata.ColumnNames.Month, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = BankAccountBalanceMetadata.PropertyNames.Month;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(BankAccountBalanceMetadata.ColumnNames.Year, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BankAccountBalanceMetadata.PropertyNames.Year;
			c.CharacterMaxLength = 4;
			_columns.Add(c);
				
			c = new esColumnMetadata(BankAccountBalanceMetadata.ColumnNames.InitialBalance, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BankAccountBalanceMetadata.PropertyNames.InitialBalance;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(BankAccountBalanceMetadata.ColumnNames.DebitAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BankAccountBalanceMetadata.PropertyNames.DebitAmount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(BankAccountBalanceMetadata.ColumnNames.CreditAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BankAccountBalanceMetadata.PropertyNames.CreditAmount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(BankAccountBalanceMetadata.ColumnNames.FinalBalance, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BankAccountBalanceMetadata.PropertyNames.FinalBalance;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public BankAccountBalanceMetadata Meta()
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
			 public const string BalanceId = "BalanceId";
			 public const string BankId = "BankId";
			 public const string Month = "Month";
			 public const string Year = "Year";
			 public const string InitialBalance = "InitialBalance";
			 public const string DebitAmount = "DebitAmount";
			 public const string CreditAmount = "CreditAmount";
			 public const string FinalBalance = "FinalBalance";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string BalanceId = "BalanceId";
			 public const string BankId = "BankId";
			 public const string Month = "Month";
			 public const string Year = "Year";
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
			lock (typeof(BankAccountBalanceMetadata))
			{
				if(BankAccountBalanceMetadata.mapDelegates == null)
				{
					BankAccountBalanceMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BankAccountBalanceMetadata.meta == null)
				{
					BankAccountBalanceMetadata.meta = new BankAccountBalanceMetadata();
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
				

				meta.AddTypeMap("BalanceId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("BankId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Month", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Year", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("InitialBalance", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("DebitAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("CreditAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("FinalBalance", new esTypeMap("money", "System.Decimal"));			
				
				
				
				meta.Source = "BankAccountBalance";
				meta.Destination = "BankAccountBalance";
				
				meta.spInsert = "proc_BankAccountBalanceInsert";				
				meta.spUpdate = "proc_BankAccountBalanceUpdate";		
				meta.spDelete = "proc_BankAccountBalanceDelete";
				meta.spLoadAll = "proc_BankAccountBalanceLoadAll";
				meta.spLoadByPrimaryKey = "proc_BankAccountBalanceLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BankAccountBalanceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
