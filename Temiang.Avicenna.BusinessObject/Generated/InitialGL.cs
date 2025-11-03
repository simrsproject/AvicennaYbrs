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
	abstract public class esInitialGLCollection : esEntityCollectionWAuditLog
	{
		public esInitialGLCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "InitialGLCollection";
		}

		#region Query Logic
		protected void InitQuery(esInitialGLQuery query)
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
			this.InitQuery(query as esInitialGLQuery);
		}
		#endregion
		
		virtual public InitialGL DetachEntity(InitialGL entity)
		{
			return base.DetachEntity(entity) as InitialGL;
		}
		
		virtual public InitialGL AttachEntity(InitialGL entity)
		{
			return base.AttachEntity(entity) as InitialGL;
		}
		
		virtual public void Combine(InitialGLCollection collection)
		{
			base.Combine(collection);
		}
		
		new public InitialGL this[int index]
		{
			get
			{
				return base[index] as InitialGL;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(InitialGL);
		}
	}



	[Serializable]
	abstract public class esInitialGL : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esInitialGLQuery GetDynamicQuery()
		{
			return null;
		}

		public esInitialGL()
		{

		}

		public esInitialGL(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String yearNo, System.String monthNo, System.String accountID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(yearNo, monthNo, accountID);
			else
				return LoadByPrimaryKeyStoredProcedure(yearNo, monthNo, accountID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String yearNo, System.String monthNo, System.String accountID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(yearNo, monthNo, accountID);
			else
				return LoadByPrimaryKeyStoredProcedure(yearNo, monthNo, accountID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String yearNo, System.String monthNo, System.String accountID)
		{
			esInitialGLQuery query = this.GetDynamicQuery();
			query.Where(query.YearNo == yearNo, query.MonthNo == monthNo, query.AccountID == accountID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String yearNo, System.String monthNo, System.String accountID)
		{
			esParameters parms = new esParameters();
			parms.Add("YearNo",yearNo);			parms.Add("MonthNo",monthNo);			parms.Add("AccountID",accountID);
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
						case "SRAcctLevel": this.str.SRAcctLevel = (string)value; break;							
						case "SRAcctSubsidiary": this.str.SRAcctSubsidiary = (string)value; break;							
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
		/// Maps to InitialGL.YearNo
		/// </summary>
		virtual public System.String YearNo
		{
			get
			{
				return base.GetSystemString(InitialGLMetadata.ColumnNames.YearNo);
			}
			
			set
			{
				base.SetSystemString(InitialGLMetadata.ColumnNames.YearNo, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGL.MonthNo
		/// </summary>
		virtual public System.String MonthNo
		{
			get
			{
				return base.GetSystemString(InitialGLMetadata.ColumnNames.MonthNo);
			}
			
			set
			{
				base.SetSystemString(InitialGLMetadata.ColumnNames.MonthNo, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGL.AccountID
		/// </summary>
		virtual public System.String AccountID
		{
			get
			{
				return base.GetSystemString(InitialGLMetadata.ColumnNames.AccountID);
			}
			
			set
			{
				base.SetSystemString(InitialGLMetadata.ColumnNames.AccountID, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGL.SRAcctLevel
		/// </summary>
		virtual public System.String SRAcctLevel
		{
			get
			{
				return base.GetSystemString(InitialGLMetadata.ColumnNames.SRAcctLevel);
			}
			
			set
			{
				base.SetSystemString(InitialGLMetadata.ColumnNames.SRAcctLevel, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGL.SRAcctSubsidiary
		/// </summary>
		virtual public System.String SRAcctSubsidiary
		{
			get
			{
				return base.GetSystemString(InitialGLMetadata.ColumnNames.SRAcctSubsidiary);
			}
			
			set
			{
				base.SetSystemString(InitialGLMetadata.ColumnNames.SRAcctSubsidiary, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGL.SRCurrency
		/// </summary>
		virtual public System.String SRCurrency
		{
			get
			{
				return base.GetSystemString(InitialGLMetadata.ColumnNames.SRCurrency);
			}
			
			set
			{
				base.SetSystemString(InitialGLMetadata.ColumnNames.SRCurrency, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGL.DebetAmount
		/// </summary>
		virtual public System.Decimal? DebetAmount
		{
			get
			{
				return base.GetSystemDecimal(InitialGLMetadata.ColumnNames.DebetAmount);
			}
			
			set
			{
				base.SetSystemDecimal(InitialGLMetadata.ColumnNames.DebetAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGL.CreditAmount
		/// </summary>
		virtual public System.Decimal? CreditAmount
		{
			get
			{
				return base.GetSystemDecimal(InitialGLMetadata.ColumnNames.CreditAmount);
			}
			
			set
			{
				base.SetSystemDecimal(InitialGLMetadata.ColumnNames.CreditAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGL.InitialRate
		/// </summary>
		virtual public System.Decimal? InitialRate
		{
			get
			{
				return base.GetSystemDecimal(InitialGLMetadata.ColumnNames.InitialRate);
			}
			
			set
			{
				base.SetSystemDecimal(InitialGLMetadata.ColumnNames.InitialRate, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGL.DebetConvert
		/// </summary>
		virtual public System.Decimal? DebetConvert
		{
			get
			{
				return base.GetSystemDecimal(InitialGLMetadata.ColumnNames.DebetConvert);
			}
			
			set
			{
				base.SetSystemDecimal(InitialGLMetadata.ColumnNames.DebetConvert, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGL.CreditConvert
		/// </summary>
		virtual public System.Decimal? CreditConvert
		{
			get
			{
				return base.GetSystemDecimal(InitialGLMetadata.ColumnNames.CreditConvert);
			}
			
			set
			{
				base.SetSystemDecimal(InitialGLMetadata.ColumnNames.CreditConvert, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGL.IsClosed
		/// </summary>
		virtual public System.Boolean? IsClosed
		{
			get
			{
				return base.GetSystemBoolean(InitialGLMetadata.ColumnNames.IsClosed);
			}
			
			set
			{
				base.SetSystemBoolean(InitialGLMetadata.ColumnNames.IsClosed, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGL.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(InitialGLMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(InitialGLMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialGL.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(InitialGLMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(InitialGLMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esInitialGL entity)
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
				
			public System.String SRAcctLevel
			{
				get
				{
					System.String data = entity.SRAcctLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAcctLevel = null;
					else entity.SRAcctLevel = Convert.ToString(value);
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
			

			private esInitialGL entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esInitialGLQuery query)
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
				throw new Exception("esInitialGL can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class InitialGL : esInitialGL
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
	abstract public class esInitialGLQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return InitialGLMetadata.Meta();
			}
		}	
		

		public esQueryItem YearNo
		{
			get
			{
				return new esQueryItem(this, InitialGLMetadata.ColumnNames.YearNo, esSystemType.String);
			}
		} 
		
		public esQueryItem MonthNo
		{
			get
			{
				return new esQueryItem(this, InitialGLMetadata.ColumnNames.MonthNo, esSystemType.String);
			}
		} 
		
		public esQueryItem AccountID
		{
			get
			{
				return new esQueryItem(this, InitialGLMetadata.ColumnNames.AccountID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRAcctLevel
		{
			get
			{
				return new esQueryItem(this, InitialGLMetadata.ColumnNames.SRAcctLevel, esSystemType.String);
			}
		} 
		
		public esQueryItem SRAcctSubsidiary
		{
			get
			{
				return new esQueryItem(this, InitialGLMetadata.ColumnNames.SRAcctSubsidiary, esSystemType.String);
			}
		} 
		
		public esQueryItem SRCurrency
		{
			get
			{
				return new esQueryItem(this, InitialGLMetadata.ColumnNames.SRCurrency, esSystemType.String);
			}
		} 
		
		public esQueryItem DebetAmount
		{
			get
			{
				return new esQueryItem(this, InitialGLMetadata.ColumnNames.DebetAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CreditAmount
		{
			get
			{
				return new esQueryItem(this, InitialGLMetadata.ColumnNames.CreditAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem InitialRate
		{
			get
			{
				return new esQueryItem(this, InitialGLMetadata.ColumnNames.InitialRate, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DebetConvert
		{
			get
			{
				return new esQueryItem(this, InitialGLMetadata.ColumnNames.DebetConvert, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CreditConvert
		{
			get
			{
				return new esQueryItem(this, InitialGLMetadata.ColumnNames.CreditConvert, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsClosed
		{
			get
			{
				return new esQueryItem(this, InitialGLMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, InitialGLMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, InitialGLMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("InitialGLCollection")]
	public partial class InitialGLCollection : esInitialGLCollection, IEnumerable<InitialGL>
	{
		public InitialGLCollection()
		{

		}
		
		public static implicit operator List<InitialGL>(InitialGLCollection coll)
		{
			List<InitialGL> list = new List<InitialGL>();
			
			foreach (InitialGL emp in coll)
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
				return  InitialGLMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InitialGLQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new InitialGL(row);
		}

		override protected esEntity CreateEntity()
		{
			return new InitialGL();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public InitialGLQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InitialGLQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(InitialGLQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public InitialGL AddNew()
		{
			InitialGL entity = base.AddNewEntity() as InitialGL;
			
			return entity;
		}

		public InitialGL FindByPrimaryKey(System.String yearNo, System.String monthNo, System.String accountID)
		{
			return base.FindByPrimaryKey(yearNo, monthNo, accountID) as InitialGL;
		}


		#region IEnumerable<InitialGL> Members

		IEnumerator<InitialGL> IEnumerable<InitialGL>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as InitialGL;
			}
		}

		#endregion
		
		private InitialGLQuery query;
	}


	/// <summary>
	/// Encapsulates the 'InitialGL' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("InitialGL ({YearNo},{MonthNo},{AccountID})")]
	[Serializable]
	public partial class InitialGL : esInitialGL
	{
		public InitialGL()
		{

		}
	
		public InitialGL(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return InitialGLMetadata.Meta();
			}
		}
		
		
		
		override protected esInitialGLQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InitialGLQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public InitialGLQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InitialGLQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(InitialGLQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private InitialGLQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class InitialGLQuery : esInitialGLQuery
	{
		public InitialGLQuery()
		{

		}		
		
		public InitialGLQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "InitialGLQuery";
        }
		
			
	}


	[Serializable]
	public partial class InitialGLMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected InitialGLMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(InitialGLMetadata.ColumnNames.YearNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = InitialGLMetadata.PropertyNames.YearNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 4;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLMetadata.ColumnNames.MonthNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = InitialGLMetadata.PropertyNames.MonthNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLMetadata.ColumnNames.AccountID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = InitialGLMetadata.PropertyNames.AccountID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLMetadata.ColumnNames.SRAcctLevel, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = InitialGLMetadata.PropertyNames.SRAcctLevel;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLMetadata.ColumnNames.SRAcctSubsidiary, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = InitialGLMetadata.PropertyNames.SRAcctSubsidiary;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLMetadata.ColumnNames.SRCurrency, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = InitialGLMetadata.PropertyNames.SRCurrency;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLMetadata.ColumnNames.DebetAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InitialGLMetadata.PropertyNames.DebetAmount;
			c.NumericPrecision = 20;
			c.NumericScale = 7;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLMetadata.ColumnNames.CreditAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InitialGLMetadata.PropertyNames.CreditAmount;
			c.NumericPrecision = 20;
			c.NumericScale = 7;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLMetadata.ColumnNames.InitialRate, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InitialGLMetadata.PropertyNames.InitialRate;
			c.NumericPrecision = 20;
			c.NumericScale = 7;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLMetadata.ColumnNames.DebetConvert, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InitialGLMetadata.PropertyNames.DebetConvert;
			c.NumericPrecision = 20;
			c.NumericScale = 7;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLMetadata.ColumnNames.CreditConvert, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InitialGLMetadata.PropertyNames.CreditConvert;
			c.NumericPrecision = 20;
			c.NumericScale = 7;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLMetadata.ColumnNames.IsClosed, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InitialGLMetadata.PropertyNames.IsClosed;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InitialGLMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialGLMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = InitialGLMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public InitialGLMetadata Meta()
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
			 public const string SRAcctLevel = "SRAcctLevel";
			 public const string SRAcctSubsidiary = "SRAcctSubsidiary";
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
			 public const string SRAcctLevel = "SRAcctLevel";
			 public const string SRAcctSubsidiary = "SRAcctSubsidiary";
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
			lock (typeof(InitialGLMetadata))
			{
				if(InitialGLMetadata.mapDelegates == null)
				{
					InitialGLMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (InitialGLMetadata.meta == null)
				{
					InitialGLMetadata.meta = new InitialGLMetadata();
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
				meta.AddTypeMap("SRAcctLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAcctSubsidiary", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCurrency", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DebetAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreditAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("InitialRate", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("DebetConvert", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreditConvert", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "InitialGL";
				meta.Destination = "InitialGL";
				
				meta.spInsert = "proc_InitialGLInsert";				
				meta.spUpdate = "proc_InitialGLUpdate";		
				meta.spDelete = "proc_InitialGLDelete";
				meta.spLoadAll = "proc_InitialGLLoadAll";
				meta.spLoadByPrimaryKey = "proc_InitialGLLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private InitialGLMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
