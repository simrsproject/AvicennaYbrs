/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : Oracle
Date Generated  : 5/31/2021 4:45:50 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.Zetta
{

	[Serializable]
	abstract public class esHisorderCollection : esEntityCollectionWAuditLog
	{
		public esHisorderCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "HisorderCollection";
		}

		#region Query Logic
		protected void InitQuery(esHisorderQuery query)
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
			this.InitQuery(query as esHisorderQuery);
		}
		#endregion
		
		virtual public Hisorder DetachEntity(Hisorder entity)
		{
			return base.DetachEntity(entity) as Hisorder;
		}
		
		virtual public Hisorder AttachEntity(Hisorder entity)
		{
			return base.AttachEntity(entity) as Hisorder;
		}
		
		virtual public void Combine(HisorderCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Hisorder this[int index]
		{
			get
			{
				return base[index] as Hisorder;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Hisorder);
		}
	}



	[Serializable]
	abstract public class esHisorder : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esHisorderQuery GetDynamicQuery()
		{
			return null;
		}

		public esHisorder()
		{

		}

		public esHisorder(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Decimal orderKey)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderKey);
			else
				return LoadByPrimaryKeyStoredProcedure(orderKey);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Decimal orderKey)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderKey);
			else
				return LoadByPrimaryKeyStoredProcedure(orderKey);
		}

		private bool LoadByPrimaryKeyDynamic(System.Decimal orderKey)
		{
			esHisorderQuery query = this.GetDynamicQuery();
			query.Where(query.OrderKey == orderKey);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Decimal orderKey)
		{
			esParameters parms = new esParameters();
			parms.Add("ORDER_KEY",orderKey);
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
						case "OrderKey": this.str.OrderKey = (string)value; break;							
						case "CreateDate": this.str.CreateDate = (string)value; break;							
						case "Flag": this.str.Flag = (string)value; break;							
						case "PatientId": this.str.PatientId = (string)value; break;							
						case "PatientName": this.str.PatientName = (string)value; break;							
						case "PatientUid": this.str.PatientUid = (string)value; break;							
						case "LastName": this.str.LastName = (string)value; break;							
						case "FirstName": this.str.FirstName = (string)value; break;							
						case "MiddleName": this.str.MiddleName = (string)value; break;							
						case "Aetitle": this.str.Aetitle = (string)value; break;							
						case "StudyReservDate": this.str.StudyReservDate = (string)value; break;							
						case "OrderModality": this.str.OrderModality = (string)value; break;							
						case "AccessionNum": this.str.AccessionNum = (string)value; break;							
						case "OrderStatus": this.str.OrderStatus = (string)value; break;							
						case "OrderCode": this.str.OrderCode = (string)value; break;							
						case "OrderName": this.str.OrderName = (string)value; break;							
						case "OrderReason": this.str.OrderReason = (string)value; break;							
						case "StudyRemark": this.str.StudyRemark = (string)value; break;							
						case "OrderBodypart": this.str.OrderBodypart = (string)value; break;							
						case "ChargeDocId": this.str.ChargeDocId = (string)value; break;							
						case "ChargeDocName": this.str.ChargeDocName = (string)value; break;							
						case "ConsultDocId": this.str.ConsultDocId = (string)value; break;							
						case "ConsultDocName": this.str.ConsultDocName = (string)value; break;							
						case "OrderDept": this.str.OrderDept = (string)value; break;							
						case "OrderDate": this.str.OrderDate = (string)value; break;							
						case "OrderNo": this.str.OrderNo = (string)value; break;							
						case "PatientIo": this.str.PatientIo = (string)value; break;							
						case "PatientWard": this.str.PatientWard = (string)value; break;							
						case "IoDate": this.str.IoDate = (string)value; break;							
						case "PatientBirthDate": this.str.PatientBirthDate = (string)value; break;							
						case "PatientSex": this.str.PatientSex = (string)value; break;							
						case "OrderDiag": this.str.OrderDiag = (string)value; break;							
						case "PatientBlood": this.str.PatientBlood = (string)value; break;							
						case "OrderCnt": this.str.OrderCnt = (string)value; break;							
						case "Group1": this.str.Group1 = (string)value; break;							
						case "Group2": this.str.Group2 = (string)value; break;							
						case "Group3": this.str.Group3 = (string)value; break;							
						case "OrderComment": this.str.OrderComment = (string)value; break;							
						case "Extension1": this.str.Extension1 = (string)value; break;							
						case "Extension2": this.str.Extension2 = (string)value; break;							
						case "Extension3": this.str.Extension3 = (string)value; break;							
						case "Extension4": this.str.Extension4 = (string)value; break;							
						case "Extension5": this.str.Extension5 = (string)value; break;							
						case "Extension6": this.str.Extension6 = (string)value; break;							
						case "Extension7": this.str.Extension7 = (string)value; break;							
						case "Extension8": this.str.Extension8 = (string)value; break;							
						case "Extension9": this.str.Extension9 = (string)value; break;							
						case "Extension10": this.str.Extension10 = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "OrderKey":
						
							if (value == null || value is System.Decimal)
								this.OrderKey = (System.Decimal?)value;
							break;
						
						case "OrderCnt":
						
							if (value == null || value is System.Decimal)
								this.OrderCnt = (System.Decimal?)value;
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
		/// Maps to HISORDER.ORDER_KEY
		/// </summary>
		virtual public System.Decimal? OrderKey
		{
			get
			{
				return base.GetSystemDecimal(HisorderMetadata.ColumnNames.OrderKey);
			}
			
			set
			{
				base.SetSystemDecimal(HisorderMetadata.ColumnNames.OrderKey, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.CREATE_DATE
		/// </summary>
		virtual public System.String CreateDate
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.CreateDate);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.CreateDate, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.FLAG
		/// </summary>
		virtual public System.String Flag
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.Flag);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.Flag, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.PATIENT_ID
		/// </summary>
		virtual public System.String PatientId
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.PatientId);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.PatientId, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.PATIENT_NAME
		/// </summary>
		virtual public System.String PatientName
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.PatientName);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.PatientName, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.PATIENT_UID
		/// </summary>
		virtual public System.String PatientUid
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.PatientUid);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.PatientUid, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.LAST_NAME
		/// </summary>
		virtual public System.String LastName
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.LastName);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.LastName, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.FIRST_NAME
		/// </summary>
		virtual public System.String FirstName
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.FirstName);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.FirstName, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.MIDDLE_NAME
		/// </summary>
		virtual public System.String MiddleName
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.MiddleName);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.MiddleName, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.AETITLE
		/// </summary>
		virtual public System.String Aetitle
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.Aetitle);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.Aetitle, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.STUDY_RESERV_DATE
		/// </summary>
		virtual public System.String StudyReservDate
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.StudyReservDate);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.StudyReservDate, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.ORDER_MODALITY
		/// </summary>
		virtual public System.String OrderModality
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.OrderModality);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.OrderModality, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.ACCESSION_NUM
		/// </summary>
		virtual public System.String AccessionNum
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.AccessionNum);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.AccessionNum, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.ORDER_STATUS
		/// </summary>
		virtual public System.String OrderStatus
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.OrderStatus);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.OrderStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.ORDER_CODE
		/// </summary>
		virtual public System.String OrderCode
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.OrderCode);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.OrderCode, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.ORDER_NAME
		/// </summary>
		virtual public System.String OrderName
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.OrderName);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.OrderName, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.ORDER_REASON
		/// </summary>
		virtual public System.String OrderReason
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.OrderReason);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.OrderReason, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.STUDY_REMARK
		/// </summary>
		virtual public System.String StudyRemark
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.StudyRemark);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.StudyRemark, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.ORDER_BODYPART
		/// </summary>
		virtual public System.String OrderBodypart
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.OrderBodypart);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.OrderBodypart, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.CHARGE_DOC_ID
		/// </summary>
		virtual public System.String ChargeDocId
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.ChargeDocId);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.ChargeDocId, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.CHARGE_DOC_NAME
		/// </summary>
		virtual public System.String ChargeDocName
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.ChargeDocName);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.ChargeDocName, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.CONSULT_DOC_ID
		/// </summary>
		virtual public System.String ConsultDocId
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.ConsultDocId);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.ConsultDocId, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.CONSULT_DOC_NAME
		/// </summary>
		virtual public System.String ConsultDocName
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.ConsultDocName);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.ConsultDocName, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.ORDER_DEPT
		/// </summary>
		virtual public System.String OrderDept
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.OrderDept);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.OrderDept, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.ORDER_DATE
		/// </summary>
		virtual public System.String OrderDate
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.OrderDate);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.OrderDate, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.ORDER_NO
		/// </summary>
		virtual public System.String OrderNo
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.OrderNo);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.OrderNo, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.PATIENT_IO
		/// </summary>
		virtual public System.String PatientIo
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.PatientIo);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.PatientIo, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.PATIENT_WARD
		/// </summary>
		virtual public System.String PatientWard
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.PatientWard);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.PatientWard, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.IO_DATE
		/// </summary>
		virtual public System.String IoDate
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.IoDate);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.IoDate, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.PATIENT_BIRTH_DATE
		/// </summary>
		virtual public System.String PatientBirthDate
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.PatientBirthDate);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.PatientBirthDate, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.PATIENT_SEX
		/// </summary>
		virtual public System.String PatientSex
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.PatientSex);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.PatientSex, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.ORDER_DIAG
		/// </summary>
		virtual public System.String OrderDiag
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.OrderDiag);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.OrderDiag, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.PATIENT_BLOOD
		/// </summary>
		virtual public System.String PatientBlood
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.PatientBlood);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.PatientBlood, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.ORDER_CNT
		/// </summary>
		virtual public System.Decimal? OrderCnt
		{
			get
			{
				return base.GetSystemDecimal(HisorderMetadata.ColumnNames.OrderCnt);
			}
			
			set
			{
				base.SetSystemDecimal(HisorderMetadata.ColumnNames.OrderCnt, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.GROUP1
		/// </summary>
		virtual public System.String Group1
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.Group1);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.Group1, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.GROUP2
		/// </summary>
		virtual public System.String Group2
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.Group2);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.Group2, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.GROUP3
		/// </summary>
		virtual public System.String Group3
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.Group3);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.Group3, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.ORDER_COMMENT
		/// </summary>
		virtual public System.String OrderComment
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.OrderComment);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.OrderComment, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.EXTENSION1
		/// </summary>
		virtual public System.String Extension1
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.Extension1);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.Extension1, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.EXTENSION2
		/// </summary>
		virtual public System.String Extension2
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.Extension2);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.Extension2, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.EXTENSION3
		/// </summary>
		virtual public System.String Extension3
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.Extension3);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.Extension3, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.EXTENSION4
		/// </summary>
		virtual public System.String Extension4
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.Extension4);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.Extension4, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.EXTENSION5
		/// </summary>
		virtual public System.String Extension5
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.Extension5);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.Extension5, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.EXTENSION6
		/// </summary>
		virtual public System.String Extension6
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.Extension6);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.Extension6, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.EXTENSION7
		/// </summary>
		virtual public System.String Extension7
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.Extension7);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.Extension7, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.EXTENSION8
		/// </summary>
		virtual public System.String Extension8
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.Extension8);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.Extension8, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.EXTENSION9
		/// </summary>
		virtual public System.String Extension9
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.Extension9);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.Extension9, value);
			}
		}
		
		/// <summary>
		/// Maps to HISORDER.EXTENSION10
		/// </summary>
		virtual public System.String Extension10
		{
			get
			{
				return base.GetSystemString(HisorderMetadata.ColumnNames.Extension10);
			}
			
			set
			{
				base.SetSystemString(HisorderMetadata.ColumnNames.Extension10, value);
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
			public esStrings(esHisorder entity)
			{
				this.entity = entity;
			}
			
	
			public System.String OrderKey
			{
				get
				{
					System.Decimal? data = entity.OrderKey;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderKey = null;
					else entity.OrderKey = Convert.ToDecimal(value);
				}
			}
				
			public System.String CreateDate
			{
				get
				{
					System.String data = entity.CreateDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateDate = null;
					else entity.CreateDate = Convert.ToString(value);
				}
			}
				
			public System.String Flag
			{
				get
				{
					System.String data = entity.Flag;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Flag = null;
					else entity.Flag = Convert.ToString(value);
				}
			}
				
			public System.String PatientId
			{
				get
				{
					System.String data = entity.PatientId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientId = null;
					else entity.PatientId = Convert.ToString(value);
				}
			}
				
			public System.String PatientName
			{
				get
				{
					System.String data = entity.PatientName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientName = null;
					else entity.PatientName = Convert.ToString(value);
				}
			}
				
			public System.String PatientUid
			{
				get
				{
					System.String data = entity.PatientUid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientUid = null;
					else entity.PatientUid = Convert.ToString(value);
				}
			}
				
			public System.String LastName
			{
				get
				{
					System.String data = entity.LastName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastName = null;
					else entity.LastName = Convert.ToString(value);
				}
			}
				
			public System.String FirstName
			{
				get
				{
					System.String data = entity.FirstName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FirstName = null;
					else entity.FirstName = Convert.ToString(value);
				}
			}
				
			public System.String MiddleName
			{
				get
				{
					System.String data = entity.MiddleName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MiddleName = null;
					else entity.MiddleName = Convert.ToString(value);
				}
			}
				
			public System.String Aetitle
			{
				get
				{
					System.String data = entity.Aetitle;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Aetitle = null;
					else entity.Aetitle = Convert.ToString(value);
				}
			}
				
			public System.String StudyReservDate
			{
				get
				{
					System.String data = entity.StudyReservDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StudyReservDate = null;
					else entity.StudyReservDate = Convert.ToString(value);
				}
			}
				
			public System.String OrderModality
			{
				get
				{
					System.String data = entity.OrderModality;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderModality = null;
					else entity.OrderModality = Convert.ToString(value);
				}
			}
				
			public System.String AccessionNum
			{
				get
				{
					System.String data = entity.AccessionNum;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AccessionNum = null;
					else entity.AccessionNum = Convert.ToString(value);
				}
			}
				
			public System.String OrderStatus
			{
				get
				{
					System.String data = entity.OrderStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderStatus = null;
					else entity.OrderStatus = Convert.ToString(value);
				}
			}
				
			public System.String OrderCode
			{
				get
				{
					System.String data = entity.OrderCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderCode = null;
					else entity.OrderCode = Convert.ToString(value);
				}
			}
				
			public System.String OrderName
			{
				get
				{
					System.String data = entity.OrderName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderName = null;
					else entity.OrderName = Convert.ToString(value);
				}
			}
				
			public System.String OrderReason
			{
				get
				{
					System.String data = entity.OrderReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderReason = null;
					else entity.OrderReason = Convert.ToString(value);
				}
			}
				
			public System.String StudyRemark
			{
				get
				{
					System.String data = entity.StudyRemark;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StudyRemark = null;
					else entity.StudyRemark = Convert.ToString(value);
				}
			}
				
			public System.String OrderBodypart
			{
				get
				{
					System.String data = entity.OrderBodypart;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderBodypart = null;
					else entity.OrderBodypart = Convert.ToString(value);
				}
			}
				
			public System.String ChargeDocId
			{
				get
				{
					System.String data = entity.ChargeDocId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChargeDocId = null;
					else entity.ChargeDocId = Convert.ToString(value);
				}
			}
				
			public System.String ChargeDocName
			{
				get
				{
					System.String data = entity.ChargeDocName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChargeDocName = null;
					else entity.ChargeDocName = Convert.ToString(value);
				}
			}
				
			public System.String ConsultDocId
			{
				get
				{
					System.String data = entity.ConsultDocId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConsultDocId = null;
					else entity.ConsultDocId = Convert.ToString(value);
				}
			}
				
			public System.String ConsultDocName
			{
				get
				{
					System.String data = entity.ConsultDocName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConsultDocName = null;
					else entity.ConsultDocName = Convert.ToString(value);
				}
			}
				
			public System.String OrderDept
			{
				get
				{
					System.String data = entity.OrderDept;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderDept = null;
					else entity.OrderDept = Convert.ToString(value);
				}
			}
				
			public System.String OrderDate
			{
				get
				{
					System.String data = entity.OrderDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderDate = null;
					else entity.OrderDate = Convert.ToString(value);
				}
			}
				
			public System.String OrderNo
			{
				get
				{
					System.String data = entity.OrderNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderNo = null;
					else entity.OrderNo = Convert.ToString(value);
				}
			}
				
			public System.String PatientIo
			{
				get
				{
					System.String data = entity.PatientIo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientIo = null;
					else entity.PatientIo = Convert.ToString(value);
				}
			}
				
			public System.String PatientWard
			{
				get
				{
					System.String data = entity.PatientWard;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientWard = null;
					else entity.PatientWard = Convert.ToString(value);
				}
			}
				
			public System.String IoDate
			{
				get
				{
					System.String data = entity.IoDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IoDate = null;
					else entity.IoDate = Convert.ToString(value);
				}
			}
				
			public System.String PatientBirthDate
			{
				get
				{
					System.String data = entity.PatientBirthDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientBirthDate = null;
					else entity.PatientBirthDate = Convert.ToString(value);
				}
			}
				
			public System.String PatientSex
			{
				get
				{
					System.String data = entity.PatientSex;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientSex = null;
					else entity.PatientSex = Convert.ToString(value);
				}
			}
				
			public System.String OrderDiag
			{
				get
				{
					System.String data = entity.OrderDiag;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderDiag = null;
					else entity.OrderDiag = Convert.ToString(value);
				}
			}
				
			public System.String PatientBlood
			{
				get
				{
					System.String data = entity.PatientBlood;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientBlood = null;
					else entity.PatientBlood = Convert.ToString(value);
				}
			}
				
			public System.String OrderCnt
			{
				get
				{
					System.Decimal? data = entity.OrderCnt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderCnt = null;
					else entity.OrderCnt = Convert.ToDecimal(value);
				}
			}
				
			public System.String Group1
			{
				get
				{
					System.String data = entity.Group1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Group1 = null;
					else entity.Group1 = Convert.ToString(value);
				}
			}
				
			public System.String Group2
			{
				get
				{
					System.String data = entity.Group2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Group2 = null;
					else entity.Group2 = Convert.ToString(value);
				}
			}
				
			public System.String Group3
			{
				get
				{
					System.String data = entity.Group3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Group3 = null;
					else entity.Group3 = Convert.ToString(value);
				}
			}
				
			public System.String OrderComment
			{
				get
				{
					System.String data = entity.OrderComment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderComment = null;
					else entity.OrderComment = Convert.ToString(value);
				}
			}
				
			public System.String Extension1
			{
				get
				{
					System.String data = entity.Extension1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Extension1 = null;
					else entity.Extension1 = Convert.ToString(value);
				}
			}
				
			public System.String Extension2
			{
				get
				{
					System.String data = entity.Extension2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Extension2 = null;
					else entity.Extension2 = Convert.ToString(value);
				}
			}
				
			public System.String Extension3
			{
				get
				{
					System.String data = entity.Extension3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Extension3 = null;
					else entity.Extension3 = Convert.ToString(value);
				}
			}
				
			public System.String Extension4
			{
				get
				{
					System.String data = entity.Extension4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Extension4 = null;
					else entity.Extension4 = Convert.ToString(value);
				}
			}
				
			public System.String Extension5
			{
				get
				{
					System.String data = entity.Extension5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Extension5 = null;
					else entity.Extension5 = Convert.ToString(value);
				}
			}
				
			public System.String Extension6
			{
				get
				{
					System.String data = entity.Extension6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Extension6 = null;
					else entity.Extension6 = Convert.ToString(value);
				}
			}
				
			public System.String Extension7
			{
				get
				{
					System.String data = entity.Extension7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Extension7 = null;
					else entity.Extension7 = Convert.ToString(value);
				}
			}
				
			public System.String Extension8
			{
				get
				{
					System.String data = entity.Extension8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Extension8 = null;
					else entity.Extension8 = Convert.ToString(value);
				}
			}
				
			public System.String Extension9
			{
				get
				{
					System.String data = entity.Extension9;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Extension9 = null;
					else entity.Extension9 = Convert.ToString(value);
				}
			}
				
			public System.String Extension10
			{
				get
				{
					System.String data = entity.Extension10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Extension10 = null;
					else entity.Extension10 = Convert.ToString(value);
				}
			}
			

			private esHisorder entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esHisorderQuery query)
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
				throw new Exception("esHisorder can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esHisorderQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return HisorderMetadata.Meta();
			}
		}	
		

		public esQueryItem OrderKey
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.OrderKey, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CreateDate
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.CreateDate, esSystemType.String);
			}
		} 
		
		public esQueryItem Flag
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.Flag, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientId
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.PatientId, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientName
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.PatientName, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientUid
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.PatientUid, esSystemType.String);
			}
		} 
		
		public esQueryItem LastName
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.LastName, esSystemType.String);
			}
		} 
		
		public esQueryItem FirstName
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.FirstName, esSystemType.String);
			}
		} 
		
		public esQueryItem MiddleName
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.MiddleName, esSystemType.String);
			}
		} 
		
		public esQueryItem Aetitle
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.Aetitle, esSystemType.String);
			}
		} 
		
		public esQueryItem StudyReservDate
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.StudyReservDate, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderModality
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.OrderModality, esSystemType.String);
			}
		} 
		
		public esQueryItem AccessionNum
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.AccessionNum, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderStatus
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.OrderStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderCode
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.OrderCode, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderName
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.OrderName, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderReason
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.OrderReason, esSystemType.String);
			}
		} 
		
		public esQueryItem StudyRemark
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.StudyRemark, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderBodypart
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.OrderBodypart, esSystemType.String);
			}
		} 
		
		public esQueryItem ChargeDocId
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.ChargeDocId, esSystemType.String);
			}
		} 
		
		public esQueryItem ChargeDocName
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.ChargeDocName, esSystemType.String);
			}
		} 
		
		public esQueryItem ConsultDocId
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.ConsultDocId, esSystemType.String);
			}
		} 
		
		public esQueryItem ConsultDocName
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.ConsultDocName, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderDept
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.OrderDept, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderDate
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.OrderDate, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderNo
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.OrderNo, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientIo
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.PatientIo, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientWard
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.PatientWard, esSystemType.String);
			}
		} 
		
		public esQueryItem IoDate
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.IoDate, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientBirthDate
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.PatientBirthDate, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientSex
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.PatientSex, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderDiag
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.OrderDiag, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientBlood
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.PatientBlood, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderCnt
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.OrderCnt, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Group1
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.Group1, esSystemType.String);
			}
		} 
		
		public esQueryItem Group2
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.Group2, esSystemType.String);
			}
		} 
		
		public esQueryItem Group3
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.Group3, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderComment
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.OrderComment, esSystemType.String);
			}
		} 
		
		public esQueryItem Extension1
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.Extension1, esSystemType.String);
			}
		} 
		
		public esQueryItem Extension2
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.Extension2, esSystemType.String);
			}
		} 
		
		public esQueryItem Extension3
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.Extension3, esSystemType.String);
			}
		} 
		
		public esQueryItem Extension4
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.Extension4, esSystemType.String);
			}
		} 
		
		public esQueryItem Extension5
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.Extension5, esSystemType.String);
			}
		} 
		
		public esQueryItem Extension6
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.Extension6, esSystemType.String);
			}
		} 
		
		public esQueryItem Extension7
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.Extension7, esSystemType.String);
			}
		} 
		
		public esQueryItem Extension8
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.Extension8, esSystemType.String);
			}
		} 
		
		public esQueryItem Extension9
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.Extension9, esSystemType.String);
			}
		} 
		
		public esQueryItem Extension10
		{
			get
			{
				return new esQueryItem(this, HisorderMetadata.ColumnNames.Extension10, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("HisorderCollection")]
	public partial class HisorderCollection : esHisorderCollection, IEnumerable<Hisorder>
	{
		public HisorderCollection()
		{

		}
		
		public static implicit operator List<Hisorder>(HisorderCollection coll)
		{
			List<Hisorder> list = new List<Hisorder>();
			
			foreach (Hisorder emp in coll)
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
				return  HisorderMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HisorderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Hisorder(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Hisorder();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public HisorderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HisorderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(HisorderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Hisorder AddNew()
		{
			Hisorder entity = base.AddNewEntity() as Hisorder;
			
			return entity;
		}

		public Hisorder FindByPrimaryKey(System.Decimal orderKey)
		{
			return base.FindByPrimaryKey(orderKey) as Hisorder;
		}


		#region IEnumerable<Hisorder> Members

		IEnumerator<Hisorder> IEnumerable<Hisorder>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Hisorder;
			}
		}

		#endregion
		
		private HisorderQuery query;
	}


	/// <summary>
	/// Encapsulates the 'HISORDER' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Hisorder ({OrderKey})")]
	[Serializable]
	public partial class Hisorder : esHisorder
	{
		public Hisorder()
		{

		}
	
		public Hisorder(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return HisorderMetadata.Meta();
			}
		}
		
		
		
		override protected esHisorderQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HisorderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public HisorderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HisorderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(HisorderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private HisorderQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class HisorderQuery : esHisorderQuery
	{
		public HisorderQuery()
		{

		}		
		
		public HisorderQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "HisorderQuery";
        }
		
			
	}


	[Serializable]
	public partial class HisorderMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected HisorderMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(HisorderMetadata.ColumnNames.OrderKey, 0, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = HisorderMetadata.PropertyNames.OrderKey;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 38;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.CreateDate, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.CreateDate;
			c.CharacterMaxLength = 14;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.Flag, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.Flag;
			c.CharacterMaxLength = 14;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.PatientId, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.PatientId;
			c.CharacterMaxLength = 64;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.PatientName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.PatientName;
			c.CharacterMaxLength = 256;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.PatientUid, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.PatientUid;
			c.CharacterMaxLength = 64;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.LastName, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.LastName;
			c.CharacterMaxLength = 32;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.FirstName, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.FirstName;
			c.CharacterMaxLength = 32;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.MiddleName, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.MiddleName;
			c.CharacterMaxLength = 32;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.Aetitle, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.Aetitle;
			c.CharacterMaxLength = 32;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.StudyReservDate, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.StudyReservDate;
			c.CharacterMaxLength = 14;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.OrderModality, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.OrderModality;
			c.CharacterMaxLength = 8;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.AccessionNum, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.AccessionNum;
			c.CharacterMaxLength = 32;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.OrderStatus, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.OrderStatus;
			c.CharacterMaxLength = 32;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.OrderCode, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.OrderCode;
			c.CharacterMaxLength = 32;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.OrderName, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.OrderName;
			c.CharacterMaxLength = 64;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.OrderReason, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.OrderReason;
			c.CharacterMaxLength = 256;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.StudyRemark, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.StudyRemark;
			c.CharacterMaxLength = 2048;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.OrderBodypart, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.OrderBodypart;
			c.CharacterMaxLength = 32;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.ChargeDocId, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.ChargeDocId;
			c.CharacterMaxLength = 32;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.ChargeDocName, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.ChargeDocName;
			c.CharacterMaxLength = 64;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.ConsultDocId, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.ConsultDocId;
			c.CharacterMaxLength = 32;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.ConsultDocName, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.ConsultDocName;
			c.CharacterMaxLength = 64;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.OrderDept, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.OrderDept;
			c.CharacterMaxLength = 64;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.OrderDate, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.OrderDate;
			c.CharacterMaxLength = 14;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.OrderNo, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.OrderNo;
			c.CharacterMaxLength = 32;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.PatientIo, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.PatientIo;
			c.CharacterMaxLength = 1;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.PatientWard, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.PatientWard;
			c.CharacterMaxLength = 64;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.IoDate, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.IoDate;
			c.CharacterMaxLength = 14;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.PatientBirthDate, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.PatientBirthDate;
			c.CharacterMaxLength = 8;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.PatientSex, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.PatientSex;
			c.CharacterMaxLength = 1;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.OrderDiag, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.OrderDiag;
			c.CharacterMaxLength = 2048;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.PatientBlood, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.PatientBlood;
			c.CharacterMaxLength = 8;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.OrderCnt, 33, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = HisorderMetadata.PropertyNames.OrderCnt;
			c.NumericPrecision = 38;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.Group1, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.Group1;
			c.CharacterMaxLength = 32;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.Group2, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.Group2;
			c.CharacterMaxLength = 32;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.Group3, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.Group3;
			c.CharacterMaxLength = 32;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.OrderComment, 37, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.OrderComment;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.Extension1, 38, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.Extension1;
			c.CharacterMaxLength = 1024;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.Extension2, 39, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.Extension2;
			c.CharacterMaxLength = 1024;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.Extension3, 40, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.Extension3;
			c.CharacterMaxLength = 1024;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.Extension4, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.Extension4;
			c.CharacterMaxLength = 1024;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.Extension5, 42, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.Extension5;
			c.CharacterMaxLength = 1024;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.Extension6, 43, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.Extension6;
			c.CharacterMaxLength = 1024;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.Extension7, 44, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.Extension7;
			c.CharacterMaxLength = 1024;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.Extension8, 45, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.Extension8;
			c.CharacterMaxLength = 1024;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.Extension9, 46, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.Extension9;
			c.CharacterMaxLength = 1024;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HisorderMetadata.ColumnNames.Extension10, 47, typeof(System.String), esSystemType.String);
			c.PropertyName = HisorderMetadata.PropertyNames.Extension10;
			c.CharacterMaxLength = 1024;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public HisorderMetadata Meta()
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
			 public const string OrderKey = "ORDER_KEY";
			 public const string CreateDate = "CREATE_DATE";
			 public const string Flag = "FLAG";
			 public const string PatientId = "PATIENT_ID";
			 public const string PatientName = "PATIENT_NAME";
			 public const string PatientUid = "PATIENT_UID";
			 public const string LastName = "LAST_NAME";
			 public const string FirstName = "FIRST_NAME";
			 public const string MiddleName = "MIDDLE_NAME";
			 public const string Aetitle = "AETITLE";
			 public const string StudyReservDate = "STUDY_RESERV_DATE";
			 public const string OrderModality = "ORDER_MODALITY";
			 public const string AccessionNum = "ACCESSION_NUM";
			 public const string OrderStatus = "ORDER_STATUS";
			 public const string OrderCode = "ORDER_CODE";
			 public const string OrderName = "ORDER_NAME";
			 public const string OrderReason = "ORDER_REASON";
			 public const string StudyRemark = "STUDY_REMARK";
			 public const string OrderBodypart = "ORDER_BODYPART";
			 public const string ChargeDocId = "CHARGE_DOC_ID";
			 public const string ChargeDocName = "CHARGE_DOC_NAME";
			 public const string ConsultDocId = "CONSULT_DOC_ID";
			 public const string ConsultDocName = "CONSULT_DOC_NAME";
			 public const string OrderDept = "ORDER_DEPT";
			 public const string OrderDate = "ORDER_DATE";
			 public const string OrderNo = "ORDER_NO";
			 public const string PatientIo = "PATIENT_IO";
			 public const string PatientWard = "PATIENT_WARD";
			 public const string IoDate = "IO_DATE";
			 public const string PatientBirthDate = "PATIENT_BIRTH_DATE";
			 public const string PatientSex = "PATIENT_SEX";
			 public const string OrderDiag = "ORDER_DIAG";
			 public const string PatientBlood = "PATIENT_BLOOD";
			 public const string OrderCnt = "ORDER_CNT";
			 public const string Group1 = "GROUP1";
			 public const string Group2 = "GROUP2";
			 public const string Group3 = "GROUP3";
			 public const string OrderComment = "ORDER_COMMENT";
			 public const string Extension1 = "EXTENSION1";
			 public const string Extension2 = "EXTENSION2";
			 public const string Extension3 = "EXTENSION3";
			 public const string Extension4 = "EXTENSION4";
			 public const string Extension5 = "EXTENSION5";
			 public const string Extension6 = "EXTENSION6";
			 public const string Extension7 = "EXTENSION7";
			 public const string Extension8 = "EXTENSION8";
			 public const string Extension9 = "EXTENSION9";
			 public const string Extension10 = "EXTENSION10";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string OrderKey = "OrderKey";
			 public const string CreateDate = "CreateDate";
			 public const string Flag = "Flag";
			 public const string PatientId = "PatientId";
			 public const string PatientName = "PatientName";
			 public const string PatientUid = "PatientUid";
			 public const string LastName = "LastName";
			 public const string FirstName = "FirstName";
			 public const string MiddleName = "MiddleName";
			 public const string Aetitle = "Aetitle";
			 public const string StudyReservDate = "StudyReservDate";
			 public const string OrderModality = "OrderModality";
			 public const string AccessionNum = "AccessionNum";
			 public const string OrderStatus = "OrderStatus";
			 public const string OrderCode = "OrderCode";
			 public const string OrderName = "OrderName";
			 public const string OrderReason = "OrderReason";
			 public const string StudyRemark = "StudyRemark";
			 public const string OrderBodypart = "OrderBodypart";
			 public const string ChargeDocId = "ChargeDocId";
			 public const string ChargeDocName = "ChargeDocName";
			 public const string ConsultDocId = "ConsultDocId";
			 public const string ConsultDocName = "ConsultDocName";
			 public const string OrderDept = "OrderDept";
			 public const string OrderDate = "OrderDate";
			 public const string OrderNo = "OrderNo";
			 public const string PatientIo = "PatientIo";
			 public const string PatientWard = "PatientWard";
			 public const string IoDate = "IoDate";
			 public const string PatientBirthDate = "PatientBirthDate";
			 public const string PatientSex = "PatientSex";
			 public const string OrderDiag = "OrderDiag";
			 public const string PatientBlood = "PatientBlood";
			 public const string OrderCnt = "OrderCnt";
			 public const string Group1 = "Group1";
			 public const string Group2 = "Group2";
			 public const string Group3 = "Group3";
			 public const string OrderComment = "OrderComment";
			 public const string Extension1 = "Extension1";
			 public const string Extension2 = "Extension2";
			 public const string Extension3 = "Extension3";
			 public const string Extension4 = "Extension4";
			 public const string Extension5 = "Extension5";
			 public const string Extension6 = "Extension6";
			 public const string Extension7 = "Extension7";
			 public const string Extension8 = "Extension8";
			 public const string Extension9 = "Extension9";
			 public const string Extension10 = "Extension10";
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
			lock (typeof(HisorderMetadata))
			{
				if(HisorderMetadata.mapDelegates == null)
				{
					HisorderMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (HisorderMetadata.meta == null)
				{
					HisorderMetadata.meta = new HisorderMetadata();
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
				

				meta.AddTypeMap("OrderKey", new esTypeMap("NUMBER", "System.Decimal"));
				meta.AddTypeMap("CreateDate", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("Flag", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("PatientId", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("PatientName", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("PatientUid", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("LastName", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("FirstName", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("MiddleName", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("Aetitle", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("StudyReservDate", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("OrderModality", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("AccessionNum", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("OrderStatus", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("OrderCode", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("OrderName", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("OrderReason", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("StudyRemark", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("OrderBodypart", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("ChargeDocId", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("ChargeDocName", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("ConsultDocId", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("ConsultDocName", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("OrderDept", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("OrderDate", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("OrderNo", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("PatientIo", new esTypeMap("CHAR", "System.String"));
				meta.AddTypeMap("PatientWard", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("IoDate", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("PatientBirthDate", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("PatientSex", new esTypeMap("CHAR", "System.String"));
				meta.AddTypeMap("OrderDiag", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("PatientBlood", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("OrderCnt", new esTypeMap("NUMBER", "System.Decimal"));
				meta.AddTypeMap("Group1", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("Group2", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("Group3", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("OrderComment", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("Extension1", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("Extension2", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("Extension3", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("Extension4", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("Extension5", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("Extension6", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("Extension7", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("Extension8", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("Extension9", new esTypeMap("VARCHAR2", "System.String"));
				meta.AddTypeMap("Extension10", new esTypeMap("VARCHAR2", "System.String"));			
				
				
				
				meta.Source = "HISORDER";
				meta.Destination = "HISORDER";
				
				meta.spInsert = "proc_HISORDERInsert";				
				meta.spUpdate = "proc_HISORDERUpdate";		
				meta.spDelete = "proc_HISORDERDelete";
				meta.spLoadAll = "proc_HISORDERLoadAll";
				meta.spLoadByPrimaryKey = "proc_HISORDERLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private HisorderMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
