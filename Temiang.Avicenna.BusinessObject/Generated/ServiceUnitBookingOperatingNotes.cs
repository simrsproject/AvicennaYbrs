/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/4/2023 2:57:24 PM
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
	abstract public class esServiceUnitBookingOperatingNotesCollection : esEntityCollectionWAuditLog
	{
		public esServiceUnitBookingOperatingNotesCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ServiceUnitBookingOperatingNotesCollection";
		}

		#region Query Logic
		protected void InitQuery(esServiceUnitBookingOperatingNotesQuery query)
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
			this.InitQuery(query as esServiceUnitBookingOperatingNotesQuery);
		}
		#endregion

		virtual public ServiceUnitBookingOperatingNotes DetachEntity(ServiceUnitBookingOperatingNotes entity)
		{
			return base.DetachEntity(entity) as ServiceUnitBookingOperatingNotes;
		}

		virtual public ServiceUnitBookingOperatingNotes AttachEntity(ServiceUnitBookingOperatingNotes entity)
		{
			return base.AttachEntity(entity) as ServiceUnitBookingOperatingNotes;
		}

		virtual public void Combine(ServiceUnitBookingOperatingNotesCollection collection)
		{
			base.Combine(collection);
		}

		new public ServiceUnitBookingOperatingNotes this[int index]
		{
			get
			{
				return base[index] as ServiceUnitBookingOperatingNotes;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceUnitBookingOperatingNotes);
		}
	}

	[Serializable]
	abstract public class esServiceUnitBookingOperatingNotes : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceUnitBookingOperatingNotesQuery GetDynamicQuery()
		{
			return null;
		}

		public esServiceUnitBookingOperatingNotes()
		{
		}

		public esServiceUnitBookingOperatingNotes(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String bookingNo, String opNotesSeqNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bookingNo, opNotesSeqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(bookingNo, opNotesSeqNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String bookingNo, String opNotesSeqNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bookingNo, opNotesSeqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(bookingNo, opNotesSeqNo);
		}

		private bool LoadByPrimaryKeyDynamic(String bookingNo, String opNotesSeqNo)
		{
			esServiceUnitBookingOperatingNotesQuery query = this.GetDynamicQuery();
			query.Where(query.BookingNo == bookingNo, query.OpNotesSeqNo == opNotesSeqNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String bookingNo, String opNotesSeqNo)
		{
			esParameters parms = new esParameters();
			parms.Add("BookingNo", bookingNo);
			parms.Add("OpNotesSeqNo", opNotesSeqNo);
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
						case "BookingNo": this.str.BookingNo = (string)value; break;
						case "OpNotesSeqNo": this.str.OpNotesSeqNo = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "Regio": this.str.Regio = (string)value; break;
						case "OperatingNotes": this.str.OperatingNotes = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "PostSurgeryInstructions": this.str.PostSurgeryInstructions = (string)value; break;
						case "ComplicationsNotes": this.str.ComplicationsNotes = (string)value; break;
						case "PreDiagnosis": this.str.PreDiagnosis = (string)value; break;
						case "PostDiagnosis": this.str.PostDiagnosis = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "LocalistStatus":

							if (value == null || value is System.Byte[])
								this.LocalistStatus = (System.Byte[])value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "SurgeonSign":

							if (value == null || value is System.Byte[])
								this.SurgeonSign = (System.Byte[])value;
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
		/// Maps to ServiceUnitBookingOperatingNotes.BookingNo
		/// </summary>
		virtual public System.String BookingNo
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.BookingNo);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.BookingNo, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingOperatingNotes.OpNotesSeqNo
		/// </summary>
		virtual public System.String OpNotesSeqNo
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.OpNotesSeqNo);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.OpNotesSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingOperatingNotes.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingOperatingNotes.Regio
		/// </summary>
		virtual public System.String Regio
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.Regio);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.Regio, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingOperatingNotes.OperatingNotes
		/// </summary>
		virtual public System.String OperatingNotes
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.OperatingNotes);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.OperatingNotes, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingOperatingNotes.LocalistStatus
		/// </summary>
		virtual public System.Byte[] LocalistStatus
		{
			get
			{
				return base.GetSystemByteArray(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.LocalistStatus);
			}

			set
			{
				base.SetSystemByteArray(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.LocalistStatus, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingOperatingNotes.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingOperatingNotes.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingOperatingNotes.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingOperatingNotes.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingOperatingNotes.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingOperatingNotes.PostSurgeryInstructions
		/// </summary>
		virtual public System.String PostSurgeryInstructions
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.PostSurgeryInstructions);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.PostSurgeryInstructions, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingOperatingNotes.SurgeonSign
		/// </summary>
		virtual public System.Byte[] SurgeonSign
		{
			get
			{
				return base.GetSystemByteArray(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.SurgeonSign);
			}

			set
			{
				base.SetSystemByteArray(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.SurgeonSign, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingOperatingNotes.ComplicationsNotes
		/// </summary>
		virtual public System.String ComplicationsNotes
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.ComplicationsNotes);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.ComplicationsNotes, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingOperatingNotes.PreDiagnosis
		/// </summary>
		virtual public System.String PreDiagnosis
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.PreDiagnosis);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.PreDiagnosis, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBookingOperatingNotes.PostDiagnosis
		/// </summary>
		virtual public System.String PostDiagnosis
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.PostDiagnosis);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.PostDiagnosis, value);
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
			public esStrings(esServiceUnitBookingOperatingNotes entity)
			{
				this.entity = entity;
			}
			public System.String BookingNo
			{
				get
				{
					System.String data = entity.BookingNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BookingNo = null;
					else entity.BookingNo = Convert.ToString(value);
				}
			}
			public System.String OpNotesSeqNo
			{
				get
				{
					System.String data = entity.OpNotesSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OpNotesSeqNo = null;
					else entity.OpNotesSeqNo = Convert.ToString(value);
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
			public System.String Regio
			{
				get
				{
					System.String data = entity.Regio;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Regio = null;
					else entity.Regio = Convert.ToString(value);
				}
			}
			public System.String OperatingNotes
			{
				get
				{
					System.String data = entity.OperatingNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OperatingNotes = null;
					else entity.OperatingNotes = Convert.ToString(value);
				}
			}
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
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
			public System.String PostSurgeryInstructions
			{
				get
				{
					System.String data = entity.PostSurgeryInstructions;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PostSurgeryInstructions = null;
					else entity.PostSurgeryInstructions = Convert.ToString(value);
				}
			}
			public System.String ComplicationsNotes
			{
				get
				{
					System.String data = entity.ComplicationsNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ComplicationsNotes = null;
					else entity.ComplicationsNotes = Convert.ToString(value);
				}
			}
			public System.String PreDiagnosis
			{
				get
				{
					System.String data = entity.PreDiagnosis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PreDiagnosis = null;
					else entity.PreDiagnosis = Convert.ToString(value);
				}
			}
			public System.String PostDiagnosis
			{
				get
				{
					System.String data = entity.PostDiagnosis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PostDiagnosis = null;
					else entity.PostDiagnosis = Convert.ToString(value);
				}
			}
			private esServiceUnitBookingOperatingNotes entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceUnitBookingOperatingNotesQuery query)
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
				throw new Exception("esServiceUnitBookingOperatingNotes can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ServiceUnitBookingOperatingNotes : esServiceUnitBookingOperatingNotes
	{
	}

	[Serializable]
	abstract public class esServiceUnitBookingOperatingNotesQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitBookingOperatingNotesMetadata.Meta();
			}
		}

		public esQueryItem BookingNo
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingOperatingNotesMetadata.ColumnNames.BookingNo, esSystemType.String);
			}
		}

		public esQueryItem OpNotesSeqNo
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingOperatingNotesMetadata.ColumnNames.OpNotesSeqNo, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingOperatingNotesMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem Regio
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingOperatingNotesMetadata.ColumnNames.Regio, esSystemType.String);
			}
		}

		public esQueryItem OperatingNotes
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingOperatingNotesMetadata.ColumnNames.OperatingNotes, esSystemType.String);
			}
		}

		public esQueryItem LocalistStatus
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingOperatingNotesMetadata.ColumnNames.LocalistStatus, esSystemType.ByteArray);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingOperatingNotesMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingOperatingNotesMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingOperatingNotesMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingOperatingNotesMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingOperatingNotesMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem PostSurgeryInstructions
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingOperatingNotesMetadata.ColumnNames.PostSurgeryInstructions, esSystemType.String);
			}
		}

		public esQueryItem SurgeonSign
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingOperatingNotesMetadata.ColumnNames.SurgeonSign, esSystemType.ByteArray);
			}
		}

		public esQueryItem ComplicationsNotes
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingOperatingNotesMetadata.ColumnNames.ComplicationsNotes, esSystemType.String);
			}
		}

		public esQueryItem PreDiagnosis
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingOperatingNotesMetadata.ColumnNames.PreDiagnosis, esSystemType.String);
			}
		}

		public esQueryItem PostDiagnosis
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingOperatingNotesMetadata.ColumnNames.PostDiagnosis, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceUnitBookingOperatingNotesCollection")]
	public partial class ServiceUnitBookingOperatingNotesCollection : esServiceUnitBookingOperatingNotesCollection, IEnumerable<ServiceUnitBookingOperatingNotes>
	{
		public ServiceUnitBookingOperatingNotesCollection()
		{

		}

		public static implicit operator List<ServiceUnitBookingOperatingNotes>(ServiceUnitBookingOperatingNotesCollection coll)
		{
			List<ServiceUnitBookingOperatingNotes> list = new List<ServiceUnitBookingOperatingNotes>();

			foreach (ServiceUnitBookingOperatingNotes emp in coll)
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
				return ServiceUnitBookingOperatingNotesMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitBookingOperatingNotesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceUnitBookingOperatingNotes(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceUnitBookingOperatingNotes();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ServiceUnitBookingOperatingNotesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitBookingOperatingNotesQuery();
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
		public bool Load(ServiceUnitBookingOperatingNotesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ServiceUnitBookingOperatingNotes AddNew()
		{
			ServiceUnitBookingOperatingNotes entity = base.AddNewEntity() as ServiceUnitBookingOperatingNotes;

			return entity;
		}
		public ServiceUnitBookingOperatingNotes FindByPrimaryKey(String bookingNo, String opNotesSeqNo)
		{
			return base.FindByPrimaryKey(bookingNo, opNotesSeqNo) as ServiceUnitBookingOperatingNotes;
		}

		#region IEnumerable< ServiceUnitBookingOperatingNotes> Members

		IEnumerator<ServiceUnitBookingOperatingNotes> IEnumerable<ServiceUnitBookingOperatingNotes>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ServiceUnitBookingOperatingNotes;
			}
		}

		#endregion

		private ServiceUnitBookingOperatingNotesQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceUnitBookingOperatingNotes' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ServiceUnitBookingOperatingNotes ({BookingNo, OpNotesSeqNo})")]
	[Serializable]
	public partial class ServiceUnitBookingOperatingNotes : esServiceUnitBookingOperatingNotes
	{
		public ServiceUnitBookingOperatingNotes()
		{
		}

		public ServiceUnitBookingOperatingNotes(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitBookingOperatingNotesMetadata.Meta();
			}
		}

		override protected esServiceUnitBookingOperatingNotesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitBookingOperatingNotesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ServiceUnitBookingOperatingNotesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitBookingOperatingNotesQuery();
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
		public bool Load(ServiceUnitBookingOperatingNotesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ServiceUnitBookingOperatingNotesQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ServiceUnitBookingOperatingNotesQuery : esServiceUnitBookingOperatingNotesQuery
	{
		public ServiceUnitBookingOperatingNotesQuery()
		{

		}

		public ServiceUnitBookingOperatingNotesQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ServiceUnitBookingOperatingNotesQuery";
		}
	}

	[Serializable]
	public partial class ServiceUnitBookingOperatingNotesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceUnitBookingOperatingNotesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.BookingNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingOperatingNotesMetadata.PropertyNames.BookingNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.OpNotesSeqNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingOperatingNotesMetadata.PropertyNames.OpNotesSeqNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.ParamedicID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingOperatingNotesMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.Regio, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingOperatingNotesMetadata.PropertyNames.Regio;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.OperatingNotes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingOperatingNotesMetadata.PropertyNames.OperatingNotes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.LocalistStatus, 5, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = ServiceUnitBookingOperatingNotesMetadata.PropertyNames.LocalistStatus;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.CreatedDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitBookingOperatingNotesMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.CreatedByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingOperatingNotesMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.IsVoid, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitBookingOperatingNotesMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitBookingOperatingNotesMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingOperatingNotesMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.PostSurgeryInstructions, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingOperatingNotesMetadata.PropertyNames.PostSurgeryInstructions;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.SurgeonSign, 12, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = ServiceUnitBookingOperatingNotesMetadata.PropertyNames.SurgeonSign;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.ComplicationsNotes, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingOperatingNotesMetadata.PropertyNames.ComplicationsNotes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.PreDiagnosis, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingOperatingNotesMetadata.PropertyNames.PreDiagnosis;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingOperatingNotesMetadata.ColumnNames.PostDiagnosis, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingOperatingNotesMetadata.PropertyNames.PostDiagnosis;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ServiceUnitBookingOperatingNotesMetadata Meta()
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
			public const string BookingNo = "BookingNo";
			public const string OpNotesSeqNo = "OpNotesSeqNo";
			public const string ParamedicID = "ParamedicID";
			public const string Regio = "Regio";
			public const string OperatingNotes = "OperatingNotes";
			public const string LocalistStatus = "LocalistStatus";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string IsVoid = "IsVoid";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string PostSurgeryInstructions = "PostSurgeryInstructions";
			public const string SurgeonSign = "SurgeonSign";
			public const string ComplicationsNotes = "ComplicationsNotes";
			public const string PreDiagnosis = "PreDiagnosis";
			public const string PostDiagnosis = "PostDiagnosis";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string BookingNo = "BookingNo";
			public const string OpNotesSeqNo = "OpNotesSeqNo";
			public const string ParamedicID = "ParamedicID";
			public const string Regio = "Regio";
			public const string OperatingNotes = "OperatingNotes";
			public const string LocalistStatus = "LocalistStatus";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string IsVoid = "IsVoid";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string PostSurgeryInstructions = "PostSurgeryInstructions";
			public const string SurgeonSign = "SurgeonSign";
			public const string ComplicationsNotes = "ComplicationsNotes";
			public const string PreDiagnosis = "PreDiagnosis";
			public const string PostDiagnosis = "PostDiagnosis";
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
			lock (typeof(ServiceUnitBookingOperatingNotesMetadata))
			{
				if (ServiceUnitBookingOperatingNotesMetadata.mapDelegates == null)
				{
					ServiceUnitBookingOperatingNotesMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ServiceUnitBookingOperatingNotesMetadata.meta == null)
				{
					ServiceUnitBookingOperatingNotesMetadata.meta = new ServiceUnitBookingOperatingNotesMetadata();
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

				meta.AddTypeMap("BookingNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OpNotesSeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Regio", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OperatingNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LocalistStatus", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PostSurgeryInstructions", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SurgeonSign", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("ComplicationsNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PreDiagnosis", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PostDiagnosis", new esTypeMap("varchar", "System.String"));


				meta.Source = "ServiceUnitBookingOperatingNotes";
				meta.Destination = "ServiceUnitBookingOperatingNotes";
				meta.spInsert = "proc_ServiceUnitBookingOperatingNotesInsert";
				meta.spUpdate = "proc_ServiceUnitBookingOperatingNotesUpdate";
				meta.spDelete = "proc_ServiceUnitBookingOperatingNotesDelete";
				meta.spLoadAll = "proc_ServiceUnitBookingOperatingNotesLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceUnitBookingOperatingNotesLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceUnitBookingOperatingNotesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
