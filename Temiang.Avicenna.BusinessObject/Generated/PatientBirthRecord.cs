/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/19/19 9:43:52 AM
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
	abstract public class esPatientBirthRecordCollection : esEntityCollectionWAuditLog
	{
		public esPatientBirthRecordCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "PatientBirthRecordCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esPatientBirthRecordQuery query)
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
			this.InitQuery(query as esPatientBirthRecordQuery);
		}
		#endregion
			
		virtual public PatientBirthRecord DetachEntity(PatientBirthRecord entity)
		{
			return base.DetachEntity(entity) as PatientBirthRecord;
		}
		
		virtual public PatientBirthRecord AttachEntity(PatientBirthRecord entity)
		{
			return base.AttachEntity(entity) as PatientBirthRecord;
		}
		
		virtual public void Combine(PatientBirthRecordCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PatientBirthRecord this[int index]
		{
			get
			{
				return base[index] as PatientBirthRecord;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientBirthRecord);
		}
	}

	[Serializable]
	abstract public class esPatientBirthRecord : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientBirthRecordQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esPatientBirthRecord()
		{
		}
	
		public esPatientBirthRecord(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String patientID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientID);
			else
				return LoadByPrimaryKeyStoredProcedure(patientID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String patientID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientID);
			else
				return LoadByPrimaryKeyStoredProcedure(patientID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String patientID)
		{
			esPatientBirthRecordQuery query = this.GetDynamicQuery();
			query.Where(query.PatientID == patientID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String patientID)
		{
			esParameters parms = new esParameters();
			parms.Add("PatientID",patientID);
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
						case "PatientID": this.str.PatientID = (string)value; break;
						case "MotherMedicalNo": this.str.MotherMedicalNo = (string)value; break;
						case "MotherRegistrationNo": this.str.MotherRegistrationNo = (string)value; break;
						case "TimeOfBirth": this.str.TimeOfBirth = (string)value; break;
						case "SRBornAt": this.str.SRBornAt = (string)value; break;
						case "BornAtDescription": this.str.BornAtDescription = (string)value; break;
						case "SRSingleTwin": this.str.SRSingleTwin = (string)value; break;
						case "TwinNo": this.str.TwinNo = (string)value; break;
						case "SRBirthMethod": this.str.SRBirthMethod = (string)value; break;
						case "SRCaesarMethod": this.str.SRCaesarMethod = (string)value; break;
						case "SRBornCondition": this.str.SRBornCondition = (string)value; break;
						case "SRBirthComplication": this.str.SRBirthComplication = (string)value; break;
						case "SRDeathCondition": this.str.SRDeathCondition = (string)value; break;
						case "SRBirthIndication": this.str.SRBirthIndication = (string)value; break;
						case "BirthPregnancyAge": this.str.BirthPregnancyAge = (string)value; break;
						case "Length": this.str.Length = (string)value; break;
						case "Weight": this.str.Weight = (string)value; break;
						case "ApgarScore1": this.str.ApgarScore1 = (string)value; break;
						case "ApgarScore2": this.str.ApgarScore2 = (string)value; break;
						case "ApgarScore3": this.str.ApgarScore3 = (string)value; break;
						case "HeadCircumference": this.str.HeadCircumference = (string)value; break;
						case "ChestCircumference": this.str.ChestCircumference = (string)value; break;
						case "CertificateNo": this.str.CertificateNo = (string)value; break;
						case "FatherName": this.str.FatherName = (string)value; break;
						case "FatherSsn": this.str.FatherSsn = (string)value; break;
						case "FatherBirthOfDate": this.str.FatherBirthOfDate = (string)value; break;
						case "StreetName": this.str.StreetName = (string)value; break;
						case "District": this.str.District = (string)value; break;
						case "City": this.str.City = (string)value; break;
						case "County": this.str.County = (string)value; break;
						case "State": this.str.State = (string)value; break;
						case "ZipCode": this.str.ZipCode = (string)value; break;
						case "SROccupation": this.str.SROccupation = (string)value; break;
						case "PhoneNo": this.str.PhoneNo = (string)value; break;
						case "FaxNo": this.str.FaxNo = (string)value; break;
						case "MobilePhoneNo": this.str.MobilePhoneNo = (string)value; break;
						case "Email": this.str.Email = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "BirthMethod": this.str.BirthMethod = (string)value; break;
						case "BirthMethodScIndication": this.str.BirthMethodScIndication = (string)value; break;
						case "ChildNumber": this.str.ChildNumber = (string)value; break;
						case "ChildNumberFrom": this.str.ChildNumberFrom = (string)value; break;
						case "AsiToMonthAge": this.str.AsiToMonthAge = (string)value; break;
						case "CurrentDiet": this.str.CurrentDiet = (string)value; break;
						case "ProneAtMonthAge": this.str.ProneAtMonthAge = (string)value; break;
						case "SitAtMonthAge": this.str.SitAtMonthAge = (string)value; break;
						case "CrawlAtMonthAge": this.str.CrawlAtMonthAge = (string)value; break;
						case "StandUpAtMonthAge": this.str.StandUpAtMonthAge = (string)value; break;
						case "WalkAtMonthAge": this.str.WalkAtMonthAge = (string)value; break;
						case "Speak3WordAtMonthAge": this.str.Speak3WordAtMonthAge = (string)value; break;
						case "Speak2SentAtMonthAge": this.str.Speak2SentAtMonthAge = (string)value; break;
						case "SchoolClass": this.str.SchoolClass = (string)value; break;
						case "SchoolAchievement": this.str.SchoolAchievement = (string)value; break;
						case "GrowthNotes": this.str.GrowthNotes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "FormulaMilkStartAge": this.str.FormulaMilkStartAge = (string)value; break;
						case "AddFoodStartAge": this.str.AddFoodStartAge = (string)value; break;
						case "RaiseHead": this.str.RaiseHead = (string)value; break;
						case "Grabbing": this.str.Grabbing = (string)value; break;
						case "Holding": this.str.Holding = (string)value; break;
						case "PregnanDurationMonth": this.str.PregnanDurationMonth = (string)value; break;
						case "PregnanDurationWeek": this.str.PregnanDurationWeek = (string)value; break;
						
						//Tambahan Untuk CR RSJKT
						case "Smile": this.str.Smile = (string)value; break;
						case "Cooing": this.str.Cooing = (string)value; break;
						case "RollToTummy": this.str.RollToTummy = (string)value; break;
						case "RollFromTummy": this.str.RollFromTummy = (string)value; break;
						case "Babbling": this.str.Babbling = (string)value; break;

                        //Tambahan
                        case "TurnOverAtMonthAge": this.str.TurnOverAtMonthAge = (string)value; break;
                        case "SRBirthMethodPhr": this.str.SRBirthMethodPhr = (string)value; break;
                        case "MethodOther": this.str.MethodOther = (string)value; break;
                    }
				}
				else
				{
					switch (name)
					{	
						case "BirthPregnancyAge":
						
							if (value == null || value is System.Decimal)
								this.BirthPregnancyAge = (System.Decimal?)value;
							break;
						case "Length":
						
							if (value == null || value is System.Decimal)
								this.Length = (System.Decimal?)value;
							break;
						case "Weight":
						
							if (value == null || value is System.Decimal)
								this.Weight = (System.Decimal?)value;
							break;
						case "ApgarScore1":
						
							if (value == null || value is System.Decimal)
								this.ApgarScore1 = (System.Decimal?)value;
							break;
						case "ApgarScore2":
						
							if (value == null || value is System.Decimal)
								this.ApgarScore2 = (System.Decimal?)value;
							break;
						case "ApgarScore3":
						
							if (value == null || value is System.Decimal)
								this.ApgarScore3 = (System.Decimal?)value;
							break;
						case "HeadCircumference":
						
							if (value == null || value is System.Decimal)
								this.HeadCircumference = (System.Decimal?)value;
							break;
						case "ChestCircumference":
						
							if (value == null || value is System.Decimal)
								this.ChestCircumference = (System.Decimal?)value;
							break;
						case "FatherBirthOfDate":
						
							if (value == null || value is System.DateTime)
								this.FatherBirthOfDate = (System.DateTime?)value;
							break;
						case "ChildNumber":
						
							if (value == null || value is System.Int32)
								this.ChildNumber = (System.Int32?)value;
							break;
						case "ChildNumberFrom":
						
							if (value == null || value is System.Int32)
								this.ChildNumberFrom = (System.Int32?)value;
							break;
						case "AsiToMonthAge":
						
							if (value == null || value is System.Int32)
								this.AsiToMonthAge = (System.Int32?)value;
							break;
						case "ProneAtMonthAge":
						
							if (value == null || value is System.Int32)
								this.ProneAtMonthAge = (System.Int32?)value;
							break;
						case "SitAtMonthAge":
						
							if (value == null || value is System.Int32)
								this.SitAtMonthAge = (System.Int32?)value;
							break;
						case "CrawlAtMonthAge":
						
							if (value == null || value is System.Int32)
								this.CrawlAtMonthAge = (System.Int32?)value;
							break;
						case "StandUpAtMonthAge":
						
							if (value == null || value is System.Int32)
								this.StandUpAtMonthAge = (System.Int32?)value;
							break;
						case "WalkAtMonthAge":
						
							if (value == null || value is System.Int32)
								this.WalkAtMonthAge = (System.Int32?)value;
							break;
						case "Speak3WordAtMonthAge":
						
							if (value == null || value is System.Int32)
								this.Speak3WordAtMonthAge = (System.Int32?)value;
							break;
						case "Speak2SentAtMonthAge":
						
							if (value == null || value is System.Int32)
								this.Speak2SentAtMonthAge = (System.Int32?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "FormulaMilkStartAge":
						
							if (value == null || value is System.Int32)
								this.FormulaMilkStartAge = (System.Int32?)value;
							break;
						case "AddFoodStartAge":
						
							if (value == null || value is System.Int32)
								this.AddFoodStartAge = (System.Int32?)value;
							break;
						case "RaiseHead":
						
							if (value == null || value is System.Int32)
								this.RaiseHead = (System.Int32?)value;
							break;
						case "Grabbing":
						
							if (value == null || value is System.Int32)
								this.Grabbing = (System.Int32?)value;
							break;
						case "Holding":
						
							if (value == null || value is System.Int32)
								this.Holding = (System.Int32?)value;
							break;
						case "PregnanDurationMonth":
						
							if (value == null || value is System.Int32)
								this.PregnanDurationMonth = (System.Int32?)value;
							break;
						case "PregnanDurationWeek":
						
							if (value == null || value is System.Int32)
								this.PregnanDurationWeek = (System.Int32?)value;
							break;

						case "Smile":

							if (value == null || value is System.Int32)
								this.Smile = (System.Int32?)value;
							break;
						case "Cooing":
							if (value == null || value is System.Int32)
								this.Cooing = (System.Int32?)value;
							break;
						case "RollToTummy":
							if (value == null || value is System.Int32)
								this.RollToTummy = (System.Int32?)value;
							break;
						case "RollFromTummy":
							if (value == null || value is System.Int32)
								this.RollFromTummy = (System.Int32?)value;
							break;
						case "Babbling":
							if (value == null || value is System.Int32)
								this.Babbling = (System.Int32?)value;
							break;
                        case "TurnOverAtMonthAge":

                            if (value == null || value is System.Int32)
                                this.TurnOverAtMonthAge = (System.Int32?)value;
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
		/// Maps to PatientBirthRecord.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.PatientID);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.MotherMedicalNo
		/// </summary>
		virtual public System.String MotherMedicalNo
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.MotherMedicalNo);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.MotherMedicalNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.MotherRegistrationNo
		/// </summary>
		virtual public System.String MotherRegistrationNo
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.MotherRegistrationNo);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.MotherRegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.TimeOfBirth
		/// </summary>
		virtual public System.String TimeOfBirth
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.TimeOfBirth);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.TimeOfBirth, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.SRBornAt
		/// </summary>
		virtual public System.String SRBornAt
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.SRBornAt);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.SRBornAt, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.BornAtDescription
		/// </summary>
		virtual public System.String BornAtDescription
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.BornAtDescription);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.BornAtDescription, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.SRSingleTwin
		/// </summary>
		virtual public System.String SRSingleTwin
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.SRSingleTwin);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.SRSingleTwin, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.TwinNo
		/// </summary>
		virtual public System.String TwinNo
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.TwinNo);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.TwinNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.SRBirthMethod
		/// </summary>
		virtual public System.String SRBirthMethod
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.SRBirthMethod);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.SRBirthMethod, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.SRCaesarMethod
		/// </summary>
		virtual public System.String SRCaesarMethod
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.SRCaesarMethod);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.SRCaesarMethod, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.SRBornCondition
		/// </summary>
		virtual public System.String SRBornCondition
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.SRBornCondition);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.SRBornCondition, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.SRBirthComplication
		/// </summary>
		virtual public System.String SRBirthComplication
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.SRBirthComplication);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.SRBirthComplication, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.SRDeathCondition
		/// </summary>
		virtual public System.String SRDeathCondition
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.SRDeathCondition);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.SRDeathCondition, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.SRBirthIndication
		/// </summary>
		virtual public System.String SRBirthIndication
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.SRBirthIndication);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.SRBirthIndication, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.BirthPregnancyAge
		/// </summary>
		virtual public System.Decimal? BirthPregnancyAge
		{
			get
			{
				return base.GetSystemDecimal(PatientBirthRecordMetadata.ColumnNames.BirthPregnancyAge);
			}
			
			set
			{
				base.SetSystemDecimal(PatientBirthRecordMetadata.ColumnNames.BirthPregnancyAge, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.Length
		/// </summary>
		virtual public System.Decimal? Length
		{
			get
			{
				return base.GetSystemDecimal(PatientBirthRecordMetadata.ColumnNames.Length);
			}
			
			set
			{
				base.SetSystemDecimal(PatientBirthRecordMetadata.ColumnNames.Length, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.Weight
		/// </summary>
		virtual public System.Decimal? Weight
		{
			get
			{
				return base.GetSystemDecimal(PatientBirthRecordMetadata.ColumnNames.Weight);
			}
			
			set
			{
				base.SetSystemDecimal(PatientBirthRecordMetadata.ColumnNames.Weight, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.ApgarScore1
		/// </summary>
		virtual public System.Decimal? ApgarScore1
		{
			get
			{
				return base.GetSystemDecimal(PatientBirthRecordMetadata.ColumnNames.ApgarScore1);
			}
			
			set
			{
				base.SetSystemDecimal(PatientBirthRecordMetadata.ColumnNames.ApgarScore1, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.ApgarScore2
		/// </summary>
		virtual public System.Decimal? ApgarScore2
		{
			get
			{
				return base.GetSystemDecimal(PatientBirthRecordMetadata.ColumnNames.ApgarScore2);
			}
			
			set
			{
				base.SetSystemDecimal(PatientBirthRecordMetadata.ColumnNames.ApgarScore2, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.ApgarScore3
		/// </summary>
		virtual public System.Decimal? ApgarScore3
		{
			get
			{
				return base.GetSystemDecimal(PatientBirthRecordMetadata.ColumnNames.ApgarScore3);
			}
			
			set
			{
				base.SetSystemDecimal(PatientBirthRecordMetadata.ColumnNames.ApgarScore3, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.HeadCircumference
		/// </summary>
		virtual public System.Decimal? HeadCircumference
		{
			get
			{
				return base.GetSystemDecimal(PatientBirthRecordMetadata.ColumnNames.HeadCircumference);
			}
			
			set
			{
				base.SetSystemDecimal(PatientBirthRecordMetadata.ColumnNames.HeadCircumference, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.ChestCircumference
		/// </summary>
		virtual public System.Decimal? ChestCircumference
		{
			get
			{
				return base.GetSystemDecimal(PatientBirthRecordMetadata.ColumnNames.ChestCircumference);
			}
			
			set
			{
				base.SetSystemDecimal(PatientBirthRecordMetadata.ColumnNames.ChestCircumference, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.CertificateNo
		/// </summary>
		virtual public System.String CertificateNo
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.CertificateNo);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.CertificateNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.FatherName
		/// </summary>
		virtual public System.String FatherName
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.FatherName);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.FatherName, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.FatherSsn
		/// </summary>
		virtual public System.String FatherSsn
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.FatherSsn);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.FatherSsn, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.FatherBirthOfDate
		/// </summary>
		virtual public System.DateTime? FatherBirthOfDate
		{
			get
			{
				return base.GetSystemDateTime(PatientBirthRecordMetadata.ColumnNames.FatherBirthOfDate);
			}
			
			set
			{
				base.SetSystemDateTime(PatientBirthRecordMetadata.ColumnNames.FatherBirthOfDate, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.StreetName
		/// </summary>
		virtual public System.String StreetName
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.StreetName);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.StreetName, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.District
		/// </summary>
		virtual public System.String District
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.District);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.District, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.City
		/// </summary>
		virtual public System.String City
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.City);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.City, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.County
		/// </summary>
		virtual public System.String County
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.County);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.County, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.State
		/// </summary>
		virtual public System.String State
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.State);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.State, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.ZipCode
		/// </summary>
		virtual public System.String ZipCode
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.ZipCode);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.ZipCode, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.SROccupation
		/// </summary>
		virtual public System.String SROccupation
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.SROccupation);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.SROccupation, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.PhoneNo
		/// </summary>
		virtual public System.String PhoneNo
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.PhoneNo);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.PhoneNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.FaxNo
		/// </summary>
		virtual public System.String FaxNo
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.FaxNo);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.FaxNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.MobilePhoneNo
		/// </summary>
		virtual public System.String MobilePhoneNo
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.MobilePhoneNo);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.MobilePhoneNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.Email
		/// </summary>
		virtual public System.String Email
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.Email);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.Email, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.BirthMethod
		/// </summary>
		virtual public System.String BirthMethod
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.BirthMethod);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.BirthMethod, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.BirthMethodScIndication
		/// </summary>
		virtual public System.String BirthMethodScIndication
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.BirthMethodScIndication);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.BirthMethodScIndication, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.ChildNumber
		/// </summary>
		virtual public System.Int32? ChildNumber
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.ChildNumber);
			}
			
			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.ChildNumber, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.ChildNumberFrom
		/// </summary>
		virtual public System.Int32? ChildNumberFrom
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.ChildNumberFrom);
			}
			
			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.ChildNumberFrom, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.AsiToMonthAge
		/// </summary>
		virtual public System.Int32? AsiToMonthAge
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.AsiToMonthAge);
			}
			
			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.AsiToMonthAge, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.CurrentDiet
		/// </summary>
		virtual public System.String CurrentDiet
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.CurrentDiet);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.CurrentDiet, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.ProneAtMonthAge
		/// </summary>
		virtual public System.Int32? ProneAtMonthAge
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.ProneAtMonthAge);
			}
			
			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.ProneAtMonthAge, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.SitAtMonthAge
		/// </summary>
		virtual public System.Int32? SitAtMonthAge
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.SitAtMonthAge);
			}
			
			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.SitAtMonthAge, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.CrawlAtMonthAge
		/// </summary>
		virtual public System.Int32? CrawlAtMonthAge
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.CrawlAtMonthAge);
			}
			
			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.CrawlAtMonthAge, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.StandUpAtMonthAge
		/// </summary>
		virtual public System.Int32? StandUpAtMonthAge
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.StandUpAtMonthAge);
			}
			
			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.StandUpAtMonthAge, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.WalkAtMonthAge
		/// </summary>
		virtual public System.Int32? WalkAtMonthAge
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.WalkAtMonthAge);
			}
			
			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.WalkAtMonthAge, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.Speak3WordAtMonthAge
		/// </summary>
		virtual public System.Int32? Speak3WordAtMonthAge
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.Speak3WordAtMonthAge);
			}
			
			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.Speak3WordAtMonthAge, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.Speak2SentAtMonthAge
		/// </summary>
		virtual public System.Int32? Speak2SentAtMonthAge
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.Speak2SentAtMonthAge);
			}
			
			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.Speak2SentAtMonthAge, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.SchoolClass
		/// </summary>
		virtual public System.String SchoolClass
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.SchoolClass);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.SchoolClass, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.SchoolAchievement
		/// </summary>
		virtual public System.String SchoolAchievement
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.SchoolAchievement);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.SchoolAchievement, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.GrowthNotes
		/// </summary>
		virtual public System.String GrowthNotes
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.GrowthNotes);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.GrowthNotes, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientBirthRecordMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientBirthRecordMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.FormulaMilkStartAge
		/// </summary>
		virtual public System.Int32? FormulaMilkStartAge
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.FormulaMilkStartAge);
			}
			
			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.FormulaMilkStartAge, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.AddFoodStartAge
		/// </summary>
		virtual public System.Int32? AddFoodStartAge
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.AddFoodStartAge);
			}
			
			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.AddFoodStartAge, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.RaiseHead
		/// </summary>
		virtual public System.Int32? RaiseHead
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.RaiseHead);
			}
			
			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.RaiseHead, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.Grabbing
		/// </summary>
		virtual public System.Int32? Grabbing
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.Grabbing);
			}
			
			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.Grabbing, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.Holding
		/// </summary>
		virtual public System.Int32? Holding
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.Holding);
			}
			
			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.Holding, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.PregnanDurationMonth
		/// </summary>
		virtual public System.Int32? PregnanDurationMonth
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.PregnanDurationMonth);
			}
			
			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.PregnanDurationMonth, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.PregnanDurationWeek
		/// </summary>
		virtual public System.Int32? PregnanDurationWeek
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.PregnanDurationWeek);
			}
			
			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.PregnanDurationWeek, value);
			}
		}

		/// <summary>
		/// Maps to PatientBirthRecord.Smile
		/// </summary>
		virtual public System.Int32? Smile
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.Smile);
			}

			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.Smile, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.Cooing
		/// </summary>
		virtual public System.Int32? Cooing
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.Cooing);
			}

			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.Cooing, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.RollToTummy
		/// </summary>
		virtual public System.Int32? RollToTummy
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.RollToTummy);
			}

			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.RollToTummy, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.RollFromTummy
		/// </summary>
		virtual public System.Int32? RollFromTummy
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.RollFromTummy);
			}

			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.RollFromTummy, value);
			}
		}
		/// <summary>
		/// Maps to PatientBirthRecord.Babbling
		/// </summary>
		virtual public System.Int32? Babbling
		{
			get
			{
				return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.Babbling);
			}

			set
			{
				base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.Babbling, value);
			}
		}
        /// <summary>
        /// Maps to PatientBirthRecord.TurnOverAtMonthAge
        /// </summary>
        virtual public System.Int32? TurnOverAtMonthAge
        {
            get
            {
                return base.GetSystemInt32(PatientBirthRecordMetadata.ColumnNames.TurnOverAtMonthAge);
            }

            set
            {
                base.SetSystemInt32(PatientBirthRecordMetadata.ColumnNames.TurnOverAtMonthAge, value);
            }
        }
        /// <summary>
        /// Maps to PatientBirthRecord.SRBirthMethodPhr
        /// </summary>
        virtual public System.String SRBirthMethodPhr
        {
            get
            {
                return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.SRBirthMethodPhr);
            }

            set
            {
                base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.SRBirthMethodPhr, value);
            }
        }
        /// <summary>
        /// Maps to PatientBirthRecord.MethodOther
        /// </summary>
        virtual public System.String MethodOther
        {
            get
            {
                return base.GetSystemString(PatientBirthRecordMetadata.ColumnNames.MethodOther);
            }

            set
            {
                base.SetSystemString(PatientBirthRecordMetadata.ColumnNames.MethodOther, value);
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
			public esStrings(esPatientBirthRecord entity)
			{
				this.entity = entity;
			}
			public System.String PatientID
			{
				get
				{
					System.String data = entity.PatientID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientID = null;
					else entity.PatientID = Convert.ToString(value);
				}
			}
			public System.String MotherMedicalNo
			{
				get
				{
					System.String data = entity.MotherMedicalNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MotherMedicalNo = null;
					else entity.MotherMedicalNo = Convert.ToString(value);
				}
			}
			public System.String MotherRegistrationNo
			{
				get
				{
					System.String data = entity.MotherRegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MotherRegistrationNo = null;
					else entity.MotherRegistrationNo = Convert.ToString(value);
				}
			}
			public System.String TimeOfBirth
			{
				get
				{
					System.String data = entity.TimeOfBirth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TimeOfBirth = null;
					else entity.TimeOfBirth = Convert.ToString(value);
				}
			}
			public System.String SRBornAt
			{
				get
				{
					System.String data = entity.SRBornAt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBornAt = null;
					else entity.SRBornAt = Convert.ToString(value);
				}
			}
			public System.String BornAtDescription
			{
				get
				{
					System.String data = entity.BornAtDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BornAtDescription = null;
					else entity.BornAtDescription = Convert.ToString(value);
				}
			}
			public System.String SRSingleTwin
			{
				get
				{
					System.String data = entity.SRSingleTwin;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRSingleTwin = null;
					else entity.SRSingleTwin = Convert.ToString(value);
				}
			}
			public System.String TwinNo
			{
				get
				{
					System.String data = entity.TwinNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TwinNo = null;
					else entity.TwinNo = Convert.ToString(value);
				}
			}
			public System.String SRBirthMethod
			{
				get
				{
					System.String data = entity.SRBirthMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBirthMethod = null;
					else entity.SRBirthMethod = Convert.ToString(value);
				}
			}
			public System.String SRCaesarMethod
			{
				get
				{
					System.String data = entity.SRCaesarMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCaesarMethod = null;
					else entity.SRCaesarMethod = Convert.ToString(value);
				}
			}
			public System.String SRBornCondition
			{
				get
				{
					System.String data = entity.SRBornCondition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBornCondition = null;
					else entity.SRBornCondition = Convert.ToString(value);
				}
			}
			public System.String SRBirthComplication
			{
				get
				{
					System.String data = entity.SRBirthComplication;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBirthComplication = null;
					else entity.SRBirthComplication = Convert.ToString(value);
				}
			}
			public System.String SRDeathCondition
			{
				get
				{
					System.String data = entity.SRDeathCondition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDeathCondition = null;
					else entity.SRDeathCondition = Convert.ToString(value);
				}
			}
			public System.String SRBirthIndication
			{
				get
				{
					System.String data = entity.SRBirthIndication;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBirthIndication = null;
					else entity.SRBirthIndication = Convert.ToString(value);
				}
			}
			public System.String BirthPregnancyAge
			{
				get
				{
					System.Decimal? data = entity.BirthPregnancyAge;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BirthPregnancyAge = null;
					else entity.BirthPregnancyAge = Convert.ToDecimal(value);
				}
			}
			public System.String Length
			{
				get
				{
					System.Decimal? data = entity.Length;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Length = null;
					else entity.Length = Convert.ToDecimal(value);
				}
			}
			public System.String Weight
			{
				get
				{
					System.Decimal? data = entity.Weight;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Weight = null;
					else entity.Weight = Convert.ToDecimal(value);
				}
			}
			public System.String ApgarScore1
			{
				get
				{
					System.Decimal? data = entity.ApgarScore1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApgarScore1 = null;
					else entity.ApgarScore1 = Convert.ToDecimal(value);
				}
			}
			public System.String ApgarScore2
			{
				get
				{
					System.Decimal? data = entity.ApgarScore2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApgarScore2 = null;
					else entity.ApgarScore2 = Convert.ToDecimal(value);
				}
			}
			public System.String ApgarScore3
			{
				get
				{
					System.Decimal? data = entity.ApgarScore3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApgarScore3 = null;
					else entity.ApgarScore3 = Convert.ToDecimal(value);
				}
			}
			public System.String HeadCircumference
			{
				get
				{
					System.Decimal? data = entity.HeadCircumference;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HeadCircumference = null;
					else entity.HeadCircumference = Convert.ToDecimal(value);
				}
			}
			public System.String ChestCircumference
			{
				get
				{
					System.Decimal? data = entity.ChestCircumference;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChestCircumference = null;
					else entity.ChestCircumference = Convert.ToDecimal(value);
				}
			}
			public System.String CertificateNo
			{
				get
				{
					System.String data = entity.CertificateNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CertificateNo = null;
					else entity.CertificateNo = Convert.ToString(value);
				}
			}
			public System.String FatherName
			{
				get
				{
					System.String data = entity.FatherName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FatherName = null;
					else entity.FatherName = Convert.ToString(value);
				}
			}
			public System.String FatherSsn
			{
				get
				{
					System.String data = entity.FatherSsn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FatherSsn = null;
					else entity.FatherSsn = Convert.ToString(value);
				}
			}
			public System.String FatherBirthOfDate
			{
				get
				{
					System.DateTime? data = entity.FatherBirthOfDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FatherBirthOfDate = null;
					else entity.FatherBirthOfDate = Convert.ToDateTime(value);
				}
			}
			public System.String StreetName
			{
				get
				{
					System.String data = entity.StreetName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StreetName = null;
					else entity.StreetName = Convert.ToString(value);
				}
			}
			public System.String District
			{
				get
				{
					System.String data = entity.District;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.District = null;
					else entity.District = Convert.ToString(value);
				}
			}
			public System.String City
			{
				get
				{
					System.String data = entity.City;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.City = null;
					else entity.City = Convert.ToString(value);
				}
			}
			public System.String County
			{
				get
				{
					System.String data = entity.County;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.County = null;
					else entity.County = Convert.ToString(value);
				}
			}
			public System.String State
			{
				get
				{
					System.String data = entity.State;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.State = null;
					else entity.State = Convert.ToString(value);
				}
			}
			public System.String ZipCode
			{
				get
				{
					System.String data = entity.ZipCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ZipCode = null;
					else entity.ZipCode = Convert.ToString(value);
				}
			}
			public System.String SROccupation
			{
				get
				{
					System.String data = entity.SROccupation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SROccupation = null;
					else entity.SROccupation = Convert.ToString(value);
				}
			}
			public System.String PhoneNo
			{
				get
				{
					System.String data = entity.PhoneNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhoneNo = null;
					else entity.PhoneNo = Convert.ToString(value);
				}
			}
			public System.String FaxNo
			{
				get
				{
					System.String data = entity.FaxNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FaxNo = null;
					else entity.FaxNo = Convert.ToString(value);
				}
			}
			public System.String MobilePhoneNo
			{
				get
				{
					System.String data = entity.MobilePhoneNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MobilePhoneNo = null;
					else entity.MobilePhoneNo = Convert.ToString(value);
				}
			}
			public System.String Email
			{
				get
				{
					System.String data = entity.Email;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Email = null;
					else entity.Email = Convert.ToString(value);
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
			public System.String BirthMethod
			{
				get
				{
					System.String data = entity.BirthMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BirthMethod = null;
					else entity.BirthMethod = Convert.ToString(value);
				}
			}
			public System.String BirthMethodScIndication
			{
				get
				{
					System.String data = entity.BirthMethodScIndication;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BirthMethodScIndication = null;
					else entity.BirthMethodScIndication = Convert.ToString(value);
				}
			}
			public System.String ChildNumber
			{
				get
				{
					System.Int32? data = entity.ChildNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChildNumber = null;
					else entity.ChildNumber = Convert.ToInt32(value);
				}
			}
			public System.String ChildNumberFrom
			{
				get
				{
					System.Int32? data = entity.ChildNumberFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChildNumberFrom = null;
					else entity.ChildNumberFrom = Convert.ToInt32(value);
				}
			}
			public System.String AsiToMonthAge
			{
				get
				{
					System.Int32? data = entity.AsiToMonthAge;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AsiToMonthAge = null;
					else entity.AsiToMonthAge = Convert.ToInt32(value);
				}
			}
			public System.String CurrentDiet
			{
				get
				{
					System.String data = entity.CurrentDiet;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CurrentDiet = null;
					else entity.CurrentDiet = Convert.ToString(value);
				}
			}
			public System.String ProneAtMonthAge
			{
				get
				{
					System.Int32? data = entity.ProneAtMonthAge;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProneAtMonthAge = null;
					else entity.ProneAtMonthAge = Convert.ToInt32(value);
				}
			}
			public System.String SitAtMonthAge
			{
				get
				{
					System.Int32? data = entity.SitAtMonthAge;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SitAtMonthAge = null;
					else entity.SitAtMonthAge = Convert.ToInt32(value);
				}
			}
			public System.String CrawlAtMonthAge
			{
				get
				{
					System.Int32? data = entity.CrawlAtMonthAge;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CrawlAtMonthAge = null;
					else entity.CrawlAtMonthAge = Convert.ToInt32(value);
				}
			}
			public System.String StandUpAtMonthAge
			{
				get
				{
					System.Int32? data = entity.StandUpAtMonthAge;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StandUpAtMonthAge = null;
					else entity.StandUpAtMonthAge = Convert.ToInt32(value);
				}
			}
			public System.String WalkAtMonthAge
			{
				get
				{
					System.Int32? data = entity.WalkAtMonthAge;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WalkAtMonthAge = null;
					else entity.WalkAtMonthAge = Convert.ToInt32(value);
				}
			}
			public System.String Speak3WordAtMonthAge
			{
				get
				{
					System.Int32? data = entity.Speak3WordAtMonthAge;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Speak3WordAtMonthAge = null;
					else entity.Speak3WordAtMonthAge = Convert.ToInt32(value);
				}
			}
			public System.String Speak2SentAtMonthAge
			{
				get
				{
					System.Int32? data = entity.Speak2SentAtMonthAge;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Speak2SentAtMonthAge = null;
					else entity.Speak2SentAtMonthAge = Convert.ToInt32(value);
				}
			}
			public System.String SchoolClass
			{
				get
				{
					System.String data = entity.SchoolClass;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SchoolClass = null;
					else entity.SchoolClass = Convert.ToString(value);
				}
			}
			public System.String SchoolAchievement
			{
				get
				{
					System.String data = entity.SchoolAchievement;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SchoolAchievement = null;
					else entity.SchoolAchievement = Convert.ToString(value);
				}
			}
			public System.String GrowthNotes
			{
				get
				{
					System.String data = entity.GrowthNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GrowthNotes = null;
					else entity.GrowthNotes = Convert.ToString(value);
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
			public System.String FormulaMilkStartAge
			{
				get
				{
					System.Int32? data = entity.FormulaMilkStartAge;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FormulaMilkStartAge = null;
					else entity.FormulaMilkStartAge = Convert.ToInt32(value);
				}
			}
			public System.String AddFoodStartAge
			{
				get
				{
					System.Int32? data = entity.AddFoodStartAge;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AddFoodStartAge = null;
					else entity.AddFoodStartAge = Convert.ToInt32(value);
				}
			}
			public System.String RaiseHead
			{
				get
				{
					System.Int32? data = entity.RaiseHead;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RaiseHead = null;
					else entity.RaiseHead = Convert.ToInt32(value);
				}
			}
			public System.String Grabbing
			{
				get
				{
					System.Int32? data = entity.Grabbing;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Grabbing = null;
					else entity.Grabbing = Convert.ToInt32(value);
				}
			}
			public System.String Holding
			{
				get
				{
					System.Int32? data = entity.Holding;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Holding = null;
					else entity.Holding = Convert.ToInt32(value);
				}
			}
			public System.String PregnanDurationMonth
			{
				get
				{
					System.Int32? data = entity.PregnanDurationMonth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PregnanDurationMonth = null;
					else entity.PregnanDurationMonth = Convert.ToInt32(value);
				}
			}
			public System.String PregnanDurationWeek
			{
				get
				{
					System.Int32? data = entity.PregnanDurationWeek;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PregnanDurationWeek = null;
					else entity.PregnanDurationWeek = Convert.ToInt32(value);
				}
			}
			public System.String Smile
			{
				get
				{
					System.Int32? data = entity.Smile;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Smile = null;
					else entity.Smile = Convert.ToInt32(value);
				}
			}
			public System.String Cooing
			{
				get
				{
					System.Int32? data = entity.Cooing;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Cooing = null;
					else entity.Cooing = Convert.ToInt32(value);
				}
			}
			public System.String RollToTummy
			{
				get
				{
					System.Int32? data = entity.RollToTummy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RollToTummy = null;
					else entity.RollToTummy = Convert.ToInt32(value);
				}
			}
			public System.String RollFromTummy
			{
				get
				{
					System.Int32? data = entity.RollFromTummy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RollFromTummy = null;
					else entity.RollFromTummy = Convert.ToInt32(value);
				}
			}
			public System.String Babbling
			{
				get
				{
					System.Int32? data = entity.Babbling;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Babbling = null;
					else entity.Babbling = Convert.ToInt32(value);
				}
			}
            public System.String TurnOverAtMonthAge
            {
                get
                {
                    System.Int32? data = entity.TurnOverAtMonthAge;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TurnOverAtMonthAge = null;
                    else entity.TurnOverAtMonthAge = Convert.ToInt32(value);
                }
            }
            public System.String SRBirthMethodPhr
            {
                get
                {
                    System.String data = entity.SRBirthMethodPhr;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRBirthMethodPhr = null;
                    else entity.SRBirthMethodPhr = Convert.ToString(value);
                }
            }
            public System.String MethodOther
            {
                get
                {
                    System.String data = entity.MethodOther;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MethodOther = null;
                    else entity.MethodOther = Convert.ToString(value);
                }
            }
            private esPatientBirthRecord entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientBirthRecordQuery query)
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
				throw new Exception("esPatientBirthRecord can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientBirthRecord : esPatientBirthRecord
	{	
	}

	[Serializable]
	abstract public class esPatientBirthRecordQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return PatientBirthRecordMetadata.Meta();
			}
		}	
			
		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		} 
			
		public esQueryItem MotherMedicalNo
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.MotherMedicalNo, esSystemType.String);
			}
		} 
			
		public esQueryItem MotherRegistrationNo
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.MotherRegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem TimeOfBirth
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.TimeOfBirth, esSystemType.String);
			}
		} 
			
		public esQueryItem SRBornAt
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.SRBornAt, esSystemType.String);
			}
		} 
			
		public esQueryItem BornAtDescription
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.BornAtDescription, esSystemType.String);
			}
		} 
			
		public esQueryItem SRSingleTwin
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.SRSingleTwin, esSystemType.String);
			}
		} 
			
		public esQueryItem TwinNo
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.TwinNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SRBirthMethod
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.SRBirthMethod, esSystemType.String);
			}
		} 
			
		public esQueryItem SRCaesarMethod
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.SRCaesarMethod, esSystemType.String);
			}
		} 
			
		public esQueryItem SRBornCondition
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.SRBornCondition, esSystemType.String);
			}
		} 
			
		public esQueryItem SRBirthComplication
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.SRBirthComplication, esSystemType.String);
			}
		} 
			
		public esQueryItem SRDeathCondition
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.SRDeathCondition, esSystemType.String);
			}
		} 
			
		public esQueryItem SRBirthIndication
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.SRBirthIndication, esSystemType.String);
			}
		} 
			
		public esQueryItem BirthPregnancyAge
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.BirthPregnancyAge, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Length
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.Length, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Weight
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.Weight, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem ApgarScore1
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.ApgarScore1, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem ApgarScore2
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.ApgarScore2, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem ApgarScore3
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.ApgarScore3, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem HeadCircumference
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.HeadCircumference, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem ChestCircumference
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.ChestCircumference, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CertificateNo
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.CertificateNo, esSystemType.String);
			}
		} 
			
		public esQueryItem FatherName
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.FatherName, esSystemType.String);
			}
		} 
			
		public esQueryItem FatherSsn
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.FatherSsn, esSystemType.String);
			}
		} 
			
		public esQueryItem FatherBirthOfDate
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.FatherBirthOfDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem StreetName
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.StreetName, esSystemType.String);
			}
		} 
			
		public esQueryItem District
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.District, esSystemType.String);
			}
		} 
			
		public esQueryItem City
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.City, esSystemType.String);
			}
		} 
			
		public esQueryItem County
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.County, esSystemType.String);
			}
		} 
			
		public esQueryItem State
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.State, esSystemType.String);
			}
		} 
			
		public esQueryItem ZipCode
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.ZipCode, esSystemType.String);
			}
		} 
			
		public esQueryItem SROccupation
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.SROccupation, esSystemType.String);
			}
		} 
			
		public esQueryItem PhoneNo
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.PhoneNo, esSystemType.String);
			}
		} 
			
		public esQueryItem FaxNo
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.FaxNo, esSystemType.String);
			}
		} 
			
		public esQueryItem MobilePhoneNo
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.MobilePhoneNo, esSystemType.String);
			}
		} 
			
		public esQueryItem Email
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.Email, esSystemType.String);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem BirthMethod
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.BirthMethod, esSystemType.String);
			}
		} 
			
		public esQueryItem BirthMethodScIndication
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.BirthMethodScIndication, esSystemType.String);
			}
		} 
			
		public esQueryItem ChildNumber
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.ChildNumber, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChildNumberFrom
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.ChildNumberFrom, esSystemType.Int32);
			}
		} 
			
		public esQueryItem AsiToMonthAge
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.AsiToMonthAge, esSystemType.Int32);
			}
		} 
			
		public esQueryItem CurrentDiet
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.CurrentDiet, esSystemType.String);
			}
		} 
			
		public esQueryItem ProneAtMonthAge
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.ProneAtMonthAge, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SitAtMonthAge
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.SitAtMonthAge, esSystemType.Int32);
			}
		} 
			
		public esQueryItem CrawlAtMonthAge
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.CrawlAtMonthAge, esSystemType.Int32);
			}
		} 
			
		public esQueryItem StandUpAtMonthAge
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.StandUpAtMonthAge, esSystemType.Int32);
			}
		} 
			
		public esQueryItem WalkAtMonthAge
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.WalkAtMonthAge, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Speak3WordAtMonthAge
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.Speak3WordAtMonthAge, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Speak2SentAtMonthAge
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.Speak2SentAtMonthAge, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SchoolClass
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.SchoolClass, esSystemType.String);
			}
		} 
			
		public esQueryItem SchoolAchievement
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.SchoolAchievement, esSystemType.String);
			}
		} 
			
		public esQueryItem GrowthNotes
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.GrowthNotes, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem FormulaMilkStartAge
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.FormulaMilkStartAge, esSystemType.Int32);
			}
		} 
			
		public esQueryItem AddFoodStartAge
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.AddFoodStartAge, esSystemType.Int32);
			}
		} 
			
		public esQueryItem RaiseHead
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.RaiseHead, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Grabbing
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.Grabbing, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Holding
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.Holding, esSystemType.Int32);
			}
		} 
			
		public esQueryItem PregnanDurationMonth
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.PregnanDurationMonth, esSystemType.Int32);
			}
		} 
			
		public esQueryItem PregnanDurationWeek
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.PregnanDurationWeek, esSystemType.Int32);
			}
		}

		public esQueryItem Smile
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.Smile, esSystemType.Int32);
			}
		}
		public esQueryItem Cooing
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.Cooing, esSystemType.Int32);
			}
        }
        public esQueryItem RollToTummy
        {
            get
            {
                return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.RollToTummy, esSystemType.Int32);
            }
        }
		public esQueryItem RollFromTummy
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.RollFromTummy, esSystemType.Int32);
			}
		}
		public esQueryItem Babbling
		{
			get
			{
				return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.Babbling, esSystemType.Int32);
			}
		}
        public esQueryItem TurnOverAtMonthAge
        {
            get
            {
                return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.TurnOverAtMonthAge, esSystemType.Int32);
            }
        }
        public esQueryItem SRBirthMethodPhr
        {
            get
            {
                return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.SRBirthMethodPhr, esSystemType.String);
            }
        }
        public esQueryItem MethodOther
        {
            get
            {
                return new esQueryItem(this, PatientBirthRecordMetadata.ColumnNames.MethodOther, esSystemType.String);
            }
        }
    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientBirthRecordCollection")]
	public partial class PatientBirthRecordCollection : esPatientBirthRecordCollection, IEnumerable< PatientBirthRecord>
	{
		public PatientBirthRecordCollection()
		{

		}	
		
		public static implicit operator List< PatientBirthRecord>(PatientBirthRecordCollection coll)
		{
			List< PatientBirthRecord> list = new List< PatientBirthRecord>();
			
			foreach (PatientBirthRecord emp in coll)
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
				return  PatientBirthRecordMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientBirthRecordQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientBirthRecord(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientBirthRecord();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public PatientBirthRecordQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientBirthRecordQuery();
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
		public bool Load(PatientBirthRecordQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientBirthRecord AddNew()
		{
			PatientBirthRecord entity = base.AddNewEntity() as PatientBirthRecord;
			
			return entity;		
		}
		public PatientBirthRecord FindByPrimaryKey(String patientID)
		{
			return base.FindByPrimaryKey(patientID) as PatientBirthRecord;
		}

		#region IEnumerable< PatientBirthRecord> Members

		IEnumerator< PatientBirthRecord> IEnumerable< PatientBirthRecord>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PatientBirthRecord;
			}
		}

		#endregion
		
		private PatientBirthRecordQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientBirthRecord' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientBirthRecord ({PatientID})")]
	[Serializable]
	public partial class PatientBirthRecord : esPatientBirthRecord
	{
		public PatientBirthRecord()
		{
		}	
	
		public PatientBirthRecord(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientBirthRecordMetadata.Meta();
			}
		}	
	
		override protected esPatientBirthRecordQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientBirthRecordQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public PatientBirthRecordQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientBirthRecordQuery();
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
		public bool Load(PatientBirthRecordQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private PatientBirthRecordQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientBirthRecordQuery : esPatientBirthRecordQuery
	{
		public PatientBirthRecordQuery()
		{

		}		
		
		public PatientBirthRecordQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "PatientBirthRecordQuery";
        }
	}

	[Serializable]
	public partial class PatientBirthRecordMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientBirthRecordMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.PatientID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.PatientID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.MotherMedicalNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.MotherMedicalNo;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.MotherRegistrationNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.MotherRegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.TimeOfBirth, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.TimeOfBirth;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.SRBornAt, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.SRBornAt;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.BornAtDescription, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.BornAtDescription;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.SRSingleTwin, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.SRSingleTwin;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.TwinNo, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.TwinNo;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.SRBirthMethod, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.SRBirthMethod;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.SRCaesarMethod, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.SRCaesarMethod;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.SRBornCondition, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.SRBornCondition;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.SRBirthComplication, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.SRBirthComplication;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.SRDeathCondition, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.SRDeathCondition;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.SRBirthIndication, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.SRBirthIndication;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.BirthPregnancyAge, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.BirthPregnancyAge;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.Length, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.Length;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.Weight, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.Weight;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.ApgarScore1, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.ApgarScore1;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.ApgarScore2, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.ApgarScore2;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.ApgarScore3, 19, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.ApgarScore3;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.HeadCircumference, 20, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.HeadCircumference;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.ChestCircumference, 21, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.ChestCircumference;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.CertificateNo, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.CertificateNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.FatherName, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.FatherName;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.FatherSsn, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.FatherSsn;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.FatherBirthOfDate, 25, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.FatherBirthOfDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.StreetName, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.StreetName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.District, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.District;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.City, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.City;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.County, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.County;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.State, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.State;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.ZipCode, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.ZipCode;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.SROccupation, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.SROccupation;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.PhoneNo, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.PhoneNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.FaxNo, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.FaxNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.MobilePhoneNo, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.MobilePhoneNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.Email, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.Email;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.Notes, 37, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.BirthMethod, 38, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.BirthMethod;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.BirthMethodScIndication, 39, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.BirthMethodScIndication;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.ChildNumber, 40, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.ChildNumber;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.ChildNumberFrom, 41, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.ChildNumberFrom;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.AsiToMonthAge, 42, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.AsiToMonthAge;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.CurrentDiet, 43, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.CurrentDiet;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.ProneAtMonthAge, 44, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.ProneAtMonthAge;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.SitAtMonthAge, 45, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.SitAtMonthAge;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.CrawlAtMonthAge, 46, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.CrawlAtMonthAge;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.StandUpAtMonthAge, 47, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.StandUpAtMonthAge;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.WalkAtMonthAge, 48, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.WalkAtMonthAge;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.Speak3WordAtMonthAge, 49, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.Speak3WordAtMonthAge;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.Speak2SentAtMonthAge, 50, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.Speak2SentAtMonthAge;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.SchoolClass, 51, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.SchoolClass;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.SchoolAchievement, 52, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.SchoolAchievement;
			c.CharacterMaxLength = 300;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.GrowthNotes, 53, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.GrowthNotes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.LastUpdateDateTime, 54, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.LastUpdateByUserID, 55, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.FormulaMilkStartAge, 56, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.FormulaMilkStartAge;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.AddFoodStartAge, 57, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.AddFoodStartAge;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.RaiseHead, 58, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.RaiseHead;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.Grabbing, 59, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.Grabbing;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.Holding, 60, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.Holding;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.PregnanDurationMonth, 61, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.PregnanDurationMonth;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.PregnanDurationWeek, 62, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.PregnanDurationWeek;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.Smile, 58, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.Smile;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.Cooing, 58, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.Cooing;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.RollToTummy, 58, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.RollToTummy;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.RollFromTummy, 58, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.RollFromTummy;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.Babbling, 58, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientBirthRecordMetadata.PropertyNames.Babbling;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

            c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.TurnOverAtMonthAge, 45, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = PatientBirthRecordMetadata.PropertyNames.TurnOverAtMonthAge;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.SRBirthMethodPhr, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientBirthRecordMetadata.PropertyNames.SRBirthMethodPhr;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientBirthRecordMetadata.ColumnNames.MethodOther, 43, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientBirthRecordMetadata.PropertyNames.MethodOther;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);
        }
		#endregion

		static public PatientBirthRecordMetadata Meta()
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
			public const string PatientID = "PatientID";
			public const string MotherMedicalNo = "MotherMedicalNo";
			public const string MotherRegistrationNo = "MotherRegistrationNo";
			public const string TimeOfBirth = "TimeOfBirth";
			public const string SRBornAt = "SRBornAt";
			public const string BornAtDescription = "BornAtDescription";
			public const string SRSingleTwin = "SRSingleTwin";
			public const string TwinNo = "TwinNo";
			public const string SRBirthMethod = "SRBirthMethod";
			public const string SRCaesarMethod = "SRCaesarMethod";
			public const string SRBornCondition = "SRBornCondition";
			public const string SRBirthComplication = "SRBirthComplication";
			public const string SRDeathCondition = "SRDeathCondition";
			public const string SRBirthIndication = "SRBirthIndication";
			public const string BirthPregnancyAge = "BirthPregnancyAge";
			public const string Length = "Length";
			public const string Weight = "Weight";
			public const string ApgarScore1 = "ApgarScore1";
			public const string ApgarScore2 = "ApgarScore2";
			public const string ApgarScore3 = "ApgarScore3";
			public const string HeadCircumference = "HeadCircumference";
			public const string ChestCircumference = "ChestCircumference";
			public const string CertificateNo = "CertificateNo";
			public const string FatherName = "FatherName";
			public const string FatherSsn = "FatherSsn";
			public const string FatherBirthOfDate = "FatherBirthOfDate";
			public const string StreetName = "StreetName";
			public const string District = "District";
			public const string City = "City";
			public const string County = "County";
			public const string State = "State";
			public const string ZipCode = "ZipCode";
			public const string SROccupation = "SROccupation";
			public const string PhoneNo = "PhoneNo";
			public const string FaxNo = "FaxNo";
			public const string MobilePhoneNo = "MobilePhoneNo";
			public const string Email = "Email";
			public const string Notes = "Notes";
			public const string BirthMethod = "BirthMethod";
			public const string BirthMethodScIndication = "BirthMethodScIndication";
			public const string ChildNumber = "ChildNumber";
			public const string ChildNumberFrom = "ChildNumberFrom";
			public const string AsiToMonthAge = "AsiToMonthAge";
			public const string CurrentDiet = "CurrentDiet";
			public const string ProneAtMonthAge = "ProneAtMonthAge";
			public const string SitAtMonthAge = "SitAtMonthAge";
			public const string CrawlAtMonthAge = "CrawlAtMonthAge";
			public const string StandUpAtMonthAge = "StandUpAtMonthAge";
			public const string WalkAtMonthAge = "WalkAtMonthAge";
			public const string Speak3WordAtMonthAge = "Speak3WordAtMonthAge";
			public const string Speak2SentAtMonthAge = "Speak2SentAtMonthAge";
			public const string SchoolClass = "SchoolClass";
			public const string SchoolAchievement = "SchoolAchievement";
			public const string GrowthNotes = "GrowthNotes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string FormulaMilkStartAge = "FormulaMilkStartAge";
			public const string AddFoodStartAge = "AddFoodStartAge";
			public const string RaiseHead = "RaiseHead";
			public const string Grabbing = "Grabbing";
			public const string Holding = "Holding";
			public const string PregnanDurationMonth = "PregnanDurationMonth";
			public const string PregnanDurationWeek = "PregnanDurationWeek";

			public const string Smile = "Smile";
			public const string Cooing = "Cooing";
			public const string RollToTummy = "RollToTummy";
			public const string RollFromTummy = "RollFromTummy";
			public const string Babbling = "Babbling";
            public const string TurnOverAtMonthAge = "TurnOverAtMonthAge";
            public const string SRBirthMethodPhr = "SRBirthMethodPhr";
            public const string MethodOther = "MethodOther";

        }
		#endregion

		#region PropertyNames
		public class PropertyNames
		{ 
			public const string PatientID = "PatientID";
			public const string MotherMedicalNo = "MotherMedicalNo";
			public const string MotherRegistrationNo = "MotherRegistrationNo";
			public const string TimeOfBirth = "TimeOfBirth";
			public const string SRBornAt = "SRBornAt";
			public const string BornAtDescription = "BornAtDescription";
			public const string SRSingleTwin = "SRSingleTwin";
			public const string TwinNo = "TwinNo";
			public const string SRBirthMethod = "SRBirthMethod";
			public const string SRCaesarMethod = "SRCaesarMethod";
			public const string SRBornCondition = "SRBornCondition";
			public const string SRBirthComplication = "SRBirthComplication";
			public const string SRDeathCondition = "SRDeathCondition";
			public const string SRBirthIndication = "SRBirthIndication";
			public const string BirthPregnancyAge = "BirthPregnancyAge";
			public const string Length = "Length";
			public const string Weight = "Weight";
			public const string ApgarScore1 = "ApgarScore1";
			public const string ApgarScore2 = "ApgarScore2";
			public const string ApgarScore3 = "ApgarScore3";
			public const string HeadCircumference = "HeadCircumference";
			public const string ChestCircumference = "ChestCircumference";
			public const string CertificateNo = "CertificateNo";
			public const string FatherName = "FatherName";
			public const string FatherSsn = "FatherSsn";
			public const string FatherBirthOfDate = "FatherBirthOfDate";
			public const string StreetName = "StreetName";
			public const string District = "District";
			public const string City = "City";
			public const string County = "County";
			public const string State = "State";
			public const string ZipCode = "ZipCode";
			public const string SROccupation = "SROccupation";
			public const string PhoneNo = "PhoneNo";
			public const string FaxNo = "FaxNo";
			public const string MobilePhoneNo = "MobilePhoneNo";
			public const string Email = "Email";
			public const string Notes = "Notes";
			public const string BirthMethod = "BirthMethod";
			public const string BirthMethodScIndication = "BirthMethodScIndication";
			public const string ChildNumber = "ChildNumber";
			public const string ChildNumberFrom = "ChildNumberFrom";
			public const string AsiToMonthAge = "AsiToMonthAge";
			public const string CurrentDiet = "CurrentDiet";
			public const string ProneAtMonthAge = "ProneAtMonthAge";
			public const string SitAtMonthAge = "SitAtMonthAge";
			public const string CrawlAtMonthAge = "CrawlAtMonthAge";
			public const string StandUpAtMonthAge = "StandUpAtMonthAge";
			public const string WalkAtMonthAge = "WalkAtMonthAge";
			public const string Speak3WordAtMonthAge = "Speak3WordAtMonthAge";
			public const string Speak2SentAtMonthAge = "Speak2SentAtMonthAge";
			public const string SchoolClass = "SchoolClass";
			public const string SchoolAchievement = "SchoolAchievement";
			public const string GrowthNotes = "GrowthNotes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string FormulaMilkStartAge = "FormulaMilkStartAge";
			public const string AddFoodStartAge = "AddFoodStartAge";
			public const string RaiseHead = "RaiseHead";
			public const string Grabbing = "Grabbing";
			public const string Holding = "Holding";
			public const string PregnanDurationMonth = "PregnanDurationMonth";
			public const string PregnanDurationWeek = "PregnanDurationWeek";

			public const string Smile = "Smile";
			public const string Cooing = "Cooing";
			public const string RollToTummy = "RollToTummy";
			public const string RollFromTummy = "RollFromTummy";
			public const string Babbling = "Babbling";
            public const string TurnOverAtMonthAge = "TurnOverAtMonthAge";
            public const string SRBirthMethodPhr = "SRBirthMethodPhr";
            public const string MethodOther = "MethodOther";
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
			lock (typeof(PatientBirthRecordMetadata))
			{
				if(PatientBirthRecordMetadata.mapDelegates == null)
				{
					PatientBirthRecordMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PatientBirthRecordMetadata.meta == null)
				{
					PatientBirthRecordMetadata.meta = new PatientBirthRecordMetadata();
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
				
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MotherMedicalNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MotherRegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TimeOfBirth", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBornAt", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BornAtDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRSingleTwin", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TwinNo", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("SRBirthMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCaesarMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBornCondition", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBirthComplication", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDeathCondition", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBirthIndication", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BirthPregnancyAge", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Length", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Weight", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ApgarScore1", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ApgarScore2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ApgarScore3", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HeadCircumference", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ChestCircumference", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CertificateNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FatherName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FatherSsn", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FatherBirthOfDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("StreetName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("District", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("City", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("County", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("State", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ZipCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SROccupation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FaxNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MobilePhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Email", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BirthMethod", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("BirthMethodScIndication", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChildNumber", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChildNumberFrom", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AsiToMonthAge", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CurrentDiet", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProneAtMonthAge", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SitAtMonthAge", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CrawlAtMonthAge", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("StandUpAtMonthAge", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WalkAtMonthAge", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Speak3WordAtMonthAge", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Speak2SentAtMonthAge", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SchoolClass", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SchoolAchievement", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GrowthNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FormulaMilkStartAge", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AddFoodStartAge", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RaiseHead", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Grabbing", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Holding", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PregnanDurationMonth", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PregnanDurationWeek", new esTypeMap("int", "System.Int32"));

				meta.AddTypeMap("Smile", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Cooing", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RollToTummy", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RollFromTummy", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Babbling", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("TurnOverAtMonthAge", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SRBirthMethodPhr", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MethodOther", new esTypeMap("varchar", "System.String"));


                meta.Source = "PatientBirthRecord";
				meta.Destination = "PatientBirthRecord";
				meta.spInsert = "proc_PatientBirthRecordInsert";				
				meta.spUpdate = "proc_PatientBirthRecordUpdate";		
				meta.spDelete = "proc_PatientBirthRecordDelete";
				meta.spLoadAll = "proc_PatientBirthRecordLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientBirthRecordLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientBirthRecordMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
