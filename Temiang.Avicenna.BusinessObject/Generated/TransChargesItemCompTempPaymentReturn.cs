/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/19/2015 2:31:39 PM
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
	abstract public class esTransChargesItemCompTempPaymentReturnCollection : esEntityCollectionWAuditLog
	{
		public esTransChargesItemCompTempPaymentReturnCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TransChargesItemCompTempPaymentReturnCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransChargesItemCompTempPaymentReturnQuery query)
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
			this.InitQuery(query as esTransChargesItemCompTempPaymentReturnQuery);
		}
		#endregion
		
		virtual public TransChargesItemCompTempPaymentReturn DetachEntity(TransChargesItemCompTempPaymentReturn entity)
		{
			return base.DetachEntity(entity) as TransChargesItemCompTempPaymentReturn;
		}
		
		virtual public TransChargesItemCompTempPaymentReturn AttachEntity(TransChargesItemCompTempPaymentReturn entity)
		{
			return base.AttachEntity(entity) as TransChargesItemCompTempPaymentReturn;
		}
		
		virtual public void Combine(TransChargesItemCompTempPaymentReturnCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransChargesItemCompTempPaymentReturn this[int index]
		{
			get
			{
				return base[index] as TransChargesItemCompTempPaymentReturn;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransChargesItemCompTempPaymentReturn);
		}
	}



	[Serializable]
	abstract public class esTransChargesItemCompTempPaymentReturn : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransChargesItemCompTempPaymentReturnQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransChargesItemCompTempPaymentReturn()
		{

		}

		public esTransChargesItemCompTempPaymentReturn(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String transactionNo, System.String sequenceNo, System.String tariffComponentID, System.String intermBillNo, System.String paymentNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, tariffComponentID, intermBillNo, paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, tariffComponentID, intermBillNo, paymentNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo, System.String sequenceNo, System.String tariffComponentID, System.String intermBillNo, System.String paymentNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, tariffComponentID, intermBillNo, paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, tariffComponentID, intermBillNo, paymentNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String transactionNo, System.String sequenceNo, System.String tariffComponentID, System.String intermBillNo, System.String paymentNo)
		{
			esTransChargesItemCompTempPaymentReturnQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo, query.TariffComponentID == tariffComponentID, query.IntermBillNo == intermBillNo, query.PaymentNo == paymentNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo, System.String sequenceNo, System.String tariffComponentID, System.String intermBillNo, System.String paymentNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);			parms.Add("SequenceNo",sequenceNo);			parms.Add("TariffComponentID",tariffComponentID);			parms.Add("IntermBillNo",intermBillNo);			parms.Add("PaymentNo",paymentNo);
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
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;							
						case "IntermBillNo": this.str.IntermBillNo = (string)value; break;							
						case "PaymentNo": this.str.PaymentNo = (string)value; break;							
						case "Price": this.str.Price = (string)value; break;							
						case "Discount": this.str.Discount = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Price":
						
							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
							break;
						
						case "Discount":
						
							if (value == null || value is System.Decimal)
								this.Discount = (System.Decimal?)value;
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
		/// Maps to TransChargesItemCompTempPaymentReturn.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemCompTempPaymentReturn.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemCompTempPaymentReturn.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemCompTempPaymentReturn.IntermBillNo
		/// </summary>
		virtual public System.String IntermBillNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.IntermBillNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.IntermBillNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemCompTempPaymentReturn.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.PaymentNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.PaymentNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemCompTempPaymentReturn.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.Price, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemCompTempPaymentReturn.Discount
		/// </summary>
		virtual public System.Decimal? Discount
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.Discount);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.Discount, value);
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
			public esStrings(esTransChargesItemCompTempPaymentReturn entity)
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
				
			public System.String TariffComponentID
			{
				get
				{
					System.String data = entity.TariffComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffComponentID = null;
					else entity.TariffComponentID = Convert.ToString(value);
				}
			}
				
			public System.String IntermBillNo
			{
				get
				{
					System.String data = entity.IntermBillNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IntermBillNo = null;
					else entity.IntermBillNo = Convert.ToString(value);
				}
			}
				
			public System.String PaymentNo
			{
				get
				{
					System.String data = entity.PaymentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentNo = null;
					else entity.PaymentNo = Convert.ToString(value);
				}
			}
				
			public System.String Price
			{
				get
				{
					System.Decimal? data = entity.Price;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Price = null;
					else entity.Price = Convert.ToDecimal(value);
				}
			}
				
			public System.String Discount
			{
				get
				{
					System.Decimal? data = entity.Discount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Discount = null;
					else entity.Discount = Convert.ToDecimal(value);
				}
			}
			

			private esTransChargesItemCompTempPaymentReturn entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransChargesItemCompTempPaymentReturnQuery query)
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
				throw new Exception("esTransChargesItemCompTempPaymentReturn can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class TransChargesItemCompTempPaymentReturn : esTransChargesItemCompTempPaymentReturn
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
	abstract public class esTransChargesItemCompTempPaymentReturnQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TransChargesItemCompTempPaymentReturnMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
		
		public esQueryItem IntermBillNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.IntermBillNo, esSystemType.String);
			}
		} 
		
		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Discount
		{
			get
			{
				return new esQueryItem(this, TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.Discount, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransChargesItemCompTempPaymentReturnCollection")]
	public partial class TransChargesItemCompTempPaymentReturnCollection : esTransChargesItemCompTempPaymentReturnCollection, IEnumerable<TransChargesItemCompTempPaymentReturn>
	{
		public TransChargesItemCompTempPaymentReturnCollection()
		{

		}
		
		public static implicit operator List<TransChargesItemCompTempPaymentReturn>(TransChargesItemCompTempPaymentReturnCollection coll)
		{
			List<TransChargesItemCompTempPaymentReturn> list = new List<TransChargesItemCompTempPaymentReturn>();
			
			foreach (TransChargesItemCompTempPaymentReturn emp in coll)
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
				return  TransChargesItemCompTempPaymentReturnMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesItemCompTempPaymentReturnQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransChargesItemCompTempPaymentReturn(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransChargesItemCompTempPaymentReturn();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TransChargesItemCompTempPaymentReturnQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesItemCompTempPaymentReturnQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TransChargesItemCompTempPaymentReturnQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public TransChargesItemCompTempPaymentReturn AddNew()
		{
			TransChargesItemCompTempPaymentReturn entity = base.AddNewEntity() as TransChargesItemCompTempPaymentReturn;
			
			return entity;
		}

		public TransChargesItemCompTempPaymentReturn FindByPrimaryKey(System.String transactionNo, System.String sequenceNo, System.String tariffComponentID, System.String intermBillNo, System.String paymentNo)
		{
			return base.FindByPrimaryKey(transactionNo, sequenceNo, tariffComponentID, intermBillNo, paymentNo) as TransChargesItemCompTempPaymentReturn;
		}


		#region IEnumerable<TransChargesItemCompTempPaymentReturn> Members

		IEnumerator<TransChargesItemCompTempPaymentReturn> IEnumerable<TransChargesItemCompTempPaymentReturn>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransChargesItemCompTempPaymentReturn;
			}
		}

		#endregion
		
		private TransChargesItemCompTempPaymentReturnQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransChargesItemCompTempPaymentReturn' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("TransChargesItemCompTempPaymentReturn ({TransactionNo},{SequenceNo},{TariffComponentID},{IntermBillNo},{PaymentNo})")]
	[Serializable]
	public partial class TransChargesItemCompTempPaymentReturn : esTransChargesItemCompTempPaymentReturn
	{
		public TransChargesItemCompTempPaymentReturn()
		{

		}
	
		public TransChargesItemCompTempPaymentReturn(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransChargesItemCompTempPaymentReturnMetadata.Meta();
			}
		}
		
		
		
		override protected esTransChargesItemCompTempPaymentReturnQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesItemCompTempPaymentReturnQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TransChargesItemCompTempPaymentReturnQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesItemCompTempPaymentReturnQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TransChargesItemCompTempPaymentReturnQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TransChargesItemCompTempPaymentReturnQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TransChargesItemCompTempPaymentReturnQuery : esTransChargesItemCompTempPaymentReturnQuery
	{
		public TransChargesItemCompTempPaymentReturnQuery()
		{

		}		
		
		public TransChargesItemCompTempPaymentReturnQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TransChargesItemCompTempPaymentReturnQuery";
        }
		
			
	}


	[Serializable]
	public partial class TransChargesItemCompTempPaymentReturnMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransChargesItemCompTempPaymentReturnMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemCompTempPaymentReturnMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemCompTempPaymentReturnMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.TariffComponentID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemCompTempPaymentReturnMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.IntermBillNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemCompTempPaymentReturnMetadata.PropertyNames.IntermBillNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.PaymentNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemCompTempPaymentReturnMetadata.PropertyNames.PaymentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.Price, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemCompTempPaymentReturnMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemCompTempPaymentReturnMetadata.ColumnNames.Discount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemCompTempPaymentReturnMetadata.PropertyNames.Discount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TransChargesItemCompTempPaymentReturnMetadata Meta()
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
			 public const string TariffComponentID = "TariffComponentID";
			 public const string IntermBillNo = "IntermBillNo";
			 public const string PaymentNo = "PaymentNo";
			 public const string Price = "Price";
			 public const string Discount = "Discount";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransactionNo = "TransactionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string IntermBillNo = "IntermBillNo";
			 public const string PaymentNo = "PaymentNo";
			 public const string Price = "Price";
			 public const string Discount = "Discount";
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
			lock (typeof(TransChargesItemCompTempPaymentReturnMetadata))
			{
				if(TransChargesItemCompTempPaymentReturnMetadata.mapDelegates == null)
				{
					TransChargesItemCompTempPaymentReturnMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransChargesItemCompTempPaymentReturnMetadata.meta == null)
				{
					TransChargesItemCompTempPaymentReturnMetadata.meta = new TransChargesItemCompTempPaymentReturnMetadata();
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
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IntermBillNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Discount", new esTypeMap("numeric", "System.Decimal"));			
				
				
				
				meta.Source = "TransChargesItemCompTempPaymentReturn";
				meta.Destination = "TransChargesItemCompTempPaymentReturn";
				
				meta.spInsert = "proc_TransChargesItemCompTempPaymentReturnInsert";				
				meta.spUpdate = "proc_TransChargesItemCompTempPaymentReturnUpdate";		
				meta.spDelete = "proc_TransChargesItemCompTempPaymentReturnDelete";
				meta.spLoadAll = "proc_TransChargesItemCompTempPaymentReturnLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransChargesItemCompTempPaymentReturnLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransChargesItemCompTempPaymentReturnMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
