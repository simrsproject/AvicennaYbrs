/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/21/2020 9:48:42 AM
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
	abstract public class esIntermBillCollection : esEntityCollectionWAuditLog
	{
		public esIntermBillCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "IntermBillCollection";
		}

		#region Query Logic
		protected void InitQuery(esIntermBillQuery query)
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
			this.InitQuery(query as esIntermBillQuery);
		}
		#endregion

		virtual public IntermBill DetachEntity(IntermBill entity)
		{
			return base.DetachEntity(entity) as IntermBill;
		}

		virtual public IntermBill AttachEntity(IntermBill entity)
		{
			return base.AttachEntity(entity) as IntermBill;
		}

		virtual public void Combine(IntermBillCollection collection)
		{
			base.Combine(collection);
		}

		new public IntermBill this[int index]
		{
			get
			{
				return base[index] as IntermBill;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(IntermBill);
		}
	}

	[Serializable]
	abstract public class esIntermBill : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esIntermBillQuery GetDynamicQuery()
		{
			return null;
		}

		public esIntermBill()
		{
		}

		public esIntermBill(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String intermBillNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(intermBillNo);
			else
				return LoadByPrimaryKeyStoredProcedure(intermBillNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String intermBillNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(intermBillNo);
			else
				return LoadByPrimaryKeyStoredProcedure(intermBillNo);
		}

		private bool LoadByPrimaryKeyDynamic(String intermBillNo)
		{
			esIntermBillQuery query = this.GetDynamicQuery();
			query.Where(query.IntermBillNo == intermBillNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String intermBillNo)
		{
			esParameters parms = new esParameters();
			parms.Add("IntermBillNo", intermBillNo);
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
						case "IntermBillNo": this.str.IntermBillNo = (string)value; break;
						case "IntermBillDate": this.str.IntermBillDate = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "StartDate": this.str.StartDate = (string)value; break;
						case "EndDate": this.str.EndDate = (string)value; break;
						case "PatientAmount": this.str.PatientAmount = (string)value; break;
						case "GuarantorAmount": this.str.GuarantorAmount = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "AskesCoveredSeqNo": this.str.AskesCoveredSeqNo = (string)value; break;
						case "JournalIncomePaymentNo": this.str.JournalIncomePaymentNo = (string)value; break;
						case "AdministrationAmount": this.str.AdministrationAmount = (string)value; break;
						case "GuarantorAdministrationAmount": this.str.GuarantorAdministrationAmount = (string)value; break;
						case "DiscAdmPatient": this.str.DiscAdmPatient = (string)value; break;
						case "DiscAdmGuarantor": this.str.DiscAdmGuarantor = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IntermBillDate":

							if (value == null || value is System.DateTime)
								this.IntermBillDate = (System.DateTime?)value;
							break;
						case "StartDate":

							if (value == null || value is System.DateTime)
								this.StartDate = (System.DateTime?)value;
							break;
						case "EndDate":

							if (value == null || value is System.DateTime)
								this.EndDate = (System.DateTime?)value;
							break;
						case "PatientAmount":

							if (value == null || value is System.Decimal)
								this.PatientAmount = (System.Decimal?)value;
							break;
						case "GuarantorAmount":

							if (value == null || value is System.Decimal)
								this.GuarantorAmount = (System.Decimal?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "AdministrationAmount":

							if (value == null || value is System.Decimal)
								this.AdministrationAmount = (System.Decimal?)value;
							break;
						case "GuarantorAdministrationAmount":

							if (value == null || value is System.Decimal)
								this.GuarantorAdministrationAmount = (System.Decimal?)value;
							break;
						case "DiscAdmPatient":

							if (value == null || value is System.Decimal)
								this.DiscAdmPatient = (System.Decimal?)value;
							break;
						case "DiscAdmGuarantor":

							if (value == null || value is System.Decimal)
								this.DiscAdmGuarantor = (System.Decimal?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to IntermBill.IntermBillNo
		/// </summary>
		virtual public System.String IntermBillNo
		{
			get
			{
				return base.GetSystemString(IntermBillMetadata.ColumnNames.IntermBillNo);
			}

			set
			{
				base.SetSystemString(IntermBillMetadata.ColumnNames.IntermBillNo, value);
			}
		}
		/// <summary>
		/// Maps to IntermBill.IntermBillDate
		/// </summary>
		virtual public System.DateTime? IntermBillDate
		{
			get
			{
				return base.GetSystemDateTime(IntermBillMetadata.ColumnNames.IntermBillDate);
			}

			set
			{
				base.SetSystemDateTime(IntermBillMetadata.ColumnNames.IntermBillDate, value);
			}
		}
		/// <summary>
		/// Maps to IntermBill.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(IntermBillMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(IntermBillMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to IntermBill.StartDate
		/// </summary>
		virtual public System.DateTime? StartDate
		{
			get
			{
				return base.GetSystemDateTime(IntermBillMetadata.ColumnNames.StartDate);
			}

			set
			{
				base.SetSystemDateTime(IntermBillMetadata.ColumnNames.StartDate, value);
			}
		}
		/// <summary>
		/// Maps to IntermBill.EndDate
		/// </summary>
		virtual public System.DateTime? EndDate
		{
			get
			{
				return base.GetSystemDateTime(IntermBillMetadata.ColumnNames.EndDate);
			}

			set
			{
				base.SetSystemDateTime(IntermBillMetadata.ColumnNames.EndDate, value);
			}
		}
		/// <summary>
		/// Maps to IntermBill.PatientAmount
		/// </summary>
		virtual public System.Decimal? PatientAmount
		{
			get
			{
				return base.GetSystemDecimal(IntermBillMetadata.ColumnNames.PatientAmount);
			}

			set
			{
				base.SetSystemDecimal(IntermBillMetadata.ColumnNames.PatientAmount, value);
			}
		}
		/// <summary>
		/// Maps to IntermBill.GuarantorAmount
		/// </summary>
		virtual public System.Decimal? GuarantorAmount
		{
			get
			{
				return base.GetSystemDecimal(IntermBillMetadata.ColumnNames.GuarantorAmount);
			}

			set
			{
				base.SetSystemDecimal(IntermBillMetadata.ColumnNames.GuarantorAmount, value);
			}
		}
		/// <summary>
		/// Maps to IntermBill.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(IntermBillMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(IntermBillMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to IntermBill.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(IntermBillMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(IntermBillMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to IntermBill.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(IntermBillMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(IntermBillMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to IntermBill.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(IntermBillMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(IntermBillMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to IntermBill.AskesCoveredSeqNo
		/// </summary>
		virtual public System.String AskesCoveredSeqNo
		{
			get
			{
				return base.GetSystemString(IntermBillMetadata.ColumnNames.AskesCoveredSeqNo);
			}

			set
			{
				base.SetSystemString(IntermBillMetadata.ColumnNames.AskesCoveredSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to IntermBill.JournalIncomePaymentNo
		/// </summary>
		virtual public System.String JournalIncomePaymentNo
		{
			get
			{
				return base.GetSystemString(IntermBillMetadata.ColumnNames.JournalIncomePaymentNo);
			}

			set
			{
				base.SetSystemString(IntermBillMetadata.ColumnNames.JournalIncomePaymentNo, value);
			}
		}
		/// <summary>
		/// Maps to IntermBill.AdministrationAmount
		/// </summary>
		virtual public System.Decimal? AdministrationAmount
		{
			get
			{
				return base.GetSystemDecimal(IntermBillMetadata.ColumnNames.AdministrationAmount);
			}

			set
			{
				base.SetSystemDecimal(IntermBillMetadata.ColumnNames.AdministrationAmount, value);
			}
		}
		/// <summary>
		/// Maps to IntermBill.GuarantorAdministrationAmount
		/// </summary>
		virtual public System.Decimal? GuarantorAdministrationAmount
		{
			get
			{
				return base.GetSystemDecimal(IntermBillMetadata.ColumnNames.GuarantorAdministrationAmount);
			}

			set
			{
				base.SetSystemDecimal(IntermBillMetadata.ColumnNames.GuarantorAdministrationAmount, value);
			}
		}
		/// <summary>
		/// Maps to IntermBill.DiscAdmPatient
		/// </summary>
		virtual public System.Decimal? DiscAdmPatient
		{
			get
			{
				return base.GetSystemDecimal(IntermBillMetadata.ColumnNames.DiscAdmPatient);
			}

			set
			{
				base.SetSystemDecimal(IntermBillMetadata.ColumnNames.DiscAdmPatient, value);
			}
		}
		/// <summary>
		/// Maps to IntermBill.DiscAdmGuarantor
		/// </summary>
		virtual public System.Decimal? DiscAdmGuarantor
		{
			get
			{
				return base.GetSystemDecimal(IntermBillMetadata.ColumnNames.DiscAdmGuarantor);
			}

			set
			{
				base.SetSystemDecimal(IntermBillMetadata.ColumnNames.DiscAdmGuarantor, value);
			}
		}
		/// <summary>
		/// Maps to IntermBill.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(IntermBillMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(IntermBillMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to IntermBill.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(IntermBillMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(IntermBillMetadata.ColumnNames.CreatedByUserID, value);
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
			public esStrings(esIntermBill entity)
			{
				this.entity = entity;
			}
			public System.String IntermBillNo
			{
				get
				{
					System.String data = entity.IntermBillNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IntermBillNo = null;
					else entity.IntermBillNo = Convert.ToString(value);
				}
			}
			public System.String IntermBillDate
			{
				get
				{
					System.DateTime? data = entity.IntermBillDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IntermBillDate = null;
					else entity.IntermBillDate = Convert.ToDateTime(value);
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
			public System.String StartDate
			{
				get
				{
					System.DateTime? data = entity.StartDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartDate = null;
					else entity.StartDate = Convert.ToDateTime(value);
				}
			}
			public System.String EndDate
			{
				get
				{
					System.DateTime? data = entity.EndDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndDate = null;
					else entity.EndDate = Convert.ToDateTime(value);
				}
			}
			public System.String PatientAmount
			{
				get
				{
					System.Decimal? data = entity.PatientAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientAmount = null;
					else entity.PatientAmount = Convert.ToDecimal(value);
				}
			}
			public System.String GuarantorAmount
			{
				get
				{
					System.Decimal? data = entity.GuarantorAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorAmount = null;
					else entity.GuarantorAmount = Convert.ToDecimal(value);
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
			public System.String AskesCoveredSeqNo
			{
				get
				{
					System.String data = entity.AskesCoveredSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AskesCoveredSeqNo = null;
					else entity.AskesCoveredSeqNo = Convert.ToString(value);
				}
			}
			public System.String JournalIncomePaymentNo
			{
				get
				{
					System.String data = entity.JournalIncomePaymentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalIncomePaymentNo = null;
					else entity.JournalIncomePaymentNo = Convert.ToString(value);
				}
			}
			public System.String AdministrationAmount
			{
				get
				{
					System.Decimal? data = entity.AdministrationAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdministrationAmount = null;
					else entity.AdministrationAmount = Convert.ToDecimal(value);
				}
			}
			public System.String GuarantorAdministrationAmount
			{
				get
				{
					System.Decimal? data = entity.GuarantorAdministrationAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorAdministrationAmount = null;
					else entity.GuarantorAdministrationAmount = Convert.ToDecimal(value);
				}
			}
			public System.String DiscAdmPatient
			{
				get
				{
					System.Decimal? data = entity.DiscAdmPatient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscAdmPatient = null;
					else entity.DiscAdmPatient = Convert.ToDecimal(value);
				}
			}
			public System.String DiscAdmGuarantor
			{
				get
				{
					System.Decimal? data = entity.DiscAdmGuarantor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscAdmGuarantor = null;
					else entity.DiscAdmGuarantor = Convert.ToDecimal(value);
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
			private esIntermBill entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esIntermBillQuery query)
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
				throw new Exception("esIntermBill can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class IntermBill : esIntermBill
	{
	}

	[Serializable]
	abstract public class esIntermBillQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return IntermBillMetadata.Meta();
			}
		}

		public esQueryItem IntermBillNo
		{
			get
			{
				return new esQueryItem(this, IntermBillMetadata.ColumnNames.IntermBillNo, esSystemType.String);
			}
		}

		public esQueryItem IntermBillDate
		{
			get
			{
				return new esQueryItem(this, IntermBillMetadata.ColumnNames.IntermBillDate, esSystemType.DateTime);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, IntermBillMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem StartDate
		{
			get
			{
				return new esQueryItem(this, IntermBillMetadata.ColumnNames.StartDate, esSystemType.DateTime);
			}
		}

		public esQueryItem EndDate
		{
			get
			{
				return new esQueryItem(this, IntermBillMetadata.ColumnNames.EndDate, esSystemType.DateTime);
			}
		}

		public esQueryItem PatientAmount
		{
			get
			{
				return new esQueryItem(this, IntermBillMetadata.ColumnNames.PatientAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem GuarantorAmount
		{
			get
			{
				return new esQueryItem(this, IntermBillMetadata.ColumnNames.GuarantorAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, IntermBillMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, IntermBillMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, IntermBillMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, IntermBillMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem AskesCoveredSeqNo
		{
			get
			{
				return new esQueryItem(this, IntermBillMetadata.ColumnNames.AskesCoveredSeqNo, esSystemType.String);
			}
		}

		public esQueryItem JournalIncomePaymentNo
		{
			get
			{
				return new esQueryItem(this, IntermBillMetadata.ColumnNames.JournalIncomePaymentNo, esSystemType.String);
			}
		}

		public esQueryItem AdministrationAmount
		{
			get
			{
				return new esQueryItem(this, IntermBillMetadata.ColumnNames.AdministrationAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem GuarantorAdministrationAmount
		{
			get
			{
				return new esQueryItem(this, IntermBillMetadata.ColumnNames.GuarantorAdministrationAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem DiscAdmPatient
		{
			get
			{
				return new esQueryItem(this, IntermBillMetadata.ColumnNames.DiscAdmPatient, esSystemType.Decimal);
			}
		}

		public esQueryItem DiscAdmGuarantor
		{
			get
			{
				return new esQueryItem(this, IntermBillMetadata.ColumnNames.DiscAdmGuarantor, esSystemType.Decimal);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, IntermBillMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, IntermBillMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("IntermBillCollection")]
	public partial class IntermBillCollection : esIntermBillCollection, IEnumerable<IntermBill>
	{
		public IntermBillCollection()
		{

		}

		public static implicit operator List<IntermBill>(IntermBillCollection coll)
		{
			List<IntermBill> list = new List<IntermBill>();

			foreach (IntermBill emp in coll)
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
				return IntermBillMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new IntermBillQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new IntermBill(row);
		}

		override protected esEntity CreateEntity()
		{
			return new IntermBill();
		}

		#endregion

		[BrowsableAttribute(false)]
		public IntermBillQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new IntermBillQuery();
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
		public bool Load(IntermBillQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public IntermBill AddNew()
		{
			IntermBill entity = base.AddNewEntity() as IntermBill;

			return entity;
		}
		public IntermBill FindByPrimaryKey(String intermBillNo)
		{
			return base.FindByPrimaryKey(intermBillNo) as IntermBill;
		}

		#region IEnumerable< IntermBill> Members

		IEnumerator<IntermBill> IEnumerable<IntermBill>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as IntermBill;
			}
		}

		#endregion

		private IntermBillQuery query;
	}


	/// <summary>
	/// Encapsulates the 'IntermBill' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("IntermBill ({IntermBillNo})")]
	[Serializable]
	public partial class IntermBill : esIntermBill
	{
		public IntermBill()
		{
		}

		public IntermBill(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return IntermBillMetadata.Meta();
			}
		}

		override protected esIntermBillQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new IntermBillQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public IntermBillQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new IntermBillQuery();
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
		public bool Load(IntermBillQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private IntermBillQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class IntermBillQuery : esIntermBillQuery
	{
		public IntermBillQuery()
		{

		}

		public IntermBillQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "IntermBillQuery";
		}
	}

	[Serializable]
	public partial class IntermBillMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected IntermBillMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(IntermBillMetadata.ColumnNames.IntermBillNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = IntermBillMetadata.PropertyNames.IntermBillNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(IntermBillMetadata.ColumnNames.IntermBillDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = IntermBillMetadata.PropertyNames.IntermBillDate;
			_columns.Add(c);

			c = new esColumnMetadata(IntermBillMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = IntermBillMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(IntermBillMetadata.ColumnNames.StartDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = IntermBillMetadata.PropertyNames.StartDate;
			_columns.Add(c);

			c = new esColumnMetadata(IntermBillMetadata.ColumnNames.EndDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = IntermBillMetadata.PropertyNames.EndDate;
			_columns.Add(c);

			c = new esColumnMetadata(IntermBillMetadata.ColumnNames.PatientAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = IntermBillMetadata.PropertyNames.PatientAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(IntermBillMetadata.ColumnNames.GuarantorAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = IntermBillMetadata.PropertyNames.GuarantorAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(IntermBillMetadata.ColumnNames.IsApproved, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = IntermBillMetadata.PropertyNames.IsApproved;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(IntermBillMetadata.ColumnNames.IsVoid, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = IntermBillMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(IntermBillMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = IntermBillMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(IntermBillMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = IntermBillMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(IntermBillMetadata.ColumnNames.AskesCoveredSeqNo, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = IntermBillMetadata.PropertyNames.AskesCoveredSeqNo;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(IntermBillMetadata.ColumnNames.JournalIncomePaymentNo, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = IntermBillMetadata.PropertyNames.JournalIncomePaymentNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(IntermBillMetadata.ColumnNames.AdministrationAmount, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = IntermBillMetadata.PropertyNames.AdministrationAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(IntermBillMetadata.ColumnNames.GuarantorAdministrationAmount, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = IntermBillMetadata.PropertyNames.GuarantorAdministrationAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(IntermBillMetadata.ColumnNames.DiscAdmPatient, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = IntermBillMetadata.PropertyNames.DiscAdmPatient;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(IntermBillMetadata.ColumnNames.DiscAdmGuarantor, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = IntermBillMetadata.PropertyNames.DiscAdmGuarantor;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(IntermBillMetadata.ColumnNames.CreatedDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = IntermBillMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(IntermBillMetadata.ColumnNames.CreatedByUserID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = IntermBillMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public IntermBillMetadata Meta()
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
			public const string IntermBillNo = "IntermBillNo";
			public const string IntermBillDate = "IntermBillDate";
			public const string RegistrationNo = "RegistrationNo";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string PatientAmount = "PatientAmount";
			public const string GuarantorAmount = "GuarantorAmount";
			public const string IsApproved = "IsApproved";
			public const string IsVoid = "IsVoid";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string AskesCoveredSeqNo = "AskesCoveredSeqNo";
			public const string JournalIncomePaymentNo = "JournalIncomePaymentNo";
			public const string AdministrationAmount = "AdministrationAmount";
			public const string GuarantorAdministrationAmount = "GuarantorAdministrationAmount";
			public const string DiscAdmPatient = "DiscAdmPatient";
			public const string DiscAdmGuarantor = "DiscAdmGuarantor";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string IntermBillNo = "IntermBillNo";
			public const string IntermBillDate = "IntermBillDate";
			public const string RegistrationNo = "RegistrationNo";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string PatientAmount = "PatientAmount";
			public const string GuarantorAmount = "GuarantorAmount";
			public const string IsApproved = "IsApproved";
			public const string IsVoid = "IsVoid";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string AskesCoveredSeqNo = "AskesCoveredSeqNo";
			public const string JournalIncomePaymentNo = "JournalIncomePaymentNo";
			public const string AdministrationAmount = "AdministrationAmount";
			public const string GuarantorAdministrationAmount = "GuarantorAdministrationAmount";
			public const string DiscAdmPatient = "DiscAdmPatient";
			public const string DiscAdmGuarantor = "DiscAdmGuarantor";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
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
			lock (typeof(IntermBillMetadata))
			{
				if (IntermBillMetadata.mapDelegates == null)
				{
					IntermBillMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (IntermBillMetadata.meta == null)
				{
					IntermBillMetadata.meta = new IntermBillMetadata();
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

				meta.AddTypeMap("IntermBillNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IntermBillDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EndDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PatientAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("GuarantorAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AskesCoveredSeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JournalIncomePaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AdministrationAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("GuarantorAdministrationAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DiscAdmPatient", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DiscAdmGuarantor", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "IntermBill";
				meta.Destination = "IntermBill";
				meta.spInsert = "proc_IntermBillInsert";
				meta.spUpdate = "proc_IntermBillUpdate";
				meta.spDelete = "proc_IntermBillDelete";
				meta.spLoadAll = "proc_IntermBillLoadAll";
				meta.spLoadByPrimaryKey = "proc_IntermBillLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private IntermBillMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
