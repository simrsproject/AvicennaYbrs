/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/6/2022 8:46:25 PM
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
	abstract public class esRegistrationRasproCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationRasproCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "RegistrationRasproCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationRasproQuery query)
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
			this.InitQuery(query as esRegistrationRasproQuery);
		}
		#endregion

		virtual public RegistrationRaspro DetachEntity(RegistrationRaspro entity)
		{
			return base.DetachEntity(entity) as RegistrationRaspro;
		}

		virtual public RegistrationRaspro AttachEntity(RegistrationRaspro entity)
		{
			return base.AttachEntity(entity) as RegistrationRaspro;
		}

		virtual public void Combine(RegistrationRasproCollection collection)
		{
			base.Combine(collection);
		}

		new public RegistrationRaspro this[int index]
		{
			get
			{
				return base[index] as RegistrationRaspro;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationRaspro);
		}
	}

	[Serializable]
	abstract public class esRegistrationRaspro : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationRasproQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationRaspro()
		{
		}

		public esRegistrationRaspro(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 seqNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, seqNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 seqNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, seqNo);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 seqNo)
		{
			esRegistrationRasproQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.SeqNo == seqNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 seqNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("SeqNo", seqNo);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "SeqNo": this.str.SeqNo = (string)value; break;
						case "RasproDateTime": this.str.RasproDateTime = (string)value; break;
						case "SRRaspro": this.str.SRRaspro = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "AdviseByParamedicID": this.str.AdviseByParamedicID = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "AbRestrictionID": this.str.AbRestrictionID = (string)value; break;
						case "ActionNo": this.str.ActionNo = (string)value; break;
						case "AntibioticLevel": this.str.AntibioticLevel = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "AntibioticIndication": this.str.AntibioticIndication = (string)value; break;
						case "Comorbid": this.str.Comorbid = (string)value; break;
						case "ComorbidOther": this.str.ComorbidOther = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SRWoundClassification": this.str.SRWoundClassification = (string)value; break;
						case "SurgeryName": this.str.SurgeryName = (string)value; break;
						case "Diagnose": this.str.Diagnose = (string)value; break;
						case "OtherInfection": this.str.OtherInfection = (string)value; break;
						case "PrevAbRestrictionID": this.str.PrevAbRestrictionID = (string)value; break;
						case "PrevAntibioticLevel": this.str.PrevAntibioticLevel = (string)value; break;
						case "IsAntibioticIndication": this.str.IsAntibioticIndication = (string)value; break;
						case "IsInfectionSymptom": this.str.IsInfectionSymptom = (string)value; break;
						case "InfectionSymptom": this.str.InfectionSymptom = (string)value; break;
						case "IsComorbid": this.str.IsComorbid = (string)value; break;
						case "RasprajaReason": this.str.RasprajaReason = (string)value; break;
						case "IsPpraConsult": this.str.IsPpraConsult = (string)value; break;
						case "NotPpraConsultReason": this.str.NotPpraConsultReason = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "PrescriptionNo": this.str.PrescriptionNo = (string)value; break;
						case "IsExternalCultureLabTest": this.str.IsExternalCultureLabTest = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "SeqNo":

							if (value == null || value is System.Int32)
								this.SeqNo = (System.Int32?)value;
							break;
						case "RasproDateTime":

							if (value == null || value is System.DateTime)
								this.RasproDateTime = (System.DateTime?)value;
							break;
						case "ActionNo":

							if (value == null || value is System.Int32)
								this.ActionNo = (System.Int32?)value;
							break;
						case "AntibioticLevel":

							if (value == null || value is System.Int32)
								this.AntibioticLevel = (System.Int32?)value;
							break;
						case "SignImage":

							if (value == null || value is System.Byte[])
								this.SignImage = (System.Byte[])value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "PrevAntibioticLevel":

							if (value == null || value is System.Int32)
								this.PrevAntibioticLevel = (System.Int32?)value;
							break;
						case "IsAntibioticIndication":

							if (value == null || value is System.Boolean)
								this.IsAntibioticIndication = (System.Boolean?)value;
							break;
						case "IsInfectionSymptom":

							if (value == null || value is System.Boolean)
								this.IsInfectionSymptom = (System.Boolean?)value;
							break;
						case "IsComorbid":

							if (value == null || value is System.Boolean)
								this.IsComorbid = (System.Boolean?)value;
							break;
						case "IsPpraConsult":

							if (value == null || value is System.Boolean)
								this.IsPpraConsult = (System.Boolean?)value;
							break;
						case "IsExternalCultureLabTest":

							if (value == null || value is System.Boolean)
								this.IsExternalCultureLabTest = (System.Boolean?)value;
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
		/// Maps to RegistrationRaspro.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.SeqNo
		/// </summary>
		virtual public System.Int32? SeqNo
		{
			get
			{
				return base.GetSystemInt32(RegistrationRasproMetadata.ColumnNames.SeqNo);
			}

			set
			{
				base.SetSystemInt32(RegistrationRasproMetadata.ColumnNames.SeqNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.RasproDateTime
		/// </summary>
		virtual public System.DateTime? RasproDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationRasproMetadata.ColumnNames.RasproDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationRasproMetadata.ColumnNames.RasproDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.SRRaspro
		/// </summary>
		virtual public System.String SRRaspro
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.SRRaspro);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.SRRaspro, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.AdviseByParamedicID
		/// </summary>
		virtual public System.String AdviseByParamedicID
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.AdviseByParamedicID);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.AdviseByParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.AbRestrictionID
		/// </summary>
		virtual public System.String AbRestrictionID
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.AbRestrictionID);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.AbRestrictionID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.ActionNo
		/// </summary>
		virtual public System.Int32? ActionNo
		{
			get
			{
				return base.GetSystemInt32(RegistrationRasproMetadata.ColumnNames.ActionNo);
			}

			set
			{
				base.SetSystemInt32(RegistrationRasproMetadata.ColumnNames.ActionNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.AntibioticLevel
		/// </summary>
		virtual public System.Int32? AntibioticLevel
		{
			get
			{
				return base.GetSystemInt32(RegistrationRasproMetadata.ColumnNames.AntibioticLevel);
			}

			set
			{
				base.SetSystemInt32(RegistrationRasproMetadata.ColumnNames.AntibioticLevel, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.SignImage
		/// </summary>
		virtual public System.Byte[] SignImage
		{
			get
			{
				return base.GetSystemByteArray(RegistrationRasproMetadata.ColumnNames.SignImage);
			}

			set
			{
				base.SetSystemByteArray(RegistrationRasproMetadata.ColumnNames.SignImage, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.AntibioticIndication
		/// </summary>
		virtual public System.String AntibioticIndication
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.AntibioticIndication);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.AntibioticIndication, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.Comorbid
		/// </summary>
		virtual public System.String Comorbid
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.Comorbid);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.Comorbid, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.ComorbidOther
		/// </summary>
		virtual public System.String ComorbidOther
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.ComorbidOther);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.ComorbidOther, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationRasproMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationRasproMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.SRWoundClassification
		/// </summary>
		virtual public System.String SRWoundClassification
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.SRWoundClassification);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.SRWoundClassification, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.SurgeryName
		/// </summary>
		virtual public System.String SurgeryName
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.SurgeryName);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.SurgeryName, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.Diagnose
		/// </summary>
		virtual public System.String Diagnose
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.Diagnose);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.Diagnose, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.OtherInfection
		/// </summary>
		virtual public System.String OtherInfection
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.OtherInfection);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.OtherInfection, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.PrevAbRestrictionID
		/// </summary>
		virtual public System.String PrevAbRestrictionID
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.PrevAbRestrictionID);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.PrevAbRestrictionID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.PrevAntibioticLevel
		/// </summary>
		virtual public System.Int32? PrevAntibioticLevel
		{
			get
			{
				return base.GetSystemInt32(RegistrationRasproMetadata.ColumnNames.PrevAntibioticLevel);
			}

			set
			{
				base.SetSystemInt32(RegistrationRasproMetadata.ColumnNames.PrevAntibioticLevel, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.IsAntibioticIndication
		/// </summary>
		virtual public System.Boolean? IsAntibioticIndication
		{
			get
			{
				return base.GetSystemBoolean(RegistrationRasproMetadata.ColumnNames.IsAntibioticIndication);
			}

			set
			{
				base.SetSystemBoolean(RegistrationRasproMetadata.ColumnNames.IsAntibioticIndication, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.IsInfectionSymptom
		/// </summary>
		virtual public System.Boolean? IsInfectionSymptom
		{
			get
			{
				return base.GetSystemBoolean(RegistrationRasproMetadata.ColumnNames.IsInfectionSymptom);
			}

			set
			{
				base.SetSystemBoolean(RegistrationRasproMetadata.ColumnNames.IsInfectionSymptom, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.InfectionSymptom
		/// </summary>
		virtual public System.String InfectionSymptom
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.InfectionSymptom);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.InfectionSymptom, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.IsComorbid
		/// </summary>
		virtual public System.Boolean? IsComorbid
		{
			get
			{
				return base.GetSystemBoolean(RegistrationRasproMetadata.ColumnNames.IsComorbid);
			}

			set
			{
				base.SetSystemBoolean(RegistrationRasproMetadata.ColumnNames.IsComorbid, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.RasprajaReason
		/// </summary>
		virtual public System.String RasprajaReason
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.RasprajaReason);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.RasprajaReason, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.IsPpraConsult
		/// </summary>
		virtual public System.Boolean? IsPpraConsult
		{
			get
			{
				return base.GetSystemBoolean(RegistrationRasproMetadata.ColumnNames.IsPpraConsult);
			}

			set
			{
				base.SetSystemBoolean(RegistrationRasproMetadata.ColumnNames.IsPpraConsult, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.NotPpraConsultReason
		/// </summary>
		virtual public System.String NotPpraConsultReason
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.NotPpraConsultReason);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.NotPpraConsultReason, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.PrescriptionNo
		/// </summary>
		virtual public System.String PrescriptionNo
		{
			get
			{
				return base.GetSystemString(RegistrationRasproMetadata.ColumnNames.PrescriptionNo);
			}

			set
			{
				base.SetSystemString(RegistrationRasproMetadata.ColumnNames.PrescriptionNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRaspro.IsExternalCultureLabTest
		/// </summary>
		virtual public System.Boolean? IsExternalCultureLabTest
		{
			get
			{
				return base.GetSystemBoolean(RegistrationRasproMetadata.ColumnNames.IsExternalCultureLabTest);
			}

			set
			{
				base.SetSystemBoolean(RegistrationRasproMetadata.ColumnNames.IsExternalCultureLabTest, value);
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
			public esStrings(esRegistrationRaspro entity)
			{
				this.entity = entity;
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
			public System.String SeqNo
			{
				get
				{
					System.Int32? data = entity.SeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeqNo = null;
					else entity.SeqNo = Convert.ToInt32(value);
				}
			}
			public System.String RasproDateTime
			{
				get
				{
					System.DateTime? data = entity.RasproDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RasproDateTime = null;
					else entity.RasproDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String SRRaspro
			{
				get
				{
					System.String data = entity.SRRaspro;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRaspro = null;
					else entity.SRRaspro = Convert.ToString(value);
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
			public System.String AdviseByParamedicID
			{
				get
				{
					System.String data = entity.AdviseByParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdviseByParamedicID = null;
					else entity.AdviseByParamedicID = Convert.ToString(value);
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
			public System.String AbRestrictionID
			{
				get
				{
					System.String data = entity.AbRestrictionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AbRestrictionID = null;
					else entity.AbRestrictionID = Convert.ToString(value);
				}
			}
			public System.String ActionNo
			{
				get
				{
					System.Int32? data = entity.ActionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ActionNo = null;
					else entity.ActionNo = Convert.ToInt32(value);
				}
			}
			public System.String AntibioticLevel
			{
				get
				{
					System.Int32? data = entity.AntibioticLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AntibioticLevel = null;
					else entity.AntibioticLevel = Convert.ToInt32(value);
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
			public System.String AntibioticIndication
			{
				get
				{
					System.String data = entity.AntibioticIndication;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AntibioticIndication = null;
					else entity.AntibioticIndication = Convert.ToString(value);
				}
			}
			public System.String Comorbid
			{
				get
				{
					System.String data = entity.Comorbid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Comorbid = null;
					else entity.Comorbid = Convert.ToString(value);
				}
			}
			public System.String ComorbidOther
			{
				get
				{
					System.String data = entity.ComorbidOther;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ComorbidOther = null;
					else entity.ComorbidOther = Convert.ToString(value);
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
			public System.String SRWoundClassification
			{
				get
				{
					System.String data = entity.SRWoundClassification;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRWoundClassification = null;
					else entity.SRWoundClassification = Convert.ToString(value);
				}
			}
			public System.String SurgeryName
			{
				get
				{
					System.String data = entity.SurgeryName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SurgeryName = null;
					else entity.SurgeryName = Convert.ToString(value);
				}
			}
			public System.String Diagnose
			{
				get
				{
					System.String data = entity.Diagnose;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Diagnose = null;
					else entity.Diagnose = Convert.ToString(value);
				}
			}
			public System.String OtherInfection
			{
				get
				{
					System.String data = entity.OtherInfection;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherInfection = null;
					else entity.OtherInfection = Convert.ToString(value);
				}
			}
			public System.String PrevAbRestrictionID
			{
				get
				{
					System.String data = entity.PrevAbRestrictionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrevAbRestrictionID = null;
					else entity.PrevAbRestrictionID = Convert.ToString(value);
				}
			}
			public System.String PrevAntibioticLevel
			{
				get
				{
					System.Int32? data = entity.PrevAntibioticLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrevAntibioticLevel = null;
					else entity.PrevAntibioticLevel = Convert.ToInt32(value);
				}
			}
			public System.String IsAntibioticIndication
			{
				get
				{
					System.Boolean? data = entity.IsAntibioticIndication;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAntibioticIndication = null;
					else entity.IsAntibioticIndication = Convert.ToBoolean(value);
				}
			}
			public System.String IsInfectionSymptom
			{
				get
				{
					System.Boolean? data = entity.IsInfectionSymptom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInfectionSymptom = null;
					else entity.IsInfectionSymptom = Convert.ToBoolean(value);
				}
			}
			public System.String InfectionSymptom
			{
				get
				{
					System.String data = entity.InfectionSymptom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InfectionSymptom = null;
					else entity.InfectionSymptom = Convert.ToString(value);
				}
			}
			public System.String IsComorbid
			{
				get
				{
					System.Boolean? data = entity.IsComorbid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsComorbid = null;
					else entity.IsComorbid = Convert.ToBoolean(value);
				}
			}
			public System.String RasprajaReason
			{
				get
				{
					System.String data = entity.RasprajaReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RasprajaReason = null;
					else entity.RasprajaReason = Convert.ToString(value);
				}
			}
			public System.String IsPpraConsult
			{
				get
				{
					System.Boolean? data = entity.IsPpraConsult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPpraConsult = null;
					else entity.IsPpraConsult = Convert.ToBoolean(value);
				}
			}
			public System.String NotPpraConsultReason
			{
				get
				{
					System.String data = entity.NotPpraConsultReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NotPpraConsultReason = null;
					else entity.NotPpraConsultReason = Convert.ToString(value);
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
			public System.String IsExternalCultureLabTest
			{
				get
				{
					System.Boolean? data = entity.IsExternalCultureLabTest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsExternalCultureLabTest = null;
					else entity.IsExternalCultureLabTest = Convert.ToBoolean(value);
				}
			}
			private esRegistrationRaspro entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationRasproQuery query)
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
				throw new Exception("esRegistrationRaspro can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationRaspro : esRegistrationRaspro
	{
	}

	[Serializable]
	abstract public class esRegistrationRasproQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return RegistrationRasproMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem SeqNo
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.SeqNo, esSystemType.Int32);
			}
		}

		public esQueryItem RasproDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.RasproDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem SRRaspro
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.SRRaspro, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem AdviseByParamedicID
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.AdviseByParamedicID, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem AbRestrictionID
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.AbRestrictionID, esSystemType.String);
			}
		}

		public esQueryItem ActionNo
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.ActionNo, esSystemType.Int32);
			}
		}

		public esQueryItem AntibioticLevel
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.AntibioticLevel, esSystemType.Int32);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem SignImage
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.SignImage, esSystemType.ByteArray);
			}
		}

		public esQueryItem AntibioticIndication
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.AntibioticIndication, esSystemType.String);
			}
		}

		public esQueryItem Comorbid
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.Comorbid, esSystemType.String);
			}
		}

		public esQueryItem ComorbidOther
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.ComorbidOther, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SRWoundClassification
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.SRWoundClassification, esSystemType.String);
			}
		}

		public esQueryItem SurgeryName
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.SurgeryName, esSystemType.String);
			}
		}

		public esQueryItem Diagnose
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.Diagnose, esSystemType.String);
			}
		}

		public esQueryItem OtherInfection
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.OtherInfection, esSystemType.String);
			}
		}

		public esQueryItem PrevAbRestrictionID
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.PrevAbRestrictionID, esSystemType.String);
			}
		}

		public esQueryItem PrevAntibioticLevel
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.PrevAntibioticLevel, esSystemType.Int32);
			}
		}

		public esQueryItem IsAntibioticIndication
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.IsAntibioticIndication, esSystemType.Boolean);
			}
		}

		public esQueryItem IsInfectionSymptom
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.IsInfectionSymptom, esSystemType.Boolean);
			}
		}

		public esQueryItem InfectionSymptom
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.InfectionSymptom, esSystemType.String);
			}
		}

		public esQueryItem IsComorbid
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.IsComorbid, esSystemType.Boolean);
			}
		}

		public esQueryItem RasprajaReason
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.RasprajaReason, esSystemType.String);
			}
		}

		public esQueryItem IsPpraConsult
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.IsPpraConsult, esSystemType.Boolean);
			}
		}

		public esQueryItem NotPpraConsultReason
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.NotPpraConsultReason, esSystemType.String);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem PrescriptionNo
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.PrescriptionNo, esSystemType.String);
			}
		}

		public esQueryItem IsExternalCultureLabTest
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproMetadata.ColumnNames.IsExternalCultureLabTest, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationRasproCollection")]
	public partial class RegistrationRasproCollection : esRegistrationRasproCollection, IEnumerable<RegistrationRaspro>
	{
		public RegistrationRasproCollection()
		{

		}

		public static implicit operator List<RegistrationRaspro>(RegistrationRasproCollection coll)
		{
			List<RegistrationRaspro> list = new List<RegistrationRaspro>();

			foreach (RegistrationRaspro emp in coll)
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
				return RegistrationRasproMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationRasproQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationRaspro(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationRaspro();
		}

		#endregion

		[BrowsableAttribute(false)]
		public RegistrationRasproQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationRasproQuery();
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
		public bool Load(RegistrationRasproQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationRaspro AddNew()
		{
			RegistrationRaspro entity = base.AddNewEntity() as RegistrationRaspro;

			return entity;
		}
		public RegistrationRaspro FindByPrimaryKey(String registrationNo, Int32 seqNo)
		{
			return base.FindByPrimaryKey(registrationNo, seqNo) as RegistrationRaspro;
		}

		#region IEnumerable< RegistrationRaspro> Members

		IEnumerator<RegistrationRaspro> IEnumerable<RegistrationRaspro>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationRaspro;
			}
		}

		#endregion

		private RegistrationRasproQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationRaspro' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationRaspro ({RegistrationNo, SeqNo})")]
	[Serializable]
	public partial class RegistrationRaspro : esRegistrationRaspro
	{
		public RegistrationRaspro()
		{
		}

		public RegistrationRaspro(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationRasproMetadata.Meta();
			}
		}

		override protected esRegistrationRasproQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationRasproQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public RegistrationRasproQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationRasproQuery();
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
		public bool Load(RegistrationRasproQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private RegistrationRasproQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationRasproQuery : esRegistrationRasproQuery
	{
		public RegistrationRasproQuery()
		{

		}

		public RegistrationRasproQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "RegistrationRasproQuery";
		}
	}

	[Serializable]
	public partial class RegistrationRasproMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationRasproMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.SeqNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.SeqNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.RasproDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.RasproDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.SRRaspro, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.SRRaspro;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.ParamedicID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.AdviseByParamedicID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.AdviseByParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.ServiceUnitID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.AbRestrictionID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.AbRestrictionID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.ActionNo, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.ActionNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.AntibioticLevel, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.AntibioticLevel;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.ItemID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.SignImage, 11, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.SignImage;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.AntibioticIndication, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.AntibioticIndication;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.Comorbid, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.Comorbid;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.ComorbidOther, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.ComorbidOther;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.LastUpdateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.LastUpdateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.SRWoundClassification, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.SRWoundClassification;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.SurgeryName, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.SurgeryName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.Diagnose, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.Diagnose;
			c.CharacterMaxLength = 2000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.OtherInfection, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.OtherInfection;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.PrevAbRestrictionID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.PrevAbRestrictionID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.PrevAntibioticLevel, 22, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.PrevAntibioticLevel;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.IsAntibioticIndication, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.IsAntibioticIndication;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.IsInfectionSymptom, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.IsInfectionSymptom;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.InfectionSymptom, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.InfectionSymptom;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.IsComorbid, 26, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.IsComorbid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.RasprajaReason, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.RasprajaReason;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.IsPpraConsult, 28, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.IsPpraConsult;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.NotPpraConsultReason, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.NotPpraConsultReason;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.ReferenceNo, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.PrescriptionNo, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.PrescriptionNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproMetadata.ColumnNames.IsExternalCultureLabTest, 32, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationRasproMetadata.PropertyNames.IsExternalCultureLabTest;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public RegistrationRasproMetadata Meta()
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
			public const string RegistrationNo = "RegistrationNo";
			public const string SeqNo = "SeqNo";
			public const string RasproDateTime = "RasproDateTime";
			public const string SRRaspro = "SRRaspro";
			public const string ParamedicID = "ParamedicID";
			public const string AdviseByParamedicID = "AdviseByParamedicID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string AbRestrictionID = "AbRestrictionID";
			public const string ActionNo = "ActionNo";
			public const string AntibioticLevel = "AntibioticLevel";
			public const string ItemID = "ItemID";
			public const string SignImage = "SignImage";
			public const string AntibioticIndication = "AntibioticIndication";
			public const string Comorbid = "Comorbid";
			public const string ComorbidOther = "ComorbidOther";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRWoundClassification = "SRWoundClassification";
			public const string SurgeryName = "SurgeryName";
			public const string Diagnose = "Diagnose";
			public const string OtherInfection = "OtherInfection";
			public const string PrevAbRestrictionID = "PrevAbRestrictionID";
			public const string PrevAntibioticLevel = "PrevAntibioticLevel";
			public const string IsAntibioticIndication = "IsAntibioticIndication";
			public const string IsInfectionSymptom = "IsInfectionSymptom";
			public const string InfectionSymptom = "InfectionSymptom";
			public const string IsComorbid = "IsComorbid";
			public const string RasprajaReason = "RasprajaReason";
			public const string IsPpraConsult = "IsPpraConsult";
			public const string NotPpraConsultReason = "NotPpraConsultReason";
			public const string ReferenceNo = "ReferenceNo";
			public const string PrescriptionNo = "PrescriptionNo";
			public const string IsExternalCultureLabTest = "IsExternalCultureLabTest";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string SeqNo = "SeqNo";
			public const string RasproDateTime = "RasproDateTime";
			public const string SRRaspro = "SRRaspro";
			public const string ParamedicID = "ParamedicID";
			public const string AdviseByParamedicID = "AdviseByParamedicID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string AbRestrictionID = "AbRestrictionID";
			public const string ActionNo = "ActionNo";
			public const string AntibioticLevel = "AntibioticLevel";
			public const string ItemID = "ItemID";
			public const string SignImage = "SignImage";
			public const string AntibioticIndication = "AntibioticIndication";
			public const string Comorbid = "Comorbid";
			public const string ComorbidOther = "ComorbidOther";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRWoundClassification = "SRWoundClassification";
			public const string SurgeryName = "SurgeryName";
			public const string Diagnose = "Diagnose";
			public const string OtherInfection = "OtherInfection";
			public const string PrevAbRestrictionID = "PrevAbRestrictionID";
			public const string PrevAntibioticLevel = "PrevAntibioticLevel";
			public const string IsAntibioticIndication = "IsAntibioticIndication";
			public const string IsInfectionSymptom = "IsInfectionSymptom";
			public const string InfectionSymptom = "InfectionSymptom";
			public const string IsComorbid = "IsComorbid";
			public const string RasprajaReason = "RasprajaReason";
			public const string IsPpraConsult = "IsPpraConsult";
			public const string NotPpraConsultReason = "NotPpraConsultReason";
			public const string ReferenceNo = "ReferenceNo";
			public const string PrescriptionNo = "PrescriptionNo";
			public const string IsExternalCultureLabTest = "IsExternalCultureLabTest";
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
			lock (typeof(RegistrationRasproMetadata))
			{
				if (RegistrationRasproMetadata.mapDelegates == null)
				{
					RegistrationRasproMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (RegistrationRasproMetadata.meta == null)
				{
					RegistrationRasproMetadata.meta = new RegistrationRasproMetadata();
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

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SeqNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RasproDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRRaspro", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AdviseByParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AbRestrictionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ActionNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AntibioticLevel", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SignImage", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("AntibioticIndication", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Comorbid", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ComorbidOther", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRWoundClassification", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SurgeryName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Diagnose", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherInfection", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrevAbRestrictionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrevAntibioticLevel", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsAntibioticIndication", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsInfectionSymptom", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("InfectionSymptom", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsComorbid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("RasprajaReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPpraConsult", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("NotPpraConsultReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrescriptionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsExternalCultureLabTest", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "RegistrationRaspro";
				meta.Destination = "RegistrationRaspro";
				meta.spInsert = "proc_RegistrationRasproInsert";
				meta.spUpdate = "proc_RegistrationRasproUpdate";
				meta.spDelete = "proc_RegistrationRasproDelete";
				meta.spLoadAll = "proc_RegistrationRasproLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationRasproLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationRasproMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
