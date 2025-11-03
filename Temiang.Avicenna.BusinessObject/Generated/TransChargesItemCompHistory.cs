/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/27/2012 12:50:44 PM
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
	abstract public class esTransChargesItemCompHistoryCollection : esEntityCollectionWAuditLog
	{
		public esTransChargesItemCompHistoryCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TransChargesItemCompHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransChargesItemCompHistoryQuery query)
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
			this.InitQuery(query as esTransChargesItemCompHistoryQuery);
		}
		#endregion
		
		virtual public TransChargesItemCompHistory DetachEntity(TransChargesItemCompHistory entity)
		{
			return base.DetachEntity(entity) as TransChargesItemCompHistory;
		}
		
		virtual public TransChargesItemCompHistory AttachEntity(TransChargesItemCompHistory entity)
		{
			return base.AttachEntity(entity) as TransChargesItemCompHistory;
		}
		
		virtual public void Combine(TransChargesItemCompHistoryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransChargesItemCompHistory this[int index]
		{
			get
			{
				return base[index] as TransChargesItemCompHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransChargesItemCompHistory);
		}
	}



	[Serializable]
	abstract public class esTransChargesItemCompHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransChargesItemCompHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransChargesItemCompHistory()
		{

		}

		public esTransChargesItemCompHistory(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String recalculationProcessNo, System.String transactionNo, System.String sequenceNo, System.String tariffComponentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(recalculationProcessNo, transactionNo, sequenceNo, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(recalculationProcessNo, transactionNo, sequenceNo, tariffComponentID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String recalculationProcessNo, System.String transactionNo, System.String sequenceNo, System.String tariffComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(recalculationProcessNo, transactionNo, sequenceNo, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(recalculationProcessNo, transactionNo, sequenceNo, tariffComponentID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String recalculationProcessNo, System.String transactionNo, System.String sequenceNo, System.String tariffComponentID)
		{
			esTransChargesItemCompHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.RecalculationProcessNo == recalculationProcessNo, query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo, query.TariffComponentID == tariffComponentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String recalculationProcessNo, System.String transactionNo, System.String sequenceNo, System.String tariffComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("RecalculationProcessNo",recalculationProcessNo);			parms.Add("TransactionNo",transactionNo);			parms.Add("SequenceNo",sequenceNo);			parms.Add("TariffComponentID",tariffComponentID);
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
						case "RecalculationProcessNo": this.str.RecalculationProcessNo = (string)value; break;							
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "SequenceNo": this.str.SequenceNo = (string)value; break;							
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;							
						case "Price": this.str.Price = (string)value; break;							
						case "DiscountAmount": this.str.DiscountAmount = (string)value; break;							
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "IsPackage": this.str.IsPackage = (string)value; break;							
						case "AutoProcessCalculation": this.str.AutoProcessCalculation = (string)value; break;
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
						
						case "DiscountAmount":
						
							if (value == null || value is System.Decimal)
								this.DiscountAmount = (System.Decimal?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "IsPackage":
						
							if (value == null || value is System.Boolean)
								this.IsPackage = (System.Boolean?)value;
							break;
						
						case "AutoProcessCalculation":
						
							if (value == null || value is System.Decimal)
								this.AutoProcessCalculation = (System.Decimal?)value;
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
		/// Maps to TransChargesItemCompHistory.RecalculationProcessNo
		/// </summary>
		virtual public System.String RecalculationProcessNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemCompHistoryMetadata.ColumnNames.RecalculationProcessNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemCompHistoryMetadata.ColumnNames.RecalculationProcessNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemCompHistory.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemCompHistoryMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemCompHistoryMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemCompHistory.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemCompHistoryMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemCompHistoryMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemCompHistory.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(TransChargesItemCompHistoryMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemCompHistoryMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemCompHistory.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemCompHistoryMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemCompHistoryMetadata.ColumnNames.Price, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemCompHistory.DiscountAmount
		/// </summary>
		virtual public System.Decimal? DiscountAmount
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemCompHistoryMetadata.ColumnNames.DiscountAmount);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemCompHistoryMetadata.ColumnNames.DiscountAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemCompHistory.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(TransChargesItemCompHistoryMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemCompHistoryMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemCompHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransChargesItemCompHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransChargesItemCompHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemCompHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransChargesItemCompHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemCompHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemCompHistory.IsPackage
		/// </summary>
		virtual public System.Boolean? IsPackage
		{
			get
			{
				return base.GetSystemBoolean(TransChargesItemCompHistoryMetadata.ColumnNames.IsPackage);
			}
			
			set
			{
				base.SetSystemBoolean(TransChargesItemCompHistoryMetadata.ColumnNames.IsPackage, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemCompHistory.AutoProcessCalculation
		/// </summary>
		virtual public System.Decimal? AutoProcessCalculation
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemCompHistoryMetadata.ColumnNames.AutoProcessCalculation);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemCompHistoryMetadata.ColumnNames.AutoProcessCalculation, value);
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
			public esStrings(esTransChargesItemCompHistory entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RecalculationProcessNo
			{
				get
				{
					System.String data = entity.RecalculationProcessNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecalculationProcessNo = null;
					else entity.RecalculationProcessNo = Convert.ToString(value);
				}
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
				
			public System.String DiscountAmount
			{
				get
				{
					System.Decimal? data = entity.DiscountAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscountAmount = null;
					else entity.DiscountAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
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
				
			public System.String IsPackage
			{
				get
				{
					System.Boolean? data = entity.IsPackage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPackage = null;
					else entity.IsPackage = Convert.ToBoolean(value);
				}
			}
				
			public System.String AutoProcessCalculation
			{
				get
				{
					System.Decimal? data = entity.AutoProcessCalculation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AutoProcessCalculation = null;
					else entity.AutoProcessCalculation = Convert.ToDecimal(value);
				}
			}
			

			private esTransChargesItemCompHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransChargesItemCompHistoryQuery query)
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
				throw new Exception("esTransChargesItemCompHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class TransChargesItemCompHistory : esTransChargesItemCompHistory
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
	abstract public class esTransChargesItemCompHistoryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TransChargesItemCompHistoryMetadata.Meta();
			}
		}	
		

		public esQueryItem RecalculationProcessNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemCompHistoryMetadata.ColumnNames.RecalculationProcessNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemCompHistoryMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemCompHistoryMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, TransChargesItemCompHistoryMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, TransChargesItemCompHistoryMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DiscountAmount
		{
			get
			{
				return new esQueryItem(this, TransChargesItemCompHistoryMetadata.ColumnNames.DiscountAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, TransChargesItemCompHistoryMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransChargesItemCompHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransChargesItemCompHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsPackage
		{
			get
			{
				return new esQueryItem(this, TransChargesItemCompHistoryMetadata.ColumnNames.IsPackage, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem AutoProcessCalculation
		{
			get
			{
				return new esQueryItem(this, TransChargesItemCompHistoryMetadata.ColumnNames.AutoProcessCalculation, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransChargesItemCompHistoryCollection")]
	public partial class TransChargesItemCompHistoryCollection : esTransChargesItemCompHistoryCollection, IEnumerable<TransChargesItemCompHistory>
	{
		public TransChargesItemCompHistoryCollection()
		{

		}
		
		public static implicit operator List<TransChargesItemCompHistory>(TransChargesItemCompHistoryCollection coll)
		{
			List<TransChargesItemCompHistory> list = new List<TransChargesItemCompHistory>();
			
			foreach (TransChargesItemCompHistory emp in coll)
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
				return  TransChargesItemCompHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesItemCompHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransChargesItemCompHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransChargesItemCompHistory();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TransChargesItemCompHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesItemCompHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TransChargesItemCompHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public TransChargesItemCompHistory AddNew()
		{
			TransChargesItemCompHistory entity = base.AddNewEntity() as TransChargesItemCompHistory;
			
			return entity;
		}

		public TransChargesItemCompHistory FindByPrimaryKey(System.String recalculationProcessNo, System.String transactionNo, System.String sequenceNo, System.String tariffComponentID)
		{
			return base.FindByPrimaryKey(recalculationProcessNo, transactionNo, sequenceNo, tariffComponentID) as TransChargesItemCompHistory;
		}


		#region IEnumerable<TransChargesItemCompHistory> Members

		IEnumerator<TransChargesItemCompHistory> IEnumerable<TransChargesItemCompHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransChargesItemCompHistory;
			}
		}

		#endregion
		
		private TransChargesItemCompHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransChargesItemCompHistory' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("TransChargesItemCompHistory ({RecalculationProcessNo},{TransactionNo},{SequenceNo},{TariffComponentID})")]
	[Serializable]
	public partial class TransChargesItemCompHistory : esTransChargesItemCompHistory
	{
		public TransChargesItemCompHistory()
		{

		}
	
		public TransChargesItemCompHistory(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransChargesItemCompHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esTransChargesItemCompHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesItemCompHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TransChargesItemCompHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesItemCompHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TransChargesItemCompHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TransChargesItemCompHistoryQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TransChargesItemCompHistoryQuery : esTransChargesItemCompHistoryQuery
	{
		public TransChargesItemCompHistoryQuery()
		{

		}		
		
		public TransChargesItemCompHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TransChargesItemCompHistoryQuery";
        }
		
			
	}


	[Serializable]
	public partial class TransChargesItemCompHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransChargesItemCompHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransChargesItemCompHistoryMetadata.ColumnNames.RecalculationProcessNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemCompHistoryMetadata.PropertyNames.RecalculationProcessNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemCompHistoryMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemCompHistoryMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemCompHistoryMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemCompHistoryMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 7;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemCompHistoryMetadata.ColumnNames.TariffComponentID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemCompHistoryMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemCompHistoryMetadata.ColumnNames.Price, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemCompHistoryMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemCompHistoryMetadata.ColumnNames.DiscountAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemCompHistoryMetadata.PropertyNames.DiscountAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemCompHistoryMetadata.ColumnNames.ParamedicID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemCompHistoryMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemCompHistoryMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesItemCompHistoryMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemCompHistoryMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemCompHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemCompHistoryMetadata.ColumnNames.IsPackage, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesItemCompHistoryMetadata.PropertyNames.IsPackage;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemCompHistoryMetadata.ColumnNames.AutoProcessCalculation, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemCompHistoryMetadata.PropertyNames.AutoProcessCalculation;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TransChargesItemCompHistoryMetadata Meta()
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
			 public const string RecalculationProcessNo = "RecalculationProcessNo";
			 public const string TransactionNo = "TransactionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string Price = "Price";
			 public const string DiscountAmount = "DiscountAmount";
			 public const string ParamedicID = "ParamedicID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsPackage = "IsPackage";
			 public const string AutoProcessCalculation = "AutoProcessCalculation";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RecalculationProcessNo = "RecalculationProcessNo";
			 public const string TransactionNo = "TransactionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string Price = "Price";
			 public const string DiscountAmount = "DiscountAmount";
			 public const string ParamedicID = "ParamedicID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsPackage = "IsPackage";
			 public const string AutoProcessCalculation = "AutoProcessCalculation";
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
			lock (typeof(TransChargesItemCompHistoryMetadata))
			{
				if(TransChargesItemCompHistoryMetadata.mapDelegates == null)
				{
					TransChargesItemCompHistoryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransChargesItemCompHistoryMetadata.meta == null)
				{
					TransChargesItemCompHistoryMetadata.meta = new TransChargesItemCompHistoryMetadata();
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
				

				meta.AddTypeMap("RecalculationProcessNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DiscountAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPackage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AutoProcessCalculation", new esTypeMap("numeric", "System.Decimal"));			
				
				
				
				meta.Source = "TransChargesItemCompHistory";
				meta.Destination = "TransChargesItemCompHistory";
				
				meta.spInsert = "proc_TransChargesItemCompHistoryInsert";				
				meta.spUpdate = "proc_TransChargesItemCompHistoryUpdate";		
				meta.spDelete = "proc_TransChargesItemCompHistoryDelete";
				meta.spLoadAll = "proc_TransChargesItemCompHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransChargesItemCompHistoryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransChargesItemCompHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
