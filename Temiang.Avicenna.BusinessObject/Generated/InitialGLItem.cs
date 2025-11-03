/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:16 PM
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
	abstract public class esInitialGLItemCollection : esEntityCollectionWAuditLog
	{
		public esInitialGLItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "InitialGLItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esInitialGLItemQuery query)
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
			this.InitQuery(query as esInitialGLItemQuery);
		}
		#endregion
		
		virtual public InitialGLItem DetachEntity(InitialGLItem entity)
		{
			return base.DetachEntity(entity) as InitialGLItem;
		}
		
		virtual public InitialGLItem AttachEntity(InitialGLItem entity)
		{
			return base.AttachEntity(entity) as InitialGLItem;
		}
		
		virtual public void Combine(InitialGLItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public InitialGLItem this[int index]
		{
			get
			{
				return base[index] as InitialGLItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(InitialGLItem);
		}
	}



	[Serializable]
	abstract public class esInitialGLItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esInitialGLItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esInitialGLItem()
		{

		}

		public esInitialGLItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String yearNo, System.String monthNo, System.String accountID, System.String sRAcctSubsidiary, System.String subsidiaryID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(yearNo, monthNo, accountID, sRAcctSubsidiary, subsidiaryID);
			else
				return LoadByPrimaryKeyStoredProcedure(yearNo, monthNo, accountID, sRAcctSubsidiary, subsidiaryID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String yearNo, System.String monthNo, System.String accountID, System.String sRAcctSubsidiary, System.String subsidiaryID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(yearNo, monthNo, accountID, sRAcctSubsidiary, subsidiaryID);
			else
				return LoadByPrimaryKeyStoredProcedure(yearNo, monthNo, accountID, sRAcctSubsidiary, subsidiaryID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String yearNo, System.String monthNo, System.String accountID, System.String sRAcctSubsidiary, System.String subsidiaryID)
		{
			esInitialGLItemQuery query = this.GetDynamicQuery();
			query.Where(query.YearNo == yearNo, query.MonthNo == monthNo, query.AccountID == accountID, query.SRAcctSubsidiary == sRAcctSubsidiary, query.SubsidiaryID == subsidiaryID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String yearNo, System.String monthNo, System.String accountID, System.String sRAcctSubsidiary, System.String subsidiaryID)
		{
			esParameters parms = new esParameters();
			parms.Add("YearNo",yearNo);			parms.Add("MonthNo",monthNo);			parms.Add("AccountID",accountID);			parms.Add("SRAcctSubsidiary",sRAcctSubsidiary);			parms.Add("SubsidiaryID",subsidiaryID);
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
						case "YearNo": this.str.YearNo = (string)value; break;							
						case "MonthNo": this.str.MonthNo = (string)value; break;							
						case "AccountID": this.str.AccountID = (string)value; break;							
						case "SRAcctSubsidiary": this.str.SRAcctSubsidiary = (string)value; break;							
						case "SubsidiaryID": this.str.SubsidiaryID = (string)value; break;							
						case "SRCurrency": this.str.SRCurrency = (string)value; break;							
						case "DebetAmount": this.str.DebetAmount = (string)value; break;							
						case "CreditAmount": this.str.CreditAmount = (string)value; break;							
						case "InitialRate": this.str.InitialRate = (string)value; break;							
						case "DebetConvert": this.str.DebetConvert = (string)value; break;							
						case "CreditConvert": this.str.CreditConvert = (string)value; break;							
						case "IsClosed": this.str.IsClosed = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "DebetAmount":
						
							if (value == null || value is System.Decimal)
								this.DebetAmount = (System.Decimal?)value;
							break;
						
						case "CreditAmount":
						
							if (value == null || value is System.Decimal)
								this.CreditAmount = (System.Decimal?)value;
							break;
						
						case "InitialRate":
						
							if (value == null || value is System.Decimal)
								this.InitialRate = (System.Decimal?)value;
							break;
						
						case "DebetConvert":
						
							if (value == null || value is System.Decimal)
								this.DebetConvert = (System.Decimal?)value;
							break;
						
						case "CreditConvert":
						
							if (value == null || value is System.Decimal)
								this.CreditConvert = (System.Decimal?)value;
							break;
						
						case "IsClosed":
						
							if (value == null || value is System.Boolean)
								this.IsClosed = (System.Boolean?)value;
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
		/// Maps to InitialGLItem.YearNo
		/// </summary>
		virtual public System.String YearNo
		{
			get
			{
				return base.GetSystemString(InitialGLItemMetadata.ColumnNames.YearNo);
			}
			
			set
			{
				base.SetSystemString(InitialGLItemMetadata.ColumnNames.YearNo, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGLItem.MonthNo
		/// </summary>
		virtual public System.String MonthNo
		{
			get
			{
				return base.GetSystemString(InitialGLItemMetadata.ColumnNames.MonthNo);
			}
			
			set
			{
				base.SetSystemString(InitialGLItemMetadata.ColumnNames.MonthNo, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGLItem.AccountID
		/// </summary>
		virtual public System.String AccountID
		{
			get
			{
				return base.GetSystemString(InitialGLItemMetadata.ColumnNames.AccountID);
			}
			
			set
			{
				base.SetSystemString(InitialGLItemMetadata.ColumnNames.AccountID, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGLItem.SRAcctSubsidiary
		/// </summary>
		virtual public System.String SRAcctSubsidiary
		{
			get
			{
				return base.GetSystemString(InitialGLItemMetadata.ColumnNames.SRAcctSubsidiary);
			}
			
			set
			{
				base.SetSystemString(InitialGLItemMetadata.ColumnNames.SRAcctSubsidiary, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGLItem.SubsidiaryID
		/// </summary>
		virtual public System.String SubsidiaryID
		{
			get
			{
				return base.GetSystemString(InitialGLItemMetadata.ColumnNames.SubsidiaryID);
			}
			
			set
			{
				base.SetSystemString(InitialGLItemMetadata.ColumnNames.SubsidiaryID, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGLItem.SRCurrency
		/// </summary>
		virtual public System.String SRCurrency
		{
			get
			{
				return base.GetSystemString(InitialGLItemMetadata.ColumnNames.SRCurrency);
			}
			
			set
			{
				base.SetSystemString(InitialGLItemMetadata.ColumnNames.SRCurrency, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGLItem.DebetAmount
		/// </summary>
		virtual public System.Decimal? DebetAmount
		{
			get
			{
				return base.GetSystemDecimal(InitialGLItemMetadata.ColumnNames.DebetAmount);
			}
			
			set
			{
				base.SetSystemDecimal(InitialGLItemMetadata.ColumnNames.DebetAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGLItem.CreditAmount
		/// </summary>
		virtual public System.Decimal? CreditAmount
		{
			get
			{
				return base.GetSystemDecimal(InitialGLItemMetadata.ColumnNames.CreditAmount);
			}
			
			set
			{
				base.SetSystemDecimal(InitialGLItemMetadata.ColumnNames.CreditAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGLItem.InitialRate
		/// </summary>
		virtual public System.Decimal? InitialRate
		{
			get
			{
				return base.GetSystemDecimal(InitialGLItemMetadata.ColumnNames.InitialRate);
			}
			
			set
			{
				base.SetSystemDecimal(InitialGLItemMetadata.ColumnNames.InitialRate, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGLItem.DebetConvert
		/// </summary>
		virtual public System.Decimal? DebetConvert
		{
			get
			{
				return base.GetSystemDecimal(InitialGLItemMetadata.ColumnNames.DebetConvert);
			}
			
			set
			{
				base.SetSystemDecimal(InitialGLItemMetadata.ColumnNames.DebetConvert, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGLItem.CreditConvert
		/// </summary>
		virtual public System.Decimal? CreditConvert
		{
			get
			{
				return base.GetSystemDecimal(InitialGLItemMetadata.ColumnNames.CreditConvert);
			}
			
			set
			{
				base.SetSystemDecimal(InitialGLItemMetadata.ColumnNames.CreditConvert, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGLItem.IsClosed
		/// </summary>
		virtual public System.Boolean? IsClosed
		{
			get
			{
				return base.GetSystemBoolean(InitialGLItemMetadata.ColumnNames.IsClosed);
			}
			
			set
			{
				base.SetSystemBoolean(InitialGLItemMetadata.ColumnNames.IsClosed, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGLItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(InitialGLItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(InitialGLItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGLItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(InitialGLItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(InitialGLItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esInitialGLItem entity)
			{
				this.entity = entity;
			}
			
	
			public System.String YearNo
			{
				get
				{
					System.String data = entity.YearNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.YearNo = null;
					else entity.YearNo = Convert.ToString(value);
				}
			}
				
			public System.String MonthNo
			{
				get
				{
					System.String data = entity.MonthNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MonthNo = null;
					else entity.MonthNo = Convert.ToString(value);
				}
			}
				
			public System.String AccountID
			{
				get
				{
					System.String data = entity.AccountID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AccountID = null;
					else entity.AccountID = Convert.ToString(value);
				}
			}
				
			public System.String SRAcctSubsidiary
			{
				get
				{
					System.String data = entity.SRAcctSubsidiary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAcctSubsidiary = null;
					else entity.SRAcctSubsidiary = Convert.ToString(value);
				}
			}
				
			public System.String SubsidiaryID
			{
				get
				{
					System.String data = entity.SubsidiaryID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubsidiaryID = null;
					else entity.SubsidiaryID = Convert.ToString(value);
				}
			}
				
			public System.String SRCurrency
			{
				get
				{
					System.String data = entity.SRCurrency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCurrency = null;
					else entity.SRCurrency = Convert.ToString(value);
				}
			}
				
			public System.String DebetAmount
			{
				get
				{
					System.Decimal? data = entity.DebetAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DebetAmount = null;
					else entity.DebetAmount = Convert.ToDecimal(value);
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
				
			public System.String InitialRate
			{
				get
				{
					System.Decimal? data = entity.InitialRate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InitialRate = null;
					else entity.InitialRate = Convert.ToDecimal(value);
				}
			}
				
			public System.String DebetConvert
			{
				get
				{
					System.Decimal? data = entity.DebetConvert;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DebetConvert = null;
					else entity.DebetConvert = Convert.ToDecimal(value);
				}
			}
				
			public System.String CreditConvert
			{
				get
				{
					System.Decimal? data = entity.CreditConvert;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreditConvert = null;
					else entity.CreditConvert = Convert.ToDecimal(value);
				}
			}
				
			public System.String IsClosed
			{
				get
				{
					System.Boolean? data = entity.IsClosed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClosed = null;
					else entity.IsClosed = Convert.ToBoolean(value);
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
			

			private esInitialGLItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esInitialGLItemQuery query)
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
				throw new Exception("esInitialGLItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class InitialGLItem : esInitialGLItem
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
	abstract public class esInitialGLItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return InitialGLItemMetadata.Meta();
			}
		}	
		

		public esQueryItem YearNo
		{
			get
			{
				return new esQueryItem(this, InitialGLItemMetadata.ColumnNames.YearNo, esSystemType.String);
			}
		} 
		
		public esQueryItem MonthNo
		{
			get
			{
				return new esQueryItem(this, InitialGLItemMetadata.ColumnNames.MonthNo, esSystemType.String);
			}
		} 
		
		public esQueryItem AccountID
		{
			get
			{
				return new esQueryItem(this, InitialGLItemMetadata.ColumnNames.AccountID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRAcctSubsidiary
		{
			get
			{
				return new esQueryItem(this, InitialGLItemMetadata.ColumnNames.SRAcctSubsidiary, esSystemType.String);
			}
		} 
		
		public esQueryItem SubsidiaryID
		{
			get
			{
				return new esQueryItem(this, InitialGLItemMetadata.ColumnNames.SubsidiaryID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRCurrency
		{
			get
			{
				return new esQueryItem(this, InitialGLItemMetadata.ColumnNames.SRCurrency, esSystemType.String);
			}
		} 
		
		public esQueryItem DebetAmount
		{
			get
			{
				return new esQueryItem(this, InitialGLItemMetadata.ColumnNames.DebetAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CreditAmount
		{
			get
			{
				return new esQueryItem(this, InitialGLItemMetadata.ColumnNames.CreditAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem InitialRate
		{
			get
			{
				return new esQueryItem(this, InitialGLItemMetadata.ColumnNames.InitialRate, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DebetConvert
		{
			get
			{
				return new esQueryItem(this, InitialGLItemMetadata.ColumnNames.DebetConvert, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CreditConvert
		{
			get
			{
				return new esQueryItem(this, InitialGLItemMetadata.ColumnNames.CreditConvert, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsClosed
		{
			get
			{
				return new esQueryItem(this, InitialGLItemMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, InitialGLItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, InitialGLItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("InitialGLItemCollection")]
	public partial class InitialGLItemCollection : esInitialGLItemCollection, IEnumerable<InitialGLItem>
	{
		public InitialGLItemCollection()
		{

		}
		
		public static implicit operator List<InitialGLItem>(InitialGLItemCollection coll)
		{
			List<InitialGLItem> list = new List<InitialGLItem>();
			
			foreach (InitialGLItem emp in coll)
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
				return  InitialGLItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InitialGLItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new InitialGLItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new InitialGLItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public InitialGLItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InitialGLItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(InitialGLItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public InitialGLItem AddNew()
		{
			InitialGLItem entity = base.AddNewEntity() as InitialGLItem;
			
			return entity;
		}

		public InitialGLItem FindByPrimaryKey(System.String yearNo, System.String monthNo, System.String accountID, System.String sRAcctSubsidiary, System.String subsidiaryID)
		{
			return base.FindByPrimaryKey(yearNo, monthNo, accountID, sRAcctSubsidiary, subsidiaryID) as InitialGLItem;
		}


		#region IEnumerable<InitialGLItem> Members

		IEnumerator<InitialGLItem> IEnumerable<InitialGLItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as InitialGLItem;
			}
		}

		#endregion
		
		private InitialGLItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'InitialGLItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("InitialGLItem ({YearNo},{MonthNo},{AccountID},{SRAcctSubsidiary},{SubsidiaryID})")]
	[Serializable]
	public partial class InitialGLItem : esInitialGLItem
	{
		public InitialGLItem()
		{

		}
	
		public InitialGLItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return InitialGLItemMetadata.Meta();
			}
		}
		
		
		
		override protected esInitialGLItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InitialGLItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public InitialGLItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InitialGLItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(InitialGLItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private InitialGLItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class InitialGLItemQuery : esInitialGLItemQuery
	{
		public InitialGLItemQuery()
		{

		}		
		
		public InitialGLItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "InitialGLItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class InitialGLItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected InitialGLItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(InitialGLItemMetadata.ColumnNames.YearNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = InitialGLItemMetadata.PropertyNames.YearNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 4;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLItemMetadata.ColumnNames.MonthNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = InitialGLItemMetadata.PropertyNames.MonthNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLItemMetadata.ColumnNames.AccountID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = InitialGLItemMetadata.PropertyNames.AccountID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLItemMetadata.ColumnNames.SRAcctSubsidiary, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = InitialGLItemMetadata.PropertyNames.SRAcctSubsidiary;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLItemMetadata.ColumnNames.SubsidiaryID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = InitialGLItemMetadata.PropertyNames.SubsidiaryID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLItemMetadata.ColumnNames.SRCurrency, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = InitialGLItemMetadata.PropertyNames.SRCurrency;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLItemMetadata.ColumnNames.DebetAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InitialGLItemMetadata.PropertyNames.DebetAmount;
			c.NumericPrecision = 20;
			c.NumericScale = 7;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLItemMetadata.ColumnNames.CreditAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InitialGLItemMetadata.PropertyNames.CreditAmount;
			c.NumericPrecision = 20;
			c.NumericScale = 7;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLItemMetadata.ColumnNames.InitialRate, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InitialGLItemMetadata.PropertyNames.InitialRate;
			c.NumericPrecision = 20;
			c.NumericScale = 7;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLItemMetadata.ColumnNames.DebetConvert, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InitialGLItemMetadata.PropertyNames.DebetConvert;
			c.NumericPrecision = 20;
			c.NumericScale = 7;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLItemMetadata.ColumnNames.CreditConvert, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InitialGLItemMetadata.PropertyNames.CreditConvert;
			c.NumericPrecision = 20;
			c.NumericScale = 7;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLItemMetadata.ColumnNames.IsClosed, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InitialGLItemMetadata.PropertyNames.IsClosed;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLItemMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InitialGLItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLItemMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = InitialGLItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public InitialGLItemMetadata Meta()
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
			 public const string YearNo = "YearNo";
			 public const string MonthNo = "MonthNo";
			 public const string AccountID = "AccountID";
			 public const string SRAcctSubsidiary = "SRAcctSubsidiary";
			 public const string SubsidiaryID = "SubsidiaryID";
			 public const string SRCurrency = "SRCurrency";
			 public const string DebetAmount = "DebetAmount";
			 public const string CreditAmount = "CreditAmount";
			 public const string InitialRate = "InitialRate";
			 public const string DebetConvert = "DebetConvert";
			 public const string CreditConvert = "CreditConvert";
			 public const string IsClosed = "IsClosed";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string YearNo = "YearNo";
			 public const string MonthNo = "MonthNo";
			 public const string AccountID = "AccountID";
			 public const string SRAcctSubsidiary = "SRAcctSubsidiary";
			 public const string SubsidiaryID = "SubsidiaryID";
			 public const string SRCurrency = "SRCurrency";
			 public const string DebetAmount = "DebetAmount";
			 public const string CreditAmount = "CreditAmount";
			 public const string InitialRate = "InitialRate";
			 public const string DebetConvert = "DebetConvert";
			 public const string CreditConvert = "CreditConvert";
			 public const string IsClosed = "IsClosed";
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
			lock (typeof(InitialGLItemMetadata))
			{
				if(InitialGLItemMetadata.mapDelegates == null)
				{
					InitialGLItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (InitialGLItemMetadata.meta == null)
				{
					InitialGLItemMetadata.meta = new InitialGLItemMetadata();
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
				

				meta.AddTypeMap("YearNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MonthNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AccountID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAcctSubsidiary", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SubsidiaryID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCurrency", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DebetAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreditAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("InitialRate", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("DebetConvert", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreditConvert", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "InitialGLItem";
				meta.Destination = "InitialGLItem";
				
				meta.spInsert = "proc_InitialGLItemInsert";				
				meta.spUpdate = "proc_InitialGLItemUpdate";		
				meta.spDelete = "proc_InitialGLItemDelete";
				meta.spLoadAll = "proc_InitialGLItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_InitialGLItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private InitialGLItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
