/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/22/2023 1:21:04 PM
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
	abstract public class esCssdDecontaminationCollection : esEntityCollectionWAuditLog
	{
		public esCssdDecontaminationCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CssdDecontaminationCollection";
		}

		#region Query Logic
		protected void InitQuery(esCssdDecontaminationQuery query)
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
			this.InitQuery(query as esCssdDecontaminationQuery);
		}
		#endregion

		virtual public CssdDecontamination DetachEntity(CssdDecontamination entity)
		{
			return base.DetachEntity(entity) as CssdDecontamination;
		}

		virtual public CssdDecontamination AttachEntity(CssdDecontamination entity)
		{
			return base.AttachEntity(entity) as CssdDecontamination;
		}

		virtual public void Combine(CssdDecontaminationCollection collection)
		{
			base.Combine(collection);
		}

		new public CssdDecontamination this[int index]
		{
			get
			{
				return base[index] as CssdDecontamination;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CssdDecontamination);
		}
	}

	[Serializable]
	abstract public class esCssdDecontamination : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCssdDecontaminationQuery GetDynamicQuery()
		{
			return null;
		}

		public esCssdDecontamination()
		{
		}

		public esCssdDecontamination(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String decontaminationNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(decontaminationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(decontaminationNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String decontaminationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(decontaminationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(decontaminationNo);
		}

		private bool LoadByPrimaryKeyDynamic(String decontaminationNo)
		{
			esCssdDecontaminationQuery query = this.GetDynamicQuery();
			query.Where(query.DecontaminationNo == decontaminationNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String decontaminationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("DecontaminationNo", decontaminationNo);
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
						case "DecontaminationNo": this.str.DecontaminationNo = (string)value; break;
						case "DecontaminationDate": this.str.DecontaminationDate = (string)value; break;
						case "DecontaminationTime": this.str.DecontaminationTime = (string)value; break;
						case "SRDecontaminationPhase": this.str.SRDecontaminationPhase = (string)value; break;
						case "SRAbstersionType": this.str.SRAbstersionType = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
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
						case "DecontaminationDate":

							if (value == null || value is System.DateTime)
								this.DecontaminationDate = (System.DateTime?)value;
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
		/// Maps to CssdDecontamination.DecontaminationNo
		/// </summary>
		virtual public System.String DecontaminationNo
		{
			get
			{
				return base.GetSystemString(CssdDecontaminationMetadata.ColumnNames.DecontaminationNo);
			}

			set
			{
				base.SetSystemString(CssdDecontaminationMetadata.ColumnNames.DecontaminationNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdDecontamination.DecontaminationDate
		/// </summary>
		virtual public System.DateTime? DecontaminationDate
		{
			get
			{
				return base.GetSystemDateTime(CssdDecontaminationMetadata.ColumnNames.DecontaminationDate);
			}

			set
			{
				base.SetSystemDateTime(CssdDecontaminationMetadata.ColumnNames.DecontaminationDate, value);
			}
		}
		/// <summary>
		/// Maps to CssdDecontamination.DecontaminationTime
		/// </summary>
		virtual public System.String DecontaminationTime
		{
			get
			{
				return base.GetSystemString(CssdDecontaminationMetadata.ColumnNames.DecontaminationTime);
			}

			set
			{
				base.SetSystemString(CssdDecontaminationMetadata.ColumnNames.DecontaminationTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdDecontamination.SRDecontaminationPhase
		/// </summary>
		virtual public System.String SRDecontaminationPhase
		{
			get
			{
				return base.GetSystemString(CssdDecontaminationMetadata.ColumnNames.SRDecontaminationPhase);
			}

			set
			{
				base.SetSystemString(CssdDecontaminationMetadata.ColumnNames.SRDecontaminationPhase, value);
			}
		}
		/// <summary>
		/// Maps to CssdDecontamination.SRAbstersionType
		/// </summary>
		virtual public System.String SRAbstersionType
		{
			get
			{
				return base.GetSystemString(CssdDecontaminationMetadata.ColumnNames.SRAbstersionType);
			}

			set
			{
				base.SetSystemString(CssdDecontaminationMetadata.ColumnNames.SRAbstersionType, value);
			}
		}
		/// <summary>
		/// Maps to CssdDecontamination.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(CssdDecontaminationMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(CssdDecontaminationMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to CssdDecontamination.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(CssdDecontaminationMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(CssdDecontaminationMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to CssdDecontamination.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdDecontaminationMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdDecontaminationMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdDecontamination.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(CssdDecontaminationMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(CssdDecontaminationMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdDecontamination.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(CssdDecontaminationMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(CssdDecontaminationMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to CssdDecontamination.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdDecontaminationMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdDecontaminationMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdDecontamination.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(CssdDecontaminationMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(CssdDecontaminationMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdDecontamination.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdDecontaminationMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdDecontaminationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdDecontamination.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CssdDecontaminationMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CssdDecontaminationMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCssdDecontamination entity)
			{
				this.entity = entity;
			}
			public System.String DecontaminationNo
			{
				get
				{
					System.String data = entity.DecontaminationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DecontaminationNo = null;
					else entity.DecontaminationNo = Convert.ToString(value);
				}
			}
			public System.String DecontaminationDate
			{
				get
				{
					System.DateTime? data = entity.DecontaminationDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DecontaminationDate = null;
					else entity.DecontaminationDate = Convert.ToDateTime(value);
				}
			}
			public System.String DecontaminationTime
			{
				get
				{
					System.String data = entity.DecontaminationTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DecontaminationTime = null;
					else entity.DecontaminationTime = Convert.ToString(value);
				}
			}
			public System.String SRDecontaminationPhase
			{
				get
				{
					System.String data = entity.SRDecontaminationPhase;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDecontaminationPhase = null;
					else entity.SRDecontaminationPhase = Convert.ToString(value);
				}
			}
			public System.String SRAbstersionType
			{
				get
				{
					System.String data = entity.SRAbstersionType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAbstersionType = null;
					else entity.SRAbstersionType = Convert.ToString(value);
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
			private esCssdDecontamination entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCssdDecontaminationQuery query)
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
				throw new Exception("esCssdDecontamination can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CssdDecontamination : esCssdDecontamination
	{
	}

	[Serializable]
	abstract public class esCssdDecontaminationQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CssdDecontaminationMetadata.Meta();
			}
		}

		public esQueryItem DecontaminationNo
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationMetadata.ColumnNames.DecontaminationNo, esSystemType.String);
			}
		}

		public esQueryItem DecontaminationDate
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationMetadata.ColumnNames.DecontaminationDate, esSystemType.DateTime);
			}
		}

		public esQueryItem DecontaminationTime
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationMetadata.ColumnNames.DecontaminationTime, esSystemType.String);
			}
		}

		public esQueryItem SRDecontaminationPhase
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationMetadata.ColumnNames.SRDecontaminationPhase, esSystemType.String);
			}
		}

		public esQueryItem SRAbstersionType
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationMetadata.ColumnNames.SRAbstersionType, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CssdDecontaminationCollection")]
	public partial class CssdDecontaminationCollection : esCssdDecontaminationCollection, IEnumerable<CssdDecontamination>
	{
		public CssdDecontaminationCollection()
		{

		}

		public static implicit operator List<CssdDecontamination>(CssdDecontaminationCollection coll)
		{
			List<CssdDecontamination> list = new List<CssdDecontamination>();

			foreach (CssdDecontamination emp in coll)
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
				return CssdDecontaminationMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdDecontaminationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CssdDecontamination(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CssdDecontamination();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CssdDecontaminationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdDecontaminationQuery();
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
		public bool Load(CssdDecontaminationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CssdDecontamination AddNew()
		{
			CssdDecontamination entity = base.AddNewEntity() as CssdDecontamination;

			return entity;
		}
		public CssdDecontamination FindByPrimaryKey(String decontaminationNo)
		{
			return base.FindByPrimaryKey(decontaminationNo) as CssdDecontamination;
		}

		#region IEnumerable< CssdDecontamination> Members

		IEnumerator<CssdDecontamination> IEnumerable<CssdDecontamination>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CssdDecontamination;
			}
		}

		#endregion

		private CssdDecontaminationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CssdDecontamination' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CssdDecontamination ({DecontaminationNo})")]
	[Serializable]
	public partial class CssdDecontamination : esCssdDecontamination
	{
		public CssdDecontamination()
		{
		}

		public CssdDecontamination(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CssdDecontaminationMetadata.Meta();
			}
		}

		override protected esCssdDecontaminationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdDecontaminationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CssdDecontaminationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdDecontaminationQuery();
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
		public bool Load(CssdDecontaminationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CssdDecontaminationQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CssdDecontaminationQuery : esCssdDecontaminationQuery
	{
		public CssdDecontaminationQuery()
		{

		}

		public CssdDecontaminationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CssdDecontaminationQuery";
		}
	}

	[Serializable]
	public partial class CssdDecontaminationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CssdDecontaminationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CssdDecontaminationMetadata.ColumnNames.DecontaminationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDecontaminationMetadata.PropertyNames.DecontaminationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDecontaminationMetadata.ColumnNames.DecontaminationDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdDecontaminationMetadata.PropertyNames.DecontaminationDate;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDecontaminationMetadata.ColumnNames.DecontaminationTime, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDecontaminationMetadata.PropertyNames.DecontaminationTime;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDecontaminationMetadata.ColumnNames.SRDecontaminationPhase, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDecontaminationMetadata.PropertyNames.SRDecontaminationPhase;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDecontaminationMetadata.ColumnNames.SRAbstersionType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDecontaminationMetadata.PropertyNames.SRAbstersionType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDecontaminationMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDecontaminationMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDecontaminationMetadata.ColumnNames.IsApproved, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdDecontaminationMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDecontaminationMetadata.ColumnNames.ApprovedDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdDecontaminationMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDecontaminationMetadata.ColumnNames.ApprovedByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDecontaminationMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDecontaminationMetadata.ColumnNames.IsVoid, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdDecontaminationMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDecontaminationMetadata.ColumnNames.VoidDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdDecontaminationMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDecontaminationMetadata.ColumnNames.VoidByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDecontaminationMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDecontaminationMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdDecontaminationMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDecontaminationMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDecontaminationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CssdDecontaminationMetadata Meta()
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
			public const string DecontaminationNo = "DecontaminationNo";
			public const string DecontaminationDate = "DecontaminationDate";
			public const string DecontaminationTime = "DecontaminationTime";
			public const string SRDecontaminationPhase = "SRDecontaminationPhase";
			public const string SRAbstersionType = "SRAbstersionType";
			public const string Notes = "Notes";
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
			public const string DecontaminationNo = "DecontaminationNo";
			public const string DecontaminationDate = "DecontaminationDate";
			public const string DecontaminationTime = "DecontaminationTime";
			public const string SRDecontaminationPhase = "SRDecontaminationPhase";
			public const string SRAbstersionType = "SRAbstersionType";
			public const string Notes = "Notes";
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
			lock (typeof(CssdDecontaminationMetadata))
			{
				if (CssdDecontaminationMetadata.mapDelegates == null)
				{
					CssdDecontaminationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CssdDecontaminationMetadata.meta == null)
				{
					CssdDecontaminationMetadata.meta = new CssdDecontaminationMetadata();
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

				meta.AddTypeMap("DecontaminationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DecontaminationDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("DecontaminationTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDecontaminationPhase", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAbstersionType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CssdDecontamination";
				meta.Destination = "CssdDecontamination";
				meta.spInsert = "proc_CssdDecontaminationInsert";
				meta.spUpdate = "proc_CssdDecontaminationUpdate";
				meta.spDelete = "proc_CssdDecontaminationDelete";
				meta.spLoadAll = "proc_CssdDecontaminationLoadAll";
				meta.spLoadByPrimaryKey = "proc_CssdDecontaminationLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CssdDecontaminationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
