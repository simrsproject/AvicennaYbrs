/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/8/2021 2:59:53 PM
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
	abstract public class esChartOfAccountsCollection : esEntityCollectionWAuditLog
	{
		public esChartOfAccountsCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ChartOfAccountsCollection";
		}

		#region Query Logic
		protected void InitQuery(esChartOfAccountsQuery query)
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
			this.InitQuery(query as esChartOfAccountsQuery);
		}
		#endregion
		
		virtual public ChartOfAccounts DetachEntity(ChartOfAccounts entity)
		{
			return base.DetachEntity(entity) as ChartOfAccounts;
		}
		
		virtual public ChartOfAccounts AttachEntity(ChartOfAccounts entity)
		{
			return base.AttachEntity(entity) as ChartOfAccounts;
		}
		
		virtual public void Combine(ChartOfAccountsCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ChartOfAccounts this[int index]
		{
			get
			{
				return base[index] as ChartOfAccounts;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ChartOfAccounts);
		}
	}



	[Serializable]
	abstract public class esChartOfAccounts : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esChartOfAccountsQuery GetDynamicQuery()
		{
			return null;
		}

		public esChartOfAccounts()
		{

		}

		public esChartOfAccounts(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 chartOfAccountId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(chartOfAccountId);
			else
				return LoadByPrimaryKeyStoredProcedure(chartOfAccountId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 chartOfAccountId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(chartOfAccountId);
			else
				return LoadByPrimaryKeyStoredProcedure(chartOfAccountId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 chartOfAccountId)
		{
			esChartOfAccountsQuery query = this.GetDynamicQuery();
			query.Where(query.ChartOfAccountId == chartOfAccountId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 chartOfAccountId)
		{
			esParameters parms = new esParameters();
			parms.Add("ChartOfAccountId",chartOfAccountId);
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
						case "ChartOfAccountCode": this.str.ChartOfAccountCode = (string)value; break;							
						case "ChartOfAccountName": this.str.ChartOfAccountName = (string)value; break;							
						case "IsDetail": this.str.IsDetail = (string)value; break;							
						case "AccountLevel": this.str.AccountLevel = (string)value; break;							
						case "GeneralAccount": this.str.GeneralAccount = (string)value; break;							
						case "NormalBalance": this.str.NormalBalance = (string)value; break;							
						case "AccountGroup": this.str.AccountGroup = (string)value; break;							
						case "SubLedgerId": this.str.SubLedgerId = (string)value; break;							
						case "IsDocumenNumberEnabled": this.str.IsDocumenNumberEnabled = (string)value; break;							
						case "TreeCode": this.str.TreeCode = (string)value; break;							
						case "DateCreated": this.str.DateCreated = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "CreatedBy": this.str.CreatedBy = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "IsControlDocNumber": this.str.IsControlDocNumber = (string)value; break;							
						case "Note": this.str.Note = (string)value; break;							
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;							
						case "IsReconcile": this.str.IsReconcile = (string)value; break;							
						case "BkuAccountID": this.str.BkuAccountID = (string)value; break;							
						case "IsBku": this.str.IsBku = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsDetail":
						
							if (value == null || value is System.Boolean)
								this.IsDetail = (System.Boolean?)value;
							break;
						
						case "AccountLevel":
						
							if (value == null || value is System.Int32)
								this.AccountLevel = (System.Int32?)value;
							break;
						
						case "AccountGroup":
						
							if (value == null || value is System.Int32)
								this.AccountGroup = (System.Int32?)value;
							break;
						
						case "SubLedgerId":
						
							if (value == null || value is System.Int32)
								this.SubLedgerId = (System.Int32?)value;
							break;
						
						case "IsDocumenNumberEnabled":
						
							if (value == null || value is System.Boolean)
								this.IsDocumenNumberEnabled = (System.Boolean?)value;
							break;
						
						case "DateCreated":
						
							if (value == null || value is System.DateTime)
								this.DateCreated = (System.DateTime?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						
						case "IsControlDocNumber":
						
							if (value == null || value is System.Boolean)
								this.IsControlDocNumber = (System.Boolean?)value;
							break;
						
						case "ChartOfAccountId":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
							break;
						
						case "IsReconcile":
						
							if (value == null || value is System.Boolean)
								this.IsReconcile = (System.Boolean?)value;
							break;
						
						case "BkuAccountID":
						
							if (value == null || value is System.Int32)
								this.BkuAccountID = (System.Int32?)value;
							break;
						
						case "IsBku":
						
							if (value == null || value is System.Boolean)
								this.IsBku = (System.Boolean?)value;
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
		/// Maps to ChartOfAccounts.ChartOfAccountCode
		/// </summary>
		virtual public System.String ChartOfAccountCode
		{
			get
			{
				return base.GetSystemString(ChartOfAccountsMetadata.ColumnNames.ChartOfAccountCode);
			}
			
			set
			{
				base.SetSystemString(ChartOfAccountsMetadata.ColumnNames.ChartOfAccountCode, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.ChartOfAccountName
		/// </summary>
		virtual public System.String ChartOfAccountName
		{
			get
			{
				return base.GetSystemString(ChartOfAccountsMetadata.ColumnNames.ChartOfAccountName);
			}
			
			set
			{
				base.SetSystemString(ChartOfAccountsMetadata.ColumnNames.ChartOfAccountName, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.IsDetail
		/// </summary>
		virtual public System.Boolean? IsDetail
		{
			get
			{
				return base.GetSystemBoolean(ChartOfAccountsMetadata.ColumnNames.IsDetail);
			}
			
			set
			{
				base.SetSystemBoolean(ChartOfAccountsMetadata.ColumnNames.IsDetail, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.AccountLevel
		/// </summary>
		virtual public System.Int32? AccountLevel
		{
			get
			{
				return base.GetSystemInt32(ChartOfAccountsMetadata.ColumnNames.AccountLevel);
			}
			
			set
			{
				base.SetSystemInt32(ChartOfAccountsMetadata.ColumnNames.AccountLevel, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.GeneralAccount
		/// </summary>
		virtual public System.String GeneralAccount
		{
			get
			{
				return base.GetSystemString(ChartOfAccountsMetadata.ColumnNames.GeneralAccount);
			}
			
			set
			{
				base.SetSystemString(ChartOfAccountsMetadata.ColumnNames.GeneralAccount, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.NormalBalance
		/// </summary>
		virtual public System.String NormalBalance
		{
			get
			{
				return base.GetSystemString(ChartOfAccountsMetadata.ColumnNames.NormalBalance);
			}
			
			set
			{
				base.SetSystemString(ChartOfAccountsMetadata.ColumnNames.NormalBalance, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.AccountGroup
		/// </summary>
		virtual public System.Int32? AccountGroup
		{
			get
			{
				return base.GetSystemInt32(ChartOfAccountsMetadata.ColumnNames.AccountGroup);
			}
			
			set
			{
				base.SetSystemInt32(ChartOfAccountsMetadata.ColumnNames.AccountGroup, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.SubLedgerId
		/// </summary>
		virtual public System.Int32? SubLedgerId
		{
			get
			{
				return base.GetSystemInt32(ChartOfAccountsMetadata.ColumnNames.SubLedgerId);
			}
			
			set
			{
				base.SetSystemInt32(ChartOfAccountsMetadata.ColumnNames.SubLedgerId, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.IsDocumenNumberEnabled
		/// </summary>
		virtual public System.Boolean? IsDocumenNumberEnabled
		{
			get
			{
				return base.GetSystemBoolean(ChartOfAccountsMetadata.ColumnNames.IsDocumenNumberEnabled);
			}
			
			set
			{
				base.SetSystemBoolean(ChartOfAccountsMetadata.ColumnNames.IsDocumenNumberEnabled, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.TreeCode
		/// </summary>
		virtual public System.String TreeCode
		{
			get
			{
				return base.GetSystemString(ChartOfAccountsMetadata.ColumnNames.TreeCode);
			}
			
			set
			{
				base.SetSystemString(ChartOfAccountsMetadata.ColumnNames.TreeCode, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.DateCreated
		/// </summary>
		virtual public System.DateTime? DateCreated
		{
			get
			{
				return base.GetSystemDateTime(ChartOfAccountsMetadata.ColumnNames.DateCreated);
			}
			
			set
			{
				base.SetSystemDateTime(ChartOfAccountsMetadata.ColumnNames.DateCreated, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ChartOfAccountsMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ChartOfAccountsMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.CreatedBy
		/// </summary>
		virtual public System.String CreatedBy
		{
			get
			{
				return base.GetSystemString(ChartOfAccountsMetadata.ColumnNames.CreatedBy);
			}
			
			set
			{
				base.SetSystemString(ChartOfAccountsMetadata.ColumnNames.CreatedBy, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ChartOfAccountsMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ChartOfAccountsMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ChartOfAccountsMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(ChartOfAccountsMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.isControlDocNumber
		/// </summary>
		virtual public System.Boolean? IsControlDocNumber
		{
			get
			{
				return base.GetSystemBoolean(ChartOfAccountsMetadata.ColumnNames.IsControlDocNumber);
			}
			
			set
			{
				base.SetSystemBoolean(ChartOfAccountsMetadata.ColumnNames.IsControlDocNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(ChartOfAccountsMetadata.ColumnNames.Note);
			}
			
			set
			{
				base.SetSystemString(ChartOfAccountsMetadata.ColumnNames.Note, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(ChartOfAccountsMetadata.ColumnNames.ChartOfAccountId);
			}
			
			set
			{
				base.SetSystemInt32(ChartOfAccountsMetadata.ColumnNames.ChartOfAccountId, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.IsReconcile
		/// </summary>
		virtual public System.Boolean? IsReconcile
		{
			get
			{
				return base.GetSystemBoolean(ChartOfAccountsMetadata.ColumnNames.IsReconcile);
			}
			
			set
			{
				base.SetSystemBoolean(ChartOfAccountsMetadata.ColumnNames.IsReconcile, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.BkuAccountID
		/// </summary>
		virtual public System.Int32? BkuAccountID
		{
			get
			{
				return base.GetSystemInt32(ChartOfAccountsMetadata.ColumnNames.BkuAccountID);
			}
			
			set
			{
				base.SetSystemInt32(ChartOfAccountsMetadata.ColumnNames.BkuAccountID, value);
			}
		}
		
		/// <summary>
		/// Maps to ChartOfAccounts.IsBku
		/// </summary>
		virtual public System.Boolean? IsBku
		{
			get
			{
				return base.GetSystemBoolean(ChartOfAccountsMetadata.ColumnNames.IsBku);
			}
			
			set
			{
				base.SetSystemBoolean(ChartOfAccountsMetadata.ColumnNames.IsBku, value);
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
			public esStrings(esChartOfAccounts entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ChartOfAccountCode
			{
				get
				{
					System.String data = entity.ChartOfAccountCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountCode = null;
					else entity.ChartOfAccountCode = Convert.ToString(value);
				}
			}
				
			public System.String ChartOfAccountName
			{
				get
				{
					System.String data = entity.ChartOfAccountName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountName = null;
					else entity.ChartOfAccountName = Convert.ToString(value);
				}
			}
				
			public System.String IsDetail
			{
				get
				{
					System.Boolean? data = entity.IsDetail;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDetail = null;
					else entity.IsDetail = Convert.ToBoolean(value);
				}
			}
				
			public System.String AccountLevel
			{
				get
				{
					System.Int32? data = entity.AccountLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AccountLevel = null;
					else entity.AccountLevel = Convert.ToInt32(value);
				}
			}
				
			public System.String GeneralAccount
			{
				get
				{
					System.String data = entity.GeneralAccount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GeneralAccount = null;
					else entity.GeneralAccount = Convert.ToString(value);
				}
			}
				
			public System.String NormalBalance
			{
				get
				{
					System.String data = entity.NormalBalance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NormalBalance = null;
					else entity.NormalBalance = Convert.ToString(value);
				}
			}
				
			public System.String AccountGroup
			{
				get
				{
					System.Int32? data = entity.AccountGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AccountGroup = null;
					else entity.AccountGroup = Convert.ToInt32(value);
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
				
			public System.String IsDocumenNumberEnabled
			{
				get
				{
					System.Boolean? data = entity.IsDocumenNumberEnabled;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDocumenNumberEnabled = null;
					else entity.IsDocumenNumberEnabled = Convert.ToBoolean(value);
				}
			}
				
			public System.String TreeCode
			{
				get
				{
					System.String data = entity.TreeCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TreeCode = null;
					else entity.TreeCode = Convert.ToString(value);
				}
			}
				
			public System.String DateCreated
			{
				get
				{
					System.DateTime? data = entity.DateCreated;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateCreated = null;
					else entity.DateCreated = Convert.ToDateTime(value);
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
				
			public System.String CreatedBy
			{
				get
				{
					System.String data = entity.CreatedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedBy = null;
					else entity.CreatedBy = Convert.ToString(value);
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
				
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsControlDocNumber
			{
				get
				{
					System.Boolean? data = entity.IsControlDocNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsControlDocNumber = null;
					else entity.IsControlDocNumber = Convert.ToBoolean(value);
				}
			}
				
			public System.String Note
			{
				get
				{
					System.String data = entity.Note;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Note = null;
					else entity.Note = Convert.ToString(value);
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
				
			public System.String IsReconcile
			{
				get
				{
					System.Boolean? data = entity.IsReconcile;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReconcile = null;
					else entity.IsReconcile = Convert.ToBoolean(value);
				}
			}
				
			public System.String BkuAccountID
			{
				get
				{
					System.Int32? data = entity.BkuAccountID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BkuAccountID = null;
					else entity.BkuAccountID = Convert.ToInt32(value);
				}
			}
				
			public System.String IsBku
			{
				get
				{
					System.Boolean? data = entity.IsBku;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBku = null;
					else entity.IsBku = Convert.ToBoolean(value);
				}
			}
			

			private esChartOfAccounts entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esChartOfAccountsQuery query)
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
				throw new Exception("esChartOfAccounts can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esChartOfAccountsQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ChartOfAccountsMetadata.Meta();
			}
		}	
		

		public esQueryItem ChartOfAccountCode
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.ChartOfAccountCode, esSystemType.String);
			}
		} 
		
		public esQueryItem ChartOfAccountName
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.ChartOfAccountName, esSystemType.String);
			}
		} 
		
		public esQueryItem IsDetail
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.IsDetail, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem AccountLevel
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.AccountLevel, esSystemType.Int32);
			}
		} 
		
		public esQueryItem GeneralAccount
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.GeneralAccount, esSystemType.String);
			}
		} 
		
		public esQueryItem NormalBalance
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.NormalBalance, esSystemType.String);
			}
		} 
		
		public esQueryItem AccountGroup
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.AccountGroup, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubLedgerId
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.SubLedgerId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IsDocumenNumberEnabled
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.IsDocumenNumberEnabled, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem TreeCode
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.TreeCode, esSystemType.String);
			}
		} 
		
		public esQueryItem DateCreated
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.DateCreated, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem CreatedBy
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.CreatedBy, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsControlDocNumber
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.IsControlDocNumber, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.Note, esSystemType.String);
			}
		} 
		
		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IsReconcile
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.IsReconcile, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem BkuAccountID
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.BkuAccountID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IsBku
		{
			get
			{
				return new esQueryItem(this, ChartOfAccountsMetadata.ColumnNames.IsBku, esSystemType.Boolean);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ChartOfAccountsCollection")]
	public partial class ChartOfAccountsCollection : esChartOfAccountsCollection, IEnumerable<ChartOfAccounts>
	{
		public ChartOfAccountsCollection()
		{

		}
		
		public static implicit operator List<ChartOfAccounts>(ChartOfAccountsCollection coll)
		{
			List<ChartOfAccounts> list = new List<ChartOfAccounts>();
			
			foreach (ChartOfAccounts emp in coll)
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
				return  ChartOfAccountsMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ChartOfAccountsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ChartOfAccounts(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ChartOfAccounts();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ChartOfAccountsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ChartOfAccountsQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ChartOfAccountsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ChartOfAccounts AddNew()
		{
			ChartOfAccounts entity = base.AddNewEntity() as ChartOfAccounts;
			
			return entity;
		}

		public ChartOfAccounts FindByPrimaryKey(System.Int32 chartOfAccountId)
		{
			return base.FindByPrimaryKey(chartOfAccountId) as ChartOfAccounts;
		}


		#region IEnumerable<ChartOfAccounts> Members

		IEnumerator<ChartOfAccounts> IEnumerable<ChartOfAccounts>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ChartOfAccounts;
			}
		}

		#endregion
		
		private ChartOfAccountsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ChartOfAccounts' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ChartOfAccounts ({ChartOfAccountId})")]
	[Serializable]
	public partial class ChartOfAccounts : esChartOfAccounts
	{
		public ChartOfAccounts()
		{

		}
	
		public ChartOfAccounts(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ChartOfAccountsMetadata.Meta();
			}
		}
		
		
		
		override protected esChartOfAccountsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ChartOfAccountsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ChartOfAccountsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ChartOfAccountsQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ChartOfAccountsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ChartOfAccountsQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ChartOfAccountsQuery : esChartOfAccountsQuery
	{
		public ChartOfAccountsQuery()
		{

		}		
		
		public ChartOfAccountsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ChartOfAccountsQuery";
        }
		
			
	}


	[Serializable]
	public partial class ChartOfAccountsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ChartOfAccountsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.ChartOfAccountCode, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.ChartOfAccountCode;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.ChartOfAccountName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.ChartOfAccountName;
			c.CharacterMaxLength = 255;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.IsDetail, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.IsDetail;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.AccountLevel, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.AccountLevel;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.GeneralAccount, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.GeneralAccount;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.NormalBalance, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.NormalBalance;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.AccountGroup, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.AccountGroup;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.SubLedgerId, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.SubLedgerId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.IsDocumenNumberEnabled, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.IsDocumenNumberEnabled;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.TreeCode, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.TreeCode;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.DateCreated, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.DateCreated;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.CreatedBy, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.CreatedBy;
			c.CharacterMaxLength = 25;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 25;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.IsActive, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.IsActive;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.IsControlDocNumber, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.IsControlDocNumber;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.Note, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.ChartOfAccountId, 17, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.ChartOfAccountId;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.IsReconcile, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.IsReconcile;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.BkuAccountID, 19, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.BkuAccountID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChartOfAccountsMetadata.ColumnNames.IsBku, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ChartOfAccountsMetadata.PropertyNames.IsBku;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ChartOfAccountsMetadata Meta()
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
			 public const string ChartOfAccountCode = "ChartOfAccountCode";
			 public const string ChartOfAccountName = "ChartOfAccountName";
			 public const string IsDetail = "IsDetail";
			 public const string AccountLevel = "AccountLevel";
			 public const string GeneralAccount = "GeneralAccount";
			 public const string NormalBalance = "NormalBalance";
			 public const string AccountGroup = "AccountGroup";
			 public const string SubLedgerId = "SubLedgerId";
			 public const string IsDocumenNumberEnabled = "IsDocumenNumberEnabled";
			 public const string TreeCode = "TreeCode";
			 public const string DateCreated = "DateCreated";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string CreatedBy = "CreatedBy";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsActive = "IsActive";
			 public const string IsControlDocNumber = "isControlDocNumber";
			 public const string Note = "Note";
			 public const string ChartOfAccountId = "ChartOfAccountId";
			 public const string IsReconcile = "IsReconcile";
			 public const string BkuAccountID = "BkuAccountID";
			 public const string IsBku = "IsBku";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ChartOfAccountCode = "ChartOfAccountCode";
			 public const string ChartOfAccountName = "ChartOfAccountName";
			 public const string IsDetail = "IsDetail";
			 public const string AccountLevel = "AccountLevel";
			 public const string GeneralAccount = "GeneralAccount";
			 public const string NormalBalance = "NormalBalance";
			 public const string AccountGroup = "AccountGroup";
			 public const string SubLedgerId = "SubLedgerId";
			 public const string IsDocumenNumberEnabled = "IsDocumenNumberEnabled";
			 public const string TreeCode = "TreeCode";
			 public const string DateCreated = "DateCreated";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string CreatedBy = "CreatedBy";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsActive = "IsActive";
			 public const string IsControlDocNumber = "IsControlDocNumber";
			 public const string Note = "Note";
			 public const string ChartOfAccountId = "ChartOfAccountId";
			 public const string IsReconcile = "IsReconcile";
			 public const string BkuAccountID = "BkuAccountID";
			 public const string IsBku = "IsBku";
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
			lock (typeof(ChartOfAccountsMetadata))
			{
				if(ChartOfAccountsMetadata.mapDelegates == null)
				{
					ChartOfAccountsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ChartOfAccountsMetadata.meta == null)
				{
					ChartOfAccountsMetadata.meta = new ChartOfAccountsMetadata();
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
				

				meta.AddTypeMap("ChartOfAccountCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("ChartOfAccountName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("IsDetail", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AccountLevel", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("GeneralAccount", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("NormalBalance", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AccountGroup", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubLedgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsDocumenNumberEnabled", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("TreeCode", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("DateCreated", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedBy", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsControlDocNumber", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsReconcile", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("BkuAccountID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsBku", new esTypeMap("bit", "System.Boolean"));			
				
				
				
				meta.Source = "ChartOfAccounts";
				meta.Destination = "ChartOfAccounts";
				
				meta.spInsert = "proc_ChartOfAccountsInsert";				
				meta.spUpdate = "proc_ChartOfAccountsUpdate";		
				meta.spDelete = "proc_ChartOfAccountsDelete";
				meta.spLoadAll = "proc_ChartOfAccountsLoadAll";
				meta.spLoadByPrimaryKey = "proc_ChartOfAccountsLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ChartOfAccountsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
