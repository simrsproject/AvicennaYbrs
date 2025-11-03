/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/6/2021 11:46:08 AM
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
	abstract public class esCssdSterileItemsReturnedCollection : esEntityCollectionWAuditLog
	{
		public esCssdSterileItemsReturnedCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CssdSterileItemsReturnedCollection";
		}

		#region Query Logic
		protected void InitQuery(esCssdSterileItemsReturnedQuery query)
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
			this.InitQuery(query as esCssdSterileItemsReturnedQuery);
		}
		#endregion

		virtual public CssdSterileItemsReturned DetachEntity(CssdSterileItemsReturned entity)
		{
			return base.DetachEntity(entity) as CssdSterileItemsReturned;
		}

		virtual public CssdSterileItemsReturned AttachEntity(CssdSterileItemsReturned entity)
		{
			return base.AttachEntity(entity) as CssdSterileItemsReturned;
		}

		virtual public void Combine(CssdSterileItemsReturnedCollection collection)
		{
			base.Combine(collection);
		}

		new public CssdSterileItemsReturned this[int index]
		{
			get
			{
				return base[index] as CssdSterileItemsReturned;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CssdSterileItemsReturned);
		}
	}

	[Serializable]
	abstract public class esCssdSterileItemsReturned : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCssdSterileItemsReturnedQuery GetDynamicQuery()
		{
			return null;
		}

		public esCssdSterileItemsReturned()
		{
		}

		public esCssdSterileItemsReturned(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String returnNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(returnNo);
			else
				return LoadByPrimaryKeyStoredProcedure(returnNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String returnNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(returnNo);
			else
				return LoadByPrimaryKeyStoredProcedure(returnNo);
		}

		private bool LoadByPrimaryKeyDynamic(String returnNo)
		{
			esCssdSterileItemsReturnedQuery query = this.GetDynamicQuery();
			query.Where(query.ReturnNo == returnNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String returnNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ReturnNo", returnNo);
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
						case "ReturnNo": this.str.ReturnNo = (string)value; break;
						case "ReturnDate": this.str.ReturnDate = (string)value; break;
						case "ReturnTime": this.str.ReturnTime = (string)value; break;
						case "ToServiceUnitID": this.str.ToServiceUnitID = (string)value; break;
						case "HandedByUserID": this.str.HandedByUserID = (string)value; break;
						case "ReceivedBy": this.str.ReceivedBy = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "IsClosed": this.str.IsClosed = (string)value; break;
						case "ClosedDateTime": this.str.ClosedDateTime = (string)value; break;
						case "ClosedByUserID": this.str.ClosedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ReturnDate":

							if (value == null || value is System.DateTime)
								this.ReturnDate = (System.DateTime?)value;
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
						case "IsClosed":

							if (value == null || value is System.Boolean)
								this.IsClosed = (System.Boolean?)value;
							break;
						case "ClosedDateTime":

							if (value == null || value is System.DateTime)
								this.ClosedDateTime = (System.DateTime?)value;
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
		/// Maps to CssdSterileItemsReturned.ReturnNo
		/// </summary>
		virtual public System.String ReturnNo
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReturnedMetadata.ColumnNames.ReturnNo);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReturnedMetadata.ColumnNames.ReturnNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReturned.ReturnDate
		/// </summary>
		virtual public System.DateTime? ReturnDate
		{
			get
			{
				return base.GetSystemDateTime(CssdSterileItemsReturnedMetadata.ColumnNames.ReturnDate);
			}

			set
			{
				base.SetSystemDateTime(CssdSterileItemsReturnedMetadata.ColumnNames.ReturnDate, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReturned.ReturnTime
		/// </summary>
		virtual public System.String ReturnTime
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReturnedMetadata.ColumnNames.ReturnTime);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReturnedMetadata.ColumnNames.ReturnTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReturned.ToServiceUnitID
		/// </summary>
		virtual public System.String ToServiceUnitID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReturnedMetadata.ColumnNames.ToServiceUnitID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReturnedMetadata.ColumnNames.ToServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReturned.HandedByUserID
		/// </summary>
		virtual public System.String HandedByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReturnedMetadata.ColumnNames.HandedByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReturnedMetadata.ColumnNames.HandedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReturned.ReceivedBy
		/// </summary>
		virtual public System.String ReceivedBy
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReturnedMetadata.ColumnNames.ReceivedBy);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReturnedMetadata.ColumnNames.ReceivedBy, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReturned.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsReturnedMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsReturnedMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReturned.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdSterileItemsReturnedMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdSterileItemsReturnedMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReturned.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReturnedMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReturnedMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReturned.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsReturnedMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsReturnedMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReturned.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdSterileItemsReturnedMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdSterileItemsReturnedMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReturned.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReturnedMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReturnedMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReturned.IsClosed
		/// </summary>
		virtual public System.Boolean? IsClosed
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsReturnedMetadata.ColumnNames.IsClosed);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsReturnedMetadata.ColumnNames.IsClosed, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReturned.ClosedDateTime
		/// </summary>
		virtual public System.DateTime? ClosedDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdSterileItemsReturnedMetadata.ColumnNames.ClosedDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdSterileItemsReturnedMetadata.ColumnNames.ClosedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReturned.ClosedByUserID
		/// </summary>
		virtual public System.String ClosedByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReturnedMetadata.ColumnNames.ClosedByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReturnedMetadata.ColumnNames.ClosedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReturned.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdSterileItemsReturnedMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdSterileItemsReturnedMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReturned.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReturnedMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReturnedMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCssdSterileItemsReturned entity)
			{
				this.entity = entity;
			}
			public System.String ReturnNo
			{
				get
				{
					System.String data = entity.ReturnNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReturnNo = null;
					else entity.ReturnNo = Convert.ToString(value);
				}
			}
			public System.String ReturnDate
			{
				get
				{
					System.DateTime? data = entity.ReturnDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReturnDate = null;
					else entity.ReturnDate = Convert.ToDateTime(value);
				}
			}
			public System.String ReturnTime
			{
				get
				{
					System.String data = entity.ReturnTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReturnTime = null;
					else entity.ReturnTime = Convert.ToString(value);
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
			public System.String HandedByUserID
			{
				get
				{
					System.String data = entity.HandedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HandedByUserID = null;
					else entity.HandedByUserID = Convert.ToString(value);
				}
			}
			public System.String ReceivedBy
			{
				get
				{
					System.String data = entity.ReceivedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedBy = null;
					else entity.ReceivedBy = Convert.ToString(value);
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
			public System.String IsClosed
			{
				get
				{
					System.Boolean? data = entity.IsClosed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClosed = null;
					else entity.IsClosed = Convert.ToBoolean(value);
				}
			}
			public System.String ClosedDateTime
			{
				get
				{
					System.DateTime? data = entity.ClosedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedDateTime = null;
					else entity.ClosedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ClosedByUserID
			{
				get
				{
					System.String data = entity.ClosedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedByUserID = null;
					else entity.ClosedByUserID = Convert.ToString(value);
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
			private esCssdSterileItemsReturned entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCssdSterileItemsReturnedQuery query)
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
				throw new Exception("esCssdSterileItemsReturned can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CssdSterileItemsReturned : esCssdSterileItemsReturned
	{
	}

	[Serializable]
	abstract public class esCssdSterileItemsReturnedQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CssdSterileItemsReturnedMetadata.Meta();
			}
		}

		public esQueryItem ReturnNo
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReturnedMetadata.ColumnNames.ReturnNo, esSystemType.String);
			}
		}

		public esQueryItem ReturnDate
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReturnedMetadata.ColumnNames.ReturnDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ReturnTime
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReturnedMetadata.ColumnNames.ReturnTime, esSystemType.String);
			}
		}

		public esQueryItem ToServiceUnitID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReturnedMetadata.ColumnNames.ToServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem HandedByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReturnedMetadata.ColumnNames.HandedByUserID, esSystemType.String);
			}
		}

		public esQueryItem ReceivedBy
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReturnedMetadata.ColumnNames.ReceivedBy, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReturnedMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReturnedMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReturnedMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReturnedMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReturnedMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReturnedMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsClosed
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReturnedMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
			}
		}

		public esQueryItem ClosedDateTime
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReturnedMetadata.ColumnNames.ClosedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ClosedByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReturnedMetadata.ColumnNames.ClosedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReturnedMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReturnedMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CssdSterileItemsReturnedCollection")]
	public partial class CssdSterileItemsReturnedCollection : esCssdSterileItemsReturnedCollection, IEnumerable<CssdSterileItemsReturned>
	{
		public CssdSterileItemsReturnedCollection()
		{

		}

		public static implicit operator List<CssdSterileItemsReturned>(CssdSterileItemsReturnedCollection coll)
		{
			List<CssdSterileItemsReturned> list = new List<CssdSterileItemsReturned>();

			foreach (CssdSterileItemsReturned emp in coll)
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
				return CssdSterileItemsReturnedMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdSterileItemsReturnedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CssdSterileItemsReturned(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CssdSterileItemsReturned();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CssdSterileItemsReturnedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdSterileItemsReturnedQuery();
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
		public bool Load(CssdSterileItemsReturnedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CssdSterileItemsReturned AddNew()
		{
			CssdSterileItemsReturned entity = base.AddNewEntity() as CssdSterileItemsReturned;

			return entity;
		}
		public CssdSterileItemsReturned FindByPrimaryKey(String returnNo)
		{
			return base.FindByPrimaryKey(returnNo) as CssdSterileItemsReturned;
		}

		#region IEnumerable< CssdSterileItemsReturned> Members

		IEnumerator<CssdSterileItemsReturned> IEnumerable<CssdSterileItemsReturned>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CssdSterileItemsReturned;
			}
		}

		#endregion

		private CssdSterileItemsReturnedQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CssdSterileItemsReturned' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CssdSterileItemsReturned ({ReturnNo})")]
	[Serializable]
	public partial class CssdSterileItemsReturned : esCssdSterileItemsReturned
	{
		public CssdSterileItemsReturned()
		{
		}

		public CssdSterileItemsReturned(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CssdSterileItemsReturnedMetadata.Meta();
			}
		}

		override protected esCssdSterileItemsReturnedQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdSterileItemsReturnedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CssdSterileItemsReturnedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdSterileItemsReturnedQuery();
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
		public bool Load(CssdSterileItemsReturnedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CssdSterileItemsReturnedQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CssdSterileItemsReturnedQuery : esCssdSterileItemsReturnedQuery
	{
		public CssdSterileItemsReturnedQuery()
		{

		}

		public CssdSterileItemsReturnedQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CssdSterileItemsReturnedQuery";
		}
	}

	[Serializable]
	public partial class CssdSterileItemsReturnedMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CssdSterileItemsReturnedMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CssdSterileItemsReturnedMetadata.ColumnNames.ReturnNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReturnedMetadata.PropertyNames.ReturnNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReturnedMetadata.ColumnNames.ReturnDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterileItemsReturnedMetadata.PropertyNames.ReturnDate;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReturnedMetadata.ColumnNames.ReturnTime, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReturnedMetadata.PropertyNames.ReturnTime;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReturnedMetadata.ColumnNames.ToServiceUnitID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReturnedMetadata.PropertyNames.ToServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReturnedMetadata.ColumnNames.HandedByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReturnedMetadata.PropertyNames.HandedByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReturnedMetadata.ColumnNames.ReceivedBy, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReturnedMetadata.PropertyNames.ReceivedBy;
			c.CharacterMaxLength = 150;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReturnedMetadata.ColumnNames.IsApproved, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsReturnedMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReturnedMetadata.ColumnNames.ApprovedDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterileItemsReturnedMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReturnedMetadata.ColumnNames.ApprovedByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReturnedMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReturnedMetadata.ColumnNames.IsVoid, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsReturnedMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReturnedMetadata.ColumnNames.VoidDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterileItemsReturnedMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReturnedMetadata.ColumnNames.VoidByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReturnedMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReturnedMetadata.ColumnNames.IsClosed, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsReturnedMetadata.PropertyNames.IsClosed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReturnedMetadata.ColumnNames.ClosedDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterileItemsReturnedMetadata.PropertyNames.ClosedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReturnedMetadata.ColumnNames.ClosedByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReturnedMetadata.PropertyNames.ClosedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReturnedMetadata.ColumnNames.LastUpdateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterileItemsReturnedMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReturnedMetadata.ColumnNames.LastUpdateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReturnedMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CssdSterileItemsReturnedMetadata Meta()
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
			public const string ReturnNo = "ReturnNo";
			public const string ReturnDate = "ReturnDate";
			public const string ReturnTime = "ReturnTime";
			public const string ToServiceUnitID = "ToServiceUnitID";
			public const string HandedByUserID = "HandedByUserID";
			public const string ReceivedBy = "ReceivedBy";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsClosed = "IsClosed";
			public const string ClosedDateTime = "ClosedDateTime";
			public const string ClosedByUserID = "ClosedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ReturnNo = "ReturnNo";
			public const string ReturnDate = "ReturnDate";
			public const string ReturnTime = "ReturnTime";
			public const string ToServiceUnitID = "ToServiceUnitID";
			public const string HandedByUserID = "HandedByUserID";
			public const string ReceivedBy = "ReceivedBy";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsClosed = "IsClosed";
			public const string ClosedDateTime = "ClosedDateTime";
			public const string ClosedByUserID = "ClosedByUserID";
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
			lock (typeof(CssdSterileItemsReturnedMetadata))
			{
				if (CssdSterileItemsReturnedMetadata.mapDelegates == null)
				{
					CssdSterileItemsReturnedMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CssdSterileItemsReturnedMetadata.meta == null)
				{
					CssdSterileItemsReturnedMetadata.meta = new CssdSterileItemsReturnedMetadata();
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

				meta.AddTypeMap("ReturnNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReturnDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ReturnTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("HandedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceivedBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ClosedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ClosedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CssdSterileItemsReturned";
				meta.Destination = "CssdSterileItemsReturned";
				meta.spInsert = "proc_CssdSterileItemsReturnedInsert";
				meta.spUpdate = "proc_CssdSterileItemsReturnedUpdate";
				meta.spDelete = "proc_CssdSterileItemsReturnedDelete";
				meta.spLoadAll = "proc_CssdSterileItemsReturnedLoadAll";
				meta.spLoadByPrimaryKey = "proc_CssdSterileItemsReturnedLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CssdSterileItemsReturnedMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
