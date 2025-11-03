/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/5/2022 1:09:59 AM
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
	abstract public class esRegistrationEsoCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationEsoCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "RegistrationEsoCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationEsoQuery query)
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
			this.InitQuery(query as esRegistrationEsoQuery);
		}
		#endregion

		virtual public RegistrationEso DetachEntity(RegistrationEso entity)
		{
			return base.DetachEntity(entity) as RegistrationEso;
		}

		virtual public RegistrationEso AttachEntity(RegistrationEso entity)
		{
			return base.AttachEntity(entity) as RegistrationEso;
		}

		virtual public void Combine(RegistrationEsoCollection collection)
		{
			base.Combine(collection);
		}

		new public RegistrationEso this[int index]
		{
			get
			{
				return base[index] as RegistrationEso;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationEso);
		}
	}

	[Serializable]
	abstract public class esRegistrationEso : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationEsoQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationEso()
		{
		}

		public esRegistrationEso(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 esoNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, esoNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, esoNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 esoNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, esoNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, esoNo);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 esoNo)
		{
			esRegistrationEsoQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.EsoNo == esoNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 esoNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("EsoNo", esoNo);
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
						case "EsoNo": this.str.EsoNo = (string)value; break;
						case "EsoDateTime": this.str.EsoDateTime = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "BodyWeight": this.str.BodyWeight = (string)value; break;
						case "MainDisease": this.str.MainDisease = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "PregnantStatus": this.str.PregnantStatus = (string)value; break;
						case "EsoComorbidities": this.str.EsoComorbidities = (string)value; break;
						case "EsoManifestations": this.str.EsoManifestations = (string)value; break;
						case "EsoOtherManifestation": this.str.EsoOtherManifestation = (string)value; break;
						case "StartDateTime": this.str.StartDateTime = (string)value; break;
						case "EndDateTime": this.str.EndDateTime = (string)value; break;
						case "SREsoStatus": this.str.SREsoStatus = (string)value; break;
						case "PrevEsoHistory": this.str.PrevEsoHistory = (string)value; break;
						case "LaboratoryTest": this.str.LaboratoryTest = (string)value; break;
						case "EsoScaleTotal": this.str.EsoScaleTotal = (string)value; break;
						case "IsDeleted": this.str.IsDeleted = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "AssessmentNote": this.str.AssessmentNote = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EsoNo":

							if (value == null || value is System.Int32)
								this.EsoNo = (System.Int32?)value;
							break;
						case "EsoDateTime":

							if (value == null || value is System.DateTime)
								this.EsoDateTime = (System.DateTime?)value;
							break;
						case "BodyWeight":

							if (value == null || value is System.Decimal)
								this.BodyWeight = (System.Decimal?)value;
							break;
						case "StartDateTime":

							if (value == null || value is System.DateTime)
								this.StartDateTime = (System.DateTime?)value;
							break;
						case "EndDateTime":

							if (value == null || value is System.DateTime)
								this.EndDateTime = (System.DateTime?)value;
							break;
						case "EsoScaleTotal":

							if (value == null || value is System.Int32)
								this.EsoScaleTotal = (System.Int32?)value;
							break;
						case "IsDeleted":

							if (value == null || value is System.Boolean)
								this.IsDeleted = (System.Boolean?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to RegistrationEso.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationEsoMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(RegistrationEsoMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.EsoNo
		/// </summary>
		virtual public System.Int32? EsoNo
		{
			get
			{
				return base.GetSystemInt32(RegistrationEsoMetadata.ColumnNames.EsoNo);
			}

			set
			{
				base.SetSystemInt32(RegistrationEsoMetadata.ColumnNames.EsoNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.EsoDateTime
		/// </summary>
		virtual public System.DateTime? EsoDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationEsoMetadata.ColumnNames.EsoDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationEsoMetadata.ColumnNames.EsoDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(RegistrationEsoMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(RegistrationEsoMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.BodyWeight
		/// </summary>
		virtual public System.Decimal? BodyWeight
		{
			get
			{
				return base.GetSystemDecimal(RegistrationEsoMetadata.ColumnNames.BodyWeight);
			}

			set
			{
				base.SetSystemDecimal(RegistrationEsoMetadata.ColumnNames.BodyWeight, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.MainDisease
		/// </summary>
		virtual public System.String MainDisease
		{
			get
			{
				return base.GetSystemString(RegistrationEsoMetadata.ColumnNames.MainDisease);
			}

			set
			{
				base.SetSystemString(RegistrationEsoMetadata.ColumnNames.MainDisease, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(RegistrationEsoMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(RegistrationEsoMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.PregnantStatus
		/// </summary>
		virtual public System.String PregnantStatus
		{
			get
			{
				return base.GetSystemString(RegistrationEsoMetadata.ColumnNames.PregnantStatus);
			}

			set
			{
				base.SetSystemString(RegistrationEsoMetadata.ColumnNames.PregnantStatus, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.EsoComorbidities
		/// </summary>
		virtual public System.String EsoComorbidities
		{
			get
			{
				return base.GetSystemString(RegistrationEsoMetadata.ColumnNames.EsoComorbidities);
			}

			set
			{
				base.SetSystemString(RegistrationEsoMetadata.ColumnNames.EsoComorbidities, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.EsoManifestations
		/// </summary>
		virtual public System.String EsoManifestations
		{
			get
			{
				return base.GetSystemString(RegistrationEsoMetadata.ColumnNames.EsoManifestations);
			}

			set
			{
				base.SetSystemString(RegistrationEsoMetadata.ColumnNames.EsoManifestations, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.EsoOtherManifestation
		/// </summary>
		virtual public System.String EsoOtherManifestation
		{
			get
			{
				return base.GetSystemString(RegistrationEsoMetadata.ColumnNames.EsoOtherManifestation);
			}

			set
			{
				base.SetSystemString(RegistrationEsoMetadata.ColumnNames.EsoOtherManifestation, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.StartDateTime
		/// </summary>
		virtual public System.DateTime? StartDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationEsoMetadata.ColumnNames.StartDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationEsoMetadata.ColumnNames.StartDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.EndDateTime
		/// </summary>
		virtual public System.DateTime? EndDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationEsoMetadata.ColumnNames.EndDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationEsoMetadata.ColumnNames.EndDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.SREsoStatus
		/// </summary>
		virtual public System.String SREsoStatus
		{
			get
			{
				return base.GetSystemString(RegistrationEsoMetadata.ColumnNames.SREsoStatus);
			}

			set
			{
				base.SetSystemString(RegistrationEsoMetadata.ColumnNames.SREsoStatus, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.PrevEsoHistory
		/// </summary>
		virtual public System.String PrevEsoHistory
		{
			get
			{
				return base.GetSystemString(RegistrationEsoMetadata.ColumnNames.PrevEsoHistory);
			}

			set
			{
				base.SetSystemString(RegistrationEsoMetadata.ColumnNames.PrevEsoHistory, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.LaboratoryTest
		/// </summary>
		virtual public System.String LaboratoryTest
		{
			get
			{
				return base.GetSystemString(RegistrationEsoMetadata.ColumnNames.LaboratoryTest);
			}

			set
			{
				base.SetSystemString(RegistrationEsoMetadata.ColumnNames.LaboratoryTest, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.EsoScaleTotal
		/// </summary>
		virtual public System.Int32? EsoScaleTotal
		{
			get
			{
				return base.GetSystemInt32(RegistrationEsoMetadata.ColumnNames.EsoScaleTotal);
			}

			set
			{
				base.SetSystemInt32(RegistrationEsoMetadata.ColumnNames.EsoScaleTotal, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(RegistrationEsoMetadata.ColumnNames.IsDeleted);
			}

			set
			{
				base.SetSystemBoolean(RegistrationEsoMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationEsoMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationEsoMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationEsoMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationEsoMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationEsoMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationEsoMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationEsoMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationEsoMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEso.AssessmentNote
		/// </summary>
		virtual public System.String AssessmentNote
		{
			get
			{
				return base.GetSystemString(RegistrationEsoMetadata.ColumnNames.AssessmentNote);
			}

			set
			{
				base.SetSystemString(RegistrationEsoMetadata.ColumnNames.AssessmentNote, value);
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
			public esStrings(esRegistrationEso entity)
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
			public System.String EsoNo
			{
				get
				{
					System.Int32? data = entity.EsoNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EsoNo = null;
					else entity.EsoNo = Convert.ToInt32(value);
				}
			}
			public System.String EsoDateTime
			{
				get
				{
					System.DateTime? data = entity.EsoDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EsoDateTime = null;
					else entity.EsoDateTime = Convert.ToDateTime(value);
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
			public System.String BodyWeight
			{
				get
				{
					System.Decimal? data = entity.BodyWeight;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BodyWeight = null;
					else entity.BodyWeight = Convert.ToDecimal(value);
				}
			}
			public System.String MainDisease
			{
				get
				{
					System.String data = entity.MainDisease;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MainDisease = null;
					else entity.MainDisease = Convert.ToString(value);
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
			public System.String PregnantStatus
			{
				get
				{
					System.String data = entity.PregnantStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PregnantStatus = null;
					else entity.PregnantStatus = Convert.ToString(value);
				}
			}
			public System.String EsoComorbidities
			{
				get
				{
					System.String data = entity.EsoComorbidities;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EsoComorbidities = null;
					else entity.EsoComorbidities = Convert.ToString(value);
				}
			}
			public System.String EsoManifestations
			{
				get
				{
					System.String data = entity.EsoManifestations;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EsoManifestations = null;
					else entity.EsoManifestations = Convert.ToString(value);
				}
			}
			public System.String EsoOtherManifestation
			{
				get
				{
					System.String data = entity.EsoOtherManifestation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EsoOtherManifestation = null;
					else entity.EsoOtherManifestation = Convert.ToString(value);
				}
			}
			public System.String StartDateTime
			{
				get
				{
					System.DateTime? data = entity.StartDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartDateTime = null;
					else entity.StartDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String EndDateTime
			{
				get
				{
					System.DateTime? data = entity.EndDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndDateTime = null;
					else entity.EndDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String SREsoStatus
			{
				get
				{
					System.String data = entity.SREsoStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREsoStatus = null;
					else entity.SREsoStatus = Convert.ToString(value);
				}
			}
			public System.String PrevEsoHistory
			{
				get
				{
					System.String data = entity.PrevEsoHistory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrevEsoHistory = null;
					else entity.PrevEsoHistory = Convert.ToString(value);
				}
			}
			public System.String LaboratoryTest
			{
				get
				{
					System.String data = entity.LaboratoryTest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LaboratoryTest = null;
					else entity.LaboratoryTest = Convert.ToString(value);
				}
			}
			public System.String EsoScaleTotal
			{
				get
				{
					System.Int32? data = entity.EsoScaleTotal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EsoScaleTotal = null;
					else entity.EsoScaleTotal = Convert.ToInt32(value);
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
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
				}
			}
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
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
			public System.String AssessmentNote
			{
				get
				{
					System.String data = entity.AssessmentNote;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssessmentNote = null;
					else entity.AssessmentNote = Convert.ToString(value);
				}
			}
			private esRegistrationEso entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationEsoQuery query)
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
				throw new Exception("esRegistrationEso can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationEso : esRegistrationEso
	{
	}

	[Serializable]
	abstract public class esRegistrationEsoQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return RegistrationEsoMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem EsoNo
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.EsoNo, esSystemType.Int32);
			}
		}

		public esQueryItem EsoDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.EsoDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem BodyWeight
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.BodyWeight, esSystemType.Decimal);
			}
		}

		public esQueryItem MainDisease
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.MainDisease, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem PregnantStatus
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.PregnantStatus, esSystemType.String);
			}
		}

		public esQueryItem EsoComorbidities
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.EsoComorbidities, esSystemType.String);
			}
		}

		public esQueryItem EsoManifestations
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.EsoManifestations, esSystemType.String);
			}
		}

		public esQueryItem EsoOtherManifestation
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.EsoOtherManifestation, esSystemType.String);
			}
		}

		public esQueryItem StartDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.StartDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem EndDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.EndDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem SREsoStatus
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.SREsoStatus, esSystemType.String);
			}
		}

		public esQueryItem PrevEsoHistory
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.PrevEsoHistory, esSystemType.String);
			}
		}

		public esQueryItem LaboratoryTest
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.LaboratoryTest, esSystemType.String);
			}
		}

		public esQueryItem EsoScaleTotal
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.EsoScaleTotal, esSystemType.Int32);
			}
		}

		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem AssessmentNote
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoMetadata.ColumnNames.AssessmentNote, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationEsoCollection")]
	public partial class RegistrationEsoCollection : esRegistrationEsoCollection, IEnumerable<RegistrationEso>
	{
		public RegistrationEsoCollection()
		{

		}

		public static implicit operator List<RegistrationEso>(RegistrationEsoCollection coll)
		{
			List<RegistrationEso> list = new List<RegistrationEso>();

			foreach (RegistrationEso emp in coll)
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
				return RegistrationEsoMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationEsoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationEso(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationEso();
		}

		#endregion

		[BrowsableAttribute(false)]
		public RegistrationEsoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationEsoQuery();
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
		public bool Load(RegistrationEsoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationEso AddNew()
		{
			RegistrationEso entity = base.AddNewEntity() as RegistrationEso;

			return entity;
		}
		public RegistrationEso FindByPrimaryKey(String registrationNo, Int32 esoNo)
		{
			return base.FindByPrimaryKey(registrationNo, esoNo) as RegistrationEso;
		}

		#region IEnumerable< RegistrationEso> Members

		IEnumerator<RegistrationEso> IEnumerable<RegistrationEso>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationEso;
			}
		}

		#endregion

		private RegistrationEsoQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationEso' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationEso ({RegistrationNo, EsoNo})")]
	[Serializable]
	public partial class RegistrationEso : esRegistrationEso
	{
		public RegistrationEso()
		{
		}

		public RegistrationEso(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationEsoMetadata.Meta();
			}
		}

		override protected esRegistrationEsoQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationEsoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public RegistrationEsoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationEsoQuery();
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
		public bool Load(RegistrationEsoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private RegistrationEsoQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationEsoQuery : esRegistrationEsoQuery
	{
		public RegistrationEsoQuery()
		{

		}

		public RegistrationEsoQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "RegistrationEsoQuery";
		}
	}

	[Serializable]
	public partial class RegistrationEsoMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationEsoMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.EsoNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.EsoNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.EsoDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.EsoDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.ServiceUnitID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.BodyWeight, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.BodyWeight;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.MainDisease, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.MainDisease;
			c.CharacterMaxLength = 400;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.ParamedicID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.PregnantStatus, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.PregnantStatus;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.EsoComorbidities, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.EsoComorbidities;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.EsoManifestations, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.EsoManifestations;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.EsoOtherManifestation, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.EsoOtherManifestation;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.StartDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.StartDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.EndDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.EndDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.SREsoStatus, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.SREsoStatus;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.PrevEsoHistory, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.PrevEsoHistory;
			c.CharacterMaxLength = 400;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.LaboratoryTest, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.LaboratoryTest;
			c.CharacterMaxLength = 400;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.EsoScaleTotal, 16, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.EsoScaleTotal;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.IsDeleted, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.CreatedByUserID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.CreatedDateTime, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.LastUpdateByUserID, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.LastUpdateDateTime, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoMetadata.ColumnNames.AssessmentNote, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoMetadata.PropertyNames.AssessmentNote;
			c.CharacterMaxLength = 800;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public RegistrationEsoMetadata Meta()
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
			public const string EsoNo = "EsoNo";
			public const string EsoDateTime = "EsoDateTime";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string BodyWeight = "BodyWeight";
			public const string MainDisease = "MainDisease";
			public const string ParamedicID = "ParamedicID";
			public const string PregnantStatus = "PregnantStatus";
			public const string EsoComorbidities = "EsoComorbidities";
			public const string EsoManifestations = "EsoManifestations";
			public const string EsoOtherManifestation = "EsoOtherManifestation";
			public const string StartDateTime = "StartDateTime";
			public const string EndDateTime = "EndDateTime";
			public const string SREsoStatus = "SREsoStatus";
			public const string PrevEsoHistory = "PrevEsoHistory";
			public const string LaboratoryTest = "LaboratoryTest";
			public const string EsoScaleTotal = "EsoScaleTotal";
			public const string IsDeleted = "IsDeleted";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string AssessmentNote = "AssessmentNote";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string EsoNo = "EsoNo";
			public const string EsoDateTime = "EsoDateTime";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string BodyWeight = "BodyWeight";
			public const string MainDisease = "MainDisease";
			public const string ParamedicID = "ParamedicID";
			public const string PregnantStatus = "PregnantStatus";
			public const string EsoComorbidities = "EsoComorbidities";
			public const string EsoManifestations = "EsoManifestations";
			public const string EsoOtherManifestation = "EsoOtherManifestation";
			public const string StartDateTime = "StartDateTime";
			public const string EndDateTime = "EndDateTime";
			public const string SREsoStatus = "SREsoStatus";
			public const string PrevEsoHistory = "PrevEsoHistory";
			public const string LaboratoryTest = "LaboratoryTest";
			public const string EsoScaleTotal = "EsoScaleTotal";
			public const string IsDeleted = "IsDeleted";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string AssessmentNote = "AssessmentNote";
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
			lock (typeof(RegistrationEsoMetadata))
			{
				if (RegistrationEsoMetadata.mapDelegates == null)
				{
					RegistrationEsoMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (RegistrationEsoMetadata.meta == null)
				{
					RegistrationEsoMetadata.meta = new RegistrationEsoMetadata();
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
				meta.AddTypeMap("EsoNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EsoDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BodyWeight", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("MainDisease", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PregnantStatus", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("EsoComorbidities", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EsoManifestations", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EsoOtherManifestation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EndDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SREsoStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrevEsoHistory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LaboratoryTest", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EsoScaleTotal", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("AssessmentNote", new esTypeMap("varchar", "System.String"));


				meta.Source = "RegistrationEso";
				meta.Destination = "RegistrationEso";
				meta.spInsert = "proc_RegistrationEsoInsert";
				meta.spUpdate = "proc_RegistrationEsoUpdate";
				meta.spDelete = "proc_RegistrationEsoDelete";
				meta.spLoadAll = "proc_RegistrationEsoLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationEsoLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationEsoMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
