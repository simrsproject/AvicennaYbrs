/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/5/2021 4:29:49 PM
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
	abstract public class esCssdSterileItemsRequestCollection : esEntityCollectionWAuditLog
	{
		public esCssdSterileItemsRequestCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CssdSterileItemsRequestCollection";
		}

		#region Query Logic
		protected void InitQuery(esCssdSterileItemsRequestQuery query)
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
			this.InitQuery(query as esCssdSterileItemsRequestQuery);
		}
		#endregion

		virtual public CssdSterileItemsRequest DetachEntity(CssdSterileItemsRequest entity)
		{
			return base.DetachEntity(entity) as CssdSterileItemsRequest;
		}

		virtual public CssdSterileItemsRequest AttachEntity(CssdSterileItemsRequest entity)
		{
			return base.AttachEntity(entity) as CssdSterileItemsRequest;
		}

		virtual public void Combine(CssdSterileItemsRequestCollection collection)
		{
			base.Combine(collection);
		}

		new public CssdSterileItemsRequest this[int index]
		{
			get
			{
				return base[index] as CssdSterileItemsRequest;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CssdSterileItemsRequest);
		}
	}

	[Serializable]
	abstract public class esCssdSterileItemsRequest : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCssdSterileItemsRequestQuery GetDynamicQuery()
		{
			return null;
		}

		public esCssdSterileItemsRequest()
		{
		}

		public esCssdSterileItemsRequest(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String requestNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(requestNo);
			else
				return LoadByPrimaryKeyStoredProcedure(requestNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String requestNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(requestNo);
			else
				return LoadByPrimaryKeyStoredProcedure(requestNo);
		}

		private bool LoadByPrimaryKeyDynamic(String requestNo)
		{
			esCssdSterileItemsRequestQuery query = this.GetDynamicQuery();
			query.Where(query.RequestNo == requestNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String requestNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RequestNo", requestNo);
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
						case "RequestNo": this.str.RequestNo = (string)value; break;
						case "RequestDate": this.str.RequestDate = (string)value; break;
						case "FromServiceUnitID": this.str.FromServiceUnitID = (string)value; break;
						case "FromRoomID": this.str.FromRoomID = (string)value; break;
						case "SenderByID": this.str.SenderByID = (string)value; break;
						case "SenderBy": this.str.SenderBy = (string)value; break;
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
						case "RequestDate":

							if (value == null || value is System.DateTime)
								this.RequestDate = (System.DateTime?)value;
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
		/// Maps to CssdSterileItemsRequest.RequestNo
		/// </summary>
		virtual public System.String RequestNo
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsRequestMetadata.ColumnNames.RequestNo);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsRequestMetadata.ColumnNames.RequestNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequest.RequestDate
		/// </summary>
		virtual public System.DateTime? RequestDate
		{
			get
			{
				return base.GetSystemDateTime(CssdSterileItemsRequestMetadata.ColumnNames.RequestDate);
			}

			set
			{
				base.SetSystemDateTime(CssdSterileItemsRequestMetadata.ColumnNames.RequestDate, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequest.FromServiceUnitID
		/// </summary>
		virtual public System.String FromServiceUnitID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsRequestMetadata.ColumnNames.FromServiceUnitID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsRequestMetadata.ColumnNames.FromServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequest.FromRoomID
		/// </summary>
		virtual public System.String FromRoomID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsRequestMetadata.ColumnNames.FromRoomID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsRequestMetadata.ColumnNames.FromRoomID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequest.SenderByID
		/// </summary>
		virtual public System.String SenderByID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsRequestMetadata.ColumnNames.SenderByID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsRequestMetadata.ColumnNames.SenderByID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequest.SenderBy
		/// </summary>
		virtual public System.String SenderBy
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsRequestMetadata.ColumnNames.SenderBy);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsRequestMetadata.ColumnNames.SenderBy, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequest.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsRequestMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsRequestMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequest.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdSterileItemsRequestMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdSterileItemsRequestMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequest.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsRequestMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsRequestMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequest.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(CssdSterileItemsRequestMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(CssdSterileItemsRequestMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequest.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdSterileItemsRequestMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdSterileItemsRequestMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequest.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsRequestMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsRequestMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequest.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdSterileItemsRequestMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdSterileItemsRequestMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterileItemsRequest.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterileItemsRequestMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterileItemsRequestMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCssdSterileItemsRequest entity)
			{
				this.entity = entity;
			}
			public System.String RequestNo
			{
				get
				{
					System.String data = entity.RequestNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestNo = null;
					else entity.RequestNo = Convert.ToString(value);
				}
			}
			public System.String RequestDate
			{
				get
				{
					System.DateTime? data = entity.RequestDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestDate = null;
					else entity.RequestDate = Convert.ToDateTime(value);
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
			private esCssdSterileItemsRequest entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCssdSterileItemsRequestQuery query)
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
				throw new Exception("esCssdSterileItemsRequest can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CssdSterileItemsRequest : esCssdSterileItemsRequest
	{
	}

	[Serializable]
	abstract public class esCssdSterileItemsRequestQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CssdSterileItemsRequestMetadata.Meta();
			}
		}

		public esQueryItem RequestNo
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestMetadata.ColumnNames.RequestNo, esSystemType.String);
			}
		}

		public esQueryItem RequestDate
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestMetadata.ColumnNames.RequestDate, esSystemType.DateTime);
			}
		}

		public esQueryItem FromServiceUnitID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestMetadata.ColumnNames.FromServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem FromRoomID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestMetadata.ColumnNames.FromRoomID, esSystemType.String);
			}
		}

		public esQueryItem SenderByID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestMetadata.ColumnNames.SenderByID, esSystemType.String);
			}
		}

		public esQueryItem SenderBy
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestMetadata.ColumnNames.SenderBy, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterileItemsRequestMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CssdSterileItemsRequestCollection")]
	public partial class CssdSterileItemsRequestCollection : esCssdSterileItemsRequestCollection, IEnumerable<CssdSterileItemsRequest>
	{
		public CssdSterileItemsRequestCollection()
		{

		}

		public static implicit operator List<CssdSterileItemsRequest>(CssdSterileItemsRequestCollection coll)
		{
			List<CssdSterileItemsRequest> list = new List<CssdSterileItemsRequest>();

			foreach (CssdSterileItemsRequest emp in coll)
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
				return CssdSterileItemsRequestMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdSterileItemsRequestQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CssdSterileItemsRequest(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CssdSterileItemsRequest();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CssdSterileItemsRequestQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdSterileItemsRequestQuery();
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
		public bool Load(CssdSterileItemsRequestQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CssdSterileItemsRequest AddNew()
		{
			CssdSterileItemsRequest entity = base.AddNewEntity() as CssdSterileItemsRequest;

			return entity;
		}
		public CssdSterileItemsRequest FindByPrimaryKey(String requestNo)
		{
			return base.FindByPrimaryKey(requestNo) as CssdSterileItemsRequest;
		}

		#region IEnumerable< CssdSterileItemsRequest> Members

		IEnumerator<CssdSterileItemsRequest> IEnumerable<CssdSterileItemsRequest>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CssdSterileItemsRequest;
			}
		}

		#endregion

		private CssdSterileItemsRequestQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CssdSterileItemsRequest' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CssdSterileItemsRequest ({RequestNo})")]
	[Serializable]
	public partial class CssdSterileItemsRequest : esCssdSterileItemsRequest
	{
		public CssdSterileItemsRequest()
		{
		}

		public CssdSterileItemsRequest(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CssdSterileItemsRequestMetadata.Meta();
			}
		}

		override protected esCssdSterileItemsRequestQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdSterileItemsRequestQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CssdSterileItemsRequestQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdSterileItemsRequestQuery();
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
		public bool Load(CssdSterileItemsRequestQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CssdSterileItemsRequestQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CssdSterileItemsRequestQuery : esCssdSterileItemsRequestQuery
	{
		public CssdSterileItemsRequestQuery()
		{

		}

		public CssdSterileItemsRequestQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CssdSterileItemsRequestQuery";
		}
	}

	[Serializable]
	public partial class CssdSterileItemsRequestMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CssdSterileItemsRequestMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CssdSterileItemsRequestMetadata.ColumnNames.RequestNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsRequestMetadata.PropertyNames.RequestNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestMetadata.ColumnNames.RequestDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterileItemsRequestMetadata.PropertyNames.RequestDate;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestMetadata.ColumnNames.FromServiceUnitID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsRequestMetadata.PropertyNames.FromServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestMetadata.ColumnNames.FromRoomID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsRequestMetadata.PropertyNames.FromRoomID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestMetadata.ColumnNames.SenderByID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsRequestMetadata.PropertyNames.SenderByID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestMetadata.ColumnNames.SenderBy, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsRequestMetadata.PropertyNames.SenderBy;
			c.CharacterMaxLength = 250;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestMetadata.ColumnNames.IsApproved, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsRequestMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestMetadata.ColumnNames.ApprovedDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterileItemsRequestMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestMetadata.ColumnNames.ApprovedByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsRequestMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestMetadata.ColumnNames.IsVoid, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterileItemsRequestMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestMetadata.ColumnNames.VoidDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterileItemsRequestMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestMetadata.ColumnNames.VoidByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsRequestMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterileItemsRequestMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterileItemsRequestMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterileItemsRequestMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CssdSterileItemsRequestMetadata Meta()
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
			public const string RequestNo = "RequestNo";
			public const string RequestDate = "RequestDate";
			public const string FromServiceUnitID = "FromServiceUnitID";
			public const string FromRoomID = "FromRoomID";
			public const string SenderByID = "SenderByID";
			public const string SenderBy = "SenderBy";
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
			public const string RequestNo = "RequestNo";
			public const string RequestDate = "RequestDate";
			public const string FromServiceUnitID = "FromServiceUnitID";
			public const string FromRoomID = "FromRoomID";
			public const string SenderByID = "SenderByID";
			public const string SenderBy = "SenderBy";
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
			lock (typeof(CssdSterileItemsRequestMetadata))
			{
				if (CssdSterileItemsRequestMetadata.mapDelegates == null)
				{
					CssdSterileItemsRequestMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CssdSterileItemsRequestMetadata.meta == null)
				{
					CssdSterileItemsRequestMetadata.meta = new CssdSterileItemsRequestMetadata();
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

				meta.AddTypeMap("RequestNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RequestDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("FromServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromRoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SenderByID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SenderBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CssdSterileItemsRequest";
				meta.Destination = "CssdSterileItemsRequest";
				meta.spInsert = "proc_CssdSterileItemsRequestInsert";
				meta.spUpdate = "proc_CssdSterileItemsRequestUpdate";
				meta.spDelete = "proc_CssdSterileItemsRequestDelete";
				meta.spLoadAll = "proc_CssdSterileItemsRequestLoadAll";
				meta.spLoadByPrimaryKey = "proc_CssdSterileItemsRequestLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CssdSterileItemsRequestMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
