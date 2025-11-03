/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/29/2023 4:42:10 PM
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
	abstract public class esCssdSterileItemsReceivedItemCollection : esEntityCollectionWAuditLog
	{
		public esCssdSterileItemsReceivedItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CssdSterileItemsReceivedItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esCssdSterileItemsReceivedItemQuery query)
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
			this.InitQuery(query as esCssdSterileItemsReceivedItemQuery);
		}
		#endregion

		virtual public CssdSterileItemsReceivedItem DetachEntity(CssdSterileItemsReceivedItem entity)
		{
			return base.DetachEntity(entity) as CssdSterileItemsReceivedItem;
		}

		virtual public CssdSterileItemsReceivedItem AttachEntity(CssdSterileItemsReceivedItem entity)
		{
			return base.AttachEntity(entity) as CssdSterileItemsReceivedItem;
		}

		virtual public void Combine(CssdSterileItemsReceivedItemCollection collection)
		{
			base.Combine(collection);
		}

		new public CssdSterileItemsReceivedItem this[int index]
		{
			get
			{
				return base[index] as CssdSterileItemsReceivedItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CssdSterileItemsReceivedItem);
		}
	}

	[Serializable]
	abstract public class esCssdSterileItemsReceivedItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCssdSterileItemsReceivedItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esCssdSterileItemsReceivedItem()
		{
		}

		public esCssdSterileItemsReceivedItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String receivedNo, String receivedSeqNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(receivedNo, receivedSeqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(receivedNo, receivedSeqNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String receivedNo, String receivedSeqNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(receivedNo, receivedSeqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(receivedNo, receivedSeqNo);
		}

		private bool LoadByPrimaryKeyDynamic(String receivedNo, String receivedSeqNo)
		{
			esCssdSterileItemsReceivedItemQuery query = this.GetDynamicQuery();
			query.Where(query.ReceivedNo == receivedNo, query.ReceivedSeqNo == receivedSeqNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String receivedNo, String receivedSeqNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ReceivedNo", receivedNo);
			parms.Add("ReceivedSeqNo", receivedSeqNo);
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
						case "ReceivedNo": this.str.ReceivedNo = (string)value; break;
						case "ReceivedSeqNo": this.str.ReceivedSeqNo = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "SRCssdItemUnit": this.str.SRCssdItemUnit = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "CssdItemNo": this.str.CssdItemNo = (string)value; break;
						case "ExpiredDate": this.str.ExpiredDate = (string)value; break;
						case "ReuseTo": this.str.ReuseTo = (string)value; break;
						case "IsNeedUltrasound": this.str.IsNeedUltrasound = (string)value; break;
						case "IsDtt": this.str.IsDtt = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SRCssdPhase": this.str.SRCssdPhase = (string)value; break;
						case "IsDecontamination": this.str.IsDecontamination = (string)value; break;
						case "SRDecontaminationPhase": this.str.SRDecontaminationPhase = (string)value; break;
						case "IsFeasibilityTest": this.str.IsFeasibilityTest = (string)value; break;
						case "IsFeasibilityTestPassed": this.str.IsFeasibilityTestPassed = (string)value; break;
						case "QtyFeasibilityTestPassed": this.str.QtyFeasibilityTestPassed = (string)value; break;
						case "IsBrokenInstrument": this.str.IsBrokenInstrument = (string)value; break;
						case "QtyReplacements": this.str.QtyReplacements = (string)value; break;
						case "IsBrokenInstrumentDetail": this.str.IsBrokenInstrumentDetail = (string)value; break;
						case "QtyReplacementsDetail": this.str.QtyReplacementsDetail = (string)value; break;
						case "IsPackaging": this.str.IsPackaging = (string)value; break;
						case "IsUltrasound": this.str.IsUltrasound = (string)value; break;
						case "IsSterilization": this.str.IsSterilization = (string)value; break;
						case "IsReturned": this.str.IsReturned = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "Qty":

							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
							break;
						case "ExpiredDate":

							if (value == null || value is System.DateTime)
								this.ExpiredDate = (System.DateTime?)value;
							break;
						case "ReuseTo":

							if (value == null || value is System.Int16)
								this.ReuseTo = (System.Int16?)value;
							break;
						case "IsNeedUltrasound":

							if (value == null || value is System.Boolean)
								this.IsNeedUltrasound = (System.Boolean?)value;
							break;
						case "IsDtt":

							if (value == null || value is System.Boolean)
								this.IsDtt = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsDecontamination":

							if (value == null || value is System.Boolean)
								this.IsDecontamination = (System.Boolean?)value;
							break;
						case "IsFeasibilityTest":

							if (value == null || value is System.Boolean)
								this.IsFeasibilityTest = (System.Boolean?)value;
							break;
						case "IsFeasibilityTestPassed":

							if (value == null || value is System.Boolean)
								this.IsFeasibilityTestPassed = (System.Boolean?)value;
							break;
						case "QtyFeasibilityTestPassed":

							if (value == null || value is System.Decimal)
								this.QtyFeasibilityTestPassed = (System.Decimal?)value;
							break;
						case "IsBrokenInstrument":

							if (value == null || value is System.Boolean)
								this.IsBrokenInstrument = (System.Boolean?)value;
							break;
						case "QtyReplacements":

							if (value == null || value is System.Decimal)
								this.QtyReplacements = (System.Decimal?)value;
							break;
						case "IsBrokenInstrumentDetail":

							if (value == null || value is System.Boolean)
								this.IsBrokenInstrumentDetail = (System.Boolean?)value;
							break;
						case "QtyReplacementsDetail":

							if (value == null || value is System.Decimal)
								this.QtyReplacementsDetail = (System.Decimal?)value;
							break;
						case "IsPackaging":

							if (value == null || value is System.Boolean)
								this.IsPackaging = (System.Boolean?)value;
							break;
						case "IsUltrasound":

							if (value == null || value is System.Boolean)
								this.IsUltrasound = (System.Boolean?)value;
							break;
						case "IsSterilization":

							if (value == null || value is System.Boolean)
								this.IsSterilization = (System.Boolean?)value;
							break;
						case "IsReturned":

							if (value == null || value is System.Boolean)
								this.IsReturned = (System.Boolean?)value;
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
		/// Maps to CssdSterileItemsReceivedItem.ReceivedNo
		/// </summary>
		virtual public System.String ReceivedNo
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedItemMetadata.ColumnNames.ReceivedNo);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedItemMetadata.ColumnNames.ReceivedNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.ReceivedSeqNo
		/// </summary>
		virtual public System.String ReceivedSeqNo
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedItemMetadata.ColumnNames.ReceivedSeqNo);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedItemMetadata.ColumnNames.ReceivedSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedItemMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedItemMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.SRCssdItemUnit
		/// </summary>
		virtual public System.String SRCssdItemUnit
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedItemMetadata.ColumnNames.SRCssdItemUnit);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedItemMetadata.ColumnNames.SRCssdItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(CssdSterileItemsReceivedItemMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(CssdSterileItemsReceivedItemMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedItemMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedItemMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.CssdItemNo
		/// </summary>
		virtual public System.String CssdItemNo
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedItemMetadata.ColumnNames.CssdItemNo);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedItemMetadata.ColumnNames.CssdItemNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.ExpiredDate
		/// </summary>
		virtual public System.DateTime? ExpiredDate
		{
			get
			{
				return base.GetSystemDateTime(CssdSterileItemsReceivedItemMetadata.ColumnNames.ExpiredDate);
			}

			set
			{
				base.SetSystemDateTime(CssdSterileItemsReceivedItemMetadata.ColumnNames.ExpiredDate, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.ReuseTo
		/// </summary>
		virtual public System.Int16? ReuseTo
		{
			get
			{
				return base.GetSystemInt16(CssdSterileItemsReceivedItemMetadata.ColumnNames.ReuseTo);
			}

			set
			{
				base.SetSystemInt16(CssdSterileItemsReceivedItemMetadata.ColumnNames.ReuseTo, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.IsNeedUltrasound
		/// </summary>
		virtual public System.Boolean? IsNeedUltrasound
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsNeedUltrasound);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsNeedUltrasound, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.IsDtt
		/// </summary>
		virtual public System.Boolean? IsDtt
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsDtt);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsDtt, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdSterileItemsReceivedItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdSterileItemsReceivedItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.SRCssdPhase
		/// </summary>
		virtual public System.String SRCssdPhase
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedItemMetadata.ColumnNames.SRCssdPhase);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedItemMetadata.ColumnNames.SRCssdPhase, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.IsDecontamination
		/// </summary>
		virtual public System.Boolean? IsDecontamination
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsDecontamination);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsDecontamination, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.SRDecontaminationPhase
		/// </summary>
		virtual public System.String SRDecontaminationPhase
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedItemMetadata.ColumnNames.SRDecontaminationPhase);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedItemMetadata.ColumnNames.SRDecontaminationPhase, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.IsFeasibilityTest
		/// </summary>
		virtual public System.Boolean? IsFeasibilityTest
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsFeasibilityTest);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsFeasibilityTest, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.IsFeasibilityTestPassed
		/// </summary>
		virtual public System.Boolean? IsFeasibilityTestPassed
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsFeasibilityTestPassed);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsFeasibilityTestPassed, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.QtyFeasibilityTestPassed
		/// </summary>
		virtual public System.Decimal? QtyFeasibilityTestPassed
		{
			get
			{
				return base.GetSystemDecimal(CssdSterileItemsReceivedItemMetadata.ColumnNames.QtyFeasibilityTestPassed);
			}

			set
			{
				base.SetSystemDecimal(CssdSterileItemsReceivedItemMetadata.ColumnNames.QtyFeasibilityTestPassed, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.IsBrokenInstrument
		/// </summary>
		virtual public System.Boolean? IsBrokenInstrument
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsBrokenInstrument);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsBrokenInstrument, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.QtyReplacements
		/// </summary>
		virtual public System.Decimal? QtyReplacements
		{
			get
			{
				return base.GetSystemDecimal(CssdSterileItemsReceivedItemMetadata.ColumnNames.QtyReplacements);
			}

			set
			{
				base.SetSystemDecimal(CssdSterileItemsReceivedItemMetadata.ColumnNames.QtyReplacements, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.IsBrokenInstrumentDetail
		/// </summary>
		virtual public System.Boolean? IsBrokenInstrumentDetail
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsBrokenInstrumentDetail);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsBrokenInstrumentDetail, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.QtyReplacementsDetail
		/// </summary>
		virtual public System.Decimal? QtyReplacementsDetail
		{
			get
			{
				return base.GetSystemDecimal(CssdSterileItemsReceivedItemMetadata.ColumnNames.QtyReplacementsDetail);
			}

			set
			{
				base.SetSystemDecimal(CssdSterileItemsReceivedItemMetadata.ColumnNames.QtyReplacementsDetail, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.IsPackaging
		/// </summary>
		virtual public System.Boolean? IsPackaging
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsPackaging);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsPackaging, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.IsUltrasound
		/// </summary>
		virtual public System.Boolean? IsUltrasound
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsUltrasound);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsUltrasound, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.IsSterilization
		/// </summary>
		virtual public System.Boolean? IsSterilization
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsSterilization);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsSterilization, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceivedItem.IsReturned
		/// </summary>
		virtual public System.Boolean? IsReturned
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsReturned);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsReturned, value);
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
			public esStrings(esCssdSterileItemsReceivedItem entity)
			{
				this.entity = entity;
			}
			public System.String ReceivedNo
			{
				get
				{
					System.String data = entity.ReceivedNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedNo = null;
					else entity.ReceivedNo = Convert.ToString(value);
				}
			}
			public System.String ReceivedSeqNo
			{
				get
				{
					System.String data = entity.ReceivedSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedSeqNo = null;
					else entity.ReceivedSeqNo = Convert.ToString(value);
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
			public System.String SRCssdItemUnit
			{
				get
				{
					System.String data = entity.SRCssdItemUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCssdItemUnit = null;
					else entity.SRCssdItemUnit = Convert.ToString(value);
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
			public System.String CssdItemNo
			{
				get
				{
					System.String data = entity.CssdItemNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CssdItemNo = null;
					else entity.CssdItemNo = Convert.ToString(value);
				}
			}
			public System.String ExpiredDate
			{
				get
				{
					System.DateTime? data = entity.ExpiredDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExpiredDate = null;
					else entity.ExpiredDate = Convert.ToDateTime(value);
				}
			}
			public System.String ReuseTo
			{
				get
				{
					System.Int16? data = entity.ReuseTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReuseTo = null;
					else entity.ReuseTo = Convert.ToInt16(value);
				}
			}
			public System.String IsNeedUltrasound
			{
				get
				{
					System.Boolean? data = entity.IsNeedUltrasound;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNeedUltrasound = null;
					else entity.IsNeedUltrasound = Convert.ToBoolean(value);
				}
			}
			public System.String IsDtt
			{
				get
				{
					System.Boolean? data = entity.IsDtt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDtt = null;
					else entity.IsDtt = Convert.ToBoolean(value);
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
			public System.String SRCssdPhase
			{
				get
				{
					System.String data = entity.SRCssdPhase;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCssdPhase = null;
					else entity.SRCssdPhase = Convert.ToString(value);
				}
			}
			public System.String IsDecontamination
			{
				get
				{
					System.Boolean? data = entity.IsDecontamination;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDecontamination = null;
					else entity.IsDecontamination = Convert.ToBoolean(value);
				}
			}
			public System.String SRDecontaminationPhase
			{
				get
				{
					System.String data = entity.SRDecontaminationPhase;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDecontaminationPhase = null;
					else entity.SRDecontaminationPhase = Convert.ToString(value);
				}
			}
			public System.String IsFeasibilityTest
			{
				get
				{
					System.Boolean? data = entity.IsFeasibilityTest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeasibilityTest = null;
					else entity.IsFeasibilityTest = Convert.ToBoolean(value);
				}
			}
			public System.String IsFeasibilityTestPassed
			{
				get
				{
					System.Boolean? data = entity.IsFeasibilityTestPassed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeasibilityTestPassed = null;
					else entity.IsFeasibilityTestPassed = Convert.ToBoolean(value);
				}
			}
			public System.String QtyFeasibilityTestPassed
			{
				get
				{
					System.Decimal? data = entity.QtyFeasibilityTestPassed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyFeasibilityTestPassed = null;
					else entity.QtyFeasibilityTestPassed = Convert.ToDecimal(value);
				}
			}
			public System.String IsBrokenInstrument
			{
				get
				{
					System.Boolean? data = entity.IsBrokenInstrument;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBrokenInstrument = null;
					else entity.IsBrokenInstrument = Convert.ToBoolean(value);
				}
			}
			public System.String QtyReplacements
			{
				get
				{
					System.Decimal? data = entity.QtyReplacements;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyReplacements = null;
					else entity.QtyReplacements = Convert.ToDecimal(value);
				}
			}
			public System.String IsBrokenInstrumentDetail
			{
				get
				{
					System.Boolean? data = entity.IsBrokenInstrumentDetail;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBrokenInstrumentDetail = null;
					else entity.IsBrokenInstrumentDetail = Convert.ToBoolean(value);
				}
			}
			public System.String QtyReplacementsDetail
			{
				get
				{
					System.Decimal? data = entity.QtyReplacementsDetail;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyReplacementsDetail = null;
					else entity.QtyReplacementsDetail = Convert.ToDecimal(value);
				}
			}
			public System.String IsPackaging
			{
				get
				{
					System.Boolean? data = entity.IsPackaging;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPackaging = null;
					else entity.IsPackaging = Convert.ToBoolean(value);
				}
			}
			public System.String IsUltrasound
			{
				get
				{
					System.Boolean? data = entity.IsUltrasound;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUltrasound = null;
					else entity.IsUltrasound = Convert.ToBoolean(value);
				}
			}
			public System.String IsSterilization
			{
				get
				{
					System.Boolean? data = entity.IsSterilization;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSterilization = null;
					else entity.IsSterilization = Convert.ToBoolean(value);
				}
			}
			public System.String IsReturned
			{
				get
				{
					System.Boolean? data = entity.IsReturned;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReturned = null;
					else entity.IsReturned = Convert.ToBoolean(value);
				}
			}
			private esCssdSterileItemsReceivedItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCssdSterileItemsReceivedItemQuery query)
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
				throw new Exception("esCssdSterileItemsReceivedItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CssdSterileItemsReceivedItem : esCssdSterileItemsReceivedItem
	{
	}

	[Serializable]
	abstract public class esCssdSterileItemsReceivedItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CssdSterileItemsReceivedItemMetadata.Meta();
			}
		}

		public esQueryItem ReceivedNo
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.ReceivedNo, esSystemType.String);
			}
		}

		public esQueryItem ReceivedSeqNo
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.ReceivedSeqNo, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem SRCssdItemUnit
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.SRCssdItemUnit, esSystemType.String);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem CssdItemNo
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.CssdItemNo, esSystemType.String);
			}
		}

		public esQueryItem ExpiredDate
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.ExpiredDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ReuseTo
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.ReuseTo, esSystemType.Int16);
			}
		}

		public esQueryItem IsNeedUltrasound
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.IsNeedUltrasound, esSystemType.Boolean);
			}
		}

		public esQueryItem IsDtt
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.IsDtt, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SRCssdPhase
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.SRCssdPhase, esSystemType.String);
			}
		}

		public esQueryItem IsDecontamination
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.IsDecontamination, esSystemType.Boolean);
			}
		}

		public esQueryItem SRDecontaminationPhase
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.SRDecontaminationPhase, esSystemType.String);
			}
		}

		public esQueryItem IsFeasibilityTest
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.IsFeasibilityTest, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFeasibilityTestPassed
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.IsFeasibilityTestPassed, esSystemType.Boolean);
			}
		}

		public esQueryItem QtyFeasibilityTestPassed
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.QtyFeasibilityTestPassed, esSystemType.Decimal);
			}
		}

		public esQueryItem IsBrokenInstrument
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.IsBrokenInstrument, esSystemType.Boolean);
			}
		}

		public esQueryItem QtyReplacements
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.QtyReplacements, esSystemType.Decimal);
			}
		}

		public esQueryItem IsBrokenInstrumentDetail
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.IsBrokenInstrumentDetail, esSystemType.Boolean);
			}
		}

		public esQueryItem QtyReplacementsDetail
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.QtyReplacementsDetail, esSystemType.Decimal);
			}
		}

		public esQueryItem IsPackaging
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.IsPackaging, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUltrasound
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.IsUltrasound, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSterilization
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.IsSterilization, esSystemType.Boolean);
			}
		}

		public esQueryItem IsReturned
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedItemMetadata.ColumnNames.IsReturned, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CssdSterileItemsReceivedItemCollection")]
	public partial class CssdSterileItemsReceivedItemCollection : esCssdSterileItemsReceivedItemCollection, IEnumerable<CssdSterileItemsReceivedItem>
	{
		public CssdSterileItemsReceivedItemCollection()
		{

		}

		public static implicit operator List<CssdSterileItemsReceivedItem>(CssdSterileItemsReceivedItemCollection coll)
		{
			List<CssdSterileItemsReceivedItem> list = new List<CssdSterileItemsReceivedItem>();

			foreach (CssdSterileItemsReceivedItem emp in coll)
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
				return CssdSterileItemsReceivedItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdSterileItemsReceivedItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CssdSterileItemsReceivedItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CssdSterileItemsReceivedItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CssdSterileItemsReceivedItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdSterileItemsReceivedItemQuery();
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
		public bool Load(CssdSterileItemsReceivedItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CssdSterileItemsReceivedItem AddNew()
		{
			CssdSterileItemsReceivedItem entity = base.AddNewEntity() as CssdSterileItemsReceivedItem;

			return entity;
		}
		public CssdSterileItemsReceivedItem FindByPrimaryKey(String receivedNo, String receivedSeqNo)
		{
			return base.FindByPrimaryKey(receivedNo, receivedSeqNo) as CssdSterileItemsReceivedItem;
		}

		#region IEnumerable< CssdSterileItemsReceivedItem> Members

		IEnumerator<CssdSterileItemsReceivedItem> IEnumerable<CssdSterileItemsReceivedItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CssdSterileItemsReceivedItem;
			}
		}

		#endregion

		private CssdSterileItemsReceivedItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CssdSterileItemsReceivedItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CssdSterileItemsReceivedItem ({ReceivedNo, ReceivedSeqNo})")]
	[Serializable]
	public partial class CssdSterileItemsReceivedItem : esCssdSterileItemsReceivedItem
	{
		public CssdSterileItemsReceivedItem()
		{
		}

		public CssdSterileItemsReceivedItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CssdSterileItemsReceivedItemMetadata.Meta();
			}
		}

		override protected esCssdSterileItemsReceivedItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdSterileItemsReceivedItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CssdSterileItemsReceivedItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdSterileItemsReceivedItemQuery();
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
		public bool Load(CssdSterileItemsReceivedItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CssdSterileItemsReceivedItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CssdSterileItemsReceivedItemQuery : esCssdSterileItemsReceivedItemQuery
	{
		public CssdSterileItemsReceivedItemQuery()
		{

		}

		public CssdSterileItemsReceivedItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CssdSterileItemsReceivedItemQuery";
		}
	}

	[Serializable]
	public partial class CssdSterileItemsReceivedItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CssdSterileItemsReceivedItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.ReceivedNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.ReceivedNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.ReceivedSeqNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.ReceivedSeqNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.SRCssdItemUnit, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.SRCssdItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.Qty, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.CssdItemNo, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.CssdItemNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.ExpiredDate, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.ExpiredDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.ReuseTo, 8, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.ReuseTo;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsNeedUltrasound, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.IsNeedUltrasound;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsDtt, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.IsDtt;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.SRCssdPhase, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.SRCssdPhase;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsDecontamination, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.IsDecontamination;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.SRDecontaminationPhase, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.SRDecontaminationPhase;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsFeasibilityTest, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.IsFeasibilityTest;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsFeasibilityTestPassed, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.IsFeasibilityTestPassed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.QtyFeasibilityTestPassed, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.QtyFeasibilityTestPassed;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsBrokenInstrument, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.IsBrokenInstrument;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.QtyReplacements, 20, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.QtyReplacements;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsBrokenInstrumentDetail, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.IsBrokenInstrumentDetail;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.QtyReplacementsDetail, 22, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.QtyReplacementsDetail;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsPackaging, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.IsPackaging;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsUltrasound, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.IsUltrasound;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsSterilization, 25, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.IsSterilization;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedItemMetadata.ColumnNames.IsReturned, 26, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsReceivedItemMetadata.PropertyNames.IsReturned;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CssdSterileItemsReceivedItemMetadata Meta()
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
			public const string ReceivedNo = "ReceivedNo";
			public const string ReceivedSeqNo = "ReceivedSeqNo";
			public const string ItemID = "ItemID";
			public const string SRCssdItemUnit = "SRCssdItemUnit";
			public const string Qty = "Qty";
			public const string Notes = "Notes";
			public const string CssdItemNo = "CssdItemNo";
			public const string ExpiredDate = "ExpiredDate";
			public const string ReuseTo = "ReuseTo";
			public const string IsNeedUltrasound = "IsNeedUltrasound";
			public const string IsDtt = "IsDtt";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRCssdPhase = "SRCssdPhase";
			public const string IsDecontamination = "IsDecontamination";
			public const string SRDecontaminationPhase = "SRDecontaminationPhase";
			public const string IsFeasibilityTest = "IsFeasibilityTest";
			public const string IsFeasibilityTestPassed = "IsFeasibilityTestPassed";
			public const string QtyFeasibilityTestPassed = "QtyFeasibilityTestPassed";
			public const string IsBrokenInstrument = "IsBrokenInstrument";
			public const string QtyReplacements = "QtyReplacements";
			public const string IsBrokenInstrumentDetail = "IsBrokenInstrumentDetail";
			public const string QtyReplacementsDetail = "QtyReplacementsDetail";
			public const string IsPackaging = "IsPackaging";
			public const string IsUltrasound = "IsUltrasound";
			public const string IsSterilization = "IsSterilization";
			public const string IsReturned = "IsReturned";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ReceivedNo = "ReceivedNo";
			public const string ReceivedSeqNo = "ReceivedSeqNo";
			public const string ItemID = "ItemID";
			public const string SRCssdItemUnit = "SRCssdItemUnit";
			public const string Qty = "Qty";
			public const string Notes = "Notes";
			public const string CssdItemNo = "CssdItemNo";
			public const string ExpiredDate = "ExpiredDate";
			public const string ReuseTo = "ReuseTo";
			public const string IsNeedUltrasound = "IsNeedUltrasound";
			public const string IsDtt = "IsDtt";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRCssdPhase = "SRCssdPhase";
			public const string IsDecontamination = "IsDecontamination";
			public const string SRDecontaminationPhase = "SRDecontaminationPhase";
			public const string IsFeasibilityTest = "IsFeasibilityTest";
			public const string IsFeasibilityTestPassed = "IsFeasibilityTestPassed";
			public const string QtyFeasibilityTestPassed = "QtyFeasibilityTestPassed";
			public const string IsBrokenInstrument = "IsBrokenInstrument";
			public const string QtyReplacements = "QtyReplacements";
			public const string IsBrokenInstrumentDetail = "IsBrokenInstrumentDetail";
			public const string QtyReplacementsDetail = "QtyReplacementsDetail";
			public const string IsPackaging = "IsPackaging";
			public const string IsUltrasound = "IsUltrasound";
			public const string IsSterilization = "IsSterilization";
			public const string IsReturned = "IsReturned";
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
			lock (typeof(CssdSterileItemsReceivedItemMetadata))
			{
				if (CssdSterileItemsReceivedItemMetadata.mapDelegates == null)
				{
					CssdSterileItemsReceivedItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CssdSterileItemsReceivedItemMetadata.meta == null)
				{
					CssdSterileItemsReceivedItemMetadata.meta = new CssdSterileItemsReceivedItemMetadata();
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

				meta.AddTypeMap("ReceivedNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceivedSeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCssdItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CssdItemNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExpiredDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("ReuseTo", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("IsNeedUltrasound", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDtt", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCssdPhase", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDecontamination", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRDecontaminationPhase", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsFeasibilityTest", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFeasibilityTestPassed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("QtyFeasibilityTestPassed", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsBrokenInstrument", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("QtyReplacements", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsBrokenInstrumentDetail", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("QtyReplacementsDetail", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsPackaging", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUltrasound", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSterilization", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsReturned", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "CssdSterileItemsReceivedItem";
				meta.Destination = "CssdSterileItemsReceivedItem";
				meta.spInsert = "proc_CssdSterileItemsReceivedItemInsert";
				meta.spUpdate = "proc_CssdSterileItemsReceivedItemUpdate";
				meta.spDelete = "proc_CssdSterileItemsReceivedItemDelete";
				meta.spLoadAll = "proc_CssdSterileItemsReceivedItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_CssdSterileItemsReceivedItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CssdSterileItemsReceivedItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
