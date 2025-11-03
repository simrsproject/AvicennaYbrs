/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/13/2020 8:27:07 AM
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
	abstract public class esAppraisalQuestionRatingCollection : esEntityCollectionWAuditLog
	{
		public esAppraisalQuestionRatingCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AppraisalQuestionRatingCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppraisalQuestionRatingQuery query)
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
			this.InitQuery(query as esAppraisalQuestionRatingQuery);
		}
		#endregion

		virtual public AppraisalQuestionRating DetachEntity(AppraisalQuestionRating entity)
		{
			return base.DetachEntity(entity) as AppraisalQuestionRating;
		}

		virtual public AppraisalQuestionRating AttachEntity(AppraisalQuestionRating entity)
		{
			return base.AttachEntity(entity) as AppraisalQuestionRating;
		}

		virtual public void Combine(AppraisalQuestionRatingCollection collection)
		{
			base.Combine(collection);
		}

		new public AppraisalQuestionRating this[int index]
		{
			get
			{
				return base[index] as AppraisalQuestionRating;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppraisalQuestionRating);
		}
	}

	[Serializable]
	abstract public class esAppraisalQuestionRating : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppraisalQuestionRatingQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppraisalQuestionRating()
		{
		}

		public esAppraisalQuestionRating(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 ratingID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(ratingID);
			else
				return LoadByPrimaryKeyStoredProcedure(ratingID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 ratingID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(ratingID);
			else
				return LoadByPrimaryKeyStoredProcedure(ratingID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 ratingID)
		{
			esAppraisalQuestionRatingQuery query = this.GetDynamicQuery();
			query.Where(query.RatingID == ratingID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 ratingID)
		{
			esParameters parms = new esParameters();
			parms.Add("RatingID", ratingID);
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
						case "RatingID": this.str.RatingID = (string)value; break;
						case "QuestionerID": this.str.QuestionerID = (string)value; break;
						case "RatingCode": this.str.RatingCode = (string)value; break;
						case "RatingName": this.str.RatingName = (string)value; break;
						case "RatingValue": this.str.RatingValue = (string)value; break;
						case "RatingValueMin": this.str.RatingValueMin = (string)value; break;
						case "RatingValueMax": this.str.RatingValueMax = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "RatingID":

							if (value == null || value is System.Int32)
								this.RatingID = (System.Int32?)value;
							break;
						case "QuestionerID":

							if (value == null || value is System.Int32)
								this.QuestionerID = (System.Int32?)value;
							break;
						case "RatingValue":

							if (value == null || value is System.Decimal)
								this.RatingValue = (System.Decimal?)value;
							break;
						case "RatingValueMin":

							if (value == null || value is System.Decimal)
								this.RatingValueMin = (System.Decimal?)value;
							break;
						case "RatingValueMax":

							if (value == null || value is System.Decimal)
								this.RatingValueMax = (System.Decimal?)value;
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
		/// Maps to AppraisalQuestionRating.RatingID
		/// </summary>
		virtual public System.Int32? RatingID
		{
			get
			{
				return base.GetSystemInt32(AppraisalQuestionRatingMetadata.ColumnNames.RatingID);
			}

			set
			{
				base.SetSystemInt32(AppraisalQuestionRatingMetadata.ColumnNames.RatingID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionRating.QuestionerID
		/// </summary>
		virtual public System.Int32? QuestionerID
		{
			get
			{
				return base.GetSystemInt32(AppraisalQuestionRatingMetadata.ColumnNames.QuestionerID);
			}

			set
			{
				base.SetSystemInt32(AppraisalQuestionRatingMetadata.ColumnNames.QuestionerID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionRating.RatingCode
		/// </summary>
		virtual public System.String RatingCode
		{
			get
			{
				return base.GetSystemString(AppraisalQuestionRatingMetadata.ColumnNames.RatingCode);
			}

			set
			{
				base.SetSystemString(AppraisalQuestionRatingMetadata.ColumnNames.RatingCode, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionRating.RatingName
		/// </summary>
		virtual public System.String RatingName
		{
			get
			{
				return base.GetSystemString(AppraisalQuestionRatingMetadata.ColumnNames.RatingName);
			}

			set
			{
				base.SetSystemString(AppraisalQuestionRatingMetadata.ColumnNames.RatingName, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionRating.RatingValue
		/// </summary>
		virtual public System.Decimal? RatingValue
		{
			get
			{
				return base.GetSystemDecimal(AppraisalQuestionRatingMetadata.ColumnNames.RatingValue);
			}

			set
			{
				base.SetSystemDecimal(AppraisalQuestionRatingMetadata.ColumnNames.RatingValue, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionRating.RatingValueMin
		/// </summary>
		virtual public System.Decimal? RatingValueMin
		{
			get
			{
				return base.GetSystemDecimal(AppraisalQuestionRatingMetadata.ColumnNames.RatingValueMin);
			}

			set
			{
				base.SetSystemDecimal(AppraisalQuestionRatingMetadata.ColumnNames.RatingValueMin, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionRating.RatingValueMax
		/// </summary>
		virtual public System.Decimal? RatingValueMax
		{
			get
			{
				return base.GetSystemDecimal(AppraisalQuestionRatingMetadata.ColumnNames.RatingValueMax);
			}

			set
			{
				base.SetSystemDecimal(AppraisalQuestionRatingMetadata.ColumnNames.RatingValueMax, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionRating.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppraisalQuestionRatingMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AppraisalQuestionRatingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionRating.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppraisalQuestionRatingMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AppraisalQuestionRatingMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAppraisalQuestionRating entity)
			{
				this.entity = entity;
			}
			public System.String RatingID
			{
				get
				{
					System.Int32? data = entity.RatingID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RatingID = null;
					else entity.RatingID = Convert.ToInt32(value);
				}
			}
			public System.String QuestionerID
			{
				get
				{
					System.Int32? data = entity.QuestionerID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionerID = null;
					else entity.QuestionerID = Convert.ToInt32(value);
				}
			}
			public System.String RatingCode
			{
				get
				{
					System.String data = entity.RatingCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RatingCode = null;
					else entity.RatingCode = Convert.ToString(value);
				}
			}
			public System.String RatingName
			{
				get
				{
					System.String data = entity.RatingName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RatingName = null;
					else entity.RatingName = Convert.ToString(value);
				}
			}
			public System.String RatingValue
			{
				get
				{
					System.Decimal? data = entity.RatingValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RatingValue = null;
					else entity.RatingValue = Convert.ToDecimal(value);
				}
			}
			public System.String RatingValueMin
			{
				get
				{
					System.Decimal? data = entity.RatingValueMin;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RatingValueMin = null;
					else entity.RatingValueMin = Convert.ToDecimal(value);
				}
			}
			public System.String RatingValueMax
			{
				get
				{
					System.Decimal? data = entity.RatingValueMax;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RatingValueMax = null;
					else entity.RatingValueMax = Convert.ToDecimal(value);
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
			private esAppraisalQuestionRating entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppraisalQuestionRatingQuery query)
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
				throw new Exception("esAppraisalQuestionRating can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppraisalQuestionRating : esAppraisalQuestionRating
	{
	}

	[Serializable]
	abstract public class esAppraisalQuestionRatingQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AppraisalQuestionRatingMetadata.Meta();
			}
		}

		public esQueryItem RatingID
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionRatingMetadata.ColumnNames.RatingID, esSystemType.Int32);
			}
		}

		public esQueryItem QuestionerID
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionRatingMetadata.ColumnNames.QuestionerID, esSystemType.Int32);
			}
		}

		public esQueryItem RatingCode
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionRatingMetadata.ColumnNames.RatingCode, esSystemType.String);
			}
		}

		public esQueryItem RatingName
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionRatingMetadata.ColumnNames.RatingName, esSystemType.String);
			}
		}

		public esQueryItem RatingValue
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionRatingMetadata.ColumnNames.RatingValue, esSystemType.Decimal);
			}
		}

		public esQueryItem RatingValueMin
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionRatingMetadata.ColumnNames.RatingValueMin, esSystemType.Decimal);
			}
		}

		public esQueryItem RatingValueMax
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionRatingMetadata.ColumnNames.RatingValueMax, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionRatingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionRatingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppraisalQuestionRatingCollection")]
	public partial class AppraisalQuestionRatingCollection : esAppraisalQuestionRatingCollection, IEnumerable<AppraisalQuestionRating>
	{
		public AppraisalQuestionRatingCollection()
		{

		}

		public static implicit operator List<AppraisalQuestionRating>(AppraisalQuestionRatingCollection coll)
		{
			List<AppraisalQuestionRating> list = new List<AppraisalQuestionRating>();

			foreach (AppraisalQuestionRating emp in coll)
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
				return AppraisalQuestionRatingMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalQuestionRatingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppraisalQuestionRating(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppraisalQuestionRating();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AppraisalQuestionRatingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalQuestionRatingQuery();
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
		public bool Load(AppraisalQuestionRatingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppraisalQuestionRating AddNew()
		{
			AppraisalQuestionRating entity = base.AddNewEntity() as AppraisalQuestionRating;

			return entity;
		}
		public AppraisalQuestionRating FindByPrimaryKey(Int32 ratingID)
		{
			return base.FindByPrimaryKey(ratingID) as AppraisalQuestionRating;
		}

		#region IEnumerable< AppraisalQuestionRating> Members

		IEnumerator<AppraisalQuestionRating> IEnumerable<AppraisalQuestionRating>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AppraisalQuestionRating;
			}
		}

		#endregion

		private AppraisalQuestionRatingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppraisalQuestionRating' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppraisalQuestionRating ({RatingID})")]
	[Serializable]
	public partial class AppraisalQuestionRating : esAppraisalQuestionRating
	{
		public AppraisalQuestionRating()
		{
		}

		public AppraisalQuestionRating(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppraisalQuestionRatingMetadata.Meta();
			}
		}

		override protected esAppraisalQuestionRatingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalQuestionRatingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AppraisalQuestionRatingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalQuestionRatingQuery();
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
		public bool Load(AppraisalQuestionRatingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AppraisalQuestionRatingQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppraisalQuestionRatingQuery : esAppraisalQuestionRatingQuery
	{
		public AppraisalQuestionRatingQuery()
		{

		}

		public AppraisalQuestionRatingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppraisalQuestionRatingQuery";
		}
	}

	[Serializable]
	public partial class AppraisalQuestionRatingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppraisalQuestionRatingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppraisalQuestionRatingMetadata.ColumnNames.RatingID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalQuestionRatingMetadata.PropertyNames.RatingID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionRatingMetadata.ColumnNames.QuestionerID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalQuestionRatingMetadata.PropertyNames.QuestionerID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionRatingMetadata.ColumnNames.RatingCode, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalQuestionRatingMetadata.PropertyNames.RatingCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionRatingMetadata.ColumnNames.RatingName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalQuestionRatingMetadata.PropertyNames.RatingName;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionRatingMetadata.ColumnNames.RatingValue, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalQuestionRatingMetadata.PropertyNames.RatingValue;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionRatingMetadata.ColumnNames.RatingValueMin, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalQuestionRatingMetadata.PropertyNames.RatingValueMin;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionRatingMetadata.ColumnNames.RatingValueMax, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalQuestionRatingMetadata.PropertyNames.RatingValueMax;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionRatingMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppraisalQuestionRatingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionRatingMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalQuestionRatingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AppraisalQuestionRatingMetadata Meta()
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
			public const string RatingID = "RatingID";
			public const string QuestionerID = "QuestionerID";
			public const string RatingCode = "RatingCode";
			public const string RatingName = "RatingName";
			public const string RatingValue = "RatingValue";
			public const string RatingValueMin = "RatingValueMin";
			public const string RatingValueMax = "RatingValueMax";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RatingID = "RatingID";
			public const string QuestionerID = "QuestionerID";
			public const string RatingCode = "RatingCode";
			public const string RatingName = "RatingName";
			public const string RatingValue = "RatingValue";
			public const string RatingValueMin = "RatingValueMin";
			public const string RatingValueMax = "RatingValueMax";
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
			lock (typeof(AppraisalQuestionRatingMetadata))
			{
				if (AppraisalQuestionRatingMetadata.mapDelegates == null)
				{
					AppraisalQuestionRatingMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AppraisalQuestionRatingMetadata.meta == null)
				{
					AppraisalQuestionRatingMetadata.meta = new AppraisalQuestionRatingMetadata();
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

				meta.AddTypeMap("RatingID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("QuestionerID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RatingCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RatingName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RatingValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RatingValueMin", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RatingValueMax", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "AppraisalQuestionRating";
				meta.Destination = "AppraisalQuestionRating";
				meta.spInsert = "proc_AppraisalQuestionRatingInsert";
				meta.spUpdate = "proc_AppraisalQuestionRatingUpdate";
				meta.spDelete = "proc_AppraisalQuestionRatingDelete";
				meta.spLoadAll = "proc_AppraisalQuestionRatingLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppraisalQuestionRatingLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppraisalQuestionRatingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
