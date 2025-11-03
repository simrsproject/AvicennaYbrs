/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/30/2022 11:42:56 AM
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
	abstract public class esMedicalDischargeSummaryBodyDiagramBakCollection : esEntityCollectionWAuditLog
	{
		public esMedicalDischargeSummaryBodyDiagramBakCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MedicalDischargeSummaryBodyDiagramBakCollection";
		}

		#region Query Logic
		protected void InitQuery(esMedicalDischargeSummaryBodyDiagramBakQuery query)
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
			this.InitQuery(query as esMedicalDischargeSummaryBodyDiagramBakQuery);
		}
		#endregion

		virtual public MedicalDischargeSummaryBodyDiagramBak DetachEntity(MedicalDischargeSummaryBodyDiagramBak entity)
		{
			return base.DetachEntity(entity) as MedicalDischargeSummaryBodyDiagramBak;
		}

		virtual public MedicalDischargeSummaryBodyDiagramBak AttachEntity(MedicalDischargeSummaryBodyDiagramBak entity)
		{
			return base.AttachEntity(entity) as MedicalDischargeSummaryBodyDiagramBak;
		}

		virtual public void Combine(MedicalDischargeSummaryBodyDiagramBakCollection collection)
		{
			base.Combine(collection);
		}

		new public MedicalDischargeSummaryBodyDiagramBak this[int index]
		{
			get
			{
				return base[index] as MedicalDischargeSummaryBodyDiagramBak;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicalDischargeSummaryBodyDiagramBak);
		}
	}

	[Serializable]
	abstract public class esMedicalDischargeSummaryBodyDiagramBak : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicalDischargeSummaryBodyDiagramBakQuery GetDynamicQuery()
		{
			return null;
		}

		public esMedicalDischargeSummaryBodyDiagramBak()
		{
		}

		public esMedicalDischargeSummaryBodyDiagramBak(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, String bodyID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, bodyID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, bodyID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String bodyID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, bodyID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, bodyID);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, String bodyID)
		{
			esMedicalDischargeSummaryBodyDiagramBakQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.BodyID == bodyID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String bodyID)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("BodyID", bodyID);
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
						case "BodyID": this.str.BodyID = (string)value; break;
						case "IsDeleted": this.str.IsDeleted = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "BodyImage":

							if (value == null || value is System.Byte[])
								this.BodyImage = (System.Byte[])value;
							break;
						case "IsDeleted":

							if (value == null || value is System.Boolean)
								this.IsDeleted = (System.Boolean?)value;
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
		/// Maps to MedicalDischargeSummaryBodyDiagramBak.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryBodyDiagramBak.BodyID
		/// </summary>
		virtual public System.String BodyID
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.BodyID);
			}

			set
			{
				base.SetSystemString(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.BodyID, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryBodyDiagramBak.BodyImage
		/// </summary>
		virtual public System.Byte[] BodyImage
		{
			get
			{
				return base.GetSystemByteArray(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.BodyImage);
			}

			set
			{
				base.SetSystemByteArray(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.BodyImage, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryBodyDiagramBak.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.IsDeleted);
			}

			set
			{
				base.SetSystemBoolean(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryBodyDiagramBak.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryBodyDiagramBak.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryBodyDiagramBak.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryBodyDiagramBak.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryBodyDiagramBak.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.Notes, value);
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
			public esStrings(esMedicalDischargeSummaryBodyDiagramBak entity)
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
			public System.String BodyID
			{
				get
				{
					System.String data = entity.BodyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BodyID = null;
					else entity.BodyID = Convert.ToString(value);
				}
			}
			public System.String IsDeleted
			{
				get
				{
					System.Boolean? data = entity.IsDeleted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDeleted = null;
					else entity.IsDeleted = Convert.ToBoolean(value);
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
			private esMedicalDischargeSummaryBodyDiagramBak entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicalDischargeSummaryBodyDiagramBakQuery query)
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
				throw new Exception("esMedicalDischargeSummaryBodyDiagramBak can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MedicalDischargeSummaryBodyDiagramBak : esMedicalDischargeSummaryBodyDiagramBak
	{
	}

	[Serializable]
	abstract public class esMedicalDischargeSummaryBodyDiagramBakQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MedicalDischargeSummaryBodyDiagramBakMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem BodyID
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.BodyID, esSystemType.String);
			}
		}

		public esQueryItem BodyImage
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.BodyImage, esSystemType.ByteArray);
			}
		}

		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicalDischargeSummaryBodyDiagramBakCollection")]
	public partial class MedicalDischargeSummaryBodyDiagramBakCollection : esMedicalDischargeSummaryBodyDiagramBakCollection, IEnumerable<MedicalDischargeSummaryBodyDiagramBak>
	{
		public MedicalDischargeSummaryBodyDiagramBakCollection()
		{

		}

		public static implicit operator List<MedicalDischargeSummaryBodyDiagramBak>(MedicalDischargeSummaryBodyDiagramBakCollection coll)
		{
			List<MedicalDischargeSummaryBodyDiagramBak> list = new List<MedicalDischargeSummaryBodyDiagramBak>();

			foreach (MedicalDischargeSummaryBodyDiagramBak emp in coll)
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
				return MedicalDischargeSummaryBodyDiagramBakMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalDischargeSummaryBodyDiagramBakQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicalDischargeSummaryBodyDiagramBak(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicalDischargeSummaryBodyDiagramBak();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MedicalDischargeSummaryBodyDiagramBakQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalDischargeSummaryBodyDiagramBakQuery();
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
		public bool Load(MedicalDischargeSummaryBodyDiagramBakQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MedicalDischargeSummaryBodyDiagramBak AddNew()
		{
			MedicalDischargeSummaryBodyDiagramBak entity = base.AddNewEntity() as MedicalDischargeSummaryBodyDiagramBak;

			return entity;
		}
		public MedicalDischargeSummaryBodyDiagramBak FindByPrimaryKey(String registrationNo, String bodyID)
		{
			return base.FindByPrimaryKey(registrationNo, bodyID) as MedicalDischargeSummaryBodyDiagramBak;
		}

		#region IEnumerable< MedicalDischargeSummaryBodyDiagramBak> Members

		IEnumerator<MedicalDischargeSummaryBodyDiagramBak> IEnumerable<MedicalDischargeSummaryBodyDiagramBak>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MedicalDischargeSummaryBodyDiagramBak;
			}
		}

		#endregion

		private MedicalDischargeSummaryBodyDiagramBakQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicalDischargeSummaryBodyDiagramBak' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MedicalDischargeSummaryBodyDiagramBak ({RegistrationNo, BodyID})")]
	[Serializable]
	public partial class MedicalDischargeSummaryBodyDiagramBak : esMedicalDischargeSummaryBodyDiagramBak
	{
		public MedicalDischargeSummaryBodyDiagramBak()
		{
		}

		public MedicalDischargeSummaryBodyDiagramBak(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicalDischargeSummaryBodyDiagramBakMetadata.Meta();
			}
		}

		override protected esMedicalDischargeSummaryBodyDiagramBakQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalDischargeSummaryBodyDiagramBakQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MedicalDischargeSummaryBodyDiagramBakQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalDischargeSummaryBodyDiagramBakQuery();
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
		public bool Load(MedicalDischargeSummaryBodyDiagramBakQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MedicalDischargeSummaryBodyDiagramBakQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MedicalDischargeSummaryBodyDiagramBakQuery : esMedicalDischargeSummaryBodyDiagramBakQuery
	{
		public MedicalDischargeSummaryBodyDiagramBakQuery()
		{

		}

		public MedicalDischargeSummaryBodyDiagramBakQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MedicalDischargeSummaryBodyDiagramBakQuery";
		}
	}

	[Serializable]
	public partial class MedicalDischargeSummaryBodyDiagramBakMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicalDischargeSummaryBodyDiagramBakMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryBodyDiagramBakMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.BodyID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryBodyDiagramBakMetadata.PropertyNames.BodyID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.BodyImage, 2, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = MedicalDischargeSummaryBodyDiagramBakMetadata.PropertyNames.BodyImage;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.IsDeleted, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalDischargeSummaryBodyDiagramBakMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.CreatedDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalDischargeSummaryBodyDiagramBakMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.CreatedByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryBodyDiagramBakMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalDischargeSummaryBodyDiagramBakMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryBodyDiagramBakMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalDischargeSummaryBodyDiagramBakMetadata.ColumnNames.Notes, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryBodyDiagramBakMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public MedicalDischargeSummaryBodyDiagramBakMetadata Meta()
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
			public const string BodyID = "BodyID";
			public const string BodyImage = "BodyImage";
			public const string IsDeleted = "IsDeleted";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Notes = "Notes";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string BodyID = "BodyID";
			public const string BodyImage = "BodyImage";
			public const string IsDeleted = "IsDeleted";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Notes = "Notes";
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
			lock (typeof(MedicalDischargeSummaryBodyDiagramBakMetadata))
			{
				if (MedicalDischargeSummaryBodyDiagramBakMetadata.mapDelegates == null)
				{
					MedicalDischargeSummaryBodyDiagramBakMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MedicalDischargeSummaryBodyDiagramBakMetadata.meta == null)
				{
					MedicalDischargeSummaryBodyDiagramBakMetadata.meta = new MedicalDischargeSummaryBodyDiagramBakMetadata();
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
				meta.AddTypeMap("BodyID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BodyImage", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));


				meta.Source = "MedicalDischargeSummaryBodyDiagramBak";
				meta.Destination = "MedicalDischargeSummaryBodyDiagramBak";
				meta.spInsert = "proc_MedicalDischargeSummaryBodyDiagramBakInsert";
				meta.spUpdate = "proc_MedicalDischargeSummaryBodyDiagramBakUpdate";
				meta.spDelete = "proc_MedicalDischargeSummaryBodyDiagramBakDelete";
				meta.spLoadAll = "proc_MedicalDischargeSummaryBodyDiagramBakLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicalDischargeSummaryBodyDiagramBakLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicalDischargeSummaryBodyDiagramBakMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
