/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/6/2019 1:42:16 PM
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
	abstract public class esNursingDiagnosaTransDTCollection : esEntityCollectionWAuditLog
	{
		public esNursingDiagnosaTransDTCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "NursingDiagnosaTransDTCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esNursingDiagnosaTransDTQuery query)
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
			this.InitQuery(query as esNursingDiagnosaTransDTQuery);
		}
		#endregion
			
		virtual public NursingDiagnosaTransDT DetachEntity(NursingDiagnosaTransDT entity)
		{
			return base.DetachEntity(entity) as NursingDiagnosaTransDT;
		}
		
		virtual public NursingDiagnosaTransDT AttachEntity(NursingDiagnosaTransDT entity)
		{
			return base.AttachEntity(entity) as NursingDiagnosaTransDT;
		}
		
		virtual public void Combine(NursingDiagnosaTransDTCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NursingDiagnosaTransDT this[int index]
		{
			get
			{
				return base[index] as NursingDiagnosaTransDT;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NursingDiagnosaTransDT);
		}
	}

	[Serializable]
	abstract public class esNursingDiagnosaTransDT : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNursingDiagnosaTransDTQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esNursingDiagnosaTransDT()
		{
		}
	
		public esNursingDiagnosaTransDT(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 iD)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(iD);
			else
				return LoadByPrimaryKeyStoredProcedure(iD);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 iD)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(iD);
			else
				return LoadByPrimaryKeyStoredProcedure(iD);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int64 iD)
		{
			esNursingDiagnosaTransDTQuery query = this.GetDynamicQuery();
			query.Where(query.ID==iD);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int64 iD)
		{
			esParameters parms = new esParameters();
			parms.Add("ID",iD);
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
						case "ID": this.str.ID = (string)value; break;
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "NursingDiagnosaID": this.str.NursingDiagnosaID = (string)value; break;
						case "NursingDiagnosaName": this.str.NursingDiagnosaName = (string)value; break;
						case "SRNursingDiagnosaLevel": this.str.SRNursingDiagnosaLevel = (string)value; break;
						case "NursingDiagnosaParentID": this.str.NursingDiagnosaParentID = (string)value; break;
						case "Priority": this.str.Priority = (string)value; break;
						case "Skala": this.str.Skala = (string)value; break;
						case "Target": this.str.Target = (string)value; break;
						case "Respond": this.str.Respond = (string)value; break;
						case "Evaluasi": this.str.Evaluasi = (string)value; break;
						case "Reexamine": this.str.Reexamine = (string)value; break;
						case "ExecuteDateTime": this.str.ExecuteDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "TmpNursingDiagnosaID": this.str.TmpNursingDiagnosaID = (string)value; break;
						case "TmpNursingDiagnosaParentID": this.str.TmpNursingDiagnosaParentID = (string)value; break;
						case "EvalPeriod": this.str.EvalPeriod = (string)value; break;
						case "PeriodConversionInHour": this.str.PeriodConversionInHour = (string)value; break;
						case "S": this.str.S = (string)value; break;
						case "O": this.str.O = (string)value; break;
						case "A": this.str.A = (string)value; break;
						case "SRNursingCarePlanning": this.str.SRNursingCarePlanning = (string)value; break;
						case "P": this.str.P = (string)value; break;
						case "ParentID": this.str.ParentID = (string)value; break;
						case "IsDeleted": this.str.IsDeleted = (string)value; break;
						case "ReferenceToPhrNo": this.str.ReferenceToPhrNo = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDatetime": this.str.ApprovedDatetime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsPRMRJ": this.str.IsPRMRJ = (string)value; break;
						case "PpaInstruction": this.str.PpaInstruction = (string)value; break;
						case "SRUserType": this.str.SRUserType = (string)value; break;
						case "PrescriptionCurrentDay": this.str.PrescriptionCurrentDay = (string)value; break;
						case "SubmitBy": this.str.SubmitBy = (string)value; break;
						case "ReceiveBy": this.str.ReceiveBy = (string)value; break;
						case "SRNsType": this.str.SRNsType = (string)value; break;
						case "TemplateID": this.str.TemplateID = (string)value; break;
						case "Info5": this.str.Info5 = (string)value; break;
						case "DpjpNotes": this.str.DpjpNotes = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ID":
						
							if (value == null || value is System.Int64)
								this.ID = (System.Int64?)value;
							break;
						case "Priority":
						
							if (value == null || value is System.Int32)
								this.Priority = (System.Int32?)value;
							break;
						case "Skala":
						
							if (value == null || value is System.Int32)
								this.Skala = (System.Int32?)value;
							break;
						case "Target":
						
							if (value == null || value is System.Int32)
								this.Target = (System.Int32?)value;
							break;
						case "Evaluasi":
						
							if (value == null || value is System.Int32)
								this.Evaluasi = (System.Int32?)value;
							break;
						case "Reexamine":
						
							if (value == null || value is System.Boolean)
								this.Reexamine = (System.Boolean?)value;
							break;
						case "ExecuteDateTime":
						
							if (value == null || value is System.DateTime)
								this.ExecuteDateTime = (System.DateTime?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "EvalPeriod":
						
							if (value == null || value is System.Int32)
								this.EvalPeriod = (System.Int32?)value;
							break;
						case "PeriodConversionInHour":
						
							if (value == null || value is System.Int32)
								this.PeriodConversionInHour = (System.Int32?)value;
							break;
						case "ParentID":
						
							if (value == null || value is System.Int64)
								this.ParentID = (System.Int64?)value;
							break;
						case "IsDeleted":
						
							if (value == null || value is System.Boolean)
								this.IsDeleted = (System.Boolean?)value;
							break;
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDatetime":
						
							if (value == null || value is System.DateTime)
								this.ApprovedDatetime = (System.DateTime?)value;
							break;
						case "IsPRMRJ":
						
							if (value == null || value is System.Boolean)
								this.IsPRMRJ = (System.Boolean?)value;
							break;
						case "TemplateID":
						
							if (value == null || value is System.Int32)
								this.TemplateID = (System.Int32?)value;
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
		/// Maps to NursingDiagnosaTransDT.ID
		/// </summary>
		virtual public System.Int64? ID
		{
			get
			{
				return base.GetSystemInt64(NursingDiagnosaTransDTMetadata.ColumnNames.ID);
			}
			
			set
			{
				base.SetSystemInt64(NursingDiagnosaTransDTMetadata.ColumnNames.ID, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.NursingDiagnosaID
		/// </summary>
		virtual public System.String NursingDiagnosaID
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaID);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaID, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.NursingDiagnosaName
		/// </summary>
		virtual public System.String NursingDiagnosaName
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaName);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaName, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.SRNursingDiagnosaLevel
		/// </summary>
		virtual public System.String SRNursingDiagnosaLevel
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.SRNursingDiagnosaLevel);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.SRNursingDiagnosaLevel, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.NursingDiagnosaParentID
		/// </summary>
		virtual public System.String NursingDiagnosaParentID
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaParentID);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaParentID, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.Priority
		/// </summary>
		virtual public System.Int32? Priority
		{
			get
			{
				return base.GetSystemInt32(NursingDiagnosaTransDTMetadata.ColumnNames.Priority);
			}
			
			set
			{
				base.SetSystemInt32(NursingDiagnosaTransDTMetadata.ColumnNames.Priority, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.Skala
		/// </summary>
		virtual public System.Int32? Skala
		{
			get
			{
				return base.GetSystemInt32(NursingDiagnosaTransDTMetadata.ColumnNames.Skala);
			}
			
			set
			{
				base.SetSystemInt32(NursingDiagnosaTransDTMetadata.ColumnNames.Skala, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.Target
		/// </summary>
		virtual public System.Int32? Target
		{
			get
			{
				return base.GetSystemInt32(NursingDiagnosaTransDTMetadata.ColumnNames.Target);
			}
			
			set
			{
				base.SetSystemInt32(NursingDiagnosaTransDTMetadata.ColumnNames.Target, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.Respond
		/// </summary>
		virtual public System.String Respond
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.Respond);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.Respond, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.Evaluasi
		/// </summary>
		virtual public System.Int32? Evaluasi
		{
			get
			{
				return base.GetSystemInt32(NursingDiagnosaTransDTMetadata.ColumnNames.Evaluasi);
			}
			
			set
			{
				base.SetSystemInt32(NursingDiagnosaTransDTMetadata.ColumnNames.Evaluasi, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.Reexamine
		/// </summary>
		virtual public System.Boolean? Reexamine
		{
			get
			{
				return base.GetSystemBoolean(NursingDiagnosaTransDTMetadata.ColumnNames.Reexamine);
			}
			
			set
			{
				base.SetSystemBoolean(NursingDiagnosaTransDTMetadata.ColumnNames.Reexamine, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.ExecuteDateTime
		/// </summary>
		virtual public System.DateTime? ExecuteDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingDiagnosaTransDTMetadata.ColumnNames.ExecuteDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingDiagnosaTransDTMetadata.ColumnNames.ExecuteDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingDiagnosaTransDTMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingDiagnosaTransDTMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingDiagnosaTransDTMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingDiagnosaTransDTMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.TmpNursingDiagnosaID
		/// </summary>
		virtual public System.String TmpNursingDiagnosaID
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.TmpNursingDiagnosaID);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.TmpNursingDiagnosaID, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.TmpNursingDiagnosaParentID
		/// </summary>
		virtual public System.String TmpNursingDiagnosaParentID
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.TmpNursingDiagnosaParentID);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.TmpNursingDiagnosaParentID, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.EvalPeriod
		/// </summary>
		virtual public System.Int32? EvalPeriod
		{
			get
			{
				return base.GetSystemInt32(NursingDiagnosaTransDTMetadata.ColumnNames.EvalPeriod);
			}
			
			set
			{
				base.SetSystemInt32(NursingDiagnosaTransDTMetadata.ColumnNames.EvalPeriod, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.PeriodConversionInHour
		/// </summary>
		virtual public System.Int32? PeriodConversionInHour
		{
			get
			{
				return base.GetSystemInt32(NursingDiagnosaTransDTMetadata.ColumnNames.PeriodConversionInHour);
			}
			
			set
			{
				base.SetSystemInt32(NursingDiagnosaTransDTMetadata.ColumnNames.PeriodConversionInHour, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.S
		/// </summary>
		virtual public System.String S
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.S);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.S, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.O
		/// </summary>
		virtual public System.String O
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.O);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.O, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.A
		/// </summary>
		virtual public System.String A
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.A);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.A, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.SRNursingCarePlanning
		/// </summary>
		virtual public System.String SRNursingCarePlanning
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.SRNursingCarePlanning);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.SRNursingCarePlanning, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.P
		/// </summary>
		virtual public System.String P
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.P);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.P, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.ParentID
		/// </summary>
		virtual public System.Int64? ParentID
		{
			get
			{
				return base.GetSystemInt64(NursingDiagnosaTransDTMetadata.ColumnNames.ParentID);
			}
			
			set
			{
				base.SetSystemInt64(NursingDiagnosaTransDTMetadata.ColumnNames.ParentID, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(NursingDiagnosaTransDTMetadata.ColumnNames.IsDeleted);
			}
			
			set
			{
				base.SetSystemBoolean(NursingDiagnosaTransDTMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.ReferenceToPhrNo
		/// </summary>
		virtual public System.String ReferenceToPhrNo
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.ReferenceToPhrNo);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.ReferenceToPhrNo, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(NursingDiagnosaTransDTMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(NursingDiagnosaTransDTMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.ApprovedDatetime
		/// </summary>
		virtual public System.DateTime? ApprovedDatetime
		{
			get
			{
				return base.GetSystemDateTime(NursingDiagnosaTransDTMetadata.ColumnNames.ApprovedDatetime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingDiagnosaTransDTMetadata.ColumnNames.ApprovedDatetime, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.IsPRMRJ
		/// </summary>
		virtual public System.Boolean? IsPRMRJ
		{
			get
			{
				return base.GetSystemBoolean(NursingDiagnosaTransDTMetadata.ColumnNames.IsPRMRJ);
			}
			
			set
			{
				base.SetSystemBoolean(NursingDiagnosaTransDTMetadata.ColumnNames.IsPRMRJ, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.PpaInstruction
		/// </summary>
		virtual public System.String PpaInstruction
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.PpaInstruction);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.PpaInstruction, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.SRUserType
		/// </summary>
		virtual public System.String SRUserType
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.SRUserType);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.SRUserType, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.PrescriptionCurrentDay
		/// </summary>
		virtual public System.String PrescriptionCurrentDay
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.PrescriptionCurrentDay);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.PrescriptionCurrentDay, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.SubmitBy
		/// </summary>
		virtual public System.String SubmitBy
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.SubmitBy);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.SubmitBy, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.ReceiveBy
		/// </summary>
		virtual public System.String ReceiveBy
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.ReceiveBy);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.ReceiveBy, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.SRNsType
		/// </summary>
		virtual public System.String SRNsType
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.SRNsType);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.SRNsType, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.TemplateID
		/// </summary>
		virtual public System.Int32? TemplateID
		{
			get
			{
				return base.GetSystemInt32(NursingDiagnosaTransDTMetadata.ColumnNames.TemplateID);
			}
			
			set
			{
				base.SetSystemInt32(NursingDiagnosaTransDTMetadata.ColumnNames.TemplateID, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.Info5
		/// </summary>
		virtual public System.String Info5
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.Info5);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.Info5, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.DpjpNotes
		/// </summary>
		virtual public System.String DpjpNotes
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.DpjpNotes);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.DpjpNotes, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTransDT.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTransDTMetadata.ColumnNames.ParamedicID, value);
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
			public esStrings(esNursingDiagnosaTransDT entity)
			{
				this.entity = entity;
			}
			public System.String ID
			{
				get
				{
					System.Int64? data = entity.ID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ID = null;
					else entity.ID = Convert.ToInt64(value);
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
			public System.String NursingDiagnosaID
			{
				get
				{
					System.String data = entity.NursingDiagnosaID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NursingDiagnosaID = null;
					else entity.NursingDiagnosaID = Convert.ToString(value);
				}
			}
			public System.String NursingDiagnosaName
			{
				get
				{
					System.String data = entity.NursingDiagnosaName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NursingDiagnosaName = null;
					else entity.NursingDiagnosaName = Convert.ToString(value);
				}
			}
			public System.String SRNursingDiagnosaLevel
			{
				get
				{
					System.String data = entity.SRNursingDiagnosaLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRNursingDiagnosaLevel = null;
					else entity.SRNursingDiagnosaLevel = Convert.ToString(value);
				}
			}
			public System.String NursingDiagnosaParentID
			{
				get
				{
					System.String data = entity.NursingDiagnosaParentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NursingDiagnosaParentID = null;
					else entity.NursingDiagnosaParentID = Convert.ToString(value);
				}
			}
			public System.String Priority
			{
				get
				{
					System.Int32? data = entity.Priority;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Priority = null;
					else entity.Priority = Convert.ToInt32(value);
				}
			}
			public System.String Skala
			{
				get
				{
					System.Int32? data = entity.Skala;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Skala = null;
					else entity.Skala = Convert.ToInt32(value);
				}
			}
			public System.String Target
			{
				get
				{
					System.Int32? data = entity.Target;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Target = null;
					else entity.Target = Convert.ToInt32(value);
				}
			}
			public System.String Respond
			{
				get
				{
					System.String data = entity.Respond;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Respond = null;
					else entity.Respond = Convert.ToString(value);
				}
			}
			public System.String Evaluasi
			{
				get
				{
					System.Int32? data = entity.Evaluasi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Evaluasi = null;
					else entity.Evaluasi = Convert.ToInt32(value);
				}
			}
			public System.String Reexamine
			{
				get
				{
					System.Boolean? data = entity.Reexamine;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Reexamine = null;
					else entity.Reexamine = Convert.ToBoolean(value);
				}
			}
			public System.String ExecuteDateTime
			{
				get
				{
					System.DateTime? data = entity.ExecuteDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExecuteDateTime = null;
					else entity.ExecuteDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreateByUserID
			{
				get
				{
					System.String data = entity.CreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateByUserID = null;
					else entity.CreateByUserID = Convert.ToString(value);
				}
			}
			public System.String CreateDateTime
			{
				get
				{
					System.DateTime? data = entity.CreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateDateTime = null;
					else entity.CreateDateTime = Convert.ToDateTime(value);
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
			public System.String TmpNursingDiagnosaID
			{
				get
				{
					System.String data = entity.TmpNursingDiagnosaID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TmpNursingDiagnosaID = null;
					else entity.TmpNursingDiagnosaID = Convert.ToString(value);
				}
			}
			public System.String TmpNursingDiagnosaParentID
			{
				get
				{
					System.String data = entity.TmpNursingDiagnosaParentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TmpNursingDiagnosaParentID = null;
					else entity.TmpNursingDiagnosaParentID = Convert.ToString(value);
				}
			}
			public System.String EvalPeriod
			{
				get
				{
					System.Int32? data = entity.EvalPeriod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EvalPeriod = null;
					else entity.EvalPeriod = Convert.ToInt32(value);
				}
			}
			public System.String PeriodConversionInHour
			{
				get
				{
					System.Int32? data = entity.PeriodConversionInHour;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodConversionInHour = null;
					else entity.PeriodConversionInHour = Convert.ToInt32(value);
				}
			}
			public System.String S
			{
				get
				{
					System.String data = entity.S;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.S = null;
					else entity.S = Convert.ToString(value);
				}
			}
			public System.String O
			{
				get
				{
					System.String data = entity.O;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.O = null;
					else entity.O = Convert.ToString(value);
				}
			}
			public System.String A
			{
				get
				{
					System.String data = entity.A;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.A = null;
					else entity.A = Convert.ToString(value);
				}
			}
			public System.String SRNursingCarePlanning
			{
				get
				{
					System.String data = entity.SRNursingCarePlanning;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRNursingCarePlanning = null;
					else entity.SRNursingCarePlanning = Convert.ToString(value);
				}
			}
			public System.String P
			{
				get
				{
					System.String data = entity.P;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.P = null;
					else entity.P = Convert.ToString(value);
				}
			}
			public System.String ParentID
			{
				get
				{
					System.Int64? data = entity.ParentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParentID = null;
					else entity.ParentID = Convert.ToInt64(value);
				}
			}
			public System.String IsDeleted
			{
				get
				{
					System.Boolean? data = entity.IsDeleted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDeleted = null;
					else entity.IsDeleted = Convert.ToBoolean(value);
				}
			}
			public System.String ReferenceToPhrNo
			{
				get
				{
					System.String data = entity.ReferenceToPhrNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceToPhrNo = null;
					else entity.ReferenceToPhrNo = Convert.ToString(value);
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
			public System.String ApprovedDatetime
			{
				get
				{
					System.DateTime? data = entity.ApprovedDatetime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDatetime = null;
					else entity.ApprovedDatetime = Convert.ToDateTime(value);
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
			public System.String IsPRMRJ
			{
				get
				{
					System.Boolean? data = entity.IsPRMRJ;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPRMRJ = null;
					else entity.IsPRMRJ = Convert.ToBoolean(value);
				}
			}
			public System.String PpaInstruction
			{
				get
				{
					System.String data = entity.PpaInstruction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PpaInstruction = null;
					else entity.PpaInstruction = Convert.ToString(value);
				}
			}
			public System.String SRUserType
			{
				get
				{
					System.String data = entity.SRUserType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRUserType = null;
					else entity.SRUserType = Convert.ToString(value);
				}
			}
			public System.String PrescriptionCurrentDay
			{
				get
				{
					System.String data = entity.PrescriptionCurrentDay;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrescriptionCurrentDay = null;
					else entity.PrescriptionCurrentDay = Convert.ToString(value);
				}
			}
			public System.String SubmitBy
			{
				get
				{
					System.String data = entity.SubmitBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubmitBy = null;
					else entity.SubmitBy = Convert.ToString(value);
				}
			}
			public System.String ReceiveBy
			{
				get
				{
					System.String data = entity.ReceiveBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceiveBy = null;
					else entity.ReceiveBy = Convert.ToString(value);
				}
			}
			public System.String SRNsType
			{
				get
				{
					System.String data = entity.SRNsType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRNsType = null;
					else entity.SRNsType = Convert.ToString(value);
				}
			}
			public System.String TemplateID
			{
				get
				{
					System.Int32? data = entity.TemplateID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemplateID = null;
					else entity.TemplateID = Convert.ToInt32(value);
				}
			}
			public System.String Info5
			{
				get
				{
					System.String data = entity.Info5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info5 = null;
					else entity.Info5 = Convert.ToString(value);
				}
			}
			public System.String DpjpNotes
			{
				get
				{
					System.String data = entity.DpjpNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DpjpNotes = null;
					else entity.DpjpNotes = Convert.ToString(value);
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
			private esNursingDiagnosaTransDT entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNursingDiagnosaTransDTQuery query)
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
				throw new Exception("esNursingDiagnosaTransDT can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class NursingDiagnosaTransDT : esNursingDiagnosaTransDT
	{	
	}

	[Serializable]
	abstract public class esNursingDiagnosaTransDTQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return NursingDiagnosaTransDTMetadata.Meta();
			}
		}	
			
		public esQueryItem ID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.ID, esSystemType.Int64);
			}
		} 
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem NursingDiagnosaID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaID, esSystemType.String);
			}
		} 
			
		public esQueryItem NursingDiagnosaName
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaName, esSystemType.String);
			}
		} 
			
		public esQueryItem SRNursingDiagnosaLevel
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.SRNursingDiagnosaLevel, esSystemType.String);
			}
		} 
			
		public esQueryItem NursingDiagnosaParentID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaParentID, esSystemType.String);
			}
		} 
			
		public esQueryItem Priority
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.Priority, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Skala
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.Skala, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Target
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.Target, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Respond
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.Respond, esSystemType.String);
			}
		} 
			
		public esQueryItem Evaluasi
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.Evaluasi, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Reexamine
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.Reexamine, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ExecuteDateTime
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.ExecuteDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem TmpNursingDiagnosaID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.TmpNursingDiagnosaID, esSystemType.String);
			}
		} 
			
		public esQueryItem TmpNursingDiagnosaParentID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.TmpNursingDiagnosaParentID, esSystemType.String);
			}
		} 
			
		public esQueryItem EvalPeriod
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.EvalPeriod, esSystemType.Int32);
			}
		} 
			
		public esQueryItem PeriodConversionInHour
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.PeriodConversionInHour, esSystemType.Int32);
			}
		} 
			
		public esQueryItem S
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.S, esSystemType.String);
			}
		} 
			
		public esQueryItem O
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.O, esSystemType.String);
			}
		} 
			
		public esQueryItem A
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.A, esSystemType.String);
			}
		} 
			
		public esQueryItem SRNursingCarePlanning
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.SRNursingCarePlanning, esSystemType.String);
			}
		} 
			
		public esQueryItem P
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.P, esSystemType.String);
			}
		} 
			
		public esQueryItem ParentID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.ParentID, esSystemType.Int64);
			}
		} 
			
		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ReferenceToPhrNo
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.ReferenceToPhrNo, esSystemType.String);
			}
		} 
			
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ApprovedDatetime
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.ApprovedDatetime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsPRMRJ
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.IsPRMRJ, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem PpaInstruction
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.PpaInstruction, esSystemType.String);
			}
		} 
			
		public esQueryItem SRUserType
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.SRUserType, esSystemType.String);
			}
		} 
			
		public esQueryItem PrescriptionCurrentDay
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.PrescriptionCurrentDay, esSystemType.String);
			}
		} 
			
		public esQueryItem SubmitBy
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.SubmitBy, esSystemType.String);
			}
		} 
			
		public esQueryItem ReceiveBy
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.ReceiveBy, esSystemType.String);
			}
		} 
			
		public esQueryItem SRNsType
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.SRNsType, esSystemType.String);
			}
		} 
			
		public esQueryItem TemplateID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.TemplateID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Info5
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.Info5, esSystemType.String);
			}
		} 
			
		public esQueryItem DpjpNotes
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.DpjpNotes, esSystemType.String);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTransDTMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NursingDiagnosaTransDTCollection")]
	public partial class NursingDiagnosaTransDTCollection : esNursingDiagnosaTransDTCollection, IEnumerable< NursingDiagnosaTransDT>
	{
		public NursingDiagnosaTransDTCollection()
		{

		}	
		
		public static implicit operator List< NursingDiagnosaTransDT>(NursingDiagnosaTransDTCollection coll)
		{
			List< NursingDiagnosaTransDT> list = new List< NursingDiagnosaTransDT>();
			
			foreach (NursingDiagnosaTransDT emp in coll)
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
				return  NursingDiagnosaTransDTMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingDiagnosaTransDTQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NursingDiagnosaTransDT(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NursingDiagnosaTransDT();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public NursingDiagnosaTransDTQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingDiagnosaTransDTQuery();
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
		public bool Load(NursingDiagnosaTransDTQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public NursingDiagnosaTransDT AddNew()
		{
			NursingDiagnosaTransDT entity = base.AddNewEntity() as NursingDiagnosaTransDT;
			
			return entity;		
		}
		public NursingDiagnosaTransDT FindByPrimaryKey(Int64 iD)
		{
			return base.FindByPrimaryKey(iD) as NursingDiagnosaTransDT;
		}

		#region IEnumerable< NursingDiagnosaTransDT> Members

		IEnumerator< NursingDiagnosaTransDT> IEnumerable< NursingDiagnosaTransDT>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NursingDiagnosaTransDT;
			}
		}

		#endregion
		
		private NursingDiagnosaTransDTQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NursingDiagnosaTransDT' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("NursingDiagnosaTransDT ({ID})")]
	[Serializable]
	public partial class NursingDiagnosaTransDT : esNursingDiagnosaTransDT
	{
		public NursingDiagnosaTransDT()
		{
		}	
	
		public NursingDiagnosaTransDT(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NursingDiagnosaTransDTMetadata.Meta();
			}
		}	
	
		override protected esNursingDiagnosaTransDTQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingDiagnosaTransDTQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public NursingDiagnosaTransDTQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingDiagnosaTransDTQuery();
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
		public bool Load(NursingDiagnosaTransDTQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private NursingDiagnosaTransDTQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class NursingDiagnosaTransDTQuery : esNursingDiagnosaTransDTQuery
	{
		public NursingDiagnosaTransDTQuery()
		{

		}		
		
		public NursingDiagnosaTransDTQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "NursingDiagnosaTransDTQuery";
        }
	}

	[Serializable]
	public partial class NursingDiagnosaTransDTMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NursingDiagnosaTransDTMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.ID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.ID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.NursingDiagnosaID;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.NursingDiagnosaName;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.SRNursingDiagnosaLevel, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.SRNursingDiagnosaLevel;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaParentID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.NursingDiagnosaParentID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.Priority, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.Priority;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.Skala, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.Skala;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.Target, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.Target;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.Respond, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.Respond;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.Evaluasi, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.Evaluasi;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.Reexamine, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.Reexamine;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.ExecuteDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.ExecuteDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.CreateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.CreateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.LastUpdateDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.TmpNursingDiagnosaID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.TmpNursingDiagnosaID;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.TmpNursingDiagnosaParentID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.TmpNursingDiagnosaParentID;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.EvalPeriod, 19, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.EvalPeriod;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.PeriodConversionInHour, 20, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.PeriodConversionInHour;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.S, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.S;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.O, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.O;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.A, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.A;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.SRNursingCarePlanning, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.SRNursingCarePlanning;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.P, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.P;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.ParentID, 26, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.ParentID;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.IsDeleted, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.ReferenceToPhrNo, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.ReferenceToPhrNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.IsApproved, 29, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.ApprovedDatetime, 30, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.ApprovedDatetime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.ApprovedByUserID, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.IsPRMRJ, 32, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.IsPRMRJ;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.PpaInstruction, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.PpaInstruction;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.SRUserType, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.SRUserType;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.PrescriptionCurrentDay, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.PrescriptionCurrentDay;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.SubmitBy, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.SubmitBy;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.ReceiveBy, 37, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.ReceiveBy;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.SRNsType, 38, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.SRNsType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.TemplateID, 39, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.TemplateID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.Info5, 40, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.Info5;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.DpjpNotes, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.DpjpNotes;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTransDTMetadata.ColumnNames.ParamedicID, 42, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTransDTMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public NursingDiagnosaTransDTMetadata Meta()
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
			public const string ID = "ID";
			public const string TransactionNo = "TransactionNo";
			public const string NursingDiagnosaID = "NursingDiagnosaID";
			public const string NursingDiagnosaName = "NursingDiagnosaName";
			public const string SRNursingDiagnosaLevel = "SRNursingDiagnosaLevel";
			public const string NursingDiagnosaParentID = "NursingDiagnosaParentID";
			public const string Priority = "Priority";
			public const string Skala = "Skala";
			public const string Target = "Target";
			public const string Respond = "Respond";
			public const string Evaluasi = "Evaluasi";
			public const string Reexamine = "Reexamine";
			public const string ExecuteDateTime = "ExecuteDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string TmpNursingDiagnosaID = "TmpNursingDiagnosaID";
			public const string TmpNursingDiagnosaParentID = "TmpNursingDiagnosaParentID";
			public const string EvalPeriod = "EvalPeriod";
			public const string PeriodConversionInHour = "PeriodConversionInHour";
			public const string S = "S";
			public const string O = "O";
			public const string A = "A";
			public const string SRNursingCarePlanning = "SRNursingCarePlanning";
			public const string P = "P";
			public const string ParentID = "ParentID";
			public const string IsDeleted = "IsDeleted";
			public const string ReferenceToPhrNo = "ReferenceToPhrNo";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDatetime = "ApprovedDatetime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsPRMRJ = "IsPRMRJ";
			public const string PpaInstruction = "PpaInstruction";
			public const string SRUserType = "SRUserType";
			public const string PrescriptionCurrentDay = "PrescriptionCurrentDay";
			public const string SubmitBy = "SubmitBy";
			public const string ReceiveBy = "ReceiveBy";
			public const string SRNsType = "SRNsType";
			public const string TemplateID = "TemplateID";
			public const string Info5 = "Info5";
			public const string DpjpNotes = "DpjpNotes";
			public const string ParamedicID = "ParamedicID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ID = "ID";
			public const string TransactionNo = "TransactionNo";
			public const string NursingDiagnosaID = "NursingDiagnosaID";
			public const string NursingDiagnosaName = "NursingDiagnosaName";
			public const string SRNursingDiagnosaLevel = "SRNursingDiagnosaLevel";
			public const string NursingDiagnosaParentID = "NursingDiagnosaParentID";
			public const string Priority = "Priority";
			public const string Skala = "Skala";
			public const string Target = "Target";
			public const string Respond = "Respond";
			public const string Evaluasi = "Evaluasi";
			public const string Reexamine = "Reexamine";
			public const string ExecuteDateTime = "ExecuteDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string TmpNursingDiagnosaID = "TmpNursingDiagnosaID";
			public const string TmpNursingDiagnosaParentID = "TmpNursingDiagnosaParentID";
			public const string EvalPeriod = "EvalPeriod";
			public const string PeriodConversionInHour = "PeriodConversionInHour";
			public const string S = "S";
			public const string O = "O";
			public const string A = "A";
			public const string SRNursingCarePlanning = "SRNursingCarePlanning";
			public const string P = "P";
			public const string ParentID = "ParentID";
			public const string IsDeleted = "IsDeleted";
			public const string ReferenceToPhrNo = "ReferenceToPhrNo";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDatetime = "ApprovedDatetime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsPRMRJ = "IsPRMRJ";
			public const string PpaInstruction = "PpaInstruction";
			public const string SRUserType = "SRUserType";
			public const string PrescriptionCurrentDay = "PrescriptionCurrentDay";
			public const string SubmitBy = "SubmitBy";
			public const string ReceiveBy = "ReceiveBy";
			public const string SRNsType = "SRNsType";
			public const string TemplateID = "TemplateID";
			public const string Info5 = "Info5";
			public const string DpjpNotes = "DpjpNotes";
			public const string ParamedicID = "ParamedicID";
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
			lock (typeof(NursingDiagnosaTransDTMetadata))
			{
				if(NursingDiagnosaTransDTMetadata.mapDelegates == null)
				{
					NursingDiagnosaTransDTMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NursingDiagnosaTransDTMetadata.meta == null)
				{
					NursingDiagnosaTransDTMetadata.meta = new NursingDiagnosaTransDTMetadata();
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
				
				meta.AddTypeMap("ID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NursingDiagnosaID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NursingDiagnosaName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRNursingDiagnosaLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NursingDiagnosaParentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Priority", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Skala", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Target", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Respond", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Evaluasi", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Reexamine", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ExecuteDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TmpNursingDiagnosaID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TmpNursingDiagnosaParentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EvalPeriod", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PeriodConversionInHour", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("S", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("O", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("A", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRNursingCarePlanning", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("P", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParentID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ReferenceToPhrNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDatetime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPRMRJ", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PpaInstruction", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRUserType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrescriptionCurrentDay", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SubmitBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceiveBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRNsType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TemplateID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Info5", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DpjpNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "NursingDiagnosaTransDT";
				meta.Destination = "NursingDiagnosaTransDT";
				meta.spInsert = "proc_NursingDiagnosaTransDTInsert";				
				meta.spUpdate = "proc_NursingDiagnosaTransDTUpdate";		
				meta.spDelete = "proc_NursingDiagnosaTransDTDelete";
				meta.spLoadAll = "proc_NursingDiagnosaTransDTLoadAll";
				meta.spLoadByPrimaryKey = "proc_NursingDiagnosaTransDTLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NursingDiagnosaTransDTMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
