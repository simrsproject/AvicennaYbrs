/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/19/2015 2:35:37 PM
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
	abstract public class esTransChargesItemTempPaymentReturnCollection : esEntityCollectionWAuditLog
	{
		public esTransChargesItemTempPaymentReturnCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TransChargesItemTempPaymentReturnCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransChargesItemTempPaymentReturnQuery query)
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
			this.InitQuery(query as esTransChargesItemTempPaymentReturnQuery);
		}
		#endregion
		
		virtual public TransChargesItemTempPaymentReturn DetachEntity(TransChargesItemTempPaymentReturn entity)
		{
			return base.DetachEntity(entity) as TransChargesItemTempPaymentReturn;
		}
		
		virtual public TransChargesItemTempPaymentReturn AttachEntity(TransChargesItemTempPaymentReturn entity)
		{
			return base.AttachEntity(entity) as TransChargesItemTempPaymentReturn;
		}
		
		virtual public void Combine(TransChargesItemTempPaymentReturnCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransChargesItemTempPaymentReturn this[int index]
		{
			get
			{
				return base[index] as TransChargesItemTempPaymentReturn;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransChargesItemTempPaymentReturn);
		}
	}



	[Serializable]
	abstract public class esTransChargesItemTempPaymentReturn : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransChargesItemTempPaymentReturnQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransChargesItemTempPaymentReturn()
		{

		}

		public esTransChargesItemTempPaymentReturn(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String transactionNo, System.String sequenceNo, System.String intermBillNo, System.String paymentNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, intermBillNo, paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, intermBillNo, paymentNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo, System.String sequenceNo, System.String intermBillNo, System.String paymentNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, intermBillNo, paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, intermBillNo, paymentNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String transactionNo, System.String sequenceNo, System.String intermBillNo, System.String paymentNo)
		{
			esTransChargesItemTempPaymentReturnQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo, query.IntermBillNo == intermBillNo, query.PaymentNo == paymentNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo, System.String sequenceNo, System.String intermBillNo, System.String paymentNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);			parms.Add("SequenceNo",sequenceNo);			parms.Add("IntermBillNo",intermBillNo);			parms.Add("PaymentNo",paymentNo);
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
		/// Maps to TransChargesItemTempPaymentReturn.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemTempPaymentReturnMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemTempPaymentReturnMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemTempPaymentReturn.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemTempPaymentReturnMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemTempPaymentReturnMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemTempPaymentReturn.IntermBillNo
		/// </summary>
		virtual public System.String IntermBillNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemTempPaymentReturnMetadata.ColumnNames.IntermBillNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemTempPaymentReturnMetadata.ColumnNames.IntermBillNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemTempPaymentReturn.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemTempPaymentReturnMetadata.ColumnNames.PaymentNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemTempPaymentReturnMetadata.ColumnNames.PaymentNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemTempPaymentReturn.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemTempPaymentReturnMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemTempPaymentReturnMetadata.ColumnNames.Price, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemTempPaymentReturn.Discount
		/// </summary>
		virtual public System.Decimal? Discount
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemTempPaymentReturnMetadata.ColumnNames.Discount);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemTempPaymentReturnMetadata.ColumnNames.Discount, value);
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
			public esStrings(esTransChargesItemTempPaymentReturn entity)
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
			

			private esTransChargesItemTempPaymentReturn entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransChargesItemTempPaymentReturnQuery query)
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
				throw new Exception("esTransChargesItemTempPaymentReturn can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class TransChargesItemTempPaymentReturn : esTransChargesItemTempPaymentReturn
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
	abstract public class esTransChargesItemTempPaymentReturnQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TransChargesItemTempPaymentReturnMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemTempPaymentReturnMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemTempPaymentReturnMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem IntermBillNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemTempPaymentReturnMetadata.ColumnNames.IntermBillNo, esSystemType.String);
			}
		} 
		
		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemTempPaymentReturnMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, TransChargesItemTempPaymentReturnMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Discount
		{
			get
			{
				return new esQueryItem(this, TransChargesItemTempPaymentReturnMetadata.ColumnNames.Discount, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransChargesItemTempPaymentReturnCollection")]
	public partial class TransChargesItemTempPaymentReturnCollection : esTransChargesItemTempPaymentReturnCollection, IEnumerable<TransChargesItemTempPaymentReturn>
	{
		public TransChargesItemTempPaymentReturnCollection()
		{

		}
		
		public static implicit operator List<TransChargesItemTempPaymentReturn>(TransChargesItemTempPaymentReturnCollection coll)
		{
			List<TransChargesItemTempPaymentReturn> list = new List<TransChargesItemTempPaymentReturn>();
			
			foreach (TransChargesItemTempPaymentReturn emp in coll)
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
				return  TransChargesItemTempPaymentReturnMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesItemTempPaymentReturnQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransChargesItemTempPaymentReturn(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransChargesItemTempPaymentReturn();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TransChargesItemTempPaymentReturnQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesItemTempPaymentReturnQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TransChargesItemTempPaymentReturnQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public TransChargesItemTempPaymentReturn AddNew()
		{
			TransChargesItemTempPaymentReturn entity = base.AddNewEntity() as TransChargesItemTempPaymentReturn;
			
			return entity;
		}

		public TransChargesItemTempPaymentReturn FindByPrimaryKey(System.String transactionNo, System.String sequenceNo, System.String intermBillNo, System.String paymentNo)
		{
			return base.FindByPrimaryKey(transactionNo, sequenceNo, intermBillNo, paymentNo) as TransChargesItemTempPaymentReturn;
		}


		#region IEnumerable<TransChargesItemTempPaymentReturn> Members

		IEnumerator<TransChargesItemTempPaymentReturn> IEnumerable<TransChargesItemTempPaymentReturn>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransChargesItemTempPaymentReturn;
			}
		}

		#endregion
		
		private TransChargesItemTempPaymentReturnQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransChargesItemTempPaymentReturn' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("TransChargesItemTempPaymentReturn ({TransactionNo},{SequenceNo},{IntermBillNo},{PaymentNo})")]
	[Serializable]
	public partial class TransChargesItemTempPaymentReturn : esTransChargesItemTempPaymentReturn
	{
		public TransChargesItemTempPaymentReturn()
		{

		}
	
		public TransChargesItemTempPaymentReturn(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransChargesItemTempPaymentReturnMetadata.Meta();
			}
		}
		
		
		
		override protected esTransChargesItemTempPaymentReturnQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesItemTempPaymentReturnQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TransChargesItemTempPaymentReturnQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesItemTempPaymentReturnQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TransChargesItemTempPaymentReturnQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TransChargesItemTempPaymentReturnQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TransChargesItemTempPaymentReturnQuery : esTransChargesItemTempPaymentReturnQuery
	{
		public TransChargesItemTempPaymentReturnQuery()
		{

		}		
		
		public TransChargesItemTempPaymentReturnQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TransChargesItemTempPaymentReturnQuery";
        }
		
			
	}


	[Serializable]
	public partial class TransChargesItemTempPaymentReturnMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransChargesItemTempPaymentReturnMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransChargesItemTempPaymentReturnMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemTempPaymentReturnMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemTempPaymentReturnMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemTempPaymentReturnMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemTempPaymentReturnMetadata.ColumnNames.IntermBillNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemTempPaymentReturnMetadata.PropertyNames.IntermBillNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemTempPaymentReturnMetadata.ColumnNames.PaymentNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemTempPaymentReturnMetadata.PropertyNames.PaymentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemTempPaymentReturnMetadata.ColumnNames.Price, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemTempPaymentReturnMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemTempPaymentReturnMetadata.ColumnNames.Discount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemTempPaymentReturnMetadata.PropertyNames.Discount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TransChargesItemTempPaymentReturnMetadata Meta()
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
			lock (typeof(TransChargesItemTempPaymentReturnMetadata))
			{
				if(TransChargesItemTempPaymentReturnMetadata.mapDelegates == null)
				{
					TransChargesItemTempPaymentReturnMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransChargesItemTempPaymentReturnMetadata.meta == null)
				{
					TransChargesItemTempPaymentReturnMetadata.meta = new TransChargesItemTempPaymentReturnMetadata();
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
				meta.AddTypeMap("IntermBillNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Discount", new esTypeMap("numeric", "System.Decimal"));			
				
				
				
				meta.Source = "TransChargesItemTempPaymentReturn";
				meta.Destination = "TransChargesItemTempPaymentReturn";
				
				meta.spInsert = "proc_TransChargesItemTempPaymentReturnInsert";				
				meta.spUpdate = "proc_TransChargesItemTempPaymentReturnUpdate";		
				meta.spDelete = "proc_TransChargesItemTempPaymentReturnDelete";
				meta.spLoadAll = "proc_TransChargesItemTempPaymentReturnLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransChargesItemTempPaymentReturnLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransChargesItemTempPaymentReturnMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
