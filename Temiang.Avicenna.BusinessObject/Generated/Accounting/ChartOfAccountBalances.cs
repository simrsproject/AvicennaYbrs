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
	abstract public class esChartOfAccountBalancesCollection : esEntityCollectionWAuditLog
	{
		public esChartOfAccountBalancesCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ChartOfAccountBalancesCollection";
		}

		#region Query Logic
		protected void InitQuery(esChartOfAccountBalancesQuery query)
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
			this.InitQuery(query as esChartOfAccountBalancesQuery);
		}
		#endregion
		
		virtual public ChartOfAccountBalances DetachEntity(ChartOfAccountBalances entity)
		{
			return base.DetachEntity(entity) as ChartOfAccountBalances;
		}
		
		virtual public ChartOfAccountBalances AttachEntity(ChartOfAccountBalances entity)
		{
			return base.AttachEntity(entity) as ChartOfAccountBalances;
		}
		
		virtual public void Combine(ChartOfAccountBalancesCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ChartOfAccountBalances this[int index]
		{
			get
			{
				return base[index] as ChartOfAccountBalances;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ChartOfAccountBalances);
		}
	}



	[Serializable]
	abstract public class esChartOfAccountBalances : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esChartOfAccountBalancesQuery GetDynamicQuery()
		{
			return null;
		}

		public esChartOfAccountBalances()
		{

		}

		public esChartOfAccountBalances(DataRow row)
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
			esChartOfAccountBalancesQuery query = this.GetDynamicQuery();
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
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;							
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
						
						case "ChartOfAccountId":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
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
		/// Maps to ChartOfAccountBalances.BalanceId
		/// </summary>
		virtual public System.Int32? BalanceId
		{
			get
			{
				return base.GetSystemInt32(ChartOfAccountBalancesMetadata.ColumnNames.BalanceId);
			}
			
			set
			{
				base.SetSystemInt32(ChartOfAccountBalancesMetadata.ColumnNames.BalanceId, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccountBalances.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(ChartOfAccountBalancesMetadata.ColumnNames.ChartOfAccountId);
			}
			
			set
			{
				if(base.SetSystemInt32(ChartOfAccountBalancesMetadata.ColumnNames.ChartOfAccountId, value))
				{
					this._UpToChartOfAccountsByChartOfAccountId = null;
				}
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccountBalances.Month
		/// </summary>
		virtual public System.String Month
		{
			get
			{
				return base.GetSystemString(ChartOfAccountBalancesMetadata.ColumnNames.Month);
			}
			
			set
			{
				base.SetSystemString(ChartOfAccountBalancesMetadata.ColumnNames.Month, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccountBalances.Year
		/// </summary>
		virtual public System.String Year
		{
			get
			{
				return base.GetSystemString(ChartOfAccountBalancesMetadata.ColumnNames.Year);
			}
			
			set
			{
				base.SetSystemString(ChartOfAccountBalancesMetadata.ColumnNames.Year, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccountBalances.InitialBalance
		/// </summary>
		virtual public System.Decimal? InitialBalance
		{
			get
			{
				return base.GetSystemDecimal(ChartOfAccountBalancesMetadata.ColumnNames.InitialBalance);
			}
			
			set
			{
				base.SetSystemDecimal(ChartOfAccountBalancesMetadata.ColumnNames.InitialBalance, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccountBalances.DebitAmount
		/// </summary>
		virtual public System.Decimal? DebitAmount
		{
			get
			{
				return base.GetSystemDecimal(ChartOfAccountBalancesMetadata.ColumnNames.DebitAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ChartOfAccountBalancesMetadata.ColumnNames.DebitAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccountBalances.CreditAmount
		/// </summary>
		virtual public System.Decimal? CreditAmount
		{
			get
			{
				return base.GetSystemDecimal(ChartOfAccountBalancesMetadata.ColumnNames.CreditAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ChartOfAccountBalancesMetadata.ColumnNames.CreditAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccountBalances.FinalBalance
		/// </summary>
		virtual public System.Decimal? FinalBalance
		{
			get
			{
				return base.GetSystemDecimal(ChartOfAccountBalancesMetadata.ColumnNames.FinalBalance);
			}
			
			set
			{
				base.SetSystemDecimal(ChartOfAccountBalancesMetadata.ColumnNames.FinalBalance, value);
			}
		}
		
		[CLSCompliant(false)]
		internal protected ChartOfAccounts _UpToChartOfAccountsByChartOfAccountId;
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
			public esStrings(esChartOfAccountBalances entity)
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
				
			public System.String ChartOfAccountId
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountId = null;
					else entity.ChartOfAccountId = Convert.ToInt32(value);
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
			

			private esChartOfAccountBalances entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esChartOfAccountBalancesQuery query)
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
				throw new Exception("esChartOfAccountBalances can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ChartOfAccountBalances : esChartOfAccountBalances
	{

				
		#region UpToChartOfAccountsByChartOfAccountId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_ChartOfAccountBalances_ChartOfAccounts
		/// </summary>

		[XmlIgnore]
		public ChartOfAccounts UpToChartOfAccountsByChartOfAccountId
		{
			get
			{
				if(this._UpToChartOfAccountsByChartOfAccountId == null
					&& ChartOfAccountId != null					)
				{
					this._UpToChartOfAccountsByChartOfAccountId = new ChartOfAccounts();
					this._UpToChartOfAccountsByChartOfAccountId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToChartOfAccountsByChartOfAccountId", this._UpToChartOfAccountsByChartOfAccountId);
					this._UpToChartOfAccountsByChartOfAccountId.Query.Where(this._UpToChartOfAccountsByChartOfAccountId.Query.ChartOfAccountId == this.ChartOfAccountId);
					this._UpToChartOfAccountsByChartOfAccountId.Query.Load();
				}

				return this._UpToChartOfAccountsByChartOfAccountId;
			}
			
			set
			{
				this.RemovePreSave("UpToChartOfAccountsByChartOfAccountId");
				

				if(value == null)
				{
					this.ChartOfAccountId = null;
					this._UpToChartOfAccountsByChartOfAccountId = null;
				}
				else
				{
					this.ChartOfAccountId = value.ChartOfAccountId;
					this._UpToChartOfAccountsByChartOfAccountId = value;
					this.SetPreSave("UpToChartOfAccountsByChartOfAccountId", this._UpToChartOfAccountsByChartOfAccountId);
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
			if(!this.es.IsDeleted && this._UpToChartOfAccountsByChartOfAccountId != null)
			{
				this.ChartOfAccountId = this._UpToChartOfAccountsByChartOfAccountId.ChartOfAccountId;
			}
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
	abstract public class esChartOfAccountBalancesQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ChartOfAccountBalancesMetadata.Meta();
			}
		}	
		

		public esQueryItem BalanceId
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountBalancesMetadata.ColumnNames.BalanceId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountBalancesMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Month
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountBalancesMetadata.ColumnNames.Month, esSystemType.String);
			}
		} 
		
		public esQueryItem Year
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountBalancesMetadata.ColumnNames.Year, esSystemType.String);
			}
		} 
		
		public esQueryItem InitialBalance
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountBalancesMetadata.ColumnNames.InitialBalance, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DebitAmount
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountBalancesMetadata.ColumnNames.DebitAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CreditAmount
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountBalancesMetadata.ColumnNames.CreditAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem FinalBalance
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountBalancesMetadata.ColumnNames.FinalBalance, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ChartOfAccountBalancesCollection")]
	public partial class ChartOfAccountBalancesCollection : esChartOfAccountBalancesCollection, IEnumerable<ChartOfAccountBalances>
	{
		public ChartOfAccountBalancesCollection()
		{

		}
		
		public static implicit operator List<ChartOfAccountBalances>(ChartOfAccountBalancesCollection coll)
		{
			List<ChartOfAccountBalances> list = new List<ChartOfAccountBalances>();
			
			foreach (ChartOfAccountBalances emp in coll)
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
				return  ChartOfAccountBalancesMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ChartOfAccountBalancesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ChartOfAccountBalances(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ChartOfAccountBalances();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ChartOfAccountBalancesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ChartOfAccountBalancesQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ChartOfAccountBalancesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ChartOfAccountBalances AddNew()
		{
			ChartOfAccountBalances entity = base.AddNewEntity() as ChartOfAccountBalances;
			
			return entity;
		}

		public ChartOfAccountBalances FindByPrimaryKey(System.Int32 balanceId)
		{
			return base.FindByPrimaryKey(balanceId) as ChartOfAccountBalances;
		}


		#region IEnumerable<ChartOfAccountBalances> Members

		IEnumerator<ChartOfAccountBalances> IEnumerable<ChartOfAccountBalances>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ChartOfAccountBalances;
			}
		}

		#endregion
		
		private ChartOfAccountBalancesQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ChartOfAccountBalances' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ChartOfAccountBalances ({BalanceId})")]
	[Serializable]
	public partial class ChartOfAccountBalances : esChartOfAccountBalances
	{
		public ChartOfAccountBalances()
		{

		}
	
		public ChartOfAccountBalances(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ChartOfAccountBalancesMetadata.Meta();
			}
		}
		
		
		
		override protected esChartOfAccountBalancesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ChartOfAccountBalancesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ChartOfAccountBalancesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ChartOfAccountBalancesQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ChartOfAccountBalancesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ChartOfAccountBalancesQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ChartOfAccountBalancesQuery : esChartOfAccountBalancesQuery
	{
		public ChartOfAccountBalancesQuery()
		{

		}		
		
		public ChartOfAccountBalancesQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ChartOfAccountBalancesQuery";
        }
		
			
	}


	[Serializable]
	public partial class ChartOfAccountBalancesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ChartOfAccountBalancesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ChartOfAccountBalancesMetadata.ColumnNames.BalanceId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ChartOfAccountBalancesMetadata.PropertyNames.BalanceId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountBalancesMetadata.ColumnNames.ChartOfAccountId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ChartOfAccountBalancesMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountBalancesMetadata.ColumnNames.Month, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ChartOfAccountBalancesMetadata.PropertyNames.Month;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountBalancesMetadata.ColumnNames.Year, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ChartOfAccountBalancesMetadata.PropertyNames.Year;
			c.CharacterMaxLength = 4;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountBalancesMetadata.ColumnNames.InitialBalance, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ChartOfAccountBalancesMetadata.PropertyNames.InitialBalance;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountBalancesMetadata.ColumnNames.DebitAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ChartOfAccountBalancesMetadata.PropertyNames.DebitAmount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountBalancesMetadata.ColumnNames.CreditAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ChartOfAccountBalancesMetadata.PropertyNames.CreditAmount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountBalancesMetadata.ColumnNames.FinalBalance, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ChartOfAccountBalancesMetadata.PropertyNames.FinalBalance;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ChartOfAccountBalancesMetadata Meta()
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
			 public const string ChartOfAccountId = "ChartOfAccountId";
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
			 public const string ChartOfAccountId = "ChartOfAccountId";
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
			lock (typeof(ChartOfAccountBalancesMetadata))
			{
				if(ChartOfAccountBalancesMetadata.mapDelegates == null)
				{
					ChartOfAccountBalancesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ChartOfAccountBalancesMetadata.meta == null)
				{
					ChartOfAccountBalancesMetadata.meta = new ChartOfAccountBalancesMetadata();
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
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Month", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Year", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("InitialBalance", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("DebitAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("CreditAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("FinalBalance", new esTypeMap("money", "System.Decimal"));			
				
				
				
				meta.Source = "ChartOfAccountBalances";
				meta.Destination = "ChartOfAccountBalances";
				
				meta.spInsert = "proc_ChartOfAccountBalancesInsert";				
				meta.spUpdate = "proc_ChartOfAccountBalancesUpdate";		
				meta.spDelete = "proc_ChartOfAccountBalancesDelete";
				meta.spLoadAll = "proc_ChartOfAccountBalancesLoadAll";
				meta.spLoadByPrimaryKey = "proc_ChartOfAccountBalancesLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ChartOfAccountBalancesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
