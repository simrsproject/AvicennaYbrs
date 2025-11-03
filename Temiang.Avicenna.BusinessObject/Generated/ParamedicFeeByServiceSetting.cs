/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/3/2017 12:56:03 PM
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
	abstract public class esParamedicFeeByServiceSettingCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeByServiceSettingCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicFeeByServiceSettingCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicFeeByServiceSettingQuery query)
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
			this.InitQuery(query as esParamedicFeeByServiceSettingQuery);
		}
		#endregion
		
		virtual public ParamedicFeeByServiceSetting DetachEntity(ParamedicFeeByServiceSetting entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeByServiceSetting;
		}
		
		virtual public ParamedicFeeByServiceSetting AttachEntity(ParamedicFeeByServiceSetting entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeByServiceSetting;
		}
		
		virtual public void Combine(ParamedicFeeByServiceSettingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeByServiceSetting this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeByServiceSetting;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeByServiceSetting);
		}
	}



	[Serializable]
	abstract public class esParamedicFeeByServiceSetting : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeByServiceSettingQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicFeeByServiceSetting()
		{

		}

		public esParamedicFeeByServiceSetting(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 id)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 id)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 id)
		{
			esParamedicFeeByServiceSettingQuery query = this.GetDynamicQuery();
			query.Where(query.Id == id);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 id)
		{
			esParameters parms = new esParameters();
			parms.Add("Id",id);
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
						case "Id": this.str.Id = (string)value; break;							
						case "SRRegistrationType": this.str.SRRegistrationType = (string)value; break;							
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "ClassID": this.str.ClassID = (string)value; break;							
						case "SRParamedicFeeCaseType": this.str.SRParamedicFeeCaseType = (string)value; break;							
						case "SRParamedicFeeIsTeam": this.str.SRParamedicFeeIsTeam = (string)value; break;							
						case "SRParamedicFeeTeamStatus": this.str.SRParamedicFeeTeamStatus = (string)value; break;							
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;							
						case "IsFeeValueInPercent": this.str.IsFeeValueInPercent = (string)value; break;							
						case "FeeValue": this.str.FeeValue = (string)value; break;							
						case "CountMax": this.str.CountMax = (string)value; break;							
						case "IgnoredIfAnyReplacement": this.str.IgnoredIfAnyReplacement = (string)value; break;							
						case "IsReplacement": this.str.IsReplacement = (string)value; break;							
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;							
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "IsReplacementForFeeByPercentageOfAR": this.str.IsReplacementForFeeByPercentageOfAR = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
							break;
						
						case "IsFeeValueInPercent":
						
							if (value == null || value is System.Boolean)
								this.IsFeeValueInPercent = (System.Boolean?)value;
							break;
						
						case "FeeValue":
						
							if (value == null || value is System.Decimal)
								this.FeeValue = (System.Decimal?)value;
							break;
						
						case "CountMax":
						
							if (value == null || value is System.Int32)
								this.CountMax = (System.Int32?)value;
							break;
						
						case "IgnoredIfAnyReplacement":
						
							if (value == null || value is System.Boolean)
								this.IgnoredIfAnyReplacement = (System.Boolean?)value;
							break;
						
						case "IsReplacement":
						
							if (value == null || value is System.Boolean)
								this.IsReplacement = (System.Boolean?)value;
							break;
						
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "IsReplacementForFeeByPercentageOfAR":
						
							if (value == null || value is System.Boolean)
								this.IsReplacementForFeeByPercentageOfAR = (System.Boolean?)value;
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
		/// Maps to ParamedicFeeByServiceSetting.Id
		/// </summary>
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeByServiceSettingMetadata.ColumnNames.Id);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeByServiceSettingMetadata.ColumnNames.Id, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByServiceSetting.SRRegistrationType
		/// </summary>
		virtual public System.String SRRegistrationType
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.SRRegistrationType);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.SRRegistrationType, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByServiceSetting.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByServiceSetting.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByServiceSetting.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByServiceSetting.SRParamedicFeeCaseType
		/// </summary>
		virtual public System.String SRParamedicFeeCaseType
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.SRParamedicFeeCaseType);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.SRParamedicFeeCaseType, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByServiceSetting.SRParamedicFeeIsTeam
		/// </summary>
		virtual public System.String SRParamedicFeeIsTeam
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.SRParamedicFeeIsTeam);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.SRParamedicFeeIsTeam, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByServiceSetting.SRParamedicFeeTeamStatus
		/// </summary>
		virtual public System.String SRParamedicFeeTeamStatus
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.SRParamedicFeeTeamStatus);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.SRParamedicFeeTeamStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByServiceSetting.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByServiceSetting.IsFeeValueInPercent
		/// </summary>
		virtual public System.Boolean? IsFeeValueInPercent
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeByServiceSettingMetadata.ColumnNames.IsFeeValueInPercent);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeByServiceSettingMetadata.ColumnNames.IsFeeValueInPercent, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByServiceSetting.FeeValue
		/// </summary>
		virtual public System.Decimal? FeeValue
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeByServiceSettingMetadata.ColumnNames.FeeValue);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeByServiceSettingMetadata.ColumnNames.FeeValue, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByServiceSetting.CountMax
		/// </summary>
		virtual public System.Int32? CountMax
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeByServiceSettingMetadata.ColumnNames.CountMax);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeByServiceSettingMetadata.ColumnNames.CountMax, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByServiceSetting.IgnoredIfAnyReplacement
		/// </summary>
		virtual public System.Boolean? IgnoredIfAnyReplacement
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeByServiceSettingMetadata.ColumnNames.IgnoredIfAnyReplacement);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeByServiceSettingMetadata.ColumnNames.IgnoredIfAnyReplacement, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByServiceSetting.IsReplacement
		/// </summary>
		virtual public System.Boolean? IsReplacement
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeByServiceSettingMetadata.ColumnNames.IsReplacement);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeByServiceSettingMetadata.ColumnNames.IsReplacement, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByServiceSetting.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByServiceSetting.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeByServiceSettingMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeByServiceSettingMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByServiceSetting.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByServiceSettingMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByServiceSetting.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeByServiceSettingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeByServiceSettingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByServiceSetting.IsReplacementForFeeByPercentageOfAR
		/// </summary>
		virtual public System.Boolean? IsReplacementForFeeByPercentageOfAR
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeByServiceSettingMetadata.ColumnNames.IsReplacementForFeeByPercentageOfAR);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeByServiceSettingMetadata.ColumnNames.IsReplacementForFeeByPercentageOfAR, value);
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
			public esStrings(esParamedicFeeByServiceSetting entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Id
			{
				get
				{
					System.Int32? data = entity.Id;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Id = null;
					else entity.Id = Convert.ToInt32(value);
				}
			}
				
			public System.String SRRegistrationType
			{
				get
				{
					System.String data = entity.SRRegistrationType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRegistrationType = null;
					else entity.SRRegistrationType = Convert.ToString(value);
				}
			}
				
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
				
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
				}
			}
				
			public System.String ClassID
			{
				get
				{
					System.String data = entity.ClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassID = null;
					else entity.ClassID = Convert.ToString(value);
				}
			}
				
			public System.String SRParamedicFeeCaseType
			{
				get
				{
					System.String data = entity.SRParamedicFeeCaseType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRParamedicFeeCaseType = null;
					else entity.SRParamedicFeeCaseType = Convert.ToString(value);
				}
			}
				
			public System.String SRParamedicFeeIsTeam
			{
				get
				{
					System.String data = entity.SRParamedicFeeIsTeam;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRParamedicFeeIsTeam = null;
					else entity.SRParamedicFeeIsTeam = Convert.ToString(value);
				}
			}
				
			public System.String SRParamedicFeeTeamStatus
			{
				get
				{
					System.String data = entity.SRParamedicFeeTeamStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRParamedicFeeTeamStatus = null;
					else entity.SRParamedicFeeTeamStatus = Convert.ToString(value);
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
				
			public System.String IsFeeValueInPercent
			{
				get
				{
					System.Boolean? data = entity.IsFeeValueInPercent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeeValueInPercent = null;
					else entity.IsFeeValueInPercent = Convert.ToBoolean(value);
				}
			}
				
			public System.String FeeValue
			{
				get
				{
					System.Decimal? data = entity.FeeValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeeValue = null;
					else entity.FeeValue = Convert.ToDecimal(value);
				}
			}
				
			public System.String CountMax
			{
				get
				{
					System.Int32? data = entity.CountMax;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CountMax = null;
					else entity.CountMax = Convert.ToInt32(value);
				}
			}
				
			public System.String IgnoredIfAnyReplacement
			{
				get
				{
					System.Boolean? data = entity.IgnoredIfAnyReplacement;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IgnoredIfAnyReplacement = null;
					else entity.IgnoredIfAnyReplacement = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsReplacement
			{
				get
				{
					System.Boolean? data = entity.IsReplacement;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReplacement = null;
					else entity.IsReplacement = Convert.ToBoolean(value);
				}
			}
				
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
				}
			}
				
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
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
				
			public System.String IsReplacementForFeeByPercentageOfAR
			{
				get
				{
					System.Boolean? data = entity.IsReplacementForFeeByPercentageOfAR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReplacementForFeeByPercentageOfAR = null;
					else entity.IsReplacementForFeeByPercentageOfAR = Convert.ToBoolean(value);
				}
			}
			

			private esParamedicFeeByServiceSetting entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeByServiceSettingQuery query)
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
				throw new Exception("esParamedicFeeByServiceSetting can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esParamedicFeeByServiceSettingQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeByServiceSettingMetadata.Meta();
			}
		}	
		

		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByServiceSettingMetadata.ColumnNames.Id, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRRegistrationType
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByServiceSettingMetadata.ColumnNames.SRRegistrationType, esSystemType.String);
			}
		} 
		
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByServiceSettingMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByServiceSettingMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByServiceSettingMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRParamedicFeeCaseType
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByServiceSettingMetadata.ColumnNames.SRParamedicFeeCaseType, esSystemType.String);
			}
		} 
		
		public esQueryItem SRParamedicFeeIsTeam
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByServiceSettingMetadata.ColumnNames.SRParamedicFeeIsTeam, esSystemType.String);
			}
		} 
		
		public esQueryItem SRParamedicFeeTeamStatus
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByServiceSettingMetadata.ColumnNames.SRParamedicFeeTeamStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByServiceSettingMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsFeeValueInPercent
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByServiceSettingMetadata.ColumnNames.IsFeeValueInPercent, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem FeeValue
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByServiceSettingMetadata.ColumnNames.FeeValue, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CountMax
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByServiceSettingMetadata.ColumnNames.CountMax, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IgnoredIfAnyReplacement
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByServiceSettingMetadata.ColumnNames.IgnoredIfAnyReplacement, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsReplacement
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByServiceSettingMetadata.ColumnNames.IsReplacement, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByServiceSettingMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByServiceSettingMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByServiceSettingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByServiceSettingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem IsReplacementForFeeByPercentageOfAR
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByServiceSettingMetadata.ColumnNames.IsReplacementForFeeByPercentageOfAR, esSystemType.Boolean);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeByServiceSettingCollection")]
	public partial class ParamedicFeeByServiceSettingCollection : esParamedicFeeByServiceSettingCollection, IEnumerable<ParamedicFeeByServiceSetting>
	{
		public ParamedicFeeByServiceSettingCollection()
		{

		}
		
		public static implicit operator List<ParamedicFeeByServiceSetting>(ParamedicFeeByServiceSettingCollection coll)
		{
			List<ParamedicFeeByServiceSetting> list = new List<ParamedicFeeByServiceSetting>();
			
			foreach (ParamedicFeeByServiceSetting emp in coll)
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
				return  ParamedicFeeByServiceSettingMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeByServiceSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeByServiceSetting(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeByServiceSetting();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicFeeByServiceSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeByServiceSettingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicFeeByServiceSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicFeeByServiceSetting AddNew()
		{
			ParamedicFeeByServiceSetting entity = base.AddNewEntity() as ParamedicFeeByServiceSetting;
			
			return entity;
		}

		public ParamedicFeeByServiceSetting FindByPrimaryKey(System.Int32 id)
		{
			return base.FindByPrimaryKey(id) as ParamedicFeeByServiceSetting;
		}


		#region IEnumerable<ParamedicFeeByServiceSetting> Members

		IEnumerator<ParamedicFeeByServiceSetting> IEnumerable<ParamedicFeeByServiceSetting>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeByServiceSetting;
			}
		}

		#endregion
		
		private ParamedicFeeByServiceSettingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeByServiceSetting' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeeByServiceSetting ({Id})")]
	[Serializable]
	public partial class ParamedicFeeByServiceSetting : esParamedicFeeByServiceSetting
	{
		public ParamedicFeeByServiceSetting()
		{

		}
	
		public ParamedicFeeByServiceSetting(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeByServiceSettingMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicFeeByServiceSettingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeByServiceSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicFeeByServiceSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeByServiceSettingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicFeeByServiceSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicFeeByServiceSettingQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicFeeByServiceSettingQuery : esParamedicFeeByServiceSettingQuery
	{
		public ParamedicFeeByServiceSettingQuery()
		{

		}		
		
		public ParamedicFeeByServiceSettingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicFeeByServiceSettingQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicFeeByServiceSettingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeByServiceSettingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicFeeByServiceSettingMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeByServiceSettingMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByServiceSettingMetadata.ColumnNames.SRRegistrationType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByServiceSettingMetadata.PropertyNames.SRRegistrationType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByServiceSettingMetadata.ColumnNames.ServiceUnitID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByServiceSettingMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByServiceSettingMetadata.ColumnNames.ItemID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByServiceSettingMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByServiceSettingMetadata.ColumnNames.ClassID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByServiceSettingMetadata.PropertyNames.ClassID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByServiceSettingMetadata.ColumnNames.SRParamedicFeeCaseType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByServiceSettingMetadata.PropertyNames.SRParamedicFeeCaseType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByServiceSettingMetadata.ColumnNames.SRParamedicFeeIsTeam, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByServiceSettingMetadata.PropertyNames.SRParamedicFeeIsTeam;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByServiceSettingMetadata.ColumnNames.SRParamedicFeeTeamStatus, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByServiceSettingMetadata.PropertyNames.SRParamedicFeeTeamStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByServiceSettingMetadata.ColumnNames.TariffComponentID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByServiceSettingMetadata.PropertyNames.TariffComponentID;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByServiceSettingMetadata.ColumnNames.IsFeeValueInPercent, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeByServiceSettingMetadata.PropertyNames.IsFeeValueInPercent;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByServiceSettingMetadata.ColumnNames.FeeValue, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeByServiceSettingMetadata.PropertyNames.FeeValue;
			c.NumericPrecision = 12;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByServiceSettingMetadata.ColumnNames.CountMax, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeByServiceSettingMetadata.PropertyNames.CountMax;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByServiceSettingMetadata.ColumnNames.IgnoredIfAnyReplacement, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeByServiceSettingMetadata.PropertyNames.IgnoredIfAnyReplacement;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByServiceSettingMetadata.ColumnNames.IsReplacement, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeByServiceSettingMetadata.PropertyNames.IsReplacement;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByServiceSettingMetadata.ColumnNames.CreatedByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByServiceSettingMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByServiceSettingMetadata.ColumnNames.CreatedDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeByServiceSettingMetadata.PropertyNames.CreatedDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByServiceSettingMetadata.ColumnNames.LastUpdateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByServiceSettingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByServiceSettingMetadata.ColumnNames.LastUpdateDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeByServiceSettingMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByServiceSettingMetadata.ColumnNames.IsReplacementForFeeByPercentageOfAR, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeByServiceSettingMetadata.PropertyNames.IsReplacementForFeeByPercentageOfAR;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicFeeByServiceSettingMetadata Meta()
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
			 public const string Id = "Id";
			 public const string SRRegistrationType = "SRRegistrationType";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ItemID = "ItemID";
			 public const string ClassID = "ClassID";
			 public const string SRParamedicFeeCaseType = "SRParamedicFeeCaseType";
			 public const string SRParamedicFeeIsTeam = "SRParamedicFeeIsTeam";
			 public const string SRParamedicFeeTeamStatus = "SRParamedicFeeTeamStatus";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string IsFeeValueInPercent = "IsFeeValueInPercent";
			 public const string FeeValue = "FeeValue";
			 public const string CountMax = "CountMax";
			 public const string IgnoredIfAnyReplacement = "IgnoredIfAnyReplacement";
			 public const string IsReplacement = "IsReplacement";
			 public const string CreatedByUserID = "CreatedByUserID";
			 public const string CreatedDateTime = "CreatedDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string IsReplacementForFeeByPercentageOfAR = "IsReplacementForFeeByPercentageOfAR";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string SRRegistrationType = "SRRegistrationType";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ItemID = "ItemID";
			 public const string ClassID = "ClassID";
			 public const string SRParamedicFeeCaseType = "SRParamedicFeeCaseType";
			 public const string SRParamedicFeeIsTeam = "SRParamedicFeeIsTeam";
			 public const string SRParamedicFeeTeamStatus = "SRParamedicFeeTeamStatus";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string IsFeeValueInPercent = "IsFeeValueInPercent";
			 public const string FeeValue = "FeeValue";
			 public const string CountMax = "CountMax";
			 public const string IgnoredIfAnyReplacement = "IgnoredIfAnyReplacement";
			 public const string IsReplacement = "IsReplacement";
			 public const string CreatedByUserID = "CreatedByUserID";
			 public const string CreatedDateTime = "CreatedDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string IsReplacementForFeeByPercentageOfAR = "IsReplacementForFeeByPercentageOfAR";
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
			lock (typeof(ParamedicFeeByServiceSettingMetadata))
			{
				if(ParamedicFeeByServiceSettingMetadata.mapDelegates == null)
				{
					ParamedicFeeByServiceSettingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeByServiceSettingMetadata.meta == null)
				{
					ParamedicFeeByServiceSettingMetadata.meta = new ParamedicFeeByServiceSettingMetadata();
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
				

				meta.AddTypeMap("Id", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRRegistrationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRParamedicFeeCaseType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRParamedicFeeIsTeam", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRParamedicFeeTeamStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsFeeValueInPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("FeeValue", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CountMax", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IgnoredIfAnyReplacement", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsReplacement", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsReplacementForFeeByPercentageOfAR", new esTypeMap("bit", "System.Boolean"));			
				
				
				
				meta.Source = "ParamedicFeeByServiceSetting";
				meta.Destination = "ParamedicFeeByServiceSetting";
				
				meta.spInsert = "proc_ParamedicFeeByServiceSettingInsert";				
				meta.spUpdate = "proc_ParamedicFeeByServiceSettingUpdate";		
				meta.spDelete = "proc_ParamedicFeeByServiceSettingDelete";
				meta.spLoadAll = "proc_ParamedicFeeByServiceSettingLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeByServiceSettingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeByServiceSettingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
