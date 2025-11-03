/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/6/2023 10:07:54 PM
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
	abstract public class esMedicationReceiveUsedCollection : esEntityCollectionWAuditLog
	{
		public esMedicationReceiveUsedCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "MedicationReceiveUsedCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esMedicationReceiveUsedQuery query)
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
			this.InitQuery(query as esMedicationReceiveUsedQuery);
		}
		#endregion
			
		virtual public MedicationReceiveUsed DetachEntity(MedicationReceiveUsed entity)
		{
			return base.DetachEntity(entity) as MedicationReceiveUsed;
		}
		
		virtual public MedicationReceiveUsed AttachEntity(MedicationReceiveUsed entity)
		{
			return base.AttachEntity(entity) as MedicationReceiveUsed;
		}
		
		virtual public void Combine(MedicationReceiveUsedCollection collection)
		{
			base.Combine(collection);
		}
		
		new public MedicationReceiveUsed this[int index]
		{
			get
			{
				return base[index] as MedicationReceiveUsed;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicationReceiveUsed);
		}
	}

	[Serializable]
	abstract public class esMedicationReceiveUsed : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicationReceiveUsedQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esMedicationReceiveUsed()
		{
		}
	
		public esMedicationReceiveUsed(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 medicationReceiveNo, Int32 sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicationReceiveNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(medicationReceiveNo, sequenceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 medicationReceiveNo, Int32 sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicationReceiveNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(medicationReceiveNo, sequenceNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int64 medicationReceiveNo, Int32 sequenceNo)
		{
			esMedicationReceiveUsedQuery query = this.GetDynamicQuery();
			query.Where(query.MedicationReceiveNo == medicationReceiveNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int64 medicationReceiveNo, Int32 sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("MedicationReceiveNo",medicationReceiveNo);
			parms.Add("SequenceNo",sequenceNo);
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
						case "MedicationReceiveNo": this.str.MedicationReceiveNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "ScheduleDateTime": this.str.ScheduleDateTime = (string)value; break;
						case "SetupDateTime": this.str.SetupDateTime = (string)value; break;
						case "SetupByUserID": this.str.SetupByUserID = (string)value; break;
						case "VerificationDateTime": this.str.VerificationDateTime = (string)value; break;
						case "VerificationByUserID": this.str.VerificationByUserID = (string)value; break;
						case "RealizedDateTime": this.str.RealizedDateTime = (string)value; break;
						case "RealizedByUserID": this.str.RealizedByUserID = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "SRMedicationReason": this.str.SRMedicationReason = (string)value; break;
						case "IsNotConsume": this.str.IsNotConsume = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsReSchedule": this.str.IsReSchedule = (string)value; break;
						case "IsVoidSchedule": this.str.IsVoidSchedule = (string)value; break;
						case "IsAdditionalSchedule": this.str.IsAdditionalSchedule = (string)value; break;
						case "PatientSignID": this.str.PatientSignID = (string)value; break;
						case "HandoversDateTime": this.str.HandoversDateTime = (string)value; break;
						case "HandoversByUserID": this.str.HandoversByUserID = (string)value; break;
						case "HandoversToUserID": this.str.HandoversToUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "MedicationReceiveNo":
						
							if (value == null || value is System.Int64)
								this.MedicationReceiveNo = (System.Int64?)value;
							break;
						case "SequenceNo":
						
							if (value == null || value is System.Int32)
								this.SequenceNo = (System.Int32?)value;
							break;
						case "ScheduleDateTime":
						
							if (value == null || value is System.DateTime)
								this.ScheduleDateTime = (System.DateTime?)value;
							break;
						case "SetupDateTime":
						
							if (value == null || value is System.DateTime)
								this.SetupDateTime = (System.DateTime?)value;
							break;
						case "VerificationDateTime":
						
							if (value == null || value is System.DateTime)
								this.VerificationDateTime = (System.DateTime?)value;
							break;
						case "RealizedDateTime":
						
							if (value == null || value is System.DateTime)
								this.RealizedDateTime = (System.DateTime?)value;
							break;
						case "Qty":
						
							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
							break;
						case "IsNotConsume":
						
							if (value == null || value is System.Boolean)
								this.IsNotConsume = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsReSchedule":
						
							if (value == null || value is System.Boolean)
								this.IsReSchedule = (System.Boolean?)value;
							break;
						case "IsVoidSchedule":
						
							if (value == null || value is System.Boolean)
								this.IsVoidSchedule = (System.Boolean?)value;
							break;
						case "IsAdditionalSchedule":
						
							if (value == null || value is System.Boolean)
								this.IsAdditionalSchedule = (System.Boolean?)value;
							break;
						case "PatientSignID":
						
							if (value == null || value is System.Int64)
								this.PatientSignID = (System.Int64?)value;
							break;
						case "HandoversDateTime":
						
							if (value == null || value is System.DateTime)
								this.HandoversDateTime = (System.DateTime?)value;
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
		/// Maps to MedicationReceiveUsed.MedicationReceiveNo
		/// </summary>
		virtual public System.Int64? MedicationReceiveNo
		{
			get
			{
				return base.GetSystemInt64(MedicationReceiveUsedMetadata.ColumnNames.MedicationReceiveNo);
			}
			
			set
			{
				base.SetSystemInt64(MedicationReceiveUsedMetadata.ColumnNames.MedicationReceiveNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.SequenceNo
		/// </summary>
		virtual public System.Int32? SequenceNo
		{
			get
			{
				return base.GetSystemInt32(MedicationReceiveUsedMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemInt32(MedicationReceiveUsedMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.ScheduleDateTime
		/// </summary>
		virtual public System.DateTime? ScheduleDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicationReceiveUsedMetadata.ColumnNames.ScheduleDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MedicationReceiveUsedMetadata.ColumnNames.ScheduleDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.SetupDateTime
		/// </summary>
		virtual public System.DateTime? SetupDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicationReceiveUsedMetadata.ColumnNames.SetupDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MedicationReceiveUsedMetadata.ColumnNames.SetupDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.SetupByUserID
		/// </summary>
		virtual public System.String SetupByUserID
		{
			get
			{
				return base.GetSystemString(MedicationReceiveUsedMetadata.ColumnNames.SetupByUserID);
			}
			
			set
			{
				base.SetSystemString(MedicationReceiveUsedMetadata.ColumnNames.SetupByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.VerificationDateTime
		/// </summary>
		virtual public System.DateTime? VerificationDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicationReceiveUsedMetadata.ColumnNames.VerificationDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MedicationReceiveUsedMetadata.ColumnNames.VerificationDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.VerificationByUserID
		/// </summary>
		virtual public System.String VerificationByUserID
		{
			get
			{
				return base.GetSystemString(MedicationReceiveUsedMetadata.ColumnNames.VerificationByUserID);
			}
			
			set
			{
				base.SetSystemString(MedicationReceiveUsedMetadata.ColumnNames.VerificationByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.RealizedDateTime
		/// </summary>
		virtual public System.DateTime? RealizedDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicationReceiveUsedMetadata.ColumnNames.RealizedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MedicationReceiveUsedMetadata.ColumnNames.RealizedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.RealizedByUserID
		/// </summary>
		virtual public System.String RealizedByUserID
		{
			get
			{
				return base.GetSystemString(MedicationReceiveUsedMetadata.ColumnNames.RealizedByUserID);
			}
			
			set
			{
				base.SetSystemString(MedicationReceiveUsedMetadata.ColumnNames.RealizedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(MedicationReceiveUsedMetadata.ColumnNames.Qty);
			}
			
			set
			{
				base.SetSystemDecimal(MedicationReceiveUsedMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(MedicationReceiveUsedMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(MedicationReceiveUsedMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.SRMedicationReason
		/// </summary>
		virtual public System.String SRMedicationReason
		{
			get
			{
				return base.GetSystemString(MedicationReceiveUsedMetadata.ColumnNames.SRMedicationReason);
			}
			
			set
			{
				base.SetSystemString(MedicationReceiveUsedMetadata.ColumnNames.SRMedicationReason, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.IsNotConsume
		/// </summary>
		virtual public System.Boolean? IsNotConsume
		{
			get
			{
				return base.GetSystemBoolean(MedicationReceiveUsedMetadata.ColumnNames.IsNotConsume);
			}
			
			set
			{
				base.SetSystemBoolean(MedicationReceiveUsedMetadata.ColumnNames.IsNotConsume, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(MedicationReceiveUsedMetadata.ColumnNames.Note);
			}
			
			set
			{
				base.SetSystemString(MedicationReceiveUsedMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicationReceiveUsedMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MedicationReceiveUsedMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicationReceiveUsedMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(MedicationReceiveUsedMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.IsReSchedule
		/// </summary>
		virtual public System.Boolean? IsReSchedule
		{
			get
			{
				return base.GetSystemBoolean(MedicationReceiveUsedMetadata.ColumnNames.IsReSchedule);
			}
			
			set
			{
				base.SetSystemBoolean(MedicationReceiveUsedMetadata.ColumnNames.IsReSchedule, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.IsVoidSchedule
		/// </summary>
		virtual public System.Boolean? IsVoidSchedule
		{
			get
			{
				return base.GetSystemBoolean(MedicationReceiveUsedMetadata.ColumnNames.IsVoidSchedule);
			}
			
			set
			{
				base.SetSystemBoolean(MedicationReceiveUsedMetadata.ColumnNames.IsVoidSchedule, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.IsAdditionalSchedule
		/// </summary>
		virtual public System.Boolean? IsAdditionalSchedule
		{
			get
			{
				return base.GetSystemBoolean(MedicationReceiveUsedMetadata.ColumnNames.IsAdditionalSchedule);
			}
			
			set
			{
				base.SetSystemBoolean(MedicationReceiveUsedMetadata.ColumnNames.IsAdditionalSchedule, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.PatientSignID
		/// </summary>
		virtual public System.Int64? PatientSignID
		{
			get
			{
				return base.GetSystemInt64(MedicationReceiveUsedMetadata.ColumnNames.PatientSignID);
			}
			
			set
			{
				base.SetSystemInt64(MedicationReceiveUsedMetadata.ColumnNames.PatientSignID, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.HandoversDateTime
		/// </summary>
		virtual public System.DateTime? HandoversDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicationReceiveUsedMetadata.ColumnNames.HandoversDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MedicationReceiveUsedMetadata.ColumnNames.HandoversDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.HandoversByUserID
		/// </summary>
		virtual public System.String HandoversByUserID
		{
			get
			{
				return base.GetSystemString(MedicationReceiveUsedMetadata.ColumnNames.HandoversByUserID);
			}
			
			set
			{
				base.SetSystemString(MedicationReceiveUsedMetadata.ColumnNames.HandoversByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveUsed.HandoversToUserID
		/// </summary>
		virtual public System.String HandoversToUserID
		{
			get
			{
				return base.GetSystemString(MedicationReceiveUsedMetadata.ColumnNames.HandoversToUserID);
			}
			
			set
			{
				base.SetSystemString(MedicationReceiveUsedMetadata.ColumnNames.HandoversToUserID, value);
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
			public esStrings(esMedicationReceiveUsed entity)
			{
				this.entity = entity;
			}
			public System.String MedicationReceiveNo
			{
				get
				{
					System.Int64? data = entity.MedicationReceiveNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicationReceiveNo = null;
					else entity.MedicationReceiveNo = Convert.ToInt64(value);
				}
			}
			public System.String SequenceNo
			{
				get
				{
					System.Int32? data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToInt32(value);
				}
			}
			public System.String ScheduleDateTime
			{
				get
				{
					System.DateTime? data = entity.ScheduleDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScheduleDateTime = null;
					else entity.ScheduleDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String SetupDateTime
			{
				get
				{
					System.DateTime? data = entity.SetupDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SetupDateTime = null;
					else entity.SetupDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String SetupByUserID
			{
				get
				{
					System.String data = entity.SetupByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SetupByUserID = null;
					else entity.SetupByUserID = Convert.ToString(value);
				}
			}
			public System.String VerificationDateTime
			{
				get
				{
					System.DateTime? data = entity.VerificationDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationDateTime = null;
					else entity.VerificationDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VerificationByUserID
			{
				get
				{
					System.String data = entity.VerificationByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationByUserID = null;
					else entity.VerificationByUserID = Convert.ToString(value);
				}
			}
			public System.String RealizedDateTime
			{
				get
				{
					System.DateTime? data = entity.RealizedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizedDateTime = null;
					else entity.RealizedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String RealizedByUserID
			{
				get
				{
					System.String data = entity.RealizedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizedByUserID = null;
					else entity.RealizedByUserID = Convert.ToString(value);
				}
			}
			public System.String Qty
			{
				get
				{
					System.Decimal? data = entity.Qty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Qty = null;
					else entity.Qty = Convert.ToDecimal(value);
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
			public System.String SRMedicationReason
			{
				get
				{
					System.String data = entity.SRMedicationReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMedicationReason = null;
					else entity.SRMedicationReason = Convert.ToString(value);
				}
			}
			public System.String IsNotConsume
			{
				get
				{
					System.Boolean? data = entity.IsNotConsume;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNotConsume = null;
					else entity.IsNotConsume = Convert.ToBoolean(value);
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
			public System.String IsReSchedule
			{
				get
				{
					System.Boolean? data = entity.IsReSchedule;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReSchedule = null;
					else entity.IsReSchedule = Convert.ToBoolean(value);
				}
			}
			public System.String IsVoidSchedule
			{
				get
				{
					System.Boolean? data = entity.IsVoidSchedule;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoidSchedule = null;
					else entity.IsVoidSchedule = Convert.ToBoolean(value);
				}
			}
			public System.String IsAdditionalSchedule
			{
				get
				{
					System.Boolean? data = entity.IsAdditionalSchedule;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAdditionalSchedule = null;
					else entity.IsAdditionalSchedule = Convert.ToBoolean(value);
				}
			}
			public System.String PatientSignID
			{
				get
				{
					System.Int64? data = entity.PatientSignID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientSignID = null;
					else entity.PatientSignID = Convert.ToInt64(value);
				}
			}
			public System.String HandoversDateTime
			{
				get
				{
					System.DateTime? data = entity.HandoversDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HandoversDateTime = null;
					else entity.HandoversDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String HandoversByUserID
			{
				get
				{
					System.String data = entity.HandoversByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HandoversByUserID = null;
					else entity.HandoversByUserID = Convert.ToString(value);
				}
			}
			public System.String HandoversToUserID
			{
				get
				{
					System.String data = entity.HandoversToUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HandoversToUserID = null;
					else entity.HandoversToUserID = Convert.ToString(value);
				}
			}
			private esMedicationReceiveUsed entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicationReceiveUsedQuery query)
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
				throw new Exception("esMedicationReceiveUsed can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MedicationReceiveUsed : esMedicationReceiveUsed
	{	
	}

	[Serializable]
	abstract public class esMedicationReceiveUsedQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return MedicationReceiveUsedMetadata.Meta();
			}
		}	
			
		public esQueryItem MedicationReceiveNo
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.MedicationReceiveNo, esSystemType.Int64);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.SequenceNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ScheduleDateTime
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.ScheduleDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem SetupDateTime
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.SetupDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem SetupByUserID
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.SetupByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem VerificationDateTime
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.VerificationDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem VerificationByUserID
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.VerificationByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem RealizedDateTime
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.RealizedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem RealizedByUserID
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.RealizedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRMedicationReason
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.SRMedicationReason, esSystemType.String);
			}
		} 
			
		public esQueryItem IsNotConsume
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.IsNotConsume, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.Note, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsReSchedule
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.IsReSchedule, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsVoidSchedule
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.IsVoidSchedule, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsAdditionalSchedule
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.IsAdditionalSchedule, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem PatientSignID
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.PatientSignID, esSystemType.Int64);
			}
		} 
			
		public esQueryItem HandoversDateTime
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.HandoversDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem HandoversByUserID
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.HandoversByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem HandoversToUserID
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveUsedMetadata.ColumnNames.HandoversToUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicationReceiveUsedCollection")]
	public partial class MedicationReceiveUsedCollection : esMedicationReceiveUsedCollection, IEnumerable< MedicationReceiveUsed>
	{
		public MedicationReceiveUsedCollection()
		{

		}	
		
		public static implicit operator List< MedicationReceiveUsed>(MedicationReceiveUsedCollection coll)
		{
			List< MedicationReceiveUsed> list = new List< MedicationReceiveUsed>();
			
			foreach (MedicationReceiveUsed emp in coll)
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
				return  MedicationReceiveUsedMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicationReceiveUsedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicationReceiveUsed(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicationReceiveUsed();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public MedicationReceiveUsedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicationReceiveUsedQuery();
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
		public bool Load(MedicationReceiveUsedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MedicationReceiveUsed AddNew()
		{
			MedicationReceiveUsed entity = base.AddNewEntity() as MedicationReceiveUsed;
			
			return entity;		
		}
		public MedicationReceiveUsed FindByPrimaryKey(Int64 medicationReceiveNo, Int32 sequenceNo)
		{
			return base.FindByPrimaryKey(medicationReceiveNo, sequenceNo) as MedicationReceiveUsed;
		}

		#region IEnumerable< MedicationReceiveUsed> Members

		IEnumerator< MedicationReceiveUsed> IEnumerable< MedicationReceiveUsed>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as MedicationReceiveUsed;
			}
		}

		#endregion
		
		private MedicationReceiveUsedQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicationReceiveUsed' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MedicationReceiveUsed ({MedicationReceiveNo, SequenceNo})")]
	[Serializable]
	public partial class MedicationReceiveUsed : esMedicationReceiveUsed
	{
		public MedicationReceiveUsed()
		{
		}	
	
		public MedicationReceiveUsed(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicationReceiveUsedMetadata.Meta();
			}
		}	
	
		override protected esMedicationReceiveUsedQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicationReceiveUsedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public MedicationReceiveUsedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicationReceiveUsedQuery();
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
		public bool Load(MedicationReceiveUsedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private MedicationReceiveUsedQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MedicationReceiveUsedQuery : esMedicationReceiveUsedQuery
	{
		public MedicationReceiveUsedQuery()
		{

		}		
		
		public MedicationReceiveUsedQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "MedicationReceiveUsedQuery";
        }
	}

	[Serializable]
	public partial class MedicationReceiveUsedMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicationReceiveUsedMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.MedicationReceiveNo, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.MedicationReceiveNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.SequenceNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.ScheduleDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.ScheduleDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.SetupDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.SetupDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.SetupByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.SetupByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.VerificationDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.VerificationDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.VerificationByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.VerificationByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.RealizedDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.RealizedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.RealizedByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.RealizedByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.Qty, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.ParamedicID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.SRMedicationReason, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.SRMedicationReason;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.IsNotConsume, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.IsNotConsume;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.Note, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.IsReSchedule, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.IsReSchedule;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.IsVoidSchedule, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.IsVoidSchedule;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.IsAdditionalSchedule, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.IsAdditionalSchedule;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.PatientSignID, 19, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.PatientSignID;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.HandoversDateTime, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.HandoversDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.HandoversByUserID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.HandoversByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicationReceiveUsedMetadata.ColumnNames.HandoversToUserID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveUsedMetadata.PropertyNames.HandoversToUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public MedicationReceiveUsedMetadata Meta()
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
			public const string MedicationReceiveNo = "MedicationReceiveNo";
			public const string SequenceNo = "SequenceNo";
			public const string ScheduleDateTime = "ScheduleDateTime";
			public const string SetupDateTime = "SetupDateTime";
			public const string SetupByUserID = "SetupByUserID";
			public const string VerificationDateTime = "VerificationDateTime";
			public const string VerificationByUserID = "VerificationByUserID";
			public const string RealizedDateTime = "RealizedDateTime";
			public const string RealizedByUserID = "RealizedByUserID";
			public const string Qty = "Qty";
			public const string ParamedicID = "ParamedicID";
			public const string SRMedicationReason = "SRMedicationReason";
			public const string IsNotConsume = "IsNotConsume";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsReSchedule = "IsReSchedule";
			public const string IsVoidSchedule = "IsVoidSchedule";
			public const string IsAdditionalSchedule = "IsAdditionalSchedule";
			public const string PatientSignID = "PatientSignID";
			public const string HandoversDateTime = "HandoversDateTime";
			public const string HandoversByUserID = "HandoversByUserID";
			public const string HandoversToUserID = "HandoversToUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string MedicationReceiveNo = "MedicationReceiveNo";
			public const string SequenceNo = "SequenceNo";
			public const string ScheduleDateTime = "ScheduleDateTime";
			public const string SetupDateTime = "SetupDateTime";
			public const string SetupByUserID = "SetupByUserID";
			public const string VerificationDateTime = "VerificationDateTime";
			public const string VerificationByUserID = "VerificationByUserID";
			public const string RealizedDateTime = "RealizedDateTime";
			public const string RealizedByUserID = "RealizedByUserID";
			public const string Qty = "Qty";
			public const string ParamedicID = "ParamedicID";
			public const string SRMedicationReason = "SRMedicationReason";
			public const string IsNotConsume = "IsNotConsume";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsReSchedule = "IsReSchedule";
			public const string IsVoidSchedule = "IsVoidSchedule";
			public const string IsAdditionalSchedule = "IsAdditionalSchedule";
			public const string PatientSignID = "PatientSignID";
			public const string HandoversDateTime = "HandoversDateTime";
			public const string HandoversByUserID = "HandoversByUserID";
			public const string HandoversToUserID = "HandoversToUserID";
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
			lock (typeof(MedicationReceiveUsedMetadata))
			{
				if(MedicationReceiveUsedMetadata.mapDelegates == null)
				{
					MedicationReceiveUsedMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MedicationReceiveUsedMetadata.meta == null)
				{
					MedicationReceiveUsedMetadata.meta = new MedicationReceiveUsedMetadata();
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
				
				meta.AddTypeMap("MedicationReceiveNo", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ScheduleDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SetupDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SetupByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerificationDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerificationByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RealizedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RealizedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMedicationReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsNotConsume", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsReSchedule", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoidSchedule", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAdditionalSchedule", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PatientSignID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("HandoversDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("HandoversByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("HandoversToUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "MedicationReceiveUsed";
				meta.Destination = "MedicationReceiveUsed";
				meta.spInsert = "proc_MedicationReceiveUsedInsert";				
				meta.spUpdate = "proc_MedicationReceiveUsedUpdate";		
				meta.spDelete = "proc_MedicationReceiveUsedDelete";
				meta.spLoadAll = "proc_MedicationReceiveUsedLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicationReceiveUsedLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicationReceiveUsedMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
