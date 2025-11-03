/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/12/2022 11:56:07 AM
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
	abstract public class esUddItemCollection : esEntityCollectionWAuditLog
	{
		public esUddItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "UddItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esUddItemQuery query)
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
			this.InitQuery(query as esUddItemQuery);
		}
		#endregion

		virtual public UddItem DetachEntity(UddItem entity)
		{
			return base.DetachEntity(entity) as UddItem;
		}

		virtual public UddItem AttachEntity(UddItem entity)
		{
			return base.AttachEntity(entity) as UddItem;
		}

		virtual public void Combine(UddItemCollection collection)
		{
			base.Combine(collection);
		}

		new public UddItem this[int index]
		{
			get
			{
				return base[index] as UddItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(UddItem);
		}
	}

	[Serializable]
	abstract public class esUddItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esUddItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esUddItem()
		{
		}

		public esUddItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, String locationID, String sequenceNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, locationID, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, locationID, sequenceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String locationID, String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, locationID, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, locationID, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, String locationID, String sequenceNo)
		{
			esUddItemQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.LocationID == locationID, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String locationID, String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("LocationID", locationID);
			parms.Add("SequenceNo", sequenceNo);
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
						case "LocationID": this.str.LocationID = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "ParentNo": this.str.ParentNo = (string)value; break;
						case "IsRFlag": this.str.IsRFlag = (string)value; break;
						case "IsCompound": this.str.IsCompound = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
						case "ItemQtyInString": this.str.ItemQtyInString = (string)value; break;
						case "IsUsingDosageUnit": this.str.IsUsingDosageUnit = (string)value; break;
						case "SRDosageUnit": this.str.SRDosageUnit = (string)value; break;
						case "FrequencyOfDosing": this.str.FrequencyOfDosing = (string)value; break;
						case "DosingPeriod": this.str.DosingPeriod = (string)value; break;
						case "NumberOfDosage": this.str.NumberOfDosage = (string)value; break;
						case "DurationOfDosing": this.str.DurationOfDosing = (string)value; break;
						case "AcPcDc": this.str.AcPcDc = (string)value; break;
						case "SRMedicationRoute": this.str.SRMedicationRoute = (string)value; break;
						case "ConsumeMethod": this.str.ConsumeMethod = (string)value; break;
						case "PrescriptionQty": this.str.PrescriptionQty = (string)value; break;
						case "EmbalaceID": this.str.EmbalaceID = (string)value; break;
						case "IsUseSweetener": this.str.IsUseSweetener = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "IsStop": this.str.IsStop = (string)value; break;
						case "SRConsumeMethod": this.str.SRConsumeMethod = (string)value; break;
						case "DosageQty": this.str.DosageQty = (string)value; break;
						case "EmbalaceQty": this.str.EmbalaceQty = (string)value; break;
						case "ConsumeQty": this.str.ConsumeQty = (string)value; break;
						case "SRConsumeUnit": this.str.SRConsumeUnit = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "StartDateTime": this.str.StartDateTime = (string)value; break;
						case "StopDateTime": this.str.StopDateTime = (string)value; break;
						case "RasproSeqNo": this.str.RasproSeqNo = (string)value; break;
						case "RasprajaSeqNo": this.str.RasprajaSeqNo = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IsRFlag":

							if (value == null || value is System.Boolean)
								this.IsRFlag = (System.Boolean?)value;
							break;
						case "IsCompound":

							if (value == null || value is System.Boolean)
								this.IsCompound = (System.Boolean?)value;
							break;
						case "IsUsingDosageUnit":

							if (value == null || value is System.Boolean)
								this.IsUsingDosageUnit = (System.Boolean?)value;
							break;
						case "FrequencyOfDosing":

							if (value == null || value is System.Byte)
								this.FrequencyOfDosing = (System.Byte?)value;
							break;
						case "NumberOfDosage":

							if (value == null || value is System.Decimal)
								this.NumberOfDosage = (System.Decimal?)value;
							break;
						case "DurationOfDosing":

							if (value == null || value is System.Byte)
								this.DurationOfDosing = (System.Byte?)value;
							break;
						case "PrescriptionQty":

							if (value == null || value is System.Decimal)
								this.PrescriptionQty = (System.Decimal?)value;
							break;
						case "IsUseSweetener":

							if (value == null || value is System.Boolean)
								this.IsUseSweetener = (System.Boolean?)value;
							break;
						case "IsStop":

							if (value == null || value is System.Boolean)
								this.IsStop = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "StartDateTime":

							if (value == null || value is System.DateTime)
								this.StartDateTime = (System.DateTime?)value;
							break;
						case "StopDateTime":

							if (value == null || value is System.DateTime)
								this.StopDateTime = (System.DateTime?)value;
							break;
						case "RasproSeqNo":

							if (value == null || value is System.Int32)
								this.RasproSeqNo = (System.Int32?)value;
							break;
						case "RasprajaSeqNo":

							if (value == null || value is System.Int32)
								this.RasprajaSeqNo = (System.Int32?)value;
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
		/// Maps to UddItem.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.LocationID
		/// </summary>
		virtual public System.String LocationID
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.LocationID);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.LocationID, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.SequenceNo);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.ParentNo
		/// </summary>
		virtual public System.String ParentNo
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.ParentNo);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.ParentNo, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.IsRFlag
		/// </summary>
		virtual public System.Boolean? IsRFlag
		{
			get
			{
				return base.GetSystemBoolean(UddItemMetadata.ColumnNames.IsRFlag);
			}

			set
			{
				base.SetSystemBoolean(UddItemMetadata.ColumnNames.IsRFlag, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.IsCompound
		/// </summary>
		virtual public System.Boolean? IsCompound
		{
			get
			{
				return base.GetSystemBoolean(UddItemMetadata.ColumnNames.IsCompound);
			}

			set
			{
				base.SetSystemBoolean(UddItemMetadata.ColumnNames.IsCompound, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.SRItemUnit);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.ItemQtyInString
		/// </summary>
		virtual public System.String ItemQtyInString
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.ItemQtyInString);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.ItemQtyInString, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.IsUsingDosageUnit
		/// </summary>
		virtual public System.Boolean? IsUsingDosageUnit
		{
			get
			{
				return base.GetSystemBoolean(UddItemMetadata.ColumnNames.IsUsingDosageUnit);
			}

			set
			{
				base.SetSystemBoolean(UddItemMetadata.ColumnNames.IsUsingDosageUnit, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.SRDosageUnit
		/// </summary>
		virtual public System.String SRDosageUnit
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.SRDosageUnit);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.SRDosageUnit, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.FrequencyOfDosing
		/// </summary>
		virtual public System.Byte? FrequencyOfDosing
		{
			get
			{
				return base.GetSystemByte(UddItemMetadata.ColumnNames.FrequencyOfDosing);
			}

			set
			{
				base.SetSystemByte(UddItemMetadata.ColumnNames.FrequencyOfDosing, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.DosingPeriod
		/// </summary>
		virtual public System.String DosingPeriod
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.DosingPeriod);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.DosingPeriod, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.NumberOfDosage
		/// </summary>
		virtual public System.Decimal? NumberOfDosage
		{
			get
			{
				return base.GetSystemDecimal(UddItemMetadata.ColumnNames.NumberOfDosage);
			}

			set
			{
				base.SetSystemDecimal(UddItemMetadata.ColumnNames.NumberOfDosage, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.DurationOfDosing
		/// </summary>
		virtual public System.Byte? DurationOfDosing
		{
			get
			{
				return base.GetSystemByte(UddItemMetadata.ColumnNames.DurationOfDosing);
			}

			set
			{
				base.SetSystemByte(UddItemMetadata.ColumnNames.DurationOfDosing, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.AcPcDc
		/// </summary>
		virtual public System.String AcPcDc
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.AcPcDc);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.AcPcDc, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.SRMedicationRoute
		/// </summary>
		virtual public System.String SRMedicationRoute
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.SRMedicationRoute);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.SRMedicationRoute, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.ConsumeMethod
		/// </summary>
		virtual public System.String ConsumeMethod
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.ConsumeMethod);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.ConsumeMethod, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.PrescriptionQty
		/// </summary>
		virtual public System.Decimal? PrescriptionQty
		{
			get
			{
				return base.GetSystemDecimal(UddItemMetadata.ColumnNames.PrescriptionQty);
			}

			set
			{
				base.SetSystemDecimal(UddItemMetadata.ColumnNames.PrescriptionQty, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.EmbalaceID
		/// </summary>
		virtual public System.String EmbalaceID
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.EmbalaceID);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.EmbalaceID, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.IsUseSweetener
		/// </summary>
		virtual public System.Boolean? IsUseSweetener
		{
			get
			{
				return base.GetSystemBoolean(UddItemMetadata.ColumnNames.IsUseSweetener);
			}

			set
			{
				base.SetSystemBoolean(UddItemMetadata.ColumnNames.IsUseSweetener, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.IsStop
		/// </summary>
		virtual public System.Boolean? IsStop
		{
			get
			{
				return base.GetSystemBoolean(UddItemMetadata.ColumnNames.IsStop);
			}

			set
			{
				base.SetSystemBoolean(UddItemMetadata.ColumnNames.IsStop, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.SRConsumeMethod
		/// </summary>
		virtual public System.String SRConsumeMethod
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.SRConsumeMethod);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.SRConsumeMethod, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.DosageQty
		/// </summary>
		virtual public System.String DosageQty
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.DosageQty);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.DosageQty, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.EmbalaceQty
		/// </summary>
		virtual public System.String EmbalaceQty
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.EmbalaceQty);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.EmbalaceQty, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.ConsumeQty
		/// </summary>
		virtual public System.String ConsumeQty
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.ConsumeQty);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.ConsumeQty, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.SRConsumeUnit
		/// </summary>
		virtual public System.String SRConsumeUnit
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.SRConsumeUnit);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.SRConsumeUnit, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(UddItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(UddItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.StartDateTime
		/// </summary>
		virtual public System.DateTime? StartDateTime
		{
			get
			{
				return base.GetSystemDateTime(UddItemMetadata.ColumnNames.StartDateTime);
			}

			set
			{
				base.SetSystemDateTime(UddItemMetadata.ColumnNames.StartDateTime, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.StopDateTime
		/// </summary>
		virtual public System.DateTime? StopDateTime
		{
			get
			{
				return base.GetSystemDateTime(UddItemMetadata.ColumnNames.StopDateTime);
			}

			set
			{
				base.SetSystemDateTime(UddItemMetadata.ColumnNames.StopDateTime, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.RasproSeqNo
		/// </summary>
		virtual public System.Int32? RasproSeqNo
		{
			get
			{
				return base.GetSystemInt32(UddItemMetadata.ColumnNames.RasproSeqNo);
			}

			set
			{
				base.SetSystemInt32(UddItemMetadata.ColumnNames.RasproSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.RasprajaSeqNo
		/// </summary>
		virtual public System.Int32? RasprajaSeqNo
		{
			get
			{
				return base.GetSystemInt32(UddItemMetadata.ColumnNames.RasprajaSeqNo);
			}

			set
			{
				base.SetSystemInt32(UddItemMetadata.ColumnNames.RasprajaSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to UddItem.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(UddItemMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(UddItemMetadata.ColumnNames.ParamedicID, value);
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
			public esStrings(esUddItem entity)
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
			public System.String LocationID
			{
				get
				{
					System.String data = entity.LocationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationID = null;
					else entity.LocationID = Convert.ToString(value);
				}
			}
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
				}
			}
			public System.String ParentNo
			{
				get
				{
					System.String data = entity.ParentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParentNo = null;
					else entity.ParentNo = Convert.ToString(value);
				}
			}
			public System.String IsRFlag
			{
				get
				{
					System.Boolean? data = entity.IsRFlag;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRFlag = null;
					else entity.IsRFlag = Convert.ToBoolean(value);
				}
			}
			public System.String IsCompound
			{
				get
				{
					System.Boolean? data = entity.IsCompound;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCompound = null;
					else entity.IsCompound = Convert.ToBoolean(value);
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
			public System.String SRItemUnit
			{
				get
				{
					System.String data = entity.SRItemUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemUnit = null;
					else entity.SRItemUnit = Convert.ToString(value);
				}
			}
			public System.String ItemQtyInString
			{
				get
				{
					System.String data = entity.ItemQtyInString;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemQtyInString = null;
					else entity.ItemQtyInString = Convert.ToString(value);
				}
			}
			public System.String IsUsingDosageUnit
			{
				get
				{
					System.Boolean? data = entity.IsUsingDosageUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUsingDosageUnit = null;
					else entity.IsUsingDosageUnit = Convert.ToBoolean(value);
				}
			}
			public System.String SRDosageUnit
			{
				get
				{
					System.String data = entity.SRDosageUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDosageUnit = null;
					else entity.SRDosageUnit = Convert.ToString(value);
				}
			}
			public System.String FrequencyOfDosing
			{
				get
				{
					System.Byte? data = entity.FrequencyOfDosing;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FrequencyOfDosing = null;
					else entity.FrequencyOfDosing = Convert.ToByte(value);
				}
			}
			public System.String DosingPeriod
			{
				get
				{
					System.String data = entity.DosingPeriod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DosingPeriod = null;
					else entity.DosingPeriod = Convert.ToString(value);
				}
			}
			public System.String NumberOfDosage
			{
				get
				{
					System.Decimal? data = entity.NumberOfDosage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NumberOfDosage = null;
					else entity.NumberOfDosage = Convert.ToDecimal(value);
				}
			}
			public System.String DurationOfDosing
			{
				get
				{
					System.Byte? data = entity.DurationOfDosing;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DurationOfDosing = null;
					else entity.DurationOfDosing = Convert.ToByte(value);
				}
			}
			public System.String AcPcDc
			{
				get
				{
					System.String data = entity.AcPcDc;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AcPcDc = null;
					else entity.AcPcDc = Convert.ToString(value);
				}
			}
			public System.String SRMedicationRoute
			{
				get
				{
					System.String data = entity.SRMedicationRoute;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMedicationRoute = null;
					else entity.SRMedicationRoute = Convert.ToString(value);
				}
			}
			public System.String ConsumeMethod
			{
				get
				{
					System.String data = entity.ConsumeMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConsumeMethod = null;
					else entity.ConsumeMethod = Convert.ToString(value);
				}
			}
			public System.String PrescriptionQty
			{
				get
				{
					System.Decimal? data = entity.PrescriptionQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrescriptionQty = null;
					else entity.PrescriptionQty = Convert.ToDecimal(value);
				}
			}
			public System.String EmbalaceID
			{
				get
				{
					System.String data = entity.EmbalaceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmbalaceID = null;
					else entity.EmbalaceID = Convert.ToString(value);
				}
			}
			public System.String IsUseSweetener
			{
				get
				{
					System.Boolean? data = entity.IsUseSweetener;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUseSweetener = null;
					else entity.IsUseSweetener = Convert.ToBoolean(value);
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
			public System.String IsStop
			{
				get
				{
					System.Boolean? data = entity.IsStop;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsStop = null;
					else entity.IsStop = Convert.ToBoolean(value);
				}
			}
			public System.String SRConsumeMethod
			{
				get
				{
					System.String data = entity.SRConsumeMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRConsumeMethod = null;
					else entity.SRConsumeMethod = Convert.ToString(value);
				}
			}
			public System.String DosageQty
			{
				get
				{
					System.String data = entity.DosageQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DosageQty = null;
					else entity.DosageQty = Convert.ToString(value);
				}
			}
			public System.String EmbalaceQty
			{
				get
				{
					System.String data = entity.EmbalaceQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmbalaceQty = null;
					else entity.EmbalaceQty = Convert.ToString(value);
				}
			}
			public System.String ConsumeQty
			{
				get
				{
					System.String data = entity.ConsumeQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConsumeQty = null;
					else entity.ConsumeQty = Convert.ToString(value);
				}
			}
			public System.String SRConsumeUnit
			{
				get
				{
					System.String data = entity.SRConsumeUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRConsumeUnit = null;
					else entity.SRConsumeUnit = Convert.ToString(value);
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
			public System.String StopDateTime
			{
				get
				{
					System.DateTime? data = entity.StopDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StopDateTime = null;
					else entity.StopDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String RasproSeqNo
			{
				get
				{
					System.Int32? data = entity.RasproSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RasproSeqNo = null;
					else entity.RasproSeqNo = Convert.ToInt32(value);
				}
			}
			public System.String RasprajaSeqNo
			{
				get
				{
					System.Int32? data = entity.RasprajaSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RasprajaSeqNo = null;
					else entity.RasprajaSeqNo = Convert.ToInt32(value);
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
			private esUddItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esUddItemQuery query)
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
				throw new Exception("esUddItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class UddItem : esUddItem
	{
	}

	[Serializable]
	abstract public class esUddItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return UddItemMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem LocationID
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.LocationID, esSystemType.String);
			}
		}

		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		}

		public esQueryItem ParentNo
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.ParentNo, esSystemType.String);
			}
		}

		public esQueryItem IsRFlag
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.IsRFlag, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCompound
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.IsCompound, esSystemType.Boolean);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem ItemQtyInString
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.ItemQtyInString, esSystemType.String);
			}
		}

		public esQueryItem IsUsingDosageUnit
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.IsUsingDosageUnit, esSystemType.Boolean);
			}
		}

		public esQueryItem SRDosageUnit
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.SRDosageUnit, esSystemType.String);
			}
		}

		public esQueryItem FrequencyOfDosing
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.FrequencyOfDosing, esSystemType.Byte);
			}
		}

		public esQueryItem DosingPeriod
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.DosingPeriod, esSystemType.String);
			}
		}

		public esQueryItem NumberOfDosage
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.NumberOfDosage, esSystemType.Decimal);
			}
		}

		public esQueryItem DurationOfDosing
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.DurationOfDosing, esSystemType.Byte);
			}
		}

		public esQueryItem AcPcDc
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.AcPcDc, esSystemType.String);
			}
		}

		public esQueryItem SRMedicationRoute
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.SRMedicationRoute, esSystemType.String);
			}
		}

		public esQueryItem ConsumeMethod
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.ConsumeMethod, esSystemType.String);
			}
		}

		public esQueryItem PrescriptionQty
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.PrescriptionQty, esSystemType.Decimal);
			}
		}

		public esQueryItem EmbalaceID
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.EmbalaceID, esSystemType.String);
			}
		}

		public esQueryItem IsUseSweetener
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.IsUseSweetener, esSystemType.Boolean);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem IsStop
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.IsStop, esSystemType.Boolean);
			}
		}

		public esQueryItem SRConsumeMethod
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.SRConsumeMethod, esSystemType.String);
			}
		}

		public esQueryItem DosageQty
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.DosageQty, esSystemType.String);
			}
		}

		public esQueryItem EmbalaceQty
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.EmbalaceQty, esSystemType.String);
			}
		}

		public esQueryItem ConsumeQty
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.ConsumeQty, esSystemType.String);
			}
		}

		public esQueryItem SRConsumeUnit
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.SRConsumeUnit, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem StartDateTime
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.StartDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem StopDateTime
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.StopDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem RasproSeqNo
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.RasproSeqNo, esSystemType.Int32);
			}
		}

		public esQueryItem RasprajaSeqNo
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.RasprajaSeqNo, esSystemType.Int32);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, UddItemMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("UddItemCollection")]
	public partial class UddItemCollection : esUddItemCollection, IEnumerable<UddItem>
	{
		public UddItemCollection()
		{

		}

		public static implicit operator List<UddItem>(UddItemCollection coll)
		{
			List<UddItem> list = new List<UddItem>();

			foreach (UddItem emp in coll)
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
				return UddItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new UddItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new UddItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new UddItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public UddItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new UddItemQuery();
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
		public bool Load(UddItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public UddItem AddNew()
		{
			UddItem entity = base.AddNewEntity() as UddItem;

			return entity;
		}
		public UddItem FindByPrimaryKey(String registrationNo, String locationID, String sequenceNo)
		{
			return base.FindByPrimaryKey(registrationNo, locationID, sequenceNo) as UddItem;
		}

		#region IEnumerable< UddItem> Members

		IEnumerator<UddItem> IEnumerable<UddItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as UddItem;
			}
		}

		#endregion

		private UddItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'UddItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("UddItem ({RegistrationNo, LocationID, SequenceNo})")]
	[Serializable]
	public partial class UddItem : esUddItem
	{
		public UddItem()
		{
		}

		public UddItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return UddItemMetadata.Meta();
			}
		}

		override protected esUddItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new UddItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public UddItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new UddItemQuery();
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
		public bool Load(UddItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private UddItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class UddItemQuery : esUddItemQuery
	{
		public UddItemQuery()
		{

		}

		public UddItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "UddItemQuery";
		}
	}

	[Serializable]
	public partial class UddItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected UddItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.LocationID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.LocationID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.ParentNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.ParentNo;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.IsRFlag, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = UddItemMetadata.PropertyNames.IsRFlag;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.IsCompound, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = UddItemMetadata.PropertyNames.IsCompound;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.ItemID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.SRItemUnit, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.ItemQtyInString, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.ItemQtyInString;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.IsUsingDosageUnit, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = UddItemMetadata.PropertyNames.IsUsingDosageUnit;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.SRDosageUnit, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.SRDosageUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.FrequencyOfDosing, 11, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = UddItemMetadata.PropertyNames.FrequencyOfDosing;
			c.NumericPrecision = 3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.DosingPeriod, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.DosingPeriod;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.NumberOfDosage, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = UddItemMetadata.PropertyNames.NumberOfDosage;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.DurationOfDosing, 14, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = UddItemMetadata.PropertyNames.DurationOfDosing;
			c.NumericPrecision = 3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.AcPcDc, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.AcPcDc;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.SRMedicationRoute, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.SRMedicationRoute;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.ConsumeMethod, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.ConsumeMethod;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.PrescriptionQty, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = UddItemMetadata.PropertyNames.PrescriptionQty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.EmbalaceID, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.EmbalaceID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.IsUseSweetener, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = UddItemMetadata.PropertyNames.IsUseSweetener;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.Notes, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.IsStop, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = UddItemMetadata.PropertyNames.IsStop;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.SRConsumeMethod, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.SRConsumeMethod;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.DosageQty, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.DosageQty;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.EmbalaceQty, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.EmbalaceQty;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.ConsumeQty, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.ConsumeQty;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.SRConsumeUnit, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.SRConsumeUnit;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.LastUpdateDateTime, 28, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = UddItemMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.LastUpdateByUserID, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.StartDateTime, 30, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = UddItemMetadata.PropertyNames.StartDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.StopDateTime, 31, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = UddItemMetadata.PropertyNames.StopDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.RasproSeqNo, 32, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = UddItemMetadata.PropertyNames.RasproSeqNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.RasprajaSeqNo, 33, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = UddItemMetadata.PropertyNames.RasprajaSeqNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(UddItemMetadata.ColumnNames.ParamedicID, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = UddItemMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public UddItemMetadata Meta()
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
			public const string LocationID = "LocationID";
			public const string SequenceNo = "SequenceNo";
			public const string ParentNo = "ParentNo";
			public const string IsRFlag = "IsRFlag";
			public const string IsCompound = "IsCompound";
			public const string ItemID = "ItemID";
			public const string SRItemUnit = "SRItemUnit";
			public const string ItemQtyInString = "ItemQtyInString";
			public const string IsUsingDosageUnit = "IsUsingDosageUnit";
			public const string SRDosageUnit = "SRDosageUnit";
			public const string FrequencyOfDosing = "FrequencyOfDosing";
			public const string DosingPeriod = "DosingPeriod";
			public const string NumberOfDosage = "NumberOfDosage";
			public const string DurationOfDosing = "DurationOfDosing";
			public const string AcPcDc = "AcPcDc";
			public const string SRMedicationRoute = "SRMedicationRoute";
			public const string ConsumeMethod = "ConsumeMethod";
			public const string PrescriptionQty = "PrescriptionQty";
			public const string EmbalaceID = "EmbalaceID";
			public const string IsUseSweetener = "IsUseSweetener";
			public const string Notes = "Notes";
			public const string IsStop = "IsStop";
			public const string SRConsumeMethod = "SRConsumeMethod";
			public const string DosageQty = "DosageQty";
			public const string EmbalaceQty = "EmbalaceQty";
			public const string ConsumeQty = "ConsumeQty";
			public const string SRConsumeUnit = "SRConsumeUnit";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string StartDateTime = "StartDateTime";
			public const string StopDateTime = "StopDateTime";
			public const string RasproSeqNo = "RasproSeqNo";
			public const string RasprajaSeqNo = "RasprajaSeqNo";
			public const string ParamedicID = "ParamedicID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string LocationID = "LocationID";
			public const string SequenceNo = "SequenceNo";
			public const string ParentNo = "ParentNo";
			public const string IsRFlag = "IsRFlag";
			public const string IsCompound = "IsCompound";
			public const string ItemID = "ItemID";
			public const string SRItemUnit = "SRItemUnit";
			public const string ItemQtyInString = "ItemQtyInString";
			public const string IsUsingDosageUnit = "IsUsingDosageUnit";
			public const string SRDosageUnit = "SRDosageUnit";
			public const string FrequencyOfDosing = "FrequencyOfDosing";
			public const string DosingPeriod = "DosingPeriod";
			public const string NumberOfDosage = "NumberOfDosage";
			public const string DurationOfDosing = "DurationOfDosing";
			public const string AcPcDc = "AcPcDc";
			public const string SRMedicationRoute = "SRMedicationRoute";
			public const string ConsumeMethod = "ConsumeMethod";
			public const string PrescriptionQty = "PrescriptionQty";
			public const string EmbalaceID = "EmbalaceID";
			public const string IsUseSweetener = "IsUseSweetener";
			public const string Notes = "Notes";
			public const string IsStop = "IsStop";
			public const string SRConsumeMethod = "SRConsumeMethod";
			public const string DosageQty = "DosageQty";
			public const string EmbalaceQty = "EmbalaceQty";
			public const string ConsumeQty = "ConsumeQty";
			public const string SRConsumeUnit = "SRConsumeUnit";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string StartDateTime = "StartDateTime";
			public const string StopDateTime = "StopDateTime";
			public const string RasproSeqNo = "RasproSeqNo";
			public const string RasprajaSeqNo = "RasprajaSeqNo";
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
			lock (typeof(UddItemMetadata))
			{
				if (UddItemMetadata.mapDelegates == null)
				{
					UddItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (UddItemMetadata.meta == null)
				{
					UddItemMetadata.meta = new UddItemMetadata();
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
				meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRFlag", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCompound", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemQtyInString", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsUsingDosageUnit", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRDosageUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FrequencyOfDosing", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("DosingPeriod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NumberOfDosage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DurationOfDosing", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("AcPcDc", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMedicationRoute", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConsumeMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrescriptionQty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("EmbalaceID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsUseSweetener", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsStop", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRConsumeMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DosageQty", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmbalaceQty", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConsumeQty", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRConsumeUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("StopDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RasproSeqNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RasprajaSeqNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));


				meta.Source = "UddItem";
				meta.Destination = "UddItem";
				meta.spInsert = "proc_UddItemInsert";
				meta.spUpdate = "proc_UddItemUpdate";
				meta.spDelete = "proc_UddItemDelete";
				meta.spLoadAll = "proc_UddItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_UddItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private UddItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
