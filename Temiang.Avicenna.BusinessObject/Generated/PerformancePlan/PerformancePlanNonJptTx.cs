/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/21/2023 1:56:21 PM
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
	abstract public class esPerformancePlanNonJptTxCollection : esEntityCollectionWAuditLog
	{
		public esPerformancePlanNonJptTxCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PerformancePlanNonJptTxCollection";
		}

		#region Query Logic
		protected void InitQuery(esPerformancePlanNonJptTxQuery query)
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
			this.InitQuery(query as esPerformancePlanNonJptTxQuery);
		}
		#endregion

		virtual public PerformancePlanNonJptTx DetachEntity(PerformancePlanNonJptTx entity)
		{
			return base.DetachEntity(entity) as PerformancePlanNonJptTx;
		}

		virtual public PerformancePlanNonJptTx AttachEntity(PerformancePlanNonJptTx entity)
		{
			return base.AttachEntity(entity) as PerformancePlanNonJptTx;
		}

		virtual public void Combine(PerformancePlanNonJptTxCollection collection)
		{
			base.Combine(collection);
		}

		new public PerformancePlanNonJptTx this[int index]
		{
			get
			{
				return base[index] as PerformancePlanNonJptTx;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PerformancePlanNonJptTx);
		}
	}

	[Serializable]
	abstract public class esPerformancePlanNonJptTx : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPerformancePlanNonJptTxQuery GetDynamicQuery()
		{
			return null;
		}

		public esPerformancePlanNonJptTx()
		{
		}

		public esPerformancePlanNonJptTx(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 txID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(txID);
			else
				return LoadByPrimaryKeyStoredProcedure(txID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 txID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(txID);
			else
				return LoadByPrimaryKeyStoredProcedure(txID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 txID)
		{
			esPerformancePlanNonJptTxQuery query = this.GetDynamicQuery();
			query.Where(query.TxID == txID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 txID)
		{
			esParameters parms = new esParameters();
			parms.Add("TxID", txID);
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
						case "TxID": this.str.TxID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "PerformancePlanID": this.str.PerformancePlanID = (string)value; break;
						case "YearPeriod": this.str.YearPeriod = (string)value; break;
						case "IsVerification": this.str.IsVerification = (string)value; break;
						case "VerificationDateTime": this.str.VerificationDateTime = (string)value; break;
						case "VerificationByUserID": this.str.VerificationByUserID = (string)value; break;
						case "IsClosed": this.str.IsClosed = (string)value; break;
						case "ClosedDateTime": this.str.ClosedDateTime = (string)value; break;
						case "ClosedByUserID": this.str.ClosedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "TxID":

							if (value == null || value is System.Int64)
								this.TxID = (System.Int64?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "PerformancePlanID":

							if (value == null || value is System.Int32)
								this.PerformancePlanID = (System.Int32?)value;
							break;
						case "IsVerification":

							if (value == null || value is System.Boolean)
								this.IsVerification = (System.Boolean?)value;
							break;
						case "VerificationDateTime":

							if (value == null || value is System.DateTime)
								this.VerificationDateTime = (System.DateTime?)value;
							break;
						case "IsClosed":

							if (value == null || value is System.Boolean)
								this.IsClosed = (System.Boolean?)value;
							break;
						case "ClosedDateTime":

							if (value == null || value is System.DateTime)
								this.ClosedDateTime = (System.DateTime?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to PerformancePlanNonJptTx.TxID
		/// </summary>
		virtual public System.Int64? TxID
		{
			get
			{
				return base.GetSystemInt64(PerformancePlanNonJptTxMetadata.ColumnNames.TxID);
			}

			set
			{
				base.SetSystemInt64(PerformancePlanNonJptTxMetadata.ColumnNames.TxID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTx.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanNonJptTxMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanNonJptTxMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTx.PerformancePlanID
		/// </summary>
		virtual public System.Int32? PerformancePlanID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanNonJptTxMetadata.ColumnNames.PerformancePlanID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanNonJptTxMetadata.ColumnNames.PerformancePlanID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTx.YearPeriod
		/// </summary>
		virtual public System.String YearPeriod
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxMetadata.ColumnNames.YearPeriod);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxMetadata.ColumnNames.YearPeriod, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTx.IsVerification
		/// </summary>
		virtual public System.Boolean? IsVerification
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanNonJptTxMetadata.ColumnNames.IsVerification);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanNonJptTxMetadata.ColumnNames.IsVerification, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTx.VerificationDateTime
		/// </summary>
		virtual public System.DateTime? VerificationDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanNonJptTxMetadata.ColumnNames.VerificationDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanNonJptTxMetadata.ColumnNames.VerificationDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTx.VerificationByUserID
		/// </summary>
		virtual public System.String VerificationByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxMetadata.ColumnNames.VerificationByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxMetadata.ColumnNames.VerificationByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTx.IsClosed
		/// </summary>
		virtual public System.Boolean? IsClosed
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanNonJptTxMetadata.ColumnNames.IsClosed);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanNonJptTxMetadata.ColumnNames.IsClosed, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTx.ClosedDateTime
		/// </summary>
		virtual public System.DateTime? ClosedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanNonJptTxMetadata.ColumnNames.ClosedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanNonJptTxMetadata.ColumnNames.ClosedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTx.ClosedByUserID
		/// </summary>
		virtual public System.String ClosedByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxMetadata.ColumnNames.ClosedByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxMetadata.ColumnNames.ClosedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTx.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanNonJptTxMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanNonJptTxMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTx.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTx.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanNonJptTxMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanNonJptTxMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJptTx.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptTxMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptTxMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPerformancePlanNonJptTx entity)
			{
				this.entity = entity;
			}
			public System.String TxID
			{
				get
				{
					System.Int64? data = entity.TxID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TxID = null;
					else entity.TxID = Convert.ToInt64(value);
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
			public System.String PerformancePlanID
			{
				get
				{
					System.Int32? data = entity.PerformancePlanID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PerformancePlanID = null;
					else entity.PerformancePlanID = Convert.ToInt32(value);
				}
			}
			public System.String YearPeriod
			{
				get
				{
					System.String data = entity.YearPeriod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.YearPeriod = null;
					else entity.YearPeriod = Convert.ToString(value);
				}
			}
			public System.String IsVerification
			{
				get
				{
					System.Boolean? data = entity.IsVerification;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVerification = null;
					else entity.IsVerification = Convert.ToBoolean(value);
				}
			}
			public System.String VerificationDateTime
			{
				get
				{
					System.DateTime? data = entity.VerificationDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationDateTime = null;
					else entity.VerificationDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VerificationByUserID
			{
				get
				{
					System.String data = entity.VerificationByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationByUserID = null;
					else entity.VerificationByUserID = Convert.ToString(value);
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
			private esPerformancePlanNonJptTx entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPerformancePlanNonJptTxQuery query)
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
				throw new Exception("esPerformancePlanNonJptTx can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PerformancePlanNonJptTx : esPerformancePlanNonJptTx
	{
	}

	[Serializable]
	abstract public class esPerformancePlanNonJptTxQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanNonJptTxMetadata.Meta();
			}
		}

		public esQueryItem TxID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxMetadata.ColumnNames.TxID, esSystemType.Int64);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem PerformancePlanID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxMetadata.ColumnNames.PerformancePlanID, esSystemType.Int32);
			}
		}

		public esQueryItem YearPeriod
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxMetadata.ColumnNames.YearPeriod, esSystemType.String);
			}
		}

		public esQueryItem IsVerification
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxMetadata.ColumnNames.IsVerification, esSystemType.Boolean);
			}
		}

		public esQueryItem VerificationDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxMetadata.ColumnNames.VerificationDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VerificationByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxMetadata.ColumnNames.VerificationByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsClosed
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
			}
		}

		public esQueryItem ClosedDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxMetadata.ColumnNames.ClosedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ClosedByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxMetadata.ColumnNames.ClosedByUserID, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptTxMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PerformancePlanNonJptTxCollection")]
	public partial class PerformancePlanNonJptTxCollection : esPerformancePlanNonJptTxCollection, IEnumerable<PerformancePlanNonJptTx>
	{
		public PerformancePlanNonJptTxCollection()
		{

		}

		public static implicit operator List<PerformancePlanNonJptTx>(PerformancePlanNonJptTxCollection coll)
		{
			List<PerformancePlanNonJptTx> list = new List<PerformancePlanNonJptTx>();

			foreach (PerformancePlanNonJptTx emp in coll)
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
				return PerformancePlanNonJptTxMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanNonJptTxQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PerformancePlanNonJptTx(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PerformancePlanNonJptTx();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanNonJptTxQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanNonJptTxQuery();
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
		public bool Load(PerformancePlanNonJptTxQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PerformancePlanNonJptTx AddNew()
		{
			PerformancePlanNonJptTx entity = base.AddNewEntity() as PerformancePlanNonJptTx;

			return entity;
		}
		public PerformancePlanNonJptTx FindByPrimaryKey(Int64 txID)
		{
			return base.FindByPrimaryKey(txID) as PerformancePlanNonJptTx;
		}

		#region IEnumerable< PerformancePlanNonJptTx> Members

		IEnumerator<PerformancePlanNonJptTx> IEnumerable<PerformancePlanNonJptTx>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PerformancePlanNonJptTx;
			}
		}

		#endregion

		private PerformancePlanNonJptTxQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PerformancePlanNonJptTx' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PerformancePlanNonJptTx ({TxID})")]
	[Serializable]
	public partial class PerformancePlanNonJptTx : esPerformancePlanNonJptTx
	{
		public PerformancePlanNonJptTx()
		{
		}

		public PerformancePlanNonJptTx(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanNonJptTxMetadata.Meta();
			}
		}

		override protected esPerformancePlanNonJptTxQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanNonJptTxQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanNonJptTxQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanNonJptTxQuery();
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
		public bool Load(PerformancePlanNonJptTxQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PerformancePlanNonJptTxQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PerformancePlanNonJptTxQuery : esPerformancePlanNonJptTxQuery
	{
		public PerformancePlanNonJptTxQuery()
		{

		}

		public PerformancePlanNonJptTxQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PerformancePlanNonJptTxQuery";
		}
	}

	[Serializable]
	public partial class PerformancePlanNonJptTxMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PerformancePlanNonJptTxMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PerformancePlanNonJptTxMetadata.ColumnNames.TxID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = PerformancePlanNonJptTxMetadata.PropertyNames.TxID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanNonJptTxMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxMetadata.ColumnNames.PerformancePlanID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanNonJptTxMetadata.PropertyNames.PerformancePlanID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxMetadata.ColumnNames.YearPeriod, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxMetadata.PropertyNames.YearPeriod;
			c.CharacterMaxLength = 4;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxMetadata.ColumnNames.IsVerification, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanNonJptTxMetadata.PropertyNames.IsVerification;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxMetadata.ColumnNames.VerificationDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanNonJptTxMetadata.PropertyNames.VerificationDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxMetadata.ColumnNames.VerificationByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxMetadata.PropertyNames.VerificationByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxMetadata.ColumnNames.IsClosed, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanNonJptTxMetadata.PropertyNames.IsClosed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxMetadata.ColumnNames.ClosedDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanNonJptTxMetadata.PropertyNames.ClosedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxMetadata.ColumnNames.ClosedByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxMetadata.PropertyNames.ClosedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxMetadata.ColumnNames.CreatedDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanNonJptTxMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxMetadata.ColumnNames.CreatedByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanNonJptTxMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptTxMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptTxMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PerformancePlanNonJptTxMetadata Meta()
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
			public const string TxID = "TxID";
			public const string PersonID = "PersonID";
			public const string PerformancePlanID = "PerformancePlanID";
			public const string YearPeriod = "YearPeriod";
			public const string IsVerification = "IsVerification";
			public const string VerificationDateTime = "VerificationDateTime";
			public const string VerificationByUserID = "VerificationByUserID";
			public const string IsClosed = "IsClosed";
			public const string ClosedDateTime = "ClosedDateTime";
			public const string ClosedByUserID = "ClosedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TxID = "TxID";
			public const string PersonID = "PersonID";
			public const string PerformancePlanID = "PerformancePlanID";
			public const string YearPeriod = "YearPeriod";
			public const string IsVerification = "IsVerification";
			public const string VerificationDateTime = "VerificationDateTime";
			public const string VerificationByUserID = "VerificationByUserID";
			public const string IsClosed = "IsClosed";
			public const string ClosedDateTime = "ClosedDateTime";
			public const string ClosedByUserID = "ClosedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
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
			lock (typeof(PerformancePlanNonJptTxMetadata))
			{
				if (PerformancePlanNonJptTxMetadata.mapDelegates == null)
				{
					PerformancePlanNonJptTxMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PerformancePlanNonJptTxMetadata.meta == null)
				{
					PerformancePlanNonJptTxMetadata.meta = new PerformancePlanNonJptTxMetadata();
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

				meta.AddTypeMap("TxID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PerformancePlanID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("YearPeriod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVerification", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VerificationDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerificationByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ClosedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ClosedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PerformancePlanNonJptTx";
				meta.Destination = "PerformancePlanNonJptTx";
				meta.spInsert = "proc_PerformancePlanNonJptTxInsert";
				meta.spUpdate = "proc_PerformancePlanNonJptTxUpdate";
				meta.spDelete = "proc_PerformancePlanNonJptTxDelete";
				meta.spLoadAll = "proc_PerformancePlanNonJptTxLoadAll";
				meta.spLoadByPrimaryKey = "proc_PerformancePlanNonJptTxLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PerformancePlanNonJptTxMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
