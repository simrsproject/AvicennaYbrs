/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/2/2021 5:10:42 PM
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
	abstract public class esItemTariffRequestProcessCollection : esEntityCollectionWAuditLog
	{
		public esItemTariffRequestProcessCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemTariffRequestProcessCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemTariffRequestProcessQuery query)
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
			this.InitQuery(query as esItemTariffRequestProcessQuery);
		}
		#endregion

		virtual public ItemTariffRequestProcess DetachEntity(ItemTariffRequestProcess entity)
		{
			return base.DetachEntity(entity) as ItemTariffRequestProcess;
		}

		virtual public ItemTariffRequestProcess AttachEntity(ItemTariffRequestProcess entity)
		{
			return base.AttachEntity(entity) as ItemTariffRequestProcess;
		}

		virtual public void Combine(ItemTariffRequestProcessCollection collection)
		{
			base.Combine(collection);
		}

		new public ItemTariffRequestProcess this[int index]
		{
			get
			{
				return base[index] as ItemTariffRequestProcess;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemTariffRequestProcess);
		}
	}

	[Serializable]
	abstract public class esItemTariffRequestProcess : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemTariffRequestProcessQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemTariffRequestProcess()
		{
		}

		public esItemTariffRequestProcess(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String tariffRequestNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(tariffRequestNo);
			else
				return LoadByPrimaryKeyStoredProcedure(tariffRequestNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String tariffRequestNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(tariffRequestNo);
			else
				return LoadByPrimaryKeyStoredProcedure(tariffRequestNo);
		}

		private bool LoadByPrimaryKeyDynamic(String tariffRequestNo)
		{
			esItemTariffRequestProcessQuery query = this.GetDynamicQuery();
			query.Where(query.TariffRequestNo == tariffRequestNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String tariffRequestNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TariffRequestNo", tariffRequestNo);
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
						case "TariffRequestNo": this.str.TariffRequestNo = (string)value; break;
						case "TariffRequestDate": this.str.TariffRequestDate = (string)value; break;
						case "SRItemType": this.str.SRItemType = (string)value; break;
						case "FromSRTariffType": this.str.FromSRTariffType = (string)value; break;
						case "ToSRTariffType": this.str.ToSRTariffType = (string)value; break;
						case "StartingDate": this.str.StartingDate = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDate": this.str.VoidDate = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDate": this.str.ApprovedDate = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ItemGroupID": this.str.ItemGroupID = (string)value; break;
						case "RoundingValue": this.str.RoundingValue = (string)value; break;
						case "IsRoundingDown": this.str.IsRoundingDown = (string)value; break;
						case "FromDate": this.str.FromDate = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "TariffRequestDate":

							if (value == null || value is System.DateTime)
								this.TariffRequestDate = (System.DateTime?)value;
							break;
						case "StartingDate":

							if (value == null || value is System.DateTime)
								this.StartingDate = (System.DateTime?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDate":

							if (value == null || value is System.DateTime)
								this.VoidDate = (System.DateTime?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDate":

							if (value == null || value is System.DateTime)
								this.ApprovedDate = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "RoundingValue":

							if (value == null || value is System.Decimal)
								this.RoundingValue = (System.Decimal?)value;
							break;
						case "IsRoundingDown":

							if (value == null || value is System.Boolean)
								this.IsRoundingDown = (System.Boolean?)value;
							break;
						case "FromDate":

							if (value == null || value is System.DateTime)
								this.FromDate = (System.DateTime?)value;
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
		/// Maps to ItemTariffRequestProcess.TariffRequestNo
		/// </summary>
		virtual public System.String TariffRequestNo
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestProcessMetadata.ColumnNames.TariffRequestNo);
			}

			set
			{
				base.SetSystemString(ItemTariffRequestProcessMetadata.ColumnNames.TariffRequestNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestProcess.TariffRequestDate
		/// </summary>
		virtual public System.DateTime? TariffRequestDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequestProcessMetadata.ColumnNames.TariffRequestDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTariffRequestProcessMetadata.ColumnNames.TariffRequestDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestProcess.SRItemType
		/// </summary>
		virtual public System.String SRItemType
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestProcessMetadata.ColumnNames.SRItemType);
			}

			set
			{
				base.SetSystemString(ItemTariffRequestProcessMetadata.ColumnNames.SRItemType, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestProcess.FromSRTariffType
		/// </summary>
		virtual public System.String FromSRTariffType
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestProcessMetadata.ColumnNames.FromSRTariffType);
			}

			set
			{
				base.SetSystemString(ItemTariffRequestProcessMetadata.ColumnNames.FromSRTariffType, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestProcess.ToSRTariffType
		/// </summary>
		virtual public System.String ToSRTariffType
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestProcessMetadata.ColumnNames.ToSRTariffType);
			}

			set
			{
				base.SetSystemString(ItemTariffRequestProcessMetadata.ColumnNames.ToSRTariffType, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestProcess.StartingDate
		/// </summary>
		virtual public System.DateTime? StartingDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequestProcessMetadata.ColumnNames.StartingDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTariffRequestProcessMetadata.ColumnNames.StartingDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestProcess.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestProcessMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(ItemTariffRequestProcessMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestProcess.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffRequestProcessMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(ItemTariffRequestProcessMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestProcess.VoidDate
		/// </summary>
		virtual public System.DateTime? VoidDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequestProcessMetadata.ColumnNames.VoidDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTariffRequestProcessMetadata.ColumnNames.VoidDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestProcess.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestProcessMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(ItemTariffRequestProcessMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestProcess.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffRequestProcessMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(ItemTariffRequestProcessMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestProcess.ApprovedDate
		/// </summary>
		virtual public System.DateTime? ApprovedDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequestProcessMetadata.ColumnNames.ApprovedDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTariffRequestProcessMetadata.ColumnNames.ApprovedDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestProcess.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestProcessMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(ItemTariffRequestProcessMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestProcess.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequestProcessMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemTariffRequestProcessMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestProcess.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestProcessMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemTariffRequestProcessMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestProcess.ItemGroupID
		/// </summary>
		virtual public System.String ItemGroupID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestProcessMetadata.ColumnNames.ItemGroupID);
			}

			set
			{
				base.SetSystemString(ItemTariffRequestProcessMetadata.ColumnNames.ItemGroupID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestProcess.RoundingValue
		/// </summary>
		virtual public System.Decimal? RoundingValue
		{
			get
			{
				return base.GetSystemDecimal(ItemTariffRequestProcessMetadata.ColumnNames.RoundingValue);
			}

			set
			{
				base.SetSystemDecimal(ItemTariffRequestProcessMetadata.ColumnNames.RoundingValue, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestProcess.IsRoundingDown
		/// </summary>
		virtual public System.Boolean? IsRoundingDown
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffRequestProcessMetadata.ColumnNames.IsRoundingDown);
			}

			set
			{
				base.SetSystemBoolean(ItemTariffRequestProcessMetadata.ColumnNames.IsRoundingDown, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestProcess.FromDate
		/// </summary>
		virtual public System.DateTime? FromDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequestProcessMetadata.ColumnNames.FromDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTariffRequestProcessMetadata.ColumnNames.FromDate, value);
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
			public esStrings(esItemTariffRequestProcess entity)
			{
				this.entity = entity;
			}
			public System.String TariffRequestNo
			{
				get
				{
					System.String data = entity.TariffRequestNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffRequestNo = null;
					else entity.TariffRequestNo = Convert.ToString(value);
				}
			}
			public System.String TariffRequestDate
			{
				get
				{
					System.DateTime? data = entity.TariffRequestDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffRequestDate = null;
					else entity.TariffRequestDate = Convert.ToDateTime(value);
				}
			}
			public System.String SRItemType
			{
				get
				{
					System.String data = entity.SRItemType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemType = null;
					else entity.SRItemType = Convert.ToString(value);
				}
			}
			public System.String FromSRTariffType
			{
				get
				{
					System.String data = entity.FromSRTariffType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromSRTariffType = null;
					else entity.FromSRTariffType = Convert.ToString(value);
				}
			}
			public System.String ToSRTariffType
			{
				get
				{
					System.String data = entity.ToSRTariffType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToSRTariffType = null;
					else entity.ToSRTariffType = Convert.ToString(value);
				}
			}
			public System.String StartingDate
			{
				get
				{
					System.DateTime? data = entity.StartingDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartingDate = null;
					else entity.StartingDate = Convert.ToDateTime(value);
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
			public System.String VoidDate
			{
				get
				{
					System.DateTime? data = entity.VoidDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDate = null;
					else entity.VoidDate = Convert.ToDateTime(value);
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
			public System.String ApprovedDate
			{
				get
				{
					System.DateTime? data = entity.ApprovedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDate = null;
					else entity.ApprovedDate = Convert.ToDateTime(value);
				}
			}
			public System.String ApprovedByUserID
			{
				get
				{
					System.String data = entity.ApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
					else entity.ApprovedByUserID = Convert.ToString(value);
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
			public System.String ItemGroupID
			{
				get
				{
					System.String data = entity.ItemGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemGroupID = null;
					else entity.ItemGroupID = Convert.ToString(value);
				}
			}
			public System.String RoundingValue
			{
				get
				{
					System.Decimal? data = entity.RoundingValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RoundingValue = null;
					else entity.RoundingValue = Convert.ToDecimal(value);
				}
			}
			public System.String IsRoundingDown
			{
				get
				{
					System.Boolean? data = entity.IsRoundingDown;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRoundingDown = null;
					else entity.IsRoundingDown = Convert.ToBoolean(value);
				}
			}
			public System.String FromDate
			{
				get
				{
					System.DateTime? data = entity.FromDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromDate = null;
					else entity.FromDate = Convert.ToDateTime(value);
				}
			}
			private esItemTariffRequestProcess entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemTariffRequestProcessQuery query)
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
				throw new Exception("esItemTariffRequestProcess can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemTariffRequestProcess : esItemTariffRequestProcess
	{
	}

	[Serializable]
	abstract public class esItemTariffRequestProcessQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemTariffRequestProcessMetadata.Meta();
			}
		}

		public esQueryItem TariffRequestNo
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestProcessMetadata.ColumnNames.TariffRequestNo, esSystemType.String);
			}
		}

		public esQueryItem TariffRequestDate
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestProcessMetadata.ColumnNames.TariffRequestDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SRItemType
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestProcessMetadata.ColumnNames.SRItemType, esSystemType.String);
			}
		}

		public esQueryItem FromSRTariffType
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestProcessMetadata.ColumnNames.FromSRTariffType, esSystemType.String);
			}
		}

		public esQueryItem ToSRTariffType
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestProcessMetadata.ColumnNames.ToSRTariffType, esSystemType.String);
			}
		}

		public esQueryItem StartingDate
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestProcessMetadata.ColumnNames.StartingDate, esSystemType.DateTime);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestProcessMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestProcessMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDate
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestProcessMetadata.ColumnNames.VoidDate, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestProcessMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestProcessMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDate
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestProcessMetadata.ColumnNames.ApprovedDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestProcessMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestProcessMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestProcessMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem ItemGroupID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestProcessMetadata.ColumnNames.ItemGroupID, esSystemType.String);
			}
		}

		public esQueryItem RoundingValue
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestProcessMetadata.ColumnNames.RoundingValue, esSystemType.Decimal);
			}
		}

		public esQueryItem IsRoundingDown
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestProcessMetadata.ColumnNames.IsRoundingDown, esSystemType.Boolean);
			}
		}

		public esQueryItem FromDate
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestProcessMetadata.ColumnNames.FromDate, esSystemType.DateTime);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemTariffRequestProcessCollection")]
	public partial class ItemTariffRequestProcessCollection : esItemTariffRequestProcessCollection, IEnumerable<ItemTariffRequestProcess>
	{
		public ItemTariffRequestProcessCollection()
		{

		}

		public static implicit operator List<ItemTariffRequestProcess>(ItemTariffRequestProcessCollection coll)
		{
			List<ItemTariffRequestProcess> list = new List<ItemTariffRequestProcess>();

			foreach (ItemTariffRequestProcess emp in coll)
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
				return ItemTariffRequestProcessMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTariffRequestProcessQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemTariffRequestProcess(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemTariffRequestProcess();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemTariffRequestProcessQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTariffRequestProcessQuery();
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
		public bool Load(ItemTariffRequestProcessQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemTariffRequestProcess AddNew()
		{
			ItemTariffRequestProcess entity = base.AddNewEntity() as ItemTariffRequestProcess;

			return entity;
		}
		public ItemTariffRequestProcess FindByPrimaryKey(String tariffRequestNo)
		{
			return base.FindByPrimaryKey(tariffRequestNo) as ItemTariffRequestProcess;
		}

		#region IEnumerable< ItemTariffRequestProcess> Members

		IEnumerator<ItemTariffRequestProcess> IEnumerable<ItemTariffRequestProcess>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemTariffRequestProcess;
			}
		}

		#endregion

		private ItemTariffRequestProcessQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemTariffRequestProcess' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemTariffRequestProcess ({TariffRequestNo})")]
	[Serializable]
	public partial class ItemTariffRequestProcess : esItemTariffRequestProcess
	{
		public ItemTariffRequestProcess()
		{
		}

		public ItemTariffRequestProcess(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemTariffRequestProcessMetadata.Meta();
			}
		}

		override protected esItemTariffRequestProcessQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTariffRequestProcessQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemTariffRequestProcessQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTariffRequestProcessQuery();
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
		public bool Load(ItemTariffRequestProcessQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemTariffRequestProcessQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemTariffRequestProcessQuery : esItemTariffRequestProcessQuery
	{
		public ItemTariffRequestProcessQuery()
		{

		}

		public ItemTariffRequestProcessQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemTariffRequestProcessQuery";
		}
	}

	[Serializable]
	public partial class ItemTariffRequestProcessMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemTariffRequestProcessMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemTariffRequestProcessMetadata.ColumnNames.TariffRequestNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestProcessMetadata.PropertyNames.TariffRequestNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestProcessMetadata.ColumnNames.TariffRequestDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequestProcessMetadata.PropertyNames.TariffRequestDate;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestProcessMetadata.ColumnNames.SRItemType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestProcessMetadata.PropertyNames.SRItemType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestProcessMetadata.ColumnNames.FromSRTariffType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestProcessMetadata.PropertyNames.FromSRTariffType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestProcessMetadata.ColumnNames.ToSRTariffType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestProcessMetadata.PropertyNames.ToSRTariffType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestProcessMetadata.ColumnNames.StartingDate, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequestProcessMetadata.PropertyNames.StartingDate;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestProcessMetadata.ColumnNames.Notes, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestProcessMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestProcessMetadata.ColumnNames.IsVoid, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffRequestProcessMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestProcessMetadata.ColumnNames.VoidDate, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequestProcessMetadata.PropertyNames.VoidDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestProcessMetadata.ColumnNames.VoidByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestProcessMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestProcessMetadata.ColumnNames.IsApproved, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffRequestProcessMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestProcessMetadata.ColumnNames.ApprovedDate, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequestProcessMetadata.PropertyNames.ApprovedDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestProcessMetadata.ColumnNames.ApprovedByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestProcessMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestProcessMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequestProcessMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestProcessMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestProcessMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestProcessMetadata.ColumnNames.ItemGroupID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestProcessMetadata.PropertyNames.ItemGroupID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestProcessMetadata.ColumnNames.RoundingValue, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTariffRequestProcessMetadata.PropertyNames.RoundingValue;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestProcessMetadata.ColumnNames.IsRoundingDown, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffRequestProcessMetadata.PropertyNames.IsRoundingDown;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestProcessMetadata.ColumnNames.FromDate, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequestProcessMetadata.PropertyNames.FromDate;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemTariffRequestProcessMetadata Meta()
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
			public const string TariffRequestNo = "TariffRequestNo";
			public const string TariffRequestDate = "TariffRequestDate";
			public const string SRItemType = "SRItemType";
			public const string FromSRTariffType = "FromSRTariffType";
			public const string ToSRTariffType = "ToSRTariffType";
			public const string StartingDate = "StartingDate";
			public const string Notes = "Notes";
			public const string IsVoid = "IsVoid";
			public const string VoidDate = "VoidDate";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDate = "ApprovedDate";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ItemGroupID = "ItemGroupID";
			public const string RoundingValue = "RoundingValue";
			public const string IsRoundingDown = "IsRoundingDown";
			public const string FromDate = "FromDate";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TariffRequestNo = "TariffRequestNo";
			public const string TariffRequestDate = "TariffRequestDate";
			public const string SRItemType = "SRItemType";
			public const string FromSRTariffType = "FromSRTariffType";
			public const string ToSRTariffType = "ToSRTariffType";
			public const string StartingDate = "StartingDate";
			public const string Notes = "Notes";
			public const string IsVoid = "IsVoid";
			public const string VoidDate = "VoidDate";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDate = "ApprovedDate";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ItemGroupID = "ItemGroupID";
			public const string RoundingValue = "RoundingValue";
			public const string IsRoundingDown = "IsRoundingDown";
			public const string FromDate = "FromDate";
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
			lock (typeof(ItemTariffRequestProcessMetadata))
			{
				if (ItemTariffRequestProcessMetadata.mapDelegates == null)
				{
					ItemTariffRequestProcessMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemTariffRequestProcessMetadata.meta == null)
				{
					ItemTariffRequestProcessMetadata.meta = new ItemTariffRequestProcessMetadata();
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

				meta.AddTypeMap("TariffRequestNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffRequestDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRItemType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromSRTariffType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToSRTariffType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartingDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoundingValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsRoundingDown", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("FromDate", new esTypeMap("datetime", "System.DateTime"));


				meta.Source = "ItemTariffRequestProcess";
				meta.Destination = "ItemTariffRequestProcess";
				meta.spInsert = "proc_ItemTariffRequestProcessInsert";
				meta.spUpdate = "proc_ItemTariffRequestProcessUpdate";
				meta.spDelete = "proc_ItemTariffRequestProcessDelete";
				meta.spLoadAll = "proc_ItemTariffRequestProcessLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemTariffRequestProcessLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemTariffRequestProcessMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
