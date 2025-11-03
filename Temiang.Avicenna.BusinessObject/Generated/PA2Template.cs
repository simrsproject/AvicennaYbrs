/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/4/2023 10:49:43 AM
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
	abstract public class esPA2TemplateCollection : esEntityCollectionWAuditLog
	{
		public esPA2TemplateCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PA2TemplateCollection";
		}

		#region Query Logic
		protected void InitQuery(esPA2TemplateQuery query)
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
			this.InitQuery(query as esPA2TemplateQuery);
		}
		#endregion

		virtual public PA2Template DetachEntity(PA2Template entity)
		{
			return base.DetachEntity(entity) as PA2Template;
		}

		virtual public PA2Template AttachEntity(PA2Template entity)
		{
			return base.AttachEntity(entity) as PA2Template;
		}

		virtual public void Combine(PA2TemplateCollection collection)
		{
			base.Combine(collection);
		}

		new public PA2Template this[int index]
		{
			get
			{
				return base[index] as PA2Template;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PA2Template);
		}
	}

	[Serializable]
	abstract public class esPA2Template : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPA2TemplateQuery GetDynamicQuery()
		{
			return null;
		}

		public esPA2Template()
		{
		}

		public esPA2Template(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 templateID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(templateID);
			else
				return LoadByPrimaryKeyStoredProcedure(templateID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 templateID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(templateID);
			else
				return LoadByPrimaryKeyStoredProcedure(templateID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 templateID)
		{
			esPA2TemplateQuery query = this.GetDynamicQuery();
			query.Where(query.TemplateID == templateID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 templateID)
		{
			esParameters parms = new esParameters();
			parms.Add("TemplateID", templateID);
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
						case "TemplateID": this.str.TemplateID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "TemplateName": this.str.TemplateName = (string)value; break;
						case "ResultType": this.str.ResultType = (string)value; break;
						case "PaDiagnoses": this.str.PaDiagnoses = (string)value; break;
						case "Result": this.str.Result = (string)value; break;
						case "ER": this.str.ER = (string)value; break;
						case "PR": this.str.PR = (string)value; break;
						case "Her2Neu": this.str.Her2Neu = (string)value; break;
						case "Ki67": this.str.Ki67 = (string)value; break;
						case "Impression": this.str.Impression = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "TemplateID":

							if (value == null || value is System.Int64)
								this.TemplateID = (System.Int64?)value;
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
		/// Maps to PA2Template.TemplateID
		/// </summary>
		virtual public System.Int64? TemplateID
		{
			get
			{
				return base.GetSystemInt64(PA2TemplateMetadata.ColumnNames.TemplateID);
			}

			set
			{
				base.SetSystemInt64(PA2TemplateMetadata.ColumnNames.TemplateID, value);
			}
		}
		/// <summary>
		/// Maps to PA2Template.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(PA2TemplateMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(PA2TemplateMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to PA2Template.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(PA2TemplateMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(PA2TemplateMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to PA2Template.TemplateName
		/// </summary>
		virtual public System.String TemplateName
		{
			get
			{
				return base.GetSystemString(PA2TemplateMetadata.ColumnNames.TemplateName);
			}

			set
			{
				base.SetSystemString(PA2TemplateMetadata.ColumnNames.TemplateName, value);
			}
		}
		/// <summary>
		/// Maps to PA2Template.ResultType
		/// </summary>
		virtual public System.String ResultType
		{
			get
			{
				return base.GetSystemString(PA2TemplateMetadata.ColumnNames.ResultType);
			}

			set
			{
				base.SetSystemString(PA2TemplateMetadata.ColumnNames.ResultType, value);
			}
		}
		/// <summary>
		/// Maps to PA2Template.PaDiagnoses
		/// </summary>
		virtual public System.String PaDiagnoses
		{
			get
			{
				return base.GetSystemString(PA2TemplateMetadata.ColumnNames.PaDiagnoses);
			}

			set
			{
				base.SetSystemString(PA2TemplateMetadata.ColumnNames.PaDiagnoses, value);
			}
		}
		/// <summary>
		/// Maps to PA2Template.Result
		/// </summary>
		virtual public System.String Result
		{
			get
			{
				return base.GetSystemString(PA2TemplateMetadata.ColumnNames.Result);
			}

			set
			{
				base.SetSystemString(PA2TemplateMetadata.ColumnNames.Result, value);
			}
		}
		/// <summary>
		/// Maps to PA2Template.ER
		/// </summary>
		virtual public System.String ER
		{
			get
			{
				return base.GetSystemString(PA2TemplateMetadata.ColumnNames.ER);
			}

			set
			{
				base.SetSystemString(PA2TemplateMetadata.ColumnNames.ER, value);
			}
		}
		/// <summary>
		/// Maps to PA2Template.PR
		/// </summary>
		virtual public System.String PR
		{
			get
			{
				return base.GetSystemString(PA2TemplateMetadata.ColumnNames.PR);
			}

			set
			{
				base.SetSystemString(PA2TemplateMetadata.ColumnNames.PR, value);
			}
		}
		/// <summary>
		/// Maps to PA2Template.Her2Neu
		/// </summary>
		virtual public System.String Her2Neu
		{
			get
			{
				return base.GetSystemString(PA2TemplateMetadata.ColumnNames.Her2Neu);
			}

			set
			{
				base.SetSystemString(PA2TemplateMetadata.ColumnNames.Her2Neu, value);
			}
		}
		/// <summary>
		/// Maps to PA2Template.Ki67
		/// </summary>
		virtual public System.String Ki67
		{
			get
			{
				return base.GetSystemString(PA2TemplateMetadata.ColumnNames.Ki67);
			}

			set
			{
				base.SetSystemString(PA2TemplateMetadata.ColumnNames.Ki67, value);
			}
		}
		/// <summary>
		/// Maps to PA2Template.Impression
		/// </summary>
		virtual public System.String Impression
		{
			get
			{
				return base.GetSystemString(PA2TemplateMetadata.ColumnNames.Impression);
			}

			set
			{
				base.SetSystemString(PA2TemplateMetadata.ColumnNames.Impression, value);
			}
		}
		/// <summary>
		/// Maps to PA2Template.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PA2TemplateMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PA2TemplateMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PA2Template.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PA2TemplateMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PA2TemplateMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPA2Template entity)
			{
				this.entity = entity;
			}
			public System.String TemplateID
			{
				get
				{
					System.Int64? data = entity.TemplateID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemplateID = null;
					else entity.TemplateID = Convert.ToInt64(value);
				}
			}
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
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
			public System.String TemplateName
			{
				get
				{
					System.String data = entity.TemplateName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemplateName = null;
					else entity.TemplateName = Convert.ToString(value);
				}
			}
			public System.String ResultType
			{
				get
				{
					System.String data = entity.ResultType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultType = null;
					else entity.ResultType = Convert.ToString(value);
				}
			}
			public System.String PaDiagnoses
			{
				get
				{
					System.String data = entity.PaDiagnoses;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaDiagnoses = null;
					else entity.PaDiagnoses = Convert.ToString(value);
				}
			}
			public System.String Result
			{
				get
				{
					System.String data = entity.Result;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Result = null;
					else entity.Result = Convert.ToString(value);
				}
			}
			public System.String ER
			{
				get
				{
					System.String data = entity.ER;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ER = null;
					else entity.ER = Convert.ToString(value);
				}
			}
			public System.String PR
			{
				get
				{
					System.String data = entity.PR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PR = null;
					else entity.PR = Convert.ToString(value);
				}
			}
			public System.String Her2Neu
			{
				get
				{
					System.String data = entity.Her2Neu;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Her2Neu = null;
					else entity.Her2Neu = Convert.ToString(value);
				}
			}
			public System.String Ki67
			{
				get
				{
					System.String data = entity.Ki67;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Ki67 = null;
					else entity.Ki67 = Convert.ToString(value);
				}
			}
			public System.String Impression
			{
				get
				{
					System.String data = entity.Impression;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Impression = null;
					else entity.Impression = Convert.ToString(value);
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
			private esPA2Template entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPA2TemplateQuery query)
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
				throw new Exception("esPA2Template can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PA2Template : esPA2Template
	{
	}

	[Serializable]
	abstract public class esPA2TemplateQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PA2TemplateMetadata.Meta();
			}
		}

		public esQueryItem TemplateID
		{
			get
			{
				return new esQueryItem(this, PA2TemplateMetadata.ColumnNames.TemplateID, esSystemType.Int64);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, PA2TemplateMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, PA2TemplateMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem TemplateName
		{
			get
			{
				return new esQueryItem(this, PA2TemplateMetadata.ColumnNames.TemplateName, esSystemType.String);
			}
		}

		public esQueryItem ResultType
		{
			get
			{
				return new esQueryItem(this, PA2TemplateMetadata.ColumnNames.ResultType, esSystemType.String);
			}
		}

		public esQueryItem PaDiagnoses
		{
			get
			{
				return new esQueryItem(this, PA2TemplateMetadata.ColumnNames.PaDiagnoses, esSystemType.String);
			}
		}

		public esQueryItem Result
		{
			get
			{
				return new esQueryItem(this, PA2TemplateMetadata.ColumnNames.Result, esSystemType.String);
			}
		}

		public esQueryItem ER
		{
			get
			{
				return new esQueryItem(this, PA2TemplateMetadata.ColumnNames.ER, esSystemType.String);
			}
		}

		public esQueryItem PR
		{
			get
			{
				return new esQueryItem(this, PA2TemplateMetadata.ColumnNames.PR, esSystemType.String);
			}
		}

		public esQueryItem Her2Neu
		{
			get
			{
				return new esQueryItem(this, PA2TemplateMetadata.ColumnNames.Her2Neu, esSystemType.String);
			}
		}

		public esQueryItem Ki67
		{
			get
			{
				return new esQueryItem(this, PA2TemplateMetadata.ColumnNames.Ki67, esSystemType.String);
			}
		}

		public esQueryItem Impression
		{
			get
			{
				return new esQueryItem(this, PA2TemplateMetadata.ColumnNames.Impression, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PA2TemplateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PA2TemplateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PA2TemplateCollection")]
	public partial class PA2TemplateCollection : esPA2TemplateCollection, IEnumerable<PA2Template>
	{
		public PA2TemplateCollection()
		{

		}

		public static implicit operator List<PA2Template>(PA2TemplateCollection coll)
		{
			List<PA2Template> list = new List<PA2Template>();

			foreach (PA2Template emp in coll)
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
				return PA2TemplateMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PA2TemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PA2Template(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PA2Template();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PA2TemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PA2TemplateQuery();
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
		public bool Load(PA2TemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PA2Template AddNew()
		{
			PA2Template entity = base.AddNewEntity() as PA2Template;

			return entity;
		}
		public PA2Template FindByPrimaryKey(Int64 templateID)
		{
			return base.FindByPrimaryKey(templateID) as PA2Template;
		}

		#region IEnumerable< PA2Template> Members

		IEnumerator<PA2Template> IEnumerable<PA2Template>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PA2Template;
			}
		}

		#endregion

		private PA2TemplateQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PA2Template' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PA2Template ({TemplateID})")]
	[Serializable]
	public partial class PA2Template : esPA2Template
	{
		public PA2Template()
		{
		}

		public PA2Template(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PA2TemplateMetadata.Meta();
			}
		}

		override protected esPA2TemplateQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PA2TemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PA2TemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PA2TemplateQuery();
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
		public bool Load(PA2TemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PA2TemplateQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PA2TemplateQuery : esPA2TemplateQuery
	{
		public PA2TemplateQuery()
		{

		}

		public PA2TemplateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PA2TemplateQuery";
		}
	}

	[Serializable]
	public partial class PA2TemplateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PA2TemplateMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PA2TemplateMetadata.ColumnNames.TemplateID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = PA2TemplateMetadata.PropertyNames.TemplateID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(PA2TemplateMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PA2TemplateMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PA2TemplateMetadata.ColumnNames.ParamedicID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PA2TemplateMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PA2TemplateMetadata.ColumnNames.TemplateName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PA2TemplateMetadata.PropertyNames.TemplateName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(PA2TemplateMetadata.ColumnNames.ResultType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PA2TemplateMetadata.PropertyNames.ResultType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PA2TemplateMetadata.ColumnNames.PaDiagnoses, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PA2TemplateMetadata.PropertyNames.PaDiagnoses;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PA2TemplateMetadata.ColumnNames.Result, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PA2TemplateMetadata.PropertyNames.Result;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PA2TemplateMetadata.ColumnNames.ER, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PA2TemplateMetadata.PropertyNames.ER;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PA2TemplateMetadata.ColumnNames.PR, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PA2TemplateMetadata.PropertyNames.PR;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PA2TemplateMetadata.ColumnNames.Her2Neu, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PA2TemplateMetadata.PropertyNames.Her2Neu;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PA2TemplateMetadata.ColumnNames.Ki67, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PA2TemplateMetadata.PropertyNames.Ki67;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PA2TemplateMetadata.ColumnNames.Impression, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = PA2TemplateMetadata.PropertyNames.Impression;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PA2TemplateMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PA2TemplateMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PA2TemplateMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = PA2TemplateMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PA2TemplateMetadata Meta()
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
			public const string TemplateID = "TemplateID";
			public const string ItemID = "ItemID";
			public const string ParamedicID = "ParamedicID";
			public const string TemplateName = "TemplateName";
			public const string ResultType = "ResultType";
			public const string PaDiagnoses = "PaDiagnoses";
			public const string Result = "Result";
			public const string ER = "ER";
			public const string PR = "PR";
			public const string Her2Neu = "Her2Neu";
			public const string Ki67 = "Ki67";
			public const string Impression = "Impression";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TemplateID = "TemplateID";
			public const string ItemID = "ItemID";
			public const string ParamedicID = "ParamedicID";
			public const string TemplateName = "TemplateName";
			public const string ResultType = "ResultType";
			public const string PaDiagnoses = "PaDiagnoses";
			public const string Result = "Result";
			public const string ER = "ER";
			public const string PR = "PR";
			public const string Her2Neu = "Her2Neu";
			public const string Ki67 = "Ki67";
			public const string Impression = "Impression";
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
			lock (typeof(PA2TemplateMetadata))
			{
				if (PA2TemplateMetadata.mapDelegates == null)
				{
					PA2TemplateMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PA2TemplateMetadata.meta == null)
				{
					PA2TemplateMetadata.meta = new PA2TemplateMetadata();
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

				meta.AddTypeMap("TemplateID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TemplateName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResultType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaDiagnoses", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Result", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ER", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PR", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Her2Neu", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Ki67", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Impression", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PA2Template";
				meta.Destination = "PA2Template";
				meta.spInsert = "proc_PA2TemplateInsert";
				meta.spUpdate = "proc_PA2TemplateUpdate";
				meta.spDelete = "proc_PA2TemplateDelete";
				meta.spLoadAll = "proc_PA2TemplateLoadAll";
				meta.spLoadByPrimaryKey = "proc_PA2TemplateLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PA2TemplateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
