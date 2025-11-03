/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/31/2022 11:05:13 PM
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
	abstract public class esAppReportPivotCollection : esEntityCollectionWAuditLog
	{
		public esAppReportPivotCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AppReportPivotCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppReportPivotQuery query)
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
			this.InitQuery(query as esAppReportPivotQuery);
		}
		#endregion

		virtual public AppReportPivot DetachEntity(AppReportPivot entity)
		{
			return base.DetachEntity(entity) as AppReportPivot;
		}

		virtual public AppReportPivot AttachEntity(AppReportPivot entity)
		{
			return base.AttachEntity(entity) as AppReportPivot;
		}

		virtual public void Combine(AppReportPivotCollection collection)
		{
			base.Combine(collection);
		}

		new public AppReportPivot this[int index]
		{
			get
			{
				return base[index] as AppReportPivot;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppReportPivot);
		}
	}

	[Serializable]
	abstract public class esAppReportPivot : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppReportPivotQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppReportPivot()
		{
		}

		public esAppReportPivot(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String programID, Int32 customPivotID, String fieldCaption)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(programID, customPivotID, fieldCaption);
			else
				return LoadByPrimaryKeyStoredProcedure(programID, customPivotID, fieldCaption);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String programID, Int32 customPivotID, String fieldCaption)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(programID, customPivotID, fieldCaption);
			else
				return LoadByPrimaryKeyStoredProcedure(programID, customPivotID, fieldCaption);
		}

		private bool LoadByPrimaryKeyDynamic(String programID, Int32 customPivotID, String fieldCaption)
		{
			esAppReportPivotQuery query = this.GetDynamicQuery();
			query.Where(query.ProgramID == programID, query.CustomPivotID == customPivotID, query.FieldCaption == fieldCaption);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String programID, Int32 customPivotID, String fieldCaption)
		{
			esParameters parms = new esParameters();
			parms.Add("ProgramID", programID);
			parms.Add("CustomPivotID", customPivotID);
			parms.Add("FieldCaption", fieldCaption);
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
						case "ProgramID": this.str.ProgramID = (string)value; break;
						case "CustomPivotID": this.str.CustomPivotID = (string)value; break;
						case "FieldCaption": this.str.FieldCaption = (string)value; break;
						case "UserID": this.str.UserID = (string)value; break;
						case "PivotArea": this.str.PivotArea = (string)value; break;
						case "IndexNo": this.str.IndexNo = (string)value; break;
						case "FieldName": this.str.FieldName = (string)value; break;
						case "GroupInterval": this.str.GroupInterval = (string)value; break;
						case "SummaryType": this.str.SummaryType = (string)value; break;
						case "FormatType": this.str.FormatType = (string)value; break;
						case "FormatString": this.str.FormatString = (string)value; break;
						case "GroupIndex": this.str.GroupIndex = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "CustomPivotID":

							if (value == null || value is System.Int32)
								this.CustomPivotID = (System.Int32?)value;
							break;
						case "PivotArea":

							if (value == null || value is System.Int32)
								this.PivotArea = (System.Int32?)value;
							break;
						case "IndexNo":

							if (value == null || value is System.Int32)
								this.IndexNo = (System.Int32?)value;
							break;
						case "GroupInterval":

							if (value == null || value is System.Int32)
								this.GroupInterval = (System.Int32?)value;
							break;
						case "SummaryType":

							if (value == null || value is System.Int32)
								this.SummaryType = (System.Int32?)value;
							break;
						case "FormatType":

							if (value == null || value is System.Int32)
								this.FormatType = (System.Int32?)value;
							break;
						case "GroupIndex":

							if (value == null || value is System.Int32)
								this.GroupIndex = (System.Int32?)value;
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
		/// Maps to AppReportPivot.ProgramID
		/// </summary>
		virtual public System.String ProgramID
		{
			get
			{
				return base.GetSystemString(AppReportPivotMetadata.ColumnNames.ProgramID);
			}

			set
			{
				base.SetSystemString(AppReportPivotMetadata.ColumnNames.ProgramID, value);
			}
		}
		/// <summary>
		/// Maps to AppReportPivot.CustomPivotID
		/// </summary>
		virtual public System.Int32? CustomPivotID
		{
			get
			{
				return base.GetSystemInt32(AppReportPivotMetadata.ColumnNames.CustomPivotID);
			}

			set
			{
				base.SetSystemInt32(AppReportPivotMetadata.ColumnNames.CustomPivotID, value);
			}
		}
		/// <summary>
		/// Maps to AppReportPivot.FieldCaption
		/// </summary>
		virtual public System.String FieldCaption
		{
			get
			{
				return base.GetSystemString(AppReportPivotMetadata.ColumnNames.FieldCaption);
			}

			set
			{
				base.SetSystemString(AppReportPivotMetadata.ColumnNames.FieldCaption, value);
			}
		}
		/// <summary>
		/// Maps to AppReportPivot.UserID
		/// </summary>
		virtual public System.String UserID
		{
			get
			{
				return base.GetSystemString(AppReportPivotMetadata.ColumnNames.UserID);
			}

			set
			{
				base.SetSystemString(AppReportPivotMetadata.ColumnNames.UserID, value);
			}
		}
		/// <summary>
		/// Maps to AppReportPivot.PivotArea
		/// </summary>
		virtual public System.Int32? PivotArea
		{
			get
			{
				return base.GetSystemInt32(AppReportPivotMetadata.ColumnNames.PivotArea);
			}

			set
			{
				base.SetSystemInt32(AppReportPivotMetadata.ColumnNames.PivotArea, value);
			}
		}
		/// <summary>
		/// Maps to AppReportPivot.IndexNo
		/// </summary>
		virtual public System.Int32? IndexNo
		{
			get
			{
				return base.GetSystemInt32(AppReportPivotMetadata.ColumnNames.IndexNo);
			}

			set
			{
				base.SetSystemInt32(AppReportPivotMetadata.ColumnNames.IndexNo, value);
			}
		}
		/// <summary>
		/// Maps to AppReportPivot.FieldName
		/// </summary>
		virtual public System.String FieldName
		{
			get
			{
				return base.GetSystemString(AppReportPivotMetadata.ColumnNames.FieldName);
			}

			set
			{
				base.SetSystemString(AppReportPivotMetadata.ColumnNames.FieldName, value);
			}
		}
		/// <summary>
		/// Maps to AppReportPivot.GroupInterval
		/// </summary>
		virtual public System.Int32? GroupInterval
		{
			get
			{
				return base.GetSystemInt32(AppReportPivotMetadata.ColumnNames.GroupInterval);
			}

			set
			{
				base.SetSystemInt32(AppReportPivotMetadata.ColumnNames.GroupInterval, value);
			}
		}
		/// <summary>
		/// Maps to AppReportPivot.SummaryType
		/// </summary>
		virtual public System.Int32? SummaryType
		{
			get
			{
				return base.GetSystemInt32(AppReportPivotMetadata.ColumnNames.SummaryType);
			}

			set
			{
				base.SetSystemInt32(AppReportPivotMetadata.ColumnNames.SummaryType, value);
			}
		}
		/// <summary>
		/// Maps to AppReportPivot.FormatType
		/// </summary>
		virtual public System.Int32? FormatType
		{
			get
			{
				return base.GetSystemInt32(AppReportPivotMetadata.ColumnNames.FormatType);
			}

			set
			{
				base.SetSystemInt32(AppReportPivotMetadata.ColumnNames.FormatType, value);
			}
		}
		/// <summary>
		/// Maps to AppReportPivot.FormatString
		/// </summary>
		virtual public System.String FormatString
		{
			get
			{
				return base.GetSystemString(AppReportPivotMetadata.ColumnNames.FormatString);
			}

			set
			{
				base.SetSystemString(AppReportPivotMetadata.ColumnNames.FormatString, value);
			}
		}
		/// <summary>
		/// Maps to AppReportPivot.GroupIndex
		/// </summary>
		virtual public System.Int32? GroupIndex
		{
			get
			{
				return base.GetSystemInt32(AppReportPivotMetadata.ColumnNames.GroupIndex);
			}

			set
			{
				base.SetSystemInt32(AppReportPivotMetadata.ColumnNames.GroupIndex, value);
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
			public esStrings(esAppReportPivot entity)
			{
				this.entity = entity;
			}
			public System.String ProgramID
			{
				get
				{
					System.String data = entity.ProgramID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProgramID = null;
					else entity.ProgramID = Convert.ToString(value);
				}
			}
			public System.String CustomPivotID
			{
				get
				{
					System.Int32? data = entity.CustomPivotID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CustomPivotID = null;
					else entity.CustomPivotID = Convert.ToInt32(value);
				}
			}
			public System.String FieldCaption
			{
				get
				{
					System.String data = entity.FieldCaption;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FieldCaption = null;
					else entity.FieldCaption = Convert.ToString(value);
				}
			}
			public System.String UserID
			{
				get
				{
					System.String data = entity.UserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UserID = null;
					else entity.UserID = Convert.ToString(value);
				}
			}
			public System.String PivotArea
			{
				get
				{
					System.Int32? data = entity.PivotArea;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PivotArea = null;
					else entity.PivotArea = Convert.ToInt32(value);
				}
			}
			public System.String IndexNo
			{
				get
				{
					System.Int32? data = entity.IndexNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IndexNo = null;
					else entity.IndexNo = Convert.ToInt32(value);
				}
			}
			public System.String FieldName
			{
				get
				{
					System.String data = entity.FieldName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FieldName = null;
					else entity.FieldName = Convert.ToString(value);
				}
			}
			public System.String GroupInterval
			{
				get
				{
					System.Int32? data = entity.GroupInterval;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GroupInterval = null;
					else entity.GroupInterval = Convert.ToInt32(value);
				}
			}
			public System.String SummaryType
			{
				get
				{
					System.Int32? data = entity.SummaryType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SummaryType = null;
					else entity.SummaryType = Convert.ToInt32(value);
				}
			}
			public System.String FormatType
			{
				get
				{
					System.Int32? data = entity.FormatType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FormatType = null;
					else entity.FormatType = Convert.ToInt32(value);
				}
			}
			public System.String FormatString
			{
				get
				{
					System.String data = entity.FormatString;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FormatString = null;
					else entity.FormatString = Convert.ToString(value);
				}
			}
			public System.String GroupIndex
			{
				get
				{
					System.Int32? data = entity.GroupIndex;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GroupIndex = null;
					else entity.GroupIndex = Convert.ToInt32(value);
				}
			}
			private esAppReportPivot entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppReportPivotQuery query)
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
				throw new Exception("esAppReportPivot can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppReportPivot : esAppReportPivot
	{
	}

	[Serializable]
	abstract public class esAppReportPivotQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AppReportPivotMetadata.Meta();
			}
		}

		public esQueryItem ProgramID
		{
			get
			{
				return new esQueryItem(this, AppReportPivotMetadata.ColumnNames.ProgramID, esSystemType.String);
			}
		}

		public esQueryItem CustomPivotID
		{
			get
			{
				return new esQueryItem(this, AppReportPivotMetadata.ColumnNames.CustomPivotID, esSystemType.Int32);
			}
		}

		public esQueryItem FieldCaption
		{
			get
			{
				return new esQueryItem(this, AppReportPivotMetadata.ColumnNames.FieldCaption, esSystemType.String);
			}
		}

		public esQueryItem UserID
		{
			get
			{
				return new esQueryItem(this, AppReportPivotMetadata.ColumnNames.UserID, esSystemType.String);
			}
		}

		public esQueryItem PivotArea
		{
			get
			{
				return new esQueryItem(this, AppReportPivotMetadata.ColumnNames.PivotArea, esSystemType.Int32);
			}
		}

		public esQueryItem IndexNo
		{
			get
			{
				return new esQueryItem(this, AppReportPivotMetadata.ColumnNames.IndexNo, esSystemType.Int32);
			}
		}

		public esQueryItem FieldName
		{
			get
			{
				return new esQueryItem(this, AppReportPivotMetadata.ColumnNames.FieldName, esSystemType.String);
			}
		}

		public esQueryItem GroupInterval
		{
			get
			{
				return new esQueryItem(this, AppReportPivotMetadata.ColumnNames.GroupInterval, esSystemType.Int32);
			}
		}

		public esQueryItem SummaryType
		{
			get
			{
				return new esQueryItem(this, AppReportPivotMetadata.ColumnNames.SummaryType, esSystemType.Int32);
			}
		}

		public esQueryItem FormatType
		{
			get
			{
				return new esQueryItem(this, AppReportPivotMetadata.ColumnNames.FormatType, esSystemType.Int32);
			}
		}

		public esQueryItem FormatString
		{
			get
			{
				return new esQueryItem(this, AppReportPivotMetadata.ColumnNames.FormatString, esSystemType.String);
			}
		}

		public esQueryItem GroupIndex
		{
			get
			{
				return new esQueryItem(this, AppReportPivotMetadata.ColumnNames.GroupIndex, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppReportPivotCollection")]
	public partial class AppReportPivotCollection : esAppReportPivotCollection, IEnumerable<AppReportPivot>
	{
		public AppReportPivotCollection()
		{

		}

		public static implicit operator List<AppReportPivot>(AppReportPivotCollection coll)
		{
			List<AppReportPivot> list = new List<AppReportPivot>();

			foreach (AppReportPivot emp in coll)
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
				return AppReportPivotMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppReportPivotQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppReportPivot(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppReportPivot();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AppReportPivotQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppReportPivotQuery();
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
		public bool Load(AppReportPivotQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppReportPivot AddNew()
		{
			AppReportPivot entity = base.AddNewEntity() as AppReportPivot;

			return entity;
		}
		public AppReportPivot FindByPrimaryKey(String programID, Int32 customPivotID, String fieldCaption)
		{
			return base.FindByPrimaryKey(programID, customPivotID, fieldCaption) as AppReportPivot;
		}

		#region IEnumerable< AppReportPivot> Members

		IEnumerator<AppReportPivot> IEnumerable<AppReportPivot>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AppReportPivot;
			}
		}

		#endregion

		private AppReportPivotQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppReportPivot' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppReportPivot ({ProgramID, CustomPivotID, FieldCaption})")]
	[Serializable]
	public partial class AppReportPivot : esAppReportPivot
	{
		public AppReportPivot()
		{
		}

		public AppReportPivot(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppReportPivotMetadata.Meta();
			}
		}

		override protected esAppReportPivotQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppReportPivotQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AppReportPivotQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppReportPivotQuery();
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
		public bool Load(AppReportPivotQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AppReportPivotQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppReportPivotQuery : esAppReportPivotQuery
	{
		public AppReportPivotQuery()
		{

		}

		public AppReportPivotQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppReportPivotQuery";
		}
	}

	[Serializable]
	public partial class AppReportPivotMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppReportPivotMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppReportPivotMetadata.ColumnNames.ProgramID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppReportPivotMetadata.PropertyNames.ProgramID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			_columns.Add(c);

			c = new esColumnMetadata(AppReportPivotMetadata.ColumnNames.CustomPivotID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppReportPivotMetadata.PropertyNames.CustomPivotID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(AppReportPivotMetadata.ColumnNames.FieldCaption, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppReportPivotMetadata.PropertyNames.FieldCaption;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(AppReportPivotMetadata.ColumnNames.UserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppReportPivotMetadata.PropertyNames.UserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppReportPivotMetadata.ColumnNames.PivotArea, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppReportPivotMetadata.PropertyNames.PivotArea;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppReportPivotMetadata.ColumnNames.IndexNo, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppReportPivotMetadata.PropertyNames.IndexNo;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppReportPivotMetadata.ColumnNames.FieldName, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = AppReportPivotMetadata.PropertyNames.FieldName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(AppReportPivotMetadata.ColumnNames.GroupInterval, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppReportPivotMetadata.PropertyNames.GroupInterval;
			c.NumericPrecision = 10;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppReportPivotMetadata.ColumnNames.SummaryType, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppReportPivotMetadata.PropertyNames.SummaryType;
			c.NumericPrecision = 10;
			c.HasDefault = true;
			c.Default = @"((1))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppReportPivotMetadata.ColumnNames.FormatType, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppReportPivotMetadata.PropertyNames.FormatType;
			c.NumericPrecision = 10;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppReportPivotMetadata.ColumnNames.FormatString, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = AppReportPivotMetadata.PropertyNames.FormatString;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppReportPivotMetadata.ColumnNames.GroupIndex, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppReportPivotMetadata.PropertyNames.GroupIndex;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AppReportPivotMetadata Meta()
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
			public const string ProgramID = "ProgramID";
			public const string CustomPivotID = "CustomPivotID";
			public const string FieldCaption = "FieldCaption";
			public const string UserID = "UserID";
			public const string PivotArea = "PivotArea";
			public const string IndexNo = "IndexNo";
			public const string FieldName = "FieldName";
			public const string GroupInterval = "GroupInterval";
			public const string SummaryType = "SummaryType";
			public const string FormatType = "FormatType";
			public const string FormatString = "FormatString";
			public const string GroupIndex = "GroupIndex";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ProgramID = "ProgramID";
			public const string CustomPivotID = "CustomPivotID";
			public const string FieldCaption = "FieldCaption";
			public const string UserID = "UserID";
			public const string PivotArea = "PivotArea";
			public const string IndexNo = "IndexNo";
			public const string FieldName = "FieldName";
			public const string GroupInterval = "GroupInterval";
			public const string SummaryType = "SummaryType";
			public const string FormatType = "FormatType";
			public const string FormatString = "FormatString";
			public const string GroupIndex = "GroupIndex";
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
			lock (typeof(AppReportPivotMetadata))
			{
				if (AppReportPivotMetadata.mapDelegates == null)
				{
					AppReportPivotMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AppReportPivotMetadata.meta == null)
				{
					AppReportPivotMetadata.meta = new AppReportPivotMetadata();
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

				meta.AddTypeMap("ProgramID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CustomPivotID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("FieldCaption", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("UserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PivotArea", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IndexNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("FieldName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GroupInterval", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SummaryType", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("FormatType", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("FormatString", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GroupIndex", new esTypeMap("int", "System.Int32"));


				meta.Source = "AppReportPivot";
				meta.Destination = "AppReportPivot";
				meta.spInsert = "proc_AppReportPivotInsert";
				meta.spUpdate = "proc_AppReportPivotUpdate";
				meta.spDelete = "proc_AppReportPivotDelete";
				meta.spLoadAll = "proc_AppReportPivotLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppReportPivotLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppReportPivotMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
