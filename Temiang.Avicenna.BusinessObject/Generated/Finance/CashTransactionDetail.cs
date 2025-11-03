/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/4/2021 8:33:32 AM
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
	abstract public class esCashTransactionDetailCollection : esEntityCollectionWAuditLog
	{
		public esCashTransactionDetailCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "CashTransactionDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esCashTransactionDetailQuery query)
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
			this.InitQuery(query as esCashTransactionDetailQuery);
		}
		#endregion
		
		virtual public CashTransactionDetail DetachEntity(CashTransactionDetail entity)
		{
			return base.DetachEntity(entity) as CashTransactionDetail;
		}
		
		virtual public CashTransactionDetail AttachEntity(CashTransactionDetail entity)
		{
			return base.AttachEntity(entity) as CashTransactionDetail;
		}
		
		virtual public void Combine(CashTransactionDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CashTransactionDetail this[int index]
		{
			get
			{
				return base[index] as CashTransactionDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CashTransactionDetail);
		}
	}



	[Serializable]
	abstract public class esCashTransactionDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCashTransactionDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esCashTransactionDetail()
		{

		}

		public esCashTransactionDetail(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 detailId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(detailId);
			else
				return LoadByPrimaryKeyStoredProcedure(detailId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 detailId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(detailId);
			else
				return LoadByPrimaryKeyStoredProcedure(detailId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 detailId)
		{
			esCashTransactionDetailQuery query = this.GetDynamicQuery();
			query.Where(query.DetailId == detailId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 detailId)
		{
			esParameters parms = new esParameters();
			parms.Add("DetailId",detailId);
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
						case "DetailId": this.str.DetailId = (string)value; break;							
						case "TransactionId": this.str.TransactionId = (string)value; break;							
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;							
						case "SubLedgerId": this.str.SubLedgerId = (string)value; break;							
						case "CostCenterId": this.str.CostCenterId = (string)value; break;							
						case "Debit": this.str.Debit = (string)value; break;							
						case "Credit": this.str.Credit = (string)value; break;							
						case "Amount": this.str.Amount = (string)value; break;							
						case "Description": this.str.Description = (string)value; break;							
						case "ListID": this.str.ListID = (string)value; break;							
						case "IsParentRefference": this.str.IsParentRefference = (string)value; break;							
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "DetailId":
						
							if (value == null || value is System.Int32)
								this.DetailId = (System.Int32?)value;
							break;
						
						case "TransactionId":
						
							if (value == null || value is System.Int32)
								this.TransactionId = (System.Int32?)value;
							break;
						
						case "ChartOfAccountId":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
							break;
						
						case "SubLedgerId":
						
							if (value == null || value is System.Int32)
								this.SubLedgerId = (System.Int32?)value;
							break;
						
						case "CostCenterId":
						
							if (value == null || value is System.Int32)
								this.CostCenterId = (System.Int32?)value;
							break;
						
						case "Debit":
						
							if (value == null || value is System.Decimal)
								this.Debit = (System.Decimal?)value;
							break;
						
						case "Credit":
						
							if (value == null || value is System.Decimal)
								this.Credit = (System.Decimal?)value;
							break;
						
						case "Amount":
						
							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
							break;
						
						case "IsParentRefference":
						
							if (value == null || value is System.Boolean)
								this.IsParentRefference = (System.Boolean?)value;
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
		/// Maps to CashTransactionDetail.DetailId
		/// </summary>
		virtual public System.Int32? DetailId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionDetailMetadata.ColumnNames.DetailId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionDetailMetadata.ColumnNames.DetailId, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionDetail.TransactionId
		/// </summary>
		virtual public System.Int32? TransactionId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionDetailMetadata.ColumnNames.TransactionId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionDetailMetadata.ColumnNames.TransactionId, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionDetail.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionDetailMetadata.ColumnNames.ChartOfAccountId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionDetailMetadata.ColumnNames.ChartOfAccountId, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionDetail.SubLedgerId
		/// </summary>
		virtual public System.Int32? SubLedgerId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionDetailMetadata.ColumnNames.SubLedgerId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionDetailMetadata.ColumnNames.SubLedgerId, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionDetail.CostCenterId
		/// </summary>
		virtual public System.Int32? CostCenterId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionDetailMetadata.ColumnNames.CostCenterId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionDetailMetadata.ColumnNames.CostCenterId, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionDetail.Debit
		/// </summary>
		virtual public System.Decimal? Debit
		{
			get
			{
				return base.GetSystemDecimal(CashTransactionDetailMetadata.ColumnNames.Debit);
			}
			
			set
			{
				base.SetSystemDecimal(CashTransactionDetailMetadata.ColumnNames.Debit, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionDetail.Credit
		/// </summary>
		virtual public System.Decimal? Credit
		{
			get
			{
				return base.GetSystemDecimal(CashTransactionDetailMetadata.ColumnNames.Credit);
			}
			
			set
			{
				base.SetSystemDecimal(CashTransactionDetailMetadata.ColumnNames.Credit, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionDetail.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(CashTransactionDetailMetadata.ColumnNames.Amount);
			}
			
			set
			{
				base.SetSystemDecimal(CashTransactionDetailMetadata.ColumnNames.Amount, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionDetail.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(CashTransactionDetailMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(CashTransactionDetailMetadata.ColumnNames.Description, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionDetail.ListID
		/// </summary>
		virtual public System.String ListID
		{
			get
			{
				return base.GetSystemString(CashTransactionDetailMetadata.ColumnNames.ListID);
			}
			
			set
			{
				base.SetSystemString(CashTransactionDetailMetadata.ColumnNames.ListID, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionDetail.IsParentRefference
		/// </summary>
		virtual public System.Boolean? IsParentRefference
		{
			get
			{
				return base.GetSystemBoolean(CashTransactionDetailMetadata.ColumnNames.IsParentRefference);
			}
			
			set
			{
				base.SetSystemBoolean(CashTransactionDetailMetadata.ColumnNames.IsParentRefference, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransactionDetail.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(CashTransactionDetailMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(CashTransactionDetailMetadata.ColumnNames.ReferenceNo, value);
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
			public esStrings(esCashTransactionDetail entity)
			{
				this.entity = entity;
			}
			
	
			public System.String DetailId
			{
				get
				{
					System.Int32? data = entity.DetailId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DetailId = null;
					else entity.DetailId = Convert.ToInt32(value);
				}
			}
				
			public System.String TransactionId
			{
				get
				{
					System.Int32? data = entity.TransactionId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionId = null;
					else entity.TransactionId = Convert.ToInt32(value);
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
				
			public System.String CostCenterId
			{
				get
				{
					System.Int32? data = entity.CostCenterId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CostCenterId = null;
					else entity.CostCenterId = Convert.ToInt32(value);
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
				
			public System.String Amount
			{
				get
				{
					System.Decimal? data = entity.Amount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Amount = null;
					else entity.Amount = Convert.ToDecimal(value);
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
				
			public System.String ListID
			{
				get
				{
					System.String data = entity.ListID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ListID = null;
					else entity.ListID = Convert.ToString(value);
				}
			}
				
			public System.String IsParentRefference
			{
				get
				{
					System.Boolean? data = entity.IsParentRefference;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsParentRefference = null;
					else entity.IsParentRefference = Convert.ToBoolean(value);
				}
			}
				
			public System.String ReferenceNo
			{
				get
				{
					System.String data = entity.ReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceNo = null;
					else entity.ReferenceNo = Convert.ToString(value);
				}
			}
			

			private esCashTransactionDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCashTransactionDetailQuery query)
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
				throw new Exception("esCashTransactionDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esCashTransactionDetailQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CashTransactionDetailMetadata.Meta();
			}
		}	
		

		public esQueryItem DetailId
		{
			get
			{
				return new esQueryItem(this, CashTransactionDetailMetadata.ColumnNames.DetailId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem TransactionId
		{
			get
			{
				return new esQueryItem(this, CashTransactionDetailMetadata.ColumnNames.TransactionId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, CashTransactionDetailMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubLedgerId
		{
			get
			{
				return new esQueryItem(this, CashTransactionDetailMetadata.ColumnNames.SubLedgerId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem CostCenterId
		{
			get
			{
				return new esQueryItem(this, CashTransactionDetailMetadata.ColumnNames.CostCenterId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Debit
		{
			get
			{
				return new esQueryItem(this, CashTransactionDetailMetadata.ColumnNames.Debit, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Credit
		{
			get
			{
				return new esQueryItem(this, CashTransactionDetailMetadata.ColumnNames.Credit, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, CashTransactionDetailMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, CashTransactionDetailMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
		
		public esQueryItem ListID
		{
			get
			{
				return new esQueryItem(this, CashTransactionDetailMetadata.ColumnNames.ListID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsParentRefference
		{
			get
			{
				return new esQueryItem(this, CashTransactionDetailMetadata.ColumnNames.IsParentRefference, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, CashTransactionDetailMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CashTransactionDetailCollection")]
	public partial class CashTransactionDetailCollection : esCashTransactionDetailCollection, IEnumerable<CashTransactionDetail>
	{
		public CashTransactionDetailCollection()
		{

		}
		
		public static implicit operator List<CashTransactionDetail>(CashTransactionDetailCollection coll)
		{
			List<CashTransactionDetail> list = new List<CashTransactionDetail>();
			
			foreach (CashTransactionDetail emp in coll)
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
				return  CashTransactionDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CashTransactionDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CashTransactionDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CashTransactionDetail();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public CashTransactionDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CashTransactionDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(CashTransactionDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public CashTransactionDetail AddNew()
		{
			CashTransactionDetail entity = base.AddNewEntity() as CashTransactionDetail;
			
			return entity;
		}

		public CashTransactionDetail FindByPrimaryKey(System.Int32 detailId)
		{
			return base.FindByPrimaryKey(detailId) as CashTransactionDetail;
		}


		#region IEnumerable<CashTransactionDetail> Members

		IEnumerator<CashTransactionDetail> IEnumerable<CashTransactionDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CashTransactionDetail;
			}
		}

		#endregion
		
		private CashTransactionDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CashTransactionDetail' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("CashTransactionDetail ({DetailId})")]
	[Serializable]
	public partial class CashTransactionDetail : esCashTransactionDetail
	{
		public CashTransactionDetail()
		{

		}
	
		public CashTransactionDetail(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CashTransactionDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esCashTransactionDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CashTransactionDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public CashTransactionDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CashTransactionDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(CashTransactionDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private CashTransactionDetailQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class CashTransactionDetailQuery : esCashTransactionDetailQuery
	{
		public CashTransactionDetailQuery()
		{

		}		
		
		public CashTransactionDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "CashTransactionDetailQuery";
        }
		
			
	}


	[Serializable]
	public partial class CashTransactionDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CashTransactionDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CashTransactionDetailMetadata.ColumnNames.DetailId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionDetailMetadata.PropertyNames.DetailId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionDetailMetadata.ColumnNames.TransactionId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionDetailMetadata.PropertyNames.TransactionId;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionDetailMetadata.ColumnNames.ChartOfAccountId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionDetailMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionDetailMetadata.ColumnNames.SubLedgerId, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionDetailMetadata.PropertyNames.SubLedgerId;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionDetailMetadata.ColumnNames.CostCenterId, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionDetailMetadata.PropertyNames.CostCenterId;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionDetailMetadata.ColumnNames.Debit, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CashTransactionDetailMetadata.PropertyNames.Debit;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionDetailMetadata.ColumnNames.Credit, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CashTransactionDetailMetadata.PropertyNames.Credit;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionDetailMetadata.ColumnNames.Amount, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CashTransactionDetailMetadata.PropertyNames.Amount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionDetailMetadata.ColumnNames.Description, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionDetailMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 255;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionDetailMetadata.ColumnNames.ListID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionDetailMetadata.PropertyNames.ListID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionDetailMetadata.ColumnNames.IsParentRefference, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CashTransactionDetailMetadata.PropertyNames.IsParentRefference;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionDetailMetadata.ColumnNames.ReferenceNo, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionDetailMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public CashTransactionDetailMetadata Meta()
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
			 public const string DetailId = "DetailId";
			 public const string TransactionId = "TransactionId";
			 public const string ChartOfAccountId = "ChartOfAccountId";
			 public const string SubLedgerId = "SubLedgerId";
			 public const string CostCenterId = "CostCenterId";
			 public const string Debit = "Debit";
			 public const string Credit = "Credit";
			 public const string Amount = "Amount";
			 public const string Description = "Description";
			 public const string ListID = "ListID";
			 public const string IsParentRefference = "IsParentRefference";
			 public const string ReferenceNo = "ReferenceNo";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string DetailId = "DetailId";
			 public const string TransactionId = "TransactionId";
			 public const string ChartOfAccountId = "ChartOfAccountId";
			 public const string SubLedgerId = "SubLedgerId";
			 public const string CostCenterId = "CostCenterId";
			 public const string Debit = "Debit";
			 public const string Credit = "Credit";
			 public const string Amount = "Amount";
			 public const string Description = "Description";
			 public const string ListID = "ListID";
			 public const string IsParentRefference = "IsParentRefference";
			 public const string ReferenceNo = "ReferenceNo";
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
			lock (typeof(CashTransactionDetailMetadata))
			{
				if(CashTransactionDetailMetadata.mapDelegates == null)
				{
					CashTransactionDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CashTransactionDetailMetadata.meta == null)
				{
					CashTransactionDetailMetadata.meta = new CashTransactionDetailMetadata();
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
				

				meta.AddTypeMap("DetailId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TransactionId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubLedgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CostCenterId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Debit", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Credit", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Amount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Description", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ListID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsParentRefference", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "CashTransactionDetail";
				meta.Destination = "CashTransactionDetail";
				
				meta.spInsert = "proc_CashTransactionDetailInsert";				
				meta.spUpdate = "proc_CashTransactionDetailUpdate";		
				meta.spDelete = "proc_CashTransactionDetailDelete";
				meta.spLoadAll = "proc_CashTransactionDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_CashTransactionDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CashTransactionDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
