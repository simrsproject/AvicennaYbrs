/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/17/2012 5:18:16 PM
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
	abstract public class esVwTransChargesNoCorrectionCollection : esEntityCollectionWAuditLog
	{
		public esVwTransChargesNoCorrectionCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VwTransChargesNoCorrectionCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwTransChargesNoCorrectionQuery query)
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
			this.InitQuery(query as esVwTransChargesNoCorrectionQuery);
		}
		#endregion
		
		virtual public VwTransChargesNoCorrection DetachEntity(VwTransChargesNoCorrection entity)
		{
			return base.DetachEntity(entity) as VwTransChargesNoCorrection;
		}
		
		virtual public VwTransChargesNoCorrection AttachEntity(VwTransChargesNoCorrection entity)
		{
			return base.AttachEntity(entity) as VwTransChargesNoCorrection;
		}
		
		virtual public void Combine(VwTransChargesNoCorrectionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwTransChargesNoCorrection this[int index]
		{
			get
			{
				return base[index] as VwTransChargesNoCorrection;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwTransChargesNoCorrection);
		}
	}



	[Serializable]
	abstract public class esVwTransChargesNoCorrection : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwTransChargesNoCorrectionQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwTransChargesNoCorrection()
		{

		}

		public esVwTransChargesNoCorrection(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		
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
		/// Maps to vw_TransChargesNoCorrection.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(VwTransChargesNoCorrectionMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(VwTransChargesNoCorrectionMetadata.ColumnNames.TransactionDate, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.ExecutionDate
		/// </summary>
		virtual public System.DateTime? ExecutionDate
		{
			get
			{
				return base.GetSystemDateTime(VwTransChargesNoCorrectionMetadata.ColumnNames.ExecutionDate);
			}
			
			set
			{
				base.SetSystemDateTime(VwTransChargesNoCorrectionMetadata.ColumnNames.ExecutionDate, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.FromServiceUnitID
		/// </summary>
		virtual public System.String FromServiceUnitID
		{
			get
			{
				return base.GetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.FromServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.FromServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.ToServiceUnitID
		/// </summary>
		virtual public System.String ToServiceUnitID
		{
			get
			{
				return base.GetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.ToServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.ToServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.RoomID
		/// </summary>
		virtual public System.String RoomID
		{
			get
			{
				return base.GetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.RoomID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.RoomID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.BedID
		/// </summary>
		virtual public System.String BedID
		{
			get
			{
				return base.GetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.BedID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.BedID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.DueDate
		/// </summary>
		virtual public System.DateTime? DueDate
		{
			get
			{
				return base.GetSystemDateTime(VwTransChargesNoCorrectionMetadata.ColumnNames.DueDate);
			}
			
			set
			{
				base.SetSystemDateTime(VwTransChargesNoCorrectionMetadata.ColumnNames.DueDate, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.SRShift
		/// </summary>
		virtual public System.String SRShift
		{
			get
			{
				return base.GetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.SRShift);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.SRShift, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.SRItemType
		/// </summary>
		virtual public System.String SRItemType
		{
			get
			{
				return base.GetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.SRItemType);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.SRItemType, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.IsProceed
		/// </summary>
		virtual public System.Boolean? IsProceed
		{
			get
			{
				return base.GetSystemBoolean(VwTransChargesNoCorrectionMetadata.ColumnNames.IsProceed);
			}
			
			set
			{
				base.SetSystemBoolean(VwTransChargesNoCorrectionMetadata.ColumnNames.IsProceed, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(VwTransChargesNoCorrectionMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(VwTransChargesNoCorrectionMetadata.ColumnNames.IsApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(VwTransChargesNoCorrectionMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(VwTransChargesNoCorrectionMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.IsOrder
		/// </summary>
		virtual public System.Boolean? IsOrder
		{
			get
			{
				return base.GetSystemBoolean(VwTransChargesNoCorrectionMetadata.ColumnNames.IsOrder);
			}
			
			set
			{
				base.SetSystemBoolean(VwTransChargesNoCorrectionMetadata.ColumnNames.IsOrder, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.IsCorrection
		/// </summary>
		virtual public System.Boolean? IsCorrection
		{
			get
			{
				return base.GetSystemBoolean(VwTransChargesNoCorrectionMetadata.ColumnNames.IsCorrection);
			}
			
			set
			{
				base.SetSystemBoolean(VwTransChargesNoCorrectionMetadata.ColumnNames.IsCorrection, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.IsClusterAssign
		/// </summary>
		virtual public System.Boolean? IsClusterAssign
		{
			get
			{
				return base.GetSystemBoolean(VwTransChargesNoCorrectionMetadata.ColumnNames.IsClusterAssign);
			}
			
			set
			{
				base.SetSystemBoolean(VwTransChargesNoCorrectionMetadata.ColumnNames.IsClusterAssign, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.IsAutoBillTransaction
		/// </summary>
		virtual public System.Boolean? IsAutoBillTransaction
		{
			get
			{
				return base.GetSystemBoolean(VwTransChargesNoCorrectionMetadata.ColumnNames.IsAutoBillTransaction);
			}
			
			set
			{
				base.SetSystemBoolean(VwTransChargesNoCorrectionMetadata.ColumnNames.IsAutoBillTransaction, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.IsBillProceed
		/// </summary>
		virtual public System.Boolean? IsBillProceed
		{
			get
			{
				return base.GetSystemBoolean(VwTransChargesNoCorrectionMetadata.ColumnNames.IsBillProceed);
			}
			
			set
			{
				base.SetSystemBoolean(VwTransChargesNoCorrectionMetadata.ColumnNames.IsBillProceed, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(VwTransChargesNoCorrectionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(VwTransChargesNoCorrectionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.SRTypeResult
		/// </summary>
		virtual public System.String SRTypeResult
		{
			get
			{
				return base.GetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.SRTypeResult);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.SRTypeResult, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesNoCorrection.ResponUnitID
		/// </summary>
		virtual public System.String ResponUnitID
		{
			get
			{
				return base.GetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.ResponUnitID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesNoCorrectionMetadata.ColumnNames.ResponUnitID, value);
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
			public esStrings(esVwTransChargesNoCorrection entity)
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
			

			private esVwTransChargesNoCorrection entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwTransChargesNoCorrectionQuery query)
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
				throw new Exception("esVwTransChargesNoCorrection can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwTransChargesNoCorrectionQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwTransChargesNoCorrectionMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ExecutionDate
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.ExecutionDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem FromServiceUnitID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.FromServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem ToServiceUnitID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.ToServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem RoomID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.RoomID, esSystemType.String);
			}
		} 
		
		public esQueryItem BedID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.BedID, esSystemType.String);
			}
		} 
		
		public esQueryItem DueDate
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.DueDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem SRShift
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.SRShift, esSystemType.String);
			}
		} 
		
		public esQueryItem SRItemType
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.SRItemType, esSystemType.String);
			}
		} 
		
		public esQueryItem IsProceed
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.IsProceed, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsOrder
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.IsOrder, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsCorrection
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.IsCorrection, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsClusterAssign
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.IsClusterAssign, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsAutoBillTransaction
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.IsAutoBillTransaction, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsBillProceed
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.IsBillProceed, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRTypeResult
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.SRTypeResult, esSystemType.String);
			}
		} 
		
		public esQueryItem ResponUnitID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesNoCorrectionMetadata.ColumnNames.ResponUnitID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwTransChargesNoCorrectionCollection")]
	public partial class VwTransChargesNoCorrectionCollection : esVwTransChargesNoCorrectionCollection, IEnumerable<VwTransChargesNoCorrection>
	{
		public VwTransChargesNoCorrectionCollection()
		{

		}
		
		public static implicit operator List<VwTransChargesNoCorrection>(VwTransChargesNoCorrectionCollection coll)
		{
			List<VwTransChargesNoCorrection> list = new List<VwTransChargesNoCorrection>();
			
			foreach (VwTransChargesNoCorrection emp in coll)
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
				return  VwTransChargesNoCorrectionMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwTransChargesNoCorrectionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwTransChargesNoCorrection(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwTransChargesNoCorrection();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}	
		
		#endregion


		[BrowsableAttribute( false )]
		public VwTransChargesNoCorrectionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwTransChargesNoCorrectionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VwTransChargesNoCorrectionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VwTransChargesNoCorrection AddNew()
		{
			VwTransChargesNoCorrection entity = base.AddNewEntity() as VwTransChargesNoCorrection;
			
			return entity;
		}


		#region IEnumerable<VwTransChargesNoCorrection> Members

		IEnumerator<VwTransChargesNoCorrection> IEnumerable<VwTransChargesNoCorrection>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwTransChargesNoCorrection;
			}
		}

		#endregion
		
		private VwTransChargesNoCorrectionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vw_TransChargesNoCorrection' view
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VwTransChargesNoCorrection ()")]
	[Serializable]
	public partial class VwTransChargesNoCorrection : esVwTransChargesNoCorrection
	{
		public VwTransChargesNoCorrection()
		{

		}
	
		public VwTransChargesNoCorrection(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwTransChargesNoCorrectionMetadata.Meta();
			}
		}
		
		
		
		override protected esVwTransChargesNoCorrectionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwTransChargesNoCorrectionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VwTransChargesNoCorrectionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwTransChargesNoCorrectionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VwTransChargesNoCorrectionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VwTransChargesNoCorrectionQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VwTransChargesNoCorrectionQuery : esVwTransChargesNoCorrectionQuery
	{
		public VwTransChargesNoCorrectionQuery()
		{

		}		
		
		public VwTransChargesNoCorrectionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VwTransChargesNoCorrectionQuery";
        }
		
			
	}


	[Serializable]
	public partial class VwTransChargesNoCorrectionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwTransChargesNoCorrectionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.TransactionDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.TransactionDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.ExecutionDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.ExecutionDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.ReferenceNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.FromServiceUnitID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.FromServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.ToServiceUnitID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.ToServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.ClassID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.ClassID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.RoomID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.RoomID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.BedID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.BedID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.DueDate, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.DueDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.SRShift, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.SRShift;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.SRItemType, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.SRItemType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.IsProceed, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.IsProceed;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.IsApproved, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.IsApproved;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.IsVoid, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.IsVoid;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.IsOrder, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.IsOrder;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.IsCorrection, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.IsCorrection;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.IsClusterAssign, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.IsClusterAssign;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.IsAutoBillTransaction, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.IsAutoBillTransaction;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.IsBillProceed, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.IsBillProceed;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.Notes, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.LastUpdateDateTime, 22, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.LastUpdateByUserID, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.SRTypeResult, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.SRTypeResult;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesNoCorrectionMetadata.ColumnNames.ResponUnitID, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesNoCorrectionMetadata.PropertyNames.ResponUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VwTransChargesNoCorrectionMetadata Meta()
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
			lock (typeof(VwTransChargesNoCorrectionMetadata))
			{
				if(VwTransChargesNoCorrectionMetadata.mapDelegates == null)
				{
					VwTransChargesNoCorrectionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwTransChargesNoCorrectionMetadata.meta == null)
				{
					VwTransChargesNoCorrectionMetadata.meta = new VwTransChargesNoCorrectionMetadata();
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
				
				
				
				meta.Source = "vw_TransChargesNoCorrection";
				meta.Destination = "vw_TransChargesNoCorrection";
				
				meta.spInsert = "proc_vw_TransChargesNoCorrectionInsert";				
				meta.spUpdate = "proc_vw_TransChargesNoCorrectionUpdate";		
				meta.spDelete = "proc_vw_TransChargesNoCorrectionDelete";
				meta.spLoadAll = "proc_vw_TransChargesNoCorrectionLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_TransChargesNoCorrectionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwTransChargesNoCorrectionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
