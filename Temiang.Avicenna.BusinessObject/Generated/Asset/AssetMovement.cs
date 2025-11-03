/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/9/2023 1:23:06 PM
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
	abstract public class esAssetMovementCollection : esEntityCollectionWAuditLog
	{
		public esAssetMovementCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AssetMovementCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetMovementQuery query)
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
			this.InitQuery(query as esAssetMovementQuery);
		}
		#endregion

		virtual public AssetMovement DetachEntity(AssetMovement entity)
		{
			return base.DetachEntity(entity) as AssetMovement;
		}

		virtual public AssetMovement AttachEntity(AssetMovement entity)
		{
			return base.AttachEntity(entity) as AssetMovement;
		}

		virtual public void Combine(AssetMovementCollection collection)
		{
			base.Combine(collection);
		}

		new public AssetMovement this[int index]
		{
			get
			{
				return base[index] as AssetMovement;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetMovement);
		}
	}

	[Serializable]
	abstract public class esAssetMovement : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetMovementQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetMovement()
		{
		}

		public esAssetMovement(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String assetMovementNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetMovementNo);
			else
				return LoadByPrimaryKeyStoredProcedure(assetMovementNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String assetMovementNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetMovementNo);
			else
				return LoadByPrimaryKeyStoredProcedure(assetMovementNo);
		}

		private bool LoadByPrimaryKeyDynamic(String assetMovementNo)
		{
			esAssetMovementQuery query = this.GetDynamicQuery();
			query.Where(query.AssetMovementNo == assetMovementNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String assetMovementNo)
		{
			esParameters parms = new esParameters();
			parms.Add("AssetMovementNo", assetMovementNo);
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
						case "AssetMovementNo": this.str.AssetMovementNo = (string)value; break;
						case "MovementDate": this.str.MovementDate = (string)value; break;
						case "AssetID": this.str.AssetID = (string)value; break;
						case "FromDepartmentID": this.str.FromDepartmentID = (string)value; break;
						case "FromServiceUnitID": this.str.FromServiceUnitID = (string)value; break;
						case "FromAssetLocationID": this.str.FromAssetLocationID = (string)value; break;
						case "ToDepartmentID": this.str.ToDepartmentID = (string)value; break;
						case "ToServiceUnitID": this.str.ToServiceUnitID = (string)value; break;
						case "ToAssetLocationID": this.str.ToAssetLocationID = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "IsPosted": this.str.IsPosted = (string)value; break;
						case "IsDeleted": this.str.IsDeleted = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "CurrentValue": this.str.CurrentValue = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "MovementDate":

							if (value == null || value is System.DateTime)
								this.MovementDate = (System.DateTime?)value;
							break;
						case "IsPosted":

							if (value == null || value is System.Boolean)
								this.IsPosted = (System.Boolean?)value;
							break;
						case "IsDeleted":

							if (value == null || value is System.Boolean)
								this.IsDeleted = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDateTime":

							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
							break;
						case "CurrentValue":

							if (value == null || value is System.Decimal)
								this.CurrentValue = (System.Decimal?)value;
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
		/// Maps to AssetMovement.AssetMovementNo
		/// </summary>
		virtual public System.String AssetMovementNo
		{
			get
			{
				return base.GetSystemString(AssetMovementMetadata.ColumnNames.AssetMovementNo);
			}

			set
			{
				base.SetSystemString(AssetMovementMetadata.ColumnNames.AssetMovementNo, value);
			}
		}
		/// <summary>
		/// Maps to AssetMovement.MovementDate
		/// </summary>
		virtual public System.DateTime? MovementDate
		{
			get
			{
				return base.GetSystemDateTime(AssetMovementMetadata.ColumnNames.MovementDate);
			}

			set
			{
				base.SetSystemDateTime(AssetMovementMetadata.ColumnNames.MovementDate, value);
			}
		}
		/// <summary>
		/// Maps to AssetMovement.AssetID
		/// </summary>
		virtual public System.String AssetID
		{
			get
			{
				return base.GetSystemString(AssetMovementMetadata.ColumnNames.AssetID);
			}

			set
			{
				base.SetSystemString(AssetMovementMetadata.ColumnNames.AssetID, value);
			}
		}
		/// <summary>
		/// Maps to AssetMovement.FromDepartmentID
		/// </summary>
		virtual public System.String FromDepartmentID
		{
			get
			{
				return base.GetSystemString(AssetMovementMetadata.ColumnNames.FromDepartmentID);
			}

			set
			{
				base.SetSystemString(AssetMovementMetadata.ColumnNames.FromDepartmentID, value);
			}
		}
		/// <summary>
		/// Maps to AssetMovement.FromServiceUnitID
		/// </summary>
		virtual public System.String FromServiceUnitID
		{
			get
			{
				return base.GetSystemString(AssetMovementMetadata.ColumnNames.FromServiceUnitID);
			}

			set
			{
				base.SetSystemString(AssetMovementMetadata.ColumnNames.FromServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to AssetMovement.FromAssetLocationID
		/// </summary>
		virtual public System.String FromAssetLocationID
		{
			get
			{
				return base.GetSystemString(AssetMovementMetadata.ColumnNames.FromAssetLocationID);
			}

			set
			{
				base.SetSystemString(AssetMovementMetadata.ColumnNames.FromAssetLocationID, value);
			}
		}
		/// <summary>
		/// Maps to AssetMovement.ToDepartmentID
		/// </summary>
		virtual public System.String ToDepartmentID
		{
			get
			{
				return base.GetSystemString(AssetMovementMetadata.ColumnNames.ToDepartmentID);
			}

			set
			{
				base.SetSystemString(AssetMovementMetadata.ColumnNames.ToDepartmentID, value);
			}
		}
		/// <summary>
		/// Maps to AssetMovement.ToServiceUnitID
		/// </summary>
		virtual public System.String ToServiceUnitID
		{
			get
			{
				return base.GetSystemString(AssetMovementMetadata.ColumnNames.ToServiceUnitID);
			}

			set
			{
				base.SetSystemString(AssetMovementMetadata.ColumnNames.ToServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to AssetMovement.ToAssetLocationID
		/// </summary>
		virtual public System.String ToAssetLocationID
		{
			get
			{
				return base.GetSystemString(AssetMovementMetadata.ColumnNames.ToAssetLocationID);
			}

			set
			{
				base.SetSystemString(AssetMovementMetadata.ColumnNames.ToAssetLocationID, value);
			}
		}
		/// <summary>
		/// Maps to AssetMovement.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(AssetMovementMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(AssetMovementMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to AssetMovement.IsPosted
		/// </summary>
		virtual public System.Boolean? IsPosted
		{
			get
			{
				return base.GetSystemBoolean(AssetMovementMetadata.ColumnNames.IsPosted);
			}

			set
			{
				base.SetSystemBoolean(AssetMovementMetadata.ColumnNames.IsPosted, value);
			}
		}
		/// <summary>
		/// Maps to AssetMovement.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(AssetMovementMetadata.ColumnNames.IsDeleted);
			}

			set
			{
				base.SetSystemBoolean(AssetMovementMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to AssetMovement.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetMovementMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AssetMovementMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AssetMovement.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AssetMovementMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AssetMovementMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AssetMovement.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(AssetMovementMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(AssetMovementMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to AssetMovement.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetMovementMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(AssetMovementMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AssetMovement.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(AssetMovementMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(AssetMovementMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AssetMovement.CurrentValue
		/// </summary>
		virtual public System.Decimal? CurrentValue
		{
			get
			{
				return base.GetSystemDecimal(AssetMovementMetadata.ColumnNames.CurrentValue);
			}

			set
			{
				base.SetSystemDecimal(AssetMovementMetadata.ColumnNames.CurrentValue, value);
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
			public esStrings(esAssetMovement entity)
			{
				this.entity = entity;
			}
			public System.String AssetMovementNo
			{
				get
				{
					System.String data = entity.AssetMovementNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetMovementNo = null;
					else entity.AssetMovementNo = Convert.ToString(value);
				}
			}
			public System.String MovementDate
			{
				get
				{
					System.DateTime? data = entity.MovementDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MovementDate = null;
					else entity.MovementDate = Convert.ToDateTime(value);
				}
			}
			public System.String AssetID
			{
				get
				{
					System.String data = entity.AssetID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetID = null;
					else entity.AssetID = Convert.ToString(value);
				}
			}
			public System.String FromDepartmentID
			{
				get
				{
					System.String data = entity.FromDepartmentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromDepartmentID = null;
					else entity.FromDepartmentID = Convert.ToString(value);
				}
			}
			public System.String FromServiceUnitID
			{
				get
				{
					System.String data = entity.FromServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromServiceUnitID = null;
					else entity.FromServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String FromAssetLocationID
			{
				get
				{
					System.String data = entity.FromAssetLocationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromAssetLocationID = null;
					else entity.FromAssetLocationID = Convert.ToString(value);
				}
			}
			public System.String ToDepartmentID
			{
				get
				{
					System.String data = entity.ToDepartmentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToDepartmentID = null;
					else entity.ToDepartmentID = Convert.ToString(value);
				}
			}
			public System.String ToServiceUnitID
			{
				get
				{
					System.String data = entity.ToServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToServiceUnitID = null;
					else entity.ToServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String ToAssetLocationID
			{
				get
				{
					System.String data = entity.ToAssetLocationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToAssetLocationID = null;
					else entity.ToAssetLocationID = Convert.ToString(value);
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
			public System.String IsPosted
			{
				get
				{
					System.Boolean? data = entity.IsPosted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPosted = null;
					else entity.IsPosted = Convert.ToBoolean(value);
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
			public System.String ApprovedDateTime
			{
				get
				{
					System.DateTime? data = entity.ApprovedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
					else entity.ApprovedDateTime = Convert.ToDateTime(value);
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
			public System.String CurrentValue
			{
				get
				{
					System.Decimal? data = entity.CurrentValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CurrentValue = null;
					else entity.CurrentValue = Convert.ToDecimal(value);
				}
			}
			private esAssetMovement entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetMovementQuery query)
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
				throw new Exception("esAssetMovement can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AssetMovement : esAssetMovement
	{
	}

	[Serializable]
	abstract public class esAssetMovementQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AssetMovementMetadata.Meta();
			}
		}

		public esQueryItem AssetMovementNo
		{
			get
			{
				return new esQueryItem(this, AssetMovementMetadata.ColumnNames.AssetMovementNo, esSystemType.String);
			}
		}

		public esQueryItem MovementDate
		{
			get
			{
				return new esQueryItem(this, AssetMovementMetadata.ColumnNames.MovementDate, esSystemType.DateTime);
			}
		}

		public esQueryItem AssetID
		{
			get
			{
				return new esQueryItem(this, AssetMovementMetadata.ColumnNames.AssetID, esSystemType.String);
			}
		}

		public esQueryItem FromDepartmentID
		{
			get
			{
				return new esQueryItem(this, AssetMovementMetadata.ColumnNames.FromDepartmentID, esSystemType.String);
			}
		}

		public esQueryItem FromServiceUnitID
		{
			get
			{
				return new esQueryItem(this, AssetMovementMetadata.ColumnNames.FromServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem FromAssetLocationID
		{
			get
			{
				return new esQueryItem(this, AssetMovementMetadata.ColumnNames.FromAssetLocationID, esSystemType.String);
			}
		}

		public esQueryItem ToDepartmentID
		{
			get
			{
				return new esQueryItem(this, AssetMovementMetadata.ColumnNames.ToDepartmentID, esSystemType.String);
			}
		}

		public esQueryItem ToServiceUnitID
		{
			get
			{
				return new esQueryItem(this, AssetMovementMetadata.ColumnNames.ToServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem ToAssetLocationID
		{
			get
			{
				return new esQueryItem(this, AssetMovementMetadata.ColumnNames.ToAssetLocationID, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, AssetMovementMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem IsPosted
		{
			get
			{
				return new esQueryItem(this, AssetMovementMetadata.ColumnNames.IsPosted, esSystemType.Boolean);
			}
		}

		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, AssetMovementMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetMovementMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AssetMovementMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, AssetMovementMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, AssetMovementMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, AssetMovementMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem CurrentValue
		{
			get
			{
				return new esQueryItem(this, AssetMovementMetadata.ColumnNames.CurrentValue, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetMovementCollection")]
	public partial class AssetMovementCollection : esAssetMovementCollection, IEnumerable<AssetMovement>
	{
		public AssetMovementCollection()
		{

		}

		public static implicit operator List<AssetMovement>(AssetMovementCollection coll)
		{
			List<AssetMovement> list = new List<AssetMovement>();

			foreach (AssetMovement emp in coll)
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
				return AssetMovementMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetMovementQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetMovement(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetMovement();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AssetMovementQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetMovementQuery();
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
		public bool Load(AssetMovementQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AssetMovement AddNew()
		{
			AssetMovement entity = base.AddNewEntity() as AssetMovement;

			return entity;
		}
		public AssetMovement FindByPrimaryKey(String assetMovementNo)
		{
			return base.FindByPrimaryKey(assetMovementNo) as AssetMovement;
		}

		#region IEnumerable< AssetMovement> Members

		IEnumerator<AssetMovement> IEnumerable<AssetMovement>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AssetMovement;
			}
		}

		#endregion

		private AssetMovementQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetMovement' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AssetMovement ({AssetMovementNo})")]
	[Serializable]
	public partial class AssetMovement : esAssetMovement
	{
		public AssetMovement()
		{
		}

		public AssetMovement(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetMovementMetadata.Meta();
			}
		}

		override protected esAssetMovementQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetMovementQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AssetMovementQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetMovementQuery();
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
		public bool Load(AssetMovementQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AssetMovementQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AssetMovementQuery : esAssetMovementQuery
	{
		public AssetMovementQuery()
		{

		}

		public AssetMovementQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AssetMovementQuery";
		}
	}

	[Serializable]
	public partial class AssetMovementMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetMovementMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetMovementMetadata.ColumnNames.AssetMovementNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMovementMetadata.PropertyNames.AssetMovementNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMovementMetadata.ColumnNames.MovementDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMovementMetadata.PropertyNames.MovementDate;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMovementMetadata.ColumnNames.AssetID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMovementMetadata.PropertyNames.AssetID;
			c.CharacterMaxLength = 30;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMovementMetadata.ColumnNames.FromDepartmentID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMovementMetadata.PropertyNames.FromDepartmentID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMovementMetadata.ColumnNames.FromServiceUnitID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMovementMetadata.PropertyNames.FromServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMovementMetadata.ColumnNames.FromAssetLocationID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMovementMetadata.PropertyNames.FromAssetLocationID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMovementMetadata.ColumnNames.ToDepartmentID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMovementMetadata.PropertyNames.ToDepartmentID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMovementMetadata.ColumnNames.ToServiceUnitID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMovementMetadata.PropertyNames.ToServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMovementMetadata.ColumnNames.ToAssetLocationID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMovementMetadata.PropertyNames.ToAssetLocationID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMovementMetadata.ColumnNames.Notes, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMovementMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMovementMetadata.ColumnNames.IsPosted, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetMovementMetadata.PropertyNames.IsPosted;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMovementMetadata.ColumnNames.IsDeleted, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetMovementMetadata.PropertyNames.IsDeleted;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMovementMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMovementMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMovementMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMovementMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMovementMetadata.ColumnNames.IsApproved, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetMovementMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMovementMetadata.ColumnNames.ApprovedDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMovementMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMovementMetadata.ColumnNames.ApprovedByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMovementMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMovementMetadata.ColumnNames.CurrentValue, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetMovementMetadata.PropertyNames.CurrentValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AssetMovementMetadata Meta()
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
			public const string AssetMovementNo = "AssetMovementNo";
			public const string MovementDate = "MovementDate";
			public const string AssetID = "AssetID";
			public const string FromDepartmentID = "FromDepartmentID";
			public const string FromServiceUnitID = "FromServiceUnitID";
			public const string FromAssetLocationID = "FromAssetLocationID";
			public const string ToDepartmentID = "ToDepartmentID";
			public const string ToServiceUnitID = "ToServiceUnitID";
			public const string ToAssetLocationID = "ToAssetLocationID";
			public const string Notes = "Notes";
			public const string IsPosted = "IsPosted";
			public const string IsDeleted = "IsDeleted";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string CurrentValue = "CurrentValue";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string AssetMovementNo = "AssetMovementNo";
			public const string MovementDate = "MovementDate";
			public const string AssetID = "AssetID";
			public const string FromDepartmentID = "FromDepartmentID";
			public const string FromServiceUnitID = "FromServiceUnitID";
			public const string FromAssetLocationID = "FromAssetLocationID";
			public const string ToDepartmentID = "ToDepartmentID";
			public const string ToServiceUnitID = "ToServiceUnitID";
			public const string ToAssetLocationID = "ToAssetLocationID";
			public const string Notes = "Notes";
			public const string IsPosted = "IsPosted";
			public const string IsDeleted = "IsDeleted";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string CurrentValue = "CurrentValue";
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
			lock (typeof(AssetMovementMetadata))
			{
				if (AssetMovementMetadata.mapDelegates == null)
				{
					AssetMovementMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AssetMovementMetadata.meta == null)
				{
					AssetMovementMetadata.meta = new AssetMovementMetadata();
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

				meta.AddTypeMap("AssetMovementNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MovementDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("AssetID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromDepartmentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromAssetLocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToDepartmentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToAssetLocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPosted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CurrentValue", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "AssetMovement";
				meta.Destination = "AssetMovement";
				meta.spInsert = "proc_AssetMovementInsert";
				meta.spUpdate = "proc_AssetMovementUpdate";
				meta.spDelete = "proc_AssetMovementDelete";
				meta.spLoadAll = "proc_AssetMovementLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetMovementLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetMovementMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
