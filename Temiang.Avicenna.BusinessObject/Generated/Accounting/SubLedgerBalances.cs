/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:26 PM
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
	abstract public class esSubLedgerBalancesCollection : esEntityCollectionWAuditLog
	{
		public esSubLedgerBalancesCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "SubLedgerBalancesCollection";
		}

		#region Query Logic
		protected void InitQuery(esSubLedgerBalancesQuery query)
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
			this.InitQuery(query as esSubLedgerBalancesQuery);
		}
		#endregion
		
		virtual public SubLedgerBalances DetachEntity(SubLedgerBalances entity)
		{
			return base.DetachEntity(entity) as SubLedgerBalances;
		}
		
		virtual public SubLedgerBalances AttachEntity(SubLedgerBalances entity)
		{
			return base.AttachEntity(entity) as SubLedgerBalances;
		}
		
		virtual public void Combine(SubLedgerBalancesCollection collection)
		{
			base.Combine(collection);
		}
		
		new public SubLedgerBalances this[int index]
		{
			get
			{
				return base[index] as SubLedgerBalances;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SubLedgerBalances);
		}
	}



	[Serializable]
	abstract public class esSubLedgerBalances : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSubLedgerBalancesQuery GetDynamicQuery()
		{
			return null;
		}

		public esSubLedgerBalances()
		{

		}

		public esSubLedgerBalances(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 subLedgerBalanceId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(subLedgerBalanceId);
			else
				return LoadByPrimaryKeyStoredProcedure(subLedgerBalanceId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 subLedgerBalanceId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(subLedgerBalanceId);
			else
				return LoadByPrimaryKeyStoredProcedure(subLedgerBalanceId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 subLedgerBalanceId)
		{
			esSubLedgerBalancesQuery query = this.GetDynamicQuery();
			query.Where(query.SubLedgerBalanceId == subLedgerBalanceId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 subLedgerBalanceId)
		{
			esParameters parms = new esParameters();
			parms.Add("SubLedgerBalanceId",subLedgerBalanceId);
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
						case "SubLedgerBalanceId": this.str.SubLedgerBalanceId = (string)value; break;							
						case "SubLedgerId": this.str.SubLedgerId = (string)value; break;							
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
						case "SubLedgerBalanceId":
						
							if (value == null || value is System.Int32)
								this.SubLedgerBalanceId = (System.Int32?)value;
							break;
						
						case "SubLedgerId":
						
							if (value == null || value is System.Int32)
								this.SubLedgerId = (System.Int32?)value;
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
		/// Maps to SubLedgerBalances.SubLedgerBalanceId
		/// </summary>
		virtual public System.Int32? SubLedgerBalanceId
		{
			get
			{
				return base.GetSystemInt32(SubLedgerBalancesMetadata.ColumnNames.SubLedgerBalanceId);
			}
			
			set
			{
				base.SetSystemInt32(SubLedgerBalancesMetadata.ColumnNames.SubLedgerBalanceId, value);
			}
		}
		
		/// <summary>
		/// Maps to SubLedgerBalances.SubLedgerId
		/// </summary>
		virtual public System.Int32? SubLedgerId
		{
			get
			{
				return base.GetSystemInt32(SubLedgerBalancesMetadata.ColumnNames.SubLedgerId);
			}
			
			set
			{
				if(base.SetSystemInt32(SubLedgerBalancesMetadata.ColumnNames.SubLedgerId, value))
				{
					this._UpToSubLedgersBySubLedgerId = null;
				}
			}
		}
		
		/// <summary>
		/// Maps to SubLedgerBalances.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(SubLedgerBalancesMetadata.ColumnNames.ChartOfAccountId);
			}
			
			set
			{
				if(base.SetSystemInt32(SubLedgerBalancesMetadata.ColumnNames.ChartOfAccountId, value))
				{
					this._UpToChartOfAccountsByChartOfAccountId = null;
				}
			}
		}
		
		/// <summary>
		/// Maps to SubLedgerBalances.Month
		/// </summary>
		virtual public System.String Month
		{
			get
			{
				return base.GetSystemString(SubLedgerBalancesMetadata.ColumnNames.Month);
			}
			
			set
			{
				base.SetSystemString(SubLedgerBalancesMetadata.ColumnNames.Month, value);
			}
		}
		
		/// <summary>
		/// Maps to SubLedgerBalances.Year
		/// </summary>
		virtual public System.String Year
		{
			get
			{
				return base.GetSystemString(SubLedgerBalancesMetadata.ColumnNames.Year);
			}
			
			set
			{
				base.SetSystemString(SubLedgerBalancesMetadata.ColumnNames.Year, value);
			}
		}
		
		/// <summary>
		/// Maps to SubLedgerBalances.InitialBalance
		/// </summary>
		virtual public System.Decimal? InitialBalance
		{
			get
			{
				return base.GetSystemDecimal(SubLedgerBalancesMetadata.ColumnNames.InitialBalance);
			}
			
			set
			{
				base.SetSystemDecimal(SubLedgerBalancesMetadata.ColumnNames.InitialBalance, value);
			}
		}
		
		/// <summary>
		/// Maps to SubLedgerBalances.DebitAmount
		/// </summary>
		virtual public System.Decimal? DebitAmount
		{
			get
			{
				return base.GetSystemDecimal(SubLedgerBalancesMetadata.ColumnNames.DebitAmount);
			}
			
			set
			{
				base.SetSystemDecimal(SubLedgerBalancesMetadata.ColumnNames.DebitAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to SubLedgerBalances.CreditAmount
		/// </summary>
		virtual public System.Decimal? CreditAmount
		{
			get
			{
				return base.GetSystemDecimal(SubLedgerBalancesMetadata.ColumnNames.CreditAmount);
			}
			
			set
			{
				base.SetSystemDecimal(SubLedgerBalancesMetadata.ColumnNames.CreditAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to SubLedgerBalances.FinalBalance
		/// </summary>
		virtual public System.Decimal? FinalBalance
		{
			get
			{
				return base.GetSystemDecimal(SubLedgerBalancesMetadata.ColumnNames.FinalBalance);
			}
			
			set
			{
				base.SetSystemDecimal(SubLedgerBalancesMetadata.ColumnNames.FinalBalance, value);
			}
		}
		
		[CLSCompliant(false)]
		internal protected ChartOfAccounts _UpToChartOfAccountsByChartOfAccountId;
		[CLSCompliant(false)]
		internal protected SubLedgers _UpToSubLedgersBySubLedgerId;
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
			public esStrings(esSubLedgerBalances entity)
			{
				this.entity = entity;
			}
			
	
			public System.String SubLedgerBalanceId
			{
				get
				{
					System.Int32? data = entity.SubLedgerBalanceId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubLedgerBalanceId = null;
					else entity.SubLedgerBalanceId = Convert.ToInt32(value);
				}
			}
				
			public System.String SubLedgerId
			{
				get
				{
					System.Int32? data = entity.SubLedgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubLedgerId = null;
					else entity.SubLedgerId = Convert.ToInt32(value);
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
			

			private esSubLedgerBalances entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSubLedgerBalancesQuery query)
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
				throw new Exception("esSubLedgerBalances can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class SubLedgerBalances : esSubLedgerBalances
	{

				
		#region UpToChartOfAccountsByChartOfAccountId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_SubLedgerBalances_ChartOfAccounts
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
		

				
		#region UpToSubLedgersBySubLedgerId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_SubLedgerBalances_SubLedgers
		/// </summary>

		[XmlIgnore]
		public SubLedgers UpToSubLedgersBySubLedgerId
		{
			get
			{
				if(this._UpToSubLedgersBySubLedgerId == null
					&& SubLedgerId != null					)
				{
					this._UpToSubLedgersBySubLedgerId = new SubLedgers();
					this._UpToSubLedgersBySubLedgerId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToSubLedgersBySubLedgerId", this._UpToSubLedgersBySubLedgerId);
					this._UpToSubLedgersBySubLedgerId.Query.Where(this._UpToSubLedgersBySubLedgerId.Query.SubLedgerId == this.SubLedgerId);
					this._UpToSubLedgersBySubLedgerId.Query.Load();
				}

				return this._UpToSubLedgersBySubLedgerId;
			}
			
			set
			{
				this.RemovePreSave("UpToSubLedgersBySubLedgerId");
				

				if(value == null)
				{
					this.SubLedgerId = null;
					this._UpToSubLedgersBySubLedgerId = null;
				}
				else
				{
					this.SubLedgerId = value.SubLedgerId;
					this._UpToSubLedgersBySubLedgerId = value;
					this.SetPreSave("UpToSubLedgersBySubLedgerId", this._UpToSubLedgersBySubLedgerId);
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
			if(!this.es.IsDeleted && this._UpToSubLedgersBySubLedgerId != null)
			{
				this.SubLedgerId = this._UpToSubLedgersBySubLedgerId.SubLedgerId;
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
	abstract public class esSubLedgerBalancesQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return SubLedgerBalancesMetadata.Meta();
			}
		}	
		

		public esQueryItem SubLedgerBalanceId
		{
			get
			{
				return new esQueryItem(this, SubLedgerBalancesMetadata.ColumnNames.SubLedgerBalanceId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubLedgerId
		{
			get
			{
				return new esQueryItem(this, SubLedgerBalancesMetadata.ColumnNames.SubLedgerId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, SubLedgerBalancesMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Month
		{
			get
			{
				return new esQueryItem(this, SubLedgerBalancesMetadata.ColumnNames.Month, esSystemType.String);
			}
		} 
		
		public esQueryItem Year
		{
			get
			{
				return new esQueryItem(this, SubLedgerBalancesMetadata.ColumnNames.Year, esSystemType.String);
			}
		} 
		
		public esQueryItem InitialBalance
		{
			get
			{
				return new esQueryItem(this, SubLedgerBalancesMetadata.ColumnNames.InitialBalance, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DebitAmount
		{
			get
			{
				return new esQueryItem(this, SubLedgerBalancesMetadata.ColumnNames.DebitAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CreditAmount
		{
			get
			{
				return new esQueryItem(this, SubLedgerBalancesMetadata.ColumnNames.CreditAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem FinalBalance
		{
			get
			{
				return new esQueryItem(this, SubLedgerBalancesMetadata.ColumnNames.FinalBalance, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SubLedgerBalancesCollection")]
	public partial class SubLedgerBalancesCollection : esSubLedgerBalancesCollection, IEnumerable<SubLedgerBalances>
	{
		public SubLedgerBalancesCollection()
		{

		}
		
		public static implicit operator List<SubLedgerBalances>(SubLedgerBalancesCollection coll)
		{
			List<SubLedgerBalances> list = new List<SubLedgerBalances>();
			
			foreach (SubLedgerBalances emp in coll)
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
				return  SubLedgerBalancesMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SubLedgerBalancesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SubLedgerBalances(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SubLedgerBalances();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public SubLedgerBalancesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SubLedgerBalancesQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(SubLedgerBalancesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public SubLedgerBalances AddNew()
		{
			SubLedgerBalances entity = base.AddNewEntity() as SubLedgerBalances;
			
			return entity;
		}

		public SubLedgerBalances FindByPrimaryKey(System.Int32 subLedgerBalanceId)
		{
			return base.FindByPrimaryKey(subLedgerBalanceId) as SubLedgerBalances;
		}


		#region IEnumerable<SubLedgerBalances> Members

		IEnumerator<SubLedgerBalances> IEnumerable<SubLedgerBalances>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as SubLedgerBalances;
			}
		}

		#endregion
		
		private SubLedgerBalancesQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SubLedgerBalances' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("SubLedgerBalances ({SubLedgerBalanceId})")]
	[Serializable]
	public partial class SubLedgerBalances : esSubLedgerBalances
	{
		public SubLedgerBalances()
		{

		}
	
		public SubLedgerBalances(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SubLedgerBalancesMetadata.Meta();
			}
		}
		
		
		
		override protected esSubLedgerBalancesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SubLedgerBalancesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public SubLedgerBalancesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SubLedgerBalancesQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(SubLedgerBalancesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private SubLedgerBalancesQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class SubLedgerBalancesQuery : esSubLedgerBalancesQuery
	{
		public SubLedgerBalancesQuery()
		{

		}		
		
		public SubLedgerBalancesQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "SubLedgerBalancesQuery";
        }
		
			
	}


	[Serializable]
	public partial class SubLedgerBalancesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SubLedgerBalancesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SubLedgerBalancesMetadata.ColumnNames.SubLedgerBalanceId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SubLedgerBalancesMetadata.PropertyNames.SubLedgerBalanceId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubLedgerBalancesMetadata.ColumnNames.SubLedgerId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SubLedgerBalancesMetadata.PropertyNames.SubLedgerId;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubLedgerBalancesMetadata.ColumnNames.ChartOfAccountId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SubLedgerBalancesMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubLedgerBalancesMetadata.ColumnNames.Month, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = SubLedgerBalancesMetadata.PropertyNames.Month;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubLedgerBalancesMetadata.ColumnNames.Year, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = SubLedgerBalancesMetadata.PropertyNames.Year;
			c.CharacterMaxLength = 4;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubLedgerBalancesMetadata.ColumnNames.InitialBalance, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SubLedgerBalancesMetadata.PropertyNames.InitialBalance;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubLedgerBalancesMetadata.ColumnNames.DebitAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SubLedgerBalancesMetadata.PropertyNames.DebitAmount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubLedgerBalancesMetadata.ColumnNames.CreditAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SubLedgerBalancesMetadata.PropertyNames.CreditAmount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(SubLedgerBalancesMetadata.ColumnNames.FinalBalance, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SubLedgerBalancesMetadata.PropertyNames.FinalBalance;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public SubLedgerBalancesMetadata Meta()
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
			 public const string SubLedgerBalanceId = "SubLedgerBalanceId";
			 public const string SubLedgerId = "SubLedgerId";
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
			 public const string SubLedgerBalanceId = "SubLedgerBalanceId";
			 public const string SubLedgerId = "SubLedgerId";
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
			lock (typeof(SubLedgerBalancesMetadata))
			{
				if(SubLedgerBalancesMetadata.mapDelegates == null)
				{
					SubLedgerBalancesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (SubLedgerBalancesMetadata.meta == null)
				{
					SubLedgerBalancesMetadata.meta = new SubLedgerBalancesMetadata();
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
				

				meta.AddTypeMap("SubLedgerBalanceId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubLedgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Month", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Year", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("InitialBalance", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("DebitAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("CreditAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("FinalBalance", new esTypeMap("money", "System.Decimal"));			
				
				
				
				meta.Source = "SubLedgerBalances";
				meta.Destination = "SubLedgerBalances";
				
				meta.spInsert = "proc_SubLedgerBalancesInsert";				
				meta.spUpdate = "proc_SubLedgerBalancesUpdate";		
				meta.spDelete = "proc_SubLedgerBalancesDelete";
				meta.spLoadAll = "proc_SubLedgerBalancesLoadAll";
				meta.spLoadByPrimaryKey = "proc_SubLedgerBalancesLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SubLedgerBalancesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
