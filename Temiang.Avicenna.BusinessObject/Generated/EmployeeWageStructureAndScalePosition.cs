/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/15/2022 1:58:14 PM
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
	abstract public class esEmployeeWageStructureAndScalePositionCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeWageStructureAndScalePositionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeWageStructureAndScalePositionCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeWageStructureAndScalePositionQuery query)
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
			this.InitQuery(query as esEmployeeWageStructureAndScalePositionQuery);
		}
		#endregion

		virtual public EmployeeWageStructureAndScalePosition DetachEntity(EmployeeWageStructureAndScalePosition entity)
		{
			return base.DetachEntity(entity) as EmployeeWageStructureAndScalePosition;
		}

		virtual public EmployeeWageStructureAndScalePosition AttachEntity(EmployeeWageStructureAndScalePosition entity)
		{
			return base.AttachEntity(entity) as EmployeeWageStructureAndScalePosition;
		}

		virtual public void Combine(EmployeeWageStructureAndScalePositionCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeWageStructureAndScalePosition this[int index]
		{
			get
			{
				return base[index] as EmployeeWageStructureAndScalePosition;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeWageStructureAndScalePosition);
		}
	}

	[Serializable]
	abstract public class esEmployeeWageStructureAndScalePosition : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeWageStructureAndScalePositionQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeWageStructureAndScalePosition()
		{
		}

		public esEmployeeWageStructureAndScalePosition(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 wageStructureAndScalePositionID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(wageStructureAndScalePositionID);
			else
				return LoadByPrimaryKeyStoredProcedure(wageStructureAndScalePositionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 wageStructureAndScalePositionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(wageStructureAndScalePositionID);
			else
				return LoadByPrimaryKeyStoredProcedure(wageStructureAndScalePositionID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 wageStructureAndScalePositionID)
		{
			esEmployeeWageStructureAndScalePositionQuery query = this.GetDynamicQuery();
			query.Where(query.WageStructureAndScalePositionID == wageStructureAndScalePositionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 wageStructureAndScalePositionID)
		{
			esParameters parms = new esParameters();
			parms.Add("WageStructureAndScalePositionID", wageStructureAndScalePositionID);
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
						case "WageStructureAndScalePositionID": this.str.WageStructureAndScalePositionID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "ValidFrom": this.str.ValidFrom = (string)value; break;
						case "SREmployeeWorkGroup": this.str.SREmployeeWorkGroup = (string)value; break;
						case "SREmployeeWorkSubGroup": this.str.SREmployeeWorkSubGroup = (string)value; break;
						case "SREmployeeJobPosition": this.str.SREmployeeJobPosition = (string)value; break;
						case "BasePoint": this.str.BasePoint = (string)value; break;
						case "Points": this.str.Points = (string)value; break;
						case "PositionGradeID": this.str.PositionGradeID = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "WageStructureAndScalePositionID":

							if (value == null || value is System.Int32)
								this.WageStructureAndScalePositionID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "ValidFrom":

							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						case "BasePoint":

							if (value == null || value is System.Decimal)
								this.BasePoint = (System.Decimal?)value;
							break;
						case "Points":

							if (value == null || value is System.Decimal)
								this.Points = (System.Decimal?)value;
							break;
						case "PositionGradeID":

							if (value == null || value is System.Int32)
								this.PositionGradeID = (System.Int32?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDateTime":

							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
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
		/// Maps to EmployeeWageStructureAndScalePosition.WageStructureAndScalePositionID
		/// </summary>
		virtual public System.Int32? WageStructureAndScalePositionID
		{
			get
			{
				return base.GetSystemInt32(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.WageStructureAndScalePositionID);
			}

			set
			{
				base.SetSystemInt32(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.WageStructureAndScalePositionID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePosition.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePosition.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.ValidFrom);
			}

			set
			{
				base.SetSystemDateTime(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.ValidFrom, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePosition.SREmployeeWorkGroup
		/// </summary>
		virtual public System.String SREmployeeWorkGroup
		{
			get
			{
				return base.GetSystemString(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.SREmployeeWorkGroup);
			}

			set
			{
				base.SetSystemString(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.SREmployeeWorkGroup, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePosition.SREmployeeWorkSubGroup
		/// </summary>
		virtual public System.String SREmployeeWorkSubGroup
		{
			get
			{
				return base.GetSystemString(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.SREmployeeWorkSubGroup);
			}

			set
			{
				base.SetSystemString(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.SREmployeeWorkSubGroup, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePosition.SREmployeeJobPosition
		/// </summary>
		virtual public System.String SREmployeeJobPosition
		{
			get
			{
				return base.GetSystemString(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.SREmployeeJobPosition);
			}

			set
			{
				base.SetSystemString(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.SREmployeeJobPosition, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePosition.BasePoint
		/// </summary>
		virtual public System.Decimal? BasePoint
		{
			get
			{
				return base.GetSystemDecimal(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.BasePoint);
			}

			set
			{
				base.SetSystemDecimal(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.BasePoint, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePosition.Points
		/// </summary>
		virtual public System.Decimal? Points
		{
			get
			{
				return base.GetSystemDecimal(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.Points);
			}

			set
			{
				base.SetSystemDecimal(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.Points, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePosition.PositionGradeID
		/// </summary>
		virtual public System.Int32? PositionGradeID
		{
			get
			{
				return base.GetSystemInt32(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.PositionGradeID);
			}

			set
			{
				base.SetSystemInt32(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.PositionGradeID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePosition.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePosition.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePosition.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePosition.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePosition.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePosition.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePosition.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePosition.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeWageStructureAndScalePosition entity)
			{
				this.entity = entity;
			}
			public System.String WageStructureAndScalePositionID
			{
				get
				{
					System.Int32? data = entity.WageStructureAndScalePositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WageStructureAndScalePositionID = null;
					else entity.WageStructureAndScalePositionID = Convert.ToInt32(value);
				}
			}
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
				}
			}
			public System.String ValidFrom
			{
				get
				{
					System.DateTime? data = entity.ValidFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidFrom = null;
					else entity.ValidFrom = Convert.ToDateTime(value);
				}
			}
			public System.String SREmployeeWorkGroup
			{
				get
				{
					System.String data = entity.SREmployeeWorkGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeWorkGroup = null;
					else entity.SREmployeeWorkGroup = Convert.ToString(value);
				}
			}
			public System.String SREmployeeWorkSubGroup
			{
				get
				{
					System.String data = entity.SREmployeeWorkSubGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeWorkSubGroup = null;
					else entity.SREmployeeWorkSubGroup = Convert.ToString(value);
				}
			}
			public System.String SREmployeeJobPosition
			{
				get
				{
					System.String data = entity.SREmployeeJobPosition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeJobPosition = null;
					else entity.SREmployeeJobPosition = Convert.ToString(value);
				}
			}
			public System.String BasePoint
			{
				get
				{
					System.Decimal? data = entity.BasePoint;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BasePoint = null;
					else entity.BasePoint = Convert.ToDecimal(value);
				}
			}
			public System.String Points
			{
				get
				{
					System.Decimal? data = entity.Points;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Points = null;
					else entity.Points = Convert.ToDecimal(value);
				}
			}
			public System.String PositionGradeID
			{
				get
				{
					System.Int32? data = entity.PositionGradeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionGradeID = null;
					else entity.PositionGradeID = Convert.ToInt32(value);
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
			private esEmployeeWageStructureAndScalePosition entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeWageStructureAndScalePositionQuery query)
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
				throw new Exception("esEmployeeWageStructureAndScalePosition can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeWageStructureAndScalePosition : esEmployeeWageStructureAndScalePosition
	{
	}

	[Serializable]
	abstract public class esEmployeeWageStructureAndScalePositionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeWageStructureAndScalePositionMetadata.Meta();
			}
		}

		public esQueryItem WageStructureAndScalePositionID
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionMetadata.ColumnNames.WageStructureAndScalePositionID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem SREmployeeWorkGroup
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionMetadata.ColumnNames.SREmployeeWorkGroup, esSystemType.String);
			}
		}

		public esQueryItem SREmployeeWorkSubGroup
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionMetadata.ColumnNames.SREmployeeWorkSubGroup, esSystemType.String);
			}
		}

		public esQueryItem SREmployeeJobPosition
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionMetadata.ColumnNames.SREmployeeJobPosition, esSystemType.String);
			}
		}

		public esQueryItem BasePoint
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionMetadata.ColumnNames.BasePoint, esSystemType.Decimal);
			}
		}

		public esQueryItem Points
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionMetadata.ColumnNames.Points, esSystemType.Decimal);
			}
		}

		public esQueryItem PositionGradeID
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionMetadata.ColumnNames.PositionGradeID, esSystemType.Int32);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeWageStructureAndScalePositionCollection")]
	public partial class EmployeeWageStructureAndScalePositionCollection : esEmployeeWageStructureAndScalePositionCollection, IEnumerable<EmployeeWageStructureAndScalePosition>
	{
		public EmployeeWageStructureAndScalePositionCollection()
		{

		}

		public static implicit operator List<EmployeeWageStructureAndScalePosition>(EmployeeWageStructureAndScalePositionCollection coll)
		{
			List<EmployeeWageStructureAndScalePosition> list = new List<EmployeeWageStructureAndScalePosition>();

			foreach (EmployeeWageStructureAndScalePosition emp in coll)
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
				return EmployeeWageStructureAndScalePositionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeWageStructureAndScalePositionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeWageStructureAndScalePosition(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeWageStructureAndScalePosition();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeWageStructureAndScalePositionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeWageStructureAndScalePositionQuery();
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
		public bool Load(EmployeeWageStructureAndScalePositionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeWageStructureAndScalePosition AddNew()
		{
			EmployeeWageStructureAndScalePosition entity = base.AddNewEntity() as EmployeeWageStructureAndScalePosition;

			return entity;
		}
		public EmployeeWageStructureAndScalePosition FindByPrimaryKey(Int32 wageStructureAndScalePositionID)
		{
			return base.FindByPrimaryKey(wageStructureAndScalePositionID) as EmployeeWageStructureAndScalePosition;
		}

		#region IEnumerable< EmployeeWageStructureAndScalePosition> Members

		IEnumerator<EmployeeWageStructureAndScalePosition> IEnumerable<EmployeeWageStructureAndScalePosition>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeWageStructureAndScalePosition;
			}
		}

		#endregion

		private EmployeeWageStructureAndScalePositionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeWageStructureAndScalePosition' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeWageStructureAndScalePosition ({WageStructureAndScalePositionID})")]
	[Serializable]
	public partial class EmployeeWageStructureAndScalePosition : esEmployeeWageStructureAndScalePosition
	{
		public EmployeeWageStructureAndScalePosition()
		{
		}

		public EmployeeWageStructureAndScalePosition(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeWageStructureAndScalePositionMetadata.Meta();
			}
		}

		override protected esEmployeeWageStructureAndScalePositionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeWageStructureAndScalePositionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeWageStructureAndScalePositionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeWageStructureAndScalePositionQuery();
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
		public bool Load(EmployeeWageStructureAndScalePositionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeWageStructureAndScalePositionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeWageStructureAndScalePositionQuery : esEmployeeWageStructureAndScalePositionQuery
	{
		public EmployeeWageStructureAndScalePositionQuery()
		{

		}

		public EmployeeWageStructureAndScalePositionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeWageStructureAndScalePositionQuery";
		}
	}

	[Serializable]
	public partial class EmployeeWageStructureAndScalePositionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeWageStructureAndScalePositionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.WageStructureAndScalePositionID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeWageStructureAndScalePositionMetadata.PropertyNames.WageStructureAndScalePositionID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeWageStructureAndScalePositionMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.ValidFrom, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeWageStructureAndScalePositionMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.SREmployeeWorkGroup, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWageStructureAndScalePositionMetadata.PropertyNames.SREmployeeWorkGroup;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.SREmployeeWorkSubGroup, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWageStructureAndScalePositionMetadata.PropertyNames.SREmployeeWorkSubGroup;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.SREmployeeJobPosition, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWageStructureAndScalePositionMetadata.PropertyNames.SREmployeeJobPosition;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.BasePoint, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeWageStructureAndScalePositionMetadata.PropertyNames.BasePoint;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.Points, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeWageStructureAndScalePositionMetadata.PropertyNames.Points;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.PositionGradeID, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeWageStructureAndScalePositionMetadata.PropertyNames.PositionGradeID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.IsApproved, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeWageStructureAndScalePositionMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.ApprovedDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeWageStructureAndScalePositionMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.ApprovedByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWageStructureAndScalePositionMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.IsVoid, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeWageStructureAndScalePositionMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.VoidDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeWageStructureAndScalePositionMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.VoidByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWageStructureAndScalePositionMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.LastUpdateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeWageStructureAndScalePositionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.LastUpdateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWageStructureAndScalePositionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeWageStructureAndScalePositionMetadata Meta()
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
			public const string WageStructureAndScalePositionID = "WageStructureAndScalePositionID";
			public const string PersonID = "PersonID";
			public const string ValidFrom = "ValidFrom";
			public const string SREmployeeWorkGroup = "SREmployeeWorkGroup";
			public const string SREmployeeWorkSubGroup = "SREmployeeWorkSubGroup";
			public const string SREmployeeJobPosition = "SREmployeeJobPosition";
			public const string BasePoint = "BasePoint";
			public const string Points = "Points";
			public const string PositionGradeID = "PositionGradeID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string WageStructureAndScalePositionID = "WageStructureAndScalePositionID";
			public const string PersonID = "PersonID";
			public const string ValidFrom = "ValidFrom";
			public const string SREmployeeWorkGroup = "SREmployeeWorkGroup";
			public const string SREmployeeWorkSubGroup = "SREmployeeWorkSubGroup";
			public const string SREmployeeJobPosition = "SREmployeeJobPosition";
			public const string BasePoint = "BasePoint";
			public const string Points = "Points";
			public const string PositionGradeID = "PositionGradeID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
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
			lock (typeof(EmployeeWageStructureAndScalePositionMetadata))
			{
				if (EmployeeWageStructureAndScalePositionMetadata.mapDelegates == null)
				{
					EmployeeWageStructureAndScalePositionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeWageStructureAndScalePositionMetadata.meta == null)
				{
					EmployeeWageStructureAndScalePositionMetadata.meta = new EmployeeWageStructureAndScalePositionMetadata();
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

				meta.AddTypeMap("WageStructureAndScalePositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SREmployeeWorkGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeWorkSubGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeJobPosition", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BasePoint", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Points", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PositionGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeWageStructureAndScalePosition";
				meta.Destination = "EmployeeWageStructureAndScalePosition";
				meta.spInsert = "proc_EmployeeWageStructureAndScalePositionInsert";
				meta.spUpdate = "proc_EmployeeWageStructureAndScalePositionUpdate";
				meta.spDelete = "proc_EmployeeWageStructureAndScalePositionDelete";
				meta.spLoadAll = "proc_EmployeeWageStructureAndScalePositionLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeWageStructureAndScalePositionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeWageStructureAndScalePositionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
