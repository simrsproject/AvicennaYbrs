/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/18/2021 1:45:44 PM
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
	abstract public class esBloodBagNoCollection : esEntityCollectionWAuditLog
	{
		public esBloodBagNoCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "BloodBagNoCollection";
		}

		#region Query Logic
		protected void InitQuery(esBloodBagNoQuery query)
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
			this.InitQuery(query as esBloodBagNoQuery);
		}
		#endregion

		virtual public BloodBagNo DetachEntity(BloodBagNo entity)
		{
			return base.DetachEntity(entity) as BloodBagNo;
		}

		virtual public BloodBagNo AttachEntity(BloodBagNo entity)
		{
			return base.AttachEntity(entity) as BloodBagNo;
		}

		virtual public void Combine(BloodBagNoCollection collection)
		{
			base.Combine(collection);
		}

		new public BloodBagNo this[int index]
		{
			get
			{
				return base[index] as BloodBagNo;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BloodBagNo);
		}
	}

	[Serializable]
	abstract public class esBloodBagNo : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBloodBagNoQuery GetDynamicQuery()
		{
			return null;
		}

		public esBloodBagNo()
		{
		}

		public esBloodBagNo(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String bagNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bagNo);
			else
				return LoadByPrimaryKeyStoredProcedure(bagNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String bagNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bagNo);
			else
				return LoadByPrimaryKeyStoredProcedure(bagNo);
		}

		private bool LoadByPrimaryKeyDynamic(String bagNo)
		{
			esBloodBagNoQuery query = this.GetDynamicQuery();
			query.Where(query.BagNo == bagNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String bagNo)
		{
			esParameters parms = new esParameters();
			parms.Add("BagNo", bagNo);
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
						case "BagNo": this.str.BagNo = (string)value; break;
						case "SRBloodType": this.str.SRBloodType = (string)value; break;
						case "BloodRhesus": this.str.BloodRhesus = (string)value; break;
						case "SRBloodGroup": this.str.SRBloodGroup = (string)value; break;
						case "ExpiredDateTime": this.str.ExpiredDateTime = (string)value; break;
						case "IsCrossMatching": this.str.IsCrossMatching = (string)value; break;
						case "IsExtermination": this.str.IsExtermination = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "VolumeBag": this.str.VolumeBag = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ExpiredDateTime":

							if (value == null || value is System.DateTime)
								this.ExpiredDateTime = (System.DateTime?)value;
							break;
						case "IsCrossMatching":

							if (value == null || value is System.Boolean)
								this.IsCrossMatching = (System.Boolean?)value;
							break;
						case "IsExtermination":

							if (value == null || value is System.Boolean)
								this.IsExtermination = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "VolumeBag":

							if (value == null || value is System.Decimal)
								this.VolumeBag = (System.Decimal?)value;
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
		/// Maps to BloodBagNo.BagNo
		/// </summary>
		virtual public System.String BagNo
		{
			get
			{
				return base.GetSystemString(BloodBagNoMetadata.ColumnNames.BagNo);
			}

			set
			{
				base.SetSystemString(BloodBagNoMetadata.ColumnNames.BagNo, value);
			}
		}
		/// <summary>
		/// Maps to BloodBagNo.SRBloodType
		/// </summary>
		virtual public System.String SRBloodType
		{
			get
			{
				return base.GetSystemString(BloodBagNoMetadata.ColumnNames.SRBloodType);
			}

			set
			{
				base.SetSystemString(BloodBagNoMetadata.ColumnNames.SRBloodType, value);
			}
		}
		/// <summary>
		/// Maps to BloodBagNo.BloodRhesus
		/// </summary>
		virtual public System.String BloodRhesus
		{
			get
			{
				return base.GetSystemString(BloodBagNoMetadata.ColumnNames.BloodRhesus);
			}

			set
			{
				base.SetSystemString(BloodBagNoMetadata.ColumnNames.BloodRhesus, value);
			}
		}
		/// <summary>
		/// Maps to BloodBagNo.SRBloodGroup
		/// </summary>
		virtual public System.String SRBloodGroup
		{
			get
			{
				return base.GetSystemString(BloodBagNoMetadata.ColumnNames.SRBloodGroup);
			}

			set
			{
				base.SetSystemString(BloodBagNoMetadata.ColumnNames.SRBloodGroup, value);
			}
		}
		/// <summary>
		/// Maps to BloodBagNo.ExpiredDateTime
		/// </summary>
		virtual public System.DateTime? ExpiredDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodBagNoMetadata.ColumnNames.ExpiredDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodBagNoMetadata.ColumnNames.ExpiredDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodBagNo.IsCrossMatching
		/// </summary>
		virtual public System.Boolean? IsCrossMatching
		{
			get
			{
				return base.GetSystemBoolean(BloodBagNoMetadata.ColumnNames.IsCrossMatching);
			}

			set
			{
				base.SetSystemBoolean(BloodBagNoMetadata.ColumnNames.IsCrossMatching, value);
			}
		}
		/// <summary>
		/// Maps to BloodBagNo.IsExtermination
		/// </summary>
		virtual public System.Boolean? IsExtermination
		{
			get
			{
				return base.GetSystemBoolean(BloodBagNoMetadata.ColumnNames.IsExtermination);
			}

			set
			{
				base.SetSystemBoolean(BloodBagNoMetadata.ColumnNames.IsExtermination, value);
			}
		}
		/// <summary>
		/// Maps to BloodBagNo.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodBagNoMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodBagNoMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodBagNo.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BloodBagNoMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(BloodBagNoMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BloodBagNo.VolumeBag
		/// </summary>
		virtual public System.Decimal? VolumeBag
		{
			get
			{
				return base.GetSystemDecimal(BloodBagNoMetadata.ColumnNames.VolumeBag);
			}

			set
			{
				base.SetSystemDecimal(BloodBagNoMetadata.ColumnNames.VolumeBag, value);
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
			public esStrings(esBloodBagNo entity)
			{
				this.entity = entity;
			}
			public System.String BagNo
			{
				get
				{
					System.String data = entity.BagNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BagNo = null;
					else entity.BagNo = Convert.ToString(value);
				}
			}
			public System.String SRBloodType
			{
				get
				{
					System.String data = entity.SRBloodType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBloodType = null;
					else entity.SRBloodType = Convert.ToString(value);
				}
			}
			public System.String BloodRhesus
			{
				get
				{
					System.String data = entity.BloodRhesus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BloodRhesus = null;
					else entity.BloodRhesus = Convert.ToString(value);
				}
			}
			public System.String SRBloodGroup
			{
				get
				{
					System.String data = entity.SRBloodGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBloodGroup = null;
					else entity.SRBloodGroup = Convert.ToString(value);
				}
			}
			public System.String ExpiredDateTime
			{
				get
				{
					System.DateTime? data = entity.ExpiredDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExpiredDateTime = null;
					else entity.ExpiredDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String IsCrossMatching
			{
				get
				{
					System.Boolean? data = entity.IsCrossMatching;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCrossMatching = null;
					else entity.IsCrossMatching = Convert.ToBoolean(value);
				}
			}
			public System.String IsExtermination
			{
				get
				{
					System.Boolean? data = entity.IsExtermination;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsExtermination = null;
					else entity.IsExtermination = Convert.ToBoolean(value);
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
			public System.String VolumeBag
			{
				get
				{
					System.Decimal? data = entity.VolumeBag;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VolumeBag = null;
					else entity.VolumeBag = Convert.ToDecimal(value);
				}
			}
			private esBloodBagNo entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBloodBagNoQuery query)
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
				throw new Exception("esBloodBagNo can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class BloodBagNo : esBloodBagNo
	{
	}

	[Serializable]
	abstract public class esBloodBagNoQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return BloodBagNoMetadata.Meta();
			}
		}

		public esQueryItem BagNo
		{
			get
			{
				return new esQueryItem(this, BloodBagNoMetadata.ColumnNames.BagNo, esSystemType.String);
			}
		}

		public esQueryItem SRBloodType
		{
			get
			{
				return new esQueryItem(this, BloodBagNoMetadata.ColumnNames.SRBloodType, esSystemType.String);
			}
		}

		public esQueryItem BloodRhesus
		{
			get
			{
				return new esQueryItem(this, BloodBagNoMetadata.ColumnNames.BloodRhesus, esSystemType.String);
			}
		}

		public esQueryItem SRBloodGroup
		{
			get
			{
				return new esQueryItem(this, BloodBagNoMetadata.ColumnNames.SRBloodGroup, esSystemType.String);
			}
		}

		public esQueryItem ExpiredDateTime
		{
			get
			{
				return new esQueryItem(this, BloodBagNoMetadata.ColumnNames.ExpiredDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem IsCrossMatching
		{
			get
			{
				return new esQueryItem(this, BloodBagNoMetadata.ColumnNames.IsCrossMatching, esSystemType.Boolean);
			}
		}

		public esQueryItem IsExtermination
		{
			get
			{
				return new esQueryItem(this, BloodBagNoMetadata.ColumnNames.IsExtermination, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BloodBagNoMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BloodBagNoMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem VolumeBag
		{
			get
			{
				return new esQueryItem(this, BloodBagNoMetadata.ColumnNames.VolumeBag, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BloodBagNoCollection")]
	public partial class BloodBagNoCollection : esBloodBagNoCollection, IEnumerable<BloodBagNo>
	{
		public BloodBagNoCollection()
		{

		}

		public static implicit operator List<BloodBagNo>(BloodBagNoCollection coll)
		{
			List<BloodBagNo> list = new List<BloodBagNo>();

			foreach (BloodBagNo emp in coll)
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
				return BloodBagNoMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BloodBagNoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BloodBagNo(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BloodBagNo();
		}

		#endregion

		[BrowsableAttribute(false)]
		public BloodBagNoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BloodBagNoQuery();
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
		public bool Load(BloodBagNoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public BloodBagNo AddNew()
		{
			BloodBagNo entity = base.AddNewEntity() as BloodBagNo;

			return entity;
		}
		public BloodBagNo FindByPrimaryKey(String bagNo)
		{
			return base.FindByPrimaryKey(bagNo) as BloodBagNo;
		}

		#region IEnumerable< BloodBagNo> Members

		IEnumerator<BloodBagNo> IEnumerable<BloodBagNo>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as BloodBagNo;
			}
		}

		#endregion

		private BloodBagNoQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BloodBagNo' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("BloodBagNo ({BagNo})")]
	[Serializable]
	public partial class BloodBagNo : esBloodBagNo
	{
		public BloodBagNo()
		{
		}

		public BloodBagNo(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BloodBagNoMetadata.Meta();
			}
		}

		override protected esBloodBagNoQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BloodBagNoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public BloodBagNoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BloodBagNoQuery();
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
		public bool Load(BloodBagNoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private BloodBagNoQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BloodBagNoQuery : esBloodBagNoQuery
	{
		public BloodBagNoQuery()
		{

		}

		public BloodBagNoQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "BloodBagNoQuery";
		}
	}

	[Serializable]
	public partial class BloodBagNoMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BloodBagNoMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BloodBagNoMetadata.ColumnNames.BagNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBagNoMetadata.PropertyNames.BagNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBagNoMetadata.ColumnNames.SRBloodType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBagNoMetadata.PropertyNames.SRBloodType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBagNoMetadata.ColumnNames.BloodRhesus, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBagNoMetadata.PropertyNames.BloodRhesus;
			c.CharacterMaxLength = 1;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBagNoMetadata.ColumnNames.SRBloodGroup, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBagNoMetadata.PropertyNames.SRBloodGroup;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBagNoMetadata.ColumnNames.ExpiredDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodBagNoMetadata.PropertyNames.ExpiredDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBagNoMetadata.ColumnNames.IsCrossMatching, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBagNoMetadata.PropertyNames.IsCrossMatching;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBagNoMetadata.ColumnNames.IsExtermination, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBagNoMetadata.PropertyNames.IsExtermination;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBagNoMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodBagNoMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBagNoMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBagNoMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBagNoMetadata.ColumnNames.VolumeBag, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBagNoMetadata.PropertyNames.VolumeBag;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public BloodBagNoMetadata Meta()
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
			public const string BagNo = "BagNo";
			public const string SRBloodType = "SRBloodType";
			public const string BloodRhesus = "BloodRhesus";
			public const string SRBloodGroup = "SRBloodGroup";
			public const string ExpiredDateTime = "ExpiredDateTime";
			public const string IsCrossMatching = "IsCrossMatching";
			public const string IsExtermination = "IsExtermination";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string VolumeBag = "VolumeBag";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string BagNo = "BagNo";
			public const string SRBloodType = "SRBloodType";
			public const string BloodRhesus = "BloodRhesus";
			public const string SRBloodGroup = "SRBloodGroup";
			public const string ExpiredDateTime = "ExpiredDateTime";
			public const string IsCrossMatching = "IsCrossMatching";
			public const string IsExtermination = "IsExtermination";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string VolumeBag = "VolumeBag";
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
			lock (typeof(BloodBagNoMetadata))
			{
				if (BloodBagNoMetadata.mapDelegates == null)
				{
					BloodBagNoMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (BloodBagNoMetadata.meta == null)
				{
					BloodBagNoMetadata.meta = new BloodBagNoMetadata();
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

				meta.AddTypeMap("BagNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBloodType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BloodRhesus", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("SRBloodGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExpiredDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsCrossMatching", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsExtermination", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VolumeBag", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "BloodBagNo";
				meta.Destination = "BloodBagNo";
				meta.spInsert = "proc_BloodBagNoInsert";
				meta.spUpdate = "proc_BloodBagNoUpdate";
				meta.spDelete = "proc_BloodBagNoDelete";
				meta.spLoadAll = "proc_BloodBagNoLoadAll";
				meta.spLoadByPrimaryKey = "proc_BloodBagNoLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BloodBagNoMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
