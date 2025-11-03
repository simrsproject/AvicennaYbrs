/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/14/2022 1:33:03 PM
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
	abstract public class esItemProductMarginValueCollection : esEntityCollectionWAuditLog
	{
		public esItemProductMarginValueCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemProductMarginValueCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemProductMarginValueQuery query)
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
			this.InitQuery(query as esItemProductMarginValueQuery);
		}
		#endregion

		virtual public ItemProductMarginValue DetachEntity(ItemProductMarginValue entity)
		{
			return base.DetachEntity(entity) as ItemProductMarginValue;
		}

		virtual public ItemProductMarginValue AttachEntity(ItemProductMarginValue entity)
		{
			return base.AttachEntity(entity) as ItemProductMarginValue;
		}

		virtual public void Combine(ItemProductMarginValueCollection collection)
		{
			base.Combine(collection);
		}

		new public ItemProductMarginValue this[int index]
		{
			get
			{
				return base[index] as ItemProductMarginValue;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemProductMarginValue);
		}
	}

	[Serializable]
	abstract public class esItemProductMarginValue : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemProductMarginValueQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemProductMarginValue()
		{
		}

		public esItemProductMarginValue(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String marginID, String sequenceNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(marginID, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(marginID, sequenceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String marginID, String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(marginID, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(marginID, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(String marginID, String sequenceNo)
		{
			esItemProductMarginValueQuery query = this.GetDynamicQuery();
			query.Where(query.MarginID == marginID, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String marginID, String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("MarginID", marginID);
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
						case "MarginID": this.str.MarginID = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "StartingValue": this.str.StartingValue = (string)value; break;
						case "EndingValue": this.str.EndingValue = (string)value; break;
						case "MarginPercentage": this.str.MarginPercentage = (string)value; break;
						case "IsMinusDiscount": this.str.IsMinusDiscount = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "OTCMarginPercentage": this.str.OTCMarginPercentage = (string)value; break;
						case "OutpatientMarginPercentage": this.str.OutpatientMarginPercentage = (string)value; break;
						case "InpatientMarginPercentage": this.str.InpatientMarginPercentage = (string)value; break;
						case "IsGlobalWithoutVAT": this.str.IsGlobalWithoutVAT = (string)value; break;
						case "IsIpWithoutVAT": this.str.IsIpWithoutVAT = (string)value; break;
						case "IsOpWithoutVAT": this.str.IsOpWithoutVAT = (string)value; break;
						case "IsOtcWithoutVAT": this.str.IsOtcWithoutVAT = (string)value; break;
						case "EmergencyMarginPercentage": this.str.EmergencyMarginPercentage = (string)value; break;
						case "IsEmWithoutVAT": this.str.IsEmWithoutVAT = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "StartingValue":

							if (value == null || value is System.Decimal)
								this.StartingValue = (System.Decimal?)value;
							break;
						case "EndingValue":

							if (value == null || value is System.Decimal)
								this.EndingValue = (System.Decimal?)value;
							break;
						case "MarginPercentage":

							if (value == null || value is System.Decimal)
								this.MarginPercentage = (System.Decimal?)value;
							break;
						case "IsMinusDiscount":

							if (value == null || value is System.Boolean)
								this.IsMinusDiscount = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "OTCMarginPercentage":

							if (value == null || value is System.Decimal)
								this.OTCMarginPercentage = (System.Decimal?)value;
							break;
						case "OutpatientMarginPercentage":

							if (value == null || value is System.Decimal)
								this.OutpatientMarginPercentage = (System.Decimal?)value;
							break;
						case "InpatientMarginPercentage":

							if (value == null || value is System.Decimal)
								this.InpatientMarginPercentage = (System.Decimal?)value;
							break;
						case "IsGlobalWithoutVAT":

							if (value == null || value is System.Boolean)
								this.IsGlobalWithoutVAT = (System.Boolean?)value;
							break;
						case "IsIpWithoutVAT":

							if (value == null || value is System.Boolean)
								this.IsIpWithoutVAT = (System.Boolean?)value;
							break;
						case "IsOpWithoutVAT":

							if (value == null || value is System.Boolean)
								this.IsOpWithoutVAT = (System.Boolean?)value;
							break;
						case "IsOtcWithoutVAT":

							if (value == null || value is System.Boolean)
								this.IsOtcWithoutVAT = (System.Boolean?)value;
							break;
						case "EmergencyMarginPercentage":

							if (value == null || value is System.Decimal)
								this.EmergencyMarginPercentage = (System.Decimal?)value;
							break;
						case "IsEmWithoutVAT":

							if (value == null || value is System.Boolean)
								this.IsEmWithoutVAT = (System.Boolean?)value;
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
		/// Maps to ItemProductMarginValue.MarginID
		/// </summary>
		virtual public System.String MarginID
		{
			get
			{
				return base.GetSystemString(ItemProductMarginValueMetadata.ColumnNames.MarginID);
			}

			set
			{
				base.SetSystemString(ItemProductMarginValueMetadata.ColumnNames.MarginID, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMarginValue.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(ItemProductMarginValueMetadata.ColumnNames.SequenceNo);
			}

			set
			{
				base.SetSystemString(ItemProductMarginValueMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMarginValue.StartingValue
		/// </summary>
		virtual public System.Decimal? StartingValue
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMarginValueMetadata.ColumnNames.StartingValue);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMarginValueMetadata.ColumnNames.StartingValue, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMarginValue.EndingValue
		/// </summary>
		virtual public System.Decimal? EndingValue
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMarginValueMetadata.ColumnNames.EndingValue);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMarginValueMetadata.ColumnNames.EndingValue, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMarginValue.MarginPercentage
		/// </summary>
		virtual public System.Decimal? MarginPercentage
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMarginValueMetadata.ColumnNames.MarginPercentage);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMarginValueMetadata.ColumnNames.MarginPercentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMarginValue.IsMinusDiscount
		/// </summary>
		virtual public System.Boolean? IsMinusDiscount
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMarginValueMetadata.ColumnNames.IsMinusDiscount);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMarginValueMetadata.ColumnNames.IsMinusDiscount, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMarginValue.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemProductMarginValueMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemProductMarginValueMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMarginValue.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemProductMarginValueMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemProductMarginValueMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMarginValue.OTCMarginPercentage
		/// </summary>
		virtual public System.Decimal? OTCMarginPercentage
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMarginValueMetadata.ColumnNames.OTCMarginPercentage);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMarginValueMetadata.ColumnNames.OTCMarginPercentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMarginValue.OutpatientMarginPercentage
		/// </summary>
		virtual public System.Decimal? OutpatientMarginPercentage
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMarginValueMetadata.ColumnNames.OutpatientMarginPercentage);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMarginValueMetadata.ColumnNames.OutpatientMarginPercentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMarginValue.InpatientMarginPercentage
		/// </summary>
		virtual public System.Decimal? InpatientMarginPercentage
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMarginValueMetadata.ColumnNames.InpatientMarginPercentage);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMarginValueMetadata.ColumnNames.InpatientMarginPercentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMarginValue.IsGlobalWithoutVAT
		/// </summary>
		virtual public System.Boolean? IsGlobalWithoutVAT
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMarginValueMetadata.ColumnNames.IsGlobalWithoutVAT);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMarginValueMetadata.ColumnNames.IsGlobalWithoutVAT, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMarginValue.IsIpWithoutVAT
		/// </summary>
		virtual public System.Boolean? IsIpWithoutVAT
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMarginValueMetadata.ColumnNames.IsIpWithoutVAT);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMarginValueMetadata.ColumnNames.IsIpWithoutVAT, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMarginValue.IsOpWithoutVAT
		/// </summary>
		virtual public System.Boolean? IsOpWithoutVAT
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMarginValueMetadata.ColumnNames.IsOpWithoutVAT);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMarginValueMetadata.ColumnNames.IsOpWithoutVAT, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMarginValue.IsOtcWithoutVAT
		/// </summary>
		virtual public System.Boolean? IsOtcWithoutVAT
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMarginValueMetadata.ColumnNames.IsOtcWithoutVAT);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMarginValueMetadata.ColumnNames.IsOtcWithoutVAT, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMarginValue.EmergencyMarginPercentage
		/// </summary>
		virtual public System.Decimal? EmergencyMarginPercentage
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMarginValueMetadata.ColumnNames.EmergencyMarginPercentage);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMarginValueMetadata.ColumnNames.EmergencyMarginPercentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMarginValue.IsEmWithoutVAT
		/// </summary>
		virtual public System.Boolean? IsEmWithoutVAT
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMarginValueMetadata.ColumnNames.IsEmWithoutVAT);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMarginValueMetadata.ColumnNames.IsEmWithoutVAT, value);
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
			public esStrings(esItemProductMarginValue entity)
			{
				this.entity = entity;
			}
			public System.String MarginID
			{
				get
				{
					System.String data = entity.MarginID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MarginID = null;
					else entity.MarginID = Convert.ToString(value);
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
			public System.String StartingValue
			{
				get
				{
					System.Decimal? data = entity.StartingValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartingValue = null;
					else entity.StartingValue = Convert.ToDecimal(value);
				}
			}
			public System.String EndingValue
			{
				get
				{
					System.Decimal? data = entity.EndingValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndingValue = null;
					else entity.EndingValue = Convert.ToDecimal(value);
				}
			}
			public System.String MarginPercentage
			{
				get
				{
					System.Decimal? data = entity.MarginPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MarginPercentage = null;
					else entity.MarginPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String IsMinusDiscount
			{
				get
				{
					System.Boolean? data = entity.IsMinusDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMinusDiscount = null;
					else entity.IsMinusDiscount = Convert.ToBoolean(value);
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
			public System.String OTCMarginPercentage
			{
				get
				{
					System.Decimal? data = entity.OTCMarginPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OTCMarginPercentage = null;
					else entity.OTCMarginPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String OutpatientMarginPercentage
			{
				get
				{
					System.Decimal? data = entity.OutpatientMarginPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OutpatientMarginPercentage = null;
					else entity.OutpatientMarginPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String InpatientMarginPercentage
			{
				get
				{
					System.Decimal? data = entity.InpatientMarginPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InpatientMarginPercentage = null;
					else entity.InpatientMarginPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String IsGlobalWithoutVAT
			{
				get
				{
					System.Boolean? data = entity.IsGlobalWithoutVAT;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGlobalWithoutVAT = null;
					else entity.IsGlobalWithoutVAT = Convert.ToBoolean(value);
				}
			}
			public System.String IsIpWithoutVAT
			{
				get
				{
					System.Boolean? data = entity.IsIpWithoutVAT;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsIpWithoutVAT = null;
					else entity.IsIpWithoutVAT = Convert.ToBoolean(value);
				}
			}
			public System.String IsOpWithoutVAT
			{
				get
				{
					System.Boolean? data = entity.IsOpWithoutVAT;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOpWithoutVAT = null;
					else entity.IsOpWithoutVAT = Convert.ToBoolean(value);
				}
			}
			public System.String IsOtcWithoutVAT
			{
				get
				{
					System.Boolean? data = entity.IsOtcWithoutVAT;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOtcWithoutVAT = null;
					else entity.IsOtcWithoutVAT = Convert.ToBoolean(value);
				}
			}
			public System.String EmergencyMarginPercentage
			{
				get
				{
					System.Decimal? data = entity.EmergencyMarginPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmergencyMarginPercentage = null;
					else entity.EmergencyMarginPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String IsEmWithoutVAT
			{
				get
				{
					System.Boolean? data = entity.IsEmWithoutVAT;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEmWithoutVAT = null;
					else entity.IsEmWithoutVAT = Convert.ToBoolean(value);
				}
			}
			private esItemProductMarginValue entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemProductMarginValueQuery query)
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
				throw new Exception("esItemProductMarginValue can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemProductMarginValue : esItemProductMarginValue
	{
	}

	[Serializable]
	abstract public class esItemProductMarginValueQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemProductMarginValueMetadata.Meta();
			}
		}

		public esQueryItem MarginID
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginValueMetadata.ColumnNames.MarginID, esSystemType.String);
			}
		}

		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginValueMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		}

		public esQueryItem StartingValue
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginValueMetadata.ColumnNames.StartingValue, esSystemType.Decimal);
			}
		}

		public esQueryItem EndingValue
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginValueMetadata.ColumnNames.EndingValue, esSystemType.Decimal);
			}
		}

		public esQueryItem MarginPercentage
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginValueMetadata.ColumnNames.MarginPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem IsMinusDiscount
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginValueMetadata.ColumnNames.IsMinusDiscount, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginValueMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginValueMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem OTCMarginPercentage
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginValueMetadata.ColumnNames.OTCMarginPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem OutpatientMarginPercentage
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginValueMetadata.ColumnNames.OutpatientMarginPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem InpatientMarginPercentage
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginValueMetadata.ColumnNames.InpatientMarginPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem IsGlobalWithoutVAT
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginValueMetadata.ColumnNames.IsGlobalWithoutVAT, esSystemType.Boolean);
			}
		}

		public esQueryItem IsIpWithoutVAT
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginValueMetadata.ColumnNames.IsIpWithoutVAT, esSystemType.Boolean);
			}
		}

		public esQueryItem IsOpWithoutVAT
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginValueMetadata.ColumnNames.IsOpWithoutVAT, esSystemType.Boolean);
			}
		}

		public esQueryItem IsOtcWithoutVAT
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginValueMetadata.ColumnNames.IsOtcWithoutVAT, esSystemType.Boolean);
			}
		}

		public esQueryItem EmergencyMarginPercentage
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginValueMetadata.ColumnNames.EmergencyMarginPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem IsEmWithoutVAT
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginValueMetadata.ColumnNames.IsEmWithoutVAT, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemProductMarginValueCollection")]
	public partial class ItemProductMarginValueCollection : esItemProductMarginValueCollection, IEnumerable<ItemProductMarginValue>
	{
		public ItemProductMarginValueCollection()
		{

		}

		public static implicit operator List<ItemProductMarginValue>(ItemProductMarginValueCollection coll)
		{
			List<ItemProductMarginValue> list = new List<ItemProductMarginValue>();

			foreach (ItemProductMarginValue emp in coll)
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
				return ItemProductMarginValueMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductMarginValueQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemProductMarginValue(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemProductMarginValue();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemProductMarginValueQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductMarginValueQuery();
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
		public bool Load(ItemProductMarginValueQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemProductMarginValue AddNew()
		{
			ItemProductMarginValue entity = base.AddNewEntity() as ItemProductMarginValue;

			return entity;
		}
		public ItemProductMarginValue FindByPrimaryKey(String marginID, String sequenceNo)
		{
			return base.FindByPrimaryKey(marginID, sequenceNo) as ItemProductMarginValue;
		}

		#region IEnumerable< ItemProductMarginValue> Members

		IEnumerator<ItemProductMarginValue> IEnumerable<ItemProductMarginValue>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemProductMarginValue;
			}
		}

		#endregion

		private ItemProductMarginValueQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemProductMarginValue' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemProductMarginValue ({MarginID, SequenceNo})")]
	[Serializable]
	public partial class ItemProductMarginValue : esItemProductMarginValue
	{
		public ItemProductMarginValue()
		{
		}

		public ItemProductMarginValue(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemProductMarginValueMetadata.Meta();
			}
		}

		override protected esItemProductMarginValueQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductMarginValueQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemProductMarginValueQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductMarginValueQuery();
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
		public bool Load(ItemProductMarginValueQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemProductMarginValueQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemProductMarginValueQuery : esItemProductMarginValueQuery
	{
		public ItemProductMarginValueQuery()
		{

		}

		public ItemProductMarginValueQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemProductMarginValueQuery";
		}
	}

	[Serializable]
	public partial class ItemProductMarginValueMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemProductMarginValueMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemProductMarginValueMetadata.ColumnNames.MarginID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMarginValueMetadata.PropertyNames.MarginID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMarginValueMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMarginValueMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			c.HasDefault = true;
			c.Default = @"('000')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMarginValueMetadata.ColumnNames.StartingValue, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMarginValueMetadata.PropertyNames.StartingValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMarginValueMetadata.ColumnNames.EndingValue, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMarginValueMetadata.PropertyNames.EndingValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMarginValueMetadata.ColumnNames.MarginPercentage, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMarginValueMetadata.PropertyNames.MarginPercentage;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMarginValueMetadata.ColumnNames.IsMinusDiscount, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMarginValueMetadata.PropertyNames.IsMinusDiscount;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMarginValueMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemProductMarginValueMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMarginValueMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMarginValueMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMarginValueMetadata.ColumnNames.OTCMarginPercentage, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMarginValueMetadata.PropertyNames.OTCMarginPercentage;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMarginValueMetadata.ColumnNames.OutpatientMarginPercentage, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMarginValueMetadata.PropertyNames.OutpatientMarginPercentage;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMarginValueMetadata.ColumnNames.InpatientMarginPercentage, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMarginValueMetadata.PropertyNames.InpatientMarginPercentage;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMarginValueMetadata.ColumnNames.IsGlobalWithoutVAT, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMarginValueMetadata.PropertyNames.IsGlobalWithoutVAT;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMarginValueMetadata.ColumnNames.IsIpWithoutVAT, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMarginValueMetadata.PropertyNames.IsIpWithoutVAT;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMarginValueMetadata.ColumnNames.IsOpWithoutVAT, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMarginValueMetadata.PropertyNames.IsOpWithoutVAT;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMarginValueMetadata.ColumnNames.IsOtcWithoutVAT, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMarginValueMetadata.PropertyNames.IsOtcWithoutVAT;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMarginValueMetadata.ColumnNames.EmergencyMarginPercentage, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMarginValueMetadata.PropertyNames.EmergencyMarginPercentage;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMarginValueMetadata.ColumnNames.IsEmWithoutVAT, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMarginValueMetadata.PropertyNames.IsEmWithoutVAT;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemProductMarginValueMetadata Meta()
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
			public const string MarginID = "MarginID";
			public const string SequenceNo = "SequenceNo";
			public const string StartingValue = "StartingValue";
			public const string EndingValue = "EndingValue";
			public const string MarginPercentage = "MarginPercentage";
			public const string IsMinusDiscount = "IsMinusDiscount";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string OTCMarginPercentage = "OTCMarginPercentage";
			public const string OutpatientMarginPercentage = "OutpatientMarginPercentage";
			public const string InpatientMarginPercentage = "InpatientMarginPercentage";
			public const string IsGlobalWithoutVAT = "IsGlobalWithoutVAT";
			public const string IsIpWithoutVAT = "IsIpWithoutVAT";
			public const string IsOpWithoutVAT = "IsOpWithoutVAT";
			public const string IsOtcWithoutVAT = "IsOtcWithoutVAT";
			public const string EmergencyMarginPercentage = "EmergencyMarginPercentage";
			public const string IsEmWithoutVAT = "IsEmWithoutVAT";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string MarginID = "MarginID";
			public const string SequenceNo = "SequenceNo";
			public const string StartingValue = "StartingValue";
			public const string EndingValue = "EndingValue";
			public const string MarginPercentage = "MarginPercentage";
			public const string IsMinusDiscount = "IsMinusDiscount";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string OTCMarginPercentage = "OTCMarginPercentage";
			public const string OutpatientMarginPercentage = "OutpatientMarginPercentage";
			public const string InpatientMarginPercentage = "InpatientMarginPercentage";
			public const string IsGlobalWithoutVAT = "IsGlobalWithoutVAT";
			public const string IsIpWithoutVAT = "IsIpWithoutVAT";
			public const string IsOpWithoutVAT = "IsOpWithoutVAT";
			public const string IsOtcWithoutVAT = "IsOtcWithoutVAT";
			public const string EmergencyMarginPercentage = "EmergencyMarginPercentage";
			public const string IsEmWithoutVAT = "IsEmWithoutVAT";
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
			lock (typeof(ItemProductMarginValueMetadata))
			{
				if (ItemProductMarginValueMetadata.mapDelegates == null)
				{
					ItemProductMarginValueMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemProductMarginValueMetadata.meta == null)
				{
					ItemProductMarginValueMetadata.meta = new ItemProductMarginValueMetadata();
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

				meta.AddTypeMap("MarginID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartingValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("EndingValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("MarginPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsMinusDiscount", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OTCMarginPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("OutpatientMarginPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("InpatientMarginPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsGlobalWithoutVAT", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsIpWithoutVAT", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsOpWithoutVAT", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsOtcWithoutVAT", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("EmergencyMarginPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsEmWithoutVAT", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "ItemProductMarginValue";
				meta.Destination = "ItemProductMarginValue";
				meta.spInsert = "proc_ItemProductMarginValueInsert";
				meta.spUpdate = "proc_ItemProductMarginValueUpdate";
				meta.spDelete = "proc_ItemProductMarginValueDelete";
				meta.spLoadAll = "proc_ItemProductMarginValueLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemProductMarginValueLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemProductMarginValueMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
