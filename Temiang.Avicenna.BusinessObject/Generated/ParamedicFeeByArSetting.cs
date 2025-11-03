/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 12/19/2016 8:06:44 PM
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
	abstract public class esParamedicFeeByArSettingCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeByArSettingCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicFeeByArSettingCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicFeeByArSettingQuery query)
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
			this.InitQuery(query as esParamedicFeeByArSettingQuery);
		}
		#endregion
		
		virtual public ParamedicFeeByArSetting DetachEntity(ParamedicFeeByArSetting entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeByArSetting;
		}
		
		virtual public ParamedicFeeByArSetting AttachEntity(ParamedicFeeByArSetting entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeByArSetting;
		}
		
		virtual public void Combine(ParamedicFeeByArSettingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeByArSetting this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeByArSetting;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeByArSetting);
		}
	}



	[Serializable]
	abstract public class esParamedicFeeByArSetting : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeByArSettingQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicFeeByArSetting()
		{

		}

		public esParamedicFeeByArSetting(DataRow row)
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
			esParamedicFeeByArSettingQuery query = this.GetDynamicQuery();
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
						case "IsMergeToIPR": this.str.IsMergeToIPR = (string)value; break;							
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "SRParamedicFeeCaseType": this.str.SRParamedicFeeCaseType = (string)value; break;							
						case "SRParamedicFeeIsTeam": this.str.SRParamedicFeeIsTeam = (string)value; break;							
						case "LosStart": this.str.LosStart = (string)value; break;							
						case "LosEnd": this.str.LosEnd = (string)value; break;							
						case "SRParamedicFeeTeamStatus": this.str.SRParamedicFeeTeamStatus = (string)value; break;							
						case "IsFeeValueInPercent": this.str.IsFeeValueInPercent = (string)value; break;							
						case "FeeValue": this.str.FeeValue = (string)value; break;							
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;							
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "SmfID": this.str.SmfID = (string)value; break;
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
						
						case "IsMergeToIPR":
						
							if (value == null || value is System.Boolean)
								this.IsMergeToIPR = (System.Boolean?)value;
							break;
						
						case "LosStart":
						
							if (value == null || value is System.Int32)
								this.LosStart = (System.Int32?)value;
							break;
						
						case "LosEnd":
						
							if (value == null || value is System.Int32)
								this.LosEnd = (System.Int32?)value;
							break;
						
						case "IsFeeValueInPercent":
						
							if (value == null || value is System.Boolean)
								this.IsFeeValueInPercent = (System.Boolean?)value;
							break;
						
						case "FeeValue":
						
							if (value == null || value is System.Decimal)
								this.FeeValue = (System.Decimal?)value;
							break;
						
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to ParamedicFeeByArSetting.Id
		/// </summary>
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeByArSettingMetadata.ColumnNames.Id);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeByArSettingMetadata.ColumnNames.Id, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByArSetting.SRRegistrationType
		/// </summary>
		virtual public System.String SRRegistrationType
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByArSettingMetadata.ColumnNames.SRRegistrationType);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByArSettingMetadata.ColumnNames.SRRegistrationType, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByArSetting.IsMergeToIPR
		/// </summary>
		virtual public System.Boolean? IsMergeToIPR
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeByArSettingMetadata.ColumnNames.IsMergeToIPR);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeByArSettingMetadata.ColumnNames.IsMergeToIPR, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByArSetting.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByArSettingMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByArSettingMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByArSetting.SRParamedicFeeCaseType
		/// </summary>
		virtual public System.String SRParamedicFeeCaseType
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByArSettingMetadata.ColumnNames.SRParamedicFeeCaseType);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByArSettingMetadata.ColumnNames.SRParamedicFeeCaseType, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByArSetting.SRParamedicFeeIsTeam
		/// </summary>
		virtual public System.String SRParamedicFeeIsTeam
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByArSettingMetadata.ColumnNames.SRParamedicFeeIsTeam);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByArSettingMetadata.ColumnNames.SRParamedicFeeIsTeam, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByArSetting.LosStart
		/// </summary>
		virtual public System.Int32? LosStart
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeByArSettingMetadata.ColumnNames.LosStart);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeByArSettingMetadata.ColumnNames.LosStart, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByArSetting.LosEnd
		/// </summary>
		virtual public System.Int32? LosEnd
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeByArSettingMetadata.ColumnNames.LosEnd);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeByArSettingMetadata.ColumnNames.LosEnd, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByArSetting.SRParamedicFeeTeamStatus
		/// </summary>
		virtual public System.String SRParamedicFeeTeamStatus
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByArSettingMetadata.ColumnNames.SRParamedicFeeTeamStatus);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByArSettingMetadata.ColumnNames.SRParamedicFeeTeamStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByArSetting.IsFeeValueInPercent
		/// </summary>
		virtual public System.Boolean? IsFeeValueInPercent
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeByArSettingMetadata.ColumnNames.IsFeeValueInPercent);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeByArSettingMetadata.ColumnNames.IsFeeValueInPercent, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByArSetting.FeeValue
		/// </summary>
		virtual public System.Decimal? FeeValue
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeByArSettingMetadata.ColumnNames.FeeValue);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeByArSettingMetadata.ColumnNames.FeeValue, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByArSetting.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByArSettingMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByArSettingMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByArSetting.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeByArSettingMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeByArSettingMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByArSetting.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByArSettingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByArSettingMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByArSetting.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeByArSettingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeByArSettingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByArSetting.SmfID
		/// </summary>
		virtual public System.String SmfID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByArSettingMetadata.ColumnNames.SmfID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByArSettingMetadata.ColumnNames.SmfID, value);
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
			public esStrings(esParamedicFeeByArSetting entity)
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
				
			public System.String IsMergeToIPR
			{
				get
				{
					System.Boolean? data = entity.IsMergeToIPR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMergeToIPR = null;
					else entity.IsMergeToIPR = Convert.ToBoolean(value);
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
				
			public System.String LosStart
			{
				get
				{
					System.Int32? data = entity.LosStart;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LosStart = null;
					else entity.LosStart = Convert.ToInt32(value);
				}
			}
				
			public System.String LosEnd
			{
				get
				{
					System.Int32? data = entity.LosEnd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LosEnd = null;
					else entity.LosEnd = Convert.ToInt32(value);
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
				
			public System.String SmfID
			{
				get
				{
					System.String data = entity.SmfID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SmfID = null;
					else entity.SmfID = Convert.ToString(value);
				}
			}
			

			private esParamedicFeeByArSetting entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeByArSettingQuery query)
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
				throw new Exception("esParamedicFeeByArSetting can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esParamedicFeeByArSettingQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeByArSettingMetadata.Meta();
			}
		}	
		

		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByArSettingMetadata.ColumnNames.Id, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRRegistrationType
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByArSettingMetadata.ColumnNames.SRRegistrationType, esSystemType.String);
			}
		} 
		
		public esQueryItem IsMergeToIPR
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByArSettingMetadata.ColumnNames.IsMergeToIPR, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByArSettingMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRParamedicFeeCaseType
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByArSettingMetadata.ColumnNames.SRParamedicFeeCaseType, esSystemType.String);
			}
		} 
		
		public esQueryItem SRParamedicFeeIsTeam
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByArSettingMetadata.ColumnNames.SRParamedicFeeIsTeam, esSystemType.String);
			}
		} 
		
		public esQueryItem LosStart
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByArSettingMetadata.ColumnNames.LosStart, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LosEnd
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByArSettingMetadata.ColumnNames.LosEnd, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRParamedicFeeTeamStatus
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByArSettingMetadata.ColumnNames.SRParamedicFeeTeamStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem IsFeeValueInPercent
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByArSettingMetadata.ColumnNames.IsFeeValueInPercent, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem FeeValue
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByArSettingMetadata.ColumnNames.FeeValue, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByArSettingMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByArSettingMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByArSettingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByArSettingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem SmfID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByArSettingMetadata.ColumnNames.SmfID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeByArSettingCollection")]
	public partial class ParamedicFeeByArSettingCollection : esParamedicFeeByArSettingCollection, IEnumerable<ParamedicFeeByArSetting>
	{
		public ParamedicFeeByArSettingCollection()
		{

		}
		
		public static implicit operator List<ParamedicFeeByArSetting>(ParamedicFeeByArSettingCollection coll)
		{
			List<ParamedicFeeByArSetting> list = new List<ParamedicFeeByArSetting>();
			
			foreach (ParamedicFeeByArSetting emp in coll)
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
				return  ParamedicFeeByArSettingMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeByArSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeByArSetting(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeByArSetting();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicFeeByArSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeByArSettingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicFeeByArSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicFeeByArSetting AddNew()
		{
			ParamedicFeeByArSetting entity = base.AddNewEntity() as ParamedicFeeByArSetting;
			
			return entity;
		}

		public ParamedicFeeByArSetting FindByPrimaryKey(System.Int32 id)
		{
			return base.FindByPrimaryKey(id) as ParamedicFeeByArSetting;
		}


		#region IEnumerable<ParamedicFeeByArSetting> Members

		IEnumerator<ParamedicFeeByArSetting> IEnumerable<ParamedicFeeByArSetting>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeByArSetting;
			}
		}

		#endregion
		
		private ParamedicFeeByArSettingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeByArSetting' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeeByArSetting ({Id})")]
	[Serializable]
	public partial class ParamedicFeeByArSetting : esParamedicFeeByArSetting
	{
		public ParamedicFeeByArSetting()
		{

		}
	
		public ParamedicFeeByArSetting(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeByArSettingMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicFeeByArSettingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeByArSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicFeeByArSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeByArSettingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicFeeByArSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicFeeByArSettingQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicFeeByArSettingQuery : esParamedicFeeByArSettingQuery
	{
		public ParamedicFeeByArSettingQuery()
		{

		}		
		
		public ParamedicFeeByArSettingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicFeeByArSettingQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicFeeByArSettingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeByArSettingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicFeeByArSettingMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeByArSettingMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByArSettingMetadata.ColumnNames.SRRegistrationType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByArSettingMetadata.PropertyNames.SRRegistrationType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByArSettingMetadata.ColumnNames.IsMergeToIPR, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeByArSettingMetadata.PropertyNames.IsMergeToIPR;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByArSettingMetadata.ColumnNames.ServiceUnitID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByArSettingMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByArSettingMetadata.ColumnNames.SRParamedicFeeCaseType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByArSettingMetadata.PropertyNames.SRParamedicFeeCaseType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByArSettingMetadata.ColumnNames.SRParamedicFeeIsTeam, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByArSettingMetadata.PropertyNames.SRParamedicFeeIsTeam;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByArSettingMetadata.ColumnNames.LosStart, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeByArSettingMetadata.PropertyNames.LosStart;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByArSettingMetadata.ColumnNames.LosEnd, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeByArSettingMetadata.PropertyNames.LosEnd;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByArSettingMetadata.ColumnNames.SRParamedicFeeTeamStatus, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByArSettingMetadata.PropertyNames.SRParamedicFeeTeamStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByArSettingMetadata.ColumnNames.IsFeeValueInPercent, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeByArSettingMetadata.PropertyNames.IsFeeValueInPercent;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByArSettingMetadata.ColumnNames.FeeValue, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeByArSettingMetadata.PropertyNames.FeeValue;
			c.NumericPrecision = 12;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByArSettingMetadata.ColumnNames.CreatedByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByArSettingMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByArSettingMetadata.ColumnNames.CreatedDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeByArSettingMetadata.PropertyNames.CreatedDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByArSettingMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByArSettingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByArSettingMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeByArSettingMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByArSettingMetadata.ColumnNames.SmfID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByArSettingMetadata.PropertyNames.SmfID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicFeeByArSettingMetadata Meta()
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
			 public const string IsMergeToIPR = "IsMergeToIPR";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string SRParamedicFeeCaseType = "SRParamedicFeeCaseType";
			 public const string SRParamedicFeeIsTeam = "SRParamedicFeeIsTeam";
			 public const string LosStart = "LosStart";
			 public const string LosEnd = "LosEnd";
			 public const string SRParamedicFeeTeamStatus = "SRParamedicFeeTeamStatus";
			 public const string IsFeeValueInPercent = "IsFeeValueInPercent";
			 public const string FeeValue = "FeeValue";
			 public const string CreatedByUserID = "CreatedByUserID";
			 public const string CreatedDateTime = "CreatedDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string SmfID = "SmfID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string SRRegistrationType = "SRRegistrationType";
			 public const string IsMergeToIPR = "IsMergeToIPR";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string SRParamedicFeeCaseType = "SRParamedicFeeCaseType";
			 public const string SRParamedicFeeIsTeam = "SRParamedicFeeIsTeam";
			 public const string LosStart = "LosStart";
			 public const string LosEnd = "LosEnd";
			 public const string SRParamedicFeeTeamStatus = "SRParamedicFeeTeamStatus";
			 public const string IsFeeValueInPercent = "IsFeeValueInPercent";
			 public const string FeeValue = "FeeValue";
			 public const string CreatedByUserID = "CreatedByUserID";
			 public const string CreatedDateTime = "CreatedDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string SmfID = "SmfID";
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
			lock (typeof(ParamedicFeeByArSettingMetadata))
			{
				if(ParamedicFeeByArSettingMetadata.mapDelegates == null)
				{
					ParamedicFeeByArSettingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeByArSettingMetadata.meta == null)
				{
					ParamedicFeeByArSettingMetadata.meta = new ParamedicFeeByArSettingMetadata();
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
				meta.AddTypeMap("IsMergeToIPR", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRParamedicFeeCaseType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRParamedicFeeIsTeam", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LosStart", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LosEnd", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRParamedicFeeTeamStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsFeeValueInPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("FeeValue", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SmfID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ParamedicFeeByArSetting";
				meta.Destination = "ParamedicFeeByArSetting";
				
				meta.spInsert = "proc_ParamedicFeeByArSettingInsert";				
				meta.spUpdate = "proc_ParamedicFeeByArSettingUpdate";		
				meta.spDelete = "proc_ParamedicFeeByArSettingDelete";
				meta.spLoadAll = "proc_ParamedicFeeByArSettingLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeByArSettingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeByArSettingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
