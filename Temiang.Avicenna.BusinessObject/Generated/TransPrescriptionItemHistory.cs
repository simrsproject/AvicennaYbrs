/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/27/2012 1:34:08 PM
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
	abstract public class esTransPrescriptionItemHistoryCollection : esEntityCollectionWAuditLog
	{
		public esTransPrescriptionItemHistoryCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TransPrescriptionItemHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransPrescriptionItemHistoryQuery query)
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
			this.InitQuery(query as esTransPrescriptionItemHistoryQuery);
		}
		#endregion
		
		virtual public TransPrescriptionItemHistory DetachEntity(TransPrescriptionItemHistory entity)
		{
			return base.DetachEntity(entity) as TransPrescriptionItemHistory;
		}
		
		virtual public TransPrescriptionItemHistory AttachEntity(TransPrescriptionItemHistory entity)
		{
			return base.AttachEntity(entity) as TransPrescriptionItemHistory;
		}
		
		virtual public void Combine(TransPrescriptionItemHistoryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransPrescriptionItemHistory this[int index]
		{
			get
			{
				return base[index] as TransPrescriptionItemHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransPrescriptionItemHistory);
		}
	}



	[Serializable]
	abstract public class esTransPrescriptionItemHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransPrescriptionItemHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransPrescriptionItemHistory()
		{

		}

		public esTransPrescriptionItemHistory(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String recalculationProcessNo, System.String prescriptionNo, System.String sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(recalculationProcessNo, prescriptionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(recalculationProcessNo, prescriptionNo, sequenceNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String recalculationProcessNo, System.String prescriptionNo, System.String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(recalculationProcessNo, prescriptionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(recalculationProcessNo, prescriptionNo, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String recalculationProcessNo, System.String prescriptionNo, System.String sequenceNo)
		{
			esTransPrescriptionItemHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.RecalculationProcessNo == recalculationProcessNo, query.PrescriptionNo == prescriptionNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String recalculationProcessNo, System.String prescriptionNo, System.String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RecalculationProcessNo",recalculationProcessNo);			parms.Add("PrescriptionNo",prescriptionNo);			parms.Add("SequenceNo",sequenceNo);
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
						case "PrescriptionNo": this.str.PrescriptionNo = (string)value; break;							
						case "SequenceNo": this.str.SequenceNo = (string)value; break;							
						case "ParentNo": this.str.ParentNo = (string)value; break;							
						case "IsRFlag": this.str.IsRFlag = (string)value; break;							
						case "IsCompound": this.str.IsCompound = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "ItemInterventionID": this.str.ItemInterventionID = (string)value; break;							
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;							
						case "ItemQtyInString": this.str.ItemQtyInString = (string)value; break;							
						case "IsUsingDosageUnit": this.str.IsUsingDosageUnit = (string)value; break;							
						case "SRDosageUnit": this.str.SRDosageUnit = (string)value; break;							
						case "FrequencyOfDosing": this.str.FrequencyOfDosing = (string)value; break;							
						case "DosingPeriod": this.str.DosingPeriod = (string)value; break;							
						case "NumberOfDosage": this.str.NumberOfDosage = (string)value; break;							
						case "DurationOfDosing": this.str.DurationOfDosing = (string)value; break;							
						case "Acpcdc": this.str.Acpcdc = (string)value; break;							
						case "SRMedicationRoute": this.str.SRMedicationRoute = (string)value; break;							
						case "ConsumeMethod": this.str.ConsumeMethod = (string)value; break;							
						case "PrescriptionQty": this.str.PrescriptionQty = (string)value; break;							
						case "TakenQty": this.str.TakenQty = (string)value; break;							
						case "ResultQty": this.str.ResultQty = (string)value; break;							
						case "CostPrice": this.str.CostPrice = (string)value; break;							
						case "InitialPrice": this.str.InitialPrice = (string)value; break;							
						case "Price": this.str.Price = (string)value; break;							
						case "DiscountAmount": this.str.DiscountAmount = (string)value; break;							
						case "EmbalaceID": this.str.EmbalaceID = (string)value; break;							
						case "EmbalaceAmount": this.str.EmbalaceAmount = (string)value; break;							
						case "IsUseSweetener": this.str.IsUseSweetener = (string)value; break;							
						case "SweetenerAmount": this.str.SweetenerAmount = (string)value; break;							
						case "LineAmount": this.str.LineAmount = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "SRDiscountReason": this.str.SRDiscountReason = (string)value; break;							
						case "IsApprove": this.str.IsApprove = (string)value; break;							
						case "IsVoid": this.str.IsVoid = (string)value; break;							
						case "IsBillProceed": this.str.IsBillProceed = (string)value; break;							
						case "DurationRelease": this.str.DurationRelease = (string)value; break;							
						case "AutoProcessCalculation": this.str.AutoProcessCalculation = (string)value; break;							
						case "ConsumeMethodText": this.str.ConsumeMethodText = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsRFlag":
						
							if (value == null || value is System.Boolean)
								this.IsRFlag = (System.Boolean?)value;
							break;
						
						case "IsCompound":
						
							if (value == null || value is System.Boolean)
								this.IsCompound = (System.Boolean?)value;
							break;
						
						case "IsUsingDosageUnit":
						
							if (value == null || value is System.Boolean)
								this.IsUsingDosageUnit = (System.Boolean?)value;
							break;
						
						case "FrequencyOfDosing":
						
							if (value == null || value is System.Byte)
								this.FrequencyOfDosing = (System.Byte?)value;
							break;
						
						case "NumberOfDosage":
						
							if (value == null || value is System.Decimal)
								this.NumberOfDosage = (System.Decimal?)value;
							break;
						
						case "DurationOfDosing":
						
							if (value == null || value is System.Byte)
								this.DurationOfDosing = (System.Byte?)value;
							break;
						
						case "PrescriptionQty":
						
							if (value == null || value is System.Decimal)
								this.PrescriptionQty = (System.Decimal?)value;
							break;
						
						case "TakenQty":
						
							if (value == null || value is System.Decimal)
								this.TakenQty = (System.Decimal?)value;
							break;
						
						case "ResultQty":
						
							if (value == null || value is System.Decimal)
								this.ResultQty = (System.Decimal?)value;
							break;
						
						case "CostPrice":
						
							if (value == null || value is System.Decimal)
								this.CostPrice = (System.Decimal?)value;
							break;
						
						case "InitialPrice":
						
							if (value == null || value is System.Decimal)
								this.InitialPrice = (System.Decimal?)value;
							break;
						
						case "Price":
						
							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
							break;
						
						case "DiscountAmount":
						
							if (value == null || value is System.Decimal)
								this.DiscountAmount = (System.Decimal?)value;
							break;
						
						case "EmbalaceAmount":
						
							if (value == null || value is System.Decimal)
								this.EmbalaceAmount = (System.Decimal?)value;
							break;
						
						case "IsUseSweetener":
						
							if (value == null || value is System.Boolean)
								this.IsUseSweetener = (System.Boolean?)value;
							break;
						
						case "SweetenerAmount":
						
							if (value == null || value is System.Decimal)
								this.SweetenerAmount = (System.Decimal?)value;
							break;
						
						case "LineAmount":
						
							if (value == null || value is System.Decimal)
								this.LineAmount = (System.Decimal?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "IsApprove":
						
							if (value == null || value is System.Boolean)
								this.IsApprove = (System.Boolean?)value;
							break;
						
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						
						case "IsBillProceed":
						
							if (value == null || value is System.Boolean)
								this.IsBillProceed = (System.Boolean?)value;
							break;
						
						case "DurationRelease":
						
							if (value == null || value is System.Decimal)
								this.DurationRelease = (System.Decimal?)value;
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
		/// Maps to TransPrescriptionItemHistory.RecalculationProcessNo
		/// </summary>
		virtual public System.String RecalculationProcessNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.RecalculationProcessNo);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.RecalculationProcessNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.PrescriptionNo
		/// </summary>
		virtual public System.String PrescriptionNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.PrescriptionNo);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.PrescriptionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.ParentNo
		/// </summary>
		virtual public System.String ParentNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.ParentNo);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.ParentNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.IsRFlag
		/// </summary>
		virtual public System.Boolean? IsRFlag
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemHistoryMetadata.ColumnNames.IsRFlag);
			}
			
			set
			{
				base.SetSystemBoolean(TransPrescriptionItemHistoryMetadata.ColumnNames.IsRFlag, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.IsCompound
		/// </summary>
		virtual public System.Boolean? IsCompound
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemHistoryMetadata.ColumnNames.IsCompound);
			}
			
			set
			{
				base.SetSystemBoolean(TransPrescriptionItemHistoryMetadata.ColumnNames.IsCompound, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.ItemInterventionID
		/// </summary>
		virtual public System.String ItemInterventionID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.ItemInterventionID);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.ItemInterventionID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.SRItemUnit);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.ItemQtyInString
		/// </summary>
		virtual public System.String ItemQtyInString
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.ItemQtyInString);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.ItemQtyInString, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.IsUsingDosageUnit
		/// </summary>
		virtual public System.Boolean? IsUsingDosageUnit
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemHistoryMetadata.ColumnNames.IsUsingDosageUnit);
			}
			
			set
			{
				base.SetSystemBoolean(TransPrescriptionItemHistoryMetadata.ColumnNames.IsUsingDosageUnit, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.SRDosageUnit
		/// </summary>
		virtual public System.String SRDosageUnit
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.SRDosageUnit);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.SRDosageUnit, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.FrequencyOfDosing
		/// </summary>
		virtual public System.Byte? FrequencyOfDosing
		{
			get
			{
				return base.GetSystemByte(TransPrescriptionItemHistoryMetadata.ColumnNames.FrequencyOfDosing);
			}
			
			set
			{
				base.SetSystemByte(TransPrescriptionItemHistoryMetadata.ColumnNames.FrequencyOfDosing, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.DosingPeriod
		/// </summary>
		virtual public System.String DosingPeriod
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.DosingPeriod);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.DosingPeriod, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.NumberOfDosage
		/// </summary>
		virtual public System.Decimal? NumberOfDosage
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.NumberOfDosage);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.NumberOfDosage, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.DurationOfDosing
		/// </summary>
		virtual public System.Byte? DurationOfDosing
		{
			get
			{
				return base.GetSystemByte(TransPrescriptionItemHistoryMetadata.ColumnNames.DurationOfDosing);
			}
			
			set
			{
				base.SetSystemByte(TransPrescriptionItemHistoryMetadata.ColumnNames.DurationOfDosing, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.ACPCDC
		/// </summary>
		virtual public System.String Acpcdc
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.Acpcdc);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.Acpcdc, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.SRMedicationRoute
		/// </summary>
		virtual public System.String SRMedicationRoute
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.SRMedicationRoute);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.SRMedicationRoute, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.ConsumeMethod
		/// </summary>
		virtual public System.String ConsumeMethod
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.ConsumeMethod);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.ConsumeMethod, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.PrescriptionQty
		/// </summary>
		virtual public System.Decimal? PrescriptionQty
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.PrescriptionQty);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.PrescriptionQty, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.TakenQty
		/// </summary>
		virtual public System.Decimal? TakenQty
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.TakenQty);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.TakenQty, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.ResultQty
		/// </summary>
		virtual public System.Decimal? ResultQty
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.ResultQty);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.ResultQty, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.CostPrice
		/// </summary>
		virtual public System.Decimal? CostPrice
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.CostPrice);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.CostPrice, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.InitialPrice
		/// </summary>
		virtual public System.Decimal? InitialPrice
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.InitialPrice);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.InitialPrice, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.Price, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.DiscountAmount
		/// </summary>
		virtual public System.Decimal? DiscountAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.DiscountAmount);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.DiscountAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.EmbalaceID
		/// </summary>
		virtual public System.String EmbalaceID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.EmbalaceID);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.EmbalaceID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.EmbalaceAmount
		/// </summary>
		virtual public System.Decimal? EmbalaceAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.EmbalaceAmount);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.EmbalaceAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.IsUseSweetener
		/// </summary>
		virtual public System.Boolean? IsUseSweetener
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemHistoryMetadata.ColumnNames.IsUseSweetener);
			}
			
			set
			{
				base.SetSystemBoolean(TransPrescriptionItemHistoryMetadata.ColumnNames.IsUseSweetener, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.SweetenerAmount
		/// </summary>
		virtual public System.Decimal? SweetenerAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.SweetenerAmount);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.SweetenerAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.LineAmount
		/// </summary>
		virtual public System.Decimal? LineAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.LineAmount);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.LineAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionItemHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransPrescriptionItemHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.SRDiscountReason
		/// </summary>
		virtual public System.String SRDiscountReason
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.SRDiscountReason);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.SRDiscountReason, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.IsApprove
		/// </summary>
		virtual public System.Boolean? IsApprove
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemHistoryMetadata.ColumnNames.IsApprove);
			}
			
			set
			{
				base.SetSystemBoolean(TransPrescriptionItemHistoryMetadata.ColumnNames.IsApprove, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemHistoryMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(TransPrescriptionItemHistoryMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.IsBillProceed
		/// </summary>
		virtual public System.Boolean? IsBillProceed
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemHistoryMetadata.ColumnNames.IsBillProceed);
			}
			
			set
			{
				base.SetSystemBoolean(TransPrescriptionItemHistoryMetadata.ColumnNames.IsBillProceed, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.DurationRelease
		/// </summary>
		virtual public System.Decimal? DurationRelease
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.DurationRelease);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.DurationRelease, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.AutoProcessCalculation
		/// </summary>
		virtual public System.Decimal? AutoProcessCalculation
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.AutoProcessCalculation);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemHistoryMetadata.ColumnNames.AutoProcessCalculation, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemHistory.ConsumeMethodText
		/// </summary>
		virtual public System.String ConsumeMethodText
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.ConsumeMethodText);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemHistoryMetadata.ColumnNames.ConsumeMethodText, value);
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
			public esStrings(esTransPrescriptionItemHistory entity)
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
				
			public System.String PrescriptionNo
			{
				get
				{
					System.String data = entity.PrescriptionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrescriptionNo = null;
					else entity.PrescriptionNo = Convert.ToString(value);
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
				
			public System.String ParentNo
			{
				get
				{
					System.String data = entity.ParentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParentNo = null;
					else entity.ParentNo = Convert.ToString(value);
				}
			}
				
			public System.String IsRFlag
			{
				get
				{
					System.Boolean? data = entity.IsRFlag;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRFlag = null;
					else entity.IsRFlag = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsCompound
			{
				get
				{
					System.Boolean? data = entity.IsCompound;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCompound = null;
					else entity.IsCompound = Convert.ToBoolean(value);
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
				
			public System.String ItemInterventionID
			{
				get
				{
					System.String data = entity.ItemInterventionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemInterventionID = null;
					else entity.ItemInterventionID = Convert.ToString(value);
				}
			}
				
			public System.String SRItemUnit
			{
				get
				{
					System.String data = entity.SRItemUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemUnit = null;
					else entity.SRItemUnit = Convert.ToString(value);
				}
			}
				
			public System.String ItemQtyInString
			{
				get
				{
					System.String data = entity.ItemQtyInString;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemQtyInString = null;
					else entity.ItemQtyInString = Convert.ToString(value);
				}
			}
				
			public System.String IsUsingDosageUnit
			{
				get
				{
					System.Boolean? data = entity.IsUsingDosageUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUsingDosageUnit = null;
					else entity.IsUsingDosageUnit = Convert.ToBoolean(value);
				}
			}
				
			public System.String SRDosageUnit
			{
				get
				{
					System.String data = entity.SRDosageUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDosageUnit = null;
					else entity.SRDosageUnit = Convert.ToString(value);
				}
			}
				
			public System.String FrequencyOfDosing
			{
				get
				{
					System.Byte? data = entity.FrequencyOfDosing;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FrequencyOfDosing = null;
					else entity.FrequencyOfDosing = Convert.ToByte(value);
				}
			}
				
			public System.String DosingPeriod
			{
				get
				{
					System.String data = entity.DosingPeriod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DosingPeriod = null;
					else entity.DosingPeriod = Convert.ToString(value);
				}
			}
				
			public System.String NumberOfDosage
			{
				get
				{
					System.Decimal? data = entity.NumberOfDosage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NumberOfDosage = null;
					else entity.NumberOfDosage = Convert.ToDecimal(value);
				}
			}
				
			public System.String DurationOfDosing
			{
				get
				{
					System.Byte? data = entity.DurationOfDosing;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DurationOfDosing = null;
					else entity.DurationOfDosing = Convert.ToByte(value);
				}
			}
				
			public System.String Acpcdc
			{
				get
				{
					System.String data = entity.Acpcdc;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Acpcdc = null;
					else entity.Acpcdc = Convert.ToString(value);
				}
			}
				
			public System.String SRMedicationRoute
			{
				get
				{
					System.String data = entity.SRMedicationRoute;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMedicationRoute = null;
					else entity.SRMedicationRoute = Convert.ToString(value);
				}
			}
				
			public System.String ConsumeMethod
			{
				get
				{
					System.String data = entity.ConsumeMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConsumeMethod = null;
					else entity.ConsumeMethod = Convert.ToString(value);
				}
			}
				
			public System.String PrescriptionQty
			{
				get
				{
					System.Decimal? data = entity.PrescriptionQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrescriptionQty = null;
					else entity.PrescriptionQty = Convert.ToDecimal(value);
				}
			}
				
			public System.String TakenQty
			{
				get
				{
					System.Decimal? data = entity.TakenQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TakenQty = null;
					else entity.TakenQty = Convert.ToDecimal(value);
				}
			}
				
			public System.String ResultQty
			{
				get
				{
					System.Decimal? data = entity.ResultQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultQty = null;
					else entity.ResultQty = Convert.ToDecimal(value);
				}
			}
				
			public System.String CostPrice
			{
				get
				{
					System.Decimal? data = entity.CostPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CostPrice = null;
					else entity.CostPrice = Convert.ToDecimal(value);
				}
			}
				
			public System.String InitialPrice
			{
				get
				{
					System.Decimal? data = entity.InitialPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InitialPrice = null;
					else entity.InitialPrice = Convert.ToDecimal(value);
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
				
			public System.String EmbalaceID
			{
				get
				{
					System.String data = entity.EmbalaceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmbalaceID = null;
					else entity.EmbalaceID = Convert.ToString(value);
				}
			}
				
			public System.String EmbalaceAmount
			{
				get
				{
					System.Decimal? data = entity.EmbalaceAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmbalaceAmount = null;
					else entity.EmbalaceAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String IsUseSweetener
			{
				get
				{
					System.Boolean? data = entity.IsUseSweetener;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUseSweetener = null;
					else entity.IsUseSweetener = Convert.ToBoolean(value);
				}
			}
				
			public System.String SweetenerAmount
			{
				get
				{
					System.Decimal? data = entity.SweetenerAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SweetenerAmount = null;
					else entity.SweetenerAmount = Convert.ToDecimal(value);
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
				
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
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
				
			public System.String SRDiscountReason
			{
				get
				{
					System.String data = entity.SRDiscountReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDiscountReason = null;
					else entity.SRDiscountReason = Convert.ToString(value);
				}
			}
				
			public System.String IsApprove
			{
				get
				{
					System.Boolean? data = entity.IsApprove;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApprove = null;
					else entity.IsApprove = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsBillProceed
			{
				get
				{
					System.Boolean? data = entity.IsBillProceed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBillProceed = null;
					else entity.IsBillProceed = Convert.ToBoolean(value);
				}
			}
				
			public System.String DurationRelease
			{
				get
				{
					System.Decimal? data = entity.DurationRelease;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DurationRelease = null;
					else entity.DurationRelease = Convert.ToDecimal(value);
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
				
			public System.String ConsumeMethodText
			{
				get
				{
					System.String data = entity.ConsumeMethodText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConsumeMethodText = null;
					else entity.ConsumeMethodText = Convert.ToString(value);
				}
			}
			

			private esTransPrescriptionItemHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransPrescriptionItemHistoryQuery query)
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
				throw new Exception("esTransPrescriptionItemHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class TransPrescriptionItemHistory : esTransPrescriptionItemHistory
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
	abstract public class esTransPrescriptionItemHistoryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TransPrescriptionItemHistoryMetadata.Meta();
			}
		}	
		

		public esQueryItem RecalculationProcessNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.RecalculationProcessNo, esSystemType.String);
			}
		} 
		
		public esQueryItem PrescriptionNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.PrescriptionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ParentNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.ParentNo, esSystemType.String);
			}
		} 
		
		public esQueryItem IsRFlag
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.IsRFlag, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsCompound
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.IsCompound, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemInterventionID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.ItemInterventionID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemQtyInString
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.ItemQtyInString, esSystemType.String);
			}
		} 
		
		public esQueryItem IsUsingDosageUnit
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.IsUsingDosageUnit, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem SRDosageUnit
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.SRDosageUnit, esSystemType.String);
			}
		} 
		
		public esQueryItem FrequencyOfDosing
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.FrequencyOfDosing, esSystemType.Byte);
			}
		} 
		
		public esQueryItem DosingPeriod
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.DosingPeriod, esSystemType.String);
			}
		} 
		
		public esQueryItem NumberOfDosage
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.NumberOfDosage, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DurationOfDosing
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.DurationOfDosing, esSystemType.Byte);
			}
		} 
		
		public esQueryItem Acpcdc
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.Acpcdc, esSystemType.String);
			}
		} 
		
		public esQueryItem SRMedicationRoute
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.SRMedicationRoute, esSystemType.String);
			}
		} 
		
		public esQueryItem ConsumeMethod
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.ConsumeMethod, esSystemType.String);
			}
		} 
		
		public esQueryItem PrescriptionQty
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.PrescriptionQty, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem TakenQty
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.TakenQty, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ResultQty
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.ResultQty, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CostPrice
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem InitialPrice
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.InitialPrice, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DiscountAmount
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.DiscountAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem EmbalaceID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.EmbalaceID, esSystemType.String);
			}
		} 
		
		public esQueryItem EmbalaceAmount
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.EmbalaceAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsUseSweetener
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.IsUseSweetener, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem SweetenerAmount
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.SweetenerAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LineAmount
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.LineAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRDiscountReason
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.SRDiscountReason, esSystemType.String);
			}
		} 
		
		public esQueryItem IsApprove
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.IsApprove, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsBillProceed
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.IsBillProceed, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem DurationRelease
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.DurationRelease, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem AutoProcessCalculation
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.AutoProcessCalculation, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ConsumeMethodText
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemHistoryMetadata.ColumnNames.ConsumeMethodText, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransPrescriptionItemHistoryCollection")]
	public partial class TransPrescriptionItemHistoryCollection : esTransPrescriptionItemHistoryCollection, IEnumerable<TransPrescriptionItemHistory>
	{
		public TransPrescriptionItemHistoryCollection()
		{

		}
		
		public static implicit operator List<TransPrescriptionItemHistory>(TransPrescriptionItemHistoryCollection coll)
		{
			List<TransPrescriptionItemHistory> list = new List<TransPrescriptionItemHistory>();
			
			foreach (TransPrescriptionItemHistory emp in coll)
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
				return  TransPrescriptionItemHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPrescriptionItemHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransPrescriptionItemHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransPrescriptionItemHistory();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TransPrescriptionItemHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPrescriptionItemHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TransPrescriptionItemHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public TransPrescriptionItemHistory AddNew()
		{
			TransPrescriptionItemHistory entity = base.AddNewEntity() as TransPrescriptionItemHistory;
			
			return entity;
		}

		public TransPrescriptionItemHistory FindByPrimaryKey(System.String recalculationProcessNo, System.String prescriptionNo, System.String sequenceNo)
		{
			return base.FindByPrimaryKey(recalculationProcessNo, prescriptionNo, sequenceNo) as TransPrescriptionItemHistory;
		}


		#region IEnumerable<TransPrescriptionItemHistory> Members

		IEnumerator<TransPrescriptionItemHistory> IEnumerable<TransPrescriptionItemHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransPrescriptionItemHistory;
			}
		}

		#endregion
		
		private TransPrescriptionItemHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransPrescriptionItemHistory' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("TransPrescriptionItemHistory ({RecalculationProcessNo},{PrescriptionNo},{SequenceNo})")]
	[Serializable]
	public partial class TransPrescriptionItemHistory : esTransPrescriptionItemHistory
	{
		public TransPrescriptionItemHistory()
		{

		}
	
		public TransPrescriptionItemHistory(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransPrescriptionItemHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esTransPrescriptionItemHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPrescriptionItemHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TransPrescriptionItemHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPrescriptionItemHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TransPrescriptionItemHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TransPrescriptionItemHistoryQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TransPrescriptionItemHistoryQuery : esTransPrescriptionItemHistoryQuery
	{
		public TransPrescriptionItemHistoryQuery()
		{

		}		
		
		public TransPrescriptionItemHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TransPrescriptionItemHistoryQuery";
        }
		
			
	}


	[Serializable]
	public partial class TransPrescriptionItemHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransPrescriptionItemHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.RecalculationProcessNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.RecalculationProcessNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.PrescriptionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.PrescriptionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.ParentNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.ParentNo;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.IsRFlag, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.IsRFlag;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.IsCompound, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.IsCompound;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.ItemID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.ItemInterventionID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.ItemInterventionID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.SRItemUnit, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.ItemQtyInString, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.ItemQtyInString;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.IsUsingDosageUnit, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.IsUsingDosageUnit;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.SRDosageUnit, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.SRDosageUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.FrequencyOfDosing, 12, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.FrequencyOfDosing;
			c.NumericPrecision = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.DosingPeriod, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.DosingPeriod;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.NumberOfDosage, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.NumberOfDosage;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.DurationOfDosing, 15, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.DurationOfDosing;
			c.NumericPrecision = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.Acpcdc, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.Acpcdc;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.SRMedicationRoute, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.SRMedicationRoute;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.ConsumeMethod, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.ConsumeMethod;
			c.CharacterMaxLength = 500;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.PrescriptionQty, 19, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.PrescriptionQty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.TakenQty, 20, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.TakenQty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.ResultQty, 21, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.ResultQty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.CostPrice, 22, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.CostPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.InitialPrice, 23, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.InitialPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.Price, 24, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.DiscountAmount, 25, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.DiscountAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.EmbalaceID, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.EmbalaceID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.EmbalaceAmount, 27, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.EmbalaceAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.IsUseSweetener, 28, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.IsUseSweetener;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.SweetenerAmount, 29, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.SweetenerAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.LineAmount, 30, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.LineAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.Notes, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.LastUpdateDateTime, 32, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.LastUpdateByUserID, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.SRDiscountReason, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.SRDiscountReason;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.IsApprove, 35, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.IsApprove;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.IsVoid, 36, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.IsVoid;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.IsBillProceed, 37, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.IsBillProceed;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.DurationRelease, 38, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.DurationRelease;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.AutoProcessCalculation, 39, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.AutoProcessCalculation;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemHistoryMetadata.ColumnNames.ConsumeMethodText, 40, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemHistoryMetadata.PropertyNames.ConsumeMethodText;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TransPrescriptionItemHistoryMetadata Meta()
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
			 public const string PrescriptionNo = "PrescriptionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string ParentNo = "ParentNo";
			 public const string IsRFlag = "IsRFlag";
			 public const string IsCompound = "IsCompound";
			 public const string ItemID = "ItemID";
			 public const string ItemInterventionID = "ItemInterventionID";
			 public const string SRItemUnit = "SRItemUnit";
			 public const string ItemQtyInString = "ItemQtyInString";
			 public const string IsUsingDosageUnit = "IsUsingDosageUnit";
			 public const string SRDosageUnit = "SRDosageUnit";
			 public const string FrequencyOfDosing = "FrequencyOfDosing";
			 public const string DosingPeriod = "DosingPeriod";
			 public const string NumberOfDosage = "NumberOfDosage";
			 public const string DurationOfDosing = "DurationOfDosing";
			 public const string Acpcdc = "ACPCDC";
			 public const string SRMedicationRoute = "SRMedicationRoute";
			 public const string ConsumeMethod = "ConsumeMethod";
			 public const string PrescriptionQty = "PrescriptionQty";
			 public const string TakenQty = "TakenQty";
			 public const string ResultQty = "ResultQty";
			 public const string CostPrice = "CostPrice";
			 public const string InitialPrice = "InitialPrice";
			 public const string Price = "Price";
			 public const string DiscountAmount = "DiscountAmount";
			 public const string EmbalaceID = "EmbalaceID";
			 public const string EmbalaceAmount = "EmbalaceAmount";
			 public const string IsUseSweetener = "IsUseSweetener";
			 public const string SweetenerAmount = "SweetenerAmount";
			 public const string LineAmount = "LineAmount";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string SRDiscountReason = "SRDiscountReason";
			 public const string IsApprove = "IsApprove";
			 public const string IsVoid = "IsVoid";
			 public const string IsBillProceed = "IsBillProceed";
			 public const string DurationRelease = "DurationRelease";
			 public const string AutoProcessCalculation = "AutoProcessCalculation";
			 public const string ConsumeMethodText = "ConsumeMethodText";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RecalculationProcessNo = "RecalculationProcessNo";
			 public const string PrescriptionNo = "PrescriptionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string ParentNo = "ParentNo";
			 public const string IsRFlag = "IsRFlag";
			 public const string IsCompound = "IsCompound";
			 public const string ItemID = "ItemID";
			 public const string ItemInterventionID = "ItemInterventionID";
			 public const string SRItemUnit = "SRItemUnit";
			 public const string ItemQtyInString = "ItemQtyInString";
			 public const string IsUsingDosageUnit = "IsUsingDosageUnit";
			 public const string SRDosageUnit = "SRDosageUnit";
			 public const string FrequencyOfDosing = "FrequencyOfDosing";
			 public const string DosingPeriod = "DosingPeriod";
			 public const string NumberOfDosage = "NumberOfDosage";
			 public const string DurationOfDosing = "DurationOfDosing";
			 public const string Acpcdc = "Acpcdc";
			 public const string SRMedicationRoute = "SRMedicationRoute";
			 public const string ConsumeMethod = "ConsumeMethod";
			 public const string PrescriptionQty = "PrescriptionQty";
			 public const string TakenQty = "TakenQty";
			 public const string ResultQty = "ResultQty";
			 public const string CostPrice = "CostPrice";
			 public const string InitialPrice = "InitialPrice";
			 public const string Price = "Price";
			 public const string DiscountAmount = "DiscountAmount";
			 public const string EmbalaceID = "EmbalaceID";
			 public const string EmbalaceAmount = "EmbalaceAmount";
			 public const string IsUseSweetener = "IsUseSweetener";
			 public const string SweetenerAmount = "SweetenerAmount";
			 public const string LineAmount = "LineAmount";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string SRDiscountReason = "SRDiscountReason";
			 public const string IsApprove = "IsApprove";
			 public const string IsVoid = "IsVoid";
			 public const string IsBillProceed = "IsBillProceed";
			 public const string DurationRelease = "DurationRelease";
			 public const string AutoProcessCalculation = "AutoProcessCalculation";
			 public const string ConsumeMethodText = "ConsumeMethodText";
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
			lock (typeof(TransPrescriptionItemHistoryMetadata))
			{
				if(TransPrescriptionItemHistoryMetadata.mapDelegates == null)
				{
					TransPrescriptionItemHistoryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransPrescriptionItemHistoryMetadata.meta == null)
				{
					TransPrescriptionItemHistoryMetadata.meta = new TransPrescriptionItemHistoryMetadata();
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
				meta.AddTypeMap("PrescriptionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRFlag", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCompound", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemInterventionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemQtyInString", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsUsingDosageUnit", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRDosageUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FrequencyOfDosing", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("DosingPeriod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NumberOfDosage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DurationOfDosing", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("Acpcdc", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMedicationRoute", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConsumeMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrescriptionQty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TakenQty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ResultQty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CostPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("InitialPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DiscountAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("EmbalaceID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmbalaceAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsUseSweetener", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SweetenerAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LineAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDiscountReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApprove", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBillProceed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DurationRelease", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AutoProcessCalculation", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ConsumeMethodText", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "TransPrescriptionItemHistory";
				meta.Destination = "TransPrescriptionItemHistory";
				
				meta.spInsert = "proc_TransPrescriptionItemHistoryInsert";				
				meta.spUpdate = "proc_TransPrescriptionItemHistoryUpdate";		
				meta.spDelete = "proc_TransPrescriptionItemHistoryDelete";
				meta.spLoadAll = "proc_TransPrescriptionItemHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransPrescriptionItemHistoryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransPrescriptionItemHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
