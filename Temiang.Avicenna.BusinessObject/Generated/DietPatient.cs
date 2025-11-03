/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/19/2022 11:36:36 AM
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
	abstract public class esDietPatientCollection : esEntityCollectionWAuditLog
	{
		public esDietPatientCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "DietPatientCollection";
		}

		#region Query Logic
		protected void InitQuery(esDietPatientQuery query)
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
			this.InitQuery(query as esDietPatientQuery);
		}
		#endregion

		virtual public DietPatient DetachEntity(DietPatient entity)
		{
			return base.DetachEntity(entity) as DietPatient;
		}

		virtual public DietPatient AttachEntity(DietPatient entity)
		{
			return base.AttachEntity(entity) as DietPatient;
		}

		virtual public void Combine(DietPatientCollection collection)
		{
			base.Combine(collection);
		}

		new public DietPatient this[int index]
		{
			get
			{
				return base[index] as DietPatient;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(DietPatient);
		}
	}

	[Serializable]
	abstract public class esDietPatient : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDietPatientQuery GetDynamicQuery()
		{
			return null;
		}

		public esDietPatient()
		{
		}

		public esDietPatient(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo)
		{
			esDietPatientQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "Height": this.str.Height = (string)value; break;
						case "Weight": this.str.Weight = (string)value; break;
						case "BodyMassIndex": this.str.BodyMassIndex = (string)value; break;
						case "Diagnose": this.str.Diagnose = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "EffectiveStartDate": this.str.EffectiveStartDate = (string)value; break;
						case "EffectiveStartTime": this.str.EffectiveStartTime = (string)value; break;
						case "EffectiveEndDate": this.str.EffectiveEndDate = (string)value; break;
						case "EffectiveEndTime": this.str.EffectiveEndTime = (string)value; break;
						case "FormOfFood": this.str.FormOfFood = (string)value; break;
						case "IsSpecialCondition": this.str.IsSpecialCondition = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "Muac": this.str.Muac = (string)value; break;
						case "Ulna": this.str.Ulna = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "Height":

							if (value == null || value is System.Decimal)
								this.Height = (System.Decimal?)value;
							break;
						case "Weight":

							if (value == null || value is System.Decimal)
								this.Weight = (System.Decimal?)value;
							break;
						case "BodyMassIndex":

							if (value == null || value is System.Decimal)
								this.BodyMassIndex = (System.Decimal?)value;
							break;
						case "EffectiveStartDate":

							if (value == null || value is System.DateTime)
								this.EffectiveStartDate = (System.DateTime?)value;
							break;
						case "EffectiveEndDate":

							if (value == null || value is System.DateTime)
								this.EffectiveEndDate = (System.DateTime?)value;
							break;
						case "IsSpecialCondition":

							if (value == null || value is System.Boolean)
								this.IsSpecialCondition = (System.Boolean?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDateTime":

							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "Muac":

							if (value == null || value is System.Decimal)
								this.Muac = (System.Decimal?)value;
							break;
						case "Ulna":

							if (value == null || value is System.Decimal)
								this.Ulna = (System.Decimal?)value;
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
		/// Maps to DietPatient.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(DietPatientMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(DietPatientMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to DietPatient.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(DietPatientMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(DietPatientMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to DietPatient.Height
		/// </summary>
		virtual public System.Decimal? Height
		{
			get
			{
				return base.GetSystemDecimal(DietPatientMetadata.ColumnNames.Height);
			}

			set
			{
				base.SetSystemDecimal(DietPatientMetadata.ColumnNames.Height, value);
			}
		}
		/// <summary>
		/// Maps to DietPatient.Weight
		/// </summary>
		virtual public System.Decimal? Weight
		{
			get
			{
				return base.GetSystemDecimal(DietPatientMetadata.ColumnNames.Weight);
			}

			set
			{
				base.SetSystemDecimal(DietPatientMetadata.ColumnNames.Weight, value);
			}
		}
		/// <summary>
		/// Maps to DietPatient.BodyMassIndex
		/// </summary>
		virtual public System.Decimal? BodyMassIndex
		{
			get
			{
				return base.GetSystemDecimal(DietPatientMetadata.ColumnNames.BodyMassIndex);
			}

			set
			{
				base.SetSystemDecimal(DietPatientMetadata.ColumnNames.BodyMassIndex, value);
			}
		}
		/// <summary>
		/// Maps to DietPatient.Diagnose
		/// </summary>
		virtual public System.String Diagnose
		{
			get
			{
				return base.GetSystemString(DietPatientMetadata.ColumnNames.Diagnose);
			}

			set
			{
				base.SetSystemString(DietPatientMetadata.ColumnNames.Diagnose, value);
			}
		}
		/// <summary>
		/// Maps to DietPatient.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(DietPatientMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(DietPatientMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to DietPatient.EffectiveStartDate
		/// </summary>
		virtual public System.DateTime? EffectiveStartDate
		{
			get
			{
				return base.GetSystemDateTime(DietPatientMetadata.ColumnNames.EffectiveStartDate);
			}

			set
			{
				base.SetSystemDateTime(DietPatientMetadata.ColumnNames.EffectiveStartDate, value);
			}
		}
		/// <summary>
		/// Maps to DietPatient.EffectiveStartTime
		/// </summary>
		virtual public System.String EffectiveStartTime
		{
			get
			{
				return base.GetSystemString(DietPatientMetadata.ColumnNames.EffectiveStartTime);
			}

			set
			{
				base.SetSystemString(DietPatientMetadata.ColumnNames.EffectiveStartTime, value);
			}
		}
		/// <summary>
		/// Maps to DietPatient.EffectiveEndDate
		/// </summary>
		virtual public System.DateTime? EffectiveEndDate
		{
			get
			{
				return base.GetSystemDateTime(DietPatientMetadata.ColumnNames.EffectiveEndDate);
			}

			set
			{
				base.SetSystemDateTime(DietPatientMetadata.ColumnNames.EffectiveEndDate, value);
			}
		}
		/// <summary>
		/// Maps to DietPatient.EffectiveEndTime
		/// </summary>
		virtual public System.String EffectiveEndTime
		{
			get
			{
				return base.GetSystemString(DietPatientMetadata.ColumnNames.EffectiveEndTime);
			}

			set
			{
				base.SetSystemString(DietPatientMetadata.ColumnNames.EffectiveEndTime, value);
			}
		}
		/// <summary>
		/// Maps to DietPatient.FormOfFood
		/// </summary>
		virtual public System.String FormOfFood
		{
			get
			{
				return base.GetSystemString(DietPatientMetadata.ColumnNames.FormOfFood);
			}

			set
			{
				base.SetSystemString(DietPatientMetadata.ColumnNames.FormOfFood, value);
			}
		}
		/// <summary>
		/// Maps to DietPatient.IsSpecialCondition
		/// </summary>
		virtual public System.Boolean? IsSpecialCondition
		{
			get
			{
				return base.GetSystemBoolean(DietPatientMetadata.ColumnNames.IsSpecialCondition);
			}

			set
			{
				base.SetSystemBoolean(DietPatientMetadata.ColumnNames.IsSpecialCondition, value);
			}
		}
		/// <summary>
		/// Maps to DietPatient.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(DietPatientMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(DietPatientMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to DietPatient.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(DietPatientMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(DietPatientMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to DietPatient.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(DietPatientMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(DietPatientMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to DietPatient.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(DietPatientMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(DietPatientMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to DietPatient.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(DietPatientMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(DietPatientMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to DietPatient.Muac
		/// </summary>
		virtual public System.Decimal? Muac
		{
			get
			{
				return base.GetSystemDecimal(DietPatientMetadata.ColumnNames.Muac);
			}

			set
			{
				base.SetSystemDecimal(DietPatientMetadata.ColumnNames.Muac, value);
			}
		}
		/// <summary>
		/// Maps to DietPatient.Ulna
		/// </summary>
		virtual public System.Decimal? Ulna
		{
			get
			{
				return base.GetSystemDecimal(DietPatientMetadata.ColumnNames.Ulna);
			}

			set
			{
				base.SetSystemDecimal(DietPatientMetadata.ColumnNames.Ulna, value);
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
			public esStrings(esDietPatient entity)
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
			public System.String Height
			{
				get
				{
					System.Decimal? data = entity.Height;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Height = null;
					else entity.Height = Convert.ToDecimal(value);
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
			public System.String BodyMassIndex
			{
				get
				{
					System.Decimal? data = entity.BodyMassIndex;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BodyMassIndex = null;
					else entity.BodyMassIndex = Convert.ToDecimal(value);
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
			public System.String EffectiveStartDate
			{
				get
				{
					System.DateTime? data = entity.EffectiveStartDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EffectiveStartDate = null;
					else entity.EffectiveStartDate = Convert.ToDateTime(value);
				}
			}
			public System.String EffectiveStartTime
			{
				get
				{
					System.String data = entity.EffectiveStartTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EffectiveStartTime = null;
					else entity.EffectiveStartTime = Convert.ToString(value);
				}
			}
			public System.String EffectiveEndDate
			{
				get
				{
					System.DateTime? data = entity.EffectiveEndDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EffectiveEndDate = null;
					else entity.EffectiveEndDate = Convert.ToDateTime(value);
				}
			}
			public System.String EffectiveEndTime
			{
				get
				{
					System.String data = entity.EffectiveEndTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EffectiveEndTime = null;
					else entity.EffectiveEndTime = Convert.ToString(value);
				}
			}
			public System.String FormOfFood
			{
				get
				{
					System.String data = entity.FormOfFood;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FormOfFood = null;
					else entity.FormOfFood = Convert.ToString(value);
				}
			}
			public System.String IsSpecialCondition
			{
				get
				{
					System.Boolean? data = entity.IsSpecialCondition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSpecialCondition = null;
					else entity.IsSpecialCondition = Convert.ToBoolean(value);
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
			public System.String VoidDateTime
			{
				get
				{
					System.DateTime? data = entity.VoidDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDateTime = null;
					else entity.VoidDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VoidByUserID
			{
				get
				{
					System.String data = entity.VoidByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidByUserID = null;
					else entity.VoidByUserID = Convert.ToString(value);
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
			public System.String Muac
			{
				get
				{
					System.Decimal? data = entity.Muac;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Muac = null;
					else entity.Muac = Convert.ToDecimal(value);
				}
			}
			public System.String Ulna
			{
				get
				{
					System.Decimal? data = entity.Ulna;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Ulna = null;
					else entity.Ulna = Convert.ToDecimal(value);
				}
			}
			private esDietPatient entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDietPatientQuery query)
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
				throw new Exception("esDietPatient can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class DietPatient : esDietPatient
	{
	}

	[Serializable]
	abstract public class esDietPatientQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return DietPatientMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem Height
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.Height, esSystemType.Decimal);
			}
		}

		public esQueryItem Weight
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.Weight, esSystemType.Decimal);
			}
		}

		public esQueryItem BodyMassIndex
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.BodyMassIndex, esSystemType.Decimal);
			}
		}

		public esQueryItem Diagnose
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.Diagnose, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem EffectiveStartDate
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.EffectiveStartDate, esSystemType.DateTime);
			}
		}

		public esQueryItem EffectiveStartTime
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.EffectiveStartTime, esSystemType.String);
			}
		}

		public esQueryItem EffectiveEndDate
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.EffectiveEndDate, esSystemType.DateTime);
			}
		}

		public esQueryItem EffectiveEndTime
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.EffectiveEndTime, esSystemType.String);
			}
		}

		public esQueryItem FormOfFood
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.FormOfFood, esSystemType.String);
			}
		}

		public esQueryItem IsSpecialCondition
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.IsSpecialCondition, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem Muac
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.Muac, esSystemType.Decimal);
			}
		}

		public esQueryItem Ulna
		{
			get
			{
				return new esQueryItem(this, DietPatientMetadata.ColumnNames.Ulna, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DietPatientCollection")]
	public partial class DietPatientCollection : esDietPatientCollection, IEnumerable<DietPatient>
	{
		public DietPatientCollection()
		{

		}

		public static implicit operator List<DietPatient>(DietPatientCollection coll)
		{
			List<DietPatient> list = new List<DietPatient>();

			foreach (DietPatient emp in coll)
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
				return DietPatientMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DietPatientQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new DietPatient(row);
		}

		override protected esEntity CreateEntity()
		{
			return new DietPatient();
		}

		#endregion

		[BrowsableAttribute(false)]
		public DietPatientQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DietPatientQuery();
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
		public bool Load(DietPatientQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public DietPatient AddNew()
		{
			DietPatient entity = base.AddNewEntity() as DietPatient;

			return entity;
		}
		public DietPatient FindByPrimaryKey(String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as DietPatient;
		}

		#region IEnumerable< DietPatient> Members

		IEnumerator<DietPatient> IEnumerable<DietPatient>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as DietPatient;
			}
		}

		#endregion

		private DietPatientQuery query;
	}


	/// <summary>
	/// Encapsulates the 'DietPatient' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("DietPatient ({TransactionNo})")]
	[Serializable]
	public partial class DietPatient : esDietPatient
	{
		public DietPatient()
		{
		}

		public DietPatient(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DietPatientMetadata.Meta();
			}
		}

		override protected esDietPatientQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DietPatientQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public DietPatientQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DietPatientQuery();
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
		public bool Load(DietPatientQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private DietPatientQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class DietPatientQuery : esDietPatientQuery
	{
		public DietPatientQuery()
		{

		}

		public DietPatientQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "DietPatientQuery";
		}
	}

	[Serializable]
	public partial class DietPatientMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DietPatientMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = DietPatientMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = DietPatientMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.Height, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DietPatientMetadata.PropertyNames.Height;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.Weight, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DietPatientMetadata.PropertyNames.Weight;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.BodyMassIndex, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DietPatientMetadata.PropertyNames.BodyMassIndex;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.Diagnose, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = DietPatientMetadata.PropertyNames.Diagnose;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.Notes, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = DietPatientMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.EffectiveStartDate, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DietPatientMetadata.PropertyNames.EffectiveStartDate;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.EffectiveStartTime, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = DietPatientMetadata.PropertyNames.EffectiveStartTime;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.EffectiveEndDate, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DietPatientMetadata.PropertyNames.EffectiveEndDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.EffectiveEndTime, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = DietPatientMetadata.PropertyNames.EffectiveEndTime;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.FormOfFood, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = DietPatientMetadata.PropertyNames.FormOfFood;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.IsSpecialCondition, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DietPatientMetadata.PropertyNames.IsSpecialCondition;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.IsVoid, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DietPatientMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.VoidDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DietPatientMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.VoidByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = DietPatientMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.LastUpdateDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DietPatientMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.LastUpdateByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = DietPatientMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.Muac, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DietPatientMetadata.PropertyNames.Muac;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientMetadata.ColumnNames.Ulna, 19, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DietPatientMetadata.PropertyNames.Ulna;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public DietPatientMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string Height = "Height";
			public const string Weight = "Weight";
			public const string BodyMassIndex = "BodyMassIndex";
			public const string Diagnose = "Diagnose";
			public const string Notes = "Notes";
			public const string EffectiveStartDate = "EffectiveStartDate";
			public const string EffectiveStartTime = "EffectiveStartTime";
			public const string EffectiveEndDate = "EffectiveEndDate";
			public const string EffectiveEndTime = "EffectiveEndTime";
			public const string FormOfFood = "FormOfFood";
			public const string IsSpecialCondition = "IsSpecialCondition";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Muac = "Muac";
			public const string Ulna = "Ulna";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string Height = "Height";
			public const string Weight = "Weight";
			public const string BodyMassIndex = "BodyMassIndex";
			public const string Diagnose = "Diagnose";
			public const string Notes = "Notes";
			public const string EffectiveStartDate = "EffectiveStartDate";
			public const string EffectiveStartTime = "EffectiveStartTime";
			public const string EffectiveEndDate = "EffectiveEndDate";
			public const string EffectiveEndTime = "EffectiveEndTime";
			public const string FormOfFood = "FormOfFood";
			public const string IsSpecialCondition = "IsSpecialCondition";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Muac = "Muac";
			public const string Ulna = "Ulna";
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
			lock (typeof(DietPatientMetadata))
			{
				if (DietPatientMetadata.mapDelegates == null)
				{
					DietPatientMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (DietPatientMetadata.meta == null)
				{
					DietPatientMetadata.meta = new DietPatientMetadata();
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

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Height", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Weight", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BodyMassIndex", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Diagnose", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EffectiveStartDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EffectiveStartTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EffectiveEndDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EffectiveEndTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FormOfFood", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsSpecialCondition", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Muac", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Ulna", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "DietPatient";
				meta.Destination = "DietPatient";
				meta.spInsert = "proc_DietPatientInsert";
				meta.spUpdate = "proc_DietPatientUpdate";
				meta.spDelete = "proc_DietPatientDelete";
				meta.spLoadAll = "proc_DietPatientLoadAll";
				meta.spLoadByPrimaryKey = "proc_DietPatientLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DietPatientMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
