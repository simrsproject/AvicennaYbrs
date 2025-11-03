/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:20 PM
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
	abstract public class esParamedicFeeTransChargesItemCompCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeTransChargesItemCompCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicFeeTransChargesItemCompCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicFeeTransChargesItemCompQuery query)
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
			this.InitQuery(query as esParamedicFeeTransChargesItemCompQuery);
		}
		#endregion
		
		virtual public ParamedicFeeTransChargesItemComp DetachEntity(ParamedicFeeTransChargesItemComp entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeTransChargesItemComp;
		}
		
		virtual public ParamedicFeeTransChargesItemComp AttachEntity(ParamedicFeeTransChargesItemComp entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeTransChargesItemComp;
		}
		
		virtual public void Combine(ParamedicFeeTransChargesItemCompCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeTransChargesItemComp this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeTransChargesItemComp;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeTransChargesItemComp);
		}
	}



	[Serializable]
	abstract public class esParamedicFeeTransChargesItemComp : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeTransChargesItemCompQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicFeeTransChargesItemComp()
		{

		}

		public esParamedicFeeTransChargesItemComp(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String transactionNo, System.String sequenceNo, System.String tariffComponentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, tariffComponentID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo, System.String sequenceNo, System.String tariffComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, tariffComponentID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String transactionNo, System.String sequenceNo, System.String tariffComponentID)
		{
			esParamedicFeeTransChargesItemCompQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo, query.TariffComponentID == tariffComponentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo, System.String sequenceNo, System.String tariffComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);			parms.Add("SequenceNo",sequenceNo);			parms.Add("TariffComponentID",tariffComponentID);
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
						case "PhysicianFeePeriod": this.str.PhysicianFeePeriod = (string)value; break;							
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "PhysicianFeeAmount": this.str.PhysicianFeeAmount = (string)value; break;							
						case "IncPremi1Amount": this.str.IncPremi1Amount = (string)value; break;							
						case "IncPremi2Amount": this.str.IncPremi2Amount = (string)value; break;							
						case "DecProductionServicesAmount": this.str.DecProductionServicesAmount = (string)value; break;							
						case "DecTogethernessAmount": this.str.DecTogethernessAmount = (string)value; break;							
						case "DecRentalRoomsAmount": this.str.DecRentalRoomsAmount = (string)value; break;							
						case "RentalRoomsToParamedicID": this.str.RentalRoomsToParamedicID = (string)value; break;							
						case "VerificationNo": this.str.VerificationNo = (string)value; break;							
						case "LastCalculatedDateTime": this.str.LastCalculatedDateTime = (string)value; break;							
						case "LastCalculatedByUserID": this.str.LastCalculatedByUserID = (string)value; break;							
						case "LastUpdatedDateTime": this.str.LastUpdatedDateTime = (string)value; break;							
						case "LastUpdatedByUserID": this.str.LastUpdatedByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PhysicianFeePeriod":
						
							if (value == null || value is System.DateTime)
								this.PhysicianFeePeriod = (System.DateTime?)value;
							break;
						
						case "PhysicianFeeAmount":
						
							if (value == null || value is System.Decimal)
								this.PhysicianFeeAmount = (System.Decimal?)value;
							break;
						
						case "IncPremi1Amount":
						
							if (value == null || value is System.Decimal)
								this.IncPremi1Amount = (System.Decimal?)value;
							break;
						
						case "IncPremi2Amount":
						
							if (value == null || value is System.Decimal)
								this.IncPremi2Amount = (System.Decimal?)value;
							break;
						
						case "DecProductionServicesAmount":
						
							if (value == null || value is System.Decimal)
								this.DecProductionServicesAmount = (System.Decimal?)value;
							break;
						
						case "DecTogethernessAmount":
						
							if (value == null || value is System.Decimal)
								this.DecTogethernessAmount = (System.Decimal?)value;
							break;
						
						case "DecRentalRoomsAmount":
						
							if (value == null || value is System.Decimal)
								this.DecRentalRoomsAmount = (System.Decimal?)value;
							break;
						
						case "LastCalculatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastCalculatedDateTime = (System.DateTime?)value;
							break;
						
						case "LastUpdatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdatedDateTime = (System.DateTime?)value;
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
		/// Maps to ParamedicFeeTransChargesItemComp.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemComp.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemComp.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemComp.PhysicianFeePeriod
		/// </summary>
		virtual public System.DateTime? PhysicianFeePeriod
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.PhysicianFeePeriod);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.PhysicianFeePeriod, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemComp.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemComp.PhysicianFeeAmount
		/// </summary>
		virtual public System.Decimal? PhysicianFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.PhysicianFeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.PhysicianFeeAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemComp.IncPremi1Amount
		/// </summary>
		virtual public System.Decimal? IncPremi1Amount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.IncPremi1Amount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.IncPremi1Amount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemComp.IncPremi2Amount
		/// </summary>
		virtual public System.Decimal? IncPremi2Amount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.IncPremi2Amount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.IncPremi2Amount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemComp.DecProductionServicesAmount
		/// </summary>
		virtual public System.Decimal? DecProductionServicesAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.DecProductionServicesAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.DecProductionServicesAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemComp.DecTogethernessAmount
		/// </summary>
		virtual public System.Decimal? DecTogethernessAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.DecTogethernessAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.DecTogethernessAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemComp.DecRentalRoomsAmount
		/// </summary>
		virtual public System.Decimal? DecRentalRoomsAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.DecRentalRoomsAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.DecRentalRoomsAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemComp.RentalRoomsToParamedicID
		/// </summary>
		virtual public System.String RentalRoomsToParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.RentalRoomsToParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.RentalRoomsToParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemComp.VerificationNo
		/// </summary>
		virtual public System.String VerificationNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.VerificationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.VerificationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemComp.LastCalculatedDateTime
		/// </summary>
		virtual public System.DateTime? LastCalculatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.LastCalculatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.LastCalculatedDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemComp.LastCalculatedByUserID
		/// </summary>
		virtual public System.String LastCalculatedByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.LastCalculatedByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.LastCalculatedByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemComp.LastUpdatedDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.LastUpdatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.LastUpdatedDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemComp.LastUpdatedByUserID
		/// </summary>
		virtual public System.String LastUpdatedByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.LastUpdatedByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.LastUpdatedByUserID, value);
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
			public esStrings(esParamedicFeeTransChargesItemComp entity)
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
				
			public System.String PhysicianFeePeriod
			{
				get
				{
					System.DateTime? data = entity.PhysicianFeePeriod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhysicianFeePeriod = null;
					else entity.PhysicianFeePeriod = Convert.ToDateTime(value);
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
				
			public System.String PhysicianFeeAmount
			{
				get
				{
					System.Decimal? data = entity.PhysicianFeeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhysicianFeeAmount = null;
					else entity.PhysicianFeeAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String IncPremi1Amount
			{
				get
				{
					System.Decimal? data = entity.IncPremi1Amount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IncPremi1Amount = null;
					else entity.IncPremi1Amount = Convert.ToDecimal(value);
				}
			}
				
			public System.String IncPremi2Amount
			{
				get
				{
					System.Decimal? data = entity.IncPremi2Amount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IncPremi2Amount = null;
					else entity.IncPremi2Amount = Convert.ToDecimal(value);
				}
			}
				
			public System.String DecProductionServicesAmount
			{
				get
				{
					System.Decimal? data = entity.DecProductionServicesAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DecProductionServicesAmount = null;
					else entity.DecProductionServicesAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String DecTogethernessAmount
			{
				get
				{
					System.Decimal? data = entity.DecTogethernessAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DecTogethernessAmount = null;
					else entity.DecTogethernessAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String DecRentalRoomsAmount
			{
				get
				{
					System.Decimal? data = entity.DecRentalRoomsAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DecRentalRoomsAmount = null;
					else entity.DecRentalRoomsAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String RentalRoomsToParamedicID
			{
				get
				{
					System.String data = entity.RentalRoomsToParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RentalRoomsToParamedicID = null;
					else entity.RentalRoomsToParamedicID = Convert.ToString(value);
				}
			}
				
			public System.String VerificationNo
			{
				get
				{
					System.String data = entity.VerificationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationNo = null;
					else entity.VerificationNo = Convert.ToString(value);
				}
			}
				
			public System.String LastCalculatedDateTime
			{
				get
				{
					System.DateTime? data = entity.LastCalculatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCalculatedDateTime = null;
					else entity.LastCalculatedDateTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String LastCalculatedByUserID
			{
				get
				{
					System.String data = entity.LastCalculatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCalculatedByUserID = null;
					else entity.LastCalculatedByUserID = Convert.ToString(value);
				}
			}
				
			public System.String LastUpdatedDateTime
			{
				get
				{
					System.DateTime? data = entity.LastUpdatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdatedDateTime = null;
					else entity.LastUpdatedDateTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String LastUpdatedByUserID
			{
				get
				{
					System.String data = entity.LastUpdatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdatedByUserID = null;
					else entity.LastUpdatedByUserID = Convert.ToString(value);
				}
			}
			

			private esParamedicFeeTransChargesItemComp entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeTransChargesItemCompQuery query)
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
				throw new Exception("esParamedicFeeTransChargesItemComp can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ParamedicFeeTransChargesItemComp : esParamedicFeeTransChargesItemComp
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
	abstract public class esParamedicFeeTransChargesItemCompQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeTransChargesItemCompMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
		
		public esQueryItem PhysicianFeePeriod
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompMetadata.ColumnNames.PhysicianFeePeriod, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem PhysicianFeeAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompMetadata.ColumnNames.PhysicianFeeAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IncPremi1Amount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompMetadata.ColumnNames.IncPremi1Amount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IncPremi2Amount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompMetadata.ColumnNames.IncPremi2Amount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DecProductionServicesAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompMetadata.ColumnNames.DecProductionServicesAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DecTogethernessAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompMetadata.ColumnNames.DecTogethernessAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DecRentalRoomsAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompMetadata.ColumnNames.DecRentalRoomsAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem RentalRoomsToParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompMetadata.ColumnNames.RentalRoomsToParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem VerificationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompMetadata.ColumnNames.VerificationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem LastCalculatedDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompMetadata.ColumnNames.LastCalculatedDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastCalculatedByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompMetadata.ColumnNames.LastCalculatedByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdatedDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompMetadata.ColumnNames.LastUpdatedDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdatedByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompMetadata.ColumnNames.LastUpdatedByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeTransChargesItemCompCollection")]
	public partial class ParamedicFeeTransChargesItemCompCollection : esParamedicFeeTransChargesItemCompCollection, IEnumerable<ParamedicFeeTransChargesItemComp>
	{
		public ParamedicFeeTransChargesItemCompCollection()
		{

		}
		
		public static implicit operator List<ParamedicFeeTransChargesItemComp>(ParamedicFeeTransChargesItemCompCollection coll)
		{
			List<ParamedicFeeTransChargesItemComp> list = new List<ParamedicFeeTransChargesItemComp>();
			
			foreach (ParamedicFeeTransChargesItemComp emp in coll)
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
				return  ParamedicFeeTransChargesItemCompMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeTransChargesItemCompQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeTransChargesItemComp(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeTransChargesItemComp();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicFeeTransChargesItemCompQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeTransChargesItemCompQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicFeeTransChargesItemCompQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicFeeTransChargesItemComp AddNew()
		{
			ParamedicFeeTransChargesItemComp entity = base.AddNewEntity() as ParamedicFeeTransChargesItemComp;
			
			return entity;
		}

		public ParamedicFeeTransChargesItemComp FindByPrimaryKey(System.String transactionNo, System.String sequenceNo, System.String tariffComponentID)
		{
			return base.FindByPrimaryKey(transactionNo, sequenceNo, tariffComponentID) as ParamedicFeeTransChargesItemComp;
		}


		#region IEnumerable<ParamedicFeeTransChargesItemComp> Members

		IEnumerator<ParamedicFeeTransChargesItemComp> IEnumerable<ParamedicFeeTransChargesItemComp>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeTransChargesItemComp;
			}
		}

		#endregion
		
		private ParamedicFeeTransChargesItemCompQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeTransChargesItemComp' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeeTransChargesItemComp ({TransactionNo},{SequenceNo},{TariffComponentID})")]
	[Serializable]
	public partial class ParamedicFeeTransChargesItemComp : esParamedicFeeTransChargesItemComp
	{
		public ParamedicFeeTransChargesItemComp()
		{

		}
	
		public ParamedicFeeTransChargesItemComp(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeTransChargesItemCompMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicFeeTransChargesItemCompQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeTransChargesItemCompQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicFeeTransChargesItemCompQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeTransChargesItemCompQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicFeeTransChargesItemCompQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicFeeTransChargesItemCompQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicFeeTransChargesItemCompQuery : esParamedicFeeTransChargesItemCompQuery
	{
		public ParamedicFeeTransChargesItemCompQuery()
		{

		}		
		
		public ParamedicFeeTransChargesItemCompQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicFeeTransChargesItemCompQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicFeeTransChargesItemCompMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeTransChargesItemCompMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 7;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.TariffComponentID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.PhysicianFeePeriod, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransChargesItemCompMetadata.PropertyNames.PhysicianFeePeriod;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.ParamedicID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.PhysicianFeeAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompMetadata.PropertyNames.PhysicianFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.IncPremi1Amount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompMetadata.PropertyNames.IncPremi1Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.IncPremi2Amount, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompMetadata.PropertyNames.IncPremi2Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.DecProductionServicesAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompMetadata.PropertyNames.DecProductionServicesAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.DecTogethernessAmount, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompMetadata.PropertyNames.DecTogethernessAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.DecRentalRoomsAmount, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompMetadata.PropertyNames.DecRentalRoomsAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.RentalRoomsToParamedicID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompMetadata.PropertyNames.RentalRoomsToParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.VerificationNo, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompMetadata.PropertyNames.VerificationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.LastCalculatedDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransChargesItemCompMetadata.PropertyNames.LastCalculatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.LastCalculatedByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompMetadata.PropertyNames.LastCalculatedByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.LastUpdatedDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransChargesItemCompMetadata.PropertyNames.LastUpdatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompMetadata.ColumnNames.LastUpdatedByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompMetadata.PropertyNames.LastUpdatedByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicFeeTransChargesItemCompMetadata Meta()
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
			 public const string PhysicianFeePeriod = "PhysicianFeePeriod";
			 public const string ParamedicID = "ParamedicID";
			 public const string PhysicianFeeAmount = "PhysicianFeeAmount";
			 public const string IncPremi1Amount = "IncPremi1Amount";
			 public const string IncPremi2Amount = "IncPremi2Amount";
			 public const string DecProductionServicesAmount = "DecProductionServicesAmount";
			 public const string DecTogethernessAmount = "DecTogethernessAmount";
			 public const string DecRentalRoomsAmount = "DecRentalRoomsAmount";
			 public const string RentalRoomsToParamedicID = "RentalRoomsToParamedicID";
			 public const string VerificationNo = "VerificationNo";
			 public const string LastCalculatedDateTime = "LastCalculatedDateTime";
			 public const string LastCalculatedByUserID = "LastCalculatedByUserID";
			 public const string LastUpdatedDateTime = "LastUpdatedDateTime";
			 public const string LastUpdatedByUserID = "LastUpdatedByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransactionNo = "TransactionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string PhysicianFeePeriod = "PhysicianFeePeriod";
			 public const string ParamedicID = "ParamedicID";
			 public const string PhysicianFeeAmount = "PhysicianFeeAmount";
			 public const string IncPremi1Amount = "IncPremi1Amount";
			 public const string IncPremi2Amount = "IncPremi2Amount";
			 public const string DecProductionServicesAmount = "DecProductionServicesAmount";
			 public const string DecTogethernessAmount = "DecTogethernessAmount";
			 public const string DecRentalRoomsAmount = "DecRentalRoomsAmount";
			 public const string RentalRoomsToParamedicID = "RentalRoomsToParamedicID";
			 public const string VerificationNo = "VerificationNo";
			 public const string LastCalculatedDateTime = "LastCalculatedDateTime";
			 public const string LastCalculatedByUserID = "LastCalculatedByUserID";
			 public const string LastUpdatedDateTime = "LastUpdatedDateTime";
			 public const string LastUpdatedByUserID = "LastUpdatedByUserID";
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
			lock (typeof(ParamedicFeeTransChargesItemCompMetadata))
			{
				if(ParamedicFeeTransChargesItemCompMetadata.mapDelegates == null)
				{
					ParamedicFeeTransChargesItemCompMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeTransChargesItemCompMetadata.meta == null)
				{
					ParamedicFeeTransChargesItemCompMetadata.meta = new ParamedicFeeTransChargesItemCompMetadata();
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
				meta.AddTypeMap("PhysicianFeePeriod", new esTypeMap("date", "System.DateTime"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhysicianFeeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IncPremi1Amount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IncPremi2Amount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DecProductionServicesAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DecTogethernessAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DecRentalRoomsAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RentalRoomsToParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerificationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastCalculatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastCalculatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdatedByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ParamedicFeeTransChargesItemComp";
				meta.Destination = "ParamedicFeeTransChargesItemComp";
				
				meta.spInsert = "proc_ParamedicFeeTransChargesItemCompInsert";				
				meta.spUpdate = "proc_ParamedicFeeTransChargesItemCompUpdate";		
				meta.spDelete = "proc_ParamedicFeeTransChargesItemCompDelete";
				meta.spLoadAll = "proc_ParamedicFeeTransChargesItemCompLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeTransChargesItemCompLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeTransChargesItemCompMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
