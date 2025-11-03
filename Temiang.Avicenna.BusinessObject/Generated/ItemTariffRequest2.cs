/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/19/2021 9:56:16 AM
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
	abstract public class esItemTariffRequest2Collection : esEntityCollectionWAuditLog
	{
		public esItemTariffRequest2Collection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemTariffRequest2Collection";
		}

		#region Query Logic
		protected void InitQuery(esItemTariffRequest2Query query)
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
			this.InitQuery(query as esItemTariffRequest2Query);
		}
		#endregion

		virtual public ItemTariffRequest2 DetachEntity(ItemTariffRequest2 entity)
		{
			return base.DetachEntity(entity) as ItemTariffRequest2;
		}

		virtual public ItemTariffRequest2 AttachEntity(ItemTariffRequest2 entity)
		{
			return base.AttachEntity(entity) as ItemTariffRequest2;
		}

		virtual public void Combine(ItemTariffRequest2Collection collection)
		{
			base.Combine(collection);
		}

		new public ItemTariffRequest2 this[int index]
		{
			get
			{
				return base[index] as ItemTariffRequest2;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemTariffRequest2);
		}
	}

	[Serializable]
	abstract public class esItemTariffRequest2 : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemTariffRequest2Query GetDynamicQuery()
		{
			return null;
		}

		public esItemTariffRequest2()
		{
		}

		public esItemTariffRequest2(DataRow row)
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
			esItemTariffRequest2Query query = this.GetDynamicQuery();
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
						case "SRTariffType": this.str.SRTariffType = (string)value; break;
						case "SRItemType": this.str.SRItemType = (string)value; break;
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
						case "IsImport": this.str.IsImport = (string)value; break;
						case "ItemGroupID": this.str.ItemGroupID = (string)value; break;
						case "ImportFromDate": this.str.ImportFromDate = (string)value; break;
						case "IsNew": this.str.IsNew = (string)value; break;
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
						case "IsImport":

							if (value == null || value is System.Boolean)
								this.IsImport = (System.Boolean?)value;
							break;
						case "ImportFromDate":

							if (value == null || value is System.DateTime)
								this.ImportFromDate = (System.DateTime?)value;
							break;
						case "IsNew":

							if (value == null || value is System.Boolean)
								this.IsNew = (System.Boolean?)value;
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
		/// Maps to ItemTariffRequest2.TariffRequestNo
		/// </summary>
		virtual public System.String TariffRequestNo
		{
			get
			{
				return base.GetSystemString(ItemTariffRequest2Metadata.ColumnNames.TariffRequestNo);
			}

			set
			{
				base.SetSystemString(ItemTariffRequest2Metadata.ColumnNames.TariffRequestNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequest2.TariffRequestDate
		/// </summary>
		virtual public System.DateTime? TariffRequestDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequest2Metadata.ColumnNames.TariffRequestDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTariffRequest2Metadata.ColumnNames.TariffRequestDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequest2.SRTariffType
		/// </summary>
		virtual public System.String SRTariffType
		{
			get
			{
				return base.GetSystemString(ItemTariffRequest2Metadata.ColumnNames.SRTariffType);
			}

			set
			{
				base.SetSystemString(ItemTariffRequest2Metadata.ColumnNames.SRTariffType, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequest2.SRItemType
		/// </summary>
		virtual public System.String SRItemType
		{
			get
			{
				return base.GetSystemString(ItemTariffRequest2Metadata.ColumnNames.SRItemType);
			}

			set
			{
				base.SetSystemString(ItemTariffRequest2Metadata.ColumnNames.SRItemType, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequest2.StartingDate
		/// </summary>
		virtual public System.DateTime? StartingDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequest2Metadata.ColumnNames.StartingDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTariffRequest2Metadata.ColumnNames.StartingDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequest2.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ItemTariffRequest2Metadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(ItemTariffRequest2Metadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequest2.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffRequest2Metadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(ItemTariffRequest2Metadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequest2.VoidDate
		/// </summary>
		virtual public System.DateTime? VoidDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequest2Metadata.ColumnNames.VoidDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTariffRequest2Metadata.ColumnNames.VoidDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequest2.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequest2Metadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(ItemTariffRequest2Metadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequest2.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffRequest2Metadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(ItemTariffRequest2Metadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequest2.ApprovedDate
		/// </summary>
		virtual public System.DateTime? ApprovedDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequest2Metadata.ColumnNames.ApprovedDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTariffRequest2Metadata.ColumnNames.ApprovedDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequest2.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequest2Metadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(ItemTariffRequest2Metadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequest2.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequest2Metadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemTariffRequest2Metadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequest2.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequest2Metadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemTariffRequest2Metadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequest2.IsImport
		/// </summary>
		virtual public System.Boolean? IsImport
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffRequest2Metadata.ColumnNames.IsImport);
			}

			set
			{
				base.SetSystemBoolean(ItemTariffRequest2Metadata.ColumnNames.IsImport, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequest2.ItemGroupID
		/// </summary>
		virtual public System.String ItemGroupID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequest2Metadata.ColumnNames.ItemGroupID);
			}

			set
			{
				base.SetSystemString(ItemTariffRequest2Metadata.ColumnNames.ItemGroupID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequest2.ImportFromDate
		/// </summary>
		virtual public System.DateTime? ImportFromDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequest2Metadata.ColumnNames.ImportFromDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTariffRequest2Metadata.ColumnNames.ImportFromDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequest2.IsNew
		/// </summary>
		virtual public System.Boolean? IsNew
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffRequest2Metadata.ColumnNames.IsNew);
			}

			set
			{
				base.SetSystemBoolean(ItemTariffRequest2Metadata.ColumnNames.IsNew, value);
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
			public esStrings(esItemTariffRequest2 entity)
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
			public System.String SRTariffType
			{
				get
				{
					System.String data = entity.SRTariffType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTariffType = null;
					else entity.SRTariffType = Convert.ToString(value);
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
			public System.String IsImport
			{
				get
				{
					System.Boolean? data = entity.IsImport;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsImport = null;
					else entity.IsImport = Convert.ToBoolean(value);
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
			public System.String ImportFromDate
			{
				get
				{
					System.DateTime? data = entity.ImportFromDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ImportFromDate = null;
					else entity.ImportFromDate = Convert.ToDateTime(value);
				}
			}
			public System.String IsNew
			{
				get
				{
					System.Boolean? data = entity.IsNew;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNew = null;
					else entity.IsNew = Convert.ToBoolean(value);
				}
			}
			private esItemTariffRequest2 entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemTariffRequest2Query query)
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
				throw new Exception("esItemTariffRequest2 can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemTariffRequest2 : esItemTariffRequest2
	{
	}

	[Serializable]
	abstract public class esItemTariffRequest2Query : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemTariffRequest2Metadata.Meta();
			}
		}

		public esQueryItem TariffRequestNo
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2Metadata.ColumnNames.TariffRequestNo, esSystemType.String);
			}
		}

		public esQueryItem TariffRequestDate
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2Metadata.ColumnNames.TariffRequestDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SRTariffType
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2Metadata.ColumnNames.SRTariffType, esSystemType.String);
			}
		}

		public esQueryItem SRItemType
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2Metadata.ColumnNames.SRItemType, esSystemType.String);
			}
		}

		public esQueryItem StartingDate
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2Metadata.ColumnNames.StartingDate, esSystemType.DateTime);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2Metadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2Metadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDate
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2Metadata.ColumnNames.VoidDate, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2Metadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2Metadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDate
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2Metadata.ColumnNames.ApprovedDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2Metadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsImport
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2Metadata.ColumnNames.IsImport, esSystemType.Boolean);
			}
		}

		public esQueryItem ItemGroupID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2Metadata.ColumnNames.ItemGroupID, esSystemType.String);
			}
		}

		public esQueryItem ImportFromDate
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2Metadata.ColumnNames.ImportFromDate, esSystemType.DateTime);
			}
		}

		public esQueryItem IsNew
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2Metadata.ColumnNames.IsNew, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemTariffRequest2Collection")]
	public partial class ItemTariffRequest2Collection : esItemTariffRequest2Collection, IEnumerable<ItemTariffRequest2>
	{
		public ItemTariffRequest2Collection()
		{

		}

		public static implicit operator List<ItemTariffRequest2>(ItemTariffRequest2Collection coll)
		{
			List<ItemTariffRequest2> list = new List<ItemTariffRequest2>();

			foreach (ItemTariffRequest2 emp in coll)
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
				return ItemTariffRequest2Metadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTariffRequest2Query();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemTariffRequest2(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemTariffRequest2();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemTariffRequest2Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTariffRequest2Query();
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
		public bool Load(ItemTariffRequest2Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemTariffRequest2 AddNew()
		{
			ItemTariffRequest2 entity = base.AddNewEntity() as ItemTariffRequest2;

			return entity;
		}
		public ItemTariffRequest2 FindByPrimaryKey(String tariffRequestNo)
		{
			return base.FindByPrimaryKey(tariffRequestNo) as ItemTariffRequest2;
		}

		#region IEnumerable< ItemTariffRequest2> Members

		IEnumerator<ItemTariffRequest2> IEnumerable<ItemTariffRequest2>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemTariffRequest2;
			}
		}

		#endregion

		private ItemTariffRequest2Query query;
	}


	/// <summary>
	/// Encapsulates the 'ItemTariffRequest2' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemTariffRequest2 ({TariffRequestNo})")]
	[Serializable]
	public partial class ItemTariffRequest2 : esItemTariffRequest2
	{
		public ItemTariffRequest2()
		{
		}

		public ItemTariffRequest2(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemTariffRequest2Metadata.Meta();
			}
		}

		override protected esItemTariffRequest2Query GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTariffRequest2Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemTariffRequest2Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTariffRequest2Query();
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
		public bool Load(ItemTariffRequest2Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemTariffRequest2Query query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemTariffRequest2Query : esItemTariffRequest2Query
	{
		public ItemTariffRequest2Query()
		{

		}

		public ItemTariffRequest2Query(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemTariffRequest2Query";
		}
	}

	[Serializable]
	public partial class ItemTariffRequest2Metadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemTariffRequest2Metadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemTariffRequest2Metadata.ColumnNames.TariffRequestNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequest2Metadata.PropertyNames.TariffRequestNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequest2Metadata.ColumnNames.TariffRequestDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequest2Metadata.PropertyNames.TariffRequestDate;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequest2Metadata.ColumnNames.SRTariffType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequest2Metadata.PropertyNames.SRTariffType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequest2Metadata.ColumnNames.SRItemType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequest2Metadata.PropertyNames.SRItemType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequest2Metadata.ColumnNames.StartingDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequest2Metadata.PropertyNames.StartingDate;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequest2Metadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequest2Metadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequest2Metadata.ColumnNames.IsVoid, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffRequest2Metadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequest2Metadata.ColumnNames.VoidDate, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequest2Metadata.PropertyNames.VoidDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequest2Metadata.ColumnNames.VoidByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequest2Metadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequest2Metadata.ColumnNames.IsApproved, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffRequest2Metadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequest2Metadata.ColumnNames.ApprovedDate, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequest2Metadata.PropertyNames.ApprovedDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequest2Metadata.ColumnNames.ApprovedByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequest2Metadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequest2Metadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequest2Metadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequest2Metadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequest2Metadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequest2Metadata.ColumnNames.IsImport, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffRequest2Metadata.PropertyNames.IsImport;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequest2Metadata.ColumnNames.ItemGroupID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequest2Metadata.PropertyNames.ItemGroupID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequest2Metadata.ColumnNames.ImportFromDate, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequest2Metadata.PropertyNames.ImportFromDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequest2Metadata.ColumnNames.IsNew, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffRequest2Metadata.PropertyNames.IsNew;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemTariffRequest2Metadata Meta()
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
			public const string SRTariffType = "SRTariffType";
			public const string SRItemType = "SRItemType";
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
			public const string IsImport = "IsImport";
			public const string ItemGroupID = "ItemGroupID";
			public const string ImportFromDate = "ImportFromDate";
			public const string IsNew = "IsNew";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TariffRequestNo = "TariffRequestNo";
			public const string TariffRequestDate = "TariffRequestDate";
			public const string SRTariffType = "SRTariffType";
			public const string SRItemType = "SRItemType";
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
			public const string IsImport = "IsImport";
			public const string ItemGroupID = "ItemGroupID";
			public const string ImportFromDate = "ImportFromDate";
			public const string IsNew = "IsNew";
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
			lock (typeof(ItemTariffRequest2Metadata))
			{
				if (ItemTariffRequest2Metadata.mapDelegates == null)
				{
					ItemTariffRequest2Metadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemTariffRequest2Metadata.meta == null)
				{
					ItemTariffRequest2Metadata.meta = new ItemTariffRequest2Metadata();
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
				meta.AddTypeMap("SRTariffType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemType", new esTypeMap("varchar", "System.String"));
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
				meta.AddTypeMap("IsImport", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ItemGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ImportFromDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsNew", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "ItemTariffRequest2";
				meta.Destination = "ItemTariffRequest2";
				meta.spInsert = "proc_ItemTariffRequest2Insert";
				meta.spUpdate = "proc_ItemTariffRequest2Update";
				meta.spDelete = "proc_ItemTariffRequest2Delete";
				meta.spLoadAll = "proc_ItemTariffRequest2LoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemTariffRequest2LoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemTariffRequest2Metadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
