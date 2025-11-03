/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/8/2023 4:13:35 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
	[Serializable]
	abstract public class esTransPrescriptionItemCollection : esEntityCollectionWAuditLog
	{
		public esTransPrescriptionItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "TransPrescriptionItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransPrescriptionItemQuery query)
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
			this.InitQuery(query as esTransPrescriptionItemQuery);
		}
		#endregion

		virtual public TransPrescriptionItem DetachEntity(TransPrescriptionItem entity)
		{
			return base.DetachEntity(entity) as TransPrescriptionItem;
		}

		virtual public TransPrescriptionItem AttachEntity(TransPrescriptionItem entity)
		{
			return base.AttachEntity(entity) as TransPrescriptionItem;
		}

		virtual public void Combine(TransPrescriptionItemCollection collection)
		{
			base.Combine(collection);
		}

		new public TransPrescriptionItem this[int index]
		{
			get
			{
				return base[index] as TransPrescriptionItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransPrescriptionItem);
		}
	}

	[Serializable]
	abstract public class esTransPrescriptionItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransPrescriptionItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransPrescriptionItem()
		{
		}

		public esTransPrescriptionItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String prescriptionNo, String sequenceNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(prescriptionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(prescriptionNo, sequenceNo);
		}

		/// <summary>
		/// Loads an entity by primary key
		/// </summary>
		/// <remarks>
		/// Requires primary keys be defined on all tables.
		/// If a table does not have a primary key set,
		/// this method will not compile.
		/// </remarks>
		/// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String prescriptionNo, String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(prescriptionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(prescriptionNo, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(String prescriptionNo, String sequenceNo)
		{
			esTransPrescriptionItemQuery query = this.GetDynamicQuery();
			query.Where(query.PrescriptionNo == prescriptionNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String prescriptionNo, String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("PrescriptionNo", prescriptionNo);
			parms.Add("SequenceNo", sequenceNo);
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
			if (this.Row == null) this.AddNew();

			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if (value == null || value is System.String)
				{
					// Use the strongly typed property
					switch (name)
					{
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
						case "RecipeAmount": this.str.RecipeAmount = (string)value; break;
						case "IsCalcPercentage": this.str.IsCalcPercentage = (string)value; break;
						case "AutoProcessCalculation": this.str.AutoProcessCalculation = (string)value; break;
						case "IsUsingAdminReturn": this.str.IsUsingAdminReturn = (string)value; break;
						case "VerifiedByUserID": this.str.VerifiedByUserID = (string)value; break;
						case "VerifiedDateTime": this.str.VerifiedDateTime = (string)value; break;
						case "LastUpdateByUserHostName": this.str.LastUpdateByUserHostName = (string)value; break;
						case "VerifiedByUserHostName": this.str.VerifiedByUserHostName = (string)value; break;
						case "SRConsumeMethod": this.str.SRConsumeMethod = (string)value; break;
						case "DosageQty": this.str.DosageQty = (string)value; break;
						case "EmbalaceQty": this.str.EmbalaceQty = (string)value; break;
						case "IterText": this.str.IterText = (string)value; break;
						case "OrderText": this.str.OrderText = (string)value; break;
						case "ConsumeQty": this.str.ConsumeQty = (string)value; break;
						case "SRConsumeUnit": this.str.SRConsumeUnit = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "ReferenceSequenceNo": this.str.ReferenceSequenceNo = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "OriPrescriptionQty": this.str.OriPrescriptionQty = (string)value; break;
						case "OriConsumeQty": this.str.OriConsumeQty = (string)value; break;
						case "OriSRConsumeUnit": this.str.OriSRConsumeUnit = (string)value; break;
						case "OriResultQty": this.str.OriResultQty = (string)value; break;
						case "OriItemQtyInString": this.str.OriItemQtyInString = (string)value; break;
						case "OriSRItemUnit": this.str.OriSRItemUnit = (string)value; break;
						case "OriDosageQty": this.str.OriDosageQty = (string)value; break;
						case "OriSRDosageUnit": this.str.OriSRDosageUnit = (string)value; break;
						case "OriSRConsumeMethod": this.str.OriSRConsumeMethod = (string)value; break;
						case "IsReturned": this.str.IsReturned = (string)value; break;
						case "IsPendingDelivery": this.str.IsPendingDelivery = (string)value; break;
						case "DeliveryQty": this.str.DeliveryQty = (string)value; break;
						case "DaysOfUsage": this.str.DaysOfUsage = (string)value; break;
						case "IsCasemixApproved": this.str.IsCasemixApproved = (string)value; break;
						case "CasemixApprovedDateTime": this.str.CasemixApprovedDateTime = (string)value; break;
						case "CasemixApprovedByUserID": this.str.CasemixApprovedByUserID = (string)value; break;
						case "StartDateTime": this.str.StartDateTime = (string)value; break;
						case "SRInterventionReason": this.str.SRInterventionReason = (string)value; break;
						case "Qty23Days": this.str.Qty23Days = (string)value; break;
						case "CasemixNotes": this.str.CasemixNotes = (string)value; break;
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
						case "RecipeAmount":

							if (value == null || value is System.Decimal)
								this.RecipeAmount = (System.Decimal?)value;
							break;
						case "IsCalcPercentage":

							if (value == null || value is System.Boolean)
								this.IsCalcPercentage = (System.Boolean?)value;
							break;
						case "AutoProcessCalculation":

							if (value == null || value is System.Decimal)
								this.AutoProcessCalculation = (System.Decimal?)value;
							break;
						case "IsUsingAdminReturn":

							if (value == null || value is System.Boolean)
								this.IsUsingAdminReturn = (System.Boolean?)value;
							break;
						case "VerifiedDateTime":

							if (value == null || value is System.DateTime)
								this.VerifiedDateTime = (System.DateTime?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "OriPrescriptionQty":

							if (value == null || value is System.Decimal)
								this.OriPrescriptionQty = (System.Decimal?)value;
							break;
						case "OriResultQty":

							if (value == null || value is System.Decimal)
								this.OriResultQty = (System.Decimal?)value;
							break;
						case "IsReturned":

							if (value == null || value is System.Boolean)
								this.IsReturned = (System.Boolean?)value;
							break;
						case "IsPendingDelivery":

							if (value == null || value is System.Boolean)
								this.IsPendingDelivery = (System.Boolean?)value;
							break;
						case "DeliveryQty":

							if (value == null || value is System.Decimal)
								this.DeliveryQty = (System.Decimal?)value;
							break;
						case "DaysOfUsage":

							if (value == null || value is System.Int32)
								this.DaysOfUsage = (System.Int32?)value;
							break;
						case "IsCasemixApproved":

							if (value == null || value is System.Boolean)
								this.IsCasemixApproved = (System.Boolean?)value;
							break;
						case "CasemixApprovedDateTime":

							if (value == null || value is System.DateTime)
								this.CasemixApprovedDateTime = (System.DateTime?)value;
							break;
						case "StartDateTime":

							if (value == null || value is System.DateTime)
								this.StartDateTime = (System.DateTime?)value;
							break;
						case "Qty23Days":

							if (value == null || value is System.Decimal)
								this.Qty23Days = (System.Decimal?)value;
							break;

						default:
							break;
					}
				}
			}
			else if (this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to TransPrescriptionItem.PrescriptionNo
		/// </summary>
		virtual public System.String PrescriptionNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.PrescriptionNo);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.PrescriptionNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.SequenceNo);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.ParentNo
		/// </summary>
		virtual public System.String ParentNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.ParentNo);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.ParentNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.IsRFlag
		/// </summary>
		virtual public System.Boolean? IsRFlag
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsRFlag);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsRFlag, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.IsCompound
		/// </summary>
		virtual public System.Boolean? IsCompound
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsCompound);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsCompound, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.ItemInterventionID
		/// </summary>
		virtual public System.String ItemInterventionID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.ItemInterventionID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.ItemInterventionID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.SRItemUnit);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.ItemQtyInString
		/// </summary>
		virtual public System.String ItemQtyInString
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.ItemQtyInString);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.ItemQtyInString, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.IsUsingDosageUnit
		/// </summary>
		virtual public System.Boolean? IsUsingDosageUnit
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsUsingDosageUnit);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsUsingDosageUnit, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.SRDosageUnit
		/// </summary>
		virtual public System.String SRDosageUnit
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.SRDosageUnit);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.SRDosageUnit, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.FrequencyOfDosing
		/// </summary>
		virtual public System.Byte? FrequencyOfDosing
		{
			get
			{
				return base.GetSystemByte(TransPrescriptionItemMetadata.ColumnNames.FrequencyOfDosing);
			}

			set
			{
				base.SetSystemByte(TransPrescriptionItemMetadata.ColumnNames.FrequencyOfDosing, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.DosingPeriod
		/// </summary>
		virtual public System.String DosingPeriod
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.DosingPeriod);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.DosingPeriod, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.NumberOfDosage
		/// </summary>
		virtual public System.Decimal? NumberOfDosage
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.NumberOfDosage);
			}

			set
			{
				base.SetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.NumberOfDosage, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.DurationOfDosing
		/// </summary>
		virtual public System.Byte? DurationOfDosing
		{
			get
			{
				return base.GetSystemByte(TransPrescriptionItemMetadata.ColumnNames.DurationOfDosing);
			}

			set
			{
				base.SetSystemByte(TransPrescriptionItemMetadata.ColumnNames.DurationOfDosing, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.Acpcdc
		/// </summary>
		virtual public System.String Acpcdc
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.Acpcdc);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.Acpcdc, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.SRMedicationRoute
		/// </summary>
		virtual public System.String SRMedicationRoute
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.SRMedicationRoute);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.SRMedicationRoute, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.ConsumeMethod
		/// </summary>
		virtual public System.String ConsumeMethod
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.ConsumeMethod);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.ConsumeMethod, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.PrescriptionQty
		/// </summary>
		virtual public System.Decimal? PrescriptionQty
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.PrescriptionQty);
			}

			set
			{
				base.SetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.PrescriptionQty, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.TakenQty
		/// </summary>
		virtual public System.Decimal? TakenQty
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.TakenQty);
			}

			set
			{
				base.SetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.TakenQty, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.ResultQty
		/// </summary>
		virtual public System.Decimal? ResultQty
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.ResultQty);
			}

			set
			{
				base.SetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.ResultQty, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.CostPrice
		/// </summary>
		virtual public System.Decimal? CostPrice
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.CostPrice);
			}

			set
			{
				base.SetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.CostPrice, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.InitialPrice
		/// </summary>
		virtual public System.Decimal? InitialPrice
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.InitialPrice);
			}

			set
			{
				base.SetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.InitialPrice, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.Price);
			}

			set
			{
				base.SetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.Price, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.DiscountAmount
		/// </summary>
		virtual public System.Decimal? DiscountAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.DiscountAmount);
			}

			set
			{
				base.SetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.DiscountAmount, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.EmbalaceID
		/// </summary>
		virtual public System.String EmbalaceID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.EmbalaceID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.EmbalaceID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.EmbalaceAmount
		/// </summary>
		virtual public System.Decimal? EmbalaceAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.EmbalaceAmount);
			}

			set
			{
				base.SetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.EmbalaceAmount, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.IsUseSweetener
		/// </summary>
		virtual public System.Boolean? IsUseSweetener
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsUseSweetener);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsUseSweetener, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.SweetenerAmount
		/// </summary>
		virtual public System.Decimal? SweetenerAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.SweetenerAmount);
			}

			set
			{
				base.SetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.SweetenerAmount, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.LineAmount
		/// </summary>
		virtual public System.Decimal? LineAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.LineAmount);
			}

			set
			{
				base.SetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.LineAmount, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransPrescriptionItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.SRDiscountReason
		/// </summary>
		virtual public System.String SRDiscountReason
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.SRDiscountReason);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.SRDiscountReason, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.IsApprove
		/// </summary>
		virtual public System.Boolean? IsApprove
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsApprove);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsApprove, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.IsBillProceed
		/// </summary>
		virtual public System.Boolean? IsBillProceed
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsBillProceed);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsBillProceed, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.DurationRelease
		/// </summary>
		virtual public System.Decimal? DurationRelease
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.DurationRelease);
			}

			set
			{
				base.SetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.DurationRelease, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.RecipeAmount
		/// </summary>
		virtual public System.Decimal? RecipeAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.RecipeAmount);
			}

			set
			{
				base.SetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.RecipeAmount, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.IsCalcPercentage
		/// </summary>
		virtual public System.Boolean? IsCalcPercentage
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsCalcPercentage);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsCalcPercentage, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.AutoProcessCalculation
		/// </summary>
		virtual public System.Decimal? AutoProcessCalculation
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.AutoProcessCalculation);
			}

			set
			{
				base.SetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.AutoProcessCalculation, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.IsUsingAdminReturn
		/// </summary>
		virtual public System.Boolean? IsUsingAdminReturn
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsUsingAdminReturn);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsUsingAdminReturn, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.VerifiedByUserID
		/// </summary>
		virtual public System.String VerifiedByUserID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.VerifiedByUserID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.VerifiedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.VerifiedDateTime
		/// </summary>
		virtual public System.DateTime? VerifiedDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionItemMetadata.ColumnNames.VerifiedDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransPrescriptionItemMetadata.ColumnNames.VerifiedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.LastUpdateByUserHostName
		/// </summary>
		virtual public System.String LastUpdateByUserHostName
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.LastUpdateByUserHostName);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.LastUpdateByUserHostName, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.VerifiedByUserHostName
		/// </summary>
		virtual public System.String VerifiedByUserHostName
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.VerifiedByUserHostName);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.VerifiedByUserHostName, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.SRConsumeMethod
		/// </summary>
		virtual public System.String SRConsumeMethod
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.SRConsumeMethod);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.SRConsumeMethod, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.DosageQty
		/// </summary>
		virtual public System.String DosageQty
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.DosageQty);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.DosageQty, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.EmbalaceQty
		/// </summary>
		virtual public System.String EmbalaceQty
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.EmbalaceQty);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.EmbalaceQty, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.IterText
		/// </summary>
		virtual public System.String IterText
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.IterText);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.IterText, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.OrderText
		/// </summary>
		virtual public System.String OrderText
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.OrderText);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.OrderText, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.ConsumeQty
		/// </summary>
		virtual public System.String ConsumeQty
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.ConsumeQty);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.ConsumeQty, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.SRConsumeUnit
		/// </summary>
		virtual public System.String SRConsumeUnit
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.SRConsumeUnit);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.SRConsumeUnit, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.ReferenceSequenceNo
		/// </summary>
		virtual public System.String ReferenceSequenceNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.ReferenceSequenceNo);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.ReferenceSequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionItemMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransPrescriptionItemMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.OriPrescriptionQty
		/// </summary>
		virtual public System.Decimal? OriPrescriptionQty
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.OriPrescriptionQty);
			}

			set
			{
				base.SetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.OriPrescriptionQty, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.OriConsumeQty
		/// </summary>
		virtual public System.String OriConsumeQty
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.OriConsumeQty);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.OriConsumeQty, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.OriSRConsumeUnit
		/// </summary>
		virtual public System.String OriSRConsumeUnit
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.OriSRConsumeUnit);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.OriSRConsumeUnit, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.OriResultQty
		/// </summary>
		virtual public System.Decimal? OriResultQty
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.OriResultQty);
			}

			set
			{
				base.SetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.OriResultQty, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.OriItemQtyInString
		/// </summary>
		virtual public System.String OriItemQtyInString
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.OriItemQtyInString);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.OriItemQtyInString, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.OriSRItemUnit
		/// </summary>
		virtual public System.String OriSRItemUnit
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.OriSRItemUnit);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.OriSRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.OriDosageQty
		/// </summary>
		virtual public System.String OriDosageQty
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.OriDosageQty);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.OriDosageQty, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.OriSRDosageUnit
		/// </summary>
		virtual public System.String OriSRDosageUnit
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.OriSRDosageUnit);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.OriSRDosageUnit, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.OriSRConsumeMethod
		/// </summary>
		virtual public System.String OriSRConsumeMethod
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.OriSRConsumeMethod);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.OriSRConsumeMethod, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.IsReturned
		/// </summary>
		virtual public System.Boolean? IsReturned
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsReturned);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsReturned, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.IsPendingDelivery
		/// </summary>
		virtual public System.Boolean? IsPendingDelivery
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsPendingDelivery);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsPendingDelivery, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.DeliveryQty
		/// </summary>
		virtual public System.Decimal? DeliveryQty
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.DeliveryQty);
			}

			set
			{
				base.SetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.DeliveryQty, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.DaysOfUsage
		/// </summary>
		virtual public System.Int32? DaysOfUsage
		{
			get
			{
				return base.GetSystemInt32(TransPrescriptionItemMetadata.ColumnNames.DaysOfUsage);
			}

			set
			{
				base.SetSystemInt32(TransPrescriptionItemMetadata.ColumnNames.DaysOfUsage, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.IsCasemixApproved
		/// </summary>
		virtual public System.Boolean? IsCasemixApproved
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsCasemixApproved);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionItemMetadata.ColumnNames.IsCasemixApproved, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.CasemixApprovedDateTime
		/// </summary>
		virtual public System.DateTime? CasemixApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionItemMetadata.ColumnNames.CasemixApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransPrescriptionItemMetadata.ColumnNames.CasemixApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.CasemixApprovedByUserID
		/// </summary>
		virtual public System.String CasemixApprovedByUserID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.CasemixApprovedByUserID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.CasemixApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.StartDateTime
		/// </summary>
		virtual public System.DateTime? StartDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionItemMetadata.ColumnNames.StartDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransPrescriptionItemMetadata.ColumnNames.StartDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.SRInterventionReason
		/// </summary>
		virtual public System.String SRInterventionReason
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.SRInterventionReason);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.SRInterventionReason, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.Qty23Days
		/// </summary>
		virtual public System.Decimal? Qty23Days
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.Qty23Days);
			}

			set
			{
				base.SetSystemDecimal(TransPrescriptionItemMetadata.ColumnNames.Qty23Days, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescriptionItem.CasemixNotes
		/// </summary>
		virtual public System.String CasemixNotes
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemMetadata.ColumnNames.CasemixNotes);
			}

			set
			{
				base.SetSystemString(TransPrescriptionItemMetadata.ColumnNames.CasemixNotes, value);
			}
		}

		#endregion

		#region String Properties

		/// <summary>
		/// Converts an entity's properties to
		/// and from strings.
		/// </summary>
		/// <remarks>
		/// The str properties Get and Set provide easy conversion
		/// between a string and a property's data type. Not all
		/// data types will get a str property.
		/// </remarks>
		/// <example>
		/// Set a datetime from a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// entity.str.HireDate = "2007-01-01 00:00:00";
		/// entity.Save();
		/// </code>
		/// Get a datetime as a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// string theDate = entity.str.HireDate;
		/// </code>
		/// </example>
		[BrowsableAttribute(false)]
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
			public esStrings(esTransPrescriptionItem entity)
			{
				this.entity = entity;
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
			public System.String RecipeAmount
			{
				get
				{
					System.Decimal? data = entity.RecipeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecipeAmount = null;
					else entity.RecipeAmount = Convert.ToDecimal(value);
				}
			}
			public System.String IsCalcPercentage
			{
				get
				{
					System.Boolean? data = entity.IsCalcPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCalcPercentage = null;
					else entity.IsCalcPercentage = Convert.ToBoolean(value);
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
			public System.String IsUsingAdminReturn
			{
				get
				{
					System.Boolean? data = entity.IsUsingAdminReturn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUsingAdminReturn = null;
					else entity.IsUsingAdminReturn = Convert.ToBoolean(value);
				}
			}
			public System.String VerifiedByUserID
			{
				get
				{
					System.String data = entity.VerifiedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifiedByUserID = null;
					else entity.VerifiedByUserID = Convert.ToString(value);
				}
			}
			public System.String VerifiedDateTime
			{
				get
				{
					System.DateTime? data = entity.VerifiedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifiedDateTime = null;
					else entity.VerifiedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastUpdateByUserHostName
			{
				get
				{
					System.String data = entity.LastUpdateByUserHostName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdateByUserHostName = null;
					else entity.LastUpdateByUserHostName = Convert.ToString(value);
				}
			}
			public System.String VerifiedByUserHostName
			{
				get
				{
					System.String data = entity.VerifiedByUserHostName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifiedByUserHostName = null;
					else entity.VerifiedByUserHostName = Convert.ToString(value);
				}
			}
			public System.String SRConsumeMethod
			{
				get
				{
					System.String data = entity.SRConsumeMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRConsumeMethod = null;
					else entity.SRConsumeMethod = Convert.ToString(value);
				}
			}
			public System.String DosageQty
			{
				get
				{
					System.String data = entity.DosageQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DosageQty = null;
					else entity.DosageQty = Convert.ToString(value);
				}
			}
			public System.String EmbalaceQty
			{
				get
				{
					System.String data = entity.EmbalaceQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmbalaceQty = null;
					else entity.EmbalaceQty = Convert.ToString(value);
				}
			}
			public System.String IterText
			{
				get
				{
					System.String data = entity.IterText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IterText = null;
					else entity.IterText = Convert.ToString(value);
				}
			}
			public System.String OrderText
			{
				get
				{
					System.String data = entity.OrderText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderText = null;
					else entity.OrderText = Convert.ToString(value);
				}
			}
			public System.String ConsumeQty
			{
				get
				{
					System.String data = entity.ConsumeQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConsumeQty = null;
					else entity.ConsumeQty = Convert.ToString(value);
				}
			}
			public System.String SRConsumeUnit
			{
				get
				{
					System.String data = entity.SRConsumeUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRConsumeUnit = null;
					else entity.SRConsumeUnit = Convert.ToString(value);
				}
			}
			public System.String ReferenceNo
			{
				get
				{
					System.String data = entity.ReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceNo = null;
					else entity.ReferenceNo = Convert.ToString(value);
				}
			}
			public System.String ReferenceSequenceNo
			{
				get
				{
					System.String data = entity.ReferenceSequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceSequenceNo = null;
					else entity.ReferenceSequenceNo = Convert.ToString(value);
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
			public System.String OriPrescriptionQty
			{
				get
				{
					System.Decimal? data = entity.OriPrescriptionQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OriPrescriptionQty = null;
					else entity.OriPrescriptionQty = Convert.ToDecimal(value);
				}
			}
			public System.String OriConsumeQty
			{
				get
				{
					System.String data = entity.OriConsumeQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OriConsumeQty = null;
					else entity.OriConsumeQty = Convert.ToString(value);
				}
			}
			public System.String OriSRConsumeUnit
			{
				get
				{
					System.String data = entity.OriSRConsumeUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OriSRConsumeUnit = null;
					else entity.OriSRConsumeUnit = Convert.ToString(value);
				}
			}
			public System.String OriResultQty
			{
				get
				{
					System.Decimal? data = entity.OriResultQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OriResultQty = null;
					else entity.OriResultQty = Convert.ToDecimal(value);
				}
			}
			public System.String OriItemQtyInString
			{
				get
				{
					System.String data = entity.OriItemQtyInString;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OriItemQtyInString = null;
					else entity.OriItemQtyInString = Convert.ToString(value);
				}
			}
			public System.String OriSRItemUnit
			{
				get
				{
					System.String data = entity.OriSRItemUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OriSRItemUnit = null;
					else entity.OriSRItemUnit = Convert.ToString(value);
				}
			}
			public System.String OriDosageQty
			{
				get
				{
					System.String data = entity.OriDosageQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OriDosageQty = null;
					else entity.OriDosageQty = Convert.ToString(value);
				}
			}
			public System.String OriSRDosageUnit
			{
				get
				{
					System.String data = entity.OriSRDosageUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OriSRDosageUnit = null;
					else entity.OriSRDosageUnit = Convert.ToString(value);
				}
			}
			public System.String OriSRConsumeMethod
			{
				get
				{
					System.String data = entity.OriSRConsumeMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OriSRConsumeMethod = null;
					else entity.OriSRConsumeMethod = Convert.ToString(value);
				}
			}
			public System.String IsReturned
			{
				get
				{
					System.Boolean? data = entity.IsReturned;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReturned = null;
					else entity.IsReturned = Convert.ToBoolean(value);
				}
			}
			public System.String IsPendingDelivery
			{
				get
				{
					System.Boolean? data = entity.IsPendingDelivery;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPendingDelivery = null;
					else entity.IsPendingDelivery = Convert.ToBoolean(value);
				}
			}
			public System.String DeliveryQty
			{
				get
				{
					System.Decimal? data = entity.DeliveryQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeliveryQty = null;
					else entity.DeliveryQty = Convert.ToDecimal(value);
				}
			}
			public System.String DaysOfUsage
			{
				get
				{
					System.Int32? data = entity.DaysOfUsage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DaysOfUsage = null;
					else entity.DaysOfUsage = Convert.ToInt32(value);
				}
			}
			public System.String IsCasemixApproved
			{
				get
				{
					System.Boolean? data = entity.IsCasemixApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCasemixApproved = null;
					else entity.IsCasemixApproved = Convert.ToBoolean(value);
				}
			}
			public System.String CasemixApprovedDateTime
			{
				get
				{
					System.DateTime? data = entity.CasemixApprovedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CasemixApprovedDateTime = null;
					else entity.CasemixApprovedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CasemixApprovedByUserID
			{
				get
				{
					System.String data = entity.CasemixApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CasemixApprovedByUserID = null;
					else entity.CasemixApprovedByUserID = Convert.ToString(value);
				}
			}
			public System.String StartDateTime
			{
				get
				{
					System.DateTime? data = entity.StartDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartDateTime = null;
					else entity.StartDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String SRInterventionReason
			{
				get
				{
					System.String data = entity.SRInterventionReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRInterventionReason = null;
					else entity.SRInterventionReason = Convert.ToString(value);
				}
			}
			public System.String Qty23Days
			{
				get
				{
					System.Decimal? data = entity.Qty23Days;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Qty23Days = null;
					else entity.Qty23Days = Convert.ToDecimal(value);
				}
			}
			public System.String CasemixNotes
			{
				get
				{
					System.String data = entity.CasemixNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CasemixNotes = null;
					else entity.CasemixNotes = Convert.ToString(value);
				}
			}
			private esTransPrescriptionItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransPrescriptionItemQuery query)
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
				throw new Exception("esTransPrescriptionItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class TransPrescriptionItem : esTransPrescriptionItem
	{
	}

	[Serializable]
	abstract public class esTransPrescriptionItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return TransPrescriptionItemMetadata.Meta();
			}
		}

		public esQueryItem PrescriptionNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.PrescriptionNo, esSystemType.String);
			}
		}

		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		}

		public esQueryItem ParentNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.ParentNo, esSystemType.String);
			}
		}

		public esQueryItem IsRFlag
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.IsRFlag, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCompound
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.IsCompound, esSystemType.Boolean);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ItemInterventionID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.ItemInterventionID, esSystemType.String);
			}
		}

		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem ItemQtyInString
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.ItemQtyInString, esSystemType.String);
			}
		}

		public esQueryItem IsUsingDosageUnit
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.IsUsingDosageUnit, esSystemType.Boolean);
			}
		}

		public esQueryItem SRDosageUnit
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.SRDosageUnit, esSystemType.String);
			}
		}

		public esQueryItem FrequencyOfDosing
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.FrequencyOfDosing, esSystemType.Byte);
			}
		}

		public esQueryItem DosingPeriod
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.DosingPeriod, esSystemType.String);
			}
		}

		public esQueryItem NumberOfDosage
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.NumberOfDosage, esSystemType.Decimal);
			}
		}

		public esQueryItem DurationOfDosing
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.DurationOfDosing, esSystemType.Byte);
			}
		}

		public esQueryItem Acpcdc
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.Acpcdc, esSystemType.String);
			}
		}

		public esQueryItem SRMedicationRoute
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.SRMedicationRoute, esSystemType.String);
			}
		}

		public esQueryItem ConsumeMethod
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.ConsumeMethod, esSystemType.String);
			}
		}

		public esQueryItem PrescriptionQty
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.PrescriptionQty, esSystemType.Decimal);
			}
		}

		public esQueryItem TakenQty
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.TakenQty, esSystemType.Decimal);
			}
		}

		public esQueryItem ResultQty
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.ResultQty, esSystemType.Decimal);
			}
		}

		public esQueryItem CostPrice
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem InitialPrice
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.InitialPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		}

		public esQueryItem DiscountAmount
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.DiscountAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem EmbalaceID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.EmbalaceID, esSystemType.String);
			}
		}

		public esQueryItem EmbalaceAmount
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.EmbalaceAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem IsUseSweetener
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.IsUseSweetener, esSystemType.Boolean);
			}
		}

		public esQueryItem SweetenerAmount
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.SweetenerAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem LineAmount
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.LineAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SRDiscountReason
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.SRDiscountReason, esSystemType.String);
			}
		}

		public esQueryItem IsApprove
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.IsApprove, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBillProceed
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.IsBillProceed, esSystemType.Boolean);
			}
		}

		public esQueryItem DurationRelease
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.DurationRelease, esSystemType.Decimal);
			}
		}

		public esQueryItem RecipeAmount
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.RecipeAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem IsCalcPercentage
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.IsCalcPercentage, esSystemType.Boolean);
			}
		}

		public esQueryItem AutoProcessCalculation
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.AutoProcessCalculation, esSystemType.Decimal);
			}
		}

		public esQueryItem IsUsingAdminReturn
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.IsUsingAdminReturn, esSystemType.Boolean);
			}
		}

		public esQueryItem VerifiedByUserID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.VerifiedByUserID, esSystemType.String);
			}
		}

		public esQueryItem VerifiedDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.VerifiedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserHostName
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.LastUpdateByUserHostName, esSystemType.String);
			}
		}

		public esQueryItem VerifiedByUserHostName
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.VerifiedByUserHostName, esSystemType.String);
			}
		}

		public esQueryItem SRConsumeMethod
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.SRConsumeMethod, esSystemType.String);
			}
		}

		public esQueryItem DosageQty
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.DosageQty, esSystemType.String);
			}
		}

		public esQueryItem EmbalaceQty
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.EmbalaceQty, esSystemType.String);
			}
		}

		public esQueryItem IterText
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.IterText, esSystemType.String);
			}
		}

		public esQueryItem OrderText
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.OrderText, esSystemType.String);
			}
		}

		public esQueryItem ConsumeQty
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.ConsumeQty, esSystemType.String);
			}
		}

		public esQueryItem SRConsumeUnit
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.SRConsumeUnit, esSystemType.String);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem ReferenceSequenceNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.ReferenceSequenceNo, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem OriPrescriptionQty
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.OriPrescriptionQty, esSystemType.Decimal);
			}
		}

		public esQueryItem OriConsumeQty
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.OriConsumeQty, esSystemType.String);
			}
		}

		public esQueryItem OriSRConsumeUnit
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.OriSRConsumeUnit, esSystemType.String);
			}
		}

		public esQueryItem OriResultQty
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.OriResultQty, esSystemType.Decimal);
			}
		}

		public esQueryItem OriItemQtyInString
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.OriItemQtyInString, esSystemType.String);
			}
		}

		public esQueryItem OriSRItemUnit
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.OriSRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem OriDosageQty
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.OriDosageQty, esSystemType.String);
			}
		}

		public esQueryItem OriSRDosageUnit
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.OriSRDosageUnit, esSystemType.String);
			}
		}

		public esQueryItem OriSRConsumeMethod
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.OriSRConsumeMethod, esSystemType.String);
			}
		}

		public esQueryItem IsReturned
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.IsReturned, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPendingDelivery
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.IsPendingDelivery, esSystemType.Boolean);
			}
		}

		public esQueryItem DeliveryQty
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.DeliveryQty, esSystemType.Decimal);
			}
		}

		public esQueryItem DaysOfUsage
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.DaysOfUsage, esSystemType.Int32);
			}
		}

		public esQueryItem IsCasemixApproved
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.IsCasemixApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem CasemixApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.CasemixApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CasemixApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.CasemixApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem StartDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.StartDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem SRInterventionReason
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.SRInterventionReason, esSystemType.String);
			}
		}

		public esQueryItem Qty23Days
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.Qty23Days, esSystemType.Decimal);
			}
		}

		public esQueryItem CasemixNotes
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemMetadata.ColumnNames.CasemixNotes, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransPrescriptionItemCollection")]
	public partial class TransPrescriptionItemCollection : esTransPrescriptionItemCollection, IEnumerable<TransPrescriptionItem>
	{
		public TransPrescriptionItemCollection()
		{

		}

		public static implicit operator List<TransPrescriptionItem>(TransPrescriptionItemCollection coll)
		{
			List<TransPrescriptionItem> list = new List<TransPrescriptionItem>();

			foreach (TransPrescriptionItem emp in coll)
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
				return TransPrescriptionItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPrescriptionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransPrescriptionItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransPrescriptionItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public TransPrescriptionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPrescriptionItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}

		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one record was loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(TransPrescriptionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public TransPrescriptionItem AddNew()
		{
			TransPrescriptionItem entity = base.AddNewEntity() as TransPrescriptionItem;

			return entity;
		}
		public TransPrescriptionItem FindByPrimaryKey(String prescriptionNo, String sequenceNo)
		{
			return base.FindByPrimaryKey(prescriptionNo, sequenceNo) as TransPrescriptionItem;
		}

		#region IEnumerable< TransPrescriptionItem> Members

		IEnumerator<TransPrescriptionItem> IEnumerable<TransPrescriptionItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as TransPrescriptionItem;
			}
		}

		#endregion

		private TransPrescriptionItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransPrescriptionItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("TransPrescriptionItem ({PrescriptionNo, SequenceNo})")]
	[Serializable]
	public partial class TransPrescriptionItem : esTransPrescriptionItem
	{
		public TransPrescriptionItem()
		{
		}

		public TransPrescriptionItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransPrescriptionItemMetadata.Meta();
			}
		}

		override protected esTransPrescriptionItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPrescriptionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public TransPrescriptionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPrescriptionItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}

		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one row is loaded.
		/// For an entity, an exception will be thrown
		/// if more than one row is loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(TransPrescriptionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private TransPrescriptionItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class TransPrescriptionItemQuery : esTransPrescriptionItemQuery
	{
		public TransPrescriptionItemQuery()
		{

		}

		public TransPrescriptionItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "TransPrescriptionItemQuery";
		}
	}

	[Serializable]
	public partial class TransPrescriptionItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransPrescriptionItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.PrescriptionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.PrescriptionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.ParentNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.ParentNo;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.IsRFlag, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.IsRFlag;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.IsCompound, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.IsCompound;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.ItemID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.ItemInterventionID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.ItemInterventionID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.SRItemUnit, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.ItemQtyInString, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.ItemQtyInString;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.IsUsingDosageUnit, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.IsUsingDosageUnit;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.SRDosageUnit, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.SRDosageUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.FrequencyOfDosing, 11, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.FrequencyOfDosing;
			c.NumericPrecision = 3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.DosingPeriod, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.DosingPeriod;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.NumberOfDosage, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.NumberOfDosage;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.DurationOfDosing, 14, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.DurationOfDosing;
			c.NumericPrecision = 3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.Acpcdc, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.Acpcdc;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.SRMedicationRoute, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.SRMedicationRoute;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.ConsumeMethod, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.ConsumeMethod;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.PrescriptionQty, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.PrescriptionQty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.TakenQty, 19, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.TakenQty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.ResultQty, 20, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.ResultQty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.CostPrice, 21, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.CostPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.InitialPrice, 22, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.InitialPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.Price, 23, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.DiscountAmount, 24, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.DiscountAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.EmbalaceID, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.EmbalaceID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.EmbalaceAmount, 26, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.EmbalaceAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.IsUseSweetener, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.IsUseSweetener;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.SweetenerAmount, 28, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.SweetenerAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.LineAmount, 29, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.LineAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.Notes, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.LastUpdateDateTime, 31, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.LastUpdateByUserID, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.SRDiscountReason, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.SRDiscountReason;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.IsApprove, 34, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.IsApprove;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.IsVoid, 35, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.IsBillProceed, 36, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.IsBillProceed;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.DurationRelease, 37, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.DurationRelease;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.RecipeAmount, 38, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.RecipeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.IsCalcPercentage, 39, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.IsCalcPercentage;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.AutoProcessCalculation, 40, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.AutoProcessCalculation;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.IsUsingAdminReturn, 41, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.IsUsingAdminReturn;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.VerifiedByUserID, 42, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.VerifiedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.VerifiedDateTime, 43, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.VerifiedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.LastUpdateByUserHostName, 44, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.LastUpdateByUserHostName;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.VerifiedByUserHostName, 45, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.VerifiedByUserHostName;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.SRConsumeMethod, 46, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.SRConsumeMethod;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.DosageQty, 47, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.DosageQty;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.EmbalaceQty, 48, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.EmbalaceQty;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.IterText, 49, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.IterText;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.OrderText, 50, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.OrderText;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.ConsumeQty, 51, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.ConsumeQty;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.SRConsumeUnit, 52, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.SRConsumeUnit;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.ReferenceNo, 53, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.ReferenceSequenceNo, 54, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.ReferenceSequenceNo;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.CreatedDateTime, 55, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.CreatedByUserID, 56, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.OriPrescriptionQty, 57, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.OriPrescriptionQty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.OriConsumeQty, 58, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.OriConsumeQty;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.OriSRConsumeUnit, 59, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.OriSRConsumeUnit;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.OriResultQty, 60, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.OriResultQty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.OriItemQtyInString, 61, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.OriItemQtyInString;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.OriSRItemUnit, 62, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.OriSRItemUnit;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.OriDosageQty, 63, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.OriDosageQty;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.OriSRDosageUnit, 64, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.OriSRDosageUnit;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.OriSRConsumeMethod, 65, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.OriSRConsumeMethod;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.IsReturned, 66, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.IsReturned;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.IsPendingDelivery, 67, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.IsPendingDelivery;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.DeliveryQty, 68, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.DeliveryQty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.DaysOfUsage, 69, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.DaysOfUsage;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.IsCasemixApproved, 70, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.IsCasemixApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.CasemixApprovedDateTime, 71, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.CasemixApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.CasemixApprovedByUserID, 72, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.CasemixApprovedByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.StartDateTime, 73, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.StartDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.SRInterventionReason, 74, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.SRInterventionReason;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.Qty23Days, 75, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.Qty23Days;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionItemMetadata.ColumnNames.CasemixNotes, 76, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemMetadata.PropertyNames.CasemixNotes;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public TransPrescriptionItemMetadata Meta()
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
			get { return base._columns; }
		}

		#region ColumnNames
		public class ColumnNames
		{
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
			public const string RecipeAmount = "RecipeAmount";
			public const string IsCalcPercentage = "IsCalcPercentage";
			public const string AutoProcessCalculation = "AutoProcessCalculation";
			public const string IsUsingAdminReturn = "IsUsingAdminReturn";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string LastUpdateByUserHostName = "LastUpdateByUserHostName";
			public const string VerifiedByUserHostName = "VerifiedByUserHostName";
			public const string SRConsumeMethod = "SRConsumeMethod";
			public const string DosageQty = "DosageQty";
			public const string EmbalaceQty = "EmbalaceQty";
			public const string IterText = "IterText";
			public const string OrderText = "OrderText";
			public const string ConsumeQty = "ConsumeQty";
			public const string SRConsumeUnit = "SRConsumeUnit";
			public const string ReferenceNo = "ReferenceNo";
			public const string ReferenceSequenceNo = "ReferenceSequenceNo";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string OriPrescriptionQty = "OriPrescriptionQty";
			public const string OriConsumeQty = "OriConsumeQty";
			public const string OriSRConsumeUnit = "OriSRConsumeUnit";
			public const string OriResultQty = "OriResultQty";
			public const string OriItemQtyInString = "OriItemQtyInString";
			public const string OriSRItemUnit = "OriSRItemUnit";
			public const string OriDosageQty = "OriDosageQty";
			public const string OriSRDosageUnit = "OriSRDosageUnit";
			public const string OriSRConsumeMethod = "OriSRConsumeMethod";
			public const string IsReturned = "IsReturned";
			public const string IsPendingDelivery = "IsPendingDelivery";
			public const string DeliveryQty = "DeliveryQty";
			public const string DaysOfUsage = "DaysOfUsage";
			public const string IsCasemixApproved = "IsCasemixApproved";
			public const string CasemixApprovedDateTime = "CasemixApprovedDateTime";
			public const string CasemixApprovedByUserID = "CasemixApprovedByUserID";
			public const string StartDateTime = "StartDateTime";
			public const string SRInterventionReason = "SRInterventionReason";
			public const string Qty23Days = "Qty23Days";
			public const string CasemixNotes = "CasemixNotes";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
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
			public const string RecipeAmount = "RecipeAmount";
			public const string IsCalcPercentage = "IsCalcPercentage";
			public const string AutoProcessCalculation = "AutoProcessCalculation";
			public const string IsUsingAdminReturn = "IsUsingAdminReturn";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string LastUpdateByUserHostName = "LastUpdateByUserHostName";
			public const string VerifiedByUserHostName = "VerifiedByUserHostName";
			public const string SRConsumeMethod = "SRConsumeMethod";
			public const string DosageQty = "DosageQty";
			public const string EmbalaceQty = "EmbalaceQty";
			public const string IterText = "IterText";
			public const string OrderText = "OrderText";
			public const string ConsumeQty = "ConsumeQty";
			public const string SRConsumeUnit = "SRConsumeUnit";
			public const string ReferenceNo = "ReferenceNo";
			public const string ReferenceSequenceNo = "ReferenceSequenceNo";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string OriPrescriptionQty = "OriPrescriptionQty";
			public const string OriConsumeQty = "OriConsumeQty";
			public const string OriSRConsumeUnit = "OriSRConsumeUnit";
			public const string OriResultQty = "OriResultQty";
			public const string OriItemQtyInString = "OriItemQtyInString";
			public const string OriSRItemUnit = "OriSRItemUnit";
			public const string OriDosageQty = "OriDosageQty";
			public const string OriSRDosageUnit = "OriSRDosageUnit";
			public const string OriSRConsumeMethod = "OriSRConsumeMethod";
			public const string IsReturned = "IsReturned";
			public const string IsPendingDelivery = "IsPendingDelivery";
			public const string DeliveryQty = "DeliveryQty";
			public const string DaysOfUsage = "DaysOfUsage";
			public const string IsCasemixApproved = "IsCasemixApproved";
			public const string CasemixApprovedDateTime = "CasemixApprovedDateTime";
			public const string CasemixApprovedByUserID = "CasemixApprovedByUserID";
			public const string StartDateTime = "StartDateTime";
			public const string SRInterventionReason = "SRInterventionReason";
			public const string Qty23Days = "Qty23Days";
			public const string CasemixNotes = "CasemixNotes";
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
			lock (typeof(TransPrescriptionItemMetadata))
			{
				if (TransPrescriptionItemMetadata.mapDelegates == null)
				{
					TransPrescriptionItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (TransPrescriptionItemMetadata.meta == null)
				{
					TransPrescriptionItemMetadata.meta = new TransPrescriptionItemMetadata();
				}

				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if (!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

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
				meta.AddTypeMap("RecipeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsCalcPercentage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AutoProcessCalculation", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsUsingAdminReturn", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VerifiedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerifiedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserHostName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerifiedByUserHostName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRConsumeMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DosageQty", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmbalaceQty", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IterText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConsumeQty", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRConsumeUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceSequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OriPrescriptionQty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("OriConsumeQty", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OriSRConsumeUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OriResultQty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("OriItemQtyInString", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OriSRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OriDosageQty", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OriSRDosageUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OriSRConsumeMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsReturned", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPendingDelivery", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DeliveryQty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DaysOfUsage", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsCasemixApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CasemixApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CasemixApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRInterventionReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty23Days", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CasemixNotes", new esTypeMap("varchar", "System.String"));


				meta.Source = "TransPrescriptionItem";
				meta.Destination = "TransPrescriptionItem";
				meta.spInsert = "proc_TransPrescriptionItemInsert";
				meta.spUpdate = "proc_TransPrescriptionItemUpdate";
				meta.spDelete = "proc_TransPrescriptionItemDelete";
				meta.spLoadAll = "proc_TransPrescriptionItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransPrescriptionItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransPrescriptionItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
