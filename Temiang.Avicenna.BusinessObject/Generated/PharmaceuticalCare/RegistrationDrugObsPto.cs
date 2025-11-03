/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/13/2022 10:53:40 PM
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
	abstract public class esRegistrationDrugObsPtoCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationDrugObsPtoCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "RegistrationDrugObsPtoCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationDrugObsPtoQuery query)
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
			this.InitQuery(query as esRegistrationDrugObsPtoQuery);
		}
		#endregion

		virtual public RegistrationDrugObsPto DetachEntity(RegistrationDrugObsPto entity)
		{
			return base.DetachEntity(entity) as RegistrationDrugObsPto;
		}

		virtual public RegistrationDrugObsPto AttachEntity(RegistrationDrugObsPto entity)
		{
			return base.AttachEntity(entity) as RegistrationDrugObsPto;
		}

		virtual public void Combine(RegistrationDrugObsPtoCollection collection)
		{
			base.Combine(collection);
		}

		new public RegistrationDrugObsPto this[int index]
		{
			get
			{
				return base[index] as RegistrationDrugObsPto;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationDrugObsPto);
		}
	}

	[Serializable]
	abstract public class esRegistrationDrugObsPto : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationDrugObsPtoQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationDrugObsPto()
		{
		}

		public esRegistrationDrugObsPto(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 drugObsNo, String sRPto)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, drugObsNo, sRPto);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, drugObsNo, sRPto);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 drugObsNo, String sRPto)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, drugObsNo, sRPto);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, drugObsNo, sRPto);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 drugObsNo, String sRPto)
		{
			esRegistrationDrugObsPtoQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.DrugObsNo == drugObsNo, query.SRPto == sRPto);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 drugObsNo, String sRPto)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("DrugObsNo", drugObsNo);
			parms.Add("SRPto", sRPto);
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
						case "DrugObsNo": this.str.DrugObsNo = (string)value; break;
						case "SRPto": this.str.SRPto = (string)value; break;
						case "IsYes": this.str.IsYes = (string)value; break;
						case "YesNotes": this.str.YesNotes = (string)value; break;
						case "IsDrugDuplicate": this.str.IsDrugDuplicate = (string)value; break;
						case "IsMoreThan7Days": this.str.IsMoreThan7Days = (string)value; break;
						case "IsAgeMoreThan65y": this.str.IsAgeMoreThan65y = (string)value; break;
						case "IsSindromGeriatry": this.str.IsSindromGeriatry = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "DrugObsNo":

							if (value == null || value is System.Int32)
								this.DrugObsNo = (System.Int32?)value;
							break;
						case "IsYes":

							if (value == null || value is System.Boolean)
								this.IsYes = (System.Boolean?)value;
							break;
						case "IsDrugDuplicate":

							if (value == null || value is System.Boolean)
								this.IsDrugDuplicate = (System.Boolean?)value;
							break;
						case "IsMoreThan7Days":

							if (value == null || value is System.Boolean)
								this.IsMoreThan7Days = (System.Boolean?)value;
							break;
						case "IsAgeMoreThan65y":

							if (value == null || value is System.Boolean)
								this.IsAgeMoreThan65y = (System.Boolean?)value;
							break;
						case "IsSindromGeriatry":

							if (value == null || value is System.Boolean)
								this.IsSindromGeriatry = (System.Boolean?)value;
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
		/// Maps to RegistrationDrugObsPto.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationDrugObsPtoMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(RegistrationDrugObsPtoMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObsPto.DrugObsNo
		/// </summary>
		virtual public System.Int32? DrugObsNo
		{
			get
			{
				return base.GetSystemInt32(RegistrationDrugObsPtoMetadata.ColumnNames.DrugObsNo);
			}

			set
			{
				base.SetSystemInt32(RegistrationDrugObsPtoMetadata.ColumnNames.DrugObsNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObsPto.SRPto
		/// </summary>
		virtual public System.String SRPto
		{
			get
			{
				return base.GetSystemString(RegistrationDrugObsPtoMetadata.ColumnNames.SRPto);
			}

			set
			{
				base.SetSystemString(RegistrationDrugObsPtoMetadata.ColumnNames.SRPto, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObsPto.IsYes
		/// </summary>
		virtual public System.Boolean? IsYes
		{
			get
			{
				return base.GetSystemBoolean(RegistrationDrugObsPtoMetadata.ColumnNames.IsYes);
			}

			set
			{
				base.SetSystemBoolean(RegistrationDrugObsPtoMetadata.ColumnNames.IsYes, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObsPto.YesNotes
		/// </summary>
		virtual public System.String YesNotes
		{
			get
			{
				return base.GetSystemString(RegistrationDrugObsPtoMetadata.ColumnNames.YesNotes);
			}

			set
			{
				base.SetSystemString(RegistrationDrugObsPtoMetadata.ColumnNames.YesNotes, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObsPto.IsDrugDuplicate
		/// </summary>
		virtual public System.Boolean? IsDrugDuplicate
		{
			get
			{
				return base.GetSystemBoolean(RegistrationDrugObsPtoMetadata.ColumnNames.IsDrugDuplicate);
			}

			set
			{
				base.SetSystemBoolean(RegistrationDrugObsPtoMetadata.ColumnNames.IsDrugDuplicate, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObsPto.IsMoreThan7Days
		/// </summary>
		virtual public System.Boolean? IsMoreThan7Days
		{
			get
			{
				return base.GetSystemBoolean(RegistrationDrugObsPtoMetadata.ColumnNames.IsMoreThan7Days);
			}

			set
			{
				base.SetSystemBoolean(RegistrationDrugObsPtoMetadata.ColumnNames.IsMoreThan7Days, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObsPto.IsAgeMoreThan65y
		/// </summary>
		virtual public System.Boolean? IsAgeMoreThan65y
		{
			get
			{
				return base.GetSystemBoolean(RegistrationDrugObsPtoMetadata.ColumnNames.IsAgeMoreThan65y);
			}

			set
			{
				base.SetSystemBoolean(RegistrationDrugObsPtoMetadata.ColumnNames.IsAgeMoreThan65y, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObsPto.IsSindromGeriatry
		/// </summary>
		virtual public System.Boolean? IsSindromGeriatry
		{
			get
			{
				return base.GetSystemBoolean(RegistrationDrugObsPtoMetadata.ColumnNames.IsSindromGeriatry);
			}

			set
			{
				base.SetSystemBoolean(RegistrationDrugObsPtoMetadata.ColumnNames.IsSindromGeriatry, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObsPto.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationDrugObsPtoMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationDrugObsPtoMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObsPto.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationDrugObsPtoMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationDrugObsPtoMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esRegistrationDrugObsPto entity)
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
			public System.String DrugObsNo
			{
				get
				{
					System.Int32? data = entity.DrugObsNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DrugObsNo = null;
					else entity.DrugObsNo = Convert.ToInt32(value);
				}
			}
			public System.String SRPto
			{
				get
				{
					System.String data = entity.SRPto;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPto = null;
					else entity.SRPto = Convert.ToString(value);
				}
			}
			public System.String IsYes
			{
				get
				{
					System.Boolean? data = entity.IsYes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsYes = null;
					else entity.IsYes = Convert.ToBoolean(value);
				}
			}
			public System.String YesNotes
			{
				get
				{
					System.String data = entity.YesNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.YesNotes = null;
					else entity.YesNotes = Convert.ToString(value);
				}
			}
			public System.String IsDrugDuplicate
			{
				get
				{
					System.Boolean? data = entity.IsDrugDuplicate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDrugDuplicate = null;
					else entity.IsDrugDuplicate = Convert.ToBoolean(value);
				}
			}
			public System.String IsMoreThan7Days
			{
				get
				{
					System.Boolean? data = entity.IsMoreThan7Days;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMoreThan7Days = null;
					else entity.IsMoreThan7Days = Convert.ToBoolean(value);
				}
			}
			public System.String IsAgeMoreThan65y
			{
				get
				{
					System.Boolean? data = entity.IsAgeMoreThan65y;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAgeMoreThan65y = null;
					else entity.IsAgeMoreThan65y = Convert.ToBoolean(value);
				}
			}
			public System.String IsSindromGeriatry
			{
				get
				{
					System.Boolean? data = entity.IsSindromGeriatry;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSindromGeriatry = null;
					else entity.IsSindromGeriatry = Convert.ToBoolean(value);
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
			private esRegistrationDrugObsPto entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationDrugObsPtoQuery query)
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
				throw new Exception("esRegistrationDrugObsPto can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationDrugObsPto : esRegistrationDrugObsPto
	{
	}

	[Serializable]
	abstract public class esRegistrationDrugObsPtoQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return RegistrationDrugObsPtoMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsPtoMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem DrugObsNo
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsPtoMetadata.ColumnNames.DrugObsNo, esSystemType.Int32);
			}
		}

		public esQueryItem SRPto
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsPtoMetadata.ColumnNames.SRPto, esSystemType.String);
			}
		}

		public esQueryItem IsYes
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsPtoMetadata.ColumnNames.IsYes, esSystemType.Boolean);
			}
		}

		public esQueryItem YesNotes
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsPtoMetadata.ColumnNames.YesNotes, esSystemType.String);
			}
		}

		public esQueryItem IsDrugDuplicate
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsPtoMetadata.ColumnNames.IsDrugDuplicate, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMoreThan7Days
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsPtoMetadata.ColumnNames.IsMoreThan7Days, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAgeMoreThan65y
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsPtoMetadata.ColumnNames.IsAgeMoreThan65y, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSindromGeriatry
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsPtoMetadata.ColumnNames.IsSindromGeriatry, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsPtoMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsPtoMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationDrugObsPtoCollection")]
	public partial class RegistrationDrugObsPtoCollection : esRegistrationDrugObsPtoCollection, IEnumerable<RegistrationDrugObsPto>
	{
		public RegistrationDrugObsPtoCollection()
		{

		}

		public static implicit operator List<RegistrationDrugObsPto>(RegistrationDrugObsPtoCollection coll)
		{
			List<RegistrationDrugObsPto> list = new List<RegistrationDrugObsPto>();

			foreach (RegistrationDrugObsPto emp in coll)
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
				return RegistrationDrugObsPtoMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationDrugObsPtoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationDrugObsPto(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationDrugObsPto();
		}

		#endregion

		[BrowsableAttribute(false)]
		public RegistrationDrugObsPtoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationDrugObsPtoQuery();
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
		public bool Load(RegistrationDrugObsPtoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationDrugObsPto AddNew()
		{
			RegistrationDrugObsPto entity = base.AddNewEntity() as RegistrationDrugObsPto;

			return entity;
		}
		public RegistrationDrugObsPto FindByPrimaryKey(String registrationNo, Int32 drugObsNo, String sRPto)
		{
			return base.FindByPrimaryKey(registrationNo, drugObsNo, sRPto) as RegistrationDrugObsPto;
		}

		#region IEnumerable< RegistrationDrugObsPto> Members

		IEnumerator<RegistrationDrugObsPto> IEnumerable<RegistrationDrugObsPto>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationDrugObsPto;
			}
		}

		#endregion

		private RegistrationDrugObsPtoQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationDrugObsPto' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationDrugObsPto ({RegistrationNo, DrugObsNo, SRPto})")]
	[Serializable]
	public partial class RegistrationDrugObsPto : esRegistrationDrugObsPto
	{
		public RegistrationDrugObsPto()
		{
		}

		public RegistrationDrugObsPto(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationDrugObsPtoMetadata.Meta();
			}
		}

		override protected esRegistrationDrugObsPtoQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationDrugObsPtoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public RegistrationDrugObsPtoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationDrugObsPtoQuery();
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
		public bool Load(RegistrationDrugObsPtoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private RegistrationDrugObsPtoQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationDrugObsPtoQuery : esRegistrationDrugObsPtoQuery
	{
		public RegistrationDrugObsPtoQuery()
		{

		}

		public RegistrationDrugObsPtoQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "RegistrationDrugObsPtoQuery";
		}
	}

	[Serializable]
	public partial class RegistrationDrugObsPtoMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationDrugObsPtoMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationDrugObsPtoMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationDrugObsPtoMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsPtoMetadata.ColumnNames.DrugObsNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationDrugObsPtoMetadata.PropertyNames.DrugObsNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsPtoMetadata.ColumnNames.SRPto, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationDrugObsPtoMetadata.PropertyNames.SRPto;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsPtoMetadata.ColumnNames.IsYes, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationDrugObsPtoMetadata.PropertyNames.IsYes;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsPtoMetadata.ColumnNames.YesNotes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationDrugObsPtoMetadata.PropertyNames.YesNotes;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsPtoMetadata.ColumnNames.IsDrugDuplicate, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationDrugObsPtoMetadata.PropertyNames.IsDrugDuplicate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsPtoMetadata.ColumnNames.IsMoreThan7Days, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationDrugObsPtoMetadata.PropertyNames.IsMoreThan7Days;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsPtoMetadata.ColumnNames.IsAgeMoreThan65y, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationDrugObsPtoMetadata.PropertyNames.IsAgeMoreThan65y;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsPtoMetadata.ColumnNames.IsSindromGeriatry, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationDrugObsPtoMetadata.PropertyNames.IsSindromGeriatry;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsPtoMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationDrugObsPtoMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsPtoMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationDrugObsPtoMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public RegistrationDrugObsPtoMetadata Meta()
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
			public const string DrugObsNo = "DrugObsNo";
			public const string SRPto = "SRPto";
			public const string IsYes = "IsYes";
			public const string YesNotes = "YesNotes";
			public const string IsDrugDuplicate = "IsDrugDuplicate";
			public const string IsMoreThan7Days = "IsMoreThan7Days";
			public const string IsAgeMoreThan65y = "IsAgeMoreThan65y";
			public const string IsSindromGeriatry = "IsSindromGeriatry";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string DrugObsNo = "DrugObsNo";
			public const string SRPto = "SRPto";
			public const string IsYes = "IsYes";
			public const string YesNotes = "YesNotes";
			public const string IsDrugDuplicate = "IsDrugDuplicate";
			public const string IsMoreThan7Days = "IsMoreThan7Days";
			public const string IsAgeMoreThan65y = "IsAgeMoreThan65y";
			public const string IsSindromGeriatry = "IsSindromGeriatry";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(RegistrationDrugObsPtoMetadata))
			{
				if (RegistrationDrugObsPtoMetadata.mapDelegates == null)
				{
					RegistrationDrugObsPtoMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (RegistrationDrugObsPtoMetadata.meta == null)
				{
					RegistrationDrugObsPtoMetadata.meta = new RegistrationDrugObsPtoMetadata();
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
				meta.AddTypeMap("DrugObsNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRPto", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsYes", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("YesNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDrugDuplicate", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMoreThan7Days", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAgeMoreThan65y", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSindromGeriatry", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));


				meta.Source = "RegistrationDrugObsPto";
				meta.Destination = "RegistrationDrugObsPto";
				meta.spInsert = "proc_RegistrationDrugObsPtoInsert";
				meta.spUpdate = "proc_RegistrationDrugObsPtoUpdate";
				meta.spDelete = "proc_RegistrationDrugObsPtoDelete";
				meta.spLoadAll = "proc_RegistrationDrugObsPtoLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationDrugObsPtoLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationDrugObsPtoMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
