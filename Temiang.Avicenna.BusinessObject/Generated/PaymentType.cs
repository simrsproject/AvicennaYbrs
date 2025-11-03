/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/8/2022 8:52:32 PM
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
	abstract public class esPaymentTypeCollection : esEntityCollectionWAuditLog
	{
		public esPaymentTypeCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PaymentTypeCollection";
		}

		#region Query Logic
		protected void InitQuery(esPaymentTypeQuery query)
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
			this.InitQuery(query as esPaymentTypeQuery);
		}
		#endregion

		virtual public PaymentType DetachEntity(PaymentType entity)
		{
			return base.DetachEntity(entity) as PaymentType;
		}

		virtual public PaymentType AttachEntity(PaymentType entity)
		{
			return base.AttachEntity(entity) as PaymentType;
		}

		virtual public void Combine(PaymentTypeCollection collection)
		{
			base.Combine(collection);
		}

		new public PaymentType this[int index]
		{
			get
			{
				return base[index] as PaymentType;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PaymentType);
		}
	}

	[Serializable]
	abstract public class esPaymentType : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPaymentTypeQuery GetDynamicQuery()
		{
			return null;
		}

		public esPaymentType()
		{
		}

		public esPaymentType(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String sRPaymentTypeID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRPaymentTypeID);
			else
				return LoadByPrimaryKeyStoredProcedure(sRPaymentTypeID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sRPaymentTypeID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRPaymentTypeID);
			else
				return LoadByPrimaryKeyStoredProcedure(sRPaymentTypeID);
		}

		private bool LoadByPrimaryKeyDynamic(String sRPaymentTypeID)
		{
			esPaymentTypeQuery query = this.GetDynamicQuery();
			query.Where(query.SRPaymentTypeID == sRPaymentTypeID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String sRPaymentTypeID)
		{
			esParameters parms = new esParameters();
			parms.Add("SRPaymentTypeID", sRPaymentTypeID);
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
						case "SRPaymentTypeID": this.str.SRPaymentTypeID = (string)value; break;
						case "PaymentTypeName": this.str.PaymentTypeName = (string)value; break;
						case "ChartOfAccountID": this.str.ChartOfAccountID = (string)value; break;
						case "SubledgerID": this.str.SubledgerID = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "IsCashierFrontOffice": this.str.IsCashierFrontOffice = (string)value; break;
						case "IsArPayment": this.str.IsArPayment = (string)value; break;
						case "IsApPayment": this.str.IsApPayment = (string)value; break;
						case "IsFeePayment": this.str.IsFeePayment = (string)value; break;
						case "IsAssetAuctionPayment": this.str.IsAssetAuctionPayment = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ChartOfAccountID":

							if (value == null || value is System.Int32)
								this.ChartOfAccountID = (System.Int32?)value;
							break;
						case "SubledgerID":

							if (value == null || value is System.Int32)
								this.SubledgerID = (System.Int32?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "isCashierFrontOffice":

							if (value == null || value is System.Boolean)
								this.IsCashierFrontOffice = (System.Boolean?)value;
							break;
						case "IsArPayment":

							if (value == null || value is System.Boolean)
								this.IsArPayment = (System.Boolean?)value;
							break;
						case "IsApPayment":

							if (value == null || value is System.Boolean)
								this.IsApPayment = (System.Boolean?)value;
							break;
						case "IsFeePayment":

							if (value == null || value is System.Boolean)
								this.IsFeePayment = (System.Boolean?)value;
							break;
						case "IsAssetAuctionPayment":

							if (value == null || value is System.Boolean)
								this.IsAssetAuctionPayment = (System.Boolean?)value;
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
		/// Maps to PaymentType.SRPaymentTypeID
		/// </summary>
		virtual public System.String SRPaymentTypeID
		{
			get
			{
				return base.GetSystemString(PaymentTypeMetadata.ColumnNames.SRPaymentTypeID);
			}

			set
			{
				base.SetSystemString(PaymentTypeMetadata.ColumnNames.SRPaymentTypeID, value);
			}
		}
		/// <summary>
		/// Maps to PaymentType.PaymentTypeName
		/// </summary>
		virtual public System.String PaymentTypeName
		{
			get
			{
				return base.GetSystemString(PaymentTypeMetadata.ColumnNames.PaymentTypeName);
			}

			set
			{
				base.SetSystemString(PaymentTypeMetadata.ColumnNames.PaymentTypeName, value);
			}
		}
		/// <summary>
		/// Maps to PaymentType.ChartOfAccountID
		/// </summary>
		virtual public System.Int32? ChartOfAccountID
		{
			get
			{
				return base.GetSystemInt32(PaymentTypeMetadata.ColumnNames.ChartOfAccountID);
			}

			set
			{
				base.SetSystemInt32(PaymentTypeMetadata.ColumnNames.ChartOfAccountID, value);
			}
		}
		/// <summary>
		/// Maps to PaymentType.SubledgerID
		/// </summary>
		virtual public System.Int32? SubledgerID
		{
			get
			{
				return base.GetSystemInt32(PaymentTypeMetadata.ColumnNames.SubledgerID);
			}

			set
			{
				base.SetSystemInt32(PaymentTypeMetadata.ColumnNames.SubledgerID, value);
			}
		}
		/// <summary>
		/// Maps to PaymentType.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PaymentTypeMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PaymentTypeMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PaymentType.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PaymentTypeMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PaymentTypeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PaymentType.isCashierFrontOffice
		/// </summary>
		virtual public System.Boolean? IsCashierFrontOffice
		{
			get
			{
				return base.GetSystemBoolean(PaymentTypeMetadata.ColumnNames.IsCashierFrontOffice);
			}

			set
			{
				base.SetSystemBoolean(PaymentTypeMetadata.ColumnNames.IsCashierFrontOffice, value);
			}
		}
		/// <summary>
		/// Maps to PaymentType.IsArPayment
		/// </summary>
		virtual public System.Boolean? IsArPayment
		{
			get
			{
				return base.GetSystemBoolean(PaymentTypeMetadata.ColumnNames.IsArPayment);
			}

			set
			{
				base.SetSystemBoolean(PaymentTypeMetadata.ColumnNames.IsArPayment, value);
			}
		}
		/// <summary>
		/// Maps to PaymentType.IsApPayment
		/// </summary>
		virtual public System.Boolean? IsApPayment
		{
			get
			{
				return base.GetSystemBoolean(PaymentTypeMetadata.ColumnNames.IsApPayment);
			}

			set
			{
				base.SetSystemBoolean(PaymentTypeMetadata.ColumnNames.IsApPayment, value);
			}
		}
		/// <summary>
		/// Maps to PaymentType.IsFeePayment
		/// </summary>
		virtual public System.Boolean? IsFeePayment
		{
			get
			{
				return base.GetSystemBoolean(PaymentTypeMetadata.ColumnNames.IsFeePayment);
			}

			set
			{
				base.SetSystemBoolean(PaymentTypeMetadata.ColumnNames.IsFeePayment, value);
			}
		}
		/// <summary>
		/// Maps to PaymentType.IsAssetAuctionPayment
		/// </summary>
		virtual public System.Boolean? IsAssetAuctionPayment
		{
			get
			{
				return base.GetSystemBoolean(PaymentTypeMetadata.ColumnNames.IsAssetAuctionPayment);
			}

			set
			{
				base.SetSystemBoolean(PaymentTypeMetadata.ColumnNames.IsAssetAuctionPayment, value);
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
			public esStrings(esPaymentType entity)
			{
				this.entity = entity;
			}
			public System.String SRPaymentTypeID
			{
				get
				{
					System.String data = entity.SRPaymentTypeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPaymentTypeID = null;
					else entity.SRPaymentTypeID = Convert.ToString(value);
				}
			}
			public System.String PaymentTypeName
			{
				get
				{
					System.String data = entity.PaymentTypeName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentTypeName = null;
					else entity.PaymentTypeName = Convert.ToString(value);
				}
			}
			public System.String ChartOfAccountID
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountID = null;
					else entity.ChartOfAccountID = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerID
			{
				get
				{
					System.Int32? data = entity.SubledgerID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerID = null;
					else entity.SubledgerID = Convert.ToInt32(value);
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
			public System.String IsCashierFrontOffice
			{
				get
				{
					System.Boolean? data = entity.IsCashierFrontOffice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCashierFrontOffice = null;
					else entity.IsCashierFrontOffice = Convert.ToBoolean(value);
				}
			}
			public System.String IsArPayment
			{
				get
				{
					System.Boolean? data = entity.IsArPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsArPayment = null;
					else entity.IsArPayment = Convert.ToBoolean(value);
				}
			}
			public System.String IsApPayment
			{
				get
				{
					System.Boolean? data = entity.IsApPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApPayment = null;
					else entity.IsApPayment = Convert.ToBoolean(value);
				}
			}
			public System.String IsFeePayment
			{
				get
				{
					System.Boolean? data = entity.IsFeePayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeePayment = null;
					else entity.IsFeePayment = Convert.ToBoolean(value);
				}
			}
			public System.String IsAssetAuctionPayment
			{
				get
				{
					System.Boolean? data = entity.IsAssetAuctionPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAssetAuctionPayment = null;
					else entity.IsAssetAuctionPayment = Convert.ToBoolean(value);
				}
			}
			private esPaymentType entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPaymentTypeQuery query)
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
				throw new Exception("esPaymentType can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PaymentType : esPaymentType
	{
	}

	[Serializable]
	abstract public class esPaymentTypeQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PaymentTypeMetadata.Meta();
			}
		}

		public esQueryItem SRPaymentTypeID
		{
			get
			{
				return new esQueryItem(this, PaymentTypeMetadata.ColumnNames.SRPaymentTypeID, esSystemType.String);
			}
		}

		public esQueryItem PaymentTypeName
		{
			get
			{
				return new esQueryItem(this, PaymentTypeMetadata.ColumnNames.PaymentTypeName, esSystemType.String);
			}
		}

		public esQueryItem ChartOfAccountID
		{
			get
			{
				return new esQueryItem(this, PaymentTypeMetadata.ColumnNames.ChartOfAccountID, esSystemType.Int32);
			}
		}

		public esQueryItem SubledgerID
		{
			get
			{
				return new esQueryItem(this, PaymentTypeMetadata.ColumnNames.SubledgerID, esSystemType.Int32);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PaymentTypeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PaymentTypeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem IsCashierFrontOffice
		{
			get
			{
				return new esQueryItem(this, PaymentTypeMetadata.ColumnNames.IsCashierFrontOffice, esSystemType.Boolean);
			}
		}

		public esQueryItem IsArPayment
		{
			get
			{
				return new esQueryItem(this, PaymentTypeMetadata.ColumnNames.IsArPayment, esSystemType.Boolean);
			}
		}

		public esQueryItem IsApPayment
		{
			get
			{
				return new esQueryItem(this, PaymentTypeMetadata.ColumnNames.IsApPayment, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFeePayment
		{
			get
			{
				return new esQueryItem(this, PaymentTypeMetadata.ColumnNames.IsFeePayment, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAssetAuctionPayment
		{
			get
			{
				return new esQueryItem(this, PaymentTypeMetadata.ColumnNames.IsAssetAuctionPayment, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PaymentTypeCollection")]
	public partial class PaymentTypeCollection : esPaymentTypeCollection, IEnumerable<PaymentType>
	{
		public PaymentTypeCollection()
		{

		}

		public static implicit operator List<PaymentType>(PaymentTypeCollection coll)
		{
			List<PaymentType> list = new List<PaymentType>();

			foreach (PaymentType emp in coll)
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
				return PaymentTypeMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PaymentTypeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PaymentType(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PaymentType();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PaymentTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PaymentTypeQuery();
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
		public bool Load(PaymentTypeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PaymentType AddNew()
		{
			PaymentType entity = base.AddNewEntity() as PaymentType;

			return entity;
		}
		public PaymentType FindByPrimaryKey(String sRPaymentTypeID)
		{
			return base.FindByPrimaryKey(sRPaymentTypeID) as PaymentType;
		}

		#region IEnumerable< PaymentType> Members

		IEnumerator<PaymentType> IEnumerable<PaymentType>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PaymentType;
			}
		}

		#endregion

		private PaymentTypeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PaymentType' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PaymentType ({SRPaymentTypeID})")]
	[Serializable]
	public partial class PaymentType : esPaymentType
	{
		public PaymentType()
		{
		}

		public PaymentType(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PaymentTypeMetadata.Meta();
			}
		}

		override protected esPaymentTypeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PaymentTypeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PaymentTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PaymentTypeQuery();
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
		public bool Load(PaymentTypeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PaymentTypeQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PaymentTypeQuery : esPaymentTypeQuery
	{
		public PaymentTypeQuery()
		{

		}

		public PaymentTypeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PaymentTypeQuery";
		}
	}

	[Serializable]
	public partial class PaymentTypeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PaymentTypeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PaymentTypeMetadata.ColumnNames.SRPaymentTypeID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PaymentTypeMetadata.PropertyNames.SRPaymentTypeID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PaymentTypeMetadata.ColumnNames.PaymentTypeName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PaymentTypeMetadata.PropertyNames.PaymentTypeName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(PaymentTypeMetadata.ColumnNames.ChartOfAccountID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PaymentTypeMetadata.PropertyNames.ChartOfAccountID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PaymentTypeMetadata.ColumnNames.SubledgerID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PaymentTypeMetadata.PropertyNames.SubledgerID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PaymentTypeMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PaymentTypeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PaymentTypeMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PaymentTypeMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PaymentTypeMetadata.ColumnNames.IsCashierFrontOffice, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PaymentTypeMetadata.PropertyNames.IsCashierFrontOffice;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);

			c = new esColumnMetadata(PaymentTypeMetadata.ColumnNames.IsArPayment, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PaymentTypeMetadata.PropertyNames.IsArPayment;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PaymentTypeMetadata.ColumnNames.IsApPayment, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PaymentTypeMetadata.PropertyNames.IsApPayment;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PaymentTypeMetadata.ColumnNames.IsFeePayment, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PaymentTypeMetadata.PropertyNames.IsFeePayment;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PaymentTypeMetadata.ColumnNames.IsAssetAuctionPayment, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PaymentTypeMetadata.PropertyNames.IsAssetAuctionPayment;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PaymentTypeMetadata Meta()
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
			public const string SRPaymentTypeID = "SRPaymentTypeID";
			public const string PaymentTypeName = "PaymentTypeName";
			public const string ChartOfAccountID = "ChartOfAccountID";
			public const string SubledgerID = "SubledgerID";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsCashierFrontOffice = "isCashierFrontOffice";
			public const string IsArPayment = "IsArPayment";
			public const string IsApPayment = "IsApPayment";
			public const string IsFeePayment = "IsFeePayment";
			public const string IsAssetAuctionPayment = "IsAssetAuctionPayment";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string SRPaymentTypeID = "SRPaymentTypeID";
			public const string PaymentTypeName = "PaymentTypeName";
			public const string ChartOfAccountID = "ChartOfAccountID";
			public const string SubledgerID = "SubledgerID";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsCashierFrontOffice = "IsCashierFrontOffice";
			public const string IsArPayment = "IsArPayment";
			public const string IsApPayment = "IsApPayment";
			public const string IsFeePayment = "IsFeePayment";
			public const string IsAssetAuctionPayment = "IsAssetAuctionPayment";
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
			lock (typeof(PaymentTypeMetadata))
			{
				if (PaymentTypeMetadata.mapDelegates == null)
				{
					PaymentTypeMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PaymentTypeMetadata.meta == null)
				{
					PaymentTypeMetadata.meta = new PaymentTypeMetadata();
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

				meta.AddTypeMap("SRPaymentTypeID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentTypeName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsCashierFrontOffice", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsArPayment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsApPayment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFeePayment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAssetAuctionPayment", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "PaymentType";
				meta.Destination = "PaymentType";
				meta.spInsert = "proc_PaymentTypeInsert";
				meta.spUpdate = "proc_PaymentTypeUpdate";
				meta.spDelete = "proc_PaymentTypeDelete";
				meta.spLoadAll = "proc_PaymentTypeLoadAll";
				meta.spLoadByPrimaryKey = "proc_PaymentTypeLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PaymentTypeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
