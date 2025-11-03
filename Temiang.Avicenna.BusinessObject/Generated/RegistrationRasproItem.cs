/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/7/2022 6:44:01 PM
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
	abstract public class esRegistrationRasproItemCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationRasproItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "RegistrationRasproItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationRasproItemQuery query)
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
			this.InitQuery(query as esRegistrationRasproItemQuery);
		}
		#endregion

		virtual public RegistrationRasproItem DetachEntity(RegistrationRasproItem entity)
		{
			return base.DetachEntity(entity) as RegistrationRasproItem;
		}

		virtual public RegistrationRasproItem AttachEntity(RegistrationRasproItem entity)
		{
			return base.AttachEntity(entity) as RegistrationRasproItem;
		}

		virtual public void Combine(RegistrationRasproItemCollection collection)
		{
			base.Combine(collection);
		}

		new public RegistrationRasproItem this[int index]
		{
			get
			{
				return base[index] as RegistrationRasproItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationRasproItem);
		}
	}

	[Serializable]
	abstract public class esRegistrationRasproItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationRasproItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationRasproItem()
		{
		}

		public esRegistrationRasproItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 rasproSeqNo, String itemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, rasproSeqNo, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, rasproSeqNo, itemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 rasproSeqNo, String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, rasproSeqNo, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, rasproSeqNo, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 rasproSeqNo, String itemID)
		{
			esRegistrationRasproItemQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.RasproSeqNo == rasproSeqNo, query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 rasproSeqNo, String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("RasproSeqNo", rasproSeqNo);
			parms.Add("ItemID", itemID);
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
						case "RasproSeqNo": this.str.RasproSeqNo = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ZatActiveID": this.str.ZatActiveID = (string)value; break;
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
						case "SRDosageUnit": this.str.SRDosageUnit = (string)value; break;
						case "AcPcDc": this.str.AcPcDc = (string)value; break;
						case "SRMedicationRoute": this.str.SRMedicationRoute = (string)value; break;
						case "SRConsumeMethod": this.str.SRConsumeMethod = (string)value; break;
						case "DosageQty": this.str.DosageQty = (string)value; break;
						case "EmbalaceQty": this.str.EmbalaceQty = (string)value; break;
						case "ConsumeQty": this.str.ConsumeQty = (string)value; break;
						case "SRConsumeUnit": this.str.SRConsumeUnit = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "StartDateTime": this.str.StartDateTime = (string)value; break;
						case "StopDateTime": this.str.StopDateTime = (string)value; break;
						case "RasprajaSeqNo": this.str.RasprajaSeqNo = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "RasproSeqNo":

							if (value == null || value is System.Int32)
								this.RasproSeqNo = (System.Int32?)value;
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
		/// Maps to RegistrationRasproItem.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationRasproItemMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(RegistrationRasproItemMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproItem.RasproSeqNo
		/// </summary>
		virtual public System.Int32? RasproSeqNo
		{
			get
			{
				return base.GetSystemInt32(RegistrationRasproItemMetadata.ColumnNames.RasproSeqNo);
			}

			set
			{
				base.SetSystemInt32(RegistrationRasproItemMetadata.ColumnNames.RasproSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(RegistrationRasproItemMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(RegistrationRasproItemMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproItem.ZatActiveID
		/// </summary>
		virtual public System.String ZatActiveID
		{
			get
			{
				return base.GetSystemString(RegistrationRasproItemMetadata.ColumnNames.ZatActiveID);
			}

			set
			{
				base.SetSystemString(RegistrationRasproItemMetadata.ColumnNames.ZatActiveID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproItem.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(RegistrationRasproItemMetadata.ColumnNames.SRItemUnit);
			}

			set
			{
				base.SetSystemString(RegistrationRasproItemMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproItem.SRDosageUnit
		/// </summary>
		virtual public System.String SRDosageUnit
		{
			get
			{
				return base.GetSystemString(RegistrationRasproItemMetadata.ColumnNames.SRDosageUnit);
			}

			set
			{
				base.SetSystemString(RegistrationRasproItemMetadata.ColumnNames.SRDosageUnit, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproItem.AcPcDc
		/// </summary>
		virtual public System.String AcPcDc
		{
			get
			{
				return base.GetSystemString(RegistrationRasproItemMetadata.ColumnNames.AcPcDc);
			}

			set
			{
				base.SetSystemString(RegistrationRasproItemMetadata.ColumnNames.AcPcDc, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproItem.SRMedicationRoute
		/// </summary>
		virtual public System.String SRMedicationRoute
		{
			get
			{
				return base.GetSystemString(RegistrationRasproItemMetadata.ColumnNames.SRMedicationRoute);
			}

			set
			{
				base.SetSystemString(RegistrationRasproItemMetadata.ColumnNames.SRMedicationRoute, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproItem.SRConsumeMethod
		/// </summary>
		virtual public System.String SRConsumeMethod
		{
			get
			{
				return base.GetSystemString(RegistrationRasproItemMetadata.ColumnNames.SRConsumeMethod);
			}

			set
			{
				base.SetSystemString(RegistrationRasproItemMetadata.ColumnNames.SRConsumeMethod, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproItem.DosageQty
		/// </summary>
		virtual public System.String DosageQty
		{
			get
			{
				return base.GetSystemString(RegistrationRasproItemMetadata.ColumnNames.DosageQty);
			}

			set
			{
				base.SetSystemString(RegistrationRasproItemMetadata.ColumnNames.DosageQty, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproItem.EmbalaceQty
		/// </summary>
		virtual public System.String EmbalaceQty
		{
			get
			{
				return base.GetSystemString(RegistrationRasproItemMetadata.ColumnNames.EmbalaceQty);
			}

			set
			{
				base.SetSystemString(RegistrationRasproItemMetadata.ColumnNames.EmbalaceQty, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproItem.ConsumeQty
		/// </summary>
		virtual public System.String ConsumeQty
		{
			get
			{
				return base.GetSystemString(RegistrationRasproItemMetadata.ColumnNames.ConsumeQty);
			}

			set
			{
				base.SetSystemString(RegistrationRasproItemMetadata.ColumnNames.ConsumeQty, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproItem.SRConsumeUnit
		/// </summary>
		virtual public System.String SRConsumeUnit
		{
			get
			{
				return base.GetSystemString(RegistrationRasproItemMetadata.ColumnNames.SRConsumeUnit);
			}

			set
			{
				base.SetSystemString(RegistrationRasproItemMetadata.ColumnNames.SRConsumeUnit, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationRasproItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationRasproItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationRasproItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationRasproItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproItem.StartDateTime
		/// </summary>
		virtual public System.DateTime? StartDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationRasproItemMetadata.ColumnNames.StartDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationRasproItemMetadata.ColumnNames.StartDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproItem.StopDateTime
		/// </summary>
		virtual public System.DateTime? StopDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationRasproItemMetadata.ColumnNames.StopDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationRasproItemMetadata.ColumnNames.StopDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationRasproItem.RasprajaSeqNo
		/// </summary>
		virtual public System.Int32? RasprajaSeqNo
		{
			get
			{
				return base.GetSystemInt32(RegistrationRasproItemMetadata.ColumnNames.RasprajaSeqNo);
			}

			set
			{
				base.SetSystemInt32(RegistrationRasproItemMetadata.ColumnNames.RasprajaSeqNo, value);
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
			public esStrings(esRegistrationRasproItem entity)
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
			public System.String ZatActiveID
			{
				get
				{
					System.String data = entity.ZatActiveID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ZatActiveID = null;
					else entity.ZatActiveID = Convert.ToString(value);
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
			private esRegistrationRasproItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationRasproItemQuery query)
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
				throw new Exception("esRegistrationRasproItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationRasproItem : esRegistrationRasproItem
	{
	}

	[Serializable]
	abstract public class esRegistrationRasproItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return RegistrationRasproItemMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproItemMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem RasproSeqNo
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproItemMetadata.ColumnNames.RasproSeqNo, esSystemType.Int32);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ZatActiveID
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproItemMetadata.ColumnNames.ZatActiveID, esSystemType.String);
			}
		}

		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem SRDosageUnit
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproItemMetadata.ColumnNames.SRDosageUnit, esSystemType.String);
			}
		}

		public esQueryItem AcPcDc
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproItemMetadata.ColumnNames.AcPcDc, esSystemType.String);
			}
		}

		public esQueryItem SRMedicationRoute
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproItemMetadata.ColumnNames.SRMedicationRoute, esSystemType.String);
			}
		}

		public esQueryItem SRConsumeMethod
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproItemMetadata.ColumnNames.SRConsumeMethod, esSystemType.String);
			}
		}

		public esQueryItem DosageQty
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproItemMetadata.ColumnNames.DosageQty, esSystemType.String);
			}
		}

		public esQueryItem EmbalaceQty
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproItemMetadata.ColumnNames.EmbalaceQty, esSystemType.String);
			}
		}

		public esQueryItem ConsumeQty
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproItemMetadata.ColumnNames.ConsumeQty, esSystemType.String);
			}
		}

		public esQueryItem SRConsumeUnit
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproItemMetadata.ColumnNames.SRConsumeUnit, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem StartDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproItemMetadata.ColumnNames.StartDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem StopDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproItemMetadata.ColumnNames.StopDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem RasprajaSeqNo
		{
			get
			{
				return new esQueryItem(this, RegistrationRasproItemMetadata.ColumnNames.RasprajaSeqNo, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationRasproItemCollection")]
	public partial class RegistrationRasproItemCollection : esRegistrationRasproItemCollection, IEnumerable<RegistrationRasproItem>
	{
		public RegistrationRasproItemCollection()
		{

		}

		public static implicit operator List<RegistrationRasproItem>(RegistrationRasproItemCollection coll)
		{
			List<RegistrationRasproItem> list = new List<RegistrationRasproItem>();

			foreach (RegistrationRasproItem emp in coll)
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
				return RegistrationRasproItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationRasproItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationRasproItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationRasproItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public RegistrationRasproItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationRasproItemQuery();
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
		public bool Load(RegistrationRasproItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationRasproItem AddNew()
		{
			RegistrationRasproItem entity = base.AddNewEntity() as RegistrationRasproItem;

			return entity;
		}
		public RegistrationRasproItem FindByPrimaryKey(String registrationNo, Int32 rasproSeqNo, String itemID)
		{
			return base.FindByPrimaryKey(registrationNo, rasproSeqNo, itemID) as RegistrationRasproItem;
		}

		#region IEnumerable< RegistrationRasproItem> Members

		IEnumerator<RegistrationRasproItem> IEnumerable<RegistrationRasproItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationRasproItem;
			}
		}

		#endregion

		private RegistrationRasproItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationRasproItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationRasproItem ({RegistrationNo, RasproSeqNo, ItemID})")]
	[Serializable]
	public partial class RegistrationRasproItem : esRegistrationRasproItem
	{
		public RegistrationRasproItem()
		{
		}

		public RegistrationRasproItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationRasproItemMetadata.Meta();
			}
		}

		override protected esRegistrationRasproItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationRasproItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public RegistrationRasproItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationRasproItemQuery();
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
		public bool Load(RegistrationRasproItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private RegistrationRasproItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationRasproItemQuery : esRegistrationRasproItemQuery
	{
		public RegistrationRasproItemQuery()
		{

		}

		public RegistrationRasproItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "RegistrationRasproItemQuery";
		}
	}

	[Serializable]
	public partial class RegistrationRasproItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationRasproItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationRasproItemMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproItemMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproItemMetadata.ColumnNames.RasproSeqNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationRasproItemMetadata.PropertyNames.RasproSeqNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproItemMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproItemMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproItemMetadata.ColumnNames.ZatActiveID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproItemMetadata.PropertyNames.ZatActiveID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproItemMetadata.ColumnNames.SRItemUnit, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproItemMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproItemMetadata.ColumnNames.SRDosageUnit, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproItemMetadata.PropertyNames.SRDosageUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproItemMetadata.ColumnNames.AcPcDc, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproItemMetadata.PropertyNames.AcPcDc;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproItemMetadata.ColumnNames.SRMedicationRoute, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproItemMetadata.PropertyNames.SRMedicationRoute;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproItemMetadata.ColumnNames.SRConsumeMethod, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproItemMetadata.PropertyNames.SRConsumeMethod;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproItemMetadata.ColumnNames.DosageQty, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproItemMetadata.PropertyNames.DosageQty;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproItemMetadata.ColumnNames.EmbalaceQty, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproItemMetadata.PropertyNames.EmbalaceQty;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproItemMetadata.ColumnNames.ConsumeQty, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproItemMetadata.PropertyNames.ConsumeQty;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproItemMetadata.ColumnNames.SRConsumeUnit, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproItemMetadata.PropertyNames.SRConsumeUnit;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproItemMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationRasproItemMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproItemMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationRasproItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproItemMetadata.ColumnNames.StartDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationRasproItemMetadata.PropertyNames.StartDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproItemMetadata.ColumnNames.StopDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationRasproItemMetadata.PropertyNames.StopDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationRasproItemMetadata.ColumnNames.RasprajaSeqNo, 17, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationRasproItemMetadata.PropertyNames.RasprajaSeqNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public RegistrationRasproItemMetadata Meta()
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
			public const string RasproSeqNo = "RasproSeqNo";
			public const string ItemID = "ItemID";
			public const string ZatActiveID = "ZatActiveID";
			public const string SRItemUnit = "SRItemUnit";
			public const string SRDosageUnit = "SRDosageUnit";
			public const string AcPcDc = "AcPcDc";
			public const string SRMedicationRoute = "SRMedicationRoute";
			public const string SRConsumeMethod = "SRConsumeMethod";
			public const string DosageQty = "DosageQty";
			public const string EmbalaceQty = "EmbalaceQty";
			public const string ConsumeQty = "ConsumeQty";
			public const string SRConsumeUnit = "SRConsumeUnit";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string StartDateTime = "StartDateTime";
			public const string StopDateTime = "StopDateTime";
			public const string RasprajaSeqNo = "RasprajaSeqNo";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string RasproSeqNo = "RasproSeqNo";
			public const string ItemID = "ItemID";
			public const string ZatActiveID = "ZatActiveID";
			public const string SRItemUnit = "SRItemUnit";
			public const string SRDosageUnit = "SRDosageUnit";
			public const string AcPcDc = "AcPcDc";
			public const string SRMedicationRoute = "SRMedicationRoute";
			public const string SRConsumeMethod = "SRConsumeMethod";
			public const string DosageQty = "DosageQty";
			public const string EmbalaceQty = "EmbalaceQty";
			public const string ConsumeQty = "ConsumeQty";
			public const string SRConsumeUnit = "SRConsumeUnit";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string StartDateTime = "StartDateTime";
			public const string StopDateTime = "StopDateTime";
			public const string RasprajaSeqNo = "RasprajaSeqNo";
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
			lock (typeof(RegistrationRasproItemMetadata))
			{
				if (RegistrationRasproItemMetadata.mapDelegates == null)
				{
					RegistrationRasproItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (RegistrationRasproItemMetadata.meta == null)
				{
					RegistrationRasproItemMetadata.meta = new RegistrationRasproItemMetadata();
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
				meta.AddTypeMap("RasproSeqNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ZatActiveID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDosageUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AcPcDc", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMedicationRoute", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRConsumeMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DosageQty", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmbalaceQty", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConsumeQty", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRConsumeUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("StopDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RasprajaSeqNo", new esTypeMap("int", "System.Int32"));


				meta.Source = "RegistrationRasproItem";
				meta.Destination = "RegistrationRasproItem";
				meta.spInsert = "proc_RegistrationRasproItemInsert";
				meta.spUpdate = "proc_RegistrationRasproItemUpdate";
				meta.spDelete = "proc_RegistrationRasproItemDelete";
				meta.spLoadAll = "proc_RegistrationRasproItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationRasproItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationRasproItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
