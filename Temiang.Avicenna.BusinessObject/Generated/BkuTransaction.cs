/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/29/2020 4:09:04 PM
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
	abstract public class esBkuTransactionCollection : esEntityCollectionWAuditLog
	{
		public esBkuTransactionCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "BkuTransactionCollection";
		}

		#region Query Logic
		protected void InitQuery(esBkuTransactionQuery query)
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
			this.InitQuery(query as esBkuTransactionQuery);
		}
		#endregion
		
		virtual public BkuTransaction DetachEntity(BkuTransaction entity)
		{
			return base.DetachEntity(entity) as BkuTransaction;
		}
		
		virtual public BkuTransaction AttachEntity(BkuTransaction entity)
		{
			return base.AttachEntity(entity) as BkuTransaction;
		}
		
		virtual public void Combine(BkuTransactionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BkuTransaction this[int index]
		{
			get
			{
				return base[index] as BkuTransaction;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BkuTransaction);
		}
	}



	[Serializable]
	abstract public class esBkuTransaction : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBkuTransactionQuery GetDynamicQuery()
		{
			return null;
		}

		public esBkuTransaction()
		{

		}

		public esBkuTransaction(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 bkuID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bkuID);
			else
				return LoadByPrimaryKeyStoredProcedure(bkuID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 bkuID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bkuID);
			else
				return LoadByPrimaryKeyStoredProcedure(bkuID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 bkuID)
		{
			esBkuTransactionQuery query = this.GetDynamicQuery();
			query.Where(query.BkuID == bkuID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 bkuID)
		{
			esParameters parms = new esParameters();
			parms.Add("BkuID",bkuID);
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
						case "BkuID": this.str.BkuID = (string)value; break;							
						case "RekeningID": this.str.RekeningID = (string)value; break;							
						case "UnitID": this.str.UnitID = (string)value; break;							
						case "Debit": this.str.Debit = (string)value; break;							
						case "Credit": this.str.Credit = (string)value; break;							
						case "Uraian": this.str.Uraian = (string)value; break;							
						case "PaymentReferenceNo": this.str.PaymentReferenceNo = (string)value; break;							
						case "InvoiceReferenceNo": this.str.InvoiceReferenceNo = (string)value; break;							
						case "TransactionReferenceNo": this.str.TransactionReferenceNo = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "BkuID":
						
							if (value == null || value is System.Int32)
								this.BkuID = (System.Int32?)value;
							break;
						
						case "RekeningID":
						
							if (value == null || value is System.Int32)
								this.RekeningID = (System.Int32?)value;
							break;
						
						case "UnitID":
						
							if (value == null || value is System.Int32)
								this.UnitID = (System.Int32?)value;
							break;
						
						case "Debit":
						
							if (value == null || value is System.Decimal)
								this.Debit = (System.Decimal?)value;
							break;
						
						case "Credit":
						
							if (value == null || value is System.Decimal)
								this.Credit = (System.Decimal?)value;
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
		/// Maps to BkuTransaction.BkuID
		/// </summary>
		virtual public System.Int32? BkuID
		{
			get
			{
				return base.GetSystemInt32(BkuTransactionMetadata.ColumnNames.BkuID);
			}
			
			set
			{
				base.SetSystemInt32(BkuTransactionMetadata.ColumnNames.BkuID, value);
			}
		}
		
		/// <summary>
		/// Maps to BkuTransaction.RekeningID
		/// </summary>
		virtual public System.Int32? RekeningID
		{
			get
			{
				return base.GetSystemInt32(BkuTransactionMetadata.ColumnNames.RekeningID);
			}
			
			set
			{
				base.SetSystemInt32(BkuTransactionMetadata.ColumnNames.RekeningID, value);
			}
		}
		
		/// <summary>
		/// Maps to BkuTransaction.UnitID
		/// </summary>
		virtual public System.Int32? UnitID
		{
			get
			{
				return base.GetSystemInt32(BkuTransactionMetadata.ColumnNames.UnitID);
			}
			
			set
			{
				base.SetSystemInt32(BkuTransactionMetadata.ColumnNames.UnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to BkuTransaction.Debit
		/// </summary>
		virtual public System.Decimal? Debit
		{
			get
			{
				return base.GetSystemDecimal(BkuTransactionMetadata.ColumnNames.Debit);
			}
			
			set
			{
				base.SetSystemDecimal(BkuTransactionMetadata.ColumnNames.Debit, value);
			}
		}
		
		/// <summary>
		/// Maps to BkuTransaction.Credit
		/// </summary>
		virtual public System.Decimal? Credit
		{
			get
			{
				return base.GetSystemDecimal(BkuTransactionMetadata.ColumnNames.Credit);
			}
			
			set
			{
				base.SetSystemDecimal(BkuTransactionMetadata.ColumnNames.Credit, value);
			}
		}
		
		/// <summary>
		/// Maps to BkuTransaction.Uraian
		/// </summary>
		virtual public System.String Uraian
		{
			get
			{
				return base.GetSystemString(BkuTransactionMetadata.ColumnNames.Uraian);
			}
			
			set
			{
				base.SetSystemString(BkuTransactionMetadata.ColumnNames.Uraian, value);
			}
		}
		
		/// <summary>
		/// Maps to BkuTransaction.PaymentReferenceNo
		/// </summary>
		virtual public System.String PaymentReferenceNo
		{
			get
			{
				return base.GetSystemString(BkuTransactionMetadata.ColumnNames.PaymentReferenceNo);
			}
			
			set
			{
				base.SetSystemString(BkuTransactionMetadata.ColumnNames.PaymentReferenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to BkuTransaction.InvoiceReferenceNo
		/// </summary>
		virtual public System.String InvoiceReferenceNo
		{
			get
			{
				return base.GetSystemString(BkuTransactionMetadata.ColumnNames.InvoiceReferenceNo);
			}
			
			set
			{
				base.SetSystemString(BkuTransactionMetadata.ColumnNames.InvoiceReferenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to BkuTransaction.TransactionReferenceNo
		/// </summary>
		virtual public System.String TransactionReferenceNo
		{
			get
			{
				return base.GetSystemString(BkuTransactionMetadata.ColumnNames.TransactionReferenceNo);
			}
			
			set
			{
				base.SetSystemString(BkuTransactionMetadata.ColumnNames.TransactionReferenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to BkuTransaction.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BkuTransactionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BkuTransactionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to BkuTransaction.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BkuTransactionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BkuTransactionMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esBkuTransaction entity)
			{
				this.entity = entity;
			}
			
	
			public System.String BkuID
			{
				get
				{
					System.Int32? data = entity.BkuID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BkuID = null;
					else entity.BkuID = Convert.ToInt32(value);
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
				
			public System.String UnitID
			{
				get
				{
					System.Int32? data = entity.UnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UnitID = null;
					else entity.UnitID = Convert.ToInt32(value);
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
				
			public System.String Uraian
			{
				get
				{
					System.String data = entity.Uraian;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Uraian = null;
					else entity.Uraian = Convert.ToString(value);
				}
			}
				
			public System.String PaymentReferenceNo
			{
				get
				{
					System.String data = entity.PaymentReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentReferenceNo = null;
					else entity.PaymentReferenceNo = Convert.ToString(value);
				}
			}
				
			public System.String InvoiceReferenceNo
			{
				get
				{
					System.String data = entity.InvoiceReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoiceReferenceNo = null;
					else entity.InvoiceReferenceNo = Convert.ToString(value);
				}
			}
				
			public System.String TransactionReferenceNo
			{
				get
				{
					System.String data = entity.TransactionReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionReferenceNo = null;
					else entity.TransactionReferenceNo = Convert.ToString(value);
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
			

			private esBkuTransaction entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBkuTransactionQuery query)
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
				throw new Exception("esBkuTransaction can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class BkuTransaction : esBkuTransaction
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
	abstract public class esBkuTransactionQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return BkuTransactionMetadata.Meta();
			}
		}	
		

		public esQueryItem BkuID
		{
			get
			{
				return new esQueryItem(this, BkuTransactionMetadata.ColumnNames.BkuID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RekeningID
		{
			get
			{
				return new esQueryItem(this, BkuTransactionMetadata.ColumnNames.RekeningID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem UnitID
		{
			get
			{
				return new esQueryItem(this, BkuTransactionMetadata.ColumnNames.UnitID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Debit
		{
			get
			{
				return new esQueryItem(this, BkuTransactionMetadata.ColumnNames.Debit, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Credit
		{
			get
			{
				return new esQueryItem(this, BkuTransactionMetadata.ColumnNames.Credit, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Uraian
		{
			get
			{
				return new esQueryItem(this, BkuTransactionMetadata.ColumnNames.Uraian, esSystemType.String);
			}
		} 
		
		public esQueryItem PaymentReferenceNo
		{
			get
			{
				return new esQueryItem(this, BkuTransactionMetadata.ColumnNames.PaymentReferenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem InvoiceReferenceNo
		{
			get
			{
				return new esQueryItem(this, BkuTransactionMetadata.ColumnNames.InvoiceReferenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionReferenceNo
		{
			get
			{
				return new esQueryItem(this, BkuTransactionMetadata.ColumnNames.TransactionReferenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BkuTransactionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BkuTransactionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BkuTransactionCollection")]
	public partial class BkuTransactionCollection : esBkuTransactionCollection, IEnumerable<BkuTransaction>
	{
		public BkuTransactionCollection()
		{

		}
		
		public static implicit operator List<BkuTransaction>(BkuTransactionCollection coll)
		{
			List<BkuTransaction> list = new List<BkuTransaction>();
			
			foreach (BkuTransaction emp in coll)
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
				return  BkuTransactionMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BkuTransactionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BkuTransaction(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BkuTransaction();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public BkuTransactionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BkuTransactionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(BkuTransactionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public BkuTransaction AddNew()
		{
			BkuTransaction entity = base.AddNewEntity() as BkuTransaction;
			
			return entity;
		}

		public BkuTransaction FindByPrimaryKey(System.Int32 bkuID)
		{
			return base.FindByPrimaryKey(bkuID) as BkuTransaction;
		}


		#region IEnumerable<BkuTransaction> Members

		IEnumerator<BkuTransaction> IEnumerable<BkuTransaction>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BkuTransaction;
			}
		}

		#endregion
		
		private BkuTransactionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BkuTransaction' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("BkuTransaction ({BkuID})")]
	[Serializable]
	public partial class BkuTransaction : esBkuTransaction
	{
		public BkuTransaction()
		{

		}
	
		public BkuTransaction(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BkuTransactionMetadata.Meta();
			}
		}
		
		
		
		override protected esBkuTransactionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BkuTransactionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public BkuTransactionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BkuTransactionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(BkuTransactionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private BkuTransactionQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class BkuTransactionQuery : esBkuTransactionQuery
	{
		public BkuTransactionQuery()
		{

		}		
		
		public BkuTransactionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "BkuTransactionQuery";
        }
		
			
	}


	[Serializable]
	public partial class BkuTransactionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BkuTransactionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BkuTransactionMetadata.ColumnNames.BkuID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BkuTransactionMetadata.PropertyNames.BkuID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(BkuTransactionMetadata.ColumnNames.RekeningID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BkuTransactionMetadata.PropertyNames.RekeningID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BkuTransactionMetadata.ColumnNames.UnitID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BkuTransactionMetadata.PropertyNames.UnitID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BkuTransactionMetadata.ColumnNames.Debit, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BkuTransactionMetadata.PropertyNames.Debit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BkuTransactionMetadata.ColumnNames.Credit, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BkuTransactionMetadata.PropertyNames.Credit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BkuTransactionMetadata.ColumnNames.Uraian, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = BkuTransactionMetadata.PropertyNames.Uraian;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BkuTransactionMetadata.ColumnNames.PaymentReferenceNo, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = BkuTransactionMetadata.PropertyNames.PaymentReferenceNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BkuTransactionMetadata.ColumnNames.InvoiceReferenceNo, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = BkuTransactionMetadata.PropertyNames.InvoiceReferenceNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BkuTransactionMetadata.ColumnNames.TransactionReferenceNo, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = BkuTransactionMetadata.PropertyNames.TransactionReferenceNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BkuTransactionMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BkuTransactionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BkuTransactionMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = BkuTransactionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public BkuTransactionMetadata Meta()
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
			 public const string BkuID = "BkuID";
			 public const string RekeningID = "RekeningID";
			 public const string UnitID = "UnitID";
			 public const string Debit = "Debit";
			 public const string Credit = "Credit";
			 public const string Uraian = "Uraian";
			 public const string PaymentReferenceNo = "PaymentReferenceNo";
			 public const string InvoiceReferenceNo = "InvoiceReferenceNo";
			 public const string TransactionReferenceNo = "TransactionReferenceNo";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string BkuID = "BkuID";
			 public const string RekeningID = "RekeningID";
			 public const string UnitID = "UnitID";
			 public const string Debit = "Debit";
			 public const string Credit = "Credit";
			 public const string Uraian = "Uraian";
			 public const string PaymentReferenceNo = "PaymentReferenceNo";
			 public const string InvoiceReferenceNo = "InvoiceReferenceNo";
			 public const string TransactionReferenceNo = "TransactionReferenceNo";
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
			lock (typeof(BkuTransactionMetadata))
			{
				if(BkuTransactionMetadata.mapDelegates == null)
				{
					BkuTransactionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BkuTransactionMetadata.meta == null)
				{
					BkuTransactionMetadata.meta = new BkuTransactionMetadata();
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
				

				meta.AddTypeMap("BkuID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RekeningID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("UnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Debit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Credit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Uraian", new esTypeMap("nchar", "System.String"));
				meta.AddTypeMap("PaymentReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InvoiceReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "BkuTransaction";
				meta.Destination = "BkuTransaction";
				
				meta.spInsert = "proc_BkuTransactionInsert";				
				meta.spUpdate = "proc_BkuTransactionUpdate";		
				meta.spDelete = "proc_BkuTransactionDelete";
				meta.spLoadAll = "proc_BkuTransactionLoadAll";
				meta.spLoadByPrimaryKey = "proc_BkuTransactionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BkuTransactionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
