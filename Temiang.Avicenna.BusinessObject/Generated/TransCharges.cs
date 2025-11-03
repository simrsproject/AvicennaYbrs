/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/11/2023 10:50:51 AM
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
	abstract public class esTransChargesCollection : esEntityCollectionWAuditLog
	{
		public esTransChargesCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "TransChargesCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransChargesQuery query)
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
			this.InitQuery(query as esTransChargesQuery);
		}
		#endregion

		virtual public TransCharges DetachEntity(TransCharges entity)
		{
			return base.DetachEntity(entity) as TransCharges;
		}

		virtual public TransCharges AttachEntity(TransCharges entity)
		{
			return base.AttachEntity(entity) as TransCharges;
		}

		virtual public void Combine(TransChargesCollection collection)
		{
			base.Combine(collection);
		}

		new public TransCharges this[int index]
		{
			get
			{
				return base[index] as TransCharges;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransCharges);
		}
	}

	[Serializable]
	abstract public class esTransCharges : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransChargesQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransCharges()
		{
		}

		public esTransCharges(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo)
		{
			esTransChargesQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "TransactionDate": this.str.TransactionDate = (string)value; break;
						case "ExecutionDate": this.str.ExecutionDate = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "FromServiceUnitID": this.str.FromServiceUnitID = (string)value; break;
						case "ToServiceUnitID": this.str.ToServiceUnitID = (string)value; break;
						case "ClassID": this.str.ClassID = (string)value; break;
						case "RoomID": this.str.RoomID = (string)value; break;
						case "BedID": this.str.BedID = (string)value; break;
						case "DueDate": this.str.DueDate = (string)value; break;
						case "SRShift": this.str.SRShift = (string)value; break;
						case "SRItemType": this.str.SRItemType = (string)value; break;
						case "IsProceed": this.str.IsProceed = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "IsOrder": this.str.IsOrder = (string)value; break;
						case "IsCorrection": this.str.IsCorrection = (string)value; break;
						case "IsClusterAssign": this.str.IsClusterAssign = (string)value; break;
						case "IsAutoBillTransaction": this.str.IsAutoBillTransaction = (string)value; break;
						case "IsBillProceed": this.str.IsBillProceed = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SRTypeResult": this.str.SRTypeResult = (string)value; break;
						case "ResponUnitID": this.str.ResponUnitID = (string)value; break;
						case "SurgicalPackageID": this.str.SurgicalPackageID = (string)value; break;
						case "IsPackage": this.str.IsPackage = (string)value; break;
						case "PackageReferenceNo": this.str.PackageReferenceNo = (string)value; break;
						case "IsRoomIn": this.str.IsRoomIn = (string)value; break;
						case "TariffDiscountForRoomIn": this.str.TariffDiscountForRoomIn = (string)value; break;
						case "IsNonPatient": this.str.IsNonPatient = (string)value; break;
						case "ServiceUnitBookingNo": this.str.ServiceUnitBookingNo = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "PhysicianSenders": this.str.PhysicianSenders = (string)value; break;
						case "IsValidated": this.str.IsValidated = (string)value; break;
						case "ValidatedDateTime": this.str.ValidatedDateTime = (string)value; break;
						case "ValidatedByUserID": this.str.ValidatedByUserID = (string)value; break;
						case "LocationID": this.str.LocationID = (string)value; break;
						case "SRProdiaContractID": this.str.SRProdiaContractID = (string)value; break;
						case "LaboratoryParamedicID": this.str.LaboratoryParamedicID = (string)value; break;
						case "SRBloodSampleTakenBy": this.str.SRBloodSampleTakenBy = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "AnalystID": this.str.AnalystID = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "SROrderStatus": this.str.SROrderStatus = (string)value; break;
						case "ResultReadByPhysicianID": this.str.ResultReadByPhysicianID = (string)value; break;
						case "ResultReadByPhysicianDateTime": this.str.ResultReadByPhysicianDateTime = (string)value; break;
						case "ClinicalDiagnosis": this.str.ClinicalDiagnosis = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "TransactionDate":

							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
							break;
						case "ExecutionDate":

							if (value == null || value is System.DateTime)
								this.ExecutionDate = (System.DateTime?)value;
							break;
						case "DueDate":

							if (value == null || value is System.DateTime)
								this.DueDate = (System.DateTime?)value;
							break;
						case "IsProceed":

							if (value == null || value is System.Boolean)
								this.IsProceed = (System.Boolean?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "IsOrder":

							if (value == null || value is System.Boolean)
								this.IsOrder = (System.Boolean?)value;
							break;
						case "IsCorrection":

							if (value == null || value is System.Boolean)
								this.IsCorrection = (System.Boolean?)value;
							break;
						case "IsClusterAssign":

							if (value == null || value is System.Boolean)
								this.IsClusterAssign = (System.Boolean?)value;
							break;
						case "IsAutoBillTransaction":

							if (value == null || value is System.Boolean)
								this.IsAutoBillTransaction = (System.Boolean?)value;
							break;
						case "IsBillProceed":

							if (value == null || value is System.Boolean)
								this.IsBillProceed = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsPackage":

							if (value == null || value is System.Boolean)
								this.IsPackage = (System.Boolean?)value;
							break;
						case "IsRoomIn":

							if (value == null || value is System.Boolean)
								this.IsRoomIn = (System.Boolean?)value;
							break;
						case "TariffDiscountForRoomIn":

							if (value == null || value is System.Decimal)
								this.TariffDiscountForRoomIn = (System.Decimal?)value;
							break;
						case "IsNonPatient":

							if (value == null || value is System.Boolean)
								this.IsNonPatient = (System.Boolean?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "IsValidated":

							if (value == null || value is System.Boolean)
								this.IsValidated = (System.Boolean?)value;
							break;
						case "ValidatedDateTime":

							if (value == null || value is System.DateTime)
								this.ValidatedDateTime = (System.DateTime?)value;
							break;
						case "ApprovedDateTime":

							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
							break;
						case "VoidDateTime":

							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
							break;
						case "ResultReadByPhysicianDateTime":

							if (value == null || value is System.DateTime)
								this.ResultReadByPhysicianDateTime = (System.DateTime?)value;
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
		/// Maps to TransCharges.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(TransChargesMetadata.ColumnNames.TransactionDate);
			}

			set
			{
				base.SetSystemDateTime(TransChargesMetadata.ColumnNames.TransactionDate, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.ExecutionDate
		/// </summary>
		virtual public System.DateTime? ExecutionDate
		{
			get
			{
				return base.GetSystemDateTime(TransChargesMetadata.ColumnNames.ExecutionDate);
			}

			set
			{
				base.SetSystemDateTime(TransChargesMetadata.ColumnNames.ExecutionDate, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.FromServiceUnitID
		/// </summary>
		virtual public System.String FromServiceUnitID
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.FromServiceUnitID);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.FromServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.ToServiceUnitID
		/// </summary>
		virtual public System.String ToServiceUnitID
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.ToServiceUnitID);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.ToServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.ClassID);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.ClassID, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.RoomID
		/// </summary>
		virtual public System.String RoomID
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.RoomID);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.RoomID, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.BedID
		/// </summary>
		virtual public System.String BedID
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.BedID);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.BedID, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.DueDate
		/// </summary>
		virtual public System.DateTime? DueDate
		{
			get
			{
				return base.GetSystemDateTime(TransChargesMetadata.ColumnNames.DueDate);
			}

			set
			{
				base.SetSystemDateTime(TransChargesMetadata.ColumnNames.DueDate, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.SRShift
		/// </summary>
		virtual public System.String SRShift
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.SRShift);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.SRShift, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.SRItemType
		/// </summary>
		virtual public System.String SRItemType
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.SRItemType);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.SRItemType, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.IsProceed
		/// </summary>
		virtual public System.Boolean? IsProceed
		{
			get
			{
				return base.GetSystemBoolean(TransChargesMetadata.ColumnNames.IsProceed);
			}

			set
			{
				base.SetSystemBoolean(TransChargesMetadata.ColumnNames.IsProceed, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(TransChargesMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(TransChargesMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(TransChargesMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(TransChargesMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.IsOrder
		/// </summary>
		virtual public System.Boolean? IsOrder
		{
			get
			{
				return base.GetSystemBoolean(TransChargesMetadata.ColumnNames.IsOrder);
			}

			set
			{
				base.SetSystemBoolean(TransChargesMetadata.ColumnNames.IsOrder, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.IsCorrection
		/// </summary>
		virtual public System.Boolean? IsCorrection
		{
			get
			{
				return base.GetSystemBoolean(TransChargesMetadata.ColumnNames.IsCorrection);
			}

			set
			{
				base.SetSystemBoolean(TransChargesMetadata.ColumnNames.IsCorrection, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.IsClusterAssign
		/// </summary>
		virtual public System.Boolean? IsClusterAssign
		{
			get
			{
				return base.GetSystemBoolean(TransChargesMetadata.ColumnNames.IsClusterAssign);
			}

			set
			{
				base.SetSystemBoolean(TransChargesMetadata.ColumnNames.IsClusterAssign, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.IsAutoBillTransaction
		/// </summary>
		virtual public System.Boolean? IsAutoBillTransaction
		{
			get
			{
				return base.GetSystemBoolean(TransChargesMetadata.ColumnNames.IsAutoBillTransaction);
			}

			set
			{
				base.SetSystemBoolean(TransChargesMetadata.ColumnNames.IsAutoBillTransaction, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.IsBillProceed
		/// </summary>
		virtual public System.Boolean? IsBillProceed
		{
			get
			{
				return base.GetSystemBoolean(TransChargesMetadata.ColumnNames.IsBillProceed);
			}

			set
			{
				base.SetSystemBoolean(TransChargesMetadata.ColumnNames.IsBillProceed, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransChargesMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransChargesMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.SRTypeResult
		/// </summary>
		virtual public System.String SRTypeResult
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.SRTypeResult);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.SRTypeResult, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.ResponUnitID
		/// </summary>
		virtual public System.String ResponUnitID
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.ResponUnitID);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.ResponUnitID, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.SurgicalPackageID
		/// </summary>
		virtual public System.String SurgicalPackageID
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.SurgicalPackageID);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.SurgicalPackageID, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.IsPackage
		/// </summary>
		virtual public System.Boolean? IsPackage
		{
			get
			{
				return base.GetSystemBoolean(TransChargesMetadata.ColumnNames.IsPackage);
			}

			set
			{
				base.SetSystemBoolean(TransChargesMetadata.ColumnNames.IsPackage, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.PackageReferenceNo
		/// </summary>
		virtual public System.String PackageReferenceNo
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.PackageReferenceNo);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.PackageReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.IsRoomIn
		/// </summary>
		virtual public System.Boolean? IsRoomIn
		{
			get
			{
				return base.GetSystemBoolean(TransChargesMetadata.ColumnNames.IsRoomIn);
			}

			set
			{
				base.SetSystemBoolean(TransChargesMetadata.ColumnNames.IsRoomIn, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.TariffDiscountForRoomIn
		/// </summary>
		virtual public System.Decimal? TariffDiscountForRoomIn
		{
			get
			{
				return base.GetSystemDecimal(TransChargesMetadata.ColumnNames.TariffDiscountForRoomIn);
			}

			set
			{
				base.SetSystemDecimal(TransChargesMetadata.ColumnNames.TariffDiscountForRoomIn, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.IsNonPatient
		/// </summary>
		virtual public System.Boolean? IsNonPatient
		{
			get
			{
				return base.GetSystemBoolean(TransChargesMetadata.ColumnNames.IsNonPatient);
			}

			set
			{
				base.SetSystemBoolean(TransChargesMetadata.ColumnNames.IsNonPatient, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.ServiceUnitBookingNo
		/// </summary>
		virtual public System.String ServiceUnitBookingNo
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.ServiceUnitBookingNo);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.ServiceUnitBookingNo, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransChargesMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransChargesMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.PhysicianSenders
		/// </summary>
		virtual public System.String PhysicianSenders
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.PhysicianSenders);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.PhysicianSenders, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.IsValidated
		/// </summary>
		virtual public System.Boolean? IsValidated
		{
			get
			{
				return base.GetSystemBoolean(TransChargesMetadata.ColumnNames.IsValidated);
			}

			set
			{
				base.SetSystemBoolean(TransChargesMetadata.ColumnNames.IsValidated, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.ValidatedDateTime
		/// </summary>
		virtual public System.DateTime? ValidatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransChargesMetadata.ColumnNames.ValidatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransChargesMetadata.ColumnNames.ValidatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.ValidatedByUserID
		/// </summary>
		virtual public System.String ValidatedByUserID
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.ValidatedByUserID);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.ValidatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.LocationID
		/// </summary>
		virtual public System.String LocationID
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.LocationID);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.LocationID, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.SRProdiaContractID
		/// </summary>
		virtual public System.String SRProdiaContractID
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.SRProdiaContractID);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.SRProdiaContractID, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.LaboratoryParamedicID
		/// </summary>
		virtual public System.String LaboratoryParamedicID
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.LaboratoryParamedicID);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.LaboratoryParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.SRBloodSampleTakenBy
		/// </summary>
		virtual public System.String SRBloodSampleTakenBy
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.SRBloodSampleTakenBy);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.SRBloodSampleTakenBy, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransChargesMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransChargesMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.AnalystID
		/// </summary>
		virtual public System.String AnalystID
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.AnalystID);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.AnalystID, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransChargesMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransChargesMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.SROrderStatus
		/// </summary>
		virtual public System.String SROrderStatus
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.SROrderStatus);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.SROrderStatus, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.ResultReadByPhysicianID
		/// </summary>
		virtual public System.String ResultReadByPhysicianID
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.ResultReadByPhysicianID);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.ResultReadByPhysicianID, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.ResultReadByPhysicianDateTime
		/// </summary>
		virtual public System.DateTime? ResultReadByPhysicianDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransChargesMetadata.ColumnNames.ResultReadByPhysicianDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransChargesMetadata.ColumnNames.ResultReadByPhysicianDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransCharges.ClinicalDiagnosis
		/// </summary>
		virtual public System.String ClinicalDiagnosis
		{
			get
			{
				return base.GetSystemString(TransChargesMetadata.ColumnNames.ClinicalDiagnosis);
			}

			set
			{
				base.SetSystemString(TransChargesMetadata.ColumnNames.ClinicalDiagnosis, value);
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
			public esStrings(esTransCharges entity)
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
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
			public System.String TransactionDate
			{
				get
				{
					System.DateTime? data = entity.TransactionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionDate = null;
					else entity.TransactionDate = Convert.ToDateTime(value);
				}
			}
			public System.String ExecutionDate
			{
				get
				{
					System.DateTime? data = entity.ExecutionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExecutionDate = null;
					else entity.ExecutionDate = Convert.ToDateTime(value);
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
			public System.String FromServiceUnitID
			{
				get
				{
					System.String data = entity.FromServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromServiceUnitID = null;
					else entity.FromServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String ToServiceUnitID
			{
				get
				{
					System.String data = entity.ToServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToServiceUnitID = null;
					else entity.ToServiceUnitID = Convert.ToString(value);
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
			public System.String RoomID
			{
				get
				{
					System.String data = entity.RoomID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RoomID = null;
					else entity.RoomID = Convert.ToString(value);
				}
			}
			public System.String BedID
			{
				get
				{
					System.String data = entity.BedID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BedID = null;
					else entity.BedID = Convert.ToString(value);
				}
			}
			public System.String DueDate
			{
				get
				{
					System.DateTime? data = entity.DueDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DueDate = null;
					else entity.DueDate = Convert.ToDateTime(value);
				}
			}
			public System.String SRShift
			{
				get
				{
					System.String data = entity.SRShift;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRShift = null;
					else entity.SRShift = Convert.ToString(value);
				}
			}
			public System.String SRItemType
			{
				get
				{
					System.String data = entity.SRItemType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemType = null;
					else entity.SRItemType = Convert.ToString(value);
				}
			}
			public System.String IsProceed
			{
				get
				{
					System.Boolean? data = entity.IsProceed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProceed = null;
					else entity.IsProceed = Convert.ToBoolean(value);
				}
			}
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
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
			public System.String IsOrder
			{
				get
				{
					System.Boolean? data = entity.IsOrder;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOrder = null;
					else entity.IsOrder = Convert.ToBoolean(value);
				}
			}
			public System.String IsCorrection
			{
				get
				{
					System.Boolean? data = entity.IsCorrection;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCorrection = null;
					else entity.IsCorrection = Convert.ToBoolean(value);
				}
			}
			public System.String IsClusterAssign
			{
				get
				{
					System.Boolean? data = entity.IsClusterAssign;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClusterAssign = null;
					else entity.IsClusterAssign = Convert.ToBoolean(value);
				}
			}
			public System.String IsAutoBillTransaction
			{
				get
				{
					System.Boolean? data = entity.IsAutoBillTransaction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAutoBillTransaction = null;
					else entity.IsAutoBillTransaction = Convert.ToBoolean(value);
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
			public System.String SRTypeResult
			{
				get
				{
					System.String data = entity.SRTypeResult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTypeResult = null;
					else entity.SRTypeResult = Convert.ToString(value);
				}
			}
			public System.String ResponUnitID
			{
				get
				{
					System.String data = entity.ResponUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResponUnitID = null;
					else entity.ResponUnitID = Convert.ToString(value);
				}
			}
			public System.String SurgicalPackageID
			{
				get
				{
					System.String data = entity.SurgicalPackageID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SurgicalPackageID = null;
					else entity.SurgicalPackageID = Convert.ToString(value);
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
			public System.String PackageReferenceNo
			{
				get
				{
					System.String data = entity.PackageReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PackageReferenceNo = null;
					else entity.PackageReferenceNo = Convert.ToString(value);
				}
			}
			public System.String IsRoomIn
			{
				get
				{
					System.Boolean? data = entity.IsRoomIn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRoomIn = null;
					else entity.IsRoomIn = Convert.ToBoolean(value);
				}
			}
			public System.String TariffDiscountForRoomIn
			{
				get
				{
					System.Decimal? data = entity.TariffDiscountForRoomIn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffDiscountForRoomIn = null;
					else entity.TariffDiscountForRoomIn = Convert.ToDecimal(value);
				}
			}
			public System.String IsNonPatient
			{
				get
				{
					System.Boolean? data = entity.IsNonPatient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNonPatient = null;
					else entity.IsNonPatient = Convert.ToBoolean(value);
				}
			}
			public System.String ServiceUnitBookingNo
			{
				get
				{
					System.String data = entity.ServiceUnitBookingNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitBookingNo = null;
					else entity.ServiceUnitBookingNo = Convert.ToString(value);
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
			public System.String PhysicianSenders
			{
				get
				{
					System.String data = entity.PhysicianSenders;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhysicianSenders = null;
					else entity.PhysicianSenders = Convert.ToString(value);
				}
			}
			public System.String IsValidated
			{
				get
				{
					System.Boolean? data = entity.IsValidated;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsValidated = null;
					else entity.IsValidated = Convert.ToBoolean(value);
				}
			}
			public System.String ValidatedDateTime
			{
				get
				{
					System.DateTime? data = entity.ValidatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidatedDateTime = null;
					else entity.ValidatedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ValidatedByUserID
			{
				get
				{
					System.String data = entity.ValidatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidatedByUserID = null;
					else entity.ValidatedByUserID = Convert.ToString(value);
				}
			}
			public System.String LocationID
			{
				get
				{
					System.String data = entity.LocationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationID = null;
					else entity.LocationID = Convert.ToString(value);
				}
			}
			public System.String SRProdiaContractID
			{
				get
				{
					System.String data = entity.SRProdiaContractID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProdiaContractID = null;
					else entity.SRProdiaContractID = Convert.ToString(value);
				}
			}
			public System.String LaboratoryParamedicID
			{
				get
				{
					System.String data = entity.LaboratoryParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LaboratoryParamedicID = null;
					else entity.LaboratoryParamedicID = Convert.ToString(value);
				}
			}
			public System.String SRBloodSampleTakenBy
			{
				get
				{
					System.String data = entity.SRBloodSampleTakenBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBloodSampleTakenBy = null;
					else entity.SRBloodSampleTakenBy = Convert.ToString(value);
				}
			}
			public System.String ApprovedDateTime
			{
				get
				{
					System.DateTime? data = entity.ApprovedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
					else entity.ApprovedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ApprovedByUserID
			{
				get
				{
					System.String data = entity.ApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
					else entity.ApprovedByUserID = Convert.ToString(value);
				}
			}
			public System.String AnalystID
			{
				get
				{
					System.String data = entity.AnalystID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnalystID = null;
					else entity.AnalystID = Convert.ToString(value);
				}
			}
			public System.String VoidDateTime
			{
				get
				{
					System.DateTime? data = entity.VoidDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDateTime = null;
					else entity.VoidDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VoidByUserID
			{
				get
				{
					System.String data = entity.VoidByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidByUserID = null;
					else entity.VoidByUserID = Convert.ToString(value);
				}
			}
			public System.String SROrderStatus
			{
				get
				{
					System.String data = entity.SROrderStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SROrderStatus = null;
					else entity.SROrderStatus = Convert.ToString(value);
				}
			}
			public System.String ResultReadByPhysicianID
			{
				get
				{
					System.String data = entity.ResultReadByPhysicianID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultReadByPhysicianID = null;
					else entity.ResultReadByPhysicianID = Convert.ToString(value);
				}
			}
			public System.String ResultReadByPhysicianDateTime
			{
				get
				{
					System.DateTime? data = entity.ResultReadByPhysicianDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultReadByPhysicianDateTime = null;
					else entity.ResultReadByPhysicianDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ClinicalDiagnosis
			{
				get
				{
					System.String data = entity.ClinicalDiagnosis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClinicalDiagnosis = null;
					else entity.ClinicalDiagnosis = Convert.ToString(value);
				}
			}
			private esTransCharges entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransChargesQuery query)
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
				throw new Exception("esTransCharges can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class TransCharges : esTransCharges
	{
	}

	[Serializable]
	abstract public class esTransChargesQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return TransChargesMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ExecutionDate
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.ExecutionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem FromServiceUnitID
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.FromServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem ToServiceUnitID
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.ToServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		}

		public esQueryItem RoomID
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.RoomID, esSystemType.String);
			}
		}

		public esQueryItem BedID
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.BedID, esSystemType.String);
			}
		}

		public esQueryItem DueDate
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.DueDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SRShift
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.SRShift, esSystemType.String);
			}
		}

		public esQueryItem SRItemType
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.SRItemType, esSystemType.String);
			}
		}

		public esQueryItem IsProceed
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.IsProceed, esSystemType.Boolean);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem IsOrder
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.IsOrder, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCorrection
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.IsCorrection, esSystemType.Boolean);
			}
		}

		public esQueryItem IsClusterAssign
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.IsClusterAssign, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAutoBillTransaction
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.IsAutoBillTransaction, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBillProceed
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.IsBillProceed, esSystemType.Boolean);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SRTypeResult
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.SRTypeResult, esSystemType.String);
			}
		}

		public esQueryItem ResponUnitID
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.ResponUnitID, esSystemType.String);
			}
		}

		public esQueryItem SurgicalPackageID
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.SurgicalPackageID, esSystemType.String);
			}
		}

		public esQueryItem IsPackage
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.IsPackage, esSystemType.Boolean);
			}
		}

		public esQueryItem PackageReferenceNo
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.PackageReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem IsRoomIn
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.IsRoomIn, esSystemType.Boolean);
			}
		}

		public esQueryItem TariffDiscountForRoomIn
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.TariffDiscountForRoomIn, esSystemType.Decimal);
			}
		}

		public esQueryItem IsNonPatient
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.IsNonPatient, esSystemType.Boolean);
			}
		}

		public esQueryItem ServiceUnitBookingNo
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.ServiceUnitBookingNo, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem PhysicianSenders
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.PhysicianSenders, esSystemType.String);
			}
		}

		public esQueryItem IsValidated
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.IsValidated, esSystemType.Boolean);
			}
		}

		public esQueryItem ValidatedDateTime
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.ValidatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidatedByUserID
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.ValidatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LocationID
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.LocationID, esSystemType.String);
			}
		}

		public esQueryItem SRProdiaContractID
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.SRProdiaContractID, esSystemType.String);
			}
		}

		public esQueryItem LaboratoryParamedicID
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.LaboratoryParamedicID, esSystemType.String);
			}
		}

		public esQueryItem SRBloodSampleTakenBy
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.SRBloodSampleTakenBy, esSystemType.String);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem AnalystID
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.AnalystID, esSystemType.String);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem SROrderStatus
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.SROrderStatus, esSystemType.String);
			}
		}

		public esQueryItem ResultReadByPhysicianID
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.ResultReadByPhysicianID, esSystemType.String);
			}
		}

		public esQueryItem ResultReadByPhysicianDateTime
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.ResultReadByPhysicianDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ClinicalDiagnosis
		{
			get
			{
				return new esQueryItem(this, TransChargesMetadata.ColumnNames.ClinicalDiagnosis, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransChargesCollection")]
	public partial class TransChargesCollection : esTransChargesCollection, IEnumerable<TransCharges>
	{
		public TransChargesCollection()
		{

		}

		public static implicit operator List<TransCharges>(TransChargesCollection coll)
		{
			List<TransCharges> list = new List<TransCharges>();

			foreach (TransCharges emp in coll)
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
				return TransChargesMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransCharges(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransCharges();
		}

		#endregion

		[BrowsableAttribute(false)]
		public TransChargesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesQuery();
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
		public bool Load(TransChargesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public TransCharges AddNew()
		{
			TransCharges entity = base.AddNewEntity() as TransCharges;

			return entity;
		}
		public TransCharges FindByPrimaryKey(String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as TransCharges;
		}

		#region IEnumerable< TransCharges> Members

		IEnumerator<TransCharges> IEnumerable<TransCharges>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as TransCharges;
			}
		}

		#endregion

		private TransChargesQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransCharges' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("TransCharges ({TransactionNo})")]
	[Serializable]
	public partial class TransCharges : esTransCharges
	{
		public TransCharges()
		{
		}

		public TransCharges(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransChargesMetadata.Meta();
			}
		}

		override protected esTransChargesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public TransChargesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesQuery();
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
		public bool Load(TransChargesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private TransChargesQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class TransChargesQuery : esTransChargesQuery
	{
		public TransChargesQuery()
		{

		}

		public TransChargesQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "TransChargesQuery";
		}
	}

	[Serializable]
	public partial class TransChargesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransChargesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.TransactionDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesMetadata.PropertyNames.TransactionDate;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.ExecutionDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesMetadata.PropertyNames.ExecutionDate;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.ReferenceNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.FromServiceUnitID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.FromServiceUnitID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.ToServiceUnitID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.ToServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.ClassID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.ClassID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.RoomID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.RoomID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.BedID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.BedID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.DueDate, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesMetadata.PropertyNames.DueDate;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.SRShift, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.SRShift;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.SRItemType, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.SRItemType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.IsProceed, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesMetadata.PropertyNames.IsProceed;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.IsApproved, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesMetadata.PropertyNames.IsApproved;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.IsVoid, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.IsOrder, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesMetadata.PropertyNames.IsOrder;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.IsCorrection, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesMetadata.PropertyNames.IsCorrection;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.IsClusterAssign, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesMetadata.PropertyNames.IsClusterAssign;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.IsAutoBillTransaction, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesMetadata.PropertyNames.IsAutoBillTransaction;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.IsBillProceed, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesMetadata.PropertyNames.IsBillProceed;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.Notes, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.LastUpdateDateTime, 22, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.LastUpdateByUserID, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.SRTypeResult, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.SRTypeResult;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.ResponUnitID, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.ResponUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.SurgicalPackageID, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.SurgicalPackageID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.IsPackage, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesMetadata.PropertyNames.IsPackage;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.PackageReferenceNo, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.PackageReferenceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.IsRoomIn, 29, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesMetadata.PropertyNames.IsRoomIn;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.TariffDiscountForRoomIn, 30, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesMetadata.PropertyNames.TariffDiscountForRoomIn;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.IsNonPatient, 31, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesMetadata.PropertyNames.IsNonPatient;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.ServiceUnitBookingNo, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.ServiceUnitBookingNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.CreatedDateTime, 33, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.CreatedByUserID, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.PhysicianSenders, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.PhysicianSenders;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.IsValidated, 36, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesMetadata.PropertyNames.IsValidated;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.ValidatedDateTime, 37, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesMetadata.PropertyNames.ValidatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.ValidatedByUserID, 38, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.ValidatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.LocationID, 39, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.LocationID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.SRProdiaContractID, 40, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.SRProdiaContractID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.LaboratoryParamedicID, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.LaboratoryParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.SRBloodSampleTakenBy, 42, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.SRBloodSampleTakenBy;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.ApprovedDateTime, 43, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.ApprovedByUserID, 44, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.AnalystID, 45, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.AnalystID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.VoidDateTime, 46, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.VoidByUserID, 47, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.SROrderStatus, 48, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.SROrderStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.ResultReadByPhysicianID, 49, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.ResultReadByPhysicianID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.ResultReadByPhysicianDateTime, 50, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesMetadata.PropertyNames.ResultReadByPhysicianDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesMetadata.ColumnNames.ClinicalDiagnosis, 51, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesMetadata.PropertyNames.ClinicalDiagnosis;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public TransChargesMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string TransactionDate = "TransactionDate";
			public const string ExecutionDate = "ExecutionDate";
			public const string ReferenceNo = "ReferenceNo";
			public const string FromServiceUnitID = "FromServiceUnitID";
			public const string ToServiceUnitID = "ToServiceUnitID";
			public const string ClassID = "ClassID";
			public const string RoomID = "RoomID";
			public const string BedID = "BedID";
			public const string DueDate = "DueDate";
			public const string SRShift = "SRShift";
			public const string SRItemType = "SRItemType";
			public const string IsProceed = "IsProceed";
			public const string IsApproved = "IsApproved";
			public const string IsVoid = "IsVoid";
			public const string IsOrder = "IsOrder";
			public const string IsCorrection = "IsCorrection";
			public const string IsClusterAssign = "IsClusterAssign";
			public const string IsAutoBillTransaction = "IsAutoBillTransaction";
			public const string IsBillProceed = "IsBillProceed";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRTypeResult = "SRTypeResult";
			public const string ResponUnitID = "ResponUnitID";
			public const string SurgicalPackageID = "SurgicalPackageID";
			public const string IsPackage = "IsPackage";
			public const string PackageReferenceNo = "PackageReferenceNo";
			public const string IsRoomIn = "IsRoomIn";
			public const string TariffDiscountForRoomIn = "TariffDiscountForRoomIn";
			public const string IsNonPatient = "IsNonPatient";
			public const string ServiceUnitBookingNo = "ServiceUnitBookingNo";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string PhysicianSenders = "PhysicianSenders";
			public const string IsValidated = "IsValidated";
			public const string ValidatedDateTime = "ValidatedDateTime";
			public const string ValidatedByUserID = "ValidatedByUserID";
			public const string LocationID = "LocationID";
			public const string SRProdiaContractID = "SRProdiaContractID";
			public const string LaboratoryParamedicID = "LaboratoryParamedicID";
			public const string SRBloodSampleTakenBy = "SRBloodSampleTakenBy";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string AnalystID = "AnalystID";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string SROrderStatus = "SROrderStatus";
			public const string ResultReadByPhysicianID = "ResultReadByPhysicianID";
			public const string ResultReadByPhysicianDateTime = "ResultReadByPhysicianDateTime";
			public const string ClinicalDiagnosis = "ClinicalDiagnosis";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string TransactionDate = "TransactionDate";
			public const string ExecutionDate = "ExecutionDate";
			public const string ReferenceNo = "ReferenceNo";
			public const string FromServiceUnitID = "FromServiceUnitID";
			public const string ToServiceUnitID = "ToServiceUnitID";
			public const string ClassID = "ClassID";
			public const string RoomID = "RoomID";
			public const string BedID = "BedID";
			public const string DueDate = "DueDate";
			public const string SRShift = "SRShift";
			public const string SRItemType = "SRItemType";
			public const string IsProceed = "IsProceed";
			public const string IsApproved = "IsApproved";
			public const string IsVoid = "IsVoid";
			public const string IsOrder = "IsOrder";
			public const string IsCorrection = "IsCorrection";
			public const string IsClusterAssign = "IsClusterAssign";
			public const string IsAutoBillTransaction = "IsAutoBillTransaction";
			public const string IsBillProceed = "IsBillProceed";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRTypeResult = "SRTypeResult";
			public const string ResponUnitID = "ResponUnitID";
			public const string SurgicalPackageID = "SurgicalPackageID";
			public const string IsPackage = "IsPackage";
			public const string PackageReferenceNo = "PackageReferenceNo";
			public const string IsRoomIn = "IsRoomIn";
			public const string TariffDiscountForRoomIn = "TariffDiscountForRoomIn";
			public const string IsNonPatient = "IsNonPatient";
			public const string ServiceUnitBookingNo = "ServiceUnitBookingNo";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string PhysicianSenders = "PhysicianSenders";
			public const string IsValidated = "IsValidated";
			public const string ValidatedDateTime = "ValidatedDateTime";
			public const string ValidatedByUserID = "ValidatedByUserID";
			public const string LocationID = "LocationID";
			public const string SRProdiaContractID = "SRProdiaContractID";
			public const string LaboratoryParamedicID = "LaboratoryParamedicID";
			public const string SRBloodSampleTakenBy = "SRBloodSampleTakenBy";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string AnalystID = "AnalystID";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string SROrderStatus = "SROrderStatus";
			public const string ResultReadByPhysicianID = "ResultReadByPhysicianID";
			public const string ResultReadByPhysicianDateTime = "ResultReadByPhysicianDateTime";
			public const string ClinicalDiagnosis = "ClinicalDiagnosis";
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
			lock (typeof(TransChargesMetadata))
			{
				if (TransChargesMetadata.mapDelegates == null)
				{
					TransChargesMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (TransChargesMetadata.meta == null)
				{
					TransChargesMetadata.meta = new TransChargesMetadata();
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

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ExecutionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DueDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("SRShift", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsProceed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsOrder", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCorrection", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsClusterAssign", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAutoBillTransaction", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBillProceed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRTypeResult", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResponUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SurgicalPackageID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPackage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PackageReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRoomIn", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("TariffDiscountForRoomIn", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsNonPatient", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ServiceUnitBookingNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhysicianSenders", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsValidated", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRProdiaContractID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LaboratoryParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBloodSampleTakenBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AnalystID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SROrderStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResultReadByPhysicianID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResultReadByPhysicianDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ClinicalDiagnosis", new esTypeMap("varchar", "System.String"));


				meta.Source = "TransCharges";
				meta.Destination = "TransCharges";
				meta.spInsert = "proc_TransChargesInsert";
				meta.spUpdate = "proc_TransChargesUpdate";
				meta.spDelete = "proc_TransChargesDelete";
				meta.spLoadAll = "proc_TransChargesLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransChargesLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransChargesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
