/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:27 PM
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
	abstract public class esTransPrescriptionItemUnitDoseCollection : esEntityCollectionWAuditLog
	{
		public esTransPrescriptionItemUnitDoseCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TransPrescriptionItemUnitDoseCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransPrescriptionItemUnitDoseQuery query)
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
			this.InitQuery(query as esTransPrescriptionItemUnitDoseQuery);
		}
		#endregion
		
		virtual public TransPrescriptionItemUnitDose DetachEntity(TransPrescriptionItemUnitDose entity)
		{
			return base.DetachEntity(entity) as TransPrescriptionItemUnitDose;
		}
		
		virtual public TransPrescriptionItemUnitDose AttachEntity(TransPrescriptionItemUnitDose entity)
		{
			return base.AttachEntity(entity) as TransPrescriptionItemUnitDose;
		}
		
		virtual public void Combine(TransPrescriptionItemUnitDoseCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransPrescriptionItemUnitDose this[int index]
		{
			get
			{
				return base[index] as TransPrescriptionItemUnitDose;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransPrescriptionItemUnitDose);
		}
	}



	[Serializable]
	abstract public class esTransPrescriptionItemUnitDose : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransPrescriptionItemUnitDoseQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransPrescriptionItemUnitDose()
		{

		}

		public esTransPrescriptionItemUnitDose(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String prescriptionNo, System.String sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(prescriptionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(prescriptionNo, sequenceNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String prescriptionNo, System.String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(prescriptionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(prescriptionNo, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String prescriptionNo, System.String sequenceNo)
		{
			esTransPrescriptionItemUnitDoseQuery query = this.GetDynamicQuery();
			query.Where(query.PrescriptionNo == prescriptionNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String prescriptionNo, System.String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("PrescriptionNo",prescriptionNo);			parms.Add("SequenceNo",sequenceNo);
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
						case "PrescriptionNo": this.str.PrescriptionNo = (string)value; break;							
						case "SequenceNo": this.str.SequenceNo = (string)value; break;							
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;							
						case "ReferenceSequenceNo": this.str.ReferenceSequenceNo = (string)value; break;							
						case "ReleaseDate": this.str.ReleaseDate = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "ResultQty": this.str.ResultQty = (string)value; break;							
						case "Price": this.str.Price = (string)value; break;							
						case "DiscountAmount": this.str.DiscountAmount = (string)value; break;							
						case "EmbalaceAmount": this.str.EmbalaceAmount = (string)value; break;							
						case "SweetenerAmount": this.str.SweetenerAmount = (string)value; break;							
						case "LineAmount": this.str.LineAmount = (string)value; break;							
						case "IsApprove": this.str.IsApprove = (string)value; break;							
						case "IsVoid": this.str.IsVoid = (string)value; break;							
						case "IsBillProceed": this.str.IsBillProceed = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ReleaseDate":
						
							if (value == null || value is System.DateTime)
								this.ReleaseDate = (System.DateTime?)value;
							break;
						
						case "ResultQty":
						
							if (value == null || value is System.Decimal)
								this.ResultQty = (System.Decimal?)value;
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
						
						case "SweetenerAmount":
						
							if (value == null || value is System.Decimal)
								this.SweetenerAmount = (System.Decimal?)value;
							break;
						
						case "LineAmount":
						
							if (value == null || value is System.Decimal)
								this.LineAmount = (System.Decimal?)value;
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
		/// Maps to TransPrescriptionItemUnitDose.PrescriptionNo
		/// </summary>
		virtual public System.String PrescriptionNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemUnitDoseMetadata.ColumnNames.PrescriptionNo);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemUnitDoseMetadata.ColumnNames.PrescriptionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemUnitDose.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemUnitDoseMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemUnitDoseMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemUnitDose.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemUnitDoseMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemUnitDoseMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemUnitDose.ReferenceSequenceNo
		/// </summary>
		virtual public System.String ReferenceSequenceNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemUnitDoseMetadata.ColumnNames.ReferenceSequenceNo);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemUnitDoseMetadata.ColumnNames.ReferenceSequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemUnitDose.ReleaseDate
		/// </summary>
		virtual public System.DateTime? ReleaseDate
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionItemUnitDoseMetadata.ColumnNames.ReleaseDate);
			}
			
			set
			{
				base.SetSystemDateTime(TransPrescriptionItemUnitDoseMetadata.ColumnNames.ReleaseDate, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemUnitDose.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemUnitDoseMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemUnitDoseMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemUnitDose.ResultQty
		/// </summary>
		virtual public System.Decimal? ResultQty
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemUnitDoseMetadata.ColumnNames.ResultQty);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemUnitDoseMetadata.ColumnNames.ResultQty, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemUnitDose.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemUnitDoseMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemUnitDoseMetadata.ColumnNames.Price, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemUnitDose.DiscountAmount
		/// </summary>
		virtual public System.Decimal? DiscountAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemUnitDoseMetadata.ColumnNames.DiscountAmount);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemUnitDoseMetadata.ColumnNames.DiscountAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemUnitDose.EmbalaceAmount
		/// </summary>
		virtual public System.Decimal? EmbalaceAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemUnitDoseMetadata.ColumnNames.EmbalaceAmount);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemUnitDoseMetadata.ColumnNames.EmbalaceAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemUnitDose.SweetenerAmount
		/// </summary>
		virtual public System.Decimal? SweetenerAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemUnitDoseMetadata.ColumnNames.SweetenerAmount);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemUnitDoseMetadata.ColumnNames.SweetenerAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemUnitDose.LineAmount
		/// </summary>
		virtual public System.Decimal? LineAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPrescriptionItemUnitDoseMetadata.ColumnNames.LineAmount);
			}
			
			set
			{
				base.SetSystemDecimal(TransPrescriptionItemUnitDoseMetadata.ColumnNames.LineAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemUnitDose.IsApprove
		/// </summary>
		virtual public System.Boolean? IsApprove
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemUnitDoseMetadata.ColumnNames.IsApprove);
			}
			
			set
			{
				base.SetSystemBoolean(TransPrescriptionItemUnitDoseMetadata.ColumnNames.IsApprove, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemUnitDose.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemUnitDoseMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(TransPrescriptionItemUnitDoseMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemUnitDose.IsBillProceed
		/// </summary>
		virtual public System.Boolean? IsBillProceed
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionItemUnitDoseMetadata.ColumnNames.IsBillProceed);
			}
			
			set
			{
				base.SetSystemBoolean(TransPrescriptionItemUnitDoseMetadata.ColumnNames.IsBillProceed, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemUnitDose.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionItemUnitDoseMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransPrescriptionItemUnitDoseMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionItemUnitDose.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionItemUnitDoseMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionItemUnitDoseMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esTransPrescriptionItemUnitDose entity)
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
				
			public System.String ReleaseDate
			{
				get
				{
					System.DateTime? data = entity.ReleaseDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReleaseDate = null;
					else entity.ReleaseDate = Convert.ToDateTime(value);
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
			

			private esTransPrescriptionItemUnitDose entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransPrescriptionItemUnitDoseQuery query)
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
				throw new Exception("esTransPrescriptionItemUnitDose can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class TransPrescriptionItemUnitDose : esTransPrescriptionItemUnitDose
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
	abstract public class esTransPrescriptionItemUnitDoseQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TransPrescriptionItemUnitDoseMetadata.Meta();
			}
		}	
		

		public esQueryItem PrescriptionNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemUnitDoseMetadata.ColumnNames.PrescriptionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemUnitDoseMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemUnitDoseMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ReferenceSequenceNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemUnitDoseMetadata.ColumnNames.ReferenceSequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ReleaseDate
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemUnitDoseMetadata.ColumnNames.ReleaseDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemUnitDoseMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem ResultQty
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemUnitDoseMetadata.ColumnNames.ResultQty, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemUnitDoseMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DiscountAmount
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemUnitDoseMetadata.ColumnNames.DiscountAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem EmbalaceAmount
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemUnitDoseMetadata.ColumnNames.EmbalaceAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem SweetenerAmount
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemUnitDoseMetadata.ColumnNames.SweetenerAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LineAmount
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemUnitDoseMetadata.ColumnNames.LineAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsApprove
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemUnitDoseMetadata.ColumnNames.IsApprove, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemUnitDoseMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsBillProceed
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemUnitDoseMetadata.ColumnNames.IsBillProceed, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemUnitDoseMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionItemUnitDoseMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransPrescriptionItemUnitDoseCollection")]
	public partial class TransPrescriptionItemUnitDoseCollection : esTransPrescriptionItemUnitDoseCollection, IEnumerable<TransPrescriptionItemUnitDose>
	{
		public TransPrescriptionItemUnitDoseCollection()
		{

		}
		
		public static implicit operator List<TransPrescriptionItemUnitDose>(TransPrescriptionItemUnitDoseCollection coll)
		{
			List<TransPrescriptionItemUnitDose> list = new List<TransPrescriptionItemUnitDose>();
			
			foreach (TransPrescriptionItemUnitDose emp in coll)
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
				return  TransPrescriptionItemUnitDoseMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPrescriptionItemUnitDoseQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransPrescriptionItemUnitDose(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransPrescriptionItemUnitDose();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TransPrescriptionItemUnitDoseQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPrescriptionItemUnitDoseQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TransPrescriptionItemUnitDoseQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public TransPrescriptionItemUnitDose AddNew()
		{
			TransPrescriptionItemUnitDose entity = base.AddNewEntity() as TransPrescriptionItemUnitDose;
			
			return entity;
		}

		public TransPrescriptionItemUnitDose FindByPrimaryKey(System.String prescriptionNo, System.String sequenceNo)
		{
			return base.FindByPrimaryKey(prescriptionNo, sequenceNo) as TransPrescriptionItemUnitDose;
		}


		#region IEnumerable<TransPrescriptionItemUnitDose> Members

		IEnumerator<TransPrescriptionItemUnitDose> IEnumerable<TransPrescriptionItemUnitDose>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransPrescriptionItemUnitDose;
			}
		}

		#endregion
		
		private TransPrescriptionItemUnitDoseQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransPrescriptionItemUnitDose' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("TransPrescriptionItemUnitDose ({PrescriptionNo},{SequenceNo})")]
	[Serializable]
	public partial class TransPrescriptionItemUnitDose : esTransPrescriptionItemUnitDose
	{
		public TransPrescriptionItemUnitDose()
		{

		}
	
		public TransPrescriptionItemUnitDose(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransPrescriptionItemUnitDoseMetadata.Meta();
			}
		}
		
		
		
		override protected esTransPrescriptionItemUnitDoseQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPrescriptionItemUnitDoseQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TransPrescriptionItemUnitDoseQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPrescriptionItemUnitDoseQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TransPrescriptionItemUnitDoseQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TransPrescriptionItemUnitDoseQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TransPrescriptionItemUnitDoseQuery : esTransPrescriptionItemUnitDoseQuery
	{
		public TransPrescriptionItemUnitDoseQuery()
		{

		}		
		
		public TransPrescriptionItemUnitDoseQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TransPrescriptionItemUnitDoseQuery";
        }
		
			
	}


	[Serializable]
	public partial class TransPrescriptionItemUnitDoseMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransPrescriptionItemUnitDoseMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransPrescriptionItemUnitDoseMetadata.ColumnNames.PrescriptionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemUnitDoseMetadata.PropertyNames.PrescriptionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemUnitDoseMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemUnitDoseMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemUnitDoseMetadata.ColumnNames.ReferenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemUnitDoseMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemUnitDoseMetadata.ColumnNames.ReferenceSequenceNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemUnitDoseMetadata.PropertyNames.ReferenceSequenceNo;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemUnitDoseMetadata.ColumnNames.ReleaseDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionItemUnitDoseMetadata.PropertyNames.ReleaseDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemUnitDoseMetadata.ColumnNames.ItemID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemUnitDoseMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemUnitDoseMetadata.ColumnNames.ResultQty, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemUnitDoseMetadata.PropertyNames.ResultQty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemUnitDoseMetadata.ColumnNames.Price, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemUnitDoseMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemUnitDoseMetadata.ColumnNames.DiscountAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemUnitDoseMetadata.PropertyNames.DiscountAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemUnitDoseMetadata.ColumnNames.EmbalaceAmount, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemUnitDoseMetadata.PropertyNames.EmbalaceAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemUnitDoseMetadata.ColumnNames.SweetenerAmount, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemUnitDoseMetadata.PropertyNames.SweetenerAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemUnitDoseMetadata.ColumnNames.LineAmount, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPrescriptionItemUnitDoseMetadata.PropertyNames.LineAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemUnitDoseMetadata.ColumnNames.IsApprove, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemUnitDoseMetadata.PropertyNames.IsApprove;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemUnitDoseMetadata.ColumnNames.IsVoid, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemUnitDoseMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemUnitDoseMetadata.ColumnNames.IsBillProceed, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionItemUnitDoseMetadata.PropertyNames.IsBillProceed;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemUnitDoseMetadata.ColumnNames.LastUpdateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionItemUnitDoseMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionItemUnitDoseMetadata.ColumnNames.LastUpdateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionItemUnitDoseMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TransPrescriptionItemUnitDoseMetadata Meta()
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
			 public const string PrescriptionNo = "PrescriptionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string ReferenceNo = "ReferenceNo";
			 public const string ReferenceSequenceNo = "ReferenceSequenceNo";
			 public const string ReleaseDate = "ReleaseDate";
			 public const string ItemID = "ItemID";
			 public const string ResultQty = "ResultQty";
			 public const string Price = "Price";
			 public const string DiscountAmount = "DiscountAmount";
			 public const string EmbalaceAmount = "EmbalaceAmount";
			 public const string SweetenerAmount = "SweetenerAmount";
			 public const string LineAmount = "LineAmount";
			 public const string IsApprove = "IsApprove";
			 public const string IsVoid = "IsVoid";
			 public const string IsBillProceed = "IsBillProceed";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PrescriptionNo = "PrescriptionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string ReferenceNo = "ReferenceNo";
			 public const string ReferenceSequenceNo = "ReferenceSequenceNo";
			 public const string ReleaseDate = "ReleaseDate";
			 public const string ItemID = "ItemID";
			 public const string ResultQty = "ResultQty";
			 public const string Price = "Price";
			 public const string DiscountAmount = "DiscountAmount";
			 public const string EmbalaceAmount = "EmbalaceAmount";
			 public const string SweetenerAmount = "SweetenerAmount";
			 public const string LineAmount = "LineAmount";
			 public const string IsApprove = "IsApprove";
			 public const string IsVoid = "IsVoid";
			 public const string IsBillProceed = "IsBillProceed";
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
			lock (typeof(TransPrescriptionItemUnitDoseMetadata))
			{
				if(TransPrescriptionItemUnitDoseMetadata.mapDelegates == null)
				{
					TransPrescriptionItemUnitDoseMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransPrescriptionItemUnitDoseMetadata.meta == null)
				{
					TransPrescriptionItemUnitDoseMetadata.meta = new TransPrescriptionItemUnitDoseMetadata();
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
				

				meta.AddTypeMap("PrescriptionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceSequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReleaseDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResultQty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DiscountAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("EmbalaceAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SweetenerAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LineAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsApprove", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBillProceed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "TransPrescriptionItemUnitDose";
				meta.Destination = "TransPrescriptionItemUnitDose";
				
				meta.spInsert = "proc_TransPrescriptionItemUnitDoseInsert";				
				meta.spUpdate = "proc_TransPrescriptionItemUnitDoseUpdate";		
				meta.spDelete = "proc_TransPrescriptionItemUnitDoseDelete";
				meta.spLoadAll = "proc_TransPrescriptionItemUnitDoseLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransPrescriptionItemUnitDoseLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransPrescriptionItemUnitDoseMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
