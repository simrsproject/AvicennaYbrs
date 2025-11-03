/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:19 PM
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
	abstract public class esMedicalBenefitClaimItemCollection : esEntityCollectionWAuditLog
	{
		public esMedicalBenefitClaimItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "MedicalBenefitClaimItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esMedicalBenefitClaimItemQuery query)
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
			this.InitQuery(query as esMedicalBenefitClaimItemQuery);
		}
		#endregion
		
		virtual public MedicalBenefitClaimItem DetachEntity(MedicalBenefitClaimItem entity)
		{
			return base.DetachEntity(entity) as MedicalBenefitClaimItem;
		}
		
		virtual public MedicalBenefitClaimItem AttachEntity(MedicalBenefitClaimItem entity)
		{
			return base.AttachEntity(entity) as MedicalBenefitClaimItem;
		}
		
		virtual public void Combine(MedicalBenefitClaimItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public MedicalBenefitClaimItem this[int index]
		{
			get
			{
				return base[index] as MedicalBenefitClaimItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicalBenefitClaimItem);
		}
	}



	[Serializable]
	abstract public class esMedicalBenefitClaimItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicalBenefitClaimItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esMedicalBenefitClaimItem()
		{

		}

		public esMedicalBenefitClaimItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 medicalBenefitClaimItemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicalBenefitClaimItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(medicalBenefitClaimItemID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 medicalBenefitClaimItemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicalBenefitClaimItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(medicalBenefitClaimItemID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 medicalBenefitClaimItemID)
		{
			esMedicalBenefitClaimItemQuery query = this.GetDynamicQuery();
			query.Where(query.MedicalBenefitClaimItemID == medicalBenefitClaimItemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 medicalBenefitClaimItemID)
		{
			esParameters parms = new esParameters();
			parms.Add("MedicalBenefitClaimItemID",medicalBenefitClaimItemID);
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
						case "MedicalBenefitClaimItemID": this.str.MedicalBenefitClaimItemID = (string)value; break;							
						case "MedicalBenefitClaimID": this.str.MedicalBenefitClaimID = (string)value; break;							
						case "TreatedID": this.str.TreatedID = (string)value; break;							
						case "ReceiptNo": this.str.ReceiptNo = (string)value; break;							
						case "TreatmentDate": this.str.TreatmentDate = (string)value; break;							
						case "VisitTypeID": this.str.VisitTypeID = (string)value; break;							
						case "PhysicianName": this.str.PhysicianName = (string)value; break;							
						case "HospitalInfoID": this.str.HospitalInfoID = (string)value; break;							
						case "IsOccupationalInjury": this.str.IsOccupationalInjury = (string)value; break;							
						case "AmountOnBill": this.str.AmountOnBill = (string)value; break;							
						case "ReAmount": this.str.ReAmount = (string)value; break;							
						case "ApprovedAmount": this.str.ApprovedAmount = (string)value; break;							
						case "Note": this.str.Note = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "MedicalBenefitClaimItemID":
						
							if (value == null || value is System.Int32)
								this.MedicalBenefitClaimItemID = (System.Int32?)value;
							break;
						
						case "MedicalBenefitClaimID":
						
							if (value == null || value is System.Int32)
								this.MedicalBenefitClaimID = (System.Int32?)value;
							break;
						
						case "TreatedID":
						
							if (value == null || value is System.Int32)
								this.TreatedID = (System.Int32?)value;
							break;
						
						case "TreatmentDate":
						
							if (value == null || value is System.DateTime)
								this.TreatmentDate = (System.DateTime?)value;
							break;
						
						case "HospitalInfoID":
						
							if (value == null || value is System.Int32)
								this.HospitalInfoID = (System.Int32?)value;
							break;
						
						case "IsOccupationalInjury":
						
							if (value == null || value is System.Boolean)
								this.IsOccupationalInjury = (System.Boolean?)value;
							break;
						
						case "AmountOnBill":
						
							if (value == null || value is System.Decimal)
								this.AmountOnBill = (System.Decimal?)value;
							break;
						
						case "ReAmount":
						
							if (value == null || value is System.Decimal)
								this.ReAmount = (System.Decimal?)value;
							break;
						
						case "ApprovedAmount":
						
							if (value == null || value is System.Decimal)
								this.ApprovedAmount = (System.Decimal?)value;
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
		/// Maps to MedicalBenefitClaimItem.MedicalBenefitClaimItemID
		/// </summary>
		virtual public System.Int32? MedicalBenefitClaimItemID
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitClaimItemMetadata.ColumnNames.MedicalBenefitClaimItemID);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitClaimItemMetadata.ColumnNames.MedicalBenefitClaimItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaimItem.MedicalBenefitClaimID
		/// </summary>
		virtual public System.Int32? MedicalBenefitClaimID
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitClaimItemMetadata.ColumnNames.MedicalBenefitClaimID);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitClaimItemMetadata.ColumnNames.MedicalBenefitClaimID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaimItem.TreatedID
		/// </summary>
		virtual public System.Int32? TreatedID
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitClaimItemMetadata.ColumnNames.TreatedID);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitClaimItemMetadata.ColumnNames.TreatedID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaimItem.ReceiptNo
		/// </summary>
		virtual public System.String ReceiptNo
		{
			get
			{
				return base.GetSystemString(MedicalBenefitClaimItemMetadata.ColumnNames.ReceiptNo);
			}
			
			set
			{
				base.SetSystemString(MedicalBenefitClaimItemMetadata.ColumnNames.ReceiptNo, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaimItem.TreatmentDate
		/// </summary>
		virtual public System.DateTime? TreatmentDate
		{
			get
			{
				return base.GetSystemDateTime(MedicalBenefitClaimItemMetadata.ColumnNames.TreatmentDate);
			}
			
			set
			{
				base.SetSystemDateTime(MedicalBenefitClaimItemMetadata.ColumnNames.TreatmentDate, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaimItem.VisitTypeID
		/// </summary>
		virtual public System.String VisitTypeID
		{
			get
			{
				return base.GetSystemString(MedicalBenefitClaimItemMetadata.ColumnNames.VisitTypeID);
			}
			
			set
			{
				base.SetSystemString(MedicalBenefitClaimItemMetadata.ColumnNames.VisitTypeID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaimItem.PhysicianName
		/// </summary>
		virtual public System.String PhysicianName
		{
			get
			{
				return base.GetSystemString(MedicalBenefitClaimItemMetadata.ColumnNames.PhysicianName);
			}
			
			set
			{
				base.SetSystemString(MedicalBenefitClaimItemMetadata.ColumnNames.PhysicianName, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaimItem.HospitalInfoID
		/// </summary>
		virtual public System.Int32? HospitalInfoID
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitClaimItemMetadata.ColumnNames.HospitalInfoID);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitClaimItemMetadata.ColumnNames.HospitalInfoID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaimItem.IsOccupationalInjury
		/// </summary>
		virtual public System.Boolean? IsOccupationalInjury
		{
			get
			{
				return base.GetSystemBoolean(MedicalBenefitClaimItemMetadata.ColumnNames.IsOccupationalInjury);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalBenefitClaimItemMetadata.ColumnNames.IsOccupationalInjury, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaimItem.AmountOnBill
		/// </summary>
		virtual public System.Decimal? AmountOnBill
		{
			get
			{
				return base.GetSystemDecimal(MedicalBenefitClaimItemMetadata.ColumnNames.AmountOnBill);
			}
			
			set
			{
				base.SetSystemDecimal(MedicalBenefitClaimItemMetadata.ColumnNames.AmountOnBill, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaimItem.ReAmount
		/// </summary>
		virtual public System.Decimal? ReAmount
		{
			get
			{
				return base.GetSystemDecimal(MedicalBenefitClaimItemMetadata.ColumnNames.ReAmount);
			}
			
			set
			{
				base.SetSystemDecimal(MedicalBenefitClaimItemMetadata.ColumnNames.ReAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaimItem.ApprovedAmount
		/// </summary>
		virtual public System.Decimal? ApprovedAmount
		{
			get
			{
				return base.GetSystemDecimal(MedicalBenefitClaimItemMetadata.ColumnNames.ApprovedAmount);
			}
			
			set
			{
				base.SetSystemDecimal(MedicalBenefitClaimItemMetadata.ColumnNames.ApprovedAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaimItem.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(MedicalBenefitClaimItemMetadata.ColumnNames.Note);
			}
			
			set
			{
				base.SetSystemString(MedicalBenefitClaimItemMetadata.ColumnNames.Note, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaimItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalBenefitClaimItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MedicalBenefitClaimItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaimItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicalBenefitClaimItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(MedicalBenefitClaimItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMedicalBenefitClaimItem entity)
			{
				this.entity = entity;
			}
			
	
			public System.String MedicalBenefitClaimItemID
			{
				get
				{
					System.Int32? data = entity.MedicalBenefitClaimItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicalBenefitClaimItemID = null;
					else entity.MedicalBenefitClaimItemID = Convert.ToInt32(value);
				}
			}
				
			public System.String MedicalBenefitClaimID
			{
				get
				{
					System.Int32? data = entity.MedicalBenefitClaimID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicalBenefitClaimID = null;
					else entity.MedicalBenefitClaimID = Convert.ToInt32(value);
				}
			}
				
			public System.String TreatedID
			{
				get
				{
					System.Int32? data = entity.TreatedID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TreatedID = null;
					else entity.TreatedID = Convert.ToInt32(value);
				}
			}
				
			public System.String ReceiptNo
			{
				get
				{
					System.String data = entity.ReceiptNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceiptNo = null;
					else entity.ReceiptNo = Convert.ToString(value);
				}
			}
				
			public System.String TreatmentDate
			{
				get
				{
					System.DateTime? data = entity.TreatmentDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TreatmentDate = null;
					else entity.TreatmentDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String VisitTypeID
			{
				get
				{
					System.String data = entity.VisitTypeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VisitTypeID = null;
					else entity.VisitTypeID = Convert.ToString(value);
				}
			}
				
			public System.String PhysicianName
			{
				get
				{
					System.String data = entity.PhysicianName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhysicianName = null;
					else entity.PhysicianName = Convert.ToString(value);
				}
			}
				
			public System.String HospitalInfoID
			{
				get
				{
					System.Int32? data = entity.HospitalInfoID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HospitalInfoID = null;
					else entity.HospitalInfoID = Convert.ToInt32(value);
				}
			}
				
			public System.String IsOccupationalInjury
			{
				get
				{
					System.Boolean? data = entity.IsOccupationalInjury;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOccupationalInjury = null;
					else entity.IsOccupationalInjury = Convert.ToBoolean(value);
				}
			}
				
			public System.String AmountOnBill
			{
				get
				{
					System.Decimal? data = entity.AmountOnBill;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountOnBill = null;
					else entity.AmountOnBill = Convert.ToDecimal(value);
				}
			}
				
			public System.String ReAmount
			{
				get
				{
					System.Decimal? data = entity.ReAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReAmount = null;
					else entity.ReAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String ApprovedAmount
			{
				get
				{
					System.Decimal? data = entity.ApprovedAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedAmount = null;
					else entity.ApprovedAmount = Convert.ToDecimal(value);
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
			

			private esMedicalBenefitClaimItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicalBenefitClaimItemQuery query)
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
				throw new Exception("esMedicalBenefitClaimItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class MedicalBenefitClaimItem : esMedicalBenefitClaimItem
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
	abstract public class esMedicalBenefitClaimItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return MedicalBenefitClaimItemMetadata.Meta();
			}
		}	
		

		public esQueryItem MedicalBenefitClaimItemID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimItemMetadata.ColumnNames.MedicalBenefitClaimItemID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem MedicalBenefitClaimID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimItemMetadata.ColumnNames.MedicalBenefitClaimID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem TreatedID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimItemMetadata.ColumnNames.TreatedID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ReceiptNo
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimItemMetadata.ColumnNames.ReceiptNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TreatmentDate
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimItemMetadata.ColumnNames.TreatmentDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem VisitTypeID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimItemMetadata.ColumnNames.VisitTypeID, esSystemType.String);
			}
		} 
		
		public esQueryItem PhysicianName
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimItemMetadata.ColumnNames.PhysicianName, esSystemType.String);
			}
		} 
		
		public esQueryItem HospitalInfoID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimItemMetadata.ColumnNames.HospitalInfoID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IsOccupationalInjury
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimItemMetadata.ColumnNames.IsOccupationalInjury, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem AmountOnBill
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimItemMetadata.ColumnNames.AmountOnBill, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ReAmount
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimItemMetadata.ColumnNames.ReAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ApprovedAmount
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimItemMetadata.ColumnNames.ApprovedAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimItemMetadata.ColumnNames.Note, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicalBenefitClaimItemCollection")]
	public partial class MedicalBenefitClaimItemCollection : esMedicalBenefitClaimItemCollection, IEnumerable<MedicalBenefitClaimItem>
	{
		public MedicalBenefitClaimItemCollection()
		{

		}
		
		public static implicit operator List<MedicalBenefitClaimItem>(MedicalBenefitClaimItemCollection coll)
		{
			List<MedicalBenefitClaimItem> list = new List<MedicalBenefitClaimItem>();
			
			foreach (MedicalBenefitClaimItem emp in coll)
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
				return  MedicalBenefitClaimItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalBenefitClaimItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicalBenefitClaimItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicalBenefitClaimItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public MedicalBenefitClaimItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalBenefitClaimItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(MedicalBenefitClaimItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public MedicalBenefitClaimItem AddNew()
		{
			MedicalBenefitClaimItem entity = base.AddNewEntity() as MedicalBenefitClaimItem;
			
			return entity;
		}

		public MedicalBenefitClaimItem FindByPrimaryKey(System.Int32 medicalBenefitClaimItemID)
		{
			return base.FindByPrimaryKey(medicalBenefitClaimItemID) as MedicalBenefitClaimItem;
		}


		#region IEnumerable<MedicalBenefitClaimItem> Members

		IEnumerator<MedicalBenefitClaimItem> IEnumerable<MedicalBenefitClaimItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as MedicalBenefitClaimItem;
			}
		}

		#endregion
		
		private MedicalBenefitClaimItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicalBenefitClaimItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("MedicalBenefitClaimItem ({MedicalBenefitClaimItemID})")]
	[Serializable]
	public partial class MedicalBenefitClaimItem : esMedicalBenefitClaimItem
	{
		public MedicalBenefitClaimItem()
		{

		}
	
		public MedicalBenefitClaimItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicalBenefitClaimItemMetadata.Meta();
			}
		}
		
		
		
		override protected esMedicalBenefitClaimItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalBenefitClaimItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public MedicalBenefitClaimItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalBenefitClaimItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(MedicalBenefitClaimItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private MedicalBenefitClaimItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class MedicalBenefitClaimItemQuery : esMedicalBenefitClaimItemQuery
	{
		public MedicalBenefitClaimItemQuery()
		{

		}		
		
		public MedicalBenefitClaimItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "MedicalBenefitClaimItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class MedicalBenefitClaimItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicalBenefitClaimItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MedicalBenefitClaimItemMetadata.ColumnNames.MedicalBenefitClaimItemID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitClaimItemMetadata.PropertyNames.MedicalBenefitClaimItemID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimItemMetadata.ColumnNames.MedicalBenefitClaimID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitClaimItemMetadata.PropertyNames.MedicalBenefitClaimID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimItemMetadata.ColumnNames.TreatedID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitClaimItemMetadata.PropertyNames.TreatedID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimItemMetadata.ColumnNames.ReceiptNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalBenefitClaimItemMetadata.PropertyNames.ReceiptNo;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimItemMetadata.ColumnNames.TreatmentDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalBenefitClaimItemMetadata.PropertyNames.TreatmentDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimItemMetadata.ColumnNames.VisitTypeID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalBenefitClaimItemMetadata.PropertyNames.VisitTypeID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimItemMetadata.ColumnNames.PhysicianName, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalBenefitClaimItemMetadata.PropertyNames.PhysicianName;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimItemMetadata.ColumnNames.HospitalInfoID, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitClaimItemMetadata.PropertyNames.HospitalInfoID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimItemMetadata.ColumnNames.IsOccupationalInjury, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalBenefitClaimItemMetadata.PropertyNames.IsOccupationalInjury;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimItemMetadata.ColumnNames.AmountOnBill, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MedicalBenefitClaimItemMetadata.PropertyNames.AmountOnBill;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimItemMetadata.ColumnNames.ReAmount, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MedicalBenefitClaimItemMetadata.PropertyNames.ReAmount;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimItemMetadata.ColumnNames.ApprovedAmount, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MedicalBenefitClaimItemMetadata.PropertyNames.ApprovedAmount;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimItemMetadata.ColumnNames.Note, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalBenefitClaimItemMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimItemMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalBenefitClaimItemMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimItemMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalBenefitClaimItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public MedicalBenefitClaimItemMetadata Meta()
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
			 public const string MedicalBenefitClaimItemID = "MedicalBenefitClaimItemID";
			 public const string MedicalBenefitClaimID = "MedicalBenefitClaimID";
			 public const string TreatedID = "TreatedID";
			 public const string ReceiptNo = "ReceiptNo";
			 public const string TreatmentDate = "TreatmentDate";
			 public const string VisitTypeID = "VisitTypeID";
			 public const string PhysicianName = "PhysicianName";
			 public const string HospitalInfoID = "HospitalInfoID";
			 public const string IsOccupationalInjury = "IsOccupationalInjury";
			 public const string AmountOnBill = "AmountOnBill";
			 public const string ReAmount = "ReAmount";
			 public const string ApprovedAmount = "ApprovedAmount";
			 public const string Note = "Note";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string MedicalBenefitClaimItemID = "MedicalBenefitClaimItemID";
			 public const string MedicalBenefitClaimID = "MedicalBenefitClaimID";
			 public const string TreatedID = "TreatedID";
			 public const string ReceiptNo = "ReceiptNo";
			 public const string TreatmentDate = "TreatmentDate";
			 public const string VisitTypeID = "VisitTypeID";
			 public const string PhysicianName = "PhysicianName";
			 public const string HospitalInfoID = "HospitalInfoID";
			 public const string IsOccupationalInjury = "IsOccupationalInjury";
			 public const string AmountOnBill = "AmountOnBill";
			 public const string ReAmount = "ReAmount";
			 public const string ApprovedAmount = "ApprovedAmount";
			 public const string Note = "Note";
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
			lock (typeof(MedicalBenefitClaimItemMetadata))
			{
				if(MedicalBenefitClaimItemMetadata.mapDelegates == null)
				{
					MedicalBenefitClaimItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MedicalBenefitClaimItemMetadata.meta == null)
				{
					MedicalBenefitClaimItemMetadata.meta = new MedicalBenefitClaimItemMetadata();
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
				

				meta.AddTypeMap("MedicalBenefitClaimItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MedicalBenefitClaimID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TreatedID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ReceiptNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TreatmentDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VisitTypeID", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("PhysicianName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("HospitalInfoID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsOccupationalInjury", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AmountOnBill", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("ReAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("ApprovedAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "MedicalBenefitClaimItem";
				meta.Destination = "MedicalBenefitClaimItem";
				
				meta.spInsert = "proc_MedicalBenefitClaimItemInsert";				
				meta.spUpdate = "proc_MedicalBenefitClaimItemUpdate";		
				meta.spDelete = "proc_MedicalBenefitClaimItemDelete";
				meta.spLoadAll = "proc_MedicalBenefitClaimItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicalBenefitClaimItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicalBenefitClaimItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
