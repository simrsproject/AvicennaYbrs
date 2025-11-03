/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/15/2022 1:40:10 PM
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
	abstract public class esPositionGradeCollection : esEntityCollectionWAuditLog
	{
		public esPositionGradeCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PositionGradeCollection";
		}

		#region Query Logic
		protected void InitQuery(esPositionGradeQuery query)
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
			this.InitQuery(query as esPositionGradeQuery);
		}
		#endregion

		virtual public PositionGrade DetachEntity(PositionGrade entity)
		{
			return base.DetachEntity(entity) as PositionGrade;
		}

		virtual public PositionGrade AttachEntity(PositionGrade entity)
		{
			return base.AttachEntity(entity) as PositionGrade;
		}

		virtual public void Combine(PositionGradeCollection collection)
		{
			base.Combine(collection);
		}

		new public PositionGrade this[int index]
		{
			get
			{
				return base[index] as PositionGrade;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PositionGrade);
		}
	}

	[Serializable]
	abstract public class esPositionGrade : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPositionGradeQuery GetDynamicQuery()
		{
			return null;
		}

		public esPositionGrade()
		{
		}

		public esPositionGrade(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 positionGradeID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionGradeID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionGradeID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 positionGradeID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionGradeID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionGradeID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 positionGradeID)
		{
			esPositionGradeQuery query = this.GetDynamicQuery();
			query.Where(query.PositionGradeID == positionGradeID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 positionGradeID)
		{
			esParameters parms = new esParameters();
			parms.Add("PositionGradeID", positionGradeID);
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
						case "PositionGradeID": this.str.PositionGradeID = (string)value; break;
						case "PositionGradeCode": this.str.PositionGradeCode = (string)value; break;
						case "PositionGradeName": this.str.PositionGradeName = (string)value; break;
						case "Interval": this.str.Interval = (string)value; break;
						case "Ranking": this.str.Ranking = (string)value; break;
						case "RankName": this.str.RankName = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SREmploymentType": this.str.SREmploymentType = (string)value; break;
						case "LowerLimit": this.str.LowerLimit = (string)value; break;
						case "UpperLimit": this.str.UpperLimit = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PositionGradeID":

							if (value == null || value is System.Int32)
								this.PositionGradeID = (System.Int32?)value;
							break;
						case "Ranking":

							if (value == null || value is System.Int16)
								this.Ranking = (System.Int16?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "LowerLimit":

							if (value == null || value is System.Decimal)
								this.LowerLimit = (System.Decimal?)value;
							break;
						case "UpperLimit":

							if (value == null || value is System.Decimal)
								this.UpperLimit = (System.Decimal?)value;
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
		/// Maps to PositionGrade.PositionGradeID
		/// </summary>
		virtual public System.Int32? PositionGradeID
		{
			get
			{
				return base.GetSystemInt32(PositionGradeMetadata.ColumnNames.PositionGradeID);
			}

			set
			{
				base.SetSystemInt32(PositionGradeMetadata.ColumnNames.PositionGradeID, value);
			}
		}
		/// <summary>
		/// Maps to PositionGrade.PositionGradeCode
		/// </summary>
		virtual public System.String PositionGradeCode
		{
			get
			{
				return base.GetSystemString(PositionGradeMetadata.ColumnNames.PositionGradeCode);
			}

			set
			{
				base.SetSystemString(PositionGradeMetadata.ColumnNames.PositionGradeCode, value);
			}
		}
		/// <summary>
		/// Maps to PositionGrade.PositionGradeName
		/// </summary>
		virtual public System.String PositionGradeName
		{
			get
			{
				return base.GetSystemString(PositionGradeMetadata.ColumnNames.PositionGradeName);
			}

			set
			{
				base.SetSystemString(PositionGradeMetadata.ColumnNames.PositionGradeName, value);
			}
		}
		/// <summary>
		/// Maps to PositionGrade.Interval
		/// </summary>
		virtual public System.String Interval
		{
			get
			{
				return base.GetSystemString(PositionGradeMetadata.ColumnNames.Interval);
			}

			set
			{
				base.SetSystemString(PositionGradeMetadata.ColumnNames.Interval, value);
			}
		}
		/// <summary>
		/// Maps to PositionGrade.Ranking
		/// </summary>
		virtual public System.Int16? Ranking
		{
			get
			{
				return base.GetSystemInt16(PositionGradeMetadata.ColumnNames.Ranking);
			}

			set
			{
				base.SetSystemInt16(PositionGradeMetadata.ColumnNames.Ranking, value);
			}
		}
		/// <summary>
		/// Maps to PositionGrade.RankName
		/// </summary>
		virtual public System.String RankName
		{
			get
			{
				return base.GetSystemString(PositionGradeMetadata.ColumnNames.RankName);
			}

			set
			{
				base.SetSystemString(PositionGradeMetadata.ColumnNames.RankName, value);
			}
		}
		/// <summary>
		/// Maps to PositionGrade.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PositionGradeMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PositionGradeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PositionGrade.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PositionGradeMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PositionGradeMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PositionGrade.SREmploymentType
		/// </summary>
		virtual public System.String SREmploymentType
		{
			get
			{
				return base.GetSystemString(PositionGradeMetadata.ColumnNames.SREmploymentType);
			}

			set
			{
				base.SetSystemString(PositionGradeMetadata.ColumnNames.SREmploymentType, value);
			}
		}
		/// <summary>
		/// Maps to PositionGrade.LowerLimit
		/// </summary>
		virtual public System.Decimal? LowerLimit
		{
			get
			{
				return base.GetSystemDecimal(PositionGradeMetadata.ColumnNames.LowerLimit);
			}

			set
			{
				base.SetSystemDecimal(PositionGradeMetadata.ColumnNames.LowerLimit, value);
			}
		}
		/// <summary>
		/// Maps to PositionGrade.UpperLimit
		/// </summary>
		virtual public System.Decimal? UpperLimit
		{
			get
			{
				return base.GetSystemDecimal(PositionGradeMetadata.ColumnNames.UpperLimit);
			}

			set
			{
				base.SetSystemDecimal(PositionGradeMetadata.ColumnNames.UpperLimit, value);
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
			public esStrings(esPositionGrade entity)
			{
				this.entity = entity;
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
			public System.String PositionGradeCode
			{
				get
				{
					System.String data = entity.PositionGradeCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionGradeCode = null;
					else entity.PositionGradeCode = Convert.ToString(value);
				}
			}
			public System.String PositionGradeName
			{
				get
				{
					System.String data = entity.PositionGradeName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionGradeName = null;
					else entity.PositionGradeName = Convert.ToString(value);
				}
			}
			public System.String Interval
			{
				get
				{
					System.String data = entity.Interval;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Interval = null;
					else entity.Interval = Convert.ToString(value);
				}
			}
			public System.String Ranking
			{
				get
				{
					System.Int16? data = entity.Ranking;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Ranking = null;
					else entity.Ranking = Convert.ToInt16(value);
				}
			}
			public System.String RankName
			{
				get
				{
					System.String data = entity.RankName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RankName = null;
					else entity.RankName = Convert.ToString(value);
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
			public System.String SREmploymentType
			{
				get
				{
					System.String data = entity.SREmploymentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmploymentType = null;
					else entity.SREmploymentType = Convert.ToString(value);
				}
			}
			public System.String LowerLimit
			{
				get
				{
					System.Decimal? data = entity.LowerLimit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LowerLimit = null;
					else entity.LowerLimit = Convert.ToDecimal(value);
				}
			}
			public System.String UpperLimit
			{
				get
				{
					System.Decimal? data = entity.UpperLimit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UpperLimit = null;
					else entity.UpperLimit = Convert.ToDecimal(value);
				}
			}
			private esPositionGrade entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPositionGradeQuery query)
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
				throw new Exception("esPositionGrade can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PositionGrade : esPositionGrade
	{
	}

	[Serializable]
	abstract public class esPositionGradeQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PositionGradeMetadata.Meta();
			}
		}

		public esQueryItem PositionGradeID
		{
			get
			{
				return new esQueryItem(this, PositionGradeMetadata.ColumnNames.PositionGradeID, esSystemType.Int32);
			}
		}

		public esQueryItem PositionGradeCode
		{
			get
			{
				return new esQueryItem(this, PositionGradeMetadata.ColumnNames.PositionGradeCode, esSystemType.String);
			}
		}

		public esQueryItem PositionGradeName
		{
			get
			{
				return new esQueryItem(this, PositionGradeMetadata.ColumnNames.PositionGradeName, esSystemType.String);
			}
		}

		public esQueryItem Interval
		{
			get
			{
				return new esQueryItem(this, PositionGradeMetadata.ColumnNames.Interval, esSystemType.String);
			}
		}

		public esQueryItem Ranking
		{
			get
			{
				return new esQueryItem(this, PositionGradeMetadata.ColumnNames.Ranking, esSystemType.Int16);
			}
		}

		public esQueryItem RankName
		{
			get
			{
				return new esQueryItem(this, PositionGradeMetadata.ColumnNames.RankName, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PositionGradeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PositionGradeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SREmploymentType
		{
			get
			{
				return new esQueryItem(this, PositionGradeMetadata.ColumnNames.SREmploymentType, esSystemType.String);
			}
		}

		public esQueryItem LowerLimit
		{
			get
			{
				return new esQueryItem(this, PositionGradeMetadata.ColumnNames.LowerLimit, esSystemType.Decimal);
			}
		}

		public esQueryItem UpperLimit
		{
			get
			{
				return new esQueryItem(this, PositionGradeMetadata.ColumnNames.UpperLimit, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PositionGradeCollection")]
	public partial class PositionGradeCollection : esPositionGradeCollection, IEnumerable<PositionGrade>
	{
		public PositionGradeCollection()
		{

		}

		public static implicit operator List<PositionGrade>(PositionGradeCollection coll)
		{
			List<PositionGrade> list = new List<PositionGrade>();

			foreach (PositionGrade emp in coll)
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
				return PositionGradeMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionGradeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PositionGrade(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PositionGrade();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PositionGradeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionGradeQuery();
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
		public bool Load(PositionGradeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PositionGrade AddNew()
		{
			PositionGrade entity = base.AddNewEntity() as PositionGrade;

			return entity;
		}
		public PositionGrade FindByPrimaryKey(Int32 positionGradeID)
		{
			return base.FindByPrimaryKey(positionGradeID) as PositionGrade;
		}

		#region IEnumerable< PositionGrade> Members

		IEnumerator<PositionGrade> IEnumerable<PositionGrade>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PositionGrade;
			}
		}

		#endregion

		private PositionGradeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PositionGrade' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PositionGrade ({PositionGradeID})")]
	[Serializable]
	public partial class PositionGrade : esPositionGrade
	{
		public PositionGrade()
		{
		}

		public PositionGrade(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PositionGradeMetadata.Meta();
			}
		}

		override protected esPositionGradeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionGradeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PositionGradeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionGradeQuery();
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
		public bool Load(PositionGradeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PositionGradeQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PositionGradeQuery : esPositionGradeQuery
	{
		public PositionGradeQuery()
		{

		}

		public PositionGradeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PositionGradeQuery";
		}
	}

	[Serializable]
	public partial class PositionGradeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PositionGradeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PositionGradeMetadata.ColumnNames.PositionGradeID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionGradeMetadata.PropertyNames.PositionGradeID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PositionGradeMetadata.ColumnNames.PositionGradeCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionGradeMetadata.PropertyNames.PositionGradeCode;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PositionGradeMetadata.ColumnNames.PositionGradeName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionGradeMetadata.PropertyNames.PositionGradeName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(PositionGradeMetadata.ColumnNames.Interval, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionGradeMetadata.PropertyNames.Interval;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(PositionGradeMetadata.ColumnNames.Ranking, 4, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = PositionGradeMetadata.PropertyNames.Ranking;
			c.NumericPrecision = 5;
			_columns.Add(c);

			c = new esColumnMetadata(PositionGradeMetadata.ColumnNames.RankName, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionGradeMetadata.PropertyNames.RankName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PositionGradeMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PositionGradeMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(PositionGradeMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionGradeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(PositionGradeMetadata.ColumnNames.SREmploymentType, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionGradeMetadata.PropertyNames.SREmploymentType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PositionGradeMetadata.ColumnNames.LowerLimit, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PositionGradeMetadata.PropertyNames.LowerLimit;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PositionGradeMetadata.ColumnNames.UpperLimit, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PositionGradeMetadata.PropertyNames.UpperLimit;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PositionGradeMetadata Meta()
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
			public const string PositionGradeID = "PositionGradeID";
			public const string PositionGradeCode = "PositionGradeCode";
			public const string PositionGradeName = "PositionGradeName";
			public const string Interval = "Interval";
			public const string Ranking = "Ranking";
			public const string RankName = "RankName";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SREmploymentType = "SREmploymentType";
			public const string LowerLimit = "LowerLimit";
			public const string UpperLimit = "UpperLimit";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PositionGradeID = "PositionGradeID";
			public const string PositionGradeCode = "PositionGradeCode";
			public const string PositionGradeName = "PositionGradeName";
			public const string Interval = "Interval";
			public const string Ranking = "Ranking";
			public const string RankName = "RankName";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SREmploymentType = "SREmploymentType";
			public const string LowerLimit = "LowerLimit";
			public const string UpperLimit = "UpperLimit";
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
			lock (typeof(PositionGradeMetadata))
			{
				if (PositionGradeMetadata.mapDelegates == null)
				{
					PositionGradeMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PositionGradeMetadata.meta == null)
				{
					PositionGradeMetadata.meta = new PositionGradeMetadata();
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

				meta.AddTypeMap("PositionGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionGradeCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionGradeName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Interval", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Ranking", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("RankName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmploymentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LowerLimit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("UpperLimit", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "PositionGrade";
				meta.Destination = "PositionGrade";
				meta.spInsert = "proc_PositionGradeInsert";
				meta.spUpdate = "proc_PositionGradeUpdate";
				meta.spDelete = "proc_PositionGradeDelete";
				meta.spLoadAll = "proc_PositionGradeLoadAll";
				meta.spLoadByPrimaryKey = "proc_PositionGradeLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PositionGradeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
