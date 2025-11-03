/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/27/2012 12:50:44 PM
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
	abstract public class esTransChargesHistoryCollection : esEntityCollectionWAuditLog
	{
		public esTransChargesHistoryCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TransChargesHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransChargesHistoryQuery query)
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
			this.InitQuery(query as esTransChargesHistoryQuery);
		}
		#endregion
		
		virtual public TransChargesHistory DetachEntity(TransChargesHistory entity)
		{
			return base.DetachEntity(entity) as TransChargesHistory;
		}
		
		virtual public TransChargesHistory AttachEntity(TransChargesHistory entity)
		{
			return base.AttachEntity(entity) as TransChargesHistory;
		}
		
		virtual public void Combine(TransChargesHistoryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransChargesHistory this[int index]
		{
			get
			{
				return base[index] as TransChargesHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransChargesHistory);
		}
	}



	[Serializable]
	abstract public class esTransChargesHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransChargesHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransChargesHistory()
		{

		}

		public esTransChargesHistory(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String recalculationProcessNo, System.String transactionNo, System.String registrationNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(recalculationProcessNo, transactionNo, registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(recalculationProcessNo, transactionNo, registrationNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String recalculationProcessNo, System.String transactionNo, System.String registrationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(recalculationProcessNo, transactionNo, registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(recalculationProcessNo, transactionNo, registrationNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String recalculationProcessNo, System.String transactionNo, System.String registrationNo)
		{
			esTransChargesHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.RecalculationProcessNo == recalculationProcessNo, query.TransactionNo == transactionNo, query.RegistrationNo == registrationNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String recalculationProcessNo, System.String transactionNo, System.String registrationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RecalculationProcessNo",recalculationProcessNo);			parms.Add("TransactionNo",transactionNo);			parms.Add("RegistrationNo",registrationNo);
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
		/// Maps to TransChargesHistory.RecalculationProcessNo
		/// </summary>
		virtual public System.String RecalculationProcessNo
		{
			get
			{
				return base.GetSystemString(TransChargesHistoryMetadata.ColumnNames.RecalculationProcessNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesHistoryMetadata.ColumnNames.RecalculationProcessNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(TransChargesHistoryMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesHistoryMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(TransChargesHistoryMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesHistoryMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(TransChargesHistoryMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(TransChargesHistoryMetadata.ColumnNames.TransactionDate, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.ExecutionDate
		/// </summary>
		virtual public System.DateTime? ExecutionDate
		{
			get
			{
				return base.GetSystemDateTime(TransChargesHistoryMetadata.ColumnNames.ExecutionDate);
			}
			
			set
			{
				base.SetSystemDateTime(TransChargesHistoryMetadata.ColumnNames.ExecutionDate, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(TransChargesHistoryMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesHistoryMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.FromServiceUnitID
		/// </summary>
		virtual public System.String FromServiceUnitID
		{
			get
			{
				return base.GetSystemString(TransChargesHistoryMetadata.ColumnNames.FromServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(TransChargesHistoryMetadata.ColumnNames.FromServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.ToServiceUnitID
		/// </summary>
		virtual public System.String ToServiceUnitID
		{
			get
			{
				return base.GetSystemString(TransChargesHistoryMetadata.ColumnNames.ToServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(TransChargesHistoryMetadata.ColumnNames.ToServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(TransChargesHistoryMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(TransChargesHistoryMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.RoomID
		/// </summary>
		virtual public System.String RoomID
		{
			get
			{
				return base.GetSystemString(TransChargesHistoryMetadata.ColumnNames.RoomID);
			}
			
			set
			{
				base.SetSystemString(TransChargesHistoryMetadata.ColumnNames.RoomID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.BedID
		/// </summary>
		virtual public System.String BedID
		{
			get
			{
				return base.GetSystemString(TransChargesHistoryMetadata.ColumnNames.BedID);
			}
			
			set
			{
				base.SetSystemString(TransChargesHistoryMetadata.ColumnNames.BedID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.DueDate
		/// </summary>
		virtual public System.DateTime? DueDate
		{
			get
			{
				return base.GetSystemDateTime(TransChargesHistoryMetadata.ColumnNames.DueDate);
			}
			
			set
			{
				base.SetSystemDateTime(TransChargesHistoryMetadata.ColumnNames.DueDate, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.SRShift
		/// </summary>
		virtual public System.String SRShift
		{
			get
			{
				return base.GetSystemString(TransChargesHistoryMetadata.ColumnNames.SRShift);
			}
			
			set
			{
				base.SetSystemString(TransChargesHistoryMetadata.ColumnNames.SRShift, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.SRItemType
		/// </summary>
		virtual public System.String SRItemType
		{
			get
			{
				return base.GetSystemString(TransChargesHistoryMetadata.ColumnNames.SRItemType);
			}
			
			set
			{
				base.SetSystemString(TransChargesHistoryMetadata.ColumnNames.SRItemType, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.IsProceed
		/// </summary>
		virtual public System.Boolean? IsProceed
		{
			get
			{
				return base.GetSystemBoolean(TransChargesHistoryMetadata.ColumnNames.IsProceed);
			}
			
			set
			{
				base.SetSystemBoolean(TransChargesHistoryMetadata.ColumnNames.IsProceed, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(TransChargesHistoryMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(TransChargesHistoryMetadata.ColumnNames.IsApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(TransChargesHistoryMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(TransChargesHistoryMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.IsOrder
		/// </summary>
		virtual public System.Boolean? IsOrder
		{
			get
			{
				return base.GetSystemBoolean(TransChargesHistoryMetadata.ColumnNames.IsOrder);
			}
			
			set
			{
				base.SetSystemBoolean(TransChargesHistoryMetadata.ColumnNames.IsOrder, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.IsCorrection
		/// </summary>
		virtual public System.Boolean? IsCorrection
		{
			get
			{
				return base.GetSystemBoolean(TransChargesHistoryMetadata.ColumnNames.IsCorrection);
			}
			
			set
			{
				base.SetSystemBoolean(TransChargesHistoryMetadata.ColumnNames.IsCorrection, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.IsClusterAssign
		/// </summary>
		virtual public System.Boolean? IsClusterAssign
		{
			get
			{
				return base.GetSystemBoolean(TransChargesHistoryMetadata.ColumnNames.IsClusterAssign);
			}
			
			set
			{
				base.SetSystemBoolean(TransChargesHistoryMetadata.ColumnNames.IsClusterAssign, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.IsAutoBillTransaction
		/// </summary>
		virtual public System.Boolean? IsAutoBillTransaction
		{
			get
			{
				return base.GetSystemBoolean(TransChargesHistoryMetadata.ColumnNames.IsAutoBillTransaction);
			}
			
			set
			{
				base.SetSystemBoolean(TransChargesHistoryMetadata.ColumnNames.IsAutoBillTransaction, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.IsBillProceed
		/// </summary>
		virtual public System.Boolean? IsBillProceed
		{
			get
			{
				return base.GetSystemBoolean(TransChargesHistoryMetadata.ColumnNames.IsBillProceed);
			}
			
			set
			{
				base.SetSystemBoolean(TransChargesHistoryMetadata.ColumnNames.IsBillProceed, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(TransChargesHistoryMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(TransChargesHistoryMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransChargesHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransChargesHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransChargesHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(TransChargesHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.SRTypeResult
		/// </summary>
		virtual public System.String SRTypeResult
		{
			get
			{
				return base.GetSystemString(TransChargesHistoryMetadata.ColumnNames.SRTypeResult);
			}
			
			set
			{
				base.SetSystemString(TransChargesHistoryMetadata.ColumnNames.SRTypeResult, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesHistory.ResponUnitID
		/// </summary>
		virtual public System.String ResponUnitID
		{
			get
			{
				return base.GetSystemString(TransChargesHistoryMetadata.ColumnNames.ResponUnitID);
			}
			
			set
			{
				base.SetSystemString(TransChargesHistoryMetadata.ColumnNames.ResponUnitID, value);
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
			public esStrings(esTransChargesHistory entity)
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
			

			private esTransChargesHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransChargesHistoryQuery query)
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
				throw new Exception("esTransChargesHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class TransChargesHistory : esTransChargesHistory
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
	abstract public class esTransChargesHistoryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TransChargesHistoryMetadata.Meta();
			}
		}	
		

		public esQueryItem RecalculationProcessNo
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.RecalculationProcessNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ExecutionDate
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.ExecutionDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem FromServiceUnitID
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.FromServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem ToServiceUnitID
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.ToServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem RoomID
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.RoomID, esSystemType.String);
			}
		} 
		
		public esQueryItem BedID
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.BedID, esSystemType.String);
			}
		} 
		
		public esQueryItem DueDate
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.DueDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem SRShift
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.SRShift, esSystemType.String);
			}
		} 
		
		public esQueryItem SRItemType
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.SRItemType, esSystemType.String);
			}
		} 
		
		public esQueryItem IsProceed
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.IsProceed, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsOrder
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.IsOrder, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsCorrection
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.IsCorrection, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsClusterAssign
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.IsClusterAssign, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsAutoBillTransaction
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.IsAutoBillTransaction, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsBillProceed
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.IsBillProceed, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRTypeResult
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.SRTypeResult, esSystemType.String);
			}
		} 
		
		public esQueryItem ResponUnitID
		{
			get
			{
				return new esQueryItem(this, TransChargesHistoryMetadata.ColumnNames.ResponUnitID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransChargesHistoryCollection")]
	public partial class TransChargesHistoryCollection : esTransChargesHistoryCollection, IEnumerable<TransChargesHistory>
	{
		public TransChargesHistoryCollection()
		{

		}
		
		public static implicit operator List<TransChargesHistory>(TransChargesHistoryCollection coll)
		{
			List<TransChargesHistory> list = new List<TransChargesHistory>();
			
			foreach (TransChargesHistory emp in coll)
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
				return  TransChargesHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransChargesHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransChargesHistory();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TransChargesHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TransChargesHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public TransChargesHistory AddNew()
		{
			TransChargesHistory entity = base.AddNewEntity() as TransChargesHistory;
			
			return entity;
		}

		public TransChargesHistory FindByPrimaryKey(System.String recalculationProcessNo, System.String transactionNo, System.String registrationNo)
		{
			return base.FindByPrimaryKey(recalculationProcessNo, transactionNo, registrationNo) as TransChargesHistory;
		}


		#region IEnumerable<TransChargesHistory> Members

		IEnumerator<TransChargesHistory> IEnumerable<TransChargesHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransChargesHistory;
			}
		}

		#endregion
		
		private TransChargesHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransChargesHistory' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("TransChargesHistory ({RecalculationProcessNo},{TransactionNo},{RegistrationNo})")]
	[Serializable]
	public partial class TransChargesHistory : esTransChargesHistory
	{
		public TransChargesHistory()
		{

		}
	
		public TransChargesHistory(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransChargesHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esTransChargesHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TransChargesHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TransChargesHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TransChargesHistoryQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TransChargesHistoryQuery : esTransChargesHistoryQuery
	{
		public TransChargesHistoryQuery()
		{

		}		
		
		public TransChargesHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TransChargesHistoryQuery";
        }
		
			
	}


	[Serializable]
	public partial class TransChargesHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransChargesHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.RecalculationProcessNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.RecalculationProcessNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.TransactionDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.TransactionDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.ExecutionDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.ExecutionDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.ReferenceNo, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.FromServiceUnitID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.FromServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.ToServiceUnitID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.ToServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.ClassID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.ClassID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.RoomID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.RoomID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.BedID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.BedID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.DueDate, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.DueDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.SRShift, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.SRShift;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.SRItemType, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.SRItemType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.IsProceed, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.IsProceed;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.IsApproved, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.IsApproved;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.IsVoid, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.IsVoid;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.IsOrder, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.IsOrder;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.IsCorrection, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.IsCorrection;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.IsClusterAssign, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.IsClusterAssign;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.IsAutoBillTransaction, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.IsAutoBillTransaction;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.IsBillProceed, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.IsBillProceed;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.Notes, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.LastUpdateDateTime, 23, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.LastUpdateByUserID, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.SRTypeResult, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.SRTypeResult;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesHistoryMetadata.ColumnNames.ResponUnitID, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesHistoryMetadata.PropertyNames.ResponUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TransChargesHistoryMetadata Meta()
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
			 public const string RecalculationProcessNo = "RecalculationProcessNo";
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
			lock (typeof(TransChargesHistoryMetadata))
			{
				if(TransChargesHistoryMetadata.mapDelegates == null)
				{
					TransChargesHistoryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransChargesHistoryMetadata.meta == null)
				{
					TransChargesHistoryMetadata.meta = new TransChargesHistoryMetadata();
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
				
				
				
				meta.Source = "TransChargesHistory";
				meta.Destination = "TransChargesHistory";
				
				meta.spInsert = "proc_TransChargesHistoryInsert";				
				meta.spUpdate = "proc_TransChargesHistoryUpdate";		
				meta.spDelete = "proc_TransChargesHistoryDelete";
				meta.spLoadAll = "proc_TransChargesHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransChargesHistoryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransChargesHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
