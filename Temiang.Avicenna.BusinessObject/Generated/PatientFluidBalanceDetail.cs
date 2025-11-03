/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/10/2022 11:39:50 AM
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
	abstract public class esPatientFluidBalanceDetailCollection : esEntityCollectionWAuditLog
	{
		public esPatientFluidBalanceDetailCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PatientFluidBalanceDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esPatientFluidBalanceDetailQuery query)
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
			this.InitQuery(query as esPatientFluidBalanceDetailQuery);
		}
		#endregion

		virtual public PatientFluidBalanceDetail DetachEntity(PatientFluidBalanceDetail entity)
		{
			return base.DetachEntity(entity) as PatientFluidBalanceDetail;
		}

		virtual public PatientFluidBalanceDetail AttachEntity(PatientFluidBalanceDetail entity)
		{
			return base.AttachEntity(entity) as PatientFluidBalanceDetail;
		}

		virtual public void Combine(PatientFluidBalanceDetailCollection collection)
		{
			base.Combine(collection);
		}

		new public PatientFluidBalanceDetail this[int index]
		{
			get
			{
				return base[index] as PatientFluidBalanceDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientFluidBalanceDetail);
		}
	}

	[Serializable]
	abstract public class esPatientFluidBalanceDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientFluidBalanceDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esPatientFluidBalanceDetail()
		{
		}

		public esPatientFluidBalanceDetail(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 sequenceNo, Int32 detailSequenceNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo, detailSequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo, detailSequenceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 sequenceNo, Int32 detailSequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo, detailSequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo, detailSequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 sequenceNo, Int32 detailSequenceNo)
		{
			esPatientFluidBalanceDetailQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.SequenceNo == sequenceNo, query.DetailSequenceNo == detailSequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 sequenceNo, Int32 detailSequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("SequenceNo", sequenceNo);
			parms.Add("DetailSequenceNo", detailSequenceNo);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "DetailSequenceNo": this.str.DetailSequenceNo = (string)value; break;
						case "InOutDateTime": this.str.InOutDateTime = (string)value; break;
						case "SRFluidInOutMethod": this.str.SRFluidInOutMethod = (string)value; break;
						case "FluidName": this.str.FluidName = (string)value; break;
						case "FluidQty": this.str.FluidQty = (string)value; break;
						case "InOutQty": this.str.InOutQty = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SchemaInfusNo": this.str.SchemaInfusNo = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "SequenceNo":

							if (value == null || value is System.Int32)
								this.SequenceNo = (System.Int32?)value;
							break;
						case "DetailSequenceNo":

							if (value == null || value is System.Int32)
								this.DetailSequenceNo = (System.Int32?)value;
							break;
						case "InOutDateTime":

							if (value == null || value is System.DateTime)
								this.InOutDateTime = (System.DateTime?)value;
							break;
						case "FluidQty":

							if (value == null || value is System.Decimal)
								this.FluidQty = (System.Decimal?)value;
							break;
						case "InOutQty":

							if (value == null || value is System.Decimal)
								this.InOutQty = (System.Decimal?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "SchemaInfusNo":

							if (value == null || value is System.Int32)
								this.SchemaInfusNo = (System.Int32?)value;
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
		/// Maps to PatientFluidBalanceDetail.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(PatientFluidBalanceDetailMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(PatientFluidBalanceDetailMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceDetail.SequenceNo
		/// </summary>
		virtual public System.Int32? SequenceNo
		{
			get
			{
				return base.GetSystemInt32(PatientFluidBalanceDetailMetadata.ColumnNames.SequenceNo);
			}

			set
			{
				base.SetSystemInt32(PatientFluidBalanceDetailMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceDetail.DetailSequenceNo
		/// </summary>
		virtual public System.Int32? DetailSequenceNo
		{
			get
			{
				return base.GetSystemInt32(PatientFluidBalanceDetailMetadata.ColumnNames.DetailSequenceNo);
			}

			set
			{
				base.SetSystemInt32(PatientFluidBalanceDetailMetadata.ColumnNames.DetailSequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceDetail.InOutDateTime
		/// </summary>
		virtual public System.DateTime? InOutDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientFluidBalanceDetailMetadata.ColumnNames.InOutDateTime);
			}

			set
			{
				base.SetSystemDateTime(PatientFluidBalanceDetailMetadata.ColumnNames.InOutDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceDetail.SRFluidInOutMethod
		/// </summary>
		virtual public System.String SRFluidInOutMethod
		{
			get
			{
				return base.GetSystemString(PatientFluidBalanceDetailMetadata.ColumnNames.SRFluidInOutMethod);
			}

			set
			{
				base.SetSystemString(PatientFluidBalanceDetailMetadata.ColumnNames.SRFluidInOutMethod, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceDetail.FluidName
		/// </summary>
		virtual public System.String FluidName
		{
			get
			{
				return base.GetSystemString(PatientFluidBalanceDetailMetadata.ColumnNames.FluidName);
			}

			set
			{
				base.SetSystemString(PatientFluidBalanceDetailMetadata.ColumnNames.FluidName, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceDetail.FluidQty
		/// </summary>
		virtual public System.Decimal? FluidQty
		{
			get
			{
				return base.GetSystemDecimal(PatientFluidBalanceDetailMetadata.ColumnNames.FluidQty);
			}

			set
			{
				base.SetSystemDecimal(PatientFluidBalanceDetailMetadata.ColumnNames.FluidQty, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceDetail.InOutQty
		/// </summary>
		virtual public System.Decimal? InOutQty
		{
			get
			{
				return base.GetSystemDecimal(PatientFluidBalanceDetailMetadata.ColumnNames.InOutQty);
			}

			set
			{
				base.SetSystemDecimal(PatientFluidBalanceDetailMetadata.ColumnNames.InOutQty, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceDetail.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(PatientFluidBalanceDetailMetadata.ColumnNames.Note);
			}

			set
			{
				base.SetSystemString(PatientFluidBalanceDetailMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceDetail.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PatientFluidBalanceDetailMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(PatientFluidBalanceDetailMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceDetail.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientFluidBalanceDetailMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PatientFluidBalanceDetailMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientFluidBalanceDetailMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PatientFluidBalanceDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientFluidBalanceDetailMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PatientFluidBalanceDetailMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientFluidBalanceDetail.SchemaInfusNo
		/// </summary>
		virtual public System.Int32? SchemaInfusNo
		{
			get
			{
				return base.GetSystemInt32(PatientFluidBalanceDetailMetadata.ColumnNames.SchemaInfusNo);
			}

			set
			{
				base.SetSystemInt32(PatientFluidBalanceDetailMetadata.ColumnNames.SchemaInfusNo, value);
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
			public esStrings(esPatientFluidBalanceDetail entity)
			{
				this.entity = entity;
			}
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
			public System.String SequenceNo
			{
				get
				{
					System.Int32? data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToInt32(value);
				}
			}
			public System.String DetailSequenceNo
			{
				get
				{
					System.Int32? data = entity.DetailSequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DetailSequenceNo = null;
					else entity.DetailSequenceNo = Convert.ToInt32(value);
				}
			}
			public System.String InOutDateTime
			{
				get
				{
					System.DateTime? data = entity.InOutDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InOutDateTime = null;
					else entity.InOutDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String SRFluidInOutMethod
			{
				get
				{
					System.String data = entity.SRFluidInOutMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRFluidInOutMethod = null;
					else entity.SRFluidInOutMethod = Convert.ToString(value);
				}
			}
			public System.String FluidName
			{
				get
				{
					System.String data = entity.FluidName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FluidName = null;
					else entity.FluidName = Convert.ToString(value);
				}
			}
			public System.String FluidQty
			{
				get
				{
					System.Decimal? data = entity.FluidQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FluidQty = null;
					else entity.FluidQty = Convert.ToDecimal(value);
				}
			}
			public System.String InOutQty
			{
				get
				{
					System.Decimal? data = entity.InOutQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InOutQty = null;
					else entity.InOutQty = Convert.ToDecimal(value);
				}
			}
			public System.String Note
			{
				get
				{
					System.String data = entity.Note;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Note = null;
					else entity.Note = Convert.ToString(value);
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
			public System.String SchemaInfusNo
			{
				get
				{
					System.Int32? data = entity.SchemaInfusNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SchemaInfusNo = null;
					else entity.SchemaInfusNo = Convert.ToInt32(value);
				}
			}
			private esPatientFluidBalanceDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientFluidBalanceDetailQuery query)
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
				throw new Exception("esPatientFluidBalanceDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientFluidBalanceDetail : esPatientFluidBalanceDetail
	{
	}

	[Serializable]
	abstract public class esPatientFluidBalanceDetailQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PatientFluidBalanceDetailMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceDetailMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceDetailMetadata.ColumnNames.SequenceNo, esSystemType.Int32);
			}
		}

		public esQueryItem DetailSequenceNo
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceDetailMetadata.ColumnNames.DetailSequenceNo, esSystemType.Int32);
			}
		}

		public esQueryItem InOutDateTime
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceDetailMetadata.ColumnNames.InOutDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem SRFluidInOutMethod
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceDetailMetadata.ColumnNames.SRFluidInOutMethod, esSystemType.String);
			}
		}

		public esQueryItem FluidName
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceDetailMetadata.ColumnNames.FluidName, esSystemType.String);
			}
		}

		public esQueryItem FluidQty
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceDetailMetadata.ColumnNames.FluidQty, esSystemType.Decimal);
			}
		}

		public esQueryItem InOutQty
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceDetailMetadata.ColumnNames.InOutQty, esSystemType.Decimal);
			}
		}

		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceDetailMetadata.ColumnNames.Note, esSystemType.String);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceDetailMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceDetailMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SchemaInfusNo
		{
			get
			{
				return new esQueryItem(this, PatientFluidBalanceDetailMetadata.ColumnNames.SchemaInfusNo, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientFluidBalanceDetailCollection")]
	public partial class PatientFluidBalanceDetailCollection : esPatientFluidBalanceDetailCollection, IEnumerable<PatientFluidBalanceDetail>
	{
		public PatientFluidBalanceDetailCollection()
		{

		}

		public static implicit operator List<PatientFluidBalanceDetail>(PatientFluidBalanceDetailCollection coll)
		{
			List<PatientFluidBalanceDetail> list = new List<PatientFluidBalanceDetail>();

			foreach (PatientFluidBalanceDetail emp in coll)
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
				return PatientFluidBalanceDetailMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientFluidBalanceDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientFluidBalanceDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientFluidBalanceDetail();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PatientFluidBalanceDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientFluidBalanceDetailQuery();
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
		public bool Load(PatientFluidBalanceDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientFluidBalanceDetail AddNew()
		{
			PatientFluidBalanceDetail entity = base.AddNewEntity() as PatientFluidBalanceDetail;

			return entity;
		}
		public PatientFluidBalanceDetail FindByPrimaryKey(String registrationNo, Int32 sequenceNo, Int32 detailSequenceNo)
		{
			return base.FindByPrimaryKey(registrationNo, sequenceNo, detailSequenceNo) as PatientFluidBalanceDetail;
		}

		#region IEnumerable< PatientFluidBalanceDetail> Members

		IEnumerator<PatientFluidBalanceDetail> IEnumerable<PatientFluidBalanceDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PatientFluidBalanceDetail;
			}
		}

		#endregion

		private PatientFluidBalanceDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientFluidBalanceDetail' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientFluidBalanceDetail ({RegistrationNo, SequenceNo, DetailSequenceNo})")]
	[Serializable]
	public partial class PatientFluidBalanceDetail : esPatientFluidBalanceDetail
	{
		public PatientFluidBalanceDetail()
		{
		}

		public PatientFluidBalanceDetail(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientFluidBalanceDetailMetadata.Meta();
			}
		}

		override protected esPatientFluidBalanceDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientFluidBalanceDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PatientFluidBalanceDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientFluidBalanceDetailQuery();
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
		public bool Load(PatientFluidBalanceDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PatientFluidBalanceDetailQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientFluidBalanceDetailQuery : esPatientFluidBalanceDetailQuery
	{
		public PatientFluidBalanceDetailQuery()
		{

		}

		public PatientFluidBalanceDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PatientFluidBalanceDetailQuery";
		}
	}

	[Serializable]
	public partial class PatientFluidBalanceDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientFluidBalanceDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PatientFluidBalanceDetailMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientFluidBalanceDetailMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PatientFluidBalanceDetailMetadata.ColumnNames.SequenceNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientFluidBalanceDetailMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PatientFluidBalanceDetailMetadata.ColumnNames.DetailSequenceNo, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientFluidBalanceDetailMetadata.PropertyNames.DetailSequenceNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PatientFluidBalanceDetailMetadata.ColumnNames.InOutDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientFluidBalanceDetailMetadata.PropertyNames.InOutDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(PatientFluidBalanceDetailMetadata.ColumnNames.SRFluidInOutMethod, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientFluidBalanceDetailMetadata.PropertyNames.SRFluidInOutMethod;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(PatientFluidBalanceDetailMetadata.ColumnNames.FluidName, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientFluidBalanceDetailMetadata.PropertyNames.FluidName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(PatientFluidBalanceDetailMetadata.ColumnNames.FluidQty, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PatientFluidBalanceDetailMetadata.PropertyNames.FluidQty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientFluidBalanceDetailMetadata.ColumnNames.InOutQty, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PatientFluidBalanceDetailMetadata.PropertyNames.InOutQty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientFluidBalanceDetailMetadata.ColumnNames.Note, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientFluidBalanceDetailMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientFluidBalanceDetailMetadata.ColumnNames.CreatedByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientFluidBalanceDetailMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(PatientFluidBalanceDetailMetadata.ColumnNames.CreatedDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientFluidBalanceDetailMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientFluidBalanceDetailMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientFluidBalanceDetailMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientFluidBalanceDetailMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientFluidBalanceDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientFluidBalanceDetailMetadata.ColumnNames.SchemaInfusNo, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientFluidBalanceDetailMetadata.PropertyNames.SchemaInfusNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PatientFluidBalanceDetailMetadata Meta()
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
			public const string RegistrationNo = "RegistrationNo";
			public const string SequenceNo = "SequenceNo";
			public const string DetailSequenceNo = "DetailSequenceNo";
			public const string InOutDateTime = "InOutDateTime";
			public const string SRFluidInOutMethod = "SRFluidInOutMethod";
			public const string FluidName = "FluidName";
			public const string FluidQty = "FluidQty";
			public const string InOutQty = "InOutQty";
			public const string Note = "Note";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SchemaInfusNo = "SchemaInfusNo";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string SequenceNo = "SequenceNo";
			public const string DetailSequenceNo = "DetailSequenceNo";
			public const string InOutDateTime = "InOutDateTime";
			public const string SRFluidInOutMethod = "SRFluidInOutMethod";
			public const string FluidName = "FluidName";
			public const string FluidQty = "FluidQty";
			public const string InOutQty = "InOutQty";
			public const string Note = "Note";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SchemaInfusNo = "SchemaInfusNo";
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
			lock (typeof(PatientFluidBalanceDetailMetadata))
			{
				if (PatientFluidBalanceDetailMetadata.mapDelegates == null)
				{
					PatientFluidBalanceDetailMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PatientFluidBalanceDetailMetadata.meta == null)
				{
					PatientFluidBalanceDetailMetadata.meta = new PatientFluidBalanceDetailMetadata();
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

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DetailSequenceNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("InOutDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRFluidInOutMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FluidName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FluidQty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("InOutQty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SchemaInfusNo", new esTypeMap("int", "System.Int32"));


				meta.Source = "PatientFluidBalanceDetail";
				meta.Destination = "PatientFluidBalanceDetail";
				meta.spInsert = "proc_PatientFluidBalanceDetailInsert";
				meta.spUpdate = "proc_PatientFluidBalanceDetailUpdate";
				meta.spDelete = "proc_PatientFluidBalanceDetailDelete";
				meta.spLoadAll = "proc_PatientFluidBalanceDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientFluidBalanceDetailLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientFluidBalanceDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
