/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/24/2023 2:25:39 PM
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
	abstract public class esCssdSterileItemsReceivedCollection : esEntityCollectionWAuditLog
	{
		public esCssdSterileItemsReceivedCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CssdSterileItemsReceivedCollection";
		}

		#region Query Logic
		protected void InitQuery(esCssdSterileItemsReceivedQuery query)
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
			this.InitQuery(query as esCssdSterileItemsReceivedQuery);
		}
		#endregion

		virtual public CssdSterileItemsReceived DetachEntity(CssdSterileItemsReceived entity)
		{
			return base.DetachEntity(entity) as CssdSterileItemsReceived;
		}

		virtual public CssdSterileItemsReceived AttachEntity(CssdSterileItemsReceived entity)
		{
			return base.AttachEntity(entity) as CssdSterileItemsReceived;
		}

		virtual public void Combine(CssdSterileItemsReceivedCollection collection)
		{
			base.Combine(collection);
		}

		new public CssdSterileItemsReceived this[int index]
		{
			get
			{
				return base[index] as CssdSterileItemsReceived;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CssdSterileItemsReceived);
		}
	}

	[Serializable]
	abstract public class esCssdSterileItemsReceived : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCssdSterileItemsReceivedQuery GetDynamicQuery()
		{
			return null;
		}

		public esCssdSterileItemsReceived()
		{
		}

		public esCssdSterileItemsReceived(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String receivedNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(receivedNo);
			else
				return LoadByPrimaryKeyStoredProcedure(receivedNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String receivedNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(receivedNo);
			else
				return LoadByPrimaryKeyStoredProcedure(receivedNo);
		}

		private bool LoadByPrimaryKeyDynamic(String receivedNo)
		{
			esCssdSterileItemsReceivedQuery query = this.GetDynamicQuery();
			query.Where(query.ReceivedNo == receivedNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String receivedNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ReceivedNo", receivedNo);
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
						case "ReceivedDate": this.str.ReceivedDate = (string)value; break;
						case "ReceivedTime": this.str.ReceivedTime = (string)value; break;
						case "FromServiceUnitID": this.str.FromServiceUnitID = (string)value; break;
						case "FromRoomID": this.str.FromRoomID = (string)value; break;
						case "SenderByID": this.str.SenderByID = (string)value; break;
						case "SenderBy": this.str.SenderBy = (string)value; break;
						case "ReceivedByUserID": this.str.ReceivedByUserID = (string)value; break;
						case "IsFromProductionOfGoods": this.str.IsFromProductionOfGoods = (string)value; break;
						case "ProductionNo": this.str.ProductionNo = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SRInstrumentType": this.str.SRInstrumentType = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ReceivedDate":

							if (value == null || value is System.DateTime)
								this.ReceivedDate = (System.DateTime?)value;
							break;
						case "IsFromProductionOfGoods":

							if (value == null || value is System.Boolean)
								this.IsFromProductionOfGoods = (System.Boolean?)value;
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
		/// Maps to CssdSterileItemsReceived.ReceivedNo
		/// </summary>
		virtual public System.String ReceivedNo
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.ReceivedNo);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.ReceivedNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceived.ReceivedDate
		/// </summary>
		virtual public System.DateTime? ReceivedDate
		{
			get
			{
				return base.GetSystemDateTime(CssdSterileItemsReceivedMetadata.ColumnNames.ReceivedDate);
			}

			set
			{
				base.SetSystemDateTime(CssdSterileItemsReceivedMetadata.ColumnNames.ReceivedDate, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceived.ReceivedTime
		/// </summary>
		virtual public System.String ReceivedTime
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.ReceivedTime);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.ReceivedTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceived.FromServiceUnitID
		/// </summary>
		virtual public System.String FromServiceUnitID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.FromServiceUnitID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.FromServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceived.FromRoomID
		/// </summary>
		virtual public System.String FromRoomID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.FromRoomID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.FromRoomID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceived.SenderByID
		/// </summary>
		virtual public System.String SenderByID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.SenderByID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.SenderByID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceived.SenderBy
		/// </summary>
		virtual public System.String SenderBy
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.SenderBy);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.SenderBy, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceived.ReceivedByUserID
		/// </summary>
		virtual public System.String ReceivedByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.ReceivedByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.ReceivedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceived.IsFromProductionOfGoods
		/// </summary>
		virtual public System.Boolean? IsFromProductionOfGoods
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsReceivedMetadata.ColumnNames.IsFromProductionOfGoods);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsReceivedMetadata.ColumnNames.IsFromProductionOfGoods, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceived.ProductionNo
		/// </summary>
		virtual public System.String ProductionNo
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.ProductionNo);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.ProductionNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceived.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsReceivedMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsReceivedMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceived.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdSterileItemsReceivedMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdSterileItemsReceivedMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceived.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceived.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsReceivedMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsReceivedMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceived.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdSterileItemsReceivedMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdSterileItemsReceivedMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceived.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceived.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdSterileItemsReceivedMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdSterileItemsReceivedMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceived.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsReceived.SRInstrumentType
		/// </summary>
		virtual public System.String SRInstrumentType
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.SRInstrumentType);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsReceivedMetadata.ColumnNames.SRInstrumentType, value);
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
			public esStrings(esCssdSterileItemsReceived entity)
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
			public System.String ReceivedDate
			{
				get
				{
					System.DateTime? data = entity.ReceivedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedDate = null;
					else entity.ReceivedDate = Convert.ToDateTime(value);
				}
			}
			public System.String ReceivedTime
			{
				get
				{
					System.String data = entity.ReceivedTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedTime = null;
					else entity.ReceivedTime = Convert.ToString(value);
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
			public System.String FromRoomID
			{
				get
				{
					System.String data = entity.FromRoomID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromRoomID = null;
					else entity.FromRoomID = Convert.ToString(value);
				}
			}
			public System.String SenderByID
			{
				get
				{
					System.String data = entity.SenderByID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SenderByID = null;
					else entity.SenderByID = Convert.ToString(value);
				}
			}
			public System.String SenderBy
			{
				get
				{
					System.String data = entity.SenderBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SenderBy = null;
					else entity.SenderBy = Convert.ToString(value);
				}
			}
			public System.String ReceivedByUserID
			{
				get
				{
					System.String data = entity.ReceivedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedByUserID = null;
					else entity.ReceivedByUserID = Convert.ToString(value);
				}
			}
			public System.String IsFromProductionOfGoods
			{
				get
				{
					System.Boolean? data = entity.IsFromProductionOfGoods;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFromProductionOfGoods = null;
					else entity.IsFromProductionOfGoods = Convert.ToBoolean(value);
				}
			}
			public System.String ProductionNo
			{
				get
				{
					System.String data = entity.ProductionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProductionNo = null;
					else entity.ProductionNo = Convert.ToString(value);
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
			public System.String SRInstrumentType
			{
				get
				{
					System.String data = entity.SRInstrumentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRInstrumentType = null;
					else entity.SRInstrumentType = Convert.ToString(value);
				}
			}
			private esCssdSterileItemsReceived entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCssdSterileItemsReceivedQuery query)
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
				throw new Exception("esCssdSterileItemsReceived can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CssdSterileItemsReceived : esCssdSterileItemsReceived
	{
	}

	[Serializable]
	abstract public class esCssdSterileItemsReceivedQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CssdSterileItemsReceivedMetadata.Meta();
			}
		}

		public esQueryItem ReceivedNo
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedMetadata.ColumnNames.ReceivedNo, esSystemType.String);
			}
		}

		public esQueryItem ReceivedDate
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedMetadata.ColumnNames.ReceivedDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ReceivedTime
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedMetadata.ColumnNames.ReceivedTime, esSystemType.String);
			}
		}

		public esQueryItem FromServiceUnitID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedMetadata.ColumnNames.FromServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem FromRoomID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedMetadata.ColumnNames.FromRoomID, esSystemType.String);
			}
		}

		public esQueryItem SenderByID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedMetadata.ColumnNames.SenderByID, esSystemType.String);
			}
		}

		public esQueryItem SenderBy
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedMetadata.ColumnNames.SenderBy, esSystemType.String);
			}
		}

		public esQueryItem ReceivedByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedMetadata.ColumnNames.ReceivedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsFromProductionOfGoods
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedMetadata.ColumnNames.IsFromProductionOfGoods, esSystemType.Boolean);
			}
		}

		public esQueryItem ProductionNo
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedMetadata.ColumnNames.ProductionNo, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SRInstrumentType
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsReceivedMetadata.ColumnNames.SRInstrumentType, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CssdSterileItemsReceivedCollection")]
	public partial class CssdSterileItemsReceivedCollection : esCssdSterileItemsReceivedCollection, IEnumerable<CssdSterileItemsReceived>
	{
		public CssdSterileItemsReceivedCollection()
		{

		}

		public static implicit operator List<CssdSterileItemsReceived>(CssdSterileItemsReceivedCollection coll)
		{
			List<CssdSterileItemsReceived> list = new List<CssdSterileItemsReceived>();

			foreach (CssdSterileItemsReceived emp in coll)
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
				return CssdSterileItemsReceivedMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdSterileItemsReceivedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CssdSterileItemsReceived(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CssdSterileItemsReceived();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CssdSterileItemsReceivedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdSterileItemsReceivedQuery();
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
		public bool Load(CssdSterileItemsReceivedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CssdSterileItemsReceived AddNew()
		{
			CssdSterileItemsReceived entity = base.AddNewEntity() as CssdSterileItemsReceived;

			return entity;
		}
		public CssdSterileItemsReceived FindByPrimaryKey(String receivedNo)
		{
			return base.FindByPrimaryKey(receivedNo) as CssdSterileItemsReceived;
		}

		#region IEnumerable< CssdSterileItemsReceived> Members

		IEnumerator<CssdSterileItemsReceived> IEnumerable<CssdSterileItemsReceived>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CssdSterileItemsReceived;
			}
		}

		#endregion

		private CssdSterileItemsReceivedQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CssdSterileItemsReceived' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CssdSterileItemsReceived ({ReceivedNo})")]
	[Serializable]
	public partial class CssdSterileItemsReceived : esCssdSterileItemsReceived
	{
		public CssdSterileItemsReceived()
		{
		}

		public CssdSterileItemsReceived(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CssdSterileItemsReceivedMetadata.Meta();
			}
		}

		override protected esCssdSterileItemsReceivedQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdSterileItemsReceivedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CssdSterileItemsReceivedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdSterileItemsReceivedQuery();
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
		public bool Load(CssdSterileItemsReceivedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CssdSterileItemsReceivedQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CssdSterileItemsReceivedQuery : esCssdSterileItemsReceivedQuery
	{
		public CssdSterileItemsReceivedQuery()
		{

		}

		public CssdSterileItemsReceivedQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CssdSterileItemsReceivedQuery";
		}
	}

	[Serializable]
	public partial class CssdSterileItemsReceivedMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CssdSterileItemsReceivedMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CssdSterileItemsReceivedMetadata.ColumnNames.ReceivedNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedMetadata.PropertyNames.ReceivedNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedMetadata.ColumnNames.ReceivedDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterileItemsReceivedMetadata.PropertyNames.ReceivedDate;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedMetadata.ColumnNames.ReceivedTime, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedMetadata.PropertyNames.ReceivedTime;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedMetadata.ColumnNames.FromServiceUnitID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedMetadata.PropertyNames.FromServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedMetadata.ColumnNames.FromRoomID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedMetadata.PropertyNames.FromRoomID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedMetadata.ColumnNames.SenderByID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedMetadata.PropertyNames.SenderByID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedMetadata.ColumnNames.SenderBy, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedMetadata.PropertyNames.SenderBy;
			c.CharacterMaxLength = 250;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedMetadata.ColumnNames.ReceivedByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedMetadata.PropertyNames.ReceivedByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedMetadata.ColumnNames.IsFromProductionOfGoods, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsReceivedMetadata.PropertyNames.IsFromProductionOfGoods;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedMetadata.ColumnNames.ProductionNo, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedMetadata.PropertyNames.ProductionNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedMetadata.ColumnNames.IsApproved, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsReceivedMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedMetadata.ColumnNames.ApprovedDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterileItemsReceivedMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedMetadata.ColumnNames.ApprovedByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedMetadata.ColumnNames.IsVoid, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsReceivedMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedMetadata.ColumnNames.VoidDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterileItemsReceivedMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedMetadata.ColumnNames.VoidByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedMetadata.ColumnNames.LastUpdateDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterileItemsReceivedMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedMetadata.ColumnNames.LastUpdateByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsReceivedMetadata.ColumnNames.SRInstrumentType, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsReceivedMetadata.PropertyNames.SRInstrumentType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CssdSterileItemsReceivedMetadata Meta()
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
			public const string ReceivedDate = "ReceivedDate";
			public const string ReceivedTime = "ReceivedTime";
			public const string FromServiceUnitID = "FromServiceUnitID";
			public const string FromRoomID = "FromRoomID";
			public const string SenderByID = "SenderByID";
			public const string SenderBy = "SenderBy";
			public const string ReceivedByUserID = "ReceivedByUserID";
			public const string IsFromProductionOfGoods = "IsFromProductionOfGoods";
			public const string ProductionNo = "ProductionNo";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRInstrumentType = "SRInstrumentType";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ReceivedNo = "ReceivedNo";
			public const string ReceivedDate = "ReceivedDate";
			public const string ReceivedTime = "ReceivedTime";
			public const string FromServiceUnitID = "FromServiceUnitID";
			public const string FromRoomID = "FromRoomID";
			public const string SenderByID = "SenderByID";
			public const string SenderBy = "SenderBy";
			public const string ReceivedByUserID = "ReceivedByUserID";
			public const string IsFromProductionOfGoods = "IsFromProductionOfGoods";
			public const string ProductionNo = "ProductionNo";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRInstrumentType = "SRInstrumentType";
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
			lock (typeof(CssdSterileItemsReceivedMetadata))
			{
				if (CssdSterileItemsReceivedMetadata.mapDelegates == null)
				{
					CssdSterileItemsReceivedMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CssdSterileItemsReceivedMetadata.meta == null)
				{
					CssdSterileItemsReceivedMetadata.meta = new CssdSterileItemsReceivedMetadata();
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
				meta.AddTypeMap("ReceivedDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ReceivedTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromRoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SenderByID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SenderBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceivedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsFromProductionOfGoods", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ProductionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRInstrumentType", new esTypeMap("varchar", "System.String"));


				meta.Source = "CssdSterileItemsReceived";
				meta.Destination = "CssdSterileItemsReceived";
				meta.spInsert = "proc_CssdSterileItemsReceivedInsert";
				meta.spUpdate = "proc_CssdSterileItemsReceivedUpdate";
				meta.spDelete = "proc_CssdSterileItemsReceivedDelete";
				meta.spLoadAll = "proc_CssdSterileItemsReceivedLoadAll";
				meta.spLoadByPrimaryKey = "proc_CssdSterileItemsReceivedLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CssdSterileItemsReceivedMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
