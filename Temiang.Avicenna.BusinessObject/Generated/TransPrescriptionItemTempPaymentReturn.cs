/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/19/2015 3:22:12 PM
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
	abstract public class esTransPrescriptionItemTempPaymentReturnCollection : esEntityCollectionWAuditLog
	{
		public esTransPrescriptionItemTempPaymentReturnCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TransPrescriptionItemTempPaymentReturnCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransPrescriptionItemTempPaymentReturnQuery query)
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
			this.InitQuery(query as esTransPrescriptionItemTempPaymentReturnQuery);
		}
		#endregion
		
		virtual public TransPrescriptionItemTempPaymentReturn DetachEntity(TransPrescriptionItemTempPaymentReturn entity)
		{
			return base.DetachEntity(entity) as TransPrescriptionItemTempPaymentReturn;
		}
		
		virtual public TransPrescriptionItemTempPaymentReturn AttachEntity(TransPrescriptionItemTempPaymentReturn entity)
		{
			return base.AttachEntity(entity) as TransPrescriptionItemTempPaymentReturn;
		}
		
		virtual public void Combine(TransPrescriptionItemTempPaymentReturnCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransPrescriptionItemTempPaymentReturn this[int index]
		{
			get
			{
				return base[index] as TransPrescriptionItemTempPaymentReturn;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransPrescriptionItemTempPaymentReturn);
		}
	}



	[Serializable]
	abstract public class esTransPrescriptionItemTempPaymentReturn : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransPrescriptionItemTempPaymentReturnQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransPrescriptionItemTempPaymentReturn()
		{

		}

		public esTransPrescriptionItemTempPaymentReturn(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String prescription, System.String sequenceNo, System.String intermBillNo, System.String paymentNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(prescription, sequenceNo, intermBillNo, paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(prescription, sequenceNo, intermBillNo, paymentNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String prescription, System.String sequenceNo, System.String intermBillNo, System.String paymentNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(prescription, sequenceNo, intermBillNo, paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(prescription, sequenceNo, intermBillNo, paymentNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String prescription, System.String sequenceNo, System.String intermBillNo, System.String paymentNo)
		{
			esTransPrescriptionItemTempPaymentReturnQuery query = this.GetDynamicQuery();
			query.Where(query.Prescription == prescription, query.SequenceNo == sequenceNo, query.IntermBillNo == intermBillNo, query.PaymentNo == paymentNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String prescription, System.String sequenceNo, System.String intermBillNo, System.String paymentNo)
		{
			esParameters parms = new esParameters();
			parms.Add("Prescription",prescription);			parms.Add("SequenceNo",sequenceNo);			parms.Add("IntermBillNo",intermBillNo);			parms.Add("PaymentNo",paymentNo);
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
						case "Prescription": this.str.Prescription = (string)value; break;							
						case "SequenceNo": this.str.SequenceNo = (string)value; break;							
						case "IntermBillNo": this.str.IntermBillNo = (string)value; break;							
						case "PaymentNo": this.str.PaymentNo = (string)value; break;							
						case "Price": this.str.Price = (string)value; break;							
						case "Discount": this.str.Discount = (string)value; break;							
						case "LineAmount": this.str.LineAmount = (string)value; break;
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
						
						case "LineAmount":
						
							if (value == null || value is System.Decimal)
								this.LineAmount = (System.Decimal?)value;
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
		/// Maps to TransPrescriptionItemTempPaymentReturn.Prescription
		/// </summary>
		virtual public System.String Prescription
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.Prescription);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.Prescription, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemTempPaymentReturn.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemTempPaymentReturn.IntermBillNo
		/// </summary>
		virtual public System.String IntermBillNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.IntermBillNo);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.IntermBillNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemTempPaymentReturn.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.PaymentNo);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.PaymentNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemTempPaymentReturn.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.Price, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemTempPaymentReturn.Discount
		/// </summary>
		virtual public System.Decimal? Discount
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.Discount);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.Discount, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemTempPaymentReturn.LineAmount
		/// </summary>
		virtual public System.Decimal? LineAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.LineAmount);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.LineAmount, value);
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
			public esStrings(esTransPrescriptionItemTempPaymentReturn entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Prescription
			{
				get
				{
					System.String data = entity.Prescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Prescription = null;
					else entity.Prescription = Convert.ToString(value);
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
				
			public System.String LineAmount
			{
				get
				{
					System.Decimal? data = entity.LineAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LineAmount = null;
					else entity.LineAmount = Convert.ToDecimal(value);
				}
			}
			

			private esTransPrescriptionItemTempPaymentReturn entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransPrescriptionItemTempPaymentReturnQuery query)
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
				throw new Exception("esTransPrescriptionItemTempPaymentReturn can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class TransPrescriptionItemTempPaymentReturn : esTransPrescriptionItemTempPaymentReturn
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
	abstract public class esTransPrescriptionItemTempPaymentReturnQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TransPrescriptionItemTempPaymentReturnMetadata.Meta();
			}
		}	
		

		public esQueryItem Prescription
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.Prescription, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem IntermBillNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.IntermBillNo, esSystemType.String);
			}
		} 
		
		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Discount
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.Discount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LineAmount
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.LineAmount, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransPrescriptionItemTempPaymentReturnCollection")]
	public partial class TransPrescriptionItemTempPaymentReturnCollection : esTransPrescriptionItemTempPaymentReturnCollection, IEnumerable<TransPrescriptionItemTempPaymentReturn>
	{
		public TransPrescriptionItemTempPaymentReturnCollection()
		{

		}
		
		public static implicit operator List<TransPrescriptionItemTempPaymentReturn>(TransPrescriptionItemTempPaymentReturnCollection coll)
		{
			List<TransPrescriptionItemTempPaymentReturn> list = new List<TransPrescriptionItemTempPaymentReturn>();
			
			foreach (TransPrescriptionItemTempPaymentReturn emp in coll)
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
				return  TransPrescriptionItemTempPaymentReturnMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPrescriptionItemTempPaymentReturnQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransPrescriptionItemTempPaymentReturn(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransPrescriptionItemTempPaymentReturn();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TransPrescriptionItemTempPaymentReturnQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPrescriptionItemTempPaymentReturnQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TransPrescriptionItemTempPaymentReturnQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public TransPrescriptionItemTempPaymentReturn AddNew()
		{
			TransPrescriptionItemTempPaymentReturn entity = base.AddNewEntity() as TransPrescriptionItemTempPaymentReturn;
			
			return entity;
		}

		public TransPrescriptionItemTempPaymentReturn FindByPrimaryKey(System.String prescription, System.String sequenceNo, System.String intermBillNo, System.String paymentNo)
		{
			return base.FindByPrimaryKey(prescription, sequenceNo, intermBillNo, paymentNo) as TransPrescriptionItemTempPaymentReturn;
		}


		#region IEnumerable<TransPrescriptionItemTempPaymentReturn> Members

		IEnumerator<TransPrescriptionItemTempPaymentReturn> IEnumerable<TransPrescriptionItemTempPaymentReturn>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransPrescriptionItemTempPaymentReturn;
			}
		}

		#endregion
		
		private TransPrescriptionItemTempPaymentReturnQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransPrescriptionItemTempPaymentReturn' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("TransPrescriptionItemTempPaymentReturn ({Prescription},{SequenceNo},{IntermBillNo},{PaymentNo})")]
	[Serializable]
	public partial class TransPrescriptionItemTempPaymentReturn : esTransPrescriptionItemTempPaymentReturn
	{
		public TransPrescriptionItemTempPaymentReturn()
		{

		}
	
		public TransPrescriptionItemTempPaymentReturn(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransPrescriptionItemTempPaymentReturnMetadata.Meta();
			}
		}
		
		
		
		override protected esTransPrescriptionItemTempPaymentReturnQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPrescriptionItemTempPaymentReturnQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TransPrescriptionItemTempPaymentReturnQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPrescriptionItemTempPaymentReturnQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TransPrescriptionItemTempPaymentReturnQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TransPrescriptionItemTempPaymentReturnQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TransPrescriptionItemTempPaymentReturnQuery : esTransPrescriptionItemTempPaymentReturnQuery
	{
		public TransPrescriptionItemTempPaymentReturnQuery()
		{

		}		
		
		public TransPrescriptionItemTempPaymentReturnQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TransPrescriptionItemTempPaymentReturnQuery";
        }
		
			
	}


	[Serializable]
	public partial class TransPrescriptionItemTempPaymentReturnMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransPrescriptionItemTempPaymentReturnMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.Prescription, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemTempPaymentReturnMetadata.PropertyNames.Prescription;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemTempPaymentReturnMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.IntermBillNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemTempPaymentReturnMetadata.PropertyNames.IntermBillNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.PaymentNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemTempPaymentReturnMetadata.PropertyNames.PaymentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.Price, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemTempPaymentReturnMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.Discount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemTempPaymentReturnMetadata.PropertyNames.Discount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemTempPaymentReturnMetadata.ColumnNames.LineAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemTempPaymentReturnMetadata.PropertyNames.LineAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TransPrescriptionItemTempPaymentReturnMetadata Meta()
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
			 public const string Prescription = "Prescription";
			 public const string SequenceNo = "SequenceNo";
			 public const string IntermBillNo = "IntermBillNo";
			 public const string PaymentNo = "PaymentNo";
			 public const string Price = "Price";
			 public const string Discount = "Discount";
			 public const string LineAmount = "LineAmount";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Prescription = "Prescription";
			 public const string SequenceNo = "SequenceNo";
			 public const string IntermBillNo = "IntermBillNo";
			 public const string PaymentNo = "PaymentNo";
			 public const string Price = "Price";
			 public const string Discount = "Discount";
			 public const string LineAmount = "LineAmount";
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
			lock (typeof(TransPrescriptionItemTempPaymentReturnMetadata))
			{
				if(TransPrescriptionItemTempPaymentReturnMetadata.mapDelegates == null)
				{
					TransPrescriptionItemTempPaymentReturnMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransPrescriptionItemTempPaymentReturnMetadata.meta == null)
				{
					TransPrescriptionItemTempPaymentReturnMetadata.meta = new TransPrescriptionItemTempPaymentReturnMetadata();
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
				

				meta.AddTypeMap("Prescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IntermBillNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Discount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LineAmount", new esTypeMap("numeric", "System.Decimal"));			
				
				
				
				meta.Source = "TransPrescriptionItemTempPaymentReturn";
				meta.Destination = "TransPrescriptionItemTempPaymentReturn";
				
				meta.spInsert = "proc_TransPrescriptionItemTempPaymentReturnInsert";				
				meta.spUpdate = "proc_TransPrescriptionItemTempPaymentReturnUpdate";		
				meta.spDelete = "proc_TransPrescriptionItemTempPaymentReturnDelete";
				meta.spLoadAll = "proc_TransPrescriptionItemTempPaymentReturnLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransPrescriptionItemTempPaymentReturnLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransPrescriptionItemTempPaymentReturnMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
