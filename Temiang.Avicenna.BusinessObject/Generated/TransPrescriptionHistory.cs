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
	abstract public class esTransPrescriptionHistoryCollection : esEntityCollectionWAuditLog
	{
		public esTransPrescriptionHistoryCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TransPrescriptionHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransPrescriptionHistoryQuery query)
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
			this.InitQuery(query as esTransPrescriptionHistoryQuery);
		}
		#endregion
		
		virtual public TransPrescriptionHistory DetachEntity(TransPrescriptionHistory entity)
		{
			return base.DetachEntity(entity) as TransPrescriptionHistory;
		}
		
		virtual public TransPrescriptionHistory AttachEntity(TransPrescriptionHistory entity)
		{
			return base.AttachEntity(entity) as TransPrescriptionHistory;
		}
		
		virtual public void Combine(TransPrescriptionHistoryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransPrescriptionHistory this[int index]
		{
			get
			{
				return base[index] as TransPrescriptionHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransPrescriptionHistory);
		}
	}



	[Serializable]
	abstract public class esTransPrescriptionHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransPrescriptionHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransPrescriptionHistory()
		{

		}

		public esTransPrescriptionHistory(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String recalculationProcessNo, System.String prescriptionNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(recalculationProcessNo, prescriptionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(recalculationProcessNo, prescriptionNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String recalculationProcessNo, System.String prescriptionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(recalculationProcessNo, prescriptionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(recalculationProcessNo, prescriptionNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String recalculationProcessNo, System.String prescriptionNo)
		{
			esTransPrescriptionHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.RecalculationProcessNo == recalculationProcessNo, query.PrescriptionNo == prescriptionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String recalculationProcessNo, System.String prescriptionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RecalculationProcessNo",recalculationProcessNo);			parms.Add("PrescriptionNo",prescriptionNo);
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
						case "PrescriptionDate": this.str.PrescriptionDate = (string)value; break;							
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "ClassID": this.str.ClassID = (string)value; break;							
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "IsApproval": this.str.IsApproval = (string)value; break;							
						case "IsVoid": this.str.IsVoid = (string)value; break;							
						case "Note": this.str.Note = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "IsPrescriptionReturn": this.str.IsPrescriptionReturn = (string)value; break;							
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;							
						case "IsFromSOAP": this.str.IsFromSOAP = (string)value; break;							
						case "IsBillProceed": this.str.IsBillProceed = (string)value; break;							
						case "IsUnitDosePrescription": this.str.IsUnitDosePrescription = (string)value; break;							
						case "IsCito": this.str.IsCito = (string)value; break;							
						case "IsClosed": this.str.IsClosed = (string)value; break;							
						case "ApprovalDateTime": this.str.ApprovalDateTime = (string)value; break;							
						case "DeliverDateTime": this.str.DeliverDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PrescriptionDate":
						
							if (value == null || value is System.DateTime)
								this.PrescriptionDate = (System.DateTime?)value;
							break;
						
						case "IsApproval":
						
							if (value == null || value is System.Boolean)
								this.IsApproval = (System.Boolean?)value;
							break;
						
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "IsPrescriptionReturn":
						
							if (value == null || value is System.Boolean)
								this.IsPrescriptionReturn = (System.Boolean?)value;
							break;
						
						case "IsFromSOAP":
						
							if (value == null || value is System.Boolean)
								this.IsFromSOAP = (System.Boolean?)value;
							break;
						
						case "IsBillProceed":
						
							if (value == null || value is System.Boolean)
								this.IsBillProceed = (System.Boolean?)value;
							break;
						
						case "IsUnitDosePrescription":
						
							if (value == null || value is System.Boolean)
								this.IsUnitDosePrescription = (System.Boolean?)value;
							break;
						
						case "IsCito":
						
							if (value == null || value is System.Boolean)
								this.IsCito = (System.Boolean?)value;
							break;
						
						case "IsClosed":
						
							if (value == null || value is System.Boolean)
								this.IsClosed = (System.Boolean?)value;
							break;
						
						case "ApprovalDateTime":
						
							if (value == null || value is System.DateTime)
								this.ApprovalDateTime = (System.DateTime?)value;
							break;
						
						case "DeliverDateTime":
						
							if (value == null || value is System.DateTime)
								this.DeliverDateTime = (System.DateTime?)value;
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
		/// Maps to TransPrescriptionHistory.RecalculationProcessNo
		/// </summary>
		virtual public System.String RecalculationProcessNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionHistoryMetadata.ColumnNames.RecalculationProcessNo);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionHistoryMetadata.ColumnNames.RecalculationProcessNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.PrescriptionNo
		/// </summary>
		virtual public System.String PrescriptionNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionHistoryMetadata.ColumnNames.PrescriptionNo);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionHistoryMetadata.ColumnNames.PrescriptionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.PrescriptionDate
		/// </summary>
		virtual public System.DateTime? PrescriptionDate
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionHistoryMetadata.ColumnNames.PrescriptionDate);
			}
			
			set
			{
				base.SetSystemDateTime(TransPrescriptionHistoryMetadata.ColumnNames.PrescriptionDate, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionHistoryMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionHistoryMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionHistoryMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionHistoryMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionHistoryMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionHistoryMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionHistoryMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionHistoryMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.IsApproval
		/// </summary>
		virtual public System.Boolean? IsApproval
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionHistoryMetadata.ColumnNames.IsApproval);
			}
			
			set
			{
				base.SetSystemBoolean(TransPrescriptionHistoryMetadata.ColumnNames.IsApproval, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionHistoryMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(TransPrescriptionHistoryMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(TransPrescriptionHistoryMetadata.ColumnNames.Note);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionHistoryMetadata.ColumnNames.Note, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransPrescriptionHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.IsPrescriptionReturn
		/// </summary>
		virtual public System.Boolean? IsPrescriptionReturn
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionHistoryMetadata.ColumnNames.IsPrescriptionReturn);
			}
			
			set
			{
				base.SetSystemBoolean(TransPrescriptionHistoryMetadata.ColumnNames.IsPrescriptionReturn, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionHistoryMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionHistoryMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.IsFromSOAP
		/// </summary>
		virtual public System.Boolean? IsFromSOAP
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionHistoryMetadata.ColumnNames.IsFromSOAP);
			}
			
			set
			{
				base.SetSystemBoolean(TransPrescriptionHistoryMetadata.ColumnNames.IsFromSOAP, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.IsBillProceed
		/// </summary>
		virtual public System.Boolean? IsBillProceed
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionHistoryMetadata.ColumnNames.IsBillProceed);
			}
			
			set
			{
				base.SetSystemBoolean(TransPrescriptionHistoryMetadata.ColumnNames.IsBillProceed, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.IsUnitDosePrescription
		/// </summary>
		virtual public System.Boolean? IsUnitDosePrescription
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionHistoryMetadata.ColumnNames.IsUnitDosePrescription);
			}
			
			set
			{
				base.SetSystemBoolean(TransPrescriptionHistoryMetadata.ColumnNames.IsUnitDosePrescription, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.IsCito
		/// </summary>
		virtual public System.Boolean? IsCito
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionHistoryMetadata.ColumnNames.IsCito);
			}
			
			set
			{
				base.SetSystemBoolean(TransPrescriptionHistoryMetadata.ColumnNames.IsCito, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.IsClosed
		/// </summary>
		virtual public System.Boolean? IsClosed
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionHistoryMetadata.ColumnNames.IsClosed);
			}
			
			set
			{
				base.SetSystemBoolean(TransPrescriptionHistoryMetadata.ColumnNames.IsClosed, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.ApprovalDateTime
		/// </summary>
		virtual public System.DateTime? ApprovalDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionHistoryMetadata.ColumnNames.ApprovalDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransPrescriptionHistoryMetadata.ColumnNames.ApprovalDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionHistory.DeliverDateTime
		/// </summary>
		virtual public System.DateTime? DeliverDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionHistoryMetadata.ColumnNames.DeliverDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransPrescriptionHistoryMetadata.ColumnNames.DeliverDateTime, value);
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
			public esStrings(esTransPrescriptionHistory entity)
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
				
			public System.String PrescriptionDate
			{
				get
				{
					System.DateTime? data = entity.PrescriptionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrescriptionDate = null;
					else entity.PrescriptionDate = Convert.ToDateTime(value);
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
				
			public System.String IsApproval
			{
				get
				{
					System.Boolean? data = entity.IsApproval;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproval = null;
					else entity.IsApproval = Convert.ToBoolean(value);
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
				
			public System.String IsPrescriptionReturn
			{
				get
				{
					System.Boolean? data = entity.IsPrescriptionReturn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPrescriptionReturn = null;
					else entity.IsPrescriptionReturn = Convert.ToBoolean(value);
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
				
			public System.String IsFromSOAP
			{
				get
				{
					System.Boolean? data = entity.IsFromSOAP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFromSOAP = null;
					else entity.IsFromSOAP = Convert.ToBoolean(value);
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
				
			public System.String IsUnitDosePrescription
			{
				get
				{
					System.Boolean? data = entity.IsUnitDosePrescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUnitDosePrescription = null;
					else entity.IsUnitDosePrescription = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsCito
			{
				get
				{
					System.Boolean? data = entity.IsCito;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCito = null;
					else entity.IsCito = Convert.ToBoolean(value);
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
				
			public System.String ApprovalDateTime
			{
				get
				{
					System.DateTime? data = entity.ApprovalDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovalDateTime = null;
					else entity.ApprovalDateTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String DeliverDateTime
			{
				get
				{
					System.DateTime? data = entity.DeliverDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeliverDateTime = null;
					else entity.DeliverDateTime = Convert.ToDateTime(value);
				}
			}
			

			private esTransPrescriptionHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransPrescriptionHistoryQuery query)
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
				throw new Exception("esTransPrescriptionHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class TransPrescriptionHistory : esTransPrescriptionHistory
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
	abstract public class esTransPrescriptionHistoryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TransPrescriptionHistoryMetadata.Meta();
			}
		}	
		

		public esQueryItem RecalculationProcessNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.RecalculationProcessNo, esSystemType.String);
			}
		} 
		
		public esQueryItem PrescriptionNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.PrescriptionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem PrescriptionDate
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.PrescriptionDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsApproval
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.IsApproval, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.Note, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsPrescriptionReturn
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.IsPrescriptionReturn, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem IsFromSOAP
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.IsFromSOAP, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsBillProceed
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.IsBillProceed, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsUnitDosePrescription
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.IsUnitDosePrescription, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsCito
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.IsCito, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsClosed
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ApprovalDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.ApprovalDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem DeliverDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionHistoryMetadata.ColumnNames.DeliverDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransPrescriptionHistoryCollection")]
	public partial class TransPrescriptionHistoryCollection : esTransPrescriptionHistoryCollection, IEnumerable<TransPrescriptionHistory>
	{
		public TransPrescriptionHistoryCollection()
		{

		}
		
		public static implicit operator List<TransPrescriptionHistory>(TransPrescriptionHistoryCollection coll)
		{
			List<TransPrescriptionHistory> list = new List<TransPrescriptionHistory>();
			
			foreach (TransPrescriptionHistory emp in coll)
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
				return  TransPrescriptionHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPrescriptionHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransPrescriptionHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransPrescriptionHistory();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TransPrescriptionHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPrescriptionHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TransPrescriptionHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public TransPrescriptionHistory AddNew()
		{
			TransPrescriptionHistory entity = base.AddNewEntity() as TransPrescriptionHistory;
			
			return entity;
		}

		public TransPrescriptionHistory FindByPrimaryKey(System.String recalculationProcessNo, System.String prescriptionNo)
		{
			return base.FindByPrimaryKey(recalculationProcessNo, prescriptionNo) as TransPrescriptionHistory;
		}


		#region IEnumerable<TransPrescriptionHistory> Members

		IEnumerator<TransPrescriptionHistory> IEnumerable<TransPrescriptionHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransPrescriptionHistory;
			}
		}

		#endregion
		
		private TransPrescriptionHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransPrescriptionHistory' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("TransPrescriptionHistory ({RecalculationProcessNo},{PrescriptionNo})")]
	[Serializable]
	public partial class TransPrescriptionHistory : esTransPrescriptionHistory
	{
		public TransPrescriptionHistory()
		{

		}
	
		public TransPrescriptionHistory(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransPrescriptionHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esTransPrescriptionHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPrescriptionHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TransPrescriptionHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPrescriptionHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TransPrescriptionHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TransPrescriptionHistoryQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TransPrescriptionHistoryQuery : esTransPrescriptionHistoryQuery
	{
		public TransPrescriptionHistoryQuery()
		{

		}		
		
		public TransPrescriptionHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TransPrescriptionHistoryQuery";
        }
		
			
	}


	[Serializable]
	public partial class TransPrescriptionHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransPrescriptionHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.RecalculationProcessNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.RecalculationProcessNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.PrescriptionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.PrescriptionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.PrescriptionDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.PrescriptionDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.RegistrationNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.ServiceUnitID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.ClassID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.ClassID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.ParamedicID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.IsApproval, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.IsApproval;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.IsVoid, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.IsVoid;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.Note, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.IsPrescriptionReturn, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.IsPrescriptionReturn;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.ReferenceNo, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.IsFromSOAP, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.IsFromSOAP;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.IsBillProceed, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.IsBillProceed;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.IsUnitDosePrescription, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.IsUnitDosePrescription;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.IsCito, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.IsCito;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.IsClosed, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.IsClosed;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.ApprovalDateTime, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.ApprovalDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionHistoryMetadata.ColumnNames.DeliverDateTime, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionHistoryMetadata.PropertyNames.DeliverDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TransPrescriptionHistoryMetadata Meta()
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
			 public const string PrescriptionDate = "PrescriptionDate";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ClassID = "ClassID";
			 public const string ParamedicID = "ParamedicID";
			 public const string IsApproval = "IsApproval";
			 public const string IsVoid = "IsVoid";
			 public const string Note = "Note";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsPrescriptionReturn = "IsPrescriptionReturn";
			 public const string ReferenceNo = "ReferenceNo";
			 public const string IsFromSOAP = "IsFromSOAP";
			 public const string IsBillProceed = "IsBillProceed";
			 public const string IsUnitDosePrescription = "IsUnitDosePrescription";
			 public const string IsCito = "IsCito";
			 public const string IsClosed = "IsClosed";
			 public const string ApprovalDateTime = "ApprovalDateTime";
			 public const string DeliverDateTime = "DeliverDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RecalculationProcessNo = "RecalculationProcessNo";
			 public const string PrescriptionNo = "PrescriptionNo";
			 public const string PrescriptionDate = "PrescriptionDate";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ClassID = "ClassID";
			 public const string ParamedicID = "ParamedicID";
			 public const string IsApproval = "IsApproval";
			 public const string IsVoid = "IsVoid";
			 public const string Note = "Note";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsPrescriptionReturn = "IsPrescriptionReturn";
			 public const string ReferenceNo = "ReferenceNo";
			 public const string IsFromSOAP = "IsFromSOAP";
			 public const string IsBillProceed = "IsBillProceed";
			 public const string IsUnitDosePrescription = "IsUnitDosePrescription";
			 public const string IsCito = "IsCito";
			 public const string IsClosed = "IsClosed";
			 public const string ApprovalDateTime = "ApprovalDateTime";
			 public const string DeliverDateTime = "DeliverDateTime";
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
			lock (typeof(TransPrescriptionHistoryMetadata))
			{
				if(TransPrescriptionHistoryMetadata.mapDelegates == null)
				{
					TransPrescriptionHistoryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransPrescriptionHistoryMetadata.meta == null)
				{
					TransPrescriptionHistoryMetadata.meta = new TransPrescriptionHistoryMetadata();
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
				meta.AddTypeMap("PrescriptionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproval", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPrescriptionReturn", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsFromSOAP", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBillProceed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUnitDosePrescription", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCito", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovalDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("DeliverDateTime", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "TransPrescriptionHistory";
				meta.Destination = "TransPrescriptionHistory";
				
				meta.spInsert = "proc_TransPrescriptionHistoryInsert";				
				meta.spUpdate = "proc_TransPrescriptionHistoryUpdate";		
				meta.spDelete = "proc_TransPrescriptionHistoryDelete";
				meta.spLoadAll = "proc_TransPrescriptionHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransPrescriptionHistoryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransPrescriptionHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
